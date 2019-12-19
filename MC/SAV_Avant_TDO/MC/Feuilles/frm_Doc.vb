Public Class frm_Doc
    Private ClassParent As frmEcran

    Public Sub New(ByVal parentClass As frmEcran, ByVal doc As doc.Doc)
        InitializeComponent()

        ClassParent = parentClass

        ClassParent.ClassParent.connect_ADO.ADODB_connection(1)
        Dim rs As ADODB.Recordset = ClassParent.ClassParent.connect_ADO.ADODB_create_recordset(1, "SELECT * FROM LANGUE WHERE IdObjet='ADM_DOC' AND IdLangue='LINK'")
        Me.Text = rs.Fields("Libelle").Value
        rs.Close()

        rs = ClassParent.ClassParent.connect_ADO.ADODB_create_recordset(1, "SELECT * FROM LANGUE WHERE IdObjet='GENERIQUE' AND IdLangue='BTN_FERMER'")
        btnFermer.Text = rs.Fields("Libelle").Value
        rs.Close()

        PanelControl1.Controls.Add(doc)
        doc.Dock = Windows.Forms.DockStyle.Fill

    End Sub

    Private Sub btnFermer_Click(sender As System.Object, e As System.EventArgs) Handles btnFermer.Click
        Me.Close()
    End Sub

    Private Sub frm_Doc_Resize(sender As Object, e As System.EventArgs) Handles Me.Resize
        PanelControl1.Height = Me.Height - 97
        btnFermer.Top = PanelControl1.Height + 10
        btnFermer.Left = (Me.Width / 2) - 50
    End Sub
End Class