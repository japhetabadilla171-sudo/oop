' ==============================================================================
' Project: Mini-Grocery Store POS & Daily Inventory System
' Module:  Controls / IconTabBar  (UI RESTYLE)
' Purpose: 46px icon tab bar (15px Lucide-style icon + label, 10px padding).
'          Active tab: #EAF4EB bg, 3px #2E7D32 bottom border, green semibold text.
'          Inactive: #657169 regular. Overflows scroll with chevron arrows.
'          ShellTabs.Apply re-hosts the EXISTING TabControl (kept name + events)
'          below the bar and hides the native tab strip.
' ==============================================================================

Option Strict On
Option Explicit On

Imports System
Imports System.Collections.Generic
Imports System.Drawing
Imports System.Windows.Forms

Namespace Controls

    ''' <summary>Describes one entry of the icon tab bar.</summary>
    Public Class ShellTabSpec
        Public Property Page As TabPage
        Public Property Label As String
        Public Property Icon As String

        Public Sub New(page As TabPage, label As String, icon As String)
            Me.Page = page
            Me.Label = label
            Me.Icon = icon
        End Sub
    End Class

    ''' <summary>The custom-painted icon tab bar control.</summary>
    Public Class IconTabBar
        Inherits Control

        Private Class TabItem
            Public Label As String
            Public Icon As String
            Public Width As Integer
        End Class

        Public Event SelectedChanged(sender As Object, index As Integer)

        Private ReadOnly _items As New List(Of TabItem)
        Private _selected As Integer = 0
        Private _hover As Integer = -1
        Private _scroll As Integer = 0

        Private Const LeftPad As Integer = 8
        Private Const ItemSidePad As Integer = 10
        Private Const IconSize As Integer = 15
        Private Const IconTextGap As Integer = 8
        Private Const ArrowZone As Integer = 26

        Public Sub New()
            SetStyle(ControlStyles.AllPaintingInWmPaint Or ControlStyles.OptimizedDoubleBuffer Or
                     ControlStyles.ResizeRedraw Or ControlStyles.UserPaint, True)
            Height = Theme.HeightTabBar
            BackColor = Theme.White
            ForeColor = Theme.Muted
            Font = Theme.FontBody12
            Cursor = Cursors.Hand
            Theme.SetDoubleBuffered(Me, True)
        End Sub

        Public ReadOnly Property ItemCount As Integer
            Get
                Return _items.Count
            End Get
        End Property

        Public Property SelectedIndex As Integer
            Get
                Return _selected
            End Get
            Set(value As Integer)
                If value < -1 OrElse value >= _items.Count Then value = -1
                If _selected = value Then Return
                _selected = value
                Invalidate()
                RaiseEvent SelectedChanged(Me, _selected)
            End Set
        End Property

        Public Sub AddItem(label As String, icon As String)
            Dim ti As New TabItem()
            ti.Label = label
            ti.Icon = icon
            Dim textW As Integer = TextRenderer.MeasureText(label, Theme.FontBody12Semi, Size.Empty,
                                                            TextFormatFlags.SingleLine Or TextFormatFlags.NoPrefix).Width
            ti.Width = ItemSidePad + IconSize + IconTextGap + textW + ItemSidePad
            _items.Add(ti)
            ClampScroll()
            Invalidate()
        End Sub

        Private ReadOnly Property TotalWidth As Integer
            Get
                Dim w As Integer = LeftPad
                For Each it As TabItem In _items
                    w += it.Width
                Next
                Return w
            End Get
        End Property

        Private Sub ClampScroll()
            Dim max As Integer = TotalWidth + ArrowZone - ClientSize.Width
            If max < 0 Then max = 0
            If _scroll > max Then _scroll = max
            If _scroll < 0 Then _scroll = 0
        End Sub

        Private Function Overflowing() As Boolean
            Return TotalWidth > ClientSize.Width - 4
        End Function

        Private Function ItemRect(index As Integer) As Rectangle
            Dim x As Integer = LeftPad - _scroll
            For i As Integer = 0 To index - 1
                x += _items(i).Width
            Next
            Return New Rectangle(x, 0, _items(index).Width, ClientSize.Height)
        End Function

        Private Function HitItem(x As Integer) As Integer
            Dim rect As Rectangle
            For i As Integer = 0 To _items.Count - 1
                rect = ItemRect(i)
                If x >= rect.X AndAlso x <= rect.Right Then Return i
                If rect.X > Width Then Exit For
            Next
            Return -1
        End Function

        Protected Overrides Sub OnResize(e As EventArgs)
            MyBase.OnResize(e)
            ClampScroll()
            Invalidate()
        End Sub

        Protected Overrides Sub OnPaintBackground(e As PaintEventArgs)
            MyBase.OnPaintBackground(e)
        End Sub

        Protected Overrides Sub OnPaint(e As PaintEventArgs)
            Dim g As Graphics = e.Graphics
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias

            ' 1px bottom border across the full bar
            Using bBottom As New SolidBrush(Theme.BorderLine)
                g.FillRectangle(bBottom, 0, ClientSize.Height - 1, ClientSize.Width, 1)
            End Using

            Dim contentRight As Integer = ClientSize.Width
            Dim overflow As Boolean = Overflowing()
            If overflow Then contentRight -= 2 * ArrowZone

            Dim icon As Bitmap
            Dim tColor As Color
            Dim tFont As Font

            For i As Integer = 0 To _items.Count - 1
                Dim r As Rectangle = ItemRect(i)
                If r.Right < 0 OrElse r.Left > contentRight Then Continue For

                Dim hovered As Boolean = (i = _hover AndAlso i <> _selected)
                If i = _selected Then
                    Dim tabRect As New Rectangle(r.X, 0, r.Width, ClientSize.Height - 1)
                    Using b As New SolidBrush(Theme.GreenTint)
                        g.FillRectangle(b, tabRect)
                    End Using
                    ' 3px bottom accent
                    Dim accent As New Rectangle(r.X, ClientSize.Height - 4, r.Width, 3)
                    Using b As New SolidBrush(Theme.Green)
                        g.FillRectangle(b, accent)
                    End Using
                ElseIf hovered Then
                    Using b As New SolidBrush(Theme.HoverPanel)
                        g.FillRectangle(b, r.X, 0, r.Width, ClientSize.Height - 1)
                    End Using
                End If

                tColor = If(i = _selected, Theme.Green, Theme.Muted)
                tFont = If(i = _selected, Theme.FontBody12Semi, Theme.FontBody12)
                icon = IconArt.Get(_items(i).Icon, tColor, IconSize)
                If icon IsNot Nothing Then
                    g.DrawImage(icon, r.X + ItemSidePad, (ClientSize.Height - IconSize) \ 2, IconSize, IconSize)
                End If

                Dim textRect As New Rectangle(r.X + ItemSidePad + IconSize + IconTextGap, 0,
                                              r.Width - (ItemSidePad + IconSize + IconTextGap + ItemSidePad),
                                              ClientSize.Height)
                TextRenderer.DrawText(g, _items(i).Label, tFont, textRect, tColor,
                                      TextFormatFlags.VerticalCenter Or TextFormatFlags.SingleLine Or
                                      TextFormatFlags.NoPadding Or TextFormatFlags.NoPrefix)
            Next

            ' overflow scroll arrows
            If overflow Then
                Dim leftRect As New Rectangle(ClientSize.Width - 2 * ArrowZone, 0, ArrowZone, ClientSize.Height)
                Dim rightRect As New Rectangle(ClientSize.Width - ArrowZone, 0, ArrowZone, ClientSize.Height)
                Dim canLeft As Boolean = _scroll > 0
                Dim canRight As Boolean = _scroll < TotalWidth + ArrowZone - ClientSize.Width

                For Each z As Tuple(Of Rectangle, String, Boolean) In
                    {Tuple.Create(leftRect, "chevron-left", canLeft), Tuple.Create(rightRect, "chevron-right", canRight)}
                    Dim active As Boolean = z.Item3 AndAlso (_hoverSpot = z.Item2 Or _hoverSpot = "scroll")
                    If active Then
                        Using b As New SolidBrush(Theme.HoverPanel)
                            g.FillRectangle(b, z.Item1)
                        End Using
                    End If
                    tColor = If(z.Item3, Theme.Ink, Theme.BorderLine)
                    icon = IconArt.Get(z.Item2, tColor, IconSize)
                    If icon IsNot Nothing Then
                        g.DrawImage(icon, z.Item1.X + (ArrowZone - IconSize) \ 2, (ClientSize.Height - IconSize) \ 2, IconSize, IconSize)
                    End If
                Next
            End If
        End Sub

        Private _hoverSpot As String = String.Empty ' "" | "scroll-left" | "scroll-right"

        Protected Overrides Sub OnMouseMove(e As MouseEventArgs)
            MyBase.OnMouseMove(e)
            Dim newHover As Integer = -1
            Dim newSpot As String = String.Empty
            If Overflowing() AndAlso e.X >= ClientSize.Width - 2 * ArrowZone Then
                newSpot = If(e.X < ClientSize.Width - ArrowZone, "scroll-left", "scroll-right")
            Else
                newHover = HitItem(e.X)
            End If
            If newHover <> _hover OrElse newSpot <> _hoverSpot Then
                _hover = newHover
                _hoverSpot = newSpot
                Invalidate()
            End If
        End Sub

        Protected Overrides Sub OnMouseLeave(e As EventArgs)
            MyBase.OnMouseLeave(e)
            If _hover <> -1 OrElse _hoverSpot.Length > 0 Then
                _hover = -1
                _hoverSpot = String.Empty
                Invalidate()
            End If
        End Sub

        Protected Overrides Sub OnMouseDown(e As MouseEventArgs)
            MyBase.OnMouseDown(e)
            If Overflowing() AndAlso e.X >= ClientSize.Width - 2 * ArrowZone Then
                If e.X < ClientSize.Width - ArrowZone Then
                    _scroll -= 160
                Else
                    _scroll += 160
                End If
                ClampScroll()
                Invalidate()
                Return
            End If
            Dim idx As Integer = HitItem(e.X)
            If idx >= 0 AndAlso idx <> _selected Then
                SelectedIndex = idx
            End If
        End Sub

    End Class

    ''' <summary>
    ''' Builds the shell for a portal form: hides the native TabControl strip and
    ''' adds the icon tab bar above it. Existing tab pages, names and handlers
    ''' are untouched.
    ''' </summary>
    Public NotInheritable Class ShellTabs

        Private Sub New()
        End Sub

        Public Shared Function Apply(tab As TabControl, ParamArray specs As ShellTabSpec()) As IconTabBar
            If tab Is Nothing OrElse specs Is Nothing OrElse specs.Length = 0 Then Return Nothing

            Dim oldParent As Control = tab.Parent
            If oldParent IsNot Nothing Then oldParent.Controls.Remove(tab)

            Dim shell As New Panel()
            shell.Name = "pnlShellTabs"
            shell.Dock = DockStyle.Fill
            shell.BackColor = Theme.PageBg
            If oldParent IsNot Nothing Then
                oldParent.Controls.Add(shell)
                ' keep shell where the tab control was (dock processing order)
                Dim idx As Integer = 0
                Try
                    idx = Math.Max(0, oldParent.Controls.GetChildIndex(tab, False))
                Catch
                    idx = 0
                End Try
                oldParent.Controls.SetChildIndex(shell, idx)
            End If

            ' Page styling
            For i As Integer = 0 To specs.Length - 1
                Dim page As TabPage = specs(i).Page
                If page IsNot Nothing Then
                    page.BackColor = Theme.PageBg
                    page.Padding = New Padding(16)
                End If
            Next

            ' Build the bar
            Dim bar As New IconTabBar() With {.Dock = DockStyle.Top, .Name = "iconTabBar"}
            Dim pageList As New List(Of TabPage)
            For Each s As ShellTabSpec In specs
                bar.AddItem(s.Label, s.Icon)
                s.Page.Tag = s
                pageList.Add(s.Page)
            Next

            ' Hide the native strip completely: Fixed honors ItemSize(0, 1).
            ' (Normal auto-sizes items to their text, so the real page captions
            ' painted a stray, clipped strip right under the icon tab bar.)
            tab.SizeMode = TabSizeMode.Fixed
            tab.ItemSize = New Size(0, 1)
            tab.Padding = New Point(0, 0)

            ' Order matters for docking: content first, then the bar strip
            shell.Controls.Add(tab)
            shell.Controls.Add(bar)

            AddHandler bar.SelectedChanged, Sub(sender As Object, index As Integer)
                                                If index >= 0 AndAlso index < tab.TabPages.Count AndAlso tab.SelectedIndex <> index Then
                                                    tab.SelectedIndex = index
                                                End If
                                            End Sub
            AddHandler tab.SelectedIndexChanged, Sub(sender As Object, e As EventArgs)
                                                     If bar.SelectedIndex <> tab.SelectedIndex Then
                                                         bar.SelectedIndex = tab.SelectedIndex
                                                     End If
                                                 End Sub

            If tab.TabPages.Count > 0 Then
                bar.SelectedIndex = Math.Min(0, tab.TabPages.Count - 1)
                tab.SelectedIndex = bar.SelectedIndex
            End If

            Return bar
        End Function

    End Class

End Namespace
