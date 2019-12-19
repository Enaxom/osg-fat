<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmRechercheASar
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmRechercheASar))
        Me.GrpRAMVTPERSO = New DevExpress.XtraEditors.GroupControl()
        Me.dateDebChantierSAR = New DevExpress.XtraEditors.DateEdit()
        Me.cboTacheSar = New DevExpress.XtraEditors.ComboBoxEdit()
        Me.lblTravauxSar = New DevExpress.XtraEditors.LabelControl()
        Me.cboOrdreSar = New DevExpress.XtraEditors.ComboBoxEdit()
        Me.lblOrdreSar = New DevExpress.XtraEditors.LabelControl()
        Me.cboBatimentSar = New DevExpress.XtraEditors.ComboBoxEdit()
        Me.cboVoieSar = New DevExpress.XtraEditors.ComboBoxEdit()
        Me.lblBatSar = New DevExpress.XtraEditors.LabelControl()
        Me.lblVoieSar = New DevExpress.XtraEditors.LabelControl()
        Me.dateFinChantierSAR = New DevExpress.XtraEditors.DateEdit()
        Me.lblDateFinChantierSAR = New DevExpress.XtraEditors.LabelControl()
        Me.lblDateDebChantierSAR = New DevExpress.XtraEditors.LabelControl()
        Me.cboEntrepriseSar = New DevExpress.XtraEditors.ComboBoxEdit()
        Me.cboTypeActiviteSar = New DevExpress.XtraEditors.ComboBoxEdit()
        Me.cboNiveauSar = New DevExpress.XtraEditors.ComboBoxEdit()
        Me.dateFinAutoSAR = New DevExpress.XtraEditors.DateEdit()
        Me.lblDateFinAutoSAR = New DevExpress.XtraEditors.LabelControl()
        Me.dateDebAutoSAR = New DevExpress.XtraEditors.DateEdit()
        Me.lblTypeSar = New DevExpress.XtraEditors.LabelControl()
        Me.lblSocSar = New DevExpress.XtraEditors.LabelControl()
        Me.lblDateDebAutoSAR = New DevExpress.XtraEditors.LabelControl()
        Me.BtnRASarAnnuler = New DevExpress.XtraEditors.SimpleButton()
        Me.lblEtageSar = New DevExpress.XtraEditors.LabelControl()
        Me.BtnRASarRech = New DevExpress.XtraEditors.SimpleButton()
        Me.lblSAR = New DevExpress.XtraEditors.LabelControl()
        Me.lblChantier = New DevExpress.XtraEditors.LabelControl()
        CType(Me.GrpRAMVTPERSO, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GrpRAMVTPERSO.SuspendLayout()
        CType(Me.dateDebChantierSAR.Properties.VistaTimeProperties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dateDebChantierSAR.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cboTacheSar.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cboOrdreSar.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cboBatimentSar.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cboVoieSar.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dateFinChantierSAR.Properties.VistaTimeProperties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dateFinChantierSAR.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cboEntrepriseSar.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cboTypeActiviteSar.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cboNiveauSar.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dateFinAutoSAR.Properties.VistaTimeProperties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dateFinAutoSAR.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dateDebAutoSAR.Properties.VistaTimeProperties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dateDebAutoSAR.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'GrpRAMVTPERSO
        '
        Me.GrpRAMVTPERSO.AppearanceCaption.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GrpRAMVTPERSO.AppearanceCaption.Options.UseFont = True
        Me.GrpRAMVTPERSO.Controls.Add(Me.lblChantier)
        Me.GrpRAMVTPERSO.Controls.Add(Me.lblSAR)
        Me.GrpRAMVTPERSO.Controls.Add(Me.dateDebChantierSAR)
        Me.GrpRAMVTPERSO.Controls.Add(Me.cboTacheSar)
        Me.GrpRAMVTPERSO.Controls.Add(Me.lblTravauxSar)
        Me.GrpRAMVTPERSO.Controls.Add(Me.cboOrdreSar)
        Me.GrpRAMVTPERSO.Controls.Add(Me.lblOrdreSar)
        Me.GrpRAMVTPERSO.Controls.Add(Me.cboBatimentSar)
        Me.GrpRAMVTPERSO.Controls.Add(Me.cboVoieSar)
        Me.GrpRAMVTPERSO.Controls.Add(Me.lblBatSar)
        Me.GrpRAMVTPERSO.Controls.Add(Me.lblVoieSar)
        Me.GrpRAMVTPERSO.Controls.Add(Me.dateFinChantierSAR)
        Me.GrpRAMVTPERSO.Controls.Add(Me.lblDateFinChantierSAR)
        Me.GrpRAMVTPERSO.Controls.Add(Me.lblDateDebChantierSAR)
        Me.GrpRAMVTPERSO.Controls.Add(Me.cboEntrepriseSar)
        Me.GrpRAMVTPERSO.Controls.Add(Me.cboTypeActiviteSar)
        Me.GrpRAMVTPERSO.Controls.Add(Me.cboNiveauSar)
        Me.GrpRAMVTPERSO.Controls.Add(Me.dateFinAutoSAR)
        Me.GrpRAMVTPERSO.Controls.Add(Me.lblDateFinAutoSAR)
        Me.GrpRAMVTPERSO.Controls.Add(Me.dateDebAutoSAR)
        Me.GrpRAMVTPERSO.Controls.Add(Me.lblTypeSar)
        Me.GrpRAMVTPERSO.Controls.Add(Me.lblSocSar)
        Me.GrpRAMVTPERSO.Controls.Add(Me.lblDateDebAutoSAR)
        Me.GrpRAMVTPERSO.Controls.Add(Me.BtnRASarAnnuler)
        Me.GrpRAMVTPERSO.Controls.Add(Me.lblEtageSar)
        Me.GrpRAMVTPERSO.Controls.Add(Me.BtnRASarRech)
        Me.GrpRAMVTPERSO.Location = New System.Drawing.Point(12, 12)
        Me.GrpRAMVTPERSO.Name = "GrpRAMVTPERSO"
        Me.GrpRAMVTPERSO.Size = New System.Drawing.Size(694, 430)
        Me.GrpRAMVTPERSO.TabIndex = 2
        Me.GrpRAMVTPERSO.Text = "GroupControl2"
        '
        'dateDebChantierSAR
        '
        Me.dateDebChantierSAR.EditValue = Nothing
        Me.dateDebChantierSAR.Location = New System.Drawing.Point(230, 120)
        Me.dateDebChantierSAR.Name = "dateDebChantierSAR"
        Me.dateDebChantierSAR.Properties.Appearance.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.dateDebChantierSAR.Properties.Appearance.Options.UseFont = True
        Me.dateDebChantierSAR.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.dateDebChantierSAR.Properties.VistaTimeProperties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton()})
        Me.dateDebChantierSAR.Size = New System.Drawing.Size(149, 22)
        Me.dateDebChantierSAR.TabIndex = 48
        '
        'cboTacheSar
        '
        Me.cboTacheSar.Location = New System.Drawing.Point(230, 330)
        Me.cboTacheSar.Name = "cboTacheSar"
        Me.cboTacheSar.Properties.Appearance.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cboTacheSar.Properties.Appearance.Options.UseFont = True
        Me.cboTacheSar.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.cboTacheSar.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor
        Me.cboTacheSar.Size = New System.Drawing.Size(444, 22)
        Me.cboTacheSar.TabIndex = 47
        '
        'lblTravauxSar
        '
        Me.lblTravauxSar.Appearance.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblTravauxSar.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.lblTravauxSar.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.lblTravauxSar.Location = New System.Drawing.Point(18, 332)
        Me.lblTravauxSar.Name = "lblTravauxSar"
        Me.lblTravauxSar.Size = New System.Drawing.Size(189, 16)
        Me.lblTravauxSar.TabIndex = 46
        Me.lblTravauxSar.Text = "lblTexte"
        '
        'cboOrdreSar
        '
        Me.cboOrdreSar.Location = New System.Drawing.Point(230, 300)
        Me.cboOrdreSar.Name = "cboOrdreSar"
        Me.cboOrdreSar.Properties.Appearance.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cboOrdreSar.Properties.Appearance.Options.UseFont = True
        Me.cboOrdreSar.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.cboOrdreSar.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor
        Me.cboOrdreSar.Size = New System.Drawing.Size(444, 22)
        Me.cboOrdreSar.TabIndex = 45
        '
        'lblOrdreSar
        '
        Me.lblOrdreSar.Appearance.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblOrdreSar.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.lblOrdreSar.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.lblOrdreSar.Location = New System.Drawing.Point(18, 302)
        Me.lblOrdreSar.Name = "lblOrdreSar"
        Me.lblOrdreSar.Size = New System.Drawing.Size(189, 16)
        Me.lblOrdreSar.TabIndex = 44
        Me.lblOrdreSar.Text = "lblTexte"
        '
        'cboBatimentSar
        '
        Me.cboBatimentSar.Location = New System.Drawing.Point(229, 210)
        Me.cboBatimentSar.Name = "cboBatimentSar"
        Me.cboBatimentSar.Properties.Appearance.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cboBatimentSar.Properties.Appearance.Options.UseFont = True
        Me.cboBatimentSar.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.cboBatimentSar.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor
        Me.cboBatimentSar.Size = New System.Drawing.Size(444, 22)
        Me.cboBatimentSar.TabIndex = 43
        '
        'cboVoieSar
        '
        Me.cboVoieSar.Location = New System.Drawing.Point(229, 180)
        Me.cboVoieSar.Name = "cboVoieSar"
        Me.cboVoieSar.Properties.Appearance.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cboVoieSar.Properties.Appearance.Options.UseFont = True
        Me.cboVoieSar.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.cboVoieSar.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor
        Me.cboVoieSar.Size = New System.Drawing.Size(444, 22)
        Me.cboVoieSar.TabIndex = 42
        '
        'lblBatSar
        '
        Me.lblBatSar.Appearance.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblBatSar.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.lblBatSar.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.lblBatSar.Location = New System.Drawing.Point(18, 212)
        Me.lblBatSar.Name = "lblBatSar"
        Me.lblBatSar.Size = New System.Drawing.Size(189, 16)
        Me.lblBatSar.TabIndex = 41
        Me.lblBatSar.Text = "LabelControl2"
        '
        'lblVoieSar
        '
        Me.lblVoieSar.Appearance.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblVoieSar.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.lblVoieSar.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.lblVoieSar.Location = New System.Drawing.Point(18, 182)
        Me.lblVoieSar.Name = "lblVoieSar"
        Me.lblVoieSar.Size = New System.Drawing.Size(189, 16)
        Me.lblVoieSar.TabIndex = 40
        Me.lblVoieSar.Text = "lblVoie"
        '
        'dateFinChantierSAR
        '
        Me.dateFinChantierSAR.EditValue = Nothing
        Me.dateFinChantierSAR.Location = New System.Drawing.Point(524, 120)
        Me.dateFinChantierSAR.Name = "dateFinChantierSAR"
        Me.dateFinChantierSAR.Properties.Appearance.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.dateFinChantierSAR.Properties.Appearance.Options.UseFont = True
        Me.dateFinChantierSAR.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.dateFinChantierSAR.Properties.VistaTimeProperties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton()})
        Me.dateFinChantierSAR.Size = New System.Drawing.Size(149, 22)
        Me.dateFinChantierSAR.TabIndex = 39
        '
        'lblDateFinChantierSAR
        '
        Me.lblDateFinChantierSAR.Appearance.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblDateFinChantierSAR.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.lblDateFinChantierSAR.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.lblDateFinChantierSAR.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
        Me.lblDateFinChantierSAR.Location = New System.Drawing.Point(397, 122)
        Me.lblDateFinChantierSAR.Name = "lblDateFinChantierSAR"
        Me.lblDateFinChantierSAR.Size = New System.Drawing.Size(119, 16)
        Me.lblDateFinChantierSAR.TabIndex = 38
        Me.lblDateFinChantierSAR.Text = "LabelControl2"
        '
        'lblDateDebChantierSAR
        '
        Me.lblDateDebChantierSAR.Appearance.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblDateDebChantierSAR.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.lblDateDebChantierSAR.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.lblDateDebChantierSAR.Location = New System.Drawing.Point(18, 122)
        Me.lblDateDebChantierSAR.Name = "lblDateDebChantierSAR"
        Me.lblDateDebChantierSAR.Size = New System.Drawing.Size(189, 16)
        Me.lblDateDebChantierSAR.TabIndex = 36
        Me.lblDateDebChantierSAR.Text = "LabelControl2"
        '
        'cboEntrepriseSar
        '
        Me.cboEntrepriseSar.Location = New System.Drawing.Point(229, 270)
        Me.cboEntrepriseSar.Name = "cboEntrepriseSar"
        Me.cboEntrepriseSar.Properties.Appearance.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cboEntrepriseSar.Properties.Appearance.Options.UseFont = True
        Me.cboEntrepriseSar.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.cboEntrepriseSar.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor
        Me.cboEntrepriseSar.Size = New System.Drawing.Size(444, 22)
        Me.cboEntrepriseSar.TabIndex = 35
        '
        'cboTypeActiviteSar
        '
        Me.cboTypeActiviteSar.Location = New System.Drawing.Point(230, 150)
        Me.cboTypeActiviteSar.Name = "cboTypeActiviteSar"
        Me.cboTypeActiviteSar.Properties.Appearance.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cboTypeActiviteSar.Properties.Appearance.Options.UseFont = True
        Me.cboTypeActiviteSar.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.cboTypeActiviteSar.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor
        Me.cboTypeActiviteSar.Size = New System.Drawing.Size(443, 22)
        Me.cboTypeActiviteSar.TabIndex = 34
        '
        'cboNiveauSar
        '
        Me.cboNiveauSar.Location = New System.Drawing.Point(229, 240)
        Me.cboNiveauSar.Name = "cboNiveauSar"
        Me.cboNiveauSar.Properties.Appearance.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cboNiveauSar.Properties.Appearance.Options.UseFont = True
        Me.cboNiveauSar.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.cboNiveauSar.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor
        Me.cboNiveauSar.Size = New System.Drawing.Size(444, 22)
        Me.cboNiveauSar.TabIndex = 33
        '
        'dateFinAutoSAR
        '
        Me.dateFinAutoSAR.EditValue = Nothing
        Me.dateFinAutoSAR.Location = New System.Drawing.Point(522, 59)
        Me.dateFinAutoSAR.Name = "dateFinAutoSAR"
        Me.dateFinAutoSAR.Properties.Appearance.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.dateFinAutoSAR.Properties.Appearance.Options.UseFont = True
        Me.dateFinAutoSAR.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.dateFinAutoSAR.Properties.VistaTimeProperties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton()})
        Me.dateFinAutoSAR.Size = New System.Drawing.Size(151, 22)
        Me.dateFinAutoSAR.TabIndex = 32
        '
        'lblDateFinAutoSAR
        '
        Me.lblDateFinAutoSAR.Appearance.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblDateFinAutoSAR.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.lblDateFinAutoSAR.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.lblDateFinAutoSAR.Location = New System.Drawing.Point(415, 62)
        Me.lblDateFinAutoSAR.Name = "lblDateFinAutoSAR"
        Me.lblDateFinAutoSAR.Size = New System.Drawing.Size(101, 16)
        Me.lblDateFinAutoSAR.TabIndex = 31
        Me.lblDateFinAutoSAR.Text = "LabelControl2"
        '
        'dateDebAutoSAR
        '
        Me.dateDebAutoSAR.EditValue = Nothing
        Me.dateDebAutoSAR.Location = New System.Drawing.Point(230, 59)
        Me.dateDebAutoSAR.Name = "dateDebAutoSAR"
        Me.dateDebAutoSAR.Properties.Appearance.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.dateDebAutoSAR.Properties.Appearance.Options.UseFont = True
        Me.dateDebAutoSAR.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.dateDebAutoSAR.Properties.VistaTimeProperties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton()})
        Me.dateDebAutoSAR.Size = New System.Drawing.Size(151, 22)
        Me.dateDebAutoSAR.TabIndex = 30
        '
        'lblTypeSar
        '
        Me.lblTypeSar.Appearance.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblTypeSar.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.lblTypeSar.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.lblTypeSar.Location = New System.Drawing.Point(18, 152)
        Me.lblTypeSar.Name = "lblTypeSar"
        Me.lblTypeSar.Size = New System.Drawing.Size(189, 16)
        Me.lblTypeSar.TabIndex = 29
        Me.lblTypeSar.Text = "LabelControl2"
        '
        'lblSocSar
        '
        Me.lblSocSar.Appearance.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblSocSar.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.lblSocSar.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.lblSocSar.Location = New System.Drawing.Point(18, 272)
        Me.lblSocSar.Name = "lblSocSar"
        Me.lblSocSar.Size = New System.Drawing.Size(189, 16)
        Me.lblSocSar.TabIndex = 28
        Me.lblSocSar.Text = "lblTexte"
        '
        'lblDateDebAutoSAR
        '
        Me.lblDateDebAutoSAR.Appearance.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblDateDebAutoSAR.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.lblDateDebAutoSAR.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.lblDateDebAutoSAR.Location = New System.Drawing.Point(18, 62)
        Me.lblDateDebAutoSAR.Name = "lblDateDebAutoSAR"
        Me.lblDateDebAutoSAR.Size = New System.Drawing.Size(189, 16)
        Me.lblDateDebAutoSAR.TabIndex = 25
        Me.lblDateDebAutoSAR.Text = "LabelControl2"
        '
        'BtnRASarAnnuler
        '
        Me.BtnRASarAnnuler.Appearance.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BtnRASarAnnuler.Appearance.Options.UseFont = True
        Me.BtnRASarAnnuler.Location = New System.Drawing.Point(515, 381)
        Me.BtnRASarAnnuler.Name = "BtnRASarAnnuler"
        Me.BtnRASarAnnuler.Size = New System.Drawing.Size(158, 30)
        Me.BtnRASarAnnuler.TabIndex = 12
        Me.BtnRASarAnnuler.Text = "SimpleButton1"
        '
        'lblEtageSar
        '
        Me.lblEtageSar.Appearance.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblEtageSar.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.lblEtageSar.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.lblEtageSar.Location = New System.Drawing.Point(18, 242)
        Me.lblEtageSar.Name = "lblEtageSar"
        Me.lblEtageSar.Size = New System.Drawing.Size(189, 16)
        Me.lblEtageSar.TabIndex = 10
        Me.lblEtageSar.Text = "lblTexte"
        '
        'BtnRASarRech
        '
        Me.BtnRASarRech.Appearance.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BtnRASarRech.Appearance.Options.UseFont = True
        Me.BtnRASarRech.Location = New System.Drawing.Point(329, 381)
        Me.BtnRASarRech.Name = "BtnRASarRech"
        Me.BtnRASarRech.Size = New System.Drawing.Size(158, 30)
        Me.BtnRASarRech.TabIndex = 11
        Me.BtnRASarRech.Text = "SimpleButton1"
        '
        'lblSAR
        '
        Me.lblSAR.Appearance.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblSAR.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.lblSAR.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.lblSAR.Location = New System.Drawing.Point(18, 32)
        Me.lblSAR.Name = "lblSAR"
        Me.lblSAR.Size = New System.Drawing.Size(642, 16)
        Me.lblSAR.TabIndex = 49
        Me.lblSAR.Text = "LabelControl2"
        '
        'lblChantier
        '
        Me.lblChantier.Appearance.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblChantier.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.lblChantier.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.lblChantier.Location = New System.Drawing.Point(18, 92)
        Me.lblChantier.Name = "lblChantier"
        Me.lblChantier.Size = New System.Drawing.Size(642, 16)
        Me.lblChantier.TabIndex = 50
        Me.lblChantier.Text = "LabelControl2"
        '
        'frmRechercheASar
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(718, 451)
        Me.Controls.Add(Me.GrpRAMVTPERSO)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmRechercheASar"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "frmRechercheA"
        CType(Me.GrpRAMVTPERSO, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GrpRAMVTPERSO.ResumeLayout(False)
        CType(Me.dateDebChantierSAR.Properties.VistaTimeProperties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dateDebChantierSAR.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cboTacheSar.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cboOrdreSar.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cboBatimentSar.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cboVoieSar.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dateFinChantierSAR.Properties.VistaTimeProperties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dateFinChantierSAR.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cboEntrepriseSar.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cboTypeActiviteSar.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cboNiveauSar.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dateFinAutoSAR.Properties.VistaTimeProperties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dateFinAutoSAR.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dateDebAutoSAR.Properties.VistaTimeProperties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dateDebAutoSAR.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents GrpRAMVTPERSO As DevExpress.XtraEditors.GroupControl
    Friend WithEvents lblEtageSar As DevExpress.XtraEditors.LabelControl
    Friend WithEvents BtnRASarRech As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents BtnRASarAnnuler As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents lblDateDebAutoSAR As DevExpress.XtraEditors.LabelControl
    Friend WithEvents lblTypeSar As DevExpress.XtraEditors.LabelControl
    Friend WithEvents lblSocSar As DevExpress.XtraEditors.LabelControl
    Friend WithEvents dateFinAutoSAR As DevExpress.XtraEditors.DateEdit
    Friend WithEvents lblDateFinAutoSAR As DevExpress.XtraEditors.LabelControl
    Friend WithEvents dateDebAutoSAR As DevExpress.XtraEditors.DateEdit
    Friend WithEvents dateFinChantierSAR As DevExpress.XtraEditors.DateEdit
    Friend WithEvents lblDateFinChantierSAR As DevExpress.XtraEditors.LabelControl
    Friend WithEvents lblDateDebChantierSAR As DevExpress.XtraEditors.LabelControl
    Friend WithEvents cboEntrepriseSar As DevExpress.XtraEditors.ComboBoxEdit
    Friend WithEvents cboTypeActiviteSar As DevExpress.XtraEditors.ComboBoxEdit
    Friend WithEvents cboNiveauSar As DevExpress.XtraEditors.ComboBoxEdit
    Friend WithEvents cboBatimentSar As DevExpress.XtraEditors.ComboBoxEdit
    Friend WithEvents cboVoieSar As DevExpress.XtraEditors.ComboBoxEdit
    Friend WithEvents lblBatSar As DevExpress.XtraEditors.LabelControl
    Friend WithEvents lblVoieSar As DevExpress.XtraEditors.LabelControl
    Friend WithEvents cboOrdreSar As DevExpress.XtraEditors.ComboBoxEdit
    Friend WithEvents lblOrdreSar As DevExpress.XtraEditors.LabelControl
    Friend WithEvents cboTacheSar As DevExpress.XtraEditors.ComboBoxEdit
    Friend WithEvents lblTravauxSar As DevExpress.XtraEditors.LabelControl
    Friend WithEvents dateDebChantierSAR As DevExpress.XtraEditors.DateEdit
    Friend WithEvents lblChantier As DevExpress.XtraEditors.LabelControl
    Friend WithEvents lblSAR As DevExpress.XtraEditors.LabelControl
End Class
