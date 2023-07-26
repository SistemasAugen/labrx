Imports System
Imports System.IO
Imports System.Runtime.Serialization.Formatters.Soap
Imports System.Runtime.Serialization.Formatters.Binary
Imports System.Xml
Imports System.Xml.Schema
Imports System.Xml.Serialization
Imports System.Net

Public Class SpecialDesignsCreditStatus
    Private DesignIDField As Integer
    Private DesingNameField As String
    Private CreditsField As Integer

    Public ReadOnly Property DesingID() As Integer
        Get
            Return DesignIDField
        End Get
    End Property
    Public ReadOnly Property DesignName() As String
        Get
            Return DesingNameField
        End Get
    End Property
    Public ReadOnly Property Credits() As Integer
        Get
            Return CreditsField
        End Get
    End Property
    Public Sub SetCreditStatus(ByVal DesID As Integer, ByVal DesName As String, ByVal Qty As Integer)
        DesignIDField = DesID
        DesingNameField = DesName
        CreditsField = Qty
    End Sub
    Public Sub SetCredit(ByVal Qty As Integer)
        CreditsField = Qty
    End Sub
    Public Sub New(ByVal DesID As Integer, ByVal DesName As String, ByVal Qty As Integer)
        SetCreditStatus(DesID, DesName, Qty)
    End Sub
End Class

Public Class NewCalcot
    Private XMLFile As String = ""
    Private XSDFile As String = ""
    Private Schemas As New XmlSchemaCollection
    Private Valid As Boolean
    Private MyResponse As LensDsgd
    Public Contorno As DisplFrame
    Public Calculos As LensDsgd
    Private MyJob As Job
    Private ErrorNumber As Integer
    Private ErrorName As String


    Private LeftPattern As DisplPattern
    Private RightPattern As DisplPattern
    Private LeftLensDSG As LensDsg
    Private RightLensDSG As LensDsg

    Private LeftDesign As Pastilla.Types
    Private RightDesign As Pastilla.Types

    Private MyGeoFrame As GeoFrame
    Private MyPattern As Pattern
    Private MyIsManual As Boolean
    Private MySelectedEyes As Eyes

    Public LeftSide As JobItem                   ' JobItem
    Public RightSide As JobItem                   ' JobItem

    Private AccountRequest As New AccountReq
    Private AccountStatement As New AccStatement


    Public Credits_HDRx As New SpecialDesignsCreditStatus(1, "VS HI DEFINITION", 0)
    Public Credits_TRIN8_12 As New SpecialDesignsCreditStatus(2, "PROG. TRINITY 8-12", 0)
    Public Credits_TRIN8_12HD As New SpecialDesignsCreditStatus(3, "PROG. TRINITY 8-12 HD", 0)
    Public Credits_SPACIA As New SpecialDesignsCreditStatus(4, "PROG. SPACIA", 0)
    Public Credits_SPACIAHD As New SpecialDesignsCreditStatus(5, "PROG. SPACIA HD", 0)

    Public DT As DataTable

    ''' <summary>
    ''' Validates a XML File against a Schema
    ''' </summary>
    ''' <remarks></remarks>
    Public Function ValidateSchema() As Boolean

        If XMLFile.Length = 0 Or XSDFile.Length = 0 Then
            Throw New Exception("You need to load both XML File and XSD File first!")
        Else
            Dim Message As String = "Valid"
            Dim Reader As XmlTextReader = New XmlTextReader(XMLFile)
            Try
                Dim Validator As New XmlValidatingReader(Reader)
                Validator.Schemas.Add(Schemas)
                Validator.ValidationType = ValidationType.Schema
                AddHandler Validator.ValidationEventHandler, AddressOf SchemaReadError
                Dim str As String = ""
                While Validator.Read()
                    'str &= Validator.Value
                End While
                'MsgBox(str)

            Catch ex As Exception
                Valid = False
                Message = "Not Valid" + vbCrLf + ex.Message
                Throw New Exception(Message)
            Finally
                Reader.Close()
            End Try
        End If
        Return True
    End Function

    ''' <summary>
    ''' Returns Error in a Schema Validation
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="args"></param>
    ''' <remarks></remarks>
    Private Shared Sub SchemaReadError(ByVal sender As Object, ByVal args As ValidationEventArgs)
        If args.Severity = XmlSeverityType.Error Then Throw New Exception("Validation Error" & vbCrLf & "Severity: " & args.Severity & vbCrLf & "Message: " & args.Message)
        'Throw New Exception("Validation Error" & vbCrLf & "Severity: " & args.Severity & vbCrLf & "Message: " & args.Message)

    End Sub

    ''' <summary>
    ''' Creates a new Object of type NewCalcot
    ''' </summary>
    ''' <param name="Manual">Frame Radius Type</param>
    ''' <param name="SelectEyes">Selected Eyes</param>
    ''' <remarks></remarks>
    Public Sub New(ByVal Manual As Boolean, ByVal SelectEyes As Eyes)
        Try
            MyJob = New Job
            IsManual = Manual
            MySelectedEyes = SelectEyes
            ErrorNumber = 0

        Catch ex As Exception
            Throw New Exception("Error al generar objeto de Optisur." + vbCrLf + ex.Message, ex)
        End Try
    End Sub

    ''' <summary>
    ''' JobNumber
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property JobNumber() As Integer
        Get
            Return MyJob.jobNumber
        End Get
        Set(ByVal value As Integer)
            MyJob.jobNumber = value
        End Set
    End Property

    ''' <summary>
    ''' DesignID
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property DesignID() As Integer
        Get
            Return MyJob.DesignId
        End Get
        Set(ByVal value As Integer)
            MyJob.DesignId = value
        End Set
    End Property

    ''' <summary>
    ''' Is Manual Frame
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property IsManual() As Boolean
        Get
            Return MyIsManual
        End Get
        Set(ByVal value As Boolean)
            MyIsManual = value
        End Set
    End Property

    Public ReadOnly Property Response() As LensDsgd
        Get
            Return MyResponse
        End Get
    End Property

    ''' <summary>
    ''' Set Info for the Left Item
    ''' </summary>
    ''' <param name="OPC">Part Number</param>
    ''' <param name="sph">Sphere</param>
    ''' <param name="cyl">Cylinder</param>
    ''' <param name="axis">Axis</param>
    ''' <param name="add">Addition</param>
    ''' <param name="prism_diop">Prism Diopts</param>
    ''' <param name="prism_axis">Prism Axis</param>
    ''' <param name="monoDIP">Mono DIP</param>
    ''' <param name="height">Height</param>
    ''' <remarks></remarks>
    Public Sub SetLeftItem(ByVal OPC As String, ByVal sph As Double, ByVal cyl As Single, ByVal axis As Integer, ByVal add As Single, ByVal prism_diop As Single, ByVal prism_axis As Integer, ByVal monoDIP As Single, ByVal height As Single)
        LeftSide = New JobItem
        LeftSide.jobSide = "left"
        LeftSide.Blank = New Blank
        LeftSide.Blank.Item = OPC
        LeftSide.Lens = New Lens
        LeftSide.Lens.sphere = sph
        LeftSide.Lens.cylinder = cyl
        LeftSide.Lens.axis = axis
        LeftSide.Lens.addition = add
        LeftSide.Lens.Prism = New Prism
        LeftSide.Lens.Prism.diop = prism_diop
        LeftSide.Lens.Prism.axis = prism_axis
        LeftSide.Lens.prThinOk = True
        LeftSide.Lens.prThinOkSpecified = True
        LeftSide.Lens.monoPD = monoDIP
        LeftSide.Lens.monoPDSpecified = True
        LeftSide.Lens.segHeigth = height
        LeftSide.Lens.segHeigthSpecified = True
    End Sub

    ''' <summary>
    ''' Set Info for the Right Item
    ''' </summary>
    ''' <param name="OPC">Part Number</param>
    ''' <param name="sph">Sphere</param>
    ''' <param name="cyl">Cylinder</param>
    ''' <param name="axis">Axis</param>
    ''' <param name="add">Addition</param>
    ''' <param name="prism_diop">Prism Diopts</param>
    ''' <param name="prism_axis">Prism Axis</param>
    ''' <param name="monoDIP">Mono DIP</param>
    ''' <param name="height">Height</param>
    ''' <remarks></remarks>
    Public Sub SetRightItem(ByVal OPC As String, ByVal sph As Double, ByVal cyl As Double, ByVal axis As Integer, ByVal add As Double, ByVal prism_diop As Double, ByVal prism_axis As Integer, ByVal monoDIP As Double, ByVal height As Double)
        RightSide = New JobItem
        RightSide.jobSide = "right"
        RightSide.Blank = New Blank
        RightSide.Blank.Item = OPC
        RightSide.Lens = New Lens
        RightSide.Lens.sphere = sph
        RightSide.Lens.cylinder = cyl
        RightSide.Lens.axis = axis
        RightSide.Lens.addition = add
        RightSide.Lens.Prism = New Prism
        RightSide.Lens.Prism.diop = prism_diop
        RightSide.Lens.Prism.axis = prism_axis
        RightSide.Lens.prThinOk = True
        RightSide.Lens.prThinOkSpecified = True
        RightSide.Lens.monoPD = monoDIP
        RightSide.Lens.monoPDSpecified = True
        RightSide.Lens.segHeigth = height
        RightSide.Lens.segHeigthSpecified = True
    End Sub

    ''' <summary>
    ''' Add Info for Manual Traces
    ''' </summary>
    ''' <param name="FrameType">0=undefined, 1=Plastic, 2=Metal, 3=Rimless, 4=Optyl</param>
    ''' <param name="DBL">Bridge</param>
    ''' <param name="DIP">Interpupilar Distance</param>
    ''' <param name="nearDIP">Near Interpupilar Distance</param>
    ''' <param name="hrz">A</param>
    ''' <param name="vrt">B</param>
    ''' <param name="diam">ED</param>
    ''' <remarks></remarks>
    Public Sub SetGeoFrame(ByVal FrameType As Integer, ByVal hrz As Double, ByVal vrt As Double, ByVal diam As Double, ByVal DBL As Double, ByVal DIP As Double, ByVal nearDIP As Double)
        MyGeoFrame = New GeoFrame
        MyGeoFrame.Frame = New Frame
        MyGeoFrame.Frame.frameType = FrameType
        MyGeoFrame.Frame.dbl = DBL
        MyGeoFrame.Frame.distPD = DIP
        MyGeoFrame.Frame.nearPD = nearDIP
        MyGeoFrame.hrzBox = hrz
        MyGeoFrame.vrtBox = vrt
        MyGeoFrame.efDiam = diam
        MyIsManual = True
    End Sub

    ''' <summary>
    ''' Add Info for Digital Traces
    ''' </summary>
    ''' <param name="FrameType">0=undefined, 1=Plastic, 2=Metal, 3=Rimless, 4=Optyl</param>
    ''' <param name="DBL">Bridge</param>
    ''' <param name="DIP">Interpupilar Distance</param>
    ''' <param name="nearDIP">Near Interpupilar Distance</param>
    ''' <param name="sizing">Sizing</param>
    ''' <param name="curve">Curve</param>
    ''' <param name="radius">512 Radius [Format: r001,r002,r003,....,r512]</param>
    ''' <remarks></remarks>
    Public Sub SetPattern(ByVal FrameType As Integer, ByVal DBL As Double, ByVal DIP As Double, ByVal nearDIP As Double, ByVal sizing As Integer, ByVal curve As Double, ByVal radius As String)
        MyPattern = New Pattern
        MyPattern.Frame = New Frame
        MyPattern.Frame.frameType = FrameType
        MyPattern.Frame.dbl = DBL
        MyPattern.Frame.distPD = DIP
        MyPattern.Frame.nearPD = nearDIP
        MyPattern.sizing = sizing
        MyPattern.curve = curve
        MyPattern.rad = radius
        MyIsManual = False
    End Sub

    ''' <summary>
    ''' Add Info for Digital Traces
    ''' </summary>
    ''' <param name="FrameType">0=undefined, 1=Plastic, 2=Metal, 3=Rimless, 4=Optyl</param>
    ''' <param name="DBL">Bridge</param>
    ''' <param name="DIP">Interpupilar Distance</param>
    ''' <param name="nearDIP">Near Interpupilar Distance</param>
    ''' <param name="sizing">Sizing</param>
    ''' <param name="curve">Curve</param>
    ''' <param name="radius">An array of integer values that represents the radius</param>
    ''' <remarks></remarks>
    Public Sub SetPattern(ByVal FrameType As Integer, ByVal DBL As Double, ByVal DIP As Double, ByVal nearDIP As Double, ByVal sizing As Integer, ByVal curve As Double, ByVal radius() As Integer)
        Dim i As Integer
        Dim rad As String = ""

        MyPattern = New Pattern
        MyPattern.Frame = New Frame
        MyPattern.Frame.frameType = FrameType
        MyPattern.Frame.dbl = DBL
        MyPattern.Frame.distPD = DIP
        MyPattern.Frame.nearPD = nearDIP
        MyPattern.sizing = sizing
        MyPattern.curve = curve
        MyIsManual = False

        For i = 0 To radius.Length - 1
            If i > 0 Then
                rad &= ","
            End If
            rad &= radius(i)
        Next
        MyPattern.rad = rad
    End Sub

    ''' <summary>
    ''' Generates an XML File from a AccReq class with the Account item
    ''' </summary>
    ''' <param name="filename"></param>
    ''' <remarks></remarks>
    Public Sub GenerateAccountXML(ByVal filename As String)
        Try
            Dim fecha As New DateTime(2010, 5, 1)
            AccountRequest = New AccountReq
            AccountRequest.finaldate = fecha.ToString("u").Substring(0, 10)
            'AccountRequest.item = fecha.ToString("u").Substring(0, 10)()

            ' Creates the XML File with the Job Class
            Serialize("temp_req.xml", AccountRequest)

            Dim XMLDoc As New XmlDocument()
            Dim Reader As XmlTextReader = New XmlTextReader("temp_req.xml")
            Dim Validator As New XmlValidatingReader(Reader)

            Validator.ReadToFollowing("AccReq")
            Dim Writer As XmlTextWriter = New XmlTextWriter(filename, System.Text.Encoding.UTF8)

            Writer.Formatting = Formatting.Indented
            ' Escribo la etiqueta de inicio <?xml........?>
            'Writer.WriteStartDocument()

            ' Agrego la etiqueta del SOAP:Envelope
            'Writer.WriteStartElement("soap:Envelope", "'http://schemas.xmlsoap.org/soap/envelope/' xmlns:xsi='http://www.w3.org/2001/XMLSchema-instance' xmlns:xsd='http://www.w3.org/2001/XMLSchema'")
            'Writer.WriteStartElement("soap", "Envelope", "http://schemas.xmlsoap.org/soap/envelope/")
            Writer.WriteStartElement("soap", "Envelope", "http://schemas.xmlsoap.org/soap/envelope/")
            ' Agrego la etiqueta del SOAP:Header
            Writer.WriteStartElement("soap:Header")
            Writer.WriteEndElement()
            ' Agrego la etiqueta del SOAP:Body
            Writer.WriteStartElement("soap:Body")
            ' Agrego la etiqueta del Job con el namespace
            'Writer.WriteStartElement("Job", "http://192.168.35.168/augen/services/")

            ' Agrego la etiqueta del Job solo
            Writer.WriteStartElement("AccReq")
            'Writer.WriteStartElement("Job", "http://192.168.35.168/augen/services/optisur.xsd")
            Dim InnerXML As String = Reader.ReadInnerXml

            ' Eliminamos los xmlns que estan de mas
            InnerXML = InnerXML.Replace(" xmlns=""http://areyes/areyes/ws/optisur/optisur.xsd""", "")

            ' Agregamos el xmlns que le falta al Envelope
            'InnerXML = InnerXML.Replace(" xmlns:opt=""http://areyes/areyes/ws/optisur/optisur.xsd""", "")


            Writer.WriteRaw(InnerXML)

            Writer.Close()
            Reader.Close()
            XMLDoc.Load(filename)
            Writer = New XmlTextWriter(filename, System.Text.Encoding.UTF8)
            Writer.Formatting = Formatting.Indented
            XMLDoc.WriteContentTo(Writer)
            Writer.Close()

        Catch ex As Exception

        End Try
    End Sub

    ''' <summary>
    ''' Generates an XML File from a Job class with the MyJob item
    ''' </summary>
    ''' <param name="filename">Destination</param>
    ''' <remarks></remarks>
    Public Sub GenerateXML(ByVal filename As String)
        Try
            Select Case MySelectedEyes
                Case Eyes.Left
                    Dim Jobs(0) As JobItem
                    Jobs(0) = LeftSide
                    MyJob.JobItem = Jobs
                Case Eyes.Right
                    Dim Jobs(0) As JobItem
                    Jobs(0) = RightSide
                    MyJob.JobItem = Jobs
                Case Eyes.Both
                    Dim Jobs(1) As JobItem
                    Jobs(0) = RightSide
                    Jobs(1) = LeftSide
                    MyJob.JobItem = Jobs
            End Select
            If MyIsManual Then
                MyJob.Item = MyGeoFrame
            Else
                MyJob.Item = MyPattern
            End If

            ' Creates the XML File with the Job Class
            Serialize("temp_req.xml", MyJob)

            Dim XMLDoc As New XmlDocument()
            Dim Reader As XmlTextReader = New XmlTextReader("temp_req.xml")
            Dim Validator As New XmlValidatingReader(Reader)

            Validator.ReadToFollowing("Job")
            Dim Writer As XmlTextWriter = New XmlTextWriter(filename, System.Text.Encoding.UTF8)

            Writer.Formatting = Formatting.Indented
            ' Escribo la etiqueta de inicio <?xml........?>
            'Writer.WriteStartDocument()

            ' Agrego la etiqueta del SOAP:Envelope
            'Writer.WriteStartElement("soap:Envelope", "'http://schemas.xmlsoap.org/soap/envelope/' xmlns:xsi='http://www.w3.org/2001/XMLSchema-instance' xmlns:xsd='http://www.w3.org/2001/XMLSchema'")
            'Writer.WriteStartElement("soap", "Envelope", "http://schemas.xmlsoap.org/soap/envelope/")
            Writer.WriteStartElement("soap", "Envelope", "http://schemas.xmlsoap.org/soap/envelope/")
            ' Agrego la etiqueta del SOAP:Header
            Writer.WriteStartElement("soap:Header")
            Writer.WriteEndElement()
            ' Agrego la etiqueta del SOAP:Body
            Writer.WriteStartElement("soap:Body")
            ' Agrego la etiqueta del Job con el namespace
            'Writer.WriteStartElement("Job", "http://192.168.35.168/augen/services/")

            ' Agrego la etiqueta del Job solo
            Writer.WriteStartElement("Job")
            'Writer.WriteStartElement("Job", "http://192.168.35.168/augen/services/optisur.xsd")
            Dim InnerXML As String = Reader.ReadInnerXml

            ' Eliminamos los xmlns que estan de mas
            InnerXML = InnerXML.Replace(" xmlns=""http://areyes/areyes/ws/optisur/optisur.xsd""", "")

            ' Agregamos el xmlns que le falta al Envelope
            'InnerXML = InnerXML.Replace(" xmlns:opt=""http://areyes/areyes/ws/optisur/optisur.xsd""", "")


            Writer.WriteRaw(InnerXML)

            Writer.Close()
            Reader.Close()
            XMLDoc.Load(filename)
            Writer = New XmlTextWriter(filename, System.Text.Encoding.UTF8)
            Writer.Formatting = Formatting.Indented
            XMLDoc.WriteContentTo(Writer)
            Writer.Close()

        Catch ex As Exception
            Throw New Exception("Error Consolidating Job Class:" + vbCrLf + ex.Message)
        End Try

    End Sub

    Enum Eyes
        Left
        Right
        Both
    End Enum

    ''' <summary>
    ''' Serializes a class into an XML File
    ''' </summary>
    ''' <param name="filename">Destination File</param>
    ''' <param name="MyJob">Source class</param>
    ''' <remarks></remarks>
    Private Sub Serialize(ByVal filename As String, ByVal MyJob As Object)
        Dim s As New Serialization.XmlSerializer(MyJob.GetType())
        Dim w As IO.TextWriter = New IO.StreamWriter(filename)
        Dim x As New SoapFormatter()
        s.Serialize(w, MyJob)
        w.Close()

    End Sub

    ''' <summary>
    ''' DeSerializes an XML File into a Job Class object
    ''' </summary>
    ''' <param name="filename">XML File</param>
    ''' <param name="MyJob">The Job Class object that will store the data</param>
    ''' <remarks></remarks>
    Private Sub DeSerialize(ByVal filename As String, ByRef MyJob As Job)
        Dim s As New Serialization.XmlSerializer(MyJob.GetType())
        Dim r As IO.TextReader = New IO.StreamReader(filename)
        Try
            MyJob = DirectCast(s.Deserialize(r), Job)
        Catch ex As Exception
            Throw New Exception(ex.Message)
        Finally
            r.Close()
        End Try
    End Sub
    Private Sub DeSerialize(ByVal filename As String, ByRef MyJob As AccStatement)
        Dim s As New Serialization.XmlSerializer(MyJob.GetType())
        Dim r As IO.TextReader = New IO.StreamReader(filename)
        Try
            MyJob = DirectCast(s.Deserialize(r), AccStatement) ''AccStatement
        Catch ex As Exception
            Throw New Exception(ex.Message)
        Finally
            r.Close()
        End Try
    End Sub

    ''' <summary>
    ''' DeSerializes an XML File into a LensDsgd Class object
    ''' </summary>
    ''' <param name="filename">XML File</param>
    ''' <param name="MyJob">The LensDsgd Class object that will store the data</param>
    ''' <remarks></remarks>
    Private Sub DeSerialize(ByVal filename As String, ByRef MyJob As LensDsgd)

        Dim s As New Serialization.XmlSerializer(MyJob.GetType())
        Dim r As IO.TextReader = New IO.StreamReader(filename)
        Try
            MyJob = DirectCast(s.Deserialize(r), LensDsgd)
        Catch ex As Exception
            Throw New Exception(ex.Message)
        Finally
            r.Close()
        End Try
    End Sub

    ' <summary>
    ' DeSerializes an XML File into a DisplFrame Class object
    ' </summary>
    ' <param name="filename">XML File</param>
    ' <param name="MyJob">The DisplFrame Class object that will store the data</param>
    ' <remarks></remarks>
    'Private Sub DeSerialize(ByVal filename As String, ByRef MyJob As DisplFrame)
    '    Dim s As New Serialization.XmlSerializer(MyJob.GetType())
    '    Dim r As IO.TextReader = New IO.StreamReader(filename)
    '    Try
    '        MyJob = DirectCast(s.Deserialize(r), DisplFrame)
    '    Catch ex As Exception
    '        Throw New Exception(ex.Message)
    '    Finally
    '        r.Close()
    '    End Try
    'End Sub

    ''' <summary>
    ''' DeSerializes an XML File into a Displacement Class object
    ''' </summary>
    ''' <param name="filename">XML File</param>
    ''' <param name="MyJob">The Displacement Class object that will store the data</param>
    ''' <remarks></remarks>
    Private Sub DeSerialize(ByVal filename As String, ByRef MyJob As DisplFrame)
        Dim s As New Serialization.XmlSerializer(MyJob.GetType())
        Dim r As IO.TextReader = New IO.StreamReader(filename)
        Try
            MyJob = DirectCast(s.Deserialize(r), DisplFrame)
        Catch ex As Exception
            Throw New Exception(ex.Message)
        Finally
            r.Close()
        End Try
    End Sub

    ''' <summary>
    ''' Process a XML Response File of a LensDSG Request and returns as a String
    ''' </summary>
    ''' <param name="filename">XML File</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function ProcessDSGDResponse(ByVal filename As String) As String
        Try
            Dim Reader As XmlTextReader = New XmlTextReader(filename)
            Dim Validator As New XmlValidatingReader(Reader)
            Dim XMLString As String = ""
            Dim ErrorString As String = ""

            Validator.ReadToFollowing("faultstring")
            ErrorString = Reader.ReadInnerXml()
            If ErrorString.Length > 0 Then
                Reader.Close()
                Validator.Close()
                Throw New Exception("Error en Optisur" + vbCrLf + ErrorString)
            End If

            Reader.Close()
            Validator.Close()
            Reader = New XmlTextReader(filename)
            Validator = New XmlValidatingReader(Reader)


            Validator.ReadToFollowing("LensDsgd")
            'Validator.ReadToFollowing("JobResult")
            XMLString = Reader.ReadInnerXml()
            XMLString = XMLString.Replace("opt:", "")
            XMLString = XMLString.Replace("xmlns:opt=""http://areyes/augen/services/optisur.xsd""", "")

            Reader.Close()
            Validator.Close()

            Dim Writer As XmlTextWriter = New XmlTextWriter(filename, System.Text.Encoding.UTF8)
            Writer.WriteRaw(XMLString)
            Writer.Close()

            Return XMLString

        Catch ex As Exception
            Throw New Exception("Error Processing Response" + vbCrLf + ex.Message)
        End Try
    End Function

    ''' <summary>
    ''' Process a XML Response File of a AccReq Request and returns as a String
    ''' </summary>
    ''' <param name="filename"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function ProcessAccReqResponse(ByVal filename As String) As String
        Try
            Dim Reader As XmlTextReader = New XmlTextReader(filename)
            Dim Validator As New XmlValidatingReader(Reader)
            Dim XMLString As String = ""
            Dim ErrorString As String = ""

            Validator.ReadToFollowing("faultstring")
            ErrorString = Reader.ReadInnerXml()
            If ErrorString.Length > 0 Then
                Reader.Close()
                Validator.Close()
                Throw New Exception("Error en Optisur" + vbCrLf + ErrorString)
            End If

            Reader.Close()
            Validator.Close()
            Reader = New XmlTextReader(filename)
            Validator = New XmlValidatingReader(Reader)


            Validator.ReadToFollowing("AccStatus")
            'Validator.ReadToFollowing("JobResult")
            XMLString = Reader.ReadInnerXml()
            XMLString = XMLString.Replace("opt:", "")
            XMLString = XMLString.Replace("xmlns:opt=""http://areyes/augen/services/optisur.xsd""", "")

            Reader.Close()
            Validator.Close()

            Dim Writer As XmlTextWriter = New XmlTextWriter(filename, System.Text.Encoding.UTF8)
            Writer.WriteRaw(XMLString)
            Writer.Close()

            Return XMLString

        Catch ex As Exception
            Throw New Exception("Error Processing Response" + vbCrLf + ex.Message)
        End Try
    End Function
    ''' <summary>
    ''' Process a XML Response File of a Displ Request and returns as a String
    ''' </summary>
    ''' <param name="filename"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function ProcessDisplFrameResponse(ByVal filename As String) As String
        Try
            Dim Reader As XmlTextReader = New XmlTextReader(filename)
            Dim Validator As New XmlValidatingReader(Reader)
            Dim XMLString As String = ""
            Dim ErrorString As String = ""

            Validator.ReadToFollowing("faultstring")
            ErrorString = Reader.ReadInnerXml()
            If ErrorString.Length > 0 Then
                Reader.Close()
                Validator.Close()
                Throw New Exception("Error en Optisur" + vbCrLf + ErrorString)
            End If

            Reader.Close()
            Validator.Close()
            Reader = New XmlTextReader(filename)
            Validator = New XmlValidatingReader(Reader)

            'Validator.ReadToFollowing("DisplFrame")
            Validator.ReadToFollowing("Displacement")
            XMLString = Reader.ReadInnerXml()
            XMLString = XMLString.Replace("opt:", "")
            XMLString = XMLString.Replace("xmlns:opt=""http://areyes/augen/services/optisur.xsd""", "")
            Reader.Close()
            Validator.Close()

            Dim Writer As XmlTextWriter = New XmlTextWriter(filename, System.Text.Encoding.UTF8)
            Writer.WriteRaw(XMLString)
            Writer.Close()

            Return XMLString

        Catch ex As Exception
            Throw New Exception("Error Processing Response" + vbCrLf + ex.Message)
        End Try
    End Function

    ''' <summary>
    ''' Sends a XML Request to the Optisur Webservice
    ''' </summary>
    ''' <param name="xmlDoc">XML Request</param>
    ''' <param name="strURL">URL of the Optisur Webservice</param>
    ''' <param name="Method">Method to be called (LensDsg|Displ)</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function sendXML(ByVal xmlDoc As XmlDocument, ByVal strURL As String, ByVal Method As String) As Boolean
        Dim success As Boolean = False
        Dim request_stream As Stream
        Dim r_stream As Stream
        Dim response_stream As StreamReader
        Try

            'Create the Web request
            Dim request As HttpWebRequest = DirectCast(WebRequest.Create(strURL & "/" & Method), HttpWebRequest)

            'set the properties
            request.Method = "POST"
            request.ContentType = "text/xml"
            request.Timeout = My.Settings.OptisurServerTimeout * 1000
            '-----------------------------------
            'NEW
            '-----------------------------------
            'request.KeepAlive = False
            '-----------------------------------
            'open the pipe?
            request.Headers.Add("SOAPAction", Method)
            request_stream = request.GetRequestStream()

            'write the XML to the open pipe (e.g. stream)
            xmlDoc.Save(request_stream)
            'CLOSE THE PIPE !!! Very important or next step will time out!!!!
            request_stream.Close()

            'get the response from the webservice
            Dim response As HttpWebResponse = DirectCast(request.GetResponse(), HttpWebResponse)
            r_stream = response.GetResponseStream()
            'convert it
            response_stream = New StreamReader(r_stream, System.Text.Encoding.GetEncoding("utf-8"))

            Dim xmlResp As New XmlDocument()
            xmlResp.InnerXml = response_stream.ReadToEnd()

            Dim writer As XmlTextWriter = New XmlTextWriter("response.xml", System.Text.Encoding.UTF8)
            writer.Formatting = Formatting.Indented

            xmlResp.WriteContentTo(writer)
            writer.Close()

            'Dim writer As XmlTextWriter = New XmlTextWriter("displresp_t1.xml", System.Text.Encoding.UTF8)
            'writer.Formatting = Formatting.Indented

            'xmlResp.WriteContentTo(writer)
            'writer.Close()
            'ThisResponse = New LensDsgd
            'Try
            '    Dim XMLString = ProcessResponse("displresp_t1.xml")
            '    writer = New XmlTextWriter("displresp_t1.xml", System.Text.Encoding.UTF8)
            '    writer.Formatting = Formatting.Indented
            '    writer.WriteStartDocument()
            '    writer.WriteStartElement("DisplFrame", "optisur.xsd")
            '    writer.WriteRaw(XMLString)
            '    writer.Close()
            '    'DeSerialize("displresp_t1.xml", ThisResponse)
            '    'DeSerialize("f:\test5.xml", MyResponse)
            'Catch ex As Exception
            '    Throw New Exception("Reception OK" + vbCrLf + "XML Errors found: " + vbCrLf + ex.Message)
            'Finally
            '    response_stream.Close()

            'End Try
            response_stream.Close()
            success = True
            ErrorNumber = 0

            'MyResponse = MyJob

        Catch ex As WebException
            Dim failed As Boolean = False
            Dim FailedMessage As String = ""
            Try
                Dim Response = DirectCast(ex.Response, HttpWebResponse)
                Dim Stream As Stream
                Dim reader As StreamReader
                Dim xmlResp As New XmlDocument()

                Stream = Response.GetResponseStream()
                'obtienes stream de respuesta
                reader = New StreamReader(Stream)
                'bajas stream a un reader
                xmlResp.InnerXml = reader.ReadToEnd()
                'es un XML que puedes parsear normal

                'aqui obtengo el numero de error
                ErrorNumber = xmlResp.GetElementsByTagName("ID").Item(0).InnerText
                ErrorName = xmlResp.GetElementsByTagName("faultstring").Item(0).InnerText

                'aqui nomas saco el puro faultstring pero puedes parsear el xml como quieras y sacar los demas valores
                Throw (New Exception(ErrorNumber & " - " & ErrorName))
            Catch ex2 As Exception
                failed = True
                FailedMessage = ex2.Message
            Finally
                If failed Then
                    Throw New Exception(FailedMessage)
                End If

            End Try

        Catch ex As Exception
            ErrorNumber = 1
            ErrorName = ex.Message
            success = False
            Throw New Exception(ErrorNumber & " - " & ErrorName)
        End Try

        Return success
    End Function

    ''' <summary>
    ''' Sends a LensDsg Request to the Optisur Webservice
    ''' </summary>
    ''' <param name="fromfilename">XML File</param>
    ''' <remarks></remarks>
    Private Sub SendDSGRequest(ByVal fromfilename As String)
        Dim XMLDoc As New XmlDocument()
        Dim Reader As XmlTextReader = New XmlTextReader(fromfilename)
        Dim Validator As New XmlValidatingReader(Reader)
        Try
            XMLDoc.Load(Validator)
            Calculos = New LensDsgd
            sendXML(XMLDoc, My.Settings.OptisurServer, "LensDsg")
            Dim writer As XmlTextWriter
            writer = New XmlTextWriter("LensDsg_Response.xml", System.Text.Encoding.UTF8)
            Try
                Dim XMLString = ProcessDSGDResponse("response.xml")
                writer.Formatting = Formatting.Indented
                writer.WriteStartDocument()
                writer.WriteStartElement("LensDsgd")
                writer.WriteRaw(XMLString)
                writer.Close()
                DeSerialize("LensDsg_Response.xml", Calculos)
                SetReceivedLensDsgObjects()
                'DeSerialize("f:\test5.xml", MyResponse)
            Catch ex As Exception
                Throw New Exception("Reception OK" + vbCrLf + "XML Errors found: " + vbCrLf + ex.Message)
            Finally
                writer.Close()

            End Try

            'DeSerialize("displresp_t1.xml", Contorno)

        Catch ex As Exception
            Throw New Exception(ex.Message)
        Finally
            Reader.Close()
        End Try

        Reader.Close()
    End Sub

    Private Sub SendAccReqRequest(ByVal fromfilename As String)
        Dim XMLDoc As New XmlDocument()
        Dim Reader As XmlTextReader = New XmlTextReader(fromfilename)
        Dim Validator As New XmlValidatingReader(Reader)
        Try
            XMLDoc.Load(Validator)
            AccountRequest = New AccountReq
            sendXML(XMLDoc, My.Settings.OptisurServer, "Account")
            Dim writer As XmlTextWriter
            writer = New XmlTextWriter("AccReq_Response.xml", System.Text.Encoding.UTF8)
            Try
                Dim XMLString = ProcessAccReqResponse("response.xml")
                writer.Formatting = Formatting.Indented
                writer.WriteStartDocument()
                writer.WriteStartElement("AccStatement")
                writer.WriteRaw(XMLString)
                writer.Close()
                DeSerialize("AccReq_Response.xml", AccountStatement)
                'MsgBox(AccountStatement.ToString, MsgBoxStyle.Exclamation)
                GetCreditsInformation()
                'DeSerialize("f:\test5.xml", MyResponse)
            Catch ex As Exception
                Throw New Exception("Reception OK" + vbCrLf + "XML Errors found: " + vbCrLf + ex.Message)
            Finally
                writer.Close()

            End Try

            'DeSerialize("displresp_t1.xml", Contorno)

        Catch ex As Exception
            Throw New Exception(ex.Message)
        Finally
            Reader.Close()
        End Try

        Reader.Close()
    End Sub


    ''' <summary>
    ''' Sends a calcula Request to the Optisur Webservice
    ''' </summary>
    ''' <param name="fromfilename">XML File</param>
    ''' <remarks></remarks>
    Private Sub SendCalculaRequest(ByVal fromfilename As String)
        Dim XMLDoc As New XmlDocument()
        Dim Reader As XmlTextReader = New XmlTextReader(fromfilename)
        Dim Validator As New XmlValidatingReader(Reader)
        Try
            XMLDoc.Load(Validator)
            Calculos = New LensDsgd
            sendXML(XMLDoc, My.Settings.OptisurServer, "calcula")
            Dim writer As XmlTextWriter
            writer = New XmlTextWriter("Calcula_Response.xml", System.Text.Encoding.UTF8)
            Try
                Dim XMLString = ProcessDSGDResponse("response.xml")
                writer.Formatting = Formatting.Indented
                writer.WriteStartDocument()
                writer.WriteStartElement("LensDsgd")
                writer.WriteRaw(XMLString)
                writer.Close()
                DeSerialize("Calcula_Response.xml", Calculos)
                SetReceivedLensDsgObjects()
                'DeSerialize("f:\test5.xml", MyResponse)
            Catch ex As Exception
                Throw New Exception("Reception OK" + vbCrLf + "XML Errors found: " + vbCrLf + ex.Message)
            Finally
                writer.Close()

            End Try

            'DeSerialize("displresp_t1.xml", Contorno)

        Catch ex As Exception
            Throw New Exception(ex.Message)
        Finally
            Reader.Close()
        End Try

        Reader.Close()
    End Sub

    ''' <summary>
    ''' Sends a Displacement Request to the Optisur Webservice
    ''' </summary>
    ''' <param name="fromfilename">XML File</param>
    ''' <remarks></remarks>
    Private Sub SendDisplacementRequest(ByVal fromfilename As String)
        Dim XMLDoc As New XmlDocument()
        Dim Reader As XmlTextReader = New XmlTextReader(fromfilename)
        Dim Validator As New XmlValidatingReader(Reader)
        Try
            XMLDoc.Load(Validator)
            Contorno = New DisplFrame
            'Contorno = New Displacement
            sendXML(XMLDoc, My.Settings.OptisurServer, "Displ")
            Dim writer As XmlTextWriter
            writer = New XmlTextWriter("Displ_Response.xml", System.Text.Encoding.UTF8)
            Try
                Dim XMLString = ProcessDisplFrameResponse("response.xml")
                writer.Formatting = Formatting.Indented
                'writer.WriteStartDocument()
                'writer.WriteStartElement("Displacement")
                writer.WriteStartElement("DisplFrame")
                writer.WriteRaw(XMLString)
                writer.Close()
                DeSerialize("Displ_Response.xml", Contorno)
                SetReceivedDisplacementObjects()
                'DeSerialize("f:\test5.xml", MyResponse)
            Catch ex As Exception
                Throw New Exception("Reception OK" + vbCrLf + "XML Errors found: " + vbCrLf + ex.Message)
            Finally
                writer.Close()

            End Try

            'DeSerialize("displresp_t1.xml", Contorno)

        Catch ex As Exception
            Throw New Exception(ex.Message)
        Finally
            Reader.Close()
        End Try

        Reader.Close()
    End Sub

    Private Sub GetCreditsInformation()
        'Dim HDRX_Buyed As Integer = 0, HDRX_Used As Integer = 0
        'Dim TRIN8_12_Buyed As Integer = 0, TRIN8_12_Used As Integer = 0
        'Dim TRIN8_12HD_Buyed As Integer = 0, TRIN8_12HD_Used As Integer = 0
        'Dim SPACIA_Buyed As Integer = 0, SPACIA_Used As Integer = 0
        'Dim SPACIAHD_Buyed As Integer = 0, SPACIAHD_Used As Integer = 0
        'Dim TRIN13_17_Buyed As Integer = 0, TRIN13_17_Used As Integer = 0
        'Dim TRIN13_17HD_Buyed As Integer = 0, TRIN13_17HD_Used As Integer = 0
        Dim i As Integer

        Dim table As New DataTable
        table.Columns.Add("ID")
        table.Columns.Add("Descripcion")
        table.Columns.Add("Cantidad")


        For i = 0 To AccountStatement.AccStatus.Length - 1
            With AccountStatement.AccStatus(i)
                'Dim MyCredit As SpecialDesignsCreditStatus
                'Select Case .desId
                '    Case Credits_HDRx.DesingID
                '        MyCredit = Credits_HDRx
                '        HDRX_Buyed += .buyed
                '        HDRX_Used += .used
                '    Case Credits_SPACIA.DesingID
                '        MyCredit = Credits_SPACIA
                '        SPACIA_Buyed += .buyed
                '        SPACIA_Used += .used
                '    Case Credits_SPACIAHD.DesingID
                '        MyCredit = Credits_SPACIAHD
                '        SPACIAHD_Buyed += .buyed
                '        SPACIAHD_Used += .used
                '    Case Credits_TRIN8_12.DesingID
                '        MyCredit = Credits_TRIN8_12
                '        TRIN8_12_Buyed += .buyed
                '        TRIN8_12_Used += .used
                '    Case Credits_TRIN8_12HD.DesingID
                '        MyCredit = Credits_TRIN8_12HD
                '        TRIN8_12HD_Buyed += .buyed
                '        TRIN8_12HD_Used += .used
                '    Case Credits_TRIN13_17.DesingID
                '        MyCredit = Credits_TRIN13_17
                '        TRIN13_17_Buyed += .buyed
                '        TRIN13_17_Used += .used
                '    Case Credits_TRIN13_17HD.DesingID
                '        MyCredit = Credits_TRIN13_17HD
                '        TRIN13_17HD_Buyed += .buyed
                '        TRIN13_17HD_Used += .used
                'End Select

                Dim found As Boolean = False
                For Each row As DataRow In table.Rows
                    If row("ID") = .desId Then
                        row("Cantidad") += .buyed - .used
                        found = True
                        Exit For
                    End If
                Next
                If Not found Then
                    table.Rows.Add(.desId, .desName, .buyed - .used)
                End If
            End With
        Next


        Dim datatbl As DataTable = GetSpecialDesignsTable()
        For Each dr As DataRow In table.Rows
            For Each ndr As DataRow In datatbl.Rows
                If dr.Item("ID") = ndr.Item("ID") Then
                    ndr.Item("Cantidad") = dr.Item("Cantidad")
                    Exit For
                End If
            Next
        Next

        DT = datatbl
        'DT = table

        'Credits_HDRx.SetCredit(HDRX_Buyed - HDRX_Used)
        'Credits_TRIN8_12.SetCredit(TRIN8_12_Buyed - TRIN8_12_Used)
        'Credits_TRIN8_12HD.SetCredit(TRIN8_12HD_Buyed - TRIN8_12HD_Used)
        'Credits_SPACIA.SetCredit(SPACIA_Buyed - SPACIA_Used)
        'Credits_SPACIAHD.SetCredit(SPACIAHD_Buyed - SPACIAHD_Used)
        'Credits_TRIN13_17.SetCredit(TRIN13_17_Buyed - TRIN13_17_Used)
        'Credits_TRIN13_17HD.SetCredit(TRIN13_17HD_Buyed - TRIN13_17HD_Used)
    End Sub

    ''' <summary>
    ''' Returns a DataTable with the Special Designs Items Available to Order
    ''' </summary>
    ''' <returns>DataTable</returns>
    ''' <remarks></remarks>
    Public Function GetSpecialDesignsTable() As DataTable
        Dim t As New SqlDB(Laboratorios.ConnStr)
        Dim Failed As Boolean = False
        Dim FailedMessage As String = ""
        Dim value As DataTable = Nothing


        Dim ds As New DataSet()
        Dim SQLStr As String = ""
        Try

            t.OpenConn()
            SQLStr = "select ID, design as Descripcion, 0 as Cantidad from TblSpecialDesigns with(nolock) order by id"
            ds = t.SQLDS(SQLStr, "t1")
            value = ds.Tables("t1")
        Catch ex As Exception
            Failed = True
            FailedMessage = ex.Message
        Finally
            t.CloseConn()
        End Try
        If Failed Then
            Throw New Exception("Error al obtener la tabla de diseños." & vbCrLf & FailedMessage)
        End If
        Return value
    End Function


    ''' <summary>
    ''' Sets the corresponding LensDSG Objects received from the Optisur Webservice
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetReceivedLensDsgObjects()
        Dim Left As New LensDsg
        Dim Right As New LensDsg
        Try
            If Calculos.LensDsg(0).side = "right" Then
                Right = Calculos.LensDsg(0)
            ElseIf Calculos.LensDsg(0).side = "left" Then
                Left = Calculos.LensDsg(0)
            End If
        Catch ex As Exception
        End Try
        Try
            If Calculos.LensDsg(1).side = "left" Then
                Left = Calculos.LensDsg(1)
            ElseIf Calculos.LensDsg(1).side = "right" Then
                Right = Calculos.LensDsg(1)
            End If
        Catch ex As Exception
        End Try

        If Left.Prism Is Nothing Then
            Left.Prism = New Prism
            Left.Prism.axis = 0
            Left.Prism.diop = 0
        End If
        If Left.minEThk Is Nothing Then
            Left.minEThk = New Thickness
            Left.minEThk.thk = 0
        End If
        If Left.maxEthk Is Nothing Then
            Left.maxEthk = New Thickness
            Left.maxEthk.thk = 0
        End If
        If Right.Prism Is Nothing Then
            Right.Prism = New Prism
            Right.Prism.axis = 0
            Right.Prism.diop = 0
        End If
        If Right.minEThk Is Nothing Then
            Right.minEThk = New Thickness
            Right.minEThk.thk = 0
        End If
        If Right.maxEthk Is Nothing Then
            Right.maxEthk = New Thickness
            Right.maxEthk.thk = 0
        End If


        LeftLensDSG = Left
        RightLensDSG = Right

    End Sub

    ''' <summary>
    ''' Sets the corresponding DisplPattern Objects received from the Optisur Webservice
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetReceivedDisplacementObjects()
        Dim Left As New DisplPattern
        Dim Right As New DisplPattern
        Try
            If Contorno.DisplPattern(0).side = "right" Then
                Right = Contorno.DisplPattern(0)
                RightDesign = GetDrawingDesign(RightSide.Blank.Item)
                Left = New DisplPattern
            ElseIf Contorno.DisplPattern(0).side = "left" Then
                Left = Contorno.DisplPattern(0)
                LeftDesign = GetDrawingDesign(LeftSide.Blank.Item)
                Right = New DisplPattern
            End If
        Catch ex As Exception
        End Try
        Try
            If Contorno.DisplPattern(1).side = "left" Then
                Left = Contorno.DisplPattern(1)
                LeftDesign = GetDrawingDesign(LeftSide.Blank.Item)
            ElseIf Contorno.DisplPattern(1).side = "right" Then
                Right = Contorno.DisplPattern(1)
                RightDesign = GetDrawingDesign(RightSide.Blank.Item)
            End If
        Catch ex As Exception
        End Try
        '-----------------------------------------------
        If Right.descentering Is Nothing Then
            Right.descentering = New DistXY
            Right.descentering.x = 0
            Right.descentering.y = 0
        End If
        If Right.LRP Is Nothing Then
            Right.LRP = New DistXY
            Right.LRP.x = 0
            Right.LRP.y = 0
        End If
        If Right.displ Is Nothing Then
            Right.displ = New DistXY
            Right.displ.x = 0
            Right.displ.y = 0
        End If
        If Left.descentering Is Nothing Then
            Left.descentering = New DistXY
            Left.descentering.x = 0
            Left.descentering.y = 0
        End If
        If Left.LRP Is Nothing Then
            Left.LRP = New DistXY
            Left.LRP.x = 0
            Left.LRP.y = 0
        End If
        If Left.displ Is Nothing Then
            Left.displ = New DistXY
            Left.displ.x = 0
            Left.displ.y = 0
        End If
        '-----------------------------------------------


        LeftPattern = Left
        RightPattern = Right

    End Sub

    Enum CalculationOption
        JustCalculate
        CalculateAndSave
    End Enum
    ''' <summary>
    ''' Sends the request to the Optisur Webservice and stores the requested informacion in a LensDSG Object
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub GetCalculos(ByVal Options As CalculationOption)
        Select Case Options
            Case CalculationOption.CalculateAndSave
                Try : GenerateXML("LensDSGReq.xml") : Catch ex As Exception : Throw New Exception("Error de Diseños Especiales. " + vbCrLf + "Error al crear archivo XML de petición. " + vbCrLf + ex.Message) : End Try
                Try : SendDSGRequest("LensDSGReq.xml") : Catch ex As Exception : Throw New Exception("Error de Diseños Especiales. " + vbCrLf + "Error al procesar archivo XML de respuesta LensDSGReq.XML " + vbCrLf + ex.Message) : End Try
            Case CalculationOption.JustCalculate
                Try : GenerateXML("CalculaReq.xml") : Catch ex As Exception : Throw New Exception("Error de Diseños Especiales. " + vbCrLf + "Error al crear archivo XML de petición. " + vbCrLf + ex.Message) : End Try
                Try : SendCalculaRequest("CalculaReq.xml") : Catch ex As Exception : Throw New Exception("Error de Diseños Especiales. " + vbCrLf + "Error al procesar archivo XML de respuesta CalculaReq.XML" + vbCrLf + ex.Message) : End Try
        End Select

    End Sub

    Public Sub GetCredits()
        Try : GenerateAccountXML("AccReq_Request.xml") : Catch ex As Exception : Throw New Exception("Error de Diseños Especiales. " + vbCrLf + "Error al crear archivo XML de petición. " + vbCrLf + ex.Message) : End Try
        Try : SendAccReqRequest("AccReq_Request.xml") : Catch ex As Exception : Throw New Exception("Error de Diseños Especiales. " + vbCrLf + "Error de conexión con el servidor Optisur. " + vbCrLf + ex.Message) : End Try
    End Sub

    ''' <summary>
    ''' Sends the request to the Optisur Webservice and stores the requested informacion in a Displ Object
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub GetContorno()
        Try : GenerateXML("DisplReq.xml") : Catch ex As Exception : Throw New Exception("Error de Diseños Especiales. " + vbCrLf + "Error al crear archivo XML de petición. " + vbCrLf + ex.Message) : End Try
        Try : SendDisplacementRequest("DisplReq.xml") : Catch ex As Exception : Throw New Exception("Error de Diseños Especiales. " + vbCrLf + "Error de conexión con el servidor Optisur. " + vbCrLf + ex.Message) : End Try
    End Sub



    ''' <summary>
    ''' Returns an Array of 512 radius of the frame received from the Optisur Webservice
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetRadios() As Integer()
        Dim RadComma() As String

        Dim radius(511) As Integer
        Dim rlen As Integer = 1

        If (Contorno.Pattern Is Nothing) Then Throw New Exception("GetRadios() : No hay radios. Contorno.Pattern está vacío. No hay trazo.")

        RadComma = Contorno.Pattern.rad.Split(New [Char]() {" "c}, StringSplitOptions.RemoveEmptyEntries)

        If RadComma.Length > 512 Then rlen = 2

        Dim i As Integer
        For i = 0 To RadComma.Length - rlen

            If RadComma(i) <> "" Then
                radius(i) = CInt(RadComma(i))
            End If
        Next

        Return radius
    End Function

    'Public Function GetRadios() As Integer()
    '    Dim temp As String = Contorno.Pattern.rad.Replace(" ", ",")
    '    temp = temp.TrimEnd(vbCrLf)

    '    Dim RadComma() As String = temp.Split(",")

    '    Dim radius(511) As Integer

    '    Dim i As Integer
    '    For i = 0 To radius.Length - 1 And i < 512
    '        radius(i) = CInt(RadComma(i))
    '    Next

    '    Return radius
    'End Function

    ''' <summary>
    ''' Returns de Right Lens Calculations
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property LensDsgRight() As LensDsg
        Get
            Return RightLensDSG
        End Get
        'Set(ByVal value As LensDsg)
        '    Calculos.LensDsg(0) = value
        'End Set
    End Property

    ''' <summary>
    ''' Returns de Left Lens Calculations
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property LensDsgLeft() As LensDsg
        Get
            Return LeftLensDSG
        End Get
        'Set(ByVal value As LensDsg)
        '    Calculos.LensDsg(1) = value
        'End Set
    End Property

    ''' <summary>
    ''' Returns the Left Displacement Values
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property DisplLeft() As DisplPattern
        Get
            Return LeftPattern
        End Get
    End Property

    ''' <summary>
    ''' Returns the Right Displacement Values
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property DisplRight() As DisplPattern
        Get
            Return RightPattern
        End Get
    End Property

    ''' <summary>
    ''' Sets the New Left Lens Height for new calculations
    ''' </summary>
    ''' <param name="Height"></param>
    ''' <remarks></remarks>
    Public Sub SetLeftHeight(ByVal Height As Single)
        LeftSide.Lens.segHeigth = CDbl(Height)
    End Sub

    ''' <summary>
    ''' Sets the New Right Lens Height for new calculations
    ''' </summary>
    ''' <param name="Height"></param>
    ''' <remarks></remarks>
    Public Sub SetRightHeight(ByVal Height As Single)
        RightSide.Lens.segHeigth = CDbl(Height)
    End Sub

    ''' <summary>
    ''' Returns the Drawing Design of a partnum (Monofocal|Flat Top|Progressive)
    ''' </summary>
    ''' <param name="partnum">Part Number</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function GetDrawingDesign(ByVal partnum As String) As Pastilla.Types
        Dim t As New SqlDB()
        Dim Failed As Boolean = False
        Dim FailedMessage As String = ""
        Dim value As Pastilla.Types = Pastilla.Types.Monofocal
        Dim IsSV As Boolean = False, IsFT As Boolean = False, IsPG As Boolean = False
        t.SQLConn = New SqlClient.SqlConnection(Laboratorios.ConnStr)
        Dim ds As New DataSet()
        Dim SQLStr As String = ""
        Try

            t.OpenConn()
            SQLStr = "SELECT a.IsSV, a.IsFT, a.IsPG FROM TblDesigns a with(nolock) INNER JOIN VwPartTRECEUX b with(nolock) ON a.DisenoID = b.Diseño WHERE (b.partnum = '" & partnum & "')"
            ds = t.SQLDS(SQLStr, "t1")
            If ds.Tables("t1").Rows.Count > 0 Then
                With (ds.Tables("t1").Rows(0))
                    IsSV = .Item("IsSV")
                    IsFT = .Item("IsFT")
                    IsPG = .Item("IsPG")
                End With
            Else
                Failed = True
                FailedMessage = "No se encontró registro de diseño"
            End If
            If IsSV Then value = Pastilla.Types.Monofocal
            If IsFT Then value = Pastilla.Types.FlatTop
            If IsPG Then value = Pastilla.Types.Progressive
        Catch ex As Exception
            Failed = True
            FailedMessage = ex.Message
        Finally
            t.CloseConn()
        End Try
        If Failed Then
            Throw New Exception("Error al obtener diseño para dibujar pastilla: " & FailedMessage)
        End If
        Return value
    End Function

    ''' <summary>
    ''' Returns the Left Lens Drawing Design
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property LeftDrawingDesign() As Pastilla.Types
        Get
            Return LeftDesign
        End Get
    End Property

    ''' <summary>
    ''' Returns the Right Lens Drawing Design
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property RightDrawingDesign() As Pastilla.Types
        Get
            Return RightDesign
        End Get
    End Property

    ''' <summary>
    ''' Returns the Actual Error Code
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property ErrorCode() As Integer
        Get
            Return ErrorNumber
        End Get
    End Property

    ''' <summary>
    ''' Returns the Actual Error Description
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property ErrorDescription() As String
        Get
            Return ErrorName
        End Get
    End Property

    ''' <summary>
    ''' Returns the GeoFrame object
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property GeoFrame() As GeoFrame
        Get
            Return MyGeoFrame
        End Get
    End Property

    '''' <summary>
    '''' Returns de Job Item Object that is used to request the Optisur Webservice
    '''' </summary>
    '''' <value></value>
    '''' <returns></returns>
    '''' <remarks></remarks>
    'Public ReadOnly Property Job() As Job
    '    Get
    '        Return MyJob
    '    End Get
    'End Property


End Class

