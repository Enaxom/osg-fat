Imports System.Collections.Generic
Imports System.Windows.Forms

Public Class frmRechercheASar

    'Friend connect_ADO As New connect_ADODB

    Private _fichierIni As String
    Private _iD_MACHINE As Integer
    Private _iD_USER As Integer

    Friend classParent As frmAffHisto

    Public dateDebAct, dateFinAct, dateDebOpe, DateFinOpe As Double
    Public activite, voie, batiment, niveau, entreprise, ordre, tache As Integer

    Public type_evt As String

    Public sql_RAD As String
    Public car_gene As String

    'la liste dans laquelle afficher
    Public le_champ As Object

    'les dates de début et fin
    Public datedeb, datefin As Double
    Public biffees As Boolean

    'le filtre
    Public MotCle As String

    Sub New(ByVal FichierIni As String, ByVal ID_MACHINE As Integer, ByVal ID_USER As Integer, ByVal clsparent As frmAffHisto)
        InitializeComponent()
        ' TODO: Complete member initialization 
        _fichierIni = FichierIni
        _iD_MACHINE = ID_MACHINE
        _iD_USER = ID_USER

        classParent = clsparent
        If Not classParent.classParent.connect_ADO.Init(FichierIni) Then
            Me.Close()
        End If

        'Skins
        DevExpress.Skins.SkinManager.EnableFormSkins()
        DevExpress.LookAndFeel.LookAndFeelHelper.ForceDefaultLookAndFeelChanged()
        DevExpress.UserSkins.BonusSkins.Register()
        DevExpress.UserSkins.OfficeSkins.Register()

        charger_labels()
        chargement_combos()

    End Sub

    Friend Function RetCleOrZero(ByVal Obj As DevExpress.XtraEditors.ComboBoxEdit) As Long
        Dim data As Data_Set
        If IsNothing(Obj.SelectedItem) Then
            RetCleOrZero = 0
        Else
            If Obj.Text.Trim(" ") <> "" Then
                data = Obj.SelectedItem
                RetCleOrZero = data.id
            End If
        End If
    End Function

    ' REMPLISSAGE D'UN COMBOBOX
    Function charger_combobox(ByRef combobox As DevExpress.XtraEditors.ComboBoxEdit, ByVal typeBDD As Integer, ByVal table As String, ByVal WHERE As String)

        If Not classParent.classParent.connect_ADO.ADODB_connection(typeBDD) Then
            Return False
        End If

        Dim rs As ADODB.Recordset = classParent.classParent.connect_ADO.ADODB_create_recordset(typeBDD, "SELECT * FROM " & table & " WHERE Etat=True " & WHERE & " order by position,2")

        If IsNothing(rs) Then
            classParent.classParent.connect_ADO.ADODB_deconnection(typeBDD)
            Return False
        Else
            While Not rs.EOF
                combobox.Properties.Items.Add(New Data_Set(rs.Fields(1).Value, rs.Fields(0).Value))
                rs.MoveNext()
            End While

            rs.Close()
        End If

        Return True
    End Function


    Function charger_combobox_table_assoc(ByRef combobox_a_remplir As DevExpress.XtraEditors.ComboBoxEdit, ByRef combobox_cat As DevExpress.XtraEditors.ComboBoxEdit, ByVal typeBDD As Integer, ByVal table As String, ByVal table_cat As String, ByVal table_ass As String, ByVal champ_id_cat As String, ByVal champ_id As String)


        If Not classParent.classParent.connect_ADO.ADODB_connection(typeBDD) Then
            Return False
        End If

        Dim element As Data_Set = combobox_cat.SelectedItem
        Dim id_cat As Integer = element.id
        Dim sql As String
        sql = "SELECT " & table & ".* FROM " & table & ", " & table_cat & ", " & table_ass
        sql = sql & " WHERE(" & table & "." & champ_id & " = " & table_ass & "." & champ_id & " AND "
        sql = sql & table_cat & "." & champ_id_cat & "=" & table_ass & "." & champ_id_cat & " AND "
        sql = sql & table_cat & "." & champ_id_cat & "=" & id_cat & " AND " & table & ".Etat=True) "
        sql = sql & "ORDER BY " & table & ".position, 2"

        Dim rs As ADODB.Recordset = classParent.classParent.connect_ADO.ADODB_create_recordset(typeBDD, sql)

        If IsNothing(rs) Then
            classParent.classParent.connect_ADO.ADODB_deconnection(typeBDD)
            Return False
        Else
            While Not rs.EOF
                If table = "UTILISATEURS" Then
                    combobox_a_remplir.Properties.Items.Add(New Data_Set(rs.Fields(1).Value & " " & rs.Fields(2).Value, rs.Fields(0).Value))
                Else
                    combobox_a_remplir.Properties.Items.Add(New Data_Set(rs.Fields(1).Value, rs.Fields(0).Value))
                End If
                rs.MoveNext()
            End While

            rs.Close()
        End If

        Return True
    End Function


    Private Sub charger_labels()
        Dim rs As ADODB.Recordset
        If classParent.classParent.connect_ADO.ADODB_connection(1) Then

            rs = classParent.classParent.connect_ADO.ADODB_create_recordset(1, "SELECT * FROM LANGUE WHERE IdObjet='MC' AND IdLangue='TitreFenetreRechA'")
            Me.Text = rs.Fields("Libelle").Value
            Me.GrpRAMVTPERSO.Text = rs.Fields("Libelle").Value
            rs.Close()

            'rs = classParent.classParent.connect_ADO.ADODB_create_recordset(1, "SELECT * FROM LANGUE WHERE IdObjet='MC' AND IdLangue='GRP_CRITERE'")
            'Me.GrpRAMVTPERSO.Text = rs.Fields("Libelle").Value
            'rs.Close()

            rs = classParent.classParent.connect_ADO.ADODB_create_recordset(1, "SELECT * FROM LANGUE WHERE IdObjet='SAR' AND IdLangue='lblAutorisation'")
            Me.lblSAR.Text = rs.Fields("Libelle").Value
            rs.Close()

            rs = classParent.classParent.connect_ADO.ADODB_create_recordset(1, "SELECT * FROM LANGUE WHERE IdObjet='SAR' AND IdLangue='NomChantier'")
            Me.lblChantier.Text = rs.Fields("Libelle").Value
            rs.Close()

            rs = classParent.classParent.connect_ADO.ADODB_create_recordset(1, "SELECT * FROM LANGUE WHERE IdObjet='MC' AND IdLangue='LBL_DATE_DEB'")
            Me.lblDateDebAutoSAR.Text = rs.Fields("Libelle").Value
            rs.Close()

            rs = classParent.classParent.connect_ADO.ADODB_create_recordset(1, "SELECT * FROM LANGUE WHERE IdObjet='MC' AND IdLangue='LBL_DATE_FIN'")
            Me.lblDateFinAutoSAR.Text = rs.Fields("Libelle").Value
            rs.Close()

            rs = classParent.classParent.connect_ADO.ADODB_create_recordset(1, "SELECT * FROM LANGUE WHERE IdObjet='MC' AND IdLangue='LBL_DATE_DEB'")
            Me.lblDateDebChantierSAR.Text = rs.Fields("Libelle").Value
            rs.Close()

            rs = classParent.classParent.connect_ADO.ADODB_create_recordset(1, "SELECT * FROM LANGUE WHERE IdObjet='MC' AND IdLangue='LBL_DATE_FIN'")
            Me.lblDateFinChantierSAR.Text = rs.Fields("Libelle").Value
            rs.Close()

            rs = classParent.classParent.connect_ADO.ADODB_create_recordset(1, "SELECT * FROM LANGUE WHERE IdObjet='SAR' AND IdLangue='lblType'")
            Me.lblTypeSar.Text = rs.Fields("Libelle").Value
            rs.Close()

            rs = classParent.classParent.connect_ADO.ADODB_create_recordset(1, "SELECT * FROM LANGUE WHERE IdObjet='ADM_VOIES' AND IdLangue='LBL_1'")
            Me.lblVoieSar.Text = rs.Fields("Libelle").Value
            rs.Close()

            rs = classParent.classParent.connect_ADO.ADODB_create_recordset(1, "SELECT * FROM LANGUE WHERE IdObjet='ADM_VOIES' AND IdLangue='LIST_1'")
            Me.lblBatSar.Text = rs.Fields("Libelle").Value
            rs.Close()

            rs = classParent.classParent.connect_ADO.ADODB_create_recordset(1, "SELECT * FROM LANGUE WHERE IdObjet='ADM_NIVEAUX' AND IdLangue='LBL_1'")
            Me.lblEtageSar.Text = rs.Fields("Libelle").Value
            rs.Close()

            rs = classParent.classParent.connect_ADO.ADODB_create_recordset(1, "SELECT * FROM LANGUE WHERE IdObjet='SAR' AND IdLangue='lblEntNom'")
            Me.lblSocSar.Text = rs.Fields("Libelle").Value
            rs.Close()

            rs = classParent.classParent.connect_ADO.ADODB_create_recordset(1, "SELECT * FROM LANGUE WHERE IdObjet='SAR' AND IdLangue='fraOrdre'")
            Me.lblOrdreSar.Text = rs.Fields("Libelle").Value & " :"
            rs.Close()


            rs = classParent.classParent.connect_ADO.ADODB_create_recordset(1, "SELECT * FROM LANGUE WHERE IdObjet='SAR' AND IdLangue='lblTache'")
            Me.lblTravauxSar.Text = rs.Fields("Libelle").Value & " :"
            rs.Close()

            rs = classParent.classParent.connect_ADO.ADODB_create_recordset(1, "SELECT * FROM LANGUE WHERE IdObjet='MC' AND IdLangue='BTN_RECHERCHEA'")
            Me.BtnRASarRech.Text = rs.Fields("Libelle").Value
            rs.Close()

            rs = classParent.classParent.connect_ADO.ADODB_create_recordset(1, "SELECT * FROM LANGUE WHERE IdObjet='GENERIQUE' AND IdLangue='BTN_ANNULER'")
            Me.BtnRASarAnnuler.Text = rs.Fields("Libelle").Value
            rs.Close()

            classParent.classParent.connect_ADO.ADODB_deconnection(1)
        End If

    End Sub


    Public Sub chargement_combos()

        charger_combobox(cboTypeActiviteSar, 1, "OPE_TYPES", "")
        charger_combobox(cboVoieSar, 1, "VOIES", "")
        charger_combobox(cboEntrepriseSar, 1, "OPE_SOCIETES", "")
        charger_combobox(cboOrdreSar, 1, "OPE_ORDRES", "")
        charger_combobox(cboTacheSar, 1, "OPE_TACHES", " AND IDCategorieTache = 1 ")

    End Sub

    Function Init(ByVal Tp_Evt As String, ByVal DD As Double, ByVal df As Double, ByVal biffe As Boolean) As Boolean
        Init = True

        Try

            activite = 0
            voie = 0
            batiment = 0
            niveau = 0
            entreprise = 0
            ordre = 0
            tache = 0
            car_gene = "%"
            sql_RAD = ""
            datedeb = CDbl(DD)
            datefin = CDbl(df)
            biffees = biffe

            'initialisation des variables
            type_evt = Tp_Evt

            Exit Function

        Catch ex As Exception
            DevExpress.XtraEditors.XtraMessageBox.Show(ex.Message, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Init = False
        End Try

    End Function


    ' CHANGEMENT DU SELECT VOIE => REMPLISSAGE BATIMENT
    Private Sub ComboBoxVoie_SelectedIndexChanged(sender As System.Object, e As System.EventArgs) Handles cboVoieSar.SelectedIndexChanged

        cboBatimentSar.Properties.Items.Clear()
        cboBatimentSar.Text = ""
        cboNiveauSar.Properties.Items.Clear()
        cboNiveauSar.Text = ""

        If Not IsNothing(cboVoieSar.SelectedItem) Then
            charger_combobox_table_assoc(cboBatimentSar, cboVoieSar, 1, "BATIMENTS", "VOIES", "A_VOIE_BATIMENT", "IDVoie", "IDBatiment")

        End If

    End Sub

    ' CHANGEMENT DU SELECT BATIMENT => REMPLISSAGE NIVEAUX
    Private Sub ComboBoxBatiment_SelectedIndexChanged(sender As System.Object, e As System.EventArgs) Handles cboBatimentSar.SelectedIndexChanged

        cboNiveauSar.Properties.Items.Clear()
        cboNiveauSar.Text = ""

        If Not IsNothing(cboBatimentSar.SelectedItem) Then
            charger_combobox_table_assoc(cboNiveauSar, cboBatimentSar, 1, "NIVEAUX", "BATIMENTS", "BATIMENT_NIVEAU", "IDBatiment", "IDNiveau")

        End If

    End Sub


    Private Sub BtnRARech_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnRASarRech.Click
        Dim SqlWhere, sqlwhere2 As String
        SqlWhere = ""
        sqlwhere2 = ""


        If Me.type_evt = type_evt Then

            dateDebAct = dateDebAutoSAR.DateTime.ToOADate
            dateFinAct = dateFinAutoSAR.DateTime.ToOADate
            dateDebOpe = dateDebChantierSAR.DateTime.ToOADate
            DateFinOpe = dateFinChantierSAR.DateTime.ToOADate

            activite = RetCleOrZero(Me.cboTypeActiviteSar)
            voie = RetCleOrZero(Me.cboVoieSar)
            batiment = RetCleOrZero(Me.cboBatimentSar)
            niveau = RetCleOrZero(Me.cboNiveauSar)
            entreprise = RetCleOrZero(Me.cboEntrepriseSar)
            ordre = RetCleOrZero(Me.cboOrdreSar)
            tache = RetCleOrZero(Me.cboTacheSar)

            ' je crée la partie de la requête que je rajouterais à select * from MC
            sql_RAD = " and cleobj in (select OPE_AUTORISATIONS.IdAutorisation from OPE_AUTORISATIONS "

            If StrComp(dateDebOpe, 0) <> 0 Or StrComp(DateFinOpe, 0) <> 0 Or StrComp(tache, 0) <> 0 Then
                sql_RAD = sql_RAD & " INNER JOIN OPE_OPERATIONS ON OPE_OPERATIONS.idAutorisation = OPE_AUTORISATIONS.idAutorisation "
            End If

            If StrComp(tache, 0) <> 0 Then
                sql_RAD = sql_RAD & " INNER JOIN OPE_A_OPE_TACHES ON OPE_A_OPE_TACHES.Idoperation = OPE_OPERATIONS.Idoperation "
            End If


            If StrComp(dateDebAct, 0) <> 0 Then
                SqlWhere = " OPE_AUTORISATIONS.Datedebut > " & dateDebAct & " "
            End If

            If StrComp(dateFinAct, 0) <> 0 Then
                If StrComp(SqlWhere, "") <> 0 Then
                    SqlWhere = SqlWhere & " and "
                End If
                SqlWhere = " OPE_AUTORISATIONS.dateFin < " & dateFinAct & " "
            End If


            If StrComp(dateDebOpe, 0) <> 0 Then
                If StrComp(SqlWhere, "") <> 0 Then
                    SqlWhere = SqlWhere & " and "
                End If
                SqlWhere = " OPE_OPERATIONS.DateDeb > " & dateDebOpe & " "
            End If

            If StrComp(DateFinOpe, 0) <> 0 Then
                If StrComp(SqlWhere, "") <> 0 Then
                    SqlWhere = SqlWhere & " and "
                End If
                SqlWhere = " OPE_OPERATIONS.DateFin < " & DateFinOpe & " "
            End If
            If StrComp(activite, 0) <> 0 Then
                If StrComp(SqlWhere, "") <> 0 Then
                    SqlWhere = SqlWhere & " and "
                End If
                SqlWhere = " OPE_AUTORISATIONS.IdType = " & activite & " "
            End If

            If StrComp(voie, 0) <> 0 Then
                If StrComp(SqlWhere, "") <> 0 Then
                    SqlWhere = SqlWhere & " and "
                End If
                SqlWhere = SqlWhere & " OPE_AUTORISATIONS.IdVoie = " & voie & " "
            End If

            If StrComp(batiment, 0) <> "0" Then
                If StrComp(SqlWhere, "") <> 0 Then
                    SqlWhere = SqlWhere & " and "
                End If
                SqlWhere = SqlWhere & " OPE_AUTORISATIONS.IdBat = " & batiment & " "
            End If

            If StrComp(niveau, 0) <> 0 Then
                If StrComp(SqlWhere, "") <> 0 Then
                    SqlWhere = SqlWhere & " and "
                End If
                SqlWhere = SqlWhere & " OPE_AUTORISATIONS.IdEtage = " & niveau & " "
            End If

            If StrComp(entreprise, 0) <> 0 Then
                If StrComp(SqlWhere, "") <> 0 Then
                    SqlWhere = SqlWhere & " and "
                End If
                SqlWhere = SqlWhere & " OPE_AUTORISATIONS.IdSociete = " & entreprise & " "
            End If

            If StrComp(ordre, 0) <> 0 Then
                If StrComp(SqlWhere, "") <> 0 Then
                    SqlWhere = SqlWhere & " and "
                End If
                SqlWhere = SqlWhere & " OPE_AUTORISATIONS.idordre = " & ordre & " "
            End If

            If StrComp(tache, 0) <> 0 Then
                If StrComp(SqlWhere, "") <> 0 Then
                    SqlWhere = SqlWhere & " and "
                End If
                SqlWhere = SqlWhere & " OPE_A_OPE_TACHES.Idtache = " & tache & " "
            End If


            If StrComp(SqlWhere, "") <> 0 Then
                SqlWhere = " where " & SqlWhere
            End If

            SqlWhere = SqlWhere & ")"


            sql_RAD = sql_RAD & SqlWhere

        End If

        'appel de la fonction
        classParent.listnew = New List(Of grid_row)
        classParent.LimiteList = True
        classParent.LectureH(type_evt, CDbl(datedeb), datefin, biffees, sql_RAD)

        Me.Close()
    End Sub

    Private Sub BtnRASarAnnuler_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnRASarAnnuler.Click
        Me.Close()
    End Sub

End Class