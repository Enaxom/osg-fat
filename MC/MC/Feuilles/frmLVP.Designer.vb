<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmlvp
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmlvp))
        Me.BtnAnnuler = New DevExpress.XtraEditors.SimpleButton()
        Me.BtnOK = New DevExpress.XtraEditors.SimpleButton()
        Me.btnDoublet = New DevExpress.XtraEditors.SimpleButton()
        Me.btnLvp = New DevExpress.XtraEditors.SimpleButton()
        Me.btnVent = New DevExpress.XtraEditors.SimpleButton()
        Me.RadioGroup_Vent = New DevExpress.XtraEditors.RadioGroup()
        Me.RadioGroup_LVP = New DevExpress.XtraEditors.RadioGroup()
        Me.RadioGroup_Doublet = New DevExpress.XtraEditors.RadioGroup()
        CType(Me.RadioGroup_Vent.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadioGroup_LVP.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadioGroup_Doublet.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'BtnAnnuler
        '
        Me.BtnAnnuler.Appearance.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BtnAnnuler.Appearance.Options.UseFont = True
        Me.BtnAnnuler.Location = New System.Drawing.Point(207, 195)
        Me.BtnAnnuler.Name = "BtnAnnuler"
        Me.BtnAnnuler.Size = New System.Drawing.Size(118, 34)
        Me.BtnAnnuler.TabIndex = 4
        Me.BtnAnnuler.Text = "SimpleButton1"
        '
        'BtnOK
        '
        Me.BtnOK.Appearance.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BtnOK.Appearance.Options.UseFont = True
        Me.BtnOK.Location = New System.Drawing.Point(26, 195)
        Me.BtnOK.Name = "BtnOK"
        Me.BtnOK.Size = New System.Drawing.Size(118, 34)
        Me.BtnOK.TabIndex = 5
        Me.BtnOK.Text = "SimpleButton1"
        '
        'btnDoublet
        '
        Me.btnDoublet.Appearance.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnDoublet.Appearance.Options.UseFont = True
        Me.btnDoublet.Image = Global.MC.My.Resources.Resources.Smileys2
        Me.btnDoublet.ImageLocation = DevExpress.XtraEditors.ImageLocation.MiddleCenter
        Me.btnDoublet.Location = New System.Drawing.Point(12, 122)
        Me.btnDoublet.Name = "btnDoublet"
        Me.btnDoublet.Size = New System.Drawing.Size(45, 45)
        Me.btnDoublet.TabIndex = 15
        Me.btnDoublet.Text = "SimpleButton1"
        '
        'btnLvp
        '
        Me.btnLvp.Appearance.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnLvp.Appearance.Options.UseFont = True
        Me.btnLvp.Image = Global.MC.My.Resources.Resources.soleilnuageux
        Me.btnLvp.ImageLocation = DevExpress.XtraEditors.ImageLocation.MiddleCenter
        Me.btnLvp.Location = New System.Drawing.Point(12, 67)
        Me.btnLvp.Name = "btnLvp"
        Me.btnLvp.Size = New System.Drawing.Size(45, 45)
        Me.btnLvp.TabIndex = 14
        Me.btnLvp.Text = "SimpleButton1"
        '
        'btnVent
        '
        Me.btnVent.Appearance.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnVent.Appearance.Options.UseFont = True
        Me.btnVent.Image = Global.MC.My.Resources.Resources.Vent1
        Me.btnVent.ImageLocation = DevExpress.XtraEditors.ImageLocation.MiddleCenter
        Me.btnVent.Location = New System.Drawing.Point(12, 12)
        Me.btnVent.Name = "btnVent"
        Me.btnVent.Size = New System.Drawing.Size(45, 45)
        Me.btnVent.TabIndex = 13
        '
        'RadioGroup_Vent
        '
        Me.RadioGroup_Vent.EditValue = False
        Me.RadioGroup_Vent.Location = New System.Drawing.Point(65, 21)
        Me.RadioGroup_Vent.Name = "RadioGroup_Vent"
        Me.RadioGroup_Vent.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.RadioGroup_Vent.Properties.Appearance.Options.UseBackColor = True
        Me.RadioGroup_Vent.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
        Me.RadioGroup_Vent.Properties.Columns = 2
        Me.RadioGroup_Vent.Properties.Items.AddRange(New DevExpress.XtraEditors.Controls.RadioGroupItem() {New DevExpress.XtraEditors.Controls.RadioGroupItem(True, "Oui"), New DevExpress.XtraEditors.Controls.RadioGroupItem(False, "Non")})
        Me.RadioGroup_Vent.Size = New System.Drawing.Size(272, 28)
        Me.RadioGroup_Vent.TabIndex = 32
        '
        'RadioGroup_LVP
        '
        Me.RadioGroup_LVP.EditValue = False
        Me.RadioGroup_LVP.Location = New System.Drawing.Point(63, 67)
        Me.RadioGroup_LVP.Name = "RadioGroup_LVP"
        Me.RadioGroup_LVP.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.RadioGroup_LVP.Properties.Appearance.Options.UseBackColor = True
        Me.RadioGroup_LVP.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
        Me.RadioGroup_LVP.Properties.Columns = 2
        Me.RadioGroup_LVP.Properties.Items.AddRange(New DevExpress.XtraEditors.Controls.RadioGroupItem() {New DevExpress.XtraEditors.Controls.RadioGroupItem(True, "800"), New DevExpress.XtraEditors.Controls.RadioGroupItem(False, "400"), New DevExpress.XtraEditors.Controls.RadioGroupItem(Nothing, "Neant")})
        Me.RadioGroup_LVP.Size = New System.Drawing.Size(274, 45)
        Me.RadioGroup_LVP.TabIndex = 33
        '
        'RadioGroup_Doublet
        '
        Me.RadioGroup_Doublet.EditValue = False
        Me.RadioGroup_Doublet.Location = New System.Drawing.Point(63, 122)
        Me.RadioGroup_Doublet.Name = "RadioGroup_Doublet"
        Me.RadioGroup_Doublet.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.RadioGroup_Doublet.Properties.Appearance.Options.UseBackColor = True
        Me.RadioGroup_Doublet.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
        Me.RadioGroup_Doublet.Properties.Columns = 2
        Me.RadioGroup_Doublet.Properties.Items.AddRange(New DevExpress.XtraEditors.Controls.RadioGroupItem() {New DevExpress.XtraEditors.Controls.RadioGroupItem(True, "Nord"), New DevExpress.XtraEditors.Controls.RadioGroupItem(False, "Sud"), New DevExpress.XtraEditors.Controls.RadioGroupItem(Nothing, "Neant")})
        Me.RadioGroup_Doublet.Size = New System.Drawing.Size(274, 45)
        Me.RadioGroup_Doublet.TabIndex = 34
        '
        'frmlvp
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(352, 241)
        Me.Controls.Add(Me.RadioGroup_Doublet)
        Me.Controls.Add(Me.RadioGroup_LVP)
        Me.Controls.Add(Me.RadioGroup_Vent)
        Me.Controls.Add(Me.btnDoublet)
        Me.Controls.Add(Me.btnLvp)
        Me.Controls.Add(Me.btnVent)
        Me.Controls.Add(Me.BtnOK)
        Me.Controls.Add(Me.BtnAnnuler)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmlvp"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "frmLogin"
        CType(Me.RadioGroup_Vent.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadioGroup_LVP.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadioGroup_Doublet.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents BtnAnnuler As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents BtnOK As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents btnDoublet As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents btnLvp As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents btnVent As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents RadioGroup_Vent As DevExpress.XtraEditors.RadioGroup
    Friend WithEvents RadioGroup_LVP As DevExpress.XtraEditors.RadioGroup
    Friend WithEvents RadioGroup_Doublet As DevExpress.XtraEditors.RadioGroup
End Class
