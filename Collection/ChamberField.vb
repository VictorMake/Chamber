Public Class ChamberField
    Public Property ГребенкаА As Гребенка
    Public Property ГребенкаБ As Гребенка
    ' изделие
    Public Property НомерСтенда As Integer
    Public Property НомерИзделия As Integer
    Public Property Дата As Date
    Public Property ПрограммаИспытания As Integer
    Public Property НомерКорпусаКамеры As Integer
    Public Property НомерЖаровойТрубы As Integer
    Public Property НомерКоллектора As Integer
    Public Property Барометр As Single
    Public Property КонтрольЭДСКамеры As List(Of КонтрольЭДС)
    ' поля
    Public Property ПоляКамеры As List(Of Поле)

    Sub New(НомерСтенда As Integer,
            НомерИзделия As Integer,
            Дата As Date,
            ПрогрИспытания As Integer,
            НомерКорпусаКамеры As Integer,
            НомерЖаровойТрубы As Integer,
            НомерКоллектора As Integer,
            Барометр As Single,
            количествоКонтрольЭДС As Integer)

        Me.НомерСтенда = НомерСтенда
        Me.НомерИзделия = НомерИзделия
        Me.Дата = Дата
        Me.ПрограммаИспытания = ПрогрИспытания
        Me.НомерКорпусаКамеры = НомерКорпусаКамеры
        Me.НомерЖаровойТрубы = НомерЖаровойТрубы
        Me.НомерКоллектора = НомерКоллектора
        Me.Барометр = Барометр

        ' инициализировать по умолчанию
        КонтрольЭДСКамеры = New List(Of КонтрольЭДС)
        For I = 1 To количествоКонтрольЭДС
            КонтрольЭДСКамеры.Add(New КонтрольЭДС(0, 0, 0, 0, 0, 0, 0, 0, 0, 0))
        Next

        ПоляКамеры = New List(Of Поле)
    End Sub

    Public Class Гребенка
        Public Property НомерГребенки As Integer
        Public Property C As Single
        Public Property L As Single
        Public Property D As Single
        Public Property Z1 As Single
        Public Property Z2 As Single
        Public Property Z3 As Single
        Public Property Z4 As Single
        Sub New(НомерГребенки As Integer, C As Single, L As Single, D As Single, Z1 As Single, Z2 As Single, Z3 As Single, Z4 As Single)
            Me.НомерГребенки = НомерГребенки
            Me.C = C
            Me.L = L
            Me.D = D
            Me.Z1 = Z1
            Me.Z2 = Z2
            Me.Z3 = Z3
            Me.Z4 = Z4
        End Sub
    End Class

    Public Class КонтрольЭДС
        Public Property ТермопараА1 As Single
        Public Property ТермопараБ1 As Single
        Public Property ТермопараА2 As Single
        Public Property ТермопараБ2 As Single
        Public Property ТермопараА3 As Single
        Public Property ТермопараБ3 As Single
        Public Property ТермопараА4 As Single
        Public Property ТермопараБ4 As Single
        Public Property ТермопараА5 As Single
        Public Property ТермопараБ5 As Single

        Public Sub New(ТермопараА1 As Single,
                       ТермопараБ1 As Single,
                       ТермопараА2 As Single,
                       ТермопараБ2 As Single,
                       ТермопараА3 As Single,
                       ТермопараБ3 As Single,
                       ТермопараА4 As Single,
                       ТермопараБ4 As Single,
                       ТермопараА5 As Single,
                       ТермопараБ5 As Single)

            Me.ТермопараА1 = ТермопараА1
            Me.ТермопараБ1 = ТермопараБ1
            Me.ТермопараА2 = ТермопараА2
            Me.ТермопараБ2 = ТермопараБ2
            Me.ТермопараА3 = ТермопараА3
            Me.ТермопараБ3 = ТермопараБ3
            Me.ТермопараА4 = ТермопараА4
            Me.ТермопараБ4 = ТермопараБ4
            Me.ТермопараА5 = ТермопараА5
            Me.ТермопараБ5 = ТермопараБ5
        End Sub

    End Class

    Public Class Поле
        Public Property НомерПоля As Integer
        Public Property ВремяЗапуска As DateTime
        Public Property ВремяХодаТурели As DateTime
        Public Property ГорелкиКамеры As List(Of Горелка)
        Public Property КонтрольныеТочкиКамеры As List(Of КонтрольнаяТочка)

        Public Sub New(количествоГорелок As Integer,
                       количествоКТ As Integer,
                       номерПоля As Integer,
                       ВремяЗапуска As DateTime,
                       ВремяХодаТурели As DateTime)

            Me.НомерПоля = номерПоля
            Me.ВремяЗапуска = ВремяЗапуска
            Me.ВремяХодаТурели = ВремяХодаТурели

            ' инициализировать по умолчанию
            ГорелкиКамеры = New List(Of Горелка)
            For I = 1 To количествоГорелок
                ГорелкиКамеры.Add(New Горелка)
            Next

            КонтрольныеТочкиКамеры = New List(Of КонтрольнаяТочка)
            For I = 1 To количествоКТ
                КонтрольныеТочкиКамеры.Add(New КонтрольнаяТочка)
            Next
        End Sub

        Public Class Горелка
            Public Property ПолеМинимальные As ПоясТемператур
            Public Property ПолеМаксимальные As ПоясТемператур
            Public Property ПолеСредние As ПоясТемператур

            Public Sub New()
                ' инициализировать по умолчанию
                ПолеМинимальные = New ПоясТемператур(0, 0, 0, 0, 0, 0, 0, 0, 0, 0)
                ПолеМаксимальные = New ПоясТемператур(0, 0, 0, 0, 0, 0, 0, 0, 0, 0)
                ПолеСредние = New ПоясТемператур(0, 0, 0, 0, 0, 0, 0, 0, 0, 0)
            End Sub

            Public Class ПоясТемператур
                Public Property ПоясА1 As Single
                Public Property ПоясБ1 As Single
                Public Property ПоясА2 As Single
                Public Property ПоясБ2 As Single
                Public Property ПоясА3 As Single
                Public Property ПоясБ3 As Single
                Public Property ПоясА4 As Single
                Public Property ПоясБ4 As Single
                Public Property ПоясА5 As Single
                Public Property ПоясБ5 As Single

                Public Sub New(ПоясА1 As Single,
                               ПоясБ1 As Single,
                               ПоясА2 As Single,
                               ПоясБ2 As Single,
                               ПоясА3 As Single,
                               ПоясБ3 As Single,
                               ПоясА4 As Single,
                               ПоясБ4 As Single,
                               ПоясА5 As Single,
                               ПоясБ5 As Single)

                    Me.ПоясА1 = ПоясА1
                    Me.ПоясБ1 = ПоясБ1
                    Me.ПоясА2 = ПоясА2
                    Me.ПоясБ2 = ПоясБ2
                    Me.ПоясА3 = ПоясА3
                    Me.ПоясБ3 = ПоясБ3
                    Me.ПоясА4 = ПоясА4
                    Me.ПоясБ4 = ПоясБ4
                    Me.ПоясА5 = ПоясА5
                    Me.ПоясБ5 = ПоясБ5
                End Sub

            End Class
        End Class

    End Class

    Public Class КонтрольнаяТочка
        Public Property P1 As Single 'Статическое давление перед соплом мерного участка
        Public Property dP1 As Single 'Перепад давлений на сопле мерного участка
        Public Property T3 As Single 'Температура воздуха в мерном участке
        Public Property Pотб As Single 'Статическое давление участка отбора воздуха
        Public Property dPотб As Single 'Перепад давлений участка отбора воздуха
        Public Property Tотб As Single 'Т воздуха в мерном участке отбора воздуха
        Public Property ДавлениеМагистралеОтбораВн As Single 'Статическое давление участка отбора воздуха вн
        Public Property ПерепадДавленияВоздухаОтбораВн As Single 'Перепад давлений участка отбора воздуха вн
        Public Property ТотбораВн As Single 'Т воздуха в мерном участке отбора воздуха вн
        Public Property P310 As Single 'Полное давление воздуха во входном мерном участке
        Public Property P311 As Single 'Статическое давление на входе в камеру сгорания
        Public Property TтоплКС As Single 'Температура топлива подаваемого в камеру сгорания
        Public Property TтоплКП As Single 'Температура топлива подаваемого в камеру подогрева
        Public Property Tбокса As Single 'температура в боксе
        Public Property GтКС As Single 'Расход топлива камеры сгорания
        Public Property GтКП As Single 'Расход топлива камеры подогрева
        Public Property T309_1 As Single 'Температура газа на входе в камеру сгорания
        Public Property T309_2 As Single 'Температура газа на входе в камеру сгорания
        Public Property T309_3 As Single 'Температура газа на входе в камеру сгорания
        Public Property T309_4 As Single 'Температура газа на входе в камеру сгорания
        Public Property T309_5 As Single 'Температура газа на входе в камеру сгорания
        Public Property T309_6 As Single 'Температура газа на входе в камеру сгорания
        Public Property T309_7 As Single 'Температура газа на входе в камеру сгорания
        Public Property T309_8 As Single 'Температура газа на входе в камеру сгорания
        Public Property T309_9 As Single 'Температура газа на входе в камеру сгорания
        Public Property T309_10 As Single 'Температура газа на входе в камеру сгорания
        Public Property T309_11 As Single 'Температура газа на входе в камеру сгорания
        Public Property T309_12 As Single 'Температура газа на входе в камеру сгорания
        Public Property ТвоздухаНаВходеКП As Single 'Температура воздуха на входе в камеру подогрева

        Public Sub New()
        End Sub
    End Class
End Class
