' ==============================================================================
' Project: Mini-Grocery Store POS & Daily Inventory System
' Module:  Controls / AppHeader  (UI RESTYLE)
' Purpose: Restyles the EXISTING header controls of each portal in place
'          (no control renames/removals): 60px #1F5C3F bar with app title left,
'          "Role: Full name (@username)" right, red Logout button.
' ==============================================================================

Option Strict On
Option Explicit On

Imports System
Imports System.Drawing
Imports System.Windows.Forms

Namespace Controls

    Friend NotInheritable Class AppHeader

        Private Sub New()
        End Sub

        ''' <summary>Shared hover tip so a truncated user strip always reveals its full text (Step 7).</summary>
        Private Shared ReadOnly HeaderTip As New ToolTip()

        ''' <summary>
        ''' Applies the design token styling to the existing header controls.
        ''' </summary>
        Public Shared Sub Apply(header As Control, appTitle As Label, userLabel As Label, logoutBtn As Button)
            If header Is Nothing OrElse appTitle Is Nothing OrElse logoutBtn Is Nothing Then Return

            header.BackColor = Theme.HeaderGreen
            header.Height = Theme.HeightHeader

            appTitle.Text = "Mini-Grocery Store"
            appTitle.Font = Theme.FontAppTitle
            appTitle.ForeColor = Theme.White
            appTitle.AutoSize = True
            appTitle.BackColor = Color.Transparent

            If userLabel IsNot Nothing Then
                userLabel.Font = Theme.FontHeaderUser
                userLabel.ForeColor = Theme.HeaderUserText
                ' Step 7: fixed box + AutoEllipsis + tooltip — the label can never
                ' grow into the Logout button anymore; long
                ' "Role: … · Full name (@username)" strings truncate gracefully
                ' and stay reachable on hover.
                userLabel.AutoSize = False
                userLabel.AutoEllipsis = True
                userLabel.TextAlign = ContentAlignment.MiddleRight
                userLabel.Height = Theme.FontHeaderUser.Height + 4
                userLabel.BackColor = Color.Transparent
            End If

            Theme.StyleButton(logoutBtn, ThemeButtonKind.Danger)
            logoutBtn.Text = "Logout"
            logoutBtn.AutoSize = False
            logoutBtn.Size = New Size(96, Theme.HeightButton)

            ' Immediate + ongoing positioning
            AddHandler header.Resize, Sub(s As Object, e As EventArgs) Reposition(header, appTitle, userLabel, logoutBtn)
            Reposition(header, appTitle, userLabel, logoutBtn)
        End Sub

        Private Shared Sub Reposition(header As Control, appTitle As Label, userLabel As Label, logoutBtn As Button)
            Dim h As Integer = header.ClientSize.Height
            Dim yBadging As Integer = (h - logoutBtn.Height) \ 2

            logoutBtn.Location = New Point(header.ClientSize.Width - 24 - logoutBtn.Width, yBadging)

            appTitle.Location = New Point(24, Math.Max(0, (h - appTitle.Height) \ 2))

            If userLabel IsNot Nothing Then
                ' Step 7 root cause: AutoSize=True grew the label leftward from a
                ' right-anchored x, clamping at a hard 220px minimum — a long
                ' "Role … (@username)" string slid under/over Logout at narrow
                ' widths. The label is now a fixed, cap-sized box that always ENDS
                ' 16px left of Logout, truncates via AutoEllipsis, and pops the
                ' full string as a hover tooltip on every pass.
                Dim titleRight As Integer = appTitle.Right + 16
                Dim avail As Integer = Math.Max(120, logoutBtn.Left - 16 - titleRight)
                userLabel.Width = avail
                userLabel.Location = New Point(titleRight, Math.Max(0, (h - userLabel.Height) \ 2))
                HeaderTip.SetToolTip(userLabel, userLabel.Text)
            End If
        End Sub

    End Class

End Namespace
