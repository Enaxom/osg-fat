Public Class Data_Set
    Private _champ As String
    Private _id As Integer
    Private _nom As String

    Public Sub New()
        _champ = ""
        _nom = ""
        _id = 0
    End Sub

    Public Sub New(ByVal champ_val As String, ByVal nom_val As String, ByVal id_val As Integer)
        _champ = champ_val
        _id = id_val
        _nom = nom_val
    End Sub

    Public Sub New(ByVal champ_val As String, ByVal id_val As Integer)
        _champ = champ_val
        _id = id_val
    End Sub

    Public Property champ As String
        Get
            Return _champ
        End Get
        Set(ByVal value As String)
            _champ = value
        End Set
    End Property

    Public Property id As String
        Get
            Return _id
        End Get
        Set(ByVal value As String)
            _id = value
        End Set
    End Property

    Public Property nom As String
        Get
            Return _nom
        End Get
        Set(ByVal value As String)
            _nom = value
        End Set
    End Property

    Public Overrides Function ToString() As String
        Return _champ
    End Function




End Class
