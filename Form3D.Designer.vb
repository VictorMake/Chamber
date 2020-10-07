<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Form3D
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
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Form3D))
        Me.TabControlGrph = New System.Windows.Forms.TabControl()
        Me.TabPageGrph = New System.Windows.Forms.TabPage()
        Me.Panel3DGraf = New System.Windows.Forms.Panel()
        Me.CWGraph3DЦилиндр = New AxCW3DGraphLib.AxCWGraph3D()
        Me.PanelSettingCylinder = New System.Windows.Forms.Panel()
        Me.GroupBoxVolume = New System.Windows.Forms.GroupBox()
        Me.RadioButtonViewFlatness = New System.Windows.Forms.RadioButton()
        Me.RadioButtonViewVolume = New System.Windows.Forms.RadioButton()
        Me.ComboBoxBurner = New System.Windows.Forms.ComboBox()
        Me.CheckBoxNumber = New System.Windows.Forms.CheckBox()
        Me.GroupBoxPlotStyles = New System.Windows.Forms.GroupBox()
        Me.RadioButtonPoint = New System.Windows.Forms.RadioButton()
        Me.RadioButtonLine = New System.Windows.Forms.RadioButton()
        Me.RadioButtonSurface = New System.Windows.Forms.RadioButton()
        Me.RadioButtonContour = New System.Windows.Forms.RadioButton()
        Me.RadioButtonLinePoint = New System.Windows.Forms.RadioButton()
        Me.RadioButtonHiddenLine = New System.Windows.Forms.RadioButton()
        Me.RadioButtonSurfaceLine = New System.Windows.Forms.RadioButton()
        Me.RadioButtonSurfaceContour = New System.Windows.Forms.RadioButton()
        Me.RadioButtonSurfaceNormal = New System.Windows.Forms.RadioButton()
        Me.LabelViewBurner = New System.Windows.Forms.Label()
        Me.LabelHelp = New System.Windows.Forms.Label()
        Me.GroupBoxColor = New System.Windows.Forms.GroupBox()
        Me.RadioButtonNullColor = New System.Windows.Forms.RadioButton()
        Me.RadioButtonRed = New System.Windows.Forms.RadioButton()
        Me.RadioButtonRainbow = New System.Windows.Forms.RadioButton()
        Me.CheckBoxShell = New System.Windows.Forms.CheckBox()
        Me.GroupBoxAxisColor = New System.Windows.Forms.GroupBox()
        Me.RadioButtonExpanded = New System.Windows.Forms.RadioButton()
        Me.RadioButtonNormal = New System.Windows.Forms.RadioButton()
        Me.CheckBoxMarking = New System.Windows.Forms.CheckBox()
        Me.TabPageSurface = New System.Windows.Forms.TabPage()
        Me.AxCWGraph3DEvolvent = New AxCW3DGraphLib.AxCWGraph3D()
        Me.PanelSetting = New System.Windows.Forms.Panel()
        Me.LabelSurface = New System.Windows.Forms.Label()
        Me.ComboPlotsList = New System.Windows.Forms.ComboBox()
        Me.TabControlSurface = New System.Windows.Forms.TabControl()
        Me.TabPageSurface2 = New System.Windows.Forms.TabPage()
        Me.AxCWGraph3DПроэкция = New AxCW3DGraphLib.AxCWGraph3D()
        Me.GroupBoxPositionCursor = New System.Windows.Forms.GroupBox()
        Me.NumericEditColum = New NationalInstruments.UI.WindowsForms.NumericEdit()
        Me.NumericEditRow = New NationalInstruments.UI.WindowsForms.NumericEdit()
        Me.TextZPosition = New System.Windows.Forms.TextBox()
        Me.TextYPosition = New System.Windows.Forms.TextBox()
        Me.TextXPosition = New System.Windows.Forms.TextBox()
        Me.ComboSnapMode = New System.Windows.Forms.ComboBox()
        Me.LabelColumn = New System.Windows.Forms.Label()
        Me.LabelRow = New System.Windows.Forms.Label()
        Me.LabelZ = New System.Windows.Forms.Label()
        Me.LabelY = New System.Windows.Forms.Label()
        Me.LabelX = New System.Windows.Forms.Label()
        Me.LabelRegimeLinkage = New System.Windows.Forms.Label()
        Me.SlideAxis = New NationalInstruments.UI.WindowsForms.Slide()
        Me.GroupeBoxProjection = New System.Windows.Forms.GroupBox()
        Me.TextTransparency = New NationalInstruments.UI.WindowsForms.NumericEdit()
        Me.LabelTransparency = New System.Windows.Forms.Label()
        Me.CheckEnableProjection = New System.Windows.Forms.CheckBox()
        Me.CheckYZPlane = New System.Windows.Forms.RadioButton()
        Me.CheckXZPlane = New System.Windows.Forms.RadioButton()
        Me.CheckXYPlane = New System.Windows.Forms.RadioButton()
        Me.ButtonColorPlane = New System.Windows.Forms.Button()
        Me.GroupBoxtransparency = New System.Windows.Forms.GroupBox()
        Me.SlideTransparentMax = New NationalInstruments.UI.WindowsForms.Slide()
        Me.SlideTransparentMean = New NationalInstruments.UI.WindowsForms.Slide()
        Me.SlideTransparentMin = New NationalInstruments.UI.WindowsForms.Slide()
        Me.TabPageStyle = New System.Windows.Forms.TabPage()
        Me.GroupBoxPlotStylesContour = New System.Windows.Forms.GroupBox()
        Me.RadioButtonSurfaceNormal2 = New System.Windows.Forms.RadioButton()
        Me.RadioButtonSurfaceContour2 = New System.Windows.Forms.RadioButton()
        Me.RadioButtonSurfaceLine2 = New System.Windows.Forms.RadioButton()
        Me.RadioButtonHiddenLine2 = New System.Windows.Forms.RadioButton()
        Me.RadioButtonLinePoint2 = New System.Windows.Forms.RadioButton()
        Me.RadioButtonContour2 = New System.Windows.Forms.RadioButton()
        Me.RadioButtonSurface2 = New System.Windows.Forms.RadioButton()
        Me.RadioButtonLine2 = New System.Windows.Forms.RadioButton()
        Me.RadioButtonPoint2 = New System.Windows.Forms.RadioButton()
        Me.CheckBoxFastDraw = New System.Windows.Forms.CheckBox()
        Me.CheckBoxPerspective = New System.Windows.Forms.CheckBox()
        Me.TabPageCursor = New System.Windows.Forms.TabPage()
        Me.GroupBoxCursor = New System.Windows.Forms.GroupBox()
        Me.ButtonSetAllColors = New System.Windows.Forms.Button()
        Me.TextNameCursor = New System.Windows.Forms.TextBox()
        Me.CheckEnabled = New System.Windows.Forms.CheckBox()
        Me.CheckVisible = New System.Windows.Forms.CheckBox()
        Me.ComboCursors = New System.Windows.Forms.ComboBox()
        Me.LabelName = New System.Windows.Forms.Label()
        Me.GroupBoxCross = New System.Windows.Forms.GroupBox()
        Me.ComboPointStyle = New System.Windows.Forms.ComboBox()
        Me.ButtonColorPoint = New System.Windows.Forms.Button()
        Me.NumericEditTextSize = New NationalInstruments.UI.WindowsForms.NumericEdit()
        Me.LabelStyle = New System.Windows.Forms.Label()
        Me.LabelSize = New System.Windows.Forms.Label()
        Me.GroupeBoxLine = New System.Windows.Forms.GroupBox()
        Me.ComBoxLineStyle = New System.Windows.Forms.ComboBox()
        Me.NumericEditTextWidth = New NationalInstruments.UI.WindowsForms.NumericEdit()
        Me.ButtonColorLine = New System.Windows.Forms.Button()
        Me.LabelLineWight = New System.Windows.Forms.Label()
        Me.LabelStyle2 = New System.Windows.Forms.Label()
        Me.GroupBoxTextCursor = New System.Windows.Forms.GroupBox()
        Me.NumericEditTextBackTransparency = New NationalInstruments.UI.WindowsForms.NumericEdit()
        Me.CheckShowPosition = New System.Windows.Forms.CheckBox()
        Me.CheckShowName = New System.Windows.Forms.CheckBox()
        Me.ButtonTextFont = New System.Windows.Forms.Button()
        Me.ButtonColorText = New System.Windows.Forms.Button()
        Me.ButtonTextBackColor = New System.Windows.Forms.Button()
        Me.LabelBackTransparency = New System.Windows.Forms.Label()
        Me.ImageList1 = New System.Windows.Forms.ImageList(Me.components)
        Me.ColorDialog1 = New System.Windows.Forms.ColorDialog()
        Me.FontDialog1 = New System.Windows.Forms.FontDialog()
        Me.MenuStripGraph = New System.Windows.Forms.MenuStrip()
        Me.MenuFile = New System.Windows.Forms.ToolStripMenuItem()
        Me.MenuPrintGraph = New System.Windows.Forms.ToolStripMenuItem()
        Me.MenuPrint3DEvolvent = New System.Windows.Forms.ToolStripMenuItem()
        Me.MenuPrint3DCylinder = New System.Windows.Forms.ToolStripMenuItem()
        Me.MenuSaveGraph = New System.Windows.Forms.ToolStripMenuItem()
        Me.MenuSave3DEvolvent = New System.Windows.Forms.ToolStripMenuItem()
        Me.MenuSave3DCylinder = New System.Windows.Forms.ToolStripMenuItem()
        Me.TabControlGrph.SuspendLayout()
        Me.TabPageGrph.SuspendLayout()
        Me.Panel3DGraf.SuspendLayout()
        CType(Me.CWGraph3DЦилиндр, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.PanelSettingCylinder.SuspendLayout()
        Me.GroupBoxVolume.SuspendLayout()
        Me.GroupBoxPlotStyles.SuspendLayout()
        Me.GroupBoxColor.SuspendLayout()
        Me.GroupBoxAxisColor.SuspendLayout()
        Me.TabPageSurface.SuspendLayout()
        CType(Me.AxCWGraph3DEvolvent, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.PanelSetting.SuspendLayout()
        Me.TabControlSurface.SuspendLayout()
        Me.TabPageSurface2.SuspendLayout()
        CType(Me.AxCWGraph3DПроэкция, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBoxPositionCursor.SuspendLayout()
        CType(Me.NumericEditColum, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.NumericEditRow, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.SlideAxis, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupeBoxProjection.SuspendLayout()
        CType(Me.TextTransparency, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBoxtransparency.SuspendLayout()
        CType(Me.SlideTransparentMax, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.SlideTransparentMean, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.SlideTransparentMin, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TabPageStyle.SuspendLayout()
        Me.GroupBoxPlotStylesContour.SuspendLayout()
        Me.TabPageCursor.SuspendLayout()
        Me.GroupBoxCursor.SuspendLayout()
        Me.GroupBoxCross.SuspendLayout()
        CType(Me.NumericEditTextSize, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupeBoxLine.SuspendLayout()
        CType(Me.NumericEditTextWidth, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBoxTextCursor.SuspendLayout()
        CType(Me.NumericEditTextBackTransparency, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.MenuStripGraph.SuspendLayout()
        Me.SuspendLayout()
        '
        'TabControlGrph
        '
        Me.TabControlGrph.Controls.Add(Me.TabPageGrph)
        Me.TabControlGrph.Controls.Add(Me.TabPageSurface)
        Me.TabControlGrph.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TabControlGrph.Location = New System.Drawing.Point(0, 24)
        Me.TabControlGrph.Name = "TabControlGrph"
        Me.TabControlGrph.SelectedIndex = 0
        Me.TabControlGrph.Size = New System.Drawing.Size(755, 693)
        Me.TabControlGrph.TabIndex = 0
        '
        'TabPageGrph
        '
        Me.TabPageGrph.Controls.Add(Me.Panel3DGraf)
        Me.TabPageGrph.Controls.Add(Me.PanelSettingCylinder)
        Me.TabPageGrph.Location = New System.Drawing.Point(4, 22)
        Me.TabPageGrph.Name = "TabPageGrph"
        Me.TabPageGrph.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPageGrph.Size = New System.Drawing.Size(747, 667)
        Me.TabPageGrph.TabIndex = 0
        Me.TabPageGrph.Text = "Цилиндр"
        Me.TabPageGrph.UseVisualStyleBackColor = True
        '
        'Panel3DGraf
        '
        Me.Panel3DGraf.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.Panel3DGraf.Controls.Add(Me.CWGraph3DЦилиндр)
        Me.Panel3DGraf.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel3DGraf.Location = New System.Drawing.Point(3, 3)
        Me.Panel3DGraf.Name = "Panel3DGraf"
        Me.Panel3DGraf.Size = New System.Drawing.Size(557, 661)
        Me.Panel3DGraf.TabIndex = 28
        '
        'CWGraph3DЦилиндр
        '
        Me.CWGraph3DЦилиндр.Location = New System.Drawing.Point(0, 0)
        Me.CWGraph3DЦилиндр.Name = "CWGraph3DЦилиндр"
        Me.CWGraph3DЦилиндр.OcxState = CType(resources.GetObject("CWGraph3DЦилиндр.OcxState"), System.Windows.Forms.AxHost.State)
        Me.CWGraph3DЦилиндр.Size = New System.Drawing.Size(549, 529)
        Me.CWGraph3DЦилиндр.TabIndex = 22
        '
        'PanelSettingCylinder
        '
        Me.PanelSettingCylinder.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.PanelSettingCylinder.Controls.Add(Me.GroupBoxVolume)
        Me.PanelSettingCylinder.Controls.Add(Me.ComboBoxBurner)
        Me.PanelSettingCylinder.Controls.Add(Me.CheckBoxNumber)
        Me.PanelSettingCylinder.Controls.Add(Me.GroupBoxPlotStyles)
        Me.PanelSettingCylinder.Controls.Add(Me.LabelViewBurner)
        Me.PanelSettingCylinder.Controls.Add(Me.LabelHelp)
        Me.PanelSettingCylinder.Controls.Add(Me.GroupBoxColor)
        Me.PanelSettingCylinder.Controls.Add(Me.CheckBoxShell)
        Me.PanelSettingCylinder.Controls.Add(Me.GroupBoxAxisColor)
        Me.PanelSettingCylinder.Controls.Add(Me.CheckBoxMarking)
        Me.PanelSettingCylinder.Dock = System.Windows.Forms.DockStyle.Right
        Me.PanelSettingCylinder.Location = New System.Drawing.Point(560, 3)
        Me.PanelSettingCylinder.Name = "PanelSettingCylinder"
        Me.PanelSettingCylinder.Size = New System.Drawing.Size(184, 661)
        Me.PanelSettingCylinder.TabIndex = 27
        '
        'GroupBoxVolume
        '
        Me.GroupBoxVolume.BackColor = System.Drawing.SystemColors.Control
        Me.GroupBoxVolume.Controls.Add(Me.RadioButtonViewFlatness)
        Me.GroupBoxVolume.Controls.Add(Me.RadioButtonViewVolume)
        Me.GroupBoxVolume.ForeColor = System.Drawing.SystemColors.ControlText
        Me.GroupBoxVolume.Location = New System.Drawing.Point(8, 196)
        Me.GroupBoxVolume.Name = "GroupBoxVolume"
        Me.GroupBoxVolume.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.GroupBoxVolume.Size = New System.Drawing.Size(105, 74)
        Me.GroupBoxVolume.TabIndex = 25
        Me.GroupBoxVolume.TabStop = False
        Me.GroupBoxVolume.Text = "Вид"
        '
        'RadioButtonViewFlatness
        '
        Me.RadioButtonViewFlatness.Appearance = System.Windows.Forms.Appearance.Button
        Me.RadioButtonViewFlatness.BackColor = System.Drawing.SystemColors.Control
        Me.RadioButtonViewFlatness.Cursor = System.Windows.Forms.Cursors.Default
        Me.RadioButtonViewFlatness.ForeColor = System.Drawing.SystemColors.ControlText
        Me.RadioButtonViewFlatness.Location = New System.Drawing.Point(8, 43)
        Me.RadioButtonViewFlatness.Name = "RadioButtonViewFlatness"
        Me.RadioButtonViewFlatness.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.RadioButtonViewFlatness.Size = New System.Drawing.Size(89, 24)
        Me.RadioButtonViewFlatness.TabIndex = 27
        Me.RadioButtonViewFlatness.TabStop = True
        Me.RadioButtonViewFlatness.Text = "Плоскость"
        Me.RadioButtonViewFlatness.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.RadioButtonViewFlatness.UseVisualStyleBackColor = False
        '
        'RadioButtonViewVolume
        '
        Me.RadioButtonViewVolume.Appearance = System.Windows.Forms.Appearance.Button
        Me.RadioButtonViewVolume.BackColor = System.Drawing.SystemColors.Control
        Me.RadioButtonViewVolume.Checked = True
        Me.RadioButtonViewVolume.Cursor = System.Windows.Forms.Cursors.Default
        Me.RadioButtonViewVolume.ForeColor = System.Drawing.SystemColors.ControlText
        Me.RadioButtonViewVolume.Location = New System.Drawing.Point(8, 19)
        Me.RadioButtonViewVolume.Name = "RadioButtonViewVolume"
        Me.RadioButtonViewVolume.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.RadioButtonViewVolume.Size = New System.Drawing.Size(89, 24)
        Me.RadioButtonViewVolume.TabIndex = 26
        Me.RadioButtonViewVolume.TabStop = True
        Me.RadioButtonViewVolume.Text = "Объём"
        Me.RadioButtonViewVolume.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.RadioButtonViewVolume.UseVisualStyleBackColor = False
        '
        'ComboBoxBurner
        '
        Me.ComboBoxBurner.BackColor = System.Drawing.SystemColors.Window
        Me.ComboBoxBurner.Cursor = System.Windows.Forms.Cursors.Default
        Me.ComboBoxBurner.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.ComboBoxBurner.ForeColor = System.Drawing.SystemColors.WindowText
        Me.ComboBoxBurner.Location = New System.Drawing.Point(16, 368)
        Me.ComboBoxBurner.Name = "ComboBoxBurner"
        Me.ComboBoxBurner.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.ComboBoxBurner.Size = New System.Drawing.Size(89, 21)
        Me.ComboBoxBurner.TabIndex = 21
        '
        'CheckBoxNumber
        '
        Me.CheckBoxNumber.BackColor = System.Drawing.Color.Transparent
        Me.CheckBoxNumber.Cursor = System.Windows.Forms.Cursors.Default
        Me.CheckBoxNumber.ForeColor = System.Drawing.SystemColors.ControlText
        Me.CheckBoxNumber.Location = New System.Drawing.Point(16, 327)
        Me.CheckBoxNumber.Name = "CheckBoxNumber"
        Me.CheckBoxNumber.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.CheckBoxNumber.Size = New System.Drawing.Size(141, 29)
        Me.CheckBoxNumber.TabIndex = 11
        Me.CheckBoxNumber.Text = "Номера горелок"
        Me.CheckBoxNumber.UseVisualStyleBackColor = False
        '
        'GroupBoxPlotStyles
        '
        Me.GroupBoxPlotStyles.BackColor = System.Drawing.SystemColors.Control
        Me.GroupBoxPlotStyles.Controls.Add(Me.RadioButtonPoint)
        Me.GroupBoxPlotStyles.Controls.Add(Me.RadioButtonLine)
        Me.GroupBoxPlotStyles.Controls.Add(Me.RadioButtonSurface)
        Me.GroupBoxPlotStyles.Controls.Add(Me.RadioButtonContour)
        Me.GroupBoxPlotStyles.Controls.Add(Me.RadioButtonLinePoint)
        Me.GroupBoxPlotStyles.Controls.Add(Me.RadioButtonHiddenLine)
        Me.GroupBoxPlotStyles.Controls.Add(Me.RadioButtonSurfaceLine)
        Me.GroupBoxPlotStyles.Controls.Add(Me.RadioButtonSurfaceContour)
        Me.GroupBoxPlotStyles.Controls.Add(Me.RadioButtonSurfaceNormal)
        Me.GroupBoxPlotStyles.ForeColor = System.Drawing.SystemColors.ControlText
        Me.GroupBoxPlotStyles.Location = New System.Drawing.Point(8, 392)
        Me.GroupBoxPlotStyles.Name = "GroupBoxPlotStyles"
        Me.GroupBoxPlotStyles.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.GroupBoxPlotStyles.Size = New System.Drawing.Size(169, 249)
        Me.GroupBoxPlotStyles.TabIndex = 38
        Me.GroupBoxPlotStyles.TabStop = False
        Me.GroupBoxPlotStyles.Text = "Стиль контура"
        '
        'RadioButtonPoint
        '
        Me.RadioButtonPoint.BackColor = System.Drawing.SystemColors.Control
        Me.RadioButtonPoint.Cursor = System.Windows.Forms.Cursors.Default
        Me.RadioButtonPoint.ForeColor = System.Drawing.SystemColors.ControlText
        Me.RadioButtonPoint.Location = New System.Drawing.Point(16, 24)
        Me.RadioButtonPoint.Name = "RadioButtonPoint"
        Me.RadioButtonPoint.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.RadioButtonPoint.Size = New System.Drawing.Size(57, 21)
        Me.RadioButtonPoint.TabIndex = 47
        Me.RadioButtonPoint.TabStop = True
        Me.RadioButtonPoint.Text = "Точка"
        Me.RadioButtonPoint.UseVisualStyleBackColor = False
        '
        'RadioButtonLine
        '
        Me.RadioButtonLine.BackColor = System.Drawing.SystemColors.Control
        Me.RadioButtonLine.Cursor = System.Windows.Forms.Cursors.Default
        Me.RadioButtonLine.ForeColor = System.Drawing.SystemColors.ControlText
        Me.RadioButtonLine.Location = New System.Drawing.Point(16, 48)
        Me.RadioButtonLine.Name = "RadioButtonLine"
        Me.RadioButtonLine.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.RadioButtonLine.Size = New System.Drawing.Size(57, 17)
        Me.RadioButtonLine.TabIndex = 46
        Me.RadioButtonLine.TabStop = True
        Me.RadioButtonLine.Text = "Линия"
        Me.RadioButtonLine.UseVisualStyleBackColor = False
        '
        'RadioButtonSurface
        '
        Me.RadioButtonSurface.BackColor = System.Drawing.SystemColors.Control
        Me.RadioButtonSurface.Checked = True
        Me.RadioButtonSurface.Cursor = System.Windows.Forms.Cursors.Default
        Me.RadioButtonSurface.ForeColor = System.Drawing.SystemColors.ControlText
        Me.RadioButtonSurface.Location = New System.Drawing.Point(16, 120)
        Me.RadioButtonSurface.Name = "RadioButtonSurface"
        Me.RadioButtonSurface.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.RadioButtonSurface.Size = New System.Drawing.Size(125, 17)
        Me.RadioButtonSurface.TabIndex = 45
        Me.RadioButtonSurface.TabStop = True
        Me.RadioButtonSurface.Text = "Поверхность"
        Me.RadioButtonSurface.UseVisualStyleBackColor = False
        '
        'RadioButtonContour
        '
        Me.RadioButtonContour.BackColor = System.Drawing.SystemColors.Control
        Me.RadioButtonContour.Cursor = System.Windows.Forms.Cursors.Default
        Me.RadioButtonContour.ForeColor = System.Drawing.SystemColors.ControlText
        Me.RadioButtonContour.Location = New System.Drawing.Point(16, 192)
        Me.RadioButtonContour.Name = "RadioButtonContour"
        Me.RadioButtonContour.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.RadioButtonContour.Size = New System.Drawing.Size(89, 17)
        Me.RadioButtonContour.TabIndex = 44
        Me.RadioButtonContour.TabStop = True
        Me.RadioButtonContour.Text = "Контур"
        Me.RadioButtonContour.UseVisualStyleBackColor = False
        '
        'RadioButtonLinePoint
        '
        Me.RadioButtonLinePoint.BackColor = System.Drawing.SystemColors.Control
        Me.RadioButtonLinePoint.Cursor = System.Windows.Forms.Cursors.Default
        Me.RadioButtonLinePoint.ForeColor = System.Drawing.SystemColors.ControlText
        Me.RadioButtonLinePoint.Location = New System.Drawing.Point(16, 72)
        Me.RadioButtonLinePoint.Name = "RadioButtonLinePoint"
        Me.RadioButtonLinePoint.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.RadioButtonLinePoint.Size = New System.Drawing.Size(89, 17)
        Me.RadioButtonLinePoint.TabIndex = 43
        Me.RadioButtonLinePoint.TabStop = True
        Me.RadioButtonLinePoint.Text = "Линия-точка"
        Me.RadioButtonLinePoint.UseVisualStyleBackColor = False
        '
        'RadioButtonHiddenLine
        '
        Me.RadioButtonHiddenLine.BackColor = System.Drawing.SystemColors.Control
        Me.RadioButtonHiddenLine.Cursor = System.Windows.Forms.Cursors.Default
        Me.RadioButtonHiddenLine.ForeColor = System.Drawing.SystemColors.ControlText
        Me.RadioButtonHiddenLine.Location = New System.Drawing.Point(16, 96)
        Me.RadioButtonHiddenLine.Name = "RadioButtonHiddenLine"
        Me.RadioButtonHiddenLine.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.RadioButtonHiddenLine.Size = New System.Drawing.Size(121, 17)
        Me.RadioButtonHiddenLine.TabIndex = 42
        Me.RadioButtonHiddenLine.TabStop = True
        Me.RadioButtonHiddenLine.Text = "Невидимая линия"
        Me.RadioButtonHiddenLine.UseVisualStyleBackColor = False
        '
        'RadioButtonSurfaceLine
        '
        Me.RadioButtonSurfaceLine.BackColor = System.Drawing.SystemColors.Control
        Me.RadioButtonSurfaceLine.Cursor = System.Windows.Forms.Cursors.Default
        Me.RadioButtonSurfaceLine.ForeColor = System.Drawing.SystemColors.ControlText
        Me.RadioButtonSurfaceLine.Location = New System.Drawing.Point(16, 144)
        Me.RadioButtonSurfaceLine.Name = "RadioButtonSurfaceLine"
        Me.RadioButtonSurfaceLine.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.RadioButtonSurfaceLine.Size = New System.Drawing.Size(125, 17)
        Me.RadioButtonSurfaceLine.TabIndex = 41
        Me.RadioButtonSurfaceLine.TabStop = True
        Me.RadioButtonSurfaceLine.Text = "Поверхность-линия"
        Me.RadioButtonSurfaceLine.UseVisualStyleBackColor = False
        '
        'RadioButtonSurfaceContour
        '
        Me.RadioButtonSurfaceContour.BackColor = System.Drawing.SystemColors.Control
        Me.RadioButtonSurfaceContour.Cursor = System.Windows.Forms.Cursors.Default
        Me.RadioButtonSurfaceContour.ForeColor = System.Drawing.SystemColors.ControlText
        Me.RadioButtonSurfaceContour.Location = New System.Drawing.Point(16, 216)
        Me.RadioButtonSurfaceContour.Name = "RadioButtonSurfaceContour"
        Me.RadioButtonSurfaceContour.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.RadioButtonSurfaceContour.Size = New System.Drawing.Size(133, 17)
        Me.RadioButtonSurfaceContour.TabIndex = 40
        Me.RadioButtonSurfaceContour.TabStop = True
        Me.RadioButtonSurfaceContour.Text = "Поверхность-контур"
        Me.RadioButtonSurfaceContour.UseVisualStyleBackColor = False
        '
        'RadioButtonSurfaceNormal
        '
        Me.RadioButtonSurfaceNormal.BackColor = System.Drawing.SystemColors.Control
        Me.RadioButtonSurfaceNormal.Cursor = System.Windows.Forms.Cursors.Default
        Me.RadioButtonSurfaceNormal.ForeColor = System.Drawing.SystemColors.ControlText
        Me.RadioButtonSurfaceNormal.Location = New System.Drawing.Point(16, 168)
        Me.RadioButtonSurfaceNormal.Name = "RadioButtonSurfaceNormal"
        Me.RadioButtonSurfaceNormal.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.RadioButtonSurfaceNormal.Size = New System.Drawing.Size(149, 17)
        Me.RadioButtonSurfaceNormal.TabIndex = 39
        Me.RadioButtonSurfaceNormal.TabStop = True
        Me.RadioButtonSurfaceNormal.Text = "Нормаль к поверхности"
        Me.RadioButtonSurfaceNormal.UseVisualStyleBackColor = False
        '
        'LabelViewBurner
        '
        Me.LabelViewBurner.BackColor = System.Drawing.Color.Transparent
        Me.LabelViewBurner.Cursor = System.Windows.Forms.Cursors.Default
        Me.LabelViewBurner.ForeColor = System.Drawing.SystemColors.ControlText
        Me.LabelViewBurner.Location = New System.Drawing.Point(8, 352)
        Me.LabelViewBurner.Name = "LabelViewBurner"
        Me.LabelViewBurner.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.LabelViewBurner.Size = New System.Drawing.Size(105, 17)
        Me.LabelViewBurner.TabIndex = 35
        Me.LabelViewBurner.Text = "Показать горелку"
        '
        'LabelHelp
        '
        Me.LabelHelp.BackColor = System.Drawing.Color.Transparent
        Me.LabelHelp.Cursor = System.Windows.Forms.Cursors.Default
        Me.LabelHelp.ForeColor = System.Drawing.SystemColors.ControlText
        Me.LabelHelp.Location = New System.Drawing.Point(16, 648)
        Me.LabelHelp.Name = "LabelHelp"
        Me.LabelHelp.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.LabelHelp.Size = New System.Drawing.Size(161, 29)
        Me.LabelHelp.TabIndex = 36
        Me.LabelHelp.Text = "Пользоваться клавишами Alt и Shift совместно с мышью"
        Me.LabelHelp.TextAlign = System.Drawing.ContentAlignment.TopCenter
        '
        'GroupBoxColor
        '
        Me.GroupBoxColor.BackColor = System.Drawing.SystemColors.Control
        Me.GroupBoxColor.Controls.Add(Me.RadioButtonNullColor)
        Me.GroupBoxColor.Controls.Add(Me.RadioButtonRed)
        Me.GroupBoxColor.Controls.Add(Me.RadioButtonRainbow)
        Me.GroupBoxColor.ForeColor = System.Drawing.SystemColors.ControlText
        Me.GroupBoxColor.Location = New System.Drawing.Point(8, 8)
        Me.GroupBoxColor.Name = "GroupBoxColor"
        Me.GroupBoxColor.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.GroupBoxColor.Size = New System.Drawing.Size(105, 97)
        Me.GroupBoxColor.TabIndex = 31
        Me.GroupBoxColor.TabStop = False
        Me.GroupBoxColor.Text = "Цвет"
        '
        'RadioButtonNullColor
        '
        Me.RadioButtonNullColor.Appearance = System.Windows.Forms.Appearance.Button
        Me.RadioButtonNullColor.BackColor = System.Drawing.SystemColors.Control
        Me.RadioButtonNullColor.Cursor = System.Windows.Forms.Cursors.Default
        Me.RadioButtonNullColor.ForeColor = System.Drawing.SystemColors.ControlText
        Me.RadioButtonNullColor.Location = New System.Drawing.Point(8, 65)
        Me.RadioButtonNullColor.Name = "RadioButtonNullColor"
        Me.RadioButtonNullColor.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.RadioButtonNullColor.Size = New System.Drawing.Size(89, 24)
        Me.RadioButtonNullColor.TabIndex = 34
        Me.RadioButtonNullColor.TabStop = True
        Me.RadioButtonNullColor.Tag = "3"
        Me.RadioButtonNullColor.Text = "Нет"
        Me.RadioButtonNullColor.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.RadioButtonNullColor.UseVisualStyleBackColor = False
        '
        'RadioButtonRed
        '
        Me.RadioButtonRed.Appearance = System.Windows.Forms.Appearance.Button
        Me.RadioButtonRed.BackColor = System.Drawing.SystemColors.Control
        Me.RadioButtonRed.Cursor = System.Windows.Forms.Cursors.Default
        Me.RadioButtonRed.ForeColor = System.Drawing.SystemColors.ControlText
        Me.RadioButtonRed.Location = New System.Drawing.Point(8, 41)
        Me.RadioButtonRed.Name = "RadioButtonRed"
        Me.RadioButtonRed.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.RadioButtonRed.Size = New System.Drawing.Size(89, 24)
        Me.RadioButtonRed.TabIndex = 33
        Me.RadioButtonRed.TabStop = True
        Me.RadioButtonRed.Tag = "2"
        Me.RadioButtonRed.Text = "Красный"
        Me.RadioButtonRed.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.RadioButtonRed.UseVisualStyleBackColor = False
        '
        'RadioButtonRainbow
        '
        Me.RadioButtonRainbow.Appearance = System.Windows.Forms.Appearance.Button
        Me.RadioButtonRainbow.BackColor = System.Drawing.SystemColors.Control
        Me.RadioButtonRainbow.Checked = True
        Me.RadioButtonRainbow.Cursor = System.Windows.Forms.Cursors.Default
        Me.RadioButtonRainbow.ForeColor = System.Drawing.SystemColors.ControlText
        Me.RadioButtonRainbow.Location = New System.Drawing.Point(8, 17)
        Me.RadioButtonRainbow.Name = "RadioButtonRainbow"
        Me.RadioButtonRainbow.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.RadioButtonRainbow.Size = New System.Drawing.Size(89, 24)
        Me.RadioButtonRainbow.TabIndex = 32
        Me.RadioButtonRainbow.TabStop = True
        Me.RadioButtonRainbow.Tag = "1"
        Me.RadioButtonRainbow.Text = "Радуга"
        Me.RadioButtonRainbow.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.RadioButtonRainbow.UseVisualStyleBackColor = False
        '
        'CheckBoxShell
        '
        Me.CheckBoxShell.BackColor = System.Drawing.Color.Transparent
        Me.CheckBoxShell.Checked = True
        Me.CheckBoxShell.CheckState = System.Windows.Forms.CheckState.Checked
        Me.CheckBoxShell.Cursor = System.Windows.Forms.Cursors.Default
        Me.CheckBoxShell.ForeColor = System.Drawing.SystemColors.ControlText
        Me.CheckBoxShell.Location = New System.Drawing.Point(16, 276)
        Me.CheckBoxShell.Name = "CheckBoxShell"
        Me.CheckBoxShell.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.CheckBoxShell.Size = New System.Drawing.Size(73, 17)
        Me.CheckBoxShell.TabIndex = 23
        Me.CheckBoxShell.Text = "Стенки"
        Me.CheckBoxShell.UseVisualStyleBackColor = False
        '
        'GroupBoxAxisColor
        '
        Me.GroupBoxAxisColor.BackColor = System.Drawing.SystemColors.Control
        Me.GroupBoxAxisColor.Controls.Add(Me.RadioButtonExpanded)
        Me.GroupBoxAxisColor.Controls.Add(Me.RadioButtonNormal)
        Me.GroupBoxAxisColor.ForeColor = System.Drawing.SystemColors.ControlText
        Me.GroupBoxAxisColor.Location = New System.Drawing.Point(8, 112)
        Me.GroupBoxAxisColor.Name = "GroupBoxAxisColor"
        Me.GroupBoxAxisColor.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.GroupBoxAxisColor.Size = New System.Drawing.Size(105, 78)
        Me.GroupBoxAxisColor.TabIndex = 28
        Me.GroupBoxAxisColor.TabStop = False
        Me.GroupBoxAxisColor.Text = "Ось темпер."
        '
        'RadioButtonExpanded
        '
        Me.RadioButtonExpanded.Appearance = System.Windows.Forms.Appearance.Button
        Me.RadioButtonExpanded.BackColor = System.Drawing.SystemColors.Control
        Me.RadioButtonExpanded.Cursor = System.Windows.Forms.Cursors.Default
        Me.RadioButtonExpanded.ForeColor = System.Drawing.SystemColors.ControlText
        Me.RadioButtonExpanded.Location = New System.Drawing.Point(8, 43)
        Me.RadioButtonExpanded.Name = "RadioButtonExpanded"
        Me.RadioButtonExpanded.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.RadioButtonExpanded.Size = New System.Drawing.Size(89, 24)
        Me.RadioButtonExpanded.TabIndex = 30
        Me.RadioButtonExpanded.TabStop = True
        Me.RadioButtonExpanded.Text = "Растянутая"
        Me.RadioButtonExpanded.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.RadioButtonExpanded.UseVisualStyleBackColor = False
        '
        'RadioButtonNormal
        '
        Me.RadioButtonNormal.Appearance = System.Windows.Forms.Appearance.Button
        Me.RadioButtonNormal.BackColor = System.Drawing.SystemColors.Control
        Me.RadioButtonNormal.Checked = True
        Me.RadioButtonNormal.Cursor = System.Windows.Forms.Cursors.Default
        Me.RadioButtonNormal.ForeColor = System.Drawing.SystemColors.ControlText
        Me.RadioButtonNormal.Location = New System.Drawing.Point(8, 19)
        Me.RadioButtonNormal.Name = "RadioButtonNormal"
        Me.RadioButtonNormal.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.RadioButtonNormal.Size = New System.Drawing.Size(89, 24)
        Me.RadioButtonNormal.TabIndex = 29
        Me.RadioButtonNormal.TabStop = True
        Me.RadioButtonNormal.Text = "Обычная"
        Me.RadioButtonNormal.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.RadioButtonNormal.UseVisualStyleBackColor = False
        '
        'CheckBoxMarking
        '
        Me.CheckBoxMarking.BackColor = System.Drawing.Color.Transparent
        Me.CheckBoxMarking.Cursor = System.Windows.Forms.Cursors.Default
        Me.CheckBoxMarking.ForeColor = System.Drawing.SystemColors.ControlText
        Me.CheckBoxMarking.Location = New System.Drawing.Point(16, 299)
        Me.CheckBoxMarking.Name = "CheckBoxMarking"
        Me.CheckBoxMarking.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.CheckBoxMarking.Size = New System.Drawing.Size(141, 29)
        Me.CheckBoxMarking.TabIndex = 22
        Me.CheckBoxMarking.Text = "Разметка горелок"
        Me.CheckBoxMarking.UseVisualStyleBackColor = False
        '
        'TabPageSurface
        '
        Me.TabPageSurface.Controls.Add(Me.AxCWGraph3DEvolvent)
        Me.TabPageSurface.Controls.Add(Me.PanelSetting)
        Me.TabPageSurface.Location = New System.Drawing.Point(4, 22)
        Me.TabPageSurface.Name = "TabPageSurface"
        Me.TabPageSurface.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPageSurface.Size = New System.Drawing.Size(747, 667)
        Me.TabPageSurface.TabIndex = 1
        Me.TabPageSurface.Text = "Поверхность"
        Me.TabPageSurface.UseVisualStyleBackColor = True
        '
        'AxCWGraph3DEvolvent
        '
        Me.AxCWGraph3DEvolvent.Dock = System.Windows.Forms.DockStyle.Fill
        Me.AxCWGraph3DEvolvent.Location = New System.Drawing.Point(3, 3)
        Me.AxCWGraph3DEvolvent.Name = "AxCWGraph3DEvolvent"
        Me.AxCWGraph3DEvolvent.OcxState = CType(resources.GetObject("AxCWGraph3DEvolvent.OcxState"), System.Windows.Forms.AxHost.State)
        Me.AxCWGraph3DEvolvent.Size = New System.Drawing.Size(451, 661)
        Me.AxCWGraph3DEvolvent.TabIndex = 83
        '
        'PanelSetting
        '
        Me.PanelSetting.AutoScroll = True
        Me.PanelSetting.Controls.Add(Me.LabelSurface)
        Me.PanelSetting.Controls.Add(Me.ComboPlotsList)
        Me.PanelSetting.Controls.Add(Me.TabControlSurface)
        Me.PanelSetting.Dock = System.Windows.Forms.DockStyle.Right
        Me.PanelSetting.Location = New System.Drawing.Point(454, 3)
        Me.PanelSetting.Name = "PanelSetting"
        Me.PanelSetting.Size = New System.Drawing.Size(290, 661)
        Me.PanelSetting.TabIndex = 84
        '
        'LabelSurface
        '
        Me.LabelSurface.BackColor = System.Drawing.Color.Transparent
        Me.LabelSurface.Cursor = System.Windows.Forms.Cursors.Default
        Me.LabelSurface.ForeColor = System.Drawing.SystemColors.ControlText
        Me.LabelSurface.Location = New System.Drawing.Point(3, 12)
        Me.LabelSurface.Name = "LabelSurface"
        Me.LabelSurface.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.LabelSurface.Size = New System.Drawing.Size(122, 21)
        Me.LabelSurface.TabIndex = 45
        Me.LabelSurface.Text = "Текущая поверхность"
        '
        'ComboPlotsList
        '
        Me.ComboPlotsList.BackColor = System.Drawing.SystemColors.Window
        Me.ComboPlotsList.Cursor = System.Windows.Forms.Cursors.Default
        Me.ComboPlotsList.ForeColor = System.Drawing.SystemColors.WindowText
        Me.ComboPlotsList.Location = New System.Drawing.Point(130, 12)
        Me.ComboPlotsList.Name = "ComboPlotsList"
        Me.ComboPlotsList.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.ComboPlotsList.Size = New System.Drawing.Size(137, 21)
        Me.ComboPlotsList.TabIndex = 38
        Me.ComboPlotsList.Text = "Combo1"
        '
        'TabControlSurface
        '
        Me.TabControlSurface.Controls.Add(Me.TabPageSurface2)
        Me.TabControlSurface.Controls.Add(Me.TabPageStyle)
        Me.TabControlSurface.Controls.Add(Me.TabPageCursor)
        Me.TabControlSurface.ImageList = Me.ImageList1
        Me.TabControlSurface.Location = New System.Drawing.Point(6, 36)
        Me.TabControlSurface.Name = "TabControlSurface"
        Me.TabControlSurface.SelectedIndex = 0
        Me.TabControlSurface.Size = New System.Drawing.Size(265, 785)
        Me.TabControlSurface.TabIndex = 77
        '
        'TabPageSurface2
        '
        Me.TabPageSurface2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.TabPageSurface2.Controls.Add(Me.AxCWGraph3DПроэкция)
        Me.TabPageSurface2.Controls.Add(Me.GroupBoxPositionCursor)
        Me.TabPageSurface2.Controls.Add(Me.SlideAxis)
        Me.TabPageSurface2.Controls.Add(Me.GroupeBoxProjection)
        Me.TabPageSurface2.Controls.Add(Me.GroupBoxtransparency)
        Me.TabPageSurface2.ImageIndex = 0
        Me.TabPageSurface2.Location = New System.Drawing.Point(4, 23)
        Me.TabPageSurface2.Name = "TabPageSurface2"
        Me.TabPageSurface2.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPageSurface2.Size = New System.Drawing.Size(257, 758)
        Me.TabPageSurface2.TabIndex = 0
        Me.TabPageSurface2.Text = "Поверхность"
        Me.TabPageSurface2.UseVisualStyleBackColor = True
        '
        'AxCWGraph3DПроэкция
        '
        Me.AxCWGraph3DПроэкция.Location = New System.Drawing.Point(6, 486)
        Me.AxCWGraph3DПроэкция.Name = "AxCWGraph3DПроэкция"
        Me.AxCWGraph3DПроэкция.OcxState = CType(resources.GetObject("AxCWGraph3DПроэкция.OcxState"), System.Windows.Forms.AxHost.State)
        Me.AxCWGraph3DПроэкция.Size = New System.Drawing.Size(241, 241)
        Me.AxCWGraph3DПроэкция.TabIndex = 72
        '
        'GroupBoxPositionCursor
        '
        Me.GroupBoxPositionCursor.BackColor = System.Drawing.SystemColors.Control
        Me.GroupBoxPositionCursor.Controls.Add(Me.NumericEditColum)
        Me.GroupBoxPositionCursor.Controls.Add(Me.NumericEditRow)
        Me.GroupBoxPositionCursor.Controls.Add(Me.TextZPosition)
        Me.GroupBoxPositionCursor.Controls.Add(Me.TextYPosition)
        Me.GroupBoxPositionCursor.Controls.Add(Me.TextXPosition)
        Me.GroupBoxPositionCursor.Controls.Add(Me.ComboSnapMode)
        Me.GroupBoxPositionCursor.Controls.Add(Me.LabelColumn)
        Me.GroupBoxPositionCursor.Controls.Add(Me.LabelRow)
        Me.GroupBoxPositionCursor.Controls.Add(Me.LabelZ)
        Me.GroupBoxPositionCursor.Controls.Add(Me.LabelY)
        Me.GroupBoxPositionCursor.Controls.Add(Me.LabelX)
        Me.GroupBoxPositionCursor.Controls.Add(Me.LabelRegimeLinkage)
        Me.GroupBoxPositionCursor.ForeColor = System.Drawing.SystemColors.ControlText
        Me.GroupBoxPositionCursor.Location = New System.Drawing.Point(6, 261)
        Me.GroupBoxPositionCursor.Name = "GroupBoxPositionCursor"
        Me.GroupBoxPositionCursor.Padding = New System.Windows.Forms.Padding(0)
        Me.GroupBoxPositionCursor.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.GroupBoxPositionCursor.Size = New System.Drawing.Size(241, 113)
        Me.GroupBoxPositionCursor.TabIndex = 61
        Me.GroupBoxPositionCursor.TabStop = False
        Me.GroupBoxPositionCursor.Text = "Позиция курсора"
        '
        'NumericEditColum
        '
        Me.NumericEditColum.Location = New System.Drawing.Point(159, 65)
        Me.NumericEditColum.Name = "NumericEditColum"
        Me.NumericEditColum.Size = New System.Drawing.Size(66, 20)
        Me.NumericEditColum.TabIndex = 63
        '
        'NumericEditRow
        '
        Me.NumericEditRow.Location = New System.Drawing.Point(159, 39)
        Me.NumericEditRow.Name = "NumericEditRow"
        Me.NumericEditRow.Size = New System.Drawing.Size(66, 20)
        Me.NumericEditRow.TabIndex = 62
        '
        'TextZPosition
        '
        Me.TextZPosition.AcceptsReturn = True
        Me.TextZPosition.BackColor = System.Drawing.SystemColors.Window
        Me.TextZPosition.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.TextZPosition.ForeColor = System.Drawing.SystemColors.WindowText
        Me.TextZPosition.Location = New System.Drawing.Point(28, 84)
        Me.TextZPosition.MaxLength = 0
        Me.TextZPosition.Name = "TextZPosition"
        Me.TextZPosition.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.TextZPosition.Size = New System.Drawing.Size(65, 20)
        Me.TextZPosition.TabIndex = 37
        Me.TextZPosition.Text = "Text4"
        '
        'TextYPosition
        '
        Me.TextYPosition.AcceptsReturn = True
        Me.TextYPosition.BackColor = System.Drawing.SystemColors.Window
        Me.TextYPosition.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.TextYPosition.ForeColor = System.Drawing.SystemColors.WindowText
        Me.TextYPosition.Location = New System.Drawing.Point(28, 60)
        Me.TextYPosition.MaxLength = 0
        Me.TextYPosition.Name = "TextYPosition"
        Me.TextYPosition.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.TextYPosition.Size = New System.Drawing.Size(65, 20)
        Me.TextYPosition.TabIndex = 36
        Me.TextYPosition.Text = "Text3"
        '
        'TextXPosition
        '
        Me.TextXPosition.AcceptsReturn = True
        Me.TextXPosition.BackColor = System.Drawing.SystemColors.Window
        Me.TextXPosition.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.TextXPosition.ForeColor = System.Drawing.SystemColors.WindowText
        Me.TextXPosition.Location = New System.Drawing.Point(28, 36)
        Me.TextXPosition.MaxLength = 0
        Me.TextXPosition.Name = "TextXPosition"
        Me.TextXPosition.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.TextXPosition.Size = New System.Drawing.Size(65, 20)
        Me.TextXPosition.TabIndex = 35
        Me.TextXPosition.Text = "Text2"
        '
        'ComboSnapMode
        '
        Me.ComboSnapMode.BackColor = System.Drawing.SystemColors.Window
        Me.ComboSnapMode.Cursor = System.Windows.Forms.Cursors.Default
        Me.ComboSnapMode.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.ComboSnapMode.ForeColor = System.Drawing.SystemColors.WindowText
        Me.ComboSnapMode.Location = New System.Drawing.Point(111, 12)
        Me.ComboSnapMode.Name = "ComboSnapMode"
        Me.ComboSnapMode.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.ComboSnapMode.Size = New System.Drawing.Size(114, 21)
        Me.ComboSnapMode.TabIndex = 34
        '
        'LabelColumn
        '
        Me.LabelColumn.BackColor = System.Drawing.Color.Transparent
        Me.LabelColumn.Cursor = System.Windows.Forms.Cursors.Default
        Me.LabelColumn.ForeColor = System.Drawing.SystemColors.ControlText
        Me.LabelColumn.Location = New System.Drawing.Point(109, 68)
        Me.LabelColumn.Name = "LabelColumn"
        Me.LabelColumn.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.LabelColumn.Size = New System.Drawing.Size(49, 17)
        Me.LabelColumn.TabIndex = 47
        Me.LabelColumn.Text = "Столбец"
        '
        'LabelRow
        '
        Me.LabelRow.BackColor = System.Drawing.SystemColors.Control
        Me.LabelRow.Cursor = System.Windows.Forms.Cursors.Default
        Me.LabelRow.ForeColor = System.Drawing.SystemColors.ControlText
        Me.LabelRow.Location = New System.Drawing.Point(109, 42)
        Me.LabelRow.Name = "LabelRow"
        Me.LabelRow.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.LabelRow.Size = New System.Drawing.Size(49, 17)
        Me.LabelRow.TabIndex = 46
        Me.LabelRow.Text = "Строка"
        '
        'LabelZ
        '
        Me.LabelZ.BackColor = System.Drawing.Color.Transparent
        Me.LabelZ.Cursor = System.Windows.Forms.Cursors.Default
        Me.LabelZ.ForeColor = System.Drawing.SystemColors.ControlText
        Me.LabelZ.Location = New System.Drawing.Point(4, 84)
        Me.LabelZ.Name = "LabelZ"
        Me.LabelZ.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.LabelZ.Size = New System.Drawing.Size(33, 17)
        Me.LabelZ.TabIndex = 44
        Me.LabelZ.Text = "Z"
        '
        'LabelY
        '
        Me.LabelY.BackColor = System.Drawing.Color.Transparent
        Me.LabelY.Cursor = System.Windows.Forms.Cursors.Default
        Me.LabelY.ForeColor = System.Drawing.SystemColors.ControlText
        Me.LabelY.Location = New System.Drawing.Point(4, 60)
        Me.LabelY.Name = "LabelY"
        Me.LabelY.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.LabelY.Size = New System.Drawing.Size(25, 17)
        Me.LabelY.TabIndex = 43
        Me.LabelY.Text = "Y"
        '
        'LabelX
        '
        Me.LabelX.BackColor = System.Drawing.Color.Transparent
        Me.LabelX.Cursor = System.Windows.Forms.Cursors.Default
        Me.LabelX.ForeColor = System.Drawing.SystemColors.ControlText
        Me.LabelX.Location = New System.Drawing.Point(4, 36)
        Me.LabelX.Name = "LabelX"
        Me.LabelX.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.LabelX.Size = New System.Drawing.Size(25, 17)
        Me.LabelX.TabIndex = 42
        Me.LabelX.Text = "X"
        '
        'LabelRegimeLinkage
        '
        Me.LabelRegimeLinkage.BackColor = System.Drawing.Color.Transparent
        Me.LabelRegimeLinkage.Cursor = System.Windows.Forms.Cursors.Default
        Me.LabelRegimeLinkage.ForeColor = System.Drawing.SystemColors.ControlText
        Me.LabelRegimeLinkage.Location = New System.Drawing.Point(8, 16)
        Me.LabelRegimeLinkage.Name = "LabelRegimeLinkage"
        Me.LabelRegimeLinkage.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.LabelRegimeLinkage.Size = New System.Drawing.Size(97, 17)
        Me.LabelRegimeLinkage.TabIndex = 41
        Me.LabelRegimeLinkage.Text = "Режим привязки"
        '
        'SlideAxis
        '
        Me.SlideAxis.BackColor = System.Drawing.SystemColors.Control
        Me.SlideAxis.Border = NationalInstruments.UI.Border.Raised
        Me.SlideAxis.Caption = "Ось Z"
        Me.SlideAxis.Location = New System.Drawing.Point(186, 6)
        Me.SlideAxis.Name = "SlideAxis"
        Me.SlideAxis.Size = New System.Drawing.Size(61, 249)
        Me.SlideAxis.TabIndex = 74
        '
        'GroupeBoxProjection
        '
        Me.GroupeBoxProjection.BackColor = System.Drawing.SystemColors.Control
        Me.GroupeBoxProjection.Controls.Add(Me.TextTransparency)
        Me.GroupeBoxProjection.Controls.Add(Me.LabelTransparency)
        Me.GroupeBoxProjection.Controls.Add(Me.CheckEnableProjection)
        Me.GroupeBoxProjection.Controls.Add(Me.CheckYZPlane)
        Me.GroupeBoxProjection.Controls.Add(Me.CheckXZPlane)
        Me.GroupeBoxProjection.Controls.Add(Me.CheckXYPlane)
        Me.GroupeBoxProjection.Controls.Add(Me.ButtonColorPlane)
        Me.GroupeBoxProjection.ForeColor = System.Drawing.SystemColors.ControlText
        Me.GroupeBoxProjection.Location = New System.Drawing.Point(6, 380)
        Me.GroupeBoxProjection.Name = "GroupeBoxProjection"
        Me.GroupeBoxProjection.Padding = New System.Windows.Forms.Padding(0)
        Me.GroupeBoxProjection.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.GroupeBoxProjection.Size = New System.Drawing.Size(241, 100)
        Me.GroupeBoxProjection.TabIndex = 52
        Me.GroupeBoxProjection.TabStop = False
        Me.GroupeBoxProjection.Text = "Проэкция поверхности"
        '
        'TextTransparency
        '
        Me.TextTransparency.CoercionInterval = 5.0R
        Me.TextTransparency.FormatMode = NationalInstruments.UI.NumericFormatMode.CreateSimpleDoubleMode(0)
        Me.TextTransparency.Location = New System.Drawing.Point(176, 13)
        Me.TextTransparency.Name = "TextTransparency"
        Me.TextTransparency.Range = New NationalInstruments.UI.Range(0R, 100.0R)
        Me.TextTransparency.Size = New System.Drawing.Size(57, 20)
        Me.TextTransparency.TabIndex = 79
        Me.TextTransparency.Value = 75.0R
        '
        'LabelTransparency
        '
        Me.LabelTransparency.BackColor = System.Drawing.Color.Transparent
        Me.LabelTransparency.Cursor = System.Windows.Forms.Cursors.Default
        Me.LabelTransparency.ForeColor = System.Drawing.SystemColors.ControlText
        Me.LabelTransparency.Location = New System.Drawing.Point(94, 16)
        Me.LabelTransparency.Name = "LabelTransparency"
        Me.LabelTransparency.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.LabelTransparency.Size = New System.Drawing.Size(83, 17)
        Me.LabelTransparency.TabIndex = 22
        Me.LabelTransparency.Text = "Прозрачность"
        '
        'CheckEnableProjection
        '
        Me.CheckEnableProjection.AutoSize = True
        Me.CheckEnableProjection.Location = New System.Drawing.Point(11, 16)
        Me.CheckEnableProjection.Name = "CheckEnableProjection"
        Me.CheckEnableProjection.Size = New System.Drawing.Size(82, 17)
        Me.CheckEnableProjection.TabIndex = 28
        Me.CheckEnableProjection.Text = "Разрешить"
        Me.CheckEnableProjection.UseVisualStyleBackColor = True
        '
        'CheckYZPlane
        '
        Me.CheckYZPlane.AutoSize = True
        Me.CheckYZPlane.Enabled = False
        Me.CheckYZPlane.Location = New System.Drawing.Point(22, 75)
        Me.CheckYZPlane.Name = "CheckYZPlane"
        Me.CheckYZPlane.Size = New System.Drawing.Size(92, 17)
        Me.CheckYZPlane.TabIndex = 27
        Me.CheckYZPlane.Text = "YZ Проэкция"
        Me.CheckYZPlane.UseVisualStyleBackColor = True
        '
        'CheckXZPlane
        '
        Me.CheckXZPlane.AutoSize = True
        Me.CheckXZPlane.Enabled = False
        Me.CheckXZPlane.Location = New System.Drawing.Point(22, 56)
        Me.CheckXZPlane.Name = "CheckXZPlane"
        Me.CheckXZPlane.Size = New System.Drawing.Size(92, 17)
        Me.CheckXZPlane.TabIndex = 26
        Me.CheckXZPlane.Text = "XZ Проэкция"
        Me.CheckXZPlane.UseVisualStyleBackColor = True
        '
        'CheckXYPlane
        '
        Me.CheckXYPlane.AutoSize = True
        Me.CheckXYPlane.Enabled = False
        Me.CheckXYPlane.Location = New System.Drawing.Point(22, 39)
        Me.CheckXYPlane.Name = "CheckXYPlane"
        Me.CheckXYPlane.Size = New System.Drawing.Size(92, 17)
        Me.CheckXYPlane.TabIndex = 25
        Me.CheckXYPlane.Text = "XY Проэкция"
        Me.CheckXYPlane.UseVisualStyleBackColor = True
        '
        'ButtonColorPlane
        '
        Me.ButtonColorPlane.BackColor = System.Drawing.SystemColors.Control
        Me.ButtonColorPlane.Cursor = System.Windows.Forms.Cursors.Default
        Me.ButtonColorPlane.ForeColor = System.Drawing.SystemColors.ControlText
        Me.ButtonColorPlane.Location = New System.Drawing.Point(152, 48)
        Me.ButtonColorPlane.Name = "ButtonColorPlane"
        Me.ButtonColorPlane.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.ButtonColorPlane.Size = New System.Drawing.Size(81, 25)
        Me.ButtonColorPlane.TabIndex = 24
        Me.ButtonColorPlane.Text = "Цвет"
        Me.ButtonColorPlane.UseVisualStyleBackColor = False
        '
        'GroupBoxtransparency
        '
        Me.GroupBoxtransparency.BackColor = System.Drawing.SystemColors.Control
        Me.GroupBoxtransparency.Controls.Add(Me.SlideTransparentMax)
        Me.GroupBoxtransparency.Controls.Add(Me.SlideTransparentMean)
        Me.GroupBoxtransparency.Controls.Add(Me.SlideTransparentMin)
        Me.GroupBoxtransparency.Location = New System.Drawing.Point(6, 6)
        Me.GroupBoxtransparency.Name = "GroupBoxtransparency"
        Me.GroupBoxtransparency.Size = New System.Drawing.Size(177, 249)
        Me.GroupBoxtransparency.TabIndex = 75
        Me.GroupBoxtransparency.TabStop = False
        Me.GroupBoxtransparency.Text = "Прозрачность поверхностей"
        '
        'SlideTransparentMax
        '
        Me.SlideTransparentMax.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.SlideTransparentMax.Border = NationalInstruments.UI.Border.Raised
        Me.SlideTransparentMax.Caption = "Максимальные"
        Me.SlideTransparentMax.Enabled = False
        Me.SlideTransparentMax.FillBackColor = System.Drawing.Color.LightSalmon
        Me.SlideTransparentMax.FillColor = System.Drawing.Color.Red
        Me.SlideTransparentMax.FillStyle = NationalInstruments.UI.FillStyle.HorizontalGradient
        Me.SlideTransparentMax.InvertedScale = True
        Me.SlideTransparentMax.Location = New System.Drawing.Point(6, 19)
        Me.SlideTransparentMax.Name = "SlideTransparentMax"
        Me.SlideTransparentMax.Range = New NationalInstruments.UI.Range(0R, 100.0R)
        Me.SlideTransparentMax.ScalePosition = NationalInstruments.UI.NumericScalePosition.Bottom
        Me.SlideTransparentMax.Size = New System.Drawing.Size(159, 69)
        Me.SlideTransparentMax.SlideStyle = NationalInstruments.UI.SlideStyle.SunkenWithGrip
        Me.SlideTransparentMax.TabIndex = 69
        '
        'SlideTransparentMean
        '
        Me.SlideTransparentMean.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.SlideTransparentMean.Border = NationalInstruments.UI.Border.Raised
        Me.SlideTransparentMean.Caption = "Средние"
        Me.SlideTransparentMean.FillBackColor = System.Drawing.Color.LightSalmon
        Me.SlideTransparentMean.FillColor = System.Drawing.Color.Red
        Me.SlideTransparentMean.FillStyle = NationalInstruments.UI.FillStyle.HorizontalGradient
        Me.SlideTransparentMean.InvertedScale = True
        Me.SlideTransparentMean.Location = New System.Drawing.Point(6, 94)
        Me.SlideTransparentMean.Name = "SlideTransparentMean"
        Me.SlideTransparentMean.Range = New NationalInstruments.UI.Range(0R, 100.0R)
        Me.SlideTransparentMean.ScalePosition = NationalInstruments.UI.NumericScalePosition.Bottom
        Me.SlideTransparentMean.Size = New System.Drawing.Size(159, 69)
        Me.SlideTransparentMean.SlideStyle = NationalInstruments.UI.SlideStyle.SunkenWithGrip
        Me.SlideTransparentMean.TabIndex = 70
        '
        'SlideTransparentMin
        '
        Me.SlideTransparentMin.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.SlideTransparentMin.Border = NationalInstruments.UI.Border.Raised
        Me.SlideTransparentMin.Caption = "Минимальные"
        Me.SlideTransparentMin.Enabled = False
        Me.SlideTransparentMin.FillBackColor = System.Drawing.Color.LightSalmon
        Me.SlideTransparentMin.FillColor = System.Drawing.Color.Red
        Me.SlideTransparentMin.FillStyle = NationalInstruments.UI.FillStyle.HorizontalGradient
        Me.SlideTransparentMin.InvertedScale = True
        Me.SlideTransparentMin.Location = New System.Drawing.Point(6, 169)
        Me.SlideTransparentMin.Name = "SlideTransparentMin"
        Me.SlideTransparentMin.Range = New NationalInstruments.UI.Range(0R, 100.0R)
        Me.SlideTransparentMin.ScalePosition = NationalInstruments.UI.NumericScalePosition.Bottom
        Me.SlideTransparentMin.Size = New System.Drawing.Size(159, 69)
        Me.SlideTransparentMin.SlideStyle = NationalInstruments.UI.SlideStyle.SunkenWithGrip
        Me.SlideTransparentMin.TabIndex = 71
        '
        'TabPageStyle
        '
        Me.TabPageStyle.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.TabPageStyle.Controls.Add(Me.GroupBoxPlotStylesContour)
        Me.TabPageStyle.Controls.Add(Me.CheckBoxFastDraw)
        Me.TabPageStyle.Controls.Add(Me.CheckBoxPerspective)
        Me.TabPageStyle.ImageIndex = 1
        Me.TabPageStyle.Location = New System.Drawing.Point(4, 23)
        Me.TabPageStyle.Name = "TabPageStyle"
        Me.TabPageStyle.Size = New System.Drawing.Size(257, 758)
        Me.TabPageStyle.TabIndex = 2
        Me.TabPageStyle.Text = "Стиль"
        Me.TabPageStyle.UseVisualStyleBackColor = True
        '
        'GroupBoxPlotStylesContour
        '
        Me.GroupBoxPlotStylesContour.BackColor = System.Drawing.SystemColors.Control
        Me.GroupBoxPlotStylesContour.Controls.Add(Me.RadioButtonSurfaceNormal2)
        Me.GroupBoxPlotStylesContour.Controls.Add(Me.RadioButtonSurfaceContour2)
        Me.GroupBoxPlotStylesContour.Controls.Add(Me.RadioButtonSurfaceLine2)
        Me.GroupBoxPlotStylesContour.Controls.Add(Me.RadioButtonHiddenLine2)
        Me.GroupBoxPlotStylesContour.Controls.Add(Me.RadioButtonLinePoint2)
        Me.GroupBoxPlotStylesContour.Controls.Add(Me.RadioButtonContour2)
        Me.GroupBoxPlotStylesContour.Controls.Add(Me.RadioButtonSurface2)
        Me.GroupBoxPlotStylesContour.Controls.Add(Me.RadioButtonLine2)
        Me.GroupBoxPlotStylesContour.Controls.Add(Me.RadioButtonPoint2)
        Me.GroupBoxPlotStylesContour.ForeColor = System.Drawing.SystemColors.ControlText
        Me.GroupBoxPlotStylesContour.Location = New System.Drawing.Point(3, 5)
        Me.GroupBoxPlotStylesContour.Name = "GroupBoxPlotStylesContour"
        Me.GroupBoxPlotStylesContour.Padding = New System.Windows.Forms.Padding(0)
        Me.GroupBoxPlotStylesContour.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.GroupBoxPlotStylesContour.Size = New System.Drawing.Size(247, 249)
        Me.GroupBoxPlotStylesContour.TabIndex = 62
        Me.GroupBoxPlotStylesContour.TabStop = False
        Me.GroupBoxPlotStylesContour.Text = "Стиль контура"
        '
        'RadioButtonSurfaceNormal2
        '
        Me.RadioButtonSurfaceNormal2.BackColor = System.Drawing.SystemColors.Control
        Me.RadioButtonSurfaceNormal2.Cursor = System.Windows.Forms.Cursors.Default
        Me.RadioButtonSurfaceNormal2.ForeColor = System.Drawing.SystemColors.ControlText
        Me.RadioButtonSurfaceNormal2.Location = New System.Drawing.Point(16, 168)
        Me.RadioButtonSurfaceNormal2.Name = "RadioButtonSurfaceNormal2"
        Me.RadioButtonSurfaceNormal2.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.RadioButtonSurfaceNormal2.Size = New System.Drawing.Size(155, 21)
        Me.RadioButtonSurfaceNormal2.TabIndex = 14
        Me.RadioButtonSurfaceNormal2.TabStop = True
        Me.RadioButtonSurfaceNormal2.Text = "Нормаль к поверхности"
        Me.RadioButtonSurfaceNormal2.UseVisualStyleBackColor = False
        '
        'RadioButtonSurfaceContour2
        '
        Me.RadioButtonSurfaceContour2.BackColor = System.Drawing.SystemColors.Control
        Me.RadioButtonSurfaceContour2.Cursor = System.Windows.Forms.Cursors.Default
        Me.RadioButtonSurfaceContour2.ForeColor = System.Drawing.SystemColors.ControlText
        Me.RadioButtonSurfaceContour2.Location = New System.Drawing.Point(16, 216)
        Me.RadioButtonSurfaceContour2.Name = "RadioButtonSurfaceContour2"
        Me.RadioButtonSurfaceContour2.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.RadioButtonSurfaceContour2.Size = New System.Drawing.Size(130, 21)
        Me.RadioButtonSurfaceContour2.TabIndex = 13
        Me.RadioButtonSurfaceContour2.TabStop = True
        Me.RadioButtonSurfaceContour2.Text = "Поверхность-контур"
        Me.RadioButtonSurfaceContour2.UseVisualStyleBackColor = False
        '
        'RadioButtonSurfaceLine2
        '
        Me.RadioButtonSurfaceLine2.BackColor = System.Drawing.SystemColors.Control
        Me.RadioButtonSurfaceLine2.Cursor = System.Windows.Forms.Cursors.Default
        Me.RadioButtonSurfaceLine2.ForeColor = System.Drawing.SystemColors.ControlText
        Me.RadioButtonSurfaceLine2.Location = New System.Drawing.Point(16, 144)
        Me.RadioButtonSurfaceLine2.Name = "RadioButtonSurfaceLine2"
        Me.RadioButtonSurfaceLine2.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.RadioButtonSurfaceLine2.Size = New System.Drawing.Size(130, 24)
        Me.RadioButtonSurfaceLine2.TabIndex = 12
        Me.RadioButtonSurfaceLine2.TabStop = True
        Me.RadioButtonSurfaceLine2.Text = "Поверхность-линия"
        Me.RadioButtonSurfaceLine2.UseVisualStyleBackColor = False
        '
        'RadioButtonHiddenLine2
        '
        Me.RadioButtonHiddenLine2.BackColor = System.Drawing.SystemColors.Control
        Me.RadioButtonHiddenLine2.Cursor = System.Windows.Forms.Cursors.Default
        Me.RadioButtonHiddenLine2.ForeColor = System.Drawing.SystemColors.ControlText
        Me.RadioButtonHiddenLine2.Location = New System.Drawing.Point(16, 96)
        Me.RadioButtonHiddenLine2.Name = "RadioButtonHiddenLine2"
        Me.RadioButtonHiddenLine2.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.RadioButtonHiddenLine2.Size = New System.Drawing.Size(130, 21)
        Me.RadioButtonHiddenLine2.TabIndex = 11
        Me.RadioButtonHiddenLine2.TabStop = True
        Me.RadioButtonHiddenLine2.Text = "Невидимая линия"
        Me.RadioButtonHiddenLine2.UseVisualStyleBackColor = False
        '
        'RadioButtonLinePoint2
        '
        Me.RadioButtonLinePoint2.BackColor = System.Drawing.SystemColors.Control
        Me.RadioButtonLinePoint2.Cursor = System.Windows.Forms.Cursors.Default
        Me.RadioButtonLinePoint2.ForeColor = System.Drawing.SystemColors.ControlText
        Me.RadioButtonLinePoint2.Location = New System.Drawing.Point(16, 71)
        Me.RadioButtonLinePoint2.Name = "RadioButtonLinePoint2"
        Me.RadioButtonLinePoint2.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.RadioButtonLinePoint2.Size = New System.Drawing.Size(130, 21)
        Me.RadioButtonLinePoint2.TabIndex = 10
        Me.RadioButtonLinePoint2.TabStop = True
        Me.RadioButtonLinePoint2.Text = "Линия-точка"
        Me.RadioButtonLinePoint2.UseVisualStyleBackColor = False
        '
        'RadioButtonContour2
        '
        Me.RadioButtonContour2.BackColor = System.Drawing.SystemColors.Control
        Me.RadioButtonContour2.Cursor = System.Windows.Forms.Cursors.Default
        Me.RadioButtonContour2.ForeColor = System.Drawing.SystemColors.ControlText
        Me.RadioButtonContour2.Location = New System.Drawing.Point(16, 192)
        Me.RadioButtonContour2.Name = "RadioButtonContour2"
        Me.RadioButtonContour2.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.RadioButtonContour2.Size = New System.Drawing.Size(130, 21)
        Me.RadioButtonContour2.TabIndex = 9
        Me.RadioButtonContour2.TabStop = True
        Me.RadioButtonContour2.Text = "Контур"
        Me.RadioButtonContour2.UseVisualStyleBackColor = False
        '
        'RadioButtonSurface2
        '
        Me.RadioButtonSurface2.BackColor = System.Drawing.SystemColors.Control
        Me.RadioButtonSurface2.Checked = True
        Me.RadioButtonSurface2.Cursor = System.Windows.Forms.Cursors.Default
        Me.RadioButtonSurface2.ForeColor = System.Drawing.SystemColors.ControlText
        Me.RadioButtonSurface2.Location = New System.Drawing.Point(16, 120)
        Me.RadioButtonSurface2.Name = "RadioButtonSurface2"
        Me.RadioButtonSurface2.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.RadioButtonSurface2.Size = New System.Drawing.Size(130, 21)
        Me.RadioButtonSurface2.TabIndex = 7
        Me.RadioButtonSurface2.TabStop = True
        Me.RadioButtonSurface2.Text = "Поверхность"
        Me.RadioButtonSurface2.UseVisualStyleBackColor = False
        '
        'RadioButtonLine2
        '
        Me.RadioButtonLine2.BackColor = System.Drawing.SystemColors.Control
        Me.RadioButtonLine2.Cursor = System.Windows.Forms.Cursors.Default
        Me.RadioButtonLine2.ForeColor = System.Drawing.SystemColors.ControlText
        Me.RadioButtonLine2.Location = New System.Drawing.Point(16, 48)
        Me.RadioButtonLine2.Name = "RadioButtonLine2"
        Me.RadioButtonLine2.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.RadioButtonLine2.Size = New System.Drawing.Size(130, 21)
        Me.RadioButtonLine2.TabIndex = 6
        Me.RadioButtonLine2.TabStop = True
        Me.RadioButtonLine2.Text = "Линия"
        Me.RadioButtonLine2.UseVisualStyleBackColor = False
        '
        'RadioButtonPoint2
        '
        Me.RadioButtonPoint2.BackColor = System.Drawing.SystemColors.Control
        Me.RadioButtonPoint2.Cursor = System.Windows.Forms.Cursors.Default
        Me.RadioButtonPoint2.ForeColor = System.Drawing.SystemColors.ControlText
        Me.RadioButtonPoint2.Location = New System.Drawing.Point(16, 24)
        Me.RadioButtonPoint2.Name = "RadioButtonPoint2"
        Me.RadioButtonPoint2.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.RadioButtonPoint2.Size = New System.Drawing.Size(130, 21)
        Me.RadioButtonPoint2.TabIndex = 5
        Me.RadioButtonPoint2.TabStop = True
        Me.RadioButtonPoint2.Text = "Точка"
        Me.RadioButtonPoint2.UseVisualStyleBackColor = False
        '
        'CheckBoxFastDraw
        '
        Me.CheckBoxFastDraw.BackColor = System.Drawing.Color.Transparent
        Me.CheckBoxFastDraw.Cursor = System.Windows.Forms.Cursors.Default
        Me.CheckBoxFastDraw.ForeColor = System.Drawing.SystemColors.ControlText
        Me.CheckBoxFastDraw.Location = New System.Drawing.Point(19, 269)
        Me.CheckBoxFastDraw.Name = "CheckBoxFastDraw"
        Me.CheckBoxFastDraw.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.CheckBoxFastDraw.Size = New System.Drawing.Size(147, 17)
        Me.CheckBoxFastDraw.TabIndex = 60
        Me.CheckBoxFastDraw.Text = "Быстрая перерисовка"
        Me.CheckBoxFastDraw.UseVisualStyleBackColor = False
        '
        'CheckBoxPerspective
        '
        Me.CheckBoxPerspective.BackColor = System.Drawing.Color.Transparent
        Me.CheckBoxPerspective.Cursor = System.Windows.Forms.Cursors.Default
        Me.CheckBoxPerspective.ForeColor = System.Drawing.SystemColors.ControlText
        Me.CheckBoxPerspective.Location = New System.Drawing.Point(19, 287)
        Me.CheckBoxPerspective.Name = "CheckBoxPerspective"
        Me.CheckBoxPerspective.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.CheckBoxPerspective.Size = New System.Drawing.Size(155, 17)
        Me.CheckBoxPerspective.TabIndex = 59
        Me.CheckBoxPerspective.Text = "Проэкция перпективы"
        Me.CheckBoxPerspective.UseVisualStyleBackColor = False
        '
        'TabPageCursor
        '
        Me.TabPageCursor.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.TabPageCursor.Controls.Add(Me.GroupBoxCursor)
        Me.TabPageCursor.Controls.Add(Me.GroupBoxCross)
        Me.TabPageCursor.Controls.Add(Me.GroupeBoxLine)
        Me.TabPageCursor.Controls.Add(Me.GroupBoxTextCursor)
        Me.TabPageCursor.ImageIndex = 2
        Me.TabPageCursor.Location = New System.Drawing.Point(4, 23)
        Me.TabPageCursor.Name = "TabPageCursor"
        Me.TabPageCursor.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPageCursor.Size = New System.Drawing.Size(257, 758)
        Me.TabPageCursor.TabIndex = 1
        Me.TabPageCursor.Text = "Курсор"
        Me.TabPageCursor.UseVisualStyleBackColor = True
        '
        'GroupBoxCursor
        '
        Me.GroupBoxCursor.BackColor = System.Drawing.SystemColors.Control
        Me.GroupBoxCursor.Controls.Add(Me.ButtonSetAllColors)
        Me.GroupBoxCursor.Controls.Add(Me.TextNameCursor)
        Me.GroupBoxCursor.Controls.Add(Me.CheckEnabled)
        Me.GroupBoxCursor.Controls.Add(Me.CheckVisible)
        Me.GroupBoxCursor.Controls.Add(Me.ComboCursors)
        Me.GroupBoxCursor.Controls.Add(Me.LabelName)
        Me.GroupBoxCursor.ForeColor = System.Drawing.SystemColors.ControlText
        Me.GroupBoxCursor.Location = New System.Drawing.Point(6, 6)
        Me.GroupBoxCursor.Name = "GroupBoxCursor"
        Me.GroupBoxCursor.Padding = New System.Windows.Forms.Padding(0)
        Me.GroupBoxCursor.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.GroupBoxCursor.Size = New System.Drawing.Size(241, 81)
        Me.GroupBoxCursor.TabIndex = 49
        Me.GroupBoxCursor.TabStop = False
        Me.GroupBoxCursor.Text = "Курсор"
        '
        'ButtonSetAllColors
        '
        Me.ButtonSetAllColors.BackColor = System.Drawing.SystemColors.Control
        Me.ButtonSetAllColors.Cursor = System.Windows.Forms.Cursors.Default
        Me.ButtonSetAllColors.ForeColor = System.Drawing.SystemColors.ControlText
        Me.ButtonSetAllColors.Location = New System.Drawing.Point(152, 48)
        Me.ButtonSetAllColors.Name = "ButtonSetAllColors"
        Me.ButtonSetAllColors.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.ButtonSetAllColors.Size = New System.Drawing.Size(81, 25)
        Me.ButtonSetAllColors.TabIndex = 54
        Me.ButtonSetAllColors.Text = "Цвет"
        Me.ButtonSetAllColors.UseVisualStyleBackColor = False
        '
        'TextNameCursor
        '
        Me.TextNameCursor.AcceptsReturn = True
        Me.TextNameCursor.BackColor = System.Drawing.SystemColors.Window
        Me.TextNameCursor.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.TextNameCursor.ForeColor = System.Drawing.SystemColors.WindowText
        Me.TextNameCursor.Location = New System.Drawing.Point(130, 16)
        Me.TextNameCursor.MaxLength = 0
        Me.TextNameCursor.Name = "TextNameCursor"
        Me.TextNameCursor.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.TextNameCursor.Size = New System.Drawing.Size(103, 20)
        Me.TextNameCursor.TabIndex = 52
        Me.TextNameCursor.Text = "TextNameCursor"
        '
        'CheckEnabled
        '
        Me.CheckEnabled.BackColor = System.Drawing.SystemColors.Control
        Me.CheckEnabled.Cursor = System.Windows.Forms.Cursors.Default
        Me.CheckEnabled.ForeColor = System.Drawing.SystemColors.ControlText
        Me.CheckEnabled.Location = New System.Drawing.Point(8, 40)
        Me.CheckEnabled.Name = "CheckEnabled"
        Me.CheckEnabled.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.CheckEnabled.Size = New System.Drawing.Size(81, 17)
        Me.CheckEnabled.TabIndex = 51
        Me.CheckEnabled.Text = "Доступен"
        Me.CheckEnabled.UseVisualStyleBackColor = False
        '
        'CheckVisible
        '
        Me.CheckVisible.BackColor = System.Drawing.SystemColors.Control
        Me.CheckVisible.Cursor = System.Windows.Forms.Cursors.Default
        Me.CheckVisible.ForeColor = System.Drawing.SystemColors.ControlText
        Me.CheckVisible.Location = New System.Drawing.Point(8, 56)
        Me.CheckVisible.Name = "CheckVisible"
        Me.CheckVisible.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.CheckVisible.Size = New System.Drawing.Size(89, 17)
        Me.CheckVisible.TabIndex = 50
        Me.CheckVisible.Text = "Видимость"
        Me.CheckVisible.UseVisualStyleBackColor = False
        '
        'ComboCursors
        '
        Me.ComboCursors.BackColor = System.Drawing.SystemColors.Window
        Me.ComboCursors.Cursor = System.Windows.Forms.Cursors.Default
        Me.ComboCursors.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.ComboCursors.ForeColor = System.Drawing.SystemColors.WindowText
        Me.ComboCursors.Location = New System.Drawing.Point(8, 16)
        Me.ComboCursors.Name = "ComboCursors"
        Me.ComboCursors.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.ComboCursors.Size = New System.Drawing.Size(81, 21)
        Me.ComboCursors.TabIndex = 49
        '
        'LabelName
        '
        Me.LabelName.BackColor = System.Drawing.SystemColors.Control
        Me.LabelName.Cursor = System.Windows.Forms.Cursors.Default
        Me.LabelName.ForeColor = System.Drawing.SystemColors.ControlText
        Me.LabelName.Location = New System.Drawing.Point(95, 19)
        Me.LabelName.Name = "LabelName"
        Me.LabelName.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.LabelName.Size = New System.Drawing.Size(57, 17)
        Me.LabelName.TabIndex = 53
        Me.LabelName.Text = "Имя"
        '
        'GroupBoxCross
        '
        Me.GroupBoxCross.BackColor = System.Drawing.SystemColors.Control
        Me.GroupBoxCross.Controls.Add(Me.ComboPointStyle)
        Me.GroupBoxCross.Controls.Add(Me.ButtonColorPoint)
        Me.GroupBoxCross.Controls.Add(Me.NumericEditTextSize)
        Me.GroupBoxCross.Controls.Add(Me.LabelStyle)
        Me.GroupBoxCross.Controls.Add(Me.LabelSize)
        Me.GroupBoxCross.ForeColor = System.Drawing.SystemColors.ControlText
        Me.GroupBoxCross.Location = New System.Drawing.Point(6, 93)
        Me.GroupBoxCross.Name = "GroupBoxCross"
        Me.GroupBoxCross.Padding = New System.Windows.Forms.Padding(0)
        Me.GroupBoxCross.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.GroupBoxCross.Size = New System.Drawing.Size(241, 73)
        Me.GroupBoxCross.TabIndex = 50
        Me.GroupBoxCross.TabStop = False
        Me.GroupBoxCross.Text = "Формат перекрестья"
        '
        'ComboPointStyle
        '
        Me.ComboPointStyle.BackColor = System.Drawing.SystemColors.Window
        Me.ComboPointStyle.Cursor = System.Windows.Forms.Cursors.Default
        Me.ComboPointStyle.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.ComboPointStyle.ForeColor = System.Drawing.SystemColors.WindowText
        Me.ComboPointStyle.Location = New System.Drawing.Point(56, 40)
        Me.ComboPointStyle.Name = "ComboPointStyle"
        Me.ComboPointStyle.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.ComboPointStyle.Size = New System.Drawing.Size(177, 21)
        Me.ComboPointStyle.TabIndex = 55
        '
        'ButtonColorPoint
        '
        Me.ButtonColorPoint.BackColor = System.Drawing.SystemColors.Control
        Me.ButtonColorPoint.Cursor = System.Windows.Forms.Cursors.Default
        Me.ButtonColorPoint.ForeColor = System.Drawing.SystemColors.ControlText
        Me.ButtonColorPoint.Location = New System.Drawing.Point(152, 11)
        Me.ButtonColorPoint.Name = "ButtonColorPoint"
        Me.ButtonColorPoint.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.ButtonColorPoint.Size = New System.Drawing.Size(81, 25)
        Me.ButtonColorPoint.TabIndex = 12
        Me.ButtonColorPoint.Text = "Цвет"
        Me.ButtonColorPoint.UseVisualStyleBackColor = False
        '
        'NumericEditTextSize
        '
        Me.NumericEditTextSize.Location = New System.Drawing.Point(56, 16)
        Me.NumericEditTextSize.Name = "NumericEditTextSize"
        Me.NumericEditTextSize.Range = New NationalInstruments.UI.Range(1.0R, 20.0R)
        Me.NumericEditTextSize.Size = New System.Drawing.Size(47, 20)
        Me.NumericEditTextSize.TabIndex = 76
        Me.NumericEditTextSize.Value = 5.0R
        '
        'LabelStyle
        '
        Me.LabelStyle.BackColor = System.Drawing.SystemColors.Control
        Me.LabelStyle.Cursor = System.Windows.Forms.Cursors.Default
        Me.LabelStyle.ForeColor = System.Drawing.SystemColors.ControlText
        Me.LabelStyle.Location = New System.Drawing.Point(8, 43)
        Me.LabelStyle.Name = "LabelStyle"
        Me.LabelStyle.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.LabelStyle.Size = New System.Drawing.Size(42, 17)
        Me.LabelStyle.TabIndex = 10
        Me.LabelStyle.Text = "Стиль"
        '
        'LabelSize
        '
        Me.LabelSize.BackColor = System.Drawing.SystemColors.Control
        Me.LabelSize.Cursor = System.Windows.Forms.Cursors.Default
        Me.LabelSize.ForeColor = System.Drawing.SystemColors.ControlText
        Me.LabelSize.Location = New System.Drawing.Point(8, 16)
        Me.LabelSize.Name = "LabelSize"
        Me.LabelSize.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.LabelSize.Size = New System.Drawing.Size(57, 17)
        Me.LabelSize.TabIndex = 9
        Me.LabelSize.Text = "Размер"
        '
        'GroupeBoxLine
        '
        Me.GroupeBoxLine.BackColor = System.Drawing.SystemColors.Control
        Me.GroupeBoxLine.Controls.Add(Me.ComBoxLineStyle)
        Me.GroupeBoxLine.Controls.Add(Me.NumericEditTextWidth)
        Me.GroupeBoxLine.Controls.Add(Me.ButtonColorLine)
        Me.GroupeBoxLine.Controls.Add(Me.LabelLineWight)
        Me.GroupeBoxLine.Controls.Add(Me.LabelStyle2)
        Me.GroupeBoxLine.ForeColor = System.Drawing.SystemColors.ControlText
        Me.GroupeBoxLine.Location = New System.Drawing.Point(6, 172)
        Me.GroupeBoxLine.Name = "GroupeBoxLine"
        Me.GroupeBoxLine.Padding = New System.Windows.Forms.Padding(0)
        Me.GroupeBoxLine.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.GroupeBoxLine.Size = New System.Drawing.Size(241, 73)
        Me.GroupeBoxLine.TabIndex = 51
        Me.GroupeBoxLine.TabStop = False
        Me.GroupeBoxLine.Text = "Линия"
        '
        'ComBoxLineStyle
        '
        Me.ComBoxLineStyle.BackColor = System.Drawing.SystemColors.Window
        Me.ComBoxLineStyle.Cursor = System.Windows.Forms.Cursors.Default
        Me.ComBoxLineStyle.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.ComBoxLineStyle.ForeColor = System.Drawing.SystemColors.WindowText
        Me.ComBoxLineStyle.Location = New System.Drawing.Point(56, 39)
        Me.ComBoxLineStyle.Name = "ComBoxLineStyle"
        Me.ComBoxLineStyle.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.ComBoxLineStyle.Size = New System.Drawing.Size(177, 21)
        Me.ComBoxLineStyle.TabIndex = 57
        '
        'NumericEditTextWidth
        '
        Me.NumericEditTextWidth.FormatMode = NationalInstruments.UI.NumericFormatMode.CreateSimpleDoubleMode(0)
        Me.NumericEditTextWidth.Location = New System.Drawing.Point(56, 13)
        Me.NumericEditTextWidth.Name = "NumericEditTextWidth"
        Me.NumericEditTextWidth.Range = New NationalInstruments.UI.Range(1.0R, 10.0R)
        Me.NumericEditTextWidth.Size = New System.Drawing.Size(47, 20)
        Me.NumericEditTextWidth.TabIndex = 77
        Me.NumericEditTextWidth.Value = 5.0R
        '
        'ButtonColorLine
        '
        Me.ButtonColorLine.BackColor = System.Drawing.SystemColors.Control
        Me.ButtonColorLine.Cursor = System.Windows.Forms.Cursors.Default
        Me.ButtonColorLine.ForeColor = System.Drawing.SystemColors.ControlText
        Me.ButtonColorLine.Location = New System.Drawing.Point(152, 8)
        Me.ButtonColorLine.Name = "ButtonColorLine"
        Me.ButtonColorLine.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.ButtonColorLine.Size = New System.Drawing.Size(81, 25)
        Me.ButtonColorLine.TabIndex = 15
        Me.ButtonColorLine.Text = "Цвет"
        Me.ButtonColorLine.UseVisualStyleBackColor = False
        '
        'LabelLineWight
        '
        Me.LabelLineWight.BackColor = System.Drawing.SystemColors.Control
        Me.LabelLineWight.Cursor = System.Windows.Forms.Cursors.Default
        Me.LabelLineWight.ForeColor = System.Drawing.SystemColors.ControlText
        Me.LabelLineWight.Location = New System.Drawing.Point(8, 16)
        Me.LabelLineWight.Name = "LabelLineWight"
        Me.LabelLineWight.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.LabelLineWight.Size = New System.Drawing.Size(65, 17)
        Me.LabelLineWight.TabIndex = 17
        Me.LabelLineWight.Text = "Ширина"
        '
        'LabelStyle2
        '
        Me.LabelStyle2.BackColor = System.Drawing.SystemColors.Control
        Me.LabelStyle2.Cursor = System.Windows.Forms.Cursors.Default
        Me.LabelStyle2.ForeColor = System.Drawing.SystemColors.ControlText
        Me.LabelStyle2.Location = New System.Drawing.Point(8, 42)
        Me.LabelStyle2.Name = "LabelStyle2"
        Me.LabelStyle2.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.LabelStyle2.Size = New System.Drawing.Size(42, 17)
        Me.LabelStyle2.TabIndex = 16
        Me.LabelStyle2.Text = "Стиль"
        '
        'GroupBoxTextCursor
        '
        Me.GroupBoxTextCursor.BackColor = System.Drawing.SystemColors.Control
        Me.GroupBoxTextCursor.Controls.Add(Me.NumericEditTextBackTransparency)
        Me.GroupBoxTextCursor.Controls.Add(Me.CheckShowPosition)
        Me.GroupBoxTextCursor.Controls.Add(Me.CheckShowName)
        Me.GroupBoxTextCursor.Controls.Add(Me.ButtonTextFont)
        Me.GroupBoxTextCursor.Controls.Add(Me.ButtonColorText)
        Me.GroupBoxTextCursor.Controls.Add(Me.ButtonTextBackColor)
        Me.GroupBoxTextCursor.Controls.Add(Me.LabelBackTransparency)
        Me.GroupBoxTextCursor.ForeColor = System.Drawing.SystemColors.ControlText
        Me.GroupBoxTextCursor.Location = New System.Drawing.Point(6, 251)
        Me.GroupBoxTextCursor.Name = "GroupBoxTextCursor"
        Me.GroupBoxTextCursor.Padding = New System.Windows.Forms.Padding(0)
        Me.GroupBoxTextCursor.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.GroupBoxTextCursor.Size = New System.Drawing.Size(241, 88)
        Me.GroupBoxTextCursor.TabIndex = 53
        Me.GroupBoxTextCursor.TabStop = False
        Me.GroupBoxTextCursor.Text = "Текст курсора"
        '
        'NumericEditTextBackTransparency
        '
        Me.NumericEditTextBackTransparency.CoercionInterval = 5.0R
        Me.NumericEditTextBackTransparency.FormatMode = NationalInstruments.UI.NumericFormatMode.CreateSimpleDoubleMode(0)
        Me.NumericEditTextBackTransparency.Location = New System.Drawing.Point(91, 54)
        Me.NumericEditTextBackTransparency.Name = "NumericEditTextBackTransparency"
        Me.NumericEditTextBackTransparency.Range = New NationalInstruments.UI.Range(0R, 100.0R)
        Me.NumericEditTextBackTransparency.Size = New System.Drawing.Size(55, 20)
        Me.NumericEditTextBackTransparency.TabIndex = 78
        Me.NumericEditTextBackTransparency.Value = 30.0R
        '
        'CheckShowPosition
        '
        Me.CheckShowPosition.BackColor = System.Drawing.SystemColors.Control
        Me.CheckShowPosition.Cursor = System.Windows.Forms.Cursors.Default
        Me.CheckShowPosition.ForeColor = System.Drawing.SystemColors.ControlText
        Me.CheckShowPosition.Location = New System.Drawing.Point(8, 32)
        Me.CheckShowPosition.Name = "CheckShowPosition"
        Me.CheckShowPosition.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.CheckShowPosition.Size = New System.Drawing.Size(75, 17)
        Me.CheckShowPosition.TabIndex = 30
        Me.CheckShowPosition.Text = "Позиция"
        Me.CheckShowPosition.UseVisualStyleBackColor = False
        '
        'CheckShowName
        '
        Me.CheckShowName.BackColor = System.Drawing.SystemColors.Control
        Me.CheckShowName.Cursor = System.Windows.Forms.Cursors.Default
        Me.CheckShowName.ForeColor = System.Drawing.SystemColors.ControlText
        Me.CheckShowName.Location = New System.Drawing.Point(8, 16)
        Me.CheckShowName.Name = "CheckShowName"
        Me.CheckShowName.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.CheckShowName.Size = New System.Drawing.Size(75, 17)
        Me.CheckShowName.TabIndex = 29
        Me.CheckShowName.Text = "Имя"
        Me.CheckShowName.UseVisualStyleBackColor = False
        '
        'ButtonTextFont
        '
        Me.ButtonTextFont.BackColor = System.Drawing.SystemColors.Control
        Me.ButtonTextFont.Cursor = System.Windows.Forms.Cursors.Default
        Me.ButtonTextFont.ForeColor = System.Drawing.SystemColors.ControlText
        Me.ButtonTextFont.Location = New System.Drawing.Point(89, 16)
        Me.ButtonTextFont.Name = "ButtonTextFont"
        Me.ButtonTextFont.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.ButtonTextFont.Size = New System.Drawing.Size(57, 25)
        Me.ButtonTextFont.TabIndex = 28
        Me.ButtonTextFont.Text = "Font"
        Me.ButtonTextFont.UseVisualStyleBackColor = False
        '
        'ButtonColorText
        '
        Me.ButtonColorText.BackColor = System.Drawing.SystemColors.Control
        Me.ButtonColorText.Cursor = System.Windows.Forms.Cursors.Default
        Me.ButtonColorText.ForeColor = System.Drawing.SystemColors.ControlText
        Me.ButtonColorText.Location = New System.Drawing.Point(152, 16)
        Me.ButtonColorText.Name = "ButtonColorText"
        Me.ButtonColorText.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.ButtonColorText.Size = New System.Drawing.Size(81, 25)
        Me.ButtonColorText.TabIndex = 27
        Me.ButtonColorText.Text = "Цвет"
        Me.ButtonColorText.UseVisualStyleBackColor = False
        '
        'ButtonTextBackColor
        '
        Me.ButtonTextBackColor.BackColor = System.Drawing.SystemColors.Control
        Me.ButtonTextBackColor.Cursor = System.Windows.Forms.Cursors.Default
        Me.ButtonTextBackColor.ForeColor = System.Drawing.SystemColors.ControlText
        Me.ButtonTextBackColor.Location = New System.Drawing.Point(152, 54)
        Me.ButtonTextBackColor.Name = "ButtonTextBackColor"
        Me.ButtonTextBackColor.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.ButtonTextBackColor.Size = New System.Drawing.Size(81, 25)
        Me.ButtonTextBackColor.TabIndex = 26
        Me.ButtonTextBackColor.Text = "Цвет фона"
        Me.ButtonTextBackColor.UseVisualStyleBackColor = False
        '
        'LabelBackTransparency
        '
        Me.LabelBackTransparency.BackColor = System.Drawing.SystemColors.Control
        Me.LabelBackTransparency.Cursor = System.Windows.Forms.Cursors.Default
        Me.LabelBackTransparency.ForeColor = System.Drawing.SystemColors.ControlText
        Me.LabelBackTransparency.Location = New System.Drawing.Point(3, 54)
        Me.LabelBackTransparency.Name = "LabelBackTransparency"
        Me.LabelBackTransparency.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.LabelBackTransparency.Size = New System.Drawing.Size(82, 29)
        Me.LabelBackTransparency.TabIndex = 32
        Me.LabelBackTransparency.Text = "Прозрачность фона"
        '
        'ImageList1
        '
        Me.ImageList1.ImageStream = CType(resources.GetObject("ImageList1.ImageStream"), System.Windows.Forms.ImageListStreamer)
        Me.ImageList1.TransparentColor = System.Drawing.Color.Transparent
        Me.ImageList1.Images.SetKeyName(0, "configure")
        Me.ImageList1.Images.SetKeyName(1, "appearance")
        Me.ImageList1.Images.SetKeyName(2, "cursor")
        '
        'MenuStripGraph
        '
        Me.MenuStripGraph.ImageScalingSize = New System.Drawing.Size(18, 18)
        Me.MenuStripGraph.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.MenuFile})
        Me.MenuStripGraph.Location = New System.Drawing.Point(0, 0)
        Me.MenuStripGraph.Name = "MenuStripGraph"
        Me.MenuStripGraph.Size = New System.Drawing.Size(755, 24)
        Me.MenuStripGraph.TabIndex = 37
        Me.MenuStripGraph.Text = "MenuStripTarir"
        '
        'MenuFile
        '
        Me.MenuFile.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.MenuPrintGraph, Me.MenuSaveGraph})
        Me.MenuFile.Name = "MenuFile"
        Me.MenuFile.Size = New System.Drawing.Size(60, 20)
        Me.MenuFile.Text = "&График"
        '
        'MenuPrintGraph
        '
        Me.MenuPrintGraph.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.MenuPrint3DEvolvent, Me.MenuPrint3DCylinder})
        Me.MenuPrintGraph.Name = "MenuPrintGraph"
        Me.MenuPrintGraph.Size = New System.Drawing.Size(179, 22)
        Me.MenuPrintGraph.Text = "Печатать &график ..."
        '
        'MenuPrint3DEvolvent
        '
        Me.MenuPrint3DEvolvent.Name = "MenuPrint3DEvolvent"
        Me.MenuPrint3DEvolvent.Size = New System.Drawing.Size(209, 22)
        Me.MenuPrint3DEvolvent.Text = "3D диаграмма &развертка"
        '
        'MenuPrint3DCylinder
        '
        Me.MenuPrint3DCylinder.Name = "MenuPrint3DCylinder"
        Me.MenuPrint3DCylinder.Size = New System.Drawing.Size(209, 22)
        Me.MenuPrint3DCylinder.Text = "3D диаграмма &цилиндр"
        '
        'MenuSaveGraph
        '
        Me.MenuSaveGraph.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.MenuSave3DEvolvent, Me.MenuSave3DCylinder})
        Me.MenuSaveGraph.Name = "MenuSaveGraph"
        Me.MenuSaveGraph.Size = New System.Drawing.Size(179, 22)
        Me.MenuSaveGraph.Text = "&Записать график ..."
        '
        'MenuSave3DEvolvent
        '
        Me.MenuSave3DEvolvent.Name = "MenuSave3DEvolvent"
        Me.MenuSave3DEvolvent.Size = New System.Drawing.Size(209, 22)
        Me.MenuSave3DEvolvent.Text = "3D диаграмма &развертка"
        '
        'MenuSave3DCylinder
        '
        Me.MenuSave3DCylinder.Name = "MenuSave3DCylinder"
        Me.MenuSave3DCylinder.Size = New System.Drawing.Size(209, 22)
        Me.MenuSave3DCylinder.Text = "3D диаграмма &цилиндр"
        '
        'Form3D
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(755, 717)
        Me.Controls.Add(Me.TabControlGrph)
        Me.Controls.Add(Me.MenuStripGraph)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "Form3D"
        Me.Text = "Объёмное поле"
        Me.TabControlGrph.ResumeLayout(False)
        Me.TabPageGrph.ResumeLayout(False)
        Me.Panel3DGraf.ResumeLayout(False)
        CType(Me.CWGraph3DЦилиндр, System.ComponentModel.ISupportInitialize).EndInit()
        Me.PanelSettingCylinder.ResumeLayout(False)
        Me.GroupBoxVolume.ResumeLayout(False)
        Me.GroupBoxPlotStyles.ResumeLayout(False)
        Me.GroupBoxColor.ResumeLayout(False)
        Me.GroupBoxAxisColor.ResumeLayout(False)
        Me.TabPageSurface.ResumeLayout(False)
        CType(Me.AxCWGraph3DEvolvent, System.ComponentModel.ISupportInitialize).EndInit()
        Me.PanelSetting.ResumeLayout(False)
        Me.TabControlSurface.ResumeLayout(False)
        Me.TabPageSurface2.ResumeLayout(False)
        CType(Me.AxCWGraph3DПроэкция, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBoxPositionCursor.ResumeLayout(False)
        Me.GroupBoxPositionCursor.PerformLayout()
        CType(Me.NumericEditColum, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.NumericEditRow, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.SlideAxis, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupeBoxProjection.ResumeLayout(False)
        Me.GroupeBoxProjection.PerformLayout()
        CType(Me.TextTransparency, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBoxtransparency.ResumeLayout(False)
        CType(Me.SlideTransparentMax, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.SlideTransparentMean, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.SlideTransparentMin, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TabPageStyle.ResumeLayout(False)
        Me.GroupBoxPlotStylesContour.ResumeLayout(False)
        Me.TabPageCursor.ResumeLayout(False)
        Me.GroupBoxCursor.ResumeLayout(False)
        Me.GroupBoxCursor.PerformLayout()
        Me.GroupBoxCross.ResumeLayout(False)
        CType(Me.NumericEditTextSize, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupeBoxLine.ResumeLayout(False)
        CType(Me.NumericEditTextWidth, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBoxTextCursor.ResumeLayout(False)
        CType(Me.NumericEditTextBackTransparency, System.ComponentModel.ISupportInitialize).EndInit()
        Me.MenuStripGraph.ResumeLayout(False)
        Me.MenuStripGraph.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents TabPageGrph As System.Windows.Forms.TabPage
    Friend WithEvents TabPageSurface As System.Windows.Forms.TabPage
    Friend WithEvents Panel3DGraf As System.Windows.Forms.Panel
    Friend WithEvents CWGraph3DЦилиндр As AxCW3DGraphLib.AxCWGraph3D
    Friend WithEvents PanelSettingCylinder As System.Windows.Forms.Panel
    Public WithEvents GroupBoxVolume As System.Windows.Forms.GroupBox
    Public WithEvents RadioButtonViewFlatness As System.Windows.Forms.RadioButton
    Public WithEvents RadioButtonViewVolume As System.Windows.Forms.RadioButton
    Public WithEvents ComboBoxBurner As System.Windows.Forms.ComboBox
    Public WithEvents CheckBoxNumber As System.Windows.Forms.CheckBox
    Public WithEvents GroupBoxPlotStyles As System.Windows.Forms.GroupBox
    Public WithEvents RadioButtonPoint As System.Windows.Forms.RadioButton
    Public WithEvents RadioButtonLine As System.Windows.Forms.RadioButton
    Public WithEvents RadioButtonSurface As System.Windows.Forms.RadioButton
    Public WithEvents RadioButtonContour As System.Windows.Forms.RadioButton
    Public WithEvents RadioButtonLinePoint As System.Windows.Forms.RadioButton
    Public WithEvents RadioButtonHiddenLine As System.Windows.Forms.RadioButton
    Public WithEvents RadioButtonSurfaceLine As System.Windows.Forms.RadioButton
    Public WithEvents RadioButtonSurfaceContour As System.Windows.Forms.RadioButton
    Public WithEvents RadioButtonSurfaceNormal As System.Windows.Forms.RadioButton
    Public WithEvents LabelViewBurner As System.Windows.Forms.Label
    Public WithEvents LabelHelp As System.Windows.Forms.Label
    Public WithEvents GroupBoxColor As System.Windows.Forms.GroupBox
    Public WithEvents RadioButtonNullColor As System.Windows.Forms.RadioButton
    Public WithEvents RadioButtonRed As System.Windows.Forms.RadioButton
    Public WithEvents RadioButtonRainbow As System.Windows.Forms.RadioButton
    Public WithEvents CheckBoxShell As System.Windows.Forms.CheckBox
    Public WithEvents GroupBoxAxisColor As System.Windows.Forms.GroupBox
    Public WithEvents RadioButtonExpanded As System.Windows.Forms.RadioButton
    Public WithEvents RadioButtonNormal As System.Windows.Forms.RadioButton
    Public WithEvents CheckBoxMarking As System.Windows.Forms.CheckBox
    Friend WithEvents AxCWGraph3DEvolvent As AxCW3DGraphLib.AxCWGraph3D
    Friend WithEvents PanelSetting As System.Windows.Forms.Panel
    Public WithEvents LabelSurface As System.Windows.Forms.Label
    Public WithEvents ComboPlotsList As System.Windows.Forms.ComboBox
    Friend WithEvents TabControlSurface As System.Windows.Forms.TabControl
    Friend WithEvents TabPageSurface2 As System.Windows.Forms.TabPage
    Friend WithEvents AxCWGraph3DПроэкция As AxCW3DGraphLib.AxCWGraph3D
    Public WithEvents GroupBoxPositionCursor As System.Windows.Forms.GroupBox
    Friend WithEvents NumericEditColum As NationalInstruments.UI.WindowsForms.NumericEdit
    Friend WithEvents NumericEditRow As NationalInstruments.UI.WindowsForms.NumericEdit
    Public WithEvents TextZPosition As System.Windows.Forms.TextBox
    Public WithEvents TextYPosition As System.Windows.Forms.TextBox
    Public WithEvents TextXPosition As System.Windows.Forms.TextBox
    Public WithEvents ComboSnapMode As System.Windows.Forms.ComboBox
    Public WithEvents LabelColumn As System.Windows.Forms.Label
    Public WithEvents LabelRow As System.Windows.Forms.Label
    Public WithEvents LabelZ As System.Windows.Forms.Label
    Public WithEvents LabelY As System.Windows.Forms.Label
    Public WithEvents LabelX As System.Windows.Forms.Label
    Public WithEvents LabelRegimeLinkage As System.Windows.Forms.Label
    Friend WithEvents SlideAxis As NationalInstruments.UI.WindowsForms.Slide
    Public WithEvents GroupeBoxProjection As System.Windows.Forms.GroupBox
    Friend WithEvents TextTransparency As NationalInstruments.UI.WindowsForms.NumericEdit
    Public WithEvents LabelTransparency As System.Windows.Forms.Label
    Friend WithEvents CheckEnableProjection As System.Windows.Forms.CheckBox
    Friend WithEvents CheckYZPlane As System.Windows.Forms.RadioButton
    Friend WithEvents CheckXZPlane As System.Windows.Forms.RadioButton
    Friend WithEvents CheckXYPlane As System.Windows.Forms.RadioButton
    Public WithEvents ButtonColorPlane As System.Windows.Forms.Button
    Friend WithEvents GroupBoxtransparency As System.Windows.Forms.GroupBox
    Friend WithEvents SlideTransparentMax As NationalInstruments.UI.WindowsForms.Slide
    Friend WithEvents SlideTransparentMean As NationalInstruments.UI.WindowsForms.Slide
    Friend WithEvents SlideTransparentMin As NationalInstruments.UI.WindowsForms.Slide
    Friend WithEvents TabPageStyle As System.Windows.Forms.TabPage
    Public WithEvents GroupBoxPlotStylesContour As System.Windows.Forms.GroupBox
    Public WithEvents RadioButtonSurfaceNormal2 As System.Windows.Forms.RadioButton
    Public WithEvents RadioButtonSurfaceContour2 As System.Windows.Forms.RadioButton
    Public WithEvents RadioButtonSurfaceLine2 As System.Windows.Forms.RadioButton
    Public WithEvents RadioButtonHiddenLine2 As System.Windows.Forms.RadioButton
    Public WithEvents RadioButtonLinePoint2 As System.Windows.Forms.RadioButton
    Public WithEvents RadioButtonContour2 As System.Windows.Forms.RadioButton
    Public WithEvents RadioButtonSurface2 As System.Windows.Forms.RadioButton
    Public WithEvents RadioButtonLine2 As System.Windows.Forms.RadioButton
    Public WithEvents RadioButtonPoint2 As System.Windows.Forms.RadioButton
    Public WithEvents CheckBoxFastDraw As System.Windows.Forms.CheckBox
    Public WithEvents CheckBoxPerspective As System.Windows.Forms.CheckBox
    Friend WithEvents TabPageCursor As System.Windows.Forms.TabPage
    Public WithEvents GroupBoxCursor As System.Windows.Forms.GroupBox
    Public WithEvents ButtonSetAllColors As System.Windows.Forms.Button
    Public WithEvents TextNameCursor As System.Windows.Forms.TextBox
    Public WithEvents CheckEnabled As System.Windows.Forms.CheckBox
    Public WithEvents CheckVisible As System.Windows.Forms.CheckBox
    Public WithEvents ComboCursors As System.Windows.Forms.ComboBox
    Public WithEvents LabelName As System.Windows.Forms.Label
    Public WithEvents GroupBoxCross As System.Windows.Forms.GroupBox
    Public WithEvents ComboPointStyle As System.Windows.Forms.ComboBox
    Public WithEvents ButtonColorPoint As System.Windows.Forms.Button
    Friend WithEvents NumericEditTextSize As NationalInstruments.UI.WindowsForms.NumericEdit
    Public WithEvents LabelStyle As System.Windows.Forms.Label
    Public WithEvents LabelSize As System.Windows.Forms.Label
    Public WithEvents GroupeBoxLine As System.Windows.Forms.GroupBox
    Public WithEvents ComBoxLineStyle As System.Windows.Forms.ComboBox
    Friend WithEvents NumericEditTextWidth As NationalInstruments.UI.WindowsForms.NumericEdit
    Public WithEvents ButtonColorLine As System.Windows.Forms.Button
    Public WithEvents LabelLineWight As System.Windows.Forms.Label
    Public WithEvents LabelStyle2 As System.Windows.Forms.Label
    Public WithEvents GroupBoxTextCursor As System.Windows.Forms.GroupBox
    Friend WithEvents NumericEditTextBackTransparency As NationalInstruments.UI.WindowsForms.NumericEdit
    Public WithEvents CheckShowPosition As System.Windows.Forms.CheckBox
    Public WithEvents CheckShowName As System.Windows.Forms.CheckBox
    Public WithEvents ButtonTextFont As System.Windows.Forms.Button
    Public WithEvents ButtonColorText As System.Windows.Forms.Button
    Public WithEvents ButtonTextBackColor As System.Windows.Forms.Button
    Public WithEvents LabelBackTransparency As System.Windows.Forms.Label
    Friend WithEvents ColorDialog1 As System.Windows.Forms.ColorDialog
    Friend WithEvents FontDialog1 As System.Windows.Forms.FontDialog
    Friend WithEvents MenuStripGraph As System.Windows.Forms.MenuStrip
    Friend WithEvents MenuFile As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents MenuPrintGraph As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents MenuPrint3DEvolvent As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents MenuPrint3DCylinder As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents MenuSaveGraph As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents MenuSave3DEvolvent As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents MenuSave3DCylinder As System.Windows.Forms.ToolStripMenuItem
    Public WithEvents TabControlGrph As System.Windows.Forms.TabControl
    Friend WithEvents ImageList1 As Windows.Forms.ImageList
End Class
