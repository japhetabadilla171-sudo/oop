' ==============================================================================
' Project: Mini-Grocery Store POS & Daily Inventory System
' Module:  Controls / Theme  (UI RESTYLE - Figma design tokens)
' Purpose: Single source of truth for the Figma restyle: color tokens, font
'          tokens, shape helpers and shared control-styling methods.
'          All portal forms consume these tokens - no hard-coded colors elsewhere.
' ==============================================================================

Option Strict On
Option Explicit On

Imports System
Imports System.Drawing
Imports System.Drawing.Drawing2D
Imports System.Drawing.Text
Imports System.Runtime.InteropServices
Imports System.Windows.Forms

Namespace Controls

    ''' <summary>Font weight token; "Semibold" maps to Inter Bold or Segoe UI Semibold.</summary>
    Public Enum ThemeWeight
        Regular
        Semibold
        Bold
    End Enum

    ''' <summary>Button style variants from the design.</summary>
    Public Enum ThemeButtonKind
        Primary
        Secondary
        Destructive
        Danger      ' Logout button (solid red)
        Ghost       ' white with 1px border, muted text
    End Enum

    ''' <summary>Chip/notice tones.</summary>
    Public Enum ThemeTone
        Neutral
        Success
        Info
        Warn
        [Error]
    End Enum

    Public NotInheritable Class Theme

        Private Sub New()
        End Sub

        ' ============================================================
        ' COLOR TOKENS
        ' ============================================================
        Public Shared ReadOnly PageBg As Color = Color.FromArgb(243, 245, 244)          ' #F3F5F4
        Public Shared ReadOnly HeaderGreen As Color = Color.FromArgb(31, 92, 63)        ' #1F5C3F
        Public Shared ReadOnly HeaderUserText As Color = Color.FromArgb(225, 237, 229)
        Public Shared ReadOnly Ink As Color = Color.FromArgb(32, 41, 35)                ' #202923
        Public Shared ReadOnly Muted As Color = Color.FromArgb(101, 113, 105)           ' #657169
        Public Shared ReadOnly BorderLine As Color = Color.FromArgb(215, 222, 217)      ' #D7DED9
        Public Shared ReadOnly GridLine As Color = Color.FromArgb(229, 235, 231)

        Public Shared ReadOnly Green As Color = Color.FromArgb(46, 125, 50)             ' #2E7D32
        Public Shared ReadOnly GreenHover As Color = Color.FromArgb(38, 104, 42)
        Public Shared ReadOnly GreenPress As Color = Color.FromArgb(31, 88, 36)
        Public Shared ReadOnly GreenTint As Color = Color.FromArgb(234, 244, 235)       ' #EAF4EB

        ' QA B6/C5: shared grid-selection tokens. The old selection used Theme.Info
        ' (#0078D7 blue), which read as a stray blue bar/row on every table and
        ' clashed with the green theme. Light-green tint + deep-green ink keeps the
        ' orange low-stock text readable when a row is selected.
        Public Shared ReadOnly SelectionTint As Color = Color.FromArgb(214, 236, 218) ' #D6ECDA (slightly deeper than GreenTint so selection is obvious)
        Public Shared ReadOnly SelectionInk As Color = Color.FromArgb(24, 61, 34)     ' deep green-ink

        Public Shared ReadOnly BtnSecondary As Color = Color.FromArgb(224, 224, 224)    ' #E0E0E0
        Public Shared ReadOnly BtnSecondaryHover As Color = Color.FromArgb(200, 206, 202)
        Public Shared ReadOnly BtnSecondaryPress As Color = Color.FromArgb(186, 193, 189)

        Public Shared ReadOnly Destructive As Color = Color.FromArgb(255, 205, 210)     ' #FFCDD2 (dark text)
        Public Shared ReadOnly DestructiveHover As Color = Color.FromArgb(245, 180, 190)
        Public Shared ReadOnly DestructivePress As Color = Color.FromArgb(235, 160, 173)

        Public Shared ReadOnly LogoutRed As Color = Color.FromArgb(198, 40, 40)         ' #C62828
        Public Shared ReadOnly LogoutHover As Color = Color.FromArgb(169, 33, 33)
        Public Shared ReadOnly LogoutPress As Color = Color.FromArgb(142, 27, 27)

        Public Shared ReadOnly Info As Color = Color.FromArgb(0, 120, 215)              ' #0078D7
        Public Shared ReadOnly InfoTint As Color = Color.FromArgb(234, 243, 252)        ' #EAF3FC
        Public Shared ReadOnly Warn As Color = Color.FromArgb(239, 108, 0)              ' #EF6C00
        Public Shared ReadOnly WarnTint As Color = Color.FromArgb(255, 242, 229)        ' #FFF2E5
        Public Shared ReadOnly ErrorText As Color = Color.FromArgb(198, 40, 40)
        Public Shared ReadOnly ErrorTint As Color = Color.FromArgb(253, 236, 234)

        Public Shared ReadOnly GridHeader As Color = Color.FromArgb(237, 241, 238)      ' #EDF1EE
        Public Shared ReadOnly ScrollThumb As Color = Color.FromArgb(185, 194, 188)     ' #B9C2BC
        Public Shared ReadOnly White As Color = Color.White

        Public Shared ReadOnly HoverPanel As Color = Color.FromArgb(246, 249, 247)

        ' ============================================================
        ' SHAPE TOKENS
        ' ============================================================
        Public Const RadiusButton As Integer = 4
        Public Const RadiusCard As Integer = 6
        Public Const HeightButton As Integer = 34
        Public Const HeightChip As Integer = 23
        Public Const PaddingButtonX As Integer = 12
        Public Const HeightHeader As Integer = 60
        Public Const HeightTabBar As Integer = 46
        Public Const HeightGridHeader As Integer = 40
        Public Const HeightGridRow As Integer = 40
        Public Const HeightGridFooter As Integer = 30
        Public Const HeightPageHeading As Integer = 56
        Public Const GapWorkspace As Integer = 12

        ' ============================================================
        ' FONT TOKENS (Inter if installed, else Segoe UI)
        ' ============================================================
        Private Shared ReadOnly _fontFamily As FontFamily = ResolveFontFamily()
        Private Shared ReadOnly _interInstalled As Boolean = InterIsInstalled()

        ''' <summary>True when the Inter font family is available on this machine.</summary>
        Public Shared ReadOnly Property InterAvailable As Boolean
            Get
                Return _interInstalled
            End Get
        End Property

        Private Shared Function InterIsInstalled() As Boolean
            Try
                Using fc As New InstalledFontCollection()
                    For Each fam As FontFamily In fc.Families
                        If String.Equals(fam.Name, "Inter", StringComparison.OrdinalIgnoreCase) Then
                            Return True
                        End If
                    Next
                End Using
            Catch
                ' Font enumeration failed - fall back to Segoe UI.
            End Try
            Return False
        End Function

        Private Shared Function ResolveFontFamily() As FontFamily
            If InterIsInstalled() Then
                Return New FontFamily("Inter")
            End If
            Return New FontFamily("Segoe UI")
        End Function

        ''' <summary>Builds a font from the active family with the requested weight.</summary>
        Public Shared Function F(size As Single, weight As ThemeWeight) As Font
            Select Case weight
                Case ThemeWeight.Bold
                    Return New Font(_fontFamily, size, FontStyle.Bold, GraphicsUnit.Point)
                Case ThemeWeight.Semibold
                    If _interInstalled Then
                        Return New Font(_fontFamily, size, FontStyle.Bold, GraphicsUnit.Point)
                    End If
                    Return New Font("Segoe UI Semibold", size, FontStyle.Regular, GraphicsUnit.Point)
                Case Else
                    Return New Font(_fontFamily, size, FontStyle.Regular, GraphicsUnit.Point)
            End Select
        End Function

        Public Shared ReadOnly FontAppTitle As Font = F(22.0F, ThemeWeight.Semibold)
        Public Shared ReadOnly FontHeaderUser As Font = F(13.0F, ThemeWeight.Regular)
        Public Shared ReadOnly FontPageTitle As Font = F(24.0F, ThemeWeight.Semibold)
        Public Shared ReadOnly FontPageDesc As Font = F(12.0F, ThemeWeight.Regular)
        Public Shared ReadOnly FontCardTitle As Font = F(16.0F, ThemeWeight.Semibold)
        Public Shared ReadOnly FontSection As Font = F(13.0F, ThemeWeight.Semibold)
        Public Shared ReadOnly FontBody13 As Font = F(13.0F, ThemeWeight.Regular)
        Public Shared ReadOnly FontBody13Semi As Font = F(13.0F, ThemeWeight.Semibold)
        Public Shared ReadOnly FontBody12 As Font = F(12.0F, ThemeWeight.Regular)
        Public Shared ReadOnly FontBody12Semi As Font = F(12.0F, ThemeWeight.Semibold)
        Public Shared ReadOnly FontSmall As Font = F(11.0F, ThemeWeight.Regular)
        Public Shared ReadOnly FontChip As Font = F(11.0F, ThemeWeight.Semibold)
        Public Shared ReadOnly FontButton As Font = F(12.0F, ThemeWeight.Semibold)
        Public Shared ReadOnly FontBigAmount As Font = F(28.0F, ThemeWeight.Semibold)

        ' ============================================================
        ' SHAPE HELPERS
        ' ============================================================
        Public Shared Function RoundedPath(rect As Rectangle, radius As Integer) As GraphicsPath
            Dim path As New GraphicsPath()
            Dim d As Integer = radius * 2
            If d <= 0 OrElse rect.Width <= 0 OrElse rect.Height <= 0 Then
                path.AddRectangle(rect)
                Return path
            End If
            If d > rect.Width Then d = rect.Width
            If d > rect.Height Then d = rect.Height
            path.AddArc(rect.X, rect.Y, d, d, 180, 90)
            path.AddArc(rect.Right - d, rect.Y, d, d, 270, 90)
            path.AddArc(rect.Right - d, rect.Bottom - d, d, d, 0, 90)
            path.AddArc(rect.X, rect.Bottom - d, d, d, 90, 90)
            path.CloseFigure()
            Return path
        End Function

        ''' <summary>Clips a control's corners.</summary>
        Public Shared Sub RoundControl(c As Control, radius As Integer)
            If c Is Nothing Then Return
            Dim path As GraphicsPath = RoundedPath(New Rectangle(0, 0, c.ClientSize.Width, c.ClientSize.Height), radius)
            If c.Region IsNot Nothing Then
                c.Region.Dispose()
            End If
            c.Region = New Region(path)
        End Sub

        ''' <summary>Enables double buffering on any control via reflection (keeps public surface clean).</summary>
        Public Shared Sub SetDoubleBuffered(c As Control, enabled As Boolean)
            Try
                Dim pi As System.Reflection.PropertyInfo = GetType(Control).GetProperty(
                    "DoubleBuffered",
                    System.Reflection.BindingFlags.Instance Or System.Reflection.BindingFlags.NonPublic)
                If pi IsNot Nothing Then
                    pi.SetValue(c, enabled, Nothing)
                End If
            Catch ex As Exception
                ' Non-fatal: control simply won't be double-buffered.
            End Try
        End Sub

        ' ============================================================
        ' CONTROL STYLING
        ' ============================================================
        Public Shared Sub StyleButton(btn As Button, kind As ThemeButtonKind)
            If btn Is Nothing Then Return
            btn.FlatStyle = FlatStyle.Flat
            btn.FlatAppearance.BorderSize = 0
            btn.Font = FontButton
            btn.Height = HeightButton
            Select Case kind
                Case ThemeButtonKind.Primary
                    btn.BackColor = Green
                    btn.ForeColor = White
                    btn.FlatAppearance.MouseOverBackColor = GreenHover
                    btn.FlatAppearance.MouseDownBackColor = GreenPress
                Case ThemeButtonKind.Secondary
                    btn.BackColor = BtnSecondary
                    btn.ForeColor = Ink
                    btn.FlatAppearance.MouseOverBackColor = BtnSecondaryHover
                    btn.FlatAppearance.MouseDownBackColor = BtnSecondaryPress
                Case ThemeButtonKind.Destructive
                    btn.BackColor = Destructive
                    btn.ForeColor = Ink
                    btn.FlatAppearance.MouseOverBackColor = DestructiveHover
                    btn.FlatAppearance.MouseDownBackColor = DestructivePress
                Case ThemeButtonKind.Danger
                    btn.BackColor = LogoutRed
                    btn.ForeColor = White
                    btn.FlatAppearance.MouseOverBackColor = LogoutHover
                    btn.FlatAppearance.MouseDownBackColor = LogoutPress
                Case ThemeButtonKind.Ghost
                    btn.BackColor = White
                    btn.ForeColor = Muted
                    btn.FlatAppearance.BorderSize = 1
                    btn.FlatAppearance.BorderColor = BorderLine
                    btn.FlatAppearance.MouseOverBackColor = HoverPanel
                    btn.FlatAppearance.MouseDownBackColor = BtnSecondaryHover
            End Select
        End Sub

        ''' <summary>Styles a TextBox as a design input: white, borderless (hosted in an InputPanel).</summary>
        Public Shared Sub StyleInput(tb As TextBox)
            If tb Is Nothing Then Return
            tb.BorderStyle = BorderStyle.None
            tb.BackColor = White
            tb.ForeColor = Ink
            tb.Font = FontBody12
        End Sub

        Public Shared Sub StyleCombo(cbo As ComboBox)
            If cbo Is Nothing Then Return
            cbo.FlatStyle = FlatStyle.Flat
            cbo.BackColor = White
            cbo.ForeColor = Ink
            cbo.Font = FontBody12
        End Sub

        Public Shared Sub StyleNum(n As NumericUpDown)
            If n Is Nothing Then Return
            n.BackColor = White
            n.ForeColor = Ink
            n.Font = FontBody12
            n.BorderStyle = BorderStyle.None
            n.TextAlign = HorizontalAlignment.Left
        End Sub

        Public Shared Sub StyleLink(lnk As LinkLabel)
            If lnk Is Nothing Then Return
            lnk.LinkBehavior = LinkBehavior.HoverUnderline
            lnk.LinkColor = Green
            lnk.ActiveLinkColor = GreenHover
            lnk.VisitedLinkColor = Green
            lnk.Font = FontBody12
        End Sub

        ' ============================================================
        ' INPUT PLACEHOLDER (cue banner)
        ' ============================================================
        <DllImport("user32.dll", CharSet:=CharSet.Unicode)>
        Private Shared Function SendMessage(hWnd As IntPtr, msg As Integer, wParam As IntPtr, lParam As String) As IntPtr
        End Function

        Private Const EM_SETCUEBANNER As Integer = &H1501

        ''' <summary>Sets a placeholder hint on a TextBox (shown until the user types).</summary>
        Public Shared Sub SetCue(tb As TextBox, cueText As String)
            If tb Is Nothing OrElse String.IsNullOrEmpty(cueText) Then Return
            Dim send As Action =
                Sub()
                    If tb.IsHandleCreated Then
                        SendMessage(tb.Handle, EM_SETCUEBANNER, CType(1, IntPtr), cueText)
                    End If
                End Sub
            AddHandler tb.HandleCreated, Sub(s As Object, e As EventArgs) send()
            send()
        End Sub

        ''' <summary>Triggers layout and paint refresh after programmatic restyle.</summary>
        Public Shared Sub RefreshLayout(root As Control)
            If root Is Nothing Then Return
            root.PerformLayout()
            root.Invalidate(True)
        End Sub

    End Class

End Namespace
