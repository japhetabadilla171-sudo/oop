' ==============================================================================
' Project: Mini-Grocery Store POS & Daily Inventory System
' Course: Object-Oriented Programming (ITP 102) - University of Caloocan City
' Class: StoreOwner
' OOP Concept: INHERITANCE & POLYMORPHISM
' StoreOwner inherits from User and possesses full administrative privileges.
' ==============================================================================

Option Strict On
Option Explicit On

Imports System.Collections.Generic
Imports System.Windows.Forms

Namespace Models
    ''' <summary>
    ''' Represents the grocery store administrator / owner.
    ''' Possesses unrestricted access to system configuration, staff management, and reports.
    ''' Demonstrates INHERITANCE and POLYMORPHISM.
    ''' </summary>
    Public Class StoreOwner
        Inherits User

        Public Sub New()
            MyBase.New()
            Me.Role = "StoreOwner"
        End Sub

        Public Sub New(userId As Integer, username As String, fullName As String, isActive As Boolean)
            MyBase.New(userId, username, fullName, "StoreOwner", isActive)
        End Sub

        Public Overrides Function GetRoleName() As String
            Return "Store Owner"
        End Function

        Public Overrides Function GetDashboardForm() As Form
            Return New Forms.OwnerDashboardForm(Me)
        End Function

        Public Overrides Function GetPermissions() As List(Of String)
            Return New List(Of String) From {
                "ManageStaff",
                "ManageProducts",
                "ManagePrices",
                "ManageCategories",
                "ManageSuppliers",
                "ViewAllTransactions",
                "ViewReports",
                "ExportReports",
                "PrintReports",
                "StockIn",
                "StockAdjustment",
                "ViewInventoryAlerts",
                "ViewStockMovements",
                "ProcessSale"
            }
        End Function
    End Class
End Namespace
