Imports BaseForm
Imports MathematicalLibrary.Integral
Imports MathematicalLibrary.Air

' в цикле сбора когда видимость=True запускается подсчет, он обновляет массив расчетных параметров, которые будут отображаться 
' до следующей видимость=True
' если видимость=True с частотой 10 герц то при времени вращения турели 10 минут или 600 сек на каждый градус придется 600/360=1.67 секунды
' или 1,67*10герц=16.7 замера
' значит в промежутке между отсечками по градусу надо накопить около 16 замеров в переменной .НакопленноеЗначение и вести счетчик для получения осреднения
' датчик положения на каждой отсечке выдаст сигнал для процедуры осреднения в .НакопленноеЗначение и накопления в классе ПараметрыПоляНакопленные
' который содержит значения всех входных и расчетных параметров через 1 градус для последующего анализа 
' через горелки, контрольные точки (5 штук ? надо сделать 6 кратное 60 градусам, где 1 на 60 а 6 на последнем 360 градусе)
' т.е. потом можно делать все что хочешь намного проще

' Наверно для COM видимости
'<System.Runtime.InteropServices.ProgId("ClassDiagram_NET.ClassDiagram")> Public Class ClassCalculation
'    Implements BaseForm.IClassCalculation
Public Class ClassCalculation
    Implements IClassCalculation

    Public Property Manager() As ProjectManager Implements IClassCalculation.Manager
        Get
            Return mProjectManager
        End Get
        Set(ByVal value As ProjectManager)
            mProjectManager = value
        End Set
    End Property

    ''' <summary>
    ''' Входные аргументы
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property InputParam() As InputParameters

    ''' <summary>
    ''' Настроечные параметры
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property TuningParam() As TuningParameters

    ''' <summary>
    '''  Расчетные параметры
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property CalculatedParam() As CalculatedParameters
    ' входные аргументы
    ' настроечные параметры
    ' расчетные параметры

    Private Enum РасчетРасхода
        РасходGвОсновной = 1
        РасходGвОтбора = 2
        РасходGвОтбораВн = 3 ' внешний? (внутренний?)
    End Enum

    ' событие для выдачи ошибки в вызывающую программу
    'Delegate Sub DataErrorventHandler(ByVal sender As Object, ByVal e As DataErrorEventArgs)
    'Public Event DataError(ByVal sender As Object, ByVal e As BaseForm.IClassCalculation.DataErrorEventArgs) Implements BaseForm.IClassCalculation.DataError
    'Public Event DataError(ByVal sender As BaseForm.IClassCalculation, ByVal e As BaseForm.DataErrorEventArgs) Implements BaseForm.DataError
    Public Event DataError As EventHandler(Of DataErrorEventArgs)

    ' константы переменных
    Private mProjectManager As ProjectManager
    Private Const КЕЛЬВИН As Double = 273.15 ' абс. ноль
    Private Const constG As Double = 9.80665 ' ускорение силы тяжести м/сек.кв.
    Private Const const735_6 As Double = 735.56 ' для барометра
    Private относительныйБарометр As Double ' барометр база
    Private Const СтехиометрическийК As Double = 14.94
    Private Const Rg As Double = 29.29 'была 29.4 при tk=500; при tk=200 - 29.27 ' универсальная газовая постоянная кГм/кг*К
    'Private k As Double ' коэф адиабаты

    '--- конфигурация стенда -------------------------------------------------
    Public D20отвОсн As Double 'диаметр сужащивающего устройства основной
    Public D20трубОсн As Double 'диаметр трубопровода основной
    Public Ks As Double 'коф. сжимаемости измеряемой среды
    Public КоэфЛинейногоТепловогоРасширенияМерногоСопла As Double '= 0.000014 '0.0000105 ' 0.0000105-коэф. линейного теплового расширения мерного сопла

    Public D20отвОтб As Double 'диаметр сужащивающего устройства отбора
    Public D20трубОтб As Double 'диаметр трубопровода отбора
    Public KsОтбН As Double 'коф. сжимаемости измеряемой среды
    Public КоэфЛинейногоТепловогоРасширенияТрубопровода As Double '= 0.0000108 '0.0000165 ' 0.0000165-коэф. линейного теплового расширения трубопровода

    Public D20отвОтбВн As Double 'диаметр сужащивающего устройства отбора
    Public D20трубОтбВн As Double 'диаметр трубопровода отбора
    Public KsОтбВн As Double 'коф. сжимаемости измеряемой среды
    Public КоэфЛинейногоТепловогоРасширенияТрубопроводаВн As Double ' = 0.0000108 ' 0.0000165 ' 0.0000165-коэф. линейного теплового расширения трубопровода Вн

    Public Sub New(ByVal manager As ProjectManager)
        MyBase.New()

        Me.Manager = manager

        InputParam = New InputParameters
        TuningParam = New TuningParameters
        CalculatedParam = New CalculatedParameters

        ' для того чтоба вначале всех таблий и отчетов шли температуры по сечению
        ' вначале занесем имена ПроэкцияНаСтенку1, горелки, ПроэкцияНаСтенку2
        Dim имяПояса As String
        Dim listNameRows As New List(Of String)

        If ПроверитьНаличиеЗаписиРасчетныйПараметр(ПРОЭКЦИЯ_НА_СТЕНКУ1) Then
            'ListNameColumns.Add(Ordinal)
            listNameRows.Add(ПРОЭКЦИЯ_НА_СТЕНКУ1)
        End If

        For I = 1 To ЧИСЛО_ТЕРМОПАР
            имяПояса = "T340_" & I.ToString

            For Each rowИзмеренныйПараметр As BaseFormDataSet.ИзмеренныеПараметрыRow In manager.MeasurementDataTable.Rows
                If rowИзмеренныйПараметр.ИмяПараметра = имяПояса Then
                    listNameRows.Add(имяПояса)
                    Exit For
                End If
            Next
        Next

        If ПроверитьНаличиеЗаписиРасчетныйПараметр(ПРОЭКЦИЯ_НА_СТЕНКУ2) Then
            listNameRows.Add(ПРОЭКЦИЯ_НА_СТЕНКУ2)
        End If

        If ПроверитьНаличиеЗаписиРасчетныйПараметр(CalculatedParameters.conКАЧЕСТВО) Then
            listNameRows.Add(CalculatedParameters.conКАЧЕСТВО)
        End If

        ' вначале заполним имена из ListNameColumns
        For Each nameRow As String In listNameRows
            ПараметрыПоляНакопленные.Add(nameRow, ModelMeasurement.ИзмерениеВезде)
        Next

        ' для каждого измеренного и расчетного параметра сделать свою текстовую константу для произвольного доступа к значению параметра
        ' и цифровую переменную для записи в нее значения
        For Each rowИзмеренныйПараметр As BaseFormDataSet.ИзмеренныеПараметрыRow In manager.MeasurementDataTable.Rows
            ' затем добавлем только те имена которых ещё нет
            If Not listNameRows.Contains(rowИзмеренныйПараметр.ИмяПараметра) Then
                ПараметрыПоляНакопленные.Add(rowИзмеренныйПараметр.ИмяПараметра, ModelMeasurement.ИзмерениеВезде)
            End If
        Next

        For Each rowРасчетныйПараметр As BaseFormDataSet.РасчетныеПараметрыRow In manager.CalculatedDataTable.Rows
            If Not listNameRows.Contains(rowРасчетныйПараметр.ИмяПараметра) Then
                ПараметрыПоляНакопленные.Add(rowРасчетныйПараметр.ИмяПараметра, ModelMeasurement.ИзмерениеВезде)
            End If
        Next

        ' далее добавить параметры которыех нет в расчетных но они есть вспомогательные
        ' пока не надо
        ' ПараметрыПоляНакопленные.Add(conТсредняя_газа_на_входе, enuТипИзмерения.ИзмерениеВезде)
    End Sub

    ''' <summary>
    ''' Последовательное прохождение по этапам приведениия и вычисления.
    ''' Здесь индивидуальные настройки для класса.
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub Calculate() Implements IClassCalculation.Calculate
        If gГеометрияВведена = False Then
            gMainFomMdiParent.varТемпературныеПоля.StatusBar.Items(conStatusLabelMessage).Text = "Для расчета необходимо ввести геометрию!"
            Exit Sub
        End If

        ' Для Приведенных и Пересчитанных параметров входные единицы измерения
        ' только в единицах СИ, выходные единицы измерения - любого типа
        gMainFomMdiParent.varТемпературныеПоля.TextError.Visible = False

        Try
            ' здесь пока не надо получать от контролов
            If mProjectManager.NeedToRewrite Then ПолучитьЗначенияНастроечныхПараметров()
            ' Переводим в Си только измеренные пареметры
            mProjectManager.СonversionToSiUnitMeasurementParameters()
            'получение абсолютных давлений
            mProjectManager.CalculationBasePressure()
            ' весь подсчет производится исключительно в единицах СИ
            ' извлекаем значения измеренных параметров
            ИзвлечьЗначенияИзмеренныхПараметров()
            ВычислитьРасчетныеПараметры()
            mProjectManager.СonversionToTuningUnitCalculationParameters()

            If gНакопитьДляПоля Then НакопитьЗначенияИзмеренныхИРасчетныхПараметров()

            gMainFomMdiParent.varТемпературныеПоля.ОновитьИндикаторы()
            ' там же заполняется массив y()
            If gРисоватьГрафикСечений Then gMainFomMdiParent.varТемпературныеПоля.РисоватьПолеПоСечению()
        Catch ex As Exception
            ' ошибка проглатывается
            'Description = "Процедура: Подсчет"
            ''перенаправление встроенной ошибки
            'Dim fireDataErrorEventArgs As New IClassCalculation.DataErrorEventArgs(ex.Message, Description)
            ''  Теперь вызов события с помощью вызова делегата. Проходя в
            ''   object которое инициирует  событие (Me) такое же как FireEventArgs. 
            ''  Вызов обязан соответствовать сигнатуре FireEventHandler.
            'RaiseEvent DataError(Me, fireDataErrorEventArgs)
        End Try
    End Sub

    '''' <summary>
    '''' Подсчёты не связанные с графическим интерфейсом.
    '''' Графический интерфейс не блокируется.
    '''' </summary>
    '''' <returns></returns>
    'Public Async Function CalcAsynchronouslyAsync() As Task 'Task(Of String) '
    '    'Await Task.Delay(10000)
    '    'Return "Finished"
    '    Dim t As Task = Task.Factory.StartNew(Sub()
    '                                              ' здесь пока не надо получать от контролов
    '                                              If mProjectManager.NeedToRewrite Then ПолучитьЗначенияНастроечныхПараметров()
    '                                              ' Переводим в Си только измеренные пареметры
    '                                              mProjectManager.ПереводВЕдиницыСИИзмеренныеПараметры()
    '                                              'получение абсолютных давлений
    '                                              mProjectManager.УчетБазовыхВеличин()
    '                                              ' весь подсчет производится исключительно в единицах СИ
    '                                              ' извлекаем значения измеренных параметров
    '                                              ИзвлечьЗначенияИзмеренныхПараметров()
    '                                              ВычислитьРасчетныеПараметры()
    '                                              mProjectManager.ПереводВНастоечныеЕдиницыРасчетныхПараметров()

    '                                              If gНакопитьДляПоля Then НакопитьЗначенияИзмеренныхИРасчетныхПараметров()
    '                                          End Sub)
    '    t.Wait()

    '    Await t
    'End Function

    Dim description As String = $"Процедура: {NameOf(ПолучитьЗначенияНастроечныхПараметров)}"
    ''' <summary>
    ''' Получить значения параметров, используемых как настраиваемые глобальные переменные.
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub ПолучитьЗначенияНастроечныхПараметров()
        If Manager.TuningDataTable Is Nothing Then Exit Sub

        Dim success As Boolean = False

        ' Вначале проверяется наличие расчетных параметров в базе
        For Each имяНастроечногоПараметра As String In TuningParam.TuningDictionary.Keys.ToArray 'arrНастроечныеПараметры
            success = False

            For Each rowНастроечныйПараметр As BaseFormDataSet.НастроечныеПараметрыRow In Manager.TuningDataTable.Rows
                If rowНастроечныйПараметр.ИмяПараметра = имяНастроечногоПараметра Then
                    success = True
                    Exit For
                End If
            Next

            If success = False Then
                ' перенаправление встроенной ошибки
                RaiseEvent DataError(Me, New DataErrorEventArgs($"Настроечный параметр {имяНастроечногоПараметра} в базе параметров не найден!", description)) 'не ловит в конструкторе
                'MessageBox.Show(Message, Description, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                Exit Sub
            End If
        Next

        ' проверяется наличие в расчетном модуле переменных, соответствующих расчетным настроечным
        ' и присвоение им значений
        success = True
        Try
            For Each rowНастроечныйПараметр As BaseFormDataSet.НастроечныеПараметрыRow In Manager.TuningDataTable.Rows
                'If arrНастроечныеПараметры.Contains(rowНастроечныйПараметр.ИмяПараметра) Then
                If TuningParam.TuningDictionary.Keys.Contains(rowНастроечныйПараметр.ИмяПараметра) Then

                    Select Case rowНастроечныйПараметр.ИмяПараметра
                        'Case "GвМПитоПриводить"
                        '    'GвМПитоПриводить = rowНастроечныйПараметр.ЦифровоеЗначение
                        '    'n1ГПриводить = CInt(rowНастроечныйПараметр.ЛогическоеЗначение)
                        '    GвМПитоПриводить = rowНастроечныйПараметр.ЛогическоеЗначение
                        '    Exit Select
                        'Case "GвМПолеДавленийПриводить"
                        '    GвМПолеДавленийПриводить = rowНастроечныйПараметр.ЛогическоеЗначение
                        '    Exit Select
                        'Case "n1ГПриводить"
                        '    n1ГПриводить = rowНастроечныйПараметр.ЛогическоеЗначение
                        '    Exit Select
                        'Case "nИГ-03ГПриводить"
                        '    nИГ_03ГПриводить = rowНастроечныйПараметр.ЛогическоеЗначение
                    End Select
                Else
                    success = False
                    'перенаправление встроенной ошибки
                    RaiseEvent DataError(Me, New DataErrorEventArgs($"Настроечный параметр {rowНастроечныйПараметр.ИмяПараметра} не имеет соответствующей переменной в модуле расчета!", description)) ' не ловит в конструкторе
                    'MessageBox.Show(Message, Description, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                End If
            Next

            With gMainFomMdiParent.Manager.TuningDataTable
                D20трубОсн = .FindByИмяПараметра(TuningParameters.conD20трубОсн).ЦифровоеЗначение
                D20отвОсн = .FindByИмяПараметра(TuningParameters.conD20отвОсн).ЦифровоеЗначение
                Ks = .FindByИмяПараметра(TuningParameters.conKs).ЦифровоеЗначение
                КоэфЛинейногоТепловогоРасширенияМерногоСопла = .FindByИмяПараметра(TuningParameters.conKtосн).ЦифровоеЗначение

                D20отвОтб = .FindByИмяПараметра(TuningParameters.conD20отвОтб).ЦифровоеЗначение
                D20трубОтб = .FindByИмяПараметра(TuningParameters.conD20трубОтб).ЦифровоеЗначение
                KsОтбН = .FindByИмяПараметра(TuningParameters.conKsОтбН).ЦифровоеЗначение
                КоэфЛинейногоТепловогоРасширенияТрубопровода = .FindByИмяПараметра(TuningParameters.conKtотбн).ЦифровоеЗначение

                D20отвОтбВн = .FindByИмяПараметра(TuningParameters.conD20отвОтбВн).ЦифровоеЗначение
                D20трубОтбВн = .FindByИмяПараметра(TuningParameters.conD20трубОтбВн).ЦифровоеЗначение
                KsОтбВн = .FindByИмяПараметра(TuningParameters.conKsОтбВн).ЦифровоеЗначение
                КоэфЛинейногоТепловогоРасширенияТрубопроводаВн = .FindByИмяПараметра(TuningParameters.conKtотбвн).ЦифровоеЗначение
            End With

            If success = False Then Exit Sub

            ' занести значения настроечных параметров
            With Manager.TuningDataTable
                For Each keysTuning As String In TuningParam.TuningDictionary.Keys.ToArray
                    If .FindByИмяПараметра(keysTuning).ЛогикаИлиЧисло Then
                        TuningParam.TuningDictionary(keysTuning).ЛогикаИлиЧисло = True
                        TuningParam.TuningDictionary(keysTuning).ЛогическоеЗначение = .FindByИмяПараметра(keysTuning).ЛогическоеЗначение
                    Else
                        TuningParam.TuningDictionary(keysTuning).ЛогикаИлиЧисло = False
                        TuningParam.TuningDictionary(keysTuning).ЦифровоеЗначение = .FindByИмяПараметра(keysTuning).ЦифровоеЗначение
                    End If
                Next
            End With

        Catch ex As Exception
            ' перенаправление встроенной ошибки
            RaiseEvent DataError(Me, New DataErrorEventArgs(ex.Message, description)) 'не ловит в конструкторе
            'MessageBox.Show(fireDataErrorEventArgs, Description, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try
    End Sub

    ''' <summary>
    ''' Поиск всех параметров по пользовательскому запросу в DataSet.ИзмеренныеПараметры
    ''' (с одним входным параметром являющимся именем связи для реального измеряемого канала Сервера).
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub ИзвлечьЗначенияИзмеренныхПараметров()
        'Dim rowИзмеренныйПараметр As BaseFormDataSet.ИзмеренныеПараметрыRow
        Try
            With Manager.MeasurementDataTable
                ' вместо последовательного извлечения применяется обход по коллекции
                ' ARG1 = .FindByИмяПараметра(conARG1).ЗначениеВСИ
                ' ...
                ' ARG10 = .FindByИмяПараметра(conARG10).ЗначениеВСИ

                'For Each keysArg As String In inputArg.InputArgDictionary.Keys.ToArray
                '    inputArg.InputArgDictionary(keysArg) = .FindByИмяПараметра(keysArg).ЗначениеВСИ
                'Next

                ' '' иттератор по коллекции как KeyValuePair objects.
                ''For Each kvp As KeyValuePair(Of String, Double) In inputArg.InputArgDictionary
                ''    'Console.WriteLine("Key = {0}, Value = {1}", kvp.Key, kvp.Value)
                ''    inputArg.InputArgDictionary(kvp.Key) = .FindByИмяПараметра(kvp.Key).ЗначениеВСИ
                ''Next

                ''For Each value As Double In inputArg.InputArgDictionary.Values
                ''    Console.WriteLine("Value = {0}", value)
                ''Next

                ' расчетные параметры
                InputParam.Tбокса = .FindByИмяПараметра(InputParameters.conTБОКСА).ЗначениеВСИ ' температура в боксе
                InputParam.Барометр = .FindByИмяПараметра(InputParameters.conБАРОМЕТР).ЗначениеВСИ ' БРС1-М
                ' учет атмосферного давления - относительного давления воздуха
                относительныйБарометр = InputParam.Барометр / const735_6
                InputParam.T3мерн_участка = .FindByИмяПараметра(InputParameters.T3_МЕРН_УЧАСТКА).ЗначениеВСИ
                InputParam.Тотбора = .FindByИмяПараметра(InputParameters.Т_ОТБОРА).ЗначениеВСИ
                InputParam.ТотбораВн = .FindByИмяПараметра(InputParameters.Т_ОТБОРА_ВН).ЗначениеВСИ

                'rowИзмеренныйПараметр = .FindByИмяПараметра(conДавлениеВоздухаНаВходе)
                'rowИзмеренныйПараметр.ЗначениеВСИ = rowИзмеренныйПараметр.ЗначениеВСИ + B
                'ДавлениеВоздухаНаВходе = rowИзмеренныйПараметр.ЗначениеВСИ

                InputParam.ДавлениеВоздухаНаВходе = .FindByИмяПараметра(InputParameters.ДАВЛЕНИЕ_ВОЗДУХА_НА_ВХОДЕ).ЗначениеВСИ + относительныйБарометр
                InputParam.ДавлениеМагистралеОтбора = .FindByИмяПараметра(InputParameters.ДАВЛЕНИЕ_МАГИСТРАЛЕ_ОТБОРА).ЗначениеВСИ + относительныйБарометр
                InputParam.ДавлениеМагистралеОтбораВн = .FindByИмяПараметра(InputParameters.ДАВЛЕНИЕ_МАГИСТРАЛЕ_ОТБОРА_ВН).ЗначениеВСИ + относительныйБарометр
                ' можно так но хуже
                'rowИзмеренныйПараметр = .FindByИмяПараметра(conДавлениеМагистралеОтбора)
                'rowИзмеренныйПараметр.ЗначениеВСИ = rowИзмеренныйПараметр.ЗначениеВСИ + B
                'ДавлениеМагистралеОтбора = rowИзмеренныйПараметр.ЗначениеВСИ

                InputParam.ПерепадДавленияВоздухаНаВходе = .FindByИмяПараметра(InputParameters.ПЕРЕПАД_ДАВЛЕНИЯ_ВОЗДУХА_НА_ВХОДЕ).ЗначениеВСИ
                InputParam.ПерепадДавленияВоздухаОтбора = .FindByИмяПараметра(InputParameters.ПЕРЕПАД_ДАВЛЕНИЯ_ВОЗДУХА_ОТБОРА).ЗначениеВСИ
                InputParam.ПерепадДавленияВоздухаОтбораВн = .FindByИмяПараметра(InputParameters.ПЕРЕПАД_ДАВЛЕНИЯ_ВОЗДУХА_ОТБОРА_ВН).ЗначениеВСИ

                InputParam.Р310полное_воздуха_на_входе_КС = .FindByИмяПараметра(InputParameters.Р310_ПОЛНОЕ_ВОЗДУХА_НА_ВХОДЕ_КC).ЗначениеВСИ + относительныйБарометр  ' Давление воздуха на входе в КС
                'rowИзмеренныйПараметр = .FindByИмяПараметра(conР310полное_воздуха_на_входе_КС)
                'rowИзмеренныйПараметр.ЗначениеВСИ = rowИзмеренныйПараметр.ЗначениеВСИ + B
                'Р310полное_воздуха_на_входе_КС = rowИзмеренныйПараметр.ЗначениеВСИ

                InputParam.Р311статическое_воздуха_на_входе_КС = .FindByИмяПараметра(InputParameters.Р311_СТАТИЧЕСКОЕ_ВОЗДУХА_НА_ВХОДЕ_КС).ЗначениеВСИ + относительныйБарометр
                'rowИзмеренныйПараметр = .FindByИмяПараметра(conР311статическое_воздуха_на_входе_КС)
                'rowИзмеренныйПараметр.ЗначениеВСИ = rowИзмеренныйПараметр.ЗначениеВСИ + B
                'Р311статическое_воздуха_на_входе_КС = rowИзмеренныйПараметр.ЗначениеВСИ

                InputParam.ТтопливаКС = .FindByИмяПараметра(InputParameters.Т_ТОПЛИВА_КС).ЗначениеВСИ
                InputParam.ТтопливаКП = .FindByИмяПараметра(InputParameters.Т_ТОПЛИВА_КП).ЗначениеВСИ
                InputParam.РасходТопливаКамерыСгорания = .FindByИмяПараметра(InputParameters.Расход_Топлива_Камеры_Сгорания).ЗначениеВСИ
                InputParam.РасходТопливаКамерыПодогрева = .FindByИмяПараметра(InputParameters.РАСХОД_ТОПЛИВА_КАМЕРЫ_ПОДОГРЕВА).ЗначениеВСИ
                ' Test
#If EmulatorT340 = False Then
                ' закоментировал. т.к. реального входного канала нет
                'InputParam.Отсечка = .FindByИмяПараметра(InputParameter.ОТСЕЧКА_ТУРЕЛИ).ЗначениеВСИ
#End If

                ' постоянно обновляется в событии сбора, а когда отсечка следующего градуса сюда заносится 
                'arrПоясDictionary("Пояс" & I.ToString)(ИндексОтсечекДляПоля) = .FindByИмяПараметра("T340_" & I.ToString).НакопленноеЗначение 
                For I = 1 To ЧИСЛО_ТЕРМОПАР
                    'arrПоясDictionary("Пояс" & I.ToString)(ИндексОтсечекДляПоля) = .FindByИмяПараметра("T340_" & I.ToString).ЗначениеВСИ 'T340_1
#If EmulatorT340 = True Then
                    ' Test
                    arrТекущаяПоПоясам(I - 1) = gMainFomMdiParent.varТемпературныеПоля.arrDataTemperatureTest(ИндексОтсечекДляПоля, I - 1) '1506.3
#Else
                    arrТекущаяПоПоясам(I - 1) = .FindByИмяПараметра("T340_" & I.ToString).ЗначениеВСИ
#End If
                Next

                CalculatedParam.ТсредняяГазаНаВходе = 0
                For I As Integer = 1 To ЧИСЛО_Т_309
                    CalculatedParam.ТсредняяГазаНаВходе += .FindByИмяПараметра("T309_" & I.ToString).ЗначениеВСИ
                Next

                CalculatedParam.ТсредняяГазаНаВходе = CalculatedParam.ТсредняяГазаНаВходе / ЧИСЛО_Т_309
            End With
        Catch ex As Exception
            gMainFomMdiParent.varТемпературныеПоля.ShowError("Ошибка извлечения измеренных параметров")
            'перенаправление встроенной ошибки
            RaiseEvent DataError(Me, New DataErrorEventArgs(ex.Message, $"Процедура: <{NameOf(ИзвлечьЗначенияИзмеренныхПараметров)}>"))
        End Try
    End Sub

    ''' <summary>
    ''' накопление всех измеренных параметров
    ''' </summary>
    Private Sub НакопитьЗначенияИзмеренныхИРасчетныхПараметров()
        For Each rowИзмеренныйПараметр As BaseFormDataSet.ИзмеренныеПараметрыRow In Manager.MeasurementDataTable.Rows
            rowИзмеренныйПараметр.НакопленноеЗначение += rowИзмеренныйПараметр.ЗначениеВСИ
        Next

        For Each rowРасчетныйПараметр As BaseFormDataSet.РасчетныеПараметрыRow In Manager.CalculatedDataTable.Rows
            rowРасчетныйПараметр.НакопленноеЗначение += rowРасчетныйПараметр.ВычисленноеЗначениеВСИ
        Next

        СчетчикНакоплений += 1
    End Sub

    ''' <summary>
    ''' Поиск всех параметров по пользовательскому запросу в DataSet.РасчетныеПараметры
    ''' (с одним входным параметром являющимся именем расчётной величины).
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub ВычислитьРасчетныеПараметры()
        Try
            ВычислениеОсновных()
            '******************************************************************
            ' расчет интегральной температура газа по мерному сечению на поясе
            '******************************************************************
            Dim Ystart As Double, Yend As Double
            НайтиЗначенияТемпературНаСтенкахInterpolate(КоординатыТермопар, arrТекущаяПоПоясам, ШиринаМерногоУчастка, Ystart, Yend)
            y(0) = Ystart
            y(ЧИСЛО_ТЕРМОПАР + 1) = Yend

            CalculatedParam.T_интегр = ИнтегрированиеРадиальнойЭпюрыНаПроизвольныхКоординатах(КоординатыТермопар, arrТекущаяПоПоясам, ШиринаМерногоУчастка)

            '******************************************************************
            ' расчет Качество 
            '******************************************************************
            CalculatedParam.Качество = КачествоFun(CalculatedParam.T_интегр, CalculatedParam.ТсредняяГазаНаВходе, CalculatedParam.Тг_расчет)
            CalculatedParam.ПоложениеТурели = gMainFomMdiParent.varEncoder.AnglePosition

            ' занести вычисленные значения
            With Manager.CalculatedDataTable
                '' вместо последовательного извлечения применяется обход по коллекции
                '' .FindByИмяПараметра(conCalc1).ВычисленноеЗначениеВСИ = Calc1
                ' ********************************** и т.д. ********************************
                '' .FindByИмяПараметра(conCalc10).ВычисленноеЗначениеВСИ = Calc10

                '.FindByИмяПараметра(CalculatedParameter.G_СУМ_РАСХОД_ТОПЛИВА_КС_КП).ВычисленноеЗначениеВСИ = CalculatedParam.Gсум_расход_топливаКС_КП_кг_час
                '.FindByИмяПараметра(CalculatedParameter.G_ОТБОРА_ОТНОСИТЕЛЬНЫЙ).ВычисленноеЗначениеВСИ = CalculatedParam.Gотбора_относительный
                ' ********************************** и т.д. ********************************

                For Each keysCalc As String In CalculatedParam.CalcDictionary.Keys.ToArray
                    .FindByИмяПараметра(keysCalc).ВычисленноеЗначениеВСИ = CalculatedParam(keysCalc)
                Next

                ' расчетные вспомогательные ...

                .FindByИмяПараметра(ПРОЭКЦИЯ_НА_СТЕНКУ1).ВычисленноеЗначениеВСИ = Ystart
                .FindByИмяПараметра(ПРОЭКЦИЯ_НА_СТЕНКУ2).ВычисленноеЗначениеВСИ = Yend
                '.FindByИмяПараметра(conЭпюрнаяНеравномерность).ВычисленноеЗначениеВСИ = вычисляются в РасчетПоля
                '.FindByИмяПараметра(conОкружнаяНеравномерность).ВычисленноеЗначениеВСИ = вычисляются в РасчетПоля

                '  Эпюрная Неравномерность и Окружная Неравномерность
                Dim средняя, temp As Double
                Dim max As Double = Double.MinValue

                For I = 1 To ЧИСЛО_ТЕРМОПАР
                    temp = arrТекущаяПоПоясам(I - 1)
                    средняя += temp
                    If temp > max Then
                        max = temp
                    End If
                Next

                средняя = средняя / ЧИСЛО_ТЕРМОПАР

                '.FindByИмяПараметра(ОКРУЖНАЯ_НЕРАВНОМЕРНОСТЬ).ВычисленноеЗначениеВСИ = НеравномерностьFun(max, CalculatedParam.ТсредняяГазаНаВходе, CalculatedParam.Тг_расчет)
                '.FindByИмяПараметра(ЭПЮРНАЯ_НЕРАВНОМЕРНОСТЬ).ВычисленноеЗначениеВСИ = НеравномерностьFun(средняя, CalculatedParam.ТсредняяГазаНаВходе, CalculatedParam.Тг_расчет)

                .FindByИмяПараметра(ОКРУЖНАЯ_НЕРАВНОМЕРНОСТЬ).ВычисленноеЗначениеВСИ = НеравномерностьFun(max, CalculatedParam.ТсредняяГазаНаВходе, CalculatedParam.T_интегр)
                .FindByИмяПараметра(ЭПЮРНАЯ_НЕРАВНОМЕРНОСТЬ).ВычисленноеЗначениеВСИ = НеравномерностьFun(средняя, CalculatedParam.ТсредняяГазаНаВходе, CalculatedParam.T_интегр)

                '.FindByИмяПараметра(conПоясMax).ВычисленноеЗначениеВСИ = вычисляются в РасчетПоля
            End With

            'With Manager
            '    ''по имени параметра strИмяПараметраГрафика определяем нужную функцию приведения
            '    ''("n1") 'который измеряет
            '    ''должна быть вызвана функция приведения параметра "n1" например
            '    'If n1ГПриводить Then
            '    '    'должна быть вызвана функция приведения параметра "n1" например
            '    '    .FindByИмяПараметра(cn1Г).ВычисленноеЗначениеВСИ = Air.funПривестиN(.ИзмеренныеПараметры.FindByИмяПараметра("n1").ЗначениеВСИ, tm)
            '    'Else
            '    '    'приводить не надо, просто копирование
            '    '    .FindByИмяПараметра(cn1Г).ВычисленноеЗначениеВСИ = .ИзмеренныеПараметры.FindByИмяПараметра("n1").ЗначениеВСИ
            '    'End If

            '    ''cGвМПолеДавлений
            '    '.FindByИмяПараметра(cGвМПолеДавлений).ВычисленноеЗначениеВСИ = funВычислитьGвМПолеДавлений(GвМПолеДавленийПриводить)
            '    ''или так
            '    ''.FindByИмяПараметра(cGвМПолеДавлений).ВычисленноеЗначениеВСИ = funВычислитьGвМПолеДавлений(.НастроечныеПараметры.FindByИмяПараметра("GвМПолеДавленийПриводить").ЛогическоеЗначение)

            '    ''cGвМПито
            '    '.FindByИмяПараметра(cGвМПито).ВычисленноеЗначениеВСИ = funВычислитьGвМПито(GвМПитоПриводить)

            '    ''cПиК
            '    '.FindByИмяПараметра(cПиК).ВычисленноеЗначениеВСИ = funВычислитьПиК()

            '    ''cКПДадиабат
            '    '.FindByИмяПараметра(cКПДадиабат).ВычисленноеЗначениеВСИ = funВычислитьКПДадиабат()

            '    ''cnИГ_03Г
            '    ''который измеряет
            '    'nИГ_03 = .ИзмеренныеПараметры.FindByИмяПараметра("nИГ-03").ЗначениеВСИ / 46325 'коэф. перевода  n=1 при N=45190

            '    'If nИГ_03ГПриводить Then
            '    '    'должна быть вызвана функция приведения параметра "n1" например
            '    '    .FindByИмяПараметра(cnИГ_03Г).ВычисленноеЗначениеВСИ = Air.funПривестиN(nИГ_03, tm)
            '    'Else
            '    '    'приводить не надо, просто копирование
            '    '    .FindByИмяПараметра(cnИГ_03Г).ВычисленноеЗначениеВСИ = nИГ_03
            '    'End If

            '    ''cnПиК_GвМПД
            '    ''должна быть вызвана функция вычисления параметра "GПиК/GвМПД" например
            '    '.FindByИмяПараметра(cnПиК_GвМПД).ВычисленноеЗначениеВСИ = funВычислитьПиК_GвМПД()
            'End With
        Catch ex As Exception
            gMainFomMdiParent.varТемпературныеПоля.ShowError("Ошибка вычисления расчётных параметров")
            'перенаправление встроенной ошибки
            RaiseEvent DataError(Me, New DataErrorEventArgs(ex.Message, $"Процедура: <{NameOf(ВычислитьРасчетныеПараметры)}>"))
        End Try

        If CBool(CalculatedParam.Тг_расчет = 0 - КЕЛЬВИН) Then
            gMainFomMdiParent.varТемпературныеПоля.ShowError("Ошибка вычисления расчётной температуры газа")
        End If
    End Sub

    ''' <summary>
    ''' Основные вычисления, накопления, запись в базу для протокола.
    ''' Индивидуальные для наследуемого класса.
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub ВычислениеОсновных()
        '**********************************************************************
        ' расчёт основных параметров
        '**********************************************************************
        ' Барометр-барометр
        ' gp-расход топлива камеры подогрева
        ' gc-расход топлива камеры сгорания
        ' Gвоздуха-расход Gв основной
        ' go-расход Go отбора
        ' Pp-плотность топлива от температуры
        ' Ps-плотность топлива от температуры
        ' gg-расход газа
        ' gv-расход воздуха участвующий в горении
        ' gs-суммарный расход топлива через к.с. и к.п.
        ' Gотбора-относительный расход отбираемого газа
        ' АльфаКамеры-коэффициент избытка воздуха
        ' ls-суммарный коэффициент избытка воздуха
        ' Лямбда-приведенная скорость газового потока на входе в К.С.
        Dim Отн_расход_отбир_газа, Gтопл_в_отбир_газе As Double
        Dim Gрасход_газа, Gвоздуха_в_горении As Double
        Dim funGотА As Double
        Dim Gрасход_отбора_нар, РасходТопливаКамерыПодогреваКгСек, РасходТопливаКамерыСгоранияКгСек As Double
        Dim Gсум_расход_топливаКС_КП As Double
        Dim Gрасход_отбора_вн As Double ' ЛМЗ

        'найти коэф. учитывающий плотность
        Dim КплотностьКС As Double = clAir.КоэфУчитывающийПлотность(InputParam.ТтопливаКС)
        Dim КплотностьКП As Double = clAir.КоэфУчитывающийПлотность(InputParam.ТтопливаКП)
        ' расход топлива камеры сгорания
        РасходТопливаКамерыСгоранияКгСек = InputParam.РасходТопливаКамерыСгорания * КплотностьКС / 3600.0# 'перевод л/час в кг/сек камеры сгорания
        ' расход топлива камеры подогрева
        РасходТопливаКамерыПодогреваКгСек = InputParam.РасходТопливаКамерыПодогрева * КплотностьКП / 3600.0# 'перевод л/час в кг/сек камеры подогрева
        ' расход Gв основной
        CalculatedParam.Gвоздуха = Расход(InputParam.ДавлениеВоздухаНаВходе,
                                          InputParam.ПерепадДавленияВоздухаНаВходе,
                                          InputParam.T3мерн_участка,
                                          D20отвОсн,
                                          D20трубОсн,
                                          Ks,
                                          КоэфЛинейногоТепловогоРасширенияМерногоСопла,
                                          РасчетРасхода.РасходGвОсновной)
        ' расход Go отбора наружного кольцевого канала
        Gрасход_отбора_нар = Расход(InputParam.ДавлениеМагистралеОтбора,
                                    InputParam.ПерепадДавленияВоздухаОтбора,
                                    InputParam.Тотбора,
                                    D20отвОтб,
                                    D20трубОтб,
                                    KsОтбН,
                                    КоэфЛинейногоТепловогоРасширенияТрубопровода,
                                    РасчетРасхода.РасходGвОтбора)
        ' расход Go отбора Вн
        'Gрасход_отбора_вн = Расход(InputParam.ДавлениеМагистралеОтбораВн, InputParam.ПерепадДавленияВоздухаОтбораВн, InputParam.ТотбораВн, D20отвОтбВн, D20трубОтбВн, KsОтбВн, КоэфЛинейногоТепловогоРасширенияТрубопроводаВн, РасчетРасхода.РасходGвОтбораВн)
        Gрасход_отбора_вн = 0 ' Салют 06.07.2017 убрал расчёт этого параметра и таким образом вернул методику Салют

        ' ЛМЗ расход Go отбора наружного кольцевого канал + расход Go отбора Вн
        CalculatedParam.Gрасход_отбора_сумм = Gрасход_отбора_нар + Gрасход_отбора_вн

        'расход газа gg на выходе из КП
        Gрасход_газа = CalculatedParam.Gвоздуха + РасходТопливаКамерыПодогреваКгСек

        ' относительный расход отбираемого газа
        'Отн_расход_отбир_газа = Gрасход_отбора_нар / Gасход_газа ' Салют
        Отн_расход_отбир_газа = (Gрасход_отбора_нар + Gрасход_отбора_вн) / Gрасход_газа ' ЛМЗ

        ' кол. топлива в отбираемом газе
        Gтопл_в_отбир_газе = РасходТопливаКамерыПодогреваКгСек * Отн_расход_отбир_газа

        ' расход воздуха участвующий в горении К.С. (Gвкс)
        'Gвоздуха_в_горении = CalculatedParam.Gвоздуха - Gрасход_отбора_нар + Gтопл_в_отбир_газе ' Салют
        Gвоздуха_в_горении = CalculatedParam.Gвоздуха - (Gрасход_отбора_нар + Gрасход_отбора_вн) + Gтопл_в_отбир_газе ' ЛМЗ

        ' суммарный расход топлива через к.с. и к.п.
        Gсум_расход_топливаКС_КП = РасходТопливаКамерыСгоранияКгСек + РасходТопливаКамерыПодогреваКгСек - Gтопл_в_отбир_газе
        CalculatedParam.Gсум_расход_топливаКС_КП_кг_час = Gсум_расход_топливаКС_КП * 3600.0#

        ' коэффициент избытка воздуха К.С.
        CalculatedParam.АльфаКамеры = Gвоздуха_в_горении / (СтехиометрическийК * РасходТопливаКамерыСгоранияКгСек)
        ' ЛМЗ коэффициент избытка воздуха К.П.
        CalculatedParam.АльфаКП = CalculatedParam.Gвоздуха / (СтехиометрическийК * РасходТопливаКамерыПодогреваКгСек)
        'ЛМЗ коэффициент избытка воздуха суммарный
        CalculatedParam.АльфаСуммарный = Gвоздуха_в_горении / (СтехиометрическийК * Gсум_расход_топливаКС_КП)

        ' относительный расход отбираемого газа
        'CalculatedParam.Gотбора_относительный = Gрасход_отбора_нар * 100.0# / Gасход_газа ' Салют
        CalculatedParam.Gотбора_относительный_нар = Gрасход_отбора_нар * 100.0# / Gрасход_газа ' ЛМЗ
        CalculatedParam.Gотбора_относительный_вн = Gрасход_отбора_вн * 100.0# / Gрасход_газа ' ЛМЗ
        CalculatedParam.Gотбора_относительный = (Gрасход_отбора_нар + Gрасход_отбора_вн) * 100.0# / Gрасход_газа ' ЛМЗ

        ' k=1.358 при Tk на входе в камеру сгорания примерно 500 гр. цельсия
        Const k As Double = 1.358

        Dim tempM As Double = Math.Sqrt(k * (2.0 / (k + 1.0)) ^ ((k + 1.0) / (k - 1.0))) * Math.Sqrt(constG / Rg)
        ' вычисление g(a)
        funGотА = Gрасход_газа * Math.Sqrt(CalculatedParam.ТсредняяГазаНаВходе + КЕЛЬВИН) / (tempM * InputParam.Р310полное_воздуха_на_входе_КС * Fdif)
        ' приведенная скорость газового потока 
        CalculatedParam.Лямбда = clAir.ПриведеннаяСкорость(funGотА, IzoentropaK.K135)
        '' тест
        '' взяты Gасход_газа (Gгкп) из протокола 20 цеха и P310 без барометра (Ба  =  1.0173)
        '' получилось очень близкое значение к протоколу (там Лямбда равна 0.27437)
        ''  скорее всего в вычислении в 20 цехе ошибка при подстановке P310 без барометра
        'funGотА = 13.989 * Math.Sqrt(CalculatedParam.ТсредняяГазаНаВходе + КЕЛЬВИН) / (tempM * 4.0337 * Fdif)
        'CalculatedParam.Лямбда = clAir.ПриведеннаяСкорость(funGотА, IzoentropaK.K135)

        'Dim T3b, Gtksb, Gbksb, АльфаСум As Double
        'T3b = 308.7121+conКельвин
        'Gtksb = 1473.427 / 3600
        'Gbksb = 12.49772
        'АльфаСум = 2.043875
        '**********************************************************************
        ' расчетная температура газа в Цельсий
        '**********************************************************************
        ' Т3 температура на входе в мерный участок (или тккп - среднее из 3 термопар на входе в камеру подогрева)
        ' Gтопл суммарный
        ' Gвоздкс
        CalculatedParam.Тг_расчет = clAir.РасчётнаяТемпература(InputParam.T3мерн_участка + КЕЛЬВИН, Gсум_расход_топливаКС_КП, Gвоздуха_в_горении, CalculatedParam.АльфаСуммарный)

        ' тест ЛМЗ получилось 1541.4
        'CalculatedParam.Тг_расчет = clAir.РасчётнаяТемпература(229.99 + КЕЛЬВИН, 1622.21 / 3600, 11.33, 1.68) '2.142)
        ' проверка ручного протокола
        'CalculatedParam.Тг_расчет = clAir.РасчётнаяТемпература(299.84 + КЕЛЬВИН, 1400 / 3600, 12, 2.4)

    End Sub

    ''' <summary>
    ''' Функция расчёта расхода Gв основн и Gв отбора
    ''' </summary>
    ''' <param name="P"></param>
    ''' <param name="dP"></param>
    ''' <param name="ТпередСужающимУстройством"></param>
    ''' <param name="d20отв"></param>
    ''' <param name="D20труб"></param>
    ''' <param name="коэфСжимаемости"></param>
    ''' <param name="inРасчетРасхода"></param>
    ''' <returns></returns>
    Private Function Расход(ByVal P As Double,
                            ByVal dP As Double,
                            ByVal ТпередСужающимУстройством As Double,
                            ByVal d20отв As Double,
                            ByVal D20труб As Double,
                            ByVal коэфСжимаемости As Double,
                            ByVal коэфЛинейногоТепловогоРасширения As Double,
                            ByVal inРасчетРасхода As РасчетРасхода
                            ) As Double
        ' P - абсолютное давление
        ' dP - перепад давлений
        ' Т - рабочая температура
        ' D20отв - диаметр сужащивающего устроуства
        ' D20труб - диаметр трубопровода
        ' Ks - коф. сжимаемости измеряемой среды
        ' Flag - признак =1 для Gоснов и =2 для Gотб
        ' введеено считывание с базы.


        Const ТцельсияНормУсловия As Double = 20.0 ' - нормальные условия
        Const ТнормУсловия As Double = КЕЛЬВИН + ТцельсияНормУсловия ' 293.15  - нормальные условия
        Dim K, E, M As Double

        '' учёт температурных потерь в магистрале перепуска и разныз материалов сопла и трубопровода
        'Const РабочаяТемператураОсновногоЛудла As Double = 250.0
        'Const РабочаяТемператураОтбораЛудла As Double = 500.0
        'Const РабочаяТемператураОтбораТрубопровода As Double = 475.0
        'Select Case inРасчетРасхода
        '    Case РасчетРасхода.РасходGвОсновной
        '        dотв = d20отв * (1 + КоэфЛинейногоТепловогоРасширенияМерногоСопла * (РабочаяТемператураОсновногоЛудла - ТцельсияНормУсловия))
        '        Dтруб = D20труб * (1 + КоэфЛинейногоТепловогоРасширенияТрубопровода * (РабочаяТемператураОсновногоЛудла - ТцельсияНормУсловия))
        '        Exit Select
        '    Case РасчетРасхода.РасходGвОтбора
        '        dотв = d20отв * (1 + КоэфЛинейногоТепловогоРасширенияМерногоСопла * (РабочаяТемператураОтбораЛудла - ТцельсияНормУсловия))
        '        Dтруб = D20труб * (1 + КоэфЛинейногоТепловогоРасширенияТрубопровода * (РабочаяТемператураОтбораТрубопровода - ТцельсияНормУсловия))
        '        Exit Select
        '    Case РасчетРасхода.РасходGвОтбораВн
        '        Exit Select
        'End Select

        ' здесь материал сопла и трубопровода подразумевается один и тот же
        Dim dотв As Double = d20отв * (1 + коэфЛинейногоТепловогоРасширения * (ТпередСужающимУстройством - ТцельсияНормУсловия))
        Dim Dтруб As Double = D20труб * (1 + коэфЛинейногоТепловогоРасширения * (ТпередСужающимУстройством - ТцельсияНормУсловия))

        ' m - модуль сужающего устройства
        M = dотв / Dтруб
        M = M * M
        ' e - поправочный множитель на расширение среды
        ' K - показатель адиабаты
        ' K = 1.4
        If ТпередСужающимУстройством < 50 Then
            K = 1.4
        Else
            K = Kadiobaty(ТпередСужающимУстройством)
        End If

        E = (1 - dP / P) ^ (2 / K)
        E = E * (K / (K - 1))
        E = E * ((1 - (1 - dP / P) ^ ((K - 1) / K)) / (dP / P))
        E = E * (1 - M * M) / (1 - M * M * (1 - dP / P) ^ (2 / K))
        E = Math.Sqrt(E)

        Dim Re As Double ' Re - число Рейнольда
        Dim ro As Double ' ro - плотность воздуха при рабочих условиях
        Dim расходИтог As Double ' Расход - результат

        Const ДавлениеВоздухаНормальныхУсловиях As Double = 1.0332 ' 1.0332 - давление воздуха в нормальных условиях
        Const ПлотностьВоздухаНормальныхУсловиях As Double = 1.205 ' 1.205 - плотность воздуха в нормальных условиях
        'Const КоэфСжимаемости As Double = 1.0025 ' 1.0025 - коэф. сжимаемости

        '' учёт температурных потерь в магистрале перепуска
        'Const РабочееДавление As Double = 5.0 ' 5 - рабочее давление
        'Select Case inРасчетРасхода
        '    Case РасчетРасхода.РасходGвОсновной
        '        ro = ПлотностьВоздухаНормальныхУсловиях * ТнормУсловия * РабочееДавление / (ДавлениеВоздухаНормальныхУсловиях * (РабочаяТемператураОсновногоЛудла + КЕЛЬВИН) * коэфСжимаемости)
        '    Case РасчетРасхода.РасходGвОтбора
        '        ro = ПлотностьВоздухаНормальныхУсловиях * ТнормУсловия * РабочееДавление / (ДавлениеВоздухаНормальныхУсловиях * (РабочаяТемператураОтбораТрубопровода + КЕЛЬВИН) * коэфСжимаемости)
        '    Case РасчетРасхода.РасходGвОтбораВн
        '        Exit Select
        'End Select

        ro = ПлотностьВоздухаНормальныхУсловиях * ТнормУсловия * P / (ДавлениеВоздухаНормальныхУсловиях * (ТпередСужающимУстройством + КЕЛЬВИН) * коэфСжимаемости)

        ' вычисление коэф. расхода (в инструкции даётся как константа)
        Const ДинамическаяВязкость As Double = 0.00000283 ' 2,83е-6 динамическая вязкость
        Const МаксРасходПриРабочихУсловиях As Double = 12.5 ' 12.5 - макс. расход при рабочих условиях из испытаний КС-99

        Re = (0.0361 * МаксРасходПриРабочихУсловиях * ro) / (Dтруб * 0.001 * ДинамическаяВязкость)
        ' alpha - средний расход сопла
        Dim alpha As Double = 1 / Math.Sqrt(1 - M * M)
        alpha = alpha * (0.99 - 0.2262 * M ^ 2.05 + (0.000215 - 0.001125 * M ^ 0.5 + 0.0249 * M ^ 2.35) * (1000000.0# / Re) ^ 1.15)
        ' окончательный расход
        'было в Салюте dотв = d20отв * (1 + КоэфЛинейногоТепловогоРасширенияМерногоСопла * (ТпередСужающимУстройством - ТцельсияНормУсловия))

        расходИтог = 0.01252 * alpha * E * dотв * dотв / 3600
        ' включить
        расходИтог = расходИтог * Math.Sqrt(dP * P * 10000.0# * ПлотностьВоздухаНормальныхУсловиях * ТнормУсловия / (ДавлениеВоздухаНормальныхУсловиях * (ТпередСужающимУстройством + КЕЛЬВИН) * коэфСжимаемости))
        '***************
        ' отладка все убрать
        'Dim aaa As Double
        'aaa = dP * P * 10000# * ПлотностьВоздухаНормальныхУсловиях * ТнормУсловия / (ДавлениеВоздухаНормальныхУсловиях * (T + conКельвин) * Ks)
        'r = r * Sqr(Abs(aaa))
        '***************
        Return расходИтог
    End Function

    ''' <summary>
    ''' Проверить наличие записей в таблице с именами специфичных для расчёта параметров
    ''' </summary>
    ''' <remarks></remarks>
    Private Function ПроверитьНаличиеЗаписиРасчетныйПараметр(ByVal nameRow As String) As Boolean
        'If dt.Columns.Contains(NameColumn) Then
        '    Return dt.Columns(NameColumn).Ordinal
        'Else
        '    Return -1
        'End If

        For Each rowРасчетныйПараметр As BaseFormDataSet.РасчетныеПараметрыRow In Manager.CalculatedDataTable.Rows
            If rowРасчетныйПараметр.ИмяПараметра = nameRow Then
                Return True
                Exit Function
            End If
        Next

        Return False
    End Function

    'Protected Overrides Sub Finalize()
    '    MyBase.Finalize()
    'End Sub

    'Public Function ТестРасчетаБиблиотеки() As System.Data.DataSet Implements BaseForm.IClassCalculation.ТестРасчетаБиблиотеки
    '    Dim myLinearAlgebra As New LinearAlgebra
    '    With myLinearAlgebra
    '        .matrixADataTextBox = "4.00, 2.00, -1.00; 1.00, 4.00, 1.00; 0.10, 1.00, 2.00;"
    '        .matrixBDataTextBox = "2.00; 12.00; 10.00;"
    '        '.operationsComboBox = Global.MathematicalLibrary.LinearAlgebra.EnumOperationsComboBox.SolveLinearEquations_AxB
    '        .operationsComboBox = LinearAlgebra.EnumOperationsComboBox.SolveLinearEquations_AxB
    '        .Compute()
    '        ТестРасчетаБиблиотеки = .data
    '    End With
    'End Function
End Class

'Private Sub ОновитьИндикаторы()
'    With gMainFomMdiParent.varТемпературныеПоля
'        .TextPosition.Text = gMainFomMdiParent.varEncoder.AnglePosition.ToString("F")
'        .ПроверкаОтсечки(gMainFomMdiParent.varEncoder.Отсечка, gMainFomMdiParent.varEncoder.AnglePosition)

'        .GaugeРасходТоплива.Value = .ПараметрВДиапазоне(CalculatedParam.Gсум_расход_топливаКС_КП_кг_час, CalculatedParameters.G_СУМ_РАСХОД_ТОПЛИВА_КС_КП)
'        .NumericEditРасходТоплива.Value = CalculatedParam.Gсум_расход_топливаКС_КП_кг_час

'        .GaugeРасходВоздухаОтбора.Value = .ПараметрВДиапазоне(CalculatedParam.Gотбора_относительный, CalculatedParameters.G_ОТБОРА_ОТНОСИТЕЛЬНЫЙ)
'        .NumericEditGaugeРасходВоздухаОтбора.Value = CalculatedParam.Gотбора_относительный

'        .MeterАльфаИзбыткаВоздуха.Value = .ПараметрВДиапазоне(CalculatedParam.АльфаКамеры, CalculatedParameters.АЛЬФА_КАМЕРЫ)
'        .NumericEditАльфаИзбыткаВоздух.Value = CalculatedParam.АльфаКамеры

'        .MeterЛямбда.Value = .ПараметрВДиапазоне(CalculatedParam.Лямбда, CalculatedParameters.conЛЯМБДА)
'        .NumericEditЛямбда.Value = CalculatedParam.Лямбда

'        .GaugeРасходВоздуха.Value = .ПараметрВДиапазоне(CalculatedParam.Gвоздуха, CalculatedParameters.G_ВОЗДУХА)
'        .NumericEditРасходВоздуха.Value = CalculatedParam.Gвоздуха

'        .ThermometerTинтегр.Value = .ПараметрВДиапазоне(CalculatedParam.T_интегр, CalculatedParameters.conT_ИНТЕГР)
'        .NumericEditTинтегр.Value = CalculatedParam.T_интегр

'        .ThermometerTгазРасч.Value = .ПараметрВДиапазоне(CalculatedParam.Тг_расчет, CalculatedParameters.Т_Г_РАСЧЕТ)
'        .NumericEditTгазРасч.Value = CalculatedParam.Тг_расчет

'        .SlideКачество.Value = .ПараметрВДиапазоне(CalculatedParam.Качество, CalculatedParameters.conКАЧЕСТВО)
'        .NumericEditКачество.Value = CalculatedParam.Качество

'        .TankСтатическоеДавлениеМерномСопле.Value = .ПараметрВДиапазоне(InputParam.Р310полное_воздуха_на_входе_КС, InputParameters.Р310_ПОЛНОЕ_ВОЗДУХА_НА_ВХОДЕ_КC)
'        .NumericEditСтатическоеДавлениеМерномСопле.Value = InputParam.Р310полное_воздуха_на_входе_КС

'        .ThermometerT309.Value = .ПараметрВДиапазоне(CalculatedParam.ТсредняяГазаНаВходе, CalculatedParameters.Т_СРЕДНЯЯ_ГАЗА_НА_ВХОДЕ)
'        .NumericEditT309.Value = CalculatedParam.ТсредняяГазаНаВходе

'        ' там же заполняется массив y()
'        If gРисоватьГрафикСечений Then .РисоватьПолеПоСечению()
'    End With
'End Sub

'    Private Sub ВычислитьРасчетныеПараметры()
'        'Dim Ps As Double
'        Try
'            ''***********************************
'            ''расчет Расхода воздуха на входе в КС
'            ''***********************************
'            'Рс_ср = (Рс1 + Рс2) / 2
'            'dРс_ср = (dРс1 + dРс2) / 2
'            'Тс = (Тс1 + Тс2) / 2
'            ''Показатель адиабаты
'            'If (Тс) < 50 Then
'            '    k = 1.4
'            'Else
'            '    k = Kadiobaty(Тс)
'            'End If
'            'Алин = (15.6 + 8.3 * Тс * 0.001 - 6.5 * (Тс * 0.001) ^ 2) * 0.000001
'            'Kt = 1 + Алин * (Тс - 20)
'            'Kт_поправка = Kt * Kt
'            't = Abs(1 - dРс_ср / Рс_ср)

'            'e = (k - t ^ (2 / k)) / (k - 1)
'            'e = e * (1 - md * md * md * md) / (1 - md * md * md * md * t ^ (2 / k))
'            'e = e * ((1 - t) ^ ((k - 1) / k)) / (1 - t)
'            'e = Math.Sqrt(e)

'            'Gb = 51.556 * Math.Sqrt(Рс_ср * dРс_ср / (Тс + conКельвин)) * e * Kт_поправка

'            ''***********************************
'            ''расчет среднего расхода топлива камеры сгорания и подогрева
'            ''***********************************

'            ''найти коэф. учитывающий плотность
'            'Ps = clAir.КоэфУчитывающийПлотность(Тт_маг)
'            ''выведем расход в кг час ((Gт1 + Gт2) * Ps = вычисление л/час в кг/час)(Было литрах в час)
'            ''вывести на индикатор суммарный расход топлива в кг/час
'            'GтSum = (Gт1 + Gт2) * Ps + Gт_кп 'Gт1 + Gт2 + Gт_кп
'            'Gt_кс_ср = (Gт1 + Gт2) * Ps / 3600.0#  'перевод л/час в кг/сек камеры сгорания

'            ''Ps = Spline3Interpolate(UBound(TblКоэффициентыПлотностиТоплива, 2) + 1, TblКоэффициентыПлотностиТоплива, Тт_кп)
'            ''Gt_кп = Gт_кп * Ps / 3600.0#  'перевод л/час в кг/сек камеры подогрева
'            'Gt_кп = Gт_кп / 3600.0# 'перевод кг/час в кг/сек камеры подогрева

'            ''***********************************
'            ''расчет коэф. избытка воздуха
'            ''***********************************
'            'Gв_сум = Gb + Gt_кп 'суммарный расход воздуха после камеры сгорания -11
'            'АльфаКамеры = Gв_сум / (СтехиометрическийК * Gt_кс_ср)

'            '***********************************
'            'расчет абсолютного полного давления воздуха на входе КС
'            '***********************************
'            'Рст_абс_ср = Abs((Р311_1 + Р311_2 + Р311_3 + Р311_4 + Р311_5 + Р311_6) / 6) 'барометр уже добавлен
'            'к дифференциальным замерам добавить базовое давление
'            'Dim ИмяПояса As String
'            'For Зонд As Integer = 1 To 3
'            '    For Пояс As Integer = 1 To 5
'            '        ИмяПояса = "dР310-" & Зонд.ToString & "-" & Пояс.ToString
'            '        'перепад скорее всего показывает избыточное, значит в тарировке он с плюсом
'            '        Manager.ИзмеренныеПараметры.FindByИмяПараметра(ИмяПояса).ЗначениеВСИ += Рст_абс_ср
'            '    Next
'            'Next

'            'Call MathematicalLibrary.PlotSurface.PlotSurface(Рст_абс_ср)

'            '***********************************
'            'среднее значение температуры торможения -19
'            '***********************************
'            'T309 = (Т309_1 + Т309_2 + Т309_3 + Т309_4 + Т309_5 + Т309_6) / 6

'            '***********************************
'            'приведенная скорость газового потока -20
'            ''***********************************
'            'Lamda = LamdaFun(Тс, T309, Gb, Fdif, Рст_абс_ср, Рв_вх_абс_полн_ср)

'            With Manager
'                ''по имени параметра strИмяПараметраГрафика определяем нужную функцию приведения
'                ''("n1") 'который измеряет
'                ''должна быть вызвана функция приведения параметра "n1" например
'                'If n1ГПриводить Then
'                '    'должна быть вызвана функция приведения параметра "n1" например
'                '    .РасчетныеПараметры.FindByИмяПараметра(cn1Г).ВычисленноеЗначениеВСИ = Air.funПривестиN(.ИзмеренныеПараметры.FindByИмяПараметра("n1").ЗначениеВСИ, tm)
'                'Else
'                '    'приводить не надо, просто копирование
'                '    .РасчетныеПараметры.FindByИмяПараметра(cn1Г).ВычисленноеЗначениеВСИ = .ИзмеренныеПараметры.FindByИмяПараметра("n1").ЗначениеВСИ
'                'End If

'                ''cGвМПолеДавлений
'                '.РасчетныеПараметры.FindByИмяПараметра(cGвМПолеДавлений).ВычисленноеЗначениеВСИ = funВычислитьGвМПолеДавлений(GвМПолеДавленийПриводить)
'                ''или так
'                ''.РасчетныеПараметры.FindByИмяПараметра(cGвМПолеДавлений).ВычисленноеЗначениеВСИ = funВычислитьGвМПолеДавлений(.НастроечныеПараметры.FindByИмяПараметра("GвМПолеДавленийПриводить").ЛогическоеЗначение)

'                ''cGвМПито
'                '.РасчетныеПараметры.FindByИмяПараметра(cGвМПито).ВычисленноеЗначениеВСИ = funВычислитьGвМПито(GвМПитоПриводить)

'                ''cПиК
'                '.РасчетныеПараметры.FindByИмяПараметра(cПиК).ВычисленноеЗначениеВСИ = funВычислитьПиК()

'                ''cКПДадиабат
'                '.РасчетныеПараметры.FindByИмяПараметра(cКПДадиабат).ВычисленноеЗначениеВСИ = funВычислитьКПДадиабат()

'                ''cnИГ_03Г
'                ''который измеряет
'                'nИГ_03 = .ИзмеренныеПараметры.FindByИмяПараметра("nИГ-03").ЗначениеВСИ / 46325 'коэф. перевода  n=1 при N=45190

'                'If nИГ_03ГПриводить Then
'                '    'должна быть вызвана функция приведения параметра "n1" например
'                '    .РасчетныеПараметры.FindByИмяПараметра(cnИГ_03Г).ВычисленноеЗначениеВСИ = Air.funПривестиN(nИГ_03, tm)
'                'Else
'                '    'приводить не надо, просто копирование
'                '    .РасчетныеПараметры.FindByИмяПараметра(cnИГ_03Г).ВычисленноеЗначениеВСИ = nИГ_03
'                'End If

'                ''cnПиК_GвМПД
'                ''должна быть вызвана функция вычисления параметра "GПиК/GвМПД" например
'                '.РасчетныеПараметры.FindByИмяПараметра(cnПиК_GвМПД).ВычисленноеЗначениеВСИ = funВычислитьПиК_GвМПД()
'            End With

'            '*********************************************

'            'If blnИзмерениеПоТемпературам Then
'            '***********************************
'            'расчет интегральной температура газа по мерному сечению на поясе
'            'внимание этот расчет для равномерного положения термопар относительно друг друга
'            '**********************************
'            Dim Ystart As Double, Yend As Double
'            Call НайтиЗначенияТемпературНаСтенках(КоординатыТермопар, arrТекущаяПоПоясам, ШиринаМерногоУчастка, Ystart, Yend)
'            y(0) = Ystart
'            y(ЧислоТермопар + 1) = Yend

'            T_интегр = ИнтегрированиеРадиальнойЭпюрыНаПроизвольныхКоординатах(КоординатыТермопар, arrТекущаяПоПоясам, ШиринаМерногоУчастка)

'            '***********************************
'            'расчет Качество 
'            '**********************************
'            mКачество = КачествоFun(T_интегр, Тсредняя_газа_на_входе, Тг_расчет)

'            'после замены на координатнике термопар ттемпература на выходе измеряется как средняя из 6
'            'Т3г_сред = (T3_1 + T3_2 + T3_3 + T3_4 + T3_5 + T3_6) / 6

'            '0.3964 коэф. от адиабаты
'            'gFun_Lamda = Gb * Math.Sqrt(T309 + conКельвин) / (0.3964 * Рв_вх_абс_полн_ср * Fk)


'        Catch ex As Exception
'            Description = "Процедура: ВычислитьРасчетныеПараметры"
'            'перенаправление встроенной ошибки
'            Dim fireDataErrorEventArgs As New IClassCalculation.DataErrorEventArgs(ex.Message, Description)
'            '  Теперь вызов события с помощью вызова делегата. Проходя в
'            '   object которое инициирует  событие (Me) такое же как FireEventArgs. 
'            '  Вызов обязан соответствовать сигнатуре FireEventHandler.
'            RaiseEvent DataError(Me, fireDataErrorEventArgs)
'            mФормаРодителя.varТемпературныеПоля.tsTextBoxОшибкаРасчета.Visible = True

'        End Try
'        mФормаРодителя.varТемпературныеПоля.tsTextBoxОшибкаРасчета.Visible = CBool(Тг_расчет = 0 - conКельвин)
'    End Sub

'Private action As System.Action(Of FileInfo)
'Private match As System.Predicate(Of FileInfo)
'Private fileList As New List(Of FileInfo)
'Private fileArray() As FileInfo

'Private Sub outputWindowRadioButton_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles outputWindowRadioButton.CheckedChanged
'    action = New System.Action(Of FileInfo) _
'     (AddressOf DisplayInOutputWindow)
'End Sub

'Private Sub forEachListButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles forEachListButton.Click
'    ResetListBox()
'    fileList.ForEach(action)
'End Sub

'Private Sub DisplayInOutputWindow(ByVal file As FileInfo)
'    Debug.WriteLine(String.Format("{0} ({1} bytes)", _
'     file.Name, file.Length))
'End Sub

'Private Sub smallFilesRadioButton_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles smallFilesRadioButton.CheckedChanged
'    match = New System.Predicate(Of FileInfo) _
'     (AddressOf IsSmall)
'End Sub
'Private Sub findAllListButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles findAllListButton.Click
'    ResetListBox()

'    ' Create a list containing matching files,
'    ' and then take the appropriate action.
'    Dim subList As List(Of FileInfo) = fileList.FindAll(match)
'    subList.ForEach(action)
'End Sub

'Private Function IsSmall( _
'ByVal file As FileInfo) As Boolean

'    ' Return True if the file's length is less than 500 bytes.
'    Return file.Length < 500
'End Function



''имена промежуточных переменных и номер формулы
'Private Рс_ср As Double ' давление воздуха перед соплом среднее
'Private dРс_ср As Double ' перепад давления воздуха перед соплом среднее
'Private Тс As Double 'температура воздуха в на входе в сопло
'Private Kt As Double ' коэф. учитывающий расширение материалов
'Private Kт_поправка As Double 'поправка на температурное расширение материалов
'Private t As Double '1-dРс_ср/Рс_ср
'Private e As Double 'коэф. сжимаемости
'Private Алин As Double 'коэф. линейного расширения материала
'Private Gt_кп As Double 'расход камеры подогрева
''Private ИнтегрированиеПоЭпюре As Double 'временный результат интегрирования по эпюре
'Private КоэфПотерьПолнДавления As Double
'Private КоэфГидравлСопр As Double
'Private КоэфВосстановлПолнДавл As Double
'Private T309 As Double 'среднее значение температуры торможения -19


'переменные подсчета
'сонстанты имен параметров для подсчета




'Private Const конРс1 As String = "Рс1" '	Статическое давление на входе в мерное сопло
'Private Рс1 As Double
'Private Const конРс2 As String = "Рс2" '	Статическое давление на входе в мерное сопло
'Private Рс2 As Double
'Private Const конdРс1 As String = "dРс1" '	Перепад давления в мерном сопле
'Private dРс1 As Double
'Private Const конdРс2 As String = "dРс2" '	Перепад давления в мерном сопле
'Private dРс2 As Double
'Private Const конТс1 As String = "Тс1" '	Темпеатура воздуха в мерном сопле
'Private Тс1 As Double
'Private Const конТс2 As String = "Тс2" '	Темпеатура воздуха в мерном сопле
'Private Тс2 As Double



'Private Const конGт1 As String = "Gт1" '	Расход топлива 1 ступени коллектора
'Private Gт1 As Double
'Private Const конGт2 As String = "Gт2" '	Расход топлива 2 ступени коллектора
'Private Gт2 As Double
'Private Const конGт_кп As String = "Gт кп" '	Расход топлива в камере подогрева
'Private GтSum As Double 'суммарный расход топлива в кг/час


'Private Gт_кп As Double
'Private Const конТт_кп As String = "Тт кп" '	Температура топлива в камере подогрева
'Private Тт_кп As Double
'Private Const конТт_маг As String = "Тт маг" '	Температура топлива магистрали
'Private Тт_маг As Double

'Private Const конПоложениеГребенки As String = "ПоложениеГребенки" '	Положение зондов замеров давлений и температур

'константы имен расчётных параметров и номер формулы
'Private Const конGb As String = "Gb" 'Расход воздуха на входе в КС
'Private Gb As Double

'Private Const конРв_вх_абс_полн_ср As String = "Рв_вх_пол" 'абсолютное полное среднее давление воздуха на входе (интегрированное)
'Private Рв_вх_абс_полн_ср As Double

'Private Const конРст_абс_ср As String = "Рв_вх_стат" 'Абсолютное статическое давление воздуха на входе
'Private Рст_абс_ср As Double 'абсолютное статическое давление -16



'Private Const конGt_кс_ср As String = "Gt_кс_ср" 'средний расход камеры сгорания
'Private Gt_кс_ср As Double

'Private Const конПотериПолнДавления As String = "dP_ПотПолнДавл" 'потери полного давления - 24
'Private ПотериПолнДавления As Double

'Private Const конТ3г_сред As String = "Т3г_сред" 'после замены на координатнике термопар температура на выходе измеряется как средняя из 6
'Private Т3г_сред As Double

'Private Const конв_сум As String = "Gв_сум" 'суммарный расход воздуха после камеры сгорания -11
'Private Gв_сум As Double

'Private Const конgFun_Lamda As String = "g(Lamda)"
'Private gFun_Lamda As Double


'константы вычисленных имен переменных
'Private Const cGвМПолеДавлений As String = "GвМПолеДавлений"
'Private Const cGвМПито As String = "GвМПито"
'Private Const cПиК As String = "ПиК"
'Private Const cКПДадиабат As String = "КПДадиабат"
'Private Const cn1Г As String = "n1Г"
'Private Const cnИГ_03Г As String = "nИГ-03Г"
'Private Const cnПиК_GвМПД As String = "ПиК/GвМПД"
''****************************************************
'для хранения настроечных значений
'Private GвМПитоПриводить As Boolean
'Private GвМПолеДавленийПриводить As Boolean
'Private n1ГПриводить As Boolean
'Private nИГ_03ГПриводить As Boolean
'Private Const Dтр As Double = 250 'мм сопло
'Private Const dc As Double = 89.57 'мм шайба
'Private Const md As Double = dc / Dтр
'Private Const Fk As Double = 75.6 'площадь сечения см.кв.
