' ==============================================================================
' Project: Mini-Grocery Store POS & Daily Inventory System
' Course: Object-Oriented Programming (ITP 102) - University of Caloocan City
' Class: Product
' OOP Concept: ENCAPSULATION
' Encapsulates all product entity details, strict business validation rules
' (no negative price/cost/stock, non-empty code and name), and status methods.
' ==============================================================================

Option Strict On
Option Explicit On

Imports System

Namespace Models
    ''' <summary>
    ''' Represents a retail grocery product.
    ''' Demonstrates ENCAPSULATION by validating pricing, stock integrity, and protecting fields.
    ''' </summary>
    Public Class Product
        ' --- ENCAPSULATION: Private fields ---
        Private _productId As Integer
        Private _productCode As String
        Private _name As String
        Private _categoryId As Integer
        Private _categoryName As String
        Private _supplierId As Integer
        Private _supplierName As String
        Private _price As Decimal
        Private _costPrice As Decimal
        Private _stockQty As Integer
        Private _reorderLevel As Integer
        Private _expiryDate As Nullable(Of DateTime)
        Private _isActive As Boolean

        ' --- Public Properties with strict validation ---
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
                If String.IsNullOrWhiteSpace(value) Then
                    Throw New ArgumentException("Product code cannot be empty.")
                End If
                _productCode = value.Trim().ToUpperInvariant()
            End Set
        End Property

        Public Property Name As String
            Get
                Return _name
            End Get
            Set(value As String)
                If String.IsNullOrWhiteSpace(value) Then
                    Throw New ArgumentException("Product name cannot be empty.")
                End If
                _name = value.Trim()
            End Set
        End Property

        Public Property CategoryId As Integer
            Get
                Return _categoryId
            End Get
            Set(value As Integer)
                If value <= 0 Then
                    Throw New ArgumentException("A valid Category must be selected.")
                End If
                _categoryId = value
            End Set
        End Property

        Public Property CategoryName As String
            Get
                Return _categoryName
            End Get
            Set(value As String)
                _categoryName = value
            End Set
        End Property

        Public Property SupplierId As Integer
            Get
                Return _supplierId
            End Get
            Set(value As Integer)
                If value <= 0 Then
                    Throw New ArgumentException("A valid Supplier must be selected.")
                End If
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

        Public Property Price As Decimal
            Get
                Return _price
            End Get
            Set(value As Decimal)
                If value < 0D Then
                    Throw New ArgumentException("Selling price cannot be negative.")
                End If
                _price = value
            End Set
        End Property

        Public Property CostPrice As Decimal
            Get
                Return _costPrice
            End Get
            Set(value As Decimal)
                If value < 0D Then
                    Throw New ArgumentException("Cost price cannot be negative.")
                End If
                _costPrice = value
            End Set
        End Property

        Public Property StockQty As Integer
            Get
                Return _stockQty
            End Get
            Set(value As Integer)
                If value < 0 Then
                    Throw New ArgumentException("Stock quantity cannot be negative.")
                End If
                _stockQty = value
            End Set
        End Property

        Public Property ReorderLevel As Integer
            Get
                Return _reorderLevel
            End Get
            Set(value As Integer)
                If value < 0 Then
                    Throw New ArgumentException("Reorder level cannot be negative.")
                End If
                _reorderLevel = value
            End Set
        End Property

        Public Property ExpiryDate As Nullable(Of DateTime)
            Get
                Return _expiryDate
            End Get
            Set(value As Nullable(Of DateTime))
                _expiryDate = value
            End Set
        End Property

        Public Property IsActive As Boolean
            Get
                Return _isActive
            End Get
            Set(value As Boolean)
                _isActive = value
            End Set
        End Property

        Public Sub New()
            _isActive = True
            _reorderLevel = 10
            _categoryName = String.Empty
            _supplierName = String.Empty
        End Sub

        ' --- Business helper methods ---

        ''' <summary>
        ''' Checks if the item's stock has reached or dropped below reorder threshold.
        ''' </summary>
        Public Function IsLowStock() As Boolean
            Return StockQty <= ReorderLevel
        End Function

        ''' <summary>
        ''' Checks if the product is expired.
        ''' </summary>
        Public Function IsExpired() As Boolean
            If Not ExpiryDate.HasValue Then Return False
            Return ExpiryDate.Value.Date < DateTime.Today
        End Function

        ''' <summary>
        ''' Checks if the product is nearing expiration within the specified number of days (default 30).
        ''' </summary>
        Public Function IsExpiringSoon(Optional daysThreshold As Integer = 30) As Boolean
            If Not ExpiryDate.HasValue Then Return False
            Dim daysLeft As Double = (ExpiryDate.Value.Date - DateTime.Today).TotalDays
            Return daysLeft >= 0 AndAlso daysLeft <= daysThreshold
        End Function

        ''' <summary>
        ''' Formatted stock availability label for Customer viewing.
        ''' </summary>
        Public Function GetStockStatus() As String
            If StockQty <= 0 Then
                Return "Out of Stock"
            ElseIf StockQty <= ReorderLevel Then
                Return "Low Stock"
            Else
                Return "In Stock"
            End If
        End Function

        Public Overrides Function ToString() As String
            Return $"{ProductCode} - {Name} (PHP {Price:N2})"
        End Function
    End Class
End Namespace
