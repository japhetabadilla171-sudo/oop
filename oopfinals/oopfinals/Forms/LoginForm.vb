' ==============================================================================
' Project: Mini-Grocery Store POS & Daily Inventory System
' Course: Object-Oriented Programming (ITP 102) - University of Caloocan City
' Form: LoginForm (Code-Behind)
' OOP Concept: POLYMORPHISM IN ACTION
' After validating credentials, the application invokes user.GetDashboardForm()
' polymorphically on an abstract User reference to navigate to the correct dashboard.
' ==============================================================================

Option Strict On
Option Explicit On

Imports System
Imports System.Drawing
Imports System.Windows.Forms
Imports oopfinals.Data
Imports oopfinals.Models
Imports oopfinals.Services
Imports oopfinals.Controls

Namespace Forms
    Public Class LoginForm
        ' Brute-force protection: lock after 5 consecutive failures for 30 seconds
        Private Const MaxFailedAttempts As Integer = 5
        Private Const LockoutSeconds As Integer = 30
        Private _failedAttempts As Integer = 0
        Private _lockoutUntil As DateTime = DateTime.MinValue

        Private Sub LoginForm_Load(sender As Object, e As EventArgs) Handles MyBase.Load
            ApplyRestyle()
            Try
                ' Ensure database schema and seed data exist
                DatabaseHelper.InitializeDatabase()
                lblStatus.Text = String.Empty
            Catch ex As Exception
                lblStatus.Text = "Database initialization error: " & ex.Message
                lblStatus.ForeColor = Color.Red
            End Try
        End Sub

        ''' <summary>
        ''' Executes polymorphic authentication and routes the user.
        ''' Demonstrates POLYMORPHISM: User base reference executes overridden GetDashboardForm().
        ''' </summary>
        Private Sub btnLogin_Click(sender As Object, e As EventArgs) Handles btnLogin.Click
            lblStatus.Text = String.Empty

            ' Brute-force lockout check
            If DateTime.Now < _lockoutUntil Then
                Dim secsLeft As Integer = CType((_lockoutUntil - DateTime.Now).TotalSeconds, Integer)
                lblStatus.Text = $"Too many failed attempts. Locked for {secsLeft + 1} more second(s)."
                lblStatus.ForeColor = Color.Red
                Return
            End If

            Dim username As String = txtUsername.Text.Trim()
            Dim password As String = txtPassword.Text

            If String.IsNullOrWhiteSpace(username) Then
                lblStatus.Text = "Please enter your username."
                txtUsername.Focus()
                Return
            End If

            If username.Length < 3 Then
                lblStatus.Text = "Username must be at least 3 characters."
                lblStatus.ForeColor = Color.Red
                txtUsername.Focus()
                Return
            End If

            If String.IsNullOrEmpty(password) Then
                lblStatus.Text = "Please enter your password."
                lblStatus.ForeColor = Color.Red
                txtPassword.Focus()
                Return
            End If

            If password.Length < 6 Then
                lblStatus.Text = "Password must be at least 6 characters."
                lblStatus.ForeColor = Color.Red
                txtPassword.SelectAll()
                txtPassword.Focus()
                Return
            End If

            Try
                ' Authenticate and receive polymorphic User instance
                Dim loggedInUser As User = AuthService.Authenticate(username, password)

                If loggedInUser Is Nothing Then
                    RegisterFailedAttempt()
                    lblStatus.Text = "Invalid username or password."
                    txtPassword.SelectAll()
                    txtPassword.Focus()
                    Return
                End If

                _failedAttempts = 0
                _lockoutUntil = DateTime.MinValue

                ' --- POLYMORPHISM: Call overridden method on abstract base class reference ---
                ' The exact dashboard form opened (Owner, Cashier, Clerk, or Customer)
                ' is resolved dynamically at runtime without needing a Select Case or If-Else chain!
                Dim dashboard As Form = loggedInUser.GetDashboardForm()

                Me.Hide()
                dashboard.ShowDialog()

                ' When dashboard closes (User logged out)
                ' QA E5: every dashboard opens maximized on its own, and the login
                ' dialog must come back in front so the next role-switch reads right.
                Me.Show()
                Me.Activate()
                txtPassword.Clear()
                lblStatus.Text = "Logged out successfully."
                lblStatus.ForeColor = Color.DarkGreen
                txtUsername.Focus()

            Catch ex As Exception
                lblStatus.Text = ex.Message
                lblStatus.ForeColor = Color.Red
            End Try
        End Sub

        Private Sub RegisterFailedAttempt()
            _failedAttempts += 1
            If _failedAttempts >= MaxFailedAttempts Then
                _lockoutUntil = DateTime.Now.AddSeconds(LockoutSeconds)
                lblStatus.Text = $"Too many failed attempts. Locked for {LockoutSeconds} seconds."
                lblStatus.ForeColor = Color.Red
            Else
                lblStatus.ForeColor = Color.Red
            End If
        End Sub

        ''' <summary>
        ''' Block whitespace while typing into the username/password fields.
        ''' </summary>
        Private Sub BlockSpaceTyping(sender As Object, e As KeyPressEventArgs) Handles txtUsername.KeyPress, txtPassword.KeyPress
            If Char.IsWhiteSpace(e.KeyChar) Then
                e.Handled = True
                lblStatus.Text = "Spaces are not allowed in this field."
                lblStatus.ForeColor = Color.Red
            End If
        End Sub

        Private Sub chkShowPassword_CheckedChanged(sender As Object, e As EventArgs) Handles chkShowPassword.CheckedChanged
            txtPassword.UseSystemPasswordChar = Not chkShowPassword.Checked
        End Sub

        Private Sub lnkRegister_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles lnkRegister.LinkClicked
            Using regForm As New RegisterForm()
                If regForm.ShowDialog(Me) = DialogResult.OK Then
                    lblStatus.Text = "Registration successful! You may now log in."
                    lblStatus.ForeColor = Color.DarkGreen
                    txtUsername.Text = regForm.RegisteredUsername
                    txtPassword.Clear()
                    txtPassword.Focus()
                End If
            End Using
        End Sub

#Region "Figma UI restyle (visual only)"
        Private Sub ApplyRestyle()
            Me.Text = "Mini-Grocery Store · Sign in"
            Me.BackColor = Theme.PageBg

            pnlHeader.BackColor = Theme.HeaderGreen
            lblTitle.Text = "Mini-Grocery Store"
            lblTitle.Font = Theme.FontAppTitle
            lblTitle.ForeColor = Theme.White
            lblTitle.BackColor = Color.Transparent

            grpLogin.Text = ""
            grpLogin.BackColor = Theme.White
            grpLogin.Font = Theme.FontBody12
            Theme.RoundControl(grpLogin, Theme.RadiusCard)
            AddHandler grpLogin.Paint,
                Sub(s As Object, e As PaintEventArgs)
                    Using path As System.Drawing.Drawing2D.GraphicsPath = Theme.RoundedPath(
                        New Rectangle(0, 0, grpLogin.ClientSize.Width - 1, grpLogin.ClientSize.Height - 1), Theme.RadiusCard)
                        Using p As New Pen(Theme.BorderLine, 1.0F)
                            e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias
                            e.Graphics.DrawPath(p, path)
                        End Using
                    End Using
                End Sub

            lblUsername.Font = Theme.FontBody12
            lblUsername.ForeColor = Theme.Muted
            lblUsername.BackColor = Color.Transparent

            lblPassword.Font = Theme.FontBody12
            lblPassword.ForeColor = Theme.Muted
            lblPassword.BackColor = Color.Transparent

            Theme.StyleInput(txtUsername)
            txtUsername.Font = Theme.FontBody13
            Theme.SetCue(txtUsername, "Username")

            Theme.StyleInput(txtPassword)
            txtPassword.Font = Theme.FontBody13
            Theme.SetCue(txtPassword, "Password")

            chkShowPassword.Font = Theme.FontBody12
            chkShowPassword.ForeColor = Theme.Ink
            chkShowPassword.BackColor = Theme.White

            btnLogin.Text = "Log in"
            Theme.StyleButton(btnLogin, ThemeButtonKind.Primary)
            btnLogin.AutoSize = True

            Theme.StyleLink(lnkRegister)
            lnkRegister.BackColor = Color.Transparent

            lblStatus.Font = Theme.FontBody12
            lblStatus.BackColor = Color.Transparent
        End Sub
#End Region
    End Class
End Namespace
