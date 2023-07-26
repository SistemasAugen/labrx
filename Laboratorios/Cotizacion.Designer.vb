<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Cotizacion
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
        Me.LblRxTotal = New System.Windows.Forms.Label
        Me.Label23 = New System.Windows.Forms.Label
        Me.LViewLines = New System.Windows.Forms.ListView
        Me.Preview = New System.Windows.Forms.Button
        Me.Button1 = New System.Windows.Forms.Button
        Me.SuspendLayout()
        '
        'LblRxTotal
        '
        Me.LblRxTotal.AutoSize = True
        Me.LblRxTotal.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, CType((System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Underline), System.Drawing.FontStyle), System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblRxTotal.ForeColor = System.Drawing.SystemColors.ActiveCaption
        Me.LblRxTotal.Location = New System.Drawing.Point(405, 197)
        Me.LblRxTotal.Name = "LblRxTotal"
        Me.LblRxTotal.Size = New System.Drawing.Size(68, 17)
        Me.LblRxTotal.TabIndex = 40
        Me.LblRxTotal.Text = "Total Rx"
        '
        'Label23
        '
        Me.Label23.AutoSize = True
        Me.Label23.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label23.Location = New System.Drawing.Point(330, 198)
        Me.Label23.Name = "Label23"
        Me.Label23.Size = New System.Drawing.Size(68, 17)
        Me.Label23.TabIndex = 39
        Me.Label23.Text = "Total Rx"
        '
        'LViewLines
        '
        Me.LViewLines.FullRowSelect = True
        Me.LViewLines.GridLines = True
        Me.LViewLines.LabelEdit = True
        Me.LViewLines.Location = New System.Drawing.Point(26, 21)
        Me.LViewLines.MultiSelect = False
        Me.LViewLines.Name = "LViewLines"
        Me.LViewLines.Size = New System.Drawing.Size(444, 174)
        Me.LViewLines.Sorting = System.Windows.Forms.SortOrder.Ascending
        Me.LViewLines.TabIndex = 37
        Me.LViewLines.UseCompatibleStateImageBehavior = False
        Me.LViewLines.View = System.Windows.Forms.View.Details
        '
        'Preview
        '
        Me.Preview.DialogResult = System.Windows.Forms.DialogResult.OK
        Me.Preview.Font = New System.Drawing.Font("DINMittelschrift LT", 16.0!)
        Me.Preview.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Preview.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.Preview.Location = New System.Drawing.Point(192, 239)
        Me.Preview.Name = "Preview"
        Me.Preview.Size = New System.Drawing.Size(131, 37)
        Me.Preview.TabIndex = 108
        Me.Preview.Text = "Continuar"
        Me.Preview.UseVisualStyleBackColor = True
        '
        'Button1
        '
        Me.Button1.DialogResult = System.Windows.Forms.DialogResult.OK
        Me.Button1.Font = New System.Drawing.Font("DINMittelschrift LT", 16.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Button1.Location = New System.Drawing.Point(342, 239)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(131, 37)
        Me.Button1.TabIndex = 109
        Me.Button1.Text = "Cancelar"
        Me.Button1.UseVisualStyleBackColor = True
        '
        'Cotizacion
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(499, 288)
        Me.Controls.Add(Me.Button1)
        Me.Controls.Add(Me.Preview)
        Me.Controls.Add(Me.LblRxTotal)
        Me.Controls.Add(Me.Label23)
        Me.Controls.Add(Me.LViewLines)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "Cotizacion"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "Cotizacion"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents LblRxTotal As System.Windows.Forms.Label
    Friend WithEvents Label23 As System.Windows.Forms.Label
    Friend WithEvents LViewLines As System.Windows.Forms.ListView
    Friend WithEvents Preview As System.Windows.Forms.Button
    Friend WithEvents Button1 As System.Windows.Forms.Button
End Class
