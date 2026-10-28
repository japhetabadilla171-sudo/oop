' ==============================================================================
' Project: Mini-Grocery Store POS & Daily Inventory System
' Module:  Controls / StyledGrid  (UI RESTYLE)
' Purpose: Applies the design grid style to EXISTING DataGridViews (names and
'          data bindings untouched): white surface, 4px card border via host
'          card, 40px #EDF1EE header, 40px rows, 12px cell padding, blue
'          selection, and a 30px "Showing X of Y ... ‹ 1 ›" footer label.
'          Existing per-row alert ForeColors (red/orange) keep working.
' ==============================================================================

Option Strict On
Option Explicit On

Imports System
Imports System.Drawing
Imports System.Windows.Forms

Namespace Controls

    Friend NotInheritable Class GridStyler

        Private Sub New()
        End Sub

        ''' <summary>Applies the design table style to an existing grid.</summary>
        Public Shared Sub Apply(grid As DataGridView)
            If grid Is Nothing Then Return

            grid.BackgroundColor = Theme.White
            grid.BorderStyle = BorderStyle.None
            grid.EnableHeadersVisualStyles = False
            grid.RowHeadersVisible = False
            grid.AllowUserToResizeRows = False
            grid.ShowCellToolTips = True ' long values stay reachable even when columns are tight
            grid.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None
            grid.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing
            grid.ColumnHeadersHeight = Theme.HeightGridHeader
            grid.Font = Theme.FontBody12

            Dim headerStyle As New DataGridViewCellStyle()
            headerStyle.BackColor = Theme.GridHeader
            headerStyle.ForeColor = Theme.Ink
            headerStyle.SelectionBackColor = Theme.GridHeader
            headerStyle.SelectionForeColor = Theme.Ink
            headerStyle.Font = Theme.FontBody12
            headerStyle.Alignment = DataGridViewContentAlignment.MiddleLeft
            headerStyle.Padding = New Padding(8, 0, 8, 0)
            headerStyle.WrapMode = DataGridViewTriState.False
            grid.ColumnHeadersDefaultCellStyle = headerStyle

            grid.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal
            grid.GridColor = Theme.GridLine

            grid.RowTemplate.Height = Theme.HeightGridRow

            Dim cellStyle As New DataGridViewCellStyle()
            cellStyle.BackColor = Theme.White
            cellStyle.ForeColor = Theme.Ink
            ' QA B6 (shared with Owner C5): the blue selection is replaced by the
            ' theme-green tint; dark ink text keeps orange/low-stock rows readable
            ' when selected.
            cellStyle.SelectionBackColor = Theme.SelectionTint
            cellStyle.SelectionForeColor = Theme.SelectionInk
            cellStyle.Font = Theme.FontBody12
            cellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft
            cellStyle.Padding = New Padding(8, 0, 8, 0)
            cellStyle.WrapMode = DataGridViewTriState.False
            grid.DefaultCellStyle = cellStyle
            grid.RowTemplate.DefaultCellStyle = cellStyle

            Theme.SetDoubleBuffered(grid, True)
        End Sub

        ''' <summary>
        ''' Sets relative column weights (fills available width). Existing row
        ''' color logic is unaffected; column order remains as-is.
        ''' </summary>
        Public Shared Sub ApplyWeights(grid As DataGridView, ParamArray weights() As Double)
            If grid Is Nothing OrElse grid.ColumnCount = 0 Then Return
            grid.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
            Dim i As Integer = 0
            For Each col As DataGridViewColumn In grid.Columns
                Dim w As Double = 1.0
                If weights IsNot Nothing AndAlso weights.Length > 0 Then
                    w = weights(Math.Min(i, weights.Length - 1))
                End If
                col.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
                col.FillWeight = CSng(w)
                ' No truncation: fill weights shrink, but headers keep at least
                ' their full text width (plus 26px padding allowance).
                Dim headerW As Integer = TextRenderer.MeasureText(
                    col.HeaderText, Theme.FontBody12, Size.Empty,
                    TextFormatFlags.SingleLine Or TextFormatFlags.NoPrefix Or TextFormatFlags.NoPadding).Width
                col.MinimumWidth = Math.Max(40, headerW + 26)
                i += 1
            Next
        End Sub

        ''' <summary>Tints a row for low-stock / needs-review states (#FFF2E5 with orange text).</summary>
        Public Shared Sub MarkWarningRow(row As DataGridViewRow)
            If row Is Nothing Then Return
            row.DefaultCellStyle.BackColor = Theme.WarnTint
            If row.DefaultCellStyle.ForeColor.GetBrightness() > 0.3 Then
                row.DefaultCellStyle.ForeColor = Theme.Warn
            End If
        End Sub

        ''' <summary>Creates a 30px footer label ("Showing X of Y ... ‹ 1 ›").</summary>
        Public Shared Function NewFooter() As Label
            Dim f As New Label()
            f.AutoSize = False
            f.Height = Theme.HeightGridFooter
            f.ForeColor = Theme.Muted
            f.Font = Theme.FontSmall
            f.TextAlign = ContentAlignment.MiddleLeft
            f.BackColor = Theme.White
            f.Padding = New Padding(4, 0, 0, 0)
            f.Text = String.Empty
            Return f
        End Function

        Public Shared Sub UpdateFooter(f As Label, shown As Integer, total As Integer, caption As String)
            If f Is Nothing Then Return
            Dim s As String = $"Showing {shown} of {total} {caption}"
            s &= $"   ‹ 1 ›"
            f.Text = s
        End Sub

        ''' <summary>Free-form cart-style footer (e.g., "4 products · 6 units").</summary>
        Public Shared Sub UpdateSummary(f As Label, text As String)
            If f Is Nothing Then Return
            f.Text = text
        End Sub

        ''' <summary>
        ''' Overlays a centered muted message on an empty grid ("Cart is empty…").
        ''' The label is parented to the grid and hidden by the callers' updates
        ''' whenever the grid has rows.
        ''' </summary>
        Public Shared Function AttachEmptyOverlay(grid As DataGridView, text As String) As Label
            If grid Is Nothing Then Return Nothing
            Dim lbl As New Label()
            lbl.Text = text
            lbl.Font = Theme.FontBody12
            lbl.ForeColor = Theme.Muted
            lbl.BackColor = Theme.White
            lbl.TextAlign = ContentAlignment.MiddleCenter
            lbl.Dock = DockStyle.None
            lbl.Visible = False
            lbl.AutoSize = True
            lbl.BorderStyle = BorderStyle.None
            grid.Controls.Add(lbl)
            ' Keep it centered as the grid resizes.
            AddHandler grid.Resize, Sub(s As Object, e As EventArgs) CenterEmptyOverlay(grid, lbl)
            CenterEmptyOverlay(grid, lbl)
            Return lbl
        End Function

        Private Shared Sub CenterEmptyOverlay(grid As DataGridView, overlay As Label)
            If overlay Is Nothing OrElse grid Is Nothing Then Return
            overlay.Left = Math.Max(0, (grid.ClientSize.Width - overlay.Width) \ 2)
            overlay.Top = Math.Max(0, (grid.ClientSize.Height - overlay.Height) \ 2)
        End Sub

    End Class

End Namespace
