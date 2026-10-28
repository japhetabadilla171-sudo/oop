' ==============================================================================
' Project: Mini-Grocery Store POS & Daily Inventory System
' Course: Object-Oriented Programming (ITP 102) - University of Caloocan City
' Class: Cashier
' OOP Concept: INHERITANCE & POLYMORPHISM
' Cashier inherits from User and overrides GetRoleName(), GetPermissions(),
' and GetDashboardForm() to access the POS checkout environment.
' ==============================================================================

Option Strict On
Option Explicit On

Imports System.Collections.Generic
Imports System.Windows.Forms

Namespace Models
    ''' <summary>
    ''' Represents a store cashier staff member.
    ''' Demonstrates INHERITANCE and POLYMORPHISM.
    ''' </summary>
    Public Class Cashier
        Inherits User

        Public Sub New()
            MyBase.New()
            Me.Role = "Cashier"
        End Sub

        Public Sub New(userId As Integer, username As String, fullName As String, isActive As Boolean)
            MyBase.New(userId, username, fullName, "Cashier", isActive)
        End Sub

        Public Overrides Function GetRoleName() As String
            Return "Cashier"
        End Function

        Public Overrides Function GetDashboardForm() As Form
            Return New Forms.CashierDashboardForm(Me)
        End Function

        Public Overrides Function GetPermissions() As List(Of String)
            Return New List(Of String) From {
                "ProcessSale",
                "ViewTodayTransactions",
                "ApplyDiscounts",
                "PrintReceipt",
                "LookupProducts"
            }
        End Function
    End Class
End Namespace
