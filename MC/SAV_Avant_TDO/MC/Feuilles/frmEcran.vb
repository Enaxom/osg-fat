Imports DevExpress.XtraGauges.Win.Gauges.Circular
Imports DevExpress.XtraGauges.Core.Model
Imports System.Windows.Forms
Imports System.Collections.Generic
Imports System.Drawing
Imports DevExpress.XtraGrid

Imports Rappel
Imports System.Diagnostics
Imports DevExpress.XtraGrid.Views.Grid

Public Class frmEcran

    Private WithEvents timer As System.Windows.Forms.Timer
    Private scaleMinutes, scaleSeconds As ArcScaleComponent
    Private lockTimerCounter As Integer = 0

    Private _fichierIni As String
    Private _iD_MACHINE As Integer
    Private _iD_USER As Integer

    Friend Liste_event_courants As New List(Of grid_row)
    Friend Liste_event_brouillons As New List(Of grid_row)
    Friend SAR_OK As Boolean

    Friend Liste_Alertes As New List(Of grid_row)

    Public OpeDangDejaApparuEC As Boolean
    Public OpeDangDejaApparuB As Boolean

    Public documentation As doc.Doc

    Private ChgGrille As Integer
    Public ClassParent As Init_MC
    Private LeFiltre As String

    Dim mMouseHitPoint As Point

    Friend timer_rappel As Integer

    Friend droit1 As String = ""
    Friend droit2 As String = ""
    Friend texte_opedan As String

    Public Sub New()
        InitializeComponent()

        OpeDangDejaApparuEC = False
        OpeDangDejaApparuB = False
    End Sub

    Sub New(ByVal FichierIni As String, ByVal ID_MACHINE As Integer, ByVal ID_USER As Integer, ByRef CParent As Init_MC)

        InitializeComponent()
        TimerBCL.Enabled = False
        ' TODO: Complete member initialization 
        _fichierIni = FichierIni
        _iD_MACHINE = ID_MACHINE
        _iD_USER = ID_USER

        ClassParent = CParent
        timer = New System.Windows.Forms.Timer

        CLN_DATE.Width = 100
        CLN_HEURE.Width = 100
        CLN_Event.Width = GridEvent.Width - 400
        CLN_OP.Width = 130
        CLN_BIFF.Width = 70

        CLN_DATE_BR.Width = 100
        CLN_HEURE_BR.Width = 100
        CLN_TITRE_BR.Width = GridBrouillon.Width - 400
        CLN_OP_BR.Width = 130
        CLN_ANNULER_BR.Width = 70

        If Not ClassParent.connect_ADO.Init(FichierIni) Then
            Me.Close()
        End If

        'Timer
        scaleMinutes = CircularGauge1.AddScale()
        scaleSeconds = CircularGauge1.AddScale()

        scaleMinutes.Assign(scaleHours)
        scaleSeconds.Assign(scaleHours)

        ArcScaleNeedleComponent2.ArcScale = scaleMinutes
        ArcScaleNeedleComponent3.ArcScale = scaleSeconds
        timer.Start()
        OnTimerTick(Nothing, Nothing)


        'Skins
        DevExpress.Skins.SkinManager.EnableFormSkins()
        DevExpress.LookAndFeel.LookAndFeelHelper.ForceDefaultLookAndFeelChanged()
        DevExpress.UserSkins.BonusSkins.Register()
        DevExpress.UserSkins.OfficeSkins.Register()

        GridEvent.DataSource = Liste_event_courants
        GridBrouillon.DataSource = Liste_event_brouillons

        btnRondier.Visible = ClassParent.PathRondierExe <> ""

        charger_labels()
        Me.PanelSSLIA.Visible = ClassParent.IsSSLIA

        AddHandler Picture_Event.MouseUp, AddressOf Picture_Event_Click
        AddHandler Picture_Histo.MouseUp, AddressOf Picture_Histo_Click

        If ClassParent.connect_ADO.ADODB_connection(1) Then
            Dim rs_user As ADODB.Recordset
            rs_user = ClassParent.connect_ADO.ADODB_create_recordset(1, "SELECT * FROM LANGUE WHERE IdObjet='MC' AND IdLangue='MESSAGEOPEDAN'")
            texte_opedan = rs_user.Fields("Libelle").Value
            rs_user.Close()
        End If
        'ATTRIBUTION DES COULEURS
        ConditionsAdjustment()

        ClassParent.connect_ADO.ADODB_deconnection(1)

    End Sub

#Region "Timer"


    Sub OnTimerTick(ByVal sender As Object, ByVal e As System.EventArgs) Handles timer.Tick
        If lockTimerCounter = 0 Then
            lockTimerCounter += 1
            UpdateClock(DateTime.Now, scaleHours, scaleMinutes, scaleSeconds)
            lockTimerCounter -= 1
        End If
    End Sub
    Sub UpdateClock(ByVal dt As DateTime, ByVal h As IArcScale, ByVal m As IArcScale, ByVal s As IArcScale)
        Try


            Dim hour As Integer
            If dt.Hour <= 12 Then
                hour = dt.Hour
            Else
                hour = dt.Hour - 12
            End If
            Dim min As Integer = dt.Minute
            Dim sec As Integer = dt.Second
            h.Value = CSng(hour) + CSng(min) / 60.0F
            m.Value = (CSng(min) + CSng(sec) / 60.0F) / 5.0F
            s.Value = sec / 5.0F
        Catch ex As Exception

        End Try
    End Sub
#End Region

    Private Sub charger_labels()
        Dim rs As ADODB.Recordset
        If ClassParent.connect_ADO.ADODB_connection(1) Then

            TxtGroupe.Properties.ReadOnly = True
            TxtNom.Properties.ReadOnly = True
            TxtPrenom.Properties.ReadOnly = True
            TxtPriseService.Properties.ReadOnly = True

            TxtGroupe.ContextMenuStrip = New ContextMenuStrip
            TxtNom.ContextMenuStrip = New ContextMenuStrip
            TxtPrenom.ContextMenuStrip = New ContextMenuStrip
            TxtPriseService.ContextMenuStrip = New ContextMenuStrip

            rs = ClassParent.connect_ADO.ADODB_create_recordset(1, "SELECT * FROM LANGUE WHERE IdObjet='MC' AND IdLangue='TitreFenetreMC'")
            Me.Text = rs.Fields("Libelle").Value
            rs.Close()

            rs = ClassParent.connect_ADO.ADODB_create_recordset(1, "SELECT Type FROM LANGUE WHERE IdObjet='MC' AND IdLangue='TIMER_RAPPEL'")
            timer_rappel = CInt(rs.Fields(0).Value)
            rs.Close()

            rs = ClassParent.connect_ADO.ADODB_create_recordset(1, "SELECT * FROM LANGUE WHERE IdObjet='MC' AND IdLangue='Menu' AND Type=1")
            Me.BarSubItem1.Caption = rs.Fields("Libelle").Value
            rs.Close()

            rs = ClassParent.connect_ADO.ADODB_create_recordset(1, "SELECT * FROM LANGUE WHERE IdObjet='MC' AND IdLangue='NOFILTRE'")
            cboFiltre.Properties.Items.Add(New Data_Set(rs.Fields("Libelle").Value, rs.Fields("IdLangue").Value, rs.Fields("Type").Value))
            rs.Close()

            If ClassParent.connect_ADO.ADODB_connection(2) Then
                rs = ClassParent.connect_ADO.ADODB_create_recordset(1, "SELECT droit FROM NIVEAUX_UTILISATEUR, UTILISATEURS WHERE IDUtilisateur= " & _iD_USER & " AND levelU=IdNiveauxUtilisateurs")
                droit1 = rs.Fields(0).Value
                rs.Close()
                rs = ClassParent.connect_ADO.ADODB_create_recordset(2, "SELECT droit FROM MACHINE WHERE IDMachine= " & _iD_MACHINE)
                droit2 = rs.Fields(0).Value
                rs.Close()

                rs = ClassParent.connect_ADO.ADODB_create_recordset(1, "SELECT * FROM LANGUE WHERE IdObjet='MC' AND IdLangue='RAPPEL'")
                Me.BarButtonF1.Caption = rs.Fields("Libelle").Value
                cboFiltre.Properties.Items.Add(New Data_Set(rs.Fields("Libelle").Value, rs.Fields("IdLangue").Value, rs.Fields("Type").Value))
                If droit1(33) = "1" And droit2(33) = "1" Then
                    cboTypeEvenement.Properties.Items.Add(New Data_Set("F1 - " & rs.Fields("Libelle").Value, rs.Fields("IdLangue").Value, rs.Fields("Type").Value))
                    Me.BarButtonF1.ItemShortcut = New DevExpress.XtraBars.BarShortcut(Shortcut.F1)
                End If
                rs.Close()

                rs = ClassParent.connect_ADO.ADODB_create_recordset(1, "SELECT * FROM LANGUE WHERE IdObjet='MC' AND IdLangue='MVTPERSO'")
                Me.BarButtonF2.Caption = rs.Fields("Libelle").Value
                cboFiltre.Properties.Items.Add(New Data_Set(rs.Fields("Libelle").Value, rs.Fields("IdLangue").Value, rs.Fields("Type").Value))
                If droit1(34) = "1" And droit2(34) = "1" Then
                    cboTypeEvenement.Properties.Items.Add(New Data_Set("F2 - " & rs.Fields("Libelle").Value, rs.Fields("IdLangue").Value, rs.Fields("Type").Value))
                    Me.BarButtonF2.ItemShortcut = New DevExpress.XtraBars.BarShortcut(Shortcut.F2)
                End If
                rs.Close()

                rs = ClassParent.connect_ADO.ADODB_create_recordset(1, "SELECT * FROM LANGUE WHERE IdObjet='MC' AND IdLangue='MVTMAT'")
                Me.BarButtonF3.Caption = rs.Fields("Libelle").Value
                cboFiltre.Properties.Items.Add(New Data_Set(rs.Fields("Libelle").Value, rs.Fields("IdLangue").Value, rs.Fields("Type").Value))
                If droit1(35) = "1" And droit2(35) = "1" Then
                    cboTypeEvenement.Properties.Items.Add(New Data_Set("F3 - " & rs.Fields("Libelle").Value, rs.Fields("IdLangue").Value, rs.Fields("Type").Value))
                    Me.BarButtonF3.ItemShortcut = New DevExpress.XtraBars.BarShortcut(Shortcut.F3)
                End If
                rs.Close()

                rs = ClassParent.connect_ADO.ADODB_create_recordset(1, "SELECT * FROM LANGUE WHERE IdObjet='MC' AND IdLangue='DIV'")
                Me.BarButtonF4.Caption = rs.Fields("Libelle").Value
                cboFiltre.Properties.Items.Add(New Data_Set(rs.Fields("Libelle").Value, rs.Fields("IdLangue").Value, rs.Fields("Type").Value))
                If droit1(36) = "1" And droit2(36) = "1" Then
                    cboTypeEvenement.Properties.Items.Add(New Data_Set("F4 - " & rs.Fields("Libelle").Value, rs.Fields("IdLangue").Value, rs.Fields("Type").Value))
                    Me.BarButtonF4.ItemShortcut = New DevExpress.XtraBars.BarShortcut(Shortcut.F4)
                End If
                rs.Close()

                rs = ClassParent.connect_ADO.ADODB_create_recordset(1, "SELECT * FROM LANGUE WHERE IdObjet='MC' AND IdLangue='OPEMAIN'")
                Me.BarButtonF5.Caption = rs.Fields("Libelle").Value
                cboFiltre.Properties.Items.Add(New Data_Set(rs.Fields("Libelle").Value, rs.Fields("IdLangue").Value, rs.Fields("Type").Value))
                If droit1(37) = "1" And droit2(37) = "1" Then
                    cboTypeEvenement.Properties.Items.Add(New Data_Set("F5 - " & rs.Fields("Libelle").Value, rs.Fields("IdLangue").Value, rs.Fields("Type").Value))
                    Me.BarButtonF5.ItemShortcut = New DevExpress.XtraBars.BarShortcut(Shortcut.F5)
                End If
                rs.Close()

                rs = ClassParent.connect_ADO.ADODB_create_recordset(1, "SELECT * FROM LANGUE WHERE IdObjet='MC' AND IdLangue='DINTER'")
                Me.BarButtonF6.Caption = rs.Fields("Libelle").Value
                cboFiltre.Properties.Items.Add(New Data_Set(rs.Fields("Libelle").Value, rs.Fields("IdLangue").Value, rs.Fields("Type").Value))
                If droit1(39) = "1" And droit2(39) = "1" Then
                    cboTypeEvenement.Properties.Items.Add(New Data_Set("F6 - " & rs.Fields("Libelle").Value, rs.Fields("IdLangue").Value, rs.Fields("Type").Value))
                    Me.BarButtonF6.ItemShortcut = New DevExpress.XtraBars.BarShortcut(Shortcut.F6)
                End If
                rs.Close()

                rs = ClassParent.connect_ADO.ADODB_create_recordset(1, "SELECT * FROM LANGUE WHERE IdObjet='MC' AND IdLangue='RELEVE'")
                Me.BarButtonF7.Caption = rs.Fields("Libelle").Value
                If droit1(41) = "1" And droit2(41) = "1" Then
                    cboTypeEvenement.Properties.Items.Add(New Data_Set("F7 - " & rs.Fields("Libelle").Value, rs.Fields("IdLangue").Value, rs.Fields("Type").Value))
                    Me.BarButtonF7.ItemShortcut = New DevExpress.XtraBars.BarShortcut(Shortcut.F7)
                End If
                rs.Close()

                rs = ClassParent.connect_ADO.ADODB_create_recordset(1, "SELECT * FROM LANGUE WHERE IdObjet='MC' AND IdLangue='RONDE'")
                Me.BarButtonF8.Caption = rs.Fields("Libelle").Value
                cboFiltre.Properties.Items.Add(New Data_Set(rs.Fields("Libelle").Value, rs.Fields("IdLangue").Value, rs.Fields("Type").Value))
                If droit1(38) = "1" And droit2(38) = "1" Then
                    cboTypeEvenement.Properties.Items.Add(New Data_Set("F8 - " & rs.Fields("Libelle").Value, rs.Fields("IdLangue").Value, rs.Fields("Type").Value))
                    Me.BarButtonF8.ItemShortcut = New DevExpress.XtraBars.BarShortcut(Shortcut.F8)
                End If
                rs.Close()

                rs = ClassParent.connect_ADO.ADODB_create_recordset(1, "SELECT * FROM LANGUE WHERE IdObjet = 'GENERIQUE' AND IdLangue='SAR_OK'")
                SAR_OK = CBool(rs.Fields("Type").Value)
                rs.Close()

                If SAR_OK Then
                    rs = ClassParent.connect_ADO.ADODB_create_recordset(1, "SELECT * FROM LANGUE WHERE IdObjet='MC' AND IdLangue='OPEDAN'")
                    Me.BarButtonF9.Caption = rs.Fields("Libelle").Value
                    cboFiltre.Properties.Items.Add(New Data_Set(rs.Fields("Libelle").Value, rs.Fields("IdLangue").Value, rs.Fields("Type").Value))
                    If droit1(42) = "1" And droit2(42) = "1" Then
                        cboTypeEvenement.Properties.Items.Add(New Data_Set("F9 - " & rs.Fields("Libelle").Value, rs.Fields("IdLangue").Value, rs.Fields("Type").Value))
                        Me.BarButtonF9.ItemShortcut = New DevExpress.XtraBars.BarShortcut(Shortcut.F9)
                    End If
                    rs.Close()
                End If

                If ClassParent.IsRIT And droit1(51) = "1" And droit2(51) = "1" Then
                    rs = ClassParent.connect_ADO.ADODB_create_recordset(1, "SELECT * FROM LANGUE WHERE IdObjet='MC' AND IdLangue='DIT'")
                    Me.BarButtonF10.Caption = rs.Fields("Libelle").Value
                    cboFiltre.Properties.Items.Add(New Data_Set(rs.Fields("Libelle").Value, rs.Fields("IdLangue").Value, rs.Fields("Type").Value))
                    If droit1(39) = "1" And droit2(39) = "1" Then
                        cboTypeEvenement.Properties.Items.Add(New Data_Set("F10 - " & rs.Fields("Libelle").Value, rs.Fields("IdLangue").Value, rs.Fields("Type").Value))
                        Me.BarButtonF10.ItemShortcut = New DevExpress.XtraBars.BarShortcut(Shortcut.F10)
                    End If
                    rs.Close()
                End If

                If ClassParent.IsSSLIA Then
                    rs = ClassParent.connect_ADO.ADODB_create_recordset(1, "SELECT * FROM LANGUE WHERE IdObjet='MC' AND IdLangue='DIA'")
                    Me.BarButtonF11.Caption = rs.Fields("Libelle").Value
                    cboFiltre.Properties.Items.Add(New Data_Set(rs.Fields("Libelle").Value, rs.Fields("IdLangue").Value, rs.Fields("Type").Value))
                    If droit1(39) = "1" And droit2(39) = "1" Then
                        cboTypeEvenement.Properties.Items.Add(New Data_Set("F11 - " & rs.Fields("Libelle").Value, rs.Fields("IdLangue").Value, rs.Fields("Type").Value))
                        Me.BarButtonF11.ItemShortcut = New DevExpress.XtraBars.BarShortcut(Shortcut.F11)
                    End If
                    rs.Close()
                    rs = ClassParent.connect_ADO.ADODB_create_recordset(1, "SELECT * FROM LANGUE WHERE IdObjet='MC' AND IdLangue='SLVP'")
                    Me.BarButtonF12.Caption = rs.Fields("Libelle").Value
                    cboFiltre.Properties.Items.Add(New Data_Set(rs.Fields("Libelle").Value, rs.Fields("IdLangue").Value, rs.Fields("Type").Value))
                    If droit1(39) = "1" And droit2(39) = "1" Then
                        cboTypeEvenement.Properties.Items.Add(New Data_Set("F12 - " & rs.Fields("Libelle").Value, rs.Fields("IdLangue").Value, rs.Fields("Type").Value))
                        Me.BarButtonF12.ItemShortcut = New DevExpress.XtraBars.BarShortcut(Shortcut.F12)
                    End If
                    rs.Close()
                End If

            End If

            LblDate.Text = Date.Now.ToShortDateString

            rs = ClassParent.connect_ADO.ADODB_create_recordset(1, "SELECT * FROM LANGUE WHERE IdObjet='MC' AND IdLangue='BTN_HISTO'")
            Me.BtnAffHisto.Text = rs.Fields("Libelle").Value
            rs.Close()

            rs = ClassParent.connect_ADO.ADODB_create_recordset(1, "SELECT * FROM LANGUE WHERE IdObjet='MC' AND IdLangue='GRP_CREATE_EVENT'")
            Me.BarManager1.MainMenu.Text = rs.Fields("Libelle").Value
            Me.GrpNewEvt.Text = rs.Fields("Libelle").Value
            rs.Close()


            rs = ClassParent.connect_ADO.ADODB_create_recordset(1, "SELECT * FROM LANGUE WHERE IdObjet='MC' AND IdLangue='BTN_FORCEMAJ'")
            Me.BtnForceMaj.Text = rs.Fields("Libelle").Value
            rs.Close()

            rs = ClassParent.connect_ADO.ADODB_create_recordset(1, "SELECT * FROM LANGUE WHERE IdObjet='MC' AND IdLangue='GRP_OPERATEUR'")
            Me.GrpStationnaire.Text = rs.Fields("Libelle").Value
            rs.Close()

            rs = ClassParent.connect_ADO.ADODB_create_recordset(1, "SELECT * FROM LANGUE WHERE IdObjet='MC' AND IdLangue='LBL_NOM'")
            Me.LblNom.Text = rs.Fields("Libelle").Value & " :"
            rs.Close()

            rs = ClassParent.connect_ADO.ADODB_create_recordset(1, "SELECT * FROM LANGUE WHERE IdObjet='MC' AND IdLangue='LBL_PRENOM'")
            Me.LblPrenom.Text = rs.Fields("Libelle").Value & " :"
            rs.Close()

            rs = ClassParent.connect_ADO.ADODB_create_recordset(1, "SELECT * FROM LANGUE WHERE IdObjet='MC' AND IdLangue='LBL_EQUIPE'")
            Me.LblGroupe.Text = rs.Fields("Libelle").Value & " :"
            rs.Close()

            rs = ClassParent.connect_ADO.ADODB_create_recordset(1, "SELECT * FROM LANGUE WHERE IdObjet='MC' AND IdLangue='LBL_SERVICE'")
            Me.LblPriseService.Text = rs.Fields("Libelle").Value & " :"
            rs.Close()

            rs = ClassParent.connect_ADO.ADODB_create_recordset(1, "SELECT * FROM LANGUE WHERE IdObjet='MC' AND IdLangue='BTN_RELEVE'")
            Me.BtnReleveStationnaire.Text = rs.Fields("Libelle").Value
            rs.Close()

            rs = ClassParent.connect_ADO.ADODB_create_recordset(1, "SELECT * FROM LANGUE WHERE IdObjet='MC' AND IdLangue='GRP_EVENT_EN_COURS'")
            Me.GrpEnCour.Text = rs.Fields("Libelle").Value
            rs.Close()

            rs = ClassParent.connect_ADO.ADODB_create_recordset(1, "SELECT * FROM LANGUE WHERE IdObjet='MC' AND IdLangue='LBL_BROUILLON'")
            Me.GrpBrouillon.Text = rs.Fields("Libelle").Value
            rs.Close()

            rs = ClassParent.connect_ADO.ADODB_create_recordset(1, "SELECT * FROM LANGUE WHERE IdObjet='MC' AND IdLangue='BTN_QUITTER'")
            Me.BtnQuitter.Text = rs.Fields("Libelle").Value
            rs.Close()

            rs = ClassParent.connect_ADO.ADODB_create_recordset(1, "SELECT * FROM LANGUE WHERE IdObjet='MC' AND IdLangue='BTN_ALERTE'")
            Me.CMD_Alerte.Text = rs.Fields("Libelle").Value
            rs.Close()

            rs = ClassParent.connect_ADO.ADODB_create_recordset(1, "SELECT * FROM LANGUE WHERE IdObjet='MC' AND IdLangue='CLN_ANNULER'")
            Me.CLN_ANNULER_BR.Caption = rs.Fields("Libelle").Value
            rs.Close()

            rs = ClassParent.connect_ADO.ADODB_create_recordset(1, "SELECT * FROM LANGUE WHERE IdObjet='MC' AND IdLangue='CLN_BIFFAGE'")
            Me.CLN_BIFF.Caption = rs.Fields("Libelle").Value
            rs.Close()

            rs = ClassParent.connect_ADO.ADODB_create_recordset(1, "SELECT * FROM LANGUE WHERE IdObjet='MC' AND IdLangue='CLN_CHEF'")
            Me.CLN_OP.Caption = rs.Fields("Libelle").Value
            Me.CLN_OP_BR.Caption = rs.Fields("Libelle").Value
            rs.Close()

            rs = ClassParent.connect_ADO.ADODB_create_recordset(1, "SELECT * FROM LANGUE WHERE IdObjet='MC' AND IdLangue='CLN_DATE'")
            Me.CLN_DATE.Caption = rs.Fields("Libelle").Value
            Me.CLN_DATE_BR.Caption = rs.Fields("Libelle").Value
            rs.Close()

            rs = ClassParent.connect_ADO.ADODB_create_recordset(1, "SELECT * FROM LANGUE WHERE IdObjet='MC' AND IdLangue='CLN_HEURE'")
            Me.CLN_HEURE.Caption = rs.Fields("Libelle").Value
            Me.CLN_HEURE_BR.Caption = rs.Fields("Libelle").Value
            rs.Close()

            rs = ClassParent.connect_ADO.ADODB_create_recordset(1, "SELECT * FROM LANGUE WHERE IdObjet='MC' AND IdLangue='CLN_TITRE_EVENT'")
            Me.CLN_TITRE_BR.Caption = rs.Fields("Libelle").Value
            Me.CLN_Event.Caption = rs.Fields("Libelle").Value
            rs.Close()

            rs = ClassParent.connect_ADO.ADODB_create_recordset(1, "SELECT * FROM LANGUE WHERE IdObjet='MC' AND IdLangue='GRP_FILTRE'")
            Me.LblFiltre.Text = rs.Fields("Libelle").Value
            rs.Close()

            '------------------------------------------------------------------------------------------------------------------------

            rs = ClassParent.connect_ADO.ADODB_create_recordset(1, "SELECT * FROM LANGUE WHERE IdObjet='TRAKA' AND idLangue='TITRE_FENETRE'")
            Me.btnTraka.ToolTip = rs.Fields("Libelle").Value
            rs.Close()

            rs = ClassParent.connect_ADO.ADODB_create_recordset(1, "SELECT * FROM LANGUE WHERE IdObjet='ADM_DOC' AND IdLangue='LINK'")
            Me.btnFiches.ToolTip = rs.Fields("Libelle").Value
            rs.Close()

            rs = ClassParent.connect_ADO.ADODB_create_recordset(1, "SELECT * FROM LANGUE WHERE IdObjet='MC' AND IdLangue='TITRE_FRMDISPOS'")
            Me.btnDispos.ToolTip = rs.Fields("Libelle").Value
            rs.Close()

            rs = ClassParent.connect_ADO.ADODB_create_recordset(1, "SELECT * FROM LANGUE WHERE IdObjet='MC' AND IdLangue='LBL_WINKONTROL'")
            Me.btnRondier.ToolTip = rs.Fields("Libelle").Value
            rs.Close()

            ClassParent.connect_ADO.ADODB_deconnection(1)
            ClassParent.connect_ADO.ADODB_deconnection(2)

        End If

    End Sub

    Private Sub frmEcran_Resize(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Resize

        GrpEnCour.Width = Me.Width - 30
        GrpBrouillon.Width = Me.Width - 30

        BtnQuitter.Left = (Me.Width / 2) - (BtnQuitter.Width / 2)
        PanelTop.Left = (Me.Width / 2) - (PanelTop.Width / 2) + (PanelSSLIA.Width / 2)

        SplitContainerMC.Width = Me.Width - 30
        SplitContainerMC.Height = Me.Height - BtnQuitter.Height - PanelTop.Height - PanelControl3.Height - 90

        'PanelControl3.Left = GrpEnCour.Left - 1
        PanelControl3.Width = GrpEnCour.Width - 2

        BtnQuitter.Top = Me.Height - BtnQuitter.Height - 35
        CMD_Alerte.Top = BtnQuitter.Top

        GrpBrouillon.Top = Me.Height - GrpBrouillon.Height - BtnQuitter.Height - 45

        'GrpEnCour.Height = Me.Height - GrpBrouillon.Height - BtnQuitter.Height - PanelTop.Height - 90

        PanelControl3.Height = 57

        GridEvent.Height = GrpEnCour.Height - PanelControl3.Height - 27

        CLN_DATE.Width = 100
        CLN_HEURE.Width = 100
        CLN_Event.Width = GridEvent.Width - 400
        CLN_OP.Width = 130
        CLN_BIFF.Width = 70

        CLN_DATE_BR.Width = 100
        CLN_HEURE_BR.Width = 100
        CLN_TITRE_BR.Width = GridBrouillon.Width - 400
        CLN_OP_BR.Width = 130
        CLN_ANNULER_BR.Width = 70
    End Sub

    Private Sub BarButtonF1_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles BarButtonF1.ItemClick
        For Each item As Data_Set In cboTypeEvenement.Properties.Items
            If item.id = 1 Then
                LancerDll(item)
            End If
        Next
    End Sub

    Private Sub BarButtonF2_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles BarButtonF2.ItemClick
        For Each item As Data_Set In cboTypeEvenement.Properties.Items
            If item.id = 2 Then
                LancerDll(item)
            End If
        Next
    End Sub

    Private Sub BarButtonF3_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles BarButtonF3.ItemClick
        For Each item As Data_Set In cboTypeEvenement.Properties.Items
            If item.id = 3 Then
                LancerDll(item)
            End If
        Next
    End Sub

    Private Sub BarButtonF4_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles BarButtonF4.ItemClick
        For Each item As Data_Set In cboTypeEvenement.Properties.Items
            If item.id = 4 Then
                LancerDll(item)
            End If
        Next
    End Sub

    Private Sub BarButtonF5_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles BarButtonF5.ItemClick
        For Each item As Data_Set In cboTypeEvenement.Properties.Items
            If item.id = 5 Then
                LancerDll(item)
            End If
        Next
    End Sub

    Private Sub BarButtonF6_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles BarButtonF6.ItemClick
        For Each item As Data_Set In cboTypeEvenement.Properties.Items
            If item.id = 6 Then
                LancerDll(item)
            End If
        Next
    End Sub

    Private Sub BarButtonF7_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles BarButtonF7.ItemClick
        For Each item As Data_Set In cboTypeEvenement.Properties.Items
            If item.id = 7 Then
                LancerDll(item)
            End If
        Next
    End Sub

    Private Sub BarButtonF8_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles BarButtonF8.ItemClick
        For Each item As Data_Set In cboTypeEvenement.Properties.Items
            If item.id = 8 Then
                LancerDll(item)
            End If
        Next
    End Sub

    Private Sub BarButtonF9_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles BarButtonF9.ItemClick
        For Each item As Data_Set In cboTypeEvenement.Properties.Items
            If item.id = 9 Then
                LancerDll(item)
            End If
        Next
    End Sub

    Private Sub BarButtonF10_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles BarButtonF10.ItemClick
        For Each item As Data_Set In cboTypeEvenement.Properties.Items
            If item.id = 10 Then
                LancerDll(item)
            End If
        Next
    End Sub

    Private Sub BarButtonF11_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles BarButtonF11.ItemClick
        For Each item As Data_Set In cboTypeEvenement.Properties.Items
            If item.id = 11 Then
                LancerDll(item)
            End If
        Next
    End Sub

    Private Sub BarButtonF12_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles BarButtonF12.ItemClick
        For Each item As Data_Set In cboTypeEvenement.Properties.Items
            If item.id = 12 Then
                LancerDll(item)
            End If
        Next
    End Sub

    Private Sub charger_grids(ByVal Filtre As String)

        Dim rs As ADODB.Recordset
        Dim rs_user As ADODB.Recordset
        Dim SQL As String
        Dim Obj As grid_row = New grid_row(ClassParent)

        Dim nom As String

        Dim dat As Date
        Dim dat2 As Date

        Try
            OpeDangDejaApparuEC = False
            OpeDangDejaApparuB = False

            Liste_event_courants.Clear()
            Liste_event_brouillons.Clear()

            BtnForceMaj.Visible = False
            lblMessLecture.Text = ""
            lblMessLecture.Visible = False

            If ClassParent.connect_ADO.ADODB_connection(2) And ClassParent.connect_ADO.ADODB_connection(1) Then

                If Filtre = "" Or Filtre = "NOFILTRE" Then
                    SQL = "SELECT * FROM MC WHERE (ESTENCOURS=true or ESTBROUILLON=true) AND ESTSUPP=false ORDER BY DateHeureDebut "
                Else
                    SQL = "SELECT * FROM MC WHERE (ESTENCOURS=true or ESTBROUILLON=true) AND ESTSUPP=false AND (Objet='" & Filtre & "') ORDER BY DateHeureDebut "
                End If

                rs = ClassParent.connect_ADO.ADODB_create_recordset(2, SQL)

                While Not rs.EOF
                    Obj.CleObj = rs.Fields("cleobj").Value
                    Obj.EstBrouillon = CBool(rs.Fields("EstBrouillon").Value)
                    Obj.EstEncours = CBool(rs.Fields("EstEncours").Value)
                    Obj.EstSupp = CBool(rs.Fields("EstSupp").Value)
                    Obj.Titre = rs.Fields("Affichage").Value
                    Obj.TypeObj = rs.Fields("Objet").Value
                    Obj.DateHeure = rs.Fields("DateHeureDebut").Value
                    Obj.IndexEvt = CLng(rs.Fields("CleMc").Value)
                    Obj.IdOperateur = CLng(rs.Fields("CleStationnaire").Value)
                    rs_user = ClassParent.connect_ADO.ADODB_create_recordset(1, "SELECT * FROM UTILISATEURS WHERE IDUtilisateur=" & Obj.IdOperateur)
                    If Not rs_user.EOF Then
                        Obj.Operateur = rs_user.Fields("NomUtilisateur").Value & " " & rs_user.Fields("PrenomUtilisateur").Value
                    Else
                        Obj.Operateur = " "
                    End If
                    rs_user.Close()
                    Obj.AttenteRapport = CBool(rs.Fields("AttenteRapport").Value)

                    If Obj.TypeObj = "DINTER" Then
                        Obj.IdInter = Retourne_IdInter(Obj.CleObj)
                    End If

                    If StrComp(Obj.TypeObj, "OPEDAN") = 0 Then
                        If SAR_OK Then
                            'Si brouillon
                            If Obj.EstBrouillon Then
                                'si pas déjà apparu je le modifie et l'affiche
                                If Obj.EstEncours = False And Obj.AttenteRapport = False Then
                                    If OpeDangDejaApparuB = False Then
                                        nom = "MESSAGE" & Obj.TypeObj
                                        Obj.Titre = texte_opedan
                                        Liste_event_brouillons.Add(Obj)
                                        OpeDangDejaApparuB = True
                                    End If
                                End If
                            End If
                            'Si encours
                            If Obj.EstEncours Or Obj.AttenteRapport Then
                                'si pas déjà apparu je le modifie et l'affiche
                                If OpeDangDejaApparuEC = False Then
                                    nom = "MESSAGE" & Obj.TypeObj
                                    Obj.Titre = texte_opedan
                                    Liste_event_courants.Add(Obj)
                                    OpeDangDejaApparuEC = True
                                End If
                            End If
                        End If
                    Else
                        If StrComp(UCase(Obj.TypeObj), "RAPPEL") = 0 And StrComp(UCase(LeFiltre), "RAPPEL") <> 0 And Obj.EstEncours Then
                            If (ClassParent.IsTousRappel) Or (IDMAchine_Retour(Obj.CleObj, 0) = _iD_MACHINE) Then
                                dat2 = Date.Now.AddMinutes(timer_rappel)
                                dat = Date.FromOADate(Obj.DateHeure)
                                If dat < dat2 Then
                                    Liste_event_courants.Add(Obj)
                                End If
                            End If
                        Else
                            If Obj.EstEncours Then
                                If StrComp(UCase(Obj.TypeObj), "RAPPEL") = 0 Then
                                    If (ClassParent.IsTousRappel Or (IDMAchine_Retour(Obj.CleObj, 0) = _iD_MACHINE)) Then
                                        Liste_event_courants.Add(Obj)
                                    End If
                                Else
                                    Liste_event_courants.Add(Obj)
                                End If
                            Else
                                If Obj.EstBrouillon Then
                                    Liste_event_brouillons.Add(Obj)
                                End If
                            End If
                        End If
                    End If
                    rs.MoveNext()
                    Obj = New grid_row(ClassParent)
                End While
                GridEvent.RefreshDataSource()
                GridBrouillon.RefreshDataSource()
                rs.Close()
                '************************************
                '** Debut OSGBridge
                '************************************
                If ClassParent.OSG_Bridge Then
                    SQL = "SELECT * FROM ALERTES;"
                    rs = ClassParent.connect_ADO.ADODB_create_recordset(2, SQL)
                    If Not rs.EOF Then
                        If Not IsNothing(ClassParent.Warn) Then
                            ClassParent.Warn.Afficher()
                        End If
                    End If
                    rs.Close()
                    rs = Nothing
                End If
                '************************************
                '** Fin OSGBridge
                '************************************
                For Each O As grid_row In Liste_event_courants
                    If UCase(O.TypeObj) = "RAPPEL" Then
                        If Not IsNothing(O) Then
                            O.IDMachineExe = IDMAchine_Retour(O.CleObj, O.IdMail)
                            If (ClassParent.IsTousRappel <> False) Or (O.IDMachineExe = _iD_MACHINE) Then
                                O.Affichage(Me, LeFiltre)
                                If O.IsActionDiff Then
                                    Liste_Alertes.Add(O)
                                End If
                            Else
                                'On n'affiche pas l'élément
                            End If
                        End If
                    Else
                        If (O.EstEncours Or O.EstBrouillon) And Not O.EstSupp Then
                            O.Affichage(Me, LeFiltre)
                        End If
                    End If
                Next

                ClassParent.connect_ADO.ADODB_deconnection(1)
                ClassParent.connect_ADO.ADODB_deconnection(2)

            End If
        Catch ex As Exception
            DevExpress.XtraEditors.XtraMessageBox.Show(Err.Description, Application.ProductName, MessageBoxButtons.OK)
            ClassParent.connect_ADO.ADODB_deconnection(1)
            ClassParent.connect_ADO.ADODB_deconnection(2)
        End Try

    End Sub

    Private Function convert_couleur(ByVal couleur As Long) As Color
        Dim blue As Long = Int(couleur / 65536)
        Dim green As Long = Int((couleur - (65536 * blue)) / 256)
        Dim red As Long = couleur - ((blue * 65536) + (green * 256))
        Dim color As Color = color.FromArgb(255, red, green, blue)
        Return color
    End Function

    Private Sub ConditionsAdjustment()
        Dim color As Integer
        Dim IdCatInter As Long
        Dim rs As ADODB.Recordset
        Dim cn As StyleFormatCondition

        rs = ClassParent.connect_ADO.ADODB_create_recordset(1, "SELECT * FROM CATEGORIES_INTERVENTIONS")

        While Not rs.EOF
            color = CInt(rs.Fields("CodeCouleur").Value)
            IdCatInter = rs.Fields("IdCategorieIntervention").Value

            cn = New StyleFormatCondition(FormatConditionEnum.Equal, GridView1.Columns("CLN_DInter"), Nothing, IdCatInter)
            cn.Appearance.BackColor = convert_couleur(color)
            cn.Appearance.ForeColor = System.Drawing.Color.White
            cn.ApplyToRow = True
            GridView1.FormatConditions.Add(cn)

            rs.MoveNext()
        End While
        rs.Close()
        rs = Nothing

        GridView1.BestFitColumns()

    End Sub

    Private Sub BtnAffHisto_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnAffHisto.Click
        ClassParent.FrmFenetre.TimerBCL.Enabled = False
        ClassParent.AffichageHistorique()
        ClassParent.FrmFenetre.TimerBCL.Enabled = True
    End Sub

    Private Sub Timer1_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Timer1.Tick
        Me.ClassParent.Warn.EcranClignote()
    End Sub

    Private Sub BtnQuitter_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnQuitter.Click

        Me.Close()

    End Sub

    Private Sub BtnForceMaj_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnForceMaj.Click
        charger_grids(LeFiltre)
    End Sub

    Private Sub frmEcran_FormClosing(ByVal sender As System.Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles MyBase.FormClosing
        Dim rs As ADODB.Recordset
        ClassParent.Connection_Stationnaire(False)
        ClassParent.connect_ADO.ADODB_connection(1)
        rs = ClassParent.connect_ADO.ADODB_create_recordset(1, "SELECT * FROM UTILISATEURS WHERE IDUtilisateur=" & ClassParent.Stationnaire)
        rs.Fields("Disponibilite").Value = 6
        rs.Update()
        rs.Close()
        ClassParent.connect_ADO.ADODB_deconnection(1)
    End Sub

    Private Sub TimerBCL_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TimerBCL.Tick
        Me.TimerBCL.Enabled = False
        If Me.Visible Then

            Me.TimerBCL.Enabled = False
            If Me.Visible Then
                'Timer : Si ce poste a traka, s'il fait les rappels traka je lance
                If ClassParent.IsTraka And ClassParent.IsTrakaRappel Then
                    Me.TimerBCL.Enabled = False
                    Me.ClassParent.LancerRappelsTraka()
                End If
                If ClassParent.IsRondier Then
                    Me.ClassParent.ImportRondier()
                End If
                'Misa à jour de la date
                '        Parent.FrmFenetre.LblDate.Caption = Parent.MiseEnForme(Day(Now), 2) & "/" & Parent.MiseEnForme(Month(Now), 2) & "/" & Year(Now)
                ClassParent.FrmFenetre.LblDate.Text = Date.Now.ToShortDateString
                'mise à jour des informations affichée
                charger_grids(LeFiltre)

            End If

            Me.TimerBCL.Enabled = True

        End If

    End Sub

    Private Sub LancerDll(ByVal item As Data_Set)
        On Error GoTo LancerDll_Erreur
        Select Case item.id
            Case 1
                Dim Feuille As Rappel.gestionRappel = New Rappel.gestionRappel()
                Feuille.Init(ClassParent.FichierIni, 0, , ClassParent.Stationnaire)
                Feuille.execute()
            Case 2
                Dim Feuille As MvtPers.MvtPersonnel = New MvtPers.MvtPersonnel()
                Feuille.Init(ClassParent.FichierIni, 0, , ClassParent.Stationnaire)
                Feuille.Execute()
            Case 3
                Dim Feuille As MvtMat.Rapport = New MvtMat.Rapport()
                Feuille.Init(ClassParent.FichierIni, 0, , ClassParent.Stationnaire)
                Feuille.Execute()
            Case 4
                Dim Feuille As MvtDiv.gestiondiverses = New MvtDiv.gestiondiverses()
                Feuille.Init(ClassParent.FichierIni, 0, , ClassParent.Stationnaire)
                Feuille.Execute()
            Case 5
                Dim Feuille As OpeMaintenance.OperationMaintenance = New OpeMaintenance.OperationMaintenance()
                Feuille.Init(ClassParent.FichierIni, 0, , ClassParent.Stationnaire, ClassParent.IDMachine)
                Feuille.Execute()
            Case 6
                Dim Feuille As Intervention.DemIntervention = New Intervention.DemIntervention
                Feuille.Init(ClassParent.FichierIni, 0, , ClassParent.Stationnaire, ClassParent.ModeChrono, ClassParent.IDMachine)
                Feuille.execute()
            Case 7
                Dim Feuille As gestionReleve.gestionReleve = New gestionReleve.gestionReleve()
                Feuille.Init(ClassParent.FichierIni, 0, , ClassParent.Stationnaire)
                Feuille.execute()
            Case 8
                Dim Feuille As rondes.Ronde = New rondes.Ronde()
                Feuille.Init(ClassParent.FichierIni, 0, , ClassParent.Stationnaire)
                Feuille.Execute()
            Case 9
                Dim Feuille As OpeDanger.OpeDanger = New OpeDanger.OpeDanger()
                Feuille.Init(ClassParent.FichierIni, 0, , ClassParent.Stationnaire)
                Feuille.execute()
            Case 10
                Dim Feuille As DIT.DIT = New DIT.DIT
                Feuille.Init(ClassParent.FichierIni, 0, , ClassParent.Stationnaire, ClassParent.IDMachine)
                Feuille.execute()
            Case 11
                Dim Feuille As DIA.DemInterAero = New DIA.DemInterAero
                Feuille.Init(ClassParent.FichierIni, 0, , ClassParent.Stationnaire, ClassParent.IDMachine)
                Feuille.execute()
            Case 12
                Dim Feuille As LVP.LVP = New LVP.LVP
                Feuille.Init(ClassParent.FichierIni, 0, , ClassParent.Stationnaire, ClassParent.IDMachine)
                Feuille.execute()
        End Select
        Exit Sub
LancerDll_Erreur:
        MsgBox(Err.Description)
    End Sub

    Private Sub BtnCreationEvenement_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnCreationEvenement.Click
        TimerBCL.Enabled = False
        If cboTypeEvenement.SelectedIndex < 0 Then
            DevExpress.XtraEditors.XtraMessageBox.Show(ClassParent.ListeMessages("MESS_MC_2").Libelle, Application.ProductName, MessageBoxButtons.OK)
            Exit Sub
        End If
        'Me.Visible = False
        Dim obj As grid_row = New grid_row(ClassParent)
        obj.TypeObj = cboTypeEvenement.SelectedItem.nom
        obj.CleObj = 0
        'MsgBox("a")
        exec(0, obj)
        'MsgBox("b")
        charger_grids(LeFiltre)
        TimerBCL.Enabled = True
        'Me.Visible = True
    End Sub

    Private Sub MnuReleveStationnaire_Click()
        Dim Obj2 As gestionReleve.gestionReleve = New gestionReleve.gestionReleve
        Dim NomGrp As String = ""
        Dim NomU As String = ""
        Dim PreNomU As String = ""
        Dim LevelU As Integer
        Dim HeureU As Double
        Dim Clemc As Long
        Dim CleUser As Long
        Dim CleGrp As Long
        Dim Id_RELEVEOP As Integer
        On Error Resume Next
        Err.Clear()

        If Err.Number <> 0 Then
            DevExpress.XtraEditors.XtraMessageBox.Show(ClassParent.ListeMessages("MESS_MC_3").Libelle, Application.ProductName, MessageBoxButtons.OK)
        Else

            On Error GoTo 0
            Dim Chemininter As String


            Dim rs As ADODB.Recordset

            Obj2.Init(_fichierIni, 5, ClassParent.Groupe, ClassParent.Stationnaire, Id_RELEVEOP)
            If Obj2.execute(NomGrp, HeureU, NomU, PreNomU, Clemc, CleUser, CleGrp) Then
                If NomU <> "" Then
                    If Not Me.ClassParent.isBioVein Then
                        TxtGroupe.Text = NomGrp
                        TxtNom.Text = NomU
                        TxtPrenom.Text = PreNomU
                        TxtPriseService.Text = OLSDATE.LongTodate(HeureU) & " " & OLSDATE.LongToTime(HeureU).ToShortTimeString

                        ClassParent.connect_ADO.ADODB_connection(1)
                        rs = ClassParent.connect_ADO.ADODB_create_recordset(1, "SELECT * FROM UTILISATEURS WHERE IDUtilisateur=" & ClassParent.Stationnaire)
                        rs.Fields("Disponibilite").Value = 6
                        rs.Update()
                        rs.Close()

                        Me.ClassParent.Stationnaire = CleUser

                        rs = ClassParent.connect_ADO.ADODB_create_recordset(1, "SELECT * FROM UTILISATEURS WHERE IDUtilisateur=" & ClassParent.Stationnaire)
                        rs.Fields("Disponibilite").Value = 5
                        rs.Update()
                        rs.Close()
                        ClassParent.connect_ADO.ADODB_deconnection(1)

                        Me.ClassParent.Groupe = CleGrp
                    Else
                        TxtGroupe.Text = NomGrp
                        TxtPriseService.Text = OLSDATE.LongTodate(HeureU) & " " & OLSDATE.LongToTime(HeureU).ToShortTimeString
                        TxtNom.Text = NomU
                        TxtPrenom.Text = PreNomU
                        ClassParent.connect_ADO.ADODB_connection(1)

                        rs = ClassParent.connect_ADO.ADODB_create_recordset(1, "SELECT * FROM UTILISATEURS WHERE IDUtilisateur=" & ClassParent.Stationnaire)
                        rs.Fields("Disponibilite").Value = 6
                        rs.Update()
                        rs.Close()

                        Me.ClassParent.Stationnaire = CleUser

                        rs = ClassParent.connect_ADO.ADODB_create_recordset(1, "SELECT * FROM UTILISATEURS WHERE IDUtilisateur=" & ClassParent.Stationnaire)
                        rs.Fields("Disponibilite").Value = 5
                        rs.Update()
                        rs.Close()
                        ClassParent.connect_ADO.ADODB_deconnection(1)

                        Me.ClassParent.Groupe = CleGrp

                    End If
                End If
            End If
        End If
        Obj2 = Nothing
    End Sub

    Private Sub Picture_Event_Click(ByVal sender As Object, ByVal e As EventArgs) Handles Picture_Event.Click
        Throw New NotImplementedException
    End Sub

    Friend Function exec(ByVal Lemode As Integer, ByVal item As grid_row) As Boolean
        Try

            exec = True
            Dim droit_ok As Boolean
            'PAS DE JETON SI CREATION EVENEMENT
            If item.CleObj <> "0" Then
                'Si l'objet est une opération à risques
                If StrComp(item.TypeObj, "OPEDAN") = 0 Or (item.TypeObj.Equals("DINTER") And Lemode = 3) Then
                    'pas de création de jeton
                Else
                    If Not CreateJeton(item) Then
                        exec = False
                        Exit Function
                    End If
                End If
            End If

            Dim ObjM As Object = Nothing

            Select Case UCase(item.TypeObj)
                Case "RAPPEL"
                    ObjM = New Rappel.gestionRappel
                    If droit1(33) = "1" And droit2(33) = "1" Then
                        droit_ok = True
                    Else
                        droit_ok = False
                    End If
                Case "MVTPERSO"
                    ObjM = New MvtPers.MvtPersonnel
                    If droit1(34) = "1" And droit2(34) = "1" Then
                        droit_ok = True
                    Else
                        droit_ok = False
                    End If
                Case "MVTMAT"
                    ObjM = New MvtMat.Rapport
                    If droit1(35) = "1" And droit2(35) = "1" Then
                        droit_ok = True
                    Else
                        droit_ok = False
                    End If
                Case "DIV"
                    ObjM = New MvtDiv.gestiondiverses
                    If droit1(36) = "1" And droit2(36) = "1" Then
                        droit_ok = True
                    Else
                        droit_ok = False
                    End If
                Case "OPEMAIN"
                    ObjM = New OpeMaintenance.OperationMaintenance
                    If droit1(37) = "1" And droit2(37) = "1" Then
                        droit_ok = True
                    Else
                        droit_ok = False
                    End If
                Case "DINTER"
                    ObjM = New Intervention.DemIntervention(item.EstEncours)
                    droit_ok = True
                Case "RONDE"
                    ObjM = New rondes.Ronde()
                    If droit1(38) = "1" And droit2(38) = "1" Then
                        droit_ok = True
                    Else
                        droit_ok = False
                    End If
                Case "RELEVE"
                    ObjM = New gestionReleve.gestionReleve()
                    If droit1(41) = "1" And droit2(41) = "1" Then
                        droit_ok = True
                    Else
                        droit_ok = False
                    End If
                Case "OPEDAN"
                    ObjM = New OpeDanger.OpeDanger()
                    If droit1(42) = "1" And droit2(42) = "1" Then
                        droit_ok = True
                    Else
                        droit_ok = False
                    End If
                Case "DIT"
                    ObjM = New DIT.DIT()
                    droit_ok = True
                Case "DIA"
                    ObjM = New DIA.DemInterAero(item.EstEncours)
                    droit_ok = True
                Case "SLVP"
                    ObjM = New LVP.LVP()
                    droit_ok = True
            End Select

            If droit_ok Then
                If UCase(item.TypeObj) = "DINTER" Then
                    ObjM.Init(ClassParent.FichierIni, Lemode, item.CleObj, ClassParent.Stationnaire, ClassParent.ModeChrono, ClassParent.IDMachine)
                Else
                    If UCase(item.TypeObj) = "OPEMAIN" Or UCase(item.TypeObj) = "DIA" Or UCase(item.TypeObj) = "DIT" Or UCase(item.TypeObj) = "SLVP" Then
                        ObjM.Init(ClassParent.FichierIni, Lemode, item.CleObj, ClassParent.Stationnaire, ClassParent.IDMachine)
                    Else
                        ObjM.Init(ClassParent.FichierIni, Lemode, item.CleObj, ClassParent.Stationnaire)
                    End If
                End If
                ObjM.Execute()
                ObjM = Nothing
            End If

            '----------------------------------------------------------
            '-- SNGVK Ajout de la gestion des jeton pour l'accès à MC--
            '-- Fait le 14/09/2004 suite à FA sur des erreur d'accès --
            '----------------------------------------------------------

            ' Modif ASE le 24/07/2007 suite à la création des opérations à risques
            ' Si c'est un OR : pas de jeton
            If StrComp(item.TypeObj, "OPEDAN") <> 0 Then
                libereJeton(item)
            End If
            '----------------------------------------------------------
            '-- FIN d'ajout SNGVK                                    --
            '----------------------------------------------------------
        Catch ex As Exception
            DevExpress.XtraEditors.XtraMessageBox.Show(Err.Description, Application.ProductName, MessageBoxButtons.OK)
            exec = False
        End Try
    End Function

    Private Function CreateJeton(ByVal row As grid_row) As Boolean
        ClassParent.refreshJetons()
        CreateJeton = ClassParent.MetEnplaceJetonEXEC(row.TypeObj & "_" & row.CleObj)
    End Function

    Private Sub libereJeton(ByVal row As grid_row)
        ClassParent.LibereJetonEXE(row)
    End Sub

    Private Sub Picture_Histo_Click(ByVal sender As Object, ByVal e As EventArgs) Handles Picture_Histo.Click
        TimerBCL.Enabled = False


        Dim Obj As grid_row = GridView1.GetRow(GridView1.FocusedRowHandle)
        If StrComp(Obj.TypeObj, "OPEDAN") = 0 Then
            If exec(2, Obj) Then
                charger_grids(LeFiltre)
            End If
        Else
            If DevExpress.XtraEditors.XtraMessageBox.Show(ClassParent.ListeMessages("MESS_MC_11").Libelle, Application.ProductName, MessageBoxButtons.YesNo) = vbYes Then
                Supprime(Obj)
                charger_grids(LeFiltre)
            End If
        End If

        TimerBCL.Enabled = True
    End Sub

    Friend Sub Supprime(ByVal row As grid_row)
        'Si l'objet est une opération à risques

        Dim droit_ok As Boolean

        If StrComp(row.TypeObj, "OPEDAN") <> 0 Then
            If Not CreateJeton(row) Then
                Exit Sub
            End If
        End If

        Try

            Dim ObjM As Object = Nothing

            Select Case UCase(row.TypeObj)
                Case "RAPPEL"
                    ObjM = New Rappel.gestionRappel
                    If droit1(33) = "1" And droit2(33) = "1" Then
                        droit_ok = True
                    Else
                        droit_ok = False
                    End If
                Case "MVTPERSO"
                    ObjM = New MvtPers.MvtPersonnel
                    If droit1(34) = "1" And droit2(34) = "1" Then
                        droit_ok = True
                    Else
                        droit_ok = False
                    End If
                Case "MVTMAT"
                    ObjM = New MvtMat.Rapport
                    If droit1(35) = "1" And droit2(35) = "1" Then
                        droit_ok = True
                    Else
                        droit_ok = False
                    End If
                Case "DIV"
                    ObjM = New MvtDiv.gestiondiverses
                    If droit1(36) = "1" And droit2(36) = "1" Then
                        droit_ok = True
                    Else
                        droit_ok = False
                    End If
                Case "OPEMAIN"
                    ObjM = New OpeMaintenance.OperationMaintenance
                    If droit1(37) = "1" And droit2(37) = "1" Then
                        droit_ok = True
                    Else
                        droit_ok = False
                    End If
                Case "DINTER"
                    ObjM = New Intervention.DemIntervention()
                    droit_ok = True
                Case "RONDE"
                    ObjM = New rondes.Ronde()
                    If droit1(38) = "1" And droit2(38) = "1" Then
                        droit_ok = True
                    Else
                        droit_ok = False
                    End If
                Case "RELEVE"
                    ObjM = New gestionReleve.gestionReleve()
                    If droit1(41) = "1" And droit2(41) = "1" Then
                        droit_ok = True
                    Else
                        droit_ok = False
                    End If
                Case "OPEDAN"
                    ObjM = New OpeDanger.OpeDanger()
                    If droit1(42) = "1" And droit2(42) = "1" Then
                        droit_ok = True
                    Else
                        droit_ok = False
                    End If
                Case "DIT"
                    ObjM = New DIT.DIT()
                    droit_ok = True
                Case "DIA"
                    ObjM = New DIA.DemInterAero()
                    droit_ok = True
                Case "SLVP"
                    ObjM = New LVP.LVP()
                    droit_ok = True
            End Select

            If droit_ok Then
                If DevExpress.XtraEditors.XtraMessageBox.Show(ClassParent.ListeMessages("MESS_MC_5").Libelle, Application.ProductName, MessageBoxButtons.YesNo) = vbYes Then
                    Select Case UCase(row.TypeObj)
                        Case "DINTER"
                            ObjM.Init(ClassParent.FichierIni, 4, row.CleObj, ClassParent.Stationnaire, , ClassParent.IDMachine)
                        Case "DIA"
                            ObjM.Init(ClassParent.FichierIni, 4, row.CleObj, ClassParent.Stationnaire, ClassParent.IDMachine)
                        Case "OPEMAIN", "DIT", "SLVP"
                            ObjM.Init(ClassParent.FichierIni, 1, row.CleObj, ClassParent.Stationnaire, ClassParent.IDMachine)
                        Case Else
                            ObjM.Init(ClassParent.FichierIni, 1, row.CleObj, ClassParent.Stationnaire)
                    End Select

                    ObjM.Supprime()
                    'DoEvents
                    ObjM = Nothing
                End If
            End If

            ' Si c'est un SAR : pas de jeton
            If StrComp(row.TypeObj, "OPEDAN") <> 0 Then
                libereJeton(row)
            End If
        Catch ex As Exception
            DevExpress.XtraEditors.XtraMessageBox.Show(Err.Description, Application.ProductName, MessageBoxButtons.OK)
        End Try
    End Sub

    Private Function Retourne_IdInter(ByVal CleDemInter As Long) As Long
        Dim data As ADODB.Recordset

        data = ClassParent.connect_ADO.ADODB_create_recordset(2, "SELECT DEM_MOTIF.CleMotif FROM DEM_MOTIF WHERE DEM_MOTIF.EstActif<>0 AND DEM_MOTIF.CleDemIntervention=" & CleDemInter)
        If Not data.EOF Then
            Retourne_IdInter = "" & data.Fields(0).Value
        Else
            Retourne_IdInter = 0
        End If
        data.Close()
        data = Nothing
    End Function

    Private Function Retourne_Couleur(ByVal CleDemInter As Long) As Long
        Dim data As ADODB.Recordset
        Retourne_Couleur = 16777215

        data = ClassParent.connect_ADO.ADODB_create_recordset(2, "SELECT CATEGORIES_INTERVENTIONS.CodeCouleur FROM CATEGORIES_INTERVENTIONS")
        If Not data.EOF Then
            Retourne_Couleur = "" & data.Fields(0).Value
        End If
        data.Close()
        data = Nothing
        'ClassParent.connect_ADO.ADODB_deconnection(2)
    End Function

    Private Function IDMAchine_Retour(ByVal CleRappel As Long, Optional ByRef IdMail_Ret As Integer = 0) As String
        On Error GoTo IDMAchine_Retour_erreur
        Dim data As ADODB.Recordset
        IDMAchine_Retour = ""
        data = ClassParent.connect_ADO.ADODB_create_recordset(2, "SELECT IDMachine, IDMail FROM RAPPEL WHERE IDRappel = " & CleRappel)
        If Not data.EOF Then
            IDMAchine_Retour = "" & data.Fields(0).Value
            IdMail_Ret = data.Fields(1).Value
        Else
            IDMAchine_Retour = 0
            IdMail_Ret = 0
        End If
        data.Close()
        data = Nothing
        Exit Function
IDMAchine_Retour_erreur:
        IDMAchine_Retour = "0"
    End Function

    Private Sub CMD_Alerte_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CMD_Alerte.Click
        Me.Timer1.Enabled = False
        ClassParent.Warn.Masquer()
    End Sub

    Private Sub BtnReleveStationnaire_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnReleveStationnaire.Click
        TimerBCL.Enabled = False
        Call MnuReleveStationnaire_Click()
    End Sub

    Private Sub GridEvent_MouseDown(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles GridEvent.MouseDown
        If e.Button = Windows.Forms.MouseButtons.Left Then
            mMouseHitPoint = New Point(e.X, e.Y)
        Else
            mMouseHitPoint = Nothing
        End If

    End Sub

    Private Sub GridBrouillon_MouseDown(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles GridBrouillon.MouseDown
        If e.Button = Windows.Forms.MouseButtons.Left Then
            mMouseHitPoint = New Point(e.X, e.Y)
        Else
            mMouseHitPoint = Nothing
        End If

    End Sub

    Private Sub GridEvent_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles GridEvent.DoubleClick

        Dim SQL As String
        Dim droit1 As String
        Dim droit2 As String


        Dim info As DevExpress.XtraGrid.Views.Grid.ViewInfo.GridHitInfo
        If Not IsNothing(mMouseHitPoint) Then
            info = GridView1.CalcHitInfo(mMouseHitPoint)
            If info.InRow Then
                Dim curRow As Integer = GridView1.FocusedRowHandle
                'Do your work here 
                'If Not TimerBCL.Enabled Then
                '    '    Exit Sub
                'End If
                TimerBCL.Enabled = False
                Dim Obj As grid_row
                Obj = GridView1.GetRow(curRow)

                If Obj.TypeObj.Equals("DINTER") Or Obj.TypeObj.Equals("DIA") Then
                    Dim rs As ADODB.Recordset
                    If ClassParent.connect_ADO.ADODB_connection(1) And ClassParent.connect_ADO.ADODB_connection(2) Then
                        SQL = "SELECT droit FROM NIVEAUX_UTILISATEUR, UTILISATEURS WHERE IDUtilisateur= " & ClassParent.Stationnaire & " AND levelU=IdNiveauxUtilisateurs"
                        rs = ClassParent.connect_ADO.ADODB_create_recordset(1, SQL)
                        droit1 = rs.Fields(0).Value
                        rs.Close()
                        SQL = "SELECT droit FROM MACHINE WHERE IDMachine= " & _iD_MACHINE
                        rs = ClassParent.connect_ADO.ADODB_create_recordset(2, SQL)
                        droit2 = rs.Fields(0).Value
                        rs.Close()
                        If droit1(39) = "1" And droit2(39) = "1" Then
                            exec(1, Obj)
                        Else
                            exec(3, Obj)
                        End If
                        charger_grids(LeFiltre)
                    Else
                        DevExpress.XtraEditors.XtraMessageBox.Show(ClassParent.ListeMessages("MESS_ERRBDD").Libelle, Application.ProductName, MessageBoxButtons.OK)
                    End If
                Else
                    If exec(1, Obj) Then
                        charger_grids(LeFiltre)
                    End If
                End If
                'DoEvents
                TimerBCL.Enabled = True
            End If
        End If
    End Sub

    Private Sub GridBrouillon_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles GridBrouillon.DoubleClick
        Dim info As DevExpress.XtraGrid.Views.Grid.ViewInfo.GridHitInfo
        If Not IsNothing(mMouseHitPoint) Then
            info = GridView2.CalcHitInfo(mMouseHitPoint)
            If info.InRow Then
                Dim curRow As Integer = GridView2.FocusedRowHandle
                'Do your work here 
                If Not TimerBCL.Enabled Then
                    Exit Sub
                End If
                TimerBCL.Enabled = False
                On Error Resume Next

                Dim Obj As grid_row
                Obj = GridView2.GetRow(curRow)

                If exec(2, Obj) Then
                    charger_grids(LeFiltre)
                End If

                'DoEvents
                TimerBCL.Enabled = True
            End If
        End If
    End Sub

    Private Sub GridBrouillon_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles GridBrouillon.Click
        Dim info As DevExpress.XtraGrid.Views.Grid.ViewInfo.GridHitInfo
        If Not IsNothing(mMouseHitPoint) Then
            info = GridView2.CalcHitInfo(mMouseHitPoint)
            If info.InRow And Not IsNothing(info.Column) Then
                If info.Column.FieldName = "Annuler" Then
                    Dim curRow As Integer = GridView2.FocusedRowHandle
                    'Do your work here 
                    TimerBCL.Enabled = False
                    Dim Obj As grid_row
                    Obj = GridView2.GetRow(curRow)

                    If StrComp(Obj.TypeObj, "OPEDAN") = 0 Then
                        If droit1(41) = "1" And droit2(41) = "1" Then
                            If exec(2, Obj) Then
                                charger_grids(LeFiltre)
                            End If
                        End If


                    Else
                        Supprime(Obj)
                        charger_grids(LeFiltre)
                    End If
                    TimerBCL.Enabled = True
                End If
            End If
        End If
    End Sub

    Private Sub GridEvent_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles GridEvent.Click
        Dim info As DevExpress.XtraGrid.Views.Grid.ViewInfo.GridHitInfo
        If Not IsNothing(mMouseHitPoint) Then
            info = GridView1.CalcHitInfo(mMouseHitPoint)
            If info.InRow And Not IsNothing(info.Column) Then
                If info.Column.FieldName = "Biffage" Then
                    Dim curRow As Integer = GridView1.FocusedRowHandle
                    'Do your work here 
                    TimerBCL.Enabled = False
                    Dim Obj As grid_row
                    Obj = GridView1.GetRow(curRow)
                    If StrComp(UCase(Obj.TypeObj), "OPEDAN") = 0 Then
                        If droit1(41) = "1" And droit2(41) = "1" Then
                            If exec(1, Obj) Then
                                charger_grids(LeFiltre)
                            End If
                        End If
                    Else
                        Supprime(Obj)
                        System.Windows.Forms.Application.DoEvents()
                        charger_grids(LeFiltre)
                        TimerBCL.Enabled = True
                    End If
                End If
            End If
        End If
        System.Windows.Forms.Application.DoEvents()
        GridEvent.RefreshDataSource()
    End Sub

    Private Sub btnDispos_Click(sender As System.Object, e As System.EventArgs) Handles btnDispos.Click
        Dim feuille As New frmListeDispos(Me)
        If feuille.Init() Then
            feuille.ShowDialog()
        End If
    End Sub

    Private Sub btnTraka_Click(sender As System.Object, e As System.EventArgs) Handles btnTraka.Click
        btnTraka.Enabled = False
        Me.ClassParent.LancerTraka()
        btnTraka.Enabled = True
    End Sub

    Private Sub btnFiches_Click(sender As System.Object, e As System.EventArgs) Handles btnFiches.Click
        btnFiches.Enabled = False
        documentation = New doc.Doc(_fichierIni, "MC", ClassParent.IsSSLIA)
        Dim docs As frm_Doc = New frm_Doc(Me, documentation)
        docs.ShowDialog()
        btnFiches.Enabled = True
    End Sub

    Private Sub btnRondier_Click(sender As System.Object, e As System.EventArgs) Handles btnRondier.Click
        Try
            Process.Start(ClassParent.PathRondierExe)
        Catch ex As Exception
            DevExpress.XtraEditors.XtraMessageBox.Show(ClassParent.ListeMessages("MESS_ERR_PATH_RONDIER").Libelle, Application.ProductName, MessageBoxButtons.OK)
        End Try

    End Sub

    Private Sub frmEcran_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        TimerBCL.Enabled = True
        charger_grids("")
    End Sub

    Private Sub cboFiltre_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles cboFiltre.SelectedIndexChanged
        LeFiltre = cboFiltre.SelectedItem.nom
        charger_grids(cboFiltre.SelectedItem.nom())
    End Sub

    Private Sub btnVent_Click(sender As System.Object, e As System.EventArgs) Handles btnVent.Click
        Dim feuille As New frmlvp(Me.ClassParent)
        feuille.Init()
        feuille.ShowDialog()
        feuille = Nothing
    End Sub

    Private Sub btnLvp_Click(sender As Object, e As System.EventArgs) Handles btnLvp.Click
        Dim feuille As New frmlvp(Me.ClassParent)
        feuille.Init()
        feuille.ShowDialog()
        feuille = Nothing
    End Sub

    Private Sub btnDoublet_Click(sender As Object, e As System.EventArgs) Handles btnDoublet.Click
        Dim feuille As New frmlvp(Me.ClassParent)
        feuille.Init()
        feuille.ShowDialog()
        feuille = Nothing
    End Sub


End Class