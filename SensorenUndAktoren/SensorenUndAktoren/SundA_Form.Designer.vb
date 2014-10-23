<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class SundA_Form
    Inherits System.Windows.Forms.Form

    'Das Formular überschreibt den Löschvorgang, um die Komponentenliste zu bereinigen.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Wird vom Windows Form-Designer benötigt.
    Private components As System.ComponentModel.IContainer

    'Hinweis: Die folgende Prozedur ist für den Windows Form-Designer erforderlich.
    'Das Bearbeiten ist mit dem Windows Form-Designer möglich.  
    'Das Bearbeiten mit dem Code-Editor ist nicht möglich.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.TabControl1 = New System.Windows.Forms.TabControl()
        Me.TabPage1 = New System.Windows.Forms.TabPage()
        Me.CheckBox_unterdrueckt = New System.Windows.Forms.CheckBox()
        Me.Label_GUID = New System.Windows.Forms.Label()
        Me.TextBox_Grundstellung = New System.Windows.Forms.TextBox()
        Me.TextBox_Typ = New System.Windows.Forms.TextBox()
        Me.TextBox_Funktion = New System.Windows.Forms.TextBox()
        Me.TextBox_Bezeichnung = New System.Windows.Forms.TextBox()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.TextBox_BMK = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.TabPage2 = New System.Windows.Forms.TabPage()
        Me.DataGridView_Baugruppe = New System.Windows.Forms.DataGridView()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.TreeView1 = New System.Windows.Forms.TreeView()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.Button_Auswaehlen = New System.Windows.Forms.Button()
        Me.Button_Uebernehmen = New System.Windows.Forms.Button()
        Me.Button_KompAnfuegen = New System.Windows.Forms.Button()
        Me.Button_SpeichernSchließen = New System.Windows.Forms.Button()
        Me.TabControl1.SuspendLayout()
        Me.TabPage1.SuspendLayout()
        Me.TabPage2.SuspendLayout()
        CType(Me.DataGridView_Baugruppe, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox1.SuspendLayout()
        Me.SuspendLayout()
        '
        'TabControl1
        '
        Me.TabControl1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TabControl1.Controls.Add(Me.TabPage1)
        Me.TabControl1.Controls.Add(Me.TabPage2)
        Me.TabControl1.Location = New System.Drawing.Point(193, 66)
        Me.TabControl1.Name = "TabControl1"
        Me.TabControl1.SelectedIndex = 0
        Me.TabControl1.Size = New System.Drawing.Size(830, 509)
        Me.TabControl1.TabIndex = 1
        '
        'TabPage1
        '
        Me.TabPage1.BackColor = System.Drawing.SystemColors.ButtonFace
        Me.TabPage1.Controls.Add(Me.CheckBox_unterdrueckt)
        Me.TabPage1.Controls.Add(Me.Button_Uebernehmen)
        Me.TabPage1.Controls.Add(Me.Label_GUID)
        Me.TabPage1.Controls.Add(Me.TextBox_Grundstellung)
        Me.TabPage1.Controls.Add(Me.TextBox_Typ)
        Me.TabPage1.Controls.Add(Me.TextBox_Funktion)
        Me.TabPage1.Controls.Add(Me.TextBox_Bezeichnung)
        Me.TabPage1.Controls.Add(Me.Label5)
        Me.TabPage1.Controls.Add(Me.Label4)
        Me.TabPage1.Controls.Add(Me.Label3)
        Me.TabPage1.Controls.Add(Me.Label2)
        Me.TabPage1.Controls.Add(Me.TextBox_BMK)
        Me.TabPage1.Controls.Add(Me.Label1)
        Me.TabPage1.Location = New System.Drawing.Point(4, 22)
        Me.TabPage1.Name = "TabPage1"
        Me.TabPage1.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage1.Size = New System.Drawing.Size(822, 483)
        Me.TabPage1.TabIndex = 0
        Me.TabPage1.Text = "Eigenschaften"
        '
        'CheckBox_unterdrueckt
        '
        Me.CheckBox_unterdrueckt.AutoSize = True
        Me.CheckBox_unterdrueckt.Location = New System.Drawing.Point(88, 208)
        Me.CheckBox_unterdrueckt.Name = "CheckBox_unterdrueckt"
        Me.CheckBox_unterdrueckt.Size = New System.Drawing.Size(82, 17)
        Me.CheckBox_unterdrueckt.TabIndex = 11
        Me.CheckBox_unterdrueckt.Text = "Unterdrückt"
        Me.CheckBox_unterdrueckt.UseVisualStyleBackColor = True
        '
        'Label_GUID
        '
        Me.Label_GUID.AutoSize = True
        Me.Label_GUID.Font = New System.Drawing.Font("Courier New", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label_GUID.ForeColor = System.Drawing.Color.Blue
        Me.Label_GUID.Location = New System.Drawing.Point(17, 14)
        Me.Label_GUID.Name = "Label_GUID"
        Me.Label_GUID.Size = New System.Drawing.Size(77, 14)
        Me.Label_GUID.TabIndex = 10
        Me.Label_GUID.Text = "Label_GUID"
        '
        'TextBox_Grundstellung
        '
        Me.TextBox_Grundstellung.Location = New System.Drawing.Point(88, 181)
        Me.TextBox_Grundstellung.Name = "TextBox_Grundstellung"
        Me.TextBox_Grundstellung.Size = New System.Drawing.Size(210, 20)
        Me.TextBox_Grundstellung.TabIndex = 9
        '
        'TextBox_Typ
        '
        Me.TextBox_Typ.Location = New System.Drawing.Point(88, 149)
        Me.TextBox_Typ.Name = "TextBox_Typ"
        Me.TextBox_Typ.Size = New System.Drawing.Size(210, 20)
        Me.TextBox_Typ.TabIndex = 8
        '
        'TextBox_Funktion
        '
        Me.TextBox_Funktion.Location = New System.Drawing.Point(88, 117)
        Me.TextBox_Funktion.Name = "TextBox_Funktion"
        Me.TextBox_Funktion.Size = New System.Drawing.Size(210, 20)
        Me.TextBox_Funktion.TabIndex = 7
        '
        'TextBox_Bezeichnung
        '
        Me.TextBox_Bezeichnung.Location = New System.Drawing.Point(88, 85)
        Me.TextBox_Bezeichnung.Name = "TextBox_Bezeichnung"
        Me.TextBox_Bezeichnung.Size = New System.Drawing.Size(210, 20)
        Me.TextBox_Bezeichnung.TabIndex = 6
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(14, 184)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(72, 13)
        Me.Label5.TabIndex = 5
        Me.Label5.Text = "Grundstellung"
        Me.Label5.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(38, 120)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(48, 13)
        Me.Label4.TabIndex = 4
        Me.Label4.Text = "Funktion"
        Me.Label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(61, 152)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(25, 13)
        Me.Label3.TabIndex = 3
        Me.Label3.Text = "Typ"
        Me.Label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(17, 88)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(69, 13)
        Me.Label2.TabIndex = 2
        Me.Label2.Text = "Bezeichnung"
        Me.Label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'TextBox_BMK
        '
        Me.TextBox_BMK.Location = New System.Drawing.Point(88, 54)
        Me.TextBox_BMK.Name = "TextBox_BMK"
        Me.TextBox_BMK.Size = New System.Drawing.Size(210, 20)
        Me.TextBox_BMK.TabIndex = 1
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(52, 57)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(30, 13)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "BMK"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'TabPage2
        '
        Me.TabPage2.Controls.Add(Me.DataGridView_Baugruppe)
        Me.TabPage2.Location = New System.Drawing.Point(4, 22)
        Me.TabPage2.Name = "TabPage2"
        Me.TabPage2.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage2.Size = New System.Drawing.Size(822, 483)
        Me.TabPage2.TabIndex = 1
        Me.TabPage2.Text = "Tabelle"
        Me.TabPage2.UseVisualStyleBackColor = True
        '
        'DataGridView_Baugruppe
        '
        Me.DataGridView_Baugruppe.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DataGridView_Baugruppe.Dock = System.Windows.Forms.DockStyle.Fill
        Me.DataGridView_Baugruppe.Location = New System.Drawing.Point(3, 3)
        Me.DataGridView_Baugruppe.Name = "DataGridView_Baugruppe"
        Me.DataGridView_Baugruppe.Size = New System.Drawing.Size(816, 477)
        Me.DataGridView_Baugruppe.TabIndex = 2
        '
        'Panel1
        '
        Me.Panel1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Panel1.Location = New System.Drawing.Point(193, 7)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(826, 53)
        Me.Panel1.TabIndex = 2
        '
        'TreeView1
        '
        Me.TreeView1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.TreeView1.Location = New System.Drawing.Point(5, 5)
        Me.TreeView1.Name = "TreeView1"
        Me.TreeView1.Size = New System.Drawing.Size(182, 631)
        Me.TreeView1.TabIndex = 3
        '
        'GroupBox1
        '
        Me.GroupBox1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.GroupBox1.Controls.Add(Me.Button_SpeichernSchließen)
        Me.GroupBox1.Controls.Add(Me.Button_Auswaehlen)
        Me.GroupBox1.Controls.Add(Me.Button_KompAnfuegen)
        Me.GroupBox1.Location = New System.Drawing.Point(195, 579)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(823, 55)
        Me.GroupBox1.TabIndex = 4
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Funktionen"
        '
        'Button_Auswaehlen
        '
        Me.Button_Auswaehlen.Location = New System.Drawing.Point(135, 19)
        Me.Button_Auswaehlen.Name = "Button_Auswaehlen"
        Me.Button_Auswaehlen.Size = New System.Drawing.Size(121, 30)
        Me.Button_Auswaehlen.TabIndex = 2
        Me.Button_Auswaehlen.Text = "Auswählen"
        Me.Button_Auswaehlen.UseVisualStyleBackColor = True
        '
        'Button_Uebernehmen
        '
        Me.Button_Uebernehmen.Location = New System.Drawing.Point(17, 249)
        Me.Button_Uebernehmen.Name = "Button_Uebernehmen"
        Me.Button_Uebernehmen.Size = New System.Drawing.Size(98, 29)
        Me.Button_Uebernehmen.TabIndex = 1
        Me.Button_Uebernehmen.Text = "Übernehmen"
        Me.Button_Uebernehmen.UseVisualStyleBackColor = True
        '
        'Button_KompAnfuegen
        '
        Me.Button_KompAnfuegen.Location = New System.Drawing.Point(6, 19)
        Me.Button_KompAnfuegen.Name = "Button_KompAnfuegen"
        Me.Button_KompAnfuegen.Size = New System.Drawing.Size(123, 30)
        Me.Button_KompAnfuegen.TabIndex = 0
        Me.Button_KompAnfuegen.Text = "Komponente anfügen"
        Me.Button_KompAnfuegen.UseVisualStyleBackColor = True
        '
        'Button_SpeichernSchließen
        '
        Me.Button_SpeichernSchließen.Location = New System.Drawing.Point(670, 19)
        Me.Button_SpeichernSchließen.Name = "Button_SpeichernSchließen"
        Me.Button_SpeichernSchließen.Size = New System.Drawing.Size(147, 30)
        Me.Button_SpeichernSchließen.TabIndex = 3
        Me.Button_SpeichernSchließen.Text = "Speichern und Schließen"
        Me.Button_SpeichernSchließen.UseVisualStyleBackColor = True
        '
        'SundA_Form
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1025, 648)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.TreeView1)
        Me.Controls.Add(Me.Panel1)
        Me.Controls.Add(Me.TabControl1)
        Me.Name = "SundA_Form"
        Me.Text = "Sensoren und Aktoren"
        Me.TabControl1.ResumeLayout(False)
        Me.TabPage1.ResumeLayout(False)
        Me.TabPage1.PerformLayout()
        Me.TabPage2.ResumeLayout(False)
        CType(Me.DataGridView_Baugruppe, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox1.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents TabControl1 As System.Windows.Forms.TabControl
    Friend WithEvents TabPage2 As System.Windows.Forms.TabPage
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents TreeView1 As System.Windows.Forms.TreeView
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents Button_KompAnfuegen As System.Windows.Forms.Button
    Friend WithEvents Button_Uebernehmen As System.Windows.Forms.Button
    Friend WithEvents Button_Auswaehlen As System.Windows.Forms.Button
    Friend WithEvents DataGridView_Baugruppe As System.Windows.Forms.DataGridView
    Friend WithEvents TabPage1 As System.Windows.Forms.TabPage
    Friend WithEvents TextBox_Grundstellung As System.Windows.Forms.TextBox
    Friend WithEvents TextBox_Typ As System.Windows.Forms.TextBox
    Friend WithEvents TextBox_Funktion As System.Windows.Forms.TextBox
    Friend WithEvents TextBox_Bezeichnung As System.Windows.Forms.TextBox
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents TextBox_BMK As System.Windows.Forms.TextBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label_GUID As System.Windows.Forms.Label
    Friend WithEvents CheckBox_unterdrueckt As System.Windows.Forms.CheckBox
    Friend WithEvents Button_SpeichernSchließen As System.Windows.Forms.Button
End Class
