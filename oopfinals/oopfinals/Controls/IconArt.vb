' ==============================================================================
' Project: Mini-Grocery Store POS & Daily Inventory System
' Module:  Controls / IconArt  (UI RESTYLE)
' Purpose: Lucide-style vector glyphs rendered at runtime (GDI+) into cached
'          bitmaps, in any color and size. Used by the icon tab bar (15px) and
'          inline notices / empty states. No external image resources required.
' ==============================================================================

Option Strict On
Option Explicit On

Imports System
Imports System.Collections.Generic
Imports System.Drawing
Imports System.Drawing.Drawing2D
Imports System.Windows.Forms

Namespace Controls

    Public NotInheritable Class IconArt

        Private Sub New()
        End Sub

        ' Glyphs are drawn on a 24x24 logical canvas, then scaled to the target size.
        Private Const Canvas As Single = 24.0F

        Private Shared ReadOnly _cache As New Dictionary(Of String, Bitmap)()

        ''' <summary>Returns a cached bitmap for the named glyph; thread-safe enough for UI-only use.</summary>
        Public Shared Function [Get](name As String, color As Color, Optional size As Integer = 15) As Bitmap
            Dim key As String = name & "|" & color.ToArgb().ToString("X8") & "|" & size.ToString()
            Dim bmp As Bitmap = Nothing
            If _cache.TryGetValue(key, bmp) Then Return bmp

            bmp = New Bitmap(size, size)
            Using g As Graphics = Graphics.FromImage(bmp)
                g.SmoothingMode = SmoothingMode.AntiAlias
                Using pen As New Pen(color, 2.2F)
                    pen.StartCap = LineCap.Round
                    pen.EndCap = LineCap.Round
                    pen.LineJoin = LineJoin.Round
                    g.ScaleTransform(size / Canvas, size / Canvas)
                    Draw(g, pen, name)
                End Using
            End Using
            _cache(key) = bmp
            Return bmp
        End Function

        ''' <summary>Draws the named glyph on a 24x24 canvas with the supplied pen.</summary>
        Public Shared Sub Draw(g As Graphics, p As Pen, name As String)
            Select Case (If(name, String.Empty)).ToLowerInvariant()
                Case "basket" ' shopping-basket
                    g.DrawArc(p, 5.5F, 1.8F, 13.0F, 9.0F, 180, 180)
                    Dim body As PointF() = {
                        New PointF(2.5F, 9.0F), New PointF(21.5F, 9.0F),
                        New PointF(18.8F, 20.0F), New PointF(5.2F, 20.0F)}
                    g.DrawPolygon(p, body)
                    g.DrawLine(p, 8.2F, 9.0F, 9.3F, 19.6F)
                    g.DrawLine(p, 12.0F, 9.0F, 12.0F, 19.6F)
                    g.DrawLine(p, 15.8F, 9.0F, 14.7F, 19.6F)

                Case "receipt" ' receipt-text
                    Dim zig As PointF() = {
                        New PointF(5.5F, 2.5F), New PointF(18.5F, 2.5F), New PointF(18.5F, 21.5F),
                        New PointF(16.0F, 19.4F), New PointF(13.5F, 21.5F), New PointF(11.0F, 19.4F),
                        New PointF(8.5F, 21.5F), New PointF(6.2F, 19.4F), New PointF(5.5F, 21.0F)}
                    g.DrawLines(p, zig)
                    g.DrawLine(p, 8.8F, 8.0F, 15.2F, 8.0F)
                    g.DrawLine(p, 8.8F, 12.0F, 15.2F, 12.0F)

                Case "user" ' user-round
                    g.DrawEllipse(p, 8.2F, 3.9F, 7.6F, 7.6F)
                    g.DrawBezier(p, 4.5F, 20.5F, 6.0F, 13.8F, 18.0F, 13.8F, 19.5F, 20.5F)

                Case "scan" ' scan-barcode
                    ' corner brackets
                    g.DrawLine(p, 3.0F, 8.0F, 3.0F, 4.5F) : g.DrawLine(p, 3.0F, 4.5F, 8.0F, 4.5F)
                    g.DrawLine(p, 16.0F, 4.5F, 20.0F, 4.5F) : g.DrawLine(p, 20.0F, 4.5F, 20.0F, 8.0F)
                    g.DrawLine(p, 20.0F, 16.0F, 20.0F, 19.5F) : g.DrawLine(p, 20.0F, 19.5F, 16.0F, 19.5F)
                    g.DrawLine(p, 8.0F, 19.5F, 3.0F, 19.5F) : g.DrawLine(p, 3.0F, 19.5F, 3.0F, 16.0F)
                    ' bars
                    g.DrawLine(p, 8.0F, 9.5F, 8.0F, 14.5F)
                    g.DrawLine(p, 12.0F, 9.5F, 12.0F, 14.5F)
                    g.DrawLine(p, 16.0F, 9.5F, 16.0F, 14.5F)

                Case "dashboard" ' layout-dashboard
                    g.DrawRectangle(p, 3.0F, 4.0F, 8.0F, 8.0F)
                    g.DrawRectangle(p, 13.5F, 4.0F, 7.5F, 5.5F)
                    g.DrawRectangle(p, 13.5F, 12.0F, 7.5F, 8.0F)
                    g.DrawRectangle(p, 3.0F, 14.5F, 8.0F, 5.5F)

                Case "users" ' users-round
                    g.DrawEllipse(p, 5.4F, 4.4F, 7.2F, 7.2F)
                    g.DrawBezier(p, 2.2F, 20.0F, 3.4F, 13.6F, 14.8F, 13.6F, 16.0F, 20.0F)
                    g.DrawEllipse(p, 15.4F, 5.6F, 5.2F, 5.2F)
                    g.DrawBezier(p, 16.2F, 13.9F, 18.6F, 13.4F, 21.2F, 16.0F, 21.5F, 18.8F)

                Case "package"
                    Dim box As PointF() = {
                        New PointF(12.0F, 2.8F), New PointF(21.0F, 7.2F), New PointF(21.0F, 16.8F),
                        New PointF(12.0F, 21.2F), New PointF(3.0F, 16.8F), New PointF(3.0F, 7.2F)}
                    g.DrawPolygon(p, box)
                    g.DrawLine(p, 3.0F, 7.2F, 12.0F, 11.6F)
                    g.DrawLine(p, 12.0F, 11.6F, 21.0F, 7.2F)
                    g.DrawLine(p, 12.0F, 11.6F, 12.0F, 21.2F)

                Case "tags"
                    Dim tag As PointF() = {
                        New PointF(12.8F, 3.5F), New PointF(20.5F, 3.5F), New PointF(20.5F, 11.2F),
                        New PointF(12.4F, 19.3F), New PointF(8.3F, 15.2F)}
                    g.DrawPolygon(p, tag)
                    g.DrawEllipse(p, 17.2F, 4.6F, 2.3F, 2.3F)
                    Dim tag2 As PointF() = {
                        New PointF(10.6F, 7.4F), New PointF(6.2F, 11.8F), New PointF(10.6F, 16.2F)}
                    g.DrawLines(p, tag2)

                Case "chart" ' chart-no-axes-combined
                    g.DrawLines(p, New PointF() {
                        New PointF(3.0F, 17.5F), New PointF(9.0F, 10.5F),
                        New PointF(13.5F, 13.8F), New PointF(21.0F, 5.5F)})

                Case "leaf"
                    g.DrawBezier(p, 20.0F, 3.8F, 7.0F, 5.5F, 4.0F, 14.0F, 4.0F, 20.5F)
                    g.DrawBezier(p, 20.0F, 3.8F, 18.5F, 16.5F, 10.5F, 20.2F, 4.0F, 20.5F)
                    g.DrawBezier(p, 18.6F, 5.4F, 13.0F, 10.0F, 8.5F, 14.5F, 4.8F, 19.8F)

                Case "search"
                    g.DrawEllipse(p, 4.6F, 4.6F, 11.8F, 11.8F)
                    g.DrawLine(p, 14.4F, 14.4F, 20.0F, 20.0F)

                Case "chevron-right"
                    g.DrawLines(p, New PointF() {New PointF(9.2F, 5.8F), New PointF(15.6F, 12.0F), New PointF(9.2F, 18.2F)})

                Case "chevron-left"
                    g.DrawLines(p, New PointF() {New PointF(14.8F, 5.8F), New PointF(8.4F, 12.0F), New PointF(14.8F, 18.2F)})

                Case "chevron-down"
                    g.DrawLines(p, New PointF() {New PointF(5.8F, 9.2F), New PointF(12.0F, 15.6F), New PointF(18.2F, 9.2F)})

                Case "chevron-up"
                    g.DrawLines(p, New PointF() {New PointF(5.8F, 14.8F), New PointF(12.0F, 8.4F), New PointF(18.2F, 14.8F)})

                Case "x"
                    g.DrawLine(p, 6.5F, 6.5F, 17.5F, 17.5F)
                    g.DrawLine(p, 17.5F, 6.5F, 6.5F, 17.5F)

                Case "check"
                    g.DrawLines(p, New PointF() {New PointF(5.0F, 12.6F), New PointF(10.0F, 17.4F), New PointF(19.0F, 7.4F)})

                Case "clock"
                    g.DrawEllipse(p, 4.0F, 4.0F, 16.0F, 16.0F)
                    g.DrawLines(p, New PointF() {New PointF(12.0F, 7.4F), New PointF(12.0F, 12.0F), New PointF(15.4F, 13.8F)})

                Case "alert-circle"
                    g.DrawEllipse(p, 4.0F, 4.0F, 16.0F, 16.0F)
                    g.DrawLine(p, 12.0F, 7.6F, 12.0F, 12.8F)
                    Dim dot As RectangleF = New RectangleF(11.2F, 15.7F, 1.6F, 1.6F)
                    g.DrawEllipse(p, dot)

                Case "info-circle"
                    g.DrawEllipse(p, 4.0F, 4.0F, 16.0F, 16.0F)
                    g.DrawLine(p, 12.0F, 11.0F, 12.0F, 16.4F)
                    g.DrawEllipse(p, New RectangleF(11.2F, 7.6F, 1.6F, 1.6F))

                Case "shield"
                    g.DrawLines(p, New PointF() {New PointF(12.0F, 2.5F), New PointF(20.0F, 5.5F)})
                    g.DrawLines(p, New PointF() {New PointF(12.0F, 2.5F), New PointF(4.0F, 5.5F)})
                    Dim shAsSuccess As PointF() = {
                        New PointF(20.0F, 5.5F), New PointF(20.0F, 11.0F), New PointF(17.5F, 17.0F),
                        New PointF(12.0F, 21.3F), New PointF(6.5F, 17.0F), New PointF(4.0F, 11.0F), New PointF(4.0F, 5.5F)}
                    g.DrawLines(p, shAsSuccess)

                Case "lock"
                    g.DrawRectangle(p, 5.5F, 10.5F, 13.0F, 10.0F)
                    g.DrawArc(p, 8.5F, 3.5F, 7.0F, 9.0F, 180, 180)

                Case Else
                    ' Unknown glyph: draw a neutral circle placeholder.
                    g.DrawEllipse(p, 4.0F, 4.0F, 16.0F, 16.0F)
            End Select
        End Sub

    End Class

End Namespace
