<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class ReadFrame
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
        Me.TBX_FrameID = New System.Windows.Forms.TextBox
        Me.LabelARLab = New System.Windows.Forms.Label
        Me.BTN_OK = New RoundButtonSmall.UserControl1
        Me.BTN_Cancelar = New RoundButtonSmall.UserControl1
        Me.SuspendLayout()
        '
        'TBX_FrameID
        '
        Me.TBX_FrameID.Font = New System.Drawing.Font("Arial", 24.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TBX_FrameID.Location = New System.Drawing.Point(34, 36)
        Me.TBX_FrameID.Name = "TBX_FrameID"
        Me.TBX_FrameID.Size = New System.Drawing.Size(179, 44)
        Me.TBX_FrameID.TabIndex = 0
        Me.TBX_FrameID.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'LabelARLab
        '
        Me.LabelARLab.AutoSize = True
        Me.LabelARLab.BackColor = System.Drawing.Color.Transparent
        Me.LabelARLab.Font = New System.Drawing.Font("DINMittelschrift LT", 12.0!)
        Me.LabelARLab.ForeColor = System.Drawing.Color.FromArgb(CType(CType(158, Byte), Integer), CType(CType(209, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.LabelARLab.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.LabelARLab.Location = New System.Drawing.Point(29, 9)
        Me.LabelARLab.Name = "LabelARLab"
        Me.LabelARLab.Size = New System.Drawing.Size(189, 20)
        Me.LabelARLab.TabIndex = 104
        Me.LabelARLab.Text = "Lea el Código del Armazón"
        '
        'BTN_OK
        '
        Me.BTN_OK.BackColor = System.Drawing.Color.Transparent
        Me.BTN_OK.DisabledForeColor = System.Drawing.Color.Gray
        Me.BTN_OK.Font = New System.Drawing.Font("DINMittelschrift LT", 13.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel)
        Me.BTN_OK.ForeColor = System.Drawing.Color.White
        Me.BTN_OK.Highlighted = True
        Me.BTN_OK.Location = New System.Drawing.Point(24, 90)
        Me.BTN_OK.Name = "BTN_OK"
        Me.BTN_OK.Size = New System.Drawing.Size(89, 37)
        Me.BTN_OK.TabIndex = 125
        Me.BTN_OK.Texto = "Aceptar"
        '
        'BTN_Cancelar
        '
        Me.BTN_Cancelar.BackColor = System.Drawing.Color.Transparent
        Me.BTN_Cancelar.DisabledForeColor = System.Drawing.Color.Gray
        Me.BTN_Cancelar.Font = New System.Drawing.Font("DINMittelschrift LT", 13.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel)
        Me.BTN_Cancelar.ForeColor = System.Drawing.Color.White
        Me.BTN_Cancelar.Highlighted = False
        Me.BTN_Cancelar.Location = New System.Drawing.Point(134, 90)
        Me.BTN_Cancelar.Name = "BTN_Cancelar"
        Me.BTN_Cancelar.Size = New System.Drawing.Size(89, 37)
        Me.BTN_Cancelar.TabIndex = 126
        Me.BTN_Cancelar.Texto = "Cancelar"
        '
        'ReadFrame
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackgroundImage = Global.Laboratorios.My.Resources.Resources.VistaBackground
        Me.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.ClientSize = New System.Drawing.Size(247, 145)
        Me.Controls.Add(Me.BTN_Cancelar)
        Me.Controls.Add(Me.BTN_OK)
        Me.Controls.Add(Me.LabelARLab)
        Me.Controls.Add(Me.TBX_FrameID)
        Me.DoubleBuffered = True
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow
        Me.Name = "ReadFrame"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "Armazón"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents TBX_FrameID As System.Windows.Forms.TextBox
    Friend WithEvents LabelARLab As System.Windows.Forms.Label
    Friend WithEvents BTN_OK As RoundButtonSmall.UserControl1
    Friend WithEvents BTN_Cancelar As RoundButtonSmall.UserControl1
End Class
