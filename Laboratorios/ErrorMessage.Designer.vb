<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class ErrorMessage
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
        Me.components = New System.ComponentModel.Container
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(ErrorMessage))
        Me.TBX_Error = New System.Windows.Forms.TextBox
        Me.BTN_Ok = New RoundButtonSmall.UserControl1
        Me.BTN_Yes = New RoundButtonSmall.UserControl1
        Me.BTN_No = New RoundButtonSmall.UserControl1
        Me.BTN_Retry = New RoundButtonSmall.UserControl1
        Me.BTN_Cancel = New RoundButtonSmall.UserControl1
        Me.PBX_ErrorImage = New System.Windows.Forms.PictureBox
        CType(Me.PBX_ErrorImage, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'TBX_Error
        '
        Me.TBX_Error.AccessibleDescription = Nothing
        Me.TBX_Error.AccessibleName = Nothing
        resources.ApplyResources(Me.TBX_Error, "TBX_Error")
        Me.TBX_Error.BackColor = System.Drawing.Color.White
        Me.TBX_Error.BackgroundImage = Nothing
        Me.TBX_Error.Name = "TBX_Error"
        Me.TBX_Error.ReadOnly = True
        '
        'BTN_Ok
        '
        Me.BTN_Ok.AccessibleDescription = Nothing
        Me.BTN_Ok.AccessibleName = Nothing
        resources.ApplyResources(Me.BTN_Ok, "BTN_Ok")
        Me.BTN_Ok.BackColor = System.Drawing.Color.Transparent
        Me.BTN_Ok.BackgroundImage = Nothing
        Me.BTN_Ok.DisabledForeColor = System.Drawing.Color.Gray
        Me.BTN_Ok.ForeColor = System.Drawing.Color.White
        Me.BTN_Ok.Highlighted = False
        Me.BTN_Ok.Name = "BTN_Ok"
        Me.BTN_Ok.Texto = "Ok"
        '
        'BTN_Yes
        '
        Me.BTN_Yes.AccessibleDescription = Nothing
        Me.BTN_Yes.AccessibleName = Nothing
        resources.ApplyResources(Me.BTN_Yes, "BTN_Yes")
        Me.BTN_Yes.BackColor = System.Drawing.Color.Transparent
        Me.BTN_Yes.BackgroundImage = Nothing
        Me.BTN_Yes.DisabledForeColor = System.Drawing.Color.Gray
        Me.BTN_Yes.ForeColor = System.Drawing.Color.White
        Me.BTN_Yes.Highlighted = False
        Me.BTN_Yes.Name = "BTN_Yes"
        Me.BTN_Yes.Texto = "Yes"
        '
        'BTN_No
        '
        Me.BTN_No.AccessibleDescription = Nothing
        Me.BTN_No.AccessibleName = Nothing
        resources.ApplyResources(Me.BTN_No, "BTN_No")
        Me.BTN_No.BackColor = System.Drawing.Color.Transparent
        Me.BTN_No.BackgroundImage = Nothing
        Me.BTN_No.DisabledForeColor = System.Drawing.Color.Gray
        Me.BTN_No.ForeColor = System.Drawing.Color.White
        Me.BTN_No.Highlighted = False
        Me.BTN_No.Name = "BTN_No"
        Me.BTN_No.Texto = "No"
        '
        'BTN_Retry
        '
        Me.BTN_Retry.AccessibleDescription = Nothing
        Me.BTN_Retry.AccessibleName = Nothing
        resources.ApplyResources(Me.BTN_Retry, "BTN_Retry")
        Me.BTN_Retry.BackColor = System.Drawing.Color.Transparent
        Me.BTN_Retry.BackgroundImage = Nothing
        Me.BTN_Retry.DisabledForeColor = System.Drawing.Color.Gray
        Me.BTN_Retry.ForeColor = System.Drawing.Color.White
        Me.BTN_Retry.Highlighted = False
        Me.BTN_Retry.Name = "BTN_Retry"
        Me.BTN_Retry.Texto = "Reintentar"
        '
        'BTN_Cancel
        '
        Me.BTN_Cancel.AccessibleDescription = Nothing
        Me.BTN_Cancel.AccessibleName = Nothing
        resources.ApplyResources(Me.BTN_Cancel, "BTN_Cancel")
        Me.BTN_Cancel.BackColor = System.Drawing.Color.Transparent
        Me.BTN_Cancel.BackgroundImage = Nothing
        Me.BTN_Cancel.DisabledForeColor = System.Drawing.Color.Gray
        Me.BTN_Cancel.ForeColor = System.Drawing.Color.White
        Me.BTN_Cancel.Highlighted = False
        Me.BTN_Cancel.Name = "BTN_Cancel"
        Me.BTN_Cancel.Texto = "Cancel"
        '
        'PBX_ErrorImage
        '
        Me.PBX_ErrorImage.AccessibleDescription = Nothing
        Me.PBX_ErrorImage.AccessibleName = Nothing
        resources.ApplyResources(Me.PBX_ErrorImage, "PBX_ErrorImage")
        Me.PBX_ErrorImage.BackColor = System.Drawing.Color.Transparent
        Me.PBX_ErrorImage.BackgroundImage = Nothing
        Me.PBX_ErrorImage.Font = Nothing
        Me.PBX_ErrorImage.Image = Global.Laboratorios.My.Resources.Resources.ico_error
        Me.PBX_ErrorImage.ImageLocation = Nothing
        Me.PBX_ErrorImage.InitialImage = Nothing
        Me.PBX_ErrorImage.Name = "PBX_ErrorImage"
        Me.PBX_ErrorImage.TabStop = False
        '
        'ErrorMessage
        '
        Me.AccessibleDescription = Nothing
        Me.AccessibleName = Nothing
        resources.ApplyResources(Me, "$this")
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackgroundImage = Nothing
        Me.Controls.Add(Me.PBX_ErrorImage)
        Me.Controls.Add(Me.BTN_Cancel)
        Me.Controls.Add(Me.BTN_Retry)
        Me.Controls.Add(Me.BTN_No)
        Me.Controls.Add(Me.BTN_Yes)
        Me.Controls.Add(Me.BTN_Ok)
        Me.Controls.Add(Me.TBX_Error)
        Me.DoubleBuffered = True
        Me.Font = Nothing
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.Icon = Nothing
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "ErrorMessage"
        Me.ShowInTaskbar = False
        CType(Me.PBX_ErrorImage, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents PBX_ErrorImage As System.Windows.Forms.PictureBox
    Friend WithEvents TBX_Error As System.Windows.Forms.TextBox
    Friend WithEvents BTN_Ok As RoundButtonSmall.UserControl1
    Friend WithEvents BTN_Yes As RoundButtonSmall.UserControl1
    Friend WithEvents BTN_No As RoundButtonSmall.UserControl1
    Friend WithEvents BTN_Retry As RoundButtonSmall.UserControl1
    Friend WithEvents BTN_Cancel As RoundButtonSmall.UserControl1

End Class
