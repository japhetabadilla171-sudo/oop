' ==============================================================================
' Project: Mini-Grocery Store POS & Daily Inventory System
' Course: Object-Oriented Programming (ITP 102) - University of Caloocan City
' Class: CartService
' OOP Concept: POLYMORPHISM & ENCAPSULATION
' Manages the cashier shopping cart, applies polymorphic discounts (NoDiscount,
' RegularDiscount, SeniorPwdDiscount), and calculates totals and change.
' ==============================================================================

Option Strict On
Option Explicit On

Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports oopfinals.Models

Namespace Services
    Public Class CartService
        ' --- ENCAPSULATION: Private state of the cart ---
        Private ReadOnly _items As New List(Of SaleItem)()
        Private _currentDiscount As Discount

        ''' <summary>
        ''' Gets an immutable copy or read-only view of current cart items.
        ''' </summary>
        Public ReadOnly Property Items As IReadOnlyList(Of SaleItem)
            Get
                Return _items.AsReadOnly()
            End Get
        End Property

        ''' <summary>
        ''' Gets or sets the polymorphic discount strategy.
        ''' Demonstrates POLYMORPHISM: The discount type can be swapped at runtime.
        ''' </summary>
        Public Property CurrentDiscount As Discount
            Get
                If _currentDiscount Is Nothing Then
                    _currentDiscount = New NoDiscount()
                End If
                Return _currentDiscount
            End Get
            Set(value As Discount)
                _currentDiscount = If(value, New NoDiscount())
            End Set
        End Property

        Public Sub New()
            _currentDiscount = New NoDiscount()
        End Sub

        ''' <summary>
        ''' Adds an item to the cart or increments existing quantity with stock validation.
        ''' </summary>
        Public Sub AddItem(product As Product, requestedQty As Integer)
            If product Is Nothing Then Throw New ArgumentNullException(NameOf(product))
            If requestedQty <= 0 Then Throw New ArgumentException("Quantity must be greater than zero.")

            Dim existingItem As SaleItem = _items.FirstOrDefault(Function(i) i.ProductId = product.ProductId)
            Dim totalDesiredQty As Integer = If(existingItem IsNot Nothing, existingItem.Quantity + requestedQty, requestedQty)

            If totalDesiredQty > product.StockQty Then
                Throw New InvalidOperationException($"Cannot add {requestedQty} unit(s). Available stock for '{product.Name}' is only {product.StockQty}.")
            End If

            If existingItem IsNot Nothing Then
                existingItem.Quantity = totalDesiredQty
            Else
                Dim newItem As New SaleItem(product.ProductId, product.ProductCode, product.Name, product.Price, requestedQty)
                _items.Add(newItem)
            End If
        End Sub

        ''' <summary>
        ''' Updates the quantity of an item in the cart.
        ''' </summary>
        Public Sub UpdateItemQuantity(productId As Integer, newQuantity As Integer, availableStock As Integer)
            If newQuantity <= 0 Then
                RemoveItem(productId)
                Return
            End If

            If newQuantity > availableStock Then
                Throw New InvalidOperationException($"Requested quantity ({newQuantity}) exceeds available stock ({availableStock}).")
            End If

            Dim existingItem As SaleItem = _items.FirstOrDefault(Function(i) i.ProductId = productId)
            If existingItem IsNot Nothing Then
                existingItem.Quantity = newQuantity
            End If
        End Sub

        ''' <summary>
        ''' Removes an item from the cart.
        ''' </summary>
        Public Sub RemoveItem(productId As Integer)
            _items.RemoveAll(Function(i) i.ProductId = productId)
        End Sub

        ''' <summary>
        ''' Empties the cart and resets discount.
        ''' </summary>
        Public Sub ClearCart()
            _items.Clear()
            _currentDiscount = New NoDiscount()
        End Sub

        ''' <summary>
        ''' Calculates the gross sum of all line items.
        ''' </summary>
        Public Function GetSubtotal() As Decimal
            Dim sum As Decimal = 0D
            For Each itm In _items
                sum += itm.LineTotal
            Next
            Return sum
        End Function

        ''' <summary>
        ''' Computes discount amount using polymorphic strategy.
        ''' Demonstrates POLYMORPHISM: Invokes the overridden Apply() method of CurrentDiscount.
        ''' </summary>
        Public Function GetDiscountAmount() As Decimal
            Dim subtotal As Decimal = GetSubtotal()
            Return CurrentDiscount.Apply(subtotal)
        End Function

        ''' <summary>
        ''' Computes the net total due after discount.
        ''' </summary>
        Public Function GetNetTotal() As Decimal
            Dim subtotal As Decimal = GetSubtotal()
            Dim discountAmt As Decimal = GetDiscountAmount()
            Dim net As Decimal = subtotal - discountAmt
            Return If(net < 0D, 0D, net)
        End Function

        ''' <summary>
        ''' Computes change amount based on cash tendered.
        ''' </summary>
        Public Function ComputeChange(cashReceived As Decimal) As Decimal
            Dim total As Decimal = GetNetTotal()
            If cashReceived < total Then
                Throw New InvalidOperationException($"Insufficient cash. Total due is PHP {total:N2}, but received PHP {cashReceived:N2}.")
            End If
            Return Math.Round(cashReceived - total, 2, MidpointRounding.AwayFromZero)
        End Function

        ''' <summary>
        ''' Returns total item count in cart.
        ''' </summary>
        Public Function GetTotalItemCount() As Integer
            Dim count As Integer = 0
            For Each itm In _items
                count += itm.Quantity
            Next
            Return count
        End Function
    End Class
End Namespace
