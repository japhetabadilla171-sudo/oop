Namespace Forms
    <Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
    Partial Class LoginForm
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
            Me.pnlHeader = New System.Windows.Forms.Panel()
            Me.lblTitle = New System.Windows.Forms.Label()
            Me.grpLogin = New System.Windows.Forms.GroupBox()
            Me.lblStatus = New System.Windows.Forms.Label()
            Me.lnkRegister = New System.Windows.Forms.LinkLabel()
            Me.btnLogin = New System.Windows.Forms.Button()
            Me.chkShowPassword = New System.Windows.Forms.CheckBox()
            Me.txtPassword = New System.Windows.Forms.TextBox()
            Me.lblPassword = New System.Windows.Forms.Label()
            Me.txtUsername = New System.Windows.Forms.TextBox()
            Me.lblUsername = New System.Windows.Forms.Label()
            Me.pnlHeader.SuspendLayout()
            Me.grpLogin.SuspendLayout()
            Me.SuspendLayout()
            '
            'pnlHeader
            '
            Me.pnlHeader.BackColor = System.Drawing.Color.FromArgb(CType(CType(30, Byte), Integer), CType(CType(94, Byte), Integer), CType(CType(58, Byte), Integer))
            Me.pnlHeader.Controls.Add(Me.lblTitle)
            Me.pnlHeader.Dock = System.Windows.Forms.DockStyle.Top
            Me.pnlHeader.Location = New System.Drawing.Point(0, 0)
            Me.pnlHeader.Name = "pnlHeader"
            Me.pnlHeader.Size = New System.Drawing.Size(520, 105)
            Me.pnlHeader.TabIndex = 0
            '
            'lblTitle
            '
            Me.lblTitle.Font = New System.Drawing.Font("Segoe UI", 13.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.lblTitle.ForeColor = System.Drawing.Color.White
            Me.lblTitle.Location = New System.Drawing.Point(12, 37)
            Me.lblTitle.Name = "lblTitle"
            Me.lblTitle.Size = New System.Drawing.Size(496, 30)
            Me.lblTitle.TabIndex = 0
            Me.lblTitle.Text = "Mini-Grocery Store POS & Daily Inventory System"
            Me.lblTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
            '
            'grpLogin
            '
            Me.grpLogin.Controls.Add(Me.lblStatus)
            Me.grpLogin.Controls.Add(Me.lnkRegister)
            Me.grpLogin.Controls.Add(Me.btnLogin)
            Me.grpLogin.Controls.Add(Me.chkShowPassword)
            Me.grpLogin.Controls.Add(Me.txtPassword)
            Me.grpLogin.Controls.Add(Me.lblPassword)
            Me.grpLogin.Controls.Add(Me.txtUsername)
            Me.grpLogin.Controls.Add(Me.lblUsername)
            Me.grpLogin.Font = New System.Drawing.Font("Segoe UI Semibold", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.grpLogin.Location = New System.Drawing.Point(35, 120)
            Me.grpLogin.Name = "grpLogin"
            Me.grpLogin.Size = New System.Drawing.Size(450, 245)
            Me.grpLogin.TabIndex = 1
            Me.grpLogin.TabStop = False
            Me.grpLogin.Text = "Account Authentication"
            '
            'lblStatus
            '
            Me.lblStatus.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.lblStatus.ForeColor = System.Drawing.Color.Red
            Me.lblStatus.Location = New System.Drawing.Point(28, 215)
            Me.lblStatus.Name = "lblStatus"
            Me.lblStatus.Size = New System.Drawing.Size(395, 23)
            Me.lblStatus.TabIndex = 7
            Me.lblStatus.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
            '
            'lnkRegister
            '
            Me.lnkRegister.AutoSize = True
            Me.lnkRegister.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.lnkRegister.LinkColor = System.Drawing.Color.FromArgb(CType(CType(30, Byte), Integer), CType(CType(94, Byte), Integer), CType(CType(58, Byte), Integer))
            Me.lnkRegister.Location = New System.Drawing.Point(125, 192)
            Me.lnkRegister.Name = "lnkRegister"
            Me.lnkRegister.Size = New System.Drawing.Size(202, 15)
            Me.lnkRegister.TabIndex = 6
            Me.lnkRegister.TabStop = True
            Me.lnkRegister.Text = "New Customer? Click here to register"
            '
            'btnLogin
            '
            Me.btnLogin.BackColor = System.Drawing.Color.FromArgb(CType(CType(46, Byte), Integer), CType(CType(125, Byte), Integer), CType(CType(50, Byte), Integer))
            Me.btnLogin.Cursor = System.Windows.Forms.Cursors.Hand
            Me.btnLogin.FlatAppearance.BorderSize = 0
            Me.btnLogin.FlatStyle = System.Windows.Forms.FlatStyle.Flat
            Me.btnLogin.Font = New System.Drawing.Font("Segoe UI", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.btnLogin.ForeColor = System.Drawing.Color.White
            Me.btnLogin.Location = New System.Drawing.Point(28, 145)
            Me.btnLogin.Name = "btnLogin"
            Me.btnLogin.Size = New System.Drawing.Size(395, 38)
            Me.btnLogin.TabIndex = 5
            Me.btnLogin.Text = "LOG IN"
            Me.btnLogin.UseVisualStyleBackColor = False
            '
            'chkShowPassword
            '
            Me.chkShowPassword.AutoSize = True
            Me.chkShowPassword.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.chkShowPassword.Location = New System.Drawing.Point(128, 115)
            Me.chkShowPassword.Name = "chkShowPassword"
            Me.chkShowPassword.Size = New System.Drawing.Size(107, 17)
            Me.chkShowPassword.TabIndex = 4
            Me.chkShowPassword.Text = "Show Password"
            Me.chkShowPassword.UseVisualStyleBackColor = True
            '
            'txtPassword
            '
            Me.txtPassword.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.txtPassword.Location = New System.Drawing.Point(128, 79)
            Me.txtPassword.Name = "txtPassword"
            Me.txtPassword.MaxLength = 50
            Me.txtPassword.Size = New System.Drawing.Size(295, 25)
            Me.txtPassword.TabIndex = 3
            Me.txtPassword.UseSystemPasswordChar = True
            '
            'lblPassword
            '
            Me.lblPassword.AutoSize = True
            Me.lblPassword.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.lblPassword.Location = New System.Drawing.Point(28, 84)
            Me.lblPassword.Name = "lblPassword"
            Me.lblPassword.Size = New System.Drawing.Size(60, 15)
            Me.lblPassword.TabIndex = 2
            Me.lblPassword.Text = "Password:"
            '
            'txtUsername
            '
            Me.txtUsername.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.txtUsername.Location = New System.Drawing.Point(128, 38)
            Me.txtUsername.Name = "txtUsername"
            Me.txtUsername.MaxLength = 20
            Me.txtUsername.Size = New System.Drawing.Size(295, 25)
            Me.txtUsername.TabIndex = 1
            '
            'lblUsername
            '
            Me.lblUsername.AutoSize = True
            Me.lblUsername.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.lblUsername.Location = New System.Drawing.Point(28, 43)
            Me.lblUsername.Name = "lblUsername"
            Me.lblUsername.Size = New System.Drawing.Size(63, 15)
            Me.lblUsername.TabIndex = 0
            Me.lblUsername.Text = "Username:"
            '
            'LoginForm
            '
            Me.AcceptButton = Me.btnLogin
            Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 15.0!)
            Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
            Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(248, Byte), Integer), CType(CType(250, Byte), Integer), CType(CType(248, Byte), Integer))
            Me.ClientSize = New System.Drawing.Size(520, 390)
            Me.Controls.Add(Me.grpLogin)
            Me.Controls.Add(Me.pnlHeader)
            Me.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
            Me.MaximizeBox = False
            Me.Name = "LoginForm"
            Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
            Me.Text = "Login - Mini-Grocery Store POS & Inventory"
            Me.pnlHeader.ResumeLayout(False)
            Me.grpLogin.ResumeLayout(False)
            Me.grpLogin.PerformLayout()
            Me.ResumeLayout(False)

        End Sub

        Friend WithEvents pnlHeader As System.Windows.Forms.Panel
        Friend WithEvents lblTitle As System.Windows.Forms.Label
        Friend WithEvents grpLogin As System.Windows.Forms.GroupBox
        Friend WithEvents lblUsername As System.Windows.Forms.Label
        Friend WithEvents txtUsername As System.Windows.Forms.TextBox
        Friend WithEvents lblPassword As System.Windows.Forms.Label
        Friend WithEvents txtPassword As System.Windows.Forms.TextBox
        Friend WithEvents chkShowPassword As System.Windows.Forms.CheckBox
        Friend WithEvents btnLogin As System.Windows.Forms.Button
        Friend WithEvents lnkRegister As System.Windows.Forms.LinkLabel
        Friend WithEvents lblStatus As System.Windows.Forms.Label
    End Class
End Namespace


