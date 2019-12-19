Friend Class clsMessage
    Private _Libelle As String
    Private _Id As String

    Public Property Libelle() As String
        Get
            Return _Libelle
        End Get
        Set(ByVal value As String)
            _Libelle = value
        End Set
    End Property
    Public Property Cle() As String
        Get
            Return _Id
        End Get
        Set(ByVal value As String)
            _Id = value
        End Set
    End Property
    Sub Clear()
        _Libelle = ""
        _Id = ""
    End Sub
End Class




