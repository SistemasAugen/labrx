Public Class Cotizacion
    Public VantageOrder As String
    Public Pedido As String
    Public CustNum As Integer
    Public CotiAceptada As Boolean

    Private Sub Cotizacion_Disposed(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Disposed
        CotiAceptada = False
    End Sub

    Private Sub Cotizacion_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        If (e.KeyCode) = Keys.Enter Then
            CotiAceptada = True
            Me.Close()
        End If
        If e.KeyCode = Keys.Escape Then
            CotiAceptada = False
            Me.Close()
        End If
    End Sub

    Private Sub Cotizacion_KeyUp(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyUp
        If (e.KeyCode) = Keys.Enter Then
            CotiAceptada = True
            Me.Close()
        End If
        If e.KeyCode = Keys.Escape Then
            CotiAceptada = False
            Me.Close()
        End If
    End Sub

   
    Private Sub Cotizacion_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Cotizacion()
    End Sub
    Private Sub Cotizacion()
        '////////////////////////////////////////////////////////
        'buscamos si el cliente tiene una lista de precios especial
        Dim ListCode As String = ""
        Dim MyCnn As New Data.SqlClient.SqlConnection(My.Settings.MFGSysConnection) '"user ID=sa;password=proliant01;database=mfgsys80;server=AUGENSVR2;Connect Timeout=30")
        MyCnn.Open()
        Dim ItemsCmd As New SqlClient.SqlCommand("select listcode from customerpricelst where custnum =" & CustNum & " and company ='TRECEUX'", MyCnn)
        Dim DRdrItems As SqlClient.SqlDataReader = ItemsCmd.ExecuteReader
        While DRdrItems.Read()
            ListCode = DRdrItems.Item("listcode").ToString
        End While
        DRdrItems.Close()
        MyCnn.Close()


        'CALCULAMOS EL COSTO DE LA RECETA
        Dim parte, coma, str, partdesc As String
        Dim qty, i, linea As Integer
        Dim PriceCmd As SqlClient.SqlCommand
        Dim PriceRd As SqlClient.SqlDataReader
        Dim PrecioReal As Double = 0.0


        Dim RXTotal As Double = 0
        Dim UnitPrice As Double
        Dim ItemTot As Double = 0

        partdesc = ""
        '********************************
        LViewLines.Clear()
        Me.LViewLines.Columns.Add("Linea", 50, HorizontalAlignment.Center)
        Me.LViewLines.Columns.Add("Parte", 230, HorizontalAlignment.Left)
        Me.LViewLines.Columns.Add("Cant.", 40, HorizontalAlignment.Right)
        Me.LViewLines.Columns.Add("Precio.", 60, HorizontalAlignment.Right)
        Me.LViewLines.Columns.Add("TOTAL", 60, HorizontalAlignment.Right)
        '********************************

        linea = 1
        i = 0
        MyCnn.Open()
        While i < (Pedido.Length - 1)

            'extraemos el numero de parte y la cantidad de cada una
            coma = InStr(Pedido.Substring(i, Pedido.Length - i), ",", CompareMethod.Text)
            parte = Pedido.Substring(i, coma - 1)
            i = coma + i
            coma = InStr(Pedido.Substring(i, Pedido.Length - i), ",", CompareMethod.Text)
            If coma = 0 And (i = Pedido.Length - 1) Then
                qty = Pedido.Substring(i, 1)
            Else
                qty = Pedido.Substring(i, coma - 1)
            End If
            i = coma + i

            'buscamos el verdadero precio de la parte
            str = "select unitprice,partdescription from part  with (nolock) where partnum = '" & parte & "' and company = 'TRECEUX' "
            PriceCmd = New SqlClient.SqlCommand(str, MyCnn)
            PriceRd = PriceCmd.ExecuteReader
            If PriceRd.HasRows Then
                PriceRd.Read()
                PrecioReal = PriceRd.Item("unitprice").ToString
                partdesc = PriceRd.Item("partdescription").ToString
            End If
            PriceRd.Close()
            str = "select top 1 * from VwPriceListByPartQtyBreak  with (nolock) where partnum = '" & parte & "' and custnum = " & CustNum & " and listcode = '" & ListCode & "' and company = 'TRECEUX' AND quantity <= " & qty & ""
            PriceCmd = New SqlClient.SqlCommand(str, MyCnn)
            PriceRd = PriceCmd.ExecuteReader
            If PriceRd.HasRows Then
                PriceRd.Read()
                PrecioReal = PriceRd.Item("unitprice").ToString
            Else
                PriceRd.Close()
                str = "select * from VwPriceListByPart with (nolock) where partnum = '" & parte & "' and custnum = " & CustNum & " and listcode = '" & ListCode & "' and company = 'TRECEUX'"
                PriceCmd = New SqlClient.SqlCommand(str, MyCnn)
                PriceRd = PriceCmd.ExecuteReader
                If PriceRd.HasRows Then
                    PriceRd.Read()
                    PrecioReal = PriceRd.Item("baseprice").ToString
                Else
                    PriceRd.Close()
                    str = "select top 1 * from VwPriceListByGroupQtyBreak with (nolock) where partnum = '" & parte & "' and custnum = " & CustNum & " and listcode = '" & ListCode & "' and company = 'TRECEUX' AND quantity <= " & qty & ""
                    PriceCmd = New SqlClient.SqlCommand(str, MyCnn)
                    PriceRd = PriceCmd.ExecuteReader
                    If PriceRd.HasRows Then
                        PriceRd.Read()
                        PrecioReal = PriceRd.Item("unitprice").ToString
                    Else
                        PriceRd.Close()
                        str = "select * from VwPriceListByGroup with (nolock) where partnum = '" & parte & "' and custnum = " & CustNum & " and listcode = '" & ListCode & "' and company = 'TRECEUX'"
                        PriceCmd = New SqlClient.SqlCommand(str, MyCnn)
                        PriceRd = PriceCmd.ExecuteReader
                        If PriceRd.HasRows Then
                            PriceRd.Read()
                            PrecioReal = PriceRd.Item("baseprice").ToString
                        End If
                    End If
                End If

            End If
            PriceRd.Close()

            '*****************************
            Dim MyItem2 As New ListViewItem(linea)
            MyItem2.SubItems.Add(partdesc)
            MyItem2.SubItems.Add(qty)

            UnitPrice = PrecioReal

            MyItem2.SubItems.Add(UnitPrice.ToString("c"))

            'falta agregar el total= Precio * Qty
            ItemTot = UnitPrice * qty
            RXTotal += ItemTot
            MyItem2.SubItems.Add(ItemTot.ToString("c"))

            'Se agrega El numero de Parte ya que la descripcion no sirve para el Stored Proc
            MyItem2.SubItems.Add(parte)

            'Se agrega El numero de Orden ya que la descripcion no sirve para el Stored Proc
            'MyItem2.SubItems.Add(DRdrItems.Item("OrderNum").ToString)
            'Add the items to the ListView.

            Me.LViewLines.Items.AddRange(New ListViewItem() {MyItem2})
            'RXNum = DRdrItems.Item("Rx").ToString
            linea = linea + 1
            '*****************************


        End While
        LblRxTotal.Text = RXTotal.ToString("c")
        MyCnn.Close()
        MyCnn.Dispose()
        MyCnn = Nothing
        '////////////////////////////////////////////////////////
    End Sub
    Private Sub Cotiza_VantageOrderNum()
        'Dim MyListView As ListView
        'Dim MyListView As LViewLines
        Dim InvSubTot As Double
        Dim SQLSTR As String
        Dim linea As Integer = 1
        Dim MyCnn As New Data.SqlClient.SqlConnection(My.Settings.MFGSysConnection) '"user ID=sa;password=proliant01;database=mfgsys80;server=AUGENSVR2;Connect Timeout=30")
        Try
            MyCnn.Open()
            'Dim FilterStr As String = "WHERE ordernum ='" + VantageOrder + "'"
            'Dim SqlStr = "SELECT Rx, Partnum, Linedesc, Orderqty, Unitprice FROM VwRXInvoiceDetail " + FilterStr
            SQLSTR = " SELECT dbo.orderdtl.ordernum, dbo.orderdtl.orderline, dbo.orderdtl.partnum, dbo.orderdtl.linedesc, dbo.orderdtl.prodcode, " & _
                     " dbo.orderdtl.orderqty, dbo.orderdtl.ium, dbo.orderdtl.unitprice, dbo.orderdtl.unitprice * dbo.orderdtl.orderqty AS Total, " & _
                     " dbo.customer.number01 AS plant, dbo.customer.number02 AS plant2" & _
                     " FROM dbo.orderdtl WITH (nolock) " & _
                     " INNER Join " & _
                     " dbo.customer WITH (nolock) ON dbo.orderdtl.custnum = dbo.customer.custnum AND dbo.orderdtl.company = dbo.customer.company " & _
                     " WHERE (dbo.orderdtl.company = 'treceux') and ordernum = " & VantageOrder & " " & _
                     " ORDER BY dbo.orderdtl.ordernum, dbo.orderdtl.orderline"

            Dim ItemsCmd As New SqlClient.SqlCommand(SQLSTR, MyCnn)
            Dim DRdrItems As SqlClient.SqlDataReader = ItemsCmd.ExecuteReader
            Dim RXNum As String = ""
            '===============================================
            '= Ahora extraeremos las lineas para la Factura
            '===============================================
            Dim RXTotal As Double = 0
            Dim UnitPrice As Double
            Dim ItemTot As Double = 0
            LViewLines.Clear()
            Me.LViewLines.Columns.Add("Linea", 50, HorizontalAlignment.Center)
            Me.LViewLines.Columns.Add("Parte", 230, HorizontalAlignment.Left)
            Me.LViewLines.Columns.Add("Cant.", 40, HorizontalAlignment.Right)
            Me.LViewLines.Columns.Add("Precio.", 60, HorizontalAlignment.Right)
            Me.LViewLines.Columns.Add("TOTAL", 60, HorizontalAlignment.Right)

            While DRdrItems.Read()

                'Dim MyItem2 As New ListViewItem(DRdrItems.Item("Rx").ToString)
                Dim MyItem2 As New ListViewItem(linea)
                MyItem2.SubItems.Add(DRdrItems.Item("Linedesc").ToString)
                MyItem2.SubItems.Add(DRdrItems.Item("orderqty").ToString)

                UnitPrice = DRdrItems.Item("unitprice")

                MyItem2.SubItems.Add(UnitPrice.ToString("c"))

                'falta agregar el total= Precio * Qty
                ItemTot = UnitPrice * DRdrItems.Item("orderqty")
                RXTotal += ItemTot
                MyItem2.SubItems.Add(ItemTot.ToString("c"))

                'Se agrega El numero de Parte ya que la descripcion no sirve para el Stored Proc
                MyItem2.SubItems.Add(DRdrItems.Item("PartNum").ToString)

                'Se agrega El numero de Orden ya que la descripcion no sirve para el Stored Proc
                MyItem2.SubItems.Add(DRdrItems.Item("OrderNum").ToString)
                'Add the items to the ListView.

                Me.LViewLines.Items.AddRange(New ListViewItem() {MyItem2})
                'RXNum = DRdrItems.Item("Rx").ToString
                linea = linea + 1
            End While
            DRdrItems.Close()

            ''AHORA TRAEREMOS LOS CARGOS MISCELANEOS
            'FilterStr = "WHERE Ordernum ='" + OrderNum + "'"
            'SqlStr = "SELECT Ordernum, Description, miscamt FROM Ordermsc " + FilterStr
            'Dim MiscItemsCmd As New SqlClient.SqlCommand(SqlStr, MyCnn)
            'Dim DRdrMscItems As SqlClient.SqlDataReader = MiscItemsCmd.ExecuteReader

            'While DRdrMscItems.Read()

            '    Dim MyItemMisc As New ListViewItem(RXNum)
            '    MyItemMisc.SubItems.Add("MISC - " + DRdrMscItems.Item("Description").ToString)
            '    MyItemMisc.SubItems.Add("1.00")

            '    UnitPrice = DRdrMscItems.Item("miscamt")

            '    MyItemMisc.SubItems.Add(UnitPrice.ToString("c"))

            '    'falta agregar el total= Precio * Qty
            '    RXTotal += UnitPrice
            '    MyItemMisc.SubItems.Add(UnitPrice.ToString("c"))
            '    'Este UnitPrice es sin formato!!! para tomarlo como valor para el stored proc 
            '    MyItemMisc.SubItems.Add(UnitPrice.ToString)

            '    'Add the items to the ListView.
            '    MyListView.Items.AddRange(New ListViewItem() {MyItemMisc})
            'End While
            'DRdrItems.Close()

            'actualiza el total $ de la FACTURA
            InvSubTot = InvSubTot + RXTotal
            ''LblRxTotal.Text = InvSubTot.ToString("c")
            LblRxTotal.Text = RXTotal.ToString("c")
            'Dim SubTot As Double = InvSubTot
            'txtSubTot2.Text = InvSubTot.ToString("c")
            Dim Iva As Double = InvSubTot * 0.1
            'txtIVA2.Text = Iva.ToString("c")
            Dim Tot As Double = InvSubTot + Iva
            'txtTotal2.Text = Tot.ToString("c")

            'txtImporteLetra.Text = MontoStr(Tot.ToString)
            DRdrItems.Close()
            MyCnn.Close()
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub
    Private Sub Preview_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Preview.Click
        CotiAceptada = True
        Me.Close()
    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        CotiAceptada = False
        Me.Close()
    End Sub
End Class