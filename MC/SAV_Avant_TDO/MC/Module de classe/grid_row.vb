Imports System.Drawing
Imports System.Windows.Forms
Imports System.Net.Mail

Friend Class grid_row

    Private _titre As String
    Private _operateur As String
    Private _idOperateur As Long
    Private _cleObj As String
    Private _EstBrouillon As Boolean
    Private _EstEncours As Boolean
    Private _EstSuppr As Boolean
    Private _typeObj As String
    Private _indexEvt As Long
    Private _AttenteRapport As Boolean
    Private _IsActionDiff As Boolean
    Private _dateHeure As Double
    Private _dateEvent As DateTime
    Private _heureEvent As String
    Private _IDMachineExe As Integer
    Private _IdInter As Long = 0
    Private _IdMail As Integer = 0
    Private _Commentaire As String
    Private _Bat As String
    'RAPPEL
    Private DateRappel As Long
    Private DateFin As Long
    Private Famille As Long
    Private JMois As Integer
    Private NbSemaine As Integer
    Private Lundi As Boolean
    Private Mardi As Boolean
    Private Mercredi As Boolean
    Private Jeudi As Boolean
    Private Vendredi As Boolean
    Private Samedi As Boolean
    Private Dimanche As Boolean
    Private HeureDebut As String

    Dim _biffage As Icon
    Dim _annuler As Image
    Private ClassParent As Init_MC


    Public Sub New(ByVal parentClass As Init_MC)
        ClassParent = parentClass
        _annuler = My.Resources.icone_croix_suppr
        _biffage = My.Resources.biffage_16
    End Sub

    Public Property Biffage() As System.Drawing.Icon
        Get
            Return _biffage
        End Get
        Set(ByVal value As System.Drawing.Icon)
            _biffage = value
        End Set
    End Property

    Public Property Annuler() As System.Drawing.Image
        Get
            Return _annuler
        End Get
        Set(ByVal value As System.Drawing.Image)
            _annuler = value
        End Set
    End Property

    Public Property IdInter() As Long
        Get
            Return _IdInter
        End Get
        Set(ByVal value As Long)
            _IdInter = value
        End Set
    End Property

    Public Property IdMail() As Integer
        Get
            Return _IdMail
        End Get
        Set(ByVal value As Integer)
            _IdMail = value
        End Set
    End Property

    Public Property DateHeure() As Double
        Get
            Return _dateHeure
        End Get
        Set(ByVal value As Double)
            _dateHeure = value
            _heureEvent = Date.FromOADate(value).ToShortTimeString
            _dateEvent = Date.FromOADate(value).ToShortDateString
        End Set
    End Property

    Public Property Operateur() As String
        Get
            Return _operateur
        End Get
        Set(ByVal value As String)
            _operateur = value
        End Set
    End Property

    Public Property IdOperateur() As Long
        Get
            Return _idOperateur
        End Get
        Set(ByVal value As Long)
            _idOperateur = value
        End Set
    End Property

    Public Property Titre() As String
        Get
            Return _titre
        End Get
        Set(ByVal value As String)
            _titre = value
        End Set
    End Property

    Property CleObj As String
        Get
            Return _cleObj
        End Get
        Set(ByVal value As String)
            _cleObj = value
        End Set
    End Property

    Property EstBrouillon As Boolean
        Get
            Return _EstBrouillon
        End Get
        Set(ByVal value As Boolean)
            _EstBrouillon = value
        End Set
    End Property

    Property IsActionDiff As Boolean
        Get
            Return _IsActionDiff
        End Get
        Set(ByVal value As Boolean)
            _IsActionDiff = value
        End Set
    End Property

    Property EstEncours As Boolean
        Get
            Return _EstEncours
        End Get
        Set(ByVal value As Boolean)
            _EstEncours = value
        End Set
    End Property

    Property EstSupp As Boolean
        Get
            Return _EstSuppr
        End Get
        Set(ByVal value As Boolean)
            _EstSuppr = value
        End Set
    End Property

    Property TypeObj As String
        Get
            Return _typeObj
        End Get
        Set(ByVal value As String)
            _typeObj = value
        End Set
    End Property

    Property IndexEvt As Long
        Get
            Return _indexEvt
        End Get
        Set(ByVal value As Long)
            _indexEvt = value
        End Set
    End Property

    Property AttenteRapport As Boolean
        Get
            Return _AttenteRapport
        End Get
        Set(ByVal value As Boolean)
            _AttenteRapport = value
        End Set
    End Property

    Property DateEvent As DateTime
        Get
            Return _dateEvent
        End Get
        Set(ByVal value As DateTime)
            _dateEvent = value
        End Set
    End Property

    Property HeureEvent As String
        Get
            Return _heureEvent
        End Get
        Set(ByVal value As String)
            _heureEvent = value
        End Set
    End Property

    Property IDMachineExe As Integer
        Get
            Return _IDMachineExe
        End Get
        Set(ByVal value As Integer)
            _IDMachineExe = value
        End Set
    End Property

    Public Sub Affichage(ByVal frm_Ecran As frmEcran, Optional Filtre As String = "", Optional FenetreDefault As Object = Nothing)

        Dim ListeAj As Object
        Dim IndexIns As Long
        Dim NBInterval As Long
        Dim _smtp_mail As String

        On Error GoTo Affichage_Erreur

        ListeAj = FenetreDefault
        Me.IsActionDiff = False


        If UCase(TypeObj) = "RAPPEL" And EstEncours And frm_Ecran.Visible Then
            If ((DateHeure) < (Date.Now).ToOADate) Then
                'AJOUT DU TEST D'ENVOI DE MAIL
                If IdMail > 0 Then
                    'TEST POUR ENVOI DU MAIL
                    If ClassParent.Smtp_Ok = 0 Then
                        _smtp_mail = ClassParent.comunication_ini.GetCle("SYSTEME", "SMTP")
                        If _smtp_mail = "" Then
                            ClassParent.Smtp_Ok = 1 'CHAMP NON RENSEIGNE
                            'DevExpress.XtraEditors.XtraMessageBox.Show("SMTP Non configuré, envoi de mail impossible", Application.ProductName, MessageBoxButtons.OK)
                        Else
                            ClassParent.Smtp_Ok = 2 'CHAMP RENSEIGNE
                        End If
                    End If
                    'SI PARAMETRAGE EFFECTUE ON TRAITE
                    If ClassParent.Smtp_Ok = 2 Then Envoi_Mail()
                    frm_Ecran.GridEvent.RefreshDataSource()
                Else
                    If (IDMachineExe = frm_Ecran.ClassParent.IDMachine) Then
                        On Error Resume Next
                        frm_Ecran.ClassParent.sound_rappel.PlayLooping()
                        On Error GoTo 0
                        frm_Ecran.exec(10, Me)
                        frm_Ecran.GridEvent.RefreshDataSource()
                        'mise en pile pour exécution d'actions différées
                        frm_Ecran.ClassParent.sound_rappel.Stop()
                        IsActionDiff = True
                    End If
                End If
            End If
        End If
        Exit Sub
Affichage_Erreur:
        DevExpress.XtraEditors.XtraMessageBox.Show(Err.Description, Application.ProductName, MessageBoxButtons.OK)

    End Sub

    Sub MajAttenterapport(ByVal frm_Ecran As frmEcran)
        Dim rs As ADODB.Recordset
        Dim id As Integer
        Dim finOPE As Long
        If _AttenteRapport And (UCase(TypeObj) = "OPEMAIN" Or UCase(TypeObj) = "DINTER") Then

            Dim sql As String = ""
            Select Case UCase(TypeObj)
                Case "OPEMAIN"
                    sql = "SELECT IDRM, FinOpe FROM OperationMaintenance WHERE OperationMaintenance.IDOM=" & CleObj
                Case "DINTER"
                    sql = "SELECT IDRI FROM DEMINTERVENTION WHERE DEMINTERVENTION.CleDemIntervention=" & CleObj
            End Select
            If ClassParent.connect_ADO.ADODB_connection(1) And ClassParent.connect_ADO.ADODB_connection(2) Then
                rs = ClassParent.connect_ADO.ADODB_create_recordset(2, sql)
                id = CInt(rs.Fields(0).Value)
                If (UCase(TypeObj) = "OPEMAIN") Then
                    finOPE = rs.Fields(1).Value
                End If
                rs.Close()

                Select Case UCase(TypeObj)
                    Case "OPEMAIN"
                        sql = "SELECT Brouillon FROM RAPPORTSGM WHERE RAPPORTSGM.IDRapportGM=" & id
                    Case "DINTER"
                        sql = "SELECT Brouillon FROM RAPPORTSI WHERE RAPPORTSI.IDRapportI=" & id
                End Select

                rs = ClassParent.connect_ADO.ADODB_create_recordset(1, sql)
                If rs.EOF Then
                    _AttenteRapport = True
                Else
                    _AttenteRapport = CBool(rs.Fields(0).Value)
                End If
                rs.Close()

                rs = ClassParent.connect_ADO.ADODB_create_recordset(2, "SELECT * FROM MC WHERE cleobj=" & CleObj & " AND Objet='" & TypeObj & "'")
                rs.Fields("AttenteRapport").Value = _AttenteRapport
                Select Case UCase(TypeObj)
                    Case "OPEMAIN"
                        If Not _AttenteRapport And finOPE <> 0 Then
                            rs.Fields("estENcours").Value = False
                        End If
                    Case "DINTER"
                        rs.Fields("estENcours").Value = AttenteRapport
                End Select
                rs.Update()
                rs.Close()

                ClassParent.connect_ADO.ADODB_deconnection(1)
                ClassParent.connect_ADO.ADODB_deconnection(2)
            End If

        End If
    End Sub

    Sub Envoi_Mail()
        Dim rs As ADODB.Recordset
        Dim sql As String = ""
        Dim IdMail As Integer
        Dim AdresseMail As String
        Dim _NomMail_Exp As String      'Nom de l'expéditeur du mail
        Dim _AdresseMail_Exp As String  'Adresse mail de l'expéditeur
        Dim _smtp_mail As String        'SMTP du serveur mail
        Dim _mail_object As String
        Dim _mail_subject As String

        If ClassParent.connect_ADO.ADODB_connection(2) And ClassParent.connect_ADO.ADODB_connection(1) Then
            '***************************************
            '** RECUPERATION DU MAIL ET DES INFOS **
            '***************************************
            sql = "SELECT * FROM RAPPEL WHERE RAPPEL.IDRappel=" & CleObj
            rs = ClassParent.connect_ADO.ADODB_create_recordset(2, sql)
            If Not rs.EOF Then
                IdMail = CInt(rs.Fields("IDMail").Value)
                _mail_object = rs.Fields("Textevenement").Value
                DateRappel = rs.Fields("DateDebut").Value
                HeureDebut = rs.Fields("HeureDebut").Value
                DateFin = rs.Fields("DateFin").Value
                Famille = rs.Fields("Famille").Value
                JMois = rs.Fields("JMois").Value
                NbSemaine = rs.Fields("NbSemaine").Value
                Lundi = rs.Fields("Lundi").Value
                Mardi = rs.Fields("Mardi").Value
                Mercredi = rs.Fields("Mercredi").Value
                Jeudi = rs.Fields("Jeudi").Value
                Vendredi = rs.Fields("Vendredi").Value
                Samedi = rs.Fields("Samedi").Value
                Dimanche = rs.Fields("Dimanche").Value
            End If
            rs.Close()
            sql = "SELECT * FROM MAIL WHERE Id=" & IdMail
            rs = ClassParent.connect_ADO.ADODB_create_recordset(2, sql)
            If Not rs.EOF Then
                AdresseMail = rs.Fields("adresse").Value
            End If
            rs.Close()
            _AdresseMail_Exp = ClassParent.comunication_ini.GetCle("SYSTEME", "MAIL_EXP")
            _NomMail_Exp = ClassParent.comunication_ini.GetCle("SYSTEME", "NOM_EXP")
            _smtp_mail = ClassParent.comunication_ini.GetCle("SYSTEME", "SMTP")
            rs = ClassParent.connect_ADO.ADODB_create_recordset(1, "SELECT * FROM LANGUE WHERE IdObjet='RAPPEL' AND IDLangue='PV_TITRE'")
            _mail_subject = rs.Fields("Libelle").Value
            rs.Close()
            'ENVOI DU MAIL
            Dim email As New MailMessage()
            email.From = New MailAddress(_AdresseMail_Exp, _NomMail_Exp)
            email.To.Add(AdresseMail)
            email.Subject = _mail_subject
            email.Body = _mail_object
            Dim client As New SmtpClient(_smtp_mail)
            Try
                client.Send(email)
            Catch ex As Exception
                DevExpress.XtraEditors.XtraMessageBox.Show(ex.Message, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Exit Sub
            End Try
            'MODIFICATION DE MC
            ClassParent.connect_ADO.ADODB_delete(2, "UPDATE MC SET EstEncours=0 WHERE cleobj=" & CleObj & " AND Objet='" & TypeObj & "'")
            'CREATION DU RAPPEL SUIVANT SI PLANIFICATION
            'Dim interfrase As String
            'Dim inter As String
            Dim Phrase As String
            Dim numauto As Long
            Dim Maintenant As String

            If (IsBonneDate(DateRappel)) Then
                rs = ClassParent.connect_ADO.ADODB_create_recordset(2, "SELECT * FROM MC WHERE cleobj=" & CleObj & " AND Objet='" & TypeObj & "'")
                Phrase = rs.Fields("Affichage").Value
                rs.Close()

                Randomize()
                Maintenant = CStr(-1 * (Int((9999999 - 1000000 + 1) * Rnd() + 100000000)))
                ClassParent.connect_ADO.ADODB_delete(2, "INSERT INTO Rappel (textevenement) VALUES ('" & Maintenant & "');")
                rs = ClassParent.connect_ADO.ADODB_create_recordset(2, "SELECT * FROM Rappel WHERE textevenement='" & Maintenant & "';")
                numauto = rs.Fields("IDrappel").Value 'recuperation de la cle
                rs.Fields("DateDebut").Value = DateRappel
                rs.Fields("HeureDebut").Value = HeureDebut
                rs.Fields("IDMachine").Value = _IDMachineExe
                rs.Fields("Famille").Value = numauto
                rs.Fields("textevenement").Value = _mail_object
                rs.Fields("DateFin").Value = DateFin
                rs.Fields("JMois").Value = JMois
                rs.Fields("NbSemaine").Value = NbSemaine
                rs.Fields("Lundi").Value = Lundi
                rs.Fields("Mardi").Value = Mardi
                rs.Fields("Mercredi").Value = Mercredi
                rs.Fields("Jeudi").Value = Jeudi
                rs.Fields("Vendredi").Value = Vendredi
                rs.Fields("Samedi").Value = Samedi
                rs.Fields("Dimanche").Value = Dimanche
                rs.Fields("IDMail").Value = IdMail
                rs.Update()
                rs.Close()

                ' remplissage de la base MC
                ClassParent.connect_ADO.ADODB_delete(2, "INSERT INTO MC (cleobj,Objet) VALUES (" & numauto & ",'" & TypeObj & "');")
                rs = ClassParent.connect_ADO.ADODB_create_recordset(2, "SELECT * FROM MC WHERE cleobj=" & numauto & " AND Objet='" & TypeObj & "'")

                rs.Fields("DateHeuredebut").Value = (DateRappel) + (CDate((HeureDebut)).ToOADate)
                rs.Fields("affichage").Value = Phrase
                rs.Fields("Objet").Value = TypeObj
                rs.Fields("estSupp").Value = False
                rs.Fields("estBrouillon").Value = False
                rs.Fields("estEncours").Value = True
                rs.Fields("clestationnaire").Value = _idOperateur
                'rs.Fields("cleobj").Value = numauto
                rs.Update()
                rs.Close()
                rs = Nothing
                ClassParent.connect_ADO.ADODB_deconnection(2)
                ClassParent.connect_ADO.ADODB_deconnection(1)
            End If
        End If

    End Sub

    Private Function IsBonneDate(ByRef DateATraiter As Long) As Boolean
        Dim JourFin As Long
        Dim Atraiter As Boolean

        'On récupère le dernier jour du rappel
        JourFin = DateFin
        DateATraiter = DateRappel + 1

        'Traitement par mois
        If JMois > 0 Then
            While Microsoft.VisualBasic.DateAndTime.Day(Date.FromOADate(DateATraiter)) <> JMois
                DateATraiter = DateATraiter + 1
            End While
            If DateATraiter > JourFin Then
                IsBonneDate = False
            Else
                IsBonneDate = True
            End If
        Else
            If (NbSemaine > 1) Then
                DateATraiter = DateATraiter + (7 * (NbSemaine - 1)) '7 jours * nb semaine - 1 jour ajouté avant d'entrer dans la fonction
            End If

            Atraiter = False
            Do While Atraiter = False
                Select Case Weekday(Date.FromOADate(DateATraiter))
                    Case 1 'Dimanche
                        If Dimanche Then Atraiter = True
                    Case 2 'Lundi
                        If Lundi Then Atraiter = True
                    Case 3 'Mardi
                        If Mardi Then Atraiter = True
                    Case 4 'Mercredi
                        If Mercredi Then Atraiter = True
                    Case 5 'Jeudi
                        If Jeudi Then Atraiter = True
                    Case 6 'Vendredi
                        If Vendredi Then Atraiter = True
                    Case 7 'Samedi
                        If Samedi Then Atraiter = True
                End Select
                If DateATraiter > JourFin Then Exit Do
                If Atraiter = False Then DateATraiter = DateATraiter + 1
            Loop

            If DateATraiter > JourFin Then
                IsBonneDate = False
            Else
                IsBonneDate = True
            End If
        End If
    End Function

    Public Property Commentaire() As String
        Get
            Return _Commentaire
        End Get
        Set(ByVal value As String)
            _Commentaire = value
        End Set
    End Property

    Public Property Bat() As String
        Get
            Return _Bat
        End Get
        Set(ByVal value As String)
            _Bat = value
        End Set
    End Property

End Class



