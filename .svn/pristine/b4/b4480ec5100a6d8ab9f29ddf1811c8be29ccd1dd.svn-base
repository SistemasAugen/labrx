Public Class Prismas
    Public Status As Boolean
    Public PrismaR As Double
    Public EjePrismaR As Double
    Public PrismaL As Double
    Public EjePrismaL As Double
    Private Sub Prismas_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Status = False
        PrismaL = 0
        EjePrismaL = 0
        PrismaR = 0
        EjePrismaR = 0
    End Sub

    Private Sub EjePrismaROtro_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles EjePrismaROtro.CheckedChanged
        If Me.EjePrismaROtro.Checked Then
            EjePrismaROtroVal.Enabled = True
        Else
            EjePrismaROtroVal.Enabled = False
        End If
    End Sub

    Private Sub EjePrismaLOtro_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles EjePrismaLOtro.CheckedChanged
        If Me.EjePrismaLOtro.Checked Then
            EjePrismaLOtroVal.Enabled = True
        Else
            EjePrismaLOtroVal.Enabled = False
        End If
    End Sub

  

    Private Sub EjePrismaRS_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles EjePrismaRS.CheckedChanged
        If EjePrismaRS.Checked Then
            EjePrismaR = 90
        End If
    End Sub

    Private Sub EjePrismaRI_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles EjePrismaRI.CheckedChanged
        If EjePrismaRI.Checked Then
            EjePrismaR = 270
        End If

    End Sub

    Private Sub EjePrismaRN_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles EjePrismaRN.CheckedChanged
        If EjePrismaRN.Checked Then
            EjePrismaR = 0
        End If

    End Sub

    Private Sub EjePrismaRT_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles EjePrismaRT.CheckedChanged
        If EjePrismaRT.Checked Then
            EjePrismaR = 180
        End If

    End Sub

    Private Sub AceptarManual_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles AceptarManual.Click, BotonAceptar.Click
        PrismaL = TextPrismaL.Text
        PrismaR = TextPrismaR.Text
        If CheckEjePrismaRS.Checked = True Then EjePrismaR = 90
        If CheckEjePrismaRI.Checked = True Then EjePrismaR = 270
        If CheckEjePrismaRN.Checked = True Then EjePrismaR = 0
        If CheckEjePrismaRT.Checked = True Then EjePrismaR = 180
        If CheckEjePrismaLS.Checked = True Then EjePrismaL = 90
        If CheckEjePrismaLI.Checked = True Then EjePrismaL = 270
        If CheckEjePrismaLT.Checked = True Then EjePrismaL = 0
        If CheckEjePrismaLN.Checked = True Then EjePrismaL = 180
        If CheckEjePrismaLOtro.Checked = True Then EjePrismaL = EjePrismaLOtroVal.Text
        If CheckEjePrismaROtro.Checked = True Then EjePrismaR = EjePrismaROtroVal.Text
        Status = True
        Close()
    End Sub

    Private Sub EjePrismaROtroVal_Leave(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles EjePrismaROtroVal.Leave
        Try
            EjePrismaR = CDbl(EjePrismaROtroVal.Text)
        Catch ex As Exception
            EjePrismaR = 0
            EjePrismaROtroVal.Text = 0
        End Try
    End Sub

    Private Sub TextPrismaR_Leave(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TextPrismaR.Leave
        Try
            PrismaR = CDbl(TextPrismaR.Text)
        Catch ex As Exception
            PrismaR = 0
            TextPrismaR.Text = 0
        End Try
    End Sub

    Private Sub EjePrismaLOtroVal_Leave(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles EjePrismaLOtroVal.Leave
        Try
            EjePrismaL = CDbl(EjePrismaLOtroVal.Text)
        Catch ex As Exception
            EjePrismaL = 0
            EjePrismaLOtroVal.Text = 0
        End Try
    End Sub

    Private Sub TextPrismaL_Leave(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TextPrismaL.Leave
        Try
            PrismaL = CDbl(TextPrismaL.Text)
        Catch ex As Exception
            PrismaL = 0
            TextPrismaL.Text = 0
        End Try
    End Sub

    Private Sub EjePrismaLS_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CheckEjePrismaLS.CheckedChanged
        If CheckEjePrismaLS.Checked Then
            EjePrismaL = 90
            CheckEjePrismaLI.Checked = False
            CheckEjePrismaLN.Checked = False
            CheckEjePrismaLT.Checked = False
            CheckEjePrismaLOtro.Checked = False
        End If
    End Sub

    Private Sub EjePrismaLI_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CheckEjePrismaLI.CheckedChanged
        If CheckEjePrismaLI.Checked Then
            EjePrismaL = 270
            CheckEjePrismaLS.Checked = False
            CheckEjePrismaLN.Checked = False
            CheckEjePrismaLT.Checked = False
            CheckEjePrismaLOtro.Checked = False
        End If
    End Sub

    Private Sub EjePrismaLN_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CheckEjePrismaLN.CheckedChanged
        If CheckEjePrismaLN.Checked Then
            EjePrismaL = 180
            CheckEjePrismaLS.Checked = False
            CheckEjePrismaLI.Checked = False
            CheckEjePrismaLT.Checked = False
            CheckEjePrismaLOtro.Checked = False
        End If
    End Sub

    Private Sub EjePrismaLT_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CheckEjePrismaLT.CheckedChanged
        If CheckEjePrismaLT.Checked Then
            EjePrismaL = 0
            CheckEjePrismaLS.Checked = False
            CheckEjePrismaLI.Checked = False
            CheckEjePrismaLN.Checked = False
            CheckEjePrismaLOtro.Checked = False
        End If
    End Sub

    Private Sub CheckEjePrismaRS_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CheckEjePrismaRS.CheckedChanged
        If CheckEjePrismaRS.Checked Then
            EjePrismaR = 90
            CheckEjePrismaRI.Checked = False
            CheckEjePrismaRN.Checked = False
            CheckEjePrismaRT.Checked = False
            CheckEjePrismaROtro.Checked = False
        End If

    End Sub

    Private Sub CheckEjePrismaRI_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CheckEjePrismaRI.CheckedChanged
        If CheckEjePrismaRI.Checked Then
            EjePrismaR = 270
            CheckEjePrismaRS.Checked = False
            CheckEjePrismaRN.Checked = False
            CheckEjePrismaRT.Checked = False
            CheckEjePrismaROtro.Checked = False
        End If
    End Sub

    Private Sub CheckEjePrismaRN_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CheckEjePrismaRN.CheckedChanged
        If CheckEjePrismaRN.Checked Then
            EjePrismaR = 0
            CheckEjePrismaRS.Checked = False
            CheckEjePrismaRI.Checked = False
            CheckEjePrismaRT.Checked = False
            CheckEjePrismaROtro.Checked = False
        End If

    End Sub

    Private Sub CheckEjePrismaRT_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CheckEjePrismaRT.CheckedChanged
        If CheckEjePrismaRT.Checked Then
            EjePrismaR = 180
            CheckEjePrismaRS.Checked = False
            CheckEjePrismaRI.Checked = False
            CheckEjePrismaRN.Checked = False
            CheckEjePrismaROtro.Checked = False
        End If

    End Sub

    Private Sub CheckEjePrismaROtro_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CheckEjePrismaROtro.CheckedChanged
        If CheckEjePrismaROtro.Checked Then
            EjePrismaROtroVal.Enabled = True
            CheckEjePrismaRS.Checked = False
            CheckEjePrismaRI.Checked = False
            CheckEjePrismaRN.Checked = False
            CheckEjePrismaRT.Checked = False
        Else
            EjePrismaROtroVal.Enabled = False
        End If
    End Sub

    Private Sub CheckEjePrismaLOtro_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CheckEjePrismaLOtro.CheckedChanged
        If CheckEjePrismaLOtro.Checked Then
            EjePrismaLOtroVal.Enabled = True
            CheckEjePrismaLS.Checked = False
            CheckEjePrismaLI.Checked = False
            CheckEjePrismaLN.Checked = False
            CheckEjePrismaLT.Checked = False
        Else
            EjePrismaLOtroVal.Enabled = False
        End If

    End Sub
End Class