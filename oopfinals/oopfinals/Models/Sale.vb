' ==============================================================================
' Project: Mini-Grocery Store POS & Daily Inventory System
' Course: Object-Oriented Programming (ITP 102) - University of Caloocan City
' Class: Sale
' OOP Concept: ENCAPSULATION & AGGREGATION
' Encapsulates the entire sales receipt, aggregating SaleItem line items,
' totals, discount metrics, and cash tender.
' ==============================================================================

Option Strict On
Option Explicit On

Imports System
Imports System.Collections.Generic

Namespace Models
    ''' <summary>
    ''' Represents a completed or pending POS sales transaction.
    ''' Demonstrates ENCAPSULATION and AGGREGATION (has a List of SaleItem).
    ''' </summary>
    Public Class Sale
        Private _saleId As Integer
        Private _receiptNo As String
        Private _saleDate As DateTime
        Private _cashierId As Integer
        Private _cashierName As String
        Private _customerId As Nullable(Of Integer)
        Private _customerName As String
        Private _subtotal As Decimal
        Private _discountType As String
        Private _discountAmount As Decimal
        Private _total As Decimal
        Private _cashReceived As Decimal
        Private _changeAmount As Decimal
        Private _items As List(Of SaleItem)

        Public Property SaleId As Integer
            Get
                Return _saleId
            End Get
            Set(value As Integer)
                _saleId = value
            End Set
        End Property

        Public Property ReceiptNo As String
            Get
                Return _receiptNo
            End Get
            Set(value As String)
                _receiptNo = value
            End Set
        End Property

        Public Property SaleDate As DateTime
            Get
                Return _saleDate
            End Get
            Set(value As DateTime)
                _saleDate = value
            End Set
        End Property

        Public Property CashierId As Integer
            Get
                Return _cashierId
            End Get
            Set(value As Integer)
                _cashierId = value
            End Set
        End Property

        Public Property CashierName As String
            Get
                Return _cashierName
            End Get
            Set(value As String)
                _cashierName = value
            End Set
        End Property

        Public Property CustomerId As Nullable(Of Integer)
            Get
                Return _customerId
            End Get
            Set(value As Nullable(Of Integer))
                _customerId = value
            End Set
        End Property

        Public Property CustomerName As String
            Get
                Return _customerName
            End Get
            Set(value As String)
                _customerName = value
            End Set
        End Property

        Public Property Subtotal As Decimal
            Get
                Return _subtotal
            End Get
            Set(value As Decimal)
                If value < 0D Then Throw New ArgumentException("Subtotal cannot be negative.")
                _subtotal = value
            End Set
        End Property

        Public Property DiscountType As String
            Get
                Return _discountType
            End Get
            Set(value As String)
                _discountType = value
            End Set
        End Property

        Public Property DiscountAmount As Decimal
            Get
                Return _discountAmount
            End Get
            Set(value As Decimal)
                If value < 0D Then Throw New ArgumentException("Discount amount cannot be negative.")
                _discountAmount = value
            End Set
        End Property

        Public Property Total As Decimal
            Get
                Return _total
            End Get
            Set(value As Decimal)
                If value < 0D Then Throw New ArgumentException("Total cannot be negative.")
                _total = value
            End Set
        End Property

        Public Property CashReceived As Decimal
            Get
                Return _cashReceived
            End Get
            Set(value As Decimal)
                If value < 0D Then Throw New ArgumentException("Cash received cannot be negative.")
                _cashReceived = value
            End Set
        End Property

        Public Property ChangeAmount As Decimal
            Get
                Return _changeAmount
            End Get
            Set(value As Decimal)
                If value < 0D Then Throw New ArgumentException("Change amount cannot be negative.")
                _changeAmount = value
            End Set
        End Property

        Public Property Items As List(Of SaleItem)
            Get
                If _items Is Nothing Then
                    _items = New List(Of SaleItem)()
                End If
                Return _items
            End Get
            Set(value As List(Of SaleItem))
                _items = value
            End Set
        End Property

        Public Sub New()
            _saleDate = DateTime.Now
            _receiptNo = String.Empty
            _cashierName = String.Empty
            _customerName = String.Empty
            _discountType = "None"
            _items = New List(Of SaleItem)()
        End Sub
    End Class
End Namespace
