Public Class Bitacora
    Private PONumber As String
    Private Ordernum As Integer
    Private TxID As Transactions
    Private TxDate As String
    Private Username As String
    Private Observations As String
    Private Inserted As Boolean
    Private ErrorMessage As String

    Public ReadOnly Property FailedStatus() As String
        Get
            Return ErrorMessage
        End Get
    End Property

    Public ReadOnly Property IsInserted() As Boolean
        Get
            Return Inserted
        End Get
    End Property

    Enum Transactions
        AJUSTES
        CAPTURA
        CAPTURA_ARM
        CIERRE
        COBRO_FACT
        COBRO_MILAB
        ENVIO_VBPAM
        ENVIO_VIRTUAL
        FACT
        GARANTIA
        LIB_VBPAM
        RECAPTURA
        RECEP_COORD
        RECEP_VIRTUAL
        REG_MILAB
        REGVBPAM_File
        REGVBPAM_Man
        REGWEB_COSTCO
        REG_WEB
        TERM_FL
        ABRIR
        LIB_QA
        RECEP_REMOTO
        REPROCESO
        TERM_SL
        TERM_AR
        CANJE_PUNTOS
        ENTRADA_FISICA
        RETENIDA
        ENVIO_CADENA
        RECEP_CADENA
        REGVBPAM_FUND
    End Enum

    Public Sub New(ByVal ponum As String, ByVal ordernum As Integer, ByVal txid As Transactions, ByVal txdate As String, ByVal username As String, ByVal observations As String)
        Me.PONumber = ponum
        Me.Ordernum = ordernum
        Me.TxID = txid
        Me.TxDate = txdate
        Me.Username = username
        Me.Observations = observations
        Me.Inserted = False
        Me.ErrorMessage = ""
    End Sub
    Private Sub CheckDataLengths()
        If PONumber.Length > 50 Then PONumber = PONumber.Substring(0, 50)
        'If TxID.ToString.Length > 15 Then TxID.ToString.Substring(0, 15)
        If Username.Length > 20 Then Username = Username.Substring(0, 20)
        Observations = Observations.Replace(",", " ")
        If Observations.Length > 150 Then Observations = Observations.Substring(0, 150)

    End Sub
    Public Function InsertIntoDatabase(ByVal t As SqlDB, ByVal tran As SqlClient.SqlTransaction) As Boolean
        Dim Successful As Boolean = False
        CheckDataLengths()
        Dim SQLStr As String = "insert into TblBitacora (ponumber,ordernum,txid,txdate,username,observations,sincronizada) values ('" & PONumber & "'," & Ordernum & ",'" & TxID.ToString & "','" & TxDate & "','" & Username & "',cast('" & Observations.Replace("'", "") & "' as varchar(100)),0)"
        Try
            If t.Transaction(SQLStr, tran, ErrorMessage) Then
                Inserted = True
            Else
                Throw New Exception("Error al insertar registro de bitacora: " & ErrorMessage)
            End If
        Catch ex As Exception
            Inserted = False
            ErrorMessage = ex.Message
        Finally
            Successful = Inserted
        End Try
        Return Successful
    End Function
End Class
