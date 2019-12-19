<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmTablette
    Inherits DevExpress.XtraEditors.XtraForm

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        If disposing AndAlso components IsNot Nothing Then
            components.Dispose()
        End If
        MyBase.Dispose(disposing)
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmTablette))
        Me.btnFermer = New DevExpress.XtraEditors.SimpleButton()
        Me.btn_export = New DevExpress.XtraEditors.SimpleButton()
        Me.btn_import = New DevExpress.XtraEditors.SimpleButton()
        Me.SuspendLayout()
        '
        'btnFermer
        '
        Me.btnFermer.Appearance.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnFermer.Appearance.Options.UseFont = True
        Me.btnFermer.Location = New System.Drawing.Point(250, 358)
        Me.btnFermer.Name = "btnFermer"
        Me.btnFermer.Size = New System.Drawing.Size(97, 39)
        Me.btnFermer.TabIndex = 3
        Me.btnFermer.Text = "SimpleButton1"
        '
        'btn_export
        '
        Me.btn_export.Appearance.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btn_export.Appearance.Options.UseFont = True
        Me.btn_export.Location = New System.Drawing.Point(124, 218)
        Me.btn_export.Name = "btn_export"
        Me.btn_export.Size = New System.Drawing.Size(358, 86)
        Me.btn_export.TabIndex = 4
        Me.btn_export.Text = "Exporter les données"
        '
        'btn_import
        '
        Me.btn_import.Appearance.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btn_import.Appearance.Options.UseFont = True
        Me.btn_import.Location = New System.Drawing.Point(124, 77)
        Me.btn_import.Name = "btn_import"
        Me.btn_import.Size = New System.Drawing.Size(358, 83)
        Me.btn_import.TabIndex = 5
        Me.btn_import.Text = "Importer les données"
        '
        'frmTablette
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(593, 409)
        Me.Controls.Add(Me.btn_import)
        Me.Controls.Add(Me.btn_export)
        Me.Controls.Add(Me.btnFermer)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmTablette"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "frmListeDispos"
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents btnFermer As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents btn_export As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents btn_import As DevExpress.XtraEditors.SimpleButton
End Class
