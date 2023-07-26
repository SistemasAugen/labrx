Imports System.Text
Imports System.Security.Cryptography
Imports System.Data.SqlClient


Public Class MiscCharges

    Dim ordernum As Integer
    Dim t As SqlDB
    Dim userName As String
    Dim dsMiscDtl As DataSet
    Dim TotAmt As Single
    Dim SqlStrConn As String = "user ID= " & My.Settings.DBUser & " ;password = " & My.Settings.DBPassword & " ;database=" & My.Settings.LocalDBName & ";server=" & My.Settings.LocalServer & ";Connect Timeout=11"


    Public Sub New(ByVal pOrderNum As Integer, ByVal RxNum As Integer, ByVal TotalAmt As Single, ByVal usern As String)

        ' This call is required by the Windows Form Designer.
        InitializeComponent()
        ordernum = pOrderNum
        ' Add any initialization after the InitializeComponent() call.
        tbOrderNum.Text = pOrderNum.ToString()
        tbRxNum.Text = RxNum.ToString()
        TotAmt = TotalAmt
        tbMontoTotal.Text = TotalAmt.ToString("c")
        userName = usern
        txtMiscAmount.Text = "0.00"

        ' Carga el combobox con las razones de cargos miscelaneos
        GetInvoiceConcepts()

        ' Se llenara el grid con los cargos asignados a la orden.
        UpdateMiscList(pOrderNum)

    End Sub
    ' Pedro Farías L.  Ene 11 2013
    ' Esta función se movió al archivo Form1, para que al crear la orden ya tengo su monto en ccamount

    'Public Function RecalcTotal() As Boolean
    '    Dim Cnn As SqlClient.SqlConnection = Nothing
    '    Dim SqlCmd As SqlClient.SqlCommand
    '    Dim Sucess As Boolean = False
    '    Dim RespVal As Integer
    '    Dim newTotal As Double

    '    Try
    '        Cnn = New SqlClient.SqlConnection(SqlStrConn)
    '        Cnn.Open()

    '        SqlCmd = New SqlClient.SqlCommand("select (select sum(orderqty*unitprice) from orderdtl where ordernum=@Ordernum and voidline=0 and company=@Company) + coalesce((SELECT sum(miscamt) from ordermsc  where ordernum=@Ordernum and company=@Company),0)", Cnn)
    '        SqlCmd.CommandType = CommandType.Text
    '        SqlCmd.CommandTimeout = 25

    '        SqlCmd.Parameters.AddWithValue("@Company", "TRECEUX")
    '        SqlCmd.Parameters.AddWithValue("@Ordernum", ordernum)
    '        newTotal = SqlCmd.ExecuteScalar()

    '        If IsNumeric(newTotal) Then
    '            SqlCmd.Dispose()
    '            SqlCmd = New SqlClient.SqlCommand("UPDATE orderhed SET ccamount=@newtotal WHERE ordernum=@ordernum and company=@company", Cnn)
    '            SqlCmd.CommandType = CommandType.Text
    '            SqlCmd.CommandTimeout = 25

    '            SqlCmd.Parameters.AddWithValue("@Company", "TRECEUX")
    '            SqlCmd.Parameters.AddWithValue("@Ordernum", ordernum)
    '            SqlCmd.Parameters.AddWithValue("@newtotal", newTotal)
    '            SqlCmd.ExecuteNonQuery()
    '        End If

    '        Select Case RespVal
    '            Case 0
    '                Sucess = True
    '            Case -4
    '                Sucess = False
    '                MessageBox.Show("No se puede agregar ajuste porque la orden ya esta cerrada", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Information)
    '        End Select

    '    Catch ex As SqlClient.SqlException
    '        Sucess = False
    '        Throw New Exception(ex.Message)
    '    Finally

    '        Cnn.Close()
    '    End Try

    '    Return Sucess
    'End Function


    Private Function AddMiscCharge(ByVal Comment As String, ByVal TxID As String, ByVal TranType As Integer) As Boolean
        Dim Cnn As SqlClient.SqlConnection = Nothing
        Dim SqlCmd As SqlClient.SqlCommand
        Dim Sucess As Boolean = False
        Dim RespVal As Integer
        Dim newTotal As Double

        Try
            Cnn = New SqlClient.SqlConnection(SqlStrConn)
            Cnn.Open()

            SqlCmd = New SqlClient.SqlCommand("spAgregaCargoMisc", Cnn)
            SqlCmd.CommandType = CommandType.StoredProcedure
            SqlCmd.CommandTimeout = My.Settings.DBCommandTimeout
            SqlCmd.Parameters.AddWithValue("@Company", "TRECEUX")
            SqlCmd.Parameters.AddWithValue("@Ordernum", tbOrderNum.Text)
            SqlCmd.Parameters.AddWithValue("@MisDesc", ComboMiscDesc.Text)
            SqlCmd.Parameters.AddWithValue("@MisAmt", txtMiscAmount.Text)
            SqlCmd.Parameters.AddWithValue("@Comment", Comment)
            SqlCmd.Parameters.AddWithValue("@TxID", TxID)
            SqlCmd.Parameters.AddWithValue("@TranType", TranType)
            SqlCmd.Parameters.AddWithValue("@Username", userName)

            SqlCmd.Parameters.Add("@Msg", Data.SqlDbType.Int)
            SqlCmd.Parameters("@Msg").Direction = Data.ParameterDirection.Output

            SqlCmd.ExecuteNonQuery()
            RespVal = SqlCmd.Parameters("@Msg").Value

            SqlCmd.Dispose()
            SqlCmd = New SqlClient.SqlCommand("select (select sum(orderqty*unitprice) from orderdtl where ordernum=@Ordernum and voidline=0 and company=@Company) + coalesce((SELECT sum(miscamt) from ordermsc  where ordernum=@Ordernum and company=@Company),0)", Cnn)
            SqlCmd.CommandType = CommandType.Text
            SqlCmd.CommandTimeout = My.Settings.DBCommandTimeout

            SqlCmd.Parameters.AddWithValue("@Company", "TRECEUX")
            SqlCmd.Parameters.AddWithValue("@Ordernum", ordernum)
            newTotal = SqlCmd.ExecuteScalar()

            If IsNumeric(newTotal) Then
                SqlCmd.Dispose()
                SqlCmd = New SqlClient.SqlCommand("UPDATE orderhed SET ccamount=@newtotal WHERE ordernum=@ordernum and company=@company", Cnn)
                SqlCmd.CommandType = CommandType.Text
                SqlCmd.CommandTimeout = My.Settings.DBCommandTimeout

                SqlCmd.Parameters.AddWithValue("@Company", "TRECEUX")
                SqlCmd.Parameters.AddWithValue("@Ordernum", tbOrderNum.Text)
                SqlCmd.Parameters.AddWithValue("@newtotal", newTotal)
                SqlCmd.ExecuteNonQuery()
            End If

            Select Case RespVal
                Case 0
                    Sucess = True
                Case -4
                    Sucess = False
                    MessageBox.Show("No se puede agregar ajuste porque la orden ya esta cerrada", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Information)
            End Select

        Catch ex As SqlClient.SqlException
            Sucess = False
            Throw New Exception(ex.Message)
        Finally

            Cnn.Close()
        End Try

        Return Sucess
    End Function

    Private Sub DelMiscCharge(ByVal linenum As Integer)
        Dim newTotal As Double
        Dim Cnn As SqlClient.SqlConnection = Nothing
        Dim SqlCmd As SqlClient.SqlCommand
        Dim RespVal As Integer

        Try
            Cnn = New SqlClient.SqlConnection(SqlStrConn)
            Cnn.Open()

            SqlCmd = New SqlClient.SqlCommand("spBorraCargoMisc", Cnn)
            SqlCmd.CommandType = CommandType.StoredProcedure
            SqlCmd.CommandTimeout = My.Settings.DBCommandTimeout

            SqlCmd.Parameters.AddWithValue("@Company", "TRECEUX")
            SqlCmd.Parameters.AddWithValue("@Ordernum", tbOrderNum.Text)
            SqlCmd.Parameters.AddWithValue("@Seqnum", linenum)

            SqlCmd.Parameters.Add("@Msg", SqlDbType.Int)
            SqlCmd.Parameters("@Msg").Direction = ParameterDirection.Output

            SqlCmd.ExecuteNonQuery()
            RespVal = SqlCmd.Parameters("@Msg").Value

            SqlCmd.Dispose()
            SqlCmd = New SqlClient.SqlCommand("select (select sum(orderqty*unitprice) from orderdtl where ordernum=@Ordernum and voidline=0 and company=@Company) + coalesce((SELECT sum(miscamt) from ordermsc  where ordernum=@Ordernum and company=@Company),0)", Cnn)
            SqlCmd.CommandType = CommandType.Text
            SqlCmd.CommandTimeout = My.Settings.DBCommandTimeout

            SqlCmd.Parameters.AddWithValue("@Company", "TRECEUX")
            SqlCmd.Parameters.AddWithValue("@Ordernum", ordernum)
            newTotal = SqlCmd.ExecuteScalar()

            'En caso de que no existan misceláneos debe regresar solo la suma de las líneas del detalle.

            If IsNumeric(newTotal) Then
                SqlCmd.Dispose()
                SqlCmd = New SqlClient.SqlCommand("UPDATE orderhed SET ccamount=@newtotal WHERE ordernum=@ordernum and company=@company", Cnn)
                SqlCmd.CommandType = CommandType.Text
                SqlCmd.CommandTimeout = My.Settings.DBCommandTimeout
                SqlCmd.Parameters.AddWithValue("@Company", "TRECEUX")
                SqlCmd.Parameters.AddWithValue("@Ordernum", tbOrderNum.Text)
                SqlCmd.Parameters.AddWithValue("@newtotal", newTotal)
                SqlCmd.ExecuteNonQuery()
            End If

            If RespVal = -4 Then
                MessageBox.Show("No se puede quitar ajuste porque la orden ya esta cerrada", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Information)
            End If
    

        Catch ex As SqlClient.SqlException
            MessageBox.Show(ex.Message, "Error al Elminar Cargo", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally

            Cnn.Close()
        End Try
    End Sub

    Private Sub GetInvoiceConcepts()
        Dim Cnn As New SqlConnection(SqlStrConn)
        Dim SqlStr As String
        Dim SqlCmd As New SqlCommand
        Dim dr As DataRow
        Dim ds As New DataSet
        Dim da As SqlDataAdapter

        Try
            Cnn.Open()
            SqlStr = "SELECT * FROM TblFactConceptos with(nolock)"

            da = New SqlDataAdapter(SqlStr, Cnn)
            da.Fill(ds, "t1")

            dr = ds.Tables(0).NewRow()
            dr.Item(0) = "0"
            dr.Item(1) = "ELIGE UNA OPCION"
            ds.Tables(0).Rows.InsertAt(dr, 0)

            ComboMiscDesc.DataSource = ds.Tables("t1")
            ComboMiscDesc.DisplayMember = "FactName"
            ComboMiscDesc.ValueMember = "FactAmt"
            ComboMiscDesc.SelectedIndex = 0
        Catch ex As Exception
            MessageBox.Show("No se pudieron cargar los conceptos de ajuste!", "Mensaje de Error", MessageBoxButtons.OK, MessageBoxIcon.Information)

        Finally
            Cnn.Close()

        End Try

    End Sub

    Private Function GetMiscCharges(ByVal ordern As Integer) As DataSet
        Dim Cnn As New SqlConnection(SqlStrConn)
        Dim SqlStr As String
        Dim SqlCmd As New SqlCommand
        Dim ds As New DataSet
        Dim da As SqlDataAdapter

        Try
            Cnn.Open()
            SqlStr = "spMiscDtl"

            da = New SqlDataAdapter(SqlStr, Cnn)
            da.SelectCommand.CommandType = CommandType.StoredProcedure
            da.SelectCommand.Parameters.AddWithValue("@Company", "TRECEUX")
            da.SelectCommand.Parameters.AddWithValue("@Ordernum", ordern)
            da.Fill(ds, "t1")

        Catch ex As Exception
            MessageBox.Show("No se pudieron cargar los conceptos de ajuste!", "Mensaje de Error", MessageBoxButtons.OK, MessageBoxIcon.Information)

        Finally
            Cnn.Close()

        End Try

        Return ds
    End Function

    Private Sub fillListviewMisc(ByVal dsm As DataSet)
        Dim dr As DataRow
        Dim totalMisc As Single = 0
        LV_MiscCharges.Items.Clear()

        For Each dr In dsm.Tables(0).Rows
            Dim MyItemMisc As New ListViewItem(dr(0).ToString())
            MyItemMisc.SubItems.Add(dr(1))
            MyItemMisc.SubItems.Add(dr(2))

            ' Suma los cargos miscelaneos existentes en la orden
            totalMisc += dr(2)

            ' Add the items to the ListView.
            LV_MiscCharges.Items.AddRange(New ListViewItem() {MyItemMisc})

        Next

        tbMontoTotal.Text = (TotAmt + totalMisc).ToString("c")

    End Sub

    ' Metodo que actualiza la lista de cargos miscelaneos
    Private Sub UpdateMiscList(ByVal ordern As Integer)
        dsMiscDtl = GetMiscCharges(ordern)
        fillListviewMisc(dsMiscDtl)
    End Sub

    Private Sub btnAceptar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAceptar.Click
        Me.Close()
    End Sub

    Private Sub AddSATFolios_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        tbOrderNum.Focus()
    End Sub

    Private Sub BtnAddMiscCharge_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnAddMiscCharge.Click
        Dim amount As Single
        Dim amount2 As Int16

        Try
            amount = Convert.ToSingle(txtMiscAmount.Text)
            amount2 = Convert.ToInt16(amount)
            txtMiscAmount.Text = amount

        Catch ex As Exception
            amount = 0
            MsgBox("El monto del ajuste es inválido: " & vbCrLf & txtMiscAmount.Text, MsgBoxStyle.Exclamation)

        End Try

        If (ComboMiscDesc.SelectedIndex = 0) Then
            MessageBox.Show("Elige una razon de ajuste.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Return
        End If

        If amount <> 0 Then
            Try
                Dim MiscCharge As Single = CSng(ComboMiscDesc.SelectedValue)
                Dim MiscAmount As Single = CSng(txtMiscAmount.Text)

                Dim OrderAmt As Single = CSng(tbMontoTotal.Text)

                If (MiscAmount <> 0 Or MiscCharge <> 0) And (OrderAmt + MiscAmount >= 0) Then
                    Me.Cursor = Cursors.WaitCursor
                    Dim MiscDesc As String = UCase(ComboMiscDesc.Text)
                    Dim ReadyToAdjust As Boolean = True
                    Dim NewCharge As Single = MiscCharge - OrderAmt

                    If MiscCharge > 0 Then
                        txtMiscAmount.Text = NewCharge
                    End If

                    'If AlreadyClosed(tbOrderNum.Text) And (CheckGarantia2.Checked Or CBX_Cortesia.Checked) Then
                    '    Throw New Exception("Esta Rx no puede ser marcada como Garantía o Cortesía porque ya ha sido cerrada previamente con precio!")
                    'End If

                    If MiscDesc.Contains("PRUEBA") Then
                        Dim ev As New EditValues
                        ev.LabelValue.Text = "Quien autorizó la prueba?"
                        ev.ShowDialog()
                        If ev.Changed Then
                            If ev.TextValue.Text.Trim() = "" Then
                                MiscDesc += " " + UCase(userName)
                            End If
                            MiscDesc += " " + UCase(ev.TextValue.Text.Replace("'", " "))
                            ReadyToAdjust = True
                        Else
                            ReadyToAdjust = False
                        End If
                    End If

                    'Aqui revisar si queda en 0 o no
                    'If (OrderAmt + MiscAmount = 0) And Not CBX_Cortesia.Checked And Not CheckGarantia2.Checked Then
                    '    ReadyToAdjust = False
                    '    Throw New Exception("La orden no puede ser ajustada a 0 pesos sin ser marcada como GARANTIA o CORTESIA")
                    'End If

                    If (Len(Trim(MiscDesc)) > 0) And ReadyToAdjust Then

                        Try
                            Dim Comment As String
                            Dim TipoMovimiento As New Bitacora.Transactions

                            Comment = "Monto: " & String.Format("{0:c}", CSng(txtMiscAmount.Text)) & " [" & ComboMiscDesc.Text & "]"

                            ' Agrega el cargo miscelaneo a la orden y actualiza el monto total.
                            If AddMiscCharge(Comment, Bitacora.Transactions.AJUSTES, 3) Then
                                UpdateMiscList(tbOrderNum.Text)
                            End If

                            Me.txtMiscAmount.Text = "0.00"
                            ComboMiscDesc.SelectedIndex = 0
                            txtMiscAmount.Focus()
                            txtMiscAmount.SelectAll()

                        Catch ex As SqlClient.SqlException
                            MsgBox(ex.Message, MsgBoxStyle.Critical)

                        Finally

                        End Try

                    End If
                Else
                    If MiscAmount = 0 Then
                        MsgBox("El Monto del Cargo Miscelaneo debe ser distinto a Cero.", MsgBoxStyle.Critical)
                    Else
                        MsgBox("El Monto del Cargo Miscelaneo es inválido, el Total será negativo.", MsgBoxStyle.Critical)
                        txtMiscAmount.Text = 0
                    End If
                End If

            Catch ex As Exception
                MsgBox("Error al registrar Ajustes " + vbCrLf + ex.Message, MsgBoxStyle.Critical)

            Finally
                Me.Cursor = Cursors.Default
            End Try

        End If

    End Sub

    Private Function AlreadyClosed(ByVal Ordernum As Integer) As Boolean
        Dim OK As Boolean = False
        Dim t As New SqlDB()
        t.SQLConn = New SqlConnection()
        t.SQLConn.ConnectionString = SqlStrConn

        Try
            t.OpenConn()
            Dim sqlstr As String = "select * from tblbitacora with(nolock) where ordernum = " & Ordernum & " and TxID ='CIERRE'"
            Dim ds As New DataSet

            ds = t.SQLDS(sqlstr, "t1")
            If ds.Tables("t1").Rows.Count > 0 Then
                OK = True
            End If
        Catch ex As Exception
            Throw New Exception("Error al verificar si la Rx ha sido cerrada anteriormente." & vbCrLf & ex.Message)
        Finally
            t.CloseConn()
        End Try
        Return OK
    End Function

    ' Metodo para eliminar cargos miscelaneos de las recetas
    Private Sub BtnDelMiscCharge_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnDelMiscCharge.Click

        If (LV_MiscCharges.SelectedItems.Count > 1) Then
            MessageBox.Show("Debes seleccionar una sola linea a borrar", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information)
        Else
            DelMiscCharge(LV_MiscCharges.SelectedItems.Item(0).Text)
            UpdateMiscList(tbOrderNum.Text)
        End If

    End Sub

    Private Sub ComboMiscDesc_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ComboMiscDesc.SelectedIndexChanged
        txtMiscAmount.Focus()
        txtMiscAmount.SelectAll()
    End Sub

    Private Sub txtMiscAmount_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtMiscAmount.KeyPress
        If e.KeyChar = Chr(13) Then
            BtnAddMiscCharge_Click(sender, Nothing)
        End If
    End Sub
End Class