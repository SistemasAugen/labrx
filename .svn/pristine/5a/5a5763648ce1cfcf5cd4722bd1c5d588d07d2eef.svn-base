Public Class BitacoraLabRx
    'Dim IsModifying As Boolean
    Dim PONumber As String
    Dim myordernum As Integer
    Dim EntryPerson As String
    Public FailedString As String


    Dim EntradaWeb As Bitacora
    Dim EntradaFisica As Bitacora
    Dim Captura As Bitacora
    Dim CanjePuntos As Bitacora
    Dim Garantia As Bitacora
    Dim Retenida As Bitacora
    Dim EnvioVirtual As Bitacora
    Dim BajarDeVirtual As Bitacora
    Dim TerminaSL As Bitacora
    Dim TerminaAR As Bitacora
    Dim TerminaFL As Bitacora
    Dim LiberaQA As Bitacora
    Dim RecepcionLocal As Bitacora
    Dim Reproceso As Bitacora

    Dim HasEntradaWeb As Boolean
    Dim HasEntradaFisica As Boolean
    Dim HasCaptura As Boolean
    Dim HasCanjePuntos As Boolean
    Dim HasGarantia As Boolean
    Dim HasRetenida As Boolean
    Dim HasEnvioVirtual As Boolean
    Dim HasBajarDeVirtual As Boolean
    Dim HasTerminaSL As Boolean
    Dim HasTerminaAR As Boolean
    Dim HasTerminaFL As Boolean
    Dim HasLiberaQA As Boolean
    Dim HasRecepcionLocal As Boolean
    Dim HasReproceso As Boolean



    ReadOnly Property Ordernum()
        Get
            Return myordernum
        End Get
    End Property


    Public Sub New(ByVal ordernum As Integer, ByVal ponum As String, ByVal entryperson As String)
        'Me.IsModifying = IsModifying
        Me.myordernum = Ordernum
        Me.PONumber = ponum
        Me.EntryPerson = entryperson

        HasEntradaWeb = False
        HasEntradaFisica = False
        HasCaptura = False
        HasCanjePuntos = False
        HasGarantia = False
        HasRetenida = False
        HasEnvioVirtual = False
        HasBajarDeVirtual = False
        HasTerminaSL = False
        HasTerminaAR = False
        HasTerminaFL = False
        HasLiberaQA = False
        HasRecepcionLocal = False
        HasReproceso = False
        FailedString = ""
    End Sub

    Public Sub AddEntradaWeb(ByVal trandate As String, ByVal comments As String)
        HasEntradaWeb = True
        EntradaWeb = New Bitacora(PONumber, myordernum, Bitacora.Transactions.REG_WEB, trandate, EntryPerson, comments)
    End Sub
    Public Sub AddEntradaFisica(ByVal trandate As String, ByVal comments As String)
        HasEntradaFisica = True
        EntradaFisica = New Bitacora(PONumber, myordernum, Bitacora.Transactions.ENTRADA_FISICA, trandate, EntryPerson, comments)
    End Sub
    Public Sub AddCaptura(ByVal trandate As String, ByVal comments As String)
        HasCaptura = True
        Captura = New Bitacora(PONumber, myordernum, Bitacora.Transactions.CAPTURA, trandate, EntryPerson, comments)
    End Sub
    Public Sub AddCanjePuntos(ByVal trandate As String, ByVal comments As String)
        HasCanjePuntos = True
        CanjePuntos = New Bitacora(PONumber, myordernum, Bitacora.Transactions.CANJE_PUNTOS, trandate, EntryPerson, comments)
    End Sub
    Public Sub AddRetenida(ByVal trandate As String, ByVal comments As String)
        HasRetenida = True
        Retenida = New Bitacora(PONumber, myordernum, Bitacora.Transactions.RETENIDA, trandate, EntryPerson, comments)
    End Sub
    Public Sub AddGarantia(ByVal trandate As String, ByVal comments As String)
        HasGarantia = True
        Garantia = New Bitacora(PONumber, myordernum, Bitacora.Transactions.GARANTIA, trandate, EntryPerson, comments)
    End Sub
    Public Sub AddEnvioVirtual(ByVal trandate As String, ByVal comments As String)
        HasEnvioVirtual = True
        EnvioVirtual = New Bitacora(PONumber, myordernum, Bitacora.Transactions.ENVIO_VIRTUAL, trandate, EntryPerson, comments)
    End Sub
    Public Sub AddBajarDeVirtual(ByVal trandate As String, ByVal comments As String)
        HasBajarDeVirtual = True
        BajarDeVirtual = New Bitacora(PONumber, myordernum, Bitacora.Transactions.RECEP_REMOTO, trandate, EntryPerson, comments)
    End Sub
    Public Sub AddTerminaSL(ByVal trandate As String, ByVal comments As String)
        HasTerminaSL = True
        TerminaSL = New Bitacora(PONumber, myordernum, Bitacora.Transactions.TERM_SL, trandate, EntryPerson, comments)
    End Sub
    Public Sub AddTerminaAR(ByVal trandate As String, ByVal comments As String)
        HasTerminaAR = True
        TerminaAR = New Bitacora(PONumber, myordernum, Bitacora.Transactions.TERM_AR, trandate, EntryPerson, comments)
    End Sub
    Public Sub AddTerminaFL(ByVal trandate As String, ByVal comments As String)
        HasTerminaFL = True
        TerminaFL = New Bitacora(PONumber, myordernum, Bitacora.Transactions.TERM_FL, trandate, EntryPerson, comments)
    End Sub
    Public Sub AddLiberaQA(ByVal trandate As String, ByVal comments As String)
        HasLiberaQA = True
        LiberaQA = New Bitacora(PONumber, myordernum, Bitacora.Transactions.LIB_QA, trandate, EntryPerson, comments)
    End Sub
    Public Sub AddRecepcionLocal(ByVal trandate As String, ByVal comments As String)
        HasRecepcionLocal = True
        RecepcionLocal = New Bitacora(PONumber, myordernum, Bitacora.Transactions.RECEP_VIRTUAL, trandate, EntryPerson, comments)
    End Sub
    Public Sub AddReproceso(ByVal trandate As String, ByVal comments As String)
        HasReproceso = True
        Reproceso = New Bitacora(PONumber, myordernum, Bitacora.Transactions.REPROCESO, trandate, EntryPerson, comments)
    End Sub

    Sub SetBitacora(ByVal t As SqlDB, ByVal tran As SqlClient.SqlTransaction)
        Dim CanjePuntos As Boolean = False
        Dim Garantia As Boolean = False
        Dim Retenida As Boolean = False
        Dim EnvioVirtual As Boolean = False
        Dim BajarDeVirtual As Boolean = False
        Dim TerminaSL As Boolean = False
        Dim TerminaAR As Boolean = False
        Dim TerminaFL As Boolean = False
        Dim LiberaQA As Boolean = False
        Dim RecepcionLocal As Boolean = False
        Dim Reproceso As Boolean = False

        Dim SQLStr As String = "select openorder,coalesce(ordernum,0) as ordernum,coalesce(date02,'1/1/2000') as date02,coalesce(date03,'1/1/2000') as date03,coalesce(date04,'1/1/2000') as date04, " & _
                "coalesce(date05,'1/1/2000') as date05,coalesce(date06,'1/1/2000') as date06,coalesce(date07,'1/1/2000') as date07,coalesce(date08,'1/1/2000') as date08, " & _
                "coalesce(date09,'1/1/2000') as date09,coalesce(date11,'1/1/2000') as date11,coalesce(userdate1,'1/1/2000') as userdate1,coalesce(userdate2,'1/1/2000') as userdate2, " & _
                "coalesce(checkbox06,0) as cb06,coalesce(checkbox07,0) as cb07,coalesce(checkbox08,0) as cb08,coalesce(checkbox09,0) as cb09,coalesce(checkbox10,0) as cb10,coalesce(checkbox11,0) as cb11, " & _
                "coalesce(checkbox12,0) as cb12,coalesce(checkbox16,0) as cb16,coalesce(checkbox17,0) as cb17,coalesce(checkbox18,0) as cb18,coalesce(shortchar01,'') as sc01, " & _
                "coalesce(entryperson,'') as entryperson,coalesce(changedby,'') as changedby,coalesce(ccamount,0.0) as ccamount from orderhed with(nolock) where ordernum = " & myordernum
        Dim ds As DataSet
        ds = t.SQLDS(SQLStr, "t1", tran)
        If ds.Tables("t1").Rows.Count > 0 Then
            With ds.Tables("t1").Rows(0)
                If .Item("cb17") = 1 Then
                    HasCanjePuntos = False
                End If
                If .Item("cb18") = 1 Then
                    HasGarantia = False
                End If
                If .Item("cb08") = 1 Then
                    HasRetenida = False
                End If
                If .Item("cb09") = 1 Then
                    HasEnvioVirtual = False
                End If
                If .Item("cb10") = 1 Then
                    HasBajarDeVirtual = False
                End If
                If .Item("cb09") = 1 And (.Item("date03") <> Nothing) Then
                    HasBajarDeVirtual = False
                End If

            End With
        End If

    End Sub

    Public Function InsertBitacora(ByVal t As SqlDB, ByVal tran As SqlClient.SqlTransaction) As Boolean
        Dim Succesful As Boolean = False

        Try
            If HasEntradaWeb Then
                'EntradaWeb = New Bitacora(5, 5, Bitacora.Transactions.ABRIR, Now.ToString, "yo", "")
                'HasEntradaWeb = True
                If Not EntradaWeb.InsertIntoDatabase(t, tran) Then
                    Throw New Exception("No se pudo insertar registro de Entrada Web: " & EntradaWeb.FailedStatus)
                End If
            End If
            If HasEntradaFisica Then
                If Not EntradaFisica.InsertIntoDatabase(t, tran) Then
                    Throw New Exception("No se pudo insertar registro de Entrada Fisica")
                End If
            End If
            If HasCaptura Then
                If Not Captura.InsertIntoDatabase(t, tran) Then
                    Throw New Exception("No se pudo insertar registro de Captura")
                End If
            End If
            If HasCanjePuntos Then
                If Not CanjePuntos.InsertIntoDatabase(t, tran) Then
                    Throw New Exception("No se pudo insertar registro de Canje de Puntos")
                End If
            End If
            If HasGarantia Then
                If Not Garantia.InsertIntoDatabase(t, tran) Then
                    Throw New Exception("No se pudo insertar registro de Garantia")
                End If
            End If
            If HasRetenida Then
                If Not Retenida.InsertIntoDatabase(t, tran) Then
                    Throw New Exception("No se pudo insertar registro de Retenida")
                End If
            End If
            If HasEnvioVirtual Then
                If Not EnvioVirtual.InsertIntoDatabase(t, tran) Then
                    Throw New Exception("No se pudo insertar registro de Envio Virtual")
                End If
            End If
            If HasBajarDeVirtual Then
                If Not BajarDeVirtual.InsertIntoDatabase(t, tran) Then
                    Throw New Exception("No se pudo insertar registro de Envio Bajar De Virtual")
                End If
            End If
            If HasTerminaSL Then
                If Not TerminaSL.InsertIntoDatabase(t, tran) Then
                    Throw New Exception("No se pudo insertar registro de Termina GP")
                End If
            End If
            If HasTerminaAR Then
                If Not TerminaAR.InsertIntoDatabase(t, tran) Then
                    Throw New Exception("No se pudo insertar registro de Termina AR")
                End If
            End If
            If HasTerminaFL Then
                If Not TerminaFL.InsertIntoDatabase(t, tran) Then
                    Throw New Exception("No se pudo insertar registro de Termina BM")
                End If
            End If
            If HasLiberaQA Then
                If Not LiberaQA.InsertIntoDatabase(t, tran) Then
                    Throw New Exception("No se pudo insertar registro de Liberacion QA")
                End If
            End If
            If HasRecepcionLocal Then
                If Not RecepcionLocal.InsertIntoDatabase(t, tran) Then
                    Throw New Exception("No se pudo insertar registro de Recepcion Local")
                End If
            End If
            If HasReproceso Then
                If Not Reproceso.InsertIntoDatabase(t, tran) Then
                    Throw New Exception("No se pudo insertar registro de Reproceso")
                End If
            End If
            Succesful = True

        Catch ex As Exception
            Succesful = False
            FailedString = ex.Message
        Finally
        End Try
        Return Succesful

    End Function


End Class
