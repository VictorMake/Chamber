Imports System.Windows.Forms
Imports NationalInstruments
''' <summary>
''' Сохранение изображения графика в файл.
''' </summary>
Public Class IntensityGraphSave
    Implements IDisposable

    'Private myImage As Image
    'Private bmp As Bitmap = Nothing
    'Private myGraphics As Graphics

    Private targetIntensityGraph As UI.WindowsForms.IntensityGraph
    Private SFDialog As SaveFileDialog

    Public Sub New(ByVal saveTarget As UI.WindowsForms.IntensityGraph)
        If saveTarget Is Nothing Then
            Throw New ArgumentNullException("Ошибка Записи IntensityGraph")
        End If

        targetIntensityGraph = saveTarget
        SFDialog = New SaveFileDialog()
        'bmp = New Bitmap(targetIntensityGraph.Width, targetIntensityGraph.Height)
        'myGraphics = Graphics.FromImage(bmp)
    End Sub

    Public Overloads Sub Dispose() Implements IDisposable.Dispose
        Dispose(True)
        GC.SuppressFinalize(Me)
    End Sub

    Protected Overridable Overloads Sub Dispose(ByVal disposing As Boolean)
        If disposing Then
            If Not SFDialog Is Nothing Then
                SFDialog.Dispose()
            End If

            'If Not myGraphics Is Nothing Then
            '    myGraphics.Dispose()
            'End If

            'If Not bmp Is Nothing Then
            '    bmp.Dispose()
            'End If
        End If
    End Sub

    Protected Overrides Sub Finalize()
        Try
            Dispose(False)
        Finally
            MyBase.Finalize()
        End Try
    End Sub

    Public Sub Save()
        With SFDialog
            .InitialDirectory = My.Computer.FileSystem.SpecialDirectories.MyDocuments
            .FileName = vbNullString
            .Title = "Сохранить график как рисунок"
            'установить флаг атрибутов
            .Filter = "Рисунок (*.png)|*.png|Рисунок JPG (*.jpg)|*.jpg|Рисунок BMP (*.bmp)|*.bmp"
            .FilterIndex = 1

            .RestoreDirectory = True
            If .ShowDialog() = DialogResult.Cancel Or Len(.FileName) = 0 Then
                Exit Sub
            End If
        End With

        Try
            ' закоментировал
            'If Not targetIntensityGraph Is Nothing Then
            '    myImage = targetIntensityGraph.ToImage
            '    'ElseIf Not targetWaveformGraph Is Nothing Then
            '    '    myImage = targetWaveformGraph.ToImage
            'End If

            'myGraphics.DrawImage(myImage, 0, 0) ', 50, 50)

            'If SFDialog.FilterIndex = 1 Then
            '    bmp.Save(fileName, Imaging.ImageFormat.Png)
            'ElseIf SFDialog.FilterIndex = 2 Then
            '    bmp.Save(fileName, Imaging.ImageFormat.Jpeg)
            'ElseIf SFDialog.FilterIndex = 3 Then
            '    bmp.Save(fileName, Imaging.ImageFormat.Bmp)
            'End If

            ' более простой вариант в новых бибилиотеках NI
            If SFDialog.FilterIndex = 1 Then
                targetIntensityGraph.ToFile(SFDialog.FileName, UI.ImageType.Png)
            ElseIf SFDialog.FilterIndex = 2 Then
                targetIntensityGraph.ToFile(SFDialog.FileName, UI.ImageType.Jpeg)
            ElseIf SFDialog.FilterIndex = 3 Then
                targetIntensityGraph.ToFile(SFDialog.FileName, UI.ImageType.Bmp)
            End If

        Catch e As Exception
            MessageBox.Show(e.ToString, "Ошибка записи графика", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            'Finally
            '    myGraphics.Dispose()
        End Try
    End Sub
End Class
