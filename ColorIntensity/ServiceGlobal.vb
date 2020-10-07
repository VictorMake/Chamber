''' <summary>
''' Сервисный класс констант программы графика интенсивности
''' </summary>
Public Class ServiceGlobal

    ''' <summary>
    ''' размерность квадратного массива (диаметр)
    ''' </summary>
    ''' <returns></returns>
    Public ReadOnly Property DataSize As Integer
    ''' <summary>
    ''' смещение (радиус равен половине диаметра)
    ''' </summary>
    ''' <returns></returns>
    Public ReadOnly Property Radius As Integer
    ''' <summary>
    ''' Множитель ширины мерного участка
    ''' </summary>
    ''' <returns></returns>
    Public ReadOnly Property МасштабВысоты As Integer

    '''' <summary>
    '''' максимальный индекс температурного поля
    '''' </summary>
    '''' <returns></returns>
    'Public ReadOnly Property MaxIndex As Integer
    ''' <summary>
    ''' Минимальный радиус кольца температурного поля
    ''' </summary>
    ''' <returns></returns>
    Public Property Rmin As Double
    Public ReadOnly Property ЧислоГорелок As Integer
    Public ReadOnly Property ГрадусовВГорелке As Double
    Public ReadOnly Property ЧислоТермопар As Integer

    Private mПолнаяШиринаМерногоУчастка As Double
    Public Property ПолнаяШиринаМерногоУчастка() As Double
        Get
            Return mПолнаяШиринаМерногоУчастка
        End Get
        Set(ByVal value As Double)
            mПолнаяШиринаМерногоУчастка = value
            'MaxIndex = ПолнаяШиринаМерногоУчастка * МасштабВысоты
            Rmin = Radius - ПолнаяШиринаМерногоУчастка * МасштабВысоты
        End Set
    End Property

    Public Sub New(inЧислоТермопар As Integer, inЧислоГорелок As Integer)
        ' Установить глобальные значения констант
        DataSize = 2000
        Radius = DataSize \ 2
        МасштабВысоты = 10
        Me.ЧислоГорелок = inЧислоГорелок '28
        ГрадусовВГорелке = 360 / ЧислоГорелок
        Me.ЧислоТермопар = inЧислоТермопар '10
    End Sub
End Class
