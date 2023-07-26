Imports Microsoft.Win32
Imports System.Drawing
Imports System.Drawing.Drawing2D
Imports System.Drawing.Imaging
Imports System.Drawing.Text
Imports System.Data.SqlClient
Public Class ProcessEfficiency
    Dim GenProdSTD, BisProdSTD, ARProdSTD As Double

    Private Sub DrawSLLevel(ByVal Max As Integer, ByVal Value As Integer)
        Try
            Dim percent As Single = RedBarSL.Width / Max
            GreenBarSL.Width = percent * Value
        Catch ex As Exception
            GreenBarSL.Width = RedBarSL.Width
        End Try

    End Sub
    Private Sub DrawARLevel(ByVal Max As Integer, ByVal Value As Integer)
        Try
            Dim percent As Single = RedBarAR.Width / Max
            GreenBarAR.Width = percent * Value
        Catch ex As Exception
            GreenBarAR.Width = RedBarAR.Width
        End Try
    End Sub
    Private Sub DrawBMLevel(ByVal Max As Integer, ByVal Value As Integer)
        Try
            Dim percent As Single = RedBarBM.Width / Max
            GreenBarBM.Width = percent * Value
        Catch ex As Exception
            GreenBarBM.Width = RedBarBM.Width
        End Try
    End Sub
    Private Sub DrawPCLevel(ByVal Max As Integer, ByVal Value As Integer)
        Try
            Dim percent As Single = RedBarPC.Width / Max
            GreenBarPC.Width = percent * Value
        Catch ex As Exception
            GreenBarPC.Width = RedBarPC.Width
        End Try
    End Sub
    Private Sub DrawLevel(ByVal Max As Integer, ByVal Value As Integer)
        Try
            Dim percent As Single = GreenBar.Height / Max
            RedBar.Height = percent * Value
        Catch ex As Exception
            RedBar.Height = GreenBar.Height
        End Try
    End Sub
    Public Function LabID() As Integer
        Dim t As New SqlDB(My.Settings.LocalServer, My.Settings.DBUser, My.Settings.DBPassword, My.Settings.LocalDBName)
        Dim nds As New DataSet
        Try
            t.OpenConn()
            nds = t.SQLDS("Select cl_lab from tbllaboratorios where plant = '" & Read_Registry("Plant") & "'", "t1")
            If nds.Tables("t1").Rows.Count > 0 Then
                LabID = nds.Tables("t1").Rows(0).Item(0)
                Return LabID
            Else
                Throw New Exception("Error en la clave de la planta asignada a este laboratorio.")
            End If
        Catch ex As Exception
            Throw New Exception(ex.Message)
        Finally
            t.CloseConn()
        End Try
    End Function

    Public Sub ActualizaProcesos_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ActualizaProcesos.Click, BotonActualizar.Click
        Dim ds As New DataSet

        'Dim t As New SqlDB(My.Settings.VantageServer, My.Settings.DBUser, My.Settings.DBPassword, My.Settings.MfgSysDBName)
        Dim t As New SqlDB(My.Settings.LocalServer, My.Settings.DBUser, My.Settings.DBPassword, My.Settings.LocalDBName)

        Try
            Me.Cursor = Cursors.WaitCursor
            t.OpenConn()
            Dim labid As Integer = Me.LabID

            Plant.Text = Form1.Read_Registry("Plant")
            Dim Locales As Integer = 0

            ds = t.SQLDS("exec SP_OpenOrdersDetailed @Plant = " & labid, "t1")

            With ds.Tables("t1").Rows(0)
                TextOpenRx.Text = .Item("Rx")
                TextOnTimeRx.Text = .Item("RxOnTime")
                TextOutOfTimeRx.Text = .Item("RxNotOnTime")
                If Form1.Read_Registry("QAEnabled") Then
                    '--------------------------------------------------------
                    ' GENERADO 
                    '--------------------------------------------------------
                    LabelSLNotOnTime.Text = .Item("SLNotOnTime")
                    LabelSLOnTime.Text = .Item("SLOnTime")
                    LabelSL.Text = .Item("SL")
                    '--------------------------------------------------------
                    ' ANTIREFLEJANTE 
                    '--------------------------------------------------------
                    LabelARNotOnTime.Text = .Item("ARNotOnTime")
                    LabelAROnTime.Text = .Item("AROnTime")
                    LabelAR.Text = .Item("AR")
                    '--------------------------------------------------------
                    ' BISELADO Y MONTADO 
                    '--------------------------------------------------------
                    LabelBMNotOnTime.Text = .Item("FLNotOnTime")
                    LabelBMOnTime.Text = .Item("FLOnTime")
                    LabelBM.Text = .Item("FL")
                    '--------------------------------------------------------
                    ' PENDIENTES DE CERRAR 
                    '--------------------------------------------------------
                    LabelPCNotOnTime.Text = .Item("PCNotOnTime")
                    LabelPCOnTime.Text = .Item("PCOnTime")
                    LabelPC.Text = .Item("PC")

                    PanelQA.Enabled = True
                Else
                    LabelSLNotOnTime.Text = 0
                    LabelSLOnTime.Text = 0
                    LabelSL.Text = 0

                    LabelARNotOnTime.Text = 0
                    LabelAROnTime.Text = 0
                    LabelAR.Text = 0

                    LabelBMNotOnTime.Text = 0
                    LabelBMOnTime.Text = 0
                    LabelBM.Text = 0

                    LabelPCNotOnTime.Text = 0
                    LabelPCOnTime.Text = 0
                    LabelPC.Text = 0

                    PanelQA.Enabled = False
                End If
                DrawSLLevel(LabelSL.Text, CInt(LabelSL.Text) - CInt(LabelSLNotOnTime.Text))
                DrawARLevel(LabelAR.Text, CInt(LabelAR.Text) - CInt(LabelARNotOnTime.Text))
                DrawBMLevel(LabelBM.Text, CInt(LabelBM.Text) - CInt(LabelBMNotOnTime.Text))
                DrawPCLevel(LabelPC.Text, CInt(LabelPC.Text) - CInt(LabelPCNotOnTime.Text))

            End With
            DrawLevel(TextOpenRx.Text, TextOutOfTimeRx.Text)
            ' ''End If

        Catch ex As Exception
            If ex.Message.Contains("Error de Conexion") Then
                Form1.ChangeWorkingStatus(Form1.WorkingStatusType.OffLine)
            Else
                'MsgBox(ex.Message, MsgBoxStyle.Critical)
            End If

        Finally
            t.CloseConn()
            Form1.Update_Virtuales_WebOrders(Me, e)
            Me.Cursor = Cursors.Default
        End Try

    End Sub
    Function Read_Registry(ByVal var As String) As String
        Dim key As RegistryKey
        Dim Result As String = ""
        key = Registry.LocalMachine.OpenSubKey("SOFTWARE").OpenSubKey("AUGEN")
        Select Case var
            Case "Plant"
                'Result = CStr(key.GetValue("Plant"))
                Result = My.Settings.Plant
            Case "TracerDataServer"
                'Result = CStr(key.GetValue("TracerDataServer"))
                Result = My.Settings.LocalServer
            Case "ServerAdd"
                'Result = CStr(key.GetValue("MainServer"))
                Result = My.Settings.VantageServer
        End Select
        Return Result
    End Function
    Public Sub GetProdStd()


    End Sub
    Public Sub New()
        ' This call is required by the Windows Form Designer.
        InitializeComponent()

        'cargamos el tiempo estandar de cada operacion en producir un lente 
    End Sub

    Private Sub RedBar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RedBar.Click, TextOutOfTimeRx.Click
        Dim rxl As New RxList
        rxl.RxType = RxList.RxTypes.OutOfTimeRx
        rxl.Text = "Lista de Recetas Abiertas Fuera de Tiempo"
        Me.Cursor = Cursors.WaitCursor
        rxl.Show()
        Me.Cursor = Cursors.Default
    End Sub

    Private Sub GreenBar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles GreenBar.Click, TextOnTimeRx.Click
        Dim rxl As New RxList
        rxl.RxType = RxList.RxTypes.OnTimeRx
        rxl.Text = "Lista de Recetas Abiertas Dentro de Tiempo"
        Me.Cursor = Cursors.WaitCursor
        rxl.Show()
        Me.Cursor = Cursors.Default
    End Sub

    Private Sub TextOpenRx_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TextOpenRx.Click
        Dim rxl As New RxList
        rxl.RxType = RxList.RxTypes.AllOpenRx
        rxl.Text = "Lista de Recetas Abiertas"
        Me.Cursor = Cursors.WaitCursor
        rxl.Show()
        Me.Cursor = Cursors.Default
    End Sub

    Private Sub GreenBarSL_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles GreenBarSL.Click, LabelSLOnTime.Click
        Dim rxl As New RxList
        rxl.RxType = RxList.RxTypes.SLOnTimeRx
        rxl.Text = "Recetas Dentro de Tiempo Generado y Pulido"
        Me.Cursor = Cursors.WaitCursor
        rxl.Show()
        Me.Cursor = Cursors.Default
    End Sub


    Private Sub RedBarSL_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RedBarSL.Click, LabelSLNotOnTime.Click
        Dim rxl As New RxList
        rxl.RxType = RxList.RxTypes.SLOutOfTimeRx
        rxl.Text = "Recetas Fuera de Tiempo Generado y Pulido"
        Me.Cursor = Cursors.WaitCursor
        rxl.Show()
        Me.Cursor = Cursors.Default
    End Sub

    Private Sub LabelSL_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles LabelSL.Click
        Dim rxl As New RxList
        rxl.RxType = RxList.RxTypes.SLAllOpenRx
        rxl.Text = "Total Recetas Generado y Pulido"
        Me.Cursor = Cursors.WaitCursor
        rxl.Show()
        Me.Cursor = Cursors.Default
    End Sub

    Private Sub GreenBarBM_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles GreenBarBM.Click, LabelBMOnTime.Click
        Dim rxl As New RxList
        rxl.RxType = RxList.RxTypes.FLOnTimeRx
        rxl.Text = "Recetas Dentro de Tiempos Bisel y Montado"
        Me.Cursor = Cursors.WaitCursor
        rxl.Show()
        Me.Cursor = Cursors.Default
    End Sub

    Private Sub GreenBarAR_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles GreenBarAR.Click, LabelAROnTime.Click
        Dim rxl As New RxList
        rxl.RxType = RxList.RxTypes.AROnTimeRx
        rxl.Text = "Recetas Dentro de Tiempos AR"
        Me.Cursor = Cursors.WaitCursor
        rxl.Show()
        Me.Cursor = Cursors.Default
    End Sub

    Private Sub RedBarAR_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RedBarAR.Click, LabelARNotOnTime.Click
        Dim rxl As New RxList
        rxl.RxType = RxList.RxTypes.AROutOfTimeRx
        rxl.Text = "Recetas Fuera de Tiempos AR"
        Me.Cursor = Cursors.WaitCursor
        rxl.Show()
        Me.Cursor = Cursors.Default
    End Sub

    Private Sub LabelAR_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles LabelAR.Click
        Dim rxl As New RxList
        rxl.RxType = RxList.RxTypes.ARAllOpenRx
        rxl.Text = "Total Recetas AR"
        Me.Cursor = Cursors.WaitCursor
        rxl.Show()
        Me.Cursor = Cursors.Default
    End Sub

    Private Sub RedBarBM_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RedBarBM.Click, LabelBMNotOnTime.Click
        Dim rxl As New RxList
        rxl.RxType = RxList.RxTypes.FLOutOfTimeRx
        rxl.Text = "Recetas Fuera de Tiempo Bisel y Montado"
        Me.Cursor = Cursors.WaitCursor
        rxl.Show()
        Me.Cursor = Cursors.Default
    End Sub

    Private Sub LabelBM_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles LabelBM.Click
        Dim rxl As New RxList
        rxl.RxType = RxList.RxTypes.FLAllOpenRx
        rxl.Text = "Total de Recetas Bisel y Montado"
        Me.Cursor = Cursors.WaitCursor
        rxl.Show()
        Me.Cursor = Cursors.Default
    End Sub

    Private Sub ProcessEfficiency_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

    End Sub

    Private Sub LabelPC_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles LabelPC.Click
        Dim rxl As New RxList
        rxl.RxType = RxList.RxTypes.PCAllOpenRx
        rxl.Text = "Total de Recetas Por Cerrar"
        Me.Cursor = Cursors.WaitCursor
        rxl.Show()
        Me.Cursor = Cursors.Default
    End Sub

    Private Sub LabelPCOnTime_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles LabelPCOnTime.Click, GreenBarPC.Click
        Dim rxl As New RxList
        rxl.RxType = RxList.RxTypes.PCOnTimeRx
        rxl.Text = "Total Dentro de Tiempos Por Cerrar"
        Me.Cursor = Cursors.WaitCursor
        rxl.Show()
        Me.Cursor = Cursors.Default
    End Sub

    Private Sub LabelPCNotOnTime_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles LabelPCNotOnTime.Click, RedBarPC.Click
        Dim rxl As New RxList
        rxl.RxType = RxList.RxTypes.PCOutOfTimeRx
        rxl.Text = "Total Fuera de Tiempo Por Cerrar"
        Me.Cursor = Cursors.WaitCursor
        rxl.Show()
        Me.Cursor = Cursors.Default
    End Sub

    Private Sub BotonActualizar_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BotonActualizar.Load

    End Sub
End Class
