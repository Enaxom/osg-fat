<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmListeDispos
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmListeDispos))
        Me.GridEvent = New DevExpress.XtraGrid.GridControl()
        Me.GridView1 = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.CLN_Libelle = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.CLN_Dispo = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.CLN_Libelle_dispo = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.Picture_Event = New DevExpress.XtraEditors.Repository.RepositoryItemPictureEdit()
        Me.btnFermer = New DevExpress.XtraEditors.SimpleButton()
        Me.lblDispo = New DevExpress.XtraEditors.LabelControl()
        Me.cboFiltreDispo = New DevExpress.XtraEditors.ComboBoxEdit()
        CType(Me.GridEvent, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Picture_Event, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cboFiltreDispo.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'GridEvent
        '
        Me.GridEvent.Location = New System.Drawing.Point(12, 38)
        Me.GridEvent.MainView = Me.GridView1
        Me.GridEvent.Name = "GridEvent"
        Me.GridEvent.RepositoryItems.AddRange(New DevExpress.XtraEditors.Repository.RepositoryItem() {Me.Picture_Event})
        Me.GridEvent.Size = New System.Drawing.Size(570, 314)
        Me.GridEvent.TabIndex = 2
        Me.GridEvent.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.GridView1})
        '
        'GridView1
        '
        Me.GridView1.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.CLN_Libelle, Me.CLN_Dispo, Me.CLN_Libelle_dispo})
        Me.GridView1.GridControl = Me.GridEvent
        Me.GridView1.Name = "GridView1"
        Me.GridView1.OptionsBehavior.AllowAddRows = DevExpress.Utils.DefaultBoolean.[False]
        Me.GridView1.OptionsBehavior.AllowDeleteRows = DevExpress.Utils.DefaultBoolean.[False]
        Me.GridView1.OptionsBehavior.Editable = False
        Me.GridView1.OptionsBehavior.ReadOnly = True
        Me.GridView1.OptionsCustomization.AllowFilter = False
        Me.GridView1.OptionsMenu.EnableColumnMenu = False
        Me.GridView1.OptionsMenu.EnableFooterMenu = False
        Me.GridView1.OptionsMenu.EnableGroupPanelMenu = False
        Me.GridView1.OptionsSelection.EnableAppearanceFocusedCell = False
        Me.GridView1.OptionsSelection.EnableAppearanceFocusedRow = False
        Me.GridView1.OptionsView.ShowGroupPanel = False
        Me.GridView1.OptionsView.ShowIndicator = False
        '
        'CLN_Libelle
        '
        Me.CLN_Libelle.AppearanceCell.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CLN_Libelle.AppearanceCell.Options.UseFont = True
        Me.CLN_Libelle.FieldName = "Libelle"
        Me.CLN_Libelle.Name = "CLN_Libelle"
        Me.CLN_Libelle.OptionsColumn.ShowCaption = False
        Me.CLN_Libelle.Visible = True
        Me.CLN_Libelle.VisibleIndex = 0
        Me.CLN_Libelle.Width = 285
        '
        'CLN_Dispo
        '
        Me.CLN_Dispo.FieldName = "idDispo"
        Me.CLN_Dispo.Name = "CLN_Dispo"
        '
        'CLN_Libelle_dispo
        '
        Me.CLN_Libelle_dispo.AppearanceCell.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CLN_Libelle_dispo.AppearanceCell.Options.UseFont = True
        Me.CLN_Libelle_dispo.Caption = "GridColumn1"
        Me.CLN_Libelle_dispo.FieldName = "dispo"
        Me.CLN_Libelle_dispo.Name = "CLN_Libelle_dispo"
        Me.CLN_Libelle_dispo.OptionsColumn.ShowCaption = False
        Me.CLN_Libelle_dispo.Visible = True
        Me.CLN_Libelle_dispo.VisibleIndex = 1
        Me.CLN_Libelle_dispo.Width = 285
        '
        'Picture_Event
        '
        Me.Picture_Event.Name = "Picture_Event"
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
        'lblDispo
        '
        Me.lblDispo.Appearance.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblDispo.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.lblDispo.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.lblDispo.Location = New System.Drawing.Point(53, 12)
        Me.lblDispo.Name = "lblDispo"
        Me.lblDispo.Size = New System.Drawing.Size(199, 16)
        Me.lblDispo.TabIndex = 38
        Me.lblDispo.Text = "LabelControl1"
        '
        'cboFiltreDispo
        '
        Me.cboFiltreDispo.EditValue = ""
        Me.cboFiltreDispo.Location = New System.Drawing.Point(258, 9)
        Me.cboFiltreDispo.Name = "cboFiltreDispo"
        Me.cboFiltreDispo.Properties.Appearance.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cboFiltreDispo.Properties.Appearance.Options.UseFont = True
        Me.cboFiltreDispo.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.cboFiltreDispo.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor
        Me.cboFiltreDispo.Size = New System.Drawing.Size(218, 22)
        Me.cboFiltreDispo.TabIndex = 1
        '
        'frmListeDispos
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(593, 409)
        Me.Controls.Add(Me.lblDispo)
        Me.Controls.Add(Me.cboFiltreDispo)
        Me.Controls.Add(Me.btnFermer)
        Me.Controls.Add(Me.GridEvent)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmListeDispos"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "frmListeDispos"
        CType(Me.GridEvent, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GridView1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Picture_Event, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cboFiltreDispo.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents GridEvent As DevExpress.XtraGrid.GridControl
    Friend WithEvents GridView1 As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents CLN_Libelle As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents Picture_Event As DevExpress.XtraEditors.Repository.RepositoryItemPictureEdit
    Friend WithEvents btnFermer As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents CLN_Dispo As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents lblDispo As DevExpress.XtraEditors.LabelControl
    Friend WithEvents cboFiltreDispo As DevExpress.XtraEditors.ComboBoxEdit
    Friend WithEvents CLN_Libelle_dispo As DevExpress.XtraGrid.Columns.GridColumn
End Class
