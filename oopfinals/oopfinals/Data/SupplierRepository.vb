' ==============================================================================
' Project: Mini-Grocery Store POS & Daily Inventory System
' Course: Object-Oriented Programming (ITP 102) - University of Caloocan City
' Class: SupplierRepository
' OOP Concept: DATA ACCESS ENCAPSULATION
' Manages Supplier CRUD operations with parameterized OleDb queries against
' Microsoft Access.
' ==============================================================================

Option Strict On
Option Explicit On

Imports System
Imports System.Collections.Generic
Imports System.Data.OleDb
Imports oopfinals.Models

Namespace Data
    Public Class SupplierRepository
        Public Function GetAll() As List(Of Supplier)
            Dim list As New List(Of Supplier)()
            Using conn As OleDbConnection = DatabaseHelper.GetConnection()
                Using cmd As New OleDbCommand("
                    SELECT SupplierId, [Name], ContactNumber, [Address]
                    FROM Suppliers ORDER BY [Name];", conn)
                    Using reader As OleDbDataReader = cmd.ExecuteReader()
                        While reader.Read()
                            list.Add(New Supplier(Convert.ToInt32(reader("SupplierId")),
                                                  Convert.ToString(reader("Name")),
                                                  Convert.ToString(reader("ContactNumber")),
                                                  Convert.ToString(reader("Address"))))
                        End While
                    End Using
                End Using
            End Using
            Return list
        End Function

        Public Function GetById(id As Integer) As Supplier
            Using conn As OleDbConnection = DatabaseHelper.GetConnection()
                Using cmd As New OleDbCommand("
                    SELECT SupplierId, [Name], ContactNumber, [Address]
                    FROM Suppliers WHERE SupplierId = ?;", conn)
                    DatabaseHelper.AddLong(cmd, id)
                    Using reader As OleDbDataReader = cmd.ExecuteReader()
                        If reader.Read() Then
                            Return New Supplier(Convert.ToInt32(reader("SupplierId")),
                                                Convert.ToString(reader("Name")),
                                                Convert.ToString(reader("ContactNumber")),
                                                Convert.ToString(reader("Address")))
                        End If
                    End Using
                End Using
            End Using
            Return Nothing
        End Function

        Public Function Add(supplier As Supplier) As Integer
            Using conn As OleDbConnection = DatabaseHelper.GetConnection()
                Using cmd As New OleDbCommand("
                    INSERT INTO Suppliers ([Name], ContactNumber, [Address])
                    VALUES (?, ?, ?);", conn)
                    DatabaseHelper.AddText(cmd, supplier.Name, 150)
                    DatabaseHelper.AddText(cmd, supplier.ContactNumber, 30)
                    DatabaseHelper.AddText(cmd, supplier.Address, 255)
                    Return DatabaseHelper.InsertAndGetIdentity(cmd)
                End Using
            End Using
        End Function

        Public Function Update(supplier As Supplier) As Boolean
            Using conn As OleDbConnection = DatabaseHelper.GetConnection()
                Using cmd As New OleDbCommand("
                    UPDATE Suppliers
                    SET [Name] = ?, ContactNumber = ?, [Address] = ?
                    WHERE SupplierId = ?;", conn)
                    DatabaseHelper.AddText(cmd, supplier.Name, 150)
                    DatabaseHelper.AddText(cmd, supplier.ContactNumber, 30)
                    DatabaseHelper.AddText(cmd, supplier.Address, 255)
                    DatabaseHelper.AddLong(cmd, supplier.SupplierId)
                    Return cmd.ExecuteNonQuery() > 0
                End Using
            End Using
        End Function

        Public Function Delete(id As Integer) As Boolean
            Using conn As OleDbConnection = DatabaseHelper.GetConnection()
                Using checkCmd As New OleDbCommand("SELECT COUNT(*) FROM Products WHERE SupplierId = ? AND IsActive = 1;", conn)
                    DatabaseHelper.AddLong(checkCmd, id)
                    If Convert.ToInt32(checkCmd.ExecuteScalar()) > 0 Then
                        Throw New InvalidOperationException("Cannot delete supplier because active products are assigned to this supplier.")
                    End If
                End Using

                Using cmd As New OleDbCommand("DELETE FROM Suppliers WHERE SupplierId = ?;", conn)
                    DatabaseHelper.AddLong(cmd, id)
                    Return cmd.ExecuteNonQuery() > 0
                End Using
            End Using
        End Function
    End Class
End Namespace
