<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Clientes_Saldo
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Clientes_Saldo))
        Dim DataGridViewCellStyle1 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
        Dim DataGridViewCellStyle2 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
        Dim DataGridViewCellStyle3 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
        Me.DGV_Saldos = New System.Windows.Forms.DataGridView
        Me.VwSaldoClientesBindingSource = New System.Windows.Forms.BindingSource(Me.components)
        Me.DataSetclientessaldoBindingSource = New System.Windows.Forms.BindingSource(Me.components)
        Me.DataSet_clientes_saldo = New DataSet_clientes_saldo
        Me.PanelGrid = New System.Windows.Forms.Panel
        Me.BuscaClienteToolStrip = New System.Windows.Forms.ToolStrip
        Me.CustTipToolStripLabel = New System.Windows.Forms.ToolStripLabel
        Me.CustTipToolStripTextBox = New System.Windows.Forms.ToolStripTextBox
        Me.BuscaClienteToolStripButton = New System.Windows.Forms.ToolStripButton
        Me.toolStripSeparator = New System.Windows.Forms.ToolStripSeparator
        Me.tsbtn_Conslidar = New System.Windows.Forms.ToolStripButton
        Me.Clientes_Saldo_TableAdapter = New DataSet_clientes_saldoTableAdapters.VwSaldoClientesTableAdapter
        Me.LabDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.ClaveDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.NameDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.ReferenciaDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.RxDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.SaldoDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.FechaDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.DiasRetrasoDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.FacturaDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn
        CType(Me.DGV_Saldos, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.VwSaldoClientesBindingSource, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.DataSetclientessaldoBindingSource, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.DataSet_clientes_saldo, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.PanelGrid.SuspendLayout()
        Me.BuscaClienteToolStrip.SuspendLayout()
        Me.SuspendLayout()
        '
        'DGV_Saldos
        '
        Me.DGV_Saldos.AllowUserToAddRows = False
        Me.DGV_Saldos.AllowUserToDeleteRows = False
        Me.DGV_Saldos.AllowUserToOrderColumns = True
        Me.DGV_Saldos.AutoGenerateColumns = False
        Me.DGV_Saldos.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DGV_Saldos.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.LabDataGridViewTextBoxColumn, Me.ClaveDataGridViewTextBoxColumn, Me.NameDataGridViewTextBoxColumn, Me.ReferenciaDataGridViewTextBoxColumn, Me.RxDataGridViewTextBoxColumn, Me.SaldoDataGridViewTextBoxColumn, Me.FechaDataGridViewTextBoxColumn, Me.DiasRetrasoDataGridViewTextBoxColumn, Me.FacturaDataGridViewTextBoxColumn})
        Me.DGV_Saldos.DataSource = Me.VwSaldoClientesBindingSource
        Me.DGV_Saldos.Dock = System.Windows.Forms.DockStyle.Fill
        Me.DGV_Saldos.Location = New System.Drawing.Point(0, 0)
        Me.DGV_Saldos.Name = "DGV_Saldos"
        Me.DGV_Saldos.ReadOnly = True
        Me.DGV_Saldos.Size = New System.Drawing.Size(855, 408)
        Me.DGV_Saldos.TabIndex = 1
        '
        'VwSaldoClientesBindingSource
        '
        Me.VwSaldoClientesBindingSource.DataMember = "VwSaldoClientes"
        Me.VwSaldoClientesBindingSource.DataSource = Me.DataSetclientessaldoBindingSource
        '
        'DataSetclientessaldoBindingSource
        '
        Me.DataSetclientessaldoBindingSource.DataSource = Me.DataSet_clientes_saldo
        Me.DataSetclientessaldoBindingSource.Position = 0
        '
        'DataSet_clientes_saldo
        '
        Me.DataSet_clientes_saldo.DataSetName = "DataSet_clientes_saldo"
        Me.DataSet_clientes_saldo.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema
        '
        'PanelGrid
        '
        Me.PanelGrid.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.PanelGrid.Controls.Add(Me.DGV_Saldos)
        Me.PanelGrid.Location = New System.Drawing.Point(5, 41)
        Me.PanelGrid.Name = "PanelGrid"
        Me.PanelGrid.Size = New System.Drawing.Size(855, 408)
        Me.PanelGrid.TabIndex = 2
        '
        'BuscaClienteToolStrip
        '
        Me.BuscaClienteToolStrip.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.CustTipToolStripLabel, Me.CustTipToolStripTextBox, Me.BuscaClienteToolStripButton, Me.toolStripSeparator, Me.tsbtn_Conslidar})
        Me.BuscaClienteToolStrip.Location = New System.Drawing.Point(0, 0)
        Me.BuscaClienteToolStrip.Name = "BuscaClienteToolStrip"
        Me.BuscaClienteToolStrip.Size = New System.Drawing.Size(864, 25)
        Me.BuscaClienteToolStrip.TabIndex = 3
        Me.BuscaClienteToolStrip.Text = "BuscaClienteToolStrip"
        '
        'CustTipToolStripLabel
        '
        Me.CustTipToolStripLabel.Name = "CustTipToolStripLabel"
        Me.CustTipToolStripLabel.Size = New System.Drawing.Size(78, 22)
        Me.CustTipToolStripLabel.Text = "Nombre o ID:"
        '
        'CustTipToolStripTextBox
        '
        Me.CustTipToolStripTextBox.Name = "CustTipToolStripTextBox"
        Me.CustTipToolStripTextBox.Size = New System.Drawing.Size(200, 25)
        '
        'BuscaClienteToolStripButton
        '
        Me.BuscaClienteToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text
        Me.BuscaClienteToolStripButton.Name = "BuscaClienteToolStripButton"
        Me.BuscaClienteToolStripButton.Size = New System.Drawing.Size(46, 22)
        Me.BuscaClienteToolStripButton.Text = "Buscar"
        '
        'toolStripSeparator
        '
        Me.toolStripSeparator.Name = "toolStripSeparator"
        Me.toolStripSeparator.Size = New System.Drawing.Size(6, 25)
        '
        'tsbtn_Conslidar
        '
        Me.tsbtn_Conslidar.BackColor = System.Drawing.SystemColors.Info
        Me.tsbtn_Conslidar.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text
        Me.tsbtn_Conslidar.Image = CType(resources.GetObject("tsbtn_Conslidar.Image"), System.Drawing.Image)
        Me.tsbtn_Conslidar.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tsbtn_Conslidar.Name = "tsbtn_Conslidar"
        Me.tsbtn_Conslidar.Size = New System.Drawing.Size(105, 22)
        Me.tsbtn_Conslidar.Text = "Consolidar Saldos"
        '
        'Clientes_Saldo_TableAdapter
        '
        Me.Clientes_Saldo_TableAdapter.ClearBeforeFill = True
        '
        'LabDataGridViewTextBoxColumn
        '
        Me.LabDataGridViewTextBoxColumn.DataPropertyName = "Lab"
        Me.LabDataGridViewTextBoxColumn.FillWeight = 50.0!
        Me.LabDataGridViewTextBoxColumn.HeaderText = "Lab"
        Me.LabDataGridViewTextBoxColumn.Name = "LabDataGridViewTextBoxColumn"
        Me.LabDataGridViewTextBoxColumn.ReadOnly = True
        Me.LabDataGridViewTextBoxColumn.Width = 50
        '
        'ClaveDataGridViewTextBoxColumn
        '
        Me.ClaveDataGridViewTextBoxColumn.DataPropertyName = "Clave"
        Me.ClaveDataGridViewTextBoxColumn.FillWeight = 50.0!
        Me.ClaveDataGridViewTextBoxColumn.HeaderText = "Clave"
        Me.ClaveDataGridViewTextBoxColumn.Name = "ClaveDataGridViewTextBoxColumn"
        Me.ClaveDataGridViewTextBoxColumn.ReadOnly = True
        Me.ClaveDataGridViewTextBoxColumn.Width = 60
        '
        'NameDataGridViewTextBoxColumn
        '
        Me.NameDataGridViewTextBoxColumn.DataPropertyName = "name"
        Me.NameDataGridViewTextBoxColumn.HeaderText = "Cliente"
        Me.NameDataGridViewTextBoxColumn.Name = "NameDataGridViewTextBoxColumn"
        Me.NameDataGridViewTextBoxColumn.ReadOnly = True
        Me.NameDataGridViewTextBoxColumn.ToolTipText = "Nombre del cliente"
        Me.NameDataGridViewTextBoxColumn.Width = 200
        '
        'ReferenciaDataGridViewTextBoxColumn
        '
        Me.ReferenciaDataGridViewTextBoxColumn.DataPropertyName = "Referencia"
        Me.ReferenciaDataGridViewTextBoxColumn.HeaderText = "Referencia"
        Me.ReferenciaDataGridViewTextBoxColumn.Name = "ReferenciaDataGridViewTextBoxColumn"
        Me.ReferenciaDataGridViewTextBoxColumn.ReadOnly = True
        '
        'RxDataGridViewTextBoxColumn
        '
        Me.RxDataGridViewTextBoxColumn.DataPropertyName = "Rx"
        Me.RxDataGridViewTextBoxColumn.HeaderText = "Rx"
        Me.RxDataGridViewTextBoxColumn.Name = "RxDataGridViewTextBoxColumn"
        Me.RxDataGridViewTextBoxColumn.ReadOnly = True
        Me.RxDataGridViewTextBoxColumn.Width = 60
        '
        'SaldoDataGridViewTextBoxColumn
        '
        Me.SaldoDataGridViewTextBoxColumn.DataPropertyName = "saldo"
        DataGridViewCellStyle1.Format = "C2"
        DataGridViewCellStyle1.NullValue = Nothing
        Me.SaldoDataGridViewTextBoxColumn.DefaultCellStyle = DataGridViewCellStyle1
        Me.SaldoDataGridViewTextBoxColumn.HeaderText = "saldo"
        Me.SaldoDataGridViewTextBoxColumn.Name = "SaldoDataGridViewTextBoxColumn"
        Me.SaldoDataGridViewTextBoxColumn.ReadOnly = True
        Me.SaldoDataGridViewTextBoxColumn.Width = 80
        '
        'FechaDataGridViewTextBoxColumn
        '
        Me.FechaDataGridViewTextBoxColumn.DataPropertyName = "Fecha"
        DataGridViewCellStyle2.Format = "d"
        DataGridViewCellStyle2.NullValue = Nothing
        Me.FechaDataGridViewTextBoxColumn.DefaultCellStyle = DataGridViewCellStyle2
        Me.FechaDataGridViewTextBoxColumn.HeaderText = "Fecha"
        Me.FechaDataGridViewTextBoxColumn.Name = "FechaDataGridViewTextBoxColumn"
        Me.FechaDataGridViewTextBoxColumn.ReadOnly = True
        '
        'DiasRetrasoDataGridViewTextBoxColumn
        '
        Me.DiasRetrasoDataGridViewTextBoxColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.ColumnHeader
        Me.DiasRetrasoDataGridViewTextBoxColumn.DataPropertyName = "DiasRetraso"
        Me.DiasRetrasoDataGridViewTextBoxColumn.HeaderText = "Dias"
        Me.DiasRetrasoDataGridViewTextBoxColumn.Name = "DiasRetrasoDataGridViewTextBoxColumn"
        Me.DiasRetrasoDataGridViewTextBoxColumn.ReadOnly = True
        Me.DiasRetrasoDataGridViewTextBoxColumn.Width = 53
        '
        'FacturaDataGridViewTextBoxColumn
        '
        Me.FacturaDataGridViewTextBoxColumn.DataPropertyName = "Factura"
        DataGridViewCellStyle3.Format = "####0000"
        DataGridViewCellStyle3.NullValue = "N/A"
        Me.FacturaDataGridViewTextBoxColumn.DefaultCellStyle = DataGridViewCellStyle3
        Me.FacturaDataGridViewTextBoxColumn.HeaderText = "Factura"
        Me.FacturaDataGridViewTextBoxColumn.MaxInputLength = 12
        Me.FacturaDataGridViewTextBoxColumn.Name = "FacturaDataGridViewTextBoxColumn"
        Me.FacturaDataGridViewTextBoxColumn.ReadOnly = True
        '
        'Clientes_Saldo
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(864, 473)
        Me.Controls.Add(Me.BuscaClienteToolStrip)
        Me.Controls.Add(Me.PanelGrid)
        Me.MaximumSize = New System.Drawing.Size(880, 1024)
        Me.MinimumSize = New System.Drawing.Size(400, 300)
        Me.Name = "Clientes_Saldo"
        Me.Text = "Saldos por cliente"
        CType(Me.DGV_Saldos, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.VwSaldoClientesBindingSource, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.DataSetclientessaldoBindingSource, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.DataSet_clientes_saldo, System.ComponentModel.ISupportInitialize).EndInit()
        Me.PanelGrid.ResumeLayout(False)
        Me.BuscaClienteToolStrip.ResumeLayout(False)
        Me.BuscaClienteToolStrip.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents DGV_Saldos As System.Windows.Forms.DataGridView
    Friend WithEvents DataSet_clientes_saldo As DataSet_clientes_saldo
    Friend WithEvents PanelGrid As System.Windows.Forms.Panel
    Friend WithEvents BuscaClienteToolStrip As System.Windows.Forms.ToolStrip
    Friend WithEvents CustTipToolStripLabel As System.Windows.Forms.ToolStripLabel
    Friend WithEvents CustTipToolStripTextBox As System.Windows.Forms.ToolStripTextBox
    Friend WithEvents BuscaClienteToolStripButton As System.Windows.Forms.ToolStripButton
    Friend WithEvents toolStripSeparator As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents tsbtn_Conslidar As System.Windows.Forms.ToolStripButton
    Friend WithEvents Clientes_Saldo_TableAdapter As DataSet_clientes_saldoTableAdapters.VwSaldoClientesTableAdapter
    Friend WithEvents VwSaldoClientesBindingSource As System.Windows.Forms.BindingSource
    Friend WithEvents DataSetclientessaldoBindingSource As System.Windows.Forms.BindingSource
    Friend WithEvents LabDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents ClaveDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents NameDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents ReferenciaDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents RxDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents SaldoDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents FechaDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DiasRetrasoDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents FacturaDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
End Class
