Imports System.Windows.Forms
Imports System.Data.OleDb
Imports System.ComponentModel
Imports System.Threading
Imports System.Drawing

Public Class FormInputGeometry
    ''' <summary>
    ''' Гребёнка
    ''' </summary>
    Private Enum Comb
        CombA
        CombB
    End Enum

    ' сразу после ввода запись в базу тех значений которые введены
    ' и считывание диаметров стенда
    Private ЧИСЛО_ТЕРМОПАР_mod2 As Integer = ЧИСЛО_ТЕРМОПАР \ 2
    Private arrТекущиеКоординаты(ЧИСЛО_ТЕРМОПАР) As Double
    Private keyTablePhase As String
    Private newID As Integer
    Private cn As OleDbConnection
    Private isExperimental As Boolean ' эксперементальная камера
    Private regExTextBoxList As New List(Of Control) ' содержит контролы для проверки на число
    Private TextOrdinateThermopairs() As BaseForm.DoubleTextBox
    ''' <summary>
    ''' изначальный размер формы
    ''' </summary>
    Private modelForScalling As Double '= Math.Sqrt(753 * 547) 
    ''' <summary>
    ''' отношение гипотенуз размеров окна и разрешения большого экрана
    ''' </summary>
    Private hypotenuseK As Double
    ''' <summary>
    ''' коэф. увеличения размера шрифта при значении hypotenuseK
    ''' </summary>
    Private Const sizeFontForHypotenuseK As Double = 1.3
    ''' <summary>
    ''' размер шрифтов по умолчанию
    ''' </summary>
    Private Const fontSizeDesign As Single = 8.25
    ''' <summary>
    ''' увеличение гипотенузы текущего размера к изначальному
    ''' </summary>
    Private scalling As Double
    ''' <summary>
    ''' итоговое увеличение размера шрифта
    ''' </summary>
    Private decFontSize As Double

    Public Sub New()
        ' Этот вызов является обязательным для конструктора.
        InitializeComponent()

        ' Добавить код инициализации после вызова InitializeComponent().
        modelForScalling = Math.Sqrt(Me.Height * Me.Width)
        hypotenuseK = Math.Sqrt(1920 * 1080) / modelForScalling
    End Sub

    Private Sub InputGeometryForm_Load(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Load
        TextOrdinateThermopairs = {TextOrdinateThermopair0,
                                        TextOrdinateThermopair1,
                                        TextOrdinateThermopair2,
                                        TextOrdinateThermopair3,
                                        TextOrdinateThermopair4,
                                        TextOrdinateThermopair5,
                                        TextOrdinateThermopair6,
                                        TextOrdinateThermopair7,
                                        TextOrdinateThermopair8,
                                        TextOrdinateThermopair9}

        regExTextBoxList.AddRange({TextOrdinateThermopair0,
                                  TextOrdinateThermopair1,
                                  TextOrdinateThermopair2,
                                  TextOrdinateThermopair3,
                                  TextOrdinateThermopair4,
                                  TextOrdinateThermopair5,
                                  TextOrdinateThermopair6,
                                  TextOrdinateThermopair7,
                                  TextOrdinateThermopair8,
                                  TextOrdinateThermopair9,
                                  TextNumberProduct,
                                  TextNumberBodyChamber,
                                  TextNumberPipe,
                                  TextNumberColector,
                                  TextBarometer,
                                  TextNumberCombA,
                                  TextNumberCombB,
                                  TextC_0,
                                  TextL_0,
                                  TextD_0,
                                  TextZ1_0,
                                  TextZ2_0,
                                  TextZ3_0,
                                  TextZ4_0,
                                  TextC_1,
                                  TextL_1,
                                  TextD_1,
                                  TextZ1_1,
                                  TextZ2_1,
                                  TextZ3_1,
                                  TextZ4_1,
                                  TextFdiffuse,
                                  TextWidthMeasuredRegion,
                                  TextDoubleLeadAngle
                                  })
    End Sub

    Private Sub InputGeometryForm_Closing(sender As Object, e As CancelEventArgs) Handles Me.Closing
        If Not gMainFomMdiParent.IsWindowClosed Then
            e.Cancel = True
        End If
    End Sub

    Private Sub ButtonApply_Click(ByVal eventSender As Object, ByVal eventArgs As EventArgs) Handles ButtonApply.Click
        SaveAllSetting()
    End Sub

    Private Sub ButtonCheckGeometry_Click(ByVal eventSender As Object, ByVal eventArgs As EventArgs) Handles ButtonCheckGeometry.Click
        CheckGeometry()
    End Sub

    ''' <summary>
    ''' Процедура события обновления OnRowUpdated
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="args"></param>
    Private Sub OnRowUpdated(ByVal sender As Object, ByVal args As OleDbRowUpdatedEventArgs)
        ' Вкдючает в себя переменную и команду возвращающий индентификатор значения
        ' из Access базы данных.
        Dim idCMD As OleDbCommand = New OleDbCommand("SELECT @@IDENTITY", cn)

        If args.StatementType = StatementType.Insert Then
            ' возвратить значение идентификатора из колонки.
            newID = CInt(idCMD.ExecuteScalar())
            args.Row(keyTablePhase) = newID
        End If
    End Sub

    ''' <summary>
    ''' Запись настоечных параметров в базу "Камера"
    ''' и применение их к текущим испытаниям.
    ''' </summary>
    Private Sub SaveAllSetting()
        Dim I As Integer
        Dim strSQL As String
        Dim odaDataAdapter As OleDbDataAdapter
        Dim dtDataTable As New DataTable
        Dim drDataRow As DataRow
        Dim cb As OleDbCommandBuilder

        If ПроверкаКорректногоВвода() = False Then Exit Sub

        Try
            cn = New OleDbConnection(BuildCnnStr(PROVIDER_JET, gПутьКамера))

            ' гребенка А
            strSQL = "SELECT * FROM [ГребенкаА]"

            cn.Open()
            odaDataAdapter = New OleDbDataAdapter(strSQL, cn)
            odaDataAdapter.Fill(dtDataTable)
            drDataRow = dtDataTable.NewRow
            drDataRow.BeginEdit()
            drDataRow("НомерГребенкиА") = CInt(TextNumberCombA.Text)
            drDataRow("c") = CSng(TextC_0.Text)
            drDataRow("L") = CSng(TextL_0.Text)
            drDataRow("D") = CSng(TextD_0.Text)
            drDataRow("Z1") = CSng(TextZ1_0.Text)
            drDataRow("Z2") = CSng(TextZ2_0.Text)
            drDataRow("Z3") = CSng(TextZ3_0.Text)
            drDataRow("Z4") = CSng(TextZ4_0.Text)
            drDataRow.EndEdit()

            keyTablePhase = "КодГребенкиА" ' для события
            dtDataTable.Rows.Add(drDataRow)
            ' включить событие вставки Автонумерованного значения
            AddHandler odaDataAdapter.RowUpdated,
              New OleDbRowUpdatedEventHandler(AddressOf OnRowUpdated)

            cb = New OleDbCommandBuilder(odaDataAdapter)
            odaDataAdapter.Update(dtDataTable)
            gКодГребенкиА = newID

            'гребенка Б
            dtDataTable = New DataTable
            strSQL = "SELECT * FROM [ГребенкаБ]"
            odaDataAdapter = New OleDbDataAdapter(strSQL, cn)
            odaDataAdapter.Fill(dtDataTable)
            drDataRow = dtDataTable.NewRow
            drDataRow.BeginEdit()
            drDataRow("НомерГребенкиБ") = CInt(TextNumberCombB.Text)
            drDataRow("c") = CSng(TextC_1.Text)
            drDataRow("L") = CSng(TextL_1.Text)
            drDataRow("D") = CSng(TextD_1.Text)
            drDataRow("Z1") = CSng(TextZ1_1.Text)
            drDataRow("Z2") = CSng(TextZ2_1.Text)
            drDataRow("Z3") = CSng(TextZ3_1.Text)
            drDataRow("Z4") = CSng(TextZ4_1.Text)
            drDataRow.EndEdit()

            keyTablePhase = "КодГребенкиБ" ' для события
            dtDataTable.Rows.Add(drDataRow)
            ' включить событие вставки Автонумерованного значения
            AddHandler odaDataAdapter.RowUpdated, New OleDbRowUpdatedEventHandler(AddressOf OnRowUpdated)

            cb = New OleDbCommandBuilder(odaDataAdapter)
            odaDataAdapter.Update(dtDataTable)
            gКодГребенкиБ = newID

            ' изделие
            dtDataTable = New DataTable
            strSQL = "SELECT * FROM [Изделие]"
            odaDataAdapter = New OleDbDataAdapter(strSQL, cn)
            odaDataAdapter.Fill(dtDataTable)
            drDataRow = dtDataTable.NewRow
            drDataRow.BeginEdit()
            НомерСтенда = CInt(ComboBoxNumberStend.Text)
            drDataRow("НомерСтенда") = НомерСтенда
            drDataRow("НомерИзделия") = CInt(TextNumberProduct.Text)
            drDataRow("Дата") = DataTimePickerTest.Value
            drDataRow("КодГребенкиА") = gКодГребенкиА
            drDataRow("КодГребенкиБ") = gКодГребенкиБ
            drDataRow("ПрогрИспытания") = CInt(ComboBoxTestProgram.Text)
            drDataRow("НомерКорпусаКамеры") = CInt(TextNumberBodyChamber.Text)
            drDataRow("НомерЖаровойТрубы") = CInt(TextNumberPipe.Text)
            drDataRow("НомерКоллектора") = CInt(TextNumberColector.Text)
            drDataRow("Барометр") = CSng(TextBarometer.Text)
            drDataRow.EndEdit()

            keyTablePhase = "КодИзделия" ' для события
            dtDataTable.Rows.Add(drDataRow)
            ' включить событие вставки Автонумерованного значения
            AddHandler odaDataAdapter.RowUpdated, New OleDbRowUpdatedEventHandler(AddressOf OnRowUpdated)

            cb = New OleDbCommandBuilder(odaDataAdapter)
            odaDataAdapter.Update(dtDataTable)
            gКодИзделия = newID
            strSQL = "SELECT * FROM [Стенд] WHERE [НомерСтенда] = " & НомерСтенда.ToString

            '' считать конфигурацию стенда
            'Dim rdr As OleDbDataReader
            'Dim cmd As OleDbCommand = cn.CreateCommand
            'cmd.CommandType = CommandType.Text

            'cmd.CommandText = strSQL
            'rdr = cmd.ExecuteReader

            'If rdr.Read Then
            '    D20отвОсн = rdr("D20отвОсн")
            '    D20трубОсн = rdr("D20трубОсн")
            '    Ks = rdr("Ks")
            '    КоэфЛинейногоТепловогоРасширенияМерногоСопла = rdr("Ktосн")

            '    D20отвОтб = rdr("D20отвОтб")
            '    D20трубОтб = rdr("D20трубОтб")
            '    KsОтбН = rdr("KsОтбН")
            '    КоэфЛинейногоТепловогоРасширенияТрубопровода = rdr("Ktотбн")

            '    D20отвОтбВн = rdr("D20отвОтбВн")
            '    D20трубОтбВн = rdr("D20трубОтбВн")
            '    KsОтбВн = rdr("KsОтбВн")
            '    КоэфЛинейногоТепловогоРасширенияТрубопроводаВн = rdr("Ktотбвн")

            '    If isExperimental Then
            '        Fdif = CDbl(TextFdiffuse.Text)
            '        ШиринаМерногоУчастка = CDbl(TextWidthMeasuredRegion.Text)
            '    Else
            '        Fdif = rdr("Fdif")
            '        ШиринаМерногоУчастка = ШИРИНА
            '    End If
            'End If
            'rdr.Close()

            ' переписать в таблицу 'Стенд' изменённые настроечные хначения
            dtDataTable = New DataTable
            odaDataAdapter = New OleDbDataAdapter(strSQL, cn)
            odaDataAdapter.Fill(dtDataTable)
            If dtDataTable.Rows.Count <> 0 Then
                With gMainFomMdiParent.Manager.TuningDataTable
                    If isExperimental Then ' введенные пользователем
                        Fdif = CDbl(TextFdiffuse.Text)
                        ШиринаМерногоУчастка = CDbl(TextWidthMeasuredRegion.Text)
                    Else
                        Fdif = .FindByИмяПараметра(TuningParameters.conFdif).ЦифровоеЗначение
                        ШиринаМерногоУчастка = ШИРИНА
                    End If

                    dtDataTable.Rows(0)(TuningParameters.conFdif) = Fdif
                    ' такой сторки в таблице 'Стенд' нет - в расчёте зашиты константы.  dtDataTable.Rows(0)(TuningParameters.ШИРИНА_МЕРНОГО_УЧАСТКА) = ШИРИНА

                    dtDataTable.Rows(0)(TuningParameters.conD20трубОсн) = .FindByИмяПараметра(TuningParameters.conD20трубОсн).ЦифровоеЗначение
                    dtDataTable.Rows(0)(TuningParameters.conD20отвОсн) = .FindByИмяПараметра(TuningParameters.conD20отвОсн).ЦифровоеЗначение
                    dtDataTable.Rows(0)(TuningParameters.conKs) = .FindByИмяПараметра(TuningParameters.conKs).ЦифровоеЗначение
                    dtDataTable.Rows(0)(TuningParameters.conKtосн) = .FindByИмяПараметра(TuningParameters.conKtосн).ЦифровоеЗначение

                    dtDataTable.Rows(0)(TuningParameters.conD20отвОтб) = .FindByИмяПараметра(TuningParameters.conD20отвОтб).ЦифровоеЗначение
                    dtDataTable.Rows(0)(TuningParameters.conD20трубОтб) = .FindByИмяПараметра(TuningParameters.conD20трубОтб).ЦифровоеЗначение
                    dtDataTable.Rows(0)(TuningParameters.conKsОтбН) = .FindByИмяПараметра(TuningParameters.conKsОтбН).ЦифровоеЗначение
                    dtDataTable.Rows(0)(TuningParameters.conKtотбн) = .FindByИмяПараметра(TuningParameters.conKtотбн).ЦифровоеЗначение

                    dtDataTable.Rows(0)(TuningParameters.conD20отвОтбВн) = .FindByИмяПараметра(TuningParameters.conD20отвОтбВн).ЦифровоеЗначение
                    dtDataTable.Rows(0)(TuningParameters.conD20трубОтбВн) = .FindByИмяПараметра(TuningParameters.conD20трубОтбВн).ЦифровоеЗначение
                    dtDataTable.Rows(0)(TuningParameters.conKsОтбВн) = .FindByИмяПараметра(TuningParameters.conKsОтбВн).ЦифровоеЗначение
                    dtDataTable.Rows(0)(TuningParameters.conKtотбвн) = .FindByИмяПараметра(TuningParameters.conKtотбвн).ЦифровоеЗначение
                End With

                cb = New OleDbCommandBuilder(odaDataAdapter)
                odaDataAdapter.Update(dtDataTable)
                Thread.Sleep(500)
            End If
            cn.Close()

            ' теперь записать в настроечную таблицу, которая используется в качестве хранения введенных данных с предыдущего испытания
            With gMainFomMdiParent.Manager.TuningDataTable
                .FindByИмяПараметра(TuningParameters.НОМЕР_ГРЕБЕНКИ_А).ЦифровоеЗначение = Double.Parse(TextNumberCombA.Text)
                .FindByИмяПараметра(TuningParameters.НОМЕР_ГРЕБЕНКИ_Б).ЦифровоеЗначение = Double.Parse(TextNumberCombB.Text)
                .FindByИмяПараметра(TuningParameters.conCa).ЦифровоеЗначение = Double.Parse(TextC_0.Text)
                .FindByИмяПараметра(TuningParameters.conLa).ЦифровоеЗначение = Double.Parse(TextL_0.Text)
                .FindByИмяПараметра(TuningParameters.conDa).ЦифровоеЗначение = Double.Parse(TextD_0.Text)
                .FindByИмяПараметра(TuningParameters.conZ1a).ЦифровоеЗначение = Double.Parse(TextZ1_0.Text)
                .FindByИмяПараметра(TuningParameters.conZ2a).ЦифровоеЗначение = Double.Parse(TextZ2_0.Text)
                .FindByИмяПараметра(TuningParameters.conZ3a).ЦифровоеЗначение = Double.Parse(TextZ3_0.Text)
                .FindByИмяПараметра(TuningParameters.conZ4a).ЦифровоеЗначение = Double.Parse(TextZ4_0.Text)
                .FindByИмяПараметра(TuningParameters.conCb).ЦифровоеЗначение = Double.Parse(TextC_1.Text)
                .FindByИмяПараметра(TuningParameters.conLb).ЦифровоеЗначение = Double.Parse(TextL_1.Text)
                .FindByИмяПараметра(TuningParameters.conDb).ЦифровоеЗначение = Double.Parse(TextD_1.Text)
                .FindByИмяПараметра(TuningParameters.conZ1b).ЦифровоеЗначение = Double.Parse(TextZ1_1.Text)
                .FindByИмяПараметра(TuningParameters.conZ2b).ЦифровоеЗначение = Double.Parse(TextZ2_1.Text)
                .FindByИмяПараметра(TuningParameters.conZ3b).ЦифровоеЗначение = Double.Parse(TextZ3_1.Text)
                .FindByИмяПараметра(TuningParameters.conZ4b).ЦифровоеЗначение = Double.Parse(TextZ4_1.Text)
                .FindByИмяПараметра(TuningParameters.УГОЛ_УПРЕЖДЕНИЯ_ТУРЕЛИ_ДО_НУЛЯ).ЦифровоеЗначение = Double.Parse(TextDoubleLeadAngle.Text)

                .FindByИмяПараметра(TuningParameters.conFdif).ЦифровоеЗначение = Double.Parse(TextFdiffuse.Text)
                .FindByИмяПараметра(TuningParameters.ШИРИНА_МЕРНОГО_УЧАСТКА).ЦифровоеЗначение = Double.Parse(TextWidthMeasuredRegion.Text)
            End With

            gMainFomMdiParent.Manager.SaveTable()
            gMainFomMdiParent.myClassCalculation.ПолучитьЗначенияНастроечныхПараметров()

            НомерИзделия = CInt(TextNumberProduct.Text)

            For I = 1 To ЧИСЛО_ТЕРМОПАР
                КоординатыТермопар(I - 1) = arrТекущиеКоординаты(I)
            Next

            gMainFomMdiParent.varТемпературныеПоля.ИнициализацияПеременных() 'для настройки 
            gMainFomMdiParent.varТемпературныеПоля.ButtonRunMeasurement.Enabled = True
            gГеометрияВведена = True
            gMainFomMdiParent.WindowManagerPanel1.SetActiveWindow(CType(gMainFomMdiParent.varТемпературныеПоля, Form))
            gMainFomMdiParent.varEncoder.GetConfigSetting()
            gMainFomMdiParent.varEncoder.LoadEncoder()
            gMainFomMdiParent.varEncoder.RunWorkTasks()
            Enabled = False
        Catch ex As Exception
            MessageBox.Show(ex.ToString, "Запись геометрии и шапки", MessageBoxButtons.OK, MessageBoxIcon.Warning)
        Finally
            If (cn.State = ConnectionState.Open) Then
                cn.Close()
            End If
        End Try
    End Sub

    ''' <summary>
    ''' Проверка введенных данных
    ''' </summary>
    Private Sub CheckGeometry()
        Dim I As Integer

        Try
            Dim arrTemp2(ЧИСЛО_ТЕРМОПАР) As Double
            Dim ordinates As Double()

            ordinates = СчитатьФрейм(Comb.CombA) 'гребенка А

            For I = 1 To ЧИСЛО_ТЕРМОПАР_mod2
                arrTemp2(I) = ordinates(I)
            Next

            ordinates = СчитатьФрейм(Comb.CombB) 'гребенка Б

            For I = 1 To ЧИСЛО_ТЕРМОПАР_mod2
                arrTemp2(I + ЧИСЛО_ТЕРМОПАР_mod2) = ordinates(I)
            Next

            For I = 2 To ЧИСЛО_ТЕРМОПАР Step 2
                arrТекущиеКоординаты(I - 1) = arrTemp2(I \ 2)
                arrТекущиеКоординаты(I) = arrTemp2(I \ 2 + ЧИСЛО_ТЕРМОПАР_mod2)
            Next

            For I = 1 To ЧИСЛО_ТЕРМОПАР
                TextOrdinateThermopairs(I - 1).Text = arrТекущиеКоординаты(I).ToString("#0.###")
            Next

            ButtonApply.Visible = ПроверкаКорректногоВвода()

            Exit Sub
        Catch ex As Exception
            MessageBox.Show(ex.ToString, "Повторно введите геометрию!", MessageBoxButtons.OK, MessageBoxIcon.Warning)
        End Try
    End Sub

    Private Function СчитатьФрейм(ByVal intNumberComb As Comb) As Double()
        Dim temp(7) As Double
        Dim ordinates(ЧИСЛО_ТЕРМОПАР_mod2) As Double

        If intNumberComb = Comb.CombA Then
            temp(1) = Double.Parse(TextC_0.Text)
            temp(2) = Double.Parse(TextL_0.Text)
            temp(3) = Double.Parse(TextD_0.Text)
            temp(4) = Double.Parse(TextZ1_0.Text)
            temp(5) = Double.Parse(TextZ2_0.Text)
            temp(6) = Double.Parse(TextZ3_0.Text)
            temp(7) = Double.Parse(TextZ4_0.Text)
        Else
            temp(1) = Double.Parse(TextC_1.Text)
            temp(2) = Double.Parse(TextL_1.Text)
            temp(3) = Double.Parse(TextD_1.Text)
            temp(4) = Double.Parse(TextZ1_1.Text)
            temp(5) = Double.Parse(TextZ2_1.Text)
            temp(6) = Double.Parse(TextZ3_1.Text)
            temp(7) = Double.Parse(TextZ4_1.Text)
        End If

        ordinates(1) = temp(1) - temp(2) + temp(3) / 2.0#

        For I As Integer = 1 To 4
            ordinates(I + 1) = ordinates(I) + temp(I + 3)
        Next

        Return ordinates
    End Function

    Public Sub Заполнить()
        ' центрирование формы
        ComboBoxNumberStend.Items.Add("16")
        ComboBoxNumberStend.Items.Add("22")
        ComboBoxNumberStend.SelectedIndex = 0 ' по умолчанию активный 16
        ComboBoxTestProgram.Items.Add("53")
        ComboBoxTestProgram.Items.Add("79")
        ComboBoxTestProgram.Items.Add("104")
        ComboBoxTestProgram.SelectedIndex = 1 ' по умолчанию активный 79
        ButtonApply.Visible = False
        DataTimePickerTest.Value = Today

        ' ввеcти данные с предыдущего испытания
        With gMainFomMdiParent.Manager.TuningDataTable
            TextNumberCombA.Text = CStr(.FindByИмяПараметра(TuningParameters.НОМЕР_ГРЕБЕНКИ_А).ЦифровоеЗначение)
            TextNumberCombB.Text = CStr(.FindByИмяПараметра(TuningParameters.НОМЕР_ГРЕБЕНКИ_Б).ЦифровоеЗначение)
            TextC_0.Text = CStr(.FindByИмяПараметра(TuningParameters.conCa).ЦифровоеЗначение)
            TextL_0.Text = CStr(.FindByИмяПараметра(TuningParameters.conLa).ЦифровоеЗначение)
            TextD_0.Text = CStr(.FindByИмяПараметра(TuningParameters.conDa).ЦифровоеЗначение)
            TextZ1_0.Text = CStr(.FindByИмяПараметра(TuningParameters.conZ1a).ЦифровоеЗначение)
            TextZ2_0.Text = CStr(.FindByИмяПараметра(TuningParameters.conZ2a).ЦифровоеЗначение)
            TextZ3_0.Text = CStr(.FindByИмяПараметра(TuningParameters.conZ3a).ЦифровоеЗначение)
            TextZ4_0.Text = CStr(.FindByИмяПараметра(TuningParameters.conZ4a).ЦифровоеЗначение)
            TextC_1.Text = CStr(.FindByИмяПараметра(TuningParameters.conCb).ЦифровоеЗначение)
            TextL_1.Text = CStr(.FindByИмяПараметра(TuningParameters.conLb).ЦифровоеЗначение)
            TextD_1.Text = CStr(.FindByИмяПараметра(TuningParameters.conDb).ЦифровоеЗначение)
            TextZ1_1.Text = CStr(.FindByИмяПараметра(TuningParameters.conZ1b).ЦифровоеЗначение)
            TextZ2_1.Text = CStr(.FindByИмяПараметра(TuningParameters.conZ2b).ЦифровоеЗначение)
            TextZ3_1.Text = CStr(.FindByИмяПараметра(TuningParameters.conZ3b).ЦифровоеЗначение)
            TextZ4_1.Text = CStr(.FindByИмяПараметра(TuningParameters.conZ4b).ЦифровоеЗначение)
            TextDoubleLeadAngle.Text = CStr(.FindByИмяПараметра(TuningParameters.УГОЛ_УПРЕЖДЕНИЯ_ТУРЕЛИ_ДО_НУЛЯ).ЦифровоеЗначение)

            TextFdiffuse.Text = CStr(.FindByИмяПараметра(TuningParameters.conFdif).ЦифровоеЗначение)
            TextWidthMeasuredRegion.Text = CStr(.FindByИмяПараметра(TuningParameters.ШИРИНА_МЕРНОГО_УЧАСТКА).ЦифровоеЗначение)
        End With
    End Sub

    Private Sub CheckBoxExperimental_CheckStateChanged(ByVal eventSender As Object, ByVal eventArgs As EventArgs) Handles CheckBoxExperimental.CheckStateChanged
        If CBool(CheckBoxExperimental.CheckState) Then
            isExperimental = True
            TextFdiffuse.BackColor = ColorTranslator.FromOle(&H80000005)
            TextWidthMeasuredRegion.BackColor = ColorTranslator.FromOle(&H80000005)
            TextFdiffuse.Enabled = True
            TextWidthMeasuredRegion.Enabled = True
        Else
            isExperimental = False
            TextFdiffuse.BackColor = ColorTranslator.FromOle(&H8000000F)
            TextWidthMeasuredRegion.BackColor = ColorTranslator.FromOle(&H8000000F)
            TextFdiffuse.Enabled = False
            TextWidthMeasuredRegion.Enabled = False
        End If

        ButtonCheckGeometry_Click(ButtonCheckGeometry, New EventArgs())
    End Sub

    Private Function ПроверкаКорректногоВвода() As Boolean
        Dim existingControl As Control
        ' хранит сообщение, что будет выводится пользователю, показывая содержить ли контрол правильный ввод.
        Dim validationMessage As String = String.Empty

        ' цикл по всем контролам формы.
        For Each existingControl In regExTextBoxList 'Controls
            ' если текущий контрол унаследован от RegExTextBox
            If TypeOf existingControl Is BaseForm.RegExTextBox Then
                ' привести к необходимому типу и после этого проверить свойство IsValid
                Dim regexControl As BaseForm.RegExTextBox = CType(existingControl, BaseForm.RegExTextBox)
                ' если текст не корректен, то добавить контрол в лист неккоректных контролов.
                If Not regexControl.IsValid Then
                    validationMessage &= $"{regexControl.Tag}:{regexControl.ErrorMessage}{Environment.NewLine}"
                End If
            End If
        Next

        ' Есть ли контрол содержащий неправильный текст?
        If validationMessage <> "" Then
            ' вывести сообщение.
            TextMessageInvalidControls.Text = $"Следующие поля имеют некорректное значение: {Environment.NewLine}{validationMessage}"
            Return False
        Else
            ' в противном случае вывести, что всё в порядке.
            TextMessageInvalidControls.Text = "Все поля имеют корректное значение."
            Return True
        End If
    End Function

    Private Sub FormInputGeometry_Resize(sender As Object, e As EventArgs) Handles Me.Resize
        If Me.IsHandleCreated Then
            scalling = Math.Sqrt(Me.Width * Me.Height) / modelForScalling
            decFontSize = Math.Round(fontSizeDesign * scalling) * (1.0 + (scalling - 1.0) * (sizeFontForHypotenuseK - 1.0) / (hypotenuseK - 1.0))

            If decFontSize < 1.0 Then decFontSize = 1.0
            If decFontSize > 127.0 Then decFontSize = 127.0

            SetEmbeddedControlsFont(CType(TableLayoutPanelForm, Control))
        End If
    End Sub

    Public Sub SetEmbeddedControlsFont(ByRef inControl As Control)
        For Each itemControl As Control In inControl.Controls
            If TypeOf itemControl Is TableLayoutPanel Then
                SetEmbeddedControlsFont(itemControl)
            Else
                itemControl.Font = New Font("Microsoft Sans Serif", CInt(decFontSize))

                If inControl.Controls.Count > 0 Then
                    SetEmbeddedControlsFont(itemControl)
                End If
            End If
        Next
    End Sub

    'Private Sub Проверка(ByRef objПоле As System.Windows.Forms.TextBox)
    '    Dim mstrТекст As String
    '    mstrТекст = Trim(objПоле.Text)
    '    ' Если поле не заполнено, сообщить пользователю
    '    If mstrТекст = "" Then
    '        MsgBox("Это поле не может быть пустым!", MsgBoxStyle.Critical, "Ввод шапки")
    '        'objПоле.SetFocus
    '    End If
    'End Sub

    'Private Sub txtC_Leave(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs)
    '    Dim Index As Integer = txtC.GetIndex(eventSender)
    '    Call Проверка(txtC(Index))
    'End Sub

    'Private Sub txtD_Leave(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs)
    '    Dim Index As Integer = txtD.GetIndex(eventSender)
    '    Call Проверка(txtD(Index))
    'End Sub
End Class