<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FormReportGrid
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
        Dim DataGridViewCellStyle1 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle2 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle3 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FormReportGrid))
        Me.DataGridViewReport = New System.Windows.Forms.DataGridView()
        Me.SaveFileDialogProtocol = New System.Windows.Forms.SaveFileDialog()
        Me.MenuStripForm = New System.Windows.Forms.MenuStrip()
        Me.MenuFile = New System.Windows.Forms.ToolStripMenuItem()
        Me.MenuSaveProtocolAsXLS = New System.Windows.Forms.ToolStripMenuItem()
        Me.MenuPrintProtocol = New System.Windows.Forms.ToolStripMenuItem()
        CType(Me.DataGridViewReport, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.MenuStripForm.SuspendLayout()
        Me.SuspendLayout()
        '
        'DataGridViewOrder
        '
        Me.DataGridViewReport.AllowUserToAddRows = False
        Me.DataGridViewReport.AllowUserToDeleteRows = False
        DataGridViewCellStyle1.BackColor = System.Drawing.Color.LightSteelBlue
        Me.DataGridViewReport.AlternatingRowsDefaultCellStyle = DataGridViewCellStyle1
        DataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle2.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.DataGridViewReport.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle2
        Me.DataGridViewReport.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        DataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Window
        DataGridViewCellStyle3.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.ControlText
        DataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
        Me.DataGridViewReport.DefaultCellStyle = DataGridViewCellStyle3
        Me.DataGridViewReport.Dock = System.Windows.Forms.DockStyle.Fill
        Me.DataGridViewReport.Location = New System.Drawing.Point(0, 24)
        Me.DataGridViewReport.Name = "DataGridViewOrder"
        Me.DataGridViewReport.ReadOnly = True
        Me.DataGridViewReport.Size = New System.Drawing.Size(797, 573)
        Me.DataGridViewReport.TabIndex = 60
        '
        'MenuStripForm
        '
        Me.MenuStripForm.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.MenuFile})
        Me.MenuStripForm.Location = New System.Drawing.Point(0, 0)
        Me.MenuStripForm.Name = "MenuStripForm"
        Me.MenuStripForm.Size = New System.Drawing.Size(797, 24)
        Me.MenuStripForm.TabIndex = 62
        Me.MenuStripForm.Text = "MenuStrip1"
        '
        'MenuFile
        '
        Me.MenuFile.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.MenuSaveProtocolAsXLS, Me.MenuPrintProtocol})
        Me.MenuFile.Name = "MenuFile"
        Me.MenuFile.Size = New System.Drawing.Size(74, 20)
        Me.MenuFile.Text = "&Протокол"
        '
        'MenuSaveProtocolAsXLS
        '
        Me.MenuSaveProtocolAsXLS.Image = CType(resources.GetObject("MenuSaveProtocolAsXLS.Image"), System.Drawing.Image)
        Me.MenuSaveProtocolAsXLS.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.MenuSaveProtocolAsXLS.Name = "MenuSaveProtocolAsXLS"
        Me.MenuSaveProtocolAsXLS.Size = New System.Drawing.Size(251, 22)
        Me.MenuSaveProtocolAsXLS.Text = "&Сохранить протокол поля как ..."
        '
        'MenuPrintProtocol
        '
        Me.MenuPrintProtocol.Enabled = False
        Me.MenuPrintProtocol.Image = CType(resources.GetObject("MenuPrintProtocol.Image"), System.Drawing.Image)
        Me.MenuPrintProtocol.Name = "MenuPrintProtocol"
        Me.MenuPrintProtocol.Size = New System.Drawing.Size(251, 22)
        Me.MenuPrintProtocol.Text = "&Печать протокола поля"
        '
        'ReportGridForm
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(797, 597)
        Me.Controls.Add(Me.DataGridViewReport)
        Me.Controls.Add(Me.MenuStripForm)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MainMenuStrip = Me.MenuStripForm
        Me.Name = "ReportGridForm"
        Me.Text = "Протокол"
        CType(Me.DataGridViewReport, System.ComponentModel.ISupportInitialize).EndInit()
        Me.MenuStripForm.ResumeLayout(False)
        Me.MenuStripForm.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents DataGridViewReport As System.Windows.Forms.DataGridView
    Friend WithEvents SaveFileDialogProtocol As System.Windows.Forms.SaveFileDialog
    Friend WithEvents MenuStripForm As System.Windows.Forms.MenuStrip
    Friend WithEvents MenuFile As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents MenuSaveProtocolAsXLS As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents MenuPrintProtocol As System.Windows.Forms.ToolStripMenuItem
End Class
