Public Class SpecialDesigns

    Public Selected As Boolean = False
    Dim ds As DataSet

    Public Property HDRx() As String
        Get
            Return LBL_QtyHDRX.Text
        End Get
        Set(ByVal value As String)
            LBL_QtyHDRX.Text = value
        End Set
    End Property
    Public Property TRIN8_12() As String
        Get
            Return LBL_QtyTRIN8_12.Text
        End Get
        Set(ByVal value As String)
            LBL_QtyTRIN8_12.Text = value
        End Set
    End Property
    Public Property TRIN8_12HD() As String
        Get
            Return LBL_QtyTRIN8_12HD.Text
        End Get
        Set(ByVal value As String)
            LBL_QtyTRIN8_12HD.Text = value
        End Set
    End Property
    Public Property SPACIA()
        Get
            Return LBL_QtySPACIA.Text
        End Get
        Set(ByVal value)
            LBL_QtySPACIA.Text = value
        End Set
    End Property
    Public Property SPACIAHD()
        Get
            Return LBL_QtySPACIAHD.Text
        End Get
        Set(ByVal value)
            LBL_QtySPACIAHD.Text = value
        End Set
    End Property


    Private Sub BTN_Ok_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BTN_Ok.Click
        ''Dim Failed As Boolean = True
        ''For Each row As DataGridViewRow In DGV_Creditos.Rows
        ''    If row.Cells("ID").Value = CMB_Designs.SelectedValue Then
        ''        If row.Cells("Cantidad").Value > 0 Then
        ''            Failed = False
        ''        End If
        ''        Exit For
        ''    End If
        ''Next
        'Select Case CMB_Designs.SelectedValue
        '    Case 1
        '        If HDRx = 0 Then Failed = True
        '    Case 2
        '        If TRIN8_12 = 0 Then Failed = True
        '    Case 3
        '        If TRIN8_12HD = 0 Then Failed = True
        '    Case 4
        '        If SPACIA = 0 Then Failed = True
        '    Case 5
        '        If SPACIAHD = 0 Then Failed = True
        'End Select
        ''If Failed Then
        ''    'Dim em As New ErrorMessage()
        ''    'em.TBX_Error.Text = RM.GetString("ERR_DISESP_NoCredito")
        ''    'em.Text = My.Application.Info.ProductName
        ''    'em.ShowDialog()
        ''    MsgBox("El diseño seleccionado no tiene créditos disponibles." & vbCrLf & _
        ''           "Si sera una rx virtual no habra problema en la captura, de lo contrario, solicite créditos", MsgBoxStyle.Information, My.Application.Info.ProductName)
        ''    'Else                   ' Comentado por fgarcia en Abril 5, 2011.
        ''    '    Selected = True
        ''    '    Close()
        ''End If

        Selected = True
        Close()

    End Sub

    Private Sub BTN_Cancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BTN_Cancel.Click
        Selected = False
        Close()
    End Sub

    Private Sub SpecialDesigns_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'obtener disenios especiales
        Dim Failed As Boolean = False
        Dim FailedMessage As String = ""
        Dim t As New SqlDB(Laboratorios.ConnStr)
        Try
            t.OpenConn()
            'Dim ds As DataSet
            ds = t.SQLDS("select idDesign,design,partnum from tblspecialdesigns with(nolock)", "t1")
            CMB_Designs.DataSource = ds.Tables("t1")
            CMB_Designs.DisplayMember = "design"
            CMB_Designs.ValueMember = "idDesign"
        Catch ex As Exception
            Failed = True
            FailedMessage = ex.Message
        Finally
            t.CloseConn()
            If Failed Then
                Throw New Exception(FailedMessage)
            End If
        End Try
    End Sub

    Private Sub CMB_Designs_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CMB_Designs.SelectedIndexChanged

    End Sub

    Public Function getSDPartnum(ByVal id As Integer)
        Dim partnum As String = ""
        Dim row As DataRow

        Try
            For Each row In ds.Tables("t1").Rows
                If (id = CType(row(0), Integer)) Then
                    partnum = row(2).ToString()
                    Exit For
                End If
            Next
        Catch ex As Exception

        End Try

        Return partnum
    End Function
End Class