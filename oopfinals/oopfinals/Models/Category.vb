' ==============================================================================
' Project: Mini-Grocery Store POS & Daily Inventory System
' Course: Object-Oriented Programming (ITP 102) - University of Caloocan City
' Class: Category
' OOP Concept: ENCAPSULATION
' Encapsulates category data with input validation.
' ==============================================================================

Option Strict On
Option Explicit On

Imports System

Namespace Models
    ''' <summary>
    ''' Represents an inventory merchandise category (e.g. Canned Goods, Beverages).
    ''' Demonstrates ENCAPSULATION through private fields and validation properties.
    ''' </summary>
    Public Class Category
        Private _categoryId As Integer
        Private _name As String

        Public Property CategoryId As Integer
            Get
                Return _categoryId
            End Get
            Set(value As Integer)
                _categoryId = value
            End Set
        End Property

        Public Property Name As String
            Get
                Return _name
            End Get
            Set(value As String)
                If String.IsNullOrWhiteSpace(value) Then
                    Throw New ArgumentException("Category name cannot be empty.")
                End If
                _name = value.Trim()
            End Set
        End Property

        Public Sub New()
        End Sub

        Public Sub New(id As Integer, name As String)
            Me.CategoryId = id
            Me.Name = name
        End Sub

        Public Overrides Function ToString() As String
            Return Name
        End Function
    End Class
End Namespace
