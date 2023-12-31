'<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<><>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>
'<<< Clase que imprime la receta para ser procesada en el laboratorio >>>>>
'<<< esta clase tambien guarda los datos en la tabla MFGSYS80.JOBHEAD de vantage >>>>
'<<< esta impresion cuenta con todos los datos de los calculos opticos >>>>
Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.Shared

Public Class PrintLabRx
    'variables privadas de la clase
    Dim rxnum, adicl, adicR, bisel, tinnum, grad, ejeprisd, ejeprisi, arm, cte, mat, dis, modD, modI, BasD, BasI, PrisD, PrisI, BloD, BloI, Dip, Tin, AR, MolD_HD, MolI_HD, AniloD_HD, AniloI_HD, BcNameDer, BcNameIzq As String
    'Dim MCentroD, MCentroI, BCnameD, BCnameI As String
    Dim Alt, A, B, DBL, ED, lotstr, msgD, msgI, horasal, fechasal, fechaent, horaent, mtx, ordernum, ponum, ColorSL, Comments, mono As String
    'variable que guarda el jobnumber de vantage para el cual se imprimira la receta
    Dim VntgJobNumber, med, mei, pfd, pfi, abrilla, CilD, CilI, EjeD, EjeI, RXIDE, DhD, DhI, DvD, DvI, GCD, GCI, OMinD, OMinI, OMaxD, OMaxI, EsfD, EsfI As String
    Dim RxLocal As String
    Dim TXT_SpecialLabel As String = ""
    Dim IsMicro As Boolean
    Dim MED_HD, MEI_HD As String
    Dim DisenoEsp As String
    Dim D_DPR_X, D_DPR_Y, D_DPR_Sph, D_DPR_Cyl, D_DPR_Axis, D_NPR_X, D_NPR_Y, D_NPR_Sph, D_NPR_Cyl, D_NPR_Axis As String
    Dim I_DPR_X, I_DPR_Y, I_DPR_Sph, I_DPR_Cyl, I_DPR_Axis, I_NPR_X, I_NPR_Y, I_NPR_Sph, I_NPR_Cyl, I_NPR_Axis As String

    ' variables para el dinero  
    ' Pedro Far�as L.  Dic 5 2012
    Dim PU As Double
    Dim IVApercent As Integer
    Dim Concepto As String

    Public Property DineroIVApercent() As Integer
        Get
            Return IVApercent

        End Get
        Set(ByVal value As Integer)
            IVApercent = value
        End Set
    End Property

    Public Property DineroPU() As Double
        Get
            Return PU

        End Get
        Set(ByVal value As Double)
            PU = value
        End Set
    End Property


    Public Property DineroConcepto() As String
        Get
            Return Concepto

        End Get
        Set(ByVal value As String)
            Concepto = value
        End Set
    End Property

    'listado de propiedades que seran impresas en la receta
    Public Property AdicionL() As String
        Get
            Return adicl
        End Get
        Set(ByVal value As String)
            adicl = value
        End Set
    End Property
    Public Property DisenoEspecial() As String
        Get
            Return DisenoEsp
        End Get
        Set(ByVal value As String)
            DisenoEsp = value
        End Set
    End Property

    Public Property MoldeED_HD() As String
        Get
            Return MED_HD
        End Get
        Set(ByVal value As String)
            MED_HD = value
        End Set
    End Property
    Public Property MoldeEI_HD() As String
        Get
            Return MEI_HD
        End Get
        Set(ByVal value As String)
            MEI_HD = value
        End Set
    End Property

    Public Property SpecialLabel() As String
        Get
            Return TXT_SpecialLabel
        End Get
        Set(ByVal value As String)
            TXT_SpecialLabel = value
        End Set
    End Property
    Public Property AdicionR() As String
        Get
            Return adicR
        End Get
        Set(ByVal value As String)
            adicR = value
        End Set
    End Property
    Public Property Biselado() As String
        Get
            Return bisel
        End Get
        Set(ByVal value As String)
            bisel = value
        End Set
    End Property
    Public Property Rx() As String
        Get
            If rxnum Is Nothing Then rxnum = ""
            Return rxnum
        End Get
        Set(ByVal value As String)
            rxnum = value
        End Set
    End Property
    Public Property Cliente() As String
        Get
            If cte Is Nothing Then cte = ""
            Return cte
        End Get
        Set(ByVal value As String)
            cte = value
        End Set
    End Property
    Public Property Armazon() As String
        Get
            Return arm
        End Get
        Set(ByVal value As String)
            arm = value
        End Set
    End Property
    Public Property Material() As String
        Get
            Return mat
        End Get
        Set(ByVal value As String)
            mat = value
        End Set
    End Property
    Public Property Dise�o() As String
        Get
            Return dis
        End Get
        Set(ByVal value As String)
            dis = value
        End Set
    End Property
    Public Property EsferaD() As String
        Get
            Return EsfD
        End Get
        Set(ByVal value As String)
            EsfD = value
        End Set
    End Property
    Public Property EsferaI() As String
        Get
            Return EsfI
        End Get
        Set(ByVal value As String)
            EsfI = value
        End Set
    End Property
    Public Property CilindroD() As String
        Get
            Return CilD
        End Get
        Set(ByVal value As String)
            CilD = value
        End Set
    End Property
    Public Property CilindroI() As String
        Get
            Return CilI
        End Get
        Set(ByVal value As String)
            CilI = value
        End Set
    End Property
    Public Property EjeDer() As String
        Get
            Return EjeD
        End Get
        Set(ByVal value As String)
            EjeD = value
        End Set
    End Property
    Public Property EjeIzq() As String
        Get
            Return EjeI
        End Get
        Set(ByVal value As String)
            EjeI = value
        End Set
    End Property
    Public Property MoldeD() As String
        Get
            Return modD
        End Get
        Set(ByVal value As String)
            modD = value
        End Set
    End Property
    Public Property MoldeI() As String
        Get
            Return modI
        End Get
        Set(ByVal value As String)
            modI = value
        End Set
    End Property
    Public Property BaseD() As String
        Get
            Return BasD
        End Get
        Set(ByVal value As String)
            BasD = value
        End Set
    End Property
    Public Property BaseI() As String
        Get
            Return BasI
        End Get
        Set(ByVal value As String)
            BasI = value
        End Set
    End Property
    Public Property EjePrismaD() As String
        Get
            Return ejeprisd
        End Get
        Set(ByVal value As String)
            ejeprisd = value
        End Set
    End Property
    Public Property EjePrismaI() As String
        Get
            Return ejeprisi
        End Get
        Set(ByVal value As String)
            ejeprisi = value
        End Set
    End Property
    Public Property PrismaD() As String
        Get
            Return PrisD
        End Get
        Set(ByVal value As String)
            PrisD = value
        End Set
    End Property
    Public Property PrismaI() As String
        Get
            Return PrisI
        End Get
        Set(ByVal value As String)
            PrisI = value
        End Set
    End Property
    Public Property BloqueD() As String
        Get
            Return BloD
        End Get
        Set(ByVal value As String)
            BloD = value
        End Set
    End Property
    Public Property BloqueI() As String
        Get
            Return BloI
        End Get
        Set(ByVal value As String)
            BloI = value
        End Set
    End Property
    Public Property DipceDiple() As String
        Get
            Return Dip
        End Get
        Set(ByVal value As String)
            Dip = value
        End Set
    End Property
    Public Property AltOblea() As String
        Get
            Return Alt
        End Get
        Set(ByVal value As String)
            Alt = value
        End Set
    End Property
    Public Property DhDer() As String
        Get
            Return DhD
        End Get
        Set(ByVal value As String)
            DhD = value
        End Set
    End Property
    Public Property DhIzq() As String
        Get
            Return DhI
        End Get
        Set(ByVal value As String)
            DhI = value
        End Set
    End Property
    Public Property DvDer() As String
        Get
            Return DvD
        End Get
        Set(ByVal value As String)
            DvD = value
        End Set
    End Property
    Public Property DvIzq() As String
        Get
            Return DvI
        End Get
        Set(ByVal value As String)
            DvI = value
        End Set
    End Property
    Public Property GrosorCentroD() As String
        Get
            Return GCD
        End Get
        Set(ByVal value As String)
            GCD = value
        End Set
    End Property
    Public Property GrosorCentroI() As String
        Get
            Return GCI
        End Get
        Set(ByVal value As String)
            GCI = value
        End Set
    End Property
    Public Property OrillaMinD() As String
        Get
            Return OMinD
        End Get
        Set(ByVal value As String)
            OMinD = value
        End Set
    End Property
    Public Property OrillaMaxD() As String
        Get
            Return OMaxD
        End Get
        Set(ByVal value As String)
            OMaxD = value
        End Set
    End Property
    Public Property OrillaMinI() As String
        Get
            Return OMinI
        End Get
        Set(ByVal value As String)
            OMinI = value
        End Set
    End Property
    Public Property OrillaMaxI() As String
        Get
            Return OMaxI
        End Get
        Set(ByVal value As String)
            OMaxI = value
        End Set
    End Property
    Public Property CajaA() As String
        Get
            Return A
        End Get
        Set(ByVal value As String)
            A = value
        End Set
    End Property
    Public Property CajaB() As String
        Get
            Return B
        End Get
        Set(ByVal value As String)
            B = value
        End Set
    End Property
    Public Property CajaDBL() As String
        Get
            Return DBL
        End Get
        Set(ByVal value As String)
            DBL = value
        End Set
    End Property
    Public Property CajaED() As String
        Get
            Return ED
        End Get
        Set(ByVal value As String)
            ED = value
        End Set
    End Property
    Public Property TinteColor() As String
        Get
            Return Tin
        End Get
        Set(ByVal value As String)
            Tin = value
        End Set
    End Property
    Public Property TinteNum() As String
        Get
            Return tinnum
        End Get
        Set(ByVal value As String)
            tinnum = value
        End Set
    End Property
    Public Property Gradiente() As String
        Get
            Return grad
        End Get
        Set(ByVal value As String)
            grad = value
        End Set
    End Property
    Public Property AntiReflajante() As String
        Get
            Return AR
        End Get
        Set(ByVal value As String)
            AR = value
        End Set
    End Property
    Public Property PotFD() As String
        Get
            Return pfd
        End Get
        Set(ByVal value As String)
            pfd = value
        End Set
    End Property
    Public Property PotFI() As String
        Get
            Return pfi
        End Get
        Set(ByVal value As String)
            pfi = value
        End Set
    End Property
    Public Property MolED() As String
        Get
            Return med
        End Get
        Set(ByVal value As String)
            med = value
        End Set
    End Property
    Public Property MolEI() As String
        Get
            Return mei
        End Get
        Set(ByVal value As String)
            mei = value
        End Set
    End Property
    Public Property Abrillantado() As String
        Get
            Return abrilla
        End Get
        Set(ByVal value As String)
            abrilla = value
        End Set
    End Property
    Public Property Lote() As String
        Get
            Return lotstr
        End Get
        Set(ByVal value As String)
            lotstr = value
        End Set
    End Property
    Public Property MensajeD() As String
        Get
            Return msgD
        End Get
        Set(ByVal value As String)
            msgD = value
        End Set
    End Property
    Public Property MensajeI() As String
        Get
            Return msgI
        End Get
        Set(ByVal value As String)
            msgI = value
        End Set
    End Property
    Public Property RXIDEN() As String
        Get
            Return RXIDE
        End Get
        Set(ByVal value As String)
            RXIDE = value
        End Set
    End Property
    Public Property HoraSalida() As String
        Get
            Return horasal
        End Get
        Set(ByVal value As String)
            horasal = value
        End Set
    End Property
    Public Property FechaSalida() As String
        Get
            Return fechasal
        End Get
        Set(ByVal value As String)
            fechasal = value
        End Set
    End Property
    Public Property FechaEntrada() As String
        Get
            Return fechaent
        End Get
        Set(ByVal value As String)
            fechaent = value
        End Set
    End Property
    Public Property HoraEntrada() As String
        Get
            Return horaent
        End Get
        Set(ByVal value As String)
            horaent = value
        End Set
    End Property
    Public Property MatTX() As String
        Get
            Return mtx
        End Get
        Set(ByVal value As String)
            mtx = value
        End Set
    End Property
    Public Property Vantage() As String
        Get
            Return ordernum
        End Get
        Set(ByVal value As String)
            ordernum = value
        End Set
    End Property
    Public Property PONumber() As String
        Get
            Return ponum

        End Get
        Set(ByVal value As String)
            ponum = value
        End Set
    End Property
    Public Property ColorSOLEO() As String
        Get
            Return ColorSL
        End Get
        Set(ByVal value As String)
            ColorSL = value
        End Set
    End Property
    Public Property Comentarios() As String
        Get
            Return Comments
        End Get
        Set(ByVal value As String)
            Comments = value
        End Set
    End Property
    Public Property monoLR() As String
        Get
            Return mono
        End Get
        Set(ByVal value As String)
            mono = value
        End Set
    End Property
    'para nuevos generadores High Definition feb/2007
    Public Property MoldeD_HD() As String
        Get
            Return MolD_HD
        End Get
        Set(ByVal value As String)
            MolD_HD = value
        End Set
    End Property
    Public Property MoldeI_HD() As String
        Get
            Return MolI_HD
        End Get
        Set(ByVal value As String)
            MolI_HD = value
        End Set
    End Property
    Public Property AnilloD_HD() As String
        Get
            Return AniloD_HD
        End Get
        Set(ByVal value As String)
            'PFL Oct 22 2012
            'Quitamos la palabra RING -- ya sabemos que es el n�mero del anillo
            If value.ToUpper.IndexOf("RING") > -1 Then
                AniloD_HD = value.Remove(value.ToUpper.IndexOf("RING"), 4).Trim()
            Else
                AniloD_HD = value
            End If
            'AniloD_HD = value
        End Set
    End Property
    Public Property AnilloI_HD() As String
        Get
            Return AniloI_HD
        End Get
        Set(ByVal value As String)
            'PFL Oct 22 2012
            'Quitamos la palabra RING -- ya sabemos que es el n�mero del anillo
            If value.ToUpper.IndexOf("RING") > -1 Then
                AniloI_HD = value.Remove(value.ToUpper.IndexOf("RING"), 4).Trim()
            Else
                AniloI_HD = value
            End If
            'AniloI_HD = value
        End Set
    End Property
    Public Property RxNumLocal() As String
        Get
            Return RxLocal
        End Get
        Set(ByVal value As String)
            RxLocal = value
        End Set
    End Property
    Public Property BcNameD() As String
        Get
            Return BcNameDer
        End Get
        Set(ByVal value As String)
            BcNameDer = value
        End Set
    End Property
    Public Property BcNameI() As String
        Get
            Return BcNameIzq
        End Get
        Set(ByVal value As String)
            BcNameIzq = value
        End Set
    End Property

    Public Property DDPR_X() As String
        Get
            Return D_DPR_X
        End Get
        Set(ByVal value As String)
            D_DPR_X = value
        End Set
    End Property
    Public Property DDPR_Y() As String
        Get
            Return D_DPR_Y
        End Get
        Set(ByVal value As String)
            D_DPR_Y = value
        End Set
    End Property
    Public Property DDPR_SPH() As String
        Get
            Return D_DPR_Sph
        End Get
        Set(ByVal value As String)
            D_DPR_Sph = value
        End Set
    End Property
    Public Property DDPR_CYL() As String
        Get
            Return D_DPR_Cyl
        End Get
        Set(ByVal value As String)
            D_DPR_Cyl = value
        End Set
    End Property
    Public Property DDPR_AXIS() As String
        Get
            Return D_DPR_Axis
        End Get
        Set(ByVal value As String)
            D_DPR_Axis = value
        End Set
    End Property
    Public Property IDPR_X() As String
        Get
            Return I_DPR_X
        End Get
        Set(ByVal value As String)
            I_DPR_X = value
        End Set
    End Property
    Public Property IDPR_Y() As String
        Get
            Return I_DPR_Y
        End Get
        Set(ByVal value As String)
            I_DPR_Y = value
        End Set
    End Property
    Public Property IDPR_SPH() As String
        Get
            Return I_DPR_Sph
        End Get
        Set(ByVal value As String)
            I_DPR_Sph = value
        End Set
    End Property
    Public Property IDPR_CYL() As String
        Get
            Return I_DPR_Cyl
        End Get
        Set(ByVal value As String)
            I_DPR_Cyl = value
        End Set
    End Property
    Public Property IDPR_AXIS() As String
        Get
            Return I_DPR_Axis
        End Get
        Set(ByVal value As String)
            I_DPR_Axis = value
        End Set
    End Property

    Public Property DNPR_X() As String
        Get
            Return D_NPR_X
        End Get
        Set(ByVal value As String)
            D_NPR_X = value
        End Set
    End Property
    Public Property DNPR_Y() As String
        Get
            Return D_NPR_Y
        End Get
        Set(ByVal value As String)
            D_NPR_Y = value
        End Set
    End Property
    Public Property DNPR_SPH() As String
        Get
            Return D_NPR_Sph
        End Get
        Set(ByVal value As String)
            D_NPR_Sph = value
        End Set
    End Property
    Public Property DNPR_CYL() As String
        Get
            Return D_NPR_Cyl
        End Get
        Set(ByVal value As String)
            D_NPR_Cyl = value
        End Set
    End Property
    Public Property DNPR_AXIS() As String
        Get
            Return D_NPR_Axis
        End Get
        Set(ByVal value As String)
            D_NPR_Axis = value
        End Set
    End Property
    Public Property INPR_X() As String
        Get
            Return I_NPR_X
        End Get
        Set(ByVal value As String)
            I_NPR_X = value
        End Set
    End Property
    Public Property INPR_Y() As String
        Get
            Return I_NPR_Y
        End Get
        Set(ByVal value As String)
            I_NPR_Y = value
        End Set
    End Property
    Public Property INPR_SPH() As String
        Get
            Return I_NPR_Sph
        End Get
        Set(ByVal value As String)
            I_NPR_Sph = value
        End Set
    End Property
    Public Property INPR_CYL() As String
        Get
            Return I_NPR_Cyl
        End Get
        Set(ByVal value As String)
            I_NPR_Cyl = value
        End Set
    End Property
    Public Property INPR_AXIS() As String
        Get
            Return I_NPR_Axis
        End Get
        Set(ByVal value As String)
            I_NPR_Axis = value
        End Set
    End Property

    ' Pedro Far�as Oct 19 2012 -- Mejora comentada
    'Procedimiento que imprime la receta usando los reportes como recursos incrustados. Menos flexible. M�s confiable
    'Public Sub PrintRx()
    '    Dim reporteLab As New NewRecetaLab
    '    Dim reporteVB As New NewRecetaVB

    '    Try

    '        CreateXML()
    '        If My.Settings.ReporteReceta1.ToUpper = reporteLab.ResourceName.ToUpper Then
    '            reporteLab.SetDataSource(UpdateXML)
    '            reporteLab.PrintToPrinter(1, False, 1, 1)
    '        ElseIf My.Settings.ReporteReceta1.ToUpper = reporteVB.ResourceName.ToUpper Then
    '            reporteVB.SetDataSource(UpdateXML)
    '            reporteVB.PrintToPrinter(1, False, 1, 1)
    '        Else
    '            MsgBox("Error en ajuste de configuraci�n [ReporteReceta1]" + vbCrLf + "Debe ser NewRecetaLab o NewRecetaVB", MsgBoxStyle.Exclamation, "Error en ajuste de configuraci�n")

    '        End If
    '    Catch ex As Exception
    '        MsgBox("" & ex.Message)
    '        MsgBox("Ocurri� un error al imprimir la receta. Para continuar debes cerrar la aplicacion y ejecutarla de nuevo." & vbCrLf & "Debes reimprimir la Rx con una modificaci�n con Vantage: " & ordernum & vbCrLf & vbCrLf & ex.Message, MsgBoxStyle.Critical)
    '    End Try
    '    reporteLab.Dispose()
    '    reporteVB.Dispose()
    'End Sub


    'Procedimiento que imprime la receta usando archivo en disco de reporte cystal. M�s flexible pero menos confiable por los accesos a disco
    'pero muy flexible porque permite modificar solo el reporte de crystal sin tener que compilar el proyecto
    Public Sub PrintRx()
        Dim reporte As New ReportDocument
        'Dim dt As New DataSet
        'Modificaci�n 10/15/2012
        'Pedro Far�as
        'Ahora carga el reporte que se indica en el ajuste de configuraci�n ReporteReceta1

        Try
            reporte.Load(My.Application.Info.DirectoryPath & "\" & My.Settings.ReporteReceta1)

            CreateXML()
            ' Old School way to print Rx  -- just a note by PFL
            '           CreateXML()
            '           UpdateXML()
            '           dt.ReadXml(My.Application.Info.DirectoryPath & "\LabRx.xml")

            ' New School PFL Oct 19 2012
            ' -- actualizamos el archivo XML por si queremos reimprimir la receta dando doble click en el campo
            ' textbox del num. de receta
            reporte.SetDataSource(UpdateXML)
            reporte.PrintToPrinter(1, False, 1, 1)

        Catch ex As Exception
            MsgBox(ex.Message)
            MsgBox("Ocurri� un error al imprimir la receta. Para continuar debes cerrar la aplicacion y ejecutarla de nuevo." & vbCrLf & "Debes reimprimir la Rx con una modificaci�n con Vantage: " & ordernum & vbCrLf & vbCrLf & ex.Message, MsgBoxStyle.Critical)
        End Try
        reporte.Dispose()

    End Sub
    'muestra una forma con el preview de la receta
    Public Sub PrintPreview()
        Dim frm As New RxPreview
        'imprimeBloqueCentro()
        CreateXML()
        UpdateXML()
        frm.ShowDialog()
    End Sub
    'PFL Oct 16 2012
    'Imprime el contenido de la �ltima receta guardada en LabRx.xml
    Public Sub PrintPreviewTest()
        Dim frm As New RxPreview
        frm.ShowDialog()
    End Sub
    'procedimiento que reimprime una receta previamente capturada
    Public Sub RePrintRx(ByVal Job As String)
        Dim con As SqlClient.SqlConnection
        Dim cmd As SqlClient.SqlCommand
        Dim Dread As SqlClient.SqlDataReader
        Try
            con = New SqlClient.SqlConnection(My.Settings.MFGSysConnection) '"user ID=sa;password=proliant01;database=mfgtest80;server=AUGENSVR2;Connect Timeout=30")
            cmd = New SqlClient.SqlCommand("SELECT * FROM JobHead WHERE Company = 'AUGEN' AND Plant = 'MfgSys' AND Jobnum = '" & Job & "' ", con)
            con.Open()
            Dread = cmd.ExecuteReader
            If Dread.HasRows Then
                While Dread.Read
                    EsferaD = Dread("number01")
                    EsferaI = Dread("number02")
                    CilD = Dread("number03")
                    CilI = Dread("number04")
                    EjeD = Dread("number05")
                    EjeI = Dread("number06")
                    MoldeD = Dread("shortchar01")
                    MoldeI = Dread("shortchar02")
                    BaseD = Dread("shortchar03")
                    BaseI = Dread("shortchar04")
                    PrismaD = Dread("shortchar05")
                    PrismaI = Dread("shortchar06")
                    BloqueD = Dread("shortchar07")
                    BloqueI = Dread("shortchar08")
2:                  AltOblea = Dread("number07")
                    DipceDiple = Dread("shortchar10")
                    DhD = Dread("number09")
                    DhI = Dread("number10")
                    DvD = Dread("number11")
                    DvI = Dread("number12")
                    GrosorCentroD = Dread("number13")
                    GrosorCentroI = Dread("number14")
                    OrillaMinD = Dread("number15")
                    OrillaMinI = Dread("number16")
                    OrillaMaxD = Dread("number17")
                    OrillaMaxI = Dread("number18")
                    CajaA = Dread("number19")
                    CajaB = Dread("number20")
                    CajaDBL = Dread("userdecimal1")
                    CajaED = Dread("userdecimal2")
                    TinteColor = Dread("shortchar09")
                End While
                'PrintPreview()
            End If
            con.Close()
        Catch ex As Exception
            Throw New Exception("Al tratar de re-imprimir la receta!!", ex)
        End Try
    End Sub

    'procedimiento que crea el archivo XML 
    Private Sub CreateXML()
        Dim xml As Xml.XmlTextWriter

        ' Pedro Far�as L.
        ' Oct 19 2012
        ' El archivo se crea siempre porque al actualizar y usar WriteXML se pierden los valores que tienen nulo (Nothing)
        '
        'If Not My.Computer.FileSystem.FileExists(My.Application.Info.DirectoryPath & "\LabRx.xml") Then

        Try

            xml = New Xml.XmlTextWriter(My.Application.Info.DirectoryPath & "\LabRx.xml", Nothing)
            xml.IndentChar = " "
            xml.Formatting = xml.Indentation

            xml.WriteStartDocument()
            xml.WriteComment("XML utilizado para la impresion de la receta con codigo de barras.")

            xml.WriteStartElement("Receta")
            xml.WriteStartElement("RxBarCode")

            xml.WriteStartElement("LadoDerecho")
            xml.WriteElementString("Esfera", "")
            xml.WriteElementString("Cilindro", "")
            xml.WriteElementString("Eje", "")
            xml.WriteElementString("AdicionR", "")
            xml.WriteElementString("Molde", "")
            xml.WriteElementString("Prisma", "")
            xml.WriteElementString("EjePrisma", "")
            xml.WriteElementString("Bloque", "")
            xml.WriteElementString("DH", "")
            xml.WriteElementString("DV", "")
            xml.WriteElementString("GrosCent", "")
            xml.WriteElementString("OMin", "")
            xml.WriteElementString("OMax", "")
            xml.WriteElementString("BaseR", "")
            xml.WriteElementString("PotFinalR", "")
            xml.WriteElementString("MoldeExR", "")
            xml.WriteElementString("MsgD", "")
            xml.WriteElementString("MoldeD_HD", "-")
            xml.WriteElementString("MoldeED_HD", "-")
            xml.WriteElementString("AnilloD_HD", "")
            xml.WriteElementString("BcnameD", "")
            xml.WriteElementString("D_DPR_X", "")
            xml.WriteElementString("D_DPR_Y", "")
            xml.WriteElementString("D_DPR_Sph", "")
            xml.WriteElementString("D_DPR_Cyl", "")
            xml.WriteElementString("D_DPR_Axis", "")
            xml.WriteElementString("D_NPR_X", "")
            xml.WriteElementString("D_NPR_Y", "")
            xml.WriteElementString("D_NPR_Sph", "")
            xml.WriteElementString("D_NPR_Cyl", "")
            xml.WriteElementString("D_NPR_Axis", "")

            xml.WriteEndElement()
            'For LadoDerecho

            xml.WriteStartElement("LadoIzquierdo")
            xml.WriteElementString("Esfera", "")
            xml.WriteElementString("Cilindro", "")
            xml.WriteElementString("Eje", "")
            xml.WriteElementString("AdicionL", "")
            xml.WriteElementString("Molde", "")
            xml.WriteElementString("Prisma", "")
            xml.WriteElementString("EjePrisma", "")
            xml.WriteElementString("Bloque", "")
            xml.WriteElementString("DH", "")
            xml.WriteElementString("DV", "")
            xml.WriteElementString("GrosCent", "")
            xml.WriteElementString("OMin", "")
            xml.WriteElementString("OMax", "")
            xml.WriteElementString("BaseL", "")
            xml.WriteElementString("PotFinalL", "")
            xml.WriteElementString("MoldeExL", "")
            xml.WriteElementString("MsgI", "")
            xml.WriteElementString("MoldeI_HD", "-")
            xml.WriteElementString("MoldeEI_HD", "-")
            xml.WriteElementString("AnilloI_HD", "")
            xml.WriteElementString("BcnameI", "")
            xml.WriteElementString("I_DPR_X", "")
            xml.WriteElementString("I_DPR_Y", "")
            xml.WriteElementString("I_DPR_Sph", "")
            xml.WriteElementString("I_DPR_Cyl", "")
            xml.WriteElementString("I_DPR_Axis", "")
            xml.WriteElementString("I_NPR_X", "")
            xml.WriteElementString("I_NPR_Y", "")
            xml.WriteElementString("I_NPR_Sph", "")
            xml.WriteElementString("I_NPR_Cyl", "")
            xml.WriteElementString("I_NPR_Axis", "")
            'For LadoIzquierdo


            xml.WriteEndElement()
            xml.WriteStartElement("Generales")
            xml.WriteElementString("Rx", "")
            xml.WriteElementString("PONumber", "")
            xml.WriteElementString("RxLocal", "")
            xml.WriteElementString("RxBarCode", "")
            xml.WriteElementString("Material", "")
            xml.WriteElementString("Dise�o", "")
            xml.WriteElementString("Cliente", "")
            xml.WriteElementString("AltOblea", "")
            xml.WriteElementString("Mono", "")
            xml.WriteElementString("DipceDiple", "")
            xml.WriteElementString("Armazon", "")
            xml.WriteElementString("A", "")
            xml.WriteElementString("B", "")
            xml.WriteElementString("DBL", "")
            xml.WriteElementString("ED", "")
            xml.WriteElementString("TinteColor", "")
            xml.WriteElementString("TinteNum", "")
            xml.WriteElementString("Gradiente", "")
            xml.WriteElementString("Biselado", "")
            xml.WriteElementString("AR", "")
            xml.WriteElementString("dip", "")
            xml.WriteElementString("dia", "")
            xml.WriteElementString("abrillantado", "")
            xml.WriteElementString("lote", "")
            xml.WriteElementString("horasalida", "")
            xml.WriteElementString("fechasalida", "")
            xml.WriteElementString("rxident", "")
            xml.WriteElementString("trivex", "")
            xml.WriteElementString("fechaent", "")
            xml.WriteElementString("horaent", "8:20AM")
            xml.WriteElementString("ordernum", "")
            xml.WriteElementString("ColorSL", "")
            xml.WriteElementString("Comentarios", "")
            xml.WriteElementString("SpecialLabel", TXT_SpecialLabel)
            xml.WriteElementString("SpecialDesign", "")
            xml.WriteEndElement()


            'Datos monetarios
            xml.WriteStartElement("Dinero")
            xml.WriteElementString("IVApercent", "16")
            xml.WriteElementString("Concepto", "Trabajo Optico VS")
            xml.WriteElementString("PU", "0.00")
            xml.WriteEndElement()


            xml.Flush()
            xml.Close()
        Catch ex As Xml.XmlException

            Throw New Exception("�Al crear el archivo XML de impresion de receta.!", ex)
            xml.Close()
        End Try
        'End If

    End Sub

    'actualizamos el archivo XML que imprime el reporte
    Private Function UpdateXML() As DataSet
        Dim XmlDataSet As DataSet
        Dim Row As DataRow

        Try
            XmlDataSet = New DataSet
            XmlDataSet.ReadXml(My.Application.Info.DirectoryPath & "\LabRx.xml") 'el archivo por default se creara siempre en le drive c:\ del cliente

            If BcNameDer Is Nothing Then BcNameDer = ""
            If BcNameIzq Is Nothing Then BcNameIzq = ""
            For Each Row In XmlDataSet.Tables("LadoDerecho").Rows 'asignacion de valores por cada metodo
                Dim MoldeHD1 As String
                Dim MoldeHD2 As String
                Row("Esfera") = EsferaD
                Row("Cilindro") = CilindroD
                Row("Eje") = EjeDer
                Row("AdicionR") = AdicionR
                Row("Molde") = MoldeD
                Row("EjePrisma") = EjePrismaD
                Row("Prisma") = PrismaD
                If BloqueD = "SIN ANILLO" Then
                    Row("Bloque") = "S/ANILLO"
                Else
                    Row("Bloque") = BcNameDer
                End If
                Row("DH") = DhDer
                Row("DV") = DvDer
                Row("GrosCent") = GrosorCentroD
                Row("OMin") = OMinD
                Row("OMax") = OMaxD
                Row("BaseR") = BaseD
                Row("PotFinalR") = PotFD
                Row("MoldeExR") = MolED
                Row("MsgD") = MensajeD
                Try : MoldeHD1 = MoldeD_HD.Substring(0, MoldeD_HD.IndexOf("-")) : Catch : MoldeHD1 = MoldeD_HD : End Try
                Try : MoldeHD2 = MoldeD_HD.Substring(MoldeD_HD.IndexOf("-") + 1) : Catch : MoldeHD2 = "" : End Try

                If MoldeHD2 <> "" Then
                    Row("MoldeD_HD") = MoldeD_HD
                Else
                    Row("MoldeD_HD") = MoldeHD1
                End If
                Row("MoldeED_HD") = MoldeED_HD 'MED_HD
                Row("AnilloD_HD") = AnilloD_HD

                Row("BcnameD") = BcNameD


                'Row("D_DPR_X") = DDPR_X
                'Row("D_DPR_Y") = DDPR_Y
                'Row("D_DPR_Sph") = DDPR_SPH
                'Row("D_DPR_Cyl") = DDPR_CYL
                'Row("D_DPR_Axis") = DDPR_AXIS
                'Row("D_NPR_X") = DNPR_X
                'Row("D_NPR_Y") = DNPR_Y
                'Row("D_NPR_Sph") = DNPR_SPH
                'Row("D_NPR_Cyl") = DNPR_CYL
                'Row("D_NPR_Axis") = DNPR_AXIS
            Next

            For Each Row In XmlDataSet.Tables("LadoIzquierdo").Rows 'asignacion de valores por cada metodo
                Dim MoldeHD1 As String
                Dim MoldeHD2 As String
                Row("Esfera") = EsferaI
                Row("Cilindro") = CilindroI
                Row("Eje") = EjeIzq
                Row("AdicionL") = AdicionL
                Row("Molde") = MoldeI
                Row("EJEPrisma") = EjePrismaI
                Row("Prisma") = PrismaI
                If BloqueI = "SIN ANILLO" Then
                    Row("Bloque") = "S/ANILLO"
                Else
                    Row("Bloque") = BloqueI
                End If
                Row("DH") = DhIzq
                Row("DV") = DvIzq
                Row("GrosCent") = GrosorCentroI
                Row("OMin") = OMinI
                Row("OMax") = OMaxI
                Row("BaseL") = BaseI
                Row("PotFinalL") = PotFI
                Row("MoldeExL") = MolEI
                Row("MsgI") = MensajeI
                Try : MoldeHD1 = MoldeI_HD.Substring(0, MoldeI_HD.IndexOf("-")) : Catch : MoldeHD1 = MoldeI_HD : End Try
                Try : MoldeHD2 = MoldeI_HD.Substring(MoldeI_HD.IndexOf("-") + 1) : Catch : MoldeHD2 = "" : End Try
                If MoldeHD2 <> "" Then
                    Row("MoldeI_HD") = MoldeI_HD
                Else
                    Row("MoldeI_HD") = MoldeHD1
                End If
                Row("MoldeEI_HD") = MoldeEI_HD ' MEI_HD
                Row("AnilloI_HD") = AnilloI_HD
                Row("BcnameI") = BcNameI

                'Row("I_DPR_X") = IDPR_X
                'Row("I_DPR_Y") = IDPR_Y
                'Row("I_DPR_Sph") = IDPR_SPH
                'Row("I_DPR_Cyl") = IDPR_CYL
                'Row("I_DPR_Axis") = IDPR_AXIS

                'Row("I_NPR_X") = INPR_X
                'Row("I_NPR_Y") = INPR_Y
                'Row("I_NPR_Sph") = INPR_SPH
                'Row("I_NPR_Cyl") = INPR_CYL
                'Row("I_NPR_Axis") = INPR_AXIS


            Next
            For Each Row In XmlDataSet.Tables("Generales").Rows 'asignacion de valores por cada metodo
                Row("Rx") = Rx
                Row("RxLocal") = Me.RxNumLocal
                Row("RxBarCode") = RxBarcode() 'Rx
                Row("Material") = Material
                Row("Dise�o") = Dise�o
                Row("Cliente") = Cliente
                Row("AltOblea") = AltOblea
                Row("DipceDiple") = DipceDiple
                Row("Mono") = monoLR
                Row("Armazon") = Armazon
                Row("A") = CajaA
                Row("B") = CajaB
                Row("DBL") = CajaDBL
                Row("ED") = CajaED
                Row("TinteColor") = TinteColor
                Row("Tintenum") = TinteNum
                Row("Gradiente") = Gradiente
                Row("DIP") = DipceDiple
                Row("Biselado") = Biselado
                Row("AR") = AntiReflajante
                Row("dia") = DiaEspanol(Now.DayOfWeek)
                Row("abrillantado") = Abrillantado
                Row("lote") = Lote
                Row("rxident") = "*" & RXIDEN & "*"
                Row("horasalida") = HoraSalida
                Row("fechasalida") = FechaSalida
                Row("trivex") = MatTX
                Row("fechaent") = FechaEntrada
                Try
                    Row("horaent") = HoraEntrada.Trim()
                Catch ex As Exception
                    Row("horaent") = ""
                End Try
                Row("PONumber") = PONumber
                Row("ordernum") = Vantage
                Row("ColorSL") = ColorSOLEO
                Row("Comentarios") = Comentarios
                Row("SpecialLabel") = TXT_SpecialLabel
                Row("SpecialDesign") = DisenoEsp
            Next

            For Each Row In XmlDataSet.Tables("Dinero").Rows 'asignacion de valores por cada metodo

                Row("IVApercent") = IVApercent
                Row("Concepto") = Concepto
                Row("PU") = PU

            Next

            XmlDataSet.WriteXml(My.Application.Info.DirectoryPath & "\LabRx.xml")
        Catch ex As Exception
            Throw New Exception("�Al actualizar el archivo LabRx.xml!" & vbCrLf & ex.Message, ex)
        End Try

        Return XmlDataSet
        'XmlDataSet.Dispose()
        'XmlDataSet = Nothing
    End Function
    Private Function DiaEspanol(ByVal dia As Integer) As String
        Dim diaes As String = ""

        Select Case (dia)
            Case 1
                diaes = "LUNES"
            Case 2
                diaes = "MARTES"
            Case 3
                diaes = "MIERCOLES"
            Case 4
                diaes = "JUEVES"
            Case 5
                diaes = "VIERNES"
            Case 6
                diaes = "SABADO"
            Case 0
                diaes = "DOMINGO"
        End Select
        Return diaes
    End Function
    'procedimiento que graba la informacion de la receta impresa en la tabla de vantage JobHead
    Private Sub SaveOnJobHead()
        Dim con As SqlClient.SqlConnection
        Dim cmd As SqlClient.SqlCommand
        Dim str As String = ""
        Dim UpdtStr As String = ""
        Try
            con = New SqlClient.SqlConnection(My.Settings.MFGSysConnection) '"user ID=sa;password=proliant01;database=mfgtest80;server=AUGENSVR2;Connect Timeout=30")
            '************* variables del sistem ************************
            str = "number01=" & EsferaD & ","
            str += "number02=" & EsferaI & ","
            str += "number03=" & CilD & ","
            str += "number04=" & CilI & ","
            str += "number05=" & EjeD & ","
            str += "number06=" & EjeI & ","
            str += "shortchar01='" & MoldeD & "',"
            str += "shortchar02='" & MoldeI & "',"
            str += "shortchar03='" & BaseD & "',"
            str += "shortchar04='" & BaseI & "',"
            str += "shortchar05='" & PrismaD & "',"
            str += "shortchar06='" & PrismaI & "',"
            str += "shortchar07='" & BloqueD & "',"
            str += "shortchar08='" & BloqueI & "',"
            str += "number07=" & AltOblea & ","
            str += "shortchar10='" & DipceDiple & "',"
            str += "number09=" & DhD & ","
            str += "number10=" & DhI & ","
            str += "number11=" & DvD & ","
            str += "number12=" & DvI & ","
            str += "number13=" & GrosorCentroD & ","
            str += "number14=" & GrosorCentroI & ","
            str += "number15=" & OrillaMinD & ","
            str += "number17=" & OrillaMinI & ","
            str += "number16=" & OrillaMaxD & ","
            str += "number18=" & OrillaMaxI & ","
            str += "number19=" & CajaA & ","
            str += "number20=" & CajaB & ","
            str += "userdecimal1=" & CajaDBL & ","
            str += "userdecimal2=" & CajaED & ","
            str += "shortchar09='" & TinteColor & "'"
            '***********************************************************
            UpdtStr = "UPDATE JobHead SET " & str & _
                      " WHERE Jobnum = '" & VntgJobNumber & "'" & _
                      " AND Company = 'TRECEUX' AND Plant = 'MfgSys' "
            cmd = New SqlClient.SqlCommand(UpdtStr, con)
            con.Open()
            cmd.ExecuteNonQuery()
            con.Close()
            cmd.Dispose()
            con.Dispose()
            cmd = Nothing
            con = Nothing
        Catch ex As Exception
            cmd = Nothing
            con = Nothing
            Throw New Exception("No se pudo grabar la impresion de receta en vantage (JobHead)", ex)
        End Try
    End Sub
    'asignacin de la variable con el numero de job de vantage para la impresion de la receta
    Public Sub New(ByVal JobNum As String)
        VntgJobNumber = JobNum
    End Sub


    'esta funcion regresa el codigo de barras 2 de 5 para imprimir en la receta
    Private Function RxBarcode() As String
        Dim RxBarStr(2), ITFCod As String


        Dim i, pos, suma As Integer
        pos = 0
        For i = 0 To 4 Step 2
            suma = 33
            ITFCod = Rx.Substring(i, 2)
            If ITFCod >= 90 Then
                Select Case ITFCod
                    Case "90"
                        RxBarStr(pos) = "�"
                    Case "91"
                        RxBarStr(pos) = "�"
                    Case "92"
                        RxBarStr(pos) = "�"
                    Case "93"
                        RxBarStr(pos) = "�"
                    Case "94"
                        RxBarStr(pos) = "�"
                    Case "95"
                        RxBarStr(pos) = "�"
                    Case "96"
                        RxBarStr(pos) = "�"
                    Case "97"
                        RxBarStr(pos) = "�"
                    Case "98"
                        RxBarStr(pos) = "�"
                    Case "99"
                        RxBarStr(pos) = "�"
                End Select
            Else
                RxBarStr(pos) = Chr(ITFCod + suma)
            End If
            pos = pos + 1
        Next

        Return "{" & RxBarStr(0) & RxBarStr(1) & RxBarStr(2) & "}"
    End Function

End Class
'----------------------------------------------------------------------
'objeto que contiene las variables de la forma 1 para imprimir el la 
'receta cuando esta se generwe
'----------------------------------------------------------------------
Public Class RxPrintInfo
    Public TinteColor As String = ""
    Public TinteNum As String = ""
    Public Gradiente As String = ""
    Public Dise�o As String = ""
    Public Material As String = ""
    Public AdicionR As String = ""
    Public AdicionL As String = ""
    Public Cliente As String = ""
    Public DipceDiple As String = ""
    Public Armazon As String = ""
    Public Puente As Integer = 0
    Public ModificacionRx As Boolean
    Public SinArmazon As Boolean
    Public Biselado As String = ""
    Public AR As String = ""
    Public CheckedEyes As Eyes = Eyes.Both
    Enum Eyes
        None = 0
        Left = 1
        Right = 2
        Both = 3
    End Enum
    Public PotFinalD As String = ""
    Public PotFinalI As String = ""
    Public MolExacD As String = ""
    Public MolExacI As String = ""
    Public Abrillantado As String = ""
    Public Optimizado As String = ""
    Public RxFromVirtual As Boolean = False 'variable que nos dice si la receta es virtual
    Public RxNumFromVirtual As Integer 'variable que guarda el numero de receta virtual alacenada en el guiador
    Public LeftFinished As Boolean = False 'variable solo uilizada para identificar si el lente es terminado, cuando es sin armazon
    Public RightFinished As Boolean = False 'variable solo uilizada para identificar si el lente es terminado, cuando es sin armazon
    Public Rxident As String = ""
End Class
