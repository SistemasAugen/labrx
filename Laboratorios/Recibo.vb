Imports Microsoft.Win32
Imports System.Threading
Imports System.IO
Imports System.Data.OleDb

Public Class Recibo
    Dim Time As Integer = 1
    Dim minutos, segundos As Integer
    Dim dspseg As String
    Dim DGInd As Integer = -1
    Dim DS As DataSet
    Dim NumLineas As Integer

    Dim PO_OPTION As Boolean = False
    Dim PO_PARTQTY_CHG As String

    Private ProgressPS As Thread
    Public ProcesaEmbarque As EjecutarPackingSlip

    Private Sub btnFind_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnFind.Click
        Dim consys As SqlDB
        Dim shiptonum As String
        PO_OPTION = False

        Try
            Me.Cursor = Cursors.WaitCursor
            Application.DoEvents()

            If Form1.WorkingOnLine Or Form1.Ping(My.Settings.AugenServer) Then

                consys = New SqlDB(My.Settings.AugenServer, My.Settings.DBUser, My.Settings.DBPassword, My.Settings.AugenDB)


                consys.OpenConn()

                Dim SqlStr As String
                DS = New DataSet
                shiptonum = Laboratorio_shiptonum()
                If txtpacknum.Text = "" Or txtpacknum.Text = "0" Then
                    DGPacking.Visible = False
                    DGInventario.Visible = False
                    DGEmbarque.Visible = False
                    dgpackpend.Visible = True

                    SqlStr = "SELECT company,packnum as Embarque, COUNT(Parte) AS Lineas,FECHA " & _
                             " FROM VwPackingsToLabs WITH(NOLOCK) WHERE RCVDATE IS NULL AND SHIPTONUM = '" & shiptonum & "' " & _
                             " GROUP BY company,packnum,fecha" & _
                             " ORDER BY packnum"
                    DS = consys.SQLDS(SqlStr, "t1")
                    If DS.Tables(0).Rows.Count > 0 Then
                        dgpackpend.Visible = True
                        dgpackpend.DataSource = DS.Tables("t1")
                        'DS.Dispose()
                        'DS = Nothing
                    Else
                        dgpackpend.Visible = False
                        'dgpackpend.DataSource = ""
                        dgpackpend.DataSource = DS.Tables("t1")
                        MsgBox("No Existen embarques pendientes por recibir...", MsgBoxStyle.Critical)

                    End If
                Else
                    dgpackpend.Visible = False
                    DGPacking.Visible = False


                    SqlStr = "select parte as partnum, descripcion as linedesc, cantidad as ourinventoryshipqty, company, packnum, packline, ium, number02, checkbox01, binnum from VwPackingsToLabs WITH(NOLOCK) Where packnum = " & txtpacknum.Text & " AND rcvdate IS NULL AND shiptonum = '" & shiptonum & "'"

                    DS = consys.SQLDS(SqlStr, "t1")
                    If DS.Tables(0).Rows.Count > 0 Then
                        DGEmbarque.Visible = True
                        DGEmbarque.DataSource = DS.Tables("t1")
                        Button1.Enabled = True
                        InsertLocalShipDtl(DS, "AUGEN", Me.txtpacknum.Text)
                        'DS.Dispose()
                        'DS = Nothing
                    Else
                        Limpia_DG()
                        MsgBox("El numero de embarque no puede ser recibido por las sig. razones:" & vbCrLf & "1. No existe" & vbCrLf & "2. Ya fue recibido" & vbCrLf & "3. No pertecene a este laboratorio", MsgBoxStyle.Critical)

                        Button1.Enabled = False
                        txtpacknum.Text = ""
                        txtpacknum.Focus()

                    End If

                End If
                consys.CloseConn()
                consys = Nothing
            Else
                MsgBox("No hay comunicación con el servidor remoto " & My.Settings.AugenServer & vbCrLf & vbCrLf & "No hay PING.", MsgBoxStyle.Critical, "Error al consultar servidor ")
                Me.Close()
            End If
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, "Error al consultar servidor " & My.Settings.AugenServer)
            If ex.Message.Contains("Error de Conexion") Then
                Form1.ChangeWorkingStatus(Form1.WorkingStatusType.OffLine)
            End If
        Finally
            Me.Cursor = Cursors.Default
        End Try
    End Sub

    Private Function Laboratorio_shiptonum() As String
        Dim MyCnn As New Data.SqlClient.SqlConnection(My.Settings.ERPMasterConnection)
        Dim STR As String
        Dim RD As SqlClient.SqlDataReader
        Dim CMD As SqlClient.SqlCommand
        Dim CLAVE As String = ""
        Try

            MyCnn.Open()

            STR = "SELECT shiptonum FROM Tbllaboratorios WITH(NOLOCK)" & _
                 " WHERE TBLLABORATORIOS.PLANT = '" & Read_Registry("Plant") & "' "
            CMD = New SqlClient.SqlCommand(STR, MyCnn)
            RD = CMD.ExecuteReader
            If RD.HasRows Then
                RD.Read()
                CLAVE = RD.Item("SHIPTONUM")
            Else
            End If
            RD.Close()
            MyCnn.Close()
            CMD.Dispose()
            Return CLAVE
        Catch ex As Exception
            Return CLAVE
        End Try

    End Function

    Private Sub Button3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button3.Click
        btninventory.Enabled = True
        DGInventario.Visible = False
        dgpackpend.Visible = False
        DGEmbarque.Visible = False
        Limpia_DG()
        DGPacking.Visible = True
        txtpacknum.Text = 0
        Button1.Enabled = False
        txtpacknum.Focus()
    End Sub

    Public Sub Limpia_DG()
        Dim ConSys1 As New SqlDB(My.Settings.LocalServer, My.Settings.DBUser, My.Settings.DBPassword, My.Settings.LocalDBName)

        Try
            ConSys1.OpenConn()
            DS = New DataSet
            'DS = ConSys1.SQLDS("SELECT parte as partnum, descripcion as linedesc, cantidad as ourinventoryshipqty, company, packnum, packline, ium, number02, checkbox01, binnum FROM VwPackingsToLabs WITH(NOLOCK) where packnum = 0", "t1")
            DS = ConSys1.SQLDS("SELECT partnum, linedesc, ourinventoryshipqty, company, packnum, packline, ium, number02, checkbox01, binnum FROM shipdtl WITH(NOLOCK) where packnum = 0", "t1")

            DGEmbarque.DataSource = DS.Tables("t1")

        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical)
        Finally
            ConSys1.CloseConn()
            DS.Dispose()
            DS = Nothing
            ConSys1 = Nothing
        End Try
    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        Try
            If MsgBox("Estas Seguro de Aceptar este Embarque?", MsgBoxStyle.YesNo + MsgBoxStyle.Question, "Recibo de Embarque") = MsgBoxResult.Yes Then
                'verificamos si las cantidades fueron modificadas y guardamos los cambios
                lblsts.Text = "Verificando cambios..."
                Application.DoEvents()
                Dim company As String = ""

                Try : company = dgpackpend.Item("company", DGInd).Value : Catch : company = "AUGEN" : End Try

                PO_PARTQTY_CHG = ""

                'Throw New Exception("x")

                Select Case PO_OPTION
                    Case False
                        Actualiza_CantidadRecibida(company)

                        ProcesaEmbarque = New EjecutarPackingSlip(txtpacknum.Text)
                        ProcesaEmbarque.Company = company
                        ProgressPS = New Thread(New ThreadStart(AddressOf ProcesaEmbarque.Guardar_Embarque))

                    Case True
                        PO_PARTQTY_CHG = UpdateRealRcvQty()

                        ProcesaEmbarque = New EjecutarPackingSlip(txtpacknum.Text)
                        ProcesaEmbarque.setParams(PO_PARTQTY_CHG)
                        ProcesaEmbarque.Company = company
                        ProgressPS = New Thread(New ThreadStart(AddressOf ProcesaEmbarque.Guardar_PO))

                End Select


                'ProcesaEmbarque = New EjecutarPackingSlip(txtpacknum.Text)
                'ProgressPS = New Thread(New ThreadStart(AddressOf ProcesaEmbarque.Guardar_Embarque))

                '*****INICIALIZAMOS EL TIMER
                minutos = 0
                segundos = 1
                txttime.Visible = True
                lblsts.Text = "Recibiendo Embarque # " & txtpacknum.Text
                lblsts.Visible = True
                progresbar.Visible = True
                Timer1.Enabled = True
                '***************************
                ProgressPS.Start()

                'Guardar_Embarque()

                Limpia_DG()
                txtpacknum.Text = 0
                txtpacknum.Focus()
            End If
        Catch ex As Exception
            ProcesaEmbarque = Nothing
            MsgBox("Excepcion!!! " & vbCrLf & ex.Message, MsgBoxStyle.Critical)
        End Try
    End Sub

    Private Sub Actualiza_CantidadRecibida(ByVal company As String)
        'para actualizar la cantidad real recibida utilizaremos el campo number02 de la tabla shipdtl para guardar LA CANTIDAD y utilizaremos
        'el checkbox01 con valor de 1 para indicar que si cambio la cantidad al momento de ejecutar el store procedure, recordemos que es un recibo
        'miscelaneo y no tenemos un rcvdtl por que no se realizo un packins slip DE AUGEN PARA RECIBIR EN TRECEUX
        Try

            Dim i, cantidad, modificado As Integer
            Dim parte, binnum As String

            For i = 0 To NumLineas - 1
                cantidad = DGEmbarque.Item("ourinventoryshipqty", i).Value.ToString
                parte = DGEmbarque.Item("partnum", i).Value.ToString
                Try : binnum = DGEmbarque.Item("binnum", i).Value.ToString : Catch : binnum = "" : End Try

                If DGEmbarque.Item(0, i).Value Is Nothing Then
                    modificado = 0
                Else
                    modificado = DGEmbarque.Item(0, i).Value.ToString 'si vale uno entonces la celda fue modificada
                End If

                If modificado = 1 Then
                    Modifica_Cantidad(parte, cantidad, binnum, company)
                End If
            Next
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Sub

    Private Function UpdateRealRcvQty() As String
        Dim params As String = ""
        Dim i, modificado As Integer            'cantidad, 
        'Dim parte As String

        Try

            For i = 0 To DGEmbarque.RowCount - 1
                'parte = DGEmbarque.Item(1, i).Value.ToString
                'cantidad = DGEmbarque.Item(3, i).Value.ToString

                If Not DGEmbarque.Item(0, i).Value Is Nothing Then
                    modificado = 1
                    'Else
                    'modificado = DGEmbarque.Item(0, i).Value.ToString 'si vale uno entonces la celda fue modificada
                End If

                If modificado = 1 Then
                    params += DGEmbarque.Item(1, i).Value.ToString + ","
                    params += DGEmbarque.Item(3, i).Value.ToString + ","

                    modificado = 0
                End If
            Next
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        If params.Length > 0 Then
            Return params.Substring(0, params.Length - 1)
        Else
            Return params
        End If
    End Function

    Private Sub Modifica_Cantidad(ByVal part As String, ByVal cant As Integer, ByVal bin As String, ByVal company As String)
        Dim MyCnnLocal As SqlClient.SqlConnection
        Dim MyCnnServer As SqlClient.SqlConnection
        Dim Cmd As SqlClient.SqlCommand = Nothing
        Dim stR As String = ""
        Dim TranLocal As SqlClient.SqlTransaction
        Dim TranServer As SqlClient.SqlTransaction

        Try
            Select Case company
                Case "TRECEUX"
                    MyCnnServer = New SqlClient.SqlConnection(My.Settings.MFGSysConnection)
                    MyCnnServer.Open()
                    TranServer = MyCnnServer.BeginTransaction

                    MyCnnLocal = New SqlClient.SqlConnection("user ID=" & My.Settings.DBUser & ";password=" & My.Settings.DBPassword & ";database=" & My.Settings.LocalDBName & ";server=" & My.Settings.LocalServer & ";Connect Timeout=10")
                    MyCnnLocal.Open()
                    TranLocal = MyCnnLocal.BeginTransaction
                    Try

                        ' Se actualiza el shipdtl del Servidor
                        stR = " UPDATE TfShipDtl SET NUMBER02 = " & cant & ",CHECKBOX01 = 1 " & _
                              " WHERE COMPANY = '" & company & "' AND PACKNUM = " & txtpacknum.Text & "  AND PARTNUM = '" & part & "' "
                        Cmd = New SqlClient.SqlCommand(stR, MyCnnServer, TranServer)
                        Cmd.ExecuteNonQuery()

                        ' Se actualiza el shipdtl Local
                        stR = " UPDATE TfShipDtl SET NUMBER02 = " & cant & ",CHECKBOX01 = 1 " & _
                              " WHERE COMPANY = '" & company & "' AND PACKNUM = " & txtpacknum.Text & "  AND PARTNUM = '" & part & "'"
                        Cmd = New SqlClient.SqlCommand(stR, MyCnnLocal, TranLocal)
                        Cmd.ExecuteNonQuery()

                        TranLocal.Commit()
                        TranServer.Commit()

                    Catch ex As Exception
                        TranLocal.Rollback()
                        TranServer.Rollback()
                    Finally
                        MyCnnServer.Close()
                        MyCnnLocal.Close()
                    End Try

                Case Else
                    MyCnnServer = New SqlClient.SqlConnection("user ID=" & My.Settings.DBUser & ";password=" & My.Settings.DBPassword & ";database=" & My.Settings.AugenDB & ";server=" & My.Settings.AugenServer & ";Connect Timeout=10")
                    MyCnnServer.Open()
                    TranServer = MyCnnServer.BeginTransaction

                    MyCnnLocal = New SqlClient.SqlConnection("user ID=" & My.Settings.DBUser & ";password=" & My.Settings.DBPassword & ";database=" & My.Settings.LocalDBName & ";server=" & My.Settings.LocalServer & ";Connect Timeout=10")
                    MyCnnLocal.Open()
                    TranLocal = MyCnnLocal.BeginTransaction
                    Try
                        ' Se actualiza el shipdtl del Servidor
                        stR = " UPDATE SHIPDTL SET NUMBER02 = " & cant & ",CHECKBOX01 = 1 " & _
                              " WHERE COMPANY = 'AUGEN' AND PACKNUM = " & txtpacknum.Text & "  AND PARTNUM = '" & part & "' AND BINNUM = '" & bin & "' "
                        Cmd = New SqlClient.SqlCommand(stR, MyCnnServer)
                        Cmd.ExecuteNonQuery()

                        ' Se actualiza el shipdtl Local
                        stR = " UPDATE SHIPDTL SET NUMBER02 = " & cant & ",CHECKBOX01 = 1 " & _
                              " WHERE COMPANY = 'AUGEN' AND PACKNUM = " & txtpacknum.Text & "  AND PARTNUM = '" & part & "' AND BINNUM = '" & bin & "' "
                        Cmd = New SqlClient.SqlCommand(stR, MyCnnLocal)
                        Cmd.ExecuteNonQuery()

                        TranLocal.Commit()
                        TranServer.Commit()
                    Catch ex As Exception
                        TranLocal.Rollback()
                        TranServer.Rollback()
                    Finally
                        MyCnnServer.Close()
                        MyCnnLocal.Close()
                    End Try
            End Select



            Cmd.Dispose()
            Cmd = Nothing
            MyCnnLocal.Dispose()
            MyCnnLocal = Nothing
            MyCnnServer.Dispose()
            MyCnnServer = Nothing


        Catch ex As Exception
            Throw New Exception("Ocurrio un error al tratar de actualizar la tabla SHIPDTL, campo number01 y checkbox01" & vbCrLf & "Parte = " & part & vbCrLf & ex.Message)
        End Try
    End Sub

    Function Read_Registry(ByVal var As String) As String
        Dim key As RegistryKey
        Dim Result As String = ""
        key = Registry.LocalMachine.OpenSubKey("SOFTWARE").OpenSubKey("AUGEN")
        Select Case var
            Case "Plant"
                Result = My.Settings.Plant
            Case "TracerDataServer"
                Result = My.Settings.LocalServer
            Case "ServerAdd"
                Result = My.Settings.VantageServer
        End Select
        Return Result
    End Function

    Private Sub dgpackpend_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles dgpackpend.Click
        DGInd = dgpackpend.CurrentCell.RowIndex
    End Sub

    Private Sub dgpackpend_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles dgpackpend.DoubleClick
        Dim company As String = ""
        btninventory.Enabled = False
        txtpacknum.Text = dgpackpend.Item("Embarque", DGInd).Value.ToString
        NumLineas = dgpackpend.Item("Lineas", DGInd).Value.ToString
        Try : company = dgpackpend.Item("Company", DGInd).Value.ToString : Catch : End Try
        If company = "TRECEUX" Then
            BTN_TransferOrdersTRECEUX_Click(Me, e)
        ElseIf PO_OPTION = False Then
            btnFind_Click(Me, e)
        Else
            Me.btnFindPO_Click(Me, e)
        End If



    End Sub

    Private Sub Timer1_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Timer1.Tick
        Timer1.Enabled = False

        '***********************************************
        segundos = segundos + 1
        If segundos > 60 Then
            minutos = minutos + 1
            segundos = 1
        End If
        If segundos.ToString.Length = 1 Then
            dspseg = "0" & segundos.ToString
        Else
            dspseg = segundos.ToString
        End If
        '************************************************

        If ProcesaEmbarque.Yatermino Then
            lblsts.Visible = False
            txttime.Visible = False
            segundos = 1
            minutos = 0
            dspseg = ""
            progresbar.Visible = False
            ProcesaEmbarque = Nothing
            Timer1.Enabled = False
        Else
            txttime.Text = "Tiempo Transcurrido: " & minutos.ToString & ":" & dspseg & " / Tiempo Max.: 3:00"
            Application.DoEvents()
            progresbar.Value = Time
            Time = Time + 1
            If Time > 10 Then
                Time = 1
            End If
            Timer1.Enabled = True
        End If
    End Sub

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Timer1.Enabled = True
    End Sub

    Private Sub Button4_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Timer1.Enabled = False
    End Sub

    Private Sub Recibo_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        If e.KeyCode = Keys.Escape Then
            Me.Close()
        End If
    End Sub

    Private Sub Recibo_KeyUp(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyUp
        If e.KeyCode = Keys.Escape Then
            Me.Close()
        End If
    End Sub

    Private Sub Recibo_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        txtpacknum.Focus()
        txtpacknum.Select()
    End Sub

    Private Sub Button2_Click_1(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Dim main As New Form1
        main.ShowDialog()
    End Sub

    Private Sub btninventory_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btninventory.Click
        Dim consys As SqlDB
        Try
            Cursor = Cursors.WaitCursor
            Application.DoEvents()

            consys = New SqlDB(My.Settings.LocalServer, My.Settings.DBUser, My.Settings.DBPassword, My.Settings.LocalDBName)
            consys.OpenConn()

            Dim SqlStr As String
            DS = New DataSet

            SqlStr = " SELECT " & _
                    " VwCheckLabsInventory.partnum as parte, " & _
                    " VwCheckLabsInventory.onhandqty as qty," & _
                    " VwPartTRECEUX.partdescription as des " & _
                    " FROM VwCheckLabsInventory WITH(NOLOCK) " & _
                    " INNER JOIN VwPartTRECEUX WITH(NOLOCK) ON VwPartTRECEUX.partnum = VwCheckLabsInventory.partnum " & _
                    " INNER JOIN plant WITH(NOLOCK) ON (plant.defrcvwhse = VwCheckLabsInventory.warehousecode and plant.defrcvbin = VwCheckLabsInventory.binnum) " & _
                    " WHERE plant.plant = '" & Read_Registry("Plant") & "' AND plant.company = 'treceux' "

            DS = consys.SQLDS(SqlStr, "t1")
            If DS.Tables(0).Rows.Count > 0 Then
                DGInventario.Visible = True
                dgpackpend.Visible = False
                DGPacking.Visible = False
                DGEmbarque.Visible = False
                DGInventario.DataSource = DS.Tables("t1")
                DS.Dispose()
                DS = Nothing
            Else
                DGInventario.Visible = False
                dgpackpend.DataSource = ""
                MsgBox("No hay inventario...", MsgBoxStyle.Critical)
            End If
            consys.CloseConn()
            consys = Nothing
        Catch ex As Exception
            If ex.Message.Contains("Error de Conexion") Then
                Form1.ChangeWorkingStatus(Form1.WorkingStatusType.OffLine)
            Else
                MsgBox(ex.Message, MsgBoxStyle.Critical)
            End If
        Finally
            Cursor = Cursors.Default
        End Try
    End Sub

    Private Sub GetTouchScreenValue(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtpacknum.Click
        Dim t As New TextBox
        t = sender
        Dim kb As New TouchScreenKeyboard
        kb.Location = New Point((1024 - kb.Size.Width) / 2, 400)
        kb.ShowDialog()
        If kb.Status Then
            t.Text = kb.Value
        End If
        'teniendo el numero de recerta llamamos la funcion que valida el numero de orden
        Me.btnFind_Click(sender, e)
    End Sub

    Private Sub txtpacknum_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtpacknum.KeyDown
        If e.KeyCode = 13 Then
            btnFind_Click(Me, e)
        End If
        If e.KeyCode = Keys.Escape Then
            Me.Close()
        End If
    End Sub

    Private Sub DGEmbarque_CellEndEdit(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles DGEmbarque.CellEndEdit
        'modificamos la clolunma cero -> Modificada que equivale a decir que la cantidad de esa celda se modifico
        DGEmbarque.Item(0, e.RowIndex).Value = 1
        DGEmbarque.Item(3, e.RowIndex).Style.BackColor = Color.Red
        DGEmbarque.Item(2, e.RowIndex).Style.BackColor = Color.Red
        DGEmbarque.Item(1, e.RowIndex).Style.BackColor = Color.Red

        Crear_Log("Cambio de Cantidad: " & txtpacknum.Text & vbTab & DGEmbarque.Item(1, e.RowIndex).Value & vbTab & DGEmbarque.Item(3, e.RowIndex).Value & vbTab & Now())
    End Sub

    ' Funcion que genera un log si hubo un error al generar una orden de venta
    Private Sub Crear_Log(ByVal msg As String)
        Dim obj As File
        Dim str As System.IO.StreamWriter
        str = File.AppendText(System.Windows.Forms.Application.StartupPath & "\RecibosLog.txt")

        str.WriteLine(msg)
        str.Close()
        str.Dispose()
        obj = Nothing
    End Sub

    ''' <summary>
    ''' Inserta Localmente los registros en la tabla TfShipDtl (Detalles de trasnferencias TRECEUX)
    ''' </summary>
    ''' <param name="pedido">Dataset de los detalles de la transferencia.</param>
    ''' <param name="company">Compañia de donde se hace la trasnferencia.</param>
    ''' <param name="packnum">Embarque al que se le dara recepcion.</param>
    ''' <remarks></remarks>
    Private Sub InsertLocalTfShipDtl(ByVal pedido As DataSet, ByVal company As String, ByVal packnum As String)
        Dim resCmd As SqlClient.SqlDataReader = Nothing
        Dim sqlText, filtro As String

        ' Objetos para establecer la conexion con la tabla localPack de la BD remota MES
        Dim cnPack As OleDbConnection = Nothing
        Dim daPack As OleDbDataAdapter = Nothing
        Dim dtPack As DataTable
        Dim drPack As DataRow

        ' Objetos para establecer la conexion con la tabla ShipDtl de la BD Local
        Dim TfShipDtlConnStr As OleDbConnection = Nothing
        Dim TfShipDtlAdapter As OleDbDataAdapter = Nothing
        Dim cmdBuilder As OleDbCommandBuilder
        Dim TfShipDtlDS As DataSet
        Dim TfShipDtlTable As DataTable
        Dim TfShipDtlRow, drows() As DataRow

        ' Cadena de conexion para la BD Local
        Dim sqlLocalConn As String = "Provider=SQLOLEDB.1; " & _
            "Password=" & My.Settings.DBPassword & ";" & _
            "Persist Security Info=True;" & _
            "User ID=" & My.Settings.DBUser & ";" & _
            "Initial Catalog=" & My.Settings.LocalDBName & ";" & _
            "Data Source=" & My.Settings.LocalServer

        Try
            ' Dataset del detalle del embarque (shipdtl del Servidor)
            dtPack = pedido.Tables("t1")

            ' Se inicializan los objetos de acceso y manipulacion de registros de la BD del servidor
            ' de forma local a la aplicacion
            TfShipDtlConnStr = New OleDbConnection(sqlLocalConn)
            TfShipDtlConnStr.Open()
            sqlText = "SELECT * FROM TfShipDtl WITH(NOLOCK) WHERE (company='" & company & "') AND (packnum=" & packnum & ")"
            TfShipDtlAdapter = New OleDbDataAdapter(sqlText, TfShipDtlConnStr)
            TfShipDtlDS = New DataSet
            cmdBuilder = New OleDbCommandBuilder(TfShipDtlAdapter)

            ' Las sig. 2 lineas inicialian los comandos de insercion y actualizacion del dataset de la tabla ShipDtl
            TfShipDtlAdapter.InsertCommand = cmdBuilder.GetInsertCommand
            TfShipDtlAdapter.UpdateCommand = cmdBuilder.GetUpdateCommand
            TfShipDtlAdapter.FillSchema(TfShipDtlDS, SchemaType.Source, "TfShipDtl")
            TfShipDtlAdapter.Fill(TfShipDtlDS, "TfShipDtl")
            TfShipDtlConnStr.Close()

            ' Referencia a la tabla ShipDtl
            TfShipDtlTable = TfShipDtlDS.Tables("TfShipDtl")

            For Each drPack In dtPack.Rows

                filtro = "partnum = '" & drPack("partnum") & "'"

                drows = TfShipDtlTable.Select(filtro)

                ' Si no existe un registro se agrega
                If drows.Length = 0 Then

                    TfShipDtlRow = TfShipDtlTable.NewRow()

                    ' Asigna valores a las columnas
                    TfShipDtlRow("packnum") = drPack("packnum")
                    TfShipDtlRow("shipdate") = drPack("shipdate")
                    TfShipDtlRow("plant") = drPack("plant")
                    TfShipDtlRow("toplant") = drPack("toplant")
                    TfShipDtlRow("packline") = drPack("packline")
                    TfShipDtlRow("partnum") = drPack("partnum")
                    TfShipDtlRow("linedesc") = drPack("linedesc")
                    TfShipDtlRow("ourstockshippedqty") = drPack("ourinventoryshipqty")
                    TfShipDtlRow("tfordnum") = drPack("tfordnum")
                    TfShipDtlRow("tflinenum") = drPack("tflinenum")
                    TfShipDtlRow("company") = drPack("company")
                    TfShipDtlRow("number02") = drPack("number02")
                    TfShipDtlRow("checkbox01") = drPack("checkbox01")

                    TfShipDtlTable.Rows.Add(TfShipDtlRow)
                Else
                    drows(0)("packnum") = drPack("packnum")
                    drows(0)("shipdate") = drPack("shipdate")
                    drows(0)("plant") = drPack("plant")
                    drows(0)("toplant") = drPack("toplant")
                    drows(0)("packline") = drPack("packline")
                    drows(0)("partnum") = drPack("partnum")
                    drows(0)("linedesc") = drPack("linedesc")
                    drows(0)("ourstockshippedqty") = drPack("ourinventoryshipqty")
                    drows(0)("tfordnum") = drPack("tfordnum")
                    drows(0)("tflinenum") = drPack("tflinenum")
                    drows(0)("company") = drPack("company")
                    drows(0)("number02") = drPack("number02")
                    drows(0)("checkbox01") = drPack("checkbox01")

                End If

            Next

            TfShipDtlConnStr.Open()
            TfShipDtlAdapter.Update(TfShipDtlDS, "TfShipDtl")
            TfShipDtlConnStr.Close()

        Catch excp As Exception
            'Throw New Exception("No se pudo guardar el embarque en la BD Local: " & excp.Message)
        End Try
    End Sub

    ' Esta funcion guarda en la tabla local shipdtl el detalle de un packing slip del servidor
    Private Sub InsertLocalShipDtl(ByVal pedido As DataSet, ByVal company As String, ByVal packnum As String)
        Dim resCmd As SqlClient.SqlDataReader = Nothing
        Dim sqlText, filtro As String

        ' Objetos para establecer la conexion con la tabla localPack de la BD remota MES
        Dim cnPack As OleDbConnection = Nothing
        Dim daPack As OleDbDataAdapter = Nothing
        Dim dtPack As DataTable
        Dim drPack As DataRow

        ' Objetos para establecer la conexion con la tabla ShipDtl de la BD Local
        Dim ShipDtlConnStr As OleDbConnection = Nothing
        Dim ShipDtlAdapter As OleDbDataAdapter = Nothing
        Dim cmdBuilder As OleDbCommandBuilder
        Dim ShipDtlDS As DataSet
        Dim ShipDtlTable As DataTable
        Dim ShipDtlRow, drows() As DataRow

        ' Cadena de conexion para la BD Local
        Dim sqlLocalConn As String = "Provider=SQLOLEDB.1; " & _
            "Password=" & My.Settings.DBPassword & ";" & _
            "Persist Security Info=True;" & _
            "User ID=" & My.Settings.DBUser & ";" & _
            "Initial Catalog=" & My.Settings.LocalDBName & ";" & _
            "Data Source=" & My.Settings.LocalServer

        Try
            ' Dataset del detalle del embarque (shipdtl del Servidor)
            dtPack = pedido.Tables("t1")

            ' Se inicializan los objetos de acceso y manipulacion de registros de la BD del servidor
            ' de forma local a la aplicacion
            ShipDtlConnStr = New OleDbConnection(sqlLocalConn)
            ShipDtlConnStr.Open()
            sqlText = "SELECT * FROM ShipDtl WITH(NOLOCK) WHERE (company='" & company & "') AND (packnum=" & packnum & ")"
            ShipDtlAdapter = New OleDbDataAdapter(sqlText, ShipDtlConnStr)
            ShipDtlDS = New DataSet
            cmdBuilder = New OleDbCommandBuilder(ShipDtlAdapter)

            ' Las sig. 2 lineas inicialian los comandos de insercion y actualizacion del dataset de la tabla ShipDtl
            ShipDtlAdapter.InsertCommand = cmdBuilder.GetInsertCommand
            ShipDtlAdapter.UpdateCommand = cmdBuilder.GetUpdateCommand
            ShipDtlAdapter.FillSchema(ShipDtlDS, SchemaType.Source, "ShipDtl")
            ShipDtlAdapter.Fill(ShipDtlDS, "ShipDtl")
            ShipDtlConnStr.Close()

            ' Referencia a la tabla ShipDtl
            ShipDtlTable = ShipDtlDS.Tables("ShipDtl")

            For Each drPack In dtPack.Rows

                filtro = "partnum = '" & drPack("partnum") & "' AND binnum = '" & drPack("binnum") & "'"

                drows = ShipDtlTable.Select(filtro)

                ' Si no existe un registro se agrega
                If drows.Length = 0 Then

                    ShipDtlRow = ShipDtlTable.NewRow()

                    ' Asigna valores a las columnas
                    ShipDtlRow("company") = drPack("company")
                    ShipDtlRow("packnum") = drPack("packnum")
                    ShipDtlRow("packline") = drPack("packline")
                    ShipDtlRow("ourinventoryshipqty") = drPack("ourinventoryshipqty")
                    ShipDtlRow("partnum") = drPack("partnum")
                    ShipDtlRow("linedesc") = drPack("linedesc")
                    ShipDtlRow("ium") = drPack("ium")
                    ShipDtlRow("number02") = drPack("number02")
                    ShipDtlRow("checkbox01") = drPack("checkbox01")
                    ShipDtlRow("binnum") = drPack("binnum")

                    ShipDtlTable.Rows.Add(ShipDtlRow)
                Else
                    drows(0)("company") = drPack("company")
                    drows(0)("packnum") = drPack("packnum")
                    drows(0)("packline") = drPack("packline")
                    drows(0)("ourinventoryshipqty") = drPack("ourinventoryshipqty")
                    drows(0)("partnum") = drPack("partnum")
                    drows(0)("linedesc") = drPack("linedesc")
                    drows(0)("ium") = drPack("ium")
                    drows(0)("number02") = drPack("number02")
                    drows(0)("checkbox01") = drPack("checkbox01")
                    drows(0)("binnum") = drPack("binnum")
                End If

            Next

            ShipDtlConnStr.Open()
            ShipDtlAdapter.Update(ShipDtlDS, "ShipDtl")
            ShipDtlConnStr.Close()

        Catch excp As Exception
            'Throw New Exception("No se pudo guardar el embarque en la BD Local: " & excp.Message)
        End Try
    End Sub

    ' Esta funcion guarda en la tabla local shipdtl el detalle de un packing slip del servidor
    Private Sub InsertLocalPODtl(ByVal pedido As DataSet, ByVal company As String, ByVal packnum As String)
        Dim resCmd As SqlClient.SqlDataReader = Nothing
        Dim sqlText, filtro As String

        ' Objetos para establecer la conexion con la tabla localPack de la BD remota MES
        Dim cnPack As OleDbConnection = Nothing
        Dim daPack As OleDbDataAdapter = Nothing
        Dim dtPack As DataTable
        Dim drPack As DataRow

        ' Objetos para establecer la conexion con la tabla ShipDtl de la BD Local
        Dim ShipDtlConnStr As OleDbConnection = Nothing
        Dim ShipDtlAdapter As OleDbDataAdapter = Nothing
        Dim cmdBuilder As OleDbCommandBuilder
        Dim ShipDtlDS As DataSet
        Dim ShipDtlTable As DataTable
        Dim ShipDtlRow, drows() As DataRow

        ' Cadena de conexion para la BD Local
        Dim sqlLocalConn As String = "Provider=SQLOLEDB.1; " & _
            "Password=" & My.Settings.DBPassword & ";" & _
            "Persist Security Info=True;" & _
            "User ID=" & My.Settings.DBUser & ";" & _
            "Initial Catalog=" & My.Settings.LocalDBName & ";" & _
            "Data Source=" & My.Settings.LocalServer

        Try
            ' Dataset del detalle del embarque (shipdtl del Servidor)
            dtPack = pedido.Tables("t1")

            ' Se inicializan los objetos de acceso y manipulacion de registros de la BD del servidor
            ' de forma local a la aplicacion
            ShipDtlConnStr = New OleDbConnection(sqlLocalConn)
            ShipDtlConnStr.Open()
            sqlText = "SELECT * FROM PoDetail WITH(NOLOCK) WHERE (company='" & company & "') AND (ponum=" & packnum & ")"
            ShipDtlAdapter = New OleDbDataAdapter(sqlText, ShipDtlConnStr)
            ShipDtlDS = New DataSet
            cmdBuilder = New OleDbCommandBuilder(ShipDtlAdapter)

            ' Las sig. 2 lineas inicialian los comandos de insercion y actualizacion del dataset de la tabla ShipDtl
            ShipDtlAdapter.InsertCommand = cmdBuilder.GetInsertCommand
            ShipDtlAdapter.UpdateCommand = cmdBuilder.GetUpdateCommand
            ShipDtlAdapter.FillSchema(ShipDtlDS, SchemaType.Source, "PoDetail")
            ShipDtlAdapter.Fill(ShipDtlDS, "PoDetail")
            ShipDtlConnStr.Close()

            ' Referencia a la tabla ShipDtl
            ShipDtlTable = ShipDtlDS.Tables("PoDetail")

            For Each drPack In dtPack.Rows

                filtro = "partnum = '" & drPack("partnum") & "'"

                drows = ShipDtlTable.Select(filtro)

                ' Si no existe un registro se agrega
                If drows.Length = 0 Then

                    ShipDtlRow = ShipDtlTable.NewRow()

                    ' Asigna valores a las columnas
                    ShipDtlRow("company") = drPack("company")
                    ShipDtlRow("ponum") = drPack("packnum")
                    ShipDtlRow("poline") = drPack("packline")
                    ShipDtlRow("ourqty") = drPack("ourinventoryshipqty")
                    ShipDtlRow("partnum") = drPack("partnum")
                    ShipDtlRow("linedesc") = drPack("linedesc")
                    ShipDtlRow("rcvqty") = -1

                    ShipDtlTable.Rows.Add(ShipDtlRow)
                Else
                    drows(0)("company") = drPack("company")
                    drows(0)("ponum") = drPack("packnum")
                    drows(0)("poline") = drPack("packline")
                    drows(0)("ourqty") = drPack("ourinventoryshipqty")
                    drows(0)("partnum") = drPack("partnum")
                    drows(0)("linedesc") = drPack("linedesc")
                    drows(0)("rcvqty") = -1     'drPack("rcvqty")
                End If

            Next

            ShipDtlConnStr.Open()
            ShipDtlAdapter.Update(ShipDtlDS, "podetail")
            ShipDtlConnStr.Close()

        Catch excp As Exception
            'Throw New Exception("No se pudo guardar el embarque en la BD Local: " & excp.Message)
        End Try
    End Sub

    Private Sub btnFindPO_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnFindPO.Click
        Dim consys As SqlDB
        Dim shiptonum As String
        PO_OPTION = True

        Try
            Me.Cursor = Cursors.WaitCursor

            consys = New SqlDB(My.Settings.VantageServer, My.Settings.DBUser, My.Settings.DBPassword, My.Settings.MfgSysDBName)
            consys.OpenConn()

            Dim SqlStr As String
            DS = New DataSet
            shiptonum = Laboratorio_shiptonum()

            If txtpacknum.Text = "" Or txtpacknum.Text = "0" Then
                DGPacking.Visible = False
                DGInventario.Visible = False
                DGEmbarque.Visible = False
                dgpackpend.Visible = True

                SqlStr = "SELECT fromcompany as company, orden as Embarque, COUNT(Parte) as Lineas, Fecha FROM VwPOsToLabs WITH(NOLOCK) WHERE Planta='" & My.Settings.Plant & "' GROUP BY fromcompany,orden,fecha ORDER BY orden"
                'SqlStr = "SELECT orden as Embarque, COUNT(Parte) as Lineas, Fecha FROM VwPOsToLabs WITH(NOLOCK) WHERE Planta='enslab' GROUP BY orden,fecha ORDER BY orden"
                DS = consys.SQLDS(SqlStr, "t1")

                If DS.Tables(0).Rows.Count > 0 Then
                    dgpackpend.Visible = True
                    dgpackpend.DataSource = DS.Tables("t1")
                    'DS.Dispose()
                    'DS = Nothing
                Else
                    dgpackpend.Visible = False
                    dgpackpend.DataSource = ""
                    MsgBox("No Existen Ordenes pendientes por recibir...", MsgBoxStyle.Critical)
                End If
            Else
                dgpackpend.Visible = False
                DGPacking.Visible = False

                SqlStr = "SELECT parte as partnum, descripcion as linedesc, cantidad as ourinventoryshipqty, compania as company, orden as packnum, linea as packline, '' as ium, 0 as number02, 0 as checkbox01, '' as binnum FROM VwPOsToLabs WITH(NOLOCK) WHERE orden = " & txtpacknum.Text & " AND planta = '" & My.Settings.Plant & "'"
                'SqlStr = "SELECT parte as partnum, descripcion as linedesc, cantidad as ourinventoryshipqty, compania as company, orden as packnum, linea as packline, '' as ium, 0 as number02, 0 as checkbox01, '' as binnum FROM VwPOsToLabs WITH(NOLOCK) WHERE orden = " & txtpacknum.Text & " AND planta = 'enslab'"
                DS = consys.SQLDS(SqlStr, "t1")

                If DS.Tables(0).Rows.Count > 0 Then
                    DGEmbarque.Visible = True
                    DGEmbarque.DataSource = DS.Tables("t1")
                    Button1.Enabled = True
                    InsertLocalPODtl(DS, "TRECEUX", Me.txtpacknum.Text)
                    'DS.Dispose()
                    'DS = Nothing
                Else
                    Limpia_DG()
                    MsgBox("El numero de orden no puede ser recibido por las sig. razones:" & vbCrLf & "1. No existe" & vbCrLf & "2. Ya fue recibido" & vbCrLf & "3. No pertecene a este laboratorio", MsgBoxStyle.Critical)

                    Button1.Enabled = False
                    txtpacknum.Text = ""
                    txtpacknum.Focus()

                End If

            End If
            consys.CloseConn()
            consys = Nothing

        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical)
        Finally
            Me.Cursor = Cursors.Default
        End Try
    End Sub

    Private Sub BTN_TransferOrdersTRECEUX_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BTN_TransferOrdersTRECEUX.Click
        Dim consys As SqlDB
        '       Dim shiptonum As String
        PO_OPTION = False

        Try
            Me.Cursor = Cursors.WaitCursor
            Application.DoEvents()

            If Form1.WorkingOnLine Or Form1.Ping(My.Settings.AugenServer) Then

                consys = New SqlDB(My.Settings.MFGSysConnection)
                consys.OpenConn()

                Dim SqlStr As String
                DS = New DataSet
                '                shiptonum = Laboratorio_shiptonum()
                If txtpacknum.Text = "" Or txtpacknum.Text = "0" Then
                    DGPacking.Visible = False
                    DGInventario.Visible = False
                    DGEmbarque.Visible = False
                    dgpackpend.Visible = True

                    SqlStr = "select distinct company,packnum as Embarque,count(partnum) as Lineas, shipdate as FECHA from VwTransferOrdersTRECEUX with(nolock) where toplant = '" & My.Settings.Plant & "' group by company,packnum, shipdate order by packnum"
                    DS = consys.SQLDS(SqlStr, "t1")
                    If DS.Tables(0).Rows.Count > 0 Then
                        dgpackpend.Visible = True
                        dgpackpend.DataSource = DS.Tables("t1")
                        DGEmbarque.DataSource = DS.Tables("t1")
                        DS.Dispose()
                        DS = Nothing
                    Else
                        dgpackpend.Visible = False
                        dgpackpend.DataSource = ""
                        MsgBox("No Existen embarques pendientes por recibir...", MsgBoxStyle.Critical)
                    End If
                Else
                    dgpackpend.Visible = False
                    DGPacking.Visible = False

                    Dim temp As New DataSet
                    SqlStr = "select partnum, linedesc, ourstockshippedqty as ourinventoryshipqty, company from VwTransferOrdersTRECEUX WITH(NOLOCK) Where packnum = " & txtpacknum.Text & " and toplant = '" & My.Settings.Plant & "'"
                    temp = consys.SQLDS(SqlStr, "t1")
                    SqlStr = "select partnum, linedesc, ourstockshippedqty as ourinventoryshipqty, packnum, packline,* from VwTransferOrdersTRECEUX WITH(NOLOCK) Where packnum = " & txtpacknum.Text & " and toplant = '" & My.Settings.Plant & "'"
                    DS = consys.SQLDS(SqlStr, "t1")
                    If DS.Tables(0).Rows.Count > 0 Then
                        DGEmbarque.Visible = True
                        DGEmbarque.DataSource = temp.Tables("t1")
                        Button1.Enabled = True
                        InsertLocalTfShipDtl(DS, "TRECEUX", Me.txtpacknum.Text)
                        DS.Dispose()
                        DS = Nothing
                    Else
                        Limpia_DG()
                        MsgBox("El numero de embarque no puede ser recibido por las sig. razones:" & vbCrLf & "1. No existe" & vbCrLf & "2. Ya fue recibido" & vbCrLf & "3. No pertecene a este laboratorio", MsgBoxStyle.Critical)

                        Button1.Enabled = False
                        txtpacknum.Text = ""
                        txtpacknum.Focus()

                    End If

                End If
                consys.CloseConn()
                consys = Nothing
            Else
                Me.Close()
            End If
        Catch ex As Exception
            If ex.Message.Contains("Error de Conexion") Then
                Form1.ChangeWorkingStatus(Form1.WorkingStatusType.OffLine)
            Else
                MsgBox(ex.Message, MsgBoxStyle.Critical)
            End If
            Me.Close()
        Finally
            Me.Cursor = Cursors.Default
        End Try

    End Sub

    Private Sub txtpacknum_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtpacknum.TextChanged

    End Sub
End Class

Public Class EjecutarPackingSlip
    Private VarYa As Boolean
    Public Packnum As String
    Private params As String
    Public Company As String

    Public Property Yatermino() As Boolean
        Get
            Return VarYa
        End Get
        Set(ByVal value As Boolean)
            VarYa = value
        End Set
    End Property

    Public Sub setParams(ByVal p As String)
        params = p
    End Sub


    ' Se esta actualizando por Fco Garcia para trabajar localmente los inventarios
    Public Sub Guardar_PO()

        Dim conn As New SqlClient.SqlConnection(My.Settings.MFGSysConnection)
        Dim connLocal As New SqlClient.SqlConnection(Laboratorios.ConnStr)

        Dim MfgTran As SqlClient.SqlTransaction = Nothing
        Dim LocalTran As SqlClient.SqlTransaction = Nothing

        Try
            ' *********** SE ACTUALIZAN LOS INVENTARIOS LOCALES ***************
            connLocal.Open()
            LocalTran = connLocal.BeginTransaction

            Dim CommLocal As New SqlClient.SqlCommand("RcvPoEntryLabs_Local", connLocal)
            CommLocal.CommandTimeout = My.Settings.DBCommandTimeout * 5
            CommLocal.Transaction = LocalTran
            CommLocal.CommandType = Data.CommandType.StoredProcedure

            ' El parametro company tiene que ser variable porque la compañía puede ser AUGEN o BLUECOVE
            CommLocal.Parameters.AddWithValue("company", "TRECEUX")
            CommLocal.Parameters.AddWithValue("plant", My.Settings.Plant)
            CommLocal.Parameters.AddWithValue("params", params)
            CommLocal.Parameters.AddWithValue("ponum", Packnum)


            Dim returnparamLocal As New SqlClient.SqlParameter
            returnparamLocal.Direction = ParameterDirection.ReturnValue
            CommLocal.Parameters.Add(returnparamLocal)

            CommLocal.ExecuteNonQuery()
            Yatermino = False

            ' Verificamos si se ejecuto correctamente el Store Procedure en el Servidor
            If returnparamLocal.Value = 0 Then
                conn.Open()
                MfgTran = conn.BeginTransaction

                '************************************************************
                Dim Comm As New SqlClient.SqlCommand("RcvPoEntryLabs_Svr", conn)
                Comm.CommandTimeout = My.Settings.DBCommandTimeout * 5
                Comm.Transaction = MfgTran
                Comm.CommandType = Data.CommandType.StoredProcedure

                ' El parametro company tiene que ser variable porque la compañía puede ser AUGEN o BLUECOVE
                Comm.Parameters.AddWithValue("company", "TRECEUX")
                Comm.Parameters.AddWithValue("plant", My.Settings.Plant)
                Comm.Parameters.AddWithValue("params", params)
                Comm.Parameters.AddWithValue("ponum", Packnum)

                Dim returnparam As New SqlClient.SqlParameter
                returnparam.Direction = ParameterDirection.ReturnValue
                Comm.Parameters.Add(returnparam)

                Comm.ExecuteNonQuery()

                Yatermino = True

                ' Verificamos si se actualizo correctamente el inventario
                If returnparam.Value = 0 Then
                    MsgBox("Orden de Compra " & Packnum & " Guardada con Exito!", MsgBoxStyle.Information)
                End If

                MfgTran.Commit()
                LocalTran.Commit()
            End If


        Catch ex As Exception
            If Not MfgTran Is Nothing Then MfgTran.Rollback()
            If Not LocalTran Is Nothing Then LocalTran.Rollback()

            Yatermino = True
            MsgBox("Excepción!! No fue posible guardar el embarque número: " & Packnum & vbCrLf & ex.Message, MsgBoxStyle.Critical)
        Finally
            connLocal.Close()
            conn.Close()
        End Try
    End Sub

    ' Se esta actualizando por Fco Garcia para trabajar localmente los inventarios
    Public Sub Guardar_Embarque()

        Dim conn As New SqlClient.SqlConnection(My.Settings.MFGSysConnection)
        Dim connLocal As New SqlClient.SqlConnection(Laboratorios.ConnStr)

        Dim MfgTran As SqlClient.SqlTransaction = Nothing
        Dim LocalTran As SqlClient.SqlTransaction = Nothing

        Try
            ' *********** SE ACTUALIZAN LOS INVENTARIOS LOCALES ***************
            connLocal.Open()
            LocalTran = connLocal.BeginTransaction

            Dim CommLocal As SqlClient.SqlCommand
            Select Case Company
                Case "TRECEUX"
                    CommLocal = New SqlClient.SqlCommand("Rcv_Misc_Entry_Labs_TRECEUX_LOCAL", connLocal)
                Case Else
                    CommLocal = New SqlClient.SqlCommand("Rcv_Misc_Entry_Labs_LOCAL", connLocal)
            End Select

            CommLocal.CommandTimeout = My.Settings.DBCommandTimeout * 5
            CommLocal.Transaction = LocalTran
            CommLocal.CommandType = Data.CommandType.StoredProcedure

            ' El parametro company tiene que ser variable porque la compañía puede ser AUGEN o BLUECOVE
            CommLocal.Parameters.AddWithValue("company", Company)
            CommLocal.Parameters.AddWithValue("plant", Read_Registry("Plant"))
            CommLocal.Parameters.AddWithValue("packingNum", Packnum)

            Dim returnparamLocal As New SqlClient.SqlParameter
            returnparamLocal.Direction = ParameterDirection.ReturnValue
            CommLocal.Parameters.Add(returnparamLocal)

            CommLocal.ExecuteNonQuery()
            Yatermino = False

            ' Verificamos si se ejecuto correctamente el Store Procedure en el Servidor
            If returnparamLocal.Value = 0 Then
                conn.Open()
                MfgTran = conn.BeginTransaction

                '************************************************************
                Dim Comm As SqlClient.SqlCommand
                Select Case Company
                    Case "TRECEUX"
                        Comm = New SqlClient.SqlCommand("[Create_ReceiveTransferOrderTRECEUX]", conn)
                    Case Else
                        Comm = New SqlClient.SqlCommand("[Rcv_Misc_Entry_Labs_SVR]", conn)
                End Select
                Comm.CommandTimeout = My.Settings.DBCommandTimeout * 5
                Comm.Transaction = MfgTran
                Comm.CommandType = Data.CommandType.StoredProcedure

                ' El parametro company tiene que ser variable porque la compañía puede ser AUGEN o BLUECOVE
                Comm.Parameters.AddWithValue("company", Company)
                Comm.Parameters.AddWithValue("plant", Read_Registry("Plant"))
                Select Case Company
                    Case "TRECEUX"
                        Comm.Parameters.AddWithValue("packnum", Packnum)
                        Comm.Parameters.AddWithValue("entryperson", My.User.Name)
                    Case Else
                        Comm.Parameters.AddWithValue("packingNum", Packnum)
                End Select

                Dim returnparam As New SqlClient.SqlParameter
                returnparam.Direction = ParameterDirection.ReturnValue
                Comm.Parameters.Add(returnparam)

                Comm.ExecuteNonQuery()

                Yatermino = True

                ' Verificamos si se actualizo correctamente el inventario

                MfgTran.Commit()
                LocalTran.Commit()
                If returnparam.Value = 0 Then
                    MsgBox("Embarque Número: " & Packnum & " Guardado con Exito!", MsgBoxStyle.Information)
                End If
            End If


        Catch ex As Exception
            If Not MfgTran Is Nothing Then MfgTran.Rollback()
            If Not LocalTran Is Nothing Then LocalTran.Rollback()

            Yatermino = True
            MsgBox("Excepción!! No fue posible guardar el embarque número: " & Packnum & vbCrLf & ex.Message, MsgBoxStyle.Critical)
        Finally
            connLocal.Close()
            conn.Close()
        End Try
    End Sub
    Public Sub Guardar_Embarque_TRECEUX()

        Dim conn As New SqlClient.SqlConnection(My.Settings.MFGSysConnection)
        Dim connLocal As New SqlClient.SqlConnection(Laboratorios.ConnStr)

        Dim MfgTran As SqlClient.SqlTransaction = Nothing
        Dim LocalTran As SqlClient.SqlTransaction = Nothing

        Try
            ' *********** SE ACTUALIZAN LOS INVENTARIOS LOCALES ***************
            connLocal.Open()
            LocalTran = connLocal.BeginTransaction

            Dim CommLocal As New SqlClient.SqlCommand("Rcv_Misc_Entry_Labs_TRECEUX_LOCAL", connLocal)
            CommLocal.CommandTimeout = My.Settings.DBCommandTimeout * 5
            CommLocal.Transaction = LocalTran
            CommLocal.CommandType = Data.CommandType.StoredProcedure

            ' El parametro company tiene que ser variable porque la compañía puede ser AUGEN o BLUECOVE
            CommLocal.Parameters.AddWithValue("company", "TRECEUX")
            CommLocal.Parameters.AddWithValue("plant", Read_Registry("Plant"))
            CommLocal.Parameters.AddWithValue("packingNum", Packnum)

            Dim returnparamLocal As New SqlClient.SqlParameter
            returnparamLocal.Direction = ParameterDirection.ReturnValue
            CommLocal.Parameters.Add(returnparamLocal)

            CommLocal.ExecuteNonQuery()
            Yatermino = False

            ' Verificamos si se ejecuto correctamente el Store Procedure en el Servidor
            If returnparamLocal.Value = 0 Then
                conn.Open()
                MfgTran = conn.BeginTransaction

                '************************************************************
                Dim Comm As New SqlClient.SqlCommand("[Rcv_Misc_Entry_Labs_TRECEUX_SVR]", conn)
                Comm.CommandTimeout = My.Settings.DBCommandTimeout * 5
                Comm.Transaction = MfgTran
                Comm.CommandType = Data.CommandType.StoredProcedure

                ' El parametro company tiene que ser variable porque la compañía puede ser AUGEN o BLUECOVE
                Comm.Parameters.AddWithValue("company", "TRECEUX")
                Comm.Parameters.AddWithValue("plant", Read_Registry("Plant"))
                Comm.Parameters.AddWithValue("packingNum", Packnum)

                Dim returnparam As New SqlClient.SqlParameter
                returnparam.Direction = ParameterDirection.ReturnValue
                Comm.Parameters.Add(returnparam)

                Comm.ExecuteNonQuery()

                Yatermino = True

                ' Verificamos si se actualizo correctamente el inventario
                If returnparam.Value = 0 Then
                    MsgBox("Embarque Número: " & Packnum & " Guardado con Exito!", MsgBoxStyle.Information)
                End If

                MfgTran.Commit()
                LocalTran.Commit()
            End If


        Catch ex As Exception
            If Not MfgTran Is Nothing Then MfgTran.Rollback()
            If Not LocalTran Is Nothing Then LocalTran.Rollback()

            Yatermino = True
            MsgBox("Excepción!! No fue posible guardar el embarque número: " & Packnum & vbCrLf & ex.Message, MsgBoxStyle.Critical)
        Finally
            connLocal.Close()
            conn.Close()
        End Try
    End Sub


    Function Read_Registry(ByVal var As String) As String
        Dim key As RegistryKey
        Dim Result As String = ""
        key = Registry.LocalMachine.OpenSubKey("SOFTWARE").OpenSubKey("AUGEN")
        Select Case var
            Case "Plant"
                Result = My.Settings.Plant
            Case "TracerDataServer"
                Result = My.Settings.LocalServer
            Case "ServerAdd"
                Result = My.Settings.VantageServer
        End Select
        Return Result
    End Function

    Public Sub New(ByVal pack As String)
        Packnum = pack
        Yatermino = False
    End Sub
    
End Class