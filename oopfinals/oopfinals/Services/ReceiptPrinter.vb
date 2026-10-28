' ==============================================================================
' Project: Mini-Grocery Store POS & Daily Inventory System
' Course: Object-Oriented Programming (ITP 102) - University of Caloocan City
' Class: ReceiptPrinter
' OOP Concept: ENCAPSULATION & HARDWARE ABSTRACTION
' Encapsulates receipt rendering using System.Drawing.Printing.PrintDocument
' and provides a standardized receipt layout for printing and print preview.
' ==============================================================================

Option Strict On
Option Explicit On

Imports System
Imports System.Drawing
Imports System.Drawing.Printing
Imports System.Text
Imports System.Windows.Forms
Imports oopfinals.Models

Namespace Services
    Public Class ReceiptPrinter
        Private ReadOnly _sale As Sale

        Public Sub New(sale As Sale)
            If sale Is Nothing Then Throw New ArgumentNullException(NameOf(sale))
            _sale = sale
        End Sub

        ''' <summary>
        ''' Generates formatted plain text representation of the grocery receipt.
        ''' </summary>
        Public Function GenerateReceiptText() As String
            Dim sb As New StringBuilder()
            Dim divider As String = "------------------------------------------------"
            Dim dblDivider As String = "================================================"

            sb.AppendLine("         MINI-GROCERY STORE POS")
            sb.AppendLine("    University of Caloocan City - CSD")
            sb.AppendLine("       Biglang Awa St., Caloocan City")
            sb.AppendLine("           Contact: (02) 8310-1234")
            sb.AppendLine(dblDivider)
            sb.AppendLine($"Receipt No : {_sale.ReceiptNo}")
            sb.AppendLine($"Date & Time: {_sale.SaleDate:yyyy-MM-dd hh:mm tt}")
            sb.AppendLine($"Cashier    : {_sale.CashierName}")
            If Not String.IsNullOrWhiteSpace(_sale.CustomerName) Then
                sb.AppendLine($"Customer   : {_sale.CustomerName}")
            End If
            sb.AppendLine(divider)
            sb.AppendLine(String.Format("{0,-20} {1,5} {2,10} {3,10}", "ITEM", "QTY", "PRICE", "TOTAL"))
            sb.AppendLine(divider)

            For Each item In _sale.Items
                Dim name As String = item.ProductName
                If name.Length > 20 Then name = name.Substring(0, 17) & "..."
                sb.AppendLine(String.Format("{0,-20} {1,5} {2,10:N2} {3,10:N2}", name, item.Quantity, item.UnitPrice, item.LineTotal))
            Next

            sb.AppendLine(divider)
            sb.AppendLine(String.Format("{0,37} {1,10:N2}", "Subtotal: PHP", _sale.Subtotal))
            If _sale.DiscountAmount > 0D Then
                sb.AppendLine(String.Format("{0,37} {1,10:N2}", $"Discount ({_sale.DiscountType}): PHP", -_sale.DiscountAmount))
            End If
            sb.AppendLine(String.Format("{0,37} {1,10:N2}", "TOTAL DUE: PHP", _sale.Total))
            sb.AppendLine(String.Format("{0,37} {1,10:N2}", "Cash Tendered: PHP", _sale.CashReceived))
            sb.AppendLine(String.Format("{0,37} {1,10:N2}", "Change: PHP", _sale.ChangeAmount))
            sb.AppendLine(dblDivider)
            sb.AppendLine("           SALES INVOICE / OFFICIAL RECEIPT")
            sb.AppendLine("          Thank you for shopping with us!")
            sb.AppendLine("            Please come again soon.")
            sb.AppendLine(dblDivider)
            Return sb.ToString()
        End Function

        ''' <summary>
        ''' Displays the receipt in a standard Windows Print Preview dialog.
        ''' </summary>
        Public Sub ShowPrintPreview(owner As IWin32Window)
            Dim printDoc As New PrintDocument()
            AddHandler printDoc.PrintPage, AddressOf PrintPageHandler

            Using previewDlg As New PrintPreviewDialog()
                previewDlg.Document = printDoc
                previewDlg.Width = 600
                previewDlg.Height = 750
                previewDlg.StartPosition = FormStartPosition.CenterParent
                previewDlg.ShowDialog(owner)
            End Using
        End Sub

        ''' <summary>
        ''' Directly sends the receipt to the default printer.
        ''' </summary>
        Public Sub PrintDirectly()
            Dim printDoc As New PrintDocument()
            AddHandler printDoc.PrintPage, AddressOf PrintPageHandler
            printDoc.Print()
        End Sub

        ''' <summary>
        ''' Graphics drawing handler rendering the receipt.
        ''' </summary>
        Private Sub PrintPageHandler(sender As Object, e As PrintPageEventArgs)
            Dim g As Graphics = e.Graphics
            Dim printFont As New Font("Consolas", 9, FontStyle.Regular)
            Dim boldFont As New Font("Consolas", 10, FontStyle.Bold)
            Dim titleFont As New Font("Segoe UI", 12, FontStyle.Bold)
            Dim smallFont As New Font("Consolas", 8, FontStyle.Italic)

            Dim leftMargin As Single = 40
            Dim yPos As Single = 40
            Dim lineHeight As Single = printFont.GetHeight(g) + 2

            ' Store Header
            Dim centerFormat As New StringFormat() With {.Alignment = StringAlignment.Center}
            Dim headerRect As New RectangleF(leftMargin, yPos, 400, 25)
            g.DrawString("MINI-GROCERY STORE POS", titleFont, Brushes.Black, headerRect, centerFormat)
            yPos += 24
            Dim subRect As New RectangleF(leftMargin, yPos, 400, 18)
            g.DrawString("University of Caloocan City - CSD", boldFont, Brushes.Black, subRect, centerFormat)
            yPos += 18
            Dim addressRect As New RectangleF(leftMargin, yPos, 400, 16)
            g.DrawString("Biglang Awa St., 12th Ave East, Caloocan City", smallFont, Brushes.Black, addressRect, centerFormat)
            yPos += 22

            g.DrawLine(Pens.Black, leftMargin, yPos, leftMargin + 400, yPos)
            yPos += 6

            ' Metadata
            g.DrawString($"Receipt No : {_sale.ReceiptNo}", printFont, Brushes.Black, leftMargin, yPos)
            yPos += lineHeight
            g.DrawString($"Date       : {_sale.SaleDate:yyyy-MM-dd hh:mm tt}", printFont, Brushes.Black, leftMargin, yPos)
            yPos += lineHeight
            g.DrawString($"Cashier    : {_sale.CashierName}", printFont, Brushes.Black, leftMargin, yPos)
            yPos += lineHeight
            If Not String.IsNullOrWhiteSpace(_sale.CustomerName) Then
                g.DrawString($"Customer   : {_sale.CustomerName}", printFont, Brushes.Black, leftMargin, yPos)
                yPos += lineHeight
            End If

            g.DrawLine(Pens.Black, leftMargin, yPos, leftMargin + 400, yPos)
            yPos += 6

            ' Table Column Headers
            g.DrawString("ITEM", boldFont, Brushes.Black, leftMargin, yPos)
            g.DrawString("QTY", boldFont, Brushes.Black, leftMargin + 180, yPos)
            g.DrawString("PRICE", boldFont, Brushes.Black, leftMargin + 240, yPos)
            g.DrawString("TOTAL", boldFont, Brushes.Black, leftMargin + 320, yPos)
            yPos += lineHeight + 2
            g.DrawLine(Pens.Gray, leftMargin, yPos, leftMargin + 400, yPos)
            yPos += 4

            ' Items
            For Each item In _sale.Items
                Dim displayName As String = item.ProductName
                If displayName.Length > 22 Then displayName = displayName.Substring(0, 20) & ".."
                g.DrawString(displayName, printFont, Brushes.Black, leftMargin, yPos)
                g.DrawString(item.Quantity.ToString(), printFont, Brushes.Black, leftMargin + 185, yPos)
                g.DrawString(item.UnitPrice.ToString("N2"), printFont, Brushes.Black, leftMargin + 240, yPos)
                g.DrawString(item.LineTotal.ToString("N2"), printFont, Brushes.Black, leftMargin + 320, yPos)
                yPos += lineHeight
            Next

            g.DrawLine(Pens.Black, leftMargin, yPos, leftMargin + 400, yPos)
            yPos += 8

            ' Totals
            g.DrawString($"Subtotal:", printFont, Brushes.Black, leftMargin + 180, yPos)
            g.DrawString($"PHP {_sale.Subtotal:N2}", printFont, Brushes.Black, leftMargin + 300, yPos)
            yPos += lineHeight

            If _sale.DiscountAmount > 0D Then
                g.DrawString($"Discount ({_sale.DiscountType}):", printFont, Brushes.Black, leftMargin + 140, yPos)
                g.DrawString($"-PHP {_sale.DiscountAmount:N2}", printFont, Brushes.Black, leftMargin + 300, yPos)
                yPos += lineHeight
            End If

            g.DrawString($"NET TOTAL:", boldFont, Brushes.Black, leftMargin + 180, yPos)
            g.DrawString($"PHP {_sale.Total:N2}", boldFont, Brushes.Black, leftMargin + 300, yPos)
            yPos += lineHeight + 2

            g.DrawString($"Cash Received:", printFont, Brushes.Black, leftMargin + 180, yPos)
            g.DrawString($"PHP {_sale.CashReceived:N2}", printFont, Brushes.Black, leftMargin + 300, yPos)
            yPos += lineHeight

            g.DrawString($"Change Due:", boldFont, Brushes.Black, leftMargin + 180, yPos)
            g.DrawString($"PHP {_sale.ChangeAmount:N2}", boldFont, Brushes.Black, leftMargin + 300, yPos)
            yPos += lineHeight + 12

            g.DrawLine(Pens.Black, leftMargin, yPos, leftMargin + 400, yPos)
            yPos += 8

            Dim footerRect As New RectangleF(leftMargin, yPos, 400, 35)
            g.DrawString("THANK YOU FOR YOUR PURCHASE!" & vbCrLf & "Please retain this receipt for your records.", smallFont, Brushes.Black, footerRect, centerFormat)

            e.HasMorePages = False
        End Sub
    End Class
End Namespace
