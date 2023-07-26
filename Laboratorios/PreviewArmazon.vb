Imports System.Drawing
Imports System.Drawing.Drawing2D
Imports System.Drawing.Imaging
Imports System.Drawing.Text
Imports System.Math
Imports Microsoft.Win32

Public Class PreviewArmazon
    Public OMA As New OMAFile(1, 1, 400)
    Public IsChangingTrace As Boolean
    Dim World As Matrix
    Public RadiosR(NbrsTraces()) As Integer
    Public RadiosL(NbrsTraces()) As Integer
    Public LenteI As Lente
    Public LenteD As Lente
    Dim PlotLenteI As New GraphicsPath
    Dim PlotLenteD As New GraphicsPath

    Dim PastillaI As Pastilla
    Dim PastillaD As Pastilla
    Dim PlotPastillaI As GraphicsPath
    Dim PlotPastillaD As GraphicsPath

    Dim PupilaD As Pupila
    Dim PupilaI As Pupila
    Dim PlotPupilaD As GraphicsPath
    Dim PlotPupilaI As GraphicsPath

    Dim XSize As Integer = 200
    Dim InitialPoint As Point = New Point(5700, -4700)
    Dim OffsetL As Point = New Point(0, 0)
    Dim OffsetR As Point = New Point(0, 0)
    Dim OffsetX As Integer = 0
    Dim OffsetY As Integer = 0
    Public Shadows Scale As Single = 0.039 '0.0500110462

    Public Puente As Single = 22
    Public MonoL As Single = 34 - (Puente / 2)
    Public MonoR As Single = 34 - (Puente / 2)
    Public DIPLejos As Single
    Public DIPCerca As Single
    Public AlturaRight As Single = 13
    Public AlturaLeft As Single = 13
    Public Altura As Single = 13
    Public RefWECO As String = "104100"

    Public ExcHR As Single = 2.5
    Public ExcVR As Single = 4
    Public InsetR As Single = 0
    Public DropR As Single = 13
    Public AltoObleaR As Single = 19
    Public AnchoObleaR As Single = 28
    Public DiametroPastillaR As Single = 72

    Public ExcHL As Single = 2.5
    Public ExcVL As Single = 4
    Public InsetL As Single = 0
    Public DropL As Single = 13
    Public AltoObleaL As Single = 19
    Public AnchoObleaL As Single = 28
    Public DiametroPastillaL As Single = 72

    Public RightPupilaX As Single
    Public RightPupilaY As Single
    Public LeftPupilaX As Single
    Public LeftPupilaY As Single
    Public Mat_Arm As Integer
    Public IsManualFrame As Boolean
    Public BaseL As String
    Public BaseR As String
    Public Sizing As Short
    Public CheckedEyes As Eyes = Eyes.Both

    Public IsReceivingVirtualRx As Boolean = False
    Public UseFFCredit As Boolean = False

    '----------------------------------------
    'variables de grosores de lentes
    Public GrosorcentralD As Single
    Public GrosorcentralI As Single
    '------------------------------------------
    'variable que guarda  la fecha final de la receta 
    Public FechaInicial As String
    Public FechaEntrada As String
    Public Framenum As String

    '------------------
    'Objeto con las variables para imprimir el la receta

    Public RxInfo As New Calcot.RxPrintInfo
    '----------------------------------------------------
    '----------------------------------------------------
    'objeto de impresion para mandar a imprimir desde el form1
    Public PrintObj As Impresion

    Public LI As New Calcot.SetLadoIzquierdo
    Public LD As New Calcot.SetLadoDerecho

    Public LeftType As Pastilla.Types = Pastilla.Types.FlatTop
    Public RightType As Pastilla.Types = Pastilla.Types.FlatTop
    Dim LeftSide As Pastilla.Sides = Pastilla.Sides.Left
    Dim RightSide As Pastilla.Sides = Pastilla.Sides.Right
    Dim SelectedPad As Pads = Pads.Both

    '******************************************************
    '*** Variables global para seleccionar la tabla de la base de datos de los trazos
    Dim TracerTable As String = ""
    Dim VwConsultaTrazos As String = ""
    '******************************************************
    '************VARIABLES PARA OMA BISELADO***************
    Public OMA_EyeSide As String
    Public OMA_FCSGIN As String 'OMA DISTANCIA HORIZONTAL DEL CENTRO GEOMETRICO AL CENTRO DE LA HOBLEA 
    Public OMA_FCSGUP As String 'OMA DISTANCIA VERTICAL DEL CENTRO GEOMETRICO AL CENTRO DE LA HOBLEA 
    Public OMA_FCOCUP As String 'OMA DISTANCIA VERTICAL DEL CENTRO GEOMETRICO AL CENTRO OPTICO
    Public OMA_FCOCIN As String
    '******************************************************
    Public OMA_FCSGINR, OMA_FCSGINL As String
    Public OMA_FCSGUPR, OMA_FCSGUPL As String
    Public OMA_FCOCUPR, OMA_FCOCUPL As String
    Public OMA_FCOCINR, OMA_FCOCINL As String
    '******************************************************
    Public Status As Boolean = False

    Public Offset As Calcot.Desplazamiento
    Dim FlechaUp As Boolean = False

    '-----------------------------------------------------------------------------------------
    ' Objeto para Optisur
    '-----------------------------------------------------------------------------------------
    Public SpecialDesignID As Integer
    Public Optisur As NewCalcot
    '-----------------------------------------------------------------------------------------


    Enum Eyes
        None = 0
        Left = 1
        Right = 2
        Both = 3
    End Enum
    Enum Pads
        Both
        Right
    End Enum

    Function GetGraphicsObject(ByVal PBox As PictureBox) As Graphics
        Dim bmp As New Bitmap(PBox.Width, PBox.Height)
        PBox.Image = bmp
        Dim G As Graphics
        G = Graphics.FromImage(bmp)
        Return G
    End Function
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

    Function Function1Eval(ByVal X As Double) As Single
        Return Convert.ToSingle((10 + 35) * Sin(2 * X) * Sin(0.8 / X))
    End Function
    Function Read_Registry(ByVal var As String) As String
        Dim key As RegistryKey
        Dim Result As String = ""
        key = Registry.LocalMachine.OpenSubKey("SOFTWARE").OpenSubKey("AUGEN")
        Select Case var
            Case "Lab_ID"
                'Result = CStr(key.GetValue("Plant"))
                Result = My.Settings.Plant
            Case "TracerDataServer"
                'Result = CStr(key.GetValue("TracerDataServer"))
                Result = My.Settings.LocalServer
            Case "ServerAdd"
                'Result = CStr(key.GetValue("MainServer"))
                Result = My.Settings.VantageServer
        End Select
        Return Result
    End Function

    Sub GetRadiosFromDatabase(ByVal RefWECO As String, ByRef Scale As Single, ByRef TransX As Single, ByRef TransY As Single)
        Dim t As New SqlDB(My.Settings.LocalServer, My.Settings.DBUser, My.Settings.DBPassword, My.Settings.LocalDBName)
        'Dim radios(511) As Integer ' = {2613, 2613, 2612, 2612, 2610, 2609, 2608, 2613, 2612, 2612, 2610, 2609, 2608, 2613, 2612, 2612, 2610, 2609, 2608, 2613, 2612, 2612, 2610, 2609, 2608, 2613, 2612, 2612, 2610, 2609, 2608, 2613, 2612, 2612, 2610, 2609, 2608, 2613, 2612, 2612, 2610, 2609, 2608, 2613, 2612, 2612, 2610, 2609, 2608, 2613, 2612, 2612, 2610, 2609, 2608, 2613, 2612, 2612, 2610, 2609, 2608, 2613, 2612, 2612, 2610, 2609, 2608}
        Dim i As Integer

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


        Try
            t.OpenConn()
            Dim ds As New DataSet
            ds = t.SQLDS("select * from " & VwConsultaTrazos & " where jobnum = '" & RefWECO & "'", "t1")
            If My.Settings.OpticalProtocol = "DV1" Then
                For i = 0 To X4 - 1
                    RadiosR(i) = ds.Tables("t1").Rows(0).Item(i + 13)
                Next
                Dim j As Integer = 0
                For i = X2 - 1 To 0 Step -1
                    RadiosL(j) = RadiosR(i)
                    j += 1
                Next
                For i = X4 - 1 To X2 - 1 Step -1
                    RadiosL(j) = RadiosR(i)
                    j += 1
                Next
            End If
            If My.Settings.OpticalProtocol = "OMA" Then
                Dim TrcRight, TrcLeft As String
                Dim RadioIni, POS As Integer
                TrcRight = ds.Tables("t1").Rows(0).Item("TrcRight")
                TrcLeft = ds.Tables("t1").Rows(0).Item("TrcLeft")
                POS = 0
                For i = 0 To 2079 Step 52
                    RadioIni = i
                    RadiosR(POS) = TrcRight.Substring(RadioIni + 2, 4)
                    RadiosL(POS) = TrcLeft.Substring(RadioIni + 2, 4)
                    RadioIni += 5
                    POS += 1
                    RadiosR(POS) = TrcRight.Substring(RadioIni + 2, 4)
                    RadiosL(POS) = TrcLeft.Substring(RadioIni + 2, 4)
                    RadioIni += 5
                    POS += 1
                    RadiosR(POS) = TrcRight.Substring(RadioIni + 2, 4)
                    RadiosL(POS) = TrcLeft.Substring(RadioIni + 2, 4)
                    RadioIni += 5
                    POS += 1
                    RadiosR(POS) = TrcRight.Substring(RadioIni + 2, 4)
                    RadiosL(POS) = TrcLeft.Substring(RadioIni + 2, 4)
                    RadioIni += 5
                    POS += 1
                    RadiosR(POS) = TrcRight.Substring(RadioIni + 2, 4)
                    RadiosL(POS) = TrcLeft.Substring(RadioIni + 2, 4)
                    RadioIni += 5
                    POS += 1
                    RadiosR(POS) = TrcRight.Substring(RadioIni + 2, 4)
                    RadiosL(POS) = TrcLeft.Substring(RadioIni + 2, 4)
                    RadioIni += 5
                    POS += 1
                    RadiosR(POS) = TrcRight.Substring(RadioIni + 2, 4)
                    RadiosL(POS) = TrcLeft.Substring(RadioIni + 2, 4)
                    RadioIni += 5
                    POS += 1
                    RadiosR(POS) = TrcRight.Substring(RadioIni + 2, 4)
                    RadiosL(POS) = TrcLeft.Substring(RadioIni + 2, 4)
                    RadioIni += 5
                    POS += 1
                    RadiosR(POS) = TrcRight.Substring(RadioIni + 2, 4)
                    RadiosL(POS) = TrcLeft.Substring(RadioIni + 2, 4)
                    RadioIni += 5
                    POS += 1
                    RadiosR(POS) = TrcRight.Substring(RadioIni + 2, 4)
                    RadiosL(POS) = TrcLeft.Substring(RadioIni + 2, 4)
                    POS += 1
                Next
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        Finally
            t.CloseConn()
        End Try

    End Sub
    Private Sub Button3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        PictureBox1.Focus()
        DrawLenses()
    End Sub

    Private Sub DrawLenses()
        MensajeR.Text = ""
        MensajeL.Text = ""

        Dim G As Graphics = GetGraphicsObject(PictureBox1)
        G.SmoothingMode = SmoothingMode.AntiAlias
        G.Clear(PictureBox1.BackColor)
        PlotLenteD = LenteD.DrawLense(PictureBox1, InitialPoint)
        PlotLenteI = LenteI.DrawLense(PictureBox1, New Point(InitialPoint.X + LenteD.Size.Width + (Puente * 100), InitialPoint.Y))
        Dim ppupilad As Point
        Dim ppupilai As Point
        If Me.LeftType = Pastilla.Types.FlatTop Then
            '            ppupilai = New Point(LenteI.Location.X + ((-InsetL + LeftPupilaX + ExcHL) * 100), _
            ppupilai = New Point(LenteI.Location.X + PupilaI.Inset, _
                LenteI.Location.Y + ((DropL - LeftPupilaY + ExcVL) * 100) + OffsetL.Y)
        ElseIf LeftType = Pastilla.Types.Progressive Or LeftType = Pastilla.Types.Monofocal Then
            ppupilai = New Point(LenteI.Location.X + ((-LeftPupilaX) * 100), _
                LenteI.Location.Y + ((-LeftPupilaY) * 100) + OffsetL.Y)
        End If
        If RightType = Pastilla.Types.FlatTop Then
            '          ppupilad = New Point(LenteD.Location.X + ((-InsetR - RightPupilaX + ExcHR) * 100), _
            ppupilad = New Point(LenteD.Location.X + PupilaD.Inset, _
                LenteD.Location.Y + ((DropR - RightPupilaY + ExcVR) * 100) + OffsetR.Y)
        ElseIf RightType = Pastilla.Types.Progressive Or RightType = Pastilla.Types.Monofocal Then
            ppupilad = New Point(LenteD.Location.X + ((-RightPupilaX) * 100), _
                LenteD.Location.Y + ((-RightPupilaY) * 100) + OffsetR.Y)
        End If
        Try
            '*ACTUALIZAMOS LAS VARIABLES DE DISTANCIA VERTICAL DEL CENTRO OPTICO AL CENTRO GEOMETRICO
            Dim RDist, LDist, RDist2, LDist2 As String

            If CheckedEyes And Eyes.Right Then
                RDist = ((((RightPupilaY) * 100) - OffsetR.Y) / 100) - DropR
                RDist2 = Math.Round(ExcHR - (PupilaD.Inset / 100) + RightPupilaX, 2)
            Else
                RDist = OMA_FCOCUPR
                RDist2 = OMA_FCOCINR
            End If
            If CheckedEyes And Eyes.Left Then
                LDist = ((((LeftPupilaY) * 100) - OffsetL.Y) / 100) - DropL
                LDist2 = Math.Round(ExcHL - ((PupilaI.Inset / 100) - LeftPupilaX), 2)
            Else
                LDist = OMA_FCOCUPL
                LDist2 = OMA_FCOCINL
            End If
            'If CheckedEyes And Eyes.Left Then LDist = ((((LeftPupilaY) * 100) - OffsetL.Y) / 100) - DropL
            'If CheckedEyes And Eyes.Right Then RDist = ((((RightPupilaY) * 100) - OffsetR.Y) / 100) - DropR
            OMA_FCOCUP = "FCOCUP=" & RDist & ";" & LDist

            'RDist = ""
            'LDist = ""
            'If CheckedEyes And Eyes.Right Then RDist = Math.Round(ExcHR - (PupilaD.Inset / 100) + RightPupilaX, 2)
            'If CheckedEyes And Eyes.Left Then LDist = Math.Round(ExcHL - ((PupilaI.Inset / 100) - LeftPupilaX), 2)
            OMA_FCOCIN = "FCOCIN=" & RDist2 & ";" & LDist2
            '****************************************************************************************
        Catch ex As Exception

        End Try



        If CheckedEyes And Eyes.Left Then
            PlotPastillaI = PastillaI.DrawPastilla(PictureBox1, ppupilai, Altura * 100, AlturaLeft * 100)
            PlotPupilaI = PupilaI.DrawPupila(PictureBox1, PastillaI.Pupila)
            G.DrawPath(Pens.DarkGray, PlotPastillaI)
            G.DrawPath(Pens.Brown, PlotPupilaI)
        End If
        If CheckedEyes And Eyes.Right Then
            PlotPastillaD = PastillaD.DrawPastilla(PictureBox1, ppupilad, Altura * 100, AlturaRight * 100)
            PlotPupilaD = PupilaD.DrawPupila(PictureBox1, PastillaD.Pupila)
            G.DrawPath(Pens.DarkGray, PlotPastillaD)
            G.DrawPath(Pens.Brown, PlotPupilaD)
        End If

        G.DrawPath(Pens.BlueViolet, PlotLenteD)
        G.DrawPath(Pens.BlueViolet, PlotLenteI)
        PictureBox1.Invalidate()
        Me.BotonValidar_Click(Me, New System.EventArgs)
    End Sub
    Private Sub Up_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        'OffsetY += 100
        FlechaUp = True
        If SelectedPad = Pads.Both Then
            'cambio por Marco A. fecha: 24/jun/2006 
            'si se puede cambiar la altura en los monofocacles segun Rosy del Lab de ensenada.
            If CheckedEyes = Eyes.Both Or CheckedEyes = Eyes.Left Then
                OffsetL.Y += 100
                TextAlturaL.Text += 1
            End If
            'End If
            'If RightType <> Pastilla.Types.Monofocal Then
            If CheckedEyes = Eyes.Both Or CheckedEyes = Eyes.Right Then
                OffsetR.Y += 100
                TextAlturaR.Text += 1
            End If
            'End If
        Else
            If LeftType <> Pastilla.Types.Monofocal Then TextAlturaL.Text = Math.Round(CDbl(AlturaiL.Text), 0)
            OffsetL.Y += 100
            TextAlturaL.Text += 1
            'End If
        End If
        DrawLenses()
    End Sub

    Private Sub Down_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        'OffsetY -= 100
        If SelectedPad = Pads.Both Then
            'cambio por Marco A. fecha: 24/jun/2006 
            'si se puede cambiar la altura en los monofocacles segun Rosy del Lab de ensenada.
            'If LeftType <> Pastilla.Types.Monofocal Then
            If CheckedEyes = Eyes.Both Or CheckedEyes = Eyes.Left Then
                OffsetL.Y -= 100
                TextAlturaL.Text -= 1
            End If
            'End If
            'If RightType <> Pastilla.Types.Monofocal Then
            If CheckedEyes = Eyes.Both Or CheckedEyes = Eyes.Right Then
                OffsetR.Y -= 100
                TextAlturaR.Text -= 1
            End If
            'End If
        Else
            'If LeftType <> Pastilla.Types.Monofocal Then
            OffsetL.Y -= 100
            TextAlturaL.Text -= 1
            'End If
        End If
        DrawLenses()
    End Sub

    Private Sub PreviewArmazon_Disposed(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Disposed
        Me.Status = False
    End Sub

    Private Sub PreviewArmazon_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load


        '**************************************************
        '**** leemos el archivo de configuracion para determinar con la tabla de trazos que se va a trabajar
        Select Case My.Settings.OpticalProtocol
            Case "DVI"
                TracerTable = "TracerData"
                VwConsultaTrazos = "TracerData"
            Case "OMA"
                TracerTable = "OMATraces"
                VwConsultaTrazos = "VwOMATraces"
        End Select
        '**************************************************

        Dim x As String
        x = Form1.GetCustomValues(Form1.CustomValues.Lado, LeftPill.Text)
        If x = "N" Or x = "L" Then
            LeftSide = Pastilla.Sides.Left
        ElseIf x = "R" Then
            LeftSide = Pastilla.Sides.Right
        End If
        x = Form1.GetCustomValues(Form1.CustomValues.Lado, RightPill.Text)
        If x = "N" Or x = "R" Then
            RightSide = Pastilla.Sides.Right
        ElseIf x = "L" Then
            RightSide = Pastilla.Sides.Left
        End If
        LenteI = New Lente(LeftSide, RadiosL, Scale)
        LenteD = New Lente(RightSide, RadiosR, Scale)

        PupilaD = New Pupila(RightType, RightSide, Scale, (-InsetR - RightPupilaX + ExcHR), (-RightPupilaY + DropR + ExcVR))
        'PupilaD = New Pupila(RightType, RightSide, Scale, ExcHR, ExcVR)
        '        PupilaD = New Pupila(RightType, RightSide, Scale, ExcH, ExcV)
        PupilaI = New Pupila(LeftType, LeftSide, Scale, -(-InsetL + LeftPupilaX) + ExcHL, (-LeftPupilaY + DropL + ExcVL))
        '       PupilaI = New Pupila(LeftType, LeftSide, Scale, ExcH, ExcV)
        PastillaD = New Pastilla(RightType, RightSide, DiametroPastillaR, AnchoObleaR, AltoObleaR, Scale, ExcHR, ExcVR, InsetR, DropR)
        PastillaI = New Pastilla(LeftType, LeftSide, DiametroPastillaL, AnchoObleaL, AltoObleaL, Scale, ExcHL, ExcVL, InsetL, DropL)
        Try
            DrawLenses()
        Catch ex As Exception
            Throw New Exception("Error al dibujar el lente." & vbCrLf & ex.Message)
        End Try
        TextDiametroR.Text = DiametroPastillaR
        TextDiametroL.Text = DiametroPastillaL
        'TextAlturaR.Text = AlturaRight
        'TextAlturaL.Text = AlturaLeft
        AlturaiL.Text = AlturaLeft.ToString()
        AlturaiR.Text = AlturaRight.ToString()
        'TextAro.Text = LenteD.Size.Width / 100
        'TextVertical.Text = LenteD.Size.Height / 100
        'TextPuente.Text = Puente
        'TextDiagonal.Text = LenteD.Diagonal
        Me.Select()
        Me.BotonValidar_Click(Me, e)

        'cambio por Marco A 24/jun/2006
        'la primera vez la altura vale 0 en los monofocales, despues para cambiar partimos de la altura regresada  de los calculos

        ' Aquí está el error. No se debe redondear, se debe pasar exacto con 2 decimales,
        ' Pedro Farías Lozano  Nov 22 2012
        ' Antes tenía Math.Round(CDbl(AlturaiR.Text), 0)   -- Redondeo a entero, lo cual acumula un error de 1 en muchos casos
        ' debe ser Math.Round(CDbl(AlturaiR.Text), 2) -- Casi no acumula error   ( 0.01 )
        '        If LeftType = Pastilla.Types.Monofocal Then TextAlturaR.Text = Math.Round(CDbl(AlturaiR.Text), 2)
        '        If LeftType = Pastilla.Types.Monofocal Then TextAlturaL.Text = Math.Round(CDbl(AlturaiL.Text), 2)
        ' Pedro Farías Lozano  Nov 23 2012
        ' No hace falta convertir, solo tomar en cuenta cuando es inicialmente 0 y en los casos siguientes
        ' Como ambos lados de la asignación son valor string no hace falta convertir a numérico y redondear, sólo se truncan las cadenas
        ' string para evitar traer demasiados decimales.

        If LeftType = Pastilla.Types.Monofocal Then
            If AlturaRight = 0.0 Then
                TextAlturaR.Text = String.Format("{0:F2}", AlturaiR.Text)    ' Tranfiere solo los primeros 5 caracteres  Ej. 11.23 para cortar decimales
            Else
                TextAlturaR.Text = AlturaRight.ToString()
            End If
            '        End If

            '        If LeftType = Pastilla.Types.Monofocal Then
            If AlturaLeft = 0.0 Then
                TextAlturaL.Text = String.Format("{0:F2}", AlturaiR.Text)    ' Tranfiere solo los primeros 5 caracteres  Ej. 11.23 para cortar decimales
            Else
                TextAlturaL.Text = AlturaLeft.ToString()
            End If
        End If
    End Sub

    Private Sub Right_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RightB.Click
        SelectedPad = Pads.Right
    End Sub

    Private Sub LeftB_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles LeftB.Click
        SelectedPad = Pads.Both

    End Sub

    Private Sub PictureBox1_PreviewKeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.PreviewKeyDownEventArgs) Handles PictureBox1.PreviewKeyDown
        'Select Case e.KeyValue
        '    Case 37 : LeftB_Click(Me, e)
        '    Case 38 : Up_Click(Me, e)
        '    Case 39 : Right_Click(Me, e)
        '    Case 40 : Down_Click(Me, e)
        'End Select
    End Sub

    Private Sub PreviewArmazon_KeyUp(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyUp
        Select Case e.KeyValue
            Case 37 : LeftB_Click(Me, e) : FlechaUp = True
            Case 38 : Up_Click(Me, e) : FlechaUp = True
            Case 39 : Right_Click(Me, e) : FlechaUp = True
            Case 40 : Down_Click(Me, e) : FlechaUp = True
        End Select
        PictureBox1.Focus()
        'Me.Parent.Controls("MonoRight").Text = "YES"

    End Sub

    Private Sub BotonValidar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BotonValidar.Click
        Dim valor As Boolean = False
        ' llamar el servicio de atanacio y ver si los valores obtenidos estan correctos o no
        'LI.Alturai = CType(TextAlturaL.Text, Single)
        'LD.Alturai = CType(TextAlturaR.Text, Single)
        If CheckedEyes And Eyes.Left Then
            Optisur.SetLeftHeight(CType(TextAlturaL.Text, Single))
        End If
        If CheckedEyes And Eyes.Right Then
            Optisur.SetRightHeight(CType(TextAlturaR.Text, Single))
        End If

        'Dim fd As New Calcot.ReturnLadoDer
        'Dim fi As New Calcot.ReturnLadoIzq

        ''Dim Offset As New Calcot.Desplazamiento(LD, LI)

        'Offset = New Calcot.Desplazamiento(LD, LI)


        'Offset.Ajuste = Sizing                              ' es el sizing en la tabla TracerData
        'Offset.Puente = CDbl(Me.TextPuente.Text)
        'si los trazos son de OMA tendremos que convertirlos a DVI
        'Select Case My.Settings.OpticalProtocol
        '    Case "DVI"
        '        'Offset.SetRadios = RadiosL
        '    Case "OMA"
        '        'Offset.SetRadios = Convertir400To512(RadiosL)
        'End Select

        Try
            ' ''Offset.GetDesplazo()

            If FlechaUp Then
                'Offset.GetDesplazo()
                Optisur.GetContorno()
                FlechaUp = False
            End If

            'If Offset.GetCveMensaje = 0 Then
            If Optisur.ErrorCode = 0 Then
                'fd = Offset.GetFormaDer
                'fi = Offset.GetFormaIzq
                'MensajeL.Text = fi.Mensaje
                'MensajeR.Text = fd.Mensaje
                'AlturaiL.Text = fi.Altura.ToString("f")
                'AlturaiR.Text = fd.Altura.ToString("f")

                MensajeL.Text = Optisur.DisplLeft.notice
                MensajeR.Text = Optisur.DisplRight.notice
                AlturaiL.Text = Optisur.DisplLeft.heigth.ToString("f")
                AlturaiR.Text = Optisur.DisplRight.heigth.ToString("f")


                'If (fd.Mensaje.Contains("lente sale") And fi.Mensaje.Contains("lente sale")) Or (fd.Mensaje.Contains("prisma") And fi.Mensaje.Contains("prisma")) Then

                If CheckedEyes = Eyes.Both Then
                    'If (Not fd.Mensaje.Contains("lente no sale")) And (Not fi.Mensaje.Contains("lente no sale")) Then
                    If (Not Optisur.DisplRight.notice.Contains("lente no sale")) And (Not Optisur.DisplLeft.notice.Contains("lente no sale")) Then
                        'MsgBox("lentes salen")
                        valor = True
                    Else
                        'MsgBox(Offset.GetCveMensaje.ToString & " " & fd.Mensaje & " " & fi.Mensaje, MsgBoxStyle.Information)
                        valor = False
                    End If
                ElseIf CheckedEyes = Eyes.Right Then
                    'If (Not fd.Mensaje.Contains("lente no sale")) Then
                    If (Not Optisur.DisplRight.notice.Contains("lente no sale")) Then
                        valor = True
                    Else
                        valor = False
                    End If
                ElseIf CheckedEyes = Eyes.Left Then
                    ' If (Not fi.Mensaje.Contains("lente no sale")) Then
                    If (Not Optisur.DisplLeft.notice.Contains("lente no sale")) Then
                        valor = True
                    Else
                        valor = False
                    End If
                End If


            Else
                MsgBox(Offset.GetMensaje, MsgBoxStyle.Critical)
            End If

        Catch ex As Exception
            MsgBox("Error al obtener datos de desplazamiento: " & ex.Message, MsgBoxStyle.Critical)
            valor = False
        End Try
        If valor Then
            BotonAceptar.Enabled = True
            BotonContinuar.Enabled = True
        Else
            BotonAceptar.Enabled = False
            BotonContinuar.Enabled = False
        End If
    End Sub
    Private Function Convertir400To512(ByVal RadiosToConvert As Integer()) As Integer()
        Dim NewRadios(511) As Integer
        Dim i, LimInf, LimSup As Integer

        Dim SqlCon As New SqlClient.SqlConnection(Laboratorios.ConnStr)
        Dim SqlRdr As SqlClient.SqlDataReader
        Try
            SqlCon.Open()
            Dim SqlStr As String = "SELECT ConvID, Point, LimInf, LimSup FROM dbo.OMAConvTable where ConvID = '400to512' order  by Point asc " 'and Point = " & i + 1 & ""
            Dim SqlCmd As New SqlClient.SqlCommand(SqlStr, SqlCon)
            SqlRdr = SqlCmd.ExecuteReader
            For i = 0 To 511
                SqlRdr.Read()
                LimInf = SqlRdr("LimInf")
                LimSup = SqlRdr("LimSup")
                NewRadios(i) = (RadiosToConvert(LimInf - 1) + RadiosToConvert(LimSup - 1)) / 2
            Next
            SqlRdr.Close()
            Return NewRadios
        Catch ex As Exception
            MsgBox(ex.Message)
            Return NewRadios
        Finally
            SqlCon.Close()
        End Try

    End Function
    Private Sub BotonAceptar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BotonAceptar.Click, BotonContinuar.Click
        'Status = True
        'Me.Close()
        'ejecutamos los calculos opticos y desplegamos la pantalla final
        CalculosOpticos()
    End Sub

    Private Sub Preview_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Preview.Click, BotonCancelar.Click
        Status = False
        Me.Close()
    End Sub
    Public Sub CalculosOpticos()
        Dim CalcOpticos As GenerarRx
        'Dim calculoCA As Calcot.calculoCA

        Try
            '-------------------------------------------------------------------------------
            ' Como ya tenemos el contorno desde Form1, ya no es necesario generar el objeto
            '-------------------------------------------------------------------------------
            ''Aqui evaluamos si se genera un objeto para Diseños Especiales (Optisur)
            'If SpecialDesignID > 0 And My.Settings.EnableOptisur Then
            '    Dim Side As New NewCalcot.Eyes
            '    Select Case CheckedEyes
            '        Case Eyes.Both : Side = NewCalcot.Eyes.Both
            '        Case Eyes.Left : Side = NewCalcot.Eyes.Left
            '        Case Eyes.Right : Side = NewCalcot.Eyes.Right
            '    End Select

            '    ' Se genera objeto Optisur
            '    Optisur = New NewCalcot(IsManualFrame, Side)
            '    ' Se le pasa el numero de trazo (Rx para guiador)
            '    Optisur.JobNumber = RefWECO
            '    ' Se agrega la informacion de las pastillas a utilizar en la Rx
            '    Select Case Side
            '        Case NewCalcot.Eyes.Both
            '            Optisur.SetRightItem(LD.Codigob, LD.Esfera, LD.Cilindro, LD.Eje, LD.Adicion, LD.Prisma, LD.EjePrisma, LD.dpMono, LD.Alturai)
            '            Optisur.SetLeftItem(LI.Codigob, LI.Esfera, LI.Cilindro, LI.Eje, LI.Adicion, LI.Prisma, LI.EjePrisma, LI.dpMono, LI.Alturai)
            '        Case NewCalcot.Eyes.Left
            '            Optisur.SetLeftItem(LI.Codigob, LI.Esfera, LI.Cilindro, LI.Eje, LI.Adicion, LI.Prisma, LI.EjePrisma, LI.dpMono, LI.Alturai)
            '        Case NewCalcot.Eyes.Right
            '            Optisur.SetRightItem(LD.Codigob, LD.Esfera, LD.Cilindro, LD.Eje, LD.Adicion, LD.Prisma, LD.EjePrisma, LD.dpMono, LD.Alturai)
            '    End Select
            '    ' Se agrega la informacion del armazon, ya sea manual o digital
            '    If IsManualFrame Then
            '        Optisur.SetGeoFrame(Form1.GetOMAFrameType(Mat_Arm), TextAro.Text, TextVertical.Text, TextDiagonal.Text, Puente, DIPLejos, DIPCerca)
            '    Else
            '        Select Case My.Settings.OpticalProtocol
            '            Case "OMA"
            '                Optisur.SetPattern(Form1.GetOMAFrameType(Mat_Arm), Puente, DIPLejos, DIPCerca, Sizing, 0, Convertir400To512(RadiosL))
            '            Case "DVI"
            '                Optisur.SetPattern(Form1.GetOMAFrameType(Mat_Arm), Puente, DIPLejos, DIPCerca, Sizing, 0, RadiosL)
            '        End Select
            '    End If
            '    Optisur.GetContorno()
            '    'Optisur.GetCalculos()
            '    'Optisur.Calculos = New LensDsgd
            '    'Optisur.Calculos.DisplPattern = Optisur.Contorno.DisplPattern
            '    'Optisur.Calculos.GeoFrame = Optisur.Contorno.GeoFrame
            '    'Optisur.Calculos.LensDsg = Optisur.Contorno.Pattern

            'End If

            'ejecutamos los calculos y si son validos se despliega la forma para ejecutar la receta en guiador
            'If Not My.Settings.EnableOptisur Then
            '    calculoCA = New Calcot.calculoCA(LD, LI)
            '    calculoCA.Confirmar = False 'solicitamos solo calculos opticos, sin grabar en guiador
            '    calculoCA.Puente = Puente
            '    calculoCA.Receta = RefWECO
            '    calculoCA.Mat_Arm = Me.Mat_Arm
            '    calculoCA.Ajuste = Sizing
            '    Select Case My.Settings.OpticalProtocol
            '        Case "OMA"
            '            calculoCA.SetRadios = Convertir400To512(RadiosL)
            '        Case "DVI"
            '            calculoCA.SetRadios = (RadiosL)
            '    End Select

            'End If

            ' ''calculoCA.GetCalculoCA()
            'calculoCA.GetCalculoCA(My.Settings.Optimizar)
            If Optisur.ErrorCode = 0 Then
                'calculos validos
                CalcOpticos = New GenerarRx(Puente, RefWECO, Mat_Arm, IsManualFrame, RxInfo, IsChangingTrace, OMA)

                ' Para saber si se debe consumir creditos o no cuando se este procesando la receta.
                If UseFFCredit Then
                    Optisur.GetCalculos(NewCalcot.CalculationOption.CalculateAndSave)
                Else
                    Optisur.GetCalculos(NewCalcot.CalculationOption.JustCalculate)
                End If


                CalcOpticos.Framenum = Framenum ' Aqui le mando el numero de trazo

                ' Parece que el visualizador muestra los trazos invertidos ¿?
                CalcOpticos.radiosR = RadiosR
                CalcOpticos.radiosL = RadiosL

                CalcOpticos.BaseL = BaseL
                CalcOpticos.BaseR = BaseR
                'asignamos la fecha de salidad  de la orden
                CalcOpticos.FechaEntrada = Me.FechaEntrada
                CalcOpticos.FechaInicial = Me.FechaInicial
                CalcOpticos.IsReceivingVirtualRx = IsReceivingVirtualRx

                ' Aqui le paso la informacion de Optisur si es que se requiere
                'If My.Settings.EnableOptisur And SpecialDesignID > 0 Then
                'If My.Settings.EnableOptisur Then

                Dim LeftMoldGenSurf As String = Nothing
                Dim RightMoldGenSurf As String = Nothing
                Dim LeftMoldLapSurf As String = Nothing
                Dim RightMoldLapSurf As String = Nothing

                Dim LeftEye As LensDsg = Optisur.LensDsgLeft
                Dim RightEye As LensDsg = Optisur.LensDsgRight

                If CheckedEyes = Eyes.Right Or CheckedEyes = Eyes.Both Then
                    RightMoldGenSurf = String.Format("{0:000}", (RightEye.genSurf.base.power * 100)) & "-" & String.Format("{0:000}", (RightEye.genSurf.cross.power * 100))
                    RightMoldLapSurf = String.Format("{0:000}", (RightEye.lapSurf(0).base.power * 100)) & "-" & String.Format("{0:000}", (RightEye.lapSurf(0).cross.power * 100))
                    CalcOpticos.MoldeD.Anillo_HD = RightEye.ringSurf.blockName.Replace("ANILLO ", "").Replace("MOLDE ", "")
                    CalcOpticos.LBL_MoldeD_GenSurf.Text = RightMoldGenSurf
                Else
                    CalcOpticos.LBL_MoldeD_GenSurf.Text = ""
                End If
                If CheckedEyes = Eyes.Left Or CheckedEyes = Eyes.Both Then
                    LeftMoldGenSurf = String.Format("{0:000}", (LeftEye.genSurf.base.power * 100)) & "-" & String.Format("{0:000}", (LeftEye.genSurf.cross.power * 100))
                    LeftMoldLapSurf = String.Format("{0:000}", (LeftEye.lapSurf(0).base.power * 100)) & "-" & String.Format("{0:000}", (LeftEye.lapSurf(0).cross.power * 100))
                    CalcOpticos.MoldeI.Anillo_HD = LeftEye.ringSurf.blockName.Replace("ANILLO ", "").Replace("MOLDE ", "")
                    CalcOpticos.LBL_MoldeI_GenSurf.Text = LeftMoldGenSurf
                Else
                    CalcOpticos.LBL_MoldeI_GenSurf.Text = ""
                End If

                'End If


                'OMA.SetCTHICK(CalcOpticos.MoldeD.GrosorCentral, CalcOpticos.MoldeI.GrosorCentral)
                'OMA.SetTHKP(CalcOpticos.MoldeD.GrosorMaximo, CalcOpticos.MoldeI.GrosorMaximo)

                OMA.SetCTHICK(Optisur.LensDsgRight.cenThk, Optisur.LensDsgLeft.cenThk)
                OMA.SetTHKP(Optisur.LensDsgRight.maxEthk.thk, Optisur.LensDsgLeft.maxEthk.thk)

                CalcOpticos.Optisur = Optisur
                CalcOpticos.CheckedEyes = CheckedEyes

                CalcOpticos.ShowDialog()

                'GrosorcentralD = CalcOpticos.GrosCentralD
                'GrosorcentralI = CalcOpticos.GrosCentralI

                GrosorcentralD = Optisur.LensDsgRight.cenThk
                GrosorcentralI = Optisur.LensDsgLeft.cenThk

                '-------ALMACENAMOS LOS VALORES FINALES PARA OMA-------
                Dim SegHR, SegVR, SegHL, SegVL As String
                SegHR = 0
                SegHL = 0
                SegVR = 0
                SegVL = 0

                If CheckedEyes = Eyes.Both Then
                    'SegHR = calculoCA.GetFormaDer.SegmentoH
                    'SegHL = calculoCA.GetFormaIzq.SegmentoH
                    'SegVR = calculoCA.GetFormaDer.SegmentoV
                    'SegVL = calculoCA.GetFormaIzq.SegmentoV

                    SegHR = Optisur.DisplRight.LRP.x
                    SegHL = Optisur.DisplLeft.LRP.x
                    SegVR = Optisur.DisplRight.LRP.y
                    SegVL = Optisur.DisplLeft.LRP.y


                Else
                    If CheckedEyes = Eyes.Right Then
                        'SegHR = calculoCA.GetFormaDer.SegmentoH
                        'SegVR = calculoCA.GetFormaDer.SegmentoV

                        SegHR = Optisur.DisplRight.LRP.x
                        SegVR = Optisur.DisplRight.LRP.y
                    Else
                        SegHR = OMA_FCSGINR
                        SegVR = OMA_FCSGUPR
                    End If
                    If CheckedEyes = Eyes.Left Then
                        'SegHL = calculoCA.GetFormaIzq.SegmentoH
                        'SegVL = calculoCA.GetFormaIzq.SegmentoV

                        SegHL = Optisur.DisplLeft.LRP.x
                        SegVL = Optisur.DisplLeft.LRP.y
                    Else
                        SegHL = OMA_FCSGINL
                        SegVL = OMA_FCSGUPL
                    End If
                End If

                ' ----------------------------------------------------------------------------------------------
                ' Se cambio a estos valores de FCSGIN pues es lo que espera Mayte ver en la bloqueadora
                ' ----------------------------------------------------------------------------------------------
                'If CheckedEyes = Eyes.Both Then
                '    SegHR = calculoCA.GetFormaDer.DH
                '    SegHL = calculoCA.GetFormaIzq.DH
                '    SegVR = calculoCA.GetFormaDer.DV
                '    SegVL = calculoCA.GetFormaIzq.DV
                'Else
                '    If CheckedEyes = Eyes.Right Then
                '        SegHR = calculoCA.GetFormaDer.DH
                '        SegVR = calculoCA.GetFormaDer.DV
                '    Else
                '        SegHR = OMA_FCSGINR
                '        SegVR = OMA_FCSGUPR
                '    End If
                '    If CheckedEyes = Eyes.Left Then
                '        SegHL = calculoCA.GetFormaIzq.DH
                '        SegVL = calculoCA.GetFormaIzq.DV
                '    Else
                '        SegHL = OMA_FCSGINL
                '        SegVL = OMA_FCSGUPL
                '    End If
                'End If


                OMA_FCSGIN = "FCSGIN=" & SegHR & ";" & SegHL
                OMA_FCSGUP = "FCSGUP=" & SegVR & ";" & SegVL
                '------------------------------------------------------
                '------------------------------------------------------
                'IGUALAMOS EL OBJETO DE IMPRESION PARA PASARLO AL FORM1
                Me.PrintObj = New Impresion
                Me.PrintObj = CalcOpticos.PrintObj
                '------------------------------------------------------
                If CalcOpticos.SuccessfulTran Then
                    Me.Status = True
                    Me.Close()
                End If
            Else
                'los calculos no son validos y lanzamos una exepcion
                Throw New Exception(Optisur.ErrorCode & " - " & Optisur.ErrorDescription)
            End If
        Catch ex As Exception
            CalcOpticos = Nothing
            Throw New Exception("Error al mostrar cálculos para Guiador. " & vbCrLf & ex.Message, ex)
        End Try
    End Sub

    ''' <summary>
    ''' Crea un objeto de tipo Preview.
    ''' </summary>
    ''' <param name="IsChangingTrace">True/False si se está modificando el trazo o no.</param>
    ''' <param name="MyOMA">Objeto tipo OMAFile para guardar información para la biseladora MEI.</param>
    ''' <param name="SpceialDesign">Tipo de diseño para generar o no información para Optisur.</param>
    ''' <param name="DIPLejos">Distancia Interpupilar para vista normal.</param>
    ''' <param name="DIPCerca">Distancia Interpupilar para vista de cerca.</param>
    ''' <remarks></remarks>
    Public Sub New(ByVal IsChangingTrace As Boolean, ByRef MyOMA As OMAFile, ByVal SpceialDesign As Integer, ByVal DIPLejos As Single, ByVal DIPCerca As Single)

        ' This call is required by the Windows Form Designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        OMA = MyOMA
        Me.IsChangingTrace = IsChangingTrace
        SpecialDesignID = SpceialDesign
        Me.DIPLejos = DIPLejos
        Me.DIPCerca = DIPCerca
    End Sub
End Class