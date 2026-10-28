Namespace Forms
    <Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
    Partial Class CashierDashboardForm
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
            Me.lblCashierInfo = New System.Windows.Forms.Label()
            Me.lblAppTitle = New System.Windows.Forms.Label()
            Me.tabCashier = New System.Windows.Forms.TabControl()
            Me.tabPos = New System.Windows.Forms.TabPage()
            Me.splitPos = New System.Windows.Forms.SplitContainer()
            Me.dgvPosProducts = New System.Windows.Forms.DataGridView()
            Me.colPosCode = New System.Windows.Forms.DataGridViewTextBoxColumn()
            Me.colPosName = New System.Windows.Forms.DataGridViewTextBoxColumn()
            Me.colPosPrice = New System.Windows.Forms.DataGridViewTextBoxColumn()
            Me.colPosStock = New System.Windows.Forms.DataGridViewTextBoxColumn()
            Me.pnlAddToCart = New System.Windows.Forms.Panel()
            Me.btnAddToCart = New System.Windows.Forms.Button()
            Me.numQuantity = New System.Windows.Forms.NumericUpDown()
            Me.lblQty = New System.Windows.Forms.Label()
            Me.pnlSearchProd = New System.Windows.Forms.Panel()
            Me.cboPosCategory = New System.Windows.Forms.ComboBox()
            Me.txtPosSearch = New System.Windows.Forms.TextBox()
            Me.lblSearch = New System.Windows.Forms.Label()
            Me.dgvCart = New System.Windows.Forms.DataGridView()
            Me.colCartCode = New System.Windows.Forms.DataGridViewTextBoxColumn()
            Me.colCartName = New System.Windows.Forms.DataGridViewTextBoxColumn()
            Me.colCartPrice = New System.Windows.Forms.DataGridViewTextBoxColumn()
            Me.colCartQty = New System.Windows.Forms.DataGridViewTextBoxColumn()
            Me.colCartTotal = New System.Windows.Forms.DataGridViewTextBoxColumn()
            Me.pnlCheckout = New System.Windows.Forms.Panel()
            Me.btnPrintLast = New System.Windows.Forms.Button()
            Me.btnCheckout = New System.Windows.Forms.Button()
            Me.lblChange = New System.Windows.Forms.Label()
            Me.lblChangeTitle = New System.Windows.Forms.Label()
            Me.txtCashReceived = New System.Windows.Forms.TextBox()
            Me.lblCashTitle = New System.Windows.Forms.Label()
            Me.lblNetTotal = New System.Windows.Forms.Label()
            Me.lblNetTotalTitle = New System.Windows.Forms.Label()
            Me.lblDiscountAmount = New System.Windows.Forms.Label()
            Me.cboDiscount = New System.Windows.Forms.ComboBox()
            Me.lblDiscountTitle = New System.Windows.Forms.Label()
            Me.lblSubtotal = New System.Windows.Forms.Label()
            Me.lblSubtotalTitle = New System.Windows.Forms.Label()
            Me.pnlCartActions = New System.Windows.Forms.Panel()
            Me.cboCustomer = New System.Windows.Forms.ComboBox()
            Me.lblAttachCust = New System.Windows.Forms.Label()
            Me.btnClearCart = New System.Windows.Forms.Button()
            Me.btnRemoveItem = New System.Windows.Forms.Button()
            Me.btnIncQty = New System.Windows.Forms.Button()
            Me.btnDecQty = New System.Windows.Forms.Button()
            Me.tabSales = New System.Windows.Forms.TabPage()
            Me.dgvTodaySales = New System.Windows.Forms.DataGridView()
            Me.colSaleReceipt = New System.Windows.Forms.DataGridViewTextBoxColumn()
            Me.colSaleTime = New System.Windows.Forms.DataGridViewTextBoxColumn()
            Me.colSaleCustomer = New System.Windows.Forms.DataGridViewTextBoxColumn()
            Me.colSaleSubtotal = New System.Windows.Forms.DataGridViewTextBoxColumn()
            Me.colSaleDiscount = New System.Windows.Forms.DataGridViewTextBoxColumn()
            Me.colSaleTotal = New System.Windows.Forms.DataGridViewTextBoxColumn()
            Me.pnlSalesTop = New System.Windows.Forms.Panel()
            Me.btnViewSaleDetails = New System.Windows.Forms.Button()
            Me.btnRefreshTodaySales = New System.Windows.Forms.Button()
            Me.lblTodaySummary = New System.Windows.Forms.Label()
            Me.pnlHeader.SuspendLayout()
            Me.tabCashier.SuspendLayout()
            Me.tabPos.SuspendLayout()
            CType(Me.splitPos, System.ComponentModel.ISupportInitialize).BeginInit()
            Me.splitPos.Panel1.SuspendLayout()
            Me.splitPos.Panel2.SuspendLayout()
            Me.splitPos.SuspendLayout()
            CType(Me.dgvPosProducts, System.ComponentModel.ISupportInitialize).BeginInit()
            Me.pnlAddToCart.SuspendLayout()
            CType(Me.numQuantity, System.ComponentModel.ISupportInitialize).BeginInit()
            Me.pnlSearchProd.SuspendLayout()
            CType(Me.dgvCart, System.ComponentModel.ISupportInitialize).BeginInit()
            Me.pnlCheckout.SuspendLayout()
            Me.pnlCartActions.SuspendLayout()
            Me.tabSales.SuspendLayout()
            CType(Me.dgvTodaySales, System.ComponentModel.ISupportInitialize).BeginInit()
            Me.pnlSalesTop.SuspendLayout()
            Me.SuspendLayout()
            '
            'pnlHeader
            '
            Me.pnlHeader.BackColor = System.Drawing.Color.FromArgb(CType(CType(30, Byte), Integer), CType(CType(94, Byte), Integer), CType(CType(58, Byte), Integer))
            Me.pnlHeader.Controls.Add(Me.btnLogout)
            Me.pnlHeader.Controls.Add(Me.lblCashierInfo)
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
            'lblCashierInfo
            '
            Me.lblCashierInfo.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.lblCashierInfo.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.lblCashierInfo.ForeColor = System.Drawing.Color.FromArgb(CType(CType(200, Byte), Integer), CType(CType(230, Byte), Integer), CType(CType(201, Byte), Integer))
            Me.lblCashierInfo.Location = New System.Drawing.Point(545, 22)
            Me.lblCashierInfo.Name = "lblCashierInfo"
            Me.lblCashierInfo.Size = New System.Drawing.Size(350, 20)
            Me.lblCashierInfo.TabIndex = 1
            Me.lblCashierInfo.Text = "Cashier: Juan Dela Cruz"
            Me.lblCashierInfo.TextAlign = System.Drawing.ContentAlignment.MiddleRight
            '
            'lblAppTitle
            '
            Me.lblAppTitle.AutoSize = True
            Me.lblAppTitle.Font = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.lblAppTitle.ForeColor = System.Drawing.Color.White
            Me.lblAppTitle.Location = New System.Drawing.Point(15, 20)
            Me.lblAppTitle.Name = "lblAppTitle"
            Me.lblAppTitle.Size = New System.Drawing.Size(350, 21)
            Me.lblAppTitle.TabIndex = 0
            Me.lblAppTitle.Text = "Mini-Grocery Store - Cashier POS Terminal"
            '
            'tabCashier
            '
            Me.tabCashier.Controls.Add(Me.tabPos)
            Me.tabCashier.Controls.Add(Me.tabSales)
            Me.tabCashier.Dock = System.Windows.Forms.DockStyle.Fill
            Me.tabCashier.Font = New System.Drawing.Font("Segoe UI", 9.5!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.tabCashier.ItemSize = New System.Drawing.Size(150, 30)
            Me.tabCashier.Location = New System.Drawing.Point(0, 65)
            Me.tabCashier.Name = "tabCashier"
            Me.tabCashier.SelectedIndex = 0
            Me.tabCashier.Size = New System.Drawing.Size(1008, 616)
            Me.tabCashier.TabIndex = 1
            '
            'tabPos
            '
            Me.tabPos.Controls.Add(Me.splitPos)
            Me.tabPos.Location = New System.Drawing.Point(4, 34)
            Me.tabPos.Name = "tabPos"
            Me.tabPos.Padding = New System.Windows.Forms.Padding(3)
            Me.tabPos.Size = New System.Drawing.Size(1000, 578)
            Me.tabPos.TabIndex = 0
            Me.tabPos.Text = "🛒 Point of Sale (POS)"
            Me.tabPos.UseVisualStyleBackColor = True
            '
            'splitPos
            '
            Me.splitPos.Dock = System.Windows.Forms.DockStyle.Fill
            Me.splitPos.FixedPanel = System.Windows.Forms.FixedPanel.Panel1
            Me.splitPos.Location = New System.Drawing.Point(3, 3)
            Me.splitPos.Name = "splitPos"
            '
            'splitPos.Panel1
            '
            Me.splitPos.Panel1.Controls.Add(Me.dgvPosProducts)
            Me.splitPos.Panel1.Controls.Add(Me.pnlAddToCart)
            Me.splitPos.Panel1.Controls.Add(Me.pnlSearchProd)
            '
            'splitPos.Panel2
            '
            Me.splitPos.Panel2.Controls.Add(Me.dgvCart)
            Me.splitPos.Panel2.Controls.Add(Me.pnlCheckout)
            Me.splitPos.Panel2.Controls.Add(Me.pnlCartActions)
            Me.splitPos.Size = New System.Drawing.Size(994, 572)
            Me.splitPos.SplitterDistance = 450
            Me.splitPos.TabIndex = 0
            '
            'dgvPosProducts
            '
            Me.dgvPosProducts.AllowUserToAddRows = False
            Me.dgvPosProducts.AllowUserToDeleteRows = False
            Me.dgvPosProducts.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill
            Me.dgvPosProducts.BackgroundColor = System.Drawing.Color.White
            Me.dgvPosProducts.BorderStyle = System.Windows.Forms.BorderStyle.None
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(CType(CType(30, Byte), Integer), CType(CType(94, Byte), Integer), CType(CType(58, Byte), Integer))
            dataGridViewCellStyle1.Font = New System.Drawing.Font("Segoe UI", 9.5!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.White
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
            Me.dgvPosProducts.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1
            Me.dgvPosProducts.ColumnHeadersHeight = 30
            Me.dgvPosProducts.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.colPosCode, Me.colPosName, Me.colPosPrice, Me.colPosStock})
            Me.dgvPosProducts.Dock = System.Windows.Forms.DockStyle.Fill
            Me.dgvPosProducts.EnableHeadersVisualStyles = False
            Me.dgvPosProducts.Location = New System.Drawing.Point(0, 52)
            Me.dgvPosProducts.MultiSelect = False
            Me.dgvPosProducts.Name = "dgvPosProducts"
            Me.dgvPosProducts.ReadOnly = True
            Me.dgvPosProducts.RowHeadersVisible = False
            Me.dgvPosProducts.RowTemplate.Height = 26
            Me.dgvPosProducts.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
            Me.dgvPosProducts.Size = New System.Drawing.Size(450, 468)
            Me.dgvPosProducts.TabIndex = 1
            '
            'colPosCode
            '
            Me.colPosCode.FillWeight = 26.0!
            Me.colPosCode.HeaderText = "Code"
            Me.colPosCode.Name = "colPosCode"
            Me.colPosCode.ReadOnly = True
            '
            'colPosName
            '
            Me.colPosName.FillWeight = 46.0!
            Me.colPosName.HeaderText = "Product Name"
            Me.colPosName.Name = "colPosName"
            Me.colPosName.ReadOnly = True
            '
            'colPosPrice
            '
            Me.colPosPrice.FillWeight = 22.0!
            Me.colPosPrice.HeaderText = "Price"
            Me.colPosPrice.Name = "colPosPrice"
            Me.colPosPrice.ReadOnly = True
            '
            'colPosStock
            '
            Me.colPosStock.FillWeight = 16.0!
            Me.colPosStock.HeaderText = "Stock"
            Me.colPosStock.Name = "colPosStock"
            Me.colPosStock.ReadOnly = True
            '
            'pnlAddToCart
            '
            Me.pnlAddToCart.BackColor = System.Drawing.Color.FromArgb(CType(CType(245, Byte), Integer), CType(CType(248, Byte), Integer), CType(CType(245, Byte), Integer))
            Me.pnlAddToCart.Controls.Add(Me.btnAddToCart)
            Me.pnlAddToCart.Controls.Add(Me.numQuantity)
            Me.pnlAddToCart.Controls.Add(Me.lblQty)
            Me.pnlAddToCart.Dock = System.Windows.Forms.DockStyle.Bottom
            Me.pnlAddToCart.Location = New System.Drawing.Point(0, 520)
            Me.pnlAddToCart.Name = "pnlAddToCart"
            Me.pnlAddToCart.Size = New System.Drawing.Size(450, 52)
            Me.pnlAddToCart.TabIndex = 2
            '
            'btnAddToCart
            '
            Me.btnAddToCart.BackColor = System.Drawing.Color.FromArgb(CType(CType(46, Byte), Integer), CType(CType(125, Byte), Integer), CType(CType(50, Byte), Integer))
            Me.btnAddToCart.Cursor = System.Windows.Forms.Cursors.Hand
            Me.btnAddToCart.FlatAppearance.BorderSize = 0
            Me.btnAddToCart.FlatStyle = System.Windows.Forms.FlatStyle.Flat
            Me.btnAddToCart.Font = New System.Drawing.Font("Segoe UI Semibold", 9.5!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.btnAddToCart.ForeColor = System.Drawing.Color.White
            Me.btnAddToCart.Location = New System.Drawing.Point(210, 10)
            Me.btnAddToCart.Name = "btnAddToCart"
            Me.btnAddToCart.Size = New System.Drawing.Size(225, 32)
            Me.btnAddToCart.TabIndex = 2
            Me.btnAddToCart.Text = "➕ Add to Cart"
            Me.btnAddToCart.UseVisualStyleBackColor = False
            '
            'numQuantity
            '
            Me.numQuantity.Font = New System.Drawing.Font("Segoe UI", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.numQuantity.Location = New System.Drawing.Point(100, 14)
            Me.numQuantity.Minimum = New Decimal(New Integer() {1, 0, 0, 0})
            Me.numQuantity.Maximum = New Decimal(New Integer() {999, 0, 0, 0})
            Me.numQuantity.Name = "numQuantity"
            Me.numQuantity.Size = New System.Drawing.Size(95, 25)
            Me.numQuantity.TabIndex = 1
            Me.numQuantity.Value = New Decimal(New Integer() {1, 0, 0, 0})
            '
            'lblQty
            '
            Me.lblQty.AutoSize = True
            Me.lblQty.Font = New System.Drawing.Font("Segoe UI Semibold", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.lblQty.Location = New System.Drawing.Point(12, 18)
            Me.lblQty.Name = "lblQty"
            Me.lblQty.Size = New System.Drawing.Size(56, 15)
            Me.lblQty.TabIndex = 0
            Me.lblQty.Text = "Quantity:"
            '
            'pnlSearchProd
            '
            Me.pnlSearchProd.BackColor = System.Drawing.Color.FromArgb(CType(CType(245, Byte), Integer), CType(CType(248, Byte), Integer), CType(CType(245, Byte), Integer))
            Me.pnlSearchProd.Controls.Add(Me.cboPosCategory)
            Me.pnlSearchProd.Controls.Add(Me.txtPosSearch)
            Me.pnlSearchProd.Controls.Add(Me.lblSearch)
            Me.pnlSearchProd.Dock = System.Windows.Forms.DockStyle.Top
            Me.pnlSearchProd.Location = New System.Drawing.Point(0, 0)
            Me.pnlSearchProd.Name = "pnlSearchProd"
            Me.pnlSearchProd.Size = New System.Drawing.Size(450, 52)
            Me.pnlSearchProd.TabIndex = 0
            '
            'cboPosCategory
            '
            Me.cboPosCategory.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
            Me.cboPosCategory.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.cboPosCategory.FormattingEnabled = True
            Me.cboPosCategory.Location = New System.Drawing.Point(280, 14)
            Me.cboPosCategory.Name = "cboPosCategory"
            Me.cboPosCategory.Size = New System.Drawing.Size(155, 23)
            Me.cboPosCategory.TabIndex = 2
            '
            'txtPosSearch
            '
            Me.txtPosSearch.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.txtPosSearch.Location = New System.Drawing.Point(65, 14)
            Me.txtPosSearch.Name = "txtPosSearch"
            Me.txtPosSearch.MaxLength = 30
            Me.txtPosSearch.Size = New System.Drawing.Size(205, 23)
            Me.txtPosSearch.TabIndex = 1
            '
            'lblSearch
            '
            Me.lblSearch.AutoSize = True
            Me.lblSearch.Font = New System.Drawing.Font("Segoe UI Semibold", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.lblSearch.Location = New System.Drawing.Point(12, 17)
            Me.lblSearch.Name = "lblSearch"
            Me.lblSearch.Size = New System.Drawing.Size(45, 15)
            Me.lblSearch.TabIndex = 0
            Me.lblSearch.Text = "Search:"
            '
            'dgvCart
            '
            Me.dgvCart.AllowUserToAddRows = False
            Me.dgvCart.AllowUserToDeleteRows = False
            Me.dgvCart.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill
            Me.dgvCart.BackgroundColor = System.Drawing.Color.White
            Me.dgvCart.BorderStyle = System.Windows.Forms.BorderStyle.None
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.FromArgb(CType(CType(30, Byte), Integer), CType(CType(94, Byte), Integer), CType(CType(58, Byte), Integer))
            dataGridViewCellStyle2.Font = New System.Drawing.Font("Segoe UI", 9.5!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.White
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
            Me.dgvCart.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2
            Me.dgvCart.ColumnHeadersHeight = 30
            Me.dgvCart.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.colCartCode, Me.colCartName, Me.colCartPrice, Me.colCartQty, Me.colCartTotal})
            Me.dgvCart.Dock = System.Windows.Forms.DockStyle.Fill
            Me.dgvCart.EnableHeadersVisualStyles = False
            Me.dgvCart.Location = New System.Drawing.Point(0, 48)
            Me.dgvCart.MultiSelect = False
            Me.dgvCart.Name = "dgvCart"
            Me.dgvCart.ReadOnly = True
            Me.dgvCart.RowHeadersVisible = False
            Me.dgvCart.RowTemplate.Height = 26
            Me.dgvCart.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
            Me.dgvCart.Size = New System.Drawing.Size(540, 274)
            Me.dgvCart.TabIndex = 1
            '
            'colCartCode
            '
            Me.colCartCode.FillWeight = 22.0!
            Me.colCartCode.HeaderText = "Code"
            Me.colCartCode.Name = "colCartCode"
            Me.colCartCode.ReadOnly = True
            '
            'colCartName
            '
            Me.colCartName.FillWeight = 42.0!
            Me.colCartName.HeaderText = "Item"
            Me.colCartName.Name = "colCartName"
            Me.colCartName.ReadOnly = True
            '
            'colCartPrice
            '
            Me.colCartPrice.FillWeight = 20.0!
            Me.colCartPrice.HeaderText = "Price"
            Me.colCartPrice.Name = "colCartPrice"
            Me.colCartPrice.ReadOnly = True
            '
            'colCartQty
            '
            Me.colCartQty.FillWeight = 16.0!
            Me.colCartQty.HeaderText = "Qty"
            Me.colCartQty.Name = "colCartQty"
            Me.colCartQty.ReadOnly = True
            '
            'colCartTotal
            '
            Me.colCartTotal.FillWeight = 22.0!
            Me.colCartTotal.HeaderText = "Total"
            Me.colCartTotal.Name = "colCartTotal"
            Me.colCartTotal.ReadOnly = True
            '
            'pnlCheckout
            '
            Me.pnlCheckout.BackColor = System.Drawing.Color.FromArgb(CType(CType(245, Byte), Integer), CType(CType(248, Byte), Integer), CType(CType(245, Byte), Integer))
            Me.pnlCheckout.Controls.Add(Me.btnPrintLast)
            Me.pnlCheckout.Controls.Add(Me.btnCheckout)
            Me.pnlCheckout.Controls.Add(Me.lblChange)
            Me.pnlCheckout.Controls.Add(Me.lblChangeTitle)
            Me.pnlCheckout.Controls.Add(Me.txtCashReceived)
            Me.pnlCheckout.Controls.Add(Me.lblCashTitle)
            Me.pnlCheckout.Controls.Add(Me.lblNetTotal)
            Me.pnlCheckout.Controls.Add(Me.lblNetTotalTitle)
            Me.pnlCheckout.Controls.Add(Me.lblDiscountAmount)
            Me.pnlCheckout.Controls.Add(Me.cboDiscount)
            Me.pnlCheckout.Controls.Add(Me.lblDiscountTitle)
            Me.pnlCheckout.Controls.Add(Me.lblSubtotal)
            Me.pnlCheckout.Controls.Add(Me.lblSubtotalTitle)
            Me.pnlCheckout.Dock = System.Windows.Forms.DockStyle.Bottom
            Me.pnlCheckout.Location = New System.Drawing.Point(0, 322)
            Me.pnlCheckout.Name = "pnlCheckout"
            Me.pnlCheckout.Size = New System.Drawing.Size(540, 250)
            Me.pnlCheckout.TabIndex = 2
            '
            'btnPrintLast
            '
            Me.btnPrintLast.BackColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer))
            Me.btnPrintLast.Cursor = System.Windows.Forms.Cursors.Hand
            Me.btnPrintLast.FlatAppearance.BorderSize = 0
            Me.btnPrintLast.FlatStyle = System.Windows.Forms.FlatStyle.Flat
            Me.btnPrintLast.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.btnPrintLast.Location = New System.Drawing.Point(20, 202)
            Me.btnPrintLast.Name = "btnPrintLast"
            Me.btnPrintLast.Size = New System.Drawing.Size(160, 38)
            Me.btnPrintLast.TabIndex = 12
            Me.btnPrintLast.Text = "🖨 Reprint Last Receipt"
            Me.btnPrintLast.UseVisualStyleBackColor = False
            '
            'btnCheckout
            '
            Me.btnCheckout.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.btnCheckout.BackColor = System.Drawing.Color.FromArgb(CType(CType(46, Byte), Integer), CType(CType(125, Byte), Integer), CType(CType(50, Byte), Integer))
            Me.btnCheckout.Cursor = System.Windows.Forms.Cursors.Hand
            Me.btnCheckout.FlatAppearance.BorderSize = 0
            Me.btnCheckout.FlatStyle = System.Windows.Forms.FlatStyle.Flat
            Me.btnCheckout.Font = New System.Drawing.Font("Segoe UI", 11.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.btnCheckout.ForeColor = System.Drawing.Color.White
            Me.btnCheckout.Location = New System.Drawing.Point(195, 202)
            Me.btnCheckout.Name = "btnCheckout"
            Me.btnCheckout.Size = New System.Drawing.Size(325, 38)
            Me.btnCheckout.TabIndex = 11
            Me.btnCheckout.Text = "💳 COMPLETE CHECKOUT"
            Me.btnCheckout.UseVisualStyleBackColor = False
            '
            'lblChange
            '
            Me.lblChange.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.lblChange.Font = New System.Drawing.Font("Segoe UI", 11.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.lblChange.ForeColor = System.Drawing.Color.FromArgb(CType(CType(30, Byte), Integer), CType(CType(94, Byte), Integer), CType(CType(58, Byte), Integer))
            Me.lblChange.Location = New System.Drawing.Point(340, 160)
            Me.lblChange.Name = "lblChange"
            Me.lblChange.Size = New System.Drawing.Size(180, 22)
            Me.lblChange.TabIndex = 10
            Me.lblChange.Text = "PHP 0.00"
            Me.lblChange.TextAlign = System.Drawing.ContentAlignment.MiddleRight
            '
            'lblChangeTitle
            '
            Me.lblChangeTitle.AutoSize = True
            Me.lblChangeTitle.Font = New System.Drawing.Font("Segoe UI Semibold", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.lblChangeTitle.Location = New System.Drawing.Point(20, 162)
            Me.lblChangeTitle.Name = "lblChangeTitle"
            Me.lblChangeTitle.Size = New System.Drawing.Size(89, 19)
            Me.lblChangeTitle.TabIndex = 9
            Me.lblChangeTitle.Text = "Change Due:"
            '
            'txtCashReceived
            '
            Me.txtCashReceived.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.txtCashReceived.Font = New System.Drawing.Font("Segoe UI", 11.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.txtCashReceived.Location = New System.Drawing.Point(360, 122)
            Me.txtCashReceived.Name = "txtCashReceived"
            Me.txtCashReceived.MaxLength = 9
            Me.txtCashReceived.Size = New System.Drawing.Size(160, 27)
            Me.txtCashReceived.TabIndex = 8
            Me.txtCashReceived.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
            '
            'lblCashTitle
            '
            Me.lblCashTitle.AutoSize = True
            Me.lblCashTitle.Font = New System.Drawing.Font("Segoe UI Semibold", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.lblCashTitle.Location = New System.Drawing.Point(20, 126)
            Me.lblCashTitle.Name = "lblCashTitle"
            Me.lblCashTitle.Size = New System.Drawing.Size(147, 19)
            Me.lblCashTitle.TabIndex = 7
            Me.lblCashTitle.Text = "Cash Tendered (PHP):"
            '
            'lblNetTotal
            '
            Me.lblNetTotal.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.lblNetTotal.Font = New System.Drawing.Font("Segoe UI", 14.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.lblNetTotal.ForeColor = System.Drawing.Color.FromArgb(CType(CType(198, Byte), Integer), CType(CType(40, Byte), Integer), CType(CType(40, Byte), Integer))
            Me.lblNetTotal.Location = New System.Drawing.Point(300, 82)
            Me.lblNetTotal.Name = "lblNetTotal"
            Me.lblNetTotal.Size = New System.Drawing.Size(220, 28)
            Me.lblNetTotal.TabIndex = 6
            Me.lblNetTotal.Text = "PHP 0.00"
            Me.lblNetTotal.TextAlign = System.Drawing.ContentAlignment.MiddleRight
            '
            'lblNetTotalTitle
            '
            Me.lblNetTotalTitle.AutoSize = True
            Me.lblNetTotalTitle.Font = New System.Drawing.Font("Segoe UI", 11.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.lblNetTotalTitle.Location = New System.Drawing.Point(20, 88)
            Me.lblNetTotalTitle.Name = "lblNetTotalTitle"
            Me.lblNetTotalTitle.Size = New System.Drawing.Size(91, 20)
            Me.lblNetTotalTitle.TabIndex = 5
            Me.lblNetTotalTitle.Text = "TOTAL DUE:"
            '
            'lblDiscountAmount
            '
            Me.lblDiscountAmount.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.lblDiscountAmount.Font = New System.Drawing.Font("Segoe UI", 9.5!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.lblDiscountAmount.ForeColor = System.Drawing.Color.DarkGreen
            Me.lblDiscountAmount.Location = New System.Drawing.Point(360, 48)
            Me.lblDiscountAmount.Name = "lblDiscountAmount"
            Me.lblDiscountAmount.Size = New System.Drawing.Size(160, 20)
            Me.lblDiscountAmount.TabIndex = 4
            Me.lblDiscountAmount.Text = "-PHP 0.00"
            Me.lblDiscountAmount.TextAlign = System.Drawing.ContentAlignment.MiddleRight
            '
            'cboDiscount
            '
            Me.cboDiscount.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
            Me.cboDiscount.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.cboDiscount.FormattingEnabled = True
            Me.cboDiscount.Location = New System.Drawing.Point(145, 46)
            Me.cboDiscount.Name = "cboDiscount"
            Me.cboDiscount.Size = New System.Drawing.Size(195, 23)
            Me.cboDiscount.TabIndex = 3
            '
            'lblDiscountTitle
            '
            Me.lblDiscountTitle.AutoSize = True
            Me.lblDiscountTitle.Font = New System.Drawing.Font("Segoe UI Semibold", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.lblDiscountTitle.Location = New System.Drawing.Point(20, 49)
            Me.lblDiscountTitle.Name = "lblDiscountTitle"
            Me.lblDiscountTitle.Size = New System.Drawing.Size(89, 15)
            Me.lblDiscountTitle.TabIndex = 2
            Me.lblDiscountTitle.Text = "Apply Discount:"
            '
            'lblSubtotal
            '
            Me.lblSubtotal.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.lblSubtotal.Font = New System.Drawing.Font("Segoe UI", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.lblSubtotal.Location = New System.Drawing.Point(360, 14)
            Me.lblSubtotal.Name = "lblSubtotal"
            Me.lblSubtotal.Size = New System.Drawing.Size(160, 20)
            Me.lblSubtotal.TabIndex = 1
            Me.lblSubtotal.Text = "PHP 0.00"
            Me.lblSubtotal.TextAlign = System.Drawing.ContentAlignment.MiddleRight
            '
            'lblSubtotalTitle
            '
            Me.lblSubtotalTitle.AutoSize = True
            Me.lblSubtotalTitle.Font = New System.Drawing.Font("Segoe UI Semibold", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.lblSubtotalTitle.Location = New System.Drawing.Point(20, 16)
            Me.lblSubtotalTitle.Name = "lblSubtotalTitle"
            Me.lblSubtotalTitle.Size = New System.Drawing.Size(86, 15)
            Me.lblSubtotalTitle.TabIndex = 0
            Me.lblSubtotalTitle.Text = "Gross Subtotal:"
            '
            'pnlCartActions
            '
            Me.pnlCartActions.BackColor = System.Drawing.Color.FromArgb(CType(CType(245, Byte), Integer), CType(CType(248, Byte), Integer), CType(CType(245, Byte), Integer))
            Me.pnlCartActions.Controls.Add(Me.cboCustomer)
            Me.pnlCartActions.Controls.Add(Me.lblAttachCust)
            Me.pnlCartActions.Controls.Add(Me.btnClearCart)
            Me.pnlCartActions.Controls.Add(Me.btnRemoveItem)
            Me.pnlCartActions.Controls.Add(Me.btnIncQty)
            Me.pnlCartActions.Controls.Add(Me.btnDecQty)
            Me.pnlCartActions.Dock = System.Windows.Forms.DockStyle.Top
            Me.pnlCartActions.Location = New System.Drawing.Point(0, 0)
            Me.pnlCartActions.Name = "pnlCartActions"
            Me.pnlCartActions.Size = New System.Drawing.Size(540, 48)
            Me.pnlCartActions.TabIndex = 0
            '
            'cboCustomer
            '
            Me.cboCustomer.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
            Me.cboCustomer.Font = New System.Drawing.Font("Segoe UI", 8.5!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.cboCustomer.FormattingEnabled = True
            Me.cboCustomer.Location = New System.Drawing.Point(75, 12)
            Me.cboCustomer.Name = "cboCustomer"
            Me.cboCustomer.Size = New System.Drawing.Size(180, 21)
            Me.cboCustomer.TabIndex = 5
            '
            'lblAttachCust
            '
            Me.lblAttachCust.AutoSize = True
            Me.lblAttachCust.Font = New System.Drawing.Font("Segoe UI Semibold", 8.5!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.lblAttachCust.Location = New System.Drawing.Point(10, 15)
            Me.lblAttachCust.Name = "lblAttachCust"
            Me.lblAttachCust.Size = New System.Drawing.Size(61, 15)
            Me.lblAttachCust.TabIndex = 4
            Me.lblAttachCust.Text = "Customer:"
            '
            'btnClearCart
            '
            Me.btnClearCart.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.btnClearCart.BackColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer))
            Me.btnClearCart.Cursor = System.Windows.Forms.Cursors.Hand
            Me.btnClearCart.FlatAppearance.BorderSize = 0
            Me.btnClearCart.FlatStyle = System.Windows.Forms.FlatStyle.Flat
            Me.btnClearCart.Font = New System.Drawing.Font("Segoe UI", 8.5!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.btnClearCart.Location = New System.Drawing.Point(460, 10)
            Me.btnClearCart.Name = "btnClearCart"
            Me.btnClearCart.Size = New System.Drawing.Size(65, 26)
            Me.btnClearCart.TabIndex = 3
            Me.btnClearCart.Text = "Clear"
            Me.btnClearCart.UseVisualStyleBackColor = False
            '
            'btnRemoveItem
            '
            Me.btnRemoveItem.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.btnRemoveItem.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(205, Byte), Integer), CType(CType(210, Byte), Integer))
            Me.btnRemoveItem.Cursor = System.Windows.Forms.Cursors.Hand
            Me.btnRemoveItem.FlatAppearance.BorderSize = 0
            Me.btnRemoveItem.FlatStyle = System.Windows.Forms.FlatStyle.Flat
            Me.btnRemoveItem.Font = New System.Drawing.Font("Segoe UI", 8.5!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.btnRemoveItem.ForeColor = System.Drawing.Color.DarkRed
            Me.btnRemoveItem.Location = New System.Drawing.Point(375, 10)
            Me.btnRemoveItem.Name = "btnRemoveItem"
            Me.btnRemoveItem.Size = New System.Drawing.Size(75, 26)
            Me.btnRemoveItem.TabIndex = 2
            Me.btnRemoveItem.Text = "Remove"
            Me.btnRemoveItem.UseVisualStyleBackColor = False
            '
            'btnIncQty
            '
            Me.btnIncQty.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.btnIncQty.BackColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer))
            Me.btnIncQty.Cursor = System.Windows.Forms.Cursors.Hand
            Me.btnIncQty.FlatAppearance.BorderSize = 0
            Me.btnIncQty.FlatStyle = System.Windows.Forms.FlatStyle.Flat
            Me.btnIncQty.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.btnIncQty.Location = New System.Drawing.Point(330, 10)
            Me.btnIncQty.Name = "btnIncQty"
            Me.btnIncQty.Size = New System.Drawing.Size(35, 26)
            Me.btnIncQty.TabIndex = 1
            Me.btnIncQty.Text = "+"
            Me.btnIncQty.UseVisualStyleBackColor = False
            '
            'btnDecQty
            '
            Me.btnDecQty.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.btnDecQty.BackColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer))
            Me.btnDecQty.Cursor = System.Windows.Forms.Cursors.Hand
            Me.btnDecQty.FlatAppearance.BorderSize = 0
            Me.btnDecQty.FlatStyle = System.Windows.Forms.FlatStyle.Flat
            Me.btnDecQty.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.btnDecQty.Location = New System.Drawing.Point(285, 10)
            Me.btnDecQty.Name = "btnDecQty"
            Me.btnDecQty.Size = New System.Drawing.Size(35, 26)
            Me.btnDecQty.TabIndex = 0
            Me.btnDecQty.Text = "-"
            Me.btnDecQty.UseVisualStyleBackColor = False
            '
            'tabSales
            '
            Me.tabSales.Controls.Add(Me.dgvTodaySales)
            Me.tabSales.Controls.Add(Me.pnlSalesTop)
            Me.tabSales.Location = New System.Drawing.Point(4, 34)
            Me.tabSales.Name = "tabSales"
            Me.tabSales.Padding = New System.Windows.Forms.Padding(3)
            Me.tabSales.Size = New System.Drawing.Size(1000, 578)
            Me.tabSales.TabIndex = 1
            Me.tabSales.Text = "📊 Today's Transactions"
            Me.tabSales.UseVisualStyleBackColor = True
            '
            'dgvTodaySales
            '
            Me.dgvTodaySales.AllowUserToAddRows = False
            Me.dgvTodaySales.AllowUserToDeleteRows = False
            Me.dgvTodaySales.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill
            Me.dgvTodaySales.BackgroundColor = System.Drawing.Color.White
            Me.dgvTodaySales.BorderStyle = System.Windows.Forms.BorderStyle.None
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
            dataGridViewCellStyle3.BackColor = System.Drawing.Color.FromArgb(CType(CType(30, Byte), Integer), CType(CType(94, Byte), Integer), CType(CType(58, Byte), Integer))
            dataGridViewCellStyle3.Font = New System.Drawing.Font("Segoe UI", 9.5!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            dataGridViewCellStyle3.ForeColor = System.Drawing.Color.White
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
            Me.dgvTodaySales.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle3
            Me.dgvTodaySales.ColumnHeadersHeight = 32
            Me.dgvTodaySales.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.colSaleReceipt, Me.colSaleTime, Me.colSaleCustomer, Me.colSaleSubtotal, Me.colSaleDiscount, Me.colSaleTotal})
            Me.dgvTodaySales.Dock = System.Windows.Forms.DockStyle.Fill
            Me.dgvTodaySales.EnableHeadersVisualStyles = False
            Me.dgvTodaySales.Location = New System.Drawing.Point(3, 53)
            Me.dgvTodaySales.MultiSelect = False
            Me.dgvTodaySales.Name = "dgvTodaySales"
            Me.dgvTodaySales.ReadOnly = True
            Me.dgvTodaySales.RowHeadersVisible = False
            Me.dgvTodaySales.RowTemplate.Height = 26
            Me.dgvTodaySales.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
            Me.dgvTodaySales.Size = New System.Drawing.Size(994, 522)
            Me.dgvTodaySales.TabIndex = 1
            '
            'colSaleReceipt
            '
            Me.colSaleReceipt.FillWeight = 22.0!
            Me.colSaleReceipt.HeaderText = "Receipt No"
            Me.colSaleReceipt.Name = "colSaleReceipt"
            Me.colSaleReceipt.ReadOnly = True
            '
            'colSaleTime
            '
            Me.colSaleTime.FillWeight = 20.0!
            Me.colSaleTime.HeaderText = "Time"
            Me.colSaleTime.Name = "colSaleTime"
            Me.colSaleTime.ReadOnly = True
            '
            'colSaleCustomer
            '
            Me.colSaleCustomer.FillWeight = 25.0!
            Me.colSaleCustomer.HeaderText = "Customer"
            Me.colSaleCustomer.Name = "colSaleCustomer"
            Me.colSaleCustomer.ReadOnly = True
            '
            'colSaleSubtotal
            '
            Me.colSaleSubtotal.FillWeight = 18.0!
            Me.colSaleSubtotal.HeaderText = "Subtotal (PHP)"
            Me.colSaleSubtotal.Name = "colSaleSubtotal"
            Me.colSaleSubtotal.ReadOnly = True
            '
            'colSaleDiscount
            '
            Me.colSaleDiscount.FillWeight = 18.0!
            Me.colSaleDiscount.HeaderText = "Discount (PHP)"
            Me.colSaleDiscount.Name = "colSaleDiscount"
            Me.colSaleDiscount.ReadOnly = True
            '
            'colSaleTotal
            '
            Me.colSaleTotal.FillWeight = 20.0!
            Me.colSaleTotal.HeaderText = "Net Total (PHP)"
            Me.colSaleTotal.Name = "colSaleTotal"
            Me.colSaleTotal.ReadOnly = True
            '
            'pnlSalesTop
            '
            Me.pnlSalesTop.BackColor = System.Drawing.Color.FromArgb(CType(CType(245, Byte), Integer), CType(CType(248, Byte), Integer), CType(CType(245, Byte), Integer))
            Me.pnlSalesTop.Controls.Add(Me.btnViewSaleDetails)
            Me.pnlSalesTop.Controls.Add(Me.btnRefreshTodaySales)
            Me.pnlSalesTop.Controls.Add(Me.lblTodaySummary)
            Me.pnlSalesTop.Dock = System.Windows.Forms.DockStyle.Top
            Me.pnlSalesTop.Location = New System.Drawing.Point(3, 3)
            Me.pnlSalesTop.Name = "pnlSalesTop"
            Me.pnlSalesTop.Size = New System.Drawing.Size(994, 50)
            Me.pnlSalesTop.TabIndex = 0
            '
            'btnViewSaleDetails
            '
            Me.btnViewSaleDetails.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.btnViewSaleDetails.BackColor = System.Drawing.Color.FromArgb(CType(CType(46, Byte), Integer), CType(CType(125, Byte), Integer), CType(CType(50, Byte), Integer))
            Me.btnViewSaleDetails.Cursor = System.Windows.Forms.Cursors.Hand
            Me.btnViewSaleDetails.FlatAppearance.BorderSize = 0
            Me.btnViewSaleDetails.FlatStyle = System.Windows.Forms.FlatStyle.Flat
            Me.btnViewSaleDetails.Font = New System.Drawing.Font("Segoe UI Semibold", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.btnViewSaleDetails.ForeColor = System.Drawing.Color.White
            Me.btnViewSaleDetails.Location = New System.Drawing.Point(825, 10)
            Me.btnViewSaleDetails.Name = "btnViewSaleDetails"
            Me.btnViewSaleDetails.Size = New System.Drawing.Size(155, 30)
            Me.btnViewSaleDetails.TabIndex = 2
            Me.btnViewSaleDetails.Text = "📄 View / Reprint"
            Me.btnViewSaleDetails.UseVisualStyleBackColor = False
            '
            'btnRefreshTodaySales
            '
            Me.btnRefreshTodaySales.BackColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer))
            Me.btnRefreshTodaySales.Cursor = System.Windows.Forms.Cursors.Hand
            Me.btnRefreshTodaySales.FlatAppearance.BorderSize = 0
            Me.btnRefreshTodaySales.FlatStyle = System.Windows.Forms.FlatStyle.Flat
            Me.btnRefreshTodaySales.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.btnRefreshTodaySales.Location = New System.Drawing.Point(15, 10)
            Me.btnRefreshTodaySales.Name = "btnRefreshTodaySales"
            Me.btnRefreshTodaySales.Size = New System.Drawing.Size(100, 30)
            Me.btnRefreshTodaySales.TabIndex = 1
            Me.btnRefreshTodaySales.Text = "🔄 Refresh"
            Me.btnRefreshTodaySales.UseVisualStyleBackColor = False
            '
            'lblTodaySummary
            '
            Me.lblTodaySummary.AutoSize = True
            Me.lblTodaySummary.Font = New System.Drawing.Font("Segoe UI Semibold", 9.5!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.lblTodaySummary.Location = New System.Drawing.Point(135, 16)
            Me.lblTodaySummary.Name = "lblTodaySummary"
            Me.lblTodaySummary.Size = New System.Drawing.Size(201, 17)
            Me.lblTodaySummary.TabIndex = 0
            Me.lblTodaySummary.Text = "Today: 0 transactions | Total: PHP 0.00"
            '
            'CashierDashboardForm
            '
            Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 15.0!)
            Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
            Me.ClientSize = New System.Drawing.Size(1008, 681)
            Me.Controls.Add(Me.tabCashier)
            Me.Controls.Add(Me.pnlHeader)
            Me.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.MinimumSize = New System.Drawing.Size(950, 650)
            Me.Name = "CashierDashboardForm"
            Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
            Me.Text = "Cashier POS Terminal - Mini-Grocery Store"
            Me.pnlHeader.ResumeLayout(False)
            Me.pnlHeader.PerformLayout()
            Me.tabCashier.ResumeLayout(False)
            Me.tabPos.ResumeLayout(False)
            Me.splitPos.Panel1.ResumeLayout(False)
            Me.splitPos.Panel2.ResumeLayout(False)
            CType(Me.splitPos, System.ComponentModel.ISupportInitialize).EndInit()
            Me.splitPos.ResumeLayout(False)
            CType(Me.dgvPosProducts, System.ComponentModel.ISupportInitialize).EndInit()
            Me.pnlAddToCart.ResumeLayout(False)
            Me.pnlAddToCart.PerformLayout()
            CType(Me.numQuantity, System.ComponentModel.ISupportInitialize).EndInit()
            Me.pnlSearchProd.ResumeLayout(False)
            Me.pnlSearchProd.PerformLayout()
            CType(Me.dgvCart, System.ComponentModel.ISupportInitialize).EndInit()
            Me.pnlCheckout.ResumeLayout(False)
            Me.pnlCheckout.PerformLayout()
            Me.pnlCartActions.ResumeLayout(False)
            Me.pnlCartActions.PerformLayout()
            Me.tabSales.ResumeLayout(False)
            CType(Me.dgvTodaySales, System.ComponentModel.ISupportInitialize).EndInit()
            Me.pnlSalesTop.ResumeLayout(False)
            Me.pnlSalesTop.PerformLayout()
            Me.ResumeLayout(False)

        End Sub

        Friend WithEvents pnlHeader As System.Windows.Forms.Panel
        Friend WithEvents lblAppTitle As System.Windows.Forms.Label
        Friend WithEvents lblCashierInfo As System.Windows.Forms.Label
        Friend WithEvents btnLogout As System.Windows.Forms.Button
        Friend WithEvents tabCashier As System.Windows.Forms.TabControl
        Friend WithEvents tabPos As System.Windows.Forms.TabPage
        Friend WithEvents splitPos As System.Windows.Forms.SplitContainer
        Friend WithEvents pnlSearchProd As System.Windows.Forms.Panel
        Friend WithEvents lblSearch As System.Windows.Forms.Label
        Friend WithEvents txtPosSearch As System.Windows.Forms.TextBox
        Friend WithEvents cboPosCategory As System.Windows.Forms.ComboBox
        Friend WithEvents dgvPosProducts As System.Windows.Forms.DataGridView
        Friend WithEvents colPosCode As System.Windows.Forms.DataGridViewTextBoxColumn
        Friend WithEvents colPosName As System.Windows.Forms.DataGridViewTextBoxColumn
        Friend WithEvents colPosPrice As System.Windows.Forms.DataGridViewTextBoxColumn
        Friend WithEvents colPosStock As System.Windows.Forms.DataGridViewTextBoxColumn
        Friend WithEvents pnlAddToCart As System.Windows.Forms.Panel
        Friend WithEvents lblQty As System.Windows.Forms.Label
        Friend WithEvents numQuantity As System.Windows.Forms.NumericUpDown
        Friend WithEvents btnAddToCart As System.Windows.Forms.Button
        Friend WithEvents pnlCartActions As System.Windows.Forms.Panel
        Friend WithEvents btnDecQty As System.Windows.Forms.Button
        Friend WithEvents btnIncQty As System.Windows.Forms.Button
        Friend WithEvents btnRemoveItem As System.Windows.Forms.Button
        Friend WithEvents btnClearCart As System.Windows.Forms.Button
        Friend WithEvents lblAttachCust As System.Windows.Forms.Label
        Friend WithEvents cboCustomer As System.Windows.Forms.ComboBox
        Friend WithEvents dgvCart As System.Windows.Forms.DataGridView
        Friend WithEvents colCartCode As System.Windows.Forms.DataGridViewTextBoxColumn
        Friend WithEvents colCartName As System.Windows.Forms.DataGridViewTextBoxColumn
        Friend WithEvents colCartPrice As System.Windows.Forms.DataGridViewTextBoxColumn
        Friend WithEvents colCartQty As System.Windows.Forms.DataGridViewTextBoxColumn
        Friend WithEvents colCartTotal As System.Windows.Forms.DataGridViewTextBoxColumn
        Friend WithEvents pnlCheckout As System.Windows.Forms.Panel
        Friend WithEvents lblSubtotalTitle As System.Windows.Forms.Label
        Friend WithEvents lblSubtotal As System.Windows.Forms.Label
        Friend WithEvents lblDiscountTitle As System.Windows.Forms.Label
        Friend WithEvents cboDiscount As System.Windows.Forms.ComboBox
        Friend WithEvents lblDiscountAmount As System.Windows.Forms.Label
        Friend WithEvents lblNetTotalTitle As System.Windows.Forms.Label
        Friend WithEvents lblNetTotal As System.Windows.Forms.Label
        Friend WithEvents lblCashTitle As System.Windows.Forms.Label
        Friend WithEvents txtCashReceived As System.Windows.Forms.TextBox
        Friend WithEvents lblChangeTitle As System.Windows.Forms.Label
        Friend WithEvents lblChange As System.Windows.Forms.Label
        Friend WithEvents btnCheckout As System.Windows.Forms.Button
        Friend WithEvents btnPrintLast As System.Windows.Forms.Button
        Friend WithEvents tabSales As System.Windows.Forms.TabPage
        Friend WithEvents pnlSalesTop As System.Windows.Forms.Panel
        Friend WithEvents lblTodaySummary As System.Windows.Forms.Label
        Friend WithEvents btnRefreshTodaySales As System.Windows.Forms.Button
        Friend WithEvents btnViewSaleDetails As System.Windows.Forms.Button
        Friend WithEvents dgvTodaySales As System.Windows.Forms.DataGridView
        Friend WithEvents colSaleReceipt As System.Windows.Forms.DataGridViewTextBoxColumn
        Friend WithEvents colSaleTime As System.Windows.Forms.DataGridViewTextBoxColumn
        Friend WithEvents colSaleCustomer As System.Windows.Forms.DataGridViewTextBoxColumn
        Friend WithEvents colSaleSubtotal As System.Windows.Forms.DataGridViewTextBoxColumn
        Friend WithEvents colSaleDiscount As System.Windows.Forms.DataGridViewTextBoxColumn
        Friend WithEvents colSaleTotal As System.Windows.Forms.DataGridViewTextBoxColumn
    End Class
End Namespace

