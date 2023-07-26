<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class ProcessEfficiency
    Inherits System.Windows.Forms.UserControl

    'UserControl overrides dispose to clean up the component list.
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
        Me.ActualizaProcesos = New System.Windows.Forms.Button
        Me.lblplant = New System.Windows.Forms.Label
        Me.TextOpenRx = New System.Windows.Forms.TextBox
        Me.TextOnTimeRx = New System.Windows.Forms.TextBox
        Me.TextOutOfTimeRx = New System.Windows.Forms.TextBox
        Me.Plant = New System.Windows.Forms.Label
        Me.LabelSL = New System.Windows.Forms.Label
        Me.LabelSLOnTime = New System.Windows.Forms.Label
        Me.LabelSLNotOnTime = New System.Windows.Forms.Label
        Me.LabelARNotOnTime = New System.Windows.Forms.Label
        Me.LabelAROnTime = New System.Windows.Forms.Label
        Me.LabelAR = New System.Windows.Forms.Label
        Me.LabelBMOnTime = New System.Windows.Forms.Label
        Me.LabelBM = New System.Windows.Forms.Label
        Me.LabelBMNotOnTime = New System.Windows.Forms.Label
        Me.GreenBarBM = New System.Windows.Forms.PictureBox
        Me.RedBarBM = New System.Windows.Forms.PictureBox
        Me.PictureBox3 = New System.Windows.Forms.PictureBox
        Me.GreenBarAR = New System.Windows.Forms.PictureBox
        Me.GreenBarSL = New System.Windows.Forms.PictureBox
        Me.RedBarAR = New System.Windows.Forms.PictureBox
        Me.RedBarSL = New System.Windows.Forms.PictureBox
        Me.PBoxAR = New System.Windows.Forms.PictureBox
        Me.PBoxSL = New System.Windows.Forms.PictureBox
        Me.RedBar = New System.Windows.Forms.PictureBox
        Me.GreenBar = New System.Windows.Forms.PictureBox
        Me.PBox = New System.Windows.Forms.PictureBox
        Me.PanelQA = New System.Windows.Forms.Panel
        Me.LabelPC = New System.Windows.Forms.Label
        Me.GreenBarPC = New System.Windows.Forms.PictureBox
        Me.LabelPCOnTime = New System.Windows.Forms.Label
        Me.RedBarPC = New System.Windows.Forms.PictureBox
        Me.LabelPCNotOnTime = New System.Windows.Forms.Label
        Me.BotonActualizar = New RoundButtonSmall.UserControl1
        CType(Me.GreenBarBM, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RedBarBM, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PictureBox3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GreenBarAR, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GreenBarSL, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RedBarAR, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RedBarSL, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PBoxAR, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PBoxSL, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RedBar, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GreenBar, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PBox, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.PanelQA.SuspendLayout()
        CType(Me.GreenBarPC, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RedBarPC, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'ActualizaProcesos
        '
        Me.ActualizaProcesos.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ActualizaProcesos.Location = New System.Drawing.Point(9, 400)
        Me.ActualizaProcesos.Name = "ActualizaProcesos"
        Me.ActualizaProcesos.Size = New System.Drawing.Size(47, 26)
        Me.ActualizaProcesos.TabIndex = 2
        Me.ActualizaProcesos.Text = "ACTUALIZAR"
        Me.ActualizaProcesos.UseVisualStyleBackColor = True
        Me.ActualizaProcesos.Visible = False
        '
        'lblplant
        '
        Me.lblplant.AutoSize = True
        Me.lblplant.BackColor = System.Drawing.Color.Transparent
        Me.lblplant.Font = New System.Drawing.Font("DINMittelschrift LT Alternate", 17.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblplant.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.lblplant.Location = New System.Drawing.Point(138, 0)
        Me.lblplant.Name = "lblplant"
        Me.lblplant.Size = New System.Drawing.Size(62, 27)
        Me.lblplant.TabIndex = 7
        Me.lblplant.Text = "Plant"
        Me.lblplant.Visible = False
        '
        'TextOpenRx
        '
        Me.TextOpenRx.BackColor = System.Drawing.Color.White
        Me.TextOpenRx.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TextOpenRx.Cursor = System.Windows.Forms.Cursors.Hand
        Me.TextOpenRx.Font = New System.Drawing.Font("DINMittelschrift LT Alternate", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TextOpenRx.ForeColor = System.Drawing.Color.Black
        Me.TextOpenRx.Location = New System.Drawing.Point(26, 352)
        Me.TextOpenRx.Name = "TextOpenRx"
        Me.TextOpenRx.ReadOnly = True
        Me.TextOpenRx.Size = New System.Drawing.Size(30, 19)
        Me.TextOpenRx.TabIndex = 10
        Me.TextOpenRx.Text = "0"
        Me.TextOpenRx.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'TextOnTimeRx
        '
        Me.TextOnTimeRx.BackColor = System.Drawing.Color.White
        Me.TextOnTimeRx.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TextOnTimeRx.Cursor = System.Windows.Forms.Cursors.Hand
        Me.TextOnTimeRx.Font = New System.Drawing.Font("DINMittelschrift LT Alternate", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TextOnTimeRx.ForeColor = System.Drawing.Color.Green
        Me.TextOnTimeRx.Location = New System.Drawing.Point(120, 385)
        Me.TextOnTimeRx.Name = "TextOnTimeRx"
        Me.TextOnTimeRx.ReadOnly = True
        Me.TextOnTimeRx.Size = New System.Drawing.Size(30, 19)
        Me.TextOnTimeRx.TabIndex = 12
        Me.TextOnTimeRx.Text = "0"
        Me.TextOnTimeRx.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'TextOutOfTimeRx
        '
        Me.TextOutOfTimeRx.BackColor = System.Drawing.Color.White
        Me.TextOutOfTimeRx.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TextOutOfTimeRx.Cursor = System.Windows.Forms.Cursors.Hand
        Me.TextOutOfTimeRx.Font = New System.Drawing.Font("DINMittelschrift LT Alternate", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TextOutOfTimeRx.ForeColor = System.Drawing.Color.DarkRed
        Me.TextOutOfTimeRx.Location = New System.Drawing.Point(120, 295)
        Me.TextOutOfTimeRx.Name = "TextOutOfTimeRx"
        Me.TextOutOfTimeRx.ReadOnly = True
        Me.TextOutOfTimeRx.Size = New System.Drawing.Size(30, 19)
        Me.TextOutOfTimeRx.TabIndex = 14
        Me.TextOutOfTimeRx.Text = "0"
        Me.TextOutOfTimeRx.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'Plant
        '
        Me.Plant.AutoSize = True
        Me.Plant.BackColor = System.Drawing.Color.Transparent
        Me.Plant.Font = New System.Drawing.Font("DINMittelschrift LT Alternate", 16.5!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Plant.ForeColor = System.Drawing.Color.White
        Me.Plant.Location = New System.Drawing.Point(106, 23)
        Me.Plant.Name = "Plant"
        Me.Plant.Size = New System.Drawing.Size(60, 26)
        Me.Plant.TabIndex = 8
        Me.Plant.Text = "Plant"
        '
        'LabelSL
        '
        Me.LabelSL.AutoSize = True
        Me.LabelSL.BackColor = System.Drawing.Color.Transparent
        Me.LabelSL.Cursor = System.Windows.Forms.Cursors.Hand
        Me.LabelSL.Font = New System.Drawing.Font("DINMittelschrift LT Alternate", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LabelSL.ForeColor = System.Drawing.Color.White
        Me.LabelSL.Location = New System.Drawing.Point(101, 6)
        Me.LabelSL.Name = "LabelSL"
        Me.LabelSL.Size = New System.Drawing.Size(28, 19)
        Me.LabelSL.TabIndex = 34
        Me.LabelSL.Text = "GP"
        Me.LabelSL.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'LabelSLOnTime
        '
        Me.LabelSLOnTime.AutoSize = True
        Me.LabelSLOnTime.BackColor = System.Drawing.Color.Transparent
        Me.LabelSLOnTime.Cursor = System.Windows.Forms.Cursors.Hand
        Me.LabelSLOnTime.Font = New System.Drawing.Font("DINMittelschrift LT Alternate", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LabelSLOnTime.ForeColor = System.Drawing.Color.White
        Me.LabelSLOnTime.Location = New System.Drawing.Point(0, 29)
        Me.LabelSLOnTime.Name = "LabelSLOnTime"
        Me.LabelSLOnTime.Size = New System.Drawing.Size(38, 19)
        Me.LabelSLOnTime.TabIndex = 35
        Me.LabelSLOnTime.Text = "GPG"
        Me.LabelSLOnTime.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'LabelSLNotOnTime
        '
        Me.LabelSLNotOnTime.AutoSize = True
        Me.LabelSLNotOnTime.BackColor = System.Drawing.Color.Transparent
        Me.LabelSLNotOnTime.Cursor = System.Windows.Forms.Cursors.Hand
        Me.LabelSLNotOnTime.Font = New System.Drawing.Font("DINMittelschrift LT Alternate", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LabelSLNotOnTime.ForeColor = System.Drawing.Color.White
        Me.LabelSLNotOnTime.Location = New System.Drawing.Point(148, 29)
        Me.LabelSLNotOnTime.Name = "LabelSLNotOnTime"
        Me.LabelSLNotOnTime.Size = New System.Drawing.Size(38, 19)
        Me.LabelSLNotOnTime.TabIndex = 36
        Me.LabelSLNotOnTime.Text = "GPR"
        Me.LabelSLNotOnTime.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'LabelARNotOnTime
        '
        Me.LabelARNotOnTime.AutoSize = True
        Me.LabelARNotOnTime.BackColor = System.Drawing.Color.Transparent
        Me.LabelARNotOnTime.Cursor = System.Windows.Forms.Cursors.Hand
        Me.LabelARNotOnTime.Font = New System.Drawing.Font("DINMittelschrift LT Alternate", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LabelARNotOnTime.ForeColor = System.Drawing.Color.White
        Me.LabelARNotOnTime.Location = New System.Drawing.Point(148, 79)
        Me.LabelARNotOnTime.Name = "LabelARNotOnTime"
        Me.LabelARNotOnTime.Size = New System.Drawing.Size(39, 19)
        Me.LabelARNotOnTime.TabIndex = 39
        Me.LabelARNotOnTime.Text = "ARR"
        Me.LabelARNotOnTime.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'LabelAROnTime
        '
        Me.LabelAROnTime.AutoSize = True
        Me.LabelAROnTime.BackColor = System.Drawing.Color.Transparent
        Me.LabelAROnTime.Cursor = System.Windows.Forms.Cursors.Hand
        Me.LabelAROnTime.Font = New System.Drawing.Font("DINMittelschrift LT Alternate", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LabelAROnTime.ForeColor = System.Drawing.Color.White
        Me.LabelAROnTime.Location = New System.Drawing.Point(0, 79)
        Me.LabelAROnTime.Name = "LabelAROnTime"
        Me.LabelAROnTime.Size = New System.Drawing.Size(39, 19)
        Me.LabelAROnTime.TabIndex = 38
        Me.LabelAROnTime.Text = "ARG"
        Me.LabelAROnTime.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'LabelAR
        '
        Me.LabelAR.AutoSize = True
        Me.LabelAR.BackColor = System.Drawing.Color.Transparent
        Me.LabelAR.Cursor = System.Windows.Forms.Cursors.Hand
        Me.LabelAR.Font = New System.Drawing.Font("DINMittelschrift LT Alternate", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LabelAR.ForeColor = System.Drawing.Color.White
        Me.LabelAR.Location = New System.Drawing.Point(101, 56)
        Me.LabelAR.Name = "LabelAR"
        Me.LabelAR.Size = New System.Drawing.Size(29, 19)
        Me.LabelAR.TabIndex = 37
        Me.LabelAR.Text = "AR"
        Me.LabelAR.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'LabelBMOnTime
        '
        Me.LabelBMOnTime.AutoSize = True
        Me.LabelBMOnTime.BackColor = System.Drawing.Color.Transparent
        Me.LabelBMOnTime.Cursor = System.Windows.Forms.Cursors.Hand
        Me.LabelBMOnTime.Font = New System.Drawing.Font("DINMittelschrift LT Alternate", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LabelBMOnTime.ForeColor = System.Drawing.Color.White
        Me.LabelBMOnTime.Location = New System.Drawing.Point(0, 128)
        Me.LabelBMOnTime.Name = "LabelBMOnTime"
        Me.LabelBMOnTime.Size = New System.Drawing.Size(41, 19)
        Me.LabelBMOnTime.TabIndex = 45
        Me.LabelBMOnTime.Text = "BMG"
        Me.LabelBMOnTime.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'LabelBM
        '
        Me.LabelBM.AutoSize = True
        Me.LabelBM.BackColor = System.Drawing.Color.Transparent
        Me.LabelBM.Cursor = System.Windows.Forms.Cursors.Hand
        Me.LabelBM.Font = New System.Drawing.Font("DINMittelschrift LT Alternate", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LabelBM.ForeColor = System.Drawing.Color.White
        Me.LabelBM.Location = New System.Drawing.Point(101, 106)
        Me.LabelBM.Name = "LabelBM"
        Me.LabelBM.Size = New System.Drawing.Size(31, 19)
        Me.LabelBM.TabIndex = 44
        Me.LabelBM.Text = "BM"
        Me.LabelBM.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'LabelBMNotOnTime
        '
        Me.LabelBMNotOnTime.AutoSize = True
        Me.LabelBMNotOnTime.BackColor = System.Drawing.Color.Transparent
        Me.LabelBMNotOnTime.Cursor = System.Windows.Forms.Cursors.Hand
        Me.LabelBMNotOnTime.Font = New System.Drawing.Font("DINMittelschrift LT Alternate", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LabelBMNotOnTime.ForeColor = System.Drawing.Color.White
        Me.LabelBMNotOnTime.Location = New System.Drawing.Point(148, 128)
        Me.LabelBMNotOnTime.Name = "LabelBMNotOnTime"
        Me.LabelBMNotOnTime.Size = New System.Drawing.Size(41, 19)
        Me.LabelBMNotOnTime.TabIndex = 46
        Me.LabelBMNotOnTime.Text = "BMR"
        Me.LabelBMNotOnTime.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'GreenBarBM
        '
        Me.GreenBarBM.BackColor = System.Drawing.Color.Transparent
        Me.GreenBarBM.Cursor = System.Windows.Forms.Cursors.Hand
        Me.GreenBarBM.Image = Global.Laboratorios.My.Resources.Resources.VistaGreenH
        Me.GreenBarBM.Location = New System.Drawing.Point(39, 127)
        Me.GreenBarBM.Name = "GreenBarBM"
        Me.GreenBarBM.Size = New System.Drawing.Size(64, 22)
        Me.GreenBarBM.TabIndex = 42
        Me.GreenBarBM.TabStop = False
        Me.GreenBarBM.Tag = ""
        '
        'RedBarBM
        '
        Me.RedBarBM.BackColor = System.Drawing.Color.Transparent
        Me.RedBarBM.Cursor = System.Windows.Forms.Cursors.Hand
        Me.RedBarBM.Image = Global.Laboratorios.My.Resources.Resources.VistaRedH1
        Me.RedBarBM.Location = New System.Drawing.Point(39, 127)
        Me.RedBarBM.Name = "RedBarBM"
        Me.RedBarBM.Size = New System.Drawing.Size(107, 22)
        Me.RedBarBM.TabIndex = 41
        Me.RedBarBM.TabStop = False
        Me.RedBarBM.Tag = ""
        '
        'PictureBox3
        '
        Me.PictureBox3.Location = New System.Drawing.Point(40, 127)
        Me.PictureBox3.Name = "PictureBox3"
        Me.PictureBox3.Size = New System.Drawing.Size(71, 13)
        Me.PictureBox3.TabIndex = 43
        Me.PictureBox3.TabStop = False
        '
        'GreenBarAR
        '
        Me.GreenBarAR.BackColor = System.Drawing.Color.Transparent
        Me.GreenBarAR.Cursor = System.Windows.Forms.Cursors.Hand
        Me.GreenBarAR.Image = Global.Laboratorios.My.Resources.Resources.VistaGreenH
        Me.GreenBarAR.Location = New System.Drawing.Point(39, 78)
        Me.GreenBarAR.Name = "GreenBarAR"
        Me.GreenBarAR.Size = New System.Drawing.Size(64, 22)
        Me.GreenBarAR.TabIndex = 23
        Me.GreenBarAR.TabStop = False
        Me.GreenBarAR.Tag = ""
        '
        'GreenBarSL
        '
        Me.GreenBarSL.BackColor = System.Drawing.Color.Transparent
        Me.GreenBarSL.Cursor = System.Windows.Forms.Cursors.Hand
        Me.GreenBarSL.Image = Global.Laboratorios.My.Resources.Resources.VistaGreenH
        Me.GreenBarSL.Location = New System.Drawing.Point(39, 28)
        Me.GreenBarSL.Name = "GreenBarSL"
        Me.GreenBarSL.Size = New System.Drawing.Size(58, 22)
        Me.GreenBarSL.TabIndex = 22
        Me.GreenBarSL.TabStop = False
        Me.GreenBarSL.Tag = ""
        '
        'RedBarAR
        '
        Me.RedBarAR.BackColor = System.Drawing.Color.Transparent
        Me.RedBarAR.Cursor = System.Windows.Forms.Cursors.Hand
        Me.RedBarAR.Image = Global.Laboratorios.My.Resources.Resources.VistaRedH
        Me.RedBarAR.Location = New System.Drawing.Point(39, 78)
        Me.RedBarAR.Name = "RedBarAR"
        Me.RedBarAR.Size = New System.Drawing.Size(107, 22)
        Me.RedBarAR.TabIndex = 20
        Me.RedBarAR.TabStop = False
        Me.RedBarAR.Tag = ""
        '
        'RedBarSL
        '
        Me.RedBarSL.BackColor = System.Drawing.Color.Transparent
        Me.RedBarSL.Cursor = System.Windows.Forms.Cursors.Hand
        Me.RedBarSL.Image = Global.Laboratorios.My.Resources.Resources.VistaRedH
        Me.RedBarSL.Location = New System.Drawing.Point(39, 28)
        Me.RedBarSL.Name = "RedBarSL"
        Me.RedBarSL.Size = New System.Drawing.Size(107, 22)
        Me.RedBarSL.TabIndex = 19
        Me.RedBarSL.TabStop = False
        Me.RedBarSL.Tag = ""
        '
        'PBoxAR
        '
        Me.PBoxAR.Location = New System.Drawing.Point(40, 78)
        Me.PBoxAR.Name = "PBoxAR"
        Me.PBoxAR.Size = New System.Drawing.Size(71, 13)
        Me.PBoxAR.TabIndex = 30
        Me.PBoxAR.TabStop = False
        '
        'PBoxSL
        '
        Me.PBoxSL.Location = New System.Drawing.Point(39, 28)
        Me.PBoxSL.Name = "PBoxSL"
        Me.PBoxSL.Size = New System.Drawing.Size(72, 13)
        Me.PBoxSL.TabIndex = 31
        Me.PBoxSL.TabStop = False
        '
        'RedBar
        '
        Me.RedBar.BackColor = System.Drawing.Color.Transparent
        Me.RedBar.Cursor = System.Windows.Forms.Cursors.Hand
        Me.RedBar.Image = Global.Laboratorios.My.Resources.Resources.VistaRedV
        Me.RedBar.Location = New System.Drawing.Point(79, 286)
        Me.RedBar.Name = "RedBar"
        Me.RedBar.Size = New System.Drawing.Size(29, 13)
        Me.RedBar.TabIndex = 9
        Me.RedBar.TabStop = False
        Me.RedBar.Tag = ""
        '
        'GreenBar
        '
        Me.GreenBar.BackColor = System.Drawing.Color.Transparent
        Me.GreenBar.Cursor = System.Windows.Forms.Cursors.Hand
        Me.GreenBar.Image = Global.Laboratorios.My.Resources.Resources.VistaGreenV
        Me.GreenBar.Location = New System.Drawing.Point(79, 286)
        Me.GreenBar.Name = "GreenBar"
        Me.GreenBar.Size = New System.Drawing.Size(29, 162)
        Me.GreenBar.TabIndex = 11
        Me.GreenBar.TabStop = False
        Me.GreenBar.Tag = ""
        '
        'PBox
        '
        Me.PBox.BackColor = System.Drawing.SystemColors.ControlLightLight
        Me.PBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.PBox.Location = New System.Drawing.Point(79, 286)
        Me.PBox.Name = "PBox"
        Me.PBox.Size = New System.Drawing.Size(29, 161)
        Me.PBox.TabIndex = 10
        Me.PBox.TabStop = False
        '
        'PanelQA
        '
        Me.PanelQA.BackColor = System.Drawing.Color.Transparent
        Me.PanelQA.Controls.Add(Me.LabelPC)
        Me.PanelQA.Controls.Add(Me.LabelBM)
        Me.PanelQA.Controls.Add(Me.GreenBarPC)
        Me.PanelQA.Controls.Add(Me.LabelPCOnTime)
        Me.PanelQA.Controls.Add(Me.LabelSLNotOnTime)
        Me.PanelQA.Controls.Add(Me.RedBarPC)
        Me.PanelQA.Controls.Add(Me.GreenBarBM)
        Me.PanelQA.Controls.Add(Me.LabelAR)
        Me.PanelQA.Controls.Add(Me.LabelPCNotOnTime)
        Me.PanelQA.Controls.Add(Me.LabelBMOnTime)
        Me.PanelQA.Controls.Add(Me.GreenBarAR)
        Me.PanelQA.Controls.Add(Me.LabelARNotOnTime)
        Me.PanelQA.Controls.Add(Me.RedBarAR)
        Me.PanelQA.Controls.Add(Me.LabelAROnTime)
        Me.PanelQA.Controls.Add(Me.RedBarBM)
        Me.PanelQA.Controls.Add(Me.PBoxAR)
        Me.PanelQA.Controls.Add(Me.PictureBox3)
        Me.PanelQA.Controls.Add(Me.LabelSL)
        Me.PanelQA.Controls.Add(Me.LabelBMNotOnTime)
        Me.PanelQA.Controls.Add(Me.GreenBarSL)
        Me.PanelQA.Controls.Add(Me.LabelSLOnTime)
        Me.PanelQA.Controls.Add(Me.RedBarSL)
        Me.PanelQA.Controls.Add(Me.PBoxSL)
        Me.PanelQA.Font = New System.Drawing.Font("DINMittelschrift LT Alternate", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.PanelQA.Location = New System.Drawing.Point(9, 76)
        Me.PanelQA.Name = "PanelQA"
        Me.PanelQA.Size = New System.Drawing.Size(184, 201)
        Me.PanelQA.TabIndex = 48
        '
        'LabelPC
        '
        Me.LabelPC.AutoSize = True
        Me.LabelPC.BackColor = System.Drawing.Color.Transparent
        Me.LabelPC.Cursor = System.Windows.Forms.Cursors.Hand
        Me.LabelPC.Font = New System.Drawing.Font("DINMittelschrift LT Alternate", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LabelPC.ForeColor = System.Drawing.Color.White
        Me.LabelPC.Location = New System.Drawing.Point(102, 154)
        Me.LabelPC.Name = "LabelPC"
        Me.LabelPC.Size = New System.Drawing.Size(27, 19)
        Me.LabelPC.TabIndex = 127
        Me.LabelPC.Text = "PC"
        Me.LabelPC.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'GreenBarPC
        '
        Me.GreenBarPC.BackColor = System.Drawing.Color.Transparent
        Me.GreenBarPC.Cursor = System.Windows.Forms.Cursors.Hand
        Me.GreenBarPC.Image = Global.Laboratorios.My.Resources.Resources.VistaGreenH
        Me.GreenBarPC.Location = New System.Drawing.Point(40, 175)
        Me.GreenBarPC.Name = "GreenBarPC"
        Me.GreenBarPC.Size = New System.Drawing.Size(64, 22)
        Me.GreenBarPC.TabIndex = 125
        Me.GreenBarPC.TabStop = False
        Me.GreenBarPC.Tag = ""
        '
        'LabelPCOnTime
        '
        Me.LabelPCOnTime.AutoSize = True
        Me.LabelPCOnTime.BackColor = System.Drawing.Color.Transparent
        Me.LabelPCOnTime.Cursor = System.Windows.Forms.Cursors.Hand
        Me.LabelPCOnTime.Font = New System.Drawing.Font("DINMittelschrift LT Alternate", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LabelPCOnTime.ForeColor = System.Drawing.Color.White
        Me.LabelPCOnTime.Location = New System.Drawing.Point(1, 176)
        Me.LabelPCOnTime.Name = "LabelPCOnTime"
        Me.LabelPCOnTime.Size = New System.Drawing.Size(37, 19)
        Me.LabelPCOnTime.TabIndex = 128
        Me.LabelPCOnTime.Text = "PCG"
        Me.LabelPCOnTime.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'RedBarPC
        '
        Me.RedBarPC.BackColor = System.Drawing.Color.Transparent
        Me.RedBarPC.Cursor = System.Windows.Forms.Cursors.Hand
        Me.RedBarPC.Image = Global.Laboratorios.My.Resources.Resources.VistaRedH1
        Me.RedBarPC.Location = New System.Drawing.Point(40, 175)
        Me.RedBarPC.Name = "RedBarPC"
        Me.RedBarPC.Size = New System.Drawing.Size(107, 22)
        Me.RedBarPC.TabIndex = 124
        Me.RedBarPC.TabStop = False
        Me.RedBarPC.Tag = ""
        '
        'LabelPCNotOnTime
        '
        Me.LabelPCNotOnTime.AutoSize = True
        Me.LabelPCNotOnTime.BackColor = System.Drawing.Color.Transparent
        Me.LabelPCNotOnTime.Cursor = System.Windows.Forms.Cursors.Hand
        Me.LabelPCNotOnTime.Font = New System.Drawing.Font("DINMittelschrift LT Alternate", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LabelPCNotOnTime.ForeColor = System.Drawing.Color.White
        Me.LabelPCNotOnTime.Location = New System.Drawing.Point(149, 176)
        Me.LabelPCNotOnTime.Name = "LabelPCNotOnTime"
        Me.LabelPCNotOnTime.Size = New System.Drawing.Size(37, 19)
        Me.LabelPCNotOnTime.TabIndex = 129
        Me.LabelPCNotOnTime.Text = "PCR"
        Me.LabelPCNotOnTime.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'BotonActualizar
        '
        Me.BotonActualizar.BackColor = System.Drawing.Color.Transparent
        Me.BotonActualizar.DisabledForeColor = System.Drawing.Color.Gray
        Me.BotonActualizar.Font = New System.Drawing.Font("DINMittelschrift LT Alternate", 13.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel)
        Me.BotonActualizar.ForeColor = System.Drawing.Color.White
        Me.BotonActualizar.Highlighted = False
        Me.BotonActualizar.Location = New System.Drawing.Point(42, 455)
        Me.BotonActualizar.Name = "BotonActualizar"
        Me.BotonActualizar.Size = New System.Drawing.Size(118, 33)
        Me.BotonActualizar.TabIndex = 123
        Me.BotonActualizar.Texto = "Actualizar"
        '
        'ProcessEfficiency
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackgroundImage = Global.Laboratorios.My.Resources.Resources.VistaProcessEfficieny2
        Me.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None
        Me.Controls.Add(Me.BotonActualizar)
        Me.Controls.Add(Me.PanelQA)
        Me.Controls.Add(Me.TextOutOfTimeRx)
        Me.Controls.Add(Me.TextOnTimeRx)
        Me.Controls.Add(Me.TextOpenRx)
        Me.Controls.Add(Me.RedBar)
        Me.Controls.Add(Me.Plant)
        Me.Controls.Add(Me.ActualizaProcesos)
        Me.Controls.Add(Me.lblplant)
        Me.Controls.Add(Me.GreenBar)
        Me.Controls.Add(Me.PBox)
        Me.Name = "ProcessEfficiency"
        Me.Size = New System.Drawing.Size(200, 498)
        CType(Me.GreenBarBM, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RedBarBM, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PictureBox3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GreenBarAR, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GreenBarSL, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RedBarAR, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RedBarSL, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PBoxAR, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PBoxSL, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RedBar, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GreenBar, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PBox, System.ComponentModel.ISupportInitialize).EndInit()
        Me.PanelQA.ResumeLayout(False)
        Me.PanelQA.PerformLayout()
        CType(Me.GreenBarPC, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RedBarPC, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents ActualizaProcesos As System.Windows.Forms.Button
    Friend WithEvents lblplant As System.Windows.Forms.Label
    Friend WithEvents TextOpenRx As System.Windows.Forms.TextBox
    Friend WithEvents TextOnTimeRx As System.Windows.Forms.TextBox
    Friend WithEvents TextOutOfTimeRx As System.Windows.Forms.TextBox
    Friend WithEvents Plant As System.Windows.Forms.Label
    Friend WithEvents RedBar As System.Windows.Forms.PictureBox
    Friend WithEvents PBox As System.Windows.Forms.PictureBox
    Friend WithEvents GreenBar As System.Windows.Forms.PictureBox
    Friend WithEvents RedBarSL As System.Windows.Forms.PictureBox
    Friend WithEvents RedBarAR As System.Windows.Forms.PictureBox
    Friend WithEvents GreenBarSL As System.Windows.Forms.PictureBox
    Friend WithEvents GreenBarAR As System.Windows.Forms.PictureBox
    Friend WithEvents PBoxAR As System.Windows.Forms.PictureBox
    Friend WithEvents PBoxSL As System.Windows.Forms.PictureBox
    Friend WithEvents LabelSL As System.Windows.Forms.Label
    Friend WithEvents LabelSLOnTime As System.Windows.Forms.Label
    Friend WithEvents LabelSLNotOnTime As System.Windows.Forms.Label
    Friend WithEvents LabelARNotOnTime As System.Windows.Forms.Label
    Friend WithEvents LabelAROnTime As System.Windows.Forms.Label
    Friend WithEvents LabelAR As System.Windows.Forms.Label
    Friend WithEvents LabelBMOnTime As System.Windows.Forms.Label
    Friend WithEvents LabelBM As System.Windows.Forms.Label
    Friend WithEvents GreenBarBM As System.Windows.Forms.PictureBox
    Friend WithEvents RedBarBM As System.Windows.Forms.PictureBox
    Friend WithEvents PictureBox3 As System.Windows.Forms.PictureBox
    Friend WithEvents LabelBMNotOnTime As System.Windows.Forms.Label
    Friend WithEvents PanelQA As System.Windows.Forms.Panel
    Friend WithEvents BotonActualizar As RoundButtonSmall.UserControl1
    Friend WithEvents LabelPC As System.Windows.Forms.Label
    Friend WithEvents GreenBarPC As System.Windows.Forms.PictureBox
    Friend WithEvents LabelPCOnTime As System.Windows.Forms.Label
    Friend WithEvents RedBarPC As System.Windows.Forms.PictureBox
    Friend WithEvents LabelPCNotOnTime As System.Windows.Forms.Label

End Class
