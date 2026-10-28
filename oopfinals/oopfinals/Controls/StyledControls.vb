' ==============================================================================
' Project: Mini-Grocery Store POS & Daily Inventory System
' Module:  Controls / StyledControls  (UI RESTYLE)
' Purpose: Reusable visual parts from the design system:
'          CardPanel, InputPanel, StatusChip, InlineNotice, ProgressRail.
' ==============================================================================

Option Strict On
Option Explicit On

Imports System
Imports System.Drawing
Imports System.Drawing.Drawing2D
Imports System.Windows.Forms

Namespace Controls

    ''' <summary>
    ''' White surface with 6px radius and 1px border, 16px padding (design "card").
    ''' </summary>
    Public Class CardPanel
        Inherits Panel

        Private _radius As Integer = Theme.RadiusCard

        Public Property Radius As Integer
            Get
                Return _radius
            End Get
            Set(value As Integer)
                _radius = value
                UpdateRound()
                Invalidate()
            End Set
        End Property

        Public Property BorderColor As Color = Theme.BorderLine

        Public Sub New()
            SetStyle(ControlStyles.AllPaintingInWmPaint Or ControlStyles.OptimizedDoubleBuffer Or
                     ControlStyles.ResizeRedraw Or ControlStyles.UserPaint, True)
            BackColor = Theme.White
            Padding = New Padding(16)
            Theme.SetDoubleBuffered(Me, True)
        End Sub

        Protected Overrides Sub OnResize(e As EventArgs)
            MyBase.OnResize(e)
            UpdateRound()
        End Sub

        Private Sub UpdateRound()
            Theme.RoundControl(Me, _radius)
        End Sub

        Protected Overrides Sub OnPaint(e As PaintEventArgs)
            MyBase.OnPaint(e)
            Dim path As GraphicsPath = Theme.RoundedPath(New Rectangle(0, 0, ClientSize.Width - 1, ClientSize.Height - 1), _radius)
            Using pen As New Pen(BorderColor, 1.0F)
                e.Graphics.SmoothingMode = SmoothingMode.AntiAlias
                e.Graphics.DrawPath(pen, path)
            End Using
            path.Dispose()
        End Sub

    End Class

    ''' <summary>
    ''' 34px rounded (4px) white input surface hosting a borderless child control,
    ''' focus ring in info blue, as in the design. Hosts children centered at full
    ''' text height so native cue banners (placeholders) never clip.
    ''' </summary>
    Public Class InputPanel
        Inherits Panel

        Private _focused As Boolean

        Public Sub New()
            SetStyle(ControlStyles.AllPaintingInWmPaint Or ControlStyles.OptimizedDoubleBuffer Or
                     ControlStyles.ResizeRedraw Or ControlStyles.UserPaint, True)
            BackColor = Theme.White
            Height = Theme.HeightButton
            Theme.SetDoubleBuffered(Me, True)
        End Sub

        ''' <summary>Calls SetCue on the inner TextBox and tracks focus for the focus ring.</summary>
        Public Sub Bind(child As TextBox, Optional cueText As String = Nothing)
            If child Is Nothing Then Return
            AddHandler child.GotFocus, Sub(s As Object, e As EventArgs)
                                           _focused = True
                                           Invalidate()
                                       End Sub
            AddHandler child.LostFocus, Sub(s As Object, e As EventArgs)
                                            _focused = False
                                            Invalidate()
                                        End Sub
            If Not String.IsNullOrEmpty(cueText) Then
                Theme.SetCue(child, cueText)
            End If
            FitChild(child)
        End Sub

        ''' <summary>Keeps a hosted child fill-style and vertically centered when the
        ''' host resizes. A TextBox is never smaller than its preferred height, so the
        ''' cue banner always has room (forms that reposition children run after this
        ''' via their own handlers and simply win on Left/Width).</summary>
        Public Sub FitChild(child As Control)
            If child Is Nothing Then Return
            Dim tb As TextBox = TryCast(child, TextBox)
            If tb IsNot Nothing AndAlso Not tb.Multiline AndAlso tb.Height < tb.PreferredHeight Then
                tb.Height = tb.PreferredHeight
            End If
            If child.Left < 10 Then child.Left = 10
            If child.Left + child.Width > ClientSize.Width - 10 Then
                child.Width = Math.Max(24, ClientSize.Width - 20)
            End If
            child.Top = Math.Max(0, (ClientSize.Height - child.Height) \ 2)
            GrowToChild(tb)
        End Sub

        ''' <summary>Fill-anchored panels grow just enough to clear the child's height.</summary>
        Private Sub GrowToChild(tb As TextBox)
            If tb Is Nothing OrElse tb.Multiline Then Return
            If Dock <> DockStyle.Fill Then Return
            Dim preferred As Integer = Math.Max(Theme.HeightButton, tb.PreferredHeight + 12)
            If preferred > Height Then
                Height = preferred
                tb.Top = Math.Max(0, (ClientSize.Height - tb.Height) \ 2)
            End If
        End Sub

        Protected Overrides Sub OnControlAdded(e As ControlEventArgs)
            MyBase.OnControlAdded(e)
            Dim tb As TextBox = TryCast(e.Control, TextBox)
            If tb IsNot Nothing Then
                ' Fonts may be restyled after the child is hosted (Theme.StyleInput).
                AddHandler tb.FontChanged, Sub(s As Object, ev As EventArgs) FitChild(tb)
                FitChild(tb)
            End If
        End Sub

        Protected Overrides Sub OnResize(e As EventArgs)
            MyBase.OnResize(e)
            Theme.RoundControl(Me, Theme.RadiusButton)
            For Each c As Control In Controls
                FitChild(c)
            Next
        End Sub

        Protected Overrides Sub OnPaint(e As PaintEventArgs)
            MyBase.OnPaint(e)
            Dim borderColor As Color = If(_focused, Theme.Info, Theme.BorderLine)
            Dim path As GraphicsPath = Theme.RoundedPath(New Rectangle(0, 0, ClientSize.Width - 1, ClientSize.Height - 1), Theme.RadiusButton)
            Using pen As New Pen(borderColor, If(_focused, 1.6F, 1.0F))
                e.Graphics.SmoothingMode = SmoothingMode.AntiAlias
                e.Graphics.DrawPath(pen, path)
            End Using
            path.Dispose()
        End Sub

    End Class

    ''' <summary>
    ''' 23px fully-rounded status chip; green/blue/orange/neutral variants, 11px semibold text.
    ''' </summary>
    Public Class StatusChip
        Inherits Control

        Private _tone As ThemeTone

        Public Property Tone As ThemeTone
            Get
                Return _tone
            End Get
            Set(value As ThemeTone)
                _tone = value
                Invalidate()
            End Set
        End Property

        Public Sub New()
            SetStyle(ControlStyles.AllPaintingInWmPaint Or ControlStyles.OptimizedDoubleBuffer Or
                     ControlStyles.ResizeRedraw Or ControlStyles.UserPaint Or
                     ControlStyles.SupportsTransparentBackColor, True)
            Font = Theme.FontChip
            Height = Theme.HeightChip
            _tone = ThemeTone.Neutral
            Theme.SetDoubleBuffered(Me, True)
        End Sub

        Public Sub New(text As String, tone As ThemeTone)
            Me.New()
            Text = text
            _tone = tone
        End Sub

        Public Sub SetChip(text As String, tone As ThemeTone)
            Text = text
            _tone = tone
            RecalcSize()
            Invalidate()
        End Sub

        Protected Overrides Sub OnTextChanged(e As EventArgs)
            MyBase.OnTextChanged(e)
            RecalcSize()
            Invalidate()
        End Sub

        ''' <summary>Sizes the chip to its text + 9px side padding, per design.</summary>
        Public Sub RecalcSize()
            Dim w As Integer = TextRenderer.MeasureText(Text, Font, Size.Empty,
                                                        TextFormatFlags.SingleLine Or TextFormatFlags.NoPrefix).Width + 18
            If w < 24 Then w = 24
            Size = New Size(w, Theme.HeightChip)
        End Sub

        Private Sub ToneColors(ByRef bg As Color, ByRef fg As Color)
            Select Case _tone
                Case ThemeTone.Success
                    bg = Theme.GreenTint : fg = Theme.Green
                Case ThemeTone.Info
                    bg = Theme.InfoTint : fg = Theme.Info
                Case ThemeTone.Warn
                    bg = Theme.WarnTint : fg = Theme.Warn
                Case ThemeTone.Error
                    bg = Theme.ErrorTint : fg = Theme.ErrorText
                Case Else
                    bg = Theme.GridHeader : fg = Theme.Muted
            End Select
        End Sub

        Protected Overrides Sub OnPaint(e As PaintEventArgs)
            Dim bg As Color, fg As Color
            ToneColors(bg, fg)
            Dim rect As New Rectangle(0, 0, Width - 1, Height - 1)
            Using path As GraphicsPath = Theme.RoundedPath(rect, (Height - 1) \ 2)
                Using b As New SolidBrush(bg)
                    e.Graphics.SmoothingMode = SmoothingMode.AntiAlias
                    e.Graphics.FillPath(b, path)
                End Using
            End Using
            TextRenderer.DrawText(e.Graphics, Text, Font, rect, fg,
                                  TextFormatFlags.HorizontalCenter Or TextFormatFlags.SingleLine Or
                                  TextFormatFlags.NoPadding Or TextFormatFlags.NoPrefix)
        End Sub

    End Class

    ''' <summary>
    ''' Inline notice line (info/warn/success/error) with tinted background and icon.
    ''' </summary>
    Public Class InlineNotice
        Inherits Panel

        Private _tone As ThemeTone = ThemeTone.Info
        Private _text As String = String.Empty
        Private _textRect As New Rectangle()

        Public Sub New()
            SetStyle(ControlStyles.AllPaintingInWmPaint Or ControlStyles.OptimizedDoubleBuffer Or
                     ControlStyles.ResizeRedraw Or ControlStyles.UserPaint, True)
            Height = 34
            Theme.SetDoubleBuffered(Me, True)
        End Sub

        Public Sub SetNotice(text As String, tone As ThemeTone)
            _text = text
            _tone = tone
            Invalidate()
        End Sub

        Public Sub Clear()
            _text = String.Empty
            Invalidate()
        End Sub

        Protected Overrides Sub OnResize(e As EventArgs)
            MyBase.OnResize(e)
            Theme.RoundControl(Me, Theme.RadiusButton)
        End Sub

        Protected Overrides Sub OnPaint(e As PaintEventArgs)
            Dim iconColor As Color, textColor As Color, bgColor As Color
            PickColors(iconColor, textColor, bgColor)

            Using b As New SolidBrush(bgColor)
                e.Graphics.FillRectangle(b, ClientRectangle)
            End Using
            If _text.Length > 0 Then
                Dim iconName As String = If(_tone = ThemeTone.Warn, "alert-circle",
                                            If(_tone = ThemeTone.[Error], "alert-circle",
                                               If(_tone = ThemeTone.Success, "check", "info-circle")))
                Dim icon As Bitmap = IconArt.Get(iconName, iconColor, 15)
                Dim iconY As Integer = (Height - 15) \ 2
                If icon IsNot Nothing Then
                    e.Graphics.DrawImage(icon, 13, iconY, 15, 15)
                End If
                _textRect.X = 36
                _textRect.Y = 0
                _textRect.Width = Width - 48
                _textRect.Height = Height
                TextRenderer.DrawText(e.Graphics, _text, Theme.FontBody12, _textRect, textColor,
                                      TextFormatFlags.VerticalCenter Or TextFormatFlags.SingleLine Or
                                      TextFormatFlags.EndEllipsis Or TextFormatFlags.NoPrefix)
            End If
        End Sub

        Private Sub PickColors(ByRef iconColor As Color, ByRef textColor As Color, ByRef bgColor As Color)
            Select Case _tone
                Case ThemeTone.Warn
                    bgColor = Theme.WarnTint : textColor = Theme.Warn : iconColor = Theme.Warn
                Case ThemeTone.Success
                    bgColor = Theme.GreenTint : textColor = Theme.Green : iconColor = Theme.Green
                Case ThemeTone.[Error]
                    bgColor = Theme.ErrorTint : textColor = Theme.ErrorText : iconColor = Theme.ErrorText
                Case Else
                    bgColor = Theme.InfoTint : textColor = Theme.Info : iconColor = Theme.Info
            End Select
        End Sub

    End Class

    ''' <summary>
    ''' 7px progress rail used by the import status card.
    ''' </summary>
    Public Class ProgressRail
        Inherits Control

        Private _value As Double = 0.0
        Private _fillColor As Color = Theme.Info
        Private _trackColor As Color = Color.FromArgb(228, 233, 230)

        Public Property Value As Double
            Get
                Return _value
            End Get
            Set(value As Double)
                _value = Math.Max(0.0, Math.Min(100.0, value))
                Invalidate()
            End Set
        End Property

        Public Property FillColor As Color
            Get
                Return _fillColor
            End Get
            Set(value As Color)
                _fillColor = value
                Invalidate()
            End Set
        End Property

        Public Property TrackColor As Color
            Get
                Return _trackColor
            End Get
            Set(value As Color)
                _trackColor = value
                Invalidate()
            End Set
        End Property

        Public Sub New()
            SetStyle(ControlStyles.AllPaintingInWmPaint Or ControlStyles.OptimizedDoubleBuffer Or
                     ControlStyles.ResizeRedraw Or ControlStyles.UserPaint, True)
            Height = 7
            Theme.SetDoubleBuffered(Me, True)
        End Sub

        Protected Overrides Sub OnPaint(e As PaintEventArgs)
            Dim trackRect As New Rectangle(0, 0, Width - 1, Height - 1)
            Using b As New SolidBrush(_trackColor)
                Using path As GraphicsPath = Theme.RoundedPath(trackRect, (Height - 1) \ 2)
                    e.Graphics.SmoothingMode = SmoothingMode.AntiAlias
                    e.Graphics.FillPath(b, path)
                    path.Dispose()
                End Using
            End Using
            Dim fillW As Integer = CInt((Width - 2) * _value / 100.0)
            If fillW > 0 Then
                Dim fillRect As New Rectangle(1, 1, fillW, Height - 2)
                Using b As New SolidBrush(_fillColor)
                    Using path As GraphicsPath = Theme.RoundedPath(fillRect, (Height - 2) \ 2)
                        e.Graphics.SmoothingMode = SmoothingMode.AntiAlias
                        e.Graphics.FillPath(b, path)
                        path.Dispose()
                    End Using
                End Using
            End If
        End Sub

    End Class

End Namespace
