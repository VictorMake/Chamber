<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class FormColorIntensityView
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

    'Является обязательной для конструктора форм Windows Forms
    Private components As System.ComponentModel.IContainer

    'Примечание: следующая процедура является обязательной для конструктора форм Windows Forms
    'Для ее изменения используйте конструктор форм Windows Form.  
    'Не изменяйте ее в редакторе исходного кода.
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FormColorIntensityView))
        Me.ButtonEditColorMap = New System.Windows.Forms.Button()
        Me.RadioButtonGrayScaleColors = New System.Windows.Forms.RadioButton()
        Me.RadioButtonRedToneColors = New System.Windows.Forms.RadioButton()
        Me.RadioButtonRainbowColors = New System.Windows.Forms.RadioButton()
        Me.RadioButtonHighLowNormalColors = New System.Windows.Forms.RadioButton()
        Me.PropertyEditorColorMap = New NationalInstruments.UI.WindowsForms.PropertyEditor()
        Me.ColorScale = New NationalInstruments.UI.ColorScale()
        Me.intensityGraph = New NationalInstruments.UI.WindowsForms.IntensityGraph()
        Me.maxIntensityPointAnnotation = New NationalInstruments.UI.IntensityPointAnnotation()
        Me.intensityXAxis = New NationalInstruments.UI.IntensityXAxis()
        Me.intensityYAxis = New NationalInstruments.UI.IntensityYAxis()
        Me.minIntensityPointAnnotation = New NationalInstruments.UI.IntensityPointAnnotation()
        Me.intensityCursor = New NationalInstruments.UI.IntensityCursor()
        Me.intensityPlot = New NationalInstruments.UI.IntensityPlot()
        Me.RadioButtonHighLowColors = New System.Windows.Forms.RadioButton()
        Me.GroupBoxSettings = New System.Windows.Forms.GroupBox()
        Me.CheckBoxDirection = New System.Windows.Forms.CheckBox()
        Me.SlideColorHandle = New NationalInstruments.UI.WindowsForms.Slide()
        Me.SlideColorTimer = New NationalInstruments.UI.WindowsForms.Slide()
        Me.CheckBoxRunAnimation = New System.Windows.Forms.CheckBox()
        Me.RadioButtonCustom = New System.Windows.Forms.RadioButton()
        Me.RadioButtonRainbowColors2 = New System.Windows.Forms.RadioButton()
        Me.CheckBoxShowMarkingOut = New System.Windows.Forms.CheckBox()
        Me.CheckBoxPixelInterpolation = New System.Windows.Forms.CheckBox()
        Me.CheckBoxIntensityCursor = New System.Windows.Forms.CheckBox()
        Me.GroupBoxChangeCursorIndex = New System.Windows.Forms.GroupBox()
        Me.NumericEditChangeCursorXIndex = New NationalInstruments.UI.WindowsForms.NumericEdit()
        Me.NumericEditChangeCursorYIndex = New NationalInstruments.UI.WindowsForms.NumericEdit()
        Me.ButtonCursorMoveNextX = New System.Windows.Forms.Button()
        Me.ButtonCursorMoveUpY = New System.Windows.Forms.Button()
        Me.LabelChangeCursorYIndex = New System.Windows.Forms.Label()
        Me.ButtonCursorMoveBackX = New System.Windows.Forms.Button()
        Me.ButtonCursorMoveDownY = New System.Windows.Forms.Button()
        Me.LabelChangeCursorXIndex = New System.Windows.Forms.Label()
        Me.GroupBoxChangeCursorPosition = New System.Windows.Forms.GroupBox()
        Me.LabelChangeYPosition = New System.Windows.Forms.Label()
        Me.LabelChangeXPosition = New System.Windows.Forms.Label()
        Me.ButtonSetPosition = New System.Windows.Forms.Button()
        Me.NumericEditChangeAnglePosition = New NationalInstruments.UI.WindowsForms.NumericEdit()
        Me.NumericEditChangeDepthPosition = New NationalInstruments.UI.WindowsForms.NumericEdit()
        Me.LabelCursorFree = New System.Windows.Forms.Label()
        Me.LabelCursorLocked = New System.Windows.Forms.Label()
        Me.SwitchCursorMode = New NationalInstruments.UI.WindowsForms.Switch()
        Me.TableLayoutPanel1 = New System.Windows.Forms.TableLayoutPanel()
        Me.PanelIntensityGraph = New System.Windows.Forms.Panel()
        Me.PanelSetting = New System.Windows.Forms.Panel()
        Me.ShowAnnotationMinMaxCheckBox = New System.Windows.Forms.CheckBox()
        Me.PanelCursor = New System.Windows.Forms.Panel()
        Me.ColorTimer = New System.Windows.Forms.Timer(Me.components)
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.MenuStrip = New System.Windows.Forms.MenuStrip()
        Me.MenuGraph = New System.Windows.Forms.ToolStripMenuItem()
        Me.SaveAsMenu = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripSeparator2 = New System.Windows.Forms.ToolStripSeparator()
        Me.MenuPrint = New System.Windows.Forms.ToolStripMenuItem()
        CType(Me.intensityGraph, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.intensityCursor, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBoxSettings.SuspendLayout()
        CType(Me.SlideColorHandle, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.SlideColorTimer, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBoxChangeCursorIndex.SuspendLayout()
        CType(Me.NumericEditChangeCursorXIndex, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.NumericEditChangeCursorYIndex, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBoxChangeCursorPosition.SuspendLayout()
        CType(Me.NumericEditChangeAnglePosition, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.NumericEditChangeDepthPosition, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.SwitchCursorMode, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TableLayoutPanel1.SuspendLayout()
        Me.PanelIntensityGraph.SuspendLayout()
        Me.PanelSetting.SuspendLayout()
        Me.PanelCursor.SuspendLayout()
        Me.MenuStrip.SuspendLayout()
        Me.SuspendLayout()
        '
        'ButtonEditColorMap
        '
        Me.ButtonEditColorMap.FlatStyle = System.Windows.Forms.FlatStyle.System
        Me.ButtonEditColorMap.Location = New System.Drawing.Point(15, 182)
        Me.ButtonEditColorMap.Name = "ButtonEditColorMap"
        Me.ButtonEditColorMap.Size = New System.Drawing.Size(95, 35)
        Me.ButtonEditColorMap.TabIndex = 7
        Me.ButtonEditColorMap.Text = "Редактировать палитру"
        Me.ToolTip1.SetToolTip(Me.ButtonEditColorMap, "Редактировать цветовую палитру")
        Me.ButtonEditColorMap.UseVisualStyleBackColor = True
        '
        'RadioButtonGrayScaleColors
        '
        Me.RadioButtonGrayScaleColors.AutoSize = True
        Me.RadioButtonGrayScaleColors.Location = New System.Drawing.Point(15, 23)
        Me.RadioButtonGrayScaleColors.Name = "RadioButtonGrayScaleColors"
        Me.RadioButtonGrayScaleColors.Size = New System.Drawing.Size(56, 17)
        Me.RadioButtonGrayScaleColors.TabIndex = 0
        Me.RadioButtonGrayScaleColors.TabStop = True
        Me.RadioButtonGrayScaleColors.Text = "Серая"
        Me.RadioButtonGrayScaleColors.UseVisualStyleBackColor = True
        '
        'RadioButtonRedToneColors
        '
        Me.RadioButtonRedToneColors.AutoSize = True
        Me.RadioButtonRedToneColors.Location = New System.Drawing.Point(15, 46)
        Me.RadioButtonRedToneColors.Name = "RadioButtonRedToneColors"
        Me.RadioButtonRedToneColors.Size = New System.Drawing.Size(68, 17)
        Me.RadioButtonRedToneColors.TabIndex = 1
        Me.RadioButtonRedToneColors.TabStop = True
        Me.RadioButtonRedToneColors.Text = "Красная"
        Me.RadioButtonRedToneColors.UseVisualStyleBackColor = True
        '
        'RadioButtonRainbowColors
        '
        Me.RadioButtonRainbowColors.AutoSize = True
        Me.RadioButtonRainbowColors.Location = New System.Drawing.Point(15, 115)
        Me.RadioButtonRainbowColors.Name = "RadioButtonRainbowColors"
        Me.RadioButtonRainbowColors.Size = New System.Drawing.Size(69, 17)
        Me.RadioButtonRainbowColors.TabIndex = 4
        Me.RadioButtonRainbowColors.TabStop = True
        Me.RadioButtonRainbowColors.Text = "Радуга 1"
        Me.RadioButtonRainbowColors.UseVisualStyleBackColor = True
        '
        'RadioButtonHighLowNormalColors
        '
        Me.RadioButtonHighLowNormalColors.AutoSize = True
        Me.RadioButtonHighLowNormalColors.Location = New System.Drawing.Point(15, 92)
        Me.RadioButtonHighLowNormalColors.Name = "RadioButtonHighLowNormalColors"
        Me.RadioButtonHighLowNormalColors.Size = New System.Drawing.Size(103, 17)
        Me.RadioButtonHighLowNormalColors.TabIndex = 3
        Me.RadioButtonHighLowNormalColors.TabStop = True
        Me.RadioButtonHighLowNormalColors.Text = "Три цвета"
        Me.RadioButtonHighLowNormalColors.UseVisualStyleBackColor = True
        '
        'PropertyEditorColorMap
        '
        Me.PropertyEditorColorMap.BackColor = System.Drawing.SystemColors.Control
        Me.PropertyEditorColorMap.Location = New System.Drawing.Point(24, 188)
        Me.PropertyEditorColorMap.Name = "PropertyEditorColorMap"
        Me.PropertyEditorColorMap.Size = New System.Drawing.Size(81, 20)
        Me.PropertyEditorColorMap.Source = New NationalInstruments.UI.PropertyEditorSource(Me.ColorScale, "ColorMap")
        Me.PropertyEditorColorMap.TabIndex = 8
        Me.PropertyEditorColorMap.TabStop = False
        Me.PropertyEditorColorMap.Visible = False
        '
        'ColorScale
        '
        Me.ColorScale.Caption = "Градус (С)"
        Me.ColorScale.Range = New NationalInstruments.UI.Range(0R, 10000.0R)
        Me.ColorScale.RightCaptionOrientation = NationalInstruments.UI.VerticalCaptionOrientation.BottomToTop
        '
        'intensityGraph
        '
        Me.intensityGraph.Annotations.AddRange(New NationalInstruments.UI.IntensityAnnotation() {Me.maxIntensityPointAnnotation, Me.minIntensityPointAnnotation})
        Me.intensityGraph.Caption = "Температурное поле по горелкам"
        Me.intensityGraph.ColorScales.AddRange(New NationalInstruments.UI.ColorScale() {Me.ColorScale})
        Me.intensityGraph.Cursors.AddRange(New NationalInstruments.UI.IntensityCursor() {Me.intensityCursor})
        Me.intensityGraph.Location = New System.Drawing.Point(0, 0)
        Me.intensityGraph.Name = "intensityGraph"
        Me.intensityGraph.Plots.AddRange(New NationalInstruments.UI.IntensityPlot() {Me.intensityPlot})
        Me.intensityGraph.Size = New System.Drawing.Size(388, 300)
        Me.intensityGraph.TabIndex = 4
        Me.ToolTip1.SetToolTip(Me.intensityGraph, "Для масштабирования пользоваться клавишами Ctrl и Shift совместно с мышью")
        Me.intensityGraph.XAxes.AddRange(New NationalInstruments.UI.IntensityXAxis() {Me.intensityXAxis})
        Me.intensityGraph.YAxes.AddRange(New NationalInstruments.UI.IntensityYAxis() {Me.intensityYAxis})
        '
        'maxIntensityPointAnnotation
        '
        Me.maxIntensityPointAnnotation.ArrowColor = System.Drawing.Color.Black
        Me.maxIntensityPointAnnotation.ArrowLineWidth = 2.0!
        Me.maxIntensityPointAnnotation.Caption = "Max Value"
        Me.maxIntensityPointAnnotation.CaptionAlignment = New NationalInstruments.UI.AnnotationCaptionAlignment(NationalInstruments.UI.BoundsAlignment.TopRight, 0!, 0!)
        Me.maxIntensityPointAnnotation.CaptionFont = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(204, Byte))
        Me.maxIntensityPointAnnotation.CaptionForeColor = System.Drawing.Color.Black
        Me.maxIntensityPointAnnotation.ShapeFillColor = System.Drawing.Color.Transparent
        Me.maxIntensityPointAnnotation.ShapeSize = New System.Drawing.Size(16, 16)
        Me.maxIntensityPointAnnotation.XAxis = Me.intensityXAxis
        Me.maxIntensityPointAnnotation.XPosition = 1.0R
        Me.maxIntensityPointAnnotation.YAxis = Me.intensityYAxis
        Me.maxIntensityPointAnnotation.YPosition = 9.0R
        '
        'intensityXAxis
        '
        Me.intensityXAxis.Caption = "Intensity X Axis"
        Me.intensityXAxis.Visible = False
        '
        'intensityYAxis
        '
        Me.intensityYAxis.Caption = "Intensity Y Axis"
        Me.intensityYAxis.Visible = False
        '
        'minIntensityPointAnnotation
        '
        Me.minIntensityPointAnnotation.ArrowColor = System.Drawing.Color.Black
        Me.minIntensityPointAnnotation.ArrowLineWidth = 2.0!
        Me.minIntensityPointAnnotation.Caption = "Min Value"
        Me.minIntensityPointAnnotation.CaptionAlignment = New NationalInstruments.UI.AnnotationCaptionAlignment(NationalInstruments.UI.BoundsAlignment.BottomLeft, 0!, 0!)
        Me.minIntensityPointAnnotation.CaptionFont = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(204, Byte))
        Me.minIntensityPointAnnotation.CaptionForeColor = System.Drawing.Color.Black
        Me.minIntensityPointAnnotation.ShapeFillColor = System.Drawing.Color.Transparent
        Me.minIntensityPointAnnotation.ShapeSize = New System.Drawing.Size(16, 16)
        Me.minIntensityPointAnnotation.XAxis = Me.intensityXAxis
        Me.minIntensityPointAnnotation.YAxis = Me.intensityYAxis
        '
        'intensityCursor
        '
        Me.intensityCursor.LabelDisplay = NationalInstruments.UI.IntensityCursorLabelDisplay.ShowZ
        Me.intensityCursor.LabelVisible = True
        Me.intensityCursor.Plot = Me.intensityPlot
        '
        'intensityPlot
        '
        Me.intensityPlot.ColorScale = Me.ColorScale
        Me.intensityPlot.XAxis = Me.intensityXAxis
        Me.intensityPlot.YAxis = Me.intensityYAxis
        '
        'RadioButtonHighLowColors
        '
        Me.RadioButtonHighLowColors.AutoSize = True
        Me.RadioButtonHighLowColors.Location = New System.Drawing.Point(15, 69)
        Me.RadioButtonHighLowColors.Name = "RadioButtonHighLowColors"
        Me.RadioButtonHighLowColors.Size = New System.Drawing.Size(72, 17)
        Me.RadioButtonHighLowColors.TabIndex = 2
        Me.RadioButtonHighLowColors.TabStop = True
        Me.RadioButtonHighLowColors.Text = "Два цвета"
        Me.RadioButtonHighLowColors.UseVisualStyleBackColor = True
        '
        'GroupBoxSettings
        '
        Me.GroupBoxSettings.Controls.Add(Me.CheckBoxDirection)
        Me.GroupBoxSettings.Controls.Add(Me.SlideColorHandle)
        Me.GroupBoxSettings.Controls.Add(Me.ButtonEditColorMap)
        Me.GroupBoxSettings.Controls.Add(Me.SlideColorTimer)
        Me.GroupBoxSettings.Controls.Add(Me.CheckBoxRunAnimation)
        Me.GroupBoxSettings.Controls.Add(Me.RadioButtonGrayScaleColors)
        Me.GroupBoxSettings.Controls.Add(Me.RadioButtonRedToneColors)
        Me.GroupBoxSettings.Controls.Add(Me.RadioButtonCustom)
        Me.GroupBoxSettings.Controls.Add(Me.RadioButtonRainbowColors2)
        Me.GroupBoxSettings.Controls.Add(Me.RadioButtonRainbowColors)
        Me.GroupBoxSettings.Controls.Add(Me.RadioButtonHighLowNormalColors)
        Me.GroupBoxSettings.Controls.Add(Me.PropertyEditorColorMap)
        Me.GroupBoxSettings.Controls.Add(Me.RadioButtonHighLowColors)
        Me.GroupBoxSettings.Location = New System.Drawing.Point(3, 3)
        Me.GroupBoxSettings.Name = "GroupBoxSettings"
        Me.GroupBoxSettings.Size = New System.Drawing.Size(282, 269)
        Me.GroupBoxSettings.TabIndex = 3
        Me.GroupBoxSettings.TabStop = False
        Me.GroupBoxSettings.Text = "Цветовая палитра"
        '
        'CheckBoxDirection
        '
        Me.CheckBoxDirection.AutoSize = True
        Me.CheckBoxDirection.Location = New System.Drawing.Point(153, 46)
        Me.CheckBoxDirection.Name = "CheckBoxDirection"
        Me.CheckBoxDirection.Size = New System.Drawing.Size(112, 17)
        Me.CheckBoxDirection.TabIndex = 10
        Me.CheckBoxDirection.Text = "Направление <->"
        Me.CheckBoxDirection.UseVisualStyleBackColor = True
        '
        'SlideColorHandle
        '
        Me.SlideColorHandle.AutoDivisionSpacing = False
        Me.SlideColorHandle.Caption = "Ручная"
        Me.SlideColorHandle.CoercionMode = NationalInstruments.UI.NumericCoercionMode.ToDivisions
        Me.SlideColorHandle.EditRangeNumericFormatMode = NationalInstruments.UI.NumericFormatMode.CreateGenericMode("F0")
        Me.SlideColorHandle.Location = New System.Drawing.Point(194, 70)
        Me.SlideColorHandle.MajorDivisions.Interval = 1.0R
        Me.SlideColorHandle.Name = "SlideColorHandle"
        Me.SlideColorHandle.Range = New NationalInstruments.UI.Range(1.0R, 10.0R)
        Me.SlideColorHandle.Size = New System.Drawing.Size(61, 193)
        Me.SlideColorHandle.TabIndex = 12
        Me.SlideColorHandle.Value = 1.0R
        '
        'SlideColorTimer
        '
        Me.SlideColorTimer.Caption = "Скорость"
        Me.SlideColorTimer.Location = New System.Drawing.Point(127, 69)
        Me.SlideColorTimer.Name = "SlideColorTimer"
        Me.SlideColorTimer.Range = New NationalInstruments.UI.Range(0.2R, 1.0R)
        Me.SlideColorTimer.Size = New System.Drawing.Size(61, 194)
        Me.SlideColorTimer.TabIndex = 11
        Me.SlideColorTimer.Value = 1.0R
        '
        'CheckBoxRunAnimation
        '
        Me.CheckBoxRunAnimation.AutoSize = True
        Me.CheckBoxRunAnimation.Location = New System.Drawing.Point(153, 23)
        Me.CheckBoxRunAnimation.Name = "CheckBoxRunAnimation"
        Me.CheckBoxRunAnimation.Size = New System.Drawing.Size(77, 17)
        Me.CheckBoxRunAnimation.TabIndex = 9
        Me.CheckBoxRunAnimation.Text = "Анимация"
        Me.CheckBoxRunAnimation.UseVisualStyleBackColor = True
        '
        'RadioButtonCustom
        '
        Me.RadioButtonCustom.AutoSize = True
        Me.RadioButtonCustom.Location = New System.Drawing.Point(15, 161)
        Me.RadioButtonCustom.Name = "RadioButtonCustom"
        Me.RadioButtonCustom.Size = New System.Drawing.Size(98, 17)
        Me.RadioButtonCustom.TabIndex = 6
        Me.RadioButtonCustom.TabStop = True
        Me.RadioButtonCustom.Text = "Пользователь"
        Me.RadioButtonCustom.UseVisualStyleBackColor = True
        '
        'RadioButtonRainbowColors2
        '
        Me.RadioButtonRainbowColors2.AutoSize = True
        Me.RadioButtonRainbowColors2.Location = New System.Drawing.Point(15, 138)
        Me.RadioButtonRainbowColors2.Name = "RadioButtonRainbowColors2"
        Me.RadioButtonRainbowColors2.Size = New System.Drawing.Size(69, 17)
        Me.RadioButtonRainbowColors2.TabIndex = 5
        Me.RadioButtonRainbowColors2.TabStop = True
        Me.RadioButtonRainbowColors2.Text = "Радуга 2"
        Me.RadioButtonRainbowColors2.UseVisualStyleBackColor = True
        '
        'CheckBoxShowMarkingOut
        '
        Me.CheckBoxShowMarkingOut.AutoSize = True
        Me.CheckBoxShowMarkingOut.Location = New System.Drawing.Point(18, 301)
        Me.CheckBoxShowMarkingOut.Name = "CheckBoxShowMarkingOut"
        Me.CheckBoxShowMarkingOut.Size = New System.Drawing.Size(170, 17)
        Me.CheckBoxShowMarkingOut.TabIndex = 1
        Me.CheckBoxShowMarkingOut.Text = "Показать разметку горелок"
        Me.ToolTip1.SetToolTip(Me.CheckBoxShowMarkingOut, "Нанести разметку горелок на график")
        Me.CheckBoxShowMarkingOut.UseVisualStyleBackColor = True
        '
        'CheckBoxPixelInterpolation
        '
        Me.CheckBoxPixelInterpolation.AutoSize = True
        Me.CheckBoxPixelInterpolation.Location = New System.Drawing.Point(18, 278)
        Me.CheckBoxPixelInterpolation.Name = "CheckBoxPixelInterpolation"
        Me.CheckBoxPixelInterpolation.Size = New System.Drawing.Size(94, 17)
        Me.CheckBoxPixelInterpolation.TabIndex = 0
        Me.CheckBoxPixelInterpolation.Text = "Сглаживание"
        Me.ToolTip1.SetToolTip(Me.CheckBoxPixelInterpolation, "Пиксельная интерполяция графика")
        Me.CheckBoxPixelInterpolation.UseVisualStyleBackColor = True
        '
        'CheckBoxIntensityCursor
        '
        Me.CheckBoxIntensityCursor.AutoSize = True
        Me.CheckBoxIntensityCursor.Location = New System.Drawing.Point(15, 3)
        Me.CheckBoxIntensityCursor.Name = "CheckBoxIntensityCursor"
        Me.CheckBoxIntensityCursor.Size = New System.Drawing.Size(183, 17)
        Me.CheckBoxIntensityCursor.TabIndex = 2
        Me.CheckBoxIntensityCursor.Text = "Показать курсор температуры"
        Me.ToolTip1.SetToolTip(Me.CheckBoxIntensityCursor, "Показать курсор температуры")
        Me.CheckBoxIntensityCursor.UseVisualStyleBackColor = True
        '
        'GroupBoxChangeCursorIndex
        '
        Me.GroupBoxChangeCursorIndex.Controls.Add(Me.NumericEditChangeCursorXIndex)
        Me.GroupBoxChangeCursorIndex.Controls.Add(Me.NumericEditChangeCursorYIndex)
        Me.GroupBoxChangeCursorIndex.Controls.Add(Me.ButtonCursorMoveNextX)
        Me.GroupBoxChangeCursorIndex.Controls.Add(Me.ButtonCursorMoveUpY)
        Me.GroupBoxChangeCursorIndex.Controls.Add(Me.LabelChangeCursorYIndex)
        Me.GroupBoxChangeCursorIndex.Controls.Add(Me.ButtonCursorMoveBackX)
        Me.GroupBoxChangeCursorIndex.Controls.Add(Me.ButtonCursorMoveDownY)
        Me.GroupBoxChangeCursorIndex.Controls.Add(Me.LabelChangeCursorXIndex)
        Me.GroupBoxChangeCursorIndex.FlatStyle = System.Windows.Forms.FlatStyle.System
        Me.GroupBoxChangeCursorIndex.Location = New System.Drawing.Point(73, 138)
        Me.GroupBoxChangeCursorIndex.Name = "GroupBoxChangeCursorIndex"
        Me.GroupBoxChangeCursorIndex.Size = New System.Drawing.Size(205, 172)
        Me.GroupBoxChangeCursorIndex.TabIndex = 10
        Me.GroupBoxChangeCursorIndex.TabStop = False
        Me.GroupBoxChangeCursorIndex.Text = "Позиция курсора по индексу"
        '
        'NumericEditChangeCursorXIndex
        '
        Me.NumericEditChangeCursorXIndex.FormatMode = NationalInstruments.UI.NumericFormatMode.CreateSimpleDoubleMode(0)
        Me.NumericEditChangeCursorXIndex.Location = New System.Drawing.Point(80, 67)
        Me.NumericEditChangeCursorXIndex.Name = "NumericEditChangeCursorXIndex"
        Me.NumericEditChangeCursorXIndex.OutOfRangeMode = NationalInstruments.UI.NumericOutOfRangeMode.CoerceToRange
        Me.NumericEditChangeCursorXIndex.Size = New System.Drawing.Size(51, 20)
        Me.NumericEditChangeCursorXIndex.TabIndex = 4
        Me.NumericEditChangeCursorXIndex.Value = 1.0R
        '
        'NumericEditChangeCursorYIndex
        '
        Me.NumericEditChangeCursorYIndex.FormatMode = NationalInstruments.UI.NumericFormatMode.CreateSimpleDoubleMode(0)
        Me.NumericEditChangeCursorYIndex.Location = New System.Drawing.Point(80, 93)
        Me.NumericEditChangeCursorYIndex.Name = "NumericEditChangeCursorYIndex"
        Me.NumericEditChangeCursorYIndex.OutOfRangeMode = NationalInstruments.UI.NumericOutOfRangeMode.CoerceToRange
        Me.NumericEditChangeCursorYIndex.Size = New System.Drawing.Size(51, 20)
        Me.NumericEditChangeCursorYIndex.TabIndex = 5
        Me.NumericEditChangeCursorYIndex.Value = 1.0R
        '
        'ButtonCursorMoveNextX
        '
        Me.ButtonCursorMoveNextX.Image = CType(resources.GetObject("ButtonCursorMoveNextX.Image"), System.Drawing.Image)
        Me.ButtonCursorMoveNextX.Location = New System.Drawing.Point(147, 73)
        Me.ButtonCursorMoveNextX.Name = "ButtonCursorMoveNextX"
        Me.ButtonCursorMoveNextX.Size = New System.Drawing.Size(40, 40)
        Me.ButtonCursorMoveNextX.TabIndex = 3
        Me.ToolTip1.SetToolTip(Me.ButtonCursorMoveNextX, "Вправо")
        Me.ButtonCursorMoveNextX.UseVisualStyleBackColor = True
        '
        'ButtonCursorMoveUpY
        '
        Me.ButtonCursorMoveUpY.Image = CType(resources.GetObject("ButtonCursorMoveUpY.Image"), System.Drawing.Image)
        Me.ButtonCursorMoveUpY.Location = New System.Drawing.Point(80, 21)
        Me.ButtonCursorMoveUpY.Name = "ButtonCursorMoveUpY"
        Me.ButtonCursorMoveUpY.Size = New System.Drawing.Size(40, 40)
        Me.ButtonCursorMoveUpY.TabIndex = 0
        Me.ToolTip1.SetToolTip(Me.ButtonCursorMoveUpY, "Вверх")
        Me.ButtonCursorMoveUpY.UseVisualStyleBackColor = True
        '
        'LabelChangeCursorYIndex
        '
        Me.LabelChangeCursorYIndex.Location = New System.Drawing.Point(61, 90)
        Me.LabelChangeCursorYIndex.Name = "LabelChangeCursorYIndex"
        Me.LabelChangeCursorYIndex.Size = New System.Drawing.Size(23, 23)
        Me.LabelChangeCursorYIndex.TabIndex = 2
        Me.LabelChangeCursorYIndex.Text = "Y:"
        Me.LabelChangeCursorYIndex.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'ButtonCursorMoveBackX
        '
        Me.ButtonCursorMoveBackX.Image = CType(resources.GetObject("ButtonCursorMoveBackX.Image"), System.Drawing.Image)
        Me.ButtonCursorMoveBackX.Location = New System.Drawing.Point(15, 73)
        Me.ButtonCursorMoveBackX.Name = "ButtonCursorMoveBackX"
        Me.ButtonCursorMoveBackX.Size = New System.Drawing.Size(40, 40)
        Me.ButtonCursorMoveBackX.TabIndex = 2
        Me.ToolTip1.SetToolTip(Me.ButtonCursorMoveBackX, "Влево")
        Me.ButtonCursorMoveBackX.UseVisualStyleBackColor = True
        '
        'ButtonCursorMoveDownY
        '
        Me.ButtonCursorMoveDownY.Image = CType(resources.GetObject("ButtonCursorMoveDownY.Image"), System.Drawing.Image)
        Me.ButtonCursorMoveDownY.Location = New System.Drawing.Point(80, 119)
        Me.ButtonCursorMoveDownY.Name = "ButtonCursorMoveDownY"
        Me.ButtonCursorMoveDownY.Size = New System.Drawing.Size(40, 40)
        Me.ButtonCursorMoveDownY.TabIndex = 1
        Me.ToolTip1.SetToolTip(Me.ButtonCursorMoveDownY, "Вниз")
        Me.ButtonCursorMoveDownY.UseVisualStyleBackColor = True
        '
        'LabelChangeCursorXIndex
        '
        Me.LabelChangeCursorXIndex.Location = New System.Drawing.Point(61, 67)
        Me.LabelChangeCursorXIndex.Name = "LabelChangeCursorXIndex"
        Me.LabelChangeCursorXIndex.Size = New System.Drawing.Size(23, 23)
        Me.LabelChangeCursorXIndex.TabIndex = 2
        Me.LabelChangeCursorXIndex.Text = "X:"
        Me.LabelChangeCursorXIndex.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'GroupBoxChangeCursorPosition
        '
        Me.GroupBoxChangeCursorPosition.Controls.Add(Me.LabelChangeYPosition)
        Me.GroupBoxChangeCursorPosition.Controls.Add(Me.LabelChangeXPosition)
        Me.GroupBoxChangeCursorPosition.Controls.Add(Me.ButtonSetPosition)
        Me.GroupBoxChangeCursorPosition.Controls.Add(Me.NumericEditChangeAnglePosition)
        Me.GroupBoxChangeCursorPosition.Controls.Add(Me.NumericEditChangeDepthPosition)
        Me.GroupBoxChangeCursorPosition.FlatStyle = System.Windows.Forms.FlatStyle.System
        Me.GroupBoxChangeCursorPosition.Location = New System.Drawing.Point(15, 26)
        Me.GroupBoxChangeCursorPosition.Name = "GroupBoxChangeCursorPosition"
        Me.GroupBoxChangeCursorPosition.Size = New System.Drawing.Size(263, 106)
        Me.GroupBoxChangeCursorPosition.TabIndex = 9
        Me.GroupBoxChangeCursorPosition.TabStop = False
        Me.GroupBoxChangeCursorPosition.Text = "Позиция курсора на турели"
        '
        'LabelChangeYPosition
        '
        Me.LabelChangeYPosition.Location = New System.Drawing.Point(6, 39)
        Me.LabelChangeYPosition.Name = "LabelChangeYPosition"
        Me.LabelChangeYPosition.Size = New System.Drawing.Size(167, 23)
        Me.LabelChangeYPosition.TabIndex = 2
        Me.LabelChangeYPosition.Text = "Высота по мерному сечению:"
        Me.LabelChangeYPosition.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'LabelChangeXPosition
        '
        Me.LabelChangeXPosition.Location = New System.Drawing.Point(6, 16)
        Me.LabelChangeXPosition.Name = "LabelChangeXPosition"
        Me.LabelChangeXPosition.Size = New System.Drawing.Size(167, 23)
        Me.LabelChangeXPosition.TabIndex = 0
        Me.LabelChangeXPosition.Text = "Угол положения турели:"
        Me.LabelChangeXPosition.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'ButtonSetPosition
        '
        Me.ButtonSetPosition.FlatStyle = System.Windows.Forms.FlatStyle.System
        Me.ButtonSetPosition.Location = New System.Drawing.Point(66, 71)
        Me.ButtonSetPosition.Name = "ButtonSetPosition"
        Me.ButtonSetPosition.Size = New System.Drawing.Size(167, 23)
        Me.ButtonSetPosition.TabIndex = 2
        Me.ButtonSetPosition.Text = "Задать позицию"
        Me.ToolTip1.SetToolTip(Me.ButtonSetPosition, "Установить курсор в заданную позицию")
        '
        'NumericEditChangeAnglePosition
        '
        Me.NumericEditChangeAnglePosition.FormatMode = NationalInstruments.UI.NumericFormatMode.CreateSimpleDoubleMode(2)
        Me.NumericEditChangeAnglePosition.Location = New System.Drawing.Point(179, 19)
        Me.NumericEditChangeAnglePosition.Name = "NumericEditChangeAnglePosition"
        Me.NumericEditChangeAnglePosition.OutOfRangeMode = NationalInstruments.UI.NumericOutOfRangeMode.CoerceToRange
        Me.NumericEditChangeAnglePosition.Range = New NationalInstruments.UI.Range(0R, 360.0R)
        Me.NumericEditChangeAnglePosition.Size = New System.Drawing.Size(54, 20)
        Me.NumericEditChangeAnglePosition.TabIndex = 0
        '
        'NumericEditChangeDepthPosition
        '
        Me.NumericEditChangeDepthPosition.FormatMode = NationalInstruments.UI.NumericFormatMode.CreateSimpleDoubleMode(2)
        Me.NumericEditChangeDepthPosition.Location = New System.Drawing.Point(179, 45)
        Me.NumericEditChangeDepthPosition.Name = "NumericEditChangeDepthPosition"
        Me.NumericEditChangeDepthPosition.OutOfRangeMode = NationalInstruments.UI.NumericOutOfRangeMode.CoerceToRange
        Me.NumericEditChangeDepthPosition.Range = New NationalInstruments.UI.Range(0R, 46.0R)
        Me.NumericEditChangeDepthPosition.Size = New System.Drawing.Size(54, 20)
        Me.NumericEditChangeDepthPosition.TabIndex = 1
        '
        'LabelCursorFree
        '
        Me.LabelCursorFree.Location = New System.Drawing.Point(3, 275)
        Me.LabelCursorFree.Name = "LabelCursorFree"
        Me.LabelCursorFree.Size = New System.Drawing.Size(62, 35)
        Me.LabelCursorFree.TabIndex = 8
        Me.LabelCursorFree.Text = "Курсор свободен"
        Me.LabelCursorFree.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'LabelCursorLocked
        '
        Me.LabelCursorLocked.Location = New System.Drawing.Point(5, 148)
        Me.LabelCursorLocked.Name = "LabelCursorLocked"
        Me.LabelCursorLocked.Size = New System.Drawing.Size(62, 35)
        Me.LabelCursorLocked.TabIndex = 6
        Me.LabelCursorLocked.Text = "Курсор привязан"
        Me.LabelCursorLocked.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'SwitchCursorMode
        '
        Me.SwitchCursorMode.CanShowFocus = True
        Me.SwitchCursorMode.Location = New System.Drawing.Point(3, 186)
        Me.SwitchCursorMode.Name = "SwitchCursorMode"
        Me.SwitchCursorMode.Size = New System.Drawing.Size(64, 86)
        Me.SwitchCursorMode.SwitchStyle = NationalInstruments.UI.SwitchStyle.VerticalToggle3D
        Me.SwitchCursorMode.TabIndex = 0
        Me.ToolTip1.SetToolTip(Me.SwitchCursorMode, "Точная привязка к точкам графика")
        Me.SwitchCursorMode.Value = True
        '
        'TableLayoutPanel1
        '
        Me.TableLayoutPanel1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TableLayoutPanel1.ColumnCount = 2
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 300.0!))
        Me.TableLayoutPanel1.Controls.Add(Me.PanelIntensityGraph, 0, 0)
        Me.TableLayoutPanel1.Controls.Add(Me.PanelSetting, 1, 0)
        Me.TableLayoutPanel1.Location = New System.Drawing.Point(12, 27)
        Me.TableLayoutPanel1.Name = "TableLayoutPanel1"
        Me.TableLayoutPanel1.RowCount = 1
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel1.Size = New System.Drawing.Size(699, 652)
        Me.TableLayoutPanel1.TabIndex = 11
        '
        'PanelIntensityGraph
        '
        Me.PanelIntensityGraph.Controls.Add(Me.intensityGraph)
        Me.PanelIntensityGraph.Dock = System.Windows.Forms.DockStyle.Fill
        Me.PanelIntensityGraph.Location = New System.Drawing.Point(3, 3)
        Me.PanelIntensityGraph.Name = "PanelIntensityGraph"
        Me.PanelIntensityGraph.Size = New System.Drawing.Size(393, 646)
        Me.PanelIntensityGraph.TabIndex = 14
        '
        'PanelSetting
        '
        Me.PanelSetting.Controls.Add(Me.ShowAnnotationMinMaxCheckBox)
        Me.PanelSetting.Controls.Add(Me.PanelCursor)
        Me.PanelSetting.Controls.Add(Me.CheckBoxShowMarkingOut)
        Me.PanelSetting.Controls.Add(Me.GroupBoxSettings)
        Me.PanelSetting.Controls.Add(Me.CheckBoxPixelInterpolation)
        Me.PanelSetting.Dock = System.Windows.Forms.DockStyle.Fill
        Me.PanelSetting.Location = New System.Drawing.Point(402, 3)
        Me.PanelSetting.Name = "PanelSetting"
        Me.PanelSetting.Size = New System.Drawing.Size(294, 646)
        Me.PanelSetting.TabIndex = 12
        '
        'ShowAnnotationMinMaxCheckBox
        '
        Me.ShowAnnotationMinMaxCheckBox.AutoSize = True
        Me.ShowAnnotationMinMaxCheckBox.Location = New System.Drawing.Point(140, 278)
        Me.ShowAnnotationMinMaxCheckBox.Name = "ShowAnnotationMinMaxCheckBox"
        Me.ShowAnnotationMinMaxCheckBox.Size = New System.Drawing.Size(145, 17)
        Me.ShowAnnotationMinMaxCheckBox.TabIndex = 15
        Me.ShowAnnotationMinMaxCheckBox.Text = "Температура Min и Max"
        Me.ToolTip1.SetToolTip(Me.ShowAnnotationMinMaxCheckBox, "Показать позицию минимальной и максимальной температуры")
        Me.ShowAnnotationMinMaxCheckBox.UseVisualStyleBackColor = True
        '
        'PanelCursor
        '
        Me.PanelCursor.Controls.Add(Me.GroupBoxChangeCursorPosition)
        Me.PanelCursor.Controls.Add(Me.LabelCursorFree)
        Me.PanelCursor.Controls.Add(Me.CheckBoxIntensityCursor)
        Me.PanelCursor.Controls.Add(Me.SwitchCursorMode)
        Me.PanelCursor.Controls.Add(Me.LabelCursorLocked)
        Me.PanelCursor.Controls.Add(Me.GroupBoxChangeCursorIndex)
        Me.PanelCursor.Location = New System.Drawing.Point(3, 324)
        Me.PanelCursor.Name = "PanelCursor"
        Me.PanelCursor.Size = New System.Drawing.Size(282, 316)
        Me.PanelCursor.TabIndex = 14
        '
        'ColorTimer
        '
        '
        'MenuStrip
        '
        Me.MenuStrip.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.MenuGraph})
        Me.MenuStrip.Location = New System.Drawing.Point(0, 0)
        Me.MenuStrip.Name = "MenuStrip"
        Me.MenuStrip.Size = New System.Drawing.Size(719, 24)
        Me.MenuStrip.TabIndex = 12
        Me.MenuStrip.Text = "MenuStrip1"
        '
        'MenuGraph
        '
        Me.MenuGraph.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.SaveAsMenu, Me.ToolStripSeparator2, Me.MenuPrint})
        Me.MenuGraph.Name = "MenuGraph"
        Me.MenuGraph.Size = New System.Drawing.Size(60, 20)
        Me.MenuGraph.Text = "&График"
        '
        'SaveAsMenu
        '
        Me.SaveAsMenu.Image = CType(resources.GetObject("SaveAsMenu.Image"), System.Drawing.Image)
        Me.SaveAsMenu.ImageTransparentColor = System.Drawing.Color.Black
        Me.SaveAsMenu.Name = "SaveAsMenu"
        Me.SaveAsMenu.ShortcutKeys = CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.S), System.Windows.Forms.Keys)
        Me.SaveAsMenu.Size = New System.Drawing.Size(193, 22)
        Me.SaveAsMenu.Text = "&Сохранить как"
        Me.SaveAsMenu.ToolTipText = "Сохранить графическое изображение на диске"
        '
        'ToolStripSeparator2
        '
        Me.ToolStripSeparator2.Name = "ToolStripSeparator2"
        Me.ToolStripSeparator2.Size = New System.Drawing.Size(190, 6)
        '
        'MenuPrint
        '
        Me.MenuPrint.Image = CType(resources.GetObject("MenuPrint.Image"), System.Drawing.Image)
        Me.MenuPrint.ImageTransparentColor = System.Drawing.Color.Black
        Me.MenuPrint.Name = "MenuPrint"
        Me.MenuPrint.ShortcutKeys = CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.P), System.Windows.Forms.Keys)
        Me.MenuPrint.Size = New System.Drawing.Size(193, 22)
        Me.MenuPrint.Text = "&Печать"
        Me.MenuPrint.ToolTipText = "Печатать графическое изображение"
        '
        'ColorIntensityViewForm
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(719, 688)
        Me.Controls.Add(Me.MenuStrip)
        Me.Controls.Add(Me.TableLayoutPanel1)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MinimumSize = New System.Drawing.Size(735, 727)
        Me.Name = "ColorIntensityViewForm"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Интенсивность поля"
        CType(Me.intensityGraph, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.intensityCursor, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBoxSettings.ResumeLayout(False)
        Me.GroupBoxSettings.PerformLayout()
        CType(Me.SlideColorHandle, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.SlideColorTimer, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBoxChangeCursorIndex.ResumeLayout(False)
        CType(Me.NumericEditChangeCursorXIndex, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.NumericEditChangeCursorYIndex, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBoxChangeCursorPosition.ResumeLayout(False)
        CType(Me.NumericEditChangeAnglePosition, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.NumericEditChangeDepthPosition, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.SwitchCursorMode, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TableLayoutPanel1.ResumeLayout(False)
        Me.PanelIntensityGraph.ResumeLayout(False)
        Me.PanelSetting.ResumeLayout(False)
        Me.PanelSetting.PerformLayout()
        Me.PanelCursor.ResumeLayout(False)
        Me.PanelCursor.PerformLayout()
        Me.MenuStrip.ResumeLayout(False)
        Me.MenuStrip.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Private WithEvents ButtonEditColorMap As System.Windows.Forms.Button
    Private WithEvents RadioButtonGrayScaleColors As System.Windows.Forms.RadioButton
    Private WithEvents RadioButtonRedToneColors As System.Windows.Forms.RadioButton
    Private WithEvents RadioButtonRainbowColors As System.Windows.Forms.RadioButton
    Private WithEvents RadioButtonHighLowNormalColors As System.Windows.Forms.RadioButton
    Private WithEvents PropertyEditorColorMap As NationalInstruments.UI.WindowsForms.PropertyEditor
    Private WithEvents ColorScale As NationalInstruments.UI.ColorScale
    Private WithEvents intensityGraph As NationalInstruments.UI.WindowsForms.IntensityGraph
    Private WithEvents intensityPlot As NationalInstruments.UI.IntensityPlot
    Private WithEvents intensityXAxis As NationalInstruments.UI.IntensityXAxis
    Private WithEvents intensityYAxis As NationalInstruments.UI.IntensityYAxis
    Private WithEvents RadioButtonHighLowColors As System.Windows.Forms.RadioButton
    Private WithEvents GroupBoxSettings As System.Windows.Forms.GroupBox
    Private WithEvents RadioButtonCustom As System.Windows.Forms.RadioButton
    Private WithEvents CheckBoxPixelInterpolation As System.Windows.Forms.CheckBox
    Private WithEvents RadioButtonRainbowColors2 As System.Windows.Forms.RadioButton
    Friend WithEvents maxIntensityPointAnnotation As NationalInstruments.UI.IntensityPointAnnotation
    Friend WithEvents minIntensityPointAnnotation As NationalInstruments.UI.IntensityPointAnnotation
    Friend WithEvents intensityCursor As NationalInstruments.UI.IntensityCursor
    Friend WithEvents GroupBoxChangeCursorIndex As System.Windows.Forms.GroupBox
    Friend WithEvents LabelChangeCursorYIndex As System.Windows.Forms.Label
    Friend WithEvents LabelChangeCursorXIndex As System.Windows.Forms.Label
    Friend WithEvents NumericEditChangeCursorYIndex As NationalInstruments.UI.WindowsForms.NumericEdit
    Friend WithEvents NumericEditChangeCursorXIndex As NationalInstruments.UI.WindowsForms.NumericEdit
    Friend WithEvents GroupBoxChangeCursorPosition As System.Windows.Forms.GroupBox
    Friend WithEvents LabelChangeYPosition As System.Windows.Forms.Label
    Friend WithEvents LabelChangeXPosition As System.Windows.Forms.Label
    Friend WithEvents ButtonSetPosition As System.Windows.Forms.Button
    Friend WithEvents NumericEditChangeAnglePosition As NationalInstruments.UI.WindowsForms.NumericEdit
    Friend WithEvents NumericEditChangeDepthPosition As NationalInstruments.UI.WindowsForms.NumericEdit
    Friend WithEvents LabelCursorFree As System.Windows.Forms.Label
    Friend WithEvents LabelCursorLocked As System.Windows.Forms.Label
    Friend WithEvents SwitchCursorMode As NationalInstruments.UI.WindowsForms.Switch
    Friend WithEvents CheckBoxIntensityCursor As System.Windows.Forms.CheckBox
    Friend WithEvents TableLayoutPanel1 As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents PanelSetting As System.Windows.Forms.Panel
    Friend WithEvents CheckBoxShowMarkingOut As System.Windows.Forms.CheckBox
    Friend WithEvents CheckBoxRunAnimation As System.Windows.Forms.CheckBox
    Friend WithEvents SlideColorTimer As NationalInstruments.UI.WindowsForms.Slide
    Friend WithEvents ColorTimer As System.Windows.Forms.Timer
    Friend WithEvents CheckBoxDirection As System.Windows.Forms.CheckBox
    Friend WithEvents SlideColorHandle As NationalInstruments.UI.WindowsForms.Slide
    Friend WithEvents PanelIntensityGraph As System.Windows.Forms.Panel
    Friend WithEvents ButtonCursorMoveNextX As System.Windows.Forms.Button
    Friend WithEvents ToolTip1 As System.Windows.Forms.ToolTip
    Friend WithEvents ButtonCursorMoveUpY As System.Windows.Forms.Button
    Friend WithEvents ButtonCursorMoveBackX As System.Windows.Forms.Button
    Friend WithEvents ButtonCursorMoveDownY As System.Windows.Forms.Button
    Friend WithEvents PanelCursor As System.Windows.Forms.Panel
    Friend WithEvents ShowAnnotationMinMaxCheckBox As System.Windows.Forms.CheckBox
    Friend WithEvents MenuStrip As System.Windows.Forms.MenuStrip
    Friend WithEvents MenuGraph As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripSeparator2 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents MenuPrint As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents SaveAsMenu As System.Windows.Forms.ToolStripMenuItem
End Class
