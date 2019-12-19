Imports System.Windows.Forms

Public Class comXML
    Private SQLIni As New System.Xml.XmlDocument
    Private NOMIni As New System.Xml.XmlDocument

    Public Sub New(ByVal fichier As String)

        If Not System.IO.File.Exists(Application.StartupPath & "\osgdata.xml") Then
            DevExpress.XtraEditors.XtraMessageBox.Show("Aucune configuration !!!", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Else
            NOMIni.Load(Application.StartupPath & "\osgdata.xml")
        End If

        If Not (System.IO.File.Exists(Application.StartupPath & "\" & fichier)) Then
            DevExpress.XtraEditors.XtraMessageBox.Show(GetCle("MESSAGE", "NOCFGBDD"), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Else
            SQLIni.Load(Application.StartupPath & "\" & fichier)
        End If

    End Sub



    Public Function GetCle(ByVal NomFrm As String, ByVal nomCle As String) As String
        Dim Element As System.Xml.XmlNodeList
        Element = SQLIni.GetElementsByTagName(NomFrm)
        On Error Resume Next

        If IsNothing(Element.Item(0).Item(nomCle)) Then
            Return ""
        Else
            'si le noeud est vide
            If IsNothing(Element.Item(0).Item(nomCle).LastChild) Then
                Return ""
            Else
                Return Element.Item(0).Item(nomCle).LastChild.Value
            End If
        End If
    End Function
End Class
