' ==============================================================================
' Project: Mini-Grocery Store POS & Daily Inventory System
' Course: Object-Oriented Programming (ITP 102) - University of Caloocan City
' Class: SaleItem
' OOP Concept: ENCAPSULATION
' Encapsulates individual line items in a checkout sale.
' ==============================================================================

Option Strict On
Option Explicit On

Imports System

Namespace Models
    ''' <summary>
    ''' Represents an individual product line in a POS checkout transaction.
    ''' Demonstrates ENCAPSULATION with validation and automated LineTotal calculation.
    ''' </summary>
    Public Class SaleItem
        Private _saleItemId As Integer
        Private _saleId As Integer
        Private _productId As Integer
        Private _productCode As String
        Private _productName As String
        Private _quantity As Integer
        Private _unitPrice As Decimal

        Public Property SaleItemId As Integer
            Get
                Return _saleItemId
            End Get
            Set(value As Integer)
                _saleItemId = value
            End Set
        End Property

        Public Property SaleId As Integer
            Get
                Return _saleId
            End Get
            Set(value As Integer)
                _saleId = value
            End Set
        End Property

        Public Property ProductId As Integer
            Get
                Return _productId
            End Get
            Set(value As Integer)
                _productId = value
            End Set
        End Property

        Public Property ProductCode As String
            Get
                Return _productCode
            End Get
            Set(value As String)
                _productCode = value
            End Set
        End Property

        Public Property ProductName As String
            Get
                Return _productName
            End Get
            Set(value As String)
                _productName = value
            End Set
        End Property

        Public Property Quantity As Integer
            Get
                Return _quantity
            End Get
            Set(value As Integer)
                If value <= 0 Then
                    Throw New ArgumentException("Sale quantity must be greater than zero.")
                End If
                _quantity = value
            End Set
        End Property

        Public Property UnitPrice As Decimal
            Get
                Return _unitPrice
            End Get
            Set(value As Decimal)
                If value < 0D Then
                    Throw New ArgumentException("Unit price cannot be negative.")
                End If
                _unitPrice = value
            End Set
        End Property

        ''' <summary>
        ''' Encapsulated computed property: Quantity * UnitPrice
        ''' </summary>
        Public ReadOnly Property LineTotal As Decimal
            Get
                Return Math.Round(Quantity * UnitPrice, 2, MidpointRounding.AwayFromZero)
            End Get
        End Property

        Public Sub New()
            _productCode = String.Empty
            _productName = String.Empty
        End Sub

        Public Sub New(productId As Integer, productCode As String, productName As String, unitPrice As Decimal, quantity As Integer)
            Me.New()
            Me.ProductId = productId
            Me.ProductCode = productCode
            Me.ProductName = productName
            Me.UnitPrice = unitPrice
            Me.Quantity = quantity
        End Sub
    End Class
End Namespace
