Imports System.Windows.Forms

Public Class frmlvp
    Friend ClassParent As Init_MC

    Sub New(ByVal clParent As Init_MC)
        InitializeComponent()
        ClassParent = clParent
        'Skins
        DevExpress.Skins.SkinManager.EnableFormSkins()
        DevExpress.LookAndFeel.LookAndFeelHelper.ForceDefaultLookAndFeelChanged()
        DevExpress.UserSkins.BonusSkins.Register()
        DevExpress.UserSkins.OfficeSkins.Register()
        charger_labels()
    End Sub

    Private Sub charger_labels()
        Dim sql As String = ""
        Dim rs As ADODB.Recordset
        Dim i As Integer
        If ClassParent.connect_ADO.ADODB_connection(1) Then
            rs = ClassParent.connect_ADO.ADODB_create_recordset(1, "SELECT * FROM LANGUE WHERE IdObjet='DIA' AND IdLangue='LVP_TITRE'")
            If Not rs.EOF Then
                Me.Text = rs.Fields("Libelle").Value
            End If
            rs.Close()
            rs = ClassParent.connect_ADO.ADODB_create_recordset(1, "SELECT * FROM LANGUE WHERE IdObjet='GENERIQUE' AND IdLangue='BTN_VALIDER'")
            If Not rs.EOF Then
                Me.BtnOK.Text = rs.Fields("Libelle").Value
            End If
            rs.Close()
            rs = ClassParent.connect_ADO.ADODB_create_recordset(1, "SELECT * FROM LANGUE WHERE IdObjet='GENERIQUE' AND IdLangue='BTN_ANNULER'")
            If Not rs.EOF Then
                Me.BtnAnnuler.Text = rs.Fields("Libelle").Value
            End If
            rs.Close()

            sql = "Select * FROM IA_VENT Order By idVent"
            rs = ClassParent.connect_ADO.ADODB_create_recordset(1, sql)
            i = 0
            While Not rs.EOF
                Me.RadioGroup_Vent.Properties.Items.Item(i).Description = rs.Fields("NomVent").Value
                rs.MoveNext()
                i = i + 1
            End While
            Me.RadioGroup_Vent.SelectedIndex = ClassParent.idVENT - 1

            sql = "Select * FROM IA_LVP Order By idLVP"
            rs = ClassParent.connect_ADO.ADODB_create_recordset(1, sql)
            i = 0
            While Not rs.EOF
                Me.RadioGroup_LVP.Properties.Items.Item(i).Description = rs.Fields("NomLVP").Value
                rs.MoveNext()
                i = i + 1
            End While
            Me.RadioGroup_LVP.SelectedIndex = ClassParent.idLVP - 1

            sql = "Select * FROM IA_Doublet Order By idDoublet"
            rs = ClassParent.connect_ADO.ADODB_create_recordset(1, sql)
            i = 0
            While Not rs.EOF
                Me.RadioGroup_Doublet.Properties.Items.Item(i).Description = rs.Fields("NomDoublet").Value
                rs.MoveNext()
                i = i + 1
            End While
            Me.RadioGroup_Doublet.SelectedIndex = ClassParent.idDoublet - 1

            If ClassParent.idDoublet = 3 Then
                Me.btnDoublet.Image = Global.MC.My.Resources.Resources.Smileys2
            Else ' Nuages
                Me.btnDoublet.Image = Global.MC.My.Resources.Resources.Smileys1
            End If
            If ClassParent.idLVP = 3 Then 'OK
                Me.btnLvp.Image = Global.MC.My.Resources.Resources.soleil
            Else ' PAS OK
                Me.btnLvp.Image = Global.MC.My.Resources.Resources.soleilnuageux
            End If
        End If
            ClassParent.connect_ADO.ADODB_deconnection(1)
    End Sub

    Public Sub Init()

    End Sub

    Private Sub MAJ_Interface()
        If Me.RadioGroup_Doublet.SelectedIndex = 2 Then
            Me.btnDoublet.Image = Global.MC.My.Resources.Resources.Smileys2
        Else ' Nuages
            Me.btnDoublet.Image = Global.MC.My.Resources.Resources.Smileys1
        End If
        If Me.RadioGroup_LVP.SelectedIndex = 2 Then 'OK
            Me.btnLvp.Image = Global.MC.My.Resources.Resources.soleil
        Else ' PAS OK
            Me.btnLvp.Image = Global.MC.My.Resources.Resources.soleilnuageux
        End If
    End Sub
 
    Private Sub BtnOK_Click(sender As Object, e As System.EventArgs) Handles BtnOK.Click
        Dim chaine As String = ""
        Dim i As Integer = 0
        Dim Changement As Boolean = False

        ClassParent.connect_ADO.ADODB_connection(1)

        If Me.RadioGroup_Vent.SelectedIndex + 1 <> ClassParent.idVENT Then
            Changement = True
            'Mise à jour dans MC
            ClassParent.idVENT = Me.RadioGroup_Vent.SelectedIndex + 1
            ClassParent.FrmFenetre.lblVent.Text = Me.RadioGroup_Vent.Properties.Items.Item(RadioGroup_Vent.SelectedIndex).Description
            'Enregistrement dans base de données
            chaine = "INSERT INTO MC (DateHeureDebut,Affichage,Objet,EstSupp,CleObj,EstEnCours,CleStationnaire,EstBrouillon,AttenteRapport)"
            chaine = chaine & " VALUES (" & Replace(Date.Now.ToOADate.ToString, ",", ".") & ",'"
            chaine = chaine & Replace(Me.RadioGroup_Vent.Properties.Items.Item(RadioGroup_Vent.SelectedIndex).Description, "'", "''") & "','LVP',0,0,0"
            chaine = chaine & "," & ClassParent.Stationnaire & ",0,1)"
            ClassParent.connect_ADO.ADODB_delete(2, chaine)
        End If

        If Me.RadioGroup_LVP.SelectedIndex + 1 <> ClassParent.idLVP Then
            Changement = True
            'Mise à jour dans MC
            ClassParent.idLVP = Me.RadioGroup_LVP.SelectedIndex + 1
            ClassParent.FrmFenetre.lblLVP.Text = Me.RadioGroup_LVP.Properties.Items.Item(RadioGroup_LVP.SelectedIndex).Description
            'Enregistrement dans base de données
            chaine = "INSERT INTO MC (DateHeureDebut,Affichage,Objet,EstSupp,CleObj,EstEnCours,CleStationnaire,EstBrouillon,AttenteRapport)"
            chaine = chaine & " VALUES (" & Replace(Date.Now.ToOADate.ToString, ",", ".") & ",'"
            chaine = chaine & Replace(Me.RadioGroup_LVP.Properties.Items.Item(RadioGroup_LVP.SelectedIndex).Description, "'", "''") & "','LVP',0,0,0"
            chaine = chaine & "," & ClassParent.Stationnaire & ",0,1)"
            ClassParent.connect_ADO.ADODB_delete(2, chaine)
        End If

        If Me.RadioGroup_Doublet.SelectedIndex + 1 <> ClassParent.idDoublet Then
            Changement = True
            'Mise à jour dans MC
            ClassParent.idDoublet = Me.RadioGroup_Doublet.SelectedIndex + 1
            ClassParent.FrmFenetre.lblDoublet.Text = Me.RadioGroup_Doublet.Properties.Items.Item(RadioGroup_Doublet.SelectedIndex).Description
            'Enregistrement dans base de données
            chaine = "INSERT INTO MC (DateHeureDebut,Affichage,Objet,EstSupp,CleObj,EstEnCours,CleStationnaire,EstBrouillon,AttenteRapport)"
            chaine = chaine & " VALUES (" & Replace(Date.Now.ToOADate.ToString, ",", ".") & ",'"
            chaine = chaine & Replace(Me.RadioGroup_Doublet.Properties.Items.Item(RadioGroup_Doublet.SelectedIndex).Description, "'", "''") & "','LVP',0,0,0"
            chaine = chaine & "," & ClassParent.Stationnaire & ",0,1)"
            ClassParent.connect_ADO.ADODB_delete(2, chaine)
        End If

        If Changement Then
            ClassParent.connect_ADO.ADODB_delete(2, "UPDATE IA_ETAT SET idLVP=" & ClassParent.idLVP & ", idVent=" & ClassParent.idVENT & ", idEtat=" & ClassParent.idDoublet & ";")
        End If

        'Gestion des images
        If ClassParent.idDoublet = 3 Then
            ClassParent.FrmFenetre.btnDoublet.Image = Global.MC.My.Resources.Resources.Smileys2
        Else ' Nuages
            ClassParent.FrmFenetre.btnDoublet.Image = Global.MC.My.Resources.Resources.Smileys1
        End If
        If ClassParent.idLVP = 3 Then 'SOLEIL
            ClassParent.FrmFenetre.btnLvp.Image = Global.MC.My.Resources.Resources.soleil
        Else ' Nuages
            ClassParent.FrmFenetre.btnLvp.Image = Global.MC.My.Resources.Resources.soleilnuageux
        End If
        Me.Close()
    End Sub

    Private Sub BtnAnnuler_Click(sender As Object, e As System.EventArgs) Handles BtnAnnuler.Click
        Me.Close()
    End Sub

    Private Sub RadioGroup_Doublet_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles RadioGroup_Doublet.SelectedIndexChanged
        MAJ_Interface()
    End Sub

    Private Sub RadioGroup_LVP_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles RadioGroup_LVP.SelectedIndexChanged
        MAJ_Interface()
    End Sub
End Class