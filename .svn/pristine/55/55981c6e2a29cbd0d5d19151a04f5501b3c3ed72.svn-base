Imports Microsoft.Win32
Public Class CloseOrder
    Dim Ordernum, jobnum As String
    Dim DSOrdenes As DataSet
    Dim MsgError As String
    Private Sub GetTouchScreenValue(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RxID.Click
        'Dim t As New TextBox
        't = sender
        'Dim kb As New TouchScreenKeyboard
        'kb.Location = New Point((1024 - kb.Size.Width) / 2, 400)
        'kb.ShowDialog()
        'If kb.Status Then
        '    t.Text = kb.Value
        'End If
        ''teniendo el numero de recerta llamamos la funcion que valida el numero de orden
        'Show_OrderInfo()
    End Sub

    Public Sub Show_OrderInfo()
        'Form1.Ping()
        Try
            If Form1.WorkingOnLine Then
                If Existe_Orden() = True Then 'si existe la orden para la referencia digital, entonce la podemos cerrar
                    btnclose.Enabled = True
                Else
                    Limpiar()
                    btnclose.Enabled = False
                    MsgBox("Esta Receta no contiene una orden para cerrar ó la orden ya fue cerrada", MsgBoxStyle.Critical)
                End If
            Else
                Me.Close()
            End If
        Catch ex As Exception
            'Form1.Ping()
            MsgBox(ex.Message, MsgBoxStyle.Critical)
            Me.Close()
        End Try
    End Sub
    Public Function Existe_Orden() As Boolean
        'si la orden existe, desplegamos la informacion y guardamos en las variables el ordenum y jobnum para 
        'cerrarlos si el usuario desea
        Dim existe As Boolean
        Dim SqlCon As SqlDB
        Dim SqlStr As String
        Dim row As DataRow

        Try
            If RxID.Text <> "" Then
                SqlCon = New SqlDB(My.Settings.VantageServer, My.Settings.DBUser, My.Settings.DBPassword, My.Settings.MfgSysDBName)
                SqlCon.OpenConn()
                SqlStr = "SELECT distinct(ordernum), orderdate,materialid,diseñoid,CUSTNUM FROM VwRecetasTRECEUX with(nolock) " & _
                         " where refdigital = " & RxID.Text & " AND (ordernum like '" & Cve_Lab() & "%')" & _
                         " group by ordernum,orderdate,materialid,diseñoid,CUSTNUM;"

                'SqlStr = "SELECT distinct(ordernum), orderhed.ORDERNUM,orderhed.orderdate,orderhed.SHORTChar07,SHORTCHAR08,orderhed.CUSTNUM FROM ORDERHED with(nolock) " & _
                '         " INNER JOIN VwRecetasTRECEUX ON VwRecetasTRECEUX.ordernum = orderhed.ordernum " & _
                '         " WHERE orderhed.COMPANY = 'TRECEUX' " & _
                '         " AND orderhed.NUMBER01 = " & RxID.Text & " AND (VwRecetasTRECEUX.plant = " & Cve_Lab() & " or VwRecetasTRECEUX.plant2 = " & Cve_Lab() & ") group by (ordernum)"
                DSOrdenes = New DataSet
                DSOrdenes = SqlCon.SQLDS(SqlStr, "T1")
                SqlCon.CloseConn()
                'VERIFICAMOS SI EL DATASET CONTIENE INFORMACION
                If DSOrdenes.Tables(0).Rows.Count > 0 Then
                    lstOrdenes.Items.Clear()
                    If DSOrdenes.Tables(0).Rows.Count = 1 Then

                        btnclose.Location = New Point(19, 252)
                        btncancel.Location = New Point(195, 253)
                        GroupBox1.Location = New Point(19, 59)
                        Me.Size = New Size(517, 344)

                        row = DSOrdenes.Tables(0).Rows(0)
                        Ordernum = row("ordernum")
                        jobnum = BuscaJob(Ordernum)
                        txtfecha.Text = row("orderdate")
                        txtcte.Text = BuscaCliente(row("custnum"))
                        txtmat.Text = BuscaMaterial(row("materialid"))
                        txtdis.Text = BuscaDiseño(row("diseñoid"))
                        SqlCon.OpenConn()
                        Dim dsprecio As DataSet = SqlCon.SQLDS("select SubTotal from VwRxJob where ordernum = '" & Ordernum & "'", "t1")
                        TextPrecio.Text = Convert.ToSingle(dsprecio.Tables("t1").Rows(0).Item("SubTotal")).ToString("c")
                        SqlCon.CloseConn()

                    Else
                        GroupBox1.Location = New Point(17, 164)
                        Me.Size = New Size(519, 438)
                        btnclose.Location = New Point(19, 354)
                        btncancel.Location = New Point(195, 354)
                        Label6.Location = New Point(159, 59)
                        For Each row In DSOrdenes.Tables(0).Rows
                            lstOrdenes.Items.Add(row("ordernum"))
                        Next
                        lstOrdenes.SelectedIndex = 0
                        Label6.Text = "Seleccione un Numero de Orden."
                    End If
                    existe = True
                Else
                    'ANALIZAMOS PORQUE LA RECETA NO SE PUEDE CERRAR
                    'VerificaRX()
                    existe = False
                End If
                SqlCon = Nothing
            End If
            Return existe
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function
    'Private Sub VerificaRX()
    '    Dim MyCnn As SqlClient.SqlConnection
    '    Dim Cmd As SqlClient.SqlCommand
    '    Dim Rdr As SqlClient.SqlDataReader
    '    Dim str As String
    '    Try
    '        MyCnn = New SqlClient.SqlConnection("user ID=sa;password=proliant01;database=LocalLab2000;server=" & Read_Registry("TracerDataServer") & ";Connect Timeout=30")
    '        MyCnn.Open()
    '        str = "select checkbox06 as facturado,checkbox07 as cobrado from orderhed with(nolock) " & _
    '              "where number01 = " & RxID.Text & " and  "
    '        Cmd = New SqlClient.SqlCommand(Str, MyCnn)
    '        Rdr = Cmd.ExecuteReader()
    '        If Rdr.HasRows Then

    '        End If
    '    Catch ex As Exception

    '    End Try
    'End Sub
    Public Function BuscaCliente(ByVal cte As String) As String
        Dim SqlCon As SqlDB
        Dim DataSet As New DataSet
        Dim SqlStr, Cliente As String
        Dim row As DataRow

        Try
            SqlCon = New SqlDB(My.Settings.VantageServer, My.Settings.DBUser, My.Settings.DBPassword, My.Settings.MfgSysDBName)
            SqlCon.OpenConn()
            SqlStr = "SELECT  name from customer where company = 'TRECEUX' and custnum = " & cte & " "
            DataSet = SqlCon.SQLDS(SqlStr, "T1")
            SqlCon.CloseConn()
            SqlCon = Nothing
            'VERIFICAMOS SI EL DATASET CONTIENE INFORMACION
            If DataSet.Tables(0).Rows.Count > 0 Then
                row = DataSet.Tables(0).Rows(0)
                Cliente = row("name")
            Else
                Cliente = "?"
            End If
            Return Cliente
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function
    Public Function BuscaMaterial(ByVal mat As String) As String
        Dim SqlCon As SqlDB
        Dim DataSet As New DataSet
        Dim SqlStr, Material As String
        Dim row As DataRow

        Try
            SqlCon = New SqlDB(My.Settings.LocalServer, My.Settings.DBUser, My.Settings.DBPassword, My.Settings.LocalDBName)
            SqlCon.OpenConn()
            SqlStr = "SELECT  material from TblMaterials where cl_mat = " & mat & " "
            DataSet = SqlCon.SQLDS(SqlStr, "T1")
            SqlCon.CloseConn()
            SqlCon = Nothing
            'VERIFICAMOS SI EL DATASET CONTIENE INFORMACION
            If DataSet.Tables(0).Rows.Count > 0 Then
                row = DataSet.Tables(0).Rows(0)
                Material = row("Material")
            Else
                Material = "?"
            End If
            Return Material
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function

    Public Function BuscaDiseño(ByVal dis As String) As String
        Dim SqlCon As SqlDB
        Dim DataSet As New DataSet
        Dim SqlStr, diseno As String
        Dim row As DataRow

        Try
            SqlCon = New SqlDB(My.Settings.LocalServer, My.Settings.DBUser, My.Settings.DBPassword, My.Settings.LocalDBName)
            SqlCon.OpenConn()
            SqlStr = "SELECT  diseno from TblDesigns where cl_diseno = " & dis & " "
            DataSet = SqlCon.SQLDS(SqlStr, "T1")
            SqlCon.CloseConn()
            SqlCon = Nothing
            'VERIFICAMOS SI EL DATASET CONTIENE INFORMACION
            If DataSet.Tables(0).Rows.Count > 0 Then
                row = DataSet.Tables(0).Rows(0)
                diseno = row("diseno")
            Else
                diseno = "?"
            End If
            Return diseno
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function
    Public Function BuscaJob(ByVal order As String) As String
        Dim SqlCon As SqlDB
        Dim DataSet As New DataSet
        Dim SqlStr, job As String
        Dim row As DataRow

        Try
            SqlCon = New SqlDB(My.Settings.VantageServer, My.Settings.DBUser, My.Settings.DBPassword, My.Settings.MfgSysDBName)
            SqlCon.OpenConn()
            SqlStr = "SELECT  jobnum from jobhead where company = 'TRECEUX' and number01 = " & order & " "
            DataSet = SqlCon.SQLDS(SqlStr, "T1")
            SqlCon.CloseConn()
            SqlCon = Nothing
            'VERIFICAMOS SI EL DATASET CONTIENE INFORMACION
            If DataSet.Tables(0).Rows.Count > 0 Then
                row = DataSet.Tables(0).Rows(0)
                job = row("jobnum")
            Else
                job = "?"
            End If
            Return job
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
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
        End Select
        Return Result
    End Function
    Private Sub btnclose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnclose.Click
        Try
            'CerrarJob()
            If CerrarOrder() Then

                Limpiar()
                MsgBox("Orden Cerrada con exito!!!", MsgBoxStyle.Information)
                RxID.Text = ""
                RxID.Select()
                Form1.pe.ActualizaProcesos_Click(Me, New EventArgs)
            Else
                MsgBox("La receta no pudo ser cerrada" & vbCrLf & MsgError, MsgBoxStyle.Critical)
            End If
            'AjustarInventario()
            
        Catch ex As Exception
            MsgBox("Ocurrio un error al cerrar la orden!" & vbCrLf & ex.Message, MsgBoxStyle.Critical)
        End Try
    End Sub
    Private Sub AjustarInventario()
        Dim SqlCon As SqlDB
        Dim SqlStr As String
        Dim row As DataRow
        Try
            SqlCon = New SqlDB(My.Settings.VantageServer, My.Settings.DBUser, My.Settings.DBPassword, My.Settings.MfgSysDBName)
            SqlCon.OpenConn()
            SqlStr = "select * from VwOpenLinesByOrder with(nolock) where company = 'TRECEUX' and ordernum = " & Ordernum & " "
            DSOrdenes = New DataSet
            DSOrdenes = SqlCon.SQLDS(SqlStr, "T1")
            SqlCon.CloseConn()
            SqlCon = Nothing
            'VERIFICAMOS SI EL DATASET CONTIENE INFORMACION
            If DSOrdenes.Tables(0).Rows.Count > 0 Then
                For Each row In DSOrdenes.Tables(0).Rows

                Next
            Else
            End If
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Sub
    Private Sub Limpiar()
        RxID.Text = ""
        txtfecha.Text = ""
        txtcte.Text = ""
        txtmat.Text = ""
        txtdis.Text = ""
        TextPrecio.Text = ""
        btnclose.Enabled = False
        btnclose.Location = New Point(19, 252)
        btncancel.Location = New Point(195, 253)
        GroupBox1.Location = New Point(19, 59)
        Me.Size = New Size(517, 344)
    End Sub

    Private Sub CerrarJob()
        Dim comm As SqlClient.SqlCommand
        Dim Conn As SqlDB

        Try
            Conn = New SqlDB(My.Settings.VantageServer, My.Settings.DBUser, My.Settings.DBPassword, My.Settings.MfgSysDBName)
            Conn.OpenConn()
            comm = New SqlClient.SqlCommand("CloseJobEntry", Conn.SQLConn)
            comm.CommandType = Data.CommandType.StoredProcedure
            comm.Parameters.AddWithValue("@JobNum", jobnum)
            comm.ExecuteNonQuery()
            Conn.CloseConn()
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub
    Private Function CerrarOrder() As Boolean
        Dim comm As SqlClient.SqlCommand
        Dim Conn As SqlDB
        Dim Param As New SqlClient.SqlParameter
        Try
            Conn = New SqlDB(My.Settings.VantageServer, My.Settings.DBUser, My.Settings.DBPassword, My.Settings.MfgSysDBName)
            Conn.OpenConn()
            comm = New SqlClient.SqlCommand("CerrarOrdenLaboratorio", Conn.SQLConn)
            comm.CommandType = Data.CommandType.StoredProcedure
            comm.Parameters.AddWithValue("@Ordernum", Ordernum)
            comm.Parameters.AddWithValue("@plant", Read_Registry("Plant"))
            Param.ParameterName = "@retval"
            Param.Direction = ParameterDirection.ReturnValue
            comm.Parameters.Add(Param)
            comm.ExecuteNonQuery()
            If comm.Parameters("@retval").Value = 0 Then
                Return True
            Else
                Select Case comm.Parameters("@retval").Value
                    Case -1
                        MsgError = "El Trabajo de produccion (Job) no existe para esta receta"
                    Case Else
                        MsgError = "Desconocido"
                End Select
                Return False
            End If
            Conn.CloseConn()
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Function

    Private Sub lstOrdenes_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lstOrdenes.SelectedIndexChanged
        Dim row As DataRow

        row = DSOrdenes.Tables(0).Rows(lstOrdenes.SelectedIndex)
        Ordernum = row("ordernum")
        jobnum = BuscaJob(Ordernum)
        txtfecha.Text = row("orderdate")
        txtcte.Text = BuscaCliente(row("custnum"))
        txtmat.Text = BuscaMaterial(row("materialid"))
        txtdis.Text = BuscaDiseño(row("diseñoid"))
    End Sub

    Private Sub btncancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btncancel.Click
        Me.Close()
    End Sub

    Private Sub RxID_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles RxID.KeyDown
        If RxID.Text <> "" And e.KeyCode = Keys.Enter Then
            Show_OrderInfo()
            RxID.Focus()
            RxID.SelectAll()
        End If
        If e.KeyCode = Keys.Escape Then
            Me.Close()
        End If
    End Sub

    'funcion que regresa la clave del laboratorio
    Private Function Cve_Lab() As Integer
        Dim SqlCon As SqlDB
        Dim DataSet As New DataSet
        Dim SqlStr As String
        Dim row As DataRow
        Dim cl_lab As Integer

        Try
            SqlCon = New SqlDB(My.Settings.LocalServer, My.Settings.DBUser, My.Settings.DBPassword, My.Settings.LocalDBName)
            SqlCon.OpenConn()
            SqlStr = "SELECT  cl_lab from TblLaboratorios where plant = '" & Read_Registry("Plant") & "' "
            DataSet = SqlCon.SQLDS(SqlStr, "T1")
            SqlCon.CloseConn()
            SqlCon = Nothing
            'VERIFICAMOS SI EL DATASET CONTIENE INFORMACION
            If DataSet.Tables(0).Rows.Count > 0 Then
                row = DataSet.Tables(0).Rows(0)
                cl_lab = row("cl_lab")
            Else
                cl_lab = 0
            End If
            Return cl_lab
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function
   

    Private Sub CloseOrder_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

    End Sub
End Class