Public Class Clientes_ABC

    Private miLabID As Integer = 0

    Public Sub New(ByVal LabID As Integer)
        miLabID = LabID
        InitializeComponent()
    End Sub
    Private Sub Clientes_ABC_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'TODO: This line of code loads data into the 'VerbienDataSet.customer' table. You can move, or remove it, as needed.
        Me.CustomerTableAdapter.Fill(Me.VerbienDataSet.customer)
        btnCancelar.Enabled = False
        ' Pedro Farías Lozano  Oct 31, 2012
        ' Los usuarios pueden añadir clientes si la bandera EditarClientes está en true
        DGV_Clientes.AllowUserToAddRows = My.Settings.EditarClientes
    End Sub

    Private Sub BuscaClienteToolStripButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BuscaClienteToolStripButton.Click
        Try

            If CustTipToolStripTextBox.Text.Trim.Length = 0 Then
                Me.CustomerTableAdapter.Fill(Me.VerbienDataSet.customer)
            ElseIf CustTipToolStripTextBox.Text.Trim.Length <= 2 Then
                MsgBox("Debe indicar más de 2 letras o números para realizar la búsqueda.", MsgBoxStyle.Information, "Buscar")
            ElseIf CustTipToolStripTextBox.Text.Trim.Length > 2 Then
                Me.CustomerTableAdapter.BuscaCliente(Me.VerbienDataSet.customer, "%" + CustTipToolStripTextBox.Text.Trim + "%")
            End If

        Catch ex As System.Exception
            System.Windows.Forms.MessageBox.Show(ex.Message)
        End Try

    End Sub

    Private Sub btnClienteNuevo_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClienteNuevo.Click
        'Prepara el formulario para agregar un cliente nuevo
        'Los campos CUSTNUM y PROGRESS_REC_IDENT son autonuméricos (IDENT)
        Try
            Me.CustomerTableAdapter.Adapter.Update(VerbienDataSet.customer)
        Catch exceptionObj As Exception
            MessageBox.Show(exceptionObj.Message.ToString())
        End Try
        btnCancelar.Enabled = False
    End Sub

    Private Sub DGV_Clientes_UserAddedRow(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewRowEventArgs) Handles DGV_Clientes.UserAddedRow
        If (e.Row.IsNewRow) Then        'Agregamos el valor del laboratorio en number01
            'e.Row.Cells("number01").Value = miLabID
            'e.Row.Cells(2).Value = "Mi Casa"
            DGV_Clientes.CurrentRow.Cells("number01").Value = miLabID
            DGV_Clientes.CurrentRow.Cells("RFC").Value = "ABCD000000ABC0"
            DGV_Clientes.CurrentRow.Cells("pais").Value = "MEXICO"
            DGV_Clientes.CurrentRow.Cells("credithold").Value = "0"
            DGV_Clientes.CurrentRow.Cells("number06").Value = "0"
            DGV_Clientes.CurrentRow.Cells("checkbox01").Value = "0"
            DGV_Clientes.CurrentRow.Cells("checkbox04").Value = "0"

            btnCancelar.Enabled = True
        End If
    End Sub
End Class