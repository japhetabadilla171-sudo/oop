Namespace Forms
    <Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
    Partial Class CustomerDashboardForm
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
            Me.pnlHeader = New System.Windows.Forms.Panel()
            Me.btnLogout = New System.Windows.Forms.Button()
            Me.lblUserInfo = New System.Windows.Forms.Label()
            Me.lblAppTitle = New System.Windows.Forms.Label()
            Me.tabCustomer = New System.Windows.Forms.TabControl()
            Me.tabBrowse = New System.Windows.Forms.TabPage()
            Me.dgvProducts = New System.Windows.Forms.DataGridView()
            Me.colProdCode = New System.Windows.Forms.DataGridViewTextBoxColumn()
            Me.colProdName = New System.Windows.Forms.DataGridViewTextBoxColumn()
            Me.colProdCat = New System.Windows.Forms.DataGridViewTextBoxColumn()
            Me.colProdPrice = New System.Windows.Forms.DataGridViewTextBoxColumn()
            Me.colProdStatus = New System.Windows.Forms.DataGridViewTextBoxColumn()
            Me.pnlFilter = New System.Windows.Forms.Panel()
            Me.cboCategory = New System.Windows.Forms.ComboBox()
            Me.lblCategory = New System.Windows.Forms.Label()
            Me.txtSearch = New System.Windows.Forms.TextBox()
            Me.lblSearch = New System.Windows.Forms.Label()
            Me.tabHistory = New System.Windows.Forms.TabPage()
            Me.dgvHistory = New System.Windows.Forms.DataGridView()
            Me.colHistReceipt = New System.Windows.Forms.DataGridViewTextBoxColumn()
            Me.colHistDate = New System.Windows.Forms.DataGridViewTextBoxColumn()
            Me.colHistCashier = New System.Windows.Forms.DataGridViewTextBoxColumn()
            Me.colHistSubtotal = New System.Windows.Forms.DataGridViewTextBoxColumn()
            Me.colHistDiscount = New System.Windows.Forms.DataGridViewTextBoxColumn()
            Me.colHistTotal = New System.Windows.Forms.DataGridViewTextBoxColumn()
            Me.pnlHistoryTop = New System.Windows.Forms.Panel()
            Me.btnViewReceiptDetails = New System.Windows.Forms.Button()
            Me.btnRefreshHistory = New System.Windows.Forms.Button()
            Me.tabProfile = New System.Windows.Forms.TabPage()
            Me.grpPassword = New System.Windows.Forms.GroupBox()
            Me.lblPassMsg = New System.Windows.Forms.Label()
            Me.btnChangePassword = New System.Windows.Forms.Button()
            Me.txtConfPass = New System.Windows.Forms.TextBox()
            Me.lblConfPass = New System.Windows.Forms.Label()
            Me.txtNewPass = New System.Windows.Forms.TextBox()
            Me.lblNewPass = New System.Windows.Forms.Label()
            Me.txtCurPass = New System.Windows.Forms.TextBox()
            Me.lblCurPass = New System.Windows.Forms.Label()
            Me.grpProfile = New System.Windows.Forms.GroupBox()
            Me.lblProfileMsg = New System.Windows.Forms.Label()
            Me.btnSaveProfile = New System.Windows.Forms.Button()
            Me.txtProfFullName = New System.Windows.Forms.TextBox()
            Me.lblProfFullName = New System.Windows.Forms.Label()
            Me.lblProfUsernameVal = New System.Windows.Forms.Label()
            Me.lblProfUsername = New System.Windows.Forms.Label()
            Me.pnlHeader.SuspendLayout()
            Me.tabCustomer.SuspendLayout()
            Me.tabBrowse.SuspendLayout()
            CType(Me.dgvProducts, System.ComponentModel.ISupportInitialize).BeginInit()
            Me.pnlFilter.SuspendLayout()
            Me.tabHistory.SuspendLayout()
            CType(Me.dgvHistory, System.ComponentModel.ISupportInitialize).BeginInit()
            Me.pnlHistoryTop.SuspendLayout()
            Me.tabProfile.SuspendLayout()
            Me.grpPassword.SuspendLayout()
            Me.grpProfile.SuspendLayout()
            Me.SuspendLayout()
            '
            'pnlHeader
            '
            Me.pnlHeader.BackColor = System.Drawing.Color.FromArgb(CType(CType(30, Byte), Integer), CType(CType(94, Byte), Integer), CType(CType(58, Byte), Integer))
            Me.pnlHeader.Controls.Add(Me.btnLogout)
            Me.pnlHeader.Controls.Add(Me.lblUserInfo)
            Me.pnlHeader.Controls.Add(Me.lblAppTitle)
            Me.pnlHeader.Dock = System.Windows.Forms.DockStyle.Top
            Me.pnlHeader.Location = New System.Drawing.Point(0, 0)
            Me.pnlHeader.Name = "pnlHeader"
            Me.pnlHeader.Size = New System.Drawing.Size(900, 65)
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
            Me.btnLogout.Location = New System.Drawing.Point(795, 16)
            Me.btnLogout.Name = "btnLogout"
            Me.btnLogout.Size = New System.Drawing.Size(90, 32)
            Me.btnLogout.TabIndex = 2
            Me.btnLogout.Text = "🚪 Logout"
            Me.btnLogout.UseVisualStyleBackColor = False
            '
            'lblUserInfo
            '
            Me.lblUserInfo.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.lblUserInfo.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.lblUserInfo.ForeColor = System.Drawing.Color.FromArgb(CType(CType(200, Byte), Integer), CType(CType(230, Byte), Integer), CType(CType(201, Byte), Integer))
            Me.lblUserInfo.Location = New System.Drawing.Point(440, 22)
            Me.lblUserInfo.Name = "lblUserInfo"
            Me.lblUserInfo.Size = New System.Drawing.Size(340, 20)
            Me.lblUserInfo.TabIndex = 1
            Me.lblUserInfo.Text = "Welcome, Customer"
            Me.lblUserInfo.TextAlign = System.Drawing.ContentAlignment.MiddleRight
            '
            'lblAppTitle
            '
            Me.lblAppTitle.AutoSize = True
            Me.lblAppTitle.Font = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.lblAppTitle.ForeColor = System.Drawing.Color.White
            Me.lblAppTitle.Location = New System.Drawing.Point(15, 20)
            Me.lblAppTitle.Name = "lblAppTitle"
            Me.lblAppTitle.Size = New System.Drawing.Size(329, 21)
            Me.lblAppTitle.TabIndex = 0
            Me.lblAppTitle.Text = "Mini-Grocery Store - Customer Portal"
            '
            'tabCustomer
            '
            Me.tabCustomer.Controls.Add(Me.tabBrowse)
            Me.tabCustomer.Controls.Add(Me.tabHistory)
            Me.tabCustomer.Controls.Add(Me.tabProfile)
            Me.tabCustomer.Dock = System.Windows.Forms.DockStyle.Fill
            Me.tabCustomer.Font = New System.Drawing.Font("Segoe UI", 9.5!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.tabCustomer.ItemSize = New System.Drawing.Size(140, 30)
            Me.tabCustomer.Location = New System.Drawing.Point(0, 65)
            Me.tabCustomer.Name = "tabCustomer"
            Me.tabCustomer.SelectedIndex = 0
            Me.tabCustomer.Size = New System.Drawing.Size(900, 535)
            Me.tabCustomer.TabIndex = 1
            '
            'tabBrowse
            '
            Me.tabBrowse.Controls.Add(Me.dgvProducts)
            Me.tabBrowse.Controls.Add(Me.pnlFilter)
            Me.tabBrowse.Location = New System.Drawing.Point(4, 34)
            Me.tabBrowse.Name = "tabBrowse"
            Me.tabBrowse.Padding = New System.Windows.Forms.Padding(3)
            Me.tabBrowse.Size = New System.Drawing.Size(892, 497)
            Me.tabBrowse.TabIndex = 0
            Me.tabBrowse.Text = "🔍 Browse Products"
            Me.tabBrowse.UseVisualStyleBackColor = True
            '
            'dgvProducts
            '
            Me.dgvProducts.AllowUserToAddRows = False
            Me.dgvProducts.AllowUserToDeleteRows = False
            Me.dgvProducts.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill
            Me.dgvProducts.BackgroundColor = System.Drawing.Color.White
            Me.dgvProducts.BorderStyle = System.Windows.Forms.BorderStyle.None
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(CType(CType(30, Byte), Integer), CType(CType(94, Byte), Integer), CType(CType(58, Byte), Integer))
            dataGridViewCellStyle1.Font = New System.Drawing.Font("Segoe UI", 9.5!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.White
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
            Me.dgvProducts.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1
            Me.dgvProducts.ColumnHeadersHeight = 32
            Me.dgvProducts.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.colProdCode, Me.colProdName, Me.colProdCat, Me.colProdPrice, Me.colProdStatus})
            Me.dgvProducts.Dock = System.Windows.Forms.DockStyle.Fill
            Me.dgvProducts.EnableHeadersVisualStyles = False
            Me.dgvProducts.Location = New System.Drawing.Point(3, 58)
            Me.dgvProducts.MultiSelect = False
            Me.dgvProducts.Name = "dgvProducts"
            Me.dgvProducts.ReadOnly = True
            Me.dgvProducts.RowHeadersVisible = False
            Me.dgvProducts.RowTemplate.Height = 26
            Me.dgvProducts.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
            Me.dgvProducts.Size = New System.Drawing.Size(886, 436)
            Me.dgvProducts.TabIndex = 1
            '
            'colProdCode
            '
            Me.colProdCode.FillWeight = 20.0!
            Me.colProdCode.HeaderText = "Item Code"
            Me.colProdCode.Name = "colProdCode"
            Me.colProdCode.ReadOnly = True
            '
            'colProdName
            '
            Me.colProdName.FillWeight = 40.0!
            Me.colProdName.HeaderText = "Product Name"
            Me.colProdName.Name = "colProdName"
            Me.colProdName.ReadOnly = True
            '
            'colProdCat
            '
            Me.colProdCat.FillWeight = 25.0!
            Me.colProdCat.HeaderText = "Category"
            Me.colProdCat.Name = "colProdCat"
            Me.colProdCat.ReadOnly = True
            '
            'colProdPrice
            '
            Me.colProdPrice.FillWeight = 18.0!
            Me.colProdPrice.HeaderText = "Price (PHP)"
            Me.colProdPrice.Name = "colProdPrice"
            Me.colProdPrice.ReadOnly = True
            '
            'colProdStatus
            '
            Me.colProdStatus.FillWeight = 18.0!
            Me.colProdStatus.HeaderText = "Availability"
            Me.colProdStatus.Name = "colProdStatus"
            Me.colProdStatus.ReadOnly = True
            '
            'pnlFilter
            '
            Me.pnlFilter.BackColor = System.Drawing.Color.FromArgb(CType(CType(245, Byte), Integer), CType(CType(248, Byte), Integer), CType(CType(245, Byte), Integer))
            Me.pnlFilter.Controls.Add(Me.cboCategory)
            Me.pnlFilter.Controls.Add(Me.lblCategory)
            Me.pnlFilter.Controls.Add(Me.txtSearch)
            Me.pnlFilter.Controls.Add(Me.lblSearch)
            Me.pnlFilter.Dock = System.Windows.Forms.DockStyle.Top
            Me.pnlFilter.Location = New System.Drawing.Point(3, 3)
            Me.pnlFilter.Name = "pnlFilter"
            Me.pnlFilter.Size = New System.Drawing.Size(886, 55)
            Me.pnlFilter.TabIndex = 0
            '
            'cboCategory
            '
            Me.cboCategory.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
            Me.cboCategory.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.cboCategory.FormattingEnabled = True
            Me.cboCategory.Location = New System.Drawing.Point(510, 15)
            Me.cboCategory.Name = "cboCategory"
            Me.cboCategory.Size = New System.Drawing.Size(361, 23)
            Me.cboCategory.TabIndex = 3
            '
            'lblCategory
            '
            Me.lblCategory.AutoSize = True
            Me.lblCategory.Font = New System.Drawing.Font("Segoe UI Semibold", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.lblCategory.Location = New System.Drawing.Point(440, 19)
            Me.lblCategory.Name = "lblCategory"
            Me.lblCategory.Size = New System.Drawing.Size(57, 15)
            Me.lblCategory.TabIndex = 2
            Me.lblCategory.Text = "Category:"
            '
            'txtSearch
            '
            Me.txtSearch.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.txtSearch.Location = New System.Drawing.Point(120, 15)
            Me.txtSearch.Name = "txtSearch"
            Me.txtSearch.MaxLength = 30
            Me.txtSearch.Size = New System.Drawing.Size(300, 23)
            Me.txtSearch.TabIndex = 1
            '
            'lblSearch
            '
            Me.lblSearch.AutoSize = True
            Me.lblSearch.Font = New System.Drawing.Font("Segoe UI Semibold", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.lblSearch.Location = New System.Drawing.Point(15, 19)
            Me.lblSearch.Name = "lblSearch"
            Me.lblSearch.Size = New System.Drawing.Size(94, 15)
            Me.lblSearch.TabIndex = 0
            Me.lblSearch.Text = "Search Product:"
            '
            'tabHistory
            '
            Me.tabHistory.Controls.Add(Me.dgvHistory)
            Me.tabHistory.Controls.Add(Me.pnlHistoryTop)
            Me.tabHistory.Location = New System.Drawing.Point(4, 34)
            Me.tabHistory.Name = "tabHistory"
            Me.tabHistory.Padding = New System.Windows.Forms.Padding(3)
            Me.tabHistory.Size = New System.Drawing.Size(892, 497)
            Me.tabHistory.TabIndex = 1
            Me.tabHistory.Text = "📋 My Purchase History"
            Me.tabHistory.UseVisualStyleBackColor = True
            '
            'dgvHistory
            '
            Me.dgvHistory.AllowUserToAddRows = False
            Me.dgvHistory.AllowUserToDeleteRows = False
            Me.dgvHistory.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill
            Me.dgvHistory.BackgroundColor = System.Drawing.Color.White
            Me.dgvHistory.BorderStyle = System.Windows.Forms.BorderStyle.None
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.FromArgb(CType(CType(30, Byte), Integer), CType(CType(94, Byte), Integer), CType(CType(58, Byte), Integer))
            dataGridViewCellStyle2.Font = New System.Drawing.Font("Segoe UI", 9.5!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.White
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
            Me.dgvHistory.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2
            Me.dgvHistory.ColumnHeadersHeight = 32
            Me.dgvHistory.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.colHistReceipt, Me.colHistDate, Me.colHistCashier, Me.colHistSubtotal, Me.colHistDiscount, Me.colHistTotal})
            Me.dgvHistory.Dock = System.Windows.Forms.DockStyle.Fill
            Me.dgvHistory.EnableHeadersVisualStyles = False
            Me.dgvHistory.Location = New System.Drawing.Point(3, 53)
            Me.dgvHistory.MultiSelect = False
            Me.dgvHistory.Name = "dgvHistory"
            Me.dgvHistory.ReadOnly = True
            Me.dgvHistory.RowHeadersVisible = False
            Me.dgvHistory.RowTemplate.Height = 26
            Me.dgvHistory.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
            Me.dgvHistory.Size = New System.Drawing.Size(886, 441)
            Me.dgvHistory.TabIndex = 1
            '
            'colHistReceipt
            '
            Me.colHistReceipt.FillWeight = 22.0!
            Me.colHistReceipt.HeaderText = "Receipt No"
            Me.colHistReceipt.Name = "colHistReceipt"
            Me.colHistReceipt.ReadOnly = True
            '
            'colHistDate
            '
            Me.colHistDate.FillWeight = 25.0!
            Me.colHistDate.HeaderText = "Date & Time"
            Me.colHistDate.Name = "colHistDate"
            Me.colHistDate.ReadOnly = True
            '
            'colHistCashier
            '
            Me.colHistCashier.FillWeight = 22.0!
            Me.colHistCashier.HeaderText = "Cashier"
            Me.colHistCashier.Name = "colHistCashier"
            Me.colHistCashier.ReadOnly = True
            '
            'colHistSubtotal
            '
            Me.colHistSubtotal.FillWeight = 16.0!
            Me.colHistSubtotal.HeaderText = "Subtotal (PHP)"
            Me.colHistSubtotal.Name = "colHistSubtotal"
            Me.colHistSubtotal.ReadOnly = True
            '
            'colHistDiscount
            '
            Me.colHistDiscount.FillWeight = 16.0!
            Me.colHistDiscount.HeaderText = "Discount (PHP)"
            Me.colHistDiscount.Name = "colHistDiscount"
            Me.colHistDiscount.ReadOnly = True
            '
            'colHistTotal
            '
            Me.colHistTotal.FillWeight = 18.0!
            Me.colHistTotal.HeaderText = "Net Total (PHP)"
            Me.colHistTotal.Name = "colHistTotal"
            Me.colHistTotal.ReadOnly = True
            '
            'pnlHistoryTop
            '
            Me.pnlHistoryTop.BackColor = System.Drawing.Color.FromArgb(CType(CType(245, Byte), Integer), CType(CType(248, Byte), Integer), CType(CType(245, Byte), Integer))
            Me.pnlHistoryTop.Controls.Add(Me.btnViewReceiptDetails)
            Me.pnlHistoryTop.Controls.Add(Me.btnRefreshHistory)
            Me.pnlHistoryTop.Dock = System.Windows.Forms.DockStyle.Top
            Me.pnlHistoryTop.Location = New System.Drawing.Point(3, 3)
            Me.pnlHistoryTop.Name = "pnlHistoryTop"
            Me.pnlHistoryTop.Size = New System.Drawing.Size(886, 50)
            Me.pnlHistoryTop.TabIndex = 0
            '
            'btnViewReceiptDetails
            '
            Me.btnViewReceiptDetails.BackColor = System.Drawing.Color.FromArgb(CType(CType(46, Byte), Integer), CType(CType(125, Byte), Integer), CType(CType(50, Byte), Integer))
            Me.btnViewReceiptDetails.Cursor = System.Windows.Forms.Cursors.Hand
            Me.btnViewReceiptDetails.FlatAppearance.BorderSize = 0
            Me.btnViewReceiptDetails.FlatStyle = System.Windows.Forms.FlatStyle.Flat
            Me.btnViewReceiptDetails.Font = New System.Drawing.Font("Segoe UI Semibold", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.btnViewReceiptDetails.ForeColor = System.Drawing.Color.White
            Me.btnViewReceiptDetails.Location = New System.Drawing.Point(135, 10)
            Me.btnViewReceiptDetails.Name = "btnViewReceiptDetails"
            Me.btnViewReceiptDetails.Size = New System.Drawing.Size(160, 30)
            Me.btnViewReceiptDetails.TabIndex = 1
            Me.btnViewReceiptDetails.Text = "📄 View Receipt Items"
            Me.btnViewReceiptDetails.UseVisualStyleBackColor = False
            '
            'btnRefreshHistory
            '
            Me.btnRefreshHistory.BackColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer))
            Me.btnRefreshHistory.Cursor = System.Windows.Forms.Cursors.Hand
            Me.btnRefreshHistory.FlatAppearance.BorderSize = 0
            Me.btnRefreshHistory.FlatStyle = System.Windows.Forms.FlatStyle.Flat
            Me.btnRefreshHistory.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.btnRefreshHistory.Location = New System.Drawing.Point(15, 10)
            Me.btnRefreshHistory.Name = "btnRefreshHistory"
            Me.btnRefreshHistory.Size = New System.Drawing.Size(105, 30)
            Me.btnRefreshHistory.TabIndex = 0
            Me.btnRefreshHistory.Text = "🔄 Refresh"
            Me.btnRefreshHistory.UseVisualStyleBackColor = False
            '
            'tabProfile
            '
            Me.tabProfile.Controls.Add(Me.grpPassword)
            Me.tabProfile.Controls.Add(Me.grpProfile)
            Me.tabProfile.Location = New System.Drawing.Point(4, 34)
            Me.tabProfile.Name = "tabProfile"
            Me.tabProfile.Padding = New System.Windows.Forms.Padding(20)
            Me.tabProfile.Size = New System.Drawing.Size(892, 497)
            Me.tabProfile.TabIndex = 2
            Me.tabProfile.Text = "👤 My Profile & Security"
            Me.tabProfile.UseVisualStyleBackColor = True
            '
            'grpPassword
            '
            Me.grpPassword.Controls.Add(Me.lblPassMsg)
            Me.grpPassword.Controls.Add(Me.btnChangePassword)
            Me.grpPassword.Controls.Add(Me.txtConfPass)
            Me.grpPassword.Controls.Add(Me.lblConfPass)
            Me.grpPassword.Controls.Add(Me.txtNewPass)
            Me.grpPassword.Controls.Add(Me.lblNewPass)
            Me.grpPassword.Controls.Add(Me.txtCurPass)
            Me.grpPassword.Controls.Add(Me.lblCurPass)
            Me.grpPassword.Font = New System.Drawing.Font("Segoe UI Semibold", 9.5!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.grpPassword.Location = New System.Drawing.Point(30, 200)
            Me.grpPassword.Name = "grpPassword"
            Me.grpPassword.Size = New System.Drawing.Size(550, 220)
            Me.grpPassword.TabIndex = 1
            Me.grpPassword.TabStop = False
            Me.grpPassword.Text = "Change Password"
            '
            'lblPassMsg
            '
            Me.lblPassMsg.Font = New System.Drawing.Font("Segoe UI", 8.5!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.lblPassMsg.Location = New System.Drawing.Point(25, 185)
            Me.lblPassMsg.Name = "lblPassMsg"
            Me.lblPassMsg.Size = New System.Drawing.Size(500, 20)
            Me.lblPassMsg.TabIndex = 7
            '
            'btnChangePassword
            '
            Me.btnChangePassword.BackColor = System.Drawing.Color.FromArgb(CType(CType(46, Byte), Integer), CType(CType(125, Byte), Integer), CType(CType(50, Byte), Integer))
            Me.btnChangePassword.Cursor = System.Windows.Forms.Cursors.Hand
            Me.btnChangePassword.FlatAppearance.BorderSize = 0
            Me.btnChangePassword.FlatStyle = System.Windows.Forms.FlatStyle.Flat
            Me.btnChangePassword.ForeColor = System.Drawing.Color.White
            Me.btnChangePassword.Location = New System.Drawing.Point(170, 145)
            Me.btnChangePassword.Name = "btnChangePassword"
            Me.btnChangePassword.Size = New System.Drawing.Size(160, 32)
            Me.btnChangePassword.TabIndex = 6
            Me.btnChangePassword.Text = "Update Password"
            Me.btnChangePassword.UseVisualStyleBackColor = False
            '
            'txtConfPass
            '
            Me.txtConfPass.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.txtConfPass.Location = New System.Drawing.Point(170, 106)
            Me.txtConfPass.Name = "txtConfPass"
            Me.txtConfPass.MaxLength = 50
            Me.txtConfPass.Size = New System.Drawing.Size(260, 23)
            Me.txtConfPass.TabIndex = 5
            Me.txtConfPass.UseSystemPasswordChar = True
            '
            'lblConfPass
            '
            Me.lblConfPass.AutoSize = True
            Me.lblConfPass.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.lblConfPass.Location = New System.Drawing.Point(25, 110)
            Me.lblConfPass.Name = "lblConfPass"
            Me.lblConfPass.Size = New System.Drawing.Size(107, 15)
            Me.lblConfPass.TabIndex = 4
            Me.lblConfPass.Text = "Confirm Password:"
            '
            'txtNewPass
            '
            Me.txtNewPass.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.txtNewPass.Location = New System.Drawing.Point(170, 71)
            Me.txtNewPass.Name = "txtNewPass"
            Me.txtNewPass.MaxLength = 50
            Me.txtNewPass.Size = New System.Drawing.Size(260, 23)
            Me.txtNewPass.TabIndex = 3
            Me.txtNewPass.UseSystemPasswordChar = True
            '
            'lblNewPass
            '
            Me.lblNewPass.AutoSize = True
            Me.lblNewPass.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.lblNewPass.Location = New System.Drawing.Point(25, 75)
            Me.lblNewPass.Name = "lblNewPass"
            Me.lblNewPass.Size = New System.Drawing.Size(87, 15)
            Me.lblNewPass.TabIndex = 2
            Me.lblNewPass.Text = "New Password:"
            '
            'txtCurPass
            '
            Me.txtCurPass.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.txtCurPass.Location = New System.Drawing.Point(170, 36)
            Me.txtCurPass.Name = "txtCurPass"
            Me.txtCurPass.MaxLength = 50
            Me.txtCurPass.Size = New System.Drawing.Size(260, 23)
            Me.txtCurPass.TabIndex = 1
            Me.txtCurPass.UseSystemPasswordChar = True
            '
            'lblCurPass
            '
            Me.lblCurPass.AutoSize = True
            Me.lblCurPass.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.lblCurPass.Location = New System.Drawing.Point(25, 40)
            Me.lblCurPass.Name = "lblCurPass"
            Me.lblCurPass.Size = New System.Drawing.Size(103, 15)
            Me.lblCurPass.TabIndex = 0
            Me.lblCurPass.Text = "Current Password:"
            '
            'grpProfile
            '
            Me.grpProfile.Controls.Add(Me.lblProfileMsg)
            Me.grpProfile.Controls.Add(Me.btnSaveProfile)
            Me.grpProfile.Controls.Add(Me.txtProfFullName)
            Me.grpProfile.Controls.Add(Me.lblProfFullName)
            Me.grpProfile.Controls.Add(Me.lblProfUsernameVal)
            Me.grpProfile.Controls.Add(Me.lblProfUsername)
            Me.grpProfile.Font = New System.Drawing.Font("Segoe UI Semibold", 9.5!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.grpProfile.Location = New System.Drawing.Point(30, 20)
            Me.grpProfile.Name = "grpProfile"
            Me.grpProfile.Size = New System.Drawing.Size(550, 160)
            Me.grpProfile.TabIndex = 0
            Me.grpProfile.TabStop = False
            Me.grpProfile.Text = "Personal Information"
            '
            'lblProfileMsg
            '
            Me.lblProfileMsg.Font = New System.Drawing.Font("Segoe UI", 8.5!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.lblProfileMsg.Location = New System.Drawing.Point(25, 125)
            Me.lblProfileMsg.Name = "lblProfileMsg"
            Me.lblProfileMsg.Size = New System.Drawing.Size(500, 20)
            Me.lblProfileMsg.TabIndex = 5
            '
            'btnSaveProfile
            '
            Me.btnSaveProfile.BackColor = System.Drawing.Color.FromArgb(CType(CType(46, Byte), Integer), CType(CType(125, Byte), Integer), CType(CType(50, Byte), Integer))
            Me.btnSaveProfile.Cursor = System.Windows.Forms.Cursors.Hand
            Me.btnSaveProfile.FlatAppearance.BorderSize = 0
            Me.btnSaveProfile.FlatStyle = System.Windows.Forms.FlatStyle.Flat
            Me.btnSaveProfile.ForeColor = System.Drawing.Color.White
            Me.btnSaveProfile.Location = New System.Drawing.Point(170, 85)
            Me.btnSaveProfile.Name = "btnSaveProfile"
            Me.btnSaveProfile.Size = New System.Drawing.Size(160, 32)
            Me.btnSaveProfile.TabIndex = 4
            Me.btnSaveProfile.Text = "Save Profile"
            Me.btnSaveProfile.UseVisualStyleBackColor = False
            '
            'txtProfFullName
            '
            Me.txtProfFullName.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.txtProfFullName.Location = New System.Drawing.Point(170, 50)
            Me.txtProfFullName.Name = "txtProfFullName"
            Me.txtProfFullName.MaxLength = 50
            Me.txtProfFullName.Size = New System.Drawing.Size(260, 23)
            Me.txtProfFullName.TabIndex = 3
            '
            'lblProfFullName
            '
            Me.lblProfFullName.AutoSize = True
            Me.lblProfFullName.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.lblProfFullName.Location = New System.Drawing.Point(25, 54)
            Me.lblProfFullName.Name = "lblProfFullName"
            Me.lblProfFullName.Size = New System.Drawing.Size(64, 15)
            Me.lblProfFullName.TabIndex = 2
            Me.lblProfFullName.Text = "Full Name:"
            '
            'lblProfUsernameVal
            '
            Me.lblProfUsernameVal.AutoSize = True
            Me.lblProfUsernameVal.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.lblProfUsernameVal.Location = New System.Drawing.Point(170, 28)
            Me.lblProfUsernameVal.Name = "lblProfUsernameVal"
            Me.lblProfUsernameVal.Size = New System.Drawing.Size(61, 15)
            Me.lblProfUsernameVal.TabIndex = 1
            Me.lblProfUsernameVal.Text = "username"
            '
            'lblProfUsername
            '
            Me.lblProfUsername.AutoSize = True
            Me.lblProfUsername.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.lblProfUsername.Location = New System.Drawing.Point(25, 28)
            Me.lblProfUsername.Name = "lblProfUsername"
            Me.lblProfUsername.Size = New System.Drawing.Size(63, 15)
            Me.lblProfUsername.TabIndex = 0
            Me.lblProfUsername.Text = "Username:"
            '
            'CustomerDashboardForm
            '
            Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 15.0!)
            Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
            Me.ClientSize = New System.Drawing.Size(900, 600)
            Me.Controls.Add(Me.tabCustomer)
            Me.Controls.Add(Me.pnlHeader)
            Me.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.MinimumSize = New System.Drawing.Size(850, 550)
            Me.Name = "CustomerDashboardForm"
            Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
            Me.Text = "Customer Portal - Mini-Grocery Store"
            Me.pnlHeader.ResumeLayout(False)
            Me.pnlHeader.PerformLayout()
            Me.tabCustomer.ResumeLayout(False)
            Me.tabBrowse.ResumeLayout(False)
            CType(Me.dgvProducts, System.ComponentModel.ISupportInitialize).EndInit()
            Me.pnlFilter.ResumeLayout(False)
            Me.pnlFilter.PerformLayout()
            Me.tabHistory.ResumeLayout(False)
            CType(Me.dgvHistory, System.ComponentModel.ISupportInitialize).EndInit()
            Me.pnlHistoryTop.ResumeLayout(False)
            Me.tabProfile.ResumeLayout(False)
            Me.grpPassword.ResumeLayout(False)
            Me.grpPassword.PerformLayout()
            Me.grpProfile.ResumeLayout(False)
            Me.grpProfile.PerformLayout()
            Me.ResumeLayout(False)

        End Sub

        Friend WithEvents pnlHeader As System.Windows.Forms.Panel
        Friend WithEvents lblAppTitle As System.Windows.Forms.Label
        Friend WithEvents lblUserInfo As System.Windows.Forms.Label
        Friend WithEvents btnLogout As System.Windows.Forms.Button
        Friend WithEvents tabCustomer As System.Windows.Forms.TabControl
        Friend WithEvents tabBrowse As System.Windows.Forms.TabPage
        Friend WithEvents pnlFilter As System.Windows.Forms.Panel
        Friend WithEvents lblSearch As System.Windows.Forms.Label
        Friend WithEvents txtSearch As System.Windows.Forms.TextBox
        Friend WithEvents lblCategory As System.Windows.Forms.Label
        Friend WithEvents cboCategory As System.Windows.Forms.ComboBox
        Friend WithEvents dgvProducts As System.Windows.Forms.DataGridView
        Friend WithEvents colProdCode As System.Windows.Forms.DataGridViewTextBoxColumn
        Friend WithEvents colProdName As System.Windows.Forms.DataGridViewTextBoxColumn
        Friend WithEvents colProdCat As System.Windows.Forms.DataGridViewTextBoxColumn
        Friend ItemPrice As System.Windows.Forms.DataGridViewTextBoxColumn
        Friend WithEvents colProdPrice As System.Windows.Forms.DataGridViewTextBoxColumn
        Friend WithEvents colProdStatus As System.Windows.Forms.DataGridViewTextBoxColumn
        Friend WithEvents tabHistory As System.Windows.Forms.TabPage
        Friend WithEvents pnlHistoryTop As System.Windows.Forms.Panel
        Friend WithEvents btnRefreshHistory As System.Windows.Forms.Button
        Friend WithEvents btnViewReceiptDetails As System.Windows.Forms.Button
        Friend WithEvents dgvHistory As System.Windows.Forms.DataGridView
        Friend WithEvents colHistReceipt As System.Windows.Forms.DataGridViewTextBoxColumn
        Friend WithEvents colHistDate As System.Windows.Forms.DataGridViewTextBoxColumn
        Friend WithEvents colHistCashier As System.Windows.Forms.DataGridViewTextBoxColumn
        Friend WithEvents colHistSubtotal As System.Windows.Forms.DataGridViewTextBoxColumn
        Friend WithEvents colHistDiscount As System.Windows.Forms.DataGridViewTextBoxColumn
        Friend WithEvents colHistTotal As System.Windows.Forms.DataGridViewTextBoxColumn
        Friend WithEvents tabProfile As System.Windows.Forms.TabPage
        Friend WithEvents grpProfile As System.Windows.Forms.GroupBox
        Friend WithEvents lblProfUsername As System.Windows.Forms.Label
        Friend WithEvents lblProfUsernameVal As System.Windows.Forms.Label
        Friend WithEvents lblProfFullName As System.Windows.Forms.Label
        Friend WithEvents txtProfFullName As System.Windows.Forms.TextBox
        Friend WithEvents btnSaveProfile As System.Windows.Forms.Button
        Friend WithEvents lblProfileMsg As System.Windows.Forms.Label
        Friend WithEvents grpPassword As System.Windows.Forms.GroupBox
        Friend WithEvents lblCurPass As System.Windows.Forms.Label
        Friend WithEvents txtCurPass As System.Windows.Forms.TextBox
        Friend WithEvents lblNewPass As System.Windows.Forms.Label
        Friend WithEvents txtNewPass As System.Windows.Forms.TextBox
        Friend WithEvents lblConfPass As System.Windows.Forms.Label
        Friend WithEvents txtConfPass As System.Windows.Forms.TextBox
        Friend WithEvents btnChangePassword As System.Windows.Forms.Button
        Friend WithEvents lblPassMsg As System.Windows.Forms.Label
    End Class
End Namespace

