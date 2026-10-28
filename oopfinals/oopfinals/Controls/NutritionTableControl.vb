' ==============================================================================
' Project: Mini-Grocery Store POS & Daily Inventory System
' Module:  Controls / NutritionTableControl  (UI RESTYLE - PLACEHOLDER BACKED)
' Purpose: Reusable nutrition catalog table. Reads through the documented
'          placeholder contract (Services.NutritionStore.TryGetForProduct) so
'          the future OffProductRepository can replace the data source without
'          UI changes. Shows an honest empty state while no import is linked.
'
' WIRING CONTRACT (future):
'   - LoadOwnerCatalog(products) enumerates products and joins nutrition rows.
'   - A record is Verified when HasPer100Nutrition(rec), else Needs review.
'   - Double-clicking / DetailsRequested notifies the host to open the detail
'     dialog via TryGetForCode.
' ==============================================================================

Option Strict On
Option Explicit On

Imports System
Imports System.Collections.Generic
Imports System.Drawing
Imports System.Windows.Forms
Imports oopfinals.Models
Imports oopfinals.Services

Namespace Controls

    Public Class NutritionTableControl
        Inherits Panel

        Private ReadOnly _grid As New DataGridView()
        Private ReadOnly _footer As Label
        Private ReadOnly _empty As Label

        ''' <summary>Raised when the user asks to open a record's detail view.</summary>
        Public Event DetailsRequested(sender As Object, e As EventArgs)

        Public Sub New()
            SetStyle(ControlStyles.OptimizedDoubleBuffer, True)
            BackColor = Theme.White
            Margin = New Padding(0)
            Padding = New Padding(0)

            GridStyler.Apply(_grid)
            _grid.Dock = DockStyle.Fill ' fills the card (was a fixed 200x100 strip)
            _grid.AllowUserToAddRows = False
            _grid.ReadOnly = True
            _grid.MultiSelect = False
            _grid.SelectionMode = DataGridViewSelectionMode.FullRowSelect
            _grid.Columns.Clear()
            _grid.Columns.Add("colNutCode", "Code")          ' 0
            _grid.Columns.Add("colNutName", "Product")       ' 1
            _grid.Columns.Add("colNutKcal", "Energy (kcal)") ' 2
            _grid.Columns.Add("colNutProt", "Protein (g)")   ' 3
            _grid.Columns.Add("colNutFat", "Fat (g)")        ' 4
            _grid.Columns.Add("colNutCarb", "Carbs (g)")     ' 5
            _grid.Columns.Add("colNutSod", "Sodium (mg)")    ' 6
            _grid.Columns.Add("colNutStatus", "Status")      ' 7
            _grid.Columns.Add("colNutUpd", "Updated")        ' 8
            _grid.Columns(8).Visible = False                 ' shown in the detail view

            _empty = GridStyler.AttachEmptyOverlay(_grid,
                "No nutrition records yet — this catalog fills once an Open Food Facts import is linked.")

            _footer = GridStyler.NewFooter()
            _footer.Height = 24
            _footer.Dock = DockStyle.Bottom
            _footer.BackColor = Theme.White

            Controls.Add(_grid)
            Controls.Add(_footer)

            ' Internal grid options visible to the user (QA C2): columns remain
            ' fill-weighted but stay user-resizable, cells tooltip when truncated.
            _grid.AllowUserToResizeColumns = True

            GridStyler.ApplyWeights(_grid, 0.55, 3.2, 0.9, 0.9, 0.9, 0.9, 1.05, 1.0)
            ' QA C2 ROOT CAUSE: ApplyWeights pins every column to at least its
            ' header text width; "Energy (kcal)"-style headers forced wide minimums
            ' that squeezed Product and inflated Code. Numeric columns get a small
            ' fixed floor instead so the Code/Product proportions dominate.
            For i As Integer = 2 To 7
                _grid.Columns(i).MinimumWidth = 64
            Next

            AddHandler _grid.CellDoubleClick, Sub(s As Object, e As DataGridViewCellEventArgs)
                                                  If _grid.SelectedRows.Count > 0 Then
                                                      RaiseEvent DetailsRequested(Me, EventArgs.Empty)
                                                  End If
                                              End Sub
        End Sub

        ''' <summary>Current row's product code (Nothing when no selection).</summary>
        Public ReadOnly Property SelectedCode As String
            Get
                If _grid.SelectedRows.Count = 0 Then Return Nothing
                Return Convert.ToString(_grid.SelectedRows(0).Cells(0).Value)
            End Get
        End Property

        ''' <summary>
        ''' Fills the table for the owner's catalog. With the placeholder data
        ''' source this yields zero rows; nothing is fabricated.
        ''' </summary>
        Public Sub LoadOwnerCatalog(products As List(Of Product))
            _grid.Rows.Clear()

            Dim added As Integer = 0
            If products IsNot Nothing Then
                For Each p As Product In products
                    Dim rec As NutritionRecord = NutritionStore.TryGetForProduct(p.ProductCode)
                    If rec Is Nothing Then Continue For

                    Dim status As String = If(NutritionStore.HasPer100Nutrition(rec), "Verified", "Needs review")
                    Dim updated As String = If(rec.SourceDate = DateTime.MinValue, "—", rec.SourceDate.ToString("yyyy-MM-dd"))
                    Dim rowIdx As Integer = _grid.Rows.Add(
                        p.ProductCode, p.Name,
                        If(rec.HasPer100, rec.EnergyKcal.ToString("N1"), "—"),
                        If(rec.HasPer100, rec.ProteinG.ToString("N1"), "—"),
                        If(rec.HasPer100, rec.FatG.ToString("N1"), "—"),
                        If(rec.HasPer100, rec.CarbsG.ToString("N1"), "—"),
                        If(rec.SodiumMg.HasValue, rec.SodiumMg.Value.ToString("N0"), "—"),
                        status, updated)

                    Dim stl As DataGridViewCellStyle = _grid.Rows(rowIdx).Cells(7).Style
                    If status = "Verified" Then
                        stl.ForeColor = Theme.Green
                        stl.Font = Theme.FontBody12Semi
                    Else
                        stl.ForeColor = Theme.Warn
                        stl.Font = Theme.FontBody12Semi
                    End If
                    added += 1
                Next
            End If

            GridStyler.UpdateFooter(_footer, added, added, If(added = 1, "record", "records"))
            ' QA C3: visible basis label — the rows are stored per 100 g / 100 ml.
            _footer.Text = "Values per 100 g / 100 ml   ·   " & _footer.Text

            ' QA C5: a fresh load left row 0 auto-selected, which used to paint as
            ' a stray blue bar (and now would paint green); clear the phantom
            ' selection so the catalog shows no highlighted row out of nowhere.
            If _grid.Rows.Count > 0 Then _grid.ClearSelection()

            _empty.Visible = (added = 0)
            If _empty.Visible Then
                _empty.Left = Math.Max(0, (_grid.ClientSize.Width - _empty.Width) \ 2)
                _empty.Top = Math.Max(0, (_grid.ClientSize.Height - _empty.Height) \ 2)
                _empty.BringToFront()
            End If
        End Sub

    End Class

End Namespace
