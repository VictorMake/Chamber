Option Strict Off

Imports System.Drawing
Imports System.IO
Imports System.Threading
Imports System.Windows.Forms
Imports Microsoft.Office.Interop

Public Class FormReportGrid
    Private Const ПРОТОКОЛ_ПОЛЯ As String = "Протокол Поля"
    Private Const ГРАФИКИ As String = "Графики"
    Private Const ПОЛЕ_ПО_ПОЯСАМ As String = "ПолеПоПоясам"
    Private Const COLUMN_KT As String = "№ КТ"
    Private Const NameSmallReportExcel As String = "ОтчетИспытанияКамеры.xlsx"

    Private pathSmallReportExcel As String = Path.Combine(PathResourses, NameSmallReportExcel)

    Public Sub New()
        ' This call is required by the Windows Form Designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.

        With DataGridViewReport
            .Visible = True
            '.AutoGenerateColumns = False
            '.AlternatingRowsDefaultCellStyle.BackColor = Color.Lavender
            .BackColor = Color.WhiteSmoke
            .ForeColor = Color.MidnightBlue
            .CellBorderStyle = DataGridViewCellBorderStyle.None
            .ColumnHeadersDefaultCellStyle.Font = New Font("Tahoma", 8.0!, FontStyle.Bold)
            .ColumnHeadersDefaultCellStyle.BackColor = Color.MidnightBlue
            .ColumnHeadersDefaultCellStyle.ForeColor = Color.WhiteSmoke
            .DefaultCellStyle.ForeColor = Color.MidnightBlue
            .DefaultCellStyle.BackColor = Color.WhiteSmoke

            'dataGridView1.Name = "dataGridView1"
            .AllowUserToOrderColumns = True
            .AllowUserToDeleteRows = False
            .AllowUserToAddRows = False
            ' Render alternating rows with a different background color
            .AlternatingRowsDefaultCellStyle.BackColor = Color.LightSteelBlue 'SystemColors.InactiveCaptionText
            '.AutoGenerateColumns = False
            '.DataSource = bindingSourceEmployee
            ' только одно выделение поддерживается за раз
            .MultiSelect = False
            ' конфигурировать сетку используя выделение по ячейке
            .SelectionMode = DataGridViewSelectionMode.CellSelect
            ' потому что сетка будет содержать несвязанные колонки VirtualMode обязан быть включён
            .VirtualMode = True
        End With
    End Sub

    'Private Sub ReportGridForm_FormClosed(ByVal sender As Object, ByVal e As FormClosedEventArgs) Handles Me.FormClosed
    '    ExcelSmallReportExcel = Nothing
    'End Sub

    Private Sub ReportGridForm_FormClosing(ByVal sender As Object, ByVal e As FormClosingEventArgs) Handles Me.FormClosing
        If Not gMainFomMdiParent.IsWindowClosed Then
            e.Cancel = True
        End If
    End Sub

#Region "ПечатьЗаписьПоляПротокола"
    Private Sub MenuSaveProtocolAsXLS_Click(ByVal sender As Object, ByVal e As EventArgs) Handles MenuSaveProtocolAsXLS.Click
        ' можно записать в базу а затем считать в грид
        If CheckExistsFile(pathSmallReportExcel) Then
            MenuSaveProtocolAsXLS.Enabled = False
            SaveProtocolAsXLSAsync(MenuSaveProtocolAsXLS, "", True)
        End If
    End Sub

    ''' <summary>
    ''' Сохранить Excel Workbook.
    ''' Подготовка и обертка.
    ''' </summary>
    ''' <param name="inMenuItem"></param>
    ''' <param name="saveFilename"></param>
    ''' <param name="isVisible"></param>
    Private Async Sub SaveProtocolAsXLSAsync(inMenuItem As ToolStripMenuItem, ByVal saveFilename As String, ByVal isVisible As Boolean)
        Dim mFormMessage As FormMessage = New FormMessage With {.Text = "Запись"}

        mFormMessage.LabelMessage.Text = "Подождите, идет запись"
        mFormMessage.Show()
        mFormMessage.TopMost = True
        mFormMessage.Activate()
        mFormMessage.Refresh()
        Refresh()
        Await SaveProtocolAsXLSAsyncTask("", True)
        mFormMessage.Close()
        inMenuItem.Enabled = True
    End Sub

    Private Sub MenuPrintProtocol_Click(ByVal sender As Object, ByVal e As EventArgs) Handles MenuPrintProtocol.Click
        If CheckExistsFile(pathSmallReportExcel) Then
            MenuPrintProtocol.Enabled = False
            PrintSmallReportExcelAsync(MenuPrintProtocol, pathSmallReportExcel)
        End If
    End Sub

    ''' <summary>
    ''' Печать Протокола Поля И Подсчета.
    ''' Подготовка и обертка.
    ''' </summary>
    ''' <param name="inMenuItem"></param>
    Private Async Sub PrintSmallReportExcelAsync(inMenuItem As ToolStripMenuItem, ByVal pathSmallReportExcel As String)
        Dim mFormMessage As FormMessage = New FormMessage

        mFormMessage.Show()
        mFormMessage.TopMost = True
        mFormMessage.Activate()
        mFormMessage.Refresh()
        Refresh()
        Await PrintSmallReportExcelAsyncTask(pathSmallReportExcel)
        mFormMessage.Close()
        inMenuItem.Enabled = True
    End Sub

    ''' <summary>
    ''' Печать Протокола Поля И Подсчета.
    ''' Запуск задачи асинхронно.
    ''' </summary>
    ''' <param name="pathSmallReportExcel"></param>
    ''' <returns></returns>
    Private Function PrintSmallReportExcelAsyncTask(ByVal pathSmallReportExcel As String) As Tasks.Task
        Return Tasks.Task.Run(Sub()
                                  PrintSmallReportExcelTask(pathSmallReportExcel)
                              End Sub)
    End Function

    ''' <summary>
    ''' Печать Протокола Поля И Подсчета.
    ''' Задача в собственном потоке.
    ''' </summary>
    ''' <param name="pathSmallReportExcel"></param>
    Private Sub PrintSmallReportExcelTask(ByVal pathSmallReportExcel As String)
        Dim ExcelSmallReportExcel As New Excel.Application

        Try
            ' установить ссылку открыть книгу
            ExcelSmallReportExcel.Visible = False ' было False
            ExcelSmallReportExcel.Workbooks.Open(Filename:=pathSmallReportExcel)
            WorkOnExcelWorksheets(ExcelSmallReportExcel, True)
            ' запрос на изменения не надо
            ExcelSmallReportExcel.ActiveWorkbook.Saved = True ' было True
            ' изменения не сохранять
            ExcelSmallReportExcel.ActiveWindow.Close(SaveChanges:=False) ' было False
            ExcelSmallReportExcel.Quit()
        Catch ex As Exception
            MessageBox.Show(ex.ToString, "Работа с листом Excel", MessageBoxButtons.OK, MessageBoxIcon.Error)
            ForceExcelToQuit(ExcelSmallReportExcel)
        Finally
            ExcelSmallReportExcel = Nothing
        End Try
    End Sub

#Region "РаботаНаЛистеПротоколПоля"
    ''' <summary>
    ''' печать
    ''' </summary>
    ''' <param name="refExcelSmallReportExcel"></param>
    Private Sub WorkOnExcelWorksheets(ByRef refExcelSmallReportExcel As Excel.Application, isPrint As Boolean)
        With refExcelSmallReportExcel
            .Worksheets(ПРОТОКОЛ_ПОЛЯ).Activate()
            '    ActiveCell.FormulaR1C1 = "125.555"

            .Range("B2").Select()
            .ActiveCell.Value = НомерСтенда.ToString

            .Range("B3").Select()
            .ActiveCell.Value = НомерИзделия.ToString

            .Range("D3").Select()
            .ActiveCell.Value = String.Format("{0} {1}", ДатаЗаписи, ВремяЗаписи)

            .Range("B6").Select()
            .ActiveCell.Value = CSng(gMainFomMdiParent.varТемпературныеПоля.TextAlphaИтог.Text)

            .Range("B7").Select()
            .ActiveCell.Value = CSng(gMainFomMdiParent.varТемпературныеПоля.TextLamdaИтог.Text)

            '.Range("B8").Select()
            '.ActiveCell.Value = CSng(txtПотериПолнДавленияИтегрИтог.Text)

            .Range("B21").Select()
            .ActiveCell.Value = CSng(gMainFomMdiParent.varТемпературныеПоля.TextТсредняя.Text)

            .Range("B22").Select()
            .ActiveCell.Value = CSng(gMainFomMdiParent.varТемпературныеПоля.TextТинтегральная.Text)

            .Range("B23").Select()
            .ActiveCell.Value = CSng(gMainFomMdiParent.varТемпературныеПоля.TextТг_расчетИтог.Text)

            .Range("B24").Select()
            .ActiveCell.Value = CSng(gMainFomMdiParent.varТемпературныеПоля.TextКачествоИтог.Text)

            '.Range("B10").Select()
            '.ActiveCell.Value = arrMinMaxПоясов(1).dblСредняя 'txtBoxТемпература1.Text
            '.Range("B11").Select()
            '.ActiveCell.Value = arrMinMaxПоясов(2).dblСредняя ' txtBoxТемпература2.Text

            Dim nStartRow As Integer = 9

            For I As Integer = 1 To ЧИСЛО_ТЕРМОПАР
                .Range("B" & (I + nStartRow).ToString).Select()
                .ActiveCell.Value = arrMinMaxПоясов(I).dblСредняя
            Next

            .Range("E6").Select()
            .ActiveCell.Value = CSng(gMainFomMdiParent.varТемпературныеПоля.TextT4min.Text)

            .Range("E7").Select()
            .ActiveCell.Value = gMainFomMdiParent.varТемпературныеПоля.TextNКамерыМин.Text

            .Range("E8").Select()
            .ActiveCell.Value = gMainFomMdiParent.varТемпературныеПоля.TextNПоясаМин.Text

            .Range("E9").Select()
            .ActiveCell.Value = CSng(gMainFomMdiParent.varТемпературныеПоля.TextT4max.Text)

            .Range("E10").Select()
            .ActiveCell.Value = gMainFomMdiParent.varТемпературныеПоля.TextNКамерыМакс.Text

            .Range("E11").Select()
            .ActiveCell.Value = gMainFomMdiParent.varТемпературныеПоля.TextNПоясаМакс.Text

            .Worksheets("Данные2").Activate()
            '' поле по мерному сечению
            '.Range("C4").Select()
            '.ActiveCell.Value = arrMinMaxПоясов(1).Min

            '.Range("D4").Select()
            '.ActiveCell.Value = arrMinMaxПоясов(1).dblСредняя

            '.Range("E4").Select()
            '.ActiveCell.Value = arrMinMaxПоясов(1).Max
            nStartRow = 3

            With .Worksheets("Данные2")
                ' поле по поясам            objSheet.Cells(nStartRow - 1, nStartCol + nCol) = dt.Columns(nCol).Caption.Replace("arr", "")
                For I As Integer = 1 To ЧИСЛО_ТЕРМОПАР
                    .Cells(I + nStartRow, 3).Value = arrMinMaxПоясов(I).Min
                    .Cells(I + nStartRow, 4).Value = arrMinMaxПоясов(I).dblСредняя
                    .Cells(I + nStartRow, 5).Value = arrMinMaxПоясов(I).Max
                Next I
            End With

            ' вернуться на 1 лист
            .Worksheets(ПРОТОКОЛ_ПОЛЯ).Activate()
        End With

        If isPrint Then PrintSheets(refExcelSmallReportExcel, {ПРОТОКОЛ_ПОЛЯ, ГРАФИКИ, ПОЛЕ_ПО_ПОЯСАМ})
    End Sub
#End Region

    ''' <summary>
    ''' Печать Листов
    ''' </summary>
    ''' <param name="refExcelSmallReportExcel"></param>
    ''' <param name="worksheets"></param>
    Private Sub PrintSheets(ByRef refExcelSmallReportExcel As Excel.Application, ByVal worksheets() As String)
        Dim printerName As String
        Dim dlg As New PrintDialog
        Dim pd As Printing.PrintDocument = New Printing.PrintDocument

        dlg.Document = pd

        If dlg.ShowDialog() = Windows.Forms.DialogResult.OK Then
            printerName = dlg.PrinterSettings.PrinterName ' "\\PENTIUM4\HP DeskJet 1220C (Ne01:)"

            If dlg.PrinterSettings.IsValid Then
                Try
                    With refExcelSmallReportExcel
                        For Each itemWorksheet As String In worksheets
                            If itemWorksheet <> "ПолеПоПоясам" Then .Worksheets(itemWorksheet).Activate()

                            .Sheets(itemWorksheet).Select()
                            .ActiveWindow.SelectedSheets.PrintOut(Copies:=1, ActivePrinter:=printerName, Collate:=True)
                            Application.DoEvents()
                        Next

                        '1. Thread.Sleep(30000) 'Sleep(10000)
                        'Application.DoEvents()
                        'MessageBox.Show("Для продолжения нажмите любую клавишу", "Печать", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1, MessageBoxOptions.ServiceNotification)
                    End With
                Catch ex As Exception
                    MessageBox.Show(ex.ToString, "Печать", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                    If MessageBox.Show("Повторить?", "Печать", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) = Windows.Forms.DialogResult.No Then
                        Exit Sub
                    Else
                        PrintSheets(refExcelSmallReportExcel, worksheets)
                    End If
                End Try
            Else
                MessageBox.Show("Принтер не установлен.", "Печать", MessageBoxButtons.OK, MessageBoxIcon.Information)
            End If
        End If
    End Sub

    ''' <summary>
    ''' Заполнить Сетку Результатом
    ''' Создать DataTable в DataGridView. Вначале генерируются значения для показа и вставки их в DataTable.
    ''' Затем DataTable связывается с DataGridView и заполняет по колонкам
    ''' </summary>
    Public Sub PopulateDataGridViewReport()
        Dim myDataRow As DataRow
        'Dim myTime As Long
        '' Использовать new StopWatch для измерения времени 
        'Dim myWatch As New Stopwatch()
        'Dim myCommon As New Common()
        'clearLabels()
        'dataViewShowDataSetButton.Text = "Processing..."
        'dataViewShowDataSetButton.Enabled = False

        Try
            ' создать таблицу
            CreateMyDataViewTable(gMainDataset)
            '' запуск измерения
            'myWatch.Start()

            ' заморозить сектку в FirstName колонке, таким образом эта колонка будет слево
            ' зафиксированна при горизонтальном скроллировании
            DataGridViewReport.DataSource = gMainDataset.Tables(MY_DATA_VIEW_TABLE)
            DataGridViewReport.Columns(COLUMN_KT).Frozen = True

            Try
                For I As Integer = 0 To ЧИСЛО_ПРОМЕЖУТКОВ
                    myDataRow = gMainDataset.Tables(MY_DATA_VIEW_TABLE).NewRow()
                    myDataRow(COLUMN_KT) = I 'i + 1

                    For Each mcol As DataGridViewColumn In DataGridViewReport.Columns
                        If mcol.Name = COLUMN_KT Then Continue For

                        myDataRow(mcol.Name) = ПараметрыПоляНакопленные(mcol.Name)(I)
                        ' убрать первые символы "arr"
                        'myDataSet.Tables(MyDataViewTable).Columns(Замер.Name).Caption = Замер.Name.Replace("arr", "")
                        mcol.HeaderText = mcol.Name ' mcol.HeaderText = mcol.Name.Replace("arr", "")
                        'mcol.MinimumWidth = 100
                        mcol.DefaultCellStyle.Format = "####0.0###"
                    Next

                    gMainDataset.Tables(MY_DATA_VIEW_TABLE).Rows.Add(myDataRow)
                Next

                ' добавить итог
                myDataRow = gMainDataset.Tables(MY_DATA_VIEW_TABLE).NewRow()
                myDataRow(COLUMN_KT) = "Итого:"

                For Each mcol As DataGridViewColumn In DataGridViewReport.Columns
                    If mcol.Name = COLUMN_KT Then Continue For
                    myDataRow(mcol.Name) = ПараметрыПоляНакопленные(mcol.Name).Mean
                Next

                gMainDataset.Tables(MY_DATA_VIEW_TABLE).Rows.Add(myDataRow)
            Catch ex As Exception
                MessageBox.Show(ex.Message, "Добавление значения солбца в строку")
            End Try

            '' показать прошедшее время milliseconds потому, что данные слишком малы
            'myWatch.Stop()
            'myTime = myWatch.ElapsedMilliseconds
            'rowsReturnedTimeLabel.Text = "Прошло времени: " & myTime.ToString() & " ms"

            '' проверить номера строк в таблице
            'rowsReturnedLabel.Text = "Строк в таблице: " & myMainDataset.Tables(MyDataViewTable).Rows.Count
        Catch ex As Exception
            MessageBox.Show(ex.ToString, $"Процедура <{NameOf(PopulateDataGridViewReport)}>", MessageBoxButtons.OK, MessageBoxIcon.Warning)
        Finally
            MenuSaveProtocolAsXLS.Enabled = True
            MenuPrintProtocol.Enabled = True
        End Try
    End Sub

    Private Function StartExcel(Optional ByVal IsVisible As Boolean = True) As Excel.Application
        Return New Excel.Application() With {.Visible = IsVisible}
    End Function

    Private Sub ForceExcelToQuit(ByVal objExcel As Excel.Application)
        Try
            objExcel.Quit()
        Catch ex As Exception
        End Try
    End Sub

    Private Sub DataTableToExcelSheet(ByRef dt As DataTable, ByVal objSheet As Excel.Worksheet, ByVal nStartRow As Integer, ByVal nStartCol As Integer)
        Dim nRow As Integer, nCol As Integer

        For nCol = 0 To dt.Columns.Count - 1
            objSheet.Cells(nStartRow - 1, nStartCol + nCol) = dt.Columns(nCol).Caption 'dt.Columns(nCol).Caption.Replace("arr", "")
        Next nCol

        For nRow = 0 To dt.Rows.Count - 1
            For nCol = 0 To dt.Columns.Count - 1
                objSheet.Cells(nStartRow + nRow, nStartCol + nCol) = dt.Rows(nRow).Item(nCol)
            Next nCol
        Next nRow
    End Sub

    ''' <summary>
    ''' Сохранить Excel Workbook.
    ''' Запуск задачи асинхронно.
    ''' </summary>
    ''' <returns></returns>
    Private Function SaveProtocolAsXLSAsyncTask(ByVal saveFilename As String, ByVal isVisible As Boolean) As Tasks.Task
        Return Tasks.Task.Run(Sub()
                                  SaveProtocolAsXLSTask(saveFilename, isVisible)
                              End Sub)
    End Function

    ''' <summary>
    ''' Сохранить Excel Workbook.
    ''' Задача в собственном потоке.
    ''' </summary>
    ''' <param name="saveFilename"></param>
    ''' <param name="isVisible"></param>
    Private Sub SaveProtocolAsXLSTask(ByVal saveFilename As String, ByVal isVisible As Boolean)
        'Dim ds As New OrderSchema ' сначало определить схему XSD с именем Order, которая пойдет при заполнении таблицы
        'Dim dt As OrderSchema.OrderDataTable
        Dim objWorkbook As Excel.Workbook
        Dim objSheet As Excel.Worksheet

        ' загрузить данные из XML файла в базу данных
        'ds.ReadXml(My.Application.Info.DirectoryPath & "\OrderData.xml")
        ' можно заполнить ds из DataAdaptor
        Dim dt As DataTable = gMainDataset.Tables(MY_DATA_VIEW_TABLE)
        ' эдесь может сразу считываться таблица DataTable и класться в нужное место листа

        ' запустьть Excel и создать новый workbook из шаблона
        Dim objExcel As Excel.Application = StartExcel(isVisible)

        Try
            objWorkbook = objExcel.Workbooks.Add(pathSmallReportExcel)
            objSheet = objWorkbook.Sheets("Данные")

            objExcel.Visible = False ' заполнение очень долго
            objExcel.ScreenUpdating = False
            WorkOnExcelWorksheets(objExcel, False)
            ' вставить DataTable внутрь Excel рабочей книги
            DataTableToExcelSheet(dt, objSheet, 2, 1)
            objExcel.ScreenUpdating = True
            objExcel.Visible = True

            ' если Visible, значит завершение, которое может увидеть пользователь, иначе запись и выход
            If isVisible = False Then
                objWorkbook.SaveAs(Filename:=saveFilename, FileFormat:=Excel.XlFileFormat.xlOpenXMLWorkbook, CreateBackup:=False)
                objWorkbook.Close(False)
                objExcel.Quit()
            End If
        Catch ex As Exception
            If isVisible Then MsgBox(ex.ToString, MsgBoxStyle.Exclamation, "Ошибка заполнения книги")
            ForceExcelToQuit(objExcel)
        End Try
    End Sub

    ''' <summary>
    ''' Создание таблицы используя DataView.
    ''' Здесь проверка существования таблицы. Если она существует, то очистить данные.
    ''' </summary>
    ''' <param name="myDataSet"></param>
    ''' <returns></returns>
    Private Function CreateMyDataViewTable(ByVal myDataSet As DataSet) As DataSet
        For I As Integer = 0 To myDataSet.Tables.Count - 1
            Try
                If myDataSet.Tables(I).TableName = MY_DATA_VIEW_TABLE Then
                    ' удалить таблицу и сбросить к оригинальному значению
                    myDataSet.Tables.Remove(MY_DATA_VIEW_TABLE)
                    myDataSet.Clear()
                    Exit For
                End If
            Catch ex As Exception
                MessageBox.Show(ex.ToString, $"Процедура <{NameOf(CreateMyDataViewTable)}>", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            End Try
        Next

        myDataSet.Tables.Add(MY_DATA_VIEW_TABLE)
        myDataSet.Tables(MY_DATA_VIEW_TABLE).Columns.Add(COLUMN_KT, Type.GetType("System.String"))

        'For Each Замер As ЗамерыПараметраПоля In ПараметрыПоляНакопленные.ВсеВеличиныЗамеровПараметров.Values
        '    If blnИзмерениеПоТемпературам Then
        '        If Замер.ТипИзмерения = enuТипИзмерения.ИзмерениеВезде OrElse Замер.ТипИзмерения = enuТипИзмерения.ИзмерениеПоТемпературам Then
        '            myDataSet.Tables(MyDataViewTable).Columns.Add(Замер.Name, Type.GetType("System.Double"))

        '            'Dim col As New DataGridViewTextBoxColumn()
        '            'col.DataPropertyName = "EmployeeID"
        '            'col.HeaderText = "Employee ID"
        '            'col.Name = "EmployeeID"
        '            'col.ReadOnly = True
        '            '' Do not display this system internal number to the user.
        '            'col.Visible = False
        '            'dataGridView1.Columns.Add(colEmployeeId)
        '        End If
        '    Else
        '        If Замер.ТипИзмерения = enuТипИзмерения.ИзмерениеВезде OrElse Замер.ТипИзмерения = enuТипИзмерения.ИзмерениеПоПотерямДавления Then
        '            myDataSet.Tables(MyDataViewTable).Columns.Add(Замер.Name, Type.GetType("System.Double"))
        '        End If
        '    End If
        'Next

        'If blnИзмерениеПоТемпературам Then
        'Dim ЗамерыДляТипаИспытаний As Object = From Замеры In ПараметрыПоляНакопленные.ВсеВеличиныЗамеровПараметров.Values
        '                                       Where Замеры.ТипИзмерения = enuТипИзмерения.ИзмерениеВезде OrElse Замеры.ТипИзмерения = enuТипИзмерения.ИзмерениеПоТемпературам
        '                                       Select Замеры
        Dim ЗамерыДляТипаИспытаний = From Замеры In ПараметрыПоляНакопленные.ВсеВеличиныЗамеровПараметров.Values
                                     Where Замеры.ТипИзмерения = ModelMeasurement.ИзмерениеВезде OrElse Замеры.ТипИзмерения = ModelMeasurement.ИзмерениеПоТемпературам
                                     Select Замеры
        'Else
        '    ЗамерыДляТипаИспытаний = From Замеры In ПараметрыПоляНакопленные.ВсеВеличиныЗамеровПараметров.Values _
        '                      Where Замеры.ТипИзмерения = enuТипИзмерения.ИзмерениеВезде OrElse Замеры.ТипИзмерения = enuТипИзмерения.ИзмерениеПоПотерямДавления _
        '                      Select Замеры
        'End If

        For Each замер In ЗамерыДляТипаИспытаний
            'myDataSet.Tables("MyDataViewTable").Columns("ID").Unique = True
            myDataSet.Tables(MY_DATA_VIEW_TABLE).Columns.Add(замер.Name, Type.GetType("System.Double"))
        Next

        Return myDataSet
    End Function
#End Region
End Class


'''' <summary>
'''' Берётся существующий DataTable и добавляется к показу только интересующие колонки
'''' DefaultView.ToTable создает новую таблицу что через DataGridView связанна с сеткой
'''' </summary>
'''' <param name="sender"></param>
'''' <param name="e"></param>
'Private Sub dataViewShowDataViewButton_Click(ByVal sender As Object, ByVal e As EventArgs)
'    ' выбрать необходимые столбцы
'    Dim columns() As String = {"ID", "IntegerValue2"}
'    'clearLabels()
'    'dataViewShowDataViewButton.Text = "Processing..."
'    'dataViewShowDataViewButton.Enabled = False
'    Dim myWatch As New Stopwatch() ' Использовать new StopWatch для измерения времени

'    Try
'        myWatch.Start()
'        If gMainDataset.Tables(MY_DATA_VIEW_TABLE).Rows.Count > 0 Then
'            ' Создать новую таблицу доступную из DefaultView.ToTable метода, в который положить колонки, необходимые для вывода
'            Dim myDataTable As DataTable = gMainDataset.Tables(MY_DATA_VIEW_TABLE).DefaultView.ToTable("MySmallTable", True, columns)

'            ' связать новую DataTable с DataGridView показав только две колонки
'            grdOrder.DataSource = myDataTable
'            ' остановить StopWatch
'            myWatch.Stop()

'            ' Показать результат
'            'rowsReturnedLabel.Text = "Строк в таблице: " & myDataTable.Rows.Count
'            'rowsReturnedTimeLabel.Text = "Прошло времени: " & myWatch.ElapsedMilliseconds.ToString() + " ms"
'        Else
'            MessageBox.Show("Заполнить gMainDataset вначале!")
'        End If
'    Catch ex As Exception
'        MessageBox.Show(ex.ToString, "Процедура <ЗаполнитьСеткуРезультатом>", MessageBoxButtons.OK, MessageBoxIcon.Warning)
'    End Try

'    'dataViewShowDataViewButton.Text = "DataView"
'    'dataViewShowDataViewButton.Enabled = True
'End Sub

'Private Sub clearLabels()
'    'rowsInsertedLabel.Text = ""
'    'elapsedTimeLabel.Text = ""
'    'rowsReadWrittenLabel.Text = ""
'    'readWriteTimeLabel.Text = ""
'    rowsReturnedLabel.Text = ""
'    rowsReturnedTimeLabel.Text = ""
'    grdOrder.DataSource = ""
'    'resultsDataGridView.DataSource = ""
'    'xmlResultsDataGridView.DataSource = ""
'End Sub

'Private Sub cmdLoadSpreadsheet_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
'    Dim dt As DataTable
'    dt = ПерелатьExcelWorkbook("", True)
'    ' можно записать в базу а затем считать в грид
'    Me.grdOrder.DataSource = dt
'End Sub


'''' <summary>
'''' Ввод Имени Файла Excel
'''' </summary>
'''' <param name="sTitle"></param>
'Private Sub ВводИмениФайлаExcel(ByVal sTitle As String)
'    With SaveFileDialogProtocol
'        .FileName = vbNullString
'        .Title = sTitle
'        .InitialDirectory = "D:\"
'        '.CheckFileExists = True
'        .DefaultExt = ".xlsx"
'        'установить флаг атрибутов
'        .Filter = "Книга Excel (*.xlsx)|*.xlsx"
'        .RestoreDirectory = True

'        If .ShowDialog() = Windows.Forms.DialogResult.OK AndAlso Len(.FileName) <> 0 Then
'            gПутьExcel = .FileName
'        End If
'    End With
'End Sub

#Region "РаботаНаЛистеМалыйГаз"
'    Private Sub РаботаНаЛистеМалыйГаз(ByRef refExcelSmallReportExcel4 As Excel.Application)
'        With refExcelSmallReportExcel4
'            .Worksheets(scМалыйГаз).Activate()
'            .Range("C2").Select()
'            .ActiveCell.Value = strНомерСтенда
'            .Range("C3").Select()
'            .ActiveCell.Value = lngНомерИзделия.ToString
'            .Range("C9").Select()
'            .ActiveCell.Value = Val(TextBox1Обороты.Text)
'            .Range("C10").Select()
'            .ActiveCell.Value = Val(TextBox1Барометр.Text)
'            .Range("C11").Select()
'            .ActiveCell.Value = Val(TextBox1ТБокса.Text)
'            .Range("C12").Select()
'            .ActiveCell.Value = Val(TextBox1ТягаГап.Text)

'            .Range("C13").Select()
'            If OptionButton1Навеска1.Checked Then
'                .ActiveCell.Value = "1"
'            ElseIf OptionButton1Навеска2.Checked Then
'                .ActiveCell.Value = "2"
'            ElseIf OptionButton1Навеска3.Checked Then
'                .ActiveCell.Value = "3"
'            End If

'            .Range("C14").Select()
'            .ActiveCell.Value = Val(TextBox1Навеска.Text)
'            .Range("C15").Select()
'            .ActiveCell.Value = Val(TextBox1Время.Text)
'            .Range("C16").Select()
'            .ActiveCell.Value = Val(TextBox1Потери1.Text)
'            .Range("C17").Select()
'            .ActiveCell.Value = Val(TextBox1Потери2.Text)
'            .Range("C18").Select()
'            .ActiveCell.Value = Val(TextBox1Потери3.Text)
'            .Range("C19").Select()
'            .ActiveCell.Value = Val(TextBox1Потери4.Text)
'            .Range("F9").Select()
'            .ActiveCell.Value = Val(TextBox1ТягаПриборная.Text)

'            .Range("F10").Select()
'            If OptionButton923Есть.Checked Then
'                .ActiveCell.Value = "есть"
'            Else
'                .ActiveCell.Value = "нет"
'            End If

'            .Range("C24").Select()
'            .ActiveCell.Value = Val(TextBox1ОборотыМалыйГаз.Text)
'            .Range("C25").Select()
'            .ActiveCell.Value = Val(TextBox1ТягаМалыйГаз.Text)
'            .Range("C26").Select()
'            .ActiveCell.Value = Val(TextBox1Gt.Text)
'        End With
'    End Sub
'#End Region

#End Region
