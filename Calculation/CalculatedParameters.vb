Imports NationalInstruments.UI
Imports NationalInstruments.UI.WindowsForms
''' <summary>
''' Расчетные параметры
''' </summary>
''' <remarks></remarks>
Public Class CalculatedParameters
    Implements IEnumerable
    Public Property CalcDictionary As Dictionary(Of String, Parameter)

    Default Public Property Item(key As String) As Double
        Get
            Return CalcDictionary(key).CalculatedValue
        End Get
        Set(value As Double)
            CalcDictionary(key).CalculatedValue = value
        End Set
    End Property

    'Public Function GetEnumerator() As IEnumerator Implements IEnumerable.GetEnumerator
    '    Return CalcDictionary.GetEnumerator()
    'End Function

    'Реализация интерфейса IEnumerable предполагает стандартную реализацию перечислителя.
    ' Однако мы можем не полагаться на стандартную реализацию, а создать свою логику итератора с помощью ключевых слов Iterator и Yield.
    ' Конструкция итератора представляет метод, в котором используется ключевое слово Yield для перебора по коллекции или массиву.
    Public Iterator Function GetEnumerator() As IEnumerator Implements IEnumerable.GetEnumerator
        For Each keysCalc As String In CalcDictionary.Keys.ToArray
            Yield CalcDictionary(keysCalc)
        Next
    End Function

    Public Const G_СУМ_РАСХОД_ТОПЛИВА_КС_КП As String = "Gсум_расход_топливаКС_КП"
    Public Const G_ОТБОРА_СУММАРНЫЙ As String = "Gрасход_отбора_сумм"
    Public Const G_ОТБОРА_ОТНОСИТЕЛЬНЫЙ As String = "Gотбора_относительный"
    Public Const G_ОТБОРА_ОТНОСИТЕЛЬНЫЙ_НАР As String = "Gотбора_относительный_нар"
    Public Const G_ОТБОРА_ОТНОСИТЕЛЬНЫЙ_ВН As String = "Gотбора_относительный_вн"
    Public Const АЛЬФА_КАМЕРЫ As String = "АльфаКамеры" ' суммарный коэф. избытка воздуха 
    Public Const АЛЬФА_КП As String = "АльфаКП" ' коэффициент избытка воздуха К.П.
    Public Const АЛЬФА_СУММАРНЫЙ As String = "АльфаСуммарный" ' коэффициент избытка воздуха суммарный
    Public Const conЛЯМБДА As String = "Лямбда" ' приведенная скорость газового потока 
    Public Const G_ВОЗДУХА As String = "Gвоздуха"
    Public Const Т_СРЕДНЯЯ_ГАЗА_НА_ВХОДЕ As String = "Тсредняя_газа_на_входе" ' Средняя температура воздуха на входе
    Public Const conT_ИНТЕГР As String = "T_интегр"
    Public Const Т_Г_РАСЧЕТ As String = "Тг_расчет" ' расчетная температура газа
    Public Const conКАЧЕСТВО As String = "Качество"
    Public Const ПОЛОЖЕНИЕ_ТУРЕЛИ As String = "Положение_Турели"

    Public Sub New()
        CalcDictionary = New Dictionary(Of String, Parameter) From {
        {G_СУМ_РАСХОД_ТОПЛИВА_КС_КП, New Parameter With {.Name = G_СУМ_РАСХОД_ТОПЛИВА_КС_КП}},
        {G_ОТБОРА_СУММАРНЫЙ, New Parameter With {.Name = G_ОТБОРА_СУММАРНЫЙ}},
        {G_ОТБОРА_ОТНОСИТЕЛЬНЫЙ, New Parameter With {.Name = G_ОТБОРА_ОТНОСИТЕЛЬНЫЙ}},
        {G_ОТБОРА_ОТНОСИТЕЛЬНЫЙ_НАР, New Parameter With {.Name = G_ОТБОРА_ОТНОСИТЕЛЬНЫЙ_НАР}},
        {G_ОТБОРА_ОТНОСИТЕЛЬНЫЙ_ВН, New Parameter With {.Name = G_ОТБОРА_ОТНОСИТЕЛЬНЫЙ_ВН}},
        {АЛЬФА_КАМЕРЫ, New Parameter With {.Name = АЛЬФА_КАМЕРЫ}},
        {АЛЬФА_КП, New Parameter With {.Name = АЛЬФА_КП}},
        {АЛЬФА_СУММАРНЫЙ, New Parameter With {.Name = АЛЬФА_СУММАРНЫЙ}},
        {conЛЯМБДА, New Parameter With {.Name = conЛЯМБДА}},
        {G_ВОЗДУХА, New Parameter With {.Name = G_ВОЗДУХА}},
        {Т_СРЕДНЯЯ_ГАЗА_НА_ВХОДЕ, New Parameter With {.Name = Т_СРЕДНЯЯ_ГАЗА_НА_ВХОДЕ}},
        {conT_ИНТЕГР, New Parameter With {.Name = conT_ИНТЕГР}},
        {Т_Г_РАСЧЕТ, New Parameter With {.Name = Т_Г_РАСЧЕТ}},
        {conКАЧЕСТВО, New Parameter With {.Name = conКАЧЕСТВО}},
        {ПОЛОЖЕНИЕ_ТУРЕЛИ, New Parameter With {.Name = ПОЛОЖЕНИЕ_ТУРЕЛИ}}}
    End Sub

    Public Sub BindingWithControls(key As String, inINumericPointer As INumericPointer, inNumericEdit As NumericEdit)
        CalcDictionary(key).ControlNumericPointer = inINumericPointer
        CalcDictionary(key).ControlNumericEdit = inNumericEdit
    End Sub

    Public Property Gсум_расход_топливаКС_КП_кг_час() As Double
        Get
            Return CalcDictionary(G_СУМ_РАСХОД_ТОПЛИВА_КС_КП).CalculatedValue
        End Get
        Set(ByVal value As Double)
            CalcDictionary(G_СУМ_РАСХОД_ТОПЛИВА_КС_КП).CalculatedValue = value
        End Set
    End Property

    Public Property Gрасход_отбора_сумм() As Double
        Get
            Return CalcDictionary(G_ОТБОРА_СУММАРНЫЙ).CalculatedValue
        End Get
        Set(ByVal value As Double)
            CalcDictionary(G_ОТБОРА_СУММАРНЫЙ).CalculatedValue = value
        End Set
    End Property

    Public Property Gотбора_относительный() As Double
        Get
            Return CalcDictionary(G_ОТБОРА_ОТНОСИТЕЛЬНЫЙ).CalculatedValue
        End Get
        Set(ByVal value As Double)
            CalcDictionary(G_ОТБОРА_ОТНОСИТЕЛЬНЫЙ).CalculatedValue = value
        End Set
    End Property

    Public Property Gотбора_относительный_нар() As Double
        Get
            Return CalcDictionary(G_ОТБОРА_ОТНОСИТЕЛЬНЫЙ_НАР).CalculatedValue
        End Get
        Set(ByVal value As Double)
            CalcDictionary(G_ОТБОРА_ОТНОСИТЕЛЬНЫЙ_НАР).CalculatedValue = value
        End Set
    End Property

    Public Property Gотбора_относительный_вн() As Double
        Get
            Return CalcDictionary(G_ОТБОРА_ОТНОСИТЕЛЬНЫЙ_ВН).CalculatedValue
        End Get
        Set(ByVal value As Double)
            CalcDictionary(G_ОТБОРА_ОТНОСИТЕЛЬНЫЙ_ВН).CalculatedValue = value
        End Set
    End Property

    Public Property АльфаКамеры() As Double
        Get
            Return CalcDictionary(АЛЬФА_КАМЕРЫ).CalculatedValue
        End Get
        Set(ByVal value As Double)
            CalcDictionary(АЛЬФА_КАМЕРЫ).CalculatedValue = value
        End Set
    End Property

    Public Property АльфаКП() As Double
        Get
            Return CalcDictionary(АЛЬФА_КП).CalculatedValue
        End Get
        Set(ByVal value As Double)
            CalcDictionary(АЛЬФА_КП).CalculatedValue = value
        End Set
    End Property

    Public Property АльфаСуммарный() As Double
        Get
            Return CalcDictionary(АЛЬФА_СУММАРНЫЙ).CalculatedValue
        End Get
        Set(ByVal value As Double)
            CalcDictionary(АЛЬФА_СУММАРНЫЙ).CalculatedValue = value
        End Set
    End Property

    Public Property Лямбда() As Double
        Get
            Return CalcDictionary(conЛЯМБДА).CalculatedValue
        End Get
        Set(ByVal value As Double)
            CalcDictionary(conЛЯМБДА).CalculatedValue = value
        End Set
    End Property

    Public Property Gвоздуха() As Double
        Get
            Return CalcDictionary(G_ВОЗДУХА).CalculatedValue
        End Get
        Set(ByVal value As Double)
            CalcDictionary(G_ВОЗДУХА).CalculatedValue = value
        End Set
    End Property

    Public Property ТсредняяГазаНаВходе() As Double
        Get
            Return CalcDictionary(Т_СРЕДНЯЯ_ГАЗА_НА_ВХОДЕ).CalculatedValue
        End Get
        Set(ByVal value As Double)
            CalcDictionary(Т_СРЕДНЯЯ_ГАЗА_НА_ВХОДЕ).CalculatedValue = value
        End Set
    End Property

    Public Property T_интегр() As Double
        Get
            Return CalcDictionary(conT_ИНТЕГР).CalculatedValue
        End Get
        Set(ByVal value As Double)
            CalcDictionary(conT_ИНТЕГР).CalculatedValue = value
        End Set
    End Property

    Public Property Тг_расчет() As Double
        Get
            Return CalcDictionary(Т_Г_РАСЧЕТ).CalculatedValue
        End Get
        Set(ByVal value As Double)
            CalcDictionary(Т_Г_РАСЧЕТ).CalculatedValue = value
        End Set
    End Property

    Public Property Качество() As Double
        Get
            Return CalcDictionary(conКАЧЕСТВО).CalculatedValue
        End Get
        Set(ByVal value As Double)
            CalcDictionary(conКАЧЕСТВО).CalculatedValue = value
        End Set
    End Property

    Public Property ПоложениеТурели() As Double
        Get
            Return CalcDictionary(ПОЛОЖЕНИЕ_ТУРЕЛИ).CalculatedValue
        End Get
        Set(ByVal value As Double)
            CalcDictionary(ПОЛОЖЕНИЕ_ТУРЕЛИ).CalculatedValue = value
        End Set
    End Property
End Class

Public Class Parameter
    'Public Enum TypeEnum
    '    Pointer
    '    NumericEdit
    'End Enum
    'Public Property Type As TypeEnum
    Public Property Name As String
    Public Property ControlNumericPointer As INumericPointer
    Public Property ControlNumericEdit As NumericEdit
    Public Property CalculatedValue As Double
End Class