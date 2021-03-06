﻿Imports FFXIMemory
Imports RadarControls
Imports Contracts.Shared

Public Class AlphaMappedRadarForm
    Inherits LayeredForm

#Region " MEMBER VARIABLES "
    Private _shiftDown As Boolean
    Private _altDown As Boolean
    Private WithEvents _ifd As IniFixDialog
    Private _filterPanelShown As Boolean
    Private _loading As Boolean = True
    Private _isLoadingSettings As Boolean = True
    Private _initialStyle As Integer
    Private _mapRoot As String
    Private Property Mobs As MobList
#End Region

#Region " PROPERTIES "
    Private WithEvents _filterPanel As FilterForm
    Private ReadOnly Property FilterPanel() As FilterForm
        Get
            If _filterPanel Is Nothing OrElse _filterPanel.IsDisposed Then
                _filterPanel = New FilterForm()
                AddHandler _filterPanel.MouseWheel, AddressOf MapRadar_MouseWheel
                AddHandler _filterPanel.KeyDown, AddressOf MapRadar_KeyDown
                AddHandler _filterPanel.KeyUp, AddressOf AlphaMappedRadarForm_KeyUp
            End If
            Return _filterPanel
        End Get
    End Property

    Private WithEvents _selectColorForm As SelectColorForm
    Private ReadOnly Property SelectColorDialog As SelectColorForm
        Get
            If _selectColorForm Is Nothing OrElse _selectColorForm.IsDisposed Then
                _selectColorForm = New SelectColorForm("Mapped Radar")
            End If
            _selectColorForm.BaseSettings = MapRadar.Settings
            Return _selectColorForm
        End Get
    End Property

    Private _proEnabled As Boolean
    Public Property ProEnabled() As Boolean
        Get
            Return _proEnabled
        End Get
        Set(ByVal value As Boolean)
            _proEnabled = value
            cShowAll.Enabled = value
            cShowID.Enabled = value
            xShowCampedMobs.Enabled = value
            'xLinkedRadars.Enabled = value
            xTrackVNM.Enabled = value
            xShowPedometer.Enabled = value
            If value = False Then
                xShowCampedMobs.Checked = False
            End If
            MapRadar.ProEnabled = value
        End Set
    End Property

    Private WithEvents _nmListEditor As NMListEditor
    Private ReadOnly Property NMEditor() As NMListEditor
        Get
            If _nmListEditor Is Nothing OrElse _nmListEditor.IsDisposed Then
                _nmListEditor = New NMListEditor
            End If
            Return _nmListEditor
        End Get
    End Property

    Private WithEvents _lrf As LinkedRadarsForm
    Private ReadOnly Property LRF() As LinkedRadarsForm
        Get
            If _lrf Is Nothing OrElse _lrf.IsDisposed Then
                _lrf = New LinkedRadarsForm
            End If
            Return _lrf
        End Get
    End Property

    Private WithEvents _watcher As Watcher
    Private ReadOnly Property MobWatcher As Watcher
        Get
            If _watcher Is Nothing Then
                _watcher = New Watcher(MemoryScanner.WatcherTypes.MobList Or MemoryScanner.WatcherTypes.ZoneChange Or MemoryScanner.WatcherTypes.VNM)
            End If
            Return _watcher
        End Get
    End Property

#End Region

#Region " CONSTRUCTORS "
    Public Sub New()
        InitializeComponent()
    End Sub
#End Region

#Region " FORM ACTIONS "

    Private Sub AlphaMappedRadarForm_Activated(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Activated
        If _filterPanelShown Then
            PostMessage(FilterPanel.Handle, &H6, 1, Nothing)
        End If

    End Sub

    Private Sub MappedRadarForm_FormClosing(ByVal sender As Object, ByVal e As FormClosingEventArgs) Handles Me.FormClosing
        If _filterPanelShown Then
            FilterPanel.Close()
        End If
        If My.Settings.AutoSaveRadarSettings Then
            MapRadar.SaveSettings()
        End If
        MapRadar.ResignWatcher()
    End Sub

    Private Sub MappedRadarForm_KeyDown(ByVal sender As Object, ByVal e As KeyEventArgs) Handles Me.KeyDown
        If e.KeyCode = Keys.Up AndAlso e.Shift Then
            e.Handled = True
        ElseIf e.KeyCode = Keys.Down AndAlso e.Shift Then
            e.Handled = True
        End If
    End Sub

    Private Sub MappedRadarForm_Load(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Load
        cmMapRadar.Renderer = New Office2007Renderer
        cShowID.Enabled = GlobalSettings.IsProEnabled
        cShowAll.Enabled = GlobalSettings.IsProEnabled
        xShowCampedMobs.Enabled = GlobalSettings.IsProEnabled
        xLinkedRadars.Enabled = GlobalSettings.IsProEnabled
        xTrackVNM.Enabled = GlobalSettings.IsProEnabled
        xShowPedometer.Enabled = GlobalSettings.IsProEnabled

        MapRadar.ProEnabled = GlobalSettings.IsProEnabled

        MapRadar.InitializeRadar(Me)

        MapRadar.LoadSettings()
        BindSettings()

        If MapRadar.Settings.ShowFilterPanel Then
            cShowFilterPanel.PerformClick()
        End If
        MemoryScanner.Scanner.AttachWatcher(MobWatcher)

        ActiveTimer.Start()

        _mapRoot = MapRadar.MapPath
        LoadMapPacks()
        _loading = False
    End Sub

    Private Sub MappedRadarForm_Move(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Move
        MapRadar.Settings.Location = Location
        If ChildForm IsNot Nothing Then
            ChildForm.Top = Bottom
            ChildForm.Left = Left
        End If
    End Sub

    Private Sub AlphaMappedRadarForm_Paint(ByVal sender As Object, ByVal e As PaintEventArgs) Handles Me.Paint
        If Not _loading Then
            If Mobs Is Nothing Then
                Mobs = New MobList
            End If
            MapRadar.PaintRadar(e.Graphics, Mobs)
        End If
    End Sub

    Private Sub MappedRadarForm_Resize(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Resize
        If _filterPanelShown Then
            FilterPanel.Top = Bottom
            FilterPanel.Width = Width
        End If
        If MapRadar.MapZoomedSize <> Size.Empty Then
            If Width > MapRadar.MapZoomedSize.Width Then
                Width = MapRadar.MapZoomedSize.Width
            End If
            If Height > MapRadar.MapZoomedSize.Height Then
                Height = MapRadar.MapZoomedSize.Height
            End If
        End If
        MapRadar.Settings.Size = Size
    End Sub

    Private Sub AlphaMappedRadarForm_KeyUp(ByVal sender As Object, ByVal e As KeyEventArgs) Handles Me.KeyUp
        If e.KeyCode = Keys.F AndAlso e.Control AndAlso e.Shift Then
            cShowFilterPanel.PerformClick()
        End If
    End Sub
#End Region

#Region " OVERRIDES "
    Protected Overrides ReadOnly Property CreateParams() As CreateParams
        Get
            Dim cp As CreateParams = MyBase.CreateParams
            If My.Settings.HideAltTab Then
                cp.ExStyle = cp.ExStyle Or &H80
            End If
            Return cp
        End Get
    End Property
#End Region

#Region " CONTROLS "

    Private Sub cRingOnly_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs) Handles cRingOnly.CheckedChanged
        If Not _isLoadingSettings Then
            If cRingOnly.Checked Then
                MapRadar.Settings.RangeDisplay = RadarControls.RadarSettings.RangeType.Ring
            Else
                MapRadar.Settings.RangeDisplay = RadarControls.RadarSettings.RangeType.Solid
            End If
        End If
    End Sub

    Private Sub cVisible_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs) Handles cVisible.CheckedChanged
        If Not _isLoadingSettings Then
            MapRadar.Settings.ShowVisibleRange = cVisible.Checked
        End If
    End Sub

    Private Sub xLinkedRadars_Click(ByVal sender As Object, ByVal e As EventArgs) Handles xLinkedRadars.Click
        LRF.Show()
    End Sub

    Private Sub cMapPackSelect_Click(ByVal sender As Object, ByVal e As EventArgs) Handles cMapPackSelect.Click
        Dim tsi As ToolStripMenuItem = TryCast(sender, ToolStripMenuItem)
        If Not tsi Is Nothing Then
            MapRadar.MapPath = tsi.Tag
            tsi.Checked = True
            For i As Integer = 0 To cSelectPack.DropDownItems.Count - 1 Step 1
                If Not CType(cSelectPack.DropDownItems(i), ToolStripMenuItem) Is tsi Then
                    CType(cSelectPack.DropDownItems(i), ToolStripMenuItem).Checked = False
                End If
            Next
        End If

    End Sub

    Private Sub xShowCampedMobs_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs) Handles xShowCampedMobs.CheckedChanged
        MapRadar.Settings.ShowCampedMobs = xShowCampedMobs.Checked
    End Sub

    Private Sub MapRadar_KeyDown(ByVal sender As Object, ByVal e As KeyEventArgs) Handles Me.KeyDown
        If e.KeyCode = Keys.ShiftKey Then
            _shiftDown = True
        ElseIf e.KeyCode = Keys.Menu Then
            _altDown = True
        End If
        If e.KeyCode = Keys.PageUp Then
            If e.Modifiers = Keys.Shift Then
                MapRadar.Settings.Zoom += 1
            Else
                MapRadar.Settings.Zoom += 0.25
            End If
        ElseIf e.KeyCode = Keys.PageDown Then
            Dim zoom As Single = MapRadar.Settings.Zoom
            If e.Modifiers = Keys.Shift Then
                zoom -= 1
            Else
                zoom -= 0.25
            End If

            If zoom < 1 Then zoom = 1
            MapRadar.Settings.Zoom = zoom
        ElseIf e.KeyCode = Keys.Home Then
            MapRadar.Settings.Zoom = 1
        End If
    End Sub



    Private Sub MapRadar_MouseWheel(ByVal sender As Object, ByVal e As MouseEventArgs) Handles Me.MouseWheel
        If _shiftDown Then
            Dim zoomLevel As Single = MapRadar.Settings.Zoom
            zoomLevel += e.Delta / 360
            If zoomLevel > 30 Then
                zoomLevel = 30
            ElseIf zoomLevel < 1 Then
                zoomLevel = 1
            End If
            MapRadar.Settings.Zoom = zoomLevel
        End If
    End Sub

    Private Sub ApExit_Click(ByVal sender As Object, ByVal e As EventArgs) Handles ApExit.Click
        Close()
    End Sub

    'Private Sub xAdjustMap_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
    '    If Not IniFixDialog.Visible Then
    '        IniFixDialog.Left = Left + 20 '(Width / 2) - (IniFixDialog.Width / 2)
    '        IniFixDialog.Top = Top + 20 'Bottom - 20 - IniFixDialog.Height
    '        IniFixDialog.xModifier.Value = MapRadar.CurrentMapEntry.IniData.XModifier
    '        IniFixDialog.yModifier.Value = MapRadar.CurrentMapEntry.IniData.YModifier
    '        IniFixDialog.Show(Me)
    '    End If
    'End Sub

    Private Sub cShowNPC_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs) Handles cShowNPC.CheckedChanged
        If Not _isLoadingSettings Then
            MapRadar.Settings.ShowNPC = cShowNPC.Checked
        End If
    End Sub

    Private Sub cShowMobs_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs) Handles cShowMobs.CheckedChanged
        If Not _isLoadingSettings Then
            MapRadar.Settings.ShowMobs = cShowMobs.Checked
        End If
    End Sub

    Private Sub cShowNPCNames_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs) Handles cShowNPCNames.CheckedChanged
        If Not _isLoadingSettings Then
            MapRadar.Settings.ShowNPCNames = cShowNPCNames.Checked
        End If
    End Sub

    Private Sub cShowPC_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs) Handles cShowPC.CheckedChanged
        If Not _isLoadingSettings Then
            MapRadar.Settings.ShowPC = cShowPC.Checked
        End If
    End Sub

    Private Sub cShowPCNames_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs) Handles cShowPCNames.CheckedChanged
        If Not _isLoadingSettings Then
            MapRadar.Settings.ShowPCNames = cShowPCNames.Checked
        End If
    End Sub

    Private Sub cDistance_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs) Handles cDistance.CheckedChanged
        If Not _isLoadingSettings Then
            MapRadar.Settings.ShowDistance = cDistance.Checked
        End If
    End Sub

    Private Sub cShowHP_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs) Handles cShowHP.CheckedChanged
        If Not _isLoadingSettings Then
            MapRadar.Settings.ShowHP = cShowHP.Checked
        End If
    End Sub

    Private Sub cSetFont_Click(ByVal sender As Object, ByVal e As EventArgs) Handles cSetFont.Click
        Using fd As New FontDialog() With {.Font = MapRadar.Font}
            Try
                If fd.ShowDialog = Windows.Forms.DialogResult.OK Then
                    MapRadar.Font = fd.Font
                End If
            Catch ex As Exception
                MapRadar.Font = Font
                'Do nothing
            End Try
        End Using
    End Sub

    Private Sub xSaveSettings_Click(ByVal sender As Object, ByVal e As EventArgs) Handles xSaveSettings.Click
        MapRadar.SaveSettings()
    End Sub

    Private Sub xSaveSettingsAs_Click(ByVal sender As Object, ByVal e As EventArgs) Handles xSaveSettingsAs.Click
        Using sfd As New SaveFileDialog() With {.Filter = "Xml Settings File|*.xml"}
            If sfd.ShowDialog = Windows.Forms.DialogResult.OK Then
                MapRadar.SaveSettings(sfd.FileName)
            End If
        End Using
    End Sub

    Private Sub xLoadSettings_Click(ByVal sender As Object, ByVal e As EventArgs) Handles xLoadSettings.Click
        Using ofd As New OpenFileDialog() With {.Filter = "Xml Settings File|*.xml"}
            If ofd.ShowDialog = Windows.Forms.DialogResult.OK Then
                MapRadar.LoadSettings(ofd.FileName)
            End If
        End Using
    End Sub

    Private Sub cShowFilterPanel_Click(ByVal sender As Object, ByVal e As EventArgs) Handles cShowFilterPanel.Click
        If _filterPanelShown Then

            FilterPanel.Close()
            ChildForm = Nothing
            _filterPanelShown = False
            MapRadar.Settings.ShowFilterPanel = False

            Focus()
        Else
            MapRadar.Settings.ShowFilterPanel = True
            _filterPanelShown = True
            FilterPanel.Location = New Point(Left, Bottom)
            FilterPanel.Width = Width
            FilterPanel.NPCFilterType = MapRadar.Settings.NPCFilterType
            FilterPanel.NPCFilter = MapRadar.Settings.NPCFilter
            FilterPanel.PCFilterType = MapRadar.Settings.PCFilterType
            FilterPanel.PCFilter = MapRadar.Settings.PCFilter
            FilterPanel.TopMost = TopMost
            FilterPanel.Opacity = MapRadar.Settings.MapOpacity
            FilterPanel.Show()
            ChildForm = FilterPanel
            Focus()
        End If
    End Sub

    Private Sub cShowSettingSdesigner_Click(ByVal sender As Object, ByVal e As EventArgs) Handles cShowSettingsDesigner.Click
        MapRadar.ShowSettings()
    End Sub

    Private Sub cShowPOS_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs) Handles cShowPOS.CheckedChanged
        If Not _isLoadingSettings Then
            MapRadar.Settings.ShowPOS = cShowPOS.Checked
        End If
    End Sub

    Private Sub transparency_Click(ByVal sender As Object, ByVal e As EventArgs) Handles p100.Click, p90.Click, p80.Click, p70.Click, p60.Click,
    p50.Click, p40.Click, p30.Click, p20.Click, p10.Click

        Dim Opacity As Single = CSng(CType(sender, ToolStripItem).Tag) / 100.0F
        If _filterPanelShown Then
            _filterPanel.Opacity = Opacity
        End If
        If Layered Then
            MapRadar.Settings.MapOpacity = Opacity
            LayerOpacity = Opacity
        Else
            Opacity = Opacity
        End If

    End Sub

    Private Sub cShowAll_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs) Handles cShowAll.CheckedChanged
        If Not _isLoadingSettings Then
            MapRadar.Settings.ShowAll = cShowAll.Checked
        End If
    End Sub

    Private Sub cShowID_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs) Handles cShowID.CheckedChanged
        If Not _isLoadingSettings Then
            MapRadar.Settings.ShowId = cShowID.Checked
        End If
    End Sub

    Private Sub cOnTop_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs) Handles cOnTop.CheckedChanged
        TopMost = cOnTop.Checked
        If _filterPanelShown Then
            FilterPanel.TopMost = TopMost
        End If
        If Not _isLoadingSettings Then
            MapRadar.Settings.StayOnTop = cOnTop.Checked
        End If
    End Sub

    Private Sub ShowHide_Click(ByVal sender As Object, ByVal e As EventArgs) Handles ShowHide.Click
        If ShowHide.Text = "Hide Mapped Radar" Then
            Hide()
            If _filterPanelShown Then
                FilterPanel.Hide()
            End If
            ShowHide.Text = "Show Mapped Radar"
        Else
            Show()
            If _filterPanelShown Then
                FilterPanel.Show()
            End If
            ShowHide.Text = "Hide Mapped Radar"
        End If
    End Sub

    Private Sub cResetPosition_Click(ByVal sender As Object, ByVal e As EventArgs) Handles cResetPosition.Click
        Size = New Size(512, 512)
        Location = New Point(100, 100)
    End Sub

    Private Sub cSpellCasting_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs) Handles cSpellCasting.CheckedChanged
        If Not _isLoadingSettings Then
            MapRadar.Settings.ShowSpell = cSpellCasting.Checked
        End If
    End Sub

    Private Sub cAggro_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs) Handles cAggro.CheckedChanged
        If Not _isLoadingSettings Then
            MapRadar.Settings.ShowAggro = cAggro.Checked
        End If
    End Sub

    Private Sub cJobAbility_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs) Handles cJobAbility.CheckedChanged
        If Not _isLoadingSettings Then
            MapRadar.Settings.ShowJobAbility = cJobAbility.Checked
        End If
    End Sub


    Private Sub cMapPackSelect_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs) Handles cMapPackSelect.CheckedChanged
        If Not _isLoadingSettings Then
            MapRadar.Settings.ShowVisibleRange = cVisible.Checked
        End If
    End Sub

    Private Sub cCustomRanges_Click(ByVal sender As Object, ByVal e As EventArgs) Handles cCustomRanges.Click
        Using rf As New RangesForm(MapRadar.Settings.CustomRanges)
            If rf.ShowDialog = Windows.Forms.DialogResult.OK Then
                MapRadar.Settings.CustomRanges = rf.Ranges.ToArray
            End If
        End Using
    End Sub

    Private Sub cShowParty_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs) Handles cShowParty.CheckedChanged
        If Not _isLoadingSettings Then
            MapRadar.Settings.ShowPartyMembers = cShowParty.Checked
        End If
    End Sub

    Private Sub cClickThrough_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs) Handles cClickThrough.CheckedChanged
        SetClickThrough(cClickThrough.Checked)
        If Not _isLoadingSettings Then
            MapRadar.Settings.ClickThrough = cClickThrough.Checked
        End If
    End Sub

    Private Sub cDocking_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs) Handles cDocking.CheckedChanged
        If Not _isLoadingSettings Then
            MapRadar.Settings.DisableDocking = cDocking.Checked
        End If
        Dockable = Not cDocking.Checked
    End Sub

    Private Sub cDragging_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs) Handles cDragging.CheckedChanged
        If Not _isLoadingSettings Then
            MapRadar.Settings.DisableDragging = cDragging.Checked
        End If
        Draggable = Not cDragging.Checked
    End Sub

    Private Sub cEditNMList_Click(ByVal sender As Object, ByVal e As EventArgs) Handles cEditNMList.Click
        NMEditor.NMList = MapRadar.NMList
        NMEditor.Show()
    End Sub

    Private Sub cSetColors_Click(ByVal sender As Object, ByVal e As EventArgs) Handles cSetColors.Click
        SelectColorDialog.Show()
    End Sub

    Private Sub cAlwaysShowTarget_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs) Handles cAlwaysShowTarget.CheckedChanged
        If Not _isLoadingSettings Then
            MapRadar.Settings.AlwaysShowTarget = cAlwaysShowTarget.Checked
        End If
    End Sub

    Private Sub chideObjectsAndDoors_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs) Handles chideObjectsAndDoors.CheckedChanged
        If Not _isLoadingSettings Then
            MapRadar.Settings.HideObjectsOrDoors = chideObjectsAndDoors.Checked
        End If
    End Sub

    Private Sub UseOldRadarMethodToolStripMenuItem_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs) Handles UseOldRadarMethodToolStripMenuItem.CheckedChanged
        If UseOldRadarMethodToolStripMenuItem.Checked Then
            MapRadar.Settings.MapOpacity = 1
            Layered = False
        Else
            Opacity = 1
            Layered = True
        End If
    End Sub

    Private Sub xTrackVNM_CheckedChanged(sender As Object, e As EventArgs) Handles xTrackVNM.CheckedChanged
        MapRadar.Settings.TrackVNM = xTrackVNM.Checked
    End Sub

    Private Sub xShowPedometer_CheckedChanged(sender As Object, e As EventArgs) Handles xShowPedometer.CheckedChanged
        MapRadar.Settings.ShowPedometer = xShowPedometer.Checked
    End Sub

    Private Sub ActiveTimer_Tick(ByVal sender As Object, ByVal e As EventArgs) Handles ActiveTimer.Tick
        Try
            If MapRadar.Settings.StayOnTop Then
                If MemoryScanner.Scanner.IsGameLoaded Then
                    If GetForegroundWindow() = MemoryScanner.Scanner.FFXI.POL.MainWindowHandle Then
                        If Not TopMost Then
                            TopMost = True
                            BringToFront()
                            If _filterPanelShown Then
                                FilterPanel.TopMost = True
                                FilterPanel.BringToFront()
                            End If
                        End If
                    Else
                        If TopMost Then
                            TopMost = False
                            If _filterPanelShown Then
                                FilterPanel.TopMost = False
                            End If
                        End If
                    End If
                End If
            End If
        Catch
            'Do nothing
        End Try
    End Sub
#End Region

#Region " EVENT HANDLERS "

    Private Sub MapRadar_NewMobList(ByVal Mobs As Contracts.Shared.MobData()) Handles MapRadar.NewMobList
        If Not LRF Is Nothing AndAlso Not LRF.IsDisposed Then
            LRF.SendMessage(New LinkEventArgs() With {.Type = LinkEventType.MobList, .Zone = MapRadar.Settings.CurrentMap, .Mobs = Mobs})
        End If
    End Sub

    Private Sub _ifd_XModifierChanged(ByVal value As Single) Handles _ifd.XModifierChanged
        MapRadar.CurrentMapEntry.IniData.XModifier = value
    End Sub

    Private Sub _ifd_YModifierChanged(ByVal value As Single) Handles _ifd.YModifierChanged
        MapRadar.CurrentMapEntry.IniData.YModifier = value
    End Sub

    Private Sub _ifd_SaveChanges() Handles _ifd.SaveChanges
        MapRadar.SaveIniEntry()
    End Sub

    Private Sub NMListChanged() Handles _nmListEditor.ListChanged
        MapRadar.NMList = NMEditor.NMList
    End Sub

    Private Sub xHideFloors_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs) Handles xHideFloors.CheckedChanged
        If Not _isLoadingSettings Then
            MapRadar.Settings.HideOtherFloors = xHideFloors.Checked
        End If
    End Sub

    Private Sub SelectColorForm_DataSaved(ByVal sender As SelectColorForm) Handles _selectColorForm.DataSaved
        MapRadar.Settings.NPCColor = sender.NPCColor
        MapRadar.Settings.MobColor = sender.MobColor
        MapRadar.Settings.NMColor = sender.NMColor
        MapRadar.Settings.CampedColor = sender.CampedColor
        MapRadar.Settings.LinkColor = sender.LinkColor
        MapRadar.Settings.PCColor = sender.PCColor
        MapRadar.Settings.PartyColor = sender.PartyColor
        MapRadar.Settings.AllianceColor = sender.AllianceColor
        MapRadar.Settings.TargetHighlightColor = sender.TargetHighlight
    End Sub
#End Region

#Region " PRIVATE METHODS "
    Private Sub BindSettings()
        _isLoadingSettings = True
        Dim rs As RadarControls.RadarSettings = MapRadar.Settings
        If GlobalSettings.IsProEnabled Then
            cShowAll.Checked = rs.ShowAll
            cShowID.Checked = rs.ShowId
            xShowCampedMobs.Checked = rs.ShowCampedMobs
        End If
        cShowHP.Checked = rs.ShowHP
        cShowNPC.Checked = rs.ShowNPC
        cShowMobs.Checked = rs.ShowMobs
        chideObjectsAndDoors.Checked = rs.HideObjectsOrDoors
        cShowNPCNames.Checked = rs.ShowNPCNames
        cShowPC.Checked = rs.ShowPC
        cShowPCNames.Checked = rs.ShowPCNames
        cShowParty.Checked = rs.ShowPartyMembers
        cShowPOS.Checked = rs.ShowPOS
        cAlwaysShowTarget.Checked = rs.AlwaysShowTarget
        xHideFloors.Checked = rs.HideOtherFloors
        Location = rs.Location
        cAggro.Checked = rs.ShowAggro
        cJobAbility.Checked = rs.ShowJobAbility
        cSpellCasting.Checked = rs.ShowSpell
        cVisible.Checked = rs.ShowVisibleRange
        Size = rs.Size
        cOnTop.Checked = rs.StayOnTop
        cDragging.Checked = rs.DisableDragging
        cDocking.Checked = rs.DisableDocking
        cClickThrough.Checked = rs.ClickThrough
        cRingOnly.Checked = (rs.RangeDisplay = RadarControls.RadarSettings.RangeType.Ring)
        xTrackVNM.Checked = rs.TrackVNM
        xShowPedometer.Checked = rs.ShowPedometer
        LayerOpacity = rs.MapOpacity

        _isLoadingSettings = False
    End Sub

    Private Sub SetClickThrough(ByVal isClickThrough As Boolean)
        If isClickThrough Then
            _initialStyle = GetWindowLong(Handle, GWL.ExStyle)
            SetWindowLong(Handle, GWL.ExStyle, _initialStyle Or WS_EX.Layered Or WS_EX.Transparent)
        Else
            SetWindowLong(Handle, GWL.ExStyle, _initialStyle)
        End If
    End Sub

    Private Sub LoadMapPacks()
        Dim tsi As ToolStripItem
        If IO.Directory.Exists(_mapRoot) Then
            For Each Dir As String In IO.Directory.GetDirectories(_mapRoot)
                tsi = cSelectPack.DropDownItems.Add(IO.Path.GetFileName(Dir))
                tsi.Tag = Dir
                AddHandler tsi.Click, AddressOf cMapPackSelect_Click
            Next
        End If
    End Sub
#End Region

#Region " FILTER PANEL EVENTS "


    Private Sub PCFilterTypeChanged(ByVal filter As FilterType) Handles _filterPanel.PCFilterTypeChanged
        MapRadar.Settings.PCFilterType = filter
    End Sub

    Private Sub PCFilterChanged(ByVal filter As String) Handles _filterPanel.PCFilterChanged
        MapRadar.Settings.PCFilter = filter
    End Sub

    Private Sub NPCFilterTypeChanged(ByVal filter As FilterType) Handles _filterPanel.NPCFilterTypeChanged
        MapRadar.Settings.NPCFilterType = filter
    End Sub

    Private Sub NPCFilterChanged(ByVal Filter As String) Handles _filterPanel.NPCFilterChanged
        MapRadar.Settings.NPCFilter = Filter
    End Sub
#End Region

#Region " LINKED RADAR EVENTS "
    Private Sub lrf_NewMessage(ByVal e As LinkEventArgs) Handles _lrf.NewMessage
        If e.Type = LinkEventType.MobList Then
            If MapRadar.LinkServerRunning AndAlso e.Zone = MapRadar.Settings.CurrentMap Then
                If MapRadar.LinkMobs.ContainsKey(e.ClientID) Then
                    MapRadar.LinkMobs(e.ClientID) = e.Mobs
                Else
                    MapRadar.LinkMobs.Add(e.ClientID, e.Mobs)
                End If
            End If
        ElseIf e.Type = LinkEventType.ClientDisconnected Then
            MapRadar.LinkMobs.Remove(e.ClientID)
        End If
    End Sub

    Private Sub lrf_ServerStatus(ByVal IsRunning As Boolean) Handles _lrf.ServerStatus
        MapRadar.LinkServerRunning = IsRunning
    End Sub
#End Region

#Region " WATCHER EVENTS "
    Private Sub _watcher_NewMobList(ByVal InMobs As MobList) Handles _watcher.NewMobList
        Mobs = InMobs
        'MapRadar.Settings.CurrentMap = MemoryScanner.Scanner.CurrentMap
        Invalidate()
    End Sub

    Private Sub _Watcher_ZoneChanged(ByVal LastZone As Short, ByVal NewZone As Short) Handles _watcher.ZoneChanged
        MapRadar.Settings.CurrentMap = MemoryScanner.Scanner.CurrentMap
    End Sub

    Private Sub _watcher_VNMLocationUpdated(ByVal Direction As Direction, ByVal Distance As Integer) Handles _watcher.OnVNMLocationUpdated
        MapRadar.VNMDirection = Direction
        MapRadar.VNMDistance = Distance
    End Sub
#End Region

    Private Sub ToolStripMenuItem1_Click(sender As Object, e As EventArgs) Handles ToolStripMenuItem1.Click
        Me.MapRadar.MapController.ReloadIniData()
    End Sub
End Class