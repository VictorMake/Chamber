Imports NationalInstruments.Restricted

''' <summary>
''' строго-типизированная коллекция Запись объектов
''' </summary>
''' <remarks></remarks>
Friend Class CollectionTemperatureField
    Inherits CollectionBase
    'Implements System.Collections.IEnumerable

    Private mCol As List(Of TemperatureField)

    '''  <summary>
    ''' инициализация нового экземпляра cref="ColЗаписей"
    '''  </summary>
    '''  <remarks></remarks>
    Public Sub New()
        MyBase.New()
        mCol = New List(Of TemperatureField)
    End Sub

    ''' <summary>
    ''' инициализация нового экземпляра 
    ''' </summary>
    ''' <param name="value"></param>
    ''' <remarks></remarks>
    Public Sub New(ByVal value As CollectionTemperatureField)
        Me.New()
        AddRange(value)
    End Sub

    '''  <summary>
    ''' инициализация нового экземпляра 
    '''  </summary>
    '''  <param name="value">
    '''  массив  объектов с которым инициализированна коллекция
    '''  </param>
    '''  <remarks></remarks>
    Public Sub New(ByVal value As TemperatureField())
        Me.New()
        AddRange(value)
    End Sub

    Protected Overrides Sub Finalize()
        mCol = Nothing
        MyBase.Finalize()
    End Sub

    ''' <summary>
    ''' Представление входа специфицированного индекса 
    ''' </summary>
    ''' <param name="index">Начинающаяся с нуля база индекса входа местоположение в коллекции.</param>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Default Public Property Item(ByVal index As Integer) As TemperatureField
        Get
            Return mCol.Item(index) 'DirectCast((List(index)), ToolboxItem)
        End Get
        Set(ByVal value As TemperatureField)
            mCol.Item(index) = value
        End Set
    End Property

    Public Shadows ReadOnly Property Count() As Integer
        Get
            Count = mCol.Count()
        End Get
    End Property

    Public ReadOnly Property GetAll() As TemperatureField()
        Get
            'Return mCol.GetRange(0, mCol.Count)
            Return mCol.ToArray
        End Get
    End Property

    ''' <summary>
    ''' добавить типизированный член
    ''' </summary>
    ''' <param name="key"></param>
    ''' <param name="примечание"></param>
    ''' <param name="удалить"></param>
    ''' <param name="счетчик"></param>
    ''' <param name="sKey"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function Add(ByVal key As String, ByVal примечание As String, ByVal удалить As Boolean, ByVal счетчик As Integer, Optional ByVal sKey As String = "") As TemperatureField
        Dim objNewMember As New TemperatureField() With {.Key = key,
                                                .Comment = примечание,
                                                .IsDelete = удалить,
                                                .Count = счетчик}

        'If Len(sKey) = 0 Then
        '    mCol.Add(objNewMember)
        'Else
        '    mCol.Add(objNewMember, sKey)
        'End If

        mCol.Add(objNewMember) 'заменил
        'mCol2.Add(objNewMember, sKey)
        ' return the object created
        Return objNewMember
    End Function

    Public Function Add(ByVal value As TemperatureField) As TemperatureField
        mCol.Add(value)
        Return value
    End Function

    ''' <summary>
    ''' Копирование элементов из массива в конец
    ''' </summary>
    ''' <param name="value">Массив типа  содержит объекты добавленные в коллекцию.</param>
    ''' <remarks></remarks>
    Public Sub AddRange(ByVal value As TemperatureField())
        Dim i As Integer = 0
        While (i < value.Length)
            Add(value(i))
            i += 1
        End While
    End Sub

    ''' <summary>
    '''  Добавление содержимого другого [see cref="ToolboxLibrary.ColЗаписей"/> в конец коллекции
    ''' </summary>
    ''' <param name="value">Элемент содержит объекты для добавления в коллекцию</param>
    ''' <remarks></remarks>
    Public Sub AddRange(ByVal value As CollectionTemperatureField)
        'Dim i As Integer = 0
        'While (i < value.Count)
        '    Me.Add(value(i))
        '    i = (i + 1)
        'End While
        mCol.AddRange(CType(value, IEnumerable(Of TemperatureField)))
    End Sub

    ''' <summary>
    ''' Получить значение индицирующее содержится-ли специфичный в коллекции
    ''' </summary>
    ''' <param name="value"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function Contains(ByVal value As TemperatureField) As Boolean
        Return mCol.Contains(value)
    End Function

    ''' <summary>
    ''' Копировать значение в одномерный массив  экземпяр специфицированный индексом.
    ''' </summary>
    ''' <param name="array"></param>
    ''' <param name="index"></param>
    ''' <remarks></remarks>
    Public Sub CopyTo(ByVal array As TemperatureField(), ByVal index As Integer)
        mCol.CopyTo(array, index)
    End Sub

    ''' <summary>
    ''' Возвращает индекс иначе, -1.
    ''' </summary>
    ''' <param name="value"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function IndexOf(ByVal value As TemperatureField) As Integer
        Return mCol.IndexOf(value)
    End Function

    ''' <summary>
    ''' Включить по индексу.
    ''' </summary>
    ''' <param name="index"></param>
    ''' <param name="value"></param>
    ''' <remarks></remarks>
    Public Sub Insert(ByVal index As Integer, ByVal value As TemperatureField)
        mCol.Insert(index, value)
    End Sub

    'Public Shadows Function GetEnumerator() As ToolboxItemEnumerator
    '    Return New ToolboxItemEnumerator(Me)
    'End Function
    ''' <summary>
    ''' Возвращает перечислитель для иттерации сквозь коллекции
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shadows Function GetEnumerator() As IEnumerator ' Implements System.Collections.IEnumerable.GetEnumerator
        GetEnumerator = mCol.GetEnumerator
    End Function

    ''' <summary>
    ''' Возвращение специфицированного для удаления
    ''' </summary>
    ''' <param name="vntIndexKey"></param>
    ''' <remarks></remarks>
    Public Sub Remove(ByRef vntIndexKey As Integer)
        mCol.RemoveAt(vntIndexKey)
    End Sub

    Public Sub Remove(ByVal value As TemperatureField)
        mCol.Remove(value)
    End Sub

    Public Shadows Sub Clear()
        mCol.Clear()
    End Sub

    'Public Class ToolboxItemEnumerator
    '    Inherits Object
    '    Implements IEnumerator

    '    Private baseEnumerator As IEnumerator

    '    Private temp As IEnumerable

    '    Public Sub New(ByVal mappings As ColЗаписей)
    '        Me.temp = DirectCast((mappings), IEnumerable)
    '        Me.baseEnumerator = temp.GetEnumerator()
    '    End Sub

    '    Public ReadOnly Property Current() As ToolboxItem
    '        Get
    '            Return DirectCast((baseEnumerator.Current), ToolboxItem)
    '        End Get
    '    End Property

    '    Private ReadOnly Property IEnumerator_Current() As Object Implements IEnumerator.Current
    '        Get
    '            Return baseEnumerator.Current
    '        End Get
    '    End Property

    '    Public Function MoveNext() As Boolean
    '        Return baseEnumerator.MoveNext()
    '    End Function

    '    Private Function IEnumerator_MoveNext() As Boolean Implements IEnumerator.MoveNext
    '        Return baseEnumerator.MoveNext()
    '    End Function

    '    Public Sub Reset()
    '        baseEnumerator.Reset()
    '    End Sub

    '    Private Sub IEnumerator_Reset() Implements IEnumerator.Reset
    '        baseEnumerator.Reset()
    '    End Sub
    'End Class
End Class


'Friend Class ColЗаписей
'    Inherits CollectionBase
'    Implements System.Collections.IEnumerable

'    'local variable to hold collection
'    Private mCol As List(Of Запись) 'Collection
'    'Private mCol2 As Collection

'    Public Function Add(ByRef Key As String, ByRef Примечание As String, ByRef Удалить As Boolean, ByRef Счетчик As Integer, Optional ByRef sKey As String = "") As Запись
'        'create a new object
'        Dim objNewMember As New Запись


'        'set the properties passed into the method
'        objNewMember.Key = Key
'        objNewMember.Примечание = Примечание
'        objNewMember.Удалить = Удалить
'        objNewMember.Счетчик = Счетчик
'        'If Len(sKey) = 0 Then
'        '    mCol.Add(objNewMember)
'        'Else
'        '    mCol.Add(objNewMember, sKey)
'        'End If

'        mCol.Add(objNewMember) 'заменил

'        'mCol2.Add(objNewMember, sKey)


'        'return the object created
'        Return objNewMember
'    End Function

'    Default Public ReadOnly Property Item(ByVal vntIndexKey As Integer) As Запись
'        Get
'            'used when referencing an element in the collection
'            'vntIndexKey contains either the Index or Key to the collection,
'            'this is why it is declared as a Variant
'            'Syntax: Set foo = x.Item(xyz) or Set foo = x.Item(5)
'            Return mCol.Item(vntIndexKey)
'        End Get
'    End Property



'    Public ReadOnly Property Count() As Integer
'        Get
'            'used when retrieving the number of elements in the
'            'collection. Syntax: Debug.Print x.Count
'            Count = mCol.Count()
'        End Get
'    End Property

'    Public ReadOnly Property GetAll() As Запись()
'        Get
'            'Return mCol.GetRange(0, mCol.Count)
'            Return mCol.ToArray
'        End Get
'    End Property

'    'Public ReadOnly Property NewEnum() As stdole.IUnknown
'    'Get
'    'this property allows you to enumerate
'    'this collection with the For...Each syntax
'    'NewEnum = mCol._NewEnum
'    'End Get
'    'End Property

'    Public Function GetEnumerator() As System.Collections.IEnumerator Implements System.Collections.IEnumerable.GetEnumerator
'        GetEnumerator = mCol.GetEnumerator
'    End Function


'    Public Sub Remove(ByRef vntIndexKey As Integer)
'        mCol.RemoveAt(vntIndexKey)
'    End Sub

'    Public Sub Clear()
'        mCol.Clear()
'    End Sub

'    Private Sub Class_Initialize_Renamed()
'        'creates the collection when this class is created
'        mCol = New List(Of Запись)
'    End Sub
'    Public Sub New()
'        MyBase.New()
'        Class_Initialize_Renamed()
'    End Sub


'    Private Sub Class_Terminate_Renamed()
'        'destroys collection when this class is terminated
'        'UPGRADE_NOTE: Object mCol may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
'        mCol = Nothing
'    End Sub
'    Protected Overrides Sub Finalize()
'        Class_Terminate_Renamed()
'        MyBase.Finalize()
'    End Sub
'End Class
