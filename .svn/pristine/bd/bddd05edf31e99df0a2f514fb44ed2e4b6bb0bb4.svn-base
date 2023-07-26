Public Class ARLabList
    Public ARLabName As String = ""
    Public IsARLab As Boolean = True
    Private Sub ARLabList_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Dim t As New SqlDB(My.Settings.LocalServer, My.Settings.DBUser, My.Settings.DBPassword, My.Settings.LocalDBName)
        Dim ds As New DataSet
        Try
            t.OpenConn()
            If IsARLab Then
                ds = t.SQLDS("select nombre,cl_lab as plant from tbllaboratorios where isarlab = 1", "t1")
            Else
                ds = t.SQLDS("select nombre,cl_lab as plant from tbllaboratorios where isvrlab = 1", "t1")
            End If
            ComboARLab.DataSource = ds.Tables("t1")
            ComboARLab.ValueMember = "plant"
            ComboARLab.DisplayMember = "nombre"
            If ARLabName <> "" Then
                ComboARLab.Text = ARLabName
            End If

        Catch ex As Exception
        Finally
            t.CloseConn()
        End Try

    End Sub


    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click, BotonAceptar.Click
        Close()
    End Sub
End Class