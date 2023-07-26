Imports System.Data
Imports System.Data.SqlClient

Public Class Sql_AperturasVB
    ''' Operaciones de SQL para las aperturas de VB y BINS

    Dim mySql As SqlDB

    Public Sub New()
        mySql = New SqlDB(My.Settings.LocalServer, My.Settings.DBUser, My.Settings.DBPassword, My.Settings.LocalDBName)
        mySql.OpenConn()

    End Sub




    ''#ADD#JGARIBALDI#25 de Septiembre de 2014# funcion que llama al procedimiento que da el nuevo bin, recibiendo como parametro unicamente la Rx
    Public Function ObtenerBin(ByVal Rx As String) As String

        Dim output As SqlParameter = New SqlParameter("@resp", SqlDbType.VarChar, 50)
        Try

            output.Direction = ParameterDirection.Output
            Dim mySCMD As SqlCommand
            mySCMD = New SqlCommand()
            mySCMD.CommandText = "SP_WHBIN_AsignaBin"
            mySCMD.Parameters.AddWithValue("@Rx", Rx)
            mySCMD.Parameters.Add(output)

            mySql.Execute_StoreProcedure_Param(mySCMD)
            Return output.Value.ToString()

        Catch
            Return Nothing
        End Try

    End Function



    ''#MOD#JGARIBALDI#25 de Septiembre de 2014# Se comentó ya que no será necesario por la nueva manera de asignar binn (solo con la Rx)
    '' Procedimiento para obtener la clave de la escuela de una Rx de VerBien .. Rx que se encuentra en la tabla Paciente

    'Public Function DameCCTEscuela(ByVal RxVB As String) As String
    '    Try
    '        Return mySql.ExecuteScalar("Select TOP 1 ClaveEscuela from VBPAM_Paciente WITH(NoLock) WHERE Rx='" + RxVB + "'").ToString()
    '    Catch ex As Exception
    '        Return ""
    '    End Try
    'End Function


    '' Procedimiento para obtener la clave de la escuela de una órden Vantage .. ordernum que se encuentra en la tabla orderhed
    'Public Function DameCCTEscuelaOrdernum(ByVal Ordernum As String) As String

    '    Dim output As SqlParameter = New SqlParameter("@CCT", SqlDbType.VarChar, 50)
    '    Try

    '        output.Direction = ParameterDirection.Output
    '        Dim mySCMD As SqlCommand
    '        mySCMD = New SqlCommand()
    '        mySCMD.CommandText = "SP_WHBIN_EscuelaOrden"
    '        mySCMD.Parameters.AddWithValue("@Ordernum", Ordernum)
    '        mySCMD.Parameters.Add(output)

    '        mySql.Execute_StoreProcedure_Param(mySCMD)
    '        Return output.Value.ToString()

    '    Catch
    '        Return Nothing
    '    End Try

    'End Function

    '' Obtiene el id del BIN al cual se debe almacenar esta orden
    'Public Function DameBinID(ByVal Ordernum As Integer) As Integer
    '    Dim mySCMD As SqlCommand

    '    mySCMD = New SqlCommand()
    '    mySCMD.CommandText = "SP_WHBIN_BIN_Vacante_Orden"
    '    mySCMD.Parameters.AddWithValue("@ORDERNUM", Ordernum)


    '    Return Convert.ToInt32(mySql.Execute_StoreProcedure_Return(mySCMD))

    'End Function
    '' Obtiene el id del BIN al cual se debe almacenar esta orden proporcionando la clave de la escuela
    'Public Function DameBinID_Vacante(ByVal sClaveEscuela As String) As Integer
    '    Dim mySCMD As SqlCommand
    '    mySCMD = New SqlCommand()
    '    mySCMD.CommandText = "SP_WHBIN_BIN_Vacante_Escuela"
    '    mySCMD.Parameters.AddWithValue("@CCT", sClaveEscuela)

    '    Return mySql.Execute_StoreProcedure_Return(mySCMD)

    'End Function

    'Public Function DameCantidadRxEscuela(ByVal sCCT As String, ByVal iCicloID As Integer) As Integer
    '    Dim mySCMD As SqlCommand
    '    mySCMD = New SqlCommand()
    '    mySCMD.CommandText = "SP_WHBIN_CantidadRecetas"
    '    mySCMD.Parameters.AddWithValue("@ClaveEscuela", sCCT)
    '    mySCMD.Parameters.AddWithValue("@CicloID", iCicloID)

    '    Return mySql.Execute_StoreProcedure_Return(mySCMD)
    'End Function

    '' Obtiene el RxNum de VerBien a partir del número de órden Vantage
    '' Regresa -1 si hubo un error o no se encontró el cicloID
    'Public Function DameCicloID(ByVal sOrdenVntg As String) As Integer
    '    Dim outid As Integer = -1
    '    Dim outobj As Object
    '    Try

    '        Dim mySCMD As String

    '        mySCMD = "Select TOP 1 CicloID from VBPAM_Paciente PA WITH(NoLock) JOIN TBLWEBORDERS TW ON PA.Rx=TW.ponumber WHERE TW.ordernum=" + sOrdenVntg

    '        outobj = mySql.ExecuteScalar(mySCMD)
    '        If Not (outobj = Nothing) Then
    '            outid = Convert.ToInt32(outobj)
    '        End If

    '    Catch ex As Exception
    '        outid = -1
    '    End Try
    '    Return outid
    'End Function


    '' Obtiene el id de la escuela a partir de la clave CCT
    '' Regresa -1 si hubo un error o no se encontró el id
    'Public Function DameEscuelaID(ByVal CCT As String) As Integer
    '    Dim outid As Integer = -1
    '    Dim outobj As Object
    '    Try

    '        Dim mySCMD As String

    '        mySCMD = "Select TOP 1 id from VBPAM_Escuelas WITH(NoLock) WHERE clave='" + CCT + "'"

    '        outobj = mySql.ExecuteScalar(mySCMD)
    '        If Not (outobj = Nothing) Then
    '            outid = Convert.ToInt32(outobj)
    '        End If

    '    Catch ex As Exception
    '        outid = -1
    '    End Try
    '    Return outid
    'End Function

    'Public Function DameidTipoBin(ByVal Temporal As Boolean, ByVal Compartido As Boolean) As Integer
    '    Dim outid As Integer = -1
    '    Dim outobj As Object
    '    Try

    '        Dim mySCMD As String

    '        mySCMD = "Select TOP 1 id from VBPAM_WHBIN_Tipo WITH(NoLock) WHERE "

    '        If Temporal Then
    '            mySCMD += "Tipo='Temporal'"
    '        Else
    '            mySCMD += "Tipo='Permanente'"
    '        End If

    '        mySCMD += " AND "

    '        If Compartido Then
    '            mySCMD += " EsCompartida=1"
    '        Else
    '            mySCMD += " EsCompartida=0"
    '        End If
    '        outobj = mySql.ExecuteScalar(mySCMD)
    '        If Not (outobj = Nothing) Then
    '            outid = Convert.ToInt32(outobj)
    '        End If

    '    Catch ex As Exception
    '        outid = -1
    '    End Try
    '    Return outid
    'End Function


    '' Obtiene el bin para las fundaciones. Este bin es único y siempre se usa. 
    'Public Function DameBinFundaciones() As Integer
    '    Dim outid As Integer = -1
    '    Dim outobj As Object
    '    Try

    '        Dim mySCMD As String

    '        mySCMD = "SELECT VBPAM_WHBIN.id FROM  VBPAM_WHBIN INNER JOIN VBPAM_WHBIN_Tipo ON VBPAM_WHBIN.idTipo = VBPAM_WHBIN_Tipo.id WHERE Tipo='Fundaciones' AND Vacante=1"

    '        outobj = mySql.ExecuteScalar(mySCMD)
    '        If Not (outobj = Nothing) Then
    '            outid = Convert.ToInt32(outobj)
    '        End If

    '    Catch ex As Exception
    '        outid = -1
    '    End Try
    '    Return outid
    'End Function

    '' Total de RXS que se han apartado para el bin indicado. Un bin compartido contiene varias escuelas.
    'Public Function DameTotalRxsBinTemporal(ByVal idBIN As Integer) As Integer
    '    Dim outid As Integer = -1
    '    Try

    '        Dim mySCMD As SqlCommand
    '        mySCMD = New SqlCommand()
    '        mySCMD.CommandText = "SP_WHBIN_TotalRxsApartadasTemp"
    '        mySCMD.Parameters.AddWithValue("@BINID", idBIN)


    '        Return mySql.Execute_StoreProcedure_Return(mySCMD)

    '    Catch ex As Exception
    '        Return -1
    '    End Try
    'End Function


    '' Total de RXS que se han apartado para la escuela en los bins permanentes
    '' este procedimiento difiere del anterior en que aquí se buscan las parcialidades que se han apartado de 1 sóla escuela en varios bins
    'Public Function DameTotalRxsBinPermanente(ByVal CCT As String) As Integer
    '    Dim outid As Integer = -1
    '    Try

    '        Dim mySCMD As SqlCommand
    '        mySCMD = New SqlCommand()
    '        mySCMD.CommandText = "SP_WHBIN_TotalRxsApartadasPerm"
    '        mySCMD.Parameters.AddWithValue("@CCT", CCT)


    '        Return mySql.Execute_StoreProcedure_Return(mySCMD)

    '    Catch ex As Exception
    '        Return -1
    '    End Try
    'End Function

    'Public Function DameBinVacante(ByVal iNumRxs As Integer, ByVal idTipoBin As Integer) As Integer
    '    Dim outid As Integer = -1
    '    Try

    '        Dim mySCMD As SqlCommand
    '        mySCMD = New SqlCommand()
    '        mySCMD.CommandText = "SP_WHBIN_BinVacante"
    '        mySCMD.Parameters.AddWithValue("@REQQTY", iNumRxs)
    '        mySCMD.Parameters.AddWithValue("@TIPO", idTipoBin)

    '        Return mySql.Execute_StoreProcedure_Return(mySCMD)

    '    Catch ex As Exception
    '        Return -1
    '    End Try
    'End Function


    ' Obtiene regresa un DS con información del Bin indicado
    ' [id] [WH] [BIN] [Capacidad] [Ocupacion] [Vacante] [Color] [idTipo]

    Public Function DameBinData(ByVal idBIN As Integer) As DataSet
        Dim outobj As DataSet
        Try

            Dim mySCMD As String

            mySCMD = "Select *,(Capacidad - Apartado) as Disponible FROM VBPAM_WHBIN WHERE id=" + idBIN.ToString

            outobj = mySql.SQLDS(mySCMD, "BIN")

        Catch ex As Exception
            outobj = Nothing
        End Try
        Return outobj
    End Function

    ' Obtiene regresa un DS con información del tipo Bin indicado
    ' [id] [idTipo] [EsCompartida]

    Public Function DameBinTipoData(ByVal idTipo As Integer) As DataSet
        Dim outobj As DataSet
        Try

            Dim mySCMD As String

            mySCMD = "Select * FROM VBPAM_WHBIN_Tipo WHERE id=" + idTipo.ToString

            outobj = mySql.SQLDS(mySCMD, "TIPO")

        Catch ex As Exception
            outobj = Nothing
        End Try
        Return outobj
    End Function

    ' Asigna la escuela al bin temporal indicado
    ' Regresa 1 si tuvo éxito
    ' Regresa -1 si hubo un error

    ''#MOD#JGARIBALDI#25 de septiembre de 2014# Se comentó toda esta función ya que la obtención del bin se hará de otra manera
    'Public Function AsignaBinTempEsc(ByVal idBin As Integer, ByVal CCT As String, ByVal idCiclo As Integer, ByVal esCompartida As Boolean, ByVal numRxs As Integer) As Integer
    '    Dim mySCMD As String
    '    Dim outid As Integer = 1
    '    Dim Apartado, Capacidad As Integer
    '    Dim dsBinData As DataSet
    '    Dim ApartadoEsteBin As Integer
    '    Dim idEscuela As Integer
    '    Try

    '        ' Se debe insertar un registro en la tabla de bins temporales
    '        idEscuela = DameEscuelaID(CCT)

    '        If idEscuela < 0 Then Return -1 ' No podemos continuar sin el id de la escuela

    '        mySCMD = "INSERT INTO VBPAM_WHBIN_Escuelas (idBIN,idEscuela,idCiclo,Cantidad) VALUES (" + idBin.ToString + "," + idEscuela.ToString + "," + idCiclo.ToString + ",0)"
    '        mySql.ExecuteNonQuery(mySCMD)

    '        ' Calcular si ya se marca como ocupada o no, dependiendo si es compartida o no, y dependiendo si tiene más capacidad o no

    '        If Not (esCompartida) Then
    '            ' No es compartida, se quita la bandera de Vacante y se coloca la cantidad de lugares apartados

    '            If numRxs > Capacidad Then  ' Si la orden no cabe en un solo bin se asigna la cantidad a usar para este bin
    '                ApartadoEsteBin = Capacidad
    '            Else
    '                ApartadoEsteBin = numRxs
    '            End If

    '            mySCMD = "UPDATE VBPAM_WHBIN SET Vacante=0,Apartado=" + ApartadoEsteBin.ToString() + " WHERE id=" + idBin.ToString()
    '            mySql.ExecuteNonQuery(mySCMD)

    '        Else
    '            ' Es compartida, se necesita calcular la capacidad ocupada para saber si se marca como vacante o no
    '            ' Si al bin le queda menos del 10% de su capacidad disponible se marca como no vacante
    '            dsBinData = DameBinData(idBin)
    '            If Not (dsBinData Is Nothing) Then
    '                If dsBinData.Tables.Count > 0 Then
    '                    If dsBinData.Tables(0).Rows.Count > 0 Then
    '                        Capacidad = dsBinData.Tables(0).Rows(0).Item("Capacidad")
    '                        Apartado = DameTotalRxsBinTemporal(idBin)
    '                        If Capacidad > 0 Then ' En caso de que hubiese existido un error
    '                            If (Capacidad - Apartado < (Capacidad \ 10)) Then   ' \ es la división con resultado entero, conocida también como "truncation"
    '                                ' Marca el bin como no vacante
    '                                mySCMD = "UPDATE VBPAM_WHBIN SET Vacante=0 WHERE id=" + idBin.ToString()
    '                                mySql.ExecuteNonQuery(mySCMD)

    '                            End If
    '                            ' Actualiza la cantidad de lugares apartados en ese bin
    '                            mySCMD = "UPDATE VBPAM_WHBIN SET Apartado=Apartado + " + numRxs.ToString + " WHERE id=" + idBin.ToString

    '                            If Not (mySql.ExecuteNonQuery(mySCMD)) Then
    '                                MsgBox("Error al actualizar la tabla de inventarios. VBPAM_WHBIN." + vbCrLf + "No se pudo asignar la escuela a un bin.", MsgBoxStyle.Critical, "Error AsignaBinTempEsc")
    '                            End If

    '                        End If

    '                    End If
    '                End If
    '            End If
    '        End If
    '    Catch ex As Exception
    '        outid = -1
    '    End Try
    '    Return outid
    'End Function

    ' Asigna la escuela al bin permanente indicado
    ' Regresa 1 si tuvo éxito
    ' Regresa -1 si hubo un error
    'Public Function AsignaBinPermEsc(ByVal idBin As Integer, ByVal CCT As String, ByVal idCiclo As Integer, ByVal esCompartida As Boolean, ByVal numRxs As Integer, ByVal Capacidad As Integer) As Integer
    '    Dim mySCMD As String
    '    Dim outid As Integer = 1
    '    Dim Apartado As Integer
    '    Dim ApartadoEsteBin As Integer

    '    Try

    '        ' Calcular si ya se marca como ocupada o no, dependiendo si es compartida o no, y dependiendo si tiene más capacidad o no

    '        If Not (esCompartida) Then
    '            ' No es compartida, se quita la bandera de Vacante y se coloca la cantidad de lugares apartados

    '            If numRxs > Capacidad Then  ' Si la orden no cabe en un solo bin se asigna la cantidad a usar para este bin
    '                ApartadoEsteBin = Capacidad
    '            Else
    '                ApartadoEsteBin = numRxs
    '            End If

    '            mySCMD = "UPDATE VBPAM_WHBIN SET Vacante=0,idEscuela=" + DameEscuelaID(CCT).ToString() + ",Apartado=" + ApartadoEsteBin.ToString() + ",idCiclo=" + idCiclo.ToString() + " WHERE id=" + idBin.ToString()
    '            mySql.ExecuteNonQuery(mySCMD)

    '        Else
    '            ' Es compartida, se necesita calcular la capacidad ocupada para saber si se marca como vacante o no
    '            ' Si al bin le queda menos del 10% de su capacidad disponible se marca como no vacante

    '            Apartado = DameTotalRxsBinTemporal(idBin)
    '            If Capacidad > 0 Then ' En caso de que hubiese existido un error
    '                If (Capacidad - Apartado < (Capacidad \ 10)) Then   ' \ es la división con resultado entero, conocida también como "truncation"
    '                    ' Marca el bin como no vacante y actualiza la cantidad de lugares apartados
    '                    mySCMD = "UPDATE VBPAM_WHBIN SET Vacante=0 WHERE id=" + idBin.ToString()
    '                    mySql.ExecuteNonQuery(mySCMD)

    '                End If
    '                ' Actualiza la cantidad de lugares apartados en ese bin
    '                ' Como es compartida el idCiclo se pone en 0 porque puede haber escuelas de diferente ciclo en el mismo bin
    '                ' para este caso se usa la tabla VBPAM_WHBIN_Escuelas para obtener la escuela y el ciclo
    '                mySCMD = "UPDATE VBPAM_WHBIN SET idCiclo=0,Apartado=" + Apartado + " WHERE id=" + idBin.ToString()
    '                mySql.ExecuteNonQuery(mySCMD)

    '            End If

    '        End If



    '    Catch ex As Exception
    '        outid = -1
    '    End Try
    '    Return outid
    'End Function


    'Function DameUltimoBinTemp() As String
    '    Dim outid As String = Nothing

    '    Dim outobj As Object
    '    Try

    '        Dim mySCMD As String

    '        mySCMD = "select BIN FROM [VerBien].[dbo].[VBPAM_WHBIN] WHERE id=(select MAX(id) FROM [VerBien].[dbo].[VBPAM_WHBIN] WHERE idTipo=" + Me.DameidTipoBin(True, True).ToString() + ")"

    '        outobj = mySql.ExecuteScalar(mySCMD)
    '        If Not (outobj Is Nothing) Then
    '            outid = outobj.ToString()
    '        End If

    '    Catch ex As Exception
    '        outid = Nothing
    '    End Try

    '    Return outid

    'End Function



    'Function DameUltimoBinWH() As String
    '    Dim outid As String = Nothing

    '    Dim outobj As Object
    '    Try

    '        Dim mySCMD As String

    '        mySCMD = "select WH FROM [VerBien].[dbo].[VBPAM_WHBIN] WHERE id=(select MAX(id) FROM [VerBien].[dbo].[VBPAM_WHBIN] WHERE idTipo=" + Me.DameidTipoBin(True, True) + ")"

    '        outobj = mySql.ExecuteScalar(mySCMD)
    '        If Not (outobj Is Nothing) Then
    '            outid = outobj.ToString()
    '        End If

    '    Catch ex As Exception
    '        outid = Nothing
    '    End Try

    '    Return outid

    'End Function

    '' Regresa -1 si hubo un error
    '' En éxito regresa el número de registros afectados, normalmente debería ser 1
    'Function CreaBinTemp() As Integer

    '    Dim outid As Integer = -1

    '    Dim LastBin, LastWH, NewBin As String
    '    Dim NewBinNum As Integer

    '    Try

    '        Dim mySCMD As String

    '        LastBin = Me.DameUltimoBinTemp()
    '        LastWH = Me.DameUltimoBinWH()

    '        If LastBin Is Nothing Then Return -1 ' Nada que hacer - fué un error


    '        NewBinNum = Convert.ToInt32(LastBin.Remove(0))
    '        NewBin = LastBin.Substring(0, 1) + NewBinNum.ToString()
    '        mySCMD = "INSERT INTO [VBPAM_WHBIN] (WH,BIN,Capacidad,Color,idTipo) VALUES ('" + LastWH + "'," + NewBin + ",38,'CAJA'," + Me.DameidTipoBin(True, True) + ")"

    '        outid = mySql.ExecuteNonQuery(mySCMD)

    '    Catch ex As Exception
    '        outid = -1
    '    End Try

    '    Return outid

    'End Function

    ''#Fin MOD#JGARIBALDI#25 de septiembre de 2014# Se comentó toda esta función ya que la obtención del bin se hará de otra manera

    ''' <summary>
    ''' Guarda la orden, el bin y la escuela en la tabla WHBIN_RX para una consulta rápida
    ''' </summary>
    ''' <returns>Regresa -1 si hubo un error. En éxito regresa el número de registros afectados</returns>
    ''' <remarks>Normalmente debería ser 1</remarks>

    Function GuardaOrdenBinEsc(ByVal idBin As Integer, ByVal idEscuela As Integer, ByVal RX As String, ByVal Ordernum As String, ByVal idCiclo As String) As Integer
        Dim outid As Integer = -1

        Try

            Dim mySCMD As String

            mySCMD = "INSERT INTO [VBPAM_WHBIN_RX] (idBin,idEscuela,RX,Ordernum,idCiclo) VALUES (" _
                + idBin.ToString() + "," + idEscuela.ToString() + ",'" + RX + "'," + Ordernum + "," + idCiclo + ")"

            outid = mySql.ExecuteNonQuery(mySCMD)

        Catch ex As Exception
            outid = -1
        End Try

        Return outid
    End Function
End Class

