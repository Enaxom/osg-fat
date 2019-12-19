<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmLogin
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmLogin))
        Me.txtPWD = New DevExpress.XtraEditors.TextEdit()
        Me.lblPWD = New DevExpress.XtraEditors.LabelControl()
        Me.LblNomUser = New DevExpress.XtraEditors.LabelControl()
        Me.cboUSer = New DevExpress.XtraEditors.ComboBoxEdit()
        Me.cboGroupe = New DevExpress.XtraEditors.ComboBoxEdit()
        Me.LblGroupe = New DevExpress.XtraEditors.LabelControl()
        Me.BtnQuitter = New DevExpress.XtraEditors.SimpleButton()
        Me.BtnLogin = New DevExpress.XtraEditors.SimpleButton()
        CType(Me.txtPWD.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cboUSer.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cboGroupe.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'txtPWD
        '
        Me.txtPWD.Location = New System.Drawing.Point(179, 88)
        Me.txtPWD.Name = "txtPWD"
        Me.txtPWD.Properties.Appearance.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtPWD.Properties.Appearance.Options.UseFont = True
        Me.txtPWD.Properties.PasswordChar = Global.Microsoft.VisualBasic.ChrW(42)
        Me.txtPWD.Size = New System.Drawing.Size(276, 22)
        Me.txtPWD.TabIndex = 3
        '
        'lblPWD
        '
        Me.lblPWD.Appearance.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblPWD.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.lblPWD.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.lblPWD.Location = New System.Drawing.Point(12, 91)
        Me.lblPWD.Name = "lblPWD"
        Me.lblPWD.Size = New System.Drawing.Size(161, 16)
        Me.lblPWD.TabIndex = 13
        Me.lblPWD.Text = "LabelControl1"
        '
        'LblNomUser
        '
        Me.LblNomUser.Appearance.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblNomUser.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.LblNomUser.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.LblNomUser.Location = New System.Drawing.Point(12, 57)
        Me.LblNomUser.Name = "LblNomUser"
        Me.LblNomUser.Size = New System.Drawing.Size(161, 16)
        Me.LblNomUser.TabIndex = 15
        Me.LblNomUser.Text = "LabelControl1"
        '
        'cboUSer
        '
        Me.cboUSer.Location = New System.Drawing.Point(179, 54)
        Me.cboUSer.Name = "cboUSer"
        Me.cboUSer.Properties.Appearance.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cboUSer.Properties.Appearance.Options.UseFont = True
        Me.cboUSer.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.cboUSer.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor
        Me.cboUSer.Size = New System.Drawing.Size(276, 22)
        Me.cboUSer.TabIndex = 2
        '
        'cboGroupe
        '
        Me.cboGroupe.Location = New System.Drawing.Point(179, 21)
        Me.cboGroupe.Name = "cboGroupe"
        Me.cboGroupe.Properties.Appearance.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cboGroupe.Properties.Appearance.Options.UseFont = True
        Me.cboGroupe.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.cboGroupe.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor
        Me.cboGroupe.Size = New System.Drawing.Size(276, 22)
        Me.cboGroupe.TabIndex = 1
        '
        'LblGroupe
        '
        Me.LblGroupe.Appearance.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblGroupe.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.LblGroupe.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.LblGroupe.Location = New System.Drawing.Point(12, 24)
        Me.LblGroupe.Name = "LblGroupe"
        Me.LblGroupe.Size = New System.Drawing.Size(161, 16)
        Me.LblGroupe.TabIndex = 18
        Me.LblGroupe.Text = "LabelControl2"
        '
        'BtnQuitter
        '
        Me.BtnQuitter.Appearance.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BtnQuitter.Appearance.Options.UseFont = True
        Me.BtnQuitter.Location = New System.Drawing.Point(238, 129)
        Me.BtnQuitter.Name = "BtnQuitter"
        Me.BtnQuitter.Size = New System.Drawing.Size(217, 34)
        Me.BtnQuitter.TabIndex = 4
        Me.BtnQuitter.Text = "SimpleButton1"
        '
        'BtnLogin
        '
        Me.BtnLogin.Appearance.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BtnLogin.Appearance.Options.UseFont = True
        Me.BtnLogin.Location = New System.Drawing.Point(12, 129)
        Me.BtnLogin.Name = "BtnLogin"
        Me.BtnLogin.Size = New System.Drawing.Size(220, 34)
        Me.BtnLogin.TabIndex = 5
        Me.BtnLogin.Text = "SimpleButton1"
        '
        'frmLogin
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(467, 175)
        Me.Controls.Add(Me.BtnLogin)
        Me.Controls.Add(Me.BtnQuitter)
        Me.Controls.Add(Me.LblGroupe)
        Me.Controls.Add(Me.cboGroupe)
        Me.Controls.Add(Me.cboUSer)
        Me.Controls.Add(Me.LblNomUser)
        Me.Controls.Add(Me.txtPWD)
        Me.Controls.Add(Me.lblPWD)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.MaximumSize = New System.Drawing.Size(475, 209)
        Me.MinimizeBox = False
        Me.MinimumSize = New System.Drawing.Size(473, 203)
        Me.Name = "frmLogin"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "frmLogin"
        CType(Me.txtPWD.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cboUSer.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cboGroupe.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents txtPWD As DevExpress.XtraEditors.TextEdit
    Friend WithEvents lblPWD As DevExpress.XtraEditors.LabelControl
    Friend WithEvents LblNomUser As DevExpress.XtraEditors.LabelControl
    Friend WithEvents cboUSer As DevExpress.XtraEditors.ComboBoxEdit
    Friend WithEvents cboGroupe As DevExpress.XtraEditors.ComboBoxEdit
    Friend WithEvents LblGroupe As DevExpress.XtraEditors.LabelControl
    Friend WithEvents BtnQuitter As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents BtnLogin As DevExpress.XtraEditors.SimpleButton
End Class
