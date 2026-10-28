' ==============================================================================
' Project: Mini-Grocery Store POS & Daily Inventory System
' Course: Object-Oriented Programming (ITP 102) - University of Caloocan City
' Class: InventoryClerk
' OOP Concept: INHERITANCE & POLYMORPHISM
' InventoryClerk inherits from User and overrides GetRoleName(), GetPermissions(),
' and GetDashboardForm() to access inventory management tools.
' ==============================================================================

Option Strict On
Option Explicit On

Imports System.Collections.Generic
Imports System.Windows.Forms

Namespace Models
    ''' <summary>
    ''' Represents an inventory clerk staff member responsible for stock-in and stock adjustments.
    ''' Demonstrates INHERITANCE and POLYMORPHISM.
    ''' </summary>
    Public Class InventoryClerk
        Inherits User

        Public Sub New()
            MyBase.New()
            Me.Role = "InventoryClerk"
        End Sub

        Public Sub New(userId As Integer, username As String, fullName As String, isActive As Boolean)
            MyBase.New(userId, username, fullName, "InventoryClerk", isActive)
        End Sub

        Public Overrides Function GetRoleName() As String
            Return "Inventory Clerk"
        End Function

        Public Overrides Function GetDashboardForm() As Form
            Return New Forms.ClerkDashboardForm(Me)
        End Function

        Public Overrides Function GetPermissions() As List(Of String)
            Return New List(Of String) From {
                "StockIn",
                "StockAdjustment",
                "ViewInventoryAlerts",
                "ViewStockMovements",
                "BrowseProductsReadOnly"
            }
        End Function
    End Class
End Namespace
