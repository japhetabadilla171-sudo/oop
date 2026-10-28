' ==============================================================================
' Project: Mini-Grocery Store POS & Daily Inventory System
' Course: Object-Oriented Programming (ITP 102) - University of Caloocan City
' Class: UserRepository
' OOP Concept: DATA ACCESS & FACTORY POLYMORPHISM
' Handles database operations for User entities using parameterized OleDb
' queries against Microsoft Access.
' Dynamically instantiates the concrete polymorphic subclass (StoreOwner,
' Cashier, InventoryClerk, or Customer) based on the database role.
'
' OLEDB NOTE: parameters are positional ("?"), so every typed parameter is
' added in the exact order it appears in the SQL text.
' ==============================================================================

Option Strict On
Option Explicit On

Imports System
Imports System.Collections.Generic
Imports System.Data.OleDb
Imports oopfinals.Models

Namespace Data
    Public Class UserRepository
        ''' <summary>
        ''' Maps an OleDbDataReader record into the appropriate polymorphic User subclass.
        ''' Demonstrates FACTORY PATTERN and POLYMORPHISM.
        ''' </summary>
        Private Function MapUser(reader As OleDbDataReader) As User
            Dim role As String = Convert.ToString(reader("Role"))
            Dim user As User

            Select Case role.ToLowerInvariant()
                Case "storeowner", "owner"
                    user = New StoreOwner()
                Case "cashier"
                    user = New Cashier()
                Case "inventoryclerk", "clerk"
                    user = New InventoryClerk()
                Case "customer"
                    user = New Customer()
                Case Else
                    user = New Customer()
            End Select

            user.UserId = Convert.ToInt32(reader("UserId"))
            user.Username = Convert.ToString(reader("Username"))
            user.PasswordHash = Convert.ToString(reader("PasswordHash"))
            user.Salt = Convert.ToString(reader("Salt"))
            user.FullName = Convert.ToString(reader("FullName"))
            user.Role = role
            user.IsActive = Convert.ToInt32(reader("IsActive")) = 1
            user.CreatedAt = DatabaseHelper.ReadDateTime(reader, "CreatedAt")

            Return user
        End Function

        ''' <summary>
        ''' Retrieves a user by their username (case-insensitive).
        ''' </summary>
        Public Function GetByUsername(username As String) As User
            Using conn As OleDbConnection = DatabaseHelper.GetConnection()
                Using cmd As New OleDbCommand("
                    SELECT TOP 1 UserId, Username, PasswordHash, Salt, FullName, Role, IsActive, CreatedAt
                    FROM Users
                    WHERE LCASE(Username) = LCASE(?);", conn)
                    DatabaseHelper.AddText(cmd, username.Trim(), 60)

                    Using reader As OleDbDataReader = cmd.ExecuteReader()
                        If reader.Read() Then
                            Return MapUser(reader)
                        End If
                    End Using
                End Using
            End Using
            Return Nothing
        End Function

        ''' <summary>
        ''' Retrieves a user by their unique primary key ID.
        ''' </summary>
        Public Function GetById(userId As Integer) As User
            Using conn As OleDbConnection = DatabaseHelper.GetConnection()
                Using cmd As New OleDbCommand("
                    SELECT TOP 1 UserId, Username, PasswordHash, Salt, FullName, Role, IsActive, CreatedAt
                    FROM Users
                    WHERE UserId = ?;", conn)
                    DatabaseHelper.AddLong(cmd, userId)

                    Using reader As OleDbDataReader = cmd.ExecuteReader()
                        If reader.Read() Then
                            Return MapUser(reader)
                        End If
                    End Using
                End Using
            End Using
            Return Nothing
        End Function

        ''' <summary>
        ''' Retrieves all staff members (Store Owner, Cashier, Inventory Clerk) for administrative management.
        ''' </summary>
        Public Function GetAllStaff() As List(Of User)
            Dim list As New List(Of User)()
            Using conn As OleDbConnection = DatabaseHelper.GetConnection()
                Using cmd As New OleDbCommand("
                    SELECT UserId, Username, PasswordHash, Salt, FullName, Role, IsActive, CreatedAt
                    FROM Users
                    WHERE Role IN ('Cashier', 'InventoryClerk', 'StoreOwner')
                    ORDER BY Role, [FullName];", conn)

                    Using reader As OleDbDataReader = cmd.ExecuteReader()
                        While reader.Read()
                            list.Add(MapUser(reader))
                        End While
                    End Using
                End Using
            End Using
            Return list
        End Function

        ''' <summary>
        ''' Retrieves all registered customers.
        ''' </summary>
        Public Function GetAllCustomers() As List(Of Customer)
            Dim list As New List(Of Customer)()
            Using conn As OleDbConnection = DatabaseHelper.GetConnection()
                Using cmd As New OleDbCommand("
                    SELECT UserId, Username, PasswordHash, Salt, FullName, Role, IsActive, CreatedAt
                    FROM Users
                    WHERE Role = 'Customer'
                    ORDER BY [FullName];", conn)

                    Using reader As OleDbDataReader = cmd.ExecuteReader()
                        While reader.Read()
                            list.Add(DirectCast(MapUser(reader), Customer))
                        End While
                    End Using
                End Using
            End Using
            Return list
        End Function

        ''' <summary>
        ''' Checks if a username is already taken.
        ''' </summary>
        Public Function UsernameExists(username As String) As Boolean
            Using conn As OleDbConnection = DatabaseHelper.GetConnection()
                Using cmd As New OleDbCommand("SELECT COUNT(*) FROM Users WHERE LCASE(Username) = LCASE(?);", conn)
                    DatabaseHelper.AddText(cmd, username.Trim(), 60)
                    Return Convert.ToInt32(cmd.ExecuteScalar()) > 0
                End Using
            End Using
        End Function

        ''' <summary>
        ''' Inserts a new user record.
        ''' </summary>
        Public Function CreateUser(user As User) As Integer
            Using conn As OleDbConnection = DatabaseHelper.GetConnection()
                Using cmd As New OleDbCommand("
                    INSERT INTO Users (Username, PasswordHash, Salt, FullName, Role, IsActive, CreatedAt)
                    VALUES (?, ?, ?, ?, ?, ?, ?);", conn)
                    DatabaseHelper.AddText(cmd, user.Username, 60)
                    DatabaseHelper.AddText(cmd, user.PasswordHash, 255)
                    DatabaseHelper.AddText(cmd, user.Salt, 64)
                    DatabaseHelper.AddText(cmd, user.FullName, 120)
                    DatabaseHelper.AddText(cmd, user.Role, 30)
                    DatabaseHelper.AddLong(cmd, If(user.IsActive, 1L, 0L))
                    DatabaseHelper.AddDate(cmd, DateTime.Now)

                    Dim newId As Integer = DatabaseHelper.InsertAndGetIdentity(cmd)
                    user.UserId = newId
                    Return newId
                End Using
            End Using
        End Function

        ''' <summary>
        ''' Updates an existing user's profile and active status.
        ''' </summary>
        Public Function UpdateUser(user As User) As Boolean
            Using conn As OleDbConnection = DatabaseHelper.GetConnection()
                Using cmd As New OleDbCommand("
                    UPDATE Users
                    SET [FullName] = ?, Role = ?, IsActive = ?
                    WHERE UserId = ?;", conn)
                    DatabaseHelper.AddText(cmd, user.FullName, 120)
                    DatabaseHelper.AddText(cmd, user.Role, 30)
                    DatabaseHelper.AddLong(cmd, If(user.IsActive, 1L, 0L))
                    DatabaseHelper.AddLong(cmd, user.UserId)

                    Return cmd.ExecuteNonQuery() > 0
                End Using
            End Using
        End Function

        ''' <summary>
        ''' Updates the user's password hash and salt.
        ''' </summary>
        Public Function UpdatePassword(userId As Integer, newHash As String, newSalt As String) As Boolean
            Using conn As OleDbConnection = DatabaseHelper.GetConnection()
                Using cmd As New OleDbCommand("
                    UPDATE Users
                    SET PasswordHash = ?, Salt = ?
                    WHERE UserId = ?;", conn)
                    DatabaseHelper.AddText(cmd, newHash, 255)
                    DatabaseHelper.AddText(cmd, newSalt, 64)
                    DatabaseHelper.AddLong(cmd, userId)

                    Return cmd.ExecuteNonQuery() > 0
                End Using
            End Using
        End Function

        ''' <summary>
        ''' Toggles the active status of a user (activate or deactivate).
        ''' </summary>
        Public Function SetActiveStatus(userId As Integer, isActive As Boolean) As Boolean
            Using conn As OleDbConnection = DatabaseHelper.GetConnection()
                Using cmd As New OleDbCommand("UPDATE Users SET IsActive = ? WHERE UserId = ?;", conn)
                    DatabaseHelper.AddLong(cmd, If(isActive, 1L, 0L))
                    DatabaseHelper.AddLong(cmd, userId)
                    Return cmd.ExecuteNonQuery() > 0
                End Using
            End Using
        End Function
    End Class
End Namespace
