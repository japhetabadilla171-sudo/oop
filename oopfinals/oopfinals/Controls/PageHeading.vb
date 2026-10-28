' ==============================================================================
' Project: Mini-Grocery Store POS & Daily Inventory System
' Module:  Controls / PageHeading  (UI RESTYLE)
' Purpose: Auto-height workspace heading row: page title + full description
'          ("&" shown literally) on the left, chip and action buttons on the
'          right (design "PageHeading").
' ==============================================================================

Option Strict On
Option Explicit On

Imports System
Imports System.Drawing
Imports System.Windows.Forms

Namespace Controls

    Public Class PageHeading
        Inherits Panel

        Public ReadOnly Property TitleLabel As Label
        Public ReadOnly Property DescLabel As Label
        Public ReadOnly Property Actions As FlowLayoutPanel

        Private _built As Boolean

        Public Sub New(title As String, description As String)
            SetStyle(ControlStyles.AllPaintingInWmPaint Or ControlStyles.OptimizedDoubleBuffer Or
                     ControlStyles.ResizeRedraw Or ControlStyles.UserPaint, True)
            BackColor = Theme.PageBg
            Margin = New Padding(0)
            Theme.SetDoubleBuffered(Me, True)

            TitleLabel = New Label()
            TitleLabel.Text = title
            TitleLabel.Font = Theme.FontPageTitle
            TitleLabel.ForeColor = Theme.Ink
            TitleLabel.AutoSize = True
            TitleLabel.UseMnemonic = False ' render "&" literally (e.g. "Categories & suppliers")
            TitleLabel.BackColor = Color.Transparent
            TitleLabel.Location = New Point(0, 0)
            Controls.Add(TitleLabel)

            DescLabel = New Label()
            DescLabel.Text = description
            DescLabel.Font = Theme.FontPageDesc
            DescLabel.ForeColor = Theme.Muted
            DescLabel.AutoSize = True
            DescLabel.UseMnemonic = False
            DescLabel.BackColor = Color.Transparent
            Controls.Add(DescLabel)

            ' The heading is as tall as its title + subtitle (56px only as minimum),
            ' so descriptions are never clipped.
            UpdateHeight()

            Actions = New FlowLayoutPanel()
            Actions.AutoSize = True
            Actions.AutoSizeMode = AutoSizeMode.GrowAndShrink
            Actions.WrapContents = False
            Actions.FlowDirection = FlowDirection.LeftToRight
            Actions.BackColor = Color.Transparent
            Actions.Anchor = AnchorStyles.None
            Controls.Add(Actions)

            PositionParts()

            AddHandler Me.Resize, Sub(s As Object, e As EventArgs) PositionParts()
            AddHandler Actions.Resize, Sub(s As Object, e As EventArgs) PositionParts()
            _built = True
        End Sub

        ''' <summary>Adds this heading to a parent. Call after adding the body/Fill child
        ''' of the same parent so the heading strip lays out first (top).</summary>
        Public Shared Function Build(parent As Control, title As String, description As String) As PageHeading
            Dim h As New PageHeading(title, description)
            h.Dock = DockStyle.Top
            parent.Controls.Add(h)
            Return h
        End Function

        Public Sub AddAction(btn As Control)
            SetVerticalMargin(btn)
            Actions.Controls.Add(btn)
            RepositionActions()
        End Sub

        Public Sub AddChip(chip As StatusChip)
            AddAction(chip)
        End Sub

        Public Sub AddInline(c As Control)
            AddAction(c)
        End Sub

        Public Sub AddLabel(lbl As Label)
            AddAction(lbl)
        End Sub

        Private Sub SetVerticalMargin(c As Control)
            Dim top As Integer = 11
            If TypeOf c Is StatusChip Then top = 16
            If TypeOf c Is Label Then top = 19
            If TypeOf c Is Button Then top = 11
            c.Margin = New Padding(12, top, 0, 0)
        End Sub

        Private Sub RepositionActions()
            If Not _built Then Return
            PositionParts()
        End Sub

        ''' <summary>Sizes the strip to title + subtitle; 56px is only the minimum,
        ''' so long descriptions are never clipped.</summary>
        Private Sub UpdateHeight()
            Dim titleH As Integer = TextRenderer.MeasureText(TitleLabel.Text, TitleLabel.Font).Height
            Dim descH As Integer = TextRenderer.MeasureText(DescLabel.Text, DescLabel.Font).Height
            Height = Math.Max(Theme.HeightPageHeading, titleH + 2 + descH + 8)
        End Sub

        Private Sub PositionParts()
            Dim descY As Integer = TextRenderer.MeasureText(TitleLabel.Text, TitleLabel.Font).Height + 2
            DescLabel.Location = New Point(2, descY)

            Actions.PerformLayout()
            Dim actionsX As Integer = ClientSize.Width - Actions.Width
            If actionsX < 0 Then actionsX = 0
            Dim actionsY As Integer = (ClientSize.Height - Actions.Height) \ 2
            If actionsY < 0 Then actionsY = 0
            Actions.Location = New Point(actionsX, actionsY)
        End Sub

        Protected Overrides Sub OnPaint(e As PaintEventArgs)
            MyBase.OnPaint(e)
        End Sub

    End Class

End Namespace
