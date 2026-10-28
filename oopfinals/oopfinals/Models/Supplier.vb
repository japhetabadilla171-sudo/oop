' ==============================================================================
' Project: Mini-Grocery Store POS & Daily Inventory System
' Course: Object-Oriented Programming (ITP 102) - University of Caloocan City
' Class: Supplier
' OOP Concept: ENCAPSULATION
' Encapsulates vendor and distributor information.
' ==============================================================================

Option Strict On
Option Explicit On

Imports System

Namespace Models
    ''' <summary>
    ''' Represents a commercial supplier or distributor of goods.
    ''' Demonstrates ENCAPSULATION.
    ''' </summary>
    Public Class Supplier
        Private _supplierId As Integer
        Private _name As String
        Private _contactNumber As String
        Private _address As String

        Public Property SupplierId As Integer
            Get
                Return _supplierId
            End Get
            Set(value As Integer)
                _supplierId = value
            End Set
        End Property

        Public Property Name As String
            Get
                Return _name
            End Get
            Set(value As String)
                If String.IsNullOrWhiteSpace(value) Then
                    Throw New ArgumentException("Supplier name cannot be empty.")
                End If
                _name = value.Trim()
            End Set
        End Property

        Public Property ContactNumber As String
            Get
                Return _contactNumber
            End Get
            Set(value As String)
                _contactNumber = If(value IsNot Nothing, value.Trim(), String.Empty)
            End Set
        End Property

        Public Property Address As String
            Get
                Return _address
            End Get
            Set(value As String)
                _address = If(value IsNot Nothing, value.Trim(), String.Empty)
            End Set
        End Property

        Public Sub New()
            _contactNumber = String.Empty
            _address = String.Empty
        End Sub

        Public Sub New(id As Integer, name As String, contact As String, address As String)
            Me.New()
            Me.SupplierId = id
            Me.Name = name
            Me.ContactNumber = contact
            Me.Address = address
        End Sub

        Public Overrides Function ToString() As String
            Return Name
        End Function
    End Class
End Namespace
