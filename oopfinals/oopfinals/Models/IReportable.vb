' ==============================================================================
' Project: Mini-Grocery Store POS & Daily Inventory System
' Course: Object-Oriented Programming (ITP 102) - University of Caloocan City
' Interface: IReportable
' OOP Concept: INTERFACE / ABSTRACTION
' Contract for reportable entities and services capable of rendering grid data
' and exporting to CSV format.
' ==============================================================================

Option Strict On
Option Explicit On

Imports System.Data

Namespace Models
    ''' <summary>
    ''' Contract defining reporting capabilities for reports and analytics modules.
    ''' Demonstrates ABSTRACTION by decoupling report generation from UI presentation.
    ''' </summary>
    Public Interface IReportable
        ReadOnly Property ReportTitle As String
        ReadOnly Property GeneratedDate As DateTime
        Function GenerateReportData() As DataTable
        Function ExportToCsv(filePath As String) As Boolean
    End Interface
End Namespace
