<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmAffHisto
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
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmAffHisto))
        Me.GrpHisto = New DevExpress.XtraEditors.GroupControl()
        Me.TxtHeureF = New DevExpress.XtraEditors.TextEdit()
        Me.BtnREch = New DevExpress.XtraEditors.SimpleButton()
        Me.TxtHeureD = New DevExpress.XtraEditors.TextEdit()
        Me.TxtDateFin = New DevExpress.XtraEditors.DateEdit()
        Me.TxtDateDeb = New DevExpress.XtraEditors.DateEdit()
        Me.LblDateF = New DevExpress.XtraEditors.LabelControl()
        Me.LblHeureF = New DevExpress.XtraEditors.LabelControl()
        Me.LblHeureD = New DevExpress.XtraEditors.LabelControl()
        Me.LblDateD = New DevExpress.XtraEditors.LabelControl()
        Me.GrpFiltre = New DevExpress.XtraEditors.GroupControl()
        Me.BtnRechercheA = New DevExpress.XtraEditors.SimpleButton()
        Me.LblCheck = New DevExpress.XtraEditors.CheckEdit()
        Me.cboFiltre = New DevExpress.XtraEditors.ComboBoxEdit()
        Me.GrpAffHisto = New DevExpress.XtraEditors.GroupControl()
        Me.GridEvent = New DevExpress.XtraGrid.GridControl()
        Me.GridView2 = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.CLN_DATE = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.CLN_HEURE = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.CLN_Bat = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.CLN_TITRE = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.CLN_OPERATEUR = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.Column_EstSupp = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridView1 = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.BtnImpListe = New DevExpress.XtraEditors.SimpleButton()
        Me.BtnFermer = New DevExpress.XtraEditors.SimpleButton()
        Me.PrintingSystem1 = New DevExpress.XtraPrinting.PrintingSystem(Me.components)
        Me.PrintableComponentLink1 = New DevExpress.XtraPrinting.PrintableComponentLink(Me.components)
        CType(Me.GrpHisto, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GrpHisto.SuspendLayout()
        CType(Me.TxtHeureF.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.TxtHeureD.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.TxtDateFin.Properties.VistaTimeProperties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.TxtDateFin.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.TxtDateDeb.Properties.VistaTimeProperties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.TxtDateDeb.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GrpFiltre, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GrpFiltre.SuspendLayout()
        CType(Me.LblCheck.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cboFiltre.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GrpAffHisto, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GrpAffHisto.SuspendLayout()
        CType(Me.GridEvent, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GridView2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PrintingSystem1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PrintableComponentLink1.ImageCollection, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'GrpHisto
        '
        Me.GrpHisto.AppearanceCaption.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GrpHisto.AppearanceCaption.Options.UseFont = True
        Me.GrpHisto.Controls.Add(Me.TxtHeureF)
        Me.GrpHisto.Controls.Add(Me.BtnREch)
        Me.GrpHisto.Controls.Add(Me.TxtHeureD)
        Me.GrpHisto.Controls.Add(Me.TxtDateFin)
        Me.GrpHisto.Controls.Add(Me.TxtDateDeb)
        Me.GrpHisto.Controls.Add(Me.LblDateF)
        Me.GrpHisto.Controls.Add(Me.LblHeureF)
        Me.GrpHisto.Controls.Add(Me.LblHeureD)
        Me.GrpHisto.Controls.Add(Me.LblDateD)
        Me.GrpHisto.Location = New System.Drawing.Point(227, 12)
        Me.GrpHisto.Name = "GrpHisto"
        Me.GrpHisto.Size = New System.Drawing.Size(536, 121)
        Me.GrpHisto.TabIndex = 1
        Me.GrpHisto.Text = "GroupControl1"
        '
        'TxtHeureF
        '
        Me.TxtHeureF.Location = New System.Drawing.Point(431, 54)
        Me.TxtHeureF.Name = "TxtHeureF"
        Me.TxtHeureF.Properties.Appearance.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtHeureF.Properties.Appearance.Options.UseFont = True
        Me.TxtHeureF.Size = New System.Drawing.Size(100, 22)
        Me.TxtHeureF.TabIndex = 4
        '
        'BtnREch
        '
        Me.BtnREch.Appearance.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BtnREch.Appearance.Options.UseFont = True
        Me.BtnREch.Location = New System.Drawing.Point(175, 82)
        Me.BtnREch.Name = "BtnREch"
        Me.BtnREch.Size = New System.Drawing.Size(166, 34)
        Me.BtnREch.TabIndex = 5
        Me.BtnREch.Text = "SimpleButton1"
        '
        'TxtHeureD
        '
        Me.TxtHeureD.Location = New System.Drawing.Point(431, 26)
        Me.TxtHeureD.Name = "TxtHeureD"
        Me.TxtHeureD.Properties.Appearance.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtHeureD.Properties.Appearance.Options.UseFont = True
        Me.TxtHeureD.Size = New System.Drawing.Size(100, 22)
        Me.TxtHeureD.TabIndex = 2
        '
        'TxtDateFin
        '
        Me.TxtDateFin.EditValue = Nothing
        Me.TxtDateFin.Location = New System.Drawing.Point(175, 54)
        Me.TxtDateFin.Name = "TxtDateFin"
        Me.TxtDateFin.Properties.Appearance.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtDateFin.Properties.Appearance.Options.UseFont = True
        Me.TxtDateFin.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.TxtDateFin.Properties.VistaTimeProperties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton()})
        Me.TxtDateFin.Size = New System.Drawing.Size(100, 22)
        Me.TxtDateFin.TabIndex = 3
        '
        'TxtDateDeb
        '
        Me.TxtDateDeb.EditValue = Nothing
        Me.TxtDateDeb.Location = New System.Drawing.Point(175, 27)
        Me.TxtDateDeb.Name = "TxtDateDeb"
        Me.TxtDateDeb.Properties.Appearance.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtDateDeb.Properties.Appearance.Options.UseFont = True
        Me.TxtDateDeb.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.TxtDateDeb.Properties.VistaTimeProperties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton()})
        Me.TxtDateDeb.Size = New System.Drawing.Size(100, 22)
        Me.TxtDateDeb.TabIndex = 1
        '
        'LblDateF
        '
        Me.LblDateF.Appearance.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblDateF.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.LblDateF.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.LblDateF.Location = New System.Drawing.Point(5, 57)
        Me.LblDateF.Name = "LblDateF"
        Me.LblDateF.Size = New System.Drawing.Size(164, 16)
        Me.LblDateF.TabIndex = 11
        Me.LblDateF.Text = "LabelControl1"
        '
        'LblHeureF
        '
        Me.LblHeureF.Appearance.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblHeureF.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.LblHeureF.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.LblHeureF.Location = New System.Drawing.Point(281, 57)
        Me.LblHeureF.Name = "LblHeureF"
        Me.LblHeureF.Size = New System.Drawing.Size(144, 16)
        Me.LblHeureF.TabIndex = 10
        Me.LblHeureF.Text = "LabelControl1"
        '
        'LblHeureD
        '
        Me.LblHeureD.Appearance.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblHeureD.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.LblHeureD.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.LblHeureD.Location = New System.Drawing.Point(281, 29)
        Me.LblHeureD.Name = "LblHeureD"
        Me.LblHeureD.Size = New System.Drawing.Size(144, 16)
        Me.LblHeureD.TabIndex = 9
        Me.LblHeureD.Text = "LabelControl1"
        '
        'LblDateD
        '
        Me.LblDateD.Appearance.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblDateD.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.LblDateD.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.LblDateD.Location = New System.Drawing.Point(5, 29)
        Me.LblDateD.Name = "LblDateD"
        Me.LblDateD.Size = New System.Drawing.Size(164, 16)
        Me.LblDateD.TabIndex = 8
        Me.LblDateD.Text = "LabelControl1"
        '
        'GrpFiltre
        '
        Me.GrpFiltre.AppearanceCaption.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GrpFiltre.AppearanceCaption.Options.UseFont = True
        Me.GrpFiltre.Controls.Add(Me.BtnRechercheA)
        Me.GrpFiltre.Controls.Add(Me.LblCheck)
        Me.GrpFiltre.Controls.Add(Me.cboFiltre)
        Me.GrpFiltre.Location = New System.Drawing.Point(108, 139)
        Me.GrpFiltre.Name = "GrpFiltre"
        Me.GrpFiltre.Size = New System.Drawing.Size(791, 101)
        Me.GrpFiltre.TabIndex = 2
        Me.GrpFiltre.Text = "GroupControl1"
        '
        'BtnRechercheA
        '
        Me.BtnRechercheA.Appearance.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BtnRechercheA.Appearance.Options.UseFont = True
        Me.BtnRechercheA.Location = New System.Drawing.Point(294, 54)
        Me.BtnRechercheA.Name = "BtnRechercheA"
        Me.BtnRechercheA.Size = New System.Drawing.Size(166, 34)
        Me.BtnRechercheA.TabIndex = 8
        Me.BtnRechercheA.Text = "SimpleButton1"
        Me.BtnRechercheA.Visible = False
        '
        'LblCheck
        '
        Me.LblCheck.Location = New System.Drawing.Point(292, 27)
        Me.LblCheck.Name = "LblCheck"
        Me.LblCheck.Properties.Appearance.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblCheck.Properties.Appearance.Options.UseFont = True
        Me.LblCheck.Properties.Caption = "CheckEdit1"
        Me.LblCheck.Size = New System.Drawing.Size(494, 21)
        Me.LblCheck.TabIndex = 7
        '
        'cboFiltre
        '
        Me.cboFiltre.Location = New System.Drawing.Point(14, 27)
        Me.cboFiltre.Name = "cboFiltre"
        Me.cboFiltre.Properties.Appearance.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cboFiltre.Properties.Appearance.Options.UseFont = True
        Me.cboFiltre.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.cboFiltre.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor
        Me.cboFiltre.Size = New System.Drawing.Size(252, 22)
        Me.cboFiltre.TabIndex = 6
        '
        'GrpAffHisto
        '
        Me.GrpAffHisto.AppearanceCaption.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GrpAffHisto.AppearanceCaption.Options.UseFont = True
        Me.GrpAffHisto.Controls.Add(Me.GridEvent)
        Me.GrpAffHisto.Location = New System.Drawing.Point(12, 246)
        Me.GrpAffHisto.Name = "GrpAffHisto"
        Me.GrpAffHisto.Size = New System.Drawing.Size(992, 431)
        Me.GrpAffHisto.TabIndex = 3
        Me.GrpAffHisto.Text = "GroupControl2"
        '
        'GridEvent
        '
        Me.GridEvent.Location = New System.Drawing.Point(5, 27)
        Me.GridEvent.MainView = Me.GridView2
        Me.GridEvent.Name = "GridEvent"
        Me.GridEvent.Size = New System.Drawing.Size(982, 359)
        Me.GridEvent.TabIndex = 9
        Me.GridEvent.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.GridView2, Me.GridView1})
        '
        'GridView2
        '
        Me.GridView2.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.CLN_DATE, Me.CLN_HEURE, Me.CLN_Bat, Me.CLN_TITRE, Me.CLN_OPERATEUR, Me.Column_EstSupp})
        Me.GridView2.GridControl = Me.GridEvent
        Me.GridView2.Name = "GridView2"
        Me.GridView2.OptionsBehavior.ReadOnly = True
        Me.GridView2.OptionsCustomization.AllowColumnMoving = False
        Me.GridView2.OptionsCustomization.AllowColumnResizing = False
        Me.GridView2.OptionsCustomization.AllowFilter = False
        Me.GridView2.OptionsFilter.AllowFilterEditor = False
        Me.GridView2.OptionsMenu.EnableColumnMenu = False
        Me.GridView2.OptionsMenu.EnableFooterMenu = False
        Me.GridView2.OptionsMenu.EnableGroupPanelMenu = False
        Me.GridView2.OptionsPrint.PrintPreview = True
        Me.GridView2.OptionsView.ShowGroupPanel = False
        Me.GridView2.OptionsView.ShowIndicator = False
        '
        'CLN_DATE
        '
        Me.CLN_DATE.FieldName = "DateEvent"
        Me.CLN_DATE.Name = "CLN_DATE"
        Me.CLN_DATE.OptionsColumn.AllowEdit = False
        Me.CLN_DATE.OptionsColumn.AllowFocus = False
        Me.CLN_DATE.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
        Me.CLN_DATE.OptionsColumn.ReadOnly = True
        Me.CLN_DATE.Visible = True
        Me.CLN_DATE.VisibleIndex = 0
        '
        'CLN_HEURE
        '
        Me.CLN_HEURE.FieldName = "HeureEvent"
        Me.CLN_HEURE.Name = "CLN_HEURE"
        Me.CLN_HEURE.OptionsColumn.AllowEdit = False
        Me.CLN_HEURE.OptionsColumn.AllowFocus = False
        Me.CLN_HEURE.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
        Me.CLN_HEURE.OptionsColumn.ReadOnly = True
        Me.CLN_HEURE.Visible = True
        Me.CLN_HEURE.VisibleIndex = 1
        '
        'CLN_Bat
        '
        Me.CLN_Bat.Caption = "Bat"
        Me.CLN_Bat.FieldName = "Bat"
        Me.CLN_Bat.Name = "CLN_Bat"
        Me.CLN_Bat.OptionsColumn.AllowEdit = False
        Me.CLN_Bat.OptionsColumn.AllowFocus = False
        Me.CLN_Bat.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
        Me.CLN_Bat.OptionsColumn.ReadOnly = True
        Me.CLN_Bat.Visible = True
        Me.CLN_Bat.VisibleIndex = 2
        '
        'CLN_TITRE
        '
        Me.CLN_TITRE.FieldName = "Titre"
        Me.CLN_TITRE.Name = "CLN_TITRE"
        Me.CLN_TITRE.OptionsColumn.AllowEdit = False
        Me.CLN_TITRE.OptionsColumn.AllowFocus = False
        Me.CLN_TITRE.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
        Me.CLN_TITRE.OptionsColumn.ReadOnly = True
        Me.CLN_TITRE.Visible = True
        Me.CLN_TITRE.VisibleIndex = 3
        '
        'CLN_OPERATEUR
        '
        Me.CLN_OPERATEUR.FieldName = "Operateur"
        Me.CLN_OPERATEUR.Name = "CLN_OPERATEUR"
        Me.CLN_OPERATEUR.OptionsColumn.AllowEdit = False
        Me.CLN_OPERATEUR.OptionsColumn.AllowFocus = False
        Me.CLN_OPERATEUR.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
        Me.CLN_OPERATEUR.OptionsColumn.ReadOnly = True
        Me.CLN_OPERATEUR.Visible = True
        Me.CLN_OPERATEUR.VisibleIndex = 4
        '
        'Column_EstSupp
        '
        Me.Column_EstSupp.Caption = "GridColumn1"
        Me.Column_EstSupp.FieldName = "EstSupp"
        Me.Column_EstSupp.Name = "Column_EstSupp"
        '
        'GridView1
        '
        Me.GridView1.GridControl = Me.GridEvent
        Me.GridView1.Name = "GridView1"
        '
        'BtnImpListe
        '
        Me.BtnImpListe.Appearance.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BtnImpListe.Appearance.Options.UseFont = True
        Me.BtnImpListe.Location = New System.Drawing.Point(838, 688)
        Me.BtnImpListe.Name = "BtnImpListe"
        Me.BtnImpListe.Size = New System.Drawing.Size(166, 34)
        Me.BtnImpListe.TabIndex = 18
        Me.BtnImpListe.Text = "SimpleButton1"
        Me.BtnImpListe.Visible = False
        '
        'BtnFermer
        '
        Me.BtnFermer.Appearance.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BtnFermer.Appearance.Options.UseFont = True
        Me.BtnFermer.Location = New System.Drawing.Point(402, 688)
        Me.BtnFermer.Name = "BtnFermer"
        Me.BtnFermer.Size = New System.Drawing.Size(166, 34)
        Me.BtnFermer.TabIndex = 10
        Me.BtnFermer.Text = "SimpleButton1"
        '
        'PrintingSystem1
        '
        Me.PrintingSystem1.ExportOptions.PrintPreview.ShowOptionsBeforeExport = False
        Me.PrintingSystem1.Links.AddRange(New Object() {Me.PrintableComponentLink1})
        '
        'PrintableComponentLink1
        '
        Me.PrintableComponentLink1.Component = Me.GridEvent
        '
        '
        '
        Me.PrintableComponentLink1.ImageCollection.ImageStream = CType(resources.GetObject("PrintableComponentLink1.ImageCollection.ImageStream"), DevExpress.Utils.ImageCollectionStreamer)
        Me.PrintableComponentLink1.PaperName = "A4"
        Me.PrintableComponentLink1.PrintingSystem = Me.PrintingSystem1
        Me.PrintableComponentLink1.PrintingSystemBase = Me.PrintingSystem1
        Me.PrintableComponentLink1.VerticalContentSplitting = DevExpress.XtraPrinting.VerticalContentSplitting.Exact
        '
        'frmAffHisto
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1018, 736)
        Me.Controls.Add(Me.BtnFermer)
        Me.Controls.Add(Me.BtnImpListe)
        Me.Controls.Add(Me.GrpAffHisto)
        Me.Controls.Add(Me.GrpFiltre)
        Me.Controls.Add(Me.GrpHisto)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MinimizeBox = False
        Me.Name = "frmAffHisto"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "frmAffHisto"
        CType(Me.GrpHisto, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GrpHisto.ResumeLayout(False)
        CType(Me.TxtHeureF.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.TxtHeureD.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.TxtDateFin.Properties.VistaTimeProperties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.TxtDateFin.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.TxtDateDeb.Properties.VistaTimeProperties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.TxtDateDeb.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GrpFiltre, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GrpFiltre.ResumeLayout(False)
        CType(Me.LblCheck.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cboFiltre.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GrpAffHisto, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GrpAffHisto.ResumeLayout(False)
        CType(Me.GridEvent, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GridView2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GridView1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PrintingSystem1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PrintableComponentLink1.ImageCollection, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents GrpHisto As DevExpress.XtraEditors.GroupControl
    Friend WithEvents GrpFiltre As DevExpress.XtraEditors.GroupControl
    Friend WithEvents GrpAffHisto As DevExpress.XtraEditors.GroupControl
    Friend WithEvents BtnREch As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents TxtDateFin As DevExpress.XtraEditors.DateEdit
    Friend WithEvents TxtDateDeb As DevExpress.XtraEditors.DateEdit
    Friend WithEvents LblDateF As DevExpress.XtraEditors.LabelControl
    Friend WithEvents LblHeureF As DevExpress.XtraEditors.LabelControl
    Friend WithEvents LblHeureD As DevExpress.XtraEditors.LabelControl
    Friend WithEvents LblDateD As DevExpress.XtraEditors.LabelControl
    Friend WithEvents LblCheck As DevExpress.XtraEditors.CheckEdit
    Friend WithEvents cboFiltre As DevExpress.XtraEditors.ComboBoxEdit
    Friend WithEvents BtnRechercheA As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents BtnImpListe As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents BtnFermer As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents GridEvent As DevExpress.XtraGrid.GridControl
    Friend WithEvents GridView2 As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents CLN_DATE As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents CLN_HEURE As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents CLN_TITRE As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents CLN_OPERATEUR As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents TxtHeureF As DevExpress.XtraEditors.TextEdit
    Friend WithEvents TxtHeureD As DevExpress.XtraEditors.TextEdit
    Friend WithEvents PrintingSystem1 As DevExpress.XtraPrinting.PrintingSystem
    Friend WithEvents PrintableComponentLink1 As DevExpress.XtraPrinting.PrintableComponentLink
    Friend WithEvents Column_EstSupp As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridView1 As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents CLN_Bat As DevExpress.XtraGrid.Columns.GridColumn
End Class
