Imports System.Windows.Forms
Imports BaseForm
Imports MathematicalLibrary
Imports NationalInstruments.Analysis.Math

Friend Class CharacteristicsField
    Implements IEnumerable

    Private dicЗамерыПараметровПоля As Dictionary(Of String, SampleCharacteristicField)
    Private mЧислоПромежутков As Integer

    Public Sub New(ByVal числоПромежутков As Integer)
        mЧислоПромежутков = числоПромежутков
        dicЗамерыПараметровПоля = New Dictionary(Of String, SampleCharacteristicField)
    End Sub

    Public ReadOnly Property ЧислоПромежутков() As Integer
        Get
            Return mЧислоПромежутков
        End Get
    End Property

    Public ReadOnly Property ВсеВеличиныЗамеровПараметров() As Dictionary(Of String, SampleCharacteristicField)
        Get
            Return dicЗамерыПараметровПоля
        End Get
    End Property

    Default Public ReadOnly Property Item(ByVal sIndexKey As String) As SampleCharacteristicField
        Get
            Return dicЗамерыПараметровПоля.Item(sIndexKey)
        End Get
    End Property

    Public ReadOnly Property Count() As Integer
        Get
            Return dicЗамерыПараметровПоля.Count()
        End Get
    End Property

    Public Function GetEnumerator() As IEnumerator Implements IEnumerable.GetEnumerator
        Return dicЗамерыПараметровПоля.GetEnumerator
    End Function

    Public Sub Remove(ByRef sIndexKey As String)
        ' удаление по номеру или имени или объекту?
        ' если целый тип то по плавающему индексу, а если строковый то по ключу
        dicЗамерыПараметровПоля.Remove(sIndexKey)
    End Sub

    'Public Sub Clear()
    '    ''здесь удаление по ключу, а он строковый
    '    'не работает
    '    'Dim oneInst As Условие
    '    'For Each oneInst In mCol
    '    '    mCol.Remove(oneInst.ID.ToString)
    '    'Next
    '    'Dim I As Integer
    '    'With mCol
    '    '    For I = .Count To 1 Step -1
    '    '        .Remove(I)
    '    '    Next
    '    'End With
    '    ВеличиныЗамеровПараметров.Clear()
    'End Sub

    Public Sub Clear()
        For Each tempВеличинаЗагрузки As SampleCharacteristicField In dicЗамерыПараметровПоля.Values
            tempВеличинаЗагрузки.Clear()
        Next
    End Sub

    Public ReadOnly Property ToArray() As Object()
        Get
            Dim arrayObject(dicЗамерыПараметровПоля.Count - 1) As Object
            Dim I As Integer

            For Each itemЗамер As SampleCharacteristicField In dicЗамерыПараметровПоля.Values
                arrayObject(I) = itemЗамер
                I += 1
            Next

            Return arrayObject
        End Get
    End Property

    Protected Overrides Sub Finalize()
        dicЗамерыПараметровПоля = Nothing
        MyBase.Finalize()
    End Sub

    Public Sub Add(ByVal новыйПараметр As String, ByVal типИзмерения As ModelMeasurement)
        If Not Проверка(новыйПараметр) Then
            Exit Sub
        End If

        dicЗамерыПараметровПоля.Add(новыйПараметр, New SampleCharacteristicField(новыйПараметр, mЧислоПромежутков, типИзмерения))
    End Sub

    Private Function Проверка(ByVal новыйПараметр As String) As Boolean
        If dicЗамерыПараметровПоля.ContainsKey(новыйПараметр) Then
            MessageBox.Show($"Параметр с имененм {новыйПараметр.ToString} уже существует!",
                            "Ошибка добавления параметра", MessageBoxButtons.OK, MessageBoxIcon.Information)
            'Exit Function
            Return False
        End If

        Return True
    End Function
End Class

''' <summary>
''' Замеры Параметров Поля
''' </summary>
Friend Class SampleCharacteristicField
    Implements IEnumerable

    Private arrЗамеры() As Double
    Private mMean As Double
    Private mТипИзмерения As ModelMeasurement

    Public Sub New(ByVal name As String, ByVal числоПромежутков As Integer, ByVal типИзмерения As ModelMeasurement)
        Me.Name = name
        'ReDim_arrЗамеры(числоПромежутков)
        Re.Dim(arrЗамеры, числоПромежутков)
        mТипИзмерения = типИзмерения
    End Sub
    Public Property Name() As String

    Public ReadOnly Property ВсеЗамеры() As Double()
        Get
            Return arrЗамеры
        End Get
    End Property

    Public ReadOnly Property ТипИзмерения() As ModelMeasurement
        Get
            Return mТипИзмерения
        End Get
    End Property

    Default Public Property Item(ByVal indexKeyi As Integer) As Double
        Get
            Return arrЗамеры(indexKeyi)
        End Get
        Set(ByVal value As Double)
            arrЗамеры(indexKeyi) = value
        End Set
    End Property

    'Public ReadOnly Property Count() As Integer
    '    Get
    '        Count = Замеры.Count()
    '    End Get
    'End Property

    Public Function GetEnumerator() As IEnumerator Implements IEnumerable.GetEnumerator
        Return arrЗамеры.GetEnumerator
    End Function

    Public Sub Clear()
        Array.Clear(arrЗамеры, 0, arrЗамеры.Length)
    End Sub

    Public Sub CalculateMean()
        mMean = Statistics.Mean(arrЗамеры)
    End Sub

    Public ReadOnly Property Mean() As Double
        Get
            Return Statistics.Mean(arrЗамеры)
        End Get
    End Property

    Protected Overrides Sub Finalize()
        arrЗамеры = Nothing
        MyBase.Finalize()
    End Sub
End Class