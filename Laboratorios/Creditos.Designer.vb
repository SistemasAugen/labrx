<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Creditos
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
        Dim DataGridViewCellStyle1 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
        Me.PNL_Creditos = New System.Windows.Forms.Panel
        Me.LBL_QtySPACIAHD = New System.Windows.Forms.Label
        Me.LBL_QtySPACIA = New System.Windows.Forms.Label
        Me.LBL_QtyTRIN8_12HD = New System.Windows.Forms.Label
        Me.LBL_QtyTRIN8_12 = New System.Windows.Forms.Label
        Me.LBL_QtyHDRX = New System.Windows.Forms.Label
        Me.Label19 = New System.Windows.Forms.Label
        Me.Label17 = New System.Windows.Forms.Label
        Me.Label16 = New System.Windows.Forms.Label
        Me.Label13 = New System.Windows.Forms.Label
        Me.Label12 = New System.Windows.Forms.Label
        Me.Label11 = New System.Windows.Forms.Label
        Me.DGV_Creditos = New System.Windows.Forms.DataGridView
        Me.ID = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.Descripcion = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.Cantidad = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.PNL_Creditos.SuspendLayout()
        CType(Me.DGV_Creditos, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'PNL_Creditos
        '
        Me.PNL_Creditos.BackgroundImage = Global.Laboratorios.My.Resources.Resources.VistaBackground
        Me.PNL_Creditos.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.PNL_Creditos.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.PNL_Creditos.Controls.Add(Me.LBL_QtySPACIAHD)
        Me.PNL_Creditos.Controls.Add(Me.LBL_QtySPACIA)
        Me.PNL_Creditos.Controls.Add(Me.LBL_QtyTRIN8_12HD)
        Me.PNL_Creditos.Controls.Add(Me.LBL_QtyTRIN8_12)
        Me.PNL_Creditos.Controls.Add(Me.LBL_QtyHDRX)
        Me.PNL_Creditos.Controls.Add(Me.Label19)
        Me.PNL_Creditos.Controls.Add(Me.Label17)
        Me.PNL_Creditos.Controls.Add(Me.Label16)
        Me.PNL_Creditos.Controls.Add(Me.Label13)
        Me.PNL_Creditos.Controls.Add(Me.Label12)
        Me.PNL_Creditos.Controls.Add(Me.Label11)
        Me.PNL_Creditos.Font = New System.Drawing.Font("Microsoft Sans Serif", 14.0!)
        Me.PNL_Creditos.ForeColor = System.Drawing.Color.White
        Me.PNL_Creditos.Location = New System.Drawing.Point(0, 0)
        Me.PNL_Creditos.Name = "PNL_Creditos"
        Me.PNL_Creditos.Size = New System.Drawing.Size(228, 155)
        Me.PNL_Creditos.TabIndex = 139
        '
        'LBL_QtySPACIAHD
        '
        Me.LBL_QtySPACIAHD.AutoSize = True
        Me.LBL_QtySPACIAHD.BackColor = System.Drawing.Color.Transparent
        Me.LBL_QtySPACIAHD.Font = New System.Drawing.Font("DINMittelschrift LT", 12.0!)
        Me.LBL_QtySPACIAHD.ForeColor = System.Drawing.Color.Lime
        Me.LBL_QtySPACIAHD.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.LBL_QtySPACIAHD.Location = New System.Drawing.Point(179, 120)
        Me.LBL_QtySPACIAHD.Name = "LBL_QtySPACIAHD"
        Me.LBL_QtySPACIAHD.Size = New System.Drawing.Size(17, 20)
        Me.LBL_QtySPACIAHD.TabIndex = 145
        Me.LBL_QtySPACIAHD.Text = "0"
        Me.LBL_QtySPACIAHD.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'LBL_QtySPACIA
        '
        Me.LBL_QtySPACIA.AutoSize = True
        Me.LBL_QtySPACIA.BackColor = System.Drawing.Color.Transparent
        Me.LBL_QtySPACIA.Font = New System.Drawing.Font("DINMittelschrift LT", 12.0!)
        Me.LBL_QtySPACIA.ForeColor = System.Drawing.Color.Lime
        Me.LBL_QtySPACIA.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.LBL_QtySPACIA.Location = New System.Drawing.Point(179, 102)
        Me.LBL_QtySPACIA.Name = "LBL_QtySPACIA"
        Me.LBL_QtySPACIA.Size = New System.Drawing.Size(17, 20)
        Me.LBL_QtySPACIA.TabIndex = 144
        Me.LBL_QtySPACIA.Text = "0"
        Me.LBL_QtySPACIA.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'LBL_QtyTRIN8_12HD
        '
        Me.LBL_QtyTRIN8_12HD.AutoSize = True
        Me.LBL_QtyTRIN8_12HD.BackColor = System.Drawing.Color.Transparent
        Me.LBL_QtyTRIN8_12HD.Font = New System.Drawing.Font("DINMittelschrift LT", 12.0!)
        Me.LBL_QtyTRIN8_12HD.ForeColor = System.Drawing.Color.Lime
        Me.LBL_QtyTRIN8_12HD.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.LBL_QtyTRIN8_12HD.Location = New System.Drawing.Point(179, 82)
        Me.LBL_QtyTRIN8_12HD.Name = "LBL_QtyTRIN8_12HD"
        Me.LBL_QtyTRIN8_12HD.Size = New System.Drawing.Size(17, 20)
        Me.LBL_QtyTRIN8_12HD.TabIndex = 143
        Me.LBL_QtyTRIN8_12HD.Text = "0"
        Me.LBL_QtyTRIN8_12HD.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'LBL_QtyTRIN8_12
        '
        Me.LBL_QtyTRIN8_12.AutoSize = True
        Me.LBL_QtyTRIN8_12.BackColor = System.Drawing.Color.Transparent
        Me.LBL_QtyTRIN8_12.Font = New System.Drawing.Font("DINMittelschrift LT", 12.0!)
        Me.LBL_QtyTRIN8_12.ForeColor = System.Drawing.Color.Lime
        Me.LBL_QtyTRIN8_12.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.LBL_QtyTRIN8_12.Location = New System.Drawing.Point(179, 62)
        Me.LBL_QtyTRIN8_12.Name = "LBL_QtyTRIN8_12"
        Me.LBL_QtyTRIN8_12.Size = New System.Drawing.Size(17, 20)
        Me.LBL_QtyTRIN8_12.TabIndex = 142
        Me.LBL_QtyTRIN8_12.Text = "0"
        Me.LBL_QtyTRIN8_12.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'LBL_QtyHDRX
        '
        Me.LBL_QtyHDRX.AutoSize = True
        Me.LBL_QtyHDRX.BackColor = System.Drawing.Color.Transparent
        Me.LBL_QtyHDRX.Font = New System.Drawing.Font("DINMittelschrift LT", 12.0!)
        Me.LBL_QtyHDRX.ForeColor = System.Drawing.Color.Lime
        Me.LBL_QtyHDRX.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.LBL_QtyHDRX.Location = New System.Drawing.Point(179, 42)
        Me.LBL_QtyHDRX.Name = "LBL_QtyHDRX"
        Me.LBL_QtyHDRX.Size = New System.Drawing.Size(17, 20)
        Me.LBL_QtyHDRX.TabIndex = 141
        Me.LBL_QtyHDRX.Text = "0"
        Me.LBL_QtyHDRX.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label19
        '
        Me.Label19.AutoSize = True
        Me.Label19.BackColor = System.Drawing.Color.Transparent
        Me.Label19.Font = New System.Drawing.Font("DINMittelschrift LT", 12.0!)
        Me.Label19.ForeColor = System.Drawing.Color.White
        Me.Label19.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.Label19.Location = New System.Drawing.Point(3, 122)
        Me.Label19.Name = "Label19"
        Me.Label19.Size = New System.Drawing.Size(131, 20)
        Me.Label19.TabIndex = 140
        Me.Label19.Text = "PROG. SPACIA HD"
        Me.Label19.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label17
        '
        Me.Label17.AutoSize = True
        Me.Label17.BackColor = System.Drawing.Color.Transparent
        Me.Label17.Font = New System.Drawing.Font("DINMittelschrift LT", 12.0!)
        Me.Label17.ForeColor = System.Drawing.Color.White
        Me.Label17.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.Label17.Location = New System.Drawing.Point(3, 102)
        Me.Label17.Name = "Label17"
        Me.Label17.Size = New System.Drawing.Size(107, 20)
        Me.Label17.TabIndex = 139
        Me.Label17.Text = "PROG. SPACIA"
        Me.Label17.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label16
        '
        Me.Label16.AutoSize = True
        Me.Label16.BackColor = System.Drawing.Color.Transparent
        Me.Label16.Font = New System.Drawing.Font("DINMittelschrift LT", 12.0!)
        Me.Label16.ForeColor = System.Drawing.Color.White
        Me.Label16.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.Label16.Location = New System.Drawing.Point(3, 82)
        Me.Label16.Name = "Label16"
        Me.Label16.Size = New System.Drawing.Size(168, 20)
        Me.Label16.TabIndex = 138
        Me.Label16.Text = "PROG. TRINITY 8-12 HD"
        Me.Label16.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label13
        '
        Me.Label13.AutoSize = True
        Me.Label13.BackColor = System.Drawing.Color.Transparent
        Me.Label13.Font = New System.Drawing.Font("DINMittelschrift LT", 12.0!)
        Me.Label13.ForeColor = System.Drawing.Color.White
        Me.Label13.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.Label13.Location = New System.Drawing.Point(3, 62)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(144, 20)
        Me.Label13.TabIndex = 137
        Me.Label13.Text = "PROG. TRINITY 8-12"
        Me.Label13.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label12
        '
        Me.Label12.AutoSize = True
        Me.Label12.BackColor = System.Drawing.Color.Transparent
        Me.Label12.Font = New System.Drawing.Font("DINMittelschrift LT", 12.0!)
        Me.Label12.ForeColor = System.Drawing.Color.White
        Me.Label12.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.Label12.Location = New System.Drawing.Point(3, 42)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(129, 20)
        Me.Label12.TabIndex = 136
        Me.Label12.Text = "VS HI DEFINITION"
        Me.Label12.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label11
        '
        Me.Label11.AutoSize = True
        Me.Label11.BackColor = System.Drawing.Color.Transparent
        Me.Label11.Font = New System.Drawing.Font("DINMittelschrift LT", 14.0!)
        Me.Label11.ForeColor = System.Drawing.Color.DarkOrange
        Me.Label11.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.Label11.Location = New System.Drawing.Point(25, 9)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(177, 23)
        Me.Label11.TabIndex = 135
        Me.Label11.Text = "Créditos Disponibles"
        Me.Label11.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'DGV_Creditos
        '
        Me.DGV_Creditos.AllowUserToAddRows = False
        Me.DGV_Creditos.AllowUserToDeleteRows = False
        Me.DGV_Creditos.AllowUserToResizeRows = False
        Me.DGV_Creditos.BackgroundColor = System.Drawing.Color.Black
        Me.DGV_Creditos.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.DGV_Creditos.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.None
        Me.DGV_Creditos.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DGV_Creditos.ColumnHeadersVisible = False
        Me.DGV_Creditos.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.ID, Me.Descripcion, Me.Cantidad})
        Me.DGV_Creditos.Location = New System.Drawing.Point(0, 42)
        Me.DGV_Creditos.Name = "DGV_Creditos"
        Me.DGV_Creditos.RowHeadersVisible = False
        Me.DGV_Creditos.RowTemplate.DefaultCellStyle.BackColor = System.Drawing.Color.Black
        Me.DGV_Creditos.RowTemplate.DefaultCellStyle.Font = New System.Drawing.Font("DINMittelschrift LT", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.DGV_Creditos.RowTemplate.DefaultCellStyle.ForeColor = System.Drawing.Color.White
        Me.DGV_Creditos.RowTemplate.DefaultCellStyle.SelectionBackColor = System.Drawing.Color.Black
        Me.DGV_Creditos.RowTemplate.DefaultCellStyle.SelectionForeColor = System.Drawing.Color.Lime
        Me.DGV_Creditos.RowTemplate.ReadOnly = True
        Me.DGV_Creditos.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.DGV_Creditos.ShowEditingIcon = False
        Me.DGV_Creditos.Size = New System.Drawing.Size(228, 201)
        Me.DGV_Creditos.TabIndex = 148
        '
        'ID
        '
        Me.ID.DataPropertyName = "ID"
        Me.ID.HeaderText = "ID"
        Me.ID.Name = "ID"
        Me.ID.Visible = False
        '
        'Descripcion
        '
        Me.Descripcion.DataPropertyName = "Descripcion"
        Me.Descripcion.HeaderText = "Descripcion"
        Me.Descripcion.Name = "Descripcion"
        Me.Descripcion.Width = 170
        '
        'Cantidad
        '
        Me.Cantidad.DataPropertyName = "Cantidad"
        DataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        DataGridViewCellStyle1.BackColor = System.Drawing.Color.Black
        DataGridViewCellStyle1.Font = New System.Drawing.Font("DINMittelschrift LT", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle1.ForeColor = System.Drawing.Color.Lime
        DataGridViewCellStyle1.Format = "N0"
        DataGridViewCellStyle1.NullValue = Nothing
        DataGridViewCellStyle1.SelectionBackColor = System.Drawing.Color.Black
        DataGridViewCellStyle1.SelectionForeColor = System.Drawing.Color.Lime
        Me.Cantidad.DefaultCellStyle = DataGridViewCellStyle1
        Me.Cantidad.HeaderText = "Cantidad"
        Me.Cantidad.Name = "Cantidad"
        Me.Cantidad.Width = 50
        '
        'Creditos
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(227, 241)
        Me.Controls.Add(Me.DGV_Creditos)
        Me.Controls.Add(Me.PNL_Creditos)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow
        Me.Name = "Creditos"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "Creditos"
        Me.TopMost = True
        Me.PNL_Creditos.ResumeLayout(False)
        Me.PNL_Creditos.PerformLayout()
        CType(Me.DGV_Creditos, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents PNL_Creditos As System.Windows.Forms.Panel
    Friend WithEvents LBL_QtySPACIAHD As System.Windows.Forms.Label
    Friend WithEvents LBL_QtySPACIA As System.Windows.Forms.Label
    Friend WithEvents LBL_QtyTRIN8_12HD As System.Windows.Forms.Label
    Friend WithEvents LBL_QtyTRIN8_12 As System.Windows.Forms.Label
    Friend WithEvents LBL_QtyHDRX As System.Windows.Forms.Label
    Friend WithEvents Label19 As System.Windows.Forms.Label
    Friend WithEvents Label17 As System.Windows.Forms.Label
    Friend WithEvents Label16 As System.Windows.Forms.Label
    Friend WithEvents Label13 As System.Windows.Forms.Label
    Friend WithEvents Label12 As System.Windows.Forms.Label
    Friend WithEvents Label11 As System.Windows.Forms.Label
    Friend WithEvents DGV_Creditos As System.Windows.Forms.DataGridView
    Friend WithEvents ID As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Descripcion As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Cantidad As System.Windows.Forms.DataGridViewTextBoxColumn
End Class
