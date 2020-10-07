Imports System.Windows.Forms

<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class FormEncoder
    Inherits System.Windows.Forms.Form

    'Форма переопределяет dispose для очистки списка компонентов.
    <System.Diagnostics.DebuggerNonUserCode()>
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    Friend WithEvents ToolStripContainer As System.Windows.Forms.ToolStripContainer
    Friend WithEvents ImageListTreeNode As System.Windows.Forms.ImageList
    Friend WithEvents ToolStripSeparator7 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents ToolStripSeparator8 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents ToolStrip As System.Windows.Forms.ToolStrip
    Friend WithEvents ButtonStopTask As System.Windows.Forms.ToolStripButton
    Friend WithEvents ButtonRunTask As System.Windows.Forms.ToolStripButton
    Friend WithEvents SplitContainer As System.Windows.Forms.SplitContainer
    Friend WithEvents TreeView As System.Windows.Forms.TreeView
    Friend WithEvents StatusStrip As System.Windows.Forms.StatusStrip
    Friend WithEvents ToolTip As System.Windows.Forms.ToolTip
    Friend WithEvents ImageListViewSmall As System.Windows.Forms.ImageList

    'Является обязательной для конструктора форм Windows Forms
    Private components As System.ComponentModel.IContainer

    'Примечание: следующая процедура является обязательной для конструктора форм Windows Forms
    'Для ее изменения используйте конструктор форм Windows Form.  
    'Не изменяйте ее в редакторе исходного кода.
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FormEncoder))
        Dim ScaleCustomDivision1 As NationalInstruments.UI.ScaleCustomDivision = New NationalInstruments.UI.ScaleCustomDivision()
        Dim ScaleRangeFill1 As NationalInstruments.UI.ScaleRangeFill = New NationalInstruments.UI.ScaleRangeFill()
        Me.StatusStrip = New System.Windows.Forms.StatusStrip()
        Me.LabelStatus = New System.Windows.Forms.ToolStripStatusLabel()
        Me.ImageListTreeNode = New System.Windows.Forms.ImageList(Me.components)
        Me.ToolStrip = New System.Windows.Forms.ToolStrip()
        Me.ButtonStopTask = New System.Windows.Forms.ToolStripButton()
        Me.ButtonRunTask = New System.Windows.Forms.ToolStripButton()
        Me.ToolStripSeparator7 = New System.Windows.Forms.ToolStripSeparator()
        Me.ButtonEncoder = New System.Windows.Forms.ToolStripButton()
        Me.ButtonResetPosition = New System.Windows.Forms.ToolStripButton()
        Me.ToolStripLabel1 = New System.Windows.Forms.ToolStripLabel()
        Me.TextPosition = New System.Windows.Forms.ToolStripTextBox()
        Me.ButtonGetPosition = New System.Windows.Forms.ToolStripButton()
        Me.ToolStripSeparator8 = New System.Windows.Forms.ToolStripSeparator()
        Me.ToolTip = New System.Windows.Forms.ToolTip(Me.components)
        Me.TreeView = New System.Windows.Forms.TreeView()
        Me.TabPageSetting = New System.Windows.Forms.TabPage()
        Me.LabelInitAdapter = New System.Windows.Forms.Label()
        Me.ButtonAddNode = New System.Windows.Forms.Button()
        Me.LabelComPort = New System.Windows.Forms.Label()
        Me.LabelNodeId = New System.Windows.Forms.Label()
        Me.ButtonInitial = New System.Windows.Forms.Button()
        Me.ComboNode = New System.Windows.Forms.ComboBox()
        Me.LabelSpeedRate = New System.Windows.Forms.Label()
        Me.ComboCom = New System.Windows.Forms.ComboBox()
        Me.ComboBaud = New System.Windows.Forms.ComboBox()
        Me.ButtonRemove = New System.Windows.Forms.Button()
        Me.ButtonShutdown = New System.Windows.Forms.Button()
        Me.TabPageSDO_Write = New System.Windows.Forms.TabPage()
        Me.TableLayoutPanelSDOWrite = New System.Windows.Forms.TableLayoutPanel()
        Me.LabelIndexHex = New System.Windows.Forms.Label()
        Me.ButtonClearWrite = New System.Windows.Forms.Button()
        Me.ListSDODataWrite = New System.Windows.Forms.ListBox()
        Me.ComboLen = New System.Windows.Forms.ComboBox()
        Me.ButtonWriteSDO = New System.Windows.Forms.Button()
        Me.TextD3 = New System.Windows.Forms.TextBox()
        Me.LabelLen = New System.Windows.Forms.Label()
        Me.TextD2 = New System.Windows.Forms.TextBox()
        Me.LabelSubIndex = New System.Windows.Forms.Label()
        Me.TextD1 = New System.Windows.Forms.TextBox()
        Me.TextIndexWrite = New System.Windows.Forms.TextBox()
        Me.TextD0 = New System.Windows.Forms.TextBox()
        Me.TextSubIndexWrite = New System.Windows.Forms.TextBox()
        Me.LabelDataHex = New System.Windows.Forms.Label()
        Me.LabelSDOWrite = New System.Windows.Forms.Label()
        Me.TabPageSDORead = New System.Windows.Forms.TabPage()
        Me.TableLayoutPanelSDORead = New System.Windows.Forms.TableLayoutPanel()
        Me.LabelSubIndex2 = New System.Windows.Forms.Label()
        Me.TextIndexRead = New System.Windows.Forms.TextBox()
        Me.TextSubIndexRead = New System.Windows.Forms.TextBox()
        Me.ButtonReadSDO = New System.Windows.Forms.Button()
        Me.ButtonClearRead = New System.Windows.Forms.Button()
        Me.ListSDODataRead = New System.Windows.Forms.ListBox()
        Me.LabelIndexHex2 = New System.Windows.Forms.Label()
        Me.LabelSdoRead = New System.Windows.Forms.Label()
        Me.TabPageNMT_Protocol = New System.Windows.Forms.TabPage()
        Me.TableLayoutPanelNMTProtocol = New System.Windows.Forms.TableLayoutPanel()
        Me.ButtonClearNMTProtocol = New System.Windows.Forms.Button()
        Me.ListEMCY = New System.Windows.Forms.ListBox()
        Me.LabelTimeTic = New System.Windows.Forms.Label()
        Me.TextHeartbeat = New System.Windows.Forms.TextBox()
        Me.LabelTimeWaite = New System.Windows.Forms.Label()
        Me.TextConsumer = New System.Windows.Forms.TextBox()
        Me.ButtonSetHeartbeat = New System.Windows.Forms.Button()
        Me.TextGuardTime = New System.Windows.Forms.TextBox()
        Me.TextLifeTime = New System.Windows.Forms.TextBox()
        Me.ButtonSetGuard = New System.Windows.Forms.Button()
        Me.LabelTimeWathDog = New System.Windows.Forms.Label()
        Me.LabelFactorLive = New System.Windows.Forms.Label()
        Me.LabelModeSlaveNode = New System.Windows.Forms.Label()
        Me.ButtonSetState = New System.Windows.Forms.Button()
        Me.ComboState = New System.Windows.Forms.ComboBox()
        Me.LabelNMTProtocol = New System.Windows.Forms.Label()
        Me.LedInterrupter = New NationalInstruments.UI.WindowsForms.Led()
        Me.ToolStripContainer = New System.Windows.Forms.ToolStripContainer()
        Me.SplitContainer = New System.Windows.Forms.SplitContainer()
        Me.TabControlSetting = New System.Windows.Forms.TabControl()
        Me.TabPageMesage = New System.Windows.Forms.TabPage()
        Me.ListViewAlarm = New Chamber.DbListView(Me.components)
        Me.LabelCaptionGrid = New System.Windows.Forms.Label()
        Me.ImageListViewSmall = New System.Windows.Forms.ImageList(Me.components)
        Me.PanelGauge = New System.Windows.Forms.Panel()
        Me.TableLayoutPanelGauge = New System.Windows.Forms.TableLayoutPanel()
        Me.NumericEditAngularPositionEncoder = New NationalInstruments.UI.WindowsForms.NumericEdit()
        Me.GaugeAngularPositionEncoder = New NationalInstruments.UI.WindowsForms.Gauge()
        Me.NumericEditEncoderThermometer = New NationalInstruments.UI.WindowsForms.NumericEdit()
        Me.ThermometerEncoder = New NationalInstruments.UI.WindowsForms.Thermometer()
        Me.TableLayoutPanelTermometer = New System.Windows.Forms.TableLayoutPanel()
        Me.TimerMNT = New System.Windows.Forms.Timer(Me.components)
        Me.ImageList1 = New System.Windows.Forms.ImageList(Me.components)
        Me.TimerTemperature = New System.Windows.Forms.Timer(Me.components)
        Me.StatusStrip.SuspendLayout()
        Me.ToolStrip.SuspendLayout()
        Me.TabPageSetting.SuspendLayout()
        Me.TabPageSDO_Write.SuspendLayout()
        Me.TableLayoutPanelSDOWrite.SuspendLayout()
        Me.TabPageSDORead.SuspendLayout()
        Me.TableLayoutPanelSDORead.SuspendLayout()
        Me.TabPageNMT_Protocol.SuspendLayout()
        Me.TableLayoutPanelNMTProtocol.SuspendLayout()
        CType(Me.LedInterrupter, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ToolStripContainer.BottomToolStripPanel.SuspendLayout()
        Me.ToolStripContainer.ContentPanel.SuspendLayout()
        Me.ToolStripContainer.TopToolStripPanel.SuspendLayout()
        Me.ToolStripContainer.SuspendLayout()
        CType(Me.SplitContainer, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainer.Panel1.SuspendLayout()
        Me.SplitContainer.Panel2.SuspendLayout()
        Me.SplitContainer.SuspendLayout()
        Me.TabControlSetting.SuspendLayout()
        Me.TabPageMesage.SuspendLayout()
        Me.PanelGauge.SuspendLayout()
        Me.TableLayoutPanelGauge.SuspendLayout()
        CType(Me.NumericEditAngularPositionEncoder, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GaugeAngularPositionEncoder, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.NumericEditEncoderThermometer, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ThermometerEncoder, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TableLayoutPanelTermometer.SuspendLayout()
        Me.SuspendLayout()
        '
        'StatusStrip
        '
        Me.StatusStrip.Dock = System.Windows.Forms.DockStyle.None
        Me.StatusStrip.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.LabelStatus})
        Me.StatusStrip.Location = New System.Drawing.Point(0, 0)
        Me.StatusStrip.Name = "StatusStrip"
        Me.StatusStrip.Size = New System.Drawing.Size(1302, 22)
        Me.StatusStrip.TabIndex = 6
        Me.StatusStrip.Text = "StatusStrip"
        '
        'LabelStatus
        '
        Me.LabelStatus.Image = CType(resources.GetObject("LabelStatus.Image"), System.Drawing.Image)
        Me.LabelStatus.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.LabelStatus.Name = "LabelStatus"
        Me.LabelStatus.Size = New System.Drawing.Size(1287, 17)
        Me.LabelStatus.Spring = True
        Me.LabelStatus.Text = "Готов"
        Me.LabelStatus.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'ImageListTreeNode
        '
        Me.ImageListTreeNode.ImageStream = CType(resources.GetObject("ImageListTreeNode.ImageStream"), System.Windows.Forms.ImageListStreamer)
        Me.ImageListTreeNode.TransparentColor = System.Drawing.Color.Transparent
        Me.ImageListTreeNode.Images.SetKeyName(0, "ClosedFolder")
        Me.ImageListTreeNode.Images.SetKeyName(1, "OpenFolder")
        '
        'ToolStrip
        '
        Me.ToolStrip.Anchor = System.Windows.Forms.AnchorStyles.Right
        Me.ToolStrip.Dock = System.Windows.Forms.DockStyle.None
        Me.ToolStrip.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ButtonStopTask, Me.ButtonRunTask, Me.ToolStripSeparator7, Me.ButtonEncoder, Me.ButtonResetPosition, Me.ToolStripLabel1, Me.TextPosition, Me.ButtonGetPosition, Me.ToolStripSeparator8})
        Me.ToolStrip.Location = New System.Drawing.Point(3, 0)
        Me.ToolStrip.Name = "ToolStrip"
        Me.ToolStrip.Size = New System.Drawing.Size(753, 25)
        Me.ToolStrip.TabIndex = 0
        Me.ToolStrip.Text = "ToolStrip1"
        '
        'ButtonStopTask
        '
        Me.ButtonStopTask.Enabled = False
        Me.ButtonStopTask.Image = CType(resources.GetObject("ButtonStopTask.Image"), System.Drawing.Image)
        Me.ButtonStopTask.ImageTransparentColor = System.Drawing.Color.Black
        Me.ButtonStopTask.Name = "ButtonStopTask"
        Me.ButtonStopTask.Size = New System.Drawing.Size(91, 22)
        Me.ButtonStopTask.Text = "Остановить"
        Me.ButtonStopTask.ToolTipText = "Остановить непрерывный опрос"
        '
        'ButtonRunTask
        '
        Me.ButtonRunTask.Enabled = False
        Me.ButtonRunTask.Image = CType(resources.GetObject("ButtonRunTask.Image"), System.Drawing.Image)
        Me.ButtonRunTask.ImageTransparentColor = System.Drawing.Color.Black
        Me.ButtonRunTask.Name = "ButtonRunTask"
        Me.ButtonRunTask.Size = New System.Drawing.Size(82, 22)
        Me.ButtonRunTask.Text = "Запустить"
        Me.ButtonRunTask.ToolTipText = "Запустить непрерывный опрос"
        '
        'ToolStripSeparator7
        '
        Me.ToolStripSeparator7.Name = "ToolStripSeparator7"
        Me.ToolStripSeparator7.Size = New System.Drawing.Size(6, 25)
        '
        'ButtonEncoder
        '
        Me.ButtonEncoder.Image = CType(resources.GetObject("ButtonEncoder.Image"), System.Drawing.Image)
        Me.ButtonEncoder.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ButtonEncoder.Name = "ButtonEncoder"
        Me.ButtonEncoder.Size = New System.Drawing.Size(133, 22)
        Me.ButtonEncoder.Text = "Инициализировать"
        Me.ButtonEncoder.ToolTipText = "Инициализировать энкодер"
        '
        'ButtonResetPosition
        '
        Me.ButtonResetPosition.Image = CType(resources.GetObject("ButtonResetPosition.Image"), System.Drawing.Image)
        Me.ButtonResetPosition.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ButtonResetPosition.Name = "ButtonResetPosition"
        Me.ButtonResetPosition.Size = New System.Drawing.Size(112, 22)
        Me.ButtonResetPosition.Text = "Сброс позиции"
        Me.ButtonResetPosition.ToolTipText = "Установить нулевое положение"
        '
        'ToolStripLabel1
        '
        Me.ToolStripLabel1.Name = "ToolStripLabel1"
        Me.ToolStripLabel1.Size = New System.Drawing.Size(148, 22)
        Me.ToolStripLabel1.Text = "Текущая позиция турели:"
        '
        'TextPosition
        '
        Me.TextPosition.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(204, Byte))
        Me.TextPosition.ForeColor = System.Drawing.Color.Blue
        Me.TextPosition.Name = "TextPosition"
        Me.TextPosition.Size = New System.Drawing.Size(80, 25)
        Me.TextPosition.Text = "0"
        Me.TextPosition.ToolTipText = "Текущее положение"
        '
        'ButtonGetPosition
        '
        Me.ButtonGetPosition.Image = CType(resources.GetObject("ButtonGetPosition.Image"), System.Drawing.Image)
        Me.ButtonGetPosition.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ButtonGetPosition.Name = "ButtonGetPosition"
        Me.ButtonGetPosition.Size = New System.Drawing.Size(81, 22)
        Me.ButtonGetPosition.Text = "Обновить"
        Me.ButtonGetPosition.ToolTipText = "Обновить значения индикаторов"
        '
        'ToolStripSeparator8
        '
        Me.ToolStripSeparator8.Name = "ToolStripSeparator8"
        Me.ToolStripSeparator8.Size = New System.Drawing.Size(6, 25)
        '
        'TreeView
        '
        Me.TreeView.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TreeView.ImageIndex = 0
        Me.TreeView.ImageList = Me.ImageListTreeNode
        Me.TreeView.Location = New System.Drawing.Point(0, 0)
        Me.TreeView.Name = "TreeView"
        Me.TreeView.SelectedImageIndex = 1
        Me.TreeView.ShowLines = False
        Me.TreeView.Size = New System.Drawing.Size(167, 498)
        Me.TreeView.TabIndex = 0
        Me.ToolTip.SetToolTip(Me.TreeView, "Выбор вкладок настроек")
        '
        'TabPageSetting
        '
        Me.TabPageSetting.Controls.Add(Me.LabelInitAdapter)
        Me.TabPageSetting.Controls.Add(Me.ButtonAddNode)
        Me.TabPageSetting.Controls.Add(Me.LabelComPort)
        Me.TabPageSetting.Controls.Add(Me.LabelNodeId)
        Me.TabPageSetting.Controls.Add(Me.ButtonInitial)
        Me.TabPageSetting.Controls.Add(Me.ComboNode)
        Me.TabPageSetting.Controls.Add(Me.LabelSpeedRate)
        Me.TabPageSetting.Controls.Add(Me.ComboCom)
        Me.TabPageSetting.Controls.Add(Me.ComboBaud)
        Me.TabPageSetting.Controls.Add(Me.ButtonRemove)
        Me.TabPageSetting.Controls.Add(Me.ButtonShutdown)
        Me.TabPageSetting.ImageKey = "Initialize"
        Me.TabPageSetting.Location = New System.Drawing.Point(4, 24)
        Me.TabPageSetting.Name = "TabPageSetting"
        Me.TabPageSetting.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPageSetting.Size = New System.Drawing.Size(607, 470)
        Me.TabPageSetting.TabIndex = 0
        Me.TabPageSetting.Text = "Initialize"
        Me.ToolTip.SetToolTip(Me.TabPageSetting, "Инициализация энкодера")
        Me.TabPageSetting.ToolTipText = "Инициализация энкодера"
        Me.TabPageSetting.UseVisualStyleBackColor = True
        '
        'LabelInitAdapter
        '
        Me.LabelInitAdapter.BackColor = System.Drawing.Color.LightSteelBlue
        Me.LabelInitAdapter.Dock = System.Windows.Forms.DockStyle.Top
        Me.LabelInitAdapter.ForeColor = System.Drawing.Color.Black
        Me.LabelInitAdapter.Location = New System.Drawing.Point(3, 3)
        Me.LabelInitAdapter.Name = "LabelInitAdapter"
        Me.LabelInitAdapter.Size = New System.Drawing.Size(601, 22)
        Me.LabelInitAdapter.TabIndex = 118
        Me.LabelInitAdapter.Text = "Инициализация адаптера"
        Me.LabelInitAdapter.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'ButtonAddNode
        '
        Me.ButtonAddNode.Location = New System.Drawing.Point(264, 91)
        Me.ButtonAddNode.Name = "ButtonAddNode"
        Me.ButtonAddNode.Size = New System.Drawing.Size(160, 24)
        Me.ButtonAddNode.TabIndex = 111
        Me.ButtonAddNode.Text = "Добавить Узел"
        Me.ToolTip.SetToolTip(Me.ButtonAddNode, "Добавить Узел")
        '
        'LabelComPort
        '
        Me.LabelComPort.Location = New System.Drawing.Point(6, 33)
        Me.LabelComPort.Name = "LabelComPort"
        Me.LabelComPort.Size = New System.Drawing.Size(120, 20)
        Me.LabelComPort.TabIndex = 114
        Me.LabelComPort.Text = "Com Port:"
        '
        'LabelNodeId
        '
        Me.LabelNodeId.Location = New System.Drawing.Point(6, 96)
        Me.LabelNodeId.Name = "LabelNodeId"
        Me.LabelNodeId.Size = New System.Drawing.Size(120, 20)
        Me.LabelNodeId.TabIndex = 113
        Me.LabelNodeId.Text = "Узел ID:"
        '
        'ButtonInitial
        '
        Me.ButtonInitial.Location = New System.Drawing.Point(264, 62)
        Me.ButtonInitial.Name = "ButtonInitial"
        Me.ButtonInitial.Size = New System.Drawing.Size(160, 24)
        Me.ButtonInitial.TabIndex = 112
        Me.ButtonInitial.Text = "Инициализировать Мастер"
        Me.ToolTip.SetToolTip(Me.ButtonInitial, "Инициализировать Мастера")
        '
        'ComboNode
        '
        Me.ComboNode.Items.AddRange(New Object() {"    1", "    2", "    3", "    4", "    5", "    6", "    7", "    8", "    9", "  10", "  11", "  12", "  13", "  14", "  15", "  16", "  17", "  18", "  19", "  20", "  21", "  22", "  23", "  24", "  25", "  26", "  27", "  28", "  29", "  30", "  31", "  32", "  33", "  34", "  35", "  36", "  37", "  38", "  39", "  40", "  41", "  42", "  43", "  44", "  45", "  46", "  47", "  48", "  49", "  50", "  51", "  52", "  53", "  54", "  55", "  56", "  57", "  58", "  59", "  60", "  61", "  62", "  63", "  64", "  65", "  66", "  67", "  68", "  69", "  70", "  71", "  72", "  73", "  74", "  75", "  76", "  77", "  78", "  79", "  80", "  81", "  82", "  83", "  84", "  85", "  86", "  87", "  88", "  89", "  90", "  91", "  92", "  93", "  94", "  95", "  96", "  97", "  98", "  99", "100", "101", "102", "103", "104", "105", "106", "107", "108", "109", "110", "111", "112", "113", "114", "115", "116", "117", "118", "119", "120", "121", "122", "123", "124", "125", "126", "127"})
        Me.ComboNode.Location = New System.Drawing.Point(132, 95)
        Me.ComboNode.Name = "ComboNode"
        Me.ComboNode.Size = New System.Drawing.Size(126, 21)
        Me.ComboNode.TabIndex = 108
        Me.ToolTip.SetToolTip(Me.ComboNode, "Выбор номера ведомого устройства")
        '
        'LabelSpeedRate
        '
        Me.LabelSpeedRate.Location = New System.Drawing.Point(6, 64)
        Me.LabelSpeedRate.Name = "LabelSpeedRate"
        Me.LabelSpeedRate.Size = New System.Drawing.Size(120, 20)
        Me.LabelSpeedRate.TabIndex = 115
        Me.LabelSpeedRate.Text = "Скорость Передачи:"
        '
        'ComboCom
        '
        Me.ComboCom.Items.AddRange(New Object() {"0", "1", "2", "3", "4", "5", "6", "7", "8", "9", "10", "11", "12", "13", "14", "15"})
        Me.ComboCom.Location = New System.Drawing.Point(132, 32)
        Me.ComboCom.Name = "ComboCom"
        Me.ComboCom.Size = New System.Drawing.Size(126, 21)
        Me.ComboCom.TabIndex = 110
        Me.ToolTip.SetToolTip(Me.ComboCom, "Выбор номера порта")
        '
        'ComboBaud
        '
        Me.ComboBaud.Items.AddRange(New Object() {"    10 kbps", "    20 kbps", "    50 kbps", "  125 kbps", "  250 kbps", "  500 kbps", "  800 kbps", "1000 kbps"})
        Me.ComboBaud.Location = New System.Drawing.Point(132, 64)
        Me.ComboBaud.Name = "ComboBaud"
        Me.ComboBaud.Size = New System.Drawing.Size(126, 21)
        Me.ComboBaud.TabIndex = 109
        Me.ToolTip.SetToolTip(Me.ComboBaud, "Выбор скорости передачи")
        '
        'ButtonRemove
        '
        Me.ButtonRemove.Location = New System.Drawing.Point(264, 91)
        Me.ButtonRemove.Name = "ButtonRemove"
        Me.ButtonRemove.Size = New System.Drawing.Size(160, 24)
        Me.ButtonRemove.TabIndex = 116
        Me.ButtonRemove.Text = "Удалить Узел"
        Me.ToolTip.SetToolTip(Me.ButtonRemove, "Удалить Узел")
        '
        'ButtonShutdown
        '
        Me.ButtonShutdown.Location = New System.Drawing.Point(264, 62)
        Me.ButtonShutdown.Name = "ButtonShutdown"
        Me.ButtonShutdown.Size = New System.Drawing.Size(160, 24)
        Me.ButtonShutdown.TabIndex = 117
        Me.ButtonShutdown.Text = "Сбросить"
        Me.ToolTip.SetToolTip(Me.ButtonShutdown, "Сброс Мастера")
        '
        'TabPageSDO_Write
        '
        Me.TabPageSDO_Write.Controls.Add(Me.TableLayoutPanelSDOWrite)
        Me.TabPageSDO_Write.Controls.Add(Me.LabelSDOWrite)
        Me.TabPageSDO_Write.ImageKey = "SDO_Write"
        Me.TabPageSDO_Write.Location = New System.Drawing.Point(4, 24)
        Me.TabPageSDO_Write.Name = "TabPageSDO_Write"
        Me.TabPageSDO_Write.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPageSDO_Write.Size = New System.Drawing.Size(607, 470)
        Me.TabPageSDO_Write.TabIndex = 1
        Me.TabPageSDO_Write.Text = "SDO_Write"
        Me.ToolTip.SetToolTip(Me.TabPageSDO_Write, "Запись значений сервисных объектов данных")
        Me.TabPageSDO_Write.ToolTipText = "Запись значений сервисных объектов данных"
        Me.TabPageSDO_Write.UseVisualStyleBackColor = True
        '
        'TableLayoutPanelSDOWrite
        '
        Me.TableLayoutPanelSDOWrite.ColumnCount = 6
        Me.TableLayoutPanelSDOWrite.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 30.0!))
        Me.TableLayoutPanelSDOWrite.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 10.0!))
        Me.TableLayoutPanelSDOWrite.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 10.0!))
        Me.TableLayoutPanelSDOWrite.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 10.0!))
        Me.TableLayoutPanelSDOWrite.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 10.0!))
        Me.TableLayoutPanelSDOWrite.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 30.0!))
        Me.TableLayoutPanelSDOWrite.Controls.Add(Me.LabelIndexHex, 0, 0)
        Me.TableLayoutPanelSDOWrite.Controls.Add(Me.ButtonClearWrite, 5, 4)
        Me.TableLayoutPanelSDOWrite.Controls.Add(Me.ListSDODataWrite, 0, 3)
        Me.TableLayoutPanelSDOWrite.Controls.Add(Me.ComboLen, 4, 1)
        Me.TableLayoutPanelSDOWrite.Controls.Add(Me.ButtonWriteSDO, 5, 2)
        Me.TableLayoutPanelSDOWrite.Controls.Add(Me.TextD3, 4, 2)
        Me.TableLayoutPanelSDOWrite.Controls.Add(Me.LabelLen, 4, 0)
        Me.TableLayoutPanelSDOWrite.Controls.Add(Me.TextD2, 3, 2)
        Me.TableLayoutPanelSDOWrite.Controls.Add(Me.LabelSubIndex, 2, 0)
        Me.TableLayoutPanelSDOWrite.Controls.Add(Me.TextD1, 2, 2)
        Me.TableLayoutPanelSDOWrite.Controls.Add(Me.TextIndexWrite, 0, 1)
        Me.TableLayoutPanelSDOWrite.Controls.Add(Me.TextD0, 1, 2)
        Me.TableLayoutPanelSDOWrite.Controls.Add(Me.TextSubIndexWrite, 2, 1)
        Me.TableLayoutPanelSDOWrite.Controls.Add(Me.LabelDataHex, 0, 2)
        Me.TableLayoutPanelSDOWrite.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanelSDOWrite.Location = New System.Drawing.Point(3, 25)
        Me.TableLayoutPanelSDOWrite.Name = "TableLayoutPanelSDOWrite"
        Me.TableLayoutPanelSDOWrite.RowCount = 5
        Me.TableLayoutPanelSDOWrite.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30.0!))
        Me.TableLayoutPanelSDOWrite.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30.0!))
        Me.TableLayoutPanelSDOWrite.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30.0!))
        Me.TableLayoutPanelSDOWrite.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanelSDOWrite.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30.0!))
        Me.TableLayoutPanelSDOWrite.Size = New System.Drawing.Size(601, 442)
        Me.TableLayoutPanelSDOWrite.TabIndex = 115
        '
        'LabelIndexHex
        '
        Me.TableLayoutPanelSDOWrite.SetColumnSpan(Me.LabelIndexHex, 2)
        Me.LabelIndexHex.Dock = System.Windows.Forms.DockStyle.Fill
        Me.LabelIndexHex.Location = New System.Drawing.Point(3, 0)
        Me.LabelIndexHex.Name = "LabelIndexHex"
        Me.LabelIndexHex.Size = New System.Drawing.Size(234, 30)
        Me.LabelIndexHex.TabIndex = 113
        Me.LabelIndexHex.Text = " Index (hex) " & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "0000~FFFF"
        '
        'ButtonClearWrite
        '
        Me.ButtonClearWrite.Dock = System.Windows.Forms.DockStyle.Fill
        Me.ButtonClearWrite.Location = New System.Drawing.Point(423, 415)
        Me.ButtonClearWrite.Name = "ButtonClearWrite"
        Me.ButtonClearWrite.Size = New System.Drawing.Size(175, 24)
        Me.ButtonClearWrite.TabIndex = 114
        Me.ButtonClearWrite.Text = "Очистить"
        Me.ToolTip.SetToolTip(Me.ButtonClearWrite, "Очистить протокол")
        '
        'ListSDODataWrite
        '
        Me.TableLayoutPanelSDOWrite.SetColumnSpan(Me.ListSDODataWrite, 6)
        Me.ListSDODataWrite.Dock = System.Windows.Forms.DockStyle.Fill
        Me.ListSDODataWrite.Location = New System.Drawing.Point(3, 93)
        Me.ListSDODataWrite.Name = "ListSDODataWrite"
        Me.ListSDODataWrite.Size = New System.Drawing.Size(595, 316)
        Me.ListSDODataWrite.TabIndex = 108
        Me.ToolTip.SetToolTip(Me.ListSDODataWrite, "Протокол результатов сообщений")
        '
        'ComboLen
        '
        Me.ComboLen.Dock = System.Windows.Forms.DockStyle.Top
        Me.ComboLen.Items.AddRange(New Object() {"1", "2", "3", "4"})
        Me.ComboLen.Location = New System.Drawing.Point(363, 33)
        Me.ComboLen.Name = "ComboLen"
        Me.ComboLen.Size = New System.Drawing.Size(54, 21)
        Me.ComboLen.TabIndex = 109
        Me.ToolTip.SetToolTip(Me.ComboLen, "Общая длина для записи")
        '
        'ButtonWriteSDO
        '
        Me.ButtonWriteSDO.Dock = System.Windows.Forms.DockStyle.Fill
        Me.ButtonWriteSDO.Location = New System.Drawing.Point(423, 63)
        Me.ButtonWriteSDO.Name = "ButtonWriteSDO"
        Me.ButtonWriteSDO.Size = New System.Drawing.Size(175, 24)
        Me.ButtonWriteSDO.TabIndex = 101
        Me.ButtonWriteSDO.Text = "Записать SDO"
        Me.ToolTip.SetToolTip(Me.ButtonWriteSDO, "Записать назначенные данные")
        '
        'TextD3
        '
        Me.TextD3.Dock = System.Windows.Forms.DockStyle.Top
        Me.TextD3.Location = New System.Drawing.Point(363, 63)
        Me.TextD3.MaxLength = 2
        Me.TextD3.Name = "TextD3"
        Me.TextD3.Size = New System.Drawing.Size(54, 20)
        Me.TextD3.TabIndex = 102
        Me.TextD3.Text = "00"
        Me.ToolTip.SetToolTip(Me.TextD3, "Byte 3")
        '
        'LabelLen
        '
        Me.TableLayoutPanelSDOWrite.SetColumnSpan(Me.LabelLen, 2)
        Me.LabelLen.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.LabelLen.Location = New System.Drawing.Point(363, 10)
        Me.LabelLen.Name = "LabelLen"
        Me.LabelLen.Size = New System.Drawing.Size(235, 20)
        Me.LabelLen.TabIndex = 112
        Me.LabelLen.Text = "Len" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10)
        '
        'TextD2
        '
        Me.TextD2.Dock = System.Windows.Forms.DockStyle.Top
        Me.TextD2.Location = New System.Drawing.Point(303, 63)
        Me.TextD2.MaxLength = 2
        Me.TextD2.Name = "TextD2"
        Me.TextD2.Size = New System.Drawing.Size(54, 20)
        Me.TextD2.TabIndex = 103
        Me.TextD2.Text = "00"
        Me.ToolTip.SetToolTip(Me.TextD2, "Byte 2")
        '
        'LabelSubIndex
        '
        Me.TableLayoutPanelSDOWrite.SetColumnSpan(Me.LabelSubIndex, 2)
        Me.LabelSubIndex.Dock = System.Windows.Forms.DockStyle.Fill
        Me.LabelSubIndex.Location = New System.Drawing.Point(243, 0)
        Me.LabelSubIndex.Name = "LabelSubIndex"
        Me.LabelSubIndex.Size = New System.Drawing.Size(114, 30)
        Me.LabelSubIndex.TabIndex = 110
        Me.LabelSubIndex.Text = "SubIndex (hex)" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & " 00~FF"
        '
        'TextD1
        '
        Me.TextD1.Dock = System.Windows.Forms.DockStyle.Top
        Me.TextD1.Location = New System.Drawing.Point(243, 63)
        Me.TextD1.MaxLength = 2
        Me.TextD1.Name = "TextD1"
        Me.TextD1.Size = New System.Drawing.Size(54, 20)
        Me.TextD1.TabIndex = 104
        Me.TextD1.Text = "00"
        Me.ToolTip.SetToolTip(Me.TextD1, "Byte 1")
        '
        'TextIndexWrite
        '
        Me.TextIndexWrite.Dock = System.Windows.Forms.DockStyle.Top
        Me.TextIndexWrite.Location = New System.Drawing.Point(3, 33)
        Me.TextIndexWrite.MaxLength = 4
        Me.TextIndexWrite.Name = "TextIndexWrite"
        Me.TextIndexWrite.Size = New System.Drawing.Size(174, 20)
        Me.TextIndexWrite.TabIndex = 107
        Me.TextIndexWrite.Text = "1000"
        Me.ToolTip.SetToolTip(Me.TextIndexWrite, "Индекс в словаре объектов")
        '
        'TextD0
        '
        Me.TextD0.Dock = System.Windows.Forms.DockStyle.Top
        Me.TextD0.Location = New System.Drawing.Point(183, 63)
        Me.TextD0.MaxLength = 2
        Me.TextD0.Name = "TextD0"
        Me.TextD0.Size = New System.Drawing.Size(54, 20)
        Me.TextD0.TabIndex = 105
        Me.TextD0.Text = "00"
        Me.ToolTip.SetToolTip(Me.TextD0, "Byte 0")
        '
        'TextSubIndexWrite
        '
        Me.TextSubIndexWrite.Dock = System.Windows.Forms.DockStyle.Top
        Me.TextSubIndexWrite.Location = New System.Drawing.Point(243, 33)
        Me.TextSubIndexWrite.MaxLength = 2
        Me.TextSubIndexWrite.Name = "TextSubIndexWrite"
        Me.TextSubIndexWrite.Size = New System.Drawing.Size(54, 20)
        Me.TextSubIndexWrite.TabIndex = 106
        Me.TextSubIndexWrite.Text = "00"
        Me.ToolTip.SetToolTip(Me.TextSubIndexWrite, "Подиндекс в словаре объектов")
        '
        'LabelDataHex
        '
        Me.LabelDataHex.Dock = System.Windows.Forms.DockStyle.Top
        Me.LabelDataHex.Location = New System.Drawing.Point(3, 60)
        Me.LabelDataHex.Name = "LabelDataHex"
        Me.LabelDataHex.Size = New System.Drawing.Size(174, 23)
        Me.LabelDataHex.TabIndex = 111
        Me.LabelDataHex.Text = "Data (hex):"
        Me.LabelDataHex.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'LabelSDOWrite
        '
        Me.LabelSDOWrite.BackColor = System.Drawing.Color.LightSteelBlue
        Me.LabelSDOWrite.Dock = System.Windows.Forms.DockStyle.Top
        Me.LabelSDOWrite.ForeColor = System.Drawing.Color.Black
        Me.LabelSDOWrite.Location = New System.Drawing.Point(3, 3)
        Me.LabelSDOWrite.Name = "LabelSDOWrite"
        Me.LabelSDOWrite.Size = New System.Drawing.Size(601, 22)
        Me.LabelSDOWrite.TabIndex = 119
        Me.LabelSDOWrite.Text = "Запись в сервисные объекты данных"
        Me.LabelSDOWrite.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'TabPageSDORead
        '
        Me.TabPageSDORead.Controls.Add(Me.TableLayoutPanelSDORead)
        Me.TabPageSDORead.Controls.Add(Me.LabelSdoRead)
        Me.TabPageSDORead.ImageKey = "SDO_Read"
        Me.TabPageSDORead.Location = New System.Drawing.Point(4, 24)
        Me.TabPageSDORead.Name = "TabPageSDORead"
        Me.TabPageSDORead.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPageSDORead.Size = New System.Drawing.Size(607, 470)
        Me.TabPageSDORead.TabIndex = 2
        Me.TabPageSDORead.Text = "SDO_Read"
        Me.ToolTip.SetToolTip(Me.TabPageSDORead, "Чтение значений сервисных объектов данных")
        Me.TabPageSDORead.ToolTipText = "Чтение значений сервисных объектов данных"
        Me.TabPageSDORead.UseVisualStyleBackColor = True
        '
        'TableLayoutPanelSDORead
        '
        Me.TableLayoutPanelSDORead.ColumnCount = 5
        Me.TableLayoutPanelSDORead.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20.0!))
        Me.TableLayoutPanelSDORead.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20.0!))
        Me.TableLayoutPanelSDORead.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20.0!))
        Me.TableLayoutPanelSDORead.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20.0!))
        Me.TableLayoutPanelSDORead.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20.0!))
        Me.TableLayoutPanelSDORead.Controls.Add(Me.LabelSubIndex2, 2, 0)
        Me.TableLayoutPanelSDORead.Controls.Add(Me.TextIndexRead, 0, 1)
        Me.TableLayoutPanelSDORead.Controls.Add(Me.TextSubIndexRead, 2, 1)
        Me.TableLayoutPanelSDORead.Controls.Add(Me.ButtonReadSDO, 4, 1)
        Me.TableLayoutPanelSDORead.Controls.Add(Me.ButtonClearRead, 4, 3)
        Me.TableLayoutPanelSDORead.Controls.Add(Me.ListSDODataRead, 0, 2)
        Me.TableLayoutPanelSDORead.Controls.Add(Me.LabelIndexHex2, 0, 0)
        Me.TableLayoutPanelSDORead.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanelSDORead.Location = New System.Drawing.Point(3, 25)
        Me.TableLayoutPanelSDORead.Name = "TableLayoutPanelSDORead"
        Me.TableLayoutPanelSDORead.RowCount = 4
        Me.TableLayoutPanelSDORead.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30.0!))
        Me.TableLayoutPanelSDORead.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30.0!))
        Me.TableLayoutPanelSDORead.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanelSDORead.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30.0!))
        Me.TableLayoutPanelSDORead.Size = New System.Drawing.Size(601, 442)
        Me.TableLayoutPanelSDORead.TabIndex = 0
        '
        'LabelSubIndex2
        '
        Me.TableLayoutPanelSDORead.SetColumnSpan(Me.LabelSubIndex2, 2)
        Me.LabelSubIndex2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.LabelSubIndex2.Location = New System.Drawing.Point(243, 0)
        Me.LabelSubIndex2.Name = "LabelSubIndex2"
        Me.LabelSubIndex2.Size = New System.Drawing.Size(234, 30)
        Me.LabelSubIndex2.TabIndex = 52
        Me.LabelSubIndex2.Text = "SubIndex (hex)" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & " 00 ~ FF" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10)
        '
        'TextIndexRead
        '
        Me.TextIndexRead.Location = New System.Drawing.Point(3, 33)
        Me.TextIndexRead.MaxLength = 4
        Me.TextIndexRead.Name = "TextIndexRead"
        Me.TextIndexRead.Size = New System.Drawing.Size(68, 20)
        Me.TextIndexRead.TabIndex = 53
        Me.TextIndexRead.Text = "1000"
        Me.ToolTip.SetToolTip(Me.TextIndexRead, "Индекс в словаре объектов")
        '
        'TextSubIndexRead
        '
        Me.TextSubIndexRead.Location = New System.Drawing.Point(243, 33)
        Me.TextSubIndexRead.MaxLength = 2
        Me.TextSubIndexRead.Name = "TextSubIndexRead"
        Me.TextSubIndexRead.Size = New System.Drawing.Size(37, 20)
        Me.TextSubIndexRead.TabIndex = 54
        Me.TextSubIndexRead.Text = "00"
        Me.ToolTip.SetToolTip(Me.TextSubIndexRead, "Подиндекс в словаре объектов")
        '
        'ButtonReadSDO
        '
        Me.ButtonReadSDO.Dock = System.Windows.Forms.DockStyle.Fill
        Me.ButtonReadSDO.Location = New System.Drawing.Point(483, 33)
        Me.ButtonReadSDO.Name = "ButtonReadSDO"
        Me.ButtonReadSDO.Size = New System.Drawing.Size(115, 24)
        Me.ButtonReadSDO.TabIndex = 55
        Me.ButtonReadSDO.Text = "Считать SDO"
        Me.ToolTip.SetToolTip(Me.ButtonReadSDO, "Считать данные")
        '
        'ButtonClearRead
        '
        Me.ButtonClearRead.Dock = System.Windows.Forms.DockStyle.Fill
        Me.ButtonClearRead.Location = New System.Drawing.Point(483, 415)
        Me.ButtonClearRead.Name = "ButtonClearRead"
        Me.ButtonClearRead.Size = New System.Drawing.Size(115, 24)
        Me.ButtonClearRead.TabIndex = 90
        Me.ButtonClearRead.Text = "Очистить"
        Me.ToolTip.SetToolTip(Me.ButtonClearRead, "Очистить протокол")
        '
        'ListSDODataRead
        '
        Me.TableLayoutPanelSDORead.SetColumnSpan(Me.ListSDODataRead, 5)
        Me.ListSDODataRead.Dock = System.Windows.Forms.DockStyle.Fill
        Me.ListSDODataRead.HorizontalScrollbar = True
        Me.ListSDODataRead.Location = New System.Drawing.Point(3, 63)
        Me.ListSDODataRead.Name = "ListSDODataRead"
        Me.ListSDODataRead.Size = New System.Drawing.Size(595, 346)
        Me.ListSDODataRead.TabIndex = 91
        Me.ToolTip.SetToolTip(Me.ListSDODataRead, "Протокол результатов сообщений")
        '
        'LabelIndexHex2
        '
        Me.TableLayoutPanelSDORead.SetColumnSpan(Me.LabelIndexHex2, 2)
        Me.LabelIndexHex2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.LabelIndexHex2.Location = New System.Drawing.Point(3, 0)
        Me.LabelIndexHex2.Name = "LabelIndexHex2"
        Me.LabelIndexHex2.Size = New System.Drawing.Size(234, 30)
        Me.LabelIndexHex2.TabIndex = 51
        Me.LabelIndexHex2.Text = " Index (hex)" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "0000 ~ FFFF" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10)
        '
        'LabelSdoRead
        '
        Me.LabelSdoRead.BackColor = System.Drawing.Color.LightSteelBlue
        Me.LabelSdoRead.Dock = System.Windows.Forms.DockStyle.Top
        Me.LabelSdoRead.ForeColor = System.Drawing.Color.Black
        Me.LabelSdoRead.Location = New System.Drawing.Point(3, 3)
        Me.LabelSdoRead.Name = "LabelSdoRead"
        Me.LabelSdoRead.Size = New System.Drawing.Size(601, 22)
        Me.LabelSdoRead.TabIndex = 120
        Me.LabelSdoRead.Text = "Чтение из сервисных объектов данных"
        Me.LabelSdoRead.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'TabPageNMT_Protocol
        '
        Me.TabPageNMT_Protocol.Controls.Add(Me.TableLayoutPanelNMTProtocol)
        Me.TabPageNMT_Protocol.Controls.Add(Me.LabelNMTProtocol)
        Me.TabPageNMT_Protocol.ImageKey = "NMT_Protocol"
        Me.TabPageNMT_Protocol.Location = New System.Drawing.Point(4, 24)
        Me.TabPageNMT_Protocol.Name = "TabPageNMT_Protocol"
        Me.TabPageNMT_Protocol.Size = New System.Drawing.Size(607, 470)
        Me.TabPageNMT_Protocol.TabIndex = 3
        Me.TabPageNMT_Protocol.Text = "NMT_Protocol"
        Me.ToolTip.SetToolTip(Me.TabPageNMT_Protocol, "Чтение значений объектов сетевого менеджмента")
        Me.TabPageNMT_Protocol.ToolTipText = "Чтение значений объектов сетевого менеджмента"
        Me.TabPageNMT_Protocol.UseVisualStyleBackColor = True
        '
        'TableLayoutPanelNMTProtocol
        '
        Me.TableLayoutPanelNMTProtocol.ColumnCount = 5
        Me.TableLayoutPanelNMTProtocol.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 30.0!))
        Me.TableLayoutPanelNMTProtocol.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 5.0!))
        Me.TableLayoutPanelNMTProtocol.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 30.0!))
        Me.TableLayoutPanelNMTProtocol.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 5.0!))
        Me.TableLayoutPanelNMTProtocol.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 30.0!))
        Me.TableLayoutPanelNMTProtocol.Controls.Add(Me.ButtonClearNMTProtocol, 4, 6)
        Me.TableLayoutPanelNMTProtocol.Controls.Add(Me.ListEMCY, 0, 5)
        Me.TableLayoutPanelNMTProtocol.Controls.Add(Me.LabelTimeTic, 0, 3)
        Me.TableLayoutPanelNMTProtocol.Controls.Add(Me.TextHeartbeat, 0, 4)
        Me.TableLayoutPanelNMTProtocol.Controls.Add(Me.LabelTimeWaite, 2, 3)
        Me.TableLayoutPanelNMTProtocol.Controls.Add(Me.TextConsumer, 2, 4)
        Me.TableLayoutPanelNMTProtocol.Controls.Add(Me.ButtonSetHeartbeat, 4, 4)
        Me.TableLayoutPanelNMTProtocol.Controls.Add(Me.TextGuardTime, 0, 2)
        Me.TableLayoutPanelNMTProtocol.Controls.Add(Me.TextLifeTime, 2, 2)
        Me.TableLayoutPanelNMTProtocol.Controls.Add(Me.ButtonSetGuard, 4, 2)
        Me.TableLayoutPanelNMTProtocol.Controls.Add(Me.LabelTimeWathDog, 0, 1)
        Me.TableLayoutPanelNMTProtocol.Controls.Add(Me.LabelFactorLive, 2, 1)
        Me.TableLayoutPanelNMTProtocol.Controls.Add(Me.LabelModeSlaveNode, 0, 0)
        Me.TableLayoutPanelNMTProtocol.Controls.Add(Me.ButtonSetState, 4, 0)
        Me.TableLayoutPanelNMTProtocol.Controls.Add(Me.ComboState, 2, 0)
        Me.TableLayoutPanelNMTProtocol.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanelNMTProtocol.Location = New System.Drawing.Point(0, 22)
        Me.TableLayoutPanelNMTProtocol.Name = "TableLayoutPanelNMTProtocol"
        Me.TableLayoutPanelNMTProtocol.RowCount = 7
        Me.TableLayoutPanelNMTProtocol.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 50.0!))
        Me.TableLayoutPanelNMTProtocol.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 32.0!))
        Me.TableLayoutPanelNMTProtocol.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 50.0!))
        Me.TableLayoutPanelNMTProtocol.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 32.0!))
        Me.TableLayoutPanelNMTProtocol.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 50.0!))
        Me.TableLayoutPanelNMTProtocol.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanelNMTProtocol.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30.0!))
        Me.TableLayoutPanelNMTProtocol.Size = New System.Drawing.Size(607, 448)
        Me.TableLayoutPanelNMTProtocol.TabIndex = 1
        '
        'ButtonClearNMTProtocol
        '
        Me.ButtonClearNMTProtocol.Dock = System.Windows.Forms.DockStyle.Fill
        Me.ButtonClearNMTProtocol.Location = New System.Drawing.Point(427, 421)
        Me.ButtonClearNMTProtocol.Name = "ButtonClearNMTProtocol"
        Me.ButtonClearNMTProtocol.Size = New System.Drawing.Size(177, 24)
        Me.ButtonClearNMTProtocol.TabIndex = 90
        Me.ButtonClearNMTProtocol.Text = "Очистить"
        Me.ToolTip.SetToolTip(Me.ButtonClearNMTProtocol, "Очистить протокол")
        '
        'ListEMCY
        '
        Me.TableLayoutPanelNMTProtocol.SetColumnSpan(Me.ListEMCY, 5)
        Me.ListEMCY.Dock = System.Windows.Forms.DockStyle.Fill
        Me.ListEMCY.HorizontalScrollbar = True
        Me.ListEMCY.Location = New System.Drawing.Point(3, 217)
        Me.ListEMCY.Name = "ListEMCY"
        Me.ListEMCY.Size = New System.Drawing.Size(601, 198)
        Me.ListEMCY.TabIndex = 91
        Me.ToolTip.SetToolTip(Me.ListEMCY, "Протокол результатов сообщений")
        '
        'LabelTimeTic
        '
        Me.LabelTimeTic.Dock = System.Windows.Forms.DockStyle.Fill
        Me.LabelTimeTic.Location = New System.Drawing.Point(3, 132)
        Me.LabelTimeTic.Name = "LabelTimeTic"
        Me.LabelTimeTic.Size = New System.Drawing.Size(176, 32)
        Me.LabelTimeTic.TabIndex = 95
        Me.LabelTimeTic.Text = "Время контрольного тактирования (ms)"
        Me.LabelTimeTic.TextAlign = System.Drawing.ContentAlignment.BottomLeft
        '
        'TextHeartbeat
        '
        Me.TextHeartbeat.Location = New System.Drawing.Point(3, 167)
        Me.TextHeartbeat.MaxLength = 5
        Me.TextHeartbeat.Name = "TextHeartbeat"
        Me.TextHeartbeat.Size = New System.Drawing.Size(91, 20)
        Me.TextHeartbeat.TabIndex = 96
        Me.TextHeartbeat.Text = "1000"
        Me.ToolTip.SetToolTip(Me.TextHeartbeat, "Время отслеживания контрольного тактирования")
        '
        'LabelTimeWaite
        '
        Me.LabelTimeWaite.Dock = System.Windows.Forms.DockStyle.Fill
        Me.LabelTimeWaite.Location = New System.Drawing.Point(215, 132)
        Me.LabelTimeWaite.Name = "LabelTimeWaite"
        Me.LabelTimeWaite.Size = New System.Drawing.Size(176, 32)
        Me.LabelTimeWaite.TabIndex = 97
        Me.LabelTimeWaite.Text = "Ожидать (ms)"
        Me.LabelTimeWaite.TextAlign = System.Drawing.ContentAlignment.BottomLeft
        '
        'TextConsumer
        '
        Me.TextConsumer.Location = New System.Drawing.Point(215, 167)
        Me.TextConsumer.MaxLength = 5
        Me.TextConsumer.Name = "TextConsumer"
        Me.TextConsumer.Size = New System.Drawing.Size(91, 20)
        Me.TextConsumer.TabIndex = 98
        Me.TextConsumer.Text = "2500"
        Me.ToolTip.SetToolTip(Me.TextConsumer, "Время ожидания от мастера")
        '
        'ButtonSetHeartbeat
        '
        Me.ButtonSetHeartbeat.Dock = System.Windows.Forms.DockStyle.Fill
        Me.ButtonSetHeartbeat.Location = New System.Drawing.Point(427, 167)
        Me.ButtonSetHeartbeat.Name = "ButtonSetHeartbeat"
        Me.ButtonSetHeartbeat.Size = New System.Drawing.Size(177, 44)
        Me.ButtonSetHeartbeat.TabIndex = 99
        Me.ButtonSetHeartbeat.Text = "Назначить контр. тактирование"
        Me.ToolTip.SetToolTip(Me.ButtonSetHeartbeat, "Установка отслеживания контрольного тактирования")
        '
        'TextGuardTime
        '
        Me.TextGuardTime.Location = New System.Drawing.Point(3, 85)
        Me.TextGuardTime.MaxLength = 5
        Me.TextGuardTime.Name = "TextGuardTime"
        Me.TextGuardTime.Size = New System.Drawing.Size(91, 20)
        Me.TextGuardTime.TabIndex = 100
        Me.TextGuardTime.Text = "1000"
        Me.ToolTip.SetToolTip(Me.TextGuardTime, "Время отслеживания караульного узла")
        '
        'TextLifeTime
        '
        Me.TextLifeTime.Location = New System.Drawing.Point(215, 85)
        Me.TextLifeTime.MaxLength = 3
        Me.TextLifeTime.Name = "TextLifeTime"
        Me.TextLifeTime.Size = New System.Drawing.Size(91, 20)
        Me.TextLifeTime.TabIndex = 101
        Me.TextLifeTime.Text = "2"
        Me.ToolTip.SetToolTip(Me.TextLifeTime, "Установка количества периодов отслеживания караульного узла")
        '
        'ButtonSetGuard
        '
        Me.ButtonSetGuard.Dock = System.Windows.Forms.DockStyle.Fill
        Me.ButtonSetGuard.Location = New System.Drawing.Point(427, 85)
        Me.ButtonSetGuard.Name = "ButtonSetGuard"
        Me.ButtonSetGuard.Size = New System.Drawing.Size(177, 44)
        Me.ButtonSetGuard.TabIndex = 102
        Me.ButtonSetGuard.Text = "Назначить караульный узел"
        Me.ToolTip.SetToolTip(Me.ButtonSetGuard, "Установка отслеживания караульного узла")
        '
        'LabelTimeWathDog
        '
        Me.LabelTimeWathDog.Dock = System.Windows.Forms.DockStyle.Fill
        Me.LabelTimeWathDog.Location = New System.Drawing.Point(3, 50)
        Me.LabelTimeWathDog.Name = "LabelTimeWathDog"
        Me.LabelTimeWathDog.Size = New System.Drawing.Size(176, 32)
        Me.LabelTimeWathDog.TabIndex = 103
        Me.LabelTimeWathDog.Text = "Время караульного узла (ms)"
        Me.LabelTimeWathDog.TextAlign = System.Drawing.ContentAlignment.BottomLeft
        '
        'LabelFactorLive
        '
        Me.LabelFactorLive.Dock = System.Windows.Forms.DockStyle.Fill
        Me.LabelFactorLive.Location = New System.Drawing.Point(215, 50)
        Me.LabelFactorLive.Name = "LabelFactorLive"
        Me.LabelFactorLive.Size = New System.Drawing.Size(176, 32)
        Me.LabelFactorLive.TabIndex = 104
        Me.LabelFactorLive.Text = "Период фактора Времени Жизни"
        Me.LabelFactorLive.TextAlign = System.Drawing.ContentAlignment.BottomLeft
        '
        'LabelModeSlaveNode
        '
        Me.LabelModeSlaveNode.Dock = System.Windows.Forms.DockStyle.Fill
        Me.LabelModeSlaveNode.Location = New System.Drawing.Point(3, 0)
        Me.LabelModeSlaveNode.Name = "LabelModeSlaveNode"
        Me.LabelModeSlaveNode.Size = New System.Drawing.Size(176, 50)
        Me.LabelModeSlaveNode.TabIndex = 105
        Me.LabelModeSlaveNode.Text = "Состояние ведомого узла:"
        Me.LabelModeSlaveNode.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'ButtonSetState
        '
        Me.ButtonSetState.Dock = System.Windows.Forms.DockStyle.Fill
        Me.ButtonSetState.Location = New System.Drawing.Point(427, 3)
        Me.ButtonSetState.Name = "ButtonSetState"
        Me.ButtonSetState.Size = New System.Drawing.Size(177, 44)
        Me.ButtonSetState.TabIndex = 107
        Me.ButtonSetState.Text = "Назначить Состояние"
        Me.ToolTip.SetToolTip(Me.ButtonSetState, "Изменить состояние ведомого узла")
        '
        'ComboState
        '
        Me.ComboState.Dock = System.Windows.Forms.DockStyle.Top
        Me.ComboState.Items.AddRange(New Object() {"Operation", "Stop", "Pre-Operation", "Reset Node", "Reset Comunication"})
        Me.ComboState.Location = New System.Drawing.Point(215, 3)
        Me.ComboState.Name = "ComboState"
        Me.ComboState.Size = New System.Drawing.Size(176, 21)
        Me.ComboState.TabIndex = 106
        Me.ToolTip.SetToolTip(Me.ComboState, "Выбор состояние ведомого узла")
        '
        'LabelNMTProtocol
        '
        Me.LabelNMTProtocol.BackColor = System.Drawing.Color.LightSteelBlue
        Me.LabelNMTProtocol.Dock = System.Windows.Forms.DockStyle.Top
        Me.LabelNMTProtocol.ForeColor = System.Drawing.Color.Black
        Me.LabelNMTProtocol.Location = New System.Drawing.Point(0, 0)
        Me.LabelNMTProtocol.Name = "LabelNMTProtocol"
        Me.LabelNMTProtocol.Size = New System.Drawing.Size(607, 22)
        Me.LabelNMTProtocol.TabIndex = 121
        Me.LabelNMTProtocol.Text = "Чтение данных из объектов сетевого менеджмента"
        Me.LabelNMTProtocol.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'LedInterrupter
        '
        Me.LedInterrupter.Caption = "Отсечка"
        Me.LedInterrupter.Dock = System.Windows.Forms.DockStyle.Fill
        Me.LedInterrupter.LedStyle = NationalInstruments.UI.LedStyle.Square3D
        Me.LedInterrupter.Location = New System.Drawing.Point(3, 3)
        Me.LedInterrupter.Name = "LedInterrupter"
        Me.LedInterrupter.OffColor = System.Drawing.Color.Maroon
        Me.LedInterrupter.OnColor = System.Drawing.Color.Red
        Me.LedInterrupter.Size = New System.Drawing.Size(96, 74)
        Me.LedInterrupter.TabIndex = 16
        Me.ToolTip.SetToolTip(Me.LedInterrupter, "Индикатор отсечек углов")
        '
        'ToolStripContainer
        '
        '
        'ToolStripContainer.BottomToolStripPanel
        '
        Me.ToolStripContainer.BottomToolStripPanel.Controls.Add(Me.StatusStrip)
        '
        'ToolStripContainer.ContentPanel
        '
        Me.ToolStripContainer.ContentPanel.Controls.Add(Me.SplitContainer)
        Me.ToolStripContainer.ContentPanel.Controls.Add(Me.PanelGauge)
        Me.ToolStripContainer.ContentPanel.Size = New System.Drawing.Size(1302, 498)
        Me.ToolStripContainer.Dock = System.Windows.Forms.DockStyle.Fill
        Me.ToolStripContainer.Location = New System.Drawing.Point(0, 0)
        Me.ToolStripContainer.Name = "ToolStripContainer"
        Me.ToolStripContainer.Size = New System.Drawing.Size(1302, 545)
        Me.ToolStripContainer.TabIndex = 7
        Me.ToolStripContainer.Text = "ToolStripContainer1"
        '
        'ToolStripContainer.TopToolStripPanel
        '
        Me.ToolStripContainer.TopToolStripPanel.Controls.Add(Me.ToolStrip)
        '
        'SplitContainer
        '
        Me.SplitContainer.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainer.FixedPanel = System.Windows.Forms.FixedPanel.Panel1
        Me.SplitContainer.Location = New System.Drawing.Point(0, 0)
        Me.SplitContainer.Name = "SplitContainer"
        '
        'SplitContainer.Panel1
        '
        Me.SplitContainer.Panel1.Controls.Add(Me.TreeView)
        '
        'SplitContainer.Panel2
        '
        Me.SplitContainer.Panel2.Controls.Add(Me.TabControlSetting)
        Me.SplitContainer.Size = New System.Drawing.Size(786, 498)
        Me.SplitContainer.SplitterDistance = 167
        Me.SplitContainer.TabIndex = 0
        Me.SplitContainer.Text = "SplitContainer1"
        '
        'TabControlSetting
        '
        Me.TabControlSetting.Controls.Add(Me.TabPageMesage)
        Me.TabControlSetting.Controls.Add(Me.TabPageSetting)
        Me.TabControlSetting.Controls.Add(Me.TabPageSDO_Write)
        Me.TabControlSetting.Controls.Add(Me.TabPageSDORead)
        Me.TabControlSetting.Controls.Add(Me.TabPageNMT_Protocol)
        Me.TabControlSetting.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TabControlSetting.ImageList = Me.ImageListViewSmall
        Me.TabControlSetting.Location = New System.Drawing.Point(0, 0)
        Me.TabControlSetting.Name = "TabControlSetting"
        Me.TabControlSetting.SelectedIndex = 0
        Me.TabControlSetting.Size = New System.Drawing.Size(615, 498)
        Me.TabControlSetting.TabIndex = 0
        '
        'TabPageMesage
        '
        Me.TabPageMesage.Controls.Add(Me.ListViewAlarm)
        Me.TabPageMesage.Controls.Add(Me.LabelCaptionGrid)
        Me.TabPageMesage.ImageKey = "message"
        Me.TabPageMesage.Location = New System.Drawing.Point(4, 24)
        Me.TabPageMesage.Name = "TabPageMesage"
        Me.TabPageMesage.Size = New System.Drawing.Size(607, 470)
        Me.TabPageMesage.TabIndex = 4
        Me.TabPageMesage.Text = "Сообщения"
        Me.TabPageMesage.UseVisualStyleBackColor = True
        '
        'ListViewAlarm
        '
        Me.ListViewAlarm.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.ListViewAlarm.Dock = System.Windows.Forms.DockStyle.Fill
        Me.ListViewAlarm.Location = New System.Drawing.Point(0, 22)
        Me.ListViewAlarm.MultiSelect = False
        Me.ListViewAlarm.Name = "ListViewAlarm"
        Me.ListViewAlarm.Size = New System.Drawing.Size(607, 448)
        Me.ListViewAlarm.TabIndex = 34
        Me.ListViewAlarm.UseCompatibleStateImageBehavior = False
        Me.ListViewAlarm.View = System.Windows.Forms.View.SmallIcon
        '
        'LabelCaptionGrid
        '
        Me.LabelCaptionGrid.BackColor = System.Drawing.Color.LightSteelBlue
        Me.LabelCaptionGrid.Dock = System.Windows.Forms.DockStyle.Top
        Me.LabelCaptionGrid.ForeColor = System.Drawing.Color.Black
        Me.LabelCaptionGrid.Location = New System.Drawing.Point(0, 0)
        Me.LabelCaptionGrid.Name = "LabelCaptionGrid"
        Me.LabelCaptionGrid.Size = New System.Drawing.Size(607, 22)
        Me.LabelCaptionGrid.TabIndex = 33
        Me.LabelCaptionGrid.Text = "Сообщения оператору"
        Me.LabelCaptionGrid.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'ImageListViewSmall
        '
        Me.ImageListViewSmall.ImageStream = CType(resources.GetObject("ImageListViewSmall.ImageStream"), System.Windows.Forms.ImageListStreamer)
        Me.ImageListViewSmall.TransparentColor = System.Drawing.Color.Transparent
        Me.ImageListViewSmall.Images.SetKeyName(0, "Initialize")
        Me.ImageListViewSmall.Images.SetKeyName(1, "SDO_Write")
        Me.ImageListViewSmall.Images.SetKeyName(2, "SDO_Read")
        Me.ImageListViewSmall.Images.SetKeyName(3, "NMT_Protocol")
        Me.ImageListViewSmall.Images.SetKeyName(4, "message")
        '
        'PanelGauge
        '
        Me.PanelGauge.Controls.Add(Me.TableLayoutPanelGauge)
        Me.PanelGauge.Dock = System.Windows.Forms.DockStyle.Right
        Me.PanelGauge.Location = New System.Drawing.Point(786, 0)
        Me.PanelGauge.Name = "PanelGauge"
        Me.PanelGauge.Size = New System.Drawing.Size(516, 498)
        Me.PanelGauge.TabIndex = 1
        '
        'TableLayoutPanelGauge
        '
        Me.TableLayoutPanelGauge.ColumnCount = 4
        Me.TableLayoutPanelGauge.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanelGauge.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 140.0!))
        Me.TableLayoutPanelGauge.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanelGauge.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 102.0!))
        Me.TableLayoutPanelGauge.Controls.Add(Me.NumericEditAngularPositionEncoder, 1, 1)
        Me.TableLayoutPanelGauge.Controls.Add(Me.GaugeAngularPositionEncoder, 0, 0)
        Me.TableLayoutPanelGauge.Controls.Add(Me.NumericEditEncoderThermometer, 3, 1)
        Me.TableLayoutPanelGauge.Controls.Add(Me.TableLayoutPanelTermometer, 3, 0)
        Me.TableLayoutPanelGauge.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanelGauge.Location = New System.Drawing.Point(0, 0)
        Me.TableLayoutPanelGauge.Name = "TableLayoutPanelGauge"
        Me.TableLayoutPanelGauge.RowCount = 2
        Me.TableLayoutPanelGauge.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanelGauge.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 45.0!))
        Me.TableLayoutPanelGauge.Size = New System.Drawing.Size(516, 498)
        Me.TableLayoutPanelGauge.TabIndex = 15
        '
        'NumericEditAngularPositionEncoder
        '
        Me.NumericEditAngularPositionEncoder.BackColor = System.Drawing.SystemColors.Control
        Me.NumericEditAngularPositionEncoder.Dock = System.Windows.Forms.DockStyle.Fill
        Me.NumericEditAngularPositionEncoder.Font = New System.Drawing.Font("Microsoft Sans Serif", 20.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(204, Byte))
        Me.NumericEditAngularPositionEncoder.FormatMode = NationalInstruments.UI.NumericFormatMode.CreateSimpleDoubleMode(2)
        Me.NumericEditAngularPositionEncoder.Location = New System.Drawing.Point(140, 456)
        Me.NumericEditAngularPositionEncoder.Name = "NumericEditAngularPositionEncoder"
        Me.NumericEditAngularPositionEncoder.Size = New System.Drawing.Size(134, 38)
        Me.NumericEditAngularPositionEncoder.TabIndex = 14
        Me.NumericEditAngularPositionEncoder.UpDownAlign = System.Windows.Forms.LeftRightAlignment.Left
        '
        'GaugeAngularPositionEncoder
        '
        Me.GaugeAngularPositionEncoder.AutoDivisionSpacing = False
        Me.GaugeAngularPositionEncoder.Caption = "Положение турели"
        Me.TableLayoutPanelGauge.SetColumnSpan(Me.GaugeAngularPositionEncoder, 3)
        ScaleCustomDivision1.LabelFont = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(204, Byte))
        ScaleCustomDivision1.LineWidth = 2.0!
        ScaleCustomDivision1.Text = "Начало"
        ScaleCustomDivision1.TickLength = 10.0!
        ScaleCustomDivision1.Value = 350.0R
        Me.GaugeAngularPositionEncoder.CustomDivisions.AddRange(New NationalInstruments.UI.ScaleCustomDivision() {ScaleCustomDivision1})
        Me.GaugeAngularPositionEncoder.Dock = System.Windows.Forms.DockStyle.Fill
        Me.GaugeAngularPositionEncoder.Location = New System.Drawing.Point(3, 3)
        Me.GaugeAngularPositionEncoder.MajorDivisions.Interval = 10.0R
        Me.GaugeAngularPositionEncoder.Name = "GaugeAngularPositionEncoder"
        Me.GaugeAngularPositionEncoder.Range = New NationalInstruments.UI.Range(0R, 360.0R)
        ScaleRangeFill1.Range = New NationalInstruments.UI.Range(350.0R, 360.0R)
        ScaleRangeFill1.Style = NationalInstruments.UI.ScaleRangeFillStyle.CreateSolidStyle(System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(128, Byte), Integer), CType(CType(64, Byte), Integer)))
        Me.GaugeAngularPositionEncoder.RangeFills.AddRange(New NationalInstruments.UI.ScaleRangeFill() {ScaleRangeFill1})
        Me.GaugeAngularPositionEncoder.ScaleArc = New NationalInstruments.UI.Arc(90.0!, -360.0!)
        Me.GaugeAngularPositionEncoder.Size = New System.Drawing.Size(408, 447)
        Me.GaugeAngularPositionEncoder.TabIndex = 13
        '
        'NumericEditEncoderThermometer
        '
        Me.NumericEditEncoderThermometer.BackColor = System.Drawing.SystemColors.Control
        Me.NumericEditEncoderThermometer.Dock = System.Windows.Forms.DockStyle.Fill
        Me.NumericEditEncoderThermometer.Font = New System.Drawing.Font("Microsoft Sans Serif", 20.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(204, Byte))
        Me.NumericEditEncoderThermometer.FormatMode = NationalInstruments.UI.NumericFormatMode.CreateSimpleDoubleMode(0)
        Me.NumericEditEncoderThermometer.InteractionMode = NationalInstruments.UI.NumericEditInteractionModes.Indicator
        Me.NumericEditEncoderThermometer.Location = New System.Drawing.Point(417, 456)
        Me.NumericEditEncoderThermometer.Name = "NumericEditEncoderThermometer"
        Me.NumericEditEncoderThermometer.Size = New System.Drawing.Size(96, 38)
        Me.NumericEditEncoderThermometer.Source = Me.ThermometerEncoder
        Me.NumericEditEncoderThermometer.TabIndex = 16
        Me.NumericEditEncoderThermometer.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        Me.NumericEditEncoderThermometer.UpDownAlign = System.Windows.Forms.LeftRightAlignment.Left
        '
        'ThermometerEncoder
        '
        Me.ThermometerEncoder.Caption = "T °С"
        Me.ThermometerEncoder.Dock = System.Windows.Forms.DockStyle.Fill
        Me.ThermometerEncoder.FillStyle = NationalInstruments.UI.FillStyle.HorizontalGradient
        Me.ThermometerEncoder.Location = New System.Drawing.Point(3, 83)
        Me.ThermometerEncoder.Name = "ThermometerEncoder"
        Me.ThermometerEncoder.OutOfRangeMode = NationalInstruments.UI.NumericOutOfRangeMode.CoerceToRange
        Me.ThermometerEncoder.Size = New System.Drawing.Size(96, 367)
        Me.ThermometerEncoder.TabIndex = 15
        Me.ThermometerEncoder.ThermometerStyle = NationalInstruments.UI.ThermometerStyle.Flat
        Me.ThermometerEncoder.Value = 20.0R
        '
        'TableLayoutPanelTermometer
        '
        Me.TableLayoutPanelTermometer.ColumnCount = 1
        Me.TableLayoutPanelTermometer.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanelTermometer.Controls.Add(Me.ThermometerEncoder, 0, 1)
        Me.TableLayoutPanelTermometer.Controls.Add(Me.LedInterrupter, 0, 0)
        Me.TableLayoutPanelTermometer.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanelTermometer.Location = New System.Drawing.Point(414, 0)
        Me.TableLayoutPanelTermometer.Margin = New System.Windows.Forms.Padding(0)
        Me.TableLayoutPanelTermometer.Name = "TableLayoutPanelTermometer"
        Me.TableLayoutPanelTermometer.RowCount = 2
        Me.TableLayoutPanelTermometer.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 80.0!))
        Me.TableLayoutPanelTermometer.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanelTermometer.Size = New System.Drawing.Size(102, 453)
        Me.TableLayoutPanelTermometer.TabIndex = 17
        '
        'TimerMNT
        '
        '
        'ImageList1
        '
        Me.ImageList1.ImageStream = CType(resources.GetObject("ImageList1.ImageStream"), System.Windows.Forms.ImageListStreamer)
        Me.ImageList1.TransparentColor = System.Drawing.Color.Transparent
        Me.ImageList1.Images.SetKeyName(0, "ConditionUp")
        Me.ImageList1.Images.SetKeyName(1, "watchdogSet")
        Me.ImageList1.Images.SetKeyName(2, "WriteOk")
        Me.ImageList1.Images.SetKeyName(3, "ReadOk")
        Me.ImageList1.Images.SetKeyName(4, "CriterionUp")
        Me.ImageList1.Images.SetKeyName(5, "ReadOk2")
        Me.ImageList1.Images.SetKeyName(6, "InformationMessage")
        Me.ImageList1.Images.SetKeyName(7, "AlarmMessage")
        Me.ImageList1.Images.SetKeyName(8, "Номер")
        Me.ImageList1.Images.SetKeyName(9, "Сообщение")
        Me.ImageList1.Images.SetKeyName(10, "Дата")
        Me.ImageList1.Images.SetKeyName(11, "Время")
        '
        'TimerTemperature
        '
        '
        'EncoderForm
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1302, 545)
        Me.Controls.Add(Me.ToolStripContainer)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MinimumSize = New System.Drawing.Size(1024, 500)
        Me.Name = "EncoderForm"
        Me.Text = "Турель"
        Me.StatusStrip.ResumeLayout(False)
        Me.StatusStrip.PerformLayout()
        Me.ToolStrip.ResumeLayout(False)
        Me.ToolStrip.PerformLayout()
        Me.TabPageSetting.ResumeLayout(False)
        Me.TabPageSDO_Write.ResumeLayout(False)
        Me.TableLayoutPanelSDOWrite.ResumeLayout(False)
        Me.TableLayoutPanelSDOWrite.PerformLayout()
        Me.TabPageSDORead.ResumeLayout(False)
        Me.TableLayoutPanelSDORead.ResumeLayout(False)
        Me.TableLayoutPanelSDORead.PerformLayout()
        Me.TabPageNMT_Protocol.ResumeLayout(False)
        Me.TableLayoutPanelNMTProtocol.ResumeLayout(False)
        Me.TableLayoutPanelNMTProtocol.PerformLayout()
        CType(Me.LedInterrupter, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ToolStripContainer.BottomToolStripPanel.ResumeLayout(False)
        Me.ToolStripContainer.BottomToolStripPanel.PerformLayout()
        Me.ToolStripContainer.ContentPanel.ResumeLayout(False)
        Me.ToolStripContainer.TopToolStripPanel.ResumeLayout(False)
        Me.ToolStripContainer.TopToolStripPanel.PerformLayout()
        Me.ToolStripContainer.ResumeLayout(False)
        Me.ToolStripContainer.PerformLayout()
        Me.SplitContainer.Panel1.ResumeLayout(False)
        Me.SplitContainer.Panel2.ResumeLayout(False)
        CType(Me.SplitContainer, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainer.ResumeLayout(False)
        Me.TabControlSetting.ResumeLayout(False)
        Me.TabPageMesage.ResumeLayout(False)
        Me.PanelGauge.ResumeLayout(False)
        Me.TableLayoutPanelGauge.ResumeLayout(False)
        CType(Me.NumericEditAngularPositionEncoder, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GaugeAngularPositionEncoder, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.NumericEditEncoderThermometer, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ThermometerEncoder, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TableLayoutPanelTermometer.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents TabControlSetting As TabControl
    Friend WithEvents TabPageSetting As TabPage
    Friend WithEvents TabPageSDO_Write As TabPage
    Friend WithEvents ButtonAddNode As Button
    Friend WithEvents LabelComPort As Label
    Friend WithEvents LabelNodeId As Label
    Friend WithEvents ButtonInitial As Button
    Friend WithEvents ComboNode As ComboBox
    Friend WithEvents LabelSpeedRate As Label
    Friend WithEvents ComboCom As ComboBox
    Friend WithEvents ComboBaud As ComboBox
    Friend WithEvents TableLayoutPanelSDOWrite As TableLayoutPanel
    Friend WithEvents LabelIndexHex As Label
    Friend WithEvents ButtonClearWrite As Button
    Friend WithEvents ListSDODataWrite As ListBox
    Friend WithEvents ComboLen As ComboBox
    Friend WithEvents ButtonWriteSDO As Button
    Friend WithEvents TextD3 As TextBox
    Friend WithEvents LabelLen As Label
    Friend WithEvents TextD2 As TextBox
    Friend WithEvents LabelSubIndex As Label
    Friend WithEvents TextD1 As TextBox
    Friend WithEvents TextIndexWrite As TextBox
    Friend WithEvents TextD0 As TextBox
    Friend WithEvents TextSubIndexWrite As TextBox
    Friend WithEvents LabelDataHex As Label
    Friend WithEvents TabPageSDORead As TabPage
    Friend WithEvents ButtonRemove As Button
    Friend WithEvents ButtonShutdown As Button
    Friend WithEvents TableLayoutPanelSDORead As TableLayoutPanel
    Friend WithEvents LabelSubIndex2 As Label
    Friend WithEvents TextIndexRead As TextBox
    Friend WithEvents TextSubIndexRead As TextBox
    Friend WithEvents ButtonReadSDO As Button
    Friend WithEvents ButtonClearRead As Button
    Friend WithEvents ListSDODataRead As ListBox
    Friend WithEvents LabelIndexHex2 As Label
    Friend WithEvents TabPageNMT_Protocol As TabPage
    Friend WithEvents TableLayoutPanelNMTProtocol As TableLayoutPanel
    Friend WithEvents ButtonClearNMTProtocol As Button
    Friend WithEvents ListEMCY As ListBox
    Friend WithEvents LabelTimeTic As Label
    Friend WithEvents TextHeartbeat As TextBox
    Friend WithEvents LabelTimeWaite As Label
    Friend WithEvents TextConsumer As TextBox
    Friend WithEvents ButtonSetHeartbeat As Button
    Friend WithEvents TextGuardTime As TextBox
    Friend WithEvents TextLifeTime As TextBox
    Friend WithEvents ButtonSetGuard As Button
    Friend WithEvents LabelTimeWathDog As Label
    Friend WithEvents LabelFactorLive As Label
    Friend WithEvents LabelModeSlaveNode As Label
    Friend WithEvents ButtonSetState As Button
    Friend WithEvents ComboState As ComboBox
    Private WithEvents TimerMNT As Timer
    Friend WithEvents ToolStripLabel1 As ToolStripLabel
    Friend WithEvents TextPosition As ToolStripTextBox
    Friend WithEvents ButtonGetPosition As ToolStripButton
    Friend WithEvents ButtonEncoder As ToolStripButton
    Friend WithEvents PanelGauge As Panel
    Friend WithEvents NumericEditAngularPositionEncoder As NationalInstruments.UI.WindowsForms.NumericEdit
    Friend WithEvents GaugeAngularPositionEncoder As NationalInstruments.UI.WindowsForms.Gauge
    Friend WithEvents TableLayoutPanelGauge As TableLayoutPanel
    Friend WithEvents ButtonResetPosition As ToolStripButton
    Friend WithEvents ThermometerEncoder As NationalInstruments.UI.WindowsForms.Thermometer
    Friend WithEvents NumericEditEncoderThermometer As NationalInstruments.UI.WindowsForms.NumericEdit
    Friend WithEvents TabPageMesage As TabPage
    Friend WithEvents LabelCaptionGrid As Label
    Friend WithEvents ListViewAlarm As DbListView
    Friend WithEvents ImageList1 As ImageList
    Friend WithEvents LabelStatus As ToolStripStatusLabel
    Friend WithEvents TimerTemperature As Timer
    Friend WithEvents TableLayoutPanelTermometer As TableLayoutPanel
    Friend WithEvents LedInterrupter As NationalInstruments.UI.WindowsForms.Led
    Friend WithEvents LabelInitAdapter As Label
    Friend WithEvents LabelSDOWrite As Label
    Friend WithEvents LabelSdoRead As Label
    Friend WithEvents LabelNMTProtocol As Label
End Class
