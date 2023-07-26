Public Class ModificarRecetas
    Public Status As Boolean

    Private Sub OK_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OK.Click, BotonAceptar.Click
        Status = True
        Close()
    End Sub

    Private Sub Cancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Cancel.Click, BotonCancelar.Click
        Status = False
        Close()
    End Sub

    Private Sub RxID_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles RxID.KeyPress
        If e.KeyChar = Chr(13) Then
            OK_Click(Me, e)
        End If
    End Sub
End Class