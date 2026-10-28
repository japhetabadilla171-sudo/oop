Namespace Forms
    <Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
    Partial Class OwnerDashboardForm
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
            Dim dataGridViewCellStyle4 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
            Dim dataGridViewCellStyle5 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
            Dim dataGridViewCellStyle6 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
            Dim dataGridViewCellStyle7 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
            Me.pnlHeader = New System.Windows.Forms.Panel()
            Me.btnLogout = New System.Windows.Forms.Button()
            Me.lblOwnerInfo = New System.Windows.Forms.Label()
            Me.lblAppTitle = New System.Windows.Forms.Label()
            Me.tabOwner = New System.Windows.Forms.TabControl()
            Me.tabOverview = New System.Windows.Forms.TabPage()
            Me.dgvOverviewRecent = New System.Windows.Forms.DataGridView()
            Me.colOvReceipt = New System.Windows.Forms.DataGridViewTextBoxColumn()
            Me.colOvTime = New System.Windows.Forms.DataGridViewTextBoxColumn()
            Me.colOvCashier = New System.Windows.Forms.DataGridViewTextBoxColumn()
            Me.colOvTotal = New System.Windows.Forms.DataGridViewTextBoxColumn()
            Me.pnlOverviewRecentTitle = New System.Windows.Forms.Panel()
            Me.lblRecentTitle = New System.Windows.Forms.Label()
            Me.pnlCards = New System.Windows.Forms.Panel()
            Me.pnlCard4 = New System.Windows.Forms.Panel()
            Me.lblCard4Val = New System.Windows.Forms.Label()
            Me.lblCard4Title = New System.Windows.Forms.Label()
            Me.pnlCard3 = New System.Windows.Forms.Panel()
            Me.lblCard3Val = New System.Windows.Forms.Label()
            Me.lblCard3Title = New System.Windows.Forms.Label()
            Me.pnlCard2 = New System.Windows.Forms.Panel()
            Me.lblCard2Val = New System.Windows.Forms.Label()
            Me.lblCard2Title = New System.Windows.Forms.Label()
            Me.pnlCard1 = New System.Windows.Forms.Panel()
            Me.lblCard1Val = New System.Windows.Forms.Label()
            Me.lblCard1Title = New System.Windows.Forms.Label()
            Me.tabStaff = New System.Windows.Forms.TabPage()
            Me.dgvStaff = New System.Windows.Forms.DataGridView()
            Me.colStaffId = New System.Windows.Forms.DataGridViewTextBoxColumn()
            Me.colStaffUser = New System.Windows.Forms.DataGridViewTextBoxColumn()
            Me.colStaffName = New System.Windows.Forms.DataGridViewTextBoxColumn()
            Me.colStaffRole = New System.Windows.Forms.DataGridViewTextBoxColumn()
            Me.colStaffStatus = New System.Windows.Forms.DataGridViewTextBoxColumn()
            Me.colStaffCreated = New System.Windows.Forms.DataGridViewTextBoxColumn()
            Me.grpStaffEdit = New System.Windows.Forms.GroupBox()
            Me.lblStaffMsg = New System.Windows.Forms.Label()
            Me.btnStaffToggleActive = New System.Windows.Forms.Button()
            Me.btnStaffResetPass = New System.Windows.Forms.Button()
            Me.btnStaffSave = New System.Windows.Forms.Button()
            Me.btnStaffNew = New System.Windows.Forms.Button()
            Me.txtStaffPassword = New System.Windows.Forms.TextBox()
            Me.lblStaffPass = New System.Windows.Forms.Label()
            Me.cboStaffRole = New System.Windows.Forms.ComboBox()
            Me.lblStaffRole = New System.Windows.Forms.Label()
            Me.txtStaffName = New System.Windows.Forms.TextBox()
            Me.lblStaffFullName = New System.Windows.Forms.Label()
            Me.txtStaffUsername = New System.Windows.Forms.TextBox()
            Me.lblStaffUsername = New System.Windows.Forms.Label()
            Me.tabProducts = New System.Windows.Forms.TabPage()
            Me.splitProducts = New System.Windows.Forms.SplitContainer()
            Me.dgvProducts = New System.Windows.Forms.DataGridView()
            Me.colPrCode = New System.Windows.Forms.DataGridViewTextBoxColumn()
            Me.colPrName = New System.Windows.Forms.DataGridViewTextBoxColumn()
            Me.colPrCat = New System.Windows.Forms.DataGridViewTextBoxColumn()
            Me.colPrPrice = New System.Windows.Forms.DataGridViewTextBoxColumn()
            Me.colPrCost = New System.Windows.Forms.DataGridViewTextBoxColumn()
            Me.colPrStock = New System.Windows.Forms.DataGridViewTextBoxColumn()
            Me.colPrStatus = New System.Windows.Forms.DataGridViewTextBoxColumn()
            Me.pnlProdSearch = New System.Windows.Forms.Panel()
            Me.cboProdCatFilter = New System.Windows.Forms.ComboBox()
            Me.txtProdSearch = New System.Windows.Forms.TextBox()
            Me.lblProdSearch = New System.Windows.Forms.Label()
            Me.grpProdDetail = New System.Windows.Forms.GroupBox()
            Me.lblProdMsg = New System.Windows.Forms.Label()
            Me.btnProdDelete = New System.Windows.Forms.Button()
            Me.btnProdSave = New System.Windows.Forms.Button()
            Me.btnProdNew = New System.Windows.Forms.Button()
            Me.chkProdActive = New System.Windows.Forms.CheckBox()
            Me.dtpProdExpiry = New System.Windows.Forms.DateTimePicker()
            Me.lblProdExpiry = New System.Windows.Forms.Label()
            Me.numProdReorder = New System.Windows.Forms.NumericUpDown()
            Me.lblProdReorder = New System.Windows.Forms.Label()
            Me.numProdStock = New System.Windows.Forms.NumericUpDown()
            Me.lblProdStock = New System.Windows.Forms.Label()
            Me.txtProdCost = New System.Windows.Forms.TextBox()
            Me.lblProdCost = New System.Windows.Forms.Label()
            Me.txtProdPrice = New System.Windows.Forms.TextBox()
            Me.lblProdPrice = New System.Windows.Forms.Label()
            Me.cboProdSupplier = New System.Windows.Forms.ComboBox()
            Me.lblProdSupplier = New System.Windows.Forms.Label()
            Me.cboProdCategory = New System.Windows.Forms.ComboBox()
            Me.lblProdCat = New System.Windows.Forms.Label()
            Me.txtProdName = New System.Windows.Forms.TextBox()
            Me.lblProdName = New System.Windows.Forms.Label()
            Me.txtProdCode = New System.Windows.Forms.TextBox()
            Me.lblProdCode = New System.Windows.Forms.Label()
            Me.tabMasterData = New System.Windows.Forms.TabPage()
            Me.splitMaster = New System.Windows.Forms.SplitContainer()
            Me.grpCategories = New System.Windows.Forms.GroupBox()
            Me.dgvCategories = New System.Windows.Forms.DataGridView()
            Me.colCatId = New System.Windows.Forms.DataGridViewTextBoxColumn()
            Me.colCatTitle = New System.Windows.Forms.DataGridViewTextBoxColumn()
            Me.pnlCatActions = New System.Windows.Forms.Panel()
            Me.btnCatDelete = New System.Windows.Forms.Button()
            Me.btnCatSave = New System.Windows.Forms.Button()
            Me.txtCatName = New System.Windows.Forms.TextBox()
            Me.lblCatName = New System.Windows.Forms.Label()
            Me.grpSuppliers = New System.Windows.Forms.GroupBox()
            Me.dgvSuppliers = New System.Windows.Forms.DataGridView()
            Me.colSupId = New System.Windows.Forms.DataGridViewTextBoxColumn()
            Me.colSupName = New System.Windows.Forms.DataGridViewTextBoxColumn()
            Me.colSupContact = New System.Windows.Forms.DataGridViewTextBoxColumn()
            Me.colSupAddr = New System.Windows.Forms.DataGridViewTextBoxColumn()
            Me.pnlSupActions = New System.Windows.Forms.Panel()
            Me.btnSupDelete = New System.Windows.Forms.Button()
            Me.btnSupSave = New System.Windows.Forms.Button()
            Me.btnSupNew = New System.Windows.Forms.Button()
            Me.txtSupAddr = New System.Windows.Forms.TextBox()
            Me.lblSupAddr = New System.Windows.Forms.Label()
            Me.txtSupContact = New System.Windows.Forms.TextBox()
            Me.lblSupContact = New System.Windows.Forms.Label()
            Me.txtSupName = New System.Windows.Forms.TextBox()
            Me.lblSupName = New System.Windows.Forms.Label()
            Me.tabTransactions = New System.Windows.Forms.TabPage()
            Me.dgvTransactions = New System.Windows.Forms.DataGridView()
            Me.colTxReceipt = New System.Windows.Forms.DataGridViewTextBoxColumn()
            Me.colTxDate = New System.Windows.Forms.DataGridViewTextBoxColumn()
            Me.colTxCashier = New System.Windows.Forms.DataGridViewTextBoxColumn()
            Me.colTxCustomer = New System.Windows.Forms.DataGridViewTextBoxColumn()
            Me.colTxSubtotal = New System.Windows.Forms.DataGridViewTextBoxColumn()
            Me.colTxDiscount = New System.Windows.Forms.DataGridViewTextBoxColumn()
            Me.colTxTotal = New System.Windows.Forms.DataGridViewTextBoxColumn()
            Me.pnlTxFilter = New System.Windows.Forms.Panel()
            Me.btnTxViewDetails = New System.Windows.Forms.Button()
            Me.dtpTxEnd = New System.Windows.Forms.DateTimePicker()
            Me.lblTxEnd = New System.Windows.Forms.Label()
            Me.dtpTxStart = New System.Windows.Forms.DateTimePicker()
            Me.lblTxStart = New System.Windows.Forms.Label()
            Me.tabReports = New System.Windows.Forms.TabPage()
            Me.dgvReport = New System.Windows.Forms.DataGridView()
            Me.pnlReportSummary = New System.Windows.Forms.Panel()
            Me.lblReportSummary = New System.Windows.Forms.Label()
            Me.pnlReportHeader = New System.Windows.Forms.Panel()
            Me.btnPrintReport = New System.Windows.Forms.Button()
            Me.btnExportCsv = New System.Windows.Forms.Button()
            Me.btnGenerateReport = New System.Windows.Forms.Button()
            Me.dtpReportDate = New System.Windows.Forms.DateTimePicker()
            Me.lblReportDate = New System.Windows.Forms.Label()
            Me.cboReportType = New System.Windows.Forms.ComboBox()
            Me.lblReportType = New System.Windows.Forms.Label()
            Me.pnlHeader.SuspendLayout()
            Me.tabOwner.SuspendLayout()
            Me.tabOverview.SuspendLayout()
            CType(Me.dgvOverviewRecent, System.ComponentModel.ISupportInitialize).BeginInit()
            Me.pnlOverviewRecentTitle.SuspendLayout()
            Me.pnlCards.SuspendLayout()
            Me.pnlCard4.SuspendLayout()
            Me.pnlCard3.SuspendLayout()
            Me.pnlCard2.SuspendLayout()
            Me.pnlCard1.SuspendLayout()
            Me.tabStaff.SuspendLayout()
            CType(Me.dgvStaff, System.ComponentModel.ISupportInitialize).BeginInit()
            Me.grpStaffEdit.SuspendLayout()
            Me.tabProducts.SuspendLayout()
            CType(Me.splitProducts, System.ComponentModel.ISupportInitialize).BeginInit()
            Me.splitProducts.Panel1.SuspendLayout()
            Me.splitProducts.Panel2.SuspendLayout()
            Me.splitProducts.SuspendLayout()
            CType(Me.dgvProducts, System.ComponentModel.ISupportInitialize).BeginInit()
            Me.pnlProdSearch.SuspendLayout()
            Me.grpProdDetail.SuspendLayout()
            CType(Me.numProdReorder, System.ComponentModel.ISupportInitialize).BeginInit()
            CType(Me.numProdStock, System.ComponentModel.ISupportInitialize).BeginInit()
            Me.tabMasterData.SuspendLayout()
            CType(Me.splitMaster, System.ComponentModel.ISupportInitialize).BeginInit()
            Me.splitMaster.Panel1.SuspendLayout()
            Me.splitMaster.Panel2.SuspendLayout()
            Me.splitMaster.SuspendLayout()
            Me.grpCategories.SuspendLayout()
            CType(Me.dgvCategories, System.ComponentModel.ISupportInitialize).BeginInit()
            Me.pnlCatActions.SuspendLayout()
            Me.grpSuppliers.SuspendLayout()
            CType(Me.dgvSuppliers, System.ComponentModel.ISupportInitialize).BeginInit()
            Me.pnlSupActions.SuspendLayout()
            Me.tabTransactions.SuspendLayout()
            CType(Me.dgvTransactions, System.ComponentModel.ISupportInitialize).BeginInit()
            Me.pnlTxFilter.SuspendLayout()
            Me.tabReports.SuspendLayout()
            CType(Me.dgvReport, System.ComponentModel.ISupportInitialize).BeginInit()
            Me.pnlReportSummary.SuspendLayout()
            Me.pnlReportHeader.SuspendLayout()
            Me.SuspendLayout()
            '
            'pnlHeader
            '
            Me.pnlHeader.BackColor = System.Drawing.Color.FromArgb(CType(CType(30, Byte), Integer), CType(CType(94, Byte), Integer), CType(CType(58, Byte), Integer))
            Me.pnlHeader.Controls.Add(Me.btnLogout)
            Me.pnlHeader.Controls.Add(Me.lblOwnerInfo)
            Me.pnlHeader.Controls.Add(Me.lblAppTitle)
            Me.pnlHeader.Dock = System.Windows.Forms.DockStyle.Top
            Me.pnlHeader.Location = New System.Drawing.Point(0, 0)
            Me.pnlHeader.Name = "pnlHeader"
            Me.pnlHeader.Size = New System.Drawing.Size(1084, 65)
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
            Me.btnLogout.Location = New System.Drawing.Point(980, 16)
            Me.btnLogout.Name = "btnLogout"
            Me.btnLogout.Size = New System.Drawing.Size(90, 32)
            Me.btnLogout.TabIndex = 2
            Me.btnLogout.Text = "🚪 Logout"
            Me.btnLogout.UseVisualStyleBackColor = False
            '
            'lblOwnerInfo
            '
            Me.lblOwnerInfo.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.lblOwnerInfo.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.lblOwnerInfo.ForeColor = System.Drawing.Color.FromArgb(CType(CType(200, Byte), Integer), CType(CType(230, Byte), Integer), CType(CType(201, Byte), Integer))
            Me.lblOwnerInfo.Location = New System.Drawing.Point(620, 22)
            Me.lblOwnerInfo.Name = "lblOwnerInfo"
            Me.lblOwnerInfo.Size = New System.Drawing.Size(350, 20)
            Me.lblOwnerInfo.TabIndex = 1
            Me.lblOwnerInfo.Text = "Administrator: Maria Santos"
            Me.lblOwnerInfo.TextAlign = System.Drawing.ContentAlignment.MiddleRight
            '
            'lblAppTitle
            '
            Me.lblAppTitle.AutoSize = True
            Me.lblAppTitle.Font = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.lblAppTitle.ForeColor = System.Drawing.Color.White
            Me.lblAppTitle.Location = New System.Drawing.Point(15, 20)
            Me.lblAppTitle.Name = "lblAppTitle"
            Me.lblAppTitle.Size = New System.Drawing.Size(387, 21)
            Me.lblAppTitle.TabIndex = 0
            Me.lblAppTitle.Text = "Mini-Grocery Store - Store Owner Administration"
            '
            'tabOwner
            '
            Me.tabOwner.Controls.Add(Me.tabOverview)
            Me.tabOwner.Controls.Add(Me.tabStaff)
            Me.tabOwner.Controls.Add(Me.tabProducts)
            Me.tabOwner.Controls.Add(Me.tabMasterData)
            Me.tabOwner.Controls.Add(Me.tabTransactions)
            Me.tabOwner.Controls.Add(Me.tabReports)
            Me.tabOwner.Dock = System.Windows.Forms.DockStyle.Fill
            Me.tabOwner.Font = New System.Drawing.Font("Segoe UI", 9.5!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.tabOwner.ItemSize = New System.Drawing.Size(150, 30)
            Me.tabOwner.Location = New System.Drawing.Point(0, 65)
            Me.tabOwner.Name = "tabOwner"
            Me.tabOwner.SelectedIndex = 0
            Me.tabOwner.Size = New System.Drawing.Size(1084, 646)
            Me.tabOwner.TabIndex = 1
            '
            'tabOverview
            '
            Me.tabOverview.Controls.Add(Me.dgvOverviewRecent)
            Me.tabOverview.Controls.Add(Me.pnlOverviewRecentTitle)
            Me.tabOverview.Controls.Add(Me.pnlCards)
            Me.tabOverview.Location = New System.Drawing.Point(4, 34)
            Me.tabOverview.Name = "tabOverview"
            Me.tabOverview.Padding = New System.Windows.Forms.Padding(15)
            Me.tabOverview.Size = New System.Drawing.Size(1076, 608)
            Me.tabOverview.TabIndex = 0
            Me.tabOverview.Text = "📈 Dashboard Overview"
            Me.tabOverview.UseVisualStyleBackColor = True
            '
            'dgvOverviewRecent
            '
            Me.dgvOverviewRecent.AllowUserToAddRows = False
            Me.dgvOverviewRecent.AllowUserToDeleteRows = False
            Me.dgvOverviewRecent.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill
            Me.dgvOverviewRecent.BackgroundColor = System.Drawing.Color.White
            Me.dgvOverviewRecent.BorderStyle = System.Windows.Forms.BorderStyle.None
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(CType(CType(30, Byte), Integer), CType(CType(94, Byte), Integer), CType(CType(58, Byte), Integer))
            dataGridViewCellStyle1.Font = New System.Drawing.Font("Segoe UI", 9.5!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.White
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
            Me.dgvOverviewRecent.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1
            Me.dgvOverviewRecent.ColumnHeadersHeight = 30
            Me.dgvOverviewRecent.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.colOvReceipt, Me.colOvTime, Me.colOvCashier, Me.colOvTotal})
            Me.dgvOverviewRecent.Dock = System.Windows.Forms.DockStyle.Fill
            Me.dgvOverviewRecent.EnableHeadersVisualStyles = False
            Me.dgvOverviewRecent.Location = New System.Drawing.Point(15, 175)
            Me.dgvOverviewRecent.MultiSelect = False
            Me.dgvOverviewRecent.Name = "dgvOverviewRecent"
            Me.dgvOverviewRecent.ReadOnly = True
            Me.dgvOverviewRecent.RowHeadersVisible = False
            Me.dgvOverviewRecent.RowTemplate.Height = 26
            Me.dgvOverviewRecent.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
            Me.dgvOverviewRecent.Size = New System.Drawing.Size(1046, 418)
            Me.dgvOverviewRecent.TabIndex = 2
            '
            'colOvReceipt
            '
            Me.colOvReceipt.FillWeight = 25.0!
            Me.colOvReceipt.HeaderText = "Receipt No"
            Me.colOvReceipt.Name = "colOvReceipt"
            Me.colOvReceipt.ReadOnly = True
            '
            'colOvTime
            '
            Me.colOvTime.FillWeight = 25.0!
            Me.colOvTime.HeaderText = "Time"
            Me.colOvTime.Name = "colOvTime"
            Me.colOvTime.ReadOnly = True
            '
            'colOvCashier
            '
            Me.colOvCashier.FillWeight = 30.0!
            Me.colOvCashier.HeaderText = "Cashier"
            Me.colOvCashier.Name = "colOvCashier"
            Me.colOvCashier.ReadOnly = True
            '
            'colOvTotal
            '
            Me.colOvTotal.FillWeight = 20.0!
            Me.colOvTotal.HeaderText = "Total (PHP)"
            Me.colOvTotal.Name = "colOvTotal"
            Me.colOvTotal.ReadOnly = True
            '
            'pnlOverviewRecentTitle
            '
            Me.pnlOverviewRecentTitle.Controls.Add(Me.lblRecentTitle)
            Me.pnlOverviewRecentTitle.Dock = System.Windows.Forms.DockStyle.Top
            Me.pnlOverviewRecentTitle.Location = New System.Drawing.Point(15, 135)
            Me.pnlOverviewRecentTitle.Name = "pnlOverviewRecentTitle"
            Me.pnlOverviewRecentTitle.Size = New System.Drawing.Size(1046, 40)
            Me.pnlOverviewRecentTitle.TabIndex = 1
            '
            'lblRecentTitle
            '
            Me.lblRecentTitle.AutoSize = True
            Me.lblRecentTitle.Font = New System.Drawing.Font("Segoe UI Semibold", 10.5!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.lblRecentTitle.Location = New System.Drawing.Point(5, 12)
            Me.lblRecentTitle.Name = "lblRecentTitle"
            Me.lblRecentTitle.Size = New System.Drawing.Size(206, 19)
            Me.lblRecentTitle.TabIndex = 0
            Me.lblRecentTitle.Text = "Recent Transactions Completed:"
            '
            'pnlCards
            '
            Me.pnlCards.Controls.Add(Me.pnlCard4)
            Me.pnlCards.Controls.Add(Me.pnlCard3)
            Me.pnlCards.Controls.Add(Me.pnlCard2)
            Me.pnlCards.Controls.Add(Me.pnlCard1)
            Me.pnlCards.Dock = System.Windows.Forms.DockStyle.Top
            Me.pnlCards.Location = New System.Drawing.Point(15, 15)
            Me.pnlCards.Name = "pnlCards"
            Me.pnlCards.Size = New System.Drawing.Size(1046, 120)
            Me.pnlCards.TabIndex = 0
            '
            'pnlCard4
            '
            Me.pnlCard4.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(243, Byte), Integer), CType(CType(224, Byte), Integer))
            Me.pnlCard4.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
            Me.pnlCard4.Controls.Add(Me.lblCard4Val)
            Me.pnlCard4.Controls.Add(Me.lblCard4Title)
            Me.pnlCard4.Location = New System.Drawing.Point(780, 10)
            Me.pnlCard4.Name = "pnlCard4"
            Me.pnlCard4.Size = New System.Drawing.Size(240, 100)
            Me.pnlCard4.TabIndex = 3
            '
            'lblCard4Val
            '
            Me.lblCard4Val.Font = New System.Drawing.Font("Segoe UI", 16.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.lblCard4Val.ForeColor = System.Drawing.Color.FromArgb(CType(CType(230, Byte), Integer), CType(CType(81, Byte), Integer), CType(CType(0, Byte), Integer))
            Me.lblCard4Val.Location = New System.Drawing.Point(10, 42)
            Me.lblCard4Val.Name = "lblCard4Val"
            Me.lblCard4Val.Size = New System.Drawing.Size(218, 40)
            Me.lblCard4Val.TabIndex = 1
            Me.lblCard4Val.Text = "0 items"
            Me.lblCard4Val.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
            '
            'lblCard4Title
            '
            Me.lblCard4Title.Font = New System.Drawing.Font("Segoe UI Semibold", 9.5!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.lblCard4Title.ForeColor = System.Drawing.Color.FromArgb(CType(CType(100, Byte), Integer), CType(CType(50, Byte), Integer), CType(CType(0, Byte), Integer))
            Me.lblCard4Title.Location = New System.Drawing.Point(10, 12)
            Me.lblCard4Title.Name = "lblCard4Title"
            Me.lblCard4Title.Size = New System.Drawing.Size(218, 22)
            Me.lblCard4Title.TabIndex = 0
            Me.lblCard4Title.Text = "Expiring Soon (30 Days)"
            Me.lblCard4Title.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
            '
            'pnlCard3
            '
            Me.pnlCard3.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(235, Byte), Integer), CType(CType(238, Byte), Integer))
            Me.pnlCard3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
            Me.pnlCard3.Controls.Add(Me.lblCard3Val)
            Me.pnlCard3.Controls.Add(Me.lblCard3Title)
            Me.pnlCard3.Location = New System.Drawing.Point(525, 10)
            Me.pnlCard3.Name = "pnlCard3"
            Me.pnlCard3.Size = New System.Drawing.Size(240, 100)
            Me.pnlCard3.TabIndex = 2
            '
            'lblCard3Val
            '
            Me.lblCard3Val.Font = New System.Drawing.Font("Segoe UI", 16.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.lblCard3Val.ForeColor = System.Drawing.Color.FromArgb(CType(CType(198, Byte), Integer), CType(CType(40, Byte), Integer), CType(CType(40, Byte), Integer))
            Me.lblCard3Val.Location = New System.Drawing.Point(10, 42)
            Me.lblCard3Val.Name = "lblCard3Val"
            Me.lblCard3Val.Size = New System.Drawing.Size(218, 40)
            Me.lblCard3Val.TabIndex = 1
            Me.lblCard3Val.Text = "0 items"
            Me.lblCard3Val.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
            '
            'lblCard3Title
            '
            Me.lblCard3Title.Font = New System.Drawing.Font("Segoe UI Semibold", 9.5!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.lblCard3Title.ForeColor = System.Drawing.Color.FromArgb(CType(CType(100, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
            Me.lblCard3Title.Location = New System.Drawing.Point(10, 12)
            Me.lblCard3Title.Name = "lblCard3Title"
            Me.lblCard3Title.Size = New System.Drawing.Size(218, 22)
            Me.lblCard3Title.TabIndex = 0
            Me.lblCard3Title.Text = "Low Stock Alerts"
            Me.lblCard3Title.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
            '
            'pnlCard2
            '
            Me.pnlCard2.BackColor = System.Drawing.Color.FromArgb(CType(CType(227, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(253, Byte), Integer))
            Me.pnlCard2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
            Me.pnlCard2.Controls.Add(Me.lblCard2Val)
            Me.pnlCard2.Controls.Add(Me.lblCard2Title)
            Me.pnlCard2.Location = New System.Drawing.Point(270, 10)
            Me.pnlCard2.Name = "pnlCard2"
            Me.pnlCard2.Size = New System.Drawing.Size(240, 100)
            Me.pnlCard2.TabIndex = 1
            '
            'lblCard2Val
            '
            Me.lblCard2Val.Font = New System.Drawing.Font("Segoe UI", 16.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.lblCard2Val.ForeColor = System.Drawing.Color.FromArgb(CType(CType(21, Byte), Integer), CType(CType(101, Byte), Integer), CType(CType(192, Byte), Integer))
            Me.lblCard2Val.Location = New System.Drawing.Point(10, 42)
            Me.lblCard2Val.Name = "lblCard2Val"
            Me.lblCard2Val.Size = New System.Drawing.Size(218, 40)
            Me.lblCard2Val.TabIndex = 1
            Me.lblCard2Val.Text = "0"
            Me.lblCard2Val.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
            '
            'lblCard2Title
            '
            Me.lblCard2Title.Font = New System.Drawing.Font("Segoe UI Semibold", 9.5!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.lblCard2Title.ForeColor = System.Drawing.Color.FromArgb(CType(CType(10, Byte), Integer), CType(CType(50, Byte), Integer), CType(CType(100, Byte), Integer))
            Me.lblCard2Title.Location = New System.Drawing.Point(10, 12)
            Me.lblCard2Title.Name = "lblCard2Title"
            Me.lblCard2Title.Size = New System.Drawing.Size(218, 22)
            Me.lblCard2Title.TabIndex = 0
            Me.lblCard2Title.Text = "Today's Transactions"
            Me.lblCard2Title.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
            '
            'pnlCard1
            '
            Me.pnlCard1.BackColor = System.Drawing.Color.FromArgb(CType(CType(232, Byte), Integer), CType(CType(245, Byte), Integer), CType(CType(233, Byte), Integer))
            Me.pnlCard1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
            Me.pnlCard1.Controls.Add(Me.lblCard1Val)
            Me.pnlCard1.Controls.Add(Me.lblCard1Title)
            Me.pnlCard1.Location = New System.Drawing.Point(15, 10)
            Me.pnlCard1.Name = "pnlCard1"
            Me.pnlCard1.Size = New System.Drawing.Size(240, 100)
            Me.pnlCard1.TabIndex = 0
            '
            'lblCard1Val
            '
            Me.lblCard1Val.Font = New System.Drawing.Font("Segoe UI", 16.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.lblCard1Val.ForeColor = System.Drawing.Color.FromArgb(CType(CType(46, Byte), Integer), CType(CType(125, Byte), Integer), CType(CType(50, Byte), Integer))
            Me.lblCard1Val.Location = New System.Drawing.Point(10, 42)
            Me.lblCard1Val.Name = "lblCard1Val"
            Me.lblCard1Val.Size = New System.Drawing.Size(218, 40)
            Me.lblCard1Val.TabIndex = 1
            Me.lblCard1Val.Text = "PHP 0.00"
            Me.lblCard1Val.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
            '
            'lblCard1Title
            '
            Me.lblCard1Title.Font = New System.Drawing.Font("Segoe UI Semibold", 9.5!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.lblCard1Title.ForeColor = System.Drawing.Color.FromArgb(CType(CType(30, Byte), Integer), CType(CType(94, Byte), Integer), CType(CType(58, Byte), Integer))
            Me.lblCard1Title.Location = New System.Drawing.Point(10, 12)
            Me.lblCard1Title.Name = "lblCard1Title"
            Me.lblCard1Title.Size = New System.Drawing.Size(218, 22)
            Me.lblCard1Title.TabIndex = 0
            Me.lblCard1Title.Text = "Today's Total Sales"
            Me.lblCard1Title.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
            '
            'tabStaff
            '
            Me.tabStaff.Controls.Add(Me.dgvStaff)
            Me.tabStaff.Controls.Add(Me.grpStaffEdit)
            Me.tabStaff.Location = New System.Drawing.Point(4, 34)
            Me.tabStaff.Name = "tabStaff"
            Me.tabStaff.Padding = New System.Windows.Forms.Padding(10)
            Me.tabStaff.Size = New System.Drawing.Size(1076, 608)
            Me.tabStaff.TabIndex = 1
            Me.tabStaff.Text = "👥 Staff Management"
            Me.tabStaff.UseVisualStyleBackColor = True
            '
            'dgvStaff
            '
            Me.dgvStaff.AllowUserToAddRows = False
            Me.dgvStaff.AllowUserToDeleteRows = False
            Me.dgvStaff.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill
            Me.dgvStaff.BackgroundColor = System.Drawing.Color.White
            Me.dgvStaff.BorderStyle = System.Windows.Forms.BorderStyle.None
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.FromArgb(CType(CType(30, Byte), Integer), CType(CType(94, Byte), Integer), CType(CType(58, Byte), Integer))
            dataGridViewCellStyle2.Font = New System.Drawing.Font("Segoe UI", 9.5!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.White
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
            Me.dgvStaff.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2
            Me.dgvStaff.ColumnHeadersHeight = 32
            Me.dgvStaff.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.colStaffId, Me.colStaffUser, Me.colStaffName, Me.colStaffRole, Me.colStaffStatus, Me.colStaffCreated})
            Me.dgvStaff.Dock = System.Windows.Forms.DockStyle.Fill
            Me.dgvStaff.EnableHeadersVisualStyles = False
            Me.dgvStaff.Location = New System.Drawing.Point(10, 10)
            Me.dgvStaff.MultiSelect = False
            Me.dgvStaff.Name = "dgvStaff"
            Me.dgvStaff.ReadOnly = True
            Me.dgvStaff.RowHeadersVisible = False
            Me.dgvStaff.RowTemplate.Height = 26
            Me.dgvStaff.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
            Me.dgvStaff.Size = New System.Drawing.Size(1056, 388)
            Me.dgvStaff.TabIndex = 0
            '
            'colStaffId
            '
            Me.colStaffId.FillWeight = 12.0!
            Me.colStaffId.HeaderText = "ID"
            Me.colStaffId.Name = "colStaffId"
            Me.colStaffId.ReadOnly = True
            '
            'colStaffUser
            '
            Me.colStaffUser.FillWeight = 22.0!
            Me.colStaffUser.HeaderText = "Username"
            Me.colStaffUser.Name = "colStaffUser"
            Me.colStaffUser.ReadOnly = True
            '
            'colStaffName
            '
            Me.colStaffName.FillWeight = 32.0!
            Me.colStaffName.HeaderText = "Full Name"
            Me.colStaffName.Name = "colStaffName"
            Me.colStaffName.ReadOnly = True
            '
            'colStaffRole
            '
            Me.colStaffRole.FillWeight = 24.0!
            Me.colStaffRole.HeaderText = "Role"
            Me.colStaffRole.Name = "colStaffRole"
            Me.colStaffRole.ReadOnly = True
            '
            'colStaffStatus
            '
            Me.colStaffStatus.FillWeight = 18.0!
            Me.colStaffStatus.HeaderText = "Status"
            Me.colStaffStatus.Name = "colStaffStatus"
            Me.colStaffStatus.ReadOnly = True
            '
            'colStaffCreated
            '
            Me.colStaffCreated.FillWeight = 22.0!
            Me.colStaffCreated.HeaderText = "Created Date"
            Me.colStaffCreated.Name = "colStaffCreated"
            Me.colStaffCreated.ReadOnly = True
            '
            'grpStaffEdit
            '
            Me.grpStaffEdit.BackColor = System.Drawing.Color.FromArgb(CType(CType(248, Byte), Integer), CType(CType(250, Byte), Integer), CType(CType(248, Byte), Integer))
            Me.grpStaffEdit.Controls.Add(Me.lblStaffMsg)
            Me.grpStaffEdit.Controls.Add(Me.btnStaffToggleActive)
            Me.grpStaffEdit.Controls.Add(Me.btnStaffResetPass)
            Me.grpStaffEdit.Controls.Add(Me.btnStaffSave)
            Me.grpStaffEdit.Controls.Add(Me.btnStaffNew)
            Me.grpStaffEdit.Controls.Add(Me.txtStaffPassword)
            Me.grpStaffEdit.Controls.Add(Me.lblStaffPass)
            Me.grpStaffEdit.Controls.Add(Me.cboStaffRole)
            Me.grpStaffEdit.Controls.Add(Me.lblStaffRole)
            Me.grpStaffEdit.Controls.Add(Me.txtStaffName)
            Me.grpStaffEdit.Controls.Add(Me.lblStaffFullName)
            Me.grpStaffEdit.Controls.Add(Me.txtStaffUsername)
            Me.grpStaffEdit.Controls.Add(Me.lblStaffUsername)
            Me.grpStaffEdit.Dock = System.Windows.Forms.DockStyle.Bottom
            Me.grpStaffEdit.Font = New System.Drawing.Font("Segoe UI Semibold", 9.5!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.grpStaffEdit.Location = New System.Drawing.Point(10, 398)
            Me.grpStaffEdit.Name = "grpStaffEdit"
            Me.grpStaffEdit.Size = New System.Drawing.Size(1056, 200)
            Me.grpStaffEdit.TabIndex = 1
            Me.grpStaffEdit.TabStop = False
            Me.grpStaffEdit.Text = "Staff Account Details"
            '
            'lblStaffMsg
            '
            Me.lblStaffMsg.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.lblStaffMsg.Location = New System.Drawing.Point(20, 165)
            Me.lblStaffMsg.Name = "lblStaffMsg"
            Me.lblStaffMsg.Size = New System.Drawing.Size(950, 22)
            Me.lblStaffMsg.TabIndex = 12
            '
            'btnStaffToggleActive
            '
            Me.btnStaffToggleActive.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(205, Byte), Integer), CType(CType(210, Byte), Integer))
            Me.btnStaffToggleActive.Cursor = System.Windows.Forms.Cursors.Hand
            Me.btnStaffToggleActive.FlatAppearance.BorderSize = 0
            Me.btnStaffToggleActive.FlatStyle = System.Windows.Forms.FlatStyle.Flat
            Me.btnStaffToggleActive.Font = New System.Drawing.Font("Segoe UI Semibold", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.btnStaffToggleActive.ForeColor = System.Drawing.Color.DarkRed
            Me.btnStaffToggleActive.Location = New System.Drawing.Point(745, 122)
            Me.btnStaffToggleActive.Name = "btnStaffToggleActive"
            Me.btnStaffToggleActive.Size = New System.Drawing.Size(155, 32)
            Me.btnStaffToggleActive.TabIndex = 11
            Me.btnStaffToggleActive.Text = "Deactivate / Activate"
            Me.btnStaffToggleActive.UseVisualStyleBackColor = False
            '
            'btnStaffResetPass
            '
            Me.btnStaffResetPass.BackColor = System.Drawing.Color.FromArgb(CType(CType(239, Byte), Integer), CType(CType(108, Byte), Integer), CType(CType(0, Byte), Integer))
            Me.btnStaffResetPass.Cursor = System.Windows.Forms.Cursors.Hand
            Me.btnStaffResetPass.FlatAppearance.BorderSize = 0
            Me.btnStaffResetPass.FlatStyle = System.Windows.Forms.FlatStyle.Flat
            Me.btnStaffResetPass.Font = New System.Drawing.Font("Segoe UI Semibold", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.btnStaffResetPass.ForeColor = System.Drawing.Color.White
            Me.btnStaffResetPass.Location = New System.Drawing.Point(595, 122)
            Me.btnStaffResetPass.Name = "btnStaffResetPass"
            Me.btnStaffResetPass.Size = New System.Drawing.Size(135, 32)
            Me.btnStaffResetPass.TabIndex = 10
            Me.btnStaffResetPass.Text = "Reset Password"
            Me.btnStaffResetPass.UseVisualStyleBackColor = False
            '
            'btnStaffSave
            '
            Me.btnStaffSave.BackColor = System.Drawing.Color.FromArgb(CType(CType(46, Byte), Integer), CType(CType(125, Byte), Integer), CType(CType(50, Byte), Integer))
            Me.btnStaffSave.Cursor = System.Windows.Forms.Cursors.Hand
            Me.btnStaffSave.FlatAppearance.BorderSize = 0
            Me.btnStaffSave.FlatStyle = System.Windows.Forms.FlatStyle.Flat
            Me.btnStaffSave.Font = New System.Drawing.Font("Segoe UI Semibold", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.btnStaffSave.ForeColor = System.Drawing.Color.White
            Me.btnStaffSave.Location = New System.Drawing.Point(445, 122)
            Me.btnStaffSave.Name = "btnStaffSave"
            Me.btnStaffSave.Size = New System.Drawing.Size(135, 32)
            Me.btnStaffSave.TabIndex = 9
            Me.btnStaffSave.Text = "Save Staff"
            Me.btnStaffSave.UseVisualStyleBackColor = False
            '
            'btnStaffNew
            '
            Me.btnStaffNew.BackColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer))
            Me.btnStaffNew.Cursor = System.Windows.Forms.Cursors.Hand
            Me.btnStaffNew.FlatAppearance.BorderSize = 0
            Me.btnStaffNew.FlatStyle = System.Windows.Forms.FlatStyle.Flat
            Me.btnStaffNew.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.btnStaffNew.Location = New System.Drawing.Point(335, 122)
            Me.btnStaffNew.Name = "btnStaffNew"
            Me.btnStaffNew.Size = New System.Drawing.Size(95, 32)
            Me.btnStaffNew.TabIndex = 8
            Me.btnStaffNew.Text = "Clear / New"
            Me.btnStaffNew.UseVisualStyleBackColor = False
            '
            'txtStaffPassword
            '
            Me.txtStaffPassword.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.txtStaffPassword.Location = New System.Drawing.Point(120, 126)
            Me.txtStaffPassword.Name = "txtStaffPassword"
            Me.txtStaffPassword.MaxLength = 50
            Me.txtStaffPassword.Size = New System.Drawing.Size(190, 23)
            Me.txtStaffPassword.TabIndex = 7
            Me.txtStaffPassword.UseSystemPasswordChar = True
            '
            'lblStaffPass
            '
            Me.lblStaffPass.AutoSize = True
            Me.lblStaffPass.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.lblStaffPass.Location = New System.Drawing.Point(20, 130)
            Me.lblStaffPass.Name = "lblStaffPass"
            Me.lblStaffPass.Size = New System.Drawing.Size(60, 15)
            Me.lblStaffPass.TabIndex = 6
            Me.lblStaffPass.Text = "Password:"
            '
            'cboStaffRole
            '
            Me.cboStaffRole.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
            Me.cboStaffRole.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.cboStaffRole.FormattingEnabled = True
            Me.cboStaffRole.Location = New System.Drawing.Point(120, 92)
            Me.cboStaffRole.Name = "cboStaffRole"
            Me.cboStaffRole.Size = New System.Drawing.Size(190, 23)
            Me.cboStaffRole.TabIndex = 5
            '
            'lblStaffRole
            '
            Me.lblStaffRole.AutoSize = True
            Me.lblStaffRole.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.lblStaffRole.Location = New System.Drawing.Point(20, 96)
            Me.lblStaffRole.Name = "lblStaffRole"
            Me.lblStaffRole.Size = New System.Drawing.Size(33, 15)
            Me.lblStaffRole.TabIndex = 4
            Me.lblStaffRole.Text = "Role:"
            '
            'txtStaffName
            '
            Me.txtStaffName.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.txtStaffName.Location = New System.Drawing.Point(120, 58)
            Me.txtStaffName.Name = "txtStaffName"
            Me.txtStaffName.MaxLength = 50
            Me.txtStaffName.Size = New System.Drawing.Size(310, 23)
            Me.txtStaffName.TabIndex = 3
            '
            'lblStaffFullName
            '
            Me.lblStaffFullName.AutoSize = True
            Me.lblStaffFullName.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.lblStaffFullName.Location = New System.Drawing.Point(20, 62)
            Me.lblStaffFullName.Name = "lblStaffFullName"
            Me.lblStaffFullName.Size = New System.Drawing.Size(64, 15)
            Me.lblStaffFullName.TabIndex = 2
            Me.lblStaffFullName.Text = "Full Name:"
            '
            'txtStaffUsername
            '
            Me.txtStaffUsername.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.txtStaffUsername.Location = New System.Drawing.Point(120, 25)
            Me.txtStaffUsername.Name = "txtStaffUsername"
            Me.txtStaffUsername.MaxLength = 20
            Me.txtStaffUsername.MaxLength = 20
            Me.txtStaffUsername.Size = New System.Drawing.Size(190, 23)
            Me.txtStaffUsername.TabIndex = 1
            '
            'lblStaffUsername
            '
            Me.lblStaffUsername.AutoSize = True
            Me.lblStaffUsername.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.lblStaffUsername.Location = New System.Drawing.Point(20, 29)
            Me.lblStaffUsername.Name = "lblStaffUsername"
            Me.lblStaffUsername.Size = New System.Drawing.Size(63, 15)
            Me.lblStaffUsername.TabIndex = 0
            Me.lblStaffUsername.Text = "Username:"
            '
            'tabProducts
            '
            Me.tabProducts.Controls.Add(Me.splitProducts)
            Me.tabProducts.Location = New System.Drawing.Point(4, 34)
            Me.tabProducts.Name = "tabProducts"
            Me.tabProducts.Padding = New System.Windows.Forms.Padding(3)
            Me.tabProducts.Size = New System.Drawing.Size(1076, 608)
            Me.tabProducts.TabIndex = 2
            Me.tabProducts.Text = "🏷 Product Management"
            Me.tabProducts.UseVisualStyleBackColor = True
            '
            'splitProducts
            '
            Me.splitProducts.Dock = System.Windows.Forms.DockStyle.Fill
            Me.splitProducts.FixedPanel = System.Windows.Forms.FixedPanel.Panel2
            Me.splitProducts.Location = New System.Drawing.Point(3, 3)
            Me.splitProducts.Name = "splitProducts"
            '
            'splitProducts.Panel1
            '
            Me.splitProducts.Panel1.Controls.Add(Me.dgvProducts)
            Me.splitProducts.Panel1.Controls.Add(Me.pnlProdSearch)
            '
            'splitProducts.Panel2
            '
            Me.splitProducts.Panel2.Controls.Add(Me.grpProdDetail)
            Me.splitProducts.Size = New System.Drawing.Size(1070, 602)
            Me.splitProducts.SplitterDistance = 640
            Me.splitProducts.TabIndex = 0
            '
            'dgvProducts
            '
            Me.dgvProducts.AllowUserToAddRows = False
            Me.dgvProducts.AllowUserToDeleteRows = False
            Me.dgvProducts.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill
            Me.dgvProducts.BackgroundColor = System.Drawing.Color.White
            Me.dgvProducts.BorderStyle = System.Windows.Forms.BorderStyle.None
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
            dataGridViewCellStyle3.BackColor = System.Drawing.Color.FromArgb(CType(CType(30, Byte), Integer), CType(CType(94, Byte), Integer), CType(CType(58, Byte), Integer))
            dataGridViewCellStyle3.Font = New System.Drawing.Font("Segoe UI", 9.5!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            dataGridViewCellStyle3.ForeColor = System.Drawing.Color.White
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
            Me.dgvProducts.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle3
            Me.dgvProducts.ColumnHeadersHeight = 32
            Me.dgvProducts.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.colPrCode, Me.colPrName, Me.colPrCat, Me.colPrPrice, Me.colPrCost, Me.colPrStock, Me.colPrStatus})
            Me.dgvProducts.Dock = System.Windows.Forms.DockStyle.Fill
            Me.dgvProducts.EnableHeadersVisualStyles = False
            Me.dgvProducts.Location = New System.Drawing.Point(0, 50)
            Me.dgvProducts.MultiSelect = False
            Me.dgvProducts.Name = "dgvProducts"
            Me.dgvProducts.ReadOnly = True
            Me.dgvProducts.RowHeadersVisible = False
            Me.dgvProducts.RowTemplate.Height = 26
            Me.dgvProducts.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
            Me.dgvProducts.Size = New System.Drawing.Size(640, 552)
            Me.dgvProducts.TabIndex = 1
            '
            'colPrCode
            '
            Me.colPrCode.FillWeight = 18.0!
            Me.colPrCode.HeaderText = "Code"
            Me.colPrCode.Name = "colPrCode"
            Me.colPrCode.ReadOnly = True
            '
            'colPrName
            '
            Me.colPrName.FillWeight = 36.0!
            Me.colPrName.HeaderText = "Name"
            Me.colPrName.Name = "colPrName"
            Me.colPrName.ReadOnly = True
            '
            'colPrCat
            '
            Me.colPrCat.FillWeight = 22.0!
            Me.colPrCat.HeaderText = "Category"
            Me.colPrCat.Name = "colPrCat"
            Me.colPrCat.ReadOnly = True
            '
            'colPrPrice
            '
            Me.colPrPrice.FillWeight = 16.0!
            Me.colPrPrice.HeaderText = "Price"
            Me.colPrPrice.Name = "colPrPrice"
            Me.colPrPrice.ReadOnly = True
            '
            'colPrCost
            '
            Me.colPrCost.FillWeight = 16.0!
            Me.colPrCost.HeaderText = "Cost"
            Me.colPrCost.Name = "colPrCost"
            Me.colPrCost.ReadOnly = True
            '
            'colPrStock
            '
            Me.colPrStock.FillWeight = 14.0!
            Me.colPrStock.HeaderText = "Stock"
            Me.colPrStock.Name = "colPrStock"
            Me.colPrStock.ReadOnly = True
            '
            'colPrStatus
            '
            Me.colPrStatus.FillWeight = 16.0!
            Me.colPrStatus.HeaderText = "Active"
            Me.colPrStatus.Name = "colPrStatus"
            Me.colPrStatus.ReadOnly = True
            '
            'pnlProdSearch
            '
            Me.pnlProdSearch.BackColor = System.Drawing.Color.FromArgb(CType(CType(245, Byte), Integer), CType(CType(248, Byte), Integer), CType(CType(245, Byte), Integer))
            Me.pnlProdSearch.Controls.Add(Me.cboProdCatFilter)
            Me.pnlProdSearch.Controls.Add(Me.txtProdSearch)
            Me.pnlProdSearch.Controls.Add(Me.lblProdSearch)
            Me.pnlProdSearch.Dock = System.Windows.Forms.DockStyle.Top
            Me.pnlProdSearch.Location = New System.Drawing.Point(0, 0)
            Me.pnlProdSearch.Name = "pnlProdSearch"
            Me.pnlProdSearch.Size = New System.Drawing.Size(640, 50)
            Me.pnlProdSearch.TabIndex = 0
            '
            'cboProdCatFilter
            '
            Me.cboProdCatFilter.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
            Me.cboProdCatFilter.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.cboProdCatFilter.FormattingEnabled = True
            Me.cboProdCatFilter.Location = New System.Drawing.Point(340, 13)
            Me.cboProdCatFilter.Name = "cboProdCatFilter"
            Me.cboProdCatFilter.Size = New System.Drawing.Size(180, 23)
            Me.cboProdCatFilter.TabIndex = 2
            '
            'txtProdSearch
            '
            Me.txtProdSearch.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.txtProdSearch.Location = New System.Drawing.Point(80, 13)
            Me.txtProdSearch.Name = "txtProdSearch"
            Me.txtProdSearch.MaxLength = 30
            Me.txtProdSearch.Size = New System.Drawing.Size(240, 23)
            Me.txtProdSearch.TabIndex = 1
            '
            'lblProdSearch
            '
            Me.lblProdSearch.AutoSize = True
            Me.lblProdSearch.Font = New System.Drawing.Font("Segoe UI Semibold", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.lblProdSearch.Location = New System.Drawing.Point(15, 17)
            Me.lblProdSearch.Name = "lblProdSearch"
            Me.lblProdSearch.Size = New System.Drawing.Size(45, 15)
            Me.lblProdSearch.TabIndex = 0
            Me.lblProdSearch.Text = "Search:"
            '
            'grpProdDetail
            '
            Me.grpProdDetail.BackColor = System.Drawing.Color.FromArgb(CType(CType(248, Byte), Integer), CType(CType(250, Byte), Integer), CType(CType(248, Byte), Integer))
            Me.grpProdDetail.Controls.Add(Me.lblProdMsg)
            Me.grpProdDetail.Controls.Add(Me.btnProdDelete)
            Me.grpProdDetail.Controls.Add(Me.btnProdSave)
            Me.grpProdDetail.Controls.Add(Me.btnProdNew)
            Me.grpProdDetail.Controls.Add(Me.chkProdActive)
            Me.grpProdDetail.Controls.Add(Me.dtpProdExpiry)
            Me.grpProdDetail.Controls.Add(Me.lblProdExpiry)
            Me.grpProdDetail.Controls.Add(Me.numProdReorder)
            Me.grpProdDetail.Controls.Add(Me.lblProdReorder)
            Me.grpProdDetail.Controls.Add(Me.numProdStock)
            Me.grpProdDetail.Controls.Add(Me.lblProdStock)
            Me.grpProdDetail.Controls.Add(Me.txtProdCost)
            Me.grpProdDetail.Controls.Add(Me.lblProdCost)
            Me.grpProdDetail.Controls.Add(Me.txtProdPrice)
            Me.grpProdDetail.Controls.Add(Me.lblProdPrice)
            Me.grpProdDetail.Controls.Add(Me.cboProdSupplier)
            Me.grpProdDetail.Controls.Add(Me.lblProdSupplier)
            Me.grpProdDetail.Controls.Add(Me.cboProdCategory)
            Me.grpProdDetail.Controls.Add(Me.lblProdCat)
            Me.grpProdDetail.Controls.Add(Me.txtProdName)
            Me.grpProdDetail.Controls.Add(Me.lblProdName)
            Me.grpProdDetail.Controls.Add(Me.txtProdCode)
            Me.grpProdDetail.Controls.Add(Me.lblProdCode)
            Me.grpProdDetail.Dock = System.Windows.Forms.DockStyle.Fill
            Me.grpProdDetail.Font = New System.Drawing.Font("Segoe UI Semibold", 9.5!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.grpProdDetail.Location = New System.Drawing.Point(0, 0)
            Me.grpProdDetail.Name = "grpProdDetail"
            Me.grpProdDetail.Size = New System.Drawing.Size(426, 602)
            Me.grpProdDetail.TabIndex = 0
            Me.grpProdDetail.TabStop = False
            Me.grpProdDetail.Text = "Product Editor"
            '
            'lblProdMsg
            '
            Me.lblProdMsg.Font = New System.Drawing.Font("Segoe UI", 8.5!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.lblProdMsg.Location = New System.Drawing.Point(15, 520)
            Me.lblProdMsg.Name = "lblProdMsg"
            Me.lblProdMsg.Size = New System.Drawing.Size(395, 30)
            Me.lblProdMsg.TabIndex = 22
            Me.lblProdMsg.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
            '
            'btnProdDelete
            '
            Me.btnProdDelete.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(205, Byte), Integer), CType(CType(210, Byte), Integer))
            Me.btnProdDelete.Cursor = System.Windows.Forms.Cursors.Hand
            Me.btnProdDelete.FlatAppearance.BorderSize = 0
            Me.btnProdDelete.FlatStyle = System.Windows.Forms.FlatStyle.Flat
            Me.btnProdDelete.Font = New System.Drawing.Font("Segoe UI Semibold", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.btnProdDelete.ForeColor = System.Drawing.Color.DarkRed
            Me.btnProdDelete.Location = New System.Drawing.Point(265, 470)
            Me.btnProdDelete.Name = "btnProdDelete"
            Me.btnProdDelete.Size = New System.Drawing.Size(115, 34)
            Me.btnProdDelete.TabIndex = 21
            Me.btnProdDelete.Text = "Deactivate"
            Me.btnProdDelete.UseVisualStyleBackColor = False
            '
            'btnProdSave
            '
            Me.btnProdSave.BackColor = System.Drawing.Color.FromArgb(CType(CType(46, Byte), Integer), CType(CType(125, Byte), Integer), CType(CType(50, Byte), Integer))
            Me.btnProdSave.Cursor = System.Windows.Forms.Cursors.Hand
            Me.btnProdSave.FlatAppearance.BorderSize = 0
            Me.btnProdSave.FlatStyle = System.Windows.Forms.FlatStyle.Flat
            Me.btnProdSave.Font = New System.Drawing.Font("Segoe UI Semibold", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.btnProdSave.ForeColor = System.Drawing.Color.White
            Me.btnProdSave.Location = New System.Drawing.Point(140, 470)
            Me.btnProdSave.Name = "btnProdSave"
            Me.btnProdSave.Size = New System.Drawing.Size(115, 34)
            Me.btnProdSave.TabIndex = 20
            Me.btnProdSave.Text = "Save Product"
            Me.btnProdSave.UseVisualStyleBackColor = False
            '
            'btnProdNew
            '
            Me.btnProdNew.BackColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer))
            Me.btnProdNew.Cursor = System.Windows.Forms.Cursors.Hand
            Me.btnProdNew.FlatAppearance.BorderSize = 0
            Me.btnProdNew.FlatStyle = System.Windows.Forms.FlatStyle.Flat
            Me.btnProdNew.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.btnProdNew.Location = New System.Drawing.Point(25, 470)
            Me.btnProdNew.Name = "btnProdNew"
            Me.btnProdNew.Size = New System.Drawing.Size(105, 34)
            Me.btnProdNew.TabIndex = 19
            Me.btnProdNew.Text = "Clear / New"
            Me.btnProdNew.UseVisualStyleBackColor = False
            '
            'chkProdActive
            '
            Me.chkProdActive.AutoSize = True
            Me.chkProdActive.Checked = True
            Me.chkProdActive.CheckState = System.Windows.Forms.CheckState.Checked
            Me.chkProdActive.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.chkProdActive.Location = New System.Drawing.Point(135, 425)
            Me.chkProdActive.Name = "chkProdActive"
            Me.chkProdActive.Size = New System.Drawing.Size(135, 19)
            Me.chkProdActive.TabIndex = 18
            Me.chkProdActive.Text = "Active (For Retail Sale)"
            Me.chkProdActive.UseVisualStyleBackColor = True
            '
            'dtpProdExpiry
            '
            Me.dtpProdExpiry.Checked = False
            Me.dtpProdExpiry.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.dtpProdExpiry.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
            Me.dtpProdExpiry.Location = New System.Drawing.Point(135, 385)
            Me.dtpProdExpiry.Name = "dtpProdExpiry"
            Me.dtpProdExpiry.ShowCheckBox = True
            Me.dtpProdExpiry.Size = New System.Drawing.Size(180, 23)
            Me.dtpProdExpiry.TabIndex = 17
            '
            'lblProdExpiry
            '
            Me.lblProdExpiry.AutoSize = True
            Me.lblProdExpiry.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.lblProdExpiry.Location = New System.Drawing.Point(20, 390)
            Me.lblProdExpiry.Name = "lblProdExpiry"
            Me.lblProdExpiry.Size = New System.Drawing.Size(69, 15)
            Me.lblProdExpiry.TabIndex = 16
            Me.lblProdExpiry.Text = "Expiry Date:"
            '
            'numProdReorder
            '
            Me.numProdReorder.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.numProdReorder.Location = New System.Drawing.Point(135, 345)
            Me.numProdReorder.Maximum = New Decimal(New Integer() {100000, 0, 0, 0})
            Me.numProdReorder.Name = "numProdReorder"
            Me.numProdReorder.Size = New System.Drawing.Size(120, 23)
            Me.numProdReorder.TabIndex = 15
            Me.numProdReorder.Value = New Decimal(New Integer() {10, 0, 0, 0})
            '
            'lblProdReorder
            '
            Me.lblProdReorder.AutoSize = True
            Me.lblProdReorder.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.lblProdReorder.Location = New System.Drawing.Point(20, 350)
            Me.lblProdReorder.Name = "lblProdReorder"
            Me.lblProdReorder.Size = New System.Drawing.Size(81, 15)
            Me.lblProdReorder.TabIndex = 14
            Me.lblProdReorder.Text = "Reorder Point:"
            '
            'numProdStock
            '
            Me.numProdStock.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.numProdStock.Location = New System.Drawing.Point(135, 305)
            Me.numProdStock.Maximum = New Decimal(New Integer() {100000, 0, 0, 0})
            Me.numProdStock.Name = "numProdStock"
            Me.numProdStock.Size = New System.Drawing.Size(120, 23)
            Me.numProdStock.TabIndex = 13
            Me.numProdStock.Value = New Decimal(New Integer() {20, 0, 0, 0})
            '
            'lblProdStock
            '
            Me.lblProdStock.AutoSize = True
            Me.lblProdStock.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.lblProdStock.Location = New System.Drawing.Point(20, 310)
            Me.lblProdStock.Name = "lblProdStock"
            Me.lblProdStock.Size = New System.Drawing.Size(61, 15)
            Me.lblProdStock.TabIndex = 12
            Me.lblProdStock.Text = "Stock Qty:"
            '
            'txtProdCost
            '
            Me.txtProdCost.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.txtProdCost.Location = New System.Drawing.Point(135, 265)
            Me.txtProdCost.Name = "txtProdCost"
            Me.txtProdCost.MaxLength = 12
            Me.txtProdCost.Size = New System.Drawing.Size(140, 23)
            Me.txtProdCost.TabIndex = 11
            '
            'lblProdCost
            '
            Me.lblProdCost.AutoSize = True
            Me.lblProdCost.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.lblProdCost.Location = New System.Drawing.Point(20, 270)
            Me.lblProdCost.Name = "lblProdCost"
            Me.lblProdCost.Size = New System.Drawing.Size(91, 15)
            Me.lblProdCost.TabIndex = 10
            Me.lblProdCost.Text = "Cost Price (PHP):"
            '
            'txtProdPrice
            '
            Me.txtProdPrice.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.txtProdPrice.Location = New System.Drawing.Point(135, 225)
            Me.txtProdPrice.Name = "txtProdPrice"
            Me.txtProdPrice.MaxLength = 12
            Me.txtProdPrice.Size = New System.Drawing.Size(140, 23)
            Me.txtProdPrice.TabIndex = 9
            '
            'lblProdPrice
            '
            Me.lblProdPrice.AutoSize = True
            Me.lblProdPrice.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.lblProdPrice.Location = New System.Drawing.Point(20, 230)
            Me.lblProdPrice.Name = "lblProdPrice"
            Me.lblProdPrice.Size = New System.Drawing.Size(94, 15)
            Me.lblProdPrice.TabIndex = 8
            Me.lblProdPrice.Text = "Retail Price (PHP):"
            '
            'cboProdSupplier
            '
            Me.cboProdSupplier.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
            Me.cboProdSupplier.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.cboProdSupplier.FormattingEnabled = True
            Me.cboProdSupplier.Location = New System.Drawing.Point(135, 185)
            Me.cboProdSupplier.Name = "cboProdSupplier"
            Me.cboProdSupplier.Size = New System.Drawing.Size(265, 23)
            Me.cboProdSupplier.TabIndex = 7
            '
            'lblProdSupplier
            '
            Me.lblProdSupplier.AutoSize = True
            Me.lblProdSupplier.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.lblProdSupplier.Location = New System.Drawing.Point(20, 190)
            Me.lblProdSupplier.Name = "lblProdSupplier"
            Me.lblProdSupplier.Size = New System.Drawing.Size(53, 15)
            Me.lblProdSupplier.TabIndex = 6
            Me.lblProdSupplier.Text = "Supplier:"
            '
            'cboProdCategory
            '
            Me.cboProdCategory.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
            Me.cboProdCategory.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.cboProdCategory.FormattingEnabled = True
            Me.cboProdCategory.Location = New System.Drawing.Point(135, 145)
            Me.cboProdCategory.Name = "cboProdCategory"
            Me.cboProdCategory.Size = New System.Drawing.Size(265, 23)
            Me.cboProdCategory.TabIndex = 5
            '
            'lblProdCat
            '
            Me.lblProdCat.AutoSize = True
            Me.lblProdCat.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.lblProdCat.Location = New System.Drawing.Point(20, 150)
            Me.lblProdCat.Name = "lblProdCat"
            Me.lblProdCat.Size = New System.Drawing.Size(58, 15)
            Me.lblProdCat.TabIndex = 4
            Me.lblProdCat.Text = "Category:"
            '
            'txtProdName
            '
            Me.txtProdName.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.txtProdName.Location = New System.Drawing.Point(135, 68)
            Me.txtProdName.Multiline = True
            Me.txtProdName.Name = "txtProdName"
            Me.txtProdName.MaxLength = 50
            Me.txtProdName.Size = New System.Drawing.Size(265, 60)
            Me.txtProdName.TabIndex = 3
            '
            'lblProdName
            '
            Me.lblProdName.AutoSize = True
            Me.lblProdName.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.lblProdName.Location = New System.Drawing.Point(20, 72)
            Me.lblProdName.Name = "lblProdName"
            Me.lblProdName.Size = New System.Drawing.Size(87, 15)
            Me.lblProdName.TabIndex = 2
            Me.lblProdName.Text = "Product Name:"
            '
            'txtProdCode
            '
            Me.txtProdCode.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.txtProdCode.Location = New System.Drawing.Point(135, 30)
            Me.txtProdCode.Name = "txtProdCode"
            Me.txtProdCode.MaxLength = 20
            Me.txtProdCode.Size = New System.Drawing.Size(180, 23)
            Me.txtProdCode.TabIndex = 1
            '
            'lblProdCode
            '
            Me.lblProdCode.AutoSize = True
            Me.lblProdCode.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.lblProdCode.Location = New System.Drawing.Point(20, 34)
            Me.lblProdCode.Name = "lblProdCode"
            Me.lblProdCode.Size = New System.Drawing.Size(83, 15)
            Me.lblProdCode.TabIndex = 0
            Me.lblProdCode.Text = "Product Code:"
            '
            'tabMasterData
            '
            Me.tabMasterData.Controls.Add(Me.splitMaster)
            Me.tabMasterData.Location = New System.Drawing.Point(4, 34)
            Me.tabMasterData.Name = "tabMasterData"
            Me.tabMasterData.Padding = New System.Windows.Forms.Padding(3)
            Me.tabMasterData.Size = New System.Drawing.Size(1076, 608)
            Me.tabMasterData.TabIndex = 3
            Me.tabMasterData.Text = "🗂 Categories & Suppliers"
            Me.tabMasterData.UseVisualStyleBackColor = True
            '
            'splitMaster
            '
            Me.splitMaster.Dock = System.Windows.Forms.DockStyle.Fill
            Me.splitMaster.Location = New System.Drawing.Point(3, 3)
            Me.splitMaster.Name = "splitMaster"
            '
            'splitMaster.Panel1
            '
            Me.splitMaster.Panel1.Controls.Add(Me.grpCategories)
            '
            'splitMaster.Panel2
            '
            Me.splitMaster.Panel2.Controls.Add(Me.grpSuppliers)
            Me.splitMaster.Size = New System.Drawing.Size(1070, 602)
            Me.splitMaster.SplitterDistance = 450
            Me.splitMaster.TabIndex = 0
            '
            'grpCategories
            '
            Me.grpCategories.Controls.Add(Me.dgvCategories)
            Me.grpCategories.Controls.Add(Me.pnlCatActions)
            Me.grpCategories.Dock = System.Windows.Forms.DockStyle.Fill
            Me.grpCategories.Font = New System.Drawing.Font("Segoe UI Semibold", 9.5!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.grpCategories.Location = New System.Drawing.Point(0, 0)
            Me.grpCategories.Name = "grpCategories"
            Me.grpCategories.Size = New System.Drawing.Size(450, 602)
            Me.grpCategories.TabIndex = 0
            Me.grpCategories.TabStop = False
            Me.grpCategories.Text = "Categories Management"
            '
            'dgvCategories
            '
            Me.dgvCategories.AllowUserToAddRows = False
            Me.dgvCategories.AllowUserToDeleteRows = False
            Me.dgvCategories.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill
            Me.dgvCategories.BackgroundColor = System.Drawing.Color.White
            Me.dgvCategories.BorderStyle = System.Windows.Forms.BorderStyle.None
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
            dataGridViewCellStyle4.BackColor = System.Drawing.Color.FromArgb(CType(CType(30, Byte), Integer), CType(CType(94, Byte), Integer), CType(CType(58, Byte), Integer))
            dataGridViewCellStyle4.Font = New System.Drawing.Font("Segoe UI", 9.5!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            dataGridViewCellStyle4.ForeColor = System.Drawing.Color.White
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.Highlight
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.HighlightText
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
            Me.dgvCategories.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle4
            Me.dgvCategories.ColumnHeadersHeight = 30
            Me.dgvCategories.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.colCatId, Me.colCatTitle})
            Me.dgvCategories.Dock = System.Windows.Forms.DockStyle.Fill
            Me.dgvCategories.EnableHeadersVisualStyles = False
            Me.dgvCategories.Location = New System.Drawing.Point(3, 20)
            Me.dgvCategories.MultiSelect = False
            Me.dgvCategories.Name = "dgvCategories"
            Me.dgvCategories.ReadOnly = True
            Me.dgvCategories.RowHeadersVisible = False
            Me.dgvCategories.RowTemplate.Height = 26
            Me.dgvCategories.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
            Me.dgvCategories.Size = New System.Drawing.Size(444, 462)
            Me.dgvCategories.TabIndex = 0
            '
            'colCatId
            '
            Me.colCatId.FillWeight = 25.0!
            Me.colCatId.HeaderText = "ID"
            Me.colCatId.Name = "colCatId"
            Me.colCatId.ReadOnly = True
            '
            'colCatTitle
            '
            Me.colCatTitle.FillWeight = 75.0!
            Me.colCatTitle.HeaderText = "Category Name"
            Me.colCatTitle.Name = "colCatTitle"
            Me.colCatTitle.ReadOnly = True
            '
            'pnlCatActions
            '
            Me.pnlCatActions.BackColor = System.Drawing.Color.FromArgb(CType(CType(245, Byte), Integer), CType(CType(248, Byte), Integer), CType(CType(245, Byte), Integer))
            Me.pnlCatActions.Controls.Add(Me.btnCatDelete)
            Me.pnlCatActions.Controls.Add(Me.btnCatSave)
            Me.pnlCatActions.Controls.Add(Me.txtCatName)
            Me.pnlCatActions.Controls.Add(Me.lblCatName)
            Me.pnlCatActions.Dock = System.Windows.Forms.DockStyle.Bottom
            Me.pnlCatActions.Location = New System.Drawing.Point(3, 482)
            Me.pnlCatActions.Name = "pnlCatActions"
            Me.pnlCatActions.Size = New System.Drawing.Size(444, 117)
            Me.pnlCatActions.TabIndex = 1
            '
            'btnCatDelete
            '
            Me.btnCatDelete.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(205, Byte), Integer), CType(CType(210, Byte), Integer))
            Me.btnCatDelete.Cursor = System.Windows.Forms.Cursors.Hand
            Me.btnCatDelete.FlatAppearance.BorderSize = 0
            Me.btnCatDelete.FlatStyle = System.Windows.Forms.FlatStyle.Flat
            Me.btnCatDelete.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.btnCatDelete.ForeColor = System.Drawing.Color.DarkRed
            Me.btnCatDelete.Location = New System.Drawing.Point(235, 65)
            Me.btnCatDelete.Name = "btnCatDelete"
            Me.btnCatDelete.Size = New System.Drawing.Size(95, 30)
            Me.btnCatDelete.TabIndex = 3
            Me.btnCatDelete.Text = "Delete"
            Me.btnCatDelete.UseVisualStyleBackColor = False
            '
            'btnCatSave
            '
            Me.btnCatSave.BackColor = System.Drawing.Color.FromArgb(CType(CType(46, Byte), Integer), CType(CType(125, Byte), Integer), CType(CType(50, Byte), Integer))
            Me.btnCatSave.Cursor = System.Windows.Forms.Cursors.Hand
            Me.btnCatSave.FlatAppearance.BorderSize = 0
            Me.btnCatSave.FlatStyle = System.Windows.Forms.FlatStyle.Flat
            Me.btnCatSave.Font = New System.Drawing.Font("Segoe UI Semibold", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.btnCatSave.ForeColor = System.Drawing.Color.White
            Me.btnCatSave.Location = New System.Drawing.Point(115, 65)
            Me.btnCatSave.Name = "btnCatSave"
            Me.btnCatSave.Size = New System.Drawing.Size(105, 30)
            Me.btnCatSave.TabIndex = 2
            Me.btnCatSave.Text = "Save Category"
            Me.btnCatSave.UseVisualStyleBackColor = False
            '
            'txtCatName
            '
            Me.txtCatName.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.txtCatName.Location = New System.Drawing.Point(115, 20)
            Me.txtCatName.Name = "txtCatName"
            Me.txtCatName.MaxLength = 30
            Me.txtCatName.Size = New System.Drawing.Size(295, 23)
            Me.txtCatName.TabIndex = 1
            '
            'lblCatName
            '
            Me.lblCatName.AutoSize = True
            Me.lblCatName.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.lblCatName.Location = New System.Drawing.Point(15, 24)
            Me.lblCatName.Name = "lblCatName"
            Me.lblCatName.Size = New System.Drawing.Size(93, 15)
            Me.lblCatName.TabIndex = 0
            Me.lblCatName.Text = "Category Name:"
            '
            'grpSuppliers
            '
            Me.grpSuppliers.Controls.Add(Me.dgvSuppliers)
            Me.grpSuppliers.Controls.Add(Me.pnlSupActions)
            Me.grpSuppliers.Dock = System.Windows.Forms.DockStyle.Fill
            Me.grpSuppliers.Font = New System.Drawing.Font("Segoe UI Semibold", 9.5!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.grpSuppliers.Location = New System.Drawing.Point(0, 0)
            Me.grpSuppliers.Name = "grpSuppliers"
            Me.grpSuppliers.Size = New System.Drawing.Size(616, 602)
            Me.grpSuppliers.TabIndex = 0
            Me.grpSuppliers.TabStop = False
            Me.grpSuppliers.Text = "Suppliers Management"
            '
            'dgvSuppliers
            '
            Me.dgvSuppliers.AllowUserToAddRows = False
            Me.dgvSuppliers.AllowUserToDeleteRows = False
            Me.dgvSuppliers.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill
            Me.dgvSuppliers.BackgroundColor = System.Drawing.Color.White
            Me.dgvSuppliers.BorderStyle = System.Windows.Forms.BorderStyle.None
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
            dataGridViewCellStyle5.BackColor = System.Drawing.Color.FromArgb(CType(CType(30, Byte), Integer), CType(CType(94, Byte), Integer), CType(CType(58, Byte), Integer))
            dataGridViewCellStyle5.Font = New System.Drawing.Font("Segoe UI", 9.5!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            dataGridViewCellStyle5.ForeColor = System.Drawing.Color.White
            dataGridViewCellStyle5.SelectionBackColor = System.Drawing.SystemColors.Highlight
            dataGridViewCellStyle5.SelectionForeColor = System.Drawing.SystemColors.HighlightText
            dataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
            Me.dgvSuppliers.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle5
            Me.dgvSuppliers.ColumnHeadersHeight = 30
            Me.dgvSuppliers.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.colSupId, Me.colSupName, Me.colSupContact, Me.colSupAddr})
            Me.dgvSuppliers.Dock = System.Windows.Forms.DockStyle.Fill
            Me.dgvSuppliers.EnableHeadersVisualStyles = False
            Me.dgvSuppliers.Location = New System.Drawing.Point(3, 20)
            Me.dgvSuppliers.MultiSelect = False
            Me.dgvSuppliers.Name = "dgvSuppliers"
            Me.dgvSuppliers.ReadOnly = True
            Me.dgvSuppliers.RowHeadersVisible = False
            Me.dgvSuppliers.RowTemplate.Height = 26
            Me.dgvSuppliers.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
            Me.dgvSuppliers.Size = New System.Drawing.Size(610, 399)
            Me.dgvSuppliers.TabIndex = 0
            '
            'colSupId
            '
            Me.colSupId.FillWeight = 15.0!
            Me.colSupId.HeaderText = "ID"
            Me.colSupId.Name = "colSupId"
            Me.colSupId.ReadOnly = True
            '
            'colSupName
            '
            Me.colSupName.FillWeight = 35.0!
            Me.colSupName.HeaderText = "Supplier Name"
            Me.colSupName.Name = "colSupName"
            Me.colSupName.ReadOnly = True
            '
            'colSupContact
            '
            Me.colSupContact.FillWeight = 25.0!
            Me.colSupContact.HeaderText = "Contact"
            Me.colSupContact.Name = "colSupContact"
            Me.colSupContact.ReadOnly = True
            '
            'colSupAddr
            '
            Me.colSupAddr.FillWeight = 35.0!
            Me.colSupAddr.HeaderText = "Address"
            Me.colSupAddr.Name = "colSupAddr"
            Me.colSupAddr.ReadOnly = True
            '
            'pnlSupActions
            '
            Me.pnlSupActions.BackColor = System.Drawing.Color.FromArgb(CType(CType(245, Byte), Integer), CType(CType(248, Byte), Integer), CType(CType(245, Byte), Integer))
            Me.pnlSupActions.Controls.Add(Me.btnSupDelete)
            Me.pnlSupActions.Controls.Add(Me.btnSupSave)
            Me.pnlSupActions.Controls.Add(Me.btnSupNew)
            Me.pnlSupActions.Controls.Add(Me.txtSupAddr)
            Me.pnlSupActions.Controls.Add(Me.lblSupAddr)
            Me.pnlSupActions.Controls.Add(Me.txtSupContact)
            Me.pnlSupActions.Controls.Add(Me.lblSupContact)
            Me.pnlSupActions.Controls.Add(Me.txtSupName)
            Me.pnlSupActions.Controls.Add(Me.lblSupName)
            Me.pnlSupActions.Dock = System.Windows.Forms.DockStyle.Bottom
            Me.pnlSupActions.Location = New System.Drawing.Point(3, 419)
            Me.pnlSupActions.Name = "pnlSupActions"
            Me.pnlSupActions.Size = New System.Drawing.Size(610, 180)
            Me.pnlSupActions.TabIndex = 1
            '
            'btnSupDelete
            '
            Me.btnSupDelete.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(205, Byte), Integer), CType(CType(210, Byte), Integer))
            Me.btnSupDelete.Cursor = System.Windows.Forms.Cursors.Hand
            Me.btnSupDelete.FlatAppearance.BorderSize = 0
            Me.btnSupDelete.FlatStyle = System.Windows.Forms.FlatStyle.Flat
            Me.btnSupDelete.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.btnSupDelete.ForeColor = System.Drawing.Color.DarkRed
            Me.btnSupDelete.Location = New System.Drawing.Point(340, 130)
            Me.btnSupDelete.Name = "btnSupDelete"
            Me.btnSupDelete.Size = New System.Drawing.Size(95, 30)
            Me.btnSupDelete.TabIndex = 8
            Me.btnSupDelete.Text = "Delete"
            Me.btnSupDelete.UseVisualStyleBackColor = False
            '
            'btnSupSave
            '
            Me.btnSupSave.BackColor = System.Drawing.Color.FromArgb(CType(CType(46, Byte), Integer), CType(CType(125, Byte), Integer), CType(CType(50, Byte), Integer))
            Me.btnSupSave.Cursor = System.Windows.Forms.Cursors.Hand
            Me.btnSupSave.FlatAppearance.BorderSize = 0
            Me.btnSupSave.FlatStyle = System.Windows.Forms.FlatStyle.Flat
            Me.btnSupSave.Font = New System.Drawing.Font("Segoe UI Semibold", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.btnSupSave.ForeColor = System.Drawing.Color.White
            Me.btnSupSave.Location = New System.Drawing.Point(220, 130)
            Me.btnSupSave.Name = "btnSupSave"
            Me.btnSupSave.Size = New System.Drawing.Size(105, 30)
            Me.btnSupSave.TabIndex = 7
            Me.btnSupSave.Text = "Save Supplier"
            Me.btnSupSave.UseVisualStyleBackColor = False
            '
            'btnSupNew
            '
            Me.btnSupNew.BackColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer))
            Me.btnSupNew.Cursor = System.Windows.Forms.Cursors.Hand
            Me.btnSupNew.FlatAppearance.BorderSize = 0
            Me.btnSupNew.FlatStyle = System.Windows.Forms.FlatStyle.Flat
            Me.btnSupNew.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.btnSupNew.Location = New System.Drawing.Point(115, 130)
            Me.btnSupNew.Name = "btnSupNew"
            Me.btnSupNew.Size = New System.Drawing.Size(95, 30)
            Me.btnSupNew.TabIndex = 6
            Me.btnSupNew.Text = "Clear / New"
            Me.btnSupNew.UseVisualStyleBackColor = False
            '
            'txtSupAddr
            '
            Me.txtSupAddr.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.txtSupAddr.Location = New System.Drawing.Point(115, 88)
            Me.txtSupAddr.Name = "txtSupAddr"
            Me.txtSupAddr.MaxLength = 100
            Me.txtSupAddr.Size = New System.Drawing.Size(430, 23)
            Me.txtSupAddr.TabIndex = 5
            '
            'lblSupAddr
            '
            Me.lblSupAddr.AutoSize = True
            Me.lblSupAddr.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.lblSupAddr.Location = New System.Drawing.Point(15, 92)
            Me.lblSupAddr.Name = "lblSupAddr"
            Me.lblSupAddr.Size = New System.Drawing.Size(52, 15)
            Me.lblSupAddr.TabIndex = 4
            Me.lblSupAddr.Text = "Address:"
            '
            'txtSupContact
            '
            Me.txtSupContact.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.txtSupContact.Location = New System.Drawing.Point(115, 52)
            Me.txtSupContact.Name = "txtSupContact"
            Me.txtSupContact.MaxLength = 15
            Me.txtSupContact.Size = New System.Drawing.Size(260, 23)
            Me.txtSupContact.TabIndex = 3
            '
            'lblSupContact
            '
            Me.lblSupContact.AutoSize = True
            Me.lblSupContact.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.lblSupContact.Location = New System.Drawing.Point(15, 56)
            Me.lblSupContact.Name = "lblSupContact"
            Me.lblSupContact.Size = New System.Drawing.Size(99, 15)
            Me.lblSupContact.TabIndex = 2
            Me.lblSupContact.Text = "Contact Number:"
            '
            'txtSupName
            '
            Me.txtSupName.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.txtSupName.Location = New System.Drawing.Point(115, 16)
            Me.txtSupName.Name = "txtSupName"
            Me.txtSupName.MaxLength = 50
            Me.txtSupName.Size = New System.Drawing.Size(430, 23)
            Me.txtSupName.TabIndex = 1
            '
            'lblSupName
            '
            Me.lblSupName.AutoSize = True
            Me.lblSupName.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.lblSupName.Location = New System.Drawing.Point(15, 20)
            Me.lblSupName.Name = "lblSupName"
            Me.lblSupName.Size = New System.Drawing.Size(88, 15)
            Me.lblSupName.TabIndex = 0
            Me.lblSupName.Text = "Supplier Name:"
            '
            'tabTransactions
            '
            Me.tabTransactions.Controls.Add(Me.dgvTransactions)
            Me.tabTransactions.Controls.Add(Me.pnlTxFilter)
            Me.tabTransactions.Location = New System.Drawing.Point(4, 34)
            Me.tabTransactions.Name = "tabTransactions"
            Me.tabTransactions.Padding = New System.Windows.Forms.Padding(3)
            Me.tabTransactions.Size = New System.Drawing.Size(1076, 608)
            Me.tabTransactions.TabIndex = 4
            Me.tabTransactions.Text = "🧾 Transaction History"
            Me.tabTransactions.UseVisualStyleBackColor = True
            '
            'dgvTransactions
            '
            Me.dgvTransactions.AllowUserToAddRows = False
            Me.dgvTransactions.AllowUserToDeleteRows = False
            Me.dgvTransactions.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill
            Me.dgvTransactions.BackgroundColor = System.Drawing.Color.White
            Me.dgvTransactions.BorderStyle = System.Windows.Forms.BorderStyle.None
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
            dataGridViewCellStyle6.BackColor = System.Drawing.Color.FromArgb(CType(CType(30, Byte), Integer), CType(CType(94, Byte), Integer), CType(CType(58, Byte), Integer))
            dataGridViewCellStyle6.Font = New System.Drawing.Font("Segoe UI", 9.5!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            dataGridViewCellStyle6.ForeColor = System.Drawing.Color.White
            dataGridViewCellStyle6.SelectionBackColor = System.Drawing.SystemColors.Highlight
            dataGridViewCellStyle6.SelectionForeColor = System.Drawing.SystemColors.HighlightText
            dataGridViewCellStyle6.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
            Me.dgvTransactions.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle6
            Me.dgvTransactions.ColumnHeadersHeight = 32
            Me.dgvTransactions.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.colTxReceipt, Me.colTxDate, Me.colTxCashier, Me.colTxCustomer, Me.colTxSubtotal, Me.colTxDiscount, Me.colTxTotal})
            Me.dgvTransactions.Dock = System.Windows.Forms.DockStyle.Fill
            Me.dgvTransactions.EnableHeadersVisualStyles = False
            Me.dgvTransactions.Location = New System.Drawing.Point(3, 53)
            Me.dgvTransactions.MultiSelect = False
            Me.dgvTransactions.Name = "dgvTransactions"
            Me.dgvTransactions.ReadOnly = True
            Me.dgvTransactions.RowHeadersVisible = False
            Me.dgvTransactions.RowTemplate.Height = 26
            Me.dgvTransactions.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
            Me.dgvTransactions.Size = New System.Drawing.Size(1070, 552)
            Me.dgvTransactions.TabIndex = 1
            '
            'colTxReceipt
            '
            Me.colTxReceipt.FillWeight = 20.0!
            Me.colTxReceipt.HeaderText = "Receipt No"
            Me.colTxReceipt.Name = "colTxReceipt"
            Me.colTxReceipt.ReadOnly = True
            '
            'colTxDate
            '
            Me.colTxDate.FillWeight = 22.0!
            Me.colTxDate.HeaderText = "Date & Time"
            Me.colTxDate.Name = "colTxDate"
            Me.colTxDate.ReadOnly = True
            '
            'colTxCashier
            '
            Me.colTxCashier.FillWeight = 22.0!
            Me.colTxCashier.HeaderText = "Cashier"
            Me.colTxCashier.Name = "colTxCashier"
            Me.colTxCashier.ReadOnly = True
            '
            'colTxCustomer
            '
            Me.colTxCustomer.FillWeight = 22.0!
            Me.colTxCustomer.HeaderText = "Customer"
            Me.colTxCustomer.Name = "colTxCustomer"
            Me.colTxCustomer.ReadOnly = True
            '
            'colTxSubtotal
            '
            Me.colTxSubtotal.FillWeight = 16.0!
            Me.colTxSubtotal.HeaderText = "Subtotal (PHP)"
            Me.colTxSubtotal.Name = "colTxSubtotal"
            Me.colTxSubtotal.ReadOnly = True
            '
            'colTxDiscount
            '
            Me.colTxDiscount.FillWeight = 16.0!
            Me.colTxDiscount.HeaderText = "Discount (PHP)"
            Me.colTxDiscount.Name = "colTxDiscount"
            Me.colTxDiscount.ReadOnly = True
            '
            'colTxTotal
            '
            Me.colTxTotal.FillWeight = 18.0!
            Me.colTxTotal.HeaderText = "Total (PHP)"
            Me.colTxTotal.Name = "colTxTotal"
            Me.colTxTotal.ReadOnly = True
            '
            'pnlTxFilter
            '
            Me.pnlTxFilter.BackColor = System.Drawing.Color.FromArgb(CType(CType(245, Byte), Integer), CType(CType(248, Byte), Integer), CType(CType(245, Byte), Integer))
            Me.pnlTxFilter.Controls.Add(Me.btnTxViewDetails)
            Me.pnlTxFilter.Controls.Add(Me.dtpTxEnd)
            Me.pnlTxFilter.Controls.Add(Me.lblTxEnd)
            Me.pnlTxFilter.Controls.Add(Me.dtpTxStart)
            Me.pnlTxFilter.Controls.Add(Me.lblTxStart)
            Me.pnlTxFilter.Dock = System.Windows.Forms.DockStyle.Top
            Me.pnlTxFilter.Location = New System.Drawing.Point(3, 3)
            Me.pnlTxFilter.Name = "pnlTxFilter"
            Me.pnlTxFilter.Size = New System.Drawing.Size(1070, 50)
            Me.pnlTxFilter.TabIndex = 0
            '
            'btnTxViewDetails
            '
            Me.btnTxViewDetails.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.btnTxViewDetails.BackColor = System.Drawing.Color.FromArgb(CType(CType(46, Byte), Integer), CType(CType(125, Byte), Integer), CType(CType(50, Byte), Integer))
            Me.btnTxViewDetails.Cursor = System.Windows.Forms.Cursors.Hand
            Me.btnTxViewDetails.FlatAppearance.BorderSize = 0
            Me.btnTxViewDetails.FlatStyle = System.Windows.Forms.FlatStyle.Flat
            Me.btnTxViewDetails.Font = New System.Drawing.Font("Segoe UI Semibold", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.btnTxViewDetails.ForeColor = System.Drawing.Color.White
            Me.btnTxViewDetails.Location = New System.Drawing.Point(895, 10)
            Me.btnTxViewDetails.Name = "btnTxViewDetails"
            Me.btnTxViewDetails.Size = New System.Drawing.Size(160, 30)
            Me.btnTxViewDetails.TabIndex = 5
            Me.btnTxViewDetails.Text = "📄 View Receipt Items"
            Me.btnTxViewDetails.UseVisualStyleBackColor = False
            '
            'dtpTxEnd
            '
            Me.dtpTxEnd.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.dtpTxEnd.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
            Me.dtpTxEnd.Location = New System.Drawing.Point(365, 14)
            Me.dtpTxEnd.Name = "dtpTxEnd"
            Me.dtpTxEnd.Size = New System.Drawing.Size(200, 23)
            Me.dtpTxEnd.TabIndex = 3
            '
            'lblTxEnd
            '
            Me.lblTxEnd.AutoSize = True
            Me.lblTxEnd.Font = New System.Drawing.Font("Segoe UI Semibold", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.lblTxEnd.Location = New System.Drawing.Point(300, 18)
            Me.lblTxEnd.Name = "lblTxEnd"
            Me.lblTxEnd.Size = New System.Drawing.Size(51, 15)
            Me.lblTxEnd.TabIndex = 2
            Me.lblTxEnd.Text = "Date To:"
            '
            'dtpTxStart
            '
            Me.dtpTxStart.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.dtpTxStart.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
            Me.dtpTxStart.Location = New System.Drawing.Point(85, 14)
            Me.dtpTxStart.Name = "dtpTxStart"
            Me.dtpTxStart.Size = New System.Drawing.Size(200, 23)
            Me.dtpTxStart.TabIndex = 1
            '
            'lblTxStart
            '
            Me.lblTxStart.AutoSize = True
            Me.lblTxStart.Font = New System.Drawing.Font("Segoe UI Semibold", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.lblTxStart.Location = New System.Drawing.Point(15, 18)
            Me.lblTxStart.Name = "lblTxStart"
            Me.lblTxStart.Size = New System.Drawing.Size(63, 15)
            Me.lblTxStart.TabIndex = 0
            Me.lblTxStart.Text = "Date From:"
            '
            'tabReports
            '
            Me.tabReports.Controls.Add(Me.dgvReport)
            Me.tabReports.Controls.Add(Me.pnlReportSummary)
            Me.tabReports.Controls.Add(Me.pnlReportHeader)
            Me.tabReports.Location = New System.Drawing.Point(4, 34)
            Me.tabReports.Name = "tabReports"
            Me.tabReports.Padding = New System.Windows.Forms.Padding(3)
            Me.tabReports.Size = New System.Drawing.Size(1076, 608)
            Me.tabReports.TabIndex = 5
            Me.tabReports.Text = "📊 Reports & Analytics"
            Me.tabReports.UseVisualStyleBackColor = True
            '
            'dgvReport
            '
            Me.dgvReport.AllowUserToAddRows = False
            Me.dgvReport.AllowUserToDeleteRows = False
            Me.dgvReport.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill
            Me.dgvReport.BackgroundColor = System.Drawing.Color.White
            Me.dgvReport.BorderStyle = System.Windows.Forms.BorderStyle.None
            dataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
            dataGridViewCellStyle7.BackColor = System.Drawing.Color.FromArgb(CType(CType(30, Byte), Integer), CType(CType(94, Byte), Integer), CType(CType(58, Byte), Integer))
            dataGridViewCellStyle7.Font = New System.Drawing.Font("Segoe UI", 9.5!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            dataGridViewCellStyle7.ForeColor = System.Drawing.Color.White
            dataGridViewCellStyle7.SelectionBackColor = System.Drawing.SystemColors.Highlight
            dataGridViewCellStyle7.SelectionForeColor = System.Drawing.SystemColors.HighlightText
            dataGridViewCellStyle7.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
            Me.dgvReport.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle7
            Me.dgvReport.ColumnHeadersHeight = 32
            Me.dgvReport.Dock = System.Windows.Forms.DockStyle.Fill
            Me.dgvReport.EnableHeadersVisualStyles = False
            Me.dgvReport.Location = New System.Drawing.Point(3, 58)
            Me.dgvReport.MultiSelect = False
            Me.dgvReport.Name = "dgvReport"
            Me.dgvReport.ReadOnly = True
            Me.dgvReport.RowHeadersVisible = False
            Me.dgvReport.RowTemplate.Height = 26
            Me.dgvReport.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
            Me.dgvReport.Size = New System.Drawing.Size(1070, 507)
            Me.dgvReport.TabIndex = 1
            '
            'pnlReportSummary
            '
            Me.pnlReportSummary.BackColor = System.Drawing.Color.FromArgb(CType(CType(240, Byte), Integer), CType(CType(245, Byte), Integer), CType(CType(240, Byte), Integer))
            Me.pnlReportSummary.Controls.Add(Me.lblReportSummary)
            Me.pnlReportSummary.Dock = System.Windows.Forms.DockStyle.Bottom
            Me.pnlReportSummary.Location = New System.Drawing.Point(3, 565)
            Me.pnlReportSummary.Name = "pnlReportSummary"
            Me.pnlReportSummary.Size = New System.Drawing.Size(1070, 40)
            Me.pnlReportSummary.TabIndex = 2
            '
            'lblReportSummary
            '
            Me.lblReportSummary.Dock = System.Windows.Forms.DockStyle.Fill
            Me.lblReportSummary.Font = New System.Drawing.Font("Segoe UI Semibold", 9.5!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.lblReportSummary.ForeColor = System.Drawing.Color.FromArgb(CType(CType(30, Byte), Integer), CType(CType(94, Byte), Integer), CType(CType(58, Byte), Integer))
            Me.lblReportSummary.Location = New System.Drawing.Point(0, 0)
            Me.lblReportSummary.Name = "lblReportSummary"
            Me.lblReportSummary.Padding = New System.Windows.Forms.Padding(10, 0, 0, 0)
            Me.lblReportSummary.Size = New System.Drawing.Size(1070, 40)
            Me.lblReportSummary.TabIndex = 0
            Me.lblReportSummary.Text = "Report ready."
            Me.lblReportSummary.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
            '
            'pnlReportHeader
            '
            Me.pnlReportHeader.BackColor = System.Drawing.Color.FromArgb(CType(CType(245, Byte), Integer), CType(CType(248, Byte), Integer), CType(CType(245, Byte), Integer))
            Me.pnlReportHeader.Controls.Add(Me.btnPrintReport)
            Me.pnlReportHeader.Controls.Add(Me.btnExportCsv)
            Me.pnlReportHeader.Controls.Add(Me.btnGenerateReport)
            Me.pnlReportHeader.Controls.Add(Me.dtpReportDate)
            Me.pnlReportHeader.Controls.Add(Me.lblReportDate)
            Me.pnlReportHeader.Controls.Add(Me.cboReportType)
            Me.pnlReportHeader.Controls.Add(Me.lblReportType)
            Me.pnlReportHeader.Dock = System.Windows.Forms.DockStyle.Top
            Me.pnlReportHeader.Location = New System.Drawing.Point(3, 3)
            Me.pnlReportHeader.Name = "pnlReportHeader"
            Me.pnlReportHeader.Size = New System.Drawing.Size(1070, 55)
            Me.pnlReportHeader.TabIndex = 0
            '
            'btnPrintReport
            '
            Me.btnPrintReport.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.btnPrintReport.BackColor = System.Drawing.Color.FromArgb(CType(CType(67, Byte), Integer), CType(CType(160, Byte), Integer), CType(CType(71, Byte), Integer))
            Me.btnPrintReport.Cursor = System.Windows.Forms.Cursors.Hand
            Me.btnPrintReport.FlatAppearance.BorderSize = 0
            Me.btnPrintReport.FlatStyle = System.Windows.Forms.FlatStyle.Flat
            Me.btnPrintReport.Font = New System.Drawing.Font("Segoe UI Semibold", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.btnPrintReport.ForeColor = System.Drawing.Color.White
            Me.btnPrintReport.Location = New System.Drawing.Point(950, 12)
            Me.btnPrintReport.Name = "btnPrintReport"
            Me.btnPrintReport.Size = New System.Drawing.Size(105, 30)
            Me.btnPrintReport.TabIndex = 6
            Me.btnPrintReport.Text = "🖨 Print"
            Me.btnPrintReport.UseVisualStyleBackColor = False
            '
            'btnExportCsv
            '
            Me.btnExportCsv.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.btnExportCsv.BackColor = System.Drawing.Color.FromArgb(CType(CType(30, Byte), Integer), CType(CType(94, Byte), Integer), CType(CType(58, Byte), Integer))
            Me.btnExportCsv.Cursor = System.Windows.Forms.Cursors.Hand
            Me.btnExportCsv.FlatAppearance.BorderSize = 0
            Me.btnExportCsv.FlatStyle = System.Windows.Forms.FlatStyle.Flat
            Me.btnExportCsv.Font = New System.Drawing.Font("Segoe UI Semibold", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.btnExportCsv.ForeColor = System.Drawing.Color.White
            Me.btnExportCsv.Location = New System.Drawing.Point(825, 12)
            Me.btnExportCsv.Name = "btnExportCsv"
            Me.btnExportCsv.Size = New System.Drawing.Size(115, 30)
            Me.btnExportCsv.TabIndex = 5
            Me.btnExportCsv.Text = "📁 Export CSV"
            Me.btnExportCsv.UseVisualStyleBackColor = False
            '
            'btnGenerateReport
            '
            Me.btnGenerateReport.BackColor = System.Drawing.Color.FromArgb(CType(CType(46, Byte), Integer), CType(CType(125, Byte), Integer), CType(CType(50, Byte), Integer))
            Me.btnGenerateReport.Cursor = System.Windows.Forms.Cursors.Hand
            Me.btnGenerateReport.FlatAppearance.BorderSize = 0
            Me.btnGenerateReport.FlatStyle = System.Windows.Forms.FlatStyle.Flat
            Me.btnGenerateReport.Font = New System.Drawing.Font("Segoe UI Semibold", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.btnGenerateReport.ForeColor = System.Drawing.Color.White
            Me.btnGenerateReport.Location = New System.Drawing.Point(585, 12)
            Me.btnGenerateReport.Name = "btnGenerateReport"
            Me.btnGenerateReport.Size = New System.Drawing.Size(100, 30)
            Me.btnGenerateReport.TabIndex = 4
            Me.btnGenerateReport.Text = "Generate"
            Me.btnGenerateReport.UseVisualStyleBackColor = False
            '
            'dtpReportDate
            '
            Me.dtpReportDate.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.dtpReportDate.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
            Me.dtpReportDate.Location = New System.Drawing.Point(420, 16)
            Me.dtpReportDate.Name = "dtpReportDate"
            Me.dtpReportDate.Size = New System.Drawing.Size(145, 23)
            Me.dtpReportDate.TabIndex = 3
            '
            'lblReportDate
            '
            Me.lblReportDate.AutoSize = True
            Me.lblReportDate.Font = New System.Drawing.Font("Segoe UI Semibold", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.lblReportDate.Location = New System.Drawing.Point(375, 20)
            Me.lblReportDate.Name = "lblReportDate"
            Me.lblReportDate.Size = New System.Drawing.Size(35, 15)
            Me.lblReportDate.TabIndex = 2
            Me.lblReportDate.Text = "Date:"
            '
            'cboReportType
            '
            Me.cboReportType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
            Me.cboReportType.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.cboReportType.FormattingEnabled = True
            Me.cboReportType.Location = New System.Drawing.Point(105, 16)
            Me.cboReportType.Name = "cboReportType"
            Me.cboReportType.Size = New System.Drawing.Size(250, 23)
            Me.cboReportType.TabIndex = 1
            '
            'lblReportType
            '
            Me.lblReportType.AutoSize = True
            Me.lblReportType.Font = New System.Drawing.Font("Segoe UI Semibold", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.lblReportType.Location = New System.Drawing.Point(15, 20)
            Me.lblReportType.Name = "lblReportType"
            Me.lblReportType.Size = New System.Drawing.Size(74, 15)
            Me.lblReportType.TabIndex = 0
            Me.lblReportType.Text = "Select Report:"
            '
            'OwnerDashboardForm
            '
            Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 15.0!)
            Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
            Me.ClientSize = New System.Drawing.Size(1084, 711)
            Me.Controls.Add(Me.tabOwner)
            Me.Controls.Add(Me.pnlHeader)
            Me.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.MinimumSize = New System.Drawing.Size(1000, 680)
            Me.Name = "OwnerDashboardForm"
            Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
            Me.Text = "Store Owner Administration - Mini-Grocery Store"
            Me.pnlHeader.ResumeLayout(False)
            Me.pnlHeader.PerformLayout()
            Me.tabOwner.ResumeLayout(False)
            Me.tabOverview.ResumeLayout(False)
            CType(Me.dgvOverviewRecent, System.ComponentModel.ISupportInitialize).EndInit()
            Me.pnlOverviewRecentTitle.ResumeLayout(False)
            Me.pnlOverviewRecentTitle.PerformLayout()
            Me.pnlCards.ResumeLayout(False)
            Me.pnlCard4.ResumeLayout(False)
            Me.pnlCard3.ResumeLayout(False)
            Me.pnlCard2.ResumeLayout(False)
            Me.pnlCard1.ResumeLayout(False)
            Me.tabStaff.ResumeLayout(False)
            CType(Me.dgvStaff, System.ComponentModel.ISupportInitialize).EndInit()
            Me.grpStaffEdit.ResumeLayout(False)
            Me.grpStaffEdit.PerformLayout()
            Me.tabProducts.ResumeLayout(False)
            Me.splitProducts.Panel1.ResumeLayout(False)
            Me.splitProducts.Panel2.ResumeLayout(False)
            CType(Me.splitProducts, System.ComponentModel.ISupportInitialize).EndInit()
            Me.splitProducts.ResumeLayout(False)
            CType(Me.dgvProducts, System.ComponentModel.ISupportInitialize).EndInit()
            Me.pnlProdSearch.ResumeLayout(False)
            Me.pnlProdSearch.PerformLayout()
            Me.grpProdDetail.ResumeLayout(False)
            Me.grpProdDetail.PerformLayout()
            CType(Me.numProdReorder, System.ComponentModel.ISupportInitialize).EndInit()
            CType(Me.numProdStock, System.ComponentModel.ISupportInitialize).EndInit()
            Me.tabMasterData.ResumeLayout(False)
            Me.splitMaster.Panel1.ResumeLayout(False)
            Me.splitMaster.Panel2.ResumeLayout(False)
            CType(Me.splitMaster, System.ComponentModel.ISupportInitialize).EndInit()
            Me.splitMaster.ResumeLayout(False)
            Me.grpCategories.ResumeLayout(False)
            CType(Me.dgvCategories, System.ComponentModel.ISupportInitialize).EndInit()
            Me.pnlCatActions.ResumeLayout(False)
            Me.pnlCatActions.PerformLayout()
            Me.grpSuppliers.ResumeLayout(False)
            CType(Me.dgvSuppliers, System.ComponentModel.ISupportInitialize).EndInit()
            Me.pnlSupActions.ResumeLayout(False)
            Me.pnlSupActions.PerformLayout()
            Me.tabTransactions.ResumeLayout(False)
            CType(Me.dgvTransactions, System.ComponentModel.ISupportInitialize).EndInit()
            Me.pnlTxFilter.ResumeLayout(False)
            Me.pnlTxFilter.PerformLayout()
            Me.tabReports.ResumeLayout(False)
            CType(Me.dgvReport, System.ComponentModel.ISupportInitialize).EndInit()
            Me.pnlReportSummary.ResumeLayout(False)
            Me.pnlReportHeader.ResumeLayout(False)
            Me.pnlReportHeader.PerformLayout()
            Me.ResumeLayout(False)

        End Sub

        Friend WithEvents pnlHeader As System.Windows.Forms.Panel
        Friend WithEvents lblAppTitle As System.Windows.Forms.Label
        Friend WithEvents lblOwnerInfo As System.Windows.Forms.Label
        Friend WithEvents btnLogout As System.Windows.Forms.Button
        Friend WithEvents tabOwner As System.Windows.Forms.TabControl
        Friend WithEvents tabOverview As System.Windows.Forms.TabPage
        Friend WithEvents pnlCards As System.Windows.Forms.Panel
        Friend WithEvents pnlCard1 As System.Windows.Forms.Panel
        Friend WithEvents lblCard1Title As System.Windows.Forms.Label
        Friend WithEvents lblCard1Val As System.Windows.Forms.Label
        Friend WithEvents pnlCard2 As System.Windows.Forms.Panel
        Friend WithEvents lblCard2Title As System.Windows.Forms.Label
        Friend WithEvents lblCard2Val As System.Windows.Forms.Label
        Friend WithEvents pnlCard3 As System.Windows.Forms.Panel
        Friend WithEvents lblCard3Title As System.Windows.Forms.Label
        Friend WithEvents lblCard3Val As System.Windows.Forms.Label
        Friend WithEvents pnlCard4 As System.Windows.Forms.Panel
        Friend WithEvents lblCard4Title As System.Windows.Forms.Label
        Friend WithEvents lblCard4Val As System.Windows.Forms.Label
        Friend WithEvents pnlOverviewRecentTitle As System.Windows.Forms.Panel
        Friend WithEvents lblRecentTitle As System.Windows.Forms.Label
        Friend WithEvents dgvOverviewRecent As System.Windows.Forms.DataGridView
        Friend WithEvents colOvReceipt As System.Windows.Forms.DataGridViewTextBoxColumn
        Friend WithEvents colOvTime As System.Windows.Forms.DataGridViewTextBoxColumn
        Friend WithEvents colOvCashier As System.Windows.Forms.DataGridViewTextBoxColumn
        Friend WithEvents colOvTotal As System.Windows.Forms.DataGridViewTextBoxColumn
        Friend WithEvents tabStaff As System.Windows.Forms.TabPage
        Friend WithEvents dgvStaff As System.Windows.Forms.DataGridView
        Friend WithEvents colStaffId As System.Windows.Forms.DataGridViewTextBoxColumn
        Friend WithEvents colStaffUser As System.Windows.Forms.DataGridViewTextBoxColumn
        Friend WithEvents colStaffName As System.Windows.Forms.DataGridViewTextBoxColumn
        Friend WithEvents colStaffRole As System.Windows.Forms.DataGridViewTextBoxColumn
        Friend WithEvents colStaffStatus As System.Windows.Forms.DataGridViewTextBoxColumn
        Friend WithEvents colStaffCreated As System.Windows.Forms.DataGridViewTextBoxColumn
        Friend WithEvents grpStaffEdit As System.Windows.Forms.GroupBox
        Friend WithEvents lblStaffUsername As System.Windows.Forms.Label
        Friend WithEvents txtStaffUsername As System.Windows.Forms.TextBox
        Friend WithEvents lblStaffFullName As System.Windows.Forms.Label
        Friend WithEvents txtStaffName As System.Windows.Forms.TextBox
        Friend WithEvents lblStaffRole As System.Windows.Forms.Label
        Friend WithEvents cboStaffRole As System.Windows.Forms.ComboBox
        Friend WithEvents lblStaffPass As System.Windows.Forms.Label
        Friend WithEvents txtStaffPassword As System.Windows.Forms.TextBox
        Friend WithEvents btnStaffNew As System.Windows.Forms.Button
        Friend WithEvents btnStaffSave As System.Windows.Forms.Button
        Friend WithEvents btnStaffResetPass As System.Windows.Forms.Button
        Friend WithEvents btnStaffToggleActive As System.Windows.Forms.Button
        Friend WithEvents lblStaffMsg As System.Windows.Forms.Label
        Friend WithEvents tabProducts As System.Windows.Forms.TabPage
        Friend WithEvents splitProducts As System.Windows.Forms.SplitContainer
        Friend WithEvents pnlProdSearch As System.Windows.Forms.Panel
        Friend WithEvents lblProdSearch As System.Windows.Forms.Label
        Friend WithEvents txtProdSearch As System.Windows.Forms.TextBox
        Friend WithEvents cboProdCatFilter As System.Windows.Forms.ComboBox
        Friend WithEvents dgvProducts As System.Windows.Forms.DataGridView
        Friend WithEvents colPrCode As System.Windows.Forms.DataGridViewTextBoxColumn
        Friend WithEvents colPrName As System.Windows.Forms.DataGridViewTextBoxColumn
        Friend WithEvents colPrCat As System.Windows.Forms.DataGridViewTextBoxColumn
        Friend WithEvents colPrPrice As System.Windows.Forms.DataGridViewTextBoxColumn
        Friend WithEvents colPrCost As System.Windows.Forms.DataGridViewTextBoxColumn
        Friend WithEvents colPrStock As System.Windows.Forms.DataGridViewTextBoxColumn
        Friend WithEvents colPrStatus As System.Windows.Forms.DataGridViewTextBoxColumn
        Friend WithEvents grpProdDetail As System.Windows.Forms.GroupBox
        Friend WithEvents lblProdCode As System.Windows.Forms.Label
        Friend WithEvents txtProdCode As System.Windows.Forms.TextBox
        Friend WithEvents lblProdName As System.Windows.Forms.Label
        Friend WithEvents txtProdName As System.Windows.Forms.TextBox
        Friend WithEvents lblProdCat As System.Windows.Forms.Label
        Friend WithEvents cboProdCategory As System.Windows.Forms.ComboBox
        Friend WithEvents lblProdSupplier As System.Windows.Forms.Label
        Friend WithEvents cboProdSupplier As System.Windows.Forms.ComboBox
        Friend WithEvents lblProdPrice As System.Windows.Forms.Label
        Friend WithEvents txtProdPrice As System.Windows.Forms.TextBox
        Friend WithEvents lblProdCost As System.Windows.Forms.Label
        Friend WithEvents txtProdCost As System.Windows.Forms.TextBox
        Friend WithEvents lblProdStock As System.Windows.Forms.Label
        Friend WithEvents numProdStock As System.Windows.Forms.NumericUpDown
        Friend WithEvents lblProdReorder As System.Windows.Forms.Label
        Friend WithEvents numProdReorder As System.Windows.Forms.NumericUpDown
        Friend WithEvents lblProdExpiry As System.Windows.Forms.Label
        Friend WithEvents dtpProdExpiry As System.Windows.Forms.DateTimePicker
        Friend WithEvents chkProdActive As System.Windows.Forms.CheckBox
        Friend WithEvents btnProdNew As System.Windows.Forms.Button
        Friend WithEvents btnProdSave As System.Windows.Forms.Button
        Friend WithEvents btnProdDelete As System.Windows.Forms.Button
        Friend WithEvents lblProdMsg As System.Windows.Forms.Label
        Friend WithEvents tabMasterData As System.Windows.Forms.TabPage
        Friend WithEvents splitMaster As System.Windows.Forms.SplitContainer
        Friend WithEvents grpCategories As System.Windows.Forms.GroupBox
        Friend WithEvents dgvCategories As System.Windows.Forms.DataGridView
        Friend WithEvents colCatId As System.Windows.Forms.DataGridViewTextBoxColumn
        Friend WithEvents colCatTitle As System.Windows.Forms.DataGridViewTextBoxColumn
        Friend WithEvents pnlCatActions As System.Windows.Forms.Panel
        Friend WithEvents lblCatName As System.Windows.Forms.Label
        Friend WithEvents txtCatName As System.Windows.Forms.TextBox
        Friend WithEvents btnCatSave As System.Windows.Forms.Button
        Friend WithEvents btnCatDelete As System.Windows.Forms.Button
        Friend WithEvents grpSuppliers As System.Windows.Forms.GroupBox
        Friend WithEvents dgvSuppliers As System.Windows.Forms.DataGridView
        Friend WithEvents colSupId As System.Windows.Forms.DataGridViewTextBoxColumn
        Friend WithEvents colSupName As System.Windows.Forms.DataGridViewTextBoxColumn
        Friend WithEvents colSupContact As System.Windows.Forms.DataGridViewTextBoxColumn
        Friend WithEvents colSupAddr As System.Windows.Forms.DataGridViewTextBoxColumn
        Friend WithEvents pnlSupActions As System.Windows.Forms.Panel
        Friend WithEvents lblSupName As System.Windows.Forms.Label
        Friend WithEvents txtSupName As System.Windows.Forms.TextBox
        Friend WithEvents lblSupContact As System.Windows.Forms.Label
        Friend WithEvents txtSupContact As System.Windows.Forms.TextBox
        Friend WithEvents lblSupAddr As System.Windows.Forms.Label
        Friend WithEvents txtSupAddr As System.Windows.Forms.TextBox
        Friend WithEvents btnSupNew As System.Windows.Forms.Button
        Friend WithEvents btnSupSave As System.Windows.Forms.Button
        Friend WithEvents btnSupDelete As System.Windows.Forms.Button
        Friend WithEvents tabTransactions As System.Windows.Forms.TabPage
        Friend WithEvents pnlTxFilter As System.Windows.Forms.Panel
        Friend WithEvents lblTxStart As System.Windows.Forms.Label
        Friend WithEvents dtpTxStart As System.Windows.Forms.DateTimePicker
        Friend WithEvents lblTxEnd As System.Windows.Forms.Label
        Friend WithEvents dtpTxEnd As System.Windows.Forms.DateTimePicker
        Friend WithEvents btnTxViewDetails As System.Windows.Forms.Button
        Friend WithEvents dgvTransactions As System.Windows.Forms.DataGridView
        Friend WithEvents colTxReceipt As System.Windows.Forms.DataGridViewTextBoxColumn
        Friend WithEvents colTxDate As System.Windows.Forms.DataGridViewTextBoxColumn
        Friend WithEvents colTxCashier As System.Windows.Forms.DataGridViewTextBoxColumn
        Friend WithEvents colTxCustomer As System.Windows.Forms.DataGridViewTextBoxColumn
        Friend WithEvents colTxSubtotal As System.Windows.Forms.DataGridViewTextBoxColumn
        Friend WithEvents colTxDiscount As System.Windows.Forms.DataGridViewTextBoxColumn
        Friend WithEvents colTxTotal As System.Windows.Forms.DataGridViewTextBoxColumn
        Friend WithEvents tabReports As System.Windows.Forms.TabPage
        Friend WithEvents pnlReportHeader As System.Windows.Forms.Panel
        Friend WithEvents lblReportType As System.Windows.Forms.Label
        Friend WithEvents cboReportType As System.Windows.Forms.ComboBox
        Friend WithEvents lblReportDate As System.Windows.Forms.Label
        Friend WithEvents dtpReportDate As System.Windows.Forms.DateTimePicker
        Friend WithEvents btnGenerateReport As System.Windows.Forms.Button
        Friend WithEvents btnExportCsv As System.Windows.Forms.Button
        Friend WithEvents btnPrintReport As System.Windows.Forms.Button
        Friend WithEvents dgvReport As System.Windows.Forms.DataGridView
        Friend WithEvents pnlReportSummary As System.Windows.Forms.Panel
        Friend WithEvents lblReportSummary As System.Windows.Forms.Label
    End Class
End Namespace


