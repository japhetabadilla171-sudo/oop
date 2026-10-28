Namespace Forms
    <Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
    Partial Class ClerkDashboardForm
        Inherits System.Windows.Forms.Form

        <System.Diagnostics.DebuggerNonUserCode()>
        Protected Overrides Sub Dispose(ByVal disposing As Boolean)
            Try
                If disposing AndAlso components IsNot Nothing Then
                    components.Dispose()
                End If
            Finally
                MyBase.Dispose(disposing)
            End Try
        End Sub

        Private components As System.ComponentModel.IContainer

        <System.Diagnostics.DebuggerStepThrough()>
        Private Sub InitializeComponent()
            Dim dataGridViewCellStyle1 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
            Dim dataGridViewCellStyle2 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
            Dim dataGridViewCellStyle3 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
            Me.pnlHeader = New System.Windows.Forms.Panel()
            Me.btnLogout = New System.Windows.Forms.Button()
            Me.lblClerkInfo = New System.Windows.Forms.Label()
            Me.lblAppTitle = New System.Windows.Forms.Label()
            Me.tabClerk = New System.Windows.Forms.TabControl()
            Me.tabAlerts = New System.Windows.Forms.TabPage()
            Me.dgvAlerts = New System.Windows.Forms.DataGridView()
            Me.colAlertCode = New System.Windows.Forms.DataGridViewTextBoxColumn()
            Me.colAlertName = New System.Windows.Forms.DataGridViewTextBoxColumn()
            Me.colAlertCat = New System.Windows.Forms.DataGridViewTextBoxColumn()
            Me.colAlertStock = New System.Windows.Forms.DataGridViewTextBoxColumn()
            Me.colAlertReorder = New System.Windows.Forms.DataGridViewTextBoxColumn()
            Me.colAlertExpiry = New System.Windows.Forms.DataGridViewTextBoxColumn()
            Me.colAlertStatus = New System.Windows.Forms.DataGridViewTextBoxColumn()
            Me.pnlAlertTop = New System.Windows.Forms.Panel()
            Me.btnRefreshAlerts = New System.Windows.Forms.Button()
            Me.rbExpired = New System.Windows.Forms.RadioButton()
            Me.rbExpiring = New System.Windows.Forms.RadioButton()
            Me.rbLowStock = New System.Windows.Forms.RadioButton()
            Me.rbAllAlerts = New System.Windows.Forms.RadioButton()
            Me.lblKpiSummary = New System.Windows.Forms.Label()
            Me.tabStockIn = New System.Windows.Forms.TabPage()
            Me.grpStockIn = New System.Windows.Forms.GroupBox()
            Me.lblStockInMsg = New System.Windows.Forms.Label()
            Me.btnSubmitStockIn = New System.Windows.Forms.Button()
            Me.txtStockInNotes = New System.Windows.Forms.TextBox()
            Me.lblStockInNotes = New System.Windows.Forms.Label()
            Me.chkUpdateExpiry = New System.Windows.Forms.CheckBox()
            Me.dtpStockInExpiry = New System.Windows.Forms.DateTimePicker()
            Me.lblStockInExpiry = New System.Windows.Forms.Label()
            Me.numStockInQty = New System.Windows.Forms.NumericUpDown()
            Me.lblStockInQty = New System.Windows.Forms.Label()
            Me.cboStockInSupplier = New System.Windows.Forms.ComboBox()
            Me.lblStockInSupplier = New System.Windows.Forms.Label()
            Me.cboStockInProduct = New System.Windows.Forms.ComboBox()
            Me.lblStockInProduct = New System.Windows.Forms.Label()
            Me.tabAdjustment = New System.Windows.Forms.TabPage()
            Me.grpAdjustment = New System.Windows.Forms.GroupBox()
            Me.lblAdjMsg = New System.Windows.Forms.Label()
            Me.btnSubmitAdjustment = New System.Windows.Forms.Button()
            Me.txtAdjNotes = New System.Windows.Forms.TextBox()
            Me.lblAdjNotes = New System.Windows.Forms.Label()
            Me.cboAdjReason = New System.Windows.Forms.ComboBox()
            Me.lblAdjReason = New System.Windows.Forms.Label()
            Me.numAdjQty = New System.Windows.Forms.NumericUpDown()
            Me.lblAdjQty = New System.Windows.Forms.Label()
            Me.cboAdjType = New System.Windows.Forms.ComboBox()
            Me.lblAdjType = New System.Windows.Forms.Label()
            Me.lblAdjCurrentStock = New System.Windows.Forms.Label()
            Me.cboAdjProduct = New System.Windows.Forms.ComboBox()
            Me.lblAdjProduct = New System.Windows.Forms.Label()
            Me.tabMovements = New System.Windows.Forms.TabPage()
            Me.dgvMovements = New System.Windows.Forms.DataGridView()
            Me.colMovId = New System.Windows.Forms.DataGridViewTextBoxColumn()
            Me.colMovDate = New System.Windows.Forms.DataGridViewTextBoxColumn()
            Me.colMovCode = New System.Windows.Forms.DataGridViewTextBoxColumn()
            Me.colMovName = New System.Windows.Forms.DataGridViewTextBoxColumn()
            Me.colMovType = New System.Windows.Forms.DataGridViewTextBoxColumn()
            Me.colMovQty = New System.Windows.Forms.DataGridViewTextBoxColumn()
            Me.colMovReason = New System.Windows.Forms.DataGridViewTextBoxColumn()
            Me.colMovSupplier = New System.Windows.Forms.DataGridViewTextBoxColumn()
            Me.colMovUser = New System.Windows.Forms.DataGridViewTextBoxColumn()
            Me.pnlMovementsTop = New System.Windows.Forms.Panel()
            Me.cboMovType = New System.Windows.Forms.ComboBox()
            Me.lblMovType = New System.Windows.Forms.Label()
            Me.dtpMovEnd = New System.Windows.Forms.DateTimePicker()
            Me.lblMovEnd = New System.Windows.Forms.Label()
            Me.dtpMovStart = New System.Windows.Forms.DateTimePicker()
            Me.lblMovStart = New System.Windows.Forms.Label()
            Me.tabCatalog = New System.Windows.Forms.TabPage()
            Me.dgvCatalog = New System.Windows.Forms.DataGridView()
            Me.colCatCode = New System.Windows.Forms.DataGridViewTextBoxColumn()
            Me.colCatName = New System.Windows.Forms.DataGridViewTextBoxColumn()
            Me.colCatCategory = New System.Windows.Forms.DataGridViewTextBoxColumn()
            Me.colCatPrice = New System.Windows.Forms.DataGridViewTextBoxColumn()
            Me.colCatCost = New System.Windows.Forms.DataGridViewTextBoxColumn()
            Me.colCatStock = New System.Windows.Forms.DataGridViewTextBoxColumn()
            Me.colCatReorder = New System.Windows.Forms.DataGridViewTextBoxColumn()
            Me.colCatExpiry = New System.Windows.Forms.DataGridViewTextBoxColumn()
            Me.pnlCatalogTop = New System.Windows.Forms.Panel()
            Me.lblNoticeReadOnly = New System.Windows.Forms.Label()
            Me.txtCatalogSearch = New System.Windows.Forms.TextBox()
            Me.lblCatalogSearch = New System.Windows.Forms.Label()
            Me.pnlHeader.SuspendLayout()
            Me.tabClerk.SuspendLayout()
            Me.tabAlerts.SuspendLayout()
            CType(Me.dgvAlerts, System.ComponentModel.ISupportInitialize).BeginInit()
            Me.pnlAlertTop.SuspendLayout()
            Me.tabStockIn.SuspendLayout()
            Me.grpStockIn.SuspendLayout()
            CType(Me.numStockInQty, System.ComponentModel.ISupportInitialize).BeginInit()
            Me.tabAdjustment.SuspendLayout()
            Me.grpAdjustment.SuspendLayout()
            CType(Me.numAdjQty, System.ComponentModel.ISupportInitialize).BeginInit()
            Me.tabMovements.SuspendLayout()
            CType(Me.dgvMovements, System.ComponentModel.ISupportInitialize).BeginInit()
            Me.pnlMovementsTop.SuspendLayout()
            Me.tabCatalog.SuspendLayout()
            CType(Me.dgvCatalog, System.ComponentModel.ISupportInitialize).BeginInit()
            Me.pnlCatalogTop.SuspendLayout()
            Me.SuspendLayout()
            '
            'pnlHeader
            '
            Me.pnlHeader.BackColor = System.Drawing.Color.FromArgb(CType(CType(30, Byte), Integer), CType(CType(94, Byte), Integer), CType(CType(58, Byte), Integer))
            Me.pnlHeader.Controls.Add(Me.btnLogout)
            Me.pnlHeader.Controls.Add(Me.lblClerkInfo)
            Me.pnlHeader.Controls.Add(Me.lblAppTitle)
            Me.pnlHeader.Dock = System.Windows.Forms.DockStyle.Top
            Me.pnlHeader.Location = New System.Drawing.Point(0, 0)
            Me.pnlHeader.Name = "pnlHeader"
            Me.pnlHeader.Size = New System.Drawing.Size(1008, 65)
            Me.pnlHeader.TabIndex = 0
            '
            'btnLogout
            '
            Me.btnLogout.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.btnLogout.BackColor = System.Drawing.Color.FromArgb(CType(CType(198, Byte), Integer), CType(CType(40, Byte), Integer), CType(CType(40, Byte), Integer))
            Me.btnLogout.Cursor = System.Windows.Forms.Cursors.Hand
            Me.btnLogout.FlatAppearance.BorderSize = 0
            Me.btnLogout.FlatStyle = System.Windows.Forms.FlatStyle.Flat
            Me.btnLogout.Font = New System.Drawing.Font("Segoe UI Semibold", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.btnLogout.ForeColor = System.Drawing.Color.White
            Me.btnLogout.Location = New System.Drawing.Point(905, 16)
            Me.btnLogout.Name = "btnLogout"
            Me.btnLogout.Size = New System.Drawing.Size(90, 32)
            Me.btnLogout.TabIndex = 2
            Me.btnLogout.Text = "🚪 Logout"
            Me.btnLogout.UseVisualStyleBackColor = False
            '
            'lblClerkInfo
            '
            Me.lblClerkInfo.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.lblClerkInfo.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.lblClerkInfo.ForeColor = System.Drawing.Color.FromArgb(CType(CType(200, Byte), Integer), CType(CType(230, Byte), Integer), CType(CType(201, Byte), Integer))
            Me.lblClerkInfo.Location = New System.Drawing.Point(545, 22)
            Me.lblClerkInfo.Name = "lblClerkInfo"
            Me.lblClerkInfo.Size = New System.Drawing.Size(350, 20)
            Me.lblClerkInfo.TabIndex = 1
            Me.lblClerkInfo.Text = "Clerk: Pedro Penduko"
            Me.lblClerkInfo.TextAlign = System.Drawing.ContentAlignment.MiddleRight
            '
            'lblAppTitle
            '
            Me.lblAppTitle.AutoSize = True
            Me.lblAppTitle.Font = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.lblAppTitle.ForeColor = System.Drawing.Color.White
            Me.lblAppTitle.Location = New System.Drawing.Point(15, 20)
            Me.lblAppTitle.Name = "lblAppTitle"
            Me.lblAppTitle.Size = New System.Drawing.Size(357, 21)
            Me.lblAppTitle.TabIndex = 0
            Me.lblAppTitle.Text = "Mini-Grocery Store - Inventory Clerk Terminal"
            '
            'tabClerk
            '
            Me.tabClerk.Controls.Add(Me.tabAlerts)
            Me.tabClerk.Controls.Add(Me.tabStockIn)
            Me.tabClerk.Controls.Add(Me.tabAdjustment)
            Me.tabClerk.Controls.Add(Me.tabMovements)
            Me.tabClerk.Controls.Add(Me.tabCatalog)
            Me.tabClerk.Dock = System.Windows.Forms.DockStyle.Fill
            Me.tabClerk.Font = New System.Drawing.Font("Segoe UI", 9.5!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.tabClerk.ItemSize = New System.Drawing.Size(150, 30)
            Me.tabClerk.Location = New System.Drawing.Point(0, 65)
            Me.tabClerk.Name = "tabClerk"
            Me.tabClerk.SelectedIndex = 0
            Me.tabClerk.Size = New System.Drawing.Size(1008, 616)
            Me.tabClerk.TabIndex = 1
            '
            'tabAlerts
            '
            Me.tabAlerts.Controls.Add(Me.dgvAlerts)
            Me.tabAlerts.Controls.Add(Me.pnlAlertTop)
            Me.tabAlerts.Location = New System.Drawing.Point(4, 34)
            Me.tabAlerts.Name = "tabAlerts"
            Me.tabAlerts.Padding = New System.Windows.Forms.Padding(3)
            Me.tabAlerts.Size = New System.Drawing.Size(1000, 578)
            Me.tabAlerts.TabIndex = 0
            Me.tabAlerts.Text = "🚨 Inventory Alerts"
            Me.tabAlerts.UseVisualStyleBackColor = True
            '
            'dgvAlerts
            '
            Me.dgvAlerts.AllowUserToAddRows = False
            Me.dgvAlerts.AllowUserToDeleteRows = False
            Me.dgvAlerts.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill
            Me.dgvAlerts.BackgroundColor = System.Drawing.Color.White
            Me.dgvAlerts.BorderStyle = System.Windows.Forms.BorderStyle.None
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(CType(CType(30, Byte), Integer), CType(CType(94, Byte), Integer), CType(CType(58, Byte), Integer))
            dataGridViewCellStyle1.Font = New System.Drawing.Font("Segoe UI", 9.5!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.White
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
            Me.dgvAlerts.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1
            Me.dgvAlerts.ColumnHeadersHeight = 32
            Me.dgvAlerts.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.colAlertCode, Me.colAlertName, Me.colAlertCat, Me.colAlertStock, Me.colAlertReorder, Me.colAlertExpiry, Me.colAlertStatus})
            Me.dgvAlerts.Dock = System.Windows.Forms.DockStyle.Fill
            Me.dgvAlerts.EnableHeadersVisualStyles = False
            Me.dgvAlerts.Location = New System.Drawing.Point(3, 58)
            Me.dgvAlerts.MultiSelect = False
            Me.dgvAlerts.Name = "dgvAlerts"
            Me.dgvAlerts.ReadOnly = True
            Me.dgvAlerts.RowHeadersVisible = False
            Me.dgvAlerts.RowTemplate.Height = 26
            Me.dgvAlerts.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
            Me.dgvAlerts.Size = New System.Drawing.Size(994, 517)
            Me.dgvAlerts.TabIndex = 1
            '
            'colAlertCode
            '
            Me.colAlertCode.FillWeight = 18.0!
            Me.colAlertCode.HeaderText = "Item Code"
            Me.colAlertCode.Name = "colAlertCode"
            Me.colAlertCode.ReadOnly = True
            '
            'colAlertName
            '
            Me.colAlertName.FillWeight = 38.0!
            Me.colAlertName.HeaderText = "Product Name"
            Me.colAlertName.Name = "colAlertName"
            Me.colAlertName.ReadOnly = True
            '
            'colAlertCat
            '
            Me.colAlertCat.FillWeight = 22.0!
            Me.colAlertCat.HeaderText = "Category"
            Me.colAlertCat.Name = "colAlertCat"
            Me.colAlertCat.ReadOnly = True
            '
            'colAlertStock
            '
            Me.colAlertStock.FillWeight = 14.0!
            Me.colAlertStock.HeaderText = "Current Stock"
            Me.colAlertStock.Name = "colAlertStock"
            Me.colAlertStock.ReadOnly = True
            '
            'colAlertReorder
            '
            Me.colAlertReorder.FillWeight = 14.0!
            Me.colAlertReorder.HeaderText = "Reorder Pt."
            Me.colAlertReorder.Name = "colAlertReorder"
            Me.colAlertReorder.ReadOnly = True
            '
            'colAlertExpiry
            '
            Me.colAlertExpiry.FillWeight = 18.0!
            Me.colAlertExpiry.HeaderText = "Expiry Date"
            Me.colAlertExpiry.Name = "colAlertExpiry"
            Me.colAlertExpiry.ReadOnly = True
            '
            'colAlertStatus
            '
            Me.colAlertStatus.FillWeight = 20.0!
            Me.colAlertStatus.HeaderText = "Alert Type"
            Me.colAlertStatus.Name = "colAlertStatus"
            Me.colAlertStatus.ReadOnly = True
            '
            'pnlAlertTop
            '
            Me.pnlAlertTop.BackColor = System.Drawing.Color.FromArgb(CType(CType(245, Byte), Integer), CType(CType(248, Byte), Integer), CType(CType(245, Byte), Integer))
            Me.pnlAlertTop.Controls.Add(Me.btnRefreshAlerts)
            Me.pnlAlertTop.Controls.Add(Me.rbExpired)
            Me.pnlAlertTop.Controls.Add(Me.rbExpiring)
            Me.pnlAlertTop.Controls.Add(Me.rbLowStock)
            Me.pnlAlertTop.Controls.Add(Me.rbAllAlerts)
            Me.pnlAlertTop.Controls.Add(Me.lblKpiSummary)
            Me.pnlAlertTop.Dock = System.Windows.Forms.DockStyle.Top
            Me.pnlAlertTop.Location = New System.Drawing.Point(3, 3)
            Me.pnlAlertTop.Name = "pnlAlertTop"
            Me.pnlAlertTop.Size = New System.Drawing.Size(994, 55)
            Me.pnlAlertTop.TabIndex = 0
            '
            'btnRefreshAlerts
            '
            Me.btnRefreshAlerts.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.btnRefreshAlerts.BackColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer))
            Me.btnRefreshAlerts.Cursor = System.Windows.Forms.Cursors.Hand
            Me.btnRefreshAlerts.FlatAppearance.BorderSize = 0
            Me.btnRefreshAlerts.FlatStyle = System.Windows.Forms.FlatStyle.Flat
            Me.btnRefreshAlerts.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.btnRefreshAlerts.Location = New System.Drawing.Point(895, 12)
            Me.btnRefreshAlerts.Name = "btnRefreshAlerts"
            Me.btnRefreshAlerts.Size = New System.Drawing.Size(85, 30)
            Me.btnRefreshAlerts.TabIndex = 5
            Me.btnRefreshAlerts.Text = "🔄 Refresh"
            Me.btnRefreshAlerts.UseVisualStyleBackColor = False
            '
            'rbExpired
            '
            Me.rbExpired.AutoSize = True
            Me.rbExpired.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.rbExpired.ForeColor = System.Drawing.Color.Red
            Me.rbExpired.Location = New System.Drawing.Point(750, 18)
            Me.rbExpired.Name = "rbExpired"
            Me.rbExpired.Size = New System.Drawing.Size(64, 19)
            Me.rbExpired.TabIndex = 4
            Me.rbExpired.Text = "Expired"
            Me.rbExpired.UseVisualStyleBackColor = True
            '
            'rbExpiring
            '
            Me.rbExpiring.AutoSize = True
            Me.rbExpiring.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.rbExpiring.ForeColor = System.Drawing.Color.DarkGoldenrod
            Me.rbExpiring.Location = New System.Drawing.Point(590, 18)
            Me.rbExpiring.Name = "rbExpiring"
            Me.rbExpiring.Size = New System.Drawing.Size(147, 19)
            Me.rbExpiring.TabIndex = 3
            Me.rbExpiring.Text = "Expiring Soon (30 Days)"
            Me.rbExpiring.UseVisualStyleBackColor = True
            '
            'rbLowStock
            '
            Me.rbLowStock.AutoSize = True
            Me.rbLowStock.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.rbLowStock.ForeColor = System.Drawing.Color.DarkOrange
            Me.rbLowStock.Location = New System.Drawing.Point(475, 18)
            Me.rbLowStock.Name = "rbLowStock"
            Me.rbLowStock.Size = New System.Drawing.Size(103, 19)
            Me.rbLowStock.TabIndex = 2
            Me.rbLowStock.Text = "Low Stock Only"
            Me.rbLowStock.UseVisualStyleBackColor = True
            '
            'rbAllAlerts
            '
            Me.rbAllAlerts.AutoSize = True
            Me.rbAllAlerts.Checked = True
            Me.rbAllAlerts.Font = New System.Drawing.Font("Segoe UI Semibold", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.rbAllAlerts.Location = New System.Drawing.Point(365, 18)
            Me.rbAllAlerts.Name = "rbAllAlerts"
            Me.rbAllAlerts.Size = New System.Drawing.Size(97, 19)
            Me.rbAllAlerts.TabIndex = 1
            Me.rbAllAlerts.TabStop = True
            Me.rbAllAlerts.Text = "All Alert Items"
            Me.rbAllAlerts.UseVisualStyleBackColor = True
            '
            'lblKpiSummary
            '
            Me.lblKpiSummary.AutoSize = True
            Me.lblKpiSummary.Font = New System.Drawing.Font("Segoe UI Semibold", 9.5!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.lblKpiSummary.Location = New System.Drawing.Point(15, 18)
            Me.lblKpiSummary.Name = "lblKpiSummary"
            Me.lblKpiSummary.Size = New System.Drawing.Size(306, 17)
            Me.lblKpiSummary.TabIndex = 0
            Me.lblKpiSummary.Text = "Alerts: 0 Low Stock | 0 Expiring Soon | 0 Expired"
            '
            'tabStockIn
            '
            Me.tabStockIn.Controls.Add(Me.grpStockIn)
            Me.tabStockIn.Location = New System.Drawing.Point(4, 34)
            Me.tabStockIn.Name = "tabStockIn"
            Me.tabStockIn.Padding = New System.Windows.Forms.Padding(20)
            Me.tabStockIn.Size = New System.Drawing.Size(1000, 578)
            Me.tabStockIn.TabIndex = 1
            Me.tabStockIn.Text = "📥 Stock-In (Deliveries)"
            Me.tabStockIn.UseVisualStyleBackColor = True
            '
            'grpStockIn
            '
            Me.grpStockIn.Controls.Add(Me.lblStockInMsg)
            Me.grpStockIn.Controls.Add(Me.btnSubmitStockIn)
            Me.grpStockIn.Controls.Add(Me.txtStockInNotes)
            Me.grpStockIn.Controls.Add(Me.lblStockInNotes)
            Me.grpStockIn.Controls.Add(Me.chkUpdateExpiry)
            Me.grpStockIn.Controls.Add(Me.dtpStockInExpiry)
            Me.grpStockIn.Controls.Add(Me.lblStockInExpiry)
            Me.grpStockIn.Controls.Add(Me.numStockInQty)
            Me.grpStockIn.Controls.Add(Me.lblStockInQty)
            Me.grpStockIn.Controls.Add(Me.cboStockInSupplier)
            Me.grpStockIn.Controls.Add(Me.lblStockInSupplier)
            Me.grpStockIn.Controls.Add(Me.cboStockInProduct)
            Me.grpStockIn.Controls.Add(Me.lblStockInProduct)
            Me.grpStockIn.Font = New System.Drawing.Font("Segoe UI Semibold", 9.5!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.grpStockIn.Location = New System.Drawing.Point(23, 23)
            Me.grpStockIn.Name = "grpStockIn"
            Me.grpStockIn.Size = New System.Drawing.Size(650, 420)
            Me.grpStockIn.TabIndex = 0
            Me.grpStockIn.TabStop = False
            Me.grpStockIn.Text = "Record Inbound Supplier Delivery"
            '
            'lblStockInMsg
            '
            Me.lblStockInMsg.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.lblStockInMsg.Location = New System.Drawing.Point(30, 375)
            Me.lblStockInMsg.Name = "lblStockInMsg"
            Me.lblStockInMsg.Size = New System.Drawing.Size(585, 23)
            Me.lblStockInMsg.TabIndex = 12
            Me.lblStockInMsg.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
            '
            'btnSubmitStockIn
            '
            Me.btnSubmitStockIn.BackColor = System.Drawing.Color.FromArgb(CType(CType(46, Byte), Integer), CType(CType(125, Byte), Integer), CType(CType(50, Byte), Integer))
            Me.btnSubmitStockIn.Cursor = System.Windows.Forms.Cursors.Hand
            Me.btnSubmitStockIn.FlatAppearance.BorderSize = 0
            Me.btnSubmitStockIn.FlatStyle = System.Windows.Forms.FlatStyle.Flat
            Me.btnSubmitStockIn.ForeColor = System.Drawing.Color.White
            Me.btnSubmitStockIn.Location = New System.Drawing.Point(180, 325)
            Me.btnSubmitStockIn.Name = "btnSubmitStockIn"
            Me.btnSubmitStockIn.Size = New System.Drawing.Size(200, 38)
            Me.btnSubmitStockIn.TabIndex = 11
            Me.btnSubmitStockIn.Text = "📥 Record Stock-In"
            Me.btnSubmitStockIn.UseVisualStyleBackColor = False
            '
            'txtStockInNotes
            '
            Me.txtStockInNotes.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.txtStockInNotes.Location = New System.Drawing.Point(180, 245)
            Me.txtStockInNotes.Multiline = True
            Me.txtStockInNotes.Name = "txtStockInNotes"
            Me.txtStockInNotes.MaxLength = 100
            Me.txtStockInNotes.Size = New System.Drawing.Size(430, 60)
            Me.txtStockInNotes.TabIndex = 10
            '
            'lblStockInNotes
            '
            Me.lblStockInNotes.AutoSize = True
            Me.lblStockInNotes.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.lblStockInNotes.Location = New System.Drawing.Point(30, 248)
            Me.lblStockInNotes.Name = "lblStockInNotes"
            Me.lblStockInNotes.Size = New System.Drawing.Size(129, 15)
            Me.lblStockInNotes.TabIndex = 9
            Me.lblStockInNotes.Text = "Delivery Ref / Remarks:"
            '
            'chkUpdateExpiry
            '
            Me.chkUpdateExpiry.AutoSize = True
            Me.chkUpdateExpiry.Font = New System.Drawing.Font("Segoe UI", 8.5!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.chkUpdateExpiry.Location = New System.Drawing.Point(400, 198)
            Me.chkUpdateExpiry.Name = "chkUpdateExpiry"
            Me.chkUpdateExpiry.Size = New System.Drawing.Size(126, 19)
            Me.chkUpdateExpiry.TabIndex = 8
            Me.chkUpdateExpiry.Text = "Update Expiry Date"
            Me.chkUpdateExpiry.UseVisualStyleBackColor = True
            '
            'dtpStockInExpiry
            '
            Me.dtpStockInExpiry.Enabled = False
            Me.dtpStockInExpiry.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.dtpStockInExpiry.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
            Me.dtpStockInExpiry.Location = New System.Drawing.Point(180, 195)
            Me.dtpStockInExpiry.Name = "dtpStockInExpiry"
            Me.dtpStockInExpiry.Size = New System.Drawing.Size(200, 23)
            Me.dtpStockInExpiry.TabIndex = 7
            '
            'lblStockInExpiry
            '
            Me.lblStockInExpiry.AutoSize = True
            Me.lblStockInExpiry.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.lblStockInExpiry.Location = New System.Drawing.Point(30, 200)
            Me.lblStockInExpiry.Name = "lblStockInExpiry"
            Me.lblStockInExpiry.Size = New System.Drawing.Size(95, 15)
            Me.lblStockInExpiry.TabIndex = 6
            Me.lblStockInExpiry.Text = "Batch Expiry Date:"
            '
            'numStockInQty
            '
            Me.numStockInQty.Font = New System.Drawing.Font("Segoe UI", 9.5!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.numStockInQty.Location = New System.Drawing.Point(180, 145)
            Me.numStockInQty.Maximum = New Decimal(New Integer() {10000, 0, 0, 0})
            Me.numStockInQty.Minimum = New Decimal(New Integer() {1, 0, 0, 0})
            Me.numStockInQty.Name = "numStockInQty"
            Me.numStockInQty.Size = New System.Drawing.Size(120, 24)
            Me.numStockInQty.TabIndex = 5
            Me.numStockInQty.Value = New Decimal(New Integer() {10, 0, 0, 0})
            '
            'lblStockInQty
            '
            Me.lblStockInQty.AutoSize = True
            Me.lblStockInQty.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.lblStockInQty.Location = New System.Drawing.Point(30, 148)
            Me.lblStockInQty.Name = "lblStockInQty"
            Me.lblStockInQty.Size = New System.Drawing.Size(107, 15)
            Me.lblStockInQty.TabIndex = 4
            Me.lblStockInQty.Text = "Quantity Delivered:"
            '
            'cboStockInSupplier
            '
            Me.cboStockInSupplier.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
            Me.cboStockInSupplier.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.cboStockInSupplier.FormattingEnabled = True
            Me.cboStockInSupplier.Location = New System.Drawing.Point(180, 95)
            Me.cboStockInSupplier.Name = "cboStockInSupplier"
            Me.cboStockInSupplier.Size = New System.Drawing.Size(430, 23)
            Me.cboStockInSupplier.TabIndex = 3
            '
            'lblStockInSupplier
            '
            Me.lblStockInSupplier.AutoSize = True
            Me.lblStockInSupplier.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.lblStockInSupplier.Location = New System.Drawing.Point(30, 98)
            Me.lblStockInSupplier.Name = "lblStockInSupplier"
            Me.lblStockInSupplier.Size = New System.Drawing.Size(89, 15)
            Me.lblStockInSupplier.TabIndex = 2
            Me.lblStockInSupplier.Text = "Source Supplier:"
            '
            'cboStockInProduct
            '
            Me.cboStockInProduct.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
            Me.cboStockInProduct.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.cboStockInProduct.FormattingEnabled = True
            Me.cboStockInProduct.Location = New System.Drawing.Point(180, 45)
            Me.cboStockInProduct.Name = "cboStockInProduct"
            Me.cboStockInProduct.Size = New System.Drawing.Size(430, 23)
            Me.cboStockInProduct.TabIndex = 1
            '
            'lblStockInProduct
            '
            Me.lblStockInProduct.AutoSize = True
            Me.lblStockInProduct.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.lblStockInProduct.Location = New System.Drawing.Point(30, 48)
            Me.lblStockInProduct.Name = "lblStockInProduct"
            Me.lblStockInProduct.Size = New System.Drawing.Size(85, 15)
            Me.lblStockInProduct.TabIndex = 0
            Me.lblStockInProduct.Text = "Select Product:"
            '
            'tabAdjustment
            '
            Me.tabAdjustment.Controls.Add(Me.grpAdjustment)
            Me.tabAdjustment.Location = New System.Drawing.Point(4, 34)
            Me.tabAdjustment.Name = "tabAdjustment"
            Me.tabAdjustment.Padding = New System.Windows.Forms.Padding(20)
            Me.tabAdjustment.Size = New System.Drawing.Size(1000, 578)
            Me.tabAdjustment.TabIndex = 2
            Me.tabAdjustment.Text = "⚖ Stock Adjustment"
            Me.tabAdjustment.UseVisualStyleBackColor = True
            '
            'grpAdjustment
            '
            Me.grpAdjustment.Controls.Add(Me.lblAdjMsg)
            Me.grpAdjustment.Controls.Add(Me.btnSubmitAdjustment)
            Me.grpAdjustment.Controls.Add(Me.txtAdjNotes)
            Me.grpAdjustment.Controls.Add(Me.lblAdjNotes)
            Me.grpAdjustment.Controls.Add(Me.cboAdjReason)
            Me.grpAdjustment.Controls.Add(Me.lblAdjReason)
            Me.grpAdjustment.Controls.Add(Me.numAdjQty)
            Me.grpAdjustment.Controls.Add(Me.lblAdjQty)
            Me.grpAdjustment.Controls.Add(Me.cboAdjType)
            Me.grpAdjustment.Controls.Add(Me.lblAdjType)
            Me.grpAdjustment.Controls.Add(Me.lblAdjCurrentStock)
            Me.grpAdjustment.Controls.Add(Me.cboAdjProduct)
            Me.grpAdjustment.Controls.Add(Me.lblAdjProduct)
            Me.grpAdjustment.Font = New System.Drawing.Font("Segoe UI Semibold", 9.5!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.grpAdjustment.Location = New System.Drawing.Point(23, 23)
            Me.grpAdjustment.Name = "grpAdjustment"
            Me.grpAdjustment.Size = New System.Drawing.Size(650, 430)
            Me.grpAdjustment.TabIndex = 0
            Me.grpAdjustment.TabStop = False
            Me.grpAdjustment.Text = "Perform Physical Inventory Correction / Adjustment"
            '
            'lblAdjMsg
            '
            Me.lblAdjMsg.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.lblAdjMsg.Location = New System.Drawing.Point(30, 385)
            Me.lblAdjMsg.Name = "lblAdjMsg"
            Me.lblAdjMsg.Size = New System.Drawing.Size(585, 23)
            Me.lblAdjMsg.TabIndex = 12
            Me.lblAdjMsg.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
            '
            'btnSubmitAdjustment
            '
            Me.btnSubmitAdjustment.BackColor = System.Drawing.Color.FromArgb(CType(CType(239, Byte), Integer), CType(CType(108, Byte), Integer), CType(CType(0, Byte), Integer))
            Me.btnSubmitAdjustment.Cursor = System.Windows.Forms.Cursors.Hand
            Me.btnSubmitAdjustment.FlatAppearance.BorderSize = 0
            Me.btnSubmitAdjustment.FlatStyle = System.Windows.Forms.FlatStyle.Flat
            Me.btnSubmitAdjustment.ForeColor = System.Drawing.Color.White
            Me.btnSubmitAdjustment.Location = New System.Drawing.Point(180, 335)
            Me.btnSubmitAdjustment.Name = "btnSubmitAdjustment"
            Me.btnSubmitAdjustment.Size = New System.Drawing.Size(200, 38)
            Me.btnSubmitAdjustment.TabIndex = 11
            Me.btnSubmitAdjustment.Text = "⚖ Apply Adjustment"
            Me.btnSubmitAdjustment.UseVisualStyleBackColor = False
            '
            'txtAdjNotes
            '
            Me.txtAdjNotes.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.txtAdjNotes.Location = New System.Drawing.Point(180, 255)
            Me.txtAdjNotes.Multiline = True
            Me.txtAdjNotes.Name = "txtAdjNotes"
            Me.txtAdjNotes.MaxLength = 100
            Me.txtAdjNotes.Size = New System.Drawing.Size(430, 60)
            Me.txtAdjNotes.TabIndex = 10
            '
            'lblAdjNotes
            '
            Me.lblAdjNotes.AutoSize = True
            Me.lblAdjNotes.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.lblAdjNotes.Location = New System.Drawing.Point(30, 258)
            Me.lblAdjNotes.Name = "lblAdjNotes"
            Me.lblAdjNotes.Size = New System.Drawing.Size(107, 15)
            Me.lblAdjNotes.TabIndex = 9
            Me.lblAdjNotes.Text = "Additional Details:"
            '
            'cboAdjReason
            '
            Me.cboAdjReason.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
            Me.cboAdjReason.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.cboAdjReason.FormattingEnabled = True
            Me.cboAdjReason.Location = New System.Drawing.Point(180, 205)
            Me.cboAdjReason.Name = "cboAdjReason"
            Me.cboAdjReason.Size = New System.Drawing.Size(430, 23)
            Me.cboAdjReason.TabIndex = 8
            '
            'lblAdjReason
            '
            Me.lblAdjReason.AutoSize = True
            Me.lblAdjReason.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.lblAdjReason.Location = New System.Drawing.Point(30, 208)
            Me.lblAdjReason.Name = "lblAdjReason"
            Me.lblAdjReason.Size = New System.Drawing.Size(126, 15)
            Me.lblAdjReason.TabIndex = 7
            Me.lblAdjReason.Text = "Reason (Compulsory):"
            '
            'numAdjQty
            '
            Me.numAdjQty.Font = New System.Drawing.Font("Segoe UI", 9.5!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.numAdjQty.Location = New System.Drawing.Point(180, 155)
            Me.numAdjQty.Maximum = New Decimal(New Integer() {10000, 0, 0, 0})
            Me.numAdjQty.Minimum = New Decimal(New Integer() {1, 0, 0, 0})
            Me.numAdjQty.Name = "numAdjQty"
            Me.numAdjQty.Size = New System.Drawing.Size(120, 24)
            Me.numAdjQty.TabIndex = 6
            Me.numAdjQty.Value = New Decimal(New Integer() {1, 0, 0, 0})
            '
            'lblAdjQty
            '
            Me.lblAdjQty.AutoSize = True
            Me.lblAdjQty.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.lblAdjQty.Location = New System.Drawing.Point(30, 158)
            Me.lblAdjQty.Name = "lblAdjQty"
            Me.lblAdjQty.Size = New System.Drawing.Size(117, 15)
            Me.lblAdjQty.TabIndex = 5
            Me.lblAdjQty.Text = "Adjustment Amount:"
            '
            'cboAdjType
            '
            Me.cboAdjType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
            Me.cboAdjType.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.cboAdjType.FormattingEnabled = True
            Me.cboAdjType.Location = New System.Drawing.Point(180, 110)
            Me.cboAdjType.Name = "cboAdjType"
            Me.cboAdjType.Size = New System.Drawing.Size(250, 23)
            Me.cboAdjType.TabIndex = 4
            '
            'lblAdjType
            '
            Me.lblAdjType.AutoSize = True
            Me.lblAdjType.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.lblAdjType.Location = New System.Drawing.Point(30, 113)
            Me.lblAdjType.Name = "lblAdjType"
            Me.lblAdjType.Size = New System.Drawing.Size(102, 15)
            Me.lblAdjType.TabIndex = 3
            Me.lblAdjType.Text = "Adjustment Type:"
            '
            'lblAdjCurrentStock
            '
            Me.lblAdjCurrentStock.AutoSize = True
            Me.lblAdjCurrentStock.Font = New System.Drawing.Font("Segoe UI Semibold", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.lblAdjCurrentStock.ForeColor = System.Drawing.Color.DarkGreen
            Me.lblAdjCurrentStock.Location = New System.Drawing.Point(180, 78)
            Me.lblAdjCurrentStock.Name = "lblAdjCurrentStock"
            Me.lblAdjCurrentStock.Size = New System.Drawing.Size(127, 15)
            Me.lblAdjCurrentStock.TabIndex = 2
            Me.lblAdjCurrentStock.Text = "Current Stock: 0 units"
            '
            'cboAdjProduct
            '
            Me.cboAdjProduct.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
            Me.cboAdjProduct.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.cboAdjProduct.FormattingEnabled = True
            Me.cboAdjProduct.Location = New System.Drawing.Point(180, 45)
            Me.cboAdjProduct.Name = "cboAdjProduct"
            Me.cboAdjProduct.Size = New System.Drawing.Size(430, 23)
            Me.cboAdjProduct.TabIndex = 1
            '
            'lblAdjProduct
            '
            Me.lblAdjProduct.AutoSize = True
            Me.lblAdjProduct.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.lblAdjProduct.Location = New System.Drawing.Point(30, 48)
            Me.lblAdjProduct.Name = "lblAdjProduct"
            Me.lblAdjProduct.Size = New System.Drawing.Size(85, 15)
            Me.lblAdjProduct.TabIndex = 0
            Me.lblAdjProduct.Text = "Select Product:"
            '
            'tabMovements
            '
            Me.tabMovements.Controls.Add(Me.dgvMovements)
            Me.tabMovements.Controls.Add(Me.pnlMovementsTop)
            Me.tabMovements.Location = New System.Drawing.Point(4, 34)
            Me.tabMovements.Name = "tabMovements"
            Me.tabMovements.Padding = New System.Windows.Forms.Padding(3)
            Me.tabMovements.Size = New System.Drawing.Size(1000, 578)
            Me.tabMovements.TabIndex = 3
            Me.tabMovements.Text = "📜 Stock Movements Log"
            Me.tabMovements.UseVisualStyleBackColor = True
            '
            'dgvMovements
            '
            Me.dgvMovements.AllowUserToAddRows = False
            Me.dgvMovements.AllowUserToDeleteRows = False
            Me.dgvMovements.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill
            Me.dgvMovements.BackgroundColor = System.Drawing.Color.White
            Me.dgvMovements.BorderStyle = System.Windows.Forms.BorderStyle.None
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.FromArgb(CType(CType(30, Byte), Integer), CType(CType(94, Byte), Integer), CType(CType(58, Byte), Integer))
            dataGridViewCellStyle2.Font = New System.Drawing.Font("Segoe UI", 9.5!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.White
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
            Me.dgvMovements.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2
            Me.dgvMovements.ColumnHeadersHeight = 32
            Me.dgvMovements.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.colMovId, Me.colMovDate, Me.colMovCode, Me.colMovName, Me.colMovType, Me.colMovQty, Me.colMovReason, Me.colMovSupplier, Me.colMovUser})
            Me.dgvMovements.Dock = System.Windows.Forms.DockStyle.Fill
            Me.dgvMovements.EnableHeadersVisualStyles = False
            Me.dgvMovements.Location = New System.Drawing.Point(3, 58)
            Me.dgvMovements.MultiSelect = False
            Me.dgvMovements.Name = "dgvMovements"
            Me.dgvMovements.ReadOnly = True
            Me.dgvMovements.RowHeadersVisible = False
            Me.dgvMovements.RowTemplate.Height = 26
            Me.dgvMovements.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
            Me.dgvMovements.Size = New System.Drawing.Size(994, 517)
            Me.dgvMovements.TabIndex = 1
            '
            'colMovId
            '
            Me.colMovId.FillWeight = 10.0!
            Me.colMovId.HeaderText = "ID"
            Me.colMovId.Name = "colMovId"
            Me.colMovId.ReadOnly = True
            '
            'colMovDate
            '
            Me.colMovDate.FillWeight = 20.0!
            Me.colMovDate.HeaderText = "Date & Time"
            Me.colMovDate.Name = "colMovDate"
            Me.colMovDate.ReadOnly = True
            '
            'colMovCode
            '
            Me.colMovCode.FillWeight = 16.0!
            Me.colMovCode.HeaderText = "Code"
            Me.colMovCode.Name = "colMovCode"
            Me.colMovCode.ReadOnly = True
            '
            'colMovName
            '
            Me.colMovName.FillWeight = 32.0!
            Me.colMovName.HeaderText = "Product"
            Me.colMovName.Name = "colMovName"
            Me.colMovName.ReadOnly = True
            '
            'colMovType
            '
            Me.colMovType.FillWeight = 16.0!
            Me.colMovType.HeaderText = "Type"
            Me.colMovType.Name = "colMovType"
            Me.colMovType.ReadOnly = True
            '
            'colMovQty
            '
            Me.colMovQty.FillWeight = 14.0!
            Me.colMovQty.HeaderText = "Change"
            Me.colMovQty.Name = "colMovQty"
            Me.colMovQty.ReadOnly = True
            '
            'colMovReason
            '
            Me.colMovReason.FillWeight = 30.0!
            Me.colMovReason.HeaderText = "Reason / Ref"
            Me.colMovReason.Name = "colMovReason"
            Me.colMovReason.ReadOnly = True
            '
            'colMovSupplier
            '
            Me.colMovSupplier.FillWeight = 22.0!
            Me.colMovSupplier.HeaderText = "Supplier"
            Me.colMovSupplier.Name = "colMovSupplier"
            Me.colMovSupplier.ReadOnly = True
            '
            'colMovUser
            '
            Me.colMovUser.FillWeight = 20.0!
            Me.colMovUser.HeaderText = "Recorded By"
            Me.colMovUser.Name = "colMovUser"
            Me.colMovUser.ReadOnly = True
            '
            'pnlMovementsTop
            '
            Me.pnlMovementsTop.BackColor = System.Drawing.Color.FromArgb(CType(CType(245, Byte), Integer), CType(CType(248, Byte), Integer), CType(CType(245, Byte), Integer))
            Me.pnlMovementsTop.Controls.Add(Me.cboMovType)
            Me.pnlMovementsTop.Controls.Add(Me.lblMovType)
            Me.pnlMovementsTop.Controls.Add(Me.dtpMovEnd)
            Me.pnlMovementsTop.Controls.Add(Me.lblMovEnd)
            Me.pnlMovementsTop.Controls.Add(Me.dtpMovStart)
            Me.pnlMovementsTop.Controls.Add(Me.lblMovStart)
            Me.pnlMovementsTop.Dock = System.Windows.Forms.DockStyle.Top
            Me.pnlMovementsTop.Location = New System.Drawing.Point(3, 3)
            Me.pnlMovementsTop.Name = "pnlMovementsTop"
            Me.pnlMovementsTop.Size = New System.Drawing.Size(994, 55)
            Me.pnlMovementsTop.TabIndex = 0
            '
            'cboMovType
            '
            Me.cboMovType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
            Me.cboMovType.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.cboMovType.FormattingEnabled = True
            Me.cboMovType.Location = New System.Drawing.Point(580, 15)
            Me.cboMovType.Name = "cboMovType"
            Me.cboMovType.Size = New System.Drawing.Size(399, 23)
            Me.cboMovType.TabIndex = 5
            '
            'lblMovType
            '
            Me.lblMovType.AutoSize = True
            Me.lblMovType.Font = New System.Drawing.Font("Segoe UI Semibold", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.lblMovType.Location = New System.Drawing.Point(530, 19)
            Me.lblMovType.Name = "lblMovType"
            Me.lblMovType.Size = New System.Drawing.Size(35, 15)
            Me.lblMovType.TabIndex = 4
            Me.lblMovType.Text = "Type:"
            '
            'dtpMovEnd
            '
            Me.dtpMovEnd.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.dtpMovEnd.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
            Me.dtpMovEnd.Location = New System.Drawing.Point(320, 15)
            Me.dtpMovEnd.Name = "dtpMovEnd"
            Me.dtpMovEnd.Size = New System.Drawing.Size(180, 23)
            Me.dtpMovEnd.TabIndex = 3
            '
            'lblMovEnd
            '
            Me.lblMovEnd.AutoSize = True
            Me.lblMovEnd.Font = New System.Drawing.Font("Segoe UI Semibold", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.lblMovEnd.Location = New System.Drawing.Point(285, 19)
            Me.lblMovEnd.Name = "lblMovEnd"
            Me.lblMovEnd.Size = New System.Drawing.Size(23, 15)
            Me.lblMovEnd.TabIndex = 2
            Me.lblMovEnd.Text = "To:"
            '
            'dtpMovStart
            '
            Me.dtpMovStart.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.dtpMovStart.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
            Me.dtpMovStart.Location = New System.Drawing.Point(90, 15)
            Me.dtpMovStart.Name = "dtpMovStart"
            Me.dtpMovStart.Size = New System.Drawing.Size(180, 23)
            Me.dtpMovStart.TabIndex = 1
            '
            'lblMovStart
            '
            Me.lblMovStart.AutoSize = True
            Me.lblMovStart.Font = New System.Drawing.Font("Segoe UI Semibold", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.lblMovStart.Location = New System.Drawing.Point(15, 19)
            Me.lblMovStart.Name = "lblMovStart"
            Me.lblMovStart.Size = New System.Drawing.Size(63, 15)
            Me.lblMovStart.TabIndex = 0
            Me.lblMovStart.Text = "Date From:"
            '
            'tabCatalog
            '
            Me.tabCatalog.Controls.Add(Me.dgvCatalog)
            Me.tabCatalog.Controls.Add(Me.pnlCatalogTop)
            Me.tabCatalog.Location = New System.Drawing.Point(4, 34)
            Me.tabCatalog.Name = "tabCatalog"
            Me.tabCatalog.Padding = New System.Windows.Forms.Padding(3)
            Me.tabCatalog.Size = New System.Drawing.Size(1000, 578)
            Me.tabCatalog.TabIndex = 4
            Me.tabCatalog.Text = "📦 Product Catalog (Read-Only)"
            Me.tabCatalog.UseVisualStyleBackColor = True
            '
            'dgvCatalog
            '
            Me.dgvCatalog.AllowUserToAddRows = False
            Me.dgvCatalog.AllowUserToDeleteRows = False
            Me.dgvCatalog.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill
            Me.dgvCatalog.BackgroundColor = System.Drawing.Color.White
            Me.dgvCatalog.BorderStyle = System.Windows.Forms.BorderStyle.None
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
            dataGridViewCellStyle3.BackColor = System.Drawing.Color.FromArgb(CType(CType(30, Byte), Integer), CType(CType(94, Byte), Integer), CType(CType(58, Byte), Integer))
            dataGridViewCellStyle3.Font = New System.Drawing.Font("Segoe UI", 9.5!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            dataGridViewCellStyle3.ForeColor = System.Drawing.Color.White
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
            Me.dgvCatalog.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle3
            Me.dgvCatalog.ColumnHeadersHeight = 32
            Me.dgvCatalog.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.colCatCode, Me.colCatName, Me.colCatCategory, Me.colCatPrice, Me.colCatCost, Me.colCatStock, Me.colCatReorder, Me.colCatExpiry})
            Me.dgvCatalog.Dock = System.Windows.Forms.DockStyle.Fill
            Me.dgvCatalog.EnableHeadersVisualStyles = False
            Me.dgvCatalog.Location = New System.Drawing.Point(3, 53)
            Me.dgvCatalog.MultiSelect = False
            Me.dgvCatalog.Name = "dgvCatalog"
            Me.dgvCatalog.ReadOnly = True
            Me.dgvCatalog.RowHeadersVisible = False
            Me.dgvCatalog.RowTemplate.Height = 26
            Me.dgvCatalog.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
            Me.dgvCatalog.Size = New System.Drawing.Size(994, 522)
            Me.dgvCatalog.TabIndex = 1
            '
            'colCatCode
            '
            Me.colCatCode.FillWeight = 16.0!
            Me.colCatCode.HeaderText = "Code"
            Me.colCatCode.Name = "colCatCode"
            Me.colCatCode.ReadOnly = True
            '
            'colCatName
            '
            Me.colCatName.FillWeight = 36.0!
            Me.colCatName.HeaderText = "Product Name"
            Me.colCatName.Name = "colCatName"
            Me.colCatName.ReadOnly = True
            '
            'colCatCategory
            '
            Me.colCatCategory.FillWeight = 22.0!
            Me.colCatCategory.HeaderText = "Category"
            Me.colCatCategory.Name = "colCatCategory"
            Me.colCatCategory.ReadOnly = True
            '
            'colCatPrice
            '
            Me.colCatPrice.FillWeight = 16.0!
            Me.colCatPrice.HeaderText = "Selling Price"
            Me.colCatPrice.Name = "colCatPrice"
            Me.colCatPrice.ReadOnly = True
            '
            'colCatCost
            '
            Me.colCatCost.FillWeight = 16.0!
            Me.colCatCost.HeaderText = "Cost Price"
            Me.colCatCost.Name = "colCatCost"
            Me.colCatCost.ReadOnly = True
            '
            'colCatStock
            '
            Me.colCatStock.FillWeight = 14.0!
            Me.colCatStock.HeaderText = "Stock Qty"
            Me.colCatStock.Name = "colCatStock"
            Me.colCatStock.ReadOnly = True
            '
            'colCatReorder
            '
            Me.colCatReorder.FillWeight = 14.0!
            Me.colCatReorder.HeaderText = "Reorder Pt."
            Me.colCatReorder.Name = "colCatReorder"
            Me.colCatReorder.ReadOnly = True
            '
            'colCatExpiry
            '
            Me.colCatExpiry.FillWeight = 16.0!
            Me.colCatExpiry.HeaderText = "Expiry Date"
            Me.colCatExpiry.Name = "colCatExpiry"
            Me.colCatExpiry.ReadOnly = True
            '
            'pnlCatalogTop
            '
            Me.pnlCatalogTop.BackColor = System.Drawing.Color.FromArgb(CType(CType(245, Byte), Integer), CType(CType(248, Byte), Integer), CType(CType(245, Byte), Integer))
            Me.pnlCatalogTop.Controls.Add(Me.lblNoticeReadOnly)
            Me.pnlCatalogTop.Controls.Add(Me.txtCatalogSearch)
            Me.pnlCatalogTop.Controls.Add(Me.lblCatalogSearch)
            Me.pnlCatalogTop.Dock = System.Windows.Forms.DockStyle.Top
            Me.pnlCatalogTop.Location = New System.Drawing.Point(3, 3)
            Me.pnlCatalogTop.Name = "pnlCatalogTop"
            Me.pnlCatalogTop.Size = New System.Drawing.Size(994, 50)
            Me.pnlCatalogTop.TabIndex = 0
            '
            'lblNoticeReadOnly
            '
            Me.lblNoticeReadOnly.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.lblNoticeReadOnly.AutoSize = True
            Me.lblNoticeReadOnly.Font = New System.Drawing.Font("Segoe UI", 8.5!, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.lblNoticeReadOnly.ForeColor = System.Drawing.Color.DimGray
            Me.lblNoticeReadOnly.Location = New System.Drawing.Point(620, 18)
            Me.lblNoticeReadOnly.Name = "lblNoticeReadOnly"
            Me.lblNoticeReadOnly.Size = New System.Drawing.Size(358, 15)
            Me.lblNoticeReadOnly.TabIndex = 2
            Me.lblNoticeReadOnly.Text = "Notice: Inventory Clerks have read-only catalog access. Prices cannot be edited."
            '
            'txtCatalogSearch
            '
            Me.txtCatalogSearch.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.txtCatalogSearch.Location = New System.Drawing.Point(120, 14)
            Me.txtCatalogSearch.Name = "txtCatalogSearch"
            Me.txtCatalogSearch.MaxLength = 30
            Me.txtCatalogSearch.Size = New System.Drawing.Size(260, 23)
            Me.txtCatalogSearch.TabIndex = 1
            '
            'lblCatalogSearch
            '
            Me.lblCatalogSearch.AutoSize = True
            Me.lblCatalogSearch.Font = New System.Drawing.Font("Segoe UI Semibold", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.lblCatalogSearch.Location = New System.Drawing.Point(15, 18)
            Me.lblCatalogSearch.Name = "lblCatalogSearch"
            Me.lblCatalogSearch.Size = New System.Drawing.Size(90, 15)
            Me.lblCatalogSearch.TabIndex = 0
            Me.lblCatalogSearch.Text = "Search Catalog:"
            '
            'ClerkDashboardForm
            '
            Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 15.0!)
            Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
            Me.ClientSize = New System.Drawing.Size(1008, 681)
            Me.Controls.Add(Me.tabClerk)
            Me.Controls.Add(Me.pnlHeader)
            Me.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.MinimumSize = New System.Drawing.Size(950, 650)
            Me.Name = "ClerkDashboardForm"
            Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
            Me.Text = "Inventory Clerk Terminal - Mini-Grocery Store"
            Me.pnlHeader.ResumeLayout(False)
            Me.pnlHeader.PerformLayout()
            Me.tabClerk.ResumeLayout(False)
            Me.tabAlerts.ResumeLayout(False)
            CType(Me.dgvAlerts, System.ComponentModel.ISupportInitialize).EndInit()
            Me.pnlAlertTop.ResumeLayout(False)
            Me.pnlAlertTop.PerformLayout()
            Me.tabStockIn.ResumeLayout(False)
            Me.grpStockIn.ResumeLayout(False)
            Me.grpStockIn.PerformLayout()
            CType(Me.numStockInQty, System.ComponentModel.ISupportInitialize).EndInit()
            Me.tabAdjustment.ResumeLayout(False)
            Me.grpAdjustment.ResumeLayout(False)
            Me.grpAdjustment.PerformLayout()
            CType(Me.numAdjQty, System.ComponentModel.ISupportInitialize).EndInit()
            Me.tabMovements.ResumeLayout(False)
            CType(Me.dgvMovements, System.ComponentModel.ISupportInitialize).EndInit()
            Me.pnlMovementsTop.ResumeLayout(False)
            Me.pnlMovementsTop.PerformLayout()
            Me.tabCatalog.ResumeLayout(False)
            CType(Me.dgvCatalog, System.ComponentModel.ISupportInitialize).EndInit()
            Me.pnlCatalogTop.ResumeLayout(False)
            Me.pnlCatalogTop.PerformLayout()
            Me.ResumeLayout(False)

        End Sub

        Friend WithEvents pnlHeader As System.Windows.Forms.Panel
        Friend WithEvents lblAppTitle As System.Windows.Forms.Label
        Friend WithEvents lblClerkInfo As System.Windows.Forms.Label
        Friend WithEvents btnLogout As System.Windows.Forms.Button
        Friend WithEvents tabClerk As System.Windows.Forms.TabControl
        Friend WithEvents tabAlerts As System.Windows.Forms.TabPage
        Friend WithEvents pnlAlertTop As System.Windows.Forms.Panel
        Friend WithEvents lblKpiSummary As System.Windows.Forms.Label
        Friend WithEvents rbAllAlerts As System.Windows.Forms.RadioButton
        Friend WithEvents rbLowStock As System.Windows.Forms.RadioButton
        Friend WithEvents rbExpiring As System.Windows.Forms.RadioButton
        Friend WithEvents rbExpired As System.Windows.Forms.RadioButton
        Friend WithEvents btnRefreshAlerts As System.Windows.Forms.Button
        Friend WithEvents dgvAlerts As System.Windows.Forms.DataGridView
        Friend WithEvents colAlertCode As System.Windows.Forms.DataGridViewTextBoxColumn
        Friend WithEvents colAlertName As System.Windows.Forms.DataGridViewTextBoxColumn
        Friend WithEvents colAlertCat As System.Windows.Forms.DataGridViewTextBoxColumn
        Friend WithEvents colAlertStock As System.Windows.Forms.DataGridViewTextBoxColumn
        Friend WithEvents colAlertReorder As System.Windows.Forms.DataGridViewTextBoxColumn
        Friend WithEvents colAlertExpiry As System.Windows.Forms.DataGridViewTextBoxColumn
        Friend WithEvents colAlertStatus As System.Windows.Forms.DataGridViewTextBoxColumn
        Friend WithEvents tabStockIn As System.Windows.Forms.TabPage
        Friend WithEvents grpStockIn As System.Windows.Forms.GroupBox
        Friend WithEvents lblStockInProduct As System.Windows.Forms.Label
        Friend WithEvents cboStockInProduct As System.Windows.Forms.ComboBox
        Friend WithEvents lblStockInSupplier As System.Windows.Forms.Label
        Friend WithEvents cboStockInSupplier As System.Windows.Forms.ComboBox
        Friend WithEvents lblStockInQty As System.Windows.Forms.Label
        Friend WithEvents numStockInQty As System.Windows.Forms.NumericUpDown
        Friend WithEvents lblStockInExpiry As System.Windows.Forms.Label
        Friend WithEvents dtpStockInExpiry As System.Windows.Forms.DateTimePicker
        Friend WithEvents chkUpdateExpiry As System.Windows.Forms.CheckBox
        Friend WithEvents lblStockInNotes As System.Windows.Forms.Label
        Friend WithEvents txtStockInNotes As System.Windows.Forms.TextBox
        Friend WithEvents btnSubmitStockIn As System.Windows.Forms.Button
        Friend WithEvents lblStockInMsg As System.Windows.Forms.Label
        Friend WithEvents tabAdjustment As System.Windows.Forms.TabPage
        Friend WithEvents grpAdjustment As System.Windows.Forms.GroupBox
        Friend WithEvents lblAdjProduct As System.Windows.Forms.Label
        Friend WithEvents cboAdjProduct As System.Windows.Forms.ComboBox
        Friend WithEvents lblAdjCurrentStock As System.Windows.Forms.Label
        Friend WithEvents lblAdjType As System.Windows.Forms.Label
        Friend WithEvents cboAdjType As System.Windows.Forms.ComboBox
        Friend WithEvents lblAdjQty As System.Windows.Forms.Label
        Friend WithEvents numAdjQty As System.Windows.Forms.NumericUpDown
        Friend WithEvents lblAdjReason As System.Windows.Forms.Label
        Friend WithEvents cboAdjReason As System.Windows.Forms.ComboBox
        Friend WithEvents lblAdjNotes As System.Windows.Forms.Label
        Friend WithEvents txtAdjNotes As System.Windows.Forms.TextBox
        Friend WithEvents btnSubmitAdjustment As System.Windows.Forms.Button
        Friend WithEvents lblAdjMsg As System.Windows.Forms.Label
        Friend WithEvents tabMovements As System.Windows.Forms.TabPage
        Friend WithEvents pnlMovementsTop As System.Windows.Forms.Panel
        Friend WithEvents lblMovStart As System.Windows.Forms.Label
        Friend WithEvents dtpMovStart As System.Windows.Forms.DateTimePicker
        Friend WithEvents lblMovEnd As System.Windows.Forms.Label
        Friend WithEvents dtpMovEnd As System.Windows.Forms.DateTimePicker
        Friend WithEvents lblMovType As System.Windows.Forms.Label
        Friend WithEvents cboMovType As System.Windows.Forms.ComboBox
        Friend WithEvents dgvMovements As System.Windows.Forms.DataGridView
        Friend WithEvents colMovId As System.Windows.Forms.DataGridViewTextBoxColumn
        Friend WithEvents colMovDate As System.Windows.Forms.DataGridViewTextBoxColumn
        Friend WithEvents colMovCode As System.Windows.Forms.DataGridViewTextBoxColumn
        Friend WithEvents colMovName As System.Windows.Forms.DataGridViewTextBoxColumn
        Friend WithEvents colMovType As System.Windows.Forms.DataGridViewTextBoxColumn
        Friend WithEvents colMovQty As System.Windows.Forms.DataGridViewTextBoxColumn
        Friend WithEvents colMovReason As System.Windows.Forms.DataGridViewTextBoxColumn
        Friend WithEvents colMovSupplier As System.Windows.Forms.DataGridViewTextBoxColumn
        Friend WithEvents colMovUser As System.Windows.Forms.DataGridViewTextBoxColumn
        Friend WithEvents tabCatalog As System.Windows.Forms.TabPage
        Friend WithEvents pnlCatalogTop As System.Windows.Forms.Panel
        Friend WithEvents lblCatalogSearch As System.Windows.Forms.Label
        Friend WithEvents txtCatalogSearch As System.Windows.Forms.TextBox
        Friend WithEvents lblNoticeReadOnly As System.Windows.Forms.Label
        Friend WithEvents dgvCatalog As System.Windows.Forms.DataGridView
        Friend WithEvents colCatCode As System.Windows.Forms.DataGridViewTextBoxColumn
        Friend WithEvents colCatName As System.Windows.Forms.DataGridViewTextBoxColumn
        Friend WithEvents colCatCategory As System.Windows.Forms.DataGridViewTextBoxColumn
        Friend WithEvents colCatPrice As System.Windows.Forms.DataGridViewTextBoxColumn
        Friend WithEvents colCatCost As System.Windows.Forms.DataGridViewTextBoxColumn
        Friend WithEvents colCatStock As System.Windows.Forms.DataGridViewTextBoxColumn
        Friend WithEvents colCatReorder As System.Windows.Forms.DataGridViewTextBoxColumn
        Friend WithEvents colCatExpiry As System.Windows.Forms.DataGridViewTextBoxColumn
    End Class
End Namespace


