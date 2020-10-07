Imports System.Windows.Forms
Imports System.Data.OleDb
Imports System.IO

Friend Class FormDBExplorer
    Private Enum WhatBase
        InputBase
        OutputBase
    End Enum
    Private удаляемаяБаза As WhatBase

    Private isDataBaseLoaded As Boolean
    Private источникЗаписей As New CollectionTemperatureField
    Private приемникЗаписей As New CollectionTemperatureField

    Private keyIDГребенкаА As Integer
    Private keyIDГребенкаБ As Integer
    Private keyКодИзделия As Integer
    Private keyID As Integer

    Private Const LW_WIDHT As Integer = 58
    Private pathOption As String

    Private Sub DBExplorerForm_Load(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Load
        ''TODO: This line of code loads data into the 'КамераInputDataSet.Точка' table. You can move, or remove it, as needed.
        pathOption = PathResourses & "\Камера.xml"
        EnabledItemToolStrip(False)
    End Sub

    Private Sub DBExplorerForm_FormClosing(ByVal sender As Object, ByVal e As FormClosingEventArgs) Handles Me.FormClosing
        If Not gMainFomMdiParent.IsWindowClosed Then
            e.Cancel = True
        End If
    End Sub

    Private Sub DBExplorerForm_FormClosed(ByVal sender As Object, ByVal e As FormClosedEventArgs) Handles Me.FormClosed
        InputTableAdapterConnectionClose()
        OutputTableAdapterConnectionClose()
    End Sub

    ''' <summary>
    ''' Вкл/Выкл кнопки панели инструментов
    ''' </summary>
    ''' <param name="enabled"></param>
    Private Sub EnabledItemToolStrip(ByVal enabled As Boolean)
        MenuStrip.Enabled = enabled

        For Each tsItem As ToolStripItem In ToolStrip.Items
            If tsItem IsNot LoadToolStripButton Then
                tsItem.Enabled = enabled
            End If
        Next
    End Sub

    Private Sub LoadToolStripButton_Click(ByVal sender As Object, ByVal e As EventArgs) Handles LoadToolStripButton.Click
        LosdDataBase()
    End Sub

    ''' <summary>
    ''' Заполнение отсоединённых наборов данных и деревьев проводника
    ''' и очистка от пустых записей
    ''' </summary>
    Private Sub LosdDataBase()
        ' проверка файлов в каталоге
        If Not CheckExistsFile(pathOption) Then Exit Sub

        ' если имена sFileИсточник и sFileПриемник совпадают, то обнулить
        LoadPathToBase()

        If Not CheckExistsFile(gFileИсточник) Then gFileИсточник = ""
        If Not CheckExistsFile(gFileПриемник) Then gFileПриемник = ""
        If gFileИсточник = gFileПриемник Then gFileПриемник = ""

        TextPathSource.Text = gFileИсточник
        TextPathReceiver.Text = gFileПриемник

        ' если файлы отсутствуют, то запустить cmdПросмотрПриемника_Click cmdПросмотрИсточника_Click
        If gFileИсточник = "" Then ButtonReviewSource_Click(ButtonReviewSource, New EventArgs())
        If gFileПриемник = "" Then ButtonReviewReceiver_Click(ButtonReviewReceiver, New EventArgs())

        ' Конфигурация ListView control.
        ListViewInput.View = View.Details
        ListViewOutput.View = View.Details

        Dim mNodeRoot As TreeNode ' Переменная Уровня модуля для Узлов(Nodes)
        FillAllAdapters()

        Try
            ' Конфигурация TreeView
            'tvwDBДеревоИсточник.Sorted = True
            'tvwDBДеревоПриемник.Sorted = True

            mNodeRoot = TreeViewInput.Nodes.Add("Kor", "Изделия") ' корневой
            mNodeRoot.Tag = "Kor" 'mDbКамераИсточник.Name
            mNodeRoot.ImageKey = "closed"
            TreeViewInput.LabelEdit = False

            mNodeRoot = TreeViewOutput.Nodes.Add("Kor", "Изделия") ' корневой
            mNodeRoot.Tag = "Kor" 'mDbКамераИсточник.Name
            mNodeRoot.ImageKey = "closed"
            TreeViewOutput.LabelEdit = False
            'дерево и лист источника или приемника обновляются
            PopulateTree(TreeViewInput, КамераInputDataSet)
            PopulateTree(TreeViewOutput, КамераOutputDataSet)

            '*****************
            isDataBaseLoaded = True
            PopulateListBox()
            УдалитьИзделияБезПолей()
            ' если здесь, значит все нормально
            SplitContainerAll.Enabled = True
            EnabledItemToolStrip(True)
            LoadToolStripButton.Visible = False

        Catch ex As Exception
            MessageBox.Show(ex.ToString)
        End Try
    End Sub

    ''' <summary>
    ''' Заполняются адаптеры источника и приёмника
    ''' </summary>
    Private Sub FillAllAdapters()
        FillAdapterInput()
        FillAdapterOutput()
    End Sub

    ''' <summary>
    ''' Заполняется адаптер источника
    ''' </summary>
    Private Sub FillAdapterInput()
        Dim mOleDbInputConnection As OleDbConnection = New OleDbConnection(BuildCnnStr(PROVIDER_JET, gFileИсточник))

        ГребенкаАInputTableAdapter.Connection = mOleDbInputConnection
        ГребенкаБInputTableAdapter.Connection = mOleDbInputConnection
        ИзделиеInputTableAdapter.Connection = mOleDbInputConnection
        КонтрольЭДСInputTableAdapter.Connection = mOleDbInputConnection
        ПолеInputTableAdapter.Connection = mOleDbInputConnection
        ТочкаInputTableAdapter.Connection = mOleDbInputConnection
        ГорелкаInputTableAdapter.Connection = mOleDbInputConnection

        'стандартные методы заполнения, их в коде заменим на созданные
        ГребенкаАInputTableAdapter.Fill(КамераInputDataSet.ГребенкаА)
        ГребенкаБInputTableAdapter.Fill(КамераInputDataSet.ГребенкаБ)
        ИзделиеInputTableAdapter.Fill(КамераInputDataSet.Изделие)
        КонтрольЭДСInputTableAdapter.Fill(КамераInputDataSet.КонтрольЭДС)
        ПолеInputTableAdapter.Fill(КамераInputDataSet.Поле)
        ТочкаInputTableAdapter.Fill(КамераInputDataSet.Точка)
        ГорелкаInputTableAdapter.Fill(КамераInputDataSet.Горелка)

        ShowPropertiesBaseToStatusBar(mOleDbInputConnection, 0)
    End Sub

    ''' <summary>
    ''' Заполняется адаптер приёмника
    ''' </summary>
    Private Sub FillAdapterOutput()
        Dim mOleDbOutputConnection As OleDbConnection = New OleDbConnection(BuildCnnStr(PROVIDER_JET, gFileПриемник))

        ГребенкаАOutputTableAdapter.Connection = mOleDbOutputConnection
        ГребенкаБOutputTableAdapter.Connection = mOleDbOutputConnection
        ИзделиеOutputTableAdapter.Connection = mOleDbOutputConnection
        КонтрольЭДСOutputTableAdapter.Connection = mOleDbOutputConnection
        ПолеOutputTableAdapter.Connection = mOleDbOutputConnection
        ТочкаOutputTableAdapter.Connection = mOleDbOutputConnection
        ГорелкаOutputTableAdapter.Connection = mOleDbOutputConnection

        ГребенкаАOutputTableAdapter.Fill(КамераOutputDataSet.ГребенкаА)
        ГребенкаБOutputTableAdapter.Fill(КамераOutputDataSet.ГребенкаБ)
        ИзделиеOutputTableAdapter.Fill(КамераOutputDataSet.Изделие)
        КонтрольЭДСOutputTableAdapter.Fill(КамераOutputDataSet.КонтрольЭДС)
        ПолеOutputTableAdapter.Fill(КамераOutputDataSet.Поле)
        ТочкаOutputTableAdapter.Fill(КамераOutputDataSet.Точка)
        ГорелкаOutputTableAdapter.Fill(КамераOutputDataSet.Горелка)

        ShowPropertiesBaseToStatusBar(mOleDbOutputConnection, 3)
    End Sub

    ''' <summary>
    ''' Закрыть соединения входной таблицы
    ''' </summary>
    Private Sub InputTableAdapterConnectionClose()
        ГребенкаАInputTableAdapter.Connection.Close()
        ГребенкаБInputTableAdapter.Connection.Close()
        ИзделиеInputTableAdapter.Connection.Close()
        КонтрольЭДСInputTableAdapter.Connection.Close()
        ПолеInputTableAdapter.Connection.Close()
        ТочкаInputTableAdapter.Connection.Close()
        ГорелкаInputTableAdapter.Connection.Close()
    End Sub

    ''' <summary>
    ''' Закрыть соединения выходной таблицы
    ''' </summary>
    Private Sub OutputTableAdapterConnectionClose()
        ГребенкаАOutputTableAdapter.Connection.Close()
        ГребенкаБOutputTableAdapter.Connection.Close()
        ИзделиеOutputTableAdapter.Connection.Close()
        КонтрольЭДСOutputTableAdapter.Connection.Close()
        ПолеOutputTableAdapter.Connection.Close()
        ТочкаOutputTableAdapter.Connection.Close()
        ГорелкаOutputTableAdapter.Connection.Close()
    End Sub

#Region "EventsClick"
    Private Sub ToolBarToolStripMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs) Handles ToolBarToolStripMenuItem.Click
        ToolBarToolStripMenuItem.Checked = Not ToolBarToolStripMenuItem.Checked
        ToolStrip.Visible = ToolBarToolStripMenuItem.Checked
    End Sub

    Private Sub StatusBarToolStripMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs) Handles StatusBarToolStripMenuItem.Click
        StatusBarToolStripMenuItem.Checked = Not StatusBarToolStripMenuItem.Checked
        sbrСостояния.Visible = StatusBarToolStripMenuItem.Checked
    End Sub

    ''' <summary>
    ''' установить надо или нет показывать панели
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub ToggleFoldersVisible()
        ' вначале установить menu item
        FoldersToolStripMenuItem.Checked = Not FoldersToolStripMenuItem.Checked

        ' синхронизировать
        FoldersToolStripButton.Checked = FoldersToolStripMenuItem.Checked

        ' Закрыть панель.
        SplitContainerTreeInput.Panel1Collapsed = Not FoldersToolStripMenuItem.Checked
        SplitContainerTreeOutput.Panel1Collapsed = Not FoldersToolStripMenuItem.Checked
    End Sub

    Private Sub FoldersToolStripMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs) Handles FoldersToolStripMenuItem.Click
        ToggleFoldersVisible()
    End Sub

    Private Sub FoldersToolStripButton_Click(ByVal sender As Object, ByVal e As EventArgs) Handles FoldersToolStripButton.Click
        ToggleFoldersVisible()
    End Sub

    Private Sub SetView(ByVal View As View)
        ' отметить, какое menu item должно быть отмечено
        Dim MenuItemToCheck As ToolStripMenuItem = Nothing
        Select Case View
            Case View.Details
                MenuItemToCheck = DetailsToolStripMenuItem
            Case View.LargeIcon
                MenuItemToCheck = LargeIconsToolStripMenuItem
            Case View.List
                MenuItemToCheck = ListToolStripMenuItem
            Case View.SmallIcon
                MenuItemToCheck = SmallIconsToolStripMenuItem
            Case View.Tile
                MenuItemToCheck = TileToolStripMenuItem
            Case Else
                Debug.Fail("Unexpected View")
                View = View.Details
                MenuItemToCheck = DetailsToolStripMenuItem
        End Select

        ' отметить подходящее меню а с других снять выделение
        For Each MenuItem As ToolStripMenuItem In ListViewToolStripButton.DropDownItems
            If MenuItem Is MenuItemToCheck Then
                MenuItem.Checked = True
            Else
                MenuItem.Checked = False
            End If
        Next

        ' в конце установить требуемый вид
        ListViewInput.View = View
        ListViewOutput.View = View
    End Sub

    Private Sub ListToolStripMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs) Handles ListToolStripMenuItem.Click
        SetView(View.List)
    End Sub

    Private Sub DetailsToolStripMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs) Handles DetailsToolStripMenuItem.Click
        SetView(View.Details)
    End Sub

    Private Sub LargeIconsToolStripMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs) Handles LargeIconsToolStripMenuItem.Click
        SetView(View.LargeIcon)
    End Sub

    Private Sub SmallIconsToolStripMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs) Handles SmallIconsToolStripMenuItem.Click
        SetView(View.SmallIcon)
    End Sub

    Private Sub TileToolStripMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs) Handles TileToolStripMenuItem.Click
        SetView(View.Tile)
    End Sub

    Private Sub DeleteToolStripButton_Click(ByVal eventSender As Object, ByVal eventArgs As EventArgs) Handles DeleteToolStripButton.Click
        Dim Button As ToolStripItem = CType(eventSender, ToolStripItem)
        'Select Case Button.Name
        Select Case Button.Tag.ToString
            'Case "New"
            '    NewToolStripMenuItem_Click(CleanInputToolStripMenuItem, New System.EventArgs())
            'Case "Save"
            '    SaveToolStripMenuItem_Click(SaveAsInputToolStripMenuItem, New System.EventArgs())
            Case "Delete"
                DeleteActiveBase()
        End Select
    End Sub

    'Private Sub button1_Click(ByVal sender As Object, ByVal e As System.EventArgs)
    '    Dim myStream As Stream
    '    Dim saveFileDialog1 As New SaveFileDialog()

    '    saveFileDialog1.Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*"
    'saveFileDialog1.InitialDirectory = My.Computer.FileSystem.SpecialDirectories.MyDocuments

    '    saveFileDialog1.FilterIndex = 2
    '    saveFileDialog1.RestoreDirectory = True

    '    If saveFileDialog1.ShowDialog() = DialogResult.OK Then
    '        myStream = saveFileDialog1.OpenFile()
    '        If (myStream IsNot Nothing) Then
    '            ' Code to write the stream goes here.
    '            myStream.Close()
    '        End If
    '    End If
    'End Sub

    Private Sub SaveAsInputToolStripMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs) Handles SaveAsInputToolStripMenuItem.Click
        SaveBaseAsInputOutput(WhatBase.InputBase)
    End Sub

    Private Sub SaveAsOutputToolStripMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs) Handles SaveAsOutputToolStripMenuItem.Click
        SaveBaseAsInputOutput(WhatBase.OutputBase)
    End Sub

    ''' <summary>
    ''' Скопировать файл базы в указанную папку
    ''' </summary>
    ''' <param name="inWhatBase"></param>
    Private Sub SaveBaseAsInputOutput(ByVal inWhatBase As WhatBase)
        Dim путьСохраняемойБазы As String = String.Empty
        Dim sFile As String

        With FolderBrowserDialog1
            Select Case inWhatBase
                Case WhatBase.InputBase
                    путьСохраняемойБазы = TextPathSource.Text
                    .Description = "Выберите папку для сохранение текущей базы данных"
                    Exit Select
                Case WhatBase.OutputBase
                    путьСохраняемойБазы = TextPathReceiver.Text
                    .Description = "Выберите папку для сохранение архивной базы данных"
                    Exit Select
            End Select

            Dim MyResult As DialogResult = .ShowDialog(Me)

            If MyResult = DialogResult.Cancel Then
                MsgBox("Error")
                Exit Sub
            End If

            If Len(.SelectedPath) = 0 Then
                Exit Sub
            End If

            sFile = .SelectedPath
        End With

        File.Copy(путьСохраняемойБазы, sFile & "\Камера.mdb", True)
    End Sub

    Private Sub CleanInputToolStripMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs) Handles CleanInputToolStripMenuItem.Click
        CleanBaseInputOutput(WhatBase.InputBase)
    End Sub

    Private Sub CleanOutputToolStripMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs) Handles CleanOutputToolStripMenuItem.Click
        CleanBaseInputOutput(WhatBase.OutputBase)
    End Sub

    Private Sub CleanBaseInputOutput(ByVal inWhatBase As WhatBase)
        Dim путьОчищаемойемойБазы As String = String.Empty

        If MessageBox.Show($"Внимание! Если Вы не произвели предварительное сохранение базы, то все записи будут уничтожены.{Environment.NewLine}Вы уверены в удалении всех записей?",
                           "Очистка базы", MessageBoxButtons.YesNo,
                           MessageBoxIcon.Question,
                           MessageBoxDefaultButton.Button1) = Windows.Forms.DialogResult.Yes Then

            Select Case inWhatBase
                Case WhatBase.InputBase
                    путьОчищаемойемойБазы = TextPathSource.Text
                    Exit Select
                Case WhatBase.OutputBase
                    путьОчищаемойемойБазы = TextPathReceiver.Text
                    Exit Select
            End Select

            Try
                ' код копирования базы данных My.Application.Info.DirectoryPath & "\Шаблон\Камера.mdb"
                File.Copy(PathResourses & "\Шаблон\Камера.mdb", путьОчищаемойемойБазы, True)
            Catch ex As Exception
                MessageBox.Show(ex.ToString, "Ошибка очистки базы данных", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Finally
                ' востановление ссылок
                Select Case inWhatBase
                    Case WhatBase.InputBase
                        FillAdapterInput()
                        ClearTreeView(TreeViewInput, ListViewInput)
                        PopulateTree(TreeViewInput, КамераInputDataSet)
                        Exit Select
                    Case WhatBase.OutputBase
                        FillAdapterOutput()
                        ClearTreeView(TreeViewOutput, ListViewOutput)
                        PopulateTree(TreeViewOutput, КамераOutputDataSet)
                        Exit Select
                End Select

                PopulateListBox()
            End Try
        End If
    End Sub

    Private Sub PackInputToolStripMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs) Handles PackInputToolStripMenuItem.Click
        CompressAndRestoreBase(WhatBase.InputBase)
    End Sub

    Private Sub PackOutputToolStripMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs) Handles PackOutputToolStripMenuItem.Click
        CompressAndRestoreBase(WhatBase.OutputBase)
    End Sub

    Private Sub ButtonReviewSource_Click(ByVal eventSender As Object, ByVal eventArgs As EventArgs) Handles ButtonReviewSource.Click
        With DialogOpen
            .Title = "Открытие базы данных источника"
            ' установить флаг атрибутов
            .Filter = "База испытаний (Камера.mdb)|Камера.mdb"
            .FilterIndex = 2
            .RestoreDirectory = True

            Dim myResult As DialogResult = .ShowDialog()

            If myResult = DialogResult.Cancel Then
                MsgBox("Error")
                Exit Sub
            End If

            If Len(.FileName) = 0 Then Exit Sub

            gFileИсточник = .FileName
            TextPathSource.Text = gFileИсточник
            SavePathToBase()
        End With

        ' если форма уже загружена код открытия базы
        If isDataBaseLoaded Then
            'mDbКамераИсточник = mWksКамераИсточник.OpenDatabase(sFileИсточник)
            FillAdapterInput()
            ClearTreeView(TreeViewInput, ListViewInput)
            PopulateTree(TreeViewInput, КамераInputDataSet)
            PopulateListBox()
            УдалитьИзделияБезПолей()
        End If
    End Sub

    Private Sub ButtonReviewReceiver_Click(ByVal eventSender As Object, ByVal eventArgs As EventArgs) Handles ButtonReviewReceiver.Click
        With DialogOpen
            .Title = "Открытие базы данных приемника"
            ' установить флаг атрибутов
            .Filter = "База испытаний (Камера.mdb)|Камера.mdb"
            .FilterIndex = 2
            .RestoreDirectory = True

            Dim myResult As DialogResult = .ShowDialog()

            If myResult = DialogResult.Cancel Then
                MsgBox("Error")
                Exit Sub
            End If

            If Len(.FileName) = 0 Then Exit Sub

            gFileПриемник = .FileName
            TextPathReceiver.Text = gFileПриемник
            SavePathToBase()
        End With

        ' если форма уже загружена код открытия базы
        If isDataBaseLoaded Then
            'mDbКамераПриемник = mWksКамераПриемник.OpenDatabase(sFileПриемник)
            FillAdapterOutput()
            ClearTreeView(TreeViewOutput, ListViewOutput)
            PopulateTree(TreeViewOutput, КамераOutputDataSet)
            PopulateListBox()
            УдалитьИзделияБезПолей()
        End If
    End Sub
#End Region

    Private Sub PopulateListBox()
        ' в листе источника появляются только те изделия, которых нет в приемнике
        ' 1 происходит в первый раз после загрузки программы
        ' 2 затем после каждого обновления базы при cmdПросмотрИсточника и cmdПросмотрПриемника
        ' 3 после переноса при нажатии cmdИзИстосникаВПриемник и cmdИзПриемникаВИсточник
        ' 4 после удаления cmdУдалитьИсточник и cmdУдалитьПриемник
        ' цикл по узлам каждого дерева с тегом mNodeИсточник.Tag = "Изделия" и заполнение массивов источника и приемника
        ListBoxInput.Items.Clear()
        ListBoxOutput.Items.Clear()
        ' очистка коллекций
        источникЗаписей.Clear()
        приемникЗаписей.Clear()

        ' добавляем в коллекции
        ' в примечание не только номера изделий, но и дату и поля
        Dim strДатаИспытанияПоле As String
        Dim удалить, естьУдаляемыеИзИсточника, естьУдаляемыеИзПриемника As Boolean

        For Each mNode As TreeNode In TreeViewInput.Nodes("Kor").Nodes
            If mNode.Tag.ToString = "Изделия" Then
                strДатаИспытанияПоле = ДатаИспытанияПолей(КамераInputDataSet, CStr(Val(mNode.Name)), удалить)
                If удалить Then естьУдаляемыеИзИсточника = True
                источникЗаписей.Add(mNode.Text, $"Камера:{mNode.Text}{strДатаИспытанияПоле}", удалить, CInt(Val(mNode.Name)))
                'lstИсточник.Items.Add(colИсточник.Item((colИсточник.Count - 1)).Примечание)
            End If
        Next

        'lstИсточник.Items.AddRange(colИсточник.GetAll) пока не добавляем
        For Each mNode As TreeNode In TreeViewOutput.Nodes("Kor").Nodes
            If mNode.Tag.ToString = "Изделия" Then
                strДатаИспытанияПоле = ДатаИспытанияПолей(КамераOutputDataSet, CStr(Val(mNode.Name)), удалить)
                If удалить Then естьУдаляемыеИзПриемника = True
                приемникЗаписей.Add((mNode.Text), $"Камера:{mNode.Text}{strДатаИспытанияПоле}", удалить, CInt(Val(mNode.Name)))
                'lstПриемник.Items.Add(colПриемник.Item((colПриемник.Count - 1)).Примечание)
            End If
        Next

        ListBoxOutput.Items.AddRange(приемникЗаписей.GetAll)

        If естьУдаляемыеИзИсточника OrElse естьУдаляемыеИзПриемника Then
            ListBoxInput.Items.AddRange(источникЗаписей.GetAll)
            ' значит загрузка в первый раз или после смены базы Источника Приемника
            ' выйти потому, что затем выделение и удаление помеченных УдалитьИзделияБезПолей
            Exit Sub
        End If

        ' цикл по массиву источника и поиск в цикле в приемнике
        ' если найден в поле сделать логическую отметку на удаление
        For Each cЭкземплярИсточник As TemperatureField In источникЗаписей
            For Each cЭкземплярПриемник As TemperatureField In приемникЗаписей
                If cЭкземплярПриемник.Comment = cЭкземплярИсточник.Comment Then
                    cЭкземплярИсточник.IsDelete = True
                    'Exit For
                End If
            Next
        Next

        ' в цикле с пометками на удаление удалить
        For I As Integer = источникЗаписей.Count - 1 To 0 Step -1
            If источникЗаписей.Item(I).IsDelete = True Then источникЗаписей.Remove(I)
        Next

        'перенести в lstИсточник
        ListBoxInput.Items.AddRange(источникЗаписей.GetAll)
    End Sub

    Private Sub УдалитьИзделияБезПолей()
        If ВыделитьУдаляемые(ListBoxInput) Then ButtonDeleteInputOutput_Click(ButtonDeleteInput, New EventArgs)
        If ВыделитьУдаляемые(ListBoxOutput) Then ButtonDeleteInputOutput_Click(ButtonDeleteOutput, New EventArgs)
    End Sub

    Private Function ВыделитьУдаляемые(ByRef vListBox As ListBox) As Boolean
        For I As Integer = 0 To vListBox.Items.Count - 1
            If CType(vListBox.Items(I), TemperatureField).IsDelete Then
                vListBox.SetSelected(I, True)
            End If
        Next

        Return vListBox.SelectedItems.Count > 0
    End Function

    'Private Sub listBox1_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles listBox1.SelectedIndexChanged
    '    imgPanel.ClearImages()
    '    Dim selected(listBox1.SelectedIndices.Count - 1) As Integer
    '    listBox1.SelectedIndices.CopyTo(selected, 0)
    '    Dim i As Integer

    '    For i = 0 To selected.Length - 1
    '        Dim index As Integer = selected(i)
    '        Dim item As Object = listBox1.Items(index)
    '        Dim str As String = item.ToString()
    '        Dim img As Image = GetImage(str)
    '        imgPanel.AddImage(img)
    '    Next i
    '    '
    '    ' To update (paint) the image area, call the invalidate method.
    '    ' This causes the onPaint method for that control to be called.
    '    '
    '    imgPanel.Invalidate()
    'End Sub

    Private Sub ButtonMove_Click(ByVal sender As Object, ByVal e As EventArgs) Handles ButtonMoveInputToOutput.Click, ButtonMoveOutputToInput.Click
        If CType(sender, Button) Is ButtonMoveInputToOutput Then
            MoveRecord(WhatBase.InputBase)
        Else
            MoveRecord(WhatBase.OutputBase)
        End If
    End Sub

    ''' <summary>
    ''' Переместить записи испытаний между базами
    ''' с удалением последних из источников.
    ''' </summary>
    ''' <param name="откуда"></param>
    Private Sub MoveRecord(откуда As WhatBase)
        Dim mListBox As ListBox
        Dim dsMemoDataSetInput As КамераDataSet
        Dim dsMemoDataSetOutput As КамераDataSet
        Dim запистьВInput As Boolean
        Dim percentPosition As Double
        Dim rowsCount As Double

        Select Case откуда
            Case WhatBase.InputBase
                запистьВInput = False
                mListBox = ListBoxInput
                dsMemoDataSetInput = КамераInputDataSet
                dsMemoDataSetOutput = КамераOutputDataSet
                Exit Select
            Case Else 'WhatBase.OutputBase
                запистьВInput = True
                mListBox = ListBoxOutput
                dsMemoDataSetInput = КамераOutputDataSet
                dsMemoDataSetOutput = КамераInputDataSet
                Exit Select
        End Select

        If mListBox.SelectedItems.Count = 0 Then Exit Sub

        ' в цикле по новой коллекции считываем коды изделий и по ним 2 кода горелок
        ' переносим поля и удаляем горелки или просто удаляем горелки
        TSProgressBar.Value = 0
        TSProgressBar.Visible = True
        rowsCount = mListBox.SelectedItems.Count
        ПриписатьОтображенияАдаптеров(dsMemoDataSetOutput, запистьВInput)

        For Each itemЗапись As TemperatureField In mListBox.SelectedItems
            СчитатьIDТермопар(dsMemoDataSetInput, itemЗапись.Count.ToString)
            keyКодИзделия = itemЗапись.Count
            ' получили 2 ID термопар
            ' считываем всю информацию по 1 и 2 гребенки
            ' считываем всю информацию по изделию, 5 строк ЭДС
            ' узнать сколько полей
            ' в цикле по полям:
            '   считываем всю информацию по полю
            '   в цикле по 28 горелкам считать мин, мах, среднее
            '   считывание 5 контрольных точек
            'с копировать из источника в приемник
            ЗаписатьКамеруВMemoDataSet(СчитатьКамеруИзMemoDataSet(dsMemoDataSetInput))
            ' удалить источник
            УдалитьКамеруИзMemoDataSet(dsMemoDataSetInput)
            percentPosition += 1
            TSProgressBar.Value = CInt((percentPosition / rowsCount) * 100)
        Next

        TSProgressBar.Visible = False

        ' физическое удаление из базы
        Select Case откуда
            Case WhatBase.InputBase
                ГребенкаАTableAdapterUpdate(КамераInputDataSet, ГребенкаАInputTableAdapter, ГребенкаАInputBindingSource)
                ГребенкаБTableAdapterUpdate(КамераInputDataSet, ГребенкаБInputTableAdapter, ГребенкаБInputBindingSource)
                Exit Select
            Case Else 'WhatBase.OutputBase
                ГребенкаАTableAdapterUpdate(КамераOutputDataSet, ГребенкаАOutputTableAdapter, ГребенкаАOutputBindingSource)
                ГребенкаБTableAdapterUpdate(КамераOutputDataSet, ГребенкаБOutputTableAdapter, ГребенкаБOutputBindingSource)
                Exit Select
        End Select

        FillAllAdapters()
        ClearTreeView(TreeViewInput, ListViewInput)
        ClearTreeView(TreeViewOutput, ListViewOutput)
        PopulateTree(TreeViewInput, КамераInputDataSet)
        PopulateTree(TreeViewOutput, КамераOutputDataSet)
        PopulateListBox()
    End Sub

    Private Sub ButtonDeleteInputOutput_Click(ByVal sender As Object, ByVal e As EventArgs) Handles ButtonDeleteInput.Click, ButtonDeleteOutput.Click
        If CType(sender, Button) Is ButtonDeleteInput Then
            DeleteRecord(WhatBase.InputBase)
        Else
            DeleteRecord(WhatBase.OutputBase)
        End If
    End Sub

    ''' <summary>
    ''' Удаление записей испытаний в открытых базах данных
    ''' </summary>
    ''' <param name="откуда"></param>
    Private Sub DeleteRecord(откуда As WhatBase)
        Dim dsКамераDataSet As КамераDataSet
        Dim tvwDBДерево As TreeView
        Dim vwDBЛист As ListView
        Dim mListBox As ListBox

        Select Case откуда
            Case WhatBase.InputBase
                dsКамераDataSet = КамераInputDataSet
                tvwDBДерево = TreeViewInput
                vwDBЛист = ListViewInput
                mListBox = ListBoxInput

                Exit Select
            Case Else 'WhatBase.OutputBase
                dsКамераDataSet = КамераOutputDataSet
                tvwDBДерево = TreeViewOutput
                vwDBЛист = ListViewOutput
                mListBox = ListBoxOutput

                Exit Select
        End Select

        If mListBox.SelectedItems.Count = 0 Then Exit Sub

        ' в цикле по новой коллекции считываем коды изделий и по ним 2 кода горелок
        ' переносим поля и удаляем горелки или просто удаляем горелки
        For Each selectedЗапись As TemperatureField In mListBox.SelectedItems
            СчитатьIDТермопар(dsКамераDataSet, selectedЗапись.Count.ToString)
            ' удалить источник
            УдалитьКамеруИзMemoDataSet(dsКамераDataSet)
        Next

        ' физическое удаление из базы
        Select Case откуда
            Case WhatBase.InputBase
                ГребенкаАTableAdapterUpdate(КамераInputDataSet, ГребенкаАInputTableAdapter, ГребенкаАInputBindingSource)
                ГребенкаБTableAdapterUpdate(КамераInputDataSet, ГребенкаБInputTableAdapter, ГребенкаБInputBindingSource)
                FillAdapterInput()

                Exit Select
            Case Else 'WhatBase.OutputBase
                ГребенкаАTableAdapterUpdate(КамераOutputDataSet, ГребенкаАOutputTableAdapter, ГребенкаАOutputBindingSource)
                ГребенкаБTableAdapterUpdate(КамераOutputDataSet, ГребенкаБOutputTableAdapter, ГребенкаБOutputBindingSource)
                FillAdapterOutput()

                Exit Select
        End Select

        ClearTreeView(tvwDBДерево, vwDBЛист)
        PopulateTree(tvwDBДерево, dsКамераDataSet)
        PopulateListBox()
    End Sub

    ''' <summary>
    ''' Очистить дерево листа
    ''' </summary>
    ''' <param name="tvwDBДерево"></param>
    ''' <param name="lvwDBЛист"></param>
    Private Sub ClearTreeView(ByRef tvwDBДерево As TreeView, ByRef lvwDBЛист As ListView)
        tvwDBДерево.Nodes("Kor").Nodes.Clear()
        lvwDBЛист.Items.Clear()
    End Sub

    ''' <summary>
    ''' Создать строку по испытаниям всех полей исзделия
    ''' </summary>
    ''' <param name="dsКамераDataSet"></param>
    ''' <param name="кодИзделия"></param>
    ''' <param name="blnУдалить"></param>
    ''' <returns></returns>
    Private Function ДатаИспытанияПолей(ByRef dsКамераDataSet As КамераDataSet, ByVal кодИзделия As String, ByRef blnУдалить As Boolean) As String
        Dim info As String
        Dim rowИзделие As КамераDataSet.ИзделиеRow = dsКамераDataSet.Изделие.FindByКодИзделия(CInt(кодИзделия))
        Dim rowsПоле() As КамераDataSet.ПолеRow = CType(dsКамераDataSet.Поле.Select("КодИзделия = " & кодИзделия), КамераDataSet.ПолеRow())

        info = $"{vbTab}({rowИзделие.Дата.ToShortDateString})"
        blnУдалить = rowsПоле.Count = 0

        If rowsПоле.Count > 0 Then
            For Each ПолеRow As КамераDataSet.ПолеRow In rowsПоле
                With ПолеRow
                    info &= " Поле:" & .НомерПоля.ToString
                    info &= " Запуск:" & .ВремяЗапуска
                    '            strСтрока = strСтрока & " Ход:" & ![ВремяХодаТурели]
                End With
            Next
        End If

        Return info
    End Function

    ''' <summary>
    ''' Запомнить ключи записей гребёнок А и Б для использования их при каскадном удалении всех записей таблиц испытания камеры.
    ''' </summary>
    ''' <param name="dsКамераDataSet"></param>
    ''' <param name="кодИзделия"></param>
    Private Sub СчитатьIDТермопар(ByRef dsКамераDataSet As КамераDataSet, ByVal кодИзделия As String)
        Dim rowИзделие As КамераDataSet.ИзделиеRow = dsКамераDataSet.Изделие.FindByКодИзделия(CInt(кодИзделия))

        If rowИзделие IsNot Nothing Then
            keyIDГребенкаА = rowИзделие.КодГребенкиА
            keyIDГребенкаБ = rowИзделие.КодГребенкиБ
        End If
    End Sub

    ' Временные адаптеры для ссылки на источник или приёмник
    Private гребенкаАMemoTableAdapter As КамераDataSetTableAdapters.ГребенкаАTableAdapter
    Private гребенкаБMemoTableAdapter As КамераDataSetTableAdapters.ГребенкаБTableAdapter
    Private изделиеMemoTableAdapter As КамераDataSetTableAdapters.ИзделиеTableAdapter
    Private контрольЭДСMemoTableAdapter As КамераDataSetTableAdapters.КонтрольЭДСTableAdapter
    Private полеMemoTableAdapter As КамераDataSetTableAdapters.ПолеTableAdapter
    Private точкаMemoTableAdapter As КамераDataSetTableAdapters.ТочкаTableAdapter
    Private горелкаMemoTableAdapter As КамераDataSetTableAdapters.ГорелкаTableAdapter

    'Dim ИзделиеBindingSource As System.Windows.Forms.BindingSource
    'Dim ГорелкаBindingSource As System.Windows.Forms.BindingSource
    'Dim ГребенкаАBindingSource As System.Windows.Forms.BindingSource
    'Dim ГребенкаБBindingSource As System.Windows.Forms.BindingSource
    'Dim КонтрольЭДСBindingSource As System.Windows.Forms.BindingSource
    'Dim ПолеBindingSource As System.Windows.Forms.BindingSource
    'Dim ТочкаBindingSource As System.Windows.Forms.BindingSource

    Private Sub ПриписатьОтображенияАдаптеров(ByRef dsКамераDataSet As КамераDataSet, ByVal blnЗапистьВInput As Boolean)
        If blnЗапистьВInput Then
            гребенкаАMemoTableAdapter = ГребенкаАInputTableAdapter
            гребенкаБMemoTableAdapter = ГребенкаБInputTableAdapter
            изделиеMemoTableAdapter = ИзделиеInputTableAdapter
            контрольЭДСMemoTableAdapter = КонтрольЭДСInputTableAdapter
            полеMemoTableAdapter = ПолеInputTableAdapter
            горелкаMemoTableAdapter = ГорелкаInputTableAdapter
            точкаMemoTableAdapter = ТочкаInputTableAdapter

            'ГребенкаАBindingSource = ГребенкаАInputBindingSource
            'ГребенкаБBindingSource = ГребенкаБInputBindingSource
            'ИзделиеBindingSource = ИзделиеInputBindingSource
            'КонтрольЭДСBindingSource = КонтрольЭДСInputBindingSource
            'ПолеBindingSource = ПолеInputBindingSource
            'ГорелкаBindingSource = ГорелкаInputBindingSource
            'ТочкаBindingSource = ТочкаInputBindingSource
        Else
            гребенкаАMemoTableAdapter = ГребенкаАOutputTableAdapter
            гребенкаБMemoTableAdapter = ГребенкаБOutputTableAdapter
            изделиеMemoTableAdapter = ИзделиеOutputTableAdapter
            контрольЭДСMemoTableAdapter = КонтрольЭДСOutputTableAdapter
            полеMemoTableAdapter = ПолеOutputTableAdapter
            горелкаMemoTableAdapter = ГорелкаOutputTableAdapter
            точкаMemoTableAdapter = ТочкаOutputTableAdapter

            'ГребенкаАBindingSource = ГребенкаАOutputBindingSource
            'ГребенкаБBindingSource = ГребенкаБOutputBindingSource
            'ИзделиеBindingSource = ИзделиеOutputBindingSource
            'КонтрольЭДСBindingSource = КонтрольЭДСOutputBindingSource
            'ПолеBindingSource = ПолеOutputBindingSource
            'ГорелкаBindingSource = ГорелкаOutputBindingSource
            'ТочкаBindingSource = ТочкаOutputBindingSource
        End If

        гребенкаАMemoTableAdapter.FillBy(dsКамераDataSet.ГребенкаА, 0)
        гребенкаБMemoTableAdapter.FillBy(dsКамераDataSet.ГребенкаБ, 0)
        изделиеMemoTableAdapter.FillBy(dsКамераDataSet.Изделие, 0)
        контрольЭДСMemoTableAdapter.FillBy(dsКамераDataSet.КонтрольЭДС, 0)
        полеMemoTableAdapter.FillBy(dsКамераDataSet.Поле, 0)
        точкаMemoTableAdapter.FillBy(dsКамераDataSet.Точка, 0)
        горелкаMemoTableAdapter.FillBy(dsКамераDataSet.Горелка, 0)
    End Sub

    ''' <summary>
    ''' Считать испытания камеры из базы источника или приёмника во временный контейнер ChamberField,
    ''' и вернуть его для записи в отсоединённый набор источника или приёмника.
    ''' </summary>
    ''' <param name="dsКамераDataSet"></param>
    ''' <returns></returns>
    Private Function СчитатьКамеруИзMemoDataSet(ByRef dsКамераDataSet As КамераDataSet) As ChamberField
        Dim I As Integer
        ' считывание геометрии
        Dim rowГребенкаА As КамераDataSet.ГребенкаАRow = dsКамераDataSet.ГребенкаА.FindByКодГребенкиА(keyIDГребенкаА)
        Dim rowГребенкаБ As КамераDataSet.ГребенкаБRow = dsКамераDataSet.ГребенкаБ.FindByКодГребенкиБ(keyIDГребенкаБ)
        Dim rowИзделие As КамераDataSet.ИзделиеRow = dsКамераDataSet.Изделие.FindByКодИзделия(keyКодИзделия)
        Dim rowsПоле() As КамераDataSet.ПолеRow = CType(dsКамераDataSet.Поле.Select("КодИзделия = " & CStr(keyКодИзделия)), КамераDataSet.ПолеRow())
        Dim rowsГорелка() As КамераDataSet.ГорелкаRow
        Dim rowsТочка() As КамераDataSet.ТочкаRow
        Dim кодПоля As Integer
        Const КАВЫЧКА As String = "'" '""""
        Dim chamberFieldOut As ChamberField

        ' считывание и запись шапки
        With rowИзделие
            chamberFieldOut = New ChamberField(.НомерСтенда,
                                               .НомерИзделия,
                                               .Дата,
                                               .ПрогрИспытания,
                                               .НомерКорпусаКамеры,
                                               .НомерЖаровойТрубы,
                                               .НомерКоллектора,
                                               .Барометр,
                                               КОЛИЧЕСТВО_КОНТРОЛЬ_ЭДС)
        End With

        With rowГребенкаА
            chamberFieldOut.ГребенкаА = New ChamberField.Гребенка(.НомерГребенкиА, .C, .L, .D, .Z1, .Z2, .Z3, .Z4)
        End With

        With rowГребенкаБ
            chamberFieldOut.ГребенкаБ = New ChamberField.Гребенка(.НомерГребенкиБ, .C, .L, .D, .Z1, .Z2, .Z3, .Z4)
        End With

        ' считывание ЭДС
        Dim rowsКонтрольЭДС() As КамераDataSet.КонтрольЭДСRow = CType(dsКамераDataSet.КонтрольЭДС.Select("КодИзделия = " & CStr(keyКодИзделия)), КамераDataSet.КонтрольЭДСRow())

        If rowsКонтрольЭДС.Count > 0 Then
            For I = 0 To КОЛИЧЕСТВО_КОНТРОЛЬ_ЭДС - 1
                With chamberFieldOut
                    .КонтрольЭДСКамеры(I).ТермопараА1 = rowsКонтрольЭДС(I).ТермопараА1
                    .КонтрольЭДСКамеры(I).ТермопараБ1 = rowsКонтрольЭДС(I).ТермопараБ1
                    .КонтрольЭДСКамеры(I).ТермопараА2 = rowsКонтрольЭДС(I).ТермопараА2
                    .КонтрольЭДСКамеры(I).ТермопараБ2 = rowsКонтрольЭДС(I).ТермопараБ2
                    .КонтрольЭДСКамеры(I).ТермопараА3 = rowsКонтрольЭДС(I).ТермопараА3
                    .КонтрольЭДСКамеры(I).ТермопараБ3 = rowsКонтрольЭДС(I).ТермопараБ3
                    .КонтрольЭДСКамеры(I).ТермопараА4 = rowsКонтрольЭДС(I).ТермопараА4
                    .КонтрольЭДСКамеры(I).ТермопараБ4 = rowsКонтрольЭДС(I).ТермопараБ4
                    .КонтрольЭДСКамеры(I).ТермопараА5 = rowsКонтрольЭДС(I).ТермопараА5
                    .КонтрольЭДСКамеры(I).ТермопараБ5 = rowsКонтрольЭДС(I).ТермопараБ5
                End With
            Next
        End If

        ' Поле
        If rowsПоле.Count > 0 Then
            For Each rowПоле As КамераDataSet.ПолеRow In rowsПоле
                кодПоля = rowПоле.КодПоля
                Dim tempПоле As New ChamberField.Поле(ЧИСЛО_ГОРЕЛОК, КОЛИЧЕСТВО_КТ, rowПоле.НомерПоля, rowПоле.ВремяЗапуска, rowПоле.ВремяХодаТурели)

                With tempПоле
                    ' создаем запрос для нужной температуры
                    rowsГорелка = CType(dsКамераDataSet.Горелка.Select($"Температура={КАВЫЧКА}min{КАВЫЧКА} AND КодПоля = {кодПоля.ToString}"), КамераDataSet.ГорелкаRow())

                    For I = 0 To ЧИСЛО_ГОРЕЛОК - 1
                        .ГорелкиКамеры(I).ПолеМинимальные.ПоясА1 = rowsГорелка(I).ПоясА1
                        .ГорелкиКамеры(I).ПолеМинимальные.ПоясБ1 = rowsГорелка(I).ПоясБ1
                        .ГорелкиКамеры(I).ПолеМинимальные.ПоясА2 = rowsГорелка(I).ПоясА2
                        .ГорелкиКамеры(I).ПолеМинимальные.ПоясБ2 = rowsГорелка(I).ПоясБ2
                        .ГорелкиКамеры(I).ПолеМинимальные.ПоясА3 = rowsГорелка(I).ПоясА3
                        .ГорелкиКамеры(I).ПолеМинимальные.ПоясБ3 = rowsГорелка(I).ПоясБ3
                        .ГорелкиКамеры(I).ПолеМинимальные.ПоясА4 = rowsГорелка(I).ПоясА4
                        .ГорелкиКамеры(I).ПолеМинимальные.ПоясБ4 = rowsГорелка(I).ПоясБ4
                        .ГорелкиКамеры(I).ПолеМинимальные.ПоясА5 = rowsГорелка(I).ПоясА5
                        .ГорелкиКамеры(I).ПолеМинимальные.ПоясБ5 = rowsГорелка(I).ПоясБ5
                    Next

                    rowsГорелка = CType(dsКамераDataSet.Горелка.Select($"Температура={КАВЫЧКА}max{КАВЫЧКА} AND КодПоля = {кодПоля.ToString}"), КамераDataSet.ГорелкаRow())

                    For I = 0 To ЧИСЛО_ГОРЕЛОК - 1
                        .ГорелкиКамеры(I).ПолеМаксимальные.ПоясА1 = rowsГорелка(I).ПоясА1
                        .ГорелкиКамеры(I).ПолеМаксимальные.ПоясБ1 = rowsГорелка(I).ПоясБ1
                        .ГорелкиКамеры(I).ПолеМаксимальные.ПоясА2 = rowsГорелка(I).ПоясА2
                        .ГорелкиКамеры(I).ПолеМаксимальные.ПоясБ2 = rowsГорелка(I).ПоясБ2
                        .ГорелкиКамеры(I).ПолеМаксимальные.ПоясА3 = rowsГорелка(I).ПоясА3
                        .ГорелкиКамеры(I).ПолеМаксимальные.ПоясБ3 = rowsГорелка(I).ПоясБ3
                        .ГорелкиКамеры(I).ПолеМаксимальные.ПоясА4 = rowsГорелка(I).ПоясА4
                        .ГорелкиКамеры(I).ПолеМаксимальные.ПоясБ4 = rowsГорелка(I).ПоясБ4
                        .ГорелкиКамеры(I).ПолеМаксимальные.ПоясА5 = rowsГорелка(I).ПоясА5
                        .ГорелкиКамеры(I).ПолеМаксимальные.ПоясБ5 = rowsГорелка(I).ПоясБ5
                    Next

                    rowsГорелка = CType(dsКамераDataSet.Горелка.Select($"Температура={КАВЫЧКА}midl{КАВЫЧКА} AND КодПоля = {кодПоля.ToString}"), КамераDataSet.ГорелкаRow())

                    For I = 0 To ЧИСЛО_ГОРЕЛОК - 1
                        .ГорелкиКамеры(I).ПолеСредние.ПоясА1 = rowsГорелка(I).ПоясА1
                        .ГорелкиКамеры(I).ПолеСредние.ПоясБ1 = rowsГорелка(I).ПоясБ1
                        .ГорелкиКамеры(I).ПолеСредние.ПоясА2 = rowsГорелка(I).ПоясА2
                        .ГорелкиКамеры(I).ПолеСредние.ПоясБ2 = rowsГорелка(I).ПоясБ2
                        .ГорелкиКамеры(I).ПолеСредние.ПоясА3 = rowsГорелка(I).ПоясА3
                        .ГорелкиКамеры(I).ПолеСредние.ПоясБ3 = rowsГорелка(I).ПоясБ3
                        .ГорелкиКамеры(I).ПолеСредние.ПоясА4 = rowsГорелка(I).ПоясА4
                        .ГорелкиКамеры(I).ПолеСредние.ПоясБ4 = rowsГорелка(I).ПоясБ4
                        .ГорелкиКамеры(I).ПолеСредние.ПоясА5 = rowsГорелка(I).ПоясА5
                        .ГорелкиКамеры(I).ПолеСредние.ПоясБ5 = rowsГорелка(I).ПоясБ5
                    Next

                    rowsТочка = CType(dsКамераDataSet.Точка.Select("КодПоля = " & CStr(кодПоля)), КамераDataSet.ТочкаRow())

                    For I = 0 To КОЛИЧЕСТВО_КТ - 1
                        .КонтрольныеТочкиКамеры(I).P1 = rowsТочка(I).P1 'Статическое давление перед соплом мерного участка
                        .КонтрольныеТочкиКамеры(I).dP1 = rowsТочка(I).dP1 'Перепад давлений на сопле мерного участка
                        .КонтрольныеТочкиКамеры(I).T3 = rowsТочка(I).T3 'Температура воздуха в мерном участке
                        .КонтрольныеТочкиКамеры(I).Pотб = rowsТочка(I).Pотб 'Статическое давление участка отбора воздуха
                        .КонтрольныеТочкиКамеры(I).dPотб = rowsТочка(I).dPотб 'Перепад давлений участка отбора воздуха
                        .КонтрольныеТочкиКамеры(I).Tотб = rowsТочка(I).Tотб 'Т воздуха в мерном участке отбора воздуха
                        .КонтрольныеТочкиКамеры(I).ДавлениеМагистралеОтбораВн = rowsТочка(I).ДавлениеМагистралеОтбораВн 'Статическое давление участка отбора воздуха вн
                        .КонтрольныеТочкиКамеры(I).ПерепадДавленияВоздухаОтбораВн = rowsТочка(I).ПерепадДавленияВоздухаОтбораВн 'Перепад давлений участка отбора воздуха вн
                        .КонтрольныеТочкиКамеры(I).ТотбораВн = rowsТочка(I).ТотбораВн 'Т воздуха в мерном участке отбора воздуха вн
                        .КонтрольныеТочкиКамеры(I).P310 = rowsТочка(I).P310 'Полное давление воздуха во входном мерном участке
                        .КонтрольныеТочкиКамеры(I).P311 = rowsТочка(I).P311 'Статическое давление на входе в камеру сгорания
                        .КонтрольныеТочкиКамеры(I).TтоплКС = rowsТочка(I).TтоплКС 'Температура топлива подаваемого в камеру сгорания
                        .КонтрольныеТочкиКамеры(I).TтоплКП = rowsТочка(I).TтоплКП 'Температура топлива подаваемого в камеру подогрева
                        .КонтрольныеТочкиКамеры(I).Tбокса = rowsТочка(I).Tбокса 'температура в боксе
                        .КонтрольныеТочкиКамеры(I).GтКС = rowsТочка(I).GтКС 'Расход топлива камеры сгорания
                        .КонтрольныеТочкиКамеры(I).GтКП = rowsТочка(I).GтКП 'Расход топлива камеры подогрева
                        .КонтрольныеТочкиКамеры(I).T309_1 = rowsТочка(I).T309_1 'Температура газа на входе в камеру сгорания
                        .КонтрольныеТочкиКамеры(I).T309_2 = rowsТочка(I).T309_2 'Температура газа на входе в камеру сгорания
                        .КонтрольныеТочкиКамеры(I).T309_3 = rowsТочка(I).T309_3 'Температура газа на входе в камеру сгорания
                        .КонтрольныеТочкиКамеры(I).T309_4 = rowsТочка(I).T309_4 'Температура газа на входе в камеру сгорания
                        .КонтрольныеТочкиКамеры(I).T309_5 = rowsТочка(I).T309_5 'Температура газа на входе в камеру сгорания
                        .КонтрольныеТочкиКамеры(I).T309_6 = rowsТочка(I).T309_6 'Температура газа на входе в камеру сгорания
                        .КонтрольныеТочкиКамеры(I).T309_7 = rowsТочка(I).T309_7 'Температура газа на входе в камеру сгорания
                        .КонтрольныеТочкиКамеры(I).T309_8 = rowsТочка(I).T309_8 'Температура газа на входе в камеру сгорания
                        .КонтрольныеТочкиКамеры(I).T309_9 = rowsТочка(I).T309_9 'Температура газа на входе в камеру сгорания
                        .КонтрольныеТочкиКамеры(I).T309_10 = rowsТочка(I).T309_10 'Температура газа на входе в камеру сгорания
                        .КонтрольныеТочкиКамеры(I).T309_11 = rowsТочка(I).T309_11 'Температура газа на входе в камеру сгорания
                        .КонтрольныеТочкиКамеры(I).T309_12 = rowsТочка(I).T309_12 'Температура газа на входе в камеру сгорания
                        .КонтрольныеТочкиКамеры(I).ТвоздухаНаВходеКП = rowsТочка(I).ТвоздухаНаВходеКП 'Температура воздуха на входе в камеру подогрева
                    Next
                End With

                chamberFieldOut.ПоляКамеры.Add(tempПоле)
            Next
        End If

        Return chamberFieldOut
    End Function

    ''' <summary>
    ''' Вставка записи испытания камеры из базы источника или приёмника посредством временного контейнера ChamberField
    ''' в отсоединённый набор источника или приёмника.
    ''' </summary>
    ''' <param name="inChamberField"></param>
    Private Sub ЗаписатьКамеруВMemoDataSet(inChamberField As ChamberField) 'ByRef dsКамераDataSet As КамераDataSet)
        Dim I As Integer
        Dim кодГребенкиА As Integer
        Dim кодГребенкиБ As Integer
        Dim кодПоля As Integer

        ' закоментировал работающий способ
        ' если поменять на TableAdapter.Fill на TableAdapter.FillВуКеу(ПрелыдущийПолученныйКлючРодительскойТаблицы), то таблица будет заполняться пустой и скорость возрастёт
        ' в итоге сделал проще InsertQuery вставляет запись, а ScalarQuery получает ключ с максимальным значением, что равносильно получению ключа из последней вставленной записи

        ' гребенка А
        'Dim newrowГребенкаАRow As КамераDataSet.ГребенкаАRow = dsКамераDataSet.ГребенкаА.NewГребенкаАRow 'новая строка
        'With newrowГребенкаАRow
        '    .НомерГребенкиА = sngНомерГребенки(0)
        '    .C = sngC(0)
        '...................................
        '    .Z4 = sngZ4(0)
        'End With

        'dsКамераDataSet.ГребенкаА.AddГребенкаАRow(newrowГребенкаАRow)
        ' ГребенкаАTableAdapterUpdate(dsКамераDataSet, taГребенкаАTableAdapter, ГребенкаАBindingSource)
        ''ГребенкаАBindingSource.DataMember = "ГребенкаА"
        ''ГребенкаАBindingSource.DataSource = dsКамераDataSet
        'taГребенкаАTableAdapter.Fill(dsКамераDataSet.ГребенкаА)
        ''ГребенкаАBindingSource.MoveLast()
        'lngКодГребенкиА = dsКамераDataSet.ГребенкаА.Last.КодГребенкиА
        'Stop
        With inChamberField
            гребенкаАMemoTableAdapter.InsertQuery(.ГребенкаА.НомерГребенки,
                                                  CType(.ГребенкаА.C, Decimal?),
                                                  CType(.ГребенкаА.L, Decimal?),
                                                  CType(.ГребенкаА.D, Decimal?),
                                                  CType(.ГребенкаА.Z1, Decimal?),
                                                  CType(.ГребенкаА.Z2, Decimal?),
                                                  CType(.ГребенкаА.Z3, Decimal?),
                                                  CType(.ГребенкаА.Z4, Decimal?))
            кодГребенкиА = CInt(гребенкаАMemoTableAdapter.ScalarQuery)

            ' гребенка Б
            'Dim newrowГребенкаБRow As КамераDataSet.ГребенкаБRow = dsКамераDataSet.ГребенкаБ.NewГребенкаБRow 'новая строка
            'With newrowГребенкаБRow
            '    .НомерГребенкиБ = sngНомерГребенки(1)
            '    .C = sngC(1)
            '...................................
            '    .Z4 = sngZ4(1)
            'End With

            'dsКамераDataSet.ГребенкаБ.AddГребенкаБRow(newrowГребенкаБRow)
            ' ГребенкаБTableAdapterUpdate(dsКамераDataSet, taГребенкаБTableAdapter, ГребенкаБBindingSource)
            'taГребенкаБTableAdapter.Fill(dsКамераDataSet.ГребенкаБ)
            ''ГребенкаБBindingSource.MoveLast()
            'lngКодГребенкиБ = dsКамераDataSet.ГребенкаБ.Last.КодГребенкиБ

            гребенкаБMemoTableAdapter.InsertQuery(.ГребенкаБ.НомерГребенки,
                                                  CType(.ГребенкаБ.C, Decimal?),
                                                  CType(.ГребенкаБ.L, Decimal?),
                                                  CType(.ГребенкаБ.D, Decimal?),
                                                  CType(.ГребенкаБ.Z1, Decimal?),
                                                  CType(.ГребенкаБ.Z2, Decimal?),
                                                  CType(.ГребенкаБ.Z3, Decimal?),
                                                  CType(.ГребенкаБ.Z4, Decimal?))
            кодГребенкиБ = CInt(гребенкаБMemoTableAdapter.ScalarQuery)

            'изделие
            'Dim newrowИзделиеRow As КамераDataSet.ИзделиеRow = dsКамераDataSet.Изделие.NewИзделиеRow 'новая строка
            'With newrowИзделиеRow
            '    .НомерСтенда = lngНомерСтенда
            '    .НомерИзделия = lngНомерИзделия
            '    .Дата = Дата
            '    .КодГребенкиА = lngКодГребенкиА
            '    .КодГребенкиБ = lngКодГребенкиБ
            '    .ПрогрИспытания = lngПрограммаИспытания
            '    .НомерКорпусаКамеры = lngНомерКорпуса
            '    .НомерЖаровойТрубы = lngНомерЖаровойТрубы
            '    .НомерКоллектора = lngНомерКоллектора
            '    .Барометр = sgnbar
            'End With

            'dsКамераDataSet.Изделие.AddИзделиеRow(newrowИзделиеRow)
            ' ИзделиеTableAdapterUpdate(dsКамераDataSet, taИзделиеTableAdapter, ИзделиеBindingSource)
            'taИзделиеTableAdapter.Fill(dsКамераDataSet.Изделие)
            ''ИзделиеBindingSource.MoveLast()
            'lngКодИзделия = dsКамераDataSet.Изделие.Last.КодИзделия

            изделиеMemoTableAdapter.InsertQuery(.НомерСтенда,
                                            .НомерИзделия,
                                            .Дата,
                                            кодГребенкиА,
                                            кодГребенкиБ,
                                            .ПрограммаИспытания,
                                            .НомерКорпусаКамеры,
                                            .НомерЖаровойТрубы,
                                            .НомерКоллектора,
                                            CType(.Барометр, Decimal?))
            keyКодИзделия = CInt(изделиеMemoTableAdapter.ScalarQuery)

            'КонтрольЭДС
            'Dim newrowКонтрольЭДСRow As КамераDataSet.КонтрольЭДСRow
            'For i = 1 To 5
            '    newrowКонтрольЭДСRow = dsКамераDataSet.КонтрольЭДС.NewКонтрольЭДСRow 'новая строка
            '    With newrowКонтрольЭДСRow
            '        .КодИзделия = lngКодИзделия
            '        .НомерТочки = i
            '        .ТермопараА1 = arrCalc(i, 1)
            '...................................
            '        .ТермопараБ5 = arrCalc(i, 10)
            '    End With
            '    dsКамераDataSet.КонтрольЭДС.AddКонтрольЭДСRow(newrowКонтрольЭДСRow)
            'Next i
            ' КонтрольЭДСTableAdapterUpdate(dsКамераDataSet, taКонтрольЭДСTableAdapter, КонтрольЭДСBindingSource)
            ''taКонтрольЭДСTableAdapter.Fill(dsКамераDataSet.КонтрольЭДС)
            For I = 0 To КОЛИЧЕСТВО_КОНТРОЛЬ_ЭДС - 1
                контрольЭДСMemoTableAdapter.InsertQuery(keyКодИзделия,
                                                  I + 1,
                                                  CType(.КонтрольЭДСКамеры(I).ТермопараА1, Decimal?),
                                                  CType(.КонтрольЭДСКамеры(I).ТермопараБ1, Decimal?),
                                                  CType(.КонтрольЭДСКамеры(I).ТермопараА2, Decimal?),
                                                  CType(.КонтрольЭДСКамеры(I).ТермопараБ2, Decimal?),
                                                  CType(.КонтрольЭДСКамеры(I).ТермопараА3, Decimal?),
                                                  CType(.КонтрольЭДСКамеры(I).ТермопараБ3, Decimal?),
                                                  CType(.КонтрольЭДСКамеры(I).ТермопараА4, Decimal?),
                                                  CType(.КонтрольЭДСКамеры(I).ТермопараБ4, Decimal?),
                                                  CType(.КонтрольЭДСКамеры(I).ТермопараА5, Decimal?),
                                                  CType(.КонтрольЭДСКамеры(I).ТермопараБ5, Decimal?))
            Next
        End With

        ' запись в базу поля
        For Each itemПоле In inChamberField.ПоляКамеры
            'Dim newrowПолеRow As КамераDataSet.ПолеRow
            'For номерПоля = 1 To количествоПолей

            'newrowПолеRow = dsКамераDataSet.Поле.NewПолеRow 'новая строка
            'With newrowПолеRow
            '    .КодИзделия = lngКодИзделия
            '    .НомерПоля = arrНомерПоля(intНомерПоля)
            '    .ВремяЗапуска = arrВремя(intНомерПоля)
            '    .ВремяХодаТурели = arrВремяХода(intНомерПоля)
            'End With
            'dsКамераDataSet.Поле.AddПолеRow(newrowПолеRow)

            ' ПолеTableAdapterUpdate(dsКамераDataSet, taПолеTableAdapter, ПолеBindingSource)
            'taПолеTableAdapter.Fill(dsКамераDataSet.Поле)
            ''ПолеBindingSource.MoveLast()
            'lngКодПоля = dsКамераDataSet.Поле.Last.КодПоля
            With itemПоле
                полеMemoTableAdapter.InsertQuery(keyКодИзделия, itemПоле.НомерПоля, itemПоле.ВремяЗапуска, itemПоле.ВремяХодаТурели)
                кодПоля = CInt(полеMemoTableAdapter.ScalarQuery)

                ' таблице "Горелка"
                'Dim newrowГорелкаRow As КамераDataSet.ГорелкаRow
                For I = 0 To ЧИСЛО_ГОРЕЛОК - 1
                    'newrowГорелкаRow = dsКамераDataSet.Горелка.NewГорелкаRow
                    'With newrowГорелкаRow
                    '    .КодПоля = lngКодПоля
                    '    .НомерГорелки = i
                    '    .Температура = "min"
                    '    .ПоясА1 = arrПолеМинимальные(i, 1, intНомерПоля)
                    '...................................
                    '    .ПоясБ5 = arrПолеМинимальные(i, 10, intНомерПоля)
                    'End With
                    'dsКамераDataSet.Горелка.AddГорелкаRow(newrowГорелкаRow)

                    горелкаMemoTableAdapter.InsertQuery(кодПоля,
                              I + 1,
                              "min",
                              CType(.ГорелкиКамеры(I).ПолеМинимальные.ПоясА1, Decimal?),
                              CType(.ГорелкиКамеры(I).ПолеМинимальные.ПоясБ1, Decimal?),
                              CType(.ГорелкиКамеры(I).ПолеМинимальные.ПоясА2, Decimal?),
                              CType(.ГорелкиКамеры(I).ПолеМинимальные.ПоясБ2, Decimal?),
                              CType(.ГорелкиКамеры(I).ПолеМинимальные.ПоясА3, Decimal?),
                              CType(.ГорелкиКамеры(I).ПолеМинимальные.ПоясБ3, Decimal?),
                              CType(.ГорелкиКамеры(I).ПолеМинимальные.ПоясА4, Decimal?),
                              CType(.ГорелкиКамеры(I).ПолеМинимальные.ПоясБ4, Decimal?),
                              CType(.ГорелкиКамеры(I).ПолеМинимальные.ПоясА5, Decimal?),
                              CType(.ГорелкиКамеры(I).ПолеМинимальные.ПоясБ5, Decimal?))

                    горелкаMemoTableAdapter.InsertQuery(кодПоля,
                              I + 1,
                              "max",
                              CType(.ГорелкиКамеры(I).ПолеМаксимальные.ПоясА1, Decimal?),
                              CType(.ГорелкиКамеры(I).ПолеМаксимальные.ПоясБ1, Decimal?),
                              CType(.ГорелкиКамеры(I).ПолеМаксимальные.ПоясА2, Decimal?),
                              CType(.ГорелкиКамеры(I).ПолеМаксимальные.ПоясБ2, Decimal?),
                              CType(.ГорелкиКамеры(I).ПолеМаксимальные.ПоясА3, Decimal?),
                              CType(.ГорелкиКамеры(I).ПолеМаксимальные.ПоясБ3, Decimal?),
                              CType(.ГорелкиКамеры(I).ПолеМаксимальные.ПоясА4, Decimal?),
                              CType(.ГорелкиКамеры(I).ПолеМаксимальные.ПоясБ4, Decimal?),
                              CType(.ГорелкиКамеры(I).ПолеМаксимальные.ПоясА5, Decimal?),
                              CType(.ГорелкиКамеры(I).ПолеМаксимальные.ПоясБ5, Decimal?))

                    горелкаMemoTableAdapter.InsertQuery(кодПоля,
                              I + 1,
                              "midl",
                              CType(.ГорелкиКамеры(I).ПолеСредние.ПоясА1, Decimal?),
                              CType(.ГорелкиКамеры(I).ПолеСредние.ПоясБ1, Decimal?),
                              CType(.ГорелкиКамеры(I).ПолеСредние.ПоясА2, Decimal?),
                              CType(.ГорелкиКамеры(I).ПолеСредние.ПоясБ2, Decimal?),
                              CType(.ГорелкиКамеры(I).ПолеСредние.ПоясА3, Decimal?),
                              CType(.ГорелкиКамеры(I).ПолеСредние.ПоясБ3, Decimal?),
                              CType(.ГорелкиКамеры(I).ПолеСредние.ПоясА4, Decimal?),
                              CType(.ГорелкиКамеры(I).ПолеСредние.ПоясБ4, Decimal?),
                              CType(.ГорелкиКамеры(I).ПолеСредние.ПоясА5, Decimal?),
                              CType(.ГорелкиКамеры(I).ПолеСредние.ПоясБ5, Decimal?))
                Next

                'For I = 1 To КОЛИЧЕСТВО_ГОРЕЛОК
                '    'newrowГорелкаRow = dsКамераDataSet.Горелка.NewГорелкаRow
                '    'With newrowГорелкаRow
                '    '    .КодПоля = lngКодПоля
                '    '    .НомерГорелки = i
                '    '    .Температура = "max"
                '    '    .ПоясА1 = arrПолеМаксимальные(i, 1, intНомерПоля)
                '...................................
                '    '    .ПоясБ5 = arrПолеМаксимальные(i, 10, intНомерПоля)
                '    'End With
                '    'dsКамераDataSet.Горелка.AddГорелкаRow(newrowГорелкаRow)


                'For I = 1 To КОЛИЧЕСТВО_ГОРЕЛОК
                '    'newrowГорелкаRow = dsКамераDataSet.Горелка.NewГорелкаRow
                '    'With newrowГорелкаRow
                '    '    .КодПоля = lngКодПоля
                '    '    .НомерГорелки = i
                '    '    .Температура = "midl"
                '    '    .ПоясА1 = arrПолеСредние(i, 1, intНомерПоля)
                '...................................
                '    '    .ПоясБ5 = arrПолеСредние(i, 10, intНомерПоля)
                '    'End With
                '    'dsКамераDataSet.Горелка.AddГорелкаRow(newrowГорелкаRow)

                ' ГорелкаTableAdapterUpdate(dsКамераDataSet, taГорелкаTableAdapter, ГорелкаBindingSource)
                ''taГорелкаTableAdapter.Fill(dsКамераDataSet.Горелка)

                ' таблице "Точка"
                'Dim newrowТочкаRow As КамераDataSet.ТочкаRow
                For I = 0 To КОЛИЧЕСТВО_КТ - 1
                    'newrowТочкаRow = dsКамераDataSet.Точка.NewТочкаRow
                    'With newrowТочкаRow
                    '    .КодПоля = lngКодПоля
                    '    .НомерТочки = i
                    '    .P1 = arrТочка(i, 1, intНомерПоля) '1 давление воздуха на входе
                    '...................................
                    '    .T309_12 = arrТочка(i, 25, intНомерПоля)
                    'End With
                    'dsКамераDataSet.Точка.AddТочкаRow(newrowТочкаRow)

                    точкаMemoTableAdapter.InsertQuery(кодПоля,
                                                    I + 1,
                                                    CDec(.КонтрольныеТочкиКамеры(I).P1),
                                                    CDec(.КонтрольныеТочкиКамеры(I).dP1),
                                                    CDec(.КонтрольныеТочкиКамеры(I).T3),
                                                    CDec(.КонтрольныеТочкиКамеры(I).dPотб),
                                                    CDec(.КонтрольныеТочкиКамеры(I).Tотб),
                                                    CDec(.КонтрольныеТочкиКамеры(I).ДавлениеМагистралеОтбораВн),
                                                    CDec(.КонтрольныеТочкиКамеры(I).ПерепадДавленияВоздухаОтбораВн),
                                                    CDec(.КонтрольныеТочкиКамеры(I).ТотбораВн),
                                                    CDec(.КонтрольныеТочкиКамеры(I).P310),
                                                    CDec(.КонтрольныеТочкиКамеры(I).Pотб),
                                                    CDec(.КонтрольныеТочкиКамеры(I).P311),
                                                    CDec(.КонтрольныеТочкиКамеры(I).TтоплКС),
                                                    CDec(.КонтрольныеТочкиКамеры(I).TтоплКП),
                                                    CDec(.КонтрольныеТочкиКамеры(I).Tбокса),
                                                    CDec(.КонтрольныеТочкиКамеры(I).GтКС),
                                                    CDec(.КонтрольныеТочкиКамеры(I).GтКП),
                                                    CDec(.КонтрольныеТочкиКамеры(I).T309_1),
                                                    CDec(.КонтрольныеТочкиКамеры(I).T309_2),
                                                    CDec(.КонтрольныеТочкиКамеры(I).T309_3),
                                                    CDec(.КонтрольныеТочкиКамеры(I).T309_4),
                                                    CDec(.КонтрольныеТочкиКамеры(I).T309_5),
                                                    CDec(.КонтрольныеТочкиКамеры(I).T309_6),
                                                    CDec(.КонтрольныеТочкиКамеры(I).T309_7),
                                                    CDec(.КонтрольныеТочкиКамеры(I).T309_8),
                                                    CDec(.КонтрольныеТочкиКамеры(I).T309_9),
                                                    CDec(.КонтрольныеТочкиКамеры(I).T309_10),
                                                    CDec(.КонтрольныеТочкиКамеры(I).T309_11),
                                                    CDec(.КонтрольныеТочкиКамеры(I).T309_12),
                                                    CDec(.КонтрольныеТочкиКамеры(I).ТвоздухаНаВходеКП))
                Next

                ' ТочкаTableAdapterUpdate(dsКамераDataSet, taТочкаTableAdapter, ТочкаBindingSource)
                ''taТочкаTableAdapter.Fill(dsКамераDataSet.Точка)
            End With
        Next

        ' выполняются позже так как здесь нет необходимых срочно ключевых полей
        ' убрал вообще так как после идет полное заполнение всех Adapter
        'taКонтрольЭДСTableAdapter.Fill(dsКамераDataSet.КонтрольЭДС)
        'taГорелкаTableAdapter.Fill(dsКамераDataSet.Горелка)
        'taТочкаTableAdapter.Fill(dsКамераDataSet.Точка)
    End Sub

    ''' <summary>
    ''' Удаление записи из источника или приёмника
    ''' </summary>
    ''' <param name="dsКамераDataSet"></param>
    Private Sub УдалитьКамеруИзMemoDataSet(ByRef dsКамераDataSet As КамераDataSet)
        dsКамераDataSet.ГребенкаА.FindByКодГребенкиА(keyIDГребенкаА).Delete()
        dsКамераDataSet.ГребенкаБ.FindByКодГребенкиБ(keyIDГребенкаБ).Delete()
    End Sub

#Region "AdapterUpdate"

    ''' <summary>
    ''' Применить обновления в наборе записей
    '''  ВНИМАНИЕ!!! Здесь закоментировал, т.к. .AcceptChanges будет в ГребенкаБTableAdapterUpdate
    ''' </summary>
    ''' <param name="dsКамераDataSet"></param>
    ''' <param name="taГребенкаАTableAdapter"></param>
    ''' <param name="bsBindingSource"></param>
    Private Sub ГребенкаАTableAdapterUpdate(ByRef dsКамераDataSet As КамераDataSet,
                                            ByRef taГребенкаАTableAdapter As КамераDataSetTableAdapters.ГребенкаАTableAdapter,
                                            ByRef bsBindingSource As BindingSource)
        Validate()
        'Me.ГребенкаАInputBindingSource.EndEdit()
        bsBindingSource.EndEdit()

        Dim deletedChildRecords = CType(dsКамераDataSet.ГребенкаА.GetChanges(DataRowState.Deleted), КамераDataSet.ГребенкаАDataTable)
        Dim newChildRecords = CType(dsКамераDataSet.ГребенкаА.GetChanges(DataRowState.Added), КамераDataSet.ГребенкаАDataTable)
        Dim modifiedChildRecords = CType(dsКамераDataSet.ГребенкаА.GetChanges(DataRowState.Modified), КамераDataSet.ГребенкаАDataTable)

        Try
            If Not deletedChildRecords Is Nothing Then
                taГребенкаАTableAdapter.Update(deletedChildRecords)
            End If

            If Not modifiedChildRecords Is Nothing Then
                taГребенкаАTableAdapter.Update(modifiedChildRecords)
            End If

            If Not newChildRecords Is Nothing Then
                taГребенкаАTableAdapter.Update(newChildRecords)
            End If

            ' ВНИМАНИЕ!!! Здесь закоментировал, т.к. .AcceptChanges будет в ГребенкаБTableAdapterUpdate
            'dsКамераDataSet.AcceptChanges()

        Catch ex As Exception
            MsgBox("Ошибка обновления в процедуре ГребенкаАTableAdapterUpdate.")
        Finally
            If Not deletedChildRecords Is Nothing Then
                deletedChildRecords.Dispose()
            End If

            If Not modifiedChildRecords Is Nothing Then
                modifiedChildRecords.Dispose()
            End If

            If Not newChildRecords Is Nothing Then
                newChildRecords.Dispose()
            End If
        End Try
        'System.Threading.Thread.Sleep(100)
        Application.DoEvents()
    End Sub

    ''' <summary>
    ''' Применить обновления в наборе записей
    ''' </summary>
    ''' <param name="dsКамераDataSet"></param>
    ''' <param name="taГребенкаБTableAdapter"></param>
    ''' <param name="bsBindingSource"></param>
    Private Sub ГребенкаБTableAdapterUpdate(ByRef dsКамераDataSet As КамераDataSet, ByRef taГребенкаБTableAdapter As КамераDataSetTableAdapters.ГребенкаБTableAdapter, ByRef bsBindingSource As BindingSource)
        Validate()
        'Me.ГребенкаБInputBindingSource.EndEdit()
        bsBindingSource.EndEdit()

        Dim deletedChildRecords = CType(dsКамераDataSet.ГребенкаБ.GetChanges(DataRowState.Deleted), КамераDataSet.ГребенкаБDataTable)
        Dim newChildRecords = CType(dsКамераDataSet.ГребенкаБ.GetChanges(DataRowState.Added), КамераDataSet.ГребенкаБDataTable)
        Dim modifiedChildRecords = CType(dsКамераDataSet.ГребенкаБ.GetChanges(DataRowState.Modified), КамераDataSet.ГребенкаБDataTable)

        Try
            If Not deletedChildRecords Is Nothing Then
                taГребенкаБTableAdapter.Update(deletedChildRecords)
            End If

            If Not modifiedChildRecords Is Nothing Then
                taГребенкаБTableAdapter.Update(modifiedChildRecords)
            End If

            If Not newChildRecords Is Nothing Then
                taГребенкаБTableAdapter.Update(newChildRecords)
            End If

            dsКамераDataSet.AcceptChanges()

        Catch ex As Exception
            MsgBox("Ошибка обновления в процедуре ГребенкаБTableAdapterUpdate.")
        Finally
            If Not deletedChildRecords Is Nothing Then
                deletedChildRecords.Dispose()
            End If

            If Not modifiedChildRecords Is Nothing Then
                modifiedChildRecords.Dispose()
            End If

            If Not newChildRecords Is Nothing Then
                newChildRecords.Dispose()
            End If
        End Try
        'System.Threading.Thread.Sleep(100)
        Application.DoEvents()
    End Sub

    'Private Sub ИзделиеTableAdapterUpdate(ByRef dsКамераDataSet As КамераDataSet, ByRef taИзделиеTableAdapter As КамераDataSetTableAdapters.ИзделиеTableAdapter, ByRef bsBindingSource As System.Windows.Forms.BindingSource)
    '    Me.Validate()
    '    bsBindingSource.EndEdit()

    '    Dim deletedChildRecords As DataTable = dsКамераDataSet.Изделие.GetChanges(DataRowState.Deleted)
    '    Dim newChildRecords As DataTable = dsКамераDataSet.Изделие.GetChanges(DataRowState.Added)
    '    Dim modifiedChildRecords As DataTable = dsКамераDataSet.Изделие.GetChanges(DataRowState.Modified)

    '    Try
    '        If Not deletedChildRecords Is Nothing Then
    '            taИзделиеTableAdapter.Update(deletedChildRecords)
    '        End If

    '        If Not modifiedChildRecords Is Nothing Then
    '            taИзделиеTableAdapter.Update(modifiedChildRecords)
    '        End If

    '        If Not newChildRecords Is Nothing Then
    '            taИзделиеTableAdapter.Update(newChildRecords)
    '        End If

    '        dsКамераDataSet.AcceptChanges()

    '    Catch ex As Exception
    '        MsgBox("Ошибка обновления в процедуре ИзделиеTableAdapterUpdate.")
    '    Finally
    '        If Not deletedChildRecords Is Nothing Then
    '            deletedChildRecords.Dispose()
    '        End If

    '        If Not modifiedChildRecords Is Nothing Then
    '            modifiedChildRecords.Dispose()
    '        End If

    '        If Not newChildRecords Is Nothing Then
    '            newChildRecords.Dispose()
    '        End If
    '    End Try
    '    'System.Threading.Thread.Sleep(100)
    '    Application.DoEvents()
    'End Sub

    'Private Sub КонтрольЭДСTableAdapterUpdate(ByRef dsКамераDataSet As КамераDataSet, ByRef taКонтрольЭДСTableAdapter As КамераDataSetTableAdapters.КонтрольЭДСTableAdapter, ByRef bsBindingSource As System.Windows.Forms.BindingSource)
    '    Me.Validate()
    '    bsBindingSource.EndEdit()

    '    Dim deletedChildRecords As DataTable = dsКамераDataSet.КонтрольЭДС.GetChanges(DataRowState.Deleted)
    '    Dim newChildRecords As DataTable = dsКамераDataSet.КонтрольЭДС.GetChanges(DataRowState.Added)
    '    Dim modifiedChildRecords As DataTable = dsКамераDataSet.КонтрольЭДС.GetChanges(DataRowState.Modified)

    '    Try
    '        If Not deletedChildRecords Is Nothing Then
    '            taКонтрольЭДСTableAdapter.Update(deletedChildRecords)
    '        End If

    '        If Not modifiedChildRecords Is Nothing Then
    '            taКонтрольЭДСTableAdapter.Update(modifiedChildRecords)
    '        End If

    '        If Not newChildRecords Is Nothing Then
    '            taКонтрольЭДСTableAdapter.Update(newChildRecords)
    '        End If

    '        dsКамераDataSet.AcceptChanges()

    '    Catch ex As Exception
    '        MsgBox("Ошибка обновления в процедуре КонтрольЭДСTableAdapterUpdate.")
    '    Finally
    '        If Not deletedChildRecords Is Nothing Then
    '            deletedChildRecords.Dispose()
    '        End If

    '        If Not modifiedChildRecords Is Nothing Then
    '            modifiedChildRecords.Dispose()
    '        End If

    '        If Not newChildRecords Is Nothing Then
    '            newChildRecords.Dispose()
    '        End If
    '    End Try
    '    'System.Threading.Thread.Sleep(100)
    '    Application.DoEvents()
    'End Sub

    'Private Sub ПолеTableAdapterUpdate(ByRef dsКамераDataSet As КамераDataSet, ByRef taПолеTableAdapter As КамераDataSetTableAdapters.ПолеTableAdapter, ByRef bsBindingSource As System.Windows.Forms.BindingSource)
    '    Me.Validate()
    '    bsBindingSource.EndEdit()

    '    Dim deletedChildRecords As DataTable = dsКамераDataSet.Поле.GetChanges(DataRowState.Deleted)
    '    Dim newChildRecords As DataTable = dsКамераDataSet.Поле.GetChanges(DataRowState.Added)
    '    Dim modifiedChildRecords As DataTable = dsКамераDataSet.Поле.GetChanges(DataRowState.Modified)

    '    Try
    '        If Not deletedChildRecords Is Nothing Then
    '            taПолеTableAdapter.Update(deletedChildRecords)
    '        End If

    '        If Not modifiedChildRecords Is Nothing Then
    '            taПолеTableAdapter.Update(modifiedChildRecords)
    '        End If

    '        If Not newChildRecords Is Nothing Then
    '            taПолеTableAdapter.Update(newChildRecords)
    '        End If

    '        dsКамераDataSet.AcceptChanges()

    '    Catch ex As Exception
    '        MsgBox("Ошибка обновления в процедуре ПолеTableAdapterUpdate.")
    '    Finally
    '        If Not deletedChildRecords Is Nothing Then
    '            deletedChildRecords.Dispose()
    '        End If

    '        If Not modifiedChildRecords Is Nothing Then
    '            modifiedChildRecords.Dispose()
    '        End If

    '        If Not newChildRecords Is Nothing Then
    '            newChildRecords.Dispose()
    '        End If
    '    End Try
    '    'System.Threading.Thread.Sleep(100)
    '    Application.DoEvents()
    'End Sub

    'Private Sub ТочкаTableAdapterUpdate(ByRef dsКамераDataSet As КамераDataSet, ByRef taТочкаTableAdapter As КамераDataSetTableAdapters.ТочкаTableAdapter, ByRef bsBindingSource As System.Windows.Forms.BindingSource)
    '    Me.Validate()
    '    bsBindingSource.EndEdit()

    '    Dim deletedChildRecords As DataTable = dsКамераDataSet.Точка.GetChanges(DataRowState.Deleted)
    '    Dim newChildRecords As DataTable = dsКамераDataSet.Точка.GetChanges(DataRowState.Added)
    '    Dim modifiedChildRecords As DataTable = dsКамераDataSet.Точка.GetChanges(DataRowState.Modified)

    '    Try
    '        If Not deletedChildRecords Is Nothing Then
    '            taТочкаTableAdapter.Update(deletedChildRecords)
    '        End If

    '        If Not modifiedChildRecords Is Nothing Then
    '            taТочкаTableAdapter.Update(modifiedChildRecords)
    '        End If

    '        If Not newChildRecords Is Nothing Then
    '            taТочкаTableAdapter.Update(newChildRecords)
    '        End If

    '        dsКамераDataSet.AcceptChanges()

    '    Catch ex As Exception
    '        MsgBox("Ошибка обновления в процедуре ТочкаTableAdapterUpdate.")
    '    Finally
    '        If Not deletedChildRecords Is Nothing Then
    '            deletedChildRecords.Dispose()
    '        End If

    '        If Not modifiedChildRecords Is Nothing Then
    '            modifiedChildRecords.Dispose()
    '        End If

    '        If Not newChildRecords Is Nothing Then
    '            newChildRecords.Dispose()
    '        End If
    '    End Try
    '    'System.Threading.Thread.Sleep(100)
    '    Application.DoEvents()
    'End Sub

    'Private Sub ГорелкаTableAdapterUpdate(ByRef dsКамераDataSet As КамераDataSet, ByRef taГорелкаTableAdapter As КамераDataSetTableAdapters.ГорелкаTableAdapter, ByRef bsBindingSource As System.Windows.Forms.BindingSource)
    '    Me.Validate()
    '    bsBindingSource.EndEdit()

    '    Dim deletedChildRecords As DataTable = dsКамераDataSet.Горелка.GetChanges(DataRowState.Deleted)
    '    Dim newChildRecords As DataTable = dsКамераDataSet.Горелка.GetChanges(DataRowState.Added)
    '    Dim modifiedChildRecords As DataTable = dsКамераDataSet.Горелка.GetChanges(DataRowState.Modified)

    '    Try
    '        If Not deletedChildRecords Is Nothing Then
    '            taГорелкаTableAdapter.Update(deletedChildRecords)
    '        End If

    '        If Not modifiedChildRecords Is Nothing Then
    '            taГорелкаTableAdapter.Update(modifiedChildRecords)
    '        End If

    '        If Not newChildRecords Is Nothing Then
    '            taГорелкаTableAdapter.Update(newChildRecords)
    '        End If

    '        dsКамераDataSet.AcceptChanges()

    '    Catch ex As Exception
    '        MsgBox("Ошибка обновления в процедуре ГорелкаTableAdapterUpdate.")
    '    Finally
    '        If Not deletedChildRecords Is Nothing Then
    '            deletedChildRecords.Dispose()
    '        End If

    '        If Not modifiedChildRecords Is Nothing Then
    '            modifiedChildRecords.Dispose()
    '        End If

    '        If Not newChildRecords Is Nothing Then
    '            newChildRecords.Dispose()
    '        End If
    '    End Try
    '    'System.Threading.Thread.Sleep(100)
    '    Application.DoEvents()
    'End Sub

#End Region

    ''' <summary>
    ''' Обновить содержимое дерева из DataSet источника или приёмника после проведения перемещений или удалений записей.
    ''' </summary>
    ''' <param name="tvwDBДерево"></param>
    ''' <param name="dsКамераDataSet"></param>
    Private Sub PopulateTree(ByRef tvwDBДерево As TreeView, ByRef dsКамераDataSet As КамераDataSet) 'ByRef dtИзделиеDataTable As ИзделиеDataTable)
        'Объявить переменные для объектов Доступа к данным.
        Dim newNode As TreeNode
        Dim percentPosition As Double
        Dim rowsCount As Double

        If dsКамераDataSet.Изделие.Rows.Count = 0 Then Exit Sub

        ' открыть  Progressbar.
        TSProgressBar.Visible = True
        rowsCount = dsКамераDataSet.Изделие.Rows.Count

        ' Пока запись  - не последняя запись  , прибавить объект ListItem.
        For Each ИзделиеRow As КамераDataSet.ИзделиеRow In dsКамераDataSet.Изделие
            '  Прибавьте Узел к TreeView, и настроить его реквизиты.
            newNode = tvwDBДерево.Nodes("Kor").Nodes.Add(ИзделиеRow.КодИзделия.ToString & " ID", ИзделиеRow.НомерИзделия.ToString, "closed")
            newNode.Tag = "Изделия" ' Идентифицирует таблицу.
            AddNodeFromИзделия(tvwDBДерево, dsКамераDataSet, newNode)
            percentPosition += 1
            TSProgressBar.Value = CInt((percentPosition / rowsCount) * 100)
        Next

        TSProgressBar.Visible = False
        'tvwDBДерево.Nodes.Item(0).Sorted = True
        ' раскрыть высший узел.
        tvwDBДерево.Nodes("Kor").Expand()
    End Sub

    ''' <summary>
    ''' Добавить узлы изделия
    ''' </summary>
    ''' <param name="tvwDBДерево"></param>
    ''' <param name="dsКамераDataSet"></param>
    ''' <param name="nodeTree"></param>
    Private Sub AddNodeFromИзделия(ByRef tvwDBДерево As TreeView, ByRef dsКамераDataSet As КамераDataSet, ByVal nodeTree As TreeNode)
        Dim newNode, mNodeПоле As TreeNode
        Dim NodeName, NodeText As String

        tvwDBДерево.BeginUpdate()
        nodeTree.Nodes.Clear()

        Select Case nodeTree.Tag.ToString
            Case "Изделия"
                ' Установить переменную intIndex к свойству Индекса
                ' Недавно созданного Узела. Использовать  эту переменную, чтобы прибавить нижний уровень
                ' в то время как на этой записи, создать recordset
                ' Использование запроса, который находит только номера полей
                For Each ПолеRow As КамераDataSet.ПолеRow In dsКамераDataSet.Поле.Select("КодИзделия = " & CStr(Val(nodeTree.Name)))

                    NodeName = ПолеRow.КодПоля.ToString & " ID" ' Уникальный ID.
                    NodeText = ПолеRow.НомерПоля.ToString & " Поле" ' Текст.
                    mNodeПоле = nodeTree.Nodes.Add(NodeName, NodeText) '(intIndex)
                    mNodeПоле.Tag = "Поле" ' Таблица имя.
                    mNodeПоле.ImageKey = "closed"

                    NodeName = ПолеRow.КодПоля.ToString & " K" ' & " ID"    ' Уникальный ID.
                    NodeText = "Горелка" ' Текст.
                    newNode = mNodeПоле.Nodes.Add(NodeName, NodeText) '(intIndexПоле)
                    newNode.Tag = "Горелка"
                    newNode.ImageKey = "closed"

                    NodeName = ПолеRow.КодПоля.ToString & " S" ' Уникальный ID.
                    NodeText = "Точка" ' Текст.
                    newNode = mNodeПоле.Nodes.Add(NodeName, NodeText) '(intIndexПоле)
                    newNode.Tag = "Точка" ' Таблица имя.
                    newNode.ImageKey = "closed"
                Next
        End Select

        tvwDBДерево.EndUpdate()
    End Sub

    ''' <summary>
    ''' Добавить узлы поля
    ''' </summary>
    ''' <param name="tvwDBДерево"></param>
    ''' <param name="dsКамераDataSet"></param>
    ''' <param name="nodeTree"></param>
    Private Sub AddNodeFromПоле(ByRef tvwDBДерево As TreeView, ByRef dsКамераDataSet As КамераDataSet, ByVal nodeTree As TreeNode)
        Dim newNode As TreeNode
        Dim nodeName, nodeText As String

        tvwDBДерево.BeginUpdate()
        nodeTree.Nodes.Clear()

        Select Case nodeTree.Tag.ToString
            Case "Точка"
                ' вставить 5 точек
                For Each ТочкаRow As КамераDataSet.ТочкаRow In dsКамераDataSet.Точка.Select("КодПоля = " & CStr(Val(nodeTree.Name)))
                    Dim middle As Double = 0

                    For I As Integer = 1 To ЧИСЛО_Т_309
                        middle += CDbl(ТочкаRow.Item("T309_" & I.ToString))
                    Next

                    middle = middle / ЧИСЛО_Т_309
                    nodeName = String.Format("{0}{1} ID", ТочкаRow.НомерТочки, ТочкаRow.КодПоля) ' Уникальный ID.
                    nodeText = String.Format("{0} Точка T309 = {1}", ТочкаRow.НомерТочки, Format(middle, "###0.0")) ' Текст.
                    newNode = nodeTree.Nodes.Add(nodeName, nodeText)
                    newNode.Tag = "ТочкаКонкретная" ' Таблица имя.
                    newNode.ImageKey = "Точка"     ' Картинка для ImageList.
                Next

                Exit Select
            Case "Горелка"
                ' вставить 28 горелок
                For Each rowГорелка As КамераDataSet.ГорелкаRow In dsКамераDataSet.Горелка.Select("КодПоля = " & CStr(Val(nodeTree.Name)))
                    Dim arrTemp(ЧИСЛО_ТЕРМОПАР) As Double
                    Dim middle As Double = 0
                    Dim max As Double = Double.MinValue
                    Dim min As Double = Double.MaxValue

                    nodeName = String.Format("{0}{1}{2} ID", rowГорелка.НомерГорелки, rowГорелка.КодПоля, rowГорелка.Температура) ' Уникальный ID.
                    nodeText = String.Empty

                    With rowГорелка
                        arrTemp(1) = .ПоясА1
                        arrTemp(2) = .ПоясБ1
                        arrTemp(3) = .ПоясА2
                        arrTemp(4) = .ПоясБ2
                        arrTemp(5) = .ПоясА3
                        arrTemp(6) = .ПоясБ3
                        arrTemp(7) = .ПоясА4
                        arrTemp(8) = .ПоясБ4
                        arrTemp(9) = .ПоясА5
                        arrTemp(10) = .ПоясБ5

                        Select Case .Температура
                            Case "min"
                                For I As Integer = 1 To ЧИСЛО_ТЕРМОПАР
                                    If min > arrTemp(I) Then
                                        min = arrTemp(I)
                                    End If
                                Next

                                nodeText = $"{ .НомерГорелки} мин = {Format(min, "###0.0")}" ' " Горелка-мин" ' Текст.

                                Exit Select
                            Case "max"
                                For I As Integer = 1 To ЧИСЛО_ТЕРМОПАР
                                    If max < arrTemp(I) Then
                                        max = arrTemp(I)
                                    End If
                                Next

                                nodeText = $"{ .НомерГорелки} макс = {Format(max, "###0.0")}" '" Горелка-мах" ' Текст.

                                Exit Select
                            Case "midl"
                                For I As Integer = 1 To ЧИСЛО_ТЕРМОПАР
                                    middle += arrTemp(I)
                                Next
                                middle = middle / ЧИСЛО_ТЕРМОПАР

                                nodeText = $"{ .НомерГорелки} средн = {Format(middle, "###0.0")}" ' & " Горелка-сред" ' Текст.

                                Exit Select
                        End Select
                    End With

                    newNode = nodeTree.Nodes.Add(nodeName, nodeText)
                    newNode.Tag = "ГорелкаКонкретная" ' Таблица имя.
                    newNode.ImageKey = "Горелка" ' Картинка для ImageList.
                Next

                Exit Select
        End Select

        tvwDBДерево.EndUpdate()
    End Sub

    ''' <summary>
    ''' Извлечение метаданных свойств таблицы
    ''' </summary>
    ''' <param name="conn"></param>
    ''' <param name="indexOfset"></param>
    Private Sub ShowPropertiesBaseToStatusBar(ByRef conn As OleDbConnection, ByVal indexOfset As Integer)
        'Dim conn As New OleDbConnection(BuildCnnStr(strProviderJet, strПутьChannels))
        conn.Open()
        Dim schemaTable As DataTable = conn.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, Nothing) 'New Object() {Nothing, Nothing, Nothing, "TABLE"})
        conn.Close()

        For Each row As DataRow In schemaTable.Rows
            If row("TABLE_NAME").ToString = "Изделие" Then
                'Открытие основной таблицы и вывод основных параметров
                'sbrСостояния.Items(0).Text = "Имя:" & row("TABLE_NAME").ToString
                sbrСостояния.Items(0 + indexOfset).Text = "Создана:" & row("DATE_CREATED").ToString
                sbrСостояния.Items(1 + indexOfset).Text = "Модификация:" & row("DATE_MODIFIED").ToString
                sbrСостояния.Items(2 + indexOfset).Text = "Тип:" & row("TABLE_TYPE").ToString
                'TABLE_CATALOG()
                'TABLE_SCHEMA()
                'TABLE_NAME()
                'TABLE_TYPE()
                'TABLE_GUID()
                'DESCRIPTION()
                'TABLE_PROPID()
                'DATE_CREATED()
                'DATE_MODIFIED()
                Exit For
            End If
        Next row
    End Sub

    'Private Sub lvwDBЛистИсточник_ColumnClick(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.ColumnClickEventArgs) Handles lvwDBЛистИсточник.ColumnClick
    '    Dim ColumnHeader As System.Windows.Forms.ColumnHeader = lvwDBЛистИсточник.Columns(eventArgs.Column)
    '    'lvwDBЛистИсточник.SortKey = ColumnHeader.Index - 1
    '    'Установить Sorted в True для сортировки листа.
    '    lvwDBЛистИсточник.Sort()
    'End Sub

    Private Sub SetColumnsГорелка(ByRef lvwDBЛист As ListView)
        ' Очистка ColumnHeaders коллекции.
        lvwDBЛист.Columns.Clear()
        lvwDBЛист.Columns.Add("", "Горелка", 60)

        '  Добавить 10 ColumnHeaders.
        For I As Integer = 1 To ЧИСЛО_ТЕРМОПАР
            If I Mod 2 <> 0 Then ' нечетные
                lvwDBЛист.Columns.Add("", "A" & ((I \ 2) + 1).ToString, LW_WIDHT)
            Else
                lvwDBЛист.Columns.Add("", "Б" & (I \ 2).ToString, LW_WIDHT)
            End If
        Next I
    End Sub

    Private Sub SetColumnsТочка(ByRef lvwDBЛист As ListView)
        ' Очистка ColumnHeaders коллекции.
        lvwDBЛист.Columns.Clear()
        lvwDBЛист.Columns.Add("", "Точка", LW_WIDHT)
        '  Добавить 29 ColumnHeaders.
        lvwDBЛист.Columns.Add("", "P1", LW_WIDHT)
        lvwDBЛист.Columns.Add("", "dP1", LW_WIDHT)
        lvwDBЛист.Columns.Add("", "T3", LW_WIDHT)

        lvwDBЛист.Columns.Add("", "Pотб", LW_WIDHT)
        lvwDBЛист.Columns.Add("", "dPотб", LW_WIDHT)
        lvwDBЛист.Columns.Add("", "T3отб", LW_WIDHT)

        lvwDBЛист.Columns.Add("", "ДавлениеМагистралеОтбораВн", LW_WIDHT)
        lvwDBЛист.Columns.Add("", "ПерепадДавленияВоздухаОтбораВн", LW_WIDHT)
        lvwDBЛист.Columns.Add("", "ТотбораВн", LW_WIDHT)

        lvwDBЛист.Columns.Add("", "P310", LW_WIDHT)
        lvwDBЛист.Columns.Add("", "P311", LW_WIDHT)
        lvwDBЛист.Columns.Add("", "ТтопКС", LW_WIDHT)
        lvwDBЛист.Columns.Add("", "ТтоплКП", LW_WIDHT)
        lvwDBЛист.Columns.Add("", "Тбокса", LW_WIDHT)
        lvwDBЛист.Columns.Add("", "GтКС", LW_WIDHT)
        lvwDBЛист.Columns.Add("", "GтКП", LW_WIDHT)
        lvwDBЛист.Columns.Add("", "ТвоздухаНаВходеКП", LW_WIDHT)

        For I As Integer = 1 To ЧИСЛО_Т_309
            lvwDBЛист.Columns.Add("", "T309_" & I, LW_WIDHT)
        Next I

        'SetView(View.Details)
        'Dim lvColumnHeader As ColumnHeader
        'lvColumnHeader = ListViewInput.Columns.Add("Column1")
        'lvColumnHeader.Width = 100
    End Sub

    Private Sub SetColumnsИзделия(ByRef lvwDBЛист As ListView)
        ' Очистка ColumnHeaders коллекции.
        lvwDBЛист.Columns.Clear()
        lvwDBЛист.Columns.Add("", "Дата", 76)
        lvwDBЛист.Columns.Add("", "№ стенда", LW_WIDHT)
        lvwDBЛист.Columns.Add("", "Программа", LW_WIDHT)
        lvwDBЛист.Columns.Add("", "№ корп.камеры", LW_WIDHT)
        lvwDBЛист.Columns.Add("", "№ жаровой трубы", LW_WIDHT)
        lvwDBЛист.Columns.Add("", "№ коллектора", LW_WIDHT)
        lvwDBЛист.Columns.Add("", "Барометр", LW_WIDHT)
    End Sub

    Private Sub SetColumnsПоле(ByRef lvwDBЛист As ListView)
        ' Очистка ColumnHeaders коллекции.
        lvwDBЛист.Columns.Clear()
        lvwDBЛист.Columns.Add("", "№ поля", 66)
        lvwDBЛист.Columns.Add("", "Время запуска", 70)
        lvwDBЛист.Columns.Add("", "Время хода", 70)
    End Sub

    Private Sub ОбновитьТочка(ByRef dsКамераDataSet As КамераDataSet, ByRef lvwDBЛист As ListView, ByVal кодПоля As String)
        'Dim lvItem As ListViewItem
        '    lvItem = ListViewInput.Items.Add("ListViewItem1")
        '    lvItem.ImageKey = "Graph1"
        '    lvItem.SubItems.AddRange(New String() {"Column2", "Column3"})
        Dim itemList As ListViewItem ' Переменная Уровня модуля ListItem.
        ' Включить Progress bar
        TSProgressBar.Visible = True
        ' Очистите старые заглавия
        lvwDBЛист.Items.Clear()

        ' Определить объект variable типа Recordset
        Dim percentPosition As Double
        Dim rowsCount As Double
        Dim rowsТочка() As КамераDataSet.ТочкаRow = CType(dsКамераDataSet.Точка.Select("КодПоля = " & кодПоля), КамераDataSet.ТочкаRow())
        Dim I As Integer

        rowsCount = rowsТочка.Count

        For Each rowТочка As КамераDataSet.ТочкаRow In rowsТочка
            I = 0
            With rowТочка
                itemList = lvwDBЛист.Items.Add(rowТочка.НомерТочки.ToString & " ID", rowТочка.НомерТочки.ToString, "Точка") : I += 1
                itemList.SubItems.Insert(I, New ListViewItem.ListViewSubItem(Nothing, CStr(.P1))) : I += 1
                itemList.SubItems.Insert(I, New ListViewItem.ListViewSubItem(Nothing, CStr(.dP1))) : I += 1
                itemList.SubItems.Insert(I, New ListViewItem.ListViewSubItem(Nothing, CStr(.T3))) : I += 1

                itemList.SubItems.Insert(I, New ListViewItem.ListViewSubItem(Nothing, CStr(.Pотб))) : I += 1
                itemList.SubItems.Insert(I, New ListViewItem.ListViewSubItem(Nothing, CStr(.dPотб))) : I += 1
                itemList.SubItems.Insert(I, New ListViewItem.ListViewSubItem(Nothing, CStr(.Tотб))) : I += 1

                itemList.SubItems.Insert(I, New ListViewItem.ListViewSubItem(Nothing, CStr(.ДавлениеМагистралеОтбораВн))) : I += 1
                itemList.SubItems.Insert(I, New ListViewItem.ListViewSubItem(Nothing, CStr(.ПерепадДавленияВоздухаОтбораВн))) : I += 1
                itemList.SubItems.Insert(I, New ListViewItem.ListViewSubItem(Nothing, CStr(.ТотбораВн))) : I += 1

                itemList.SubItems.Insert(I, New ListViewItem.ListViewSubItem(Nothing, CStr(.P310))) : I += 1
                itemList.SubItems.Insert(I, New ListViewItem.ListViewSubItem(Nothing, CStr(.P311))) : I += 1
                itemList.SubItems.Insert(I, New ListViewItem.ListViewSubItem(Nothing, CStr(.TтоплКС))) : I += 1
                itemList.SubItems.Insert(I, New ListViewItem.ListViewSubItem(Nothing, CStr(.TтоплКП))) : I += 1
                itemList.SubItems.Insert(I, New ListViewItem.ListViewSubItem(Nothing, CStr(.Tбокса))) : I += 1
                itemList.SubItems.Insert(I, New ListViewItem.ListViewSubItem(Nothing, CStr(.GтКС))) : I += 1
                itemList.SubItems.Insert(I, New ListViewItem.ListViewSubItem(Nothing, CStr(.GтКП))) : I += 1
                itemList.SubItems.Insert(I, New ListViewItem.ListViewSubItem(Nothing, CStr(.ТвоздухаНаВходеКП))) : I += 1

                itemList.SubItems.Insert(I, New ListViewItem.ListViewSubItem(Nothing, CStr(.T309_1))) : I += 1
                itemList.SubItems.Insert(I, New ListViewItem.ListViewSubItem(Nothing, CStr(.T309_2))) : I += 1
                itemList.SubItems.Insert(I, New ListViewItem.ListViewSubItem(Nothing, CStr(.T309_3))) : I += 1
                itemList.SubItems.Insert(I, New ListViewItem.ListViewSubItem(Nothing, CStr(.T309_4))) : I += 1
                itemList.SubItems.Insert(I, New ListViewItem.ListViewSubItem(Nothing, CStr(.T309_5))) : I += 1
                itemList.SubItems.Insert(I, New ListViewItem.ListViewSubItem(Nothing, CStr(.T309_6))) : I += 1
                itemList.SubItems.Insert(I, New ListViewItem.ListViewSubItem(Nothing, CStr(.T309_7))) : I += 1
                itemList.SubItems.Insert(I, New ListViewItem.ListViewSubItem(Nothing, CStr(.T309_8))) : I += 1
                itemList.SubItems.Insert(I, New ListViewItem.ListViewSubItem(Nothing, CStr(.T309_9))) : I += 1
                itemList.SubItems.Insert(I, New ListViewItem.ListViewSubItem(Nothing, CStr(.T309_10))) : I += 1
                itemList.SubItems.Insert(I, New ListViewItem.ListViewSubItem(Nothing, CStr(.T309_11))) : I += 1
                itemList.SubItems.Insert(I, New ListViewItem.ListViewSubItem(Nothing, CStr(.T309_12))) : I += 1
            End With

            percentPosition += 1
            TSProgressBar.Value = CInt((percentPosition / rowsCount) * 100)
        Next

        TSProgressBar.Visible = False
    End Sub

    Private Sub ОбновитьГорелки(ByRef dsКамераDataSet As КамераDataSet, ByRef lvwDBЛист As ListView, ByVal кодПоля As String)
        Dim mItemList As ListViewItem ' Переменная Уровня модуля ListItem.
        ' Включить Progress bar
        TSProgressBar.Visible = True
        ' Очистите старые заглавия
        lvwDBЛист.Items.Clear()

        ' Определить объект variable типа Recordset
        Dim percentPosition As Double
        Dim rowsCount As Double
        Dim rowsГорелка() As КамераDataSet.ГорелкаRow = CType(dsКамераDataSet.Горелка.Select("КодПоля = " & кодПоля), КамераDataSet.ГорелкаRow())

        rowsCount = rowsГорелка.Count

        For Each rowГорелка As КамераDataSet.ГорелкаRow In rowsГорелка
            With rowГорелка
                mItemList = lvwDBЛист.Items.Add(rowГорелка.НомерГорелки.ToString & rowГорелка.Температура, rowГорелка.НомерГорелки.ToString & rowГорелка.Температура, "Горелка")
                mItemList.SubItems.Insert(1, New ListViewItem.ListViewSubItem(Nothing, CStr(.ПоясА1)))
                mItemList.SubItems.Insert(2, New ListViewItem.ListViewSubItem(Nothing, CStr(.ПоясБ1)))
                mItemList.SubItems.Insert(3, New ListViewItem.ListViewSubItem(Nothing, CStr(.ПоясА2)))
                mItemList.SubItems.Insert(4, New ListViewItem.ListViewSubItem(Nothing, CStr(.ПоясБ2)))
                mItemList.SubItems.Insert(5, New ListViewItem.ListViewSubItem(Nothing, CStr(.ПоясА3)))
                mItemList.SubItems.Insert(6, New ListViewItem.ListViewSubItem(Nothing, CStr(.ПоясБ3)))
                mItemList.SubItems.Insert(7, New ListViewItem.ListViewSubItem(Nothing, CStr(.ПоясА4)))
                mItemList.SubItems.Insert(8, New ListViewItem.ListViewSubItem(Nothing, CStr(.ПоясБ4)))
                mItemList.SubItems.Insert(9, New ListViewItem.ListViewSubItem(Nothing, CStr(.ПоясА5)))
                mItemList.SubItems.Insert(10, New ListViewItem.ListViewSubItem(Nothing, CStr(.ПоясБ5)))
            End With

            percentPosition += 1
            TSProgressBar.Value = CInt((percentPosition / rowsCount) * 100)
        Next

        TSProgressBar.Visible = False
    End Sub

    Private Sub ОбновитьИзделия(ByRef dsКамераDataSet As КамераDataSet, ByRef lvwDBЛист As ListView, ByVal кодИзделия As String)
        Dim mItemList As ListViewItem ' Переменная Уровня модуля ListItem.
        ' Включить Progress bar
        TSProgressBar.Visible = True
        ' Очистите старые заглавия
        lvwDBЛист.Items.Clear()

        ' Определить объект variable типа Recordset
        Dim percentPosition As Double
        Dim rowsCount As Double
        Dim rowsИзделие() As КамераDataSet.ИзделиеRow = CType(dsКамераDataSet.Изделие.Select("КодИзделия = " & кодИзделия), КамераDataSet.ИзделиеRow())

        rowsCount = rowsИзделие.Count
        For Each rowИзделие As КамераDataSet.ИзделиеRow In rowsИзделие
            With rowИзделие
                mItemList = lvwDBЛист.Items.Add(.КодИзделия.ToString & " ID", .Дата.ToShortDateString, "Изделие")
                mItemList.SubItems.Insert(1, New ListViewItem.ListViewSubItem(Nothing, CStr(.НомерСтенда)))
                mItemList.SubItems.Insert(2, New ListViewItem.ListViewSubItem(Nothing, CStr(.ПрогрИспытания)))
                mItemList.SubItems.Insert(3, New ListViewItem.ListViewSubItem(Nothing, CStr(.НомерКорпусаКамеры)))
                mItemList.SubItems.Insert(4, New ListViewItem.ListViewSubItem(Nothing, CStr(.НомерЖаровойТрубы)))
                mItemList.SubItems.Insert(5, New ListViewItem.ListViewSubItem(Nothing, CStr(.НомерКоллектора)))
                mItemList.SubItems.Insert(6, New ListViewItem.ListViewSubItem(Nothing, CStr(.Барометр)))
            End With

            percentPosition += 1
            TSProgressBar.Value = CInt((percentPosition / rowsCount) * 100)
        Next

        TSProgressBar.Visible = False
    End Sub

    Private Sub ОбновитьПоле(ByRef dsКамераDataSet As КамераDataSet, ByRef lvwDBЛист As ListView, ByVal кодПоля As String)
        Dim mItemList As ListViewItem ' Переменная Уровня модуля ListItem.
        ' Включить Progress bar
        TSProgressBar.Visible = True
        ' Очистите старые заглавия
        lvwDBЛист.Items.Clear()

        Dim percentPosition As Double
        Dim rowsCount As Double
        Dim rowsПоле() As КамераDataSet.ПолеRow = CType(dsКамераDataSet.Поле.Select("КодПоля = " & кодПоля), КамераDataSet.ПолеRow())

        rowsCount = rowsПоле.Count

        For Each rowПоле As КамераDataSet.ПолеRow In rowsПоле
            With rowПоле
                mItemList = lvwDBЛист.Items.Add(.КодПоля.ToString & " ID", .НомерПоля.ToString, "Поле")
                mItemList.SubItems.Insert(1, New ListViewItem.ListViewSubItem(Nothing, CStr(.ВремяЗапуска)))
                mItemList.SubItems.Insert(2, New ListViewItem.ListViewSubItem(Nothing, .ВремяХодаТурели.Minute.ToString))
            End With

            percentPosition += 1
            TSProgressBar.Value = CInt((percentPosition / rowsCount) * 100)
        Next

        TSProgressBar.Visible = False
    End Sub

    Private Sub TreeViewInput_AfterCollapse(ByVal eventSender As Object, ByVal eventArgs As TreeViewEventArgs) Handles TreeViewInput.AfterCollapse
        Dim node As TreeNode = eventArgs.Node
        'If Node.Tag = "Горелка" Or Node.Index = 1 _
        ''Then Node.Image = "closed"
        node.ImageKey = "closed"
    End Sub

    Private Sub TreeViewOutput_AfterCollapse(ByVal eventSender As Object, ByVal eventArgs As TreeViewEventArgs) Handles TreeViewOutput.AfterCollapse
        Dim Node As TreeNode = eventArgs.Node
        'If Node.Tag = "Горелка" Or Node.Index = 1 _
        ''Then Node.Image = "closed"
        Node.ImageKey = "closed"
    End Sub

    Private Sub TreeViewInput_AfterExpand(ByVal eventSender As Object, ByVal eventArgs As TreeViewEventArgs) Handles TreeViewInput.AfterExpand
        Dim node As TreeNode = eventArgs.Node
        'If Node.Tag = "Горелка" Or Node.Index = 1 Then
        '   Node.Image = "open"
        '    Node.Sorted = True
        'End If
        node.ImageKey = "open"
        'Node.Sorted = True
    End Sub

    Private Sub TreeViewOutput_AfterExpand(ByVal eventSender As Object, ByVal eventArgs As TreeViewEventArgs) Handles TreeViewOutput.AfterExpand
        Dim node As TreeNode = eventArgs.Node
        'If Node.Tag = "Горелка" Or Node.Index = 1 Then
        '   Node.Image = "open"
        '    Node.Sorted = True
        'End If
        node.ImageKey = "open"
        'Node.Sorted = True
    End Sub

    Private Sub TreeViewInput_BeforeExpand(ByVal sender As Object, ByVal e As TreeViewCancelEventArgs) Handles TreeViewInput.BeforeExpand
        ' заполняются последующие узлы
        ' например узлы точек и горелок
        Select Case e.Node.Tag.ToString
            Case "Поле"
                TreeViewInput.BeginUpdate()
                ' пройти по 2 узлам "Точка" и "Горелка"
                For Each tn As TreeNode In e.Node.Nodes
                    AddNodeFromПоле(TreeViewInput, КамераInputDataSet, tn)
                Next tn
                TreeViewInput.EndUpdate()
        End Select
    End Sub

    Private Sub TreeViewOutput_BeforeExpand(ByVal sender As Object, ByVal e As TreeViewCancelEventArgs) Handles TreeViewOutput.BeforeExpand
        Select Case e.Node.Tag.ToString
            Case "Поле"
                TreeViewOutput.BeginUpdate()
                ' пройти по 2 узлам "Точка" и "Горелка"
                For Each tn As TreeNode In e.Node.Nodes
                    AddNodeFromПоле(TreeViewOutput, КамераOutputDataSet, tn)
                Next tn
                TreeViewOutput.EndUpdate()
        End Select
    End Sub

    Private Sub TreeView_Input_Output_AfterSelect(ByVal sender As Object, ByVal e As TreeViewEventArgs) Handles TreeViewInput.AfterSelect, TreeViewOutput.AfterSelect
        'flvFiles.ShowFiles(tvea.Node.FullPath)
        TreeViewNodeSelect(sender, e.Node)
    End Sub

    Private Sub TreeViewNodeSelect(ByVal sender As Object, ByVal Node As TreeNode)
        Dim vwDBЛист As ListView
        Dim dsКамераDataSet As КамераDataSet

        If CType(sender, TreeView) Is TreeViewInput Then
            vwDBЛист = ListViewInput
            dsКамераDataSet = КамераInputDataSet
            удаляемаяБаза = WhatBase.InputBase
        Else
            vwDBЛист = ListViewOutput
            dsКамераDataSet = КамераOutputDataSet
            удаляемаяБаза = WhatBase.OutputBase
        End If

        ' проверка Tag на значение "Горелка"  и "Точка"
        Select Case Node.Tag.ToString
            Case "Изделия"
                SetColumnsИзделия(vwDBЛист)
                ОбновитьИзделия(dsКамераDataSet, vwDBЛист, CStr(Val(Node.Name)))
                keyID = CInt(Val(Node.Name))
                'blnУдалитьИсточник = True
            Case "Поле"
                SetColumnsПоле(vwDBЛист)
                ОбновитьПоле(dsКамераDataSet, vwDBЛист, CStr(Val(Node.Name)))
            Case "Точка"
                SetColumnsТочка(vwDBЛист)
                ОбновитьТочка(dsКамераDataSet, vwDBЛист, CStr(Val(Node.Name)))
            Case "Горелка"
                SetColumnsГорелка(vwDBЛист)
                ОбновитьГорелки(dsКамераDataSet, vwDBЛист, CStr(Val(Node.Name)))
        End Select
    End Sub

    Private Sub TSMenuItemDelete_Click(ByVal sender As Object, ByVal e As EventArgs) Handles TSMenuItemDelete.Click
        DeleteActiveBase()
    End Sub

    Private Sub DeleteActiveBase()
        ' обработка удаления lngKeyID а затем присвоить 0
        If keyID = 0 Then Exit Sub

        Select Case удаляемаяБаза
            Case WhatBase.InputBase
                If TreeViewInput.Nodes("Kor").Nodes.Count = 0 Then
                    keyID = 0
                    Exit Sub
                End If
                Exit Select
            Case WhatBase.OutputBase
                If TreeViewOutput.Nodes("Kor").Nodes.Count = 0 Then
                    keyID = 0
                    Exit Sub
                End If
                Exit Select
        End Select

        Select Case удаляемаяБаза
            Case WhatBase.InputBase
                ' удалить запись у источника
                СчитатьIDТермопар(КамераInputDataSet, keyID.ToString)
                УдалитьКамеруИзMemoDataSet(КамераInputDataSet)
                ' физическое удаление из базы
                ГребенкаАTableAdapterUpdate(КамераInputDataSet, ГребенкаАInputTableAdapter, ГребенкаАInputBindingSource)
                ГребенкаБTableAdapterUpdate(КамераInputDataSet, ГребенкаБInputTableAdapter, ГребенкаБInputBindingSource)
                FillAdapterInput()
                ClearTreeView(TreeViewInput, ListViewInput)
                PopulateTree(TreeViewInput, КамераInputDataSet)

                Exit Select
            Case WhatBase.OutputBase
                ' удалить запись у приемника
                СчитатьIDТермопар(КамераOutputDataSet, keyID.ToString)
                УдалитьКамеруИзMemoDataSet(КамераOutputDataSet)
                ' физическое удаление из базы
                ГребенкаАTableAdapterUpdate(КамераOutputDataSet, ГребенкаАOutputTableAdapter, ГребенкаАOutputBindingSource)
                ГребенкаБTableAdapterUpdate(КамераOutputDataSet, ГребенкаБOutputTableAdapter, ГребенкаБOutputBindingSource)
                FillAdapterOutput()
                ClearTreeView(TreeViewOutput, ListViewOutput)
                PopulateTree(TreeViewOutput, КамераOutputDataSet)

                Exit Select
        End Select

        PopulateListBox()
        ' после этого обновление дерева
        keyID = 0
    End Sub

    ''' <summary>
    ''' Восстанавливает и испорченную базу данных и выполняет ее сжатие.
    ''' </summary>
    ''' <param name="inWhatBase"></param>
    ''' <remarks></remarks>
    Private Sub CompressAndRestoreBase(ByVal inWhatBase As WhatBase)
        'Dim je As New JRO.JetEngine
        'Dim strBackup As String = My.Application.Info.DirectoryPath & "\Copy.mdb"
        Dim fileName As String = String.Empty ' где будет копия

        Select Case inWhatBase
            Case WhatBase.InputBase ' сжать источник
                fileName = TextPathSource.Text
                InputTableAdapterConnectionClose()
                Exit Select
            Case WhatBase.OutputBase ' сжать приемник
                fileName = TextPathReceiver.Text
                OutputTableAdapterConnectionClose()
                Exit Select
        End Select

        ' сжатие баз источника и приемника
        ' заменил работу JRO в отдельном исполняемом файле
        ' было

        ''**********************************************************
        '' Сделать рабочую копию базы данных.
        ''**********************************************************
        'Try
        '    File.Copy(fileName, strBackup, True)
        'Catch ex As Exception
        '    MessageBox.Show(ex.ToString, "Ошибка копирования базы данных при сжатии", MessageBoxButtons.OK, MessageBoxIcon.Warning)
        'End Try
        'Application.DoEvents()

        'Try
        '    File.Delete(fileName)
        '    System.Threading.Thread.Sleep(200)
        '    '******************************************************
        '    ' Восстановленные базы данных должны быть сжаты.
        '    '******************************************************
        '    je.CompactDatabase(PROVIDER_JET & "Data Source=" & strBackup & ";", PROVIDER_JET & "Data Source=" & fileName & ";Jet OLEDB:Encrypt Database=True")
        '    '******************************************************
        '    ' В случае успеха удалить резервную копию.
        '    '******************************************************
        '    File.Delete(strBackup)
        '    Application.DoEvents()
        'Catch ex As Exception
        '    MessageBox.Show(ex.ToString, "Ошибка восстановления базы данных при сжатии", MessageBoxButtons.OK, MessageBoxIcon.Warning)
        'Finally
        '    Beep()
        'End Try


        Try
            Dim startInfo As New ProcessStartInfo(Path.Combine(PathResourses, "BackupJRO.exe")) With {
                .WindowStyle = ProcessWindowStyle.Minimized, 'Normal 
                .WorkingDirectory = PathResourses
            }
            'startInfo.UseShellExecute = True
            'startInfo.WorkingDirectory = Path.Combine(Path.GetDirectoryName(Process.GetCurrentProcess().MainModule.FileName), "Store\МодулиРасчета\Chamber")

            ' сжать базу Channels
            WriteINI(Path.Combine(PathResourses, "Опции.ini"), "BackupJRO", "PathTarget", fileName)
            Process.Start(startInfo)
            Threading.Thread.Sleep(5000)
        Catch ex As Exception
            Const captionEx As String = "Ошибка запуска BackupJRO.exe"
            Dim textEx As String = String.Format("Функция Process.Start привела к ошибке:{0}{1}", Environment.NewLine, ex.Message)
            Console.WriteLine(String.Format("{0}{1}{2}", captionEx, Environment.NewLine, textEx))
            Console.ReadKey()
        End Try

        ' востановление ссылок
        Select Case inWhatBase
            Case WhatBase.InputBase
                FillAdapterInput()
                ClearTreeView(TreeViewInput, ListViewInput)
                PopulateTree(TreeViewInput, КамераInputDataSet)
                Exit Select
            Case WhatBase.OutputBase
                FillAdapterOutput()
                ClearTreeView(TreeViewOutput, ListViewOutput)
                PopulateTree(TreeViewOutput, КамераOutputDataSet)
                Exit Select
        End Select

        PopulateListBox()
    End Sub

    Private Sub LoadPathToBase()
        Try
            Dim documentSettings = New XDocument

            documentSettings = XDocument.Load(pathOption)
            gFileИсточник = documentSettings...<ПутьИсточника>.Value
            gFileПриемник = documentSettings...<ПутьПриемника>.Value
        Catch ex As Exception
            MessageBox.Show(Me, "Ошибка в процедуре СчитатьПутиКБазам." & vbCr & vbLf & "Error: " & ex.Message, Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub SavePathToBase()
        Try
            Dim documentSettings = New XDocument

            documentSettings = XDocument.Load(pathOption)
            documentSettings...<ПутьИсточника>.Value = gFileИсточник
            documentSettings...<ПутьПриемника>.Value = gFileПриемник
            documentSettings.Save(pathOption)
        Catch ex As Exception
            MessageBox.Show(Me, $"Ошибка в процедуре СохранитьПутиКБазам.{vbCr}{vbLf}Error: {ex.Message}",
                            Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

End Class
