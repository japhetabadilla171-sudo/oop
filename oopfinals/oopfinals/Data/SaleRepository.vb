' ==============================================================================
' Project: Mini-Grocery Store POS & Daily Inventory System
' Course: Object-Oriented Programming (ITP 102) - University of Caloocan City
' Class: SaleRepository
' OOP Concept: TRANSACTION MANAGEMENT & DATA ACCESS
' Manages POS checkout recording inside an atomic Access (OleDb) transaction,
' ensuring Sales, SaleItems, Stock updates, and StockMovements stay consistent.
' ==============================================================================

Option Strict On
Option Explicit On

Imports System
Imports System.Collections.Generic
Imports System.Data.OleDb
Imports oopfinals.Models

Namespace Data
    Public Class SaleRepository
        ''' <summary>
        ''' Generates a unique sequential receipt number: RCP-yyyyMMdd-XXXX
        ''' </summary>
        Public Function GenerateReceiptNumber() As String
            Dim todayPrefix As String = "RCP-" & DateTime.Today.ToString("yyyyMMdd") & "-"
            Using conn As OleDbConnection = DatabaseHelper.GetConnection()
                Using cmd As New OleDbCommand("
                    SELECT COUNT(*)
                    FROM Sales
                    WHERE ReceiptNo LIKE ?;", conn)
                    DatabaseHelper.AddText(cmd, todayPrefix & "%", 40)
                    Dim countToday As Long = Convert.ToInt64(cmd.ExecuteScalar())
                    Return todayPrefix & (countToday + 1).ToString("D4")
                End Using
            End Using
        End Function

        ''' <summary>
        ''' Records a complete POS sale inside a database TRANSACTION.
        ''' Inserts Sale, inserts SaleItems, decrements product inventory, and records StockMovements.
        ''' </summary>
        Public Function RecordSale(sale As Sale, items As List(Of SaleItem)) As Integer
            If sale Is Nothing Then Throw New ArgumentNullException(NameOf(sale))
            If items Is Nothing OrElse items.Count = 0 Then Throw New ArgumentException("Sale must have at least one line item.")

            Using conn As OleDbConnection = DatabaseHelper.GetConnection()
                Using trans As OleDbTransaction = conn.BeginTransaction()
                    Try
                        ' 1. Insert into Sales
                        Dim saleId As Integer
                        Using cmdSale As New OleDbCommand("
                            INSERT INTO Sales (ReceiptNo, SaleDate, CashierId, CustomerId, Subtotal, DiscountType, DiscountAmount, Total, CashReceived, ChangeAmount)
                            VALUES (?, ?, ?, ?, ?, ?, ?, ?, ?, ?);", conn, trans)
                            DatabaseHelper.AddText(cmdSale, sale.ReceiptNo, 40)
                            DatabaseHelper.AddDate(cmdSale, sale.SaleDate)
                            DatabaseHelper.AddLong(cmdSale, sale.CashierId)
                            DatabaseHelper.AddNullableLong(cmdSale, sale.CustomerId)
                            DatabaseHelper.AddCurrency(cmdSale, sale.Subtotal)
                            DatabaseHelper.AddText(cmdSale, sale.DiscountType, 40)
                            DatabaseHelper.AddCurrency(cmdSale, sale.DiscountAmount)
                            DatabaseHelper.AddCurrency(cmdSale, sale.Total)
                            DatabaseHelper.AddCurrency(cmdSale, sale.CashReceived)
                            DatabaseHelper.AddCurrency(cmdSale, sale.ChangeAmount)

                            saleId = DatabaseHelper.InsertAndGetIdentity(cmdSale)
                            sale.SaleId = saleId
                        End Using

                        ' 2. Insert line items, deduct inventory, and record stock movements
                        For Each item In items
                            item.SaleId = saleId

                            ' Insert SaleItem
                            Using cmdItem As New OleDbCommand("
                                INSERT INTO SaleItems (SaleId, ProductId, Quantity, UnitPrice, LineTotal)
                                VALUES (?, ?, ?, ?, ?);", conn, trans)
                                DatabaseHelper.AddLong(cmdItem, saleId)
                                DatabaseHelper.AddLong(cmdItem, item.ProductId)
                                DatabaseHelper.AddLong(cmdItem, item.Quantity)
                                DatabaseHelper.AddCurrency(cmdItem, item.UnitPrice)
                                DatabaseHelper.AddCurrency(cmdItem, item.LineTotal)
                                item.SaleItemId = DatabaseHelper.InsertAndGetIdentity(cmdItem)
                            End Using

                            ' Deduct inventory from Products (guard against insufficient stock)
                            Using cmdStock As New OleDbCommand("
                                UPDATE Products
                                SET StockQty = StockQty - ?
                                WHERE ProductId = ? AND StockQty >= ?;", conn, trans)
                                DatabaseHelper.AddLong(cmdStock, item.Quantity)
                                DatabaseHelper.AddLong(cmdStock, item.ProductId)
                                DatabaseHelper.AddLong(cmdStock, item.Quantity)
                                Dim affected As Integer = cmdStock.ExecuteNonQuery()
                                If affected = 0 Then
                                    Throw New InvalidOperationException($"Insufficient stock for a cart item during checkout. Transaction cancelled.")
                                End If
                            End Using

                            ' Log audit trail in StockMovements
                            Using cmdMove As New OleDbCommand("
                                INSERT INTO StockMovements (ProductId, MovementType, QuantityChange, Reason, SupplierId, UserId, MovementDate)
                                VALUES (?, ?, ?, ?, ?, ?, ?);", conn, trans)
                                DatabaseHelper.AddLong(cmdMove, item.ProductId)
                                DatabaseHelper.AddText(cmdMove, "Sale", 20)
                                DatabaseHelper.AddLong(cmdMove, -item.Quantity)
                                DatabaseHelper.AddText(cmdMove, "POS Sale: " & sale.ReceiptNo, 255)
                                DatabaseHelper.AddNullableLong(cmdMove, Nothing) ' sales have no supplier
                                DatabaseHelper.AddLong(cmdMove, sale.CashierId)
                                DatabaseHelper.AddDate(cmdMove, sale.SaleDate)
                                cmdMove.ExecuteNonQuery()
                            End Using
                        Next

                        trans.Commit()
                        Return saleId
                    Catch
                        trans.Rollback()
                        Throw
                    End Try
                End Using
            End Using
        End Function

        Private Function MapSale(reader As OleDbDataReader) As Sale
            Dim s As New Sale()
            s.SaleId = Convert.ToInt32(reader("SaleId"))
            s.ReceiptNo = Convert.ToString(reader("ReceiptNo"))
            s.SaleDate = DatabaseHelper.ReadDateTime(reader, "SaleDate")
            s.CashierId = Convert.ToInt32(reader("CashierId"))
            s.CashierName = If(reader("CashierName") IsNot DBNull.Value, Convert.ToString(reader("CashierName")), "Cashier")

            If reader("CustomerId") IsNot DBNull.Value Then
                s.CustomerId = Convert.ToInt32(reader("CustomerId"))
            End If
            s.CustomerName = If(reader("CustomerName") IsNot DBNull.Value, Convert.ToString(reader("CustomerName")), "Walk-In")

            s.Subtotal = Convert.ToDecimal(reader("Subtotal"))
            s.DiscountType = Convert.ToString(reader("DiscountType"))
            s.DiscountAmount = Convert.ToDecimal(reader("DiscountAmount"))
            s.Total = Convert.ToDecimal(reader("Total"))
            s.CashReceived = Convert.ToDecimal(reader("CashReceived"))
            s.ChangeAmount = Convert.ToDecimal(reader("ChangeAmount"))
            Return s
        End Function

        ' Shared column list for the sale SELECT statements below.
        ' Jet requires parenthesized nesting for chained joins in one FROM clause.
        Private Const SaleSelectColumns As String = "
                    SELECT s.SaleId, s.ReceiptNo, s.SaleDate, s.CashierId, uCash.[FullName] AS CashierName,
                           s.CustomerId, uCust.[FullName] AS CustomerName, s.Subtotal, s.DiscountType,
                           s.DiscountAmount, s.Total, s.CashReceived, s.ChangeAmount
                    FROM (Sales s
                    LEFT JOIN Users uCash ON s.CashierId = uCash.UserId)
                    LEFT JOIN Users uCust ON s.CustomerId = uCust.UserId"

        Public Function GetById(saleId As Integer) As Sale
            Using conn As OleDbConnection = DatabaseHelper.GetConnection()
                Using cmd As New OleDbCommand(SaleSelectColumns & "
                    WHERE s.SaleId = ?;", conn)
                    DatabaseHelper.AddLong(cmd, saleId)
                    Using reader As OleDbDataReader = cmd.ExecuteReader()
                        If reader.Read() Then
                            Dim sale As Sale = MapSale(reader)
                            sale.Items = GetSaleItems(sale.SaleId)
                            Return sale
                        End If
                    End Using
                End Using
            End Using
            Return Nothing
        End Function

        Public Function GetByReceiptNo(receiptNo As String) As Sale
            Using conn As OleDbConnection = DatabaseHelper.GetConnection()
                Using cmd As New OleDbCommand(SaleSelectColumns & "
                    WHERE s.ReceiptNo = ?;", conn)
                    DatabaseHelper.AddText(cmd, receiptNo.Trim(), 40)
                    Using reader As OleDbDataReader = cmd.ExecuteReader()
                        If reader.Read() Then
                            Dim sale As Sale = MapSale(reader)
                            sale.Items = GetSaleItems(sale.SaleId)
                            Return sale
                        End If
                    End Using
                End Using
            End Using
            Return Nothing
        End Function

        Public Function GetSaleItems(saleId As Integer) As List(Of SaleItem)
            Dim list As New List(Of SaleItem)()
            Using conn As OleDbConnection = DatabaseHelper.GetConnection()
                Using cmd As New OleDbCommand("
                    SELECT si.SaleItemId, si.SaleId, si.ProductId, p.ProductCode, p.[Name] AS ProductName,
                           si.Quantity, si.UnitPrice, si.LineTotal
                    FROM SaleItems si
                    LEFT JOIN Products p ON si.ProductId = p.ProductId
                    WHERE si.SaleId = ?
                    ORDER BY si.SaleItemId;", conn)
                    DatabaseHelper.AddLong(cmd, saleId)
                    Using reader As OleDbDataReader = cmd.ExecuteReader()
                        While reader.Read()
                            Dim item As New SaleItem()
                            item.SaleItemId = Convert.ToInt32(reader("SaleItemId"))
                            item.SaleId = Convert.ToInt32(reader("SaleId"))
                            item.ProductId = Convert.ToInt32(reader("ProductId"))
                            item.ProductCode = If(reader("ProductCode") IsNot DBNull.Value, Convert.ToString(reader("ProductCode")), "N/A")
                            item.ProductName = If(reader("ProductName") IsNot DBNull.Value, Convert.ToString(reader("ProductName")), "Unknown Item")
                            item.Quantity = Convert.ToInt32(reader("Quantity"))
                            item.UnitPrice = Convert.ToDecimal(reader("UnitPrice"))
                            list.Add(item)
                        End While
                    End Using
                End Using
            End Using
            Return list
        End Function

        ''' <summary>
        ''' Date-range queries use an EXCLUSIVE end (next day 00:00) so matches at
        ''' 00:00 sharp are never double-counted and nothing at the day edge is lost.
        ''' </summary>
        Public Function GetSalesByDate(targetDate As DateTime) As List(Of Sale)
            Return GetSalesBetween(targetDate.Date, targetDate.Date.AddDays(1))
        End Function

        Public Function GetSalesByDateRange(startDate As DateTime, endDate As DateTime) As List(Of Sale)
            Dim start As DateTime = startDate.Date
            Dim exclusiveEnd As DateTime = endDate.Date.AddDays(1)
            Return GetSalesBetween(start, exclusiveEnd)
        End Function

        Private Function GetSalesBetween(inclusiveStart As DateTime, exclusiveEnd As DateTime) As List(Of Sale)
            Dim list As New List(Of Sale)()
            Using conn As OleDbConnection = DatabaseHelper.GetConnection()
                Using cmd As New OleDbCommand(SaleSelectColumns & "
                    WHERE s.SaleDate >= ? AND s.SaleDate < ?
                    ORDER BY s.SaleDate DESC;", conn)
                    DatabaseHelper.AddDate(cmd, inclusiveStart)
                    DatabaseHelper.AddDate(cmd, exclusiveEnd)
                    Using reader As OleDbDataReader = cmd.ExecuteReader()
                        While reader.Read()
                            list.Add(MapSale(reader))
                        End While
                    End Using
                End Using
            End Using
            Return list
        End Function

        Public Function GetSalesByCustomer(customerId As Integer) As List(Of Sale)
            Dim list As New List(Of Sale)()
            Using conn As OleDbConnection = DatabaseHelper.GetConnection()
                Using cmd As New OleDbCommand(SaleSelectColumns & "
                    WHERE s.CustomerId = ?
                    ORDER BY s.SaleDate DESC;", conn)
                    DatabaseHelper.AddLong(cmd, customerId)
                    Using reader As OleDbDataReader = cmd.ExecuteReader()
                        While reader.Read()
                            list.Add(MapSale(reader))
                        End While
                    End Using
                End Using
            End Using
            Return list
        End Function

        Public Function GetSalesByCashierAndDate(cashierId As Integer, targetDate As DateTime) As List(Of Sale)
            Dim list As New List(Of Sale)()
            Using conn As OleDbConnection = DatabaseHelper.GetConnection()
                Using cmd As New OleDbCommand(SaleSelectColumns & "
                    WHERE s.CashierId = ? AND s.SaleDate >= ? AND s.SaleDate < ?
                    ORDER BY s.SaleDate DESC;", conn)
                    DatabaseHelper.AddLong(cmd, cashierId)
                    DatabaseHelper.AddDate(cmd, targetDate.Date)
                    DatabaseHelper.AddDate(cmd, targetDate.Date.AddDays(1))
                    Using reader As OleDbDataReader = cmd.ExecuteReader()
                        While reader.Read()
                            list.Add(MapSale(reader))
                        End While
                    End Using
                End Using
            End Using
            Return list
        End Function
    End Class
End Namespace
