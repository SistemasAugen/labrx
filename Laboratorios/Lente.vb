Imports System.Drawing
Imports System.Drawing.Drawing2D
Imports System.Drawing.Imaging
Imports System.Drawing.Text
Imports System.Math
Public Class Lente
    Public Radios(NbrsTraces()) As Integer
    Public XSize As Integer
    Public World As Matrix
    Public PBox As PictureBox
    Public Size As Size
    Public Location As Point
    Public Scale As Single
    Dim Side As Sides
    Public DIP As Integer
    Dim Altura As Integer
    Public Diagonal As Single


    Enum Sides
        Left
        Right
    End Enum
    Public Function NbrsTraces() As Integer
        Select Case My.Settings.OpticalProtocol
            Case "DVI"
                Return 511
            Case "OMA"
                Return 399
            Case Else
                Return 0
        End Select
    End Function
    Function GetGraphicsObject(ByVal PBox As PictureBox) As Graphics
        Dim bmp As New Bitmap(PBox.Width, PBox.Height)
        Try
            If PBox.Image.Equals(Nothing) Then
                PBox.Image = bmp
            Else
                bmp = PBox.Image
            End If
        Catch
            PBox.Image = bmp
        End Try
        Dim G As Graphics
        G = Graphics.FromImage(bmp)
        Return G
    End Function
    Public Sub CalculaContorno()
        Dim MaxRadio As Integer = 0

        '**********VARIABLES QUE DETERMINAN EL VALOR DE LOS EJES EN CUESTION A CANTIDA DE RADIOS, EJ 180 GRADOS = 256 RADIOS, 90 GRADOS = 128 RADIOS, PARA EL CASO DE 512 TRAZOS
        Dim X1, X2, X3, X4 As Integer
        Select Case My.Settings.OpticalProtocol
            Case "DVI"
                X1 = 128
                X2 = 256
                X3 = 384
                X4 = 512
            Case "OMA"
                X1 = 100
                X2 = 200
                X3 = 300
                X4 = 400
        End Select
        '******************************************************************************************************************



        Dim i As Integer = 0
        Dim index As Integer = 0
        For i = 0 To Radios.GetLength(0) - 1
            If ((Side = Sides.Right) And ((i <= X2) And (i > X1))) Then
                MaxRadio = Math.Max(Radios(i), MaxRadio)
                If MaxRadio = Radios(i) Then
                    index = i
                End If
            ElseIf ((Side = Sides.Left) And (i <= 90)) Then
                MaxRadio = Math.Max(Radios(i), MaxRadio)
                If MaxRadio = Radios(i) Then
                    index = i
                End If
            End If
        Next
        If index < X2 Then
            Diagonal = (Radios(index) + Radios(index + X2)) / 100
        Else
            Diagonal = (Radios(0) + Radios(X2)) / 100
        End If

    End Sub

    Public Function DrawLense(ByRef PBox As PictureBox, ByVal Center As Point) As GraphicsPath
        Me.Location = Center
        Dim G As Graphics = GetGraphicsObject(PBox)
        G.SmoothingMode = SmoothingMode.AntiAlias
        G.Clear(PBox.BackColor)
        Dim angulo As Double = 360 / Radios.GetLength(0)
        Dim x, y, x1, y1 As Single
        Dim i As Integer
        Dim Plot As New GraphicsPath()


        '**********VARIABLES QUE DETERMINAN EL VALOR DE LOS EJES EN CUESTION A CANTIDA DE RADIOS, EJ 180 GRADOS = 256 RADIOS, 90 GRADOS = 128 RADIOS, PARA EL CASO DE 512 TRAZOS
        Dim X4 As Integer
        Select Case My.Settings.OpticalProtocol
            Case "DVI"
                X4 = 128
            Case "OMA"
                X4 = 100
        End Select
        '******************************************************************************************************************

        World = New Matrix()
        World.Scale(Scale, -Scale)
        World.Translate(Convert.ToSingle(Center.X), Convert.ToSingle(Center.Y))
        Dim initx, inity As Single
        Dim init As Boolean = True
        Dim hsize As Integer = 0
        Dim vsize As Integer = 0
        Dim MaxRadio As Integer = 0
        Dim index As Integer = 0
        For i = 0 To Radios.GetLength(0) - 1
            Dim ang As Double = angulo * i
            If Not init Then
                x1 = x
                y1 = y
            End If
            y = (Math.Sin((ang * (Math.PI / 180)))) * Radios(i)
            x = ((Math.Sin(Math.PI / 2 - (ang * (Math.PI / 180))))) * Radios(i)
            If i = 0 Then
                hsize = x * 2
            End If
            If i = X4 Then
                vsize = y * 2
            End If
            If init Then
                x1 = x
                y1 = y
                initx = x
                inity = y
                init = False
            End If
            Plot.AddLine(x1, y1, x, y)
        Next
        CalculaContorno()

        Size = New Size(hsize, vsize)
        Plot.AddLine(x, y, initx, inity)

        Plot.AddEllipse(-75, -75, 150, 150)

        Dim plotpen As New Pen(Color.DarkBlue)
        Plot.Transform(World)

        Return Plot

    End Function

    Public Sub New(ByVal Side As Sides, ByVal Puntos() As Integer, ByVal Scale As Single)
        Radios = Puntos
        XSize = Scale
        Dim xmax As Integer = Integer.MinValue
        Dim ymax As Integer = Integer.MinValue
        Dim angulo As Double = 360 / Radios.GetLength(0)
        Dim MaxRadio As Integer = 0
        Dim index As Integer = 0
        Me.Scale = Scale
        Me.Side = Side
    End Sub
End Class
