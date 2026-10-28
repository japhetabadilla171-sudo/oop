Namespace Forms
    <Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
    Partial Class PastReceiptForm
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
            Me.pnlHeader = New System.Windows.Forms.Panel()
            Me.lblTitle = New System.Windows.Forms.Label()
            Me.pnlMeta = New System.Windows.Forms.Panel()
            Me.lblDate = New System.Windows.Forms.Label()
            Me.lblCustomer = New System.Windows.Forms.Label()
            Me.lblCashier = New System.Windows.Forms.Label()
            Me.lblReceiptNo = New System.Windows.Forms.Label()
            Me.dgvItems = New System.Windows.Forms.DataGridView()
            Me.colCode = New System.Windows.Forms.DataGridViewTextBoxColumn()
            Me.colName = New System.Windows.Forms.DataGridViewTextBoxColumn()
            Me.colQty = New System.Windows.Forms.DataGridViewTextBoxColumn()
            Me.colPrice = New System.Windows.Forms.DataGridViewTextBoxColumn()
            Me.colTotal = New System.Windows.Forms.DataGridViewTextBoxColumn()
            Me.pnlBottom = New System.Windows.Forms.Panel()
            Me.btnPrint = New System.Windows.Forms.Button()
            Me.btnClose = New System.Windows.Forms.Button()
            Me.lblNetTotal = New System.Windows.Forms.Label()
            Me.lblDiscount = New System.Windows.Forms.Label()
            Me.lblSubtotal = New System.Windows.Forms.Label()
            Me.pnlHeader.SuspendLayout()
            Me.pnlMeta.SuspendLayout()
            CType(Me.dgvItems, System.ComponentModel.ISupportInitialize).BeginInit()
            Me.pnlBottom.SuspendLayout()
            Me.SuspendLayout()
            '
            'pnlHeader
            '
            Me.pnlHeader.BackColor = System.Drawing.Color.FromArgb(CType(CType(30, Byte), Integer), CType(CType(94, Byte), Integer), CType(CType(58, Byte), Integer))
            Me.pnlHeader.Controls.Add(Me.lblTitle)
            Me.pnlHeader.Dock = System.Windows.Forms.DockStyle.Top
            Me.pnlHeader.Location = New System.Drawing.Point(0, 0)
            Me.pnlHeader.Name = "pnlHeader"
            Me.pnlHeader.Size = New System.Drawing.Size(634, 45)
            Me.pnlHeader.TabIndex = 0
            '
            'lblTitle
            '
            Me.lblTitle.AutoSize = True
            Me.lblTitle.Font = New System.Drawing.Font("Segoe UI", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.lblTitle.ForeColor = System.Drawing.Color.White
            Me.lblTitle.Location = New System.Drawing.Point(12, 12)
            Me.lblTitle.Name = "lblTitle"
            Me.lblTitle.Size = New System.Drawing.Size(183, 20)
            Me.lblTitle.TabIndex = 0
            Me.lblTitle.Text = "Purchase Receipt Details"
            '
            'pnlMeta
            '
            Me.pnlMeta.BackColor = System.Drawing.Color.FromArgb(CType(CType(240, Byte), Integer), CType(CType(245, Byte), Integer), CType(CType(240, Byte), Integer))
            Me.pnlMeta.Controls.Add(Me.lblDate)
            Me.pnlMeta.Controls.Add(Me.lblCustomer)
            Me.pnlMeta.Controls.Add(Me.lblCashier)
            Me.pnlMeta.Controls.Add(Me.lblReceiptNo)
            Me.pnlMeta.Dock = System.Windows.Forms.DockStyle.Top
            Me.pnlMeta.Location = New System.Drawing.Point(0, 45)
            Me.pnlMeta.Name = "pnlMeta"
            Me.pnlMeta.Size = New System.Drawing.Size(634, 60)
            Me.pnlMeta.TabIndex = 1
            '
            'lblDate
            '
            Me.lblDate.AutoSize = True
            Me.lblDate.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.lblDate.Location = New System.Drawing.Point(340, 33)
            Me.lblDate.Name = "lblDate"
            Me.lblDate.Size = New System.Drawing.Size(76, 15)
            Me.lblDate.TabIndex = 3
            Me.lblDate.Text = "Date & Time: -"
            '
            'lblCustomer
            '
            Me.lblCustomer.AutoSize = True
            Me.lblCustomer.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.lblCustomer.Location = New System.Drawing.Point(340, 10)
            Me.lblCustomer.Name = "lblCustomer"
            Me.lblCustomer.Size = New System.Drawing.Size(68, 15)
            Me.lblCustomer.TabIndex = 2
            Me.lblCustomer.Text = "Customer: -"
            '
            'lblCashier
            '
            Me.lblCashier.AutoSize = True
            Me.lblCashier.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.lblCashier.Location = New System.Drawing.Point(15, 33)
            Me.lblCashier.Name = "lblCashier"
            Me.lblCashier.Size = New System.Drawing.Size(55, 15)
            Me.lblCashier.TabIndex = 1
            Me.lblCashier.Text = "Cashier: -"
            '
            'lblReceiptNo
            '
            Me.lblReceiptNo.AutoSize = True
            Me.lblReceiptNo.Font = New System.Drawing.Font("Segoe UI Semibold", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.lblReceiptNo.Location = New System.Drawing.Point(15, 10)
            Me.lblReceiptNo.Name = "lblReceiptNo"
            Me.lblReceiptNo.Size = New System.Drawing.Size(84, 17)
            Me.lblReceiptNo.TabIndex = 0
            Me.lblReceiptNo.Text = "Receipt No: -"
            '
            'dgvItems
            '
            Me.dgvItems.AllowUserToAddRows = False
            Me.dgvItems.AllowUserToDeleteRows = False
            Me.dgvItems.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill
            Me.dgvItems.BackgroundColor = System.Drawing.Color.White
            Me.dgvItems.BorderStyle = System.Windows.Forms.BorderStyle.None
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(CType(CType(30, Byte), Integer), CType(CType(94, Byte), Integer), CType(CType(58, Byte), Integer))
            dataGridViewCellStyle1.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.White
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
            Me.dgvItems.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1
            Me.dgvItems.ColumnHeadersHeight = 30
            Me.dgvItems.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.colCode, Me.colName, Me.colQty, Me.colPrice, Me.colTotal})
            Me.dgvItems.Dock = System.Windows.Forms.DockStyle.Fill
            Me.dgvItems.EnableHeadersVisualStyles = False
            Me.dgvItems.Location = New System.Drawing.Point(0, 105)
            Me.dgvItems.MultiSelect = False
            Me.dgvItems.Name = "dgvItems"
            Me.dgvItems.ReadOnly = True
            Me.dgvItems.RowHeadersVisible = False
            Me.dgvItems.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
            Me.dgvItems.Size = New System.Drawing.Size(634, 255)
            Me.dgvItems.TabIndex = 2
            '
            'colCode
            '
            Me.colCode.FillWeight = 25.0!
            Me.colCode.HeaderText = "Item Code"
            Me.colCode.Name = "colCode"
            Me.colCode.ReadOnly = True
            '
            'colName
            '
            Me.colName.FillWeight = 45.0!
            Me.colName.HeaderText = "Product Name"
            Me.colName.Name = "colName"
            Me.colName.ReadOnly = True
            '
            'colQty
            '
            Me.colQty.FillWeight = 15.0!
            Me.colQty.HeaderText = "Qty"
            Me.colQty.Name = "colQty"
            Me.colQty.ReadOnly = True
            '
            'colPrice
            '
            Me.colPrice.FillWeight = 20.0!
            Me.colPrice.HeaderText = "Unit Price"
            Me.colPrice.Name = "colPrice"
            Me.colPrice.ReadOnly = True
            '
            'colTotal
            '
            Me.colTotal.FillWeight = 20.0!
            Me.colTotal.HeaderText = "Total"
            Me.colTotal.Name = "colTotal"
            Me.colTotal.ReadOnly = True
            '
            'pnlBottom
            '
            Me.pnlBottom.BackColor = System.Drawing.Color.FromArgb(CType(CType(245, Byte), Integer), CType(CType(248, Byte), Integer), CType(CType(245, Byte), Integer))
            Me.pnlBottom.Controls.Add(Me.btnPrint)
            Me.pnlBottom.Controls.Add(Me.btnClose)
            Me.pnlBottom.Controls.Add(Me.lblNetTotal)
            Me.pnlBottom.Controls.Add(Me.lblDiscount)
            Me.pnlBottom.Controls.Add(Me.lblSubtotal)
            Me.pnlBottom.Dock = System.Windows.Forms.DockStyle.Bottom
            Me.pnlBottom.Location = New System.Drawing.Point(0, 360)
            Me.pnlBottom.Name = "pnlBottom"
            Me.pnlBottom.Size = New System.Drawing.Size(634, 115)
            Me.pnlBottom.TabIndex = 3
            '
            'btnPrint
            '
            Me.btnPrint.BackColor = System.Drawing.Color.FromArgb(CType(CType(46, Byte), Integer), CType(CType(125, Byte), Integer), CType(CType(50, Byte), Integer))
            Me.btnPrint.Cursor = System.Windows.Forms.Cursors.Hand
            Me.btnPrint.FlatAppearance.BorderSize = 0
            Me.btnPrint.FlatStyle = System.Windows.Forms.FlatStyle.Flat
            Me.btnPrint.Font = New System.Drawing.Font("Segoe UI Semibold", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.btnPrint.ForeColor = System.Drawing.Color.White
            Me.btnPrint.Location = New System.Drawing.Point(18, 65)
            Me.btnPrint.Name = "btnPrint"
            Me.btnPrint.Size = New System.Drawing.Size(120, 35)
            Me.btnPrint.TabIndex = 4
            Me.btnPrint.Text = "🖨 Print / Preview"
            Me.btnPrint.UseVisualStyleBackColor = False
            '
            'btnClose
            '
            Me.btnClose.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.btnClose.BackColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer))
            Me.btnClose.Cursor = System.Windows.Forms.Cursors.Hand
            Me.btnClose.FlatAppearance.BorderSize = 0
            Me.btnClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat
            Me.btnClose.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.btnClose.Location = New System.Drawing.Point(525, 65)
            Me.btnClose.Name = "btnClose"
            Me.btnClose.Size = New System.Drawing.Size(95, 35)
            Me.btnClose.TabIndex = 3
            Me.btnClose.Text = "Close"
            Me.btnClose.UseVisualStyleBackColor = False
            '
            'lblNetTotal
            '
            Me.lblNetTotal.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.lblNetTotal.Font = New System.Drawing.Font("Segoe UI", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.lblNetTotal.ForeColor = System.Drawing.Color.FromArgb(CType(CType(30, Byte), Integer), CType(CType(94, Byte), Integer), CType(CType(58, Byte), Integer))
            Me.lblNetTotal.Location = New System.Drawing.Point(320, 48)
            Me.lblNetTotal.Name = "lblNetTotal"
            Me.lblNetTotal.Size = New System.Drawing.Size(300, 20)
            Me.lblNetTotal.TabIndex = 2
            Me.lblNetTotal.Text = "Total Due: PHP 0.00"
            Me.lblNetTotal.TextAlign = System.Drawing.ContentAlignment.MiddleRight
            '
            'lblDiscount
            '
            Me.lblDiscount.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.lblDiscount.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.lblDiscount.Location = New System.Drawing.Point(320, 28)
            Me.lblDiscount.Name = "lblDiscount"
            Me.lblDiscount.Size = New System.Drawing.Size(300, 18)
            Me.lblDiscount.TabIndex = 1
            Me.lblDiscount.Text = "Discount: PHP 0.00"
            Me.lblDiscount.TextAlign = System.Drawing.ContentAlignment.MiddleRight
            '
            'lblSubtotal
            '
            Me.lblSubtotal.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.lblSubtotal.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.lblSubtotal.Location = New System.Drawing.Point(320, 8)
            Me.lblSubtotal.Name = "lblSubtotal"
            Me.lblSubtotal.Size = New System.Drawing.Size(300, 18)
            Me.lblSubtotal.TabIndex = 0
            Me.lblSubtotal.Text = "Subtotal: PHP 0.00"
            Me.lblSubtotal.TextAlign = System.Drawing.ContentAlignment.MiddleRight
            '
            'PastReceiptForm
            '
            Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 15.0!)
            Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
            Me.ClientSize = New System.Drawing.Size(634, 475)
            Me.Controls.Add(Me.dgvItems)
            Me.Controls.Add(Me.pnlBottom)
            Me.Controls.Add(Me.pnlMeta)
            Me.Controls.Add(Me.pnlHeader)
            Me.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
            Me.MaximizeBox = False
            Me.MinimizeBox = False
            Me.Name = "PastReceiptForm"
            Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
            Me.Text = "Receipt Details"
            Me.pnlHeader.ResumeLayout(False)
            Me.pnlHeader.PerformLayout()
            Me.pnlMeta.ResumeLayout(False)
            Me.pnlMeta.PerformLayout()
            CType(Me.dgvItems, System.ComponentModel.ISupportInitialize).EndInit()
            Me.pnlBottom.ResumeLayout(False)
            Me.ResumeLayout(False)

        End Sub

        Friend WithEvents pnlHeader As System.Windows.Forms.Panel
        Friend WithEvents lblTitle As System.Windows.Forms.Label
        Friend WithEvents pnlMeta As System.Windows.Forms.Panel
        Friend WithEvents lblReceiptNo As System.Windows.Forms.Label
        Friend WithEvents lblCashier As System.Windows.Forms.Label
        Friend WithEvents lblCustomer As System.Windows.Forms.Label
        Friend WithEvents lblDate As System.Windows.Forms.Label
        Friend WithEvents dgvItems As System.Windows.Forms.DataGridView
        Friend WithEvents colCode As System.Windows.Forms.DataGridViewTextBoxColumn
        Friend WithEvents colName As System.Windows.Forms.DataGridViewTextBoxColumn
        Friend WithEvents colQty As System.Windows.Forms.DataGridViewTextBoxColumn
        Friend WithEvents colPrice As System.Windows.Forms.DataGridViewTextBoxColumn
        Friend WithEvents colTotal As System.Windows.Forms.DataGridViewTextBoxColumn
        Friend WithEvents pnlBottom As System.Windows.Forms.Panel
        Friend WithEvents lblSubtotal As System.Windows.Forms.Label
        Friend WithEvents lblDiscount As System.Windows.Forms.Label
        Friend WithEvents lblNetTotal As System.Windows.Forms.Label
        Friend WithEvents btnPrint As System.Windows.Forms.Button
        Friend WithEvents btnClose As System.Windows.Forms.Button
    End Class
End Namespace
