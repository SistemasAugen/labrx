Imports System.Data.SqlClient
Imports System.Data

Public Class AperturaRx
    Private OrderNum As Integer
    Private CustNum As Integer
    Private Plant As Integer
    Private SqlStrConn As String
    Private IsLiverpool, IsIPN As Boolean
    Protected Friend PlantTaxCode As String

    Private InvSubTot As Double = 0
    Private IsGarantia As Boolean = False
    Private IsGratis As Boolean = False

    Public VoidOrder As Boolean
    Public RxTotalNoMisc As Double = 0
    Protected Friend TaxPercent As Single = 0
    Private MyRemisionXML As String = My.Application.Info.DirectoryPath & "\Remision.xml"
    Private dsReporte As New DataSet



    Private Sub InitValues()
        LViewLines.Clear()
        LViewRX.Clear()
    End Sub

    Public Sub New(ByVal myPlant As Integer, ByVal OrdenCreada As Integer)
        Plant = myPlant
        SqlStrConn = "user ID=" & My.Settings.DBUser & ";password =" & My.Settings.DBPassword & ";database=" & My.Settings.LocalDBName & ";server=" & My.Settings.LocalServer & ";Connect Timeout=11"
        PlantTaxCode = GetTaxCode(myPlant)
        TaxPercent = Convert.ToSingle(PlantTaxCode.Substring(PlantTaxCode.IndexOf("IVA") + 3))

        InitializeComponent()
        OrderNum = OrdenCreada
        TextVantage.Text = OrderNum.ToString
        lblIVA.Text = PlantTaxCode + "%"
        Carga_Orden()
        'Calcula los totales de la orden
        Try
            Dim objMC As MiscCharges
            objMC = New MiscCharges(OrderNum, TextRX.Text, RxTotalNoMisc, Environment.UserName)

        Catch ex As Exception

        End Try

    End Sub

    Private Sub CheckOrderStatus(ByVal ordernum As Integer)
        Dim Failed As Boolean = False
        Dim FailedMessage As String = ""
        Dim t As New SqlDB(My.Settings.ERPMasterConnection)

        Try
            Try
                t.OpenConn()
                Dim SqlStr As String = "select openorder from orderhed with(nolock) where ordernum = " & ordernum
                Dim ds As New DataSet
                ds = t.SQLDS(SqlStr, "t1")
                If ds.Tables("t1").Rows.Count = 0 Then
                    Throw New Exception("Rx Inexistente")
                ElseIf ds.Tables("t1").Rows(0).Item("openorder") = 0 Then
                    Throw New Exception("Rx ya Cerrada")
                End If
            Catch ex As SqlClient.SqlException
                Failed = True
                FailedMessage = ex.Message
            Finally
                t.CloseConn()
            End Try

        Catch ex As Exception
            Failed = True
            FailedMessage = ex.Message
        Finally
            If Failed Then
                Throw New Exception("Error al buscar Rx" & vbCrLf & FailedMessage)
            End If
        End Try
    End Sub


    Private Function GetPriceListDesc(ByVal custnum As Integer) As DataSet
        Dim Failed As Boolean = False
        Dim FailedMessage As String = ""
        Dim t As New SqlDB(SqlStrConn)
        Dim ds As New DataSet

        't.SQLConn = New SqlConnection(SqlStrConn)
        Try
            Try
                t.OpenConn()
                Dim SqlStr As String = "select PL.listdescription from pricelst PL inner join customerpricelst CPL on PL.listcode = CPL.listcode AND PL.enddate>getdate()" & _
                " WHERE CPL.custnum = " & custnum


                ds = t.SQLDS(SqlStr, "Listas")
                If ds.Tables(0).Rows.Count = 0 Then
                    Throw New Exception("Cliente sin lista asignada")
                End If
            Catch ex As SqlClient.SqlException
                Failed = True
                FailedMessage = ex.Message
            Finally
                t.CloseConn()

            End Try

        Catch ex As Exception
            Failed = True
            FailedMessage = ex.Message
        Finally
            If Failed Then
                Throw New Exception("Error al buscar Rx" & vbCrLf & FailedMessage)
            End If
        End Try
        Return ds
    End Function

    Public Function MontoStr(ByVal numero As String)
        Dim b, paso
        Dim expresion As String = ""
        Dim entero As String = ""
        Dim deci As String = ""
        Dim flag As String = ""


        flag = "N"
        For paso = 1 To Len(numero)
            If Mid(numero, paso, 1) = "." Then
                flag = "S"
            Else
                If flag = "N" Then
                    entero = entero + Mid(numero, paso, 1) 'Extae la parte entera del numero
                Else
                    deci = deci + Mid(numero, paso, 1) 'Extrae la parte decimal del numero
                End If
            End If
        Next

        If Len(deci) = 1 Then
            deci = deci & "0"
        End If

        flag = "N"
        If Int(numero) >= -999999999 And Int(numero) <= 999999999 Then 'si el numero esta dentro de 0 a 999.999.999
            For paso = Len(entero) To 1 Step -1
                b = Len(entero) - (paso - 1)
                Select Case paso
                    Case 3, 6, 9
                        Select Case Mid(entero, b, 1)
                            Case "1"
                                If Mid(entero, b + 1, 1) = "0" And Mid(entero, b + 2, 1) = "0" Then
                                    expresion = expresion & "cien "
                                Else
                                    expresion = expresion & "ciento "
                                End If
                            Case "2"
                                expresion = expresion & "doscientos "
                            Case "3"
                                expresion = expresion & "trescientos "
                            Case "4"
                                expresion = expresion & "cuatrocientos "
                            Case "5"
                                expresion = expresion & "quinientos "
                            Case "6"
                                expresion = expresion & "seiscientos "
                            Case "7"
                                expresion = expresion & "setecientos "
                            Case "8"
                                expresion = expresion & "ochocientos "
                            Case "9"
                                expresion = expresion & "novecientos "
                        End Select

                    Case 2, 5, 8
                        Select Case Mid(entero, b, 1)
                            Case "1"
                                If Mid(entero, b + 1, 1) = "0" Then
                                    flag = "S"
                                    expresion = expresion & "diez "
                                End If
                                If Mid(entero, b + 1, 1) = "1" Then
                                    flag = "S"
                                    expresion = expresion & "once "
                                End If
                                If Mid(entero, b + 1, 1) = "2" Then
                                    flag = "S"
                                    expresion = expresion & "doce "
                                End If
                                If Mid(entero, b + 1, 1) = "3" Then
                                    flag = "S"
                                    expresion = expresion & "trece "
                                End If
                                If Mid(entero, b + 1, 1) = "4" Then
                                    flag = "S"
                                    expresion = expresion & "catorce "
                                End If
                                If Mid(entero, b + 1, 1) = "5" Then
                                    flag = "S"
                                    expresion = expresion & "quince "
                                End If
                                If Mid(entero, b + 1, 1) > "5" Then
                                    flag = "N"
                                    expresion = expresion & "dieci"
                                End If

                            Case "2"
                                If Mid(entero, b + 1, 1) = "0" Then
                                    expresion = expresion & "veinte "
                                    flag = "S"
                                Else
                                    expresion = expresion & "veinti"
                                    flag = "N"
                                End If

                            Case "3"
                                If Mid(entero, b + 1, 1) = "0" Then
                                    expresion = expresion & "treinta "
                                    flag = "S"
                                Else
                                    expresion = expresion & "treinta y "
                                    flag = "N"
                                End If

                            Case "4"
                                If Mid(entero, b + 1, 1) = "0" Then
                                    expresion = expresion & "cuarenta "
                                    flag = "S"
                                Else
                                    expresion = expresion & "cuarenta y "
                                    flag = "N"
                                End If

                            Case "5"
                                If Mid(entero, b + 1, 1) = "0" Then
                                    expresion = expresion & "cincuenta "
                                    flag = "S"
                                Else
                                    expresion = expresion & "cincuenta y "
                                    flag = "N"
                                End If

                            Case "6"
                                If Mid(entero, b + 1, 1) = "0" Then
                                    expresion = expresion & "sesenta "
                                    flag = "S"
                                Else
                                    expresion = expresion & "sesenta y "
                                    flag = "N"
                                End If

                            Case "7"
                                If Mid(entero, b + 1, 1) = "0" Then
                                    expresion = expresion & "setenta "
                                    flag = "S"
                                Else
                                    expresion = expresion & "setenta y "
                                    flag = "N"
                                End If

                            Case "8"
                                If Mid(entero, b + 1, 1) = "0" Then
                                    expresion = expresion & "ochenta "
                                    flag = "S"
                                Else
                                    expresion = expresion & "ochenta y "
                                    flag = "N"
                                End If

                            Case "9"
                                If Mid(entero, b + 1, 1) = "0" Then
                                    expresion = expresion & "noventa "
                                    flag = "S"
                                Else
                                    expresion = expresion & "noventa y "
                                    flag = "N"
                                End If
                        End Select

                    Case 1, 4, 7
                        Select Case Mid(entero, b, 1)
                            Case "1"
                                If flag = "N" Then
                                    If paso = 1 Then
                                        expresion = expresion & "uno "
                                    Else
                                        expresion = expresion & "un "
                                    End If
                                End If
                            Case "2"
                                If flag = "N" Then
                                    expresion = expresion & "dos "
                                End If
                            Case "3"
                                If flag = "N" Then
                                    expresion = expresion & "tres "
                                End If
                            Case "4"
                                If flag = "N" Then
                                    expresion = expresion & "cuatro "
                                End If
                            Case "5"
                                If flag = "N" Then
                                    expresion = expresion & "cinco "
                                End If
                            Case "6"
                                If flag = "N" Then
                                    expresion = expresion & "seis "
                                End If
                            Case "7"
                                If flag = "N" Then
                                    expresion = expresion & "siete "
                                End If
                            Case "8"
                                If flag = "N" Then
                                    expresion = expresion & "ocho "

                                End If
                            Case "9"
                                If flag = "N" Then
                                    expresion = expresion & "nueve "
                                End If
                        End Select
                End Select
                If paso = 4 Then
                    If Mid(entero, 6, 1) <> "0" Or Mid(entero, 5, 1) <> "0" Or Mid(entero, 4, 1) <> "0" Or (Mid(entero, 6, 1) = "0" And Mid(entero, 5, 1) = "0" And Mid(entero, 4, 1) = "0" And Len(entero) <= 6) Then
                        expresion = expresion & "mil "
                        flag = "N"
                    End If
                End If
                If paso = 7 Then
                    If Len(entero) = 7 And Mid(entero, 1, 1) = "1" Then
                        expresion = expresion & "millón "
                    Else
                        expresion = expresion & "millones "
                    End If
                End If
            Next

            If deci <> "" Then
                If Mid(entero, 1, 1) = "-" Then 'si el numero es negativo
                    '                    MontoStr = "menos " & expresion & "pesos con " & deci & " centavos."
                    MontoStr = "menos " & expresion & "pesos " & deci & "/100."
                Else
                    '                    MontoStr = expresion & "pesos con " & deci & " centavos."
                    MontoStr = expresion & "pesos " & deci & "/100."
                End If
            Else
                If Mid(entero, 1, 1) = "-" Then 'si el numero es negativo
                    MontoStr = "menos " & expresion
                Else
                    MontoStr = expresion + "pesos"
                End If
            End If
            If Len(MontoStr) > 5 Then MontoStr = "Son " + MontoStr
        Else 'si el numero a convertir esta fuera del rango superior e inferior
            MontoStr = ""
        End If
    End Function

    Public Function PrepareDSXMLOrden(ByVal ordernum As String, ByVal fpEsteArchivo As String) As Boolean
        ' Esta función prepara el DataSet dsReporte con los datos de la Rx, y opcionalmente si se proporciona un nombre de archivo
        ' se escriben los datos el archivo en formato XML
        Dim SqlCon As SqlConnection
        Dim SqlStr As String
        Dim Labs As String = ""
        Dim orderParams(2) As SqlParameter
        Dim resultado As Boolean = True
        Dim da As SqlDataAdapter


        orderParams(0) = New SqlParameter("@ordernum", ordernum)
        orderParams(1) = New SqlParameter("@labid", Me.Plant)
        orderParams(2) = New SqlParameter("@vblabid", 0)

        SqlCon = New SqlConnection(SqlStrConn)


        Try
            SqlCon.Open()

            dsReporte.Clear()


            ' Obtiene datos de la receta sin detalles, solo el total y el material
            ' Consultamos el encabezado de la orden

            SqlStr = "SELECT ordernum,cctax as taxcode,cast(OH.number01 as integer) as Rx,OH.userinteger1 as ReferenciaDigital,OH.custnum,C.custid,C.name as Cliente," & _
            " OH.userdate1 as Creacion,OH.shortchar01 as PONum_Order,OH.character06 as NumPaquete,coalesce(P.partdescription,'') AS Paquete,OH.character07 as special_Design,OH.shortchar06 as AR,OH.checkbox20 as Optimizado," & _
            " TblMaterials.Material,TblMaterials.material + ' ' + TblDesigns.diseno as Descripcion, OH.ccamount as Total, " & _
            " OH.character05 AS Comentarios, " & _
            " CAST(OH.Number02 as decimal(10,2)) AS REsfera,CAST(OH.Number03 as decimal(10,2)) AS RCilindro,CAST(OH.Number04 as decimal(10,2)) AS REje,CAST(OH.Number05 as decimal(10,2)) AS RAdicion," & _
            " CAST(OH.Number06 as decimal(10,2)) AS LEsfera,CAST(OH.Number07 as decimal(10,2)) AS LCilindro,CAST(OH.Number08 as decimal(10,2)) AS LEje,CAST(OH.Number09 as decimal(10,2)) AS LAdicion " & _
            " ,FT.Description as TipoArmazon " & _
            " FROM orderhed OH WITH (nolock)" & _
            " INNER JOIN TblMaterials WITH (nolock) ON OH.shortchar07 = TblMaterials.cl_mat " & _
            " INNER JOIN TblDesigns WITH (nolock) ON OH.shortchar08 = TblDesigns.cl_diseno " & _
            " INNER JOIN customer C WITH (nolock) ON OH.custnum = C.custnum " & _
            " INNER JOIN TblFrameTypes FT WITH (nolock) ON OH.number19=FT.FrameType" & _
            " LEFT JOIN part P WITH (nolock) ON cast(OH.character06 as varchar)=P.partnum " & _
            " WHERE ordernum = " & ordernum

            da = New SqlDataAdapter(SqlStr, SqlCon)
            da.SelectCommand.CommandType = CommandType.Text
            da.SelectCommand.CommandTimeout = My.Settings.DBCommandTimeout
            ' da.SelectCommand.Parameters.AddWithValue("@ordernum", ordernum)

            da.Fill(dsReporte, "Orden")
            dsReporte.Namespace = "Remision"
            dsReporte.DataSetName = "Remision"
            dsReporte.Tables("Orden").Rows(0)("taxcode") = TaxPercent

            If fpEsteArchivo.Length > 1 Then dsReporte.WriteXml(fpEsteArchivo)

        Catch ex As Exception
            resultado = False
            'Throw New Exception(ex.Message)

        Finally
            SqlCon.Close()

        End Try

        Return resultado

    End Function
    Private Function GetOrder(ByVal rx As String) As String

        Dim SqlCon As SqlConnection
        Dim SqlStr As String
        Dim Labs As String = ""
        Dim orderParams(2) As SqlParameter

        orderParams(0) = New SqlParameter("@ordernum", rx)
        orderParams(1) = New SqlParameter("@labid", Me.Plant)

        '        If My.Settings.CierraVerBien Then
        '            Labs = Me.Plant & "," & My.Settings.VerBienPlant
        '        orderParams(2) = New SqlParameter("@vblabid", My.Settings.VerBienPlant)
        '        Else
        '        Labs = Me.Plant
        '        orderParams(2) = New SqlParameter("@vblabid", 0)
        '        End If

        'En este proceso no cerramos la orden, sólo consultamos
        orderParams(2) = New SqlParameter("@vblabid", rx.Substring(0, 2))

        SqlCon = New SqlConnection(SqlStrConn)
        SqlStr = "spObtieneRxInfo"


        Try
            SqlCon.Open()

            Dim ds As New DataSet
            Dim da As New SqlDataAdapter(SqlStr, SqlCon)
            da.SelectCommand.CommandType = CommandType.StoredProcedure
            da.SelectCommand.CommandTimeout = My.Settings.DBCommandTimeout

            da.SelectCommand.Parameters.Add(orderParams(0))
            da.SelectCommand.Parameters.Add(orderParams(1))
            da.SelectCommand.Parameters.Add(orderParams(2))
            da.Fill(ds, "t1")


            Dim so As New ShowOrders()
            so.GridOrders.DataSource = ds.Tables("t1")
            'CustNum = ds.Tables("t1").Rows(0)("Custnum").Value

            If so.GridOrders.RowCount > 1 Then

                so.ShowDialog()
                TextCustName.Text = so.GridOrders.SelectedRows(0).Cells("Cliente").Value
                TextPO.Text = so.GridOrders.SelectedRows(0).Cells("OrdenLab").Value
                Return so.GridOrders.SelectedRows(0).Cells("ordernum").Value.ToString

            Else
                If so.GridOrders.RowCount = 1 Then
                    so.GridOrders.Rows(0).Selected = True

                    TextRX.Text = so.GridOrders.SelectedRows(0).Cells("RefDigital").Value
                    TextCustName.Text = so.GridOrders.SelectedRows(0).Cells("Cliente").Value
                    TextPO.Text = so.GridOrders.SelectedRows(0).Cells("OrdenLab").Value
                    CustNum = so.GridOrders.SelectedRows(0).Cells("Custnum").Value

                    'If (so.GridOrders.SelectedRows(0).Cells("TipoClte").Value = 4) Then IsLiverpool = True

                    Select Case so.GridOrders.SelectedRows(0).Cells("TipoClte").Value
                        Case 4
                            IsLiverpool = True
                            Exit Select
                        Case 12
                            IsIPN = True
                            Exit Select
                        Case Else
                            Exit Select
                    End Select

                    Return so.GridOrders.SelectedRows(0).Cells("RefDigital").Value.ToString

                Else
                    Return "0"
                End If
            End If

        Catch ex As Exception

            Throw New Exception(ex.Message)
        Finally

            SqlCon.Close()
        End Try

    End Function
    Public Function GetTaxCode(ByVal PlantNum As Integer) As String
        GetTaxCode = ""

        Dim Cnn As SqlClient.SqlConnection
        Cnn = New SqlClient.SqlConnection(SqlStrConn)
        Try
            Cnn.Open()
        Catch ex As SqlClient.SqlException
            MsgBox(ex.Message, MsgBoxStyle.Critical)
        End Try

        Dim SqlStr As String = "SELECT TaxCode FROM TblLaboratorios with(nolock) WHERE cl_lab =" + PlantNum.ToString + ";"
        Dim SqlCmd As New SqlClient.SqlCommand(SqlStr, Cnn)
        Dim SQLDataRdr As SqlClient.SqlDataReader = SqlCmd.ExecuteReader

        While SQLDataRdr.Read

            Try
                GetTaxCode = SQLDataRdr.Item("TaxCode")
            Catch ex As Exception
            End Try
        End While

        SQLDataRdr.Close()
        Cnn.Close()
    End Function


    ' Metodo para calcular los totales de la factura al momento de cerrar la orden
    Private Sub CalculaTotalesNew()
        Dim i As Integer
        Dim SubtotalDtls As Single = 0
        Dim SubtotalHead As Single = 0
        Dim TaxCode As String = PlantTaxCode
        Dim Iva As Single

        Try

            ' Todos los clientes
            For i = 0 To LViewLines.Items.Count - 1
                With LViewLines.Items(i)
                    SubtotalDtls += Math.Round(CSng(.SubItems(4).Text), 3)
                End With
            Next

            SubtotalDtls = Math.Round(SubtotalDtls, 2)


            Dim SubTotal As Single = 0
            SubTotal = SubtotalDtls

            ' Todos los clientes
            cl_txtSubTot2.Text = SubtotalDtls.ToString("c")


            Try
                Iva = Math.Round(SubTotal * (TaxPercent / CSng(100)), 2)
            Catch ex As Exception
                Iva = 0
            End Try

            
            Iva = Math.Round(Iva, 2)

            Dim Tot As Single = SubtotalDtls + Iva

            cl_txtIVA2.Text = Iva.ToString("c")
            cl_txtTotal2.Text = Tot.ToString("c")
            cl_txtImporteLetra.Text = MontoStr(CDbl(Tot.ToString).ToString)


        Catch ex As Exception
            SubtotalDtls = 0
            SubtotalHead = 0
            i = 0
        Finally

        End Try

    End Sub

    ' Metodo que se modificara del original Get_Order_Items para extraer la información de una orden y poder cerrarla y
    ' facturarla
    Private Sub Get_Order_ItemsNew(ByVal Order As String, ByVal OrderNum As String, ByVal MyListView As ListView)
        Dim RxDtl As RxDetail
        Dim MyCnn As SqlClient.SqlConnection

        MyCnn = New SqlClient.SqlConnection(SqlStrConn)


        MyCnn.Open()

        Dim ItemsCmd As New SqlClient.SqlCommand("SP_GetOrderDtl", MyCnn)
        ItemsCmd.CommandType = CommandType.StoredProcedure
        ItemsCmd.CommandTimeout = My.Settings.DBCommandTimeout

        ItemsCmd.Parameters.AddWithValue("@company", "TRECEUX")
        ItemsCmd.Parameters.AddWithValue("@ordernum", OrderNum)

        Dim DRdrItems As SqlClient.SqlDataReader = ItemsCmd.ExecuteReader
        Dim RXNum As String = ""

        RxDtl = New RxDetail()

        Dim SanbornsItems As String = ""

        '===============================================
        ' Ahora extraeremos las lineas para la Factura
        '===============================================
        Dim RXTotal As Double = 0
        Dim UnitPrice As Double
        Dim ItemTot As Double = 0
        TextRX.Text = Order
        InvSubTot = 0

        IsGarantia = False
        IsGratis = False

        While DRdrItems.Read()

            Dim MyItem2 As New ListViewItem(DRdrItems.Item("Rx").ToString)
            Dim ProdCode As String
            Dim IsVirtual As Boolean = DRdrItems.Item("IsVirtual")
            Dim VirtualLab As String = DRdrItems.Item("VirtualLab").ToString
            Dim Recibida As Boolean = DRdrItems.Item("Recibida")
            Dim Partnum As String

            VoidOrder = DRdrItems.Item("VoidOrder")

            Dim PO As String = DRdrItems.Item("shortchar01").ToString
            MyItem2.SubItems.Add(DRdrItems.Item("orderqty").ToString)
            MyItem2.SubItems.Add(DRdrItems.Item("Linedesc").ToString)

            If DRdrItems.Item("IsGratis") = 1 Then
                LabelIsGratis.Visible = True
                RxDtl.IsGratis = True
                IsGratis = True
            Else
                LabelIsGratis.Visible = False
                RxDtl.IsGratis = False
            End If

            ' ----------------------------------------------------------------
            ' Aqui checa si la orden es virtual y/o garantía
            ' ----------------------------------------------------------------

            If DRdrItems.Item("IsGarantia") = 1 Then
                GroupBox6.Enabled = False
                LabelIsGarantia.Visible = True
                RxDtl.IsGarantia = True
                IsGarantia = True
            Else
                GroupBox6.Enabled = True
                LabelIsGarantia.Visible = False
                RxDtl.IsGarantia = True
            End If

            UnitPrice = DRdrItems.Item("unitprice")
            ProdCode = DRdrItems.Item("ProdCode")
            Partnum = DRdrItems.Item("PartNum")

            MyItem2.SubItems.Add(UnitPrice.ToString("c"))

            ' Falta agregar el total= Precio * Qty
            ItemTot = UnitPrice * DRdrItems.Item("orderqty")
            RXTotal += ItemTot
            MyItem2.SubItems.Add(ItemTot.ToString("c"))

            ' Se agrega el numero de parte ya que la descripción no sirve para el Stored Proc
            MyItem2.SubItems.Add(DRdrItems.Item("PartNum").ToString)
            If Partnum.Length() >= 10 Then
                If RxDtl.Eye1.Name = "" Then
                    RxDtl.Eye1.Name = ProdCode
                    RxDtl.Eye1.Value = UnitPrice
                Else
                    RxDtl.Eye2.Name = ProdCode
                    RxDtl.Eye2.Value = UnitPrice
                End If
            Else
                If Partnum.Contains("BIS") Then
                    If RxDtl.Bisel1.Name = "" Then
                        RxDtl.Bisel1.Name = Partnum
                        RxDtl.Bisel1.Value = UnitPrice
                    Else
                        RxDtl.Bisel2.Name = Partnum
                        RxDtl.Bisel2.Value = UnitPrice
                    End If
                    RxDtl.Biselado = True
                End If
            End If

            'Se agrega el numero de orden ya que la descripcion no sirve para el Stored Proc
            MyItem2.SubItems.Add(DRdrItems.Item("OrderNum").ToString)
            MyItem2.SubItems.Add(DRdrItems.Item("ProdCode").ToString)
            MyItem2.SubItems.Add(PO)
            MyItem2.SubItems.Add(DRdrItems.Item("RxMatDes").ToString)

            'Add the items to the ListView.
            MyListView.Items.AddRange(New ListViewItem() {MyItem2})

            RXNum = DRdrItems.Item("Rx").ToString
            If SanbornsItems.Length > 0 Then
                SanbornsItems &= ","
            End If
            SanbornsItems &= "'" & Partnum & "'"

        End While

        DRdrItems.Close()

        ' Guarda el monto total de la receta sin incluir los cargos miscelaneos
        RxTotalNoMisc = RXTotal

        ' AHORA TRAEREMOS LOS CARGOS MISCELANEOS
        Dim MiscItemsCmd As New SqlClient.SqlCommand("SP_GetOrderMisc", MyCnn)
        MiscItemsCmd.CommandType = CommandType.StoredProcedure
        MiscItemsCmd.CommandTimeout = My.Settings.DBCommandTimeout

        MiscItemsCmd.Parameters.AddWithValue("@company", "TRECEUX")
        MiscItemsCmd.Parameters.AddWithValue("@ordernum", OrderNum)

        Dim DRdrMscItems As SqlClient.SqlDataReader = MiscItemsCmd.ExecuteReader

        While DRdrMscItems.Read()

            Dim MyItemMisc As New ListViewItem(RXNum)

            ' Se bajo la linea MISC despues "1"
            MyItemMisc.SubItems.Add("1")
            MyItemMisc.SubItems.Add("MISC - " + DRdrMscItems.Item("Description").ToString)


            UnitPrice = DRdrMscItems.Item("miscamt")

            MyItemMisc.SubItems.Add(UnitPrice.ToString("c"))

            ' Falta agregar el total= Precio * Qty
            RXTotal += UnitPrice
            MyItemMisc.SubItems.Add(UnitPrice.ToString("c"))

            ' Este UnitPrice es sin formato!!! para tomarlo como valor para el stored proc 
            MyItemMisc.SubItems.Add(UnitPrice.ToString)
            MyItemMisc.SubItems.Add(OrderNum.ToString)

            ' Add the items to the ListView.
            MyListView.Items.AddRange(New ListViewItem() {MyItemMisc})
        End While
        DRdrItems.Close()

        ' Actualiza el total $ de la FACTURA
        InvSubTot = InvSubTot + RXTotal
        LblRxTotal.Text = RXTotal.ToString("c")
        'txtSubTot2.Text = InvSubTot.ToString("c")
        cl_txtSubTot2.Text = RXTotal.ToString("c")

        Dim Iva As Double = 0
        '----------------------------------------------------------------
        ' Chequeo de la planta en la que se trabaja, para determinar
        ' el monto del IVA
        '----------------------------------------------------------------


        'If CheckNoIVA.Checked Then Iva = 0

        cl_txtIVA2.Text = Iva.ToString("c")

        Dim Tot As Double = InvSubTot + Iva

        cl_txtTotal2.Text = Tot.ToString("c")

        cl_txtImporteLetra.Text = MontoStr(CDbl(cl_txtTotal2.Text).ToString())
        CloseRxNew.Enabled = False

        DRdrItems.Close()
        MyCnn.Close()
    End Sub

    Private Sub TextVantage_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles TextVantage.KeyPress

        ' El 22 se refiere a cuando se ejecuta el Ctrl+V (Pegar)
        If Char.IsNumber(e.KeyChar) Or Asc(e.KeyChar) = Keys.Back Or Asc(e.KeyChar) = 22 Then
            e.Handled = False
        Else
            e.Handled = True
        End If

        If e.KeyChar = Chr(13) Then
            If TextVantage.Text <> "" Then
                Carga_Orden()
            End If
        End If

    End Sub

    Private Sub Carga_Orden()
        Try
            Dim ErrorMsg As String = ""
            Dim RxNum As String
            Dim RxInspected As Boolean = True
            Dim myDataRow As DataRow
            Dim myDS As DataSet

            LblRxTotal.Text = "0"

            Me.Cursor = Cursors.WaitCursor
            LViewLines.Items.Clear()

            If LViewRX.SelectedItems.Count > 0 Then
                GroupBox6.Enabled = True
            Else
            End If

            Try : OrderNum = Convert.ToInt32(TextVantage.Text) : Catch ex As Exception : Throw New Exception("Vantage Inválido [" & TextVantage.Text & "]") : End Try

            Try : RxNum = GetOrder(OrderNum)

            Catch ex As Exception
                ErrorMsg = ex.Message
                RxNum = 0
                RxInspected = False

            End Try

            If RxNum = 0 Then
                CheckOrderStatus(OrderNum)
            End If



            If RxNum <> "0" Then

                If Val(OrderNum) > 0 Then
                    'Try
                    '    objNegociosReceptor = New Negocios.NegociosReceptor(My.Settings.LocalDBConnection)

                    '    dsListaReceptor = New DataSet()
                    '    dsListaReceptor = objNegociosReceptor.listaReceptor(CustNum)

                    'Catch ex As Exception

                    'End Try

                    LViewLines.Clear()
                    Me.LViewLines.Columns.Add("Rx", 0, HorizontalAlignment.Center)
                    Me.LViewLines.Columns.Add("Cant.", 45, HorizontalAlignment.Right)
                    Me.LViewLines.Columns.Add("Parte", 330, HorizontalAlignment.Left)
                    Me.LViewLines.Columns.Add("Precio", 80, HorizontalAlignment.Right)
                    Me.LViewLines.Columns.Add("Total", 80, HorizontalAlignment.Right)
                    Me.LViewLines.Columns.Add("Prodcode", 0, HorizontalAlignment.Right)
                    Me.LViewLines.Columns.Add("MatDes", 0, HorizontalAlignment.Right)

                    Get_Order_ItemsNew(RxNum, OrderNum, Me.LViewLines)

                    If LViewLines.Items.Count > 0 Then

                        CloseRxNew.Enabled = True
                        TextCustName.Visible = True
                        TextPO.Visible = True
                        Label47.Visible = True
                        Label46.Visible = True
                        Label45.Visible = True
                        TextRX.Visible = True


                        'Fill Listview with dataset

                        myDS = GetPriceListDesc(CustNum)
                        If myDS.Tables(0).Rows.Count > 0 Then


                            lvListasPrecios.Visible = False
                            lvListasPrecios.Items.Clear()
                            'lvListasPrecios.Columns.Add("Lista de Precios", 320, HorizontalAlignment.Center)
                            For Each myDataRow In myDS.Tables(0).Rows
                                With lvListasPrecios
                                    .Items.Add(myDataRow(0).ToString())
                                    '.Items(.Items.Count - 1).SubItems.Add(myDataRow(0).ToString())
                                End With
                            Next
                            lvListasPrecios.Visible = True
                        End If

                        cl_CheckEnvio.Enabled = True

                        CalculaTotalesNew()

                        Dim cl_total As Decimal = cl_txtTotal2.Text.Replace("$", "")

                    Else
                    End If
                Else
                    GroupBox6.Enabled = False       ' Se desactiva porque la orden es invalida
                    CloseRxNew.Enabled = False
                    TextRX.Clear()
                    TextCustName.Clear()
                    TextCustName.Visible = False
                    Label47.Visible = False
                    TextPO.Visible = False
                    Label46.Visible = False
                    MsgBox("No se encuentra la orden o la orden ya fue cerrada", MsgBoxStyle.Critical)
                    TextRX.Text = ""
                    TextRX.Focus()
                    InitValues()
                End If

                TextVantage.SelectAll()
                Me.Cursor = Cursors.Default

            Else

                Me.Cursor = Cursors.Default

                If (ErrorMsg.Length > 0) Then
                    ErrorMessage.ShowDialog(ErrorMsg, "Apertura Orden", ErrorMessage.ErrorMessageIcon.Exclamation, ErrorMessage.ErrorMessageButtons.OK)
                Else
                End If

                TextVantage.Text = ""
                TextVantage.Focus()
                InitValues()

            End If

        Catch ex As Exception
            ErrorMessage.ShowDialog(ex.Message, "Apertura Orden", ErrorMessage.ErrorMessageIcon.Exclamation, ErrorMessage.ErrorMessageButtons.OK)
            InitValues()
        Finally
            Cursor = Cursors.Default
        End Try
    End Sub

    Private Sub Close_Rx_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CloseRxNew.Click
        Dim MyInvoiceNum As Integer = 0
        Dim CadenaOriginal As String = ""

        Dim cnn As SqlClient.SqlConnection
        Dim cnn2 As SqlClient.SqlConnection
        Dim TranMfgSys As SqlClient.SqlTransaction = Nothing
        Dim TranErpMaster As SqlClient.SqlTransaction = Nothing
        Dim Failed As Boolean = False
        Dim FailedMessage As String = ""

        cnn = New SqlClient.SqlConnection(SqlStrConn)
        cnn2 = New SqlClient.SqlConnection(SqlStrConn)
        
        Try
            Me.Cursor = Cursors.WaitCursor

            cnn.Open()
            cnn2.Open()
            TranMfgSys = cnn.BeginTransaction
            TranErpMaster = cnn2.BeginTransaction



            If OrderNum > 0 Then
                ' Se deben guardar los cambios hechos a la orden
                'MyInvoiceNum = CerrarOrder(cnn, TranMfgSys)
                Dim SubTotalStr, ImpuestoStr, TotalStr, TasaStr As String
                Dim CfdDate As DateTime = DateTime.Now

                SubTotalStr = cl_txtSubTot2.Text.Replace("$", "")
                ImpuestoStr = cl_txtIVA2.Text.Replace("$", "")
                TotalStr = cl_txtTotal2.Text.Replace("$", "")
                TasaStr = PlantTaxCode.Substring(PlantTaxCode.IndexOf("IVA") + 3)
                'PrepareDSXMLOrden(OrderNum.ToString(), MyRemisionXML)
                PrepareDSXMLOrden(OrderNum.ToString(), MyRemisionXML)
                PrintRemision()

            End If


        Catch ex As Exception
            Failed = True
            FailedMessage = ex.Message
            TranMfgSys.Rollback()
            TranErpMaster.Rollback()

        Finally
            cnn.Close()
            cnn2.Close()

            If Failed Then
                MsgBox(FailedMessage, MsgBoxStyle.Critical)
            Else
                InitValues()


                TextRX.SelectAll()
                TextRX.Focus()
            End If
        End Try

        Me.Cursor = Cursors.Default
        Me.Close()

    End Sub

    Private Sub btnMiscCharge_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnMiscCharge.Click
        If (LViewLines.Items.Count >= 1) Then
            Dim objMC As MiscCharges
            Dim RxNum, OrderN As Integer

            RxNum = TextRX.Text
            OrderN = TextVantage.Text

            ' Forma rápida de obtener el username bajo el cual corre el programa
            'string name = WindowsIdentity.GetCurrent().Name;
            'string name = Thread.CurrentPrincipal.Identity.Name;
            objMC = New MiscCharges(OrderN, RxNum, RxTotalNoMisc, Environment.UserName)
            objMC.ShowDialog()

            TextVantage_KeyPress(sender, New KeyPressEventArgs(Chr(13)))
        Else
            MessageBox.Show("No existen detalles en esta orden. Recapture la orden por favor.","Orden sin detalles",MessageBoxButtons.OK)
        End If


    End Sub

    'Procedimiento que imprime la receta usando archivo en disco de reporte cystal. Más flexible pero menos confiable por los accesos a disco
    'pero muy flexible porque permite modificar solo el reporte de crystal sin tener que compilar el proyecto
    Public Sub PrintRemision()
        Dim reporte As New CrystalDecisions.CrystalReports.Engine.ReportDocument
        Dim dsXML As New DataSet

        'Pedro Farías  Dic 6 2012

        Try

            dsXML.ReadXml(MyRemisionXML)
            reporte.Load(My.Application.Info.DirectoryPath & "\Remision.rpt")

            reporte.SetDataSource(dsXML)
            reporte.PrintToPrinter(1, False, 1, 1)

        Catch ex As Exception
            MsgBox(ex.Message)
            MsgBox("Ocurrió un error al imprimir la receta. Para continuar debes cerrar la aplicacion y ejecutarla de nuevo." & vbCrLf & "Debes reimprimir la Rx con una modificación con Vantage: " & OrderNum & vbCrLf & vbCrLf & ex.Message, MsgBoxStyle.Critical)
        End Try
        reporte.Dispose()

    End Sub
    'muestra una forma con el preview de la receta
    Public Sub PrintPreview()
        Dim frm As New RxPreview
        'imprimeBloqueCentro()
        frm.ShowDialog()
    End Sub
End Class

Public Class RxDetail
    Public Eye1 As New ItemWithValue()
    Public Eye2 As New ItemWithValue()
    Public IsGarantia As Boolean
    Public AR As Boolean
    Public Biselado As Boolean
    Public IsGratis As Boolean
    Public Bisel1 As New ItemWithValue()
    Public Bisel2 As New ItemWithValue()
End Class