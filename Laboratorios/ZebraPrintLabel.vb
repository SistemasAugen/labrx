Imports System.Drawing.Printing

Public Class ZebraPrintLabel
    Public AdicionD As String
    Public AdicionI As String
    Public Armazon As String
    Public AnilloD As String
    Public AnilloI As String
    Public AnilloD_HD As String
    Public AnilloI_HD As String
    Public BaseD As String
    Public BaseI As String
    Public BloqueD As String
    Public BloqueI As String
    Public CilindroD As String
    Public CilindroI As String
    Public DIP As String
    Public Diseño As String
    Public DV_D As String
    Public DH_D As String
    Public DV_I As String
    Public DH_I As String
    Public EjeD As String
    Public EjeI As String
    Public EsferaD As String
    Public EsferaI As String
    Public GrosorCentroD As String
    Public GrosorCentroI As String
    Public Ordernum As String
    Public RefDigital As String
    Public PO As String
    Public Material As String
    Public MoldeD As String
    Public MoldeI As String
    Public RxNumLocal As String
    Public Rx As String

    Public NombreCompleto As String
    Public Nombres As String
    Public APaterno As String
    Public AMaterno As String
    Public Grado As String
    Public Grupo As String
    Public Escolaridad As String
    Public Fecha As String
    Public ID_Opto As String
    Public Escuela As String
    Public ID_Escuela As String
    Public Municipio As String
    Public Estado As String

    Public Tienda As String
    Public Sucursal As String
    Public NumSucursal As String
    Public NoEmpleado As String

    Dim Label2D_Name As String
    Dim Label2D_Rx As String
    Dim Label2D_LambdaFile As String
    Dim Label2D_FrameSizeFile As String
    Dim Label2D As String

    Public Custnum As Integer

    Public PrintedOK As String

    '#JGARIBALDI 25 DE JULIO# Variable que almacenará el BIN
    Public WHBIN_Bin As String

    'Llamada principal, de aquí se decide que tipo de etiqueta se imprime
    Public Sub PrintMEILabel()
        ' Mod # Pedro Farías Lozano #
        ' # Sólo se imprime un tipo de etiqueta - PrintMEILabel_SemiTerminado
        Dim Etiqueta As String = ""
        Try
            PrintMEILabel_SemiTerminado(Etiqueta)
            Manda_a_Impresora(Etiqueta, My.Application.Info.DirectoryPath + "\LabelVantage_ZPLII.txt")

            PrintWHBIN_Label_200dpi(Etiqueta)
            Manda_a_Impresora(Etiqueta, My.Application.Info.DirectoryPath + "\LabelVantage_ZPLII.txt")
        Catch ex As Exception
            MsgBox("PrintMEILabel" + ex.Message, MsgBoxStyle.Critical, "PrintMEILabel")
        End Try

    End Sub

    Public Sub SetLabel2D_Info(ByVal LambdaFile As String, ByVal FrameSizeFile As String)
        Dim Nombre As String = Nombres
        Dim Apellido As String = APaterno
        Label2D = ""

        If Nombres.Contains(" ") Then
            Nombre = Nombres.Substring(0, Nombres.IndexOf(" "))
        End If
        If APaterno.Length > 0 Then
            APaterno = APaterno.Substring(0, 1) & "."
        End If

        Label2D_Name = (Nombre & " " & APaterno).PadRight(20, " ")
        Label2D_Rx = "Rx " & PO
        Label2D_LambdaFile = LambdaFile
        Label2D_FrameSizeFile = FrameSizeFile

        'VFFernanda P.         ,WFRx 255667,XFlambda-m.dxf,YF46-18-135.dxf,s
        'VFLUDOVICO M.         ,WFRx 364302,XFlambda-h.dxf,YF46-18-135.dxf,s
        Label2D &= "VF" & Label2D_Name & ","                    ' Nombres
        Label2D &= "WF" & Label2D_Rx & ","                      ' No. Receta
        Label2D &= "XF" & Label2D_LambdaFile & ","              ' Archivo Lambda (Hombre o Mujer)
        Label2D &= "YF" & Label2D_FrameSizeFile & ","           ' Archivo Medidas Armazon
        Label2D &= "s"                                          ' Inicializar Lasser

    End Sub

    'ADD # RTORRES # 21 OCT 2015 # SE MODIFICO LA ETIQUETA QUE LEE LA MEI
    Public Sub PrintWHBIN_Label_200dpi(ByRef etiqueta1 As String)
        Dim WHBIN As Sql_AperturasVB = New Sql_AperturasVB()

        etiqueta1 = ""
        etiqueta1 = etiqueta1 + " -------------------------------------------------------------------------------------------------" + vbCrLf
        etiqueta1 = etiqueta1 + " Archivo de Etiqueta LabRx WHBIN RTG 10/21/2015 a 200 DPI"
        etiqueta1 = etiqueta1 + " -------------------------------------------------------------------------------------------------" + vbCrLf
        etiqueta1 = etiqueta1 + " Date/Time : " & DateTime.Now.ToShortDateString & vbCrLf
        etiqueta1 = etiqueta1 + " Language : ZPL II (Zebra Programming Language V2.0)" & vbCrLf
        etiqueta1 = etiqueta1 + "--------------------------------------------------------------------------------------------------" + vbCrLf
        etiqueta1 += "^XA"


        etiqueta1 += "^FO210,0^BY3,2.0^BCN,150,50,N,Y^FD" & Me.Ordernum & "^FS"

        etiqueta1 += vbCrLf
        etiqueta1 += "^FO50,20^ABN,20,^FDRx.^FS"
        etiqueta1 += vbCrLf
        etiqueta1 += "^FO50,50^ABN,20,^FD " & Me.RxNumLocal & "^FS"
        etiqueta1 += vbCrLf

        etiqueta1 += "^FO50,220^A0N,20,20"
        etiqueta1 += "^FD" & Date.Now.ToShortDateString & " " & Date.Now.ToShortTimeString & "^FS"
        etiqueta1 += vbCrLf

        etiqueta1 += "^FO660,50^A0N,75,"
        etiqueta1 += vbCrLf
        etiqueta1 += " ^FD" & Me.WHBIN_Bin & "^FS"

        etiqueta1 += "^XZ"
        etiqueta1 += vbCrLf

    End Sub


    Function Print_300DPI_FromString(ByVal StringCode As String, ByVal SourceFileStr As String, ByVal Extension As String) As Boolean
        Dim Result As Boolean = False
        SourceFileStr &= "." & Extension

        Dim MyStream As System.IO.Stream = System.IO.File.Open(SourceFileStr, IO.FileMode.Create, IO.FileAccess.ReadWrite, IO.FileShare.None)

        Dim SWriter As New System.IO.StreamWriter(MyStream)
        SWriter.WriteLine(StringCode)
        SWriter.Close()
        Try
            Dim Printed As Boolean = False
            Do While Not Printed

                'If Entrada = 216 Or Entrada = 222 Or Entrada = 121 Then
                System.IO.File.Copy(SourceFileStr, My.Settings.ZebraPrintPort)  ' Copy source to target.
                Printed = True
                PrintedOK = True
                'Else
                'Printed = False
                'If MsgBox("La impresora no está disponible." + vbCrLf + "Por favor, verifique que esté bien conectada y encendida." + vbCrLf + "Deseas imprimirla de nuevo?", MsgBoxStyle.YesNo + MsgBoxStyle.Exclamation) = MsgBoxResult.No Then
                'Printed = True
                'PrintedOK = False
                'End If
            Loop
            Result = PrintedOK
        Catch ex As Exception
            'Return False
            Throw New Exception("Error al imprimir: " + ex.Message)
        End Try
        Return Result
    End Function

    Function PrintLabelVantage_Fundaciones_300DPI_String() As String
        Dim Label As String = ""

        Label &= (" -------------------------------------------------------------------------------------------------") & vbCrLf
        Label &= (" Archivo de Etiqueta LabRx") & vbCrLf
        Label &= (" -------------------------------------------------------------------------------------------------") & vbCrLf
        Label &= (" FileName : LabelOne.txt") & vbCrLf
        Label &= (" Date/Time : " + DateTime.Now.ToShortDateString) & vbCrLf
        Label &= (" Language : ZPL II (Zebra Programming Language V2.0)") & vbCrLf
        Label &= ("--------------------------------------------------------------------------------------------------") & vbCrLf
        Label &= ("") & vbCrLf : Label &= ("") & vbCrLf
        Label &= ("^XA") & vbCrLf 'header
        Label &= ("") & vbCrLf
        'Label &= ("^FO30,25^BY3")       ' Label Vantage
        'Label &= ("^AFN,25,13")
        'Label &= ("^FDVantage:^FS")
        Label &= ("") & vbCrLf
        Label &= ("^FO245,15") & vbCrLf     ' Vantage Barcode
        Label &= ("^BCN,120,N,N,N") & vbCrLf
        Label &= ("^FD" & Me.Ordernum & "^FS") & vbCrLf
        Label &= ("") & vbCrLf
        Label &= ("^FO315,140^BY3") & vbCrLf       ' Vantage
        Label &= ("^A0N,40,60") & vbCrLf
        Label &= ("^FD" & Me.Ordernum & "^FS") & vbCrLf
        Label &= ("") & vbCrLf
        Label &= ("^FO130,200^BY3") & vbCrLf       ' Label de Esfera
        Label &= ("^A0N,25,25") & vbCrLf
        Label &= ("^FDESFERA^FS") & vbCrLf
        Label &= ("") & vbCrLf & vbCrLf
        Label &= ("^FO270,200^BY3") & vbCrLf      ' Label de Cilindo
        Label &= ("^A0N,25,25") & vbCrLf
        Label &= ("^FDCILINDRO^FS") & vbCrLf
        Label &= ("") & vbCrLf
        Label &= ("^FO420,200^BY3") & vbCrLf      ' Label de Eje
        Label &= ("^A0N,25,25") & vbCrLf
        Label &= ("^FDEJE^FS") & vbCrLf
        Label &= ("") & vbCrLf & vbCrLf
        Label &= ("^FO510,200^BY3") & vbCrLf      ' Label de Adicion
        Label &= ("^A0N,25,25") & vbCrLf
        Label &= ("^FDADICION^FS") & vbCrLf
        Label &= ("") & vbCrLf & vbCrLf
        Label &= ("^FO625,200^BY3") & vbCrLf      ' Label de DH
        Label &= ("^A0N,25,25") & vbCrLf
        Label &= ("^FDDH^FS") & vbCrLf
        Label &= ("") & vbCrLf & vbCrLf
        Label &= ("^FO705,200^BY3") & vbCrLf      ' Label de DV
        Label &= ("^A0N,25,25") & vbCrLf
        Label &= ("^FDDV^FS") & vbCrLf
        Label &= ("") & vbCrLf & vbCrLf
        Label &= ("^FO780,200^BY3") & vbCrLf      ' Label de DIP
        Label &= ("^A0N,25,25") & vbCrLf
        Label &= ("^FDDIP^FS") & vbCrLf
        Label &= ("") & vbCrLf & vbCrLf
        Label &= ("^FO862,15^BY3") & vbCrLf     ' Material
        Label &= ("^A@N,35,30,E:LTE50846.FNT") & vbCrLf
        Label &= ("^FD" & Material & " " & Diseño & "^FS") & vbCrLf
        Label &= ("") & vbCrLf & vbCrLf
        Label &= ("^FO862,92^BY3") & vbCrLf          ' Label Armazon
        Label &= ("^A0N,27,33") & vbCrLf
        Label &= ("^FDArmazon:^FS") & vbCrLf
        Label &= ("") & vbCrLf & vbCrLf
        Label &= ("^FO862,130") & vbCrLf          ' Armazon
        Label &= ("^A@N,60,75,E:LTE50846.FNT") & vbCrLf
        Label &= ("^FD" & Me.Armazon & "^FS") & vbCrLf
        Label &= "" & vbCrLf
        Label &= ("^FO862,250^BY3") & vbCrLf      ' Label de Rx
        Label &= ("^A@N,60,75,E:LTE50846.FNT") & vbCrLf
        Label &= ("^FDTz " & Me.RefDigital & "^FS") & vbCrLf
        Label &= ("") & vbCrLf & vbCrLf
        Label &= ("^FO872,325") & vbCrLf          ' Rx
        Label &= ("^BCN,75,N,N,N") & vbCrLf
        Label &= ("^FD" & Me.RefDigital & "^FS") & vbCrLf ' Trazo Barra
        Label &= ("") & vbCrLf & vbCrLf
        Label &= ("^FO30,233") & vbCrLf
        Label &= ("^GB730,1,2,^FS") & vbCrLf
        Label &= ("") & vbCrLf & vbCrLf
        Label &= ("^LRY ^FO30,235") & vbCrLf
        Label &= ("^GB730,75,90,,0^FS") & vbCrLf     '^GB265
        Label &= ("^FO45,250^BY3") & vbCrLf          ' Label D
        Label &= ("^A@N,60,75") & vbCrLf
        Label &= ("^FDD^FS") & vbCrLf
        Label &= ("") & vbCrLf & vbCrLf
        Label &= ("^FO120,250^BY3") & vbCrLf          ' Esfera D
        Label &= ("^A@N,60,60") & vbCrLf
        Label &= ("^FD" & Me.EsferaD & "^FS") & vbCrLf
        Label &= ("") & vbCrLf & vbCrLf
        Label &= ("^FO255,250^BY3") & vbCrLf         ' Cilindro D
        Label &= ("^A@N,60,60") & vbCrLf
        Label &= ("^FD" & Me.CilindroD & "^FS") & vbCrLf
        Label &= ("") & vbCrLf & vbCrLf
        Label &= ("^FO410,250^BY3") & vbCrLf         ' Eje D
        Label &= ("^A@N,60,60") & vbCrLf
        Label &= ("^FD" & Me.EjeD & "^FS") & vbCrLf
        Label &= ("") & vbCrLf & vbCrLf
        Label &= ("^FO510,250^BY3") & vbCrLf         ' Adicion D
        Label &= ("^A@N,60,60") & vbCrLf
        Label &= ("^FD" & Me.AdicionD & "^FS") & vbCrLf
        Label &= ("") & vbCrLf & vbCrLf
        Label &= ("^FO620,250^BY3") & vbCrLf         ' DH D
        Label &= ("^A@N,60,60") & vbCrLf
        Label &= ("^FD" & Me.DH_D & "^FS") & vbCrLf
        Label &= ("") & vbCrLf & vbCrLf
        Label &= ("^FO700,250^BY3") & vbCrLf         ' DV D
        Label &= ("^A@N,60,60") & vbCrLf
        Label &= ("^FD" & Me.DV_D & "^FS") & vbCrLf
        Label &= ("") & vbCrLf & vbCrLf
        Label &= ("^FO775,250^BY3") & vbCrLf         ' DIP
        Label &= ("^A@N,60,60") & vbCrLf
        Label &= ("^FD" & Me.DIP & "^FS") & vbCrLf
        Label &= ("") & vbCrLf & vbCrLf
        Label &= ("^FO52,355^BY3") & vbCrLf          ' Label I
        Label &= ("^A@N,60,60") & vbCrLf
        Label &= ("^FDI^FS") & vbCrLf
        Label &= ("") & vbCrLf & vbCrLf
        Label &= ("^FO120,355^BY3") & vbCrLf          ' Esfera I
        Label &= ("^A@N,60,60") & vbCrLf
        Label &= ("^FD" & Me.EsferaI & "^FS") & vbCrLf
        Label &= ("") & vbCrLf & vbCrLf
        Label &= ("^FO255,355^BY3") & vbCrLf         ' Cilindro I
        Label &= ("^A@N,60,60") & vbCrLf
        Label &= ("^FD" & Me.CilindroI & "^FS") & vbCrLf
        Label &= ("") & vbCrLf & vbCrLf
        Label &= ("^FO410,355^BY3") & vbCrLf         ' Eje I
        Label &= ("^A@N,60,60") & vbCrLf
        Label &= ("^FD" & Me.EjeI & "^FS") & vbCrLf
        Label &= ("") & vbCrLf & vbCrLf
        Label &= ("^FO510,355^BY3") & vbCrLf         ' Adicion I
        Label &= ("^A@N,60,60") & vbCrLf
        Label &= ("^FD" & Me.AdicionI & "^FS") & vbCrLf
        Label &= ("") & vbCrLf & vbCrLf
        Label &= ("^FO620,355^BY3") & vbCrLf         ' DH I
        Label &= ("^A@N,60,60") & vbCrLf
        Label &= ("^FD" & Me.DH_I & "^FS") & vbCrLf
        Label &= ("") & vbCrLf & vbCrLf
        Label &= ("^FO700,355^BY3") & vbCrLf         ' DV I
        Label &= ("^A@N,60,60") & vbCrLf
        Label &= ("^FD" & Me.DV_I & "^FS") & vbCrLf
        Label &= ("") & vbCrLf & vbCrLf
        Label &= ("^FO30,415") & vbCrLf
        Label &= ("^GB730,1,2,^FS") & vbCrLf
        Label &= ("") & vbCrLf & vbCrLf
        Label &= ("") & vbCrLf & vbCrLf
        Label &= ("^BY2,2.0,10") & vbCrLf
        Label &= ("") & vbCrLf & vbCrLf
        Label &= ("^FO37,455^BY3") & vbCrLf          ' Nombre Completo
        Label &= ("^A@N,35,35,E:LTE50846.FNT") & vbCrLf
        Label &= ("^FD" & Me.NombreCompleto & "^FS") & vbCrLf
        Label &= ("") & vbCrLf & vbCrLf
        Label &= ("^FO900,461^BY3") & vbCrLf          ' Label Fecha
        Label &= ("^A0N,30,30") & vbCrLf
        Label &= ("^FDFecha: ^FS") & vbCrLf
        Label &= ("") & vbCrLf & vbCrLf
        Label &= ("^FO990,455^BY3") & vbCrLf          ' Fecha
        Label &= ("^A@N,35,35,E:LTE50846.FNT") & vbCrLf
        Label &= ("^FD" & Me.Fecha & "^FS") & vbCrLf
        Label &= ("") & vbCrLf & vbCrLf
        Label &= ("") & vbCrLf & vbCrLf
        Label &= ("^XZ")
        PrintedOK = True
        Return Label
    End Function


    Sub PrintLabLabel(ByVal SourceFileStr As String)
        Dim MyStream As System.IO.Stream = System.IO.File.Open(SourceFileStr, IO.FileMode.Create, IO.FileAccess.ReadWrite, IO.FileShare.None)

        Dim SWriter As New System.IO.StreamWriter(MyStream)

        SWriter.WriteLine(" -------------------------------------------------------------------------------------------------")
        SWriter.WriteLine(" Archivo de Etiqueta LabRx")
        SWriter.WriteLine(" -------------------------------------------------------------------------------------------------")
        SWriter.WriteLine(" FileName : LabelOne.txt")
        SWriter.WriteLine(" Date/Time : " + DateTime.Now.ToShortDateString)
        SWriter.WriteLine(" Language : ZPL II (Zebra Programming Language V2.0)")
        SWriter.WriteLine("--------------------------------------------------------------------------------------------------")
        SWriter.WriteLine("") : SWriter.WriteLine("")
        SWriter.WriteLine("^XA")                'header
        SWriter.WriteLine("")
        'SWriter.WriteLine("^FO30,25^BY3")       ' Label Vantage
        'SWriter.WriteLine("^AFN,25,13")
        'SWriter.WriteLine("^FDVantage:^FS")
        SWriter.WriteLine("")
        SWriter.WriteLine("^FO30,10")          ' Vantage
        SWriter.WriteLine("^BCN,60,Y,N,N")
        SWriter.WriteLine("^FD" & Me.Ordernum & "^FS")
        SWriter.WriteLine("")
        SWriter.WriteLine("^FO80,110^BY3")       ' Label de Esfera
        SWriter.WriteLine("^AFN,25,10")
        SWriter.WriteLine("^FDEsfera^FS")
        SWriter.WriteLine("")
        SWriter.WriteLine("^FO200,110^BY3")      ' Label de Cilindo
        SWriter.WriteLine("^AFN,25,10")
        SWriter.WriteLine("^FDCilindro^FS")
        SWriter.WriteLine("")
        SWriter.WriteLine("^FO350,110^BY3")      ' Label de Eje
        SWriter.WriteLine("^AFN,25,10")
        SWriter.WriteLine("^FDEje^FS")
        SWriter.WriteLine("")
        SWriter.WriteLine("^FO440,110^BY3")      ' Label de DH
        SWriter.WriteLine("^AFN,25,10")
        SWriter.WriteLine("^FDDH^FS")
        SWriter.WriteLine("")
        SWriter.WriteLine("^FO530,110^BY3")      ' Label de DIP
        SWriter.WriteLine("^AFN,25,10")
        SWriter.WriteLine("^FDDIP^FS")
        SWriter.WriteLine("")
        SWriter.WriteLine("^FO650,35^BY3")          ' Label Armazon
        SWriter.WriteLine("^A0N,13,18")
        SWriter.WriteLine("^FDArmazon:^FS")
        SWriter.WriteLine("")
        SWriter.WriteLine("^FO615,60")          ' Armazon
        SWriter.WriteLine("^A@N,40,50,E:LTE50846.FNT")
        SWriter.WriteLine("^FD" & Me.Armazon & "^FS")
        SWriter.WriteLine("^FO590,150^BY3")      ' Label de Rx
        SWriter.WriteLine("^A@N,40,50,E:LTE50846.FNT")
        SWriter.WriteLine("^FDTz." & Me.RefDigital & "^FS")
        SWriter.WriteLine("")
        SWriter.WriteLine("^FO615,200")          ' Rx
        SWriter.WriteLine("^B2N,50,N,N,N")
        SWriter.WriteLine("^FD" & Me.RefDigital & "^FS")
        SWriter.WriteLine("")
        SWriter.WriteLine("^FO20,139")
        SWriter.WriteLine("^GB450,1,2,^FS")
        SWriter.WriteLine("")
        SWriter.WriteLine("^LRY ^FO20,140")
        SWriter.WriteLine("^GB420,50,61,,0^FS")     '^GB265
        SWriter.WriteLine("^FO30,150^BY3")          ' Label D
        SWriter.WriteLine("^A@N,40,50")
        SWriter.WriteLine("^FDD^FS")
        SWriter.WriteLine("")
        SWriter.WriteLine("^FO80,150^BY3")          ' Esfera D
        SWriter.WriteLine("^A@N,40,40")
        SWriter.WriteLine("^FD" & Me.EsferaD & "^FS")
        SWriter.WriteLine("")
        SWriter.WriteLine("^FO200,150^BY3")         ' Cilindro D
        SWriter.WriteLine("^A@N,40,40")
        SWriter.WriteLine("^FD" & Me.CilindroD & "^FS")
        SWriter.WriteLine("")
        SWriter.WriteLine("^FO340,150^BY3")         ' Eje D
        SWriter.WriteLine("^A@N,40,40")
        SWriter.WriteLine("^FD" & Me.EjeD & "^FS")
        SWriter.WriteLine("")
        SWriter.WriteLine("^FO440,150^BY3")         ' DH D
        SWriter.WriteLine("^A@N,40,40")
        SWriter.WriteLine("^FD" & Me.DH_D & "^FS")
        SWriter.WriteLine("")
        SWriter.WriteLine("^FO530,150^BY3")         ' DIP
        SWriter.WriteLine("^A@N,40,40")
        SWriter.WriteLine("^FD" & Me.DIP & "^FS")
        SWriter.WriteLine("")
        SWriter.WriteLine("^FO35,220^BY3")          ' Label I
        SWriter.WriteLine("^A@N,40,50")
        SWriter.WriteLine("^FDI^FS")
        SWriter.WriteLine("")
        SWriter.WriteLine("^FO80,220^BY3")          ' Esfera I
        SWriter.WriteLine("^A@N,40,40")
        SWriter.WriteLine("^FD" & Me.EsferaI & "^FS")
        SWriter.WriteLine("")
        SWriter.WriteLine("^FO200,220^BY3")         ' Cilindro I
        SWriter.WriteLine("^A@N,40,40")
        SWriter.WriteLine("^FD" & Me.CilindroI & "^FS")
        SWriter.WriteLine("")
        SWriter.WriteLine("^FO340,220^BY3")         ' Eje I
        SWriter.WriteLine("^A@N,40,40")
        SWriter.WriteLine("^FD" & Me.EjeI & "^FS")
        SWriter.WriteLine("")
        SWriter.WriteLine("^FO440,220^BY3")         ' DH I
        SWriter.WriteLine("^A@N,40,40")
        SWriter.WriteLine("^FD" & Me.DH_I & "^FS")
        SWriter.WriteLine("")
        SWriter.WriteLine("^FO20,260")
        SWriter.WriteLine("^GB450,1,2,^FS")
        SWriter.WriteLine("")
        SWriter.WriteLine("")
        SWriter.WriteLine("^BY2,2.0,10")
        SWriter.WriteLine("")
        SWriter.WriteLine("^FO25,280^BY3")          ' Nombre Completo
        SWriter.WriteLine("^A0N,18,22")
        SWriter.WriteLine("^FD" & Me.NombreCompleto & "^FS")
        SWriter.WriteLine("")
        SWriter.WriteLine("^FO400,280^BY3")          ' Grado
        SWriter.WriteLine("^A0N,18,22")
        SWriter.WriteLine("^FDGrado: " & Me.Grado & "^FS")
        SWriter.WriteLine("")
        SWriter.WriteLine("^FO500,280^BY3")          ' Grupo
        SWriter.WriteLine("^A0N,18,22")
        SWriter.WriteLine("^FDGrupo: " & Me.Grupo & "^FS")
        SWriter.WriteLine("")
        SWriter.WriteLine("^FO620,280^BY3")          ' Fecha
        SWriter.WriteLine("^A0N,18,22")
        SWriter.WriteLine("^FDFecha: " & Me.Fecha & "^FS")
        SWriter.WriteLine("")
        SWriter.WriteLine("")
        SWriter.WriteLine("^XZ")
        ' Arbitrary objects can also be written to the file.
        SWriter.Close()
        Try
            Dim Printed As Boolean = False
            Do While Not Printed

                System.IO.File.Copy(SourceFileStr, My.Settings.ZebraPrintPort)  ' Copy source to target.
                Printed = True
            Loop

        Catch ex As Exception
            Throw New Exception("Error al imprimir: " + ex.Message)
        End Try
    End Sub
    Sub PrintMEI_Terminado_V2(ByRef ZPLII As String)

        Dim SZPLII As New System.Text.StringBuilder()

        SZPLII.AppendLine(" -------------------------------------------------------------------------------------------------")
        SZPLII.AppendLine(" Archivo de Etiqueta LabRx")
        SZPLII.AppendLine(" -------------------------------------------------------------------------------------------------")
        SZPLII.AppendLine(" FileName : LabelVantage.txt")
        SZPLII.AppendLine(" Date/Time : " + DateTime.Now.ToShortDateString)
        SZPLII.AppendLine(" Language : ZPL II (Zebra Programming Language V2.0)")
        SZPLII.AppendLine("--------------------------------------------------------------------------------------------------")
        SZPLII.AppendLine("") : SZPLII.AppendLine("")
        SZPLII.AppendLine("^XA")                'header
        SZPLII.AppendLine("")
        'Pedro Farías Lozano Ene 16 2013
        'Cambio solicitado por Sol Gaxiola, eliminar barra bidimensional, 
        'mover un poco a la derecha los códigos de barra para evitar que salgan cortados cuando la etiqueta se desplaza a la derecha 1/16 "
        SZPLII.AppendLine("^BY4,2.0,25")          ' Vantage Barcode
        SZPLII.AppendLine("^FO600,15")
        SZPLII.AppendLine("^BCN,140,Y,N,N")
        SZPLII.AppendLine("^FD" & Me.Ordernum & "^FS")
        SZPLII.AppendLine("")
        SZPLII.AppendLine("^FO20,42^BY3")       ' Texto Rx
        SZPLII.AppendLine("^A0N,40,60")
        SZPLII.AppendLine("^FDRX: " & Me.Rx & "^FS") ' Número Rx
        SZPLII.AppendLine("")
        SZPLII.AppendLine("^FO20,100")          ' Armazon
        SZPLII.AppendLine("^A@N,60,75,E:LTE50846.FNT")
        SZPLII.AppendLine("^FD" & Me.Armazon & "^FS")
        SZPLII.AppendLine("")
        SZPLII.AppendLine("^FO120,200^BY3")      ' Label de Esfera
        SZPLII.AppendLine("^A0N,25,25")
        SZPLII.AppendLine("^FDESFERA^FS")
        SZPLII.AppendLine("")
        SZPLII.AppendLine("^FO300,200^BY3")      ' Label de Cilindo
        SZPLII.AppendLine("^A0N,25,25")
        SZPLII.AppendLine("^FDCILINDRO^FS")
        SZPLII.AppendLine("")
        SZPLII.AppendLine("^FO510,200^BY3")      ' Label de Eje
        SZPLII.AppendLine("^A0N,25,25")
        SZPLII.AppendLine("^FDEJE^FS")
        SZPLII.AppendLine("")
        SZPLII.AppendLine("^FO660,200^BY3")      ' Label de DH
        SZPLII.AppendLine("^A0N,25,25")
        SZPLII.AppendLine("^FDDH^FS")
        SZPLII.AppendLine("")
        SZPLII.AppendLine("^FO780,200^BY3")      ' Label de DIP
        SZPLII.AppendLine("^A0N,25,25")
        SZPLII.AppendLine("^FDDIP^FS")
        SZPLII.AppendLine("")
        SZPLII.AppendLine("^FO862,250^BY3")      ' Número de Trazo
        SZPLII.AppendLine("^A@N,60,75,E:LTE50846.FNT")
        SZPLII.AppendLine("^FDTz " & Me.RefDigital & "^FS")
        SZPLII.AppendLine("")
        SZPLII.AppendLine("^FO872,325")          ' Número de Trazo Código Barras
        SZPLII.AppendLine("^BCN,75,N,N,N")
        SZPLII.AppendLine("^FD" & Me.RefDigital & "^FS")
        SZPLII.AppendLine("")
        SZPLII.AppendLine("^FO30,233")
        SZPLII.AppendLine("^GB675,1,2,^FS")
        SZPLII.AppendLine("")
        SZPLII.AppendLine("^LRY ^FO30,235")
        SZPLII.AppendLine("^GB615,75,90,,0^FS")     '^GB265
        SZPLII.AppendLine("^FO45,250^BY3")          ' Label D
        SZPLII.AppendLine("^A@N,60,75")
        SZPLII.AppendLine("^FDD^FS")
        SZPLII.AppendLine("")
        SZPLII.AppendLine("^FO120,250^BY3")          ' Esfera D
        SZPLII.AppendLine("^A@N,60,60")
        SZPLII.AppendLine("^FD" & Me.EsferaD & "^FS")
        SZPLII.AppendLine("")
        SZPLII.AppendLine("^FO300,250^BY3")         ' Cilindro D
        SZPLII.AppendLine("^A@N,60,60")
        SZPLII.AppendLine("^FD" & Me.CilindroD & "^FS")
        SZPLII.AppendLine("")
        SZPLII.AppendLine("^FO510,250^BY3")         ' Eje D
        SZPLII.AppendLine("^A@N,60,60")
        SZPLII.AppendLine("^FD" & Me.EjeD & "^FS")
        SZPLII.AppendLine("")
        SZPLII.AppendLine("^FO660,250^BY3")         ' DH D
        SZPLII.AppendLine("^A@N,60,60")
        SZPLII.AppendLine("^FD" & Me.DH_D & "^FS")
        SZPLII.AppendLine("")
        SZPLII.AppendLine("^FO775,250^BY3")         ' DIP
        SZPLII.AppendLine("^A@N,60,60")
        SZPLII.AppendLine("^FD" & Me.DIP & "^FS")
        SZPLII.AppendLine("")
        SZPLII.AppendLine("^FO52,355^BY3")          ' Label I
        SZPLII.AppendLine("^A@N,60,60")
        SZPLII.AppendLine("^FDI^FS")
        SZPLII.AppendLine("")
        SZPLII.AppendLine("^FO120,340^BY3")          ' Esfera I
        SZPLII.AppendLine("^A@N,60,60")
        SZPLII.AppendLine("^FD" & Me.EsferaI & "^FS")
        SZPLII.AppendLine("")
        SZPLII.AppendLine("^FO300,355^BY3")         ' Cilindro I
        SZPLII.AppendLine("^A@N,60,60")
        SZPLII.AppendLine("^FD" & Me.CilindroI & "^FS")
        SZPLII.AppendLine("")
        SZPLII.AppendLine("^FO510,354^BY3")         ' Eje I
        SZPLII.AppendLine("^A@N,60,60")
        SZPLII.AppendLine("^FD" & Me.EjeI & "^FS")
        SZPLII.AppendLine("")
        SZPLII.AppendLine("^FO660,355^BY3")         ' DH I
        SZPLII.AppendLine("^A@N,60,60")
        SZPLII.AppendLine("^FD" & Me.DH_I & "^FS")
        SZPLII.AppendLine("")
        SZPLII.AppendLine("^FO30,415")
        SZPLII.AppendLine("^GB675,1,2,^FS")
        SZPLII.AppendLine("")
        SZPLII.AppendLine("")
        SZPLII.AppendLine("^BY2,2.0,10")
        SZPLII.AppendLine("")
        SZPLII.AppendLine("^FO37,455^BY3")          ' Nombre Completo
        SZPLII.AppendLine("^A@N,35,35,E:LTE50846.FNT")
        SZPLII.AppendLine("^FD" & Me.NombreCompleto & "^FS")
        SZPLII.AppendLine("")
        SZPLII.AppendLine("^FO600,461^BY3")          ' Label Grado
        SZPLII.AppendLine("^A0N,27,33")
        SZPLII.AppendLine("^FDGrado:^FS")
        SZPLII.AppendLine("")
        SZPLII.AppendLine("^FO700,455^BY3")          ' Grado
        SZPLII.AppendLine("^A@N,35,35,E:LTE50846.FNT")
        SZPLII.AppendLine("^FD" & Me.Grado & "^FS")
        SZPLII.AppendLine("")
        SZPLII.AppendLine("^FO750,461^BY3")          ' Label Grupo
        SZPLII.AppendLine("^A0N,27,33")
        SZPLII.AppendLine("^FDGrupo:^FS")
        SZPLII.AppendLine("")
        SZPLII.AppendLine("^FO850,455^BY3")          ' Grupo
        SZPLII.AppendLine("^A@N,35,35,E:LTE50846.FNT")
        SZPLII.AppendLine("^FD" & Me.Grupo & "^FS")
        SZPLII.AppendLine("")
        SZPLII.AppendLine("^FO900,461^BY3")          ' Label Fecha
        SZPLII.AppendLine("^A0N,30,30")
        SZPLII.AppendLine("^FDFecha: ^FS")
        SZPLII.AppendLine("")
        SZPLII.AppendLine("^FO990,455^BY3")          ' Fecha
        SZPLII.AppendLine("^A@N,35,35,E:LTE50846.FNT")
        SZPLII.AppendLine("^FD" & Me.Fecha & "^FS")
        SZPLII.AppendLine("")
        SZPLII.AppendLine("")
        SZPLII.AppendLine("^XZ")

        ZPLII = SZPLII.ToString()

    End Sub
    Sub PrintPacienteLabel(ByVal SourceFileStr As String)
        Dim MyStream As System.IO.Stream = System.IO.File.Open(SourceFileStr, IO.FileMode.Create, IO.FileAccess.ReadWrite, IO.FileShare.None)

        Dim SWriter As New System.IO.StreamWriter(MyStream)

        SWriter.WriteLine(" -------------------------------------------------------------------------------------------------")
        SWriter.WriteLine(" Archivo de Etiqueta LabRx")
        SWriter.WriteLine(" -------------------------------------------------------------------------------------------------")
        SWriter.WriteLine(" FileName : LabelOne.txt")
        SWriter.WriteLine(" Date/Time : " + DateTime.Now.ToShortDateString)
        SWriter.WriteLine(" Language : ZPL II (Zebra Programming Language V2.0)")
        SWriter.WriteLine("--------------------------------------------------------------------------------------------------")
        SWriter.WriteLine("") : SWriter.WriteLine("")
        SWriter.WriteLine("^XA") 'header
        SWriter.WriteLine("")
        SWriter.WriteLine("^FO50,30^BY3")       ' Nombre Completo
        SWriter.WriteLine("^A0N,40,50")
        SWriter.WriteLine("^FD" & Me.NombreCompleto & "^FS")
        SWriter.WriteLine("")
        SWriter.WriteLine("^FO520,35^BY3")      ' Grado
        SWriter.WriteLine("^A0N,35,35")
        SWriter.WriteLine("^FDGrado: " & Me.Grado & "^FS")
        SWriter.WriteLine("")
        SWriter.WriteLine("^FO670,35^BY3")          ' Grupo
        SWriter.WriteLine("^A0N,35,35")
        SWriter.WriteLine("^FDGrupo: " & Me.Grupo & "^FS")
        SWriter.WriteLine("")
        SWriter.WriteLine("^FO100,80^BY3")      ' Label Esfera
        SWriter.WriteLine("^ABN,5,2")
        SWriter.WriteLine("^FDEsfera^FS")
        SWriter.WriteLine("")
        SWriter.WriteLine("^FO180,80^BY3")      ' Label Cilindro
        SWriter.WriteLine("^ABN,5,2")
        SWriter.WriteLine("^FDCilindro^FS")
        SWriter.WriteLine("")
        SWriter.WriteLine("^FO280,80^BY3")      ' Label Eje
        SWriter.WriteLine("^ABN,5,2")
        SWriter.WriteLine("^FDEje^FS")
        SWriter.WriteLine("")
        SWriter.WriteLine("^FO380,80^BY3")      ' Label de DIP
        SWriter.WriteLine("^ABN,5,2")
        SWriter.WriteLine("^FDDIP^FS")
        SWriter.WriteLine("")
        SWriter.WriteLine("^FO550,80^BY3")      ' Label de Rx
        SWriter.WriteLine("^A@N,40,50,E:LTE50846.FNT")
        SWriter.WriteLine("^FDRx. " & Me.PO & "^FS")
        SWriter.WriteLine("")
        SWriter.WriteLine("^FO60,115^BY3")          ' Label D
        SWriter.WriteLine("^A@N,30,20")
        SWriter.WriteLine("^FDD^FS")
        SWriter.WriteLine("")
        SWriter.WriteLine("^FO100,115^BY3")          ' Esfera D
        SWriter.WriteLine("^A@N,30,20")
        SWriter.WriteLine("^FD" & Me.EsferaD & "^FS")
        SWriter.WriteLine("")
        SWriter.WriteLine("^FO180,115^BY3")         ' Cilindro D
        SWriter.WriteLine("^A@N,30,20")
        SWriter.WriteLine("^FD" & Me.CilindroD & "^FS")
        SWriter.WriteLine("")
        SWriter.WriteLine("^FO280,115^BY3")         ' Eje D
        SWriter.WriteLine("^A@N,30,20")
        SWriter.WriteLine("^FD" & Me.EjeD & "^FS")
        SWriter.WriteLine("")
        SWriter.WriteLine("^FO380,115^BY3")         ' DIP
        SWriter.WriteLine("^A@N,30,20")
        SWriter.WriteLine("^FD" & Me.DIP & "^FS")
        SWriter.WriteLine("")
        SWriter.WriteLine("^FO50,145")
        SWriter.WriteLine("^GB300,1,2,^FS")         '^GB265
        SWriter.WriteLine("^FO60,160^BY3")          ' Label I
        SWriter.WriteLine("^A@N,30,20")
        SWriter.WriteLine("^FDI^FS")
        SWriter.WriteLine("")
        SWriter.WriteLine("^FO100,160^BY3")          ' Esfera I
        SWriter.WriteLine("^A@N,30,20")
        SWriter.WriteLine("^FD" & Me.EsferaI & "^FS")
        SWriter.WriteLine("")
        SWriter.WriteLine("^FO180,160^BY3")         ' Cilindro I
        SWriter.WriteLine("^A@N,30,20")
        SWriter.WriteLine("^FD" & Me.CilindroI & "^FS")
        SWriter.WriteLine("")
        SWriter.WriteLine("^FO280,160^BY3")         ' Eje I
        SWriter.WriteLine("^A@N,30,20")
        SWriter.WriteLine("^FD" & Me.EjeI & "^FS")
        SWriter.WriteLine("")
        SWriter.WriteLine("^FO50,190")
        SWriter.WriteLine("^GB300,1,2,^FS")
        SWriter.WriteLine("")
        SWriter.WriteLine("")
        SWriter.WriteLine("^BY2,2.0,10")
        SWriter.WriteLine("")
        SWriter.WriteLine("^FO600,150^BY3")          ' Label Armazon
        SWriter.WriteLine("^A0N,13,18")
        SWriter.WriteLine("^FDArmazon:^FS")
        SWriter.WriteLine("")
        SWriter.WriteLine("^FO680,140")             ' Armazon
        SWriter.WriteLine("^A@N,30,30,E:LTE50846.FNT")
        SWriter.WriteLine("^FD" & Me.Armazon & "^FS")
        SWriter.WriteLine("")
        SWriter.WriteLine("^FO630,190^BY3")             ' Optometrista
        SWriter.WriteLine("^A0N,13,18")
        SWriter.WriteLine("^FDOpto: " & Me.ID_Opto & "^FS")
        SWriter.WriteLine("")
        SWriter.WriteLine("^FO060,235^BY3")             ' Label Escuela
        SWriter.WriteLine("^A0N,13,18")
        SWriter.WriteLine("^FDEscuela: ^FS")
        SWriter.WriteLine("")
        SWriter.WriteLine("^FO140,230^BY3")             ' Escuela
        SWriter.WriteLine("^A0N,22,24")
        SWriter.WriteLine("^FD" & Me.Escuela & "^FS")
        SWriter.WriteLine("")
        SWriter.WriteLine("^FO380,235^BY3")             ' Label CCT
        SWriter.WriteLine("^A0N,13,18")
        SWriter.WriteLine("^FDCCT: ^FS")
        SWriter.WriteLine("")
        SWriter.WriteLine("^FO420,230^BY3")             ' CCT
        SWriter.WriteLine("^A0N,20,22")
        SWriter.WriteLine("^FD" & Me.ID_Escuela & "^FS")
        SWriter.WriteLine("")
        SWriter.WriteLine("^FO060,275^BY3")             ' Label Municipio
        SWriter.WriteLine("^A0N,13,18")
        SWriter.WriteLine("^FDMunicipio:  ^FS")
        SWriter.WriteLine("")
        SWriter.WriteLine("^FO160,270^BY3")             ' Municipio
        SWriter.WriteLine("^A0N,22,24")
        SWriter.WriteLine("^FD" & Me.Municipio & "^FS")
        SWriter.WriteLine("")
        SWriter.WriteLine("^FO290,275^BY3")             ' Label Estado
        SWriter.WriteLine("^A0N,13,18")
        SWriter.WriteLine("^FDEstado:^FS")
        SWriter.WriteLine("")
        SWriter.WriteLine("^FO360,270^BY3")             ' Estado
        SWriter.WriteLine("^A0N,22,24")
        SWriter.WriteLine("^FD" & Me.Estado & "^FS")
        SWriter.WriteLine("")
        SWriter.WriteLine("^FO590,230^FWI^XGE:AUGENVER.GRF,1,1^FS")     ' Logo AUGEN
        SWriter.WriteLine("^XZ")
        ' Arbitrary objects can also be written to the file.
        SWriter.Close()
        Try
            Dim Printed As Boolean = False
            Do While Not Printed
                System.IO.File.Copy(SourceFileStr, My.Settings.ZebraPrintPort)  ' Copy source to target.
                Printed = True                
            Loop

        Catch ex As Exception
            Throw New Exception("Error al imprimir: " + ex.Message)
        End Try
    End Sub
    Sub PrintOrderLabel(ByVal SourceFileStr As String)
        Dim MyStream As System.IO.Stream = System.IO.File.Open(SourceFileStr, IO.FileMode.Create, IO.FileAccess.ReadWrite, IO.FileShare.None)

        Dim SWriter As New System.IO.StreamWriter(MyStream)

        SWriter.WriteLine(" -------------------------------------------------------------------------------------------------")
        SWriter.WriteLine(" Archivo de Etiqueta LabRx")
        SWriter.WriteLine(" -------------------------------------------------------------------------------------------------")
        SWriter.WriteLine(" FileName : LabelOne.txt")
        SWriter.WriteLine(" Date/Time : " + DateTime.Now.ToShortDateString)
        SWriter.WriteLine(" Language : ZPL II (Zebra Programming Language V2.0)")
        SWriter.WriteLine("--------------------------------------------------------------------------------------------------")
        SWriter.WriteLine("") : SWriter.WriteLine("")
        SWriter.WriteLine("^XA")                'header
        SWriter.WriteLine("")

        SWriter.WriteLine("^FO80,110^BY3")       ' Label de Esfera
        SWriter.WriteLine("^AFN,25,13")
        SWriter.WriteLine("^FDEsfera^FS")
        SWriter.WriteLine("")
        SWriter.WriteLine("^FO220,110^BY3")      ' Label de Cilindo
        SWriter.WriteLine("^AFN,25,13")
        SWriter.WriteLine("^FDCilindro^FS")
        SWriter.WriteLine("")
        SWriter.WriteLine("^FO390,110^BY3")      ' Label de Eje
        SWriter.WriteLine("^AFN,25,13")
        SWriter.WriteLine("^FDEje^FS")
        SWriter.WriteLine("")
        SWriter.WriteLine("^FO500,110^BY3")      ' Label de DIP
        SWriter.WriteLine("^AFN,25,13")
        SWriter.WriteLine("^FDDIP^FS")
        SWriter.WriteLine("")
        SWriter.WriteLine("^FO590,20^BY3")      ' Label de Rx
        SWriter.WriteLine("^A@N,40,50,E:LTE50846.FNT")
        SWriter.WriteLine("^FDRx." & Me.PO & "^FS")
        SWriter.WriteLine("")
        SWriter.WriteLine("^FO590,60")          ' Rx
        SWriter.WriteLine("^B2N,50,N,N,N")
        SWriter.WriteLine("^FD" & Me.PO & "^FS")
        SWriter.WriteLine("")
        SWriter.WriteLine("^LRY ^FO20,140")
        SWriter.WriteLine("^GB450,50,61,,0^FS")     '^GB265
        SWriter.WriteLine("^FO30,150^BY3")          ' Label D
        SWriter.WriteLine("^A@N,40,50")
        SWriter.WriteLine("^FDD^FS")
        SWriter.WriteLine("")
        SWriter.WriteLine("^FO80,150^BY3")          ' Esfera D
        SWriter.WriteLine("^A@N,40,50")
        SWriter.WriteLine("^FD" & Me.EsferaD & "^FS")
        SWriter.WriteLine("")
        SWriter.WriteLine("^FO220,150^BY3")         ' Cilindro D
        SWriter.WriteLine("^A@N,40,50")
        SWriter.WriteLine("^FD" & Me.CilindroD & "^FS")
        SWriter.WriteLine("")
        SWriter.WriteLine("^FO380,150^BY3")         ' Eje D
        SWriter.WriteLine("^A@N,40,50")
        SWriter.WriteLine("^FD" & Me.EjeD & "^FS")
        SWriter.WriteLine("")
        SWriter.WriteLine("^FO500,150^BY3")         ' DIP
        SWriter.WriteLine("^A@N,40,50")
        SWriter.WriteLine("^FD" & Me.DIP & "^FS")
        SWriter.WriteLine("")
        SWriter.WriteLine("^FO35,220^BY3")          ' Label I
        SWriter.WriteLine("^A@N,40,50")
        SWriter.WriteLine("^FDI^FS")
        SWriter.WriteLine("")
        SWriter.WriteLine("^FO80,220^BY3")          ' Esfera I
        SWriter.WriteLine("^A@N,40,50")
        SWriter.WriteLine("^FD" & Me.EsferaI & "^FS")
        SWriter.WriteLine("")
        SWriter.WriteLine("^FO220,220^BY3")         ' Cilindro I
        SWriter.WriteLine("^A@N,40,50")
        SWriter.WriteLine("^FD" & Me.CilindroI & "^FS")
        SWriter.WriteLine("")
        SWriter.WriteLine("^FO380,220^BY3")         ' Eje I
        SWriter.WriteLine("^A@N,40,50")
        SWriter.WriteLine("^FD" & Me.EjeI & "^FS")
        SWriter.WriteLine("")
        SWriter.WriteLine("^FO20,260")
        SWriter.WriteLine("^GB450,1,2,^FS")
        SWriter.WriteLine("")
        SWriter.WriteLine("")
        SWriter.WriteLine("^BY2,2.0,10")
        SWriter.WriteLine("")
        SWriter.WriteLine("^FO25,280^BY3")          ' Nombre Completo
        SWriter.WriteLine("^A0N,18,22")
        SWriter.WriteLine("^FD" & Me.NombreCompleto & "^FS")
        SWriter.WriteLine("")
        SWriter.WriteLine("^FO400,280^BY3")          ' Grado
        SWriter.WriteLine("^A0N,18,22")
        SWriter.WriteLine("^FDGrado: " & Me.Grado & "^FS")
        SWriter.WriteLine("")
        SWriter.WriteLine("^FO500,280^BY3")          ' Grupo
        SWriter.WriteLine("^A0N,18,22")
        SWriter.WriteLine("^FDGrupo: " & Me.Grupo & "^FS")
        SWriter.WriteLine("")
        SWriter.WriteLine("^FO620,280^BY3")          ' Fecha
        SWriter.WriteLine("^A0N,18,22")
        SWriter.WriteLine("^FDFecha: " & Me.Fecha & "^FS")
        SWriter.WriteLine("")
        SWriter.WriteLine("^FO650,150^BY3")          ' Label Armazon
        SWriter.WriteLine("^A0N,13,18")
        SWriter.WriteLine("^FDArmazon:^FS")
        SWriter.WriteLine("")
        SWriter.WriteLine("^FO620,170")          ' Armazon
        SWriter.WriteLine("^A@N,40,50,E:LTE50846.FNT")
        SWriter.WriteLine("^FD" & Me.Armazon & "^FS")
        SWriter.WriteLine("")
        SWriter.WriteLine("")
        SWriter.WriteLine("^XZ")
        ' Arbitrary objects can also be written to the file.
        SWriter.Close()
        Dim Printed As Boolean = False
        Do While Not Printed
            Try
                System.IO.File.Copy(SourceFileStr, My.Settings.ZebraPrintPort)  ' Copy source to target.
                Printed = True
            Catch ex As Exception
                Throw New Exception("Error al imprimir: " + ex.Message)
            End Try
        Loop

    End Sub

    Sub Print(ByVal SourceFileStr As String)
        PrintLabLabel(SourceFileStr)
        PrintPacienteLabel(SourceFileStr)
    End Sub

    'ADD # PEDRO FARIAS LOZANO # 25 FEBRERO 2013 # Imprime la etiqueta de semiterminado
    Sub PrintMEILabel_SemiTerminado(ByRef etiqueta1 As String)
        ' Utiliza el puerto definido en los ajustes

        ' Falta por hacer : usar comando ~TA para ajustar la posición de corte de la etiqueta -- debe tener un poco más
        ' ~TAnnn   donde -120<nnn<120   y debe ser un número de 3 dígitos Ej. ~TA090
        etiqueta1 = ""
        etiqueta1 = etiqueta1 + " -------------------------------------------------------------------------------------------------" + vbCrLf
        etiqueta1 = etiqueta1 + " Archivo de Etiqueta LabRx SemiTerminado PFL 2/18/2013"
        etiqueta1 = etiqueta1 + " -------------------------------------------------------------------------------------------------" + vbCrLf
        etiqueta1 = etiqueta1 + " FileName : Semiterminado.txt" + vbCrLf
        etiqueta1 = etiqueta1 + " Date/Time : " & DateTime.Now.ToShortDateString & vbCrLf
        etiqueta1 = etiqueta1 + " Language : ZPL II (Zebra Programming Language V2.0)" & vbCrLf
        etiqueta1 = etiqueta1 + "--------------------------------------------------------------------------------------------------" + vbCrLf
        etiqueta1 += "^XA"
        etiqueta1 += "^FO380,20"
        etiqueta1 += "^BY3,2.0,15"
        etiqueta1 += "^BCN,80,Y,N,N"
        etiqueta1 += "^FD" & Me.Ordernum & "^FS"       'Order Num Vantage
        etiqueta1 &= vbCrLf
        etiqueta1 += "^FO20,10"
        etiqueta1 += "^A0N,32,32"
        etiqueta1 += "^FDRx." & Me.RxNumLocal & " FF^FS"

        '#ADD#JGARIBALDI 25 DE JULIO# Instruciones que ubican el BIN Al Lado de Rx 
        etiqueta1 &= vbCrLf
        etiqueta1 += "^FO20,45"
        etiqueta1 += "^A0N,80,80"
        etiqueta1 += "^FDBIN: " & Me.WHBIN_Bin & " ^FS"

        'etiqueta1 &= vbCrLf
        'etiqueta1 += "^FO20,45"
        'etiqueta1 += "^A0N,18,18"
        'etiqueta1 += "^FDArmazon:^FS"
        'etiqueta1 &= vbCrLf
        'etiqueta1 += "^FO20,70"
        'etiqueta1 += "^A0N,48,48"
        'etiqueta1 += "^FD" & Me.Armazon & "^FS"
        etiqueta1 &= vbCrLf
        etiqueta1 += "^FO60,140"
        etiqueta1 += "^AFN,18,10"
        etiqueta1 += "^FDBASE^FS"
        etiqueta1 += vbCrLf
        etiqueta1 += "^FO180,140"
        etiqueta1 += "^AFN,18,10"
        etiqueta1 += "^FDANILLO^FS"
        etiqueta1 += vbCrLf
        etiqueta1 += "^FO340,140"
        etiqueta1 += "^AFN,18,10"
        etiqueta1 += "^FDBLOQUE^FS"
        etiqueta1 += vbCrLf
        etiqueta1 += "^FO500,140"
        etiqueta1 += "^AFN,18,10"
        etiqueta1 += "^FDMOLDE^FS"
        etiqueta1 += vbCrLf

        etiqueta1 += "^FO640,140"
        etiqueta1 += "^AFN,18,10"
        etiqueta1 += "^FDGR.CNT.^FS"
        etiqueta1 += vbCrLf
        etiqueta1 += "^FO750,140"
        etiqueta1 += "^AFN,18,10"
        etiqueta1 += "^FDDH^FS"
        etiqueta1 += vbCrLf
        etiqueta1 += "^FO10,180"
        etiqueta1 += "^LRY^GB770,60,60^FS"
        etiqueta1 += vbCrLf

        etiqueta1 += "^FO15,190"
        etiqueta1 += "^A0N,48,48"
        etiqueta1 += "^FDD^FS"
        etiqueta1 += vbCrLf

        etiqueta1 += "^FO60,190"
        etiqueta1 += "^A0N,48,48"
        etiqueta1 += "^FD" & Me.BaseD & "^FS"
        etiqueta1 += vbCrLf

        etiqueta1 += "^FO180,190"
        etiqueta1 += "^A0N,48,32"
        etiqueta1 += "^FD" & Me.AnilloD & "^FS"
        etiqueta1 += vbCrLf

        etiqueta1 += "^FO310,190"
        etiqueta1 += "^A0N,48,32"
        etiqueta1 += "^FD" & Me.BloqueD & "^FS"
        etiqueta1 += vbCrLf

        etiqueta1 += "^FO500,190"
        etiqueta1 += "^A0N,48,32"
        etiqueta1 += "^FD" & Me.MoldeD & "^FS"
        etiqueta1 += vbCrLf

        etiqueta1 += "^FO640,190"
        etiqueta1 += "^A0N,48,32"
        etiqueta1 += "^FD" & Me.GrosorCentroD & "^FS"
        etiqueta1 += vbCrLf

        etiqueta1 += "^FO720,190"
        etiqueta1 += "^A0N,48,32"
        'etiqueta1 += "^FD" & String.Format("{0,-3}", Convert.ToInt16(DH_D)) & "^FS"
        etiqueta1 += "^FD" & DH_D & "^FS"
        etiqueta1 += vbCrLf

        etiqueta1 += "^LRN"
        etiqueta1 += vbCrLf

        etiqueta1 += "^FO15,250"
        etiqueta1 += "^A0N,48,48"
        etiqueta1 += "^FDI^FS"
        etiqueta1 += vbCrLf

        etiqueta1 += "^FO60,250"
        etiqueta1 += "^A0N,48,48"
        etiqueta1 += "^FD" & Me.BaseI & "^FS"
        etiqueta1 += vbCrLf

        etiqueta1 += "^FO180,250"
        etiqueta1 += "^A0N,48,32"
        etiqueta1 += "^FD" & Me.AnilloI & "^FS"
        etiqueta1 += vbCrLf

        etiqueta1 += "^FO310,250"
        etiqueta1 += "^A0N,48,32"
        etiqueta1 += "^FD" & Me.BloqueI & "^FS"
        etiqueta1 += vbCrLf

        etiqueta1 += "^FO500,250"
        etiqueta1 += "^A0N,48,32"
        etiqueta1 += "^FD" & Me.MoldeI & "^FS"
        etiqueta1 += vbCrLf

        etiqueta1 += "^FO640,250"
        etiqueta1 += "^A0N,48,32"
        etiqueta1 += "^FD" & Me.GrosorCentroI & "^FS"
        etiqueta1 += vbCrLf

        etiqueta1 += "^FO720,250"
        etiqueta1 += "^A0N,48,48"
        'etiqueta1 += "^FD" & String.Format("{0,-3}", Convert.ToInt16(DH_I)) & "^FS"
        etiqueta1 += "^FD" & DH_I & "^FS"
        etiqueta1 += vbCrLf

        etiqueta1 += "^FO20,320"
        etiqueta1 += "^AFN,25,10"
        etiqueta1 += "^FD" & Date.Now.ToShortDateString & " " & Date.Now.ToShortTimeString & "^FS"
        etiqueta1 += vbCrLf

        etiqueta1 += "^FO350,320"
        etiqueta1 += "^AFN,25,10"
        etiqueta1 += "^FD" & Me.RefDigital & "^FS"
        etiqueta1 += vbCrLf

        etiqueta1 += "^FO460,300"
        etiqueta1 += "^BY3,2.0,10"
        etiqueta1 += "^BCN,46,N,N,N"
        etiqueta1 += "^FD" & Me.RefDigital & "^FS"       'Rx Trazo para el Generador - Ref. Digital (a) Trazo
        etiqueta1 &= vbCrLf


        '        etiqueta1 += "^FO626,314"
        '        etiqueta1 += "^A0N,36,36"
        '        etiqueta1 += "^FD" & Me.RxNumLocal & "^FS"
        '        etiqueta1 += vbCrLf

        ' etiqueta1 += "^FO780,10"
        ' etiqueta1 += "^A2B,24,"
        ' etiqueta1 += "^FDF.F.^FS"
        ' etiqueta1 += vbCrLf

        etiqueta1 += "^XZ"
        etiqueta1 += vbCrLf

    End Sub


    ' Esta rutina decide a dónde se manda la etiqueta según el archivo de configuración 
    ' puede ser a una impresora por omisión de windows o la especificada en el archivo de configuración
    ' # Pedro Farías Lozano # 7/17/2013 #
    ' # Ahora se imprime mediante Raw Data a una impresora de Windows especificada en el archivo de .config o la default del sistema
    ' 
    Public Sub Manda_a_Impresora(ByRef ZPLII As String, ByRef SourceFileStr As String)
        Dim MyStream As System.IO.Stream = System.IO.File.Open(SourceFileStr, IO.FileMode.Create, IO.FileAccess.ReadWrite, IO.FileShare.None)
        Try
            ' Arbitrary objects can also be written to the file.
            ' Guardamos en un archivo la última etiqueta impresa, para lo que se ocupe, como reimprimir
            Dim SWriter As New System.IO.StreamWriter(MyStream)
            SWriter.Write(ZPLII)
            SWriter.Close()
            ' Pedro Farías Lozano

            ' # Pedro Farías Lozano # 7/17/2013 #
            ' # Ahora se imprime mediante Raw Data a una impresora de Windows especificada en el archivo de .config o la default del sistema

            ' Obtiene la impresora por default del sistema windows
            Dim settings As PrinterSettings = New PrinterSettings()

            ' Revisamos si tenemos una impresora válida en el archivo de configuración
            ' Si ZebraPrintPort especifica un puerto LPT<n> entonces usamos DirectPrint para copiar el stream al dispositivo LPT<n>
            If My.Settings.ZebraPrintPort.ToUpper.StartsWith("LPT") Then
                Direct_LPT_Print(ZPLII, SourceFileStr)
            Else
                If My.Settings.ZebraPrintPort.Length > 3 Then
                    ' Usamos la impresora de la configuración
                    settings.PrinterName = My.Settings.ZebraPrintPort
                End If
                ' Usamos una impresora de windows modo RAW
                RawPrinterHelper.SendStringToPrinter(settings.PrinterName, ZPLII)
            End If

        Catch ex As Exception
            Throw New Exception("Error al imprimir: " + ex.Message)
        End Try

    End Sub

 
    Public Sub Direct_LPT_Print(ByVal ZPLII As String, ByVal TempFile As String)
        'Dim printer As FileStream
        'Dim stwriter As StreamWriter
        Dim Printed As Boolean = False

        Try
            'Dim strbytes As Byte() = New UTF8Encoding(True).GetBytes(ZPLII)

            Do While Not Printed
                Try
                    'System.IO.File.WriteAllText("zplII.txt", ZPLII)
                    'System.IO.File.Copy("zplII.txt", My.Settings.ZebraPrintPort.ToLower)
                    System.IO.File.Copy(TempFile, My.Settings.ZebraPrintPort.ToLower)
                    Printed = True

                Catch Ex As Exception
                    If MsgBox("La impresora no está disponible." + vbCrLf + Ex.Message + vbCrLf + "Por favor, verifique que esté bien conectada y encendida." + vbCrLf + "¿Desea mandarla imprimir de nuevo?", MsgBoxStyle.YesNo + MsgBoxStyle.Exclamation) = MsgBoxResult.No Then
                        Printed = True
                    End If

                End Try
            Loop


        Catch ex As Exception
            Throw New Exception("Error al imprimir: [Direct_LPT_Print] " + ex.Message)
        End Try

    End Sub

End Class
