<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class RxPreview
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.RxReport = New CrystalDecisions.Windows.Forms.CrystalReportViewer()
        Me.ReportExecutionService1 = New Microsoft.Reporting.WinForms.Internal.Soap.ReportingServices2005.Execution.ReportExecutionService()
        Me.SuspendLayout()
        '
        'RxReport
        '
        Me.RxReport.ActiveViewIndex = -1
        Me.RxReport.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.RxReport.Cursor = System.Windows.Forms.Cursors.Default
        Me.RxReport.Dock = System.Windows.Forms.DockStyle.Fill
        Me.RxReport.Location = New System.Drawing.Point(0, 0)
        Me.RxReport.Name = "RxReport"
        Me.RxReport.SelectionFormula = ""
        Me.RxReport.Size = New System.Drawing.Size(523, 427)
        Me.RxReport.TabIndex = 0
        Me.RxReport.ViewTimeSelectionFormula = ""
        '
        'ReportExecutionService1
        '
        Me.ReportExecutionService1.Credentials = Nothing
        Me.ReportExecutionService1.ExecutionHeaderValue = Nothing
        Me.ReportExecutionService1.PrintControlClsidHeaderValue = Nothing
        Me.ReportExecutionService1.ServerInfoHeaderValue = Nothing
        Me.ReportExecutionService1.TrustedUserHeaderValue = Nothing
        Me.ReportExecutionService1.Url = "http://localhost/ReportServer/ReportExecution2005.asmx"
        Me.ReportExecutionService1.UseDefaultCredentials = False
        '
        'RxPreview
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(523, 427)
        Me.Controls.Add(Me.RxReport)
        Me.Name = "RxPreview"
        Me.Text = "RxPreview"
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents RxReport As CrystalDecisions.Windows.Forms.CrystalReportViewer
    Friend WithEvents ReportExecutionService1 As Microsoft.Reporting.WinForms.Internal.Soap.ReportingServices2005.Execution.ReportExecutionService
End Class
