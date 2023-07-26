Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.Shared
Public Class RxPreview

    Private Sub RxPreview_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Dim reporte As New ReportDocument
        Dim dt As New DataSet
        Try
            reporte.Load(My.Application.Info.DirectoryPath & "\" & My.Settings.ReporteReceta1)
            dt.ReadXml(My.Application.Info.DirectoryPath & "\LabRx.xml")

            reporte.SetDataSource(dt)
            RxReport.ReportSource = reporte
        Catch ex As Exception
            MsgBox("RxPreview " + ex.Message, MsgBoxStyle.Critical, "RxPreview")
        End Try

        ''Dim dt As New DataSet
        ''Dim reporte As New ReportDocument
        '' Try
        ''    reporte.Load(My.Application.Info.DirectoryPath & "\" & My.Settings.ReporteReceta1)

        ''    dt.ReadXml(My.Application.Info.DirectoryPath & "\LabRx.xml")
        ''    'reporte.PrintToPrinter(1, False, 1, 1)
        ''    reporte.SetDataSource(dt)
        ''    reporte.PrintToPrinter(1, False, 1, 1)
        ''    'RxReport.ReportSource = reporte

        ''Catch ex As Exception
        ''    MsgBox(ex.Message)
        ''    MsgBox("Ocurrió un error al imprimir la receta. Para continuar debes cerrar la aplicacion y ejecutarla de nuevo." & vbCrLf & "Debes reimprimir la Rx con una modificación con Vantage: " & vbCrLf & vbCrLf & ex.Message, MsgBoxStyle.Critical)
        ''End Try
        ''reporte.Dispose()


    End Sub
End Class