'////////////////////////////////////////////////////////////////////
'DESARROLLADA POR:  Marco Angulo
'Fecha:             Marzo 2006
'********************************************************************
'CLASE UTILIZADA PARA CONECTARSE A CUALQUIER DABSE DE DATOS DE SQL, CON LAS SUIGUIENTES FUNCIONALIDADES:
'1. ABRIR CONEXION CON SQL
'2. CERRAR LA CONEXXION
'3. EJECUTAR UNA TRANSACCION (UPDATE,INSERT,DELETE)
'4. OBTEBER UN DATASET
'5. OBTENER UN DATAREADER
'
'************************************
'EJEMPLO DE COMO UTILIZAR ESTA CLASE
'************************************
'Dim Var as New SqlDB
'
'Var.svr = "MyServername"
'Var.usr = "Myusername"
'Var.dbase = "MyDatabase"
'Var.pass = "MyDBPassword"
'
'Var.OpenConn
'
'Var.Transaction("colocar aqui la trasaccion de tipo UPDATE,INSERT o DELETE")
'
'MyDataReader = Var.Trasaction2("colocar aqui el comando SELECT para regresarlo en un datareader")
'
'MyDataSet = Var.SQLDS("SELECT que se regresara en un DATASET","TABLA X")
'
'Var.CloseConn

'////////////////////////////////////////////////////////////////////
Imports System.Data
Imports System.Data.SqlClient

Module Laboratorios
    Friend ConnStr As String = "user ID=" & My.Settings.DBUser & ";password=" & My.Settings.DBPassword & ";database=" & My.Settings.LocalDBName & ";server=" & My.Settings.LocalServer & ";Connect Timeout=" & My.Settings.OptisurServerTimeout
End Module

Public Class SqlDB
    ''' <summary>
    ''' 
    ''' </summary>
    ''' <remarks></remarks>
    Private server, password, database, user, StringConn As String

    Public Property svr() As String
        Get
            Return server
        End Get
        Set(ByVal value As String)
            server = value
        End Set

    End Property
    Public Property dbase() As String

        Set(ByVal value As String)
            database = value
        End Set
        Get
            Return database
        End Get
    End Property
    Public Property usr() As String
        Get
            Return user
        End Get
        Set(ByVal value As String)
            user = value
        End Set
    End Property
    Public Property pass() As String
        Get
            Return password
        End Get
        Set(ByVal value As String)
            password = value
        End Set
    End Property


    Public SQLConn As SqlConnection 'variable de conexion generica para Access
    Private SQLCmd As SqlCommand 'variable generica para los comandos Access
    Private SQLAdp As SqlDataAdapter
    Private SQLCmdBuilder As SqlCommandBuilder 'variable utilizada para actualizar la base de datos dedes cualquier dataset 

    Public Function ExecuteNonQuery(ByVal SQLstr As String, ByVal Tran As SqlClient.SqlTransaction) As Boolean
        Dim OKResult As Boolean = True
        SQLCmd = New SqlClient.SqlCommand
        SQLCmd.Connection = SQLConn
        SQLCmd.CommandText = SQLstr
        SQLCmd.Transaction = Tran
        SQLCmd.CommandTimeout = My.Settings.DBCommandTimeout
        Try
            SQLCmd.ExecuteNonQuery()
        Catch tranEx As System.Data.SqlClient.SqlException

            OKResult = False
        End Try
        Return OKResult
    End Function

    ' PFL 2 Aug 2013 # Esta función regresa la ejecución de un query scalar, esto es, el primer campo de la primera fila del resultado del query
    Public Function ExecuteScalar(ByVal SQLstr As String) As Object
        Dim OKResult As Object = Nothing

        SQLCmd = New SqlClient.SqlCommand
        SQLCmd.Connection = SQLConn
        SQLCmd.CommandText = SQLstr
        SQLCmd.CommandTimeout = My.Settings.DBCommandTimeout
        Try
            OKResult = SQLCmd.ExecuteScalar()
        Catch tranEx As System.Data.SqlClient.SqlException

        End Try
        Return OKResult
    End Function
    ' PFL Nov 26 2013 # Adivinen que hace esta función
    Public Function Execute_StoreProcedure_Param(ByVal oSCMD As SqlCommand) As Object
        Try
            oSCMD.Connection = SQLConn
            oSCMD.CommandType = CommandType.StoredProcedure
            oSCMD.CommandTimeout = My.Settings.CreateOrderTimeout

            oSCMD.ExecuteNonQuery()

        Catch ex As Exception
            Throw New Exception("Error al ejecutar SP: " + ex.Message, ex.InnerException)
        End Try
        Return oSCMD.Parameters
    End Function

    ' PFL Nov 26 2013 # esta también adivinen.
    Public Function Execute_StoreProcedure_Scalar(ByVal oSCMD As SqlCommand) As Object
        Dim oDT As Object = Nothing

        Try
            oSCMD.Connection = SQLConn
            oSCMD.CommandType = CommandType.StoredProcedure
            oSCMD.CommandTimeout = My.Settings.CreateOrderTimeout

            oDT = oSCMD.ExecuteScalar()

        Catch ex As Exception
            Throw New Exception("Error al ejecutar SP: " + ex.Message, ex.InnerException)
        End Try
        Return oDT
    End Function

    ' PFL Nov 26 2013 # Adivinen que hace esta función 
    Public Function Execute_NonQuerySP(ByVal sSCMD As String) As Integer
        Dim RowsAffected As Integer
        Try
            Dim oSCMD As SqlCommand = New SqlCommand
            oSCMD.Connection = SQLConn
            oSCMD.CommandType = CommandType.StoredProcedure
            oSCMD.CommandTimeout = My.Settings.CreateOrderTimeout
            oSCMD.CommandText = sSCMD

            RowsAffected = oSCMD.ExecuteNonQuery()

        Catch ex As Exception
            Throw New Exception("Error al ejecutar comando Execute_NonQuery : " + ex.Message, ex.InnerException)
        End Try
        Return RowsAffected
    End Function


    ' PFL Nov 28 #
    ' # Encuentren las 5 diferencias entre esta y la anterior
    Public Function ExecuteNonQuery(ByVal SQLstr As String) As Boolean
        Dim OKResult As Boolean = True
        SQLCmd = New SqlClient.SqlCommand
        SQLCmd.Connection = SQLConn
        SQLCmd.CommandText = SQLstr
        SQLCmd.CommandType = CommandType.Text
        Try
            SQLCmd.ExecuteNonQuery()
        Catch tranEx As System.Data.SqlClient.SqlException
            OKResult = False
        End Try
        Return OKResult
    End Function

    Public Function Execute_StoreProcedure_Return(ByVal oSCMD As SqlCommand) As Integer

        ' PFL Nov 13 2013 # Esta función regresa el resultado int return value de un SP
        Dim output As SqlParameter = New SqlParameter("@RETURN_VALUE", SqlDbType.Int)
        output.Direction = ParameterDirection.ReturnValue
        output.Value = -1

        Try


            oSCMD.Parameters.Add(output)
            oSCMD.Connection = SQLConn
            oSCMD.CommandType = CommandType.StoredProcedure
            oSCMD.CommandTimeout = My.Settings.CreateOrderTimeout

            oSCMD.ExecuteNonQuery()

        Catch tranEx As SqlException

            Throw New Exception("Error al ejecutar SP: " + tranEx.Number.ToString() + " " + tranEx.Message)
        End Try
        Return output.Value

    End Function

    Public Sub OpenConn()
        Try

            ' Primero revisamos nuestro string de conexión
            If Not (StringConn Is Nothing) Then
                SQLConn = New SqlConnection
                SQLConn.ConnectionString = StringConn
                ' Después intentamos el string del objeto SQLConn
            ElseIf Not (SQLConn.ConnectionString Is Nothing) Then
                StringConn = SQLConn.ConnectionString
            Else
                ' No tenemos un string de conexión válido
                Throw New Exception("Error de Conexion : No se ha especificado una cadena de conexión." & vbCrLf & "Use SQLDB(<cadena de conexión>) para crear una instancia de la clase SQLDB.")
            End If
            SQLConn.Open()
        Catch sqlconEx As SqlException
            Select Case sqlconEx.Number
                Case 53
                    Throw New Exception("Error de Conexion")
                Case -2
                    Throw New Exception("Error de Conexion - TIMEOUT: " & sqlconEx.Message)
                Case Else
                    Throw New Exception("Error de Conexion - OTROS: " & sqlconEx.Message)
            End Select
        Catch ex As Exception
            Throw New Exception("Error de Conexion - OTROS: " & ex.Message)

            'If SQLConn.State = ConnectionState.Open Then
            '    SQLConn.Close()
            'End If
            'SQLConn = Nothing
        End Try
    End Sub
    Public Sub CloseConn()
        Try
            SQLConn.Close()
            SQLConn.Dispose()
        Catch ex As Exception
        Finally
            SQLConn = Nothing
            SQLCmd = Nothing
        End Try
    End Sub
    Public Function Transaction(ByVal SQLstr As String) As Boolean
        Dim OKResult As Boolean = True
        SQLCmd = New SqlClient.SqlCommand
        SQLCmd.Connection = SQLConn
        SQLCmd.CommandText = SQLstr
        Try
            SQLCmd.ExecuteNonQuery()
        Catch tranEx As System.Data.SqlClient.SqlException
            'MsgBox(ex.Message, MsgBoxStyle.Critical, "Error de Access")
            'If SQLConn.State = ConnectionState.Open Then
            '    SQLConn.Close()
            '    SQLConn = Nothing
            'End If
            'Throw New System.Exception("Error al ejecutar transaccion en SQL" & vbCrLf & tranEx.Message & vbCrLf & SQLstr, tranEx)
            OKResult = False
        End Try
        Return OKResult
    End Function
    Public Function Transaction(ByVal SQLstr As String, ByVal TransactionName As SqlTransaction) As Boolean
        Dim OKResult As Boolean = True
        SQLCmd = New SqlClient.SqlCommand
        SQLCmd.Connection = SQLConn
        SQLCmd.CommandText = SQLstr
        SQLCmd.Transaction = TransactionName
        Try
            SQLCmd.ExecuteNonQuery()
        Catch tranEx As System.Data.SqlClient.SqlException
            'MsgBox(ex.Message, MsgBoxStyle.Critical, "Error de Access")
            'If SQLConn.State = ConnectionState.Open Then
            '    SQLConn.Close()
            '    SQLConn = Nothing
            'End If
            'Throw New System.Exception("Error al ejecutar transaccion en SQL" & vbCrLf & tranEx.Message & vbCrLf & SQLstr, tranEx)
            OKResult = False
        End Try
        Return OKResult
    End Function
    Public Function Transaction(ByVal SQLstr As String, ByVal TransactionName As SqlTransaction, ByRef ErrorMessage As String) As Boolean
        Dim OKResult As Boolean = True
        SQLCmd = New SqlClient.SqlCommand
        SQLCmd.Connection = SQLConn
        SQLCmd.CommandText = SQLstr
        SQLCmd.Transaction = TransactionName
        Try
            SQLCmd.ExecuteNonQuery()
        Catch tranEx As System.Data.SqlClient.SqlException
            'MsgBox(ex.Message, MsgBoxStyle.Critical, "Error de Access")
            'If SQLConn.State = ConnectionState.Open Then
            '    SQLConn.Close()
            '    SQLConn = Nothing
            'End If
            'Throw New System.Exception("Error al ejecutar transaccion en SQL" & vbCrLf & tranEx.Message & vbCrLf & SQLstr, tranEx)
            ErrorMessage = tranEx.Message
            OKResult = False
        End Try
        Return OKResult
    End Function

    Public Function Transaction2(ByVal SQLstr As String) As SqlClient.SqlDataReader
        SQLCmd = New SqlClient.SqlCommand
        SQLCmd.Connection = SQLConn
        SQLCmd.CommandText = SQLstr
        Try

            Transaction2 = SQLCmd.ExecuteReader
            'SQLCmd.Connection.Close()
            'SQLCmd = Nothing
        Catch tran2Ex As System.Data.SqlClient.SqlException
            'MsgBox(ex.Message, MsgBoxStyle.Critical, "Error de Access")
            'If SQLConn.State = ConnectionState.Open Then
            '    SQLConn.Close()
            '    SQLConn = Nothing
            'End If
            Throw New System.Exception("Error al ejecutar transaccion en SQL" & vbCrLf & tran2Ex.Message & vbCrLf & SQLstr, tran2Ex)
        End Try
    End Function
    Public Function SQLDS(ByVal SQLstr As String, ByVal TblRef As String) As DataSet
        Try
            SQLAdp = New SqlClient.SqlDataAdapter(SQLstr, SQLConn)

            SQLCmdBuilder = New SqlClient.SqlCommandBuilder(SQLAdp)
            SQLDS = New DataSet
            SQLAdp.Fill(SQLDS, TblRef)

        Catch dsEx As SqlClient.SqlException
            'If SQLConn.State = ConnectionState.Open Then
            '    SQLConn.Close()
            'End If
            'SQLConn = Nothing
            Throw New System.Exception("Error de conexión SQL DS " & vbCrLf & dsEx.Message & vbCrLf & SQLstr, dsEx)
        End Try

    End Function
    Public Function SQLDS(ByVal SQLstr As String, ByVal TblRef As String, ByVal TransactionName As SqlTransaction) As DataSet
        Try
            SQLAdp = New SqlClient.SqlDataAdapter(SQLstr, SQLConn)
            SQLCmdBuilder = New SqlClient.SqlCommandBuilder(SQLAdp)
            SQLDS = New DataSet
            'SQLCmd.Transaction = TransactionName
            SQLAdp.SelectCommand.Transaction = TransactionName
            SQLAdp.Fill(SQLDS, TblRef)

        Catch dsEx As SqlClient.SqlException
            'If SQLConn.State = ConnectionState.Open Then
            '    SQLConn.Close()
            'End If
            'SQLConn = Nothing
            Throw New System.Exception("Error de conexión SQL DS " & vbCrLf & dsEx.Message & vbCrLf & SQLstr, dsEx)
        End Try

    End Function
    Public Sub uptds(ByRef ds1 As DataSet, ByVal TblRef As String)
        SQLAdp.Update(ds1, TblRef)
    End Sub

    Public Sub New(ByVal Server As String, ByVal User As String, ByVal Password As String, ByVal Database As String)
        svr = Server
        usr = User
        pass = Password
        dbase = Database
        StringConn = "user ID= " & usr & " ;password = " & pass & " ;database=" & dbase & ";server=" & svr & ";Connect Timeout=30"
        ' Se prefiere un time out de conexión de 30 segundos. # PFL # Oct 4 2013
        ' & My.Settings.DBCommandTimeout
    End Sub

    Public Sub New(ByVal SqlStringConnection As String)
        'asigamos el string de conexion de SQL
        StringConn = SqlStringConnection

    End Sub
    Public Sub New()
        ' Necesitamos colocar después un string de conexión, esto lo podemos hacer si a la instancia le asignamos el string de conexión
        ' <instance>.SQLConn = New SqlConnection(A__Connection__String)
    End Sub

End Class
