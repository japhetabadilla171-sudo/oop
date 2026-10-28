' ==============================================================================
' Project: Mini-Grocery Store POS & Daily Inventory System
' Course: Object-Oriented Programming (ITP 102) - University of Caloocan City
' Form: ReceiptPreviewForm (Code-Behind)
' Displays the formatted official receipt and facilitates printing.
' ==============================================================================

Option Strict On
Option Explicit On

Imports System
Imports System.Windows.Forms
Imports oopfinals.Models
Imports oopfinals.Services
Imports oopfinals.Controls

Namespace Forms
    Public Class ReceiptPreviewForm
        Private ReadOnly _sale As Sale
        Private ReadOnly _printer As ReceiptPrinter

        Public Sub New(sale As Sale)
            InitializeComponent()
            _sale = sale
            _printer = New ReceiptPrinter(_sale)
            ApplyRestyle() ' RESTYLE (visual only)
        End Sub

        Private Sub ReceiptPreviewForm_Load(sender As Object, e As EventArgs) Handles MyBase.Load
            If _sale IsNot Nothing Then
                txtReceipt.Text = _printer.GenerateReceiptText()
                txtReceipt.SelectionStart = 0
                txtReceipt.SelectionLength = 0
            End If
        End Sub

        Private Sub btnPrint_Click(sender As Object, e As EventArgs) Handles btnPrint.Click
            Try
                _printer.ShowPrintPreview(Me)
            Catch ex As Exception
                MessageBox.Show($"Print preview error: {ex.Message}", "Print Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
        End Sub

        Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
            Me.Close()
        End Sub

#Region "Figma UI restyle (visual only)"
        Private Sub ApplyRestyle()
            Me.Text = "Mini-Grocery Store · Official receipt"

            pnlTop.BackColor = Theme.HeaderGreen
            lblTitle.Text = "Official receipt"
            lblTitle.Font = Theme.FontBody13Semi
            lblTitle.ForeColor = Theme.White
            lblTitle.BackColor = Color.Transparent

            btnPrint.Text = "Print"
            Theme.StyleButton(btnPrint, ThemeButtonKind.Primary)
            btnPrint.AutoSize = True

            btnClose.Text = "Close"
            Theme.StyleButton(btnClose, ThemeButtonKind.Ghost)
            btnClose.AutoSize = True

            ' txtReceipt is intentionally untouched: its Consolas mono font and
            ' column alignment come from ReceiptPrinter and are print-critical.
        End Sub
#End Region
    End Class
End Namespace
