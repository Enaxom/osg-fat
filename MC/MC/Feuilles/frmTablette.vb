Imports System.Collections.Generic
Imports DevExpress.XtraGrid
Imports System.Drawing
Imports DevExpress.Utils
Imports System.Text
Imports System.IO
Imports System.Web.Script.Serialization
Imports System.Windows.Forms

Public Class frmTablette

    Private ClassParent As frmEcran

    Sub New(frmEcran As frmEcran)
        InitializeComponent()
        ClassParent = frmEcran
        'Skins
        DevExpress.Skins.SkinManager.EnableFormSkins()
        DevExpress.LookAndFeel.LookAndFeelHelper.ForceDefaultLookAndFeelChanged()
        DevExpress.UserSkins.BonusSkins.Register()
        DevExpress.UserSkins.OfficeSkins.Register()
    End Sub

    Function Init() As Boolean
        Dim rs As ADODB.Recordset

        Try

            If ClassParent.ClassParent.connect_ADO.ADODB_connection(1) Then
                rs = ClassParent.ClassParent.connect_ADO.ADODB_create_recordset(1, "SELECT * FROM LANGUE WHERE IdObjet='GENERIQUE' AND IdLangue='BTN_FERMER'")
                btnFermer.Text = rs.Fields("Libelle").Value
                rs.Close()

                rs = ClassParent.ClassParent.connect_ADO.ADODB_create_recordset(1, "SELECT * FROM LANGUE WHERE IdObjet='MC' AND IdLangue='TITRE_FRMDISPOS'")
                Me.Text = rs.Fields("Libelle").Value
                rs.Close()

                rs = ClassParent.ClassParent.connect_ADO.ADODB_create_recordset(1, "SELECT * FROM LANGUE WHERE IdObjet='ANDROID' AND IdLangue='BTN_IMPORT'")
                btn_import.Text = rs.Fields("Libelle").Value
                rs.Close()

                rs = ClassParent.ClassParent.connect_ADO.ADODB_create_recordset(1, "SELECT * FROM LANGUE WHERE IdObjet='ANDROID' AND IdLangue='BTN_EXPORT'")
                Dim str As String = rs.Fields("Libelle").Value
                Dim strBtn() As String = str.Split("#1")
                btn_export.Text = strBtn(0) & vbNewLine & strBtn(1)
                rs.Close()
            Else
                Return False
            End If

            ClassParent.ClassParent.connect_ADO.ADODB_deconnection(1)
            Return True
        Catch ex As Exception
            Return False
        End Try

    End Function

    Private Sub btnFermer_Click(sender As System.Object, e As System.EventArgs) Handles btnFermer.Click
        Me.Close()
    End Sub

    ' Import data
    Private Sub SimpleButton2_Click(sender As System.Object, e As System.EventArgs) Handles btn_import.Click
        Dim path As String = ClassParent.ClassParent.pathAndroid
        ' Dim stream As Stream = File.OpenRead("C:\Temp\data_export.txt")
        If Not System.IO.File.Exists(path & "\data_export.txt") Then
            ' Error message when importation fails
            DevExpress.XtraEditors.XtraMessageBox.Show(ClassParent.ClassParent.ListeMessages("MESS_IMPORT_ERR").Libelle, Application.ProductName, MessageBoxButtons.OK)
            Exit Sub
        End If

        'Open file located in xml path android
        Dim stream As Stream = File.OpenRead(path & "\data_export.txt")
        Dim reader As New StreamReader(stream)
        Dim jsonData As String = reader.ReadToEnd
        reader.Close()

        Dim ser As New JavaScriptSerializer
        Dim reports() As Report = ser.Deserialize(Of Report())(jsonData)
        Dim data As ADODB.Recordset
        Dim now As String
        Dim idR As Integer
        Dim d As Date
        Dim detIndex As Integer
        Dim detail As ADODB.Recordset

        If ClassParent.ClassParent.connect_ADO.ADODB_connection(1) Then

            For i As Integer = 0 To reports.Length - 1
                ' Report creation
                now = CStr(-1 * (Int((999999999 - 100000000 + 1) * Rnd() + 100000000)))
                ClassParent.ClassParent.connect_ADO.ADODB_delete(1, "INSERT INTO RAPPORTSGM (CodeOperation) VALUES ('" & now & "');")

                data = ClassParent.ClassParent.connect_ADO.ADODB_create_recordset(1, "SELECT * FROM RAPPORTSGM WHERE CodeOperation='" & now & "';")
                idR = data.Fields("IDRapportGM").Value

                data.Fields("IDGroupe").Value = reports(i).idTeam
                data.Fields("IDUtilisateur").Value = reports(i).idUser
                data.Fields("IDVoie").Value = reports(i).idZone
                data.Fields("IDBatiment").Value = reports(i).idBuilding
                data.Fields("IDNiveau").Value = reports(i).idLevel
                data.Fields("IDCanton").Value = reports(i).idCanton
                data.Fields("Pilier").Value = reports(i).pillar
                data.Fields("IDCatLocalisation").Value = reports(i).idSpaceCat
                data.Fields("IDLocalisation").Value = reports(i).idSpace
                data.Fields("IDNatureOperation").Value = reports(i).idNature
                data.Fields("IDNatureEvenement").Value = reports(i).idFrequency
                data.Fields("IDMaterielGM").Value = reports(i).idMaterial
                data.Fields("CodeOperation").Value = reports(i).code

                d = reports(i).startDate
                If reports(i).startDate <> "" Then data.Fields("DateDebut").Value = d.ToOADate
                If reports(i).endDate <> "" Then
                    d = reports(i).endDate
                    data.Fields("DateFin").Value = d.ToOADate
                End If

                data.Fields("HeureDebut").Value = reports(i).startTime
                data.Fields("HeureFin").Value = reports(i).endTime
                data.Fields("MaterielConcerne").Value = Replace(reports(i).comment, "'", "''")
                data.Fields("DateCreationRapportGM").Value = Date.Now.ToOADate
                data.Fields("Brouillon").Value = True
                data.Update()
                data.Close()

                ' Fill A_SERVICES_RAPPORTSGM table
                reports(i).serviceCat.ForEach(Sub(obj)
                                                  For j As Integer = 0 To obj.idServices.Length - 1
                                                      ClassParent.ClassParent.connect_ADO.ADODB_delete(1, "INSERT INTO A_SERVICES_RAPPORTSGM (IDRapportGM, IDService) VALUES (" & idR & "," & obj.idServices(j) & ");")
                                                  Next
                                              End Sub)

                ' Fill A_INTERVENANTS_RAPPORTSGM table
                reports(i).participants.ForEach(Sub(part)
                                                    ClassParent.ClassParent.connect_ADO.ADODB_delete(1, "INSERT INTO A_INTERVENANTS_RAPPORTSGM (IDRapportsGM, IDUtilisateur, IDGroupe) VALUES (" & idR & "," & part.idParticipant & "," & part.idTeam & ");")
                                                End Sub)

                ' Fill A_INCIDENTSGM_RAPPORTSGM table
                reports(i).incidents.ForEach(Sub(inc)
                                                 ClassParent.ClassParent.connect_ADO.ADODB_delete(1, "INSERT INTO A_INCIDENTSGM_RAPPORTSGM (IDRapportGM, IDIncident, NbIncident) VALUES (" & idR & "," & inc.idIncident & "," & inc.nbAnomaly & ");")
                                             End Sub)

                ' Fill DETAILSGM table
                detIndex = 1
                reports(i).details.ForEach(Sub(det)
                                               If det.answer <> "" Then
                                                   If detIndex = 1 Then
                                                       ClassParent.ClassParent.connect_ADO.ADODB_execute(1, "INSERT INTO DETAILSGM (IDRapportGM, Detail1, Detail1bis, Label1) VALUES (" & idR & ",'" & Replace(det.answer, "'", "''") & "','" & Replace(det.answer, "'", "''") & "','" & Replace(det.title, "'", "''") & "');")
                                                   Else
                                                       detail = ClassParent.ClassParent.connect_ADO.ADODB_create_recordset(1, "SELECT * FROM DETAILSGM WHERE IDRapportGM=" & idR & ";")
                                                       detail.Fields("Detail" & detIndex).Value = det.answer
                                                       detail.Fields("Detail" & detIndex & "bis").Value = det.answer
                                                       detail.Fields("Label" & detIndex).Value = det.title
                                                       detail.Update()
                                                       detail.Close()
                                                   End If
                                                   detIndex += 1
                                               End If
                                           End Sub)

            Next

        End If

        ClassParent.ClassParent.connect_ADO.ADODB_deconnection(1)

        ' Import confirmation
        DevExpress.XtraEditors.XtraMessageBox.Show(ClassParent.ClassParent.ListeMessages("MESS_IMPORT_SUCCESS").Libelle, Application.ProductName, MessageBoxButtons.OK)
    End Sub

    ' Export data
    Private Sub SimpleButton1_Click(sender As System.Object, e As System.EventArgs) Handles btn_export.Click
        ' First file : label.json
        Dim labels(27) As String
        Dim rs As ADODB.Recordset
        Dim path As String = ClassParent.ClassParent.pathAndroid

        If ClassParent.ClassParent.connect_ADO.ADODB_connection(1) Then
            rs = ClassParent.ClassParent.connect_ADO.ADODB_create_recordset(1, "SELECT * FROM LANGUE WHERE IdObjet='ADM_GROUPES' AND IdLangue='LBL_1'")
            labels(1) = rs.Fields("Libelle").Value
            rs.Close()

            rs = ClassParent.ClassParent.connect_ADO.ADODB_create_recordset(1, "SELECT * FROM LANGUE WHERE IdObjet='RAP_ADMIN' AND IdLangue='COMBOBOX_2'")
            labels(2) = rs.Fields("Libelle").Value
            rs.Close()

            rs = ClassParent.ClassParent.connect_ADO.ADODB_create_recordset(1, "SELECT * FROM LANGUE WHERE IdObjet='RAP_ADMIN' AND IdLangue='GRP' And Type=0")
            labels(3) = rs.Fields("Libelle").Value
            rs.Close()

            rs = ClassParent.ClassParent.connect_ADO.ADODB_create_recordset(1, "SELECT * FROM LANGUE WHERE IdObjet='ADM_VOIES' AND IdLangue='LBL_1'")
            labels(4) = rs.Fields("Libelle").Value
            rs.Close()

            rs = ClassParent.ClassParent.connect_ADO.ADODB_create_recordset(1, "SELECT * FROM LANGUE WHERE IdObjet='ADM_BATIMENTS' AND IdLangue='LBL_1'")
            labels(5) = rs.Fields("Libelle").Value
            rs.Close()

            rs = ClassParent.ClassParent.connect_ADO.ADODB_create_recordset(1, "SELECT * FROM LANGUE WHERE IdObjet='ADM_NIVEAUX' AND IdLangue='LBL_1'")
            labels(6) = rs.Fields("Libelle").Value
            rs.Close()

            rs = ClassParent.ClassParent.connect_ADO.ADODB_create_recordset(1, "SELECT * FROM LANGUE WHERE IdObjet='ADM_CANTONS' AND IdLangue='LBL_1'")
            labels(7) = rs.Fields("Libelle").Value
            rs.Close()

            rs = ClassParent.ClassParent.connect_ADO.ADODB_create_recordset(1, "SELECT * FROM LANGUE WHERE IdObjet='RAP_ADMIN' AND IdLangue='LBL_TXT_GRP'")
            labels(8) = rs.Fields("Libelle").Value
            rs.Close()

            rs = ClassParent.ClassParent.connect_ADO.ADODB_create_recordset(1, "SELECT * FROM LANGUE WHERE IdObjet='ADM_LOCALISATIONS' AND IdLangue='LBL_CATEGORIE'")
            labels(9) = rs.Fields("Libelle").Value
            rs.Close()

            rs = ClassParent.ClassParent.connect_ADO.ADODB_create_recordset(1, "SELECT * FROM LANGUE WHERE IdObjet='ADM_LOCALISATIONS' AND IdLangue='LBL_1'")
            labels(10) = rs.Fields("Libelle").Value
            rs.Close()

            rs = ClassParent.ClassParent.connect_ADO.ADODB_create_recordset(1, "SELECT * FROM LANGUE WHERE IdObjet='RAP_ADMIN' AND IdLangue='GRP' And Type=1")
            labels(11) = rs.Fields("Libelle").Value
            rs.Close()

            rs = ClassParent.ClassParent.connect_ADO.ADODB_create_recordset(1, "SELECT * FROM LANGUE WHERE IdObjet='ADM_NATURES_OPERATIONS' AND IdLangue='LBL_1'")
            labels(12) = rs.Fields("Libelle").Value
            rs.Close()

            rs = ClassParent.ClassParent.connect_ADO.ADODB_create_recordset(1, "SELECT * FROM LANGUE WHERE IdObjet='ADM_NATURES_EVENEMENTS' AND IdLangue='LBL_1'")
            labels(13) = rs.Fields("Libelle").Value
            rs.Close()

            rs = ClassParent.ClassParent.connect_ADO.ADODB_create_recordset(1, "SELECT * FROM LANGUE WHERE IdObjet='ADM_MATERIELS' AND IdLangue='LBL_1'")
            labels(14) = rs.Fields("Libelle").Value
            rs.Close()

            rs = ClassParent.ClassParent.connect_ADO.ADODB_create_recordset(1, "SELECT * FROM LANGUE WHERE IdObjet='RAP_ADMIN' AND IdLangue='LBL_TXT_GRP'")
            labels(15) = rs.Fields("Libelle").Value
            rs.Close()

            rs = ClassParent.ClassParent.connect_ADO.ADODB_create_recordset(1, "SELECT * FROM LANGUE WHERE IdObjet='RAP_ADMIN' AND IdLangue='LBL_DATEDEBUT'")
            labels(16) = rs.Fields("Libelle").Value
            rs.Close()

            rs = ClassParent.ClassParent.connect_ADO.ADODB_create_recordset(1, "SELECT * FROM LANGUE WHERE IdObjet='RAP_ADMIN' AND IdLangue='LBL_DATEFIN'")
            labels(17) = rs.Fields("Libelle").Value
            rs.Close()

            rs = ClassParent.ClassParent.connect_ADO.ADODB_create_recordset(1, "SELECT * FROM LANGUE WHERE IdObjet='RAP_ADMIN' AND IdLangue='GRP' And Type=2")
            labels(18) = rs.Fields("Libelle").Value
            rs.Close()

            rs = ClassParent.ClassParent.connect_ADO.ADODB_create_recordset(1, "SELECT * FROM LANGUE WHERE IdObjet='ADM_GROUPES' AND IdLangue='LINK'")
            labels(19) = rs.Fields("Libelle").Value
            rs.Close()

            rs = ClassParent.ClassParent.connect_ADO.ADODB_create_recordset(1, "SELECT * FROM LANGUE WHERE IdObjet='INTER_ADMIN' AND IdLangue='GRP'")
            labels(20) = rs.Fields("Libelle").Value
            rs.Close()

            rs = ClassParent.ClassParent.connect_ADO.ADODB_create_recordset(1, "SELECT * FROM LANGUE WHERE IdObjet='INTER_ADMIN' AND IdLangue='LBL_FRM_INTERVENANT'")
            labels(21) = rs.Fields("Libelle").Value
            rs.Close()

            rs = ClassParent.ClassParent.connect_ADO.ADODB_create_recordset(1, "SELECT * FROM LANGUE WHERE IdObjet='RAP_ADMIN' AND IdLangue='GRP' And Type=4")
            labels(22) = rs.Fields("Libelle").Value
            rs.Close()

            rs = ClassParent.ClassParent.connect_ADO.ADODB_create_recordset(1, "SELECT * FROM LANGUE WHERE IdObjet='RAP_ADMIN' AND IdLangue='LBL_COM'")
            labels(23) = rs.Fields("Libelle").Value
            rs.Close()

            rs = ClassParent.ClassParent.connect_ADO.ADODB_create_recordset(1, "SELECT * FROM LANGUE WHERE IdObjet='RAP_ADMIN' AND IdLangue='GRP' And Type=5")
            labels(24) = rs.Fields("Libelle").Value
            rs.Close()

            rs = ClassParent.ClassParent.connect_ADO.ADODB_create_recordset(1, "SELECT * FROM LANGUE WHERE IdObjet='GENERIQUE' AND IdLangue='BTN_ENR'")
            labels(25) = rs.Fields("Libelle").Value
            rs.Close()

            rs = ClassParent.ClassParent.connect_ADO.ADODB_create_recordset(1, "SELECT * FROM LANGUE WHERE IdObjet='GENERIQUE' AND IdLangue='BTN_BROUILLON'")
            labels(26) = rs.Fields("Libelle").Value
            rs.Close()

            rs = ClassParent.ClassParent.connect_ADO.ADODB_create_recordset(1, "SELECT * FROM LANGUE WHERE IdObjet='GENERIQUE' AND IdLangue='BTN_ANNULER'")
            labels(27) = rs.Fields("Libelle").Value
            rs.Close()
        End If

        ' Object to serialize to Json object
        Dim label As New Label()
        label.team = labels(1)
        label.user = labels(2)
        label.address = labels(3)
        label.zone = labels(4)
        label.building = labels(5)
        label.level = labels(6)
        label.canton = labels(7)
        label.pillar = labels(8)
        label.spaceCat = labels(9)
        label.space = labels(10)
        label.maintenance = labels(11)
        label.nature = labels(12)
        label.frequency = labels(13)
        label.material = labels(14)
        label.code = labels(15)
        label.startDate = labels(16)
        label.endDate = labels(17)
        label.services = labels(18)
        label.teams = labels(19)
        label.participant = labels(20)
        label.participants = labels(21)
        label.incident = labels(22)
        label.comment = labels(23)
        label.details = labels(24)
        label.save = labels(25)
        label.draft = labels(26)
        label.cancel = labels(27)

        Dim objSerializer As New JavaScriptSerializer()
        Dim labelJson As String
        labelJson = objSerializer.Serialize(label)

        ' Save of the file
        ' File.WriteAllText("C:\Temp\label.json", labelJson)
        ' Save file to path in xml
        Try
            File.WriteAllText(path & "\label.json", labelJson)
        Catch ex As Exception
            DevExpress.XtraEditors.XtraMessageBox.Show(ClassParent.ClassParent.ListeMessages("MESS_EXPORT_ERR").Libelle, Application.ProductName, MessageBoxButtons.OK)
            Exit Sub
        End Try

        ' data_transfer.json
        Dim data As New ReportData()

        ' Fill local value
        If ClassParent.ClassParent.connect_ADO.ADODB_connection(1) Then
            rs = ClassParent.ClassParent.connect_ADO.ADODB_create_recordset(1, "SELECT * FROM ADMIN WHERE IdObjet='LOCAL'")
            data.local = rs.Fields("Valeur").Value
            rs.Close()
        End If

        ' Fill teams values
        If ClassParent.ClassParent.connect_ADO.ADODB_connection(1) Then
            rs = ClassParent.ClassParent.connect_ADO.ADODB_create_recordset(1, "SELECT * FROM GROUPES WHERE Etat=1 order by Position")

            While Not rs.EOF
                Dim idTeam As Integer = rs.Fields("IDGroupe").Value
                Dim nameTeam As String = rs.Fields("NomGroupe").Value

                Dim rs2 As ADODB.Recordset = ClassParent.ClassParent.connect_ADO.ADODB_create_recordset(1, "SELECT IDUtilisateur FROM A_GROUPES_UTILISATEURS where IDGroupe=" & idTeam)
                Dim members(rs2.RecordCount - 1) As Integer

                For j As Integer = 0 To rs2.RecordCount - 1
                    members(j) = rs2.Fields("IDUtilisateur").Value
                    rs2.MoveNext()
                Next
                rs2.Close()

                ' Team to add and its members
                Dim teamToAdd As New Team With {.id = idTeam, .name = nameTeam, .members = members}

                ' Team is added to the list of teams
                data.teams.Add(teamToAdd)
                rs.MoveNext()
            End While
            rs.Close()
        End If

        ' Fill users values
        If ClassParent.ClassParent.connect_ADO.ADODB_connection(1) Then
            rs = ClassParent.ClassParent.connect_ADO.ADODB_create_recordset(1, "SELECT IDUtilisateur, NomUtilisateur, PrenomUtilisateur FROM UTILISATEURS where Etat=1 order by Position")

            While Not rs.EOF
                Dim idUser As Integer = rs.Fields("IDUtilisateur").Value
                Dim userToAdd As New User With {.id = idUser, .firstname = rs.Fields("PrenomUtilisateur").Value, .lastname = rs.Fields("NomUtilisateur").Value}

                ' The user is added to the users list
                data.users.Add(userToAdd)
                rs.MoveNext()
            End While
            rs.Close()
        End If

        ' Fill address values
        Dim address As New Address()

        ' Fill zones values
        If ClassParent.ClassParent.connect_ADO.ADODB_connection(1) Then
            rs = ClassParent.ClassParent.connect_ADO.ADODB_create_recordset(1, "SELECT * FROM VOIES WHERE Etat=1 order by Position")

            While Not rs.EOF
                Dim idZone As Integer = rs.Fields("IDVoie").Value
                Dim nameZone As String = rs.Fields("NomVoie").Value

                Dim rs2 As ADODB.Recordset = ClassParent.ClassParent.connect_ADO.ADODB_create_recordset(1, "SELECT IDBatiment FROM A_VOIE_BATIMENT where IDVoie=" & idZone)

                If (rs2.RecordCount = 0) Then
                    rs2 = ClassParent.ClassParent.connect_ADO.ADODB_create_recordset(1, "SELECT IDBatiment FROM BATIMENTS WHERE Etat=1 order by Position")
                End If

                Dim buildings(rs2.RecordCount - 1) As Integer

                For j As Integer = 0 To rs2.RecordCount - 1
                    buildings(j) = rs2.Fields("IDBatiment").Value
                    rs2.MoveNext()
                Next
                rs2.Close()

                ' Zone to add and its buildings
                Dim zoneToAdd As New Zone With {.id = idZone, .name = nameZone, .buildings = buildings}

                ' Zone is added to the list of zones
                address.zones.Add(zoneToAdd)
                rs.MoveNext()
            End While
            rs.Close()
        End If

        ' Fill buildings values
        If ClassParent.ClassParent.connect_ADO.ADODB_connection(1) Then
            rs = ClassParent.ClassParent.connect_ADO.ADODB_create_recordset(1, "SELECT * FROM BATIMENTS WHERE Etat=1 order by Position")

            While Not rs.EOF
                Dim idBuilding As Integer = rs.Fields("IDBatiment").Value
                Dim nameBuilding As String = rs.Fields("NomBatiment").Value

                Dim rs2 As ADODB.Recordset = ClassParent.ClassParent.connect_ADO.ADODB_create_recordset(1, "SELECT IDNiveau FROM BATIMENT_NIVEAU where IDBatiment=" & idBuilding)
                Dim levels(rs2.RecordCount - 1) As Integer

                For j As Integer = 0 To rs2.RecordCount - 1
                    levels(j) = rs2.Fields("IDNiveau").Value
                    rs2.MoveNext()
                Next
                rs2.Close()

                ' Building to add and its levels
                Dim buildingToAdd As New Building With {.id = idBuilding, .name = nameBuilding, .levels = levels}

                ' Building is added to the list of buildings
                address.buildings.Add(buildingToAdd)
                rs.MoveNext()
            End While
            rs.Close()
        End If

        ' Fill levels values
        If ClassParent.ClassParent.connect_ADO.ADODB_connection(1) Then
            rs = ClassParent.ClassParent.connect_ADO.ADODB_create_recordset(1, "SELECT * FROM NIVEAUX where Etat=1 order by Position")

            While Not rs.EOF
                Dim idLevel As Integer = rs.Fields("IDNiveau").Value
                Dim levelToAdd As New Level With {.id = idLevel, .name = rs.Fields("NomNiveau").Value}

                ' The level is added to the levels list
                address.levels.Add(levelToAdd)
                rs.MoveNext()
            End While
            rs.Close()
        End If

        ' Fill cantons values
        If ClassParent.ClassParent.connect_ADO.ADODB_connection(1) Then
            rs = ClassParent.ClassParent.connect_ADO.ADODB_create_recordset(1, "SELECT * FROM CANTONS where Etat=1 order by Position")

            While Not rs.EOF
                Dim idCanton As Integer = rs.Fields("IDCanton").Value
                Dim cantonToAdd As New Canton With {.id = idCanton, .name = rs.Fields("NomCanton").Value}

                ' The canton is added to the cantons list
                address.cantons.Add(cantonToAdd)
                rs.MoveNext()
            End While
            rs.Close()
        End If

        ' Fill spaceCats values
        If ClassParent.ClassParent.connect_ADO.ADODB_connection(1) Then
            rs = ClassParent.ClassParent.connect_ADO.ADODB_create_recordset(1, "SELECT * FROM CATEGORIES_LOCALISATIONS WHERE Etat=1 order by Position")

            While Not rs.EOF
                Dim idSpaceCat As Integer = rs.Fields("IDCategorieLocalisation").Value
                Dim nameSpaceCat As String = rs.Fields("NomCategorieLocalisation").Value

                Dim rs2 As ADODB.Recordset = ClassParent.ClassParent.connect_ADO.ADODB_create_recordset(1, "SELECT IDLocalisation FROM LOCALISATIONS where IDCategorieLocalisation=" & idSpaceCat & " and etat=1 order by Position")
                Dim spaces(rs2.RecordCount - 1) As Integer

                For j As Integer = 0 To rs2.RecordCount - 1
                    spaces(j) = rs2.Fields("IDLocalisation").Value
                    rs2.MoveNext()
                Next
                rs2.Close()

                ' SpaceCat to add and its spaces
                Dim spaceCatToAdd As New SpaceCat With {.id = idSpaceCat, .name = nameSpaceCat, .spaces = spaces}

                ' SpaceCat is added to the list of spaceCats
                address.spaceCats.Add(spaceCatToAdd)
                rs.MoveNext()
            End While
            rs.Close()
        End If

        ' Fill spaces values
        If ClassParent.ClassParent.connect_ADO.ADODB_connection(1) Then
            rs = ClassParent.ClassParent.connect_ADO.ADODB_create_recordset(1, "SELECT * FROM LOCALISATIONS where Etat=1 order by Position")

            While Not rs.EOF
                Dim idSpace As Integer = rs.Fields("IDLocalisation").Value
                Dim spaceToAdd As New Space With {.id = idSpace, .name = rs.Fields("NomLocalisation").Value}

                ' The space is added to the spaces list
                address.spaces.Add(spaceToAdd)
                rs.MoveNext()
            End While
            rs.Close()
        End If

        ' Address is ready to be added to data (object ReportData)
        data.address = address

        ' Fill maintenance values
        Dim maintenance As New Maintenance()

        ' Fill natures values
        If ClassParent.ClassParent.connect_ADO.ADODB_connection(1) Then
            rs = ClassParent.ClassParent.connect_ADO.ADODB_create_recordset(1, "SELECT * FROM NATURES_OPERATIONS where Etat=1 order by Position")

            While Not rs.EOF
                Dim idNature As Integer = rs.Fields("IDNatureOperation").Value
                Dim natureToAdd As New Nature With {.id = idNature, .name = rs.Fields("NomNatureOperation").Value}

                ' The nature is added to the natures list
                maintenance.natures.Add(natureToAdd)
                rs.MoveNext()
            End While
            rs.Close()
        End If

        ' Fill frequencies values
        If ClassParent.ClassParent.connect_ADO.ADODB_connection(1) Then
            rs = ClassParent.ClassParent.connect_ADO.ADODB_create_recordset(1, "SELECT * FROM NATURES_EVENEMENTS where Etat=1 order by Position")

            While Not rs.EOF
                Dim idFrequency As Integer = rs.Fields("IDNatureEvenement").Value
                Dim frequencyToAdd As New Frequency With {.id = idFrequency, .name = rs.Fields("NomNatureEvenement").Value}

                ' The frequency is added to the frequencies list
                maintenance.frequencies.Add(frequencyToAdd)
                rs.MoveNext()
            End While
            rs.Close()
        End If

        ' Fill materials values
        If ClassParent.ClassParent.connect_ADO.ADODB_connection(1) Then
            rs = ClassParent.ClassParent.connect_ADO.ADODB_create_recordset(1, "SELECT * FROM MATERIELS WHERE Etat=1 order by Position")

            While Not rs.EOF
                Dim idMaterial As Integer = rs.Fields("IDMateriel").Value
                Dim nameMaterial As String = rs.Fields("NomMateriel").Value

                Dim rs2 As ADODB.Recordset = ClassParent.ClassParent.connect_ADO.ADODB_create_recordset(1, "SELECT IDIncident FROM INCIDENTSGM where IDMaterielGM=" & idMaterial & " and etat=1 order by Position")
                Dim incidents(rs2.RecordCount - 1) As Integer

                For j As Integer = 0 To rs2.RecordCount - 1
                    incidents(j) = rs2.Fields("IDIncident").Value
                    rs2.MoveNext()
                Next
                rs2.Close()

                ' Material to add and its incidents
                Dim materialToAdd As New Material With {.id = idMaterial, .name = nameMaterial, .incidents = incidents}

                ' Material is added to the list of materials
                maintenance.materials.Add(materialToAdd)
                rs.MoveNext()
            End While
            rs.Close()
        End If

        ' Maintenance is ready to be added to data
        data.maintenance = maintenance

        ' Fill serviceCat values
        If ClassParent.ClassParent.connect_ADO.ADODB_connection(1) Then
            rs = ClassParent.ClassParent.connect_ADO.ADODB_create_recordset(1, "SELECT * FROM CATEGORIES_SERVICES WHERE Etat=1 and IDCategorieService <= 2 order by Position")

            While Not rs.EOF
                Dim idServiceCat As Integer = rs.Fields("IDCategorieService").Value
                Dim nameServiceCat As String = rs.Fields("NomCategorieService").Value

                Dim rs2 As ADODB.Recordset = ClassParent.ClassParent.connect_ADO.ADODB_create_recordset(1, "SELECT * FROM SERVICES where IDCategorieService=" & idServiceCat & " and etat=1 order by Position")
                Dim services As New List(Of Service)

                While Not rs2.EOF
                    Dim idService As Integer = rs2.Fields("IDService").Value
                    Dim nameService As String = rs2.Fields("NomService").Value
                    Dim service As New Service With {.id = idService, .name = nameService}
                    services.Add(service)

                    rs2.MoveNext()
                End While
                rs2.Close()

                ' ServiceCat to add and its services
                Dim serviceCatToAdd As New ServiceCat With {.id = idServiceCat, .name = nameServiceCat, .services = services}

                ' ServiceCat is added to the list of ServiceCats
                data.serviceCats.Add(serviceCatToAdd)
                rs.MoveNext()
            End While
            rs.Close()
        End If

        ' Fill incidents values
        If ClassParent.ClassParent.connect_ADO.ADODB_connection(1) Then
            rs = ClassParent.ClassParent.connect_ADO.ADODB_create_recordset(1, "SELECT * FROM INCIDENTSGM where Etat=1 order by Position")

            While Not rs.EOF
                Dim idIncident As Integer = rs.Fields("IDIncident").Value
                Dim incidentToAdd As New Incident With {.id = idIncident, .name = rs.Fields("NomIncident").Value}

                ' The incident is added to the incidents list
                data.incidents.Add(incidentToAdd)
                rs.MoveNext()
            End While
            rs.Close()
        End If

        ' Fill details values
        For i As Integer = 1 To 6
            Dim title As String
            Dim answers() As String

            If ClassParent.ClassParent.connect_ADO.ADODB_connection(1) Then
                rs = ClassParent.ClassParent.connect_ADO.ADODB_create_recordset(1, "SELECT * FROM LANGUE where IdObjet='RAP_ADMIN' and IdLangue='LBL_DETAIL' and Type=" & i)

                title = rs.Fields("Libelle").Value & ""

                ' If there is a title then there are answers, it'll be add to the details list
                If title & "" <> "" Then
                    Dim rs2 As ADODB.Recordset = ClassParent.ClassParent.connect_ADO.ADODB_create_recordset(1, "SELECT * FROM LANGUE where IdObjet='RAP_ADMIN' and IdLangue='COMBOBOX_DETAIL' and Type=" & i)
                    answers = rs2.Fields("Libelle").Value.ToString.Split(";")
                    rs2.Close()

                    Dim detailToAdd As New Detail With {.title = title, .answers = answers}

                    ' The detail is added to the details list
                    data.details.Add(detailToAdd)
                End If

                rs.Close()
            End If
        Next

        Dim dataJson As String
        dataJson = objSerializer.Serialize(data)

        ' Save of the file
        ' File.WriteAllText("C:\Temp\data_transfer.json", dataJson)
        ' Save file to path in xml
        File.WriteAllText(path & "\data_transfer.json", dataJson)

        ' Exportation confirmation
        DevExpress.XtraEditors.XtraMessageBox.Show(ClassParent.ClassParent.ListeMessages("MESS_EXPORT_SUCCESS").Libelle, Application.ProductName, MessageBoxButtons.OK)
    End Sub
End Class

Public Class Report
    Public Property idTeam As Integer
    Public Property idUser As Integer
    Public Property idZone As Integer
    Public Property idBuilding As Integer
    Public Property idLevel As Integer
    Public Property idCanton As Integer
    Public Property pillar As String
    Public Property idSpaceCat As Integer
    Public Property idSpace As Integer
    Public Property idNature As Integer
    Public Property idFrequency As Integer
    Public Property idMaterial As Integer
    Public Property code As String
    Public Property startDate As String
    Public Property startTime As String
    Public Property endDate As String
    Public Property endTime As String
    Public Property comment As String
    Public Property serviceCat As New List(Of ServicesId)
    Public Property participants As New List(Of Participant)
    Public Property incidents As New List(Of IncidentId)
    Public Property details As New List(Of DetailComplete)
End Class

Public Class ServicesId
    Public Property idServiceCat As Integer
    Public Property idServices As Integer()
End Class

Public Class Participant
    Public Property idTeam As Integer
    Public Property idParticipant As Integer
End Class

Public Class IncidentId
    Public Property idIncident As Integer
    Public Property nbAnomaly As Integer
End Class

Public Class DetailComplete
    Public Property title As String
    Public Property answer As String
End Class

Public Class Label
    Public Property team As String
    Public Property user As String
    Public Property address As String
    Public Property zone As String
    Public Property building As String
    Public Property level As String
    Public Property canton As String
    Public Property pillar As String
    Public Property spaceCat As String
    Public Property space As String
    Public Property maintenance As String
    Public Property nature As String
    Public Property frequency As String
    Public Property material As String
    Public Property code As String
    Public Property startDate As String
    Public Property endDate As String
    Public Property services As String
    Public Property teams As String
    Public Property participant As String
    Public Property participants As String
    Public Property incident As String
    Public Property comment As String
    Public Property details As String
    Public Property save As String
    Public Property draft As String
    Public Property cancel As String
End Class

Public Class ReportData
    Public Property local As Integer
    Public Property teams As New List(Of Team)
    Public Property users As New List(Of User)
    Public Property address As Address
    Public Property maintenance As Maintenance
    Public Property serviceCats As New List(Of ServiceCat)
    Public Property incidents As New List(Of Incident)
    Public Property details As New List(Of Detail)
End Class

Public Class Team
    Public Property id As Integer
    Public Property name As String
    Public Property members As Integer()
End Class

Public Class User
    Public Property id As Integer
    Public Property lastname As String
    Public Property firstname As String
End Class

Public Class Address
    Public Property zones As New List(Of Zone)
    Public Property buildings As New List(Of Building)
    Public Property levels As New List(Of Level)
    Public Property cantons As New List(Of Canton)
    Public Property spaceCats As New List(Of SpaceCat)
    Public Property spaces As New List(Of Space)
End Class

Public Class Maintenance
    Public Property natures As New List(Of Nature)
    Public Property frequencies As New List(Of Frequency)
    Public Property materials As New List(Of Material)
End Class

Public Class Zone
    Public Property id As Integer
    Public Property name As String
    Public Property buildings As Integer()
End Class

Public Class Building
    Public Property id As Integer
    Public Property name As String
    Public Property levels As Integer()
End Class

Public Class Level
    Public Property id As Integer
    Public Property name As String
End Class

Public Class Canton
    Public Property id As Integer
    Public Property name As String
End Class

Public Class SpaceCat
    Public Property id As Integer
    Public Property name As String
    Public Property spaces As Integer()
End Class

Public Class Space
    Public Property id As Integer
    Public Property name As String
End Class

Public Class Nature
    Public Property id As Integer
    Public Property name As String
End Class

Public Class Frequency
    Public Property id As Integer
    Public Property name As String
End Class

Public Class Material
    Public Property id As Integer
    Public Property name As String
    Public Property incidents As Integer()
End Class

Public Class ServiceCat
    Public Property id As Integer
    Public Property name As String
    Public Property services As New List(Of Service)
End Class

Public Class Service
    Public Property id As Integer
    Public Property name As String
End Class

Public Class Incident
    Public Property id As Integer
    Public Property name As String
End Class

Public Class Detail
    Public Property title As String
    Public Property answers As String()
End Class