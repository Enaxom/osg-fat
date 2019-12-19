Imports System.Collections.Generic
Imports DevExpress.XtraGrid
Imports System.Drawing
Imports DevExpress.Utils

Public Class frmListeDispos

    Private ClassParent As frmEcran

    Private TOUS As String

    Private listUtilALL As List(Of utilisateur)
    Private listFiltre As List(Of utilisateur)

    Sub New(frmEcran As frmEcran)
        ' TODO: Complete member initialization 

        ' Cet appel est requis par le concepteur.
        InitializeComponent()

        ' Ajoutez une initialisation quelconque après l'appel InitializeComponent().

        ClassParent = frmEcran
        'Skins
        DevExpress.Skins.SkinManager.EnableFormSkins()
        DevExpress.LookAndFeel.LookAndFeelHelper.ForceDefaultLookAndFeelChanged()
        DevExpress.UserSkins.BonusSkins.Register()
        DevExpress.UserSkins.OfficeSkins.Register()

        listUtilALL = New List(Of utilisateur)
        listFiltre = New List(Of utilisateur)

        GridEvent.DataSource = listUtilALL

        ConditionsAdjustment()
    End Sub

    Function Init() As Boolean
        Dim rs As ADODB.Recordset
        Dim utilisateur As utilisateur

        Try

            If ClassParent.ClassParent.connect_ADO.ADODB_connection(1) Then
                rs = ClassParent.ClassParent.connect_ADO.ADODB_create_recordset(1, "SELECT * FROM LANGUE WHERE IdObjet='GENERIQUE' AND IdLangue='BTN_FERMER'")
                btnFermer.Text = rs.Fields("Libelle").Value
                rs.Close()

                rs = ClassParent.ClassParent.connect_ADO.ADODB_create_recordset(1, "SELECT * FROM LANGUE WHERE IdObjet='GENERIQUE' AND IdLangue='LBL_PRESENT'")
                TOUS = rs.Fields("Libelle").Value
                rs.Close()

                rs = ClassParent.ClassParent.connect_ADO.ADODB_create_recordset(1, "SELECT * FROM LANGUE WHERE IdObjet='MC' AND IdLangue='LBL_DISPO'")
                lblDispo.Text = rs.Fields("Libelle").Value
                rs.Close()

                rs = ClassParent.ClassParent.connect_ADO.ADODB_create_recordset(1, "SELECT * FROM LANGUE WHERE IdObjet='MC' AND IdLangue='TITRE_FRMDISPOS'")
                Me.Text = rs.Fields("Libelle").Value
                rs.Close()

                rs = ClassParent.ClassParent.connect_ADO.ADODB_create_recordset(1, "SELECT DISTINCT UTILISATEURS.IDUtilisateur, UTILISATEURS.NomUtilisateur,UTILISATEURS.PrenomUtilisateur, UTILISATEURS.Disponibilite, DISPONIBILITE.libelle FROM UTILISATEURS, DISPONIBILITE WHERE DISPONIBILITE.id=UTILISATEURS.Disponibilite AND ETAT=1")
                If Not IsNothing(rs) Then
                    While Not rs.EOF
                        utilisateur = New utilisateur(rs.Fields(2).Value, rs.Fields(1).Value, rs.Fields(0).Value, rs.Fields(3).Value)
                        utilisateur.dispo = rs.Fields(4).Value
                        listUtilALL.Add(utilisateur)
                        rs.MoveNext()
                    End While
                End If

                rs = ClassParent.ClassParent.connect_ADO.ADODB_create_recordset(1, "SELECT * FROM DISPONIBILITE")
                cboFiltreDispo.Properties.Items.Add(New Data_Set(TOUS, 0))
                If Not IsNothing(rs) Then
                    While Not rs.EOF
                        cboFiltreDispo.Properties.Items.Add(New Data_Set(rs.Fields("libelle").Value, CInt(rs.Fields("id").Value)))
                        rs.MoveNext()
                    End While
                End If

                For Each item As Data_Set In cboFiltreDispo.Properties.Items
                    If item.id = 0 Then
                        cboFiltreDispo.SelectedItem = item
                        Exit For
                    End If
                Next
            Else
                Return False
            End If

            GridEvent.RefreshDataSource()

            ClassParent.ClassParent.connect_ADO.ADODB_deconnection(1)
            Return True
        Catch ex As Exception
            Return False
        End Try

    End Function

    Private Function convert_couleur(ByVal couleur As Long) As Color
        Dim blue As Long = Int(couleur / 65536)
        Dim green As Long = Int((couleur - (65536 * blue)) / 256)
        Dim red As Long = couleur - ((blue * 65536) + (green * 256))
        Dim color As Color = color.FromArgb(255, red, green, blue)
        Return color
    End Function

    Private Function convert_argb(ByVal rouge As Long, ByVal bleu As Long, ByVal vert As Long) As Long
        Return (rouge + vert * 256 + bleu * 256 * 256)
    End Function

    Private Sub ConditionsAdjustment()

        Dim color As Integer
        Dim libelle As String


        Dim rs As ADODB.Recordset
        If ClassParent.ClassParent.connect_ADO.ADODB_connection(1) Then
            rs = ClassParent.ClassParent.connect_ADO.ADODB_create_recordset(1, "SELECT * FROM DISPONIBILITE WHERE id=1")
            color = CInt(rs.Fields("couleur").Value)
            libelle = rs.Fields("libelle").Value
            rs.Close()

            Dim cn As StyleFormatCondition
            cn = New StyleFormatCondition(FormatConditionEnum.Equal, GridView1.Columns("CLN_Dispo"), Nothing, 1)
            cn.Appearance.BackColor = convert_couleur(color)
            cn.ApplyToRow = True
            GridView1.FormatConditions.Add(cn)

            rs = ClassParent.ClassParent.connect_ADO.ADODB_create_recordset(1, "SELECT * FROM DISPONIBILITE WHERE id=2")
            color = CInt(rs.Fields("couleur").Value)
            libelle = rs.Fields("libelle").Value
            rs.Close()

            cn = New StyleFormatCondition(FormatConditionEnum.Equal, GridView1.Columns("CLN_Dispo"), Nothing, 2)
            cn.Appearance.BackColor = convert_couleur(color)
            cn.ApplyToRow = True
            GridView1.FormatConditions.Add(cn)

            rs = ClassParent.ClassParent.connect_ADO.ADODB_create_recordset(1, "SELECT * FROM DISPONIBILITE WHERE id=3")
            color = CInt(rs.Fields("couleur").Value)
            libelle = rs.Fields("libelle").Value
            rs.Close()

            cn = New StyleFormatCondition(FormatConditionEnum.Equal, GridView1.Columns("CLN_Dispo"), Nothing, 3)
            cn.Appearance.BackColor = convert_couleur(color)
            cn.ApplyToRow = True
            GridView1.FormatConditions.Add(cn)

            rs = ClassParent.ClassParent.connect_ADO.ADODB_create_recordset(1, "SELECT * FROM DISPONIBILITE WHERE id=4")
            color = CInt(rs.Fields("couleur").Value)
            libelle = rs.Fields("libelle").Value
            rs.Close()

            cn = New StyleFormatCondition(FormatConditionEnum.Equal, GridView1.Columns("CLN_Dispo"), Nothing, 4)
            cn.Appearance.BackColor = convert_couleur(color)
            cn.ApplyToRow = True
            GridView1.FormatConditions.Add(cn)

            rs = ClassParent.ClassParent.connect_ADO.ADODB_create_recordset(1, "SELECT * FROM DISPONIBILITE WHERE id=5")
            color = CInt(rs.Fields("couleur").Value)
            libelle = rs.Fields("libelle").Value
            rs.Close()

            cn = New StyleFormatCondition(FormatConditionEnum.Equal, GridView1.Columns("CLN_Dispo"), Nothing, 5)
            cn.Appearance.BackColor = convert_couleur(color)
            cn.ApplyToRow = True
            GridView1.FormatConditions.Add(cn)

            rs = ClassParent.ClassParent.connect_ADO.ADODB_create_recordset(1, "SELECT * FROM DISPONIBILITE WHERE id=6")
            color = CInt(rs.Fields("couleur").Value)
            libelle = rs.Fields("libelle").Value
            rs.Close()

            cn = New StyleFormatCondition(FormatConditionEnum.Equal, GridView1.Columns("CLN_Dispo"), Nothing, 6)
            cn.Appearance.BackColor = convert_couleur(color)
            cn.ApplyToRow = True
            GridView1.FormatConditions.Add(cn)


            '<gridControl1>
            GridView1.BestFitColumns()
        End If
        ClassParent.ClassParent.connect_ADO.ADODB_deconnection(1)
    End Sub

    Private Sub cboFiltreDispo_SelectedIndexChanged(sender As System.Object, e As System.EventArgs) Handles cboFiltreDispo.SelectedIndexChanged
        If cboFiltreDispo.SelectedItem.id = 0 Then
            listFiltre = New List(Of utilisateur)
            For Each u As utilisateur In listUtilALL
                If u.idDispo <> 1 Then
                    listFiltre.Add(u)
                End If
            Next
            GridEvent.DataSource = listFiltre
        Else
            listFiltre = New List(Of utilisateur)
            For Each u As utilisateur In listUtilALL
                If u.idDispo = cboFiltreDispo.SelectedItem.id Then
                    listFiltre.Add(u)
                End If

            Next
            GridEvent.DataSource = listFiltre
        End If
    End Sub

    Private Sub btnFermer_Click(sender As System.Object, e As System.EventArgs) Handles btnFermer.Click
        Me.Close()
    End Sub
End Class