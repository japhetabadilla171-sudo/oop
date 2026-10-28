' ==============================================================================
' Project: Mini-Grocery Store POS & Daily Inventory System
' Course: Object-Oriented Programming (ITP 102) - University of Caloocan City
' Class: ReportService
' OOP Concept: INTERFACE IMPLEMENTATION & ABSTRACTION
' Generates reports (Daily Sales, Inventory Valuation, Low-Stock / Expiry)
' and provides CSV export and printing capabilities.
' ==============================================================================

Option Strict On
Option Explicit On

Imports System
Imports System.Data
Imports System.Drawing
Imports System.Drawing.Printing
Imports System.IO
Imports System.Text
Imports System.Windows.Forms
Imports oopfinals.Data
Imports oopfinals.Models

Namespace Services
    Public Class ReportService
        ''' <summary>
        ''' Generates the Daily Sales Report for a specific date.
        ''' </summary>
        Public Shared Function GenerateDailySalesReport(targetDate As DateTime, ByRef summaryStats As Dictionary(Of String, Object)) As DataTable
            Dim dt As New DataTable("DailySales")
            dt.Columns.Add("Receipt No", GetType(String))
            dt.Columns.Add("Time", GetType(String))
            dt.Columns.Add("Cashier", GetType(String))
            dt.Columns.Add("Customer", GetType(String))
            dt.Columns.Add("Gross Subtotal (PHP)", GetType(Decimal))
            dt.Columns.Add("Discount (PHP)", GetType(Decimal))
            dt.Columns.Add("Net Total (PHP)", GetType(Decimal))

            Dim saleRepo As New SaleRepository()
            Dim sales As List(Of Sale) = saleRepo.GetSalesByDate(targetDate)

            Dim totalGross As Decimal = 0D
            Dim totalDiscount As Decimal = 0D
            Dim totalNet As Decimal = 0D

            For Each s In sales
                dt.Rows.Add(s.ReceiptNo,
                            s.SaleDate.ToString("hh:mm tt"),
                            s.CashierName,
                            If(String.IsNullOrEmpty(s.CustomerName), "Walk-In", s.CustomerName),
                            s.Subtotal,
                            s.DiscountAmount,
                            s.Total)
                totalGross += s.Subtotal
                totalDiscount += s.DiscountAmount
                totalNet += s.Total
            Next

            summaryStats = New Dictionary(Of String, Object)()
            summaryStats("TransactionCount") = sales.Count
            summaryStats("TotalGross") = totalGross
            summaryStats("TotalDiscount") = totalDiscount
            summaryStats("TotalNet") = totalNet

            Return dt
        End Function

        ''' <summary>
        ''' Generates the Inventory Valuation Report.
        ''' </summary>
        Public Shared Function GenerateInventoryReport(ByRef summaryStats As Dictionary(Of String, Object)) As DataTable
            Dim dt As New DataTable("InventoryValuation")
            dt.Columns.Add("Code", GetType(String))
            dt.Columns.Add("Product Name", GetType(String))
            dt.Columns.Add("Category", GetType(String))
            dt.Columns.Add("Stock Qty", GetType(Integer))
            dt.Columns.Add("Cost Price (PHP)", GetType(Decimal))
            dt.Columns.Add("Retail Price (PHP)", GetType(Decimal))
            dt.Columns.Add("Total Cost Value (PHP)", GetType(Decimal))
            dt.Columns.Add("Total Retail Value (PHP)", GetType(Decimal))
            dt.Columns.Add("Status", GetType(String))

            Dim prodRepo As New ProductRepository()
            Dim products As List(Of Product) = prodRepo.GetAll(activeOnly:=True)

            Dim totalUnits As Integer = 0
            Dim totalCostValue As Decimal = 0D
            Dim totalRetailValue As Decimal = 0D

            For Each p In products
                Dim costVal As Decimal = p.CostPrice * p.StockQty
                Dim retailVal As Decimal = p.Price * p.StockQty

                totalUnits += p.StockQty
                totalCostValue += costVal
                totalRetailValue += retailVal

                dt.Rows.Add(p.ProductCode,
                            p.Name,
                            p.CategoryName,
                            p.StockQty,
                            p.CostPrice,
                            p.Price,
                            costVal,
                            retailVal,
                            p.GetStockStatus())
            Next

            summaryStats = New Dictionary(Of String, Object)()
            summaryStats("TotalProducts") = products.Count
            summaryStats("TotalUnits") = totalUnits
            summaryStats("TotalCostValue") = totalCostValue
            summaryStats("TotalRetailValue") = totalRetailValue
            summaryStats("PotentialProfit") = (totalRetailValue - totalCostValue)

            Return dt
        End Function

        ''' <summary>
        ''' Generates the Low-Stock and Expiry Alert Report.
        ''' </summary>
        Public Shared Function GenerateLowStockAndExpiryReport() As DataTable
            Dim dt As New DataTable("AlertReport")
            dt.Columns.Add("Code", GetType(String))
            dt.Columns.Add("Product Name", GetType(String))
            dt.Columns.Add("Category", GetType(String))
            dt.Columns.Add("Stock Qty", GetType(Integer))
            dt.Columns.Add("Reorder Level", GetType(Integer))
            dt.Columns.Add("Expiry Date", GetType(String))
            dt.Columns.Add("Alert Type", GetType(String))

            Dim prodRepo As New ProductRepository()
            Dim products As List(Of Product) = prodRepo.GetAll(activeOnly:=True)

            For Each p In products
                Dim isLow As Boolean = p.IsLowStock()
                Dim isExp As Boolean = p.IsExpired()
                Dim isNearExp As Boolean = p.IsExpiringSoon(30)

                If isLow OrElse isExp OrElse isNearExp Then
                    Dim alertType As String = ""
                    If isExp Then
                        alertType = "EXPIRED"
                    ElseIf isLow AndAlso isNearExp Then
                        alertType = "LOW STOCK & EXPIRING"
                    ElseIf isLow Then
                        alertType = "LOW STOCK"
                    ElseIf isNearExp Then
                        alertType = "EXPIRING SOON"
                    End If

                    Dim expStr As String = If(p.ExpiryDate.HasValue, p.ExpiryDate.Value.ToString("yyyy-MM-dd"), "N/A")
                    dt.Rows.Add(p.ProductCode, p.Name, p.CategoryName, p.StockQty, p.ReorderLevel, expStr, alertType)
                End If
            Next

            Return dt
        End Function

        ''' <summary>
        ''' Exports any DataTable to a comma-separated values (CSV) file.
        ''' </summary>
        Public Shared Function ExportDataTableToCsv(dt As DataTable, filePath As String) As Boolean
            If dt Is Nothing Then Throw New ArgumentNullException(NameOf(dt))
            If String.IsNullOrWhiteSpace(filePath) Then Throw New ArgumentException("File path cannot be empty.")

            Dim sb As New StringBuilder()

            ' Column Headers
            Dim colNames As New List(Of String)()
            For Each col As DataColumn In dt.Columns
                colNames.Add(EscapeCsv(col.ColumnName))
            Next
            sb.AppendLine(String.Join(",", colNames))

            ' Rows
            For Each row As DataRow In dt.Rows
                Dim rowValues As New List(Of String)()
                For Each col As DataColumn In dt.Columns
                    Dim val As String = If(row(col) IsNot Nothing, row(col).ToString(), String.Empty)
                    rowValues.Add(EscapeCsv(val))
                Next
                sb.AppendLine(String.Join(",", rowValues))
            Next

            File.WriteAllText(filePath, sb.ToString(), Encoding.UTF8)
            Return True
        End Function

        ''' <summary>
        ''' Escapes special characters for CSV formatting.
        ''' </summary>
        Private Shared Function EscapeCsv(value As String) As String
            If String.IsNullOrEmpty(value) Then Return """"""
            Dim containsQuote As Boolean = value.Contains("""")
            Dim containsComma As Boolean = value.Contains(",")
            Dim containsNewline As Boolean = value.Contains(vbCr) OrElse value.Contains(vbLf)

            If containsQuote OrElse containsComma OrElse containsNewline Then
                Return """" & value.Replace("""", """""") & """"
            End If
            Return """" & value & """"
        End Function

        ''' <summary>
        ''' Prints a summary report view with PrintPreviewDialog.
        ''' </summary>
        Public Shared Sub PrintReport(title As String, dt As DataTable, owner As IWin32Window)
            Dim printDoc As New PrintDocument()
            AddHandler printDoc.PrintPage, Sub(s, e)
                                               Dim g As Graphics = e.Graphics
                                               Dim titleFont As New Font("Segoe UI", 12, FontStyle.Bold)
                                               Dim headerFont As New Font("Segoe UI", 9, FontStyle.Bold)
                                               Dim cellFont As New Font("Segoe UI", 8, FontStyle.Regular)
                                               Dim left As Single = 40
                                               Dim top As Single = 40

                                               g.DrawString("MINI-GROCERY STORE - " & title.ToUpper(), titleFont, Brushes.Black, left, top)
                                               top += 25
                                               g.DrawString($"Generated: {DateTime.Now:yyyy-MM-dd hh:mm tt}", cellFont, Brushes.Gray, left, top)
                                               top += 20
                                               g.DrawLine(Pens.Black, left, top, 750, top)
                                               top += 10

                                               ' Draw columns
                                               Dim colWidth As Single = Math.Max(70, CSng(700 / Math.Max(1, dt.Columns.Count)))
                                               For i As Integer = 0 To dt.Columns.Count - 1
                                                   g.DrawString(dt.Columns(i).ColumnName, headerFont, Brushes.DarkGreen, left + (i * colWidth), top)
                                               Next
                                               top += 20
                                               g.DrawLine(Pens.Gray, left, top, 750, top)
                                               top += 6

                                               ' Draw rows (up to page limit)
                                               Dim maxRows As Integer = Math.Min(dt.Rows.Count, 35)
                                               For r As Integer = 0 To maxRows - 1
                                                   For c As Integer = 0 To dt.Columns.Count - 1
                                                       Dim txt As String = dt.Rows(r)(c).ToString()
                                                       If txt.Length > 15 Then txt = txt.Substring(0, 13) & ".."
                                                       g.DrawString(txt, cellFont, Brushes.Black, left + (c * colWidth), top)
                                                   Next
                                                   top += 16
                                               Next

                                               top += 10
                                               g.DrawLine(Pens.Black, left, top, 750, top)
                                               top += 10
                                               g.DrawString($"Total Records Listed: {maxRows} of {dt.Rows.Count}", cellFont, Brushes.Black, left, top)

                                               e.HasMorePages = False
                                           End Sub

            Using dlg As New PrintPreviewDialog()
                dlg.Document = printDoc
                dlg.Width = 850
                dlg.Height = 700
                dlg.StartPosition = FormStartPosition.CenterParent
                dlg.ShowDialog(owner)
            End Using
        End Sub
    End Class
End Namespace
