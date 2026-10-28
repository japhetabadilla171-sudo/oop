' ==============================================================================
' Project: Mini-Grocery Store POS & Daily Inventory System
' Course: Object-Oriented Programming (ITP 102) - University of Caloocan City
' Class: ProductRepository
' OOP Concept: DATA ACCESS ENCAPSULATION
' Manages Product persistence, search, stock thresholds, and expiry queries
' using parameterized OleDb commands against Microsoft Access.
' ==============================================================================

Option Strict On
Option Explicit On

Imports System
Imports System.Collections.Generic
Imports System.Data.OleDb
Imports oopfinals.Models

Namespace Data
    Public Class ProductRepository
        Private Function MapProduct(reader As OleDbDataReader) As Product
            Dim p As New Product()
            p.ProductId = Convert.ToInt32(reader("ProductId"))
            p.ProductCode = Convert.ToString(reader("ProductCode"))
            p.Name = Convert.ToString(reader("Name"))
            p.CategoryId = Convert.ToInt32(reader("CategoryId"))
            p.CategoryName = If(reader("CategoryName") IsNot DBNull.Value, Convert.ToString(reader("CategoryName")), "Uncategorized")
            p.SupplierId = Convert.ToInt32(reader("SupplierId"))
            p.SupplierName = If(reader("SupplierName") IsNot DBNull.Value, Convert.ToString(reader("SupplierName")), "Unknown Supplier")
            p.Price = Convert.ToDecimal(reader("Price"))
            p.CostPrice = Convert.ToDecimal(reader("CostPrice"))
            p.StockQty = Convert.ToInt32(reader("StockQty"))
            p.ReorderLevel = Convert.ToInt32(reader("ReorderLevel"))

            p.ExpiryDate = DatabaseHelper.ReadNullableDateTime(reader, "ExpiryDate")

            p.IsActive = Convert.ToInt32(reader("IsActive")) = 1
            Return p
        End Function

        Public Function GetAll(Optional activeOnly As Boolean = True) As List(Of Product)
            Dim list As New List(Of Product)()
            Using conn As OleDbConnection = DatabaseHelper.GetConnection()
                Dim sql As String = "
                    SELECT p.ProductId, p.ProductCode, p.[Name], p.CategoryId, c.[Name] AS CategoryName,
                           p.SupplierId, s.[Name] AS SupplierName, p.Price, p.CostPrice, p.StockQty,
                           p.ReorderLevel, p.ExpiryDate, p.IsActive
                    FROM ((Products p
                    LEFT JOIN Categories c ON p.CategoryId = c.CategoryId)
                    LEFT JOIN Suppliers s ON p.SupplierId = s.SupplierId) "

                If activeOnly Then
                    sql &= " WHERE p.IsActive = 1 "
                End If
                sql &= " ORDER BY p.[Name];"

                Using cmd As New OleDbCommand(sql, conn)
                    Using reader As OleDbDataReader = cmd.ExecuteReader()
                        While reader.Read()
                            list.Add(MapProduct(reader))
                        End While
                    End Using
                End Using
            End Using
            Return list
        End Function

        Public Function Search(keyword As String, Optional categoryId As Nullable(Of Integer) = Nothing, Optional activeOnly As Boolean = True) As List(Of Product)
            Dim list As New List(Of Product)()
            Using conn As OleDbConnection = DatabaseHelper.GetConnection()
                Dim sql As String = "
                    SELECT p.ProductId, p.ProductCode, p.[Name], p.CategoryId, c.[Name] AS CategoryName,
                           p.SupplierId, s.[Name] AS SupplierName, p.Price, p.CostPrice, p.StockQty,
                           p.ReorderLevel, p.ExpiryDate, p.IsActive
                    FROM ((Products p
                    LEFT JOIN Categories c ON p.CategoryId = c.CategoryId)
                    LEFT JOIN Suppliers s ON p.SupplierId = s.SupplierId)
                    WHERE 1=1 "

                If activeOnly Then
                    sql &= " AND p.IsActive = 1 "
                End If

                If categoryId.HasValue AndAlso categoryId.Value > 0 Then
                    sql &= " AND p.CategoryId = ? "
                End If

                If Not String.IsNullOrWhiteSpace(keyword) Then
                    sql &= " AND (LCASE(p.[Name]) LIKE ? OR LCASE(p.ProductCode) LIKE ?) "
                End If

                sql &= " ORDER BY p.[Name];"

                Using cmd As New OleDbCommand(sql, conn)
                    ' OleDb binds parameters by POSITION: the added order must
                    ' follow the order the "?" placeholders appear above.
                    If categoryId.HasValue AndAlso categoryId.Value > 0 Then
                        DatabaseHelper.AddLong(cmd, categoryId.Value)
                    End If

                    If Not String.IsNullOrWhiteSpace(keyword) Then
                        Dim kw As String = "%" & keyword.Trim().ToLowerInvariant() & "%"
                        DatabaseHelper.AddText(cmd, kw, 160)
                        DatabaseHelper.AddText(cmd, kw, 40)
                    End If

                    Using reader As OleDbDataReader = cmd.ExecuteReader()
                        While reader.Read()
                            list.Add(MapProduct(reader))
                        End While
                    End Using
                End Using
            End Using
            Return list
        End Function

        Public Function GetById(productId As Integer) As Product
            Using conn As OleDbConnection = DatabaseHelper.GetConnection()
                Using cmd As New OleDbCommand("
                    SELECT TOP 1 p.ProductId, p.ProductCode, p.[Name], p.CategoryId, c.[Name] AS CategoryName,
                           p.SupplierId, s.[Name] AS SupplierName, p.Price, p.CostPrice, p.StockQty,
                           p.ReorderLevel, p.ExpiryDate, p.IsActive
                    FROM ((Products p
                    LEFT JOIN Categories c ON p.CategoryId = c.CategoryId)
                    LEFT JOIN Suppliers s ON p.SupplierId = s.SupplierId)
                    WHERE p.ProductId = ?;", conn)
                    DatabaseHelper.AddLong(cmd, productId)
                    Using reader As OleDbDataReader = cmd.ExecuteReader()
                        If reader.Read() Then
                            Return MapProduct(reader)
                        End If
                    End Using
                End Using
            End Using
            Return Nothing
        End Function

        Public Function GetByCode(code As String) As Product
            Using conn As OleDbConnection = DatabaseHelper.GetConnection()
                Using cmd As New OleDbCommand("
                    SELECT TOP 1 p.ProductId, p.ProductCode, p.[Name], p.CategoryId, c.[Name] AS CategoryName,
                           p.SupplierId, s.[Name] AS SupplierName, p.Price, p.CostPrice, p.StockQty,
                           p.ReorderLevel, p.ExpiryDate, p.IsActive
                    FROM ((Products p
                    LEFT JOIN Categories c ON p.CategoryId = c.CategoryId)
                    LEFT JOIN Suppliers s ON p.SupplierId = s.SupplierId)
                    WHERE UCASE(p.ProductCode) = UCASE(?);", conn)
                    DatabaseHelper.AddText(cmd, code.Trim(), 30)
                    Using reader As OleDbDataReader = cmd.ExecuteReader()
                        If reader.Read() Then
                            Return MapProduct(reader)
                        End If
                    End Using
                End Using
            End Using
            Return Nothing
        End Function

        Public Function Add(p As Product) As Integer
            Using conn As OleDbConnection = DatabaseHelper.GetConnection()
                Using cmd As New OleDbCommand("
                    INSERT INTO Products (ProductCode, [Name], CategoryId, SupplierId, Price, CostPrice, StockQty, ReorderLevel, ExpiryDate, IsActive)
                    VALUES (?, ?, ?, ?, ?, ?, ?, ?, ?, ?);", conn)
                    DatabaseHelper.AddText(cmd, p.ProductCode, 30)
                    DatabaseHelper.AddText(cmd, p.Name, 150)
                    DatabaseHelper.AddLong(cmd, p.CategoryId)
                    DatabaseHelper.AddLong(cmd, p.SupplierId)
                    DatabaseHelper.AddCurrency(cmd, p.Price)
                    DatabaseHelper.AddCurrency(cmd, p.CostPrice)
                    DatabaseHelper.AddLong(cmd, p.StockQty)
                    DatabaseHelper.AddLong(cmd, p.ReorderLevel)
                    DatabaseHelper.AddNullableDate(cmd, p.ExpiryDate)
                    DatabaseHelper.AddLong(cmd, If(p.IsActive, 1L, 0L))

                    Dim newId As Integer = DatabaseHelper.InsertAndGetIdentity(cmd)
                    p.ProductId = newId
                    Return newId
                End Using
            End Using
        End Function

        Public Function Update(p As Product) As Boolean
            Using conn As OleDbConnection = DatabaseHelper.GetConnection()
                Using cmd As New OleDbCommand("
                    UPDATE Products
                    SET ProductCode = ?, [Name] = ?, CategoryId = ?, SupplierId = ?,
                        Price = ?, CostPrice = ?, StockQty = ?, ReorderLevel = ?,
                        ExpiryDate = ?, IsActive = ?
                    WHERE ProductId = ?;", conn)
                    DatabaseHelper.AddText(cmd, p.ProductCode, 30)
                    DatabaseHelper.AddText(cmd, p.Name, 150)
                    DatabaseHelper.AddLong(cmd, p.CategoryId)
                    DatabaseHelper.AddLong(cmd, p.SupplierId)
                    DatabaseHelper.AddCurrency(cmd, p.Price)
                    DatabaseHelper.AddCurrency(cmd, p.CostPrice)
                    DatabaseHelper.AddLong(cmd, p.StockQty)
                    DatabaseHelper.AddLong(cmd, p.ReorderLevel)
                    DatabaseHelper.AddNullableDate(cmd, p.ExpiryDate)
                    DatabaseHelper.AddLong(cmd, If(p.IsActive, 1L, 0L))
                    DatabaseHelper.AddLong(cmd, p.ProductId)

                    Return cmd.ExecuteNonQuery() > 0
                End Using
            End Using
        End Function

        Public Function UpdatePrice(productId As Integer, newPrice As Decimal, newCostPrice As Decimal) As Boolean
            Using conn As OleDbConnection = DatabaseHelper.GetConnection()
                Using cmd As New OleDbCommand("
                    UPDATE Products
                    SET Price = ?, CostPrice = ?
                    WHERE ProductId = ?;", conn)
                    DatabaseHelper.AddCurrency(cmd, newPrice)
                    DatabaseHelper.AddCurrency(cmd, newCostPrice)
                    DatabaseHelper.AddLong(cmd, productId)
                    Return cmd.ExecuteNonQuery() > 0
                End Using
            End Using
        End Function

        Public Function DeleteOrDeactivate(productId As Integer) As Boolean
            Using conn As OleDbConnection = DatabaseHelper.GetConnection()
                Using cmd As New OleDbCommand("UPDATE Products SET IsActive = 0 WHERE ProductId = ?;", conn)
                    DatabaseHelper.AddLong(cmd, productId)
                    Return cmd.ExecuteNonQuery() > 0
                End Using
            End Using
        End Function

        Public Function GetLowStockProducts() As List(Of Product)
            Dim list As New List(Of Product)()
            For Each p In GetAll(activeOnly:=True)
                If p.IsLowStock() Then
                    list.Add(p)
                End If
            Next
            Return list
        End Function

        Public Function GetExpiringProducts(Optional daysThreshold As Integer = 30) As List(Of Product)
            Dim list As New List(Of Product)()
            For Each p In GetAll(activeOnly:=True)
                If p.IsExpiringSoon(daysThreshold) Then
                    list.Add(p)
                End If
            Next
            Return list
        End Function

        Public Function GetExpiredProducts() As List(Of Product)
            Dim list As New List(Of Product)()
            For Each p In GetAll(activeOnly:=True)
                If p.IsExpired() Then
                    list.Add(p)
                End If
            Next
            Return list
        End Function
    End Class
End Namespace
