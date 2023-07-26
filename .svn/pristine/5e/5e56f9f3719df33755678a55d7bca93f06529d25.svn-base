Public Class ReadFrame
    Public Read As Boolean
    Public FrameDescription As String
    Private RequestedFrame As String


    Private Sub ReadFrame_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

    End Sub

    Public Sub New(ByVal FrameID As String)

        ' This call is required by the Windows Form Designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        RequestedFrame = FrameID
        If RequestedFrame.Length > 5 Then
            RequestedFrame = RequestedFrame.Substring(0, 5)
        End If
        Read = False

    End Sub

    Private Sub BTN_OK_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BTN_OK.Click
        If CheckFrameStatus() Then
            Read = True
            Close()
        End If
    End Sub

    Private Sub BTN_Cancelar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BTN_Cancelar.Click
        Read = False
        Close()
    End Sub
    Private Function CheckFrameStatus() As Boolean
        Dim Failed As Boolean = False
        Dim FailedMessage As String = ""

        Dim sqlstr As String = ""
        Dim ds As New DataSet
        Dim OnHanQty As Integer = 0
        Dim ThisFrame As String = TBX_FrameID.Text
        Dim Description As String = ""


        Dim t As New SqlDB()
        t.SQLConn = New SqlClient.SqlConnection(Laboratorios.ConnStr)
        Try
            t.OpenConn()
            sqlstr = "select * from vwarmazones with(nolock) where partnum = '" & ThisFrame & "'"
            ds = t.SQLDS(sqlstr, "t1")
            If ds.Tables("t1").Rows.Count > 0 Then
                OnHanQty = ds.Tables("t1").Rows(0).Item("onhandqty")
                Try
                    FrameDescription = ds.Tables("t1").Rows(0).Item("partdescription")
                Catch ex As Exception
                    FrameDescription = "No se encontró descripción"
                End Try
                If ThisFrame <> RequestedFrame Then
                    Throw New Exception("Este armazón no corresponde al requerido en la Receta" & vbCrLf & "[" & FrameDescription & "]")
                End If

                ' # PEDRO FARIAS LOZANO # 16 AGO 2013 # Ahora todos los armazones los surte Ensenada
                'If OnHanQty <= 0 Then
                'Throw New Exception("No hay inventario" & vbCrLf & "[" & FrameDescription & "]")
                'End If
            Else
            Throw New Exception("Armazón [" & ThisFrame & "]inexistente en la base de datos")
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
        Return Not Failed
    End Function

    Private Sub TBX_FrameID_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles TBX_FrameID.KeyDown
        If e.KeyCode = Keys.Enter Then
            If CheckFrameStatus() Then
                Read = True
                Close()
            End If
        End If
    End Sub
End Class