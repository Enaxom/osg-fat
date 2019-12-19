Imports System.Collections.Generic
Imports System.Windows.Forms

Public Class frmRechercheADiv

    'Friend connect_ADO As New connect_ADODB

    Private _fichierIni As String
    Private _iD_MACHINE As Integer
    Private _iD_USER As Integer

    Friend classParent As frmAffHisto

    Public objet, texte As String
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

    End Sub

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

            rs = classParent.classParent.connect_ADO.ADODB_create_recordset(1, "SELECT * FROM LANGUE WHERE IdObjet='ADM_ListObjetDivers' AND IdLangue='LBL_1'")
            Me.lblRADObjet.Text = rs.Fields("Libelle").Value
            rs.Close()

            rs = classParent.classParent.connect_ADO.ADODB_create_recordset(1, "SELECT * FROM LANGUE WHERE IdObjet='ADM_ListObjetDivers' AND IdLangue='LBL_COM'")
            Me.LblRADTxt.Text = rs.Fields("Libelle").Value
            rs.Close()

            rs = classParent.classParent.connect_ADO.ADODB_create_recordset(1, "SELECT * FROM LANGUE WHERE IdObjet='MC' AND IdLangue='BTN_RECHERCHEA'")
            Me.BtnRADRech.Text = rs.Fields("Libelle").Value
            rs.Close()

            rs = classParent.classParent.connect_ADO.ADODB_create_recordset(1, "SELECT * FROM LANGUE WHERE IdObjet='GENERIQUE' AND IdLangue='BTN_ANNULER'")
            Me.BtnRADAnnuler.Text = rs.Fields("Libelle").Value
            rs.Close()

            classParent.classParent.connect_ADO.ADODB_deconnection(1)
        End If

    End Sub

    Function Init(ByVal Tp_Evt As String, ByVal DD As Double, ByVal df As Double, ByVal biffe As Boolean) As Boolean
        Init = True

        Try
            objet = ""
            texte = ""
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

    Private Sub BtnRARech_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnRADRech.Click
        Dim SqlWhere, sqlwhere2 As String
        SqlWhere = ""
        sqlwhere2 = ""


        If Me.type_evt = type_evt Then

            'récupération des champs
            objet = Trim(Replace(Me.txtRADObjet.Text, "'", "''"))
            texte = Trim(Replace(Me.TxtRADTexte.Text, "'", "''"))
 
            ' je crée la partie de la requête que je rajouterais à select * from MC
            sql_RAD = " and cleobj in (select Divers.IDdivers from Divers INNER JOIN DATADIVERS ON DATADIVERS.CLEHISTO = Divers.IDdivers"

            If StrComp(objet, "") <> 0 Then
                SqlWhere = " IntitulerObjet like '" & Me.car_gene & objet & car_gene & "' "
            End If


            If StrComp(texte, "") <> 0 Then
                If StrComp(SqlWhere, "") <> 0 Then
                    SqlWhere = SqlWhere & " and "
                End If
                SqlWhere = SqlWhere & " TexteHistorique like '" & car_gene & texte & car_gene & "'"
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

    Private Sub BtnRAAnnuler_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnRADAnnuler.Click
        Me.Close()
    End Sub

End Class