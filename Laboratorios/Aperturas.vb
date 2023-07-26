'' Clase para manejar la asignación de BINS
'' Mejora Continua AUGEN
''

Public Class Aperturas_WHBIN
    ' Clase para encapsular el manejo de los bins y las aperturas de órdenes

    ' Variables para almacenar los datos del último bin asignado o consultado
    Public ReadOnly idBinVB As Integer
    Public ReadOnly sColorBinVB As String
    Public ReadOnly sTipoBinVB As String
    Public ReadOnly sCicloId As String
    Public ReadOnly sBIN As String
    Public ReadOnly iApartados As Integer
    Public ReadOnly iCapacidad As Integer

    Private BinsManager As Sql_AperturasVB = New Sql_AperturasVB()
    Private Escuela_Abierta
    Private No_Mas_Vacantes As Boolean

    Public Sub New(ByVal ordenvntg As String, ByVal Rx As String)
        Dim mySqlMan As Sql_AperturasVB = New Sql_AperturasVB
        Dim idBinActual As Integer
        'Dim dsBin As DataSet
        'Dim CCT As String

        idBinVB = -1
        sColorBinVB = ""
        sTipoBinVB = ""
        sBIN = ""
        No_Mas_Vacantes = False
        ' Revisar si la escuela ya está abierta

        ' Procedimiento para obtener la clave de la escuela de una Rx de VerBien .. Rx que se encuentra en la tabla Paciente
        Try
            'idBinActual = mySqlManDameBinID(ordenvntg)
            idBinVB = idBinActual

            Escuela_Abierta = idBinActual > 0

            Try
                ''#ADD#JGARIBALDI#25 de Septiembre de 2014# Permite la asignacion directa(solo con el Rx) de bin 
                sBIN = mySqlMan.ObtenerBin(Rx)


                ''#ADD#JGARIBALDI#25 de Septiembre de 2014# Ya no será necesario
                ' '' CCT = mySqlMan.DameCCTEscuelaOrdernum(ordenvntg)
                ''If Not (Escuela_Abierta) And CCT <> "" Then idBinVB = AbreEscuela(ordenvntg) ' Asigna la escuela a un bin y guarda el id

                ''If idBinVB = -1 Then
                ''    sColorBinVB = "ERROR"
                ''    sTipoBinVB = "Falló Abrir Escuela"
                ''End If

                ' '' If No_Mas_Vacantes Then sBIN = "N/V"

                ''dsBin = mySqlMan.DameBinData(idBinVB)
                ''If Not (dsBin Is Nothing) And idBinVB <> -1 Then
                ''    sBIN = dsBin.Tables(0).Rows(0).Item("BIN").ToString()
                ''    iApartados = Convert.ToInt32(dsBin.Tables(0).Rows(0).Item("Apartado"))
                ''    iCapacidad = Convert.ToInt32(dsBin.Tables(0).Rows(0).Item("Capacidad"))
                ''    sColorBinVB = dsBin.Tables(0).Rows(0).Item("Color").ToString()
                ''    sTipoBinVB = mySqlMan.DameBinTipoData(Convert.ToInt32(dsBin.Tables(0).Rows(0).Item("idTipo"))).Tables(0).Rows(0).Item("Tipo").ToString()
                ''    sCicloId = dsBin.Tables(0).Rows(0).Item("idCiclo").ToString()
                ''    '''''AGREGADO
                ''    ' If sCicloId = 0 Then sCicloId = BinsManager.DameCicloID(Convert.ToInt32(ordenvntg))

                ' '' Guardamos un registro de RX ORDEN ESCUELA
                ''    'mySqlMan.GuardaOrdenBinEsc(idBinVB, mySqlMan.DameEscuelaID(CCT), Rx, ordenvntg, sCicloId)
                ''End If

            Catch ex As Exception
                'sColorBinVB = "ERROR"
                'sTipoBinVB = "ERROR"
            End Try

        Catch ex As Exception
            MsgBox("mySqlMan.DameBinID(" + ordenvntg.ToString() + ")" + vbCrLf + ex.Message, MsgBoxStyle.Critical, "Error al Asignar Bin")
        End Try

    End Sub

    ' Abre un bin para esta escuela
    'Public Function AbreEscuela(ByVal OrdenVtge As String) As Integer

    '    Dim CCT As String
    '    Dim NumRxProcesar As Integer
    '    Dim cicloID, idBinVacante As Integer
    '    Dim EsFundaciones As Boolean
    '    Dim dsBinData As DataSet
    '    Dim Capacidad As Integer
    '    Dim NumRxApartadas As Integer

    '    'CCT = BinsManager.DameCCTEscuelaOrdernum(Convert.ToInt32(OrdenVtge))


    '    If CCT.Length > 0 Then  ' Sólo si encontramos una escuela asociada

    '        EsFundaciones = (CCT.Length < 5)
    '        'cicloID = BinsManager.DameCicloID(Convert.ToInt32(OrdenVtge))

    '        ' En el constructor de la clase ya se revisó que no hay un bin para esta escuela
    '        ' No existe bin para esta escuela
    '        ' Apartamos un bin
    '        'NumRxProcesar = BinsManager.DameCantidadRxEscuela(CCT, cicloID)
    '        ' Buscamos un bin que :
    '        '   esté vacante
    '        '   tenga el tamaño lo más cercano al número de órdenes
    '        '   Si la escuela tiene menos de 10 Rx se asiganará a un bin temporal
    '        If NumRxProcesar > 0 Then
    '            If (NumRxProcesar <= My.Settings.WHBIN_Temp_Qty) Or EsFundaciones Then
    '                ' Esta es una escuela con pocas Rx, la asignaremos a un bin compartido
    '                If EsFundaciones Then
    '                    'Usar bin de fundaciones 'F1'
    '                    'idBinVacante = BinsManager.DameBinFundaciones()
    '                    'BinsManager.AsignaBinTempEsc(idBinVacante, CCT, cicloID, True, NumRxProcesar)
    '                Else
    '                    'Usar bin temporal y compartido
    '                    idBinVacante = BinsManager.DameBinVacante(NumRxProcesar, BinsManager.DameidTipoBin(True, True))
    '                    If idBinVacante >= 0 Then
    '                        BinsManager.AsignaBinTempEsc(idBinVacante, CCT, cicloID, True, NumRxProcesar)
    '                    Else
    '                        ' Error, no hay bins disponibles
    '                        ' Aquí tenemos dos opciones
    '                        '  1-Reportar el error
    '                        MsgBox("Error : No hay bins temporales disponibles", MsgBoxStyle.Exclamation, "Error : No hay BINS disponibles")
    '                        '  2-Crear un bin temporal nuevo
    '                        If 1 = BinsManager.CreaBinTemp() Then
    '                            MsgBox("Se ha creado un bin temporal nuevo", MsgBoxStyle.Information, "Creación de bin/caja temporal")
    '                        End If
    '                    End If

    '                End If
    '            Else
    '                ' Esta es una escuela con varias Rx, la asignaremos a un bin permamente
    '                ' hacer un for con steps del tamaño de los bins

    '                ' Primero revisamos si la orden puede entrar en un bin
    '                ' La función DameBinVacante regresa un bin que contenga toda la cantidad solicitada, o bien el bin con más vacantes disponibles
    '                idBinVacante = BinsManager.DameBinVacante(NumRxProcesar, BinsManager.DameidTipoBin(False, False))

    '                If idBinVacante <> -1 Then
    '                    ' Tenemos un bin, puede ser uno que contenga a toda la órden o uno de varios
    '                    dsBinData = BinsManager.DameBinData(idBinVacante)


    '                    If Not (dsBinData Is Nothing) Then
    '                        If dsBinData.Tables.Count > 0 Then
    '                            If dsBinData.Tables(0).Rows.Count > 0 Then
    '                                Capacidad = dsBinData.Tables(0).Rows(0).Item("Capacidad") 'Redundante, DameBinVacante ya revisó que todas las Rxs de esta escuela caben en el bin
    '                            End If

    '                            If (dsBinData.Tables(0).Rows(0).Item("Disponible") >= NumRxProcesar) Then
    '                                ' Este bin contiene a toda la orden
    '                                BinsManager.AsignaBinPermEsc(idBinVacante, CCT, cicloID, False, NumRxProcesar, Capacidad)
    '                            Else
    '                                ' Este es un bin parcial tenemos que apartar sólo la cantidad que cabe en este bin
    '                                ' NumRxProcesar es el total de Rxs para esta escuela
    '                                ' NumRxApartadas es el total de Rxs que ya están asignadas en bins

    '                                NumRxApartadas = BinsManager.DameTotalRxsBinPermanente(CCT)
    '                                Do
    '                                    ' Si la orden es muy grande hacemos parcialidades
    '                                    ' Primero revisamos si la orden puede entrar en un bin
    '                                    idBinVacante = BinsManager.DameBinVacante(NumRxProcesar, BinsManager.DameidTipoBin(False, False))
    '                                    If idBinVacante = -1 Then Continue Do


    '                                    If NumRxApartadas = -1 Then Continue Do

    '                                    BinsManager.AsignaBinPermEsc(idBinVacante, CCT, cicloID, False, NumRxProcesar - NumRxApartadas, Capacidad)

    '                                    NumRxApartadas = BinsManager.DameTotalRxsBinPermanente(CCT)
    '                                Loop While (NumRxProcesar - NumRxApartadas > 0 Or (idBinVacante = -1) Or (NumRxApartadas = -1))

    '                                If idBinVacante = -1 Then MsgBox("Lo siento, ya no hay BINS disponibles para asignar a esta orden." + vbCrLf + "Requeridos : " + NumRxProcesar.ToString() + " Apartados : " + NumRxApartadas.ToString(), MsgBoxStyle.Exclamation, "Faltan BINS")
    '                                If idBinVacante = -1 Then MsgBox("Lo siento, ocurrió un error al consultar el total de Rxs apartadas" + vbCrLf + "para esta escuela." + vbCrLf + "CCT : " + CCT, MsgBoxStyle.Exclamation, "Faltan BINS")
    '                            End If
    '                        End If
    '                    End If
    '                Else
    '                    ' No más vacantes
    '                    No_Mas_Vacantes = True
    '                End If
    '            End If
    '        End If
    '        'End If
    '    End If
    '    Return idBinVacante  'Regresa el bin asignado
    'End Function

    'Public Function DameUltimoBinID() As Integer
    '    Return idBinVB
    'End Function

    'Public Function DameUltimoBinColor() As String
    '    Return sColorBinVB
    'End Function

    'Public Function DameUltimoBinTipo() As String
    '    Return sTipoBinVB
    'End Function

    '' Manda a imprimir la etiqueta con la información del BIN
    'Public Sub ImprimeEtiqueta(ByVal idBIN As Integer, ByVal idEscuela As Integer)
    '    Dim dsBIN As DataSet

    '    dsBIN = BinsManager.DameBinData(idBIN)
    '    If Not (dsBIN Is Nothing) Then

    '    End If
    'End Sub
End Class

