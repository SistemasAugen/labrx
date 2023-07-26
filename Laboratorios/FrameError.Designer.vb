<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrameError
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
        Me.BTN_Cancelar = New RoundButtonSmall.UserControl1
        Me.CMB_FrameErrors = New System.Windows.Forms.ComboBox
        Me.BTN_Ok = New RoundButtonSmall.UserControl1
        Me.Label1 = New System.Windows.Forms.Label
        Me.SuspendLayout()
        '
        'BTN_Cancelar
        '
        Me.BTN_Cancelar.BackColor = System.Drawing.Color.Transparent
        Me.BTN_Cancelar.DisabledForeColor = System.Drawing.Color.Gray
        Me.BTN_Cancelar.Font = New System.Drawing.Font("DINMittelschrift LT", 13.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel)
        Me.BTN_Cancelar.ForeColor = System.Drawing.Color.White
        Me.BTN_Cancelar.Highlighted = False
        Me.BTN_Cancelar.Location = New System.Drawing.Point(150, 87)
        Me.BTN_Cancelar.Name = "BTN_Cancelar"
        Me.BTN_Cancelar.Size = New System.Drawing.Size(93, 30)
        Me.BTN_Cancelar.TabIndex = 125
        Me.BTN_Cancelar.Texto = "Cancelar"
        '
        'CMB_FrameErrors
        '
        Me.CMB_FrameErrors.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.CMB_FrameErrors.Font = New System.Drawing.Font("DINMittelschrift LT", 14.0!)
        Me.CMB_FrameErrors.FormattingEnabled = True
        Me.CMB_FrameErrors.Items.AddRange(New Object() {"METAL", "PLASTICO", "PERFORADO", "RANURADO"})
        Me.CMB_FrameErrors.Location = New System.Drawing.Point(26, 40)
        Me.CMB_FrameErrors.MaxDropDownItems = 27
        Me.CMB_FrameErrors.Name = "CMB_FrameErrors"
        Me.CMB_FrameErrors.Size = New System.Drawing.Size(230, 31)
        Me.CMB_FrameErrors.TabIndex = 123
        '
        'BTN_Ok
        '
        Me.BTN_Ok.BackColor = System.Drawing.Color.Transparent
        Me.BTN_Ok.DisabledForeColor = System.Drawing.Color.Gray
        Me.BTN_Ok.Font = New System.Drawing.Font("DINMittelschrift LT", 13.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel)
        Me.BTN_Ok.ForeColor = System.Drawing.Color.White
        Me.BTN_Ok.Highlighted = True
        Me.BTN_Ok.Location = New System.Drawing.Point(38, 87)
        Me.BTN_Ok.Name = "BTN_Ok"
        Me.BTN_Ok.Size = New System.Drawing.Size(93, 30)
        Me.BTN_Ok.TabIndex = 124
        Me.BTN_Ok.Texto = "Aceptar"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.BackColor = System.Drawing.Color.Transparent
        Me.Label1.Font = New System.Drawing.Font("DINMittelschrift LT", 14.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.ForeColor = System.Drawing.Color.FromArgb(CType(CType(158, Byte), Integer), CType(CType(209, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.Label1.Location = New System.Drawing.Point(43, 10)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(194, 23)
        Me.Label1.TabIndex = 126
        Me.Label1.Text = "Escoge el tipo de error"
        '
        'FrameError
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackgroundImage = Global.Laboratorios.My.Resources.Resources.VistaBackground
        Me.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.ClientSize = New System.Drawing.Size(283, 136)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.BTN_Cancelar)
        Me.Controls.Add(Me.CMB_FrameErrors)
        Me.Controls.Add(Me.BTN_Ok)
        Me.DoubleBuffered = True
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow
        Me.Name = "FrameError"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "Error de Armazón"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents BTN_Cancelar As RoundButtonSmall.UserControl1
    Friend WithEvents CMB_FrameErrors As System.Windows.Forms.ComboBox
    Friend WithEvents BTN_Ok As RoundButtonSmall.UserControl1
    Friend WithEvents Label1 As System.Windows.Forms.Label
End Class
