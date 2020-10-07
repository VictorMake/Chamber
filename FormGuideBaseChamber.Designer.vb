<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FormGuideBaseChamber
    Inherits System.Windows.Forms.Form

    'Форма переопределяет dispose для очистки списка компонентов.
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

    'Является обязательной для конструктора форм Windows Forms
    Private components As System.ComponentModel.IContainer

    'Примечание: следующая процедура является обязательной для конструктора форм Windows Forms
    'Для ее изменения используйте конструктор форм Windows Form.  
    'Не изменяйте ее в редакторе исходного кода.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim DataGridViewCellStyle4 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle5 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle6 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FormGuideBaseChamber))
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.DataGridFields = New System.Windows.Forms.DataGridView()
        Me.ButtonRowMoveFirst = New System.Windows.Forms.Button()
        Me.LabelRowPosition = New System.Windows.Forms.Label()
        Me.ButtonRowMoveLast = New System.Windows.Forms.Button()
        Me.ButtonRowMoveNext = New System.Windows.Forms.Button()
        Me.ButtonRowMovePrevious = New System.Windows.Forms.Button()
        Me.ButtonApply = New System.Windows.Forms.Button()
        Me.ButtonCancel = New System.Windows.Forms.Button()
        Me.ButtonRowDelete = New System.Windows.Forms.Button()
        Me.BindingSourceTableFields = New System.Windows.Forms.BindingSource(Me.components)
        Me.PanelGrid = New System.Windows.Forms.Panel()
        Me.LabelDescriptionFields = New System.Windows.Forms.Label()
        Me.TableLayoutPanelNavigation = New System.Windows.Forms.TableLayoutPanel()
        CType(Me.DataGridFields, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.BindingSourceTableFields, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.PanelGrid.SuspendLayout()
        Me.TableLayoutPanelNavigation.SuspendLayout()
        Me.SuspendLayout()
        '
        'DataGridChamberFields
        '
        Me.DataGridFields.AllowUserToAddRows = False
        Me.DataGridFields.AllowUserToDeleteRows = False
        DataGridViewCellStyle4.BackColor = System.Drawing.Color.Lavender
        DataGridViewCellStyle4.ForeColor = System.Drawing.Color.MidnightBlue
        DataGridViewCellStyle4.SelectionBackColor = System.Drawing.Color.Teal
        DataGridViewCellStyle4.SelectionForeColor = System.Drawing.Color.PaleGreen
        Me.DataGridFields.AlternatingRowsDefaultCellStyle = DataGridViewCellStyle4
        Me.DataGridFields.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells
        Me.DataGridFields.BackgroundColor = System.Drawing.Color.Lavender
        Me.DataGridFields.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.DataGridFields.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.Sunken
        DataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        DataGridViewCellStyle5.BackColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle5.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(204, Byte))
        DataGridViewCellStyle5.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle5.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle5.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.DataGridFields.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle5
        Me.DataGridFields.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        DataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle6.BackColor = System.Drawing.Color.GhostWhite
        DataGridViewCellStyle6.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(204, Byte))
        DataGridViewCellStyle6.ForeColor = System.Drawing.Color.MidnightBlue
        DataGridViewCellStyle6.SelectionBackColor = System.Drawing.Color.Teal
        DataGridViewCellStyle6.SelectionForeColor = System.Drawing.Color.PaleGreen
        DataGridViewCellStyle6.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
        Me.DataGridFields.DefaultCellStyle = DataGridViewCellStyle6
        Me.DataGridFields.Dock = System.Windows.Forms.DockStyle.Fill
        Me.DataGridFields.GridColor = System.Drawing.Color.RoyalBlue
        Me.DataGridFields.Location = New System.Drawing.Point(0, 17)
        Me.DataGridFields.MultiSelect = False
        Me.DataGridFields.Name = "DataGridChamberFields"
        Me.DataGridFields.ReadOnly = True
        Me.DataGridFields.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.DataGridFields.Size = New System.Drawing.Size(475, 302)
        Me.DataGridFields.TabIndex = 40
        Me.ToolTip1.SetToolTip(Me.DataGridFields, "Существующие записи испытаний камеры сгорания")
        '
        'ButtonRowMoveFirst
        '
        Me.ButtonRowMoveFirst.BackColor = System.Drawing.Color.Silver
        Me.ButtonRowMoveFirst.Cursor = System.Windows.Forms.Cursors.Default
        Me.ButtonRowMoveFirst.Dock = System.Windows.Forms.DockStyle.Fill
        Me.ButtonRowMoveFirst.ForeColor = System.Drawing.SystemColors.ControlText
        Me.ButtonRowMoveFirst.Image = CType(resources.GetObject("ButtonRowMoveFirst.Image"), System.Drawing.Image)
        Me.ButtonRowMoveFirst.Location = New System.Drawing.Point(96, 6)
        Me.ButtonRowMoveFirst.Name = "ButtonRowMoveFirst"
        Me.ButtonRowMoveFirst.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.ButtonRowMoveFirst.Size = New System.Drawing.Size(34, 30)
        Me.ButtonRowMoveFirst.TabIndex = 39
        Me.ButtonRowMoveFirst.Tag = "102"
        Me.ToolTip1.SetToolTip(Me.ButtonRowMoveFirst, "В начало таблицы")
        Me.ButtonRowMoveFirst.UseVisualStyleBackColor = False
        '
        'LabelRowPosition
        '
        Me.LabelRowPosition.BackColor = System.Drawing.SystemColors.Control
        Me.LabelRowPosition.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.LabelRowPosition.Dock = System.Windows.Forms.DockStyle.Fill
        Me.LabelRowPosition.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LabelRowPosition.Location = New System.Drawing.Point(176, 3)
        Me.LabelRowPosition.Name = "LabelRowPosition"
        Me.LabelRowPosition.Size = New System.Drawing.Size(124, 36)
        Me.LabelRowPosition.TabIndex = 38
        Me.LabelRowPosition.Text = "Строка x из y"
        Me.LabelRowPosition.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.ToolTip1.SetToolTip(Me.LabelRowPosition, "Текущая запись")
        '
        'ButtonRowMoveLast
        '
        Me.ButtonRowMoveLast.BackColor = System.Drawing.Color.Silver
        Me.ButtonRowMoveLast.Cursor = System.Windows.Forms.Cursors.Default
        Me.ButtonRowMoveLast.Dock = System.Windows.Forms.DockStyle.Fill
        Me.ButtonRowMoveLast.ForeColor = System.Drawing.SystemColors.ControlText
        Me.ButtonRowMoveLast.Image = CType(resources.GetObject("ButtonRowMoveLast.Image"), System.Drawing.Image)
        Me.ButtonRowMoveLast.Location = New System.Drawing.Point(346, 6)
        Me.ButtonRowMoveLast.Name = "ButtonRowMoveLast"
        Me.ButtonRowMoveLast.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.ButtonRowMoveLast.Size = New System.Drawing.Size(34, 30)
        Me.ButtonRowMoveLast.TabIndex = 41
        Me.ButtonRowMoveLast.Tag = "103"
        Me.ToolTip1.SetToolTip(Me.ButtonRowMoveLast, "В конец таблицы")
        Me.ButtonRowMoveLast.UseVisualStyleBackColor = False
        '
        'ButtonRowMoveNext
        '
        Me.ButtonRowMoveNext.BackColor = System.Drawing.Color.Silver
        Me.ButtonRowMoveNext.Cursor = System.Windows.Forms.Cursors.Default
        Me.ButtonRowMoveNext.Dock = System.Windows.Forms.DockStyle.Fill
        Me.ButtonRowMoveNext.ForeColor = System.Drawing.SystemColors.ControlText
        Me.ButtonRowMoveNext.Image = CType(resources.GetObject("ButtonRowMoveNext.Image"), System.Drawing.Image)
        Me.ButtonRowMoveNext.Location = New System.Drawing.Point(306, 6)
        Me.ButtonRowMoveNext.Name = "ButtonRowMoveNext"
        Me.ButtonRowMoveNext.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.ButtonRowMoveNext.Size = New System.Drawing.Size(34, 30)
        Me.ButtonRowMoveNext.TabIndex = 39
        Me.ButtonRowMoveNext.Tag = "103"
        Me.ToolTip1.SetToolTip(Me.ButtonRowMoveNext, "Следующая запись")
        Me.ButtonRowMoveNext.UseVisualStyleBackColor = False
        '
        'ButtonRowMovePrevious
        '
        Me.ButtonRowMovePrevious.BackColor = System.Drawing.Color.Silver
        Me.ButtonRowMovePrevious.Cursor = System.Windows.Forms.Cursors.Default
        Me.ButtonRowMovePrevious.Dock = System.Windows.Forms.DockStyle.Fill
        Me.ButtonRowMovePrevious.ForeColor = System.Drawing.SystemColors.ControlText
        Me.ButtonRowMovePrevious.Image = CType(resources.GetObject("ButtonRowMovePrevious.Image"), System.Drawing.Image)
        Me.ButtonRowMovePrevious.Location = New System.Drawing.Point(136, 6)
        Me.ButtonRowMovePrevious.Name = "ButtonRowMovePrevious"
        Me.ButtonRowMovePrevious.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.ButtonRowMovePrevious.Size = New System.Drawing.Size(34, 30)
        Me.ButtonRowMovePrevious.TabIndex = 40
        Me.ButtonRowMovePrevious.Tag = "102"
        Me.ToolTip1.SetToolTip(Me.ButtonRowMovePrevious, "Предыдущая запись")
        Me.ButtonRowMovePrevious.UseVisualStyleBackColor = False
        '
        'ButtonApply
        '
        Me.ButtonApply.BackColor = System.Drawing.Color.Silver
        Me.ButtonApply.Cursor = System.Windows.Forms.Cursors.Default
        Me.ButtonApply.DialogResult = System.Windows.Forms.DialogResult.OK
        Me.ButtonApply.Dock = System.Windows.Forms.DockStyle.Fill
        Me.ButtonApply.ForeColor = System.Drawing.SystemColors.ControlText
        Me.ButtonApply.Image = CType(resources.GetObject("ButtonApply.Image"), System.Drawing.Image)
        Me.ButtonApply.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.ButtonApply.Location = New System.Drawing.Point(176, 42)
        Me.ButtonApply.Name = "ButtonApply"
        Me.ButtonApply.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.ButtonApply.Size = New System.Drawing.Size(124, 31)
        Me.ButtonApply.TabIndex = 35
        Me.ButtonApply.Text = "&Считать поле"
        Me.ToolTip1.SetToolTip(Me.ButtonApply, "Загрузить выбранное испытание камеры сгорания")
        Me.ButtonApply.UseVisualStyleBackColor = False
        '
        'ButtonCancel
        '
        Me.ButtonCancel.BackColor = System.Drawing.Color.Silver
        Me.TableLayoutPanelNavigation.SetColumnSpan(Me.ButtonCancel, 2)
        Me.ButtonCancel.Cursor = System.Windows.Forms.Cursors.Default
        Me.ButtonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.ButtonCancel.Dock = System.Windows.Forms.DockStyle.Fill
        Me.ButtonCancel.ForeColor = System.Drawing.SystemColors.ControlText
        Me.ButtonCancel.Image = CType(resources.GetObject("ButtonCancel.Image"), System.Drawing.Image)
        Me.ButtonCancel.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.ButtonCancel.Location = New System.Drawing.Point(346, 42)
        Me.ButtonCancel.Name = "ButtonCancel"
        Me.ButtonCancel.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.ButtonCancel.Size = New System.Drawing.Size(124, 31)
        Me.ButtonCancel.TabIndex = 36
        Me.ButtonCancel.Text = "&Отмена"
        Me.ToolTip1.SetToolTip(Me.ButtonCancel, "Закрыть Окно")
        Me.ButtonCancel.UseVisualStyleBackColor = False
        '
        'ButtonRowDelete
        '
        Me.ButtonRowDelete.BackColor = System.Drawing.Color.Silver
        Me.TableLayoutPanelNavigation.SetColumnSpan(Me.ButtonRowDelete, 2)
        Me.ButtonRowDelete.Dock = System.Windows.Forms.DockStyle.Fill
        Me.ButtonRowDelete.Image = CType(resources.GetObject("ButtonRowDelete.Image"), System.Drawing.Image)
        Me.ButtonRowDelete.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.ButtonRowDelete.Location = New System.Drawing.Point(6, 42)
        Me.ButtonRowDelete.Name = "ButtonRowDelete"
        Me.ButtonRowDelete.Size = New System.Drawing.Size(124, 31)
        Me.ButtonRowDelete.TabIndex = 37
        Me.ButtonRowDelete.Text = "     &Удалить запись"
        Me.ToolTip1.SetToolTip(Me.ButtonRowDelete, "Удалить выделенную запись")
        Me.ButtonRowDelete.UseVisualStyleBackColor = False
        '
        'BindingSourceFoundedSnapshotDataTable
        '
        '
        'PanelGrid
        '
        Me.PanelGrid.Controls.Add(Me.DataGridFields)
        Me.PanelGrid.Controls.Add(Me.LabelDescriptionFields)
        Me.PanelGrid.Dock = System.Windows.Forms.DockStyle.Fill
        Me.PanelGrid.Location = New System.Drawing.Point(5, 5)
        Me.PanelGrid.Name = "PanelGrid"
        Me.PanelGrid.Size = New System.Drawing.Size(475, 319)
        Me.PanelGrid.TabIndex = 0
        '
        'LabelDescriptionStage
        '
        Me.LabelDescriptionFields.Dock = System.Windows.Forms.DockStyle.Top
        Me.LabelDescriptionFields.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(204, Byte))
        Me.LabelDescriptionFields.ForeColor = System.Drawing.Color.Blue
        Me.LabelDescriptionFields.Location = New System.Drawing.Point(0, 0)
        Me.LabelDescriptionFields.Name = "LabelDescriptionStage"
        Me.LabelDescriptionFields.Size = New System.Drawing.Size(475, 17)
        Me.LabelDescriptionFields.TabIndex = 41
        Me.LabelDescriptionFields.Text = "Снятые температурные поля"
        Me.LabelDescriptionFields.TextAlign = System.Drawing.ContentAlignment.TopCenter
        '
        'TableLayoutPanelNavigation
        '
        Me.TableLayoutPanelNavigation.ColumnCount = 7
        Me.TableLayoutPanelNavigation.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 90.0!))
        Me.TableLayoutPanelNavigation.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 40.0!))
        Me.TableLayoutPanelNavigation.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 40.0!))
        Me.TableLayoutPanelNavigation.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 130.0!))
        Me.TableLayoutPanelNavigation.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 40.0!))
        Me.TableLayoutPanelNavigation.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 40.0!))
        Me.TableLayoutPanelNavigation.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 90.0!))
        Me.TableLayoutPanelNavigation.Controls.Add(Me.ButtonApply, 3, 1)
        Me.TableLayoutPanelNavigation.Controls.Add(Me.LabelRowPosition, 3, 0)
        Me.TableLayoutPanelNavigation.Controls.Add(Me.ButtonRowMovePrevious, 2, 0)
        Me.TableLayoutPanelNavigation.Controls.Add(Me.ButtonRowDelete, 0, 1)
        Me.TableLayoutPanelNavigation.Controls.Add(Me.ButtonRowMoveFirst, 1, 0)
        Me.TableLayoutPanelNavigation.Controls.Add(Me.ButtonRowMoveNext, 4, 0)
        Me.TableLayoutPanelNavigation.Controls.Add(Me.ButtonRowMoveLast, 5, 0)
        Me.TableLayoutPanelNavigation.Controls.Add(Me.ButtonCancel, 5, 1)
        Me.TableLayoutPanelNavigation.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.TableLayoutPanelNavigation.Location = New System.Drawing.Point(5, 324)
        Me.TableLayoutPanelNavigation.Name = "TableLayoutPanelNavigation"
        Me.TableLayoutPanelNavigation.Padding = New System.Windows.Forms.Padding(3)
        Me.TableLayoutPanelNavigation.RowCount = 2
        Me.TableLayoutPanelNavigation.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanelNavigation.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanelNavigation.Size = New System.Drawing.Size(475, 79)
        Me.TableLayoutPanelNavigation.TabIndex = 1
        '
        'FormGuidBaseChamberNew
        '
        Me.AcceptButton = Me.ButtonApply
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.CancelButton = Me.ButtonCancel
        Me.ClientSize = New System.Drawing.Size(485, 403)
        Me.Controls.Add(Me.PanelGrid)
        Me.Controls.Add(Me.TableLayoutPanelNavigation)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "FormGuidBaseChamberNew"
        Me.Padding = New System.Windows.Forms.Padding(5, 5, 5, 0)
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Обслуживание записей полей"
        Me.TopMost = True
        CType(Me.DataGridFields, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.BindingSourceTableFields, System.ComponentModel.ISupportInitialize).EndInit()
        Me.PanelGrid.ResumeLayout(False)
        Me.TableLayoutPanelNavigation.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents ToolTip1 As Windows.Forms.ToolTip
    Friend WithEvents BindingSourceTableFields As Windows.Forms.BindingSource
    Friend WithEvents PanelGrid As Windows.Forms.Panel
    Friend WithEvents TableLayoutPanelNavigation As Windows.Forms.TableLayoutPanel
    Friend WithEvents DataGridFields As Windows.Forms.DataGridView
    Friend WithEvents LabelDescriptionFields As Windows.Forms.Label
    Public WithEvents ButtonRowMoveFirst As Windows.Forms.Button
    Public WithEvents ButtonRowMoveLast As Windows.Forms.Button
    Friend WithEvents LabelRowPosition As Windows.Forms.Label
    Public WithEvents ButtonRowMoveNext As Windows.Forms.Button
    Public WithEvents ButtonRowMovePrevious As Windows.Forms.Button
    Public WithEvents ButtonApply As Windows.Forms.Button
    Public WithEvents ButtonCancel As Windows.Forms.Button
    Friend WithEvents ButtonRowDelete As Windows.Forms.Button
End Class
