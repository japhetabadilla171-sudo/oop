Namespace Forms
    <Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
    Partial Class RegisterForm
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
            Me.lblSubtitle = New System.Windows.Forms.Label()
            Me.lblTitle = New System.Windows.Forms.Label()
            Me.lblFullName = New System.Windows.Forms.Label()
            Me.txtFullName = New System.Windows.Forms.TextBox()
            Me.lblUsername = New System.Windows.Forms.Label()
            Me.txtUsername = New System.Windows.Forms.TextBox()
            Me.lblPassword = New System.Windows.Forms.Label()
            Me.txtPassword = New System.Windows.Forms.TextBox()
            Me.lblConfirmPassword = New System.Windows.Forms.Label()
            Me.txtConfirmPassword = New System.Windows.Forms.TextBox()
            Me.chkShowPassword = New System.Windows.Forms.CheckBox()
            Me.btnRegister = New System.Windows.Forms.Button()
            Me.btnCancel = New System.Windows.Forms.Button()
            Me.lblError = New System.Windows.Forms.Label()
            Me.pnlHeader.SuspendLayout()
            Me.SuspendLayout()
            '
            'pnlHeader
            '
            Me.pnlHeader.BackColor = System.Drawing.Color.FromArgb(CType(CType(30, Byte), Integer), CType(CType(94, Byte), Integer), CType(CType(58, Byte), Integer))
            Me.pnlHeader.Controls.Add(Me.lblSubtitle)
            Me.pnlHeader.Controls.Add(Me.lblTitle)
            Me.pnlHeader.Dock = System.Windows.Forms.DockStyle.Top
            Me.pnlHeader.Location = New System.Drawing.Point(0, 0)
            Me.pnlHeader.Name = "pnlHeader"
            Me.pnlHeader.Size = New System.Drawing.Size(460, 75)
            Me.pnlHeader.TabIndex = 0
            '
            'lblSubtitle
            '
            Me.lblSubtitle.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.lblSubtitle.ForeColor = System.Drawing.Color.FromArgb(CType(CType(200, Byte), Integer), CType(CType(230, Byte), Integer), CType(CType(201, Byte), Integer))
            Me.lblSubtitle.Location = New System.Drawing.Point(12, 42)
            Me.lblSubtitle.Name = "lblSubtitle"
            Me.lblSubtitle.Size = New System.Drawing.Size(436, 20)
            Me.lblSubtitle.TabIndex = 1
            Me.lblSubtitle.Text = "Create an account to browse groceries and track your purchases"
            Me.lblSubtitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
            '
            'lblTitle
            '
            Me.lblTitle.Font = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.lblTitle.ForeColor = System.Drawing.Color.White
            Me.lblTitle.Location = New System.Drawing.Point(12, 12)
            Me.lblTitle.Name = "lblTitle"
            Me.lblTitle.Size = New System.Drawing.Size(436, 25)
            Me.lblTitle.TabIndex = 0
            Me.lblTitle.Text = "Customer Registration"
            Me.lblTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
            '
            'lblFullName
            '
            Me.lblFullName.AutoSize = True
            Me.lblFullName.Font = New System.Drawing.Font("Segoe UI Semibold", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.lblFullName.Location = New System.Drawing.Point(35, 100)
            Me.lblFullName.Name = "lblFullName"
            Me.lblFullName.Size = New System.Drawing.Size(64, 15)
            Me.lblFullName.TabIndex = 1
            Me.lblFullName.Text = "Full Name:"
            '
            'txtFullName
            '
            Me.txtFullName.Font = New System.Drawing.Font("Segoe UI", 9.5!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.txtFullName.Location = New System.Drawing.Point(155, 96)
            Me.txtFullName.Name = "txtFullName"
            Me.txtFullName.MaxLength = 50
            Me.txtFullName.Size = New System.Drawing.Size(265, 24)
            Me.txtFullName.TabIndex = 2
            '
            'lblUsername
            '
            Me.lblUsername.AutoSize = True
            Me.lblUsername.Font = New System.Drawing.Font("Segoe UI Semibold", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.lblUsername.Location = New System.Drawing.Point(35, 140)
            Me.lblUsername.Name = "lblUsername"
            Me.lblUsername.Size = New System.Drawing.Size(63, 15)
            Me.lblUsername.TabIndex = 3
            Me.lblUsername.Text = "Username:"
            '
            'txtUsername
            '
            Me.txtUsername.Font = New System.Drawing.Font("Segoe UI", 9.5!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.txtUsername.Location = New System.Drawing.Point(155, 136)
            Me.txtUsername.Name = "txtUsername"
            Me.txtUsername.MaxLength = 20
            Me.txtUsername.Size = New System.Drawing.Size(265, 24)
            Me.txtUsername.TabIndex = 4
            '
            'lblPassword
            '
            Me.lblPassword.AutoSize = True
            Me.lblPassword.Font = New System.Drawing.Font("Segoe UI Semibold", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.lblPassword.Location = New System.Drawing.Point(35, 180)
            Me.lblPassword.Name = "lblPassword"
            Me.lblPassword.Size = New System.Drawing.Size(60, 15)
            Me.lblPassword.TabIndex = 5
            Me.lblPassword.Text = "Password:"
            '
            'txtPassword
            '
            Me.txtPassword.Font = New System.Drawing.Font("Segoe UI", 9.5!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.txtPassword.Location = New System.Drawing.Point(155, 176)
            Me.txtPassword.Name = "txtPassword"
            Me.txtPassword.MaxLength = 50
            Me.txtPassword.Size = New System.Drawing.Size(265, 24)
            Me.txtPassword.TabIndex = 6
            Me.txtPassword.UseSystemPasswordChar = True
            '
            'lblConfirmPassword
            '
            Me.lblConfirmPassword.AutoSize = True
            Me.lblConfirmPassword.Font = New System.Drawing.Font("Segoe UI Semibold", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.lblConfirmPassword.Location = New System.Drawing.Point(35, 220)
            Me.lblConfirmPassword.Name = "lblConfirmPassword"
            Me.lblConfirmPassword.Size = New System.Drawing.Size(107, 15)
            Me.lblConfirmPassword.TabIndex = 7
            Me.lblConfirmPassword.Text = "Confirm Password:"
            '
            'txtConfirmPassword
            '
            Me.txtConfirmPassword.Font = New System.Drawing.Font("Segoe UI", 9.5!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.txtConfirmPassword.Location = New System.Drawing.Point(155, 216)
            Me.txtConfirmPassword.Name = "txtConfirmPassword"
            Me.txtConfirmPassword.MaxLength = 50
            Me.txtConfirmPassword.Size = New System.Drawing.Size(265, 24)
            Me.txtConfirmPassword.TabIndex = 8
            Me.txtConfirmPassword.UseSystemPasswordChar = True
            '
            'chkShowPassword
            '
            Me.chkShowPassword.AutoSize = True
            Me.chkShowPassword.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.chkShowPassword.Location = New System.Drawing.Point(155, 250)
            Me.chkShowPassword.Name = "chkShowPassword"
            Me.chkShowPassword.Size = New System.Drawing.Size(107, 17)
            Me.chkShowPassword.TabIndex = 9
            Me.chkShowPassword.Text = "Show Passwords"
            Me.chkShowPassword.UseVisualStyleBackColor = True
            '
            'btnRegister
            '
            Me.btnRegister.BackColor = System.Drawing.Color.FromArgb(CType(CType(46, Byte), Integer), CType(CType(125, Byte), Integer), CType(CType(50, Byte), Integer))
            Me.btnRegister.Cursor = System.Windows.Forms.Cursors.Hand
            Me.btnRegister.FlatAppearance.BorderSize = 0
            Me.btnRegister.FlatStyle = System.Windows.Forms.FlatStyle.Flat
            Me.btnRegister.Font = New System.Drawing.Font("Segoe UI Semibold", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.btnRegister.ForeColor = System.Drawing.Color.White
            Me.btnRegister.Location = New System.Drawing.Point(155, 280)
            Me.btnRegister.Name = "btnRegister"
            Me.btnRegister.Size = New System.Drawing.Size(130, 35)
            Me.btnRegister.TabIndex = 10
            Me.btnRegister.Text = "Register"
            Me.btnRegister.UseVisualStyleBackColor = False
            '
            'btnCancel
            '
            Me.btnCancel.BackColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer))
            Me.btnCancel.Cursor = System.Windows.Forms.Cursors.Hand
            Me.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel
            Me.btnCancel.FlatAppearance.BorderSize = 0
            Me.btnCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat
            Me.btnCancel.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.btnCancel.Location = New System.Drawing.Point(295, 280)
            Me.btnCancel.Name = "btnCancel"
            Me.btnCancel.Size = New System.Drawing.Size(125, 35)
            Me.btnCancel.TabIndex = 11
            Me.btnCancel.Text = "Cancel"
            Me.btnCancel.UseVisualStyleBackColor = False
            '
            'lblError
            '
            Me.lblError.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.lblError.ForeColor = System.Drawing.Color.Red
            Me.lblError.Location = New System.Drawing.Point(35, 325)
            Me.lblError.Name = "lblError"
            Me.lblError.Size = New System.Drawing.Size(385, 30)
            Me.lblError.TabIndex = 12
            Me.lblError.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
            '
            'RegisterForm
            '
            Me.AcceptButton = Me.btnRegister
            Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 15.0!)
            Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
            Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(248, Byte), Integer), CType(CType(250, Byte), Integer), CType(CType(248, Byte), Integer))
            Me.CancelButton = Me.btnCancel
            Me.ClientSize = New System.Drawing.Size(460, 365)
            Me.Controls.Add(Me.lblError)
            Me.Controls.Add(Me.btnCancel)
            Me.Controls.Add(Me.btnRegister)
            Me.Controls.Add(Me.chkShowPassword)
            Me.Controls.Add(Me.txtConfirmPassword)
            Me.Controls.Add(Me.lblConfirmPassword)
            Me.Controls.Add(Me.txtPassword)
            Me.Controls.Add(Me.lblPassword)
            Me.Controls.Add(Me.txtUsername)
            Me.Controls.Add(Me.lblUsername)
            Me.Controls.Add(Me.txtFullName)
            Me.Controls.Add(Me.lblFullName)
            Me.Controls.Add(Me.pnlHeader)
            Me.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
            Me.MaximizeBox = False
            Me.MinimizeBox = False
            Me.Name = "RegisterForm"
            Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
            Me.Text = "Register Account"
            Me.pnlHeader.ResumeLayout(False)
            Me.ResumeLayout(False)
            Me.PerformLayout()

        End Sub

        Friend WithEvents pnlHeader As System.Windows.Forms.Panel
        Friend WithEvents lblTitle As System.Windows.Forms.Label
        Friend WithEvents lblSubtitle As System.Windows.Forms.Label
        Friend WithEvents lblFullName As System.Windows.Forms.Label
        Friend WithEvents txtFullName As System.Windows.Forms.TextBox
        Friend WithEvents lblUsername As System.Windows.Forms.Label
        Friend WithEvents txtUsername As System.Windows.Forms.TextBox
        Friend WithEvents lblPassword As System.Windows.Forms.Label
        Friend WithEvents txtPassword As System.Windows.Forms.TextBox
        Friend WithEvents lblConfirmPassword As System.Windows.Forms.Label
        Friend WithEvents txtConfirmPassword As System.Windows.Forms.TextBox
        Friend WithEvents chkShowPassword As System.Windows.Forms.CheckBox
        Friend WithEvents btnRegister As System.Windows.Forms.Button
        Friend WithEvents btnCancel As System.Windows.Forms.Button
        Friend WithEvents lblError As System.Windows.Forms.Label
    End Class
End Namespace


