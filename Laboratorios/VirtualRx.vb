Imports Microsoft.Win32
Public Class VirtualRx
    Dim DGInd As Integer = -1
    Public VirtualRx As Integer
    Public RxDS As DataSet
    Public RxArmazon As Boolean
    Public Cve_Lab As Integer

    Private Sub Refresh_VirtualRxFields()
        Dim OdbcCon As New Odbc.OdbcConnection
        Dim adp As Odbc.OdbcDataAdapter
        Dim dataset As DataSet
        Dim SQLCmd As String

        Try
            OdbcCon.ConnectionString = "DSN=" & Read_Registry("DSN") & ";UID=calcot;Connect Timeout=5"

            OdbcCon.Open()
            SQLCmd = " SELECT geo_arm.rx as Receta,laboratorios.nombre as Laboratorio,geo_arm.fecha as Fecha,laboratorios.cl_lab " & _
                     " FROM geo_arm geo_arm, laboratorios laboratorios " & _
                     " WHERE(geo_arm.cl_lab = Laboratorios.cl_lab And ((geo_arm.pend = 21 or geo_arm.pend = 20)))"
            adp = New Odbc.OdbcDataAdapter(SQLCmd, OdbcCon)
            dataset = New DataSet
            adp.Fill(dataset, "T1")
            adp.Dispose()
            adp = Nothing

            SQLCmd = " SELECT recetas.rx as Receta,laboratorios.nombre as Laboratorio,recetas.fecha as Fecha,laboratorios.cl_lab " & _
                                 " FROM recetas recetas, laboratorios laboratorios " & _
                                 " WHERE(recetas.cl_lab = Laboratorios.cl_lab And ((recetas.pend = 21 or recetas.pend = 20)))"
            adp = New Odbc.OdbcDataAdapter(SQLCmd, OdbcCon)
            adp.Fill(dataset, "T2")
            adp.Dispose()
            adp = Nothing

            'juntamos T1 y T2 en una sola tabla para desplegarla en el datagrid de recetas virtuales
            Dim RxDS As New DataSet
            Dim row As DataRow

            RxDS.Tables.Add("Rxs")
            RxDS.Tables("Rxs").Columns.Add("receta")
            RxDS.Tables("Rxs").Columns.Add("Laboratorio")
            RxDS.Tables("Rxs").Columns.Add("Fecha")
            RxDS.Tables("Rxs").Columns.Add("Armazon")
            RxDS.Tables("Rxs").Columns.Add("cl_lab")

            For Each row In dataset.Tables("T1").Rows
                RxDS.Tables("Rxs").Rows.Add(row("receta"), row("Laboratorio"), row("fecha"), 1, row("cl_lab"))
            Next
            For Each row In dataset.Tables("T2").Rows
                RxDS.Tables("Rxs").Rows.Add(row("receta"), row("Laboratorio"), row("fecha"), 0, row("cl_lab"))
            Next
            '***************************************************************************************
            OdbcCon.Close()
            DGVirtualRx.DataSource = RxDS.Tables("Rxs")
        Catch ex As Exception
            MsgBox(ex.Message)
            Me.Close()
        End Try
    End Sub

    Private Sub VirtualRx_Disposed(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Disposed
        Me.VirtualRx = 0

    End Sub

    Private Sub VirtualRx_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Refresh_VirtualRxFields()
        txtrxvirtual.Focus()
        txtrxvirtual.SelectAll()
    End Sub



    Private Sub DGVirtualRx_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles DGVirtualRx.Click

        DGInd = DGVirtualRx.CurrentCell.RowIndex
        If DGVirtualRx.Item(0, DGInd).Value.ToString <> "" Then
            txtrxvirtual.Text = DGVirtualRx.Item(0, DGInd).Value.ToString
            txtrxvirtual.Focus()
            txtrxvirtual.SelectAll()
        End If
    End Sub

    Private Sub DGVirtualRx_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles DGVirtualRx.DoubleClick
        If DGVirtualRx.Item(0, DGInd).Value.ToString <> "" Then
            Me.VirtualRx = DGVirtualRx.Item(0, DGInd).Value.ToString
            Me.Cve_Lab = DGVirtualRx.Item(4, DGInd).Value.ToString
            If DGVirtualRx.Item(3, DGInd).Value.ToString = "1" Then
                Me.RxArmazon = True
            Else
                Me.RxArmazon = False
            End If
            GetVirtualRx()
            Me.Close()
        End If
    End Sub
    Private Function GetVirtualRx() As Boolean

        Dim OdbcCon As New Odbc.OdbcConnection
        Dim adp As Odbc.OdbcDataAdapter
        Dim SQLCmd As String
        Dim valor As Boolean

        Try
            OdbcCon.ConnectionString = "DSN=" & Read_Registry("DSN") & ";UID=calcot;Connect Timeout=5"

            OdbcCon.Open()
            Me.RxDS = New DataSet
            'si la receta contiene armazon jalamos la receta de la tabla geo_arm
            If Me.RxArmazon Then
                SQLCmd = " SELECT color.color,geo_arm.aro,geo_arm.tono,geo_arm.grad,geo_arm.puente,geo_arm.dipce,geo_arm.diple,geo_arm.mat_arm,geo_arm.radios " & _
                         " FROM geo_arm geo_arm, color color " & _
                         " WHERE(geo_arm.cl_color = color.cl_color And ((geo_arm.rx = " & Me.VirtualRx & ")))"
            Else 'si no tiene armazon entonces del a tabla recetas
                SQLCmd = " SELECT color.color,recetas.tono,recetas.grad " & _
                         " FROM recetas recetas, color color " & _
                         " WHERE(recetas.cl_color = color.cl_color And ((recetas.rx = " & Me.VirtualRx & ")))"
            End If

            adp = New Odbc.OdbcDataAdapter(SQLCmd, OdbcCon)
            adp.Fill(Me.RxDS, "RxFields")
            adp.Dispose()
            adp = Nothing

            If Me.RxArmazon Then
                SQLCmd = " SELECT lado_bis.lado,lado_bis.esfera,lado_bis.cil,lado_bis.eje,lado_bis.adicion,lado_bis.prismap,lado_bis.eg_pris, " & _
                         " lado_bis.mono,lado_bis.altura " & _
                         " FROM lado_bis lado_bis " & _
                         " WHERE lado_bis.rx = " & Me.VirtualRx & " "
            Else
                SQLCmd = " SELECT rx_lado.lado,rx_lado.esfera,rx_lado.cil,rx_lado.eje,rx_lado.adicion,rx_lado.prismap,rx_lado.eg_pris " & _
                         " FROM rx_lado rx_lado " & _
                         " WHERE rx_lado.rx = " & Me.VirtualRx & " "
            End If

            adp = New Odbc.OdbcDataAdapter(SQLCmd, OdbcCon)
            adp.Fill(Me.RxDS, "RxSides")
            adp.Dispose()
            adp = Nothing
            If RxDS.Tables("RxFields").Rows.Count > 0 Or RxDS.Tables("RxSides").Rows.Count > 0 Then
                valor = True
            Else
                RxDS.Dispose()
                RxDS = Nothing
                valor = False
            End If

            OdbcCon.Close()
            Return valor
        Catch ex As Exception
            MsgBox(ex.Message)
            valor = False
            Return valor
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
            Case "DSN"
                'Result = CStr(key.GetValue("DSNGuiador"))
                Result = My.Settings.DSNGuiador
        End Select
        Return Result
    End Function

    Private Sub txtrxvirtual_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtrxvirtual.KeyDown
        If (e.KeyCode) = Keys.Enter And txtrxvirtual.Text <> "" Then
            Me.VirtualRx = txtrxvirtual.Text
            Check_Armazon()
            If GetVirtualRx() Then
                Me.Close()
            End If
        End If
        If e.KeyCode = Keys.Escape Then
            Me.VirtualRx = 0
            Me.Close()
        End If
    End Sub
    Private Sub Check_Armazon()
        Dim OdbcCon As New Odbc.OdbcConnection
        Dim adp As Odbc.OdbcDataAdapter
        Dim dataset As DataSet
        Dim SQLCmd As String

        Try
            OdbcCon.ConnectionString = "DSN=" & Read_Registry("DSN") & ";UID=calcot;Connect Timeout=5"

            OdbcCon.Open()
            SQLCmd = " SELECT geo_arm.rx as Receta " & _
                     " FROM geo_arm geo_arm " & _
                     " WHERE geo_arm.rx = " & txtrxvirtual.Text & " "
            adp = New Odbc.OdbcDataAdapter(SQLCmd, OdbcCon)
            dataset = New DataSet
            adp.Fill(dataset, "T1")
            adp.Dispose()
            adp = Nothing

            SQLCmd = " SELECT recetas.rx as Receta " & _
                                 " FROM recetas recetas " & _
                                 " WHERE(recetas.rx = " & txtrxvirtual.Text & ")"
            adp = New Odbc.OdbcDataAdapter(SQLCmd, OdbcCon)
            adp.Fill(dataset, "T2")
            adp.Dispose()
            adp = Nothing

            OdbcCon.Close()
            OdbcCon.Dispose()
            OdbcCon = Nothing
            If dataset.Tables("T1").Rows.Count > 0 Then RxArmazon = True
            If dataset.Tables("T2").Rows.Count > 0 Then RxArmazon = False

        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical)
            RxArmazon = False
        End Try
    End Sub

End Class