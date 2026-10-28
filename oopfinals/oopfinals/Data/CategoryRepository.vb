' ==============================================================================
' Project: Mini-Grocery Store POS & Daily Inventory System
' Course: Object-Oriented Programming (ITP 102) - University of Caloocan City
' Class: CategoryRepository
' OOP Concept: DATA ACCESS ENCAPSULATION
' Manages Category CRUD operations with parameterized OleDb queries against
' Microsoft Access.
' ==============================================================================

Option Strict On
Option Explicit On

Imports System
Imports System.Collections.Generic
Imports System.Data.OleDb
Imports oopfinals.Models

Namespace Data
    Public Class CategoryRepository
        Public Function GetAll() As List(Of Category)
            Dim list As New List(Of Category)()
            Using conn As OleDbConnection = DatabaseHelper.GetConnection()
                Using cmd As New OleDbCommand("SELECT CategoryId, [Name] FROM Categories ORDER BY [Name];", conn)
                    Using reader As OleDbDataReader = cmd.ExecuteReader()
                        While reader.Read()
                            list.Add(New Category(Convert.ToInt32(reader("CategoryId")), Convert.ToString(reader("Name"))))
                        End While
                    End Using
                End Using
            End Using
            Return list
        End Function

        Public Function GetById(id As Integer) As Category
            Using conn As OleDbConnection = DatabaseHelper.GetConnection()
                Using cmd As New OleDbCommand("SELECT CategoryId, [Name] FROM Categories WHERE CategoryId = ?;", conn)
                    DatabaseHelper.AddLong(cmd, id)
                    Using reader As OleDbDataReader = cmd.ExecuteReader()
                        If reader.Read() Then
                            Return New Category(Convert.ToInt32(reader("CategoryId")), Convert.ToString(reader("Name")))
                        End If
                    End Using
                End Using
            End Using
            Return Nothing
        End Function

        Public Function Add(name As String) As Integer
            Using conn As OleDbConnection = DatabaseHelper.GetConnection()
                Using cmd As New OleDbCommand("INSERT INTO Categories ([Name]) VALUES (?);", conn)
                    DatabaseHelper.AddText(cmd, name.Trim(), 90)
                    Return DatabaseHelper.InsertAndGetIdentity(cmd)
                End Using
            End Using
        End Function

        Public Function Update(id As Integer, name As String) As Boolean
            Using conn As OleDbConnection = DatabaseHelper.GetConnection()
                Using cmd As New OleDbCommand("UPDATE Categories SET [Name] = ? WHERE CategoryId = ?;", conn)
                    DatabaseHelper.AddText(cmd, name.Trim(), 90)
                    DatabaseHelper.AddLong(cmd, id)
                    Return cmd.ExecuteNonQuery() > 0
                End Using
            End Using
        End Function

        Public Function Delete(id As Integer) As Boolean
            ' Verify no products attached
            Using conn As OleDbConnection = DatabaseHelper.GetConnection()
                Using checkCmd As New OleDbCommand("SELECT COUNT(*) FROM Products WHERE CategoryId = ? AND IsActive = 1;", conn)
                    DatabaseHelper.AddLong(checkCmd, id)
                    If Convert.ToInt32(checkCmd.ExecuteScalar()) > 0 Then
                        Throw New InvalidOperationException("Cannot delete category because it contains active products.")
                    End If
                End Using

                Using cmd As New OleDbCommand("DELETE FROM Categories WHERE CategoryId = ?;", conn)
                    DatabaseHelper.AddLong(cmd, id)
                    Return cmd.ExecuteNonQuery() > 0
                End Using
            End Using
        End Function
    End Class
End Namespace
