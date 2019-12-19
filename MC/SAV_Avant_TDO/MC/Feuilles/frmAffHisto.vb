Imports System.Collections.Generic
Imports System.Windows.Forms
Imports System.Drawing
Imports DevExpress.XtraPrinting
Imports DevExpress.XtraGrid
Imports DevExpress.XtraGrid.Views.Grid

Public Class frmAffHisto

    'Friend connect_ADO As New connect_ADODB

    Private _fichierIni As String
    Private _iD_MACHINE As Integer
    Private _iD_USER As Integer

    Private Datd As Double
    Private DatF As Double
    Friend listnew As List(Of grid_row)
    Public LimiteList As Boolean
    Private IsActivate As Boolean
    Private IsBiffer As Boolean

    Friend lbl_nofiltre As String

    Private LeFiltre As String

    Friend classParent As Init_MC
    Private print_subtitle As String
    Private print_title As String

    Friend droit1 As String = ""
    Friend SAR_OK As Boolean
    Friend droit2 As String = ""

    Dim mMouseHitPoint As Point

    Friend Is_Commentaire_DI As Boolean = False

    Sub New(ByVal FichierIni As String, ByVal ID_MACHINE As Integer, ByVal ID_USER As Integer, ByRef cParent As Init_MC)
        InitializeComponent()
        ' TODO: Complete member initialization 
        _fichierIni = FichierIni
        _iD_MACHINE = ID_MACHINE
        _iD_USER = ID_USER

        classParent = cParent

        'Skins
        DevExpress.Skins.SkinManager.EnableFormSkins()
        DevExpress.LookAndFeel.LookAndFeelHelper.ForceDefaultLookAndFeelChanged()
        DevExpress.UserSkins.BonusSkins.Register()
        DevExpress.UserSkins.OfficeSkins.Register()

        TxtDateDeb.ContextMenuStrip = New ContextMenuStrip
        TxtDateFin.ContextMenuStrip = New ContextMenuStrip
        TxtHeureD.ContextMenuStrip = New ContextMenuStrip
        TxtHeureF.ContextMenuStrip = New ContextMenuStrip

        IsBiffer = False

        charger_labels()

        GridEvent.DataSource = listnew
    End Sub

    Private Property TimerBCL As Object

    Private Sub charger_labels()
        Dim rs As ADODB.Recordset
        If classParent.connect_ADO.ADODB_connection(1) Then

            rs = classParent.connect_ADO.ADODB_create_recordset(1, "SELECT * FROM LANGUE WHERE IdObjet='MC' AND IdLangue='TitreFenetreHisto'")
            Me.Text = rs.Fields("Libelle").Value
            rs.Close()

            rs = classParent.connect_ADO.ADODB_create_recordset(1, "SELECT * FROM LANGUE WHERE IdObjet='MC' AND IdLangue='BTN_HISTO'")
            Me.GrpHisto.Text = rs.Fields("Libelle").Value
            rs.Close()

            rs = classParent.connect_ADO.ADODB_create_recordset(1, "SELECT * FROM LANGUE WHERE IdObjet='MC' AND IdLangue='LBL_DATE_DEB'")
            Me.LblDateD.Text = rs.Fields("Libelle").Value
            rs.Close()

            rs = classParent.connect_ADO.ADODB_create_recordset(1, "SELECT * FROM LANGUE WHERE IdObjet='MC' AND IdLangue='LBL_HEURE_DEB'")
            Me.LblHeureD.Text = rs.Fields("Libelle").Value
            rs.Close()

            rs = classParent.connect_ADO.ADODB_create_recordset(1, "SELECT * FROM LANGUE WHERE IdObjet='MC' AND IdLangue='LBL_DATE_FIN'")
            Me.LblDateF.Text = rs.Fields("Libelle").Value
            rs.Close()

            rs = classParent.connect_ADO.ADODB_create_recordset(1, "SELECT * FROM LANGUE WHERE IdObjet='MC' AND IdLangue='LBL_HEURE_FIN'")
            Me.LblHeureF.Text = rs.Fields("Libelle").Value
            rs.Close()

            rs = classParent.connect_ADO.ADODB_create_recordset(1, "SELECT * FROM LANGUE WHERE IdObjet='MC' AND IdLangue='BTN_AFF_HISTO'")
            Me.BtnREch.Text = rs.Fields("Libelle").Value
            rs.Close()

            rs = classParent.connect_ADO.ADODB_create_recordset(1, "SELECT * FROM LANGUE WHERE IdObjet='MC' AND IdLangue='GRP_FILTRE'")
            Me.GrpFiltre.Text = rs.Fields("Libelle").Value
            rs.Close()

            rs = classParent.connect_ADO.ADODB_create_recordset(1, "SELECT * FROM LANGUE WHERE IdObjet='MC' AND IdLangue='CHK_BIFF'")
            Me.LblCheck.Text = rs.Fields("Libelle").Value
            rs.Close()

            rs = classParent.connect_ADO.ADODB_create_recordset(1, "SELECT * FROM LANGUE WHERE IdObjet='MC' AND IdLangue='BTN_RECHERCHEA'")
            Me.BtnRechercheA.Text = rs.Fields("Libelle").Value
            rs.Close()

            rs = classParent.connect_ADO.ADODB_create_recordset(1, "SELECT * FROM LANGUE WHERE IdObjet='MC' AND IdLangue='GRP_AFF_HISTO'")
            Me.GrpAffHisto.Text = rs.Fields("Libelle").Value
            rs.Close()

            rs = classParent.connect_ADO.ADODB_create_recordset(1, "SELECT * FROM LANGUE WHERE IdObjet='MC' AND IdLangue='BTN_IMPR_LST'")
            Me.BtnImpListe.Text = rs.Fields("Libelle").Value
            rs.Close()

            rs = classParent.connect_ADO.ADODB_create_recordset(1, "SELECT * FROM LANGUE WHERE IdObjet='GENERIQUE' AND IdLangue='BTN_FERMER'")
            Me.BtnFermer.Text = rs.Fields("Libelle").Value
            rs.Close()

            rs = classParent.connect_ADO.ADODB_create_recordset(1, "SELECT * FROM LANGUE WHERE IdObjet='MC' AND IdLangue='CLN_CHEF'")
            Me.CLN_OPERATEUR.Caption = rs.Fields("Libelle").Value
            rs.Close()

            rs = classParent.connect_ADO.ADODB_create_recordset(1, "SELECT * FROM LANGUE WHERE IdObjet='MC' AND IdLangue='CLN_DATE'")
            Me.CLN_DATE.Caption = rs.Fields("Libelle").Value
            rs.Close()

            rs = classParent.connect_ADO.ADODB_create_recordset(1, "SELECT * FROM LANGUE WHERE IdObjet='MC' AND IdLangue='CLN_HEURE'")
            Me.CLN_HEURE.Caption = rs.Fields("Libelle").Value
            rs.Close()

            rs = classParent.connect_ADO.ADODB_create_recordset(1, "SELECT * FROM LANGUE WHERE IdObjet='MC' AND IdLangue='CLN_TITRE_EVENT'")
            Me.CLN_TITRE.Caption = rs.Fields("Libelle").Value
            rs.Close()

            If classParent.connect_ADO.ADODB_connection(2) Then
                rs = classParent.connect_ADO.ADODB_create_recordset(1, "SELECT droit FROM NIVEAUX_UTILISATEUR, UTILISATEURS WHERE IDUtilisateur= " & _iD_USER & " AND levelU=IdNiveauxUtilisateurs")
                droit1 = rs.Fields(0).Value
                rs.Close()
                rs = classParent.connect_ADO.ADODB_create_recordset(2, "SELECT droit FROM MACHINE WHERE IDMachine= " & _iD_MACHINE)
                droit2 = rs.Fields(0).Value
                rs.Close()
            End If

            rs = classParent.connect_ADO.ADODB_create_recordset(1, "SELECT * FROM LANGUE WHERE IdObjet='MC' AND IdLangue='NOFILTRE'")
            lbl_nofiltre = rs.Fields("IdLangue").Value
            cboFiltre.Properties.Items.Add(New Data_Set(rs.Fields("Libelle").Value, rs.Fields("IdLangue").Value, rs.Fields("Type").Value))
            rs.Close()

            rs = classParent.connect_ADO.ADODB_create_recordset(1, "SELECT * FROM LANGUE WHERE IdObjet='MC' AND IdLangue='RAPPEL'")
            cboFiltre.Properties.Items.Add(New Data_Set(rs.Fields("Libelle").Value, rs.Fields("IdLangue").Value, rs.Fields("Type").Value))
            rs.Close()

            rs = classParent.connect_ADO.ADODB_create_recordset(1, "SELECT * FROM LANGUE WHERE IdObjet='MC' AND IdLangue='MVTPERSO'")
            cboFiltre.Properties.Items.Add(New Data_Set(rs.Fields("Libelle").Value, rs.Fields("IdLangue").Value, rs.Fields("Type").Value))
            rs.Close()

            rs = classParent.connect_ADO.ADODB_create_recordset(1, "SELECT * FROM LANGUE WHERE IdObjet='MC' AND IdLangue='MVTMAT'")
            cboFiltre.Properties.Items.Add(New Data_Set(rs.Fields("Libelle").Value, rs.Fields("IdLangue").Value, rs.Fields("Type").Value))
            rs.Close()

            rs = classParent.connect_ADO.ADODB_create_recordset(1, "SELECT * FROM LANGUE WHERE IdObjet='MC' AND IdLangue='DIV'")
            cboFiltre.Properties.Items.Add(New Data_Set(rs.Fields("Libelle").Value, rs.Fields("IdLangue").Value, rs.Fields("Type").Value))
            rs.Close()

            rs = classParent.connect_ADO.ADODB_create_recordset(1, "SELECT * FROM LANGUE WHERE IdObjet='MC' AND IdLangue='OPEMAIN'")
            cboFiltre.Properties.Items.Add(New Data_Set(rs.Fields("Libelle").Value, rs.Fields("IdLangue").Value, rs.Fields("Type").Value))
            rs.Close()

            rs = classParent.connect_ADO.ADODB_create_recordset(1, "SELECT * FROM LANGUE WHERE IdObjet='MC' AND IdLangue='DINTER'")
            cboFiltre.Properties.Items.Add(New Data_Set(rs.Fields("Libelle").Value, rs.Fields("IdLangue").Value, rs.Fields("Type").Value))
            rs.Close()


            rs = classParent.connect_ADO.ADODB_create_recordset(1, "SELECT * FROM LANGUE WHERE IdObjet='MC' AND IdLangue='RONDE'")
            cboFiltre.Properties.Items.Add(New Data_Set(rs.Fields("Libelle").Value, rs.Fields("IdLangue").Value, rs.Fields("Type").Value))
            rs.Close()

            rs = classParent.connect_ADO.ADODB_create_recordset(1, "SELECT * FROM LANGUE WHERE IdObjet = 'GENERIQUE' AND IdLangue='SAR_OK'")
            SAR_OK = CBool(rs.Fields("Type").Value)
            rs.Close()

            If SAR_OK Then
                rs = classParent.connect_ADO.ADODB_create_recordset(1, "SELECT * FROM LANGUE WHERE IdObjet='MC' AND IdLangue='OPEDAN'")
                cboFiltre.Properties.Items.Add(New Data_Set(rs.Fields("Libelle").Value, rs.Fields("IdLangue").Value, rs.Fields("Type").Value))
                rs.Close()
            End If

            rs = classParent.connect_ADO.ADODB_create_recordset(1, "SELECT * FROM LANGUE WHERE IdObjet='MC' AND IdLangue='RELEVEDECO'")
            cboFiltre.Properties.Items.Add(New Data_Set(rs.Fields("Libelle").Value, rs.Fields("IdLangue").Value, rs.Fields("Type").Value))
            rs.Close()

            rs = classParent.connect_ADO.ADODB_create_recordset(1, "SELECT * FROM LANGUE WHERE IdObjet='MC' AND IdLangue='RELEVECO'")
            cboFiltre.Properties.Items.Add(New Data_Set(rs.Fields("Libelle").Value, rs.Fields("IdLangue").Value, rs.Fields("Type").Value))
            rs.Close()

            rs = classParent.connect_ADO.ADODB_create_recordset(1, "SELECT * FROM LANGUE WHERE IdObjet='MC' AND IdLangue='RELEVE'")
            cboFiltre.Properties.Items.Add(New Data_Set(rs.Fields("Libelle").Value, rs.Fields("IdLangue").Value, rs.Fields("Type").Value))
            rs.Close()

            If classParent.IsRIT Then
                rs = classParent.connect_ADO.ADODB_create_recordset(1, "SELECT * FROM LANGUE WHERE IdObjet='MC' AND IdLangue='DIT'")
                cboFiltre.Properties.Items.Add(New Data_Set(rs.Fields("Libelle").Value, rs.Fields("IdLangue").Value, rs.Fields("Type").Value))
                rs.Close()
            End If

            If classParent.IsSSLIA Then
                rs = classParent.connect_ADO.ADODB_create_recordset(1, "SELECT * FROM LANGUE WHERE IdObjet='MC' AND IdLangue='DIA'")
                If Not rs.EOF Then
                    cboFiltre.Properties.Items.Add(New Data_Set(rs.Fields("Libelle").Value, rs.Fields("IdLangue").Value, rs.Fields("Type").Value))
                End If
                rs.Close()
                rs = classParent.connect_ADO.ADODB_create_recordset(1, "SELECT * FROM LANGUE WHERE IdObjet='MC' AND IdLangue='SLVP'")
                If Not rs.EOF Then
                    cboFiltre.Properties.Items.Add(New Data_Set(rs.Fields("Libelle").Value, rs.Fields("IdLangue").Value, rs.Fields("Type").Value))
                End If
                rs.Close()
                rs = classParent.connect_ADO.ADODB_create_recordset(1, "SELECT * FROM LANGUE WHERE IdObjet='MC' AND IdLangue='LVP'")
                If Not rs.EOF Then
                    cboFiltre.Properties.Items.Add(New Data_Set(rs.Fields("Libelle").Value, rs.Fields("IdLangue").Value, rs.Fields("Type").Value))
                End If
                rs.Close()
            End If

            rs = classParent.connect_ADO.ADODB_create_recordset(1, "SELECT * FROM LANGUE WHERE IdObjet='MC' AND IdLangue='PRINT_SUBTITLE'")
            print_subtitle = rs.Fields("Libelle").Value
            rs.Close()

            rs = classParent.connect_ADO.ADODB_create_recordset(1, "SELECT * FROM LANGUE WHERE IdObjet='MC' AND IdLangue='PRINT_TITLE'")
            print_title = rs.Fields("Libelle").Value
            rs.Close()

            rs = classParent.connect_ADO.ADODB_create_recordset(1, "SELECT * FROM LANGUE WHERE IdObjet='GENERIQUE' AND IdLangue='TYPEDATE'")

            TypeDate = UCase(rs.Fields("Libelle").Value)

            rs.Close()

            rs = classParent.connect_ADO.ADODB_create_recordset(1, "SELECT * FROM LANGUE WHERE IdObjet='GENERIQUE' AND IdLangue='TYPEHEURE'")

            TypeHeure = UCase(rs.Fields("Libelle").Value)
            rs.Close()

            rs = classParent.connect_ADO.ADODB_create_recordset(1, "SELECT * FROM ADMIN WHERE IdObjet='COMM_DI'")
            If Not rs.EOF Then
                Is_Commentaire_DI = rs.Fields("Valeur").Value
                rs.Close()
            End If

            classParent.connect_ADO.ADODB_deconnection(1)
            classParent.connect_ADO.ADODB_deconnection(2)

            OLSDATE.Parametrer_DateEdit(TxtDateDeb)
            OLSDATE.Parametrer_DateEdit(TxtDateFin)
            OLSDATE.Parametrer_TimeEdit(TxtHeureD)
            OLSDATE.Parametrer_TimeEdit(TxtHeureF)

            Me.GridView2.Columns.Item(2).Visible = False

        End If

    End Sub


    Private Sub BtnFermer_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnFermer.Click
        Me.Close()
    End Sub

    Private Sub Print_SetHeader(sender As System.Object, e As DevExpress.XtraPrinting.CreateAreaEventArgs) Handles PrintableComponentLink1.CreateReportHeaderArea
        Dim reportHeader As String = print_title
        e.Graph.StringFormat = New BrickStringFormat(StringAlignment.Center)
        e.Graph.Font = New Font("Tahoma", 14, FontStyle.Bold)
        Dim rec As RectangleF = New RectangleF(0, 0, e.Graph.ClientPageSize.Width, 50)
        e.Graph.DrawString(reportHeader, Color.Black, rec, BorderSide.None)


        reportHeader = print_subtitle
        Dim dd As String = creerdatedebut()
        Dim df As String = creerdatefin()
        reportHeader = Replace(reportHeader, "#1", dd)
        reportHeader = Replace(reportHeader, "#2", df)
        e.Graph.StringFormat = New BrickStringFormat(StringAlignment.Center)
        e.Graph.Font = New Font("Tahoma", 13, FontStyle.Regular)
        rec = New RectangleF(0, 40, e.Graph.ClientPageSize.Width, 50)
        e.Graph.DrawString(reportHeader, Color.Black, rec, BorderSide.None)

    End Sub

    Private Sub BtnImpListe_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnImpListe.Click

        'GridEvent.ShowPrintPreview()

        PrintableComponentLink1.CreateDocument()
        PrintableComponentLink1.ShowPreview()

    End Sub

    Private Sub BtnREch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnREch.Click

        IsBiffer = LblCheck.Checked


        If TxtDateDeb.DateTime.ToOADate.ToString > 0 Then
            '        Datd = CDbl(CDate(TxtDateDeb.Text))
            Datd = TxtDateDeb.DateTime.ToOADate
            If TxtHeureD.Text <> "" Then
                '        Datd = Datd + CDbl(CDate(TxtHeureD.Text))
                Datd = OLSDATE.timeToLong(Date.FromOADate(Datd), OLSDATE.Time12ToTime24(TxtHeureD.Text))
            Else
                Datd = OLSDATE.timeToLong(Date.FromOADate(Datd), OLSDATE.Time12ToTime24("00:00"))
            End If
        Else
            Datd = -1
        End If


        If TxtDateFin.DateTime.ToOADate > 0 Then
            '        DatF = CDbl(CDate(TxtDateFin.Text))
            DatF = TxtDateFin.DateTime.ToOADate
            If TxtHeureF.Text <> "" Then
                '        Datd = Datd + CDbl(CDate(TxtHeureD.Text))
                DatF = OLSDATE.timeToLong(Date.FromOADate(DatF), OLSDATE.Time12ToTime24(TxtHeureF.Text))
            Else
                DatF = OLSDATE.timeToLong(Date.FromOADate(DatF), OLSDATE.Time12ToTime24("23:59"))
            End If
        Else
            DatF = Date.Now.ToOADate
        End If

        listnew = New List(Of grid_row)
        LimiteList = True
        LectureH(LeFiltre, Datd, DatF, IsBiffer)
    End Sub

    Public Sub LectureH(Optional ByVal LeFiltre As String = "", Optional ByVal DateD As Double = 0, Optional ByVal DateF As Double = 0, Optional ByVal Biffer As Boolean = True, Optional ByVal SqlWhere As String = "")
        '----------------------------------------------------------
        '-- SNGVK Ajout de la gestion des jeton pour l'accès à MC--
        '-- Fait le 14/09/2004 suite à FA sur des erreur d'accès --
        '----------------------------------------------------------
        'If Not classParent.MetEnplaceJetonLecture(False) Then
        '    Exit Sub
        'End If
        '----------------------------------------------------------
        '-- FIN d'ajout SNGVK                                    --
        '----------------------------------------------------------

        Dim data As ADODB.Recordset
        Dim data2 As ADODB.Recordset
        Dim SQL As String
        Dim Obj As grid_row
        classParent.Construct_LstUser()

        If (LeFiltre = "DINTER" And Is_Commentaire_DI) Then
            Me.GridView2.OptionsView.ShowPreview = True
            Me.GridView2.Columns.Item(2).Visible = True
        Else
            If (LeFiltre = "DIV") Then
                Me.GridView2.Columns.Item(2).Visible = False
                Me.GridView2.OptionsView.ShowPreview = True
            Else
                Me.GridView2.Columns.Item(2).Visible = False
                Me.GridView2.OptionsView.ShowPreview = False
            End If
        End If


        listnew.Clear()
        If Not classParent.connect_ADO.ADODB_connection(2) And classParent.connect_ADO.ADODB_connection(1) Then
            DevExpress.XtraEditors.XtraMessageBox.Show(classParent.ListeMessages("MESS_MC_1").Libelle, Application.ProductName, MessageBoxButtons.OK)
            Exit Sub
        End If
        If Biffer Then
            SQL = "SELECT TOP " & classParent.NBMaxHisto & " * FROM MC WHERE ESTENCOURS=false and ESTBROUILLON=false and DateHeureDebut>=" & Replace(DateD, ",", ".") & " and DateHeureDebut<=" & Replace(DateF, ",", ".") & " " '& Parent.OrdreTriEvenement
        Else
            SQL = "SELECT TOP " & classParent.NBMaxHisto & " * FROM MC WHERE ESTENCOURS=false and ESTBROUILLON=false and DateHeureDebut>=" & Replace(DateD, ",", ".") & " and DateHeureDebut<=" & Replace(DateF, ",", ".") & " and estsupp =false " '& Parent.OrdreTriEvenement
        End If

        If LeFiltre <> "" And LeFiltre <> lbl_nofiltre Then
            If LeFiltre = "RELEVECO" Or LeFiltre = "RELEVEDECO" Then
                SqlWhere = SqlWhere & " AND Objet='RELEVE'"
            Else
                SqlWhere = SqlWhere & " AND Objet='" & LeFiltre & "'"
            End If

        End If

        If LeFiltre = "RELEVE" Then
            SqlWhere = SqlWhere & " AND cleobj <> 0"
        ElseIf LeFiltre = "RELEVECO" Then
            SqlWhere = SqlWhere & " AND affichage LIKE 'C%'"
        ElseIf LeFiltre = "RELEVEDECO" Then
            SqlWhere = SqlWhere & " AND affichage LIKE 'D%'"
        End If

        If Not SAR_OK Then
            SqlWhere = SqlWhere & " AND Objet<>'OPEDAN'"
        End If

        If StrComp(SqlWhere, "") <> 0 Then
            SQL = SQL & SqlWhere
        End If

        SQL = SQL & " ORDER BY DateHeureDebut desc "

        Dim compteurhisto As String = 0

        data = classParent.connect_ADO.ADODB_create_recordset(2, SQL)
        Do While Not data.EOF
            Obj = New grid_row(classParent)

            Obj.CleObj = data.Fields("cleobj").Value
            Obj.EstBrouillon = False
            Obj.EstEncours = False
            Obj.EstSupp = CBool(data.Fields("EstSupp").Value)
            Obj.Titre = data.Fields("Affichage").Value
            Obj.TypeObj = data.Fields("Objet").Value
            Obj.DateHeure = data.Fields("DateHeureDebut").Value
            If Datd > Obj.DateHeure Or Datd = -1 Then
                Datd = Obj.DateHeure
            End If
            Obj.IndexEvt = CLng(data.Fields("CleMc").Value)
            Obj.IdOperateur = CLng(data.Fields("CleStationnaire").Value)

            data2 = classParent.connect_ADO.ADODB_create_recordset(1, "SELECT * FROM UTILISATEURS WHERE IDUtilisateur= " & Obj.IdOperateur)
            If Not data2.EOF Then
                Obj.Operateur = UCase(data2.Fields("NomUtilisateur").Value) & " " & data2.Fields("PrenomUtilisateur").Value
            End If
            data2.Close()

            If LeFiltre = "DINTER" And Is_Commentaire_DI Then
                data2 = classParent.connect_ADO.ADODB_create_recordset(2, "SELECT * FROM DEMINTERVENTION WHERE CleDemIntervention= " & Obj.CleObj)
                If Not data2.EOF Then
                    Obj.Commentaire = data2.Fields("Commentaire").Value & ""
                End If
                data2.Close()
                Dim Id_Bat As Integer
                Dim sql_bat As String
                sql_bat = "SELECT DEM_BATIMENT.CleBatiment FROM DEM_BATIMENT WHERE DEM_BATIMENT.CleDEMINTERVENTION = " & Obj.CleObj & " ORDER BY CleDemBatiment DESC"
                data2 = classParent.connect_ADO.ADODB_create_recordset(2, sql_bat)
                If Not data2.EOF Then
                    Id_Bat = data2.Fields(0).Value
                    data2.Close()
                    sql_bat = "SELECT NomBatiment FROM BATIMENTS WHERE BATIMENTS.IDBatiment = " & Id_Bat
                    data2 = classParent.connect_ADO.ADODB_create_recordset(1, sql_bat)
                    If Not data2.EOF Then
                        Obj.Bat = data2.Fields(0).Value & ""
                    End If
                End If
                data2.Close()
            Else
                If LeFiltre = "DIV" Then
                    data2 = classParent.connect_ADO.ADODB_create_recordset(2, "SELECT * FROM DATADIVERS WHERE CleHisto=" & Obj.CleObj & " ORDER BY NumOnglet Desc")
                    If Not data2.EOF Then
                        Dim Texte_Temp As String
                        Texte_Temp = data2.Fields("TexteHistorique").Value & ""
                        Texte_Temp = Replace(Texte_Temp, vbNewLine, "-") & ""
                        If Texte_Temp.Length > 255 Then
                            Obj.Commentaire = Texte_Temp.Substring(0, 254)
                        Else
                            Obj.Commentaire = Texte_Temp
                        End If
                    End If
                    data2.Close()
                Else
                    If LeFiltre = "DIT" Then
                        data2 = classParent.connect_ADO.ADODB_create_recordset(2, "SELECT * FROM DIT WHERE idDIT=" & Obj.CleObj)
                        If Not data2.EOF Then
                            Dim Texte_Temp As String
                            Texte_Temp = data2.Fields("observation").Value & ""
                            Texte_Temp = Replace(Texte_Temp, vbNewLine, "-") & ""
                            If Texte_Temp.Length > 255 Then
                                Obj.Commentaire = Texte_Temp.Substring(0, 254)
                            Else
                                Obj.Commentaire = Texte_Temp
                            End If
                        End If
                        data2.Close()
                    End If
                End If
            End If

            listnew.Add(Obj)
            data.MoveNext()
            compteurhisto = compteurhisto + 1
        Loop
        data.Close()
        data = Nothing

        '----------------------------------------------------------
        '-- SNGVK Ajout de la gestion des jeton pour l'accès à MC--
        '-- Fait le 14/09/2004 suite à FA sur des erreur d'accès --
        '----------------------------------------------------------
        If listnew.Count = 0 Then
            BtnImpListe.Visible = False
        Else
            BtnImpListe.Visible = True
        End If
        'classParent.LibereJetonLecture()
        GridEvent.DataSource = listnew
        GridEvent.RefreshDataSource()
        '----------------------------------------------------------
        '-- FIN d'ajout SNGVK                                    --
        '----------------------------------------------------------
        If compteurhisto = classParent.NBMaxHisto Then
            Dim labelMSG As String
            labelMSG = Replace(classParent.ListeMessages("MESS_MC_7").Libelle, "#1#", classParent.NBMaxHisto)
            DevExpress.XtraEditors.XtraMessageBox.Show(labelMSG, Application.ProductName, MessageBoxButtons.OK)
        End If
        classParent.connect_ADO.ADODB_deconnection(1)
        classParent.connect_ADO.ADODB_deconnection(2)
    End Sub

    Private Sub GridEvent_MouseDown(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles GridEvent.MouseDown
        If e.Button = Windows.Forms.MouseButtons.Left Then
            mMouseHitPoint = New Point(e.X, e.Y)
        Else
            mMouseHitPoint = Nothing
        End If

    End Sub

    Private Sub GridEvent_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles GridEvent.DoubleClick

        Dim info As DevExpress.XtraGrid.Views.Grid.ViewInfo.GridHitInfo
        If Not IsNothing(mMouseHitPoint) Then
            info = GridView2.CalcHitInfo(mMouseHitPoint)
            If info.InRow Then
                Dim curRow As Integer = GridView2.FocusedRowHandle

                Dim Obj As grid_row
                Obj = GridView2.GetRow(curRow)
                exec(3, Obj)
            End If
        End If
    End Sub

    Private Sub GridView2_CalcPreviewText(ByVal sender As Object, ByVal e As DevExpress.XtraGrid.Views.Grid.CalcPreviewTextEventArgs) Handles GridView2.CalcPreviewText
        Dim View As GridView = sender
        Dim Str As String = GridView2.DataSource.Item(e.RowHandle).commentaire
        e.PreviewText = Str
    End Sub

    Private Function CreateJeton(ByVal row As grid_row) As Boolean
        CreateJeton = classParent.MetEnplaceJetonEXEC(row.TypeObj & "_" & row.CleObj)
    End Function

    Private Sub libereJeton(ByVal row As grid_row)
        classParent.LibereJetonEXE(row)
    End Sub

    Friend Function exec(ByVal Lemode As Integer, ByVal item As grid_row) As Boolean
        '----------------------------------------------------------
        '-- SNGVK Ajout de la gestion des jeton pour l'accès à MC--
        '-- Fait le 14/09/2004 suite à FA sur des erreur d'accès --
        '----------------------------------------------------------
        exec = True

        Dim droit_ok As Boolean

        'Si l'objet est une opération à risques
        If StrComp(item.TypeObj, "OPEDAN") = 0 Then
            'pas de création de jeton
        Else
            If Not CreateJeton(item) Then
                exec = False
                Exit Function
            End If
        End If
        '----------------------------------------------------------
        '-- FIN d'ajout SNGVK                                    --
        '----------------------------------------------------------
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
                If droit1(39) = "1" And droit2(39) = "1" Then
                    droit_ok = True
                Else
                    droit_ok = False
                End If
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
                If droit1(51) = "1" And droit2(51) = "1" Then
                    droit_ok = True
                Else
                    droit_ok = False
                End If
            Case "DIA"
                ObjM = New DIA.DemInterAero(item.EstEncours)
                If classParent.IsSSLIA Then
                    droit_ok = True
                Else
                    droit_ok = False
                End If
            Case "LVP"
                'Pas d'action
            Case "SLVP"
                ObjM = New LVP.LVP()
                If classParent.IsSSLIA Then
                    droit_ok = True
                Else
                    droit_ok = False
                End If
        End Select

        If droit_ok Then
            If UCase(item.TypeObj) = "DINTER" Then
                ObjM.Init(classParent.FichierIni, Lemode, item.CleObj, classParent.Stationnaire, , classParent.IDMachine)
            Else
                If UCase(item.TypeObj) = "OPEMAIN" Or UCase(item.TypeObj) = "DIA" Or UCase(item.TypeObj) = "DIT" Or UCase(item.TypeObj) = "SLVP" Then
                    ObjM.Init(classParent.FichierIni, Lemode, item.CleObj, classParent.Stationnaire, classParent.IDMachine)
                Else
                    ObjM.Init(classParent.FichierIni, Lemode, item.CleObj, classParent.Stationnaire)
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
        Exit Function



    End Function

    Private Sub TxtDateDeb_MouseUp(sender As Object, e As System.Windows.Forms.MouseEventArgs) Handles TxtDateDeb.MouseUp
        If e.Button = Windows.Forms.MouseButtons.Right Then
            '        ctDate.Text = Day(Now) & "/" & Month(Now) & "/" & Year(Now)
            TxtDateDeb.DateTime = Date.Now

        End If
    End Sub

    Private Sub TxtDateFin_MouseUp(sender As Object, e As System.Windows.Forms.MouseEventArgs) Handles TxtDateFin.MouseUp
        If e.Button = Windows.Forms.MouseButtons.Right Then
            '        ctDate.Text = Day(Now) & "/" & Month(Now) & "/" & Year(Now)
            TxtDateFin.DateTime = Date.Now

        End If
    End Sub

    Private Sub TxtHeureD_MouseUp(sender As Object, e As System.Windows.Forms.MouseEventArgs) Handles TxtHeureD.MouseUp
        If e.Button = Windows.Forms.MouseButtons.Right Then
            '        ctDate.Text = Day(Now) & "/" & Month(Now) & "/" & Year(Now)
            If TxtDateDeb.Text = "" Then
                TxtDateDeb.DateTime = Date.Now
            End If

            TxtHeureD.Text = TimeToTimeText(TimeOfDay)

        End If
    End Sub

    Private Sub TxtHeureF_MouseUp(sender As Object, e As System.Windows.Forms.MouseEventArgs) Handles TxtHeureF.MouseUp
        If e.Button = Windows.Forms.MouseButtons.Right Then
            '        ctDate.Text = Day(Now) & "/" & Month(Now) & "/" & Year(Now)
            If TxtDateFin.Text = "" Then
                TxtDateFin.DateTime = Date.Now
            End If

            TxtHeureF.Text = TimeToTimeText(TimeOfDay)

        End If
    End Sub

    Private Sub cboFiltre_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cboFiltre.SelectedIndexChanged
        If Not IsActivate Then
            Exit Sub
        End If

        listnew = New List(Of grid_row)

        LimiteList = True
        If cboFiltre.SelectedItem.champ <> "" Then
            LeFiltre = cboFiltre.SelectedItem.nom
        Else
            LeFiltre = ""
        End If
        LectureH(LeFiltre, Datd, DatF, IsBiffer)

        ' si il filtre permet une recherche avancée
        If cboFiltre.SelectedItem.nom = "MVTPERSO" Then
            Me.BtnRechercheA.Visible = True
        Else
            Me.BtnRechercheA.Visible = False
        End If
    End Sub

    Private Sub frmAffHisto_Activated(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Activated
        IsActivate = True
    End Sub

    Private Sub frmAffHisto_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        IsBiffer = True
        IsActivate = False

        ConditionsAdjustment()

    End Sub

    Private Sub LblCheck_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles LblCheck.CheckedChanged
        If Not IsActivate Then
            Exit Sub
        End If
        IsBiffer = (LblCheck.Checked = True)
        listnew = New List(Of grid_row)
        LimiteList = True
        LectureH(LeFiltre, Datd, DatF, IsBiffer)
    End Sub

    Private Sub BtnRechercheA_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnRechercheA.Click
        Dim fen As New frmRechercheA(_fichierIni, _iD_MACHINE, _iD_USER, Me)
        'fen.classParent = Me


        Dim DD, df As Double


        If TxtDateDeb.DateTime.ToOADate.ToString > 0 Then
            '        Datd = CDbl(CDate(TxtDateDeb.Text))
            DD = TxtDateDeb.DateTime.ToOADate
        Else
            DD = -1
        End If

        If TxtHeureD.Text <> "" Then
            '        Datd = Datd + CDbl(CDate(TxtHeureD.Text))
            DD = OLSDATE.timeToLong(Date.FromOADate(Datd), OLSDATE.Time12ToTime24(TxtHeureD.Text))
        End If


        If TxtDateFin.DateTime.ToOADate > 0 Then
            '        DatF = CDbl(CDate(TxtDateFin.Text))
            df = TxtDateFin.DateTime.ToOADate
        Else
            df = Date.Now.ToOADate
        End If

        If TxtHeureF.Text <> "" Then
            '        Datd = Datd + CDbl(CDate(TxtHeureD.Text))
            df = OLSDATE.timeToLong(Date.FromOADate(DatF), OLSDATE.Time12ToTime24(TxtHeureF.Text))
        End If


        If fen.Init(LeFiltre, DD, df, IsBiffer) Then
            fen.ShowDialog()
        End If
    End Sub

    Private Function creerdatedebut() As String
        Return OLSDATE.LongToDateText(Date.FromOADate(Datd))
    End Function

    Private Function creerdatefin() As String
        Return OLSDATE.LongToDateText(Date.FromOADate(DatF)) & " " & OLSDATE.TimeToTimeText(Date.FromOADate(DatF))
    End Function

    Private Sub frmAffHisto_Resize(sender As System.Object, e As System.EventArgs) Handles MyBase.Resize

        GrpAffHisto.Width = Me.Width - 30


        BtnFermer.Left = (Me.Width / 2) - (BtnFermer.Width / 2)
        BtnImpListe.Left = Me.Width - 185
        GrpFiltre.Left = (Me.Width / 2) - (GrpFiltre.Width / 2)
        GrpHisto.Left = (Me.Width / 2) - (GrpHisto.Width / 2)


        BtnFermer.Top = Me.Height - BtnFermer.Height - 48
        BtnImpListe.Top = Me.Height - BtnFermer.Height - 48

        GrpAffHisto.Height = Me.Height - BtnFermer.Height - GrpFiltre.Height - GrpHisto.Height - 90
        GridEvent.Dock = DockStyle.Fill

        CLN_DATE.Width = 100
        CLN_HEURE.Width = 100
        CLN_Bat.Width = 100
        CLN_TITRE.Width = GridEvent.Width - 350
        CLN_OPERATEUR.Width = 150

    End Sub

    Private Sub ConditionsAdjustment()

        Dim cn As StyleFormatCondition
        cn = New StyleFormatCondition(FormatConditionEnum.Equal, GridView2.Columns("EstSupp"), Nothing, True)
        cn.Appearance.Font = New Font("Tahoma", 9.25, System.Drawing.FontStyle.Strikeout)
        cn.ApplyToRow = True
        GridView2.FormatConditions.Add(cn)

        cn = New StyleFormatCondition(FormatConditionEnum.Equal, GridView2.Columns("EstSupp"), Nothing, False)
        cn.Appearance.Font = New Font("Tahoma", 9.25, System.Drawing.FontStyle.Regular)
        cn.ApplyToRow = True
        GridView2.FormatConditions.Add(cn)

    End Sub

End Class