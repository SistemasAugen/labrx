<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class AperturaRx
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
        Dim ListViewItem3 As System.Windows.Forms.ListViewItem = New System.Windows.Forms.ListViewItem("Dato para la lista")
        Dim ListViewItem4 As System.Windows.Forms.ListViewItem = New System.Windows.Forms.ListViewItem("Otro dato para la lista")
        Me.Label133 = New System.Windows.Forms.Label
        Me.cl_CheckEnvio = New System.Windows.Forms.CheckBox
        Me.GroupBox19 = New System.Windows.Forms.GroupBox
        Me.Label106 = New System.Windows.Forms.Label
        Me.lblIVA = New System.Windows.Forms.Label
        Me.Label108 = New System.Windows.Forms.Label
        Me.cl_txtTotal2 = New System.Windows.Forms.TextBox
        Me.cl_txtIVA2 = New System.Windows.Forms.TextBox
        Me.cl_txtSubTot2 = New System.Windows.Forms.TextBox
        Me.cl_txtImporteLetra = New System.Windows.Forms.TextBox
        Me.Label109 = New System.Windows.Forms.Label
        Me.Panel3 = New System.Windows.Forms.Panel
        Me.TreeOrders = New System.Windows.Forms.TreeView
        Me.Label15 = New System.Windows.Forms.Label
        Me.LViewRX = New System.Windows.Forms.ListView
        Me.Label22 = New System.Windows.Forms.Label
        Me.BtnExpand = New System.Windows.Forms.Button
        Me.BtnCollapse = New System.Windows.Forms.Button
        Me.ButtonGetClosedRx = New System.Windows.Forms.Button
        Me.Label59 = New System.Windows.Forms.Label
        Me.TextVantage = New System.Windows.Forms.TextBox
        Me.LabelIsGarantia = New System.Windows.Forms.Label
        Me.LabelIsGratis = New System.Windows.Forms.Label
        Me.CloseRxNew = New RoundButtonSmall.UserControl1
        Me.Label47 = New System.Windows.Forms.Label
        Me.TextPO = New System.Windows.Forms.TextBox
        Me.Label46 = New System.Windows.Forms.Label
        Me.TextCustName = New System.Windows.Forms.TextBox
        Me.Label45 = New System.Windows.Forms.Label
        Me.TextRX = New System.Windows.Forms.TextBox
        Me.GroupBox6 = New System.Windows.Forms.GroupBox
        Me.CBX_Cortesia = New System.Windows.Forms.CheckBox
        Me.CheckGarantia2 = New System.Windows.Forms.CheckBox
        Me.AgregarNew = New RoundButtonSmall.UserControl1
        Me.ComboMiscDesc = New System.Windows.Forms.ComboBox
        Me.Label25 = New System.Windows.Forms.Label
        Me.txtMiscAmount = New System.Windows.Forms.TextBox
        Me.Button2 = New System.Windows.Forms.Button
        Me.Label24 = New System.Windows.Forms.Label
        Me.txtMiscDesc = New System.Windows.Forms.TextBox
        Me.LblRxTotal = New System.Windows.Forms.Label
        Me.Label23 = New System.Windows.Forms.Label
        Me.Label20 = New System.Windows.Forms.Label
        Me.LViewLines = New System.Windows.Forms.ListView
        Me.btnMiscCharge = New System.Windows.Forms.Button
        Me.lvListasPrecios = New System.Windows.Forms.ListView
        Me.lista1 = New System.Windows.Forms.ColumnHeader
        Me.GroupBox19.SuspendLayout()
        Me.Panel3.SuspendLayout()
        Me.GroupBox6.SuspendLayout()
        Me.SuspendLayout()
        '
        'Label133
        '
        Me.Label133.AutoSize = True
        Me.Label133.BackColor = System.Drawing.Color.Transparent
        Me.Label133.Font = New System.Drawing.Font("Verdana", 9.5!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label133.Location = New System.Drawing.Point(64, 447)
        Me.Label133.Name = "Label133"
        Me.Label133.Size = New System.Drawing.Size(136, 16)
        Me.Label133.TabIndex = 231
        Me.Label133.Text = "Cargo Misceláneo"
        '
        'cl_CheckEnvio
        '
        Me.cl_CheckEnvio.AutoSize = True
        Me.cl_CheckEnvio.BackColor = System.Drawing.Color.Transparent
        Me.cl_CheckEnvio.Enabled = False
        Me.cl_CheckEnvio.Font = New System.Drawing.Font("Verdana", 9.5!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cl_CheckEnvio.Location = New System.Drawing.Point(23, 608)
        Me.cl_CheckEnvio.Name = "cl_CheckEnvio"
        Me.cl_CheckEnvio.Size = New System.Drawing.Size(206, 20)
        Me.cl_CheckEnvio.TabIndex = 228
        Me.cl_CheckEnvio.Text = "Agregar Cargos de Envío"
        Me.cl_CheckEnvio.UseVisualStyleBackColor = False
        Me.cl_CheckEnvio.Visible = False
        '
        'GroupBox19
        '
        Me.GroupBox19.BackColor = System.Drawing.Color.Transparent
        Me.GroupBox19.Controls.Add(Me.Label106)
        Me.GroupBox19.Controls.Add(Me.lblIVA)
        Me.GroupBox19.Controls.Add(Me.Label108)
        Me.GroupBox19.Controls.Add(Me.cl_txtTotal2)
        Me.GroupBox19.Controls.Add(Me.cl_txtIVA2)
        Me.GroupBox19.Controls.Add(Me.cl_txtSubTot2)
        Me.GroupBox19.Controls.Add(Me.cl_txtImporteLetra)
        Me.GroupBox19.Controls.Add(Me.Label109)
        Me.GroupBox19.Font = New System.Drawing.Font("Verdana", 9.5!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GroupBox19.ForeColor = System.Drawing.Color.White
        Me.GroupBox19.Location = New System.Drawing.Point(20, 476)
        Me.GroupBox19.Name = "GroupBox19"
        Me.GroupBox19.Size = New System.Drawing.Size(543, 115)
        Me.GroupBox19.TabIndex = 226
        Me.GroupBox19.TabStop = False
        Me.GroupBox19.Text = "Resumen"
        '
        'Label106
        '
        Me.Label106.AutoSize = True
        Me.Label106.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label106.Location = New System.Drawing.Point(12, 86)
        Me.Label106.Name = "Label106"
        Me.Label106.Size = New System.Drawing.Size(48, 13)
        Me.Label106.TabIndex = 27
        Me.Label106.Text = "TOTAL"
        Me.Label106.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblIVA
        '
        Me.lblIVA.AutoSize = True
        Me.lblIVA.Location = New System.Drawing.Point(12, 60)
        Me.lblIVA.Name = "lblIVA"
        Me.lblIVA.Size = New System.Drawing.Size(49, 16)
        Me.lblIVA.TabIndex = 26
        Me.lblIVA.Text = "I.V.A."
        '
        'Label108
        '
        Me.Label108.AutoSize = True
        Me.Label108.Location = New System.Drawing.Point(12, 34)
        Me.Label108.Name = "Label108"
        Me.Label108.Size = New System.Drawing.Size(71, 16)
        Me.Label108.TabIndex = 25
        Me.Label108.Text = "SubTotal"
        '
        'cl_txtTotal2
        '
        Me.cl_txtTotal2.BackColor = System.Drawing.Color.White
        Me.cl_txtTotal2.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cl_txtTotal2.ForeColor = System.Drawing.Color.Black
        Me.cl_txtTotal2.Location = New System.Drawing.Point(156, 83)
        Me.cl_txtTotal2.Name = "cl_txtTotal2"
        Me.cl_txtTotal2.ReadOnly = True
        Me.cl_txtTotal2.Size = New System.Drawing.Size(100, 22)
        Me.cl_txtTotal2.TabIndex = 24
        Me.cl_txtTotal2.Text = "test"
        Me.cl_txtTotal2.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'cl_txtIVA2
        '
        Me.cl_txtIVA2.BackColor = System.Drawing.Color.White
        Me.cl_txtIVA2.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cl_txtIVA2.ForeColor = System.Drawing.Color.Black
        Me.cl_txtIVA2.Location = New System.Drawing.Point(156, 57)
        Me.cl_txtIVA2.Name = "cl_txtIVA2"
        Me.cl_txtIVA2.ReadOnly = True
        Me.cl_txtIVA2.Size = New System.Drawing.Size(100, 22)
        Me.cl_txtIVA2.TabIndex = 23
        Me.cl_txtIVA2.Text = "test"
        Me.cl_txtIVA2.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'cl_txtSubTot2
        '
        Me.cl_txtSubTot2.BackColor = System.Drawing.Color.White
        Me.cl_txtSubTot2.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cl_txtSubTot2.ForeColor = System.Drawing.Color.Black
        Me.cl_txtSubTot2.Location = New System.Drawing.Point(156, 31)
        Me.cl_txtSubTot2.Name = "cl_txtSubTot2"
        Me.cl_txtSubTot2.ReadOnly = True
        Me.cl_txtSubTot2.Size = New System.Drawing.Size(100, 22)
        Me.cl_txtSubTot2.TabIndex = 22
        Me.cl_txtSubTot2.Text = "test"
        Me.cl_txtSubTot2.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'cl_txtImporteLetra
        '
        Me.cl_txtImporteLetra.BackColor = System.Drawing.Color.White
        Me.cl_txtImporteLetra.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cl_txtImporteLetra.ForeColor = System.Drawing.Color.Black
        Me.cl_txtImporteLetra.Location = New System.Drawing.Point(265, 31)
        Me.cl_txtImporteLetra.Multiline = True
        Me.cl_txtImporteLetra.Name = "cl_txtImporteLetra"
        Me.cl_txtImporteLetra.ReadOnly = True
        Me.cl_txtImporteLetra.Size = New System.Drawing.Size(268, 74)
        Me.cl_txtImporteLetra.TabIndex = 26
        Me.cl_txtImporteLetra.Text = "test"
        '
        'Label109
        '
        Me.Label109.AutoSize = True
        Me.Label109.Location = New System.Drawing.Point(262, 12)
        Me.Label109.Name = "Label109"
        Me.Label109.Size = New System.Drawing.Size(179, 16)
        Me.Label109.TabIndex = 27
        Me.Label109.Text = "Importe Total con Letra"
        '
        'Panel3
        '
        Me.Panel3.Controls.Add(Me.TreeOrders)
        Me.Panel3.Controls.Add(Me.Label15)
        Me.Panel3.Controls.Add(Me.LViewRX)
        Me.Panel3.Controls.Add(Me.Label22)
        Me.Panel3.Controls.Add(Me.BtnExpand)
        Me.Panel3.Controls.Add(Me.BtnCollapse)
        Me.Panel3.Location = New System.Drawing.Point(582, 534)
        Me.Panel3.Name = "Panel3"
        Me.Panel3.Size = New System.Drawing.Size(114, 48)
        Me.Panel3.TabIndex = 225
        Me.Panel3.Visible = False
        '
        'TreeOrders
        '
        Me.TreeOrders.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.5!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TreeOrders.ImageKey = "Dr2.png"
        Me.TreeOrders.Location = New System.Drawing.Point(15, 33)
        Me.TreeOrders.Name = "TreeOrders"
        Me.TreeOrders.Size = New System.Drawing.Size(130, 118)
        Me.TreeOrders.TabIndex = 29
        '
        'Label15
        '
        Me.Label15.AutoSize = True
        Me.Label15.BackColor = System.Drawing.Color.Transparent
        Me.Label15.Font = New System.Drawing.Font("Verdana", 9.5!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label15.ForeColor = System.Drawing.Color.White
        Me.Label15.Location = New System.Drawing.Point(12, 160)
        Me.Label15.Name = "Label15"
        Me.Label15.Size = New System.Drawing.Size(141, 16)
        Me.Label15.TabIndex = 9
        Me.Label15.Text = "Trabajos Cerrados"
        '
        'LViewRX
        '
        Me.LViewRX.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.5!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LViewRX.FullRowSelect = True
        Me.LViewRX.GridLines = True
        Me.LViewRX.Location = New System.Drawing.Point(15, 179)
        Me.LViewRX.MultiSelect = False
        Me.LViewRX.Name = "LViewRX"
        Me.LViewRX.Size = New System.Drawing.Size(130, 136)
        Me.LViewRX.Sorting = System.Windows.Forms.SortOrder.Ascending
        Me.LViewRX.TabIndex = 12
        Me.LViewRX.UseCompatibleStateImageBehavior = False
        Me.LViewRX.View = System.Windows.Forms.View.Details
        '
        'Label22
        '
        Me.Label22.AutoSize = True
        Me.Label22.BackColor = System.Drawing.Color.Transparent
        Me.Label22.Font = New System.Drawing.Font("Verdana", 9.5!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label22.ForeColor = System.Drawing.Color.White
        Me.Label22.Location = New System.Drawing.Point(12, 14)
        Me.Label22.Name = "Label22"
        Me.Label22.Size = New System.Drawing.Size(67, 16)
        Me.Label22.TabIndex = 30
        Me.Label22.Text = "Clientes"
        '
        'BtnExpand
        '
        Me.BtnExpand.ForeColor = System.Drawing.SystemColors.ControlText
        Me.BtnExpand.Location = New System.Drawing.Point(29, 121)
        Me.BtnExpand.Name = "BtnExpand"
        Me.BtnExpand.Size = New System.Drawing.Size(91, 23)
        Me.BtnExpand.TabIndex = 33
        Me.BtnExpand.Text = "Expandir"
        Me.BtnExpand.UseVisualStyleBackColor = True
        Me.BtnExpand.Visible = False
        '
        'BtnCollapse
        '
        Me.BtnCollapse.ForeColor = System.Drawing.SystemColors.ControlText
        Me.BtnCollapse.Location = New System.Drawing.Point(54, 106)
        Me.BtnCollapse.Name = "BtnCollapse"
        Me.BtnCollapse.Size = New System.Drawing.Size(91, 23)
        Me.BtnCollapse.TabIndex = 34
        Me.BtnCollapse.Text = "Colapsar"
        Me.BtnCollapse.UseVisualStyleBackColor = True
        Me.BtnCollapse.Visible = False
        '
        'ButtonGetClosedRx
        '
        Me.ButtonGetClosedRx.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.5!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ButtonGetClosedRx.ForeColor = System.Drawing.Color.Black
        Me.ButtonGetClosedRx.Location = New System.Drawing.Point(582, 488)
        Me.ButtonGetClosedRx.Name = "ButtonGetClosedRx"
        Me.ButtonGetClosedRx.Size = New System.Drawing.Size(110, 31)
        Me.ButtonGetClosedRx.TabIndex = 47
        Me.ButtonGetClosedRx.Text = "Obtener Rx Cerradas"
        Me.ButtonGetClosedRx.UseVisualStyleBackColor = True
        Me.ButtonGetClosedRx.Visible = False
        '
        'Label59
        '
        Me.Label59.AutoSize = True
        Me.Label59.BackColor = System.Drawing.Color.Transparent
        Me.Label59.Font = New System.Drawing.Font("Verdana", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label59.ForeColor = System.Drawing.Color.White
        Me.Label59.Location = New System.Drawing.Point(306, 14)
        Me.Label59.Name = "Label59"
        Me.Label59.Size = New System.Drawing.Size(82, 18)
        Me.Label59.TabIndex = 224
        Me.Label59.Text = "Vantage"
        '
        'TextVantage
        '
        Me.TextVantage.BackColor = System.Drawing.SystemColors.Control
        Me.TextVantage.Font = New System.Drawing.Font("Microsoft Sans Serif", 17.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TextVantage.Location = New System.Drawing.Point(394, 7)
        Me.TextVantage.MaxLength = 9
        Me.TextVantage.Name = "TextVantage"
        Me.TextVantage.ReadOnly = True
        Me.TextVantage.Size = New System.Drawing.Size(169, 33)
        Me.TextVantage.TabIndex = 223
        Me.TextVantage.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'LabelIsGarantia
        '
        Me.LabelIsGarantia.AutoSize = True
        Me.LabelIsGarantia.BackColor = System.Drawing.Color.White
        Me.LabelIsGarantia.Font = New System.Drawing.Font("Arial", 18.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LabelIsGarantia.ForeColor = System.Drawing.Color.Red
        Me.LabelIsGarantia.Location = New System.Drawing.Point(197, 380)
        Me.LabelIsGarantia.Name = "LabelIsGarantia"
        Me.LabelIsGarantia.Size = New System.Drawing.Size(202, 29)
        Me.LabelIsGarantia.TabIndex = 222
        Me.LabelIsGarantia.Text = "Garantía AUGEN"
        Me.LabelIsGarantia.Visible = False
        '
        'LabelIsGratis
        '
        Me.LabelIsGratis.AutoSize = True
        Me.LabelIsGratis.BackColor = System.Drawing.Color.White
        Me.LabelIsGratis.Font = New System.Drawing.Font("Arial", 18.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LabelIsGratis.ForeColor = System.Drawing.Color.Red
        Me.LabelIsGratis.Location = New System.Drawing.Point(76, 380)
        Me.LabelIsGratis.Name = "LabelIsGratis"
        Me.LabelIsGratis.Size = New System.Drawing.Size(430, 29)
        Me.LabelIsGratis.TabIndex = 221
        Me.LabelIsGratis.Text = "Canje de Puntos Cliente Distinguido"
        Me.LabelIsGratis.Visible = False
        '
        'CloseRxNew
        '
        Me.CloseRxNew.BackColor = System.Drawing.Color.Transparent
        Me.CloseRxNew.DisabledForeColor = System.Drawing.Color.Gray
        Me.CloseRxNew.Enabled = False
        Me.CloseRxNew.Font = New System.Drawing.Font("Microsoft Sans Serif", 14.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel)
        Me.CloseRxNew.ForeColor = System.Drawing.Color.White
        Me.CloseRxNew.Highlighted = True
        Me.CloseRxNew.Location = New System.Drawing.Point(369, 600)
        Me.CloseRxNew.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.CloseRxNew.Name = "CloseRxNew"
        Me.CloseRxNew.Size = New System.Drawing.Size(194, 66)
        Me.CloseRxNew.TabIndex = 220
        Me.CloseRxNew.Texto = "Remisión"
        '
        'Label47
        '
        Me.Label47.AutoSize = True
        Me.Label47.BackColor = System.Drawing.Color.Transparent
        Me.Label47.Font = New System.Drawing.Font("Verdana", 9.5!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label47.Location = New System.Drawing.Point(20, 19)
        Me.Label47.Name = "Label47"
        Me.Label47.Size = New System.Drawing.Size(87, 16)
        Me.Label47.TabIndex = 219
        Me.Label47.Text = "Orden Lab."
        '
        'TextPO
        '
        Me.TextPO.BackColor = System.Drawing.SystemColors.Control
        Me.TextPO.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TextPO.Location = New System.Drawing.Point(113, 19)
        Me.TextPO.Name = "TextPO"
        Me.TextPO.ReadOnly = True
        Me.TextPO.Size = New System.Drawing.Size(114, 20)
        Me.TextPO.TabIndex = 218
        Me.TextPO.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'Label46
        '
        Me.Label46.AutoSize = True
        Me.Label46.BackColor = System.Drawing.Color.Transparent
        Me.Label46.Font = New System.Drawing.Font("Verdana", 9.5!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label46.Location = New System.Drawing.Point(193, 46)
        Me.Label46.Name = "Label46"
        Me.Label46.Size = New System.Drawing.Size(59, 16)
        Me.Label46.TabIndex = 217
        Me.Label46.Text = "Cliente"
        '
        'TextCustName
        '
        Me.TextCustName.BackColor = System.Drawing.SystemColors.Control
        Me.TextCustName.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TextCustName.Location = New System.Drawing.Point(257, 45)
        Me.TextCustName.Name = "TextCustName"
        Me.TextCustName.ReadOnly = True
        Me.TextCustName.Size = New System.Drawing.Size(306, 20)
        Me.TextCustName.TabIndex = 216
        Me.TextCustName.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'Label45
        '
        Me.Label45.AutoSize = True
        Me.Label45.BackColor = System.Drawing.Color.Transparent
        Me.Label45.Font = New System.Drawing.Font("Verdana", 9.5!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label45.ForeColor = System.Drawing.Color.White
        Me.Label45.Location = New System.Drawing.Point(20, 45)
        Me.Label45.Name = "Label45"
        Me.Label45.Size = New System.Drawing.Size(27, 16)
        Me.Label45.TabIndex = 215
        Me.Label45.Text = "RX"
        '
        'TextRX
        '
        Me.TextRX.BackColor = System.Drawing.SystemColors.Control
        Me.TextRX.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TextRX.Location = New System.Drawing.Point(113, 45)
        Me.TextRX.Name = "TextRX"
        Me.TextRX.ReadOnly = True
        Me.TextRX.Size = New System.Drawing.Size(74, 20)
        Me.TextRX.TabIndex = 214
        Me.TextRX.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'GroupBox6
        '
        Me.GroupBox6.BackColor = System.Drawing.Color.Transparent
        Me.GroupBox6.Controls.Add(Me.CBX_Cortesia)
        Me.GroupBox6.Controls.Add(Me.CheckGarantia2)
        Me.GroupBox6.Controls.Add(Me.AgregarNew)
        Me.GroupBox6.Controls.Add(Me.ComboMiscDesc)
        Me.GroupBox6.Controls.Add(Me.Label25)
        Me.GroupBox6.Controls.Add(Me.txtMiscAmount)
        Me.GroupBox6.Controls.Add(Me.Button2)
        Me.GroupBox6.Controls.Add(Me.Label24)
        Me.GroupBox6.Controls.Add(Me.txtMiscDesc)
        Me.GroupBox6.Font = New System.Drawing.Font("Verdana", 9.5!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GroupBox6.ForeColor = System.Drawing.Color.White
        Me.GroupBox6.Location = New System.Drawing.Point(576, 588)
        Me.GroupBox6.Name = "GroupBox6"
        Me.GroupBox6.Size = New System.Drawing.Size(120, 77)
        Me.GroupBox6.TabIndex = 213
        Me.GroupBox6.TabStop = False
        Me.GroupBox6.Text = "Cargo Miscelaneo"
        Me.GroupBox6.Visible = False
        '
        'CBX_Cortesia
        '
        Me.CBX_Cortesia.AutoSize = True
        Me.CBX_Cortesia.BackColor = System.Drawing.Color.Transparent
        Me.CBX_Cortesia.Font = New System.Drawing.Font("Verdana", 9.5!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CBX_Cortesia.Location = New System.Drawing.Point(9, 98)
        Me.CBX_Cortesia.Name = "CBX_Cortesia"
        Me.CBX_Cortesia.Size = New System.Drawing.Size(190, 20)
        Me.CBX_Cortesia.TabIndex = 53
        Me.CBX_Cortesia.Text = "Marcar como Cortesía "
        Me.CBX_Cortesia.UseVisualStyleBackColor = False
        '
        'CheckGarantia2
        '
        Me.CheckGarantia2.AutoSize = True
        Me.CheckGarantia2.BackColor = System.Drawing.Color.Transparent
        Me.CheckGarantia2.Font = New System.Drawing.Font("Verdana", 9.5!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CheckGarantia2.Location = New System.Drawing.Point(9, 76)
        Me.CheckGarantia2.Name = "CheckGarantia2"
        Me.CheckGarantia2.Size = New System.Drawing.Size(187, 20)
        Me.CheckGarantia2.TabIndex = 52
        Me.CheckGarantia2.Text = "Marcar como Garantía"
        Me.CheckGarantia2.UseVisualStyleBackColor = False
        '
        'AgregarNew
        '
        Me.AgregarNew.BackColor = System.Drawing.Color.Transparent
        Me.AgregarNew.DisabledForeColor = System.Drawing.Color.Gray
        Me.AgregarNew.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel)
        Me.AgregarNew.ForeColor = System.Drawing.Color.White
        Me.AgregarNew.Highlighted = False
        Me.AgregarNew.Location = New System.Drawing.Point(419, 71)
        Me.AgregarNew.Name = "AgregarNew"
        Me.AgregarNew.Size = New System.Drawing.Size(100, 28)
        Me.AgregarNew.TabIndex = 51
        Me.AgregarNew.Texto = "Agregar"
        '
        'ComboMiscDesc
        '
        Me.ComboMiscDesc.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.ComboMiscDesc.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ComboMiscDesc.FormattingEnabled = True
        Me.ComboMiscDesc.Items.AddRange(New Object() {"DESCUENTO COORDINADOR", "GARANTIA", "AJUSTE CAPTURA", "PROMOCIONES", "MODIFICACION LISTA DE PRECIO", "AJUSTE CADENAS"})
        Me.ComboMiscDesc.Location = New System.Drawing.Point(9, 44)
        Me.ComboMiscDesc.Name = "ComboMiscDesc"
        Me.ComboMiscDesc.Size = New System.Drawing.Size(318, 22)
        Me.ComboMiscDesc.TabIndex = 45
        '
        'Label25
        '
        Me.Label25.AutoSize = True
        Me.Label25.Location = New System.Drawing.Point(414, 23)
        Me.Label25.Name = "Label25"
        Me.Label25.Size = New System.Drawing.Size(53, 16)
        Me.Label25.TabIndex = 44
        Me.Label25.Text = "Monto"
        '
        'txtMiscAmount
        '
        Me.txtMiscAmount.Location = New System.Drawing.Point(419, 42)
        Me.txtMiscAmount.Name = "txtMiscAmount"
        Me.txtMiscAmount.Size = New System.Drawing.Size(100, 23)
        Me.txtMiscAmount.TabIndex = 43
        Me.txtMiscAmount.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Button2
        '
        Me.Button2.ForeColor = System.Drawing.Color.Black
        Me.Button2.Location = New System.Drawing.Point(419, 71)
        Me.Button2.Name = "Button2"
        Me.Button2.Size = New System.Drawing.Size(100, 28)
        Me.Button2.TabIndex = 44
        Me.Button2.Text = "Agregar"
        Me.Button2.UseVisualStyleBackColor = True
        '
        'Label24
        '
        Me.Label24.AutoSize = True
        Me.Label24.Location = New System.Drawing.Point(6, 23)
        Me.Label24.Name = "Label24"
        Me.Label24.Size = New System.Drawing.Size(143, 16)
        Me.Label24.TabIndex = 42
        Me.Label24.Text = "Descripción Ajuste"
        '
        'txtMiscDesc
        '
        Me.txtMiscDesc.Location = New System.Drawing.Point(9, 43)
        Me.txtMiscDesc.MaxLength = 30
        Me.txtMiscDesc.Name = "txtMiscDesc"
        Me.txtMiscDesc.Size = New System.Drawing.Size(318, 23)
        Me.txtMiscDesc.TabIndex = 41
        Me.txtMiscDesc.Visible = False
        '
        'LblRxTotal
        '
        Me.LblRxTotal.AutoSize = True
        Me.LblRxTotal.BackColor = System.Drawing.Color.Transparent
        Me.LblRxTotal.Font = New System.Drawing.Font("Arial", 18.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblRxTotal.ForeColor = System.Drawing.Color.Red
        Me.LblRxTotal.Location = New System.Drawing.Point(577, 441)
        Me.LblRxTotal.Name = "LblRxTotal"
        Me.LblRxTotal.Size = New System.Drawing.Size(106, 29)
        Me.LblRxTotal.TabIndex = 212
        Me.LblRxTotal.Text = "Total Rx"
        Me.LblRxTotal.TextAlign = System.Drawing.ContentAlignment.TopRight
        Me.LblRxTotal.Visible = False
        '
        'Label23
        '
        Me.Label23.AutoSize = True
        Me.Label23.BackColor = System.Drawing.Color.Transparent
        Me.Label23.Font = New System.Drawing.Font("Verdana", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label23.Location = New System.Drawing.Point(491, 447)
        Me.Label23.Name = "Label23"
        Me.Label23.Size = New System.Drawing.Size(72, 17)
        Me.Label23.TabIndex = 211
        Me.Label23.Text = "Total Rx"
        Me.Label23.Visible = False
        '
        'Label20
        '
        Me.Label20.AutoSize = True
        Me.Label20.BackColor = System.Drawing.Color.Transparent
        Me.Label20.Font = New System.Drawing.Font("Verdana", 9.5!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label20.Location = New System.Drawing.Point(17, 153)
        Me.Label20.Name = "Label20"
        Me.Label20.Size = New System.Drawing.Size(103, 16)
        Me.Label20.TabIndex = 210
        Me.Label20.Text = "Detalle de Rx"
        '
        'LViewLines
        '
        Me.LViewLines.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.5!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LViewLines.FullRowSelect = True
        Me.LViewLines.GridLines = True
        Me.LViewLines.LabelEdit = True
        Me.LViewLines.Location = New System.Drawing.Point(20, 172)
        Me.LViewLines.MultiSelect = False
        Me.LViewLines.Name = "LViewLines"
        Me.LViewLines.Size = New System.Drawing.Size(543, 255)
        Me.LViewLines.Sorting = System.Windows.Forms.SortOrder.Ascending
        Me.LViewLines.TabIndex = 209
        Me.LViewLines.UseCompatibleStateImageBehavior = False
        Me.LViewLines.View = System.Windows.Forms.View.Details
        '
        'btnMiscCharge
        '
        Me.btnMiscCharge.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnMiscCharge.Image = Global.Laboratorios.My.Resources.Resources.edit_icon
        Me.btnMiscCharge.Location = New System.Drawing.Point(20, 417)
        Me.btnMiscCharge.Name = "btnMiscCharge"
        Me.btnMiscCharge.Size = New System.Drawing.Size(40, 40)
        Me.btnMiscCharge.TabIndex = 230
        Me.btnMiscCharge.UseVisualStyleBackColor = True
        '
        'lvListasPrecios
        '
        Me.lvListasPrecios.BackColor = System.Drawing.Color.FromArgb(CType(CType(16, Byte), Integer), CType(CType(16, Byte), Integer), CType(CType(32, Byte), Integer))
        Me.lvListasPrecios.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.lista1})
        Me.lvListasPrecios.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lvListasPrecios.ForeColor = System.Drawing.SystemColors.Info
        Me.lvListasPrecios.GridLines = True
        Me.lvListasPrecios.Items.AddRange(New System.Windows.Forms.ListViewItem() {ListViewItem3, ListViewItem4})
        Me.lvListasPrecios.Location = New System.Drawing.Point(20, 71)
        Me.lvListasPrecios.Name = "lvListasPrecios"
        Me.lvListasPrecios.Size = New System.Drawing.Size(543, 60)
        Me.lvListasPrecios.TabIndex = 233
        Me.lvListasPrecios.UseCompatibleStateImageBehavior = False
        Me.lvListasPrecios.View = System.Windows.Forms.View.List
        '
        'lista1
        '
        Me.lista1.Text = "Lista de precios del cliente"
        Me.lista1.Width = 260
        '
        'AperturaRx
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(16, Byte), Integer), CType(CType(16, Byte), Integer), CType(CType(32, Byte), Integer))
        Me.ClientSize = New System.Drawing.Size(602, 678)
        Me.Controls.Add(Me.lvListasPrecios)
        Me.Controls.Add(Me.Label133)
        Me.Controls.Add(Me.btnMiscCharge)
        Me.Controls.Add(Me.cl_CheckEnvio)
        Me.Controls.Add(Me.ButtonGetClosedRx)
        Me.Controls.Add(Me.GroupBox19)
        Me.Controls.Add(Me.Panel3)
        Me.Controls.Add(Me.Label59)
        Me.Controls.Add(Me.TextVantage)
        Me.Controls.Add(Me.LabelIsGarantia)
        Me.Controls.Add(Me.LabelIsGratis)
        Me.Controls.Add(Me.CloseRxNew)
        Me.Controls.Add(Me.Label47)
        Me.Controls.Add(Me.TextPO)
        Me.Controls.Add(Me.Label46)
        Me.Controls.Add(Me.TextCustName)
        Me.Controls.Add(Me.Label45)
        Me.Controls.Add(Me.TextRX)
        Me.Controls.Add(Me.GroupBox6)
        Me.Controls.Add(Me.LblRxTotal)
        Me.Controls.Add(Me.Label23)
        Me.Controls.Add(Me.Label20)
        Me.Controls.Add(Me.LViewLines)
        Me.ForeColor = System.Drawing.SystemColors.ControlLightLight
        Me.Name = "AperturaRx"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Apertura Orden Rx"
        Me.GroupBox19.ResumeLayout(False)
        Me.GroupBox19.PerformLayout()
        Me.Panel3.ResumeLayout(False)
        Me.Panel3.PerformLayout()
        Me.GroupBox6.ResumeLayout(False)
        Me.GroupBox6.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Label133 As System.Windows.Forms.Label
    Friend WithEvents btnMiscCharge As System.Windows.Forms.Button
    Friend WithEvents cl_CheckEnvio As System.Windows.Forms.CheckBox
    Friend WithEvents GroupBox19 As System.Windows.Forms.GroupBox
    Friend WithEvents Label106 As System.Windows.Forms.Label
    Friend WithEvents lblIVA As System.Windows.Forms.Label
    Friend WithEvents Label108 As System.Windows.Forms.Label
    Friend WithEvents cl_txtTotal2 As System.Windows.Forms.TextBox
    Friend WithEvents cl_txtIVA2 As System.Windows.Forms.TextBox
    Friend WithEvents cl_txtSubTot2 As System.Windows.Forms.TextBox
    Friend WithEvents cl_txtImporteLetra As System.Windows.Forms.TextBox
    Friend WithEvents Label109 As System.Windows.Forms.Label
    Friend WithEvents Panel3 As System.Windows.Forms.Panel
    Friend WithEvents TreeOrders As System.Windows.Forms.TreeView
    Friend WithEvents Label15 As System.Windows.Forms.Label
    Friend WithEvents LViewRX As System.Windows.Forms.ListView
    Friend WithEvents Label22 As System.Windows.Forms.Label
    Friend WithEvents BtnExpand As System.Windows.Forms.Button
    Friend WithEvents BtnCollapse As System.Windows.Forms.Button
    Friend WithEvents ButtonGetClosedRx As System.Windows.Forms.Button
    Friend WithEvents Label59 As System.Windows.Forms.Label
    Friend WithEvents TextVantage As System.Windows.Forms.TextBox
    Friend WithEvents LabelIsGarantia As System.Windows.Forms.Label
    Friend WithEvents LabelIsGratis As System.Windows.Forms.Label
    Friend WithEvents CloseRxNew As RoundButtonSmall.UserControl1
    Friend WithEvents Label47 As System.Windows.Forms.Label
    Friend WithEvents TextPO As System.Windows.Forms.TextBox
    Friend WithEvents Label46 As System.Windows.Forms.Label
    Friend WithEvents TextCustName As System.Windows.Forms.TextBox
    Friend WithEvents Label45 As System.Windows.Forms.Label
    Friend WithEvents TextRX As System.Windows.Forms.TextBox
    Friend WithEvents GroupBox6 As System.Windows.Forms.GroupBox
    Friend WithEvents CBX_Cortesia As System.Windows.Forms.CheckBox
    Friend WithEvents CheckGarantia2 As System.Windows.Forms.CheckBox
    Friend WithEvents AgregarNew As RoundButtonSmall.UserControl1
    Friend WithEvents ComboMiscDesc As System.Windows.Forms.ComboBox
    Friend WithEvents Label25 As System.Windows.Forms.Label
    Friend WithEvents txtMiscAmount As System.Windows.Forms.TextBox
    Friend WithEvents Button2 As System.Windows.Forms.Button
    Friend WithEvents Label24 As System.Windows.Forms.Label
    Friend WithEvents txtMiscDesc As System.Windows.Forms.TextBox
    Friend WithEvents LblRxTotal As System.Windows.Forms.Label
    Friend WithEvents Label23 As System.Windows.Forms.Label
    Friend WithEvents Label20 As System.Windows.Forms.Label
    Friend WithEvents LViewLines As System.Windows.Forms.ListView
    Friend WithEvents lvListasPrecios As System.Windows.Forms.ListView
    Friend WithEvents lista1 As System.Windows.Forms.ColumnHeader
End Class
