Imports System.Windows.Forms
Imports BaseForm

' в этой форме организовать счетчик перемещений
' имя класса состоит из имени DLL (varName) и имени главной формы визуального наследования (frmMain)
' Dim ClassName As String = varName & ".frmMain"
' имя класса DLL и тег визуально наследуемой формы должны совпадать (в данном случае Me.Tag=Chamber)
' assembly name  и root namespace - FormOne на странице свойств проекта также совпадает
Public Class FrmMain
    Public WithEvents myClassCalculation As ClassCalculation
    'Private mClassCalculation As IClassCalculation
    Public Overrides Property ClassCalculation() As IClassCalculation
        Get
            Return myClassCalculation
        End Get
        Set(ByVal value As IClassCalculation)
            myClassCalculation = CType(value, ClassCalculation)
        End Set
    End Property

    Private mOwnCatalogue As String ' = IO.Path.Combine(IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().GetName().CodeBase), Me.Tag)
    Public Overrides Property OwnCatalogue() As String
        Get
            Return mOwnCatalogue
        End Get
        Set(ByVal value As String)
            mOwnCatalogue = value

            If Not IO.Directory.Exists(mOwnCatalogue) Then
                MessageBox.Show(Me, $"Рабочий каталог {OwnCatalogue} для модуля расчета отсутствует. Необходимо его скопировать.",
                                Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                'System.Windows.Forms.Application.Exit()
                'System.Environment.Exit(0)
            End If
        End Set
    End Property

    ''' <summary>
    ''' Видима DLL или нет, т.е. имеет ли другие окна или она только вычисляет
    ''' </summary>
    ''' <remarks></remarks>
    Private mIsDLLVisible As Boolean = False
    Public Overrides ReadOnly Property IsDLLVisible() As Boolean
        Get
            Return mIsDLLVisible
        End Get
    End Property

    'Private varProjectManager As ProjectManager
    'Public Overrides Property Manager() As ProjectManager
    '    Get
    '        Return varProjectManager
    '    End Get
    '    Set(ByVal value As ProjectManager)
    '        varProjectManager = value
    '    End Set
    'End Property

    Public varТемпературныеПоля As FormMeasurementTemperature
    Public varПротокол As FormReportGrid
    Private varВводГеометрии As FormInputGeometry
    Public var3D As Form3D
    Private varExplorer As FormDBExplorer
    Public varEncoder As FormEncoder

    Public Overrides Sub GetValueTuningParameters()
        If myClassCalculation IsNot Nothing Then myClassCalculation.ПолучитьЗначенияНастроечныхПараметров()
    End Sub

    Friend gControllerTemperature As ControllerTemperature
    Private gModelTemperature As ModelTemperature

    Private Sub FrmMain_Load(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Load
        mOwnCatalogue = IO.Path.Combine(IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().GetName().CodeBase), Me.Tag.ToString)

        'WindowManagerPanel1.AutoDetectMdiChildWindows = False
        'MyBase.WindowManagerPanel1.CloseAllWindows

        ' выполняется после MyBase_Load
        mIsDLLVisible = True 'False ' это свойство определяет поведение расчетного модуля

        If mIsDLLVisible Then
            'Dim m_ChildFormNumber As Integer
            'For I As Integer = 1 To 1
            '    Dim ChildForm As New Explorer 'System.Windows.Forms.Form
            '    ' Make it a child of this MDI form before showing it.
            '    ChildForm.MdiParent = Me

            '    m_ChildFormNumber += 1
            '    ChildForm.Text = "Window " & m_ChildFormNumber
            '    ChildForm.Show()
            '    'MyBase.WindowManagerPanel1.SetActiveWindow(CType(ChildForm, System.Windows.Forms.Form))
            '    'MyBase.WindowManagerPanel1.AddWindow(ChildForm)
            'Next

            'MyBase.Manager.ПутьКБазеПараметров = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().GetName().CodeBase) & "\Ресурсы\МодулиРасчета\" & Me.Name & ".mdb"
            'каталог нужен только для видимых форм, где производится сохранение результатов работы
            'OwnCatalogue = "D:\Project\Saturn\Work\SSDNetworkVariable\SSDNetworkVariableFW45Ex\SSDNetworkVariable\bin\Debug\Store\ФормыПодсчетаПараметровBase\Chamber\bin\Debug" & "\" & Me.Tag '& "Chamber" 
            OwnCatalogue = IO.Path.Combine(MyBase.Manager.PathCatalog, Tag.ToString)
            PathResourses = OwnCatalogue
            gPathFieldsChamber = PathResourses & "\FieldChamber.mdb"
        End If

        MyBase.FrmBaseLoad()

        varExplorer = New FormDBExplorer() With {.MdiParent = Me}
        varExplorer.Show()

        varВводГеометрии = New FormInputGeometry() With {.MdiParent = Me}
        varВводГеометрии.Show()

        varEncoder = New FormEncoder() With {.MdiParent = Me}
        varEncoder.Show()

        varТемпературныеПоля = New FormMeasurementTemperature() With {.MdiParent = Me}
        varТемпературныеПоля.Show()

        ' сделать невозможным закрытие формы
        Manager.LoadConfiguration() ' из XML

        ' идет вслед за Me.Manager.СчитатьНастройки()
        ' в varТемпературныеПоля инициализируется ПараметрыПоляНакопленные, которая используется в конструкторе New ClassCalculation(
        myClassCalculation = New ClassCalculation(Manager)
        ClassCalculation = myClassCalculation
        ' затем связать коллекцию расчётных параметров с контролами
        varТемпературныеПоля.BindingCalculationWithControls()

        Manager.FillCombo()

        ' если какое-то сообщение будет до загрузки сеток то перерисовка их будет вызывать исключения, поэтому
        ' myClassCalculation.ПолучитьЗначенияНастроечныхПараметров() идет после Me.Manager.FillCombo()
        myClassCalculation.ПолучитьЗначенияНастроечныхПараметров()

        'If Not varIsDLLVisible Then Me.Hide()

        'Dim varfrmПротокол As New frmРезультат
        varПротокол = New FormReportGrid() With {.MdiParent = Me}
        varПротокол.Show()

        varВводГеометрии.Заполнить()

        var3D = New Form3D() With {.MdiParent = Me}
        var3D.Show()
        var3D.TabControlGrph.Enabled = False

        '--- ColorIntensityViewForm -------------------------------------------
        ' Так как явно переменная на форму ColorIntensityViewForm не создаётся, 
        ' то ссылаться на неё через контроллер gControllerTemperature.ViewColorIntensityForm
        Dim gServiceGlobal As New ServiceGlobal(ЧИСЛО_ТЕРМОПАР, ЧИСЛО_ГОРЕЛОК)

        ' можно использовать фабрики для создания ModelTemperature и ControllerTemperature
        gModelTemperature = New ModelTemperature(gServiceGlobal)
        gControllerTemperature = New ControllerTemperature(gModelTemperature, gServiceGlobal)

        ' Получить объект Представления (View), который вызывает методы, специфичные для Представления.
        gControllerTemperature.ViewColorIntensityForm.MdiParent = Me
        gControllerTemperature.ViewColorIntensityForm.Show()

        'MyBase.WindowManagerPanel1.SetActiveWindow(CType(varТемпературныеПоля, System.Windows.Forms.Form))
        ''Me.WindowManagerPanel1.SetActiveWindow(CType(varТемпературныеПоля, System.Windows.Forms.Form))

        '' установить фокус вначале на mdi child
        ''Me.MdiChildren(0).BringToFront()
        '' эквивалентный метод: 
        ''WindowManagerPanel1.SetActiveWindow(MdiChildren.Count - 1)
        ''рекомендуемый (хотя и не необходимый) использовать WindowManager methods

        'Application.DoEvents()
        'Thread.Sleep(10000)

        'Application.DoEvents()
        'Me.WindowManagerPanel1.SetActiveWindow(CType(varТемпературныеПоля, System.Windows.Forms.Form))
        'Application.DoEvents()
        TimerUpdate.Enabled = True

        'WindowManagerPanel1.ShowCloseButton = True
        'WindowManagerPanel1.ShowLayoutButtons = True
    End Sub

    'Public Sub InitializeForm3D()
    '    ' вставил сюда
    '    varfrm3D = New frm3D
    '    varfrm3D.MdiParent = Me
    '    varfrm3D.Show()
    '    varfrm3D.TabControl1.Enabled = False
    'End Sub

    Private Sub myClassCalculation_DataError(sender As Object, e As DataErrorEventArgs) Handles myClassCalculation.DataError
        ' sender.Manager.ПроверкаСоответствияПройдена - узнать что-то
        ' если произошла ошибка в классе или ошибка была специально сгенерирована то вывести сообщение
        Dim TITLE As String = "Подсчет для модуля " & Text
        MessageBox.Show($"Ошибка в подсчете ClassCalculation.dll:{ Environment.NewLine}{e.Message}{Environment.NewLine}{e.Description}",
                        TITLE, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
    End Sub

    ''' <summary>
    ''' Для активации вкладки формы ввода геометрии
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub TimerUpdate_Tick(ByVal sender As Object, ByVal e As EventArgs) Handles TimerUpdate.Tick
        TimerUpdate.Enabled = False
        MyBase.WindowManagerPanel1.SetActiveWindow(0)
        MyBase.WindowManagerPanel1.SetActiveWindow(CType(varВводГеометрии, Form))
    End Sub

End Class
