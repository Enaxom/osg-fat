Imports System.Windows.Forms
Imports System.Collections.Generic
Imports System.IO

Public Class Init_MC
    Const AccesSaisie = 0               'Acces en saisie du rapport
    Const AccesModif = 1                'Acces en modification du brouillon
    Const AccesBrouillon = 2            'Acces en modification du brouillon
    Const AccesVisu = 3                 'Acces en visualisation du rapport

    Friend FichierIni As String
    Private connection_string As String = New String("")
    Friend comunication_ini As comXML
    Private comunication_OSGDATA As comXML
    Private SQLIni As New System.Xml.XmlDocument 'nom du fichier SQL.XML

    Private _ID_MACHINE As Integer
    Friend _ID_USER As Integer

    Friend Feuille As Object

    Private mvarStationnaire As Long 'copie locale
    Private mvarGroupe As Long 'copie locale
    Friend FrmFenetre As frmEcran
    Friend ListeEvenement As List(Of grid_row)
    Friend LstUser As New List(Of Data_Set)

    Friend IsTousRappel As String

    Friend Stationnaire As Integer
    Friend Groupe As Integer

    Friend IDRappel As String = ""

    Friend connect_ADO As connect_ADODB
    Friend ListeMessages As New clsMessages

    Friend isPWD As Boolean
    Friend isBioVein As Boolean

    Friend NBMaxHisto As Integer

    Friend IsBioVeinAuto As Boolean

    Const TIMEOUTLECTUREDEFAUT = 0.0001736 '15 secondes

    Friend Timeout As String
    Friend TimeoutLecture As String
    Friend NomMachine As String

    Friend Warn As Alerte
    Friend Logo As Object
    Friend OSG_Bridge As Boolean

    Friend nom_skin As String

    Friend sound_alarme As System.Media.SoundPlayer
    Friend sound_rappel As System.Media.SoundPlayer
    Friend Smtp_Ok As Integer = 0

    'TRAKA
    Friend IsTraka As Boolean
    Friend IsTrakaRappel As Boolean
    'Heure & Controle
    Friend IsHetC As Boolean
    Friend IsHetCRappel As Boolean

    Friend NatOpe As String 'Nature opération pour rondier MWS
    Friend IsRondier As Boolean
    Friend PathRondierExe As String
    Friend PathRondier As String
    Friend ModeChrono As Integer = 0
    Friend pathAndroid As String
    'SSLIA
    Friend IsSSLIA As Boolean = False
    Friend idLVP As Integer = 0
    Friend idDoublet As Integer = 0
    Friend idVENT As Integer = 0
    'CEA - DIT
    Friend IsRIT As Boolean = False

    Public Function Init(ByVal CheminIni As String, ByVal ID_Machine As Integer, ByVal ID_USER As Integer, Optional ByVal _ModeChrono As Integer = 0) As Boolean
        Dim res As Boolean = True

        'LevelUSER = NivUser
        _ID_MACHINE = ID_Machine
        _ID_USER = ID_USER
        FichierIni = CheminIni

        ModeChrono = _ModeChrono

        comunication_ini = New comXML(CheminIni)

        connect_ADO = New connect_ADODB

        connect_ADO.Init(CheminIni)

        Lecture_Message()

        If Not System.IO.File.Exists(Application.StartupPath & "\ico\alarme2.wav") Or Not System.IO.File.Exists(Application.StartupPath & "\ico\rappel.WAV") Then
            DevExpress.XtraEditors.XtraMessageBox.Show(Feuille.ListeMessages("MESS_ERRNOSOUND").Libelle, Application.ProductName, MessageBoxButtons.OK)
            Return False
        End If

        sound_alarme = New System.Media.SoundPlayer(Application.StartupPath & "\ico\alarme2.wav")
        sound_rappel = New System.Media.SoundPlayer(Application.StartupPath & "\ico\rappel.WAV")

        If connect_ADO.ADODB_connection(1) Then
            Dim rs As ADODB.Recordset = connect_ADO.ADODB_create_recordset(1, "SELECT * FROM LANGUE WHERE IdObjet='MC' AND IdLangue='NBMaxHisto'")
            NBMaxHisto = CInt(rs.Fields("Libelle").Value)
            rs.Close()

            rs = connect_ADO.ADODB_create_recordset(1, "SELECT * FROM LANGUE WHERE IdObjet='GENERIQUE' AND IdLangue='TYPEDATE'")
            TypeDate = UCase(rs.Fields("Libelle").Value)
            rs.Close()

            rs = connect_ADO.ADODB_create_recordset(1, "SELECT * FROM LANGUE WHERE IdObjet='GENERIQUE' AND IdLangue='TYPEHEURE'")
            TypeHeure = UCase(rs.Fields("Libelle").Value)
            rs.Close()

            rs = connect_ADO.ADODB_create_recordset(1, "SELECT * FROM LANGUE WHERE IdObjet='GENERIQUE' AND IdLangue='SSLIA_OK'")
            If Not rs.EOF Then
                IsSSLIA = (UCase(rs.Fields("Type").Value) = 1)
            End If
            rs.Close()

            rs = connect_ADO.ADODB_create_recordset(1, "SELECT * FROM LANGUE WHERE IdObjet='GENERIQUE' AND IdLangue='RIT_OK'")
            If Not rs.EOF Then
                IsRIT = (UCase(rs.Fields("Type").Value) = 1)
            End If
            rs.Close()
        End If

        connect_ADO.ADODB_deconnection(1)

        Return True


    End Function

    Private Sub Lecture_Message()

        'Fonction de lecture des libellés
        If connect_ADO.ADODB_connection(1) Then
            Dim rs As ADODB.Recordset
            'Mise à jour des libellés
            rs = connect_ADO.ADODB_create_recordset(1, "SELECT * FROM LANGUE WHERE IdObjet='GENERIQUE' AND IdLangue Like 'MESS_%';")
            While Not rs.EOF
                Dim Newmsg As New clsMessage
                Newmsg.Clear()
                Newmsg.Cle = rs.Fields("IdLangue").Value
                Newmsg.Libelle = rs.Fields("Libelle").Value & ""
                ListeMessages.Add(rs.Fields("IdLangue").Value, Newmsg)
                rs.MoveNext()
            End While
            rs.Close()
        End If
        connect_ADO.ADODB_deconnection(1)
    End Sub

    Public Function execute() As Boolean

        Dim rs As ADODB.Recordset
        Dim SQL As String

        execute = False


        OSG_Bridge = comunication_ini.GetCle("SYSTEME", "OSGBridge") <> "OFF"
        'TRAKA
        IsTraka = comunication_ini.GetCle("SYSTEME", "ISTRAKA") <> "OFF"
        IsTrakaRappel = comunication_ini.GetCle("SYSTEME", "TRAKA_RAPPEL") <> "OFF"
        'HetC
        IsHetC = (comunication_ini.GetCle("SYSTEME", "ISHETC") <> "OFF" And comunication_ini.GetCle("SYSTEME", "ISHETC") <> "")
        IsHetCRappel = comunication_ini.GetCle("SYSTEME", "HetC_RAPPEL") <> "OFF"

        IsRondier = comunication_ini.GetCle("SYSTEME", "PathRondier") <> ""
        IsTousRappel = comunication_ini.GetCle("SYSTEME", "IsTousRappel") = "1"

        NatOpe = comunication_ini.GetCle("SYSTEME", "NATOPE")

        TimeoutLecture = comunication_ini.GetCle("SYSTEME", "TimeoutLecture")
        If Not IsNumeric(TimeoutLecture) Then
            TimeoutLecture = TIMEOUTLECTUREDEFAUT
        Else
            TimeoutLecture = TimeoutLecture / (3600.0# * 24.0#)
        End If

        Timeout = comunication_ini.GetCle("SYSTEME", "Timeout")
        If Not IsNumeric(Timeout) Then
            Timeout = TIMEOUTLECTUREDEFAUT
        Else
            Timeout = Timeout / (3600.0# * 24.0#)
        End If

        PathRondierExe = comunication_ini.GetCle("SYSTEME", "XRondier")
        PathRondier = comunication_ini.GetCle("SYSTEME", "PathRondier")
        pathAndroid = comunication_ini.GetCle("SYSTEME", "PATH_ANDROID")

        FrmFenetre = New frmEcran(FichierIni, _ID_MACHINE, _ID_USER, Me)
        '*******************************************************************
        '** TEST SI MOT DE PASSE
        '*******************************************************************
        FrmFenetre.btnRondier.Visible = PathRondierExe <> ""

        If Not IsTraka And Not IsHetC Then
            FrmFenetre.btnTraka.Visible = False
        End If

        If OSG_Bridge Then
            Warn = New Alerte
            Warn.ClassParent = Me
            Warn.Generer()
        Else
            FrmFenetre.CMD_Alerte.Visible = False
        End If

        If Not connect_ADO.ADODB_connection(1) Then
            DevExpress.XtraEditors.XtraMessageBox.Show(ListeMessages("MESS_MC_1").Libelle, Application.ProductName, MessageBoxButtons.OK)
            Exit Function
        End If
        'GESTION DE MOT DE PASSE
        SQL = "SELECT Valeur FROM ADMIN WHERE IdObjet='PWDMC'"
        rs = connect_ADO.ADODB_create_recordset(1, SQL)
        If Not rs.EOF Then
            isPWD = rs.Fields(0).Value
        End If
        rs.Close()
        rs = Nothing
        connect_ADO.ADODB_deconnection(1)
        '*******************************************************************
        '** TEST BIOVEIN
        '*******************************************************************

        isBioVein = comunication_ini.GetCle("SYSTEME", "BIOVEIN") <> "OFF"

        '*******************************************************************
        'récupération du nom du stationnaire
        Dim FrL As frmLogin
        FrL = New frmLogin(FichierIni, _ID_MACHINE, _ID_USER, Me)
        FrL.Init()
        FrL.ShowDialog()
        FrL = Nothing
        connect_ADO.ADODB_connection(1)
        If Me.Stationnaire <> 0 Then
            SQL = "SELECT UTILISATEURS.NomUtilisateur, UTILISATEURS.PrenomUtilisateur, GROUPES.NomGroupe, GROUPES.IDGroupe FROM UTILISATEURS INNER JOIN (GROUPES INNER JOIN A_GROUPES_UTILISATEURS ON GROUPES.IDGroupe = A_GROUPES_UTILISATEURS.IDGroupe) ON UTILISATEURS.IDUtilisateur = A_GROUPES_UTILISATEURS.IDUtilisateur Where UTILISATEURS.IDutilisateur=" & Stationnaire & " AND GROUPES.IDGroupe=" & Groupe
            rs = connect_ADO.ADODB_create_recordset(1, SQL)
            If Not rs.EOF Then
                FrmFenetre.TxtNom.Text = rs.Fields("NomUtilisateur").Value
                FrmFenetre.TxtPrenom.Text = rs.Fields("PrenomUtilisateur").Value
                FrmFenetre.TxtGroupe.Text = rs.Fields("NomGroupe").Value
                Me.Groupe = rs.Fields("IDGroupe").Value
                FrmFenetre.TxtPriseService.Text = Date.Now.ToShortDateString & " " & Date.Now.ToShortTimeString
            Else
                rs.Close()
                rs = Nothing
                FrmFenetre.Close()
                FrmFenetre = Nothing
                DevExpress.XtraEditors.XtraMessageBox.Show(ListeMessages("MESS_MSGNOGROUP").Libelle, Application.ProductName, MessageBoxButtons.OK)
                Exit Function
            End If
            rs.Close()
            rs = Nothing
            Connection_Stationnaire(True)
            For Each item As Data_Set In FrmFenetre.cboFiltre.Properties.Items
                If item.id = 0 Then
                    FrmFenetre.cboFiltre.SelectedItem = item
                End If
            Next
            '*******************
            '** GESTION SSLIA **
            '*******************
            If IsSSLIA Then
                'Mise à jour des valeurs
                Call SSLIA_MAJ()
            End If
            '*******************
            connect_ADO.ADODB_deconnection(1)

            FrmFenetre.ShowDialog()
        Else
            FrmFenetre.Close()
            FrmFenetre = Nothing
        End If
        connect_ADO.ADODB_deconnection(1)

    End Function

    Public Property IDMachine As Integer
        Get
            Return _ID_MACHINE
        End Get
        Set(value As Integer)
            _ID_MACHINE = value
        End Set
    End Property

    Public Sub Connection_Stationnaire(ByVal TypeConect As Boolean)
        'TypeConnect = True : Connection
        'TypeConnect = False : Déconnection
        Dim rs As ADODB.Recordset
        Dim PhraseConnect As String
        Dim DateConnect As Double

        If Not connect_ADO.ADODB_connection(1) And Not connect_ADO.ADODB_connection(2) Then
            DevExpress.XtraEditors.XtraMessageBox.Show(ListeMessages("MESS_MC_1").Libelle, Application.ProductName, MessageBoxButtons.OK)
            Exit Sub
        End If

        If TypeConect Then
            rs = connect_ADO.ADODB_create_recordset(1, "SELECT * FROM LANGUE WHERE IdObjet='MC' AND IdLangue='PhraseConnect'")
            PhraseConnect = rs.Fields("Libelle").Value
            rs.Close()
        Else
            rs = connect_ADO.ADODB_create_recordset(1, "SELECT * FROM LANGUE WHERE IdObjet='MC' AND IdLangue='PhraseNoConnect'")
            PhraseConnect = rs.Fields("Libelle").Value
            rs.Close()
        End If
        PhraseConnect = Replace(PhraseConnect, "#1#", (FrmFenetre.TxtNom.Text & " " & FrmFenetre.TxtPrenom.Text))
        DateConnect = Date.Now.ToOADate
        PhraseConnect = Replace(PhraseConnect, "#2#", Date.FromOADate(DateConnect).ToShortDateString & " " & Date.FromOADate(DateConnect).ToShortTimeString)

        Dim STR_SQL As String
        STR_SQL = "INSERT INTO MC (DateHeureDebut,Affichage,Objet,EstSupp,CleObj,EstEnCours,CleStationnaire,EstBrouillon,AttenteRapport) "
        STR_SQL = STR_SQL & "VALUES (" & Replace(DateConnect, ",", ".")
        STR_SQL = STR_SQL & ",'" & Replace(PhraseConnect, "'", "''") & "'"
        STR_SQL = STR_SQL & ",'" & "RELEVE" & "'"
        STR_SQL = STR_SQL & ",0,0,0," & Me.Stationnaire & ",0,0)"

        connect_ADO.ADODB_delete(2, STR_SQL)

        rs = Nothing
        connect_ADO.ADODB_deconnection(1)
        connect_ADO.ADODB_deconnection(2)

    End Sub

    Private Sub BioVein_Automatique()
        Throw New NotImplementedException
    End Sub

    Public Sub AffichageHistorique()
        Dim Mafenetre As frmAffHisto
        Dim CHeminIni As String

        CHeminIni = FichierIni
        Mafenetre = New frmAffHisto(FichierIni, _ID_MACHINE, _ID_USER, Me)

        Mafenetre.ShowDialog()
        Mafenetre = Nothing
    End Sub

    Public Sub Construct_LstUser()

        Dim data As ADODB.Recordset
        Dim SQL As String
        Dim obj_User As New Data_Set

        Me.LstUser.Clear()

        If Not connect_ADO.ADODB_connection(1) Then
            DevExpress.XtraEditors.XtraMessageBox.Show(ListeMessages("MESS_MC_1").Libelle, Application.ProductName, MessageBoxButtons.OK)
            Exit Sub
        End If

        SQL = "SELECT NomUtilisateur,PrenomUtilisateur,IDutilisateur FROM UTILISATEURS"
        data = connect_ADO.ADODB_create_recordset(1, SQL)
        While Not data.EOF
            obj_User.nom = data.Fields("NomUtilisateur").Value
            obj_User.champ = data.Fields("PrenomUtilisateur").Value
            obj_User.id = data.Fields("IDutilisateur").Value
            Me.LstUser.Add(obj_User)
            data.movenext()
        End While
        data.Close()
        connect_ADO.ADODB_deconnection(1)
        data = Nothing
    End Sub

    Public Sub LibereJetonLecture()

        If connect_ADO.ADODB_connection(2) Then
            connect_ADO.ADODB_delete(2, "DELETE FROM JETONS WHERE NomClasse='LECTURE'")
        End If

    End Sub

    Friend Function MetEnplaceJetonLecture(Optional ByVal ModeSilencieux As Boolean = True) As Boolean

        MetEnplaceJetonLecture = True

        Dim Ladate As String
        Dim ConvertDate As Double
        Dim NomMachineQuiOuvre As String

        Try
            'SI GESTION PAR BDD ALORS
            Dim data As ADODB.Recordset

            If connect_ADO.ADODB_connection(2) Then
                data = connect_ADO.ADODB_create_recordset(2, "SELECT * FROM JETONS WHERE NomClasse='LECTURE'")
                If Not data.EOF Then
                    Ladate = data.Fields("DateNow").Value
                    NomMachineQuiOuvre = data.Fields("NomMachine").Value
                    ConvertDate = CDbl(Ladate)
                    If (ConvertDate + TimeoutLecture) > Date.Now.ToOADate Then
                        'Blocage
                        If Not ModeSilencieux Then 'Doit on afficher un message ?
                            DevExpress.XtraEditors.XtraMessageBox.Show(ListeMessages("MESS_MC_9").Libelle, Application.ProductName, MessageBoxButtons.OK)
                        End If
                        MetEnplaceJetonLecture = False
                    Else
                        'Réalouement
                        data.Fields("NomClasse").Value = "LECTURE"
                        data.Fields("DateNow").Value = Date.Now.ToOADate
                        data.Fields("NomMachine").Value = IDMachine
                        data.Fields("TimeOut").Value = TimeoutLecture
                        data.Update()
                        data.Close()
                    End If
                Else
                    data.AddNew()
                    data.Fields("NomClasse").Value = "LECTURE"
                    data.Fields("DateNow").Value = Date.Now.ToOADate
                    data.Fields("NomMachine").Value = IDMachine
                    data.Fields("TimeOut").Value = TimeoutLecture
                    data.Update()
                    data.Close()
                End If
                data = Nothing
            End If
            connect_ADO.ADODB_deconnection(2)
            Exit Function
        Catch ex As Exception
            MsgBox(Err.Description)
            connect_ADO.ADODB_deconnection(2)
            MetEnplaceJetonLecture = False
        End Try

    End Function

    Public Function MetEnplaceJetonEXEC(ByVal Nomfichier As String) As Boolean

        MetEnplaceJetonEXEC = True

        Dim Ladate As String
        Dim ConvertDate As Double
        Dim NomMachineQuiOuvre As String

        Try

            Dim data As ADODB.Recordset
            Dim data2 As ADODB.Recordset

            If connect_ADO.ADODB_connection(2) Then
                'SUPPRESSION DE ".INI"
                data = connect_ADO.ADODB_create_recordset(2, "SELECT * FROM JETONS WHERE NomClasse='" & Nomfichier & "'")
                If Not data.EOF Then
                    Ladate = data.Fields("DateNow").Value
                    NomMachineQuiOuvre = data.Fields("NomMachine").Value

                    data2 = connect_ADO.ADODB_create_recordset(2, "SELECT NomMachine FROM Machine WHERE IDMachine=" & NomMachineQuiOuvre)
                    NomMachineQuiOuvre = data2.Fields(0).Value
                    data2.Close()

                    ConvertDate = CDbl(Ladate)
                    If (ConvertDate + Timeout) > Date.Now.ToOADate Then
                        'Blocage
                        DevExpress.XtraEditors.XtraMessageBox.Show(Replace("Message : utilisé par #1#", "#1#", NomMachineQuiOuvre), Application.ProductName, MessageBoxButtons.OK)
                        MetEnplaceJetonEXEC = False
                    Else
                        'Réalouement
                        data.Fields("NomClasse").Value = Nomfichier
                        data.Fields("DateNow").Value = Date.Now.ToOADate
                        data.Fields("NomMachine").Value = IDMachine
                        data.Fields("TimeOut").Value = TimeoutLecture
                        data.Update()
                        data.Close()
                    End If
                Else
                    data.AddNew()
                    data.Fields("NomClasse").Value = Nomfichier
                    data.Fields("DateNow").Value = Date.Now.ToOADate
                    data.Fields("NomMachine").Value = IDMachine
                    data.Fields("TimeOut").Value = TimeoutLecture
                    data.Update()
                    data.Close()
                End If
                data = Nothing
                connect_ADO.ADODB_deconnection(2)
            End If

            Exit Function
        Catch ex As Exception
            connect_ADO.ADODB_deconnection(2)
            MsgBox(Err.Description)
            MetEnplaceJetonEXEC = False
        End Try
    End Function

    Friend Sub LibereJetonEXE(ByVal Nomfichier As grid_row)

        If connect_ADO.ADODB_connection(2) Then
            connect_ADO.ADODB_delete(2, "DELETE FROM JETONS WHERE NomClasse='" & Nomfichier.TypeObj & "_" & Nomfichier.CleObj & "'")
            connect_ADO.ADODB_deconnection(2)
        End If

    End Sub

    Friend Sub LancerTraka()
        If IsTraka Then
            Dim traka As New OSGTraka32.Traka32()

            If traka.Init(FichierIni) Then
                traka.Execute()
            Else
                DevExpress.XtraEditors.XtraMessageBox.Show(ListeMessages("MESS_ERRINIT").Libelle, Application.ProductName, MessageBoxButtons.OK)
            End If
            traka = Nothing
        End If
    End Sub
    Friend Sub LancerHetC()
        If IsHetC Then
            Dim HetC As New HetC.clsHetC()

            If HetC.Init(FichierIni) Then
                HetC.Execute()
            Else
                DevExpress.XtraEditors.XtraMessageBox.Show(ListeMessages("MESS_ERRINIT").Libelle, Application.ProductName, MessageBoxButtons.OK)
            End If
            HetC = Nothing
        End If
    End Sub
    Friend Sub LancerRappelsTraka()
        Dim traka As New Rappel_traka.RappelTraka()
        If traka.Init(Me.FichierIni, Me.Stationnaire) Then
            traka.Execute()
        Else
            DevExpress.XtraEditors.XtraMessageBox.Show(ListeMessages("MESS_ERRINIT").Libelle, Application.ProductName, MessageBoxButtons.OK)
        End If
        traka = Nothing
    End Sub
    Friend Sub LancerRappelsHetC()
        Dim traka As New Rappel_HetC.RappelHetC()
        If traka.Init(Me.FichierIni, Me.Stationnaire) Then
            traka.Execute()
        Else
            DevExpress.XtraEditors.XtraMessageBox.Show(ListeMessages("MESS_ERRINIT").Libelle, Application.ProductName, MessageBoxButtons.OK)
        End If
        traka = Nothing
    End Sub

    Public Function ImportRondier() As Boolean
        Dim IsTraite As Boolean = False
        Dim ValeurEtat As Boolean = False
        ImportRondier = False
        If Not System.IO.Directory.Exists(PathRondier) Then
            DevExpress.XtraEditors.XtraMessageBox.Show(ListeMessages("MESS_PBRONDIER").Libelle, Application.ProductName, MessageBoxButtons.OK)
            IsRondier = False
            Exit Function
        End If
        Dim data As ADODB.Recordset
        If connect_ADO.ADODB_connection(1) Then
            'TEST MWS
            data = connect_ADO.ADODB_create_recordset(1, "SELECT * FROM LANGUE WHERE IdObjet='MC' AND IdLangue='MWS'")
            If Not data.EOF Then
                ValeurEtat = (data.Fields("Type").Value = "1")
                data.Close()
                If ValeurEtat Then
                    ImportRondier = ImportRondierMWS()
                    IsTraite = True
                End If
            Else
                data.Close()
            End If
            'TEST ASCOM
            If Not IsTraite Then
                data = connect_ADO.ADODB_create_recordset(1, "SELECT * FROM LANGUE WHERE IdObjet='MC' AND IdLangue='ASCOM'")
                If Not data.EOF Then
                    ValeurEtat = (data.Fields("Type").Value = "1")
                    data.Close()
                    If ValeurEtat Then
                        ImportRondier = ImportRondierAscom()
                        IsTraite = True
                    End If
                Else
                    data.Close()
                End If
            End If
            'TEST MEMOSTIC
            If Not IsTraite Then
                ImportRondier = ImportRondierMemo()
            End If
        End If
        connect_ADO.ADODB_deconnection(1)
    End Function

    Public Function ImportRondierAscom() As Boolean
        ' 0 début de ronde
        ' 1 fin de ronde
        ' 3 dépassement temps
        ' 4 saut de point
        ' 5 saut de point non autorisé
        ' 6 incidents
        Try

            Dim data As ADODB.Recordset
            Dim NomRep As DirectoryInfo
            Dim ListFic() As FileInfo
            Dim TempStr As String
            Dim IDRM As Long
            Dim SytemeCodePage As New System.Text.UTF7Encoding
            Dim ChaineLabel As String
            Dim PosDieze As Integer
            Dim chainetmp As String
            Dim strMonth As String
            Dim strYear As String
            Dim strDay As String
            Dim varDate As Integer

            NomRep = New DirectoryInfo(PathRondier)

            ListFic = NomRep.GetFiles()

            For Each NomFic As FileInfo In ListFic
                TempStr = NomFic.Name
                If UCase(Right(TempStr, 3)) = "CSV" And (Mid(TempStr, 7, 1) = "-") And (Mid(TempStr, 14, 1) = "-") Then
                    If InStr(TempStr, "#start") > 0 Then 'Création Activité préventive
                        Dim textstr As New StreamReader(PathRondier & "\" & TempStr, SytemeCodePage)
                        Dim pos As Integer
                        Dim posold As Integer

                        Dim varheure As String
                        Dim varEvent As String
                        Dim VarIdRonde As String
                        Dim varIdUser As String

                        TempStr = textstr.ReadLine()
                        'non traitement de la ligne des titres
                        TempStr = textstr.ReadLine()
                        TempStr = Replace(TempStr, """", "")
                        pos = 1
                        pos = InStr(pos, TempStr, ";")
                        '** RECUPERATION CHAMP DATE **
                        varheure = Replace(Mid(TempStr, 1, pos - 1), "-", ":")
                        strDay = Left(varheure, 2)
                        strMonth = Mid(varheure, 4, 2)
                        strYear = "20" & Mid(varheure, 7, 2)
                        varDate = CInt(CDate(strDay & "/" & strMonth & "/" & strYear).ToOADate)
                        varheure = Mid(varheure, 10, 5)
                        posold = pos
                        pos = InStr(pos + 1, TempStr, ";")
                        '** RECUPERATION CHAMP CODE **
                        'Code= Mid(TempStr, posold + 1, pos - posold - 1)
                        posold = pos
                        pos = InStr(pos + 1, TempStr, ";")
                        '** RECUPERATION CHAMP EVENEMENT **
                        varEvent = Mid(TempStr, posold + 1, pos - posold - 1)
                        PosDieze = InStr(varEvent, "#")
                        VarIdRonde = Mid(varEvent, PosDieze + 1, InStr(PosDieze, varEvent, ")") - PosDieze - 1)
                        posold = pos
                        pos = InStr(pos + 1, TempStr, ";")
                        '** RECUPERATION CHAMP POINT DE RONDE **
                        'Point de ronde = ChaineLabel & Mid(TempStr, posold + 1, pos - posold - 1)
                        posold = pos
                        'pos = InStr(pos + 1, TempStr, ";")
                        '** RECUPERATION CHAMP DESCRIPTION **
                        ChaineLabel = Right(TempStr, Len(TempStr) - pos)
                        PosDieze = InStr(ChaineLabel, "#")
                        varIdUser = Mid(ChaineLabel, PosDieze + 1, InStr(PosDieze, ChaineLabel, ")") - PosDieze - 1)
                        textstr.Close()
                        NomFic.Delete()
                        textstr = Nothing

                        If Verification_OpeMain(VarIdRonde, varIdUser) Then
                            'Enregistrement de l'opération préventive
                            Enregistrement_ActPrev(varDate, varheure, varEvent, VarIdRonde, varIdUser)
                        End If
                    Else
                        If connect_ADO.ADODB_connection(1) Then
                            data = connect_ADO.ADODB_create_recordset(1, "SELECT IDRM FROM RONDIER_LECTURE ORDER BY IDRM DESC")
                            If Not data.EOF Then
                                IDRM = data.Fields(0).Value + 1
                            Else
                                IDRM = 1
                            End If
                            data.Close()

                            Dim textstr As New StreamReader(PathRondier & "\" & TempStr, SytemeCodePage)
                            Dim pos As Integer
                            Dim posold As Integer
                            Dim CodeInc As String
                            Dim varheure As String
                            Dim chaine As String
                            Dim ImageRonde As String
                            Dim IdPhoto As Integer
                            TempStr = textstr.ReadLine()
                            'non traitement de la ligne des titres
                            Do Until textstr.Peek = -1
                                ImageRonde = ""
                                TempStr = textstr.ReadLine()
                                TempStr = Replace(TempStr, """", "")
                                pos = 1
                                pos = InStr(pos, TempStr, ";") 'Position apres champ date
                                '** RECUPERATION CHAMP DATE **
                                varheure = Replace(Mid(TempStr, 1, pos - 1), "-", ":")
                                strDay = Left(varheure, 2)
                                strMonth = Mid(varheure, 4, 2)
                                strYear = "20" & Mid(varheure, 7, 2)
                                varheure = (strDay & "/" & strMonth & "/" & strYear) & " " & Mid(varheure, 10, 5)
                                posold = pos
                                pos = InStr(pos + 1, TempStr, ";") 'Position aprés Code
                                CodeInc = Mid(TempStr, posold + 1, pos - posold - 1)
                                posold = pos
                                pos = InStr(pos + 1, TempStr, ";") 'Position aprés Libellé évènement
                                ChaineLabel = Mid(TempStr, posold + 1, pos - posold - 1) 'Right(TempStr, TempStr.Length - pos) 
                                PosDieze = InStr(ChaineLabel, "#")
                                If PosDieze > 0 Then
                                    ChaineLabel = Left(ChaineLabel, PosDieze - 2)
                                End If
                                posold = pos
                                pos = InStr(pos + 1, TempStr, ";") 'Position aprés Point de ronde
                                If Mid(TempStr, posold + 1, pos - posold - 1).Trim <> "" Then
                                    ChaineLabel = ChaineLabel & " - " & Mid(TempStr, posold + 1, pos - posold - 1)
                                End If
                                If InStr(pos + 1, TempStr, ";") > 0 Then 'TEST si présence fichier image
                                    posold = pos
                                    pos = InStr(pos + 1, TempStr, ";")
                                    ChaineLabel = ChaineLabel & " - " & Mid(TempStr, posold + 1, pos - posold - 1)
                                    ImageRonde = Right(TempStr, TempStr.Length - pos).Trim
                                    'TRAITEMENT DE L'IMAGE
                                    IdPhoto = ImportImage(ImageRonde)
                                Else
                                    IdPhoto = 0
                                    If Right(TempStr, TempStr.Length - pos).Trim <> "" Then
                                        ChaineLabel = ChaineLabel & " - " & Right(TempStr, TempStr.Length - pos)
                                    End If
                                End If

                                PosDieze = InStr(ChaineLabel, "#")
                                If PosDieze > 0 Then
                                    If CodeInc = "6" Then 'ANOMALIE
                                        chainetmp = Mid(ChaineLabel, PosDieze + 1, InStr(PosDieze, ChaineLabel, ")") - PosDieze - 1)
                                    Else
                                        chainetmp = "-"
                                    End If
                                    ChaineLabel = Left(ChaineLabel, PosDieze - 2)
                                Else
                                    chainetmp = "-"
                                End If
                                chaine = "INSERT INTO RONDIER_LECTURE (NomMemo, RefMemo, RefType, RefInfo, RefLabel, IdPuce, DateHeure, IDRM) "
                                chaine = chaine & " VALUES ('-','-','" & CodeInc & "', '" & IdPhoto & "', '" & Replace(ChaineLabel, "'", "''") & "','" & chainetmp
                                chaine = chaine & "','" & varheure & "'," & IDRM & ");"
                                connect_ADO.ADODB_delete(1, chaine)
                            Loop

                            textstr.Close()
                            data = Nothing
                            NomFic.Delete()
                            textstr.Close()
                            textstr = Nothing
                        End If
                        connect_ADO.ADODB_deconnection(1)
                    End If
                End If
            Next
            NomRep = Nothing
            ListFic = Nothing
            Return True
            Exit Function

        Catch ex As Exception
            DevExpress.XtraEditors.XtraMessageBox.Show(ex.Message, Application.ProductName, MessageBoxButtons.OK)
            connect_ADO.ADODB_deconnection(1)
            Return False
        End Try

    End Function

    Private Function ImportImage(NomFicImage As String) As Integer
        'FONCTION DE STOCKAGE DE LA PHOTO ET RETOUR DE LA CLE DANS LA TABLE
        Dim masterDir As New DirectoryInfo(PathRondier)
        Dim files As FileInfo() = masterDir.GetFiles()
        Dim Maintenant As String
        Dim IdPhoto As Integer

        For Each f As FileInfo In files
            If f.Name.Contains(NomFicImage) Then
                Dim Photo As Byte() = GetPhoto(PathRondier & "\" & f.Name)
                If connect_ADO.ADODB_connection(1) Then
                    Randomize()
                    Maintenant = CStr(-1 * (Int((999999999 - 100000000 + 1) * Rnd() + 100000000)))
                    connect_ADO.ADODB_delete(1, "INSERT INTO RONDIER_PHOTOS (RefLabel) VALUES ('" & Maintenant & "')")
                    Dim rs As ADODB.Recordset
                    rs = connect_ADO.ADODB_create_recordset(1, "SELECT * FROM RONDIER_PHOTOS WHERE RefLabel = '" & Maintenant & "'")
                    If Not IsDBNull(rs) Then
                        IdPhoto = rs.Fields("IDPhoto").Value
                        rs.Fields("FICHIER").Value = Photo
                        rs.Fields("RefLabel").Value = NomFicImage
                        rs.Update()
                        File.Delete(PathRondier & "\" & f.Name)
                    End If
                    rs.Close()
                End If
                Exit For
            End If
        Next
        Return IdPhoto
    End Function

    Private Function GetPhoto(filePath As String) As Byte()
        Dim stream As FileStream = New FileStream(filePath, FileMode.Open, FileAccess.Read)
        Dim reader As BinaryReader = New BinaryReader(stream)

        Dim photo() As Byte = reader.ReadBytes(stream.Length)

        reader.Close()
        stream.Close()

        Return photo
    End Function

    Private Function ImportRondierMWS() As Boolean
        ' 0 début de ronde
        ' 1 fin de ronde
        ' 3 dépassement temps
        ' 4 saut de point
        ' 5 saut de point non autorisé
        ' 6 incidents
        Try

            Dim data As ADODB.Recordset
            Dim NomRep As DirectoryInfo
            Dim ListFic() As FileInfo
            Dim TempStr As String
            Dim IDRM As Long
            Dim SytemeCodePage As New System.Text.UTF7Encoding
            Dim ChaineLabel As String
            Dim PosDieze As Integer
            Dim chainetmp As String

            NomRep = New DirectoryInfo(PathRondier)

            ListFic = NomRep.GetFiles()

            For Each NomFic As FileInfo In ListFic
                TempStr = NomFic.Name
                If UCase(Right(TempStr, 3)) = "CSV" And (Mid(TempStr, 7, 1) = "-") And (Mid(TempStr, 14, 1) = "-") Then
                    If InStr(TempStr, "#start") > 0 Then 'Création Activité préventive
                        Dim textstr As New StreamReader(PathRondier & "\" & TempStr, SytemeCodePage)
                        Dim pos As Integer
                        Dim posold As Integer
                        Dim varDate As Integer
                        Dim varheure As String
                        Dim varEvent As String
                        Dim strMonth As String
                        Dim strYear As String
                        Dim strDay As String
                        Dim VarIdRonde As String
                        Dim varIdUser As String

                        TempStr = textstr.ReadLine()
                        'non traitement de la ligne des titres
                        TempStr = textstr.ReadLine()
                        TempStr = Replace(TempStr, """", "")
                        pos = 1
                        pos = InStr(pos, TempStr, ";")
                        '** RECUPERATION CHAMP DATE **
                        varheure = Replace(Mid(TempStr, 1, pos - 1), "-", ":")
                        strDay = Left(varheure, 2)
                        strMonth = Mid(varheure, 4, 2)
                        strYear = "20" & Mid(varheure, 7, 2)
                        varDate = CInt(CDate(strDay & "/" & strMonth & "/" & strYear).ToOADate)
                        varheure = Mid(varheure, 10, 5)
                        posold = pos
                        pos = InStr(pos + 1, TempStr, ";")
                        '** RECUPERATION CHAMP CODE **
                        'Code= Mid(TempStr, posold + 1, pos - posold - 1)
                        posold = pos
                        pos = InStr(pos + 1, TempStr, ";")
                        '** RECUPERATION CHAMP EVENEMENT **
                        varEvent = Mid(TempStr, posold + 1, pos - posold - 1)
                        PosDieze = InStr(varEvent, "#")
                        VarIdRonde = Mid(varEvent, PosDieze + 1, InStr(PosDieze, varEvent, ")") - PosDieze - 1)
                        posold = pos
                        pos = InStr(pos + 1, TempStr, ";")
                        '** RECUPERATION CHAMP POINT DE RONDE **
                        'Point de ronde = ChaineLabel & Mid(TempStr, posold + 1, pos - posold - 1)
                        posold = pos
                        'pos = InStr(pos + 1, TempStr, ";")
                        '** RECUPERATION CHAMP DESCRIPTION **
                        ChaineLabel = Right(TempStr, Len(TempStr) - pos)
                        PosDieze = InStr(ChaineLabel, "#")
                        varIdUser = Mid(ChaineLabel, PosDieze + 1, InStr(PosDieze, ChaineLabel, ")") - PosDieze - 1)
                        textstr.Close()
                        NomFic.Delete()
                        textstr = Nothing

                        If Verification_OpeMain(VarIdRonde, varIdUser) Then
                            'Enregistrement de l'opération préventive
                            Enregistrement_ActPrev(varDate, varheure, varEvent, VarIdRonde, varIdUser)
                        End If
                    Else
                        If connect_ADO.ADODB_connection(1) Then
                            data = connect_ADO.ADODB_create_recordset(1, "SELECT IDRM FROM RONDIER_LECTURE ORDER BY IDRM DESC")
                            If Not data.EOF Then
                                IDRM = data.Fields(0).Value + 1
                            Else
                                IDRM = 1
                            End If
                            data.Close()

                            Dim textstr As New StreamReader(PathRondier & "\" & TempStr, SytemeCodePage)
                            Dim pos As Integer
                            Dim posold As Integer
                            Dim CodeInc As String
                            Dim varheure As String
                            Dim chaine As String

                            TempStr = textstr.ReadLine()
                            'non traitement de la ligne des titres
                            Do Until textstr.Peek = -1
                                TempStr = textstr.ReadLine()
                                TempStr = Replace(TempStr, """", "")
                                pos = 1
                                pos = InStr(pos, TempStr, ";")
                                '** RECUPERATION CHAMP DATE **
                                varheure = Replace(Mid(TempStr, 1, pos - 1), "-", ":")
                                posold = pos
                                pos = InStr(pos + 1, TempStr, ";")
                                CodeInc = Mid(TempStr, posold + 1, pos - posold - 1)
                                posold = pos
                                pos = InStr(pos + 1, TempStr, ";")
                                ChaineLabel = Mid(TempStr, posold + 1, pos - posold - 1) 'Right(TempStr, TempStr.Length - pos) 
                                PosDieze = InStr(ChaineLabel, "#")
                                If PosDieze > 0 Then
                                    ChaineLabel = Left(ChaineLabel, PosDieze - 2)
                                End If
                                posold = pos
                                pos = InStr(pos + 1, TempStr, ";")
                                If Mid(TempStr, posold + 1, pos - posold - 1).Trim <> "" Then
                                    ChaineLabel = ChaineLabel & " - " & Mid(TempStr, posold + 1, pos - posold - 1)
                                End If
                                'posold = pos
                                'pos = InStr(pos + 1, TempStr, ";")
                                If Right(TempStr, TempStr.Length - pos).Trim <> "" Then
                                    ChaineLabel = ChaineLabel & " - " & Right(TempStr, TempStr.Length - pos)
                                End If
                                PosDieze = InStr(ChaineLabel, "#")

                                If PosDieze > 0 Then
                                    If CodeInc = "6" Then 'ANOMALIE
                                        chainetmp = Mid(ChaineLabel, PosDieze + 1, InStr(PosDieze, ChaineLabel, ")") - PosDieze - 1)
                                    Else
                                        chainetmp = "-"
                                    End If
                                    ChaineLabel = Left(ChaineLabel, PosDieze - 2)
                                Else
                                    chainetmp = "-"
                                End If
                                chaine = "INSERT INTO RONDIER_LECTURE (NomMemo, RefMemo, RefType, RefInfo, RefLabel, IdPuce, DateHeure, IDRM) "
                                chaine = chaine & " VALUES ('-','-','" & CodeInc & "', '-', '" & Replace(ChaineLabel, "'", "''") & "','" & chainetmp
                                chaine = chaine & "','" & varheure & "'," & IDRM & ");"
                                connect_ADO.ADODB_delete(1, chaine)
                            Loop
                            textstr.Close()
                            data = Nothing
                            NomFic.Delete()
                            textstr.Close()
                            textstr = Nothing
                        End If
                        connect_ADO.ADODB_deconnection(1)
                    End If
                End If
            Next
            NomRep = Nothing
            ListFic = Nothing
            Return True
            Exit Function

        Catch ex As Exception
            DevExpress.XtraEditors.XtraMessageBox.Show(ex.Message, Application.ProductName, MessageBoxButtons.OK)
            connect_ADO.ADODB_deconnection(1)
            Return False
        End Try

    End Function

    Public Function ImportRondierMemo() As Boolean

        Try

            Dim data As ADODB.Recordset
            Dim NomRep As DirectoryInfo
            Dim ListFic() As FileInfo
            Dim TempStr As String
            Dim IDRM As Long
            Dim SytemeCodePage As New System.Text.UTF7Encoding

            NomRep = New DirectoryInfo(PathRondier)

            ListFic = NomRep.GetFiles()

            Dim Resultat As Integer = 0
            Dim mess As String = "Un nouveau fichier de ronde a été trouvé. Voulez vous l'importer ?"

            For Each NomFic As FileInfo In ListFic
                TempStr = NomFic.Name
                If Len(TempStr) = 29 And UCase(Right(TempStr, 3)) = "TXT" Then
                    If (Mid(TempStr, 3, 1) = "_") And (Mid(TempStr, 6, 1) = "_") And (Mid(TempStr, 11, 1) = "_") And (Mid(TempStr, 14, 1) = "_") And (Mid(TempStr, 17, 1) = "_") Then
                        'ON TRAITE UN FICHIER MEMO STICK
                        If Resultat = 0 Then
                            Resultat = DevExpress.XtraEditors.XtraMessageBox.Show(mess, Application.ProductName, MessageBoxButtons.YesNo)
                            'RESULTAT = 6 => OUI
                            'RESULTAT = 7 => NON
                        End If
                        If Resultat = 6 Then
                            If connect_ADO.ADODB_connection(1) Then
                                data = connect_ADO.ADODB_create_recordset(1, "SELECT IDRM FROM RONDIER_LECTURE ORDER BY IDRM DESC")
                                If Not data.EOF Then
                                    IDRM = data.Fields(0).Value + 1
                                    data.Close()
                                Else
                                    IDRM = 1
                                End If

                                data = Nothing
                                data = connect_ADO.ADODB_create_recordset(1, "SELECT * FROM RONDIER_LECTURE")
                                Dim textstr As New StreamReader(PathRondier & "\" & TempStr, SytemeCodePage)
                                Dim pos As Integer
                                Dim posold As Integer

                                Do Until textstr.Peek = -1
                                    TempStr = textstr.ReadLine()
                                    data.AddNew()
                                    pos = 1
                                    pos = InStr(pos, TempStr, ";")
                                    data.Fields("NomMemo").Value = Mid(TempStr, 1, pos - 1)
                                    posold = pos
                                    pos = InStr(pos + 1, TempStr, ";")
                                    data.Fields("RefMemo").Value = Mid(TempStr, posold + 1, pos - posold - 1)
                                    posold = pos
                                    pos = InStr(pos + 1, TempStr, ";")
                                    data.Fields("RefType").Value = Mid(TempStr, posold + 1, pos - posold - 1)
                                    posold = pos
                                    pos = InStr(pos + 1, TempStr, ";")
                                    data.Fields("RefInfo").Value = Mid(TempStr, posold + 1, pos - posold - 1)
                                    posold = pos
                                    pos = InStr(pos + 1, TempStr, ";")
                                    data.Fields("RefLabel").Value = Mid(TempStr, posold + 1, pos - posold - 1)
                                    posold = pos
                                    pos = InStr(pos + 1, TempStr, ";")
                                    data.Fields("IdPuce").Value = Mid(TempStr, posold + 1, pos - posold - 1)
                                    data.Fields("DateHeure").Value = Mid(TempStr, pos + 1, Len(TempStr) - pos)
                                    data.Fields("IDRM").Value = IDRM
                                    data.Update()
                                Loop
                                textstr.Close()
                                data.Close()
                                data = Nothing
                                NomFic.Delete()
                                textstr.Close()
                                textstr = Nothing
                            End If
                            connect_ADO.ADODB_deconnection(1)
                        Else
                            NomFic.Delete()
                        End If
                    End If
                End If
            Next
            NomRep = Nothing
            ListFic = Nothing
            Return True
            Exit Function

        Catch ex As Exception
            DevExpress.XtraEditors.XtraMessageBox.Show(ex.Message, Application.ProductName, MessageBoxButtons.OK)
            connect_ADO.ADODB_deconnection(1)
            Return False
        End Try

    End Function

    Sub refreshJetons()
        connect_ADO.ADODB_connection(2)
        connect_ADO.ADODB_delete(2, "DELETE  FROM JETONS WHERE NomMachine='" & _ID_MACHINE & "' AND NomClasse <> 'LECTURE'")
        connect_ADO.ADODB_deconnection(2)
    End Sub

    Friend Sub SSLIA_MAJ()
        'Fonction de mise à jour des valeurs du SSLIA
        Dim sql As String = ""
        Dim rs As ADODB.Recordset
        connect_ADO.ADODB_connection(1)
        rs = connect_ADO.ADODB_create_recordset(1, "SELECT * FROM LANGUE WHERE IdObjet='DIA' AND IdLangue='LVP_TITRE'")
        If Not rs.EOF Then
            FrmFenetre.lblEtatSSLIA.Text = rs.Fields("Libelle").Value
        End If
        rs.Close()
        sql = "SELECT IA_VENT.NomVent, IA_LVP.nomLVP, IA_DOUBLET.nomDoublet, IA_ETAT.idETAT, IA_ETAT.idLVP, IA_ETAT.idVENT "
        sql = sql & "FROM IA_VENT, IA_LVP, IA_DOUBLET, IA_ETAT WHERE(IA_VENT.idVENT = IA_ETAT.idVENT) "
        sql = sql & "AND IA_LVP.idLVP = IA_ETAT.idLVP AND IA_DOUBLET.idDoublet = IA_ETAT.idETAT"
        rs = connect_ADO.ADODB_create_recordset(1, sql)
        If Not rs.EOF Then
            FrmFenetre.lblVent.Text = (rs.Fields("NomVent").Value)
            FrmFenetre.lblDoublet.Text = (rs.Fields("nomDoublet").Value)
            FrmFenetre.lblLVP.Text = (rs.Fields("nomLVP").Value)
            idVENT = (rs.Fields("idVENT").Value)
            idDoublet = (rs.Fields("idETAT").Value)
            idLVP = (rs.Fields("idLVP").Value)
        End If
        rs.Close()
        'Gestion des images
        If idDoublet = 3 Then
            FrmFenetre.btnDoublet.Image = Global.MC.My.Resources.Resources.Smileys2
        Else ' Nuages
            FrmFenetre.btnDoublet.Image = Global.MC.My.Resources.Resources.Smileys1
        End If
        If idLVP = 3 Then 'SOLEIL
            FrmFenetre.btnLvp.Image = Global.MC.My.Resources.Resources.soleil
        Else ' Nuages
            FrmFenetre.btnLvp.Image = Global.MC.My.Resources.Resources.soleilnuageux
        End If
        connect_ADO.ADODB_deconnection(1)
    End Sub

    Friend Function Verification_OpeMain(ByRef VarIdRonde As String, ByRef varIdUser As String) As Boolean
        Dim Enr As ADODB.Recordset
        If Not connect_ADO.ADODB_connection(1) Then
            DevExpress.XtraEditors.XtraMessageBox.Show(ListeMessages("MESS_ERRBDD").Libelle, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Return False
        End If
        Enr = connect_ADO.ADODB_create_recordset(1, "SELECT * FROM RONDIER_MATERIELSGM WHERE CodePuce='" & VarIdRonde & "'")
        If Not Enr.EOF Then
            VarIdRonde = Enr.Fields("IdMateriel").Value
        Else
            Enr.Close()
            connect_ADO.ADODB_deconnection(1)
            Return False
        End If
        Enr.Close()
        Enr = connect_ADO.ADODB_create_recordset(1, "SELECT * FROM RONDIER_USERS WHERE CodePuce='" & varIdUser & "'")
        If Not Enr.EOF Then
            varIdUser = Enr.Fields("IdUser").Value
        Else
            Enr.Close()
            connect_ADO.ADODB_deconnection(1)
            Return False
        End If
        Enr.Close()
        connect_ADO.ADODB_deconnection(1)
        Return True
    End Function

    Friend Sub Enregistrement_ActPrev(ByVal varDate As Integer, ByVal varHeure As String, ByVal varEvent As String, ByVal varIdRonde As String, ByVal varIdUser As String)
        Dim Enr As ADODB.Recordset
        Dim chaine As String
        Dim IdRM As Integer
        Dim IdOM As Integer
        Dim BrouillonEncours As Boolean
        Dim Maintenant As String
        Dim stringtemp As String
        Dim PHRASE As String
        Dim OBJET As String

        'CAS DE L'ENREGISTREMENT OSGRIM
        If Not connect_ADO.ADODB_connection(1) Then
            DevExpress.XtraEditors.XtraMessageBox.Show(ListeMessages("MESS_ERRBDD").Libelle, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Exit Sub
        End If
 
        Randomize()
        Maintenant = CStr(-1 * (Int((999999999 - 100000000 + 1) * Rnd() + 100000000)))
        chaine = "INSERT INTO RAPPORTSGM (Pilier) VALUES ('" & Maintenant & "')"
        connect_ADO.ADODB_delete(1, chaine)
        Enr = connect_ADO.ADODB_create_recordset(1, "SELECT * FROM RAPPORTSGM WHERE Pilier='" & Maintenant & "';")
        IdRM = Enr.Fields("IDRapportGM").Value
        With Enr
            .Fields("HeureDebut").Value = varHeure
            .Fields("DateDebut").Value = varDate
            .Fields("IDNatureOperation").Value = NatOpe
            .Fields("IDMaterielGM").Value = varIdRonde
            .Fields("IDGroupe").Value = Groupe
            .Fields("Brouillon").Value = True
            .Fields("IDUtilisateurStationnaire").Value = Stationnaire
            Dim rs As ADODB.Recordset = connect_ADO.ADODB_create_recordset(1, "SELECT * FROM LANGUE WHERE IdObjet='OPEMAIN' AND IdLangue='DEFAUT_Pilier'")
            .Fields("Pilier").Value = rs.Fields("Libelle").Value
            rs.Close()
            rs = connect_ADO.ADODB_create_recordset(1, "SELECT * FROM LANGUE WHERE IdObjet='OPEMAIN' AND IdLangue='DEFAUT_CodeOperation'")
            .Fields("CodeOperation").Value = rs.Fields("Libelle").Value
            rs.Close()
            rs = connect_ADO.ADODB_create_recordset(1, "SELECT * FROM LANGUE WHERE IdObjet='OPEMAIN' AND IdLangue='DEFAUT_MaterielConcerne'")
            .Fields("MaterielConcerne").Value = rs.Fields("Libelle").Value
            rs.Close()
            .Update()
            BrouillonEncours = True
            .Close()
        End With
        Dim NomNat As String
        Dim NomIntervenant As String
        Enr = connect_ADO.ADODB_create_recordset(1, "SELECT * FROM MATERIELSGM WHERE IdMaterielGM=" & varIdRonde & ";")
        If Not Enr.EOF Then
            NomNat = Enr.Fields("NomMaterielGM").Value
        Else
            NomNat = ""
        End If
        Enr.Close()
        Enr = connect_ADO.ADODB_create_recordset(1, "SELECT * FROM UTILISATEURS WHERE IdUtilisateur=" & varIdUser & ";")
        If Not Enr.EOF Then
            NomIntervenant = Enr.Fields("NomUtilisateur").Value & " " & Enr.Fields("PrenomUtilisateur").Value
        Else
            NomIntervenant = ""
        End If
        Enr.Close()
        Enr = connect_ADO.ADODB_create_recordset(1, "SELECT * FROM LANGUE WHERE IdObjet='OPEMAIN' AND IdLangue='PHRASE'")
        PHRASE = Enr.Fields("Libelle").Value
        Enr.Close()
        Enr = connect_ADO.ADODB_create_recordset(1, "SELECT * FROM LANGUE WHERE IdObjet='OPEMAIN' AND IdLangue='OBJET'")
        OBJET = Enr.Fields("Libelle").Value
        Enr.Close()
        connect_ADO.ADODB_deconnection(1)
        'ENREGISTREMENT MAIN COURANTE
        If Not connect_ADO.ADODB_connection(2) Then
            DevExpress.XtraEditors.XtraMessageBox.Show(ListeMessages("MESS_ERRBDD").Libelle, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Exit Sub
        End If
        Maintenant = CStr(-1 * (Int((999999 - 100000 + 1) * Rnd() + 100000)))
        chaine = "INSERT INTO OPERATIONMAINTENANCE (IDMateriel) VALUES (" & Maintenant & ")"
        connect_ADO.ADODB_delete(2, chaine)
        Enr = connect_ADO.ADODB_create_recordset(2, "SELECT * FROM OPERATIONMAINTENANCE WHERE IDMateriel=" & Maintenant & ";")
        IdOM = Enr.Fields("IDOM").Value
        With Enr
            .Fields("IDNatOpe").Value = NatOpe
            .Fields("IDMateriel").Value = varIdRonde
            .Fields("IDGroupe").Value = Groupe
            .Fields("DebutOPE").Value = Date.Now.ToOADate
            .Fields("FinOPE").Value = 0
            .Fields("EstBrouillon").Value = True
            .Fields("EstActif").Value = True
            .Fields("DateRapportSuppression").Value = 0
            .Fields("IDRM").Value = IdRM
        End With
        Enr.Update()
        Enr.Close()
        'ENREGISTREMENT DANS MC
        chaine = "INSERT INTO MC (DateHeureDebut,Affichage,Objet,EstSupp,CleObj,EstEnCours,CleStationnaire,EstBrouillon,AttenteRapport"
        stringtemp = Replace(PHRASE, "#1#", NomNat)
        stringtemp = Replace(stringtemp, "#intervenant#", NomIntervenant)
        stringtemp = Me.connect_ADO.mysql_escape_string(stringtemp)
        chaine = chaine & ") VALUES (" & Replace(Date.Now.ToOADate.ToString, ",", ".") & ",'" & stringtemp & "','" & OBJET & "',0," & IdOM & ",1"
        chaine = chaine & "," & Stationnaire & ",0,1)"
        connect_ADO.ADODB_delete(2, chaine)
        connect_ADO.ADODB_deconnection(2)

        'ENREGISTREMENT DES INTERVENANTS
        IntervenantsTraite(IdOM, varIdUser, IdRM)

    End Sub

    Public Sub IntervenantsTraite(ByVal IdOM As Integer, ByVal IdUser As Integer, ByVal IdRM As Integer)
        Dim Enr As ADODB.Recordset
        Dim SQL As String

        If Not connect_ADO.ADODB_connection(2) And connect_ADO.ADODB_connection(1) Then
            DevExpress.XtraEditors.XtraMessageBox.Show(ListeMessages("MESS_ERRBDD").Libelle, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Exit Sub
        End If

        Enr = connect_ADO.ADODB_create_recordset(1, "SELECT * FROM UTILISATEURS WHERE IDUtilisateur=" & IdUser)
        If Not Enr.EOF Then
            Enr.Fields("EvenementEnCours").Value = 0
            Enr.Fields("Disponibilite").Value = 3
            Enr.Update()
        End If
        Enr.Close()
        'suppression si ancien
        connect_ADO.ADODB_delete(2, "DELETE FROM IntervenantOM WHERE IDOM=" & IdOM)
        SQL = "INSERT INTO IntervenantOM (IdOM,IDUtilisateur,EstSupprime) VALUES (" & IdOM & "," & IdUser & ",0);"
        connect_ADO.ADODB_delete(2, SQL)
        connect_ADO.ADODB_delete(1, "INSERT INTO A_INTERVENANTS_RAPPORTSGM (IDRapportsGM,IDUtilisateur) VALUES (" & IdRM & "," & IdUser & ");")
        Enr = Nothing
        connect_ADO.ADODB_deconnection(1)
        connect_ADO.ADODB_deconnection(2)

    End Sub
End Class
