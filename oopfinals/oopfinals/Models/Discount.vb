' ==============================================================================
' Project: Mini-Grocery Store POS & Daily Inventory System
' Course: Object-Oriented Programming (ITP 102) - University of Caloocan City
' Classes: Discount, NoDiscount, RegularDiscount, SeniorPwdDiscount
' OOP Concept: POLYMORPHISM, ABSTRACTION & INHERITANCE
' The abstract base class Discount defines the contract for discount calculation.
' Specific discount schemes override the Apply method to calculate their deduction.
' ==============================================================================

Option Strict On
Option Explicit On

Imports System

Namespace Models
    ''' <summary>
    ''' Abstract base class representing a discount strategy.
    ''' Demonstrates ABSTRACTION by declaring MustOverride members for discount computation.
    ''' </summary>
    Public MustInherit Class Discount
        Protected _discountName As String
        Protected _percentage As Decimal

        ''' <summary>
        ''' Gets the descriptive name of the discount policy.
        ''' </summary>
        Public Overridable ReadOnly Property DiscountName As String
            Get
                Return _discountName
            End Get
        End Property

        ''' <summary>
        ''' Gets the percentage rate of the discount (e.g., 20.0 for 20%).
        ''' </summary>
        Public Overridable ReadOnly Property Percentage As Decimal
            Get
                Return _percentage
            End Get
        End Property

        ''' <summary>
        ''' Protected constructor ensuring proper encapsulation of child classes.
        ''' </summary>
        Protected Sub New(name As String, percentage As Decimal)
            If String.IsNullOrWhiteSpace(name) Then
                Throw New ArgumentException("Discount name cannot be empty.")
            End If
            If percentage < 0D OrElse percentage > 100D Then
                Throw New ArgumentOutOfRangeException(NameOf(percentage), "Percentage must be between 0 and 100.")
            End If
            _discountName = name
            _percentage = percentage
        End Sub

        ''' <summary>
        ''' Computes the discount deduction amount given a gross subtotal.
        ''' Demonstrates POLYMORPHISM: each subclass provides its own computation logic.
        ''' </summary>
        Public MustOverride Function Apply(subtotal As Decimal) As Decimal

        Public Overrides Function ToString() As String
            Return $"{DiscountName} ({Percentage:0.#}%)"
        End Function
    End Class

    ''' <summary>
    ''' Represents standard transaction with zero discount applied.
    ''' Demonstrates INHERITANCE and POLYMORPHISM.
    ''' </summary>
    Public Class NoDiscount
        Inherits Discount

        Public Sub New()
            MyBase.New("None", 0D)
        End Sub

        Public Overrides Function Apply(subtotal As Decimal) As Decimal
            ' No deduction is made
            Return 0D
        End Function

        Public Overrides Function ToString() As String
            Return "None (0%)"
        End Function
    End Class

    ''' <summary>
    ''' Represents a regular/promotional discount with a configurable percentage (e.g. 5%, 10%).
    ''' Demonstrates INHERITANCE and POLYMORPHISM.
    ''' </summary>
    Public Class RegularDiscount
        Inherits Discount

        Public Sub New(Optional percentageRate As Decimal = 5D)
            MyBase.New("Regular / Promo", percentageRate)
        End Sub

        Public Overrides Function Apply(subtotal As Decimal) As Decimal
            If subtotal <= 0D Then Return 0D
            Return Math.Round(subtotal * (_percentage / 100D), 2, MidpointRounding.AwayFromZero)
        End Function
    End Class

    ''' <summary>
    ''' Represents Philippine Senior Citizen / Persons with Disabilities (PWD) statutory 20% discount.
    ''' Demonstrates INHERITANCE and POLYMORPHISM.
    ''' </summary>
    Public Class SeniorPwdDiscount
        Inherits Discount

        Public Sub New()
            MyBase.New("Senior Citizen / PWD", 20D)
        End Sub

        Public Overrides Function Apply(subtotal As Decimal) As Decimal
            If subtotal <= 0D Then Return 0D
            ' 20% discount on grocery subtotal
            Return Math.Round(subtotal * 0.2D, 2, MidpointRounding.AwayFromZero)
        End Function

        Public Overrides Function ToString() As String
            Return "Senior Citizen / PWD (20%)"
        End Function
    End Class
End Namespace
