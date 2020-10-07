Imports System.Data.OleDb
Imports System.Windows.Forms

Public Class FormGuideBaseChamber
    Public ReadOnly Property KeyId() As Integer
        Get
            Return mKeyId
        End Get
    End Property

    Private mKeyId As Integer
    Private Const Fields As String = "Поля"
    Private Const SQL As String = "SELECT * FROM Fields ORDER BY KeyId"
    ' Для работы с таблицей испытаний    
    Private dataSetTableFields As New DataSet
    Private dataTableFields As DataTable

    Private Sub GuideBaseChamberForm_Load(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Load
        Using cn As New OleDbConnection(BuildCnnStr(PROVIDER_JET, gPathFieldsChamber))
            cn.Open()
            Dim dataAdapterFields As New OleDbDataAdapter(SQL, cn)
            dataAdapterFields.Fill(dataSetTableFields, Fields)
            dataTableFields = dataSetTableFields.Tables(Fields)
            ' связывание
            BindingSourceTableFields.DataSource = dataTableFields
            With DataGridFields
                .DataSource = BindingSourceTableFields
                .Columns("KeyId").Visible = False
                .Columns("НомерИзделия").HeaderText = $"№{Environment.NewLine}Изделия"
                .Columns("ВремяНачалаСбора").HeaderText = $"Время{Environment.NewLine}запуска"
                .Columns("ВремяХодаТурели").HeaderText = $"Длительность{Environment.NewLine}замера"
            End With

            If dataTableFields.Rows.Count > 0 Then ShowCurrentRowOnTable()
        End Using
    End Sub

    Private Sub ButtonApply_Click(ByVal sender As Object, ByVal e As EventArgs) Handles ButtonApply.Click
        ' здесь определить KeyId
        If BindingSourceTableFields.Count > 0 Then
            mKeyId = Convert.ToInt32(DataGridFields.Rows(BindingSourceTableFields.Position).Cells("KeyId").Value)
        End If
        Close()
    End Sub

    Private Sub ButtonCancel_Click(ByVal sender As Object, ByVal e As EventArgs) Handles ButtonCancel.Click
        Close()
    End Sub

    ''' <summary>
    ''' Обновить позицию текущей записи
    ''' </summary>
    Private Sub ShowCurrentRowOnTable()
        ButtonRowMoveFirst.Enabled = True
        ButtonRowMovePrevious.Enabled = True
        ButtonRowMoveNext.Enabled = True
        ButtonRowMoveLast.Enabled = True

        If BindingSourceTableFields.Count = 0 Then
            LabelDescriptionFields.Text = "Нет записей"
            LabelRowPosition.Text = "Нет записей"
            ButtonRowMoveFirst.Enabled = False
            ButtonRowMovePrevious.Enabled = False
            ButtonRowMoveNext.Enabled = False
            ButtonRowMoveLast.Enabled = False
        Else
            LabelDescriptionFields.Text = $"Снятые температурные поля (запись {BindingSourceTableFields.Position + 1} из {BindingSourceTableFields.Count})"

            If BindingSourceTableFields.Position - 1 < 0 Then
                ButtonRowMoveFirst.Enabled = False
                ButtonRowMovePrevious.Enabled = False
            ElseIf BindingSourceTableFields.Position + 1 > BindingSourceTableFields.Count - 1 Then
                ButtonRowMoveNext.Enabled = False
                ButtonRowMoveLast.Enabled = False
            End If

            LabelRowPosition.Text = $"Запись {BindingSourceTableFields.Position + 1} из {BindingSourceTableFields.Count}"
        End If
    End Sub

    Private Sub DataGridFields_SelectionChanged(sender As Object, e As EventArgs) Handles DataGridFields.SelectionChanged
        If BindingSourceTableFields.DataSource Is Nothing Then Exit Sub ' убрать при срабатывании, когда источник данных равен Nothing
    End Sub

    Private Sub DataGridChamberFields_Click(ByVal sender As Object, ByVal e As EventArgs) Handles DataGridFields.Click
        ' отрабатывать щелчок только когда одна строка
        If BindingSourceTableFields.Count = 1 Then
            Try
                DataGridFields.Rows(BindingSourceTableFields.Position).Selected = True
            Catch ex As Exception
                MessageBox.Show(ex.ToString, "Навигация", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            End Try
        End If
    End Sub

    Private Sub BindingSourceFoundedSnapshotDataTable_PositionChanged(sender As Object, e As EventArgs) Handles BindingSourceTableFields.PositionChanged
        ShowCurrentRowOnTable()
    End Sub

#Region "Navigation"
    Private Sub ButtonRowMoveFirst_Click(ByVal sender As Object, ByVal e As EventArgs) Handles ButtonRowMoveFirst.Click
        FirstRecord()
    End Sub
    Private Sub ButtonRowMovePrevious_Click(ByVal sender As Object, ByVal e As EventArgs) Handles ButtonRowMovePrevious.Click
        PreviousRecord()
    End Sub
    Private Sub ButtonRowMoveNext_Click(ByVal sender As Object, ByVal e As EventArgs) Handles ButtonRowMoveNext.Click
        NextRecord()
    End Sub
    Private Sub ButtonRowMoveLast_Click(ByVal sender As Object, ByVal e As EventArgs) Handles ButtonRowMoveLast.Click
        LastRecord()
    End Sub

    Private Sub FirstRecord()
        If BindingSourceTableFields.Count > 0 AndAlso BindingSourceTableFields.Position <> 0 Then
            BindingSourceTableFields.MoveFirst()
            ShowCurrentRowOnTable()
        End If
    End Sub

    Private Sub PreviousRecord()
        If BindingSourceTableFields.Count > 0 AndAlso BindingSourceTableFields.Position > 0 Then
            BindingSourceTableFields.MovePrevious()
            ShowCurrentRowOnTable()
        End If
    End Sub

    Private Sub NextRecord()
        If BindingSourceTableFields.Count > 0 AndAlso BindingSourceTableFields.Position + 1 < BindingSourceTableFields.Count Then
            BindingSourceTableFields.MoveNext()
            ShowCurrentRowOnTable()
        End If
    End Sub

    Private Sub LastRecord()
        If BindingSourceTableFields.Count > 0 AndAlso BindingSourceTableFields.Position + 1 <> BindingSourceTableFields.Count Then
            BindingSourceTableFields.MoveLast()
            BindingSourceTableFields.Position = BindingSourceTableFields.Count - 1
            ShowCurrentRowOnTable()
        End If
    End Sub
#End Region

#Region "UpdateTable"
    Private Sub ButtonRowDelete_Click(ByVal sender As Object, ByVal e As EventArgs) Handles ButtonRowDelete.Click
        If BindingSourceTableFields.Count > 0 Then
            If MessageBox.Show("Вы уверены в удалении записи?", "Удаление записи", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) = Windows.Forms.DialogResult.Yes Then
                dataTableFields.Rows(BindingSourceTableFields.Position).Delete()
                UpdateTable()
                ShowCurrentRowOnTable()
            End If
        Else
            MessageBox.Show("Нет текущих записей для удаления!", "Удаление записи", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End If
    End Sub

    ''' <summary>
    ''' Обновить Базу
    ''' </summary>
    Private Sub UpdateTable()
        If dataSetTableFields.HasChanges Then
            Using cn As New OleDbConnection(BuildCnnStr(PROVIDER_JET, gPathFieldsChamber))
                cn.Open()
                Dim dataAdapterFields As New OleDbDataAdapter(SQL, cn)
                Dim txn As OleDbTransaction
                Try
                    '1. StartTransaction
                    Dim myDataRowsCommandBuilder As OleDbCommandBuilder = New OleDbCommandBuilder(dataAdapterFields)

                    dataAdapterFields.UpdateCommand = myDataRowsCommandBuilder.GetUpdateCommand
                    dataAdapterFields.InsertCommand = myDataRowsCommandBuilder.GetInsertCommand
                    dataAdapterFields.DeleteCommand = myDataRowsCommandBuilder.GetDeleteCommand
                    txn = cn.BeginTransaction
                    dataAdapterFields.UpdateCommand.Transaction = txn
                    dataAdapterFields.InsertCommand.Transaction = txn
                    dataAdapterFields.DeleteCommand.Transaction = txn

                    Dim countRowModified As Integer = dataAdapterFields.Update(dataSetTableFields.Tables(Fields).Select("", "", DataViewRowState.Added Or DataViewRowState.ModifiedCurrent))

                    'dataAdapterFields.Update(dataSetTableFields, Fields)
                    'dataSetTableFields.Tables(Fields).AcceptChanges()
                    'dataAdapterFields.DeleteCommand.Connection.Close()
                    BindingContext(dataTableFields).EndCurrentEdit()
                    countRowModified += dataAdapterFields.Update(dataSetTableFields.Tables(Fields).Select("", "", DataViewRowState.Deleted))
                    '2. CommitTransaction
                    txn.Commit()
                    MessageBox.Show($"Изменено {countRowModified} записей",
                                "Обновление успешно!", MessageBoxButtons.OK, MessageBoxIcon.Information)
                Catch ex As Exception
                    '3. RollbackTransaction
                    txn.Rollback()
                    MessageBox.Show(ex.Message, "Обновление не успешно!", MessageBoxButtons.OK, MessageBoxIcon.Error)
                End Try
            End Using
        Else
            MessageBox.Show("Нет изменений для записи!", "Запись изменений", MessageBoxButtons.OK, MessageBoxIcon.Information)
        End If
    End Sub
#End Region
End Class