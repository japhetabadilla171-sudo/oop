' ==============================================================================
' Project: Mini-Grocery Store POS & Daily Inventory System
' Course: Object-Oriented Programming (ITP 102) - University of Caloocan City
' Form: ClerkDashboardForm (Code-Behind)
' OOP Concept: TRANSACTION INTEGRITY, AUDIT TRAIL, ROLE RESTRICTION
' Provides inventory operations for the Clerk: stock alerts with color coding,
' stock-in inbound deliveries, manual adjustments, audit logs, and read-only catalog.
' ==============================================================================

Option Strict On
Option Explicit On

Imports System
Imports System.Collections.Generic
Imports System.Drawing
Imports System.Windows.Forms
Imports oopfinals.Data
Imports oopfinals.Models
Imports oopfinals.Controls

Namespace Forms
    Public Class ClerkDashboardForm
        Private ReadOnly _clerk As InventoryClerk
        Private ReadOnly _productRepo As New ProductRepository()
        Private ReadOnly _supplierRepo As New SupplierRepository()
        Private ReadOnly _stockRepo As New StockRepository()

        Public Sub New(clerk As InventoryClerk)
            InitializeComponent()
            _clerk = clerk
            Me.DoubleBuffered = True
            InitializeUiRestyle()
        End Sub

        Private Sub ClerkDashboardForm_Resize(sender As Object, e As EventArgs) Handles MyBase.Resize
            Me.Refresh()
        End Sub

        Private Sub ClerkDashboardForm_Load(sender As Object, e As EventArgs) Handles MyBase.Load
            ' SECURITY: verify role on form load
            If _clerk.Role <> "InventoryClerk" Then
                MessageBox.Show("Access denied: this dashboard is restricted to Inventory Clerk accounts.", "Access Denied", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Me.BeginInvoke(New MethodInvoker(Sub() Me.Close()))
                Return
            End If

            ' Step 7 (Figma): "Role: Inventory Clerk · <Full name> (@username)".
            lblClerkInfo.Text = $"Role: {_clerk.GetRoleName()} · {_clerk.GetDisplayName()} (@{_clerk.Username})"
            AppHeader.Apply(pnlHeader, lblAppTitle, lblClerkInfo, btnLogout) ' RESTYLE: reposition after label text set here

            PopulateSuppliers()
            PopulateAdjustmentReasons()
            PopulateMovementTypes()
            PopulateProductSelectors()

            dtpMovStart.Value = DateTime.Today.AddDays(-7)
            dtpMovEnd.Value = DateTime.Today

            LoadAlerts()
            LoadMovements()
            LoadCatalog()
        End Sub

        Private Sub PopulateSuppliers()
            cboStockInSupplier.Items.Clear()
            Dim suppliers As List(Of Supplier) = _supplierRepo.GetAll()
            For Each s In suppliers
                cboStockInSupplier.Items.Add(s)
            Next
            If cboStockInSupplier.Items.Count > 0 Then cboStockInSupplier.SelectedIndex = 0
        End Sub

        Private Sub PopulateProductSelectors()
            cboStockInProduct.Items.Clear()
            cboAdjProduct.Items.Clear()

            Dim prods As List(Of Product) = _productRepo.GetAll(activeOnly:=True)
            For Each p In prods
                cboStockInProduct.Items.Add(p)
                cboAdjProduct.Items.Add(p)
            Next

            If cboStockInProduct.Items.Count > 0 Then cboStockInProduct.SelectedIndex = 0
            If cboAdjProduct.Items.Count > 0 Then cboAdjProduct.SelectedIndex = 0
        End Sub

        Private Sub PopulateAdjustmentReasons()
            cboAdjType.Items.Clear()
            cboAdjType.Items.Add("Deduct / Decrease (-)")
            cboAdjType.Items.Add("Add / Increase (+)")
            cboAdjType.SelectedIndex = 0

            cboAdjReason.Items.Clear()
            cboAdjReason.Items.Add("Damaged Goods / Spillage")
            cboAdjReason.Items.Add("Expired Stock Disposal")
            cboAdjReason.Items.Add("Physical Inventory Count Correction")
            cboAdjReason.Items.Add("Customer Return / Restocked")
            cboAdjReason.Items.Add("Defective / Factory Recall")
            cboAdjReason.SelectedIndex = 0
        End Sub

        Private Sub PopulateMovementTypes()
            cboMovType.Items.Clear()
            cboMovType.Items.Add("All")
            cboMovType.Items.Add("StockIn")
            cboMovType.Items.Add("Adjustment")
            cboMovType.Items.Add("Sale")
            cboMovType.SelectedIndex = 0
        End Sub

        ''' <summary>
        ''' Loads inventory alerts and highlights items with color coding:
        ''' Red = Expired, DarkOrange = Low Stock, Gold = Expiring Soon.
        ''' </summary>
        Private Sub LoadAlerts()
            dgvAlerts.Rows.Clear()
            Dim products As List(Of Product) = _productRepo.GetAll(activeOnly:=True)

            Dim lowCount As Integer = 0
            Dim expSoonCount As Integer = 0
            Dim expiredCount As Integer = 0

            For Each p In products
                Dim isLow As Boolean = p.IsLowStock()
                Dim isExp As Boolean = p.IsExpired()
                Dim isExpSoon As Boolean = p.IsExpiringSoon(30)

                If isLow Then lowCount += 1
                If isExpSoon Then expSoonCount += 1
                If isExp Then expiredCount += 1

                ' Filter selection
                Dim showRow As Boolean = False
                If rbAllAlerts.Checked AndAlso (isLow OrElse isExp OrElse isExpSoon) Then
                    showRow = True
                ElseIf rbLowStock.Checked AndAlso isLow Then
                    showRow = True
                ElseIf rbExpiring.Checked AndAlso isExpSoon Then
                    showRow = True
                ElseIf rbExpired.Checked AndAlso isExp Then
                    showRow = True
                End If

                If showRow Then
                    Dim statusText As String = ""
                    Dim rowBg As Color = Theme.White
                    Dim rowFg As Color = Theme.Ink

                    If isExp Then
                        statusText = "EXPIRED"
                        rowBg = Theme.ErrorTint
                        rowFg = Theme.ErrorText
                    ElseIf isLow AndAlso isExpSoon Then
                        statusText = "LOW & EXPIRING"
                        rowBg = Theme.WarnTint
                        rowFg = Theme.ErrorText
                    ElseIf isLow Then
                        statusText = "LOW STOCK"
                        rowBg = Theme.WarnTint
                        rowFg = Theme.Warn
                    ElseIf isExpSoon Then
                        statusText = "EXPIRING SOON"
                        rowBg = Theme.InfoTint
                        rowFg = Theme.Info
                    End If

                    Dim expDateStr As String = If(p.ExpiryDate.HasValue, p.ExpiryDate.Value.ToString("yyyy-MM-dd"), "N/A")
                    Dim rowIdx As Integer = dgvAlerts.Rows.Add(p.ProductCode, p.Name, p.CategoryName, p.StockQty, p.ReorderLevel, expDateStr, statusText)
                    ' RESTYLE (visual only): row tint tokens + semibold text
                    With dgvAlerts.Rows(rowIdx).DefaultCellStyle
                        .BackColor = rowBg
                        .ForeColor = rowFg
                        .Font = Theme.FontBody12Semi
                    End With
                End If
            Next

            lblKpiSummary.Text = $"Alerts: {lowCount} Low Stock | {expSoonCount} Expiring Soon | {expiredCount} Expired"

            ' RESTYLE (visual only): footer + empty state
            UpdateAlertsFooter()
        End Sub

        Private Sub rbAlertFilter_CheckedChanged(sender As Object, e As EventArgs) Handles rbAllAlerts.CheckedChanged, rbLowStock.CheckedChanged, rbExpiring.CheckedChanged, rbExpired.CheckedChanged
            Dim rb = TryCast(sender, RadioButton)
            If rb IsNot Nothing AndAlso rb.Checked Then
                LoadAlerts()
            End If
        End Sub

        Private Sub btnRefreshAlerts_Click(sender As Object, e As EventArgs) Handles btnRefreshAlerts.Click
            LoadAlerts()
        End Sub

        Private Sub chkUpdateExpiry_CheckedChanged(sender As Object, e As EventArgs) Handles chkUpdateExpiry.CheckedChanged
            dtpStockInExpiry.Enabled = chkUpdateExpiry.Checked
        End Sub

        Private Sub btnSubmitStockIn_Click(sender As Object, e As EventArgs) Handles btnSubmitStockIn.Click
            lblStockInMsg.Text = String.Empty

            Dim selectedProd As Product = TryCast(cboStockInProduct.SelectedItem, Product)
            If selectedProd Is Nothing Then
                lblStockInMsg.Text = "Please select a product."
                lblStockInMsg.ForeColor = Color.Red
                Return
            End If

            Dim selectedSup As Supplier = TryCast(cboStockInSupplier.SelectedItem, Supplier)
            Dim supId As Nullable(Of Integer) = If(selectedSup IsNot Nothing, selectedSup.SupplierId, CType(Nothing, Nullable(Of Integer)))

            Dim qty As Integer = Convert.ToInt32(numStockInQty.Value)
            Dim newExpiry As Nullable(Of DateTime) = Nothing
            If chkUpdateExpiry.Checked Then
                newExpiry = dtpStockInExpiry.Value.Date

                ' BUSINESS RULE: expired stock received must be rejected (record disposal instead)
                If newExpiry.Value.Date <= DateTime.Today Then
                    lblStockInMsg.Text = "Stock with an expired or today-expiring date must be rejected, not received." & vbCrLf &
                                         "Use the Disposal adjustment for expired stock."
                    lblStockInMsg.ForeColor = Color.Red
                    Return
                End If
            End If

            Dim notes As String = txtStockInNotes.Text.Trim()
            If String.IsNullOrWhiteSpace(notes) Then notes = "Supplier Delivery"

            Try
                If _stockRepo.RecordStockIn(selectedProd.ProductId, qty, supId, _clerk.UserId, newExpiry, notes) Then
                    lblStockInMsg.Text = $"Successfully stocked in {qty} units of '{selectedProd.Name}'!"
                    lblStockInMsg.ForeColor = Color.DarkGreen
                    txtStockInNotes.Clear()
                    numStockInQty.Value = 10
                    chkUpdateExpiry.Checked = False

                    ' Reload dependent views
                    LoadAlerts()
                    LoadMovements()
                    LoadCatalog()
                    PopulateProductSelectors()
                Else
                    lblStockInMsg.Text = "Failed to record stock-in."
                    lblStockInMsg.ForeColor = Color.Red
                End If
            Catch ex As Exception
                lblStockInMsg.Text = ex.Message
                lblStockInMsg.ForeColor = Color.Red
            End Try
        End Sub

        Private Sub cboAdjProduct_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cboAdjProduct.SelectedIndexChanged
            Dim p As Product = TryCast(cboAdjProduct.SelectedItem, Product)
            If p IsNot Nothing Then
                lblAdjCurrentStock.Text = $"Current Stock: {p.StockQty} unit(s)"
            End If
        End Sub

        Private Sub btnSubmitAdjustment_Click(sender As Object, e As EventArgs) Handles btnSubmitAdjustment.Click
            lblAdjMsg.Text = String.Empty

            Dim selectedProd As Product = TryCast(cboAdjProduct.SelectedItem, Product)
            If selectedProd Is Nothing Then
                lblAdjMsg.Text = "Please select a product."
                lblAdjMsg.ForeColor = Color.Red
                Return
            End If

            Dim rawQty As Integer = Convert.ToInt32(numAdjQty.Value)
            Dim isDeduct As Boolean = cboAdjType.SelectedIndex = 0
            Dim qtyChange As Integer = If(isDeduct, -rawQty, rawQty)

            Dim reasonCategory As String = Convert.ToString(cboAdjReason.SelectedItem)
            Dim userDetails As String = txtAdjNotes.Text.Trim()

            ' BUSINESS RULE: spoilage/damage/expiry deductions require a specific written reason
            Dim isSpoilage As Boolean = isDeduct AndAlso
                (reasonCategory = "Damaged Goods / Spillage" OrElse reasonCategory = "Expired Stock Disposal")
            If isSpoilage AndAlso String.IsNullOrWhiteSpace(userDetails) Then
                lblAdjMsg.Text = $"A reason detail in Notes is required for '{reasonCategory}' deductions."
                lblAdjMsg.ForeColor = Color.Red
                txtAdjNotes.Focus()
                Return
            End If

            Dim fullReason As String = If(String.IsNullOrWhiteSpace(userDetails), reasonCategory, $"{reasonCategory} - {userDetails}")

            Try
                If _stockRepo.RecordAdjustment(selectedProd.ProductId, qtyChange, fullReason, _clerk.UserId) Then
                    lblAdjMsg.Text = $"Adjustment applied successfully: {qtyChange:+0;-0} unit(s) for '{selectedProd.Name}'!"
                    lblAdjMsg.ForeColor = Color.DarkGreen
                    txtAdjNotes.Clear()
                    numAdjQty.Value = 1

                    ' Reload dependent views
                    LoadAlerts()
                    LoadMovements()
                    LoadCatalog()
                    PopulateProductSelectors()
                Else
                    lblAdjMsg.Text = "Failed to apply adjustment."
                    lblAdjMsg.ForeColor = Color.Red
                End If
            Catch ex As Exception
                lblAdjMsg.Text = ex.Message
                lblAdjMsg.ForeColor = Color.Red
            End Try
        End Sub

        Private Sub LoadMovements()
            dgvMovements.Rows.Clear()
            Dim mType As String = Convert.ToString(cboMovType.SelectedItem)
            Dim sDate As DateTime = dtpMovStart.Value.Date
            Dim eDate As DateTime = dtpMovEnd.Value.Date.AddDays(1).AddSeconds(-1)

            Dim movements As List(Of StockMovement) = _stockRepo.GetMovements(Nothing, mType, sDate, eDate)
            For Each m In movements
                Dim rowIdx As Integer = dgvMovements.Rows.Add(m.MovementId, m.MovementDate.ToString("yyyy-MM-dd hh:mm tt"), m.ProductCode, m.ProductName, m.MovementType, m.QuantityChange.ToString("+0;-0;0"), m.Reason, m.SupplierName, m.UserName)

                If m.QuantityChange > 0 Then
                    dgvMovements.Rows(rowIdx).Cells(5).Style.ForeColor = Color.DarkGreen
                Else
                    dgvMovements.Rows(rowIdx).Cells(5).Style.ForeColor = Color.Red
                End If
            Next

            ' RESTYLE (visual only): footer + empty state
            UpdateMovementsFooter(movements.Count)
        End Sub

        Private Sub MovementFilter_Changed(sender As Object, e As EventArgs) Handles cboMovType.SelectedIndexChanged, dtpMovStart.ValueChanged, dtpMovEnd.ValueChanged
            LoadMovements()
        End Sub

        Private Sub LoadCatalog()
            dgvCatalog.Rows.Clear()
            Dim kw As String = txtCatalogSearch.Text.Trim()
            Dim prods As List(Of Product) = _productRepo.Search(kw, Nothing, activeOnly:=True)

            For Each p In prods
                Dim expStr As String = If(p.ExpiryDate.HasValue, p.ExpiryDate.Value.ToString("yyyy-MM-dd"), "N/A")
                dgvCatalog.Rows.Add(p.ProductCode, p.Name, p.CategoryName, p.Price.ToString("N2"), p.CostPrice.ToString("N2"), p.StockQty, p.ReorderLevel, expStr)
            Next

            ' RESTYLE (visual only): footer + empty state
            UpdateCatalogFooter(prods.Count)
        End Sub

        Private Sub txtCatalogSearch_TextChanged(sender As Object, e As EventArgs) Handles txtCatalogSearch.TextChanged
            LoadCatalog()
        End Sub

        Private Sub btnLogout_Click(sender As Object, e As EventArgs) Handles btnLogout.Click
            If MessageBox.Show("Are you sure you want to log out?", "Confirm Logout", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then
                Me.Close()
            End If
        End Sub

#Region "Figma UI restyle (visual only - no business behavior changes)"

        ' ================= UI-only state =================
        Private _lblAlertsFooter As Label
        Private _lblAlertsEmpty As Label
        Private _lblMovementsFooter As Label
        Private _lblMovementsEmpty As Label
        Private _lblCatalogFooter As Label
        Private _lblCatalogEmpty As Label

        ''' <summary>Applies the design tokens + layout. Visual only.</summary>
        Private Sub InitializeUiRestyle()
            Me.Text = "Mini-Grocery Store · Inventory Clerk"
            Me.ClientSize = New Size(1280, 800)
            Me.MinimumSize = New Size(1024, 700)
            ' QA E1/E4: open maximized like the other portals (constructor-time).
            Me.WindowState = FormWindowState.Maximized

            AppHeader.Apply(pnlHeader, lblAppTitle, lblClerkInfo, btnLogout)
            ShellTabs.Apply(tabClerk,
                New ShellTabSpec(tabAlerts, "Alerts", "alert-circle"),
                New ShellTabSpec(tabStockIn, "Stock-In", "package"),
                New ShellTabSpec(tabAdjustment, "Adjustment", "tags"),
                New ShellTabSpec(tabMovements, "Movements", "clock"),
                New ShellTabSpec(tabCatalog, "Catalog", "basket"))

            RestyleAlerts()
            RestyleStockIn()
            RestyleAdjustment()
            RestyleMovements()
            RestyleCatalog()

            Theme.RefreshLayout(Me)
        End Sub

        ' ================= Alerts tab =================
        Private Sub RestyleAlerts()
            tabAlerts.BackColor = Theme.PageBg

            pnlAlertTop.Visible = False
            tabAlerts.Controls.Remove(pnlAlertTop)

            Dim card As New CardPanel()
            card.Dock = DockStyle.Fill
            card.Margin = New Padding(0)
            card.Padding = New Padding(14, 10, 14, 8)

            Dim body As New Panel()
            body.Dock = DockStyle.Fill
            body.BackColor = Theme.White
            body.Margin = New Padding(0)
            body.Padding = New Padding(0)

            GridStyler.Apply(dgvAlerts)
            GridStyler.ApplyWeights(dgvAlerts, 1.0, 2.2, 1.2, 0.8, 1.0, 1.2, 1.6)
            body.Controls.Add(dgvAlerts)
            _lblAlertsEmpty = GridStyler.AttachEmptyOverlay(dgvAlerts, "No alerts for this filter.")

            _lblAlertsFooter = GridStyler.NewFooter()
            _lblAlertsFooter.Height = 24
            _lblAlertsFooter.Dock = DockStyle.Bottom
            _lblAlertsFooter.BackColor = Theme.White

            card.Controls.Add(body)
            card.Controls.Add(_lblAlertsFooter)

            ' Radio filter strip (kept: same controls, same CheckedChanged handling)
            Dim filterStrip As New Panel()
            filterStrip.Dock = DockStyle.Top
            filterStrip.Height = 40
            filterStrip.BackColor = Theme.PageBg
            filterStrip.Margin = New Padding(0)
            filterStrip.Padding = New Padding(2, 8, 0, 0)

            Dim rbFlow As New FlowLayoutPanel()
            rbFlow.Dock = DockStyle.Fill
            rbFlow.FlowDirection = FlowDirection.LeftToRight
            rbFlow.WrapContents = False
            rbFlow.BackColor = Theme.PageBg
            rbFlow.Margin = New Padding(0)
            rbFlow.Padding = New Padding(0)

            For Each rb As RadioButton In New RadioButton() {rbAllAlerts, rbLowStock, rbExpiring, rbExpired}
                rb.AutoSize = True
                rb.Font = Theme.FontBody12
                rb.ForeColor = Theme.Ink
                rb.BackColor = Theme.PageBg
                rb.Margin = New Padding(0, 2, 28, 0)
                rbFlow.Controls.Add(rb)
            Next
            filterStrip.Controls.Add(rbFlow)

            tabAlerts.Controls.Add(card)
            tabAlerts.Controls.Add(filterStrip)

            Dim heading As PageHeading = PageHeading.Build(tabAlerts, "Inventory alerts", _
                "Products that need attention before customers are affected.")
            heading.AddChip(New StatusChip("Needs review", ThemeTone.Warn))

            lblKpiSummary.Font = Theme.FontSmall
            lblKpiSummary.ForeColor = Theme.Muted
            lblKpiSummary.AutoSize = True
            lblKpiSummary.BackColor = Color.Transparent
            heading.AddLabel(lblKpiSummary)

            btnRefreshAlerts.Text = "Refresh"
            Theme.StyleButton(btnRefreshAlerts, ThemeButtonKind.Secondary)
            btnRefreshAlerts.AutoSize = True
            heading.AddAction(btnRefreshAlerts)
        End Sub

        Private Sub UpdateAlertsFooter()
            If _lblAlertsFooter Is Nothing Then Return
            Dim n As Integer = dgvAlerts.RowCount
            GridStyler.UpdateFooter(_lblAlertsFooter, n, n, If(n = 1, "alert", "alerts"))
            If _lblAlertsEmpty IsNot Nothing Then _lblAlertsEmpty.Visible = (n = 0)
        End Sub

        ' ================= Stock-In tab =================
        Private Sub RestyleStockIn()
            tabStockIn.BackColor = Theme.PageBg

            grpStockIn.Visible = False
            tabStockIn.Controls.Remove(grpStockIn)

            Dim card As New CardPanel()
            card.Dock = DockStyle.Fill
            card.Margin = New Padding(0, 0, 12, 0)
            card.Padding = New Padding(16, 12, 16, 12)
            card.AutoScroll = True ' keeps the submit row reachable on short windows

            Dim tlp As New TableLayoutPanel()
            tlp.Dock = DockStyle.Top ' grows to its rows; scrolls inside the card when tight
            tlp.AutoSize = True
            tlp.AutoSizeMode = AutoSizeMode.GrowOnly
            tlp.BackColor = Theme.White
            tlp.ColumnCount = 1
            tlp.RowCount = 16
            tlp.Margin = New Padding(0)
            tlp.Padding = New Padding(0)
            For r As Integer = 0 To 14
                tlp.RowStyles.Add(New RowStyle(SizeType.AutoSize))
            Next
            tlp.RowStyles.Add(New RowStyle(SizeType.Percent, 100F))
            Dim addRow As Action(Of Control) = Sub(c As Control)
                                                   tlp.Controls.Add(c, 0, tlp.Controls.Count)
                                               End Sub

            Dim lblTitle As New Label()
            lblTitle.Text = "Record inbound delivery"
            lblTitle.Font = Theme.FontCardTitle
            lblTitle.ForeColor = Theme.Ink
            lblTitle.AutoSize = True
            lblTitle.Margin = New Padding(0, 0, 0, 6)
            addRow(lblTitle)

            addRow(FormLabel(lblStockInProduct, "Select product:"))
            addRow(WrapCombo(cboStockInProduct))
            addRow(FormLabel(lblStockInSupplier, "Source supplier:"))
            addRow(WrapCombo(cboStockInSupplier))
            addRow(FormLabel(lblStockInQty, "Quantity delivered:"))
            addRow(WrapNum(numStockInQty, 120))
            addRow(FormLabel(lblStockInExpiry, "Batch expiry date:"))

            Dim expRow As New Panel()
            expRow.Dock = DockStyle.Fill
            expRow.Height = 34
            expRow.BackColor = Theme.White
            expRow.Margin = New Padding(0, 0, 0, 0)
            chkUpdateExpiry.Font = Theme.FontBody12
            chkUpdateExpiry.ForeColor = Theme.Ink
            chkUpdateExpiry.AutoSize = True
            chkUpdateExpiry.BackColor = Theme.White
            chkUpdateExpiry.Location = New Point(0, 4)
            expRow.Controls.Add(chkUpdateExpiry)
            dtpStockInExpiry.Font = Theme.FontBody12
            dtpStockInExpiry.Location = New Point(190, 2)
            expRow.Controls.Add(dtpStockInExpiry)
            AddHandler expRow.Resize,
                Sub(s As Object, e As EventArgs)
                    expRow.Height = 34
                    dtpStockInExpiry.Top = 2
                End Sub
            addRow(expRow)

            addRow(FormLabel(lblStockInNotes, "Delivery ref / remarks:"))
            addRow(WrapTB(txtStockInNotes, "Delivery reference or remarks (optional)"))

            btnSubmitStockIn.Text = "Record stock-in"
            Theme.StyleButton(btnSubmitStockIn, ThemeButtonKind.Primary)
            btnSubmitStockIn.AutoSize = True
            addRow(MsgRow(btnSubmitStockIn, lblStockInMsg))

            Dim filler As New Panel()
            filler.Dock = DockStyle.Fill
            filler.BackColor = Theme.White
            filler.Margin = New Padding(0)
            addRow(filler)

            card.Controls.Add(tlp)

            ' Body: card at 470px + page filler
            Dim columns As New TableLayoutPanel()
            columns.Dock = DockStyle.Fill
            columns.BackColor = Theme.PageBg
            columns.Margin = New Padding(0)
            columns.Padding = New Padding(0)
            columns.ColumnCount = 2
            columns.RowCount = 1
            columns.ColumnStyles.Add(New ColumnStyle(SizeType.Absolute, 470F))
            columns.ColumnStyles.Add(New ColumnStyle(SizeType.Percent, 100F))
            columns.RowStyles.Add(New RowStyle(SizeType.Percent, 100F))
            columns.Controls.Add(card, 0, 0)

            tabStockIn.Controls.Add(columns)

            Dim heading As PageHeading = PageHeading.Build(tabStockIn, "Stock-in · Deliveries", _
                "Log inbound supplier deliveries and keep batch expiry dates current.")
            heading.AddChip(New StatusChip("Inbound", ThemeTone.Info))
        End Sub

        ' ================= Adjustment tab =================
        Private Sub RestyleAdjustment()
            tabAdjustment.BackColor = Theme.PageBg

            grpAdjustment.Visible = False
            tabAdjustment.Controls.Remove(grpAdjustment)

            Dim card As New CardPanel()
            card.Dock = DockStyle.Fill
            card.Margin = New Padding(0, 0, 12, 0)
            card.Padding = New Padding(16, 12, 16, 12)
            card.AutoScroll = True ' whole form stays visible (Submit button never clipped)

            Dim tlp As New TableLayoutPanel()
            tlp.Dock = DockStyle.Top ' grows to its rows; scrolls inside the card when tight
            tlp.AutoSize = True
            tlp.AutoSizeMode = AutoSizeMode.GrowOnly
            tlp.BackColor = Theme.White
            tlp.ColumnCount = 1
            tlp.RowCount = 16
            tlp.Margin = New Padding(0)
            tlp.Padding = New Padding(0)
            For r As Integer = 0 To 14
                tlp.RowStyles.Add(New RowStyle(SizeType.AutoSize))
            Next
            tlp.RowStyles.Add(New RowStyle(SizeType.Percent, 100F))
            Dim addRow As Action(Of Control) = Sub(c As Control)
                                                   tlp.Controls.Add(c, 0, tlp.Controls.Count)
                                               End Sub

            Dim lblTitle As New Label()
            lblTitle.Text = "Physical correction & adjustment"
            lblTitle.UseMnemonic = False ' render "&" literally
            lblTitle.Font = Theme.FontCardTitle
            lblTitle.ForeColor = Theme.Ink
            lblTitle.AutoSize = True
            lblTitle.Margin = New Padding(0, 0, 0, 6)
            addRow(lblTitle)

            addRow(FormLabel(lblAdjProduct, "Select product:"))
            addRow(WrapCombo(cboAdjProduct))

            lblAdjCurrentStock.Font = Theme.FontBody12Semi
            lblAdjCurrentStock.ForeColor = Theme.Info
            lblAdjCurrentStock.AutoSize = True
            lblAdjCurrentStock.BackColor = Color.Transparent
            lblAdjCurrentStock.Margin = New Padding(0, 4, 0, 0)
            addRow(lblAdjCurrentStock)

            addRow(FormLabel(lblAdjType, "Adjustment type:"))
            addRow(WrapCombo(cboAdjType))
            addRow(FormLabel(lblAdjQty, "Adjustment amount:"))
            addRow(WrapNum(numAdjQty, 120))
            addRow(FormLabel(lblAdjReason, "Reason (compulsory):"))
            addRow(WrapCombo(cboAdjReason))
            addRow(FormLabel(lblAdjNotes, "Additional details:"))
            addRow(WrapTB(txtAdjNotes, "Required for damage / expiry deductions"))

            btnSubmitAdjustment.Text = "Apply adjustment"
            Theme.StyleButton(btnSubmitAdjustment, ThemeButtonKind.Primary)
            btnSubmitAdjustment.AutoSize = True
            addRow(MsgRow(btnSubmitAdjustment, lblAdjMsg))

            Dim filler As New Panel()
            filler.Dock = DockStyle.Fill
            filler.BackColor = Theme.White
            filler.Margin = New Padding(0)
            addRow(filler)

            card.Controls.Add(tlp)

            Dim columns As New TableLayoutPanel()
            columns.Dock = DockStyle.Fill
            columns.BackColor = Theme.PageBg
            columns.Margin = New Padding(0)
            columns.Padding = New Padding(0)
            columns.ColumnCount = 2
            columns.RowCount = 1
            columns.ColumnStyles.Add(New ColumnStyle(SizeType.Absolute, 490F))
            columns.ColumnStyles.Add(New ColumnStyle(SizeType.Percent, 100F))
            columns.RowStyles.Add(New RowStyle(SizeType.Percent, 100F))
            columns.Controls.Add(card, 0, 0)

            tabAdjustment.Controls.Add(columns)

            Dim heading As PageHeading = PageHeading.Build(tabAdjustment, "Stock adjustment", _
                "Correct physical counts with an explicit, auditable reason.")
            heading.AddChip(New StatusChip("Audit trail", ThemeTone.Info))
        End Sub

        ' ================= Movements tab =================
        Private Sub RestyleMovements()
            tabMovements.BackColor = Theme.PageBg

            pnlMovementsTop.Visible = False
            tabMovements.Controls.Remove(pnlMovementsTop)

            ' Filter strip: kept date pickers + type combo (same events)
            Dim strip As New Panel()
            strip.Dock = DockStyle.Top
            strip.Height = 46
            strip.BackColor = Theme.PageBg
            strip.Margin = New Padding(0)

            Dim tlp As New TableLayoutPanel()
            tlp.Dock = DockStyle.Fill
            tlp.BackColor = Theme.PageBg
            tlp.Margin = New Padding(0)
            tlp.ColumnCount = 6
            tlp.RowCount = 1
            tlp.ColumnStyles.Add(New ColumnStyle(SizeType.AutoSize))          ' from label
            tlp.ColumnStyles.Add(New ColumnStyle(SizeType.Absolute, 190F))    ' dtp start
            tlp.ColumnStyles.Add(New ColumnStyle(SizeType.AutoSize))          ' to label
            tlp.ColumnStyles.Add(New ColumnStyle(SizeType.Absolute, 190F))    ' dtp end
            tlp.ColumnStyles.Add(New ColumnStyle(SizeType.AutoSize))          ' type label
            tlp.ColumnStyles.Add(New ColumnStyle(SizeType.Percent, 100F))     ' combo/fill
            tlp.RowStyles.Add(New RowStyle(SizeType.Percent, 100F))

            lblMovStart.Font = Theme.FontSmall
            lblMovStart.ForeColor = Theme.Muted
            lblMovStart.AutoSize = True
            lblMovStart.Margin = New Padding(0, 0, 8, 0)
            tlp.Controls.Add(lblMovStart, 0, 0)

            dtpMovStart.Font = Theme.FontBody12
            dtpMovStart.Margin = New Padding(0, 6, 12, 6)
            tlp.Controls.Add(dtpMovStart, 1, 0)

            lblMovEnd.Font = Theme.FontSmall
            lblMovEnd.ForeColor = Theme.Muted
            lblMovEnd.AutoSize = True
            lblMovEnd.Margin = New Padding(0, 0, 8, 0)
            tlp.Controls.Add(lblMovEnd, 2, 0)

            dtpMovEnd.Font = Theme.FontBody12
            dtpMovEnd.Margin = New Padding(0, 6, 12, 6)
            tlp.Controls.Add(dtpMovEnd, 3, 0)

            lblMovType.Font = Theme.FontSmall
            lblMovType.ForeColor = Theme.Muted
            lblMovType.AutoSize = True
            lblMovType.Margin = New Padding(0, 0, 8, 0)
            tlp.Controls.Add(lblMovType, 4, 0)

            Theme.StyleCombo(cboMovType)
            cboMovType.Width = 160
            cboMovType.Margin = New Padding(0, 6, 0, 6)
            tlp.Controls.Add(cboMovType, 5, 0)

            strip.Controls.Add(tlp)

            Dim card As New CardPanel()
            card.Dock = DockStyle.Fill
            card.Margin = New Padding(0)
            card.Padding = New Padding(14, 10, 14, 8)

            Dim body As New Panel()
            body.Dock = DockStyle.Fill
            body.BackColor = Theme.White
            body.Margin = New Padding(0)
            body.Padding = New Padding(0)

            GridStyler.Apply(dgvMovements)
            GridStyler.ApplyWeights(dgvMovements, 0.6, 1.4, 1.0, 1.8, 1.0, 0.8, 2.4, 1.2, 0.9)
            body.Controls.Add(dgvMovements)
            _lblMovementsEmpty = GridStyler.AttachEmptyOverlay(dgvMovements, "No movements match the current filters.")

            _lblMovementsFooter = GridStyler.NewFooter()
            _lblMovementsFooter.Height = 24
            _lblMovementsFooter.Dock = DockStyle.Bottom
            _lblMovementsFooter.BackColor = Theme.White

            card.Controls.Add(body)
            card.Controls.Add(_lblMovementsFooter)

            tabMovements.Controls.Add(card)
            tabMovements.Controls.Add(strip)

            Dim heading As PageHeading = PageHeading.Build(tabMovements, "Stock movements", _
                "Full audit trail of inbound, outbound and correction movements.")
            heading.AddChip(New StatusChip("Audit log", ThemeTone.Neutral))
        End Sub

        Private Sub UpdateMovementsFooter(total As Integer)
            If _lblMovementsFooter Is Nothing Then Return
            GridStyler.UpdateFooter(_lblMovementsFooter, total, total, If(total = 1, "movement", "movements"))
            If _lblMovementsEmpty IsNot Nothing Then _lblMovementsEmpty.Visible = (dgvMovements.RowCount = 0)
        End Sub

        ' ================= Catalog tab =================
        Private Sub RestyleCatalog()
            tabCatalog.BackColor = Theme.PageBg

            pnlCatalogTop.Visible = False
            tabCatalog.Controls.Remove(pnlCatalogTop)

            ' Read-only notice strip (existing lblNoticeReadOnly, info tint)
            lblNoticeReadOnly.AutoSize = False
            lblNoticeReadOnly.Height = 34
            lblNoticeReadOnly.Dock = DockStyle.Top
            lblNoticeReadOnly.Font = Theme.FontBody12
            lblNoticeReadOnly.ForeColor = Theme.Info
            lblNoticeReadOnly.BackColor = Theme.InfoTint
            lblNoticeReadOnly.TextAlign = ContentAlignment.MiddleLeft
            lblNoticeReadOnly.Margin = New Padding(0, 0, 0, 12)
            lblNoticeReadOnly.Padding = New Padding(16, 0, 0, 0)

            Dim card As New CardPanel()
            card.Dock = DockStyle.Fill
            card.Margin = New Padding(0)
            card.Padding = New Padding(14, 10, 14, 8)

            Dim body As New Panel()
            body.Dock = DockStyle.Fill
            body.BackColor = Theme.White
            body.Margin = New Padding(0)
            body.Padding = New Padding(0)

            GridStyler.Apply(dgvCatalog)
            GridStyler.ApplyWeights(dgvCatalog, 1.0, 2.4, 1.4, 1.0, 1.0, 0.8, 1.0, 1.2)
            body.Controls.Add(dgvCatalog)
            _lblCatalogEmpty = GridStyler.AttachEmptyOverlay(dgvCatalog, "No products match your search.")

            _lblCatalogFooter = GridStyler.NewFooter()
            _lblCatalogFooter.Height = 24
            _lblCatalogFooter.Dock = DockStyle.Bottom
            _lblCatalogFooter.BackColor = Theme.White

            card.Controls.Add(body)
            card.Controls.Add(_lblCatalogFooter)

            tabCatalog.Controls.Add(card)
            tabCatalog.Controls.Add(lblNoticeReadOnly)

            Dim heading As PageHeading = PageHeading.Build(tabCatalog, "Product catalog", _
                "Read-only reference of every active product, cost and price.")
            heading.AddChip(New StatusChip("Read-only", ThemeTone.Info))

            ' Search moves into the heading (existing txtCatalogSearch)
            Dim searchWrap As New InputPanel()
            searchWrap.Size = New Size(280, Theme.HeightButton)
            searchWrap.Margin = New Padding(0, 11, 12, 0)
            Theme.StyleInput(txtCatalogSearch)
            Theme.SetCue(txtCatalogSearch, "Search catalog")
            searchWrap.Controls.Add(txtCatalogSearch)
            AddHandler searchWrap.Resize,
                Sub(s As Object, e As EventArgs)
                    txtCatalogSearch.Location = New Point(12, Math.Max(0, (searchWrap.ClientSize.Height - txtCatalogSearch.Height) \ 2))
                    txtCatalogSearch.Width = searchWrap.ClientSize.Width - 24
                End Sub
            heading.AddInline(searchWrap)
        End Sub

        Private Sub UpdateCatalogFooter(total As Integer)
            If _lblCatalogFooter Is Nothing Then Return
            GridStyler.UpdateFooter(_lblCatalogFooter, total, total, If(total = 1, "product", "products"))
            If _lblCatalogEmpty IsNot Nothing Then _lblCatalogEmpty.Visible = (dgvCatalog.RowCount = 0)
        End Sub

        ' ================= Shared restyle helpers =================
        Private Function FormLabel(lbl As Label, text As String) As Label
            lbl.Text = text
            lbl.Font = Theme.FontBody12
            lbl.ForeColor = Theme.Muted
            lbl.AutoSize = True
            lbl.BackColor = Color.Transparent
            lbl.Margin = New Padding(0, 12, 0, 0)
            Return lbl
        End Function

        Private Function WrapTB(tb As TextBox, Optional cue As String = Nothing) As InputPanel
            Dim wrap As New InputPanel()
            wrap.Dock = DockStyle.Fill
            wrap.Margin = New Padding(0, 6, 0, 0)
            Theme.StyleInput(tb)
            If Not String.IsNullOrEmpty(cue) Then
                Theme.SetCue(tb, cue)
            End If
            wrap.Controls.Add(tb)
            AddHandler wrap.Resize,
                Sub(s As Object, e As EventArgs)
                    tb.Location = New Point(12, Math.Max(0, (wrap.ClientSize.Height - tb.Height) \ 2))
                    tb.Width = wrap.ClientSize.Width - 24
                End Sub
            Return wrap
        End Function

        Private Function WrapCombo(cbo As ComboBox) As InputPanel
            Dim wrap As New InputPanel()
            wrap.Dock = DockStyle.Fill
            wrap.Margin = New Padding(0, 6, 0, 0)
            Theme.StyleCombo(cbo)
            wrap.Controls.Add(cbo)
            AddHandler wrap.Resize,
                Sub(s As Object, e As EventArgs)
                    cbo.Location = New Point(10, Math.Max(0, (wrap.ClientSize.Height - cbo.Height) \ 2))
                    cbo.Width = Math.Max(60, wrap.ClientSize.Width - 20)
                End Sub
            Return wrap
        End Function

        Private Function WrapNum(n As NumericUpDown, width As Integer) As InputPanel
            Dim wrap As New InputPanel()
            wrap.Dock = DockStyle.Fill
            wrap.Margin = New Padding(0, 6, 0, 0)
            Theme.StyleNum(n)
            wrap.Controls.Add(n)
            AddHandler wrap.Resize,
                Sub(s As Object, e As EventArgs)
                    n.Location = New Point(12, Math.Max(0, (wrap.ClientSize.Height - n.Height) \ 2))
                    n.Width = width
                End Sub
            Return wrap
        End Function

        Private Function MsgRow(btn As Button, msg As Label) As Panel
            msg.Font = Theme.FontBody12
            msg.AutoSize = False
            msg.Dock = DockStyle.Fill
            msg.TextAlign = ContentAlignment.MiddleLeft
            msg.BackColor = Color.Transparent

            Dim row As New Panel()
            row.Dock = DockStyle.Fill
            row.Height = 40
            row.Margin = New Padding(0, 14, 0, 0)
            row.BackColor = Color.Transparent
            row.Controls.Add(msg)
            row.Controls.Add(btn)
            AddHandler row.Resize,
                Sub(s As Object, e As EventArgs)
                    btn.Location = New Point(0, 0)
                    msg.Location = New Point(btn.Width + 12, (row.ClientSize.Height - Math.Min(20, msg.Height)) \ 2)
                    msg.Size = New Size(Math.Max(60, row.ClientSize.Width - btn.Width - 16), row.ClientSize.Height)
                End Sub
            Return row
        End Function

#End Region
    End Class
End Namespace
