' ==============================================================================
' Project: Mini-Grocery Store POS & Daily Inventory System
' Course: Object-Oriented Programming (ITP 102) - University of Caloocan City
' Form: PastReceiptForm (Code-Behind)
' Displays details of a historical sales transaction and line items.
' ==============================================================================

Option Strict On
Option Explicit On

Imports System
Imports System.Windows.Forms
Imports oopfinals.Models
Imports oopfinals.Services
Imports oopfinals.Controls

Namespace Forms
    Public Class PastReceiptForm
        Private ReadOnly _sale As Sale

        Public Sub New(sale As Sale)
            InitializeComponent()
            _sale = sale
            ApplyRestyle() ' RESTYLE (visual only)
        End Sub

        Private Sub PastReceiptForm_Load(sender As Object, e As EventArgs) Handles MyBase.Load
            If _sale Is Nothing Then Return

            lblReceiptNo.Text = "Receipt No: " & _sale.ReceiptNo
            lblCashier.Text = "Cashier: " & _sale.CashierName
            lblCustomer.Text = "Customer: " & If(String.IsNullOrEmpty(_sale.CustomerName), "Walk-In", _sale.CustomerName)
            lblDate.Text = "Date: " & _sale.SaleDate.ToString("yyyy-MM-dd hh:mm tt")

            lblSubtotal.Text = $"Subtotal: PHP {_sale.Subtotal:N2}"
            lblDiscount.Text = $"Discount ({_sale.DiscountType}): PHP {_sale.DiscountAmount:N2}"
            lblNetTotal.Text = $"Total Due: PHP {_sale.Total:N2}"

            dgvItems.Rows.Clear()
            For Each item In _sale.Items
                dgvItems.Rows.Add(item.ProductCode, item.ProductName, item.Quantity, item.UnitPrice.ToString("N2"), item.LineTotal.ToString("N2"))
            Next
        End Sub

        Private Sub btnPrint_Click(sender As Object, e As EventArgs) Handles btnPrint.Click
            If _sale IsNot Nothing Then
                Dim printer As New ReceiptPrinter(_sale)
                printer.ShowPrintPreview(Me)
            End If
        End Sub

        Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
            Me.Close()
        End Sub

#Region "Figma UI restyle (visual only)"
        Private Sub ApplyRestyle()
            Me.Text = "Mini-Grocery Store · Receipt details"

            pnlHeader.BackColor = Theme.HeaderGreen
            lblTitle.Font = Theme.FontBody13Semi
            lblTitle.ForeColor = Theme.White
            lblTitle.BackColor = Color.Transparent

            ' Meta strip: subtle gray header band instead of the light-green block
            pnlMeta.BackColor = Theme.GridHeader
            For Each metaLbl As Label In New Label() {lblReceiptNo, lblCashier, lblCustomer, lblDate}
                metaLbl.Font = Theme.FontBody12
                metaLbl.ForeColor = Theme.Muted
                metaLbl.BackColor = Theme.GridHeader
            Next
            lblReceiptNo.Font = Theme.FontBody12Semi
            lblReceiptNo.ForeColor = Theme.Ink

            GridStyler.Apply(dgvItems)
            GridStyler.ApplyWeights(dgvItems, 1.1, 3.0, 0.8, 1.4, 1.4)

            pnlBottom.BackColor = Theme.White
            lblSubtotal.Font = Theme.FontBody12
            lblSubtotal.ForeColor = Theme.Muted
            lblSubtotal.BackColor = Theme.White
            lblDiscount.Font = Theme.FontBody12
            lblDiscount.ForeColor = Theme.Muted
            lblDiscount.BackColor = Theme.White
            lblNetTotal.Font = Theme.FontBody13Semi
            lblNetTotal.ForeColor = Theme.Ink
            lblNetTotal.BackColor = Theme.White

            btnPrint.Text = "Print / preview"
            Theme.StyleButton(btnPrint, ThemeButtonKind.Primary)
            btnPrint.AutoSize = True

            btnClose.Text = "Close"
            Theme.StyleButton(btnClose, ThemeButtonKind.Ghost)
            btnClose.AutoSize = True
        End Sub
#End Region
    End Class
End Namespace
