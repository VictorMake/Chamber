Imports System.Windows.Forms
Imports System.IO

Module GlobalVariable
    Public Enum ModelMeasurement
        ИзмерениеВезде
        ИзмерениеПоТемпературам
        ИзмерениеПоПотерямДавления
    End Enum

    Public Enum GraphForPrint
        ПоПоясам '= 1
        ПоСечению ' = 2
        ОтклоненияПоПоясам ' = 3
        'T4
    End Enum
    Public ГрафикДляПечати As GraphForPrint

    Public Structure MinMaxПоясов
        Dim Max As Double
        Dim imax As Integer
        Dim Min As Double
        Dim imin As Integer
        Dim dblСредняя As Double
    End Structure
    Public arrMinMaxПоясов(ЧИСЛО_ТЕРМОПАР) As MinMaxПоясов

    Public Structure Polar
        Public Angle As Double
        Public Radius As Double
    End Structure

    Public ДатаЗаписи, ВремяЗаписи As String
    Public gMainDataset As New DataSet()
    Public Const MY_DATA_VIEW_TABLE As String = "MyDataViewTable"
    Public Const ЧИСЛО_ТЕРМОПАР As Integer = 10
    Public Const ЧИСЛО_Т_309 As Integer = 12
    Public Const ЧИСЛО_ПРОМЕЖУТКОВ As Integer = 360
    Public Const ЧИСЛО_ГОРЕЛОК As Integer = 28
    Public Const КОЛИЧЕСТВО_КТ As Integer = 5
    Public Const КОЛИЧЕСТВО_КОНТРОЛЬ_ЭДС As Integer = 5

    'Public Const ЧислоОтсечек As Integer = ЧислоПромежутков + 1
    Public gНакопитьДляПоля As Boolean
    Public gРисоватьГрафикСечений As Boolean
    Public gГеометрияВведена As Boolean

    Public КоординатыТермопар As Double() = {3, 6, 9, 12, 15, 18, 21, 24, 27, 30}  'в последствии заполняются из формы и заносятся в этот массив и в таблицу настроечных параметров и записывается
    Public Const ШИРИНА As Double = 45.5 '46 ' ширина мерного участка
    Public ШиринаМерногоУчастка As Double = ШИРИНА ' настроечный параметр
    Public Fdif As Double  ' настроечный параметр площадь проходного сечения диффузора'Fдиффузора камеры сгорания
    Public y() As Double ' для сплайна

    Public ПараметрыПоляНакопленные As CharacteristicsField
    Public arrПоясDictionary As Dictionary(Of String, Double())
    Public arrТекущаяПоПоясам(ЧИСЛО_ТЕРМОПАР - 1) As Double

    Public PathResourses As String
    Public gПутьExcel As String
    Public НомерИзделия As Integer
    Public НомерСтенда As Integer
    Public ИндексОтсечекДляПоля As Integer ' для накопления через 1 градус
    Public СчетчикНакоплений As Integer
    Public Const PROVIDER_JET As String = "Provider=Microsoft.Jet.OLEDB.4.0;"
    Public gPathFieldsChamber As String
    Public gFileИсточник, gFileПриемник As String

    Public gMainFomMdiParent As FrmMain
    Public clAir As MathematicalLibrary.Air
    Public gКодИзделия As Integer
    Public gКодПоля As Integer
    Public gКодГребенкиА, gКодГребенкиБ As Integer

    Public gПутьКамера As String

    '--- расчетные вспомогательные --------------------------------------------
    Public Const ПРОЭКЦИЯ_НА_СТЕНКУ1 As String = "ПроэкцияНаСтенку1"
    Public Const ПРОЭКЦИЯ_НА_СТЕНКУ2 As String = "ПроэкцияНаСтенку2"
    Public Const ЭПЮРНАЯ_НЕРАВНОМЕРНОСТЬ As String = "ЭпюрнаяНеравномерность"
    Public Const ОКРУЖНАЯ_НЕРАВНОМЕРНОСТЬ As String = "ОкружнаяНеравномерность"
    Public Const ПОЯС_MAX As String = "Tmax"

    '--- для ссылки на статус бар формы FormMeasurementTemperature
    Public Const conStatusLabelMessage As String = "StatusLabelMessage"

    Public Function BuildCnnStr(ByVal provider As String, ByVal dataBase As String) As String
        'Jet OLEDB:Global Partial Bulk Ops=2;Jet OLEDB:Registry Path=;Jet OLEDB:Database Locking Mode=1;Data Source="D:\ПрограммыVBNET\RUD\RUD.NET\bin\Ресурсы\Channels.mdb";Jet OLEDB:Engine Type=5;Provider="Microsoft.Jet.OLEDB.4.0";Jet OLEDB:System database=;Jet OLEDB:SFP=False;persist security info=False;Extended Properties=;Mode=Share Deny None;Jet OLEDB:Encrypt Database=False;Jet OLEDB:Create System Database=False;Jet OLEDB:Don't Copy Locale on Compact=False;Jet OLEDB:Compact Without Replica Repair=False;User ID=Admin;Jet OLEDB:Global Bulk Transactions=1
        Return String.Format("{0}Data Source={1};", provider, dataBase)
    End Function

    ''' <summary>
    ''' True - файла нет
    ''' </summary>
    ''' <param name="path"></param>
    ''' <returns></returns>
    Private Function FileNotExists(ByVal path As String) As Boolean
        'FileExists = CBool(Dir(FileName) = vbNullString) 
        Return Not File.Exists(path)
    End Function

    ''' <summary>
    ''' Проверка существования файла
    ''' </summary>
    ''' <param name="path"></param>
    ''' <returns></returns>
    Public Function CheckExistsFile(ByVal path As String) As Boolean
        If FileNotExists(path) Then
            MessageBox.Show($"В каталоге нет файла <{path}> !", "Провека существования файла", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return False
        Else
            Return True
        End If
    End Function

    ''' <summary>
    ''' Запись данных в INI файл - аргументы:
    ''' </summary>
    ''' <param name="sINIFile"></param>
    ''' <param name="sSection">sSection = Название раздела</param>
    ''' <param name="sKey">sKey = Название параметра</param>
    ''' <param name="sValue">sValue = Значение параметра</param>
    ''' <remarks></remarks>
    Public Sub WriteINI(ByRef sINIFile As String, ByRef sSection As String, ByRef sKey As String, ByRef sValue As String)
        Dim N As Integer
        Dim sTemp As String = sValue

        ' Заменить символы CR/LF на пробелы
        For N = 1 To CInt(Len(sValue))
            If Mid(sValue, N, 1) = vbCr Or Mid(sValue, N, 1) = vbLf Then Mid(sValue, N) = " "
        Next

        Try
            ' Пишем значения
            N = NativeMethods.WritePrivateProfileString(sSection, sKey, sTemp, sINIFile)
            ' Проверка результата записи
            If N <> 1 Then ' Неудачное завершение
                MsgBox($"Процедура WriteINI не смогла записать параметр INI Файла:{vbCrLf}{sINIFile}{vbCrLf}
-----------------------------------------------------------------{vbCrLf}[{sSection}]{vbCrLf}{sKey}={sValue}")
            End If
        Catch ex As ApplicationException
            MessageBox.Show($"Процедура {NameOf(WriteINI)} привела к ошибке:{vbCrLf}#{ex.Message}",
                            "Ощибка чтения INI", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

End Module
