Public Class FrameError
    Public FrameErrorID As Integer
    Public FrameErrorDesc As String
    Public Changed As Boolean

    Private Sub GetErrors()
        Dim Failed As Boolean = False
        Dim FailedMessage As String = ""

        Dim sqlstr As String = ""
        Dim ds As New DataSet


        Dim t As New SqlDB()
        t.SQLConn = New SqlClient.SqlConnection(Laboratorios.ConnStr)
        Try
            t.OpenConn()
            sqlstr = "select * from TblFrameErrors with(nolock) where enabled = 1 order by description"
            ds = t.SQLDS(sqlstr, "t1")
            CMB_FrameErrors.DisplayMember = "Description"
            CMB_FrameErrors.ValueMember = "ErrorID"
            CMB_FrameErrors.DataSource = ds.Tables("t1")
            If ds.Tables("t1").Rows.Count > 0 Then

            Else
                Throw New Exception("No se encontraron Errores de Armazón.")
            End If

        Catch ex As Exception
            Failed = True
            FailedMessage = "Error al obtener datos de armazón" & vbCrLf & ex.Message
        Finally
            t.CloseConn()
            If Failed Then
                MsgBox(FailedMessage, MsgBoxStyle.Exclamation)
            End If
        End Try       
    End Sub
    Private Sub FrameError_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        FrameErrorID = 0
        FrameErrorDesc = ""
        Changed = False
        GetErrors()
    End Sub

    Private Sub BTN_Ok_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BTN_Ok.Click
        FrameErrorID = CMB_FrameErrors.SelectedValue
        FrameErrorDesc = CMB_FrameErrors.Text
        Changed = True
        Close()
    End Sub

    Private Sub BTN_Cancelar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BTN_Cancelar.Click
        Changed = False
        Close()
    End Sub
End Class