Imports System.Data.SqlClient
Public Class RxList
    Public RxType As RxTypes
    Enum RxTypes
        AllOpenRx
        OnTimeRx
        OutOfTimeRx
        SLAllOpenRx
        SLOnTimeRx
        SLOutOfTimeRx
        ARAllOpenRx
        AROnTimeRx
        AROutOfTimeRx
        FLAllOpenRx
        FLOnTimeRx
        FLOutOfTimeRx
        PCAllOpenRx
        PCOnTimeRx
        PCOutOfTimeRx
    End Enum
    Private Sub RxList_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Dim t As SqlDB
        Dim ds As New DataSet
        Dim LabID As Integer = Form1.LabID
        t = New SqlDB(My.Settings.LocalServer, My.Settings.DBUser, My.Settings.DBPassword, My.Settings.LocalDBName)
        ' t = New SqlDB("AUGENSVR2", "sa", "Proliant01", "MfgSys80")
        Try
            t.OpenConn()
            'Dim Hours As Integer

            'ds = t.SQLDS("select coalesce(difhourstoserver,0) as hours from ERPMaster.dbo.LaborCodes with(nolock) where plant = " & LabID, "t1")
            'Hours = ds.Tables("t1").Rows(0).Item("hours")

            Select Case RxType
                Case RxTypes.OutOfTimeRx

                    Dim Comm As New SqlCommand("SP_RxAbiertas", t.SQLConn)
                    Comm.CommandType = Data.CommandType.StoredProcedure
                    Comm.Parameters.AddWithValue("@PLANTA", LabID)
                    Comm.Parameters.AddWithValue("@TIPO", 0)
                    Comm.Parameters.AddWithValue("@CADENA", 1)
                    Comm.Parameters.AddWithValue("@PROCESO", 19)

                    Dim da As SqlDataAdapter = New SqlDataAdapter(Comm)
                    Comm.ExecuteNonQuery()
                    da.Fill(ds, "t1")

                Case RxTypes.OnTimeRx
                    Dim Comm As New SqlCommand("SP_RxAbiertas", t.SQLConn)
                    Comm.CommandType = Data.CommandType.StoredProcedure
                    Comm.Parameters.AddWithValue("@PLANTA", LabID)
                    Comm.Parameters.AddWithValue("@TIPO", 0)
                    Comm.Parameters.AddWithValue("@CADENA", 1)
                    Comm.Parameters.AddWithValue("@PROCESO", 20)

                    Dim da As SqlDataAdapter = New SqlDataAdapter(Comm)
                    Comm.ExecuteNonQuery()
                    da.Fill(ds, "t1")

                Case RxTypes.AllOpenRx
                    Dim Comm As New SqlCommand("SP_RxAbiertas", t.SQLConn)
                    Comm.CommandType = Data.CommandType.StoredProcedure
                    Comm.Parameters.AddWithValue("@PLANTA", LabID)
                    Comm.Parameters.AddWithValue("@TIPO", 0)
                    Comm.Parameters.AddWithValue("@CADENA", 1)
                    Comm.Parameters.AddWithValue("@PROCESO", 1)

                    Dim da As SqlDataAdapter = New SqlDataAdapter(Comm)
                    Comm.ExecuteNonQuery()
                    da.Fill(ds, "t1")

                    '---------------------------------------------------
                    ' Surfacing Lab
                    '---------------------------------------------------
                Case RxTypes.SLOutOfTimeRx
                    Dim Comm As New SqlCommand("SP_RxAbiertas", t.SQLConn)
                    Comm.CommandType = Data.CommandType.StoredProcedure
                    Comm.Parameters.AddWithValue("@PLANTA", LabID)
                    Comm.Parameters.AddWithValue("@TIPO", 0)
                    Comm.Parameters.AddWithValue("@CADENA", 1)
                    Comm.Parameters.AddWithValue("@PROCESO", 9)

                    Dim da As SqlDataAdapter = New SqlDataAdapter(Comm)
                    Comm.ExecuteNonQuery()
                    da.Fill(ds, "t1")


                Case RxTypes.SLOnTimeRx
                    Dim Comm As New SqlCommand("SP_RxAbiertas", t.SQLConn)
                    Comm.CommandType = Data.CommandType.StoredProcedure
                    Comm.Parameters.AddWithValue("@PLANTA", LabID)
                    Comm.Parameters.AddWithValue("@TIPO", 0)
                    Comm.Parameters.AddWithValue("@CADENA", 1)
                    Comm.Parameters.AddWithValue("@PROCESO", 8)

                    Dim da As SqlDataAdapter = New SqlDataAdapter(Comm)
                    Comm.ExecuteNonQuery()
                    da.Fill(ds, "t1")

                Case RxTypes.SLAllOpenRx
                    Dim Comm As New SqlCommand("SP_RxAbiertas", t.SQLConn)
                    Comm.CommandType = Data.CommandType.StoredProcedure
                    Comm.Parameters.AddWithValue("@PLANTA", LabID)
                    Comm.Parameters.AddWithValue("@TIPO", 0)
                    Comm.Parameters.AddWithValue("@CADENA", 1)
                    Comm.Parameters.AddWithValue("@PROCESO", 7)

                    Dim da As SqlDataAdapter = New SqlDataAdapter(Comm)
                    Comm.ExecuteNonQuery()
                    da.Fill(ds, "t1")

                    ' Antireflejante
                    '---------------------------------------------------
                Case RxTypes.AROutOfTimeRx
                    Dim Comm As New SqlCommand("SP_RxAbiertas", t.SQLConn)
                    Comm.CommandType = Data.CommandType.StoredProcedure
                    Comm.Parameters.AddWithValue("@PLANTA", LabID)
                    Comm.Parameters.AddWithValue("@TIPO", 0)
                    Comm.Parameters.AddWithValue("@CADENA", 1)
                    Comm.Parameters.AddWithValue("@PROCESO", 12)

                    Dim da As SqlDataAdapter = New SqlDataAdapter(Comm)
                    Comm.ExecuteNonQuery()
                    da.Fill(ds, "t1")

                Case RxTypes.AROnTimeRx
                    Dim Comm As New SqlCommand("SP_RxAbiertas", t.SQLConn)
                    Comm.CommandType = Data.CommandType.StoredProcedure
                    Comm.Parameters.AddWithValue("@PLANTA", LabID)
                    Comm.Parameters.AddWithValue("@TIPO", 0)
                    Comm.Parameters.AddWithValue("@CADENA", 1)
                    Comm.Parameters.AddWithValue("@PROCESO", 11)

                    Dim da As SqlDataAdapter = New SqlDataAdapter(Comm)
                    Comm.ExecuteNonQuery()
                    da.Fill(ds, "t1")

                Case RxTypes.ARAllOpenRx
                    Dim Comm As New SqlCommand("SP_RxAbiertas", t.SQLConn)
                    Comm.CommandType = Data.CommandType.StoredProcedure
                    Comm.Parameters.AddWithValue("@PLANTA", LabID)
                    Comm.Parameters.AddWithValue("@TIPO", 0)
                    Comm.Parameters.AddWithValue("@CADENA", 1)
                    Comm.Parameters.AddWithValue("@PROCESO", 10)

                    Dim da As SqlDataAdapter = New SqlDataAdapter(Comm)
                    Comm.ExecuteNonQuery()
                    da.Fill(ds, "t1")

                    ' Finishing Lab
                    '---------------------------------------------------
                Case RxTypes.FLOutOfTimeRx
                    Dim Comm As New SqlCommand("SP_RxAbiertas", t.SQLConn)
                    Comm.CommandType = Data.CommandType.StoredProcedure
                    Comm.Parameters.AddWithValue("@PLANTA", LabID)
                    Comm.Parameters.AddWithValue("@TIPO", 0)
                    Comm.Parameters.AddWithValue("@CADENA", 1)
                    Comm.Parameters.AddWithValue("@PROCESO", 15)

                    Dim da As SqlDataAdapter = New SqlDataAdapter(Comm)
                    Comm.ExecuteNonQuery()
                    da.Fill(ds, "t1")

                Case RxTypes.FLOnTimeRx
                    Dim Comm As New SqlCommand("SP_RxAbiertas", t.SQLConn)
                    Comm.CommandType = Data.CommandType.StoredProcedure
                    Comm.Parameters.AddWithValue("@PLANTA", LabID)
                    Comm.Parameters.AddWithValue("@TIPO", 0)
                    Comm.Parameters.AddWithValue("@CADENA", 1)
                    Comm.Parameters.AddWithValue("@PROCESO", 14)

                    Dim da As SqlDataAdapter = New SqlDataAdapter(Comm)
                    Comm.ExecuteNonQuery()
                    da.Fill(ds, "t1")

                Case RxTypes.FLAllOpenRx
                    Dim Comm As New SqlCommand("SP_RxAbiertas", t.SQLConn)
                    Comm.CommandType = Data.CommandType.StoredProcedure
                    Comm.Parameters.AddWithValue("@PLANTA", LabID)
                    Comm.Parameters.AddWithValue("@TIPO", 0)
                    Comm.Parameters.AddWithValue("@CADENA", 1)
                    Comm.Parameters.AddWithValue("@PROCESO", 13)

                    Dim da As SqlDataAdapter = New SqlDataAdapter(Comm)
                    Comm.ExecuteNonQuery()
                    da.Fill(ds, "t1")

                    ' Por Cerrar
                    '---------------------------------------------------
                Case RxTypes.PCOutOfTimeRx
                    Dim Comm As New SqlCommand("SP_RxAbiertas", t.SQLConn)
                    Comm.CommandType = Data.CommandType.StoredProcedure
                    Comm.Parameters.AddWithValue("@PLANTA", LabID)
                    Comm.Parameters.AddWithValue("@TIPO", 0)
                    Comm.Parameters.AddWithValue("@CADENA", 1)
                    Comm.Parameters.AddWithValue("@PROCESO", 17)

                    Dim da As SqlDataAdapter = New SqlDataAdapter(Comm)
                    Comm.ExecuteNonQuery()
                    da.Fill(ds, "t1")

                Case RxTypes.PCOnTimeRx
                    Dim Comm As New SqlCommand("SP_RxAbiertas", t.SQLConn)
                    Comm.CommandType = Data.CommandType.StoredProcedure
                    Comm.Parameters.AddWithValue("@PLANTA", LabID)
                    Comm.Parameters.AddWithValue("@TIPO", 0)
                    Comm.Parameters.AddWithValue("@CADENA", 1)
                    Comm.Parameters.AddWithValue("@PROCESO", 16)

                    Dim da As SqlDataAdapter = New SqlDataAdapter(Comm)
                    Comm.ExecuteNonQuery()
                    da.Fill(ds, "t1")

                Case RxTypes.PCAllOpenRx
                    Dim Comm As New SqlCommand("SP_RxAbiertas", t.SQLConn)
                    Comm.CommandType = Data.CommandType.StoredProcedure
                    Comm.Parameters.AddWithValue("@PLANTA", LabID)
                    Comm.Parameters.AddWithValue("@TIPO", 0)
                    Comm.Parameters.AddWithValue("@CADENA", 1)
                    Comm.Parameters.AddWithValue("@PROCESO", 18)

                    Dim da As SqlDataAdapter = New SqlDataAdapter(Comm)
                    Comm.ExecuteNonQuery()
                    da.Fill(ds, "t1")

            End Select
            Dim dv As New DataView(ds.Tables("t1"))
            dv.RowFilter = "Virtual is not null"
            GridRx.DataSource = dv 'ds.Tables("t1")
            GridRx.Sort(GridRx.Columns("Entrada"), System.ComponentModel.ListSortDirection.Ascending)
            'Form1.pe.ActualizaProcesos_Click(sender, e)

        Catch ex As Exception
            If ex.Message.Contains("Error de Conexion") Then
                Form1.ChangeWorkingStatus(Form1.WorkingStatusType.OffLine)
            Else
                MsgBox(ex.Message, MsgBoxStyle.Critical)
            End If
        Finally
            t.CloseConn()
        End Try
    End Sub

    Public Sub New()

        ' This call is required by the Windows Form Designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.

    End Sub
    Public Sub New(ByVal ds As DataSet)
        ' This call is required by the Windows Form Designer.
        InitializeComponent()

        GridRx.DataSource = ds

    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click, BotonCerrar.Click
        Me.Close()
    End Sub

    Private Sub GridRx_BindingContextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles GridRx.BindingContextChanged
        If GridRx.RowCount > 0 Then
            Dim i As Integer
            Dim hours As Integer = 0
            'Dim day As Integer
            'Dim days As Integer
            'Dim indate, nowdate As Date
            'Dim mins As Integer
            'Dim duehours As Integer
            'Dim InitHour As Integer
            'Dim EndHour As Integer
            'Dim DayHours As Integer
            'Dim StartHour As Integer
            'Dim IsVirtualRx As Boolean
            'Dim LocalLab As Integer


            Dim t As New SqlDB(My.Settings.LocalServer, My.Settings.DBUser, My.Settings.DBPassword, My.Settings.LocalDBName)
            Dim t2 As New SqlDB(My.Settings.VantageServer, My.Settings.DBUser, My.Settings.DBPassword, My.Settings.ERPDBName)
            Dim ds As New DataSet
            Dim ds2 As New DataSet
            Try
                t.OpenConn()
                t2.OpenConn()

                Dim TotalRows As Integer = GridRx.RowCount
                'If TotalRows > 100 Then
                '    TotalRows = 100
                'End If
                Dim LabID As Integer = Form1.LabID.ToString



                'For i = 0 To TotalRows - 1
                'Dim x As String = GridRx("Virtual", i).Value.ToString
                '    ds = t.SQLDS("select coalesce(difhourstoserver,0) as hours, getdate() as nowdate from LaborCodes with(nolock) where plant = " & LabID, "t1")
                '    hours = ds.Tables("t1").Rows(0).Item("hours")
                '    IsVirtualRx = GridRx("Virtual", i).Value
                '    LocalLab = GridRx("Lab", i).Value
                '    If LocalLab <> LabID Then
                '        ds2 = t2.SQLDS("select nombre from tbllaboratorios where cl_lab = " & LocalLab, "t1")
                '        GridRx("Cliente", i).Value = "LAB. " & ds2.Tables("t1").Rows(0).Item("nombre").ToString.ToUpper()
                '    End If
                '    ds = t.SQLDS("select coalesce(difhourstoserver,0) as hours from LaborCodes with(nolock) where plant = " & LabID, "t1")
                '    hours = ds.Tables("t1").Rows(0).Item("hours")
                '    GridRx("Entrada", i).Value = CDate(GridRx("Entrada", i).Value).AddHours(hours)
                'Next
            Catch ex As Exception
                If ex.Message.Contains("Error de Conexion") Then
                    Form1.ChangeWorkingStatus(Form1.WorkingStatusType.OffLine)
                Else
                    'MsgBox(ex.Message, MsgBoxStyle.Critical)
                End If
                MsgBox(i, MsgBoxStyle.Critical)
            Finally
                For i = 0 To GridRx.Columns.Count - 1
                    GridRx.Columns(i).Visible = False
                Next

                GridRx.Columns("Rx").Visible = True
                GridRx.Columns("Vantage").Visible = True
                GridRx.Columns("Cliente").Visible = True
                GridRx.Columns("Virtual").Visible = True
                GridRx.Columns("Horas del Proceso").Visible = True
                GridRx.Columns("Entrada").Visible = True
                'GridRx.Columns("Virtual").HeaderText = "AR"
                t.CloseConn()
                t2.CloseConn()
            End Try

        End If

    End Sub
End Class