''' <summary>
''' Входные аргументы
''' </summary>
''' <remarks></remarks>
Public Class InputParameters
    Public Const conБАРОМЕТР As String = "Барометр"
    Public Const conTБОКСА As String = "Tбокса" ' температура в боксе
    Public Const T3_МЕРН_УЧАСТКА As String = "T3мерн_участка"
    Public Const Т_ОТБОРА As String = "Тотбора"
    Public Const Т_ОТБОРА_ВН As String = "ТотбораВн"
    Public Const ДАВЛЕНИЕ_ВОЗДУХА_НА_ВХОДЕ As String = "ДавлениеВоздухаНаВходе"
    Public Const ДАВЛЕНИЕ_МАГИСТРАЛЕ_ОТБОРА As String = "ДавлениеМагистралеОтбора"
    Public Const ДАВЛЕНИЕ_МАГИСТРАЛЕ_ОТБОРА_ВН As String = "ДавлениеМагистралеОтбораВн"
    Public Const ПЕРЕПАД_ДАВЛЕНИЯ_ВОЗДУХА_НА_ВХОДЕ As String = "ПерепадДавленияВоздухаНаВходе"
    Public Const ПЕРЕПАД_ДАВЛЕНИЯ_ВОЗДУХА_ОТБОРА As String = "ПерепадДавленияВоздухаОтбора"
    Public Const ПЕРЕПАД_ДАВЛЕНИЯ_ВОЗДУХА_ОТБОРА_ВН As String = "ПерепадДавленияВоздухаОтбораВн"
    Public Const Р310_ПОЛНОЕ_ВОЗДУХА_НА_ВХОДЕ_КC As String = "Р310полное_воздуха_на_входе_КС" ' Полное Давление воздуха на входе в КС
    Public Const Р311_СТАТИЧЕСКОЕ_ВОЗДУХА_НА_ВХОДЕ_КС As String = "Р311статическое_воздуха_на_входе_КС" ' Статическое давление на входе в  КС
    Public Const Т_ТОПЛИВА_КС As String = "ТтопливаКС"
    Public Const Т_ТОПЛИВА_КП As String = "ТтопливаКП"
    Public Const Расход_Топлива_Камеры_Сгорания As String = "РасходТопливаКамерыСгорания"
    Public Const РАСХОД_ТОПЛИВА_КАМЕРЫ_ПОДОГРЕВА As String = "РасходТопливаКамерыПодогрева"
    Public Const ТВОЗДУХА_НА_ВХОДЕ_КП As String = "ТвоздухаНаВходеКП"

    Private mБарометр As Double
    Public Property Барометр() As Double
        Get
            Return mБарометр
        End Get
        Set(ByVal value As Double)
            mБарометр = value
            InputParameterDictionary(conБАРОМЕТР) = value
        End Set
    End Property

    Private mTбокса As Double
    Public Property Tбокса() As Double
        Get
            Return mTбокса
        End Get
        Set(ByVal value As Double)
            mTбокса = value
            InputParameterDictionary(conTБОКСА) = value
        End Set
    End Property

    Private mT3мерн_участка As Double
    Public Property T3мерн_участка() As Double
        Get
            Return mT3мерн_участка
        End Get
        Set(ByVal value As Double)
            mT3мерн_участка = value
            InputParameterDictionary(T3_МЕРН_УЧАСТКА) = value
        End Set
    End Property

    Private mТотбора As Double
    Public Property Тотбора() As Double
        Get
            Return mТотбора
        End Get
        Set(ByVal value As Double)
            mТотбора = value
            InputParameterDictionary(Т_ОТБОРА) = value
        End Set
    End Property

    Private mТотбораВн As Double
    Public Property ТотбораВн() As Double
        Get
            Return mТотбораВн
        End Get
        Set(ByVal value As Double)
            mТотбораВн = value
            InputParameterDictionary(Т_ОТБОРА_ВН) = value
        End Set
    End Property

    Private mДавлениеВоздухаНаВходе As Double
    Public Property ДавлениеВоздухаНаВходе() As Double
        Get
            Return mДавлениеВоздухаНаВходе
        End Get
        Set(ByVal value As Double)
            mДавлениеВоздухаНаВходе = value
            InputParameterDictionary(ДАВЛЕНИЕ_ВОЗДУХА_НА_ВХОДЕ) = value
        End Set
    End Property

    Private mДавлениеМагистралеОтбора As Double
    Public Property ДавлениеМагистралеОтбора() As Double
        Get
            Return mДавлениеМагистралеОтбора
        End Get
        Set(ByVal value As Double)
            mДавлениеМагистралеОтбора = value
            InputParameterDictionary(ДАВЛЕНИЕ_МАГИСТРАЛЕ_ОТБОРА) = value
        End Set
    End Property

    Private mДавлениеМагистралеОтбораВн As Double
    Public Property ДавлениеМагистралеОтбораВн() As Double
        Get
            Return mДавлениеМагистралеОтбораВн
        End Get
        Set(ByVal value As Double)
            mДавлениеМагистралеОтбораВн = value
            InputParameterDictionary(ДАВЛЕНИЕ_МАГИСТРАЛЕ_ОТБОРА_ВН) = value
        End Set
    End Property

    Private mПерепадДавленияВоздухаНаВходе As Double
    Public Property ПерепадДавленияВоздухаНаВходе() As Double
        Get
            Return mПерепадДавленияВоздухаНаВходе
        End Get
        Set(ByVal value As Double)
            mПерепадДавленияВоздухаНаВходе = value
            InputParameterDictionary(ПЕРЕПАД_ДАВЛЕНИЯ_ВОЗДУХА_НА_ВХОДЕ) = value
        End Set
    End Property

    Private mПерепадДавленияВоздухаОтбора As Double
    Public Property ПерепадДавленияВоздухаОтбора() As Double
        Get
            Return mПерепадДавленияВоздухаОтбора
        End Get
        Set(ByVal value As Double)
            mПерепадДавленияВоздухаОтбора = value
            InputParameterDictionary(ПЕРЕПАД_ДАВЛЕНИЯ_ВОЗДУХА_ОТБОРА) = value
        End Set
    End Property

    Private mПерепадДавленияВоздухаОтбораВн As Double
    Public Property ПерепадДавленияВоздухаОтбораВн() As Double
        Get
            Return mПерепадДавленияВоздухаОтбораВн
        End Get
        Set(ByVal value As Double)
            mПерепадДавленияВоздухаОтбораВн = value
            InputParameterDictionary(ПЕРЕПАД_ДАВЛЕНИЯ_ВОЗДУХА_ОТБОРА_ВН) = value
        End Set
    End Property

    Private mР310полное_воздуха_на_входе_КС As Double
    Public Property Р310полное_воздуха_на_входе_КС() As Double
        Get
            Return mР310полное_воздуха_на_входе_КС
        End Get
        Set(ByVal value As Double)
            mР310полное_воздуха_на_входе_КС = value
            InputParameterDictionary(Р310_ПОЛНОЕ_ВОЗДУХА_НА_ВХОДЕ_КC) = value
        End Set
    End Property

    Private mР311статическое_воздуха_на_входе_КС As Double
    Public Property Р311статическое_воздуха_на_входе_КС() As Double
        Get
            Return mР311статическое_воздуха_на_входе_КС
        End Get
        Set(ByVal value As Double)
            mР311статическое_воздуха_на_входе_КС = value
            InputParameterDictionary(Р311_СТАТИЧЕСКОЕ_ВОЗДУХА_НА_ВХОДЕ_КС) = value
        End Set
    End Property

    Private mТтопливаКС As Double
    Public Property ТтопливаКС() As Double
        Get
            Return mТтопливаКС
        End Get
        Set(ByVal value As Double)
            mТтопливаКС = value
            InputParameterDictionary(Т_ТОПЛИВА_КС) = value
        End Set
    End Property

    Private mТтопливаКП As Double
    Public Property ТтопливаКП() As Double
        Get
            Return mТтопливаКП
        End Get
        Set(ByVal value As Double)
            mТтопливаКП = value
            InputParameterDictionary(Т_ТОПЛИВА_КП) = value
        End Set
    End Property

    Private mРасходТопливаКамерыСгорания As Double
    Public Property РасходТопливаКамерыСгорания() As Double
        Get
            Return mРасходТопливаКамерыСгорания
        End Get
        Set(ByVal value As Double)
            mРасходТопливаКамерыСгорания = value
            InputParameterDictionary(Расход_Топлива_Камеры_Сгорания) = value
        End Set
    End Property

    Private mРасходТопливаКамерыПодогрева As Double
    Public Property РасходТопливаКамерыПодогрева() As Double
        Get
            Return mРасходТопливаКамерыПодогрева
        End Get
        Set(ByVal value As Double)
            mРасходТопливаКамерыПодогрева = value
            InputParameterDictionary(РАСХОД_ТОПЛИВА_КАМЕРЫ_ПОДОГРЕВА) = value
        End Set
    End Property

    Private mТвоздухаНаВходеКП As Double
    Public Property ТвоздухаНаВходеКП() As Double
        Get
            Return mТвоздухаНаВходеКП
        End Get
        Set(ByVal value As Double)
            mТвоздухаНаВходеКП = value
            InputParameterDictionary(РАСХОД_ТОПЛИВА_КАМЕРЫ_ПОДОГРЕВА) = value
        End Set
    End Property

    'Public Const ОТСЕЧКА_ТУРЕЛИ As String = "Отсечка"
    'Private mОтсечка As Double
    'Public Property Отсечка() As Double
    '    Get
    '        Return mОтсечка
    '    End Get
    '    Set(ByVal value As Double)
    '        mОтсечка = value
    '        InputParameterDictionary(ОТСЕЧКА_ТУРЕЛИ) = value
    '    End Set
    'End Property

    Public Property InputParameterDictionary As Dictionary(Of String, Double)

    Public Sub New()
        InputParameterDictionary = New Dictionary(Of String, Double) From {
        {conБАРОМЕТР, Барометр},
        {conTБОКСА, Tбокса},
        {ДАВЛЕНИЕ_ВОЗДУХА_НА_ВХОДЕ, ДавлениеВоздухаНаВходе},
        {ПЕРЕПАД_ДАВЛЕНИЯ_ВОЗДУХА_НА_ВХОДЕ, ПерепадДавленияВоздухаНаВходе},
        {T3_МЕРН_УЧАСТКА, T3мерн_участка},
        {ДАВЛЕНИЕ_МАГИСТРАЛЕ_ОТБОРА, ДавлениеМагистралеОтбора},
        {ПЕРЕПАД_ДАВЛЕНИЯ_ВОЗДУХА_ОТБОРА, ПерепадДавленияВоздухаОтбора},
        {Т_ОТБОРА, Тотбора},
        {ДАВЛЕНИЕ_МАГИСТРАЛЕ_ОТБОРА_ВН, ДавлениеМагистралеОтбораВн},
        {ПЕРЕПАД_ДАВЛЕНИЯ_ВОЗДУХА_ОТБОРА_ВН, ПерепадДавленияВоздухаОтбораВн},
        {Т_ОТБОРА_ВН, ТотбораВн},
        {Р310_ПОЛНОЕ_ВОЗДУХА_НА_ВХОДЕ_КC, Р310полное_воздуха_на_входе_КС},
        {Р311_СТАТИЧЕСКОЕ_ВОЗДУХА_НА_ВХОДЕ_КС, Р311статическое_воздуха_на_входе_КС},
        {Т_ТОПЛИВА_КС, ТтопливаКС},
        {Т_ТОПЛИВА_КП, ТтопливаКП},
        {Расход_Топлива_Камеры_Сгорания, РасходТопливаКамерыСгорания},
        {РАСХОД_ТОПЛИВА_КАМЕРЫ_ПОДОГРЕВА, РасходТопливаКамерыПодогрева},
        {ТВОЗДУХА_НА_ВХОДЕ_КП, ТвоздухаНаВходеКП}}

        'InputParameterDictionary.Add(ОТСЕЧКА_ТУРЕЛИ, Отсечка)
    End Sub

End Class
