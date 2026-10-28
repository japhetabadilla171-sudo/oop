' ==============================================================================
' Project: Mini-Grocery Store POS & Daily Inventory System
' Course: Object-Oriented Programming (ITP 102) - University of Caloocan City
' Class: StockMovement
' OOP Concept: ENCAPSULATION
' Encapsulates audit trail events for all inventory modifications (StockIn,
' Adjustment, and Sale).
' ==============================================================================

Option Strict On
Option Explicit On

Imports System

Namespace Models
    ''' <summary>
    ''' Represents an immutable audit record for an inventory change event.
    ''' Demonstrates ENCAPSULATION.
    ''' </summary>
    Public Class StockMovement
        Private _movementId As Integer
        Private _productId As Integer
        Private _productCode As String
        Private _productName As String
        Private _movementType As String ' "StockIn", "Adjustment", "Sale"
        Private _quantityChange As Integer
        Private _reason As String
        Private _supplierId As Nullable(Of Integer)
        Private _supplierName As String
        Private _userId As Integer
        Private _userName As String
        Private _movementDate As DateTime

        Public Property MovementId As Integer
            Get
                Return _movementId
            End Get
            Set(value As Integer)
                _movementId = value
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

        Public Property MovementType As String
            Get
                Return _movementType
            End Get
            Set(value As String)
                _movementType = value
            End Set
        End Property

        Public Property QuantityChange As Integer
            Get
                Return _quantityChange
            End Get
            Set(value As Integer)
                _quantityChange = value
            End Set
        End Property

        Public Property Reason As String
            Get
                Return _reason
            End Get
            Set(value As String)
                _reason = value
            End Set
        End Property

        Public Property SupplierId As Nullable(Of Integer)
            Get
                Return _supplierId
            End Get
            Set(value As Nullable(Of Integer))
                _supplierId = value
            End Set
        End Property

        Public Property SupplierName As String
            Get
                Return _supplierName
            End Get
            Set(value As String)
                _supplierName = value
            End Set
        End Property

        Public Property UserId As Integer
            Get
                Return _userId
            End Get
            Set(value As Integer)
                _userId = value
            End Set
        End Property

        Public Property UserName As String
            Get
                Return _userName
            End Get
            Set(value As String)
                _userName = value
            End Set
        End Property

        Public Property MovementDate As DateTime
            Get
                Return _movementDate
            End Get
            Set(value As DateTime)
                _movementDate = value
            End Set
        End Property

        Public Sub New()
            _movementDate = DateTime.Now
            _productCode = String.Empty
            _productName = String.Empty
            _reason = String.Empty
            _supplierName = String.Empty
            _userName = String.Empty
            _movementType = "Adjustment"
        End Sub
    End Class
End Namespace
