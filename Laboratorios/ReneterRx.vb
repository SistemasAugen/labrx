Imports Microsoft.Win32

Public Class ReneterRx
    Dim Biselado, AR As Boolean
    Dim Armazon As Integer
    Private Sub RxID_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles RxID.KeyPress
        If e.KeyChar = Chr(13) Then
            If RxID.Text.Length > 1 Then
                Busca_Rx()
            End If
        End If
        If e.KeyChar = Chr(27) Then
            Me.Close()
        End If
    End Sub
    Private Sub Cancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Cancel.Click
        Me.Close()
    End Sub
    Private Sub Busca_Rx()
        Dim MyCnn As SqlClient.SqlConnection
        Dim Cmd As SqlClient.SqlCommand
        Dim Rdr As SqlClient.SqlDataReader
        Dim str As String

        Try
            'MyCnn = New SqlClient.SqlConnection("user ID=" & My.Settings.DBUser & ";password=" & My.Settings.DBPassword & ";database=" & My.Settings.MfgSysDBName & ";server=" & My.Settings.VantageServer & ";Connect Timeout=5")
            MyCnn = New SqlClient.SqlConnection(Laboratorios.ConnStr)
            MyCnn.Open()
            str = "select distinct(ordernum),customer.name,fechasalida,biselado,antireflejante,tipoarmazon,comentarios from vwrecetastreceux with(nolock) " & _
                  "inner join customer with(nolock) on customer.custnum = vwrecetastreceux.custnum and customer.company = 'TRECEUX' " & _
                  "where (left(ordernum,2) = " & Cve_Lab() & ") and (ordernum = " & RxID.Text & ")  and ((VwRecetasTRECEUX.inarlab = 0) or (VwRecetasTRECEUX.inarlab = 1 and VwRecetasTRECEUX.arlab = " & Cve_Lab() & ") or (VwRecetasTRECEUX.inarlab = 1 and VwRecetasTRECEUX.arlab <> " & Cve_Lab() & " and VwRecetasTRECEUX.receivedinlocallab = 1))"
            Cmd = New SqlClient.SqlCommand(str, MyCnn)
            Rdr = Cmd.ExecuteReader()
            If Rdr.HasRows Then
                While Rdr.Read()
                    txtordernum.Text = Rdr("ordernum")
                    txtcliente.Text = Rdr("name")
                    txtfechasalida.Text = Rdr("fechasalida")
                    Biselado = Rdr("biselado")
                    AR = Rdr("antireflejante")
                    Armazon = Rdr("tipoarmazon")
                    txtcomentario.Text = Rdr("comentarios")
                    OK.Enabled = True
                    txtcomentario.Focus()
                End While
            Else
                Rdr.Close()
                txtordernum.Text = ""
                txtcliente.Text = ""
                txtfechasalida.Text = ""
                Biselado = False
                AR = False
                OK.Enabled = False
                Armazon = 0

                'Aqui reviso el estatus de la orden si es que existe
                str = "select * from orderhed with(nolock) where ordernum = " & RxID.Text
                Cmd = New SqlClient.SqlCommand(str, MyCnn)
                Rdr = Cmd.ExecuteReader()
                If Rdr.HasRows Then
                    While Rdr.Read()
                        If Rdr("openorder") = 0 Then
                            MsgBox("La Rx ya esta cerrada", MsgBoxStyle.Exclamation)
                        ElseIf Rdr("checkbox08") = 1 Then
                            MsgBox("Rx ya está retenida", MsgBoxStyle.Exclamation)
                        ElseIf Rdr("checkbox10") = 1 Then
                            MsgBox("Rx ya recibida por el Laboratorio Virtual", MsgBoxStyle.Exclamation)
                        Else
                            MsgBox("Esta receta no existe en el sistema", MsgBoxStyle.Critical)
                        End If

                    End While


                Else
                    MsgBox("Esta receta no existe en el sistema.", MsgBoxStyle.Critical)
                End If

                RxID.Focus()
                RxID.SelectAll()
            End If
            Cmd.Dispose()
            MyCnn.Close()
            MyCnn.Dispose()
            MyCnn = Nothing
        Catch ex As Exception
            MsgBox("No fue posible desplegar el detalle de la orden." & vbCrLf & ex.Message, MsgBoxStyle.Critical)
        End Try
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
            Case "MainServer"
                'Result = CStr(key.GetValue("MainServer"))
                Result = My.Settings.VantageServer
        End Select
        Return Result
    End Function
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

    Private Sub ReneterRx_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        CMBDIAS.SelectedIndex = 0
    End Sub

    Private Sub OK_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OK.Click
        If txtcomentario.Text.Trim().Length > 0 Then
            RetenerRx()
        Else
            MsgBox("Favor de introducir Comentario.", MsgBoxStyle.Exclamation)
            'MsgBox("Favor de introducir la razon por la cual ha sido retenida.", MsgBoxStyle.Exclamation)
        End If
    End Sub
    Private Sub RetenerRx()
        Dim comm As SqlClient.SqlCommand
        Dim Conn As SqlDB
        Dim Param As New SqlClient.SqlParameter
        'Dim FechaSalida, FechaRealSalida, str As String
        Dim str As String
        'Dim fs As Date
        Dim Failed As Boolean = False
        Dim FailedMessage As String = ""
        Dim tran As SqlClient.SqlTransaction


        Conn = New SqlDB(Laboratorios.ConnStr) 'My.Settings.LocalServer, My.Settings.DBUser, My.Settings.DBPassword, My.Settings.LocalDBName)
        Conn.OpenConn()

        tran = Conn.SQLConn.BeginTransaction

        Try
            'fs = CDate(txtfechasalida.Text)
            'FechaSalida = DateAdd(DateInterval.Day, CInt(CMBDIAS.Text), fs)
            'FechaRealSalida = GetFechaHoraFinal(FechaSalida)


            str = "UPDATE orderhed SET ordercomment ='" & txtcomentario.Text & "', sincronizada = 0  WHERE ordernum = " & txtordernum.Text & " and company = 'TRECEUX' "
            'str = "UPDATE orderhed SET ordercomment ='" & txtcomentario.Text & "',checkbox08 = 1,sincronizada = 0, date01='" & FechaRealSalida & "'  WHERE ordernum = " & txtordernum.Text & " and company = 'TRECEUX' "


            'Conn = New SqlDB(My.Settings.VantageServer, My.Settings.DBUser, My.Settings.DBPassword, My.Settings.MfgSysDBName)

            Dim ds As DataSet = Conn.SQLDS("select shortchar01 as ponumber from orderhed with(nolock) where ordernum = " & txtordernum.Text, "t1", tran)
            Dim ponum As String = ""
            If ds.Tables("t1").Rows.Count > 0 Then
                ponum = ds.Tables("t1").Rows(0).Item("ponumber")
            End If

            comm = New SqlClient.SqlCommand(str, Conn.SQLConn)
            comm.Transaction = tran
            comm.ExecuteNonQuery()

            OK.Enabled = False
            RxID.Text = ""
            txtcomentario.Text = ""
            txtcliente.Text = ""
            RxID.Focus()
            'If My.Settings.EnableBitacora Then
            '    Dim log As New Bitacora(ponum, txtordernum.Text, Bitacora.Transactions.RETENIDA, Now.ToString, My.User.Name.ToString, txtcomentario.Text)
            '    If Not log.InsertIntoDatabase(Conn, tran) Then
            '        Throw New Exception(log.FailedStatus)
            '    End If
            'End If
            tran.Commit()

        Catch ex As Exception
            Failed = True
            FailedMessage = ex.Message
            tran.Rollback()
            'If ex.Message.Contains("Error de Conexion") Then
            '    Form1.ChangeWorkingStatus(Form1.WorkingStatusType.OffLine)
            'Else
            '    MsgBox(ex.Message, MsgBoxStyle.Critical)
            'End If
        Finally
            Conn.CloseConn()
            Conn = Nothing
            If Failed Then MsgBox(FailedMessage, MsgBoxStyle.Exclamation)
        End Try
    End Sub
    Private Function GetFechaHoraFinal(ByVal sdte As Date) As String
        Dim TiempoDeRuta As Integer
        Dim finaldate As String = ""
        Dim day As Integer
        Dim startdate, sdate, initdate, nextdate, starthour As String

        Dim MyCnn As SqlClient.SqlConnection
        Dim Cmd As SqlClient.SqlCommand
        Dim Rdr As SqlClient.SqlDataReader
        Dim str As String

        '****************************************
        '** OBTENEMOS EL TIEMPO DE RUTA DE LA RECETA
        'If AR = True Then
        '    TiempoDeRuta = 16
        'ElseIf Biselado = False Then
        '    TiempoDeRuta = 3
        'ElseIf Armazon = 3 Or Armazon = 4 Then
        '    TiempoDeRuta = 5
        'Else
        '    TiempoDeRuta = 4
        'End If
        TiempoDeRuta = 0
        '****************************************
        startdate = sdte
        initdate = startdate
        sdate = sdte.ToShortDateString
        day = sdte.DayOfWeek

        nextdate = sdte.ToShortDateString
        starthour = sdte.Hour

        MyCnn = New SqlClient.SqlConnection("user ID=" & My.Settings.DBUser & ";password=" & My.Settings.DBPassword & ";database=" & My.Settings.LocalDBName & ";server=" & My.Settings.LocalServer & ";Connect Timeout=5")
        MyCnn.Open()
        While TiempoDeRuta >= 0
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

    Private Sub RxID_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RxID.TextChanged

    End Sub
End Class