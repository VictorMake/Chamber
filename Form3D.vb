Imports System.Windows.Forms
Imports System.Drawing
Imports MathematicalLibrary.Spline3
Imports NationalInstruments.UI
Imports System.Math
Imports BaseForm
Imports MathematicalLibrary

Public Class Form3D
    ' Globals 3D отсек
    Private currentCursor As CW3DGraphLib.CWCursor3D
    Private currentPlot As CW3DGraphLib.CWPlot3D
    Private refreshing As Boolean
    Private curve As Boolean

    Private plotsList As New List(Of DataSourceDisplayMember)
    Private cursorsList As New List(Of DataSourceDisplayMember)
    Private snapModeList As New List(Of DataSourceDisplayMember)
    Private pointStyleList As New List(Of DataSourceDisplayMember)
    Private lineStyleList As New List(Of DataSourceDisplayMember)

    Private currentPlaneProjection As ProjectionXYZ
    Private Const ANGLE_359 As Integer = 359

    Private Enum ProjectionXYZ
        XYPlane
        XZPlane
        YZPlane
    End Enum

    ''' <summary>
    ''' Цвет заливки поверхности
    ''' </summary>
    Private Enum ColorFilling
        Rainbow
        Red
        Grey
    End Enum

    ''' <summary>
    ''' Растяжка оси
    ''' </summary>
    Private Enum HeightAxis
        Normal
        Expanded
    End Enum

    ''' <summary>
    ''' Вид графика 3D объёмный или плоский
    ''' </summary>
    Private Enum ViewGraphPlane
        Volume
        Flatness
    End Enum

    ''' <summary>
    ''' Поверхность температурного поля
    ''' </summary>
    Private Enum Surface
        Max = 1 'Максимальные
        Mean = 2 'Средние
        Min = 3 'Минимальные
    End Enum

    Public Sub New()
        ' This call is required by the Windows Form Designer.
        InitializeComponent()

        '' Add any initialization after the InitializeComponent() call.

        pointStyleList.Add(New DataSourceDisplayMember(0, "Нет"))              '"None"
        pointStyleList.Add(New DataSourceDisplayMember(1, "Пустой квадрат"))   '"Empty Square"
        pointStyleList.Add(New DataSourceDisplayMember(2, "Сплошной квадрат")) '"Solid Square"
        pointStyleList.Add(New DataSourceDisplayMember(3, "Звёздочка"))        '"Asterisk"
        pointStyleList.Add(New DataSourceDisplayMember(6, "Сплошной ромб"))    '"Solid Diamond"
        pointStyleList.Add(New DataSourceDisplayMember(9, "Пустой круг"))      '"Empty Circle"
        pointStyleList.Add(New DataSourceDisplayMember(10, "Сплошной круг"))   '"Solid Circle"
        pointStyleList.Add(New DataSourceDisplayMember(14, "Жирная X"))        '"Bold X"
        pointStyleList.Add(New DataSourceDisplayMember(30, "Каркас сфера"))    '"Wireframe Sphere"
        pointStyleList.Add(New DataSourceDisplayMember(31, "Сплошная сфера"))  '"Solid Sphere"
        pointStyleList.Add(New DataSourceDisplayMember(32, "Каркас куб"))      '"Wireframe Cube"
        pointStyleList.Add(New DataSourceDisplayMember(33, "Сплошной куб"))    '"Solid Cube"
        ComboPointStyle.DataSource = pointStyleList
        ComboPointStyle.ValueMember = "ItemData"
        ComboPointStyle.DisplayMember = "List"

        cursorsList.Add(New DataSourceDisplayMember(1, "Cursor 1"))
        'CursorsList.Add(New DataSourceDisplayMember(2, "Cursor 2"))
        'CursorsList.Add(New DataSourceDisplayMember(3, "Cursor 3"))
        ComboCursors.DataSource = cursorsList
        ComboCursors.ValueMember = "ItemData"
        ComboCursors.DisplayMember = "List"


        snapModeList.Add(New DataSourceDisplayMember(0, "Фиксация"))
        snapModeList.Add(New DataSourceDisplayMember(1, "Ближайшая поверхность"))
        snapModeList.Add(New DataSourceDisplayMember(2, "Закрепить на текущей"))
        ComboSnapMode.DataSource = snapModeList
        ComboSnapMode.ValueMember = "ItemData"
        ComboSnapMode.DisplayMember = "List"

        lineStyleList.Add(New DataSourceDisplayMember(0, "Нет"))              '"Нет"
        lineStyleList.Add(New DataSourceDisplayMember(1, "Сплошная"))         '"Solid"
        lineStyleList.Add(New DataSourceDisplayMember(4, "Чёрточка"))         '"Dash"
        lineStyleList.Add(New DataSourceDisplayMember(5, "Пунктир"))          '"Dot"
        lineStyleList.Add(New DataSourceDisplayMember(6, "Тире,точка"))       '"Dash Dot"
        ComBoxLineStyle.DataSource = lineStyleList
        ComBoxLineStyle.ValueMember = "ItemData"
        ComBoxLineStyle.DisplayMember = "List"

        plotsList.Add(New DataSourceDisplayMember(Surface.Max, "Максимальные"))
        plotsList.Add(New DataSourceDisplayMember(Surface.Mean, "Средние"))
        plotsList.Add(New DataSourceDisplayMember(Surface.Min, "Минимальные"))
        ComboPlotsList.DataSource = plotsList
        ComboPlotsList.ValueMember = "ItemData"
        ComboPlotsList.DisplayMember = "List"

        AddHandler ComboPlotsList.SelectedValueChanged, AddressOf ComboPlotsList_SelectedIndexChanged
        AddHandler ComboPointStyle.SelectedValueChanged, AddressOf ComboPointStyle_SelectedIndexChanged
        AddHandler ComBoxLineStyle.SelectedValueChanged, AddressOf ComboLineStyle_SelectedIndexChanged
        AddHandler ComboSnapMode.SelectedValueChanged, AddressOf ComboSnapMode_SelectedIndexChanged
        AddHandler ComboCursors.SelectedValueChanged, AddressOf ComboCursors_SelectedIndexChanged

        ' удалить неиспользуемую закладку
        'TabControlAll.TabPages.Remove(TabPage63Dграфик)
    End Sub

    Private Sub Form3D_Load(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Load
        'AxCWGraph3DEvolvent.Plots.RemoveAll()
        'AxCWGraph3DEvolvent.Plots.Add.ColorMapStyle = CW3DGraphLib.CWColorMapStyles.cwColorSpectrum
        'AxCWGraph3DEvolvent.Plots.Add.ColorMapStyle = CW3DGraphLib.CWColorMapStyles.cwShaded

        ' добавить ещё 2 поверхности
        AxCWGraph3DПроэкция.Plots.Add.Style = CW3DGraphLib.CWPlot3DStyles.cwContourLine
        AxCWGraph3DПроэкция.Plots.Add.Style = CW3DGraphLib.CWPlot3DStyles.cwContourLine

        AxCWGraph3DEvolvent.Cursors.Add()
        currentCursor = AxCWGraph3DEvolvent.Cursors.Item(1)
        currentPlot = AxCWGraph3DEvolvent.Plots.Item(Surface.Max)
        ComboCursors.SelectedIndex = 0

        AxCWGraph3DПроэкция.Plots.Item(Surface.Max).Contours.RemoveAll()
        AxCWGraph3DПроэкция.Plots.Item(Surface.Max).Contours.Add(-100)
        AxCWGraph3DПроэкция.Plots.Item(Surface.Max).Contours.Item(1).LabelFormat = "###0.0#"
        'AxCWGraph3DПроэкция.Plots.Item(1).Contours.Item(1).LineColor = System.Convert.ToUInt32(ChooseColor((System.Drawing.ColorTranslator.FromOle(System.Convert.ToInt32(Color.Gold)))))
        AxCWGraph3DПроэкция.Plots.Item(Surface.Max).Contours.Item(1).LineColor = Convert.ToUInt32(ColorTranslator.ToOle(Color.Gold))

        AxCWGraph3DПроэкция.Plots.Item(Surface.Mean).Contours.RemoveAll()
        AxCWGraph3DПроэкция.Plots.Item(Surface.Mean).Contours.Add(-100)
        AxCWGraph3DПроэкция.Plots.Item(Surface.Mean).Contours.Item(1).LabelFormat = "###0.0#"
        AxCWGraph3DПроэкция.Plots.Item(Surface.Mean).Contours.Item(1).LineColor = Convert.ToUInt32(ColorTranslator.ToOle(Color.Red))

        AxCWGraph3DПроэкция.Plots.Item(Surface.Min).Contours.RemoveAll()
        AxCWGraph3DПроэкция.Plots.Item(Surface.Min).Contours.Add(-100)
        AxCWGraph3DПроэкция.Plots.Item(Surface.Min).Contours.Item(1).LabelFormat = "###0.0#"
        AxCWGraph3DПроэкция.Plots.Item(Surface.Min).Contours.Item(1).LineColor = Convert.ToUInt32(ColorTranslator.ToOle(Color.Magenta))

        refreshing = False
        RefreshControls()

        'CType(Me.MdiParent, BaseForm.frmBase).WindowManagerPanel1.SetActiveWindow(CType(CType(Me.MdiParent, Chamber).varТемпературныеПоля, System.Windows.Forms.Form))
        'Задать Диапазон Оси
        SlideAxis.Range = New Range(0, 1500)
        SlideAxis.Value = 0
        ' Test()
    End Sub

    Private Sub Form3D_FormClosing(ByVal sender As Object, ByVal e As FormClosingEventArgs) Handles Me.FormClosing
        If Not gMainFomMdiParent.IsWindowClosed Then
            e.Cancel = True
        End If
    End Sub

    Private Class DataSourceDisplayMember
        Private myItemData As Integer
        Private myList As String

        Public Sub New(ByVal iItemData As Integer, ByVal sList As String)
            myItemData = iItemData
            myList = sList
        End Sub 'New

        Public ReadOnly Property ItemData() As Integer
            Get
                Return myItemData
            End Get
        End Property

        Public ReadOnly Property List() As String
            Get
                Return myList
            End Get
        End Property

        Public Overrides Function ToString() As String
            Return $"{ItemData.ToString} - {myList}"
        End Function
    End Class

#Region "выделить cursor / plot, Initialize / Update controls"

    Private Sub ComboCursors_SelectedIndexChanged(ByVal eventSender As Object, ByVal eventArgs As EventArgs) ' Handles ComboCursors.SelectedIndexChangedcurrentCursor.Enabled
        If Not IsHandleCreated Then Exit Sub
        If refreshing Then Exit Sub
        If ComboCursors.SelectedIndex <> -1 Then currentCursor = AxCWGraph3DEvolvent.Cursors.Item(ComboCursors.SelectedValue)

        RefreshControls()
    End Sub

    Private Sub ComboPlotsList_SelectedIndexChanged(ByVal eventSender As Object, ByVal eventArgs As EventArgs) ' Handles ComboPlotsList.SelectedIndexChanged
        If Not IsHandleCreated Then Exit Sub
        If refreshing Then Exit Sub
        'CursorCurrent.Plot = AxCWGraph3DEvolvent.Plots.Item(VB6.GetItemData(ComboPlotsList, ComboPlotsList.SelectedIndex))
        If ComboPlotsList.SelectedIndex <> -1 Then currentCursor.Plot = AxCWGraph3DEvolvent.Plots.Item(ComboPlotsList.SelectedValue)

        EnableVisibleControlsTransparent()
        RefreshControls()
    End Sub

    Private Sub AxCWGraph3DEvolvent_CursorChange(ByVal eventSender As Object, ByVal eventArgs As AxCW3DGraphLib._DCWGraph3DEvents_CursorChangeEvent) Handles AxCWGraph3DEvolvent.CursorChange
        If Not IsHandleCreated Then Exit Sub

        RefreshControls()
    End Sub

    Private Sub RefreshControls()
        refreshing = True
        CheckEnabled.CheckState = CType(BoolToInt((currentCursor.Enabled)), CheckState)
        CheckShowPosition.CheckState = CType(BoolToInt((currentCursor.PositionVisible)), CheckState)
        CheckVisible.CheckState = CType(BoolToInt((currentCursor.Visible)), CheckState)
        CheckShowName.CheckState = CType(BoolToInt((currentCursor.NameVisible)), CheckState)
        CheckXYPlane.Checked = CBool(BoolToInt((currentCursor.XYPlaneVisible)))
        CheckXZPlane.Checked = CBool(BoolToInt((currentCursor.XZPlaneVisible)))
        CheckYZPlane.Checked = CBool(BoolToInt((currentCursor.YZPlaneVisible)))

        If currentCursor.Plot IsNot Nothing Then
            If currentCursor.Plot.Name = AxCWGraph3DEvolvent.Plots.Item(Surface.Max).Name Then
                ComboPlotsList.SelectedIndex = Surface.Max - 1
            ElseIf currentCursor.Plot.Name = AxCWGraph3DEvolvent.Plots.Item(Surface.Mean).Name Then
                ComboPlotsList.SelectedIndex = Surface.Mean - 1
            ElseIf currentCursor.Plot.Name = AxCWGraph3DEvolvent.Plots.Item(Surface.Min).Name Then
                ComboPlotsList.SelectedIndex = Surface.Min - 1
            End If
        End If

        ComboSnapMode.SelectedIndex = currentCursor.SnapMode
        TextNameCursor.Text = currentCursor.Name
        TextXPosition.Text = CStr(currentCursor.XPosition)
        TextYPosition.Text = CStr(currentCursor.YPosition)
        TextZPosition.Text = CStr(currentCursor.ZPosition)
        NumericEditRow.Value = currentCursor.Row
        NumericEditColum.Value = currentCursor.Column
        SetCombo(ComboPointStyle, (currentCursor.PointStyle))
        NumericEditTextSize.Value = currentCursor.PointSize
        NumericEditTextWidth.Value = currentCursor.LineWidth
        SetCombo(ComBoxLineStyle, (currentCursor.LineStyle))
        TextTransparency.Value = currentCursor.PlaneTransparency
        NumericEditTextBackTransparency.Value = currentCursor.TextBackgroundTransparency

        currentPlot = AxCWGraph3DEvolvent.Plots.Item(ComboPlotsList.SelectedValue)

        If currentPlot IsNot Nothing Then
            Select Case currentPlot.Style
                Case CW3DGraphLib.CWPlot3DStyles.cwContourLine
                    RadioButtonContour2.Checked = True
                Case CW3DGraphLib.CWPlot3DStyles.cwHiddenLine
                    RadioButtonHiddenLine2.Checked = True
                Case CW3DGraphLib.CWPlot3DStyles.cwLine
                    RadioButtonLine2.Checked = True
                Case CW3DGraphLib.CWPlot3DStyles.cwLinePoint
                    RadioButtonLinePoint2.Checked = True
                Case CW3DGraphLib.CWPlot3DStyles.cwPoint
                    RadioButtonPoint2.Checked = True
                Case CW3DGraphLib.CWPlot3DStyles.cwSurfaceContour
                    RadioButtonSurfaceContour2.Checked = True
                Case CW3DGraphLib.CWPlot3DStyles.cwSurfaceLine
                    RadioButtonSurfaceLine2.Checked = True
                Case CW3DGraphLib.CWPlot3DStyles.cwSurfaceNormal
                    RadioButtonSurfaceNormal2.Checked = True
                Case CW3DGraphLib.CWPlot3DStyles.cwSurface
                    RadioButtonSurface2.Checked = True
            End Select
        End If

        refreshing = False

        'AxCWGraph3DПроэкция.Plots.Item(1).Contours.RemoveAll()
        'AxCWGraph3DПроэкция.Plots.Item(1).Contours.Add(1)

        If CheckEnableProjection.Checked Then
            Select Case currentPlaneProjection
                Case ProjectionXYZ.XYPlane
                    If AxCWGraph3DПроэкция.Plots.Item(Surface.Max).Contours.Item(1).Level <> currentCursor.ZPosition Then
                        AxCWGraph3DПроэкция.Plots.Item(Surface.Max).Contours.Item(1).Level = currentCursor.ZPosition
                        AxCWGraph3DПроэкция.Plots.Item(Surface.Mean).Contours.Item(1).Level = currentCursor.ZPosition
                        AxCWGraph3DПроэкция.Plots.Item(Surface.Min).Contours.Item(1).Level = currentCursor.ZPosition
                    End If
                Case ProjectionXYZ.XZPlane
                    If AxCWGraph3DПроэкция.Plots.Item(Surface.Max).Contours.Item(1).Level <> currentCursor.YPosition Then
                        AxCWGraph3DПроэкция.Plots.Item(Surface.Max).Contours.Item(1).Level = currentCursor.YPosition 'ZPosition '
                        AxCWGraph3DПроэкция.Plots.Item(Surface.Mean).Contours.Item(1).Level = currentCursor.YPosition 'ZPosition '
                        AxCWGraph3DПроэкция.Plots.Item(Surface.Min).Contours.Item(1).Level = currentCursor.YPosition 'ZPosition '
                    End If
                Case ProjectionXYZ.YZPlane
                    If AxCWGraph3DПроэкция.Plots.Item(Surface.Max).Contours.Item(1).Level <> currentCursor.XPosition Then
                        AxCWGraph3DПроэкция.Plots.Item(Surface.Max).Contours.Item(1).Level = currentCursor.XPosition 'ZPosition 'XPosition
                        AxCWGraph3DПроэкция.Plots.Item(Surface.Mean).Contours.Item(1).Level = currentCursor.XPosition 'ZPosition 'XPosition
                        AxCWGraph3DПроэкция.Plots.Item(Surface.Min).Contours.Item(1).Level = currentCursor.XPosition 'ZPosition 'XPosition
                    End If
            End Select
        End If
    End Sub

    Private Sub SetCombo(ByRef combo As ComboBox, ByRef index As Integer)
        Dim tempMember As DataSourceDisplayMember

        For I As Integer = 0 To combo.Items.Count - 1
            'If VB6.GetItemData(combo, i) = index Then
            tempMember = CType(combo.Items(I), DataSourceDisplayMember)
            If tempMember.ItemData = index Then
                combo.SelectedIndex = I
                Exit For
            End If
        Next
    End Sub
#End Region

#Region "Enumerations"
    Private Sub ComboSnapMode_SelectedIndexChanged(ByVal eventSender As Object, ByVal eventArgs As EventArgs) ' Handles ComboSnapMode.SelectedIndexChanged
        If Not IsHandleCreated Then Exit Sub
        If refreshing Then Exit Sub
        'CursorCurrent.SnapMode = VB6.GetItemData(ComboSnapMode, ComboSnapMode.SelectedIndex)
        If ComboSnapMode.SelectedIndex <> -1 Then currentCursor.SnapMode = CType(ComboSnapMode.SelectedValue, CW3DGraphLib.CWCursor3DSnapModes)

        RefreshControls()
    End Sub

    Private Sub ComboPointStyle_SelectedIndexChanged(ByVal eventSender As Object, ByVal eventArgs As EventArgs) ' Handles ComboPointStyle.SelectedIndexChanged
        If Not IsHandleCreated Then Exit Sub
        If refreshing Then Exit Sub
        'CursorCurrent.PointStyle = VB6.GetItemData(ComboPointStyle, ComboPointStyle.SelectedIndex)
        If ComboPointStyle.SelectedIndex <> -1 Then currentCursor.PointStyle = CType(ComboPointStyle.SelectedValue, CW3DGraphLib.CW3DPointStyles)
    End Sub

    Private Sub ComboLineStyle_SelectedIndexChanged(ByVal eventSender As Object, ByVal eventArgs As EventArgs) ' Handles ComboLineStyle.SelectedIndexChanged
        If Not IsHandleCreated Then Exit Sub
        If refreshing Then Exit Sub
        'CursorCurrent.LineStyle = VB6.GetItemData(ComboLineStyle, ComboLineStyle.SelectedIndex)
        If ComBoxLineStyle.SelectedIndex <> -1 Then currentCursor.LineStyle = CType(ComBoxLineStyle.SelectedValue, CW3DGraphLib.CW3DLineStyles)
    End Sub
#End Region

#Region "Визуализация"

    Private Sub CheckEnabled_CheckStateChanged(ByVal eventSender As Object, ByVal eventArgs As EventArgs) Handles CheckEnabled.CheckStateChanged
        If Not IsHandleCreated Then Exit Sub
        If refreshing Then Exit Sub

        currentCursor.Enabled = CheckEnabled.Checked
    End Sub

    Private Sub CheckShowPosition_CheckStateChanged(ByVal eventSender As Object, ByVal eventArgs As EventArgs) Handles CheckShowPosition.CheckStateChanged
        If Not IsHandleCreated Then Exit Sub
        If refreshing Then Exit Sub

        currentCursor.PositionVisible = CheckShowPosition.Checked
    End Sub

    Private Sub CheckVisible_CheckStateChanged(ByVal eventSender As Object, ByVal eventArgs As EventArgs) Handles CheckVisible.CheckStateChanged
        If Not IsHandleCreated Then Exit Sub
        If refreshing Then Exit Sub

        currentCursor.Visible = CheckVisible.Checked
    End Sub

    Private Sub CheckРазрешитьТранспонировать_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs) Handles CheckEnableProjection.CheckedChanged
        CheckXYPlane.Checked = True ' сброс
        CheckXYPlane.Checked = False
        CheckXZPlane.Checked = False
        CheckYZPlane.Checked = False
        CheckXYPlane.Enabled = CheckEnableProjection.Checked
        CheckXZPlane.Enabled = CheckEnableProjection.Checked
        CheckYZPlane.Enabled = CheckEnableProjection.Checked

        'SplitContainer3D2.Panel2Collapsed = Not CheckРазрешитьТранспонировать.Checked
        'CurrentPlaneProjection = ProjectionXYZ.XYPlane
        'AxCWGraph3DПроэкция.Plots.Item(1).Contours.Basis = CW3DGraphLib.CWBases.cwMagnitude
    End Sub

    Private Sub CheckXYPlane_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs) Handles CheckXYPlane.CheckedChanged
        If Not IsHandleCreated Then Exit Sub
        If refreshing Then Exit Sub

        currentCursor.XYPlaneVisible = CheckXYPlane.Checked
        ОтобразитьPlaneProjection(ProjectionXYZ.XYPlane, CheckXYPlane.Checked)
    End Sub

    Private Sub CheckXZPlane_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs) Handles CheckXZPlane.CheckedChanged
        If Not IsHandleCreated Then Exit Sub
        If refreshing Then Exit Sub

        currentCursor.XZPlaneVisible = CheckXZPlane.Checked
        ОтобразитьPlaneProjection(ProjectionXYZ.XZPlane, CheckXZPlane.Checked)
    End Sub

    Private Sub CheckYZPlane_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs) Handles CheckYZPlane.CheckedChanged
        If Not IsHandleCreated Then Exit Sub
        If refreshing Then Exit Sub

        currentCursor.YZPlaneVisible = CheckYZPlane.Checked
        ОтобразитьPlaneProjection(ProjectionXYZ.YZPlane, CheckYZPlane.Checked)
    End Sub

    Private Sub ОтобразитьPlaneProjection(ByVal mPlaneProjection As ProjectionXYZ, ByVal enable As Boolean)
        currentPlaneProjection = mPlaneProjection
        currentPlot = AxCWGraph3DEvolvent.Plots.Item(ComboPlotsList.SelectedValue)

        If currentPlot IsNot Nothing Then
            ' вначале выключить все проэкции
            AxCWGraph3DEvolvent.Plots.Item(Surface.Max).ProjectionXY = False
            AxCWGraph3DEvolvent.Plots.Item(Surface.Max).ProjectionXZ = False
            AxCWGraph3DEvolvent.Plots.Item(Surface.Max).ProjectionYZ = False

            AxCWGraph3DEvolvent.Plots.Item(Surface.Mean).ProjectionXY = False
            AxCWGraph3DEvolvent.Plots.Item(Surface.Mean).ProjectionXZ = False
            AxCWGraph3DEvolvent.Plots.Item(Surface.Mean).ProjectionYZ = False

            AxCWGraph3DEvolvent.Plots.Item(Surface.Min).ProjectionXY = False
            AxCWGraph3DEvolvent.Plots.Item(Surface.Min).ProjectionXZ = False
            AxCWGraph3DEvolvent.Plots.Item(Surface.Min).ProjectionYZ = False

            Select Case mPlaneProjection
                Case ProjectionXYZ.XYPlane
                    currentPlot.ProjectionXY = enable
                    AxCWGraph3DПроэкция.Plots.Item(Surface.Max).Contours.Basis = CW3DGraphLib.CWBases.cwMagnitude
                    AxCWGraph3DПроэкция.Plots.Item(Surface.Mean).Contours.Basis = CW3DGraphLib.CWBases.cwMagnitude
                    AxCWGraph3DПроэкция.Plots.Item(Surface.Min).Contours.Basis = CW3DGraphLib.CWBases.cwMagnitude
                    'AxCWGraph3DПроэкция.ViewLatitude = 0
                    'AxCWGraph3DПроэкция.ViewLongitude = -90
                    AxCWGraph3DПроэкция.ViewMode = CW3DGraphLib.CWGraph3DViewModes.cwViewXYPlane
                Case ProjectionXYZ.XZPlane
                    currentPlot.ProjectionXZ = enable
                    AxCWGraph3DПроэкция.Plots.Item(Surface.Max).Contours.Basis = CW3DGraphLib.CWBases.cwY
                    AxCWGraph3DПроэкция.Plots.Item(Surface.Mean).Contours.Basis = CW3DGraphLib.CWBases.cwY
                    AxCWGraph3DПроэкция.Plots.Item(Surface.Min).Contours.Basis = CW3DGraphLib.CWBases.cwY
                    'AxCWGraph3DПроэкция.ViewLatitude = 90
                    'AxCWGraph3DПроэкция.ViewLongitude = -90
                    AxCWGraph3DПроэкция.ViewMode = CW3DGraphLib.CWGraph3DViewModes.cwViewXZPlane
                Case ProjectionXYZ.YZPlane
                    currentPlot.ProjectionYZ = enable
                    AxCWGraph3DПроэкция.Plots.Item(Surface.Max).Contours.Basis = CW3DGraphLib.CWBases.cwX
                    AxCWGraph3DПроэкция.Plots.Item(Surface.Mean).Contours.Basis = CW3DGraphLib.CWBases.cwX
                    AxCWGraph3DПроэкция.Plots.Item(Surface.Min).Contours.Basis = CW3DGraphLib.CWBases.cwX
                    'AxCWGraph3DПроэкция.ViewLatitude = 90
                    'AxCWGraph3DПроэкция.ViewLongitude = 0
                    AxCWGraph3DПроэкция.ViewMode = CW3DGraphLib.CWGraph3DViewModes.cwViewYZPlane
            End Select
        End If
    End Sub

    Private Sub CheckFast_Draw_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs) Handles CheckBoxFastDraw.CheckedChanged
        If Not IsHandleCreated Then Exit Sub

        AxCWGraph3DEvolvent.FastDraw = CheckBoxFastDraw.Checked
        AxCWGraph3DПроэкция.FastDraw = CheckBoxFastDraw.Checked
        'If CheckFast_Draw.Checked Then
        '    AxCWGraph3DEvolvent.FastDraw = True
        'Else
        '    AxCWGraph3DEvolvent.FastDraw = False
        'End If
    End Sub

    Private Sub CheckShowName_CheckStateChanged(ByVal eventSender As Object, ByVal eventArgs As EventArgs) Handles CheckShowName.CheckStateChanged
        If Not IsHandleCreated Then Exit Sub
        If refreshing Then Exit Sub

        currentCursor.NameVisible = CheckShowName.Checked
    End Sub

    Private Sub EnableVisibleControlsTransparent()
        If Not IsHandleCreated Then Exit Sub

        AxCWGraph3DEvolvent.Plots.Item(Surface.Max).Visible = False
        AxCWGraph3DПроэкция.Plots.Item(Surface.Max).Visible = False
        AxCWGraph3DEvolvent.Plots.Item(Surface.Mean).Visible = False
        AxCWGraph3DПроэкция.Plots.Item(Surface.Mean).Visible = False
        AxCWGraph3DEvolvent.Plots.Item(Surface.Min).Visible = False
        AxCWGraph3DПроэкция.Plots.Item(Surface.Min).Visible = False
        SlideTransparentMax.Enabled = False
        SlideTransparentMean.Enabled = False
        SlideTransparentMin.Enabled = False

        If ComboPlotsList.SelectedIndex = Surface.Max - 1 Then
            AxCWGraph3DEvolvent.Plots.Item(Surface.Max).Visible = True
            AxCWGraph3DПроэкция.Plots.Item(Surface.Max).Visible = True
            SlideTransparentMax.Enabled = True
        ElseIf ComboPlotsList.SelectedIndex = Surface.Mean - 1 Then
            'ComboPlotsList.SelectedIndex = 1
            AxCWGraph3DEvolvent.Plots.Item(Surface.Mean).Visible = True
            AxCWGraph3DПроэкция.Plots.Item(Surface.Mean).Visible = True
            SlideTransparentMean.Enabled = True
        ElseIf ComboPlotsList.SelectedIndex = Surface.Min - 1 Then
            AxCWGraph3DEvolvent.Plots.Item(Surface.Min).Visible = True
            AxCWGraph3DПроэкция.Plots.Item(Surface.Min).Visible = True
            SlideTransparentMin.Enabled = True
        End If

        CheckEnableProjection.Checked = False
    End Sub

    Private Sub SlideTransparentMax_AfterChangeValue(ByVal sender As Object, ByVal e As AfterChangeNumericValueEventArgs) Handles SlideTransparentMax.AfterChangeValue
        If Not IsHandleCreated Then Exit Sub

        AxCWGraph3DEvolvent.Plots.Item(Surface.Max).Transparency = CInt(SlideTransparentMax.Value)
    End Sub

    Private Sub SlideTransparentMean_AfterChangeValue(ByVal sender As Object, ByVal e As AfterChangeNumericValueEventArgs) Handles SlideTransparentMean.AfterChangeValue
        If Not IsHandleCreated Then Exit Sub

        AxCWGraph3DEvolvent.Plots.Item(Surface.Mean).Transparency = CInt(SlideTransparentMean.Value)
    End Sub

    Private Sub SlideTransparentMin_AfterChangeValue(ByVal sender As Object, ByVal e As AfterChangeNumericValueEventArgs) Handles SlideTransparentMin.AfterChangeValue
        If Not IsHandleCreated Then Exit Sub

        AxCWGraph3DEvolvent.Plots.Item(Surface.Min).Transparency = CInt(SlideTransparentMin.Value)
    End Sub

    Private Sub SlideОсь_AfterChangeValue(ByVal sender As Object, ByVal e As AfterChangeNumericValueEventArgs) Handles SlideAxis.AfterChangeValue
        AxCWGraph3DEvolvent.Axes.Item("ZAxis").Minimum = SlideAxis.Value
    End Sub

    Private Sub PerspectiveCheck_CheckStateChanged(ByVal eventSender As Object, ByVal eventArgs As EventArgs) Handles CheckBoxPerspective.CheckStateChanged
        If Not IsHandleCreated Then Exit Sub

        If CheckBoxPerspective.Checked Then
            AxCWGraph3DEvolvent.ProjectionStyle = CW3DGraphLib.CWGraph3DProjectionStyles.cwPerspective
        Else
            AxCWGraph3DEvolvent.ProjectionStyle = CW3DGraphLib.CWGraph3DProjectionStyles.cwOrthographic
        End If
    End Sub

    Private Function BoolToInt(ByRef b As Boolean) As Integer
        If b Then
            Return 1
        Else
            Return 0
        End If
    End Function

#End Region

#Region "Colors"

    Private Sub ButtonColorPoint_Click(ByVal eventSender As Object, ByVal eventArgs As EventArgs) Handles ButtonColorPoint.Click
        Try
            currentCursor.PointColor = Convert.ToUInt32(ChooseColor((ColorTranslator.FromOle(Convert.ToInt32(currentCursor.PointColor)))))
        Catch ex As Exception
            MessageBox.Show($"Невозможно изменить цвет{Environment.NewLine}{ex.Message}", "Изменить цвет", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try
    End Sub

    Private Sub ButtonColorLine_Click(ByVal eventSender As Object, ByVal eventArgs As EventArgs) Handles ButtonColorLine.Click
        Try
            currentCursor.LineColor = Convert.ToUInt32(ChooseColor((ColorTranslator.FromOle(Convert.ToInt32(currentCursor.LineColor)))))
        Catch ex As Exception
            MessageBox.Show($"Невозможно изменить цвет{Environment.NewLine}{ex.Message}", "Изменить цвет", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try
    End Sub

    Private Sub ButtonColorPlane_Click(ByVal eventSender As Object, ByVal eventArgs As EventArgs) Handles ButtonColorPlane.Click
        Try
            currentCursor.PlaneColor = Convert.ToUInt32(ChooseColor((ColorTranslator.FromOle(Convert.ToInt32(currentCursor.PlaneColor)))))
        Catch ex As Exception
            MessageBox.Show($"Невозможно изменить цвет{Environment.NewLine}{ex.Message}", "Изменить цвет", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try
    End Sub

    Private Sub ButtonColorText_Click(ByVal eventSender As Object, ByVal eventArgs As EventArgs) Handles ButtonColorText.Click
        Try
            currentCursor.TextColor = Convert.ToUInt32(ChooseColor((ColorTranslator.FromOle(Convert.ToInt32(currentCursor.TextColor)))))
        Catch ex As Exception
            MessageBox.Show($"Невозможно изменить цвет{Environment.NewLine}{ex.Message}", "Изменить цвет", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try
    End Sub

    Private Sub ButtonSetAllColors_Click(ByVal eventSender As Object, ByVal eventArgs As EventArgs) Handles ButtonSetAllColors.Click
        Try
            currentCursor.SetColor(CUInt(ChooseColor(Color.Black)))
        Catch ex As Exception
            MessageBox.Show($"Невозможно изменить цвет{Environment.NewLine}{ex.Message}", "Изменить цвет", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try
    End Sub

    Private Sub ButtonTextBackColor_Click(ByVal eventSender As Object, ByVal eventArgs As EventArgs) Handles ButtonTextBackColor.Click
        Try
            currentCursor.TextBackColor = Convert.ToUInt32(ChooseColor((ColorTranslator.FromOle(Convert.ToInt32(currentCursor.TextBackColor)))))
        Catch ex As Exception
            MessageBox.Show($"Невозможно изменить цвет{Environment.NewLine}{ex.Message}", "Изменить цвет", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try
    End Sub

    Private Function ChooseColor(ByRef c As Color) As Object
        Try
            ' Инициализировать dialog
            ColorDialog1.Color = c
            ' Вывести Color dialog box.
            If ColorDialog1.ShowDialog() = DialogResult.OK Then
                Return ColorTranslator.ToOle(ColorDialog1.Color)
                'Else
                '    Return c
            End If

            'Return System.Drawing.ColorTranslator.ToOle(Color.Black)
            ' установить цвет курсора в выбранный цвет
        Catch ex As Exception
            MessageBox.Show($"Невозможно изменить цвет{Environment.NewLine}{ex.Message}", "Изменить цвет", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try

        Return ColorTranslator.ToOle(Color.Red)
    End Function

    ''' <summary>
    ''' Font
    ''' </summary>
    ''' <param name="eventSender"></param>
    ''' <param name="eventArgs"></param>
    ''' <remarks></remarks>
    Private Sub ButtonTextFont_Click(ByVal eventSender As Object, ByVal eventArgs As EventArgs) Handles ButtonTextFont.Click
        Try
            ' Инициализироватьdialog
            'FontDialog1.Font = VB6.FontChangeName(FontDialog1.Font, CursorCurrent.Font.Name)
            'FontDialog1.Font = VB6.FontChangeSize(FontDialog1.Font, CursorCurrent.Font.Size) 'SizeInPoints)
            'FontDialog1.Font = VB6.FontChangeBold(FontDialog1.Font, CursorCurrent.Font.Bold)
            'FontDialog1.Font = VB6.FontChangeItalic(FontDialog1.Font, CursorCurrent.Font.Italic)

            FontDialog1.Font = New Font(currentCursor.Font.Name, currentCursor.Font.Size, FontStyle.Bold Or FontStyle.Italic)

            ' Вывести Font dialog box.
            ' Установить font курсора в выбранный font.
            If FontDialog1.ShowDialog() <> DialogResult.Cancel Then
                'CursorCurrent.Font = VB6.FontToIFont(FontDialog1.Font)
                'CursorCurrent.Font.Name = VB6.FontChangeName(CursorCurrent.Font, FontDialog1.Font.Name).Name.ToString 'ToString 'Name)
                'CursorCurrent.Font.Size = VB6.FontChangeSize(CursorCurrent.Font, FontDialog1.Font.Size).Size
                'CursorCurrent.Font.Bold = VB6.FontChangeBold(CursorCurrent.Font, FontDialog1.Font.Bold).Bold
                'CursorCurrent.Font.Italic = VB6.FontChangeItalic(CursorCurrent.Font, FontDialog1.Font.Italic).Italic

                currentCursor.Font.Name = FontDialog1.Font.Name
                currentCursor.Font.Size = CDec(FontDialog1.Font.Size)
                currentCursor.Font.Bold = FontDialog1.Font.Bold
                currentCursor.Font.Italic = FontDialog1.Font.Italic
            End If
        Catch ex As Exception
            MessageBox.Show($"Невозможно изменить Font{Environment.NewLine}{ex.Message}", "Изменить цвет", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try
    End Sub

#End Region

#Region "Edit boxes"

    Private Sub TextNameCursor_TextChanged(ByVal eventSender As Object, ByVal eventArgs As EventArgs) Handles TextNameCursor.TextChanged
        If Not IsHandleCreated Then Exit Sub
        If refreshing Then Exit Sub

        currentCursor.Name = TextNameCursor.Text
    End Sub

    Private Sub TextBackTransparency_AfterChangeValue(ByVal sender As Object, ByVal e As AfterChangeNumericValueEventArgs) Handles NumericEditTextBackTransparency.AfterChangeValue
        If Not IsHandleCreated Then Exit Sub
        If refreshing Then Exit Sub
        If currentCursor Is Nothing Then Exit Sub

        currentCursor.TextBackgroundTransparency = CInt(NumericEditTextBackTransparency.Value)
    End Sub

    Private Sub TextXPosition_TextChanged(ByVal eventSender As Object, ByVal eventArgs As EventArgs) Handles TextXPosition.TextChanged
        If Not IsHandleCreated Then Exit Sub
        If refreshing Then Exit Sub

        currentCursor.XPosition = CDbl(TextXPosition.Text)
        RefreshControls()
    End Sub

    Private Sub TextYPosition_TextChanged(ByVal eventSender As Object, ByVal eventArgs As EventArgs) Handles TextYPosition.TextChanged
        If Not IsHandleCreated Then Exit Sub
        If refreshing Then Exit Sub

        currentCursor.YPosition = CDbl(TextYPosition.Text)
        RefreshControls()
    End Sub

    Private Sub TextZPosition_TextChanged(ByVal eventSender As Object, ByVal eventArgs As EventArgs) Handles TextZPosition.TextChanged
        If Not IsHandleCreated Then Exit Sub
        If refreshing Then Exit Sub

        currentCursor.ZPosition = CDbl(TextZPosition.Text)
        RefreshControls()
    End Sub

    Private Sub NumericEditRow_AfterChangeValue(ByVal sender As Object, ByVal e As AfterChangeNumericValueEventArgs) Handles NumericEditRow.AfterChangeValue
        If refreshing Then Exit Sub

        currentCursor.Row = CInt(e.NewValue)
        RefreshControls()
    End Sub

    Private Sub NumericEditColum_AfterChangeValue(ByVal sender As Object, ByVal e As AfterChangeNumericValueEventArgs) Handles NumericEditColum.AfterChangeValue
        If refreshing Then Exit Sub
        currentCursor.Column = CInt(e.NewValue)
        RefreshControls()
    End Sub

    Private Sub TextTransparency_AfterChangeValue(ByVal sender As Object, ByVal e As AfterChangeNumericValueEventArgs) Handles TextTransparency.AfterChangeValue
        If Not IsHandleCreated Then Exit Sub
        If refreshing Then Exit Sub
        If currentCursor Is Nothing Then Exit Sub

        currentCursor.PlaneTransparency = CInt(TextTransparency.Value)
    End Sub

    Private Sub TextSize_AfterChangeValue(ByVal sender As Object, ByVal e As AfterChangeNumericValueEventArgs) Handles NumericEditTextSize.AfterChangeValue
        If Not IsHandleCreated Then Exit Sub
        If refreshing Then Exit Sub
        If currentCursor Is Nothing Then Exit Sub

        currentCursor.PointSize = NumericEditTextSize.Value
    End Sub

    Private Sub TextWidth_AfterChangeValue(ByVal sender As Object, ByVal e As AfterChangeNumericValueEventArgs) Handles NumericEditTextWidth.AfterChangeValue
        If Not IsHandleCreated Then Exit Sub
        If refreshing Then Exit Sub
        If currentCursor Is Nothing Then Exit Sub

        currentCursor.LineWidth = NumericEditTextWidth.Value
    End Sub

    Private Sub RadioButtonContour2_CheckedChanged(ByVal eventSender As Object, ByVal eventArgs As EventArgs) Handles RadioButtonContour2.CheckedChanged
        If Not IsHandleCreated Then Exit Sub
        If refreshing Then Exit Sub

        If CType(eventSender, RadioButton).Checked Then
            currentPlot = AxCWGraph3DEvolvent.Plots.Item(ComboPlotsList.SelectedValue)

            If currentPlot IsNot Nothing Then
                currentPlot.Style = CW3DGraphLib.CWPlot3DStyles.cwContourLine
            End If
        End If
    End Sub

    Private Sub HiddenLineOption2_CheckedChanged(ByVal eventSender As Object, ByVal eventArgs As EventArgs) Handles RadioButtonHiddenLine2.CheckedChanged
        If Not IsHandleCreated Then Exit Sub
        If refreshing Then Exit Sub

        If CType(eventSender, RadioButton).Checked Then
            currentPlot = AxCWGraph3DEvolvent.Plots.Item(ComboPlotsList.SelectedValue)

            If currentPlot IsNot Nothing Then
                currentPlot.Style = CW3DGraphLib.CWPlot3DStyles.cwHiddenLine
            End If
        End If
    End Sub

    Private Sub LineOption2_CheckedChanged(ByVal eventSender As Object, ByVal eventArgs As EventArgs) Handles RadioButtonLine2.CheckedChanged
        If Not IsHandleCreated Then Exit Sub
        If refreshing Then Exit Sub

        If CType(eventSender, RadioButton).Checked Then
            currentPlot = AxCWGraph3DEvolvent.Plots.Item(ComboPlotsList.SelectedValue)

            If currentPlot IsNot Nothing Then
                currentPlot.Style = CW3DGraphLib.CWPlot3DStyles.cwLine
            End If
        End If
    End Sub

    Private Sub LinePointOption2_CheckedChanged(ByVal eventSender As Object, ByVal eventArgs As EventArgs) Handles RadioButtonLinePoint2.CheckedChanged
        If Not IsHandleCreated Then Exit Sub
        If refreshing Then Exit Sub

        If CType(eventSender, RadioButton).Checked Then
            currentPlot = AxCWGraph3DEvolvent.Plots.Item(ComboPlotsList.SelectedValue)

            If currentPlot IsNot Nothing Then
                currentPlot.Style = CW3DGraphLib.CWPlot3DStyles.cwLinePoint
            End If
        End If
    End Sub

    Private Sub PointOption2_CheckedChanged(ByVal eventSender As Object, ByVal eventArgs As EventArgs) Handles RadioButtonPoint2.CheckedChanged
        If Not IsHandleCreated Then Exit Sub
        If refreshing Then Exit Sub

        If CType(eventSender, RadioButton).Checked Then
            currentPlot = AxCWGraph3DEvolvent.Plots.Item(ComboPlotsList.SelectedValue)

            If currentPlot IsNot Nothing Then
                currentPlot.Style = CW3DGraphLib.CWPlot3DStyles.cwPoint
            End If
        End If
    End Sub

    Private Sub SurfaceContourOption2_CheckedChanged(ByVal eventSender As Object, ByVal eventArgs As EventArgs) Handles RadioButtonSurfaceContour2.CheckedChanged
        If Not IsHandleCreated Then Exit Sub
        If refreshing Then Exit Sub

        If CType(eventSender, RadioButton).Checked Then
            currentPlot = AxCWGraph3DEvolvent.Plots.Item(ComboPlotsList.SelectedValue)

            If currentPlot IsNot Nothing Then
                currentPlot.Style = CW3DGraphLib.CWPlot3DStyles.cwSurfaceContour
            End If
        End If
    End Sub

    Private Sub SurfaceLineOption2_CheckedChanged(ByVal eventSender As Object, ByVal eventArgs As EventArgs) Handles RadioButtonSurfaceLine2.CheckedChanged
        If Not IsHandleCreated Then Exit Sub
        If refreshing Then Exit Sub

        If CType(eventSender, RadioButton).Checked Then
            currentPlot = AxCWGraph3DEvolvent.Plots.Item(ComboPlotsList.SelectedValue)

            If currentPlot IsNot Nothing Then
                currentPlot.Style = CW3DGraphLib.CWPlot3DStyles.cwSurfaceLine
            End If
        End If
    End Sub

    Private Sub SurfaceNormalOption2_CheckedChanged(ByVal eventSender As Object, ByVal eventArgs As EventArgs) Handles RadioButtonSurfaceNormal2.CheckedChanged
        If Not IsHandleCreated Then Exit Sub
        If refreshing Then Exit Sub

        If CType(eventSender, RadioButton).Checked Then
            currentPlot = AxCWGraph3DEvolvent.Plots.Item(ComboPlotsList.SelectedValue)

            If currentPlot IsNot Nothing Then
                currentPlot.Style = CW3DGraphLib.CWPlot3DStyles.cwSurfaceNormal
            End If
        End If
    End Sub

    Private Sub SurfaceOption2_CheckedChanged(ByVal eventSender As Object, ByVal eventArgs As EventArgs) Handles RadioButtonSurface2.CheckedChanged
        If Not IsHandleCreated Then Exit Sub
        If refreshing Then Exit Sub

        If CType(eventSender, RadioButton).Checked Then
            currentPlot = AxCWGraph3DEvolvent.Plots.Item(ComboPlotsList.SelectedValue)

            If currentPlot IsNot Nothing Then
                currentPlot.Style = CW3DGraphLib.CWPlot3DStyles.cwSurface
            End If
        End If
    End Sub
#End Region

    'Private Sub Import1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Import1.Click
    '    'currentPlot = AxCWGraph3DEvolvent.Plots.Item(1)
    '    'PlotDualSine()
    '    'заполнить только средние
    '    currentPlot = AxCWGraph3DEvolvent.Plots.Item(2)
    '    PlotDualSine()
    '    'currentPlot = AxCWGraph3DEvolvent.Plots.Item(3)
    '    'PlotDualSine()

    '    'Dim f As String
    '    'f = "file:" + FileName.Text
    '    'CWDataSocket1.ConnectTo(f, cwdsReadAutoUpdate)
    '    'PlotDualSine() '.PerformClick()
    '    RefreshControls()
    'End Sub

    ''=============================================================================
    '' Plot Curve Button Handler
    ''=============================================================================
    'Private Sub PlotCurve_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles PlotCurve.Click
    '    If Not Me.IsHandleCreated Then Exit Sub

    '    Dim i As Integer
    '    ' Create curve data
    '    Dim x(40) As Double
    '    Dim y(40) As Double
    '    Dim z(40) As Double
    '    For i = 0 To 40
    '        x(i) = System.Math.Sin(i / 3.0#)
    '        y(i) = System.Math.Cos(i / 3.0#)
    '        z(i) = i
    '    Next i
    '    ' Plot the curve data
    '    AxCWGraph3DEvolvent.Plot3DCurve(x, y, z)
    '    ' Set the initial style to Line
    '    AxCWGraph3DEvolvent.Plots.Item(1).Style = CW3DGraphLib.CWPlot3DStyles.cwLine
    '    LineOption.Checked = True
    '    ' Set global flag indicating we plotted a curve plot
    '    curve = True
    '    ' Disable surface plot style Options
    '    HiddenLineOption.Enabled = False
    '    SurfaceOption.Enabled = False
    '    SurfaceLineOption.Enabled = False
    '    SurfaceNormalOption.Enabled = False
    '    ContourOption.Enabled = False
    '    SurfaceContourOption.Enabled = False
    '    SurfaceOption.Enabled = False
    'End Sub

    '=============================================================================
    ' Plot Curve Button Handler
    '=============================================================================
    'Private Sub PlotDualSine_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles PlotDualSine.Click
    'Private Sub PlotDualSine()
    '    If Not IsHandleCreated Then Exit Sub

    '    Dim смещение As Double

    '    'If Not CursorCurrent.Plot Is Nothing Then
    '    If currentPlot IsNot Nothing Then
    '        If currentPlot.Name = AxCWGraph3DРазвертка.Plots.Item(1).Name Then
    '            смещение = 0
    '        ElseIf currentPlot.Name = AxCWGraph3DРазвертка.Plots.Item(2).Name Then
    '            смещение = -0.5
    '        ElseIf currentPlot.Name = AxCWGraph3DРазвертка.Plots.Item(3).Name Then
    '            смещение = -1
    '        End If
    '    End If

    '    Dim I, J As Integer
    '    'Dim Pi As Object

    '    ' Сбросить свойства после того как поверхность была построена
    '    ResetAfterCurve()
    '    ' Create dual sine data
    '    Dim data(40, 80) As Double
    '    Dim x(40) As Double
    '    Dim y(80) As Double
    '    'Pi = 3.1415926535

    '    For I = 0 To 40
    '        x(I) = (I - 20) / 20.0# * Math.PI + смещение
    '        'y(i) = (i - 20) / 20.0# * Pi
    '    Next I

    '    For I = 0 To 80
    '        y(I) = (I - 20) / 20.0# * Math.PI + смещение
    '    Next I

    '    For I = 0 To 40
    '        For J = 0 To 80
    '            data(I, J) = Math.Sin(x(I)) * Math.Cos(y(J)) + смещение + 10
    '        Next J
    '    Next I

    '    ' рисовать двойные sine данные
    '    'Levels.Value = AxCWGraph3DПроэкция.Plots.Item(1).Contours.Levels

    '    ' было
    '    'AxCWGraph3DПроэкция.Plot3DSurface(x, y, data)

    '    If currentPlot IsNot Nothing Then
    '        If currentPlot.Name = AxCWGraph3DРазвертка.Plots.Item(1).Name Then
    '            AxCWGraph3DПроэкция.Plots.Item(Surface.Max).Plot3DSurface(x, y, data)
    '        ElseIf currentPlot.Name = AxCWGraph3DРазвертка.Plots.Item(2).Name Then
    '            AxCWGraph3DПроэкция.Plots.Item(Surface.Mean).Plot3DSurface(x, y, data)
    '        ElseIf currentPlot.Name = AxCWGraph3DРазвертка.Plots.Item(3).Name Then
    '            AxCWGraph3DПроэкция.Plots.Item(Surface.Min).Plot3DSurface(x, y, data)
    '        End If
    '    End If

    '    currentPlot.Plot3DSurface(x, y, data)
    'End Sub

    ''=============================================================================
    '' Plot Curve Button Handler
    ''=============================================================================
    'Private Sub PlotRandom_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles PlotRandom.Click
    '    If Not Me.IsHandleCreated Then Exit Sub

    '    Dim randObj As New Random
    '    Dim j As Integer
    '    Dim i As Integer
    '    ' Сбросить свойства после того как поверхность была построена
    '    ResetAfterCurve()
    '    ' Создать случайные данные
    '    Dim data(10, 10) As Double
    '    For i = 0 To 10
    '        For j = 0 To 10
    '            data(i, j) = randObj.Next 'Rnd()
    '        Next j
    '    Next i
    '    ' Plot random data
    '    AxCWGraph3DEvolvent.Plot3DSimpleSurface(data)
    'End Sub

    ''=============================================================================
    '' Plot Curve Button Handler
    ''=============================================================================
    'Private Sub PlotTorus_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles PlotTorus.Click
    '    If Not Me.IsHandleCreated Then Exit Sub

    '    Dim j As Integer
    '    Dim i As Integer
    '    'Dim Pi As Object
    '    ' Сбросить свойства после того как поверхность была построена
    '    ResetAfterCurve()
    '    ' Create torus data
    '    Dim x(40, 40) As Double
    '    Dim y(40, 40) As Double
    '    Dim z(40, 40) As Double
    '    Dim t(40) As Double
    '    'Pi = 3.1415926535
    '    For i = 0 To 40
    '        t(i) = (i - 20) / 20.0# * System.Math.PI 'Pi
    '    Next i
    '    For i = 0 To 40
    '        For j = 0 To 40
    '            x(i, j) = (System.Math.Cos(t(j)) + 3) * System.Math.Cos(t(i))
    '            y(i, j) = (System.Math.Cos(t(j)) + 3) * System.Math.Sin(t(i))
    '            z(i, j) = System.Math.Sin(t(j))
    '        Next j
    '    Next i
    '    ' Plot torus data
    '    AxCWGraph3DEvolvent.Plot3DParametricSurface(x, y, z)
    'End Sub

    Private Sub ResetAfterCurve()
        ' Если последний plot был построен, тогда сбросить некоторые свойства
        If curve Then
            ' переопределить поверхность plot опции стиля
            RadioButtonHiddenLine2.Enabled = True
            RadioButtonSurface2.Enabled = True
            RadioButtonSurfaceLine2.Enabled = True
            RadioButtonSurfaceNormal2.Enabled = True
            RadioButtonContour2.Enabled = True
            RadioButtonSurfaceContour2.Enabled = True
            RadioButtonSurface2.Enabled = True
            ' удалить plot данные
            AxCWGraph3DEvolvent.Plots.Item(Surface.Max).ClearData()
            AxCWGraph3DEvolvent.Plots.Item(Surface.Mean).ClearData()
            AxCWGraph3DEvolvent.Plots.Item(Surface.Min).ClearData()
            ' установить начальный стиль поверхность
            AxCWGraph3DEvolvent.Plots.Item(Surface.Max).Style = CW3DGraphLib.CWPlot3DStyles.cwSurface
            AxCWGraph3DEvolvent.Plots.Item(Surface.Mean).Style = CW3DGraphLib.CWPlot3DStyles.cwSurface
            AxCWGraph3DEvolvent.Plots.Item(Surface.Min).Style = CW3DGraphLib.CWPlot3DStyles.cwSurface
            RadioButtonSurface2.Checked = True
            ' вернуть карту цвета с тех пор, как она была выключена вместе с кривой
            AxCWGraph3DEvolvent.Plots.Item(Surface.Max).ColorMapStyle = CW3DGraphLib.CWColorMapStyles.cwShaded
            AxCWGraph3DEvolvent.Plots.Item(Surface.Mean).ColorMapStyle = CW3DGraphLib.CWColorMapStyles.cwShaded
            AxCWGraph3DEvolvent.Plots.Item(Surface.Min).ColorMapStyle = CW3DGraphLib.CWColorMapStyles.cwShaded
            ' очистить глобальный флаг показывающий, что кривой нет
            curve = False
        End If
    End Sub

#Region "Contour"
    ''=============================================================================
    '' Contour Property Handlers
    ''=============================================================================

    'Private Sub LevelsOption_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles LevelsOption.CheckedChanged
    '    If Not Me.IsHandleCreated Then Exit Sub
    '    ' Enable levels control
    '    Levels.Enabled = True
    '    ' Disable other controls
    '    Interval.Enabled = False
    '    AnchorEnabled.Enabled = False
    '    Anchor.Enabled = False
    '    ' Set the number of levels
    '    'Levels_Change()
    '     Levels_AfterChangeValue(Levels, New NationalInstruments.UI.AfterChangeNumericValueEventArgs(Levels.Value, Levels.Value, NationalInstruments.UI.Action.Programmatic))
    'End Sub

    'Private Sub IntervalOption_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles IntervalOption.CheckedChanged
    '    If Not Me.IsHandleCreated Then Exit Sub
    '    ' Disable levels controls
    '    Levels.Enabled = False
    '    ' Enable other controls
    '    Interval.Enabled = True
    '    AnchorEnabled.Enabled = True
    '    Anchor.Enabled = AnchorEnabled.Checked
    '    ' Set the interval
    '    'Interval_Change()
    '     Interva_AfterChangeValue(Interval, New NationalInstruments.UI.AfterChangeNumericValueEventArgs(Interval.Value, Interval.Value, NationalInstruments.UI.Action.Programmatic))
    'End Sub

    'Private Sub AnchorEnabled_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles AnchorEnabled.CheckedChanged
    '    If Not Me.IsHandleCreated Then Exit Sub
    '    ' Enable the anchor control
    '    Anchor.Enabled = AnchorEnabled.Checked
    '    ' Set the anchorEnabled property
    '    AxCWGraph3DПроэкция.Plots.Item(1).Contours.AnchorEnabled = AnchorEnabled.Checked
    'End Sub

    'Private Sub Levels_AfterChangeValue(ByVal sender As System.Object, ByVal e As NationalInstruments.UI.AfterChangeNumericValueEventArgs) Handles Levels.AfterChangeValue
    '    If Not Me.IsHandleCreated Then Exit Sub
    '    ' Set the number of contours
    '    Try
    '        AxCWGraph3DПроэкция.Plots.Item(1).Contours.Levels = Levels.Value
    '        'AxCWGraph3DПроэкция.Plots.Item(1).Contours.Basis = CW3DGraphLib.CWBases.cwZ
    '        'AxCWGraph3DПроэкция.Plots.Item(1).Contours.RemoveAll()
    '        'AxCWGraph3DПроэкция.Plots.Item(1).Contours.Add(Levels.Value)

    '        'AxCWGraph3DПроэкция.Plots.Item(1).Contours.Item(1).SetLevel(Levels.Value)
    '        'm_ContourGraph.Plots.Item(1).Contours.Add(levels);

    '    Catch ex As Exception
    '        MessageBox.Show("Невозможно установить уровень " & Environment.NewLine & ex.Message, "Изменить уровень", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
    '    End Try
    'End Sub

    'Private Sub Interva_AfterChangeValue(ByVal sender As System.Object, ByVal e As NationalInstruments.UI.AfterChangeNumericValueEventArgs) Handles Interval.AfterChangeValue
    '    If Not Me.IsHandleCreated Then Exit Sub
    '    ' Set the contour interval
    '    Try
    '        'AxCWGraph3DПроэкция.Plots.Item(1).Contours.Interval = Interval.Value

    '        'AxCWGraph3DПроэкция.Plots.Item(1).Contours.RemoveAll()
    '        'AxCWGraph3DПроэкция.Plots.Item(1).Contours.Add(Interval.Value)

    '        AxCWGraph3DПроэкция.Plots.Item(1).Contours.Item(1).LabelFormat = "###0.0#"
    '        AxCWGraph3DПроэкция.Plots.Item(1).Contours.Item(1).Level = Interval.Value
    '    Catch ex As Exception
    '        MessageBox.Show("Невозможно установить интервал " & Environment.NewLine & ex.Message, "Изменить интервал", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
    '    End Try

    'End Sub

    'Private Sub Anchor_AfterChangeValue(ByVal sender As System.Object, ByVal e As NationalInstruments.UI.AfterChangeNumericValueEventArgs) Handles Anchor.AfterChangeValue
    '    If Not Me.IsHandleCreated Then Exit Sub
    '    ' Set the anchor
    '    Try
    '        AxCWGraph3DПроэкция.Plots.Item(1).Contours.Anchor = Anchor.Value
    '    Catch ex As Exception
    '        MessageBox.Show("Невозможно установить привязку " & Environment.NewLine & ex.Message, "Изменить привязку", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
    '    End Try

    'End Sub
#End Region

    Private Sub Panel3DGraf_Resize(ByVal sender As Object, ByVal e As EventArgs) Handles Panel3DGraf.Resize
        Dim pHeight As Integer = Panel3DGraf.Height
        Dim pWidth As Integer = Panel3DGraf.Width

        If pHeight < pWidth Then
            pHeight -= 2
            pWidth = pHeight
        Else
            pWidth -= 2
            pHeight = pWidth
        End If

        CWGraph3DЦилиндр.Size = New Size(pWidth, pHeight)
    End Sub

    Private Sub MenuPrint3DCylinder_Click(ByVal sender As Object, ByVal e As EventArgs) Handles MenuPrint3DCylinder.Click
        Dim printer As GraphPrinter = New GraphPrinter(CWGraph3DЦилиндр)
        printer.Print()
    End Sub

    Private Sub MenuSave3DCylinder_Click(ByVal sender As Object, ByVal e As EventArgs) Handles MenuSave3DCylinder.Click
        Dim GraphSave As GraphSave = New GraphSave(CWGraph3DЦилиндр)
        GraphSave.Save()
    End Sub

    Private Sub MenuPrint3DEvolvent_Click(ByVal sender As Object, ByVal e As EventArgs) Handles MenuPrint3DEvolvent.Click
        Dim printer As GraphPrinter = New GraphPrinter(AxCWGraph3DEvolvent)
        printer.Print()
    End Sub

    Private Sub MenuSave3DEvolvent_Click(ByVal sender As Object, ByVal e As EventArgs) Handles MenuSave3DEvolvent.Click
        Dim GraphSave As GraphSave = New GraphSave(AxCWGraph3DEvolvent)
        GraphSave.Save()
    End Sub

    Public Sub ИнициализацияAxCWGraph3D()
        AxCWGraph3DEvolvent.Axes.Item("XAxis").Minimum = 0
        AxCWGraph3DEvolvent.Axes.Item("XAxis").Maximum = ЧИСЛО_ПРОМЕЖУТКОВ
        AxCWGraph3DEvolvent.Axes.Item("YAxis").Minimum = 0
        AxCWGraph3DEvolvent.Axes.Item("YAxis").Maximum = ШиринаМерногоУчастка
        Подготовить3D()
    End Sub

    Public Sub PlotSurface(ByVal числоТермопар As Integer,
                           ByVal числоПромежутков As Integer,
                           ByVal полнаяШиринаМерногоУчастка As Double,
                           ByVal координатыСтенокИТермопар() As Double,
                           ByVal arrSurfaceMin(,) As Double,
                           ByVal arrSurfaceMean(,) As Double,
                           ByVal arrSurfaceMax(,) As Double,
                           ByVal Tgmin As Double,
                           ByVal Tgmax As Double)

        Dim ширинаМерногоУчасткаЦелая As Integer = CInt(полнаяШиринаМерногоУчастка + 1) 'CInt(полнаяШиринаМерногоУчастка)
        Dim arrИнтерполированныйВходнойМинимальное(,) As Double = InterpolateSurface(числоТермопар, числоПромежутков, координатыСтенокИТермопар, ширинаМерногоУчасткаЦелая, arrSurfaceMin)
        Dim arrИнтерполированныйВходнойСреднее(,) As Double = InterpolateSurface(числоТермопар, числоПромежутков, координатыСтенокИТермопар, ширинаМерногоУчасткаЦелая, arrSurfaceMean)
        Dim arrИнтерполированныйВходнойМаксимальное(,) As Double = InterpolateSurface(числоТермопар, числоПромежутков, координатыСтенокИТермопар, ширинаМерногоУчасткаЦелая, arrSurfaceMax)

        Dim arrИнтерполированныйВходнойИнверсия(числоПромежутков, ширинаМерногоУчасткаЦелая) As Double ' для цилиндра А1-радиус макс, Б5 - радиус мин
        For угол As Integer = 0 To числоПромежутков
            For шаг As Integer = 0 To ширинаМерногоУчасткаЦелая
                arrИнтерполированныйВходнойИнверсия(угол, ширинаМерногоУчасткаЦелая - шаг) = arrИнтерполированныйВходнойСреднее(угол, шаг)
            Next
        Next

        Dim x(числоПромежутков) As Double
        Dim y(ширинаМерногоУчасткаЦелая) As Double

        For I As Integer = 0 To числоПромежутков
            x(I) = I
        Next

        For I As Integer = 0 To ширинаМерногоУчасткаЦелая
            y(I) = I
        Next

        AxCWGraph3DEvolvent.Axes.Item("ZAxis").Minimum = Tgmin * 0.9
        AxCWGraph3DEvolvent.Axes.Item("ZAxis").Maximum = Tgmax * 1.1
        ComboPlotsList.SelectedIndex = Surface.Mean - 1
        ' max
        AxCWGraph3DEvolvent.Plots.Item(Surface.Max).Plot3DSurface(x, y, arrИнтерполированныйВходнойМаксимальное)
        AxCWGraph3DПроэкция.Plots.Item(Surface.Max).Plot3DSurface(x, y, arrИнтерполированныйВходнойМаксимальное)
        ' mean
        AxCWGraph3DEvolvent.Plots.Item(Surface.Mean).Plot3DSurface(x, y, arrИнтерполированныйВходнойСреднее)
        AxCWGraph3DПроэкция.Plots.Item(Surface.Mean).Plot3DSurface(x, y, arrИнтерполированныйВходнойСреднее)
        ' min
        AxCWGraph3DEvolvent.Plots.Item(Surface.Min).Plot3DSurface(x, y, arrИнтерполированныйВходнойМинимальное)
        AxCWGraph3DПроэкция.Plots.Item(Surface.Min).Plot3DSurface(x, y, arrИнтерполированныйВходнойМинимальное)

        currentPlot = AxCWGraph3DEvolvent.Plots.Item(Surface.Mean)
        min = Tgmin
        max = Tgmax

        Построить3DГрафик(arrИнтерполированныйВходнойИнверсия)
        arrTemp = CType(arrИнтерполированныйВходнойИнверсия.Clone, Double(,))
    End Sub

    Private Function InterpolateSurface(числоТермопар As Integer,
                               числоПромежутков As Integer,
                               ByRef координатыСтенокИТермопар() As Double,
                               ширинаМерногоУчасткаЦелая As Integer,
                               arrSurface(,) As Double) As Double(,)

        Dim arrInputY(числоТермопар + 1) As Double ' срез температур по термопарам в сечении углового положения
        Dim arrTblКоэффициенты As Double(,) = Nothing ' таблица коэффициентов сплайн-аппроксимации
        Dim arrИнтерполированный(числоПромежутков, ширинаМерногоУчасткаЦелая) As Double

        For угол As Integer = 0 To числоПромежутков
            For J = 0 To числоТермопар + 1
                arrInputY(J) = arrSurface(J, угол)
            Next
            ' найдем таблицу коэффициентов
            ' интерполяция среза температур по термопарам в сечении углового положения
            Spline3BuildTable(UBound(координатыСтенокИТермопар) + 1, 2, координатыСтенокИТермопар, arrInputY, 0, 0, arrTblКоэффициенты)
            ' через 1 мм проходим по каждой высоте
            For шаг As Integer = 0 To ширинаМерногоУчасткаЦелая
                arrИнтерполированный(угол, шаг) = Spline3Interpolate(UBound(arrTblКоэффициенты, 2) + 1, arrTblКоэффициенты, шаг)
            Next
        Next

        Return arrИнтерполированный
    End Function

#Region "3D"
    Private Const R As Double = 0.01751 ' при 0.01751 бублик состыкован, если меньше бублик разрывается, если больше наезжает
    Private Const R2 As Double = 2 '1.84' при 1.7 внутренний радиус доходит до центра, при 2.0 внутренний радиус доходит до внешнего радиуса

    'Private arrТекущиеКоординаты(5) As Double
    ''*****************************
    Private cursor_Renamed As CW3DGraphLib.CWCursor3D
    'Private Const ШАГПОЯСА As Double = 40 '50.5
    'Private Const MaxValueCountWork As Integer = 365
    'Private OldValue As Integer 'для сохранения счетчика выполнения
    Private первыйВход As Boolean

    ''**************************
    'Private XX() As Double
    'Private YY() As Double
    'Private M() As Double
    'Const ШиринаМерногоУчастка3D As Short = 21 '203/10+1  'ШиринаМерногоУчастка +1 для стенки
    Private ширинаМерногоУчастка3D As Integer

    Private xMatrix As Double(,)
    Private yMatrix As Double(,)
    Private zMatrix As Double(,)
    'Private RangeПоле(ЧислоОтсечек, 5) As Double

    Private min As Double
    Private max As Double
    'Private RangeXdiag(ЧислоТермопар) As Double

    'Private Const RADIUSMAX As Double = 40 '406.6 / 10
    'Private RADIUSMIN As Double ' = 20 '204.6 / 10

    Private Sub CheckBoxNumber_CheckStateChanged(ByVal eventSender As Object, ByVal eventArgs As EventArgs) Handles CheckBoxNumber.CheckStateChanged
        With CWGraph3DЦилиндр
            For I As Integer = 1 To ЧИСЛО_ГОРЕЛОК
                .Cursors.Item(I).Visible = CBool(CheckBoxNumber.CheckState)
                .Cursors.Item(I).NameVisible = CBool(CheckBoxNumber.CheckState)
            Next
        End With
    End Sub

    Private arrTemp As Double(,) = Nothing

    Private Sub Построить3DГрафик(ByVal inputMatrix(,) As Double)
        'zMatrix = NationalInstruments.Analysis.Math.LinearAlgebra.Transpose(ВходнойМассив)
        'arrTemp = ВходнойМассив.Clone
        zMatrix = CType(inputMatrix.Clone, Double(,))
        'dblМин = Double.MaxValue
        'dblMax = Double.MinValue
        'For I = 0 To 359
        '    For J = 1 To ШиринаМерногоУчастка3D - 1
        '        If zMatrix(I, J) < dblМин Then dblМин = zMatrix(I, J)
        '        If zMatrix(I, J) > dblMax Then dblMax = zMatrix(I, J)
        '    Next J
        'Next 

        If первыйВход Then
            If RadioButtonViewVolume.Checked Then
                ViewGraphPlaneChange(ViewGraphPlane.Volume)
            ElseIf RadioButtonViewFlatness.Checked Then
                ViewGraphPlaneChange(ViewGraphPlane.Flatness)
            End If
        End If

        If RadioButtonRainbow.Checked Then
            FillGraphColorMapStyle(ColorFilling.Rainbow)
        ElseIf RadioButtonRed.Checked Then
            FillGraphColorMapStyle(ColorFilling.Red)
        ElseIf RadioButtonNullColor.Checked Then
            FillGraphColorMapStyle(ColorFilling.Grey)
        End If

        If RadioButtonNormal.Checked Then
            HeightAxisChange(HeightAxis.Normal)
        ElseIf RadioButtonExpanded.Checked Then
            HeightAxisChange(HeightAxis.Expanded)
        End If
        'ProgressBarВыполнение.Visible = False
    End Sub

    'Private Sub Построить3DГрафик(ByVal ВходнойМассив(,) As Double)
    '    zMatrix = Math.LinearAlgebra.Transpose(ВходнойМассив)

    '    Dim IndexЦвет, IndexВид, IndexВысота As Integer
    '    Dim J, I, N As Integer
    '    Dim intПроцент As Integer
    '    Dim Градус As Integer
    '    Dim Dia As Integer
    '    Dim DiagMin(360, ЧислоГорелок) As Double
    '    'отладка
    '    'For I = 0 To 144
    '    '    arrПоле(0, I) = 100 + I
    '    '    arrПоле(1, I) = 200 + I
    '    '    arrПоле(2, I) = 300 + I
    '    '    arrПоле(3, I) = 400 + I
    '    '    arrПоле(4, I) = 500 + I
    '    'Next
    '    'перезапись поля с учетом обратного хода поясов
    '    'For I = 1 To ЧислоПромежутков
    '    '    For J = 1 To 5
    '    '        'RangeПоле(I, J) = arrПоле(6 - J - 1, I - 1) '+ rnd.Next(500, 900) 'убрать
    '    '        RangeПоле(I, J) = arrПоле(J - 1, ЧислоОтсечек - I - 1) '+ rnd.Next(500, 900) 'убрать
    '    '    Next J
    '    'Next I

    '    ''вначале во временный массив с учетом разрыва вперед на 12
    '    'Dim arrTemp(4, ЧислоПромежутков) As Double
    '    'For I = 0 To 4
    '    '    For J = 133 To ЧислоПромежутков
    '    '        arrTemp(I, J - 133) = arrПоле(I, J)
    '    '    Next J
    '    'Next I
    '    'For I = 0 To 4
    '    '    For J = 0 To 132
    '    '        arrTemp(I, J + 12) = arrПоле(I, J)
    '    '    Next J
    '    'Next I

    '    ''перезапись поля с учетом обратного хода турели
    '    'For I = 0 To ЧислоПромежутков
    '    '    For J = 0 To 4
    '    '        RangeПоле(I + 1, J + 1) = arrTemp(J, ЧислоПромежутков - I)
    '    '    Next J
    '    'Next I

    '    'копировать
    '    For I = 0 To ЧислоПромежутков
    '        For J = 0 To 4
    '            RangeПоле(I + 1, J + 1) = arrПоле(J, I)
    '        Next J
    '    Next I


    '    N = ЧислоОтсечек 'виктор
    '    'для горелки 24-345 град для вводимой 25-360 град
    '    'копировать с 1 горелки
    '    'For J = 1 To 5
    '    '    RangeПоле(N, J) = RangeПоле(1, J)
    '    'Next J

    '    'вычисления для диаграммы
    '    intПроцент = 0
    '    ProgressBarВыполнение.Visible = True
    '     ПроцентВыполнения(intПроцент)

    '    ReDim_XX(N)
    '    ReDim_YY(N)
    '    ReDim_M(N)
    '    'текущие координаты горелок
    '    For I = 1 To N - 1
    '        XX(I) = 360 * (I - 1) / (N - 1)
    '    Next I
    '    XX(N) = 360

    '    For J = 1 To 5
    '        'подготовка 1 размерности
    '        For I = 1 To N
    '            YY(I) = RangeПоле(I, J)
    '        Next I
    '         ВводАппроксимация(N, XX, YY, M)
    '        'во временный массив
    '        For Градус = 1 To 360
    '            DiagMin(Градус, J) = Аппроксимация(N, XX, YY, M, Градус)
    '        Next Градус
    '        intПроцент = intПроцент + 1
    '         ПроцентВыполнения(intПроцент)
    '    Next J
    '    'на лист
    '    N = 5
    '    ReDim_XX(N)
    '    ReDim_YY(N)
    '    ReDim_M(N)
    '    For J = 1 To N
    '        XX(J) = RangeXdiag(J)
    '    Next J
    '    'подготовка 2 размерности
    '    For I = 1 To 360
    '        For J = 1 To N
    '            YY(J) = DiagMin(I, J)
    '        Next J
    '         ВводАппроксимация(N, XX, YY, M)
    '        For Dia = 1 To ШиринаМерногоУчастка3D - 1
    '            zMatrix(I - 1, Dia) = Аппроксимация(N, XX, YY, M, RADIUSMIN + Dia - 1)
    '        Next Dia
    '        intПроцент = intПроцент + 1
    '         ПроцентВыполнения(intПроцент)
    '    Next I

    '    dblМин = Double.MaxValue
    '    dblMax = Double.MinValue
    '    For I = 0 To 359
    '        For J = 1 To ШиринаМерногоУчастка3D - 1
    '            If zMatrix(I, J) < dblМин Then dblМин = zMatrix(I, J)
    '            If zMatrix(I, J) > dblMax Then dblMax = zMatrix(I, J)
    '        Next J
    '    Next I

    '    If blnПервыйВход Then
    '        If optВид0.Checked Then
    '            IndexВид = 0
    '        ElseIf optВид1.Checked Then
    '            IndexВид = 1
    '        End If
    '    End If

    '    If optЦвет0.Checked Then
    '        IndexЦвет = 0
    '    ElseIf optЦвет1.Checked Then
    '        IndexЦвет = 1
    '    ElseIf optЦвет2.Checked Then
    '        IndexЦвет = 2
    '    End If

    '    If optВысота0.Checked Then
    '        IndexВысота = 0
    '    ElseIf optВысота1.Checked Then
    '        IndexВысота = 1
    '    End If

    '    If blnПервыйВход Then  optВидCheckedChanged(IndexВид)

    '     optЦветCheckedChanged(IndexЦвет)
    '     optВысотаCheckedChanged(IndexВысота)
    '    ProgressBarВыполнение.Visible = False
    'End Sub

    'Private Sub ПроцентВыполнения(ByVal СчетчикТекущегоЗначения As Integer)
    '    Dim NewValue As Integer = Int(Round((СчетчикТекущегоЗначения * 100 / MaxValueCountWork), 0))
    '    If OldValue <> NewValue Then
    '        ProgressBarВыполнение.Value = NewValue
    '        OldValue = NewValue
    '        ProgressBarВыполнение.Caption = NewValue & "%"
    '    End If
    'End Sub

    Private Sub CheckBoxShell_CheckStateChanged(ByVal eventSender As Object, ByVal eventArgs As EventArgs) Handles CheckBoxShell.CheckStateChanged
        If RadioButtonNormal.Checked Then
            HeightAxisChange(HeightAxis.Normal)
        ElseIf RadioButtonExpanded.Checked Then
            HeightAxisChange(HeightAxis.Expanded)
        End If
    End Sub

    Private Sub ОтметитьГорелки()
        Dim indexВысота As Integer
        Dim I, J As Integer

        If RadioButtonNormal.Checked Then
            indexВысота = 0
        ElseIf RadioButtonExpanded.Checked Then
            indexВысота = 1
        End If

        Dim градусовВГорелке As Double = 360 / ЧИСЛО_ГОРЕЛОК
        Dim конецСектора As Integer

        If CBool(CheckBoxMarking.CheckState) Then ' показать разметку
            For I = 0 To ЧИСЛО_ГОРЕЛОК - 1
                конецСектора = CInt(Round(I * градусовВГорелке))

                For J = 0 To ширинаМерногоУчастка3D
                    zMatrix(конецСектора, J) = min
                Next J
            Next

            CWGraph3DЦилиндр.Plot3DParametricSurface(xMatrix, yMatrix, zMatrix)
        Else
            If arrTemp IsNot Nothing Then
                Построить3DГрафик(arrTemp)
            End If
        End If
    End Sub

    Private Sub CheckBoxMarking_CheckStateChanged(ByVal eventSender As Object, ByVal eventArgs As EventArgs) Handles CheckBoxMarking.CheckStateChanged
        ОтметитьГорелки()
    End Sub

    Private Sub ComboBoxBurner_SelectedIndexChanged(ByVal eventSender As Object, ByVal eventArgs As EventArgs) Handles ComboBoxBurner.SelectedIndexChanged
        If arrTemp IsNot Nothing Then
            Построить3DГрафик(arrTemp)
        End If
    End Sub

    Private Sub Подготовить3D()
        ширинаМерногоУчастка3D = CInt(ШиринаМерногоУчастка + 1)
        'ReDim_xMatrix(ANGLE_359, ширинаМерногоУчастка3D)
        'ReDim_yMatrix(ANGLE_359, ширинаМерногоУчастка3D)
        Re.Dim(xMatrix, ANGLE_359, ширинаМерногоУчастка3D)
        Re.Dim(yMatrix, ANGLE_359, ширинаМерногоУчастка3D)
        'RADIUSMIN = RADIUSMAX - ШиринаМерногоУчастка

        Dim градусовВГорелке As Double = 360 / ЧИСЛО_ГОРЕЛОК
        Dim I, J As Integer
        'ProgressBarВыполнение.Range = New Range(0, 100)

        For I = 0 To ANGLE_359
            For J = 0 To ширинаМерногоУчастка3D
                yMatrix(I, J) = Math.Cos(J * R + R2) * Math.Cos(I * R + R2)
                xMatrix(I, J) = Math.Cos(J * R + R2) * Math.Sin(I * R + R2)
            Next J
        Next I
        '****************************
        '    Randomize
        '    Dim K As Integer
        '    For K = 1 To 4
        '        For I = 1 To 24
        '            For J = 1 To 5
        '                garrСредние(I, J) = Rnd
        '            Next J
        '        Next I
        '    Next K
        'Dim sum As Single
        'sum = 0
        'For I = 1 To 5
        '    arrТекущиеКоординаты(I) = sum
        '    sum = sum + CSng(ШАГПОЯСА) / 10
        'Next I
        ''*************************
        ''считывани текущих координат и нахождение радиусов А1 - самый большой радиус
        'For I = 1 To ЧислоТермопар
        '    RangeXdiag((ЧислоТермопар + 1) - I) = RADIUSMAX - arrТекущиеКоординаты(I)
        'Next I

        J = ЧИСЛО_ГОРЕЛОК '12

        With CWGraph3DЦилиндр
            .Cursors.RemoveAll()

            For I = 0 To ЧИСЛО_ГОРЕЛОК - 1
                .Cursors.Add()
                cursor_Renamed = CWGraph3DЦилиндр.Cursors.Item(I + 1)
                cursor_Renamed.LineStyle = CW3DGraphLib.CW3DLineStyles.cwLine3DNone
                cursor_Renamed.SnapMode = CW3DGraphLib.CWCursor3DSnapModes.cwSnapFixed

                If I = 0 Then
                    cursor_Renamed.Name = ЧИСЛО_ГОРЕЛОК.ToString '"12"  ' & "гребенка"
                Else
                    cursor_Renamed.Name = CStr(ЧИСЛО_ГОРЕЛОК - I) ' & "гребенка"
                End If

                cursor_Renamed.XPosition = xMatrix(CInt(J * градусовВГорелке - градусовВГорелке / 2), ширинаМерногоУчастка3D)
                cursor_Renamed.YPosition = yMatrix(CInt(J * градусовВГорелке - градусовВГорелке / 2), ширинаМерногоУчастка3D)
                '            cursor.ZPosition
                cursor_Renamed.PositionVisible = False
                cursor_Renamed.Visible = False
                cursor_Renamed.NameVisible = False
                J -= 1
            Next I
        End With

        ComboBoxBurner.Items.Clear()
        ComboBoxBurner.Items.Add("Все")

        For I = 1 To ЧИСЛО_ГОРЕЛОК
            ComboBoxBurner.Items.Add("Горелка " & I)
        Next I

        первыйВход = True
        ComboBoxBurner.SelectedIndex = 0 'там  Построить3DГрафик()
        первыйВход = False
    End Sub

    Private Sub ОставитьОднуГорелку()
        Dim index As Integer = ComboBoxBurner.SelectedIndex
        Dim градусовВГорелке As Integer = 360 \ ЧИСЛО_ГОРЕЛОК

        If index = 0 Then Exit Sub
        'Select Case Index
        '    Case 12
        '         Очистка(0, 330)
        '    Case Else
        '         Очистка(0, (12 - Index - 1) * Горелка)
        '         Очистка(((12 - Index) * Горелка) - 1, Angle)
        'End Select
        Select Case index
            Case ЧИСЛО_ГОРЕЛОК
                Очистка(0, 360 - градусовВГорелке)
            Case Else
                Очистка(0, (index - 1) * градусовВГорелке)
                Очистка(((index) * градусовВГорелке) - 1, ANGLE_359)
        End Select
    End Sub

    Private Sub Очистка(ByRef начало As Integer, ByRef конец As Integer)
        For I As Integer = начало To конец
            For J As Integer = 0 To ширинаМерногоУчастка3D
                zMatrix(I, J) = min
            Next
        Next
    End Sub

    Private Sub ContourOption_CheckedChanged(ByVal eventSender As Object, ByVal eventArgs As EventArgs) Handles RadioButtonContour.CheckedChanged
        If IsHandleCreated AndAlso CType(eventSender, RadioButton).Checked Then
            CWGraph3DЦилиндр.Plots.Item(1).Style = CW3DGraphLib.CWPlot3DStyles.cwContourLine
        End If
    End Sub

    Private Sub HiddenLineOption_CheckedChanged(ByVal eventSender As Object, ByVal eventArgs As EventArgs) Handles RadioButtonHiddenLine.CheckedChanged
        If IsHandleCreated AndAlso CType(eventSender, RadioButton).Checked Then
            CWGraph3DЦилиндр.Plots.Item(1).Style = CW3DGraphLib.CWPlot3DStyles.cwHiddenLine
        End If
    End Sub

    Private Sub LineOption_CheckedChanged(ByVal eventSender As Object, ByVal eventArgs As EventArgs) Handles RadioButtonLine.CheckedChanged
        If IsHandleCreated AndAlso CType(eventSender, RadioButton).Checked Then
            CWGraph3DЦилиндр.Plots.Item(1).Style = CW3DGraphLib.CWPlot3DStyles.cwLine
        End If
    End Sub

    Private Sub LinePointOption_CheckedChanged(ByVal eventSender As Object, ByVal eventArgs As EventArgs) Handles RadioButtonLinePoint.CheckedChanged
        If IsHandleCreated AndAlso CType(eventSender, RadioButton).Checked Then
            CWGraph3DЦилиндр.Plots.Item(1).Style = CW3DGraphLib.CWPlot3DStyles.cwLinePoint
        End If
    End Sub

    Private Sub PointOption_CheckedChanged(ByVal eventSender As Object, ByVal eventArgs As EventArgs) Handles RadioButtonPoint.CheckedChanged
        If IsHandleCreated AndAlso CType(eventSender, RadioButton).Checked Then
            CWGraph3DЦилиндр.Plots.Item(1).Style = CW3DGraphLib.CWPlot3DStyles.cwPoint
        End If
    End Sub

    Private Sub SurfaceContourOption_CheckedChanged(ByVal eventSender As Object, ByVal eventArgs As EventArgs) Handles RadioButtonSurfaceContour.CheckedChanged
        If IsHandleCreated AndAlso CType(eventSender, RadioButton).Checked Then
            CWGraph3DЦилиндр.Plots.Item(1).Style = CW3DGraphLib.CWPlot3DStyles.cwSurfaceContour
        End If
    End Sub

    Private Sub SurfaceLineOption_CheckedChanged(ByVal eventSender As Object, ByVal eventArgs As EventArgs) Handles RadioButtonSurfaceLine.CheckedChanged
        If IsHandleCreated AndAlso CType(eventSender, RadioButton).Checked Then
            CWGraph3DЦилиндр.Plots.Item(1).Style = CW3DGraphLib.CWPlot3DStyles.cwSurfaceLine
        End If
    End Sub

    Private Sub SurfaceNormalOption_CheckedChanged(ByVal eventSender As Object, ByVal eventArgs As EventArgs) Handles RadioButtonSurfaceNormal.CheckedChanged
        If IsHandleCreated AndAlso CType(eventSender, RadioButton).Checked Then
            CWGraph3DЦилиндр.Plots.Item(1).Style = CW3DGraphLib.CWPlot3DStyles.cwSurfaceNormal
        End If
    End Sub

    Private Sub SurfaceOption_CheckedChanged(ByVal eventSender As Object, ByVal eventArgs As EventArgs) Handles RadioButtonSurface.CheckedChanged
        If IsHandleCreated AndAlso CType(eventSender, RadioButton).Checked Then
            CWGraph3DЦилиндр.Plots.Item(1).Style = CW3DGraphLib.CWPlot3DStyles.cwSurface
        End If
    End Sub

    Private Sub RadioButtonRainbow_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs) Handles RadioButtonRainbow.CheckedChanged
        FillGraphColorMapStyle(ColorFilling.Rainbow)
    End Sub
    Private Sub RadioButtonRed_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs) Handles RadioButtonRed.CheckedChanged
        FillGraphColorMapStyle(ColorFilling.Red)
    End Sub
    Private Sub RadioButtonNullColor_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs) Handles RadioButtonNullColor.CheckedChanged
        FillGraphColorMapStyle(ColorFilling.Grey)
    End Sub
    Private Sub FillGraphColorMapStyle(inColor As ColorFilling)
        If IsHandleCreated Then
            Select Case inColor
                Case ColorFilling.Rainbow
                    CWGraph3DЦилиндр.Plots.Item(1).ColorMapStyle = CW3DGraphLib.CWColorMapStyles.cwColorSpectrum
                    Exit Select
                Case ColorFilling.Red
                    CWGraph3DЦилиндр.Plots.Item(1).ColorMapStyle = CW3DGraphLib.CWColorMapStyles.cwShaded
                    Exit Select
                Case ColorFilling.Grey
                    CWGraph3DЦилиндр.Plots.Item(1).ColorMapStyle = CW3DGraphLib.CWColorMapStyles.cwGrayScale
                    Exit Select
            End Select
        End If
    End Sub

    Private Sub RadioButtonNormal_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs) Handles RadioButtonNormal.CheckedChanged
        HeightAxisChange(HeightAxis.Normal)
    End Sub
    Private Sub RadioButtonExpanded_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs) Handles RadioButtonExpanded.CheckedChanged
        HeightAxisChange(HeightAxis.Expanded)
    End Sub
    Private Sub HeightAxisChange(inHeight As HeightAxis)
        ' Index=0 означает показать все
        If IsHandleCreated Then
            Dim I, J As Integer

            With CWGraph3DЦилиндр
                For I = 0 To ANGLE_359
                    If inHeight = HeightAxis.Normal Then
                        If CBool(CheckBoxShell.CheckState) Then
                            zMatrix(I, 0) = 0
                            zMatrix(I, ширинаМерногоУчастка3D) = 0
                        Else
                            zMatrix(I, 0) = zMatrix(I, 1)
                            zMatrix(I, ширинаМерногоУчастка3D) = zMatrix(I, ширинаМерногоУчастка3D - 1)
                        End If

                        For J = 1 To ЧИСЛО_ГОРЕЛОК
                            .Cursors.Item(J).ZPosition = 0
                        Next J
                    Else
                        If CBool(CheckBoxShell.CheckState) Then
                            zMatrix(I, 0) = min
                            zMatrix(I, ширинаМерногоУчастка3D) = min
                        Else
                            zMatrix(I, 0) = zMatrix(I, 1)
                            zMatrix(I, ширинаМерногоУчастка3D) = zMatrix(I, ширинаМерногоУчастка3D - 1)
                        End If

                        For J = 1 To ЧИСЛО_ГОРЕЛОК
                            .Cursors.Item(J).ZPosition = min
                        Next J
                    End If
                Next I

                If inHeight = HeightAxis.Normal Then
                    .Axes.Item(3).Minimum = 0
                Else
                    .Axes.Item(3).Minimum = Int(min)
                End If

                .Axes.Item(3).Maximum = Int(max)
                ОставитьОднуГорелку()
                If CBool(CheckBoxMarking.CheckState) Then ОтметитьГорелки()
                .Plot3DParametricSurface(xMatrix, yMatrix, zMatrix)
            End With
        End If
    End Sub

    Private Sub RadioButtonViewVolume_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs) Handles RadioButtonViewVolume.CheckedChanged
        ViewGraphPlaneChange(ViewGraphPlane.Volume)
    End Sub
    Private Sub RadioButtonViewFlatness_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs) Handles RadioButtonViewFlatness.CheckedChanged
        ViewGraphPlaneChange(ViewGraphPlane.Flatness)
    End Sub
    Private Sub ViewGraphPlaneChange(inViewGraph As ViewGraphPlane)
        If IsHandleCreated Then
            If inViewGraph = ViewGraphPlane.Volume Then
                CWGraph3DЦилиндр.ViewLatitude = 45
                CWGraph3DЦилиндр.ViewLongitude = 45
            Else
                CWGraph3DЦилиндр.ViewLatitude = 0
                CWGraph3DЦилиндр.ViewLongitude = 0
            End If
        End If
    End Sub

#End Region

End Class

'Private Sub Test()
'     ИнициализацияAxCWGraph3D()
'    Dim I As Integer
'    Dim КоординатыТермопар As Double() = {0, 3, 6, 9, 12, 15, 18, 21, 24, 27, 30, 45.5}
'    Dim arrSurface(ЧислоТермопар + 1, ЧислоПромежутков) As Double
'    'For I = 0 To ЧислоТермопар
'    For I = 0 To ЧислоПромежутков
'        'RangeПоле(I, J) = arrПоле(6 - J - 1, I - 1) '+ rnd.Next(500, 900) 'убрать
'        'arrSurface(I, J) = arrSurface(J - 1, ЧислоОтсечек - I - 1) '+ rnd.Next(500, 900) 'убрать

'        arrSurface(0, I) = 900
'        arrSurface(1, I) = 1000
'        arrSurface(2, I) = 1100
'        arrSurface(3, I) = 1200
'        arrSurface(4, I) = 1300
'        arrSurface(5, I) = 1400
'        arrSurface(6, I) = 1400
'        arrSurface(7, I) = 1300
'        arrSurface(8, I) = 1200
'        arrSurface(9, I) = 1100
'        arrSurface(10, I) = 1000
'        arrSurface(11, I) = 900

'    Next I
'    'Next I
'    arrSurface(1, (360 / 28) * 2 + ((360 / 28) / 2)) = 500 'a1 середина 3 горелки
'    arrSurface(10, 359) = 500 'Б5 последняя точка

'    PlotSurface(10, 360, 45.5, КоординатыТермопар, arrSurface, 500, 1500)

'End Sub

'Private Sub PlotRandom_Click()
'    ' Reset properties changed after a curve was plotted
'    ResetAfterCurve()
'    ' Create random data
'    Dim data(10, 10)
'    For i = 0 To 10
'        For j = 0 To 10
'            data(i, j) = Rnd()
'        Next j
'    Next i
'    ' Plot random data
'    CWGraph3D1.Plot3DSimpleSurface(data)
'End Sub