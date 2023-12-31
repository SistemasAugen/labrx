Imports Microsoft.Win32

Public Class GenerarRx
    Public IsChangingTrace As Boolean
    Public OMA As OMAFile
    Public LI As New Calcot.SetLadoIzquierdo
    Public LD As New Calcot.SetLadoDerecho
    Public MoldeD As New Calcot.ReturnMoldeDer
    Public MoldeI As New Calcot.ReturnMoldeIzq
    Public FormaD As New Calcot.ReturnLadoDer
    Public FormaI As New Calcot.ReturnLadoIzq
    Public BaseL As String
    Public BaseR As String
    Public puente As Single
    Dim receta As String
    Public radiosL(NbrsTraces()) As Integer
    Public radiosR(NbrsTraces()) As Integer
    Dim Mat_Arm As Integer
    Public IsManualFrame As Boolean
    Public SuccessfulTran As Boolean
    Public CheckedEyes As Eyes = Eyes.Both
    'variables publicas para guardar los grosores centrales de ambos ojos
    Public GrosCentralD As Single
    Public GrosCentralI As Single
    '********************************
    Public PrintObj As Impresion
    '-------------------------------
    'variable que guarda la fecha final
    Public FechaInicial, FechaEntrada As String
    Public IsReceivingVirtualRx As Boolean = False
    Public Framenum As String

    '******************************************************
    '*** Variables global para seleccionar la tabla de la base de datos de los trazos
    Dim TracerTable As String = ""
    Dim VwConsultaTrazos As String = ""
    '******************************************************
    Public Optisur As NewCalcot


    Enum Eyes
        None = 0
        Left = 1
        Right = 2
        Both = 3
    End Enum

    '------------
    'objeto con variables de la receta para impresion
    Public RxInfo As New Calcot.RxPrintInfo
    '-------------------

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click, BotonCancelar.Click
        SuccessfulTran = False
        'Me.Dispose()
        Me.Close()
    End Sub

    Private Sub GenerarRx_Disposed(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Disposed
        SuccessfulTran = False
    End Sub
    Private Function Convertir400To512(ByVal RadiosToConvert As Integer()) As Integer()
        Dim NewRadios(511) As Integer
        Dim i, LimInf, LimSup As Integer

        Dim SqlCon As New SqlClient.SqlConnection(Laboratorios.ConnStr)
        Dim SqlRdr As SqlClient.SqlDataReader
        Try
            SqlCon.Open()
            Dim SqlStr As String = "SELECT ConvID, Point, LimInf, LimSup FROM dbo.OMAConvTable where ConvID = '400to512' order  by Point asc" 'and Point = " & i + 1 & ""
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
    Private Sub GenerarRx_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        PanelLeftSide.Visible = False
        PanelRightSide.Visible = False
        If My.Settings.EnableOptisur Then
            'lado derecho
            If (CheckedEyes = Eyes.Right Or CheckedEyes = Eyes.Both) And (Not Optisur.RightSide Is Nothing) Then
                txtesfD.Text = Math.Round(Optisur.RightSide.Lens.sphere, 2)
                txtcilD.Text = Math.Round(Optisur.RightSide.Lens.cylinder, 2)
                txtejeD.Text = Optisur.RightSide.Lens.axis
                txtadicD.Text = Optisur.RightSide.Lens.addition
                txtprisD.Text = Math.Round(Optisur.LensDsgRight.Prism.diop, 2)
                txtejeprisD.Text = Optisur.LensDsgRight.Prism.axis
                txtaltD.Text = Math.Round(Optisur.RightSide.Lens.segHeigth, 1)
                lblmoldeD.Text = String.Format("{0:000}", Optisur.LensDsgRight.lapSurf(0).base.power * 100) & "-" & String.Format("{0:000}", Optisur.LensDsgRight.lapSurf(0).cross.power * 100)
                lblmoldeExD.Text = String.Format("{0:000}", Math.Round(Optisur.LensDsgRight.lapSurf(0).base.power, 2) * 100) & "-" & String.Format("{0:000}", Math.Round(Optisur.LensDsgRight.lapSurf(0).cross.power, 2) * 100)
                'MOLDE DE HIGH DEFINITION
                If Optisur.LensDsgRight.lapSurf(0).base.power = 0.0 Then
                    lblmoldeD_HD.Text = ""
                Else
                    lblmoldeD_HD.Text = String.Format("{0:000}", Optisur.LensDsgRight.lapSurf(0).base.power * 100) & "-" & String.Format("{0:000}", Optisur.LensDsgRight.lapSurf(0).cross.power * 100)
                End If

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

                '**************************
                txtorillaminD.Text = Math.Round(Optisur.LensDsgRight.minEThk.thk, 2)
                txtcentroD.Text = Math.Round(Optisur.LensDsgRight.cenThk, 2)
                txtorillamaxD.Text = Math.Round(Optisur.LensDsgRight.maxEthk.thk, 2)
                txtdhD.Text = Math.Round(Optisur.DisplRight.displ.x, 0)
                txtdvD.Text = Math.Round(Optisur.DisplRight.displ.y, 0)
                LBLFTD.Text = ""
                'Select Case MoldeD.CveMensaje
                '    Case 0
                '        LBLFTD.Text = ""
                '    Case 9
                '        LBLFTD.Text = ""
                '        'If Not Form1.TextComentarios.Text.Contains("D: " + MoldeD.Mensaje) Then
                '        '    Form1.TextComentarios.Text &= vbCrLf & "D: " + MoldeD.Mensaje
                '        'End If
                '        'MsgBox(Form1.TextComentarios.Text)
                '    Case Else
                '        LBLFTD.Text = MoldeD.Mensaje
                'End Select

                PanelRightSide.Visible = True

            End If



            'lado izquierdo
            If (CheckedEyes = Eyes.Left Or CheckedEyes = Eyes.Both) And (Not Optisur.LeftSide Is Nothing) Then
                txtesfI.Text = Math.Round(Optisur.LeftSide.Lens.sphere, 2)
                txtcilI.Text = Math.Round(Optisur.LeftSide.Lens.cylinder, 2)
                txtejeI.Text = Optisur.LeftSide.Lens.axis
                txtadicI.Text = Optisur.LeftSide.Lens.addition
                txtprisI.Text = Math.Round(Optisur.LensDsgLeft.Prism.diop, 2)
                txtejeprisI.Text = Optisur.LensDsgLeft.Prism.axis
                txtaltI.Text = Math.Round(Optisur.LeftSide.Lens.segHeigth, 2)
                lblmoldeI.Text = String.Format("{0:000}", Optisur.LensDsgLeft.lapSurf(0).base.power * 100) & "-" & String.Format("{0:000}", Optisur.LensDsgLeft.lapSurf(0).cross.power * 100)
                lblmoldeExI.Text = String.Format("{0:000}", Math.Round(Optisur.LensDsgLeft.lapSurf(0).base.power, 2) * 100) & "-" & String.Format("{0:000}", Math.Round(Optisur.LensDsgLeft.lapSurf(0).cross.power, 2) * 100)
                'molde high definition
                If Optisur.LensDsgLeft.lapSurf(0).base.power = 0.0 Then
                    lblmoldeI_HD.Text = ""
                Else
                    lblmoldeI_HD.Text = String.Format("{0:000}", Optisur.LensDsgLeft.lapSurf(0).base.power * 100) & "-" & String.Format("{0:000}", Optisur.LensDsgLeft.lapSurf(0).cross.power * 100)
                End If
                '*********************
                txtorillaminI.Text = Math.Round(Optisur.LensDsgLeft.minEThk.thk, 2)
                txtcentroI.Text = Math.Round(Optisur.LensDsgLeft.cenThk, 2)
                txtorillaMaxI.Text = Math.Round(Optisur.LensDsgLeft.maxEthk.thk, 2)
                txtdhI.Text = Math.Round(Optisur.DisplLeft.displ.x, 0)
                txtdvI.Text = Math.Round(Optisur.DisplLeft.displ.y, 0)
                LBLFTI.Text = ""
                'Select Case MoldeI.CveMensaje
                '    Case 0
                '        LBLFTI.Text = ""
                '    Case 9
                '        LBLFTI.Text = ""
                '    Case Else
                '        LBLFTI.Text = MoldeI.Mensaje
                'End Select

                PanelLeftSide.Visible = True

            End If


            'caja
            If Optisur.IsManual Then
                txtaro.Text = Optisur.GeoFrame.hrzBox
                txtdiag.Text = Optisur.GeoFrame.efDiam
                txtvert.Text = Optisur.GeoFrame.vrtBox
                txtpuente.Text = Optisur.GeoFrame.Frame.dbl
            Else
                txtaro.Text = Optisur.Calculos.GeoFrame.hrzBox
                txtdiag.Text = Optisur.Calculos.GeoFrame.efDiam
                txtvert.Text = Optisur.Calculos.GeoFrame.vrtBox
                txtpuente.Text = Optisur.Calculos.GeoFrame.Frame.dbl
            End If
        Else
            'txtesfD.Text = Math.Round(MoldeD.Esfera, 2)
            'txtcilD.Text = Math.Round(MoldeD.Cilindro, 2)
            'txtejeD.Text = MoldeD.Eje
            'txtadicD.Text = MoldeD.Adicion
            'txtprisD.Text = Math.Round(MoldeD.Prisma, 2)
            'txtejeprisD.Text = MoldeD.EjePrisma
            'txtaltD.Text = Math.Round(FormaD.Altura, 1)
            'lblmoldeD.Text = String.Format("{0:000}", MoldeD.M_Base * 100) & " - " & String.Format("{0:000}", MoldeD.M_Cruz * 100)
            'lblmoldeExD.Text = String.Format("{0:000}", Math.Round(MoldeD.M_Base_Ex, 2) * 100) & " - " & String.Format("{0:000}", Math.Round(MoldeD.M_Cruz_Ex, 2) * 100)
            ''MOLDE DE HIGH DEFINITION
            'If MoldeD.M_Base_HD = 0.0 Then
            '    lblmoldeD_HD.Text = ""
            'Else
            '    lblmoldeD_HD.Text = String.Format("{0:000}", MoldeD.M_Base_HD * 100) & " - " & String.Format("{0:000}", MoldeD.M_Cruz_HD * 100)
            'End If

            ''**************************************************
            ''**** leemos el archivo de configuracion para determinar con la tabla de trazos que se va a trabajar
            'Select Case My.Settings.OpticalProtocol
            '    Case "DVI"
            '        TracerTable = "TracerData"
            '        VwConsultaTrazos = "TracerData"
            '    Case "OMA"
            '        TracerTable = "OMATraces"
            '        VwConsultaTrazos = "VwOMATraces"
            'End Select
            ''**************************************************

            ''**************************
            'txtorillaminD.Text = Math.Round(MoldeD.GrosorMinimo, 2)
            'txtcentroD.Text = Math.Round(MoldeD.GrosorCentral, 2)
            'txtorillamaxD.Text = Math.Round(MoldeD.GrosorMaximo, 2)
            'txtdhD.Text = Math.Round(FormaD.DH, 0)
            'txtdvD.Text = Math.Round(FormaD.DV, 0)
            'Select Case MoldeD.CveMensaje
            '    Case 0
            '        LBLFTD.Text = ""
            '    Case 9
            '        LBLFTD.Text = ""
            '        'If Not Form1.TextComentarios.Text.Contains("D: " + MoldeD.Mensaje) Then
            '        '    Form1.TextComentarios.Text &= vbCrLf & "D: " + MoldeD.Mensaje
            '        'End If
            '        'MsgBox(Form1.TextComentarios.Text)
            '    Case Else
            '        LBLFTD.Text = MoldeD.Mensaje
            'End Select



            ''lado izquierdo
            'txtesfI.Text = Math.Round(MoldeI.Esfera, 2)
            'txtcilI.Text = Math.Round(MoldeI.Cilindro, 2)
            'txtejeI.Text = MoldeI.Eje
            'txtadicI.Text = MoldeI.Adicion
            'txtprisI.Text = Math.Round(MoldeI.Prisma, 2)
            'txtejeprisI.Text = MoldeI.EjePrisma
            'txtaltI.Text = Math.Round(FormaI.Altura, 2)
            'lblmoldeI.Text = String.Format("{0:000}", MoldeI.M_Base * 100) & " - " & String.Format("{0:000}", MoldeI.M_Cruz * 100)
            'lblmoldeExI.Text = String.Format("{0:000}", Math.Round(MoldeI.M_Base_Ex, 2) * 100) & " - " & String.Format("{0:000}", Math.Round(MoldeI.M_Cruz_Ex, 2) * 100)
            ''molde high definition
            'If MoldeI.M_Base_HD = 0.0 Then
            '    lblmoldeI_HD.Text = ""
            'Else
            '    lblmoldeI_HD.Text = String.Format("{0:000}", MoldeI.M_Base_HD * 100) & " - " & String.Format("{0:000}", MoldeI.M_Cruz_HD * 100)
            'End If
            ''*********************
            'txtorillaminI.Text = Math.Round(MoldeI.GrosorMinimo, 2)
            'txtcentroI.Text = Math.Round(MoldeI.GrosorCentral, 2)
            'txtorillaMaxI.Text = Math.Round(MoldeI.GrosorMaximo, 2)
            'txtdhI.Text = Math.Round(FormaI.DH, 0)
            'txtdvI.Text = Math.Round(FormaI.DV, 0)
            'Select Case MoldeI.CveMensaje
            '    Case 0
            '        LBLFTI.Text = ""
            '    Case 9
            '        LBLFTI.Text = ""
            '    Case Else
            '        LBLFTI.Text = MoldeI.Mensaje
            'End Select


            ''caja
            'txtaro.Text = FormaD.Aro
            'txtdiag.Text = FormaD.Diagonal
            'txtvert.Text = FormaD.Vertical
            'txtpuente.Text = puente
        End If
        btnEjecutarRx.Select()

    End Sub


    Public Sub New(ByVal puente2 As Single, ByVal rx As String, ByVal mat_arm As Integer, ByVal IsManualframe As Boolean, ByVal RxVars As Calcot.RxPrintInfo, ByVal ChangingTraces As Boolean, ByRef OMA As OMAFile)

        ' This call is required by the Windows Form Designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        IsChangingTrace = ChangingTraces
        Me.OMA = OMA
        puente = puente2
        receta = rx
        Me.Mat_Arm = mat_arm
        Me.IsManualFrame = IsManualframe
        Me.RxInfo = RxVars
        If Not (RxInfo.CheckedEyes = Calcot.RxPrintInfo.Eyes.Both) Then
            If RxInfo.CheckedEyes And Calcot.RxPrintInfo.Eyes.Left Then
                PanelLeftSide.Visible = True
                PanelRightSide.Visible = False
                Label17.Visible = False
                Label18.Visible = False
                Label19.Visible = True
                Label20.Visible = True
                txtdhD.Visible = False
                txtdvD.Visible = False
                txtdhI.Visible = True
                txtdvI.Visible = True
            Else
                PanelLeftSide.Visible = False
                PanelRightSide.Visible = True
                Label17.Visible = True
                Label18.Visible = True
                Label19.Visible = False
                Label20.Visible = False
                txtdhD.Visible = True
                txtdvD.Visible = True
                txtdhI.Visible = False
                txtdvI.Visible = False
            End If
        Else
            'lbldhd.Visible = True
            'lbldvd.Visible = True
            'lbldhi.Visible = True
            'lbldvi.Visible = True
            'txtdhD.Visible = True
            'txtdvD.Visible = True
            'txtdhI.Visible = True
            'txtdvI.Visible = True
        End If
        txtprisD.Text = 0
        txtprisI.Text = 0
        txtejeprisD.Text = 0
        txtejeprisI.Text = 0
        If receta = "0" Then
            Throw New Exception("No existe Referencia Weco = Receta #")
        End If
    End Sub
    Function MirrorRadios(ByVal radios() As Integer) As Integer()
        Dim i As Integer
        Dim j As Integer = 0
        Dim mirror(NbrsTraces()) As Integer


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

        For i = X2 - 1 To 0 Step -1
            mirror(j) = radios(i)
            j += 1
        Next
        For i = X4 - 1 To X2 Step -1
            mirror(j) = radios(i)
            j += 1
        Next
        Return mirror
    End Function
    Sub WriteOMAFile()
        Dim oma As New OMAFile("150081235", "1234", 400)
        oma.SetSPH(-2, -2)
        oma.SetCYL(0, 0)
        oma.SetAX(0, 0)
        oma.SetIPD(31, 31)
        oma.SetNPD(0, 0)
        oma.SetADD(0, 0)
        oma.SetSEGHT(0, 0)
        oma.SetOCHT(15, 15)
        oma.SetPRVA(0, 0)
        oma.SetPRVM(0, 0)
        oma.SetHBOX(48.37, 48.26)
        oma.SetVBOX(29.77, 29.88)
        oma.SetDBL(21.33)
        oma.SetCIRC(129.15, 129.35)
        oma.SetFMAT(OMAFile.FrameMaterial.Zyl)
        oma.SetZTILT(4.9, 6.8)
        oma.SetPIND(1.586, 1.586)
        oma.SetLIND(1.586, 1.586)
        oma.SetLMATID(3, 3)
        oma.SetLMATNAME(OMAFile.MaterialName.Polycarbonate, OMAFile.MaterialName.Polycarbonate)
        oma.SetLMATTYPE(OMAFile.MaterialType.Polycarbonate, OMAFile.MaterialType.Polycarbonate)
        oma.SetLMFR(OMAFile.Manufacturer.GTX, OMAFile.Manufacturer.GTX)
        oma.SetLNAM(OMAFile.LensType.SingleVision, OMAFile.LensType.SingleVision)
        oma.SetLTYP(OMAFile.LensType.SingleVision, OMAFile.LensType.SingleVision)
        oma.SetCOLR("SRC", "SRC")
        oma.SetLSIZ(70, 70)
        oma.SetTINT("", "")
        oma.SetACOAT("", "")
        oma.SetDIA(70, 70)
        oma.SetMBASE(5, 5)
        oma.SetFRNT(5.37, 5.37)
        oma.SetBACK(-6.1, -6.1)
        oma.SetSWIDTH(0, 0)
        oma.SetOPC("2105030007", "2105030007")
        oma.SetSBSGUP(0, 0)
        oma.SetSBSGIN(0, 0)
        oma.SetSBBCUP(0, 0)
        oma.SetSBBCIN(0, 0)
        oma.SetGAX(0, 0)
        oma.SetBCOCIN(0, 0)
        oma.SetBCOCUP(0, 0)
        oma.SetBCSGIN(0, 0)
        oma.SetBCSGUP(0, 0)
        oma.SetBCTHK(8, 8)
        oma.SetBETHK(8.9, 8.9)
        oma.SetBPRVA(0, 0)
        oma.SetBPRVM(0, 0)
        oma.SetRNGD(60, 60)
        oma.SetRNGH(11.8, 11.8)
        oma.SetBLKB(4, 4)
        oma.SetBLKD(54.58, 54.58)
        oma.SetBLKTYP(3, 3)
        oma.SetKPRVA(0, 0)
        oma.SetKPRVM(0, 0)
        oma.SetTIND(1.53, 1.53)
        oma.SetSBBCUP(0, 0)
        oma.SetSBBCUP(-4.5, -4.5)
        oma.SetGBASE(-7.25, -7.25)
        oma.SetGCROS(-7.25, -7.25)
        oma.SetGBASEX(-7.21, -7.21)
        oma.SetGCROSX(-7.21, -7.21)
        oma.SetGTHK(2.11, 2.11)
        oma.SetFINCMP(0, 0)
        oma.SetTHKCMP(0, 0)
        oma.SetBLKCMP(0, 0)
        oma.SetRNGCMP(0, 0)
        oma.SetSAGBD(6.41, 6.41)
        oma.SetSAGCD(4.74, 4.74)
        oma.SetSAGRD(4.67, 4.67)
        oma.SetGPRVA(0, 0)
        oma.SetGPRVM(0, 0)
        oma.SetCPRVA(0, 0)
        oma.SetCPRVM(0, 0)
        oma.SetIFRNT(5.37, 5.37)
        oma.SetOTHK(2.11, 2.11)
        oma.SetSBOCUP(0, 0)
        oma.SetSBOCIN(0, 0)
        oma.SetRPRVA(0, 0)
        oma.SetRPRVM(0, 0)
        oma.SetCRIB(60.47, 60.47)
        oma.SetAVAL(0, 0)
        oma.SetSVAL(0, 0)
        oma.SetELLH(60.47, 60.47)
        oma.SetFLATA(0, 0)
        oma.SetFLATB(0, 0)
        oma.SetPREEDGE(OMAFile.EnableStatus.Disable, OMAFile.EnableStatus.Disable)
        oma.SetPADTHK(0.46, 0.46)
        oma.SetLAPBAS(7.25, 7.25)
        oma.SetLAPCRS(7.25, 7.25)
        oma.SetLAPBASX(7.21, 7.21)
        oma.SetLAPCRSX(7.21, 7.21)
        oma.SetLAPM(0, 0)
        oma.SetFBFCIN(0, 0)
        oma.SetFBFCUP(0, 0)
        oma.SetFBOCIN(4.5, 4.5)
        oma.SetFBOCUP(0, 0)
        oma.SetFBSGIN(4.5, 4.5)
        oma.SetFBSGUP(0, 0)
        oma.SetFCOCIN(4.5, 4.5)
        oma.SetFCOCUP(0, 0)
        oma.SetFCSGIN(4.5, 4.5)
        oma.SetFCSGUP(0, 0)
        oma.SetSGOCIN(0, 0)
        oma.SetSGOCUP(0, 0)
        oma.SetCTHICK(1.61, 1.61)
        oma.SetTHKA(162, 18)
        oma.SetTHKP(3.09, 3.09)
        oma.SetTHNA(90, 270)
        oma.SetTHNP(1.99, 1.99)
        oma.SetMCIRC(0.3)
        oma.SetSLBP(0, 0)
        Dim radios(399) As Integer
        Dim zradios(199) As Integer
        Dim i As Integer
        For i = 0 To 399
            radios(i) = 2200 + i
        Next
        For i = 0 To 199
            zradios(i) = 200 + i
        Next

        oma.SetTRCFMT(OMAFile.TRACEDataFormatIdentifier.BasicASCIIradiiFormat, 400, OMAFile.TRACERadiusModeIdentifier.EvenlySpaced, OMAFile.TRACESide.Both, OMAFile.TRACEObject.Frame, radios)
        oma.SetZFMT(OMAFile.TRACEDataFormatIdentifier.BasicASCIIradiiFormat, 200, OMAFile.TRACERadiusModeIdentifier.EvenlySpaced, OMAFile.TRACESide.Both, OMAFile.TRACEObject.Frame, zradios)
        oma.SetETYP(OMAFile.LensTypes.Bevel, OMAFile.LensTypes.Bevel)
        oma.SetFTYP(OMAFile.FrameTypes.Plastic)
        oma.SetFPINB(0, 0)
        oma.SetPINB(0.3, 0.3)
        oma.SetGDEPTH(0, 0)
        oma.SetGWIDTH(0, 0)
        oma.SetBEVP(OMAFile.BevelPossition.FreeFloat, OMAFile.BevelPossition.FreeFloat)
        oma.SetBEVC(CSng(1.498), CSng(1.498))
        oma.SetPOLISH(0)
        oma.SetCLAMP(1, 1)
        oma.SetDRILL(OMAFile.EyeSides.Both, 2, 15, 1.5, 0, 2, 15, OMAFile.DrillAngleType.NormalToFront, 90, 90)

        oma.SetDRILLE(OMAFile.EyeSides.None, OMAFile.DrillLocationAndReference.BoxReference, OMAFile.DrillNasalOrTemporalSide.Nasal, OMAFile.DrillMounting.Front, 24.3, 4.17, 3, 24.03, 4.17, 0, OMAFile.DrillFeatureType.NoneOrType1, OMAFile.DrillAngleType.AngleSpecified, 0, 0)
        oma.SetDRILLE(OMAFile.EyeSides.Both, OMAFile.DrillLocationAndReference.BoxReference, OMAFile.DrillNasalOrTemporalSide.Nasal, OMAFile.DrillMounting.Front, 24.3, 4.17, 3, 24.03, 4.17, 0, OMAFile.DrillFeatureType.NoneOrType1, OMAFile.DrillAngleType.AngleSpecified, 0, 0)
        oma.SetDRILLE(OMAFile.EyeSides.Both, OMAFile.DrillLocationAndReference.BoxReference, OMAFile.DrillNasalOrTemporalSide.Nasal, OMAFile.DrillMounting.Front, 24.3, 4.17)


        Try
            oma.WriteFile("c:\Test_OMA_File.txt")
            MsgBox("Archivo Guardado Exitosamente!!", MsgBoxStyle.Information)
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Exclamation)
        End Try

        'MsgBox(oma.GetOMAString(), MsgBoxStyle.Information)
        End
    End Sub

    Private Sub btnEjecutarRx_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnEjecutarRx.Click, BotonEjecutar.Click
        Dim calculoCA As Calcot.calculoCA = Nothing
        Dim calculoSA As Calcot.calculoSA

        Try
            If Not RxInfo.SinArmazon Then
                If Framenum > "0" Then
                    If My.Settings.SaveUniqueTraces Then
                        radiosR = MirrorRadios(radiosL)
                        SaveDataToSavedTracesDB()
                    End If
                End If
                If My.Settings.EnableOptisur Then
                    'calculoCA = New Calcot.calculoCA(LD, LI)
                    'calculoCA.Confirmar = True 'solicitamos grabar en guiador la receta
                    'calculoCA.Puente = puente
                    'calculoCA.Receta = receta
                    'calculoCA.Mat_Arm = Me.Mat_Arm
                    'calculoCA.Ajuste = 0
                    'Select Case My.Settings.OpticalProtocol
                    '   Case "DVI"
                    'calculoCA.SetRadios = radios
                    '    Case "OMA"
                    'calculoCA.SetRadios = Convertir400To512(radios)
                    'End Select

                    'calculoCA.GetCalculoCA(My.Settings.Optimizar)
                    'si se acepto la receta en el guiador imprimos la receta, si no mandamos el error
                    If Optisur.ErrorCode = 0 Then
                        'grabar trazos si estos fueron manuales en la base de datos local o si la receta proviene del virtual
                        If IsManualFrame Or RxInfo.RxFromVirtual Then
                            ' PFL Sep 9/19/2013 Al parecer no es necesario hacer el reflejo del trazo si nos aseguramos de que
                            ' el ds proveniente de la vista MfgSys803.dbo.vwRxVirtualesConTrazo del AR_List trae en su primera file el lado L
                            'radios = MirrorRadios(radios)
                            SaveDataToTracerDB()
                        Else
                            'radios = MirrorRadios(radios)
                            'SaveDataToTracerDB()
                            'Dim t As New SqlDB(Read_Registry("TracerDataServer"), My.Settings.DBUser, My.Settings.DBPassword, My.Settings.LocalDBName)
                            'Try
                            '    t.OpenConn()
                            '    t.SQLDS("UPDATE TracerData set status = 0 WHERE jobnum = '" & receta & "' ", "t1")
                            'Catch ex As Exception
                            'Finally
                            '    t.CloseConn()
                            'End Try
                        End If


                        'MOD BY Marco Angulo
                        'SI LA RECETA ES REMOTA (PROVIENE DEL VIRTUAL) ES NECESARIO MARCAR LA RECETA COMO YA PROCESADA
                        'CONECCION ALA BASE DE DATOS POSTGRESQL DE GUIADOR Y ACTUALIZAR EL CAMPO PEND AL NUM 22 = RX PROCESADA
                        If RxInfo.RxFromVirtual Then Update_RxVirtual()
                        'impresion de la receta
                        ' Dim myt As Threading.Thread
                        ' myt = New Threading.Thread(New Threading.ThreadStart(AddressOf PrintRx))
                        'myt.Start()
                        'PrintRx()
                        'CREAMOS EL OBJETO PARA IMPRIMIR EN EL FORM1
                        CreatePrintRxObj()
                        '*******************************************
                        SuccessfulTran = True
                        Me.Close()
                    Else
                        Throw New Exception(calculoCA.GetMensaje)
                    End If
                Else
                    calculoCA = New Calcot.calculoCA(LD, LI)
                    calculoCA.Confirmar = True 'solicitamos grabar en guiador la receta
                    calculoCA.Puente = puente
                    calculoCA.Receta = receta
                    calculoCA.Mat_Arm = Me.Mat_Arm
                    calculoCA.Ajuste = 0
                    Select Case My.Settings.OpticalProtocol
                        Case "DVI"
                            calculoCA.SetRadios = radiosL
                        Case "OMA"
                            calculoCA.SetRadios = Convertir400To512(radiosL)
                    End Select

                    calculoCA.GetCalculoCA(My.Settings.Optimizar)
                    'si se acepto la receta en el guiador imprimos la receta, si no mandamos el error
                    If calculoCA.GetCveMensaje = 1 Then
                        'grabar trazos si estos fueron manuales en la base de datos local o si la receta proviene del virtual
                        If IsManualFrame Or RxInfo.RxFromVirtual Then
                            'radios = MirrorRadios(radiosL)
                            SaveDataToTracerDB()
                        Else
                            'radios = MirrorRadios(radios)
                            'SaveDataToTracerDB()
                            'Dim t As New SqlDB(Read_Registry("TracerDataServer"), My.Settings.DBUser, My.Settings.DBPassword, My.Settings.LocalDBName)
                            'Try
                            '    t.OpenConn()
                            '    t.SQLDS("UPDATE TracerData set status = 0 WHERE jobnum = '" & receta & "' ", "t1")
                            'Catch ex As Exception
                            'Finally
                            '    t.CloseConn()
                            'End Try
                        End If


                        'MOD BY Marco Angulo
                        'SI LA RECETA ES REMOTA (PROVIENE DEL VIRTUAL) ES NECESARIO MARCAR LA RECETA COMO YA PROCESADA
                        'CONECCION ALA BASE DE DATOS POSTGRESQL DE GUIADOR Y ACTUALIZAR EL CAMPO PEND AL NUM 22 = RX PROCESADA
                        If RxInfo.RxFromVirtual Then Update_RxVirtual()
                        'impresion de la receta
                        ' Dim myt As Threading.Thread
                        ' myt = New Threading.Thread(New Threading.ThreadStart(AddressOf PrintRx))
                        'myt.Start()
                        'PrintRx()
                        'CREAMOS EL OBJETO PARA IMPRIMIR EN EL FORM1
                        CreatePrintRxObj()
                        '*******************************************
                        SuccessfulTran = True
                        Me.Close()
                    Else
                        Throw New Exception(calculoCA.GetMensaje)
                    End If

                End If
            Else
                If Not Form1.RxNoCalculos Then
                    'calculoSA = New Calcot.calculoSA(LD, LI)
                    'calculoSA.Confirmar = True 'solicitamos grabar en guiador la receta
                    'calculoSA.Receta = receta

                    ' ''calculoSA.GetCalculoSA()
                    'calculoSA.GetCalculoSA(My.Settings.Optimizar)
                    Optisur.GetCalculos(NewCalcot.CalculationOption.CalculateAndSave)


                    'Mod By Marco Angulo 3/July/2006
                    'Si la receta es sin armazon y el lente es terminado entonces la rx no tiene calculos y por
                    'lo tanto no se graba en guiador, solo se recibe la cve del msg en cero (0)
                    'si los dos ojos son terminado, entonces la clave regresada por el guiador es cero en vez de uno

                    'If RxInfo.LeftFinished And RxInfo.RightFinished Then
                    '    If calculoSA.GetCveMensaje <> 0 Then Throw New Exception(calculoSA.GetMensaje)
                    'Else
                    '    If Form1.CheckLEye.Checked And RxInfo.LeftFinished Then
                    '        If calculoSA.GetCveMensaje <> 0 Then Throw New Exception(calculoSA.GetMensaje)
                    '        'ElseIf Form1.CheckLEye.Checked And Not RxInfo.LeftFinished Then
                    '        '    If calculoSA.GetCveMensaje <> 0 Then Throw New Exception(calculoSA.GetMensaje)
                    '    ElseIf Form1.CheckREye.Checked And RxInfo.RightFinished Then
                    '        If calculoSA.GetCveMensaje <> 0 Then Throw New Exception(calculoSA.GetMensaje)
                    '        'ElseIf Form1.CheckREye.Checked And Not RxInfo.RightFinished Then
                    '        '    If calculoSA.GetCveMensaje <> 0 Then Throw New Exception(calculoSA.GetMensaje)
                    '    Else
                    '        If calculoSA.GetCveMensaje <> 1 Then Throw New Exception(calculoSA.GetMensaje)
                    '    End If

                    'End If

                    If RxInfo.LeftFinished And RxInfo.RightFinished Then
                        If Optisur.ErrorCode <> 0 Then Throw New Exception(Optisur.ErrorCode & " - " & Optisur.ErrorDescription)
                    Else
                        If Form1.CheckLEye.Checked And RxInfo.LeftFinished Then
                            If Optisur.ErrorCode <> 0 Then Throw New Exception(Optisur.ErrorCode & " - " & Optisur.ErrorDescription)
                            'ElseIf Form1.CheckLEye.Checked And Not RxInfo.LeftFinished Then
                            '    If calculoSA.GetCveMensaje <> 0 Then Throw New Exception(calculoSA.GetMensaje)
                        ElseIf Form1.CheckREye.Checked And RxInfo.RightFinished Then
                            If Optisur.ErrorCode <> 0 Then Throw New Exception(Optisur.ErrorCode & " - " & Optisur.ErrorDescription)
                            'ElseIf Form1.CheckREye.Checked And Not RxInfo.RightFinished Then
                            '    If calculoSA.GetCveMensaje <> 0 Then Throw New Exception(calculoSA.GetMensaje)
                        Else
                            If Optisur.ErrorCode <> 1 Then Throw New Exception(Optisur.ErrorCode & " - " & Optisur.ErrorDescription)
                        End If

                    End If

                End If

                'SI NO OCURRIO UNA EXEPCION EN EL IF ANTERIOR, ENTONCES LOS CALCULOS ESTAN BIEN Y CONTINUAMOS CON LA IMPRESION

                'si se acepto la receta en el guiador imprimos la receta, si no mandamos el error
                'If calculoSA.GetCveMensaje = 1 Then
                'grabar trazos si estos fueron manuales en la base de datos local (todos en 0)
                SaveDataToTracerDB()
                'MOD BY Marco Angulo
                'SI LA RECETA ES REMOTA (PROVIENE DEL VIRTUAL) ES NECESARIO MARCAR LA RECETA COMO YA PROCESADA
                'CONECCION A LA BASE DE DATOS POSTGRESQL DE GUIADOR Y ACTUALIZAR EL CAMPO PEND AL NUM 22 = RX PROCESADA
                If RxInfo.RxFromVirtual Then Update_RxVirtual()
                'impresion de la receta
                ' Dim myt As Threading.Thread
                ' myt = New Threading.Thread(New Threading.ThreadStart(AddressOf PrintRx))
                ' myt.Start()
                'PrintRx()
                'CREAMOS EL OBJETO PARA IMPRIMIR EN EL FORM1
                CreatePrintRxObj()
                '*******************************************
                SuccessfulTran = True
                Me.Close()
                'Else
                'Throw New Exception(calculoSA.GetMensaje)
                'End If
                'MsgBox("Poner codigo ejecutar sin armazon", MsgBoxStyle.Critical)
            End If
        Catch ex As Exception
            calculoCA = Nothing
            calculoSA = Nothing
            MsgBox(ex.Message)
        End Try
    End Sub

    ''' <summary>
    ''' Returns a ReturnMoldeDer object with the Right Info
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetMoldeDerInfo() As Calcot.ReturnMoldeDer
        Dim Molde As New Calcot.ReturnMoldeDer
        Molde.Adicion = Optisur.RightSide.Lens.addition
        Molde.Anillo = Optisur.LensDsgRight.ringSurf.blockName.Replace("ANILLO ", "").Replace("MOLDE ", "")
        Molde.Anillo_HD = Molde.Anillo
        Molde.Cilindro = Optisur.RightSide.Lens.cylinder
        Molde.CveMensaje = Optisur.ErrorCode
        Molde.Eje = Optisur.RightSide.Lens.axis
        Molde.EjePrisma = Optisur.LensDsgRight.Prism.axis
        Molde.Esfera = Optisur.RightSide.Lens.sphere
        Molde.GrosorCentral = Optisur.LensDsgRight.cenThk
        Molde.GrosorMaximo = Optisur.LensDsgRight.maxEthk.thk
        Molde.GrosorMinimo = Optisur.LensDsgRight.minEThk.thk
        Molde.M_Base = Optisur.LensDsgRight.lapSurf(0).base.power
        If Optisur.LensDsgRight.lapSurf().Length > 1 Then
            Molde.M_Base_HD = Optisur.LensDsgRight.lapSurf(1).base.power
            Molde.M_Cruz_HD = Optisur.LensDsgRight.lapSurf(1).cross.power
        End If
        Molde.M_Base_Ex = Optisur.LensDsgRight.genSurf.base.power
        'Molde.M_Base
        'Molde.M_Base_HD = Molde.M_Base
        Molde.M_Cruz = Optisur.LensDsgRight.lapSurf(0).cross.power
        Molde.M_Cruz_Ex = Optisur.LensDsgRight.genSurf.cross.power
        'Molde.M_Cruz_HD = Molde.M_Cruz
        Molde.Mensaje = Optisur.DisplRight.notice
        Molde.Optimizada = "False"
        Molde.Prisma = Optisur.LensDsgLeft.Prism.diop

        If Not (Optisur.LensDsgRight.ringSurf.BlockCurve Is Nothing) Then
            Molde.Bname = Optisur.LensDsgRight.ringSurf.BlockCurve.bCName
        Else
            ' Pedro Far�as L. Nov 28 2012
            ' Ya no queremos tener que dar 2 enters para continuar ...

            'MsgBox("Su Generador de Forma libre no manda informaci�n completa del anillo." + vbCrLf + "Actualize su guiador", MsgBoxStyle.Information, "Optisur.LendsDsgRight.ringSurf.BlockCurve")
        End If

        Return Molde
    End Function

    ''' <summary>
    ''' Returns a ReturnMoldeIzq object with the Left Info
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetMoldeIzqInfo() As Calcot.ReturnMoldeIzq
        Dim Molde As New Calcot.ReturnMoldeIzq
        Molde.Adicion = Optisur.LeftSide.Lens.addition
        Molde.Anillo = Optisur.LensDsgLeft.ringSurf.blockName.Replace("ANILLO ", "").Replace("MOLDE ", "")
        Molde.Anillo_HD = Molde.Anillo
        Molde.Cilindro = Optisur.LeftSide.Lens.cylinder
        Molde.CveMensaje = Optisur.ErrorCode
        Molde.Eje = Optisur.LeftSide.Lens.axis
        Molde.EjePrisma = Optisur.LensDsgLeft.Prism.axis
        Molde.Esfera = Optisur.LeftSide.Lens.sphere
        Molde.GrosorCentral = Optisur.LensDsgLeft.cenThk
        Molde.GrosorMaximo = Optisur.LensDsgLeft.maxEthk.thk
        Molde.GrosorMinimo = Optisur.LensDsgLeft.minEThk.thk
        Molde.M_Base = Optisur.LensDsgLeft.lapSurf(0).base.power
        If Optisur.LensDsgLeft.lapSurf().Length > 1 Then
            Molde.M_Base_HD = Optisur.LensDsgLeft.lapSurf(1).base.power
            Molde.M_Cruz_HD = Optisur.LensDsgLeft.lapSurf(1).cross.power
        End If
        Molde.M_Base_Ex = Optisur.LensDsgLeft.genSurf.base.power
        'Molde.M_Base_HD = Molde.M_Base
        Molde.M_Cruz = Optisur.LensDsgLeft.lapSurf(0).cross.power

        Molde.M_Cruz_Ex = Optisur.LensDsgLeft.genSurf.cross.power
        'Molde.M_Cruz_HD = Molde.M_Cruz
        Molde.Mensaje = Optisur.DisplLeft.notice
        Molde.Optimizada = "false"
        Molde.Prisma = Optisur.LensDsgLeft.Prism.diop


        If Not (Optisur.LensDsgLeft.ringSurf.BlockCurve Is Nothing) Then
            Molde.Bname = Optisur.LensDsgLeft.ringSurf.BlockCurve.bCName
        Else
            'MsgBox("Su Generador de Forma libre no manda informaci�n completa del anillo." + vbCrLf + "Actualize su guiador", MsgBoxStyle.Information, "Optisur.LendsDsgLeft.ringSurf.BlockCurve")
            Beep()
        End If

        Return Molde
    End Function

    ''' <summary>
    ''' Returns a SetLadoDerecho Object with the Right Info
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetLD() As Calcot.SetLadoDerecho
        Dim Lado As New Calcot.SetLadoDerecho
        Lado.Adicion = Optisur.RightSide.Lens.addition
        Lado.Cilindro = Optisur.RightSide.Lens.cylinder
        Lado.Codigob = Optisur.RightSide.Blank.Item
        Lado.dpMono = Optisur.RightSide.Lens.monoPD
        Lado.Eje = Optisur.RightSide.Lens.axis
        Lado.Esfera = Optisur.RightSide.Lens.sphere
        Lado.Alturai = Optisur.DisplRight.segHt
        Lado.EjePrisma = Optisur.LensDsgRight.Prism.axis
        Lado.Prisma = Optisur.LensDsgRight.Prism.diop
        If Optisur.IsManual Then
            Lado.Dipce = Optisur.GeoFrame.Frame.nearPD
        Else
            Lado.Dipce = Optisur.Calculos.GeoFrame.Frame.nearPD
        End If
        Lado.Lado = 0
        Lado.Optimizada = 0

        Return Lado
    End Function

    ''' <summary>
    ''' Returns a SetLadoDerecho Object with the Left Info
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetLI() As Calcot.SetLadoIzquierdo
        Dim Lado As New Calcot.SetLadoIzquierdo

        Lado.Adicion = Optisur.LeftSide.Lens.addition
        Lado.Cilindro = Optisur.LeftSide.Lens.cylinder
        Lado.Codigob = Optisur.LeftSide.Blank.Item
        Lado.dpMono = Optisur.LeftSide.Lens.monoPD
        Lado.Eje = Optisur.LeftSide.Lens.axis
        Lado.Esfera = Optisur.LeftSide.Lens.sphere
        Lado.Alturai = Optisur.DisplLeft.segHt
        Lado.EjePrisma = Optisur.LensDsgLeft.Prism.axis
        Lado.Prisma = Optisur.LensDsgLeft.Prism.diop
        If Optisur.IsManual Then
            Lado.Dipce = Optisur.GeoFrame.Frame.nearPD
        Else
            Lado.Dipce = Optisur.Calculos.GeoFrame.Frame.nearPD
        End If
        Lado.Lado = 0
        Lado.Optimizada = 0

        Return Lado
    End Function

    ''' <summary>
    ''' Returns a ReturnLadoDer object with the Right Info
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetFormaD() As Calcot.ReturnLadoDer
        Dim Forma As New Calcot.ReturnLadoDer
        Forma.Altura = Optisur.DisplRight.heigth
        Forma.Altura_Oblea = Optisur.DisplRight.segHt
        Forma.Ancho_Oblea = Optisur.DisplRight.segWd
        Forma.DHPrisma = Optisur.DisplRight.displ.x '???????????????
        Forma.DH = Optisur.DisplRight.displ.x
        Forma.diametro = Optisur.DisplRight.diam
        Forma.DV = Optisur.DisplRight.displ.y
        Forma.ExcenH = Optisur.DisplRight.descentering.x
        Forma.ExcenV = Optisur.DisplRight.descentering.y
        Forma.Mensaje = Optisur.DisplRight.notice
        Forma.SegmentoH = Optisur.DisplRight.LRP.x
        Forma.SegmentoV = Optisur.DisplRight.LRP.y
        If Optisur.IsManual Then
            Forma.Aro = Optisur.GeoFrame.hrzBox
            Forma.Diagonal = Optisur.GeoFrame.efDiam
            Forma.Vertical = Optisur.GeoFrame.vrtBox
        Else
            Forma.Aro = Optisur.Calculos.GeoFrame.hrzBox
            Forma.Diagonal = Optisur.Calculos.GeoFrame.efDiam
            Forma.Vertical = Optisur.Calculos.GeoFrame.vrtBox
        End If
        Forma.ClaveMsg = Optisur.ErrorCode

        Return Forma
    End Function

    ''' <summary>
    ''' Returns a ReturnLadoIzq Object with the Left Info
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetFormaI() As Calcot.ReturnLadoIzq
        Dim Forma As New Calcot.ReturnLadoIzq
        Forma.Altura = Optisur.DisplLeft.heigth
        Forma.Altura_Oblea = Optisur.DisplLeft.segHt
        Forma.Ancho_Oblea = Optisur.DisplLeft.segWd
        Forma.DHPrisma = Optisur.DisplLeft.displ.x '???????????????
        Forma.DH = Optisur.DisplLeft.displ.x
        Forma.diametro = Optisur.DisplLeft.diam
        Forma.DV = Optisur.DisplLeft.displ.y
        Forma.ExcenH = Optisur.DisplLeft.descentering.x
        Forma.ExcenV = Optisur.DisplLeft.descentering.y
        Forma.Mensaje = Optisur.DisplLeft.notice
        Forma.SegmentoH = Optisur.DisplLeft.LRP.x
        Forma.SegmentoV = Optisur.DisplLeft.LRP.y
        If Optisur.IsManual Then
            Forma.Aro = Optisur.GeoFrame.hrzBox
            Forma.Diagonal = Optisur.GeoFrame.efDiam
            Forma.Vertical = Optisur.GeoFrame.vrtBox
        Else
            Forma.Aro = Optisur.Calculos.GeoFrame.hrzBox
            Forma.Diagonal = Optisur.Calculos.GeoFrame.efDiam
            Forma.Vertical = Optisur.Calculos.GeoFrame.vrtBox
        End If

        Forma.ClaveMsg = Optisur.ErrorCode

        Return Forma
    End Function


    Private Sub CreatePrintRxObj()
        If My.Settings.EnableOptisur Then

            PrintObj = New Impresion
            If (CheckedEyes = Eyes.Left Or CheckedEyes = Eyes.Both) And (Not Optisur.LeftSide Is Nothing) Then
                PrintObj.MoldeI = GetMoldeIzqInfo()
                PrintObj.LI = GetLI()
                PrintObj.FormaI = GetFormaI()
                PrintObj.PrismaI = txtprisI.Text
                PrintObj.PotFI = "I " & txtesfI.Text & ", " & txtcilI.Text
                PrintObj.MolExI = "I " & lblmoldeExI.Text
                If BaseL = "" Then BaseL = "0"
                PrintObj.BaseL = String.Format("{0:0.00}", CDbl(BaseL))
                PrintObj.AdicionI = RxInfo.AdicionL

            End If
            If (CheckedEyes = Eyes.Right Or CheckedEyes = Eyes.Both) And (Not Optisur.RightSide Is Nothing) Then
                PrintObj.MoldeD = GetMoldeDerInfo()
                PrintObj.LD = GetLD()
                PrintObj.FormaD = GetFormaD()
                PrintObj.PrismaD = txtprisD.Text
                PrintObj.PotFD = "D " & txtesfD.Text & ", " & txtcilD.Text
                PrintObj.MolExD = "D " & lblmoldeExD.Text
                If BaseR = "" Then BaseR = "0"
                PrintObj.BaseR = String.Format("{0:0.00}", CDbl(BaseR))
                PrintObj.AdicionR = RxInfo.AdicionR
            End If


            PrintObj.Puente = String.Format("{0:00.0}", CDbl(puente))
            PrintObj.TinteColor = RxInfo.TinteColor
            PrintObj.TinteNum = RxInfo.TinteNum
            PrintObj.Gradiente = RxInfo.Gradiente
            PrintObj.Dise�o = RxInfo.Dise�o
            PrintObj.Material = RxInfo.Material
            If UCase(RxInfo.Material) = "TRIVEX" Then
                PrintObj.MatTX = "TRIVEX"
            Else
                PrintObj.MatTX = ""
            End If
            PrintObj.Biselado = RxInfo.Biselado
            PrintObj.Cliente = RxInfo.Cliente
            PrintObj.DIP = RxInfo.DipceDiple
            PrintObj.Armazon = RxInfo.Armazon
            'verifiocamos si el armazon es ranurado o perforad para imprimirlo en junto con la etq de biselado
            If RxInfo.Biselado <> "" Then
                Select Case UCase(RxInfo.Armazon)
                    Case "PERFORADO"
                        PrintObj.Biselado = "PERFORADO"
                    Case "RANURADO"
                        PrintObj.Biselado = "RANURADO"
                End Select
            End If
            Dim Salida As Date
            If CStr(Me.FechaInicial) = "" Then
                Try : Salida = CDate(GetFechaHoraFinal(Now)) : Catch ex As Exception : Salida = Now.Date : End Try
                PrintObj.HoraSalida = Salida.ToShortTimeString
                PrintObj.FechaSalida = FormateaFecha(Salida.ToShortDateString) 'Salida.ToShortDateString
                PrintObj.FechaEntrada = FormateaFecha(Now.ToShortDateString)
                PrintObj.HoraEntrada = Now.ToShortTimeString
            Else 'ya existe una fecha de salida
                Try : Salida = CDate(GetFechaHoraFinal(FechaInicial)) : Catch ex As Exception : Salida = Now.Date : End Try
                PrintObj.HoraSalida = FormateaHora(Salida.ToShortTimeString)
                PrintObj.FechaSalida = FormateaFecha(Salida.ToShortDateString)
                PrintObj.FechaEntrada = FormateaFecha(Me.FechaEntrada)
                PrintObj.HoraEntrada = CDate(FechaEntrada).ToShortTimeString
            End If
            PrintObj.AR = RxInfo.AR
            PrintObj.Abrillantado = RxInfo.Abrillantado
            PrintObj.Receta = receta
            PrintObj.RxIdent = RxInfo.Rxident
            'GENERACION DEL CONSECUTIVO (TRABAJO + LOTE) EJ. A01-01 DE LA RECETA
            PrintObj.Lote = Genera_Lote()
        Else
            PrintObj = New Impresion
            PrintObj.MoldeD = Me.MoldeD
            PrintObj.MoldeI = Me.MoldeI
            PrintObj.LD = Me.LD
            PrintObj.LI = Me.LI
            PrintObj.FormaD = Me.FormaD
            PrintObj.FormaI = Me.FormaI

            PrintObj.PrismaD = txtprisD.Text
            PrintObj.PotFD = "D " & txtesfD.Text & ", " & txtcilD.Text
            PrintObj.MolExD = "D " & lblmoldeExD.Text
            PrintObj.PrismaI = txtprisI.Text
            PrintObj.PotFI = "I " & txtesfI.Text & ", " & txtcilI.Text
            PrintObj.MolExI = "I " & lblmoldeExI.Text

            If BaseR = "" Then BaseR = "0"
            PrintObj.BaseR = String.Format("{0:0.00}", CDbl(BaseR))

            If BaseL = "" Then BaseL = "0"
            PrintObj.BaseL = String.Format("{0:0.00}", CDbl(BaseL))

            PrintObj.AdicionR = RxInfo.AdicionR
            PrintObj.AdicionI = RxInfo.AdicionL

            PrintObj.Puente = String.Format("{0:00.0}", CDbl(puente))
            PrintObj.TinteColor = RxInfo.TinteColor
            PrintObj.TinteNum = RxInfo.TinteNum
            PrintObj.Gradiente = RxInfo.Gradiente
            PrintObj.Dise�o = RxInfo.Dise�o
            PrintObj.Material = RxInfo.Material
            If UCase(RxInfo.Material) = "TRIVEX" Then
                PrintObj.MatTX = "TRIVEX"
            Else
                PrintObj.MatTX = ""
            End If
            PrintObj.Biselado = RxInfo.Biselado
            PrintObj.Cliente = RxInfo.Cliente
            PrintObj.DIP = RxInfo.DipceDiple
            PrintObj.Armazon = RxInfo.Armazon
            'verifiocamos si el armazon es ranurado o perforad para imprimirlo en junto con la etq de biselado
            If RxInfo.Biselado <> "" Then
                Select Case UCase(RxInfo.Armazon)
                    Case "PERFORADO"
                        PrintObj.Biselado = "PERFORADO"
                    Case "RANURADO"
                        PrintObj.Biselado = "RANURADO"
                End Select
            End If
            Dim Salida As Date
            If CStr(Me.FechaInicial) = "" Then
                Try : Salida = CDate(GetFechaHoraFinal(Now)) : Catch ex As Exception : Salida = Now.Date : End Try
                PrintObj.HoraSalida = Salida.ToShortTimeString
                PrintObj.FechaSalida = FormateaFecha(Salida.ToShortDateString) 'Salida.ToShortDateString
                PrintObj.FechaEntrada = FormateaFecha(Now.ToShortDateString)
                PrintObj.HoraEntrada = Now.ToShortTimeString
            Else 'ya existe una fecha de salida
                Try : Salida = CDate(GetFechaHoraFinal(FechaInicial)) : Catch ex As Exception : Salida = Now.Date : End Try
                PrintObj.HoraSalida = FormateaHora(Salida.ToShortTimeString)
                PrintObj.FechaSalida = FormateaFecha(Salida.ToShortDateString)
                PrintObj.FechaEntrada = FormateaFecha(Me.FechaEntrada)
                PrintObj.HoraEntrada = CDate(FechaEntrada).ToShortTimeString
            End If
            PrintObj.AR = RxInfo.AR
            PrintObj.Abrillantado = RxInfo.Abrillantado
            PrintObj.Receta = receta
            PrintObj.RxIdent = RxInfo.Rxident
            'GENERACION DEL CONSECUTIVO (TRABAJO + LOTE) EJ. A01-01 DE LA RECETA
            PrintObj.Lote = Genera_Lote()
        End If
    End Sub
    Private Sub SaveDataToTracerDB()
        Dim radios1, radios2, RadiosUpd, sqlstr As String
        Dim i As Integer
        'VARIABLES DE OMA PARA INSERTAR EN LA TABLA
        Dim EyeSide As String = ""
        Dim RadiosStrR, RadiosStrL, OMAFields, OMAValues As String

        Try
            radios1 = ""
            radios2 = ""
            RadiosUpd = ""
            RadiosStrR = ""
            RadiosStrL = ""
            sqlstr = ""
            'dependiendo del protocolo a usar para guardar los trazos asignamos las variables correspondientes
            If My.Settings.OpticalProtocol = "DVI" Then
                For i = 0 To 511
                    If i = 0 Then
                        radios1 = "Int" & i + 1
                        radios2 = RadiosL(i)
                        If RxInfo.ModificacionRx Then RadiosUpd = "Int" & i + 1 & "=" & RadiosL(i)
                    Else
                        radios1 = radios1 & ",Int" & i + 1
                        radios2 = radios2 & "," & RadiosL(i)
                        RadiosUpd = RadiosUpd & ",Int" & i + 1 & "=" & RadiosL(i)
                    End If
                Next
            End If
            If My.Settings.OpticalProtocol = "OMA" Then
                ' El lente que traemos de la variable radiosL es el L (Izquierdo) ya que proviene de la vista
                'MFGSYS803.dbo.VwRxVirtualesconTrazo que trae primero el Lente L  en ds.Table(0).Rows(0)

                'OMA.SetTRCFMT(1, 400, OMAFile.TRACERadiusModeIdentifier.EvenlySpaced, OMAFile.TRACESide.Right, OMAFile.TRACEObject.Frame, RadiosR)
                'OMA.SetTRCFMT(1, 400, OMAFile.TRACERadiusModeIdentifier.EvenlySpaced, OMAFile.TRACESide.Left, OMAFile.TRACEObject.Frame, Me.MirrorRadios(RadiosR))

                'llenamos el ojo izquierdo

                If RadiosR(0) = RadiosR(1) And RadiosR(1) = RadiosR(2) And RadiosR(0) = 0 Then
                    ' No traemos informaci�n del trazo derecho, por lo cual la obtenemos al hacer reflejo del lado Izquierdo
                    Dim IMirror As Integer
                    IMirror = 199
                    For i = 0 To 199
                        RadiosR(IMirror) = RadiosL(i)
                        IMirror -= 1
                    Next
                    IMirror = 399
                    For i = 200 To 399
                        RadiosR(IMirror) = RadiosL(i)
                        IMirror -= 1
                    Next
                End If

                'colocamos las etiquetas del ojo derecho
                Dim Paquete As Integer = 1
                For i = 0 To 399
                    If Paquete = 1 Then
                        RadiosStrR = RadiosStrR + "R="
                    End If
                    If Paquete = 10 Then
                        RadiosStrR = RadiosStrR + CStr(RadiosR(i)) + vbCr
                        Paquete = 1
                    Else
                        RadiosStrR = RadiosStrR + CStr(RadiosR(i)) + ","
                        Paquete += 1
                    End If

                Next


                'colocamos las etiquetas del ojo izquierdo
                Paquete = 1
                For i = 0 To 399
                    If Paquete = 1 Then
                        RadiosStrL = RadiosStrL + "R="
                    End If
                    If Paquete = 10 Then
                        RadiosStrL = RadiosStrL + CStr(RadiosL(i)) + vbCr
                        Paquete = 1
                    Else
                        RadiosStrL = RadiosStrL + CStr(RadiosL(i)) + ","
                        Paquete += 1
                    End If
                Next
                'ACOMPLETAMOS LAS VARIABLES CORRESPONDIENTES PARA ACTUALIZAR OMA TABLE
                'Select Case Me.RxInfo.CheckedEyes
                '    Case Calcot.RxPrintInfo.Eyes.Both
                '        EyeSide = "B"
                '    Case Calcot.RxPrintInfo.Eyes.Left
                '        RadiosStrR = ""
                '        EyeSide = "L"
                '    Case Calcot.RxPrintInfo.Eyes.Right
                '        RadiosStrL = ""
                '        EyeSide = "R"
                'End Select
                '----------------------------------------------------------------
                ' Aqui tenemos los dos ojos de todos modos
                EyeSide = "B"
                OMA.SetDO(OMAFile.EyeSides.Both)
                '----------------------------------------------------------------
                'OMA.SetTRCFMT_R(RadiosStrR)
                'OMA.SetTRCFMT_L(RadiosStrL)
                'MsgBox(OMA.GetOMA_EdgerString())
                'OMA.SetTRCFMT(1, 400, OMAFile.TRACERadiusModeIdentifier.EvenlySpaced, OMAFile.TRACESide.Right, OMAFile.TRACEObject.Frame, RadiosR)
                'OMA.SetTRCFMT(1, 400, OMAFile.TRACERadiusModeIdentifier.EvenlySpaced, OMAFile.TRACESide.Left, OMAFile.TRACEObject.Frame, RadiosL)
                'MsgBox(OMA.GetOMA_EdgerString())
            End If
            '**FIN RADIOS1 CONTIENE EL CAMPO A INSERTAT Y RADIOS2 CONTIENE LOS VALORES A INSERTAR
            '**************************************************************************************************
            Dim conn As New SqlDB(My.Settings.LocalServer, My.Settings.DBUser, My.Settings.DBPassword, My.Settings.LocalDBName)
            conn.OpenConn()
            ' If RxInfo.ModificacionRx And Not Form1.IsReceivingVirtualRx Then 'SI LA RECETA SE ESTA MODIFICANDO ACTUALIZAMOS LOS TRAZOS CASO CONTRARIO LOS GRABAMOS
            Dim ExecCommand As Boolean = False
            If RxInfo.ModificacionRx And Not IsReceivingVirtualRx And IsChangingTrace Then 'SI LA RECETA SE ESTA MODIFICANDO ACTUALIZAMOS LOS TRAZOS CASO CONTRARIO LOS GRABAMOS
                Select Case My.Settings.OpticalProtocol
                    Case "DVI"
                        sqlstr = "UPDATE " & TracerTable & " set dbl = " & RxInfo.Puente & ",fecha = '" & Now & "'," & _
                        RadiosUpd & " WHERE jobnum = '" & Form1.StuffRx(receta) & "' and fecha between '" & Now.Date & "' and '" & Now.Date & " 11:59:59 PM" & "' "

                        'sqlstr = "UPDATE " & TracerTable & " set dbl = " & RxInfo.Puente & ",fecha = '" & Now & "'," & _
                        'RadiosUpd & " WHERE jobnum = '" & receta & "' and fecha between '" & Now.Date & "' and '" & Now.Date & " 11:59:59 PM" & "' "
                        ExecCommand = True
                    Case "OMA"
                        sqlstr = "UPDATE " & TracerTable & " set dbl ='DBL=" & RxInfo.Puente / 100 & "',fecha = '" & Now & "' WHERE jobnum = '" & Form1.StuffRx(receta) & "' --and fecha between '" & Now.Date & "' and '" & Now.Date & " 11:59:59 PM" & "' "

                        'sqlstr = "UPDATE " & TracerTable & " set dbl ='DBL=" & RxInfo.Puente / 100 & "',fecha = '" & Now & "'," & _
                        '"TrcRight='" & RadiosStrR & "',TrcLeft='" & RadiosStrL & "' " & " WHERE jobnum = '" & receta & "' and fecha between '" & Now.Date & "' and '" & Now.Date & " 11:59:59 PM" & "' "
                        ExecCommand = True
                End Select
            ElseIf Not RxInfo.ModificacionRx Then
                Dim fuente As Int16 = 0
                If IsReceivingVirtualRx Then
                    fuente = 1
                End If
                Select Case My.Settings.OpticalProtocol
                    Case "DVI"
                        sqlstr = "INSERT INTO " & TracerTable & " (jobnum,fuente,dbl,fecha,status,sizing," & radios1 & ") " & _
                                 "VALUES ('" & receta & "'," & fuente & "," & RxInfo.Puente & ",'" & Now & "',0,0," & radios2 & ")"

                        'sqlstr = "INSERT INTO " & TracerTable & " (jobnum,fuente,dbl,fecha,status,sizing," & radios1 & ") " & _
                        '         "VALUES ('" & receta & "',0," & RxInfo.Puente & ",'" & Now & "',0,0," & radios2 & ")"
                        ExecCommand = True
                    Case "OMA"
                        ' Asi es como estaba originalmente
                        OMAFields = "jobnum,fuente,dbl,fecha,status,TrcRight,TrcLeft,EyeSide" & _
                                    ",HBOX,VBOX,CIRC,BEVC,FCRV,TRCFMT,ZFMT,TRCFMTR,ZFMTR,TRCFMTL,ZFMTL" & _
                                    ",ZTRCRIGHT,ZTRCLEFT"
                        OMAValues = "'','','','','','TRCFMT=1;400;E;B','','TRCFMT=1;400;E;R;F','','TRCFMTL=1;400;E;L;F',''," & _
                                    "'',''"

                        'OMAFields = "jobnum,fuente,dbl,fecha,status,TrcRight,TrcLeft,EyeSide" & _
                        '            ",HBOX,VBOX,CIRC,BEVC,FCRV,TRCFMT,ZFMT,TRCFMTR,ZFMTR,TRCFMTL,ZFMTL" & _
                        '            ",ZTRCRIGHT,ZTRCLEFT" & _
                        '            ",FBOCIN,FBSGUP,LTYP,POLISH,BEVP" & _
                        '            ",LMATID,IPD,NPD,CRIB,DRILL" & _
                        '            ",FTYP,ETYP,SWIDTH,SDEPTH,BCOCIN,BCOCUP,DIA,FCSGIN,FCSGUP,FCOCUP,FCOCIN"
                        'OMAValues = "'','','','','','TRCFMT=1;400;E;B','','TRCFMT=1;400;E;R;F','','TRCFMT=1;400;E;L;F',''," & _
                        '            "'',''" & _
                        '            ",'','','','','','','','','','','','','','','','','','','','',''"

                        If My.Settings.HasMEIEdger Then
                            OMAValues = "'" & OMA.GetHBOX.Replace(vbCrLf, "") & "','" & OMA.GetVBOX.Replace(vbCrLf, "") & "','CIRC=?;?','BEVC=?;?','FCRV=?;?','TRCFMT=1;400;E;B;F','ZFMT=?;?;?;?','TRCFMT=1;400;E;R;F','ZFMT=?;?;?;?','TRCFMT=1;400;E;L;F','ZFMT=?;?;?;?'," & _
                                        "'','','FBOCIN=?;?','FBSGUP=?;?','" & OMA.GetLTYP.Replace(vbCrLf, "") & "','" & OMA.GetPOLISH.Replace(vbCrLf, "") & "','" & OMA.GetBEVP.Replace(vbCrLf, "") & "'" & _
                                        ",'" & OMA.GetLMATID.Replace(vbCrLf, "") & "','" & OMA.GetIPD.Replace(vbCrLf, "") & "','" & OMA.GetNPD.Replace(vbCrLf, "") & "','" & OMA.GetCRIB.Replace(vbCrLf, "") & "','" & OMA.GetDRILL.Replace(vbCrLf, "") & "'" & _
                                        ",'" & OMA.GetFTYP.Replace(vbCrLf, "") & "','" & OMA.GetETYP.Replace(vbCrLf, "") & "','" & OMA.GetSWIDTH.Replace(vbCrLf, "") & "','" & OMA.GetSDEPTH.Replace(vbCrLf, "") & "','" & OMA.GetBCOCIN.Replace(vbCrLf, "") & "','" & OMA.GetBCOCUP.Replace(vbCrLf, "") & "','" & OMA.GetDIA.Replace(vbCrLf, "") & "','" & OMA.GetFCSGIN.Replace(vbCrLf, "") & "','" & OMA.GetFCSGUP.Replace(vbCrLf, "") & "','" & OMA.GetFCOCUP.Replace(vbCrLf, "") & "','" & OMA.GetFCOCIN.Replace(vbCrLf, "") & "'"
                        End If


                        sqlstr = "INSERT INTO " & TracerTable & " (" & OMAFields & ")" & _
                                 "VALUES('" & receta & "',0,'DBL=" & RxInfo.Puente / 100 & "','" & Now & "',0, " & _
                                 "'" & RadiosStrR & "','" & RadiosStrL & "','" & EyeSide & "'," & _
                                 OMAValues & ")"
                        ExecCommand = True
                End Select
            End If
            'Throw New Exception("x")
            If ExecCommand Then
                conn.Transaction(sqlstr)
            End If
            conn.CloseConn()


        Catch ex As Exception
            Throw New Exception(ex.Message)
        Finally
            IsReceivingVirtualRx = False
        End Try
    End Sub
    Private Sub SaveDataToSavedTracesDB()
        Dim radios1, radios2, RadiosUpd, sqlstr As String
        Dim i, Paquete As Integer
        'VARIABLES DE OMA PARA INSERTAR EN LA TABLA
        Dim EyeSide As String = ""
        Dim RadiosR(NbrsTraces()), RadiosL(NbrsTraces()) As Integer
        Dim RadiosStrR, RadiosStrL, OMAFields, OMAValues As String

        Try
            radios1 = ""
            radios2 = ""
            RadiosUpd = ""
            RadiosStrR = ""
            RadiosStrL = ""
            sqlstr = ""
            'If CDbl(txtaro.Text) = 0 And CDbl(txtvert.Text) = 0 And CDbl(txtdiag.Text) = 0 And CDbl(txtpuente.Text) = 0 Then
            '    For i = 0 To NbrsTraces()
            '        radios(i) = 0
            '    Next
            'End If
            'dependiendo del protocolo a usar para guardar los trazos asignamos las variables correspondientes
            If My.Settings.OpticalProtocol = "DVI" Then
                For i = 0 To 511
                    If i = 0 Then
                        radios1 = "Int" & i + 1
                        radios2 = RadiosL(i)
                        If RxInfo.ModificacionRx Then RadiosUpd = "Int" & i + 1 & "=" & RadiosL(i)
                    Else
                        radios1 = radios1 & ",Int" & i + 1
                        radios2 = radios2 & "," & RadiosL(i)
                        RadiosUpd = RadiosUpd & ",Int" & i + 1 & "=" & RadiosL(i)
                    End If
                Next
            End If
            If My.Settings.OpticalProtocol = "OMA" Then

                If RadiosR(0) = RadiosR(1) And RadiosR(1) = RadiosR(2) And RadiosR(0) = 0 Then
                    ' No traemos informaci�n del trazo derecho, por lo cual la obtenemos al hacer reflejo del lado Izquierdo
                    Dim IMirror As Integer
                    IMirror = 199
                    For i = 0 To 199
                        RadiosR(IMirror) = RadiosL(i)
                        IMirror -= 1
                    Next
                    IMirror = 399
                    For i = 200 To 399
                        RadiosR(IMirror) = RadiosL(i)
                        IMirror -= 1
                    Next

                End If
            End If

            'colocamos las etiquetas del ojo izquierdo
            Paquete = 1
            For i = 0 To 399
                If Paquete = 1 Then
                    RadiosStrL = RadiosStrL + "R="
                End If
                If Paquete = 10 Then
                    RadiosStrL = RadiosStrL + CStr(RadiosR(i)) + vbCr
                    Paquete = 1
                Else
                    RadiosStrL = RadiosStrL + CStr(RadiosR(i)) + ","
                    Paquete += 1
                End If
            Next
            'colocamos las etiquetas del ojo derecho
            Paquete = 1
            For i = 0 To 399
                If Paquete = 1 Then
                    RadiosStrR = RadiosStrR + "R="
                End If
                If Paquete = 10 Then
                    RadiosStrR = RadiosStrR + CStr(RadiosR(i)) + vbCr
                    Paquete = 1
                Else
                    RadiosStrR = RadiosStrR + CStr(RadiosR(i)) + ","
                    Paquete += 1
                End If

            Next
            'ACOMPLETAMOS LAS VARIABLES CORRESPONDIENTES PARA ACTUALIZAR OMA TABLE
            Select Case Me.RxInfo.CheckedEyes
                Case Calcot.RxPrintInfo.Eyes.Both
                    EyeSide = "B"
                Case Calcot.RxPrintInfo.Eyes.Left
                    RadiosStrR = ""
                    EyeSide = "L"
                Case Calcot.RxPrintInfo.Eyes.Right
                    RadiosStrL = ""
                    EyeSide = "R"
            End Select

            '**FIN RADIOS1 CONTIENE EL CAMPO A INSERTAT Y RADIOS2 CONTIENE LOS VALORES A INSERTAR
            '**************************************************************************************************
            Dim conn As New SqlDB(My.Settings.LocalServer, My.Settings.DBUser, My.Settings.DBPassword, My.Settings.LocalDBName)
            conn.OpenConn()
            ' If RxInfo.ModificacionRx And Not Form1.IsReceivingVirtualRx Then 'SI LA RECETA SE ESTA MODIFICANDO ACTUALIZAMOS LOS TRAZOS CASO CONTRARIO LOS GRABAMOS

            Dim fuente As Int16 = 0
            fuente = 1
            Select Case My.Settings.OpticalProtocol
                Case "DVI"
                    sqlstr = "INSERT INTO " & My.Settings.SavedDVITracesDB & " (jobnum,fuente,dbl,fecha,status,sizing," & radios1 & ") " & _
                             "VALUES ('" & Framenum & "'," & fuente & "," & RxInfo.Puente & ",'" & Now & "',1,0," & radios2 & ")"

                    'sqlstr = "INSERT INTO " & TracerTable & " (jobnum,fuente,dbl,fecha,status,sizing," & radios1 & ") " & _
                    '         "VALUES ('" & receta & "',0," & RxInfo.Puente & ",'" & Now & "',0,0," & radios2 & ")"
                Case "OMA"
                    OMAFields = "jobnum,fuente,dbl,fecha,status,TrcRight,TrcLeft,EyeSide" & _
                                ",HBOX,VBOX,CIRC,BEVC,FCRV,TRCFMT,ZFMT,TRCFMTR,ZFMTR,TRCFMTL,ZFMTL" & _
                                ",ZTRCRIGHT,ZTRCLEFT"
                    OMAValues = "'','','','','','TRCFMT=1;400;E;B','','TRCFMT=1;400;E;R;F','','TRCFMTL=1;400;E;L;F',''," & _
                                "'',''"
                    sqlstr = "INSERT INTO " & My.Settings.SavedOMATracesDB & " (" & OMAFields & ")" & _
                             "VALUES('" & Framenum & "',0,'DBL=" & RxInfo.Puente & "','" & Now & "',1, " & _
                             "'" & RadiosStrR & "','" & RadiosStrL & "','" & EyeSide & "'," & _
                             OMAValues & ")"
            End Select
            conn.Transaction(sqlstr)
        Catch ex As Exception
            Throw New Exception(ex.Message)
        Finally
            IsReceivingVirtualRx = False
        End Try
    End Sub
    Private Sub PrintRx()
        Dim rx As New Calcot.PrintRx(receta)

        'OJO DERECHO
        rx.BloqueD = MoldeD.Anillo
        rx.CilindroD = LD.Cilindro
        rx.DhDer = Math.Round(FormaD.DH, 0)
        rx.DvDer = Math.Round(FormaD.DV, 0)
        rx.EjeDer = LD.Eje
        rx.EsferaD = LD.Esfera
        rx.GrosorCentroD = String.Format("{0:0.0}", Math.Round(MoldeD.GrosorCentral, 1))
        'varible public para guardar el grosoer en la tabla orderhed campo userdecimal1
        GrosCentralD = MoldeD.GrosorCentral
        rx.MoldeD = String.Format("{0:000}", (MoldeD.M_Base) * 100) & "-" & String.Format("{0:000}", (MoldeD.M_Cruz) * 100)
        rx.OrillaMaxD = String.Format("{0:0.0}", Math.Round(MoldeD.GrosorMaximo, 1))
        rx.OrillaMinD = String.Format("{0:0.0}", Math.Round(MoldeD.GrosorMinimo, 1))
        'rx.PrismaD = Math.Round(MoldeD.Prisma, 2)
        rx.PrismaD = txtprisD.Text
        If BaseR = "" Then BaseR = "0"
        rx.BaseD = String.Format("{0:0.00}", CDbl(BaseR))
        rx.EjePrismaD = MoldeD.EjePrisma
        rx.AdicionR = RxInfo.AdicionR
        rx.PotFD = "D " & txtesfD.Text & ", " & txtcilD.Text
        rx.MolED = "D " & lblmoldeExD.Text
        If MoldeD.CveMensaje = 0 Then
            rx.MensajeD = ""
        Else
            rx.MensajeD = "D " & MoldeD.Mensaje
        End If

        'OJO IZQUIERDO
        rx.BloqueI = MoldeI.Anillo
        rx.CilindroI = LI.Cilindro
        rx.DhIzq = Math.Round(FormaI.DH, 0)
        rx.DvIzq = Math.Round(FormaI.DV, 0)
        rx.EjeIzq = LI.Eje
        rx.EsferaI = LI.Esfera
        rx.GrosorCentroI = String.Format("{0:0.0}", Math.Round(MoldeI.GrosorCentral, 1))
        'varible public para guardar el grosoer en la tabla orderhed campo userdecimal1
        GrosCentralI = MoldeI.GrosorCentral
        rx.MoldeI = String.Format("{0:000}", (MoldeI.M_Base) * 100) & "-" & String.Format("{0:000}", (MoldeI.M_Cruz) * 100)
        rx.OrillaMaxI = String.Format("{0:0.0}", Math.Round(MoldeI.GrosorMaximo, 1))
        rx.OrillaMinI = String.Format("{0:0.0}", Math.Round(MoldeI.GrosorMinimo, 1))
        'rx.PrismaI = Math.Round(MoldeI.Prisma, 2)
        rx.PrismaI = txtprisI.Text
        If BaseL = "" Then BaseL = "0"
        rx.BaseI = String.Format("{0:0.00}", CDbl(BaseL))
        rx.EjePrismaI = MoldeI.EjePrisma
        rx.AdicionL = RxInfo.AdicionL
        rx.PotFI = "I " & txtesfI.Text & ", " & txtcilI.Text
        rx.MolEI = "I " & lblmoldeExI.Text
        If MoldeI.CveMensaje = 0 Then
            rx.MensajeI = ""
        Else
            rx.MensajeI = "I " & MoldeI.Mensaje
        End If

        'VERIFICAMOS SI SOLO FUE SELECCIONADO UNO DE LOS OJOS PARA LIMPIAR LAS VAR. DEL OJO NO SELECCIONADO
        If Not (RxInfo.CheckedEyes = Calcot.RxPrintInfo.Eyes.Both) Then
            If RxInfo.CheckedEyes And Calcot.RxPrintInfo.Eyes.Left Then
                'OJO DERECHO
                rx.BloqueD = ""
                rx.CilindroD = 0
                rx.DhDer = 0
                rx.DvDer = 0
                rx.EjeDer = 0
                rx.EsferaD = 0
                rx.GrosorCentroD = 0
                rx.MoldeD = ""
                rx.OrillaMaxD = 0
                rx.OrillaMinD = 0
                rx.PrismaD = 0
                rx.BaseD = ""
                rx.EjePrismaD = 0
                rx.AdicionR = 0
                rx.PotFD = ""
                rx.MolED = ""
                rx.MensajeD = ""
            Else
                'OJO IZQUIERDO
                rx.BloqueI = ""
                rx.CilindroI = 0
                rx.DhIzq = 0
                rx.DvIzq = 0
                rx.EjeIzq = 0
                rx.EsferaI = 0
                rx.GrosorCentroI = 0
                rx.MoldeI = ""
                rx.OrillaMaxI = 0
                rx.OrillaMinI = 0
                rx.PrismaI = 0
                rx.BaseI = ""
                rx.EjePrismaI = 0
                rx.AdicionL = 0
                rx.PotFI = ""
                rx.MolEI = ""
                rx.MensajeI = ""
            End If
        End If

        rx.CajaA = String.Format("{0:00.0}", Math.Round(FormaD.Aro, 1))
        rx.CajaB = String.Format("{0:00.0}", Math.Round(FormaD.Vertical, 1))
        rx.CajaDBL = String.Format("{0:00.0}", CDbl(puente))
        rx.CajaED = String.Format("{0:00.0}", Math.Round(FormaD.Diagonal, 1))
        rx.TinteColor = RxInfo.TinteColor
        rx.TinteNum = RxInfo.TinteNum
        rx.Gradiente = RxInfo.Gradiente
        rx.Dise�o = RxInfo.Dise�o
        rx.Material = RxInfo.Material
        If UCase(RxInfo.Material) = "TRIVEX" Then
            rx.MatTX = "TRIVEX"
        Else
            rx.MatTX = ""
        End If
        rx.Biselado = RxInfo.Biselado
        rx.Cliente = RxInfo.Cliente
        rx.DipceDiple = RxInfo.DipceDiple
        rx.Armazon = RxInfo.Armazon
        'verifiocamos si el armazon es ranurado o perforad para imprimirlo en junto con la etq de biselado
        If RxInfo.Biselado <> "" Then
            Select Case UCase(RxInfo.Armazon)
                Case "PERFORADO"
                    rx.Biselado = "PERFORADO"
                Case "RANURADO"
                    rx.Biselado = "RANURADO"
            End Select
        End If
        Dim Salida As Date
        If CStr(Me.FechaInicial) = "" Then
            Salida = CDate(GetFechaHoraFinal(Now))
            rx.HoraSalida = Salida.ToShortTimeString
            rx.FechaSalida = FormateaFecha(Salida.ToShortDateString) 'Salida.ToShortDateString
            rx.FechaEntrada = FormateaFecha(Now.ToShortDateString)
        Else 'ya existe una fecha de salida
            'si es el recibo de un AR en el laboratorio original la fecha inicial es date05 que es la fecha
            'en la que se recibio el AR en el lab local, por eso es nuestra fecha inicial para terminar la rx que ve de 1 a 2 hrs
            'esta fecha fue asignada cuando en el form1 en la opcion fillrx cuando se modifica una receta
            Salida = CDate(GetFechaHoraFinal(Me.FechaInicial))
            rx.HoraSalida = FormateaHora(Salida.ToShortTimeString)
            rx.FechaSalida = FormateaFecha(Salida.ToShortDateString)
            rx.FechaEntrada = FormateaFecha(Me.FechaEntrada)
        End If
        rx.AntiReflajante = RxInfo.AR
        rx.AltOblea = Math.Round(FormaD.Altura, 0)
        rx.Abrillantado = RxInfo.Abrillantado
        rx.Rx = receta
        rx.RXIDEN = RxInfo.Rxident
        'GENERACION DEL CONSECUTIVO (TRABAJO + LOTE) EJ. A01-01 DE LA RECETA
        rx.Lote = Genera_Lote()
        'rx.PrintPreview()
        Try

            'COLOCAMOS CADENA VACIA EN LAS VARIABLES CON CERO PARA NO IMPRIMIRLAS EN EL REPORTER
            If CSng(rx.EsferaD) = 0 Then rx.EsferaD = ""
            If CSng(rx.EsferaI) = 0 Then rx.EsferaI = ""
            If CSng(rx.CilindroD) = 0 Then rx.CilindroD = ""
            If CSng(rx.CilindroI) = 0 Then rx.CilindroI = ""
            If CInt(rx.EjeDer) = 0 Then rx.EjeDer = ""
            If CInt(rx.EjeIzq) = 0 Then rx.EjeIzq = ""
            If CInt(rx.AdicionR) = 0 Then rx.AdicionR = ""
            If CInt(rx.AdicionL) = 0 Then rx.AdicionL = ""
            If rx.MoldeD = "000-000" Then rx.MoldeD = ""
            If rx.MoldeI = "000-000" Then rx.MoldeI = ""
            If rx.BaseD = "0.00" Then rx.BaseD = ""
            If rx.BaseI = "0.00" Then rx.BaseI = ""
            If CInt(rx.EjePrismaD) = 0 And CSng(rx.PrismaD) = 0 Then rx.EjePrismaD = ""
            If CInt(rx.EjePrismaI) = 0 And CSng(rx.PrismaI) = 0 Then rx.EjePrismaI = ""
            If CSng(rx.PrismaD) = 0 Then rx.PrismaD = ""
            If CSng(rx.PrismaI) = 0 Then rx.PrismaI = ""
            If rx.BloqueD = "0" Then rx.BloqueD = ""
            If rx.BloqueI = "0" Then rx.BloqueI = ""
            If CInt(rx.AltOblea) = 0 Then rx.AltOblea = ""

            If CInt(rx.DhDer) = 0 And Not Form1.CheckREye.Checked Then rx.DhDer = ""
            If CInt(rx.DhIzq) = 0 And Not Form1.CheckLEye.Checked Then rx.DhIzq = ""
            If CInt(rx.DvDer) = 0 And Not Form1.CheckREye.Checked Then rx.DvDer = ""
            If CInt(rx.DvIzq) = 0 And Not Form1.CheckLEye.Checked Then rx.DvIzq = ""
            If CSng(rx.GrosorCentroD) = 0 Then rx.GrosorCentroD = ""
            If CSng(rx.GrosorCentroI) = 0 Then rx.GrosorCentroI = ""
            If CSng(rx.OrillaMinD) = 0 Then rx.OrillaMinD = ""
            If CSng(rx.OrillaMinI) = 0 Then rx.OrillaMinI = ""
            If CSng(rx.OrillaMaxD) = 0 Then rx.OrillaMaxD = ""
            If CSng(rx.OrillaMaxI) = 0 Then rx.OrillaMaxI = ""
            If CInt(rx.CajaA) = 0 Then rx.CajaA = ""
            If CInt(rx.CajaB) = 0 Then rx.CajaB = ""
            If CInt(rx.CajaDBL) = 0 Then rx.CajaDBL = ""
            If CInt(rx.CajaED) = 0 Then rx.CajaED = ""
            If rx.PotFD = "0, 0" Then rx.PotFD = ""
            If rx.PotFI = "0, 0" Then rx.PotFI = ""
            If rx.MolED = "0 - 0" Then rx.MolED = ""
            If rx.MolEI = "0 - 0" Then rx.MolEI = ""
        Catch ex As Exception

        End Try

        'MANDAMOS A UN HILO LA IMPRESION DE LA RECETA
        Dim myt As Threading.Thread
        myt = New Threading.Thread(New Threading.ThreadStart(AddressOf rx.PrintRx))
        myt.Start()
        'rx.PrintRx()
        'rx = Nothing
    End Sub
    Private Function FormateaHora(ByVal hora As Date) As String
        Dim horafinal As String = ""
        Dim ampm, hr, min As String

        Try
            If hora.Hour >= 12 Then
                ampm = "PM"
                hr = hora.Hour - 12
                If hr = 0 Then hr = 12
            Else
                ampm = "AM"
                hr = hora.Hour
            End If
            If CStr(hora.Minute).Length = 1 Then
                min = "0" + CStr(hora.Minute)
            Else
                min = hora.Minute
            End If
            horafinal = CStr(hr) + ":" + CStr(min) + " " + ampm
            Return horafinal
        Catch ex As Exception
            Return horafinal
        End Try

    End Function
    Private Function FormateaFecha(ByVal fecha As Date) As String
        Dim fechacorta As String = ""
        Select Case fecha.Month
            Case 1
                fechacorta = "Ene"
            Case 2
                fechacorta = "Feb"
            Case 3
                fechacorta = "Mar"
            Case 4
                fechacorta = "Abr"
            Case 5
                fechacorta = "May"
            Case 6
                fechacorta = "Jun"
            Case 7
                fechacorta = "Jul"
            Case 8
                fechacorta = "Ago"
            Case 9
                fechacorta = "Sep"
            Case 10
                fechacorta = "Oct"
            Case 11
                fechacorta = "Nov"
            Case 12
                fechacorta = "Dic"
        End Select
        fechacorta = fechacorta & "/" & fecha.Day
        Return fechacorta
    End Function
    Private Function GetFechaHoraFinal(ByVal sdte As Date) As String
        Dim TiempoDeRuta, TAR, TSL, TFL As Integer
        Dim finaldate As String = ""
        Dim day As Integer
        Dim startdate, sdate, initdate, nextdate, starthour As String

        Dim MyCnn As SqlClient.SqlConnection
        Dim Cmd As SqlClient.SqlCommand
        Dim Rdr As SqlClient.SqlDataReader
        Dim str As String

        '**************************************************************************************************
        '**************************** OBTENEMOS EL TIEMPO DE RUTA DE LA RECETA ****************************
        If RxInfo.AR <> "" Then
            If Form1.IsReceivingVirtualRx Then
                TAR = 6 'tiempo para cuando tiene AR
                TSL = 3 'todo el lente tiene 3 horas en SL (no importa si es Semiterinado o Terminado)
            Else
                'verificamos si la receta de ar es creada y sera procesada localmente por el mismo laboratorio
                'form1.arlab contiene la clave del laboratorio seleccionado a procesar la rx con AR y form1.labid contien la clave del lab local
                If Form1.ARLab = Form1.LabID Then
                    TAR = 6
                    TSL = 3
                Else
                    'VERIFICAR SI SE LE ESTA DANDO ENTRADA AL AR EN EL LAB LOCAL QUE SOLICITO AR O APENAS SE ESTA
                    'IMPRIMIENDO POR PRIMERA VEZ LA HOJA DE CALCULO DE AR
                    TAR = 0
                    TSL = 0
                End If
            End If
        Else
            TSL = 3 'si no tiene ar entonces le damos 3 horaS en SL a la receta
        End If

        If UCase(RxInfo.Armazon) = "PERFORADO" Or UCase(RxInfo.Armazon) = "RANURADO" Then
            TFL = 2 'tiempo para cuando el bisel es perforado o ranurado
        Else
            TFL = 1 'tiempo para cuando el bisel es metal o plastico
        End If
        'SI SE ESTA RECIBIENDO EL VIRTUAL ENTONCES NO TIENE TIEMPO EN FL
        If Form1.IsReceivingVirtualRx Then
            TFL = 0
        Else 'si se esta modificando en el labn remoto tampoco tiene FL
            If Form1.IsVirtualRx Then
                If Form1.IsModifying And Not Form1.IsLocalRx Then TFL = 0
            End If
        End If
        'sumanos el tiempo total de ruta de la receta
        TiempoDeRuta = TAR + TSL + TFL
        '**************************************************************************************************
        '**************************************************************************************************
        startdate = sdte
        initdate = startdate
        sdate = sdte.ToShortDateString
        day = sdte.DayOfWeek

        nextdate = sdte.ToShortDateString
        starthour = sdte.Hour

        MyCnn = New SqlClient.SqlConnection("user ID=" & My.Settings.DBUser & ";password=" & My.Settings.DBPassword & ";database=" & My.Settings.LocalDBName & ";server=" & My.Settings.LocalServer & ";Connect Timeout=10")
        MyCnn.Open()
        Dim iter As Integer = 0
        While TiempoDeRuta >= 0 And iter < 10
            iter += 1
            str = "SELECT LaborCodes.InitHour,LaborCodes.EndHour,LaborCodes.Hours " & _
                  "FROM Calendar INNER JOIN " & _
                  "LaborCodes ON Calendar.LaborCode = LaborCodes.LaborCode " & _
                  "WHERE Calendar.[Date] = '" & nextdate & "' AND LaborCodes.Plant = (SELECT  cl_lab from TblLaboratorios where plant = '" & Read_Registry("Plant") & "') "
            Cmd = New SqlClient.SqlCommand(str, MyCnn)
            Rdr = Cmd.ExecuteReader()
            If Rdr.HasRows Then
                Rdr.Read()
                If Rdr("inithour") = 0 Then
                    starthour = 0
                ElseIf starthour = 0 Then
                    starthour = Rdr("inithour")
                End If
                TiempoDeRuta = TiempoDeRuta - (Rdr("endhour") - starthour)
                starthour = Rdr("inithour")
                If TiempoDeRuta <= 0 Then
                    finaldate = nextdate + " " + CStr(Rdr("endhour") + TiempoDeRuta) + ":" + CStr(DatePart(DateInterval.Minute, CDate(initdate)))

                End If
            End If
            nextdate = DateAdd(DateInterval.Day, 1, CDate(nextdate))
            Rdr.Close()
            Cmd.Dispose()
        End While

        MyCnn.Close()

        Return CStr(finaldate)
    End Function
    Function Read_Registry(ByVal var As String) As String
        Dim key As RegistryKey
        Dim Result As String = ""
        key = Registry.LocalMachine.OpenSubKey("SOFTWARE").OpenSubKey("AUGEN")
        Select Case var
            Case "Plant"
                'Result = CStr(key.GetValue("Plant"))
                Result = My.Settings.Plant
            Case "TracerDataServer"
                'Result = CStr(key.GetValue("TracerDataServer"))
                Result = My.Settings.LocalServer
            Case "ServerAdd"
                'Result = CStr(key.GetValue("MainServer"))
                Result = My.Settings.VantageServer
            Case "DSN"
                'Result = CStr(key.GetValue("DSNGuiador"))
                Result = My.Settings.DSNGuiador
        End Select
        Return Result
    End Function

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        lbldhd.Visible = True
        lbldvd.Visible = True
    End Sub
    Public Function Genera_Lote() As String
        Dim LoteGenerado As String = ""
        Dim letra, grupo, trabajo As Integer
        Dim DSet As DataSet
        Dim sqlstr As String
        Dim Hoy, UFecha As Date

        Dim conn As New SqlDB(My.Settings.LocalServer, My.Settings.DBUser, My.Settings.DBPassword, My.Settings.LocalDBName)
        conn.OpenConn()

        sqlstr = "select letra,grupo,trabajo,ufecha,getdate() as FechaActual from tbltrabajospordia"
        'El lote empieza desde la letra A (ascii 65) por dia, checamos si ya cambiamos de dia
        DSet = New DataSet
        DSet = conn.SQLDS(sqlstr, "t1")
        conn.CloseConn()
        letra = DSet.Tables("t1").Rows(0).Item(0)
        grupo = DSet.Tables("t1").Rows(0).Item(1)
        trabajo = DSet.Tables("t1").Rows(0).Item(2)
        UFecha = DSet.Tables("t1").Rows(0).Item(3)
        Hoy = DSet.Tables("t1").Rows(0).Item(4)

        If (DateDiff(DateInterval.Day, UFecha, Hoy)) > 0 Then
            'ACTUALIZAMOS LAS VARIABLES PARA QUE EL LOTE SEA: A00-01
            conn.OpenConn()
            conn.Transaction("UPDATE TblTrabajosPorDia set letra = 65,grupo=0,trabajo=0,ufecha='" & Hoy & "' ")
            conn.CloseConn()
            letra = 65 'corresponde a la letra A en ascii
            grupo = 0
            trabajo = 0
        End If
        'CREAMOS EL LOTE DEL DIA
        conn.OpenConn()
        trabajo = trabajo + 1
        If trabajo >= 11 Then
            trabajo = 1
            grupo = grupo + 1
            If grupo >= 11 Then
                grupo = 0
                letra = letra + 1
                LoteGenerado = Chr(letra) & String.Format("{0:00}", grupo) & "-" & String.Format("{0:00}", trabajo)
                conn.Transaction("UPDATE TblTrabajosPorDia set letra = " & letra & ",grupo= " & grupo & ",trabajo=" & trabajo & " ")
            Else
                'continuamos con el grupo siguiente ej. A02-01
                LoteGenerado = Chr(letra) & String.Format("{0:00}", grupo) & "-" & String.Format("{0:00}", trabajo)
                conn.Transaction("UPDATE TblTrabajosPorDia set letra = " & letra & ",grupo= " & grupo & ",trabajo=" & trabajo & " ")
            End If
        Else
            'continuamos con el trabajo siguiente ej. A01-02
            LoteGenerado = Chr(letra) & String.Format("{0:00}", grupo) & "-" & String.Format("{0:00}", trabajo)
            conn.Transaction("UPDATE TblTrabajosPorDia set letra = " & letra & ",grupo= " & grupo & ",trabajo=" & trabajo & " ")
        End If
        conn.CloseConn()
        conn = Nothing
        Return LoteGenerado
    End Function
    Private Sub Update_RxVirtual()
        Dim OdbcCon As New Odbc.OdbcConnection
        Dim OdbcCmd As Odbc.OdbcCommand
        Dim OdbcCmd2 As Odbc.OdbcCommand
        Dim SQLCmd As String

        Try
            OdbcCon.ConnectionString = "DSN=" & Read_Registry("DSN") & ";UID=calcot;"
            OdbcCon.Open()

            'si la receta contiene armazon jalamos la receta de la tabla geo_arm

            SQLCmd = " UPDATE geo_arm SET pend=22 " & _
                     " WHERE rx=" & RxInfo.RxNumFromVirtual & " "
            OdbcCmd = New Odbc.OdbcCommand(SQLCmd, OdbcCon)
            OdbcCmd.ExecuteNonQuery()
            OdbcCmd.Dispose()
            OdbcCmd = Nothing

            SQLCmd = " UPDATE recetas SET pend = 22 " & _
                     " WHERE rx = " & RxInfo.RxNumFromVirtual & " "
            OdbcCmd2 = New Odbc.OdbcCommand(SQLCmd, OdbcCon)
            OdbcCmd2.ExecuteNonQuery()
            OdbcCmd2.Dispose()
            OdbcCmd2 = Nothing

            OdbcCon.Close()
            OdbcCon.Dispose()
            OdbcCon = Nothing

        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try

    End Sub

End Class