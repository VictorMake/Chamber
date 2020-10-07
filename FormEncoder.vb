Imports System.Drawing
Imports System.Threading
Imports System.Threading.Tasks
Imports System.Windows.Forms
Imports BaseForm
Imports I7565CPM_Net
Imports MathematicalLibrary

Public Class FormEncoder
    ' 1 инициализация параметров приложения
    ' 2 инициализация параметров CANopen          <-
    ' 3 активация модуля CAN и прерываний           |
    ' 4 асинхронныая обработка данных CANopen <-    |
    ' бесконечный цикл                  --------| --|
    ' сброс соединения
    ' 5 завершение программы, удаление  CANopen
    ''' <summary>
    ''' виртуальный порт - считывается из настроек
    ''' </summary>
    Private ComPortI7565CPM As Integer = 5
    ''' <summary>
    ''' скорость шины 250 kbps, - считывается из настроек
    ''' </summary>
    Private BaudI7565CPM As Integer = 250
    ''' <summary>
    ''' индекс устройства - считывается из настроек
    ''' </summary>
    Private NodeI7565CPM As Integer = 63
    ''' <summary>
    ''' номер Com потра I7565CPM
    ''' </summary>
    Private COMPORT As Byte
    ''' <summary>
    ''' номер ведомого узла энкодера
    ''' </summary>
    Private NODE As Byte
    ''' <summary>
    ''' индекс скорость обмена по шине CAN
    ''' </summary>
    Private BAUD As Byte
    ''' <summary>
    ''' параметр минимального временного интервала между отправками сообщений
    ''' </summary>
    Private DelayTime As UShort
    ''' <summary>
    ''' шифр состояния (Network Management) ведомого узла
    ''' </summary>
    Private STA As Byte
    ''' <summary>
    ''' текстовое состояния (Network Management) ведомого узла
    ''' </summary>
    Private STA_Str As String = ""
    ''' <summary>
    ''' синхронный режим
    ''' </summary>
    Private Const BlockMode As Byte = 1
    ''' <summary>
    ''' код возврата ошибок процедур I7565CPM
    ''' </summary>
    Private ret As UShort
    ''' <summary>
    ''' лист TxPDO COB-ID
    ''' </summary>
    Dim Id_List(9) As UShort
    ''' <summary>
    ''' число инсталлированных TxPDO объектов
    ''' </summary>
    Dim PDO_Cnt As Byte
    ''' <summary>
    ''' временный текст сторожевых протоколов
    ''' </summary>
    Private NMTErr As String = ""
    ''' <summary>
    ''' временный текст срочных сообщений
    ''' </summary>
    Private EMCY As String = ""
    ''' <summary>
    ''' делегат обработки сторожевых протоколов
    ''' </summary>
    Private MyNMTErrISR As I7565CPM.NMTErrISR
    ''' <summary>
    ''' делегат обработки срочных сообщений
    ''' </summary>
    Private MyEMCYISR As I7565CPM.EMCYISR
    ''' <summary>
    ''' таймер индикатора нового угла
    ''' </summary>
    Private TimerLedShutoff As Timers.Timer

    Private mIsEncoderLoadedAndRunSuccess As Boolean

    ''' <summary>
    ''' Флаг успешной загрузки/выгрузки энкодера.
    ''' Управление доступностью кнопки запуска земера поля.
    ''' </summary>
    Public Property IsEncoderLoadedAndRunSuccess() As Boolean
        Get
            Return mIsEncoderLoadedAndRunSuccess
        End Get
        Set(ByVal value As Boolean)
            mIsEncoderLoadedAndRunSuccess = value

            If gMainFomMdiParent IsNot Nothing AndAlso Not gMainFomMdiParent.IsWindowClosed Then
                gMainFomMdiParent.varТемпературныеПоля.ButtonRunMeasurement.Enabled = value
            End If
        End Set
    End Property

    Private Sub EncoderForm_Load(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Load
        ' создать и настроить таймер индикатора нового угла
        TimerLedShutoff = New Timers.Timer() With {.Interval = TimeShowShutoff}
        AddHandler TimerLedShutoff.Elapsed, AddressOf OnTimedEvent
        TimerLedShutoff.AutoReset = False ' только однократное срабатывание

        ' Настроить интерфейс пользователя
        LoadSettingTree()
        isCreatedForm = True
        CreateMyListView()
    End Sub

    Private Sub EncoderForm_FormClosing(sender As Object, e As FormClosingEventArgs) Handles Me.FormClosing
        If Not gMainFomMdiParent.IsWindowClosed Then
            e.Cancel = True
        End If
        isCreatedForm = False
    End Sub

    Private Sub EncoderForm_Closed(ByVal sender As Object, ByVal e As EventArgs) Handles MyBase.Closed
        ' Функция I7565CPM_ShutdownMaster удаляет всех ведомых, которые были добавлены в Мастер и остановить все функции в I-7565-CPM.
        ' Функция должна быть вызвана перед выходом прикладных программ пользователей.
        'I7565CPM.I7565CPM_ShutdownMaster(COMPORT)
        If ButtonInitial.Enabled = False Then Reset()
    End Sub

#Region "Explorer"
    ''' <summary>
    ''' Заполнить узлы дерева настроек
    ''' </summary>
    Private Sub LoadSettingTree()
        Dim tvRoot As TreeNode

        TreeView.BeginUpdate()
        tvRoot = Me.TreeView.Nodes.Add("Settings")

        For Each itemTabPage As TabPage In TabControlSetting.TabPages
            tvRoot.Nodes.Add(itemTabPage.Text, itemTabPage.Text, 0, 1)
            tvRoot.Nodes(tvRoot.Nodes.Count - 1).ToolTipText = itemTabPage.ToolTipText
        Next

        tvRoot.ExpandAll()
        TreeView.EndUpdate()
    End Sub

    Private Sub TreeView_AfterSelect(sender As Object, e As TreeViewEventArgs) Handles TreeView.AfterSelect
        For Each itemTabPage As TabPage In TabControlSetting.TabPages
            If itemTabPage.Text = CType(sender, TreeView).SelectedNode.Text Then
                TabControlSetting.SelectedTab = itemTabPage
                Exit For
            End If
        Next
    End Sub

#End Region

#Region "ListViewAlarm"
    ''' <summary>
    ''' Для предотвращения преждевременной обработки событий
    ''' </summary>
    Private isCreatedForm As Boolean
    Private Const NUMBER_COLUMN As String = "Номер"
    Private Const MESSAGE_COLUMN As String = "Сообщение"
    Private Const DATE_COLUMN As String = "Дата"
    Private Const TIME_COLUMN As String = "Время"

    Private Enum ColorForAlarm
        ConditionUp
        watchdogSet
        WriteOk
        ReadOk
        CriterionUp
        ReadOk2
        InformationMessage
        AlarmMessage
    End Enum

    ''' <summary>
    ''' Настройка ListView.
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub CreateMyListView()
        ' Установить вид просмотра по умолчанию.
        ListViewAlarm.View = View.Details
        ' Разрешить пользователю редактировать текст.
        'ListViewAlaarm.LabelEdit = True
        ' Разрешить пользователю реорганизовывать колонки.
        ListViewAlarm.AllowColumnReorder = True
        ' Показать check boxes.
        'ListViewAlaarm.CheckBoxes = True
        ' Выделять item и subitems в режиме выделения.
        'ListViewAlaarm.FullRowSelect = True
        ' Показать сетку линий.
        ListViewAlarm.GridLines = True
        ' Сортировать элементы листа по возрастанию.
        'ListViewAlaarm.Sorting = SortOrder.Ascending

        ' Назначить ImageList для ListView.
        ListViewAlarm.LargeImageList = ImageList1
        ListViewAlarm.SmallImageList = ImageList1

        ' Создать колонки для элементов и подэлементов.
        ' При установке в -2 индикация в auto-size.
        ListViewAlarm.Columns.Add(NUMBER_COLUMN, NUMBER_COLUMN, 90, HorizontalAlignment.Center, NUMBER_COLUMN)
        ListViewAlarm.Columns.Add(MESSAGE_COLUMN, MESSAGE_COLUMN, -2, HorizontalAlignment.Left, MESSAGE_COLUMN)
        ListViewAlarm.Columns.Add(DATE_COLUMN, DATE_COLUMN, 90, HorizontalAlignment.Center, DATE_COLUMN)
        ListViewAlarm.Columns.Add(TIME_COLUMN, TIME_COLUMN, 90, HorizontalAlignment.Center, TIME_COLUMN)

        ' Добавить элементы в ListView.
        'ListViewAlarm.Items.AddRange(New ListViewItem() {item1, item2, item3})
        'AddToListView("AlertUp", ColorForAlarm.AlertUp)

        ' Создать 2 ImageList программно.
        'Dim imageListSmall As New ImageList()
        'Dim imageListLarge As New ImageList()

        ' Инициализировать ImageList из bitmaps.
        'imageListSmall.Images.Add(Bitmap.FromFile("C:\MySmallImage1.bmp"))
        'imageListSmall.Images.Add(Bitmap.FromFile("C:\MySmallImage2.bmp"))

        ' Добавить ListView коллекцию контролов.
        'Controls.Add(ListViewAlaarm)
    End Sub

    Private Sub ListViewAlarm_Resize(sender As Object, e As EventArgs) Handles ListViewAlarm.Resize
        If Me.IsHandleCreated AndAlso isCreatedForm Then
            If ListViewAlarm.Columns.Count > 0 Then
                ListViewAlarm.Columns(NUMBER_COLUMN).Width = 90
                ListViewAlarm.Columns(DATE_COLUMN).Width = 90
                ListViewAlarm.Columns(TIME_COLUMN).Width = 90
                ListViewAlarm.Columns(MESSAGE_COLUMN).Width = ListViewAlarm.Width - (ListViewAlarm.Columns(NUMBER_COLUMN).Width + ListViewAlarm.Columns(DATE_COLUMN).Width + ListViewAlarm.Columns(TIME_COLUMN).Width)
            End If
        End If
    End Sub

    ''' <summary>
    ''' Возвращает значение Color в зависимости от типа загрузки.
    ''' </summary>
    Private Shared Sub GetAlarmColor(ByRef subItem As ListViewItem.ListViewSubItem, ByVal alarm As ColorForAlarm)
        subItem.Font = New Font("Arial", 10, FontStyle.Bold) 'System.Drawing.FontStyle.Italic)

        Select Case alarm
            Case ColorForAlarm.watchdogSet, ColorForAlarm.InformationMessage
                subItem.ForeColor = Color.Blue
                subItem.BackColor = Color.White
                subItem.Font = New Font("Arial", 8, FontStyle.Regular)
                Exit Select
            Case ColorForAlarm.ConditionUp
                subItem.ForeColor = Color.Blue
                subItem.BackColor = Color.LightSteelBlue
                subItem.Font = New Font("Arial", 8, FontStyle.Regular)
                Exit Select
            Case ColorForAlarm.ReadOk2
                subItem.ForeColor = Color.Green
                subItem.BackColor = Color.LightGreen
                Exit Select
            Case ColorForAlarm.WriteOk
                subItem.ForeColor = Color.Olive
                subItem.BackColor = Color.PaleGoldenrod
                Exit Select
            Case ColorForAlarm.CriterionUp, ColorForAlarm.AlarmMessage
                subItem.ForeColor = Color.Black
                subItem.BackColor = Color.OrangeRed
                Exit Select
            Case ColorForAlarm.ReadOk
                subItem.ForeColor = Color.Red
                subItem.BackColor = Color.MistyRose
                Exit Select
        End Select
    End Sub

    Const LIMIT_LIST_COUNT As Integer = 200
    ''' <summary>
    ''' Эта процедура добавляет новое сообщение 
    ''' в ListView и задает текст, размер и цвет.
    ''' </summary>
    Private Sub AddToListView(ByVal message As String, ByVal alarm As ColorForAlarm)
        'If InvokeRequired Then
        '    Invoke(New MethodInvoker(Sub() AddToListView(message, alarm)))
        'Else

        'Dim item1 As New ListViewItem("item1", 0)
        ''item1.Checked = True
        'item1.SubItems.Add("1")

        'ListViewAlarm.Items(0).UseItemStyleForSubItems = False
        'ListViewAlarm.Items(0).SubItems.Add("Авария", Color.Pink, Color.Yellow, Font)

        ListViewAlarm.BeginUpdate()

        Do While ListViewAlarm.Items.Count > LIMIT_LIST_COUNT
            ListViewAlarm.Items.RemoveAt(0)
        Loop

        Dim listViewItem As New ListViewItem()
        Dim listViewSubItemДата As ListViewItem.ListViewSubItem = New ListViewItem.ListViewSubItem() With {.Text = Date.Now.ToShortDateString} '= listViewItem.SubItems.Add("1")
        Dim listViewSubItemВремя As ListViewItem.ListViewSubItem = New ListViewItem.ListViewSubItem() With {.Text = Date.Now.ToLongTimeString}
        Dim listViewSubItemСообщение As ListViewItem.ListViewSubItem = New ListViewItem.ListViewSubItem() With {.Text = message}

        listViewItem.ImageKey = alarm.ToString
        listViewItem.UseItemStyleForSubItems = False

        If ListViewAlarm.Items.Count = 0 Then
            listViewItem.Text = "1"
        Else
            listViewItem.Text = CType(Integer.Parse(ListViewAlarm.Items(ListViewAlarm.Items.Count - 1).Text) + 1, String)
        End If

        'listViewItem.BackColor = GetBackColor(ColorForAlarm.Normal)
        GetAlarmColor(listViewSubItemДата, ColorForAlarm.watchdogSet)
        GetAlarmColor(listViewSubItemВремя, ColorForAlarm.watchdogSet)
        GetAlarmColor(listViewSubItemСообщение, alarm)

        listViewItem.SubItems.Add(listViewSubItemСообщение)
        listViewItem.SubItems.Add(listViewSubItemДата)
        listViewItem.SubItems.Add(listViewSubItemВремя)
        ListViewAlarm.Items.Add(listViewItem)
        listViewItem.EnsureVisible()
        ListViewAlarm.EndUpdate()

        Do While ListSDODataRead.Items.Count > LIMIT_LIST_COUNT
            ListSDODataRead.Items.RemoveAt(0)
        Loop
        ListSDODataRead.SelectedIndex = ListSDODataRead.Items.Count - 1
        'End If
    End Sub

    ''' <summary>
    ''' Вывести сообщение в панели статуса.
    ''' </summary>
    ''' <param name="message"></param>
    ''' <remarks></remarks>
    Private Sub ShowMessageToPanel(ByVal message As String)
        LabelStatus.Text = message
        'Const CAPTION As String = "Сообщение в Окне Проигрывателя Циклограмм"
        'RegistrationEventLog.EventLog_MSG_USER_ACTION(String.Format("<{0}> {1}", CAPTION, message))
    End Sub

    ''' <summary>
    ''' Окрашивание индикаторов в различные цвета в зависимости от уровня
    ''' вывод предупреждений в лист сообщений.
    ''' Поддержка вывода из другого потока.
    ''' </summary>
    ''' <param name="message"></param>
    ''' <param name="stateColor"></param>
    ''' <remarks></remarks>
    Private Sub AddLogMessage(message As String, ByVal stateColor As ColorForAlarm)
        If InvokeRequired Then
            Invoke(New MethodInvoker(Sub() AddLogMessage(message, stateColor)))
        Else
            AddToListView(message, stateColor)
            ShowMessageToPanel(message)
        End If
    End Sub

    ' ''' <summary>
    ' ''' Очистить панель статуса.
    ' ''' </summary>
    ' ''' <remarks></remarks>
    'Private Sub ClearPanel()
    '    tsStatus.Text = ""
    'End Sub

#End Region

#Region "LoadEncoder"
    ''' <summary>
    ''' Применение настроек из базы данных настроечных параметров
    ''' </summary>
    Public Sub GetConfigSetting()
        With gMainFomMdiParent.Manager.TuningDataTable
            ComPortI7565CPM = Convert.ToInt32(.FindByИмяПараметра(TuningParameters.COM_PORT_I7565CPM).ЦифровоеЗначение)
            BaudI7565CPM = Convert.ToInt32(.FindByИмяПараметра(TuningParameters.BAUND_I7565CPM).ЦифровоеЗначение)
            NodeI7565CPM = Convert.ToInt32(.FindByИмяПараметра(TuningParameters.NODE_I7565CPMУ).ЦифровоеЗначение)
            Kreduction = .FindByИмяПараметра(TuningParameters.K_REDUCTION).ЦифровоеЗначение
            IsClockwise = .FindByИмяПараметра(TuningParameters.CLOCK_WISE).ЛогическоеЗначение
            SetPredictedAngle(.FindByИмяПараметра(TuningParameters.УГОЛ_УПРЕЖДЕНИЯ_ТУРЕЛИ_ДО_НУЛЯ).ЦифровоеЗначение)
        End With
    End Sub

    ''' <summary>
    ''' Полная загрузка энкодера в соответствии с конфигурационными настройками
    ''' </summary>
    Public Sub LoadEncoder()
        IsEncoderLoadedAndRunSuccess = False
        COMPORT = 0
        BAUD = 0
        NODE = 1

        ComboCom.Enabled = True
        ComboBaud.Enabled = True
        ComboNode.Enabled = False

        SelectCombo()

        ComboLen.SelectedIndex = 0
        ComboState.SelectedIndex = 0
        STA = &H1
        STA_Str = "Operation mode"

        ButtonAddNode.Enabled = False
        ButtonAddNode.Visible = True
        ButtonRemove.Enabled = False
        ButtonInitial.Enabled = True
        ButtonShutdown.Enabled = False

        EnableDisableContrtol(False)

        ' Добавить пользовательский обработчик события остслеживания ошибок сторожевое устройство/тактовый импульс  (Guarding/Heartbeat) процесса
        MyNMTErrISR = New I7565CPM.NMTErrISR(AddressOf UserNMTErrISR)
        ' Добавить пользовательский обработчик события остслеживания внутренних ошибок
        MyEMCYISR = New I7565CPM.EMCYISR(AddressOf UserEMCYISR)

        SetParameters()
        EnableDisableContrtol(True)
    End Sub

    ''' <summary>
    ''' Выделить в списке предопределённый параметр
    ''' </summary>
    Private Sub SelectCombo()
        Dim selectedIndex As Integer
        ' сбросить первоначальные уставки
        ComboCom.SelectedIndex = 0
        ComboBaud.SelectedIndex = 0
        ComboNode.SelectedIndex = 0
        ' повторно выделить для инициализации значения
        ComboCom.SelectedIndex = ComPortI7565CPM

        For Each itemBaud In ComboBaud.Items
            If itemBaud.ToString.Contains(BaudI7565CPM.ToString) Then
                ComboBaud.SelectedIndex = selectedIndex ' = 4 '250 kbps
                Exit For
            End If

            selectedIndex += 1
        Next

        selectedIndex = 0

        For Each itemBaud In ComboNode.Items
            If itemBaud.ToString.Contains(NodeI7565CPM.ToString) Then
                ComboNode.SelectedIndex = selectedIndex ' = 62 ' на самом деле устройство с номером 63 
                Exit For
            End If

            selectedIndex += 1
        Next
    End Sub

    ''' <summary>
    ''' Установка параметров энкодера по умолчанию
    ''' </summary>
    Private Sub SetParameters()
        InitializeMaster()
        Thread.Sleep(100)
        If ret <> 0 Then Exit Sub

        AddNode()
        Thread.Sleep(100)
        If ret <> 0 Then Exit Sub

        SetCANopenOperationMode()
        Thread.Sleep(100)
        If ret <> 0 Then Exit Sub

        SetMeasuringUnitsPerRevolution()
        If ret <> 0 Then Exit Sub

        'МаксимальноеФизическоеРазрешение()
        ResetPositionEncoder()
        If ret <> 0 Then Exit Sub

        SetNMTHeartbeat()
        Thread.Sleep(100)
        'If ret <> 0 Then Exit Sub
    End Sub
#End Region

#Region "CommandsSetting"
    ''' <summary>
    ''' Перевести код в разумное сообщение
    ''' </summary>
    ''' <param name="kod"></param>
    ''' <returns></returns>
    Private Function TranslateReturnCode(kod As Integer) As String
        Dim description As String = String.Empty
        Dim errorID As String = String.Empty

        Select Case kod
            Case 0
                errorID = "CPM_NoError" : description = "OK "
                Exit Select
            Case 2
                errorID = "CPM_OpenComErr" : description = "Ошибка открытия виртуального USB COM-порт"
                Exit Select
            Case 3
                errorID = "CPM_ComPortErr" : description = "Устройство I-7565-CPM не подключено к COM-порту"
                Exit Select
            Case 4
                errorID = "CPM_MasterFull" : description = "Устройство I-7565-CPM поддерживает максимум 8 ведущих узлов"
                Exit Select
            Case 5
                errorID = "CPM_ConfigErr" : description = "The I-8123W hasn't been configured successfully"
                Exit Select
            Case 6
                errorID = "CPM_MasterInitErr" : description = "The I-8123W initialization Error"
                Exit Select
            Case 7
                errorID = "CPM_MasterNotInit" : description = "The I-8123W hasn't been initialized"
                Exit Select
            Case 8
                errorID = "CPM_ListenMode" : description = "The I-8123W Is In listen mode now"
                Exit Select
            Case 9
                errorID = "CPM_NodeErr" : description = "Ошибка установки номера узла"
                Exit Select
            Case 10
                errorID = "CPM_NodeExist" : description = "Узел уже был добавлен к Master"
                Exit Select
            Case 11
                errorID = "CPM_AddModeErr" : description = "Параметр режима функции AddNode неправильный"
                Exit Select
            Case 12
                errorID = "CPM_TxBusy" : description = "Tx буфер переполнен, подождите минуту и отправьте команду заново"
                Exit Select
            Case 13
                errorID = "CPM_UnknowCmd" : description = "Текущая версия прошивки не поддерживает данную функцию"
                Exit Select
            Case 14
                errorID = "CPM_CmdReceErr" : description = "I-8123W receive command Of wrong length"
                Exit Select
            Case 15
                errorID = "CPM_DataEmpty" : description = "Нет полученных данных"
                Exit Select
            Case 16
                errorID = "CPM_MemAllocErr" : description = "I-8123W has Not enough memory"
                Exit Select
            Case 17
                errorID = "CPM_SendCycMsgErr" : description = "Циклическая отправка сообщения ошибки"
                Exit Select
            Case 18
                errorID = "CPM_StatusErr" : description = "NMT состояние CANopen ведомого выдает ошибку"
                Exit Select
            Case 20
                errorID = "CPM_SetGuardErr" : description = "Установка параметра караульного узла и временного фактора выдает ошибку"
                Exit Select
            Case 21
                errorID = "CPM_SetHbeatErr" : description = "Установка параметра отслеживания тактовых импульсов выдает ошибку"
                Exit Select
            Case 22
                errorID = "CPM_SegLenErr" : description = "SDO Segment получена ошибка длины "
                Exit Select
            Case 23
                errorID = "CPM_SegToggleErr" : description = "SDO Segment получена ошибка переключения"
                Exit Select
            Case 24
                errorID = "CPM_SegWriteErr" : description = "SDO Segment ошибка записи сегмента"
                Exit Select
            Case 25
                errorID = "CPM_Abort" : description = "Ответное сообщение представляет собой сообщение Abort"
                Exit Select
            Case 26
                errorID = "CPM_PDOLenErr" : description = "PDO ошибка выходных данных"
                Exit Select
            Case 27
                errorID = "CPM_COBIDErr" : description = "COB-ID не существует или неправильный"
                Exit Select
            Case 28
                errorID = "CPM_PDOInstErr" : description = "Инсталлирование PDO объекта неправильно"
                Exit Select
            Case 29
                errorID = "CPM_PDODynaErr" : description = "Ошибка установки PDO Сопоставление данных"
                Exit Select
            Case 30
                errorID = "CPM_PDONumErr" : description = "Номер PDO и COB-ID не совпадают"
                Exit Select
            Case 31
                errorID = "CPM_PDOSetErr" : description = "Ошибка установки параметра PDO"
                Exit Select
            Case 32
                errorID = "CPM_PDOEntryErr" : description = "Внутренняя PDO запись параметра более полезна"
                Exit Select
            Case 33
                errorID = "CPM_SetCobIdErr" : description = "Ошибка установки MCY или SYNC COB-ID"
                Exit Select
            Case 34
                errorID = "CPM_CycFullErr" : description = "Более 5 циклических сообщений"
                Exit Select
            Case 35
                errorID = "CPM_Timeout" : description = "Истекло время ожидания ответного сообщения"
                Exit Select
            Case 36
                errorID = "CPM_DataLenErr" : description = "Ошибка установки длины данных"
                Exit Select
            Case 38
                errorID = "CPM_SendLose" : description = "Потеряна строка, отправленная в COM-port"
                Exit Select
            Case 39
                errorID = "CPM_SendCmdErr" : description = "Ошибка отправки команды в COM-port"
                Exit Select
            Case 40
                errorID = "CPM_Wait" : description = "Команда незакончена (только для non-block режима)"
                Exit Select
            Case 41
                errorID = "CPM_Processing" : description = "Команда в процессе (только для non-block режима)"
                Exit Select
            Case 50
                errorID = "CPM_LoadEDSErr" : description = "Ошибка загрузки EDS файла"
                Exit Select
            Case 51
                errorID = "CPM_EDSFormatErr" : description = "Некорректный формат EDS файла"
                Exit Select

            Case Else
                errorID = "???" : description = "Ошибка не определена"
        End Select

        If kod <> 25 Then Reset()

        Return $"kod={kod}; errorID={errorID}; description={description}"
    End Function

    ''' <summary>
    ''' предотвратить повторный вызов Reset
    ''' </summary>
    Private isResetting As Boolean

    ''' <summary>
    ''' Сброс адаптера I7565CPM
    ''' </summary>
    Private Sub Reset()
        If isResetting Then Exit Sub

        isResetting = True
        StopTask()
        RemoveNode() ' может быть ошибка, если USB выключен
        Thread.Sleep(100)

        If ret = 0 Then
            ShutdownMaster() ' может быть ошибка, если USB выключен
            Thread.Sleep(100)
        End If

        ' подстраховаться и скрыть контролы
        HideControlsShutdownMaster()
        isResetting = False
    End Sub

    ''' <summary>
    ''' Включение/выключение доступности кнопок и вкладок
    ''' </summary>
    ''' <param name="enabled"></param>
    Private Sub EnableDisableContrtol(enabled As Boolean)
        ButtonEncoder.Enabled = Not enabled
        ButtonResetPosition.Enabled = enabled
        ButtonGetPosition.Enabled = enabled

        TabPageSDO_Write.Enabled = enabled
        TabPageSDORead.Enabled = enabled
        TabPageNMT_Protocol.Enabled = enabled
    End Sub

    ''' <summary>
    ''' Установка ведомого узла в состояние Operation mode
    ''' </summary>
    Private Sub SetCANopenOperationMode()
        ComboState.SelectedIndex = 0 ' "Operation mode"
        NMTChangeState()
    End Sub

    ''' <summary>
    ''' Установка разрешения энкодера
    ''' </summary>
    Private Sub SetMeasuringUnitsPerRevolution()
        TextIndexWrite.Text = "6001"
        TextSubIndexWrite.Text = "00"
        ComboLen.SelectedIndex = 3 ' длина равна 4
        TextD0.Text = "FF"
        TextD1.Text = "FF"
        TextD2.Text = "00"
        TextD3.Text = "00"
        SDOWriteData()
        Thread.Sleep(100)
    End Sub

    ''' <summary>
    ''' Сброс позиции энкодера
    ''' </summary>
    Public Sub ResetPositionEncoder()
        TextIndexWrite.Text = "6003"
        TextSubIndexWrite.Text = "00"
        ComboLen.SelectedIndex = 3 ' длина равна 4
        TextD0.Text = "00"
        TextD1.Text = "00"
        TextD2.Text = "00"
        TextD3.Text = "00"
        SDOWriteData()
        Thread.Sleep(100)
    End Sub

    ''' <summary>
    ''' Прочитать текущую позицию энкодера
    ''' </summary>
    Private Sub ReadPositionEncoder()
        TextIndexRead.Text = "6004"
        TextSubIndexRead.Text = "00"
        SDOReadData()
    End Sub

    ''' <summary>
    ''' Прочитать текущую температуру энкодера
    ''' Actual temperature Position‐Sensor
    ''' </summary>
    Private Sub ReadTemperature()
        TextIndexRead.Text = "2120"
        TextSubIndexRead.Text = "04"
        SDOReadData()
    End Sub

    'Private Sub МаксимальноеФизическоеРазрешение()
    '    Text_Index_Write.Text = "6002"
    '    Text_SubIndex_Write.Text = "00"
    '    Combo_Len.SelectedIndex = 3 ' длина равна 4
    '    Text_D0.Text = "00"
    '    Text_D1.Text = "00"
    '    Text_D2.Text = "00"
    '    Text_D3.Text = "10"
    '    SDOWriteData()
    '    Thread.Sleep(100)
    'End Sub

    Private Sub Combo_Com_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles ComboCom.SelectedIndexChanged
        COMPORT = CByte(ComboCom.SelectedIndex)
    End Sub

    Private Sub Combo_Baud_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles ComboBaud.SelectedIndexChanged
        BAUD = CByte(ComboBaud.SelectedIndex)

        Select Case (BAUD)
            Case 0 '10 kbps
                DelayTime = 15
                Exit Select
            Case 1 '20 kbps
                DelayTime = 7
                Exit Select
            Case 2 '50 kbps
                DelayTime = 3
                Exit Select
            Case 3, 4 '125 kbps'250 kbps
                DelayTime = 1
                Exit Select
            Case 5, 6, 7 '500 kbps'800 kbps'1000 kbps
                DelayTime = 0
        End Select
    End Sub

    Private Sub Combo_Node_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles ComboNode.SelectedIndexChanged
        NODE = CByte(ComboNode.SelectedIndex + 1)
    End Sub

    Private Sub Btn_Initial_Click(ByVal sender As Object, ByVal e As EventArgs) Handles ButtonInitial.Click
        'InitializeMaster()
        LoadEncoder()
    End Sub

    ''' <summary>
    ''' Инициализация I-7565-CPM
    ''' </summary>
    Private Sub InitializeMaster()
        ' Функция должна быть применена при настройке регулятора и инициализировать в I-7565-CPM.
        ' Она должен вызываться один раз перед использованием других функций I7565CPM.lib
        ret = I7565CPM.I7565CPM_InitMaster(COMPORT, 0, BAUD, BlockMode)

        If ret <> 0 Then
            MessageBox.Show("Initial Error, = " + ret.ToString("D"),
                            "Ошибка I-7565-CPM", MessageBoxButtons.OK, MessageBoxIcon.Error)
            AddLogMessage($"Инициализация Мастера. Процедура: <{NameOf(InitializeMaster)}> Ошибка: " & TranslateReturnCode(ret), ColorForAlarm.AlarmMessage)
        Else
            AddLogMessage($"Инициализация Мастера произведена успешно. Процедура: <{NameOf(InitializeMaster)}>", ColorForAlarm.InformationMessage)

            ComboCom.Enabled = False
            ComboBaud.Enabled = False
            ComboNode.Enabled = True

            ButtonInitial.Enabled = False
            ButtonInitial.Visible = False
            ButtonShutdown.Enabled = True

            ButtonAddNode.Enabled = True
            ButtonAddNode.Visible = True

            ' Интерфейсом CANopen предусмотрены два протокола контроля функционирования сети: 
            ' протокол караула узлов (Node guarding protocol) 
            ' и протокол контрольного тактирования (Heartbeat protocol). 
            ' В первом случае выделенный NMT-мастер опрашивает Slave-устройства через одинаковые промежутки времени, называемые guard time. 
            ' В ответ каждое Slave-устройство посылает сообщение, содержащее его сетевой статус.
            ' Время ожидания подобного сообщения может быть настроено индивидуально для каждого узла. 
            ' Если узел по истечении заданного времени не получил запрос от Master-устройства, на его стороне с помощью сервиса Life Guarding Event возникнет ошибка, 
            ' свидетельствующая об отсутствии сторожевого запроса.
            ' Если удаленный запрос передачи не был подтвержден за время сторожевого ожидания, или указанный в ответном сообщении статус Slave-устройства не соответствует ожидаемому,
            ' со стороны Master-устройства возникнет ошибка караула узла, сообщаемая с помощью сервиса Node guarding event.

            ' Heartbeat-протокол позволяет контролировать функционирование сети без необходимости посылки Slave-устройствами удаленных ответов.
            ' В данном случае узел, сконфигурированный на широковещательную передачу Heartbeat-сообщения, является производителем контрольных тактов.
            ' Остальные устройства, настроенные на прием Heartbeat-сообщения, являются потребителями контрольных тактов,
            ' и в случае если за время ожидания контрольного такта (Heartbeat Consumer Time) Heartbeat-сообщение не пришло,
            ' генерируется ошибка контрольного тактирования. 

            ' Оба рассмотренных протокола контроля функционирования сети являются взаимоисключающими, то есть в сети можно использовать лишь один из них.
            ' !!! Heartbeat-протокол имеет более высокий приоритет, и по умолчанию предполагается использование именно его
            I7565CPM.I7565CPM_InstallNMTErrISR(COMPORT, MyNMTErrISR)
            AddLogMessage($"Инициализация протокола контрольного тактирования произведена успешно. Процедура: <{NameOf(InitializeMaster)}>", ColorForAlarm.ConditionUp)
            ' Для повышения надежности функционирования сети имеются объекты срочных сообщений (Emergency Object или EMCY).
            ' Их передача осуществляется при возникновении внутренних ошибок какого-либо узла.
            ' Срочное сообщение передается в сеть лишь один раз после возникновения определенной ошибки, и,
            ' как бы долго состояние активной ошибки не присутствовало, нового соответствующего ей EMCY передано не будет.
            ' Только при возникновении новых ошибок могут быть переданы соответствующие им EMCY
            I7565CPM.I7565CPM_InstallEMCYISR(COMPORT, MyEMCYISR)
            AddLogMessage($"Инициализация протокола срочных сообщений произведена успешно. Процедура: <{NameOf(InitializeMaster)}>", ColorForAlarm.ConditionUp)
            TimerMNT.Enabled = True
        End If
    End Sub

    Private Sub Btn_Shutdown_Click(ByVal sender As Object, ByVal e As EventArgs) Handles ButtonShutdown.Click
        ShutdownMaster()
    End Sub

    ''' <summary>
    ''' Сбрасывает и остановить все функции Мастера в I-7565-CPM
    ''' </summary>
    Private Sub ShutdownMaster()
        StopTask()

        ' удалить обработчики
        TimerMNT.Enabled = False
        I7565CPM.I7565CPM_RemoveNMTErrISR(COMPORT)
        I7565CPM.I7565CPM_RemoveEMCYISR(COMPORT)
        MyNMTErrISR = Nothing
        MyEMCYISR = Nothing
        'List_EMCY.Items.Clear()

        ' Функция I7565CPM_ShutdownMaster удаляет всех ведомых, которые были добавлены в Мастер и остановить все функции в I-7565-CPM.
        ' Функция должна быть вызвана перед выходом прикладных программ пользователей.
        ret = I7565CPM.I7565CPM_ShutdownMaster(COMPORT)

        If ret <> 0 Then
            MessageBox.Show("Shutdown Error, = " + ret.ToString("D"),
                            "Ошибка I-7565-CPM", MessageBoxButtons.OK, MessageBoxIcon.Error)
            AddLogMessage($"Выгрузка Мастера. Процедура: <{NameOf(ShutdownMaster)}> Ошибка: " & TranslateReturnCode(ret), ColorForAlarm.AlarmMessage)
        Else
            AddLogMessage($"Выгрузка Мастера произведена успешно. Процедура: <{NameOf(ShutdownMaster)}>", ColorForAlarm.InformationMessage)
            HideControlsShutdownMaster()
        End If
    End Sub

    ''' <summary>
    ''' Выключить доступность контролов при выгрузке мастера
    ''' </summary>
    Private Sub HideControlsShutdownMaster()
        ComboCom.Enabled = True
        ComboBaud.Enabled = True
        ComboNode.Enabled = False

        ButtonInitial.Enabled = True
        ButtonInitial.Visible = True
        ButtonShutdown.Enabled = False

        ButtonAddNode.Enabled = False
        ButtonAddNode.Visible = True
        ButtonRemove.Enabled = False

        EnableDisableContrtol(False)
        IsEncoderLoadedAndRunSuccess = False
    End Sub

    Private Sub Btn_AddNode_Click(ByVal sender As Object, ByVal e As EventArgs) Handles ButtonAddNode.Click
        AddNode()
    End Sub

    ''' <summary>
    ''' Добавить в список узлов ведомый узел с указанным идентификатором  
    ''' </summary>
    Private Sub AddNode()
        ' Функция I7565CPM_AddNode можете добавить к canopen ведомый с указанным идентификатором узла в список узлов.
        ' Есть три режима функции. Способ 1 автоматическое добавление узла, 2 режим добавления узлов вручную, и режим 3 ждет узла загрузки массаж для добавить.
        ' В автоматическом режиме, вызова этой функции добавляет ведомого. Ведомый выйдет в рабочее состояние непосредственно.
        ' И мастер будет сканировать TxPDO1 ~ TxPDO10 и RxPDO1 ~ RxPDO10 и установить их в прошивку на I-7565-CPM, 
        ' если ведомый обслуживает эти объекты PDO.
        ' В ручном режиме, эта функция будет добавить к canopen ведомого только основной перечень узлов, и не будет посылать никакое сообщение, 
        ' чтобы проверить, ведомый существующей или нет. Кроме того, в ручном режиме не установить идентификатор синхронизации, код паспорта, и любого PDO объекта в прошивку на I-7565-CPM.
        ' Пользователи должны позвонить I7565CPM_SetSYNC_List, I7565CPM_SetEMCY_List, и I7565CPM_InstallPDO_List, чтобы завершить установку объекта для завершения процесса добавления узла.
        ' Добавленный узел может быть удален из списка основной узел функцией I7565CPM_RemoveNode.
        ret = I7565CPM.I7565CPM_AddNode(COMPORT, NODE, 1, DelayTime, 200, BlockMode)

        If ret <> 0 Then
            MessageBox.Show("AddNode Error, = " + ret.ToString("D"),
                            "Ошибка I-7565-CPM", MessageBoxButtons.OK, MessageBoxIcon.Error)
            AddLogMessage($"Добавление ведомого узла. Процедура: <{NameOf(AddNode)}> Ошибка: " & TranslateReturnCode(ret), ColorForAlarm.AlarmMessage)
        Else
            AddLogMessage($"Добавление ведомого узла произведено успешно. Процедура: <{NameOf(AddNode)}>", ColorForAlarm.InformationMessage)

            ButtonAddNode.Enabled = False
            ButtonAddNode.Visible = False
            ButtonRemove.Enabled = True
            ComboNode.Enabled = False

            ' Используйте I7565CPM_GetTxPDOID функцию, чтобы получить список всех TxPDO COB-IDs указанного ведомого,
            ' которые были установлены к мастеру.
            ret = I7565CPM.I7565CPM_GetTxPDOID(COMPORT, NODE, PDO_Cnt, Id_List, BlockMode)
            'ReDim_Preserve Id_List(PDO_Cnt - 1)
            Re.DimPreserve(Id_List, PDO_Cnt - 1)

            If (ret <> 0) Then
                MessageBox.Show("GetTxPDOID Error, = " + ret.ToString("D"),
                                "Ошибка I-7565-CPM", MessageBoxButtons.OK, MessageBoxIcon.Error)
                AddLogMessage($"Получение списка всех TxPDO идентификаторов (COB-ID) ведомого узла. Процедура: <{NameOf(AddNode)}> Ошибка: " & TranslateReturnCode(ret), ColorForAlarm.AlarmMessage)

                ButtonRunTask.Enabled = False
            Else
                AddLogMessage($"Получение списка всех TxPDO идентификаторов (COB-ID) ведомого узла произведено успешно. Процедура: <{NameOf(AddNode)}>", ColorForAlarm.InformationMessage)
                ButtonRunTask.Enabled = True
            End If

            EnableDisableContrtol(True)
        End If
    End Sub

    Private Sub Btn_Remove_Click(ByVal sender As Object, ByVal e As EventArgs) Handles ButtonRemove.Click
        RemoveNode()
    End Sub

    ''' <summary>
    ''' Удалить ведомый узел
    ''' </summary>
    Private Sub RemoveNode()
        ' Функция I7565CPM_RemoveNode удаляет указанный узел ведомого из списка узлов мастера. 
        ' Это требует действительного узла, который установлен с помощью функции I7565CPM_AddNode раньше.
        ret = I7565CPM.I7565CPM_RemoveNode(COMPORT, NODE, BlockMode)

        If ret <> 0 Then
            MessageBox.Show("RemoveNode Error, = " + ret.ToString("D"),
                            "Ошибка I-7565-CPM", MessageBoxButtons.OK, MessageBoxIcon.Error)
            AddLogMessage($"Удаление ведомого узла. Процедура: <{NameOf(RemoveNode)}> Ошибка: " & TranslateReturnCode(ret), ColorForAlarm.AlarmMessage)
        Else
            AddLogMessage($"Удаление ведомого узла произведено успешно. Процедура: <{NameOf(RemoveNode)}>", ColorForAlarm.InformationMessage)

            ButtonRemove.Enabled = False
            ButtonAddNode.Enabled = True
            ButtonAddNode.Visible = True
            ComboNode.Enabled = True

            EnableDisableContrtol(False)
        End If
    End Sub

    Private Sub Check_Is_Hex(ByVal sender As Object, ByVal e As KeyPressEventArgs) Handles TextIndexWrite.KeyPress,
                                                                                                    TextSubIndexWrite.KeyPress,
                                                                                                    TextD3.KeyPress,
                                                                                                    TextD2.KeyPress,
                                                                                                    TextD1.KeyPress,
                                                                                                    TextD0.KeyPress,
                                                                                                    TextIndexRead.KeyPress,
                                                                                                    TextSubIndexRead.KeyPress

        If ((("1234567890ABCDEFabcdef".IndexOf(e.KeyChar) <> -1) OrElse (Convert.ToInt16(e.KeyChar) = 8)) = False) Then e.Handled = True
    End Sub

    Private Sub Check_Is_Dec(ByVal sender As Object, ByVal e As KeyPressEventArgs) Handles TextGuardTime.KeyPress
        If (("1234567890".IndexOf(e.KeyChar) <> -1) OrElse (Convert.ToInt16(e.KeyChar) = 8)) Then
            e.Handled = False
        Else
            e.Handled = True
        End If
    End Sub

#End Region

#Region "SDO_Write"
    Private Sub Btn_WriteSDO_Click(ByVal sender As Object, ByVal e As EventArgs) Handles ButtonWriteSDO.Click
        SDOWriteData()
    End Sub

    ''' <summary>
    ''' Отправить сообщение SDO
    ''' </summary>
    Private Sub SDOWriteData()
        Dim Rlen As UShort
        Dim TData(4), RData(8) As Byte
        Dim dataStr As String

        Dim Index As UShort = Convert.ToUInt16(TextIndexWrite.Text, 16)
        Dim SubIndex As Byte = Convert.ToByte(TextSubIndexWrite.Text, 16)
        Dim Tlen As Byte = CByte(ComboLen.SelectedIndex + 1)

        TData(0) = Convert.ToByte(TextD0.Text, 16)
        TData(1) = Convert.ToByte(TextD1.Text, 16)
        TData(2) = Convert.ToByte(TextD2.Text, 16)
        TData(3) = Convert.ToByte(TextD3.Text, 16)

        ' I7565CPM_SDOWriteData функции можете отправить сообщение SDO к указанному ведомому устройству.
        ' Эта процедура также вызывает загрузить протокол SDO. Параметр узла функции I7565CPM_SDOWriteData используется для указания какое подчиненное устройство получите это сообщение SDO.
        ' Из-за того, что длина данных каждого объекта, хранимого в словаре объекта различна, пользователи должны знать длину данных при записи объекта словаря объектов указанного ведомого устройства. 
        ' Эта функция поддерживает как режим экспедиции (менее 4 байт длина данных) и режим сегменте (более 4 байт длина данных)
        ret = I7565CPM.I7565CPM_SDOWriteData(COMPORT, NODE, Index, SubIndex, Tlen, TData, Rlen, RData, BlockMode)

        If ((ret <> 0) AndAlso (ret <> I7565CPM._25_CPM_Abort)) Then
            MessageBox.Show("SDO Write Error, = " + ret.ToString("D"),
                            "Ошибка I-7565-CPM", MessageBoxButtons.OK, MessageBoxIcon.Error)
            AddLogMessage($"Отправка сообщения SDO к ведомому узлу. Процедура: <{NameOf(SDOWriteData)}> Ошибка: " & TranslateReturnCode(ret), ColorForAlarm.AlarmMessage)
        Else
            dataStr = $"{Index.ToString("X4")}-{SubIndex.ToString("X2")}: "

            If Rlen <> 0 Then
                For I = 0 To (Rlen - 1)
                    dataStr += RData(I).ToString("X2") + " "
                Next
            End If

            If ret = I7565CPM._25_CPM_Abort Then
                dataStr += ": Write Abort"
            Else
                dataStr += ": Write OK"
            End If

            ListSDODataWrite.Items.Add(dataStr)
            AddLogMessage($"Отправка сообщения SDO к ведомому узлу. Процедура: <{NameOf(SDOWriteData)}> произведена: " & dataStr, ColorForAlarm.WriteOk)
            ret = 0
        End If
    End Sub

    Private Sub Btn_Clear_Click(ByVal sender As Object, ByVal e As EventArgs) Handles ButtonClearWrite.Click
        ListSDODataWrite.Items.Clear()
    End Sub
#End Region

#Region "SDO_Read"
    Private Sub Btn_ReadSDO_Click(ByVal sender As Object, ByVal e As EventArgs) Handles ButtonReadSDO.Click
        SDOReadData()
    End Sub

    ''' <summary>
    ''' Чтение значения SDO
    ''' </summary>
    Private Sub SDOReadData()
        Dim RDlen As UShort
        Dim RData(50) As Byte
        Dim Index As UShort = Convert.ToUInt16(TextIndexRead.Text, 16)
        Dim SubIndex As Byte = Convert.ToByte(TextSubIndexRead.Text, 16)

        ' Функция I7565CPM_SDOReadData позволяет загрузить SDO из указанного ведомого. 
        ' При использовании этой функции передать ведомого устройства код, индекс объекта и объекта субиндекс в этой функции.
        ' Эта функция поддерживает как режим экспедиции (менее 4 байт длина данных) и режим сегменте (более 4 байт длина данных).
        ret = I7565CPM.I7565CPM_SDOReadData(COMPORT, NODE, Index, SubIndex, CUInt(RDlen), RData, BlockMode)

        If ((ret <> 0) AndAlso (ret <> I7565CPM._25_CPM_Abort)) Then
            MessageBox.Show("SDO Read Error, = " + ret.ToString("D"),
                            "Ошибка I-7565-CPM", MessageBoxButtons.OK, MessageBoxIcon.Error)
            AddLogMessage($"Чтение значения SDO. Процедура: <{NameOf(SDOReadData)}> Ошибка: " & TranslateReturnCode(ret), ColorForAlarm.AlarmMessage)
        Else
            Dim DataStr As String = $"{Index.ToString("X4")}-{SubIndex.ToString("X2")}: "

            For I = 0 To (RDlen - 1)
                DataStr += RData(I).ToString("X2") + " "
            Next

            If TextIndexRead.Text = "6004" Then
                GetPosition(RData, RDlen)
            ElseIf TextIndexRead.Text = "2120" Then
                GetTemperature(RData, RDlen)
            End If

            If ret = I7565CPM._25_CPM_Abort Then
                DataStr = DataStr + ": Read Abort"
            End If

            ListSDODataRead.Items.Add(DataStr)
            AddLogMessage($"Чтение значения SDO. Процедура: <{NameOf(SDOReadData)}> произведено: " & DataStr, ColorForAlarm.ReadOk)
        End If
    End Sub

    Private Sub Btn_Clear_Read_Click(sender As Object, e As EventArgs) Handles ButtonClearRead.Click
        ListSDODataRead.Items.Clear()
    End Sub
#End Region

#Region "NMT_Protocol"
    ''' <summary>
    ''' пользовательский обработчик события остслеживания ошибок сторожевое устройство/тактовый импульс  
    ''' (Guarding/Heartbeat) процесса
    ''' </summary>
    Private Sub UserNMTErrISR()
        Dim board, node, mode As Byte
        Dim tempstr As String = ""

        ' проверить ошибки сетевого менеджмента NMT Error Event для всех узлов
        If (I7565CPM.I7565CPM_GetNMTError(board, node, mode) = 0) Then
            ' режим произошедшей ошибки
            If (mode = I7565CPM.CPM_Node_Guarding_Event) Then
                tempstr = $"NMTErr: Board {board.ToString("D")}, Node {node.ToString("D")} Неисправность караульного узла"
            ElseIf (mode = I7565CPM.CPM_Heartbeat_Event) Then
                tempstr = $"NMTErr: Board {board.ToString("D")}, Node {node.ToString("D")} Ошибка контрольного тактирования"
            End If
        End If

        NMTErr = tempstr
        AddLogMessage(NMTErr, ColorForAlarm.AlarmMessage)
    End Sub

    ''' <summary>
    ''' пользовательский обработчик события остслеживания внутренних ошибок
    ''' </summary>
    Private Sub UserEMCYISR()
        Dim board, node As Byte
        Dim data(7) As Byte
        Dim tempstr As String = ""

        ' узнать, какой узел ведомого устройства вызвал внутреннюю ошибку
        If (I7565CPM.I7565CPM_GetEMCYData(board, node, data) = 0) Then
            Dim i As UShort
            tempstr = $"EMCY: Board {board.ToString("D")}, Node {node.ToString("D")} :"
            For i = 0 To 7
                tempstr += data(i).ToString("X2") + " "
            Next
        End If

        EMCY = tempstr
        AddLogMessage(EMCY, ColorForAlarm.AlarmMessage)
        Dim emergencyErrorCode As String = data(1).ToString("X2") & data(0).ToString("X2")

        If emergencyErrorCode = "5200" Then
            ' сбросить сообщение об этой ошибке, т.к. скорее всего она не критическая, а служебная. 
            EMCY = ""
        End If

        AddLogMessage(TranslateEmergencyErrorCodeToString(emergencyErrorCode), ColorForAlarm.AlarmMessage)
    End Sub

    ''' <summary>
    ''' Перевести код в разумное сообщение.
    ''' Код ошибки (EmergencyErrorCode) состоит из 1 и 0 бита 8-bytes EMCY данных буфера ответа.
    ''' </summary>
    ''' <param name="inEmergencyErrorCode"></param>
    ''' <returns></returns>
    Private Function TranslateEmergencyErrorCodeToString(inEmergencyErrorCode As String) As String
        Dim groupErrorCodeHex As String = String.Empty  ' группа в котору попадает ошибка
        Dim description As String = String.Empty        ' описание ошибки

        If inEmergencyErrorCode.StartsWith("00") Then
            groupErrorCodeHex = "00xx" : description = "Error Reset or No Error"
        ElseIf inEmergencyErrorCode.StartsWith("10") Then
            groupErrorCodeHex = "10xx" : description = "Generic Error"
        ElseIf inEmergencyErrorCode.StartsWith("20") Then
            groupErrorCodeHex = "20xx" : description = "Current"
        ElseIf inEmergencyErrorCode.StartsWith("21") Then
            groupErrorCodeHex = "21xx" : description = "Current, device input side"
        ElseIf inEmergencyErrorCode.StartsWith("22") Then
            groupErrorCodeHex = "22xx" : description = "Current inside the device"
        ElseIf inEmergencyErrorCode.StartsWith("23") Then
            groupErrorCodeHex = "23xx" : description = "Current, device output side"
        ElseIf inEmergencyErrorCode.StartsWith("30") Then
            groupErrorCodeHex = "30xx" : description = "Voltage"
        ElseIf inEmergencyErrorCode.StartsWith("31") Then
            groupErrorCodeHex = "31xx" : description = "Mains Voltage"
        ElseIf inEmergencyErrorCode.StartsWith("32") Then
            groupErrorCodeHex = "32xx" : description = "Voltage inside the device"
        ElseIf inEmergencyErrorCode.StartsWith("33") Then
            groupErrorCodeHex = "33xx" : description = "Output Voltage"
        ElseIf inEmergencyErrorCode.StartsWith("40") Then
            groupErrorCodeHex = "40xx" : description = "Temperature"
        ElseIf inEmergencyErrorCode.StartsWith("41") Then
            groupErrorCodeHex = "41xx" : description = "Ambient Temperature"
        ElseIf inEmergencyErrorCode.StartsWith("42") Then
            groupErrorCodeHex = "42xx" : description = "Device Temperature"
        ElseIf inEmergencyErrorCode.StartsWith("50") Then
            groupErrorCodeHex = "50xx" : description = "Device Hardware"
        ElseIf inEmergencyErrorCode.StartsWith("52") Then
            groupErrorCodeHex = "52xx" : description = "ICLG Optic Failure"
        ElseIf inEmergencyErrorCode.StartsWith("53") Then
            groupErrorCodeHex = "53xx" : description = "ICLG Gear Error"
        ElseIf inEmergencyErrorCode.StartsWith("60") Then
            groupErrorCodeHex = "60xx" : description = "Device Software"
        ElseIf inEmergencyErrorCode.StartsWith("61") Then
            groupErrorCodeHex = "61xx" : description = "Internal Software"
        ElseIf inEmergencyErrorCode.StartsWith("62") Then
            groupErrorCodeHex = "62xx" : description = "User Software"
        ElseIf inEmergencyErrorCode.StartsWith("63") Then
            groupErrorCodeHex = "63xx" : description = "Data Set"
        ElseIf inEmergencyErrorCode.StartsWith("70") Then
            groupErrorCodeHex = "70xx" : description = "Additional Modules"
        ElseIf inEmergencyErrorCode.StartsWith("80") Then
            groupErrorCodeHex = "80xx" : description = "Monitoring"
        ElseIf inEmergencyErrorCode.StartsWith("81") Then
            If inEmergencyErrorCode = "8110" Then
                groupErrorCodeHex = "8110" : description = "CAN Overrun (Objects lost)"
            ElseIf inEmergencyErrorCode = "8120" Then
                groupErrorCodeHex = "8120" : description = "CAN In Error Passive Mode"
            ElseIf inEmergencyErrorCode = "8130" Then
                groupErrorCodeHex = "8130" : description = "Life Guard Error or Heartbeat Error"
            ElseIf inEmergencyErrorCode = "8140" Then
                groupErrorCodeHex = "8140" : description = "Recovered from bus off"
            ElseIf inEmergencyErrorCode = "8150" Then
                groupErrorCodeHex = "8150" : description = "Transmit COB-ID collision"
            Else
                groupErrorCodeHex = "81xx" : description = "Communication"
            End If
        ElseIf inEmergencyErrorCode.StartsWith("82") Then
            If inEmergencyErrorCode = "8210" Then
                groupErrorCodeHex = "8210" : description = "PDO not processed due to lenght error"
            ElseIf inEmergencyErrorCode = "8220" Then
                groupErrorCodeHex = "8220" : description = "PDO lenght exceeded"
            Else
                groupErrorCodeHex = "82xx" : description = "Protocol Error"
            End If
        ElseIf inEmergencyErrorCode.StartsWith("90") Then
            groupErrorCodeHex = "90xx" : description = "External Error"
        ElseIf inEmergencyErrorCode.StartsWith("F0") Then
            groupErrorCodeHex = "F0xx" : description = "Additional Function"
        ElseIf inEmergencyErrorCode.StartsWith("FF") Then
            groupErrorCodeHex = "FFxx" : description = "Device specific"
        Else
            groupErrorCodeHex = "???" : description = "Ошибка не определена"
        End If

        Return $"Emergency Error (hex)={inEmergencyErrorCode}; ID error group={groupErrorCodeHex}; description={description}"
    End Function

    'Private Sub ShowError()
    '    Dim arrError As String() = {"00xx", "10xx", "20xx", "21xx", "22xx", "23xx", "30xx", "31xx", "32xx", "33xx", "40xx", "41xx", "42xx", "50xx",
    '        "60xx", "61xx", "62xx", "63xx", "70xx", "80xx", "8110", "8120", "8130", "8140", "8150", "81xx", "8210", "8220", "82xx", "90xx", "F0xx", "FFxx", "???"}

    '    For Each itemErr In arrError
    '        AddLogMessage(TranslateEmergencyErrorCodeToString(itemErr), ColorForAlarm.AlarmMessage)
    '    Next
    'End Sub

    ' NMT-объекты (Network Management), позволяющие управлять работой этой сети. Вначале стоит отметить, что в конкретный момент времени устройство должно находиться в одном из четырех состояний:
    ' инициализация (Initialization), готовность (Pre-operational), работа (Operational) или остановка (Stopped).
    ' При включении устройство проходит этап внутренней инициализации, и после ее успешного завершения переходит в состояние готовности.
    ' В этом состоянии уже возможно осуществить настройку CANopen-узла с помощью SDO. Затем узел может перейти в рабочее состояние. 
    ' Для этого необходимо, чтобы Master сети (передача NMT сообщений происходит в соответствии с моделью Master-Slave) передал широковещательное сообщение Start_remote_node.
    ' ID NMT-сообщений равен 0, поскольку они должны иметь самый высокий приоритет в сети.
    ' Сообщение                     Обозначение         Команда в составе формата сообщения
    ' Запуск удаленного узла        Start_remote_node       1
    ' Остановка удаленного узла     Stop_remote_node        2
    ' Вход в состояние готовности   Enter_pre_operational   128
    ' Сброс узла                    Reset_node              129
    ' Сброс связи                   Reset_communication     130
    ' Формат NMT-сообщения также предполагает наличие ID того узла, которому адресовано сообщение. В случае равенства этого параметра 0 сообщение будет адресовано всем узлам сети
    Private Sub Combo_State_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles ComboState.SelectedIndexChanged
        Select Case (ComboState.SelectedIndex)
            Case 0
                STA = &H1
                STA_Str = "Operation mode"
            Case 1
                STA = &H2
                STA_Str = "Stop mode"
            Case 2
                STA = &H80
                STA_Str = "Pre-Operation mode"
            Case 3
                STA = &H81
                STA_Str = "Reset Node"
            Case 4
                STA = &H82
                STA_Str = "Reset Communication"
        End Select
    End Sub

    Private Sub Btn_SetState_Click(ByVal sender As Object, ByVal e As EventArgs) Handles ButtonSetState.Click
        NMTChangeState()
    End Sub

    ''' <summary>
    ''' Изменение CANopen состояние ведомого узла
    ''' </summary>
    Private Sub NMTChangeState()
        ret = I7565CPM.I7565CPM_NMTChangeState(COMPORT, NODE, STA, BlockMode)

        If (ret <> 0) Then
            MessageBox.Show("Change Slave State Error, = " + ret.ToString("D"),
                            "Ошибка I-7565-CPM", MessageBoxButtons.OK, MessageBoxIcon.Error)
            AddLogMessage($"Измение состояние ведомого узла. Процедура: <{NameOf(NMTChangeState)}> Ошибка: " & TranslateReturnCode(ret), ColorForAlarm.AlarmMessage)
        Else
            Dim result As String = $"Change Slave State to {STA_Str} OK"
            ListEMCY.Items.Add(result)
            AddLogMessage($"Измение состояние ведомого узла. Процедура: <{NameOf(NMTChangeState)}> произведено: " & result, ColorForAlarm.watchdogSet)
        End If
    End Sub

    Private Sub Btn_Clear_NMT_Protocol_Click(ByVal sender As Object, ByVal e As EventArgs) Handles ButtonClearNMTProtocol.Click
        ListEMCY.Items.Clear()
    End Sub

    Private Sub Btn_SetGuard_Click(ByVal sender As Object, ByVal e As EventArgs) Handles ButtonSetGuard.Click
        SetNMTGuarding()
    End Sub

    ''' <summary>
    ''' Установить сторожевое время и фактор времени жизни караульного ведомого узла 
    ''' </summary>
    Private Sub SetNMTGuarding()
        Dim GuardTime As UInt16 = CUShort(Val(TextGuardTime.Text))
        Dim LifeTime As Byte = CByte(Val(TextLifeTime.Text))

        ' караульный узел
        ' Использовать I7565CPM_NMTGuarding функцию, чтобы установить сторожевое время и фактор времени жизни ведомого с указанным идентификатором узла.
        ret = I7565CPM.I7565CPM_NMTGuarding(COMPORT, NODE, GuardTime, LifeTime, BlockMode)

        If ret <> 0 Then
            MessageBox.Show("При установке отслеживания караульного узла произошла ошибка, = " + ret.ToString("D"),
                            "Ошибка I-7565-CPM", MessageBoxButtons.OK, MessageBoxIcon.Error)
            AddLogMessage($"Установка отслеживания караульного узла. Процедура: <{NameOf(SetNMTGuarding)}> Ошибка: " & TranslateReturnCode(ret), ColorForAlarm.AlarmMessage)
        Else
            ListEMCY.Items.Add("Установлено отслеживание караульного узла")
            AddLogMessage($"Установка отслеживания караульного узла произведена успешно. Процедура: <{NameOf(SetNMTGuarding)}> ", ColorForAlarm.watchdogSet)
        End If
    End Sub

    Private Sub Btn_SetHeartbeat_Click(ByVal sender As Object, ByVal e As EventArgs) Handles ButtonSetHeartbeat.Click
        SetNMTHeartbeat()
    End Sub

    ''' <summary>
    ''' Установить время проверки контрольного тактирования ведомого узла
    ''' </summary>
    Private Sub SetNMTHeartbeat()
        Dim Heartbeat As UInt16 = CUShort(Val(TextHeartbeat.Text))
        Dim Consumer As UInt16 = CUShort(Val(TextConsumer.Text))

        ' контрольное тактирование
        ' Используйте функцию I7565CPM_NMTHeartbeat, чтобы установить время проверки контрольного тактирования ведомого с указанным идентификатором узла I-7565-CPM
        ret = I7565CPM.I7565CPM_NMTHeartbeat(COMPORT, NODE, Heartbeat, Consumer, BlockMode)

        If ret <> 0 Then
            MessageBox.Show("При установке отслеживания контрольного тактирования произошла ошибка, = " + ret.ToString("D"),
                            "Ошибка I-7565-CPM", MessageBoxButtons.OK, MessageBoxIcon.Error)
            AddLogMessage($"Установка отслеживания контрольного тактирования. Процедура: <{NameOf(SetNMTHeartbeat)}> Ошибка: " & TranslateReturnCode(ret), ColorForAlarm.AlarmMessage)
        Else
            ListEMCY.Items.Add("Установлено отслеживание контрольного тактирования")
            AddLogMessage($"Установка отслеживания контрольного тактирования произведена успешно. Процедура: <{NameOf(SetNMTHeartbeat)}> ", ColorForAlarm.watchdogSet)
        End If
    End Sub

    Private Sub TimerMNT_Tick(ByVal sender As Object, ByVal e As EventArgs) Handles TimerMNT.Tick
        If (EMCY <> "") Then
            ListEMCY.Items.Add(EMCY)
            EMCY = ""
            Reset()
        End If

        If (NMTErr <> "") Then
            ListEMCY.Items.Add(NMTErr)
            NMTErr = ""
            Reset()
        End If
    End Sub
#End Region

#Region "PDO_MultiData"

    ''' <summary>
    ''' Запустить TxPDO Polling
    ''' </summary>
    Private Sub SetPolling()
        ' Если canopen с ведомый не поддерживают функцию таймера событие TxPDOs, 
        ' используя I7565CPM_SetPDORemotePolling функцю можно конфигурировать не более 125 объектов TxPDO в список удаленного опроса. 
        ' Затем, в I-7565-CPM, которая будет опрашивать настроен TxPDOs и автоматически обновлять данные в буфер. 
        ' Пользователи могут использовать I7565CPM_GetMultiPDOData, чтобы получить эти данные TxPDOs из буфера быстрее и легче.
        ret = I7565CPM.I7565CPM_SetPDORemotePolling(COMPORT, PDO_Cnt, Id_List, TimePolling, BlockMode)

        If (ret <> 0) Then
            MessageBox.Show("Ошибка конфигурирования группового опроса всех TxPDO, = " + ret.ToString("D"),
                            "Ошибка I-7565-CPM", MessageBoxButtons.OK, MessageBoxIcon.Error)
            AddLogMessage($"Конфигурирование объектов TxPDO в список удаленного опроса. Процедура: <{NameOf(SetPolling)}> Ошибка: " & TranslateReturnCode(ret), ColorForAlarm.AlarmMessage)
        Else
            AddLogMessage($"Конфигурирование объектов TxPDO в список удаленного опроса произведено успешно. Процедура: <{NameOf(SetPolling)}>", ColorForAlarm.InformationMessage)
        End If
    End Sub

    ''' <summary>
    ''' Остановить TxPDO Polling
    ''' </summary>
    Private Sub ResetPolling()
        Dim CobId(0) As UShort

        CobId.Initialize()
        ' Удалить список ранее сконфигурированный PDO-Polling
        ret = I7565CPM.I7565CPM_SetPDORemotePolling(COMPORT, 0, CobId, 0, BlockMode)

        If (ret <> 0) Then
            MessageBox.Show("Ошибка остановки группового опроса TxPDO, = " + ret.ToString("D"),
                            "Ошибка I-7565-CPM", MessageBoxButtons.OK, MessageBoxIcon.Error)
            AddLogMessage($"Остановка группового опроса объектов TxPDO. Процедура: <{NameOf(ResetPolling)}> Ошибка: " & TranslateReturnCode(ret), ColorForAlarm.AlarmMessage)
        Else
            AddLogMessage($"Остановка группового опроса объектов TxPDO произведена успешно. Процедура: <{NameOf(ResetPolling)}>", ColorForAlarm.InformationMessage)
        End If
    End Sub

    ''' <summary>
    ''' Получить несколько входных и выходных данных PDO сразу
    ''' </summary>
    Private Sub GetMultiPDOData()
        Dim isNew(PDO_Cnt - 1) As Byte
        Dim DLen(PDO_Cnt - 1) As Byte
        Dim Data(8 * PDO_Cnt - 1) As Byte
        Dim arrBytes(3) As Byte ' временный буфер

        ' Иногда пользователи хотят, чтобы опрашивать несколько объектов PDO данных одновременно для повышения производительности.
        ' Отправка удаленного PDO для опроса каждого данных PDO по одному медленно.
        ' Таким образом, пользователи могут установить Таймер событий или дистанционный список этих PDO.
        ' Когда PDO данные опрашиваются по I-7565-CPM или получены от ведомого автоматически, используйтся функцию I7565CPM_GetMultiPDOData 
        ' для получения этих данных PDO из буфера в то же время.
        ret = I7565CPM.I7565CPM_GetMultiPDOData(COMPORT, PDO_Cnt, Id_List, isNew, DLen, Data, 1) '0)' BlockMode = 1 иначе через раз ошибка 41 CPM_Processing: Команда в процессе (только для non-block режима)

        If (ret = 0) Then
            ' текущее положение является первым элементом в полученных данных, поэтому не заморачиваться для чтения остальных
            Array.Copy(Data, 0, arrBytes, 0, DLen(0))
            GetPosition(arrBytes, DLen(0))

            ' разбор полученных данных в соответствии с длиной значения объекта TxPDO
            'Dim totallen As Integer = 0

            ''List_PDOData.Items.Clear()

            'If PDO_Cnt <> 0 Then
            '    Dim temp_Str As String
            '    For I As Integer = 0 To PDO_Cnt - 1
            '        temp_Str = "TxPDO " + Id_List(I).ToString("X") + " : "

            '        If DLen(I) <> 0 Then
            '            For J As Integer = 0 To DLen(I) - 1
            '                temp_Str = temp_Str + Data(J + totallen).ToString("X2") + " "
            '            Next
            '        End If

            '        totallen += DLen(I)
            '        'List_PDOData.Items.Add(Temp_Str)
            '    Next
            'End If
        End If

        'Debug.Print(ret)
    End Sub

#End Region

#Region "ViewShow"
    Private Sub ButtonGetPosition_Click(sender As Object, e As EventArgs) Handles ButtonGetPosition.Click
        ' позиция
        ReadPositionEncoder()
        ShowPositionEncoder()
        ' температура
        ReadTemperature()
        ShowThermometer()
    End Sub

    ''' <summary>
    ''' Отобразить на индикаторах приведенные значения углов
    ''' </summary>
    Private Sub ShowPositionEncoder()
        If InvokeRequired Then
            Invoke(New MethodInvoker(Sub() ShowPositionEncoder()))
        Else
            TextPosition.Text = AnglePosition.ToString("F")
            NumericEditAngularPositionEncoder.Value = AnglePosition

            If AnglePosition < 0 Then
                GaugeAngularPositionEncoder.Value = angle360 + AnglePosition Mod angle360
            Else
                If AnglePosition > angle360 Then
                    GaugeAngularPositionEncoder.Value = AnglePosition Mod angle360
                Else
                    GaugeAngularPositionEncoder.Value = AnglePosition
                End If
            End If
        End If
    End Sub

    ''' <summary>
    ''' Отобразить на индикаторах текущую температуру
    ''' </summary>
    Private Sub ShowThermometer()
        If InvokeRequired Then
            Invoke(New MethodInvoker(Sub() ShowThermometer()))
        Else
            ThermometerEncoder.Value = ActualTemperature
        End If
    End Sub

    Private predictedAngle As Double ' начальное положение энкодера в градусах

    ''' <summary>
    ''' Установить начальное положение энкодера в градусах - угол упреждения (концевика)
    ''' </summary>
    ''' <param name="newValue"></param>
    Private Sub SetPredictedAngle(ByVal newValue As Double)
        If newValue < 0 OrElse newValue > 359 Then Exit Sub

        predictedAngle = newValue
        GaugeAngularPositionEncoder.RangeFills(0).Range = New NationalInstruments.UI.Range(360.0R - predictedAngle, 360.0R)
        GaugeAngularPositionEncoder.CustomDivisions(0).Value = 360.0R - predictedAngle
    End Sub

    ''' <summary>
    ''' Инкрементное положение энкодера в градусах от начального при запуске
    ''' </summary>
    ''' <returns></returns>
    Public Property AnglePosition As Double
    ''' <summary>
    ''' Текущая температура энкодера
    ''' </summary>
    ''' <returns></returns>
    Private Property ActualTemperature As Integer
    ''' <summary>
    ''' предаточное отношение редуктора - считывается из настроек
    ''' </summary>
    Private Kreduction As Double = 1.0
    ''' <summary>
    ''' Вращение по часовой стрелке=True- считывается из настроек
    ''' </summary>
    Private IsClockwise As Boolean = True
    Private Const angle360 As Double = 360.0
    ''' <summary>
    ''' разрешение энкодера на один оборот
    ''' </summary>
    Private Const StepsPerRevolution As Integer = 65536
    ''' <summary>
    ''' коррекция = 2^25 - 4096
    ''' </summary>
    Private Const MaxPosition As Integer = 33554432 - 4096
    ''' <summary>
    ''' = 16777216 предположем,  что это точка от которой нужно определять угол в обратном направлении равна середине рабочего диапазона
    ''' </summary>
    Private Const MaxPositionMod2 As Integer = CInt(2 ^ 24)

    ''' <summary>
    ''' Преобразовать 16-ричное значение в десятичное и привести в градусы.
    ''' Учесть направление вращения, разрешение на 1 оборот и Кредукции.
    ''' </summary>
    ''' <param name="rData"></param>
    ''' <param name="rDlen"></param>
    Private Sub GetPosition(rData() As Byte, rDlen As UShort)
        Dim hexString As String = String.Empty

        For I As Integer = rDlen - 1 To 0 Step -1
            hexString += rData(I).ToString("X2")
        Next

        Dim positionResolution As Integer = Integer.Parse(hexString, Globalization.NumberStyles.HexNumber)

        If Not IsClockwise Then
            ' произвести переворот значения при обратном вращении
            If positionResolution <> 0 Then
                positionResolution = MaxPosition - positionResolution
            End If
        End If

        If positionResolution > MaxPositionMod2 Then
            ' произвести скачок на значение максимальной позиции
            positionResolution = positionResolution - MaxPosition
        End If

        AnglePosition = (positionResolution * angle360 / StepsPerRevolution) / Kreduction - predictedAngle
        CheckSwitchingStage(AnglePosition)
    End Sub

    ''' <summary>
    ''' Прочитать текущую температуру энкодера
    ''' </summary>
    ''' <param name="rData"></param>
    ''' <param name="rDlen"></param>
    Private Sub GetTemperature(rData() As Byte, rDlen As UShort)
        Dim hexString As String = String.Empty

        For I As Integer = rDlen - 1 To 0 Step -1
            hexString += rData(I).ToString("X2")
        Next

        ActualTemperature = Integer.Parse(hexString, Globalization.NumberStyles.HexNumber) - 64 ' 0x71h – 0x40h = 0x31h correspond to 49°C decimal
        If ActualTemperature < -40 OrElse ActualTemperature > 80 Then
            ' включить индикатор перегрева
            Me.NumericEditEncoderThermometer.BackColor = Color.Red
            AddLogMessage($"Температура энкодера = {ActualTemperature} вне разрешенного диапазона! ", ColorForAlarm.AlarmMessage)
        Else
            ' выключить индикатор перегрева
            NumericEditEncoderThermometer.BackColor = SystemColors.Control
            AddLogMessage($"Температура энкодера = {ActualTemperature} в допуске. ", ColorForAlarm.WriteOk)
        End If

        'If ActualTemperature < 0 Then
        '    ActualTemperature = 0
        'ElseIf ActualTemperature > 100 Then
        '    ActualTemperature = 100
        'End If
    End Sub

    Private Sub ButtonEncoder_Click(sender As Object, e As EventArgs) Handles ButtonEncoder.Click
        LoadEncoder()
    End Sub

    Private Sub ButtonResetPosition_Click(sender As Object, e As EventArgs) Handles ButtonResetPosition.Click
        ResetPositionEncoder()
    End Sub

#End Region

#Region "TaskRefreshControl"

    ''' <summary>
    ''' Один токен отмены должен относиться к одной отменяемой операции, однако эта операция может быть реализована в программе.
    ''' После того как свойство IsCancellationRequested токена примет значение true, для него невозможно будет восстановить значение false.
    ''' Таким образом, токены отмены невозможно использовать повторно после выполнения отмены.
    ''' </summary>
    Private tokenSource As CancellationTokenSource
    ''' <summary>
    ''' задача запроса значения положения энкодера
    ''' </summary>
    Private TaskGetMultiPDOData As Task
    ''' <summary>
    ''' задача обновления пользовательского интерфейса
    ''' </summary>
    Private TaskRefreshControl As Task
    'Private TaskRefreshTemperature As Task  ' задача обновления температуры
    Private taskIsRunning As Boolean
    ''' <summary>
    ''' минимальное время для массива COB-ID
    ''' </summary>
    Private Const TimePolling As UShort = 50
    ''' <summary>
    ''' время обновления контролов
    ''' </summary>
    Private Const TimeRefreshControl As Integer = 100
    ''' <summary>
    ''' время обновления температуры = 10 сек
    ''' </summary>
    Private Const TimeRefreshTemperature As Integer = 10000
    ''' <summary>
    ''' время ступеньки значения нового угла
    ''' </summary>
    Private Const TimeShowShutoff As Integer = 200

    Private Sub ButtonRunTask_Click(sender As Object, e As EventArgs) Handles ButtonRunTask.Click
        RunWorkTasks()
    End Sub

    Private Sub ButtonStopTask_Click(sender As Object, e As EventArgs) Handles ButtonStopTask.Click
        StopTask()
    End Sub

    ''' <summary>
    ''' Запустить задачу считывания позиции энкодера и проверки на переход на новый градус,
    ''' задачу отображения на индикаторе, включения таймера для контроля температуры.
    ''' </summary>
    Public Sub RunWorkTasks()
        SetPolling()

        If (ret = 0) Then
            tokenSource = New CancellationTokenSource
            TaskGetMultiPDOData = Task.Factory.StartNew(Sub() WorkGetMultiPDOData(tokenSource.Token), tokenSource.Token, TaskCreationOptions.LongRunning)
            TaskRefreshControl = Task.Factory.StartNew(Sub() WorkRefreshControl(tokenSource.Token), tokenSource.Token, TaskCreationOptions.LongRunning)
            'TaskRefreshTemperature = Task.Factory.StartNew(Sub() WorkRefreshTemperature(tokenSource.Token), tokenSource.Token, TaskCreationOptions.LongRunning)
            TimerTemperature.Interval = TimeRefreshTemperature
            TimerTemperature.Enabled = True
            taskIsRunning = True
            EnabledBottomTask(taskIsRunning)
            ' если пришли сюда значит энкодер подключен успешно
            IsEncoderLoadedAndRunSuccess = True
        End If
    End Sub

    ''' <summary>
    ''' Сбросить фоновые задачи и групповой опрос
    ''' </summary>
    Private Sub StopTask()
        If taskIsRunning Then
            ' 1. Выключить таймеры
            TimerTemperature.Enabled = False
            If tokenSource IsNot Nothing Then tokenSource.Cancel() ' прервать задачу
            ' 2. Выключить индикаторы
            taskIsRunning = False
            EnabledBottomTask(taskIsRunning)
            ' 3. Сбросить групповой опрос (при обрыве USB порта может быть ошибка)
            ResetPolling()
            IsEncoderLoadedAndRunSuccess = False
        End If
    End Sub

    ''' <summary>
    ''' Переключить кнопки запуска/остановки задач
    ''' </summary>
    ''' <param name="enabled"></param>
    Private Sub EnabledBottomTask(enabled As Boolean)
        ButtonRunTask.Enabled = Not enabled
        ButtonStopTask.Enabled = enabled
    End Sub

    ''' <summary>
    ''' Получение текущей позиции энкодера
    ''' </summary>
    ''' <param name="ct"></param>
    Private Sub WorkGetMultiPDOData(ByVal ct As CancellationToken)
        ' Прерывание было запрошено до запуска?
        If ct.IsCancellationRequested = True Then
            ct.ThrowIfCancellationRequested()
        End If

        Do
            If ct.IsCancellationRequested Then
                Exit Do ' завершить задачу
            End If

            GetMultiPDOData()

            Thread.Sleep(TimePolling)
        Loop While True
    End Sub

    ''' <summary>
    ''' Отобразить изменения на контролах
    ''' </summary>
    ''' <param name="ct"></param>
    Private Sub WorkRefreshControl(ByVal ct As CancellationToken)
        ' Прерывание было запрошено до запуска?
        If ct.IsCancellationRequested = True Then
            ct.ThrowIfCancellationRequested()
        End If

        Do
            If ct.IsCancellationRequested Then
                Exit Do ' завершить задачу
            End If

            ShowPositionEncoder()

            Thread.Sleep(TimeRefreshControl)
        Loop While True
    End Sub

    '''' <summary>
    '''' Отобразить температуру
    '''' </summary>
    '''' <param name="ct"></param>
    'Private Sub WorkRefreshTemperature(ByVal ct As CancellationToken)
    '    ' Прерывание было запрошено до запуска?
    '    If ct.IsCancellationRequested = True Then
    '        ct.ThrowIfCancellationRequested()
    '    End If

    '    Do
    '        If ct.IsCancellationRequested Then
    '            Exit Do ' завершить задачу
    '        End If

    '        ReadTemperature()
    '        ShowThermometer()

    '        Thread.Sleep(TimeRefreshTemperature)
    '    Loop While True
    'End Sub

    ''' <summary>
    ''' Не делал фоновую задачу, т.к. процесс измерения температуры редкий и не надо усложнений для работы в потоке UI
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub TimerTemperature_Tick(sender As Object, e As EventArgs) Handles TimerTemperature.Tick
        ReadTemperature()
        ShowThermometer()
    End Sub
    Public Property Отсечка() As Boolean

    ''' <summary>
    ''' предыдущее значение положения угла
    ''' </summary>
    Private oldValueAngle As Integer
    Private oldValueAngleDouble As Double

    ''' <summary>
    ''' Включить/выключить таймер
    ''' Включить/выключить индикатор отсечку нового угла
    ''' </summary>
    ''' <param name="Value"></param>
    Private Sub SetTimerLedShutoff(ByVal Value As Boolean)
        If InvokeRequired Then
            Invoke(New MethodInvoker(Sub() SetTimerLedShutoff(Value)))
        Else
            If Value Then
                TimerLedShutoff.Enabled = True
            End If

            LedInterrupter.Value = Value
            Отсечка = Value
        End If
    End Sub

    ''' <summary>
    ''' Выключить таймер.
    ''' Выключить индикатор отсечки нового угла.
    ''' </summary>
    ''' <param name="source"></param>
    ''' <param name="e"></param>
    Private Sub OnTimedEvent(source As Object, e As System.Timers.ElapsedEventArgs)
        TimerLedShutoff.Enabled = False
        SetTimerLedShutoff(False)
    End Sub

    ''' <summary>
    ''' Проверить на новую ступень изменения целого значения угла
    ''' </summary>
    ''' <param name="newValueAngle"></param>
    Private Sub CheckSwitchingStage(newValueAngle As Double)
        Dim difference As Integer

        If newValueAngle > 0 Then
            difference = Math.Abs(Convert.ToInt32(Math.Truncate(newValueAngle)) - oldValueAngle)
        Else
            difference = Math.Abs(oldValueAngle - Convert.ToInt32(Math.Truncate(newValueAngle)))
        End If

        If difference >= 1.0 Then
            oldValueAngle = Convert.ToInt32(Math.Truncate(newValueAngle))
            SetTimerLedShutoff(True)
        End If

        If newValueAngle >= -1 AndAlso newValueAngle <= 1 Then
            ' переход через 0
            If Math.Sign(newValueAngle) <> Math.Sign(oldValueAngleDouble) Then
                SetTimerLedShutoff(True)
            End If
        End If

        oldValueAngleDouble = newValueAngle
    End Sub

#End Region

End Class
