Imports System.Windows.Forms

Public Class frmLogin
    'Friend connect_ADO As New connect_ADODB

    Private _fichierIni As String
    Private _iD_MACHINE As Integer
    Private _iD_USER As Integer
    Private fermeture_ok As Boolean
    Public ClassParent As Init_MC

    Sub New(ByVal FichierIni As String, ByVal ID_MACHINE As Integer, ByVal ID_USER As Integer, ByVal clParent As Init_MC)
        InitializeComponent()
        ' TODO: Complete member initialization 
        _fichierIni = FichierIni
        _iD_MACHINE = ID_MACHINE
        _iD_USER = ID_USER
        ClassParent = clParent


        If Not ClassParent.connect_ADO.Init(FichierIni) Then
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
        If ClassParent.connect_ADO.ADODB_connection(1) Then

            rs = ClassParent.connect_ADO.ADODB_create_recordset(1, "SELECT * FROM LANGUE WHERE IdObjet='MC' AND IdLangue='TitreFenetreIdent'")
            Me.Text = rs.Fields("Libelle").Value
            rs.Close()

            rs = ClassParent.connect_ADO.ADODB_create_recordset(1, "SELECT * FROM LANGUE WHERE IdObjet='MC' AND IdLangue='LBL_EQUIPE'")
            Me.LblGroupe.Text = rs.Fields("Libelle").Value & " :"
            rs.Close()

            rs = ClassParent.connect_ADO.ADODB_create_recordset(1, "SELECT * FROM LANGUE WHERE IdObjet='MC' AND IdLangue='LBL_NOM_OP'")
            Me.LblNomUser.Text = rs.Fields("Libelle").Value & " :"
            rs.Close()

            rs = ClassParent.connect_ADO.ADODB_create_recordset(1, "SELECT * FROM LANGUE WHERE IdObjet='MC' AND IdLangue='LBL_PWD'")
            Me.lblPWD.Text = rs.Fields("Libelle").Value & " :"
            rs.Close()

            rs = ClassParent.connect_ADO.ADODB_create_recordset(1, "SELECT * FROM LANGUE WHERE IdObjet='MC' AND IdLangue='BTN_CONNEXION'")
            Me.BtnLogin.Text = rs.Fields("Libelle").Value
            rs.Close()

            rs = ClassParent.connect_ADO.ADODB_create_recordset(1, "SELECT * FROM LANGUE WHERE IdObjet='MC' AND IdLangue='BTN_QUITTER'")
            Me.BtnQuitter.Text = rs.Fields("Libelle").Value
            rs.Close()

            ClassParent.connect_ADO.ADODB_deconnection(1)
        End If

    End Sub



    Public Sub Init()
        Dim enr As ADODB.Recordset
        Dim IndexI As Integer
        Dim SQL As String

        fermeture_ok = False

        ClassParent.Stationnaire = 0

        If Not ClassParent.connect_ADO.ADODB_connection(1) Then
            DevExpress.XtraEditors.XtraMessageBox.Show(ClassParent.ListeMessages("MESS_MC_1").Libelle, Application.ProductName, MessageBoxButtons.OK)
            Exit Sub
        End If

        If ClassParent.isBioVein Then
            Me.lblPWD.Visible = False
            Me.txtPWD.Visible = False
        Else
            Me.lblPWD.Visible = ClassParent.isPWD
            Me.txtPWD.Visible = ClassParent.isPWD
        End If
        '*************************
        SQL = "SELECT NomGroupe,IDGroupe FROM Groupes WHERE ETat=true  ORDER BY Position, NomGroupe"
        enr = ClassParent.connect_ADO.ADODB_create_recordset(1, SQL)
        cboUSer.Properties.Items.Clear()
        While Not enr.EOF
            IndexI = cboGroupe.Properties.Items.Add(New Data_Set(enr.Fields("NomGroupe").Value, enr.Fields("IDGroupe").Value))
            enr.MoveNext()
        End While

        enr.Close()
        enr = Nothing
        ClassParent.connect_ADO.ADODB_deconnection(1)

    End Sub


    Private Function PWD_Traiter() As Boolean
        Dim data As ADODB.Recordset
        Dim SQL As String

        PWD_Traiter = False
        If Not ClassParent.connect_ADO.ADODB_connection(1) Then
            DevExpress.XtraEditors.XtraMessageBox.Show(ClassParent.ListeMessages("MESS_MC_1").Libelle, Application.ProductName, MessageBoxButtons.OK)
            Return False
        End If
        SQL = "SELECT PASSWORD FROM UTILISATEURS WHERE IDUtilisateur=" & ClassParent.Stationnaire
        data = ClassParent.connect_ADO.ADODB_create_recordset(1, SQL)
        If Not data.EOF Then
            If txtPWD.Text = (data.Fields(0).Value) & "" Then
                PWD_Traiter = True
            Else
                PWD_Traiter = False
                DevExpress.XtraEditors.XtraMessageBox.Show(ClassParent.ListeMessages("MESS_ERRPWD").Libelle, Application.ProductName, MessageBoxButtons.OK)
            End If
        End If
        data.Close()
        data = Nothing
        ClassParent.connect_ADO.ADODB_deconnection(1)

        cboUSer.Enabled = True
    End Function

    Private Function Biovein_Traiter() As Boolean
        Dim data As ADODB.Recordset

        On Error GoTo Biovein_Traiter_Erreur

        Biovein_Traiter = False
        If ClassParent.connect_ADO.ADODB_connection(1) Then
            data = ClassParent.connect_ADO.ADODB_create_recordset(1, "SELECT * FROM UTILISATEURS WHERE IDUTILISATEUR = " & ClassParent.Stationnaire)
            If Not data.EOF Then
                Dim DLLBioVein As New BioVein.clsBioVein
                Dim retour As Boolean
                Dim Empreinte As String
                Dim Longueur_empreinte As Integer
                Empreinte = data.Fields("BioVein").Value & ""
                Longueur_empreinte = data.Fields("BioVeinLong").Value
                If Longueur_empreinte = 0 Then
                    DevExpress.XtraEditors.XtraMessageBox.Show(ClassParent.ListeMessages("MESS_BIOVEINNOK").Libelle, Application.ProductName, MessageBoxButtons.OK)
                    Me.lblPWD.Visible = True
                    Me.txtPWD.Visible = True
                    Return False
                End If
                'DLLBioVein = CreateObject("BioVein.clsBioVein")
                retour = DLLBioVein.Valider_Vein(Empreinte, Longueur_empreinte)
                DLLBioVein = Nothing
                If retour Then
                    Biovein_Traiter = True
                Else
                    DevExpress.XtraEditors.XtraMessageBox.Show(ClassParent.ListeMessages("MESS_BIOVEINNOK").Libelle, Application.ProductName, MessageBoxButtons.OK)
                    Biovein_Traiter = False
                End If
                data.Close()
            End If
            data = Nothing
            ClassParent.connect_ADO.ADODB_deconnection(1)
        End If
        Exit Function
Biovein_Traiter_Erreur:
        DevExpress.XtraEditors.XtraMessageBox.Show(ClassParent.ListeMessages("MESS_BIOVEINNOK").Libelle, Application.ProductName, MessageBoxButtons.OK)
        Return False
    End Function

    Private Sub BtnQuitter_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnQuitter.Click
        fermeture_ok = True
        ClassParent.Stationnaire = 0
        Me.Close()
    End Sub

    Private Sub cboGroupe_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cboGroupe.SelectedIndexChanged
        Dim data As ADODB.Recordset

        Dim SQL As String

        If Not ClassParent.connect_ADO.ADODB_connection(1) Then
            DevExpress.XtraEditors.XtraMessageBox.Show(ClassParent.ListeMessages("MESS_MC_1").Libelle, Application.ProductName, MessageBoxButtons.OK)
            Exit Sub
        End If
        SQL = "SELECT * FROM UTILISATEURS INNER JOIN A_GROUPES_UTILISATEURS ON UTILISATEURS.IDUtilisateur = A_GROUPES_UTILISATEURS.IDUtilisateur WHERE (((UTILISATEURS.Etat)=True) AND ((A_GROUPES_UTILISATEURS.IDGroupe)=" & cboGroupe.SelectedItem.id & ")) ORDER BY  UTILISATEURS.Position, UTILISATEURS.NomUtilisateur, UTILISATEURS.PrenomUtilisateur"
        data = ClassParent.connect_ADO.ADODB_create_recordset(1, SQL)
        cboUSer.Properties.Items.Clear()
        cboUSer.Text = ""
        While Not data.EOF
            cboUSer.Properties.Items.Add(New Data_Set(UCase(data.Fields("NomUtilisateur").Value) & " " & data.Fields("PrenomUtilisateur").Value, data.Fields("IDUtilisateur").Value))
            data.MoveNext()
        End While
        data.Close()
        data = Nothing
        ClassParent.connect_ADO.ADODB_deconnection(1)

        cboUSer.Enabled = True
    End Sub

    Private Sub BtnLogin_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnLogin.Click
        If cboUSer.SelectedIndex >= 0 Then
            ClassParent.Stationnaire = cboUSer.SelectedItem.id
            ClassParent.Groupe = cboGroupe.SelectedItem.id

            If ClassParent.isBioVein And ClassParent.isPWD And lblPWD.Visible = False Then
                If Biovein_Traiter() Then
                    ClassParent.connect_ADO.ADODB_connection(1)
                    Dim rs As ADODB.Recordset
                    rs = ClassParent.connect_ADO.ADODB_create_recordset(1, "SELECT * FROM UTILISATEURS WHERE IDUtilisateur=" & cboUSer.SelectedItem.id)
                    rs.Fields("Disponibilite").Value = 5
                    rs.Update()
                    rs.Close()
                    fermeture_ok = True
                    Me.Close()
                End If
            Else
                If lblPWD.Visible Then
                    If PWD_Traiter() Then
                        ClassParent.connect_ADO.ADODB_connection(1)
                        Dim rs As ADODB.Recordset
                        rs = ClassParent.connect_ADO.ADODB_create_recordset(1, "SELECT * FROM UTILISATEURS WHERE IDUtilisateur=" & cboUSer.SelectedItem.id)
                        rs.Fields("Disponibilite").Value = 5
                        rs.Update()
                        rs.Close()
                        fermeture_ok = True
                        Me.Close()
                    End If
                Else
                    'DoEvents
                    If DevExpress.XtraEditors.XtraMessageBox.Show(Replace(ClassParent.ListeMessages("MESS_MC_6").Libelle, "#1#", cboUSer.Text), Application.ProductName, MessageBoxButtons.YesNo) = vbYes Then
                        ClassParent.connect_ADO.ADODB_connection(1)
                        Dim rs As ADODB.Recordset
                        rs = ClassParent.connect_ADO.ADODB_create_recordset(1, "SELECT * FROM UTILISATEURS WHERE IDUtilisateur=" & cboUSer.SelectedItem.id)
                        rs.Fields("Disponibilite").Value = 5
                        rs.Update()
                        rs.Close()
                        fermeture_ok = True
                        Me.Close()
                    End If
                End If
            End If
        Else
            fermeture_ok = False
            DevExpress.XtraEditors.XtraMessageBox.Show(ClassParent.ListeMessages("MESS_MC_4").Libelle, Application.ProductName, MessageBoxButtons.OK)
        End If
    End Sub

   
    Private Sub txtPWD_KeyPress(sender As Object, e As System.Windows.Forms.KeyPressEventArgs) Handles txtPWD.KeyPress
        If e.KeyChar = Microsoft.VisualBasic.ChrW(Keys.Return) Then
            BtnLogin.PerformClick()

        End If
    End Sub

    Private Sub frmLogin_FormClosing(sender As Object, e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        If Not fermeture_ok Then
            ClassParent.Stationnaire = 0
        End If
    End Sub

    Private Sub cboUSer_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles cboUSer.SelectedIndexChanged
        If ClassParent.isBioVein Then
            Me.lblPWD.Visible = False
            Me.txtPWD.Visible = False
        End If
    End Sub
End Class