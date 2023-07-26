Imports System.Windows.Forms
Imports System.Data.SqlClient

Public Class WebOrders

    Private Sub OK_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OK_Button.Click
        Me.DialogResult = System.Windows.Forms.DialogResult.OK
        'Form1.ind = DGCostco.CurrentRow.Index
        ''Me.Close()
        'Form1.btnselec_Click(Me, e)

    End Sub

    Private Sub Cancel_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Cancel_Button.Click
        Me.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.Close()
    End Sub

    Private Sub WebOrders_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Me.Location = New Point(Form1.Location.X + 810, Form1.Location.Y + 200)
        'Opacity = 0.9
    End Sub

    Private Sub DGCostco_CellDoubleClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles DGCostco.CellDoubleClick
        Try
            Dim ponum As String = DGCostco.Rows(DGCostco.CurrentRow.Index).Cells("OrdenLab").Value
            Form1.ind = DGCostco.CurrentRow.Index
            'Me.Close()
            Form1.btnselec_Click(Me, e)

        Catch ex As Exception
            MsgBox("Esta orden no esta disponible para procesar!", MsgBoxStyle.Exclamation)
        End Try
        If DGCostco.Rows(DGCostco.CurrentRow.Index).Cells("OrdenLab").Value.ToString.ToUpper = "PROCESADO" Then

        End If
    End Sub

    Private Sub DGCostco_DataSourceChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles DGCostco.DataSourceChanged
        Dim i As Integer = 0
        For i = 0 To DGCostco.Columns.Count - 1
            DGCostco.Columns(i).Visible = False
        Next
        For i = 0 To DGCostco.RowCount - 1
            If DGCostco.Rows(i).Cells("OrdenLab").Value.ToString.Length < 10 Then
                DGCostco.Rows(i).DefaultCellStyle.BackColor = Color.Red 'Cells("OrdenLab").Style.BackColor = Color.Red
                DGCostco.Rows(i).DefaultCellStyle.ForeColor = Color.Yellow
            End If
        Next       
        DGCostco.Columns("ordenlab").Visible = True
        DGCostco.Columns("name").Visible = True
        DGCostco.Columns("material").Visible = True
        DGCostco.Columns("diseno").Visible = True
        DGCostco.Columns("Esf D").Visible = True
        DGCostco.Columns("Cil D").Visible = True
        DGCostco.Columns("Adic D").Visible = True
        DGCostco.Columns("Esf I").Visible = True
        DGCostco.Columns("Cil I").Visible = True
        DGCostco.Columns("Adic I").Visible = True
        DGCostco.Columns("Comentarios").Visible = True
        DGCostco.Columns("TipoAR").Visible = True

    End Sub

    Private Sub WebOrders_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Activated
        TextOrdenLab.Text = ""
        TextOrdenLab.Focus()
    End Sub

    Private Sub TextOrdenLab_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles TextOrdenLab.KeyDown
        If (e.KeyCode = Keys.NumPad0 Or e.KeyCode = Keys.Space) And TextOrdenLab.Text.Length = 0 Then
            e.SuppressKeyPress = True
        Else
            If e.KeyCode = Keys.Enter Then
                If TextOrdenLab.Text.Length >= 10 And TextOrdenLab.Text.StartsWith("0") Then
                    TextOrdenLab.Text = TextOrdenLab.Text.Substring(1)
                End If
                Dim i As Integer = 0
                For i = 0 To DGCostco.Rows.Count - 1
                    If DGCostco.Item("OrdenLab", i).Value = TextOrdenLab.Text Then
                        'MsgBox(DGCostco.Item("OrdenLab", i).Value)
                        DGCostco.Item("OrdenLab", i).Selected = True
                        Exit For
                    End If
                Next
                If i <> DGCostco.Rows.Count Then
                    DGCostco_CellDoubleClick(Me, New DataGridViewCellEventArgs(0, i))
                Else
                    'Dim t As New SqlDB(My.Settings.VantageServer, My.Settings.DBUser, My.Settings.DBPassword, My.Settings.MfgSysDBName)
                    Dim t As New SqlDB
                    Dim Comm As SqlCommand

                    If My.Settings.LocalDBName = "verbien" Then
                        t = New SqlDB(My.Settings.LocalServer, My.Settings.DBUser, My.Settings.DBPassword, My.Settings.LocalDBName)

                    Else
                        t = New SqlDB(My.Settings.VantageServer, My.Settings.DBUser, My.Settings.DBPassword, My.Settings.MfgSysDBName)
                    End If

                    'Dim t As New SqlDB(My.Settings.VantageServer, My.Settings.DBUser, My.Settings.DBPassword, My.Settings.MfgSysDBName)
                    'Dim Comm As SqlCommand
                    Try
                        t.OpenConn()
                        Comm = New SqlCommand("[SP_CheckOrderByOrdenLab]", t.SQLConn)
                        Comm.CommandType = CommandType.StoredProcedure
                        Comm.CommandTimeout = My.Settings.DBCommandTimeout
                        Comm.Parameters.AddWithValue("@OrdenLab", TextOrdenLab.Text)
                        Comm.Parameters.Add("@Comments", Data.SqlDbType.VarChar, 500)
                        Comm.Parameters("@Comments").Direction = Data.ParameterDirection.Output
                        Comm.ExecuteNonQuery()
                        If Comm.Parameters("@Comments").Value <> "" Then
                            MsgBox(Comm.Parameters("@Comments").Value, MsgBoxStyle.Exclamation)
                        End If
                    Catch ex As Exception
                        If ex.Message.Contains("Error de Conexion") Then
                            Form1.ChangeWorkingStatus(Form1.WorkingStatusType.OffLine)
                        End If
                    Finally
                        t.CloseConn()
                    End Try

                End If
            End If
        End If
    End Sub
    

    'Private Sub TextOrdenLab_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TextOrdenLab.TextChanged
    '    If TextOrdenLab.Text.EndsWith(vbCrLf) Then
    '        MsgBox(TextOrdenLab.Text)
    '    End If
    'End Sub

    Private Sub DGCostco_CellContentClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles DGCostco.CellContentClick

    End Sub
End Class
