' ==============================================================================
' Project: Mini-Grocery Store POS & Daily Inventory System
' Course: Object-Oriented Programming (ITP 102) - University of Caloocan City
' Class: Customer
' OOP Concept: INHERITANCE & POLYMORPHISM
' Customer inherits from User and overrides GetRoleName(), GetPermissions(),
' and GetDashboardForm() to provide customer-specific behavior.
' ==============================================================================

Option Strict On
Option Explicit On

Imports System.Collections.Generic
Imports System.Windows.Forms

Namespace Models
    ''' <summary>
    ''' Represents a grocery customer account.
    ''' Demonstrates INHERITANCE (inherits User) and POLYMORPHISM (overrides abstract methods).
    ''' </summary>
    Public Class Customer
        Inherits User

        Public Sub New()
            MyBase.New()
            Me.Role = "Customer"
        End Sub

        Public Sub New(userId As Integer, username As String, fullName As String, isActive As Boolean)
            MyBase.New(userId, username, fullName, "Customer", isActive)
        End Sub

        ''' <summary>
        ''' POLYMORPHISM: Returns formal role name.
        ''' </summary>
        Public Overrides Function GetRoleName() As String
            Return "Customer"
        End Function

        ''' <summary>
        ''' POLYMORPHISM: Returns the Customer dashboard form instance.
        ''' </summary>
        Public Overrides Function GetDashboardForm() As Form
            Return New Forms.CustomerDashboardForm(Me)
        End Function

        ''' <summary>
        ''' POLYMORPHISM: Returns customer permissions list.
        ''' </summary>
        Public Overrides Function GetPermissions() As List(Of String)
            Return New List(Of String) From {
                "ViewProducts",
                "ViewPurchaseHistory",
                "ManageProfile"
            }
        End Function
    End Class
End Namespace
