' ==============================================================================
' Project: Mini-Grocery Store POS & Daily Inventory System
' Module:  Forms / NutritionDetailView  (UI RESTYLE - PLACEHOLDER BACKED)
' Purpose: Modal detail dialog for one nutrition record: Per100g / PerServing
'          tabs, a raw JSON toggle (imported record shape), and a Done button.
'          Works entirely from the documented NutritionRecord contract so the
'          future OFF integration can feed it without UI changes.
' ==============================================================================

Option Strict On
Option Explicit On

Imports System
Imports System.Drawing
Imports System.Globalization
Imports System.Text
Imports System.Windows.Forms
Imports oopfinals.Controls
Imports oopfinals.Services

Namespace Forms

    ''' <summary>Modal nutrition details viewer; requires a non-null record.</summary>
    Public Class NutritionDetailView
        Inherits Form

        Private ReadOnly _rec As NutritionRecord
        Private ReadOnly _rawBox As TextBox
        Private ReadOnly _chkRaw As CheckBox

        Public Sub New(record As NutritionRecord)
            _rec = record

            Me.Text = "Nutrition details"
            Me.FormBorderStyle = FormBorderStyle.FixedDialog
            Me.MaximizeBox = False
            Me.MinimizeBox = False
            Me.StartPosition = FormStartPosition.CenterParent
            Me.ClientSize = New Size(520, 500)
            Me.BackColor = Theme.PageBg
            Me.Font = Theme.FontBody12

            Dim card As New CardPanel()
            card.Dock = DockStyle.Fill
            card.Margin = New Padding(16)
            card.Padding = New Padding(16, 14, 16, 12)

            Dim layout As New TableLayoutPanel()
            layout.Dock = DockStyle.Fill
            layout.BackColor = Theme.White
            layout.ColumnCount = 1
            layout.RowCount = 4
            layout.Margin = New Padding(0)
            layout.RowStyles.Add(New RowStyle(SizeType.AutoSize))        ' 0 title
            layout.RowStyles.Add(New RowStyle(SizeType.Percent, 100F))   ' 1 tabs
            layout.RowStyles.Add(New RowStyle(SizeType.AutoSize))        ' 2 raw section
            layout.RowStyles.Add(New RowStyle(SizeType.AutoSize))        ' 3 footer

            ' Title
            Dim lblTitle As New Label()
            lblTitle.Text = "Nutrition · " & If(_rec Is Nothing, "record", _rec.ProductCode)
            lblTitle.Font = Theme.FontCardTitle
            lblTitle.ForeColor = Theme.Ink
            lblTitle.AutoSize = True
            lblTitle.Margin = New Padding(0, 0, 0, 2)
            lblTitle.BackColor = Color.Transparent
            layout.Controls.Add(lblTitle, 0, 0)

            ' Tabs area (Per 100 g / Per serving)
            Dim tabs As New TabControl()
            tabs.Dock = DockStyle.Fill
            tabs.Font = Theme.FontBody12

            Dim page100 As New TabPage("Per 100 g")
            page100.BackColor = Theme.White
            page100.Padding = New Padding(2, 10, 2, 2)

            Dim p100 As New TableLayoutPanel()
            p100.Dock = DockStyle.Fill
            p100.BackColor = Theme.White
            p100.ColumnCount = 2
            p100.ColumnStyles.Add(New ColumnStyle(SizeType.Percent, 55F))
            p100.ColumnStyles.Add(New ColumnStyle(SizeType.Percent, 45F))
            If _rec IsNot Nothing Then
                AddRowPairs(p100,
                    "Energy", Fmt(_rec.EnergyKcal, "N1") & " kcal",
                    "Protein", Fmt(_rec.ProteinG, "N1") & " g",
                    "Fat", Fmt(_rec.FatG, "N1") & " g",
                    "Carbohydrates", Fmt(_rec.CarbsG, "N1") & " g",
                    "Sodium", If(_rec.SodiumMg.HasValue, Fmt(_rec.SodiumMg.Value, "N0") & " mg", "Not provided"),
                    "Source", _rec.SourceName,
                    "Imported", If(_rec.SourceDate = DateTime.MinValue, "—",
                                   _rec.SourceDate.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture)),
                    "Imported by", If(String.IsNullOrEmpty(_rec.ImportedBy), "—", _rec.ImportedBy))
            End If
            page100.Controls.Add(p100)

            Dim pageSrv As New TabPage("Per serving")
            pageSrv.BackColor = Theme.White
            pageSrv.Padding = New Padding(2, 10, 2, 2)

            Dim pSrv As New TableLayoutPanel()
            pSrv.Dock = DockStyle.Fill
            pSrv.BackColor = Theme.White
            pSrv.ColumnCount = 2
            pSrv.ColumnStyles.Add(New ColumnStyle(SizeType.Percent, 55F))
            pSrv.ColumnStyles.Add(New ColumnStyle(SizeType.Percent, 45F))
            If _rec IsNot Nothing AndAlso _rec.ServingSizeG.HasValue AndAlso _rec.HasPer100 Then
                AddRowPairs(pSrv,
                    "Serving size", Fmt(_rec.ServingSizeG.Value, "N1") & " g",
                    "Energy", Fmt(PerServing(_rec.EnergyKcal, _rec.ServingSizeG.Value), "N1") & " kcal",
                    "Protein", Fmt(PerServing(_rec.ProteinG, _rec.ServingSizeG.Value), "N1") & " g",
                    "Fat", Fmt(PerServing(_rec.FatG, _rec.ServingSizeG.Value), "N1") & " g",
                    "Carbohydrates", Fmt(PerServing(_rec.CarbsG, _rec.ServingSizeG.Value), "N1") & " g",
                    "Sodium", If(_rec.SodiumMg.HasValue, Fmt(PerServing(_rec.SodiumMg.Value, _rec.ServingSizeG.Value), "N0") & " mg", "Not provided"))
            Else
                AddRowPairs(pSrv,
                    "Serving size", "Not imported",
                    "Energy", "—",
                    "Protein", "—",
                    "Fat", "—",
                    "Carbohydrates", "—",
                    "Sodium", "—")
            End If
            pageSrv.Controls.Add(pSrv)

            tabs.TabPages.Add(page100)
            tabs.TabPages.Add(pageSrv)
            layout.Controls.Add(tabs, 0, 1)

            ' Raw JSON section
            Dim rawSection As New Panel()
            rawSection.Dock = DockStyle.Fill
            rawSection.Height = 218
            rawSection.Margin = New Padding(0)
            rawSection.BackColor = Theme.White

            _chkRaw = New CheckBox()
            _chkRaw.Text = "Show raw record (JSON)"
            _chkRaw.Font = Theme.FontBody12
            _chkRaw.ForeColor = Theme.Muted
            _chkRaw.AutoSize = True
            _chkRaw.Location = New Point(0, 0)
            _chkRaw.BackColor = Theme.White
            AddHandler _chkRaw.CheckedChanged, AddressOf OnRawToggle
            rawSection.Controls.Add(_chkRaw)

            _rawBox = New TextBox()
            _rawBox.Multiline = True
            _rawBox.ReadOnly = True
            _rawBox.ScrollBars = ScrollBars.Vertical
            _rawBox.Font = New Font("Consolas", 8.5F)
            _rawBox.BackColor = Color.FromArgb(250, 251, 250)
            _rawBox.BorderStyle = BorderStyle.FixedSingle
            _rawBox.Visible = False
            _rawBox.Text = BuildRawJson()
            AddHandler rawSection.Resize,
                Sub(s As Object, e As EventArgs)
                    _rawBox.Location = New Point(0, 22)
                    _rawBox.Size = New Size(rawSection.ClientSize.Width, rawSection.ClientSize.Height - 22)
                End Sub
            rawSection.Controls.Add(_rawBox)
            layout.Controls.Add(rawSection, 0, 2)

            ' Footer: Done (right)
            Dim footer As New FlowLayoutPanel()
            footer.Dock = DockStyle.Fill
            footer.FlowDirection = FlowDirection.RightToLeft
            footer.WrapContents = False
            footer.BackColor = Theme.White
            footer.Margin = New Padding(0, 4, 0, 0)

            Dim done As New Button()
            done.Text = "Done"
            Theme.StyleButton(done, ThemeButtonKind.Primary)
            done.Width = 120
            done.Margin = New Padding(0, 4, 0, 0)
            AddHandler done.Click, Sub(s As Object, e As EventArgs)
                                       Me.Close()
                                   End Sub
            footer.Controls.Add(done)
            layout.Controls.Add(footer, 0, 3)

            card.Controls.Add(layout)
            Controls.Add(card)
        End Sub

        Private Function PerServing(value As Decimal, servingSizeG As Decimal) As Decimal
            Return value * servingSizeG / 100.0D
        End Function

        Private Function Fmt(value As Decimal, format As String) As String
            Return value.ToString(format, CultureInfo.InvariantCulture)
        End Function

        Private Sub AddRowPairs(tlp As TableLayoutPanel, ParamArray pairs As String())
            Dim row As Integer = 0
            For i As Integer = 0 To pairs.Length - 1 Step 2
                Dim lk As New Label()
                lk.Text = pairs(i)
                lk.Font = Theme.FontBody12
                lk.ForeColor = Theme.Muted
                lk.AutoSize = True
                lk.Margin = New Padding(2, 8, 4, 0)
                lk.BackColor = Color.Transparent

                Dim lv As New Label()
                lv.Text = pairs(i + 1)
                lv.Font = Theme.FontBody13Semi
                lv.ForeColor = Theme.Ink
                lv.AutoSize = True
                lv.Margin = New Padding(4, 6, 0, 0)
                lv.BackColor = Color.Transparent

                tlp.Controls.Add(lk, 0, row)
                tlp.Controls.Add(lv, 1, row)
                row += 1
            Next
        End Sub

        Private Sub OnRawToggle(sender As Object, e As EventArgs)
            _rawBox.Visible = _chkRaw.Checked
        End Sub

        Private Function BuildRawJson() As String
            Dim sb As New StringBuilder()
            sb.AppendLine("{")
            sb.AppendLine("  ""source"": """ & _rec.SourceName & """,")
            sb.AppendLine("  ""per100g"": {")
            sb.AppendLine("    ""energyKcal"": " & Fmt(_rec.EnergyKcal, "0.0") & ",")
            sb.AppendLine("    ""proteinsG"": " & Fmt(_rec.ProteinG, "0.0") & ",")
            sb.AppendLine("    ""fatG"": " & Fmt(_rec.FatG, "0.0") & ",")
            sb.AppendLine("    ""carbohydratesG"": " & Fmt(_rec.CarbsG, "0.0") & ",")
            sb.AppendLine("    ""sodiumMg"": " & If(_rec.SodiumMg.HasValue, Fmt(_rec.SodiumMg.Value, "0"), "null") & ",")
            sb.AppendLine("    ""servingSizeG"": " & If(_rec.ServingSizeG.HasValue, Fmt(_rec.ServingSizeG.Value, "0.0"), "null"))
            sb.AppendLine("  }")
            sb.AppendLine("}")
            Return sb.ToString()
        End Function

    End Class

End Namespace
