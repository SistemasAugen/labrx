Public Class Clientes_Saldo


    Private Sub Clientes_Saldo_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'TODO: This line of code loads data into the 'VerbienDataSet.customer' table. You can move, or remove it, as needed.
        Me.Clientes_Saldo_TableAdapter.Fill(Me.DataSet_clientes_saldo.VwSaldoClientes)

    End Sub

    Private Sub tsbtn_Conslidar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tsbtn_Conslidar.Click
        Me.Clientes_Saldo_TableAdapter.SaldosConsolidados(Me.DataSet_clientes_saldo.VwSaldoClientes)
    End Sub

    Private Sub BuscaClienteToolStripButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BuscaClienteToolStripButton.Click
        Try
            If CustTipToolStripTextBox.Text.Trim.Length > 2 Then
                Me.Clientes_Saldo_TableAdapter.BuscaCliente(Me.DataSet_clientes_saldo.VwSaldoClientes, "%" + CustTipToolStripTextBox.Text.Trim + "%")
            ElseIf CustTipToolStripTextBox.Text.Trim.Length = 0 Then
                Me.Clientes_Saldo_TableAdapter.Fill(Me.DataSet_clientes_saldo.VwSaldoClientes)
            ElseIf CustTipToolStripTextBox.Text.Trim.Length <= 2 Then
                MsgBox("Debe indicar más de 2 letras o números para realizar la búsqueda.", MsgBoxStyle.Information, "Buscar")
            End If

        Catch ex As System.Exception
            System.Windows.Forms.MessageBox.Show(ex.Message)
        End Try
    End Sub
End Class