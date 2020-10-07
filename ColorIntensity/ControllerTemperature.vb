''' <summary>
''' Контроллер. Обеспечивает связь между пользователем и системой: 
''' контролирует ввод данных пользователем и использует модель и представление для реализации необходимой реакции.
''' </summary>
Friend Class ControllerTemperature
    ' Контроллеры представляет собой лишь элементы системы, 
    ' в чьи непосредственные обязанности входит приём данных из запроса и передача их другим элементам системы.
    ' Контроллер представляет собой связующее звено между двумя основными компонентами системы — Моделью (Model) и Представлением (View). 
    ' Модель ничего «не знает» о Представлении, а Контроллер не содержит в себе какой-либо бизнес-логики.
    ' Presenter действует над Моделью и Представлением. Он извлекает данные из хранилища (Модели) и форматирует их для отображения в Представлении.
    ' Контроллер представляет класс, с которого собственно и начинается работа приложения. Этот класс обеспечивает связь между моделью и представлением. 
    ' Получая вводимые пользователем данные, контроллер исходя из внутренней логики при необходимости обращается к модели и генерирует соответствующее представление.
    Public Property ViewColorIntensityForm As FormColorIntensityView
    Public Property Model As ModelTemperature
    Private ReadOnly Property Service As ServiceGlobal

    Public Sub New(inModelTemperature As ModelTemperature, inServiceGlobal As ServiceGlobal)
        Model = inModelTemperature
        Service = inServiceGlobal
        ViewColorIntensityForm = New FormColorIntensityView(Me, Service)
    End Sub

    ''' <summary>
    ''' Переадресовать запрос поиска индексов минимума и максимума прямоугольного массива.
    ''' </summary>
    Public Sub GetMinMaxValues()
        Model.GetMinMaxValues()
    End Sub

    ''' <summary>
    ''' Преобразовать индексы из полярных координат к декартовым.
    ''' </summary>
    ''' <param name="RIndex"></param>
    ''' <param name="angleIndex"></param>
    Public Sub ConvertToPolarIndex(ByRef RIndex As Integer, ByRef angleIndex As Double)
        Model.ConvertToPolarIndex(RIndex, angleIndex)
    End Sub

    ''' <summary>
    ''' Перевести позицию курсора в угол и высоту мерного сечения.
    ''' </summary>
    ''' <param name="XPosition"></param>
    ''' <param name="YPosition"></param>
    ''' <returns></returns>
    Public Function GetPolarFromXYPosition(XPosition As Double, YPosition As Double) As Polar
        Return Model.GetPolarFromXYPosition(XPosition, YPosition)
    End Function

    Public Function GetZData() As Double(,)
        Return Model.ZData
    End Function

    ''' <summary>
    ''' Клон массива по которому строится IntensityGraph.
    ''' </summary>
    ''' <returns></returns>
    Public Function GetZDataMemoClone() As Double(,)
        Return CType(Model.ZData.Clone(), Double(,))
    End Function

    Public Function GetZDataMemo() As Double(,)
        Return Model.ZData
    End Function

    ''' <summary>
    ''' Запрос от клиента через контроллер.
    ''' Добавление аннотаций разметок горелок и массива температурного поля с секторами.
    ''' </summary>
    Public Sub GetArrPointВurnerAnnotation()
        Model.GetArrPointВurnerAnnotation()
    End Sub

    ''' <summary>
    ''' Подготовить график интенсивности
    ''' </summary>
    ''' <param name="arrSurface"></param>
    Public Sub PreparePlotSurface(ByVal arrSurface As Double(,), ByVal координатыСтенокИТермопар() As Double)
        Service.ПолнаяШиринаМерногоУчастка = ШиринаМерногоУчастка
        ViewColorIntensityForm.InitializeApplication()
        Model.PreparePlotSurface(arrSurface, координатыСтенокИТермопар)
        ' запросить координаты минимума и максимума с прямоугольного массива температур
        GetMinMaxValues()
    End Sub

End Class
