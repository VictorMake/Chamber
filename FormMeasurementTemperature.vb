Option Strict Off

Imports System.Windows.Forms
Imports BaseForm
Imports NationalInstruments.Analysis.Math
Imports NationalInstruments.Analysis.Dsp
Imports NationalInstruments.UI
Imports System.Data.OleDb
Imports System.Math
Imports System.Drawing.Printing
Imports System.Threading
Imports System.Drawing
Imports MathematicalLibrary
Imports MathematicalLibrary.Air
Imports MathematicalLibrary.Integral
Imports NationalInstruments.UI.WindowsForms

Public Class FormMeasurementTemperature
    Friend WithEvents PlotT As WaveformPlot ' шлейф температуры
    Friend WithEvents LegendItem As LegendItem ' подпись температуры

    Friend WithEvents XyCursorОкружная As XYCursor
    Friend WithEvents XyCursorMax As XYCursor
    Friend WithEvents XyCursorMin As XYCursor
    Friend WithEvents XyCursorAver As XYCursor

    Private ColorsNet As Color() = {Color.White, Color.Lime, Color.Red, Color.Yellow, Color.DeepSkyBlue, Color.Cyan, Color.Magenta, Color.DeepPink, Color.BlueViolet, Color.CadetBlue}

    Private XyCursorMinDictionary As New Dictionary(Of String, XYCursor)
    Private XyCursorAverDictionary As New Dictionary(Of String, XYCursor)
    Private XyCursorMaxDictionary As New Dictionary(Of String, XYCursor)
    Private XyCursorОкружнаяDictionary As New Dictionary(Of String, XYCursor)
    Private PlotTDictionary As New Dictionary(Of String, WaveformPlot)

    Private arrПоле(ЧИСЛО_ТЕРМОПАР - 1, ЧИСЛО_ПРОМЕЖУТКОВ) As Double
    Private arrЭпюрнаяНеравномерность(ЧИСЛО_ТЕРМОПАР - 1) As Double

    Private СредняяПоТермопаре, СредняяТсредняя_газа_на_входе, СредняяT_интегр, НакопленнаяВСечении As Double 'СредняяТг_расчет
    Private SummaСредняяПоТермопаре(ЧИСЛО_ТЕРМОПАР - 1), SummaТсредняя_газа_на_входе(ЧИСЛО_ТЕРМОПАР - 1), SummaT_интегр(ЧИСЛО_ТЕРМОПАР - 1) As Double 'SummaТг_расчет(ЧИСЛО_ТЕРМОПАР - 1)

    Private Tgmin, Tgmax As Double
    Private NКамерыМин, NПоясаМин, NКамерыМакс, NПоясаМакс As Integer
    Private ИнтегральнаяПоПолю As Double
    Private arrТемперГаза As Double()

    '--- для сплайна ---------------------------------------------------------
    Private x As Double()
    Private Y2 As Double()

    '--- для графика ---------------------------------------------------------
    Private y3 As Double()
    Private xGraf As Double()

    Private ts As TimeSpan
    Private dtStart As DateTime
    Private dtEnd As DateTime
    Private strВремяХодаТурели As String

    Private KeyId, tmpНомерИзделия As Integer
    'Private random As Random = New Random
    Private Const ТОЧНОСТЬ As Integer = 2
    '--- Итоговые вычисления по полю -----------------------------------------
    Private lamdaResult As Double
    Private alphaResult As Double
    Private Тг_расчетResult As Double
    Private T_интегрResult As Double
    Private качествоResult As Double
    Private arrКоординатыГрадусов(ЧИСЛО_ПРОМЕЖУТКОВ) As Double

    '--- Поле ----------------------------------------------------------------
    Private arrСнятыеКамеры(4) As Boolean
    Private arrПолеМинимальные(ЧИСЛО_ГОРЕЛОК, ЧИСЛО_ТЕРМОПАР) As Double
    Private arrПолеМаксимальные(ЧИСЛО_ГОРЕЛОК, ЧИСЛО_ТЕРМОПАР) As Double
    Private arrПолеСредние(ЧИСЛО_ГОРЕЛОК, ЧИСЛО_ТЕРМОПАР) As Double

    Private путьАктXLS As String

    Private Xgor(ЧИСЛО_ГОРЕЛОК) As Double ' текущие координаты горелок
    Private справка As String
    Private первыйЗапуск As Boolean
    Private номерПоля As Integer
    Private Const ARRAY_SIZE As Short = 900
    '--- допуск для Р310 -----------------------------------------------------
    Private Const conPkL As Double = 4.9
    Private Const conPkH As Double = 5.1
    '--- допуск для G отбора % -----------------------------------------------
    Private Const conGotL As Double = 15.7
    Private Const conGotH As Double = 16.3
    '--- допуск для альфа ----------------------------------------------------
    Private Const conAksL As Double = 2.4
    Private Const conAksH As Double = 2.5
    '--- допуск для Лямбда ----------------------------------------------------
    Private Const conLksL As Double = 0.28
    Private Const conLksH As Double = 0.3
    '--- допуск для Т309 ----------------------------------------------------
    Private Const conT309L As Short = 490
    Private Const conT309H As Short = 510
    Private dataГрафик(4, ARRAY_SIZE) As Double
    Private xTemp As Single
    Private lastx As Single

    'Private arrНазваниеПоясов(10) As String
    Private Excel As Object

    Private FlowLayoutPanelControlsSize As New Dictionary(Of Control, Size)

#Region "Форма"
    Sub New()
        MyBase.New()
        InitializeComponent()
        'Me.MdiParent = ChamberRegistration.fMainForm

        upperLimitCursor.YPosition = 110
        lowerLimitCursor.YPosition = 90
        historyXAxis.Range = New Range(0, ARRAY_SIZE - 1)
        historyYAxis.Range = New Range(70, 130)

        coldAnnotation.RangeFillColor = Color.FromArgb(90, Color.Aqua)
        coldAnnotation.RangeFillStyle = FillStyle.CreateVerticalGradient(coldAnnotation.RangeFillColor, Color.Aqua)
        hotAnnotation.RangeFillColor = Color.FromArgb(90, Color.Red)
        hotAnnotation.RangeFillStyle = FillStyle.CreateVerticalGradient(Color.Red, hotAnnotation.RangeFillColor)
    End Sub

    Private Sub MeasurementTemperatureForm_Load(ByVal eventSender As Object, ByVal eventArgs As EventArgs) Handles Me.Load
        gMainFomMdiParent = CType(MdiParent, FrmMain)
        номерПоля = 1

        КонфигурацияГрафиков()
        НастроитьПределыИндикаторов(FlowLayoutPanelControls)
        НастроитьДопускиИндикаторов()
        StatusBar.Items("StatusLabelStend").Text = "Установка " & НомерСтенда.ToString
        StatusBar.Items("StatusLabelNumberField").Text = "Поле " & номерПоля.ToString

        путьАктXLS = PathResourses & "\Акт.xls"
        gПутьКамера = PathResourses & "\Камера.mdb"
        справка = PathResourses & "\Справка\default.htm" ' |    & "query?pg=q?what=web&fmt=.&q="

        ' проверка файлов в каталоге
        If CheckExistsFile(путьАктXLS) Then
        End If
        If CheckExistsFile(gПутьКамера) Then
        End If

        'blnЗаписьТаблИзделие = False
        первыйЗапуск = True

        For I As Integer = 1 To 4
            arrСнятыеКамеры(I) = False
        Next

        СоответствиеГрадусОтсечка()

        'arrНазваниеПоясов(1) = "A1"
        'arrНазваниеПоясов(2) = "Б1"
        'arrНазваниеПоясов(3) = "A2"
        'arrНазваниеПоясов(4) = "Б2"
        'arrНазваниеПоясов(5) = "A3"
        'arrНазваниеПоясов(6) = "Б3"
        'arrНазваниеПоясов(7) = "A4"
        'arrНазваниеПоясов(8) = "Б4"
        'arrНазваниеПоясов(9) = "A5"
        'arrНазваниеПоясов(10) = "Б5"

        ' вначале должно обновляться график по сечению
        gРисоватьГрафикСечений = True
        gГеометрияВведена = False

        ' Test
#If EmulatorT340 = True Then
        arrDataTemperatureTest = GenerateIntensityData(362, 10)
#End If
    End Sub

    Private Sub MeasurementTemperatureForm_FormClosing(ByVal sender As Object, ByVal e As FormClosingEventArgs) Handles Me.FormClosing
        If Not gMainFomMdiParent.IsWindowClosed Then
            e.Cancel = True
        End If

        'Dim Cancel As Boolean = e.Cancel
        'Dim UnloadMode As System.Windows.Forms.CloseReason = e.CloseReason
        If номерПоля < 2 Then
            'не было записи полей надо удалить пустое изделие
            УдалитьКамеру()
        End If
    End Sub

    Private Sub MeasurementTemperatureForm_FormClosed(ByVal sender As Object, ByVal e As FormClosedEventArgs) Handles Me.FormClosed
        gНакопитьДляПоля = False
        gMainFomMdiParent = Nothing
    End Sub

    ''' <summary>
    ''' Связать коллекцию расчётных параметров с контролами.
    ''' </summary>
    Public Sub BindingCalculationWithControls()
        With gMainFomMdiParent.myClassCalculation.CalculatedParam
            .BindingWithControls(CalculatedParameters.G_СУМ_РАСХОД_ТОПЛИВА_КС_КП, GaugeРасходТоплива, NumericEditРасходТоплива)
            .BindingWithControls(CalculatedParameters.G_ОТБОРА_ОТНОСИТЕЛЬНЫЙ, GaugeРасходВоздухаОтбора, NumericEditGaugeРасходВоздухаОтбора)
            .BindingWithControls(CalculatedParameters.АЛЬФА_КАМЕРЫ, MeterАльфаИзбыткаВоздуха, NumericEditАльфаИзбыткаВоздух)
            .BindingWithControls(CalculatedParameters.conЛЯМБДА, MeterЛямбда, NumericEditЛямбда)
            .BindingWithControls(CalculatedParameters.G_ВОЗДУХА, GaugeРасходВоздуха, NumericEditРасходВоздуха)
            .BindingWithControls(CalculatedParameters.conT_ИНТЕГР, ThermometerTинтегр, NumericEditTинтегр)
            .BindingWithControls(CalculatedParameters.Т_Г_РАСЧЕТ, ThermometerTгазРасч, NumericEditTгазРасч)
            .BindingWithControls(CalculatedParameters.conКАЧЕСТВО, SlideКачество, NumericEditКачество)
            .BindingWithControls(CalculatedParameters.Т_СРЕДНЯЯ_ГАЗА_НА_ВХОДЕ, ThermometerT309, NumericEditT309)
        End With
    End Sub

    Private Sub КонфигурацияГрафиков()
        arrПоясDictionary = New Dictionary(Of String, Double())
        ПараметрыПоляНакопленные = New CharacteristicsField(ЧИСЛО_ПРОМЕЖУТКОВ)

        For I As Integer = 0 To ЧИСЛО_ТЕРМОПАР - 1
            '--- PlotT ------------------------------------------------------
            PlotT = New WaveformPlot
            GraphTrendZone.Plots.AddRange({PlotT})
            PlotT.LineColor = ColorsNet(I)
            PlotT.XAxis = XAxisПояса
            PlotT.YAxis = YAxisПояса

            '--- LegendItem --------------------------------------------------
            LegendItem = New LegendItem
            LegendTrends.Items.AddRange({LegendItem})
            LegendItem.Source = PlotT
            LegendItem.Text = "T" & (I + 1).ToString

            '--- XyCursorОкружная --------------------------------------------
            XyCursorОкружная = New XYCursor
            CType(XyCursorОкружная, System.ComponentModel.ISupportInitialize).BeginInit()
            GraphDeviationZone.Cursors.AddRange({XyCursorОкружная})
            XyCursorОкружная.HorizontalCrosshairMode = CursorCrosshairMode.Custom
            XyCursorОкружная.Plot = PlotЭпюрнаяНеравномерность
            XyCursorОкружная.PointStyle = PointStyle.SolidCircle
            XyCursorОкружная.SnapMode = CursorSnapMode.Fixed
            XyCursorОкружная.VerticalCrosshairMode = CursorCrosshairMode.Custom
            CType(XyCursorОкружная, System.ComponentModel.ISupportInitialize).EndInit()

            '--- XyCursorMax -------------------------------------------------
            XyCursorMax = New XYCursor
            CType(XyCursorMax, System.ComponentModel.ISupportInitialize).BeginInit()
            XyCursorMax.Color = Color.Yellow
            XyCursorMax.HorizontalCrosshairMode = CursorCrosshairMode.Custom
            XyCursorMax.Plot = PlotТемператураПоСечению
            XyCursorMax.PointStyle = PointStyle.SolidCircle
            XyCursorMax.SnapMode = CursorSnapMode.Fixed
            XyCursorMax.VerticalCrosshairMode = CursorCrosshairMode.Custom
            CType(XyCursorMax, System.ComponentModel.ISupportInitialize).EndInit()

            '--- XyCursorMin -------------------------------------------------
            XyCursorMin = New XYCursor
            CType(XyCursorMin, System.ComponentModel.ISupportInitialize).BeginInit()
            XyCursorMin.Color = Color.Lime
            XyCursorMin.HorizontalCrosshairMode = CursorCrosshairMode.Custom
            XyCursorMin.Plot = PlotТемператураПоСечению
            XyCursorMin.PointStyle = PointStyle.SolidCircle
            XyCursorMin.SnapMode = CursorSnapMode.Fixed
            XyCursorMin.VerticalCrosshairMode = CursorCrosshairMode.Custom
            CType(XyCursorMin, System.ComponentModel.ISupportInitialize).EndInit()

            '--- XyCursorAver ------------------------------------------------
            XyCursorAver = New XYCursor
            CType(XyCursorAver, System.ComponentModel.ISupportInitialize).BeginInit()
            XyCursorAver.Color = Color.Orange
            XyCursorAver.HorizontalCrosshairMode = CursorCrosshairMode.Custom
            XyCursorAver.Plot = PlotТемператураПоСечению
            XyCursorAver.PointStyle = PointStyle.SolidCircle
            XyCursorAver.SnapMode = CursorSnapMode.Fixed
            XyCursorAver.VerticalCrosshairMode = CursorCrosshairMode.Custom
            CType(XyCursorAver, System.ComponentModel.ISupportInitialize).EndInit()

            GraphProfile.Cursors.AddRange({XyCursorMax, XyCursorMin, XyCursorAver})

            Dim AxisCustomDivision1 As AxisCustomDivision = New AxisCustomDivision
            Dim AxisCustomDivision2 As AxisCustomDivision = New AxisCustomDivision

            '--- XAxisСечение ------------------------------------------------
            AxisCustomDivision1.Text = "T" & (I + 1).ToString
            AxisCustomDivision1.TickColor = Color.Black
            AxisCustomDivision1.Value = КоординатыТермопар(I)
            XAxisСечение.CustomDivisions.AddRange({AxisCustomDivision1})

            '--- XAxisОтклонения ---------------------------------------------
            AxisCustomDivision2.Text = "T" & (I + 1).ToString
            AxisCustomDivision2.TickColor = Color.Black
            AxisCustomDivision2.Value = КоординатыТермопар(I)
            XAxisОтклонения.CustomDivisions.AddRange({AxisCustomDivision2})

            XyCursorMin.XPosition = I + 3
            XyCursorMin.YPosition = 5 + I * 5

            XyCursorAver.XPosition = I + 3
            XyCursorAver.YPosition = 10 + I * 5

            XyCursorMax.XPosition = I + 3
            XyCursorMax.YPosition = 15 + I * 5

            XyCursorОкружная.XPosition = I + 3
            XyCursorОкружная.YPosition = 1

            XyCursorMinDictionary.Add("Min" & (I + 1).ToString, XyCursorMin)
            XyCursorAverDictionary.Add("Aver" & (I + 1).ToString, XyCursorAver)
            XyCursorMaxDictionary.Add("Max" & (I + 1).ToString, XyCursorMax)
            XyCursorОкружнаяDictionary.Add("Окружная" & (I + 1).ToString, XyCursorОкружная)
            PlotTDictionary.Add("PlotT" & (I + 1).ToString, PlotT)
            Dim arr(ЧИСЛО_ПРОМЕЖУТКОВ) As Double
            arrПоясDictionary.Add("Пояс" & (I + 1).ToString, arr)
        Next

        Dim arr2(ЧИСЛО_ПРОМЕЖУТКОВ) As Double
        arrПоясDictionary.Add("Averege", arr2)

        Dim Tbase As Double = 1000

        For Each tempPlot As WaveformPlot In PlotTDictionary.Values
            Dim arrT(360) As Double

            For i As Integer = 0 To 360
                arrT(i) = Tbase + Rnd() * 10
            Next

            Tbase += 100
            tempPlot.PlotY(arrT)
        Next
    End Sub

    Public Sub ИнициализацияПеременных()
        Dim I As Integer
        ' для сплайна
        'ReDim_x(ЧИСЛО_ТЕРМОПАР + 1)
        'ReDim_y(ЧИСЛО_ТЕРМОПАР + 1)
        Re.Dim(x, ЧИСЛО_ТЕРМОПАР + 1)
        Re.Dim(y, ЧИСЛО_ТЕРМОПАР + 1)
        Dim size As Integer = CInt(ШиринаМерногоУчастка * 10)
        'ReDim_y3(size)
        'ReDim_xGraf(size)
        Re.Dim(y3, size)
        Re.Dim(xGraf, size)

        'заполнить через миллиметр
        For I = 1 To size
            xGraf(I) = xGraf(I - 1) + 1 / 10
        Next

        x(0) = 0
        For I = 0 To ЧИСЛО_ТЕРМОПАР - 1
            x(I + 1) = КоординатыТермопар(I)
        Next
        x(ЧИСЛО_ТЕРМОПАР + 1) = ШиринаМерногоУчастка

        For I = 1 To ЧИСЛО_ПРОМЕЖУТКОВ
            arrКоординатыГрадусов(I) = arrКоординатыГрадусов(I - 1) + 1
        Next

        clAir = New Air
        clAir.ИнициализацияПеременных()

        For I = 0 To ЧИСЛО_ТЕРМОПАР - 1
            XyCursorMinDictionary("Min" & (I + 1).ToString).XPosition = КоординатыТермопар(I)
            XyCursorMaxDictionary("Max" & (I + 1).ToString).XPosition = КоординатыТермопар(I)
            XyCursorAverDictionary("Aver" & (I + 1).ToString).XPosition = КоординатыТермопар(I)
            XyCursorОкружнаяDictionary("Окружная" & (I + 1).ToString).XPosition = КоординатыТермопар(I)
            XAxisСечение.CustomDivisions(I).Value = КоординатыТермопар(I)
            XAxisОтклонения.CustomDivisions(I).Value = КоординатыТермопар(I)
        Next

        Dim RangeMinMax As New Range(0, ШиринаМерногоУчастка)
        XAxisСечение.Range = RangeMinMax
        XAxisОтклонения.Range = RangeMinMax
        ТехническиеУсловияНаЗамеренных()

        gMainFomMdiParent.var3D.ИнициализацияAxCWGraph3D()
    End Sub

    Private Sub ТехническиеУсловияНаЗамеренных()
        Dim arrКоординатаТУ_Х(ЧИСЛО_ТЕРМОПАР + 1) As Double
        Dim arrЭпюрнаяНерТУ(ЧИСЛО_ТЕРМОПАР + 1) As Double
        Dim arrОкружнаяНерТУ(ЧИСЛО_ТЕРМОПАР + 1) As Double

        With gMainFomMdiParent.Manager.TuningDataTable
            For I As Integer = 1 To ЧИСЛО_ТЕРМОПАР
                arrКоординатаТУ_Х(I) = .FindByИмяПараметра("КоординатаТУ_Х_" & I.ToString).ЦифровоеЗначение
                arrЭпюрнаяНерТУ(I) = .FindByИмяПараметра("ЭпюрнаяНерТУ_" & I.ToString).ЦифровоеЗначение
                arrОкружнаяНерТУ(I) = .FindByИмяПараметра("ОкружнаяНерТУ_" & I.ToString).ЦифровоеЗначение
            Next
        End With

        arrКоординатаТУ_Х(0) = 0
        arrКоординатаТУ_Х(ЧИСЛО_ТЕРМОПАР + 1) = ШиринаМерногоУчастка
        ПриведениеТУнаСтенки(arrКоординатаТУ_Х, arrЭпюрнаяНерТУ)
        ПриведениеТУнаСтенки(arrКоординатаТУ_Х, arrОкружнаяНерТУ)

        PlotЭпюрнаяТУ.PlotXY(arrКоординатаТУ_Х, arrЭпюрнаяНерТУ)
        PlotОкружнаяТУ.PlotXY(arrКоординатаТУ_Х, arrОкружнаяНерТУ)
    End Sub

    Private Sub ПриведениеТУнаСтенки(ByVal координатыТУ_Х As Double(), ByRef нерТУ As Double())
        Dim inputX(ЧИСЛО_ТЕРМОПАР - 1) As Double
        Dim inputY(ЧИСЛО_ТЕРМОПАР - 1) As Double
        Dim Ystart, Yend As Double

        For I As Integer = 1 To ЧИСЛО_ТЕРМОПАР
            inputX(I - 1) = координатыТУ_Х(I)
            inputY(I - 1) = нерТУ(I)
        Next

        ' НайтиЗначенияТемпературНаСтенкахInterpolate(InputX, InputY, ШиринаМерногоУчастка, Ystart, Yend)
        НайтиЗначенияТемпературНаСтенкахЛинейно(inputX, inputY, ШиринаМерногоУчастка, Ystart, Yend)

        нерТУ(0) = Ystart
        нерТУ(ЧИСЛО_ТЕРМОПАР + 1) = Yend
    End Sub

#End Region

#Region "Events"
    Private Sub ВключитьВыключитьButtonEnabled(ByVal isEnabled As Boolean)
        ButtonRunMeasurement.Enabled = isEnabled
        ButtonRunMeasurement.Checked = Not isEnabled
        ButtonCancelMeasurement.Enabled = Not isEnabled
        SetИндикаторОтсечки(False)
        TextLedCutoff.Visible = Not isEnabled
    End Sub

    Private Sub ButtonRunMeasurement_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs) Handles ButtonRunMeasurement.CheckedChanged
        ' проверить, что сборщик запушен
        If ButtonRunMeasurement.Checked = True Then
            ВключитьВыключитьСчетчик(True)
            ВключитьВыключитьButtonEnabled(False)
            dtStart = DateTime.Now
            StatusBar.Items(conStatusLabelMessage).Text = "Запуск замера поля"
            StatusBar.Items("StatusLabelNumberField").Text = "Поле " & номерПоля.ToString
            StatusBar.Items("StatusLabelStend").Text = "Установка " & НомерСтенда.ToString
            ' зарегистрировать событие СобытиеПеремещения и включить счетчик
            ОчисткаДляПоля()
            XyCursorОтсечка.XPosition = ИндексОтсечекДляПоля
            gРисоватьГрафикСечений = True
            gMainFomMdiParent.varEncoder.ResetPositionEncoder()
        Else
            ' снять регистрирацию событие СобытиеПеремещения и выключить счетчик
            ВключитьВыключитьСчетчик(False)
        End If
    End Sub

    Private Sub ButtonCancelMeasurement_Click(ByVal sender As Object, ByVal e As EventArgs) Handles ButtonCancelMeasurement.Click
        ОчисткаДляПоля()
        ВключитьВыключитьButtonEnabled(True)
        StatusBar.Items(conStatusLabelMessage).Text = "Прерывание замера поля"
    End Sub

    Private Sub ButtonContinue_Click(ByVal sender As Object, ByVal e As EventArgs) Handles ButtonContinue.Click
        ButtonContinue.Visible = False
        ButtonProtocol.Visible = False
        ButtonRunMeasurement.Enabled = True
        gРисоватьГрафикСечений = True

        If Excel IsNot Nothing Then
            Excel.Quit()
            Excel = Nothing
        End If
    End Sub

    Private Sub ButtonProtocol_Click(ByVal sender As Object, ByVal e As EventArgs) Handles ButtonProtocol.Click
        ' запуск Excel протокола
        Excel = CreateObject("excel.application")
        Excel.Visible = True
        Excel.Workbooks.Open(filename:=путьАктXLS)
    End Sub

#End Region

    Private Sub ОчисткаДляПоля()
        gНакопитьДляПоля = False
        SetИндикаторОтсечки(False)
        СчетчикНакоплений = 0
        SlideКачество.Value = 0
        ИндексОтсечекДляПоля = 0
        ' очистка массивов и коллекций
        Array.Clear(arrПоле, 0, arrПоле.Length)
        Array.Clear(SummaСредняяПоТермопаре, 0, SummaСредняяПоТермопаре.Length)
        Array.Clear(SummaТсредняя_газа_на_входе, 0, SummaТсредняя_газа_на_входе.Length)
        'Array.Clear(SummaТг_расчет, 0, SummaТг_расчет.Length)
        Array.Clear(SummaT_интегр, 0, SummaT_интегр.Length)

        For Each arrПоясTemp As Double() In arrПоясDictionary.Values
            Array.Clear(arrПоясTemp, 0, arrПоясTemp.Length)
        Next

        ПараметрыПоляНакопленные.Clear()

        ' обнуление осредненных параметров идущих в расчет
        ' можно через коллекцию графика
        For I As Integer = 0 To GraphProfile.Cursors.Count - 1
            GraphProfile.Cursors(I).YPosition = 0
        Next

        ' можно через обобщенную коллекцию
        For Each XyCursorОкружнаяTemp As XYCursor In XyCursorОкружнаяDictionary.Values
            XyCursorОкружнаяTemp.YPosition = 0
        Next

        For I As Integer = 1 To ЧИСЛО_ТЕРМОПАР
            arrMinMaxПоясов(I).Min = Double.MaxValue
            arrMinMaxПоясов(I).imin = 0
            arrMinMaxПоясов(I).Max = Double.MinValue
            arrMinMaxПоясов(I).imax = 0
            arrMinMaxПоясов(I).dblСредняя = 0
        Next

        With gMainFomMdiParent.Manager
            '    For Each rowРасчетныйПараметр As BaseFormDataSet.РасчетныеПараметрыRow In .РасчетныеПараметры.Rows
            '        rowРасчетныйПараметр.ВычисленноеЗначениеВСИ = 0
            '    Next

            For Each rowИзмеренныйПараметр As BaseFormDataSet.ИзмеренныеПараметрыRow In .MeasurementDataTable.Rows
                rowИзмеренныйПараметр.НакопленноеЗначение = 0
            Next

            For Each rowРасчетныйПараметр As BaseFormDataSet.РасчетныеПараметрыRow In .CalculatedDataTable.Rows
                rowРасчетныйПараметр.НакопленноеЗначение = 0
            Next
        End With

        ПовернутьТурель(0)
    End Sub

    Public Sub Протокол()
        gНакопитьДляПоля = False

        ВключитьВыключитьButtonEnabled(True)
        ' здесь осреднение накопленных параметров и вывод на экран
        ВремяСбора()
        РасчетПоля(True)
        ДатаЗаписи = Today.Date.ToLongDateString 'Today.ToString
        ВремяЗаписи = Now.ToLongTimeString
        ЗаписьПоля()
        ' далее смотреть как в программе Chamber обработка
        ' разбить на горелки, подсчет ПодсчетКонтрольныхТочек и т. д.
        ' может объединить две базы в одну
        ЗаписьПоляChamber()
        номерПоля = номерПоля + 1

        If номерПоля > 4 Then
            номерПоля = 4
            ' выдать предупреждение о конце сбора (надо перегрузить программу) из-за конца количества полей
            MessageBox.Show($"Количество записанных полей для данной камеры равно 4!{Environment.NewLine}Программа должна быть перезагружена.",
                            "Сбор", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            'Exit Do
        End If
    End Sub

    Private Sub НастроитьПределыИндикаторов(ByVal valGroupBox As FlowLayoutPanel)
        With Me
            .GaugeРасходТоплива.Tag = CalculatedParameters.G_СУМ_РАСХОД_ТОПЛИВА_КС_КП
            .GaugeРасходВоздуха.Tag = CalculatedParameters.G_ВОЗДУХА
            .GaugeРасходВоздухаОтбора.Tag = CalculatedParameters.G_ОТБОРА_ОТНОСИТЕЛЬНЫЙ
            .TankСтатическоеДавлениеМерномСопле.Tag = InputParameters.Р310_ПОЛНОЕ_ВОЗДУХА_НА_ВХОДЕ_КC
            .MeterАльфаИзбыткаВоздуха.Tag = CalculatedParameters.АЛЬФА_КАМЕРЫ
            .MeterЛямбда.Tag = CalculatedParameters.conЛЯМБДА
            .ThermometerT309.Tag = CalculatedParameters.Т_СРЕДНЯЯ_ГАЗА_НА_ВХОДЕ
            .ThermometerTгазРасч.Tag = CalculatedParameters.Т_Г_РАСЧЕТ
            .ThermometerTинтегр.Tag = CalculatedParameters.conT_ИНТЕГР
            .SlideКачество.Tag = CalculatedParameters.conКАЧЕСТВО
        End With

        For Each itemControl As Control In valGroupBox.Controls
            FlowLayoutPanelControlsSize(itemControl) = itemControl.Size

            If TypeOf itemControl Is Panel Then
                For Each g2Control As Control In itemControl.Controls
                    If g2Control.Tag IsNot Nothing AndAlso g2Control.Tag.ToString <> "" Then
                        ' NationalInstruments.UI.WindowsForms.NumericPointer есть базовый класс для всех индикаторов
                        If TypeOf (g2Control) Is NumericPointer Then
                            With gMainFomMdiParent.Manager
                                For Each rowРасчетныйПараметр As BaseFormDataSet.РасчетныеПараметрыRow In .CalculatedDataTable.Rows
                                    If rowРасчетныйПараметр.ИмяПараметра = g2Control.Tag.ToString Then
                                        CType(g2Control, NumericPointer).Range = New Range(rowРасчетныйПараметр.ДопускМинимум, rowРасчетныйПараметр.ДопускМаксимум)
                                        Exit For
                                    End If
                                Next
                            End With

                        End If
                    End If
                Next
            End If
        Next
    End Sub

    Private Sub НастроитьДопускиИндикаторов()
        ДопускиИндикаторов(CType(TankСтатическоеДавлениеМерномСопле, NumericPointer), conPkL, conPkH) ' допуск для Р310
        ДопускиИндикаторов(CType(GaugeРасходВоздухаОтбора, NumericPointer), conGotL, conGotH) ' допуск для G отбора %
        ДопускиИндикаторов(CType(MeterАльфаИзбыткаВоздуха, NumericPointer), conAksL, conAksH) ' допуск для альфа
        ДопускиИндикаторов(CType(MeterЛямбда, NumericPointer), conLksL, conLksH) ' допуск для лямбда
        ДопускиИндикаторовТермометр(CType(ThermometerT309, NumericPointer), conT309L, conT309H) ' допуск для Т309
    End Sub

    Private Sub ДопускиИндикаторов(ByRef inNumericPointer As NumericPointer, low As Double, high As Double)
        inNumericPointer.RangeFills.Item(0).Range = New Range(inNumericPointer.Range.Minimum, low)
        inNumericPointer.RangeFills.Item(1).Range = New Range(low, high)
        inNumericPointer.RangeFills.Item(2).Range = New Range(high, inNumericPointer.Range.Maximum)
    End Sub

    Private Sub ДопускиИндикаторовТермометр(ByRef inNumericPointer As NumericPointer, low As Double, high As Double)
        inNumericPointer.RangeFills.Item(0).Range = New Range(inNumericPointer.Range.Minimum, inNumericPointer.Range.Maximum)  ' вначале весь диапазон
        inNumericPointer.RangeFills.Item(1).Range = New Range(low, high) ' диапазон в ТУ
    End Sub

    Public Function ПараметрВДиапазонеНакопленные(ByVal currentValue As Double) As Double
        'If currentValue.ToString = Double.NaN.ToString OrElse currentValue = Double.NegativeInfinity OrElse currentValue = Double.PositiveInfinity Then
        If Double.IsNaN(currentValue) OrElse Double.IsNegativeInfinity(currentValue) OrElse Double.IsPositiveInfinity(currentValue) Then
            'Throw New System.Exception(Имя & " вне диапазона")
            Return 9999999
        Else
            Return currentValue
        End If
    End Function

    Public Sub ShowError(message As String)
        TextError.Text = message
        TextError.Visible = True
    End Sub

    Private Sub SetИндикаторОтсечки(ByVal Value As Boolean)
        'LedОтсечка.Value = Value
        If Value Then
            TextLedCutoff.BackColor = Color.Red
            TextLedCutoff.ForeColor = Color.Yellow
            TimerIndicatorShutoff.Enabled = True
        Else
            TextLedCutoff.BackColor = Color.Maroon 'System.Drawing.SystemColors.Control
            TextLedCutoff.ForeColor = Color.Red
        End If
    End Sub

    Public Sub ОбновитьГрафикиДляТекущейОтсечки()
        Dim I As Integer
        SetИндикаторОтсечки(True)

        ' для интегрирования нужны фактические координаты гребенки
        ' ПараметрыПоляНакопленные(conПоложениеГребенки)(ИндексОтсечекДляПоля) = ПоложениеГребенки ' .FindByИмяПараметра(конПоложениеГребенки).НакопленноеЗначение 
        ' здесь накопление всех накопленных параметров для всего замера поля
        НакопленнаяВСечении = 0
        Dim имяПояса As String
        Dim temperature As Double

        For I = 1 To ЧИСЛО_ТЕРМОПАР
            имяПояса = "Пояс" & I.ToString
            'If ПараметрыПоляНакопленные("T340_" & I.ToString)(ИндексОтсечекДляПоля).ToString = "NaN" Then
            '    ПараметрыПоляНакопленные("T340_" & I.ToString)(ИндексОтсечекДляПоля) = 0
            'End If
            temperature = ПараметрыПоляНакопленные("T340_" & I.ToString)(ИндексОтсечекДляПоля)

            'If Temperature = Double.NaN OrElse Temperature = Double.NegativeInfinity OrElse Temperature = Double.PositiveInfinity Then
            '    Temperature = 0
            'End If
            arrПоясDictionary(имяПояса)(ИндексОтсечекДляПоля) = temperature
            arrПоле(I - 1, ИндексОтсечекДляПоля) = temperature
            НакопленнаяВСечении += temperature
        Next

        arrПоясDictionary("Averege")(ИндексОтсечекДляПоля) = НакопленнаяВСечении / ЧИСЛО_ТЕРМОПАР

        ' для 15Б дурка была что турель начиналась вращаться не 0 отметки а где-то  в конце
        ' поэтому сделан такой геморой, 
        'Dim Aver(ЧислоТермопар - 1) As Double
        'Dim CountAver(ЧислоТермопар - 1) As Integer
        'For I = 1 To ЧислоТермопар
        '    ИмяПояса = "Пояс" & I.ToString
        '    For J = 0 To ЧислоПромежутков
        '        If arrПоясDictionary(ИмяПояса)(J) <> 0 Then
        '            Aver(I - 1) += arrПоясDictionary(ИмяПояса)(J)
        '            CountAver(I - 1) += 1
        '        End If
        '    Next
        'Next
        'For I = 0 To ЧислоТермопар - 1
        '    Aver(I) = Aver(I) / CountAver(I)
        'Next

        ' поэтому сделаем по другому
        For I = 1 To ЧИСЛО_ТЕРМОПАР
            имяПояса = "Пояс" & I.ToString
            'накопление
            SummaСредняяПоТермопаре(I - 1) += arrПоясDictionary(имяПояса)(ИндексОтсечекДляПоля)
            SummaТсредняя_газа_на_входе(I - 1) += ПараметрыПоляНакопленные(CalculatedParameters.Т_СРЕДНЯЯ_ГАЗА_НА_ВХОДЕ)(ИндексОтсечекДляПоля)
            'SummaТг_расчет(I - 1) += ПараметрыПоляНакопленные(CalculatedParameter.Т_Г_РАСЧЕТ)(ИндексОтсечекДляПоля)
            SummaT_интегр(I - 1) += ПараметрыПоляНакопленные(CalculatedParameters.conT_ИНТЕГР)(ИндексОтсечекДляПоля)

            ' найти среднее, счетчик сложенных равен ИндексОтсечекДляПоля + 1
            СредняяПоТермопаре = SummaСредняяПоТермопаре(I - 1) / (ИндексОтсечекДляПоля + 1)
            СредняяТсредняя_газа_на_входе = SummaТсредняя_газа_на_входе(I - 1) / (ИндексОтсечекДляПоля + 1)
            'СредняяТг_расчет = SummaТг_расчет(I - 1) / (ИндексОтсечекДляПоля + 1)
            СредняяT_интегр = SummaT_интегр(I - 1) / (ИндексОтсечекДляПоля + 1)

            'If blnИзмерениеПоТемпературам Then
            имяПояса = "T340_" & I.ToString
            'Else
            'ИмяПояса = "dPкс-" & I.ToString
            'End If
            temperature = ПараметрыПоляНакопленные(имяПояса)(ИндексОтсечекДляПоля)

            If temperature < arrMinMaxПоясов(I).Min Then
                arrMinMaxПоясов(I).Min = temperature
                arrMinMaxПоясов(I).imin = ИндексОтсечекДляПоля
            End If

            If temperature > arrMinMaxПоясов(I).Max Then
                arrMinMaxПоясов(I).Max = temperature
                arrMinMaxПоясов(I).imax = ИндексОтсечекДляПоля
            End If

            arrMinMaxПоясов(I).dblСредняя = СредняяПоТермопаре 'Aver(I - 1)

            XyCursorMinDictionary("Min" & I.ToString).YPosition = arrMinMaxПоясов(I).Min
            XyCursorMaxDictionary("Max" & I.ToString).YPosition = arrMinMaxПоясов(I).Max
            XyCursorAverDictionary("Aver" & I.ToString).YPosition = arrMinMaxПоясов(I).dblСредняя

            ' найти эпюрную и окружную неравномерность в мерном участке
            ' Накопленные и осредненные от 0 до ИндексОтсечекДляПоля
            'XyCursorОкружнаяDictionary("Окружная" & I.ToString).YPosition = ПараметрВДиапазонеНакопленные(НеравномерностьFun(arrMinMaxПоясов(I).Max, СредняяТсредняя_газа_на_входе, СредняяТг_расчет))
            'arrЭпюрнаяНеравномерность(I - 1) = НеравномерностьFun(arrMinMaxПоясов(I).dblСредняя, СредняяТсредняя_газа_на_входе, СредняяТг_расчет)

            XyCursorОкружнаяDictionary("Окружная" & I.ToString).YPosition = ПараметрВДиапазонеНакопленные(НеравномерностьFun(arrMinMaxПоясов(I).Max, СредняяТсредняя_газа_на_входе, СредняяT_интегр))
            arrЭпюрнаяНеравномерность(I - 1) = НеравномерностьFun(arrMinMaxПоясов(I).dblСредняя, СредняяТсредняя_газа_на_входе, СредняяT_интегр)

            PlotTDictionary("PlotT" & I.ToString).PlotY(arrПоясDictionary("Пояс" & I.ToString))
        Next

        ' было - перенес в цикл рисовать график
        'PlotTDictionary("PlotT" & (I + 1).ToString).PlotYAppend(arrПояс1) '(ПараметрыПоляНакопленные(conПоложениеГребенки).ВсеЗамеры, arrПояс1)

        'PlotAverage.PlotXY(ПараметрыПоляНакопленные(conПоложениеГребенки).ВсеЗамеры, arrПоясAverege)
        PlotAverage.PlotY(arrПоясDictionary("Averege"))
        PlotЭпюрнаяНеравномерность.PlotXY(КоординатыТермопар, arrЭпюрнаяНеравномерность)

        XyCursorОтсечка.XPosition = ИндексОтсечекДляПоля 'ПараметрыПоляНакопленные(conПоложениеГребенки)(ИндексОтсечекДляПоля)  

        ПовернутьТурель(ИндексОтсечекДляПоля)
        ВремяСбора()
    End Sub

    'Private Sub ПреобразоватьМассив(ByRef Массив(,) As Single)
    '    'производится соотношение гребёнки В(1-5) к гребенки А(1-5)
    '    'из ненормального в нормальное
    '    Dim I, J As Short
    '    Dim vrem(14) As Double
    '    For J = 2 To 10 Step 2
    '        For I = 1 To 14
    '            vrem(I) = Массив(I, J)
    '        Next I
    '        For I = 15 To 28
    '            Массив(I - 14, J) = Массив(I, J)
    '        Next I
    '        For I = 15 To 28
    '            Массив(I, J) = vrem(I - 14)
    '        Next I
    '    Next J
    'End Sub

    Private Sub ПреобразоватьМассив(ByRef Массив() As Double)
        ' производится соотношение гребёнки В(1-5) к гребенки А(1-5)
        ' из ненормального в нормальное
        Dim I As Integer
        Dim середина As Integer = ЧИСЛО_ПРОМЕЖУТКОВ \ 2
        Dim vrem(середина) As Double

        For I = 0 To середина ' от 0 до 180
            vrem(I) = Массив(I)
        Next

        For I = середина + 1 To ЧИСЛО_ПРОМЕЖУТКОВ
            Массив((I - середина) - 1) = Массив(I) ' от 0 до 179
        Next

        For I = середина To ЧИСЛО_ПРОМЕЖУТКОВ
            Массив(I) = vrem(I - середина)
        Next
    End Sub

    Private Sub РасчетПоля(ByVal полеИзмеренно As Boolean)
        Dim I, J As Integer
        Dim max, min As Double
        Dim imin, imax As Integer
        Dim имяПояса As String
        Dim Ystart, Yend As Double ' интерполируемые значения на стенки
        Dim arrSurfaceMin(ЧИСЛО_ТЕРМОПАР + 1, ЧИСЛО_ПРОМЕЖУТКОВ) As Double
        Dim arrSurfaceMean(ЧИСЛО_ТЕРМОПАР + 1, ЧИСЛО_ПРОМЕЖУТКОВ) As Double
        Dim arrSurfaceMax(ЧИСЛО_ТЕРМОПАР + 1, ЧИСЛО_ПРОМЕЖУТКОВ) As Double

        ' преобразовать четные (Б) термопары
        ' найти новые проэкции на стенки
        If полеИзмеренно Then
            For I = 2 To ЧИСЛО_ТЕРМОПАР Step 2
                ПреобразоватьМассив(ПараметрыПоляНакопленные("T340_" & I.ToString).ВсеЗамеры)
            Next
        End If

        Dim arrStat(ЧИСЛО_ТЕРМОПАР - 1) As Double
        Dim накопленнаяВСечении As Double = 0
        Dim temperature As Double

        For angle As Integer = 0 To ЧИСЛО_ПРОМЕЖУТКОВ
            накопленнаяВСечении = 0
            J = 0

            For I = 1 To ЧИСЛО_ТЕРМОПАР
                имяПояса = "Пояс" & I.ToString
                temperature = ПараметрыПоляНакопленные("T340_" & I.ToString)(angle)
                arrПоясDictionary(имяПояса)(angle) = temperature
                arrПоле(I - 1, angle) = temperature
                arrSurfaceMean(I, angle) = temperature
                arrStat(I - 1) = temperature
                накопленнаяВСечении += temperature
                J += 1
            Next
            arrПоясDictionary("Averege")(angle) = накопленнаяВСечении / J

            НайтиЗначенияТемпературНаСтенкахInterpolate(КоординатыТермопар, arrStat, ШиринаМерногоУчастка, Ystart, Yend)
            'y(0) = Ystart
            'y(ЧислоТермопар + 1) = Yend

            gMainFomMdiParent.myClassCalculation.CalculatedParam.T_интегр = ИнтегрированиеРадиальнойЭпюрыНаПроизвольныхКоординатах(КоординатыТермопар, arrStat, ШиринаМерногоУчастка)
            ПараметрыПоляНакопленные(CalculatedParameters.conT_ИНТЕГР)(angle) = gMainFomMdiParent.myClassCalculation.CalculatedParam.T_интегр

            ' внимание Качество и Тг_расчет заново не рассчитываются

            ПараметрыПоляНакопленные(ПРОЭКЦИЯ_НА_СТЕНКУ1)(angle) = Ystart 'y(0)
            ПараметрыПоляНакопленные(ПРОЭКЦИЯ_НА_СТЕНКУ2)(angle) = Yend 'y(ЧислоТермопар + 1)
            arrSurfaceMean(0, angle) = Ystart
            arrSurfaceMean(ЧИСЛО_ТЕРМОПАР + 1, angle) = Yend

            Dim средняя As Double = arrПоясDictionary("Averege")(angle)
            ArrayOperation.MaxMin1D(arrStat, max, imax, min, imin) ' Statistics.Mean(arrStat)

            'ПараметрыПоляНакопленные(ОКРУЖНАЯ_НЕРАВНОМЕРНОСТЬ)(Градус) = НеравномерностьFun(max, ПараметрыПоляНакопленные(CalculatedParameter.Т_СРЕДНЯЯ_ГАЗА_НА_ВХОДЕ)(Градус), ПараметрыПоляНакопленные(CalculatedParameter.Т_Г_РАСЧЕТ)(Градус))
            'ПараметрыПоляНакопленные(ЭПЮРНАЯ_НЕРАВНОМЕРНОСТЬ)(Градус) = НеравномерностьFun(средняя, ПараметрыПоляНакопленные(CalculatedParameter.Т_СРЕДНЯЯ_ГАЗА_НА_ВХОДЕ)(Градус), ПараметрыПоляНакопленные(CalculatedParameter.Т_Г_РАСЧЕТ)(Градус))

            ПараметрыПоляНакопленные(ОКРУЖНАЯ_НЕРАВНОМЕРНОСТЬ)(angle) = НеравномерностьFun(max, ПараметрыПоляНакопленные(CalculatedParameters.Т_СРЕДНЯЯ_ГАЗА_НА_ВХОДЕ)(angle), ПараметрыПоляНакопленные(CalculatedParameters.conT_ИНТЕГР)(angle))
            ПараметрыПоляНакопленные(ЭПЮРНАЯ_НЕРАВНОМЕРНОСТЬ)(angle) = НеравномерностьFun(средняя, ПараметрыПоляНакопленные(CalculatedParameters.Т_СРЕДНЯЯ_ГАЗА_НА_ВХОДЕ)(angle), ПараметрыПоляНакопленные(CalculatedParameters.conT_ИНТЕГР)(angle))
            ПараметрыПоляНакопленные(ПОЯС_MAX)(angle) = max
        Next

        '--- здесь итоговый расчет по полю -----------------------------------
        '--- мин, макс, средняя по поясам ------------------------------------
        For I = 1 To ЧИСЛО_ТЕРМОПАР
            имяПояса = "Пояс" & I.ToString

            ArrayOperation.MaxMin1D(arrПоясDictionary(имяПояса), max, imax, min, imin)
            arrMinMaxПоясов(I).dblСредняя = Statistics.Mean(arrПоясDictionary(имяПояса))

            XyCursorMinDictionary("Min" & I.ToString).YPosition = min
            XyCursorMaxDictionary("Max" & I.ToString).YPosition = max
            XyCursorAverDictionary("Aver" & I.ToString).YPosition = arrMinMaxПоясов(I).dblСредняя

            arrMinMaxПоясов(I).Max = max
            arrMinMaxПоясов(I).imax = imax + 1
            arrMinMaxПоясов(I).Min = min
            arrMinMaxПоясов(I).imin = imin + 1
            arrТекущаяПоПоясам(I - 1) = arrMinMaxПоясов(I).dblСредняя
        Next

        ArrayOperation.MaxMin2D(arrПоле, Tgmax, NПоясаМакс, NКамерыМакс, Tgmin, NПоясаМин, NКамерыМин)
        ' преобразуем индекс в камеру =ЦЕЛОЕ((B2*28/360)+1)
        NКамерыМакс = CInt(Int(NКамерыМакс * ЧИСЛО_ГОРЕЛОК / 360 + 1))
        NКамерыМин = CInt(Int(NКамерыМин * ЧИСЛО_ГОРЕЛОК / 360 + 1))

        If NКамерыМакс > ЧИСЛО_ГОРЕЛОК Then NКамерыМакс = ЧИСЛО_ГОРЕЛОК
        If NКамерыМин > ЧИСЛО_ГОРЕЛОК Then NКамерыМин = ЧИСЛО_ГОРЕЛОК

        NПоясаМакс += 1
        NПоясаМин += 1
        TextT4min.Text = Round(Tgmin, ТОЧНОСТЬ).ToString
        TextNКамерыМин.Text = NКамерыМин.ToString
        TextNПоясаМин.Text = NПоясаМин.ToString

        TextT4max.Text = Round(Tgmax, ТОЧНОСТЬ).ToString
        TextNКамерыМакс.Text = NКамерыМакс.ToString
        TextNПоясаМакс.Text = NПоясаМакс.ToString

        '--- расчет интегральной температура газа ----------------------------
        ' вычисления интегральной по полю температур или давлений
        ' общие для видов испытаний
        ' T_интегрИтог = ИнтегрированиеРадиальнойЭпюрыНаПроизвольныхКоординатах(ПараметрыПоляНакопленные(conПоложениеГребенки).ВсеЗамеры, ПараметрыПоляНакопленные(conПоясИнтегрированное).ВсеЗамеры, ЧислоПромежутков)
        ' T_интегрИтог = ИнтегрированиеРадиальнойЭпюрыНаРавномерныхКоординатах(КоординатыГрадусов, ПараметрыПоляНакопленные(conT_интегр).ВсеЗамеры, ЧислоПромежутков)
        T_интегрResult = ИнтегрированиеРадиальнойЭпюры(arrКоординатыГрадусов, ПараметрыПоляНакопленные(CalculatedParameters.conT_ИНТЕГР).ВсеЗамеры)

        'LamdaИтог = LamdaFun(ПараметрыПоляНакопленные(conПоясТс).Mean, ПараметрыПоляНакопленные(conТсредняя_газа_на_входе).Mean, ПараметрыПоляНакопленные(conПоясGb).Mean, Fdif, ПараметрыПоляНакопленные(conПоясРст_абс_ср).Mean, ПараметрыПоляНакопленные(conПоясРв_вх_абс_полн_ср).Mean)
        'AlphaИтог = ПараметрыПоляНакопленные(conПоясGв_сум).Mean / (14.94 * ПараметрыПоляНакопленные(conПоясGt_кс_ср).Mean)
        'Тг_расчетИтог = clAir.РасчётнаяТемпература(ПараметрыПоляНакопленные(conПоясТс).Mean + conКельвин, ПараметрыПоляНакопленные(conПоясGt_кс_ср).Mean + ПараметрыПоляНакопленные(conПоясGt_кп).Mean, ПараметрыПоляНакопленные(conПоясGb).Mean, AlphaИтог)

        lamdaResult = ПараметрыПоляНакопленные(CalculatedParameters.conЛЯМБДА).Mean
        alphaResult = ПараметрыПоляНакопленные(CalculatedParameters.АЛЬФА_КАМЕРЫ).Mean
        Тг_расчетResult = ПараметрыПоляНакопленные(CalculatedParameters.Т_Г_РАСЧЕТ).Mean
        качествоResult = КачествоFun(T_интегрResult, ПараметрыПоляНакопленные(CalculatedParameters.Т_СРЕДНЯЯ_ГАЗА_НА_ВХОДЕ).Mean, Тг_расчетResult)

        ' для РисоватьПолеПоСечению найти на стенках
        НайтиЗначенияТемпературНаСтенкахInterpolate(КоординатыТермопар, arrТекущаяПоПоясам, ШиринаМерногоУчастка, Ystart, Yend)
        y(0) = Ystart
        y(ЧИСЛО_ТЕРМОПАР + 1) = Yend
        РисоватьПолеПоСечению() ' т.е. нарисовать не в каком-то промежуточном замере, а как среднеее по всему полю за 17 сечений

        'txtПотериПолнДавленияИтегрИтог.Text = "0"
        TextКачествоИтог.Text = качествоResult.ToString("#0.0###")
        TextТсредняя.Text = arrТекущаяПоПоясам.Average().ToString("###0.0#")
        TextТинтегральная.Text = T_интегрResult.ToString("###0.0#")
        TextLamdaИтог.Text = lamdaResult.ToString("#0.0###")
        TextAlphaИтог.Text = alphaResult.ToString("#0.0###")
        TextТг_расчетИтог.Text = Тг_расчетResult.ToString("###0.0#")

        If gMainFomMdiParent.var3D.TabControlGrph.Enabled = False Then gMainFomMdiParent.var3D.TabControlGrph.Enabled = True

        ' ввиду того, что пока не реализовано измерение минимальных и максимальных (происходит накапливание значения параметра и его осреднение),
        ' на данный момент произвести ссылочное копирование в массив минимальных и максимальных
        arrSurfaceMin = arrSurfaceMean
        arrSurfaceMax = arrSurfaceMean
        gMainFomMdiParent.var3D.PlotSurface(ЧИСЛО_ТЕРМОПАР, ЧИСЛО_ПРОМЕЖУТКОВ, ШиринаМерногоУчастка, x, arrSurfaceMin, arrSurfaceMean, arrSurfaceMax, Tgmin, Tgmax)
        gMainFomMdiParent.varПротокол.PopulateDataGridViewReport()

        gMainFomMdiParent.gControllerTemperature.PreparePlotSurface(arrSurfaceMean, x)

        ' после расчета поля надо дополнительную команду для продолжения обновления индикаторов
        ButtonContinue.Visible = True
        ButtonProtocol.Visible = True
        gРисоватьГрафикСечений = False
    End Sub

    ''' <summary>
    ''' Индикатор выведет самое первое неправильное значение.
    ''' Остальные неправильные не будут выведены для предотвращения мерцания.
    ''' </summary>
    Public Sub ОновитьИндикаторы()
        Dim isShowError As Boolean = True

        With gMainFomMdiParent.myClassCalculation
            TextPosition.Text = gMainFomMdiParent.varEncoder.AnglePosition.ToString("F")
            ПроверкаОтсечки(gMainFomMdiParent.varEncoder.Отсечка, gMainFomMdiParent.varEncoder.AnglePosition)

            For Each par As Parameter In .CalculatedParam
                ПроверкаПараметрВДиапазоне(par.Name, isShowError)
            Next

            ПроверкаПараметрВДиапазоне(InputParameters.Р310_ПОЛНОЕ_ВОЗДУХА_НА_ВХОДЕ_КC,
                                   .InputParam.Р310полное_воздуха_на_входе_КС,
                                   isShowError,
                                   CType(TankСтатическоеДавлениеМерномСопле, INumericPointer),
                                   NumericEditСтатическоеДавлениеМерномСопле)
        End With

        ' ошибок не было, значить погасить индикатор, если он зажегся ло этого
        TextError.Visible = Not isShowError
    End Sub

    Private Sub ПроверкаПараметрВДиапазоне(ByVal name As String, ByRef isShowError As Boolean)
        With gMainFomMdiParent.myClassCalculation
            ПроверкаПараметрВДиапазоне(name,
                                       .CalculatedParam(name),
                                       isShowError,
                                       .CalculatedParam.CalcDictionary(name).ControlNumericPointer,
                                       .CalculatedParam.CalcDictionary(name).ControlNumericEdit)
        End With
    End Sub

    Private Sub ПроверкаПараметрВДиапазоне(ByVal name As String,
                                           ByVal inCurrentValue As Double,
                                           ByRef isShowError As Boolean,
                                           ByRef inINumericPointer As INumericPointer,
                                           ByRef inNumericEdit As NumericEdit)
        ' NaN == NaN: False применять нельзя
        ' Double.NaN.Equals(Double.NaN) - можно NaN.Equals(NaN): True
        ' Double.IsNaN(Double.NaN))- можно IsNaN: True

        If Double.IsNaN(inCurrentValue) OrElse Double.IsNegativeInfinity(inCurrentValue) OrElse Double.IsPositiveInfinity(inCurrentValue) Then
            'Throw New ArgumentOutOfRangeException(name & " вне диапазона")
            If isShowError Then
                ShowError($"Ошибка расчета: параметр <{name}> не вычислен!")
                isShowError = False
            End If

            inCurrentValue = 0.0
        End If

        If inINumericPointer IsNot Nothing Then inINumericPointer.Value = inCurrentValue
        If inNumericEdit IsNot Nothing Then inNumericEdit.Value = inCurrentValue
    End Sub

    Public Sub РисоватьПолеПоСечению()
        Dim I As Integer ' универсальная для температур и для давлений
        Dim xInit, xFinal As Double

        ' Значения Температур На Стенках уже вычисленны в ClassCalculation или РасчетПоля
        For I = 1 To ЧИСЛО_ТЕРМОПАР
            y(I) = arrТекущаяПоПоясам(I - 1)
        Next

        Y2 = CurveFit.SplineInterpolant(x, y, 0, 0)

        For I = 0 To CInt(ШиринаМерногоУчастка * 10)
            y3(I) = CurveFit.SplineInterpolation(x, y, Y2, I / 10)
        Next

        Dim dtX As Double = 1 / 10
        xInit = y3(0) '0
        'xFinal = ШиринаМерногоУчастка
        xFinal = y3(UBound(y3)) ' ЧислоПромежутков

        arrТемперГаза = SignalProcessing.Integrate(y3, dtX, xInit, xFinal)
        ИнтегральнаяПоПолю = arrТемперГаза(UBound(arrТемперГаза)) / (ШиринаМерногоУчастка + dtX) ' y3.Length-для целых чисел ' по идеи но округляет (ШиринаМерногоУчастка + 1

        ' вывести на график по сечению - гладкая линия
        'PlotТемператураПоСечению.PlotY(y3, 0, 1)
        PlotТемператураПоСечению.PlotXY(xGraf, y3)

        ' линия с точками подходит если равномерное расположение точек
        PlotТемператураПоПоясам.PlotXY(КоординатыТермопар, arrТекущаяПоПоясам)

        РисоватьГрафики()

        XyCursorСредняяПоля.YPosition = ИнтегральнаяПоПолю
        LabelTMeanIntegral.Text = Round(ИнтегральнаяПоПолю, 1).ToString
        ' обновление графика с допуском
        SweepChart()

        CheckAlarmLimit()
    End Sub

    ''' <summary>
    ''' Проверка на настроенные аварийные допуски параметров в таблице измеренных параметров.
    ''' Вывод собщения в статусной строке. Аварийный индикатор не нужен.
    ''' </summary>
    Private Sub CheckAlarmLimit()
        With gMainFomMdiParent.Manager
            Dim valueT As Double
            'Dim ИмяПояса As String
            'For I As Integer = 1 To ЧислоТермопар
            '    ИмяПояса = "T340_" & I.ToString
            '    Dim rowИзмеренныйПараметр As BaseFormDataSet.ИзмеренныеПараметрыRow = .ИзмеренныеПараметры.FindByИмяПараметра(ИмяПояса)
            '    ValueT = rowИзмеренныйПараметр.ЗначениеВСИ
            '    If ValueT < rowИзмеренныйПараметр.ДопускМинимум Or ValueT > rowИзмеренныйПараметр.ДопускМаксимум Then
            '        'вывести сообщение об обрыве
            '        sbStatusBar.Items.Item(1).Text = "Обрыв термапары " & arrНазваниеПоясов(I)
            '    End If
            'Next

            StatusBar.Items(conStatusLabelMessage).Text = "Ready"

            For Each rowИзмеренныйПараметр As BaseFormDataSet.ИзмеренныеПараметрыRow In .MeasurementDataTable.Rows
                valueT = rowИзмеренныйПараметр.ЗначениеВСИ
                If valueT < rowИзмеренныйПараметр.ДопускМинимум OrElse valueT > rowИзмеренныйПараметр.ДопускМаксимум Then
                    ' вывести сообщение об обрыве
                    StatusBar.Items(conStatusLabelMessage).Text = $"Значение параметра {rowИзмеренныйПараметр.ИмяПараметра} = {Format(valueT, "##,##0.00")} вне допуска ({Format(rowИзмеренныйПараметр.ДопускМинимум, "F")} : {Format(rowИзмеренныйПараметр.ДопускМаксимум, "F")})!"
                End If
            Next

            For Each rowРасчетныйПараметр As BaseFormDataSet.РасчетныеПараметрыRow In .CalculatedDataTable.Rows
                valueT = rowРасчетныйПараметр.ВычисленноеЗначениеВСИ
                If valueT < rowРасчетныйПараметр.ДопускМинимум OrElse valueT > rowРасчетныйПараметр.ДопускМаксимум Then
                    ' вывести сообщение об обрыве
                    StatusBar.Items(conStatusLabelMessage).Text = $"Значение параметра {rowРасчетныйПараметр.ИмяПараметра} = {Format(valueT, "##,##0.00")} вне допуска ({Format(rowРасчетныйПараметр.ДопускМинимум, "F")} : {Format(rowРасчетныйПараметр.ДопускМаксимум, "F")})!"
                End If
            Next
        End With
    End Sub

    Private Sub РисоватьГрафики()
        ' рисовать график т.к. график по равномерной оси то использовать PlotY
        'PlotT1.PlotXY(ПараметрыПоляНакопленные(conПоложениеГребенки).ВсеЗамеры, arrПояс1)

        For I As Integer = 1 To ЧИСЛО_ТЕРМОПАР
            PlotTDictionary("PlotT" & I.ToString).PlotY(arrПоясDictionary("Пояс" & I.ToString))
        Next

        'PlotAverage.PlotXY(ПараметрыПоляНакопленные(conПоложениеГребенки).ВсеЗамеры, arrПоясAverege)
        PlotAverage.PlotY(arrПоясDictionary("Averege"))

        'XyCursorОтсечка.XPosition = ПараметрыПоляНакопленные(conПоложениеГребенки)(ЧислоПромежутков)  'ИндексОтсечекДляПоля
        XyCursorОтсечка.XPosition = ИндексОтсечекДляПоля
    End Sub

    Private Sub СоответствиеГрадусОтсечка()
        For I As Integer = 1 To ЧИСЛО_ГОРЕЛОК
            Xgor((ЧИСЛО_ГОРЕЛОК + 1) - I) = 360 * I / ЧИСЛО_ГОРЕЛОК
        Next
    End Sub

    Private Sub SweepChart()
        xTemp = (lastx + 1) Mod ARRAY_SIZE ' Обнуление счетчика при достижении максимального значения диапазона
        Dim index As Integer = CInt(xTemp)
        dataГрафик(0, index) = ГрафикДопуска(gMainFomMdiParent.myClassCalculation.InputParam.Р310полное_воздуха_на_входе_КС, conPkL, conPkH) ' давление Р310
        dataГрафик(1, index) = ГрафикДопуска(gMainFomMdiParent.myClassCalculation.CalculatedParam.Gотбора_относительный, conGotL, conGotH) ' Gотбора
        dataГрафик(2, index) = ГрафикДопуска(gMainFomMdiParent.myClassCalculation.CalculatedParam.АльфаКамеры, conAksL, conAksH) ' альфа камеры
        dataГрафик(3, index) = ГрафикДопуска(gMainFomMdiParent.myClassCalculation.CalculatedParam.Лямбда, conLksL, conLksH) ' Лямбда (приведенная скорость)
        dataГрафик(4, index) = ГрафикДопуска(gMainFomMdiParent.myClassCalculation.CalculatedParam.ТсредняяГазаНаВходе, conT309L, conT309H) ' Т камеры
        XyCursorДопуск.XPosition = xTemp
        WaveformGraphLimitParameters.PlotYMultiple(dataГрафик)
        lastx = xTemp
    End Sub

    Private Function ГрафикДопуска(ByRef inX As Double, ByRef inX1 As Double, ByRef inX2 As Double) As Double
        Return 90 + 20 * (inX - inX1) / (inX2 - inX1)
    End Function

    Public Sub ПересчетПоляПослеКоррекции()
        'Dim I As Integer
        'For I = 0 To ЧислоПромежутков
        '    arrПоясAverege(I) = (arrПояс1(I) + arrПояс2(I) + arrПояс3(I) + arrПояс4(I) + arrПояс5(I)) / 5
        '    arrПоле(0, I) = arrПояс1(I)
        '    arrПоле(1, I) = arrПояс2(I)
        '    arrПоле(2, I) = arrПояс3(I)
        '    arrПоле(3, I) = arrПояс4(I)
        '    arrПоле(4, I) = arrПояс5(I)
        'Next I
        РасчетПоля(False)
        РисоватьГрафики()
    End Sub

    Private Sub TimerИндикаторОтсечки_Tick(ByVal sender As Object, ByVal e As EventArgs) Handles TimerIndicatorShutoff.Tick
        TimerIndicatorShutoff.Enabled = False
        SetИндикаторОтсечки(False)
    End Sub

    Private Sub ПовернутьТурель(ByVal newValue As Double)
        GaugeTurret.Value = newValue
        GaugeTurret.CustomDivisions.Item(0).Value = newValue

        If newValue <= 180 Then
            GaugeTurret.CustomDivisions.Item(1).Value = newValue + 180
        Else
            GaugeTurret.CustomDivisions.Item(1).Value = newValue - 180
        End If

        'If intСчетчикГорелок <> 0 Then
        '    knobGorelka.Pointers.Item(1)._Value = Xgor(intСчетчикГорелок)
        '    If intСчетчикГорелок >= 15 Then
        '        knobGorelka.Pointers.Item(2)._Value = Xgor(intСчетчикГорелок - 14)
        '    Else
        '        knobGorelka.Pointers.Item(2)._Value = Xgor(intСчетчикГорелок + 14)
        '    End If
        'End If
    End Sub

    ''' <summary>
    ''' вычислить разницу хода турели
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub ВремяСбора()
        dtEnd = DateTime.Now
        ts = dtEnd.Subtract(dtStart).Duration
        DisplayTSProperties(ts)
    End Sub

    Private Sub DisplayTSProperties(ByVal ts As TimeSpan)
        ' использовать экземпляр свойства типа TimeSpan.
        ' Пример:
        '  TimeSpan.Days
        '  TimeSpan.Hours
        '  TimeSpan.Milliseconds
        '  TimeSpan.Minutes
        '  TimeSpan.Seconds
        '  TimeSpan.Ticks
        '  TimeSpan.TotalDays
        '  TimeSpan.TotalHours
        '  TimeSpan.TotalMilliseconds
        '  TimeSpan.TotalMinutes
        '  TimeSpan.TotalSeconds
        'Try
        'lblDays.Text = ts.Days.ToString
        'lblHours.Text = ts.Hours.ToString
        'lblMilliseconds.Text = ts.Milliseconds.ToString
        'lblMinutes.Text = ts.Minutes.ToString
        'lblSeconds.Text = ts.Seconds.ToString
        'lblTimeSpanTicks.Text = ts.Ticks.ToString
        'lblTotalDays.Text = ts.TotalDays.ToString
        'lblTotalHours.Text = ts.TotalHours.ToString
        'lblTotalMilliseconds.Text = ts.TotalMilliseconds.ToString
        'lblTotalMinutes.Text = ts.TotalMinutes.ToString
        'lblTotalSeconds.Text = ts.TotalSeconds.ToString

        strВремяХодаТурели = $"{ts.Minutes} мин. {ts.Seconds} сек."
        StatusLabelTime.Text = strВремяХодаТурели
        'Catch exp As Exception
        '    MessageBox.Show(exp.Message, Me.Text)
        'End Try
    End Sub

    Private Sub ЗаписьПоля()
        Dim cn As New OleDbConnection(BuildCnnStr(PROVIDER_JET, gPathFieldsChamber))
        Dim cmd As OleDbCommand = cn.CreateCommand
        Dim strSQL As String
        Dim odaDataAdapter As OleDbDataAdapter
        Dim dtDataTable As New DataTable
        Dim drDataRow As DataRow
        Dim cb As OleDbCommandBuilder

        Try
            cmd.CommandType = CommandType.Text
            strSQL = $"INSERT INTO Fields (НомерИзделия, Дата, ВремяНачалаСбора, ВремяХодаТурели) VALUES ({НомерИзделия},'{ДатаЗаписи}','{ВремяЗаписи}','{strВремяХодаТурели}')"
            '             "', " & tsbТемпературы.Checked & ")"
            tmpНомерИзделия = НомерИзделия
            cmd.CommandText = strSQL
            cn.Open()
            cmd.ExecuteNonQuery()
            strSQL = "SELECT @@IDENTITY"
            cmd.CommandText = strSQL
            KeyId = CInt(cmd.ExecuteScalar)

            ' Была страшная глюка если поле называется "Max" или содержит тире (T309-1) - ошибка вставки строки и не разберешь что за ошибка
            ' поля "Max" использовать нельзя
            strSQL = $"SELECT * FROM Поле WHERE (Поле.KeyId = {KeyId})"

            odaDataAdapter = New OleDbDataAdapter(strSQL, cn)
            odaDataAdapter.Fill(dtDataTable)

            Dim Keys As String() = ПараметрыПоляНакопленные.ВсеВеличиныЗамеровПараметров.Keys.ToArray
            ' не работает так как есть 3 поля вначале таблицы
            'For Each colTable As DataColumn In dtDataTable.Columns
            '    If Not Keys.Contains(colTable.ColumnName) Then
            '        'MessageBox.Show("Поле с имененм " & colTable.ColumnName & " не существует в таблице <Поле>" & Environment.NewLine & "базы данных " & strПутьFieldChamber, "Ошибка записи поля", MessageBoxButtons.OK, MessageBoxIcon.Error)
            '        'Exit Sub
            '        Throw New ApplicationException("Поле с имененм " & colTable.ColumnName & " не существует в таблице <Поле>" & Environment.NewLine & "базы данных " & strПутьFieldChamber)
            '    End If
            'Next

            For Each colTable As String In Keys
                If Not dtDataTable.Columns.Contains(colTable) Then
                    Throw New ApplicationException($"Поле с имененм {colTable} не существует в таблице <Поле>{Environment.NewLine}базы данных {gPathFieldsChamber}")
                End If
            Next

            For I As Integer = 0 To ЧИСЛО_ПРОМЕЖУТКОВ
                drDataRow = dtDataTable.NewRow
                drDataRow.BeginEdit()
                drDataRow("KeyId") = KeyId
                drDataRow("НомерОтсечки") = I 'I + 1
                For Each colName As String In Keys
                    'drDataRow(colName) = ПараметрыПоляНакопленные(colName)(I)
                    drDataRow(colName) = ПараметрВДиапазонеНакопленные(ПараметрыПоляНакопленные(colName)(I))
                Next
                drDataRow.EndEdit()
                dtDataTable.Rows.Add(drDataRow)
            Next

            cb = New OleDbCommandBuilder(odaDataAdapter)
            ' далее необязательно, только для проверки
            'odaDataAdapter.UpdateCommand = cb.GetUpdateCommand
            'odaDataAdapter.InsertCommand = cb.GetInsertCommand

            odaDataAdapter.Update(dtDataTable)
            'dtDataTable.AcceptChanges()
            cn.Close()
        Catch ex As Exception
            MessageBox.Show(ex.ToString, "Ошибка обновления базы FieldChamber", MessageBoxButtons.OK, MessageBoxIcon.Warning)
        Finally
            If (cn.State = ConnectionState.Open) Then
                cn.Close()
            End If
        End Try
    End Sub

    Private Sub ЗаписьПоляChamber()
        Dim I, J As Integer
        Dim strSQL As String
        Dim odaDataAdapter As OleDbDataAdapter
        Dim dtDataTable As New DataTable
        Dim drDataRow As DataRow
        Dim cb As OleDbCommandBuilder
        ' отметили снятое поле
        arrСнятыеКамеры(номерПоля) = True

        ' заполнить для камеры 99 изделия
        ' по 28 горелкам вычисляем среднее в секторе этой горелки
        Dim ГрадусВГорелке As Double = 360 / ЧИСЛО_ГОРЕЛОК
        Dim имяПояса As String
        Dim началоСектора, конецСектора, count As Integer
        Dim minСектора As Double = Double.MaxValue
        Dim maxСектора As Double = Double.MinValue
        Dim average, temperature As Double
        Dim memoКонецСектора As Integer

        For I = 1 To ЧИСЛО_ТЕРМОПАР
            имяПояса = "T340_" & I.ToString

            For J = 1 To ЧИСЛО_ГОРЕЛОК
                'началоСектора = CInt(Round((J - 1) * ГрадусВГорелке))
                If J = 1 Then
                    началоСектора = 0
                Else
                    началоСектора = memoКонецСектора + 1
                End If

                конецСектора = CInt(Round(J * ГрадусВГорелке))
                minСектора = Double.MaxValue
                maxСектора = Double.MinValue

                For S As Integer = началоСектора To конецСектора
                    temperature = ПараметрыПоляНакопленные(имяПояса)(S)

                    If temperature < minСектора Then
                        minСектора = temperature
                    End If

                    If temperature > maxСектора Then
                        maxСектора = temperature
                    End If

                    average += temperature
                    count += 1
                Next

                arrПолеМинимальные(J, I) = minСектора
                arrПолеМаксимальные(J, I) = maxСектора
                arrПолеСредние(J, I) = average / count

                average = 0
                count = 0
                memoКонецСектора = конецСектора
            Next
        Next

        Try
            ' запись в базу поля
            ' если нужно создать новое поле, делаем проверку на его отсутствие
            cn = New OleDbConnection(BuildCnnStr(PROVIDER_JET, gПутьКамера))
            strSQL = $"SELECT * FROM [Поле] WHERE ([НомерПоля]={номерПоля} AND [КодИзделия] = {Str(gКодИзделия)})"
            cn.Open()
            odaDataAdapter = New OleDbDataAdapter(strSQL, cn)
            odaDataAdapter.Fill(dtDataTable)

            If dtDataTable.Rows.Count > 0 Then
                dtDataTable.Rows(0).Delete()
            End If

            cb = New OleDbCommandBuilder(odaDataAdapter)
            odaDataAdapter.Update(dtDataTable)
            Thread.Sleep(500)

            ' запрос уже создан, он должен быть пустым и в него добавляется запись нового поля
            drDataRow = dtDataTable.NewRow
            drDataRow.BeginEdit()
            drDataRow("КодИзделия") = gКодИзделия
            drDataRow("НомерПоля") = номерПоля
            drDataRow("ВремяЗапуска") = dtStart ' ВремяНачалаСбора
            drDataRow("ВремяХодаТурели") = $"00:{dtEnd.Subtract(dtStart).Duration.Minutes}:{dtEnd.Subtract(dtStart).Duration.Seconds}" '"00:" & strВремяХодаТурели
            drDataRow.EndEdit()

            keyTablePhase = "КодПоля" ' для события
            dtDataTable.Rows.Add(drDataRow)
            ' включить событие занесения значения Автозаполнителя.
            AddHandler odaDataAdapter.RowUpdated, New OleDbRowUpdatedEventHandler(AddressOf OnRowUpdated)

            cb = New OleDbCommandBuilder(odaDataAdapter)
            odaDataAdapter.Update(dtDataTable)
            gКодПоля = newID

            ' создается запрос таблице "Горелка", он пустой и вставляются записи
            strSQL = $"SELECT * FROM Горелка WHERE ([КодПоля]={Str(gКодПоля)})"
            dtDataTable = New DataTable
            odaDataAdapter = New OleDbDataAdapter(strSQL, cn)
            odaDataAdapter.Fill(dtDataTable)

            For I = 1 To ЧИСЛО_ГОРЕЛОК
                drDataRow = dtDataTable.NewRow
                drDataRow.BeginEdit()
                drDataRow("КодПоля") = gКодПоля
                drDataRow("НомерГорелки") = I
                drDataRow("Температура") = "min"
                drDataRow("ПоясА1") = arrПолеМинимальные(I, 1)
                drDataRow("ПоясБ1") = arrПолеМинимальные(I, 2)
                drDataRow("ПоясА2") = arrПолеМинимальные(I, 3)
                drDataRow("ПоясБ2") = arrПолеМинимальные(I, 4)
                drDataRow("ПоясА3") = arrПолеМинимальные(I, 5)
                drDataRow("ПоясБ3") = arrПолеМинимальные(I, 6)
                drDataRow("ПоясА4") = arrПолеМинимальные(I, 7)
                drDataRow("ПоясБ4") = arrПолеМинимальные(I, 8)
                drDataRow("ПоясА5") = arrПолеМинимальные(I, 9)
                drDataRow("ПоясБ5") = arrПолеМинимальные(I, 10)
                drDataRow.EndEdit()
                dtDataTable.Rows.Add(drDataRow)

                drDataRow = dtDataTable.NewRow
                drDataRow.BeginEdit()
                drDataRow("КодПоля") = gКодПоля
                drDataRow("НомерГорелки") = I
                drDataRow("Температура") = "max"
                drDataRow("ПоясА1") = arrПолеМаксимальные(I, 1)
                drDataRow("ПоясБ1") = arrПолеМаксимальные(I, 2)
                drDataRow("ПоясА2") = arrПолеМаксимальные(I, 3)
                drDataRow("ПоясБ2") = arrПолеМаксимальные(I, 4)
                drDataRow("ПоясА3") = arrПолеМаксимальные(I, 5)
                drDataRow("ПоясБ3") = arrПолеМаксимальные(I, 6)
                drDataRow("ПоясА4") = arrПолеМаксимальные(I, 7)
                drDataRow("ПоясБ4") = arrПолеМаксимальные(I, 8)
                drDataRow("ПоясА5") = arrПолеМаксимальные(I, 9)
                drDataRow("ПоясБ5") = arrПолеМаксимальные(I, 10)
                drDataRow.EndEdit()
                dtDataTable.Rows.Add(drDataRow)

                drDataRow = dtDataTable.NewRow
                drDataRow.BeginEdit()
                drDataRow("КодПоля") = gКодПоля
                drDataRow("НомерГорелки") = I
                drDataRow("Температура") = "midl"
                drDataRow("ПоясА1") = arrПолеСредние(I, 1)
                drDataRow("ПоясБ1") = arrПолеСредние(I, 2)
                drDataRow("ПоясА2") = arrПолеСредние(I, 3)
                drDataRow("ПоясБ2") = arrПолеСредние(I, 4)
                drDataRow("ПоясА3") = arrПолеСредние(I, 5)
                drDataRow("ПоясБ3") = arrПолеСредние(I, 6)
                drDataRow("ПоясА4") = arrПолеСредние(I, 7)
                drDataRow("ПоясБ4") = arrПолеСредние(I, 8)
                drDataRow("ПоясА5") = arrПолеСредние(I, 9)
                drDataRow("ПоясБ5") = arrПолеСредние(I, 10)
                drDataRow.EndEdit()
                dtDataTable.Rows.Add(drDataRow)
            Next

            cb = New OleDbCommandBuilder(odaDataAdapter)
            odaDataAdapter.Update(dtDataTable)

            ' создается запрос таблице "Точка"
            strSQL = $"SELECT * FROM [Точка] WHERE ([КодПоля]={Str(gКодПоля)})"

            'strSQL = "SELECT Точка.КодПоля, Точка.НомерТочки, Точка.P1, Точка.dP1, Точка.Pотб, Точка.dPотб, Точка.P310, Точка.P311, Точка.T3, Точка.Tотб, Точка.TтоплКС, Точка.TтоплКП, Точка.Tбокса, Точка.GтКС, Точка.GтКП, Точка.[T309-1], Точка.[T309-2], Точка.[T309-3], Точка.[T309-4], Точка.[T309-5], Точка.[T309-6], Точка.[T309-7], Точка.[T309-8], Точка.[T309-9], Точка.[T309-10], Точка.[T309-11], Точка.[T309-12] " & _
            '"FROM(Точка) " & _
            '"WHERE ([КодПоля]=" & Str(lngКодПоля) & ")"

            dtDataTable = New DataTable
            odaDataAdapter = New OleDbDataAdapter(strSQL, cn)
            odaDataAdapter.Fill(dtDataTable)

            For I = 1 To КОЛИЧЕСТВО_КТ
                drDataRow = dtDataTable.NewRow
                drDataRow.BeginEdit()
                drDataRow("КодПоля") = gКодПоля
                drDataRow("НомерТочки") = I
                drDataRow("P1") = ВыдатьСреднееЗначениеПараметраДляКТ(I, InputParameters.ДАВЛЕНИЕ_ВОЗДУХА_НА_ВХОДЕ) '1 давление воздуха на входе
                drDataRow("dP1") = ВыдатьСреднееЗначениеПараметраДляКТ(I, InputParameters.ПЕРЕПАД_ДАВЛЕНИЯ_ВОЗДУХА_НА_ВХОДЕ) '2 перепад давления воздуха на входе
                drDataRow("T3") = ВыдатьСреднееЗначениеПараметраДляКТ(I, InputParameters.T3_МЕРН_УЧАСТКА) '7 T3

                drDataRow("Pотб") = ВыдатьСреднееЗначениеПараметраДляКТ(I, InputParameters.ДАВЛЕНИЕ_МАГИСТРАЛЕ_ОТБОРА) '3 давление в магистрале отбора
                drDataRow("dPотб") = ВыдатьСреднееЗначениеПараметраДляКТ(I, InputParameters.ПЕРЕПАД_ДАВЛЕНИЯ_ВОЗДУХА_ОТБОРА) '4 перепад давления воздуха отбора
                drDataRow("Tотб") = ВыдатьСреднееЗначениеПараметраДляКТ(I, InputParameters.Т_ОТБОРА) '8 Т отбора

                drDataRow("ДавлениеМагистралеОтбораВн") = ВыдатьСреднееЗначениеПараметраДляКТ(I, InputParameters.ДАВЛЕНИЕ_МАГИСТРАЛЕ_ОТБОРА_ВН) '3 давление в магистрале отбора
                drDataRow("ПерепадДавленияВоздухаОтбораВн") = ВыдатьСреднееЗначениеПараметраДляКТ(I, InputParameters.ПЕРЕПАД_ДАВЛЕНИЯ_ВОЗДУХА_ОТБОРА_ВН) '4 перепад давления воздуха отбора
                drDataRow("ТотбораВн") = ВыдатьСреднееЗначениеПараметраДляКТ(I, InputParameters.Т_ОТБОРА_ВН) '8 Т отбора

                drDataRow("P310") = ВыдатьСреднееЗначениеПараметраДляКТ(I, InputParameters.Р310_ПОЛНОЕ_ВОЗДУХА_НА_ВХОДЕ_КC) '5 P310
                drDataRow("P311") = ВыдатьСреднееЗначениеПараметраДляКТ(I, InputParameters.Р311_СТАТИЧЕСКОЕ_ВОЗДУХА_НА_ВХОДЕ_КС) '6 P311
                drDataRow("TтоплКС") = ВыдатьСреднееЗначениеПараметраДляКТ(I, InputParameters.Т_ТОПЛИВА_КС) '10 Т топлива КС
                drDataRow("TтоплКП") = ВыдатьСреднееЗначениеПараметраДляКТ(I, InputParameters.Т_ТОПЛИВА_КП) '11 Т топлива КП
                drDataRow("Tбокса") = ВыдатьСреднееЗначениеПараметраДляКТ(I, InputParameters.conTБОКСА) '12 Т кабины
                drDataRow("GтКС") = ВыдатьСреднееЗначениеПараметраДляКТ(I, InputParameters.Расход_Топлива_Камеры_Сгорания) '13 расход топлива камеры сгорания
                drDataRow("GтКП") = ВыдатьСреднееЗначениеПараметраДляКТ(I, InputParameters.РАСХОД_ТОПЛИВА_КАМЕРЫ_ПОДОГРЕВА) '14 расход топлива камеры подогрева
                drDataRow("T309_1") = ВыдатьСреднееЗначениеПараметраДляКТ(I, "T309_1")
                drDataRow("T309_2") = ВыдатьСреднееЗначениеПараметраДляКТ(I, "T309_2")
                drDataRow("T309_3") = ВыдатьСреднееЗначениеПараметраДляКТ(I, "T309_3")
                drDataRow("T309_4") = ВыдатьСреднееЗначениеПараметраДляКТ(I, "T309_4")
                drDataRow("T309_5") = ВыдатьСреднееЗначениеПараметраДляКТ(I, "T309_5")
                drDataRow("T309_6") = ВыдатьСреднееЗначениеПараметраДляКТ(I, "T309_6")
                drDataRow("T309_7") = ВыдатьСреднееЗначениеПараметраДляКТ(I, "T309_7")
                drDataRow("T309_8") = ВыдатьСреднееЗначениеПараметраДляКТ(I, "T309_8")
                drDataRow("T309_9") = ВыдатьСреднееЗначениеПараметраДляКТ(I, "T309_9")
                drDataRow("T309_10") = ВыдатьСреднееЗначениеПараметраДляКТ(I, "T309_10")
                drDataRow("T309_11") = ВыдатьСреднееЗначениеПараметраДляКТ(I, "T309_11")
                drDataRow("T309_12") = ВыдатьСреднееЗначениеПараметраДляКТ(I, "T309_12")

                drDataRow("ТвоздухаНаВходеКП") = ВыдатьСреднееЗначениеПараметраДляКТ(I, InputParameters.ТВОЗДУХА_НА_ВХОДЕ_КП)

                drDataRow.EndEdit()
                dtDataTable.Rows.Add(drDataRow)
            Next

            cb = New OleDbCommandBuilder(odaDataAdapter)
            odaDataAdapter.Update(dtDataTable)

            '--- только для ЭДС T340 сделать среднее за все замеры -----------
            Dim числоЗаписей As Integer
            ' если нужно создать новое ЭДС, делаем проверку на его отсутствие
            strSQL = "Select * FROM [КонтрольЭДС] WHERE [КодИзделия] = " & Str(gКодИзделия)
            ' создали запрос для ЭДС
            dtDataTable = New DataTable
            odaDataAdapter = New OleDbDataAdapter(strSQL, cn)
            odaDataAdapter.Fill(dtDataTable)

            числоЗаписей = dtDataTable.Rows.Count

            For I = 1 To КОЛИЧЕСТВО_КОНТРОЛЬ_ЭДС
                'ИмяПояса = "T340_" & I.ToString
                If числоЗаписей <> 0 Then 'надо обновлять
                    '.Edit()
                    drDataRow = dtDataTable.Rows(I - 1)
                Else 'надо добавлять
                    '.AddNew()
                    drDataRow = dtDataTable.NewRow
                End If

                drDataRow.BeginEdit()
                drDataRow("КодИзделия") = gКодИзделия
                drDataRow("НомерТочки") = I
                drDataRow("ТермопараА1") = ПараметрыПоляНакопленные("T340_1").Mean
                drDataRow("ТермопараБ1") = ПараметрыПоляНакопленные("T340_2").Mean
                drDataRow("ТермопараА2") = ПараметрыПоляНакопленные("T340_3").Mean
                drDataRow("ТермопараБ2") = ПараметрыПоляНакопленные("T340_4").Mean
                drDataRow("ТермопараА3") = ПараметрыПоляНакопленные("T340_5").Mean
                drDataRow("ТермопараБ3") = ПараметрыПоляНакопленные("T340_6").Mean
                drDataRow("ТермопараА4") = ПараметрыПоляНакопленные("T340_7").Mean
                drDataRow("ТермопараБ4") = ПараметрыПоляНакопленные("T340_8").Mean
                drDataRow("ТермопараА5") = ПараметрыПоляНакопленные("T340_9").Mean
                drDataRow("ТермопараБ5") = ПараметрыПоляНакопленные("T340_10").Mean
                drDataRow.EndEdit()

                If числоЗаписей = 0 Then dtDataTable.Rows.Add(drDataRow)
            Next

            cb = New OleDbCommandBuilder(odaDataAdapter)
            odaDataAdapter.Update(dtDataTable)
        Catch ex As Exception
            MessageBox.Show(ex.ToString, "Процедура: ЗаписьПоляChamber", MessageBoxButtons.OK, MessageBoxIcon.Warning)
        Finally
            If (cn.State = ConnectionState.Open) Then
                cn.Close()
            End If
        End Try

        'For I = 1 To 28
        '    For J = 1 To 10
        '        garrМинимальные(I, J) = arrПолеМинимальные(I, J)
        '        garrМаксимальные(I, J) = arrПолеМаксимальные(I, J)
        '        garrСредние(I, J) = arrПолеСредние(I, J)
        '    Next J
        'Next I
    End Sub

    Private Sub УдалитьКамеру()
        Dim strSQL As String
        Dim odaDataAdapter As OleDbDataAdapter
        Dim dtDataTable As New DataTable
        Dim cb As OleDbCommandBuilder

        Try
            ' запись в базу поля
            ' если нужно создать новое поле, делаем проверку на его отсутствие
            cn = New OleDbConnection(BuildCnnStr(PROVIDER_JET, gПутьКамера))
            ' удалить гребенку А, а за ней каскадно все записи
            strSQL = $"SELECT * FROM [ГребенкаА] WHERE ([КодГребенкиА]={Str(gКодГребенкиА)})"
            cn.Open()
            odaDataAdapter = New OleDbDataAdapter(strSQL, cn)
            odaDataAdapter.Fill(dtDataTable)

            If dtDataTable.Rows.Count > 0 Then
                dtDataTable.Rows(0).Delete()
            End If

            cb = New OleDbCommandBuilder(odaDataAdapter)
            odaDataAdapter.Update(dtDataTable)
            Thread.Sleep(500)

            ' удалить гребенку Б
            dtDataTable = New DataTable
            strSQL = $"SELECT * FROM [ГребенкаБ] WHERE ([КодГребенкиБ]={Str(gКодГребенкиБ)})"
            odaDataAdapter = New OleDbDataAdapter(strSQL, cn)
            odaDataAdapter.Fill(dtDataTable)

            If dtDataTable.Rows.Count > 0 Then
                dtDataTable.Rows(0).Delete()
            End If

            cb = New OleDbCommandBuilder(odaDataAdapter)
            odaDataAdapter.Update(dtDataTable)
            Thread.Sleep(500)
        Catch ex As Exception
            MessageBox.Show(ex.ToString, "Процедура: УдалитьКамеру", MessageBoxButtons.OK, MessageBoxIcon.Warning)
        Finally
            If (cn.State = ConnectionState.Open) Then
                cn.Close()
            End If
        End Try
    End Sub

    Private keyTablePhase As String
    Private newID As Integer = 0
    Private cn As OleDbConnection

    ''' <summary>
    ''' Процедура события обновления OnRowUpdated
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="args"></param>
    ''' <remarks></remarks>
    Private Sub OnRowUpdated(ByVal sender As Object, ByVal args As OleDbRowUpdatedEventArgs) 'Shared
        ' Вкдючает в себя переменную и команду возвращающий индентификатор значения
        ' из Access базы данных.
        'Dim newID As Integer = 0
        Dim idCMD As OleDbCommand = New OleDbCommand("SELECT @@IDENTITY", cn)

        If args.StatementType = StatementType.Insert Then
            ' возвратить значение идентификатора из колонки.
            newID = CInt(idCMD.ExecuteScalar())
            args.Row(keyTablePhase) = newID
        End If
    End Sub

    Private Function ВыдатьСреднееЗначениеПараметраДляКТ(ByVal номерКТ As Integer, ByVal имяПараметра As String) As Double
        ' НомерКТ - это номер сектора из 360 градусов их 5, а надо бы 6 тогда ровно через 60 градусов
        Const ЧИСЛО_СЕКТОРОВ As Integer = КОЛИЧЕСТВО_КТ
        Dim градусВСекторе As Double = 360.0 / ЧИСЛО_СЕКТОРОВ
        Dim count As Integer
        Dim average As Double
        Dim началоСектора As Integer = CInt(Round((номерКТ - 1) * градусВСекторе))
        Dim конецСектора As Integer = CInt(Round(номерКТ * градусВСекторе))

        For S As Integer = началоСектора To конецСектора
            average += ПараметрыПоляНакопленные(имяПараметра)(S)
            count += 1
        Next

        Return average / count
    End Function

    Private Sub MenuLoadFieldFromDBase_Click(ByVal sender As Object, ByVal e As EventArgs) Handles MenuLoadFieldFromDBase.Click
        ' открыть форму выбора записи поля и если KeyIdPole<>0 тогда
        ' обновить поля  strДатаЗаписи, strВремяЗаписи чтобы они сохранялись с прежней датой
        Dim mfrmПроводникПоБазеПолей As New FormGuideBaseChamber

        If mfrmПроводникПоБазеПолей.ShowDialog() = DialogResult.OK Then
            If mfrmПроводникПоБазеПолей.KeyId <> 0 Then
                ' считать выбранное поле
                KeyId = mfrmПроводникПоБазеПолей.KeyId

                Dim cn As New OleDbConnection(BuildCnnStr(PROVIDER_JET, gPathFieldsChamber))
                Dim rdr As OleDbDataReader
                Dim cmd As OleDbCommand = cn.CreateCommand
                Dim I As Integer
                Dim strSQL As String = $"SELECT * FROM Fields WHERE (KeyId={KeyId})"

                cmd.CommandType = CommandType.Text

                Try
                    cn.Open()
                    cmd.CommandText = strSQL
                    rdr = cmd.ExecuteReader ' Создание reader

                    If rdr.Read Then
                        ' потребуются для печати
                        tmpНомерИзделия = CInt(rdr("НомерИзделия"))
                        ДатаЗаписи = CStr(rdr("Дата"))
                        ВремяЗаписи = CStr(rdr("ВремяНачалаСбора"))
                        strВремяХодаТурели = rdr("ВремяХодаТурели").ToString
                        'tsbТемпературы.Checked = CBool(rdr("ИзмерениеПоТемпературам"))
                    End If
                    rdr.Close()

                    strSQL = $"SELECT * FROM Поле WHERE (KeyId={KeyId}) ORDER BY НомерОтсечки"
                    cmd.CommandText = strSQL
                    rdr = cmd.ExecuteReader ' Создание recordset

                    Dim keys As String() = ПараметрыПоляНакопленные.ВсеВеличиныЗамеровПараметров.Keys.ToArray

                    ' затем добавим по порядку
                    Do While rdr.Read
                        For Each colName As String In keys
                            ПараметрыПоляНакопленные(colName)(I) = CDbl(rdr(colName))
                        Next
                        I += 1
                    Loop
                    rdr.Close()
                    cn.Close()

                    ' ВключитьВыключитьКурсоры(True)
                    ПересчетПоляПослеКоррекции()
                    'mnuСохранитьИзмененияПоляВБазе.Enabled = True
                Catch ex As Exception
                    MessageBox.Show(ex.ToString, "Ошибка открытия базы FieldChamber", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Finally
                    If (cn.State = ConnectionState.Open) Then
                        cn.Close()
                    End If
                End Try
            End If
            'Else
        End If
    End Sub

#Region "Печать графиков"
    Private Sub PrintPageEntireGraph(ByVal sender As Object, ByVal e As PrintPageEventArgs)
        Dim g As Graphics = e.Graphics
        Dim bounds As Rectangle = e.MarginBounds

        Select Case ГрафикДляПечати
            Case GraphForPrint.ПоПоясам
                GraphTrendZone.Draw(New ComponentDrawArgs(g, bounds))
            Case GraphForPrint.ПоСечению
                GraphProfile.Draw(New ComponentDrawArgs(g, bounds))
            Case GraphForPrint.ОтклоненияПоПоясам
                GraphDeviationZone.Draw(New ComponentDrawArgs(g, bounds))
                'Case enuГрафикДляПечатати.T4
                '    GraphT4.Draw(New ComponentDrawArgs(g, bounds))
        End Select
    End Sub

    'Private Sub OnPrintPreviewClick()
    '    Dim document As PrintDocument = New PrintDocument

    '    Try
    '        'document = New PrintDocument
    '        If document.PrinterSettings.IsValid Then
    '            AddHandler document.PrintPage, Me.GetPrintPageEventHandler()
    '            printPreview.Document = document
    '            printPreview.ShowDialog(Me)
    '        Else
    '            MessageBox.Show(Me, New InvalidPrinterException(document.PrinterSettings).Message, Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Error)
    '        End If
    '    Finally
    '        document.Dispose()
    '    End Try
    'End Sub

    Private Sub OnPrintClick()
        Dim document As PrintDocument = New PrintDocument

        Try
            'document = New PrintDocument
            If document.PrinterSettings.IsValid Then
                AddHandler document.PrintPage, GetPrintPageEventHandler()
                document.Print()
            Else
                MessageBox.Show(Me, New InvalidPrinterException(document.PrinterSettings).Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Error)
            End If
        Finally
            document.Dispose()
        End Try
    End Sub

    Private Function GetPrintPageEventHandler() As PrintPageEventHandler
        Dim handler As PrintPageEventHandler = New PrintPageEventHandler(AddressOf PrintPageEntireGraph)
        'If (optionGraph.Checked) Then
        '    handler = New PrintPageEventHandler(AddressOf Me.PrintPageEntireGraph)
        'ElseIf (optionStackedPlots.Checked) Then
        '    handler = New PrintPageEventHandler(AddressOf Me.PrintPageAllPlotsStacked)
        'ElseIf (optionSineWave.Checked) Then
        '    handler = New PrintPageEventHandler(AddressOf Me.PrintPageOnlySineWave)
        'ElseIf (optionTriangleWave.Checked) Then
        '    handler = New PrintPageEventHandler(AddressOf Me.PrintPageOnlyTriangleWave)
        'ElseIf (optionSquareWave.Checked) Then
        '    handler = New PrintPageEventHandler(AddressOf Me.PrintPageOnlySquareWave)
        'End If

        Return handler
    End Function

    ''' <summary>
    ''' Печать графика по отклонениям
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub MenuPrintDeviationZone_Click(ByVal sender As Object, ByVal e As EventArgs) Handles MenuPrintDeviationZone.Click
        ГрафикДляПечати = GraphForPrint.ОтклоненияПоПоясам
        ConvertColorForGraph(GraphDeviationZone, True)
        OnPrintClick() 'OnPrintPreviewClick()
        ConvertColorForGraph(GraphDeviationZone, False)
    End Sub

    ''' <summary>
    ''' Печать графика по поясам
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub MenuPrintTemperatureZone_Click(ByVal sender As Object, ByVal e As EventArgs) Handles MenuPrintTemperatureZone.Click
        ГрафикДляПечати = GraphForPrint.ПоПоясам
        ConvertColorForGraph(GraphTrendZone, True)
        OnPrintClick() 'OnPrintPreviewClick()
        ConvertColorForGraph(GraphTrendZone, False)
    End Sub

    ''' <summary>
    ''' Печать Поле По Сечению
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub MenuPrintCrossSectional_Click(ByVal sender As Object, ByVal e As EventArgs) Handles MenuPrintCrossSectional.Click
        ГрафикДляПечати = GraphForPrint.ПоСечению
        ConvertColorForGraph(GraphProfile, True)
        OnPrintClick() 'OnPrintPreviewClick()
        ConvertColorForGraph(GraphProfile, False)
    End Sub

    ''' <summary>
    ''' Запись графика по отклонениям
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub MenuSaveDeviationZone_Click(ByVal sender As Object, ByVal e As EventArgs) Handles MenuSaveDeviationZone.Click
        'ГрафикДляПечати = enuГрафикДляПечатати.ОтклоненияПоПоясам
        ConvertColorForGraph(GraphDeviationZone, True)
        Dim GraphSave As GraphSave = New GraphSave(GraphDeviationZone)
        GraphSave.Save()
        ConvertColorForGraph(GraphDeviationZone, False)
    End Sub

    ''' <summary>
    ''' Запись Поле По Сечению
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub MenuSaveCrossSectional_Click(ByVal sender As Object, ByVal e As EventArgs) Handles MenuSaveCrossSectional.Click
        'ГрафикДляПечати = enuГрафикДляПечатати.ПоСечению
        ConvertColorForGraph(GraphProfile, True)
        Dim GraphSave As GraphSave = New GraphSave(GraphProfile)
        GraphSave.Save()
        ConvertColorForGraph(GraphProfile, False)
    End Sub

    ''' <summary>
    ''' Запись графика по поясам
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub MenuSaveTemperatureZone_Click(ByVal sender As Object, ByVal e As EventArgs) Handles MenuSaveTemperatureZone.Click
        'ГрафикДляПечати = enuГрафикДляПечатати.ПоПоясам
        ConvertColorForGraph(GraphTrendZone, True)
        Dim GraphSave As GraphSave = New GraphSave(GraphTrendZone)
        GraphSave.Save()
        ConvertColorForGraph(GraphTrendZone, False)
    End Sub

    Private Sub ConvertColorForGraph(ByRef NIGraph As ScatterGraph, ByVal вперед As Boolean)
        If вперед Then
            NIGraph.BackColor = Color.White
            NIGraph.Border = Border.None
            NIGraph.PlotAreaColor = Color.White
        Else
            NIGraph.BackColor = Color.Transparent
            NIGraph.Border = Border.ThickFrame3D
            NIGraph.PlotAreaColor = Color.Black
        End If
    End Sub

    Private Sub ConvertColorForGraph(ByRef NIGraph As WaveformGraph, ByVal вперед As Boolean)
        If вперед Then
            NIGraph.BackColor = Color.White
            NIGraph.Border = Border.None
            NIGraph.PlotAreaColor = Color.White
        Else
            NIGraph.BackColor = Color.Transparent
            NIGraph.Border = Border.ThickFrame3D
            NIGraph.PlotAreaColor = Color.Black
        End If
    End Sub
#End Region

    ''' <summary>
    ''' Установить Номер Поля
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub MenuSetNumberField_Click(ByVal sender As Object, ByVal e As EventArgs) Handles MenuSetNumberField.Click
        Dim strНомерПоля As String
        Dim I, номерПоляВременный As Integer

        Do
            Do
                strНомерПоля = InputBox("Введите номер поля (1-4)?", "Ввод номера поля")
                If strНомерПоля = "" Then Exit Sub
            Loop Until strНомерПоля.Length > 0
        Loop Until strНомерПоля = "1" OrElse strНомерПоля = "2" OrElse strНомерПоля = "3" OrElse strНомерПоля = "4"

        ' проверка снятых камер
        номерПоляВременный = Integer.Parse(strНомерПоля)

        For I = 1 To 4
            If arrСнятыеКамеры(I) AndAlso I = номерПоляВременный Then
                If MessageBox.Show($"Поле с таким номером уже записано!{Environment.NewLine}Сделать новое поле с таким номером?",
                                   "Ввод номера поля",
                                   MessageBoxButtons.YesNo,
                                   MessageBoxIcon.Exclamation) = DialogResult.No Then Exit Sub
            End If
        Next

        номерПоля = CInt(strНомерПоля)
        StatusBar.Items.Item("ПанельНомерПоля").Text = "Поле " & номерПоля
    End Sub

    Private oldValueStrobe As Boolean ' отсечка
    Private isRunCounter As Boolean

    Public Sub ПроверкаОтсечки(newValueStrobe As Boolean, anglePosition As Double)
        ' только если нажата кнопка
        If isRunCounter Then
            If oldValueStrobe <> newValueStrobe Then
                oldValueStrobe = newValueStrobe
                ' новое значение, надо анализировать
                If newValueStrobe Then
                    ' ступенька отсечки
#If EmulatorT340 = True Then
                    If ИндексОтсечекДляПоля = 0 AndAlso gНакопитьДляПоля = False Then
                        gНакопитьДляПоля = True ' включить сбор для того, чтобы начать накапливание для нулевого градуса
                        Exit Sub
                    End If

                    ОбработатьНовуюОтсечку(ИндексОтсечекДляПоля)
#Else
                    ОбработатьНовуюОтсечку(anglePosition)
#End If
                Else
                    ' условие if оставить
                    ' сброс отсечки
                End If
            End If
        End If
    End Sub

    ''' <summary>
    ''' Для отладки в событии таймера иммитируем поступление отсечек
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub TimerCount_Tick(sender As Object, e As EventArgs) Handles TimerCount.Tick
        gMainFomMdiParent.varEncoder.Отсечка = Not gMainFomMdiParent.varEncoder.Отсечка
    End Sub

    Private Sub ОбработатьНовуюОтсечку(inAnglePosition As Double)
        ' перемещение только вперед
        ' регистрация события включается и выключается по кнопке пуск
        If inAnglePosition >= -1.0 AndAlso gНакопитьДляПоля = False Then
            gНакопитьДляПоля = True ' включить сбор для того, чтобы начать накапливание для нулевого градуса
        End If

        ' пропуск угла упреждения турели и значение вне диапазона
        If inAnglePosition < 0 OrElse inAnglePosition >= ЧИСЛО_ПРОМЕЖУТКОВ + 1 Then Exit Sub

        ЗанестиСредниеЗначенияОдногоГрадусаВПараметрыПоляНакопленные()

        ' Test
#If EmulatorT340 = True Then
        ОтладкаПоля()
#End If

        ОбновитьГрафикиДляТекущейОтсечки() ' обновление графиков, накопление поля и параметров в подсчет для поля
        TextRemainToEndMotionringMount.Text = (ЧИСЛО_ПРОМЕЖУТКОВ - ИндексОтсечекДляПоля).ToString

        If ИндексОтсечекДляПоля >= ЧИСЛО_ПРОМЕЖУТКОВ Then
            ' конец замера поля
            Протокол() ' там осреднение накопленных в переменных деленных на число отсечек
            ButtonRunMeasurement.Checked = False
            ButtonRunMeasurement.Enabled = False
        Else
            ИндексОтсечекДляПоля = Convert.ToInt32(Math.Truncate(inAnglePosition)) + 1 ' следующая отсечка
        End If
    End Sub

    ''' <summary>
    ''' Накопленные значение собранные с промежутке между отсечками через 1 градус
    ''' осредняются и заносятся в таблицу
    ''' </summary>
    Private Sub ЗанестиСредниеЗначенияОдногоГрадусаВПараметрыПоляНакопленные()
        ' ОтметкаОдногоГрадуса
        For Each rowИзмеренныйПараметр As BaseFormDataSet.ИзмеренныеПараметрыRow In gMainFomMdiParent.Manager.MeasurementDataTable.Rows
            If СчетчикНакоплений = 0 Then
                ПараметрыПоляНакопленные(rowИзмеренныйПараметр.ИмяПараметра)(ИндексОтсечекДляПоля) = 0
            Else
                ПараметрыПоляНакопленные(rowИзмеренныйПараметр.ИмяПараметра)(ИндексОтсечекДляПоля) = rowИзмеренныйПараметр.НакопленноеЗначение / СчетчикНакоплений
            End If
            rowИзмеренныйПараметр.НакопленноеЗначение = 0
        Next

        For Each rowРасчетныйПараметр As BaseFormDataSet.РасчетныеПараметрыRow In gMainFomMdiParent.Manager.CalculatedDataTable.Rows
            If СчетчикНакоплений = 0 Then
                ПараметрыПоляНакопленные(rowРасчетныйПараметр.ИмяПараметра)(ИндексОтсечекДляПоля) = 0
            Else
                ПараметрыПоляНакопленные(rowРасчетныйПараметр.ИмяПараметра)(ИндексОтсечекДляПоля) = rowРасчетныйПараметр.НакопленноеЗначение / СчетчикНакоплений
            End If
            rowРасчетныйПараметр.НакопленноеЗначение = 0
        Next

        СчетчикНакоплений = 0
    End Sub

    Private Sub ВключитьВыключитьСчетчик(ByVal включить As Boolean)
        oldValueStrobe = False
        isRunCounter = включить

#If EmulatorT340 = True Then
        If включить Then
            TimerCount.Enabled = True
        Else
            TimerCount.Enabled = False
        End If
#End If
    End Sub

    Public arrDataTemperatureTest As Double(,)
    Private Sub ОтладкаПоля()
        For I As Integer = 1 To ЧИСЛО_ТЕРМОПАР
            ПараметрыПоляНакопленные("T340_" & I.ToString)(ИндексОтсечекДляПоля) = arrDataTemperatureTest(ИндексОтсечекДляПоля, I - 1) '1506.3 
        Next
    End Sub

    Private Function GenerateIntensityData(xArraySize As Integer, yArraySize As Integer) As Double(,)
        ' Даннык по окружности.
        Dim data As Double(,) = New Double(xArraySize - 1, yArraySize - 1) {}

        ' maxDistance это расстояние создающее максимальный угол (здесь maxPhaseAngle = 5).
        Dim maxDistance As Double = If(xArraySize <= yArraySize, xArraySize, yArraySize)
        Const maxPhaseAngle As Double = 4.8395061728395063
        '5
        ' амплитуда определяется из максимальных данных в массиве
        Const maxAmplitude As Double = 300 'ColorScale.Range.Interval / 2
        Const baseValue As Double = 1300 ' ColorScale.Range.Minimum + ColorScale.Range.Interval / 2

        For i As Integer = 0 To xArraySize - 1
            For j As Integer = 0 To yArraySize - 1
                ' используя уравнение окружности, определить расстояние от (i,j) до (0,0).
                Dim distance As Double = Math.Sqrt(i * i + j * j)
                ' вычислить фазу угла к противоположенной стороне
                Dim phaseAngle As Double = distance * maxPhaseAngle / maxDistance
                ' Вычислить амплитуду к фазовому углу. Добавить к базовой величине для получения данных (i,j).
                'data[i, j] = baseValue + maxAmplitude * Math.Sin(phaseAngle);

                Dim coff As Double = 0
                Select Case j
                    Case 0
                        coff = 300
                        Exit Select
                    Case 1
                        coff = 200
                        Exit Select
                    Case 2
                        coff = 100
                        Exit Select
                    Case 3
                        coff = 50
                        Exit Select
                    Case 4
                        coff = 20
                        Exit Select
                    Case 5
                        coff = 0
                        Exit Select
                    Case 6
                        coff = 50
                        Exit Select
                    Case 7
                        coff = 100
                        Exit Select
                    Case 8
                        coff = 200
                        Exit Select
                    Case 9
                        coff = 300
                        Exit Select
                    Case Else
                        Console.WriteLine("Default case")
                        Exit Select
                End Select

                data(i, j) = baseValue + maxAmplitude * Math.Sin(phaseAngle) - coff
            Next
        Next

        Return data
    End Function

    Private Sub FlowLayoutPanelControls_Resize(sender As Object, e As EventArgs) Handles FlowLayoutPanelControls.Resize
        If Me.IsHandleCreated Then
            Const modelForScalling As Double = 660.0
            Dim factor As Double = Math.Sqrt(FlowLayoutPanelControls.Width * FlowLayoutPanelControls.Height)

            If factor > modelForScalling Then
                Dim scalling As Double = factor / modelForScalling

                For Each itemControl As Control In FlowLayoutPanelControls.Controls
                    If Not itemControl.Equals(TableLayoutPanelProtocol) Then
                        itemControl.Width = CInt(FlowLayoutPanelControlsSize(itemControl).Width * scalling)
                        itemControl.Height = CInt(FlowLayoutPanelControlsSize(itemControl).Height * scalling)
                    End If
                Next
            Else
                For Each itemControl As Control In FlowLayoutPanelControls.Controls
                    itemControl.Size = FlowLayoutPanelControlsSize(itemControl)
                Next
            End If
        End If
    End Sub
End Class

'    Private Sub CWNumEditВремяСбора_ValueChanged(ByVal sender As Object, ByVal e As AxCWUIControlsLib._DCWNumEditEvents_ValueChangedEvent) Handles CWNumEditВремяСбора.ValueChanged
'        intНакопленийКТ = CShort(CWNumEditВремяСбора.Value * intЧастотаФонового) ' / intСтепеньПередиск
'        If intНакопленийКТ < 1 Then intНакопленийКТ = 1
'    End Sub

'    Private Sub CWNumEditВремяСбораНаРадиусе_ValueChanged(ByVal sender As System.Object, ByVal e As AxCWUIControlsLib._DCWNumEditEvents_ValueChangedEvent) Handles CWNumEditВремяСбораНаРадиусе.ValueChanged
'        intНакопленийНаРадиусе = CShort(CWNumEditВремяСбораНаРадиусе.Value) * intЧастотаФонового
'    End Sub


'    Private Sub TabControlAll_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles TabControlAll.SelectedIndexChanged
'        Me.Text = TabControlAll.TabPages(TabControlAll.SelectedIndex).Text '(TabControl1.SelectedTab)
'        If TabControlAll.TabPages(TabControlAll.SelectedIndex).Name = "TabPage63Dграфик" Then
'             Построить3DГрафик()
'        End If
'    End Sub

'Array.ForEach(inputX, AddressOf Increment)

'Public Shared Sub Increment(ByVal s As Double)
'    s += 1
'End Sub
