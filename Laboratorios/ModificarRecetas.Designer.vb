<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class ModificarRecetas
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        If disposing AndAlso components IsNot Nothing Then
            components.Dispose()
        End If
        MyBase.Dispose(disposing)
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container
        Me.Label1 = New System.Windows.Forms.Label
        Me.RxID = New System.Windows.Forms.TextBox
        Me.Cancel = New System.Windows.Forms.Button
        Me.OK = New System.Windows.Forms.Button
        Me.BotonAceptar = New RoundButtonSmall.UserControl1
        Me.BotonCancelar = New RoundButtonSmall.UserControl1
        Me.PanelRecepcion = New System.Windows.Forms.Panel
        Me.PanelRecepcion.SuspendLayout()
        Me.SuspendLayout()
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("DINMittelschrift LT Alternate", 18.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(16, 4)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(251, 28)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "Recepción de Rx Virtual"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'RxID
        '
        Me.RxID.BackColor = System.Drawing.Color.FromArgb(CType(CType(221, Byte), Integer), CType(CType(228, Byte), Integer), CType(CType(236, Byte), Integer))
        Me.RxID.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.RxID.Font = New System.Drawing.Font("DINMittelschrift LT Alternate", 15.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RxID.Location = New System.Drawing.Point(210, 133)
        Me.RxID.MaxLength = 9
        Me.RxID.Name = "RxID"
        Me.RxID.Size = New System.Drawing.Size(307, 24)
        Me.RxID.TabIndex = 0
        Me.RxID.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'Cancel
        '
        Me.Cancel.Font = New System.Drawing.Font("DINMittelschrift LT", 16.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Cancel.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Cancel.Location = New System.Drawing.Point(39, 92)
        Me.Cancel.Name = "Cancel"
        Me.Cancel.Size = New System.Drawing.Size(131, 37)
        Me.Cancel.TabIndex = 2
        Me.Cancel.TabStop = False
        Me.Cancel.Text = "Cancelar"
        Me.Cancel.UseVisualStyleBackColor = True
        Me.Cancel.Visible = False
        '
        'OK
        '
        Me.OK.Font = New System.Drawing.Font("DINMittelschrift LT", 16.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.OK.ForeColor = System.Drawing.SystemColors.ControlText
        Me.OK.Location = New System.Drawing.Point(27, 185)
        Me.OK.Name = "OK"
        Me.OK.Size = New System.Drawing.Size(131, 37)
        Me.OK.TabIndex = 1
        Me.OK.Text = "Aceptar"
        Me.OK.UseVisualStyleBackColor = True
        Me.OK.Visible = False
        '
        'BotonAceptar
        '
        Me.BotonAceptar.BackColor = System.Drawing.Color.Transparent
        Me.BotonAceptar.DisabledForeColor = System.Drawing.Color.Gray
        Me.BotonAceptar.Font = New System.Drawing.Font("DINMittelschrift LT Alternate", 13.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel)
        Me.BotonAceptar.ForeColor = System.Drawing.Color.White
        Me.BotonAceptar.Highlighted = True
        Me.BotonAceptar.Location = New System.Drawing.Point(201, 181)
        Me.BotonAceptar.Name = "BotonAceptar"
        Me.BotonAceptar.Size = New System.Drawing.Size(112, 40)
        Me.BotonAceptar.TabIndex = 119
        Me.BotonAceptar.Texto = "ACEPTAR"
        '
        'BotonCancelar
        '
        Me.BotonCancelar.BackColor = System.Drawing.Color.Transparent
        Me.BotonCancelar.DisabledForeColor = System.Drawing.Color.Gray
        Me.BotonCancelar.Font = New System.Drawing.Font("DINMittelschrift LT Alternate", 13.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel)
        Me.BotonCancelar.ForeColor = System.Drawing.Color.White
        Me.BotonCancelar.Highlighted = False
        Me.BotonCancelar.Location = New System.Drawing.Point(318, 181)
        Me.BotonCancelar.Name = "BotonCancelar"
        Me.BotonCancelar.Size = New System.Drawing.Size(112, 40)
        Me.BotonCancelar.TabIndex = 120
        Me.BotonCancelar.Texto = "CANCELAR"
        '
        'PanelRecepcion
        '
        Me.PanelRecepcion.BackColor = System.Drawing.Color.White
        Me.PanelRecepcion.Controls.Add(Me.Label1)
        Me.PanelRecepcion.Location = New System.Drawing.Point(178, 82)
        Me.PanelRecepcion.Name = "PanelRecepcion"
        Me.PanelRecepcion.Size = New System.Drawing.Size(277, 38)
        Me.PanelRecepcion.TabIndex = 121
        Me.PanelRecepcion.Visible = False
        '
        'ModificarRecetas
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackgroundImage = Global.Laboratorios.My.Resources.Resources.VistaPideRx
        Me.ClientSize = New System.Drawing.Size(585, 248)
        Me.ControlBox = False
        Me.Controls.Add(Me.PanelRecepcion)
        Me.Controls.Add(Me.BotonCancelar)
        Me.Controls.Add(Me.BotonAceptar)
        Me.Controls.Add(Me.OK)
        Me.Controls.Add(Me.Cancel)
        Me.Controls.Add(Me.RxID)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "ModificarRecetas"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "Receta a Modificar"
        Me.PanelRecepcion.ResumeLayout(False)
        Me.PanelRecepcion.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents RxID As System.Windows.Forms.TextBox
    Friend WithEvents Cancel As System.Windows.Forms.Button
    Friend WithEvents OK As System.Windows.Forms.Button
    Friend WithEvents BotonAceptar As RoundButtonSmall.UserControl1
    Friend WithEvents BotonCancelar As RoundButtonSmall.UserControl1
    Friend WithEvents PanelRecepcion As System.Windows.Forms.Panel
End Class
