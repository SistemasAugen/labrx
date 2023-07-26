Public Class Creditos

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

    Private Sub Creditos_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

    End Sub
End Class