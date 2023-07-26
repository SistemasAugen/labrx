<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class ShowOrders
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
        Me.GridOrders = New System.Windows.Forms.DataGridView
        Me.ordernum = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.OK = New System.Windows.Forms.Button
        CType(Me.GridOrders, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'GridOrders
        '
        Me.GridOrders.AllowUserToAddRows = False
        Me.GridOrders.AllowUserToDeleteRows = False
        Me.GridOrders.AllowUserToOrderColumns = True
        Me.GridOrders.AllowUserToResizeRows = False
        Me.GridOrders.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells
        Me.GridOrders.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.GridOrders.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.ordernum})
        Me.GridOrders.Location = New System.Drawing.Point(12, 12)
        Me.GridOrders.MultiSelect = False
        Me.GridOrders.Name = "GridOrders"
        Me.GridOrders.ReadOnly = True
        Me.GridOrders.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.AutoSizeToAllHeaders
        Me.GridOrders.RowTemplate.ReadOnly = True
        Me.GridOrders.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.GridOrders.Size = New System.Drawing.Size(579, 169)
        Me.GridOrders.TabIndex = 0
        '
        'ordernum
        '
        Me.ordernum.DataPropertyName = "ordernum"
        Me.ordernum.HeaderText = "No Vantage"
        Me.ordernum.Name = "ordernum"
        Me.ordernum.ReadOnly = True
        Me.ordernum.Width = 89
        '
        'OK
        '
        Me.OK.Location = New System.Drawing.Point(496, 187)
        Me.OK.Name = "OK"
        Me.OK.Size = New System.Drawing.Size(95, 23)
        Me.OK.TabIndex = 1
        Me.OK.Text = "Aceptar"
        Me.OK.UseVisualStyleBackColor = True
        '
        'ShowOrders
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(603, 220)
        Me.Controls.Add(Me.OK)
        Me.Controls.Add(Me.GridOrders)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "ShowOrders"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "Selecciona la Rx Deseada"
        CType(Me.GridOrders, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Public WithEvents GridOrders As System.Windows.Forms.DataGridView
    Friend WithEvents OK As System.Windows.Forms.Button
    Friend WithEvents ordernum As System.Windows.Forms.DataGridViewTextBoxColumn
End Class
