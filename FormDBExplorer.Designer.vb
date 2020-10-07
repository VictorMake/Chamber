<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FormDBExplorer
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

    Friend WithEvents ToolStripContainer As System.Windows.Forms.ToolStripContainer
    Friend WithEvents ToolStripSeparator7 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents ToolStripSeparator8 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents ToolStrip As System.Windows.Forms.ToolStrip
    Friend WithEvents FoldersToolStripButton As System.Windows.Forms.ToolStripButton
    Friend WithEvents ListViewToolStripButton As System.Windows.Forms.ToolStripDropDownButton
    Friend WithEvents ListToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents DetailsToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents LargeIconsToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents SmallIconsToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents TileToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents MenuStrip As System.Windows.Forms.MenuStrip
    Friend WithEvents FileToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents CleanInputToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents SaveAsInputToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ViewToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolBarToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents StatusBarToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents FoldersToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents SplitContainerTreeInput As System.Windows.Forms.SplitContainer
    Friend WithEvents TreeViewInput As System.Windows.Forms.TreeView
    Friend WithEvents ListViewInput As System.Windows.Forms.ListView
    Friend WithEvents sbrСостояния As System.Windows.Forms.StatusStrip
    Friend WithEvents ToolTip As System.Windows.Forms.ToolTip
    Friend WithEvents ListViewLargeImageList As System.Windows.Forms.ImageList

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FormDBExplorer))
        Me.sbrСостояния = New System.Windows.Forms.StatusStrip()
        Me.ToolStripStatusLabel1 = New System.Windows.Forms.ToolStripStatusLabel()
        Me.ToolStripStatusLabel2 = New System.Windows.Forms.ToolStripStatusLabel()
        Me.ToolStripStatusLabel3 = New System.Windows.Forms.ToolStripStatusLabel()
        Me.ToolStripStatusLabel4 = New System.Windows.Forms.ToolStripStatusLabel()
        Me.ToolStripStatusLabel5 = New System.Windows.Forms.ToolStripStatusLabel()
        Me.ToolStripStatusLabel6 = New System.Windows.Forms.ToolStripStatusLabel()
        Me.TSProgressBar = New System.Windows.Forms.ToolStripProgressBar()
        Me.ToolStrip = New System.Windows.Forms.ToolStrip()
        Me.LoadToolStripButton = New System.Windows.Forms.ToolStripButton()
        Me.DeleteToolStripButton = New System.Windows.Forms.ToolStripButton()
        Me.ToolStripSeparator7 = New System.Windows.Forms.ToolStripSeparator()
        Me.FoldersToolStripButton = New System.Windows.Forms.ToolStripButton()
        Me.ToolStripSeparator8 = New System.Windows.Forms.ToolStripSeparator()
        Me.ListViewToolStripButton = New System.Windows.Forms.ToolStripDropDownButton()
        Me.ListToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.DetailsToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.LargeIconsToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.SmallIconsToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.TileToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.MenuStrip = New System.Windows.Forms.MenuStrip()
        Me.FileToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.PackInputToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.SaveAsInputToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.CleanInputToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripSeparator9 = New System.Windows.Forms.ToolStripSeparator()
        Me.PackOutputToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.SaveAsOutputToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.CleanOutputToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ViewToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolBarToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.StatusBarToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.FoldersToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolTip = New System.Windows.Forms.ToolTip(Me.components)
        Me.SplitContainerAll = New System.Windows.Forms.SplitContainer()
        Me.SplitContainerInput = New System.Windows.Forms.SplitContainer()
        Me.SplitContainerTreeInput = New System.Windows.Forms.SplitContainer()
        Me.TreeViewInput = New System.Windows.Forms.TreeView()
        Me.ContextMenuStripTreeDelete = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.TSMenuItemDelete = New System.Windows.Forms.ToolStripMenuItem()
        Me.imlSmallIcons = New System.Windows.Forms.ImageList(Me.components)
        Me.ListViewInput = New System.Windows.Forms.ListView()
        Me.ListViewLargeImageList = New System.Windows.Forms.ImageList(Me.components)
        Me.TextBoxInput = New System.Windows.Forms.TextBox()
        Me.PanelListInput = New System.Windows.Forms.Panel()
        Me.ListBoxInput = New System.Windows.Forms.ListBox()
        Me.PanelButtonsInput = New System.Windows.Forms.Panel()
        Me.ButtonMoveInputToOutput = New System.Windows.Forms.Button()
        Me.ButtonDeleteInput = New System.Windows.Forms.Button()
        Me.PanelPatchInput = New System.Windows.Forms.Panel()
        Me.ButtonReviewSource = New System.Windows.Forms.Button()
        Me.TextPathSource = New System.Windows.Forms.TextBox()
        Me.LabelPathInput = New System.Windows.Forms.Label()
        Me.SplitContainerOutput = New System.Windows.Forms.SplitContainer()
        Me.SplitContainerTreeOutput = New System.Windows.Forms.SplitContainer()
        Me.TreeViewOutput = New System.Windows.Forms.TreeView()
        Me.ListViewOutput = New System.Windows.Forms.ListView()
        Me.TextBoxOutput = New System.Windows.Forms.TextBox()
        Me.PanelListOutput = New System.Windows.Forms.Panel()
        Me.ListBoxOutput = New System.Windows.Forms.ListBox()
        Me.PanelButtonsOutput = New System.Windows.Forms.Panel()
        Me.ButtonMoveOutputToInput = New System.Windows.Forms.Button()
        Me.ButtonDeleteOutput = New System.Windows.Forms.Button()
        Me.PanelPatchOutput = New System.Windows.Forms.Panel()
        Me.ButtonReviewReceiver = New System.Windows.Forms.Button()
        Me.TextPathReceiver = New System.Windows.Forms.TextBox()
        Me.LabelPathOutput = New System.Windows.Forms.Label()
        Me.ToolStripContainer = New System.Windows.Forms.ToolStripContainer()
        Me.DialogOpen = New System.Windows.Forms.OpenFileDialog()
        Me.imlToolbarIcons = New System.Windows.Forms.ImageList(Me.components)
        Me.DialogSave = New System.Windows.Forms.SaveFileDialog()
        Me.FolderBrowserDialog1 = New System.Windows.Forms.FolderBrowserDialog()
        Me.КамераInputDataSet = New Chamber.КамераDataSet()
        Me.КамераOutputDataSet = New Chamber.КамераDataSet()
        Me.ГорелкаOutputBindingSource = New System.Windows.Forms.BindingSource(Me.components)
        Me.ГорелкаInputBindingSource = New System.Windows.Forms.BindingSource(Me.components)
        Me.ИзделиеInputBindingSource = New System.Windows.Forms.BindingSource(Me.components)
        Me.ИзделиеOutputBindingSource = New System.Windows.Forms.BindingSource(Me.components)
        Me.ГребенкаАInputBindingSource = New System.Windows.Forms.BindingSource(Me.components)
        Me.ГребенкаАOutputBindingSource = New System.Windows.Forms.BindingSource(Me.components)
        Me.ГребенкаБInputBindingSource = New System.Windows.Forms.BindingSource(Me.components)
        Me.ГребенкаБOutputBindingSource = New System.Windows.Forms.BindingSource(Me.components)
        Me.КонтрольЭДСInputBindingSource = New System.Windows.Forms.BindingSource(Me.components)
        Me.КонтрольЭДСOutputBindingSource = New System.Windows.Forms.BindingSource(Me.components)
        Me.ПолеInputBindingSource = New System.Windows.Forms.BindingSource(Me.components)
        Me.ПолеOutputBindingSource = New System.Windows.Forms.BindingSource(Me.components)
        Me.ТочкаInputBindingSource = New System.Windows.Forms.BindingSource(Me.components)
        Me.ТочкаOutputBindingSource = New System.Windows.Forms.BindingSource(Me.components)
        Me.ИзделиеInputTableAdapter = New Chamber.КамераDataSetTableAdapters.ИзделиеTableAdapter()
        Me.ГорелкаInputTableAdapter = New Chamber.КамераDataSetTableAdapters.ГорелкаTableAdapter()
        Me.ГребенкаАInputTableAdapter = New Chamber.КамераDataSetTableAdapters.ГребенкаАTableAdapter()
        Me.ГребенкаБInputTableAdapter = New Chamber.КамераDataSetTableAdapters.ГребенкаБTableAdapter()
        Me.КонтрольЭДСInputTableAdapter = New Chamber.КамераDataSetTableAdapters.КонтрольЭДСTableAdapter()
        Me.ПолеInputTableAdapter = New Chamber.КамераDataSetTableAdapters.ПолеTableAdapter()
        Me.ТочкаInputTableAdapter = New Chamber.КамераDataSetTableAdapters.ТочкаTableAdapter()
        Me.ИзделиеOutputTableAdapter = New Chamber.КамераDataSetTableAdapters.ИзделиеTableAdapter()
        Me.ГорелкаOutputTableAdapter = New Chamber.КамераDataSetTableAdapters.ГорелкаTableAdapter()
        Me.ГребенкаАOutputTableAdapter = New Chamber.КамераDataSetTableAdapters.ГребенкаАTableAdapter()
        Me.ГребенкаБOutputTableAdapter = New Chamber.КамераDataSetTableAdapters.ГребенкаБTableAdapter()
        Me.КонтрольЭДСOutputTableAdapter = New Chamber.КамераDataSetTableAdapters.КонтрольЭДСTableAdapter()
        Me.ПолеOutputTableAdapter = New Chamber.КамераDataSetTableAdapters.ПолеTableAdapter()
        Me.ТочкаOutputTableAdapter = New Chamber.КамераDataSetTableAdapters.ТочкаTableAdapter()
        Me.sbrСостояния.SuspendLayout()
        Me.ToolStrip.SuspendLayout()
        Me.MenuStrip.SuspendLayout()
        CType(Me.SplitContainerAll, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainerAll.Panel1.SuspendLayout()
        Me.SplitContainerAll.Panel2.SuspendLayout()
        Me.SplitContainerAll.SuspendLayout()
        CType(Me.SplitContainerInput, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainerInput.Panel1.SuspendLayout()
        Me.SplitContainerInput.Panel2.SuspendLayout()
        Me.SplitContainerInput.SuspendLayout()
        CType(Me.SplitContainerTreeInput, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainerTreeInput.Panel1.SuspendLayout()
        Me.SplitContainerTreeInput.Panel2.SuspendLayout()
        Me.SplitContainerTreeInput.SuspendLayout()
        Me.ContextMenuStripTreeDelete.SuspendLayout()
        Me.PanelListInput.SuspendLayout()
        Me.PanelButtonsInput.SuspendLayout()
        Me.PanelPatchInput.SuspendLayout()
        CType(Me.SplitContainerOutput, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainerOutput.Panel1.SuspendLayout()
        Me.SplitContainerOutput.Panel2.SuspendLayout()
        Me.SplitContainerOutput.SuspendLayout()
        CType(Me.SplitContainerTreeOutput, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainerTreeOutput.Panel1.SuspendLayout()
        Me.SplitContainerTreeOutput.Panel2.SuspendLayout()
        Me.SplitContainerTreeOutput.SuspendLayout()
        Me.PanelListOutput.SuspendLayout()
        Me.PanelButtonsOutput.SuspendLayout()
        Me.PanelPatchOutput.SuspendLayout()
        Me.ToolStripContainer.BottomToolStripPanel.SuspendLayout()
        Me.ToolStripContainer.ContentPanel.SuspendLayout()
        Me.ToolStripContainer.TopToolStripPanel.SuspendLayout()
        Me.ToolStripContainer.SuspendLayout()
        CType(Me.КамераInputDataSet, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.КамераOutputDataSet, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ГорелкаOutputBindingSource, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ГорелкаInputBindingSource, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ИзделиеInputBindingSource, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ИзделиеOutputBindingSource, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ГребенкаАInputBindingSource, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ГребенкаАOutputBindingSource, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ГребенкаБInputBindingSource, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ГребенкаБOutputBindingSource, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.КонтрольЭДСInputBindingSource, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.КонтрольЭДСOutputBindingSource, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ПолеInputBindingSource, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ПолеOutputBindingSource, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ТочкаInputBindingSource, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ТочкаOutputBindingSource, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'sbrСостояния
        '
        Me.sbrСостояния.Dock = System.Windows.Forms.DockStyle.None
        Me.sbrСостояния.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ToolStripStatusLabel1, Me.ToolStripStatusLabel2, Me.ToolStripStatusLabel3, Me.ToolStripStatusLabel4, Me.ToolStripStatusLabel5, Me.ToolStripStatusLabel6, Me.TSProgressBar})
        Me.sbrСостояния.Location = New System.Drawing.Point(0, 0)
        Me.sbrСостояния.Name = "sbrСостояния"
        Me.sbrСостояния.Size = New System.Drawing.Size(760, 22)
        Me.sbrСостояния.TabIndex = 6
        Me.sbrСостояния.Text = "StatusStrip"
        '
        'ToolStripStatusLabel1
        '
        Me.ToolStripStatusLabel1.BorderSides = CType((((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left Or System.Windows.Forms.ToolStripStatusLabelBorderSides.Top) _
            Or System.Windows.Forms.ToolStripStatusLabelBorderSides.Right) _
            Or System.Windows.Forms.ToolStripStatusLabelBorderSides.Bottom), System.Windows.Forms.ToolStripStatusLabelBorderSides)
        Me.ToolStripStatusLabel1.BorderStyle = System.Windows.Forms.Border3DStyle.Sunken
        Me.ToolStripStatusLabel1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text
        Me.ToolStripStatusLabel1.Name = "ToolStripStatusLabel1"
        Me.ToolStripStatusLabel1.Size = New System.Drawing.Size(90, 17)
        Me.ToolStripStatusLabel1.Spring = True
        '
        'ToolStripStatusLabel2
        '
        Me.ToolStripStatusLabel2.BorderSides = CType((((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left Or System.Windows.Forms.ToolStripStatusLabelBorderSides.Top) _
            Or System.Windows.Forms.ToolStripStatusLabelBorderSides.Right) _
            Or System.Windows.Forms.ToolStripStatusLabelBorderSides.Bottom), System.Windows.Forms.ToolStripStatusLabelBorderSides)
        Me.ToolStripStatusLabel2.BorderStyle = System.Windows.Forms.Border3DStyle.Sunken
        Me.ToolStripStatusLabel2.Name = "ToolStripStatusLabel2"
        Me.ToolStripStatusLabel2.Size = New System.Drawing.Size(90, 17)
        Me.ToolStripStatusLabel2.Spring = True
        '
        'ToolStripStatusLabel3
        '
        Me.ToolStripStatusLabel3.BorderSides = CType((((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left Or System.Windows.Forms.ToolStripStatusLabelBorderSides.Top) _
            Or System.Windows.Forms.ToolStripStatusLabelBorderSides.Right) _
            Or System.Windows.Forms.ToolStripStatusLabelBorderSides.Bottom), System.Windows.Forms.ToolStripStatusLabelBorderSides)
        Me.ToolStripStatusLabel3.BorderStyle = System.Windows.Forms.Border3DStyle.Sunken
        Me.ToolStripStatusLabel3.Name = "ToolStripStatusLabel3"
        Me.ToolStripStatusLabel3.Size = New System.Drawing.Size(90, 17)
        Me.ToolStripStatusLabel3.Spring = True
        '
        'ToolStripStatusLabel4
        '
        Me.ToolStripStatusLabel4.BorderSides = CType((((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left Or System.Windows.Forms.ToolStripStatusLabelBorderSides.Top) _
            Or System.Windows.Forms.ToolStripStatusLabelBorderSides.Right) _
            Or System.Windows.Forms.ToolStripStatusLabelBorderSides.Bottom), System.Windows.Forms.ToolStripStatusLabelBorderSides)
        Me.ToolStripStatusLabel4.BorderStyle = System.Windows.Forms.Border3DStyle.Sunken
        Me.ToolStripStatusLabel4.Name = "ToolStripStatusLabel4"
        Me.ToolStripStatusLabel4.Size = New System.Drawing.Size(90, 17)
        Me.ToolStripStatusLabel4.Spring = True
        '
        'ToolStripStatusLabel5
        '
        Me.ToolStripStatusLabel5.BorderSides = CType((((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left Or System.Windows.Forms.ToolStripStatusLabelBorderSides.Top) _
            Or System.Windows.Forms.ToolStripStatusLabelBorderSides.Right) _
            Or System.Windows.Forms.ToolStripStatusLabelBorderSides.Bottom), System.Windows.Forms.ToolStripStatusLabelBorderSides)
        Me.ToolStripStatusLabel5.BorderStyle = System.Windows.Forms.Border3DStyle.Sunken
        Me.ToolStripStatusLabel5.Name = "ToolStripStatusLabel5"
        Me.ToolStripStatusLabel5.Size = New System.Drawing.Size(90, 17)
        Me.ToolStripStatusLabel5.Spring = True
        '
        'ToolStripStatusLabel6
        '
        Me.ToolStripStatusLabel6.BorderSides = CType((((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left Or System.Windows.Forms.ToolStripStatusLabelBorderSides.Top) _
            Or System.Windows.Forms.ToolStripStatusLabelBorderSides.Right) _
            Or System.Windows.Forms.ToolStripStatusLabelBorderSides.Bottom), System.Windows.Forms.ToolStripStatusLabelBorderSides)
        Me.ToolStripStatusLabel6.BorderStyle = System.Windows.Forms.Border3DStyle.Sunken
        Me.ToolStripStatusLabel6.Name = "ToolStripStatusLabel6"
        Me.ToolStripStatusLabel6.Size = New System.Drawing.Size(90, 17)
        Me.ToolStripStatusLabel6.Spring = True
        '
        'TSProgressBar
        '
        Me.TSProgressBar.Name = "TSProgressBar"
        Me.TSProgressBar.Size = New System.Drawing.Size(200, 16)
        '
        'ToolStrip
        '
        Me.ToolStrip.Anchor = System.Windows.Forms.AnchorStyles.Right
        Me.ToolStrip.Dock = System.Windows.Forms.DockStyle.None
        Me.ToolStrip.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.LoadToolStripButton, Me.DeleteToolStripButton, Me.ToolStripSeparator7, Me.FoldersToolStripButton, Me.ToolStripSeparator8, Me.ListViewToolStripButton})
        Me.ToolStrip.Location = New System.Drawing.Point(3, 24)
        Me.ToolStrip.Name = "ToolStrip"
        Me.ToolStrip.Size = New System.Drawing.Size(250, 25)
        Me.ToolStrip.TabIndex = 0
        Me.ToolStrip.Text = "ToolStrip1"
        '
        'LoadToolStripButton
        '
        Me.LoadToolStripButton.Image = CType(resources.GetObject("LoadToolStripButton.Image"), System.Drawing.Image)
        Me.LoadToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.LoadToolStripButton.Name = "LoadToolStripButton"
        Me.LoadToolStripButton.Size = New System.Drawing.Size(111, 22)
        Me.LoadToolStripButton.Text = "Загрузить базы"
        '
        'DeleteToolStripButton
        '
        Me.DeleteToolStripButton.AutoSize = False
        Me.DeleteToolStripButton.Image = CType(resources.GetObject("DeleteToolStripButton.Image"), System.Drawing.Image)
        Me.DeleteToolStripButton.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None
        Me.DeleteToolStripButton.Name = "DeleteToolStripButton"
        Me.DeleteToolStripButton.Size = New System.Drawing.Size(24, 22)
        Me.DeleteToolStripButton.Tag = "Delete"
        Me.DeleteToolStripButton.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.DeleteToolStripButton.ToolTipText = "Удалить выделенное испытание"
        '
        'ToolStripSeparator7
        '
        Me.ToolStripSeparator7.Name = "ToolStripSeparator7"
        Me.ToolStripSeparator7.Size = New System.Drawing.Size(6, 25)
        '
        'FoldersToolStripButton
        '
        Me.FoldersToolStripButton.Checked = True
        Me.FoldersToolStripButton.CheckState = System.Windows.Forms.CheckState.Checked
        Me.FoldersToolStripButton.Image = CType(resources.GetObject("FoldersToolStripButton.Image"), System.Drawing.Image)
        Me.FoldersToolStripButton.ImageTransparentColor = System.Drawing.Color.Black
        Me.FoldersToolStripButton.Name = "FoldersToolStripButton"
        Me.FoldersToolStripButton.Size = New System.Drawing.Size(62, 22)
        Me.FoldersToolStripButton.Text = "Папки"
        Me.FoldersToolStripButton.ToolTipText = "Переключить просмотр папок"
        '
        'ToolStripSeparator8
        '
        Me.ToolStripSeparator8.Name = "ToolStripSeparator8"
        Me.ToolStripSeparator8.Size = New System.Drawing.Size(6, 25)
        '
        'ListViewToolStripButton
        '
        Me.ListViewToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.ListViewToolStripButton.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ListToolStripMenuItem, Me.DetailsToolStripMenuItem, Me.LargeIconsToolStripMenuItem, Me.SmallIconsToolStripMenuItem, Me.TileToolStripMenuItem})
        Me.ListViewToolStripButton.Image = CType(resources.GetObject("ListViewToolStripButton.Image"), System.Drawing.Image)
        Me.ListViewToolStripButton.ImageTransparentColor = System.Drawing.Color.Black
        Me.ListViewToolStripButton.Name = "ListViewToolStripButton"
        Me.ListViewToolStripButton.Size = New System.Drawing.Size(29, 22)
        Me.ListViewToolStripButton.Text = "Вид"
        '
        'ListToolStripMenuItem
        '
        Me.ListToolStripMenuItem.Name = "ListToolStripMenuItem"
        Me.ListToolStripMenuItem.Size = New System.Drawing.Size(132, 22)
        Me.ListToolStripMenuItem.Text = "Лист"
        '
        'DetailsToolStripMenuItem
        '
        Me.DetailsToolStripMenuItem.Checked = True
        Me.DetailsToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked
        Me.DetailsToolStripMenuItem.Name = "DetailsToolStripMenuItem"
        Me.DetailsToolStripMenuItem.Size = New System.Drawing.Size(132, 22)
        Me.DetailsToolStripMenuItem.Text = "Детали"
        '
        'LargeIconsToolStripMenuItem
        '
        Me.LargeIconsToolStripMenuItem.Name = "LargeIconsToolStripMenuItem"
        Me.LargeIconsToolStripMenuItem.Size = New System.Drawing.Size(132, 22)
        Me.LargeIconsToolStripMenuItem.Text = "Крупно"
        '
        'SmallIconsToolStripMenuItem
        '
        Me.SmallIconsToolStripMenuItem.Name = "SmallIconsToolStripMenuItem"
        Me.SmallIconsToolStripMenuItem.Size = New System.Drawing.Size(132, 22)
        Me.SmallIconsToolStripMenuItem.Text = "Мелко"
        '
        'TileToolStripMenuItem
        '
        Me.TileToolStripMenuItem.Name = "TileToolStripMenuItem"
        Me.TileToolStripMenuItem.Size = New System.Drawing.Size(132, 22)
        Me.TileToolStripMenuItem.Text = "Заголовок"
        '
        'MenuStrip
        '
        Me.MenuStrip.Dock = System.Windows.Forms.DockStyle.None
        Me.MenuStrip.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.FileToolStripMenuItem, Me.ViewToolStripMenuItem})
        Me.MenuStrip.Location = New System.Drawing.Point(0, 0)
        Me.MenuStrip.Name = "MenuStrip"
        Me.MenuStrip.Size = New System.Drawing.Size(760, 24)
        Me.MenuStrip.TabIndex = 0
        Me.MenuStrip.Text = "MenuStrip1"
        '
        'FileToolStripMenuItem
        '
        Me.FileToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.PackInputToolStripMenuItem, Me.SaveAsInputToolStripMenuItem, Me.CleanInputToolStripMenuItem, Me.ToolStripSeparator9, Me.PackOutputToolStripMenuItem, Me.SaveAsOutputToolStripMenuItem, Me.CleanOutputToolStripMenuItem})
        Me.FileToolStripMenuItem.Name = "FileToolStripMenuItem"
        Me.FileToolStripMenuItem.Size = New System.Drawing.Size(48, 20)
        Me.FileToolStripMenuItem.Text = "&Файл"
        '
        'PackInputToolStripMenuItem
        '
        Me.PackInputToolStripMenuItem.Image = CType(resources.GetObject("PackInputToolStripMenuItem.Image"), System.Drawing.Image)
        Me.PackInputToolStripMenuItem.Name = "PackInputToolStripMenuItem"
        Me.PackInputToolStripMenuItem.Size = New System.Drawing.Size(245, 22)
        Me.PackInputToolStripMenuItem.Text = "Сжать &текущую базу"
        '
        'SaveAsInputToolStripMenuItem
        '
        Me.SaveAsInputToolStripMenuItem.Image = CType(resources.GetObject("SaveAsInputToolStripMenuItem.Image"), System.Drawing.Image)
        Me.SaveAsInputToolStripMenuItem.ImageTransparentColor = System.Drawing.Color.Black
        Me.SaveAsInputToolStripMenuItem.Name = "SaveAsInputToolStripMenuItem"
        Me.SaveAsInputToolStripMenuItem.Size = New System.Drawing.Size(245, 22)
        Me.SaveAsInputToolStripMenuItem.Text = "Сохранить те&кущую базу как ..."
        '
        'CleanInputToolStripMenuItem
        '
        Me.CleanInputToolStripMenuItem.Image = CType(resources.GetObject("CleanInputToolStripMenuItem.Image"), System.Drawing.Image)
        Me.CleanInputToolStripMenuItem.ImageTransparentColor = System.Drawing.Color.Black
        Me.CleanInputToolStripMenuItem.Name = "CleanInputToolStripMenuItem"
        Me.CleanInputToolStripMenuItem.Size = New System.Drawing.Size(245, 22)
        Me.CleanInputToolStripMenuItem.Text = "Очистить т&екущую базу"
        '
        'ToolStripSeparator9
        '
        Me.ToolStripSeparator9.Name = "ToolStripSeparator9"
        Me.ToolStripSeparator9.Size = New System.Drawing.Size(242, 6)
        '
        'PackOutputToolStripMenuItem
        '
        Me.PackOutputToolStripMenuItem.Image = CType(resources.GetObject("PackOutputToolStripMenuItem.Image"), System.Drawing.Image)
        Me.PackOutputToolStripMenuItem.Name = "PackOutputToolStripMenuItem"
        Me.PackOutputToolStripMenuItem.Size = New System.Drawing.Size(245, 22)
        Me.PackOutputToolStripMenuItem.Text = "Сжать базу &архива"
        '
        'SaveAsOutputToolStripMenuItem
        '
        Me.SaveAsOutputToolStripMenuItem.Image = CType(resources.GetObject("SaveAsOutputToolStripMenuItem.Image"), System.Drawing.Image)
        Me.SaveAsOutputToolStripMenuItem.ImageTransparentColor = System.Drawing.Color.Black
        Me.SaveAsOutputToolStripMenuItem.Name = "SaveAsOutputToolStripMenuItem"
        Me.SaveAsOutputToolStripMenuItem.Size = New System.Drawing.Size(245, 22)
        Me.SaveAsOutputToolStripMenuItem.Text = "Сохранить базу ар&хива как ..."
        '
        'CleanOutputToolStripMenuItem
        '
        Me.CleanOutputToolStripMenuItem.Image = CType(resources.GetObject("CleanOutputToolStripMenuItem.Image"), System.Drawing.Image)
        Me.CleanOutputToolStripMenuItem.ImageTransparentColor = System.Drawing.Color.Black
        Me.CleanOutputToolStripMenuItem.Name = "CleanOutputToolStripMenuItem"
        Me.CleanOutputToolStripMenuItem.Size = New System.Drawing.Size(245, 22)
        Me.CleanOutputToolStripMenuItem.Text = "Очистить базу а&рхива"
        '
        'ViewToolStripMenuItem
        '
        Me.ViewToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ToolBarToolStripMenuItem, Me.StatusBarToolStripMenuItem, Me.FoldersToolStripMenuItem})
        Me.ViewToolStripMenuItem.Name = "ViewToolStripMenuItem"
        Me.ViewToolStripMenuItem.Size = New System.Drawing.Size(39, 20)
        Me.ViewToolStripMenuItem.Text = "&Вид"
        '
        'ToolBarToolStripMenuItem
        '
        Me.ToolBarToolStripMenuItem.Checked = True
        Me.ToolBarToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked
        Me.ToolBarToolStripMenuItem.Name = "ToolBarToolStripMenuItem"
        Me.ToolBarToolStripMenuItem.Size = New System.Drawing.Size(196, 22)
        Me.ToolBarToolStripMenuItem.Text = "&Панель инструментов"
        '
        'StatusBarToolStripMenuItem
        '
        Me.StatusBarToolStripMenuItem.Checked = True
        Me.StatusBarToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked
        Me.StatusBarToolStripMenuItem.Name = "StatusBarToolStripMenuItem"
        Me.StatusBarToolStripMenuItem.Size = New System.Drawing.Size(196, 22)
        Me.StatusBarToolStripMenuItem.Text = "&Статусная панель"
        '
        'FoldersToolStripMenuItem
        '
        Me.FoldersToolStripMenuItem.Checked = True
        Me.FoldersToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked
        Me.FoldersToolStripMenuItem.Name = "FoldersToolStripMenuItem"
        Me.FoldersToolStripMenuItem.Size = New System.Drawing.Size(196, 22)
        Me.FoldersToolStripMenuItem.Text = "&Папки"
        '
        'SplitContainerAll
        '
        Me.SplitContainerAll.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.SplitContainerAll.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainerAll.Enabled = False
        Me.SplitContainerAll.Location = New System.Drawing.Point(0, 0)
        Me.SplitContainerAll.Name = "SplitContainerAll"
        '
        'SplitContainerAll.Panel1
        '
        Me.SplitContainerAll.Panel1.Controls.Add(Me.SplitContainerInput)
        '
        'SplitContainerAll.Panel2
        '
        Me.SplitContainerAll.Panel2.Controls.Add(Me.SplitContainerOutput)
        Me.ToolTip.SetToolTip(Me.SplitContainerAll.Panel2, "Просмотр")
        Me.SplitContainerAll.Size = New System.Drawing.Size(760, 514)
        Me.SplitContainerAll.SplitterDistance = 380
        Me.SplitContainerAll.TabIndex = 1
        '
        'SplitContainerInput
        '
        Me.SplitContainerInput.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.SplitContainerInput.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainerInput.Location = New System.Drawing.Point(0, 0)
        Me.SplitContainerInput.Name = "SplitContainerInput"
        Me.SplitContainerInput.Orientation = System.Windows.Forms.Orientation.Horizontal
        '
        'SplitContainerInput.Panel1
        '
        Me.SplitContainerInput.Panel1.Controls.Add(Me.SplitContainerTreeInput)
        Me.SplitContainerInput.Panel1.Controls.Add(Me.TextBoxInput)
        '
        'SplitContainerInput.Panel2
        '
        Me.SplitContainerInput.Panel2.Controls.Add(Me.PanelListInput)
        Me.SplitContainerInput.Panel2.Controls.Add(Me.PanelPatchInput)
        Me.SplitContainerInput.Size = New System.Drawing.Size(380, 514)
        Me.SplitContainerInput.SplitterDistance = 256
        Me.SplitContainerInput.TabIndex = 0
        '
        'SplitContainerTreeInput
        '
        Me.SplitContainerTreeInput.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.SplitContainerTreeInput.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainerTreeInput.Location = New System.Drawing.Point(0, 13)
        Me.SplitContainerTreeInput.Name = "SplitContainerTreeInput"
        '
        'SplitContainerTreeInput.Panel1
        '
        Me.SplitContainerTreeInput.Panel1.Controls.Add(Me.TreeViewInput)
        '
        'SplitContainerTreeInput.Panel2
        '
        Me.SplitContainerTreeInput.Panel2.Controls.Add(Me.ListViewInput)
        Me.SplitContainerTreeInput.Size = New System.Drawing.Size(380, 243)
        Me.SplitContainerTreeInput.SplitterDistance = 188
        Me.SplitContainerTreeInput.TabIndex = 0
        Me.SplitContainerTreeInput.Text = "SplitContainer1"
        '
        'TreeViewInput
        '
        Me.TreeViewInput.ContextMenuStrip = Me.ContextMenuStripTreeDelete
        Me.TreeViewInput.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TreeViewInput.ImageIndex = 0
        Me.TreeViewInput.ImageList = Me.imlSmallIcons
        Me.TreeViewInput.Location = New System.Drawing.Point(0, 0)
        Me.TreeViewInput.Name = "TreeViewInput"
        Me.TreeViewInput.SelectedImageIndex = 0
        Me.TreeViewInput.Size = New System.Drawing.Size(184, 239)
        Me.TreeViewInput.TabIndex = 0
        '
        'ContextMenuStripTreeDelete
        '
        Me.ContextMenuStripTreeDelete.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.TSMenuItemDelete})
        Me.ContextMenuStripTreeDelete.Name = "ContextMenuStripTreeInput"
        Me.ContextMenuStripTreeDelete.Size = New System.Drawing.Size(119, 26)
        '
        'TSMenuItemDelete
        '
        Me.TSMenuItemDelete.Name = "TSMenuItemDelete"
        Me.TSMenuItemDelete.Size = New System.Drawing.Size(118, 22)
        Me.TSMenuItemDelete.Text = "Удалить"
        '
        'imlSmallIcons
        '
        Me.imlSmallIcons.ImageStream = CType(resources.GetObject("imlSmallIcons.ImageStream"), System.Windows.Forms.ImageListStreamer)
        Me.imlSmallIcons.TransparentColor = System.Drawing.Color.Transparent
        Me.imlSmallIcons.Images.SetKeyName(0, "open")
        Me.imlSmallIcons.Images.SetKeyName(1, "closed")
        Me.imlSmallIcons.Images.SetKeyName(2, "Изделие")
        Me.imlSmallIcons.Images.SetKeyName(3, "Поле")
        Me.imlSmallIcons.Images.SetKeyName(4, "Горелка")
        Me.imlSmallIcons.Images.SetKeyName(5, "Точка")
        Me.imlSmallIcons.Images.SetKeyName(6, "nfs_unmount.png")
        '
        'ListViewInput
        '
        Me.ListViewInput.Dock = System.Windows.Forms.DockStyle.Fill
        Me.ListViewInput.LargeImageList = Me.ListViewLargeImageList
        Me.ListViewInput.Location = New System.Drawing.Point(0, 0)
        Me.ListViewInput.Name = "ListViewInput"
        Me.ListViewInput.Size = New System.Drawing.Size(184, 239)
        Me.ListViewInput.SmallImageList = Me.imlSmallIcons
        Me.ListViewInput.TabIndex = 0
        Me.ListViewInput.UseCompatibleStateImageBehavior = False
        '
        'ListViewLargeImageList
        '
        Me.ListViewLargeImageList.ImageStream = CType(resources.GetObject("ListViewLargeImageList.ImageStream"), System.Windows.Forms.ImageListStreamer)
        Me.ListViewLargeImageList.TransparentColor = System.Drawing.Color.Transparent
        Me.ListViewLargeImageList.Images.SetKeyName(0, "open")
        Me.ListViewLargeImageList.Images.SetKeyName(1, "closed")
        Me.ListViewLargeImageList.Images.SetKeyName(2, "Изделие")
        Me.ListViewLargeImageList.Images.SetKeyName(3, "Поле")
        Me.ListViewLargeImageList.Images.SetKeyName(4, "Горелка")
        Me.ListViewLargeImageList.Images.SetKeyName(5, "Точка")
        '
        'TextBoxInput
        '
        Me.TextBoxInput.BackColor = System.Drawing.Color.LightSteelBlue
        Me.TextBoxInput.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TextBoxInput.Dock = System.Windows.Forms.DockStyle.Top
        Me.TextBoxInput.Location = New System.Drawing.Point(0, 0)
        Me.TextBoxInput.Name = "TextBoxInput"
        Me.TextBoxInput.ReadOnly = True
        Me.TextBoxInput.Size = New System.Drawing.Size(380, 13)
        Me.TextBoxInput.TabIndex = 1
        Me.TextBoxInput.Text = "Рабочая база"
        Me.TextBoxInput.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'PanelListInput
        '
        Me.PanelListInput.Controls.Add(Me.ListBoxInput)
        Me.PanelListInput.Controls.Add(Me.PanelButtonsInput)
        Me.PanelListInput.Dock = System.Windows.Forms.DockStyle.Fill
        Me.PanelListInput.Location = New System.Drawing.Point(0, 0)
        Me.PanelListInput.Name = "PanelListInput"
        Me.PanelListInput.Size = New System.Drawing.Size(376, 194)
        Me.PanelListInput.TabIndex = 1
        '
        'ListBoxInput
        '
        Me.ListBoxInput.Dock = System.Windows.Forms.DockStyle.Fill
        Me.ListBoxInput.FormattingEnabled = True
        Me.ListBoxInput.Location = New System.Drawing.Point(0, 0)
        Me.ListBoxInput.Name = "ListBoxInput"
        Me.ListBoxInput.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended
        Me.ListBoxInput.Size = New System.Drawing.Size(299, 194)
        Me.ListBoxInput.TabIndex = 1
        Me.ToolTip.SetToolTip(Me.ListBoxInput, "Выделить запись для перемещения")
        '
        'PanelButtonsInput
        '
        Me.PanelButtonsInput.Controls.Add(Me.ButtonMoveInputToOutput)
        Me.PanelButtonsInput.Controls.Add(Me.ButtonDeleteInput)
        Me.PanelButtonsInput.Dock = System.Windows.Forms.DockStyle.Right
        Me.PanelButtonsInput.Location = New System.Drawing.Point(299, 0)
        Me.PanelButtonsInput.Name = "PanelButtonsInput"
        Me.PanelButtonsInput.Size = New System.Drawing.Size(77, 194)
        Me.PanelButtonsInput.TabIndex = 0
        '
        'ButtonMoveInputToOutput
        '
        Me.ButtonMoveInputToOutput.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ButtonMoveInputToOutput.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(204, Byte))
        Me.ButtonMoveInputToOutput.ForeColor = System.Drawing.Color.Blue
        Me.ButtonMoveInputToOutput.Image = CType(resources.GetObject("ButtonMoveInputToOutput.Image"), System.Drawing.Image)
        Me.ButtonMoveInputToOutput.Location = New System.Drawing.Point(8, 122)
        Me.ButtonMoveInputToOutput.Name = "ButtonMoveInputToOutput"
        Me.ButtonMoveInputToOutput.Size = New System.Drawing.Size(66, 66)
        Me.ButtonMoveInputToOutput.TabIndex = 1
        Me.ButtonMoveInputToOutput.Text = "Перенос"
        Me.ButtonMoveInputToOutput.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip.SetToolTip(Me.ButtonMoveInputToOutput, "Переместить из источника в приёмник")
        Me.ButtonMoveInputToOutput.UseVisualStyleBackColor = True
        '
        'ButtonDeleteInput
        '
        Me.ButtonDeleteInput.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ButtonDeleteInput.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(204, Byte))
        Me.ButtonDeleteInput.ForeColor = System.Drawing.Color.Blue
        Me.ButtonDeleteInput.Image = CType(resources.GetObject("ButtonDeleteInput.Image"), System.Drawing.Image)
        Me.ButtonDeleteInput.Location = New System.Drawing.Point(8, 3)
        Me.ButtonDeleteInput.Name = "ButtonDeleteInput"
        Me.ButtonDeleteInput.Size = New System.Drawing.Size(66, 66)
        Me.ButtonDeleteInput.TabIndex = 0
        Me.ButtonDeleteInput.Text = "Удалить"
        Me.ButtonDeleteInput.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip.SetToolTip(Me.ButtonDeleteInput, "Удалить выделенную запись")
        Me.ButtonDeleteInput.UseVisualStyleBackColor = True
        '
        'PanelPatchInput
        '
        Me.PanelPatchInput.Controls.Add(Me.ButtonReviewSource)
        Me.PanelPatchInput.Controls.Add(Me.TextPathSource)
        Me.PanelPatchInput.Controls.Add(Me.LabelPathInput)
        Me.PanelPatchInput.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.PanelPatchInput.Location = New System.Drawing.Point(0, 194)
        Me.PanelPatchInput.Name = "PanelPatchInput"
        Me.PanelPatchInput.Size = New System.Drawing.Size(376, 56)
        Me.PanelPatchInput.TabIndex = 0
        '
        'ButtonReviewSource
        '
        Me.ButtonReviewSource.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ButtonReviewSource.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(204, Byte))
        Me.ButtonReviewSource.ForeColor = System.Drawing.Color.Blue
        Me.ButtonReviewSource.Image = CType(resources.GetObject("ButtonReviewSource.Image"), System.Drawing.Image)
        Me.ButtonReviewSource.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.ButtonReviewSource.Location = New System.Drawing.Point(331, 8)
        Me.ButtonReviewSource.Name = "ButtonReviewSource"
        Me.ButtonReviewSource.Size = New System.Drawing.Size(42, 42)
        Me.ButtonReviewSource.TabIndex = 11
        Me.ButtonReviewSource.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.ToolTip.SetToolTip(Me.ButtonReviewSource, "Определить базу Источника")
        Me.ButtonReviewSource.UseVisualStyleBackColor = True
        '
        'TextPathSource
        '
        Me.TextPathSource.AcceptsReturn = True
        Me.TextPathSource.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TextPathSource.BackColor = System.Drawing.SystemColors.Window
        Me.TextPathSource.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.TextPathSource.ForeColor = System.Drawing.SystemColors.WindowText
        Me.TextPathSource.Location = New System.Drawing.Point(8, 21)
        Me.TextPathSource.MaxLength = 0
        Me.TextPathSource.Name = "TextPathSource"
        Me.TextPathSource.ReadOnly = True
        Me.TextPathSource.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.TextPathSource.Size = New System.Drawing.Size(317, 20)
        Me.TextPathSource.TabIndex = 8
        Me.ToolTip.SetToolTip(Me.TextPathSource, "Путь Источника")
        '
        'LabelPathInput
        '
        Me.LabelPathInput.BackColor = System.Drawing.SystemColors.Control
        Me.LabelPathInput.Cursor = System.Windows.Forms.Cursors.Default
        Me.LabelPathInput.ForeColor = System.Drawing.SystemColors.ControlText
        Me.LabelPathInput.Location = New System.Drawing.Point(8, 1)
        Me.LabelPathInput.Name = "LabelPathInput"
        Me.LabelPathInput.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.LabelPathInput.Size = New System.Drawing.Size(173, 17)
        Me.LabelPathInput.TabIndex = 9
        Me.LabelPathInput.Text = "Путь к рабочей базе данных"
        '
        'SplitContainerOutput
        '
        Me.SplitContainerOutput.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.SplitContainerOutput.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainerOutput.Location = New System.Drawing.Point(0, 0)
        Me.SplitContainerOutput.Name = "SplitContainerOutput"
        Me.SplitContainerOutput.Orientation = System.Windows.Forms.Orientation.Horizontal
        '
        'SplitContainerOutput.Panel1
        '
        Me.SplitContainerOutput.Panel1.Controls.Add(Me.SplitContainerTreeOutput)
        Me.SplitContainerOutput.Panel1.Controls.Add(Me.TextBoxOutput)
        '
        'SplitContainerOutput.Panel2
        '
        Me.SplitContainerOutput.Panel2.Controls.Add(Me.PanelListOutput)
        Me.SplitContainerOutput.Panel2.Controls.Add(Me.PanelPatchOutput)
        Me.SplitContainerOutput.Size = New System.Drawing.Size(376, 514)
        Me.SplitContainerOutput.SplitterDistance = 256
        Me.SplitContainerOutput.TabIndex = 6
        '
        'SplitContainerTreeOutput
        '
        Me.SplitContainerTreeOutput.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.SplitContainerTreeOutput.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainerTreeOutput.Location = New System.Drawing.Point(0, 13)
        Me.SplitContainerTreeOutput.Name = "SplitContainerTreeOutput"
        '
        'SplitContainerTreeOutput.Panel1
        '
        Me.SplitContainerTreeOutput.Panel1.Controls.Add(Me.TreeViewOutput)
        '
        'SplitContainerTreeOutput.Panel2
        '
        Me.SplitContainerTreeOutput.Panel2.Controls.Add(Me.ListViewOutput)
        Me.SplitContainerTreeOutput.Size = New System.Drawing.Size(376, 243)
        Me.SplitContainerTreeOutput.SplitterDistance = 186
        Me.SplitContainerTreeOutput.TabIndex = 3
        Me.SplitContainerTreeOutput.Text = "SplitContainer1"
        '
        'TreeViewOutput
        '
        Me.TreeViewOutput.ContextMenuStrip = Me.ContextMenuStripTreeDelete
        Me.TreeViewOutput.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TreeViewOutput.ImageIndex = 0
        Me.TreeViewOutput.ImageList = Me.imlSmallIcons
        Me.TreeViewOutput.Location = New System.Drawing.Point(0, 0)
        Me.TreeViewOutput.Name = "TreeViewOutput"
        Me.TreeViewOutput.SelectedImageIndex = 0
        Me.TreeViewOutput.Size = New System.Drawing.Size(182, 239)
        Me.TreeViewOutput.TabIndex = 0
        '
        'ListViewOutput
        '
        Me.ListViewOutput.Dock = System.Windows.Forms.DockStyle.Fill
        Me.ListViewOutput.LargeImageList = Me.ListViewLargeImageList
        Me.ListViewOutput.Location = New System.Drawing.Point(0, 0)
        Me.ListViewOutput.Name = "ListViewOutput"
        Me.ListViewOutput.Size = New System.Drawing.Size(182, 239)
        Me.ListViewOutput.SmallImageList = Me.imlSmallIcons
        Me.ListViewOutput.TabIndex = 0
        Me.ListViewOutput.UseCompatibleStateImageBehavior = False
        '
        'TextBoxOutput
        '
        Me.TextBoxOutput.BackColor = System.Drawing.Color.LightSteelBlue
        Me.TextBoxOutput.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TextBoxOutput.Dock = System.Windows.Forms.DockStyle.Top
        Me.TextBoxOutput.Location = New System.Drawing.Point(0, 0)
        Me.TextBoxOutput.Name = "TextBoxOutput"
        Me.TextBoxOutput.ReadOnly = True
        Me.TextBoxOutput.Size = New System.Drawing.Size(376, 13)
        Me.TextBoxOutput.TabIndex = 5
        Me.TextBoxOutput.Text = "База архива"
        Me.TextBoxOutput.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'PanelListOutput
        '
        Me.PanelListOutput.Controls.Add(Me.ListBoxOutput)
        Me.PanelListOutput.Controls.Add(Me.PanelButtonsOutput)
        Me.PanelListOutput.Dock = System.Windows.Forms.DockStyle.Fill
        Me.PanelListOutput.Location = New System.Drawing.Point(0, 0)
        Me.PanelListOutput.Name = "PanelListOutput"
        Me.PanelListOutput.Size = New System.Drawing.Size(372, 194)
        Me.PanelListOutput.TabIndex = 4
        '
        'ListBoxOutput
        '
        Me.ListBoxOutput.Dock = System.Windows.Forms.DockStyle.Fill
        Me.ListBoxOutput.FormattingEnabled = True
        Me.ListBoxOutput.Location = New System.Drawing.Point(77, 0)
        Me.ListBoxOutput.Name = "ListBoxOutput"
        Me.ListBoxOutput.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended
        Me.ListBoxOutput.Size = New System.Drawing.Size(295, 194)
        Me.ListBoxOutput.TabIndex = 1
        Me.ToolTip.SetToolTip(Me.ListBoxOutput, "Выделить запись для перемещения")
        '
        'PanelButtonsOutput
        '
        Me.PanelButtonsOutput.Controls.Add(Me.ButtonMoveOutputToInput)
        Me.PanelButtonsOutput.Controls.Add(Me.ButtonDeleteOutput)
        Me.PanelButtonsOutput.Dock = System.Windows.Forms.DockStyle.Left
        Me.PanelButtonsOutput.Location = New System.Drawing.Point(0, 0)
        Me.PanelButtonsOutput.Name = "PanelButtonsOutput"
        Me.PanelButtonsOutput.Size = New System.Drawing.Size(77, 194)
        Me.PanelButtonsOutput.TabIndex = 0
        '
        'ButtonMoveOutputToInput
        '
        Me.ButtonMoveOutputToInput.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.ButtonMoveOutputToInput.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(204, Byte))
        Me.ButtonMoveOutputToInput.ForeColor = System.Drawing.Color.Blue
        Me.ButtonMoveOutputToInput.Image = CType(resources.GetObject("ButtonMoveOutputToInput.Image"), System.Drawing.Image)
        Me.ButtonMoveOutputToInput.Location = New System.Drawing.Point(3, 122)
        Me.ButtonMoveOutputToInput.Name = "ButtonMoveOutputToInput"
        Me.ButtonMoveOutputToInput.Size = New System.Drawing.Size(66, 66)
        Me.ButtonMoveOutputToInput.TabIndex = 2
        Me.ButtonMoveOutputToInput.Text = "Перенос"
        Me.ButtonMoveOutputToInput.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip.SetToolTip(Me.ButtonMoveOutputToInput, "Переместить из приёмника в источник")
        Me.ButtonMoveOutputToInput.UseVisualStyleBackColor = True
        '
        'ButtonDeleteOutput
        '
        Me.ButtonDeleteOutput.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(204, Byte))
        Me.ButtonDeleteOutput.ForeColor = System.Drawing.Color.Blue
        Me.ButtonDeleteOutput.Image = CType(resources.GetObject("ButtonDeleteOutput.Image"), System.Drawing.Image)
        Me.ButtonDeleteOutput.Location = New System.Drawing.Point(3, 3)
        Me.ButtonDeleteOutput.Name = "ButtonDeleteOutput"
        Me.ButtonDeleteOutput.Size = New System.Drawing.Size(66, 66)
        Me.ButtonDeleteOutput.TabIndex = 0
        Me.ButtonDeleteOutput.Text = "Удалить"
        Me.ButtonDeleteOutput.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip.SetToolTip(Me.ButtonDeleteOutput, "Удалить выделенную запись")
        Me.ButtonDeleteOutput.UseVisualStyleBackColor = True
        '
        'PanelPatchOutput
        '
        Me.PanelPatchOutput.Controls.Add(Me.ButtonReviewReceiver)
        Me.PanelPatchOutput.Controls.Add(Me.TextPathReceiver)
        Me.PanelPatchOutput.Controls.Add(Me.LabelPathOutput)
        Me.PanelPatchOutput.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.PanelPatchOutput.Location = New System.Drawing.Point(0, 194)
        Me.PanelPatchOutput.Name = "PanelPatchOutput"
        Me.PanelPatchOutput.Size = New System.Drawing.Size(372, 56)
        Me.PanelPatchOutput.TabIndex = 2
        '
        'ButtonReviewReceiver
        '
        Me.ButtonReviewReceiver.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ButtonReviewReceiver.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(204, Byte))
        Me.ButtonReviewReceiver.ForeColor = System.Drawing.Color.Blue
        Me.ButtonReviewReceiver.Image = CType(resources.GetObject("ButtonReviewReceiver.Image"), System.Drawing.Image)
        Me.ButtonReviewReceiver.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.ButtonReviewReceiver.Location = New System.Drawing.Point(327, 8)
        Me.ButtonReviewReceiver.Name = "ButtonReviewReceiver"
        Me.ButtonReviewReceiver.Size = New System.Drawing.Size(42, 42)
        Me.ButtonReviewReceiver.TabIndex = 11
        Me.ButtonReviewReceiver.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.ToolTip.SetToolTip(Me.ButtonReviewReceiver, "Определить базу Приёмника")
        Me.ButtonReviewReceiver.UseVisualStyleBackColor = True
        '
        'TextPathReceiver
        '
        Me.TextPathReceiver.AcceptsReturn = True
        Me.TextPathReceiver.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TextPathReceiver.BackColor = System.Drawing.SystemColors.Window
        Me.TextPathReceiver.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.TextPathReceiver.ForeColor = System.Drawing.SystemColors.WindowText
        Me.TextPathReceiver.Location = New System.Drawing.Point(8, 21)
        Me.TextPathReceiver.MaxLength = 0
        Me.TextPathReceiver.Name = "TextPathReceiver"
        Me.TextPathReceiver.ReadOnly = True
        Me.TextPathReceiver.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.TextPathReceiver.Size = New System.Drawing.Size(313, 20)
        Me.TextPathReceiver.TabIndex = 8
        Me.ToolTip.SetToolTip(Me.TextPathReceiver, "Путь Приемника")
        '
        'LabelPathOutput
        '
        Me.LabelPathOutput.BackColor = System.Drawing.SystemColors.Control
        Me.LabelPathOutput.Cursor = System.Windows.Forms.Cursors.Default
        Me.LabelPathOutput.ForeColor = System.Drawing.SystemColors.ControlText
        Me.LabelPathOutput.Location = New System.Drawing.Point(8, 1)
        Me.LabelPathOutput.Name = "LabelPathOutput"
        Me.LabelPathOutput.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.LabelPathOutput.Size = New System.Drawing.Size(173, 17)
        Me.LabelPathOutput.TabIndex = 9
        Me.LabelPathOutput.Text = "Путь к архивной базе данных"
        '
        'ToolStripContainer
        '
        '
        'ToolStripContainer.BottomToolStripPanel
        '
        Me.ToolStripContainer.BottomToolStripPanel.Controls.Add(Me.sbrСостояния)
        '
        'ToolStripContainer.ContentPanel
        '
        Me.ToolStripContainer.ContentPanel.Controls.Add(Me.SplitContainerAll)
        Me.ToolStripContainer.ContentPanel.Size = New System.Drawing.Size(760, 514)
        Me.ToolStripContainer.Dock = System.Windows.Forms.DockStyle.Fill
        Me.ToolStripContainer.Location = New System.Drawing.Point(0, 0)
        Me.ToolStripContainer.Name = "ToolStripContainer"
        Me.ToolStripContainer.Size = New System.Drawing.Size(760, 585)
        Me.ToolStripContainer.TabIndex = 7
        Me.ToolStripContainer.Text = "ToolStripContainer1"
        '
        'ToolStripContainer.TopToolStripPanel
        '
        Me.ToolStripContainer.TopToolStripPanel.Controls.Add(Me.MenuStrip)
        Me.ToolStripContainer.TopToolStripPanel.Controls.Add(Me.ToolStrip)
        '
        'imlToolbarIcons
        '
        Me.imlToolbarIcons.ImageStream = CType(resources.GetObject("imlToolbarIcons.ImageStream"), System.Windows.Forms.ImageListStreamer)
        Me.imlToolbarIcons.TransparentColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.imlToolbarIcons.Images.SetKeyName(0, "New")
        Me.imlToolbarIcons.Images.SetKeyName(1, "Open")
        Me.imlToolbarIcons.Images.SetKeyName(2, "Save")
        Me.imlToolbarIcons.Images.SetKeyName(3, "Cut")
        Me.imlToolbarIcons.Images.SetKeyName(4, "Copy")
        Me.imlToolbarIcons.Images.SetKeyName(5, "Paste")
        '
        'КамераInputDataSet
        '
        Me.КамераInputDataSet.DataSetName = "КамераDataSet"
        Me.КамераInputDataSet.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema
        '
        'КамераOutputDataSet
        '
        Me.КамераOutputDataSet.DataSetName = "КамераDataSet"
        Me.КамераOutputDataSet.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema
        '
        'ГорелкаOutputBindingSource
        '
        Me.ГорелкаOutputBindingSource.DataMember = "Горелка"
        Me.ГорелкаOutputBindingSource.DataSource = Me.КамераOutputDataSet
        '
        'ГорелкаInputBindingSource
        '
        Me.ГорелкаInputBindingSource.DataMember = "Горелка"
        Me.ГорелкаInputBindingSource.DataSource = Me.КамераInputDataSet
        '
        'ИзделиеInputBindingSource
        '
        Me.ИзделиеInputBindingSource.DataMember = "Изделие"
        Me.ИзделиеInputBindingSource.DataSource = Me.КамераInputDataSet
        '
        'ИзделиеOutputBindingSource
        '
        Me.ИзделиеOutputBindingSource.DataMember = "Изделие"
        Me.ИзделиеOutputBindingSource.DataSource = Me.КамераOutputDataSet
        '
        'ГребенкаАInputBindingSource
        '
        Me.ГребенкаАInputBindingSource.DataMember = "ГребенкаА"
        Me.ГребенкаАInputBindingSource.DataSource = Me.КамераInputDataSet
        '
        'ГребенкаАOutputBindingSource
        '
        Me.ГребенкаАOutputBindingSource.DataMember = "ГребенкаА"
        Me.ГребенкаАOutputBindingSource.DataSource = Me.КамераOutputDataSet
        '
        'ГребенкаБInputBindingSource
        '
        Me.ГребенкаБInputBindingSource.DataMember = "ГребенкаБ"
        Me.ГребенкаБInputBindingSource.DataSource = Me.КамераInputDataSet
        '
        'ГребенкаБOutputBindingSource
        '
        Me.ГребенкаБOutputBindingSource.DataMember = "ГребенкаБ"
        Me.ГребенкаБOutputBindingSource.DataSource = Me.КамераOutputDataSet
        '
        'КонтрольЭДСInputBindingSource
        '
        Me.КонтрольЭДСInputBindingSource.DataMember = "КонтрольЭДС"
        Me.КонтрольЭДСInputBindingSource.DataSource = Me.КамераInputDataSet
        '
        'КонтрольЭДСOutputBindingSource
        '
        Me.КонтрольЭДСOutputBindingSource.DataMember = "КонтрольЭДС"
        Me.КонтрольЭДСOutputBindingSource.DataSource = Me.КамераOutputDataSet
        '
        'ПолеInputBindingSource
        '
        Me.ПолеInputBindingSource.DataMember = "Поле"
        Me.ПолеInputBindingSource.DataSource = Me.КамераInputDataSet
        '
        'ПолеOutputBindingSource
        '
        Me.ПолеOutputBindingSource.DataMember = "Поле"
        Me.ПолеOutputBindingSource.DataSource = Me.КамераOutputDataSet
        '
        'ТочкаInputBindingSource
        '
        Me.ТочкаInputBindingSource.DataMember = "Точка"
        Me.ТочкаInputBindingSource.DataSource = Me.КамераInputDataSet
        '
        'ТочкаOutputBindingSource
        '
        Me.ТочкаOutputBindingSource.DataMember = "Точка"
        Me.ТочкаOutputBindingSource.DataSource = Me.КамераOutputDataSet
        '
        'ИзделиеInputTableAdapter
        '
        Me.ИзделиеInputTableAdapter.ClearBeforeFill = True
        '
        'ГорелкаInputTableAdapter
        '
        Me.ГорелкаInputTableAdapter.ClearBeforeFill = True
        '
        'ГребенкаАInputTableAdapter
        '
        Me.ГребенкаАInputTableAdapter.ClearBeforeFill = True
        '
        'ГребенкаБInputTableAdapter
        '
        Me.ГребенкаБInputTableAdapter.ClearBeforeFill = True
        '
        'КонтрольЭДСInputTableAdapter
        '
        Me.КонтрольЭДСInputTableAdapter.ClearBeforeFill = True
        '
        'ПолеInputTableAdapter
        '
        Me.ПолеInputTableAdapter.ClearBeforeFill = True
        '
        'ТочкаInputTableAdapter
        '
        Me.ТочкаInputTableAdapter.ClearBeforeFill = True
        '
        'ИзделиеOutputTableAdapter
        '
        Me.ИзделиеOutputTableAdapter.ClearBeforeFill = True
        '
        'ГорелкаOutputTableAdapter
        '
        Me.ГорелкаOutputTableAdapter.ClearBeforeFill = True
        '
        'ГребенкаАOutputTableAdapter
        '
        Me.ГребенкаАOutputTableAdapter.ClearBeforeFill = True
        '
        'ГребенкаБOutputTableAdapter
        '
        Me.ГребенкаБOutputTableAdapter.ClearBeforeFill = True
        '
        'КонтрольЭДСOutputTableAdapter
        '
        Me.КонтрольЭДСOutputTableAdapter.ClearBeforeFill = True
        '
        'ПолеOutputTableAdapter
        '
        Me.ПолеOutputTableAdapter.ClearBeforeFill = True
        '
        'ТочкаOutputTableAdapter
        '
        Me.ТочкаOutputTableAdapter.ClearBeforeFill = True
        '
        'DBExplorerForm
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(760, 585)
        Me.Controls.Add(Me.ToolStripContainer)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "DBExplorerForm"
        Me.Text = "Обслуживание базы данных"
        Me.ToolTip.SetToolTip(Me, "Обслуживание базы данных")
        Me.sbrСостояния.ResumeLayout(False)
        Me.sbrСостояния.PerformLayout()
        Me.ToolStrip.ResumeLayout(False)
        Me.ToolStrip.PerformLayout()
        Me.MenuStrip.ResumeLayout(False)
        Me.MenuStrip.PerformLayout()
        Me.SplitContainerAll.Panel1.ResumeLayout(False)
        Me.SplitContainerAll.Panel2.ResumeLayout(False)
        CType(Me.SplitContainerAll, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainerAll.ResumeLayout(False)
        Me.SplitContainerInput.Panel1.ResumeLayout(False)
        Me.SplitContainerInput.Panel1.PerformLayout()
        Me.SplitContainerInput.Panel2.ResumeLayout(False)
        CType(Me.SplitContainerInput, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainerInput.ResumeLayout(False)
        Me.SplitContainerTreeInput.Panel1.ResumeLayout(False)
        Me.SplitContainerTreeInput.Panel2.ResumeLayout(False)
        CType(Me.SplitContainerTreeInput, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainerTreeInput.ResumeLayout(False)
        Me.ContextMenuStripTreeDelete.ResumeLayout(False)
        Me.PanelListInput.ResumeLayout(False)
        Me.PanelButtonsInput.ResumeLayout(False)
        Me.PanelPatchInput.ResumeLayout(False)
        Me.PanelPatchInput.PerformLayout()
        Me.SplitContainerOutput.Panel1.ResumeLayout(False)
        Me.SplitContainerOutput.Panel1.PerformLayout()
        Me.SplitContainerOutput.Panel2.ResumeLayout(False)
        CType(Me.SplitContainerOutput, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainerOutput.ResumeLayout(False)
        Me.SplitContainerTreeOutput.Panel1.ResumeLayout(False)
        Me.SplitContainerTreeOutput.Panel2.ResumeLayout(False)
        CType(Me.SplitContainerTreeOutput, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainerTreeOutput.ResumeLayout(False)
        Me.PanelListOutput.ResumeLayout(False)
        Me.PanelButtonsOutput.ResumeLayout(False)
        Me.PanelPatchOutput.ResumeLayout(False)
        Me.PanelPatchOutput.PerformLayout()
        Me.ToolStripContainer.BottomToolStripPanel.ResumeLayout(False)
        Me.ToolStripContainer.BottomToolStripPanel.PerformLayout()
        Me.ToolStripContainer.ContentPanel.ResumeLayout(False)
        Me.ToolStripContainer.TopToolStripPanel.ResumeLayout(False)
        Me.ToolStripContainer.TopToolStripPanel.PerformLayout()
        Me.ToolStripContainer.ResumeLayout(False)
        Me.ToolStripContainer.PerformLayout()
        CType(Me.КамераInputDataSet, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.КамераOutputDataSet, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ГорелкаOutputBindingSource, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ГорелкаInputBindingSource, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ИзделиеInputBindingSource, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ИзделиеOutputBindingSource, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ГребенкаАInputBindingSource, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ГребенкаАOutputBindingSource, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ГребенкаБInputBindingSource, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ГребенкаБOutputBindingSource, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.КонтрольЭДСInputBindingSource, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.КонтрольЭДСOutputBindingSource, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ПолеInputBindingSource, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ПолеOutputBindingSource, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ТочкаInputBindingSource, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ТочкаOutputBindingSource, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents SplitContainerAll As System.Windows.Forms.SplitContainer
    Friend WithEvents SplitContainerInput As System.Windows.Forms.SplitContainer
    Friend WithEvents PanelListInput As System.Windows.Forms.Panel
    Friend WithEvents PanelButtonsInput As System.Windows.Forms.Panel
    Friend WithEvents PanelPatchInput As System.Windows.Forms.Panel
    Friend WithEvents ListBoxInput As System.Windows.Forms.ListBox
    Friend WithEvents ButtonDeleteInput As System.Windows.Forms.Button
    Public WithEvents TextPathSource As System.Windows.Forms.TextBox
    Public WithEvents LabelPathInput As System.Windows.Forms.Label
    Friend WithEvents TextBoxInput As System.Windows.Forms.TextBox
    Friend WithEvents ButtonMoveInputToOutput As System.Windows.Forms.Button
    Friend WithEvents ButtonReviewSource As System.Windows.Forms.Button
    Friend WithEvents TextBoxOutput As System.Windows.Forms.TextBox
    Friend WithEvents SplitContainerTreeOutput As System.Windows.Forms.SplitContainer
    Friend WithEvents TreeViewOutput As System.Windows.Forms.TreeView
    Friend WithEvents ListViewOutput As System.Windows.Forms.ListView
    Friend WithEvents PanelListOutput As System.Windows.Forms.Panel
    Friend WithEvents ListBoxOutput As System.Windows.Forms.ListBox
    Friend WithEvents PanelButtonsOutput As System.Windows.Forms.Panel
    Friend WithEvents ButtonMoveOutputToInput As System.Windows.Forms.Button
    Friend WithEvents ButtonDeleteOutput As System.Windows.Forms.Button
    Friend WithEvents PanelPatchOutput As System.Windows.Forms.Panel
    Friend WithEvents ButtonReviewReceiver As System.Windows.Forms.Button
    Public WithEvents TextPathReceiver As System.Windows.Forms.TextBox
    Public WithEvents LabelPathOutput As System.Windows.Forms.Label
    Friend WithEvents SplitContainerOutput As System.Windows.Forms.SplitContainer
    Public WithEvents DialogOpen As System.Windows.Forms.OpenFileDialog
    Public WithEvents imlToolbarIcons As System.Windows.Forms.ImageList
    Public WithEvents DialogSave As System.Windows.Forms.SaveFileDialog
    Friend WithEvents ContextMenuStripTreeDelete As System.Windows.Forms.ContextMenuStrip
    Friend WithEvents TSMenuItemDelete As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents imlSmallIcons As System.Windows.Forms.ImageList
    Friend WithEvents КамераInputDataSet As Chamber.КамераDataSet
    Friend WithEvents КамераOutputDataSet As Chamber.КамераDataSet
    Friend WithEvents ГорелкаOutputBindingSource As System.Windows.Forms.BindingSource
    Friend WithEvents ГорелкаInputBindingSource As System.Windows.Forms.BindingSource
    Friend WithEvents ИзделиеInputBindingSource As System.Windows.Forms.BindingSource
    Friend WithEvents ИзделиеOutputBindingSource As System.Windows.Forms.BindingSource
    Friend WithEvents ГребенкаАInputBindingSource As System.Windows.Forms.BindingSource
    Friend WithEvents ГребенкаАOutputBindingSource As System.Windows.Forms.BindingSource
    Friend WithEvents ГребенкаБInputBindingSource As System.Windows.Forms.BindingSource
    Friend WithEvents ГребенкаБOutputBindingSource As System.Windows.Forms.BindingSource
    Friend WithEvents КонтрольЭДСInputBindingSource As System.Windows.Forms.BindingSource
    Friend WithEvents КонтрольЭДСOutputBindingSource As System.Windows.Forms.BindingSource
    Friend WithEvents ПолеInputBindingSource As System.Windows.Forms.BindingSource
    Friend WithEvents ПолеOutputBindingSource As System.Windows.Forms.BindingSource
    Friend WithEvents ТочкаInputBindingSource As System.Windows.Forms.BindingSource
    Friend WithEvents ТочкаOutputBindingSource As System.Windows.Forms.BindingSource
    Friend WithEvents ИзделиеInputTableAdapter As Chamber.КамераDataSetTableAdapters.ИзделиеTableAdapter
    Friend WithEvents ГорелкаInputTableAdapter As Chamber.КамераDataSetTableAdapters.ГорелкаTableAdapter
    Friend WithEvents ГребенкаАInputTableAdapter As Chamber.КамераDataSetTableAdapters.ГребенкаАTableAdapter
    Friend WithEvents ГребенкаБInputTableAdapter As Chamber.КамераDataSetTableAdapters.ГребенкаБTableAdapter
    Friend WithEvents КонтрольЭДСInputTableAdapter As Chamber.КамераDataSetTableAdapters.КонтрольЭДСTableAdapter
    Friend WithEvents ПолеInputTableAdapter As Chamber.КамераDataSetTableAdapters.ПолеTableAdapter
    Friend WithEvents ТочкаInputTableAdapter As Chamber.КамераDataSetTableAdapters.ТочкаTableAdapter
    Friend WithEvents ИзделиеOutputTableAdapter As Chamber.КамераDataSetTableAdapters.ИзделиеTableAdapter
    Friend WithEvents ГорелкаOutputTableAdapter As Chamber.КамераDataSetTableAdapters.ГорелкаTableAdapter
    Friend WithEvents ГребенкаАOutputTableAdapter As Chamber.КамераDataSetTableAdapters.ГребенкаАTableAdapter
    Friend WithEvents ГребенкаБOutputTableAdapter As Chamber.КамераDataSetTableAdapters.ГребенкаБTableAdapter
    Friend WithEvents КонтрольЭДСOutputTableAdapter As Chamber.КамераDataSetTableAdapters.КонтрольЭДСTableAdapter
    Friend WithEvents ПолеOutputTableAdapter As Chamber.КамераDataSetTableAdapters.ПолеTableAdapter
    Friend WithEvents ТочкаOutputTableAdapter As Chamber.КамераDataSetTableAdapters.ТочкаTableAdapter
    Friend WithEvents ToolStripStatusLabel1 As System.Windows.Forms.ToolStripStatusLabel
    Friend WithEvents ToolStripStatusLabel2 As System.Windows.Forms.ToolStripStatusLabel
    Friend WithEvents ToolStripStatusLabel3 As System.Windows.Forms.ToolStripStatusLabel
    Friend WithEvents ToolStripStatusLabel4 As System.Windows.Forms.ToolStripStatusLabel
    Friend WithEvents ToolStripStatusLabel5 As System.Windows.Forms.ToolStripStatusLabel
    Friend WithEvents ToolStripStatusLabel6 As System.Windows.Forms.ToolStripStatusLabel
    Friend WithEvents TSProgressBar As System.Windows.Forms.ToolStripProgressBar
    Friend WithEvents ToolStripSeparator9 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents DeleteToolStripButton As System.Windows.Forms.ToolStripButton
    Friend WithEvents PackInputToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents PackOutputToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents LoadToolStripButton As System.Windows.Forms.ToolStripButton
    Friend WithEvents FolderBrowserDialog1 As System.Windows.Forms.FolderBrowserDialog
    Friend WithEvents CleanOutputToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents SaveAsOutputToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem

End Class
