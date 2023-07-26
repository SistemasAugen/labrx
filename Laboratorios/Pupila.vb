Imports System.Drawing
Imports System.Drawing.Drawing2D
Imports System.Drawing.Imaging
Imports System.Drawing.Text
Imports System.Math

Public Class Pupila

    Public World As Matrix
    Public Location As Point
    Public Size As Size
    Public Scale As Single
    Public Side As Sides
    Public Inset As Single '= 500
    Public Drop As Single '= 500
    Public Shadows Type As Types
    Public Enum Sides
        Left
        Right
    End Enum
    Public Enum Types
        Monofocal
        FlatTop
        Progressive
    End Enum

    Public Function DrawPupila(ByRef PBox As PictureBox, ByVal Center As Point) As GraphicsPath
        Location = Center
        Dim Plot As New GraphicsPath()
        World = New Matrix()
        World.Scale(Scale, -Scale)
        World.Translate(Location.X, Location.Y)
        If Side = Sides.Right Then
            Plot.AddLine(-250, 0, +250, 0)
            Plot.CloseFigure()
            Plot.AddLine(0, -250, 0, +250)
            Plot.CloseFigure()
        Else
            Plot.AddLine(-250, 0, +250, 0)
            Plot.CloseFigure()
            Plot.AddLine(0, -250, 0, +250)
            Plot.CloseFigure()
        End If
        Plot.Transform(World)
        Return Plot
    End Function

    Public Sub New(ByVal Type As Types, ByVal Side As Sides, ByVal Scale As Single, ByVal Inset As Single, ByVal Drop As Single)
        Location = New Point(0, 0)
        Me.Scale = Scale
        Me.Side = Side
        Me.Inset = Inset * 100
        Me.Drop = Drop * 100
        Select Case Type
            Case Types.Monofocal
                Me.Inset = 0
                Me.Drop = 0
            Case Types.FlatTop
                Me.Inset = Inset * 100
                Me.Drop = 0
            Case Types.Progressive
                Me.Inset = Inset * 100
                Me.Drop = Drop * 100
        End Select
    End Sub
End Class

