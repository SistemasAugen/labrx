Imports System.Net
Imports System.Net.Sockets
Imports System.Runtime.InteropServices

'***********************************************************
'Dim Ping As New PingObj.HacerPing
'   If Ping.PingServer("192.168.35.20") Then
'       MsgBox("ok")
'   Else
'       MsgBox("host inalcanzable")
'   End If
'***********************************************************
Namespace PingObj

    Structure Angel_Ping
#Region "VARIABLES"
        Dim Data() As Byte
        Dim Type_Message As Byte
        Dim SubCode_type As Byte
        Dim Complement_CheckSum As UInt16
        Dim Identifier As UInt16
        Dim SequenceNumber As UInt16
#End Region
#Region "Metodos"
        Public Sub Initialize(ByVal type As Byte, ByVal subCode As Byte, ByVal payload() As Byte)
            Dim Buffer_IcmpPacket() As Byte
            Dim CksumBuffer() As UInt16
            Dim IcmpHeaderBufferIndex As Int32 = 0
            Dim Index As Integer
            Me.Type_Message = type
            Me.SubCode_type = subCode
            Complement_CheckSum = UInt16.Parse("0")
            Identifier = UInt16.Parse("45")
            SequenceNumber = UInt16.Parse("0")
            Data = payload
            Buffer_IcmpPacket = Serialize()
            ReDim CksumBuffer((Buffer_IcmpPacket.Length() \ 2) - 1)
            For Index = 0 To (CksumBuffer.Length() - 1)
                CksumBuffer(Index) = BitConverter.ToUInt16(Buffer_IcmpPacket, IcmpHeaderBufferIndex)
                IcmpHeaderBufferIndex += 2
            Next Index
            Complement_CheckSum = MCheckSum.Calculate(CksumBuffer, CksumBuffer.Length())
        End Sub
        Public Function Size() As Integer
            Return (8 + Data.Length())
        End Function
        Public Function Serialize() As Byte()
            Dim Buffer() As Byte
            Dim B_Seq() As Byte = BitConverter.GetBytes(SequenceNumber)
            Dim B_Cksum() As Byte = BitConverter.GetBytes(Complement_CheckSum)
            Dim B_Id() As Byte = BitConverter.GetBytes(Identifier)
            Dim Index As Int32 = 0
            ReDim Buffer(Size() - 1)
            Buffer(0) = Type_Message
            Buffer(1) = SubCode_type
            Index += 2
            Array.Copy(B_Cksum, 0, Buffer, Index, 2) : Index += 2
            Array.Copy(B_Id, 0, Buffer, Index, 2) : Index += 2
            Array.Copy(B_Seq, 0, Buffer, Index, 2) : Index += 2
            If (Data.Length() > 0) Then Array.Copy(Data, 0, Buffer, Index, Data.Length())
            Return Buffer
        End Function
#End Region
    End Structure
    Public Class CPing
#Region "Contactes"
        Private Const DATA_SIZE As Integer = 32
        Private Const DEFAULT_TIMEOUT As Integer = 1500
        Private Const ICMP_ECHO As Integer = 8
        Private Const SOCKET_ERROR As Integer = -1
        Private Const PING_ERROR As Integer = -1
        Private Const RECV_SIZE As Integer = 128
#End Region
#Region "VARIABLES"
        Private _Open As Boolean = False
        Private _Initialized As Boolean
        Private _RecvBuffer() As Byte
        Private _Packet As Angel_Ping
        Private _HostName As String
        Private _Server As EndPoint
        Private _Local As EndPoint
        Private _Socket As Socket
#End Region
#Region "CONSTRUCTORS & FINALIZER"
        Public Sub New(ByVal hostName As String)
            Me.HostName() = hostName
            ReDim _RecvBuffer(RECV_SIZE - 1)
        End Sub
        Public Sub New()
            Me.HostName() = Dns.GetHostName()
            ReDim _RecvBuffer(RECV_SIZE - 1)
        End Sub
        Private Overloads Sub finalize()
            Me.Close()
            Erase _RecvBuffer
        End Sub
#End Region
#Region "Metodos"
        Public Property HostName() As String
            Get
                Return _HostName
            End Get
            Set(ByVal Value As String)
                _HostName = Value
                If (_Open) Then
                    Me.Close()
                    Me.Open()
                End If
            End Set
        End Property
        Public ReadOnly Property IsOpen() As Boolean
            Get
                Return _Open
            End Get
        End Property
        Public Function Open() As Boolean
            Dim Payload() As Byte
            If (Not _Open) Then
                Try
                    ReDim Payload(DATA_SIZE)
                    _Packet.Initialize(ICMP_ECHO, 0, Payload)
                    _Socket = New Socket(AddressFamily.InterNetwork, SocketType.Raw, ProtocolType.Icmp)
                    _Server = New IPEndPoint(Dns.GetHostEntry(_HostName).AddressList(0), 0)
                    _Local = New IPEndPoint(Dns.GetHostEntry(Dns.GetHostName()).AddressList(0), 0)
                    _Open = True
                Catch
                    Return False
                End Try
            End If
            Return True
        End Function
        Public Function Close() As Boolean
            If (_Open) Then
                _Socket.Close()
                _Socket = Nothing
                _Server = Nothing
                _Local = Nothing
                _Open = False
            End If
            Return True
        End Function
        Public Overloads Function Ping() As Integer
            Return Ping(DEFAULT_TIMEOUT)
        End Function
        Public Overloads Function Ping(ByVal timeOutMilliSeconds As Integer) As Integer
            Dim TimeOut As Integer = Environment.TickCount
            Try
                If (SOCKET_ERROR = _Socket.SendTo(_Packet.Serialize(), _Packet.Size(), 0, _Server)) Then
                    Return PING_ERROR
                End If
            Catch
            End Try
            Do
                If (_Socket.Poll(-1, SelectMode.SelectRead)) Then
                    _Socket.ReceiveFrom(_RecvBuffer, RECV_SIZE, 0, _Local)
                    Return (Environment.TickCount() - TimeOut)
                ElseIf ((Environment.TickCount() - TimeOut) >= timeOutMilliSeconds) Then
                    Return PING_ERROR
                End If
            Loop While (True)
        End Function
#End Region
    End Class
    Public Class HacerPing
        Public Function PingServer(ByVal ip As String) As Boolean
            Dim Packet As New CPing
            Dim retValue
            Dim Resultado As Boolean
            Try
                Packet.HostName = ip
                If Packet.Open Then
                    retValue = Packet.Ping
                    If retValue <> -1 Then
                        Resultado = True
                    Else
                        Resultado = False
                    End If
                    Packet.Close()
                End If
                Return Resultado
            Catch ex As Exception
                Throw New Exception("error en la clase ping", ex)
            End Try
        End Function
    End Class
    Module MCheckSum
#Region "Metodos"
        <StructLayout(LayoutKind.Explicit)> Structure UNION_INT16
            <FieldOffset(0)> Dim lsb As Byte
            <FieldOffset(1)> Dim msb As Byte
            <FieldOffset(0)> Dim w16 As Short
        End Structure
        <StructLayout(LayoutKind.Explicit)> Structure UNION_INT32
            <FieldOffset(0)> Dim lsw As UNION_INT16
            <FieldOffset(2)> Dim msw As UNION_INT16     '
            <FieldOffset(0)> Dim w32 As Integer
        End Structure
        Public Function Calculate(ByRef buffer() As UInt16, ByVal size As Int32) As UInt16
            Dim Counter As Int32 = 0
            Dim Cksum32 As UNION_INT32
            Do While (size > 0)
                Cksum32.w32 += Convert.ToInt32(buffer(Counter))
                Counter += 1
                size -= 1
            Loop
            Cksum32.w32 = Cksum32.msw.w16 + Cksum32.lsw.w16 + Cksum32.msw.w16
            Return Convert.ToUInt16(Cksum32.lsw.w16 Xor &HFFFF)
        End Function
#End Region
    End Module
End Namespace


