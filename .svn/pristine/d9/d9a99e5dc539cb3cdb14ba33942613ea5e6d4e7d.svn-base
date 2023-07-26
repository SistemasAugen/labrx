Public Class ARList
    Public ARProcess As Boolean
    Public Sub New(ByVal ds As DataSet)
        ' This call is required by the Windows Form Designer.
        InitializeComponent()
        ' Add any initialization after the InitializeComponent() call.
        GridARList.DataSource = ds.Tables("t1")
    End Sub
    Public Sub New()
        ' This call is required by the Windows Form Designer.
        InitializeComponent()
        ' Add any initialization after the InitializeComponent() call.
        ARProcess = False
    End Sub
    Private Sub GridARList_CellDoubleClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles GridARList.CellDoubleClick
        Dim t As New SqlDB(My.Settings.VantageServer, My.Settings.DBUser, My.Settings.DBPassword, My.Settings.MfgSysDBName)
        Dim Change As Boolean = True
        Try
            If Form1.PanelInformacion.Visible Then
                If MsgBox("Estas seguro de mostrar esta receta?" & vbCrLf & "La información capturada se perderá.", MsgBoxStyle.OkCancel + MsgBoxStyle.Exclamation) = MsgBoxResult.Cancel Then
                    Change = False
                End If
            End If
            If Change Then
                ARProcess = True
                t.OpenConn()
                Dim ds As New DataSet
                Dim sqlstr As String '= "select armazon as contorno,* from VwRecetasTRECEUX with(nolock) where ordernum = " & Me.GridARList.Rows(e.RowIndex).Cells("ordernum").Value.ToString
                sqlstr = "select armazon as contorno,* from VwRxVirtualesConTrazo with(nolock) where ordernum = " & Me.GridARList.Rows(e.RowIndex).Cells("ordernum").Value.ToString

                ds = t.SQLDS(sqlstr, "t1")
                Dim rxnum As String = ds.Tables("t1").Rows(0).Item("ordernum")
                If rxnum.Substring(0, 2) = Form1.ARLab Then
                    Form1.IsReceivingVirtualRx = True
                    Form1.FillRx(ds, False)
                    'Form1.AceptarModificar(Form1.RxID.Text)
                    Form1.RadioManual.Checked = True
                    Form1.AceptarManualNew.Visible = False
                    Form1.PanelInformacion.Visible = True
                    Form1.PanelOpciones.Visible = True
                    Form1.PanelArmazon.Enabled = False
                    Form1.PanelRx.Enabled = True
                    Form1.PanelContorno.Enabled = False
                    Form1.Cancel.Enabled = True
                Else
                    Form1.InitValues()
                    Form1.IsReceivingVirtualRx = True
                    Form1.FillRx(ds, False)
                    ' Es una Rx Virtual que se est'a recibiendo
                    ' Form1.AceptarManual_Click(Me, e)
                    Form1.AceptarVirtualAR()
                End If
                Form1.ReceivingVirtualRx = Me.GridARList.SelectedRows(0).Cells("Rx").Value
                ARProcess = False
            End If
        Catch ex As Exception
            If ex.Message.Contains("Error de Conexion") Then
                Form1.ChangeWorkingStatus(Form1.WorkingStatusType.OffLine)
            End If
        Finally
            t.CloseConn()
        End Try
    End Sub

    Private Sub TextSearch_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles TextSearch.KeyDown
        If (e.KeyCode = Keys.NumPad0 Or e.KeyCode = Keys.Space) And TextSearch.Text.Length = 0 Then
            e.SuppressKeyPress = True
        Else
            If e.KeyCode = Keys.Enter Then
                Cursor = Cursors.WaitCursor
                Application.DoEvents()
                If TextSearch.Text.Length >= 10 And TextSearch.Text.StartsWith("0") Then
                    TextSearch.Text = TextSearch.Text.Substring(1)
                End If
                Dim i As Integer = 0
                For i = 0 To GridARList.Rows.Count - 1
                    If GridARList.Item("ordernum", i).Value.ToString = TextSearch.Text Then
                        GridARList.Item("ordernum", i).Selected = True
                        Exit For
                    End If
                Next
                If i <> GridARList.Rows.Count Then
                    GridARList_CellDoubleClick(Me, New DataGridViewCellEventArgs(0, i))
                Else
                    MsgBox("La Rx no se encuentra en el listado de virtuales.", MsgBoxStyle.Information)
                    'Dim t As New SqlDB(My.Settings.VantageServer, My.Settings.DBUser, My.Settings.DBPassword, My.Settings.MfgSysDBName)
                    'Dim Comm As SqlCommand
                    'Try
                    '    t.OpenConn()
                    '    Comm = New SqlCommand("[SP_CheckOrderByOrdenLab]", t.SQLConn)
                    '    Comm.CommandType = CommandType.StoredProcedure
                    '    Comm.CommandTimeout = 15
                    '    Comm.Parameters.AddWithValue("@OrdenLab", TextOrdenLab.Text)
                    '    Comm.Parameters.Add("@Comments", Data.SqlDbType.VarChar, 500)
                    '    Comm.Parameters("@Comments").Direction = Data.ParameterDirection.Output
                    '    Comm.ExecuteNonQuery()
                    '    If Comm.Parameters("@Comments").Value <> "" Then
                    '        MsgBox(Comm.Parameters("@Comments").Value, MsgBoxStyle.Exclamation)
                    '    End If
                    'Catch ex As Exception
                    '    If ex.Message.Contains("Error de Conexion") Then
                    '        Form1.ChangeWorkingStatus(Form1.WorkingStatusType.OffLine)
                    '    End If
                    'Finally
                    '    t.CloseConn()
                    'End Try

                End If
                Cursor = Cursors.Default
            End If
        End If
    End Sub

    Private Sub ARList_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.Location = New Point(Form1.Location.X + 810, Form1.Location.Y + 0)
    End Sub
End Class