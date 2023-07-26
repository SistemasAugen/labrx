Imports System.Drawing
Imports System.Drawing.Drawing2D
Imports System.Drawing.Imaging
Imports System.Drawing.Text
Imports System.Math

Public Class Pastilla

    Public Radios(511) As Integer
    Public World As Matrix
    Public Location As Point
    Public Size As Size
    Public Radio As Single
    Public AnchoOblea As Single '= 2800
    Public AltoOblea As Single '= 2800
    Public Scale As Single
    Public Type As Types
    Public Side As Sides
    Public ExcH As Single '= 0
    Public ExcV As Single '= 0
    Public Inset As Single
    Public Drop As Single
    Public Height As Single '= 2200
    Public Pupila As Point
    Public Enum Sides
        Left
        Right
    End Enum
    Public Enum Types
        Monofocal
        FlatTop
        Progressive
    End Enum

    Public Function DrawPastilla(ByRef PBox As PictureBox, ByVal PuntoPupilar As Point, ByVal AlturaPupila As Single, ByVal AlturaObleaLente As Single) As GraphicsPath
        Pupila = PuntoPupilar
        Dim InternalOffset As Single = 0
        Select Case Type
            Case Types.Monofocal
                Location.X = PuntoPupilar.X
                Location.Y = PuntoPupilar.Y
            Case Types.FlatTop
                If Side = Sides.Right Then
                    Location.X = PuntoPupilar.X - ExcH
                Else
                    Location.X = PuntoPupilar.X - ExcH
                End If
                Location.Y = PuntoPupilar.Y
            Case Types.Progressive
                If Side = Sides.Right Then
                    Location.X = PuntoPupilar.X - ExcH
                Else
                    Location.X = PuntoPupilar.X - ExcH
                End If
                'Location.Y = PuntoPupilar.Y + ExcV
                Location.Y = PuntoPupilar.Y - ExcV
        End Select
        Dim plot1 As New GraphicsPath()
        Dim RadioOblea As Single = AnchoOblea / 2
        If Type = Types.FlatTop Then
            Dim CO As Double = RadioOblea - (AnchoOblea - AltoOblea)
            Dim StartAngle As Single = Math.Asin(CO / RadioOblea) * (180 / Math.PI)
            InternalOffset = CO
            '            InternalOffset = -(AltoOblea - RadioOblea) + AlturaObleaLente
            If Side = Sides.Right Then

                plot1.AddArc(Inset - RadioOblea, -Drop - InternalOffset - RadioOblea, AnchoOblea, AnchoOblea, 180 - StartAngle, 180 + (StartAngle * 2))
                plot1.CloseFigure()
            Else
                plot1.AddArc(-Inset - RadioOblea, -Drop - InternalOffset - RadioOblea, AnchoOblea, AnchoOblea, 180 - StartAngle, 180 + (StartAngle * 2))
                plot1.CloseFigure()
            End If
        ElseIf Type = Types.Progressive Then
            If Side = Sides.Right Then
                '              plot1.AddEllipse(+ExcH - RadioOblea, -ExcV - Drop - InternalOffset - RadioOblea, AnchoOblea, AnchoOblea)
                plot1.AddEllipse(+Inset - (AnchoOblea / 2), -Drop - (AnchoOblea / 2), AnchoOblea, AnchoOblea)
            Else
                plot1.AddEllipse(-Inset - (AnchoOblea / 2), -Drop - (AnchoOblea / 2), AnchoOblea, AnchoOblea)
            End If
        End If
        plot1.AddLine(-Radio, 0, Radio, 0)
        plot1.AddEllipse(-Radio, -Radio, Size.Width, Size.Height)
        plot1.AddLine(0, -Radio, 0, Radio)

        'If Type = Types.FlatTop Then
        '    ' 
        '    Location.Y += (Drop - AlturaPupila + AlturaObleaLente)
        '    Pupila.Y += (Drop - AlturaPupila + AlturaObleaLente)
        'ElseIf Type = Types.Progressive Then
        '    ' La altura del la pastilla se recorre ExcV
        '    Location.Y += ExcV
        'ElseIf Type = Types.Monofocal Then
        '    ' El centro de la pastilla se queda en el centro de la pupila
        '    Location = Pupila
        'End If

        World = New Matrix()
        World.Scale(Scale, -Scale)
        World.Translate(Location.X, Location.Y)
        plot1.Transform(World)
        Return plot1

    End Function

    Public Sub New(ByVal Type As Types, ByVal Side As Sides, ByVal Diameter As Single, ByVal AnchoOblea As Single, ByVal AltoOblea As Single, ByVal Scale As Single, ByVal ExcH As Single, ByVal ExcV As Single, ByVal Inset As Single, ByVal Drop As Single)
        Size = New Size(Diameter * 100, Diameter * 100)
        Location = New Point(0, 0)
        Me.Scale = Scale
        Me.Radio = Diameter / 2 * 100
        Me.Type = Type
        Me.Side = Side
        Me.AnchoOblea = AnchoOblea * 100
        Me.AltoOblea = AltoOblea * 100
        Me.ExcH = ExcH * 100
        Me.ExcV = ExcV * 100
        Me.Inset = Inset * 100
        Me.Drop = Drop * 100
        Select Case Type
            Case Types.Monofocal
                Pupila = New Point(0, 0)
            Case Types.FlatTop
                'Pupila = New Point((-Inset - 5 + ExcH) * 100, (5 + Drop + ExcV) * 100)
                Pupila = New Point((-Inset + ExcH) * 100, (Drop + ExcV) * 100)
            Case Types.Progressive
                Pupila = New Point(Inset * 100, Drop * 100)
        End Select

    End Sub


End Class
