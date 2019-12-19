Class utilisateur

    Private _prenom As String
    Private _nom As String
    Private _idUtilisateur As Integer
    Private _idDispo As Integer
    Private _dispo As String

    Sub New()
        _prenom = ""
        _nom = ""
        _idUtilisateur = -1
        _idDispo = -1

    End Sub

    Sub New(ByVal prenom As String, ByVal nom As String, ByVal idUtilisateur As Integer, ByVal idDispo As Integer)

        _prenom = prenom
        _nom = nom
        _idUtilisateur = idUtilisateur
        _idDispo = idDispo

    End Sub

    Public Property dispo As String
        Get
            Return _dispo
        End Get
        Set(ByVal value As String)
            _dispo = value
        End Set
    End Property

    Public Property nom() As String
        Get
            Return _nom
        End Get
        Set(ByVal value As String)
            _nom = value
        End Set
    End Property

    Public Property prenom() As String
        Get
            Return _prenom
        End Get
        Set(ByVal value As String)
            _prenom = value
        End Set
    End Property

    Public Property idUtilisateur() As Integer
        Get
            Return _idUtilisateur
        End Get
        Set(ByVal value As Integer)
            _idUtilisateur = value
        End Set
    End Property

    Public Property idDispo() As Integer
        Get
            Return _idDispo
        End Get
        Set(ByVal value As Integer)
            _idDispo = value
        End Set
    End Property



    Public ReadOnly Property Libelle() As String
        Get
            Return UCase(_nom) & " " & _prenom
        End Get

    End Property

End Class


