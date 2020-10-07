Imports System.Drawing.Printing
Imports NationalInstruments
Imports System.Windows.Forms

''' <summary>
''' Печать изображения графика в файл.
''' </summary>
Public Class IntensityGraphPrinter
    Implements IDisposable

    Private document As PrintDocument
    Private dialog As PrintDialog
    Private dlg As New PrintPreviewDialog()
    Private psDlg As New PageSetupDialog()
    Private storedPageSettings As PageSettings
    Private targetIntensityGraph As UI.WindowsForms.IntensityGraph

    Public Sub New(ByVal printTarget As UI.WindowsForms.IntensityGraph)
        If printTarget Is Nothing Then
            Throw New ArgumentNullException("Ошибка Печати IntensityGraph")
        End If

        targetIntensityGraph = printTarget
        document = New PrintDocument()
        dialog = New PrintDialog()
        AddHandler document.PrintPage, AddressOf OnPrintPageIntensityGraph
        dialog.Document = document
    End Sub

    Public Overloads Sub Dispose() Implements IDisposable.Dispose
        Dispose(True)
        GC.SuppressFinalize(Me)
    End Sub

    Protected Overridable Overloads Sub Dispose(ByVal disposing As Boolean)
        If disposing Then
            If Not document Is Nothing Then
                document.Dispose()
            End If
            If Not dialog Is Nothing Then
                dialog.Dispose()
            End If
            If Not dlg Is Nothing Then
                dlg.Dispose()
            End If
            If Not psDlg Is Nothing Then
                psDlg.Dispose()
            End If
        End If
    End Sub

    Protected Overrides Sub Finalize()
        Try
            Dispose(False)
        Finally
            MyBase.Finalize()
        End Try
    End Sub

    'Private Sub OnPrintPage(ByVal sender As Object, ByVal e As PrintPageEventArgs)
    '    e.Graphics.DrawImage(target.ControlImage(), 0, 0)
    'End Sub

    'Private Sub OnPrintPageIntensityGraph(ByVal sender As Object, ByVal e As PrintPageEventArgs)
    '    e.Graphics.DrawImage(targetIntensityGraph.ToImage(), 0, 0)
    'End Sub

    ' можно и  так
    Private Sub OnPrintPageIntensityGraph(ByVal sender As Object, ByVal e As PrintPageEventArgs)
        targetIntensityGraph.Draw(New UI.ComponentDrawArgs(e.Graphics, targetIntensityGraph.Bounds))
    End Sub

    Private Sub PageSetup()
        Try
            If (storedPageSettings Is Nothing) Then
                storedPageSettings = New PageSettings()
            End If

            psDlg.PageSettings = storedPageSettings
            psDlg.ShowDialog()
        Catch ex As Exception
            MessageBox.Show("Произошла ошибка - " + ex.Message, "Печать графика", MessageBoxButtons.OK, MessageBoxIcon.Warning)
        End Try
    End Sub

    Private Sub PrintPreview()
        Try
            If Not (storedPageSettings Is Nothing) Then
                document.DefaultPageSettings = storedPageSettings
            End If

            dlg.Document = document
            dlg.ShowDialog()
        Catch ex As Exception
            MessageBox.Show("Произошла ошибка - " + ex.Message, "Печать графика", MessageBoxButtons.OK, MessageBoxIcon.Warning)
        End Try
    End Sub

    Public Sub Print()
        PageSetup()
        PrintPreview()

        Try
            If Not (storedPageSettings Is Nothing) Then
                document.DefaultPageSettings = storedPageSettings
            End If

            dialog.Document = document
            If dialog.ShowDialog() = DialogResult.OK Then
                document.PrinterSettings.PrinterName = dialog.PrinterSettings.PrinterName

                If document.PrinterSettings.IsValid Then
                    document.Print()
                Else
                    MessageBox.Show("Принтер не установлен.", "Печать", MessageBoxButtons.OK, MessageBoxIcon.Information)
                End If
            End If
        Catch ex As Exception
            MessageBox.Show("Произошла ошибка при печати в файл - " + ex.Message, "Печать графика", MessageBoxButtons.OK, MessageBoxIcon.Warning)
        End Try
    End Sub
End Class
