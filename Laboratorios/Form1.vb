Imports Microsoft.Win32
Imports System.Data.SqlClient
Imports System.Net.Mail
Imports System.Drawing
Imports System.Drawing.Drawing2D
Imports System.Drawing.Imaging
Imports System.Drawing.Text
Imports System.Threading
Imports System.IO
Imports System.Xml

Public Class Form1

    Dim MCentroD, MCentroI, BCnameD, BCnameI As String

    Dim MyFrame As Frames
    Dim FrameErrorID As Integer
    Dim FrameErrorDesc As String
    Dim IsFrameOK As Boolean

    Dim SpecialDesignID As Integer
    Dim Original_SpecialDesID As Integer
    'Dim RadiosVirtuales(511) As Integer

    Dim SD_Partnum As String
    Dim ChangeTraceDenied As Boolean = False
    Dim HasSpecialLabel As Boolean
    Dim SpecialLabel As String
    Dim OMA As New OMAFile(1, 1, 400)
    Dim LabelMaterials() As Label
    Dim InternetOrdersLoaded As Boolean = False
    Dim OperGenerador As Boolean = False
    Dim OperBiselado As Boolean = False
    Dim OperAntireflejante As Boolean = False
    Dim OperEmpaque As Boolean = False
    Dim OrderExists As Boolean = False
    Dim MaterialStr As String = ""
    Dim DesignStr As String = ""
    Dim MaterialID As Integer
    Dim ClaseIDR As String
    Dim ClaseIDL As String
    Dim DesignID As Integer
    Dim InitStatus As InitStatusType = InitStatusType.Starting
    Dim RxType As RxTypes = RxTypes.NormalRx
    Dim TESTING As Boolean = False
    Public WorkingOnLine As Boolean = True
    Dim TickStatus As Boolean = False
    Public ind As Integer               ' Variable que guarda el row seleccionado del grid de costco
    Dim ServerAdd As String = ""        ' Variable que contiene la direccion del servidor para generar SO's y Jobs
    Public DataBase As String = ""
    Dim Sizing As Short = 0             ' Variable para guardar el ajuste de los trazos digitales [Sizing]
    Dim IsManualRx As Boolean = False   ' Variable para saber que tipo de receta es [Manual/Digital]
    Dim PreviewForm As PreviewArmazon
    Dim LI As New Calcot.SetLadoIzquierdo
    Dim LD As New Calcot.SetLadoDerecho
    Dim BaseLStr As String
    Dim BaseRStr As String
    Dim RadiosL(NbrsTraces) As Integer
    Dim RadiosR(NbrsTraces) As Integer
    Public pe As New ProcessEfficiency
    Dim JNStr As String, JobRev As String, GenPNStr As String, QtyStr As String, Pedido As String
    Dim MyThread As Thread
    Dim PingThread As Thread
    Dim IsThreadFinished As Boolean
    Dim InsertLog As Boolean            ' Variable para indicar que se tiene que meter un registro al Log
    Dim InsertLogSide As Short          ' Variable para definir que lente se modifico y meterlo al Log   [0: Ninguno | 1: Izquierdo | 2: Derecho | 3:Ambos]
    Dim PrismaL As Double
    Dim PrismaR As Double
    Dim EjePrismaL As Double
    Dim EjePrismaR As Double
    Dim SystemsPickL As String
    Dim SystemsPickR As String
    Public IsModifying As Boolean
    Dim WO As New WebOrders
    Dim AL As New ARList
    Dim ChangingTraces As Boolean = False
    Dim RxVirtual As Boolean = False 'variable para identificar si la receta que se esta calculando proviene del guiador virtual
    Dim OriginalLeftPart As String
    Dim OriginalRightPart As String
    Dim OriginalUnmodifyedPart As String
    Dim FrameNum As String
    Public ARLab As Integer                 ' Variable que guarda el numero del lab. de AR al que se mandara la receta
    Public ARLabName As String              ' Variable que almacena el nombre del Lab. AR
    Public IsARLaboratory As Boolean        ' Variable que almacena si el laboratorio es de AR de acuerdo a TblLaboratorios
    Public IsVirtualRx As Boolean
    Public IsReceivingVirtualRx As Boolean  ' Variable que marca que se esta dando entrada a una Rx Virtual
    Public OriginalRefDigital As Integer    ' Variable que almacena la ref. digital original de una Rx Virtual
    Public OriginalCustNum As Integer       ' Variable que almacena el custnum original de las Rx Virtuales (ya que cuando se recibe se imprime el nombre del lab.)
    Public IsLocalReceive As Boolean        ' Variable que dice si se hasta recibiendo una Rx Visrtual en Lab. Local.
    Public IsLocalRx As Boolean             ' Variable que indica si se esta haciendo un movimiento en el Lab. Local o Remoto.
    Public IsGratisRx As Boolean            ' Variable que indica si la orden sera marcada como gratis (sin costo).
    Public IsGarantia As Boolean            ' Variable que indica si la orden sera marcada como garantia (sin costo).
    Public ReceivingVirtualRx As String     ' Variable que guarda el valor de la Rx que esta siendo recibida.
    Public IsCompleta As Boolean            ' Variable que dice si la captura es para Procesar o Incompleta
    Public ClienteTecleado As String        ' Variable que almacena el customer que teclea el capturista
    Public RxNumLocal As Integer            ' Variable que almacena el numero de referencia digital en el lab. local
    Public NoTrace As Boolean               ' Variable que decide si la Rx tiene trazo o no al modificar la Rx.
    Public OrderOK As Boolean               ' Variable que nos indica si la Rx se genero correctamente o no
    Public LeftRequested As Boolean         ' Variable que dice que ojos se enviaron por virtual
    Public RightRequested As Boolean        ' Variable que dice que ojos se enviaron por virtual
    Public LeftBarcodeRead As Boolean
    Public RightBarcodeRead As Boolean
    Public OverrideClientSelectionChanged As Boolean = False
    Dim Original_IsLocalRx, Original_IsVirtualRx As Boolean 'Variables que guardan los valores originales de la receta antes de que el usuario modifique de nuevo la receta

    Dim Original_ARLab As Integer
    Dim ModifyingFromLocal As Boolean       ' Variable que indica si se esta modificando una Rx que se encontro en el servidor local.

    Dim ReadingBarCode As Boolean           ' Indica si se esta leyendo un codigo de barras
    Dim StartReadingBarcodeTime As System.TimeSpan  ' Hora de inicio de lectura del lente
    Dim EndReadingBarcodeTime As System.TimeSpan    ' Hora de termino de lectura del lente

    Public PedidoCreate As String
    Public PedidoUpdate As String

    Public PACKTRAN_TYPE As Integer          ' Contiene el tipo de trans. que se hara para un paquete en una modificacion.
    '                                         0: Nada,  1: Quitar Paquete,  2: Cambiar Armaz�n 3: Cambiar Paquete

    Public ArmazonOriginal As Integer
    Public numpaq As String
    Public NumArmazon As Integer

    Public CostcoLente As String
    Public CostcoTinte As String
    Public CostcoAR As String
    Public CostcoAbrillantado As String

    Public SeqID As Integer
    Public FehcaEntradaWeb As String
    Public FechaEntradaFisica As String

    Public Rx As Receta

    Public SavedInRemoteServer As Boolean

    Dim FechaARInLocalLab As String         'Variable que guarda la fecha de entrada de AR en el laboratorio local que solicito el AR
    Dim IsARReceived As Boolean             'Bandera que guarda si la receta con AR que se esta modificando ya fue recibida en el lab local

    Dim ShowLeftEye As Boolean              ' Bandera para mostrar checkbox de ojo izquierdo
    Dim ShowRightEye As Boolean             ' Bandera para mostrar checkbox de ojo derecho

    '-----------------------------------
    'variables que guardan los grosores de los lentes 
    Public GrosorCentralD As Single
    Public GrosorCentralI As Single
    '----------------------------------------------------
    'objeto de impresion para mandar a imprimir desde el form1
    Public PrintObj As Impresion
    '-----------------------------------
    'variable que guarda la fecha final de la receta si esta se esta modificando
    Dim FechaInicial, FechaEntrada As String
    Public RxNoCalculos As Boolean = False
    Public SaveToTracerData As Boolean      ' Variable para identificar si se debe guardar en el trazador o no
    '******************************************************
    '*** Variables global para seleccionar la tabla de la base de datos de los trazos
    Dim TracerTable As String = ""
    Dim VwConsultaTrazos As String = ""
    Dim OMA_IN_OUT, OMA_UP_DOWN As String 'VARIABLES PARA EL BLOQUEO
    Dim OMA_LTYP As String      'OMA LENS TYPE PARA EL BLOQUE
    Dim OMA_SWIDHT As String    'OMA DIAMETRO DE LA OBLEA
    Dim OMA_LMATID As String    'OMA MATERIAL DEL OJO PARA BISELADO
    Dim OMA_POLISH As String    'OMA SI LLEVA ABRILLANTADO O NO PARA BISELADO
    Dim OMA_BEVP As String      'OMA BEVEL PARA BISELADO
    Dim OMA_IPD As String       'OMA MONOCULAR DISTANCIA PARA BISELADO
    Dim OMA_BCOCIN As String    'OMA DISTANCIA HORIZONTAL DEL CENTRO GEOMETRICO AL CENTRO OPTICO
    Dim OMA_BCOCUP As String    'OMA DISTANCIA VERTICAL DEL CENTRO GEOMETRICO AL CENTRO OPTICO
    Dim OMA_DIA As String       'OMA DIAMETRO DE LAS PASTILLAS BISELADORA
    Dim OMA_CRIB As String      'OMA ? SIEMPRE ES CERO BISELADORA
    Dim OMA_FCSGIN As String    'OMA DISTANCIA HORIZONTAL DEL CENTRO GEOMETRICO AL CENTRO DE LA OBLEA  BISELADORA
    Dim OMA_FCSGUP As String    'OMA DISTANCIA VERTICAL DEL CENTSDRO GEOMETRICO AL CENTRO DE LA OBLEA  BISELADORA
    Dim OMA_SDEPTH As String    'OMA ALTURA DE LA OBLEA EN BIFOCAL PARA BISELADORA
    Dim OMA_NPD As String       'OMA BLOQUEADORA NEAR MONOCULAR DISTANCE 
    Dim OMA_FCOCIN As String    'OMA BISELADORA DISTANCIA HORIZONTAL DEL CENTRO DEL ARMAZON AL C. OPTICO
    Dim OMA_FCOCUP As String    'OMA BISELADORA DISTANCIA VERTICAL DEL CENTRO DEL ARMAZON AL C. OPTICO
    Dim OMA_EyeSide As String   'OMA LADO TRAZADO EN LA BIASELADORA
    Dim OMA_DRILL As String     'OMA DATOS DE PERFORADO PARA LA BISELADORA

    ' Variables para el perforado de lentes
    Dim OMA_PER_X1, OMA_PER_X2, OMA_PER_Y1, OMA_PER_Y2 As String
    Dim OMA_PER_DIA, OMA_PER_DEPTH, OMA_PER_BFA, OMA_PER_ANGLAT, OMA_PER_ANGVER As String
    '******************************************************
    ' Variables para guardar los valores en numero de las etiquetas OMA
    Dim OMA_DHR, OMA_DVR, OMA_DHL, OMA_DVL As String 'VARIABLES PARA EL BLOQUEO
    Dim OMA_LTYPR, OMA_LTYPL As String      'OMA LENS TYPE PARA EL BLOQUE
    Dim OMA_LMATIDR, OMA_LMATIDL As String  'OMA MATERIAL DEL OJO PARA BISELADO
    Dim OMA_IPDR, OMA_IPDL As String        'OMA MONOCULAR DISTANCIA PARA BISELADO
    Dim OMA_BCOCINR, OMA_BCOCINL As String  'OMA DISTANCIA HORIZONTAL DEL CENTRO GEOMETRICO AL CENTRO OPTICO
    Dim OMA_BCOCUPR, OMA_BCOCUPL As String  'OMA DISTANCIA VERTICAL DEL CENTRO GEOMETRICO AL CENTRO OPTICO
    Dim OMA_FCSGINR, OMA_FCSGINL As String  'OMA DISTANCIA HORIZONTAL DEL CENTRO GEOMETRICO AL CENTRO DE LA OBLEA  BISELADORA
    Dim OMA_FCSGUPR, OMA_FCSGUPL As String  'OMA DISTANCIA VERTICAL DEL CENTSDRO GEOMETRICO AL CENTRO DE LA OBLEA  BISELADORA
    Dim OMA_SDEPTHR, OMA_SDEPTHL As String  'OMA ALTURA DE LA OBLEA EN BIFOCAL PARA BISELADORA
    Dim OMA_NPDR, OMA_NPDL As String        'OMA BLOQUEADORA NEAR MONOCULAR DISTANCE 
    Dim OMA_FCOCINR, OMA_FCOCINL As String  'OMA BISELADORA DISTANCIA HORIZONTAL DEL CENTRO DEL ARMAZON AL C. OPTICO
    Dim OMA_FCOCUPR, OMA_FCOCUPL As String  'OMA BISELADORA DISTANCIA VERTICAL DEL CENTRO DEL ARMAZON AL C. OPTICO
    Dim OMA_CRIBR, OMA_CRIBL As String
    Dim OMA_SWIDHTR, OMA_SWIDHTL As String
    Dim OMA_DIAR, OMA_DIAL As String
    '******************************************************
    Dim FechaDeCreacion As String
    Dim date02, date03, date04, date05, date06, date07 As String
    Dim checkbox10, checkbox11, checkbox12, checkbox13, checkbox16 As Boolean


    '-----------------------------------------------------------------------------------------------------------------
    ' Variables para nuevo servidor de calculos opticos OptiSur
    '-----------------------------------------------------------------------------------------------------------------
    Dim Optisur As NewCalcot
    'Dim MyJob As Job
    'Dim MyJobResp As DisplFrame
    'Dim LeftJob As JobItem
    'Dim RightJob As JobItem
    'Dim ManualFrame As GeoFrame
    'Dim DigitalFrame As Pattern

    '-----------------------------------------------------------------------------------------------------------------
    Enum QAStatus
        GP = 0
        AR = 1
        BM = 2
        PC = 3
        Unknown = -1
    End Enum
    Enum InitStatusType
        Starting
        Working
    End Enum
    Enum RxStatusOptions
        Create
        CreateFromWeb
        Update
        VirtualReceive
        LocalReceive
    End Enum
    Enum LabLocation
        Virtual
        Local
    End Enum
    Enum VirtualRxAction
        Create
    End Enum
    Enum CustomValues
        Clase
        Material
        MaterialID
        Dise�o
        Dise�oID
        Base
        Lado
        AR
        ProdCode
        OMADis_ID
        OMAMat_ID
        OMASeg_Width
    End Enum
    Enum UpdateType
        OrderHeader
        OrderAndLines
    End Enum
    Enum WorkingStatusType
        OnLine
        OffLine
    End Enum
    Enum RxTypes
        WebRx
        NormalRx
        VirtualRx
    End Enum

    Private Sub ActivaAjustes(ByVal LabID As Integer, ByVal orden As String)
        ' Nueva funci�n del LabRx - Agregar ajustes de precios/cargos miscel�neos
        ' Pedro Far�as Lozano Dic 4 2012
        ' Se muestra ventana para ajustas e imprimir remisi�n para el cliente

        Dim frmAjustame As New AperturaRx(LabID, orden)
        frmAjustame.ShowDialog()

    End Sub

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
    Public Sub InitValues()
        Dim temp As String = ""

        '        CBX_SpecialDesign.Enabled = My.Settings.EnableOptisur
        MyFrame = New Frames(0, Frames.ThisFrameStatus.[New])
        FrameErrorDesc = ""
        FrameErrorID = 0
        SpecialDesignID = 0
        Original_SpecialDesID = 0
        SD_Partnum = ""
        PanelRightEye.Enabled = True
        PanelLeftEye.Enabled = True
        IsFrameOK = False
        PNL_Armazon.Visible = False
        PNL_Armazon.Enabled = True
        CBX_SpecialDesign.Checked = False
        ChangeTraceDenied = False
        HasSpecialLabel = False
        SpecialLabel = ""

        Rx = New Receta
        OMA = New OMAFile(1, 1, 400)

        date02 = ""
        date03 = ""
        date04 = ""
        date05 = ""
        date06 = ""
        date07 = ""

        checkbox10 = False : checkbox11 = False : checkbox12 = False : checkbox13 = False : checkbox16 = False

        PedidoCreate = ""
        PedidoUpdate = ""

        SavedInRemoteServer = False

        SeqID = 0
        FehcaEntradaWeb = ""
        FechaEntradaFisica = ""

        ModifyingFromLocal = False
        ReadingBarCode = False
        'BackPix.Visible = True
        RightRequested = False
        LeftRequested = False
        LabelWorking.Visible = False
        OrderOK = False
        NoTrace = False
        RxNumLocal = 0
        TextBox1.Visible = False
        OverrideClientSelectionChanged = False
        PanelTipoCaptura.Visible = False
        IsCompleta = False
        ReceivingVirtualRx = ""
        FrameNum = "0"
        CheckGarantia.Checked = False
        CheckGarantia.Visible = True
        CheckGarantia.Enabled = True
        IsGarantia = False
        IsGratisRx = False
        CostcoLente = ""
        CostcoTinte = ""
        CostcoAR = ""
        CostcoAbrillantado = ""
        PanelInformacion.Visible = False
        PanelComentarios.Visible = False
        TextComentarios.Text = ""
        AceptarManualNew.Visible = True
        AceptarDigitalNew.Visible = True
        RadioManualNew.Enabled = True
        RadioDigitalNew.Enabled = True
        RadioDigitalNew.Checked = True
        ComboTrazos.Enabled = True
        ComboTrazos.ComboBox1.DataSource = Nothing
        ComboTrazos.Bind()
        ComboTrazos.Texto = ""
        ComboArmazon2.Enabled = True
        PanelPreviewTrazo.Visible = True
        PanelErrores.Location = New Point(265, 233)

        CheckLevel1.Checked = False
        CheckLevel2.Checked = False
        CheckLevel3.Checked = False
        ShowLeftEye = False
        ShowRightEye = False
        SaveToTracerData = False
        LabelARLab.Visible = False
        ComboArmazon2.ComboBox1.SelectedIndex = 0
        IsLocalRx = True
        IsLocalReceive = False
        ARLabName = "NINGUNO"
        ARLab = 0
        IsVirtualRx = False
        Original_IsVirtualRx = False
        Original_IsLocalRx = False
        OriginalCustNum = 0
        Original_ARLab = 0
        PanelGraduacion.Enabled = True
        PanelOpciones.Enabled = True
        OriginalRefDigital = 0
        ComboClients.Visible = True
        TextLabClient.Visible = False
        IsReceivingVirtualRx = False
        RxNoCalculos = False
        OriginalLeftPart = ""
        OriginalRightPart = ""
        ChangingTraces = False
        PanelErrores.Visible = False
        Me.Cursor = Cursors.Default
        IsModifying = False
        SystemsPickL = ""
        SystemsPickR = ""
        PrismaL = 0
        PrismaR = 0
        EjePrismaL = 0
        EjePrismaR = 0
        InsertLog = False
        InsertLogSide = 0
        RightRead.ReadOnly = False
        LeftRead.ReadOnly = False
        CheckREye.Checked = True
        CheckLEye.Checked = True
        ComboClients.SelectedIndex = 0
        RxType = RxTypes.NormalRx
        'GroupTrazos.Location = New Point(12, 180)
        OrderExists = False
        OperGenerador = False
        OperBiselado = False
        OperAntireflejante = False
        OperEmpaque = False
        RxID.Text = ""
        RxNum.Text = "" : RxNum.Enabled = True : RxNum.Visible = False
        RxWECO.Text = "" : RxWECO.Enabled = True : RxWECO.Visible = False
        PanelRx.Enabled = True
        PanelArmazon.Enabled = True
        PanelOpciones.Visible = False
        AceptarManualNew.Visible = False
        ' AceptarDigital.Enabled = False
        RadioDigitalNew.Checked = True
        'Trazos.DataSource = Nothing
        'Trazos.Items.Clear()
        'Trazos.Enabled = False
        PreviewTrazo.Image = Nothing
        'GroupTrazos.Visible = True
        EsferaRight.Text = "0.00"
        EsferaLeft.Text = "0.00"
        CilindroRight.Text = "0.00"
        CilindroLeft.Text = "0.00"
        EjeRight.Text = "0"
        EjeLeft.Text = "0"
        AdicionLeft.Text = "0.00"
        AdicionRight.Text = "0.00"
        AlturaRight.Text = "0"
        AlturaLeft.Text = "0"
        MonoLeft.Text = "0"
        MonoRight.Text = "0"
        DIPLejos.Text = "0"
        DIPCerca.Text = "0"
        Altura.Text = "0"
        ContornoA.Text = "0"
        ContornoB.Text = "0"
        ContornoED.Text = "0"
        ContornoPuente.Text = "0"
        ContornoA.Enabled = True
        ContornoB.Enabled = True
        ContornoED.Enabled = True
        PanelContorno.Enabled = False
        PanelOblea.Enabled = False
        CheckTintes.Checked = False
        ComboTinte.SelectedValue = 0
        ComboTinte.Texto = "NINGUNO"
        RadioGradiente.Checked = False
        PanelInformacion.Visible = False
        CheckAR.Checked = False
        CheckARGold.Checked = False
        CheckARMatiz.Checked = False
        CheckBiselado.Checked = False
        CheckRLX.Checked = False
        CheckAbril.Checked = False
        CheckOptim.Checked = False
        ListLeftParts.DataSource = Nothing : ListLeftParts.Items.Clear()
        ListRightParts.DataSource = Nothing : ListRightParts.Items.Clear()
        RxSave.Enabled = False
        ComboClients.Enabled = True
        RxID.Enabled = True
        RxID.Select()
        ClaseIDR = ""
        ClaseIDL = ""
        ComboMonofocal.SelectedValue = "0"
        ComboBifocal.SelectedValue = "0"
        ComboProgresivo.SelectedValue = "0"
        ComboOtros.SelectedValue = "0"

        txtQtyOnHandRight.Text = ""
        txtQtyOnHandLeft.Text = ""

        PACKTRAN_TYPE = 0
        ArmazonOriginal = 0
        NumArmazon = 0
        numpaq = ""

        'variables de oma 
        OMA_EyeSide = ""
        OMA_DHR = ""
        OMA_DHL = ""
        OMA_DVR = ""
        OMA_DVL = ""
        OMA_SWIDHTR = ""
        OMA_SWIDHTL = ""
        OMA_LTYPR = ""
        OMA_LTYPL = ""
        OMA_LMATIDR = ""
        OMA_LMATIDL = ""
        OMA_IPDR = ""
        OMA_IPDL = ""
        OMA_BCOCINR = ""
        OMA_BCOCINL = ""
        OMA_BCOCUPR = ""
        OMA_BCOCUPL = ""
        OMA_DIAR = ""
        OMA_DIAL = ""
        OMA_CRIBR = ""
        OMA_CRIBL = ""
        OMA_FCSGINR = ""
        OMA_FCSGINL = ""
        OMA_FCSGUPR = ""
        OMA_FCSGUPL = ""
        OMA_SDEPTHR = ""
        OMA_SDEPTHL = ""
        OMA_NPDR = ""
        OMA_NPDL = ""
        OMA_FCOCUPR = ""
        OMA_FCOCUPL = ""
        OMA_FCOCINR = ""
        OMA_FCOCINL = ""
        'variables para el drill
        OMA_PER_ANGLAT = "?"
        OMA_PER_ANGVER = "?"
        OMA_PER_BFA = "?"
        OMA_PER_DEPTH = "?"
        OMA_PER_DIA = "?"
        OMA_PER_X1 = "?"
        OMA_PER_X2 = "?"
        OMA_PER_Y1 = "?"
        OMA_PER_Y2 = "?"

        Dim i As Integer
        For i = 0 To NbrsTraces()
            RadiosL(i) = 0
            RadiosR(i) = 0
            'RadiosVirtuales(i) = 0
        Next

        'PanelArmazon.Size = New Size(406, 45)
        Me.AdicionLeft_Leave(Me, New System.EventArgs)
        PanelModInfo.Visible = False

        'Revisa los ajustes necesarios, si no existen en el archivo de configuraci�n

    End Sub
    Private Sub ClearValues()
        EsferaRight.Text = "0.00"
        EsferaLeft.Text = "0.00"
        CilindroRight.Text = "0.00"
        CilindroLeft.Text = "0.00"
        EjeRight.Text = "0.00"
        EjeLeft.Text = "0.00"
        AdicionLeft.Text = "0.00"
        AdicionRight.Text = "0.00"
        AlturaRight.Text = "0"
        AlturaLeft.Text = "0"
        MonoLeft.Text = "0"
        MonoRight.Text = "0"
        DIPLejos.Text = "0"
        DIPCerca.Text = "0"
        Altura.Text = "0"
        If IsManualRx Then
            ContornoA.Text = ""
            ContornoB.Text = ""
            ContornoED.Text = ""
            ContornoPuente.Text = ""
        End If
        ListLeftParts.DataSource = Nothing : ListLeftParts.Items.Clear()
        ListRightParts.DataSource = Nothing : ListRightParts.Items.Clear()
        SelectedLeftEye.Items.Clear() : SelectedLeftEye.Visible = False
        SelectedRightEye.Items.Clear() : SelectedRightEye.Visible = False
        CheckBiselado.Checked = False
        CheckAR.Checked = False
        CheckTintes.Checked = False
        ComboTinte.SelectedValue = 0
        RadioGradiente.Checked = False
        ClaseIDR = ""
        ClaseIDL = ""
        OrderExists = False
    End Sub
    Function GetBase(ByRef Base As String, ByRef Adicion As String, ByRef Asferico As String, ByVal Clase As String, ByVal Cilindro As String, ByVal Esfera As String, ByVal AdicionReal As String, ByVal Material As String, ByVal Dise�o As String) As Boolean
        Dim Value As Boolean = False

        If Esfera <> "" Then
            Esfera = CDbl(Esfera)
        Else
            Esfera = 0
        End If
        If Cilindro <> "" Then
            Cilindro = CDbl(Cilindro)
        Else
            Cilindro = 0
        End If
        If AdicionReal <> "" Then
            AdicionReal = CDbl(AdicionReal)
        Else
            AdicionReal = 0
        End If

        If Clase = "T" Then
            If Dise�o > 3 Then
                Base = Esfera
                Adicion = AdicionReal
                Value = True
            Else
                Base = Esfera
                Adicion = Cilindro
                Value = True
            End If
        Else
            Dim Conn As New SqlDB(My.Settings.LocalServer, My.Settings.DBUser, My.Settings.DBPassword, My.Settings.LocalDBName)
            Try
                Conn.OpenConn()
                Dim Comm As New SqlCommand("SP_ObtenBase", Conn.SQLConn)
                Comm.CommandType = Data.CommandType.StoredProcedure
                Comm.Parameters.AddWithValue("esfera", Esfera)
                Comm.Parameters.AddWithValue("cilindro", Cilindro)
                Comm.Parameters.AddWithValue("cl_mat", Material)
                Comm.Parameters.AddWithValue("cl_diseno", Dise�o)
                Comm.Parameters.Add("base", Data.SqlDbType.Float)
                Comm.Parameters.Add("optimo", Data.SqlDbType.Bit)
                Comm.Parameters.Add("Asferico", Data.SqlDbType.Int)
                Comm.Parameters("base").Direction = Data.ParameterDirection.Output
                Comm.Parameters("optimo").Direction = Data.ParameterDirection.Output
                Comm.Parameters("Asferico").Direction = Data.ParameterDirection.Output
                Comm.ExecuteNonQuery()

                Base = Comm.Parameters("base").Value
                Asferico = Comm.Parameters("Asferico").Value

                Dim Optimo = Comm.Parameters("optimo").Value
                Adicion = AdicionReal

                If Optimo Then
                    Value = True
                Else
                    Value = False
                End If
            Catch ex As Exception
                MsgBox(ex.Message, MsgBoxStyle.Critical)
            Finally
                Conn.CloseConn()
            End Try
        End If
        Return Value
    End Function

    Private Sub BuscarLentes_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BuscarLentes.Click
        Dim Material As Integer = 0
        Dim Design As Integer = 0
        Dim MaterialDesignStr As String = ""
        Dim AdicionLStr As String = ""
        Dim AdicionRStr As String = ""
        Dim Base As String = ""
        Dim AsfericoLStr As String = ""
        Dim AsfericoRStr As String = ""
        Dim buscarlentes As Boolean = True
        Dim t As SqlDB
        Dim ds As New DataSet
        Dim row As DataRow

        ListLeftParts.BeginUpdate()
        ListRightParts.BeginUpdate()
        ListLeftParts.Items.Clear()
        ListRightParts.Items.Clear()
        If (Not CheckLEye.Checked) And (Not CheckREye.Checked) Then
            ListLeftParts.EndUpdate()
            ListRightParts.EndUpdate()
            MsgBox("No se han seleccionado ojos", MsgBoxStyle.Critical)
        Else
            t = New SqlDB(My.Settings.LocalServer, My.Settings.DBUser, My.Settings.DBPassword, My.Settings.LocalDBName)
            Try
                t.OpenConn()
                Application.DoEvents()
                GetMaterialDesign(Material, Design)
                If Material <> 0 Then
                    ds = t.SQLDS("select tblmaterials.materialid,tbldesigns.disenoid from tblmaterials,tbldesigns where cl_mat = " & Material & " and cl_diseno = " & Design, "t1")
                    For Each row In ds.Tables("t1").Rows
                        MaterialStr = row("MaterialID")
                        DesignStr = row("DisenoID")
                    Next
                End If

                If Design < 8 Then
                    If CheckLEye.Checked And CheckREye.Checked Then
                        If Not (GetBase(BaseLStr, AdicionLStr, AsfericoLStr, "T", Redondea(CilindroLeft.Text), Redondea(EsferaLeft.Text), AdicionLeft.Text, Material, Design) And _
                           GetBase(BaseRStr, AdicionRStr, AsfericoRStr, "T", Redondea(CilindroRight.Text), Redondea(EsferaRight.Text), AdicionRight.Text, Material, Design)) Then
                            buscarlentes = MsgBox("Los datos no cumplen con las Especificaciones Est�ticas para un lente." & vbCrLf & "Deseas continuar con la busqueda de lentes?", MsgBoxStyle.YesNo) = MsgBoxResult.Yes
                        End If
                    Else
                        If CheckLEye.Checked Then
                            If Not (GetBase(BaseLStr, AdicionLStr, AsfericoLStr, "T", Redondea(CilindroLeft.Text), Redondea(EsferaLeft.Text), AdicionLeft.Text, Material, Design)) Then
                                buscarlentes = MsgBox("Los datos no cumplen con las Especificaciones Est�ticas para un lente." & vbCrLf & "Deseas continuar con la busqueda de lentes?", MsgBoxStyle.YesNo) = MsgBoxResult.Yes
                            End If

                        End If
                        If CheckREye.Checked Then
                            If Not (GetBase(BaseRStr, AdicionRStr, AsfericoRStr, "T", Redondea(CilindroRight.Text), Redondea(EsferaRight.Text), AdicionRight.Text, Material, Design)) Then
                                buscarlentes = MsgBox("Los datos no cumplen con las Especificaciones Est�ticas para un lente." & vbCrLf & "Deseas continuar con la busqueda de lentes?", MsgBoxStyle.YesNo) = MsgBoxResult.Yes
                            End If
                        End If
                    End If
                    If buscarlentes Then
                        If BaseRStr < BaseLStr Then
                            Base = BaseRStr
                        Else
                            Base = BaseLStr
                        End If
                        If Material > 3 And Design <> 14 And Design <> 12 And Design <> 17 Then
                            If CheckREye.Checked Then
                                ds = t.SQLDS("select * from vwpartTRECEUX where material like '%" & MaterialStr & _
                                    "%' and dise�o like '%" & DesignStr & _
                                    "%' and base = '" & (BaseRStr) & _
                                    "' and (adicion = '" & (AdicionRStr) & _
                                    "' and lado = 'R')" & _
                                    " and clase = 'T'", "t1")
                                For Each rw As DataRow In ds.Tables("t1").Rows
                                    ListRightParts.Items.Add(New ItemWithValue(rw("partdescription"), rw("partnum")))
                                Next
                            End If
                            If CheckLEye.Checked Then
                                ds = t.SQLDS("select * from vwpartTRECEUX where material like '%" & MaterialStr & _
                                    "%' and dise�o like '%" & DesignStr & _
                                    "%' and base = '" & (BaseLStr) & _
                                    "' and (adicion = '" & (AdicionLStr) & _
                                    "' and lado = 'L')" & _
                                    " and clase = 'T'", "t2")
                                For Each rw As DataRow In ds.Tables("t2").Rows
                                    ListLeftParts.Items.Add(New ItemWithValue(rw("partdescription"), rw("partnum")))
                                Next
                            End If
                        Else
                            Dim NewDesign As String = DesignStr

                            If CheckLEye.Checked Then
                                NewDesign = DesignStr
                                If DesignStr = "TA" Then
                                    NewDesign = "VS"
                                End If
                                ds = t.SQLDS("select * from vwpartTRECEUX where material like '%" & MaterialStr & _
                                    "%' and dise�o like '%" & NewDesign & _
                                    "%' and base = '" & (BaseLStr) & _
                                    "' and (adicion = '" & (AdicionLStr) & _
                                    "' and lado = 'N')" & _
                                    " and clase = 'T'", "t1")
                                For Each rw As DataRow In ds.Tables("t1").Rows
                                    ListLeftParts.Items.Add(New ItemWithValue(rw("partdescription"), rw("partnum")))
                                Next
                            End If
                            If CheckREye.Checked Then
                                NewDesign = DesignStr
                                If DesignStr = "TA" Then
                                    NewDesign = "VS"
                                End If
                                ds = t.SQLDS("select * from vwpartTRECEUX where material like '%" & MaterialStr & _
                                           "%' and dise�o like '%" & NewDesign & _
                                           "%' and base = '" & (BaseRStr) & _
                                           "' and (adicion = '" & (AdicionRStr) & _
                                           "' and lado = 'N')" & _
                                           " and clase = 'T'", "t1")
                                For Each rw As DataRow In ds.Tables("t1").Rows
                                    ListRightParts.Items.Add(New ItemWithValue(rw("partdescription"), rw("partnum")))
                                Next
                            End If
                        End If
                    End If
                End If
                If CheckLEye.Checked And CheckREye.Checked Then
                    If Not (GetBase(BaseLStr, AdicionLStr, AsfericoLStr, "ST", Redondea(CilindroLeft.Text), Redondea(EsferaLeft.Text), AdicionLeft.Text, Material, Design) And _
                    GetBase(BaseRStr, AdicionRStr, AsfericoRStr, "ST", Redondea(CilindroRight.Text), Redondea(EsferaRight.Text), AdicionRight.Text, Material, Design)) Then
                        If Not IsModifying And RxType = RxTypes.NormalRx Then
                            buscarlentes = MsgBox("Los datos no cumplen con las Especificaciones Est�ticas para un lente." & vbCrLf & "Deseas continuar con la busqueda de lentes?", MsgBoxStyle.YesNo + MsgBoxStyle.Question) = MsgBoxResult.Yes
                            IsModifying = False
                        End If
                    End If
                Else
                    If CheckLEye.Checked Then
                        If Not (GetBase(BaseLStr, AdicionLStr, AsfericoLStr, "ST", Redondea(CilindroLeft.Text), Redondea(EsferaLeft.Text), AdicionLeft.Text, Material, Design)) Then
                            If Not IsModifying And RxType = RxTypes.NormalRx Then
                                buscarlentes = MsgBox("Los datos no cumplen con las Especificaciones Est�ticas para un lente." & vbCrLf & "Deseas continuar con la busqueda de lentes?", MsgBoxStyle.YesNo + MsgBoxStyle.Question) = MsgBoxResult.Yes
                                IsModifying = False
                            End If
                        End If
                    End If
                    If CheckREye.Checked Then
                        If Not (GetBase(BaseRStr, AdicionRStr, AsfericoRStr, "ST", Redondea(CilindroRight.Text), Redondea(EsferaRight.Text), AdicionRight.Text, Material, Design)) Then
                            If Not IsModifying And RxType = RxTypes.NormalRx Then
                                buscarlentes = MsgBox("Los datos no cumplen con las Especificaciones Est�ticas para un lente." & vbCrLf & "Deseas continuar con la busqueda de lentes?", MsgBoxStyle.YesNo + MsgBoxStyle.Question) = MsgBoxResult.Yes
                                IsModifying = False
                            End If
                        End If
                    End If

                End If

                If buscarlentes Then
                    If BaseRStr < BaseLStr Then
                        Base = BaseRStr
                    Else
                        Base = BaseLStr
                    End If
                    MaterialDesignStr = "Material: " & MaterialStr & "  Dise�o: " & DesignStr

                    If (Material > 3 And Design <> 1 And Design <> 2 And Design <> 9 And Design <> 14 And Design <> 12 And Design <> 11 And Design <> 15 And Design <> 17) Or (Material < 4 And Design > 3 And Design <> 17) Then

                        If CheckREye.Checked Then
                            ds = t.SQLDS("select * from vwpartTRECEUX where material like '%" & MaterialStr & _
                                "%' and dise�o like '%" & DesignStr & _
                                "%' and (adicion = '" & (AdicionRStr) & _
                                "' and lado = 'R') and base = '" & (BaseRStr) & "'", "t3")
                            For Each rw As DataRow In ds.Tables("t3").Rows
                                ListRightParts.Items.Add(New ItemWithValue(rw("partdescription"), rw("partnum")))
                            Next
                        End If
                        If CheckLEye.Checked Then
                            ds = t.SQLDS("select * from vwpartTRECEUX where material like '%" & MaterialStr & _
                                "%' and dise�o like '%" & DesignStr & _
                                "%' and (adicion = '" & (AdicionLStr) & _
                                "' and lado = 'L') and base = '" & (BaseLStr) & "'", "t4")

                            For Each rw As DataRow In ds.Tables("t4").Rows
                                ListLeftParts.Items.Add(New ItemWithValue(rw("partdescription"), rw("partnum")))
                            Next
                        End If
                    Else
                        Dim desc As String = ""

                        If CheckLEye.Checked Then
                            desc = ""
                            If AsfericoLStr = "1" Then
                                desc = "ASFERICO"
                            ElseIf AsfericoRStr = "2" Then
                                desc = "DOBLE ASF"
                            End If
                            If DesignStr = "TA" Then
                                desc = "TRINITY " + desc
                            ElseIf DesignStr = "PHVS" Then
                                desc = ""
                            End If
                            ds = t.SQLDS("select * from vwpartTRECEUX where material = '" & MaterialStr & _
                                          "' and dise�o like '" & DesignStr & _
                                          "' and partdescription like '%" & desc & _
                                          "%' and (adicion = '" & (AdicionLStr) & _
                                          "' and lado = 'N') and base = '" & (BaseLStr) & "'", "t4")

                            For Each rw As DataRow In ds.Tables("t4").Rows
                                ListLeftParts.Items.Add(New ItemWithValue(rw("partdescription"), rw("partnum")))
                            Next
                        End If

                        If CheckREye.Checked Then
                            desc = ""
                            If AsfericoRStr = "1" Then
                                desc = "ASFERICO"
                            ElseIf AsfericoRStr = "2" Then
                                desc = "DOBLE ASF"
                            End If
                            If DesignStr = "TA" Then
                                desc = "TRINITY " + desc
                            End If
                            ds = t.SQLDS("select * from vwpartTRECEUX where material = '" & MaterialStr & _
                                                        "' and dise�o = '" & DesignStr & _
                                                        "' and partdescription like '%" & desc & _
                                                        "%' and (adicion = '" & (AdicionRStr) & _
                                                        "' and lado = 'N') and base = '" & (BaseRStr) & "'", "t4")

                            For Each rw As DataRow In ds.Tables("t4").Rows
                                ListRightParts.Items.Add(New ItemWithValue(rw("partdescription"), rw("partnum")))
                            Next
                        End If
                    End If
                End If
                SelectedLeftEye.Visible = False
                SelectedRightEye.Visible = False
                ListLeftParts.Visible = True
                ListRightParts.Visible = True
                ListLeftParts.Enabled = True
                ListRightParts.Enabled = True
                ClaseIDR = ""
                ClaseIDL = ""
                Dim NotFoundMessage As String = ""
                Dim x As ItemWithValue
                If CheckLEye.Checked Then
                    If ListLeftParts.Items.Count = 0 Then
                        If buscarlentes = True Then
                            NotFoundMessage &= "No se encontr� ningun lente izquierdo con estas caracteristicas." & vbCrLf
                        End If
                    Else
                        ListLeftParts.SelectedIndex = 0
                        x = ListLeftParts.SelectedItem
                        SystemsPickL = x.Value
                    End If
                End If
                If CheckREye.Checked Then
                    If ListRightParts.Items.Count = 0 Then
                        If buscarlentes = True Then
                            NotFoundMessage &= "No se encontr� ningun lente derecho con estas caracteristicas." & vbCrLf
                        End If
                    Else
                        ListRightParts.SelectedIndex = 0
                        x = ListRightParts.SelectedItem
                        SystemsPickR = x.Value
                    End If
                End If
                If NotFoundMessage <> "" Then
                    MsgBox(NotFoundMessage, MsgBoxStyle.Critical)
                Else
                    Preview.Enabled = True
                End If

            Catch ex As Exception
                MsgBox(ex.Message, MsgBoxStyle.Critical)

            Finally
                t.CloseConn()
                ListLeftParts.EndUpdate()
                ListRightParts.EndUpdate()
            End Try
            RxSave.Enabled = True
        End If
    End Sub

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
            Case "MainServer"
                'Result = CStr(key.GetValue("MainServer"))
                Result = My.Settings.VantageServer
            Case "DSN"
                'Result = CStr(key.GetValue("DSNGuiador"))
                Result = My.Settings.DSNGuiador
            Case "QAEnabled"
                'Result = CStr(key.GetValue("QAEnabled"))
                Result = My.Settings.QAEnabled
            Case "InventoryON"
                'Result = CStr(key.GetValue("InventoryON"))
                Result = My.Settings.InventoryON
            Case "OMAEnabled"
                'Result = CStr(key.GetValue("OMAEnabled"))
                Result = My.Settings.OMAEnabled
            Case "DVIEnabled"
                'Result = CStr(key.GetValue("DVIEnabled"))
                Result = My.Settings.DVIEnabled
        End Select
        Return Result
    End Function


    Private Sub Form1_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'WriteOMAFile()
        My.Application.ChangeUICulture("en-US")
        My.Application.ChangeCulture("en-US")

        Dim GetLocalCustomers As Boolean = False
        Application.DoEvents()
        Dim TerminateApplication As Boolean = False
        Dim LocalServerOK As Boolean = False
        Dim t As SqlDB
        Dim ds As New DataSet



        t = New SqlDB(My.Settings.LocalServer, My.Settings.DBUser, My.Settings.DBPassword, My.Settings.LocalDBName)



        Select Case My.Settings.WorkingMode
            Case "ONLINE" : ComboWorkingMode.Text = "CONECTADO"
            Case "OFFLINE" : ComboWorkingMode.Text = "DESCONECTADO"
        End Select



        Try
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

            t.OpenConn()

            '************************
            'asignamos el nombre del servidor a la variable global
            ServerAdd = My.Settings.VantageServer
            '************************
            If RadioDigitalNew.Checked Then
                ComboArmazon2.ComboBox1.Items.Clear()
                ComboArmazon2.ComboBox1.Items.Add("Metal")
                ComboArmazon2.ComboBox1.Items.Add("Plastico")
                ComboArmazon2.ComboBox1.Items.Add("Ranurado")
                ComboArmazon2.ComboBox1.Items.Add("Perforado")
                ComboArmazon2.Bind()
                ComboArmazon2.Texto = "Metal"
            End If
            DateTag.Text = Now.Date
            ds = t.SQLDS("select * from VwMaterialDesign where cl_diseno < 4 order by cl_mat", "t1")
            ds.Tables("t1").Rows.Add("0", 0, 0, "")
            ComboMonofocal.DataSource = ds.Tables("t1")
            ComboMonofocal.DisplayMember = "Description"
            ComboMonofocal.ValueMember = "MatDesID"
            ComboMonofocal.SelectedValue = "0"

            ds = t.SQLDS("select * from VwMaterialDesign where cl_diseno = 4 order by cl_mat", "t1")
            ds.Tables("t1").Rows.Add("0", 0, 0, "")
            ComboBifocal.DataSource = ds.Tables("t1")
            ComboBifocal.DisplayMember = "Description"
            ComboBifocal.ValueMember = "MatDesID"
            ComboBifocal.SelectedValue = "0"

            ds = t.SQLDS("select * from VwMaterialDesign where (cl_diseno > 4) and (cl_diseno < 8) order by cl_mat", "t1")
            ds.Tables("t1").Rows.Add("0", 0, 0, "")
            ComboProgresivo.DataSource = ds.Tables("t1")
            ComboProgresivo.DisplayMember = "Description"
            ComboProgresivo.ValueMember = "MatDesID"
            ComboProgresivo.SelectedValue = "0"

            ds = t.SQLDS("select * from VwMaterialDesign where cl_diseno > 7 order by cl_mat", "t1")
            ds.Tables("t1").Rows.Add("0", 0, 0, "")
            ComboOtros.DataSource = ds.Tables("t1")
            ComboOtros.DisplayMember = "Description"
            ComboOtros.ValueMember = "MatDesID"
            ComboOtros.SelectedValue = "0"

            ds = t.SQLDS("select * from TblColors order by Color", "t1")
            ComboTinte.ComboBox1.DataSource = ds.Tables("t1")
            ComboTinte.ComboBox1.DisplayMember = "color"
            ComboTinte.ComboBox1.ValueMember = "cl_color"
            ComboTinte.ComboBox1.SelectedIndex = 0

            LocalServerOK = True

            If My.Settings.WorkingMode.ToUpper = "OFFLINE" Then
                Me.ChangeWorkingStatus(WorkingStatusType.OffLine)
            End If

            If Not Actualiza_Clientes() Then
                'SI ES VERDADERO ENTONCES SE ACTUALIZARON LOS CLIENTES DESDE VANTAGE SIN NINGUN PROBLEMA
                'SI NO EXTRAE LOS CLIENTES DE LA BASE DE DATOS LOCAL
                'If GetLocalCustomers Then

                MsgBox("Error al obtener la lista de clientes.", MsgBoxStyle.Critical)


                'Dim str As String = "select custid + ' ' + name AS name, custnum, name AS Expr1 from customer where number01 = " & LabID() & " or number02 = " & LabID() & " order by name"
                ''Dim str As String = "select name, custnum, name AS Expr1 from customer where number01 = " & LabID() & " or number02 = " & LabID() & " order by name"
                'ds = t.SQLDS(str, "t1")
                'ComboClients.DataSource = ds.Tables("t1")
                'ComboClients.DisplayMember = "name"
                'ComboClients.ValueMember = "custnum"
                'ComboClients.SelectedIndex = 0

                'ds = t.SQLDS("select * from ErrorRel order by description", "t1")
                'ComboErrores.DataSource = ds.Tables("t1")
                'ComboErrores.DisplayMember = "description"
                'ComboErrores.ValueMember = "errorid"
                'ComboErrores.SelectedValue = 27
                'Else

                '    Dim n As New SqlDB(Read_Registry("MainServer"), My.Settings.DBUser, My.Settings.DBPassword, My.Settings.ERPDBName)
                '    Try
                '        n.OpenConn()
                '        ds = n.SQLDS("select * from ErrorRel order by description", "t1")
                '        ComboErrores.DataSource = ds.Tables("t1")
                '        ComboErrores.DisplayMember = "description"
                '        ComboErrores.ValueMember = "errorid"
                '        ComboErrores.SelectedValue = 27
                '    Catch ex As Exception
                '        If ex.Message.Contains("Error de Conexion") Then
                '            ChangeWorkingStatus(WorkingStatusType.OffLine)
                '        Else
                '            MsgBox(ex.Message, MsgBoxStyle.Critical)
                '        End If
                '    Finally
                '        n.CloseConn()
                '    End Try
            End If



            'ComboTintes.DataSource = ds.Tables("t1")
            'ComboTintes.DisplayMember = "color"
            'ComboTintes.ValueMember = "cl_color"
            'ComboTintes.SelectedIndex = 0

            InitValues()

            Me.Panel1.Controls.Add(pe)
            Panel1.Size = pe.Size
            pe.Location = New Point(0, 0)
            pe.BringToFront()
            pe.Refresh()
            's.Close()
            'RxID.Select()
        Catch ex As Exception

            If Not LocalServerOK Then
                TerminateApplication = True
                MsgBox("Error de Conexion con el Servidor Local, sin conexion local no se puede continuar la ejecucion del programa." & vbCrLf & ex.Message, MsgBoxStyle.Critical)
            Else
                Me.ChangeWorkingStatus(WorkingStatusType.OffLine)
            End If

        Finally
            '****** Mod By Marco Angulo 04/07/2006 ************
            'desplgamos la descripcion de la planta en el nombre del forma con tiempo max de 5 seg, para no detener la ejec. del programa
            Me.Text = "Captura de Recetas - " & PlantDescription()
            LabelVersion.Text = System.String.Format(LabelVersion.Text, My.Application.Info.Version.Major, My.Application.Info.Version.Minor, My.Application.Info.Version.Build, My.Application.Info.Version.Revision)
            LabelVersion.Location = New Point(Me.Width - LabelVersion.Width - 15, LabelVersion.Location.Y)

            ' BANDERA_OPTIMIZADO [ESTAS 3 LINEAS NO ESTABAN ANTES]
            If Not My.Settings.Optimizar Then
                CheckOptim.Enabled = False
            End If

            t.CloseConn()
            If TerminateApplication Then
                End
            End If
            InitStatus = InitStatusType.Working

            pe.ActualizaProcesos_Click(Me, e)

        End Try
    End Sub
    Sub SetBitacora(ByRef log As BitacoraLabRx, ByVal t As SqlDB)
        'Dim CanjePuntos As Boolean
        'Dim Garantia As Boolean
        'Dim Retenida As Boolean
        'Dim EnvioVirtual As Boolean
        'Dim BajarDeVirtual As Boolean
        'Dim TerminaSL As Boolean
        'Dim TerminaAR As Boolean
        'Dim TerminaFL As Boolean
        'Dim LiberaQA As Boolean
        'Dim RecepcionLocal As Boolean
        'Dim Reproceso As Boolean

        Dim SQLStr As String = "select openorder,coalesce(ordernum,0),coalesce(date02,'1/1/2000'),coalesce(date03,'1/1/2000'),coalesce(date04,'1/1/2000'), " & _
                "coalesce(date05,'1/1/2000'),coalesce(date06,'1/1/2000'),coalesce(date07,'1/1/2000'),coalesce(date08,'1/1/2000'), " & _
                "coalesce(date09,'1/1/2000'),coalesce(date11,'1/1/2000'),coalesce(userdate1,'1/1/2000'),coalesce(userdate2,'1/1/2000'), " & _
                "coalesce(checkbox06,0),coalesce(checkbox07,0),coalesce(checkbox08,0),coalesce(checkbox09,0),coalesce(checkbox10,0),coalesce(checkbox11,0), " & _
                "coalesce(checkbox12,0),coalesce(checkbox16,0),coalesce(checkbox17,0),coalesce(checkbox18,0),coalesce(shortchar01,''), " & _
                "coalesce(entryperson,''),coalesce(changedby,''),coalesce(ccamount,0.0) from orderhed with(nolock) where ordernum = " & log.Ordernum
        Dim ds As DataSet
        ds = t.SQLDS(SQLStr, "t1")

    End Sub

    Function CheckCurrentStatus(ByVal Table As String, ByVal Field As String, ByVal FilterField As String, ByVal FilterValue As String) As String
        Dim Result As String = ""
        Dim Failed As Boolean = False
        Dim FailedMessage As String = ""
        Dim t As New SqlDB()
        If IsReceivingVirtualRx Then
            t.SQLConn = New SqlConnection(My.Settings.MFGSysConnection)
        Else
            t.SQLConn = New SqlConnection(Laboratorios.ConnStr)
        End If
        Try
            t.OpenConn()
            Dim sqlstr As String = "select " & Field & " from " & Table & " with(nolock) where " & FilterField & " = " & FilterValue
            Dim ds As New DataSet
            ds = t.SQLDS(sqlstr, "t1")
            If ds.Tables("t1").Rows.Count > 0 Then
                Result = ds.Tables("t1").Rows(0).Item(Field)
            End If
        Catch ex As Exception
            Failed = True
            FailedMessage = ex.Message
            Result = "FAILED"
        Finally
            t.CloseConn()
            If Failed Then
                MsgBox(FailedMessage, MsgBoxStyle.Exclamation)
            End If
        End Try
        Return Result
    End Function
    Function CheckCurrentStatus(ByVal Table As String, ByVal Field As String, ByVal FieldName As String, ByVal FilterField As String, ByVal FilterValue As String) As String
        Dim Result As String = ""
        Dim Failed As Boolean = False
        Dim FailedMessage As String = ""
        Dim t As New SqlDB()
        t.SQLConn = New SqlConnection(Laboratorios.ConnStr)
        Try
            t.OpenConn()
            Dim sqlstr As String = "select " & Field & " from " & Table & " with(nolock) where " & FilterField & " = " & FilterValue
            Dim ds As New DataSet
            ds = t.SQLDS(sqlstr, "t1")
            If ds.Tables("t1").Rows.Count > 0 Then
                Result = ds.Tables("t1").Rows(0).Item(FieldName)
            End If
        Catch ex As Exception
            Failed = True
            FailedMessage = ex.Message
            Result = "FAILED"
        Finally
            t.CloseConn()
            If Failed Then
                MsgBox(FailedMessage, MsgBoxStyle.Exclamation)
            End If
        End Try
        Return Result
    End Function

    Private Function IsVBLab(ByVal LabID As Integer) As Boolean
        Dim Result As Boolean = False
        Dim Failed As Boolean = False
        Dim FailedMessage As String = ""
        Dim t As New SqlDB(Laboratorios.ConnStr)

        Try
            t.OpenConn()
            Dim sqlstr As String = "select IsVB_Lab from TblLaboratorios with(nolock) where cl_lab = " & LabID
            Dim ds As New DataSet
            ds = t.SQLDS(sqlstr, "t1")
            If ds.Tables("t1").Rows.Count > 0 Then
                Result = ds.Tables("t1").Rows(0).Item("IsVB_Lab")
            End If
        Catch ex As Exception
            Failed = True
            FailedMessage = ex.Message
            Result = False
        Finally
            t.CloseConn()
            If Failed Then
                MsgBox(FailedMessage, MsgBoxStyle.Exclamation)
            End If
        End Try
        Return Result

    End Function

    Private Sub UpdateOrder(ByVal OrderNum As Integer, ByVal Type As UpdateType, ByVal ProcessLocal As Boolean)
        Dim Failed As Boolean = False
        Dim Message As String = ""
        Dim AR As Integer = 0
        Dim Tono As Integer = 1
        Dim Color As Integer = 0
        Dim Material As Integer
        Dim Dise�o As Integer
        Dim ErrorQty As Integer = 0
        Dim ExecuteConsumePastilla As Boolean = False
        Dim Comentarios_Param As String
        Dim LabNum As Integer = LabID()

        ' VARIABLE PARA DETERMINAR SI ES NECESARIO CONSUMIR PASTILLAS AL HACER UN CAMBIO DE VIRTUAL-->LOCAL
        Dim YaNoConsumir As Boolean = False
        '***********************************

        'Dim TranMfgSys As SqlTransaction
        'Dim TranERPMaster As SqlTransaction
        Dim TranLocal As SqlTransaction
        Dim trantt As SqlTransaction = Nothing

        GetMaterialDesign(Material, Dise�o)
        If CheckAR.Checked Then
            AR = 1
        ElseIf CheckARGold.Checked Then
            AR = 2
        ElseIf CheckARMatiz.Checked Then
            AR = 3
        End If
        If ComboTinte.SelectedValue <> 0 Then
            '            If CheckTintes.Checked Then
            If CheckLevel1.Checked Then
                Tono = 1
            ElseIf CheckLevel2.Checked Then
                Tono = 2
            ElseIf CheckLevel3.Checked Then
                Tono = 3
            End If
            Color = ComboTinte.SelectedValue
        End If
        'If CheckTintes.Checked Then
        'End If
        Dim ARFunction As Integer = 0
        If IsReceivingVirtualRx Then
            ARFunction = 3
        ElseIf IsLocalReceive Then
            ARFunction = 4
        ElseIf (ARLab <> LabNum) Or (CInt(OrderNum.ToString.Substring(0, 2) = ARLab)) Then
            ARFunction = 1
            If (ComboErrores.SelectedValue <> 27) Or (PanelModInfo.Visible) Then
                ARFunction = 5
            End If
        Else
            ARFunction = 2
        End If

        'Dim Conn As New SqlDB()
        'Dim ConnERP As New SqlDB


        ' *****************************************************************
        ' * Se comento esta seccion porque ya se trabaja siempre en linea *
        ' *****************************************************************
        'If WorkingOnLine And Not ProcessLocal Then
        '    Conn = New SqlDB(ChooseServer, My.Settings.DBUser, My.Settings.DBPassword, DataBase)
        '    If DataBase = My.Settings.MfgSysDBName Then
        '        ConnERP = New SqlDB(ServerAdd, My.Settings.DBUser, My.Settings.DBPassword, My.Settings.ERPDBName)
        '    Else
        '        ConnERP = New SqlDB(ServerAdd, My.Settings.DBUser, My.Settings.DBPassword, DataBase)
        '    End If
        'Else
        '    Conn = New SqlDB(My.Settings.LocalServer, My.Settings.DBUser, My.Settings.DBPassword, My.Settings.LocalDBName)
        '    ConnERP = New SqlDB(My.Settings.LocalServer, My.Settings.DBUser, My.Settings.DBPassword, My.Settings.LocalDBName)
        'End If

        Dim ConnLocal As New SqlDB(My.Settings.LocalServer, My.Settings.DBUser, My.Settings.DBPassword, My.Settings.LocalDBName)

        'Conn.OpenConn()
        ConnLocal.OpenConn()
        'ConnERP.OpenConn()

        'TranMfgSys = Conn.SQLConn.BeginTransaction
        TranLocal = ConnLocal.SQLConn.BeginTransaction
        'TranERPMaster = ConnERP.SQLConn.BeginTransaction

        Try


            Dim t As New SqlDB()
            t.SQLConn = New SqlConnection(Laboratorios.ConnStr)
            Try
                t.OpenConn()
                Dim ds As DataSet
                ds = t.SQLDS("select distinct upper(b.descripcion) as label,b.showinrxlabel as isspeciallabel from dbo.tblpaquetesdtl a with(nolock) inner join tblpaquetescadenas b with(nolock) on a.idpaquete = b.idpaquete where a.idpaqueteitem = " & NumArmazon, "t1")
                If ds.Tables("t1").Rows.Count > 0 Then
                    If ds.Tables("t1").Rows(0).Item("isspeciallabel") = True Then
                        HasSpecialLabel = True
                        SpecialLabel = ds.Tables("t1").Rows(0).Item("label")
                    End If
                End If
            Catch ex As Exception
                Throw New Exception(ex.Message)
            Finally
                t.CloseConn()
            End Try



            Dim Comm As New SqlCommand("SP_UpdateCustomOrderHed_V4", ConnLocal.SQLConn)
            Comm.CommandTimeout = My.Settings.DBCommandTimeout
            Comm.Transaction = TranLocal
            Comm.CommandType = Data.CommandType.StoredProcedure
            Comm.Parameters.AddWithValue("@Company", "TRECEUX")
            Comm.Parameters.AddWithValue("@OrderNum", OrderNum)
            'POR SI SE MODIFICA EL CLIENTE DE LA ORDEN SE ACTUALIZA TAMBIEN
            If IsReceivingVirtualRx Then
                Comm.Parameters.AddWithValue("@custnum", OriginalCustNum)
            Else
                If Not Convert.ToString(OrderNum).StartsWith(LabNum) Then '  ComboClients.SelectedValue = Nothing Then
                    ComboClients.SelectedIndex = -1
                    Comm.Parameters.AddWithValue("@custnum", OriginalCustNum)
                Else
                    Comm.Parameters.AddWithValue("@custnum", ComboClients.SelectedValue)
                End If
            End If
            '*********************************************************************

            Comm.Parameters.AddWithValue("@OrdenLab", RxID.Text)
            Comm.Parameters.AddWithValue("@RefDigital", Val(RxWECO.Text))
            Comm.Parameters.AddWithValue("@TrazadoArmazon", Convert.ToByte(RadioDigitalNew.Checked))
            Comm.Parameters.AddWithValue("@TipoArmazon", ComboArmazon2.ComboBox1.SelectedIndex + 1)
            Comm.Parameters.AddWithValue("@Biselado", Convert.ToByte(CheckBiselado.Checked))
            Comm.Parameters.AddWithValue("@Antireflejante", AR)
            Comm.Parameters.AddWithValue("@Color", Color)
            Comm.Parameters.AddWithValue("@Gradiente", Convert.ToByte(RadioGradiente.Checked))
            Comm.Parameters.AddWithValue("@Tono", Tono)
            Comm.Parameters.AddWithValue("@EsferaR", CDbl(EsferaRight.Text))
            Comm.Parameters.AddWithValue("@CilindroR", CDbl(CilindroRight.Text))
            Comm.Parameters.AddWithValue("@EjeR", CDbl(EjeRight.Text))
            Comm.Parameters.AddWithValue("@AdicionR", CDbl(AdicionRight.Text))
            Comm.Parameters.AddWithValue("@EsferaL", CDbl(EsferaLeft.Text))
            Comm.Parameters.AddWithValue("@CilindroL", CDbl(CilindroLeft.Text))
            Comm.Parameters.AddWithValue("@EjeL", CDbl(EjeLeft.Text))
            Comm.Parameters.AddWithValue("@AdicionL", CDbl(AdicionLeft.Text))
            Comm.Parameters.AddWithValue("@AlturaR", CDbl(AlturaRight.Text))
            Comm.Parameters.AddWithValue("@AlturaL", CDbl(AlturaLeft.Text))
            Comm.Parameters.AddWithValue("@MonoR", CDbl(MonoRight.Text))
            Comm.Parameters.AddWithValue("@MonoL", CDbl(MonoLeft.Text))
            Comm.Parameters.AddWithValue("@DIPle", CDbl(DIPLejos.Text))
            Comm.Parameters.AddWithValue("@DIPce", CDbl(DIPCerca.Text))
            Comm.Parameters.AddWithValue("@Altura", CDbl(Altura.Text))
            Try
                Comm.Parameters.AddWithValue("@A", CDbl(ContornoA.Text))
                Comm.Parameters.AddWithValue("@B", CDbl(ContornoB.Text))
                Comm.Parameters.AddWithValue("@ED", CDbl(ContornoED.Text))
            Catch ex As Exception
                Comm.Parameters.AddWithValue("@A", 0)
                Comm.Parameters.AddWithValue("@B", 0)
                Comm.Parameters.AddWithValue("@ED", 0)
            End Try
            Comm.Parameters.AddWithValue("@Puente", CDbl(ContornoPuente.Text))
            Comm.Parameters.AddWithValue("@Material", Material)
            Comm.Parameters.AddWithValue("@Diseno", Dise�o)
            Comm.Parameters.AddWithValue("@Status", 0)
            Comm.Parameters.AddWithValue("@Foco", Convert.ToByte(PanelOblea.Enabled))
            Comm.Parameters.AddWithValue("@ArmazonID", NumArmazon)
            Comm.Parameters.AddWithValue("@Coated", Convert.ToByte(CheckRLX.Checked))
            Comm.Parameters.AddWithValue("@Abrillantado", Convert.ToByte(CheckAbril.Checked))

            ' BANDERA_OPTIMIZAR
            'If SpecialDesignID = 1 Then
            '    Comm.Parameters.AddWithValue("@Optimizado", True)
            'Else
            '    Comm.Parameters.AddWithValue("@Optimizado", False)
            'End If

            If SD_Partnum.ToUpper() = "HDRX" Then
                Comm.Parameters.AddWithValue("@Optimizado", True)
            Else
                Comm.Parameters.AddWithValue("@Optimizado", False)
            End If

            Comm.Parameters.AddWithValue("@PrismaR", PrismaR)
            Comm.Parameters.AddWithValue("@EjePrismaR", EjePrismaR)
            Comm.Parameters.AddWithValue("@PrismaL", PrismaL)
            Comm.Parameters.AddWithValue("@EjePrismaL", EjePrismaL)
            Comm.Parameters.AddWithValue("@CurrentLab", LabNum)
            Comm.Parameters.AddWithValue("@ARLab", ARLab)
            Comm.Parameters.AddWithValue("@OriginalRefDigital", OriginalRefDigital)
            Comm.Parameters.AddWithValue("@ARFunction", ARFunction)

            Comm.Parameters.AddWithValue("@CostcoLente", CostcoLente)
            ' Esta campo ya no se usa para Costco y se tomara para los dise�os especiales [character07 de orderhed]
            Comm.Parameters.AddWithValue("@CostcoAR", SpecialDesignID)

            Comm.Parameters.AddWithValue("@CostcoTinte", CostcoTinte)
            Comm.Parameters.AddWithValue("@CostcoPulido", CostcoAbrillantado)
            Comm.Parameters.AddWithValue("@SeqID", SeqID)
            Comm.Parameters.AddWithValue("@FechaEntradaWeb", FehcaEntradaWeb)
            Comm.Parameters.AddWithValue("@FechaEntradaFisica", FechaEntradaFisica)
            Comentarios_Param = TextComentarios.Text
            If RxType = RxTypes.WebRx Then
                Comentarios_Param = Comentarios_Param & "[WEB_RX]"
            End If
            If IsGratisRx Then
                Comentarios_Param = Comentarios_Param & "[FREE_RX]"
            End If
            If IsGarantia Or CheckGarantia.Checked Then
                Comentarios_Param = Comentarios_Param & "[GARANTIA_RX]"
            End If

            If ARFunction = 3 Then              ' Falta checar que guarde el status actual de las banderas de los ojos
                If CheckREye.Checked Then
                    Comentarios_Param = Comentarios_Param & "[HAS_R_EYE]"
                End If
                If CheckLEye.Checked Then
                    Comentarios_Param = Comentarios_Param & "[HAS_L_EYE]"
                End If
            Else
                If RightRequested Then
                    Comentarios_Param = Comentarios_Param & "[HAS_R_EYE]"
                End If
                If LeftRequested Then
                    Comentarios_Param = Comentarios_Param & "[HAS_L_EYE]"
                End If
            End If
            Comm.Parameters.AddWithValue("@Comentarios", Comentarios_Param)

            If My.Settings.LocalDBName = ConnLocal.SQLConn.Database Then
                Comm.Parameters.AddWithValue("@Sincronizada", SavedInRemoteServer)
            End If

            ' PARAMETROS DE FECHAS DE PROCESOS DE VIRTUAL Y QA
            Comm.Parameters.AddWithValue("@ReceivedARDate", date02)
            Comm.Parameters.AddWithValue("@date03", date03)
            Comm.Parameters.AddWithValue("@date04", date04)
            Comm.Parameters.AddWithValue("@LocalReceiveDate", date05)
            Comm.Parameters.AddWithValue("@date06", date06)
            Comm.Parameters.AddWithValue("@date07", date07)

            ' PARAMETROS DE INDICADORES DE TRABAJOS VIRTUALES
            Comm.Parameters.AddWithValue("@ReceivedAR", checkbox10)
            Comm.Parameters.AddWithValue("@OutARLab", checkbox11)
            Comm.Parameters.AddWithValue("@LocalReceive", checkbox12)
            Comm.Parameters.AddWithValue("@Reproceso", checkbox13)
            Comm.Parameters.AddWithValue("@FinishedQA", checkbox16)

            ' No funciona correctamente este parametro porque se guarda en el campo satpickup de orderhed pero es de tipo bit y no tipo
            ' tinyint como suponia RL. Se quitara eventualmente este campo.
            Comm.Parameters.AddWithValue("@SpecialDesignID", SpecialDesignID)

            '--@ReceivedAR	=	checkbox10
            '--@OutARLab	=	checkbox11
            '--@LocalReceive =	checkbox12
            '--@Reproceso	=	checkbox13
            '--@FinishedQA	=	checkbox16

            Comm.Parameters.Add("@Msg", Data.SqlDbType.VarChar, 100)
            Comm.Parameters("@Msg").Direction = Data.ParameterDirection.Output
            Dim Mensaje As String = Comm.Parameters("@Msg").Value
            Try

                If My.Settings.EnableBitacora Then
                    '                    Dim log As New BitacoraLabRx(OrderNum, RxID.Text, My.User.Name)
                    Dim log As New BitacoraLabRx(OrderNum, RxID.Text, My.User.Name.Replace("AUGENOPTICOS\", ""))

                    Try
                        ' Se comento la garantia porque esta solo puede capturarse al generar la rx
                        'If IsGarantia <> Me.CheckCurrentStatus("orderhed", "checkbox18", "ordernum", OrderNum) Then log.AddGarantia(Now.ToString, "")

                        'If Not (PanelModInfo.Visible Or ComboErrores.SelectedValue <> 27) Then
                        '    log.AddReproceso(Now.ToString(), "prueba")
                        'End If
                        If ARFunction < 2 And ARLab <> LabID() Then
                            Dim CurrentAR As Boolean = CheckCurrentStatus("orderhed", "checkbox09", "ordernum", OrderNum)
                            Dim CurrentARLab As String = CheckCurrentStatus("orderhed", "coalesce(cast(character01 as varchar(5)),0) as character01", "character01", "ordernum", OrderNum)
                            If ((CurrentAR = False) Or (CurrentAR = True And CurrentARLab <> ARLab)) And ARLab > 0 Then
                                log.AddEnvioVirtual(Now.ToString, "Lab Remoto: " & ARLab)
                            End If
                        End If

                        If ARFunction = 3 Then log.AddBajarDeVirtual(Now.ToString, "")
                        If ARFunction = 4 Then log.AddRecepcionLocal(Now.ToString, "")
                        If ARFunction = 5 Then log.AddReproceso(Now.ToString, "")
                        'log.SetBitacora(ConnLocal, TranLocal)

                        If Not log.InsertBitacora(ConnLocal, TranLocal) Then
                            Throw New Exception("[BITACORA]: " & log.FailedString)
                        End If
                    Catch ex As Exception
                        Throw New Exception(ex.Message)
                    End Try
                End If

                Comm.ExecuteNonQuery()
                Mensaje = Comm.Parameters("@Msg").Value
                If Mensaje <> "OK" Then
                    Throw New Exception(Mensaje)
                End If

            Catch ex As Exception
                Mensaje = "FAILED"
                Throw New Exception("Error al Ejecutar Stored Procedure de Modificacion: " & vbCrLf & ex.Message)
            End Try


            'generamos registro de calidad para la modificacion
            'si el sistema tiene habilitado el registro de QA
            'If Read_Registry("QAEnabled") Then
            If My.Settings.QAEnabled Then
                ' Se graba registro solo si no es Recaptura SP
                If ((ComboErrores.SelectedValue <> 27) Or (PanelModInfo.Visible)) Or IsReceivingVirtualRx Then
                    RegistroQA(OrderNum, True, ConnLocal.SQLConn, TranLocal)
                Else
                    '------------------------------------------------------------------------------------------------------------------
                    ' Aqui verifico si tiene Registro de QA, si no lo tiene, mando grabarlo para que aparezca en la estacion de calidad
                    '------------------------------------------------------------------------------------------------------------------
                    If Not HasQARegistry(RxNum.Text) Then
                        RegistroQA(OrderNum, True, ConnLocal.SQLConn, TranLocal)
                    End If
                    '------------------------------------------------------------------------------------------------------------------
                End If
            End If



            If (Mensaje = "OK") And (Type = UpdateType.OrderAndLines) Then
                Dim LP As String = ""
                Dim RP As String = ""
                Dim Parametros As String = ""

                Try
                    Dim temp As ItemWithValue = ListLeftParts.SelectedItem
                    If CheckLEye.Checked Then
                        LP = temp.Value
                    ElseIf OriginalLeftPart <> "" Then
                        LP = OriginalLeftPart
                    End If
                    If CheckREye.Checked Then
                        temp = ListRightParts.SelectedItem
                        RP = temp.Value
                    ElseIf OriginalRightPart <> "" Then
                        RP = OriginalRightPart
                    End If

                    Dim ds As DataSet

                    If ChangingTraces And (CInt(ComboTrazos.ComboBox1.SelectedValue) <> OriginalRefDigital) Then
                        ConnLocal.Transaction("update " & TracerTable & " set status = 0 where jobnum = '" & ComboTrazos.ComboBox1.SelectedValue & "'", TranLocal)
                        ConnLocal.Transaction("update " & TracerTable & " set status = 1 where jobnum = '" & OriginalRefDigital & "'", TranLocal)
                    End If

                    If (CheckLEye.Checked) Or (OriginalLeftPart <> "") Then
                        Parametros &= LP & ",1,"
                        ds = ConnLocal.SQLDS("select Clase from VwPartTreceux with(nolock) where partnum = '" & LP & "'", "t1", TranLocal)
                        If ds.Tables("t1").Rows(0).Item("Clase") = "T" Then
                            Parametros &= "2"
                        ElseIf ds.Tables("t1").Rows(0).Item("Clase") = "S" Then
                            Parametros &= "0"
                        End If
                    End If

                    If (CheckREye.Checked) Or (OriginalRightPart <> "") Then
                        If Parametros <> "" Then
                            Parametros &= ","
                        End If
                        ds = ConnLocal.SQLDS("select Clase from VwPartTreceux with(nolock) where partnum = '" & RP & "'", "t1", TranLocal)
                        If ds.Tables("t1").Rows(0).Item("Clase") = "T" Then
                            Parametros &= RP & ",1,3"
                        ElseIf ds.Tables("t1").Rows(0).Item("Clase") = "S" Then
                            Parametros &= RP & ",1,1"
                        End If
                    End If

                    Pedido = Parametros
                    AddOpciones(True)
                    Parametros = Pedido
                    PedidoUpdate = Pedido

                Catch ex As Exception
                    Throw New Exception("Error al obtener datos de las pastillas")
                End Try

                Dim OrderStr As String = OrderNum.ToString

                ExecuteConsumePastilla = False
                Dim RxNum As Integer = RxWECO.Text

                ' -------------- ESTA SECCION ESTA SIENDO ACTUALIZADA PARA CONSUMIR INVENTARIOS DE MANERA LOCAL --------------------
                '*** dtls: dentro de este modulo sabemos que se esta modificando la receta, entonces verificamos
                '***       si la receta cambio de estado de local-->virtual (regresar/inventario) o de virtual-->local(consumir/inventario)

                ' Funcion logica para consumir/inventario (virtual--->>local)
                If Not IsVirtualRx Then
                    If IsLocalRx Then
                        If Not (((ComboErrores.SelectedValue <> My.Settings.RecapturaSP_ID) And (ComboErrores.SelectedValue <> My.Settings.FrameError_ID)) Or PanelModInfo.Visible) Then
                            If (Original_IsVirtualRx) And (CInt(GetLabNum()) <> Original_ARLab) Then
                                ' Al cambiar una rx de virtual--->local ya no es necesario consumir otra vez al realizar la actualizacion de la rx
                                YaNoConsumir = True

                                Dim PartRightStr As ItemWithValue
                                Dim PartLeftStr As ItemWithValue
                                'checamos si son ambos ojos o solo uno
                                PartLeftStr = ListLeftParts.SelectedItem
                                PartRightStr = ListRightParts.SelectedItem

                                ' Se ejecuta el procedimiento para consumir pastillas
                                Comm = New SqlCommand("SP_ConsumePastillasTRECEUX", ConnLocal.SQLConn)
                                Comm.Transaction = TranLocal
                                Comm.CommandType = CommandType.StoredProcedure
                                Comm.CommandTimeout = My.Settings.DBCommandTimeout
                                Comm.Parameters.AddWithValue("Ordernum", OrderNum)
                                Comm.Parameters.AddWithValue("OrdenLab", RxID.Text)
                                Comm.Parameters.AddWithValue("RxNum", Val(RxWECO.Text))
                                Comm.Parameters.AddWithValue("TranReference", "Cons. Pastilla Cambio Virtual->Local")

                                If CheckLEye.Checked Then
                                    Comm.Parameters.AddWithValue("Izquierdo", PartLeftStr.Value)

                                    Dim OrderDate As DateTime = CDate(FechaDeCreacion)

                                    If OrderDate.DayOfYear <> Now.DayOfYear Then
                                        ' Ejecutamos la funcion para realizar el consumo del cambio de virtual a local
                                        RegConsumoAtrasado(FechaDeCreacion, PartLeftStr.Value, OrderNum.ToString.Substring(0, 2), "", TranLocal, ConnLocal.SQLConn)
                                    End If

                                Else
                                    Comm.Parameters.AddWithValue("Izquierdo", "")
                                End If

                                If CheckREye.Checked Then
                                    Comm.Parameters.AddWithValue("Derecho", PartRightStr.Value)

                                    Dim OrderDate As DateTime = CDate(FechaDeCreacion)

                                    If OrderDate.DayOfYear <> Now.DayOfYear Then
                                        ' Ejecutamos la funcion para realizar el consumo del cambio de virtual a local
                                        RegConsumoAtrasado(FechaDeCreacion, PartRightStr.Value, OrderNum.ToString.Substring(0, 2), "", TranLocal, ConnLocal.SQLConn)
                                    End If

                                Else
                                    Comm.Parameters.AddWithValue("Derecho", "")
                                End If

                                Comm.Parameters.AddWithValue("plant", Read_Registry("Plant"))
                                Comm.Parameters.AddWithValue("EntryPerson", "Lab: " & LabNum)
                                Comm.Parameters.Add("@Mensaje", Data.SqlDbType.VarChar, 30)
                                Comm.Parameters("@Mensaje").Direction = Data.ParameterDirection.Output
                                Comm.ExecuteNonQuery()
                            End If
                        End If
                    End If
                End If

                ' Funcion logica para regresar/inventario (local--->>virtual)
                If IsVirtualRx Then
                    If Not IsReceivingVirtualRx Then
                        ' ------------------------------------
                        ' Codigo para decidir donde se grabara el trazo virtual y madnarlo grabar
                        ' ------------------------------------
                        Dim Location As LabLocation
                        If ConnLocal.svr = My.Settings.VantageServer Then
                            Location = LabLocation.Virtual
                        Else
                            Location = LabLocation.Local
                        End If
                        'If OrderNum.ToString.StartsWith() Then
                        Me.SaveVirtualTraces(ConnLocal, TranLocal, Location, VirtualRxAction.Create, OrderNum)
                        'End If
                        ' ------------------------------------

                        If Not (ComboErrores.SelectedValue <> My.Settings.RecapturaSP_ID Or PanelModInfo.Visible) Then
                            If Original_IsLocalRx Then
                                Dim dsWhse As DataSet
                                If (OriginalRightPart <> "") And (CInt(GetLabNum()) <> ARLab) Then

                                    dsWhse = ConnLocal.SQLDS("SELECT primbin,warehousecode from plantwhse WITH(NOLOCK) where company='TRECEUX' and partnum='" & OriginalRightPart & "' and plant='" & Read_Registry("Plant") & "'", "t1", TranLocal)

                                    ConnLocal.Transaction("EXEC SP_Qty_AdjustmentTRECEUX 'TRECEUX'," & OrderNum & "," & RxID.Text & "," & RxNum & ",'" & Me.OriginalRightPart & "','" & Now.Date & "','" & dsWhse.Tables("t1").Rows(0).Item("warehousecode") & "','" & dsWhse.Tables("t1").Rows(0).Item("primbin") & "',1,'RSN01','Regresa/Inventario Por RXLOCAL->RXVIRTUAL',0,'" & My.User.Name & "',''", TranLocal)

                                    Dim OrderDate As DateTime = CDate(FechaDeCreacion)

                                    If OrderDate.DayOfYear <> Now.DayOfYear Then
                                        ' Ejecutamos la funcion para realizar ELIMINAR el consumo del cambio de local a virtual
                                        DelConsumoAtrasado(FechaDeCreacion, OriginalRightPart, OrderNum.ToString.Substring(0, 2), "", TranLocal, ConnLocal.SQLConn)
                                    End If
                                End If

                                If (OriginalLeftPart <> "") And (CInt(GetLabNum()) <> ARLab) Then

                                    dsWhse = ConnLocal.SQLDS("SELECT primbin,warehousecode from plantwhse WITH(NOLOCK) where company='TRECEUX' and partnum='" & OriginalLeftPart & "' and plant='" & Read_Registry("Plant") & "'", "t2", TranLocal)
                                    ConnLocal.Transaction("EXEC SP_Qty_AdjustmentTRECEUX 'TRECEUX'," & OrderNum & "," & RxID.Text & "," & RxNum & ",'" & Me.OriginalLeftPart & "','" & Now.Date & "','" & dsWhse.Tables("t2").Rows(0).Item("warehousecode") & "','" & dsWhse.Tables("t2").Rows(0).Item("primbin") & "',1,'RSN01','Regresa/Inventario Por RXLOCAL->RXVIRTUAL',0,'" & My.User.Name & "',''", TranLocal)

                                    Dim OrderDate As DateTime = CDate(FechaDeCreacion)

                                    If OrderDate.DayOfYear <> Now.DayOfYear Then
                                        ' Ejecutamos la funcion para realizar ELIMINAR el consumo del cambio de local a virtual
                                        DelConsumoAtrasado(FechaDeCreacion, OriginalLeftPart, OrderNum.ToString.Substring(0, 2), "", TranLocal, ConnLocal.SQLConn)
                                    End If
                                End If
                            End If
                        End If
                    End If
                End If

                Dim PartStrChanged As ItemWithValue
                Dim RightOrLeftChanged As Boolean = False
                Dim RegresaInventario As Boolean = False ' Variable que nos dice si se tiene que regresar a inventario una pastilla si esta es cambiada por una modificacion "RECAPTURA SP"

                ' Revisamos si algun lado cambio primero derecho y luego el izquierdo
                If CheckREye.Checked Then
                    PartStrChanged = ListRightParts.SelectedItem
                    If PartStrChanged.Value <> OriginalRightPart Then
                        If OriginalRightPart <> "" Then
                            RightOrLeftChanged = True
                        End If
                    End If
                End If
                If CheckLEye.Checked Then
                    PartStrChanged = ListLeftParts.SelectedItem
                    If PartStrChanged.Value <> OriginalLeftPart Then
                        If OriginalLeftPart <> "" Then
                            RightOrLeftChanged = True
                        End If
                    End If
                End If
                '********fin:verifca si el numero de pastilla de algun lado cambio **

                '**codigo: que verifica si es necerario consumir/regresar a inventario los numeros de parte
                If IsVirtualRx Then
                    If Me.IsReceivingVirtualRx Then
                        OriginalLeftPart = ""
                        OriginalRightPart = ""
                        ExecuteConsumePastilla = True
                    Else
                        If (ComboErrores.SelectedValue <> My.Settings.RecapturaSP_ID Or PanelModInfo.Visible) Then
                            If ARLab = LabNum Then
                                ExecuteConsumePastilla = True
                            End If
                        Else
                            If (RightOrLeftChanged) And (ARLab = LabNum) Then
                                ExecuteConsumePastilla = True
                                RegresaInventario = True
                            End If
                        End If
                    End If
                Else
                    If IsLocalRx Then
                        If (ComboErrores.SelectedValue <> My.Settings.RecapturaSP_ID Or PanelModInfo.Visible) Then
                            'si ya se consumieron las pastillas por el cambio de virtual-->local ya no consumir de nuevo
                            If Not YaNoConsumir Then ExecuteConsumePastilla = True
                        Else
                            If RightOrLeftChanged Then
                                'si ya se consumieron las pastillas por el cambio de virtual-->local ya no consumir de nuevo
                                If Not YaNoConsumir Then
                                    ExecuteConsumePastilla = True
                                    RegresaInventario = True
                                End If
                            End If
                        End If
                    End If
                End If
                '**fin: termina codigo para determinar si es necesario consumir/regresar a inventario

                'If ExecuteConsumePastilla Then
                Dim RxLab As Integer = OrderNum.ToString.Substring(0, 2)
                ' Consume pastillas solo si no son de Ver Bien
                If ExecuteConsumePastilla And (Not IsVBLab(RxLab)) Then

                    If ComboTrazos.Texto <> "" Then
                        RxNum = ComboTrazos.Texto
                    End If

                    ' Ejecutar Stored Procedure para consumir pastillas
                    ' ------------------------------------------------------------------
                    Comm = New SqlCommand("SP_ConsumePastillasTRECEUX", ConnLocal.SQLConn)
                    Comm.Transaction = TranLocal
                    Comm.CommandType = CommandType.StoredProcedure
                    Comm.CommandTimeout = My.Settings.DBCommandTimeout
                    Comm.Parameters.AddWithValue("Ordernum", OrderNum)
                    Comm.Parameters.AddWithValue("OrdenLab", RxID.Text)
                    If ChangingTraces And RadioDigitalNew.Checked Then
                        Comm.Parameters.AddWithValue("RxNum", ComboTrazos.ComboBox1.SelectedValue)
                    Else
                        Comm.Parameters.AddWithValue("RxNum", Val(RxWECO.Text))
                    End If
                    If Me.IsReceivingVirtualRx Then
                        Comm.Parameters.AddWithValue("TranReference", "Consumo de Pastillas por Recepci�n Virtual")
                    Else
                        Comm.Parameters.AddWithValue("TranReference", "Consumo de Pastillas por Error Generado en Laboratorio")
                    End If

                    Dim x As ItemWithValue = ListLeftParts.SelectedItem

                    Try
                        ErrorQty = 0
                        If CheckLEye.Checked Then
                            ErrorQty += 1
                            x = ListLeftParts.SelectedItem
                            If (ComboErrores.SelectedValue <> 27) Or (PanelModInfo.Visible) Then
                                Comm.Parameters.AddWithValue("Izquierdo", x.Value)
                            Else
                                If x.Value <> OriginalLeftPart Then
                                    If (RegresaInventario) And (OriginalLeftPart <> "") Then

                                        ' Sumar 1 al lente original y restar 1 al nuevo
                                        Dim ds As DataSet
                                        ds = ConnLocal.SQLDS("SELECT primbin,warehousecode from plantwhse WITH(NOLOCK) where company='TRECEUX' and partnum='" & OriginalLeftPart & "' and plant='" & Read_Registry("Plant") & "'", "t1", TranLocal)
                                        If ds.Tables("t1").Rows.Count > 0 Then
                                            ConnLocal.Transaction("EXEC SP_Qty_AdjustmentTRECEUX 'TRECEUX'," & OrderNum & "," & RxID.Text & "," & RxNum & ",'" & Me.OriginalLeftPart & "','" & Now.Date & "','" & ds.Tables("t1").Rows(0).Item("warehousecode") & "','" & ds.Tables("t1").Rows(0).Item("primbin") & "',1,'RSN01','Error al escoger pastilla. Se devuelve a inventario',0,'" & My.User.Name & "',''", TranLocal)
                                        Else
                                            Throw New Exception("No se encontro pastilla en PlantWhse [" & OriginalLeftPart & "]")
                                        End If
                                    End If
                                    Comm.Parameters.AddWithValue("Izquierdo", x.Value)
                                Else
                                    Comm.Parameters.AddWithValue("Izquierdo", "")
                                End If
                            End If
                        Else
                            Comm.Parameters.AddWithValue("Izquierdo", "")
                        End If

                        If CheckREye.Checked Then
                            ErrorQty += 1
                            x = ListRightParts.SelectedItem
                            If (ComboErrores.SelectedValue <> 27) Or (PanelModInfo.Visible) Then
                                Comm.Parameters.AddWithValue("Derecho", x.Value)
                            Else
                                If x.Value <> OriginalRightPart Then
                                    If (RegresaInventario) And (OriginalRightPart <> "") Then
                                        ' Sumar 1 al lente original y restar 1 al nuevo
                                        Dim ds As DataSet
                                        ds = ConnLocal.SQLDS("SELECT primbin,warehousecode from plantwhse WITH(NOLOCK) where company='TRECEUX' and partnum='" & OriginalRightPart & "' and plant='" & Read_Registry("Plant") & "'", "t1", TranLocal)
                                        If ds.Tables("t1").Rows.Count > 0 Then
                                            ConnLocal.Transaction("EXEC SP_Qty_AdjustmentTRECEUX 'TRECEUX'," & OrderNum & "," & RxID.Text & "," & RxNum & ",'" & Me.OriginalRightPart & "','" & Now.Date & "','" & ds.Tables("t1").Rows(0).Item("warehousecode") & "','" & ds.Tables("t1").Rows(0).Item("primbin") & "',1,'RSN01','Error al escoger pastilla. Se devuelve a inventario',0,'" & My.User.Name & "',''", TranLocal)
                                        Else
                                            Throw New Exception("No se encontro pastilla en PlantWhse [" & OriginalRightPart & "]")
                                        End If

                                    End If
                                    Comm.Parameters.AddWithValue("Derecho", x.Value)
                                Else
                                    Comm.Parameters.AddWithValue("Derecho", "")
                                End If
                            End If
                        Else
                            Comm.Parameters.AddWithValue("Derecho", "")
                        End If
                        Comm.Parameters.AddWithValue("plant", Read_Registry("Plant"))
                        Comm.Parameters.AddWithValue("EntryPerson", "Lab: " & LabNum)
                        Comm.Parameters.Add("@Mensaje", Data.SqlDbType.VarChar, 30)
                        Comm.Parameters("@Mensaje").Direction = Data.ParameterDirection.Output
                        Comm.ExecuteNonQuery()
                    Catch ex As Exception
                        Throw New Exception(ex.Message)
                    Finally

                    End Try

                    ' ------------------------------------------------------------------
                End If
                'Throw New Exception("Test")

                ' -------------- TERMINA SECCION PARA CONSUMIR INVENTARIOS DE MANERA LOCAL --------------------

                ' ------------------ SECCION PARA CONSUMO DE ARMAZONES EN UNA ACTUALIZACION -----------------------

                Select Case Me.PACKTRAN_TYPE

                    Case 0  ' Se agrego Paquete/Armazon
                        Dim part As String
                        If NumArmazon.ToString.Length = 5 Then
                            Try : part = NumArmazon.ToString.Substring(0, 5) : Catch : part = 0 : End Try
                            Dim TempMyFrame As Frames = New Frames(part, Frames.ThisFrameStatus.[New])
                            Dim CheckOurInventory As Boolean = TempMyFrame.CheckInventoryFrameFamily()

                            If CheckOurInventory Then
                                If Me.FrameErrorID <> My.Settings.FrameErrorSP_ID And part <> 0 Then    ' Si se gasto el armazon
                                    Dim str As String = "SELECT plantwhse.primbin, plantwhse.warehousecode, isnull(partbin.onhandqty,0) as onhandqty " & _
                                        "FROM plantwhse WITH(NOLOCK) " & _
                                        "LEFT OUTER JOIN partbin WITH(NOLOCK) ON partbin.partnum = plantwhse.partnum " & _
                                        "WHERE plantwhse.company='TRECEUX' AND plantwhse.partnum='" & part & _
                                        "' AND plantwhse.plant='" & My.Settings.Plant & "'"

                                    Dim dsWhse As DataSet
                                    dsWhse = ConnLocal.SQLDS(str, "t1", TranLocal)

                                    Try
                                        str = "EXEC SP_Qty_AdjustmentTRECEUX 'TRECEUX'," & OrderNum & "," & RxID.Text & "," & RxWECO.Text & ",'" & part & "','" & Now.Date & "','" & dsWhse.Tables("t1").Rows(0).Item("warehousecode") & "','" & dsWhse.Tables("t1").Rows(0).Item("primbin") & "',-1,'NEW','Descuento de Armazon',0,'" & My.User.Name & "',''"
                                        ConnLocal.Transaction(str, TranLocal)
                                    Catch ex As Exception
                                        Throw New Exception("No se pudo actualizar inventario del armazon!" + vbCrLf + ex.Message)
                                    End Try

                                End If
                            End If
                        End If
                    Case 1  ' Quitar Paquete
                        Dim part As String
                        Try : part = ArmazonOriginal.ToString.Substring(0, 5) : Catch : part = 0 : End Try
                        If part <> 0 Then
                            Dim dsWhse As DataSet
                            dsWhse = ConnLocal.SQLDS("SELECT plantwhse.primbin, plantwhse.warehousecode, isnull(partbin.onhandqty,0) as onhandqty " & _
                                "FROM plantwhse WITH(NOLOCK) " & _
                                "LEFT OUTER JOIN partbin WITH(NOLOCK) ON partbin.partnum = plantwhse.partnum " & _
                                "WHERE plantwhse.company='TRECEUX' AND plantwhse.partnum='" & part & _
                                "' AND plantwhse.plant='" & My.Settings.Plant & "'", "t1", TranLocal)

                            Try
                                ConnLocal.Transaction("EXEC SP_Qty_AdjustmentTRECEUX 'TRECEUX'," & OrderNum & "," & RxID.Text & "," & RxWECO.Text & ",'" & part & "','" & Now.Date & "','" & dsWhse.Tables("t1").Rows(0).Item("warehousecode") & "','" & dsWhse.Tables("t1").Rows(0).Item("primbin") & "',1,'RINA01','Regresa/Inventario Por Cambio de Armazon',0,'" & My.User.Name & "',''", TranLocal)
                            Catch ex As Exception
                                Throw New Exception("No se pudo actualizar inventario del armazon!" + vbCrLf + ex.Message)
                            End Try

                        End If

                    Case 2  ' Cambiar Paquete
                        Dim part As String
                        Try : part = MyFrame.ArmazonOriginal.ToString.Substring(0, 5) : Catch : part = 0 : End Try

                        Dim str As String = "SELECT plantwhse.primbin, plantwhse.warehousecode, isnull(partbin.onhandqty,0) as onhandqty " & _
                            "FROM plantwhse WITH(NOLOCK) " & _
                            "LEFT OUTER JOIN partbin WITH(NOLOCK) ON partbin.partnum = plantwhse.partnum " & _
                            "WHERE plantwhse.company='TRECEUX' AND plantwhse.partnum='" & part & _
                            "' AND plantwhse.plant='" & My.Settings.Plant & "'"

                        Dim dsWhse As DataSet
                        dsWhse = ConnLocal.SQLDS(str, "t1", TranLocal)

                        Try
                            If part > 0 Then
                                ' Regresa a inventario el armazon original (En caso de haber tenido uno) y solo si es una recaptura SP de armazon
                                If MyFrame.ErrorID = My.Settings.FrameErrorSP_ID Then
                                    str = "EXEC SP_Qty_AdjustmentTRECEUX 'TRECEUX'," & OrderNum & "," & RxID.Text & "," & RxWECO.Text & ",'" & part & "','" & Now.Date & "','" & dsWhse.Tables("t1").Rows(0).Item("warehousecode") & "','" & dsWhse.Tables("t1").Rows(0).Item("primbin") & "',1,'RINA01','Regresa/Inventario Por Cambio de Armazon',0,'" & My.User.Name & "',''"
                                    ConnLocal.Transaction(str, TranLocal)
                                End If
                            End If

                            If MyFrame.NumArmazon > 0 Then
                                Try : part = MyFrame.NumArmazon.ToString.Substring(0, 5) : Catch : part = 0 : End Try
                                str = "SELECT plantwhse.primbin, plantwhse.warehousecode, isnull(partbin.onhandqty,0) as onhandqty " & _
                                "FROM plantwhse WITH(NOLOCK) " & _
                                "LEFT OUTER JOIN partbin WITH(NOLOCK) ON partbin.partnum = plantwhse.partnum " & _
                                "WHERE plantwhse.company='TRECEUX' AND plantwhse.partnum='" & part & _
                                "' AND plantwhse.plant='" & My.Settings.Plant & "'"
                                dsWhse = ConnLocal.SQLDS(str, "t1", TranLocal)
                                If dsWhse.Tables("t1").Rows.Count > 0 Then
                                    If (CInt(dsWhse.Tables("t1").Rows(0).Item("onhandqty")) > 0) Then
                                        dsWhse = ConnLocal.SQLDS(str, "t1", TranLocal)
                                        str = "EXEC SP_Qty_AdjustmentTRECEUX 'TRECEUX'," & OrderNum & "," & RxID.Text & "," & RxWECO.Text & ",'" & part & "','" & Now.Date & "','" & dsWhse.Tables("t1").Rows(0).Item("warehousecode") & "','" & dsWhse.Tables("t1").Rows(0).Item("primbin") & "',-1,'DINA01','Descuenta/Inventario Por Cambio de Armazon',0,'" & My.User.Name & "',''"
                                        ConnLocal.Transaction(str, TranLocal)
                                    End If
                                Else
                                    Throw New Exception("No existe registro de inventario en base de datos")
                                End If

                            End If
                        Catch ex As Exception
                            MsgBox("No se pudo actualizar inventario del armazon!" & vbCrLf & ex.Message, MsgBoxStyle.Exclamation)
                        End Try

                    Case 3 ' Cambio del n�mero de paquete



                End Select

                ' ------------------ TERMINA SECCION DE CONSUMO DE ARMAZONES -----------------------------

                Comm = New SqlCommand("SP_UpdateSalesOrderLines", ConnLocal.SQLConn)
                Comm.CommandType = Data.CommandType.StoredProcedure
                Comm.Transaction = TranLocal
                Comm.CommandTimeout = My.Settings.DBCommandTimeout
                Comm.Parameters.AddWithValue("@Company", "TRECEUX")
                Comm.Parameters.AddWithValue("@OrderNum", OrderNum)
                Comm.Parameters.AddWithValue("@parametros", Parametros) '"2105030007,1,0,2105030007,1,1")
                Comm.Parameters.Add("@Msg", Data.SqlDbType.VarChar, 100)
                Comm.Parameters("@Msg").Direction = Data.ParameterDirection.Output
                Try
                    Comm.ExecuteNonQuery()
                    Mensaje = Comm.Parameters("@Msg").Value
                Catch ex As Exception
                    Mensaje = "FAILED"
                    Throw New Exception("Error al Actualizar los Detalles de la orden: " & vbCrLf & ex.Message)
                End Try

                If Mensaje <> "OK" Then
                    MsgBox("Error en las lineas")
                End If

                '----------------------------------------------------------------------
                ' Marco la Rx Virtual con los ojos que debe mostrar en el lab. remoto
                '----------------------------------------------------------------------
                If IsVirtualRx Then
                    If Not ConnLocal.Transaction("update orderhed set checkbox14 = " & Convert.ToInt16(Me.CheckLEye.Checked) & ", checkbox15 = " & Convert.ToInt16(Me.CheckREye.Checked) & " where ordernum = " & OrderNum, TranLocal) Then
                        MsgBox("Error al actualizar datos de ojos seleccionados", MsgBoxStyle.Exclamation)
                    End If
                End If

            ElseIf Mensaje <> "OK" Then
                MsgBox("Fallo al modificar la orden" & vbCrLf & Mensaje)
            Else
                If ReceivingVirtualRx <> "" Then
                    AL.GridARList.SelectedRows(0).Visible = False
                End If

                If ChangingTraces Then
                    Try
                        If ChangingTraces And (ComboTrazos.ComboBox1.SelectedValue <> RxWECO.Text) Then
                            ConnLocal.SQLDS("update " & TracerTable & " set status = 0 where jobnum = '" & ComboTrazos.ComboBox1.SelectedValue & "'", "t2", TranLocal)
                            ConnLocal.SQLDS("update " & TracerTable & " set status = 1 where jobnum = '" & RxWECO.Text & "'", "t2", TranLocal)
                        Else
                            ConnLocal.SQLDS("update " & TracerTable & " set status = 0 where jobnum = '" & RxWECO.Text & "'", "t2", TranLocal)
                        End If
                    Catch ex As Exception
                        MsgBox(ex.Message, MsgBoxStyle.Critical)
                    End Try
                End If

            End If

            If (ExecuteConsumePastilla Or ComboErrores.SelectedValue = My.Settings.RecapturaSP_ID) And Not Me.IsReceivingVirtualRx And Not Me.IsLocalReceive Then
                Dim RxNum As Integer
                If ChangingTraces Then
                    RxNum = ComboTrazos.ComboBox1.SelectedValue
                Else
                    RxNum = Val(RxWECO.Text)
                End If

                If Not Me.SavedInRemoteServer Then
                    Try
                        Dim ds As DataSet
                        If FrameErrorID <> My.Settings.FrameErrorSP_ID And ComboErrores.SelectedValue = My.Settings.FrameError_ID Then
                            Dim partnum As String
                            Try
                                partnum = MyFrame.NumArmazon.ToString.Substring(0, 5)
                                ds = ConnLocal.SQLDS("INSERT INTO TblErrores (ordernum, description, errorid, errordate, ordenlab,plant,partnum,rxnum) VALUES (" & OrderNum & ", '" & FrameErrorDesc & "', " & FrameErrorID & ", GETDATE(), '" & RxID.Text & "', " & LabNum & ", '" & partnum & "', " & RxNum & ")", "t1", TranLocal)
                            Catch ex As Exception
                                partnum = 0
                            End Try

                        Else
                            If OrderNum.ToString.Substring(0, 2) = 12 Then LabNum = 12

                            If CheckREye.Checked Then
                                Dim x As ItemWithValue = ListRightParts.SelectedItem
                                If ExecuteConsumePastilla And Me.PanelModInfo.Visible = True Then
                                    ComboErrores.Text = LBLDEFDER.Text
                                End If
                                ds = ConnLocal.SQLDS("INSERT INTO TblErrores (ordernum, description, errorid, errordate, ordenlab,plant,partnum,rxnum) VALUES (" & OrderNum & ", '" & ComboErrores.Text & "', " & ComboErrores.SelectedValue & ", GETDATE(), '" & RxID.Text & "', " & LabNum & ", '" & x.Value & "', " & RxNum & ")", "t1", TranLocal)
                            End If
                            If CheckLEye.Checked Then
                                Dim x As ItemWithValue = ListLeftParts.SelectedItem
                                If ExecuteConsumePastilla And Me.PanelModInfo.Visible = True Then
                                    ComboErrores.Text = LBLDEFIZQ.Text
                                End If
                                ds = ConnLocal.SQLDS("INSERT INTO TblErrores (ordernum, description, errorid, errordate, ordenlab,plant,partnum,rxnum) VALUES (" & OrderNum & ", '" & ComboErrores.Text & "', " & ComboErrores.SelectedValue & ", GETDATE(), '" & RxID.Text & "', " & LabNum & ", '" & x.Value & "', " & RxNum & ")", "t1", TranLocal)
                            End If

                        End If

                    Catch ex As Exception
                        'MsgBox(ex.Message, MsgBoxStyle.Information)
                        Throw New Exception("Error al insertar registro de Error: " & ex.Message)
                    End Try
                    ' ''End If
                End If

            End If
            If ReceivingVirtualRx <> "" Then
                AL.GridARList.SelectedRows(0).Cells("Rx").Value = 0
                AL.GridARList.SelectedRows(0).Cells(0).Value = 0
                AL.GridARList.SelectedRows(0).Cells("Laboratorio").Value = "PROCESADA"
            End If

            OrderOK = True
            TranLocal.Commit()

        Catch ex As Exception
            TranLocal.Rollback()
            OrderOK = False

            If ProcessLocal And Not Me.SavedInRemoteServer Then
                Failed = True
                Message = ex.Message
            Else
                Throw New Exception(ex.Message)
            End If
        Finally
            ConnLocal.CloseConn()
            If Failed And Message.Length > 0 Then
                MsgBox(Message, MsgBoxStyle.Exclamation)
            End If
        End Try

        Try
            ' Pedro Far�as Lozano   Ene 11 2013
            ' Recalcula los totales de la orden

            RecalcTotal(OrderNum)
        Catch ex As Exception
            MsgBox("Error al recalcular el total de la orden." + vbCrLf + vbCrLf + ex.Message, MsgBoxStyle.Information, "Recalcula Total Orden")
        End Try

        ' Pedro Far�as Lozano Abril 9 2013
        ' Problema : Las �rdenes virtuales de reproceso siguen apareciendo en el listado de virtuales, hasta que se sincroniza con el servidor central
        ' Soluci�n : Marca las �rdenes virtuales en el servidor central como 'Recibidas por el Lab. Remoto' y quitar bandera de Reproceso
        ' Si est� recibiendo una orden virtual limpia la bandera de reproceso en el servidor central para que no aparezca nuevamente en el listado
        If IsVirtualRx Then
            If Me.IsReceivingVirtualRx Then
                Dim t As SqlDB = Nothing
                Dim TranSVR4 As SqlTransaction = Nothing

                If (My.Settings.VantageServer.Length >= 8 And Ping(My.Settings.VantageServer)) Then
                    t = New SqlDB(My.Settings.VantageServer, My.Settings.DBUser, My.Settings.DBPassword, My.Settings.MfgSysDBName)
                    Try
                        t.OpenConn()
                        TranSVR4 = t.SQLConn.BeginTransaction

                        Dim sqlstr As String = "UPDATE ORDERHED SET CHECKBOX13=0,CHECKBOX10=1,DATE07=GETDATE() WHERE ORDERNUM=" & OrderNum.ToString & " AND COMPANY='TRECEUX'"

                        If t.ExecuteNonQuery(sqlstr, TranSVR4) Then TranSVR4.Commit()

                    Catch Ex As Exception
                        TranSVR4.Rollback()
                    End Try

                    t.SQLConn.Close()

                End If

            End If
        End If

    End Sub

    ' Funcion que suma a los consumos
    Private Sub RegConsumoAtrasado(ByVal FechaConsum As String, ByVal Parte As String, ByVal LabLocal As String, _
        ByVal LabVirtual As String, ByRef TransLocal As SqlTransaction, ByRef mc As SqlClient.SqlConnection)

        Dim Cmd As New SqlClient.SqlCommand
        Dim ReaderConn As New SqlClient.SqlConnection
        Dim reader As SqlClient.SqlDataReader = Nothing

        Dim orderdate As DateTime = CDate(FechaConsum)
        Dim currentqty As Integer = 0

        Dim QryStr As String = "SELECT cantidad FROM TblPastillasxLab WITH(NOLOCK) WHERE fecha='" & orderdate.ToShortDateString & _
                "' AND planta=" & LabLocal & " AND pltar='" & LabVirtual.Trim() & "' AND pastilla='" & Parte & "' AND es_consumo=1 AND sincronizado=0"

        Try
            ReaderConn.ConnectionString = Laboratorios.ConnStr

            ReaderConn.Open()
            Cmd.CommandText = QryStr
            Cmd.Connection = ReaderConn

            reader = Cmd.ExecuteReader()

            If reader.HasRows Then
                reader.Read()
                currentqty = CInt(reader("cantidad"))
            End If
            ReaderConn.Close()

            If currentqty > 0 Then
                Cmd.CommandText = "UPDATE TblPastillasxLab SET cantidad = " & CStr(currentqty + 1) & " WHERE fecha='" & orderdate.ToShortDateString & _
                "' AND planta=" & LabLocal & " AND pltar='" & LabVirtual.Trim() & "' AND pastilla='" & Parte & "' AND es_consumo=1 AND sincronizado=0"
            Else
                Cmd.CommandText = "INSERT INTO TblPastillasxLab (fecha, pastilla, cantidad, planta, pltar, es_consumo) VALUES ('" & orderdate.ToShortDateString & _
                                "', '" & Parte & "', 1, '" & LabLocal.Trim() & "', '" & LabVirtual.Trim() & "', 1)"
            End If

            Cmd.Connection = mc

            Cmd.Transaction = Translocal
            Cmd.ExecuteNonQuery()
            Cmd.Dispose()
            Cmd = Nothing

        Catch ex As Exception
            MsgBox(ex.Message)
        End Try

    End Sub

    ' Funcion que resta de los consumos (Esta siendo actualizado por Fco. Garcia)
    Private Sub DelConsumoAtrasado(ByVal FechaConsum As String, ByVal Parte As String, ByVal LabLocal As String, _
        ByVal LabVirtual As String, ByRef TransLocal As SqlTransaction, ByRef mc As SqlClient.SqlConnection)

        Dim Cmd As New SqlClient.SqlCommand
        Dim ReaderConn As New SqlClient.SqlConnection
        Dim reader As SqlClient.SqlDataReader = Nothing

        Dim orderdate As DateTime = CDate(FechaConsum)
        Dim currentqty As Integer = 0

        Dim QryStr As String = "SELECT cantidad FROM TblPastillasxLab WITH(NOLOCK) WHERE fecha='" & orderdate.ToShortDateString & _
                "' AND planta=" & LabLocal & " AND pltar='" & LabVirtual.Trim() & "' AND pastilla='" & Parte & "' AND es_consumo=0 AND sincronizado=0"

        Try
            ReaderConn.ConnectionString = Laboratorios.ConnStr

            ReaderConn.Open()
            Cmd.CommandText = QryStr
            Cmd.Connection = ReaderConn

            reader = Cmd.ExecuteReader()

            If reader.HasRows Then
                reader.Read()
                currentqty = CInt(reader("cantidad"))
            End If
            ReaderConn.Close()

            If currentqty > 0 Then
                Cmd.CommandText = "UPDATE TblPastillasxLab SET cantidad = " & CStr(currentqty + 1) & " WHERE fecha='" & orderdate.ToShortDateString & _
                "' AND planta=" & LabLocal & " AND pltar='" & LabVirtual.Trim() & "' AND pastilla='" & Parte & "' AND es_consumo=0 AND sincronizado=0"
            Else
                Cmd.CommandText = "INSERT INTO TblPastillasxLab (fecha, pastilla, cantidad, planta, pltar, es_consumo) VALUES ('" & orderdate.ToShortDateString & _
                "', '" & Parte & "', 1, '" & LabLocal.Trim() & "', '" & LabVirtual.Trim() & "', 0)"

            End If

            Cmd.Connection = mc

            Cmd.Transaction = TransLocal
            Cmd.ExecuteNonQuery()
            Cmd.Dispose()
            Cmd = Nothing

        Catch ex As Exception
            MsgBox(ex.Message)
        End Try

    End Sub

    Private Function HasQARegistry(ByVal ordernum As Integer)
        Dim Result As Boolean = False
        Dim Failed As Boolean = False
        Dim FailedMessage As String = ""
        Dim t As New SqlDB(Laboratorios.ConnStr)

        t.OpenConn()
        Try
            Dim sqlstr As String = "select ordernum from TblQAhead with(nolock) where ordernum = " & ordernum
            Dim ds As New DataSet
            ds = t.SQLDS(sqlstr, "t1")
            If ds.Tables("t1").Rows.Count > 0 Then
                Result = True
            Else
                Result = False
            End If
        Catch ex As Exception
            Result = False
            Failed = True
            FailedMessage = ex.Message
        Finally
            t.CloseConn()
            If Failed Then
                MsgBox(FailedMessage, MsgBoxStyle.Exclamation)
            End If
        End Try
        Return Result
    End Function

    Sub FinishSavingRx()
        WorkingBar.Visible = True
        Dim VntgOrder As Integer = 0
        Try
            WorkingBar.Value = 0
            Me.Cursor = Cursors.WaitCursor
            Application.DoEvents()

            If OrderExists Or IsReceivingVirtualRx Then

                OrderExists = True      ' Esto se hace para ser congruentes con las recetas virtuales. Las Rx Virtuales todav�a no existen, pero es necesario
                WorkingBar.Value = 30   ' para conservar el mismo n�mero de orden vantage
                Application.DoEvents()
                OrderOK = False
                UpdateOrder(Val(RxNum.Text), UpdateType.OrderAndLines, True)

                If Not OrderOK Then
                    ' Se comento la linea porque ya se trabaja siempre OffLine
                    'Me.ChangeWorkingStatus(WorkingStatusType.OffLine)
                    UpdateOrder(Val(RxNum.Text), UpdateType.OrderAndLines, True)
                End If


                If OrderOK Then
                    ' SE IMPRIME LA RECETA SI SE GENERO LA ORDEN
                    Generar_Impresion(Val(RxNum.Text))

                    WorkingBar.Value = 50
                    Application.DoEvents()

                    If My.Settings.HasMEIEdger Then
                        '-----------------------------------------------
                        ' Genero el archivo OMA para la Biseladora MEI
                        '-----------------------------------------------
                        OMA.SetACCN(RxNum.Text)

                        ' Pedro Far�as Lozano Oct 17 2012
                        ' Se cambia el nombre de archivo para la MEI. Ahora se usa el n�mero de Vantage para evitar duplicidad
                        ' OMA.SetJOB(VntgOrder)
                        ' Por el momento no se va a cambiar hasta que las personas de Chapultepec entiendan mejor el proceso, y para
                        ' que no se confundan m�s. Sorry quedar� pendiente. En el programa de VerBien ya est� listo.
                        OMA.SetJOB(RxWECO.Text)

                        If CheckAbril.Checked Then
                            OMA.SetPOLISH(My.Settings.POLISH)
                        Else
                            OMA.SetPOLISH(OMAFile.PolishingOptions.NoPolish)
                        End If
                        '-----------------------------------------------
                        ' Nuevo
                        '-----------------------------------------------
                        OMA.Set_VVINCON(0, 0)
                        OMA.Set_VANGLON(0, 0)
                        OMA.Set_CHKRAD(1, 1)
                        OMA.Set_CHRFRAM(1)
                        OMA.Set_TBASEFR("")
                        OMA.Set_CKSEQMAN(0)
                        OMA.Set_COPYDXSX(0)
                        OMA.Set_VDEPTON(0, 0)
                        OMA.Set_GANGS(0, 0)
                        OMA.Set_GANGE(0, 0)
                        OMA.Set_GANGS2(0, 0)
                        OMA.Set_GANGE2(0, 0)
                        OMA.Set_GRV2ON(0, 0)
                        OMA.Set_GRV2ON(0, 0)
                        OMA.Set_GRV2SEL(0, 0)
                        If OMA.ETYP_R = OMAFile.EdgeType.GROOVE Or OMA.ETYP_L = OMAFile.EdgeType.GROOVE Then
                            OMA.SetBEVM(My.Settings.BEVM, My.Settings.BEVM)
                        Else
                            OMA.SetBEVM(0, 0)
                        End If


                        OMA.SetCTHICK(Math.Round(Me.PrintObj.MoldeD.GrosorCentral, 1), Math.Round(Me.PrintObj.MoldeI.GrosorCentral, 1))
                        OMA.SetTHKP(Math.Round(Me.PrintObj.MoldeD.GrosorMaximo, 1), Math.Round(Me.PrintObj.MoldeI.GrosorMaximo, 1))
                        OMA.SetFRAM(GetFrameName(Me.FrameNum))
                        OMA.SetBSIZ(GetOffsetToFrame(Me.FrameNum, "R"), GetOffsetToFrame(Me.FrameNum, "L"))

                        OMA.SetDO(GetEyeSides())


                    End If

                    '********** OMA *******************************
                    '** Si el laboratorio cuenta con los trazos para el OMA, actualizamos el tipo de armazon en la tabla OMATraces
                    '** el numero de receta = jobnum se encuetra en el textbox RxNum.Text
                    If (My.Settings.Plant <> "VBENS") Then
                        If My.Settings.OpticalProtocol = "OMA" Then UpdateOMATrace()
                        WorkingBar.Value = 75
                        Application.DoEvents()

                    End If

                    '**********************************************

                    'If My.Settings.HasMEIEdger And Not My.Settings.MEI_OMA_Service Then
                    If My.Settings.HasMEIEdger Then
                        OMA.WriteFile(My.Settings.MEI_JobsFolder & OMA.GetFileName)
                        WorkingBar.Value = 90
                        Application.DoEvents()

                    End If


                    '-----------------------------------------------
                    ''generamos registro de calidad para la modificacion
                    ''si el sistema tiene habilitado el registro de QA
                    'If Read_Registry("QAEnabled") Then
                    '    ' Se graba registro solo si no es Recaptura SP
                    '    If ((ComboErrores.SelectedValue <> 27) Or (PanelModInfo.Visible)) Or IsReceivingVirtualRx Then
                    '        RegistroQA(Val(RxNum.Text), True)
                    '    Else
                    '        '------------------------------------------------------------------------------------------------------------------
                    '        ' Aqui verifico si tiene Registro de QA, si no lo tiene, mando grabarlo para que aparezca en la estacion de calidad
                    '        '------------------------------------------------------------------------------------------------------------------
                    '        If Not HasQARegistry(RxNum.Text) Then
                    '            RegistroQA(Val(RxNum.Text), True)
                    '        End If
                    '        '------------------------------------------------------------------------------------------------------------------
                    '    End If
                    'End If
                End If
                WorkingBar.Value = 100
                WorkingBar.Invalidate()

            Else
                WorkingBar.Value = 15
                OrderOK = False

                ' Esta funcion unifica los stored procedures de crear una orden y job
                VntgOrder = Ejecutar_Rx()
                Application.DoEvents()
                RxNum.Text = VntgOrder
                If OrderOK Then
                    ' SE IMPRIME LA RECETA SI SE GENERO LA ORDEN
                    Generar_Impresion(Val(RxNum.Text))
                    WorkingBar.Value = 35
                    Application.DoEvents()
                    '********** OMA *******************************
                    '** Si el laboratorio cuenta con los trazos para el OMA, actualizamos el tipo de armazon en la tabla OMATraces
                    '** el numero de receta = jobnum se encuetra en el textbox RxNum.Text
                    If (My.Settings.Plant <> "VBENS") Then
                        If My.Settings.OpticalProtocol = "OMA" Then UpdateOMATrace()
                    End If
                    'If My.Settings.OpticalProtocol = "OMA" Then UpdateOMATrace()
                    '**********************************************
                    If My.Settings.HasMEIEdger Then
                        '-----------------------------------------------
                        ' Genero el archivo OMA para la Biseladora MEI
                        '-----------------------------------------------
                        'OMA.SetJOB(RxWECO.Text)
                        ' Pedro Far�as Lozano Oct 17 2012
                        ' Se cambia el nombre de archivo para la MEI. Ahora se usa el n�mero de Vantage para evitar duplicidad
                        'OMA.SetJOB(VntgOrder)
                        ' Por el momento no se va a cambiar hasta que las personas de Chapultepec entiendan mejor el proceso, y para
                        ' que no se confundan m�s. Sorry quedar� pendiente. En el programa de VerBien ya est� listo.
                        OMA.SetJOB(RxWECO.Text)
                        OMA.SetACCN(RxNum.Text)

                        WorkingBar.Value = 65
                        WorkingBar.Invalidate()

                        If CheckAbril.Checked Then
                            OMA.SetPOLISH(My.Settings.POLISH)
                        Else
                            OMA.SetPOLISH(OMAFile.PolishingOptions.NoPolish)
                        End If
                        '-----------------------------------------------
                        ' Nuevo
                        '-----------------------------------------------
                        OMA.Set_VVINCON(0, 0)
                        OMA.Set_VANGLON(0, 0)
                        OMA.Set_CHKRAD(1, 1)
                        OMA.Set_CHRFRAM(1)
                        OMA.Set_TBASEFR("")
                        OMA.Set_CKSEQMAN(0)
                        OMA.Set_COPYDXSX(0)
                        OMA.Set_VDEPTON(0, 0)
                        OMA.Set_GANGS(0, 0)
                        OMA.Set_GANGE(0, 0)
                        OMA.Set_GANGS2(0, 0)
                        OMA.Set_GANGE2(0, 0)
                        OMA.Set_GRV2ON(0, 0)
                        OMA.Set_GRV2ON(0, 0)
                        OMA.Set_GRV2SEL(0, 0)
                        If OMA.ETYP_R = OMAFile.EdgeType.GROOVE Or OMA.ETYP_L = OMAFile.EdgeType.GROOVE Then
                            OMA.SetBEVM(My.Settings.BEVM, My.Settings.BEVM)
                        Else
                            OMA.SetBEVM(0, 0)
                        End If

                        '-----------------------------------------------
                        Try
                            OMA.WriteFile(My.Settings.MEI_JobsFolder & OMA.GetFileName)
                        Catch ex As Exception
                            Throw New Exception("No se pudo generar archivo en la biseladora MEI" + vbCrLf + "Favor de reimprimir la Rx" + vbCrLf + ex.Message)
                        End Try
                        '-----------------------------------------------
                        WorkingBar.Value = 85
                        Application.DoEvents()
                    End If
                    ''GENERAMOS REGISTRO EN BASE DE DATOS LOCAL PARA INSPECCION DE CALIDAD
                    If My.Settings.QAEnabled Then
                        Dim t As New SqlDB(My.Settings.LocalServer, My.Settings.DBUser, My.Settings.DBPassword, My.Settings.LocalDBName)
                        Dim TranLocal As SqlTransaction
                        Try
                            t.OpenConn()
                            TranLocal = t.SQLConn.BeginTransaction
                            RegistroQA(VntgOrder, False, t.SQLConn, TranLocal)

                        Catch ex As Exception
                            MsgBox("No fu� posible crear el registro para QA", MsgBoxStyle.Information, "FinishSavingRx")

                        End Try

                    End If
                End If

            End If
            Me.Cursor = Cursors.Default
            WorkingBar.Value = 100
            Application.DoEvents()

        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical)
        Finally
            WorkingBar.Visible = False
            If OrderOK Then
                InitValues()
                ' # PEDRO FARIAS LOZANO # 7/19/2013 #
                ' No es necesario actualizar SIEMPRE el listado de �rdenes de internet y virtuales. Dejamos que el timer lo haga.
                '    Update_Virtuales_WebOrders(Me, New EventArgs)
            End If
            Me.Cursor = Cursors.Default
        End Try

    End Sub

    Private Sub UpdateOMATrace()
        Dim FTYP, ETYP As Integer
        If RxWECO.Text = "" Then
            Throw New Exception("Numero de Rx (RXWeco) referencia digital esta vacio y no se podra actualizar los datos de OMA")
        End If
        Select Case ComboArmazon2.Texto
            Case "Metal"
                FTYP = 2
                ETYP = 1 'EDGE BEVEL
                If My.Settings.HasMEIEdger Then
                    OMA.SetFTYP(OMAFile.FrameTypes.Metal)   'METAL
                    OMA.SetETYP(OMAFile.LensTypes.Bevel, OMAFile.LensTypes.Bevel)    'EDGE BEVEL
                End If
            Case "Plastico"
                FTYP = 1
                ETYP = 1  'EDGE BEVEL'
                If My.Settings.HasMEIEdger Then
                    OMA.SetFTYP(OMAFile.FrameTypes.Plastic) 'PLASTICO
                    OMA.SetETYP(OMAFile.LensTypes.Bevel, OMAFile.LensTypes.Bevel)    'EDGE BEVEL
                End If
            Case "Ranurado"
                FTYP = 3
                ETYP = 3 'EDGE GROOVE'
                If My.Settings.HasMEIEdger Then
                    OMA.SetFTYP(OMAFile.FrameTypes.Rimless)
                    OMA.SetETYP(OMAFile.LensTypes.Groove, OMAFile.LensTypes.Groove)   'EDGE GROOVE'
                End If
            Case "Perforado"
                FTYP = 3
                ETYP = 2 'EDGE RIMLESS'
                If My.Settings.HasMEIEdger Then
                    OMA.SetFTYP(OMAFile.FrameTypes.Rimless)
                    OMA.SetETYP(OMAFile.LensTypes.Rimless, OMAFile.LensTypes.Groove)  'EDGE RIMLESS'
                End If
        End Select

        Dim Failed As Boolean = False
        Dim FailedMessage As String = ""

        Dim t As New SqlDB(My.Settings.LocalServer, My.Settings.DBUser, My.Settings.DBPassword, My.Settings.LocalDBName)
        Try
            t.OpenConn()
            Dim ds As New DataSet
            Dim UpdtStr As String
            UpdtStr = "UPDATE OMATraces set status = 0, DBL = 'DBL=" & Me.ContornoPuente.Text & vbCr & "', FTYP = 'FTYP=" & CStr(FTYP) & "',LTYP='" & OMA_LTYP & vbCrLf & "'," & _
              "FBOCIN='" & OMA_IN_OUT & "',FBSGUP='" & OMA_UP_DOWN & "',SWIDTH='" & OMA_SWIDHT & "'," & _
              "LMATID='" & OMA_LMATID & "',IPD='" & OMA_IPD & "'," & _
              "BCOCIN='" & OMA_BCOCIN & "',BCOCUP='" & OMA_BCOCUP & "',FCSGIN='" & OMA_FCSGIN & "',FCSGUP='" & OMA_FCSGUP & "'," & _
              "DIA='" & OMA_DIA & "',CRIB='" & OMA_CRIB & "',SDEPTH='" & OMA_SDEPTH & "',NPD='" & OMA_NPD & "',FCOCIN='" & OMA_FCOCIN & "',FCOCUP='" & OMA_FCOCUP & "'," & _
              "DRILL = '" & OMA_DRILL & "' "
            If My.Settings.HasMEIEdger And My.Settings.MEI_OMA_Service Then
                OMA.SetACCN(RxNum.Text)
                UpdtStr &= ", ACCN ='" & OMA.GetAccountNum().Replace(vbCrLf, "") & "', RXNM = '" & OMA.GetRxNum.Replace(vbCrLf, "") & "', _ETYP2 = '" & OMA.Get_ETYP2.Replace(vbCrLf, "") & "', _CKBEV = '" & OMA.Get_CKBEV.Replace(vbCrLf, "") & "' " & _
                        ", _PINONOF = '" & OMA.Get_PINONOF.Replace(vbCrLf, "") & "', _FPINONF = '" & OMA.Get_FPINONF.Replace(vbCrLf, "") & "', CTHICK = '" & OMA.GetCTHICK.Replace(vbCrLf, "") & "', THKP = '" & OMA.GetTHKP.Replace(vbCrLf, "") & "' " & _
                        ", _VARINC = '" & OMA.Get_VARINC.Replace(vbCrLf, "") & "', _VANGLE = '" & OMA.Get_VANGLE.Replace(vbCrLf, "") & "', _LCOAT = '" & OMA.Get_LCOAT.Replace(vbCrLf, "") & "', _AUTOBFR = '" & OMA.Get_AUTOBFR.Replace(vbCrLf, "") & "' " & _
                        ", _CIRCON = '" & OMA.Get_CIRCON.Replace(vbCrLf, "") & "', LMATTYPE = '" & OMA.GetLMATTYPE.Replace(vbCrLf, "") & "', BEVM = '" & OMA.GetBEVM.Replace(vbCrLf, "") & "', FRNT = '" & OMA.GetFRNT.Replace(vbCrLf, "") & "' " & _
                        ", BSIZ = '" & OMA.GetBSIZ.Replace(vbCrLf, "") & "', PINB = '" & OMA.GetPINB.Replace(vbCrLf, "") & "', FPINB = '" & OMA.GetFPINB.Replace(vbCrLf, "") & "', GDEPTH = '" & OMA.GetGDEPTH.Replace(vbCrLf, "") & "' " & _
                        ", GWIDTH = '" & OMA.GetGWIDTH.Replace(vbCrLf, "") & "', ETYP = '" & OMA.GetETYP.Replace(vbCrLf, "") & "', BEVP = '" & OMA.GetBEVP.Replace(vbCrLf, "") & "', POLISH = '" & OMA.GetPOLISH.Replace(vbCrLf, "") & "' " & _
                        ", FCRV = '" & OMA.GetFCRV.Replace(vbCrLf, "") & "' "
            Else
                UpdtStr &= ",ETYP='ETYP=" & ETYP & "',BEVP='" & OMA_BEVP & "',POLISH='" & OMA_POLISH & "' "
            End If
            UpdtStr &= " WHERE jobnum = '" & Me.StuffRx(RxWECO.Text) & "'"



            If My.Settings.HasMEIEdger Then
                Dim UpdtStrTemp As String = ""
                Dim temp As String = ""
                Dim templ As String = "", tempr As String = ""
                'OMA.SetFTYP(FTYP)
                'OMA.SetETYP(ETYP, ETYP)
                temp = OMA_LTYP.Replace("LTYP=", "") : tempr = temp.Substring(0, temp.IndexOf(";")) : templ = temp.Substring(temp.IndexOf(";") + 1)
                OMA.SetLTYP(tempr, templ)
                temp = OMA_IN_OUT.Replace("FBOCIN=", "") : tempr = temp.Substring(0, temp.IndexOf(";")) : templ = temp.Substring(temp.IndexOf(";") + 1)
                OMA.SetFBOCIN(tempr, templ)
                temp = OMA_UP_DOWN.Replace("FBSGUP=", "") : tempr = temp.Substring(0, temp.IndexOf(";")) : templ = temp.Substring(temp.IndexOf(";") + 1)
                OMA.SetFBSGUP(tempr, templ)
                Try
                    temp = OMA_SWIDHT.Replace("SWIDTH=", "") : tempr = temp.Substring(0, temp.IndexOf(";")) : templ = temp.Substring(temp.IndexOf(";") + 1)
                Catch ex As Exception : tempr = 0 : templ = 0 : Finally : OMA.SetSWIDTH(tempr, templ) : End Try
                'If tempr = templ And templ <> 0 Then
                temp = OMA_LMATID.Replace("LMATID=", "") : tempr = temp.Substring(0, temp.IndexOf(";")) : templ = temp.Substring(temp.IndexOf(";") + 1)
                OMA.SetLMATID(tempr, templ)
                'Else
                '   OMA.SetLMATID(0, 0)
                'End If
                'lmattype_l = 5
                'temp = OMA_POLISH.Replace("POLISH=", "")
                'OMA.SetPOLISH(temp)
                OMA.SetPOLISH(My.Settings.POLISH)
                'temp = OMA_BEVP.Replace("BEVP=", "")
                'OMA.SetBEVP(temp, temp)
                temp = OMA_IPD.Replace("IPD=", "") : tempr = temp.Substring(0, temp.IndexOf(";")) : templ = temp.Substring(temp.IndexOf(";") + 1)
                OMA.SetIPD(tempr, templ)
                Try
                    temp = OMA_BCOCIN.Replace("BCOCIN=", "") : tempr = temp.Substring(0, temp.IndexOf(";")) : templ = temp.Substring(temp.IndexOf(";") + 1)
                Catch ex As Exception : tempr = 0 : templ = 0 : Finally : OMA.SetBCOCIN(tempr, templ) : End Try
                Try
                    temp = OMA_BCOCUP.Replace("BCOCUP=", "") : tempr = temp.Substring(0, temp.IndexOf(";")) : templ = temp.Substring(temp.IndexOf(";") + 1)
                Catch ex As Exception : tempr = 0 : templ = 0 : Finally : OMA.SetBCOCUP(tempr, templ) : End Try
                Try
                    temp = OMA_FCSGIN.Replace("FCSGIN=", "") : tempr = temp.Substring(0, temp.IndexOf(";")) : templ = temp.Substring(temp.IndexOf(";") + 1)
                Catch ex As Exception : tempr = 0 : templ = 0 : Finally : OMA.SetFCSGIN(tempr, templ) : End Try
                Try
                    temp = OMA_FCSGUP.Replace("FCSGUP=", "") : tempr = temp.Substring(0, temp.IndexOf(";")) : templ = temp.Substring(temp.IndexOf(";") + 1)
                Catch ex As Exception : tempr = 0 : templ = 0 : Finally : OMA.SetFCSGUP(tempr, templ) : End Try
                Try
                    temp = OMA_DIA.Replace("DIA=", "") : tempr = temp.Substring(0, temp.IndexOf(";")) : templ = temp.Substring(temp.IndexOf(";") + 1)
                Catch ex As Exception : tempr = 0 : templ = 0 : Finally : OMA.SetDIA(tempr, templ) : End Try
                Try
                    temp = OMA_CRIB.Replace("CRIB=", "") : tempr = temp.Substring(0, temp.IndexOf(";")) : templ = temp.Substring(temp.IndexOf(";") + 1)
                Catch ex As Exception : tempr = 0 : templ = 0 : Finally : OMA.SetCRIB(tempr, templ) : End Try
                Try
                    temp = OMA_SDEPTH.Replace("SDEPTH=", "") : tempr = temp.Substring(0, temp.IndexOf(";")) : templ = temp.Substring(temp.IndexOf(";") + 1)
                Catch ex As Exception : tempr = 0 : templ = 0 : Finally : OMA.SetSDEPTH(tempr, templ) : End Try
                Try
                    temp = OMA_NPD.Replace("NPD=", "") : tempr = temp.Substring(0, temp.IndexOf(";")) : templ = temp.Substring(temp.IndexOf(";") + 1)
                Catch ex As Exception : tempr = 0 : templ = 0 : Finally : OMA.SetNPD(tempr, templ) : End Try
                Try
                    temp = OMA_FCOCIN.Replace("FCOCIN=", "") : tempr = temp.Substring(0, temp.IndexOf(";")) : templ = temp.Substring(temp.IndexOf(";") + 1)
                Catch ex As Exception : tempr = 0 : templ = 0 : Finally : OMA.SetFCOCIN(tempr, templ) : End Try
                Try
                    temp = OMA_FCOCUP.Replace("FCOCUP=", "") : tempr = temp.Substring(0, temp.IndexOf(";")) : templ = temp.Substring(temp.IndexOf(";") + 1)
                Catch ex As Exception : tempr = 0 : templ = 0 : Finally : OMA.SetFCOCUP(tempr, templ) : End Try
                Try
                    temp = OMA_DRILL.Replace("DRILL=", "")
                Catch ex As Exception
                    temp = ""
                Finally
                    OMA.SetDRILL(temp)
                End Try




            End If

            ds = t.SQLDS(UpdtStr, "t1")

            Dim sqlstr As String = "select trcleft,trcright from omatraces with(nolock) where jobnum = '" & Me.StuffRx(RxWECO.Text) & "'"
            Dim trcleft As String = ""
            Dim trcright As String = ""
            ds = t.SQLDS(sqlstr, "t1")

            ' Ojo ... si Rows.Count=0 el trazo no existe. Esto sucede con las virtuales que no son paquetes.
            If ds.Tables("t1").Rows.Count > 0 Then
                With ds.Tables("t1").Rows(0)
                    trcleft = .Item("trcleft")
                    trcright = .Item("trcright")
                End With
            End If

        Catch ex As Exception
            Failed = True
            FailedMessage = "Ocurrio un error al actualizar el tipo de armazon en la tabla de Trazos OMA" & vbCrLf & ex.Message
            'MsgBox("Ocurrio un error al actualizar el tipo de armazon en la tabla de Trazos OMA" & vbCrLf & ex.Message, MsgBoxStyle.Critical, "ERROR")
        Finally
            t.CloseConn()
            t = Nothing
            If Failed Then
                MsgBox(FailedMessage, MsgBoxStyle.Exclamation)
            End If
        End Try
    End Sub
    Private Function GetWebRxRow(ByVal OrdenLab As String) As Integer
        Dim i As Integer = 0
        For i = 0 To WO.DGCostco.RowCount - 1
            If WO.DGCostco.Item("OrdenLab", i).Value = OrdenLab Then
                Return i
            End If
        Next
        Return -1
    End Function

    Public Function RecalcTotal(ByVal ordernum As Integer) As Boolean
        Dim Cnn As SqlClient.SqlConnection = Nothing
        Dim SqlCmd As SqlClient.SqlCommand
        Dim Sucess As Boolean = True

        Dim newTotal As Double
        Dim SqlStrConn = Laboratorios.ConnStr
        Try
            Cnn = New SqlClient.SqlConnection(SqlStrConn)
            Cnn.Open()

            SqlCmd = New SqlClient.SqlCommand("select (select sum(orderqty*unitprice) from orderdtl where ordernum=@Ordernum and voidline=0 and company=@Company) + coalesce((SELECT sum(miscamt) from ordermsc  where ordernum=@Ordernum and company=@Company),0)", Cnn)
            SqlCmd.CommandType = CommandType.Text
            SqlCmd.CommandTimeout = My.Settings.DBCommandTimeout

            SqlCmd.Parameters.AddWithValue("@Company", "TRECEUX")
            SqlCmd.Parameters.AddWithValue("@Ordernum", ordernum)
            newTotal = SqlCmd.ExecuteScalar()

            If IsNumeric(newTotal) Then
                SqlCmd.Dispose()
                SqlCmd = New SqlClient.SqlCommand("UPDATE orderhed SET ccamount=@newtotal WHERE ordernum=@ordernum and company=@company", Cnn)
                SqlCmd.CommandType = CommandType.Text
                SqlCmd.CommandTimeout = My.Settings.DBCommandTimeout

                SqlCmd.Parameters.AddWithValue("@Company", "TRECEUX")
                SqlCmd.Parameters.AddWithValue("@Ordernum", ordernum)
                SqlCmd.Parameters.AddWithValue("@newtotal", newTotal)
                SqlCmd.ExecuteNonQuery()
            End If


        Catch ex As SqlClient.SqlException
            Sucess = False
            Throw New Exception(ex.Message)
        Finally

            Cnn.Close()
        End Try

        Return Sucess
    End Function


    Private Function Ejecutar_Rx() As Integer
        Dim orden As Integer
        Try
            AddOpciones(False)

            PedidoCreate = Pedido
            If WorkingOnLine Then
                orden = Crear_Orden_Job(GenPNStr, QtyStr, JobRev, JNStr, ChooseServer, DataBase)
            Else
                orden = Crear_Orden_Job(GenPNStr, QtyStr, JobRev, JNStr, My.Settings.LocalServer, My.Settings.LocalDBName)
            End If
            If RxType = RxTypes.WebRx Then
                Dim x As Integer = GetWebRxRow(RxID.Text)
                If x > -1 Then
                    WO.DGCostco.Item("OrdenLab", x).Value = "Procesado"
                End If

            End If



            If CInt(orden) = 0 Then
                OrderOK = False
                Throw New Exception("!!! No fue posible crear la orden de venta , Es necesario capturarla de nuevo !!!")
            Else
                OrderOK = True
                RecalcTotal(orden)
            End If
            '--------------------------------------------
            Return orden
        Catch ex As Exception
            Dim i As Integer
            i = InStr(ex.Message, "Timeout", CompareMethod.Text)
            If i <> 0 Then
                Me.ChangeWorkingStatus(WorkingStatusType.OffLine)
                orden = Crear_Orden_Job(GenPNStr, QtyStr, JobRev, JNStr, My.Settings.LocalServer, My.Settings.LocalDBName)
                OrderOK = True
                'Generar_Impresion(orden)
                'If Not UpdateLastIndex(orden) Then
                '    MsgBox("No se pudo actualizar el numero en la tabla LastIndex", MsgBoxStyle.Critical)
                'End If
                Return orden
            Else
                i = InStr(UCase(ex.Message), "DEADLOCK", CompareMethod.Text)
                If i <> 0 Then
                    Me.ChangeWorkingStatus(WorkingStatusType.OffLine)
                    orden = Crear_Orden_Job(GenPNStr, QtyStr, JobRev, JNStr, My.Settings.LocalServer, My.Settings.LocalDBName)
                    OrderOK = True
                    '     Generar_Impresion(orden)
                    'If Not UpdateLastIndex(orden) Then
                    '    MsgBox("No se pudo actualizar el numero en la tabla LastIndex", MsgBoxStyle.Critical)
                    'End If
                    Return orden
                Else
                    i = InStr(UCase(ex.Message), "LaborCodes", CompareMethod.Text)
                    If i <> 0 Then
                        Me.ChangeWorkingStatus(WorkingStatusType.OffLine)
                        orden = Crear_Orden_Job(GenPNStr, QtyStr, JobRev, JNStr, My.Settings.LocalServer, My.Settings.LocalDBName)
                        OrderOK = True
                        'Generar_Impresion(orden)
                        'If Not UpdateLastIndex(orden) Then
                        '    MsgBox("No se pudo actualizar el numero en la tabla LastIndex", MsgBoxStyle.Critical)
                        'End If
                        Return orden
                    Else
                        OrderOK = False
                        Crear_Log(Now & " : Error al crear la orden. " & vbCrLf & ex.Message)
                        Throw New Exception("Error al crear la orden." & vbCrLf & ex.Message)
                    End If
                End If
            End If
        End Try
    End Function

    '************************************
    '*Funcion que genera un log si hubo un error al generar una orden de venta
    Private Sub Crear_Log(ByVal msg As String)
        Dim obj As File
        Dim str As System.IO.StreamWriter
        str = File.AppendText(System.Windows.Forms.Application.StartupPath & "\LabRxLog.txt")

        str.WriteLine(msg)
        str.Close()
        str.Dispose()
        obj = Nothing
    End Sub

    'asigna valor a variables Mcentro y BCName,  ultimos campos solicitados x atanacio
    Public Sub imprimeBloqueCentro()
        Dim valor As String = ""
        Dim strElementName As String = ""
        'Dim m_node As XmlNode
        'Dim Value As String
        Dim side As String = Nothing
        Dim bcname As String

        Dim lapSurf, base, cross As Boolean
        Dim powerR(10), powerL(10) As String
        Dim cont As Integer
        lapSurf = False
        base = False
        cross = False
        cont = 0
        bcname = ""
        MCentroD = ""
        MCentroI = ""
        BCnameD = ""
        BCnameI = ""

        Try


            Dim reader As New XmlTextReader("Calcula_Response.xml")
            reader.MoveToContent()

            Dim elemento As String = Nothing
            Dim endElement = Nothing
            'Dim addressData As Collection, elementName As String

            Do While reader.Read

                Select Case reader.NodeType

                    Case XmlNodeType.Element 'And reader.Name = "side"
                        elemento = reader.Name.Trim
                        If elemento = "lapSurf" Then
                            lapSurf = True
                        ElseIf elemento = "base" Then
                            base = True
                        ElseIf elemento = "cross" Then
                            cross = True
                        End If
                    Case XmlNodeType.Text
                        If elemento = "side" Then
                            side = reader.Value.Trim
                            cont = 0
                        ElseIf elemento = "bCName" Then
                            bcname = reader.Value.Trim
                        ElseIf elemento = "power" And lapSurf And (base Or cross) Then
                            If side = "right" Then
                                powerR(cont) = reader.Value.Trim
                                cont = cont + 1
                            Else : powerL(cont) = reader.Value.Trim
                                cont = cont + 1

                            End If

                        End If
                    Case XmlNodeType.EndElement
                        endElement = True
                        elemento = reader.Name.Trim

                        'Console.WriteLine("XmlNodeType.EndElement " & reader.Name)

                End Select


                If endElement And elemento = "LensDsg" Then
                    If side = "right" Then

                        'If Not bcname.IsNullOrEmpty Then
                        If BCnameD = "" And bcname <> "" Then
                            BCnameD = bcname
                            bcname = ""
                        End If
                        'End If
                        If (UBound(powerR) > 0) Then
                            If Val(powerR(2)) > 0 And Val(powerR(3)) > 0 Then
                                'MCentroD = powerR(2) & "-" & powerR(3)
                                MCentroD = Convert.ToString((Val(powerR(2)) * 100)) & "-" & Convert.ToString((Val(powerR(3)) * 100))

                            ElseIf Val(powerR(2)) > 0 And Val(powerR(3)) = 0 Then
                                MCentroD = Convert.ToString((Val(powerR(2)) * 100))
                            End If
                            lapSurf = False
                        End If
                    Else
                        If BCnameI = "" And bcname <> "" Then
                            BCnameI = bcname
                            bcname = ""
                        End If
                        If (UBound(powerL) > 0) Then
                            If Val(powerL(2)) > 0 And Val(powerL(3)) > 0 Then
                                ' MCentroI = powerL(2) & "-" & powerL(3)
                                MCentroI = Convert.ToString((Val(powerL(2)) * 100)) & "-" & Convert.ToString((Val(powerL(3)) * 100))
                            ElseIf Val(powerL(2)) > 0 And Val(powerL(3)) = 0 Then
                                'MCentroI = powerL(2).ToString
                                MCentroI = Convert.ToString((Val(powerL(2)) * 100))

                            End If
                            lapSurf = False
                        End If
                    End If
                End If



            Loop

        Catch ex As Exception

        End Try
    End Sub



    '************************************
    '************************************
    '*nuevo procedimiento de impresion


    Private Sub Generar_Impresion(ByVal orden As String)
        Dim rx As New PrintLabRx(Me.PrintObj.Receta)
        Dim LeftEye As New LensDsg
        Dim RightEye As New LensDsg

        Dim HasLeftEye As Boolean = False
        Dim HasRightEye As Boolean = False

        Dim monL, monR As String
        Dim ZebraPrint As Boolean = My.Settings.ZebraPrint



        'imprimeBloqueCentro()

        If (PrintObj.MoldeD.Anillo_HD Is Nothing) Then
            rx.AnilloD_HD = ""
        Else
            rx.AnilloD_HD = Me.PrintObj.MoldeD.Anillo_HD
            'Si no hay informaci�n del MoldeD del Anillo en HD entonces... dejamos la variable  rx.AnilloD_HD como estaba (Nothing)
        End If

        If (PrintObj.MoldeI.Anillo_HD Is Nothing) Then
            rx.AnilloI_HD = ""
        Else
            rx.AnilloI_HD = Me.PrintObj.MoldeI.Anillo_HD
            'Si no hay informaci�n del MoldeD del Anillo en HD entonces... dejamos la variable  rx.AnilloD_HD como estaba (Nothing)
        End If

        'OJO DERECHO
        If Me.CheckREye.Checked And Not Optisur.RightSide Is Nothing Then
            rx.BloqueD = Me.PrintObj.MoldeD.Anillo
            rx.CilindroD = Me.PrintObj.LD.Cilindro
            rx.DhDer = Math.Round(Me.PrintObj.FormaD.DH, 0)
            rx.DvDer = Math.Round(Me.PrintObj.FormaD.DV, 0)
            rx.EjeDer = Me.PrintObj.LD.Eje
            rx.EsferaD = Me.PrintObj.LD.Esfera
            rx.GrosorCentroD = String.Format("{0:0.0}", Math.Round(Me.PrintObj.MoldeD.GrosorCentral, 1))

            ' Variable publica para guardar el grosoer en la tabla orderhed campo userdecimal1
            ' GrosCentralD = Me.PrintObj.MoldeD.GrosorCentral

            rx.MoldeD = String.Format("{0:000}", (Me.PrintObj.MoldeD.M_Base) * 100) & "-" & String.Format("{0:000}", (Me.PrintObj.MoldeD.M_Cruz) * 100)

            If (Me.PrintObj.MoldeD.M_Cruz = 0) Then
                rx.MoldeD = String.Format("{0:000}", (Me.PrintObj.MoldeD.M_Base) * 100)
            End If

            rx.MoldeD_HD = String.Format("{0:000}", (Me.PrintObj.MoldeD.M_Base_HD) * 100) & "-" & String.Format("{0:000}", (Me.PrintObj.MoldeD.M_Cruz_HD) * 100)
            If (Me.PrintObj.MoldeD.M_Cruz_HD = 0) Then
                rx.MoldeD_HD = String.Format("{0:000}", (Me.PrintObj.MoldeD.M_Base_HD) * 100)
            End If

            rx.BcNameD = Me.PrintObj.MoldeD.Bname

            'rx.DDPR_X = "0"
            'rx.DDPR_Y = RightEye.powDsgDRP.point.y
            'rx.DNPR_SPH = RightEye.powDsgDRP.sph
            'rx.DDPR_CYL = RightEye.powDsgDRP.cyl
            'rx.DDPR_AXIS = RightEye.powDsgDRP.axis
            'rx.DNPR_X = RightEye.powDsgNRP.point.x
            'rx.DNPR_Y = RightEye.powDsgNRP.point.y
            'rx.DNPR_SPH = RightEye.powDsgNRP.sph
            'rx.DNPR_CYL = RightEye.powDsgNRP.cyl
            'rx.DNPR_AXIS = RightEye.powDsgNRP.axis

            'rx.BcNameD=Me.PrintObj.
            ' Impresion del molde high definition
            'If Me.PrintObj.MoldeD.M_Base_HD = 0.0 Then
            '    rx.MoldeD_HD = ""
            'Else
            '    If Me.PrintObj.MoldeD.M_Cruz_HD > 0 Then
            '        rx.MoldeD_HD = String.Format("{0:000}", (Me.PrintObj.MoldeD.M_Base_HD) * 100) & "-" & String.Format("{0:000}", (Me.PrintObj.MoldeD.M_Cruz_HD) * 100)
            '    Else
            '        rx.MoldeD_HD = String.Format("{0:000}", (Me.PrintObj.MoldeD.M_Base_HD) * 100)
            '    End If
            'End If

            'rx.MoldeED_HD = rx.MoldeD_HD
            rx.MoldeED_HD = String.Format("{0:000}", (Me.PrintObj.MoldeD.M_Base_Ex) * 100) & "-" & String.Format("{0:000}", (Me.PrintObj.MoldeD.M_Cruz_Ex) * 100)
            rx.OrillaMaxD = String.Format("{0:0.0}", Math.Round(Me.PrintObj.MoldeD.GrosorMaximo, 1))
            rx.OrillaMinD = String.Format("{0:0.0}", Math.Round(Me.PrintObj.MoldeD.GrosorMinimo, 1))
            rx.PrismaD = Me.PrintObj.PrismaD

            If Me.PrintObj.BaseR = "" Then Me.PrintObj.BaseR = "0"

            rx.BaseD = (Me.PrintObj.BaseR)
            rx.EjePrismaD = Me.PrintObj.MoldeD.EjePrisma
            rx.AdicionR = Me.PrintObj.AdicionR
            rx.PotFD = Me.PrintObj.PotFD
            rx.MolED = Me.PrintObj.MolExD

            rx.Comentarios = ""

            rx.MensajeD = ""
        End If

        'OJO IZQUIERDO
        If CheckLEye.Checked And Not Optisur.LeftSide Is Nothing Then
            rx.BloqueI = Me.PrintObj.MoldeI.Anillo
            rx.AnilloI_HD = Me.PrintObj.MoldeI.Anillo_HD
            rx.CilindroI = Me.PrintObj.LI.Cilindro
            rx.DhIzq = Math.Round(Me.PrintObj.FormaI.DH, 0)
            rx.DvIzq = Math.Round(Me.PrintObj.FormaI.DV, 0)
            rx.EjeIzq = Me.PrintObj.LI.Eje
            rx.EsferaI = Me.PrintObj.LI.Esfera
            rx.GrosorCentroI = String.Format("{0:0.0}", Math.Round(Me.PrintObj.MoldeI.GrosorCentral, 1))
            'varible public para guardar el grosoer en la tabla orderhed campo userdecimal1
            'GrosCentralI = MoldeI.GrosorCentral
            rx.MoldeI = String.Format("{0:000}", (Me.PrintObj.MoldeI.M_Base) * 100) & "-" & String.Format("{0:000}", (Me.PrintObj.MoldeI.M_Cruz) * 100)
            If (Me.PrintObj.MoldeI.M_Cruz) = 0 Then
                rx.MoldeI = String.Format("{0:000}", (Me.PrintObj.MoldeI.M_Base) * 100)
            End If
            rx.MoldeI_HD = String.Format("{0:000}", (Me.PrintObj.MoldeI.M_Base_HD) * 100) & "-" & String.Format("{0:000}", (Me.PrintObj.MoldeI.M_Cruz_HD) * 100)
            If (Me.PrintObj.MoldeI.M_Cruz_HD) = 0 Then
                rx.MoldeI_HD = String.Format("{0:000}", (Me.PrintObj.MoldeI.M_Base_HD) * 100)
            End If
            rx.BcNameI = Me.PrintObj.MoldeI.Bname

            'rx.IDPR_X = String.Format("", LeftEye.powDsgDRP.point.x)
            'rx.IDPR_Y = LeftEye.powDsgDRP.point.y
            'rx.INPR_SPH = LeftEye.powDsgDRP.sph
            'rx.IDPR_CYL = LeftEye.powDsgDRP.cyl
            'rx.IDPR_AXIS = LeftEye.powDsgDRP.axis
            'rx.INPR_X = LeftEye.powDsgNRP.point.x
            'rx.INPR_Y = LeftEye.powDsgNRP.point.y
            'rx.INPR_SPH = LeftEye.powDsgNRP.sph
            'rx.INPR_CYL = LeftEye.powDsgNRP.cyl
            'rx.INPR_AXIS = LeftEye.powDsgNRP.axis

            'molde de high definition
            'If Me.PrintObj.MoldeI.M_Base_HD = 0.0 Then
            '    rx.MoldeI_HD = ""
            'Else
            '    If Me.PrintObj.MoldeI.M_Cruz_HD > 0 Then
            '        rx.MoldeI_HD = String.Format("{0:000}", (Me.PrintObj.MoldeI.M_Base_HD) * 100) & "-" & String.Format("{0:000}", (Me.PrintObj.MoldeI.M_Cruz_HD) * 100)
            '    Else
            '        rx.MoldeI_HD = String.Format("{0:000}", (Me.PrintObj.MoldeI.M_Base_HD) * 100)
            '    End If
            'End If


            'rx.MoldeEI_HD = rx.MoldeI_HD
            rx.MoldeEI_HD = String.Format("{0:000}", (Me.PrintObj.MoldeI.M_Base_Ex) * 100) & "-" & String.Format("{0:000}", (Me.PrintObj.MoldeI.M_Cruz_Ex) * 100)

            '**********************************
            rx.OrillaMaxI = String.Format("{0:0.0}", Math.Round(Me.PrintObj.MoldeI.GrosorMaximo, 1))
            rx.OrillaMinI = String.Format("{0:0.0}", Math.Round(Me.PrintObj.MoldeI.GrosorMinimo, 1))
            'rx.PrismaI = Math.Round(MoldeI.Prisma, 2)
            rx.PrismaI = Me.PrintObj.PrismaI 'txtprisI.Text
            If Me.PrintObj.BaseL = "" Then Me.PrintObj.BaseL = "0"
            rx.BaseI = (Me.PrintObj.BaseL)
            rx.EjePrismaI = Me.PrintObj.MoldeI.EjePrisma
            rx.AdicionL = Me.PrintObj.AdicionI 'RxInfo.AdicionL
            rx.PotFI = Me.PrintObj.PotFI '"I " & txtesfI.Text & ", " & txtcilI.Text
            rx.MolEI = Me.PrintObj.MolExI '"I " & lblmoldeExI.Text
            rx.MensajeI = ""
            'Select Case Me.PrintObj.MoldeI.CveMensaje
            '    Case 0
            '        rx.MensajeI = ""
            '    Case 9
            '        rx.MensajeI = ""
            '        rx.Comentarios &= "I: " & Me.PrintObj.MoldeI.Mensaje & vbCrLf
            '    Case Else
            '        rx.MensajeI = "I: " & Me.PrintObj.MoldeI.Mensaje
            'End Select

        End If

        'VARIABLES PARA EL PROTOCOLO OMA
        '***********************************************************************

        Dim DHR, DVR, DHL, DVL, LTYPR, LTYPL, LMATIDR, LMATIDL, CRIBR, CRIBL As String

        Dim Der, Izq As ItemWithValue
        Dim IPDDer, IPDIzq As String
        Dim NPDDer, NPDIzq As String

        If My.Settings.OpticalProtocol = "OMA" Then
            If CheckREye.Checked Then
                If IsReceivingVirtualRx Then
                    OMA_EyeSide = "R"
                End If
                Der = Me.ListRightParts.SelectedItem
                DHR = rx.DhDer
                DVR = rx.DvDer
                Dim x As String = ""
                LTYPR = Me.GetCustomValues(CustomValues.OMADis_ID, Der.Value.ToString)
                LMATIDR = Me.GetCustomValues(CustomValues.OMAMat_ID, Der.Value.ToString)
                If LMATIDR.ToUpper = "ERROR" Then LMATIDR = "9"

                'GetOMALabelsFromParts(Der.Value.ToString, x, x, x, x, x, x)
                'GetOMALabelsFromParts(p.RightPill.Text, LMATID_L, LMATNAME_L, LMATTYPE_L, LNAM_L, LTYP_L, FRNT_L)
                IPDDer = MonoRight.Text
                NPDDer = DIPCerca.Text / 2
                CRIBR = "0"
            Else 'dejamos las variables de l ojo como esttaban en un inicio
                DHR = OMA_DHR
                DVR = OMA_DVR
                LTYPR = OMA_LTYPR
                LMATIDR = OMA_LMATIDR
                IPDDer = OMA_IPDR
                NPDDer = OMA_NPDR
                CRIBR = OMA_CRIBR
            End If
            If CheckLEye.Checked Then
                If IsReceivingVirtualRx Then
                    If OMA_EyeSide = "" Then
                        OMA_EyeSide = "L"
                    Else
                        OMA_EyeSide = "B"
                    End If

                End If

                Izq = Me.ListLeftParts.SelectedItem
                DHL = rx.DhIzq
                DVL = rx.DvIzq
                LTYPL = Me.GetCustomValues(CustomValues.OMADis_ID, Izq.Value.ToString)
                LMATIDL = Me.GetCustomValues(CustomValues.OMAMat_ID, Izq.Value.ToString)
                If LMATIDL.ToUpper = "ERROR" Then LMATIDL = "9"

                'GetOMALabelsFromParts(Izq.Value.ToString, LMATIDL, x, x, x, LTYPL, x)
                IPDIzq = MonoLeft.Text
                NPDIzq = DIPCerca.Text / 2
                CRIBL = "0"
            Else
                DHL = OMA_DHL
                DVL = OMA_DVL
                LTYPL = OMA_LTYPL
                LMATIDL = OMA_LMATIDL
                IPDIzq = OMA_IPDL
                NPDIzq = OMA_NPDL
                CRIBL = OMA_CRIBL
            End If
            'identificamos que lado fue trazado 
            If OMA_EyeSide <> "" Then
                'LOS VALORES QUE VAN MULTIPLICADOS POR MENOS UNO VERIFICAMOS QUE NO SEA CADENA VACIA
                If DHR = "" Then
                    DHR = ""
                Else
                    DHR = DHR * -1
                End If
                If DVR = "" Then
                    DVR = ""
                Else
                    DVR = DVR * -1
                End If
                If DVL = "" Then
                    DVL = ""
                Else
                    DVL = DVL * -1
                End If

                OMA_IN_OUT = "FBOCIN=" & DHR & ";" & DHL
                OMA_UP_DOWN = "FBSGUP=" & DVR & ";" & DVL
                OMA_CRIB = "CRIB=" & CRIBR & ";" & CRIBL & ""
                OMA_LTYP = "LTYP=" & LTYPR & ";" & LTYPL
                OMA_LMATID = "LMATID=" & LMATIDR & ";" & LMATIDL
                'PARA EL ABRILLANTADO O POLISH
                If Me.PrintObj.Abrillantado = "ABRILLANTADO" Then
                    OMA_POLISH = "POLISH=1"
                Else
                    OMA_POLISH = "POLISH=0"
                End If
                OMA_BEVP = "BEVP=10"
                OMA_IPD = "IPD=" & IPDDer & ";" & IPDIzq
                OMA_NPD = "NPD=" & NPDDer & ";" & NPDIzq

                'LLENAMOS LA VARIABLE PARA EL DRILL si el armazon es de tipo perforado
                If ComboArmazon2.Texto <> "Perforado" Then
                    OMA_PER_X1 = "?"
                    OMA_PER_Y1 = "?"
                    OMA_PER_X2 = "?"
                    OMA_PER_Y2 = "?"
                    OMA_PER_ANGLAT = "?"
                    OMA_PER_ANGVER = "?"
                    OMA_PER_BFA = "?"
                    OMA_PER_DEPTH = "?"
                    OMA_PER_DIA = "?"
                End If
                'el lado en la variable side para armazones de metal y plastico debe de ser de '?'

                Dim DrillEyeSide As String = OMA_EyeSide
                Select Case ComboArmazon2.Texto
                    Case "Metal", "Plastico"
                        DrillEyeSide = "?"
                End Select
                OMA_DRILL = "DRILL=" & DrillEyeSide & ";" & OMA_PER_X1 & ";" & OMA_PER_Y1 & ";" & OMA_PER_DIA & _
                ";" & OMA_PER_X2 & ";" & OMA_PER_Y2 & ";" & OMA_PER_DEPTH & ";" & OMA_PER_BFA & ";" & OMA_PER_ANGLAT & ";" & OMA_PER_ANGVER


            Else
                MsgBox("Error al actualizar las variables de OMA en la base de datos, no existe un lado trazado", MsgBoxStyle.Critical, "OMA")
            End If
        End If
        '***********************************************************************

        'VERIFICAMOS SI SOLO FUE SELECCIONADO UNO DE LOS OJOS PARA LIMPIAR LAS VAR. DEL OJO NO SELECCIONADO
        'If Not (CheckREye.Checked And CheckLEye.Checked) Then
        If (Not CheckREye.Checked) Or (Optisur.RightSide Is Nothing) Then
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
            rx.MoldeED_HD = ""
            rx.MensajeD = ""

            rx.DDPR_X = ""
            rx.DDPR_Y = ""
            rx.DDPR_SPH = ""
            rx.DDPR_CYL = ""
            rx.DDPR_AXIS = ""
            rx.DNPR_X = ""
            rx.DNPR_Y = ""
            rx.DNPR_SPH = ""
            rx.DNPR_CYL = ""
            rx.DNPR_AXIS = ""

        End If
        If (Not CheckLeftEye.Checked) Or (Optisur.LeftSide Is Nothing) Then
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
            rx.MoldeEI_HD = ""
            rx.MensajeI = ""

            rx.IDPR_X = ""
            rx.IDPR_Y = ""
            rx.IDPR_SPH = ""
            rx.IDPR_CYL = ""
            rx.IDPR_AXIS = ""
            rx.INPR_X = ""
            rx.INPR_Y = ""
            rx.INPR_SPH = ""
            rx.INPR_CYL = ""
            rx.INPR_AXIS = ""
        End If
        'End If

        'Modificaci�n para imprimir el n�mero de PO en lugar del Num. de orden
        rx.Vantage = orden
        'Si la bandera
        If (My.Settings.printPOinstead) Then rx.PONumber = RxID.Text Else rx.PONumber = orden
        rx.RxNumLocal = RxNumLocal
        rx.CajaDBL = String.Format("{0:00.0}", CDbl(Me.PrintObj.Puente))
        If Optisur.IsManual Then
            rx.CajaA = String.Format("{0:00.0}", Math.Round(Optisur.GeoFrame.hrzBox, 1))
            rx.CajaB = String.Format("{0:00.0}", Math.Round(Optisur.GeoFrame.vrtBox, 1))
            rx.CajaED = String.Format("{0:00.0}", Math.Round(Optisur.GeoFrame.efDiam, 1))
        Else
            rx.CajaA = String.Format("{0:00.0}", Math.Round(Optisur.Calculos.GeoFrame.hrzBox, 1))
            rx.CajaB = String.Format("{0:00.0}", Math.Round(Optisur.Calculos.GeoFrame.vrtBox, 1))
            rx.CajaED = String.Format("{0:00.0}", Math.Round(Optisur.Calculos.GeoFrame.efDiam, 1))
        End If
        rx.TinteColor = Me.PrintObj.TinteColor
        rx.TinteNum = Me.PrintObj.TinteNum
        rx.Gradiente = Me.PrintObj.Gradiente
        rx.Dise�o = Me.PrintObj.Dise�o
        rx.Material = Me.PrintObj.Material
        'imprimimos si el color del soleo es gris o cafe
        If rx.Material = "SOLEO" Then
            Dim color As New ItemWithValue
            If CheckREye.Checked Then
                color = ListRightParts.Items(ListRightParts.SelectedIndex)
            ElseIf CheckLEye.Checked Then
                color = ListLeftParts.Items(ListLeftParts.SelectedIndex)
            End If
            If InStr(color.Name, "GRIS", CompareMethod.Text) > 0 Then rx.ColorSOLEO = "GRIS"
            If InStr(color.Name, "CAFE", CompareMethod.Text) > 0 Then rx.ColorSOLEO = "CAFE"

        Else
            rx.ColorSOLEO = ""
        End If
        If UCase(Me.PrintObj.Material) = "TRIVEX" Then
            rx.MatTX = "TRIVEX"
        Else
            rx.MatTX = ""
        End If
        rx.Biselado = Me.PrintObj.Biselado
        'If IsReceivingVirtualRx Then
        '    rx.Cliente = Me.TextLabClient.Text
        'Else
        '    rx.Cliente = Me.PrintObj.Cliente
        'End If
        If ComboClients.Text = "" Then
            rx.Cliente = TextLabClient.Text
        Else
            rx.Cliente = ComboClients.Text
        End If
        rx.DipceDiple = Me.PrintObj.DIP
        rx.Armazon = ComboArmazon2.Texto
        'verifiocamos si el armazon es ranurado o perforad para imprimirlo en junto con la etq de biselado
        If Me.PrintObj.Biselado <> "" Then
            Select Case UCase(Me.PrintObj.Armazon)
                Case "PERFORADO"
                    rx.Biselado = "PERFORADO"
                Case "RANURADO"
                    rx.Biselado = "RANURADO"
            End Select
        End If

        rx.Comentarios &= TextComentarios.Text      ' Aqui se agrega al campo comentarios pues puede tener mensajes del guiador desde antes

        rx.SpecialLabel = SpecialLabel

        rx.HoraSalida = Me.PrintObj.HoraSalida
        rx.FechaSalida = Me.PrintObj.FechaSalida
        rx.FechaEntrada = Me.PrintObj.FechaEntrada
        rx.HoraEntrada = Me.PrintObj.HoraEntrada

        'verificamos si la receta se envio por el virtual, esto para imprimir al lab que se mando
        If IsVirtualRx Then
            If LabID() <> ARLab Then
                If Not IsLocalReceive Then
                    If IsModifying Then
                        If ComboErrores.SelectedValue <> My.Settings.RecapturaSP_ID Then
                            rx.FechaSalida = "LAB"
                            rx.HoraSalida = UCase(PlantID(ARLab).ToString.Substring(0, 3))
                        End If
                    Else
                        rx.FechaSalida = "LAB"
                        rx.HoraSalida = UCase(PlantID(ARLab).ToString.Substring(0, 3))
                    End If
                End If
            End If
        End If

        rx.AntiReflajante = Me.PrintObj.AR
        rx.AltOblea = Math.Round(Me.PrintObj.FormaD.Altura, 0)
        rx.Abrillantado = Me.PrintObj.Abrillantado
        rx.Rx = StuffRx(Me.PrintObj.Receta)

        If rx.RxNumLocal = 0 Then
            rx.RxNumLocal = CInt(rx.Rx)
        End If
        rx.RxNumLocal = StuffRx(rx.RxNumLocal)
        rx.RXIDEN = Me.PrintObj.RxIdent
        'GENERACION DEL CONSECUTIVO (TRABAJO + LOTE) EJ. A01-01 DE LA RECETA
        rx.Lote = Me.PrintObj.Lote

        ''''' se modifica la impresion  de la Rx, se cambia dip por Mono''''
        ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
        monR = MonoRight.Text
        monL = MonoLeft.Text

        If rx.Dise�o.Contains("Flat") Then
            If MonoRight.Text = "0" And MonoLeft.Text = "0" Then
                monR = CDbl(DIPCerca.Text) / 2
                monL = MonoRight.Text

            Else
                If MonoLeft.Text = "0" And MonoRight.Text <> "0" Then
                    monL = CDbl(DIPCerca.Text) - CDbl(MonoRight.Text)
                ElseIf MonoLeft.Text = "0" Then
                    monL = CDbl(DIPCerca.Text) / 2
                End If
                If MonoRight.Text = "0" And MonoLeft.Text <> "0" Then
                    monR = CDbl(DIPCerca.Text) - CDbl(MonoLeft.Text)
                ElseIf MonoRight.Text = "0" Then
                    monR = CDbl(DIPCerca.Text) / 2
                End If
            End If
        Else
            If MonoRight.Text = "0" And MonoLeft.Text = "0" Then
                monR = CDbl(DIPLejos.Text) / 2
                monL = MonoRight.Text
            Else
                If MonoLeft.Text = "0" And MonoRight.Text <> "0" Then
                    monL = CDbl(DIPLejos.Text) - CDbl(MonoRight.Text)
                ElseIf MonoLeft.Text = "0" Then
                    monL = CDbl(DIPLejos.Text) / 2
                End If
                If MonoRight.Text = "0" And MonoLeft.Text <> "0" Then
                    monR = CDbl(DIPLejos.Text) - CDbl(MonoLeft.Text)
                ElseIf MonoRight.Text = "0" Then
                    monR = CDbl(DIPLejos.Text) / 2
                End If
            End If
        End If

        rx.monoLR = monR + "-" + monL




        'Obtenemos los objetos de los ojos del nuevo calcot
        If My.Settings.EnableOptisur Then
            RightEye = Optisur.LensDsgRight
            LeftEye = Optisur.LensDsgLeft

            If Me.SpecialDesignID > 0 Then
                rx.DisenoEspecial = CBX_SpecialDesign.Texto
            Else
                rx.DisenoEspecial = ""
            End If

        End If



        'rx.PrintPreview()
        Try

            'COLOCAMOS CADENA VACIA EN LAS VARIABLES CON CERO PARA NO IMPRIMIRLAS EN EL REPORTER
            If CSng(rx.EsferaD) = 0 And Not CheckREye.Checked Then rx.EsferaD = ""
            If CSng(rx.EsferaI) = 0 And Not CheckLEye.Checked Then rx.EsferaI = ""
            If CSng(rx.CilindroD) = 0 And Not CheckREye.Checked Then rx.CilindroD = ""
            If CSng(rx.CilindroI) = 0 And Not CheckLEye.Checked Then rx.CilindroI = ""
            If CInt(rx.EjeDer) = 0 Then rx.EjeDer = ""
            If CInt(rx.EjeIzq) = 0 Then rx.EjeIzq = ""
            If CInt(rx.AdicionR) = 0 Then rx.AdicionR = ""
            If CInt(rx.AdicionL) = 0 Then rx.AdicionL = ""
            If rx.MoldeD = "000" Then rx.MoldeD = ""
            If rx.MoldeI = "000" Then rx.MoldeI = ""
            If rx.MoldeD_HD = "000" Then rx.MoldeD_HD = ""
            If rx.MoldeI_HD = "000" Then rx.MoldeI_HD = ""
            If rx.BaseD = "0.00" Then rx.BaseD = ""
            If rx.BaseI = "0.00" Then rx.BaseI = ""
            If CInt(rx.EjePrismaD) = 0 And CSng(rx.PrismaD) = 0 Then rx.EjePrismaD = ""
            If CInt(rx.EjePrismaI) = 0 And CSng(rx.PrismaI) = 0 Then rx.EjePrismaI = ""
            If CSng(rx.PrismaD) = 0 Then rx.PrismaD = ""
            If CSng(rx.PrismaI) = 0 Then rx.PrismaI = ""
            If rx.BloqueD = "0" Then rx.BloqueD = ""
            If rx.BloqueI = "0" Then rx.BloqueI = ""
            If CInt(rx.AltOblea) = 0 Then rx.AltOblea = ""

            If CInt(rx.DhDer) = 0 And Not CheckREye.Checked Then rx.DhDer = ""
            If CInt(rx.DhIzq) = 0 And Not CheckLEye.Checked Then rx.DhIzq = ""
            If CInt(rx.DvDer) = 0 And Not CheckREye.Checked Then rx.DvDer = ""
            If CInt(rx.DvIzq) = 0 And Not CheckLEye.Checked Then rx.DvIzq = ""
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

            If Not Optisur.LensDsgRight.lapSurf Is Nothing Then
                If Optisur.LensDsgRight.lapSurf(0).profName <> Nothing Then
                    rx.MoldeD = Optisur.LensDsgRight.lapSurf(0).profName
                End If
            Else
                rx.MoldeD = ""
            End If
            If Not Optisur.LensDsgLeft.lapSurf Is Nothing Then
                If Optisur.LensDsgLeft.lapSurf(0).profName <> Nothing Then
                    rx.MoldeI = Optisur.LensDsgLeft.lapSurf(0).profName
                End If
            Else
                rx.MoldeI = ""
            End If

        Catch ex As Exception
            Throw New Exception("Generar la Impresion 1" + ex.ToString(), ex)
        End Try

        ' Pedro Far�as Lozano Ene 22 2013
        ' Las ordenes que se reciben virtuales no muestran la pantalla de ajustes miscel�neos, s�lo las �rdenes locales
        If (My.Settings.AjustesApertura And (IsLocalRx)) Then
            ActivaAjustes(LabID, orden)
        End If

        rx.DineroIVApercent = 0
        rx.DineroPU = 0
        rx.DineroConcepto = ""

        If Not My.Settings.AskToPrint Then  'No preguntes, s�lo imprime
            Try
                If Not ZebraPrint Then 'Impresi�n de receta en formato de Crystal Reports .rpt
                    'MANDAMOS A UN HILO LA IMPRESION DE LA RECETA
                    '                    Dim myt As Threading.Thread
                    'myt = New Threading.Thread(New Threading.ThreadStart(AddressOf rx.PrintRx))
                    'myt.Start()
                    rx.PrintRx()

                Else
                    'Impresi�n de etiqueta en impresora Zebra Thermal Label Printer
                    If rx.EjeDer = "" Then rx.EjeDer = "0.00"
                    If rx.EjeIzq = "" Then rx.EjeIzq = "0.00"
                    Dim StringCode As String = ""
                    Dim Failed As Boolean = False
                    Dim FailedMessage As String = ""

                    Try
                        Dim P As New ZebraPrintLabel
                        Dim Custnum As Integer = Me.ComboClients.SelectedValue
                        Dim Sexo As String = ""
                        Dim LambdaFile As String = ""
                        Dim FrameSize As String = ""


                        ' ---------------------
                        ' Cambios para FreeForm
                        ' P.Rx = rx.Rx
                        ' P.RxNumLocal = rx.RxNumLocal
                        ' ---------------------
                        P.Rx = rx.Rx
                        P.RxNumLocal = rx.RXIDEN
                        P.RefDigital = RxWECO.Text
                        P.Ordernum = rx.Vantage
                        P.PO = rx.PONumber

                        P.Armazon = rx.Armazon
                        P.AnilloD_HD = rx.AnilloD_HD
                        P.AnilloI_HD = rx.AnilloI_HD
                        P.AnilloD = rx.BloqueD
                        If P.AnilloD.Length > 7 Then P.AnilloD = rx.BloqueD.Substring(0, 7)
                        P.AnilloI = rx.BloqueI
                        If P.AnilloI.Length > 7 Then P.AnilloI = rx.BloqueI.Substring(0, 7)
                        P.MoldeD = rx.MoldeD
                        P.MoldeI = rx.MoldeI
                        P.GrosorCentroD = rx.GrosorCentroD
                        P.GrosorCentroI = rx.GrosorCentroI
                        P.BaseD = rx.BaseD
                        P.BaseI = rx.BaseI
                        P.BloqueD = rx.BcNameD
                        P.BloqueI = rx.BcNameI
                        P.EsferaD = rx.EsferaD
                        P.EsferaI = rx.EsferaI
                        P.CilindroD = rx.CilindroD
                        P.CilindroI = rx.CilindroI
                        If CSng(P.EsferaD) = 0 Then P.EsferaD = "PL"
                        If CSng(P.EsferaI) = 0 Then P.EsferaI = "PL"
                        If CSng(P.CilindroD) = 0 Then P.CilindroD = "PL"
                        If CSng(P.CilindroI) = 0 Then P.CilindroI = "PL"
                        P.EjeD = CInt(rx.EjeDer)
                        P.EjeI = CInt(rx.EjeIzq)
                        P.DIP = rx.DipceDiple.Substring(0, rx.DipceDiple.IndexOf("-"))
                        P.DH_D = rx.DhDer
                        P.DH_I = rx.DhIzq
                        P.DV_D = rx.DvDer
                        P.DV_I = rx.DvIzq

                        ' Buscamos informaci�n adicional si es para Ver Bien
                        If My.Settings.LocalDBName.ToLower = "verbien" Then
                            Try
                                Dim t As New SqlDB(My.Settings.LocalServer, My.Settings.DBUser, My.Settings.DBPassword, My.Settings.LocalDBName)
                                Dim sqlstr As String = "SELECT a.Rx, a.ClaveEscuela, a.Turno, a.Nombre, a.APaterno, a.AMaterno, a.Grado, a.Grupo, a.opt_id,a.NoEmpleado,a.idpaqueteitem as Armazon," & _
                                "b.framenum AS NumArmazon, d.nivel,d.nom_esc, d.municipio, c.Estado, e.material, f.diseno AS Dise�o, d.municipio AS Tienda, d.nom_esc AS Sucursal, d.clave AS NumSucursal, COALESCE (r.sexo, '') AS sexo, coalesce(rel.archivo_medidas,'') as archivo_medidas FROM TblMaterials AS e WITH (nolock) INNER JOIN VBPAM_Paciente AS a WITH (nolock) INNER JOIN TblWebOrders AS b WITH (nolock) ON a.Rx = b.ponumber AND a.custnum = b.custnum ON e.cl_mat = b.material INNER JOIN TblDesigns AS f WITH (nolock) ON b.design = f.cl_diseno LEFT OUTER JOIN VBPAM_Estados AS c WITH (nolock) INNER JOIN VBPAM_Escuelas AS d WITH (nolock) ON c.id_estado = d.edo ON a.ClaveEscuela = d.clave LEFT OUTER JOIN Rx AS r WITH (nolock) ON a.Rx = r.Rx inner join vbpam_armazonrel rel with(nolock) on a.idpaqueteitem = rel.idpaqueteitem WHERE(b.custnum = " & Me.ComboClients.SelectedValue & ") AND (b.ponumber = '" & rx.RXIDEN & "')"
                                Dim ds As DataSet
                                t.OpenConn()
                                ds = t.SQLDS(sqlstr, "T1")

                                If ds.Tables("t1").Rows.Count > 0 Then
                                    'MsgBox("Si se encontro la informacionen la vista", MsgBoxStyle.Information)


                                    With ds.Tables("t1").Rows(0)


                                        P.Grado = .Item("Grado")
                                        P.Grupo = .Item("Grupo")
                                        P.Escolaridad = .Item("Nivel")
                                        P.NombreCompleto = .Item("Nombre") + " " + .Item("APaterno") + " " + .Item("AMaterno")
                                        P.Nombres = .Item("Nombre")
                                        P.APaterno = .Item("APaterno")
                                        P.AMaterno = .Item("AMaterno")
                                        P.Armazon = .Item("Armazon")
                                        P.ID_Escuela = .Item("ClaveEscuela")
                                        P.Escuela = .Item("nom_esc")
                                        P.Municipio = .Item("municipio")
                                        P.Estado = .Item("Estado")
                                        P.Fecha = Now.Day & " " & String.Format("{0:M}", Now).Substring(0, 3) & ", " & Now.Year
                                        P.Custnum = Custnum
                                        ' Datos para la etiqueta de apertura de BINS
                                        '#ADD#JGARIBALDI#25 de Septiembre de 2014#Funcionalidad nueva del programa : Ahora asigna el bin correspondiente
                                        Dim WHBINS As Aperturas_WHBIN = New Aperturas_WHBIN(rx.Vantage, rx.RXIDEN)
                                        P.WHBIN_Bin = WHBINS.sBIN
                                    End With

                                End If
                            Catch ex As Exception
                                MessageBox.Show("No se encontr� informaci�n del alumno/escuela", "Error al buscar alumno/escuela VB", MessageBoxButtons.OK)
                            End Try
                        End If



                        'MOD # PEDRO FARIAS LOZANO # 25/Febrero/2013 # Se imprime el formato seg�n la variable de configuraci�n
                        'MOD # PEDRO FARIAS LOZANO # 26/Febrero/2013 $ Se manda a un hilo la impresi�n

                        'Dim mytZ As Threading.Thread
                        'mytZ = New Threading.Thread(New Threading.ThreadStart(AddressOf P.PrintMEILabel))
                        'mytZ.Start()

                        P.PrintMEILabel()
                    Catch ex As Exception
                        Failed = True
                        FailedMessage = ex.Message


                    Finally
                        If Failed Then
                            MsgBox("Error de impresi�n: " + FailedMessage, MsgBoxStyle.Information)
                        End If
                    End Try

                End If
            Catch ex As Exception
                Throw New Exception("Generar la Impresion 2" + ex.ToString(), ex)
            End Try
        Else
            If MsgBox("Deseas imprimir la Rx?", MsgBoxStyle.YesNo + MsgBoxStyle.Question) = MsgBoxResult.Yes Then
                Try
                    rx.PrintRx()
                Catch ex As Exception
                    Throw New Exception("Generar la Impresion 3" + ex.ToString(), ex)
                End Try
            End If
        End If
        'rx.PrintRx()
        'rx = Nothing
    End Sub


    Function StuffRx(ByVal Rx As String) As String
        Dim i As Integer
        For i = Rx.Length To 5
            Rx = "0" & Rx
        Next
        Return Rx
    End Function

    Sub AddOpciones(ByVal IsUpdate As Boolean)
        Dim Mat As Integer
        Dim Dis As Integer
        Dim Cla As String
        Dim AR As Integer
        Dim ProdCode As String = ""
        Dim x As ItemWithValue
        Dim Qty As String = 0
        Dim Diopt As Double = 0
        Dim TotalDiop As Single = 0
        Dim TotalKappa As Integer = 0
        Dim TotalAR As Integer = 0
        Dim ARSelected As Boolean = False
        Dim PaqQty As String = "1"
        Dim FrameQty As String = "1"

        If (CheckLEye.Checked And CheckREye.Checked) Or (OriginalLeftPart <> "" And OriginalRightPart <> "") Then
            Qty = 2
        ElseIf CheckLEye.Checked Or CheckREye.Checked Then
            Qty = 1
        End If
        If CheckAR.Checked Or CheckARGold.Checked Or CheckARMatiz.Checked Then
            ARSelected = True
        End If

        If CInt(Qty) > 0 Then
            If IsUpdate Then
                Qty &= ",0"
                PaqQty &= ",0"
                FrameQty &= ",0"
            End If

            If CheckLEye.Checked Or (OriginalLeftPart <> "") Then
                If CheckLEye.Checked Then
                    x = Me.ListLeftParts.SelectedItem
                    Mat = Me.GetCustomValues(CustomValues.MaterialID, x.Value.ToString)
                    Dis = Me.GetCustomValues(CustomValues.Dise�oID, x.Value.ToString)
                    Cla = Me.GetCustomValues(CustomValues.Clase, x.Value.ToString)
                    AR = Me.GetCustomValues(CustomValues.AR, x.Value.ToString)
                    ProdCode = Me.GetCustomValues(CustomValues.ProdCode, x.Value.ToString)
                Else
                    Mat = Me.GetCustomValues(CustomValues.MaterialID, OriginalLeftPart)
                    Dis = Me.GetCustomValues(CustomValues.Dise�oID, OriginalLeftPart)
                    Cla = Me.GetCustomValues(CustomValues.Clase, OriginalLeftPart)
                    AR = Me.GetCustomValues(CustomValues.AR, OriginalLeftPart)
                    ProdCode = Me.GetCustomValues(CustomValues.ProdCode, OriginalLeftPart)
                End If

                TotalAR += AR

                '--------------------------------------------------
                'Chequeo de Tipo de Biselado
                '--------------------------------------------------
                If CheckBiselado.Checked Then
                    If (Mat = 4) And (Dis = 1) And (((Cla = "T") And (Not ProdCode.EndsWith("TST"))) Or (Cla <> "T") And (Not ARSelected)) And (ComboArmazon2.ComboBox1.SelectedIndex <> 3) And (ComboArmazon2.ComboBox1.SelectedIndex <> 2) Then
                        Pedido &= ",BISVS,1"
                        If IsUpdate Then
                            Pedido &= ",0"
                        End If
                    ElseIf (Mat = 4) And (Dis = 4) And (((Cla = "T") And (Not ProdCode.EndsWith("TST"))) Or ((Cla <> "T") And (Not ARSelected))) And (ComboArmazon2.ComboBox1.SelectedIndex <> 3) And (ComboArmazon2.ComboBox1.SelectedIndex <> 2) Then
                        Pedido &= ",BISFT,1"
                        If IsUpdate Then
                            Pedido &= ",0"
                        End If
                    ElseIf (ComboArmazon2.ComboBox1.SelectedIndex <> 3) And (ComboArmazon2.ComboBox1.SelectedIndex <> 2) Then
                        Pedido &= ",BISPR,1"
                        If IsUpdate Then
                            Pedido &= ",0"
                        End If
                    End If
                End If

                '--------------------------------------------------
                'Chequeo de Dioptrias
                '--------------------------------------------------
                '    Dim d As Double = 0
                '    If Dis >= 11 And Dis <= 13 Then
                '        d = 0
                '    Else
                '        Diopt = Math.Abs(CDbl(EsferaLeft.Text))
                '        If Now.Date < "1/15/2007" Then
                '            Diopt += Math.Abs(CDbl(CilindroRight.Text))
                '        End If

                '        ' Esto es las Combinadas de CR39, TRIVEX y SOLEO
                '        '----------------------------------------
                '        If Mat <> 3 And Diopt > 4 Then
                '            d = CDbl(Diopt) - CDbl(4)
                '            If d - CInt(d) > 0 Then
                '                d += 1
                '                d = CInt(d.ToString.Substring(0, d.ToString.IndexOf(".")))
                '            End If
                '        ElseIf (Mat = 3) And (Diopt > 10) Then

                '            ' Esto es las combinadas de Hi Index
                '            '----------------------------------------
                '            d = Diopt - 10
                '            If d - CInt(d) > 0 Then
                '                d += 1
                '                d = CInt(d.ToString.Substring(0, d.ToString.IndexOf(".")))
                '            End If
                '        End If

                '        Diopt = Math.Abs(CDbl(CilindroLeft.Text))
                '        ' Estas son las de cilindro para CR39,TRIVEX y SOLEO
                '        '-----------------------------------------------------
                '        If (Diopt > 3) And (Mat <> 3) Then
                '            d += Diopt - 3
                '            If d - CInt(d) <> 0 Then
                '                d += 1
                '                d = CInt(d.ToString.Substring(0, d.ToString.IndexOf(".")))
                '            End If
                '        End If
                '    End If
                '    If d > 0 Then
                '        TotalDiop += d
                '    End If
                'End If

                ' Pedro Far�as Lozano  Dic 21 2011 --- Uff y dec�an que hoy se acababa el mundo las mentes mediocres
                ' La l�gica anterior calculaba mal las dioptr�as pues arrojaba 1 de m�s en ciertos casos, y el ojo izquierdo se calculaba
                ' diferente del ojo derecho

                If Not (Dis >= 11 And Dis <= 13) Then
                    Dim DC, DE As Double

                    DE = Math.Abs(CDbl(EsferaLeft.Text))
                    DC = Math.Abs(CDbl(CilindroLeft.Text))

                    ' Esto es las Combinadas de CR39, TRIVEX y SOLEO
                    '----------------------------------------
                    If Mat <> 3 Then
                        If DE > 4 Then
                            DE = Math.Ceiling(DE - 4.0)
                        Else
                            DE = 0
                        End If

                        If DC > 3 Then
                            DC = Math.Ceiling(DC - 3.0)
                        Else
                            DC = 0
                        End If

                        Diopt = DC + DE

                    Else ' Mat=3 Hi Index MR 10 1.56

                        ' Esto es las combinadas de Hi Index
                        '----------------------------------------
                        DE = Math.Ceiling(DE - 10.0)
                        Diopt = DE
                    End If

                    DE = Math.Abs(CDbl(EsferaRight.Text))
                    DC = Math.Abs(CDbl(CilindroRight.Text))

                    ' Esto es las Combinadas de CR39, TRIVEX y SOLEO
                    '----------------------------------------
                    If Mat <> 3 Then
                        If DE > 4 Then
                            DE = Math.Ceiling(DE - 4.0)
                        Else
                            DE = 0
                        End If

                        If DC > 3 Then
                            DC = Math.Ceiling(DC - 3.0)
                        Else
                            DC = 0
                        End If
                    
                        Diopt += DC + DE

                    Else ' Mat=3 Hi Index MR 10 1.56

                        ' Esto es las combinadas de Hi Index
                        '----------------------------------------
                        DE = Math.Ceiling(DE - 10.0)
                        Diopt += DE
                    End If



                End If
                If Diopt > 0 Then
                    TotalDiop = Diopt
                End If
            End If

            If CheckREye.Checked Or (OriginalRightPart <> "") Then
                If CheckREye.Checked Then
                    x = Me.ListRightParts.SelectedItem
                    Mat = Me.GetCustomValues(CustomValues.MaterialID, x.Value.ToString)
                    Dis = Me.GetCustomValues(CustomValues.Dise�oID, x.Value.ToString)
                    Cla = Me.GetCustomValues(CustomValues.Clase, x.Value.ToString)
                    AR = Me.GetCustomValues(CustomValues.AR, x.Value.ToString)
                    ProdCode = Me.GetCustomValues(CustomValues.ProdCode, x.Value.ToString)
                Else
                    Mat = Me.GetCustomValues(CustomValues.MaterialID, OriginalRightPart)
                    Dis = Me.GetCustomValues(CustomValues.Dise�oID, OriginalRightPart)
                    Cla = Me.GetCustomValues(CustomValues.Clase, OriginalRightPart)
                    AR = Me.GetCustomValues(CustomValues.AR, OriginalRightPart)
                    ProdCode = Me.GetCustomValues(CustomValues.ProdCode, OriginalRightPart)
                End If
                TotalAR += AR

                '--------------------------------------------------
                'Chequeo de Tipo de Biselado
                '--------------------------------------------------
                If CheckBiselado.Checked Then
                    If (Mat = 4) And (Dis = 1) And (((Cla = "T") And (Not ProdCode.EndsWith("TST"))) Or (Cla <> "T") And (Not ARSelected)) And (ComboArmazon2.ComboBox1.SelectedIndex <> 3) And (ComboArmazon2.ComboBox1.SelectedIndex <> 2) Then
                        Pedido &= ",BISVS,1"
                        If IsUpdate Then
                            Pedido &= ",0"
                        End If
                    ElseIf (Mat = 4) And (Dis = 4) And (((Cla = "T") And (Not ProdCode.EndsWith("TST"))) Or (Cla <> "T") And (Not ARSelected)) And (ComboArmazon2.ComboBox1.SelectedIndex <> 3) And (ComboArmazon2.ComboBox1.SelectedIndex <> 2) Then
                        Pedido &= ",BISFT,1"
                        If IsUpdate Then
                            Pedido &= ",0"
                        End If
                    ElseIf (ComboArmazon2.ComboBox1.SelectedIndex <> 3) And (ComboArmazon2.ComboBox1.SelectedIndex <> 2) Then
                        Pedido &= ",BISPR,1"
                        If IsUpdate Then
                            Pedido &= ",0"
                        End If
                    End If
                End If



            End If

            '--------------------------------------------------
            'Chequeo de Dioptrias
            '--------------------------------------------------
            If TotalDiop > 0 Then
                Pedido &= ",DIOPT," & CInt(TotalDiop)
                If IsUpdate Then
                    Pedido &= ",0"
                End If
            End If
            '--------------------------------------------------
            'Chequeo de Biselado Perforado
            '--------------------------------------------------
            If (ComboArmazon2.ComboBox1.SelectedIndex = 3) And (CheckBiselado.Checked) Then
                Pedido &= ",BISPE," & Qty
            End If

            '--------------------------------------------------
            'Chequeo de Biselado Ranurado
            '--------------------------------------------------
            If ComboArmazon2.ComboBox1.SelectedIndex = 2 And (CheckBiselado.Checked) Then
                Pedido &= ",BISRA," & Qty
            End If

            '--------------------------------------------------
            'Chequeo de Abrillantado de Bisel
            '--------------------------------------------------
            If CheckAbril.Checked Then
                Pedido &= ",ABRIL," & Qty
            End If

            '--------------------------------------------------
            'Chequeo de Optimizacion
            '--------------------------------------------------
            If SD_Partnum.ToUpper() = "HDRX" Then
                Pedido &= ",HDRX," & Qty
            End If

            ' Agregado por fgarcia. March 23, 2011.
            If (SpecialDesignID > 0) And (SD_Partnum.ToUpper() <> "HDRX") Then
                Pedido &= "," & SD_Partnum & "," & Qty
            End If

            '--------------------------------------------------
            'Chequeo de Abrillantado de Antireflejante
            '--------------------------------------------------
            If CheckAR.Checked Then
                Pedido &= ",ARAUG," & Qty
            ElseIf CheckARGold.Checked Then
                Pedido &= ",ARGOL," & Qty
            ElseIf CheckARMatiz.Checked Then
                Pedido &= ",ARMAT," & Qty
            End If
            If (CheckAR.Checked) And (TotalAR > Qty) Then
                Pedido &= ",ARAUG," & TotalAR - Qty
            End If

            '--------------------------------------------------
            'Chequeo de Tintes
            '--------------------------------------------------
            If ComboTinte.SelectedValue <> 0 Then
                If RadioGradiente.Checked Then
                    Pedido &= ",GRADI," & Qty
                Else
                    Pedido &= ",TINTE," & Qty
                End If
                If CheckRLX.Checked Then
                    Pedido &= ",TINKA," & Qty
                End If
            End If

            '--------------------------------------------------
            'Chequeo de Proteccion Antirayas
            '--------------------------------------------------
            If CheckRLX.Checked Then
                Pedido &= ",KAPPA," & Qty
            End If

            '--------------------------------------------------
            'Agregar No. Parte del paquete si es que lleva
            '--------------------------------------------------
            If numpaq.Length > 0 Then
                Pedido &= "," & numpaq & "," & PaqQty
            End If

            '--------------------------------------------------
            'Agregar No. Parte del armazon si es que lleva
            '--------------------------------------------------
            If MyFrame.NumArmazon And MyFrame.ReadStatus = Frames.ThisFrameReadStatus.Ok Then
                Pedido &= "," & MyFrame.PartNum & "," & FrameQty
            End If

        End If
    End Sub

    'Sub AddOpciones(ByVal IsUpdate As Boolean)
    '    Dim Mat As Integer
    '    Dim Dis As Integer
    '    Dim Cla As String
    '    Dim AR As Integer
    '    Dim ProdCode As String = ""
    '    Dim x As ItemWithValue
    '    Dim Qty As String = 0
    '    Dim Diopt As Double = 0
    '    Dim TotalDiop As Single = 0
    '    Dim TotalKappa As Integer = 0
    '    Dim TotalAR As Integer = 0
    '    Dim ARSelected As Boolean = False
    '    If (CheckLeftEye.Checked And CheckRightEye.Checked) Or (OriginalLeftPart <> "" And OriginalRightPart <> "") Then
    '        Qty = 2
    '    ElseIf CheckLeftEye.Checked Or CheckRightEye.Checked Then
    '        Qty = 1
    '    End If
    '    If CheckAR.Checked Or CheckARGold.Checked Then
    '        ARSelected = True
    '    End If

    '    If CInt(Qty) > 0 Then
    '        If IsUpdate Then
    '            Qty &= ",0"
    '        End If
    '        If CheckLeftEye.Checked Or (OriginalLeftPart <> "") Then
    '            If CheckLeftEye.Checked Then
    '                x = Me.ListLeftParts.SelectedItem
    '                Mat = Me.GetCustomValues(CustomValues.MaterialID, x.Value.ToString)
    '                Dis = Me.GetCustomValues(CustomValues.Dise�oID, x.Value.ToString)
    '                Cla = Me.GetCustomValues(CustomValues.Clase, x.Value.ToString)
    '                AR = Me.GetCustomValues(CustomValues.AR, x.Value.ToString)
    '                ProdCode = Me.GetCustomValues(CustomValues.ProdCode, x.Value.ToString)
    '            Else
    '                Mat = Me.GetCustomValues(CustomValues.MaterialID, OriginalLeftPart)
    '                Dis = Me.GetCustomValues(CustomValues.Dise�oID, OriginalLeftPart)
    '                Cla = Me.GetCustomValues(CustomValues.Clase, OriginalLeftPart)
    '                AR = Me.GetCustomValues(CustomValues.AR, OriginalLeftPart)
    '                ProdCode = Me.GetCustomValues(CustomValues.ProdCode, OriginalLeftPart)
    '            End If
    '            TotalAR += AR
    '            '--------------------------------------------------
    '            'Chequeo de Tipo de Biselado
    '            '--------------------------------------------------

    '            If CheckBiselado.Checked Then
    '                If (Mat = 4) And (Dis = 1) And (((Cla = "T") And (Not ProdCode.EndsWith("TST"))) Or (Cla <> "T") And (Not ARSelected)) And (ComboTipoArmazon.SelectedIndex <> 3) And (ComboTipoArmazon.SelectedIndex <> 2) Then
    '                    Pedido &= ",BISVS,1"
    '                    If IsUpdate Then
    '                        Pedido &= ",0"
    '                    End If
    '                ElseIf (Mat = 4) And (Dis = 4) And (((Cla = "T") And (Not ProdCode.EndsWith("TST"))) Or ((Cla <> "T") And (Not ARSelected))) And (ComboTipoArmazon.SelectedIndex <> 3) And (ComboTipoArmazon.SelectedIndex <> 2) Then
    '                    Pedido &= ",BISFT,1"
    '                    If IsUpdate Then
    '                        Pedido &= ",0"
    '                    End If
    '                ElseIf (ComboTipoArmazon.SelectedIndex <> 3) And (ComboTipoArmazon.SelectedIndex <> 2) Then
    '                    Pedido &= ",BISPR,1"
    '                    If IsUpdate Then
    '                        Pedido &= ",0"
    '                    End If
    '                End If
    '            Else
    '                If (Mat = 4) And (Dis = 1) And (Cla = "T") And (Not ProdCode.EndsWith("TST")) Then
    '                    Pedido &= ",BISVS,1"
    '                    If IsUpdate Then
    '                        Pedido &= ",0"
    '                    End If
    '                End If
    '                If Mat = 4 And Dis = 4 And Cla = "T" And (Not ProdCode.EndsWith("TST")) Then
    '                    Pedido &= ",BISFT,1"
    '                    If IsUpdate Then
    '                        Pedido &= ",0"
    '                    End If
    '                End If
    '            End If
    '            '--------------------------------------------------
    '            'Chequeo de Dioptrias
    '            '--------------------------------------------------
    '            Dim d As Double = 0
    '            If Dis >= 11 And Dis <= 13 Then
    '                d = 0
    '            Else
    '                Diopt = Math.Abs(CDbl(EsferaLeft.Text)) + Math.Abs(CDbl(CilindroLeft.Text))
    '                ' Esto es las Combinadas de CR39, TRIVEX y SOLEO
    '                '----------------------------------------
    '                If Mat <> 3 And Diopt > 4 Then
    '                    d = CDbl(Diopt) - CDbl(4)
    '                ElseIf (Mat = 3) And (Diopt > 10) Then
    '                    ' Esto es las combinadas de Hi Index
    '                    '----------------------------------------
    '                    d = Diopt - 10
    '                End If
    '                Diopt = Math.Abs(CDbl(CilindroLeft.Text))
    '                ' Estas son las de cilindro para CR39,TRIVEX y SOLEO
    '                '-----------------------------------------------------
    '                If (Diopt > 3) And (Mat <> 3) Then
    '                    d += Diopt - 3
    '                End If
    '            End If
    '            If d > 0 Then
    '                TotalDiop += d
    '            End If
    '        End If
    '        If CheckRightEye.Checked Or (OriginalRightPart <> "") Then
    '            If CheckRightEye.Checked Then
    '                x = Me.ListRightParts.SelectedItem
    '                Mat = Me.GetCustomValues(CustomValues.MaterialID, x.Value.ToString)
    '                Dis = Me.GetCustomValues(CustomValues.Dise�oID, x.Value.ToString)
    '                Cla = Me.GetCustomValues(CustomValues.Clase, x.Value.ToString)
    '                AR = Me.GetCustomValues(CustomValues.AR, x.Value.ToString)
    '                ProdCode = Me.GetCustomValues(CustomValues.ProdCode, x.Value.ToString)
    '            Else
    '                Mat = Me.GetCustomValues(CustomValues.MaterialID, OriginalRightPart)
    '                Dis = Me.GetCustomValues(CustomValues.Dise�oID, OriginalRightPart)
    '                Cla = Me.GetCustomValues(CustomValues.Clase, OriginalRightPart)
    '                AR = Me.GetCustomValues(CustomValues.AR, OriginalRightPart)
    '                ProdCode = Me.GetCustomValues(CustomValues.ProdCode, OriginalRightPart)
    '            End If
    '            TotalAR += AR
    '            '--------------------------------------------------
    '            'Chequeo de Tipo de Biselado
    '            '--------------------------------------------------
    '            If CheckBiselado.Checked Then
    '                If (Mat = 4) And (Dis = 1) And (((Cla = "T") And (Not ProdCode.EndsWith("TST"))) Or (Cla <> "T") And (Not ARSelected)) And (ComboTipoArmazon.SelectedIndex <> 3) And (ComboTipoArmazon.SelectedIndex <> 2) Then
    '                    Pedido &= ",BISVS,1"
    '                    If IsUpdate Then
    '                        Pedido &= ",0"
    '                    End If
    '                ElseIf (Mat = 4) And (Dis = 4) And (((Cla = "T") And (Not ProdCode.EndsWith("TST"))) Or (Cla <> "T") And (Not ARSelected)) And (ComboTipoArmazon.SelectedIndex <> 3) And (ComboTipoArmazon.SelectedIndex <> 2) Then
    '                    Pedido &= ",BISFT,1"
    '                    If IsUpdate Then
    '                        Pedido &= ",0"
    '                    End If
    '                ElseIf (ComboTipoArmazon.SelectedIndex <> 3) And (ComboTipoArmazon.SelectedIndex <> 2) Then
    '                    Pedido &= ",BISPR,1"
    '                    If IsUpdate Then
    '                        Pedido &= ",0"
    '                    End If
    '                End If
    '            Else
    '                If (Mat = 4) And (Dis = 1) And (Cla = "T") And (Not ProdCode.EndsWith("TST")) Then
    '                    Pedido &= ",BISVS,1"
    '                    If IsUpdate Then
    '                        Pedido &= ",0"
    '                    End If
    '                End If
    '                If Mat = 4 And Dis = 4 And Cla = "T" And (Not ProdCode.EndsWith("TST")) Then
    '                    Pedido &= ",BISFT,1"
    '                    If IsUpdate Then
    '                        Pedido &= ",0"
    '                    End If
    '                End If

    '            End If
    '            '--------------------------------------------------
    '            'Chequeo de Dioptrias
    '            '--------------------------------------------------
    '            Dim d As Double = 0
    '            If Dis >= 11 And Dis <= 13 Then
    '                d = 0
    '            Else
    '                Diopt = Math.Abs(CDbl(EsferaRight.Text)) + Math.Abs(CDbl(CilindroRight.Text))
    '                ' Esto es las Combinadas de CR39, TRIVEX y SOLEO
    '                '----------------------------------------
    '                If Mat <> 3 And Diopt > 4 Then
    '                    d = CDbl(Diopt) - CDbl(4)
    '                ElseIf (Mat = 3) And (Diopt > 10) Then
    '                    ' Esto es las combinadas de Hi Index
    '                    '----------------------------------------
    '                    d = Diopt - 10
    '                End If
    '                Diopt = Math.Abs(CDbl(CilindroRight.Text))
    '                ' Estas son las de cilindro para CR39,TRIVEX y SOLEO
    '                '-----------------------------------------------------
    '                If (Diopt > 3) And (Mat <> 3) Then
    '                    d += Diopt - 3
    '                End If
    '            End If
    '            If d > 0 Then
    '                TotalDiop += d
    '            End If
    '        End If

    '        '--------------------------------------------------
    '        'Chequeo de Dioptrias
    '        '--------------------------------------------------
    '        If TotalDiop > 0 Then
    '            If TotalDiop - CInt(TotalDiop) > 0 Then
    '                TotalDiop += 1
    '            End If
    '            Pedido &= ",DIOPT," & CInt(TotalDiop)
    '            If IsUpdate Then
    '                Pedido &= ",0"
    '            End If
    '        End If

    '        '--------------------------------------------------
    '        'Chequeo de Biselado Perforado
    '        '--------------------------------------------------
    '        If (ComboTipoArmazon.SelectedIndex = 3) And (CheckBiselado.Checked) Then
    '            Pedido &= ",BISPE," & Qty
    '        End If

    '        '--------------------------------------------------
    '        'Chequeo de Biselado Ranurado
    '        '--------------------------------------------------
    '        If ComboTipoArmazon.SelectedIndex = 2 And (CheckBiselado.Checked) Then
    '            'If TotalAR <> 2 Then           ' Para paquete con bisel a 16 pesos
    '            Pedido &= ",BISRA," & Qty
    '            'Else                           ' Para paquete con bisel a 16 pesos
    '            '    Pedido &= ",RANAR,2"       ' Para paquete con bisel a 16 pesos
    '            'End If                         ' Para paquete con bisel a 16 pesos
    '        End If
    '        '--------------------------------------------------
    '        'Chequeo de Abrillantado de Bisel
    '        '--------------------------------------------------
    '        If CheckAbrillantado.Checked Then
    '            Pedido &= ",ABRIL," & Qty
    '        End If
    '        '--------------------------------------------------
    '        'Chequeo de Abrillantado de Antireflejante
    '        '--------------------------------------------------
    '        If CheckAR.Checked Then
    '            Pedido &= ",ARAUG," & Qty
    '        ElseIf CheckARGold.Checked Then
    '            Pedido &= ",ARGOL," & Qty
    '        End If
    '        If (CheckAR.Checked) And (TotalAR > Qty) Then
    '            Pedido &= ",ARAUG," & TotalAR - Qty
    '        End If
    '        '--------------------------------------------------
    '        'Chequeo de Tintes
    '        '--------------------------------------------------
    '        If CheckTintes.Checked Then
    '            If CheckGradiente.Checked Then
    '                Pedido &= ",GRADI," & Qty
    '            Else
    '                Pedido &= ",TINTE," & Qty
    '            End If
    '            If CheckCoated.Checked Then
    '                Pedido &= ",TINKA," & Qty
    '                'TotalKappa += Val(Qty)
    '            End If
    '        End If
    '        '--------------------------------------------------
    '        'Chequeo de Proteccion Antirayas
    '        '--------------------------------------------------
    '        If CheckCoated.Checked Then
    '            'TotalKappa += Val(Qty)
    '            Pedido &= ",KAPPA," & Qty
    '        End If
    '    End If
    'End Sub

    Private Sub RxSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RxSave.Click
        Dim Info As New RxResume
        Dim ds As New DataSet()
        ds.Tables.Add(New DataTable("t1"))
        'Label6.Visible = True
        RxNum.Visible = True
        'Label5.Visible = True
        RxWECO.Visible = True

        Pedido = ""
        PedidoCreate = ""
        Dim MyValue As ItemWithValue
        Dim GenLPNStr As String = ""
        Dim GenRPNStr As String = ""
        If (ListLeftParts.SelectedIndex > -1) Then
            MyValue = ListLeftParts.Items(ListLeftParts.SelectedIndex)
            GenLPNStr = GetGenericPartNum(MyValue.Value)
            If Val(MyValue.Value) > 0 Then
                Pedido = MyValue.Value & ",1"
            End If
        ElseIf OriginalLeftPart <> "" Then
            GenLPNStr = GetGenericPartNum(OriginalLeftPart)
            Pedido = OriginalLeftPart & ",1"
        End If
        If ListRightParts.SelectedIndex > -1 Then
            MyValue = ListRightParts.Items(ListRightParts.SelectedIndex)
            GenRPNStr = GetGenericPartNum(MyValue.Value)
            If Val(MyValue.Value) > 0 Then
                If Pedido <> "" Then
                    Pedido &= ","
                End If
                Pedido &= MyValue.Value & ",1"
            End If
        ElseIf OriginalRightPart <> "" Then
            GenRPNStr = GetGenericPartNum(OriginalRightPart)
            Pedido = OriginalRightPart & ",1"
        End If
        PedidoCreate = Pedido
        If Pedido <> "" Then
            GenPNStr = ""
            QtyStr = 0
            If GenLPNStr = GenRPNStr Then
                If GenLPNStr <> "" Then
                    GenPNStr = GenLPNStr
                    QtyStr = 2
                Else
                    QtyStr = 0
                End If
            Else
                If GenLPNStr <> "" Then
                    GenPNStr = GenLPNStr
                Else
                    GenPNStr = GenRPNStr
                End If
                QtyStr = 1
            End If
            JNStr = GetJobNumber()
            JobRev = GetRevision(GenPNStr)
            FinishSavingRx()
        End If
    End Sub
    Private Sub CheckTipoArmazon()
        ComboArmazon2.ComboBox1.Items.Clear()
        If RadioDigitalNew.Checked Then
            ComboArmazon2.ComboBox1.Items.Add("Metal")
            ComboArmazon2.ComboBox1.Items.Add("Plastico")
        Else
            ComboArmazon2.ComboBox1.Items.Add("Perforado")
            ComboArmazon2.ComboBox1.Items.Add("Ranurado")
        End If
        ComboArmazon2.ComboBox1.SelectedIndex = 0
    End Sub
    Private Sub TipoReceta_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RadioDigitalNew.CheckedChanged, RadioDigital.CheckedChanged
        If RadioDigitalNew.Checked = True Then
            If (IsModifying = ChangingTraces) Then
                PanelContorno.Enabled = False
                'GroupTrazos.Visible = True
                ComboTrazos.Enabled = True
                PanelInformacion.Visible = False
                AceptarManualNew.Visible = False
                AceptarDigitalNew.Visible = True
                'PanelArmazon.Size = New Size(406, 45)
            ElseIf IsModifying Then
                CambiarTrazosToolStripMenuItem_Click(sender, e)
                AceptarDigitalNew.Visible = True
            Else
                RadioManual.Checked = True
                MsgBox("Presione F1 para modificar el trazo", MsgBoxStyle.Exclamation)
            End If
            RadioManualNew.Checked = False
        Else

            RadioManualNew.Checked = True
            PanelContorno.Enabled = True
            'GroupTrazos.Visible = False
            ComboTrazos.Enabled = False
            PanelInformacion.Visible = False
            AceptarDigitalNew.Visible = False
            AceptarManualNew.Visible = True
            'PanelArmazon.Size = New Size(552, 45)
        End If
        'CheckTipoArmazon()
    End Sub

    Private Sub CheckTintes_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CheckTintes.CheckedChanged, ComboTinte.SelectedIndexChanged
        Try
            If ComboTinte.SelectedValue <> 0 Then
                CheckLevel1.Enabled = True
                CheckLevel2.Enabled = True
                CheckLevel3.Enabled = True
                'ComboTinte.Enabled = True
                RadioGradiente.Enabled = True
            Else
                CheckLevel1.Enabled = False
                CheckLevel2.Enabled = False
                CheckLevel3.Enabled = False
                'ComboTinte.Enabled = False
                RadioGradiente.Enabled = False
            End If

        Catch ex As Exception
            CheckLevel1.Enabled = True
            CheckLevel2.Enabled = True
            CheckLevel3.Enabled = True
            'ComboTinte.Enabled = True
            RadioGradiente.Enabled = True
        End Try
    End Sub

    Private Sub CheckAntiReflejante_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
        If CheckAR.Checked = True Then
            OperAntireflejante = True
        Else
            OperAntireflejante = False
        End If
    End Sub

    Private Sub CheckOthers_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
        'If CheckOthers.Checked Then
        '    ComboOtros.Enabled = True
        '    GroupAdicion.Enabled = False
        '    GroupNoAdicion.Enabled = False
        'Else
        '    ComboOtros.Enabled = False
        '    AdicionChanged()
        'End If
    End Sub

    Private Sub CheckBiselado_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
        If CheckBiselado.Checked Then
            OperBiselado = True
        Else
            OperBiselado = False
        End If
    End Sub

    Private Sub AdicionChanged()
        If CDbl(AdicionRight.Text) = 0 And CDbl(AdicionLeft.Text) = 0 Then
            PanelOblea.Enabled = False
            ComboMonofocal.Enabled = True
            ComboBifocal.Enabled = False
            ComboProgresivo.Enabled = False
            If RxType = RxTypes.NormalRx Then
                AlturaRight.Text = "0"
                AlturaLeft.Text = "0"
                MonoRight.Text = "0"
                MonoLeft.Text = "0"
                Altura.Text = "0"
                Altura.Enabled = False
            End If
        Else
            'GroupBifocal.Visible = True
            'GroupMonofocal.Visible = False
            PanelOblea.Enabled = True
            'If Not CheckOthers.Checked Then
            ComboMonofocal.Enabled = False
            ComboBifocal.Enabled = True
            ComboProgresivo.Enabled = True
            Altura.Enabled = True
            'End If
        End If
    End Sub

    Private Sub CilindroRight_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Try
            If (CilindroRight.Text) = "" Then
                CilindroRight.Text = "0.00"
            End If
            If CDbl(CilindroRight.Text) <> 0 Then
                EjeRight.Enabled = True
                EjeRight.Select()
            Else
                EjeRight.Enabled = False
            End If
        Catch ex As Exception
            CilindroRight.Text = "0.00"
        End Try
    End Sub

    Private Sub CilindroLeft_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Try
            If CilindroLeft.Text = "" Then
                CilindroLeft.Text = "0.00"
            End If
            If CDbl(CilindroLeft.Text) <> 0 Then
                EjeLeft.Enabled = True
                EjeLeft.Select()
            Else
                EjeLeft.Enabled = False
            End If
        Catch ex As Exception
            CilindroLeft.Text = "0.00"
        End Try
    End Sub

    Private Sub CilindroRight_Leave(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CilindroRight.Leave
        Try
            If CilindroRight.Text = "" Then
                CilindroRight.Text = "0.00"
            Else
                If CilindroRight.Text > 25 Or CilindroRight.Text < -25 Then
                    CilindroRight.Undo()
                End If
                CilindroRight.Text = String.Format("{0:0.00}", CDbl(CilindroRight.Text))
                If CDbl(CilindroRight.Text) > 0 Then MsgBox("El Cilindro es positivo. Verifique que est� haciendo una transposici�n.")
            End If
            CilindroRight_TextChanged(sender, e)
        Catch ex As Exception
            CilindroRight.Text = "0.00"
            CilindroRight_TextChanged(sender, e)
        End Try
    End Sub

    Private Sub CilindroLeft_Leave(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CilindroLeft.Leave
        Try
            If CilindroLeft.Text = "" Then
                CilindroLeft.Text = "0.00"
            Else
                If CilindroLeft.Text > 25 Or CilindroLeft.Text < -25 Then
                    CilindroLeft.Undo()
                End If
                CilindroLeft.Text = String.Format("{0:0.00}", CDbl(CilindroLeft.Text))
                If CDbl(CilindroLeft.Text) > 0 Then MsgBox("El Cilindro es positivo. Verifique que est� haciendo una transposici�n.")
            End If
            CilindroLeft_TextChanged(sender, e)
        Catch ex As Exception
            CilindroLeft.Text = "0.00"
            CilindroLeft_TextChanged(sender, e)
        End Try
    End Sub

    Private Sub EsferaRight_Leave(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles EsferaRight.Leave
        Try
            If EsferaRight.Text = "" Then
                EsferaRight.Text = "0.00"
            Else
                If EsferaRight.Text > 25 Or EsferaRight.Text < -25 Then
                    EsferaRight.Undo()
                End If
                EsferaRight.Text = String.Format("{0:0.00}", CDbl(EsferaRight.Text))
            End If
        Catch ex As Exception
            EsferaRight.Text = "0.00"
        End Try
    End Sub

    Private Sub EsferaLeft_Leave(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles EsferaLeft.Leave
        Try
            If EsferaLeft.Text = "" Then
                EsferaLeft.Text = "0.00"
            Else
                If EsferaLeft.Text > 25 Or EsferaLeft.Text < -25 Then
                    EsferaLeft.Undo()
                End If
                EsferaLeft.Text = String.Format("{0:0.00}", CDbl(EsferaLeft.Text))
            End If
        Catch ex As Exception
            EsferaLeft.Text = "0.00"
        End Try
    End Sub

    Private Sub EjeRight_Leave(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles EjeRight.Leave
        Try
            If EjeRight.Text = "" Then
                EjeRight.Text = "0"
            Else
                If CDbl(EjeRight.Text) > 180 Then
                    EjeRight.Text = 180
                ElseIf Val(EjeRight.Text) < 0 Then
                    EjeRight.Text = "0"
                Else
                    EjeRight.Text = String.Format("{0:0}", CDbl(EjeRight.Text))
                End If
                'EjeRight.Text = Val(EjeRight.Text)
            End If
        Catch ex As Exception
            EjeRight.Text = "0"
        End Try
    End Sub

    Private Sub EjeLeft_Leave(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles EjeLeft.Leave
        Try
            If EjeLeft.Text = "" Then
                EjeLeft.Text = "0"
            Else
                If CDbl(EjeLeft.Text) > 180 Then
                    EjeLeft.Text = 180
                ElseIf Val(EjeLeft.Text) < 0 Then
                    EjeLeft.Text = "0"
                Else
                    EjeLeft.Text = String.Format("{0:0}", CDbl(EjeLeft.Text))
                End If
                '    EjeLeft.Text = Val(EjeLeft.Text)
            End If
        Catch ex As Exception
            EjeLeft.Text = "0"
        End Try
    End Sub

    Private Sub AdicionRight_Leave(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles AdicionRight.Leave
        Try
            If AdicionRight.Text = "" Or AdicionRight.Text = "-" Or AdicionRight.Text = "+" Then
                AdicionRight.Text = "0.00"
            Else
                AdicionRight.Text = String.Format("{0:0.00}", Math.Round((CDbl(AdicionRight.Text) / 0.25), 0) * 0.25)
                AdicionChanged()
            End If
        Catch ex As Exception
            AdicionRight.Text = "0.00"
        End Try
    End Sub

    Private Sub AdicionLeft_Leave(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles AdicionLeft.Leave
        Try
            If AdicionLeft.Text = "" Or AdicionLeft.Text = "+" Or AdicionLeft.Text = "-" Then
                AdicionLeft.Text = "0.00"
            Else
                AdicionLeft.Text = String.Format("{0:0.00}", Math.Round((CDbl(AdicionLeft.Text) / 0.25), 0) * 0.25)
                AdicionChanged()
            End If
        Catch ex As Exception
            AdicionLeft.Text = "0.00"
        End Try
        If PanelOblea.Enabled Then
            AlturaRight.Focus()
        Else
            DIPLejos.Focus()
        End If
    End Sub

    Sub GetMaterialDesign(ByRef Material As Integer, ByRef Design As Integer)
        Dim x As ItemWithValue
        If ListLeftParts.SelectedIndex <> -1 Then
            x = ListLeftParts.SelectedItem
            Material = GetCustomValues(CustomValues.MaterialID, x.Value.ToString)
            Design = GetCustomValues(CustomValues.Dise�oID, x.Value.ToString)
        ElseIf ListRightParts.SelectedIndex <> -1 Then
            x = ListRightParts.SelectedItem
            Material = GetCustomValues(CustomValues.MaterialID, x.Value.ToString)
            Design = GetCustomValues(CustomValues.Dise�oID, x.Value.ToString)
        Else
            Dim temp As System.Windows.Forms.ComboBox = Nothing
            Try
                If ComboMonofocal.SelectedValue <> "0" Then
                    temp = ComboMonofocal
                ElseIf ComboBifocal.SelectedValue <> "0" Then
                    temp = ComboBifocal
                ElseIf ComboProgresivo.SelectedValue <> "0" Then
                    temp = ComboProgresivo
                ElseIf ComboOtros.SelectedValue <> "0" Then
                    temp = ComboOtros
                End If
                If temp.Items.Count > 0 Then
                    Dim a(1) As String
                    a = temp.SelectedValue.ToString.Split(",")
                    Material = a(0)
                    Design = a(1)
                Else
                    Material = 0
                    Design = 0
                End If
            Catch ex As Exception
                Material = 0
                Design = 0
            End Try
        End If
    End Sub
    Function GetLabNum() As String
        Dim Value As String = "ERROR"
        Dim t As New SqlDB(My.Settings.LocalServer, My.Settings.DBUser, My.Settings.DBPassword, My.Settings.LocalDBName)
        Try
            t.OpenConn()
            Dim ds As New DataSet
            ds = t.SQLDS("select cl_lab from TblLaboratorios where Plant = '" & My.Settings.Plant & "'", "t1")
            Value = ds.Tables("t1").Rows(0).Item(0).ToString.PadLeft(2, "0")
        Catch ex As Exception
            MsgBox(ex.Message)
        Finally
            t.CloseConn()
        End Try
        Return Value
    End Function
    Function GetNextOrderNumber(ByVal t As SqlDB, ByVal tran As SqlTransaction) As String
        Dim Value As String = "ERROR"
        'Dim t As New SqlDB(My.Settings.LocalServer, My.Settings.DBUser, My.Settings.DBPassword, My.Settings.LocalDBName)
        Try
            't.OpenConn()
            Dim ds As New DataSet
            ds = t.SQLDS("select LastSO from TblLastIndex", "t1", tran)
            Value = CInt(ds.Tables("t1").Rows(0).Item("LastSO")) + 1
            Value = GetLabNum() & Value.PadLeft(7, "0")
            If Not t.Transaction("update TblLastIndex set LastSO = LastSO + 1", tran) Then
                Throw New Exception("No se pudo actualizar el indice local de Vantage")
            End If
        Catch ex As Exception
            Throw New Exception(ex.Message)
        Finally
            't.CloseConn()
        End Try
        Return Value
        'Return "260000001"
    End Function
    Function UpdateLastIndex(ByVal LastOrder As String) As Boolean
        Dim Updated As Boolean = False
        Dim Value As Integer = Convert.ToInt32(LastOrder.Substring(2))
        Dim t As New SqlDB(My.Settings.LocalServer, My.Settings.DBUser, My.Settings.DBPassword, My.Settings.LocalDBName)
        Try
            t.OpenConn()
            Dim ds As New DataSet
            ds = t.SQLDS("update TblLastIndex set LastSO = " & Value, "t1")
            Updated = True
        Catch ex As Exception
            Updated = False
            MsgBox(ex.Message)
        Finally
            t.CloseConn()
        End Try
        Return Updated
    End Function

    Function GetRevision(ByVal PartNum As String) As String
        'Dim conn As New SqlDB("Read_Registry("MainServer")", My.Settings.DBUser, My.Settings.DBPassword, My.Settings.MfgSysDBName)
        Try
            'conn.OpenConn()
            Dim ds As New DataSet
            If PartNum.EndsWith("T") Then
                If (CheckAR.Checked Or CheckARGold.Checked Or CheckARMatiz.Checked) And CheckBiselado.Checked Then
                    Return "D"
                ElseIf (CheckAR.Checked Or CheckARGold.Checked Or CheckARMatiz.Checked) Then
                    Return "A"
                ElseIf CheckBiselado.Checked Then
                    Return "B"
                End If
                Return "H"          ' Llega aqui si no hay Bis ni AR
            ElseIf PartNum.EndsWith("S") Then
                If (CheckAR.Checked Or CheckARGold.Checked Or CheckARMatiz.Checked) And CheckBiselado.Checked Then
                    Return "G"
                ElseIf (CheckAR.Checked Or CheckARGold.Checked Or CheckARMatiz.Checked) Then
                    Return "F"
                ElseIf CheckBiselado.Checked Then
                    Return "E"
                End If
                Return "C"          ' Llega aqui si no hay Bis ni AR
            Else
                Return "Error"
            End If
        Catch ex As Exception
        Finally
            'conn.CloseConn()
        End Try
        Return "Error"
    End Function
    Function GetJobNumber() As String
        Dim JNStr As String = ""
        Dim temp As String = Now.Date.Year
        JNStr = temp.Substring(2)
        temp = Now.Date.Month
        If temp < 10 Then
            JNStr &= "0"
        End If
        JNStr &= temp
        temp = Date.Now.Day
        If temp < 10 Then
            JNStr &= "0"
        End If
        JNStr &= temp
        JNStr &= GetLabNum() & RxWECO.Text
        Return JNStr
    End Function
    Function GetGenericPartNum(ByVal PartNum As String)
        Dim PNStr As String = ""
        Dim conn As New SqlDB(My.Settings.LocalServer, My.Settings.DBUser, My.Settings.DBPassword, My.Settings.LocalDBName)
        Try
            conn.OpenConn()
            Dim ds As New DataSet

            ds = conn.SQLDS("select clase,material,dise�o from VwPartTRECEUX where partnum = '" & PartNum & "'", "t1")
            Dim Clase As String = ds.Tables("t1").Rows(0).Item("Clase")
            Dim Material As String = ds.Tables("t1").Rows(0).Item("Material")
            Dim Dise�o As String = ds.Tables("t1").Rows(0).Item("Dise�o")
            PNStr = Material & Dise�o & Clase


            'If Clase = "T" Then
            '    PNStr &= "T"
            'ElseIf Clase = "ST" Then
            '    PNStr &= "S"
            'Else
            '    Return "Parte Inexistente"
            'End If

        Catch ex As Exception
            MsgBox(ex.Message)
        Finally
            conn.CloseConn()
        End Try
        Return PNStr
    End Function
    Function CreateJobEntry(ByVal PNStr As String, ByVal QtyStr As Integer, ByVal RevStr As String, ByVal JNStr As String) As Boolean
        Dim Value As Boolean = False
        'JNStr = "060327-446900"

        Dim Conn As New SqlDB(ChooseServer, My.Settings.DBUser, My.Settings.DBPassword, DataBase)
        Try
            Conn.OpenConn()
            Dim ds As New DataSet
            ds = Conn.SQLDS("select prodcode from part where partnum = '" & PNStr & "'", "t1")
            Dim ProdCode As String = ds.Tables("t1").Rows(0).Item("prodcode")

            Dim Comm As New SqlCommand("SP_JobEntry", Conn.SQLConn)
            Comm.CommandTimeout = My.Settings.DBCommandTimeout
            Comm.CommandType = Data.CommandType.StoredProcedure
            Comm.Parameters.AddWithValue("Company", "TRECEUX")
            Comm.Parameters.AddWithValue("PNStr", PNStr)
            Comm.Parameters.AddWithValue("JNStr", JNStr)
            Comm.Parameters.AddWithValue("QtyStr", QtyStr)
            Comm.Parameters.AddWithValue("RevStr", RevStr)
            Comm.Parameters.AddWithValue("StartDate", Now.Date) 'ShipTo.SelectedValue)
            Comm.Parameters.AddWithValue("ProdCode", ProdCode)
            Comm.Parameters.AddWithValue("Plant", Read_Registry("Plant"))
            Comm.Parameters.AddWithValue("OrderNum", RxNum.Text)
            Comm.Parameters.Add("@Msg", Data.SqlDbType.VarChar, 30)
            Comm.Parameters("@Msg").Direction = Data.ParameterDirection.Output
            Comm.ExecuteNonQuery()
            Dim Mensaje As String = Comm.Parameters("@Msg").Value
            If (Mensaje <> "OK") Then
                MsgBox(Mensaje, MsgBoxStyle.Critical, "Error")
                Value = False
            Else
                'MsgBox("Orden de Produccion Generada: " & JNStr, MsgBoxStyle.Information)
                'ds = Conn.SQLDS("update orderhed set userchar1 = '" & JNStr & "'", "t1")
                Value = True
            End If

        Catch ex As Exception
            MsgBox(ex.Message)
        Finally
            Conn.CloseConn()
        End Try
        Return Value
    End Function

    Private Sub ListRightParts_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Dim MyValue As ItemWithValue = ListRightParts.Items(ListRightParts.SelectedIndex)
        MsgBox(MyValue.Value)
    End Sub
    Private Sub ListLeftParts_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Dim MyValue As ItemWithValue = ListLeftParts.Items(ListLeftParts.SelectedIndex)
        MsgBox(MyValue.Value)
    End Sub

    Private Sub BuscarRecetaExistente(ByVal RxID As String)
        Dim conn As New SqlDB(My.Settings.LocalServer, My.Settings.DBUser, My.Settings.DBPassword, My.Settings.LocalDBName)
        Try
            Dim SqlStr As String = ""

            ' Importante para poder encontrar los trazos cuando la receta tiene menos de 4 d�gitos o lleva 0 al inicio
            ' PFL 2013-10-14
            RxID = String.Format("{0:000000}", Int(RxID))

            ''QUERY PARA BUSCAR EL JOB DE RX VERBIEN


            Select Case My.Settings.OpticalProtocol
                Case "DVI"
                    SqlStr = "SELECT jobnum, fecha, dbl, sizing, fuente FROM "
                Case "OMA"
                    SqlStr = "SELECT jobnum, fecha, dbl, sizing, fuente,lado " & _
                             ",FBOCIN,FBSGUP,SWIDTH,LTYP,LMATID,IPD,BCOCIN,BCOCUP," & _
                             "DIA,CRIB,FCSGIN,FCSGUP,SDEPTH,NPD,FCOCUP,FCOCIN,DRILL FROM "
            End Select



            conn.OpenConn()
            Dim ds As New DataSet
            ' PFL Ago 1  2013 # Se corrige 'bug' que hac�a fallar el query cuando en la tabla OmaTraces existes campos jobnum que no se pueden convertir a int
            ' el campo jobnum es nvarchar, y las consultas deben proporcionar un valor de string Ej. jobnum='[trazp]' para evitar que falle el query.
            ' ds = conn.SQLDS(SqlStr & VwConsultaTrazos & " with(nolock) where status = 0 and jobnum = '" & RxID & "'", "t1")
            ' Bug : El trazo no se encuentra : Como la Rx ya existe lo m�s probable es que el trazo ya se marc� como usado (status=1), por lo tanto no hacer caso de status
            ds = conn.SQLDS(SqlStr & VwConsultaTrazos & " with(nolock) where jobnum = '" & RxID & "'", "t1")

            If (My.Settings.Plant = "VBENS") Then
                ds = conn.SQLDS("SELECT     VwOMATraces.Jobnum, VwOMATraces.Fecha, VwOMATraces.dbl, VwOMATraces.sizing, VwOMATraces.Fuente, VwOMATraces.lado, VwOMATraces.FBOCIN, " & _
                      "VwOMATraces.FBSGUP, VwOMATraces.SWIDTH, VwOMATraces.LTYP, VwOMATraces.LMATID, VwOMATraces.IPD, VwOMATraces.BCOCIN, VwOMATraces.BCOCUP, " & _
                      "VwOMATraces.DIA, VwOMATraces.CRIB, VwOMATraces.FCSGIN, VwOMATraces.FCSGUP, VwOMATraces.SDEPTH, VwOMATraces.NPD, VwOMATraces.FCOCUP, " & _
                      "VwOMATraces.FCOCIN, VwOMATraces.DRILL " & _
                      "FROM VwOMATraces WITH (nolock) INNER JOIN " & _
                      "TblWebOrders ON VwOMATraces.Jobnum = TblWebOrders.framenum " & _
                      "WHERE     TblWebOrders.ponumber = " & RxID & "", "t1")
            End If



            ComboTrazos.ComboBox1.DataSource = ds.Tables(0)
            ComboTrazos.ComboBox1.DisplayMember = "jobnum"
            ComboTrazos.ComboBox1.ValueMember = "jobnum"
            ComboTrazos.Bind()
            If ComboTrazos.ComboBox1.Items.Count > 0 Then
                ComboTrazos.ComboBox1.SelectedIndex = 0
                NoTrace = False

                If My.Settings.OpticalProtocol = "OMA" Then
                    OMA_EyeSide = ds.Tables("t1").Rows(0).Item("lado")
                    LeerValorOMA(ds.Tables("t1").Rows(0).Item("FBOCIN"), "FBOCIN")
                    LeerValorOMA(ds.Tables("t1").Rows(0).Item("FBSGUP"), "FBSGUP")
                    LeerValorOMA(ds.Tables("t1").Rows(0).Item("SWIDTH"), "SWIDTH")
                    LeerValorOMA(ds.Tables("t1").Rows(0).Item("LTYP"), "LTYP")
                    LeerValorOMA(ds.Tables("t1").Rows(0).Item("LMATID"), "LMATID")
                    LeerValorOMA(ds.Tables("t1").Rows(0).Item("IPD"), "IPD")
                    LeerValorOMA(ds.Tables("t1").Rows(0).Item("BCOCIN"), "BCOCIN")
                    LeerValorOMA(ds.Tables("t1").Rows(0).Item("BCOCUP"), "BCOCUP")
                    LeerValorOMA(ds.Tables("t1").Rows(0).Item("DIA"), "DIA")
                    LeerValorOMA(ds.Tables("t1").Rows(0).Item("CRIB"), "CRIB")
                    LeerValorOMA(ds.Tables("t1").Rows(0).Item("FCSGIN"), "FCSGIN")
                    LeerValorOMA(ds.Tables("t1").Rows(0).Item("FCSGUP"), "FCSGUP")
                    LeerValorOMA(ds.Tables("t1").Rows(0).Item("SDEPTH"), "SDEPTH")
                    LeerValorOMA(ds.Tables("t1").Rows(0).Item("NPD"), "NPD")
                    LeerValorOMA(ds.Tables("t1").Rows(0).Item("FCOCUP"), "FCOCUP")
                    LeerValorOMA(ds.Tables("t1").Rows(0).Item("FCOCIN"), "FCOCIN")
                    LeerDrillOMA(ds.Tables("t1").Rows(0).Item("DRILL"))
                Else
                    OMA_EyeSide = "N"
                End If

                PreviewTrazo.Image = Nothing
                Trazos_Click(Me, New EventArgs)
                ' Cuando es por Vantage no necesita esta linea porque se trae esta informacion desde antes
                'ContornoPuente.Text = CDbl(ds.Tables("t1").Rows(0).Item("dbl")) / 100
                Sizing = CDbl(ds.Tables("t1").Rows(0).Item("sizing"))
                If ds.Tables("t1").Rows(0).Item("fuente") = 0 Then
                    IsManualRx = True
                Else
                    IsManualRx = False
                End If
            Else
                MsgBox("No existe un trazo relacionado con esta Rx.", MsgBoxStyle.Exclamation)
                NoTrace = True
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        Finally
            conn.CloseConn()
        End Try

    End Sub

    Private Sub LeerValorOMA(ByVal Dato As String, ByVal Var As String)
        Dim ValR, ValL As String
        ValR = "0"
        ValL = "0"
        Try
            If Dato <> "" Then
                ValR = Dato.Substring(InStr(Dato, "=", CompareMethod.Text), InStr(Dato, ";", CompareMethod.Text) - InStr(Dato, "=", CompareMethod.Text) - 1)
                ValL = Dato.Substring(InStr(Dato, ";", CompareMethod.Text), Len(Dato) - InStr(Dato, ";", CompareMethod.Text))
                If ValR.Length = 0 Then ValR = 0
                If ValL.Length = 0 Then ValL = 0
            End If
        Catch ex As Exception

        Finally
            Select Case Var
                Case "FBOCIN"
                    OMA_DHR = ValR
                    OMA_DHL = ValL
                Case "FBSGUP"
                    OMA_DVR = ValR
                    OMA_DVL = ValL
                Case "SWIDTH"
                    OMA_SWIDHTR = ValR
                    OMA_SWIDHTL = ValL
                Case "LTYP"
                    If ValR = "0" Then OMA_LTYPR = "?" Else OMA_LTYPR = ValR
                    If ValL = "0" Then OMA_LTYPL = "?" Else OMA_LTYPL = ValL
                Case "LMATID"
                    OMA_LMATIDR = ValR
                    OMA_LMATIDL = ValL
                Case "IPD"
                    OMA_IPDR = ValR
                    OMA_IPDL = ValL
                Case "BCOCIN"
                    OMA_BCOCINR = ValR
                    OMA_BCOCINL = ValL
                Case "BCOCUP"
                    OMA_BCOCUPR = ValR
                    OMA_BCOCUPL = ValL
                Case "DIA"
                    OMA_DIAR = ValR
                    OMA_DIAL = ValL
                Case "CRIB"
                    OMA_CRIBR = ValR
                    OMA_CRIBL = ValL
                Case "FCSGIN"
                    OMA_FCSGINR = ValR
                    OMA_FCSGINL = ValL
                Case "FCSGUP"
                    OMA_FCSGUPR = ValR
                    OMA_FCSGUPL = ValL
                Case "SDEPTH"
                    OMA_SDEPTHR = ValR
                    OMA_SDEPTHL = ValL
                Case "NPD"
                    OMA_NPDR = ValR
                    OMA_NPDL = ValL
                Case "FCOCUP"
                    OMA_FCOCUPR = ValR
                    OMA_FCOCUPL = ValL
                Case "FCOCIN"
                    OMA_FCOCINR = ValR
                    OMA_FCOCINL = ValL
            End Select
        End Try
    End Sub
    Private Sub LeerDrillOMA(ByVal Dato As String)
        Dim Val2, Val3, Val4, Val5, Val6, Val7, Val8, Val9, Val10 As String
        Val9 = "90"
        Val10 = "90"
        Val8 = "F"
        Val7 = "?"
        Val4 = "?"
        Val2 = "0"
        Val5 = "0"
        Val3 = "0"
        Val6 = "0"
        Dim LastPuntoComa As Integer = 8 'EMPEZAMOS CON LA segunda coma ya que el primer campo es el lado
        Dim NextPuntoComa As Integer = 0
        Try
            If Dato <> "" Then
                NextPuntoComa = Dato.IndexOf(";", LastPuntoComa)
                Val2 = Dato.Substring(LastPuntoComa, NextPuntoComa - LastPuntoComa)

                LastPuntoComa = NextPuntoComa + 1
                NextPuntoComa = Dato.IndexOf(";", LastPuntoComa)
                Val3 = Dato.Substring(LastPuntoComa, NextPuntoComa - LastPuntoComa)

                LastPuntoComa = NextPuntoComa + 1
                NextPuntoComa = Dato.IndexOf(";", LastPuntoComa)
                Val4 = Dato.Substring(LastPuntoComa, NextPuntoComa - LastPuntoComa)

                LastPuntoComa = NextPuntoComa + 1
                NextPuntoComa = Dato.IndexOf(";", LastPuntoComa)
                Val5 = Dato.Substring(LastPuntoComa, NextPuntoComa - LastPuntoComa)

                LastPuntoComa = NextPuntoComa + 1
                NextPuntoComa = Dato.IndexOf(";", LastPuntoComa)
                Val6 = Dato.Substring(LastPuntoComa, NextPuntoComa - LastPuntoComa)

                LastPuntoComa = NextPuntoComa + 1
                NextPuntoComa = Dato.IndexOf(";", LastPuntoComa)
                Val7 = Dato.Substring(LastPuntoComa, NextPuntoComa - LastPuntoComa)

                LastPuntoComa = NextPuntoComa + 1
                NextPuntoComa = Dato.IndexOf(";", LastPuntoComa)
                Val8 = Dato.Substring(LastPuntoComa, NextPuntoComa - LastPuntoComa)

                LastPuntoComa = NextPuntoComa + 1
                NextPuntoComa = Dato.IndexOf(";", LastPuntoComa)
                Val9 = Dato.Substring(LastPuntoComa, NextPuntoComa - LastPuntoComa)

                LastPuntoComa = NextPuntoComa + 1
                Val10 = Dato.Substring(LastPuntoComa, Len(Dato) - LastPuntoComa)
            End If
        Catch ex As Exception

        Finally
            OMA_PER_X1 = Val2
            OMA_PER_Y1 = Val3
            OMA_PER_DIA = Val4
            OMA_PER_X2 = Val5
            OMA_PER_Y2 = Val6
            OMA_PER_DEPTH = Val7
            OMA_PER_BFA = Val8
            OMA_PER_ANGLAT = Val9
            OMA_PER_ANGVER = Val10
        End Try
    End Sub
    Private Sub BuscarTrazos_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        PanelInformacion.Visible = False
        Dim JobNum As String = RxID.Text

        ' Obtengo los ultimos 4 digitos de la orden de laboratorio
        '---------------------------------------------------------
        JobNum = JobNum.PadLeft(4, "0")
        JobNum = JobNum.Substring(JobNum.Length - 4, 4)
        '---------------------------------------------------------

        If FrameNum <> "0" And My.Settings.GetCOSTCOTraces Then
            ' Buscar el trazo en la tabla de trazos del lab. local
            Dim Mensaje As String
            Dim Conn2 As New SqlDB(My.Settings.LocalServer, My.Settings.DBUser, My.Settings.DBPassword, My.Settings.LocalDBName)
            Conn2.OpenConn()
            Try
                Dim Comm As New SqlCommand("SP_TrazosManuales" & My.Settings.OpticalProtocol, Conn2.SQLConn)
                Comm.CommandType = Data.CommandType.StoredProcedure
                Comm.Parameters.AddWithValue("@JobNum", JobNum)
                Comm.Parameters.Add("@JobUnico", Data.SqlDbType.VarChar, 100)
                Comm.Parameters("@JobUnico").Direction = Data.ParameterDirection.Output
                Comm.ExecuteNonQuery()
                Mensaje = Comm.Parameters("@JobUnico").Value
                ComboTrazos.ComboBox1.Items.Clear()
                ComboTrazos.ComboBox1.Items.Add(Mensaje)
                ComboTrazos.Bind()
                ComboTrazos.ComboBox1.Invalidate()
                'RxWECO.Text = Mensaje
                'RxWECO.Visible = True
            Catch ex As Exception
                MsgBox(ex.Message)
            Finally
                Conn2.CloseConn()
            End Try
            'MsgBox("El frame que trae es: " & FrameNum, MsgBoxStyle.Information)
        Else
            Dim Conn2 As New SqlDB(My.Settings.LocalServer, My.Settings.DBUser, My.Settings.DBPassword, My.Settings.LocalDBName)
            Conn2.OpenConn()
            Try
                Dim ds As New DataSet
                Dim FieldsStr As String = ""
                Select Case My.Settings.OpticalProtocol
                    Case "DVI"
                        FieldsStr = "JOBNUM,FECHA,DBL,SIZING"
                    Case "OMA"
                        FieldsStr = "JOBNUM,FECHA,DBL,SIZING,ARMAZON,LADO"
                End Select
                ds = Conn2.SQLDS("select " & FieldsStr & " from " & VwConsultaTrazos & " with(nolock) where fuente = 1 and status = 1 and jobnum like '" & JobNum & "%' order by Fecha desc", "t1") ' and DATEPART(dayofyear,fecha) = datepart(dayofyear,getdate()) and datepart(year,fecha) = datepart(year,getdate())", "t1")

                If (My.Settings.Plant = "VBENS") Then
                    Dim stringTmp As String = "SELECT     VwOMATraces.Jobnum, VwOMATraces.Fecha, VwOMATraces.dbl, VwOMATraces.sizing, VwOMATraces.Fuente, VwOMATraces.ARMAZON, VwOMATraces.lado, VwOMATraces.FBOCIN, " & _
                          "VwOMATraces.FBSGUP, VwOMATraces.SWIDTH, VwOMATraces.LTYP, VwOMATraces.LMATID, VwOMATraces.IPD, VwOMATraces.BCOCIN, VwOMATraces.BCOCUP," & _
                          "VwOMATraces.DIA, VwOMATraces.CRIB, VwOMATraces.FCSGIN, VwOMATraces.FCSGUP, VwOMATraces.SDEPTH, VwOMATraces.NPD, VwOMATraces.FCOCUP, " & _
                          "VwOMATraces.FCOCIN, VwOMATraces.DRILL " & _
                          "FROM VwOMATraces WITH (nolock) INNER JOIN " & _
                          "TblWebOrders ON VwOMATraces.Jobnum = TblWebOrders.framenum WHERE     TblWebOrders.ponumber =" & RxID.Text & ""
                    ds = Conn2.SQLDS(stringTmp, "t1")
                    'ds = Conn2.SQLDS("SELECT     VwOMATraces.Jobnum, VwOMATraces.Fecha, VwOMATraces.dbl, VwOMATraces.sizing, VwOMATraces.Fuente, VwOMATraces.ARMAZON, VwOMATraces.lado, VwOMATraces.FBOCIN, " & _
                    '      "VwOMATraces.FBSGUP, VwOMATraces.SWIDTH, VwOMATraces.LTYP, VwOMATraces.LMATID, VwOMATraces.IPD, VwOMATraces.BCOCIN, VwOMATraces.BCOCUP," & _
                    '      "VwOMATraces.DIA, VwOMATraces.CRIB, VwOMATraces.FCSGIN, VwOMATraces.FCSGUP, VwOMATraces.SDEPTH, VwOMATraces.NPD, VwOMATraces.FCOCUP, " & _
                    '      "VwOMATraces.FCOCIN, VwOMATraces.DRILL " & _
                    '      "FROM VwOMATraces WITH (nolock) INNER JOIN " & _
                    '      "TblWebOrders ON VwOMATraces.Jobnum = TblWebOrders.framenum WHERE     TblWebOrders.ponumber =" & RxID.Text & "", "t1")
                End If
                If (My.Settings.Plant = "augenlab") Then
                    Dim stringTmp As String = "SELECT     VwOMATraces.Jobnum, VwOMATraces.Fecha, VwOMATraces.dbl, VwOMATraces.sizing, VwOMATraces.Fuente, VwOMATraces.ARMAZON, VwOMATraces.lado, VwOMATraces.FBOCIN, " & _
                          "VwOMATraces.FBSGUP, VwOMATraces.SWIDTH, VwOMATraces.LTYP, VwOMATraces.LMATID, VwOMATraces.IPD, VwOMATraces.BCOCIN, VwOMATraces.BCOCUP," & _
                          "VwOMATraces.DIA, VwOMATraces.CRIB, VwOMATraces.FCSGIN, VwOMATraces.FCSGUP, VwOMATraces.SDEPTH, VwOMATraces.NPD, VwOMATraces.FCOCUP, " & _
                          "VwOMATraces.FCOCIN, VwOMATraces.DRILL " & _
                          "FROM VwOMATraces WITH (nolock) INNER JOIN " & _
                          "TblWebOrders ON VwOMATraces.Jobnum = TblWebOrders.framenum WHERE     TblWebOrders.ponumber =" & RxID.Text & ""
                    ds = Conn2.SQLDS(stringTmp, "t1")
                    'ds = Conn2.SQLDS("SELECT     VwOMATraces.Jobnum, VwOMATraces.Fecha, VwOMATraces.dbl, VwOMATraces.sizing, VwOMATraces.Fuente, VwOMATraces.ARMAZON, VwOMATraces.lado, VwOMATraces.FBOCIN, " & _
                    '      "VwOMATraces.FBSGUP, VwOMATraces.SWIDTH, VwOMATraces.LTYP, VwOMATraces.LMATID, VwOMATraces.IPD, VwOMATraces.BCOCIN, VwOMATraces.BCOCUP," & _
                    '      "VwOMATraces.DIA, VwOMATraces.CRIB, VwOMATraces.FCSGIN, VwOMATraces.FCSGUP, VwOMATraces.SDEPTH, VwOMATraces.NPD, VwOMATraces.FCOCUP, " & _
                    '      "VwOMATraces.FCOCIN, VwOMATraces.DRILL " & _
                    '      "FROM VwOMATraces WITH (nolock) INNER JOIN " & _
                    '      "TblWebOrders ON VwOMATraces.Jobnum = TblWebOrders.framenum WHERE     TblWebOrders.ponumber =" & RxID.Text & "", "t1")
                End If
                'Trazos.DataSource = ds.Tables(0)
                'Trazos.DisplayMember = "jobnum
                'Trazos.ValueMember = "jobnum"

                ComboTrazos.ComboBox1.DataSource = Nothing
                ComboTrazos.Bind()
                ComboTrazos.ComboBox1.DataSource = ds.Tables(0)
                ComboTrazos.ComboBox1.DisplayMember = "jobnum"
                ComboTrazos.ComboBox1.ValueMember = "jobnum"
                ComboTrazos.Bind()
                If ComboTrazos.ComboBox1.Items.Count > 0 Then
                    Select Case My.Settings.OpticalProtocol
                        Case "DVI"
                            ContornoPuente.Text = CDbl(ds.Tables("t1").Rows(0).Item("dbl")) / 100
                        Case "OMA"
                            ContornoPuente.Text = CDbl(ds.Tables("t1").Rows(0).Item("dbl"))
                    End Select
                    'ContornoPuente.Text = CDbl(ds.Tables("t1").Rows(0).Item("dbl")) / 100
                    Sizing = CDbl(ds.Tables("t1").Rows(0).Item("sizing"))
                    If My.Settings.OpticalProtocol = "OMA" Then OMA_EyeSide = ds.Tables("t1").Rows(0).Item("lado")
                    'DESPLEGAMOS EL TIPO DE ARMAZON SELECCIONADO EN LA TRAZADORA DE OMA
                    'guardamos el valor del armazon en la variable FrameType
                    Dim FrameType As Integer = 0
                    If My.Settings.OpticalProtocol = "OMA" Then
                        Select Case ds.Tables("t1").Rows(0).Item("armazon")
                            Case 1
                                FrameType = 1 'PLASTICO
                            Case 2
                                FrameType = 0 'METAL
                            Case 3
                                FrameType = 2 'RANURADO O PERFORADO
                        End Select
                    End If

                    'DESPLEGAMOS EL ARMAZON TYPE EN EL COMBOBOX
                    ComboArmazon2.Enabled = True
                    ComboArmazon2.ComboBox1.SelectedIndex = FrameType
                End If

            Catch ex As Exception
                MsgBox(ex.Message)

            Finally
                Conn2.CloseConn()
            End Try
            'MsgBox("El frame que trae es: " & FrameNum, MsgBoxStyle.Information)
        End If


        ' Si es una orden de internet y no lleva paquete Augen Air, debe tener trazo manual por default.
        If (Me.RxType = RxTypes.WebRx And MyFrame.NumArmazon = 0 And Not Me.ChangingTraces And (RxNum.Text = "" Or RxNum.Text = "0")) Then ComboTrazos.ComboBox1.DataSource = Nothing

        Dim conn As New SqlDB(My.Settings.LocalServer, My.Settings.DBUser, My.Settings.DBPassword, My.Settings.LocalDBName)
        'Dim conn As New SqlDB(ChooseServer, "sa", "Proliant01", "ERPMaster")
        conn.OpenConn()
        Try
            If ComboTrazos.ComboBox1.Items.Count > 0 Then
                'AceptarDigital.Enabled = True
                'Trazos.Enabled = True
                PreviewTrazo.Image = Nothing
                ComboTrazos.ComboBox1.SelectedIndex = 0
                'ComboTrazos.Texto = ComboTrazos.ComboBox1.Text
                Trazos_Click(Me, e)
            Else
                PreviewTrazo.Image = Nothing
                'AceptarDigital.Enabled = False
                ComboTrazos.ComboBox1.DataSource = Nothing
                ComboTrazos.Bind()
                ComboTrazos.ComboBox1.Items.Clear()
                ComboTrazos.Texto = ""
                'Trazos.Enabled = False
                'MsgBox("No se encontro Referencia Digital para esta receta", MsgBoxStyle.Exclamation)
                'RxID.Text = 0
                'RxID.Select()
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        Finally
            conn.CloseConn()
        End Try
    End Sub

    Function GetGraphicsObject(ByVal PBox As PictureBox) As Graphics
        Dim bmp As New Bitmap(PBox.Width, PBox.Height)
        PBox.Image = bmp
        Dim G As Graphics
        G = Graphics.FromImage(bmp)
        Return G
    End Function

    Private Sub Trazos_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ComboTrazos.SelectedIndexChanged
        Dim Radios(NbrsTraces()) As Integer
        Dim ScaleRatio, h, b As Double

        '**********VARIABLES QUE DETERMINAN EL VALOR DE LOS EJES EN CUESTION A CANTIDAD DE RADIOS, EJ 180 GRADOS = 256 RADIOS, 90 GRADOS = 128 RADIOS, PARA EL CASO DE 512 TRAZOS
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


        If ComboTrazos.ComboBox1.Text.Length > 0 Then
            If RadiosL(0) = 0 Or Me.ChangingTraces Or RxNum.Text = "0" Or RxNum.Text = "" Then
                GetRadiosFromDatabase(ComboTrazos.ComboBox1.Text)
            End If
            Radios = RadiosL
            If Radios(0) <> 0 Then
                h = (PreviewTrazo.Height / (Radios(X1) + Radios(X1 + X2)))
                b = (PreviewTrazo.Width / (Radios(0) + Radios(X2)))
                If b < h Then ScaleRatio = b - 0.002 Else ScaleRatio = h - 0.002
                Dim Preview As New Lente(Lente.Sides.Right, Radios, ScaleRatio)
                Dim Trazo As New GraphicsPath
                Trazo = Preview.DrawLense(PreviewTrazo, New Point(PreviewTrazo.Width / 2 / ScaleRatio, -PreviewTrazo.Height / 2 / ScaleRatio))
                Preview.CalculaContorno()
                ContornoA.Text = Preview.Size.Width / 100
                ContornoB.Text = Preview.Size.Height / 100
                ContornoED.Text = Preview.Diagonal
                Dim G As Graphics = GetGraphicsObject(PreviewTrazo)
                G.SmoothingMode = SmoothingMode.AntiAlias
                G.Clear(PreviewTrazo.BackColor)
                G.DrawPath(Pens.DarkBlue, Trazo)
                PreviewTrazo.Invalidate()

                'ScaleRatio = (PreviewLente.Height / (Radios(0) + Radios(256)))
                h = (PreviewLente.Height / (Radios(X1) + Radios(X3)))
                b = (PreviewLente.Width / (Radios(0) + Radios(X2)))
                If b < h Then ScaleRatio = b - 0.002 Else ScaleRatio = h - 0.002
                'Preview = New Lente(Lente.Sides.Right, Radios,  0.025)
                Preview = New Lente(Lente.Sides.Right, Radios, ScaleRatio)
                'Trazo = Preview.DrawLense(PreviewLente, New Point(PreviewLente.Width / 2 / 0.025, -PreviewLente.Height / 2 / 0.025))
                Trazo = Preview.DrawLense(PreviewLente, New Point(PreviewLente.Width / 2 / ScaleRatio, -PreviewLente.Height / 2 / ScaleRatio))
                Preview.CalculaContorno()
                G = GetGraphicsObject(PreviewLente)
                G.SmoothingMode = SmoothingMode.AntiAlias
                G.Clear(PreviewLente.BackColor)
                G.DrawPath(Pens.DarkBlue, Trazo)
                PreviewLente.Invalidate()
            End If

        End If
    End Sub
    Public Sub FillRxResume(ByVal Info As RxResume)
        Info.RxWECO.Text = Me.StuffRx(RxWECO.Text)
        Info.TextCliente.Text = ComboClients.Text
        If ComboClients.Text = "" Then
            Info.TextCliente.Text = Me.TextLabClient.Text
        End If
        Info.RxNum.Text = RxNum.Text
        Info.RxID.Text = RxID.Text
        Info.EsferaL.Text = Val(EsferaLeft.Text)
        Info.EsferaR.Text = Val(EsferaRight.Text)
        Info.CilindroR.Text = Val(CilindroRight.Text)
        Info.CilindroL.Text = Val(CilindroLeft.Text)
        Info.EjeR.Text = Val(EjeRight.Text)
        Info.EjeL.Text = Val(EjeLeft.Text)
        Info.AdicionR.Text = Val(AdicionRight.Text)
        Info.AdicionL.Text = Val(AdicionLeft.Text)
        Info.PrismaL.Text = PrismaL
        Info.PrismaR.Text = PrismaR
        Info.EjePrismaL.Text = EjePrismaL
        Info.EjePrismaR.Text = EjePrismaR
        If CheckLevel1.Checked Then
            Info.Tono.Text = 1
        ElseIf CheckLevel2.Checked Then
            Info.Tono.Text = 2
        ElseIf CheckLevel3.Checked Then
            Info.Tono.Text = 3
        End If
        Info.RadioGradiente.Checked = RadioGradiente.Checked
        'If combotinte.Text <> "" Then
        Info.Color.Text = ComboTinte.Texto
        'Else
        'Info.Color.Text = "Ninguno "
        'End If
        If CheckAR.Checked Then
            Info.AntireflejanteL.Text = "AR"
            Info.AntireflejanteR.Text = "AR"
            '           Info.TextARLab.Text = ARLabName
        ElseIf CheckARGold.Checked Then
            Info.AntireflejanteL.Text = "AR GOLD"
            Info.AntireflejanteR.Text = "AR GOLD"
            '            Info.TextARLab.Text = ARLabName
        ElseIf CheckARMatiz.Checked Then
            Info.AntireflejanteL.Text = "AR MATIZ"
            Info.AntireflejanteR.Text = "AR MATIZ"
        Else
            Info.AntireflejanteL.Text = "Ninguno "
            Info.AntireflejanteR.Text = "Ninguno "
        End If
        Info.TextARLab.Text = ARLabName
        'Me.GetMaterialDesign(MaterialID, DesignID)


        If (SelectedRightEye.Items.Count > 0) Or (ListRightParts.SelectedItems.Count > 0) Then
            Dim SelItem As ItemWithValue = ListRightParts.SelectedItem
            Info.Dise�oR.Text = GetCustomValues(CustomValues.Dise�o, SelItem.Value) 'DesignID.ToString)
            Info.MaterialR.Text = GetCustomValues(CustomValues.Material, SelItem.Value) 'MaterialID.ToString)
            Info.TipoR.Text = "ERROR"
            Select Case GetCustomValues(CustomValues.Clase, SelItem.Value)
                Case "S" : Info.TipoR.Text = "Semiterminado"
                Case "T" : Info.TipoR.Text = "Terminado"
            End Select
        Else
            Info.MaterialR.Text = "No Capturado"
            Info.TipoR.Text = "No Capturado"
            Info.Dise�oR.Text = "No Capturado"
        End If
        If (SelectedLeftEye.Items.Count > 0) Or (ListLeftParts.SelectedItems.Count > 0) Then
            Dim SelItem As ItemWithValue = ListLeftParts.SelectedItem
            Info.MaterialL.Text = GetCustomValues(CustomValues.Material, SelItem.Value) 'MaterialID.ToString)
            Info.Dise�oL.Text = GetCustomValues(CustomValues.Dise�o, SelItem.Value) 'DesignID.ToString)
            Info.TipoL.Text = "ERROR"
            Select Case GetCustomValues(CustomValues.Clase, SelItem.Value)
                Case "S" : Info.TipoL.Text = "Semiterminado"
                Case "T" : Info.TipoL.Text = "Terminado"
            End Select
        Else
            Info.MaterialL.Text = "No Capturado"
            Info.TipoL.Text = "No Capturado"
            Info.Dise�oL.Text = "No Capturado"
        End If
    End Sub


    Public Function getTrazoVB(ByVal rxVB As Integer) As String
        Dim t As New SqlDB(My.Settings.LocalServer, My.Settings.DBUser, My.Settings.DBPassword, My.Settings.LocalDBName)
        Dim nds As New DataSet
        Dim trazo As Integer
        Try
            t.OpenConn()
            nds = t.SQLDS("SELECT VwOMATraces.Jobnum " & _
                      "FROM VwOMATraces WITH (nolock) INNER JOIN " & _
                      "TblWebOrders ON VwOMATraces.Jobnum = TblWebOrders.framenum " & _
                      "WHERE     TblWebOrders.ponumber = " & rxVB & "", "t1")
            If nds.Tables("t1").Rows.Count > 0 Then
                trazo = nds.Tables("t1").Rows(0).Item(0)
                Return trazo
            Else
                Throw New Exception("Error no encontro trazo asociado a la Rx")
            End If
        Catch ex As Exception
            Throw New Exception("Error de conexion local.")
            'Finallyt()
            t.CloseConn()
        End Try
    End Function


    Public Sub FillRx(ByVal ds As DataSet, ByVal guarantee As Boolean)
        Dim custIndex As Integer

        Try
            Rx = New Receta
            'RxWECO.Text = Trazos.SelectedValue

            Try
                RxNum.Text = ds.Tables("t1").Rows(0).Item("ordernum")
                ' ------------------------------------------------------------------
                If My.Settings.UseRxClass Then
                    Try : Rx.Vantage = ds.Tables("t1").Rows(0).Item("ordernum") : Catch : Rx.Vantage = 0 : End Try
                End If

            Catch ex As Exception

            End Try

            ' PFL Oct 16 2012 Siempre obtiene los valores del number01 = RefDigital = JobNum = N�mero del Trazo
            ' PFL Ago 1  2013 El campo jobnum de la tabla OmaTraces es de tipo nvarchar, por lo cual se debe usar una cadena string al momento de 
            ' especificar un query con restricci�n de WHERE jobnum='[trazo]', de no hacerlo as� se pueden producir errores de conversion (jobnum=integer)
            ' que hacen que falle el query.
            'Msg 245, Level 16, State 1, Line 1
            'Conversion failed when converting the nvarchar value 'Jobnum' to data type int.
            Rx.RefDigital = ds.Tables("t1").Rows(0).Item("RefDigital")
            RxWECO.Text = ds.Tables("t1").Rows(0).Item("RefDigital")

            If Not guarantee Then
                RxWECO.Text = ds.Tables("t1").Rows(0).Item("RefDigital")
                If (My.Settings.Plant = "VBENS") Then
                    RxWECO.Text = ds.Tables("t1").Rows(0).Item("OrdenLab")
                End If

                OriginalCustNum = ds.Tables("t1").Rows(0).Item("custnum")
                IsGratisRx = ds.Tables("t1").Rows(0).Item("IsGratis")

                ' # Pedro Far�as Lozano # 5 de Agosto 2013
                ' Necesario para imprimir el texto correcto del cliente virtual *(Lab. XXX) en la Rx impresa
                ComboClients.SelectedIndex = -1

                For Each DRV As DataRowView In ComboClients.Items
                    If DRV.Row.Item(1) = OriginalCustNum Then
                        ComboClients.SelectedIndex = custIndex
                        Exit For
                    End If
                    custIndex += 1
                Next


                ' ------------------------------------------------------------------
                If My.Settings.UseRxClass Then
                    Rx.RefDigital = ds.Tables("t1").Rows(0).Item("RefDigital")
                    Rx.Custnum = ds.Tables("t1").Rows(0).Item("custnum")
                    Rx.OriginalCustnum = ds.Tables("t1").Rows(0).Item("custnum")
                    Rx.IsGratis = ds.Tables("t1").Rows(0).Item("IsGratis")
                End If

            End If
            Try : OriginalRefDigital = CInt(RxWECO.Text) : Catch ex As Exception : OriginalRefDigital = 0 : End Try

            RxID.Text = ds.Tables("t1").Rows(0).Item("OrdenLab")
            EsferaLeft.Text = String.Format("{0:0.00}", CDbl(ds.Tables("t1").Rows(0).Item("EsferaL")))
            EsferaRight.Text = String.Format("{0:0.00}", CDbl(ds.Tables("t1").Rows(0).Item("EsferaR")))
            CilindroRight.Text = String.Format("{0:0.00}", CDbl(ds.Tables("t1").Rows(0).Item("CilindroR")))
            CilindroLeft.Text = String.Format("{0:0.00}", CDbl(ds.Tables("t1").Rows(0).Item("CilindroL")))
            EjeRight.Text = String.Format("{0:0}", CDbl(ds.Tables("t1").Rows(0).Item("EjeR")))
            EjeLeft.Text = String.Format("{0:0}", CDbl(ds.Tables("t1").Rows(0).Item("EjeL")))
            AdicionRight.Text = String.Format("{0:0.00}", CDbl(ds.Tables("t1").Rows(0).Item("AdicionR")))
            AdicionLeft.Text = String.Format("{0:0.00}", CDbl(ds.Tables("t1").Rows(0).Item("AdicionL")))
            MonoRight.Text = CDbl(ds.Tables("t1").Rows(0).Item("MonoR"))
            MonoLeft.Text = CDbl(ds.Tables("t1").Rows(0).Item("MonoL"))
            AlturaLeft.Text = CDbl(ds.Tables("t1").Rows(0).Item("AlturaL"))
            AlturaRight.Text = CDbl(ds.Tables("t1").Rows(0).Item("AlturaR"))
            DIPLejos.Text = CDbl(ds.Tables("t1").Rows(0).Item("DIPle"))
            DIPCerca.Text = CDbl(ds.Tables("t1").Rows(0).Item("DIPce"))
            Altura.Text = CDbl(ds.Tables("t1").Rows(0).Item("Altura"))
            ContornoA.Text = CDbl(ds.Tables("t1").Rows(0).Item("A"))
            ContornoB.Text = CDbl(ds.Tables("t1").Rows(0).Item("B"))
            ContornoED.Text = CDbl(ds.Tables("t1").Rows(0).Item("ED"))
            Dim bridge As Single = 0
            Try
                bridge = CSng(ds.Tables("t1").Rows(0).Item("Puente"))
                If bridge > 1000 Then
                    bridge = bridge / 100
                ElseIf (bridge > 100) Then
                    bridge = bridge / 10
                End If
            Catch : End Try

            ContornoPuente.Text = bridge

            MaterialID = ds.Tables("t1").Rows(0).Item("MaterialID")
            DesignID = ds.Tables("t1").Rows(0).Item("Dise�oID")
            RadioGradiente.Checked = ds.Tables("t1").Rows(0).Item("Gradiente")
            CostcoLente = ds.Tables("t1").Rows(0).Item("CostcoLente")
            CostcoTinte = ds.Tables("t1").Rows(0).Item("CostcoTinte")

            'CostcoAR = ds.Tables("t1").Rows(0).Item("CostcoAR")
            If (ds.Tables("t1").Rows(0).Item("CostcoAR") <> "") Then
                Try
                    SpecialDesignID = ds.Tables("t1").Rows(0).Item("CostcoAR")
                    Original_SpecialDesID = SpecialDesignID

                Catch : End Try
            End If

            CostcoAbrillantado = ds.Tables("t1").Rows(0).Item("CostcoPulido")
            FechaDeCreacion = ds.Tables("t1").Rows(0).Item("orderdate")

            'Este campo hace referencia al ArmazonID de la orden.
            'No se si sea correcto pero si existe un ArmazonID>0 entones se considera como RxManual.
            'As� estaba el c�digo, y ten�a un comentario err�neo
            'El valor original del campo Armazon number20 es
            '1-Digital |  0-Manual
            'RL ten�a este dato, pero estaba al rev�s
            '0-Digital |  1-Manual
            'IsManualRx = Not ds.Tables("t1").Rows(0).Item("contorno")
            IsManualRx = ds.Tables("t1").Rows(0).Item("contorno") < 1

            If (My.Settings.Plant = "VBENS") Then
                'getTrazoVB()
                ComboTrazos.ComboBox1.Items.Clear()
                ComboTrazos.ComboBox1.Items.Add(getTrazoVB(RxID.Text))

                'If ComboTrazos.ComboBox1.Items.Count > 0 Then
                ComboTrazos.ComboBox1.SelectedIndex = 0
                IsManualRx = False
                '    NoTrace = False
            Else
                ' PFL Oct 16 2012 
                If Rx.RefDigital > 0 Then
                    ComboTrazos.ComboBox1.Items.Clear()
                    ComboTrazos.ComboBox1.Items.Add(Rx.RefDigital)

                    'If ComboTrazos.ComboBox1.Items.Count > 0 Then
                    ComboTrazos.ComboBox1.SelectedIndex = 0

                End If
            End If



            Try
                NumArmazon = ds.Tables("t1").Rows(0).Item("ArmazonID")
                ArmazonOriginal = NumArmazon
            Catch ex As Exception
                NumArmazon = 0
                ArmazonOriginal = 0
            End Try
            MyFrame = New Frames(NumArmazon, Frames.ThisFrameStatus.[New])

            Try
                numpaq = ds.Tables("t1").Rows(0).Item("paquete")
                MyFrame.NumPaquete = numpaq
            Catch ex As Exception
                numpaq = ""
                MyFrame.NumPaquete = 0
            End Try

            If IsModifying Then
                ' Aqui entra solo si es modificacion y debe mostrar la opcion para escoger trazo como ke se escogio bien.
                IsFrameOK = True
                MyFrame.ReadStatus = Frames.ThisFrameReadStatus.Ok

                'If NumArmazon > 0 Then
                'solo si es diferente de ver bien checa el inventario
                If (My.Settings.Plant <> "VBENS") Then
                    If MyFrame.NumArmazon > 0 Then
                        'If CheckInventoryFrameFamily(NumArmazon) Then
                        If MyFrame.CheckInventoryFrameFamily Then
                            'CheckFrameStatus(NumArmazon)
                            MyFrame.CheckFrameStatus()
                            MyFrame.FrameStatus = Frames.ThisFrameStatus.NoChange
                            TBX_FrameDescription.Text = MyFrame.ArmazonDescripcion
                            PNL_Armazon.Visible = True
                            If MyFrame.ErrorID = 0 Then
                                PNL_Armazon.Enabled = False
                            Else
                                PNL_Armazon.Enabled = True
                            End If
                        End If
                    End If
                End If

                'If Not IsFrameOK Then
                If MyFrame.ReadStatus = Frames.ThisFrameReadStatus.NotRead Then
                    PIC_Armazon.Image = My.Resources.Lens_Error_2
                Else
                    PIC_Armazon.Image = My.Resources.Lens_OK_2
                End If

            End If


            If My.Settings.EnableOptisur Then
                Try
                    'Dim mydesign As Integer = ds.Tables("t1").Rows(0).Item("SpecialDesignID")
                    If SpecialDesignID > 0 Then
                        Dim mydesign As Integer = SpecialDesignID
                        Dim tt As New SqlDB
                        tt.SQLConn = New SqlClient.SqlConnection(Laboratorios.ConnStr)
                        Try
                            tt.OpenConn()
                            Dim ds1 As DataSet
                            ds1 = tt.SQLDS("select design, partnum from tblspecialdesigns with(nolock) where id = " & mydesign, "t1")
                            CBX_SpecialDesign.Enabled = False
                            CBX_SpecialDesign.Checked = True
                            CBX_SpecialDesign.Enabled = True
                            If Not ds1.Tables("t1").Rows(0).Item("design") = Nothing Then
                                CBX_SpecialDesign.Texto = "F.F. [" & ds1.Tables("t1").Rows(0).Item("design") & "]"
                                SD_Partnum = ds1.Tables("t1").Rows(0).Item("partnum")
                            End If
                        Catch ex As Exception
                            Throw New Exception("Error al obtener Dise�o Especial:" & vbCrLf & ex.Message)
                        Finally
                            SpecialDesignID = mydesign
                            tt.CloseConn()
                        End Try
                    End If
                Catch ex As Exception
                    CBX_SpecialDesign.Checked = False
                    SpecialDesignID = 0
                    SD_Partnum = ""
                End Try
            End If
            Try : date02 = ds.Tables("t1").Rows(0).Item("date02") : Catch ex As Exception : date02 = "" : End Try
            Try : date03 = ds.Tables("t1").Rows(0).Item("date03") : Catch ex As Exception : date03 = "" : End Try
            Try : date04 = ds.Tables("t1").Rows(0).Item("date04") : Catch ex As Exception : date04 = "" : End Try
            Try : date05 = ds.Tables("t1").Rows(0).Item("FechaARInLocalLab") : Catch ex As Exception : FechaARInLocalLab = "" : End Try
            Try : date06 = ds.Tables("t1").Rows(0).Item("date06") : Catch ex As Exception : date06 = "" : End Try
            Try : date07 = ds.Tables("t1").Rows(0).Item("date07") : Catch ex As Exception : date07 = "" : End Try

            Try : checkbox10 = ds.Tables("t1").Rows(0).Item("InARLab") : Catch ex As Exception : checkbox10 = False : End Try
            Try : checkbox11 = ds.Tables("t1").Rows(0).Item("checkbox11") : Catch ex As Exception : checkbox11 = False : End Try
            Try : checkbox12 = ds.Tables("t1").Rows(0).Item("ReceivedInLocalLab") : Catch ex As Exception : checkbox12 = False : End Try
            Try : checkbox13 = ds.Tables("t1").Rows(0).Item("checkbox13") : Catch ex As Exception : checkbox13 = False : End Try
            Try : checkbox16 = ds.Tables("t1").Rows(0).Item("OutARLab") : Catch ex As Exception : checkbox16 = False : End Try

            If My.Settings.UseRxClass Then
                Rx.OriginalRefDigital = OriginalRefDigital
                Rx.PONum = ds.Tables("t1").Rows(0).Item("OrdenLab")
                Rx.EsferaL = ds.Tables("t1").Rows(0).Item("EsferaL")
                Rx.EsferaR = ds.Tables("t1").Rows(0).Item("Esferar")
                Rx.CilindroR = ds.Tables("t1").Rows(0).Item("CilindroR")
                Rx.CilindroL = ds.Tables("t1").Rows(0).Item("CilindroL")
                Rx.EjeR = ds.Tables("t1").Rows(0).Item("EjeR")
                Rx.EjeL = ds.Tables("t1").Rows(0).Item("EjeL")
                Rx.AdicionR = ds.Tables("t1").Rows(0).Item("AdicionR")
                Rx.AdicionL = ds.Tables("t1").Rows(0).Item("AdicionL")


                Rx.Gradiente = ds.Tables("t1").Rows(0).Item("Gradiente")
                Rx.COSTCO_Lente = ds.Tables("t1").Rows(0).Item("CostcoLente")
                Rx.COSTCO_Tinte = ds.Tables("t1").Rows(0).Item("CostcoTinte")
                Rx.COSTCO_AR = ds.Tables("t1").Rows(0).Item("CostcoAR")
                Rx.COSTCO_Abrillantado = ds.Tables("t1").Rows(0).Item("CostcoPulido")
                Rx.FechaOrden = ds.Tables("t1").Rows(0).Item("orderdate")

                Rx.FechaEnvioVirtual = date02
                Rx.FechaRecibeVirtual = date07
                Rx.FechaSL = date03
                Rx.FechaAR = date04
                Rx.FechaFL = date06
                Rx.FechaRecibeLocal = date05
                Rx.RxReceivedVirtual = checkbox10
                Rx.RxReleasedVirtual = checkbox11
                Rx.RxReceivedLocal = checkbox12
                Rx.Reproceso = checkbox13

            End If


            Try : FechaEntradaFisica = ds.Tables("t1").Rows(0).Item("FechaEntradaFisica") : Catch : FechaEntradaFisica = "" : End Try   ' Aqui obtengo la FechaEntradaFisica de la Rx de COSTCO
            Try : FehcaEntradaWeb = ds.Tables("t1").Rows(0).Item("FechaEntradaWeb") : Catch : FehcaEntradaWeb = "" : End Try     ' Aqui obtengo la FechaEntradaWeb de la Rx de COSTCO
            Try : SeqID = ds.Tables("t1").Rows(0).Item("seqid") : Catch : SeqID = 0 : End Try                       ' Aqui obtengo el SeqID de la Rx de COSTCO
            If My.Settings.WorkingMode.ToUpper = "ONLINE" Then
                ' aki buscar en el local la fecha de entradafisica
                Dim tt As New SqlDB(Laboratorios.ConnStr)
                Dim data As DataSet

                Try
                    tt.OpenConn()
                    data = tt.SQLDS("select fecha from tblentradacostco with(nolock) where seqid = " & SeqID, "t1")
                    If data.Tables("t1").Rows.Count > 0 And SeqID <> 0 Then
                        FechaEntradaFisica = data.Tables("t1").Rows(0).Item("Fecha")
                    Else
                        FechaEntradaFisica = ""
                    End If
                Catch ex As Exception
                    FechaEntradaFisica = ""
                    tt.CloseConn()
                End Try
            End If
            'If RadioDigitalNew.Checked Then
            '    ComboTrazos.ComboBox1.DataSource = Nothing
            '    ComboTrazos.ComboBox1.Text = RxWECO.Text
            'End If
            If IsReceivingVirtualRx Then
                With ds.Tables("t1").Rows(0)
                    Try
                        Dim i As Integer
                        Dim Radios(511) As Integer

                        Try
                            For i = 0 To 511
                                Radios(i) = .Item("Int" & (i + 1).ToString)
                            Next
                            If My.Settings.OpticalProtocol = "OMA" Then
                                RadiosR = Convertir512To400(Radios)

                                'RadiosVirtuales = Radios
                            End If
                            If My.Settings.OpticalProtocol = "DVI" Then
                                RadiosR = Radios
                            End If

                            ' # PFL # Obtenemos el reflejo del trazo para obtener el lado (Izq) L
                            ' RadiosL = MirrorRadios(Pastilla.Sides.Right)
                            MirrorRadios(Pastilla.Sides.Right)

                            'ContornoPuente.Text = .Item("dbl") / 100           ' Se comento porque el puente ya se obtuvo arriba con lo que especifico el usuario

                            ComboTrazos.ComboBox1.DataSource = Nothing
                            ComboTrazos.ComboBox1.Text = RxWECO.Text
                            Me.Trazos_Click(Me, New EventArgs)
                            PanelPreviewTrazo.Visible = False
                            'PreviewLente = PreviewTrazo
                            PreviewLente.Visible = True
                            'PreviewTrazo.Visible = True
                            'PictureBox2.Visible = True
                            '         IsManualRx = False

                        Catch ex As Exception
                            PreviewLente.Image = My.Resources.ContornoManual
                        End Try
                    Catch ex As Exception
                    End Try
                End With

                ' Alg�n d�a la vista traer� el trazo para ambos ojos, mientras tanto seguir� comentado este c�digo
                'If ds.Tables(0).Rows.Count >= 2 and Rows(1).Item("Lado")='R' Then    ' Alg�n d�a la vista trer� los trazos de ambos ojos
                '    With ds.Tables("t1").Rows(1)
                '        Dim i As Integer
                '        Dim Radios(511) As Integer
                '        Try
                '            For i = 0 To 511
                '                Radios(i) = .Item("Int" & (i + 1).ToString)
                '            Next
                '            If My.Settings.OpticalProtocol = "OMA" Then
                '                RadiosR = Convertir512To400(Radios)
                '                'RadiosVirtuales = Radios
                '            End If
                '            If My.Settings.OpticalProtocol = "DVI" Then
                '                RadiosR = Radios
                '            End If
                '        Catch
                '        End Try
                '    End With
                'End If

            End If




            Try
                TextComentarios.Text = ds.Tables("t1").Rows(0).Item("ComentariosLab")
                TextComentarios.Text = TextComentarios.Text.Replace("[FREE_RX]", "")
                TextComentarios.Text = TextComentarios.Text.Replace("[GARANTIA_RX]", "")
                TextComentarios.Text = TextComentarios.Text.Replace("[WEB_RX]", "")
                If TextComentarios.Text.Contains("[HAS_L_EYE]") Then
                    LeftRequested = True
                    TextComentarios.Text = TextComentarios.Text.Replace("[HAS_L_EYE]", "")
                End If
                If TextComentarios.Text.Contains("[HAS_R_EYE]") Then
                    RightRequested = True
                    TextComentarios.Text = TextComentarios.Text.Replace("[HAS_R_EYE]", "")
                End If
                If TextComentarios.Text.Length > 0 Then
                    PanelComentarios.Visible = True
                    TextComentarios.ReadOnly = False
                End If
            Catch ex As Exception
                TextComentarios.Clear()
                PanelComentarios.Visible = False
                TextComentarios.ReadOnly = True
            End Try
            Try
                IsGarantia = ds.Tables("t1").Rows(0).Item("IsGarantia")
                CheckGarantia.Checked = IsGarantia
            Catch ex As Exception
            End Try

            Dim t As SqlDB
            t = New SqlDB(My.Settings.LocalServer, My.Settings.DBUser, My.Settings.DBPassword, My.Settings.LocalDBName)
            'If WorkingOnLine Then
            '    t = New SqlDB(ChooseServer, My.Settings.DBUser, My.Settings.DBPassword, DataBase)
            'Else
            'End If

            If Not guarantee Then
                Try
                    t.OpenConn()
                    Dim ds1 As DataSet
                    ds1 = t.SQLDS("select userinteger2 from orderhed with(nolock) where company = 'TRECEUX' and orderhed.ordernum = " & RxNum.Text, "t1")
                    Me.RxNumLocal = ds1.Tables("t1").Rows(0).Item("userinteger2")
                Catch ex As Exception
                    Me.RxNumLocal = 0
                Finally
                    t.CloseConn()

                End Try


                'ARLab = CSng(ds.Tables("t1").Rows(0).Item("ARLab"))
                If RxType = RxTypes.NormalRx Then
                    'verificamos si la Rx es un AR Y ha sido recibida en el laboratrio local proveniente del lab remoto, si es si la fecha inical de recibimiento 
                    'es "Now" y se almacena en el campo orderhed.date05
                    If IsLocalReceive Then
                        FechaInicial = Now

                        ' Se asigna el rxlocal original con que se envio la receta al virtual. fgarcia Abril 27, 2011.
                        Me.RxWECO.Text = RxNumLocal
                        Me.OriginalRefDigital = RxNumLocal

                        ' Se agrego esta condicion para que se agregue la linea del armazon en el detalle de la orden
                        ' cuando se esta haciendo una recepcion local.
                        If (MyFrame.NumArmazon > 0) Then
                            'IsFrameOK = True
                            MyFrame.ReadStatus = Frames.ThisFrameReadStatus.Ok
                        End If

                    Else
                        FechaInicial = ds.Tables("t1").Rows(0).Item("FechaInicial")
                    End If
                    'guardamos si la receta ya fue recibida en el lab local
                    IsARReceived = ds.Tables("t1").Rows(0).Item("ReceivedInLocalLab")
                    'guardamos la fecha en la que entro AR al lab local
                    FechaARInLocalLab = ds.Tables("t1").Rows(0).Item("FechaARInLocalLab")
                    If IsARReceived And Not FechaARInLocalLab.ToString().StartsWith("1/1/1900") Then
                        FechaInicial = FechaARInLocalLab
                    End If

                    'FechaEntrada = ds.Tables("t1").Rows(0).Item("orderdate")
                    FechaEntrada = ds.Tables("t1").Rows(0).Item("FechaInicial")
                    IsVirtualRx = ds.Tables("t1").Rows(0).Item("IsVirtualRx")
                    Original_IsVirtualRx = ds.Tables("t1").Rows(0).Item("IsVirtualRx")
                    'SI LA RECETA QUE SE VA A MODIFICAR NO ES VIRTUAL, ENTONCES ES LOCAL
                    'asignamos la variable con true que nos dice que la rx es local
                    If Not Original_IsVirtualRx Then Original_IsLocalRx = True
                    If ds.Tables("t1").Rows(0).Item("ARLab") = "" Then
                        ARLab = 0
                        ARLabName = "NINGUNO"
                        Original_ARLab = CInt(ARLab)
                        LabelARLab.Visible = False
                        TextComentarios.ReadOnly = False
                    Else
                        ARLab = ds.Tables("t1").Rows(0).Item("ARLab")
                        ARLabName = ds.Tables("t1").Rows(0).Item("ARLabName")
                        Original_ARLab = CInt(ARLab)
                        Dim plant As Integer = LabID()
                        If (ARLab = plant) And RxNum.Text.StartsWith(ARLab) Then
                            IsLocalRx = True
                            Original_IsLocalRx = True
                            TextComentarios.ReadOnly = False
                        ElseIf ARLab = plant And Not RxNum.Text.StartsWith(ARLab) Then
                            IsLocalRx = False
                            Original_IsLocalRx = False
                        Else
                            IsLocalRx = True
                            Original_IsLocalRx = False
                        End If
                        If ds.Tables("t1").Rows(0).Item("Antireflejante") > 0 Then
                            LabelARLab.Text = "AR " & ARLabName
                        Else
                            LabelARLab.Text = "VR " & ARLabName
                        End If
                        LabelARLab.Visible = True
                    End If
                    If ((IsVirtualRx) And (ARLab = LabID()) And (ARLab <> CInt(RxNum.Text.Substring(0, 2)))) Or IsReceivingVirtualRx Then
                        OriginalRefDigital = CInt(RxWECO.Text)
                        If IsLocalReceive Then  ' Se est� recibiendo de regreso la Rx Virtual (Bot�n Recibir Rx Virtual)
                            OrderExists = True
                        Else
                            OrderExists = False ' se est� bajando de la lista AR_List de Rx Virtuales para procesar
                        End If

                        If WorkingOnLine Then
                            t = New SqlDB(My.Settings.VantageServer, My.Settings.DBUser, My.Settings.DBPassword, My.Settings.ERPDBName)
                        Else
                            t = New SqlDB(My.Settings.LocalServer, My.Settings.DBUser, My.Settings.DBPassword, My.Settings.LocalDBName)
                        End If
                        Try
                            t.OpenConn()
                            Dim ds1 As DataSet
                            ds1 = t.SQLDS("select ('Lab. ' + nombre) as ARLab from TblLaboratorios with(nolock) where cl_lab = " & RxNum.Text.Substring(0, 2), "t1")
                            TextLabClient.Text = ds1.Tables("t1").Rows(0).Item("arlab")
                            ''t = New SqlDB(My.Settings.VantageServer, My.Settings.DBUser, My.Settings.DBPassword, My.Settings.VantageServer)
                            'ds1 = t.SQLDS("select userinteger2 from MfgSys80.dbo.orderhed with(nolock) where MfgSys80.dbo.orderhed.ordernum = " & RxNum.Text, "t1")
                            'Me.RxNumLocal = ds1.Tables("t1").Rows(0).Item("userinteger2")

                        Catch ex As Exception
                            If ex.Message.Contains("Error de Conexion") Then
                                ChangeWorkingStatus(WorkingStatusType.OffLine)
                            End If
                            TextLabClient.Text = "LAB X AR"
                        Finally
                            t.CloseConn()
                        End Try
                        ComboClients.Visible = False
                        TextLabClient.Visible = True
                        TextComentarios.ReadOnly = True
                    End If
                ElseIf RxType = RxTypes.WebRx Then
                    ' El framenum se utilizara para lo de los trazos con item de costco
                    'FrameNum = ds.Tables("t1").Rows(0).Item("NumArmazon")
                    FrameNum = ds.Tables("t1").Rows(0).Item("ArmazonID")
                    ComboArmazon2.ComboBox1.SelectedIndex = ds.Tables("t1").Rows(0).Item("contorno") - 1
                End If               '******************************************

            End If
            Try
                ComboArmazon2.ComboBox1.SelectedIndex = ds.Tables("t1").Rows(0).Item("tipoarmazon") - 1
            Catch ex As Exception
            End Try
            '******************************************
            If ds.Tables("t1").Rows(0).Item("Tinte") > 0 Then
                'CheckTintes.Checked = True
                'If RxType = RxTypes.NormalRx Then
                Dim value As String = ds.Tables("t1").Rows(0).Item("Tinte")
                t = New SqlDB(My.Settings.LocalServer, My.Settings.DBUser, My.Settings.DBPassword, My.Settings.LocalDBName)
                Try
                    t.OpenConn()
                    Dim nds As DataSet = t.SQLDS("select color from TblColors where cl_color = " & value, "t1")
                    ComboTinte.Texto = nds.Tables("t1").Rows(0).Item("color")
                    Select Case ds.Tables("t1").Rows(0).Item("Tono")
                        Case 1 : CheckLevel1.Checked = True
                        Case 2 : CheckLevel2.Checked = True
                        Case 3 : CheckLevel3.Checked = True
                    End Select
                Catch ex As Exception
                Finally
                    t.CloseConn()
                End Try
                'End If
            Else
                ComboTinte.Texto = "NINGUNO"
                CheckTintes.Checked = False
                CheckLevel1.Checked = False
                CheckLevel2.Checked = False
                CheckLevel3.Checked = False
            End If
            If ds.Tables("t1").Rows(0).Item("Foco") Then

            End If
            If ds.Tables("t1").Rows(0).Item("Antireflejante") > 0 Then
                CheckAR.Checked = True
                Select Case ds.Tables("t1").Rows(0).Item("Antireflejante")
                    Case 1 : CheckAR.Checked = True
                    Case 2 : CheckARGold.Checked = True
                    Case 3 : CheckARMatiz.Checked = True
                End Select
                If guarantee Then
                    PanelInformacion.Visible = True
                    If CheckAR.Checked Then Me.CheckAR_Changed(CheckAR, New EventArgs)
                    If CheckARGold.Checked Then Me.CheckAR_Changed(CheckARGold, New EventArgs)
                    If CheckARMatiz.Checked Then Me.CheckAR_Changed(CheckARMatiz, New EventArgs)
                End If
            Else
                CheckAR.Checked = False
                CheckARGold.Checked = False
                CheckARMatiz.Checked = False
            End If
            Try
                If ds.Tables("t1").Rows(0).Item("Biselado") Then
                    CheckBiselado.Checked = True
                Else
                    CheckBiselado.Checked = False
                End If
            Catch ex As Exception
                CheckBiselado.Checked = False
            End Try
            If RxType = RxTypes.NormalRx Then
                If ds.Tables("t1").Rows(0).Item("Coated") Then
                    CheckRLX.Checked = True
                Else
                    CheckRLX.Checked = False
                End If
            End If
            If ds.Tables("t1").Rows(0).Item("Abrillantado") Then
                CheckAbril.Checked = True
            Else
                CheckAbril.Checked = False
            End If

            ' BANDERA_OPTIMIZAR
            If (My.Settings.Optimizar) Then
                If ds.Tables("t1").Rows(0).Item("Optimizado") Then
                    CheckOptim.Checked = True
                Else
                    CheckOptim.Checked = False
                End If
            End If

            Try
                PrismaR = ds.Tables("t1").Rows(0).Item("PrismaR")
                EjePrismaR = ds.Tables("t1").Rows(0).Item("EjePrismaR")
                PrismaL = ds.Tables("t1").Rows(0).Item("PrismaL")
                EjePrismaL = ds.Tables("t1").Rows(0).Item("EjePrismaL")
            Catch ex As Exception
                PrismaR = 0
                EjePrismaR = 0
                PrismaL = 0
                EjePrismaL = 0
            End Try
            If Not guarantee Then
                ListLeftParts.BeginUpdate()
                ListRightParts.BeginUpdate()
                SelectedRightEye.BeginUpdate()
                SelectedLeftEye.BeginUpdate()
                ListLeftParts.Visible = True
                ListRightParts.Visible = True
                SelectedLeftEye.Visible = True
                SelectedRightEye.Visible = True
                SelectedRightEye.Items.Clear()
                SelectedLeftEye.Items.Clear()
                CheckLEye.Checked = False
                CheckREye.Checked = False
                If ds.Tables("t1").Rows.Count > 0 Then
                    If ds.Tables("t1").Rows(0).Item("RxLado") = 1 Then
                        'Try
                        SelectedRightEye.Items.Add(New ItemWithValue(ds.Tables("t1").Rows(0).Item("partdescription"), ds.Tables("t1").Rows(0).Item("partnum")))
                        OriginalRightPart = ds.Tables("t1").Rows(0).Item("partnum")
                        'SelectedRightEye.Visible = True
                        SelectedRightEye.SelectedIndex = 0
                        ClaseIDR = ds.Tables("t1").Rows(0).Item("Clase")
                        'SelectedLeftEye.SelectedItem = 0
                        CheckREye.Checked = True
                        'Catch ex As Exception

                        'End Try
                    ElseIf ds.Tables("t1").Rows(0).Item("RxLado") = 0 Then
                        'Try
                        SelectedLeftEye.Items.Add(New ItemWithValue(ds.Tables("t1").Rows(0).Item("partdescription"), ds.Tables("t1").Rows(0).Item("partnum")))
                        OriginalLeftPart = ds.Tables("t1").Rows(0).Item("partnum")
                        'SelectedLeftEye.Visible = True
                        ClaseIDL = ds.Tables("t1").Rows(0).Item("Clase")
                        'SelectedLeftEye.SelectedIndex = 0
                        CheckLEye.Checked = True
                        'Catch ex As Exception

                        'End Try
                    End If
                    If ds.Tables("t1").Rows.Count > 1 Then
                        If ds.Tables("t1").Rows(1).Item("RxLado") = 1 Then
                            'Try
                            SelectedRightEye.Items.Add(New ItemWithValue(ds.Tables("t1").Rows(1).Item("partdescription"), ds.Tables("t1").Rows(1).Item("partnum")))
                            OriginalRightPart = ds.Tables("t1").Rows(1).Item("partnum")
                            'SelectedRightEye.Visible = True
                            SelectedRightEye.SelectedIndex = 0
                            ClaseIDR = ds.Tables("t1").Rows(1).Item("Clase")
                            'SelectedLeftEye.SelectedItem = 0
                            CheckREye.Checked = True
                            'Catch ex As Exception

                            'End Try
                        ElseIf ds.Tables("t1").Rows(1).Item("RxLado") = 0 Then
                            'Try
                            SelectedLeftEye.Items.Add(New ItemWithValue(ds.Tables("t1").Rows(1).Item("partdescription"), ds.Tables("t1").Rows(1).Item("partnum")))
                            OriginalLeftPart = ds.Tables("t1").Rows(1).Item("partnum")
                            'SelectedLeftEye.Visible = True
                            SelectedLeftEye.SelectedIndex = 0
                            ClaseIDL = ds.Tables("t1").Rows(1).Item("Clase")
                            'SelectedRightEye.SelectedItem = 0
                            CheckLEye.Checked = True
                            'Catch ex As Exception

                            'End Try
                        End If
                    End If
                End If
                'ShowLeftEye = Convert.ToBoolean(ds.Tables("t1").Rows(0).Item("CheckLeftEye"))
                'ShowRightEye = Convert.ToBoolean(ds.Tables("t1").Rows(0).Item("CheckRightEye"))

            End If

            If RxType = RxTypes.WebRx Then
                CheckLEye.Checked = True
                CheckREye.Checked = True
                ShowLeftEye = True
                ShowRightEye = True
            Else
                ShowLeftEye = Convert.ToBoolean(ds.Tables("t1").Rows(0).Item("CheckLeftEye"))
                ShowRightEye = Convert.ToBoolean(ds.Tables("t1").Rows(0).Item("CheckRightEye"))

                'CheckLEye.Checked = True
                'CheckREye.Checked = True
            End If
            If IsLocalReceive Then
                ShowLeftEye = LeftRequested
                ShowRightEye = RightRequested
            End If

            If (Not IsModifying) And (Not IsReceivingVirtualRx) And (Not IsLocalReceive) And Not AL.ARProcess Then

                Try
                    ComboOtros.SelectedValue = MaterialID & "," & DesignID
                Catch ex As Exception

                End Try
                Try
                    ComboMonofocal.SelectedValue = MaterialID & "," & DesignID
                Catch ex As Exception

                End Try
                Try
                    ComboBifocal.SelectedValue = MaterialID & "," & DesignID
                Catch ex As Exception

                End Try
                Try
                    ComboProgresivo.SelectedValue = MaterialID & "," & DesignID
                Catch ex As Exception

                End Try
            End If

            Dim Material As Integer = 0
            Dim Design As Integer = 0

            If RxType = RxTypes.NormalRx Then
                ListLeftParts.Items.Clear()
                ListRightParts.Items.Clear()
                ListLeftParts.Items.AddRange(SelectedLeftEye.Items)
                ListRightParts.Items.AddRange(SelectedRightEye.Items)
                If CheckLEye.Checked And ListLeftParts.Items.Count > 0 Then ListLeftParts.SelectedIndex = 0
                If CheckREye.Checked And ListRightParts.Items.Count > 0 Then ListRightParts.SelectedIndex = 0
            End If

            'Label5.Visible = True
            RxWECO.Visible = True
            If IsReceivingVirtualRx Then
                PanelGraduacion.Enabled = True
                PanelOpciones.Enabled = False
            Else
                PanelGraduacion.Enabled = True
                PanelOpciones.Enabled = True
                PanelArmazon.Enabled = True
            End If
            If IsLocalReceive Then
                PanelErrores.Visible = False
            End If
            Try
                If ds.Tables("t1").Rows(0).Item("IsWebRx") Then
                    RxType = RxTypes.WebRx
                End If
            Catch ex As Exception

            End Try

        Catch ex As Exception
            MsgBox("Error al obtener datos de la receta:" & vbCrLf & ex.Message, MsgBoxStyle.Exclamation)
        Finally
            SelectedRightEye.Visible = False
            SelectedLeftEye.Visible = False
            ListLeftParts.EndUpdate()
            ListRightParts.EndUpdate()
            SelectedLeftEye.EndUpdate()
            SelectedRightEye.EndUpdate()
        End Try
    End Sub
    Public Delegate Sub ExecutePingThread()


    Public Sub ChangeWorkingStatus(ByVal Status As WorkingStatusType)
        Select Case Status
            Case WorkingStatusType.OnLine
                If WorkingOnLine = False Then
                    WorkingOnLine = True
                    '        PingTimer.Enabled = False
                    '        'ConnectionStatus.Invoke(New AddText(AddressOf ConnectionStatus.Text), "Conectado a Servidor")
                    '        ConnectionStatus.Text = "Conectado a Servidor"
                    '        StatusPic.Image = My.Resources.greencirclet
                    '        CloseRx.Enabled = True
                    '        ReceiveMaterial.Enabled = True
                    '        pe.BTNGEN.Enabled = True
                    '        Notify.ShowBalloonTip(1500, Application.ProductName, "Se ha reestablecido la conexi�n con el Servidor Principal.", ToolTipIcon.Info)
                    If Not PingWorker.IsBusy Then
                        RefreshOnlineIndicator()
                    End If
                End If
            Case WorkingStatusType.OffLine
                If WorkingOnLine = True Then
                    WorkingOnLine = False
                    '        PingTimer.Enabled = True
                    '        ConnectionStatus.Text = "Desconectado"
                    '        StatusPic.Image = My.Resources.light
                    '        CloseRx.Enabled = False
                    '        ReceiveMaterial.Enabled = False
                    '        pe.BTNGEN.Enabled = False
                    '        Notify.ShowBalloonTip(1500, Application.ProductName, "Se ha perdido la conexi�n con el Servidor Principal." & vbCrLf & "Se trabajar� de manera local.", ToolTipIcon.Warning)
                    If Not PingWorker.IsBusy Then
                        RefreshOnlineIndicator()
                    End If
                End If
        End Select
    End Sub

    Public Sub AceptarManual_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles AceptarManualNew.Click
        Dim ContinuarRx As Boolean = True
        Dim IsGuaranteeFromOriginal As Boolean = False
        Dim custnum As Integer = ComboClients.SelectedValue

        If GetCadena(custnum) = My.Settings.CadenaLiverpoolID And Not IsModifying And RxType = RxTypes.NormalRx And Not IsVirtualRx Then

            Try
                ValidateLiverpoolPO(RxID.Text)
            Catch ex As Exception
                ContinuarRx = False
                MsgBox(ex.Message, MsgBoxStyle.Exclamation)
            End Try
            
        ElseIf GetCadena(custnum) = My.Settings.CadenaCostcoID And Not IsModifying And Not IsLocalReceive And Not IsVirtualRx Then

            If RxType <> RxTypes.WebRx Then
                If CheckGarantia.Checked Then
                    If ValidatePO(RxID.Text) Then
                        ContinuarRx = True
                    Else
                        MsgBox("Receta inv�lida, debe introducir el PO de COSTCO en No Rx.", MsgBoxStyle.Critical)
                        ContinuarRx = False
                    End If
                Else
                    MsgBox("Las Rx de Costco deben entrar por internet, ya no pueden ser manuales", MsgBoxStyle.Exclamation)
                    ContinuarRx = False
                End If
            Else
                If Not ValidatePO(RxID.Text) Then
                    MsgBox("Receta inv�lida, debe introducir el PO de COSTCO en No Rx.", MsgBoxStyle.Critical)
                    ContinuarRx = False

                ElseIf ExistsForWeb(RxID.Text) And RxType <> RxTypes.WebRx Then
                    MsgBox("Esta Rx se encuentra en las Ordenes de Internet." + vbCrLf + "Favor de capturarla desde esa ventana.", MsgBoxStyle.Exclamation)
                    ContinuarRx = False

                End If
            End If
        Else
            If Len(RxID.Text) > My.Settings.RxLength And RxType = RxTypes.NormalRx And Not IsVirtualRx And Not IsModifying Then
                ContinuarRx = False
                MsgBox("Las Rx no pueden ser de mas de " & My.Settings.RxLength & " d�gitos", MsgBoxStyle.Critical)

            End If
        End If
        If Me.ComboArmazon2.Texto = "" And ContinuarRx Then
            ContinuarRx = False
            MsgBox("No se ha capturado el Tipo de Armaz�n." + vbCrLf + "Favor de seleccionar el tipo correspondiente.", MsgBoxStyle.Critical)
        End If

        If ContinuarRx Then
            'PanelTipoCaptura.Visible = True
            ' Checo si existe ya la receta para el mismo doctor el mismo dia
            '---------------------------------------------------------
            Dim ds As New DataSet
            Dim t As SqlDB
            If WorkingOnLine Then
                t = New SqlDB(ChooseServer, My.Settings.DBUser, My.Settings.DBPassword, DataBase)
            Else
                t = New SqlDB(My.Settings.LocalServer, My.Settings.DBUser, My.Settings.DBPassword, My.Settings.LocalDBName)
            End If
            Try
                t.OpenConn()
                ds = t.SQLDS("select ordernum from orderhed with(nolock) where shortchar01 = '" & RxID.Text & "' and orderdate = '" & Now.Date & "' and custnum = " & ComboClients.SelectedValue, "t1")
                If (ds.Tables("t1").Rows.Count > 0) And (Not IsModifying) And (Not IsReceivingVirtualRx) Then
                    MsgBox("Esta receta ya ha sido registrada para este cliente en el sistema.", MsgBoxStyle.Information)
                    InitValues()
                    Return
                End If

            Catch ex As Exception
            Finally
                t.CloseConn()
            End Try

            If RxID.Text <> "" Then
                IsManualRx = True
                Dim ShowNew As Boolean = False
                Dim JobNum As String = RxID.Text

                ' Obtengo los ultimos 4 digitos de la orden de laboratorio
                '---------------------------------------------------------
                JobNum = JobNum.PadLeft(4, "0")
                JobNum = JobNum.Substring(JobNum.Length - 4, 4)

                Try
                    t.OpenConn()
                    ds = t.SQLDS("select Armazon as contorno,* from VwRecetasTRECEUX where RefDigital = " & RxID.Text & " and (plant = " & LabID() & " or plant2 = " & LabID() & ")", "t1")

                    'ds = t.SQLDS("select Armazon as contorno,* from VwRecetasTRECEUX where RefDigital = " & RxID.Text & " and (plant = " & LabID() & " or plant2 = " & LabID() & ")", "t1")

                    If ds.Tables("t1").Rows.Count > 0 Then
                        Dim Mensaje As String
                        Dim Conn As New SqlDB(My.Settings.LocalServer, My.Settings.DBUser, My.Settings.DBPassword, My.Settings.LocalDBName)
                        Conn.OpenConn()

                        Try
                            Dim Comm As New SqlCommand("SP_TrazosManuales" & My.Settings.OpticalProtocol, Conn.SQLConn)
                            Comm.CommandType = Data.CommandType.StoredProcedure
                            Comm.Parameters.AddWithValue("@JobNum", JobNum)
                            Comm.Parameters.Add("@JobUnico", Data.SqlDbType.VarChar, 100)
                            Comm.Parameters("@JobUnico").Direction = Data.ParameterDirection.Output
                            Comm.ExecuteNonQuery()
                            Mensaje = Comm.Parameters("@JobUnico").Value
                            RxWECO.Text = Mensaje
                            RxWECO.Visible = True
                            ShowNew = True

                        Catch ex As Exception
                            MsgBox(ex.Message)

                        Finally
                            Conn.CloseConn()
                        End Try
                    Else
                        Dim Mensaje As String
                        Dim Conn As New SqlDB(My.Settings.LocalServer, My.Settings.DBUser, My.Settings.DBPassword, My.Settings.LocalDBName)
                        Conn.OpenConn()

                        Try
                            Dim Comm As New SqlCommand("SP_TrazosManuales" & My.Settings.OpticalProtocol, Conn.SQLConn)
                            Comm.CommandType = Data.CommandType.StoredProcedure
                            Comm.Parameters.AddWithValue("@JobNum", JobNum)
                            Comm.Parameters.Add("@JobUnico", Data.SqlDbType.VarChar, 100)
                            Comm.Parameters("@JobUnico").Direction = Data.ParameterDirection.Output
                            Comm.ExecuteNonQuery()
                            Mensaje = Comm.Parameters("@JobUnico").Value
                            RxWECO.Text = Mensaje
                            RxWECO.Visible = True

                            'Si se cambio el trazo, establecer los siguientes valores en el objeto OMA
                            If ChangingTraces And My.Settings.HasMEIEdger Then
                                OMA.SetHBOX(ContornoA.Text, ContornoA.Text)
                                OMA.SetVBOX(ContornoB.Text, ContornoB.Text)
                                OMA.SetDIA(ContornoED.Text, ContornoED.Text)
                                OMA.SetDBL(ContornoPuente.Text)
                                OMA.SetJOB(Me.StuffRx(Me.RxWECO.Text))
                            End If

                        Catch ex As Exception
                            MsgBox(ex.Message)

                        Finally
                            Conn.CloseConn()
                        End Try

                        ShowNew = True
                    End If
                Catch ex As Exception
                    MsgBox(ex.Message)
                Finally
                    t.CloseConn()
                End Try

                Dim cancelagarantia As Boolean = False

                If CheckGarantia.Checked And Not IsModifying And Not Me.IsReceivingVirtualRx Then
                    Dim lt = New SqlDB
                    Try
                        lt.SQLConn = New SqlConnection(Laboratorios.ConnStr)
                        lt.OpenConn()
                        ds = lt.SQLDS("select checkbox03 as IsCostco from customer with(nolock) where company = 'TRECEUX' and custnum = " & Me.ComboClients.SelectedValue, "t1")

                        If ds.Tables("t1").Rows(0).Item("IsCostco") = 1 Then
                            Dim sqlstr As String = "select Fecha, Custnum, LabID, OrdenLab,A, B, ED, Puente, Antireflejante, MaterialID, DisenoID as Dise�oID, Tinte, EsferaR, CilindroR, EjeR, AdicionR," & _
                                " EsferaL, CilindroL, EjeL, AdicionL, Foco, AlturaR, MonoR, AlturaL, MonoL, DIPce, DIPle, Altura, NumArmazon, Contorno, " & _
                                " CapturaArmazon, Biselado, Gradiente, Status, Name, Abrillantado, ComentariosLab, CostcoLente, CostcoAR, IsGratis, CostcoPulido, CostcoTinte, CustID, orderdate, seqid, material, diseno, FechaEntradaWeb, FechaEntradaFisica " & _
                                " from vwprocessedweborders with(nolock) where ordenlab = '" & RxID.Text & "'"
                            ds = lt.SQLDS(sqlstr, "t1")

                            If ds.Tables("t1").Rows.Count > 0 Then
                                RxType = RxTypes.WebRx
                                FillRx(ds, True)
                                RxNum.Text = ""

                            Else
                                MsgBox("No se encontro informacion de la Rx Original.", MsgBoxStyle.Exclamation)
                                cancelagarantia = True

                            End If
                            'Else
                            '    Dim x As New EditValues()
                            '    x.LabelValue.Text = "Introduce el #Item de COSTCO"
                            '    x.Text = "Garant�as"
                            '    x.ShowDialog()
                            '    If x.Changed Then
                            '        'If x.TextValue.Text <> "" Then
                            '        Dim tt As SqlDB = New SqlDB(My.Settings.LocalServer, My.Settings.DBUser, My.Settings.DBPassword, My.Settings.LocalDBName)
                            '        Try
                            '            tt.OpenConn()
                            '            ds = tt.SQLDS("select * from costcopricelist with(nolock) where costco_item = '" & x.TextValue.Text & "'", "t1")
                            '            If ds.Tables("t1").Rows(0).Item("costco_item") = x.TextValue.Text Then
                            '                CostcoLente = x.TextValue.Text
                            '            End If
                            '        Catch ex As Exception
                            '            Throw New Exception("#Item de COSTCO inv�lido")
                            '        Finally
                            '            tt.CloseConn()
                            '        End Try
                            '        'End If
                            '    Else
                            '        cancelagarantia = True
                            '    End If
                            'End If
                        End If

                    Catch ex As Exception
                        cancelagarantia = True
                        MsgBox(ex.Message, MsgBoxStyle.Critical)

                    Finally
                        t.CloseConn()
                        lt.closeconn()
                    End Try

                    If Not cancelagarantia And CheckGarantia.Checked Then
                        IsGarantia = True
                    Else
                        ShowNew = False
                        Exit Sub
                    End If
                End If

                Dim CustomerOK As Boolean = True

                If Not IsModifying Then
                    Dim tt As SqlDB
                    MyFrame.ReadStatus = Frames.ThisFrameReadStatus.Ok
                    tt = New SqlDB(My.Settings.LocalServer, My.Settings.DBUser, My.Settings.DBPassword, My.Settings.LocalDBName)

                    Try
                        tt.OpenConn()
                        Dim dst As New DataSet
                        Dim sqlstr As String = "SELECT customer.company, customer.custid, customer.custnum, customer.name, terms.termscode, terms.description, terms.duedays, customer.credithold " & _
                            "FROM customer WITH (nolock) INNER JOIN " & _
                            "terms WITH (nolock) ON customer.company = terms.company AND customer.termscode = terms.termscode " & _
                            "WHERE (customer.company = 'TRECEUX') and custnum = " & ComboClients.SelectedValue.ToString
                        dst = tt.SQLDS(sqlstr, "t1")

                        If dst.Tables("t1").Rows.Count > 0 Then
                            With (dst.Tables("t1").Rows(0))
                                If .Item("credithold") = 1 Then
                                    CustomerOK = False
                                    ''MsgBox("El Cliente " + .Item("name") + " tiene t�rminos de pago a " + .Item("description") + "." + vbCrLf + "Algunas Rx han excedido este tiempo por lo cual no se permite ingresar m�s." + vbCrLf + "Favor de consultar con el coordinador su estado de cuenta.", MsgBoxStyle.Critical)
                                    MsgBox("Algunas Rx del cliente " + .Item("name") + " han excedido el limite de termino de pago, por lo cual el sistema no le permite ingresar Rx adicionales. Favor de consultar con el coordinador el estatus de su estado de cuenta.", MsgBoxStyle.Critical)
                                Else

                                    CustomerOK = True
                                End If
                            End With

                        End If
                    Catch ex As Exception

                    Finally
                        tt.CloseConn()
                    End Try
                End If

                If CustomerOK Then
                    ShowNew = True
                Else
                    ShowNew = False
                End If

                If CustomerOK Then
                    If (ShowNew) And ((RxType = RxTypes.NormalRx) Or (RxType = RxTypes.WebRx And IsGarantia) Or (RxType = RxTypes.WebRx And RadiosL(0) <> 0)) Then
                        If ChangingTraces Then
                            RxNumLocal = RxWECO.Text
                        End If

                        If Val(RxID.Text) > 0 Then
                            ' ------------------------------------------------------------
                            ' Si no tiene trazo virtual, entonces poner foto sin trazo
                            ' ------------------------------------------------------------
                            If RadiosL(0) = 0 Then
                                PreviewLente.Image = My.Resources.ContornoManual
                            End If
                            ' ------------------------------------------------------------

                            PanelPreviewTrazo.Visible = False
                            If Not OrderExists And Not IsReceivingVirtualRx And Not IsGuaranteeFromOriginal Then
                                ContornoA.Text = "0"
                                ContornoB.Text = "0"
                                ContornoED.Text = "0"
                                ContornoPuente.Text = "0"
                            End If

                            PanelInformacion.Location = New Point(6, 187)
                            If Not IsModifying Then
                                PanelArmazon.Enabled = False
                                PanelRx.Enabled = False
                            End If

                            PanelOpciones.Visible = True
                            AceptarManualNew.Visible = False
                            Cancel.Enabled = True

                            ComboTrazos.Enabled = False
                            ComboArmazon2.Enabled = False

                            RadioManualNew.Enabled = False
                            RadioDigitalNew.Enabled = False

                            CheckGarantia.Enabled = False

                            PanelInformacion.Enabled = True
                            AdicionRight_Leave(Me, e)
                            EsferaRight.Select()

                            If IsReceivingVirtualRx Then
                                LeftRead.Text = OriginalLeftPart
                                RightRead.Text = OriginalRightPart

                                If Not ShowLeftEye Then
                                    CheckLEye.Checked = False
                                    Enter_RightPart()
                                End If

                                If Not ShowRightEye Then
                                    CheckREye.Checked = False
                                    Enter_LeftPart()
                                End If
                            End If

                            AceptarDigitalNew.Visible = False
                            CancelarDigitalNew.Visible = False
                            PanelInformacion.Visible = True
                            LeftBarcodeRead = False
                            RightBarcodeRead = False

                            If CheckGarantia.Checked Then
                                IsGarantia = True
                            Else
                                IsGarantia = False
                            End If

                        Else
                            MsgBox("La orden de laboratorio es inv�lida.", MsgBoxStyle.Exclamation)
                        End If
                    Else
                        Dim x As String = ComboTinte.Texto
                        PanelInformacion.Enabled = True
                        PanelInformacion.Visible = True

                        If x <> ComboTinte.Texto Then
                            ComboTinte.Texto = x
                        End If

                        PreviewLente.Image = My.Resources.ContornoManual
                        ComboTrazos.Enabled = False
                        ComboArmazon2.Enabled = False

                    End If
                End If

                If PanelInformacion.Visible Then
                    AceptarManualNew.Visible = False
                    AceptarDigitalNew.Visible = False
                    CancelarDigitalNew.Visible = False
                End If

                EsferaRight.Focus()
                EsferaRight.Select()
            Else
                MsgBox("Orden de Laboratorio inv�lida.", MsgBoxStyle.Exclamation)
                RxID.Select()

            End If
        End If
    End Sub

    Public Sub AceptarVirtualAR()

        Dim ContinuarRx As Boolean = True
        Dim IsGuaranteeFromOriginal As Boolean = False
        Dim custnum As Integer = ComboClients.SelectedValue

        If GetCadena(custnum) = My.Settings.CadenaLiverpoolID And Not IsModifying And RxType = RxTypes.NormalRx And Not IsVirtualRx Then

            Try
                ValidateLiverpoolPO(RxID.Text)
            Catch ex As Exception
                ContinuarRx = False
                MsgBox(ex.Message, MsgBoxStyle.Exclamation)
            End Try

        ElseIf GetCadena(custnum) = My.Settings.CadenaCostcoID And Not IsModifying And Not IsLocalReceive And Not IsVirtualRx Then

            If RxType <> RxTypes.WebRx Then
                If CheckGarantia.Checked Then
                    If ValidatePO(RxID.Text) Then
                        ContinuarRx = True
                    Else
                        MsgBox("Receta inv�lida, debe introducir el PO de COSTCO en No Rx.", MsgBoxStyle.Critical)
                        ContinuarRx = False
                    End If
                Else
                    MsgBox("Las Rx de Costco deben entrar por internet, ya no pueden ser manuales", MsgBoxStyle.Exclamation)
                    ContinuarRx = False
                End If
            Else
                If Not ValidatePO(RxID.Text) Then
                    MsgBox("Receta inv�lida, debe introducir el PO de COSTCO en No Rx.", MsgBoxStyle.Critical)
                    ContinuarRx = False

                ElseIf ExistsForWeb(RxID.Text) And RxType <> RxTypes.WebRx Then
                    MsgBox("Esta Rx se encuentra en las Ordenes de Internet." + vbCrLf + "Favor de capturarla desde esa ventana.", MsgBoxStyle.Exclamation)
                    ContinuarRx = False

                End If
            End If
        Else
            If Len(RxID.Text) > My.Settings.RxLength And RxType = RxTypes.NormalRx And Not IsVirtualRx And Not IsModifying Then
                ContinuarRx = False
                MsgBox("Las Rx no pueden ser de mas de " & My.Settings.RxLength & " d�gitos", MsgBoxStyle.Critical)

            End If
        End If
        If Me.ComboArmazon2.Texto = "" And ContinuarRx Then
            ContinuarRx = False
            MsgBox("No se ha capturado el Tipo de Armaz�n." + vbCrLf + "Favor de seleccionar el tipo correspondiente.", MsgBoxStyle.Critical)
        End If

        If ContinuarRx Then
            ' Checo si existe ya la receta para el mismo doctor el mismo dia
            '---------------------------------------------------------
            Dim ds As New DataSet
            Dim t As SqlDB
            t = New SqlDB(My.Settings.LocalServer, My.Settings.DBUser, My.Settings.DBPassword, My.Settings.LocalDBName)
            Try
                t.OpenConn()
                ds = t.SQLDS("select ordernum from orderhed with(nolock) where shortchar01 = '" & RxID.Text & "' and orderdate = '" & Now.Date & "' and custnum = " & OriginalCustNum, "t1")
                If (ds.Tables("t1").Rows.Count > 0) And (Not IsModifying) And (Not IsReceivingVirtualRx) Then
                    MsgBox("Esta receta ya ha sido registrada para este cliente en el sistema.", MsgBoxStyle.Information)
                    InitValues()
                    Return
                End If

            Catch ex As Exception
            Finally
                t.CloseConn()
            End Try

            If RxID.Text <> "" Then
                IsManualRx = True

                Dim cancelagarantia As Boolean = False

                If CheckGarantia.Checked And Not IsModifying And Not Me.IsReceivingVirtualRx Then
                    Dim lt = New SqlDB
                    Try
                        lt.SQLConn = New SqlConnection(Laboratorios.ConnStr)
                        lt.OpenConn()
                        ds = lt.SQLDS("select checkbox03 as IsCostco from customer with(nolock) where company = 'TRECEUX' and custnum = " & Me.ComboClients.SelectedValue, "t1")

                        If ds.Tables("t1").Rows(0).Item("IsCostco") = 1 Then
                            Dim sqlstr As String = "select Fecha, Custnum, LabID, OrdenLab,A, B, ED, Puente, Antireflejante, MaterialID, DisenoID as Dise�oID, Tinte, EsferaR, CilindroR, EjeR, AdicionR," & _
                                " EsferaL, CilindroL, EjeL, AdicionL, Foco, AlturaR, MonoR, AlturaL, MonoL, DIPce, DIPle, Altura, NumArmazon, Contorno, " & _
                                " CapturaArmazon, Biselado, Gradiente, Status, Name, Abrillantado, ComentariosLab, CostcoLente, CostcoAR, IsGratis, CostcoPulido, CostcoTinte, CustID, orderdate, seqid, material, diseno, FechaEntradaWeb, FechaEntradaFisica " & _
                                " from vwprocessedweborders with(nolock) where ordenlab = '" & RxID.Text & "'"
                            ds = lt.SQLDS(sqlstr, "t1")

                            If ds.Tables("t1").Rows.Count > 0 Then
                                RxType = RxTypes.WebRx
                                FillRx(ds, True)
                                RxNum.Text = ""

                            Else
                                MsgBox("No se encontro informacion de la Rx Original.", MsgBoxStyle.Exclamation)
                                cancelagarantia = True

                            End If
                        End If

                    Catch ex As Exception
                        cancelagarantia = True
                        MsgBox(ex.Message, MsgBoxStyle.Critical)

                    Finally
                        t.CloseConn()
                        lt.closeconn()
                    End Try

                End If


                If Not IsModifying Then
                    MyFrame.ReadStatus = Frames.ThisFrameReadStatus.Ok
                End If


                If ((RxType = RxTypes.NormalRx) Or (RxType = RxTypes.WebRx And IsGarantia) Or (RxType = RxTypes.WebRx And RadiosL(0) <> 0)) Then
                    If ChangingTraces Then
                        RxNumLocal = RxWECO.Text
                    End If

                    If Val(RxID.Text) > 0 Then
                        ' ------------------------------------------------------------
                        ' Si no tiene trazo virtual, entonces poner foto sin trazo
                        ' ------------------------------------------------------------
                        If RadiosL(0) = 0 Then
                            PreviewLente.Image = My.Resources.ContornoManual
                        End If
                        ' ------------------------------------------------------------

                        PanelPreviewTrazo.Visible = False
                        If Not OrderExists And Not IsReceivingVirtualRx And Not IsGuaranteeFromOriginal Then
                            ContornoA.Text = "0"
                            ContornoB.Text = "0"
                            ContornoED.Text = "0"
                            ContornoPuente.Text = "0"
                        End If

                        PanelInformacion.Location = New Point(6, 187)
                        If Not IsModifying Then
                            PanelArmazon.Enabled = False
                            PanelRx.Enabled = False
                        End If

                        PanelOpciones.Visible = True
                        AceptarManualNew.Visible = False
                        Cancel.Enabled = True

                        ComboTrazos.Enabled = False
                        ComboArmazon2.Enabled = False

                        RadioManualNew.Enabled = False
                        RadioDigitalNew.Enabled = False

                        CheckGarantia.Enabled = False

                        PanelInformacion.Enabled = True
                        AdicionRight_Leave(Me, Nothing)
                        EsferaRight.Select()

                        If IsReceivingVirtualRx Then
                            LeftRead.Text = OriginalLeftPart
                            RightRead.Text = OriginalRightPart

                            If Not ShowLeftEye Then
                                CheckLEye.Checked = False
                                Enter_RightPart()
                            End If

                            If Not ShowRightEye Then
                                CheckREye.Checked = False
                                Enter_LeftPart()
                            End If
                        End If

                        AceptarDigitalNew.Visible = False
                        CancelarDigitalNew.Visible = False
                        PanelInformacion.Visible = True
                        LeftBarcodeRead = False
                        RightBarcodeRead = False

                        If CheckGarantia.Checked Then
                            IsGarantia = True
                        Else
                            IsGarantia = False
                        End If

                    Else
                        MsgBox("La orden de laboratorio es inv�lida.", MsgBoxStyle.Exclamation)
                    End If
                Else
                    Dim x As String = ComboTinte.Texto
                    PanelInformacion.Enabled = True
                    PanelInformacion.Visible = True

                    If x <> ComboTinte.Texto Then
                        ComboTinte.Texto = x
                    End If

                    PreviewLente.Image = My.Resources.ContornoManual
                    ComboTrazos.Enabled = False
                    ComboArmazon2.Enabled = False

                End If

                If PanelInformacion.Visible Then
                    AceptarManualNew.Visible = False
                    AceptarDigitalNew.Visible = False
                    CancelarDigitalNew.Visible = False
                End If

                EsferaRight.Focus()
                EsferaRight.Select()
            Else
                MsgBox("Orden de Laboratorio inv�lida.", MsgBoxStyle.Exclamation)
                RxID.Select()

            End If
        End If
    End Sub


    Public Sub AceptarModificar(ByVal RxID As String)
        Dim ShowNew As Boolean = False

        Dim t As SqlDB
        Dim ds As DataSet
        Dim Local As Boolean

        t = New SqlDB(My.Settings.LocalServer, My.Settings.DBUser, My.Settings.DBPassword, My.Settings.LocalDBName)

        Try
            'PFL Oct 16 2012
            'Orden Sincronizada o no
            'Quiz� estamos trabajando en modo desconectado o no, o puede ser virtual o no. checkbox09=0 es local
            'KISS : Si no es virtual entonces es local.
            'Bueno, siempre no, mejor lo dejamos como estaba.
            ds = New DataSet
            t.OpenConn()
            'ds = t.SQLDS("select ordernum from orderhed with(nolock) where ordernum = " & RxID & " and sincronizada = 0", "t1")
            ds = t.SQLDS("select ordernum from orderhed with(nolock) where ordernum = " & RxID & " and checkbox09 = 0", "t1")
            If ds.Tables("t1").Rows.Count > 0 Then
                Local = True
            End If

        Catch ex As Exception
        Finally
            t.CloseConn()
        End Try

        'Pedro Far�as Lozano Oct 31, 2012
        'Si la orden es virtual (Local=False) se debe buscar el servidor corporativo.

        If (My.Settings.VantageServer.Length >= 8 And Local = False And Ping(My.Settings.VantageServer)) Then
            t = New SqlDB(My.Settings.VantageServer, My.Settings.DBUser, My.Settings.DBPassword, My.Settings.MfgSysDBName)
        Else
            t = New SqlDB(My.Settings.LocalServer, My.Settings.DBUser, My.Settings.DBPassword, My.Settings.LocalDBName)
        End If

        Try
            t.OpenConn()
            ds = New DataSet

            If Not ChangingTraces Then
                Dim Plant As Integer = LabID()
                Dim sqlstr As String = ""

                If (IsLocalReceive) And Not NoTrace Then
                    sqlstr = "select armazon as contorno, * from VwRecetasTRECEUX with(nolock) " & _
                             " where ordernum = " & RxID & " and (ReceivedInLocalLab = 0) and " & _
                             " (plant = " & Plant & " or plant2 = " & Plant & " or ARLab = " & Plant & ")"
                    ' # PFL # 18 Sep 2013 #
                    ' Hay laboratorios sin QA, por lo tanto siempre tendr�n outarlab=0
                    '" (outarlab = 1) and" & _

                    If Not NoTrace Then
                        Me.IsVirtualRx = True
                    End If
                Else
                    sqlstr = "select armazon as contorno, * from VwRecetasTRECEUX with(nolock) " & _
                             " where ordernum = " & RxID & " and (((ordernum like '" & Plant & "%') " & _
                             " and ((IsVirtualRx = 0) or (IsVirtualRx = 1 and ARLAB = " & Plant & ") or (IsVirtualRx = 1 and ARLab <> " & Plant & _
                             " and (ReceivedInLocalLab = 1 or (receivedinlocallab = 0 and inarlab = 0))))) or ((arlab = " & Plant & ") " & _
                             " and (inarlab = 1) and (outarlab = 0))) order by orderline"

                End If
                ds = t.SQLDS(sqlstr, "t1")
                ' Si se est� recibiendo una Rx Virtual generada por este laboratorio (IsLocalReceive=True) entonces el n�mero de trazo debe ser el de �ste laboratorio
                ' debido a que el trazo que se lee del AUGENSVR4 corresponde al �ltimo labortorio que sincroniz� la �rden, en este caso el Lab. de AR y ese n�mero
                ' no �st� relacionado con el trazo del laboratorio que gener� la Rx Virtual

                If ds.Tables("t1").Rows.Count > 0 Then
                    Dim Info As New RxResume

                    If IsLocalReceive Then
                        Dim TrazoLocal As Integer
                        Dim LDB As New SqlDB(My.Settings.LocalServer, My.Settings.DBUser, My.Settings.DBPassword, My.Settings.LocalDBName)

                        sqlstr = "Select number01 From ORDERHED WHERE ordernum=" & RxID
                        LDB.OpenConn()

                        TrazoLocal = Convert.ToInt32(LDB.ExecuteScalar(sqlstr))
                        LDB.CloseConn()

                        If TrazoLocal > 0 Then                            
                            ds.Tables(0).Rows(0).Item("RefDigital") = TrazoLocal
                            ds.Tables(0).AcceptChanges()
                        End If
                    End If

                    FillRx(ds, False)

                    If IsModifying Then
                        Dim x As Integer = ComboClients.SelectedValue
                        FillRxResume(Info)
                        Info.ShowDialog()
                        ShowNew = Info.Modify
                        ComboClients.SelectedValue = x
                        CheckGarantia.Enabled = False

                    Else
                        ShowNew = True
                    End If

                    If ShowNew Then
                        OrderExists = True
                        PanelInformacion.Location = New Point(6, 187)
                        PanelInformacion.Enabled = True
                        PanelOpciones.Enabled = True
                        PanelOpciones.Visible = True

                        ComboTrazos.Enabled = False
                        ComboArmazon2.Enabled = False
                        RadioDigitalNew.Enabled = False
                        RadioManualNew.Enabled = False
                        PanelPreviewTrazo.Visible = False

                        ' Dejar que se cambie el cliente en la modificacion                     
                        PanelRx.Enabled = True
                        Me.RxID.Enabled = False
                        RxWECO.Enabled = False
                        RxNum.Enabled = False

                        If ComboTrazos.ComboBox1.SelectedValue <> Nothing Then
                            RxWECO.Text = ComboTrazos.ComboBox1.SelectedValue
                        End If
                        RxWECO.Visible = True
                        Cancel.Enabled = True

                        If (IsManualRx Or (Val(ContornoPuente.Text) = 0)) And Not IsReceivingVirtualRx Then
                            PanelContorno.Enabled = True
                            ContornoA.Enabled = True
                            ContornoB.Enabled = True
                            ContornoED.Enabled = True
                        End If

                        RightRead.ReadOnly = False
                        LeftRead.ReadOnly = False
                        AdicionLeft_Leave(Me, New EventArgs)

                        If Not IsLocalReceive Then
                            ' Se habilita el combo de errores para que se seleccione un error
                            PanelErrores.Visible = True
                            ComboErrores.SelectedValue = My.Settings.RecapturaSP_ID

                            ' Si el laboratorio tiene estacion de QA, entonces la razon de modificacion sera capturada
                            If My.Settings.QAEnabled Then
                                ' Desplegamos la razon de la modificacion asi como el ojo que se modificara
                                If Existe_DefectoQA() Then
                                    PanelModInfo.Visible = True
                                    PanelErrores.Visible = False

                                    ' Se verifica que lados seran modificados
                                    If LBLDEFDER.Text = "" Then CheckREye.Checked = False
                                    If LBLDEFIZQ.Text = "" Then CheckLEye.Checked = False
                                    ShowLeftEye = CheckLEye.Checked
                                    ShowRightEye = CheckREye.Checked

                                    If CheckREye.Checked Then
                                        RightRead.Text = OriginalRightPart
                                        Enter_RightPart()
                                    End If
                                    If CheckLEye.Checked Then
                                        LeftRead.Text = OriginalLeftPart
                                        Enter_LeftPart()
                                    End If
                                Else
                                    Try : ComboErrores.SelectedValue = My.Settings.RecapturaSP_ID : Catch : End Try
                                    PanelErrores.Location = New System.Drawing.Point(200, 100)
                                    PanelErrores.Visible = True
                                    PanelErrores.BringToFront()

                                End If
                            Else
                                'si no tiene QA entonces se despliega toda la lista de errores
                            End If

                            AceptarDigitalNew.Visible = False
                            AceptarManualNew.Visible = False
                            CancelarDigitalNew.Visible = False

                        End If

                        If IsReceivingVirtualRx Or (IsVirtualRx) Then
                            LeftRead.Text = OriginalLeftPart
                            RightRead.Text = OriginalRightPart

                            If Not ShowLeftEye Then
                                CheckLEye.Checked = False
                                Enter_RightPart()
                            End If
                            If Not ShowRightEye Then
                                CheckREye.Checked = False
                                Enter_LeftPart()
                            End If
                        End If

                        PanelInformacion.Visible = True
                        ModifyingFromLocal = Local      ' Indica si se esta modificando una Rx desde el servidor local o no.
                    Else
                        InitValues()
                    End If
                Else
                    If Not RxExists(RxID) Then
                        MsgBox("La Rx no existe", MsgBoxStyle.Exclamation)
                    Else
                        If RxClosed(RxID) Then
                            MsgBox("La Rx ya est� cerrada", MsgBoxStyle.Exclamation)
                        Else
                            If RxRecibida(RxID) Then
                                MsgBox("Esta receta ya ha sido recibida.", MsgBoxStyle.Exclamation)

                            ElseIf RxSinLiberar(RxID) Then
                                MsgBox("Esta receta no ha sido liberada del Lab. Remoto" + vbCrLf + "o no se ha sincronizado con el servidor corporativo.", MsgBoxStyle.Exclamation)

                            Else
                                If Not Local Then
                                    If RxID.StartsWith(Me.LabID) Then
                                        MsgBox("Esta Rx aun no se le ha dado entrada al laboratorio.", MsgBoxStyle.Exclamation)

                                    Else
                                        MsgBox("Esta Rx no pertenece a tu laboratorio.", MsgBoxStyle.Exclamation)

                                    End If
                                Else
                                    MsgBox("Esta receta ya ha sido surtida.", MsgBoxStyle.Critical)
                                End If
                            End If

                        End If
                    End If

                    InitValues()
                End If
            Else
                PanelInformacion.Enabled = True
                PanelInformacion.Visible = True

            End If

        Catch ex As Exception
            MsgBox("Error al obtener datos de la Receta" & vbCrLf & ex.Message, MsgBoxStyle.Exclamation)
        Finally
            t.CloseConn()
        End Try
    End Sub
    Private Function RxClosed(ByVal ordernum As Integer) As Boolean
        Dim ReturnValue As Boolean = False
        Dim ds As New DataSet
        Dim t As New SqlDB

        t.SQLConn = New SqlConnection(Laboratorios.ConnStr)

        Try
            t.OpenConn()
            ds = t.SQLDS("SELECT openorder FROM orderhed WITH(NOLOCK) WHERE ordernum = " & ordernum, "t1")
            If (ds.Tables("t1").Rows(0).Item("openorder") = 1) Then
                ReturnValue = False
            Else
                ReturnValue = True
            End If
        Catch ex As Exception
            ReturnValue = True
        Finally
            t.CloseConn()
        End Try
        Return ReturnValue
    End Function
    Private Function RxSinLiberar(ByVal ordernum As Integer) As Boolean
        Dim ReturnValue As Boolean = False
        Dim ds As New DataSet
        Dim t As SqlDB = Nothing

        'Pedro Far�as Lozano Dic 10, 2012
        'Se debe buscar en el AUGENSVR4 servidor corporativo porque es donde se encuentra la orden que ya fu� actualizada
        'el Lab. remoto(A�os sin arreglar esto!)
        Try

            If (My.Settings.VantageServer.Length >= 8) And Ping(My.Settings.VantageServer) Then
                t = New SqlDB(My.Settings.VantageServer, My.Settings.DBUser, My.Settings.DBPassword, My.Settings.MfgSysDBName)
            Else
                t = New SqlDB(My.Settings.LocalServer, My.Settings.DBUser, My.Settings.DBPassword, My.Settings.LocalDBName)
            End If


            't.SQLConn = New SqlConnection(Laboratorios.ConnStr)


            'Pedro Far�as Lozano
            ' Oct 16 2012
            ' Se agreg� condici�n para revisar si tiene AR, lo cual le da relevancia a si ha sido liberada o no por el lab. remoto
            t.OpenConn()
            ds = t.SQLDS("SELECT checkbox11,shortchar06 FROM orderhed WITH(NOLOCK) WHERE ordernum = " & ordernum, "t1")

            ' Hay que agregar esta condicion para que puedan modificar en el lab. remoto
            ' cuando ya bajaron la receta y esta todavia no se ha sincronizado
            ' And ordernum.ToString.Substring(0, 2) = LabID()

            If (ds.Tables("t1").Rows(0).Item("checkbox11") = 0 And ds.Tables("t1").Rows(0).Item("shortchar06") > 0) Then
                ReturnValue = True
            Else
                ReturnValue = False
            End If
        Catch ex As Exception
            ReturnValue = True
        Finally
            t.CloseConn()
        End Try
        Return ReturnValue
    End Function
    Private Function RxRecibida(ByVal ordernum As Integer) As Boolean
        Dim ReturnValue As Boolean = False
        Dim ds As New DataSet
        Dim t As New SqlDB

        t.SQLConn = New SqlConnection(Laboratorios.ConnStr)

        Try
            t.OpenConn()
            ds = t.SQLDS("SELECT checkbox12 FROM orderhed WITH(NOLOCK) WHERE ordernum = " & ordernum, "t1")
            If (ds.Tables("t1").Rows(0).Item("checkbox12") = 1) Then
                ReturnValue = True
            Else
                ReturnValue = False
            End If
        Catch ex As Exception
            ReturnValue = True
        Finally
            t.CloseConn()
        End Try
        Return ReturnValue
    End Function

    Private Function GetCadena(ByVal custnum As Integer) As Integer
        Dim ReturnValue As Integer = 0
        Dim ds As New DataSet
        Dim t As New SqlDB

        t.SQLConn = New SqlConnection(Laboratorios.ConnStr)

        Try
            t.OpenConn()
            ds = t.SQLDS("SELECT number06 FROM customer WITH(NOLOCK) WHERE custnum = " & custnum, "t1")
            If (ds.Tables("t1").Rows.Count > 0) Then
                ReturnValue = ds.Tables("t1").Rows(0).Item("number06")
            End If
        Catch ex As Exception
        Finally
            t.CloseConn()
        End Try
        Return ReturnValue
    End Function
    Private Function RxExists(ByVal ordernum As Integer) As Boolean
        Dim ReturnValue As Boolean = False
        Dim ds As New DataSet
        Dim t As New SqlDB

        t.SQLConn = New SqlConnection(Laboratorios.ConnStr)

        Try
            t.OpenConn()
            ds = t.SQLDS("SELECT ordernum FROM orderhed WITH(NOLOCK) WHERE ordernum = " & ordernum, "t1")
            If (ds.Tables("t1").Rows.Count > 0) Then
                ReturnValue = True
            End If
        Catch ex As Exception
        Finally
            t.CloseConn()
        End Try
        Return ReturnValue
    End Function
    Private Function ExistsForWeb(ByVal OrdenLab As String) As Boolean
        Dim Exists As Boolean = False
        Dim i As Integer = 0

        For i = 0 To WO.DGCostco.RowCount - 1
            If WO.DGCostco.Item("OrdenLab", i).Value = OrdenLab Then
                Exists = True
                Exit For
            End If
        Next

        Return Exists
    End Function

    Private Sub AceptarDigital_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles AceptarDigitalNew.Click
        Dim ShowNew As Boolean = False
        Dim ContinuarRx As Boolean = True
        Dim custnum As Integer = ComboClients.SelectedValue

        If ComboTrazos.ComboBox1.Items.Count > 0 Then
            If GetCadena(custnum) = My.Settings.CadenaLiverpoolID And Not IsModifying And RxType = RxTypes.NormalRx And Not IsVirtualRx Then
                'If (ComboClients.Text.Contains("LIVERPOOL") Or ComboClients.Text.Contains("FAB FRANCIA")) And Not IsModifying And RxType = RxTypes.NormalRx And Not IsVirtualRx Then
                If Not ValidateLiverpoolPO(RxID.Text) Then
                    ContinuarRx = False
                    MsgBox("El ID de Liverpool es incorrecto, favor de revisarlo", MsgBoxStyle.Exclamation)
                End If
            ElseIf GetCadena(custnum) = My.Settings.CadenaCostcoID And Not IsModifying And Not IsLocalReceive And Not IsVirtualRx Then
                'ElseIf ComboClients.Text.Contains("COSTCO") And Not IsModifying And Not IsLocalReceive And Not IsVirtualRx Then
                If RxType <> RxTypes.WebRx Then
                    If CheckGarantia.Checked Then
                        If ValidatePO(RxID.Text) Then
                            ContinuarRx = True
                        Else
                            MsgBox("Receta inv�lida, debe introducir el PO de COSTCO en No Rx.", MsgBoxStyle.Critical)
                            ContinuarRx = False
                        End If
                    Else
                        MsgBox("Las Rx de Costco deben entrar por internet, ya no pueden ser manuales", MsgBoxStyle.Exclamation)
                        ContinuarRx = False
                    End If

                Else
                    If Not ValidatePO(RxID.Text) Then
                        MsgBox("Receta inv�lida, debe introducir el PO de COSTCO en No Rx.", MsgBoxStyle.Critical)
                        ContinuarRx = False

                    ElseIf ExistsForWeb(RxID.Text) And RxType <> RxTypes.WebRx Then
                        MsgBox("Esta Rx se encuentra en las Ordenes de Internet." + vbCrLf + "Favor de capturarla desde esa ventana.", MsgBoxStyle.Exclamation)
                        ContinuarRx = False
                    End If
                End If
            Else
                If Len(RxID.Text) > My.Settings.RxLength And RxType = RxTypes.NormalRx And Not IsVirtualRx And Not IsModifying Then
                    ContinuarRx = False
                    MsgBox("Las Rx no pueden ser de mas de " & My.Settings.RxLength & " d�gitos", MsgBoxStyle.Critical)
                End If
            End If

            If Me.ComboArmazon2.Texto = "" And ContinuarRx Then
                ContinuarRx = False
                MsgBox("No se ha capturado el Tipo de Armaz�n." + vbCrLf + "Favor de seleccionar el tipo correspondiente.", MsgBoxStyle.Critical)
            End If

            If ContinuarRx Then
                Dim ds As New DataSet
                Dim t As SqlDB

                If WorkingOnLine Then
                    t = New SqlDB(ChooseServer, My.Settings.DBUser, My.Settings.DBPassword, DataBase)
                Else
                    t = New SqlDB(My.Settings.LocalServer, My.Settings.DBUser, My.Settings.DBPassword, My.Settings.LocalDBName)
                End If
                If Not Me.ChangingTraces Then

                    ' Checo si existe ya la receta para el mismo doctor el mismo dia
                    ' ---------------------------------------------------------
                    Try
                        t.OpenConn()

                        ds = t.SQLDS("SELECT ordernum FROM orderhed WITH(NOLOCK) WHERE shortchar01 = '" & RxID.Text & "' and orderdate = '" & Now.Date & "' and custnum = " & ComboClients.SelectedValue, "t1")
                        If (ds.Tables("t1").Rows.Count > 0) And (Not IsModifying) Then
                            MsgBox("Esta receta ya ha sido registrada para este cliente en el sistema.", MsgBoxStyle.Information)
                            Return
                        End If

                    Catch ex As Exception
                    Finally
                        t.CloseConn()
                    End Try
                End If
                If ChangingTraces Then
                    RxNumLocal = ComboTrazos.Texto
                End If


                If IsModifying Or IsLocalReceive Then
                    AceptarModificar(RxID.Text)
                    IsManualRx = False
                    RxWECO.Text = ComboTrazos.Texto
                    PanelPreviewTrazo.Visible = False
                    AceptarDigitalNew.Visible = False
                    CancelarDigitalNew.Visible = False
                Else
                    IsManualRx = False
                    'Ping()
                    'If Ping(Read_Registry("ServerAdd")) Then
                    '    btnverifica_Click(sender, e)
                    'End If
                    If (RxType <> RxTypes.WebRx) And IsModifying Then
                        If WorkingOnLine Then
                            t = New SqlDB(ChooseServer, My.Settings.DBUser, My.Settings.DBPassword, DataBase)
                        Else
                            t = New SqlDB(My.Settings.LocalServer, My.Settings.DBUser, My.Settings.DBPassword, My.Settings.LocalDBName)
                        End If
                        Try
                            t.OpenConn()
                            ' Dim ds As New DataSet
                            ds = t.SQLDS("select Armazon as contorno,* from VwRecetasTRECEUX where RefDigital = " & ComboTrazos.ComboBox1.SelectedValue & " and (plant = " & LabID() & " or plant2 = " & LabID() & ")", "t1")
                            'ds = t.SQLDS("select * from VwRecetasTRECEUX where RefDigital = " & Trazos.SelectedValue & "and plant = " & LabID(), "t1")
                            If ds.Tables("t1").Rows.Count > 0 Then
                                Dim Info As New RxResume
                                Dim a, b, ed, puente As Single
                                ' Aqui respaldo las variables del contorno digital antes que se sobreescriban con las que trae ya la receta
                                a = ContornoA.Text
                                b = ContornoB.Text
                                ed = ContornoED.Text
                                puente = ContornoPuente.Text
                                FillRx(ds, False)
                                ' Restauro los valores del trazo para que no aparezcan en 0
                                ContornoA.Text = a
                                ContornoB.Text = b
                                ContornoED.Text = ed
                                ContornoPuente.Text = puente

                                FillRxResume(Info)
                                Info.ShowDialog()
                                ShowNew = Info.Modify
                                If ShowNew Then
                                    OrderExists = True
                                    RxNum.Text = Info.RxNum.Text
                                    PanelErrores.Visible = True
                                ElseIf RxType = RxTypes.NormalRx Then
                                    ClearValues()
                                End If
                            Else
                                If RxType = RxTypes.NormalRx Then
                                    ClearValues()
                                End If
                                ShowNew = True
                            End If
                        Catch ex As Exception
                            MsgBox(ex.Message)
                        Finally
                            t.CloseConn()
                        End Try
                    Else
                        ShowNew = True
                    End If

                    Dim cancelagarantia As Boolean = False

                    If CheckGarantia.Checked And Not IsModifying And Not Me.IsReceivingVirtualRx Then
                        Try
                            Dim lt = New SqlDB
                            lt.SQLConn = New SqlConnection(Laboratorios.ConnStr)
                            lt.OpenConn()
                            ds = lt.SQLDS("select checkbox03 as IsCostco from customer with(nolock) where company = 'TRECEUX' and custnum = " & Me.ComboClients.SelectedValue, "t1")

                            If ds.Tables("t1").Rows(0).Item("IsCostco") = 1 Then
                                Dim sqlstr As String = "select Fecha, Custnum, LabID, OrdenLab,A, B, ED, Puente, Antireflejante, MaterialID, DisenoID as Dise�oID, Tinte, EsferaR, CilindroR, EjeR, AdicionR," & _
                                   " EsferaL, CilindroL, EjeL, AdicionL, Foco, AlturaR, MonoR, AlturaL, MonoL, DIPce, DIPle, Altura, NumArmazon, Contorno, " & _
                                   " CapturaArmazon, Biselado, Gradiente, Status, Name, Abrillantado, ComentariosLab, CostcoLente, CostcoAR, IsGratis, CostcoPulido, CostcoTinte, CustID, orderdate, seqid, material, diseno, FechaEntradaWeb, FechaEntradaFisica " & _
                                   " from vwprocessedweborders with(nolock) where ordenlab = '" & RxID.Text & "'"
                                ds = lt.SQLDS(sqlstr, "t1")

                                If ds.Tables("t1").Rows.Count > 0 Then
                                    RxType = RxTypes.WebRx
                                    FillRx(ds, True)
                                    RxNum.Text = ""

                                Else
                                    MsgBox("No se encontro informacion de la Rx Original.", MsgBoxStyle.Exclamation)
                                    cancelagarantia = True
                                End If

                            End If
                        Catch ex As Exception
                            cancelagarantia = True
                            If ex.Message.Contains("inv�lido") Then
                                MsgBox(ex.Message, MsgBoxStyle.Critical)
                            End If
                        Finally
                            t.CloseConn()

                        End Try
                        If Not cancelagarantia And CheckGarantia.Checked Then
                            IsGarantia = True
                        Else
                            ShowNew = False
                            Exit Sub
                        End If
                    End If

                    Dim CustomerOK As Boolean = True
                    If Not IsModifying Then
                        Dim tt As SqlDB
                        tt = New SqlDB(My.Settings.LocalServer, My.Settings.DBUser, My.Settings.DBPassword, My.Settings.LocalDBName)

                        Try
                            tt.OpenConn()
                            Dim dst As New DataSet
                            Dim sqlstr As String = "SELECT     customer.company, customer.custid, customer.custnum, customer.name, terms.termscode, terms.description, terms.duedays, customer.credithold " & _
                                "FROM         customer WITH (nolock) INNER JOIN " & _
                                " terms WITH (nolock) ON customer.company = terms.company AND customer.termscode = terms.termscode " & _
                                " WHERE     (customer.company = 'TRECEUX') and custnum = " & ComboClients.SelectedValue.ToString
                            dst = tt.SQLDS(sqlstr, "t1")

                            If dst.Tables("t1").Rows.Count > 0 Then
                                With (dst.Tables("t1").Rows(0))
                                    If .Item("credithold") = 1 Then
                                        CustomerOK = False
                                        MsgBox("El Cliente " + .Item("name") + " tiene t�rminos de pago a " + .Item("description") + "." + vbCrLf + "Algunas Rx han excedido este tiempo por lo cual no se permite ingresar m�s." + vbCrLf + "Favor de consultar con el coordinador su estado de cuenta.", MsgBoxStyle.Critical)
                                    Else
                                        CustomerOK = True
                                    End If
                                End With

                            End If
                        Catch ex As Exception

                        Finally
                            tt.CloseConn()
                        End Try
                    End If
                    If CustomerOK Then
                        ShowNew = True
                    Else
                        ShowNew = False
                    End If

                    If ShowNew Then
                        If Val(RxID.Text) > 0 Then
                            PanelPreviewTrazo.Visible = False
                            'especificamos que la nueva recera no es del guiador virtual
                            RxVirtual = False
                            PanelInformacion.Location = New Point(6, 187)
                            PanelInformacion.Enabled = True
                            If Not IsModifying Then
                                PanelArmazon.Enabled = False
                                PanelRx.Enabled = False
                            End If
                            PanelOpciones.Enabled = True
                            PanelOpciones.Visible = True
                            PanelRx.Enabled = False

                            RxWECO.Text = ComboTrazos.ComboBox1.SelectedValue

                            If (My.Settings.Plant = "VBENS") Then
                                RxWECO.Text = Microsoft.VisualBasic.Right(RxID.Text, 4)
                                RxWECO.Text = RxWECO.Text + "00"
                            End If

                            RxWECO.Visible = True
                            Cancel.Enabled = True
                            PanelContorno.Enabled = True
                            ContornoA.Enabled = False
                            ContornoB.Enabled = False
                            ContornoED.Enabled = False

                            ' No desplegar los valores de A/B/ED para no distraer
                            ContornoA.Text = ""
                            ContornoB.Text = ""
                            ContornoED.Text = ""
                            ' ---------------------------------

                            'End If
                            AceptarManualNew.Visible = False
                            AceptarDigitalNew.Visible = False
                            CancelarDigitalNew.Visible = False
                            CheckGarantia.Enabled = False           ' Se deshabilitan las garantias para que no haya problemas con PO de costco repetidos en el sistema
                            If CheckGarantia.Checked Then
                                IsGarantia = True
                            Else
                                IsGarantia = False
                            End If
                            ComboTrazos.Enabled = False
                            ComboArmazon2.Enabled = False
                            PanelInformacion.Visible = True
                            LeftBarcodeRead = False
                            RightBarcodeRead = False

                            If PanelInformacion.Visible Then
                                AceptarManualNew.Visible = False
                                AceptarDigitalNew.Visible = False
                                CancelarDigitalNew.Visible = False

                            End If

                            EsferaRight.Focus()
                            EsferaRight.Select()
                        Else
                            MsgBox("La orden de laboratorio es inv�lida.", MsgBoxStyle.Exclamation)
                        End If
                    End If
                End If
            End If
        End If
    End Sub
    Function GetCustomValues(ByVal CustomValue As CustomValues, ByVal ValueID As String) As String
        Dim Value As String = ""
        Dim t As New SqlDB(My.Settings.LocalServer, My.Settings.DBUser, My.Settings.DBPassword, My.Settings.LocalDBName)
        Try
            t.OpenConn()
            Dim ds As New DataSet()
            Select Case CustomValue
                Case CustomValues.Clase
                    ds = t.SQLDS("SELECT Clase FROM VwPartTRECEUX WHERE (VwPartTRECEUX.partnum = '" & ValueID & "')", "t1")

                Case CustomValues.Dise�o
                    ds = t.SQLDS("SELECT TblDesigns.diseno FROM TblDesigns with(nolock) INNER JOIN VwPartTRECEUX with(nolock) ON TblDesigns.DisenoID = VwPartTRECEUX.Dise�o WHERE (VwPartTRECEUX.partnum = '" & ValueID & "')", "t1")
                Case CustomValues.OMADis_ID
                    ds = t.SQLDS("SELECT TblDesigns.OMADesign FROM TblDesigns with(nolock) INNER JOIN VwPartTRECEUX with(nolock) ON TblDesigns.DisenoID = VwPartTRECEUX.Dise�o WHERE (VwPartTRECEUX.partnum = '" & ValueID & "')", "t1")
                Case CustomValues.Dise�oID
                    ds = t.SQLDS("SELECT TblDesigns.cl_diseno FROM TblDesigns with(nolock) INNER JOIN VwPartTRECEUX with(nolock) ON TblDesigns.DisenoID = VwPartTRECEUX.Dise�o WHERE (VwPartTRECEUX.partnum = '" & ValueID & "')", "t1")
                Case CustomValues.Material
                    ds = t.SQLDS("SELECT TblMaterials.material FROM VwPartTRECEUX with(nolock) INNER JOIN TblMaterials with(nolock) ON VwPartTRECEUX.Material = TblMaterials.MaterialID WHERE (VwPartTRECEUX.partnum = '" & ValueID & "')", "t1")
                Case CustomValues.Base
                    ds = t.SQLDS("SELECT VwPartTRECEUX.Base FROM VwPartTRECEUX with(nolock) INNER JOIN TblMaterials with(nolock) ON VwPartTRECEUX.Material = TblMaterials.MaterialID WHERE (VwPartTRECEUX.partnum = '" & ValueID & "' AND clase = 'S')", "t1")
                Case CustomValues.MaterialID
                    ds = t.SQLDS("SELECT TblMaterials.cl_mat FROM VwPartTRECEUX with(nolock) INNER JOIN TblMaterials with(nolock) ON VwPartTRECEUX.Material = TblMaterials.MaterialID WHERE (VwPartTRECEUX.partnum = '" & ValueID & "')", "t1")
                Case CustomValues.Lado
                    ds = t.SQLDS("SELECT lado FROM VwPartTRECEUX with(nolock) WHERE (VwPartTRECEUX.partnum = '" & ValueID & "')", "t1")
                Case CustomValues.AR
                    ds = t.SQLDS("SELECT AR FROM VwPartTRECEUX with(nolock) WHERE (VwPartTRECEUX.partnum = '" & ValueID & "')", "t1")
                Case CustomValues.ProdCode
                    ds = t.SQLDS("SELECT prodcode FROM VwPartTRECEUX with(nolock) WHERE (VwPartTRECEUX.partnum = '" & ValueID & "')", "t1")
                Case CustomValues.OMAMat_ID

                    ' Buscamos el equivalente del material en OMA de la pastilla seleccionada junto con si lleva AR o no
                    Dim LlevaAR As Integer = 0
                    If CheckAR.Checked Or CheckARGold.Checked Or CheckARMatiz.Checked Then LlevaAR = 1
                    ds = t.SQLDS("SELECT TBLOMAMATERIALS.OMA_IDMat FROM VwPartTRECEUX INNER JOIN TBLOMAMATERIALS ON TBLOMAMATERIALS.CVE_MAT =  VwPartTRECEUX.MATERIAL where len(material) <3 and VwPartTRECEUX.partnum = '" & ValueID & "' and TBLOMAMATERIALS.AR  = " & LlevaAR & " ", "t1")
                Case CustomValues.OMASeg_Width
                    ds = t.SQLDS("SELECT VwPartTRECEUX.partdescription FROM VwPartTRECEUX with(nolock) WHERE (VwPartTRECEUX.partnum = '" & ValueID & "')", "t1")
            End Select

            If ds.Tables("t1").Rows.Count > 0 Then
                Value = ds.Tables("t1").Rows(0).Item(0)
                If CustomValue = CustomValues.Dise�o Then
                    If Value = "TRINITY" Then
                        ds = t.SQLDS("select partdescription from part with(nolock) where partnum = '" & ValueID & "' and company = 'TRECEUX'", "t1")
                        Dim x As String = ds.Tables("t1").Rows(0).Item("partdescription")
                        If x.Contains("DOBLE") Then
                            Value &= " DBL"
                        Else
                            Value &= " ASF"
                        End If
                    End If
                End If
            Else
                Value = "ERROR"
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        Finally
            t.CloseConn()
        End Try
        Return Value
    End Function
    Function CheckContorno() As Boolean
        If ContornoA.Text = "0" And ContornoB.Text = "0" And ContornoED.Text = "0" And ContornoPuente.Text = "0" Then
            Return False
        Else
            Return True
        End If
    End Function
    Function GetCustomValues(ByVal CustomValue As CustomValues, ByVal Partnum As System.UInt64) As String
        Dim Value As String = ""
        Dim t As New SqlDB(My.Settings.LocalServer, My.Settings.DBUser, My.Settings.DBPassword, My.Settings.LocalDBName)
        Try
            t.OpenConn()
            Dim ds As New DataSet()
            Select Case CustomValue
                Case CustomValues.Clase
                    ds = t.SQLDS("select Clase from VwPartTRECEUX where partnum = '" & Partnum & "'", "t1")
                Case CustomValues.Dise�o
                    ds = t.SQLDS("select diseno from TblDesigns where cl_diseno = " & Partnum, "t1")
                Case CustomValues.Material
                    ds = t.SQLDS("select material from TblMaterials where cl_mat = " & Partnum, "t1")
            End Select
            If ds.Tables("t1").Rows.Count > 0 Then
                Value = ds.Tables("t1").Rows(0).Item(0)
                If CustomValue = CustomValues.Clase Then
                    If Value = "T" Then
                        Value = "Terminado"
                    Else
                        Value = "Semiterminado"
                    End If
                End If
            Else
                Value = "ERROR"
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        Finally
            t.CloseConn()
        End Try
        Return Value
    End Function

    Private Sub Cancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Cancel.Click, CancelarNew.Click, UserControl11.Click
        InitValues()
        'RxID_Click(Me, e)
    End Sub

    Sub MuestraCalculosSinArmazon()
        Dim rx As New Calcot.RxPrintInfo()
        Dim value As New ItemWithValue
        Dim lpart As String = ""
        Dim rpart As String = ""
        rx.AdicionL = AdicionLeft.Text
        rx.AdicionR = AdicionRight.Text
        rx.Cliente = ComboClients.Text
        rx.DipceDiple = DIPLejos.Text & "-" & DIPCerca.Text
        'GetMaterialDesign(MaterialID, DesignID)
        rx.CheckedEyes = Calcot.RxPrintInfo.Eyes.None
        If ContornoPuente.Text < 1 Then
            rx.Puente = CDbl(ContornoPuente.Text) * 100
        Else
            rx.Puente = CDbl(ContornoPuente.Text)
        End If
        If CheckLEye.Checked Then
            value = ListLeftParts.SelectedItem
            BaseLStr = GetCustomValues(CustomValues.Base, value.Value.ToString)
            lpart = value.Value.ToString
            If BaseLStr = "ERROR" Then BaseLStr = ""
            rx.CheckedEyes += Calcot.RxPrintInfo.Eyes.Left
            'Mod By Marco Angulo 3/July/2006
            'Verificamos si el numero de parte es Terminado, ya que si es terminado y no tiene armazon no se graba en guiador
            If GetCustomValues(CustomValues.Clase, value.Value.ToString) = "T" Then rx.LeftFinished = True
        End If
        If CheckREye.Checked Then
            value = ListRightParts.SelectedItem
            rpart = value.Value.ToString
            BaseRStr = GetCustomValues(CustomValues.Base, value.Value.ToString)
            If BaseRStr = "ERROR" Then BaseRStr = ""
            rx.CheckedEyes += Calcot.RxPrintInfo.Eyes.Right
            'Mod By Marco Angulo 3/July/2006
            'Verificamos si el numero de parte es Terminado, ya que si es terminado y no tiene armazon no se graba en guiador
            If GetCustomValues(CustomValues.Clase, value.Value.ToString) = "T" Then rx.RightFinished = True
        End If
        If value.Value <> "" Then
            rx.Dise�o = GetCustomValues(CustomValues.Dise�o, value.Value.ToString)
            rx.Material = GetCustomValues(CustomValues.Material, value.Value.ToString)
        End If

        If RadioGradiente.Checked Then
            rx.Gradiente = "GRADIENTE"
        End If
        If CheckAR.Checked Then
            rx.AR = "A/R"
        ElseIf CheckARGold.Checked Then
            rx.AR = "A/R GOLD"
        ElseIf CheckARMatiz.Checked Then
            rx.AR = "A/R MATIZ"
        End If
        If CheckBiselado.Checked Then
            rx.Biselado = "BISELADO"
        End If
        If CheckAbril.Checked Then
            rx.Abrillantado = "ABRILLANTADO"
        End If

        ' BANDERA_OPTIMIZAR

        'If CheckOptim.Checked Then
        If CheckOptim.Checked And My.Settings.Optimizar Then
            rx.Optimizado = "OPTIMIZADO"
        End If

        rx.ModificacionRx = OrderExists
        If ComboTinte.SelectedValue <> 0 Then
            If CheckLevel1.Checked Then
                rx.TinteNum = "1"
            Else
                If CheckLevel2.Checked Then
                    rx.TinteNum = "2"
                Else
                    rx.TinteNum = "3"
                End If
            End If
            rx.TinteColor = ComboTinte.Texto
        End If
        rx.SinArmazon = True
        rx.RxFromVirtual = Me.RxVirtual
        rx.Rxident = RxID.Text
        'rx.RxNumFromVirtual = RxID.Text
        Dim gen As New GenerarRx(0, RxWECO.Text, 0, False, rx, ChangingTraces, OMA)
        gen.radiosR = Me.RadiosR
        gen.radiosL = Me.RadiosL


        'Dim ca As New Calcot.calculoSA(LD, LI)
        'ca.Confirmar = False
        'ca.Receta = RxWECO.Text

        'If ((CheckREye.Checked) And (CheckLEye.Checked) And (lpart = My.Settings.LenteSinCalculos) And (rpart = My.Settings.LenteSinCalculos)) Then
        'Or (CheckREye.Checked And (rpart = My.Settings.LenteSinCalculos)) _
        'Or (CheckLEye.Checked And (lpart = My.Settings.LenteSinCalculos)) Then

        If CheckREye.Checked And CheckLEye.Checked Then
            If rpart = My.Settings.LenteSinCalculos And lpart = My.Settings.LenteSinCalculos Then
                RxNoCalculos = True
            Else
                ' ''ca.GetCalculoSA()
                'ca.GetCalculoSA(My.Settings.Optimizar)
                Optisur.GetCalculos(NewCalcot.CalculationOption.CalculateAndSave)

            End If
        Else
            If CheckREye.Checked Then
                If rpart = My.Settings.LenteSinCalculos Then
                    RxNoCalculos = True
                Else
                    ' ''ca.GetCalculoSA()
                    'ca.GetCalculoSA(My.Settings.Optimizar)
                    Optisur.GetCalculos(NewCalcot.CalculationOption.CalculateAndSave)
                End If
            Else
                If CheckLEye.Checked Then
                    If lpart = My.Settings.LenteSinCalculos Then
                        RxNoCalculos = True
                    Else
                        ' ''ca.GetCalculoSA()
                        'ca.GetCalculoSA(My.Settings.Optimizar)
                        Optisur.GetCalculos(NewCalcot.CalculationOption.CalculateAndSave)
                    End If
                Else
                    'error del sistema la receta debe de contener un ojo
                End If
            End If
        End If

        gen.IsReceivingVirtualRx = IsReceivingVirtualRx
        gen.puente = rx.Puente


        'If ca.GetCveMensaje = 0 Then
        If Optisur.ErrorCode = 0 Then
            'gen.LD = LD
            'gen.LI = LI
            'gen.MoldeD = ca.GetMoldeDer
            'gen.MoldeI = ca.GetMoldeIzq

            'gen.FormaD = New Calcot.ReturnLadoDer
            'gen.FormaI = New Calcot.ReturnLadoIzq
            gen.BaseL = BaseLStr
            gen.BaseR = BaseRStr
            'le mandamos la fecha final si es una modificacion
            If IsModifying Then
                gen.FechaInicial = Me.FechaInicial
                gen.FechaEntrada = Me.FechaEntrada
            Else
                If IsReceivingVirtualRx Then
                    gen.FechaInicial = Me.FechaInicial
                    gen.FechaEntrada = Me.FechaEntrada
                Else
                    gen.FechaInicial = ""
                    gen.FechaEntrada = ""
                End If

            End If
            gen.Optisur = Optisur
            gen.IsManualFrame = IsManualRx
            gen.ShowDialog()

            '----------- ASIGNAMOS EL OBJETO DE IMPRESION EN FORM1
            Me.PrintObj = New Impresion
            Me.PrintObj = gen.PrintObj
            '------------------------------------------------------
            GrosorCentralD = gen.GrosCentralD
            GrosorCentralI = gen.GrosCentralI
            If gen.SuccessfulTran Then
                Me.RxSave_Click(Me, New EventArgs)
            End If
        Else
            'los calculos no son validos y lanzamos una exepcion
            'Throw New Exception(ca.GetMensaje)
            Throw New Exception(Optisur.ErrorCode & " - " & Optisur.ErrorDescription)
        End If
    End Sub

    Sub GetOMALabelsFromParts(ByVal partnum As String, ByRef LMATID As String, ByRef LMATNAME As String, ByRef LMATTYPE As String, ByRef LNAM As String, ByRef LTYP As String, ByRef FRNT As String)
        Dim Failed As Boolean = False
        Dim FailedMessage As String = ""
        Dim t As New SqlDB()
        t.SQLConn = New SqlConnection(Laboratorios.ConnStr)
        Try
            t.OpenConn()
            Dim ds As DataSet = t.SQLDS("select * from VwPartOMA with(nolock) where partnum = '" & partnum & "'", "t1")
            If ds.Tables("t1").Rows.Count = 0 Then
                Failed = True
                FailedMessage = "No se encontr� informacion de la pastilla [" & partnum & "]"
            Else
                With ds.Tables("t1").Rows(0)
                    LMATID = .Item("LMATID")
                    LMATNAME = .Item("LMATNAME")
                    LMATTYPE = .Item("LMATTYPE")
                    LNAM = .Item("LNAM")
                    LTYP = .Item("LTYP")
                    If .Item("Clase") = "S" Then FRNT = .Item("FRNT") Else FRNT = -1

                End With
            End If


        Catch ex As Exception
            Failed = True
            FailedMessage = ex.Message
        Finally
            t.CloseConn()
            If Failed Then
                Throw New Exception(FailedMessage)
                'MsgBox(FailedMessage, MsgBoxStyle.Exclamation)
            End If

        End Try
    End Sub
    Function GetOMAFrameType(ByVal FrameType As Integer) As Integer
        Dim Failed As Boolean = False
        Dim FailedMessage As String = ""
        Dim Value As Integer = 0
        Dim t As New SqlDB()
        t.SQLConn = New SqlConnection(Laboratorios.ConnStr)
        Try
            t.OpenConn()
            Dim ds As DataSet = t.SQLDS("select OMA_FTYP from TblFrameTypes with(nolock) where FrameType = " & FrameType, "t1")
            If ds.Tables("t1").Rows.Count = 0 Then
                Failed = True
                FailedMessage = "No se encontr� informaci�n del Tipo de Armazon solicitado"
            Else
                Value = ds.Tables("t1").Rows(0).Item("OMA_FTYP")
            End If
        Catch ex As Exception
            Failed = True
            FailedMessage = ex.Message
        Finally
            t.CloseConn()
            If Failed Then
                Throw New Exception("Error de Tipo de Armazon OMA" & vbCrLf & FailedMessage)
                'MsgBox(FailedMessage, MsgBoxStyle.Exclamation)
            End If
        End Try
        Return Value
    End Function

    Public Function GetBaseCurveFromSphereandCylinder(ByVal cl_mat As String, ByVal cl_diseno As String, ByVal esfera As String, ByVal cilindro As String) As String
        Dim Failed As Boolean = False
        Dim FailedMessage As String = ""
        Dim Value As String = "0"

        Dim t As New SqlDB()
        t.SQLConn = New SqlConnection(Laboratorios.ConnStr)
        Try
            t.OpenConn()
            Dim sqlstr As String = "select * from VwEsfCilBase with(nolock) where cl_mat = " & cl_mat & " and cl_diseno = " & cl_diseno & " and esfera = " & esfera & " and cilindro = " & cilindro
            sqlstr = "select base from tblbasegrosor with(nolock) where cl_mat = " & cl_mat & " and cl_diseno = " & cl_diseno & " and (" & esfera & " between esfera_fin and esfera)"
            'sqlstr = "SELECT     a.base FROM         TblBaseGrosor AS a WITH (nolock) INNER JOIN TblMaterials AS b WITH (nolock) ON a.cl_mat = b.cl_mat INNER JOIN TblDesigns AS c WITH (nolock) ON a.cl_diseno = c.cl_diseno INNER JOIN VwPartTRECEUX AS d WITH (nolock) ON b.MaterialID = d.Material AND c.DisenoID = d.Dise�o AND a.esfera = d.Base WHERE d.partnum = '" & partnum & "'"
            Dim ds As DataSet = t.SQLDS(sqlstr, "t1")
            If ds.Tables("t1").Rows.Count = 0 Then
                Failed = True
                FailedMessage = "No se encontr� informaci�n de Curva Base para el lente terminado!"
            Else
                With ds.Tables("t1").Rows(0)
                    Value = .Item("Base").ToString
                End With
            End If

        Catch ex As Exception
            Failed = True
            FailedMessage = ex.Message
        Finally
            t.CloseConn()
            If Failed Then
                Throw New Exception(FailedMessage)
                'MsgBox(FailedMessage, MsgBoxStyle.Exclamation)
            End If
        End Try
        Return Value
    End Function
    Public Function GetBaseCurveFromSphereandCylinder(ByVal Partnum As String) As String
        Dim Failed As Boolean = False
        Dim FailedMessage As String = ""
        Dim Value As String = "0"

        Dim t As New SqlDB()
        t.SQLConn = New SqlConnection(Laboratorios.ConnStr)
        Try
            t.OpenConn()
            Dim sqlstr As String = ""
            sqlstr = "SELECT     a.base FROM         TblBaseGrosor AS a WITH (nolock) INNER JOIN TblMaterials AS b WITH (nolock) ON a.cl_mat = b.cl_mat INNER JOIN TblDesigns AS c WITH (nolock) ON a.cl_diseno = c.cl_diseno INNER JOIN VwPartTRECEUX AS d WITH (nolock) ON b.MaterialID = d.Material AND c.DisenoID = d.Dise�o AND a.esfera = d.Base WHERE d.partnum = '" & Partnum & "'"
            Dim ds As DataSet = t.SQLDS(sqlstr, "t1")
            If ds.Tables("t1").Rows.Count = 0 Then
                Failed = True
                FailedMessage = "No se encontr� informaci�n de Curva Base para el lente terminado!"
            Else
                With ds.Tables("t1").Rows(0)
                    Value = .Item("Base").ToString
                End With
            End If

        Catch ex As Exception
            Failed = True
            FailedMessage = ex.Message
        Finally
            t.CloseConn()
            If Failed Then
                Throw New Exception(FailedMessage)
                'MsgBox(FailedMessage, MsgBoxStyle.Exclamation)
            End If
        End Try
        Return Value
    End Function

    'Private Function UseFreeFormCredit()
    '    Dim SpendFFCredit As Boolean = False

    '    ' Si el numero de vantage(ordernum) es igual a cero quiere decir que es una orden nueva
    '    If (RxNum.Text = "" And IsVirtualRx And ARLab = LabID()) Then
    '        SpendFFCredit = True

    '    ElseIf (RxNum.Text = "" And Not IsVirtualRx) Then    ' Si es nueva (ordernum=0), lleva AR y se hara en el mismo lab
    '        SpendFFCredit = True

    '    ElseIf (RxNum.Text <> "" And IsModifying And Not IsVirtualRx And Original_IsVirtualRx) Then  ' Es una modificacion y se quita que lleva el AR.
    '        SpendFFCredit = True

    '        'ElseIf (RxNum.Text <> "" And IsModifying And Not IsVirtualRx And Original_SpecialDesID = 0)
    '    ElseIf (RxNum.Text <> "" And IsModifying And Not IsVirtualRx And (Original_SpecialDesID = 0 Or SpecialDesignID > 0)) Then    ' Es una modificacion de una orden no virtual y que no tenia dise�o especial
    '        SpendFFCredit = True

    '    ElseIf (RxNum.Text <> "" And IsModifying And IsVirtualRx And IsARLaboratory) Then    ' Es una modificacion de una orden no virtual y que no tenia dise�o especial
    '        SpendFFCredit = True

    '    ElseIf (IsReceivingVirtualRx) Then
    '        SpendFFCredit = True
    '    End If

    '    Return SpendFFCredit
    'End Function

    ' Esta funcion se utiliza para indicar si se van a consumir disenos especiales
    Private Function UseFreeFormCredit() As Boolean
        Dim SpendFFCredit As Boolean = False

        ' Si el ordernum es igual a cero (orden nueva) y se procesa local
        If (RxNum.Text = "" And ((ARLab = LabID()) Or (Not IsVirtualRx))) Then
            SpendFFCredit = True

            ' Si se esta recibiendo una orden virtual en el lab. remoto
        ElseIf (RxNum.Text <> "" And IsReceivingVirtualRx And ARLab = LabID()) Then
            SpendFFCredit = True

        End If

        Return SpendFFCredit
    End Function

    ' Esta funcion se utiliza para generar calculos sin diseno especial aunque la rx tenga seleccionado uno.
    ' Aplica en las rx's virtuales que se procesan en otro laboratorio y las virtuales de reproceso
    Private Function CalcSpecialDesign() As Boolean
        Dim CalcSD As Boolean = False

        ' Si el ordernum es igual a cero (orden nueva) y se procesara remoto
        If ((ARLab = LabID()) And (IsVirtualRx)) Or (LabID() = RxNum.Text.Substring(0, 2) And ARLab = 0) Then
            CalcSD = True
        End If

        Return CalcSD
    End Function
    'Private Function imprimeDpMono() As String
    '    Dim DpMono As String
    '    Dim mr, ml As Double
    '    mr = 0
    '    ml = 0

    '    If (MonoRight.Text.Length > 0 And MonoLeft.Text.Length > 0) Then
    '        If MonoRight.Text = "0" And MonoLeft.Text = "0" Then
    '            mr = CDbl(DIPLejos.Text) / 2
    '            ml = mr
    '        End If
    '    End If
    '    Return (DpMono)
    'End Function
    Private Sub Preview_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Preview.Click, PreviewNew.Click
        Dim IsWebServiceOK As Boolean = False
        Dim p As New PreviewArmazon(Me.ChangingTraces, OMA, SpecialDesignID, DIPLejos.Text, DIPCerca.Text)

        Dim Offset As Calcot.Desplazamiento = Nothing

        Dim NoCalculos As Short = 0
        Dim LIMaterial As Integer = 0
        Dim LDMaterial As Integer = 0
        Dim LeftEyeOK As Boolean = False
        Dim RightEyeOK As Boolean = False

        'obtenemos MONO




        ' Verifica si se deben consumir creditos para los Free Form
        If SpecialDesignID > 0 Then p.UseFFCredit = UseFreeFormCredit()

        Try
            If RxWECO.Text = "" Then
                RxWECO.Text = 0
            End If
        Catch ex As Exception
            RxWECO.Text = 0
        End Try

        If Val(RxWECO.Text = 0) And ComboTrazos.ComboBox1.Items.Count > 0 Then
            RxWECO.Text = ComboTrazos.Texto
        End If

        If (My.Settings.Plant = "VBENS") Then
            RxWECO.Text = Microsoft.VisualBasic.Right(RxID.Text, 4)
            RxWECO.Text = RxWECO.Text + "00"
        End If

        If IsModifying Then
            If ComboClients.Text.Contains("COSTCO") And RxID.Text.Length < 10 Then
                MsgBox("Error al modificar el cliente, esta Rx no se puede modificar a un cliente de COSTCO", MsgBoxStyle.Critical)
                Exit Sub
            End If
        End If

        If CheckLEye.Checked Then
            If LeftBarcodeRead Then
                LeftEyeOK = True
            End If
        Else
            LeftEyeOK = True
        End If
        If CheckREye.Checked Then
            If RightBarcodeRead Then
                RightEyeOK = True
            End If
        Else
            RightEyeOK = True
        End If
        If (IsModifying And (Me.IsReceivingVirtualRx = False)) Or (Me.IsLocalReceive) Then
            RightEyeOK = True
            LeftEyeOK = True
        End If
        If Not (LeftEyeOK And RightEyeOK) Then
            If ListLeftParts.Items.Count > 0 Or ListRightParts.Items.Count > 0 Then
                MsgBox("No se ha leido la pastilla a utilizar." & vbCrLf & "Recuerda que el listado de partes que te aparece es solamente informativo.", MsgBoxStyle.Critical)
            Else
                MsgBox("No se ha leido la pastilla a utilizar." & vbCrLf & "Debes leer el codigo a utilizar para poder continuar con la Rx", MsgBoxStyle.Critical)
            End If
        Else
            ' Aqui verifica si la Rx proviene de un laboratorio Ver Bien, entonces no revisa el inventario solo cuando modifica o recibe virtuales
            ' Si no pertenece si hace el chequeo al igual que cuando es Rx nueva, pues ningun otro laboratorio genera Rx para ver bien
            ' 10/10/2012 Pedro Far�as
            ' Se a�ade un nuevo ajuste de configuraci�n para ignorar la existencia de inventario, en otras palabras, permite pasar pastillas
            ' aunque no haya cantidad en existencia.

            Dim InvOK As Boolean = My.Settings.IgnoraExistenciasInventario

            If Not (InvOK) Then     '�Revisamos o no el inventario?
                If IsModifying Or IsReceivingVirtualRx Then
                    If IsVBLab(RxNum.Text.Substring(0, 2)) Then
                        InvOK = True
                    ElseIf InventarioOK() Then
                        InvOK = True
                    End If
                ElseIf InventarioOK() Then
                    InvOK = True
                End If
            End If

            'If InvOK And (IsFrameOK) Then
            If InvOK And ((MyFrame.ReadStatus = Frames.ThisFrameReadStatus.Ok) Or PNL_Armazon.Visible = False) Then
                If ListLeftParts.SelectedItems.Count > 0 Or ListRightParts.SelectedItems.Count > 0 Then
                    Try
                        p.RxInfo.Rxident = RxID.Text
                        If AlturaRight.Text = "0" Then
                            AlturaRight.Text = Altura.Text
                        End If
                        If AlturaLeft.Text = "0" Then
                            AlturaLeft.Text = Altura.Text
                        End If
                        If MonoRight.Text = "0" And MonoLeft.Text = "0" Then
                            MonoRight.Text = CDbl(DIPLejos.Text) / 2
                            MonoLeft.Text = MonoRight.Text
                        Else
                            If MonoLeft.Text = "0" And MonoRight.Text <> "0" Then
                                MonoLeft.Text = CDbl(DIPLejos.Text) - CDbl(MonoRight.Text)
                            ElseIf MonoLeft.Text = "0" Then
                                MonoLeft.Text = CDbl(DIPLejos.Text) / 2
                            End If
                            If MonoRight.Text = "0" And MonoLeft.Text <> "0" Then
                                MonoRight.Text = CDbl(DIPLejos.Text) - CDbl(MonoLeft.Text)
                            ElseIf MonoRight.Text = "0" Then
                                MonoRight.Text = CDbl(DIPLejos.Text) / 2
                            End If
                        End If
                        If CDbl(MonoLeft.Text) < 0 Then
                            MonoLeft.Text = "0"
                        End If
                        If CDbl(MonoRight.Text) < 0 Then
                            MonoRight.Text = "0"
                        End If
                        If DIPLejos.Text = "0" Then
                            If CheckLEye.Checked And CheckREye.Checked Then
                                DIPLejos.Text = CDbl(MonoLeft.Text) + CDbl(MonoRight.Text)

                            ElseIf CheckLEye.Checked Then
                                DIPLejos.Text = CDbl(MonoLeft.Text) * 2
                            ElseIf CheckREye.Checked Then
                                DIPLejos.Text = CDbl(MonoRight.Text) * 2
                            End If
                            DIPCerca.Text = DIPLejos.Text
                        End If

                        '---------------------------------------------------------------------------------------------
                        'Aqui enviar datos al Servicio de Atanacio y recibir datos de la pastilla.
                        'Al recibir los datos requeridos, introducirlos al objeto y mostrarlo.
                        '---------------------------------------------------------------------------------------------
                        p.CheckedEyes = PreviewArmazon.Eyes.None
                        p.RxInfo.CheckedEyes = Calcot.RxPrintInfo.Eyes.None

                        Dim x As New ItemWithValue

                        '------------------------------------------------------------------
                        ' Inicializar objeto para OptiSur [Job]
                        '------------------------------------------------------------------
                        'If CBX_SpecialDesign.Checked And My.Settings.EnableOptisur And My.Settings.CheckOptisurBeforePreview Then
                        If My.Settings.EnableOptisur Then
                            Dim SelectedEyes As NewCalcot.Eyes

                            If CheckLEye.Checked And CheckREye.Checked Then
                                SelectedEyes = NewCalcot.Eyes.Both
                            ElseIf CheckLEye.Checked Then
                                SelectedEyes = NewCalcot.Eyes.Left
                            ElseIf CheckREye.Checked Then
                                SelectedEyes = NewCalcot.Eyes.Right
                            End If
                            Optisur = New NewCalcot(IsManualRx, SelectedEyes)
                            Optisur.JobNumber = RxWECO.Text

                        End If

                        '------------------------------------------------------------------

                        ' MANDAMOS EJECUTAR LOS CALCULOS DEL OJO SI ESTA MARCADO EL CHECKBOX Y SI EL LENTE ES DIFERENTE AL LENTE SIN CALCULO 2105099999
                        If CheckLEye.Checked Then
                            x = ListLeftParts.SelectedItem

                            'SI EL LENTE ES DIFERENTE A SIN CALCULO, MANDAMOS EJECUTAR LOS CALCULO PARA EL OJO IZQUIERDO
                            If x.Value <> My.Settings.LenteSinCalculos Then
                                Dim Lente As New InfoPastillas(x.Value)
                                If Lente.Clase = "T" And (Lente.Esfera <> CSng(EsferaLeft.Text) Or (Lente.Cilindro <> CSng(CilindroLeft.Text)) Or (Lente.Adicion <> CSng(AdicionLeft.Text))) Then
                                    'Lente.Clase = "T" And (Lente.Esfera <> CSng(EsferaLeft.Text) Or (Lente.Dise�o = "VS" And Lente.Cilindro <> CSng(CilindroLeft.Text)) Or Lente.Dise�o = "VS" And (Lente.Adicion <> CSng(AdicionLeft.Text))) Then
                                    Throw New Exception("No coincide graduaci�n del LENTE IZQUIERDO con la pastilla." & vbCrLf & vbCrLf _
                                        & "Pastilla = " & vbTab & vbTab & "[" & String.Format("{0:0.00}", Lente.Esfera) & "/" & String.Format("{0:0.00}", Lente.Cilindro) & "]" & vbTab & "Adici�n = " & String.Format("{0:0.00}", Lente.Adicion) & vbCrLf _
                                        & "Graduaci�n =" & vbTab & "[" & String.Format("{0:0.00}", EsferaLeft.Text) & "/" & String.Format("{0:0.00}", CilindroLeft.Text) & "]" & vbTab & "Adici�n = " & String.Format("{0:0.00}", AdicionLeft.Text))
                                End If

                                '---------------------------------------------------------------------------------------------
                                ' Pedro Far�as Lozano # Ago 7 2013 - Verificar que la pastilla a utilizar es progresiva o flat top para lentes con adici�n.
                                ' Si la pastilla es visi�n sencilla debe de seleccionarse un dise�o Free Form
                                '---------------------------------------------------------------------------------------------
                                If Convert.ToDouble(AdicionLeft.Text) <> 0 Then
                                    ' Revisa el tipo de dise�o para hacer un FT o un Progresivo
                                    If Lente.IsSV And SpecialDesignID = 0 Then
                                        If DialogResult.No = MessageBox.Show("Est� usando una pastilla de visi�n sencilla pero ha indicado una adici�n en el lado izquierdo sin dise�o de Free Form." & vbCrLf & vbCrLf & "�Desea continuar?", "Verifique dise�o", MessageBoxButtons.YesNo) Then
                                            Exit Sub
                                        End If
                                    End If
                                End If

                                Dim esf, cil, eje As Single
                                esf = CSng(EsferaLeft.Text)
                                cil = CSng(CilindroLeft.Text)
                                eje = CSng(EjeLeft.Text)
                                If cil > 0 Then
                                    esf += cil
                                    cil *= -1
                                    eje += 90
                                    If eje > 180 Then
                                        eje -= 180
                                    End If
                                End If
                                p.CheckedEyes = PreviewArmazon.Eyes.Left

                                Dim stroptim As String = ""
                                If CheckOptim.Checked Then stroptim = "true" Else stroptim = "false"

                                ' BANDERA_OPTIMIZAR
                                'If My.Settings.Optimizar Then
                                '    LI = New Calcot.SetLadoIzquierdo(x.Value, CDbl(AlturaLeft.Text), (CDbl(DIPLejos.Text) - CDbl(DIPCerca.Text)) / 2, CType(MonoLeft.Text, Single), esf, cil, eje, CType(AdicionLeft.Text, Single), PrismaL, EjePrismaL, stroptim)
                                'Else
                                '    LI = New Calcot.SetLadoIzquierdo(x.Value, CDbl(AlturaLeft.Text), (CDbl(DIPLejos.Text) - CDbl(DIPCerca.Text)) / 2, CType(MonoLeft.Text, Single), esf, cil, eje, CType(AdicionLeft.Text, Single), PrismaL, EjePrismaL, "")
                                'End If

                                p.LeftPill.Text = x.Value.ToString
                                'If CBX_SpecialDesign.Checked And My.Settings.EnableOptisur And My.Settings.CheckOptisurBeforePreview Then
                                If My.Settings.EnableOptisur Then
                                    '-------------------------------------------------------------------------------------------------
                                    ' Calculos OptiSur Ojo Izquierdo
                                    '-------------------------------------------------------------------------------------------------
                                    Optisur.SetLeftItem(x.Value.ToString, esf, cil, eje, CType(AdicionLeft.Text, Single), PrismaL, EjePrismaL, CType(MonoLeft.Text, Single), CType(AlturaLeft.Text, Single))
                                    '-------------------------------------------------------------------------------------------------
                                End If
                                If x.Value = My.Settings.LenteSinCalculos Then
                                    NoCalculos += 1
                                Else
                                    LIMaterial = Me.GetCustomValues(CustomValues.MaterialID, x.Value.ToString)
                                End If
                                p.RxInfo.CheckedEyes = Calcot.RxPrintInfo.Eyes.Left
                                p.BaseL = GetCustomValues(CustomValues.Base, x.Value.ToString)


                                ' Si la variable base contiene el valor de ERROR significa que el lente es terminado y no tiene base
                                If p.BaseL = "ERROR" Then
                                    p.BaseL = ""
                                End If
                                Try
                                    DesignID = GetCustomValues(CustomValues.Dise�oID, x.Value.ToString)
                                    If DesignID = 12 Or DesignID = 14 Then
                                        p.LeftType = Pastilla.Types.FlatTop
                                    End If
                                Catch ex As Exception
                                    DesignID = 0
                                End Try

                                '-------------------------------------------------------------------------------------------------
                                ' Dise�o para OptiSur
                                '-------------------------------------------------------------------------------------------------
                                'If My.Settings.EnableOptisur Then 'And My.Settings.CheckOptisurBeforePreview Then
                                '    Optisur.DesignID = SpecialDesignID

                                'End If
                                '-------------------------------------------------------------------------------------------------
                            Else
                                p.RxInfo.CheckedEyes = Calcot.RxPrintInfo.Eyes.Left
                                NoCalculos += 1
                                LI = New Calcot.SetLadoIzquierdo
                                '-------------------------------------------------------------------------------------------------
                                ' Dise�o para OptiSur
                                '-------------------------------------------------------------------------------------------------
                                'If My.Settings.EnableOptisur Then
                                '    Optisur.DesignID = SpecialDesignID
                                'End If
                                '-------------------------------------------------------------------------------------------------
                            End If
                        Else
                            LI = New Calcot.SetLadoIzquierdo
                        End If

                        If CheckREye.Checked Then
                            x = ListRightParts.SelectedItem

                            'SI EL LENTE ES DIFERENTE A SIN CALCULO, MANDAMOS EJECUTAR LOS CALCULO PARA EL OJO IZQUIERDO
                            If x.Value <> My.Settings.LenteSinCalculos Then
                                Dim Lente As New InfoPastillas(x.Value)
                                If Lente.Clase = "T" And (Lente.Esfera <> CSng(EsferaRight.Text) Or (Lente.Cilindro <> CSng(CilindroRight.Text)) Or (Lente.Adicion <> CSng(AdicionRight.Text))) Then
                                    'Lente.Clase = "T" And (Lente.Esfera <> CSng(EsferaRight.Text) Or (Lente.Dise�o = "VS" And Lente.Cilindro <> CSng(CilindroRight.Text)) Or Lente.Dise�o <> "VS" And (Lente.Adicion <> CSng(AdicionRight.Text))) Then
                                    Throw New Exception("No coincide graduaci�n del LENTE DERECHO con la pastilla." & vbCrLf & vbCrLf _
                                        & "Pastilla = " & vbTab & vbTab & "[" & String.Format("{0:0.00}", Lente.Esfera) & "/" & String.Format("{0:0.00}", Lente.Cilindro) & "]" & vbTab & "Adici�n = " & String.Format("{0:0.00}", Lente.Adicion) & vbCrLf _
                                        & "Graduaci�n =" & vbTab & "[" & String.Format("{0:0.00}", EsferaRight.Text) & "/" & String.Format("{0:0.00}", CilindroRight.Text) & "]" & vbTab & "Adici�n = " & String.Format("{0:0.00}", AdicionRight.Text))
                                    'Throw New Exception("No coincide graduaci�n de lente derecho" & vbCrLf & "Lente = " & Lente.Esfera & "/" & Lente.Cilindro)
                                End If
                                '---------------------------------------------------------------------------------------------
                                ' Pedro Far�as Lozano # Ago 7 2013 - Verificar que la pastilla a utilizar es progresiva o flat top para lentes con adici�n.
                                ' Si la pastilla es visi�n sencilla debe de seleccionarse un dise�o Free Form
                                '---------------------------------------------------------------------------------------------
                                If Convert.ToDouble(AdicionRight.Text) <> 0 Then
                                    ' Revisa el tipo de dise�o para hacer un FT o un Progresivo
                                    If Lente.IsSV And SpecialDesignID = 0 Then
                                        If DialogResult.No = MessageBox.Show("Est� usando una pastilla de visi�n sencilla pero ha indicado una adici�n en el lado derecho sin dise�o de Free Form." & vbCrLf & vbCrLf & "�Desea continuar?", "Verifique dise�o Der.", MessageBoxButtons.YesNo) Then
                                            Exit Sub
                                        End If
                                    End If
                                End If

                                Dim esf, cil, eje As Single
                                esf = CSng(EsferaRight.Text)
                                cil = CSng(CilindroRight.Text)
                                eje = CSng(EjeRight.Text)
                                If cil > 0 Then
                                    esf += cil
                                    cil *= -1
                                    eje += 90
                                    If eje > 180 Then
                                        eje -= 180
                                    End If
                                End If
                                p.CheckedEyes += PreviewArmazon.Eyes.Right

                                Dim stroptim As String = ""
                                If CheckOptim.Checked Then stroptim = "true" Else stroptim = "false"

                                ' BANDERA_OPTIMIZAR
                                'If My.Settings.Optimizar Then
                                '    LD = New Calcot.SetLadoDerecho(x.Value, CDbl(AlturaRight.Text), (CDbl(DIPLejos.Text) - CDbl(DIPCerca.Text)) / 2, CType(MonoRight.Text, Single), esf, cil, eje, CType(AdicionRight.Text, Single), PrismaR, EjePrismaR, stroptim)
                                'Else
                                '    LD = New Calcot.SetLadoDerecho(x.Value, CDbl(AlturaRight.Text), (CDbl(DIPLejos.Text) - CDbl(DIPCerca.Text)) / 2, CType(MonoRight.Text, Single), esf, cil, eje, CType(AdicionRight.Text, Single), PrismaR, EjePrismaR, "")
                                'End If
                                p.RightPill.Text = x.Value.ToString
                                '-------------------------------------------------------------------------------------------------
                                ' Calculos OptiSur Ojo Derecho 
                                '-------------------------------------------------------------------------------------------------
                                'If CBX_SpecialDesign.Checked And My.Settings.EnableOptisur And My.Settings.CheckOptisurBeforePreview Then
                                If My.Settings.EnableOptisur Then
                                    Optisur.SetRightItem(x.Value.ToString, esf, cil, eje, CType(AdicionRight.Text, Single), PrismaR, EjePrismaR, CType(MonoRight.Text, Single), CType(AlturaRight.Text, Single))
                                End If
                                '-------------------------------------------------------------------------------------------------

                                If x.Value = My.Settings.LenteSinCalculos Then
                                    NoCalculos += 1
                                Else
                                    LDMaterial = Me.GetCustomValues(CustomValues.MaterialID, x.Value.ToString)
                                End If
                                p.RxInfo.CheckedEyes += Calcot.RxPrintInfo.Eyes.Right
                                p.BaseR = GetCustomValues(CustomValues.Base, x.Value.ToString)

                                ' Si la variable base contiene el valor de ERROR significa que el lente es terminado y no tiene base
                                If p.BaseR = "ERROR" Then
                                    p.BaseR = ""
                                End If
                                Try
                                    DesignID = GetCustomValues(CustomValues.Dise�oID, x.Value.ToString)
                                    If DesignID = 12 Or DesignID = 14 Then
                                        p.RightType = Pastilla.Types.FlatTop
                                    End If
                                Catch ex As Exception
                                    DesignID = 0
                                End Try
                                ''-------------------------------------------------------------------------------------------------
                                '' Dise�o para OptiSur
                                ''-------------------------------------------------------------------------------------------------
                                ''If CBX_SpecialDesign.Checked And My.Settings.EnableOptisur And My.Settings.CheckOptisurBeforePreview Then
                                'If My.Settings.EnableOptisur Then
                                '    Optisur.DesignID = SpecialDesignID
                                'End If
                                ''-------------------------------------------------------------------------------------------------
                            Else
                                p.RxInfo.CheckedEyes += Calcot.RxPrintInfo.Eyes.Right
                                NoCalculos += 1
                                LD = New Calcot.SetLadoDerecho
                            End If

                        Else
                            LD = New Calcot.SetLadoDerecho
                        End If
                        '-------------------------------------------------------------------------------------------------
                        ' Dise�o para OptiSur
                        '-------------------------------------------------------------------------------------------------
                        'If CBX_SpecialDesign.Checked And My.Settings.EnableOptisur And My.Settings.CheckOptisurBeforePreview Then
                        'If My.Settings.EnableOptisur Then          ' Comentado por fgarcia en Abril 5, 2011

                        ' Se agrego el AND para que no se calcule la rx con dise�o especial cuando no lo requiera el sistema, ya sea
                        ' por ser una rx virtual o alguna otra razon.
                        If My.Settings.EnableOptisur And (My.Settings.FreeFormEnabled Or SpecialDesignID = 1) Then
                            Optisur.DesignID = SpecialDesignID
                        End If
                        '-------------------------------------------------------------------------------------------------


                        If ((p.RxInfo.CheckedEyes = Calcot.RxPrintInfo.Eyes.Both) And (NoCalculos = 2)) _
                            Or ((p.RxInfo.CheckedEyes = Calcot.RxPrintInfo.Eyes.Left) And NoCalculos = 1) _
                            Or ((p.RxInfo.CheckedEyes = Calcot.RxPrintInfo.Eyes.Right) And NoCalculos = 1) Then

                            ' Se procesa sin meter datos a guiador (Lentes sin calculos)
                            '----------------------------------------------------------------
                            PrintObj = New Impresion
                            PrintObj.Puente = CDbl(ContornoPuente.Text) * 100
                            If My.Settings.EnableOptisur Then
                                If IsManualRx Then
                                    Optisur.SetGeoFrame(GetOMAFrameType(ComboArmazon.ComboBox1.SelectedValue), CDbl(ContornoA.Text), CDbl(ContornoB.Text), CDbl(ContornoED.Text), CDbl(ContornoPuente.Text), CDbl(DIPLejos.Text), CDbl(DIPCerca.Text))
                                Else
                                    Select Case My.Settings.OpticalProtocol
                                        Case "DVI"
                                            'Offset.SetRadios = RadiosR
                                            Optisur.SetPattern(GetOMAFrameType(ComboArmazon2.ComboBox1.SelectedIndex), CDbl(ContornoPuente.Text), DIPLejos.Text, DIPCerca.Text, Sizing, 0, RadiosR)
                                        Case "OMA"
                                            'Offset.SetRadios = Convertir400To512(RadiosR)
                                            Optisur.SetPattern(GetOMAFrameType(ComboArmazon2.ComboBox1.SelectedIndex), CDbl(ContornoPuente.Text), DIPLejos.Text, DIPCerca.Text, Sizing, 0, Convertir400To512(RadiosR))
                                    End Select
                                End If
                                Optisur.SetRightItem("2105000000", 0, 0, 0, 0, 0, 0, 0, 0)
                                Optisur.GetContorno()
                                Optisur.GetCalculos(NewCalcot.CalculationOption.CalculateAndSave)
                            End If
                            If My.Settings.EnableOptisur And CBX_SpecialDesign.Checked Then
                                Throw New Exception("Error de Dise�os Especiales" & vbCrLf & "Las pastillas que se ingresaron no pueden pasar por el generador para aplicar el Dise�o Especial Seleccionado.")
                            End If
                            MuestraCalculosSinArmazon()

                            '----------------------------------------------------------------
                        Else
                            Dim fd As New Calcot.ReturnLadoDer
                            Dim fi As New Calcot.ReturnLadoIzq

                            'Dim LS As New DisplPattern
                            'Dim RS As New DisplPattern

                            If Not CheckContorno() Then
                                MsgBox("No se puede continuar con la receta, los valores de contorno son invalidos.", MsgBoxStyle.Exclamation)
                                'If MsgBox("Los valores de contorno son 0. " + vbCrLf + "Deseas que se procese este receta sin armazon?", MsgBoxStyle.YesNo + MsgBoxStyle.Question) = MsgBoxResult.Yes Then
                                '    MuestraCalculosSinArmazon()
                                'End If
                            Else
                                If IsManualRx Then

                                    'Dim contour As New Calcot.Contorno(LD, LI)
                                    'contour.Aro = CDbl(ContornoA.Text)
                                    'contour.Vertical = CDbl(ContornoB.Text)
                                    'contour.Diagonal = CDbl(ContornoED.Text)
                                    'contour.Puente = CDbl(ContornoPuente.Text)

                                    If My.Settings.EnableOptisur Then
                                        If RadioDigitalNew.Checked Then
                                            'Optisur.IsManual = False
                                            Select Case My.Settings.OpticalProtocol
                                                Case "DVI"
                                                    'Offset.SetRadios = RadiosR
                                                    Optisur.SetPattern(GetOMAFrameType(ComboArmazon2.ComboBox1.SelectedIndex), CDbl(ContornoPuente.Text), DIPLejos.Text, DIPCerca.Text, Sizing, 0, RadiosL)
                                                Case "OMA"
                                                    'Offset.SetRadios = Convertir400To512(RadiosR)
                                                    Optisur.SetPattern(GetOMAFrameType(ComboArmazon2.ComboBox1.SelectedIndex), CDbl(ContornoPuente.Text), DIPLejos.Text, DIPCerca.Text, Sizing, 0, Convertir400To512(RadiosL))
                                                    'Optisur.SetPattern(GetOMAFrameType(ComboArmazon2.ComboBox1.SelectedIndex), CDbl(ContornoPuente.Text), DIPLejos.Text, DIPCerca.Text, Sizing, 0, RadiosVirtuales)
                                            End Select
                                        Else
                                            'Optisur.IsManual = True
                                            Optisur.SetGeoFrame(GetOMAFrameType(ComboArmazon2.ComboBox1.SelectedIndex), CDbl(ContornoA.Text), CDbl(ContornoB.Text), CDbl(ContornoED.Text), CDbl(ContornoPuente.Text), CDbl(DIPLejos.Text), CDbl(DIPCerca.Text))
                                        End If
                                    End If
                                    If CBX_SpecialDesign.Checked And My.Settings.EnableOptisur And My.Settings.CheckOptisurBeforePreview Then
                                    End If
                                    Try
                                        'contour.GetContorno()
                                        'If contour.GetCveMensaje = 0 Then
                                        '    IsWebServiceOK = True

                                        '    fd = contour.GetFormaDer
                                        '    fi = contour.GetFormaIzq
                                        '    p.LeftType = contour.TipoL
                                        '    p.RightType = contour.TipoR
                                        '    'CUANDO ES SOLICITADO EL ARMAZON A ATANACIO NOS REGRESA 512 RADIOS, PERO SI ESTAMOS CON OMA 
                                        '    'DEMEMOS DE TRANSFORMAR ESOS RADIOS DE 512 A 400 PUNTOS
                                        '    Select Case My.Settings.OpticalProtocol
                                        '        Case "DVI"
                                        '            RadiosL = contour.GetRadios
                                        '        Case "OMA"
                                        '            RadiosL = Convertir512To400(contour.GetRadios)
                                        '    End Select
                                        '    p.RadiosL = RadiosL
                                        '    MirrorRadios(Pastilla.Sides.Left)
                                        '    p.RadiosR = RadiosR
                                        'Else
                                        '    IsWebServiceOK = False
                                        '    Throw New Exception(contour.GetMensaje)
                                        'End If
                                        If Not Me.IsReceivingVirtualRx Or RadiosL(0) = 0 Then

                                            '--------------------------------------------------------------------------------------------------------------
                                            '--------------------------------------------------------------------------------------------------------------
                                            If My.Settings.EnableOptisur Then
                                                Try
                                                    Optisur.GetContorno()
                                                    'Optisur.GetCalculos()

                                                    IsWebServiceOK = True
                                                    '----------------------------------------------------------------
                                                    ' Verificar el tipo de lente de la pastilla
                                                    '----------------------------------------------------------------
                                                    p.LeftType = Optisur.LeftDrawingDesign
                                                    p.RightType = Optisur.RightDrawingDesign
                                                    '----------------------------------------------------------------
                                                    'CUANDO ES SOLICITADO EL ARMAZON A ATANACIO NOS REGRESA 512 RADIOS, PERO SI ESTAMOS CON OMA 
                                                    'DEBEMOS DE TRANSFORMAR ESOS RADIOS DE 512 A 400 PUNTOS
                                                    Select Case My.Settings.OpticalProtocol
                                                        Case "DVI"
                                                            RadiosL = Optisur.GetRadios()
                                                        Case "OMA"
                                                            RadiosL = Convertir512To400(Optisur.GetRadios())
                                                    End Select
                                                    p.RadiosL = RadiosL

                                                    MirrorRadios(Pastilla.Sides.Left)
                                                    p.RadiosR = RadiosR

                                                Catch ex As Exception
                                                    IsWebServiceOK = False
                                                    Throw New Exception("Error al obtener c�lculos de Optisur." & vbCrLf & ex.Message)
                                                End Try
                                            Else
                                                'Offset = New Calcot.Desplazamiento(LD, LI)
                                                'contour.GetContorno()
                                                'Offset.GetFormaDer = contour.GetFormaDer
                                                'Offset.GetFormaIzq = contour.GetFormaIzq
                                                'If contour.GetCveMensaje = 0 Then
                                                '    IsWebServiceOK = True

                                                '    fd = contour.GetFormaDer
                                                '    fi = contour.GetFormaIzq
                                                '    p.LeftType = contour.TipoL
                                                '    p.RightType = contour.TipoR

                                                '    'CUANDO ES SOLICITADO EL ARMAZON A ATANACIO NOS REGRESA 512 RADIOS, PERO SI ESTAMOS CON OMA 
                                                '    'DEBEMOS DE TRANSFORMAR ESOS RADIOS DE 512 A 400 PUNTOS
                                                '    Select Case My.Settings.OpticalProtocol
                                                '        Case "DVI"
                                                '            RadiosL = contour.GetRadios
                                                '        Case "OMA"
                                                '            RadiosL = Convertir512To400(contour.GetRadios)
                                                '    End Select

                                                '    p.RadiosL = RadiosL

                                                '    MirrorRadios(Pastilla.Sides.Left)
                                                '    p.RadiosR = RadiosR
                                                '    'OMA.SetTRCFMT(1, 400, OMAFile.TRACERadiusModeIdentifier.EvenlySpaced, OMAFile.TRACESide.Right, OMAFile.TRACEObject.Frame, RadiosR)
                                                '    'OMA.SetTRCFMT(1, 400, OMAFile.TRACERadiusModeIdentifier.EvenlySpaced, OMAFile.TRACESide.Left, OMAFile.TRACEObject.Frame, RadiosL)
                                                'Else
                                                '    IsWebServiceOK = False
                                                '    Throw New Exception(contour.GetMensaje)
                                                'End If
                                            End If
                                            If CBX_SpecialDesign.Checked And My.Settings.EnableOptisur And My.Settings.CheckOptisurBeforePreview Then
                                            Else

                                            End If
                                            '--------------------------------------------------------------------------------------------------------------
                                            ' End del checkbox de Dise�os Especiales
                                            '--------------------------------------------------------------------------------------------------------------

                                        Else
                                            ' ''Dim Offset As New Calcot.Desplazamiento(LD, LI)
                                            'Offset = New Calcot.Desplazamiento(LD, LI)
                                            'Offset.Ajuste = Sizing                           ' es el sizing en la tabla TracerData
                                            'Offset.Puente = CDbl(ContornoPuente.Text)
                                            'Select Case My.Settings.OpticalProtocol
                                            '    Case "DVI"
                                            '        Offset.SetRadios = RadiosL
                                            '    Case "OMA"
                                            '        Offset.SetRadios = Convertir400To512(RadiosL)
                                            'End Select

                                            'Offset.GetDesplazo()

                                            Optisur.GetContorno()
                                            IsWebServiceOK = True

                                            'If Offset.GetCveMensaje = 0 Then
                                            IsWebServiceOK = True
                                            'fd = Offset.GetFormaDer
                                            'fi = Offset.GetFormaIzq
                                            '----------------------------------------------------------------
                                            ' Verificar el tipo de lente de la pastilla
                                            '----------------------------------------------------------------
                                            p.LeftType = Optisur.LeftDrawingDesign
                                            p.RightType = Optisur.RightDrawingDesign
                                            '----------------------------------------------------------------

                                            'p.LeftType = Offset.TipoL
                                            'p.RightType = Offset.TipoR
                                            'RadiosL = RadiosR

                                            p.RadiosR = RadiosR
                                            MirrorRadios(Pastilla.Sides.Right)
                                            p.RadiosL = RadiosL
                                            p.Sizing = Sizing
                                            'Else
                                            'IsWebServiceOK = False
                                            'Throw New Exception(Offset.GetMensaje)
                                            'End If

                                            'Try
                                            '    Optisur.GetContorno()
                                            '    'Optisur.GetCalculos()

                                            '    IsWebServiceOK = True
                                            '    '----------------------------------------------------------------
                                            '    ' Verificar el tipo de lente de la pastilla
                                            '    '----------------------------------------------------------------
                                            '    p.LeftType = Optisur.LeftDrawingDesign
                                            '    p.RightType = Optisur.RightDrawingDesign
                                            '    '----------------------------------------------------------------
                                            '    'CUANDO ES SOLICITADO EL ARMAZON A ATANACIO NOS REGRESA 512 RADIOS, PERO SI ESTAMOS CON OMA 
                                            '    'DEBEMOS DE TRANSFORMAR ESOS RADIOS DE 512 A 400 PUNTOS
                                            '    Select Case My.Settings.OpticalProtocol
                                            '        Case "DVI"
                                            '            RadiosL = Optisur.GetRadios()
                                            '        Case "OMA"
                                            '            RadiosL = Convertir512To400(Optisur.GetRadios())
                                            '    End Select
                                            '    p.RadiosL = RadiosL

                                            '    MirrorRadios(Pastilla.Sides.Left)
                                            '    p.RadiosR = RadiosR

                                            'Catch ex As Exception
                                            '    IsWebServiceOK = False
                                            '    Throw New Exception("Error al obtener c�lculos de Optisur." & vbCrLf & ex.Message)
                                            'End Try

                                        End If

                                    Catch ex As Exception
                                        Throw New Exception("Error al obtener datos de contorno manual. " & vbCrLf & ex.Message)
                                    End Try

                                Else
                                    'If CBX_SpecialDesign.Checked And My.Settings.EnableOptisur And My.Settings.CheckOptisurBeforePreview Then
                                    If My.Settings.EnableOptisur Then
                                        Try

                                            'Offset = New Calcot.Desplazamiento(LD, LI)



                                            'mod by Marco Angulo
                                            'si la receta es del guiador virtual entonces los radios son extraidos de la bd postgress del guiador
                                            'de lo contrario los radios son extraidos del LocalLab200

                                            If Not RxVirtual Then
                                                Dim temp As String = ContornoPuente.Text
                                                If ChangingTraces Then
                                                    GetRadiosFromDatabase(ComboTrazos.ComboBox1.SelectedValue)
                                                Else
                                                    GetRadiosFromDatabase(RxWECO.Text)
                                                End If
                                                ContornoPuente.Text = temp
                                            End If
                                            '****CONVERTIMOS LOS 400 RADIOS A 512, YA QUE CALCOT FUNCIONA CON 512 RADISO
                                            Select Case My.Settings.OpticalProtocol
                                                Case "DVI"
                                                    'Offset.SetRadios = RadiosR
                                                    Optisur.SetPattern(GetOMAFrameType(ComboArmazon2.ComboBox1.SelectedIndex), CDbl(ContornoPuente.Text), DIPLejos.Text, DIPCerca.Text, Sizing, 0, RadiosR)
                                                Case "OMA"
                                                    'Offset.SetRadios = Convertir400To512(RadiosR)
                                                    Optisur.SetPattern(GetOMAFrameType(ComboArmazon2.ComboBox1.SelectedIndex), CDbl(ContornoPuente.Text), DIPLejos.Text, DIPCerca.Text, Sizing, 0, Convertir400To512(RadiosR))
                                            End Select

                                            Optisur.GetContorno()


                                            'Offset.GetDesplazo()

                                            '---------------------------------------------------------------------------
                                            'Aqui se empiezan a llenar las variables que regres� el servicio viejo
                                            '---------------------------------------------------------------------------
                                            Dim LeftOk As Boolean = True
                                            Dim RightOk As Boolean = True
                                            'Try
                                            '    If Optisur.DisplLeft.notice.ToUpper <> "LENTE SALE" Then
                                            '        LeftOk = False
                                            '    End If
                                            'Catch ex As Exception

                                            'End Try
                                            'Try
                                            '    If Optisur.DisplRight.notice.ToUpper <> "LENTE SALE" Then
                                            '        RightOk = False
                                            '    End If
                                            'Catch ex As Exception

                                            'End Try

                                            If LeftOk And RightOk Then

                                                IsWebServiceOK = True
                                                'fd = Offset.GetFormaDer
                                                'fi = Offset.GetFormaIzq
                                                p.LeftType = Optisur.LeftDrawingDesign
                                                p.RightType = Optisur.RightDrawingDesign
                                                p.RadiosL = RadiosR
                                                p.RadiosR = RadiosL
                                                p.Sizing = Sizing
                                            Else
                                                IsWebServiceOK = False
                                                Throw New Exception(Optisur.DisplRight.notice & " - " & Optisur.DisplLeft.notice)
                                            End If
                                            'If Offset.GetCveMensaje = 0 Then
                                            '    IsWebServiceOK = True
                                            '    fd = Offset.GetFormaDer
                                            '    fi = Offset.GetFormaIzq
                                            '    p.LeftType = Offset.TipoL
                                            '    p.RightType = Offset.TipoR
                                            '    p.RadiosL = RadiosR
                                            '    p.RadiosR = RadiosL
                                            '    p.Sizing = Sizing
                                            'Else
                                            '    IsWebServiceOK = False
                                            '    Throw New Exception(Offset.GetMensaje)
                                            'End If
                                        Catch ex As Exception
                                            Throw New Exception("Error al obtener datos de contorno digital. " & vbCrLf & ex.Message)
                                        End Try

                                    Else
                                        ' ''Dim Offset As New Calcot.Desplazamiento(LD, LI)
                                        Offset = New Calcot.Desplazamiento(LD, LI)

                                        Offset.Ajuste = Sizing                           ' es el sizing en la tabla TracerData
                                        Offset.Puente = CDbl(ContornoPuente.Text)
                                        Try
                                            'mod by Marco Angulo
                                            'si la receta es del guiador virtual entonces los radios son extraidos de la bd postgress del guiador
                                            'de lo contrario los radios son extraidos del LocalLab200

                                            If Not RxVirtual Then
                                                Dim temp As String = ContornoPuente.Text
                                                If ChangingTraces Then
                                                    GetRadiosFromDatabase(ComboTrazos.ComboBox1.SelectedValue)
                                                Else
                                                    GetRadiosFromDatabase(RxWECO.Text)
                                                End If
                                                ContornoPuente.Text = temp
                                            End If
                                            '****CONVERTIMOS LOS 400 RADIOS A 512, YA QUE CALCOT FUNCIONA CON 512 RADISO
                                            Select Case My.Settings.OpticalProtocol
                                                Case "DVI"
                                                    Offset.SetRadios = RadiosR
                                                Case "OMA"
                                                    Offset.SetRadios = Convertir400To512(RadiosR)
                                            End Select
                                            Offset.GetDesplazo()
                                            If Offset.GetCveMensaje = 0 Then
                                                IsWebServiceOK = True
                                                fd = Offset.GetFormaDer
                                                fi = Offset.GetFormaIzq
                                                p.LeftType = Offset.TipoL
                                                p.RightType = Offset.TipoR
                                                p.RadiosL = RadiosR
                                                p.RadiosR = RadiosL
                                                p.Sizing = Sizing
                                            Else
                                                IsWebServiceOK = False
                                                Throw New Exception(Offset.GetMensaje)
                                            End If
                                        Catch ex As Exception
                                            Throw New Exception("Error al obtener datos de contorno digital. " & vbCrLf & ex.Message)
                                        End Try

                                    End If
                                End If
                                '---------------------------------------------------------------------------------------------
                                If IsWebServiceOK Then
                                    Dim Continuar As Boolean = True 'False

                                    '' Validacion para que los materiales sean iguales
                                    'If LIMaterial <> 0 Then
                                    '    If LDMaterial = LIMaterial Or LDMaterial = 0 Then
                                    '        Continuar = True
                                    '    End If
                                    'Else
                                    '    If LDMaterial <> 0 Then
                                    '        Continuar = True
                                    '    End If
                                    'End If

                                    If Continuar Then
                                        p.Puente = Val(ContornoPuente.Text)
                                        p.MonoL = Val(MonoLeft.Text) - (Val(ContornoPuente.Text) / 2)
                                        p.MonoR = Val(MonoRight.Text) - (Val(ContornoPuente.Text) / 2)
                                        If Val(MonoRight.Text) = 0 Then
                                            p.MonoR = ((Val(DIPLejos.Text) - Val(ContornoPuente.Text)) / 2)
                                            p.MonoL = p.MonoR
                                        End If
                                        p.Altura = Val(Altura.Text)
                                        p.AlturaLeft = Val(AlturaLeft.Text)
                                        p.AlturaRight = Val(AlturaRight.Text)
                                        If Val(AlturaRight.Text) = 0 Then
                                            p.AlturaLeft = p.Altura
                                            p.AlturaRight = p.Altura
                                        End If

                                        p.RefWECO = (RxWECO.Text)

                                        p.Scale = 0.039




                                        'If CBX_SpecialDesign.Checked And My.Settings.EnableOptisur And My.Settings.CheckOptisurBeforePreview Then
                                        If My.Settings.EnableOptisur Then
                                            Try
                                                'Optisur.GetContorno()
                                                p.ExcVR = Optisur.DisplRight.descentering.y
                                                p.ExcHR = Optisur.DisplRight.descentering.x
                                                p.InsetR = Math.Abs(Optisur.DisplRight.LRP.x)
                                                p.DropR = Math.Abs(Optisur.DisplRight.LRP.y)
                                                p.AltoObleaR = Optisur.DisplRight.segHt
                                                p.AnchoObleaR = Optisur.DisplRight.segWd
                                                p.DiametroPastillaR = Optisur.DisplRight.diam
                                                p.RightPupilaX = Optisur.DisplRight.displ.x
                                                p.RightPupilaY = Optisur.DisplRight.displ.y

                                                p.ExcVL = Optisur.DisplLeft.descentering.y
                                                p.ExcHL = Optisur.DisplLeft.descentering.x
                                                p.InsetL = Math.Abs(Optisur.DisplLeft.LRP.x)
                                                p.DropL = Math.Abs(Optisur.DisplLeft.LRP.y)
                                                p.AltoObleaL = Optisur.DisplLeft.segHt
                                                p.AnchoObleaL = Optisur.DisplLeft.segWd
                                                p.DiametroPastillaL = Optisur.DisplLeft.diam
                                                p.LeftPupilaX = Optisur.DisplLeft.displ.x
                                                p.LeftPupilaY = Optisur.DisplLeft.displ.y

                                                If IsManualRx Then
                                                    p.TextAro.Text = ContornoA.Text
                                                    p.TextVertical.Text = ContornoB.Text
                                                    p.TextDiagonal.Text = ContornoED.Text
                                                Else
                                                    If Optisur.Contorno.GeoFrame.hrzBox = 0 Then
                                                        p.TextAro.Text = ContornoA.Text
                                                        p.TextVertical.Text = ContornoB.Text
                                                        p.TextDiagonal.Text = ContornoED.Text
                                                    Else
                                                        p.TextAro.Text = Optisur.Contorno.GeoFrame.hrzBox
                                                        p.TextVertical.Text = Optisur.Contorno.GeoFrame.vrtBox
                                                        p.TextDiagonal.Text = Optisur.Contorno.GeoFrame.efDiam
                                                    End If

                                                End If

                                            Catch ex As Exception
                                                Throw New Exception("Error al llenar la informacion de Optisur en la Vista Preliminar." & vbCrLf & ex.Message)
                                            End Try
                                        Else
                                            p.ExcVR = fd.ExcenV
                                            p.ExcHR = fd.ExcenH
                                            p.InsetR = fd.SegmentoH
                                            p.DropR = fd.SegmentoV
                                            p.AltoObleaR = fd.Altura_Oblea
                                            p.AnchoObleaR = fd.Ancho_Oblea
                                            p.DiametroPastillaR = fd.diametro
                                            p.RightPupilaX = fd.DH
                                            p.RightPupilaY = fd.DV

                                            p.ExcVL = fi.ExcenV
                                            p.ExcHL = fi.ExcenH
                                            p.InsetL = fi.SegmentoH
                                            p.DropL = fi.SegmentoV
                                            p.AltoObleaL = fi.Altura_Oblea
                                            p.AnchoObleaL = fi.Ancho_Oblea
                                            p.DiametroPastillaL = fi.diametro
                                            p.LeftPupilaX = fi.DH
                                            p.LeftPupilaY = fi.DV

                                            If fi.Aro = 0 Then
                                                p.TextAro.Text = fd.Aro
                                                p.TextVertical.Text = fd.Vertical
                                                p.TextDiagonal.Text = fd.Diagonal
                                            Else
                                                p.TextAro.Text = fi.Aro
                                                p.TextVertical.Text = fi.Vertical
                                                p.TextDiagonal.Text = fi.Diagonal
                                            End If
                                            ContornoA.Text = p.TextAro.Text
                                            ContornoB.Text = p.TextVertical.Text
                                            ContornoED.Text = p.TextDiagonal.Text


                                        End If



                                        GetMaterialDesign(MaterialID, DesignID)

                                        p.TextPuente.Text = ContornoPuente.Text
                                        p.Mat_Arm = ComboArmazon2.ComboBox1.SelectedIndex
                                        p.IsManualFrame = IsManualRx

                                        'p.LI = LI
                                        'p.LD = LD
                                        p.Optisur = Optisur

                                        If ComboTinte.SelectedValue <> 0 Then
                                            If CheckLevel1.Checked Then
                                                p.RxInfo.TinteNum = "1"
                                            Else
                                                If CheckLevel2.Checked Then
                                                    p.RxInfo.TinteNum = "2"
                                                Else
                                                    p.RxInfo.TinteNum = "3"
                                                End If
                                            End If
                                            p.RxInfo.TinteColor = ComboTinte.Texto
                                        End If
                                        p.RxInfo.AdicionL = AdicionLeft.Text
                                        p.RxInfo.AdicionR = AdicionRight.Text
                                        p.RxInfo.Armazon = ComboArmazon.ComboBox1.Text
                                        p.RxInfo.Cliente = ComboClients.Text
                                        p.RxInfo.Rxident = RxID.Text
                                        p.RxInfo.DipceDiple = DIPLejos.Text & "-" & DIPCerca.Text
                                        If RadioGradiente.Checked Then p.RxInfo.Gradiente = "GRADIENTE"
                                        If CheckBiselado.Checked Then p.RxInfo.Biselado = "BISELADO"
                                        If CheckAR.Checked Then p.RxInfo.AR = "A/R"
                                        If CheckARGold.Checked Then p.RxInfo.AR = "A/R GOLD"
                                        If CheckARMatiz.Checked Then p.RxInfo.AR = "A/R MATIZ"
                                        If CheckAbril.Checked Then p.RxInfo.Abrillantado = "ABRILLANTADO"

                                        ' BANDERA_OPTIMIZAR
                                        If My.Settings.Optimizar Then
                                            If CheckOptim.Checked Then p.RxInfo.Optimizado = "OPTIMIZADO"
                                        End If


                                        If CheckREye.Checked Then
                                            x = ListRightParts.SelectedItem
                                            If My.Settings.LenteSinCalculos = x.Value Then
                                                If CheckLEye.Checked Then
                                                    x = ListLeftParts.SelectedItem
                                                End If
                                            End If
                                        ElseIf CheckLEye.Checked Then
                                            x = ListLeftParts.SelectedItem
                                        End If
                                        If x.Value <> "" Then
                                            'rx.Dise�o = GetCustomValues(CustomValues.Dise�o, value.Value.ToString)
                                            'rx.Material = GetCustomValues(CustomValues.Material, value.Value.ToString)
                                            p.RxInfo.Dise�o = GetCustomValues(CustomValues.Dise�o, x.Value.ToString)
                                            p.RxInfo.Material = GetCustomValues(CustomValues.Material, x.Value.ToString)
                                        End If

                                        p.RxInfo.Puente = CDbl(ContornoPuente.Text) * 100
                                        p.RxInfo.ModificacionRx = OrderExists
                                        p.RxInfo.SinArmazon = False
                                        p.RxInfo.RxFromVirtual = Me.RxVirtual 'especificamos si la receta proviene del virtual
                                        'verificamos si la receta es una modificacion 
                                        If IsModifying Then
                                            p.FechaInicial = Me.FechaInicial
                                            p.FechaEntrada = Me.FechaEntrada
                                        Else
                                            p.FechaEntrada = ""
                                            p.FechaInicial = ""
                                        End If

                                        If Me.RxVirtual Then p.RxInfo.RxNumFromVirtual = RxID.Text 'mandamos el numero de rx virtual almacenado en guiador, para act. pend = 22 al generar la rx
                                        p.IsReceivingVirtualRx = Me.IsReceivingVirtualRx

                                        'MANDAMOS VARIABLES OMA ALA FORMA PREVIEW
                                        p.OMA_EyeSide = Me.OMA_EyeSide
                                        p.OMA_FCOCINR = Me.OMA_FCOCINR
                                        p.OMA_FCOCINL = Me.OMA_FCOCINL
                                        p.OMA_FCOCUPR = Me.OMA_FCOCUPR
                                        p.OMA_FCOCUPL = Me.OMA_FCOCUPL
                                        p.OMA_FCSGINR = Me.OMA_FCSGINR
                                        p.OMA_FCSGINL = Me.OMA_FCSGINL
                                        p.OMA_FCSGUPR = Me.OMA_FCSGUPR
                                        p.OMA_FCSGUPL = Me.OMA_FCSGUPL

                                        '---------------------------------------
                                        ' Aqui le mando el numero de trazo
                                        '---------------------------------------
                                        p.Framenum = FrameNum
                                        '---------------------------------------
                                        'p.Offset = Offset
                                        If My.Settings.HasMEIEdger Then
                                            '-------------------------------------------------------------------------------
                                            ' Populamos el objeto OMA con la informacion que hasta el momento se tiene
                                            '-------------------------------------------------------------------------------
                                            OMA.SetRXNM(RxID.Text)
                                            OMA.SetSPH(EsferaRight.Text, EsferaLeft.Text)
                                            OMA.SetCYL(CilindroRight.Text, CilindroLeft.Text)
                                            OMA.SetAX(EjeRight.Text, EjeLeft.Text)
                                            OMA.SetIPD(MonoRight.Text, MonoLeft.Text)
                                            OMA.SetNPD(MonoRight.Text - 1, MonoLeft.Text - 1)
                                            OMA.SetADD(AdicionRight.Text, AdicionLeft.Text)
                                            OMA.SetPRVA(EjePrismaR, EjePrismaL)
                                            OMA.SetPRVM(PrismaR, PrismaL)
                                            OMA.SetHBOX(p.TextAro.Text, p.TextAro.Text)
                                            OMA.SetVBOX(p.TextVertical.Text, p.TextVertical.Text)
                                            OMA.SetDBL(ContornoPuente.Text)
                                            OMA.SetDIA(p.TextDiagonal.Text, p.TextDiagonal.Text)
                                            Dim LMATID_R As String = "?"
                                            Dim LMATTYPE_R As String = 0
                                            Dim LMATNAME_R As String = "?"
                                            Dim LNAM_R As String = 0
                                            Dim LTYP_R As String = 0
                                            Dim LMATID_L As String = "?"
                                            Dim LMATTYPE_L As String = 0
                                            Dim LMATNAME_L As String = "?"
                                            Dim LNAM_L As String = 0
                                            Dim LTYP_L As String = 0
                                            Dim FRNT_R As Single = 0
                                            Dim FRNT_L As Single = 0

                                            If CheckREye.Checked And p.RightPill.Text.Length > 0 Then GetOMALabelsFromParts(p.RightPill.Text, LMATID_R, LMATNAME_R, LMATTYPE_R, LNAM_R, LTYP_R, FRNT_R)
                                            If CheckLEye.Checked And p.LeftPill.Text.Length > 0 Then GetOMALabelsFromParts(p.LeftPill.Text, LMATID_L, LMATNAME_L, LMATTYPE_L, LNAM_L, LTYP_L, FRNT_L)

                                            OMA.SetLMATID(LMATID_R, LMATID_L)
                                            OMA.SetLMATNAME(LMATNAME_R, LMATNAME_L)
                                            OMA.SetLMATTYPE(LMATTYPE_R, LMATTYPE_L)
                                            OMA.SetLMFR(OMAFile.Manufacturer.AUGEN, OMAFile.Manufacturer.AUGEN)
                                            OMA.SetLNAM(LNAM_R, LNAM_L)
                                            OMA.SetLTYP(LTYP_R, LTYP_L)

                                            If FRNT_R = -1 Then
                                                Try
                                                    FRNT_R = GetBaseCurveFromSphereandCylinder(p.RightPill.Text)
                                                Catch ex As Exception
                                                    Dim ev As New EditValues()
                                                    ev.LabelValue.Text = "Curva Base del OJO DERECHO:"
                                                    ev.ShowDialog()
                                                    If ev.Changed Then
                                                        FRNT_R = ev.TextValue.Text
                                                    End If
                                                End Try
                                            End If
                                            If FRNT_L = -1 Then
                                                Try
                                                    FRNT_L = GetBaseCurveFromSphereandCylinder(p.LeftPill.Text)
                                                Catch ex As Exception
                                                    Dim ev As New EditValues()
                                                    ev.LabelValue.Text = "Curva Base del OJO IZQUIERDO:"
                                                    ev.ShowDialog()
                                                    If ev.Changed Then
                                                        FRNT_L = ev.TextValue.Text
                                                    End If
                                                End Try
                                            End If


                                            OMA.SetFRNT(FRNT_R, FRNT_L)         ' Curva Base del Armazon
                                            OMA.Set_TBASE(FRNT_R, FRNT_L)       ' Curva Base de la Pastilla
                                            OMA.SetFCRV(FRNT_R, FRNT_L)         ' Curva Base del Armazon
                                            OMA.Set_FRNT(ConvertBaseCurveToRadius(FRNT_R), ConvertBaseCurveToRadius(FRNT_L))        ' Radio del Armazon
                                            OMA.Set_RMONT(ConvertBaseCurveToRadius(FRNT_R), ConvertBaseCurveToRadius(FRNT_L))       ' Radio de la Pastilla


                                            OMA.SetCOLR(ComboTinte.Texto, ComboTinte.Texto)
                                            'oma.SetDO(OMAFile.EyeSides.Both)
                                            Select Case ComboArmazon2.Texto.ToUpper
                                                Case "METAL" : OMA.SetFTYP(OMAFile.FrameTypes.Metal) : OMA.SetETYP(OMAFile.LensTypes.Bevel, OMAFile.LensTypes.Bevel)
                                                Case "PLASTICO" : OMA.SetFTYP(OMAFile.FrameTypes.Metal) : OMA.SetETYP(OMAFile.LensTypes.Bevel, OMAFile.LensTypes.Bevel)
                                                Case "RANURADO" : OMA.SetFTYP(OMAFile.FrameTypes.Rimless) : OMA.SetETYP(OMAFile.LensTypes.Rimless, OMAFile.LensTypes.Rimless)
                                                Case "PERFORADO" : OMA.SetFTYP(OMAFile.FrameTypes.Rimless) : OMA.SetETYP(OMAFile.LensTypes.Groove, OMAFile.LensTypes.Groove)
                                            End Select

                                            'oma.WriteFile("C:\Test_OMA_File.txt")

                                        End If

                                        p.TextAlturaL.Text = Me.AlturaLeft.Text
                                        p.TextAlturaR.Text = Me.AlturaRight.Text
                                        p.ShowDialog()

                                        Optisur = p.Optisur

                                        'Me.AlturaLeft.Text = p.TextAlturaL.Text
                                        'Me.AlturaRight.Text = p.TextAlturaR.Text


                                        '----LLENAMOS LAS VARIABLES PARA OMA-----------------------
                                        'CUANDO EL LENTE ES UN BIFOCAL LA EXCENTRICIDAD ES LA DISTANCIA DEL C. GEO AL C. OPTICO
                                        Dim ExHR, AltOblR, AnchOblR, ExHL, AltOblL, AnchOblL, ExVR, ExVL, DiaR, DiaL As String
                                        'ExHR = ""
                                        'AltOblR = ""
                                        'AnchOblR = ""
                                        'ExHL = ""
                                        'AltOblL = ""
                                        'AnchOblL = ""
                                        'ExVL = ""
                                        'ExVR = ""
                                        'If (CheckREye.Checked And CheckLEye.Checked) Then
                                        'ExHR = fd.ExcenH
                                        'AltOblR = fd.Altura_Oblea
                                        'AnchOblR = fd.Ancho_Oblea
                                        'ExHL = fi.ExcenH
                                        'AltOblL = fi.Altura_Oblea
                                        'AnchOblL = fi.Ancho_Oblea
                                        'ExVR = fd.ExcenV
                                        'ExVL = fi.ExcenV
                                        'Else
                                        If (CheckREye.Checked) Then
                                            'ExHR = fd.ExcenH
                                            'AltOblR = fd.Altura_Oblea
                                            'AnchOblR = fd.Ancho_Oblea
                                            'ExVR = fd.ExcenV
                                            'DiaR = fd.diametro

                                            ExHR = Optisur.DisplRight.descentering.x
                                            ExVR = Optisur.DisplRight.descentering.y
                                            AltOblR = Optisur.DisplRight.segHt
                                            AnchOblR = Optisur.DisplRight.segWd
                                            DiaR = Optisur.DisplRight.diam

                                        Else
                                            ExHR = OMA_BCOCINR
                                            AltOblR = OMA_SDEPTHR
                                            AnchOblR = OMA_SWIDHTR
                                            ExVR = OMA_BCOCUPR
                                            DiaR = OMA_DIAR
                                        End If

                                        If (CheckLEye.Checked) Then
                                            'ExHL = fi.ExcenH
                                            'AltOblL = fi.Altura_Oblea
                                            'AnchOblL = fi.Ancho_Oblea
                                            'ExVL = fi.ExcenV
                                            'DiaL = fi.diametro

                                            ExHL = Optisur.DisplLeft.descentering.x
                                            ExVL = Optisur.DisplLeft.descentering.y
                                            AltOblL = Optisur.DisplLeft.segHt
                                            AnchOblL = Optisur.DisplLeft.segWd
                                            DiaL = Optisur.DisplLeft.diam

                                        Else
                                            ExHL = OMA_BCOCINL
                                            AltOblL = OMA_SDEPTHL
                                            AnchOblL = OMA_SWIDHTL
                                            ExVL = OMA_BCOCUPL
                                            DiaL = OMA_DIAL
                                        End If
                                        'End If
                                        OMA_BCOCIN = "BCOCIN=" & ExHR & ";" & ExHL
                                        OMA_BCOCUP = "BCOCUP=" & ExVR & ";" & ExVL
                                        OMA_FCOCUP = p.OMA_FCOCUP
                                        OMA_FCOCIN = p.OMA_FCOCIN
                                        OMA_FCSGIN = p.OMA_FCSGIN
                                        OMA_FCSGUP = p.OMA_FCSGUP
                                        OMA_SDEPTH = "SDEPTH=" & AltOblR & ";" & AltOblL
                                        OMA_SWIDHT = "SWIDTH=" & AnchOblR & ";" & AnchOblL

                                        'If (CheckREye.Checked And CheckLEye.Checked) Then
                                        '    DiaR = fd.diametro
                                        '    DiaL = fi.diametro
                                        'Else
                                        '    If (CheckREye.Checked) Then DiaR = fd.diametro
                                        '    If (CheckLEye.Checked) Then DiaL = fi.diametro
                                        'End If
                                        OMA_DIA = "DIA=" & DiaR & ";" & DiaL
                                        '----------------------------------------------------------

                                        If My.Settings.HasMEIEdger And p.Status Then
                                            OMA.SetBCOCIN(OMA_BCOCIN)
                                            OMA.SetBCOCUP(OMA_BCOCUP)
                                            OMA.SetFCOCUP(OMA_FCOCUP)
                                            OMA.SetFCOCIN(OMA_FCOCIN)
                                            OMA.SetFCSGIN(OMA_FCSGIN)
                                            OMA.SetFCSGUP(OMA_FCSGUP)
                                            OMA.SetSDEPTH(OMA_SDEPTH)
                                            OMA.SetSWIDTH(OMA_SWIDHT)
                                            OMA.SetDIA(OMA_DIA)
                                        End If
                                        'Throw New Exception("x")
                                        '-----------------------------------------------------------------------------
                                        '-- ASIGNAMOS EL OBJETO DE IMPRESION PARA GENERAR LA IMPRESION AL GENERAR LA ORDEN EN VANTAGE
                                        Me.PrintObj = New Impresion
                                        Me.PrintObj = p.PrintObj
                                        '-----------------------------------------------------------------------------
                                        ' Por alg�n error en el programa siempre que se consulta la altura del lente en el objeto p (Formulario de Visualizaci�n del Trazo)
                                        ' la altura que regresa es menor que la altura que se proporcion�
                                        ' si se hace repetidamente la altura va disminuyendo hasta quedar un lente descentrado que sale de la pastilla
                                        ' Pedro Far�as Lozano  Nov 22 2012
                                        p.LenteD.CalculaContorno()
                                        AlturaLeft.Text = p.TextAlturaL.Text
                                        AlturaRight.Text = p.TextAlturaR.Text
                                        'variables de grosores
                                        GrosorCentralD = p.GrosorcentralD
                                        GrosorCentralI = p.GrosorcentralI
                                        PreviewForm = p                 ' Aqui le asigno los valores locales del preview local al preview global

                                        If p.Status Then
                                            'RxSave.Enabled = True
                                            Me.RxSave_Click(Me, e)
                                        Else
                                            RxSave.Enabled = False
                                        End If
                                        GetMaterialDesign(MaterialID, DesignID)
                                        LD = Nothing
                                        LI = Nothing
                                    Else
                                        MsgBox("Los materiales difieren, favor de checar las pastillas", MsgBoxStyle.Critical)
                                    End If
                                Else
                                    MsgBox("Error en el WebService", MsgBoxStyle.Critical)
                                End If
                            End If
                        End If

                    Catch ex As Exception
                        MsgBox(ex.Message, MsgBoxStyle.Critical)
                        RxSave.Enabled = True
                    End Try
                Else
                    MsgBox("No hay lentes seleccionados", MsgBoxStyle.Information)
                End If
            Else
                'If Not IsFrameOK Then
                If MyFrame.ReadStatus <> Frames.ThisFrameReadStatus.Ok Then
                    MsgBox("Debes leer un armaz�n v�lido para esta Rx", MsgBoxStyle.Exclamation)
                End If
            End If

            End If


        ' End If
    End Sub
    Public Function ConvertBaseCurveToRadius(ByVal Base As Single)
        Return Math.Round((530 / Base), 2)
    End Function
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
    Private Function Convertir512To400(ByVal RadiosToConvert As Integer()) As Integer()
        Dim NewRadios(399) As Integer
        Dim i, LimInf, LimSup As Integer

        Dim SqlCon As New SqlClient.SqlConnection(Laboratorios.ConnStr)
        Dim SqlRdr As SqlClient.SqlDataReader
        Try
            SqlCon.Open()
            Dim SqlStr As String = "SELECT ConvID, Point, LimInf, LimSup FROM dbo.OMAConvTable where ConvID = '512to400' order  by Point asc " 'and Point = " & i + 1 & ""
            Dim SqlCmd As New SqlClient.SqlCommand(SqlStr, SqlCon)
            SqlRdr = SqlCmd.ExecuteReader
            For i = 0 To 399
                'Dim SqlStr As String = "SELECT ConvID, Point, LimInf, LimSup FROM dbo.OMAConvTable where ConvID = '512to400' and Point = " & i + 1 & ""
                'Dim SqlCmd As New SqlClient.SqlCommand(SqlStr, SqlCon)
                'SqlRdr = SqlCmd.ExecuteReader
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

    '********************************************
    '*** FUNCION QUE REDONDEA YA SEA LA ESFERA O CILINDRO
    '*** AL FORMATO QUE SE REQUIERA PARA BUSCAR LA BASE EN LA DB. EJ. 1.85  ES IGUAL A 2.00
    Private Function Redondea(ByVal NumeroDecimal As String) As String
        Dim Numero As String = ""
        If NumeroDecimal = "" Then
            NumeroDecimal = 0
        End If
        Dim i As Double = Math.Round(CDbl(NumeroDecimal), 2)
        Dim ent As Integer = Math.Truncate(i)
        Dim Punto As Integer
        Dim Fraccion As String = ""
        Punto = InStr(i.ToString, ".", CompareMethod.Text)
        'extraemos la fraccion del numero si tiene
        If Punto > 0 Then
            Fraccion = CStr(i).Substring(Punto, CStr(i).Length - Punto)
            Fraccion = Fraccion.PadRight(2, "0")
            Select Case CInt(Fraccion)
                Case 0 To 25
                    Fraccion = "0.25"
                Case 26 To 50
                    Fraccion = "0.50"
                Case 51 To 75
                    Fraccion = "0.75"
                Case 76 To 99
                    Fraccion = "0.0"
                    If Math.Sign(i) = 1 Then
                        ent = ent + 1
                    Else
                        ent = ent - 1
                    End If
            End Select
            If Math.Sign(i) = 1 Then
                Numero = ent + CDbl(Fraccion)
            Else
                Numero = ent - CDbl(Fraccion)
            End If
        Else
            'EL NUMERO NO TIENE FRACCIONES ENTONCES EL NUMERO ES IGUAL
            Numero = NumeroDecimal
        End If
        Return Numero
    End Function
    '******************************************************************************************
    '*****Procedimiento que verifica nuevas ordenes entradas por internet****************
    Public Sub LoadInternetOrders()
        Dim ConSys As SqlDB
        Dim t As SqlDB
        Dim HideOrders As Boolean = False

        If WorkingOnLine Then
            ConSys = New SqlDB(My.Settings.VantageServer, My.Settings.DBUser, My.Settings.DBPassword, My.Settings.ERPDBName)
            HideOrders = True
        Else
            ConSys = New SqlDB(My.Settings.LocalServer, My.Settings.DBUser, My.Settings.DBPassword, My.Settings.LocalDBName)
        End If

        t = New SqlDB(My.Settings.LocalServer, My.Settings.DBUser, My.Settings.DBPassword, My.Settings.LocalDBName)

        If My.Settings.LocalDBName.ToLower = "locallab2000" Then
            t = New SqlDB(My.Settings.LocalServer, My.Settings.DBUser, My.Settings.DBPassword, My.Settings.LocalDBName)
        End If


        'If WorkingOnLine Then
        'Dim ConSys As New SqlDB(Read_Registry("MainServer"), My.Settings.DBUser, My.Settings.DBPassword, My.Settings.ERPDBName)
        Try
            ConSys.OpenConn()
            t.OpenConn()
            Dim DS As New DataSet
            Dim ds2 As DataSet
            Dim SqlStr As String
            Dim i As Integer = 0, j As Integer = 0
            If WorkingOnLine Then
                Dim PORecibidos As String = ""

                DS = t.SQLDS("select seqid,fecha from tblentradacostco with(nolock) where fecha >= getdate() - 7 order by fecha desc,seqid", "t1")
                If DS.Tables("t1").Rows.Count > 0 Then
                    For i = 0 To DS.Tables("t1").Rows.Count - 1
                        With DS.Tables("t1").Rows(i)
                            If i > 0 Then
                                PORecibidos &= ","
                            End If
                            PORecibidos &= .Item("seqid")
                        End With
                    Next
                End If
                If PORecibidos = "" Then
                    PORecibidos = 0
                End If
                SqlStr = "select Fecha, Custnum, LabID, OrdenLab,A, B, ED, Puente, Antireflejante, MaterialID, DisenoID, Tinte, EsferaR, CilindroR, EjeR, AdicionR," & _
                           " EsferaL, CilindroL, EjeL, AdicionL, Foco, AlturaR, MonoR, AlturaL, MonoL, DIPce, DIPle, Altura, NumArmazon, Contorno, " & _
                           " CapturaArmazon, Biselado, Gradiente, Status, Name, Abrillantado, ComentariosLab, CostcoLente, CostcoAR, IsGratis, CostcoPulido, CostcoTinte, CustID, orderdate, seqid, material, diseno, FechaEntradaWeb, FechaEntradaFisica, date02, date03, date04, FechaArInLocalLab, date06, date07, Optimizado,idFreeForm " & _
                           " from VwWebOrders where Status = 0 and (((len(ordenlab) = 10 or LabID = " & LabID() & " ) and (seqid in (" & PORecibidos & "))) or (len(ordenlab) < 10 and labid = " & LabID() & ")) order by len(ordenlab),ordenlab"
            Else
                SqlStr = "SELECT *, EsferaR as [Esf D], CilindroR as [Cil D], AdicionR as [Adic D], " & _
                         "EsferaL as [Esf I], CilindroL as [Cil I], AdicionL as [Adic I], " & _
                         "CASE antireflejante WHEN 0 THEN '' WHEN 1 THEN 'AUGEN' WHEN 2 THEN 'GOLD' WHEN 3 THEN 'MATIZ' END as [TipoAR], " & _
                         "ComentariosLab as [Comentarios] FROM VwMyWebOrders WITH(NOLOCK) ORDER BY len(ordenlab), ordenlab"
            End If

            ds2 = ConSys.SQLDS(SqlStr, "t1")
            ''MsgBox(ds2.Tables("t1").Rows(1).Item("seqid") & " - " & ds2.Tables("t1").Rows(1).Item("fechaentradafisica"))
            'For i = 0 To ds2.Tables("t1").Rows.Count - 1
            '    For j = 0 To DS.Tables("t1").Rows.Count - 1
            '        With ds2.Tables("t1").Rows(i)
            '            If .Item("seqid") = DS.Tables("t1").Rows(j).Item("seqid") Then
            '                .Item("fechaentradafisica") = DS.Tables("t1").Rows(j).Item("fecha")
            '            End If
            '        End With
            '    Next
            'Next

            DGCostco.DataSource = ds2.Tables("t1")

            'For i = 0 To DGCostco.Columns.Count - 1
            '    DGCostco.Columns(i).Visible = False
            'Next

            'DGCostco.Columns("ordenlab").Visible = True
            'DGCostco.Columns("name").Visible = True
            'DGCostco.Columns("material").Visible = True
            'DGCostco.Columns("diseno").Visible = True
            'DGCostco.Columns("esferar").Visible = True

            'If HideOrders Then
            '    DS = ConSys.SQLDS("select * from tblEntradaCOSTCO")    '''' Aqui buscar las Rx que si se han dado entrada y no mostrar las que no
            '    For i = 0 To DGCostco.RowCount - 1
            '        With DGCostco.Rows(i)
            '            If .Cells("OrdenLab") Then
            '        End With
            '    Next
            'End If
        Catch ex As Exception
            If ex.Message.Contains("Error de Conexion") Then
                ChangeWorkingStatus(WorkingStatusType.OffLine)
            Else
                '     MsgBox(ex.Message, MsgBoxStyle.Critical)
            End If
        Finally
            ConSys.CloseConn()
            t.CloseConn()
        End Try
        'End If
    End Sub
    '************************************************************************************
    '*******************funcion que regresa la clave del laboratorio para filtrar clientes*****
    Public Function LabID() As Integer
        Dim t As New SqlDB(My.Settings.LocalServer, My.Settings.DBUser, My.Settings.DBPassword, My.Settings.LocalDBName)
        Dim nds As New DataSet

        Try
            t.OpenConn()
            nds = t.SQLDS("Select cl_lab from tbllaboratorios where plant = '" & My.Settings.Plant & "'", "t1")
            If nds.Tables("t1").Rows.Count > 0 Then
                LabID = nds.Tables("t1").Rows(0).Item(0)
                Return LabID
            Else
                Throw New Exception("Error en la clave de la planta asignada a este laboratorio.")
            End If
        Catch ex As Exception
        Finally
            t.CloseConn()
        End Try
    End Function

    Public Function PlantID(ByVal cl_lab As Integer) As String
        Dim t As New SqlDB(My.Settings.LocalServer, My.Settings.DBUser, My.Settings.DBPassword, My.Settings.LocalDBName)
        Dim nds As New DataSet
        Try
            t.OpenConn()
            nds = t.SQLDS("Select plant from tbllaboratorios where cl_lab = " & cl_lab & " ", "t1")
            If nds.Tables("t1").Rows.Count > 0 Then
                PlantID = nds.Tables("t1").Rows(0).Item(0)
                Return PlantID
            Else
                Throw New Exception("Error en la clave de la planta asignada a este laboratorio.")
            End If
        Catch ex As Exception
            Throw New Exception("Error de conexion local.")
        Finally
            t.CloseConn()
        End Try
    End Function
    '******************************************************************************************
    '******** funcion que regresa la descripcion de la planta para desplegarla en el nombre de la forma
    Public Function PlantDescription() As String
        Dim plant As String = ""
        Dim Conn As SqlClient.SqlConnection = Nothing
        Dim DRdr As SqlClient.SqlDataReader = Nothing
        Dim Cmd As SqlClient.SqlCommand
        Try
            Conn = New SqlClient.SqlConnection
            If WorkingOnLine Then
                Conn.ConnectionString = My.Settings.ERPMasterConnection '"user ID=sa;password =proliant01;database=mfgsys80;server=AUGENSVR2;Connect Timeout=5"
            Else
                Conn.ConnectionString = Laboratorios.ConnStr
            End If
            Conn.Open()
            Cmd = New SqlClient.SqlCommand("SELECT Nombre, IsARLab FROM tbllaboratorios with(nolock) WHERE plant = '" & My.Settings.Plant & "' ", Conn)
            Cmd.CommandTimeout = My.Settings.DBCommandTimeout
            DRdr = Cmd.ExecuteReader
            If DRdr.HasRows Then
                While DRdr.Read
                    plant = DRdr("Nombre")
                    IsARLaboratory = DRdr("IsARLab")
                End While
            Else
                plant = "?"
            End If
            Cmd.Dispose()
            Cmd = Nothing
        Catch ex As Exception
            Return plant 'si hubo error regresamos vacia la variable y continuamo con la ejecu. del programa
        Finally
            If Not DRdr.IsClosed Then
                DRdr.Close()
            End If
            If Not Conn.State = ConnectionState.Closed Then
                Conn.Close()
            End If
        End Try
        Return plant
    End Function
    Public Sub CheckCostcoCustnum(ByVal DataS As DataSet, ByVal custnum As Integer)
        ' ********* AGREGO FRANCISCO PARA TOMAR LOS DATOS LOCALES O DEL SERVER DE LAS RX'S*************
        Dim t As SqlDB
        If WorkingOnLine Then
            t = New SqlDB(My.Settings.VantageServer, My.Settings.DBUser, My.Settings.DBPassword, My.Settings.ERPDBName)
        Else
            t = New SqlDB(My.Settings.LocalServer, My.Settings.DBUser, My.Settings.DBPassword, My.Settings.LocalDBName)
        End If
        '********** TERMINA FCO ***************

        'Dim t As New SqlDB(My.Settings.VantageServer, My.Settings.DBUser, My.Settings.DBPassword, My.Settings.ERPDBName)
        Dim ds As DataSet
        Dim Bodega As Integer
        'If DataS.Tables("t1").Rows(0).Item("ordenlab").ToString.Length() >= 10 And WorkingOnLine Then
        If DataS.Tables("t1").Rows(0).Item("ordenlab").ToString.Length() >= 10 Then
            Try
                Bodega = DataS.Tables("t1").Rows(0).Item("ordenlab").ToString.Substring(0, 3)
            Catch ex As Exception
                Bodega = 0
            End Try
            Try
                t.OpenConn()
                ds = t.SQLDS("select * from tblcostcotiendas where ID = " & Bodega, "t1")
                If ds.Tables("t1").Rows.Count > 0 Then
                    If LabID() = 34 Then
                        DataS.Tables("t1").Rows(0).Item("custnum") = ds.Tables("t1").Rows(0).Item("custnummex")
                    Else
                        DataS.Tables("t1").Rows(0).Item("custnum") = ds.Tables("t1").Rows(0).Item("custnumlocal")

                    End If
                End If
            Catch ex As Exception
                If ex.Message.Contains("Error de Conexion") Then
                    Me.ChangeWorkingStatus(WorkingStatusType.OffLine)
                Else
                    '                    MsgBox(ex.Message, MsgBoxStyle.Critical)
                End If
            Finally
                t.CloseConn()
            End Try

        End If
    End Sub

    Public Function CheckPORecibido(ByVal custnum As Integer, ByVal PO As String, ByVal SeqID As Integer) As Boolean
        Dim t As SqlDB

        t = New SqlDB(My.Settings.LocalServer, My.Settings.DBUser, My.Settings.DBPassword, My.Settings.LocalDBName)
        
        Dim ds As DataSet
        Dim SqlStr As String = "select * from tblEntradaCOSTCO with(nolock) where PO = '" & PO & "' and seqid = " & SeqID
        Try
            t.OpenConn()
            ds = t.SQLDS(SqlStr, "t1")
            If ds.Tables("t1").Rows.Count > 0 Then
                Return True
            Else
                SqlStr = "select custnum from customer with(nolock) where custnum = " & custnum & " and number06 = 1"
                ds = t.SQLDS(SqlStr, "t1")
                If ds.Tables("t1").Rows.Count > 0 Then
                    MsgBox("Este PO no ha sido recibido en la aplicaci�n de Entrada COSTCO." + vbCrLf + "Favor de registrarlo para poder procesarlo.", MsgBoxStyle.Critical)
                    Return False
                Else
                    Return True
                End If
            End If

        Catch ex As Exception
            Return False
        Finally
            t.CloseConn()
        End Try
    End Function

    Public Sub ShowCostcoRx()
        Try


            If DGCostco.RowCount > 0 Then
                RxType = RxTypes.WebRx
                IsManualRx = True
                Dim DATA As DataGridViewRow
                DATA = DGCostco.Rows(ind)
                Dim ds As New DataSet

                ds.Tables.Add("T1")
                ds.Tables("T1").Columns.Add("ordernum")     ' ????
                ds.Tables("T1").Columns.Add("Fecha")        ' 0
                ds.Tables("T1").Columns.Add("custnum")      ' 1
                ds.Tables("T1").Columns.Add("cliente")      ' 34
                ds.Tables("T1").Columns.Add("LabID")        ' 2
                ds.Tables("T1").Columns.Add("ordenlab")     ' 3
                ds.Tables("T1").Columns.Add("A")            ' 4
                ds.Tables("T1").Columns.Add("B")            ' 5
                ds.Tables("T1").Columns.Add("ED")           ' 6
                ds.Tables("T1").Columns.Add("Puente")       ' 7
                ds.Tables("T1").Columns.Add("Antireflejante") '8
                ds.Tables("T1").Columns.Add("MaterialID")   ' 9
                ds.Tables("T1").Columns.Add("Dise�oID")     ' 10
                ds.Tables("T1").Columns.Add("color")        ' ??????
                ds.Tables("T1").Columns.Add("tinte")        ' 11
                ds.Tables("T1").Columns.Add("esferar")      ' 12
                ds.Tables("T1").Columns.Add("cilindror")    ' 13
                ds.Tables("T1").Columns.Add("ejer")         ' 14
                ds.Tables("T1").Columns.Add("adicionr")     ' 15
                ds.Tables("T1").Columns.Add("esferal")      ' 16
                ds.Tables("T1").Columns.Add("cilindrol")    ' 17
                ds.Tables("T1").Columns.Add("ejel")         ' 18
                ds.Tables("T1").Columns.Add("adicionl")     ' 19
                ds.Tables("T1").Columns.Add("foco")         ' 20
                ds.Tables("T1").Columns.Add("alturar")      ' 21
                ds.Tables("T1").Columns.Add("monor")        ' 22
                ds.Tables("T1").Columns.Add("altural")      ' 23
                ds.Tables("T1").Columns.Add("monol")        ' 24
                ds.Tables("T1").Columns.Add("dipce")        ' 25
                ds.Tables("T1").Columns.Add("diple")        ' 26
                ds.Tables("T1").Columns.Add("altura")       ' 27
                'ds.Tables("T1").Columns.Add("numarmazon")   ' 28
                ds.Tables("T1").Columns.Add("ArmazonID")   ' 28
                ds.Tables("T1").Columns.Add("contorno")     ' 29
                ds.Tables("T1").Columns.Add("capturaarmazon") '30
                ds.Tables("T1").Columns.Add("biselado")     ' 31
                ds.Tables("T1").Columns.Add("gradiente")    ' 32
                ds.Tables("T1").Columns.Add("status")       ' 33
                ds.Tables("T1").Columns.Add("RxLado")
                ds.Tables("T1").Columns.Add("partdescription")
                ds.Tables("T1").Columns.Add("partnum")
                ds.Tables("T1").Columns.Add("RxClase")
                ds.Tables("T1").Columns.Add("RefDigital")
                ds.Tables("T1").Columns.Add("Abrillantado")
                ds.Tables("T1").Columns.Add("ComentariosLab")
                ds.Tables("T1").Columns.Add("CostcoLente")
                ds.Tables("T1").Columns.Add("CostcoAR")
                ds.Tables("T1").Columns.Add("IsGratis")
                ds.Tables("T1").Columns.Add("CostcoPulido")
                ds.Tables("T1").Columns.Add("CostcoTinte")
                ds.Tables("T1").Columns.Add("Orderdate")
                ds.Tables("T1").Columns.Add("SeqID")
                ds.Tables("T1").Columns.Add("FechaEntradaWeb")
                ds.Tables("T1").Columns.Add("FechaEntradaFisica")
                ds.Tables("T1").Columns.Add("FechaArInLocalLab")
                ds.Tables("T1").Columns.Add("date03")
                ds.Tables("T1").Columns.Add("date04")
                ds.Tables("T1").Columns.Add("date05")
                ds.Tables("T1").Columns.Add("date06")
                ds.Tables("T1").Columns.Add("date07")
                ds.Tables("T1").Columns.Add("Optimizado")
                ds.Tables("T1").Columns.Add("paquete")
                'ds.Tables("T1").Columns.Add("idFreeForm")

                'ds.Tables("T1").Columns.Add("Checklefteye")
                'ds.Tables("T1").Columns.Add("Checkrighteye")

                ds.Tables("T1").Rows.Add(0, DATA.Cells("Fecha").Value.ToString, DATA.Cells("Custnum").Value.ToString, DATA.Cells("Name").Value.ToString, _
                                        DATA.Cells("LabID").Value.ToString, DATA.Cells("OrdenLab").Value.ToString, _
                                        DATA.Cells("A").Value.ToString, DATA.Cells("B").Value.ToString, _
                                        DATA.Cells("ED").Value.ToString, DATA.Cells("Puente").Value.ToString, _
                                        DATA.Cells("Antireflejante").Value.ToString, DATA.Cells("MaterialID").Value.ToString, _
                                        DATA.Cells("DisenoID").Value.ToString, 0, DATA.Cells("Tinte").Value.ToString, _
                                        DATA.Cells("EsferaR").Value.ToString, DATA.Cells("CilindroR").Value.ToString, _
                                        DATA.Cells("EjeR").Value.ToString, DATA.Cells("AdicionR").Value.ToString, _
                                        DATA.Cells("EsferaL").Value.ToString, DATA.Cells("CilindroL").Value.ToString, _
                                        DATA.Cells("EjeL").Value.ToString, DATA.Cells("AdicionL").Value.ToString, _
                                        DATA.Cells("Foco").Value.ToString, DATA.Cells("AlturaR").Value.ToString, _
                                        DATA.Cells("MonoR").Value.ToString, DATA.Cells("AlturaL").Value.ToString, _
                                        DATA.Cells("MonoL").Value.ToString, DATA.Cells("DIPce").Value.ToString, _
                                        DATA.Cells("DIPle").Value.ToString, DATA.Cells("Altura").Value.ToString, _
                                        DATA.Cells("NumArmazon").Value.ToString, DATA.Cells("Contorno").Value.ToString, _
                                        DATA.Cells("CapturaArmazon").Value.ToString, DATA.Cells("Biselado").Value.ToString, _
                                        DATA.Cells("Gradiente").Value.ToString, DATA.Cells("Status").Value.ToString, _
                                        "2", "", "", "", 0, DATA.Cells("Abrillantado").Value.ToString, DATA.Cells("ComentariosLab").Value.ToString, _
                                        DATA.Cells("CostcoLente").Value.ToString, DATA.Cells("idFreeForm").Value.ToString, _
                                        DATA.Cells("IsGratis").Value.ToString, DATA.Cells("CostcoPulido").Value.ToString, DATA.Cells("CostcoTinte").Value.ToString, _
                                        DATA.Cells("orderdate").Value.ToString, DATA.Cells("SeqID").Value.ToString, DATA.Cells("FechaEntradaWeb").Value.ToString, _
                                        DATA.Cells("FechaEntradaFisica").Value.ToString, DATA.Cells("date02").Value.ToString, DATA.Cells("date03").Value.ToString, _
                                        DATA.Cells("date04").Value.ToString, DATA.Cells("FechaArInLocalLab").Value.ToString, DATA.Cells("date06").Value.ToString, _
                                        DATA.Cells("date07").Value.ToString, DATA.Cells("Optimizado").Value.ToString, DATA.Cells("paquete").Value.ToString)

                'DATA.Cells("CostcoAR").Value.ToString

                ds.Tables("T1").Rows.Add("", "", "", "", "", "", "", "", "", "", _
                                        "", "", "", "", "", "", "", "", "", "", _
                                        "", "", "", "", "", "", "", "", "", "", _
                                        "", "", "", "", "", "", "", "2", "", "", "", 0, DATA.Cells("Abrillantado").Value.ToString, _
                                        DATA.Cells("ComentariosLab").Value.ToString, DATA.Cells("CostcoLente").Value.ToString, DATA.Cells("idFreeForm").Value.ToString, _
                                        DATA.Cells("IsGratis").Value.ToString, DATA.Cells("CostcoPulido").Value.ToString, DATA.Cells("CostcoTinte").Value.ToString, DATA.Cells(43).Value.ToString, DATA.Cells("SeqID").Value.ToString, DATA.Cells("FechaEntradaWeb").Value.ToString, DATA.Cells("FechaEntradaFisica").Value.ToString, DATA.Cells("date02").Value.ToString, DATA.Cells("date03").Value.ToString, _
                                        DATA.Cells("date04").Value.ToString, DATA.Cells("FechaArInLocalLab").Value.ToString, DATA.Cells("date06").Value.ToString, _
                                        DATA.Cells("date07").Value.ToString, DATA.Cells("Optimizado").Value.ToString, DATA.Cells("paquete").Value.ToString)

                'Preview.Enabled = False
                CheckCostcoCustnum(ds, ds.Tables("T1").Rows(0).Item("Custnum"))
                If Not (CheckPORecibido(ds.Tables("T1").Rows(0).Item("Custnum"), ds.Tables("T1").Rows(0).Item("OrdenLab"), ds.Tables("T1").Rows(0).Item("SeqID"))) Then
                    InitValues()
                Else
                    OrderExists = False
                    FillRx(ds, False)
                    '  grouptrazos.Visible = False
                    RxNum.Visible = False
                    RxID.Text = DATA.Cells(3).Value.ToString
                    'Label6.Visible = False
                    'Label5.Visible = False
                    RxWECO.Visible = False
                    PanelOpciones.Enabled = True
                    PanelOpciones.Visible = True
                    PanelArmazon.Enabled = True
                    RxID.Enabled = False
                    ComboClients.Enabled = False
                    PanelInformacion.Visible = False
                    'RxID_Leave(Me, New EventArgs)
                    AdicionLeft_Leave(Me, New EventArgs)
                    AdicionRight_Leave(Me, New EventArgs)

                    RadioDigitalNew.Checked = True
                    'GroupTrazos.Visible = True

                    ' **************************************************************************************************************************
                    ' Se buscara el trazo de un trabajo de internet que tenga un numero de armazon en el campo framenum de la tabla tblweborders
                    ' **************************************************************************************************************************
                    'Dim NumArmazon As Integer = CType(DATA.Cells("NumArmazon").Value, Integer)
                    IsFrameOK = True
                    NumArmazon = CType(DATA.Cells("NumArmazon").Value, Integer)
                    'Dim bridge As Integer = CType(DATA.Cells("Puente").Value, Integer)
                    Dim contorno As Integer = CType(DATA.Cells("Puente").Value, Integer)
                    numpaq = DATA.Cells("Paquete").Value.ToString

                    MyFrame = New Frames(NumArmazon, Frames.ThisFrameStatus.[New])
                    MyFrame.ReadStatus = Frames.ThisFrameReadStatus.Ok

                    If numpaq.Length > 0 Then
                        MyFrame.NumPaquete = numpaq
                    Else
                        MyFrame.NumPaquete = 0
                    End If

                    If (MyFrame.NumArmazon > 0) Then

                        If Me.GetFrameTrace(NumArmazon).Length > 0 Then
                            If MyFrame.CheckInventoryFrameFamily() Then
                                'If CheckInventoryFrameFamily(NumArmazon) Then
                                PNL_Armazon.Visible = True
                                PIC_Armazon.Image = My.Resources.Lens_Over_2
                                'CheckFrameStatus(NumArmazon)
                                MyFrame.CheckFrameStatus()
                                TBX_FrameDescription.Text = MyFrame.ArmazonDescripcion
                                IsFrameOK = False
                                MyFrame.ReadStatus = Frames.ThisFrameReadStatus.NotRead
                            End If
                            'Me.GetRadiosFromDatabase(RxWECO.Text)
                            Dim MyRadios() As Integer = Me.GetOMAFrameRadios(RxWECO.Text)

                            'Dim radios() As Integer = Me.Convertir400To512(RadiosL)
                            Me.SaveDVITrace(ContornoPuente.Text, MyRadios)
                        ElseIf MyFrame.NumArmazon.ToString.Length > 5 Then
                            MyFrame.FrameStatus = Frames.ThisFrameStatus.NoFrame
                        End If
                    Else
                        PNL_Armazon.Visible = False
                    End If
                    'If Not IsFrameOK Then
                    If MyFrame.ReadStatus = Frames.ThisFrameReadStatus.NotRead Then
                        PIC_Armazon.Image = My.Resources.Lens_Error_2
                    Else
                        PIC_Armazon.Image = Nothing
                    End If

                    BuscarTrazos_Click(Me, New EventArgs)
                    ' AceptarDigital_Click(Me, New EventArgs)

                    'PanelInformacion.Enabled = True
                    'PanelInformacion.Visible = True
                    If ComboTrazos.ComboBox1.Items.Count = 0 Then
                        RadioManualNew.Checked = True
                    End If
                    Cancel.Enabled = True

                    'AceptarManual_Click(Me, New EventArgs)


                End If

            End If

        Catch ex As Exception

        End Try

    End Sub




    ' Obtiene el numero de trazo para el armaz�n de la receta de internet
    Private Function GetFrameTrace(ByVal numarm As String) As String
        Dim Mensaje As String = ""
        Dim JobNum As String = RxID.Text

        Try
            ' Obtengo los ultimos 4 digitos de la orden de laboratorio
            '---------------------------------------------------------
            JobNum = JobNum.PadLeft(4, "0")
            JobNum = JobNum.Substring(JobNum.Length - 4, 4)

            Dim Conn As New SqlDB(My.Settings.LocalServer, My.Settings.DBUser, My.Settings.DBPassword, My.Settings.LocalDBName)
            Dim Comm As SqlCommand


            '' Entra a esta seccion si existe un trazo en OMAStoredTraces para el numero de armazon del trabajo
            'If (ds.Tables("t1").Rows.Count > 0) Then
            Conn.OpenConn()
            Comm = New SqlCommand("SP_CopiarTrazoOMA", Conn.SQLConn)
            Comm.CommandType = Data.CommandType.StoredProcedure

            Comm.Parameters.AddWithValue("@FrameNum", numarm)
            Comm.Parameters.AddWithValue("@RxNum", JobNum)
            Comm.Parameters.Add("@Job", Data.SqlDbType.VarChar, 100)
            Comm.Parameters("@Job").Direction = Data.ParameterDirection.Output

            Comm.ExecuteNonQuery()

            Mensaje = Comm.Parameters("@Job").Value

            If Mensaje.Length = 0 Then Return Mensaje

            RxWECO.Text = Mensaje
            RxWECO.Visible = True
            Conn.CloseConn()

        Catch ex As Exception
            MessageBox.Show(ex.Message)

        End Try

        Return Mensaje

    End Function

    Public Function Ping(ByVal Server As String) As Boolean
        Dim t As New SqlDB(Server, My.Settings.DBUser, My.Settings.DBPassword, "master")

        Try
            t.OpenConn()
            If Server = My.Settings.VantageServer And My.Settings.WorkingMode.ToUpper = "ONLINE" Then
                ChangeWorkingStatus(WorkingStatusType.OnLine)
            End If
            Return True
        Catch ex As Exception
            If ex.Message.Contains("Error de Conexion") And Server = My.Settings.VantageServer Then
                ChangeWorkingStatus(WorkingStatusType.OffLine)
            End If
            Return False
        Finally
            t.CloseConn()
        End Try
    End Function

    ' FUNCION QUE VERIFICA LA CONEXION CON EL SERVIDOR PRINCIPAL DE TRECEUX
    Public Function ChooseServer() As String
        Dim t As New SqlDB(My.Settings.VantageServer, My.Settings.DBUser, My.Settings.DBPassword, My.Settings.MfgSysDBName)

        Try
            t.OpenConn()
            ServerAdd = My.Settings.VantageServer
            DataBase = My.Settings.MfgSysDBName
        Catch ex As Exception
            If ex.Message.Contains("Error de Conexion") Then
                Me.ChangeWorkingStatus(WorkingStatusType.OffLine)
                ServerAdd = My.Settings.LocalServer
                DataBase = My.Settings.LocalDBName
            End If
        Finally
            t.CloseConn()
            If My.Settings.WorkingMode.ToUpper = "OFFLINE" Then
                ServerAdd = My.Settings.LocalServer
                DataBase = My.Settings.LocalDBName
            End If
        End Try

        Return ServerAdd
    End Function

    Private Function ValidateLiverpoolPO(ByVal PONumber As String) As Boolean
        Dim result As Boolean = False
        Dim Failed As Boolean = False
        Dim FailedMessage As String = ""
        If PONumber.Length = My.Settings.LiverpoolRxLength Then
            result = True
        Else
            result = False
        End If
        If result Then
            Dim t As SqlDB
            Dim ds As New DataSet
            t = New SqlDB() '(My.Settings.VantageServer, My.Settings.DBUser, My.Settings.DBPassword, My.Settings.MfgSysDBName)
            t.SQLConn = New SqlConnection(Laboratorios.ConnStr)
            Try
                t.OpenConn()
                ds = t.SQLDS("select * from orderhed with(nolock) where shortchar01 like '" & PONumber & "%' and company = 'TRECEUX'", "t1")
                If ds.Tables("t1").Rows.Count > 0 And Not IsGarantia Then
                    result = False
                    Throw New Exception("Este ID de Liverpool ya ha sido registrado en el sitema anteriormente.")
                End If
            Catch ex As Exception
                Failed = True
                FailedMessage = ex.Message
            Finally
                t.CloseConn()
                If Failed Then
                    If FailedMessage.Contains("Error de Conexion") Then
                        Me.ChangeWorkingStatus(WorkingStatusType.OffLine)
                    End If
                End If
            End Try
        End If
        Return result
    End Function

    Private Function ValidatePO(ByVal PONumber As String) As Boolean
        Dim result As Boolean = False
        If PONumber.Length = My.Settings.CostcoRxLength Then
            Dim COSTCOID As Integer = PONumber.Substring(0, 3)
            Dim Mes As Integer = PONumber.Substring(3, 2)
            Dim Dia As Integer = PONumber.Substring(5, 2)
            Dim ID As String = PONumber.Substring(7, 3)
            Dim t As New SqlDB(My.Settings.LocalServer, My.Settings.DBUser, My.Settings.DBPassword, My.Settings.LocalDBName)
            Try
                t.OpenConn()
                Dim ds As DataSet = t.SQLDS("select * from tblcostcotiendas where id = " & COSTCOID, "t1")
                If ds.Tables("t1").Rows.Count = 0 Then
                    result = False
                Else
                    If IsGarantia Then
                        result = True
                    Else
                        Dim mydate As Date = Now.Date
                        If Mes = mydate.Month Then
                            result = True
                        Else
                            mydate = mydate.AddMonths(-1)
                            If Mes = mydate.Month Then
                                result = True
                            Else
                                mydate = mydate.AddMonths(-1)
                                If Mes = mydate.Month Then
                                    result = True
                                Else

                                    result = False
                                End If
                            End If
                        End If
                        If result Then
                            If Dia > 0 And Dia < 32 Then
                                result = True
                            Else
                                result = False
                            End If
                        End If
                    End If
                End If
            Catch ex As Exception
                result = False
            Finally
                t.CloseConn()
            End Try
            If result And WorkingOnLine Then
                t = New SqlDB(My.Settings.VantageServer, My.Settings.DBUser, My.Settings.DBPassword, My.Settings.MfgSysDBName)
                Try
                    t.OpenConn()
                    Dim ds As DataSet = t.SQLDS("select * from orderhed with(nolock) where shortchar01 like '" & PONumber & "%' and company = 'TRECEUX' and shipcomment like '" & Now.Date.Year & "'", "t1")
                    If ds.Tables("t1").Rows.Count > 0 And Not IsGarantia Then
                        result = False
                        MsgBox("El PO de COSTCO ya ha sido registrado en el sitema anteriormente.", MsgBoxStyle.Exclamation)
                    End If
                Catch ex As Exception
                    If ex.Message.Contains("Error de Conexion") Then
                        Me.ChangeWorkingStatus(WorkingStatusType.OffLine)
                    End If
                Finally
                    t.CloseConn()

                End Try
            End If
        Else
            If IsGarantia And PONumber.Length = My.Settings.CostcoRxLength Then
                result = True
            Else
                result = False
            End If
        End If
        Return result
    End Function

    Private Sub RxID_Leave(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RxID.Leave
        Dim test As Integer
        Dim temp As String = RxID.Text
        temp = temp.Replace(" ", "*")
        If RxID.Text <> "" And Not Me.IsReceivingVirtualRx Then
            Try
                test = temp
                If RxVirtual = False Then BuscarTrazos_Click(Me, e)
                ComboClients.Select()
                ERR_Errores.SetError(RxID, "")
                AceptarManualNew.Enabled = True
                AceptarDigitalNew.Enabled = True
            Catch ex As Exception
                AceptarManualNew.Enabled = False
                AceptarDigitalNew.Enabled = False
                ERR_Errores.SetError(RxID, "Error en numero de Rx, debe ser num�rico")
                RxID.SelectAll()
                'RxID.Text = ""
            End Try

        End If
    End Sub

    Public Sub Update_Virtuales_WebOrders(ByVal sender As System.Object, ByVal e As System.EventArgs)
        'Ping()
        Dim DGRow As DataGridViewRow
        If (Not PanelInformacion.Visible) Then
            DateTag.Text = Now.Date
            LoadInternetOrders()
            If WO.IsDisposed Then
                WO = New WebOrders
                'WO.Location = New Point(700, 400)
            End If
            If DGCostco.RowCount > 0 Then
                WO.DGCostco.DataSource = DGCostco.DataSource
                WO.Show()
            Else
                WO.Hide()
            End If
            If WorkingOnLine Or (Ping(My.Settings.VantageServer)) Then
                Dim t As New SqlDB(My.Settings.VantageServer, My.Settings.DBUser, My.Settings.DBPassword, My.Settings.MfgSysDBName)
                Dim t2 As New SqlDB(My.Settings.LocalServer, My.Settings.DBUser, My.Settings.DBPassword, My.Settings.LocalDBName)
                Dim i, j As Integer
                Try
                    t.OpenConn()
                    t2.OpenConn()
                    Dim ds As New DataSet
                    Dim ds2 As New DataSet
                    Dim Eliminar As Boolean
                    Dim sqlstr As String = "select cast(number01 as int) as Rx,cast(ordernum as int) as ordernum, (select nombre from ERPMaster.dbo.TblLaboratorios where cl_lab like substring(cast(ordernum as varchar(9)),1,2)) as Laboratorio from orderhed with(nolock) where checkbox09 = 1 and checkbox10 = 0 and character01 like '" & LabID() & "' and openorder = 1 order by number01"
                    sqlstr = "SELECT     CAST(orderhed.ordernum AS int) AS ordernum, ERPMaster.dbo.TblLaboratorios.Nombre AS Laboratorio, ERPMaster.dbo.TblMaterials.material, " & _
                        "ERPMaster.dbo.TblDesigns.diseno, CAST(orderhed.number01 AS int) AS Rx, CAST(orderhed.number02 AS decimal(5, 2)) AS [Esf D], " & _
                        "CAST(orderhed.number03 AS decimal(5, 2)) AS [Cil D], CAST(orderhed.number05 AS decimal(5, 2)) AS [Adic D], CAST(orderhed.number06 AS decimal(5,2)) AS [Esf I], " & _
                        "CAST(orderhed.number07 AS decimal(5, 2)) AS [Cil I], CAST(orderhed.number09 AS decimal(5, 2)) AS [Adic I], " & _
                        "CASE orderhed.shortchar06 WHEN 0 THEN '' WHEN 1 THEN 'AUGEN' WHEN 2 THEN 'GOLD' WHEN 3 THEN 'MATIZ' END AS AR, " & _
                        "orderhed.character05 AS Comentarios, orderhed.checkbox13 as Reproceso " & _
                        "FROM         orderhed WITH (nolock) INNER JOIN " & _
                        "ERPMaster.dbo.TblLaboratorios WITH (nolock) ON LEFT(orderhed.ordernum, 2) = ERPMaster.dbo.TblLaboratorios.cl_lab INNER JOIN " & _
                        "ERPMaster.dbo.TblMaterials WITH (nolock) ON orderhed.shortchar07 = ERPMaster.dbo.TblMaterials.cl_mat INNER JOIN " & _
                        "ERPMaster.dbo.TblDesigns WITH (nolock) ON orderhed.shortchar08 = ERPMaster.dbo.TblDesigns.cl_diseno " & _
                            "WHERE     (orderhed.checkbox09 = 1) AND (orderhed.checkbox10 = 0) AND (orderhed.character01 LIKE '" & Me.LabID & "') AND (orderhed.openorder = 1) " & _
                            "ORDER BY material, diseno"
                    ds = t.SQLDS(sqlstr, "t1")

                    ' PFL Nota : Forma ineficiente de exclu�r las �rdenes que ya fueron bajadas, pero que por problemas de sincronizaci�n en el servidor
                    ' remoto siguen con el checkbox10 en 0 ( No recibidas en lab AR )
                    sqlstr = "select ordernum from orderhed with(nolock) where orderdate>getdate()-30 AND checkbox09=1 order by userdate1 desc"
                    ds2 = t2.SQLDS(sqlstr, "t1")
                    With ds.Tables("t1")
                        For i = 0 To .Rows.Count - 1
                            'PFL - S�lo eliminamos las �rdenes virtuales que no son reproceso 
                            If .Rows(i).Item("Reproceso") = 0 Then
                                Eliminar = False
                                For j = 0 To ds2.Tables("t1").Rows.Count - 1
                                    If .Rows(i).Item("Ordernum") = ds2.Tables("t1").Rows(j).Item("Ordernum") Then
                                        Eliminar = True
                                        Exit For 'Ya encontramos un match, ya no seguimos todo el ciclo.
                                    End If
                                Next
                                If Eliminar Then
                                    .Rows(i).Item("Rx") = 0
                                    .Rows(i).Item("ordernum") = 0
                                    .Rows(i).Item("Laboratorio") = "PROCESADA"
                                End If
                            End If
                        Next
                    End With
                    If AL.IsDisposed Then
                        AL = New ARList(ds)
                    Else
                        AL.GridARList.DataSource = ds.Tables("t1")
                    End If

                    ' PFL Abril 9 2013
                    ' Marcamos las l�neas que corresponden a �rdenes de reproceso con un color de fondo diferente
                    If ds.Tables("t1").Rows.Count > 0 Then
                        'AL.Location = New Point(Me.Location.X + Panel1.Location.X, Me.Location.Y + WO.Size.Height + 25)
                        AL.Show()
                        For Each DGRow In AL.GridARList.Rows
                            If DGRow.Cells("Reproceso").Value = 1 Then DGRow.DefaultCellStyle.BackColor = Color.LightYellow
                        Next

                    Else
                        AL.Hide()
                    End If

                Catch ex As Exception
                    If ex.Message.Contains("Error de Conexion") Then
                        Me.ChangeWorkingStatus(WorkingStatusType.OffLine)
                    Else
                        '    MsgBox(ex.Message, MsgBoxStyle.Critical)
                    End If
                    AL.Hide()
                Finally
                    t.CloseConn()
                    t2.CloseConn()
                End Try

            End If
        End If
    End Sub

    Public Sub btnselec_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        If PanelInformacion.Visible = True Then
            If MsgBox("Estas seguro de mostrar esta receta?" & vbCrLf & "La informaci�n capturada se perder�.", MsgBoxStyle.OkCancel + MsgBoxStyle.Exclamation) = MsgBoxResult.Ok Then
                InitValues()
                ShowCostcoRx()
            End If
        Else
            ShowCostcoRx()
        End If
    End Sub

    Private Sub DGCostco_CellContentClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs)
        ind = e.RowIndex
    End Sub

    Private Sub EsferaRight_Enter(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MonoRight.Enter, MonoLeft.Enter, EsferaRight.Enter, EsferaLeft.Enter, DIPLejos.Enter, DIPCerca.Enter, ContornoPuente.Enter, ContornoED.Enter, ContornoB.Enter, ContornoA.Enter, ComboArmazon2.Enter, CilindroRight.Enter, CilindroLeft.Enter, AlturaRight.Enter, AlturaLeft.Enter, Altura.Enter, AdicionRight.Enter, AdicionLeft.Enter
        Try
            Dim tb As TextBox = sender
            tb.SelectAll()
        Catch ex As Exception
        End Try
        RxSave.Enabled = False

        PanelLeftEye.BackgroundImage = My.Resources.VistaLeftEyePanel
        PanelRightEye.BackgroundImage = My.Resources.VistaRightEyePanel
    End Sub

    Private Sub ComboMonofocal_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ComboMonofocal.SelectedIndexChanged

        If ComboMonofocal.SelectedValue.ToString <> "0" And InitStatus = InitStatusType.Working Then
            ComboBifocal.SelectedValue = "0"
            ComboProgresivo.SelectedValue = "0"
            ComboOtros.SelectedValue = "0"
            BuscarLentes_Click(sender, e)
            RxSave.Enabled = True
        End If
    End Sub

    Private Sub ComboBifocal_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ComboBifocal.SelectedIndexChanged
        If ComboBifocal.SelectedValue.ToString <> "0" And InitStatus = InitStatusType.Working Then
            ComboMonofocal.SelectedValue = "0"
            ComboProgresivo.SelectedValue = "0"
            ComboOtros.SelectedValue = "0"
            BuscarLentes_Click(Me, e)
        End If
    End Sub

    Private Sub ComboProgresivo_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ComboProgresivo.SelectedIndexChanged
        If ComboProgresivo.SelectedValue.ToString <> "0" And InitStatus = InitStatusType.Working Then
            ComboMonofocal.SelectedValue = "0"
            ComboBifocal.SelectedValue = "0"
            ComboOtros.SelectedValue = "0"
            BuscarLentes_Click(Me, e)
        End If
    End Sub

    Private Sub ComboOtros_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ComboOtros.SelectedIndexChanged
        If ComboOtros.SelectedValue.ToString <> "0" And InitStatus = InitStatusType.Working Then
            ComboMonofocal.SelectedValue = "0"
            ComboBifocal.SelectedValue = "0"
            ComboProgresivo.SelectedValue = "0"
            BuscarLentes_Click(Me, e)
        End If
    End Sub
    Public Sub Ping()
        If Not PingWorker.IsBusy Then
            PingWorker.RunWorkerAsync()
        End If
    End Sub

    Private Sub PingTimer_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PingTimer.Tick
        'If Ping(Read_Registry("ServerAdd")) Then
        '    ChangeWorkingStatus(WorkingStatusType.OnLine)
        'End If
        '        MyThread.Start()
        'PingThread.Start()
        PingTimer.Enabled = False
        Ping()
        If Not WorkingOnLine Then
            PingTimer.Enabled = True
        End If
        'Ping(Read_Registry("ServerAdd"))
        'BackgroundWorker1_DoWork(sender, New System.ComponentModel.DoWorkEventArgs)
    End Sub

    Private Sub DGCostco_DoubleClick(ByVal sender As System.Object, ByVal e As System.EventArgs)
        ind = DGCostco.CurrentRow.Index
        btnselec_Click(Me, e)
    End Sub

    Private Sub EntradaMaterial_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles EntradaMaterial.Click
        'Entrada de Material
        Dim rec As New Recibo
        rec.ShowDialog()
    End Sub
    Sub GetValuesFromOMALabel(ByVal label As String, ByRef Right As String, ByRef Left As String)
        Dim tl As String
        Dim tr As String
        tr = label.Substring(label.IndexOf("=") + 1, label.IndexOf(";") - (label.IndexOf("=") + 1))
        tl = label.Substring(label.IndexOf(";") + 1)
        If tr.Length = 0 And tl.Length > 0 Then
            tr = tl
        ElseIf tl.Length = 0 And tr.Length > 0 Then
            tl = tr
        End If
        Right = tr
        Left = tl
    End Sub
    Sub GetValuesFromOMALabel(ByVal label As String, ByRef value As String)
        value = label.Substring(label.IndexOf("=") + 1)
        If value.Length = 0 Then
            value = "?"
        End If
    End Sub
    Sub GetRadiosFromDatabase(ByVal RefWECO As String)
        Dim t As SqlDB
        Dim ds As New DataSet
        Dim Offset As Integer
        Dim SQLString As String

        '**********************************************
        '*** Variables que guardan el valor max de radios por cuadrante para dibujar el trazo
        Dim TotalTrazos, MedioCuadrante As Integer
        If My.Settings.OpticalProtocol = "DVI" Then
            TotalTrazos = 511
            MedioCuadrante = 255
        End If
        '**********************************************
        RefWECO = Me.StuffRx(RefWECO)

        If FrameNum <> "0" And My.Settings.GetCOSTCOTraces Then
            t = New SqlDB(My.Settings.VantageServer, My.Settings.DBUser, My.Settings.DBPassword, My.Settings.ERPDBName)
            SQLString = "select * from VwTracerCotscoItems with(nolock) where framenum = '" & FrameNum & "'"
            Offset = 10
        Else
            t = New SqlDB(My.Settings.LocalServer, My.Settings.DBUser, My.Settings.DBPassword, My.Settings.LocalDBName)
            If (IsModifying Or IsLocalReceive) And Not ChangingTraces Then
                SQLString = "select * from " & VwConsultaTrazos & " with(nolock) where jobnum = '" & RefWECO & "'"  ' and status = 0"
            Else
                SQLString = "select * from " & VwConsultaTrazos & " with(nolock) where jobnum = '" & RefWECO & "' and status = 1"
            End If


            If (My.Settings.Plant = "VBENS") Then
                SQLString = "select * from VwOMATraces with(nolock) INNER JOIN TblWebOrders ON VwOMATraces.Jobnum = TblWebOrders.framenum   WHERE TblWebOrders.ponumber = '" & RxID.Text & "' and custnum in (" + My.Settings.vbCustomer + ")"
                'select VwOMATraces.* from VwOMATraces with(nolock) INNER JOIN TblWebOrders ON VwOMATraces.Jobnum = TblWebOrders.framenum whERE TblWebOrders.ponumber =              '15950' and custnum in (4332,4784)
            End If

            If (My.Settings.Plant = "augenlab") Then
                SQLString = "select * from VwOMATraces with(nolock) INNER JOIN TblWebOrders ON VwOMATraces.Jobnum = TblWebOrders.framenum   WHERE TblWebOrders.ponumber = '" & RxID.Text & "'"
                'select VwOMATraces.* from VwOMATraces with(nolock) INNER JOIN TblWebOrders ON VwOMATraces.Jobnum = TblWebOrders.framenum whERE TblWebOrders.ponumber =              '15950' and custnum in (4332,4784)
            End If
            Offset = 13
        End If
        Dim i As Integer
        Try
            t.OpenConn()
            ds = t.SQLDS(SQLString, "t1") ' and (DATEPART(dayofyear, fecha) = DATEPART(dayofyear, GETDATE())) AND (DATEPART(year, fecha)= DATEPART(year, GETDATE()))", "t1")
            'EL VALOR DE PUENTE EN EL PROTOCOLO OMA YA VIENE EN MILIMETROS A DIFERENCIA DE DVI
            Select Case My.Settings.OpticalProtocol
                Case "OMA"
                    With ds.Tables("t1").Rows(0)
                        ContornoPuente.Text = (CSng(ds.Tables("t1").Rows(0).Item("dbl"))).ToString
                        If My.Settings.HasMEIEdger Then
                            Dim x As String = ""
                            Dim right As String = ""
                            Dim left As String = ""
                            Dim temp As String = ""
                            OMA.SetJOB(.Item("jobnum"))

                            If (My.Settings.Plant = "VBENS") Then
                                RxWECO.Text = Microsoft.VisualBasic.Right(RxID.Text, 4)
                                RxWECO.Text = RxWECO.Text + "00"
                                OMA.SetJOB(RxID.Text)
                            End If

                            OMA.SetDBL(.Item("dbl"))

                            x = .Item("lado") : OMA.SetDO(OMAFile.EyeSides.Both)
                            x = .Item("sizing") / 100 : OMA.SetBSIZ(x, x)
                            x = .Item("FBOCIN") : If x.Length > 0 Then GetValuesFromOMALabel(x, right, left) : OMA.SetFBOCIN(right, left)

                            x = .Item("FBSGUP") : If x.Length > 0 Then GetValuesFromOMALabel(x, right, left) : OMA.SetFBSGUP(right, left)
                            x = .Item("SWIDTH") : If x.Length > 0 Then GetValuesFromOMALabel(x, right, left) : OMA.SetSWIDTH(right, left)
                            x = .Item("SWIDTH") : If x.Length > 0 Then GetValuesFromOMALabel(x, right, left) : OMA.SetSWIDTH(right, left)
                            x = .Item("LTYP") : If x.Length > 0 Then GetValuesFromOMALabel(x, right, left) : OMA.SetLTYP(right, left)
                            x = .Item("IPD") : If x.Length > 0 Then GetValuesFromOMALabel(x, right, left) : OMA.SetIPD(right, left)
                            x = .Item("BCOCIN") : If x.Length > 0 Then GetValuesFromOMALabel(x, right, left) : OMA.SetBCOCIN(right, left)
                            x = .Item("BCOCUP") : If x.Length > 0 Then GetValuesFromOMALabel(x, right, left) : OMA.SetBCOCUP(right, left)
                            x = .Item("DIA") : If x.Length > 0 Then GetValuesFromOMALabel(x, right, left) : OMA.SetDIA(right, left)
                            x = .Item("CRIB") : If x.Length > 0 Then GetValuesFromOMALabel(x, right, left) : OMA.SetCRIB(right, left)
                            x = .Item("FCSGIN") : If x.Length > 0 Then GetValuesFromOMALabel(x, right, left) : OMA.SetFCSGIN(right, left)
                            x = .Item("FCSGUP") : If x.Length > 0 Then GetValuesFromOMALabel(x, right, left) : OMA.SetFCSGUP(right, left)
                            x = .Item("SDEPTH") : If x.Length > 0 Then GetValuesFromOMALabel(x, right, left) : OMA.SetSDEPTH(right, left)
                            x = .Item("NPD") : If x.Length > 0 Then GetValuesFromOMALabel(x, right, left) : OMA.SetNPD(right, left)
                            x = .Item("FCOCUP") : If x.Length > 0 Then GetValuesFromOMALabel(x, right, left) : OMA.SetFCOCUP(right, left)
                            x = .Item("FCOCIN") : If x.Length > 0 Then GetValuesFromOMALabel(x, right, left) : OMA.SetFCOCIN(right, left)
                            x = .Item("HBOX") : If x.Length > 0 Then GetValuesFromOMALabel(x, right, left) : OMA.SetHBOX(right, left)
                            x = .Item("VBOX") : If x.Length > 0 Then GetValuesFromOMALabel(x, right, left) : OMA.SetVBOX(right, left)
                            x = .Item("BEVC") : If x.Length > 0 Then GetValuesFromOMALabel(x, right, left) : OMA.SetBEVC(right, left)

                            OMA.SetPINB(My.Settings.PINB, My.Settings.PINB)
                            OMA.SetFPINB(My.Settings.FPINB, My.Settings.FPINB)
                            OMA.Set_PINONOF(My.Settings._PINONOF, My.Settings._PINONOF)
                            OMA.Set_FPINONF(My.Settings._FPINONF, My.Settings._FPINONF)
                            OMA.Set_ETYP2(My.Settings._ETYP2, My.Settings._ETYP2)
                            OMA.Set_POLISH2(My.Settings._POLISH2, My.Settings._POLISH2)
                            OMA.Set_VARINC(My.Settings._VARINC, My.Settings._VARINC)
                            OMA.Set_VANGLE(My.Settings._VANGLE, My.Settings._VANGLE)
                            OMA.Set_LCOAT(My.Settings._LCOAT, My.Settings._LCOAT)
                            OMA.Set_AUTOBFR(My.Settings._AUTOBFR, My.Settings._AUTOBFR)
                            OMA.Set_CKDOCUR(My.Settings._CKDOCUR)
                            OMA.Set_NDOCUR(My.Settings._NDOCUR)
                            OMA.Set_CURVELV(My.Settings._CURVELV)
                            OMA.SetGDEPTH(My.Settings.GDEPTH, My.Settings.GDEPTH)
                            OMA.SetGWIDTH(My.Settings.GWIDTH, My.Settings.GWIDTH)
                            OMA.Set_GDEPTH2(My.Settings.GDEPTH, My.Settings.GDEPTH)
                            OMA.Set_GWIDTH2(My.Settings.GWIDTH, My.Settings.GWIDTH)

                            OMA.Set_LPRESS(My.Settings._LPRESS, My.Settings._LPRESS)
                            OMA.Set_CIRCON(My.Settings._CIRCON, My.Settings._CIRCON)
                            OMA.Set_FBFCANG(My.Settings._FBFCANG, My.Settings._FBFCANG)
                            OMA.SetBEVP(My.Settings.BEVP.ToString, My.Settings.BEVP.ToString)


                            'x = .Item("LMATID") : If x.Length > 0 Then GetValuesFromOMALabel(x, right, left) : OMA.SetLMATID(right, left)
                            x = .Item("FTYP") : If x.Length > 0 Then GetValuesFromOMALabel(x, temp) : OMA.SetFTYP(temp)
                            x = .Item("ETYP")
                            If x.Length > 0 And x.Contains(";") Then
                                GetValuesFromOMALabel(x, right, left) : OMA.SetETYP(right, left)
                            ElseIf x.Length > 0 Then
                                GetValuesFromOMALabel(x, temp) : OMA.SetETYP(temp, temp)
                            End If

                            'x = .Item("BEVP") : If x.Length > 0 Then  GetValuesFromOMALabel(x, temp) : OMA.SetBEVP(temp, temp)
                            'x = .Item("POLISH") : If x.Length > 0 Then GetValuesFromOMALabel(x, temp) : OMA.SetPOLISH(temp)
                            x = .Item("CIRC") : If x.Length > 0 Then GetValuesFromOMALabel(x, right, left) : OMA.SetCIRC(right, left)
                            'x = .Item("FCRV") : If x.Length > 0 Then GetValuesFromOMALabel(x, right, left) : OMA.SetFCRV(right, left)
                            'OMA.SetFCRV("0.75", "0.75")
                            x = .Item("DRILL") : If x.Length > 0 Then OMA.SetDRILL(x)
                            x = .Item("TRCFMTR") & .Item("TRCRIGHT") : OMA.SetTRCFMT_R(x)
                            x = .Item("TRCFMTL") & .Item("TRCLEFT") : OMA.SetTRCFMT_L(x)

                            'OMA.WriteFile("C:\Test_OMA_File.txt")

                        End If

                    End With

                Case "DVI"
                    ContornoPuente.Text = (CSng(ds.Tables("t1").Rows(0).Item("dbl")) / 100).ToString
            End Select
            ' Una fumada mientras se arregla la vista OMATraces
            '-----------------------------------------------------
            If ContornoPuente.Text < 1 Then
                ContornoPuente.Text *= 100
            End If
            '-----------------------------------------------------
            '* SI EL PROTOCOLO ES DVI SE USARA LA TABLA TRACERDATA Y SE EXTRAEN LOS TRAZOS SIN CAMBIOS
            If My.Settings.OpticalProtocol = "DVI" Then
                For i = 0 To TotalTrazos
                    RadiosL(i) = ds.Tables("t1").Rows(0).Item(i + Offset)
                Next
                Dim j As Integer = 0
                For i = MedioCuadrante To 0 Step -1
                    RadiosR(j) = RadiosL(i)
                    j += 1
                Next
                For i = TotalTrazos To (MedioCuadrante + 1) Step -1
                    RadiosR(j) = RadiosL(i)
                    j += 1
                Next
            End If
            '* SI EL PROTOCOLO ES OMA SE EXTRAEN LOS DATOS DE LA TABLA OMATRACES
            '**recorremos los radios almacenado en el campo string de 2080 caracteres = 400 trazos 
            Dim TrcRight As String = "", TrcLeft As String = "", ScanEye As String
            'Dim RadioIni, POS As Integer
            If My.Settings.OpticalProtocol = "OMA" Then
                ScanEye = ds.Tables("t1").Rows(0).Item("lado")
                'dependiendo del ojo scaneado llenamos las variables
                Select Case ScanEye
                    Case "B"
                        TrcRight = ds.Tables("t1").Rows(0).Item("TrcRight")
                        TrcLeft = ds.Tables("t1").Rows(0).Item("TrcLeft")
                    Case "R"
                        TrcRight = ds.Tables("t1").Rows(0).Item("TrcRight")
                    Case "L"
                        TrcLeft = ds.Tables("t1").Rows(0).Item("TrcLeft")
                    Case Else
                        MsgBox("Error!!! No se sabe el lado trazado de OMA en la tabla OMATraces", MsgBoxStyle.Critical)
                        Return
                End Select
                Try
                    Dim tempr() As String = {""}
                    Dim templ() As String = {""}
                    If TrcRight.Length > 0 Then tempr = TrcRight.Replace("R=", ",").Replace(vbCr, "").Replace(vbCrLf, "").Replace(";", ",").Substring(1).Split(",")
                    If TrcLeft.Length > 0 Then templ = TrcLeft.Replace("R=", ",").Replace(vbCr, "").Replace(vbCrLf, "").Replace(";", ",").Substring(1).Split(",")
                    For i = 0 To 399
                        If (ScanEye = "B" Or ScanEye = "R") And (TrcRight.Length > 0) Then RadiosL(i) = tempr(i) 'No se por que esten volteados
                        If (ScanEye = "B" Or ScanEye = "L") And (TrcLeft.Length > 0) Then RadiosR(i) = templ(i) 'No se por que esten volteados
                    Next

                    'POS = 0
                    'For i = 0 To 2079 Step 52

                    '    RadioIni = i
                    '    If ScanEye = "B" Or ScanEye = "R" Then RadiosL(POS) = TrcRight.Substring(RadioIni + 2, 4)
                    '    If ScanEye = "B" Or ScanEye = "L" Then RadiosR(POS) = TrcLeft.Substring(RadioIni + 2, 4)
                    '    RadioIni += 5
                    '    POS += 1
                    '    If ScanEye = "B" Or ScanEye = "R" Then RadiosL(POS) = TrcRight.Substring(RadioIni + 2, 4)
                    '    If ScanEye = "B" Or ScanEye = "L" Then RadiosR(POS) = TrcLeft.Substring(RadioIni + 2, 4)
                    '    RadioIni += 5
                    '    POS += 1
                    '    If ScanEye = "B" Or ScanEye = "R" Then RadiosL(POS) = TrcRight.Substring(RadioIni + 2, 4)
                    '    If ScanEye = "B" Or ScanEye = "L" Then RadiosR(POS) = TrcLeft.Substring(RadioIni + 2, 4)
                    '    RadioIni += 5
                    '    POS += 1
                    '    If ScanEye = "B" Or ScanEye = "R" Then RadiosL(POS) = TrcRight.Substring(RadioIni + 2, 4)
                    '    If ScanEye = "B" Or ScanEye = "L" Then RadiosR(POS) = TrcLeft.Substring(RadioIni + 2, 4)
                    '    RadioIni += 5
                    '    POS += 1
                    '    If ScanEye = "B" Or ScanEye = "R" Then RadiosL(POS) = TrcRight.Substring(RadioIni + 2, 4)
                    '    If ScanEye = "B" Or ScanEye = "L" Then RadiosR(POS) = TrcLeft.Substring(RadioIni + 2, 4)
                    '    RadioIni += 5
                    '    POS += 1
                    '    If ScanEye = "B" Or ScanEye = "R" Then RadiosL(POS) = TrcRight.Substring(RadioIni + 2, 4)
                    '    If ScanEye = "B" Or ScanEye = "L" Then RadiosR(POS) = TrcLeft.Substring(RadioIni + 2, 4)
                    '    RadioIni += 5
                    '    POS += 1
                    '    If ScanEye = "B" Or ScanEye = "R" Then RadiosL(POS) = TrcRight.Substring(RadioIni + 2, 4)
                    '    If ScanEye = "B" Or ScanEye = "L" Then RadiosR(POS) = TrcLeft.Substring(RadioIni + 2, 4)
                    '    RadioIni += 5
                    '    POS += 1
                    '    If ScanEye = "B" Or ScanEye = "R" Then RadiosL(POS) = TrcRight.Substring(RadioIni + 2, 4)
                    '    If ScanEye = "B" Or ScanEye = "L" Then RadiosR(POS) = TrcLeft.Substring(RadioIni + 2, 4)
                    '    RadioIni += 5
                    '    POS += 1
                    '    If ScanEye = "B" Or ScanEye = "R" Then RadiosL(POS) = TrcRight.Substring(RadioIni + 2, 4)
                    '    If ScanEye = "B" Or ScanEye = "L" Then RadiosR(POS) = TrcLeft.Substring(RadioIni + 2, 4)
                    '    RadioIni += 5
                    '    POS += 1
                    '    If ScanEye = "B" Or ScanEye = "R" Then RadiosL(POS) = TrcRight.Substring(RadioIni + 2, 4)
                    '    If ScanEye = "B" Or ScanEye = "L" Then RadiosR(POS) = TrcLeft.Substring(RadioIni + 2, 4)
                    '    POS += 1
                    'Next

                Catch ex As Exception
                    MsgBox(ex.Message, MsgBoxStyle.Exclamation)

                End Try

                'dependiendo del ojo trazado, completamos los radios del ojo faltante
                Dim IMirror As Integer
                If ScanEye = "L" Then
                    IMirror = 199
                    For i = 0 To 199
                        RadiosL(IMirror) = RadiosR(i)
                        IMirror -= 1
                    Next
                    IMirror = 399
                    For i = 200 To 399
                        RadiosL(IMirror) = RadiosR(i)
                        IMirror -= 1
                    Next
                End If
                If ScanEye = "R" Then
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
        Catch ex As Exception
            If ex.Message.Contains("Error de Conexion") And t.svr = My.Settings.VantageServer Then
                Me.ChangeWorkingStatus(WorkingStatusType.OffLine)
            Else
                MsgBox(ex.Message, MsgBoxStyle.Critical)
            End If
        Finally
            t.CloseConn()
        End Try

    End Sub

    Function GetOMAFrameRadios(ByVal RefWECO As String) As Integer()
        Dim t As SqlDB
        Dim ds As New DataSet
        Dim Offset As Integer
        Dim SQLString As String
        Dim LRadios(399) As Integer


        t = New SqlDB(My.Settings.LocalServer, My.Settings.DBUser, My.Settings.DBPassword, My.Settings.LocalDBName)

        SQLString = "SELECT * FROM VwOMATraces WITH(NOLOCK) WHERE jobnum = '" & RefWECO & "' AND status = 1"
        Offset = 13

        Dim i As Integer
        Try
            t.OpenConn()
            ds = t.SQLDS(SQLString, "t1")

            'EL VALOR DE PUENTE EN EL PROTOCOLO OMA YA VIENE EN MILIMETROS A DIFERENCIA DE DVI
            ContornoPuente.Text = (CSng(ds.Tables("t1").Rows(0).Item("dbl")) / 100).ToString

            ' Una fumada mientras se arregla la vista OMATraces
            '-----------------------------------------------------
            If ContornoPuente.Text < 1 Then
                ContornoPuente.Text *= 100
            End If

            '-----------------------------------------------------

            '* SI EL PROTOCOLO ES OMA SE EXTRAEN LOS DATOS DE LA TABLA OMATRACES
            '**recorremos los radios almacenado en el campo string de 2080 caracteres = 400 trazos 
            Dim TrcRight, TrcLeft, ScanEye As String
            Dim RadioIni, POS As Integer

            TrcRight = ""
            TrcLeft = ""

            ScanEye = ds.Tables("t1").Rows(0).Item("lado")

            ' Dependiendo del ojo scaneado llenamos las variables
            Select Case ScanEye
                Case "B"
                    TrcRight = ds.Tables("t1").Rows(0).Item("TrcRight")
                    TrcLeft = ds.Tables("t1").Rows(0).Item("TrcLeft")
                Case "R"
                    TrcRight = ds.Tables("t1").Rows(0).Item("TrcRight")
                Case "L"
                    TrcLeft = ds.Tables("t1").Rows(0).Item("TrcLeft")
                Case Else
                    MsgBox("Error!!! No se sabe el lado trazado de OMA en la tabla OMATraces", MsgBoxStyle.Critical)
                    Return LRadios
            End Select
            POS = 0

            ' La informacion que regresa VwOMATraces varia  porque hay un espacio de mas. Si el trazo es un paquete Augen Air es pasos de 52, si es
            ' de VerBien es 53.
            Dim steps As Integer = 53
            'If (numpaq.Length > 0) Then steps = 52

            Dim ArmazonStr As String = NumArmazon.ToString.Substring(0, 2)
            Select Case armazonStr
                Case "61", "62", "63", "64", "65", "66", "67", "68", "69"
                    steps = 52
            End Select


            For i = 0 To 2079 Step steps
                RadioIni = i
                If ScanEye = "B" Or ScanEye = "R" Then LRadios(POS) = TrcRight.Substring(RadioIni + 2, 4)
                'If ScanEye = "B" Or ScanEye = "L" Then RadiosR(POS) = TrcLeft.Substring(RadioIni + 2, 4)
                RadioIni += 5
                POS += 1
                If ScanEye = "B" Or ScanEye = "R" Then LRadios(POS) = TrcRight.Substring(RadioIni + 2, 4)
                'If ScanEye = "B" Or ScanEye = "L" Then RadiosR(POS) = TrcLeft.Substring(RadioIni + 2, 4)
                RadioIni += 5
                POS += 1
                If ScanEye = "B" Or ScanEye = "R" Then LRadios(POS) = TrcRight.Substring(RadioIni + 2, 4)
                'If ScanEye = "B" Or ScanEye = "L" Then RadiosR(POS) = TrcLeft.Substring(RadioIni + 2, 4)
                RadioIni += 5
                POS += 1
                If ScanEye = "B" Or ScanEye = "R" Then LRadios(POS) = TrcRight.Substring(RadioIni + 2, 4)
                'If ScanEye = "B" Or ScanEye = "L" Then RadiosR(POS) = TrcLeft.Substring(RadioIni + 2, 4)
                RadioIni += 5
                POS += 1
                If ScanEye = "B" Or ScanEye = "R" Then LRadios(POS) = TrcRight.Substring(RadioIni + 2, 4)
                'If ScanEye = "B" Or ScanEye = "L" Then RadiosR(POS) = TrcLeft.Substring(RadioIni + 2, 4)
                RadioIni += 5
                POS += 1
                If ScanEye = "B" Or ScanEye = "R" Then LRadios(POS) = TrcRight.Substring(RadioIni + 2, 4)
                'If ScanEye = "B" Or ScanEye = "L" Then RadiosR(POS) = TrcLeft.Substring(RadioIni + 2, 4)
                RadioIni += 5
                POS += 1
                If ScanEye = "B" Or ScanEye = "R" Then LRadios(POS) = TrcRight.Substring(RadioIni + 2, 4)
                'If ScanEye = "B" Or ScanEye = "L" Then RadiosR(POS) = TrcLeft.Substring(RadioIni + 2, 4)
                RadioIni += 5
                POS += 1
                If ScanEye = "B" Or ScanEye = "R" Then LRadios(POS) = TrcRight.Substring(RadioIni + 2, 4)
                'If ScanEye = "B" Or ScanEye = "L" Then RadiosR(POS) = TrcLeft.Substring(RadioIni + 2, 4)
                RadioIni += 5
                POS += 1
                If ScanEye = "B" Or ScanEye = "R" Then LRadios(POS) = TrcRight.Substring(RadioIni + 2, 4)
                'If ScanEye = "B" Or ScanEye = "L" Then RadiosR(POS) = TrcLeft.Substring(RadioIni + 2, 4)
                RadioIni += 5
                POS += 1
                If ScanEye = "B" Or ScanEye = "R" Then LRadios(POS) = TrcRight.Substring(RadioIni + 2, 4)
                'If ScanEye = "B" Or ScanEye = "L" Then RadiosR(POS) = TrcLeft.Substring(RadioIni + 2, 4)
                POS += 1
            Next

            ' Dependiendo del ojo trazado, completamos los radios del ojo faltante
            Dim IMirror As Integer
            If ScanEye = "L" Then
                IMirror = 199
                For i = 0 To 199
                    RadiosL(IMirror) = RadiosR(i)
                    IMirror -= 1
                Next
                IMirror = 399
                For i = 200 To 399
                    RadiosL(IMirror) = RadiosR(i)
                    IMirror -= 1
                Next
            End If
            If ScanEye = "R" Then
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

        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical)
        Finally
            t.CloseConn()
        End Try

        Return LRadios

    End Function

    Sub MirrorRadios(ByVal mirrorside As Pastilla.Sides)
        Try
            Dim i As Integer
            Dim j As Integer = 0


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

            If mirrorside = Pastilla.Sides.Left Then
                For i = X2 - 1 To 0 Step -1
                    RadiosR(j) = RadiosL(i)
                    j += 1
                Next
                For i = X4 - 1 To X2 Step -1
                    RadiosR(j) = RadiosL(i)
                    j += 1
                Next
            Else
                For i = X2 - 1 To 0 Step -1
                    RadiosL(j) = RadiosR(i)
                    j += 1
                Next
                For i = X4 - 1 To X2 Step -1
                    RadiosL(j) = RadiosR(i)
                    j += 1
                Next
            End If

        Catch ex As Exception
            Throw New Exception("Error al espejear trazos. " & vbCrLf & ex.Message)
        End Try

    End Sub

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Dim kb As New TouchScreenKeyboard
        kb.ShowDialog()

    End Sub

    Private Sub GetTouchScreenValue(ByVal sender As System.Object, ByVal e As System.EventArgs) ' Handles DIPCerca.Click, DIPLejos.Click, Altura.Click, MonoRight.Click, MonoLeft.Click, EsferaRight.Click, EsferaLeft.Click, EjeRight.Click, EjeLeft.Click, ContornoPuente.Click, ContornoED.Click, ContornoB.Click, ContornoA.Click, CilindroRight.Click, CilindroLeft.Click, AlturaRight.Click, AlturaLeft.Click, AdicionRight.Click, AdicionLeft.Click
        Dim t As New TextBox
        t = sender
        Dim kb As New TouchScreenKeyboard
        kb.Location = New Point(((1024 - kb.Size.Width) / 2) + Me.Location.X, 400 + Me.Location.Y)
        kb.ShowDialog()
        If kb.Status Then
            t.Text = kb.Value
            If ListLeftParts.Items.Count <> 0 And ListRightParts.Items.Count <> 0 And t.Name <> DIPLejos.Name And _
                t.Name <> DIPCerca.Name And t.Name <> Altura.Name And t.Name <> ContornoA.Name And t.Name <> ContornoB.Name _
                And t.Name <> ContornoED.Name And t.Name <> ContornoPuente.Name Then
                SelectedLeftEye.Items.Clear()
                SelectedRightEye.Items.Clear()
                ListLeftParts.Items.Clear()
                ListRightParts.Items.Clear()
            End If
        End If
    End Sub

    Private Sub RxID_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RxID.Click
        'Dim kb As New TouchScreenKeyboard
        'kb.ShowDialog()
        'If kb.Status Then
        '    RxID.Text = kb.Value
        '    RxID_Leave(Me, e)
        'End If
    End Sub

    Private Sub RxID_Enter(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RxID.Enter
        'Dim kb As New TouchScreenKeyboard
        'kb.ShowDialog()
        'If kb.Status Then
        '    RxID.Text = kb.Value
        'End If
    End Sub

    Private Sub CheckAR_Changed(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CheckARGold.CheckedChanged, CheckAR.CheckedChanged, CheckARMatiz.CheckedChanged
        Dim HasAR As Boolean = False
        If sender.Name = CheckAR.Name Then
            If CheckAR.Checked Then
                CheckARGold.Checked = False
                CheckARMatiz.Checked = False
                HasAR = True
            End If
        End If
        If sender.name = CheckARGold.Name Then
            If CheckARGold.Checked Then
                CheckAR.Checked = False
                CheckARMatiz.Checked = False
                HasAR = True
            End If
        End If
        If sender.name = CheckARMatiz.Name Then
            If CheckARMatiz.Checked Then
                CheckAR.Checked = False
                CheckARGold.Checked = False
                HasAR = True
            End If
        End If
        If ((PanelInformacion.Visible = True) And (Not IsReceivingVirtualRx)) Or (RxType = RxTypes.WebRx) Then
            If HasAR Then
                Dim al As New ARLabList
                al.Location = New Point(Me.Location.X + 60, Me.Location.Y + 490)
                al.Label1.Text = "Laboratorio Antireflejante"
                al.IsARLab = True
                If RxType = RxTypes.WebRx And PanelInformacion.Visible = False Then
                    al.Location = New Point(Me.Location.X + 421, Me.Location.Y + 210)
                End If
                al.ARLabName = LabelARLab.Text.Substring(3)
                al.ShowDialog()
                ARLab = al.ComboARLab.SelectedValue
                ARLabName = al.ComboARLab.Text
                IsVirtualRx = False
                LabelARLab.Text = "AR " & ARLabName
                LabelARLab.Visible = True
                al.Dispose()
            Else
                ARLab = 0
                ARLabName = "NINGUNO"
                IsVirtualRx = False
                LabelARLab.Visible = False
            End If
        End If
    End Sub

    Private Sub CheckWorkingLenses(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CheckREye.CheckedChanged, CheckLEye.CheckedChanged
        If Not CheckLEye.Checked Then
            ListLeftParts.Items.Clear()
            LeftBarcodeRead = False
            'desabilitamos los campor de captura para este ojo
            EsferaLeft.Enabled = False
            CilindroLeft.Enabled = False
            AdicionLeft.Enabled = False
        Else
            EsferaLeft.Enabled = True
            CilindroLeft.Enabled = True
            AdicionLeft.Enabled = True
            ListLeftParts.Items.Clear()
            LeftBarcodeRead = False
        End If
        If Not CheckREye.Checked Then
            ListRightParts.Items.Clear()
            EsferaRight.Enabled = False
            CilindroRight.Enabled = False
            AdicionRight.Enabled = False
            RightBarcodeRead = False
        Else
            ListRightParts.Items.Clear()
            EsferaRight.Enabled = True
            CilindroRight.Enabled = True
            AdicionRight.Enabled = True
            RightBarcodeRead = False
        End If
        Dim cb As CheckBox.CheckBox = sender
        If cb.Name = CheckLEye.Name Then
            If cb.Checked Then
                EsferaLeft.Select()
            Else
                EsferaRight.Select()
            End If
        End If
        If cb.Name = CheckREye.Name Then
            If cb.Checked Then
                EsferaRight.Select()
            Else
                EsferaLeft.Select()
            End If
        End If
    End Sub

    Private Sub ModificarRx_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ModificarRx.Click
        Dim Search As Boolean = False
        If PanelInformacion.Visible = True Then
            If MsgBox("Estas seguro de abandonar esta receta?" & vbCrLf & "La informaci�n capturada se perder�.", MsgBoxStyle.OkCancel + MsgBoxStyle.Exclamation) = MsgBoxResult.Ok Then
                InitValues()
                Search = True
            Else
                Search = False
            End If
        Else
            Search = True
        End If
        If Search Then
            IsModifying = False
            Dim mr As New ModificarRecetas
            mr.ShowDialog()
            If mr.Status Then

                ' Esto es para modificar con el numero de Rx
                '-------------------------------------------------------
                'RxWECO.Text = mr.RxID.Text.PadLeft(4, "0")
                'IsModifying = True
                'BuscarRecetaExistente(RxWECO.Text)

                'If (ComboTrazos.ComboBox1.Items.Count > 0) Or NoTrace Then
                '    IsReceivingVirtualRx = False
                '    AceptarModificar(RxWECO.Text)
                'Else
                '    IsModifying = False
                'End If

                ' Esto es para modificar con el numero de Vantage
                '-------------------------------------------------------
                IsModifying = True
                AceptarModificar(mr.RxID.Text)
                If RxWECO.Text <> "" Then

                    ' Se agrego esta condicion por fgarcia. March 23, 2011. Antes solo estaba el metodo 'BuscarRecetaExistente'
                    ' Se puso esta condicion para evitar el error que se presentaba al modificar recetas sin trazo digital.
                    If (Not IsManualRx) Then

                        'BuscarRecetaExistente(RxWECO.Text)
                        If (My.Settings.Plant = "VBENS") Then
                            BuscarRecetaExistente(RxID.Text)
                        Else
                            BuscarRecetaExistente(RxWECO.Text)
                        End If
                    Else
                        RadioDigitalNew.Checked = False
                        RadioManualNew.Checked = True
                        AceptarManual_Click(Me, e)
                        NoTrace = True
                    End If

                    If (ComboTrazos.ComboBox1.Items.Count > 0) Or NoTrace Then
                        IsReceivingVirtualRx = False
                        If (ComboTrazos.ComboBox1.Items.Count > 0) Then
                            RadioManualNew_CheckedChanged(Me, e)
                            PanelPreviewTrazo.Visible = False
                            'TipoReceta_CheckedChanged(Me, e)
                            '------------------
                            'IsManualRx = False
                            '------------------
                        End If
                    Else
                        IsModifying = False
                    End If
                End If


            End If
        End If
    End Sub

    Private Function GetPartDescirption(ByVal PNStr As String)
        Dim t As New SqlDB(My.Settings.LocalServer, My.Settings.DBUser, My.Settings.DBPassword, My.Settings.LocalDBName)
        Dim ds As New Data.DataSet
        t.OpenConn()
        Try
            ds = t.SQLDS("select partdescription from part where partnum = '" & PNStr & "' and company = 'TRECEUX'", "t1")
            If ds.Tables("t1").Rows.Count > 0 Then
                Return ds.Tables("t1").Rows(0).Item("partdescription")
            Else
                Throw New Exception("N�mero de parte inexistente [" & PNStr & "]")
            End If
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, "LabRx")
            Return ""
        End Try
    End Function

    Private Sub RightRead_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles RightRead.KeyPress
        If Not ReadingBarCode Then
            Me.StartReadingBarcodeTime = Now.TimeOfDay
            ReadingBarCode = True
        End If
        If e.KeyChar = Chr(13) And CheckREye.Checked And RightRead.ReadOnly = False Then
            Me.EndReadingBarcodeTime = Now.TimeOfDay
            ReadingBarCode = False
            Dim x As TimeSpan = EndReadingBarcodeTime.Subtract(StartReadingBarcodeTime)
            If (x.Seconds = 0 And x.Milliseconds < 600) Or RightRead.Text = My.Settings.LenteSinCalculos Then
                Enter_RightPart()
            Else
                MsgBox("Error al leer codigo de barras, fue introducido manualmente", MsgBoxStyle.Critical)
            End If
            RightRead.Text = ""
        End If
    End Sub
    Private Sub Enter_RightPart()
        Dim PDStr As String
        Dim Partnum As String = RightRead.Text
        ListRightParts.Items.Clear()
        txtQtyOnHandRight.Text = "0"
        'If CBX_SpecialDesign.Checked And My.Settings.EnableOptisur Then
        '    If Not IsSpecialDesignCompatible(Partnum, SpecialDesignID) Then
        '        Exit Sub
        '    End If
        'End If
        PDStr = GetPartDescirption(Partnum)
        If PDStr <> "" Then
            '***************************************
            '*Marco A. 8-jun 2007
            '*Si el numero de parte existe, lo que sigue es 
            '*verificar la  cantidad que existe del numero de parte en inventario
            '***************************************
            ShowQtyOnHand(RightRead.Text, "Right")
            'If ParteEnInventarior(RightRead.Text) Then
            Dim x As New ItemWithValue
            x.Value = RightRead.Text
            x.Name = PDStr
            ListRightParts.Items.Add(x)
            ListRightParts.SelectedIndex = 0
            InsertLog = True
            InsertLogSide = InsertLogSide Or 2
            Dim Mat As Integer
            Dim Dis As Integer
            Dim Esfera As Single = EsferaRight.Text
            Dim Cilindro As Single = CilindroRight.Text
            Mat = GetCustomValues(CustomValues.MaterialID, x.Value.ToString)
            Dis = GetCustomValues(CustomValues.Dise�oID, x.Value.ToString)
            If Me.PanelInformacion.Visible Then
                RightBarcodeRead = True
            End If

            If Mat = 1 And Dis = 2 Then
                Dim ds As New DataSet
                Dim t As New SqlDB(My.Settings.LocalServer, My.Settings.DBUser, My.Settings.DBPassword, My.Settings.LocalDBName)
                Try
                    t.OpenConn()
                    ds = t.SQLDS("select partnum,partdescription from VwPartTreceux where material = 'TX' and dise�o = 'VS' and base = " & Esfera & " and adicion = " & Cilindro, "t1")
                    If ds.Tables("t1").Rows.Count > 0 Then
                        MsgBox("Esta graduacion puede surtirse con el siguiente lente terminado: " & vbCrLf & ds.Tables("t1").Rows(0).Item("partnum") & " " & ds.Tables("t1").Rows(0).Item("partdescription"))
                    End If

                Catch ex As Exception
                Finally
                    t.CloseConn()
                End Try
            End If
            'Else
            'MsgBox("La pastilla no se encuentra en inventario, es necesario actualizar primero el inventario para poder capturar la receta.", MsgBoxStyle.Information, "LABRX")
            'End If
        End If
        SelectedRightEye.Items.Clear()
        SelectedRightEye.Items.AddRange(ListRightParts.Items)
        RightRead.Text = ""
    End Sub

    Private Sub ShowQtyOnHand(ByVal PartNumStr As String, ByVal SideStr As String)
        Me.Cursor = Cursors.WaitCursor
        Try
            'VERIFICAMOS EL INVENTARIO DE LA PARTE EN TODO EL WHSE INDEPENDIENTEMENTE DEL BIN
            Dim Conn As SqlClient.SqlConnection
            Dim DRdr As SqlClient.SqlDataReader
            Dim Cmd As SqlClient.SqlCommand
            Dim SQLStr As String = ""
            Dim OnHandQty As Integer

            Conn = New SqlClient.SqlConnection
            Conn.ConnectionString = Laboratorios.ConnStr

            Conn.Open()
            SQLStr = "SELECT onhandqty from partbin with(nolock) " & _
                     "where partnum = '" & PartNumStr & "' and warehousecode = (select warehousecode from plantwhse with(nolock) " & _
                     "where partnum = '" & PartNumStr & "' and company = 'TRECEUX' AND PLANT = '" & Read_Registry("Plant") & "' )"
            Cmd = New SqlClient.SqlCommand(SQLStr, Conn)
            Cmd.CommandTimeout = My.Settings.DBCommandTimeout
            DRdr = Cmd.ExecuteReader
            If DRdr.HasRows Then
                OnHandQty = 0
                While DRdr.Read
                    Try
                        'si hay un nulo que no marque error
                        OnHandQty = CInt(DRdr("onhandqty")) + OnHandQty
                    Catch ex As Exception
                        OnHandQty = 0
                    End Try
                End While
                'si onhandqty es mayor que o igual a uno, entonces si tenemos inventario para consumir la pastilla
                Select Case SideStr
                    Case "Right"
                        txtQtyOnHandRight.Text = OnHandQty
                    Case "Left"
                        txtQtyOnHandLeft.Text = OnHandQty
                End Select
            Else
                'si el registro no existe  = el numero de parte no esta en inventario
                Select Case SideStr
                    Case "Right"
                        txtQtyOnHandRight.Text = "NoInv"
                    Case "Left"
                        txtQtyOnHandLeft.Text = "NoInv"
                End Select
            End If
            DRdr.Close()
            DRdr = Nothing

            Cmd.Dispose()
            Cmd = Nothing
            Conn.Close()
            Conn.Dispose()
            Conn = Nothing

            Me.Cursor = Cursors.Default
        Catch ex As Exception
            Me.Cursor = Cursors.Default
            'MsgBox("Error al consultar el inventario de la pastilla" & vbCrLf & ex.Message, MsgBoxStyle.Exclamation)
            Select Case SideStr
                Case "Right"
                    txtQtyOnHandRight.Text = "Err"
                Case "Left"
                    txtQtyOnHandLeft.Text = "Err"
            End Select
        End Try
    End Sub

    Private Function InventarioOK() As Boolean
        Dim der, izq As String
        Dim derOK, izqOK As Boolean


        If Me.ListRightParts.Items.Count > 0 Then
            Dim x As ItemWithValue = Me.ListRightParts.SelectedItem
            der = x.Value
        Else
            der = ""
        End If
        If Me.ListLeftParts.Items.Count > 0 Then
            Dim x As ItemWithValue = Me.ListLeftParts.SelectedItem
            izq = x.Value
        Else
            izq = ""
        End If
        If ((ARLab <> LabID()) And (ARLab > 0)) Then
            derOK = True
            izqOK = True
        End If
        '********OJO DERECHO **********************
        If der <> "" Then
            If der = My.Settings.LenteSinCalculos Then
                derOK = True
            Else
                If OriginalRightPart <> der Then
                    If OriginalRightPart = "" Then 'RECETA NUEVA
                        If Not IsVirtualRx Then 'SI NO ES VIRTUAL CHECAMOS INVENTARIO
                            derOK = Me.ParteEnInventarioR(der)
                        Else
                            If ARLab = LabID() Then 'ES VIRTUAL Y EL LAB ACTUAL ES AR
                                derOK = Me.ParteEnInventarioR(der)
                            Else
                                'ES VIRTUAL Y ES UN LAB SIN AR, ENTONCES NO CHECAMOS INVENTARIO
                                derOK = True
                            End If
                        End If
                    Else
                        If Not IsVirtualRx Then 'CAMBIO LA PASTILLA Y NO ES UNA RX VIRTUAL, CHECAMO EL INVENTARIO
                            derOK = Me.ParteEnInventarioR(der)
                        Else
                            If ARLab = LabID() Then 'SI CAMBIO LA PASTILLA Y ES UN LAB CON AR CHECAMOS EL INVENRATIO
                                derOK = Me.ParteEnInventarioR(der)
                            Else 'CAMBIO LA PASTIILA Y ES UN LAB SIN AR NO CHECAMOS EL INVENTARIO
                                derOK = True
                            End If
                        End If
                    End If
                    'si hubo cambio en la pastilla o la receta es nueva del ojo checamos inventario dfe la nueva pastilla
                Else
                    'ES UNA MODIFICACION SIN CAMBIO DE PASTILLA
                    If ComboErrores.SelectedValue <> My.Settings.RecapturaSP_ID Or PanelErrores.Visible = True Then 'SI NO ES RECAPTURA SP CHECAMOS EL INVENTARIO
                        If Not IsVirtualRx Then 'SI NO ES VIRTUAL CHECAMOS INVENTARIO
                            derOK = Me.ParteEnInventarioR(der)
                        Else
                            If ARLab = LabID() Then 'ES VIRTUAL Y EL LAB ES AR
                                derOK = Me.ParteEnInventarioR(der)
                            Else
                                'ES VIRTUAL Y ES UN LAB SIN AR, ENTONCES NO CHECAMOS INVENTARIO
                                derOK = True
                            End If
                        End If
                    Else
                        If IsVirtualRx Then
                            If IsReceivingVirtualRx Then
                                derOK = Me.ParteEnInventarioR(der)
                            Else
                                derOK = True
                            End If
                        Else
                            'ES UNA MODIFICACION SIN CAMBIO DE PASTILLA Y DE TIPO RECAPTURA SP, NO CHECAMOS EL INVENTARIO
                            derOK = True
                        End If
                    End If
                End If

            End If
        Else
            derOK = True
        End If
        '********OJO IZQUIERDO **********************
        If izq <> "" Then
            If izq = My.Settings.LenteSinCalculos Then
                izqOK = True
            Else
                If OriginalLeftPart <> izq Then
                    If OriginalLeftPart = "" Then 'RECETA NUEVA
                        If Not IsVirtualRx Then 'SI NO ES VIRTUAL CHECAMOS INVENTARIO
                            izqOK = Me.ParteEnInventarioL(izq)
                        Else
                            If ARLab = LabID() Then 'ES VIRTUAL Y EL LAB ACTUAL ES AR
                                izqOK = Me.ParteEnInventarioL(izq)
                            Else
                                'ES VIRTUAL Y ES UN LAB SIN AR, ENTONCES NO CHECAMOS INVENTARIO
                                izqOK = True
                            End If
                        End If
                    Else
                        If Not IsVirtualRx Then 'CAMBIO LA PASTILLA Y NO ES UNA RX VIRTUAL, CHECAMO EL INVENTARIO
                            izqOK = Me.ParteEnInventarioL(izq)
                        Else
                            If ARLab = LabID() Then 'SI CAMBIO LA PASTILLA Y ES UN LAB CON AR CHECAMOS EL INVENRATIO
                                izqOK = Me.ParteEnInventarioL(izq)
                            Else 'CAMBIO LA PASTIILA Y ES UN LAB SIN AR NO CHECAMOS EL INVENTARIO
                                izqOK = True
                            End If
                        End If
                    End If
                    'si hubo cambio en la pastilla o la receta es nueva del ojo checamos inventario dfe la nueva pastilla
                Else
                    'ES UNA MODIFICACION SIN CAMBIO DE PASTILLA
                    If ComboErrores.SelectedValue <> My.Settings.RecapturaSP_ID Or PanelErrores.Visible = True Then 'SI NO ES RECAPTURA SP CHECAMOS EL INVENTARIO
                        If Not IsVirtualRx Then 'SI NO ES VIRTUAL CHECAMOS INVENTARIO
                            izqOK = Me.ParteEnInventarioL(izq)
                        Else
                            If ARLab = LabID() Then 'ES VIRTUAL Y EL LAB ES AR
                                izqOK = Me.ParteEnInventarioL(izq)
                            Else
                                'ES VIRTUAL Y ES UN LAB SIN AR, ENTONCES NO CHECAMOS INVENTARIO
                                izqOK = True
                            End If
                        End If
                    Else
                        If IsVirtualRx Then
                            If IsReceivingVirtualRx Then
                                izqOK = Me.ParteEnInventarioL(izq)
                            Else
                                izqOK = True
                            End If
                        Else
                            'ES UNA MODIFICACION SIN CAMBIO DE PASTILLA Y DE TIPO RECAPTURA SP, NO CHECAMOS EL INVENTARIO
                            izqOK = True
                        End If
                    End If
                End If
            End If
        Else
            izqOK = True
        End If

        If RxNum.Text.Length = 9 Then
            If (RxNum.Text.Substring(0, 2) = 12) Then
                derOK = True
                izqOK = True
            End If
        End If
        'caso si existe el inventario para ambos ojos
        If derOK And izqOK Then
            Return True
            Exit Function
        End If
        'caso si no existe inventario para ambos ojos
        If Not derOK And Not izqOK Then
            MsgBox("La pastilla del ojo derecho e izquierdo no existen en inventario, favor de actualizar el inventario primero.", MsgBoxStyle.Information, "LabRX")
            Return False
            Exit Function
        End If
        'caso si solo existe inventario para el ojo derecho
        If derOK And Not izqOK Then
            MsgBox("La pastilla del ojo izquierdo no existe en inventario, favor de actualizar el inventario primero.", MsgBoxStyle.Information, "LabRX")
            Return False
            Exit Function
        End If
        'caso si solo existe inventario para el ojo izquierdo
        If izqOK And Not derOK Then
            MsgBox("La pastilla del ojo derecho no existe en inventario, favor de actualizar el inventario primero.", MsgBoxStyle.Information, "LabRX")
            Return False
            Exit Function
        End If
    End Function

    '********* FUNCION QUE REVISA EL INVENTARIO DEL NUMERO DE PARTE A CONSUMIR ***************
    Private Function ParteEnInventarioR(ByVal part As String) As Boolean
        Dim ExisteInv As Boolean = False
        Me.Cursor = Cursors.WaitCursor
        Dim izq As String

        Try
            'verificamos si el laboratorio tiene encendida la badera para revisar el inventario
            If Read_Registry("InventoryON") Then
                If part = My.Settings.LenteSinCalculos Then
                    ExisteInv = True
                End If

                'VERIFICAMOS EL INVENTARIO DE LA PARTE EN TODO EL WHSE INDEPENDIENTEMENTE DEL BIN
                Dim Conn As SqlClient.SqlConnection
                Dim DRdr As SqlClient.SqlDataReader
                Dim Cmd As SqlClient.SqlCommand
                Dim SQLStr As String = ""
                Dim OnHandQty As Integer

                Conn = New SqlClient.SqlConnection
                Conn.ConnectionString = "user ID=" & My.Settings.DBUser & ";password =" & My.Settings.DBPassword & ";database=" & My.Settings.LocalDBName & ";server=" & My.Settings.LocalServer & ";Connect Timeout=10"
                Conn.Open()
                SQLStr = "SELECT onhandqty from partbin with(nolock) " & _
                         "where partnum = '" & part & "' and warehousecode = (select warehousecode from plantwhse with(nolock) " & _
                         "where partnum = '" & part & "' and company = 'TRECEUX' AND PLANT = '" & Read_Registry("Plant") & "' )"
                Cmd = New SqlClient.SqlCommand(SQLStr, Conn)
                Cmd.CommandTimeout = My.Settings.DBCommandTimeout
                DRdr = Cmd.ExecuteReader
                If DRdr.HasRows Then
                    OnHandQty = 0
                    While DRdr.Read
                        Try
                            ' Si hay un nulo que no marque error
                            OnHandQty = CInt(DRdr("onhandqty")) + OnHandQty
                        Catch ex As Exception
                            OnHandQty = 0
                        End Try
                    End While

                    ' Si onhandqty es mayor que o igual a uno, entonces si tenemos inventario para consumir la pastilla
                    If Me.ListLeftParts.Items.Count > 0 Then
                        Dim x As ItemWithValue = Me.ListLeftParts.SelectedItem
                        izq = x.Value
                    Else
                        izq = ""
                    End If
                    If izq = part Then
                        If OnHandQty >= 2 Then
                            ExisteInv = True
                        Else
                            ExisteInv = False
                        End If
                    Else
                        If OnHandQty >= 1 Then
                            ExisteInv = True
                        Else
                            ExisteInv = False
                        End If
                    End If
                Else
                    ' Si el registro no existe  = el numero de parte no esta en inventario
                    ExisteInv = False
                End If
                DRdr.Close()
                DRdr = Nothing

                Cmd.Dispose()
                Cmd = Nothing
                Conn.Close()
                Conn.Dispose()
                Conn = Nothing
            Else
                ExisteInv = True
            End If
            Me.Cursor = Cursors.Default
            Return ExisteInv
        Catch ex As Exception
            Me.Cursor = Cursors.Default
            MsgBox("Error al consultar el inventario de la pastilla" & vbCrLf & ex.Message, MsgBoxStyle.Exclamation)
            Return True
        End Try
    End Function

    Private Function ParteEnInventarioL(ByVal part As String) As Boolean
        Dim ExisteInv As Boolean = False
        Me.Cursor = Cursors.WaitCursor
        Dim der As String
        Try
            ' Verificamos si el laboratorio tiene encendida la badera para revisar el inventario
            If Read_Registry("InventoryON") Then
                If part = My.Settings.LenteSinCalculos Then
                    ExisteInv = True
                End If

                'VERIFICAMOS EL INVENTARIO DE LA PARTE EN TODO EL WHSE INDEPENDIENTEMENTE DEL BIN
                Dim Conn As SqlClient.SqlConnection
                Dim DRdr As SqlClient.SqlDataReader
                Dim Cmd As SqlClient.SqlCommand
                Dim SQLStr As String = ""
                Dim OnHandQty As Integer

                Conn = New SqlClient.SqlConnection
                Conn.ConnectionString = "user ID=" & My.Settings.DBUser & ";password =" & My.Settings.DBPassword & ";database=" & My.Settings.LocalDBName & ";server=" & My.Settings.LocalServer & ";Connect Timeout=10"
                Conn.Open()

                SQLStr = "SELECT onhandqty from partbin with(nolock) " & _
                         "where partnum = '" & part & "' and warehousecode = (select warehousecode from plantwhse with(nolock) " & _
                         "where partnum = '" & part & "' and company = 'TRECEUX' AND PLANT = '" & Read_Registry("Plant") & "' )"
                Cmd = New SqlClient.SqlCommand(SQLStr, Conn)
                Cmd.CommandTimeout = My.Settings.DBCommandTimeout
                DRdr = Cmd.ExecuteReader
                If DRdr.HasRows Then
                    OnHandQty = 0
                    While DRdr.Read
                        Try
                            ' Si hay un nulo que no marque error
                            OnHandQty = CInt(DRdr("onhandqty")) + OnHandQty
                        Catch ex As Exception
                            OnHandQty = 0
                        End Try
                    End While
                    ' Si onhandqty es mayor que o igual a uno, entonces si tenemos inventario para consumir la pastilla
                    If Me.ListRightParts.Items.Count > 0 Then
                        Dim x As ItemWithValue = Me.ListRightParts.SelectedItem
                        der = x.Value
                    Else
                        der = ""
                    End If

                    If der = part Then
                        If OnHandQty >= 2 Then
                            ExisteInv = True
                        Else
                            ExisteInv = False
                        End If
                    Else
                        If OnHandQty >= 1 Then
                            ExisteInv = True
                        Else
                            ExisteInv = False
                        End If
                    End If

                Else
                    ' Si el registro no existe  = el numero de parte no esta en inventario
                    ExisteInv = False
                End If
                DRdr.Close()
                DRdr = Nothing

                Cmd.Dispose()
                Cmd = Nothing
                Conn.Close()
                Conn.Dispose()
                Conn = Nothing

            Else
                ExisteInv = True
            End If
            Me.Cursor = Cursors.Default
            Return ExisteInv
        Catch ex As Exception
            Me.Cursor = Cursors.Default
            MsgBox("Error al consultar el inventario de la pastilla" & vbCrLf & ex.Message, MsgBoxStyle.Exclamation)
            Return True
        End Try
    End Function

    Private Sub ListRightParts_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ListRightParts.Click, SelectedRightEye.Click, PanelRightEye.Click
        RightRead.Text = ""
        RightRead.Select()
    End Sub

    Private Sub ListLeftParts_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ListLeftParts.Click, SelectedLeftEye.Click, PanelLeftEye.Click
        LeftRead.Text = ""
        LeftRead.Select()
    End Sub

    Private Sub LeftRead_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles LeftRead.KeyPress
        If Not ReadingBarCode Then
            Me.StartReadingBarcodeTime = Now.TimeOfDay
            ReadingBarCode = True
        End If
        If e.KeyChar = Chr(13) And CheckLEye.Checked And LeftRead.ReadOnly = False Then
            Me.EndReadingBarcodeTime = Now.TimeOfDay
            ReadingBarCode = False
            Dim x As TimeSpan = EndReadingBarcodeTime.Subtract(StartReadingBarcodeTime)
            If (x.Seconds = 0 And x.Milliseconds < 600) Or LeftRead.Text = My.Settings.LenteSinCalculos Then
                Enter_LeftPart()
            Else
                MsgBox("Error al leer codigo de barras, fue introducido manualmente", MsgBoxStyle.Critical)
            End If
            LeftRead.Text = ""
        End If
    End Sub
    Private Function IsSpecialDesignCompatible(ByVal Partnum As String, ByVal DesignID As String) As Boolean
        IsSpecialDesignCompatible = True
        Dim Failed As Boolean = False
        Dim FailedMessage As String = ""
        Dim ds As New DataSet
        Dim t As New SqlDB()

        If Partnum <> My.Settings.LenteSinCalculos Then
            t.SQLConn = New SqlConnection(Laboratorios.ConnStr)
            Try
                t.OpenConn()
                ds = t.SQLDS("select * from VwValidSpecialDesignPartnum with(nolock) where partnum = '" & Partnum & "' and specialdesignid = " & DesignID, "t1")
                If ds.Tables("t1").Rows.Count = 0 Then
                    Throw New Exception("Esta pastilla no es compatible con el Dise�o Especial seleccionado")
                End If
            Catch ex As Exception
                IsSpecialDesignCompatible = False
                Failed = True
                FailedMessage = ex.Message
            Finally
                t.CloseConn()
                If Failed Then
                    MsgBox(FailedMessage, MsgBoxStyle.Exclamation)
                End If
            End Try
        End If
        Return IsSpecialDesignCompatible
    End Function
    Private Sub Enter_LeftPart()
        Dim PDStr As String
        Dim Partnum As String = LeftRead.Text
        Dim SpecialDesignCompatible As Boolean = True
        ListLeftParts.Items.Clear()
        txtQtyOnHandLeft.Text = "0"
        'If CBX_SpecialDesign.Checked And My.Settings.EnableOptisur Then
        '    If Not IsSpecialDesignCompatible(Partnum, SpecialDesignID) Then
        '        Exit Sub
        '    End If
        'End If
        PDStr = GetPartDescirption(Partnum)
        If PDStr <> "" Then
            '***************************************
            '*Marco A. 8-jun 2007
            '*Si el numero de parte existe, lo que sigue es 
            '*verificar la  cantidad que existe del numero de parte en inventario
            '***************************************
            ShowQtyOnHand(LeftRead.Text, "Left")
            Dim x As New ItemWithValue

            x.Value = LeftRead.Text
            x.Name = PDStr

            ListLeftParts.Items.Add(x)
            ListLeftParts.SelectedIndex = 0
            InsertLog = True
            InsertLogSide = InsertLogSide Or 1

            Dim Mat As Integer
            Dim Dis As Integer
            Dim Esfera As Single = EsferaLeft.Text
            Dim Cilindro As Single = CilindroLeft.Text
            Mat = GetCustomValues(CustomValues.MaterialID, x.Value.ToString)
            Dis = GetCustomValues(CustomValues.Dise�oID, x.Value.ToString)
            If Me.PanelInformacion.Visible Then
                LeftBarcodeRead = True
            End If


            'If CBX_SpecialDesign.Checked Then
            '    If Not IsSpecialDesignCompatible(Partnum, Dis) Then
            '        txtQtyOnHandLeft.Text = "0"
            '    End If
            'End If


            If Mat = 1 And Dis = 2 Then
                Dim ds As New DataSet
                Dim t As New SqlDB(My.Settings.LocalServer, My.Settings.DBUser, My.Settings.DBPassword, My.Settings.LocalDBName)
                Try
                    t.OpenConn()
                    ds = t.SQLDS("select partnum,partdescription from VwPartTreceux where material = 'TX' and dise�o = 'VS' and base = " & Esfera & " and adicion = " & Cilindro, "t1")
                    If ds.Tables("t1").Rows.Count > 0 Then
                        MsgBox("Esta graduacion puede surtirse con el siguiente lente terminado: " & vbCrLf & ds.Tables("t1").Rows(0).Item("partnum") & " " & ds.Tables("t1").Rows(0).Item("partdescription"))
                    End If

                Catch ex As Exception
                Finally
                    t.CloseConn()

                End Try
            End If
            'Else
            '   MsgBox("La pastilla no se encuentra en inventario, es necesario actualizar primero el inventario para poder capturar la receta.", MsgBoxStyle.Information, "LABRX")
            'End If
        End If
        SelectedLeftEye.Items.Clear()
        SelectedLeftEye.Items.AddRange(ListLeftParts.Items)
        LeftRead.Text = ""
    End Sub

    Private Sub EsferaRight_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles EsferaRight.TextChanged, EsferaLeft.TextChanged, EjeRight.TextChanged, EjeLeft.TextChanged, CilindroRight.TextChanged, CilindroLeft.TextChanged, AdicionRight.TextChanged, AdicionLeft.TextChanged
        ListLeftParts.Items.Clear()
        ListRightParts.Items.Clear()
        SelectedLeftEye.Items.Clear()
        SelectedRightEye.Items.Clear()
        InsertLog = False
        InsertLogSide = 0
        Dim tb As TextBox = sender
    End Sub

    Private Sub PingWorker_DoWork(ByVal sender As System.Object, ByVal e As System.ComponentModel.DoWorkEventArgs) Handles PingWorker.DoWork
        If Ping(My.Settings.VantageServer) Then
            'btnverifica_Click(Me, New EventArgs)       'es para cuando este implementado costco
        End If
        'me.PingWorker.
    End Sub
    Private Sub RefreshOnlineIndicator()
        EntradaMaterial.Enabled = True

        If WorkingOnLine Then
            PingTimer.Enabled = False
            PictureStatus.Image = My.Resources.VistaConnected
            ReceiveRx.Enabled = True
            ReceiveMaterial.Enabled = True
            btnretener.Enabled = True

            RetenerRx.Enabled = True
            RexibeVirtualRx.Enabled = True
            'EntradaMaterial.Enabled = True

            InternetOrdersLoaded = False

            pe.Enabled = True
            If LabelStatus.Text <> "Conectado" Then
                LabelStatus.Text = "Conectado"
                Notify.ShowBalloonTip(1500, Application.ProductName, "Se ha reestablecido la conexi�n con el Servidor Principal.", ToolTipIcon.Info)
                ActualizaClientesToolStripMenuItem_Click(Me, New EventArgs)
            End If
        Else
            WorkingOnLine = False
            PictureStatus.Image = My.Resources.VistaDisconnected

            ReceiveRx.Enabled = False
            If AL.IsAccessible Then Me.AL.Close()
            ReceiveMaterial.Enabled = False
            btnretener.Enabled = False
            '            pe.ActualizaProcesos.Enabled = False
            pe.Enabled = True

            If Not InternetOrdersLoaded And My.Settings.CheckLocalWebOrders Then
                LoadInternetOrders()        ' Agregado por Fco Garcia 8/3/2007
                InternetOrdersLoaded = True

                If DGCostco.RowCount > 0 Then
                    WO.DGCostco.DataSource = DGCostco.DataSource
                    WO.Show()
                Else
                    WO.Hide()
                End If
            End If

            'If (Ping(My.Settings.VantageServer)) Then : EntradaMaterial.Enabled = True : Else : EntradaMaterial.Enabled = False : End If
            If LabelStatus.Text <> "Desconectado" Then
                LabelStatus.Text = "Desconectado"
                ' ''Notify.ShowBalloonTip(1500, Application.ProductName, "Se ha perdido la conexi�n con el Servidor Principal." & vbCrLf & "Se trabajar� de manera local.", ToolTipIcon.Warning)
            End If
        End If


        If My.Settings.WorkingMode.ToUpper = "ONLINE" Then
            PingTimer.Enabled = True
        End If

    End Sub

    Private Sub PingWorker_RunWorkerCompleted(ByVal sender As System.Object, ByVal e As System.ComponentModel.RunWorkerCompletedEventArgs) Handles PingWorker.RunWorkerCompleted
        RefreshOnlineIndicator()
    End Sub

    Private Sub PrismasToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PrismasToolStripMenuItem.Click
        If PanelInformacion.Visible = True Then
            Dim pr As New Prismas
            'desplegamos los valores ya capturados en la forma
            pr.TextPrismaL.Text = Me.PrismaL
            pr.TextPrismaR.Text = Me.PrismaR
            Select Case Me.EjePrismaR
                Case 0
                    pr.EjePrismaRN.Checked = True
                Case 90
                    pr.EjePrismaRS.Checked = True
                Case 180
                    pr.EjePrismaRT.Checked = True
                Case 270
                    pr.EjePrismaRI.Checked = True
                Case Else
                    pr.EjePrismaROtro.Checked = True
                    pr.EjePrismaROtroVal.Text = Me.EjePrismaR
            End Select
            Select Case Me.EjePrismaL
                Case 0
                    pr.EjePrismaLT.Checked = True
                Case 90
                    pr.EjePrismaLS.Checked = True
                Case 180
                    pr.EjePrismaLN.Checked = True
                Case 270
                    pr.EjePrismaLI.Checked = True
                Case Else
                    pr.EjePrismaLOtro.Checked = True
                    pr.EjePrismaLOtroVal.Text = Me.EjePrismaL
            End Select
            pr.ShowDialog()
            If pr.Status Then
                PrismaL = pr.PrismaL
                PrismaR = pr.PrismaR
                EjePrismaL = pr.EjePrismaL
                EjePrismaR = pr.EjePrismaR
            End If
        End If
    End Sub

    Private Sub RxVirtualesToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RxVirtualesToolStripMenuItem.Click
        Dim rv As New EditValues
        rv.Text = "Rx Virtuales"
        rv.LabelValue.Text = "Introduce No. Vantage"
        rv.ShowDialog()
        If rv.Changed Then
            Dim ordernum As String = rv.TextValue.Text

            ' Obtener Rx con la vista VwVirtualesCHAP
            Dim t As New SqlDB(My.Settings.AuxVantageServer, My.Settings.DBUser, My.Settings.DBPassword, My.Settings.MfgSysDBName)
            Dim Change As Boolean = True
            Try
                If PanelInformacion.Visible Then
                    If MsgBox("Estas seguro de mostrar esta receta?" & vbCrLf & "La informaci�n capturada se perder�.", MsgBoxStyle.OkCancel + MsgBoxStyle.Exclamation) = MsgBoxResult.Cancel Then
                        Change = False
                    End If
                End If
                If Change Then
                    t.OpenConn()
                    Dim ds As New DataSet
                    Dim sqlstr As String = "SELECT     TOP (1) ordernum, Clase, Material, Dise�o, Lado, OrdenLab, RefDigital, Armazon, TipoArmazon, Biselado, Antireflejante, Tinte, Gradiente, Tono, EsferaR, EsferaL, CilindroR, CilindroL, EjeR, EjeL, AdicionR, AdicionL, AlturaR, AlturaL, MonoR, MonoL, DIPle, DIPce, Altura, A, B, ED, Puente, MaterialID, Dise�oID, ArmazonID, Status, Foco, orderdate, RxLado, partnum, partdescription, RxClase, custnum, Coated, Abrillantado, PrismaR, EjePrismaR, PrismaL, EjePrismaL, FechaInicial, FechaSalida, Retenida, Comentarios, Plant, Plant2, CustName, IsVirtualRx, ARLab, InARLab, OutARLab, ARLabName, ReceivedInLocalLab, FechaARInLocalLab, CheckLeftEye, CheckRightEye, RxInspected, ComentariosLab, IsGratis, IsGarantia, CostcoLente " & _
                                                "FROM         VwRecetasTRECEUX WITH (nolock) " & _
                                                "WHERE(ordernum = " & ordernum & ")"

                    ds = t.SQLDS(sqlstr, "t1")
                    Dim rxnum As String = ds.Tables("t1").Rows(0).Item("ordernum")
                    If rxnum.Substring(0, 2) = ARLab Then
                        IsReceivingVirtualRx = True
                        FillRx(ds, False)
                        RadioManual.Checked = True
                        AceptarManualNew.Visible = False
                        PanelInformacion.Visible = True
                        PanelOpciones.Visible = True
                        PanelArmazon.Enabled = False
                        PanelRx.Enabled = True
                        PanelContorno.Enabled = False
                        Cancel.Enabled = True
                    Else
                        InitValues()
                        IsReceivingVirtualRx = True
                        FillRx(ds, False)
                        AceptarManual_Click(Me, e)
                    End If
                End If
            Catch ex As Exception
                MsgBox(ex.Message, MsgBoxStyle.Exclamation)
            Finally
                t.CloseConn()
            End Try
        End If

    End Sub
    Private Function BuscarCliente(ByVal cve As Integer)
        Dim cust As Integer = 0

        Try
            Dim t As New SqlDB(My.Settings.VantageServer, My.Settings.DBUser, My.Settings.DBPassword, My.Settings.MfgSysDBName)
            Dim ds As New Data.DataSet
            t.OpenConn()
            ds = t.SQLDS("select custid from customer where number03 = " & cve & " and company = 'TRECEUX'", "t1")
            If ds.Tables("t1").Rows.Count > 0 Then
                cust = ds.Tables("t1").Rows(0).Item("custid")
            Else
                cust = 0
            End If
            Return cust
        Catch ex As Exception
            If ex.Message.Contains("Error de Conexion") Then
                Me.ChangeWorkingStatus(WorkingStatusType.OffLine)
            Else
                MsgBox(ex.Message, MsgBoxStyle.Critical)
            End If
            Return ""
        End Try
    End Function
    Private Sub show_color(ByVal color As String)
        ComboTinte.Enabled = True
        CheckTintes.Checked = True
        Select Case color
            Case "NINGUNO"
                ComboTinte.Enabled = False
                CheckTintes.Checked = False
                ComboTinte.Texto = "NINGUNO"
            Case "GRIS"
                ComboTinte.Texto = "GRIS"
            Case "CAFE"
                ComboTinte.Texto = "CAFE"
            Case "ROSA"
                ComboTinte.Texto = "ROSA"
            Case "SAHARA"
                ComboTinte.Texto = "SAHARA"
            Case "VERDE"
                ComboTinte.Texto = "VERDE"
            Case "TINTO"
                ComboTinte.Texto = "TINTO"
            Case "AZUL MARINO"
                ComboTinte.Texto = "AZUL MARINO"
            Case "AZUL"
                ComboTinte.Texto = "AZUL"
            Case "UVA"
                ComboTinte.Texto = "UVA"
            Case "CIRUELA"
                ComboTinte.Texto = "CIRUELA"
            Case "AMARILLO"
                ComboTinte.Texto = "AMARILLO"
        End Select
    End Sub
    Private Sub show_tinte(ByVal tinte As Integer)
        Select Case tinte
            Case 0
                CheckLevel1.Checked = False
                CheckLevel2.Checked = False
                CheckLevel3.Checked = False
            Case 1
                CheckLevel1.Checked = True
                CheckLevel2.Checked = False
                CheckLevel3.Checked = False
            Case 2
                CheckLevel1.Checked = False
                CheckLevel2.Checked = True
                CheckLevel3.Checked = False
        End Select
    End Sub
    Private Sub show_Contorno(ByVal RadArray As String)
        Dim pos, i, radios(NbrsTraces()) As Integer
        Dim ScaleRatio, h, b As Double

        'recorremos los radios para guardarlos en el arreglo de radios
        pos = 1
        For i = 0 To NbrsTraces()
            radios(i) = RadArray.Substring(pos, 4)
            pos = pos + 5
        Next

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

        RadiosR = radios
        MirrorRadios(Pastilla.Sides.Right)

        h = (PreviewTrazo.Height / (radios(X1) + radios(X1 + X2)))
        b = (PreviewTrazo.Width / (radios(0) + radios(X2)))
        If b < h Then ScaleRatio = b - 0.002 Else ScaleRatio = h - 0.002
        Dim Preview As New Lente(Lente.Sides.Right, radios, ScaleRatio)
        Dim Trazo As New GraphicsPath
        Trazo = Preview.DrawLense(PreviewTrazo, New Point(PreviewTrazo.Width / 2 / ScaleRatio, -PreviewTrazo.Height / 2 / ScaleRatio))
        Preview.CalculaContorno()
        ContornoA.Text = Preview.Size.Width / 100
        ContornoB.Text = Preview.Size.Height / 100
        ContornoED.Text = Preview.Diagonal
        Dim G As Graphics = GetGraphicsObject(PreviewTrazo)

        h = (PreviewLente.Height / (radios(X1) + radios(X3)))
        b = (PreviewLente.Width / (radios(0) + radios(X2)))
        If b < h Then ScaleRatio = b - 0.002 Else ScaleRatio = h - 0.002

        Preview = New Lente(Lente.Sides.Right, radios, ScaleRatio)

        Trazo = Preview.DrawLense(PreviewLente, New Point(PreviewLente.Width / 2 / ScaleRatio, -PreviewLente.Height / 2 / ScaleRatio))
        Preview.CalculaContorno()
        G = GetGraphicsObject(PreviewLente)
        G.SmoothingMode = SmoothingMode.AntiAlias
        G.Clear(PreviewLente.BackColor)
        G.DrawPath(Pens.DarkBlue, Trazo)
        PreviewLente.Invalidate()
    End Sub
    Private Sub Genera_ReferenciaDig()
        Dim Mensaje As String
        Dim Conn As New SqlDB(My.Settings.LocalServer, My.Settings.DBUser, My.Settings.DBPassword, My.Settings.LocalDBName)
        Conn.OpenConn()
        Try
            Dim Comm As New SqlCommand("SP_TrazosManuales" & My.Settings.OpticalProtocol, Conn.SQLConn)
            Comm.CommandType = Data.CommandType.StoredProcedure
            Comm.Parameters.AddWithValue("@JobNum", RxID.Text)
            Comm.Parameters.Add("@JobUnico", Data.SqlDbType.VarChar, 100)
            Comm.Parameters("@JobUnico").Direction = Data.ParameterDirection.Output
            Comm.ExecuteNonQuery()
            Mensaje = Comm.Parameters("@JobUnico").Value
            RxWECO.Text = Mensaje
            RxWECO.Visible = True
            'Label5.Visible = True
        Catch ex As Exception
            MsgBox(ex.Message)
        Finally
            Conn.CloseConn()
        End Try
    End Sub

    Private Sub Button1_Click_1(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Update_Virtuales_WebOrders(Me, e)
        If WO.IsDisposed Then
            WO = New WebOrders
        End If
        'WO.DGCostco.DataSource = DGCostco.DataSource
        'WO.Location = New Point(Me.Location.X + Panel1.Location.X, Me.Location.Y)
        'WO.Show()
    End Sub

    Private Sub CancelarWebRxToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CancelarWebRxToolStripMenuItem.Click
        If PanelInformacion.Visible Then
            If MsgBox("Est� seguro de cancelar la captura?" & vbCrLf & "Los datos se perder�n.", MsgBoxStyle.YesNo + MsgBoxStyle.Question) = MsgBoxResult.Yes Then
                InitValues()
            End If
        Else
            InitValues()
        End If
    End Sub

    Private Sub StripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles StripMenuItem.Click
        If CheckLEye.Checked Then
            EsferaLeft.Text = EsferaRight.Text
            CilindroLeft.Text = CilindroRight.Text
            EjeLeft.Text = EjeRight.Text
            AdicionLeft.Text = AdicionRight.Text
        End If
    End Sub

    Private Sub CopiarAOjoDerechoToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        If CheckREye.Checked Then
            EsferaRight.Text = EsferaLeft.Text
            CilindroRight.Text = CilindroLeft.Text
            EjeRight.Text = EjeLeft.Text
            AdicionRight.Text = AdicionLeft.Text
        End If
    End Sub

    Private Sub GroupErrores_VisibleChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles GroupErrores.VisibleChanged, PanelErrores.VisibleChanged
        If PanelErrores.Visible Then
            PanelInformacion.Enabled = False
            PanelOpciones.Enabled = False
            PanelArmazon.Enabled = False
            PanelRx.Enabled = False
            ComboArmazon2.Enabled = False
        Else
            If RxNum.Text.Length < 2 Then RxNum.Text = RxNum.Text.PadLeft(6, "0")
            If Me.IsVirtualRx And (ARLab = LabID()) And (CInt(RxNum.Text.Substring(0, 2)) <> ARLab) Then
                'cuando es una modificacion de una rx virtual, quedara habilitado todo para ser cambiodo  exeptio el cliente
                '--- Ricardo/Marco 7/jun/2007
                'PanelGraduacion.Enabled = False
                'PanelOpciones.Enabled = False
                PanelRx.Enabled = False
            Else
                PanelOpciones.Enabled = True
                PanelArmazon.Enabled = True
                PanelRx.Enabled = True
                ComboArmazon2.Enabled = True
            End If
            PanelInformacion.Enabled = True

        End If
    End Sub

    Private Sub AceptarErrores_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles AceptarErrores.Click, AceptarErroresNew.Click
        Dim OKToModify As Boolean = True
        Dim Reason As String = ""
        'If My.Settings.QAEnabled Then
        '    If CheckQAStatus(RxNum.Text) = QAStatus.PC And ComboErrores.SelectedValue = my.settings.recapturasp_id Then
        '        OKToModify = False
        '        Reason = "La Rx ya est� lista para cerrar, ya no se permiten modificaciones a menos que sean errores."
        '    End If
        'End If
        If OKToModify Then
            PanelErrores.Visible = False
            AceptarManualNew.Visible = False
            EsferaRight.Select()

            'If NoTrace Then        ' Asi estaba. Actualizado por fgarcia. March 23,2011.
            If NoTrace And Not IsManualRx Then
                CambiarTrazosToolStripMenuItem_Click(Me, e)
            End If
            MyFrame.ErrorID = FrameErrorID
            If FrameErrorID > 0 And PNL_Armazon.Visible = True Then
                IsFrameOK = False
                MyFrame.ReadStatus = Frames.ThisFrameReadStatus.NotRead
                MyFrame.ErrorID = FrameErrorID

                PIC_Armazon.Image = My.Resources.Lens_Error_2
                PanelGraduacion.Enabled = False
                PanelContorno.Enabled = False
                PanelOpciones.Enabled = False
                PanelRightEye.Enabled = False
                PanelLeftEye.Enabled = False
                PanelRx.Enabled = False
                PanelArmazon.Enabled = False
                ComboArmazon2.Enabled = False
                PNL_Armazon.Enabled = True
            Else
                PanelRightEye.Enabled = True
                PanelLeftEye.Enabled = True
                PNL_Armazon.Enabled = False
            End If
        Else
            MsgBox(Reason, MsgBoxStyle.Exclamation)
        End If
    End Sub

    Private Sub AlturaRight_Leave_1(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MonoRight.Leave, MonoLeft.Leave, AlturaRight.Leave, AlturaLeft.Leave, DIPLejos.Leave, DIPCerca.Leave, Altura.Leave, ContornoPuente.Leave, ContornoED.Leave, ContornoB.Leave, ContornoA.Leave
        Dim tb As TextBox = sender
        If tb.Text = "" Then

            tb.Text = 0
        End If
        If tb.Name = DIPLejos.Name Or tb.Name = DIPCerca.Name Then
            If MonoLeft.Text = MonoRight.Text Then
                MonoLeft.Text = 0
                MonoRight.Text = 0
            End If
        End If
        If tb.Name = MonoLeft.Name Then
            Altura.Focus()
        End If
        If tb.Name = DIPCerca.Name Then
            If ContornoA.Enabled Then
                ContornoA.Focus()
            Else
                ContornoPuente.Focus()
            End If
        End If
    End Sub

    Private Sub CambiarTrazosToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CambiarTrazosToolStripMenuItem.Click
        Dim OKToModify As Boolean = True
        Dim Reason As String = ""
        If My.Settings.QAEnabled And IsModifying Then
            If CheckQAStatus(RxNum.Text) = QAStatus.PC And ComboErrores.SelectedValue = My.Settings.RecapturaSP_ID Then
                OKToModify = False
                Reason = "La Rx ya est� lista para cerrar, ya no se permiten modificaciones de trazos."
                ChangeTraceDenied = True
            Else
                ChangeTraceDenied = False
                'PFL Oct 16 2012
                'Permite al usuario hacer los cambios necesarios
                'Nueva filosof�a por indicaciones del Dr. Machado.
                'M�s flexibe. Nudo Gorgiano. Ya me est� gustando la idea de darle un espadazo al LabRx

                'PanelErrores.Visible = True

            End If
        End If
        If Not OKToModify Then
            MsgBox(Reason, MsgBoxStyle.Exclamation)
        Else
            'If Not PanelErrores.Visible Then
            ComboTrazos.Enabled = True

            PanelRx.Enabled = True              ' Este se agrego
            RadioManualNew.Enabled = True
            RadioDigitalNew.Enabled = True

            PanelArmazon.Enabled = True         ' Este se agrego
            AceptarManualNew.Enabled = True     ' Este se agrego
            AceptarDigitalNew.Enabled = True

            ChangingTraces = True
            RadioDigitalNew.Checked = True
            BuscarTrazos_Click(sender, e)
            'GroupTrazos.Visible = True
            PanelInformacion.Enabled = False
            PanelPreviewTrazo.Visible = True
            AceptarDigitalNew.Visible = True

            If NoTrace Then
                MsgBox("Como esta receta no tiene trazo, es necesario que se escoja uno nuevo", MsgBoxStyle.Information)
                TextLabClient.Enabled = False
                RxID.Enabled = False
                PanelArmazon.Enabled = True
                PanelRx.Enabled = True

            End If

            'End If

        End If

    End Sub

    Private Sub CancelarDigital_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CancelarDigitalNew.Click
        If ChangingTraces Then
            ChangingTraces = False
            'GroupTrazos.Visible = False
            PanelInformacion.Enabled = True
            'BuscarRecetaExistente(RxWECO.Text)
            If (My.Settings.Plant = "VBENS") Then
                BuscarRecetaExistente(RxID.Text)
            Else
                BuscarRecetaExistente(RxWECO.Text)
            End If
            PanelInformacion.Visible = True
        Else
            InitValues()
        End If
    End Sub
    Private Function Actualiza_Clientes() As Boolean
        Dim CmdSucess As Boolean = True

        Dim t As New SqlDB(Laboratorios.ConnStr) 'ChooseServer, My.Settings.DBUser, My.Settings.DBPassword, DataBase)

        Dim ds As New DataSet
        Dim str As String = "select custid + ' ' + name AS name, custnum, name AS Expr1 from customer with(nolock) where number01 = " & LabID() & " or number02 = " & LabID() & " order by name"
        'Dim str As String = "select name, custnum, name AS Expr1 from customer with(nolock) where number01 = " & LabID() & " or number02 = " & LabID() & " order by name"
        Try
            t.OpenConn()
            ds = t.SQLDS(str, "t1")
            ComboClients.DataSource = ds.Tables("t1")
            ComboClients.DisplayMember = "name"
            ComboClients.ValueMember = "custnum"
            If ds.Tables("t1").Rows.Count > 0 Then
                ComboClients.SelectedIndex = 0
            Else
                MsgBox("No se encontraron clientes." + vbCrLf + "�Revis� bien los ajustes del n�mero de planta y la base de datos local?", MsgBoxStyle.Critical, "Error en ajustes de Configuraci�n")
            End If
            'If WorkingOnLine Then
            'ds = t.SQLDS("select * from erpmaster.dbo.ErrorRel order by description", "t1")
            'Else
            ds = t.SQLDS("select * from ErrorRel order by description", "t1")
            'End If

            ComboErrores.DataSource = ds.Tables("t1")
            ComboErrores.DisplayMember = "description"
            ComboErrores.ValueMember = "errorid"
            ComboErrores.SelectedValue = 27

            If DataBase = My.Settings.MfgSysDBName Then
                ChangeWorkingStatus(WorkingStatusType.OnLine)
            End If

            'si no hay error, si se puede obtener info del server
            Return CmdSucess

        Catch ex As Exception
            If ex.Message.Contains("Error de Conexion") Then
                Me.ChangeWorkingStatus(WorkingStatusType.OffLine)
            Else
                MsgBox(ex.Message, MsgBoxStyle.Critical)
            End If

            'si ocurrio un error consultamos de la tabla local los clientes
            CmdSucess = False
            'ChangeWorkingStatus(WorkingStatusType.OffLine)
            'RefreshOnlineIndicator()
        Finally
            t.CloseConn()
        End Try
    End Function

    Private Sub ActualizaClientesToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ActualizaClientesToolStripMenuItem.Click
        If PanelInformacion.Visible = False Then
            Actualiza_Clientes()
        End If

    End Sub
    Private Sub Muestra_CostoOrden(ByVal VantageOrder As String)
        'Dim frm_cotizacion As New Cotizacion

        'frm_cotizacion.VantageOrder = VantageOrder
        'frm_cotizacion.ShowDialog()

    End Sub
    Private Function Cotizacion() As Boolean
        Try

            If ComboClients.Text.IndexOf("COSTCO") < 0 Then
                Pedido = ""
                Dim MyValue As ItemWithValue
                Dim GenLPNStr As String = ""
                Dim GenRPNStr As String = ""
                If (ListLeftParts.SelectedIndex > -1) Then
                    MyValue = ListLeftParts.Items(ListLeftParts.SelectedIndex)
                    GenLPNStr = GetGenericPartNum(MyValue.Value)
                    If Val(MyValue.Value) > 0 Then
                        Pedido = MyValue.Value & ",1"
                    End If
                ElseIf OriginalLeftPart <> "" Then
                    GenLPNStr = GetGenericPartNum(OriginalLeftPart)
                    Pedido = OriginalLeftPart & ",1"
                End If
                If ListRightParts.SelectedIndex > -1 Then
                    MyValue = ListRightParts.Items(ListRightParts.SelectedIndex)
                    GenRPNStr = GetGenericPartNum(MyValue.Value)
                    If Val(MyValue.Value) > 0 Then
                        If Pedido <> "" Then
                            Pedido &= ","
                        End If
                        Pedido &= MyValue.Value & ",1"
                    End If
                ElseIf OriginalRightPart <> "" Then
                    GenRPNStr = GetGenericPartNum(OriginalRightPart)
                    Pedido = OriginalRightPart & ",1"
                End If
                If Pedido <> "" Then
                    'Dim GenPNStr As String = ""
                    'Dim QtyStr As Integer = 0
                    GenPNStr = ""
                    QtyStr = 0
                    If GenLPNStr = GenRPNStr Then
                        If GenLPNStr <> "" Then
                            GenPNStr = GenLPNStr
                            QtyStr = 2
                        Else
                            QtyStr = 0
                        End If
                    Else
                        If GenLPNStr <> "" Then
                            GenPNStr = GenLPNStr
                        Else
                            GenPNStr = GenRPNStr
                        End If
                        QtyStr = 1
                    End If
                End If
                AddOpciones(False)
                Dim frm_cotizacion As New Cotizacion

                'frm_cotizacion.VantageOrder = VantageOrder
                frm_cotizacion.Pedido = Pedido
                frm_cotizacion.CustNum = ComboClients.SelectedValue
                frm_cotizacion.ShowDialog()
                If frm_cotizacion.CotiAceptada Then
                    Return True
                Else
                    Return False
                End If
            Else
                Return True
            End If
        Catch ex As Exception
            MsgBox("Error al Crear la Cotizacion." & vbCrLf & ex.Message)
            Return False
        End Try
    End Function
    ' Pedro Far�as L.  Nov 28 2012
    ' "Por alguna raz�n"  esta rutina debe de usar el procedimiento nuevo para crear orden SP_ORDERENTRYTRECEUX_V4
    ' EL SP SP_ORDERENTRYTRECEUX_V4 ahora da prioridad al valor de @COSTCOLENTE para indicar que la orden incluye un paquete AUGEN AIR
    ' y que debe usar �nicamente el valor del precio del paquete. Todos los dem�s materiales van con precio 0.
    Private Function Crear_Orden_Job(ByVal PNStr As String, ByVal QtyStr As Integer, ByVal RevStr As String, ByVal JNStr As String, ByVal svr As String, ByVal db As String) As String
        Dim Ordernum As String = ""
        Dim VntgOrder As String = ""
        Dim AR As Integer = 0
        Dim Tono As Integer = 1
        Dim Color As Integer = 0
        Dim Material As Integer
        Dim Dise�o As Integer
        Dim Param As New SqlClient.SqlParameter
        Dim MsgError As String = ""
        Dim t As SqlDB = Nothing
        Dim Conn As SqlDB = Nothing
        Dim Lados As Integer
        Dim Comentarios_Param As String
        Dim LabNum As Integer = LabID()
        Dim part As String = ""
        Dim Failed As Boolean = False
        Dim FailedMessage As String = ""

        Dim TranMfgsys As SqlClient.SqlTransaction = Nothing
        Dim TranLocalLab2000 As SqlClient.SqlTransaction = Nothing

        If (CheckREye.Checked) And (Not CheckLEye.Checked) Then
            Lados = 1
        Else
            Lados = 0
        End If

        Try
            t = New SqlDB(My.Settings.LocalServer, My.Settings.DBUser, My.Settings.DBPassword, My.Settings.LocalDBName)
            t.SQLConn = New SqlConnection(Laboratorios.ConnStr)
            t.OpenConn()
            TranLocalLab2000 = t.SQLConn.BeginTransaction

            Conn = New SqlDB(svr, My.Settings.DBUser, My.Settings.DBPassword, db) 'NOS CONECTAMOS AL SERVIDOR LOCAL O REMOTO
            Conn.SQLConn = New SqlConnection(Laboratorios.ConnStr)
            Conn.OpenConn()

            TranMfgsys = Conn.SQLConn.BeginTransaction

            Ordernum = GetNextOrderNumber(t, TranLocalLab2000)

            Try
                Dim ds As DataSet
                ds = t.SQLDS("select distinct upper(b.descripcion) as label,b.showinrxlabel as isspeciallabel from dbo.tblpaquetesdtl a with(nolock) inner join tblpaquetescadenas b with(nolock) on a.idpaquete = b.idpaquete where a.idpaqueteitem = " & NumArmazon, "t1", TranLocalLab2000)
                If ds.Tables("t1").Rows.Count > 0 Then
                    If ds.Tables("t1").Rows(0).Item("isspeciallabel") = True Then
                        HasSpecialLabel = True
                        SpecialLabel = ds.Tables("t1").Rows(0).Item("label")
                    End If
                End If
            Catch ex As Exception
                Throw New Exception(ex.Message)
            Finally
            End Try


            Dim Comm As SqlCommand
            Comm = New SqlCommand("SP_OrderEntryTRECEUX_V4", Conn.SQLConn)
            Comm.Transaction = TranMfgsys
            Comm.CommandType = CommandType.StoredProcedure
            Comm.CommandTimeout = My.Settings.DBCommandTimeout


            'PARAMETROS DEL STORED PROCEDURE PARA CREAR LA ORDEN DE VENTA
            Comm.Parameters.AddWithValue("@compania", "TRECEUX")
            Comm.Parameters.AddWithValue("@Plant", My.Settings.Plant)
            Comm.Parameters.AddWithValue("@ordernum", Ordernum)
            'POR SI SE MODIFICA EL CLIENTE DE LA ORDEN SE ACTUALIZA TAMBIEN
            If IsReceivingVirtualRx Then
                Comm.Parameters.AddWithValue("@cliente", OriginalCustNum)
            Else
                If Not Convert.ToString(Ordernum).StartsWith(LabNum) Then '  ComboClients.SelectedValue = Nothing Then
                    ComboClients.SelectedIndex = -1
                    Comm.Parameters.AddWithValue("@cliente", OriginalCustNum)
                Else
                    Comm.Parameters.AddWithValue("@cliente", ComboClients.SelectedValue)
                End If
            End If
            '*********************************************************************

            Comm.Parameters.AddWithValue("@entryperson", My.User.Name)

            If IsVirtualRx Then
                Comm.Parameters.AddWithValue("@ponum", ARLab)       ' Marcar una Rx como Virtual
            Else
                Comm.Parameters.AddWithValue("@ponum", "")          ' Marcar una Rx como Virtual
            End If

            Comm.Parameters.AddWithValue("@descto", 0)            ' Cantidad de descuento a aplicar en la orden
            Comm.Parameters.AddWithValue("@lado", Lados)          ' 0: Ambos o solo Izquierdo     1: Solo Derecho
            Comm.Parameters.AddWithValue("@fecEnvio", Now.Date)
            Comm.Parameters.AddWithValue("@shiptonum", "")          'ShipTo.SelectedValue)
            Comm.Parameters.AddWithValue("@mensajeria", "MULP")
            Comm.Parameters.AddWithValue("@parametros", Pedido)
            Comentarios_Param = TextComentarios.Text

            If RxType = RxTypes.WebRx Then
                Comentarios_Param = Comentarios_Param & "[WEB_RX]"
            End If

            If IsGratisRx Then
                Comentarios_Param = Comentarios_Param & "[FREE_RX]"
            End If

            If IsGarantia Then
                Comentarios_Param = Comentarios_Param & "[GARANTIA_RX]"
            End If

            If Me.IsVirtualRx And Not Me.IsLocalRx Then
                If CheckREye.Checked Then
                    Comentarios_Param = Comentarios_Param & "[HAS_R_EYE]"
                End If
                If CheckLEye.Checked Then
                    Comentarios_Param = Comentarios_Param & "[HAS_L_EYE]"
                End If
            End If

            Comm.Parameters.AddWithValue("@Comentarios", Comentarios_Param)

            If CheckAR.Checked Then
                AR = 1
            ElseIf CheckARGold.Checked Then
                AR = 2
            ElseIf CheckARMatiz.Checked Then
                AR = 3
            End If

            If ComboTinte.SelectedValue <> 0 Then
                If CheckLevel1.Checked Then
                    Tono = 1
                ElseIf CheckLevel2.Checked Then
                    Tono = 2
                ElseIf CheckLevel3.Checked Then
                    Tono = 3
                End If
                Color = ComboTinte.SelectedValue
            End If

            Comm.Parameters.AddWithValue("@OrdenLab", RxID.Text)

            If ChangingTraces Then
                Comm.Parameters.AddWithValue("@RefDigital", ComboTrazos.ComboBox1.SelectedValue)
            Else
                Comm.Parameters.AddWithValue("@RefDigital", Val(RxWECO.Text))
            End If

            GetMaterialDesign(Material, Dise�o)
            Comm.Parameters.AddWithValue("@TrazadoArmazon", Convert.ToByte(RadioDigitalNew.Checked))
            Comm.Parameters.AddWithValue("@TipoArmazon", ComboArmazon2.ComboBox1.SelectedIndex + 1)
            Comm.Parameters.AddWithValue("@Biselado", Convert.ToByte(CheckBiselado.Checked))
            Comm.Parameters.AddWithValue("@Antireflejante", AR)
            Comm.Parameters.AddWithValue("@Color", Color)
            Comm.Parameters.AddWithValue("@Gradiente", Convert.ToByte(RadioGradiente.Checked))
            Comm.Parameters.AddWithValue("@Tono", Tono)
            Comm.Parameters.AddWithValue("@EsferaR", CDbl(EsferaRight.Text))
            Comm.Parameters.AddWithValue("@CilindroR", CDbl(CilindroRight.Text))
            Comm.Parameters.AddWithValue("@EjeR", CDbl(EjeRight.Text))
            Comm.Parameters.AddWithValue("@AdicionR", CDbl(AdicionRight.Text))
            Comm.Parameters.AddWithValue("@EsferaL", CDbl(EsferaLeft.Text))
            Comm.Parameters.AddWithValue("@CilindroL", CDbl(CilindroLeft.Text))
            Comm.Parameters.AddWithValue("@EjeL", CDbl(EjeLeft.Text))
            Comm.Parameters.AddWithValue("@AdicionL", CDbl(AdicionLeft.Text))
            Comm.Parameters.AddWithValue("@AlturaR", CDbl(AlturaRight.Text))
            Comm.Parameters.AddWithValue("@AlturaL", CDbl(AlturaLeft.Text))
            Comm.Parameters.AddWithValue("@MonoR", CDbl(MonoRight.Text))
            Comm.Parameters.AddWithValue("@MonoL", CDbl(MonoLeft.Text))
            Comm.Parameters.AddWithValue("@DIPle", CDbl(DIPLejos.Text))
            Comm.Parameters.AddWithValue("@DIPce", CDbl(DIPCerca.Text))
            Comm.Parameters.AddWithValue("@Altura", CDbl(Altura.Text))

            Try
                Comm.Parameters.AddWithValue("@A", CDbl(ContornoA.Text))
                Comm.Parameters.AddWithValue("@B", CDbl(ContornoB.Text))
                Comm.Parameters.AddWithValue("@ED", CDbl(ContornoED.Text))
            Catch ex As Exception
                Comm.Parameters.AddWithValue("@A", 0)
                Comm.Parameters.AddWithValue("@B", 0)
                Comm.Parameters.AddWithValue("@ED", 0)
            End Try

            Comm.Parameters.AddWithValue("@Puente", CDbl(ContornoPuente.Text))
            Comm.Parameters.AddWithValue("@Material", Material)
            Comm.Parameters.AddWithValue("@Diseno", Dise�o)
            Comm.Parameters.AddWithValue("@ArmazonID", NumArmazon)
            Comm.Parameters.AddWithValue("@Status", 0)
            Comm.Parameters.AddWithValue("@Foco", Convert.ToByte(PanelOblea.Enabled))
            Comm.Parameters.AddWithValue("@Coated", Convert.ToByte(CheckRLX.Checked))
            Comm.Parameters.AddWithValue("@Abrillantado", Convert.ToByte(CheckAbril.Checked))

            ' BANDERA_OPTIMIZAR
            'If SpecialDesignID = 1 Then
            '    Comm.Parameters.AddWithValue("@Optimizado", True)
            'Else
            '    Comm.Parameters.AddWithValue("@Optimizado", False)
            'End If

            If SD_Partnum.ToUpper() = "HDRX" Then
                Comm.Parameters.AddWithValue("@Optimizado", True)
            Else
                Comm.Parameters.AddWithValue("@Optimizado", False)
            End If

            Comm.Parameters.AddWithValue("@PrismaR", PrismaR)
            Comm.Parameters.AddWithValue("@EjePrismaR", EjePrismaR)
            Comm.Parameters.AddWithValue("@PrismaL", PrismaL)
            Comm.Parameters.AddWithValue("@EjePrismaL", EjePrismaL)
            '----------------------------------
            Comm.Parameters.AddWithValue("@GrosCenD", GrosorCentralD)
            Comm.Parameters.AddWithValue("@GrosCenI", GrosorCentralI)
            '----------------------------------
            Comm.Parameters.AddWithValue("@CostcoLente", CostcoLente)

            'Comm.Parameters.AddWithValue("@CostcoAR", CostcoAR)
            ' Esta campo ya no se usa para Costco y se tomara para los dise�os especiales [character07 de orderhed]
            Comm.Parameters.AddWithValue("@CostcoAR", SpecialDesignID)
            Comm.Parameters.AddWithValue("@CostcoTinte", CostcoTinte)
            Comm.Parameters.AddWithValue("@CostcoPulido", CostcoAbrillantado)
            '----------------------------------

            ' ---------------------------------------------------------------------------------
            ' Campos para marcar las fechas en las ordenes con que entraron tanto web como fisicamente
            ' ---------------------------------------------------------------------------------
            Comm.Parameters.AddWithValue("SeqID", SeqID)
            Comm.Parameters.AddWithValue("FechaEntradaWeb", FehcaEntradaWeb)
            Comm.Parameters.AddWithValue("FechaEntradaFisica", FechaEntradaFisica)
            ' ---------------------------------------------------------------------------------
            If svr = My.Settings.LocalServer Then
                Comm.Parameters.AddWithValue("@Sincronizada", SavedInRemoteServer)
            End If

            ' No funciona correctamente este parametro porque se guarda en el campo satpickup de orderhed pero es de tipo bit y no tipo
            ' tinyint como suponia RL. Se quitara eventualmente este campo.
            Comm.Parameters.AddWithValue("@SpecialDesignID", SpecialDesignID)

            'Dim ProdCode As String = "CRVST"
            'Comm.Parameters.AddWithValue("Company", "TRECEUX")
            'Comm.Parameters.AddWithValue("PNStr", PNStr)
            'Comm.Parameters.AddWithValue("JNStr", JNStr)
            'Comm.Parameters.AddWithValue("QtyStr", QtyStr)
            'Comm.Parameters.AddWithValue("RevStr", RevStr)
            'Comm.Parameters.AddWithValue("StartDate", Now.Date)
            'Comm.Parameters.AddWithValue("ProdCode", ProdCode)


            Comm.Parameters.Add("@mensaje", Data.SqlDbType.VarChar, 100)
            Comm.Parameters("@mensaje").Direction = Data.ParameterDirection.Output

            'GENERAMOS EL PARAMETRO DE SALIDA
            Param.ParameterName = "@retval"
            Param.Direction = ParameterDirection.ReturnValue
            Comm.Parameters.Add(Param)

            'EXECUTAMOS EL PROCEDIMIENTO
            Try
                Comm.ExecuteNonQuery()

            Catch ex As Exception
                Throw ex
                'MessageBox.Show(ex.Message, Comm.Parameters("@retval").Value.ToString())
            End Try

            'VERIFICAMOS SI NO HUBO NINGUN ERROR
            If Comm.Parameters("@retval").Value = 0 Then
                'todo salio bien
                Dim mensaje As String = Comm.Parameters("@mensaje").Value
                Ordernum = mensaje.ToString

                '--------------------------------------------------------------------------------
                ' Aqui se ingresan los campos en la bitacora
                '--------------------------------------------------------------------------------
                'COMENTO RENEE AGOSTO 2012
                'DESCOMENTO PEDRO FARIAS NOVIEMBRE 2012

                If My.Settings.EnableBitacora Then
                    Dim log As New BitacoraLabRx(Ordernum, RxID.Text, My.User.Name.ToString)
                    Dim SQLStr As String = "select coalesce(date10,'1/1/1900') as date10,coalesce(date11,'1/1/1900') as date11,checkbox17,checkbox18,checkbox08,checkbox09,coalesce(character01,'') as character01,date03,checkbox11,date06,checkbox16 from orderhed with(nolock) where ordernum = " & Ordernum
                    'Dim tt As New SqlDB()
                    'tt.SQLConn = New SqlConnection(Laboratorios.ConnStr)
                    Try
                        Dim ds As New DataSet()
                        ds = Conn.SQLDS(SQLStr, "t1", TranMfgsys)
                        With ds.Tables("t1").Rows(0)
                            If .Item("date10") > "1/1/2000" Then log.AddEntradaWeb(.Item("date10"), "")
                            If .Item("date11") > "1/1/2000" Then log.AddEntradaFisica(.Item("date11"), "")
                            log.AddCaptura(Now.ToString, TextComentarios.Text)
                            If .Item("checkbox17") = 1 Then log.AddCanjePuntos(Now.ToString, "")
                            If .Item("checkbox18") = 1 Then log.AddGarantia(Now.ToString, "")
                            If (.Item("checkbox09") = 1) Then
                                If (LabID() <> .Item("character01")) Then log.AddEnvioVirtual(Now.ToString, "Lab. " & .Item("character01"))
                            End If

                            If Not log.InsertBitacora(Conn, TranMfgsys) Then
                                Throw New Exception(log.FailedString)
                            End If

                        End With

                    Catch ex As Exception
                        Throw New Exception(ex.Message)
                    Finally
                    End Try
                End If

                '--------------------------------------------------------------------------------
                ' Aqui se guarda el registro en QA
                '--------------------------------------------------------------------------------
                If My.Settings.QAEnabled Then
                    RegistroQA(Ordernum, False, Conn.SQLConn, TranMfgsys)
                End If

                ' ------------------------------------------------
                ' ---------- DESCUENTO LOCAL DE PASTILLAS --------
                ' ------------------------------------------------
                Dim ExecuteConsumePastilla As Boolean = False

                OriginalLeftPart = ""
                OriginalRightPart = ""

                ExecuteConsumePastilla = False

                If ARLab = LabNum And mensaje.StartsWith(LabNum) Then
                    ExecuteConsumePastilla = True
                ElseIf (ARLab = LabNum) Or (ARLab = 0) Then
                    ExecuteConsumePastilla = True
                End If

                If ExecuteConsumePastilla Then
                    Comm = New SqlCommand("SP_ConsumePastillasTRECEUX", Conn.SQLConn)
                    Comm.Transaction = TranMfgsys
                    Comm.CommandType = CommandType.StoredProcedure
                    'PFL Dic 26 2012
                    'El timeout ya viene ajustado a 30
                    'Comm.CommandTimeout = My.Settings.DBCommandTimeout
                    Comm.Parameters.AddWithValue("Ordernum", Ordernum)
                    Comm.Parameters.AddWithValue("OrdenLab", RxID.Text)

                    If ChangingTraces Then
                        Comm.Parameters.AddWithValue("RxNum", ComboTrazos.ComboBox1.SelectedValue)
                    Else
                        Comm.Parameters.AddWithValue("RxNum", Val(RxWECO.Text))
                    End If

                    Comm.Parameters.AddWithValue("TranReference", "Consumo de Pastillas por Creacion de Orden")

                    Dim x As ItemWithValue = ListLeftParts.SelectedItem
                    If CheckLEye.Checked And (((IsModifying = True) And (ComboErrores.Text <> "RECAPTURAS SP")) Or (IsModifying = False)) Then
                        x = ListLeftParts.SelectedItem
                        Comm.Parameters.AddWithValue("Izquierdo", x.Value)
                    Else
                        Comm.Parameters.AddWithValue("Izquierdo", "")
                    End If

                    If CheckREye.Checked And (((IsModifying = True) And (ComboErrores.Text <> "RECAPTURAS SP")) Or (IsModifying = False)) Then
                        x = ListRightParts.SelectedItem
                        Comm.Parameters.AddWithValue("Derecho", x.Value)
                    Else
                        Comm.Parameters.AddWithValue("Derecho", "")
                    End If

                    Comm.Parameters.AddWithValue("plant", Read_Registry("Plant"))
                    Comm.Parameters.AddWithValue("EntryPerson", "Lab: " & LabNum)
                    Comm.Parameters.Add("@Mensaje", Data.SqlDbType.VarChar, 30)
                    Comm.Parameters("@Mensaje").Direction = Data.ParameterDirection.Output
                    Comm.ExecuteNonQuery()
                End If
                ' ------ TERMINA EL DESC. LOCAL PARA ORDENES NUEVAS ---------

                ' ------ DESCUENTA ARMAZONES DEL INVENTARIO PARA ORDENES NUEVAS -----
                Dim CheckOurInventory As Boolean = MyFrame.CheckInventoryFrameFamily()


                If MyFrame.FrameStatus = Frames.ThisFrameStatus.[New] Or MyFrame.FrameStatus = Frames.ThisFrameStatus.Changed Then ' Se agrego Paquete/Armazon

                    If CheckOurInventory Then
                        Try : part = NumArmazon.ToString.Substring(0, 5) : Catch : part = 0 : End Try

                        Dim str As String = "SELECT plantwhse.primbin, plantwhse.warehousecode, isnull(partbin.onhandqty,0) as onhandqty " & _
                            "FROM plantwhse WITH(NOLOCK) " & _
                            "LEFT OUTER JOIN partbin WITH(NOLOCK) ON partbin.partnum = plantwhse.partnum " & _
                            "WHERE plantwhse.company='TRECEUX' AND plantwhse.partnum='" & part & _
                            "' AND plantwhse.plant='" & My.Settings.Plant & "'"

                        Dim dsWhse As DataSet
                        dsWhse = t.SQLDS(str, "t1", TranLocalLab2000)

                        Try
                            If part <> 0 Then
                                str = "EXEC SP_Qty_AdjustmentTRECEUX 'TRECEUX'," & Ordernum & "," & RxID.Text & "," & RxWECO.Text & ",'" & part & "','" & Now.Date & "','" & dsWhse.Tables("t1").Rows(0).Item("warehousecode") & "','" & dsWhse.Tables("t1").Rows(0).Item("primbin") & "',-1,'NEW','Descuento de Armaz�n orden local.',0,'" & My.User.Name & "',''"
                                t.Transaction(str, TranLocalLab2000)

                            End If
                        Catch ex As Exception
                            Throw New Exception("No se pudo actualizar inventario del armazon!" + vbCrLf + ex.Message)
                        End Try
                    End If
                End If
                ' ------ TERMINA EL DESC. ARMAZONES DEL INVENTARIO PARA ORDENES NUEVAS -----
                '----------------------------------------------------------------------
                ' Marco el trazo como ya usado
                '----------------------------------------------------------------------
                Dim NewComm As SqlCommand = Nothing
                Try
                    Dim Value As Integer = Convert.ToInt32(mensaje.Substring(2))

                    NewComm = New SqlCommand("/*update TblLastIndex set LastSO = " & Value & "*/;update " & TracerTable & " set status = 0 where jobnum like '" & Me.RxWECO.Text & "'", t.SQLConn)
                    NewComm.Transaction = TranLocalLab2000
                    NewComm.ExecuteNonQuery()
                Catch ex As Exception
                    Throw New Exception("Error al marcar el trazo." & vbCrLf & ex.Message)
                End Try

                '----------------------------------------------------------------------
                ' Marco la Rx Virtual con los ojos que debe mostrar en el lab. remoto
                '----------------------------------------------------------------------
                If IsVirtualRx Then
                    ' ------------------------------------------------
                    ' Codigo para decidir donde grabar el trazo virtual, si local o en el server
                    ' ------------------------------------------------
                    Dim Location As LabLocation
                    If svr = My.Settings.VantageServer Then
                        Location = LabLocation.Virtual
                    Else
                        Location = LabLocation.Local
                    End If
                    Me.SaveVirtualTraces(Conn, TranMfgsys, Location, VirtualRxAction.Create, Ordernum)
                    ' ------------------------------------------------

                    Try
                        NewComm = New SqlCommand("UPDATE orderhed set checkbox14 = " & Convert.ToInt16(Me.CheckLEye.Checked) & ", checkbox15 = " & Convert.ToInt16(Me.CheckREye.Checked) & " WHERE ordernum = " & mensaje, Conn.SQLConn)
                        NewComm.Transaction = TranMfgsys
                        NewComm.ExecuteNonQuery()
                    Catch ex As Exception
                        Throw New Exception("Error al actualizar datos de ojos seleccionados")
                    End Try

                End If

                '----------------------------------------------------------------------
                ' Grabo la Rx en el Local para asegurarme que exista y pueda facturar offline
                '----------------------------------------------------------------------
                If SavedInRemoteServer Then
                    Try
                        UpdateOrder(Ordernum, UpdateType.OrderAndLines, True)
                    Catch ex As Exception
                        SavedInRemoteServer = False
                        Throw New Exception("SavedInRemoteServer = false: " + ex.Message)
                    End Try
                End If
                ' ------------------------------------------------------------------

                TranMfgsys.Commit()
                TranLocalLab2000.Commit()
                OrderOK = True
                Return mensaje
            Else
                Select Case Comm.Parameters("@retval").Value
                    Case -1
                        MsgError = "No fue posible crear la orden de venta de la receta"
                    Case -2
                        MsgError = "No fue posible crear el trabajo de produccion de la receta"
                    Case Else
                        MsgError = "No se pudo crear la orden"

                End Select
                'TranMfgsys.Rollback()
                Throw New Exception(MsgError)
            End If
            'VntgOrder = Comm.Parameters("@mensaje").Value

        Catch ex As SqlException
            'If ex.Message.Contains("Error de Conexion") And ex.Server.ToString = My.Settings.VantageServer Then
            '    Me.ChangeWorkingStatus(WorkingStatusType.OffLine)
            'Else
            '    'MsgBox("Ocurrio una Excepcion al crear la Orden..." & vbCrLf & ex.Message, MsgBoxStyle.Critical)
            'End If

            TranMfgsys.Rollback()
            TranLocalLab2000.Rollback()
            Failed = True
            FailedMessage = ex.Message
            Throw New Exception("Ocurrio una Excepcion al crear la Orden..." & vbCrLf & ex.Message & " Server: [" & ex.Server.ToString & "]")
            Return "0"
        Catch ex As Exception
            TranMfgsys.Rollback()
            TranLocalLab2000.Rollback()
            Failed = True
            FailedMessage = ex.Message
            Return "0"

        Finally
            Conn.CloseConn()
            t.CloseConn()
            Conn = Nothing

            If Failed And FailedMessage.Contains("SavedInRemoteServer") Then
                Throw New Exception(FailedMessage)
            ElseIf Failed Then
                MsgBox("Ocurrio una Excepcion al crear la Orden..." & vbCrLf & FailedMessage, MsgBoxStyle.Critical)
            End If

        End Try
    End Function

    Private Sub RegistroQA(ByVal vtgorder As String, ByVal EsModificacion As Boolean, ByVal Conn As SqlConnection, ByVal tran As SqlTransaction)
        Dim QA As New RxQA
        Dim SelItem As ItemWithValue
        QA.orden = vtgorder
        QA.ordenlab = RxWECO.Text
        QA.LocalSvr = My.Settings.LocalServer
        QA.IsLocalRcv = Me.IsLocalReceive
        QA.Cliente = ComboClients.Text 'Me.PrintObj.Cliente
        If QA.Cliente = "" Then QA.Cliente = Me.TextLabClient.Text
        If CheckAR.Checked Or CheckARGold.Checked Or CheckARMatiz.Checked Then QA.AR = 1
        QA.EsMod = EsModificacion
        If CheckREye.Checked Then
            QA.esfd = EsferaRight.Text
            QA.cild = CilindroRight.Text
            QA.adcd = AdicionRight.Text
            QA.groscd = PrintObj.MoldeD.GrosorCentral 'GrosorCentralD
            QA.LadoD = True
            QA.lado = Asc("D")
            SelItem = ListRightParts.SelectedItem
            QA.ClaseD = GetCustomValues(CustomValues.Clase, SelItem.Value)
        End If
        If CheckLEye.Checked Then
            QA.esfi = EsferaLeft.Text
            QA.cili = CilindroLeft.Text
            QA.adci = AdicionLeft.Text
            QA.grosci = PrintObj.MoldeI.GrosorCentral 'GrosorCentralI
            QA.LadoI = True
            QA.lado = Asc("I")
            SelItem = ListLeftParts.SelectedItem
            QA.ClaseI = GetCustomValues(CustomValues.Clase, SelItem.Value)
        End If
        If CheckLEye.Checked And CheckREye.Checked Then QA.lado = Asc("A")

        QA.Save_QAInfo(Conn, tran)

        ''MANDAMOS A UN HILO LA ACCION DE GRABAR EN LA BD LOCAL
        'Dim myt As Threading.Thread
        'myt = New Threading.Thread(New Threading.ThreadStart(AddressOf QA.Save_QAInfo))
        'myt.Start()

    End Sub

    Private Sub btnretener_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RetenerRx.Click
        Dim retenerRx As New ReneterRx
        retenerRx.ShowDialog()
    End Sub

    Private Sub GroupOpciones_Enter(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PanelOpciones.Enter, ComboProgresivo.Enter, ComboOtros.Enter, ComboMonofocal.Enter, ComboBifocal.Enter, ListLeftParts.Enter, ListRightParts.Enter
        'LabelReadBarCodeL.Visible = False
        'LabelReadBarCodeR.Visible = False
        PanelLeftEye.BackgroundImage = My.Resources.VistaLeftEyePanel
        PanelRightEye.BackgroundImage = My.Resources.VistaRightEyePanel
    End Sub

    Private Function Existe_DefectoQA() As Boolean
        '//// buscamos en la base de datos local si existe capturado un defecto por la estacion de qa ////////
        Dim Existe As Boolean = False
        Dim Failed As Boolean = False
        Dim FailedMessage As String = ""

        Dim MyCnn As SqlClient.SqlConnection
        Dim Cmd As SqlClient.SqlCommand
        Dim Rdr As SqlClient.SqlDataReader
        Dim str As String

        MyCnn = New SqlClient.SqlConnection(Laboratorios.ConnStr)
        '"user ID=sa;password=proliant01;database=LocalLab2000;server=" & Read_Registry("TracerDataServer") & ";Connect Timeout=30")
        MyCnn.Open()

        Try
            LBLDEFDER.Text = ""
            LBLDEFIZQ.Text = ""
            'MyCnn = New SqlClient.SqlConnection("user ID=" & My.Settings.DBUser & ";password =" & My.Settings.DBPassword & ";database=" & My.Settings.LocalDBName & ";server=" & My.Settings.LocalServer & ";Connect Timeout=10")
            'str = "select * from VwRxToMod where ordernum = " & RxNum.Text & " and numinsp = (select max(numinsp) from VwRxToMod where ordernum = " & RxNum.Text & ") "
            str = "select * from VwRxToMod where ordernum = " & RxNum.Text & " and numinsp = (select max(numinsp) from TblQADtl where ordernum = " & RxNum.Text & ") "
            Cmd = New SqlClient.SqlCommand(str, MyCnn)
            Rdr = Cmd.ExecuteReader()
            If Rdr.HasRows Then
                Existe = True
                While Rdr.Read()
                    'verificamos lado derecho
                    If Rdr("lado") = 68 Then
                        LBLDEFDER.Text = Rdr("defecto")
                    End If
                    'verificamos lado izquierdo
                    If Rdr("lado") = 73 Then
                        LBLDEFIZQ.Text = Rdr("defecto")
                    End If
                End While
            Else
                Existe = False
            End If
            Cmd.Dispose()
        Catch ex As Exception
            Existe = False
            Failed = True
            FailedMessage = "No fue posible desplegar el detalle de la orden." & vbCrLf & ex.Message
        Finally
            MyCnn.Close()
            If Failed Then
                MsgBox(FailedMessage, MsgBoxStyle.Critical)
            End If
        End Try
        Return Existe
    End Function

    Private Sub GroupModInfo_Enter(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PanelModInfo.Enter

    End Sub

    Private Sub ReceiveRx_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RexibeVirtualRx.Click
        Dim rrx As New ModificarRecetas
        Dim rx As Integer = 0
        rrx.Text = "Recepci�n de Rx Virtual"
        rrx.Label1.Text = "Recepci�n de Rx Virtual"
        rrx.PanelRecepcion.Visible = True
        rrx.ShowDialog()
        rrx.PanelRecepcion.Visible = False
        If rrx.Status = True Then
            'RxWECO.Text = rrx.RxID.Text
            'IsLocalReceive = True
            'BuscarRecetaExistente(RxWECO.Text)
            'AceptarModificar(RxID.Text)
            'If NoTrace Then
            '    CambiarTrazosToolStripMenuItem_Click(Me, e)
            'End If

            '   ChangingTraces = False
            IsLocalReceive = True
            AceptarModificar(rrx.RxID.Text)
            If RxWECO.Text <> "" Then
                If (My.Settings.Plant = "VBENS") Then
                    BuscarRecetaExistente(RxID.Text)
                Else
                    BuscarRecetaExistente(RxWECO.Text)
                End If
                'BuscarRecetaExistente(RxWECO.Text)
                'RxWECO.Text = rrx.RxID.Text
                If NoTrace Then
                    CambiarTrazosToolStripMenuItem_Click(Me, e)
                End If
            End If

        Else
            IsLocalReceive = False
        End If
    End Sub

    Private Sub CheckLevel1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
    End Sub

    Private Sub CheckLevel3_MouseUp(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles CheckLevel3.MouseUp
        If CheckLevel3.Checked Then
            CheckLevel2.Checked = False
            CheckLevel1.Checked = False
        End If
    End Sub

    Private Sub CheckLevel2_MouseUp(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles CheckLevel2.MouseUp
        If CheckLevel2.Checked Then
            CheckLevel1.Checked = False
            CheckLevel3.Checked = False
        End If
    End Sub

    Private Sub CheckLevel1_MouseUp(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles CheckLevel1.MouseUp
        If CheckLevel1.Checked Then
            CheckLevel2.Checked = False
            CheckLevel3.Checked = False
        End If
    End Sub

    Private Sub RadioManualNew_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RadioManualNew.CheckedChanged
        If RadioManualNew.Checked Then
            RadioDigitalNew.Checked = False
            PreviewTrazo.Image = My.Resources.ContornoManual
            PreviewLente.Image = My.Resources.ContornoManual
            PanelPreviewTrazo.Visible = False
            OMA_EyeSide = "B"
        Else
            Trazos_Click(Me, e)
            RadioDigitalNew.Checked = True
            PanelPreviewTrazo.Visible = True
        End If
    End Sub

    Private Sub PanelLeftEye_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SelectedLeftEye.Click, PanelLeftEye.Click, ListLeftParts.Click
        If CheckLEye.Checked Then PanelLeftEye.BackgroundImage = My.Resources.VistaLeftEyePanelSelected
        PanelRightEye.BackgroundImage = My.Resources.VistaRightEyePanel
    End Sub
    Private Sub PanelRightEye_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ListRightParts.Click, PanelRightEye.Click, ListRightParts.Click
        PanelLeftEye.BackgroundImage = My.Resources.VistaLeftEyePanel
        If CheckREye.Checked Then PanelRightEye.BackgroundImage = My.Resources.VistaRightEyePanelSelected
    End Sub

    Private Sub PanelLeftEye_Leave(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PanelLeftEye.Leave, SelectedLeftEye.Leave, ListLeftParts.Leave
        If Not PanelLeftEye.Focused And Not SelectedLeftEye.Focused And Not ListLeftParts.Focused Then
            PanelLeftEye.BackgroundImage = My.Resources.VistaLeftEyePanel
        End If
    End Sub
    Private Sub PanelRightEye_Leave(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PanelRightEye.Leave, SelectedRightEye.Leave, ListRightParts.Leave
        If Not PanelRightEye.Focused And Not SelectedRightEye.Focused And Not ListRightParts.Focused Then
            PanelRightEye.BackgroundImage = My.Resources.VistaRightEyePanel
        End If
    End Sub

    Private Sub ComboBoxLarge1_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ComboArmazon2.Load

    End Sub

    Private Sub Panel3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Panel3.Click
        Me.Close() 'End
    End Sub

    Private Sub Panel3_MouseEnter(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Panel3.MouseEnter
        ToolTip1.ToolTipTitle = ""
        ToolTip1.Show("Cerrar Aplicaci�n", Me.Panel3)

    End Sub

    Private Sub Panel3_MouseLeave(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Panel3.MouseLeave
        ToolTip1.Hide(Me.Panel3)
    End Sub

    Private Sub DespliegaComentariosToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles DespliegaComentariosToolStripMenuItem.Click
        If PanelInformacion.Visible Then
            If (Not IsModifying And Not IsReceivingVirtualRx) Or (IsModifying And Not IsReceivingVirtualRx) Or (IsModifying And ARLab = LabID()) Then
                If PanelComentarios.Enabled Then
                    PanelComentarios.Visible = Not PanelComentarios.Visible
                    If PanelComentarios.Visible Then
                        TextComentarios.ReadOnly = False
                    Else
                        TextComentarios.ReadOnly = True
                    End If
                End If
            End If
        End If
    End Sub

    Private Sub MarcarVirtualToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MarcarVirtualToolStripMenuItem.Click
        IsVirtualRx = Not IsVirtualRx
        If IsVirtualRx Then
            Dim al As New ARLabList
            al.Location = New Point(Me.Location.X + 60, Me.Location.Y + 490)
            al.Label1.Text = "Laboratorio Virtual"
            al.IsARLab = False
            If RxType = RxTypes.WebRx And PanelInformacion.Visible = False Then
                al.Location = New Point(Me.Location.X + 421, Me.Location.Y + 210)
            End If
            al.ARLabName = LabelARLab.Text.Substring(3)
            al.ShowDialog()
            ARLab = al.ComboARLab.SelectedValue
            ARLabName = al.ComboARLab.Text
            IsVirtualRx = True
            LabelARLab.Text = "VR " & ARLabName
            LabelARLab.Visible = True
            al.Dispose()
        Else
            ARLab = 0
            ARLabName = "NINGUNO"
            LabelARLab.Visible = False
        End If
    End Sub

    Private Sub TimerRefreshRxs_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TimerRefreshRxs.Tick
        'If WorkingOnLine Then
        pe.ActualizaProcesos_Click(Me, New EventArgs)
        'End If
    End Sub

    Private Sub RxID_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles RxID.KeyDown
        'If (e.KeyCode = Keys.NumPad0 Or e.KeyCode = Keys.Space) And RxID.Text.Length = 0 Then
        '    e.SuppressKeyPress = True
        'End If
        'ERR_Errores.SetError(RxID, "")
    End Sub

    Private Sub CheckGarantia_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CheckGarantia.CheckedChanged
        Dim cancelagarantia As Boolean = False
        If CheckGarantia.Checked And Not IsModifying Then
            If Not Me.IsReceivingVirtualRx And Not IsLocalReceive Then
                If MsgBox("Estas seguro de marcar esta Rx como garant�a?", MsgBoxStyle.YesNo + MsgBoxStyle.Question) = MsgBoxResult.Yes Then
                    'Dim t As New SqlDB(My.Settings.LocalServer, My.Settings.DBUser, My.Settings.DBPassword, My.Settings.LocalDBName)
                    'Dim ds As DataSet
                    'Try
                    '    t.OpenConn()
                    '    ds = t.SQLDS("select checkbox03 as IsCostco from customer with(nolock) where company = 'TRECEUX' and custnum = " & Me.ComboClients.SelectedValue, "t1")
                    '    If ds.Tables("t1").Rows(0).Item("IsCostco") = 1 Then
                    '        Dim x As New EditValues()
                    '        x.LabelValue.Text = "Introduce el #Item de COSTCO"
                    '        x.ShowDialog()
                    '        If x.Changed Then
                    '            If x.TextValue.Text <> "" Then
                    '                Try
                    '                    ds = t.SQLDS("select * from erpmaster.dbo.tblcostcopricelist with(nolock) where costco_item = '" & x.TextValue.Text & "'", "t1")
                    '                    If ds.Tables("t1").Rows(0).Item("costco_item") = x.TextValue.Text Then
                    '                        CostcoLente = x.TextValue.Text
                    '                    End If
                    '                Catch ex As Exception
                    '                    Throw New Exception("#Item de COSTCO inv�lido")
                    '                End Try
                    '            End If
                    '        End If
                    '    End If
                    'Catch ex As Exception
                    '    If ex.Message.Contains("inv�lido") Then
                    '        cancelagarantia = True
                    '        MsgBox(ex.Message, MsgBoxStyle.Critical)
                    '    End If
                    'Finally
                    '    t.CloseConn()

                    'End Try
                    'If Not cancelagarantia Then
                    'Else
                    '    CheckGarantia.Checked = False
                    'End If
                    IsGarantia = True
                Else
                    CheckGarantia.Checked = False
                End If
            Else
                IsGarantia = True
            End If
        Else
            IsGarantia = False
        End If
    End Sub

    Private Sub AProcesar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles AProcesar.Click
        IsCompleta = True
        Me.PanelTipoCaptura.Visible = False
    End Sub

    Private Sub Incompleta_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Incompleta.Click
        IsCompleta = False
        Me.PanelTipoCaptura.Visible = False
    End Sub

    Private Sub ComboClients_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles ComboClients.KeyPress
        If TextBox1.Text = "" Then
            TextBox1.Visible = True
            TextBox1.Focus()
            TextBox1.Text = e.KeyChar
            TextBox1.Select(1, 0)
            OverrideClientSelectionChanged = True
        End If
    End Sub

    Private Sub TextBox1_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles TextBox1.KeyPress
        Dim Found As Boolean = False
        'Aqui almacenar lo que se ha tecleado
        If e.KeyChar = Chr(13) Then
            Dim i As Integer
            For i = 0 To ComboClients.Items.Count - 1
                Dim dr As DataRowView = ComboClients.Items.Item(i)
                If dr.Item(0).ToString.StartsWith(TextBox1.Text & " ") Or dr.Item(0).ToString.ToUpper.Contains(" " & TextBox1.Text.ToUpper) Then
                    Found = True
                End If
            Next
            If Not Found Then
                MsgBox("El cliente " & TextBox1.Text.ToUpper() & " no se encuentra en el listado de clientes.", MsgBoxStyle.Critical)
            Else
            End If
            ClienteTecleado = ""
            TextBox1.Text = ""
            TextBox1.Visible = False
            ComboClients.Focus()
        Else
            Dim i As Integer
            For i = 0 To ComboClients.Items.Count - 1
                Dim dr As DataRowView = ComboClients.Items.Item(i)
                If dr.Item(0).ToString.StartsWith(TextBox1.Text & e.KeyChar) Or dr.Item(0).ToString.ToUpper.Contains(" " & (TextBox1.Text & e.KeyChar).ToUpper) Then
                    Found = True
                    Exit For
                End If
            Next
            If Found Then
                OverrideClientSelectionChanged = True
                ComboClients.SelectedIndex = i
                AceptarDigitalNew.Enabled = True
                AceptarManualNew.Enabled = True
            Else
                AceptarDigitalNew.Enabled = False
                AceptarManualNew.Enabled = False
            End If
        End If
    End Sub

    Private Sub ComboClients_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ComboClients.SelectedIndexChanged
        If Not OverrideClientSelectionChanged Then
            AceptarManualNew.Enabled = True
            AceptarDigitalNew.Enabled = True
            TextBox1.Text = ""
            TextBox1.Visible = False
            ComboClients.Focus()
        End If
        OverrideClientSelectionChanged = False
    End Sub

    Private Sub CheckAR_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CheckAR.Load

    End Sub


    Private Sub GetGarantiaRx(ByVal Rx As Integer)
        'dim SQLStr as String = "select * from orderhed with(nolock) where ordernum
    End Sub

    Private Sub SincronizarClientesToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SincronizarClientesToolStripMenuItem.Click
        If MsgBox("Esta seguro de ejecutar este procedimiento?" & vbCrLf & "Puede tardar algunos minutos", MsgBoxStyle.Exclamation + MsgBoxStyle.YesNo, "Sincronizaci�n de Clientes/Precios") = MsgBoxResult.Yes Then
            WRKR_Customers.RunWorkerAsync()

            'WorkingBar.Value = 0
            'WorkingBar.Visible = True
            'Application.DoEvents()
            'TimerWorking.Enabled = True

            'Dim sqlstr As String = ""
            'Dim ds As New DataSet

            'Dim t1 As New SqlDB(My.Settings.LocalServer, My.Settings.DBUser, My.Settings.DBPassword, My.Settings.LocalDBName)

            't1.OpenConn()

            'Try
            '    Cursor = Cursors.WaitCursor
            '    My.Application.DoEvents()

            '    Dim Comm As New SqlCommand("SP_ImportCustomerPriceLst", t1.SQLConn)
            '    Comm.CommandTimeout = 120
            '    Comm.CommandType = Data.CommandType.StoredProcedure '
            '    Comm.Parameters.AddWithValue("@labid", Me.GetLabNum())

            '    Comm.ExecuteNonQuery()

            '    t1.CloseConn()

            '    MsgBox("La Lista de Clientes y Precios ha sido actualizada", MsgBoxStyle.Information)

            'Catch ex As Exception
            '    MsgBox("Error al sincronizar clientes:" & vbCrLf & ex.Message, MsgBoxStyle.Information)
            'Finally
            '    ActualizaClientesToolStripMenuItem_Click(Me, e)
            '    TimerWorking.Enabled = False
            '    WorkingBar.Visible = False
            '    Cursor = Cursors.Default
            'End Try

        End If

    End Sub


    Private Sub SincronizarPastillasToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SincronizarPastillasToolStripMenuItem.Click
        If MsgBox("Esta seguro de ejecutar este procedimiento?" & vbCrLf & "Puede tardar varios minutos", MsgBoxStyle.Exclamation + MsgBoxStyle.YesNo, "Sincronizacion de Pastillas") = MsgBoxResult.Yes Then
            Dim t As New SqlDB(My.Settings.VantageServer, My.Settings.DBUser, My.Settings.DBPassword, My.Settings.MfgSysDBName)
            Try
                t.OpenConn()
                Dim sqlstr As String
                Dim ds As New DataSet

                LabelWorking.Text = "Actualizando Pastillas"
                sqlstr = "select company,partnum,partdescription,prodcode,typecode,unitprice,shortchar01,shortchar02,shortchar03,shortchar04,shortchar05,shortchar06,shortchar07,shortchar08,shortchar09,shortchar10,checkbox01,checkbox02,checkbox20,number01,number02 from part with(nolock) where company = 'TRECEUX'  order by partnum "
                ds = t.SQLDS(sqlstr, "t1")
                If ds.Tables("t1").Rows.Count = 0 Then
                    MsgBox("No se encontro nunguna pastilla." & vbCrLf & "Favor de checar la configuracion de la aplicacion", MsgBoxStyle.Critical)
                Else
                    Dim i As Integer
                    Dim t1 As New SqlDB(My.Settings.LocalServer, My.Settings.DBUser, My.Settings.DBPassword, My.Settings.LocalDBName)
                    Dim wasvisible As Boolean = WorkingBar.Visible
                    Try
                        t1.OpenConn()
                        With ds.Tables("t1")
                            Dim offset As Single = 100 / .Rows.Count
                            Dim value As Single = 0
                            Dim sqlstr2 As String = ""
                            WorkingBar.Value = 0
                            For i = 0 To .Rows.Count - 1
                                WorkingBar.Visible = True
                                LabelWorking.Visible = True
                                value += offset
                                'If .Rows(i).Item("custnum") = 2335 Then
                                '    MsgBox("acervantes")
                                'End If
                                With .Rows(i)
                                    Try
                                        Dim Comm As New SqlCommand("[SP_SyncParts]", t1.SQLConn)
                                        Comm.CommandType = Data.CommandType.StoredProcedure
                                        Comm.Parameters.AddWithValue("company", .Item("company"))
                                        Comm.Parameters.AddWithValue("partnum", .Item("partnum"))
                                        Comm.Parameters.AddWithValue("partdescription", .Item("partdescription"))
                                        Comm.Parameters.AddWithValue("prodcode", .Item("prodcode"))
                                        Comm.Parameters.AddWithValue("typecode", .Item("typecode"))
                                        Comm.Parameters.AddWithValue("unitprice", .Item("unitprice"))
                                        Comm.Parameters.AddWithValue("shortchar01", .Item("shortchar01"))
                                        Comm.Parameters.AddWithValue("shortchar02", .Item("shortchar02"))
                                        Comm.Parameters.AddWithValue("shortchar03", .Item("shortchar03"))
                                        Comm.Parameters.AddWithValue("shortchar04", .Item("shortchar04"))
                                        Comm.Parameters.AddWithValue("shortchar05", .Item("shortchar05"))
                                        Comm.Parameters.AddWithValue("shortchar06", .Item("shortchar06"))
                                        Comm.Parameters.AddWithValue("shortchar07", .Item("shortchar07"))
                                        Comm.Parameters.AddWithValue("shortchar08", .Item("shortchar08"))
                                        Comm.Parameters.AddWithValue("shortchar09", .Item("shortchar09"))
                                        Comm.Parameters.AddWithValue("shortchar10", .Item("shortchar10"))
                                        Comm.Parameters.AddWithValue("checkbox01", .Item("checkbox01"))
                                        Comm.Parameters.AddWithValue("checkbox02", .Item("checkbox02"))
                                        Comm.Parameters.AddWithValue("checkbox20", .Item("checkbox20"))
                                        Comm.Parameters.AddWithValue("number01", .Item("number01"))
                                        Comm.Parameters.AddWithValue("number02", .Item("number02"))
                                        Comm.ExecuteNonQuery()
                                    Catch ex As Exception
                                        MsgBox("ALGO FALLO:" & vbCrLf & ex.Message, MsgBoxStyle.Information)
                                    End Try
                                End With
                                WorkingBar.Value = CInt(value)
                                My.Application.DoEvents()
                            Next
                        End With
                    Catch ex As Exception
                    Finally
                        WorkingBar.Visible = wasvisible
                        LabelWorking.Visible = False
                        t1.CloseConn()
                    End Try
                End If
            Catch ex As Exception
            Finally
                t.CloseConn()
            End Try
        End If

    End Sub

    Private Sub Button2_Click_1(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Dim x As ItemWithValue = ListRightParts.SelectedItem
        MsgBox(x.Value)
    End Sub

    Private Sub ComboArmazon2_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ComboArmazon2.SelectedIndexChanged
        If ComboArmazon2.Texto = "Perforado" And My.Settings.OpticalProtocol = "OMA" Then
            Dim Drill As New FrmDrill
            'le pasamos las variables de perforado
            Drill.txtanglat.Text = OMA_PER_ANGLAT
            Drill.txtangvert.Text = OMA_PER_ANGVER
            Drill.txtbfa.Text = OMA_PER_BFA
            Drill.txtprofundidad.Text = OMA_PER_DEPTH
            Drill.TXTDIA.Text = OMA_PER_DIA
            Drill.txtX1.Text = OMA_PER_X1
            Drill.txtY1.Text = OMA_PER_Y1
            Drill.txtX2.Text = OMA_PER_X2
            Drill.txtY2.Text = OMA_PER_Y2

            Drill.ShowDialog()
            'poblamos las variables de perforado capturadas
            If Drill.DataChanged Then
                OMA_PER_ANGLAT = Drill.AngLat
                OMA_PER_ANGVER = Drill.AngVer
                OMA_PER_BFA = Drill.BFA
                OMA_PER_DEPTH = Drill.Depth
                OMA_PER_DIA = Drill.Diametro
                OMA_PER_X1 = Drill.X1
                OMA_PER_X2 = Drill.X2
                OMA_PER_Y1 = Drill.Y1
                OMA_PER_Y2 = Drill.Y2
            End If
            Drill.Dispose()
            Drill = Nothing
        End If
    End Sub

    Private Sub MedidasDePerforadoToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MedidasDePerforadoToolStripMenuItem.Click
        If ComboArmazon2.Texto = "Perforado" Then
            Dim Drill As New FrmDrill

            Drill.txtanglat.Text = OMA_PER_ANGLAT
            Drill.txtangvert.Text = OMA_PER_ANGVER
            Drill.txtbfa.Text = OMA_PER_BFA
            Drill.txtprofundidad.Text = OMA_PER_DEPTH
            Drill.TXTDIA.Text = OMA_PER_DIA
            Drill.txtX1.Text = OMA_PER_X1
            Drill.txtY1.Text = OMA_PER_Y1
            Drill.txtX2.Text = OMA_PER_X2
            Drill.txtY2.Text = OMA_PER_Y2

            Drill.ShowDialog()
            'populmos las variables de perforado capturadas
            If Drill.DataChanged Then
                OMA_PER_ANGLAT = Drill.AngLat
                OMA_PER_ANGVER = Drill.AngVer
                OMA_PER_BFA = Drill.BFA
                OMA_PER_DEPTH = Drill.Depth
                OMA_PER_DIA = Drill.Diametro
                OMA_PER_X1 = Drill.X1
                OMA_PER_X2 = Drill.X2
                OMA_PER_Y1 = Drill.Y1
                OMA_PER_Y2 = Drill.Y2
            End If
            Drill.Dispose()
            Drill = Nothing
        End If
    End Sub

    Private Sub TimerSincronizar_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TimerSincronizar.Tick
        Dim t As New SqlDB(My.Settings.LocalServer, My.Settings.DBUser, My.Settings.DBPassword, My.Settings.LocalDBName)
        Try
            t.OpenConn()
            Dim ds As New DataSet
            ds = t.SQLDS("select count(*) as Cantidad from orderhed with(nolock) where sincronizada = 0", "t1")
            If ds.Tables("t1").Rows(0).Item("Cantidad") > 0 Then
                LabelSincronizar.Text = ds.Tables("t1").Rows(0).Item("Cantidad") & " Rx por Sincronizar"
                LabelSincronizar.Visible = True
                PictureSincronizar.Visible = True
            Else
                LabelSincronizar.Visible = False
                PictureSincronizar.Visible = False
            End If
        Catch ex As Exception
            LabelSincronizar.Visible = False
            PictureSincronizar.Visible = False
        Finally
            t.CloseConn()
            Update_Virtuales_WebOrders(Me, New EventArgs)
        End Try
    End Sub

    Private Sub ComboWorkingMode_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ComboWorkingMode.SelectedIndexChanged
        Select Case ComboWorkingMode.Text
            Case "CONECTADO"
                My.Settings.WorkingMode = "ONLINE"
                My.Settings.Save()
                Me.ChangeWorkingStatus(WorkingStatusType.OnLine)

            Case "DESCONECTADO"
                My.Settings.WorkingMode = "OFFLINE"
                My.Settings.Save()
                Me.ChangeWorkingStatus(WorkingStatusType.OffLine)
        End Select
    End Sub

    Private Sub SaveVirtualTraces(ByVal t As SqlDB, ByVal Tran As SqlClient.SqlTransaction, ByVal Location As LabLocation, ByVal Action As VirtualRxAction, ByVal Ordernum As Integer)
        Dim sqlstr As String = ""
        Dim TableTrazosVirtuales As String = ""
        Select Case Location
            Case LabLocation.Local
                TableTrazosVirtuales = My.Settings.LocalTrazosVirtuales
            Case LabLocation.Virtual
                TableTrazosVirtuales = My.Settings.ERPTrazosVirtuales
        End Select

        Dim values As String = "values ('" & RxWECO.Text & "',1,'','" & Now.ToString & "',0,0," & (ContornoPuente.Text * 100).ToString & ",0,0,0,0,0"
        sqlstr = "insert into " & TableTrazosVirtuales & " (jobnum,fuente,cl_lab,fecha,status,tracesrc,dbl,circ,eyeflag,sizing,framecurve,frameangle"
        Dim i As Integer
        Try
            Dim Radios(511) As Integer
            If My.Settings.OpticalProtocol = "OMA" Then
                Radios = Me.Convertir400To512(RadiosL)
            End If
            If My.Settings.OpticalProtocol = "DVI" Then
                Radios = RadiosL
            End If

            For i = 0 To 511
                values &= "," & Radios(i)
                sqlstr &= ",Int" & (i + 1).ToString
            Next
        Catch ex As Exception

        End Try
        sqlstr &= ",ordernum) " & values & "," & Ordernum & ")"

        sqlstr = "delete from " & TableTrazosVirtuales & " where ordernum = " & Ordernum & "; " & sqlstr

        'Select Case Action
        '    Case VirtualRxAction.Create
        '    Case VirtualRxAction.Modify
        '        sqlstr = "update"

        'End Select

        'MsgBox("La cadena es: " + vbCrLf + sqlstr, MsgBoxStyle.Information)

        If Not t.ExecuteNonQuery(sqlstr, Tran) Then
            Throw New Exception("Error de Conexion: No se pudo guardar el trazo virtual")
        End If
    End Sub


    Private Sub SaveDVITrace(ByVal ContornoPuente As Integer, ByVal MyRadios() As Integer)
        Dim t As New SqlDB(My.Settings.LocalServer, My.Settings.DBUser, My.Settings.DBPassword, My.Settings.LocalDBName)
        Dim cmd As SqlClient.SqlCommand

        Dim sqlstr As String = ""
        Dim TableTrazosVirtuales As String = ""

        Dim values As String = "values ('" & RxWECO.Text & "',1,'','" & Now.ToString & "',1,0," & (ContornoPuente * 100).ToString & ",0,0,0,0,0"
        sqlstr = "insert into tracerdata (jobnum,fuente,cl_lab,fecha,status,tracesrc,dbl,circ,eyeflag,sizing,framecurve,frameangle"
        Dim i As Integer

        Try

            Dim Radios(511) As Integer
            'If My.Settings.OpticalProtocol = "OMA" Then
            Radios = Me.Convertir400To512(MyRadios)
            'End If
            'If My.Settings.OpticalProtocol = "DVI" Then
            '    Radios = MyRadios
            'End If

            For i = 0 To 511
                values &= "," & Radios(i)
                sqlstr &= ",Int" & (i + 1).ToString
            Next

            sqlstr &= ") " & values & ")"

            t.OpenConn()
            cmd = New SqlClient.SqlCommand(sqlstr, t.SQLConn)
            cmd.ExecuteNonQuery()

        Catch ex As Exception
            Throw New Exception("Error de Conexion: No se pudo guardar el trazo virtual")
        Finally
            t.CloseConn()
        End Try


        'If Not t.ExecuteQuery(sqlstr, Tran) Then
        '    Throw New Exception("Error de Conexion: No se pudo guardar el trazo virtual")
        'End If
    End Sub

    Private Sub CapturaPaqueteToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CapturaPaqueteToolStripMenuItem.Click

        ' Si ya se hizo un cambio en el paquete entonces no podra volver a entrar a esta opcion hasta Guardar o Cancelar la Rx
        If Me.PACKTRAN_TYPE = 0 Then
            Dim package As New Paquetes()
            package.lblNumParte.Text = ""
            package.lblNumArmazon.Text = ""
            Dim OriginalNumArmazon As Integer = NumArmazon

            MyFrame.ArmazonOriginal = NumArmazon

            package.NumCliente = Me.ComboClients.SelectedValue

            If Me.IsModifying Then
                package.ArmazonOriginal = Me.NumArmazon
                package.PaqueteOriginal = Me.CostcoLente
                package.ComentarioOriginal = Me.TextComentarios.Text
                package.SetRxValues(Me.StuffRx(RxWECO.Text), RxNum.Text, RxID.Text)
            End If

            ' Solo puede cambiar el armazon si y solo si es una Rx nueva (IsModifying = false) 
            ' o si es una modificacion de error de armazon(MyFrame.ErrorID > 0)
            If (IsModifying And MyFrame.ErrorID > 0) Or Not IsModifying Then
                Try
                    package.ShowDialog()

                    Me.PACKTRAN_TYPE = package.PackTran

                    If PACKTRAN_TYPE = 0 Then
                        MyFrame.NumArmazon = MyFrame.ArmazonOriginal
                        NumArmazon = MyFrame.ArmazonOriginal
                        MyFrame.ReadStatus = Frames.ThisFrameReadStatus.NotRead
                        PIC_Armazon.Image = My.Resources.Lens_NoInventory_2
                        Throw New Exception("Operaci�n Cancelada!")
                    End If

                    Me.NumArmazon = package.lblNumArmazon.Text
                    Me.numpaq = package.lblNumParte.Text
                    MyFrame.NumArmazon = NumArmazon
                    If numpaq.Length = 0 Then
                        MyFrame.NumPaquete = 0
                        Me.CostcoLente = ""
                    Else
                        MyFrame.NumPaquete = package.lblNumPaquete.Text
                        Me.CostcoLente = package.lblNumPaquete.Text

                    End If


                    IsFrameOK = True
                    MyFrame.ReadStatus = Frames.ThisFrameReadStatus.Ok

                    If (NumArmazon > 0) Then
                        If (IsModifying And (MyFrame.NumArmazon <> MyFrame.ArmazonOriginal)) Or Not IsModifying Then
                            Dim NewFrame As New Frames(MyFrame.NumArmazon, Frames.ThisFrameStatus.Changed)
                            NewFrame.ArmazonOriginal = OriginalNumArmazon
                            NewFrame.ReadStatus = MyFrame.ReadStatus
                            MyFrame = NewFrame

                            PNL_Armazon.Enabled = True

                            'MyFrame.FrameStatus = Frames.ThisFrameStatus.Changed

                        End If

                        If Me.GetFrameTrace(NumArmazon).Length > 0 Then 'Obtiene el trazo de OMAStoredTraces y lo guarda en OMATraces
                            'If CheckInventoryFrameFamily(NumArmazon) Then
                            If MyFrame.CheckInventoryFrameFamily Then
                                IsFrameOK = False
                                MyFrame.ReadStatus = Frames.ThisFrameReadStatus.NotRead
                                PNL_Armazon.Visible = True
                                PIC_Armazon.Image = My.Resources.Lens_Over_2
                                'CheckFrameStatus(NumArmazon)
                                MyFrame.CheckFrameStatus()
                                TBX_FrameDescription.Text = MyFrame.ArmazonDescripcion
                            Else
                                PNL_Armazon.Visible = False
                            End If
                            Dim MyRadios() As Integer = Me.GetOMAFrameRadios(RxWECO.Text)

                            Me.SaveDVITrace(ContornoPuente.Text, MyRadios)
                        Else
                            Throw New Exception("No se encontr� el trazo asociado con este armaz�n en la base de datos")
                            'MsgBox("No se encontr� el trazo asociado con este armaz�n en la base de datos", MsgBoxStyle.Exclamation)
                        End If
                    Else
                        PNL_Armazon.Visible = False
                    End If
                    'If Not IsFrameOK Then
                    If MyFrame.ReadStatus = Frames.ThisFrameReadStatus.NotRead Then
                        PIC_Armazon.Image = My.Resources.Lens_Error_2
                    Else
                        PIC_Armazon.Image = My.Resources.Lens_OK_2
                    End If

                    If Me.IsModifying Then
                        Me.CambiarTrazosToolStripMenuItem_Click(Me, e)
                        If Not ChangeTraceDenied Then
                            TextComentarios.Text = package.ComentarioOriginal
                            If TextComentarios.Text.Length > 0 Then
                                PanelComentarios.Visible = True
                                PanelGraduacion.Visible = True
                                PanelOpciones.Visible = True
                            End If
                        End If
                    Else
                        BuscarTrazos_Click(Me, New EventArgs)

                        If ComboTrazos.ComboBox1.Items.Count = 0 Then
                            RadioManualNew.Checked = True
                        End If
                        Cancel.Enabled = True

                        TextComentarios.Text &= package.ComentarioOriginal
                        If TextComentarios.Text.Length > 0 Then
                            PanelComentarios.Visible = True
                            PanelGraduacion.Visible = True
                            PanelOpciones.Visible = True
                        End If
                    End If

                    If Not IsModifying Then
                        PanelArmazon.Enabled = True
                        RadioDigitalNew.Checked = True
                        AceptarDigitalNew.Visible = True
                        AceptarDigitalNew.Enabled = True
                    End If

                Catch ex As Exception
                    PACKTRAN_TYPE = 0
                    MsgBox("Error al modificar Paquetes/Armazones" + vbCrLf + ex.Message, MsgBoxStyle.Exclamation)
                End Try
            Else
                MsgBox("No se puede modificar el armazon o paquete de este modo, tiene que seleccionar un tipo de error de armazon", MsgBoxStyle.Exclamation)
            End If




            package.Dispose()
            package = Nothing

        Else
            MsgBox("NO PUEDE ENTRAR A ESTA OPCION PORQUE YA SE MODIFICO UNA VEZ LA INFORMACION DEL PAQUETE." & vbCrLf & _
                   "CANCELE LA MODIFICACION DE LA RX Y VUELVA A INTENTARLO.", MsgBoxStyle.Information)
        End If
        'PanelGraduacion.Visible = True
        'PanelOpciones.Visible = True

    End Sub
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
        'oma.SetBEVP(OMAFile.BevelPossition.FreeFloat, OMAFile.BevelPossition.FreeFloat)
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

    Private Sub TimerWorking_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TimerWorking.Tick
        Dim NewValue As Integer = WorkingBar.Value + 10
        If NewValue > 100 Then
            NewValue = 0
        End If

        WorkingBar.Value = NewValue
        WorkingBar.Visible = True
        Application.DoEvents()
    End Sub

    Private Sub WRKR_Customers_DoWork(ByVal sender As System.Object, ByVal e As System.ComponentModel.DoWorkEventArgs) Handles WRKR_Customers.DoWork
        Dim Failed As Boolean = False
        Dim FailedMessage As String = ""

        Dim sqlstr As String = ""
        Dim ds As New DataSet

        Dim t1 As New SqlDB(My.Settings.LocalServer, My.Settings.DBUser, My.Settings.DBPassword, My.Settings.LocalDBName)


        Try
            t1.OpenConn()
            My.Application.DoEvents()

            Dim Comm As New SqlCommand("SP_ImportCustomerPriceLst", t1.SQLConn)
            Comm.CommandTimeout = My.Settings.DBCommandTimeout * 5
            Comm.CommandType = Data.CommandType.StoredProcedure
            Comm.Parameters.AddWithValue("@labid", Me.GetLabNum())
            WRKR_Customers.ReportProgress(20, "Listo para ejecutar SP")
            Comm.ExecuteNonQuery()
            WRKR_Customers.ReportProgress(60, "SP Ejecutado Satisfactoriamente")

        Catch ex As Exception
            Failed = True
            FailedMessage = "Error al sincronizar clientes:" & vbCrLf & ex.Message
        Finally
            t1.CloseConn()
            WRKR_Customers.ReportProgress(90, "Conexion Cerrada")
            If Failed Then
                WRKR_Customers.ReportProgress(0, FailedMessage)
            Else
                WRKR_Customers.ReportProgress(100, "La Lista de Clientes y Precios ha sido actualizada")
            End If
        End Try
    End Sub

    Private Sub WRKR_Customers_ProgressChanged(ByVal sender As System.Object, ByVal e As System.ComponentModel.ProgressChangedEventArgs) Handles WRKR_Customers.ProgressChanged
        If WorkingBar.Visible = False Then
            WorkingBar.Visible = True
            LabelWorking.Visible = True
        End If
        WorkingBar.Value = e.ProgressPercentage
        LabelWorking.Text = e.UserState.ToString

        Application.DoEvents()

    End Sub

    Private Sub WRKR_Customers_RunWorkerCompleted(ByVal sender As System.Object, ByVal e As System.ComponentModel.RunWorkerCompletedEventArgs) Handles WRKR_Customers.RunWorkerCompleted
        WorkingBar.Visible = False
        LabelWorking.Visible = False

        Select Case WorkingBar.Value
            Case 100
                ActualizaClientesToolStripMenuItem_Click(Me, e)
                MsgBox(LabelWorking.Text, MsgBoxStyle.Information)
            Case 0
                MsgBox(LabelWorking.Text, MsgBoxStyle.Exclamation)
        End Select
    End Sub

    Private Sub PanelInformacion_Paint(ByVal sender As System.Object, ByVal e As System.Windows.Forms.PaintEventArgs)

    End Sub
    Private Function CheckQAStatus(ByVal ordernum As Integer) As QAStatus
        Dim Failed As Boolean = False
        Dim FailedMessage As String = ""

        Dim sqlstr As String = ""
        Dim ds As New DataSet

        Dim t As New SqlDB()
        t.SQLConn = New SqlConnection(Laboratorios.ConnStr)

        Try
            t.OpenConn()
            sqlstr = "select coalesce(cctax,0) as cctax from orderhed with(nolock) where ordernum = " & ordernum
            ds = t.SQLDS(sqlstr, "t1")
            If ds.Tables("t1").Rows.Count = 0 Then
                Return QAStatus.Unknown
            Else
                Select Case ds.Tables("t1").Rows(0).Item("cctax")
                    Case 0
                        Return QAStatus.GP
                    Case 1
                        Return QAStatus.AR
                    Case 2
                        Return QAStatus.BM
                    Case 3
                        Return QAStatus.PC
                End Select
            End If
        Catch ex As Exception
            Failed = True
            FailedMessage = ex.Message
        Finally
            t.CloseConn()
            If Failed Then
                MsgBox(FailedMessage, MsgBoxStyle.Exclamation)
            End If
        End Try
    End Function

    Private Sub ContornoPuente_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ContornoPuente.TextChanged
        'Dim puente As Single = 0
        'Try
        '    puente = ContornoPuente.Text
        'Catch ex As Exception

        'End Try
        'If puente > 1000 Then
        '    ContornoPuente.Text = puente / 1000
        'ElseIf puente > 100 Then
        '    ContornoPuente.Text = puente / 100
        'End If
    End Sub

    Private Sub ContornoPuente_ModifiedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ContornoPuente.ModifiedChanged
        Dim puente As Single = 0
        Try
            puente = ContornoPuente.Text
        Catch ex As Exception

        End Try
        If puente > 1000 Then
            ContornoPuente.Text = puente / 1000
        ElseIf puente > 100 Then
            ContornoPuente.Text = puente / 100
        End If

    End Sub
    Private Sub ClearLenses()
        ListLeftParts.Items.Clear()
        ListRightParts.Items.Clear()
        SelectedLeftEye.Items.Clear()
        SelectedRightEye.Items.Clear()
        InsertLog = False
        InsertLogSide = 0
        txtQtyOnHandRight.Text = ""
        txtQtyOnHandLeft.Text = ""
    End Sub
    'Private Sub CBX_SpecialDesign_CheckedChanged2(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CBX_SpecialDesign.CheckedChanged
    '    Dim Failed As Boolean = False
    '    'If Not Failed Then
    '    If CBX_SpecialDesign.Checked And CBX_SpecialDesign.Enabled And My.Settings.EnableOptisur Then
    '        Dim sd As New SpecialDesigns
    '        'Try
    '        'Dim tempoptisur As New NewCalcot(True, NewCalcot.Eyes.Both)
    '        'tempoptisur.GetCredits()
    '        'sd.HDRx = tempoptisur.Credits_HDRx.Credits
    '        'sd.SPACIA = tempoptisur.Credits_SPACIA.Credits
    '        'sd.SPACIAHD = tempoptisur.Credits_SPACIAHD.Credits
    '        'sd.TRIN8_12 = tempoptisur.Credits_TRIN8_12.Credits
    '        'sd.TRIN8_12HD = tempoptisur.Credits_TRIN8_12HD.Credits

    '        'PNL_Creditos.Visible = True
    '        'Catch ex As Exception
    '        '    Failed = True
    '        '    'PNL_Creditos.Visible = False
    '        '    'CBX_SpecialDesign.Checked = False
    '        '    MsgBox(ex.Message, MsgBoxStyle.Exclamation, My.Application.Info.ProductName)
    '        'End Try
    '        If Not Failed Then
    '            Try
    '                sd.ShowDialog()
    '                If sd.Selected Then
    '                    CBX_SpecialDesign.Texto = "F.F. [" & sd.CMB_Designs.Text & "]"
    '                    SpecialDesignID = sd.CMB_Designs.SelectedValue
    '                    SD_Partnum = sd.getSDPartnum(SpecialDesignID)
    '                    'ClearLenses()
    '                Else
    '                    CBX_SpecialDesign.Checked = False
    '                End If
    '            Catch ex As Exception
    '                CBX_SpecialDesign.Checked = False
    '            End Try
    '        Else
    '            CBX_SpecialDesign.Checked = False
    '        End If
    '    Else
    '        CBX_SpecialDesign.Texto = "Free Form"
    '        'PNL_Creditos.Visible = False
    '    End If
    '    'End If
    '    If Not CBX_SpecialDesign.Checked Then
    '        SpecialDesignID = 0
    '    End If
    'End Sub


    Private Sub CBX_SpecialDesign_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CBX_SpecialDesign.CheckedChanged
        Dim Failed As Boolean = False
        'If Not Failed Then
        If CBX_SpecialDesign.Checked And CBX_SpecialDesign.Enabled And My.Settings.EnableOptisur Then
            Dim sd As New SpecialDesigns
            'RT Dim tempoptisur As New NewCalcot(True, NewCalcot.Eyes.Both)

            ''Try
            ''    'RT tempoptisur.GetCredits()
            ''    'RT sd.DGV_Creditos.DataSource = tempoptisur.DT

            ''    'PNL_Creditos.Visible = True
            ''Catch ex As Exception
            ''    If ex.Message.Contains("10007") Then
            ''        sd.DGV_Creditos.DataSource = tempoptisur.GetSpecialDesignsTable
            ''    Else
            ''        Failed = True
            ''        MsgBox(ex.Message, MsgBoxStyle.Exclamation, My.Application.Info.ProductName)
            ''    End If
            ''End Try
            ''If Not Failed Then
            Try
                sd.ShowDialog()
                If sd.Selected Then
                    CBX_SpecialDesign.Texto = "F.F. [" & sd.CMB_Designs.Text & "]"
                    SpecialDesignID = sd.CMB_Designs.SelectedValue
                    SD_Partnum = sd.getSDPartnum(SpecialDesignID)

                    'If SpecialDesignID = 1 Then
                    '    CheckOptim.Checked = True
                    'End If
                    'ClearLenses()
                Else
                    CBX_SpecialDesign.Checked = False
                End If
            Catch ex As Exception
                CBX_SpecialDesign.Checked = False
            End Try
            'Else
            '    CBX_SpecialDesign.Checked = False
            'End If
        Else
        CBX_SpecialDesign.Texto = "Free Form"
        'PNL_Creditos.Visible = False
        End If
        'End If
        If Not CBX_SpecialDesign.Checked Then
            SpecialDesignID = 0
            SD_Partnum = ""
        End If
    End Sub

    Private Sub PIC_Armazon_MouseEnter(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PIC_Armazon.MouseEnter
        'If Not IsFrameOK Then
        PIC_Armazon.Image = My.Resources.Lens_Over_2
        'End If
    End Sub

    Private Sub PIC_Armazon_MouseLeave(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PIC_Armazon.MouseLeave
        'If Not IsFrameOK Then
        'PIC_Armazon.Image = My.Resources.Lens_Error_2
        'Else
        'PIC_Armazon.Image = My.Resources.Lens_OK_2
        'End If
        Select Case MyFrame.ReadStatus
            Case Frames.ThisFrameReadStatus.Failed
                PIC_Armazon.Image = My.Resources.Lens_Error_2
            Case Frames.ThisFrameReadStatus.Ok
                PIC_Armazon.Image = My.Resources.Lens_OK_2
            Case Frames.ThisFrameReadStatus.NotRead
                PIC_Armazon.Image = My.Resources.Lens_NoInventory_2

        End Select
    End Sub

    Private Sub PIC_Armazon_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PIC_Armazon.Click
        'MsgBox("Favor de leer el trazo", MsgBoxStyle.Information)
        'Dim rf As New ReadFrame(NumArmazon)
        Dim rf As New ReadFrame(MyFrame.PartNum)
        rf.ShowDialog()
        If rf.Read Then
            PIC_Armazon.Image = My.Resources.Lens_OK_2
            IsFrameOK = True
            MyFrame.ReadStatus = Frames.ThisFrameReadStatus.Ok
            'TBX_FrameDescription.Text = rf.FrameDescription
        Else
            PIC_Armazon.Image = My.Resources.Lens_Error_2
            IsFrameOK = False
            MyFrame.ReadStatus = Frames.ThisFrameReadStatus.NotRead
        End If
        'PIC_Armazon.Image = My.Resources.Lens_OK_2
    End Sub
    Private Function CheckFrameStatus(ByVal RequestedFrame As String) As Boolean
        Dim Failed As Boolean = False
        Dim FailedMessage As String = ""

        Dim sqlstr As String = ""
        Dim ds As New DataSet
        Dim OnHanQty As Integer = 0
        Dim Description As String = ""

        If RequestedFrame.Length > 0 Then
            RequestedFrame = RequestedFrame.Substring(0, 5)
        End If


        Dim t As New SqlDB()
        t.SQLConn = New SqlClient.SqlConnection(Laboratorios.ConnStr)
        Try
            t.OpenConn()
            sqlstr = "select * from vwarmazones with(nolock) where partnum = '" & RequestedFrame & "'"
            ds = t.SQLDS(sqlstr, "t1")
            If ds.Tables("t1").Rows.Count > 0 Then
                OnHanQty = ds.Tables("t1").Rows(0).Item("onhandqty")
                Try
                    TBX_FrameDescription.Text = ds.Tables("t1").Rows(0).Item("partdescription")
                Catch ex As Exception
                    TBX_FrameDescription.Text = "No se encontr� descripci�n"
                    Throw New Exception("Descripci�n en Null")
                End Try
            Else
                Throw New Exception("Armaz�n [" & RequestedFrame & "]inexistente en la base de datos")
            End If

        Catch ex As Exception
            Failed = True
            FailedMessage = "Error al obtener datos del armaz�n" & vbCrLf & ex.Message
        Finally
            t.CloseConn()
            If Failed Then
                MsgBox(FailedMessage, MsgBoxStyle.Exclamation)
            End If
        End Try
        Return Not Failed
    End Function
    Private Function CheckInventoryFrameFamily(ByVal RequestedFrame As String) As Boolean
        Dim ReturnValue As Boolean = False
        Dim Failed As Boolean = False
        Dim FailedMessage As String = ""

        Dim sqlstr As String = ""
        Dim ds As New DataSet
        Dim OnHanQty As Integer = 0
        Dim Description As String = ""

        If RequestedFrame.Length > 0 Then
            RequestedFrame = RequestedFrame.Substring(0, 5)
        End If


        Dim t As New SqlDB()
        t.SQLConn = New SqlClient.SqlConnection(Laboratorios.ConnStr)
        Try
            t.OpenConn()
            sqlstr = "select distinct framefamily from vwarmazones with(nolock) where framefamily in (" & My.Settings.InventoryFrameFamily & ") and partnum in (" & RequestedFrame & ")"
            ds = t.SQLDS(sqlstr, "t1")
            If ds.Tables("t1").Rows.Count > 0 Then
                ReturnValue = True
            End If

        Catch ex As Exception
            Failed = True
            FailedMessage = "Error al obtener datos del armaz�n" & vbCrLf & ex.Message
        Finally
            t.CloseConn()
            If Failed Then
                MsgBox(FailedMessage, MsgBoxStyle.Exclamation)
            End If
        End Try
        Return ReturnValue
    End Function

    Private Function GetFrameName(ByVal pFrameNum As Integer) As String
        Dim frameName As String = "None"

        If pFrameNum = 4602 Then 'ANGULO
            frameName = My.Settings.FNameAngulo
        ElseIf pFrameNum = 4802 Then 'CUADO
            frameName = My.Settings.FNameCuado
        ElseIf pFrameNum = 5202 Then 'GADO
            frameName = My.Settings.FNameGado
        End If

        Return frameName

    End Function
    Private Function GetOffsetToFrame(ByVal pFrameNum As Integer, ByVal pSide As String) As Single
        Dim offset As Single = 0

        If pFrameNum = 4602 Then 'ANGULO

            If pSide = "L" Then
                offset = Single.Parse(My.Settings.OffSetAnguloL)
            Else
                offset = Single.Parse(My.Settings.OffSetAnguloR)
            End If

        ElseIf pFrameNum = 4802 Then 'CUADO
            If pSide = "L" Then
                offset = Single.Parse(My.Settings.OffSetCuadoL)
            Else
                offset = Single.Parse(My.Settings.OffSetCuadoR)
            End If
        ElseIf pFrameNum = 5202 Then 'GADO
            If pSide = "L" Then
                offset = Single.Parse(My.Settings.OffSetGadoL)
            Else
                offset = Single.Parse(My.Settings.OffSetGadoR)
            End If
        End If

        Return offset

    End Function
    Private Function GetEyeSides() As OMAFile.EyeSides
        If CheckREye.Checked And CheckLEye.Checked Then
            Return OMAFile.EyeSides.Both
        ElseIf CheckREye.Checked Then
            Return OMAFile.EyeSides.Right
        ElseIf CheckLEye.Checked Then
            Return OMAFile.EyeSides.Left
        Else
            Return OMAFile.EyeSides.None
        End If

    End Function



    Private Sub ComboErrores_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ComboErrores.SelectedIndexChanged
        Try
            If ComboErrores.SelectedValue = My.Settings.FrameError_ID Then
                Dim fe As New FrameError
                fe.ShowDialog()
                If fe.Changed Then
                    FrameErrorID = fe.FrameErrorID
                    FrameErrorDesc = fe.FrameErrorDesc
                    AceptarErroresNew.Enabled = True
                    fe.Dispose()
                Else
                    AceptarErroresNew.Enabled = False
                End If
            Else
                AceptarErroresNew.Enabled = True
            End If

        Catch ex As Exception

        End Try
    End Sub

    Private Sub ConsultaDeCreditosToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ConsultaDeCreditosToolStripMenuItem.Click
        Dim credits As New Creditos
        Dim tempoptisur As New NewCalcot(True, NewCalcot.Eyes.Both)
        Try
            tempoptisur.GetCredits()
            'credits.HDRx = tempoptisur.Credits_HDRx.Credits
            'credits.SPACIA = tempoptisur.Credits_SPACIA.Credits
            'credits.SPACIAHD = tempoptisur.Credits_SPACIAHD.Credits
            'credits.TRIN8_12 = tempoptisur.Credits_TRIN8_12.Credits
            'credits.TRIN8_12HD = tempoptisur.Credits_TRIN8_12HD.Credits

            credits.DGV_Creditos.DataSource = tempoptisur.DT
            credits.ShowDialog()
        Catch ex As Exception

            'MsgBox("No se pudo obtener la informaci�n requerida: " & ex.Message, MsgBoxStyle.Exclamation, My.Application.Info.ProductName)
            If ex.Message.Contains("10007") Then
                credits.DGV_Creditos.DataSource = tempoptisur.GetSpecialDesignsTable
                credits.ShowDialog()
            Else
                MsgBox("No se pudo obtener la informaci�n requerida: " & ex.Message, MsgBoxStyle.Exclamation, My.Application.Info.ProductName)
            End If

        End Try

    End Sub

    Private Sub Preview_DragDrop(ByVal sender As Object, ByVal e As System.Windows.Forms.DragEventArgs) Handles Preview.DragDrop

    End Sub

    Private Sub UCClientes_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Label3.Click, AltaConsultaClientesToolStripMenuItem.Click
        Dim vbFClientes As New Clientes_ABC(LabID)
        Cursor = Cursors.WaitCursor
        vbFClientes.ShowDialog()
        Cursor = Cursors.Arrow
    End Sub

    Private Sub RxID_DoubleClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RxID.DoubleClick
        'Efecto especial para imprimir una receta
        Dim rxp As New PrintLabRx(RxID.Text)
        If My.Settings.ZebraPrint Then
            Try
                Dim settings As System.Drawing.Printing.PrinterSettings = New System.Drawing.Printing.PrinterSettings()

                ' Revisamos si tenemos una impresora v�lida en el archivo de configuraci�n

                If My.Settings.ZebraPrintPort.Length > 3 And Not (My.Settings.ZebraPrintPort.ToUpper().StartsWith("LPT")) Then
                    ' Usamos la impresora de la configuraci�n
                    settings.PrinterName = My.Settings.ZebraPrintPort
                End If
                RawPrinterHelper.SendFileToPrinter(settings.PrinterName, My.Application.Info.DirectoryPath + "\LabelVantage_ZPLII.txt")
            Catch ex As Exception
                MsgBox("PrintMEILabel" + ex.Message, MsgBoxStyle.Critical, "PrintMEILabel")
            End Try

        Else
            rxp.PrintPreviewTest()
        End If
    End Sub

    Private Sub SaldosToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SaldosToolStripMenuItem.Click
        '**************************************
        '  MUESTRA DIALOGO DE CONSULTA DE SALDOS
        '**************************************
        ' Pedro Far�as L.  Nov 7 2012
        Dim dlgClientesSaldo As New Clientes_Saldo
        Cursor = Cursors.WaitCursor
        dlgClientesSaldo.ShowDialog()
        Cursor = Cursors.Arrow
    End Sub

    Private Sub menuAjustarOrden(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles AjustarOrdenToolStripMenuItem.Click
        ActivaAjustes(LabID, "260115892")
    End Sub

End Class
'**********************************************
'ESTA CLASE GENERA EL REGISTRO DE QA DE LA RECETA
'**********************************************
Public Class RxQA
    Public orden, LocalSvr, ClaseD, ClaseI, Cliente As String
    Public ordenlab, lado As Integer
    Public esfd, cild, adcd, groscd, esfi, cili, adci, grosci As Single
    Public LadoD, LadoI, EsMod, AR, IsLocalRcv As Boolean


    Public Sub Save_QAInfo(ByVal MyCnn As SqlConnection, ByVal tran As SqlTransaction)
        'Dim MyCnn As SqlClient.SqlConnection
        Dim Cmd As SqlCommand
        Dim Param As New SqlClient.SqlParameter
        Dim errornum As Integer
        Dim Failed As Boolean = False
        Dim FailedMessage As String = ""

        'If EsMod Then
        'MyCnn = New Data.SqlClient.SqlConnection("user ID=" & My.Settings.DBUser & ";password =" & My.Settings.DBPassword & ";database=" & My.Settings.LocalDBName & ";server=" & LocalSvr & ";Connect Timeout=10")
        'MyCnn = New Data.SqlClient.SqlConnection(Laboratorios.ConnStr)
        'MyCnn.Open()
        Try
            Cmd = New SqlCommand("InsertQAReg", MyCnn)
            Cmd.CommandType = CommandType.StoredProcedure
            Cmd.CommandTimeout = My.Settings.DBCommandTimeout
            Cmd.Transaction = tran
            Cmd.Parameters.AddWithValue("@ordernum", orden)
            Cmd.Parameters.AddWithValue("@orderlab", ordenlab)
            Cmd.Parameters.AddWithValue("@Existe", EsMod)
            Cmd.Parameters.AddWithValue("@lado", lado)
            Cmd.Parameters.AddWithValue("@esfd", esfd)
            Cmd.Parameters.AddWithValue("@cild", cild)
            Cmd.Parameters.AddWithValue("@adcd", adcd)
            Cmd.Parameters.AddWithValue("@groscd", groscd)
            Cmd.Parameters.AddWithValue("@esfi", esfi)
            Cmd.Parameters.AddWithValue("@cili", cili)
            Cmd.Parameters.AddWithValue("@adci", adci)
            Cmd.Parameters.AddWithValue("@grosci", grosci)
            Cmd.Parameters.AddWithValue("@clased", ClaseD)
            Cmd.Parameters.AddWithValue("@clasei", ClaseI)
            Cmd.Parameters.AddWithValue("@AR", AR)
            Cmd.Parameters.AddWithValue("@IsLocalRcv", IsLocalRcv)
            Cmd.Parameters.AddWithValue("@Cliente", Cliente)

            ' Parametro de salida return
            Param.ParameterName = "@retval"
            Param.Direction = ParameterDirection.ReturnValue
            Cmd.Parameters.Add(Param)
            Cmd.ExecuteNonQuery()
            errornum = Cmd.Parameters("@retval").Value <> 0
            If errornum <> 0 Then
                Cmd.Dispose()
                'MyCnn.Close()
                Throw New Exception("Codigo de Error: " & errornum)
            End If
            Cmd.Dispose()
            Cmd = Nothing
        Catch ex As Exception
            Failed = True
            FailedMessage = "Ocurrio una excepcion al grabar el registro de Calidad." & vbCrLf & ex.Message
        Finally
            'MyCnn.Close()
            'MyCnn.Dispose()
            'MyCnn = Nothing
            If Failed Then
                Throw New Exception(FailedMessage)
            End If
        End Try
        'End If

    End Sub

    Public Sub New()
        esfd = 0
        cild = 0
        adcd = 0
        groscd = 0
        ClaseD = "N"
        esfi = 0
        cili = 0
        adci = 0
        grosci = 0
        ClaseI = "N"
    End Sub
   
End Class
