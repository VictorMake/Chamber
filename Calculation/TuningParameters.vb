''' <summary>
''' Настроечные параметры
''' </summary>
''' <remarks></remarks>
Public Class TuningParameters
    Public Const НОМЕР_ГРЕБЕНКИ_А As String = "НомерГребенкиА"
    Public Const conCa As String = "Ca"
    Public Const conLa As String = "La"
    Public Const conDa As String = "Da"
    Public Const conZ1a As String = "Z1a"
    Public Const conZ2a As String = "Z2a"
    Public Const conZ3a As String = "Z3a"
    Public Const conZ4a As String = "Z4a"
    Public Const НОМЕР_ГРЕБЕНКИ_Б As String = "НомерГребенкиБ"
    Public Const conCb As String = "Cb"
    Public Const conLb As String = "Lb"
    Public Const conDb As String = "Db"
    Public Const conZ1b As String = "Z1b"
    Public Const conZ2b As String = "Z2b"
    Public Const conZ3b As String = "Z3b"
    Public Const conZ4b As String = "Z4b"
    Private Const conКоординатаТУ_Х_1 As String = "КоординатаТУ_Х_1"
    Private Const conКоординатаТУ_Х_2 As String = "КоординатаТУ_Х_2"
    Private Const conКоординатаТУ_Х_3 As String = "КоординатаТУ_Х_3"
    Private Const conКоординатаТУ_Х_4 As String = "КоординатаТУ_Х_4"
    Private Const conКоординатаТУ_Х_5 As String = "КоординатаТУ_Х_5"
    Private Const conКоординатаТУ_Х_6 As String = "КоординатаТУ_Х_6"
    Private Const conКоординатаТУ_Х_7 As String = "КоординатаТУ_Х_7"
    Private Const conКоординатаТУ_Х_8 As String = "КоординатаТУ_Х_8"
    Private Const conКоординатаТУ_Х_9 As String = "КоординатаТУ_Х_9"
    Private Const conКоординатаТУ_Х_10 As String = "КоординатаТУ_Х_10"
    Private Const conЭпюрнаяНерТУ_1 As String = "ЭпюрнаяНерТУ_1"
    Private Const conЭпюрнаяНерТУ_2 As String = "ЭпюрнаяНерТУ_2"
    Private Const conЭпюрнаяНерТУ_3 As String = "ЭпюрнаяНерТУ_3"
    Private Const conЭпюрнаяНерТУ_4 As String = "ЭпюрнаяНерТУ_4"
    Private Const conЭпюрнаяНерТУ_5 As String = "ЭпюрнаяНерТУ_5"
    Private Const conЭпюрнаяНерТУ_6 As String = "ЭпюрнаяНерТУ_6"
    Private Const conЭпюрнаяНерТУ_7 As String = "ЭпюрнаяНерТУ_7"
    Private Const conЭпюрнаяНерТУ_8 As String = "ЭпюрнаяНерТУ_8"
    Private Const conЭпюрнаяНерТУ_9 As String = "ЭпюрнаяНерТУ_9"
    Private Const conЭпюрнаяНерТУ_10 As String = "ЭпюрнаяНерТУ_10"
    Private Const conОкружнаяНерТУ_1 As String = "ОкружнаяНерТУ_1"
    Private Const conОкружнаяНерТУ_2 As String = "ОкружнаяНерТУ_2"
    Private Const conОкружнаяНерТУ_3 As String = "ОкружнаяНерТУ_3"
    Private Const conОкружнаяНерТУ_4 As String = "ОкружнаяНерТУ_4"
    Private Const conОкружнаяНерТУ_5 As String = "ОкружнаяНерТУ_5"
    Private Const conОкружнаяНерТУ_6 As String = "ОкружнаяНерТУ_6"
    Private Const conОкружнаяНерТУ_7 As String = "ОкружнаяНерТУ_7"
    Private Const conОкружнаяНерТУ_8 As String = "ОкружнаяНерТУ_8"
    Private Const conОкружнаяНерТУ_9 As String = "ОкружнаяНерТУ_9"
    Private Const conОкружнаяНерТУ_10 As String = "ОкружнаяНерТУ_10"
    Public Const conFdif As String = "Fdif"
    Public Const ШИРИНА_МЕРНОГО_УЧАСТКА As String = "ШиринаМерногоУчастка"
    Public Const УГОЛ_УПРЕЖДЕНИЯ_ТУРЕЛИ_ДО_НУЛЯ As String = "УголУпрежденияТурелиДоНуля"

    Public Const COM_PORT_I7565CPM As String = "ComPortI7565CPM"
    Public Const BAUND_I7565CPM As String = "BaudI7565CPM"
    Public Const NODE_I7565CPMУ As String = "NodeI7565CPM"
    Public Const K_REDUCTION As String = "Kreduction"
    Public Const CLOCK_WISE As String = "Clockwise"

    Public Const conD20трубОсн As String = "D20трубОсн"
    Public Const conD20отвОсн As String = "D20отвОсн"
    Public Const conKs As String = "Ks"
    Public Const conKtосн As String = "Ktосн"

    Public Const conD20отвОтб As String = "D20отвОтб"
    Public Const conD20трубОтб As String = "D20трубОтб"
    Public Const conKsОтбН As String = "KsОтбН"
    Public Const conKtотбн As String = "Ktотбн"

    Public Const conD20отвОтбВн As String = "D20отвОтбВн"
    Public Const conD20трубОтбВн As String = "D20трубОтбВн"
    Public Const conKsОтбВн As String = "KsОтбВн"
    Public Const conKtотбвн As String = "Ktотбвн"

#Region "Гребенка А"
    'Public Property НомерГребенкиА As Dual = New Dual ' With {.ЛогикаИлиЧисло = False, .ЦифровоеЗначение = 0, .ЛогическоеЗначение = False}
    Private mНомерГребенкиА As Dual = New Dual
    Public Property НомерГребенкиА() As Dual
        Get
            Return mНомерГребенкиА
        End Get
        Set(ByVal value As Dual)
            mНомерГребенкиА = value
            TuningDictionary(НОМЕР_ГРЕБЕНКИ_А) = value
        End Set
    End Property

    Private mCa As Dual = New Dual
    Public Property Ca() As Dual
        Get
            Return mCa
        End Get
        Set(ByVal value As Dual)
            mCa = value
            TuningDictionary(conCa) = value
        End Set
    End Property

    Private mLa As Dual = New Dual
    Public Property La() As Dual
        Get
            Return mLa
        End Get
        Set(ByVal value As Dual)
            mLa = value
            TuningDictionary(conLa) = value
        End Set
    End Property

    Private mDa As Dual = New Dual
    Public Property Da() As Dual
        Get
            Return mDa
        End Get
        Set(ByVal value As Dual)
            mDa = value
            TuningDictionary(conDa) = value
        End Set
    End Property

    Private mZ1a As Dual = New Dual
    Public Property Z1a() As Dual
        Get
            Return mZ1a
        End Get
        Set(ByVal value As Dual)
            mZ1a = value
            TuningDictionary(conZ1a) = value
        End Set
    End Property

    Private mZ2a As Dual = New Dual
    Public Property Z2a() As Dual
        Get
            Return mZ2a
        End Get
        Set(ByVal value As Dual)
            mZ2a = value
            TuningDictionary(conZ2a) = value
        End Set
    End Property

    Private mZ3a As Dual = New Dual
    Public Property Z3a() As Dual
        Get
            Return mZ3a
        End Get
        Set(ByVal value As Dual)
            mZ3a = value
            TuningDictionary(conZ3a) = value
        End Set
    End Property

    Private mZ4a As Dual = New Dual
    Public Property Z4a() As Dual
        Get
            Return mZ4a
        End Get
        Set(ByVal value As Dual)
            mZ4a = value
            TuningDictionary(conZ4a) = value
        End Set
    End Property
#End Region

#Region "Гребенка Б"
    Private mНомерГребенкиБ As Dual = New Dual
    Public Property НомерГребенкиБ() As Dual
        Get
            Return mНомерГребенкиБ
        End Get
        Set(ByVal value As Dual)
            mНомерГребенкиБ = value
            TuningDictionary(НОМЕР_ГРЕБЕНКИ_Б) = value
        End Set
    End Property

    Private mCb As Dual = New Dual
    Public Property Cb() As Dual
        Get
            Return mCb
        End Get
        Set(ByVal value As Dual)
            mCb = value
            TuningDictionary(conCb) = value
        End Set
    End Property

    Private mLb As Dual = New Dual
    Public Property Lb() As Dual
        Get
            Return mLb
        End Get
        Set(ByVal value As Dual)
            mLb = value
            TuningDictionary(conLb) = value
        End Set
    End Property

    Private mDb As Dual = New Dual
    Public Property Db() As Dual
        Get
            Return mDb
        End Get
        Set(ByVal value As Dual)
            mDb = value
            TuningDictionary(conDb) = value
        End Set
    End Property

    Private mZ1b As Dual = New Dual
    Public Property Z1b() As Dual
        Get
            Return mZ1b
        End Get
        Set(ByVal value As Dual)
            mZ1b = value
            TuningDictionary(conZ1b) = value
        End Set
    End Property

    Private mZ2b As Dual = New Dual
    Public Property Z2b() As Dual
        Get
            Return mZ2b
        End Get
        Set(ByVal value As Dual)
            mZ2b = value
            TuningDictionary(conZ2b) = value
        End Set
    End Property

    Private mZ3b As Dual = New Dual
    Public Property Z3b() As Dual
        Get
            Return mZ3b
        End Get
        Set(ByVal value As Dual)
            mZ3b = value
            TuningDictionary(conZ3b) = value
        End Set
    End Property

    Private mZ4b As Dual = New Dual
    Public Property Z4b() As Dual
        Get
            Return mZ4b
        End Get
        Set(ByVal value As Dual)
            mZ4b = value
            TuningDictionary(conZ4b) = value
        End Set
    End Property

#End Region

#Region "КоординатаТУ"
    Private mКоординатаТУ_Х_1 As Dual = New Dual
    Public Property КоординатаТУ_Х_1() As Dual
        Get
            Return mКоординатаТУ_Х_1
        End Get
        Set(ByVal value As Dual)
            mКоординатаТУ_Х_1 = value
            TuningDictionary(conКоординатаТУ_Х_1) = value
        End Set
    End Property

    Private mКоординатаТУ_Х_2 As Dual = New Dual
    Public Property КоординатаТУ_Х_2() As Dual
        Get
            Return mКоординатаТУ_Х_2
        End Get
        Set(ByVal value As Dual)
            mКоординатаТУ_Х_2 = value
            TuningDictionary(conКоординатаТУ_Х_2) = value
        End Set
    End Property

    Private mКоординатаТУ_Х_3 As Dual = New Dual
    Public Property КоординатаТУ_Х_3() As Dual
        Get
            Return mКоординатаТУ_Х_3
        End Get
        Set(ByVal value As Dual)
            mКоординатаТУ_Х_3 = value
            TuningDictionary(conКоординатаТУ_Х_3) = value
        End Set
    End Property

    Private mКоординатаТУ_Х_4 As Dual = New Dual
    Public Property КоординатаТУ_Х_4() As Dual
        Get
            Return mКоординатаТУ_Х_4
        End Get
        Set(ByVal value As Dual)
            mКоординатаТУ_Х_4 = value
            TuningDictionary(conКоординатаТУ_Х_4) = value
        End Set
    End Property

    Private mКоординатаТУ_Х_5 As Dual = New Dual
    Public Property КоординатаТУ_Х_5() As Dual
        Get
            Return mКоординатаТУ_Х_5
        End Get
        Set(ByVal value As Dual)
            mКоординатаТУ_Х_5 = value
            TuningDictionary(conКоординатаТУ_Х_5) = value
        End Set
    End Property

    Private mКоординатаТУ_Х_6 As Dual = New Dual
    Public Property КоординатаТУ_Х_6() As Dual
        Get
            Return mКоординатаТУ_Х_6
        End Get
        Set(ByVal value As Dual)
            mКоординатаТУ_Х_6 = value
            TuningDictionary(conКоординатаТУ_Х_6) = value
        End Set
    End Property

    Private mКоординатаТУ_Х_7 As Dual = New Dual
    Public Property КоординатаТУ_Х_7() As Dual
        Get
            Return mКоординатаТУ_Х_7
        End Get
        Set(ByVal value As Dual)
            mКоординатаТУ_Х_7 = value
            TuningDictionary(conКоординатаТУ_Х_7) = value
        End Set
    End Property

    Private mКоординатаТУ_Х_8 As Dual = New Dual
    Public Property КоординатаТУ_Х_8() As Dual
        Get
            Return mКоординатаТУ_Х_8
        End Get
        Set(ByVal value As Dual)
            mКоординатаТУ_Х_8 = value
            TuningDictionary(conКоординатаТУ_Х_8) = value
        End Set
    End Property

    Private mКоординатаТУ_Х_9 As Dual = New Dual
    Public Property КоординатаТУ_Х_9() As Dual
        Get
            Return mКоординатаТУ_Х_9
        End Get
        Set(ByVal value As Dual)
            mКоординатаТУ_Х_9 = value
            TuningDictionary(conКоординатаТУ_Х_9) = value
        End Set
    End Property

    Private mКоординатаТУ_Х_10 As Dual = New Dual
    Public Property КоординатаТУ_Х_10() As Dual
        Get
            Return mКоординатаТУ_Х_10
        End Get
        Set(ByVal value As Dual)
            mКоординатаТУ_Х_10 = value
            TuningDictionary(conКоординатаТУ_Х_10) = value
        End Set
    End Property

#End Region

#Region "ЭпюрнаяНерТУ"
    Private mЭпюрнаяНерТУ_1 As Dual = New Dual
    Public Property ЭпюрнаяНерТУ_1() As Dual
        Get
            Return mЭпюрнаяНерТУ_1
        End Get
        Set(ByVal value As Dual)
            mЭпюрнаяНерТУ_1 = value
            TuningDictionary(conЭпюрнаяНерТУ_1) = value
        End Set
    End Property

    Private mЭпюрнаяНерТУ_2 As Dual = New Dual
    Public Property ЭпюрнаяНерТУ_2() As Dual
        Get
            Return mЭпюрнаяНерТУ_2
        End Get
        Set(ByVal value As Dual)
            mЭпюрнаяНерТУ_2 = value
            TuningDictionary(conЭпюрнаяНерТУ_2) = value
        End Set
    End Property

    Private mЭпюрнаяНерТУ_3 As Dual = New Dual
    Public Property ЭпюрнаяНерТУ_3() As Dual
        Get
            Return mЭпюрнаяНерТУ_3
        End Get
        Set(ByVal value As Dual)
            mЭпюрнаяНерТУ_3 = value
            TuningDictionary(conЭпюрнаяНерТУ_3) = value
        End Set
    End Property

    Private mЭпюрнаяНерТУ_4 As Dual = New Dual
    Public Property ЭпюрнаяНерТУ_4() As Dual
        Get
            Return mЭпюрнаяНерТУ_4
        End Get
        Set(ByVal value As Dual)
            mЭпюрнаяНерТУ_4 = value
            TuningDictionary(conЭпюрнаяНерТУ_4) = value
        End Set
    End Property

    Private mЭпюрнаяНерТУ_5 As Dual = New Dual
    Public Property ЭпюрнаяНерТУ_5() As Dual
        Get
            Return mЭпюрнаяНерТУ_5
        End Get
        Set(ByVal value As Dual)
            mЭпюрнаяНерТУ_5 = value
            TuningDictionary(conЭпюрнаяНерТУ_5) = value
        End Set
    End Property

    Private mЭпюрнаяНерТУ_6 As Dual = New Dual
    Public Property ЭпюрнаяНерТУ_6() As Dual
        Get
            Return mЭпюрнаяНерТУ_6
        End Get
        Set(ByVal value As Dual)
            mЭпюрнаяНерТУ_6 = value
            TuningDictionary(conЭпюрнаяНерТУ_6) = value
        End Set
    End Property

    Private mЭпюрнаяНерТУ_7 As Dual = New Dual
    Public Property ЭпюрнаяНерТУ_7() As Dual
        Get
            Return mЭпюрнаяНерТУ_7
        End Get
        Set(ByVal value As Dual)
            mЭпюрнаяНерТУ_7 = value
            TuningDictionary(conЭпюрнаяНерТУ_7) = value
        End Set
    End Property

    Private mЭпюрнаяНерТУ_8 As Dual = New Dual
    Public Property ЭпюрнаяНерТУ_8() As Dual
        Get
            Return mЭпюрнаяНерТУ_8
        End Get
        Set(ByVal value As Dual)
            mЭпюрнаяНерТУ_8 = value
            TuningDictionary(conЭпюрнаяНерТУ_8) = value
        End Set
    End Property

    Private mЭпюрнаяНерТУ_9 As Dual = New Dual
    Public Property ЭпюрнаяНерТУ_9() As Dual
        Get
            Return mЭпюрнаяНерТУ_9
        End Get
        Set(ByVal value As Dual)
            mЭпюрнаяНерТУ_9 = value
            TuningDictionary(conЭпюрнаяНерТУ_9) = value
        End Set
    End Property

    Private mЭпюрнаяНерТУ_10 As Dual = New Dual
    Public Property ЭпюрнаяНерТУ_10() As Dual
        Get
            Return mЭпюрнаяНерТУ_10
        End Get
        Set(ByVal value As Dual)
            mЭпюрнаяНерТУ_10 = value
            TuningDictionary(conЭпюрнаяНерТУ_10) = value
        End Set
    End Property

#End Region

#Region "ОкружнаяНерТУ"
    Private mОкружнаяНерТУ_1 As Dual = New Dual
    Public Property ОкружнаяНерТУ_1() As Dual
        Get
            Return mОкружнаяНерТУ_1
        End Get
        Set(ByVal value As Dual)
            mОкружнаяНерТУ_1 = value
            TuningDictionary(conОкружнаяНерТУ_1) = value
        End Set
    End Property

    Private mОкружнаяНерТУ_2 As Dual = New Dual
    Public Property ОкружнаяНерТУ_2() As Dual
        Get
            Return mОкружнаяНерТУ_2
        End Get
        Set(ByVal value As Dual)
            mОкружнаяНерТУ_2 = value
            TuningDictionary(conОкружнаяНерТУ_2) = value
        End Set
    End Property

    Private mОкружнаяНерТУ_3 As Dual = New Dual
    Public Property ОкружнаяНерТУ_3() As Dual
        Get
            Return mОкружнаяНерТУ_3
        End Get
        Set(ByVal value As Dual)
            mОкружнаяНерТУ_3 = value
            TuningDictionary(conОкружнаяНерТУ_3) = value
        End Set
    End Property

    Private mОкружнаяНерТУ_4 As Dual = New Dual
    Public Property ОкружнаяНерТУ_4() As Dual
        Get
            Return mОкружнаяНерТУ_4
        End Get
        Set(ByVal value As Dual)
            mОкружнаяНерТУ_4 = value
            TuningDictionary(conОкружнаяНерТУ_4) = value
        End Set
    End Property

    Private mОкружнаяНерТУ_5 As Dual = New Dual
    Public Property ОкружнаяНерТУ_5() As Dual
        Get
            Return mОкружнаяНерТУ_5
        End Get
        Set(ByVal value As Dual)
            mОкружнаяНерТУ_5 = value
            TuningDictionary(conОкружнаяНерТУ_5) = value
        End Set
    End Property

    Private mОкружнаяНерТУ_6 As Dual = New Dual
    Public Property ОкружнаяНерТУ_6() As Dual
        Get
            Return mОкружнаяНерТУ_6
        End Get
        Set(ByVal value As Dual)
            mОкружнаяНерТУ_6 = value
            TuningDictionary(conОкружнаяНерТУ_6) = value
        End Set
    End Property

    Private mОкружнаяНерТУ_7 As Dual = New Dual
    Public Property ОкружнаяНерТУ_7() As Dual
        Get
            Return mОкружнаяНерТУ_7
        End Get
        Set(ByVal value As Dual)
            mОкружнаяНерТУ_7 = value
            TuningDictionary(conОкружнаяНерТУ_7) = value
        End Set
    End Property

    Private mОкружнаяНерТУ_8 As Dual = New Dual
    Public Property ОкружнаяНерТУ_8() As Dual
        Get
            Return mОкружнаяНерТУ_8
        End Get
        Set(ByVal value As Dual)
            mОкружнаяНерТУ_8 = value
            TuningDictionary(conОкружнаяНерТУ_8) = value
        End Set
    End Property

    Private mОкружнаяНерТУ_9 As Dual = New Dual
    Public Property ОкружнаяНерТУ_9() As Dual
        Get
            Return mОкружнаяНерТУ_9
        End Get
        Set(ByVal value As Dual)
            mОкружнаяНерТУ_9 = value
            TuningDictionary(conОкружнаяНерТУ_9) = value
        End Set
    End Property

    Private mОкружнаяНерТУ_10 As Dual = New Dual
    Public Property ОкружнаяНерТУ_10() As Dual
        Get
            Return mОкружнаяНерТУ_10
        End Get
        Set(ByVal value As Dual)
            mОкружнаяНерТУ_10 = value
            TuningDictionary(conОкружнаяНерТУ_10) = value
        End Set
    End Property

#End Region

    Private mFdif As Dual = New Dual
    Public Property Fdif() As Dual
        Get
            Return mFdif
        End Get
        Set(ByVal value As Dual)
            mFdif = value
            TuningDictionary(conFdif) = value
        End Set
    End Property

    Private mШиринаМерногоУчастка As Dual = New Dual
    Public Property ШиринаМерногоУчастка() As Dual
        Get
            Return mШиринаМерногоУчастка
        End Get
        Set(ByVal value As Dual)
            mШиринаМерногоУчастка = value
            TuningDictionary(ШИРИНА_МЕРНОГО_УЧАСТКА) = value
        End Set
    End Property

#Region "Энкодер"

    Private mУголУпрежденияТурелиДоНуля As Dual = New Dual
    Public Property УголУпрежденияТурелиДоНуля() As Dual
        Get
            Return mУголУпрежденияТурелиДоНуля
        End Get
        Set(ByVal value As Dual)
            mУголУпрежденияТурелиДоНуля = value
            TuningDictionary(УГОЛ_УПРЕЖДЕНИЯ_ТУРЕЛИ_ДО_НУЛЯ) = value
        End Set
    End Property

    Private mComPortI7565CPM As Dual = New Dual
    ''' <summary>
    ''' виртуальный порт I7565CPM
    ''' </summary>
    ''' <returns></returns>
    Public Property ComPortI7565CPM() As Dual
        Get
            Return mComPortI7565CPM
        End Get
        Set(ByVal value As Dual)
            mComPortI7565CPM = value
            TuningDictionary(COM_PORT_I7565CPM) = value
        End Set
    End Property

    Private mBaudI7565CPM As Dual = New Dual
    ''' <summary>
    ''' скорость протокола CANopen
    ''' </summary>
    ''' <returns></returns>
    Public Property BaudI7565CPM() As Dual
        Get
            Return mBaudI7565CPM
        End Get
        Set(ByVal value As Dual)
            mBaudI7565CPM = value
            TuningDictionary(BAUND_I7565CPM) = value
        End Set
    End Property

    Private mNodeI7565CPM As Dual = New Dual
    ''' <summary>
    ''' индекс устройства энкодера
    ''' </summary>
    ''' <returns></returns>
    Public Property NodeI7565CPM() As Dual
        Get
            Return mNodeI7565CPM
        End Get
        Set(ByVal value As Dual)
            mNodeI7565CPM = value
            TuningDictionary(NODE_I7565CPMУ) = value
        End Set
    End Property

    Private mKreduction As Dual = New Dual
    ''' <summary>
    ''' предаточное отношение редуктора от турели к энкоде
    ''' </summary>
    ''' <returns></returns>
    Public Property Kreduction() As Dual
        Get
            Return mKreduction
        End Get
        Set(ByVal value As Dual)
            mKreduction = value
            TuningDictionary(K_REDUCTION) = value
        End Set
    End Property

    Private mClockwise As Dual = New Dual
    ''' <summary>
    ''' вращение по часовой стрелке (=True)
    ''' </summary>
    ''' <returns></returns>
    Public Property Clockwise() As Dual
        Get
            Return mClockwise
        End Get
        Set(ByVal value As Dual)
            mClockwise = value
            TuningDictionary(CLOCK_WISE) = value
        End Set
    End Property

#End Region

#Region "Коэффициенты расхода воздуха"
    Private mD20трубОсн As Dual = New Dual
    ''' <summary>
    ''' внутренний диаметр ИТ при 20 гр. С
    ''' </summary>
    ''' <returns></returns>
    Public Property D20трубОсн() As Dual
        Get
            Return mD20трубОсн
        End Get
        Set(ByVal value As Dual)
            mD20трубОсн = value
            TuningDictionary(conD20трубОсн) = value
        End Set
    End Property

    Private mD20отвОсн As Dual = New Dual
    ''' <summary>
    ''' диаметр отверстия СУ при 20 гр. С
    ''' </summary>
    ''' <returns></returns>
    Public Property D20отвОсн() As Dual
        Get
            Return mD20отвОсн
        End Get
        Set(ByVal value As Dual)
            mD20отвОсн = value
            TuningDictionary(conD20отвОсн) = value
        End Set
    End Property

    Private mKs As Dual = New Dual
    Public Property Ks() As Dual
        Get
            Return mKs
        End Get
        Set(ByVal value As Dual)
            mKs = value
            TuningDictionary(conKs) = value
        End Set
    End Property

    Private mKtосн As Dual = New Dual
    ''' <summary>
    ''' Коэф Линейного Теплового Расширения Трубопровода
    ''' </summary>
    ''' <returns></returns>
    Public Property Ktосн() As Dual
        Get
            Return mKtосн
        End Get
        Set(ByVal value As Dual)
            mKtосн = value
            TuningDictionary(conKtосн) = value
        End Set
    End Property

    Private mD20отвОтб As Dual = New Dual
    Public Property D20отвОтб() As Dual
        Get
            Return mD20отвОтб
        End Get
        Set(ByVal value As Dual)
            mD20отвОтб = value
            TuningDictionary(conD20отвОтб) = value
        End Set
    End Property

    Private mD20трубОтб As Dual = New Dual
    Public Property D20трубОтб() As Dual
        Get
            Return mD20трубОтб
        End Get
        Set(ByVal value As Dual)
            mD20трубОтб = value
            TuningDictionary(conD20трубОтб) = value
        End Set
    End Property

    Private mKsОтбН As Dual = New Dual
    Public Property KsОтбН() As Dual
        Get
            Return mKsОтбН
        End Get
        Set(ByVal value As Dual)
            mKsОтбН = value
            TuningDictionary(conKsОтбН) = value
        End Set
    End Property

    Private mKtотбн As Dual = New Dual
    ''' <summary>
    ''' Коэф Линейного Теплового Расширения Трубопровода
    ''' </summary>
    ''' <returns></returns>
    Public Property Ktотбн() As Dual
        Get
            Return mKtотбн
        End Get
        Set(ByVal value As Dual)
            mKtотбн = value
            TuningDictionary(conKtотбн) = value
        End Set
    End Property

    Private mD20отвОтбВн As Dual = New Dual
    Public Property D20отвОтбВн() As Dual
        Get
            Return mD20отвОтбВн
        End Get
        Set(ByVal value As Dual)
            mD20отвОтбВн = value
            TuningDictionary(conD20отвОтбВн) = value
        End Set
    End Property

    Private mD20трубОтбВн As Dual = New Dual
    Public Property D20трубОтбВн() As Dual
        Get
            Return mD20трубОтбВн
        End Get
        Set(ByVal value As Dual)
            mD20трубОтбВн = value
            TuningDictionary(conD20трубОтбВн) = value
        End Set
    End Property

    Private mKsОтбВн As Dual = New Dual
    Public Property KsОтбВн() As Dual
        Get
            Return mKsОтбВн
        End Get
        Set(ByVal value As Dual)
            mKsОтбВн = value
            TuningDictionary(conKsОтбВн) = value
        End Set
    End Property

    Private mKtотбвн As Dual = New Dual
    ''' <summary>
    ''' Коэф Линейного Теплового Расширения Трубопровода
    ''' </summary>
    ''' <returns></returns>
    Public Property Ktотбвн() As Dual
        Get
            Return mKtотбвн
        End Get
        Set(ByVal value As Dual)
            mKtотбвн = value
            TuningDictionary(conKtотбвн) = value
        End Set
    End Property
#End Region

    Public Property TuningDictionary As Dictionary(Of String, Dual)

    Public Sub New()
        TuningDictionary = New Dictionary(Of String, Dual) From {
        {НОМЕР_ГРЕБЕНКИ_А, НомерГребенкиА},
        {conCa, Ca},
        {conLa, La},
        {conDa, Da},
        {conZ1a, Z1a},
        {conZ2a, Z2a},
        {conZ3a, Z3a},
        {conZ4a, Z4a},
        {НОМЕР_ГРЕБЕНКИ_Б, НомерГребенкиБ},
        {conCb, Cb},
        {conLb, Lb},
        {conDb, Db},
        {conZ1b, Z1b},
        {conZ2b, Z2b},
        {conZ3b, Z3b},
        {conZ4b, Z4b},
        {conКоординатаТУ_Х_1, КоординатаТУ_Х_1},
        {conКоординатаТУ_Х_2, КоординатаТУ_Х_2},
        {conКоординатаТУ_Х_3, КоординатаТУ_Х_3},
        {conКоординатаТУ_Х_4, КоординатаТУ_Х_4},
        {conКоординатаТУ_Х_5, КоординатаТУ_Х_5},
        {conКоординатаТУ_Х_6, КоординатаТУ_Х_6},
        {conКоординатаТУ_Х_7, КоординатаТУ_Х_7},
        {conКоординатаТУ_Х_8, КоординатаТУ_Х_8},
        {conКоординатаТУ_Х_9, КоординатаТУ_Х_9},
        {conКоординатаТУ_Х_10, КоординатаТУ_Х_10},
        {conЭпюрнаяНерТУ_1, ЭпюрнаяНерТУ_1},
        {conЭпюрнаяНерТУ_2, ЭпюрнаяНерТУ_2},
        {conЭпюрнаяНерТУ_3, ЭпюрнаяНерТУ_3},
        {conЭпюрнаяНерТУ_4, ЭпюрнаяНерТУ_4},
        {conЭпюрнаяНерТУ_5, ЭпюрнаяНерТУ_5},
        {conЭпюрнаяНерТУ_6, ЭпюрнаяНерТУ_6},
        {conЭпюрнаяНерТУ_7, ЭпюрнаяНерТУ_7},
        {conЭпюрнаяНерТУ_8, ЭпюрнаяНерТУ_8},
        {conЭпюрнаяНерТУ_9, ЭпюрнаяНерТУ_9},
        {conЭпюрнаяНерТУ_10, ЭпюрнаяНерТУ_10},
        {conОкружнаяНерТУ_1, ОкружнаяНерТУ_1},
        {conОкружнаяНерТУ_2, ОкружнаяНерТУ_2},
        {conОкружнаяНерТУ_3, ОкружнаяНерТУ_3},
        {conОкружнаяНерТУ_4, ОкружнаяНерТУ_4},
        {conОкружнаяНерТУ_5, ОкружнаяНерТУ_5},
        {conОкружнаяНерТУ_6, ОкружнаяНерТУ_6},
        {conОкружнаяНерТУ_7, ОкружнаяНерТУ_7},
        {conОкружнаяНерТУ_8, ОкружнаяНерТУ_8},
        {conОкружнаяНерТУ_9, ОкружнаяНерТУ_9},
        {conОкружнаяНерТУ_10, ОкружнаяНерТУ_10},
        {conFdif, Fdif},
        {ШИРИНА_МЕРНОГО_УЧАСТКА, ШиринаМерногоУчастка},
        {УГОЛ_УПРЕЖДЕНИЯ_ТУРЕЛИ_ДО_НУЛЯ, УголУпрежденияТурелиДоНуля},
        {COM_PORT_I7565CPM, ComPortI7565CPM},
        {BAUND_I7565CPM, BaudI7565CPM},
        {NODE_I7565CPMУ, NodeI7565CPM},
        {K_REDUCTION, Kreduction},
        {CLOCK_WISE, Clockwise},
        {conD20трубОсн, D20трубОсн},
        {conD20отвОсн, D20отвОсн},
        {conKs, Ks},
        {conKtосн, Ktосн},
        {conD20отвОтб, D20отвОтб},
        {conD20трубОтб, D20трубОтб},
        {conKsОтбН, KsОтбН},
        {conKtотбн, Ktотбн},
        {conD20отвОтбВн, D20отвОтбВн},
        {conD20трубОтбВн, D20трубОтбВн},
        {conKsОтбВн, KsОтбВн},
        {conKtотбвн, Ktотбвн}}
    End Sub
End Class

Public Class Dual
    Public Property ЛогикаИлиЧисло As Boolean
    Public Property ЦифровоеЗначение As Double
    Public Property ЛогическоеЗначение As Boolean
End Class

