Public Class Frames
    Dim FrameID As Integer
    Dim FrameName As String
    Dim PaqID As String
    Dim OriginalFrameID As Integer
    Dim FrameErrorID As Integer
    Dim FrameErrorDescription As String
    Dim Status As ThisFrameStatus
    Dim FrameReadStatus As ThisFrameReadStatus
    Dim FramePartnum As String


    Property NumArmazon() As Integer
        Get
            Return FrameID
        End Get
        Set(ByVal value As Integer)
            FrameID = value
        End Set
    End Property
    Property ArmazonDescripcion() As String
        Get
            Return FrameName
        End Get
        Set(ByVal value As String)
            FrameName = value
        End Set
    End Property
    Property NumPaquete() As String
        Get
            Return PaqID
        End Get
        Set(ByVal value As String)
            PaqID = value
        End Set
    End Property
    Property ArmazonOriginal() As Integer
        Get
            Return OriginalFrameID
        End Get
        Set(ByVal value As Integer)
            OriginalFrameID = value
        End Set
    End Property
    Property ErrorID() As Integer
        Get
            Return FrameErrorID
        End Get
        Set(ByVal value As Integer)
            FrameErrorID = value
        End Set
    End Property
    Property ErrorDescription() As String
        Get
            Return FrameErrorDescription
        End Get
        Set(ByVal value As String)
            FrameErrorDescription = value
        End Set
    End Property
    Property FrameStatus() As ThisFrameStatus
        Get
            Return Status
        End Get
        Set(ByVal value As ThisFrameStatus)
            Status = value
        End Set
    End Property
    Property ReadStatus() As ThisFrameReadStatus
        Get
            Return FrameReadStatus
        End Get
        Set(ByVal value As ThisFrameReadStatus)
            FrameReadStatus = value
        End Set
    End Property
    ReadOnly Property PartNum() As String
        Get
            Return FramePartnum
        End Get
    End Property

    Enum ThisFrameStatus
        [New]
        Changed
        NoChange
        NoFrame
    End Enum
    Enum ThisFrameReadStatus
        Ok
        NotRead
        Failed
    End Enum


    Public Sub New(ByVal NumArmazon As Integer, ByVal Status As ThisFrameStatus)
        FramePartnum = NumArmazon
        If FramePartnum.Length > 5 Then
            FramePartnum = FramePartnum.Substring(0, 5)
        End If
        FrameID = NumArmazon
        FrameName = ""
        OriginalFrameID = NumArmazon
        FrameErrorID = 0
        FrameErrorDescription = ""
        Me.Status = Status
        FrameReadStatus = ThisFrameReadStatus.NotRead
    End Sub

    Public Function CheckInventoryFrameFamily() As Boolean
        Dim ReturnValue As Boolean = False
        Dim Failed As Boolean = False
        Dim FailedMessage As String = ""

        Dim sqlstr As String = ""
        Dim ds As New DataSet
        Dim OnHanQty As Integer = 0
        Dim Description As String = ""

        Dim t As New SqlDB()
        t.SQLConn = New SqlClient.SqlConnection(Laboratorios.ConnStr)
        Try
            t.OpenConn()
            sqlstr = "select distinct framefamily from vwarmazones with(nolock) where framefamily in (" & My.Settings.InventoryFrameFamily & ") and partnum in (" & FramePartnum & ")"
            ds = t.SQLDS(sqlstr, "t1")
            If ds.Tables("t1").Rows.Count > 0 Then
                ReturnValue = True
            End If

        Catch ex As Exception
            Failed = True
            FailedMessage = "Error al obtener datos del armazón" & vbCrLf & ex.Message
        Finally
            t.CloseConn()
            If Failed Then
                MsgBox(FailedMessage, MsgBoxStyle.Exclamation)
            End If
        End Try
        Return ReturnValue
    End Function
    Public Function CheckFrameStatus() As Boolean
        Dim Failed As Boolean = False
        Dim FailedMessage As String = ""

        Dim sqlstr As String = ""
        Dim ds As New DataSet
        Dim OnHanQty As Integer = 0
        Dim Description As String = ""


        Dim t As New SqlDB()
        t.SQLConn = New SqlClient.SqlConnection(Laboratorios.ConnStr)
        Try
            t.OpenConn()
            sqlstr = "select * from vwarmazones with(nolock) where partnum = '" & FramePartnum & "'"
            ds = t.SQLDS(sqlstr, "t1")
            If ds.Tables("t1").Rows.Count > 0 Then
                OnHanQty = ds.Tables("t1").Rows(0).Item("onhandqty")
                Try

                    FrameName = ds.Tables("t1").Rows(0).Item("partdescription")
                Catch ex As Exception
                    FrameName = "No se encontró descripción"
                    Throw New Exception("Descripción en Null")
                End Try
            Else
                Throw New Exception("Armazón [" & FramePartnum & "]inexistente en la base de datos")
            End If

        Catch ex As Exception
            Failed = True
            FailedMessage = "Error al obtener datos del armazón" & vbCrLf & ex.Message
        Finally
            t.CloseConn()
            If Failed Then
                MsgBox(FailedMessage, MsgBoxStyle.Exclamation)
            End If
        End Try
        Return Not Failed
    End Function

End Class
