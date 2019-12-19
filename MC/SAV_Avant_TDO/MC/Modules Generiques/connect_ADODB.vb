Imports System.Windows.Forms
Imports ADODB

Class connect_ADODB
    Private xml As comXML
    Private _connect_string1 As String
    Private _est_connecte1 As Boolean = False
    Private cn1 As ADODB.Connection
    Private _connect_string2 As String
    Private _est_connecte2 As Boolean = False
    Private cn2 As ADODB.Connection
    Private _connect_string3 As String
    Private _est_connecte3 As Boolean = False
    Private cn3 As ADODB.Connection

    Private _BDDOSGRIM As String
    Private _BDDGESTION As String
    Private _BDDFORMATION As String
    Private _UserID As String
    Private _UserPWD As String

    Private cmd As ADODB.Command
    Private _Mode As Integer
    'Chaine de retour du decodage
    Const Chaine1 = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789abcdefghijklmnopqrstuvwxyz&é'(-è_çà)=+°] [{}#~ù$¤£*!:;,.?<>%µ§"
    'Chaine crypter dans fichier ini
    Const Chaine2 = "NfIoLnsYWjtHGg95c1iP=ù$¤£UzSAd3CbrQqDX&é'(-+°][{}#~è_çà)*!:;,.?<>%µ§0hMOJk7lE4yRwa2uK mTVvZexpBF86"

    Public ReadOnly Property BDDOSGRIM As String
        Get
            Return _BDDOSGRIM
        End Get
    End Property

    Public ReadOnly Property BDDGESTION As String
        Get
            Return _BDDGESTION
        End Get
    End Property

    Public ReadOnly Property BDDFORMATION As String
        Get
            Return _BDDFORMATION
        End Get
    End Property

    Public ReadOnly Property UserID As String
        Get
            Return _UserID
        End Get
    End Property

    Public ReadOnly Property UserPWD As String
        Get
            Return _UserPWD
        End Get
    End Property

    Public ReadOnly Property Mode As String
        Get
            Return _Mode
        End Get
    End Property

    Public Function Init(ByVal SQLXML As String) As Boolean


        If Not System.IO.File.Exists(Application.StartupPath & "\" & SQLXML) Then
            DevExpress.XtraEditors.XtraMessageBox.Show("Erreur SQL XML", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return False
        Else
            xml = New comXML(SQLXML)
        End If
        Try
            _BDDOSGRIM = xml.GetCle("SYSTEME", "OSGRIM")
            _BDDGESTION = xml.GetCle("SYSTEME", "GESTION")
            _BDDFORMATION = xml.GetCle("SYSTEME", "FORMATION")

            _Mode = CType(New String(xml.GetCle("SYSTEME", "Mode")), Integer)
            Select Case (_Mode)
                Case 1 'bd acces
                    _connect_string1 = "DSN=" & _BDDOSGRIM & ";Uid=;Pwd=OSGOLS2003"
                    _connect_string2 = "DSN=" & _BDDGESTION & ";Uid=;Pwd=OSGOLS2003"
                    _connect_string3 = "DSN=" & _BDDFORMATION & ";Uid=;Pwd=OSGOLS2003"
                    _UserID = ""
                    _UserPWD = "OSGOLS2003"
                Case 2 'SQl avec login
                    _UserID = DeCodage(xml.GetCle("SYSTEME", "Uid"))
                    _UserPWD = DeCodage(xml.GetCle("SYSTEME", "Pwd"))
                    _connect_string1 = "DSN=" & _BDDOSGRIM & ";Uid=" & _UserID & ";Pwd=" & _UserPWD
                Case 3 'SQL Windows NT Auth
                    _connect_string1 = "DSN=" & _BDDOSGRIM & ";Uid=;Pwd="
                    _UserID = ""
                    _UserPWD = ""
            End Select
            Return True
        Catch ex As Exception
            DevExpress.XtraEditors.XtraMessageBox.Show(xml.GetCle("SYSTEME", "NOCONNECTBDD") & vbNewLine & ex.Message, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return False
        End Try
        Return True
    End Function

    Public Function ADODB_connection(ByVal TypeBdd As Integer) As Boolean
        Dim chaine_connect As String = ""

        If _Mode = 1 Then 'TRAITEMENT SOUS ACCESS AVEC BDD DIFFERENTES
            Select Case TypeBdd
                Case 1 : chaine_connect = _connect_string1
                Case 2 : chaine_connect = _connect_string2
                Case 3 : chaine_connect = _connect_string3
            End Select
        Else
            chaine_connect = _connect_string1
        End If

        If Not ADODB_EstConnecte(TypeBdd) Then
            Try
                If _Mode = 1 Then 'TRAITEMENT SOUS ACCESS AVEC BDD DIFFERENTES
                    Select Case TypeBdd
                        Case 1
                            cn1 = New ADODB.Connection
                            cn1.CursorLocation = CursorLocationEnum.adUseServer
                            cn1.Open(chaine_connect)
                            _est_connecte1 = True
                        Case 2
                            cn2 = New ADODB.Connection
                            cn2.CursorLocation = CursorLocationEnum.adUseServer
                            cn2.Open(chaine_connect)
                            _est_connecte2 = True
                        Case 3
                            cn3 = New ADODB.Connection
                            cn3.CursorLocation = CursorLocationEnum.adUseServer
                            cn3.Open(chaine_connect)
                            _est_connecte3 = True
                    End Select
                Else
                    cn1 = New ADODB.Connection
                    cn1.CursorLocation = CursorLocationEnum.adUseClient
                    cn1.Open(chaine_connect)
                    _est_connecte1 = True
                End If
                Return True
            Catch ex As Exception
                DevExpress.XtraEditors.XtraMessageBox.Show(xml.GetCle("SYSTEME", "NOCONNECTBDD") & " : " & ex.Message, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error)
                If _Mode = 1 Then 'TRAITEMENT SOUS ACCESS AVEC BDD DIFFERENTES
                    Select Case TypeBdd
                        Case 1 : _est_connecte1 = False
                        Case 2 : _est_connecte2 = False
                        Case 3 : _est_connecte3 = False
                    End Select
                Else
                    _est_connecte1 = False
                End If
                Return False
            End Try
        Else
            Return True
        End If
    End Function

    Public Sub ADODB_delete(ByVal TypeBdd As Integer, ByVal commande As String)

        Try
            cmd = New ADODB.Command
            If _Mode = 1 Then 'TRAITEMENT SOUS ACCESS AVEC BDD DIFFERENTES
                Select Case TypeBdd
                    Case 1 : cmd.ActiveConnection = cn1
                    Case 2 : cmd.ActiveConnection = cn2
                    Case 3 : cmd.ActiveConnection = cn3
                End Select
            Else
                commande = UCase(commande)
                commande = commande.Replace("=TRUE", "=1")
                commande = commande.Replace("= TRUE", "=1")
                commande = commande.Replace("=FALSE", "=0")
                commande = commande.Replace("= FALSE", "=0")
                cmd.ActiveConnection = cn1
            End If
            cmd.CommandText = commande
            cmd.Execute()
            cmd = Nothing
        Catch ex As Exception
            DevExpress.XtraEditors.XtraMessageBox.Show(Err.Description, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Public Function ADODB_create_recordset(ByVal TypeBdd As Integer, ByVal commande As String) As ADODB.Recordset
        Try

            cmd = New ADODB.Command
            Dim rs As ADODB.Recordset = New ADODB.Recordset
            If _Mode = 1 Then 'TRAITEMENT SOUS ACCESS AVEC BDD DIFFERENTES
                Select Case TypeBdd
                    Case 1 : cmd.ActiveConnection = cn1
                    Case 2 : cmd.ActiveConnection = cn2
                    Case 3 : cmd.ActiveConnection = cn3
                End Select
            Else
                commande = UCase(commande)
                commande = commande.Replace("=TRUE", "=1")
                commande = commande.Replace("= TRUE", "=1")
                commande = commande.Replace("=FALSE", "=0")
                commande = commande.Replace("= FALSE", "=0")
                cmd.ActiveConnection = cn1
            End If
            cmd.CommandText = commande
            cmd.CommandType = CommandTypeEnum.adCmdText
            rs.Open(cmd, , , LockTypeEnum.adLockOptimistic, )
            Return rs
        Catch ex As Exception
            Return Nothing
        End Try
    End Function
    Public Function ADODB_create_recordset_Table(ByVal TypeBdd As Integer, ByVal commande As String) As ADODB.Recordset
        Try

            cmd = New ADODB.Command
            Dim rs As ADODB.Recordset = New ADODB.Recordset
            If _Mode = 1 Then 'TRAITEMENT SOUS ACCESS AVEC BDD DIFFERENTES
                Select Case TypeBdd
                    Case 1 : cmd.ActiveConnection = cn1
                    Case 2 : cmd.ActiveConnection = cn2
                    Case 3 : cmd.ActiveConnection = cn3
                End Select
            Else
                commande = UCase(commande)
                commande = commande.Replace("=TRUE", "=1")
                commande = commande.Replace("= TRUE", "=1")
                commande = commande.Replace("=FALSE", "=0")
                commande = commande.Replace("= FALSE", "=0")
                cmd.ActiveConnection = cn1
            End If
            cmd.CommandText = commande
            cmd.CommandType = CommandTypeEnum.adCmdTable
            'cmd.
            rs.Open(cmd, , , LockTypeEnum.adLockOptimistic, )
            Return rs
        Catch ex As Exception
            Return Nothing
        End Try
    End Function
    Public Function Maj_space_work_begin(ByVal TypeBdd As Integer) As Boolean
        If ADODB_EstConnecte(TypeBdd) Then
            Try
                If _Mode = 1 Then 'TRAITEMENT SOUS ACCESS AVEC BDD DIFFERENTES
                    Select Case TypeBdd
                        Case 1
                            cn1.BeginTrans()
                        Case 2
                            cn2.BeginTrans()
                        Case 3
                            cn3.BeginTrans()
                    End Select
                Else
                    cn1.BeginTrans()
                End If
                Return True
            Catch ex As Exception
                Return False
            End Try
        End If
        Return False
    End Function

    Public Function Maj_space_work_end(ByVal TypeBdd As Integer) As Boolean
        If ADODB_EstConnecte(TypeBdd) Then
            Try
                If _Mode = 1 Then 'TRAITEMENT SOUS ACCESS AVEC BDD DIFFERENTES
                    Select Case TypeBdd
                        Case 1
                            cn1.RollbackTrans()
                        Case 2
                            cn2.RollbackTrans()
                        Case 3
                            cn3.RollbackTrans()
                    End Select
                Else
                    cn1.RollbackTrans()
                End If
                Return True
            Catch ex As Exception
                Return False
            End Try
        End If
        Return False
    End Function

    Public Function Maj_space_work_commit(ByVal TypeBdd As Integer) As Boolean
        If ADODB_EstConnecte(TypeBdd) Then
            Try
                If _Mode = 1 Then 'TRAITEMENT SOUS ACCESS AVEC BDD DIFFERENTES
                    Select Case TypeBdd
                        Case 1
                            cn1.CommitTrans()
                        Case 2
                            cn2.CommitTrans()
                        Case 3
                            cn3.CommitTrans()
                    End Select
                Else
                    cn1.CommitTrans()
                End If
                Return True
            Catch ex As Exception
                Return False
            End Try
        End If
        Return False
    End Function

    Public Sub ADODB_deconnection(ByVal TypeBdd As Integer)
        If ADODB_EstConnecte(TypeBdd) Then
            Try
                If _Mode = 1 Then 'TRAITEMENT SOUS ACCESS AVEC BDD DIFFERENTES
                    Select Case TypeBdd
                        Case 1
                            cn1.Close()
                            cn1 = Nothing
                            _est_connecte1 = False
                        Case 2
                            cn2.Close()
                            cn2 = Nothing
                            _est_connecte2 = False
                        Case 3
                            cn3.Close()
                            cn3 = Nothing
                            _est_connecte3 = False
                    End Select
                Else
                    cn1.Close()
                    cn1 = Nothing
                    _est_connecte1 = False
                End If
                System.GC.Collect()
            Catch ex As Exception
            End Try
        End If
    End Sub

    Public Function ADODB_EstConnecte(ByVal TypeBdd As Integer) As Boolean
        If _Mode = 1 Then 'TRAITEMENT SOUS ACCESS AVEC BDD DIFFERENTES
            Select Case TypeBdd
                Case 1 : Return _est_connecte1
                Case 2 : Return _est_connecte2
                Case 3 : Return _est_connecte3
            End Select
        Else
            Return _est_connecte1
        End If
        Return False
    End Function

    'Decodage de la chaine de caractere
    Private Function DeCodage(ByVal Chaine As String) As String
        Dim i As Integer
        DeCodage = ""
        For i = 1 To Len(Chaine)
            DeCodage = DeCodage & Mid(Chaine1, InStr(Chaine2, Mid(Chaine, i, 1)), 1)
        Next i
    End Function

    'Codage de la chaine de caractere
    Private Function Codage(ByVal Chaine As String) As String
        Dim i As Integer
        Codage = ""
        For i = 1 To Len(Chaine)
            Codage = Codage & Mid(Chaine2, InStr(Chaine1, Mid(Chaine, i, 1)), 1)
        Next i
    End Function

    Public Function mysql_escape_string(ByVal entreeSQL As String) As String
        Dim strClean As String = entreeSQL
        strClean = strClean.Replace("'", "''")
        Return strClean
    End Function
End Class
