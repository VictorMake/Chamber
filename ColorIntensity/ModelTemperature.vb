Imports System.Drawing
Imports MathematicalLibrary

''' <summary>
''' Модель предоставляет знания: данные и методы работы с этими данными, реагирует на запросы, изменяя своё состояние. 
''' Не содержит информации, как эти знания можно визуализировать.
''' </summary>
Public Class ModelTemperature
    ' Модель представляет собой фундаментальные данные, необходимые для работы приложения.
    ' При каждом изменении внутренних данных в модели она оповещает все зависящие от неё представления, и представление обновляется.
    ' Это не только совокупность кода доступа к данным, а вся бизнес-логика.
    ' Модель представляет набор классов, описывающих логику используемых данных.

    ''' <summary>
    ''' Представляет метод, который будет обрабатывать событие, когда событие разметки горелок предоставляет данные.
    ''' </summary>
    Public Event ВurnersPointReached As EventHandler(Of ВurnersPointEventArgs)
    ''' <summary>
    ''' Представляет метод, который будет обрабатывать событие, 
    ''' когда событие поиска минимального и максимального значения предоставляет данные.
    ''' </summary>
    Public Event MinMaxValuesReached As EventHandler(Of MinMaxValuesEventArgs)

    Private ReadOnly Property Service As ServiceGlobal

    Private mTemperature As Double(,)
    ''' <summary>
    ''' Температурное поле.
    ''' </summary>
    ''' <returns></returns>
    Public ReadOnly Property Temperature() As Double(,)
        Get
            Return mTemperature
        End Get
    End Property

    Private mZData As Double(,)
    ''' <summary>
    ''' Немодифицировнная квадратная матрица графика интенсивности температурного поля.
    ''' </summary>
    ''' <returns></returns>
    Public ReadOnly Property ZData() As Double(,)
        Get
            Return mZData
        End Get
    End Property

    Private mMaxT As Double
    ''' <summary>
    ''' Максималная температура.
    ''' </summary>
    ''' <returns></returns>
    Public ReadOnly Property MaxT() As Double
        Get
            Return mMaxT
        End Get
    End Property

    Private mMinT As Double
    ''' <summary>
    ''' Минимальная температура.
    ''' </summary>
    ''' <returns></returns>
    Public ReadOnly Property MinT() As Double
        Get
            Return mMinT
        End Get
    End Property

    Public Sub New(inServiceGlobal As ServiceGlobal)
        Me.Service = inServiceGlobal

        'InitializeModel()
    End Sub

    'Private Sub InitializeModel()
    '    mTemperature = New Double(mService.MaxIndex, 359) {} ' температурное поле
    '    'mZData = GenerateIntensityDataRadius()
    'End Sub

    '''' <summary>
    '''' Тест
    '''' </summary>
    '''' <returns></returns>
    'Private Function GenerateIntensityData() As Double(,)
    '    Dim radius As Integer = mService.DataSize / 2 ' равен половине диаметра
    '    Dim data As Double(,) = New Double(mService.DataSize, mService.DataSize) {}
    '    ' Сгенерированные данные как круг.
    '    ' Использование подобие круга и транспонирование оригинала.
    '    Dim i As Integer = -radius

    '    While i <= radius
    '        Dim j As Integer = -radius

    '        While j <= radius
    '            data(radius + i, radius + j) = i * i + j * j
    '            Math.Max(Threading.Interlocked.Increment(j), j - 1)
    '        End While

    '        Math.Max(Threading.Interlocked.Increment(i), i - 1)
    '    End While

    '    Return data
    'End Function

    '''' <summary>
    '''' Тест: заполнить начальный массив>
    '''' </summary>
    'Private Sub TestFillArray()
    '    'Dim tempValue As Integer = maxIndex
    '    'For I As Integer = 0 To maxIndex
    '    '    For J As Integer = 0 To 359
    '    '        Temperature(I, J) = J + tempValue
    '    '    Next
    '    '    tempValue -= 1
    '    'Next

    '    Dim tempValue As Integer = 0

    '    For I As Integer = 0 To mService.MaxIndex
    '        For J As Integer = 0 To 359
    '            mTemperature(I, J) = (J + tempValue) * 2 + 300
    '        Next

    '        tempValue += 1
    '    Next

    '    ' теперь отобразить 
    '    For J As Integer = 0 To 359
    '        Dim index As Integer = mService.MaxIndex

    '        For I As Integer = 0 To mService.MaxIndex / 2
    '            tempValue = mTemperature(I, J)
    '            mTemperature(I, J) = mTemperature(index, J)
    '            mTemperature(index, J) = tempValue
    '            index -= 1
    '        Next
    '    Next
    'End Sub

    ''' <summary>
    ''' Подготовить график интенсивности
    ''' </summary>
    ''' <param name="arrSurface"></param>
    Public Sub PreparePlotSurface(arrSurface As Double(,), ByVal координатыСтенокИТермопар() As Double)
        mZData = GenerateIntensityDataRadius(arrSurface, координатыСтенокИТермопар)
    End Sub

    ''' <summary>
    ''' Генерация прямоугольной матрицы температкрного поля и 
    ''' квадратной матрицы графика интенсивности с полярным представлением температурного поля.
    ''' </summary>
    ''' <returns></returns>
    Private Function GenerateIntensityDataRadius(arrSurface As Double(,), ByVal координатыСтенокИТермопар() As Double) As Double(,)
        Dim data As Double(,) = New Double(Service.DataSize, Service.DataSize) {} ' массив графика в декартовых координатах
        Dim dataPolar As Polar(,) = New Polar(Service.DataSize, Service.DataSize) {} ' каждая точка содержит также второе представление в полярных координатах
        Dim P As Double ' радиус вектор
        Dim anglePHi As Double ' угол в градусах после перевода из радиан
        Dim angle As Double ' скорректированный угол для своего квадранта

        'TestFillArray()

        Const градус359 As Integer = 359
        Dim ширинаМерногоУчастка As Integer = CInt(Math.Round(Service.ПолнаяШиринаМерногоУчастка))
        Dim arrInputY(Service.ЧислоТермопар + 1) As Double ' срез температур по термопарам в сечении углового положения
        Dim arrTblКоэффициенты(,) As Double = Nothing ' таблица коэффициентов сплайн-аппроксимации
        Dim arrИнтерполированныйВходной(градус359, ширинаМерногоУчастка) As Double
        Dim arrИнтерполированныйВходнойИнверсия(градус359, ширинаМерногоУчастка) As Double ' для цилиндра А1-радиус макс, Б5 - радиус мин

        ' Можно умножить координаты термопра на Service.МасштабВысоты
        ' вычислить arrИнтерполированныйВходнойИнверсия и присвоить его mTemperature.
        ' Но здесь задействаован Spline3.BicubicResample для масштабирования arrВходнойМассивТранспонирован
        ' Результаты совпадают очень сильно.

        For угол As Integer = 0 To градус359
            For J = 0 To Service.ЧислоТермопар + 1
                arrInputY(J) = arrSurface(J, угол)
            Next

            ' найдем таблицу коэффициентов
            Spline3.Spline3BuildTable(UBound(координатыСтенокИТермопар) + 1, 2, координатыСтенокИТермопар, arrInputY, 0, 0, arrTblКоэффициенты)

            ' через 1 мм проходим по каждой высоте
            ' интерполяция среза температур по термопарам в сечении углового положения
            For шаг As Integer = 0 To ширинаМерногоУчастка
                arrИнтерполированныйВходной(угол, шаг) = Spline3.Spline3Interpolate(UBound(arrTblКоэффициенты, 2) + 1, arrTblКоэффициенты, шаг)
                arrИнтерполированныйВходнойИнверсия(угол, ширинаМерногоУчастка - шаг) = arrИнтерполированныйВходной(угол, шаг)
            Next
        Next

        ' в итоге arrВходнойМассивТранспонирован(ширинаМерногоУчастка, градус359)
        Dim arrВходнойМассивТранспонирован As Double(,) = NationalInstruments.Analysis.Math.LinearAlgebra.Transpose(arrИнтерполированныйВходнойИнверсия)

        ' масштабирование массива по узлам сетки
        Dim oldHeight As Integer = UBound(arrВходнойМассивТранспонирован, 1) + 1 '47 '- старый размер сетки
        Dim oldWidth As Integer = UBound(arrВходнойМассивТранспонирован, 2) + 1 ' 359 + 1- старый размер сетки
        Dim newHeight As Integer = CInt(Service.ПолнаяШиринаМерногоУчастка * Service.МасштабВысоты)  '454 '- новый размер сетки
        Dim newWidth As Integer = oldWidth * Service.МасштабВысоты ' 3599 

        Spline3.BicubicResample(oldWidth, oldHeight, newWidth, newHeight, arrВходнойМассивТранспонирован, mTemperature)

        '' вставить минимум и максимум для теста
        'mTemperature(100, 180) = -1999
        'mTemperature(100, 270) = 1999

        ' квадрант 1
        For X As Integer = 0 To Service.Radius
            For Y As Integer = 0 To Service.Radius
                P = Math.Sqrt(X * X + Y * Y)
                If P >= Service.Rmin AndAlso P <= Service.Radius Then
                    anglePHi = Math.Atan2(Y, X) * (180 / Math.PI)
                    angle = 90 - anglePHi
                    dataPolar(X + Service.Radius, Y + Service.Radius) = New Polar With {.Angle = angle, .Radius = P}
                    'dataPolar(X + radius, Y + radius).Angle = 1
                    'dataPolar(X + radius, Y + radius).Radius = 1
                    'data(X + radius, Y + radius) = 400
                    'data(X + radius, Y + radius) = angle + P
                End If
            Next
        Next

        ' квадрант 2
        For X As Integer = 0 To -Service.Radius Step -1
            For Y As Integer = 0 To Service.Radius
                P = Math.Sqrt(X * X + Y * Y)
                If P >= Service.Rmin AndAlso P <= Service.Radius Then
                    anglePHi = Math.Atan2(Y, X) * (180 / Math.PI)
                    angle = 450 - anglePHi
                    dataPolar(X + Service.Radius, Y + Service.Radius) = New Polar With {.Angle = angle, .Radius = P}
                    'data(X + radius, Y + radius) = 800
                    'data(X + radius, Y + radius) = angle + P
                End If
            Next
        Next

        ' квадрант 3
        For X As Integer = 0 To -Service.Radius Step -1
            For Y As Integer = 0 To -Service.Radius Step -1
                P = Math.Sqrt(X * X + Y * Y)
                If P >= Service.Rmin AndAlso P <= Service.Radius Then
                    If Y = 0 Then
                        angle = 270
                        dataPolar(X + Service.Radius, Y + Service.Radius) = New Polar With {.Angle = angle, .Radius = P}
                        'data(X + radius, Y + radius) = angle + P
                    Else
                        anglePHi = Math.Atan2(Y, X) * (180 / Math.PI)
                        angle = 90 - anglePHi
                        dataPolar(X + Service.Radius, Y + Service.Radius) = New Polar With {.Angle = angle, .Radius = P}
                        'data(X + radius, Y + radius) = 1200
                        'data(X + radius, Y + radius) = angle + P
                    End If
                End If
            Next
        Next

        ' квадрант 4
        For X As Integer = 0 To Service.Radius
            For Y As Integer = 0 To -Service.Radius Step -1
                P = Math.Sqrt(X * X + Y * Y)
                If P >= Service.Rmin AndAlso P <= Service.Radius Then
                    anglePHi = Math.Atan2(Y, X) * (180 / Math.PI)
                    angle = 90 - anglePHi
                    dataPolar(X + Service.Radius, Y + Service.Radius) = New Polar With {.Angle = angle, .Radius = P}
                    'data(X + radius, Y + radius) = 1600
                    'data(X + radius, Y + radius) = 90 - anglePHi + P
                End If
            Next
        Next

        ' перевести из массива temperature() в массив data() используя вспомогательный массив dataPolar() 
        ' по значениям угла и радиуса
        For I As Integer = 0 To Service.DataSize
            For J As Integer = 0 To Service.DataSize
                angle = dataPolar(I, J).Angle
                P = dataPolar(I, J).Radius

                If angle <> 0 AndAlso P <> 0 Then
                    ' не позволить превысить размерность массива
                    If Math.Round(angle) >= 360 Then angle = 359.9
                    If Math.Round(P - Service.Rmin) > mTemperature.GetUpperBound(0) Then P = P - 1

                    data(I, J) = mTemperature(CInt(Math.Round(P - Service.Rmin)), CInt(Math.Round(angle * Service.МасштабВысоты)))
                End If
            Next
        Next

        'mZDataMemo = data.Clone() ' копировать в массив памяти

        Return data
    End Function

    ''' <summary>
    ''' Найти индексы минимума и максимума прямоугольного массива.
    ''' </summary>
    Public Sub GetMinMaxValues()
        'ByRef minXIndex As Integer, ByRef minYIndex As Integer, ByRef maxXIndex As Integer, ByRef maxYIndex As Integer)
        ' сканирование массива и установка ссылок минимума и максимума 
        'Dim data As Double(,) = intensityPlot.GetZData()
        ' на всякий случай обнулить
        'minXIndex = 0
        'minYIndex = 0
        'maxXIndex = 0
        'maxYIndex = 0

        Dim minXIndex As Integer
        Dim minYIndex As Integer
        Dim maxXIndex As Integer
        Dim maxYIndex As Integer

        mMaxT = Double.MinValue
        mMinT = Double.MaxValue

        Dim i As Integer
        For i = 0 To mTemperature.GetUpperBound(0) Step i + 1
            Dim j As Integer
            j = 0
            For j = 0 To mTemperature.GetUpperBound(1) Step j + 1
                If mMaxT < mTemperature(i, j) Then
                    mMaxT = mTemperature(i, j)
                    maxXIndex = i
                    maxYIndex = j
                End If

                If mMinT > mTemperature(i, j) Then
                    mMinT = mTemperature(i, j)
                    minXIndex = i
                    minYIndex = j
                End If
            Next
        Next

        ' перевести индекс в градусы
        Dim minDegree As Double = minYIndex / Service.МасштабВысоты
        Dim maxDegree As Double = maxYIndex / Service.МасштабВысоты

        Dim args As MinMaxValuesEventArgs = New MinMaxValuesEventArgs With {
            .ВысотаMinT = Service.ПолнаяШиринаМерногоУчастка - minXIndex / Service.МасштабВысоты,
            .УголMinT = minDegree,
            .ВысотаMaxT = Service.ПолнаяШиринаМерногоУчастка - maxXIndex / Service.МасштабВысоты,
            .УголMaxT = maxDegree
        }

        ' надо преобразовать индексы из полярных координат к декартовым
        ConvertToPolarIndex(minXIndex, minDegree)
        ConvertToPolarIndex(maxXIndex, maxDegree)

        args.MinPosition = New Point(minXIndex, CInt(minDegree))
        args.MaxPosition = New Point(maxXIndex, CInt(maxDegree))
        args.MinT = mMinT
        args.MaxT = mMaxT

        OnGetMinMaxValuesReached(args)
    End Sub

    Protected Overridable Sub OnGetMinMaxValuesReached(e As MinMaxValuesEventArgs)
        RaiseEvent MinMaxValuesReached(Me, e)
    End Sub

    ''' <summary>
    ''' Преобразовать индексы из полярных координат к декартовым.
    ''' </summary>
    ''' <param name="RIndex"></param>
    ''' <param name="angleDegree"></param>
    Public Sub ConvertToPolarIndex(ByRef RIndex As Integer, ByRef angleDegree As Double)
        ' RIndex - это радиус вектор в обратном порядке (или высота мерного сечения)
        ' angleIndex - это угол (положения турели)
        Dim degrees As Double = 450 - angleDegree
        Dim angle As Double = Math.PI * degrees / 180.0 ' перевод из радиан в углы
        Dim sinAngle As Double = Math.Sin(angle)
        Dim cosAngle As Double = Math.Cos(angle)
        ' привести к декартовым
        Dim x As Double = (RIndex + Service.Rmin) * cosAngle ' проекция радиус вектора на ось X
        Dim y As Double = (RIndex + Service.Rmin) * sinAngle ' проекция радиус вектора на ось Y

        ' возврат ссылочного значения с учётом смещения
        RIndex = CInt(Service.Radius + x)
        angleDegree = Service.Radius + y
    End Sub

    ''' <summary>
    ''' Перевести позицию курсора в угол и высоту мерного сечения.
    ''' </summary>
    ''' <param name="XPosition"></param>
    ''' <param name="YPosition"></param>
    ''' <returns></returns>
    Friend Function GetPolarFromXYPosition(XPosition As Double, YPosition As Double) As Polar
        XPosition = XPosition - Service.Radius
        YPosition = YPosition - Service.Radius
        Dim P As Double = Math.Sqrt(XPosition * XPosition + YPosition * YPosition)
        Dim anglePHi As Double
        Dim angle As Double

        ' перевод из радиан в углы
        If XPosition >= 0 AndAlso YPosition >= 0 Then
            ' квадрант 1
            anglePHi = Math.Atan2(YPosition, XPosition) * (180 / Math.PI)
            angle = 90 - anglePHi
        ElseIf XPosition < 0 AndAlso YPosition >= 0 Then
            ' квадрант 2
            anglePHi = Math.Atan2(YPosition, XPosition) * (180 / Math.PI)
            angle = 450 - anglePHi
        ElseIf XPosition < 0 AndAlso YPosition < 0 Then
            ' квадрант 3
            If YPosition = 0 Then
                angle = 270
            Else
                anglePHi = Math.Atan2(YPosition, XPosition) * (180 / Math.PI)
                angle = 90 - anglePHi
            End If
        ElseIf XPosition >= 0 AndAlso YPosition < 0 Then
            ' квадрант 4
            anglePHi = Math.Atan2(YPosition, XPosition) * (180 / Math.PI)
            angle = 90 - anglePHi
        Else
            ' чистый 0
            angle = 0
            P = 0
        End If

        ' высота мерного сечения в милиметрах высчитывается из радиус-вектора, но в обратном порядке
        Dim polarRadius As Double
        If P >= Service.Rmin AndAlso P <= Service.Radius Then
            polarRadius = (Service.Radius - P) / Service.МасштабВысоты
        Else
            polarRadius = 0
        End If

        Return New Polar With {.Angle = angle, .Radius = polarRadius}
    End Function

    ''' <summary>
    ''' Запрос от клиента через контроллер.
    ''' Вызвать событие с аргументом содержащий массив координат аннотаций разметок горелок 
    ''' и массив температурного поля с секторами.
    ''' </summary>
    Public Sub GetArrPointВurnerAnnotation()
        ' пройти по окружности и в секторе горелок в массиве data() сделать дырки
        Dim data As Double(,) = CType(mZData.Clone(), Double(,))
        Dim angle As Double
        Dim degrees As Double
        Dim arrXYPosition(Service.ЧислоГорелок - 1) As PointF

        For I As Integer = 0 To Service.ЧислоГорелок - 1
            'конецСектора = CInt(System.Math.Round(I * градусовВГорелке))
            ' радиус идёт по теории против часовой стрелки от 0 к 90 градусам
            ' надо сместить и вращать в другую сторону, по часовой стрелке
            degrees = 90 - I * Service.ГрадусовВГорелке

            ' радиус вектор от Rmin to Rmax
            For R As Integer = CInt(Service.Rmin) To Service.Radius
                angle = Math.PI * degrees / 180.0
                Dim sinAngle As Double = Math.Sin(angle)
                Dim cosAngle As Double = Math.Cos(angle)

                Dim x As Double = R * cosAngle
                Dim y As Double = R * sinAngle

                data(CInt(Service.Radius + x), CInt(Service.Radius + y)) = 0

                If R = CInt(Service.Rmin) Then
                    ' положение аннотации смещено на половину сектора горелки
                    Dim degreesAnnotation As Double = 90 - I * Service.ГрадусовВГорелке - Service.ГрадусовВГорелке / 2
                    Dim angleAnnotation As Double = Math.PI * degreesAnnotation / 180.0 ' перевод из радиан в углы
                    Dim sinAngleAnnotation As Double = Math.Sin(angleAnnotation)
                    Dim cosAngleAnnotation As Double = Math.Cos(angleAnnotation)
                    Dim xAnnotation As Double = R * cosAngleAnnotation ' проекция радиус вектора на ось X
                    Dim yAnnotation As Double = R * sinAngleAnnotation

                    arrXYPosition(I).X = CSng(Service.Radius + xAnnotation)
                    arrXYPosition(I).Y = CSng(Service.Radius + yAnnotation)
                End If
            Next
        Next

        Dim args As ВurnersPointEventArgs = New ВurnersPointEventArgs() With {.XYPositions = arrXYPosition, .ZDataMarkingOut = data}
        OnGetArrВurnerPointReached(args)
    End Sub

    Protected Overridable Sub OnGetArrВurnerPointReached(e As ВurnersPointEventArgs)
        RaiseEvent ВurnersPointReached(Me, e)
    End Sub

End Class

''' <summary>
''' Унаследованный класс аргумента события.
''' </summary>
Public Class ВurnersPointEventArgs
    Inherits EventArgs

    ''' <summary>
    ''' Массив координат разметок
    ''' </summary>
    ''' <returns></returns>
    Public Property XYPositions As PointF()
    ''' <summary>
    ''' Поле температур с нулевыми значениями в месте раздела горелок
    ''' </summary>
    ''' <returns></returns>
    Public Property ZDataMarkingOut() As Double(,)
End Class


' MinMaxValues
''' <summary>
''' Унаследованный класс аргумента события.
''' </summary>
Public Class MinMaxValuesEventArgs
    Inherits EventArgs

    ''' <summary>
    ''' Позиция аннотации минимальной температуры
    ''' </summary>
    ''' <returns></returns>
    Public Property MinPosition As Point
    ''' <summary>
    ''' Минимальная температура
    ''' </summary>
    ''' <returns></returns>
    Public Property MinT As Double
    ''' <summary>
    ''' Угол турели с минимальной температурой
    ''' </summary>
    ''' <returns></returns>
    Public Property УголMinT As Double
    ''' <summary>
    ''' Координата минимальной температуры по высоте мерного сечения (обратный порядок)
    ''' </summary>
    ''' <returns></returns>
    Public Property ВысотаMinT As Double

    ''' <summary>
    ''' Позиция аннотации максимальной температуры
    ''' </summary>
    ''' <returns></returns>
    Public Property MaxPosition As Point
    ''' <summary>
    ''' Максимальная температура
    ''' </summary>
    ''' <returns></returns>
    Public Property MaxT As Double
    ''' <summary>
    ''' Угол турели с максимальной температурой
    ''' </summary>
    ''' <returns></returns>
    Public Property УголMaxT As Double
    ''' <summary>
    ''' Координата максимальной температуры по высоте мерного сечения (обратный порядок)
    ''' </summary>
    ''' <returns></returns>
    Public Property ВысотаMaxT As Double
End Class