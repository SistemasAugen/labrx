Public Class FrmDrill
    Public X1, X2, Y1, Y2, Depth, Diametro, BFA, AngLat, AngVer As String
    Public DataChanged As Boolean
    Private Sub FrmDrill_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown

        If e.KeyCode = Keys.Escape Then
            Me.Close()
        End If
    End Sub

    Private Sub FrmDrill_KeyUp(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyUp
        If e.KeyCode = Keys.Escape Then
            Me.Close()
        End If
    End Sub

    Private Sub FrmDrill_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles MyBase.KeyPress

    End Sub

    
    Private Sub BTNCLOSE_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BTNCLOSE.Click
        DataChanged = False
        Me.Close()
    End Sub

    Private Sub BTNSAVE_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BTNSAVE.Click
        X1 = txtX1.Text
        X2 = txtX2.Text
        Y1 = txtY1.Text
        Y2 = txtY2.Text
        Depth = txtprofundidad.Text
        Diametro = TXTDIA.Text
        BFA = txtbfa.Text
        AngLat = txtanglat.Text
        AngVer = txtangvert.Text
        DataChanged = True
        Me.Close()
    End Sub
End Class