' ==============================================================================
' Project: Mini-Grocery Store POS & Daily Inventory System
' Course: Object-Oriented Programming (ITP 102) - University of Caloocan City
' Form: RegisterForm (Code-Behind)
' OOP Concept: ENCAPSULATION & DATA VALIDATION
' Provides self-registration for Customers with client-side validation and
' secure password hashing.
' ==============================================================================

Option Strict On
Option Explicit On

Imports System
Imports System.Drawing
Imports System.Windows.Forms
Imports oopfinals.Services
Imports oopfinals.Controls

Namespace Forms
    Public Class RegisterForm
        Private _registeredUsername As String = String.Empty

        Public ReadOnly Property RegisteredUsername As String
            Get
                Return _registeredUsername
            End Get
        End Property

        Private Sub btnRegister_Click(sender As Object, e As EventArgs) Handles btnRegister.Click
            lblError.Text = String.Empty

            Dim fullName As String = txtFullName.Text.Trim()
            Dim username As String = txtUsername.Text.Trim()
            Dim password As String = txtPassword.Text
            Dim confirmPassword As String = txtConfirmPassword.Text

            If String.IsNullOrWhiteSpace(fullName) Then
                lblError.Text = "Please enter your full name."
                txtFullName.Focus()
                Return
            End If

            If not System.Text.RegularExpressions.Regex.IsMatch(fullName, "[A-Za-z]") Then
                lblError.Text = "Full name must contain at least one letter."
                txtFullName.Focus()
                Return
            End If

            If System.Text.RegularExpressions.Regex.IsMatch(fullName, "[0-9@#%&*!+=^?/\[\]{}|~<>:;,-]") Then
                lblError.Text = "Numbers and symbols are not allowed in the full name."
                txtFullName.Focus()
                Return
            End If

            If username.Length < 3 Then
                lblError.Text = "Username must be at least 3 characters."
                txtUsername.Focus()
                Return
            End If

            If Not System.Text.RegularExpressions.Regex.IsMatch(username, "^[A-Za-z0-9._]+$") Then
                lblError.Text = "Username may only contain letters, numbers, dots and underscores."
                txtUsername.Focus()
                Return
            End If

            If password.Contains(" "c) OrElse password.Contains(vbTab) Then
                lblError.Text = "Password cannot contain spaces."
                txtPassword.SelectAll()
                txtPassword.Focus()
                Return
            End If

            If password.Length < 6 Then
                lblError.Text = "Password must be at least 6 characters long."
                txtPassword.Focus()
                Return
            End If

            If password <> confirmPassword Then
                lblError.Text = "Passwords do not match."
                txtConfirmPassword.SelectAll()
                txtConfirmPassword.Focus()
                Return
            End If

            Try
                Dim success As Boolean = AuthService.RegisterCustomer(username, password, fullName)
                If success Then
                    _registeredUsername = username
                    MessageBox.Show("Registration successful! You may now log in to browse groceries.", "Registration Complete", MessageBoxButtons.OK, MessageBoxIcon.Information)
                    Me.DialogResult = DialogResult.OK
                    Me.Close()
                Else
                    lblError.Text = "Registration failed. Please try again."
                End If
            Catch ex As Exception
                lblError.Text = ex.Message
            End Try
        End Sub

        ''' <summary>
        ''' Full name: only letters, spaces, hyphen, apostrophe, dot and comma allowed.
        ''' </summary>
        Private Sub txtFullName_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtFullName.KeyPress
            Dim allowedPunct As String = " '-.,"
            If Not (Char.IsControl(e.KeyChar) OrElse Char.IsLetter(e.KeyChar) OrElse
                    Char.IsWhiteSpace(e.KeyChar) OrElse allowedPunct.Contains(e.KeyChar.ToString())) Then
                e.Handled = True
                lblError.Text = "Numbers and symbols are not allowed in the full name."
            End If
        End Sub

        Private Sub BlockSpaceTyping(sender As Object, e As KeyPressEventArgs) Handles txtUsername.KeyPress, txtPassword.KeyPress, txtConfirmPassword.KeyPress
            If Char.IsWhiteSpace(e.KeyChar) Then
                e.Handled = True
                lblError.Text = "Spaces are not allowed in this field."
            End If
        End Sub

        Private Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
            Me.DialogResult = DialogResult.Cancel
            Me.Close()
        End Sub

        Private Sub chkShowPassword_CheckedChanged(sender As Object, e As EventArgs) Handles chkShowPassword.CheckedChanged
            txtPassword.UseSystemPasswordChar = Not chkShowPassword.Checked
            txtConfirmPassword.UseSystemPasswordChar = Not chkShowPassword.Checked
        End Sub

        ' RESTYLE (visual only): this form had no Load handler; added purely to
        ' apply the design tokens. No behavior changes.
        Private Sub RegisterForm_Load(sender As Object, e As EventArgs) Handles MyBase.Load
            ApplyRestyle()
        End Sub

#Region "Figma UI restyle (visual only)"
        Private Sub ApplyRestyle()
            Me.Text = "Mini-Grocery Store · Register"
            Me.BackColor = Theme.PageBg

            pnlHeader.BackColor = Theme.HeaderGreen
            lblTitle.Text = "Create your account"
            lblTitle.Font = Theme.FontPageTitle
            lblTitle.ForeColor = Theme.White
            lblTitle.BackColor = Color.Transparent

            lblSubtitle.Font = Theme.FontSmall
            lblSubtitle.ForeColor = Theme.HeaderUserText
            lblSubtitle.BackColor = Color.Transparent

            lblFullName.Font = Theme.FontBody12
            lblFullName.ForeColor = Theme.Muted
            lblFullName.BackColor = Color.Transparent

            lblUsername.Font = Theme.FontBody12
            lblUsername.ForeColor = Theme.Muted
            lblUsername.BackColor = Color.Transparent

            lblPassword.Font = Theme.FontBody12
            lblPassword.ForeColor = Theme.Muted
            lblPassword.BackColor = Color.Transparent

            lblConfirmPassword.Font = Theme.FontBody12
            lblConfirmPassword.ForeColor = Theme.Muted
            lblConfirmPassword.BackColor = Color.Transparent

            Theme.StyleInput(txtFullName)
            txtFullName.Font = Theme.FontBody13
            Theme.SetCue(txtFullName, "First and last name")

            Theme.StyleInput(txtUsername)
            txtUsername.Font = Theme.FontBody13
            Theme.SetCue(txtUsername, "Choose a username")

            Theme.StyleInput(txtPassword)
            txtPassword.Font = Theme.FontBody13
            Theme.SetCue(txtPassword, "At least 6 characters")

            Theme.StyleInput(txtConfirmPassword)
            txtConfirmPassword.Font = Theme.FontBody13
            Theme.SetCue(txtConfirmPassword, "Repeat your password")

            chkShowPassword.Font = Theme.FontBody12
            chkShowPassword.ForeColor = Theme.Ink
            chkShowPassword.BackColor = Color.Transparent

            btnRegister.Text = "Register"
            Theme.StyleButton(btnRegister, ThemeButtonKind.Primary)
            btnRegister.AutoSize = True

            btnCancel.Text = "Cancel"
            Theme.StyleButton(btnCancel, ThemeButtonKind.Ghost)
            btnCancel.AutoSize = True

            lblError.Font = Theme.FontBody12
            lblError.ForeColor = Theme.ErrorText
            lblError.BackColor = Color.Transparent
        End Sub
#End Region
    End Class
End Namespace
