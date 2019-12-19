Imports System.Collections.Generic
Imports System.Windows.Forms

Public Class frmRechercheA

    'Friend connect_ADO As New connect_ADODB

    Private _fichierIni As String
    Private _iD_MACHINE As Integer
    Private _iD_USER As Integer

    Friend classParent As frmAffHisto

    Public contact, nom, prenom, societe, badge, divers, motif1, motif2, motif3, motif4 As String

    Public type_evt As String
    Public car_gene As String

    Public sql_RA As String

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

        TxtRABadge.ContextMenuStrip = New Windows.Forms.ContextMenuStrip
        txtRAContact.ContextMenuStrip = New Windows.Forms.ContextMenuStrip
        TxtRADivers.ContextMenuStrip = New Windows.Forms.ContextMenuStrip
        TxtRAMotif1.ContextMenuStrip = New Windows.Forms.ContextMenuStrip
        TxtRAMotif2.ContextMenuStrip = New Windows.Forms.ContextMenuStrip
        TxtRAMotif3.ContextMenuStrip = New Windows.Forms.ContextMenuStrip
        TxtRAMotif4.ContextMenuStrip = New Windows.Forms.ContextMenuStrip
        TxtRANom.ContextMenuStrip = New Windows.Forms.ContextMenuStrip
        TxtRAPrenom.ContextMenuStrip = New Windows.Forms.ContextMenuStrip
        TxtRASociete.ContextMenuStrip = New Windows.Forms.ContextMenuStrip


        charger_labels()

    End Sub

    Private Sub charger_labels()
        Dim rs As ADODB.Recordset
        If classParent.classParent.connect_ADO.ADODB_connection(1) Then

            rs = classParent.classParent.connect_ADO.ADODB_create_recordset(1, "SELECT * FROM LANGUE WHERE IdObjet='MC' AND IdLangue='TitreFenetreRechA'")
            Me.Text = rs.Fields("Libelle").Value
            rs.Close()

            rs = classParent.classParent.connect_ADO.ADODB_create_recordset(1, "SELECT * FROM LANGUE WHERE IdObjet='MC' AND IdLangue='GRP_CRITERE'")
            Me.GrpRAMVTPERSO.Text = rs.Fields("Libelle").Value
            rs.Close()

            rs = classParent.classParent.connect_ADO.ADODB_create_recordset(1, "SELECT * FROM LANGUE WHERE IdObjet='MC' AND IdLangue='LBL_CONTACT'")
            Me.LblRAContact.Text = rs.Fields("Libelle").Value & " :"
            rs.Close()

            rs = classParent.classParent.connect_ADO.ADODB_create_recordset(1, "SELECT * FROM LANGUE WHERE IdObjet='MC' AND IdLangue='LBL_NOM'")
            Me.LblRANom.Text = rs.Fields("Libelle").Value & " :"
            rs.Close()

            rs = classParent.classParent.connect_ADO.ADODB_create_recordset(1, "SELECT * FROM LANGUE WHERE IdObjet='MC' AND IdLangue='LBL_PRENOM'")
            Me.LblRAPrenom.Text = rs.Fields("Libelle").Value & " :"
            rs.Close()

            rs = classParent.classParent.connect_ADO.ADODB_create_recordset(1, "SELECT * FROM LANGUE WHERE IdObjet='MC' AND IdLangue='LBL_SOCIETE'")
            Me.LblRASociete.Text = rs.Fields("Libelle").Value & " :"
            rs.Close()

            rs = classParent.classParent.connect_ADO.ADODB_create_recordset(1, "SELECT * FROM LANGUE WHERE IdObjet='MC' AND IdLangue='LBL_BADGE'")
            Me.LblRABadge.Text = rs.Fields("Libelle").Value & " :"
            rs.Close()

            rs = classParent.classParent.connect_ADO.ADODB_create_recordset(1, "SELECT * FROM LANGUE WHERE IdObjet='MC' AND IdLangue='LBL_DIVERS'")
            Me.LblRADivers.Text = rs.Fields("Libelle").Value & " :"
            rs.Close()

            rs = classParent.classParent.connect_ADO.ADODB_create_recordset(1, "SELECT * FROM LANGUE WHERE IdObjet='MC' AND IdLangue='LBL_MOTIF'")
            Me.LblRAMotif.Text = rs.Fields("Libelle").Value & " :"
            rs.Close()

            rs = classParent.classParent.connect_ADO.ADODB_create_recordset(1, "SELECT * FROM LANGUE WHERE IdObjet='MC' AND IdLangue='LBL_RECH_PERS'")
            Me.LblRARechPers.Text = rs.Fields("Libelle").Value & " :"
            rs.Close()

            rs = classParent.classParent.connect_ADO.ADODB_create_recordset(1, "SELECT * FROM LANGUE WHERE IdObjet='MC' AND IdLangue='BTN_RECHERCHEA'")
            Me.BtnRARech.Text = rs.Fields("Libelle").Value
            rs.Close()

            rs = classParent.classParent.connect_ADO.ADODB_create_recordset(1, "SELECT * FROM LANGUE WHERE IdObjet='GENERIQUE' AND IdLangue='BTN_ANNULER'")
            Me.BtnRAAnnuler.Text = rs.Fields("Libelle").Value
            rs.Close()

            classParent.classParent.connect_ADO.ADODB_deconnection(1)
        End If

    End Sub

    Function Init(ByVal Tp_Evt As String, ByVal DD As Double, ByVal df As Double, ByVal biffe As Boolean) As Boolean
        Init = True

        Try

            datedeb = CDbl(DD)
            datefin = CDbl(df)
            biffees = biffe

            'initialisation des variables
            type_evt = Tp_Evt
            contact = ""
            nom = ""
            prenom = ""
            societe = ""
            badge = ""
            divers = ""
            motif1 = ""
            motif2 = ""
            motif3 = ""
            motif4 = ""
            car_gene = "%"
            sql_RA = ""


            Exit Function

        Catch ex As Exception
            DevExpress.XtraEditors.XtraMessageBox.Show(ex.Message, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Init = False
        End Try

    End Function

    Private Sub BtnRARech_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnRARech.Click
        Dim SqlWhere, sqlwhere2 As String
        SqlWhere = ""
        sqlwhere2 = ""

        If Me.type_evt = type_evt Then

            'récupération des champs
            contact = Trim(Replace(Me.txtRAContact.Text, "'", "''"))
            nom = Trim(Replace(Me.TxtRANom.Text, "'", "''"))
            prenom = Trim(Replace(Me.TxtRAPrenom.Text, "'", "''"))
            societe = Trim(Replace(Me.TxtRASociete.Text, "'", "''"))
            badge = Trim(Replace(Me.TxtRABadge.Text, "'", "''"))
            divers = Trim(Replace(Me.TxtRADivers.Text, "'", "''"))
            motif1 = Trim(Replace(Me.TxtRAMotif1.Text, "'", "''"))
            motif2 = Trim(Replace(Me.TxtRAMotif2.Text, "'", "''"))
            motif3 = Trim(Replace(Me.TxtRAMotif3.Text, "'", "''"))
            motif4 = Trim(Replace(Me.TxtRAMotif4.Text, "'", "''"))

            ' je crée la partie de la requête que je rajouterais à select * from MC
            sql_RA = "and Objet='" & type_evt & "' and cleobj in (select IDMvtPerso from MVT_PERSONNEL "

            If StrComp(contact, "") <> 0 Then
                SqlWhere = " Contact like '" & Me.car_gene & contact & car_gene & "' "
            End If

            If StrComp(motif1, "") <> 0 Then
                If StrComp(SqlWhere, "") <> 0 Then
                    SqlWhere = SqlWhere & " and "
                End If
                SqlWhere = SqlWhere & " Motif like '" & car_gene & motif1 & car_gene & "'"
            End If

            If StrComp(motif2, "") <> 0 Then
                If StrComp(SqlWhere, "") <> 0 Then
                    SqlWhere = SqlWhere & " and "
                End If
                SqlWhere = SqlWhere & " Motif like '" & car_gene & motif2 & car_gene & "'"
            End If

            If StrComp(motif3, "") <> 0 Then
                If StrComp(SqlWhere, "") <> 0 Then
                    SqlWhere = SqlWhere & " and "
                End If
                SqlWhere = SqlWhere & " Motif like '" & car_gene & motif3 & car_gene & "'"
            End If

            If StrComp(motif4, "") <> 0 Then
                If StrComp(SqlWhere, "") <> 0 Then
                    SqlWhere = SqlWhere & " and "
                End If
                SqlWhere = SqlWhere & " Motif like '" & car_gene & motif4 & car_gene & "'"
            End If


            'pour la partie sur les personnels
            If StrComp(SqlWhere, "") <> 0 Then
                SqlWhere = SqlWhere & " and "
            End If

            SqlWhere = SqlWhere & " IDMvtPerso in (select IDMvtPerso from PERSONNEL "

            If StrComp(nom, "") <> 0 Then
                sqlwhere2 = " Nom like '" & car_gene & nom & car_gene & "' "
            End If

            If StrComp(prenom, "") <> 0 Then
                If StrComp(sqlwhere2, "") <> 0 Then
                    sqlwhere2 = sqlwhere2 & " and "
                End If
                sqlwhere2 = sqlwhere2 & " Prenom like '" & car_gene & prenom & car_gene & "' "
            End If

            If StrComp(societe, "") <> 0 Then
                If StrComp(sqlwhere2, "") <> 0 Then
                    sqlwhere2 = sqlwhere2 & " and "
                End If
                sqlwhere2 = sqlwhere2 & " Societe like '" & car_gene & societe & car_gene & "' "
            End If

            If StrComp(badge, "") <> 0 Then
                If StrComp(sqlwhere2, "") <> 0 Then
                    sqlwhere2 = sqlwhere2 & " and "
                End If
                sqlwhere2 = sqlwhere2 & " Num like '" & car_gene & badge & car_gene & "' "
            End If

            If StrComp(divers, "") <> 0 Then
                If StrComp(sqlwhere2, "") <> 0 Then
                    sqlwhere2 = sqlwhere2 & " and "
                End If
                sqlwhere2 = sqlwhere2 & " Divers like '" & car_gene & divers & car_gene & "' "
            End If

            If StrComp(sqlwhere2, "") <> 0 Then
                sqlwhere2 = " where " & sqlwhere2
            End If

            If StrComp(SqlWhere, "") <> 0 Then
                SqlWhere = " where " & SqlWhere
            End If

            SqlWhere = SqlWhere & sqlwhere2

            SqlWhere = SqlWhere & "))"

            sql_RA = sql_RA & SqlWhere

        End If


        'appel de la fonction
        classParent.listnew = New List(Of grid_row)
        classParent.LimiteList = True
        classParent.LectureH(type_evt, CDbl(datedeb), datefin, biffees, sql_RA)

        Me.Close()
    End Sub

    Private Sub BtnRAAnnuler_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnRAAnnuler.Click
        Me.Close()
    End Sub
End Class