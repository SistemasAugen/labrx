Public Class RxResume
    Public Modify As Boolean = False
    Private Sub Preview_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Modificar.Click, BotonModificar.Click
        Modify = True
        Me.Close()
    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Cancelar.Click, BotonCancelar.Click
        Modify = False
        Me.Close()
    End Sub

    Private Sub RxResume_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Modificar.Select()
    End Sub
End Class