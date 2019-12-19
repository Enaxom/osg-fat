Imports System.Drawing

Public Class Alerte
    '-------------------------------------------------
    '--     Passerelle OSGRIM < - > SINOVIA         --
    '-------------------------------------------------
    '--                  16/06/2009                 --
    '-------------------------------------------------
    '--  Auteur : DALLARD Jérémy                    --
    '-------------------------------------------------

    'La classe Alerte est une classe rajoutée à la Main Courante
    ' Afin de pouvoir gérer les Alertes déclenchées par
    ' l'insertion de données dans la table Alerte de la base
    ' Gestion.
    'Cette classe Alerte permet de générer une icône et un son
    ' d'alerte lorsqu'il y a des alertes en cours.
    ' Pour déclencher une Alerte, il suffit d'insérer un nouveau champ
    ' Dans la table ALERTE. pour le moment, le clic sur l'icone
    ' de l'alerte n'ouvre qu'une demande d'intervention.

    'Variable Handle de la classe Main courante
    ' qui nous permet de définir qui a appelé cette classe
    Public ClassParent As Init_MC

    'ID de la Demande d'intervention concernée par l'alerte en cours.
    ' Lorsqu'on séléctionne les alertes dans la Base de données,
    ' on les ordonnent du plus ancien au plus récent. Cela veut donc dire que
    ' Cette variable contient l'ID de l'alerte la plus ancienne qui a étée déclenchée
    Public IDDemIntervention As Long
    Public LibelleCatIntervention As String
    Public IDCatIntervention As Long

    'GESTION COULEUR DE L'ALARME
    'Code couleur correspondant à la catégorie de l'intervention
    Public IDCodeCouleur As Integer
    Public LibelleCouleur As String
    Public CodeCouleur As String

    'Variable qui va nous permettre de stocker une requête SQL lorsque
    ' l'on travaillera avec la base de données.
    Public SQL As String

    'Variable contenant le nombre d'interventions total concernées par
    ' une alerte. Tant que ce nombre est différent de 0, on affiche l'alerte
    ' et on relance le son.
    Public NombreDInterventions As Long

    'Ces Constantes servent à manipuler la fonction sndPlaySound
    Public Enum SND_Settings
        SND_SYNC = &H0
        SND_ASYNC = &H1
        SND_NODEFAULT = &H2
        SND_MEMORY = &H4
        SND_LOOP = &H8
        SND_NOSTOP = &H10
        SW_SHOW = 5
    End Enum



    'Cette fonction est appelée dans la fonction Lecture de la classe
    ' Evenements. Elle se déclenche seulement si la fonction parente
    ' a détecté au moins une alerte dans la table Alertes.
    Public Sub Afficher()


        'Handle de l'enregistrement en cours de la base de données
        Dim data As ADODB.Recordset

        'Variable temporaire nous permettant de stocker le calcul
        ' du nombre total d'alertes générées par le système.
        Dim Temp As Long
        'On initialise Temp à 0 pour éviter tout problèmes.
        Temp = 0

        'Ouverture de la connexion avec la base. le chemin de la base
        ' Gestion est stockée dans les variables globales de la classe parente
        ' MainCourante.
        If ClassParent.connect_ADO.ADODB_connection(2) Then

            'On séléctionne l'ensemble des Alertes et on trie selon la date de création
            ' du plus ancien au plus récent.
            SQL = "SELECT * FROM ALERTES ORDER BY DateCreation;"
            data = ClassParent.connect_ADO.ADODB_create_recordset(2, SQL)

            'On stocke l'id de l'intervention la plus ancienne. Cela nous servira pour
            ' ouvrir la bonne demande d'intervention.
            Me.IDDemIntervention = data.Fields("cleDemIntervention").Value

            'On calcule le nombre d'entrées se trouvant dans la table Alertes.
            While Not data.EOF

                Temp = Temp + 1
                data.MoveNext()

            End While
            data.Close()
            data = Nothing
            'ClassParent.connect_ADO.ADODB_deconnection(2)
            'Récupération du code couleur et du libelle de la categorie d'intervention

            SQL = "SELECT * FROM DEM_MOTIF WHERE cleDEMINTERVENTION = " & Me.IDDemIntervention & " ;"
            data = ClassParent.connect_ADO.ADODB_create_recordset(2, SQL)
            Me.IDCatIntervention = data.Fields("cleMotif").Value
            data.Close()
        
            data = Nothing
        
            If ClassParent.connect_ADO.ADODB_connection(1) Then
                SQL = "SELECT * FROM CATEGORIES_INTERVENTIONS WHERE IDCategorieIntervention = " & Me.IDCatIntervention & " ;"
                data = ClassParent.connect_ADO.ADODB_create_recordset(1, SQL)
                If data.Fields("CodeCouleur").Value & "" = "" Then
                    Me.CodeCouleur = System.Drawing.Color.White.ToString
                Else
                    Me.CodeCouleur = data.Fields("CodeCouleur").Value
                End If
                Me.LibelleCatIntervention = data.Fields("NomCategorieIntervention").Value
                data.Close()
                data = Nothing
            End If
            data = Nothing
        
            'On affecte Temp au nombre d'interventions seulement si Temp est différent de 0.
            ' Ainsi on évitera de modifier cette valeur lors d'un nouvel appel de la fonction Alerte.
            ' c'est une double sécurité car en théorie, si le nombre d'alertes est nul, la fonction
            ' Afficher ne se lancera pas.
            If Temp > 0 Then
                Me.NombreDInterventions = Temp
            End If

            'On affiche l'icone de l'alerte seulement si il n'était pas déjà visible.
            If Not Me.ClassParent.FrmFenetre.CMD_Alerte.Visible Then
                Me.ClassParent.FrmFenetre.CMD_Alerte.Visible = True
            End If

            'Enfin, on lance le son d'alerte en boucle tant que
            ' on n'a pas appelé le son stop.
            Me.PlayWave()

        End If

        'De plus, on active le timer pour l'affichage des alertes
        Me.ClassParent.FrmFenetre.Timer1.Enabled = True

        'Me.ChangerCouleur

    End Sub

    'Cette fonction est appelée lors de l'initialisation de la feuille principale
    ' de la main courante. Elle nous sers à initialiser la zone de bouton
    ' correspondant à l'alerte.
    Public Sub Generer()

        'Par sécurité, on initialise nos valeurs principales de calculs à zéro.
        Me.IDDemIntervention = 0
        Me.NombreDInterventions = 0

        'Maintenant, on initialise les images se trouvant dans ce bouton que l'on rend invisible
        ' dans la foulée.
        Me.ClassParent.FrmFenetre.CMD_Alerte.Visible = False

        'Enfin, par sécurité, on arrête le son bien qu'en théorie il n'y a pas de raisons qu'il soit lancé
        ' à ce moment là.
        Me.StopWave()

    End Sub

    'Cette fonction est appelée lorsque l'on clique sur l'icone de l'alerte.
    ' elle nous permet de masquer cette alerte. Si on a un nombre d'interventions
    ' nul, cette fonction stoppera aussi le son correspondant à cette alerte.
    'De plus, Masquer() supprime l'enregistrement dans la table ALERTES correspondant à
    ' la demande d'intervention qui est lancée juste après.
    Public Sub Masquer()
        Dim data As ADODB.Recordset

        If ClassParent.connect_ADO.ADODB_connection(2) Then

            'On séléctionne l'ensemble des Alertes et on trie selon la date de création
            ' du plus ancien au plus récent.
            SQL = "SELECT * FROM ALERTES ORDER BY DateCreation;"
            data = ClassParent.connect_ADO.ADODB_create_recordset(2, SQL)

            'On stocke l'id de l'intervention la plus ancienne. Cela nous servira pour
            ' supprimer la bonne demande d'intervention.
            Me.IDDemIntervention = data.Fields("cleDemIntervention").Value


            'Récupération du code couleur et du libelle de la categorie d'intervention
            data = Nothing
            ClassParent.connect_ADO.ADODB_deconnection(2)
            If ClassParent.connect_ADO.ADODB_connection(1) Then
                SQL = "SELECT * FROM CATEGORIES_INTERVENTIONS WHERE IDCategorieIntervention = " & Me.IDCatIntervention & " ;"
                data = ClassParent.connect_ADO.ADODB_create_recordset(1, SQL)
                If data.Fields("CodeCouleur").Value & "" = "" Then
                    Me.CodeCouleur = Color.White.ToString
                Else
                    Me.CodeCouleur = data.Fields("CodeCouleur").Value
                End If
                Me.LibelleCatIntervention = data.Fields("NomCategorieIntervention").Value

                data = Nothing
                ClassParent.connect_ADO.ADODB_deconnection(1)
            End If

            'On lance l'intevervention correspondant à cette alerte. On place cette ligne
            ' juste après l'arrêt du son au cas où le son continue de tourner pendant
            ' l'éxécution de la demande d'intervention
            Me.LancerDemIntervention()

            'On lance la fonction qui va nous supprimer une alerte.
            Me.SupprimerAlerte()

            'Si le nombre d'interventions calculé est nul, on alerte le son d'alerte.
            ' ce nombre d'interventions est décrémenté dans la fonction supprimerAlerte.
            ' c'est pour cela qu'il a fallut placer ce test après l'appel de cette fonction.
            If Me.NombreDInterventions = 0 Then
                Me.StopWave()
                Me.ClassParent.FrmFenetre.Timer1.Enabled = False
                ClassParent.FrmFenetre.BackColor = Color.White
            Else
                'ClassParent.FrmFenetre.Timer1.Enabled = True
            End If


        End If

    End Sub

    'Cette fonction nous permet de définir où se trouve le fichier Wave à lire
    ' et nous permet de lancer ce même son.
    Public Sub PlayWave(Optional ByVal Settings As SND_Settings = SND_Settings.SND_ASYNC Or SND_Settings.SND_LOOP)

        'Dim WAVFile As Object
        ClassParent.sound_alarme.PlayLooping()

        'WAVFile = API.ReadIni("SYSTEME", "CheminSonAlarme", Parent.LeCheminINI)
        'Call sndPlaySound(WAVFile, Settings)

    End Sub

    'Cette fonction nous permet de stopper le son qui est en train d'être joué.
    Public Sub StopWave()
        ClassParent.sound_alarme.Stop()
        ClassParent.sound_rappel.Stop()
        'Call sndPlaySound(vbNullString, False)
    End Sub

    'Cette fonction nous permet de supprimer une alerte de la base de données
    ' et de décrémenter le nombreD'interventions en cours.
    ' De plus, elle s'occupe de l'affichage de l'alerte en fonction du nombre
    ' d'alertes encore à traiter.
    Public Sub SupprimerAlerte()

        'On manipule toujours les deux mêmes variables pour traiter des échanges
        ' avec la base de données.
        Dim data As ADODB.Recordset


        'ouverture de la base. On éxécute le code seulement si l'ouverture s'est bien passée.

        If ClassParent.connect_ADO.ADODB_connection(2) Then
            'On ne supprime dans la base que l'alerte correspondant à l'Id de la demande
            ' d'intervention en cours de traitement.
            SQL = "SELECT * FROM ALERTES WHERE cleDemIntervention = " & Me.IDDemIntervention & " ;"
            data = ClassParent.connect_ADO.ADODB_create_recordset(2, SQL)
            data.Delete()
            data.Update()
            data = Nothing

            'On séléctionne l'ensemble des Alertes et on trie selon la date de création
            ' du plus ancien au plus récent.
            SQL = "SELECT * FROM ALERTES WHERE cleDemIntervention = " & Me.IDDemIntervention + 1 & ""
            data = ClassParent.connect_ADO.ADODB_create_recordset(2, SQL)

            'On stocke l'id de l'intervention la plus ancienne. Cela nous servira pour
            ' supprimer la bonne demande d'intervention.
            If Not data.EOF Then
                Me.IDDemIntervention = data.Fields("cleDemIntervention").Value
            End If
            data.Close()
        End If

        'On décrémente le nombre d'interventions gérées par la classe.
        Me.NombreDInterventions = Me.NombreDInterventions - 1

        If ClassParent.connect_ADO.ADODB_connection(1) Then
            SQL = "SELECT * FROM CATEGORIES_INTERVENTIONS WHERE IDCategorieIntervention = " & Me.IDCatIntervention & " ;"
            data = ClassParent.connect_ADO.ADODB_create_recordset(1, SQL)
            If data.Fields("CodeCouleur").Value & "" = "" Then
                Me.CodeCouleur = Color.White.ToString
            Else
                Me.CodeCouleur = data.Fields("CodeCouleur").Value
            End If
            Me.LibelleCatIntervention = data.Fields("NomCategorieIntervention").Value

            data = Nothing
            ClassParent.connect_ADO.ADODB_deconnection(1)
        End If
        'Me.ChangerCouleur

        'Si après cette décrémentation, on a toujours une alerte à traiter,
        ' on redessine le bouton et on le remet en visible par sécurité.
        ' Sinon, on le rend invisible.
        If Me.NombreDInterventions > 0 Then
            Me.ClassParent.FrmFenetre.CMD_Alerte.Visible = True
        Else
            Me.ClassParent.FrmFenetre.CMD_Alerte.Visible = False
        End If


    End Sub

    'Cette fonction va nous permettre de lancer l'interface de la demande d'intervention
    ' qui doit être traitée au clic sur le bouton de l'alerte.
    Public Sub LancerDemIntervention()

        'Variables classiques de manipulation de base de données.
        Dim data As ADODB.Recordset

        'Cette objet correspond à un évènement. Dans ce code, cet évènement
        ' est une demande d'intervention générée automatiquement par le logiciel
        ' SINOVIA car cette classe a étée conçut pour cette passerelle.
        ' Pour changer le genre de modules à lancer, il faut modifier la requête ci dessous
        ' et remplacer 'DINTER' par une variable contenant la chaîne permettant d'identifier
        ' le type de module à lancer.
        Dim Obj As grid_row

        'On ouvre la base Gestion pour travailler sur la Main Courante.
        If ClassParent.connect_ADO.ADODB_connection(2) Then
            'Requête permettant de séléctionner l'évènement correspondant à l'alerte en cours
            ' dans la table MC.
            SQL = "SELECT * FROM MC WHERE Objet = 'DINTER' AND cleobj = " & Me.IDDemIntervention & ";"
            data = ClassParent.connect_ADO.ADODB_create_recordset(2, SQL)

            'On créé notre nouvel évènement et on initialise ses variables
            ' pour pouvoir lancer notre demande d'intervention. Ce code est quasiement
            ' un copié collé du code de la fonction lecture  de la classe evenements.
            Obj = New grid_row(ClassParent)
            Obj.CleObj = Me.IDDemIntervention
            Obj.EstBrouillon = CBool(data.Fields("EstBrouillon").Value)
            Obj.EstEncours = CBool(data.Fields("EstEncours").Value)
            Obj.EstSupp = CBool(data.Fields("EstSupp").Value)
            Obj.Titre = data.Fields("Affichage").Value
            Obj.TypeObj = data.Fields("Objet").Value
            Obj.DateHeure = CDbl(data.Fields("DateHeureDebut").Value)
            Obj.IndexEvt = CLng(data.Fields("CleMc").Value)
            Obj.IdOperateur = CLng(ClassParent.Stationnaire)
            Obj.AttenteRapport = CBool(data.Fields("AttenteRapport").Value)

            'On éxécute notre évènement en lui indiquant comme mode 1 car ce mode
            ' correspond à l'affichage d'une nouvelle demande d'intervention.
            ' voir classe Evenement.exec pour plus d'informations.
            ClassParent.FrmFenetre.exec(1, Obj)
        End If

    End Sub

    Public Sub EcranClignote()

        If ClassParent.FrmFenetre.CMD_Alerte.Visible = True Then
            If ClassParent.FrmFenetre.BackColor = Color.White Then
                ClassParent.FrmFenetre.BackColor = convert_couleur(CodeCouleur)
        Else
            ClassParent.FrmFenetre.BackColor = Color.White
            End If
        End If


    End Sub


    Private Function convert_couleur(ByVal couleur As Long) As Drawing.Color
        Dim blue As Long = Int(couleur / 65536)
        Dim green As Long = Int((couleur - (65536 * blue)) / 256)
        Dim red As Long = couleur - ((blue * 65536) + (green * 256))
        Dim color As Drawing.Color = Drawing.Color.FromArgb(255, red, green, blue)
        Return color
    End Function

    'Public Sub ChangerCouleur()
    '
    '    Dim varBdd As BD
    '    Dim data As Object
    '    Set varBdd = New BD
    '
    '        If varBdd.OuvertureTable(Parent.CheminOsgrim) Then
    '                Me.SQL = "SELECT * FROM CATEGORIES_INTERVENTIONS WHERE IDCategorieIntervention = " & Me.IDCatIntervention & " ;"
    '                Set data = varBdd.Bdd.Bdd.OpenRecordset(SQL)
    '                Me.CodeCouleur = data.Fields("CodeCouleur")
    '                data.rsClose
    '        End If
    'End Sub








End Class
