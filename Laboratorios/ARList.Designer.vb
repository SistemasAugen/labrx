<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class ARList
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
        Dim DataGridViewCellStyle1 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
        Dim DataGridViewCellStyle2 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
        Dim DataGridViewCellStyle3 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
        Dim DataGridViewCellStyle4 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
        Me.GridARList = New System.Windows.Forms.DataGridView
        Me.Ordernum = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.Laboratorio = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.Material = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.Diseno = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.Rx = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.TextSearch = New System.Windows.Forms.TextBox
        CType(Me.GridARList, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'GridARList
        '
        Me.GridARList.AllowUserToAddRows = False
        Me.GridARList.AllowUserToDeleteRows = False
        Me.GridARList.AllowUserToOrderColumns = True
        Me.GridARList.AllowUserToResizeRows = False
        Me.GridARList.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.GridARList.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells
        Me.GridARList.BackgroundColor = System.Drawing.Color.Black
        DataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle1.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.GridARList.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle1
        Me.GridARList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.GridARList.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.Ordernum, Me.Laboratorio, Me.Material, Me.Diseno, Me.Rx})
        DataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window
        DataGridViewCellStyle2.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText
        DataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
        Me.GridARList.DefaultCellStyle = DataGridViewCellStyle2
        Me.GridARList.Location = New System.Drawing.Point(0, 44)
        Me.GridARList.MultiSelect = False
        Me.GridARList.Name = "GridARList"
        Me.GridARList.ReadOnly = True
        DataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle3.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.GridARList.RowHeadersDefaultCellStyle = DataGridViewCellStyle3
        Me.GridARList.RowHeadersVisible = False
        DataGridViewCellStyle4.ForeColor = System.Drawing.Color.Black
        DataGridViewCellStyle4.SelectionBackColor = System.Drawing.Color.Black
        DataGridViewCellStyle4.SelectionForeColor = System.Drawing.Color.White
        Me.GridARList.RowsDefaultCellStyle = DataGridViewCellStyle4
        Me.GridARList.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.GridARList.Size = New System.Drawing.Size(341, 148)
        Me.GridARList.TabIndex = 0
        '
        'Ordernum
        '
        Me.Ordernum.DataPropertyName = "ordernum"
        Me.Ordernum.HeaderText = "Vantage"
        Me.Ordernum.Name = "Ordernum"
        Me.Ordernum.ReadOnly = True
        Me.Ordernum.Width = 72
        '
        'Laboratorio
        '
        Me.Laboratorio.DataPropertyName = "laboratorio"
        Me.Laboratorio.HeaderText = "Laboratorio"
        Me.Laboratorio.Name = "Laboratorio"
        Me.Laboratorio.ReadOnly = True
        Me.Laboratorio.Width = 85
        '
        'Material
        '
        Me.Material.DataPropertyName = "material"
        Me.Material.HeaderText = "Material"
        Me.Material.Name = "Material"
        Me.Material.ReadOnly = True
        Me.Material.Width = 69
        '
        'Diseno
        '
        Me.Diseno.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells
        Me.Diseno.DataPropertyName = "diseno"
        Me.Diseno.HeaderText = "Diseño"
        Me.Diseno.Name = "Diseno"
        Me.Diseno.ReadOnly = True
        Me.Diseno.Width = 65
        '
        'Rx
        '
        Me.Rx.DataPropertyName = "Rx"
        Me.Rx.HeaderText = "Rx"
        Me.Rx.Name = "Rx"
        Me.Rx.ReadOnly = True
        Me.Rx.Width = 45
        '
        'TextSearch
        '
        Me.TextSearch.AcceptsReturn = True
        Me.TextSearch.Font = New System.Drawing.Font("DINMittelschrift LT Alternate", 13.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TextSearch.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.TextSearch.Location = New System.Drawing.Point(57, 10)
        Me.TextSearch.MaxLength = 11
        Me.TextSearch.Name = "TextSearch"
        Me.TextSearch.Size = New System.Drawing.Size(229, 28)
        Me.TextSearch.TabIndex = 131
        Me.TextSearch.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        Me.TextSearch.WordWrap = False
        '
        'ARList
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackgroundImage = Global.Laboratorios.My.Resources.Resources.VistaBackground
        Me.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.ClientSize = New System.Drawing.Size(339, 192)
        Me.ControlBox = False
        Me.Controls.Add(Me.TextSearch)
        Me.Controls.Add(Me.GridARList)
        Me.DoubleBuffered = True
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "ARList"
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.Manual
        Me.Text = "Rx Virtuales"
        Me.TopMost = True
        CType(Me.GridARList, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Public WithEvents GridARList As System.Windows.Forms.DataGridView
    Friend WithEvents Ordernum As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Laboratorio As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Material As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Diseno As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Rx As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents TextSearch As System.Windows.Forms.TextBox
End Class
