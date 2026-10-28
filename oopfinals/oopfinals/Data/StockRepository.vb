' ==============================================================================
' Project: Mini-Grocery Store POS & Daily Inventory System
' Course: Object-Oriented Programming (ITP 102) - University of Caloocan City
' Class: StockRepository
' OOP Concept: TRANSACTION MANAGEMENT & DATA ACCESS
' Manages inventory deliveries (Stock-In), adjustments, and audit trail movements
' inside atomic Access (OleDb) transactions.
'
' OLEDB NOTE: with conditional WHERE clauses the typed parameters MUST be added
' in the exact order their "?" placeholders appear in the built SQL text.
' ==============================================================================

Option Strict On
Option Explicit On

Imports System
Imports System.Collections.Generic
Imports System.Data.OleDb
Imports oopfinals.Models

Namespace Data
    Public Class StockRepository
        ''' <summary>
        ''' Records a stock delivery from a supplier inside an atomic database TRANSACTION.
        ''' </summary>
        Public Function RecordStockIn(productId As Integer, quantity As Integer, supplierId As Nullable(Of Integer),
                                      userId As Integer, newExpiryDate As Nullable(Of DateTime), reason As String) As Boolean
            If quantity <= 0 Then Throw New ArgumentException("Stock-in quantity must be greater than zero.")
            If quantity > 10000 Then Throw New ArgumentException("Stock-in quantity must not exceed 10,000 units per delivery.")
            If newExpiryDate.HasValue AndAlso newExpiryDate.Value.Date <= DateTime.Today Then
                Throw New InvalidOperationException("Expired stock must be rejected, not received. Record a disposal adjustment instead.")
            End If

            Using conn As OleDbConnection = DatabaseHelper.GetConnection()
                Using trans As OleDbTransaction = conn.BeginTransaction()
                    Try
                        ' 1. Update product quantity and optionally expiry date.
                        '    (OleDb binds by position: @qty, then optional @exp, then @pid.)
                        Dim updateSql As String = "UPDATE Products SET StockQty = StockQty + ? "
                        If newExpiryDate.HasValue Then
                            updateSql &= ", ExpiryDate = ? "
                        End If
                        updateSql &= " WHERE ProductId = ?;"

                        Using cmdProd As New OleDbCommand(updateSql, conn, trans)
                            DatabaseHelper.AddLong(cmdProd, quantity)
                            If newExpiryDate.HasValue Then
                                DatabaseHelper.AddDate(cmdProd, newExpiryDate.Value.Date)
                            End If
                            DatabaseHelper.AddLong(cmdProd, productId)
                            cmdProd.ExecuteNonQuery()
                        End Using

                        ' 2. Log StockMovement
                        Using cmdMove As New OleDbCommand("
                            INSERT INTO StockMovements (ProductId, MovementType, QuantityChange, Reason, SupplierId, UserId, MovementDate)
                            VALUES (?, ?, ?, ?, ?, ?, ?);", conn, trans)
                            DatabaseHelper.AddLong(cmdMove, productId)
                            DatabaseHelper.AddText(cmdMove, "StockIn", 20)
                            DatabaseHelper.AddLong(cmdMove, quantity)
                            DatabaseHelper.AddText(cmdMove, If(String.IsNullOrWhiteSpace(reason), "Supplier Delivery", reason.Trim()), 255)
                            DatabaseHelper.AddNullableLong(cmdMove, supplierId)
                            DatabaseHelper.AddLong(cmdMove, userId)
                            DatabaseHelper.AddDate(cmdMove, DateTime.Now)
                            cmdMove.ExecuteNonQuery()
                        End Using

                        trans.Commit()
                        Return True
                    Catch
                        trans.Rollback()
                        Throw
                    End Try
                End Using
            End Using
        End Function

        ''' <summary>
        ''' Records a manual stock correction/adjustment inside an atomic database TRANSACTION.
        ''' Prevents negative final stock.
        ''' </summary>
        Public Function RecordAdjustment(productId As Integer, quantityChange As Integer, reason As String, userId As Integer) As Boolean
            If quantityChange = 0 Then Throw New ArgumentException("Quantity change cannot be zero.")
            If String.IsNullOrWhiteSpace(reason) Then Throw New ArgumentException("A specific adjustment reason is required.")

            Using conn As OleDbConnection = DatabaseHelper.GetConnection()
                Using trans As OleDbTransaction = conn.BeginTransaction()
                    Try
                        ' Check current stock
                        Using checkCmd As New OleDbCommand("SELECT StockQty FROM Products WHERE ProductId = ?;", conn, trans)
                            DatabaseHelper.AddLong(checkCmd, productId)
                            Dim currentQty As Integer = Convert.ToInt32(checkCmd.ExecuteScalar())
                            If currentQty + quantityChange < 0 Then
                                Throw New InvalidOperationException($"Adjustment would result in negative stock. Current: {currentQty}, Adjustment: {quantityChange}.")
                            End If
                        End Using

                        ' Update product stock
                        Using cmdProd As New OleDbCommand("
                            UPDATE Products
                            SET StockQty = StockQty + ?
                            WHERE ProductId = ?;", conn, trans)
                            DatabaseHelper.AddLong(cmdProd, quantityChange)
                            DatabaseHelper.AddLong(cmdProd, productId)
                            cmdProd.ExecuteNonQuery()
                        End Using

                        ' Log Movement
                        Using cmdMove As New OleDbCommand("
                            INSERT INTO StockMovements (ProductId, MovementType, QuantityChange, Reason, SupplierId, UserId, MovementDate)
                            VALUES (?, ?, ?, ?, ?, ?, ?);", conn, trans)
                            DatabaseHelper.AddLong(cmdMove, productId)
                            DatabaseHelper.AddText(cmdMove, "Adjustment", 20)
                            DatabaseHelper.AddLong(cmdMove, quantityChange)
                            DatabaseHelper.AddText(cmdMove, reason.Trim(), 255)
                            DatabaseHelper.AddNullableLong(cmdMove, Nothing)
                            DatabaseHelper.AddLong(cmdMove, userId)
                            DatabaseHelper.AddDate(cmdMove, DateTime.Now)
                            cmdMove.ExecuteNonQuery()
                        End Using

                        trans.Commit()
                        Return True
                    Catch
                        trans.Rollback()
                        Throw
                    End Try
                End Using
            End Using
        End Function

        ''' <summary>
        ''' Retrieves audit movements with optional filters.
        ''' </summary>
        Public Function GetMovements(Optional productId As Nullable(Of Integer) = Nothing,
                                     Optional movementType As String = Nothing,
                                     Optional startDate As Nullable(Of DateTime) = Nothing,
                                     Optional endDate As Nullable(Of DateTime) = Nothing) As List(Of StockMovement)
            Dim list As New List(Of StockMovement)()
            Using conn As OleDbConnection = DatabaseHelper.GetConnection()
                Dim sql As String = "
                    SELECT sm.MovementId, sm.ProductId, p.ProductCode, p.[Name] AS ProductName,
                           sm.MovementType, sm.QuantityChange, sm.Reason, sm.SupplierId,
                           s.[Name] AS SupplierName, sm.UserId, u.[FullName] AS UserName, sm.MovementDate
                    FROM (((StockMovements sm
                    LEFT JOIN Products p ON sm.ProductId = p.ProductId)
                    LEFT JOIN Suppliers s ON sm.SupplierId = s.SupplierId)
                    LEFT JOIN Users u ON sm.UserId = u.UserId)
                    WHERE 1=1 "

                If productId.HasValue AndAlso productId.Value > 0 Then
                    sql &= " AND sm.ProductId = ?"
                End If

                If Not String.IsNullOrWhiteSpace(movementType) AndAlso movementType <> "All" Then
                    sql &= " AND sm.MovementType = ?"
                End If

                If startDate.HasValue Then
                    sql &= " AND sm.MovementDate >= ?"
                End If

                If endDate.HasValue Then
                    ' Exclusive end (next day 00:00) so no time-of-day is lost
                    sql &= " AND sm.MovementDate < ?"
                End If

                sql &= " ORDER BY sm.MovementDate DESC;"

                Using cmd As New OleDbCommand(sql, conn)
                    ' Parameters are positional: added strictly in WHERE-clause order
                    If productId.HasValue AndAlso productId.Value > 0 Then
                        DatabaseHelper.AddLong(cmd, productId.Value)
                    End If
                    If Not String.IsNullOrWhiteSpace(movementType) AndAlso movementType <> "All" Then
                        DatabaseHelper.AddText(cmd, movementType, 20)
                    End If
                    If startDate.HasValue Then
                        DatabaseHelper.AddDate(cmd, startDate.Value.Date)
                    End If
                    If endDate.HasValue Then
                        DatabaseHelper.AddDate(cmd, endDate.Value.Date.AddDays(1))
                    End If

                    Using reader As OleDbDataReader = cmd.ExecuteReader()
                        While reader.Read()
                            Dim m As New StockMovement()
                            m.MovementId = Convert.ToInt32(reader("MovementId"))
                            m.ProductId = Convert.ToInt32(reader("ProductId"))
                            m.ProductCode = If(reader("ProductCode") IsNot DBNull.Value, Convert.ToString(reader("ProductCode")), "N/A")
                            m.ProductName = If(reader("ProductName") IsNot DBNull.Value, Convert.ToString(reader("ProductName")), "N/A")
                            m.MovementType = Convert.ToString(reader("MovementType"))
                            m.QuantityChange = Convert.ToInt32(reader("QuantityChange"))
                            m.Reason = If(reader("Reason") IsNot DBNull.Value, Convert.ToString(reader("Reason")), "")
                            If reader("SupplierId") IsNot DBNull.Value Then
                                m.SupplierId = Convert.ToInt32(reader("SupplierId"))
                            End If
                            m.SupplierName = If(reader("SupplierName") IsNot DBNull.Value, Convert.ToString(reader("SupplierName")), "N/A")
                            m.UserId = Convert.ToInt32(reader("UserId"))
                            m.UserName = If(reader("UserName") IsNot DBNull.Value, Convert.ToString(reader("UserName")), "System")
                            m.MovementDate = DatabaseHelper.ReadDateTime(reader, "MovementDate")
                            list.Add(m)
                        End While
                    End Using
                End Using
            End Using
            Return list
        End Function
    End Class
End Namespace
