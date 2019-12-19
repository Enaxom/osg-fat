Imports System
Imports System.Collections
Imports System.Collections.Specialized

Friend Class clsMessages
    Inherits NameObjectCollectionBase 'heritage de la collection

    ' Appel de Item avec un index.
    Default Public Property Item(ByVal index As Integer) As clsMessage
        Get
            Return Me.BaseGet(index)
        End Get
        Set(ByVal value As clsMessage)
            Me.BaseSet(index, value)
        End Set
    End Property

    Default Public Property Item(ByVal key As String) As clsMessage
        Get
            Return Me.BaseGet(key)
        End Get
        Set(ByVal Value As clsMessage)
            Me.BaseSet(key, Value)
        End Set
    End Property

    ' Ajouter une entrée à la collection
    Public Sub Add(ByVal key As String, ByVal value As clsMessage)
        Me.BaseAdd(key, value)
    End Sub

    ' supprime une entrée sur un index.
    Public Overloads Sub Remove(ByVal index As Integer)
        Me.BaseRemoveAt(index)
    End Sub

    ' Efface tous les éléments.
    Public Sub Clear()
        Me.BaseClear()
    End Sub
End Class



