Imports System.Drawing
Imports System.Windows.Forms
Imports NationalInstruments
Imports NationalInstruments.UI

''' <summary>
''' Представление. Отвечает за отображение информации (визуализацию). 
''' В качестве представления выступает форма (окно) с графическими элементами.
''' </summary>
Friend Class FormColorIntensityView
    ' При обработке реакции пользователя View выбирает, в зависимости от нужной реакции, 
    ' нужный контроллер, который обеспечит ту или иную связь с моделью.
    ' Отвечает за управление событиями пользовательского интерфейса, которое обычно было заботой представления.
    ' Это интерфейс, который отображает данные (Модель) и маршрутизирует пользовательские команды (или события) Presenter-у(Контроллеру), 
    ' чтобы тот действовал над этими данными.
    ' Представление является подписчиком на событие изменения значений свойств или команд, предоставляемых Моделью представления. 
    ' В случае, если в Модели представления изменилось какое-либо свойство, то она оповещает всех подписчиков об этом,
    ' и Представление, в свою очередь, запрашивает обновленное значение свойства из Модели представления.
    ' Представление - это собственно визуальная часть или пользовательский интерфейс взаимодействия с приложением.
    Private Property mController As ControllerTemperature
    Private ReadOnly Property mService As ServiceGlobal

    Private WithEvents mModel As ModelTemperature

    Private Const WidthColorScale As Integer = 88 ' ширина панели цветовой палитры для resize

    Private radioButtonsEnabled As Boolean
    Private editorLaunched As Boolean

    Public Sub New(inControllerTemperature As ControllerTemperature, inServiceGlobal As ServiceGlobal)
        ' Этот вызов является обязательным для конструктора.
        InitializeComponent()

        Me.mController = inControllerTemperature
        Me.mService = inServiceGlobal
        Me.mModel = inControllerTemperature.Model

        ' Добавить код инициализации после вызова InitializeComponent().
        CheckBoxPixelInterpolation.Checked = intensityPlot.PixelInterpolation
        'intensityPlot.SmoothUpdates = True ' установка SmoothUpdates в true улучшает интерактивность курсора. Перестаёт перерисовываться.

        InitializeApplication()
        TableLayoutPanel1.Enabled = False ' выключить от неадекватных действия до момента получения поля
        AddHandler ColorScale.ColorMap.CollectionChanged, AddressOf ColorMap_CollectionChanged
    End Sub

    ''' <summary>
    ''' Первичная инициализация
    ''' </summary>
    Public Sub InitializeApplication()
        ' Скалирование цветового масштаба зависящего от сгенерированных данных
        'colorScale.ScaleColorScale(New NationalInstruments.UI.Range(0, zData(DataSize, DataSize)))
        'intensityPlot.Plot(mController.GetZData) ' отобразить на графике

        radioButtonsEnabled = True
        RadioButtonGrayScaleColors.Checked = True
        RadioButtonCustom.Enabled = False

        intensityCursor.XPosition = mService.DataSize / 2
        intensityCursor.YPosition = mService.DataSize / 2
        CheckBoxIntensityCursor_CheckedChanged(CheckBoxIntensityCursor, Nothing) 'intensityCursor.Visible = False
        ShowAnnotationMinMaxCheckBox_CheckedChanged(ShowAnnotationMinMaxCheckBox, Nothing) 'ShowAnnotationMinMaxCheckBox.Checked = False

        ' установить ограничения на ввод координат
        NumericEditChangeDepthPosition.Range = New Range(0R, mService.ПолнаяШиринаМерногоУчастка + 0.1)
        TableLayoutPanel1.Enabled = True
    End Sub

    'Private Sub ColorScaleViewForm_Load(sender As Object, e As EventArgs) Handles Me.Load
    '    '' запросить координаты минимума и максимума с прямоугольного массива температур
    '    'mController.GetMinMaxValues()
    'End Sub

    Private Sub ColorIntensityViewForm_Activated(sender As Object, e As EventArgs) Handles Me.Activated
        ColorScaleForm_Resize(Me, Nothing)
    End Sub

    Private Sub ColorIntensityViewForm_FormClosing(sender As Object, e As FormClosingEventArgs) Handles Me.FormClosing
        If Not gMainFomMdiParent.IsWindowClosed Then
            e.Cancel = True
        End If
    End Sub

    Private Sub ColorScaleForm_Resize(sender As Object, e As EventArgs) Handles Me.Resize
        ' держать квадрат области графика (без ширина панели цветовой палитры) максимально вписанным в свободную область
        If PanelIntensityGraph.Width <= PanelIntensityGraph.Height + WidthColorScale Then
            intensityGraph.Width = PanelIntensityGraph.Width - 2
            intensityGraph.Height = intensityGraph.Width - WidthColorScale
        Else
            intensityGraph.Height = PanelIntensityGraph.Height - 2
            intensityGraph.Width = intensityGraph.Height + WidthColorScale
        End If
    End Sub

    Private Sub OnRadioButtonCheckedChanged(ByVal sender As Object, ByVal e As EventArgs) Handles RadioButtonRedToneColors.CheckedChanged,
                                                                                                RadioButtonRainbowColors.CheckedChanged,
                                                                                                RadioButtonHighLowNormalColors.CheckedChanged,
                                                                                                RadioButtonHighLowColors.CheckedChanged,
                                                                                                RadioButtonGrayScaleColors.CheckedChanged,
                                                                                                RadioButtonCustom.CheckedChanged,
                                                                                                RadioButtonRainbowColors2.CheckedChanged
        ' Если пользовательская палитра была выбрана, то её выключить.
        If RadioButtonCustom.Enabled = True Then
            RadioButtonCustom.Enabled = False
        End If

        If CType(sender, RadioButton).Checked Then
            ' Конфигурировать палитру в соответствии с предварительными настройками.
            ConfigureColorScale()
            intensityGraph.Refresh()
        End If
    End Sub

    ''' <summary>
    ''' Конфигурировать палитру для рисования включая конфигурирование следующих свойств цветовой шкалы:
    ''' Диапазон, Цветовая палитра, Тип Шкалы, Нижний Цвет, Верхний Цвет и Цветовую Карту.
    ''' </summary>
    Private Sub ConfigureColorScale()
        If radioButtonsEnabled Then
            editorLaunched = False
            ColorScale.ColorMap.Clear()
            ColorScale.Range = New Range(0, 2000)
            ColorScale.InterpolateColor = True
            ColorScale.ScaleType = ScaleType.Linear

            If RadioButtonGrayScaleColors.Checked Then
                ColorScale.LowColor = Color.White
                ColorScale.HighColor = Color.Black
            ElseIf RadioButtonRedToneColors.Checked Then
                ColorScale.LowColor = Color.White 'Color.DarkRed
                ColorScale.ColorMap.Add(500, Color.Brown)
                ColorScale.ColorMap.Add(1000, Color.Red)
                ColorScale.ColorMap.Add(1500, Color.Orange)
                ColorScale.HighColor = Color.Yellow
            ElseIf RadioButtonHighLowColors.Checked Then
                ColorScale.LowColor = Color.White
                ColorScale.ColorMap.Add(500, Color.Blue)
                ColorScale.HighColor = Color.Red
            ElseIf RadioButtonHighLowNormalColors.Checked Then
                ColorScale.LowColor = Color.White
                ColorScale.ColorMap.Add(100, Color.Blue)
                ColorScale.ColorMap.Add(1000, Color.Lime)
                ColorScale.HighColor = Color.Red
            ElseIf RadioButtonRainbowColors2.Checked Then
                ColorScale.ColorMap.AddRange(New ColorMapEntry() {
                    New ColorMapEntry(0, Color.White),
                    New ColorMapEntry(200, Color.Violet),
                    New ColorMapEntry(300, Color.Indigo),
                    New ColorMapEntry(500, Color.Blue),
                    New ColorMapEntry(700, Color.Cyan),
                    New ColorMapEntry(900, Color.Green),
                    New ColorMapEntry(1000, Color.Lime),
                    New ColorMapEntry(1200, Color.Yellow),
                    New ColorMapEntry(1400, Color.Orange),
                    New ColorMapEntry(1600, Color.Red),
                    New ColorMapEntry(2000, Color.Maroon)})

            ElseIf RadioButtonRainbowColors.Checked Then
                ColorScale.LowColor = Color.White ' Color.DarkViolet
                ColorScale.ColorMap.Add(300, Color.Indigo)
                ColorScale.ColorMap.Add(600, Color.Blue)
                ColorScale.ColorMap.Add(1000, Color.Green)
                ColorScale.ColorMap.Add(1400, Color.Yellow)
                ColorScale.ColorMap.Add(1700, Color.Orange)
                ColorScale.HighColor = Color.Red
            End If
        End If
    End Sub

    Private Sub OnEditColorMapButtonClick(ByVal sender As Object, ByVal e As EventArgs) Handles ButtonEditColorMap.Click
        ' Запустить редактор цветовой карты.	
        editorLaunched = True
        PropertyEditorColorMap.EditValue()
    End Sub

    ''' <summary>
    ''' Цветовая карта была модифицирована в редакторе.
    ''' Выключить кнопки других карт.
    ''' </summary>
    Private Sub ColorScaleModified()
        radioButtonsEnabled = False
        RadioButtonGrayScaleColors.Checked = False
        RadioButtonRedToneColors.Checked = False
        RadioButtonHighLowColors.Checked = False
        RadioButtonHighLowNormalColors.Checked = False
        RadioButtonRainbowColors.Checked = False

        ' Включить и выбрать кнопку пользовательской настройки Цветовой Шкалы для сигнализации, что свойство было изменено.
        RadioButtonCustom.Checked = True
        RadioButtonCustom.Enabled = True
        radioButtonsEnabled = True
    End Sub

    ''' <summary>
    ''' Обработчик события изменения палитры для перерисовки графика после ручного редактирования
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Sub ColorMap_CollectionChanged(ByVal sender As Object, ByVal e As System.ComponentModel.CollectionChangeEventArgs)
        If editorLaunched = True Then
            editorLaunched = False
            ColorScaleModified()
            ConfigureColorScale()
        End If
    End Sub

    ''' <summary>
    ''' Пиксельная интерполяция
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub CheckBoxPixelInterpolation_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBoxPixelInterpolation.CheckedChanged
        intensityPlot.PixelInterpolation = CheckBoxPixelInterpolation.Checked
    End Sub

#Region "Annotation"
    ''' <summary>
    ''' Обработчик события запроса поиска минимальной и максимальной температуры
    ''' и установка позиций аннотаций для этих значений.
    ''' Возвращённые значения пересчитанны к размерности графика интенсивности.
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub mModel_MinMaxValuesReached(sender As Object, e As MinMaxValuesEventArgs) Handles mModel.MinMaxValuesReached
        intensityPlot.Plot(mController.GetZData) ' отобразить на графике

        Dim xData As Double() = intensityPlot.GetXData() ' получить значения шкалы X
        Dim yData As Double() = intensityPlot.GetYData() ' получить значения шкалы Y

        ' установить позицию аннотации в точку минимума и максимума
        ' по индексам узнём позицию аннотации
        minIntensityPointAnnotation.XPosition = xData(e.MinPosition.X)
        minIntensityPointAnnotation.YPosition = yData(e.MinPosition.Y)
        maxIntensityPointAnnotation.XPosition = xData(e.MaxPosition.X)
        maxIntensityPointAnnotation.YPosition = yData(e.MaxPosition.Y)

        minIntensityPointAnnotation.Caption = String.Format("Min(высота:{0}, угол:{1}) = {2} C", e.ВысотаMinT, e.УголMinT, Math.Round(e.MinT, 1))
        maxIntensityPointAnnotation.Caption = String.Format("Max(высота:{0}, угол:{1}) = {2} C", e.ВысотаMaxT, e.УголMaxT, Math.Round(e.MaxT, 1))
    End Sub

    'Private Sub OnHideArrowsRadioButtonCheckedChanged(ByVal sender As Object, ByVal e As EventArgs)
    '    minIntensityPointAnnotation.ArrowVisible = Not minIntensityPointAnnotation.ArrowVisible
    '    maxIntensityPointAnnotation.ArrowVisible = Not maxIntensityPointAnnotation.ArrowVisible
    'End Sub

    'Private Sub OnHideShapesRadioButtonCheckedChanged(ByVal sender As Object, ByVal e As EventArgs)
    '    minIntensityPointAnnotation.ShapeVisible = Not minIntensityPointAnnotation.ShapeVisible
    '    maxIntensityPointAnnotation.ShapeVisible = Not maxIntensityPointAnnotation.ShapeVisible
    'End Sub

    ''' <summary>
    ''' Показать/скрыть аннотации.
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub ShowAnnotationMinMaxCheckBox_CheckedChanged(sender As Object, e As EventArgs) Handles ShowAnnotationMinMaxCheckBox.CheckedChanged
        minIntensityPointAnnotation.Visible = ShowAnnotationMinMaxCheckBox.Checked
        maxIntensityPointAnnotation.Visible = ShowAnnotationMinMaxCheckBox.Checked
    End Sub

    'Private Sub OnShowAllRadioButtonCheckedChanged(ByVal sender As Object, ByVal e As EventArgs)
    '    minIntensityPointAnnotation.ArrowVisible = True
    '    maxIntensityPointAnnotation.ArrowVisible = True
    '    minIntensityPointAnnotation.ShapeVisible = True
    '    maxIntensityPointAnnotation.ShapeVisible = True
    '    minIntensityPointAnnotation.Visible = True
    '    maxIntensityPointAnnotation.Visible = True
    'End Sub
#End Region

#Region "Cursors"
    ''' <summary>
    ''' Вывести на индикаторы положение курсора в
    ''' полярных координатах: угол турели и высоту по мерному сечению по позиции курсора;
    ''' индексы текущего подложения курсора.
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub OnCursorAfterMove(ByVal sender As Object, ByVal e As AfterMoveIntensityCursorEventArgs) Handles intensityCursor.AfterMove
        'changeXPositionNumericEdit.Value = intensityCursor.XPosition
        'changeYPositionNumericEdit.Value = intensityCursor.YPosition
        Dim cursorPolarPosition As Polar = mController.GetPolarFromXYPosition(intensityCursor.XPosition, intensityCursor.YPosition)
        NumericEditChangeAnglePosition.Value = cursorPolarPosition.Angle
        NumericEditChangeDepthPosition.Value = cursorPolarPosition.Radius

        Dim xIndex As Integer, yIndex As Integer
        intensityCursor.GetCurrentIndexes(xIndex, yIndex)
        NumericEditChangeCursorXIndex.Value = xIndex
        NumericEditChangeCursorYIndex.Value = yIndex
    End Sub

    ''' <summary>
    ''' Привязать/освободить курсор при перемещении
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub OnCursorModeStateChanged(ByVal sender As Object, ByVal e As ActionEventArgs) Handles SwitchCursorMode.StateChanged
        If SwitchCursorMode.Value Then
            intensityCursor.SnapMode = CursorSnapMode.ToPlot
        Else
            intensityCursor.SnapMode = CursorSnapMode.Floating
        End If

        GroupBoxChangeCursorIndex.Enabled = SwitchCursorMode.Value
    End Sub

    ''' <summary>
    ''' Установить курсор в положение угла и высоты по мерному сечению
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub OnSetPositionClick(ByVal sender As Object, ByVal e As EventArgs) Handles ButtonSetPosition.Click
        Dim xPosition As Double = (mService.ПолнаяШиринаМерногоУчастка - NumericEditChangeDepthPosition.Value) * mService.МасштабВысоты
        Dim yPosition As Double = NumericEditChangeAnglePosition.Value

        ' надо преобразовать позицию из полярных координат в декартовые
        mController.ConvertToPolarIndex(CInt(xPosition), yPosition)
        intensityCursor.MoveCursor(xPosition, yPosition)

        ' для привязанного курсора может быть скачок позиции, поэтому цифровые поля надо подкорректировать
        'changeXPositionNumericEdit.Value = intensityCursor.XPosition
        'changeYPositionNumericEdit.Value = intensityCursor.YPosition

        ' перевести позицию курсора в угол и высоту мерного сечения
        Dim cursorPolarPosition As Polar = mController.GetPolarFromXYPosition(intensityCursor.XPosition, intensityCursor.YPosition)
        NumericEditChangeAnglePosition.Value = cursorPolarPosition.Angle
        NumericEditChangeDepthPosition.Value = cursorPolarPosition.Radius
    End Sub

    Private Sub OnChangeCursorXIndexValueChanged(ByVal sender As Object, ByVal e As BeforeChangeNumericValueEventArgs) Handles NumericEditChangeCursorXIndex.BeforeChangeValue
        Try
            Dim currentXIndex As Integer, currentYIndex As Integer
            intensityCursor.GetCurrentIndexes(currentXIndex, currentYIndex)
            intensityCursor.MoveCursor(CInt(e.NewValue), currentYIndex)
        Catch
            e.Cancel = True
        End Try
    End Sub

    Private Sub OnChangeCursorYIndexValueChanged(ByVal sender As Object, ByVal e As BeforeChangeNumericValueEventArgs) Handles NumericEditChangeCursorYIndex.BeforeChangeValue
        Try
            Dim currentXIndex As Integer, currentYIndex As Integer
            intensityCursor.GetCurrentIndexes(currentXIndex, currentYIndex)
            intensityCursor.MoveCursor(currentXIndex, CInt(e.NewValue))
        Catch
            e.Cancel = True
        End Try
    End Sub

    Private Sub OnCursorMoveBackXClick(ByVal sender As Object, ByVal e As EventArgs) Handles ButtonCursorMoveBackX.Click
        intensityCursor.MovePreviousX()
    End Sub

    Private Sub OnCursorMoveNextXClick(ByVal sender As Object, ByVal e As EventArgs) Handles ButtonCursorMoveNextX.Click
        intensityCursor.MoveNextX()
    End Sub

    Private Sub OnCursorMoveBackYClick(ByVal sender As Object, ByVal e As EventArgs) Handles ButtonCursorMoveDownY.Click
        intensityCursor.MovePreviousY()
    End Sub

    Private Sub OnCursorMoveNextYClick(ByVal sender As Object, ByVal e As EventArgs) Handles ButtonCursorMoveUpY.Click
        intensityCursor.MoveNextY()
    End Sub

    ''' <summary>
    ''' Показать/скрыть курсор
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub CheckBoxIntensityCursor_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBoxIntensityCursor.CheckedChanged
        intensityCursor.Visible = CheckBoxIntensityCursor.Checked
        GroupBoxChangeCursorPosition.Enabled = CheckBoxIntensityCursor.Checked
        GroupBoxChangeCursorIndex.Enabled = CheckBoxIntensityCursor.Checked
        SwitchCursorMode.Enabled = CheckBoxIntensityCursor.Checked
    End Sub

    ''' <summary>
    ''' Показать/скрыть разметку горелок
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub ShowMarkingOutCheckBox_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBoxShowMarkingOut.CheckedChanged
        ShowMarkingOut(CheckBoxShowMarkingOut.Checked)
    End Sub

    ''' <summary>
    ''' Отображение массива развёртки или из памяти или с разметкой и аннотациями секторов.
    ''' </summary>
    ''' <param name="isShowMarking"></param>
    Private Sub ShowMarkingOut(isShowMarking As Boolean)
        If isShowMarking Then
            ' запросить событие обновления от Модели
            mController.GetArrPointВurnerAnnotation()
        Else
            ' удалить разметку горелок
            For I As Integer = intensityGraph.Annotations.Count - 1 To 2 Step -1
                intensityGraph.Annotations.RemoveAt(I)
            Next

            intensityPlot.Plot(mController.GetZDataMemo)
        End If
    End Sub

    ''' <summary>
    ''' Обработчик события модели по запросу GetArrPointIntensityAnnotation от клиента через контроллер.
    ''' Добавление аннотаций разметок горелок и массива температурного поля с секторами.
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub mModel_ВurnersPointReached(sender As Object, e As ВurnersPointEventArgs) Handles mModel.ВurnersPointReached
        Dim arrIntensityAnnotation(mService.ЧислоГорелок - 1) As IntensityAnnotation ' массив разметок горелок

        For I As Integer = 0 To mService.ЧислоГорелок - 1
            ' добавить IntensityPointAnnotation
            Dim mIntensityPointAnnotation As New UI.IntensityPointAnnotation With {
                .ArrowHeadStyle = ArrowStyle.EmptyTriangle,
                .ArrowLineStyle = LineStyle.Dot,
                .ArrowTailAlignment = BoundsAlignment.None,
                .ArrowVisible = False,
                .Caption = (I + 1).ToString,
                .CaptionAlignment = New AnnotationCaptionAlignment(BoundsAlignment.None, 0!, 0!),
                .CaptionFont = New Font("Microsoft Sans Serif", 8.25!, FontStyle.Bold, GraphicsUnit.Point, CType(204, Byte)),
                .CaptionForeColor = Color.Black,
                .ShapeFillColor = Color.Black,
                .ShapeSize = New Size(5, 5),
                .ShapeStyle = ShapeStyle.Oval,
                .InteractionMode = AnnotationInteractionMode.None,
                .XAxis = intensityXAxis,
                .YAxis = intensityYAxis,
                .XPosition = e.XYPositions(I).X,
                .YPosition = e.XYPositions(I).Y
            }

            arrIntensityAnnotation(I) = mIntensityPointAnnotation
        Next

        intensityGraph.Annotations.AddRange(arrIntensityAnnotation)
        intensityPlot.Plot(e.ZDataMarkingOut) ' вывести массив с разметкой
    End Sub

#End Region

#Region "Color Animation"
    ' шкала значений цветовой палитры
    Private arrRange() As Integer = New Integer() {0, 200, 300, 500, 700, 900, 1000, 1200, 1400, 1600, 2000}
    ' шкала цветов цветовой палитры
    Private arrColor() As Color = New Color() {
                                                Color.White,
                                                Color.Violet,
                                                Color.Indigo,
                                                Color.Blue,
                                                Color.Cyan,
                                                Color.Green,
                                                Color.Lime,
                                                Color.Yellow,
                                                Color.Orange,
                                                Color.Red,
                                                Color.Maroon}
    ' индексы значений и цветов шкалы цветовой палитры
    Private arrRangeIndex() As Integer = New Integer() {0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10}
    Private arrColorIndex() As Integer = New Integer() {0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10}

    ''' <summary>
    ''' Запустить таймер анимации
    ''' </summary>
    ''' <param name="isRunAnimation"></param>
    Private Sub RunAnimation(isRunAnimation As Boolean)
        SlideColorHandle.Enabled = Not isRunAnimation

        If isRunAnimation Then
            ColorTimer.Interval = 1200 - CInt(SlideColorTimer.Value * 1000)
            ColorTimer.Start()
        Else
            ' вернуть по умолчанию
            ColorTimer.Stop()
            ConfigureColorScale()
            intensityGraph.Refresh()
        End If
    End Sub

    ''' <summary>
    ''' Запустить анимацию
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub RunAnimationCheckBox_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBoxRunAnimation.CheckedChanged
        RunAnimation(CheckBoxRunAnimation.Checked)
    End Sub

    Private Sub ColorTimer_Tick(sender As Object, e As EventArgs) Handles ColorTimer.Tick
        SetColorTimerTick()
    End Sub

    ''' <summary>
    ''' Создать коллекцию цветовой палитры и нарисовать график.
    ''' </summary>
    Private Sub SetColorMap()
        Dim arrCount As Integer = arrColorIndex.Length - 1
        Dim arrColorMapEntry(arrCount) As ColorMapEntry ' массив пар значения и цвета

        ' создать коллекцию цветовой палитры по индексам
        For I As Integer = 0 To arrCount
            arrColorMapEntry(I) = New ColorMapEntry(arrRange(arrRangeIndex(I)), arrColor(arrColorIndex(I)))
        Next

        ColorScale.ColorMap.Clear()
        ColorScale.ColorMap.AddRange(arrColorMapEntry)
        intensityGraph.Refresh()
    End Sub

    ''' <summary>
    ''' Обновить палитру и подготовить новый расклад индексов.
    ''' </summary>
    Private Sub SetColorTimerTick()
        SetColorMap()

        Dim arrCount As Integer = arrColorIndex.Length - 1

        ' массив индексов переставить в зависимости от напрвления
        If CheckBoxDirection.Checked Then
            ' сверху вниз
            ' arrColorIndex(0) = Color.White всегда на месте
            Dim first As Integer = arrColorIndex(1)

            For I As Integer = 2 To arrCount
                arrColorIndex(I - 1) = arrColorIndex(I)
            Next

            arrColorIndex(arrCount) = first
        Else
            ' снизу вверх
            Dim first As Integer = arrColorIndex(arrCount)

            For I As Integer = arrCount To 2 Step -1
                arrColorIndex(I) = arrColorIndex(I - 1)
            Next

            arrColorIndex(1) = first
        End If
    End Sub

    ''' <summary>
    ''' Установить цветовую палитру по индексу заданному вручную.
    ''' </summary>
    ''' <param name="index"></param>
    Private Sub SetColorByHandleSlader(index As Integer)
        Dim arrCount As Integer = arrColorIndex.Length - 1

        ' смещать индексы до тех пор, пока индексы не встанут в нужный порядок
        Do
            ' сверху вниз
            ' arrColorIndex(0) = Color.White всегда на месте
            Dim first As Integer = arrColorIndex(1)

            For I As Integer = 2 To arrCount
                arrColorIndex(I - 1) = arrColorIndex(I)
            Next

            arrColorIndex(arrCount) = first
        Loop Until arrColorIndex(1) = arrCount + 1 - index

        SetColorMap()
    End Sub

    ''' <summary>
    ''' Установить интервал таймера.
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub ColorSlide_AfterChangeValue(sender As Object, e As AfterChangeNumericValueEventArgs) Handles SlideColorTimer.AfterChangeValue
        If Me.IsHandleCreated Then
            ColorTimer.Interval = 1200 - CInt(SlideColorTimer.Value * 1000)
        End If
    End Sub

    ''' <summary>
    ''' Задать цветовую палитру.
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub ColorHandleSlide_AfterChangeValue(sender As Object, e As AfterChangeNumericValueEventArgs) Handles SlideColorHandle.AfterChangeValue
        If Me.IsHandleCreated Then
            SetColorByHandleSlader(CInt(SlideColorHandle.Value))
        End If
    End Sub

    Private Sub SaveAsToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles SaveAsMenu.Click
        intensityGraph.Caption = GetDescription()
        Dim GraphSave As IntensityGraphSave = New IntensityGraphSave(intensityGraph)
        GraphSave.Save()
        intensityGraph.Caption = "Температурное поле по горелкам"
    End Sub

    Private Sub PrintToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles MenuPrint.Click
        intensityGraph.Caption = GetDescription()
        Dim printer As IntensityGraphPrinter = New IntensityGraphPrinter(intensityGraph)
        printer.Print()
        intensityGraph.Caption = "Температурное поле по горелкам"
    End Sub

    ''' <summary>
    ''' Создание загловка графика с описанием температурного поля.
    ''' </summary>
    ''' <returns></returns>
    Private Function GetDescription() As String
        Return String.Format("Температурное поле изделия №:{0}, дата испытания:{1}, время записи:{2}", НомерИзделия, ДатаЗаписи, ВремяЗаписи)
    End Function

#End Region

End Class
