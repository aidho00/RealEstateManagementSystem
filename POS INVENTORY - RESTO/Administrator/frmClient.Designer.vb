<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmClient
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
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

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmClient))
        Me.ClientImage = New System.Windows.Forms.PictureBox()
        Me.ToolStripLabel1 = New System.Windows.Forms.ToolStripLabel()
        Me.ToolStripButton2 = New System.Windows.Forms.ToolStripButton()
        Me.OpenFileDialog1 = New System.Windows.Forms.OpenFileDialog()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.txt_ID = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.txtFBAccount = New System.Windows.Forms.TextBox()
        Me.txtEmail = New System.Windows.Forms.TextBox()
        Me.txtAddress = New System.Windows.Forms.TextBox()
        Me.txtContact = New System.Windows.Forms.TextBox()
        Me.txtLname = New System.Windows.Forms.TextBox()
        Me.txtMname = New System.Windows.Forms.TextBox()
        Me.txtFname = New System.Windows.Forms.TextBox()
        Me.btnCancel = New System.Windows.Forms.Button()
        Me.btnUpdate = New System.Windows.Forms.Button()
        Me.btnBrowse = New System.Windows.Forms.Button()
        Me.btnSave = New System.Windows.Forms.Button()
        Me.ToolStrip1 = New System.Windows.Forms.ToolStrip()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.txtDriver = New System.Windows.Forms.TextBox()
        Me.txtVoters = New System.Windows.Forms.TextBox()
        Me.txtSSS = New System.Windows.Forms.TextBox()
        Me.txtGSIS = New System.Windows.Forms.TextBox()
        Me.txtEmployer = New System.Windows.Forms.TextBox()
        Me.txtOccupation = New System.Windows.Forms.TextBox()
        Me.txtIncome = New System.Windows.Forms.TextBox()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.Label13 = New System.Windows.Forms.Label()
        Me.Label14 = New System.Windows.Forms.Label()
        Me.dtBirthdate = New System.Windows.Forms.DateTimePicker()
        Me.Label15 = New System.Windows.Forms.Label()
        Me.txtAge = New System.Windows.Forms.Label()
        Me.Label17 = New System.Windows.Forms.Label()
        Me.sfn = New System.Windows.Forms.TextBox()
        Me.smn = New System.Windows.Forms.TextBox()
        Me.sln = New System.Windows.Forms.TextBox()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.Label18 = New System.Windows.Forms.Label()
        Me.Label19 = New System.Windows.Forms.Label()
        Me.Label20 = New System.Windows.Forms.Label()
        Me.refcontact1 = New System.Windows.Forms.TextBox()
        Me.refcontact2 = New System.Windows.Forms.TextBox()
        Me.refcontact3 = New System.Windows.Forms.TextBox()
        Me.Label21 = New System.Windows.Forms.Label()
        Me.Label22 = New System.Windows.Forms.Label()
        Me.Label23 = New System.Windows.Forms.Label()
        Me.Label24 = New System.Windows.Forms.Label()
        Me.cbStatus = New System.Windows.Forms.ComboBox()
        Me.Label25 = New System.Windows.Forms.Label()
        Me.Label26 = New System.Windows.Forms.Label()
        Me.Panel2 = New System.Windows.Forms.Panel()
        Me.txtTIN = New System.Windows.Forms.TextBox()
        Me.Label16 = New System.Windows.Forms.Label()
        CType(Me.ClientImage, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ToolStrip1.SuspendLayout()
        Me.Panel2.SuspendLayout()
        Me.SuspendLayout()
        '
        'ClientImage
        '
        Me.ClientImage.BackColor = System.Drawing.SystemColors.Control
        Me.ClientImage.BackgroundImage = CType(resources.GetObject("ClientImage.BackgroundImage"), System.Drawing.Image)
        Me.ClientImage.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.ClientImage.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.ClientImage.Location = New System.Drawing.Point(752, 90)
        Me.ClientImage.Name = "ClientImage"
        Me.ClientImage.Size = New System.Drawing.Size(250, 250)
        Me.ClientImage.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom
        Me.ClientImage.TabIndex = 50
        Me.ClientImage.TabStop = False
        '
        'ToolStripLabel1
        '
        Me.ToolStripLabel1.Font = New System.Drawing.Font("Century Gothic", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ToolStripLabel1.Name = "ToolStripLabel1"
        Me.ToolStripLabel1.Size = New System.Drawing.Size(44, 22)
        Me.ToolStripLabel1.Text = "CLIENT"
        '
        'ToolStripButton2
        '
        Me.ToolStripButton2.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right
        Me.ToolStripButton2.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text
        Me.ToolStripButton2.Font = New System.Drawing.Font("Century Gothic", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ToolStripButton2.Image = CType(resources.GetObject("ToolStripButton2.Image"), System.Drawing.Image)
        Me.ToolStripButton2.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ToolStripButton2.Name = "ToolStripButton2"
        Me.ToolStripButton2.Size = New System.Drawing.Size(57, 22)
        Me.ToolStripButton2.Text = "[CLOSE]"
        '
        'OpenFileDialog1
        '
        Me.OpenFileDialog1.FileName = "OpenFileDialog1"
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Font = New System.Drawing.Font("Century Gothic", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label9.ForeColor = System.Drawing.Color.Black
        Me.Label9.Location = New System.Drawing.Point(3, 254)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(68, 16)
        Me.Label9.TabIndex = 46
        Me.Label9.Text = "FB Account"
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Font = New System.Drawing.Font("Century Gothic", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label8.ForeColor = System.Drawing.Color.Black
        Me.Label8.Location = New System.Drawing.Point(3, 227)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(36, 16)
        Me.Label8.TabIndex = 45
        Me.Label8.Text = "Email"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Font = New System.Drawing.Font("Century Gothic", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.ForeColor = System.Drawing.Color.Black
        Me.Label4.Location = New System.Drawing.Point(3, 119)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(54, 16)
        Me.Label4.TabIndex = 43
        Me.Label4.Text = "Contact"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Century Gothic", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.ForeColor = System.Drawing.Color.Black
        Me.Label3.Location = New System.Drawing.Point(3, 92)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(65, 16)
        Me.Label3.TabIndex = 42
        Me.Label3.Text = "Last Name"
        '
        'txt_ID
        '
        Me.txt_ID.AutoSize = True
        Me.txt_ID.Font = New System.Drawing.Font("Century Gothic", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txt_ID.ForeColor = System.Drawing.Color.Black
        Me.txt_ID.Location = New System.Drawing.Point(3, 11)
        Me.txt_ID.Name = "txt_ID"
        Me.txt_ID.Size = New System.Drawing.Size(19, 16)
        Me.txt_ID.TabIndex = 39
        Me.txt_ID.Text = "ID"
        Me.txt_ID.Visible = False
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Century Gothic", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.ForeColor = System.Drawing.Color.Black
        Me.Label1.Location = New System.Drawing.Point(3, 65)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(82, 16)
        Me.Label1.TabIndex = 40
        Me.Label1.Text = "Middle Name"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Century Gothic", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.ForeColor = System.Drawing.Color.Black
        Me.Label2.Location = New System.Drawing.Point(3, 38)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(62, 16)
        Me.Label2.TabIndex = 41
        Me.Label2.Text = "First Name"
        '
        'txtFBAccount
        '
        Me.txtFBAccount.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtFBAccount.Location = New System.Drawing.Point(110, 252)
        Me.txtFBAccount.Name = "txtFBAccount"
        Me.txtFBAccount.Size = New System.Drawing.Size(616, 21)
        Me.txtFBAccount.TabIndex = 36
        '
        'txtEmail
        '
        Me.txtEmail.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtEmail.Location = New System.Drawing.Point(110, 225)
        Me.txtEmail.Name = "txtEmail"
        Me.txtEmail.Size = New System.Drawing.Size(232, 21)
        Me.txtEmail.TabIndex = 37
        '
        'txtAddress
        '
        Me.txtAddress.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtAddress.Location = New System.Drawing.Point(110, 173)
        Me.txtAddress.Multiline = True
        Me.txtAddress.Name = "txtAddress"
        Me.txtAddress.Size = New System.Drawing.Size(232, 46)
        Me.txtAddress.TabIndex = 35
        '
        'txtContact
        '
        Me.txtContact.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtContact.Location = New System.Drawing.Point(110, 117)
        Me.txtContact.Name = "txtContact"
        Me.txtContact.Size = New System.Drawing.Size(232, 21)
        Me.txtContact.TabIndex = 34
        '
        'txtLname
        '
        Me.txtLname.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtLname.Location = New System.Drawing.Point(110, 90)
        Me.txtLname.Name = "txtLname"
        Me.txtLname.Size = New System.Drawing.Size(232, 21)
        Me.txtLname.TabIndex = 33
        '
        'txtMname
        '
        Me.txtMname.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtMname.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtMname.Location = New System.Drawing.Point(110, 63)
        Me.txtMname.Name = "txtMname"
        Me.txtMname.Size = New System.Drawing.Size(232, 21)
        Me.txtMname.TabIndex = 30
        '
        'txtFname
        '
        Me.txtFname.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtFname.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtFname.Location = New System.Drawing.Point(110, 36)
        Me.txtFname.Name = "txtFname"
        Me.txtFname.Size = New System.Drawing.Size(232, 21)
        Me.txtFname.TabIndex = 29
        '
        'btnCancel
        '
        Me.btnCancel.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnCancel.BackColor = System.Drawing.Color.RoyalBlue
        Me.btnCancel.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnCancel.FlatAppearance.BorderSize = 0
        Me.btnCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnCancel.ForeColor = System.Drawing.Color.White
        Me.btnCancel.Location = New System.Drawing.Point(934, 457)
        Me.btnCancel.Name = "btnCancel"
        Me.btnCancel.Size = New System.Drawing.Size(90, 33)
        Me.btnCancel.TabIndex = 51
        Me.btnCancel.Text = "CANCEL"
        Me.btnCancel.UseVisualStyleBackColor = False
        '
        'btnUpdate
        '
        Me.btnUpdate.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnUpdate.BackColor = System.Drawing.Color.MidnightBlue
        Me.btnUpdate.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnUpdate.FlatAppearance.BorderSize = 0
        Me.btnUpdate.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnUpdate.ForeColor = System.Drawing.Color.White
        Me.btnUpdate.Location = New System.Drawing.Point(843, 457)
        Me.btnUpdate.Name = "btnUpdate"
        Me.btnUpdate.Size = New System.Drawing.Size(90, 33)
        Me.btnUpdate.TabIndex = 49
        Me.btnUpdate.Text = "UPDATE"
        Me.btnUpdate.UseVisualStyleBackColor = False
        '
        'btnBrowse
        '
        Me.btnBrowse.BackColor = System.Drawing.Color.DarkOrange
        Me.btnBrowse.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnBrowse.FlatAppearance.BorderSize = 0
        Me.btnBrowse.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnBrowse.ForeColor = System.Drawing.Color.White
        Me.btnBrowse.Location = New System.Drawing.Point(752, 346)
        Me.btnBrowse.Name = "btnBrowse"
        Me.btnBrowse.Size = New System.Drawing.Size(250, 38)
        Me.btnBrowse.TabIndex = 47
        Me.btnBrowse.Text = "BROWSE"
        Me.btnBrowse.UseVisualStyleBackColor = False
        '
        'btnSave
        '
        Me.btnSave.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnSave.BackColor = System.Drawing.Color.MidnightBlue
        Me.btnSave.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnSave.FlatAppearance.BorderSize = 0
        Me.btnSave.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnSave.ForeColor = System.Drawing.Color.White
        Me.btnSave.Location = New System.Drawing.Point(752, 457)
        Me.btnSave.Name = "btnSave"
        Me.btnSave.Size = New System.Drawing.Size(90, 33)
        Me.btnSave.TabIndex = 48
        Me.btnSave.Text = "SAVE"
        Me.btnSave.UseVisualStyleBackColor = False
        '
        'ToolStrip1
        '
        Me.ToolStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ToolStripLabel1, Me.ToolStripButton2})
        Me.ToolStrip1.Location = New System.Drawing.Point(0, 5)
        Me.ToolStrip1.Name = "ToolStrip1"
        Me.ToolStrip1.Size = New System.Drawing.Size(1036, 25)
        Me.ToolStrip1.TabIndex = 32
        Me.ToolStrip1.Text = "ToolStrip1"
        '
        'Panel1
        '
        Me.Panel1.BackColor = System.Drawing.Color.DarkOrange
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel1.Location = New System.Drawing.Point(0, 0)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(1036, 5)
        Me.Panel1.TabIndex = 31
        '
        'txtDriver
        '
        Me.txtDriver.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtDriver.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtDriver.Location = New System.Drawing.Point(494, 36)
        Me.txtDriver.Name = "txtDriver"
        Me.txtDriver.Size = New System.Drawing.Size(232, 21)
        Me.txtDriver.TabIndex = 29
        '
        'txtVoters
        '
        Me.txtVoters.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtVoters.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtVoters.Location = New System.Drawing.Point(494, 63)
        Me.txtVoters.Name = "txtVoters"
        Me.txtVoters.Size = New System.Drawing.Size(232, 21)
        Me.txtVoters.TabIndex = 30
        '
        'txtSSS
        '
        Me.txtSSS.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtSSS.Location = New System.Drawing.Point(494, 90)
        Me.txtSSS.Name = "txtSSS"
        Me.txtSSS.Size = New System.Drawing.Size(232, 21)
        Me.txtSSS.TabIndex = 33
        '
        'txtGSIS
        '
        Me.txtGSIS.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtGSIS.Location = New System.Drawing.Point(494, 117)
        Me.txtGSIS.Name = "txtGSIS"
        Me.txtGSIS.Size = New System.Drawing.Size(232, 21)
        Me.txtGSIS.TabIndex = 34
        '
        'txtEmployer
        '
        Me.txtEmployer.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtEmployer.Location = New System.Drawing.Point(494, 171)
        Me.txtEmployer.Name = "txtEmployer"
        Me.txtEmployer.Size = New System.Drawing.Size(232, 21)
        Me.txtEmployer.TabIndex = 35
        '
        'txtOccupation
        '
        Me.txtOccupation.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtOccupation.Location = New System.Drawing.Point(494, 198)
        Me.txtOccupation.Name = "txtOccupation"
        Me.txtOccupation.Size = New System.Drawing.Size(232, 21)
        Me.txtOccupation.TabIndex = 37
        '
        'txtIncome
        '
        Me.txtIncome.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtIncome.Location = New System.Drawing.Point(494, 225)
        Me.txtIncome.Name = "txtIncome"
        Me.txtIncome.Size = New System.Drawing.Size(232, 21)
        Me.txtIncome.TabIndex = 36
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Font = New System.Drawing.Font("Century Gothic", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.ForeColor = System.Drawing.Color.Black
        Me.Label5.Location = New System.Drawing.Point(378, 38)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(96, 16)
        Me.Label5.TabIndex = 41
        Me.Label5.Text = "Drivers License #"
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Font = New System.Drawing.Font("Century Gothic", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.ForeColor = System.Drawing.Color.Black
        Me.Label6.Location = New System.Drawing.Point(378, 65)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(54, 16)
        Me.Label6.TabIndex = 40
        Me.Label6.Text = "Voters ID"
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.Font = New System.Drawing.Font("Century Gothic", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label10.ForeColor = System.Drawing.Color.Black
        Me.Label10.Location = New System.Drawing.Point(378, 92)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(37, 16)
        Me.Label10.TabIndex = 42
        Me.Label10.Text = "SSS #"
        '
        'Label11
        '
        Me.Label11.AutoSize = True
        Me.Label11.Font = New System.Drawing.Font("Century Gothic", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label11.ForeColor = System.Drawing.Color.Black
        Me.Label11.Location = New System.Drawing.Point(378, 119)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(44, 16)
        Me.Label11.TabIndex = 43
        Me.Label11.Text = "GSIS #"
        '
        'Label12
        '
        Me.Label12.AutoSize = True
        Me.Label12.Font = New System.Drawing.Font("Century Gothic", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label12.ForeColor = System.Drawing.Color.Black
        Me.Label12.Location = New System.Drawing.Point(378, 173)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(57, 16)
        Me.Label12.TabIndex = 44
        Me.Label12.Text = "Employer"
        '
        'Label13
        '
        Me.Label13.AutoSize = True
        Me.Label13.Font = New System.Drawing.Font("Century Gothic", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label13.ForeColor = System.Drawing.Color.Black
        Me.Label13.Location = New System.Drawing.Point(378, 200)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(75, 16)
        Me.Label13.TabIndex = 45
        Me.Label13.Text = "Occupation"
        '
        'Label14
        '
        Me.Label14.AutoSize = True
        Me.Label14.Font = New System.Drawing.Font("Century Gothic", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label14.ForeColor = System.Drawing.Color.Black
        Me.Label14.Location = New System.Drawing.Point(378, 227)
        Me.Label14.Name = "Label14"
        Me.Label14.Size = New System.Drawing.Size(96, 16)
        Me.Label14.TabIndex = 46
        Me.Label14.Text = "Monthly Income"
        '
        'dtBirthdate
        '
        Me.dtBirthdate.CustomFormat = "yyyy-MM-dd"
        Me.dtBirthdate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtBirthdate.Location = New System.Drawing.Point(110, 144)
        Me.dtBirthdate.Name = "dtBirthdate"
        Me.dtBirthdate.Size = New System.Drawing.Size(112, 21)
        Me.dtBirthdate.TabIndex = 52
        '
        'Label15
        '
        Me.Label15.AutoSize = True
        Me.Label15.Font = New System.Drawing.Font("Century Gothic", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label15.ForeColor = System.Drawing.Color.Black
        Me.Label15.Location = New System.Drawing.Point(3, 148)
        Me.Label15.Name = "Label15"
        Me.Label15.Size = New System.Drawing.Size(60, 16)
        Me.Label15.TabIndex = 43
        Me.Label15.Text = "Birth Date"
        '
        'txtAge
        '
        Me.txtAge.AutoSize = True
        Me.txtAge.Font = New System.Drawing.Font("Century Gothic", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtAge.ForeColor = System.Drawing.Color.Black
        Me.txtAge.Location = New System.Drawing.Point(228, 146)
        Me.txtAge.Name = "txtAge"
        Me.txtAge.Size = New System.Drawing.Size(71, 16)
        Me.txtAge.TabIndex = 43
        Me.txtAge.Text = "# Years Old"
        '
        'Label17
        '
        Me.Label17.AutoSize = True
        Me.Label17.Font = New System.Drawing.Font("Century Gothic", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label17.ForeColor = System.Drawing.Color.Black
        Me.Label17.Location = New System.Drawing.Point(3, 173)
        Me.Label17.Name = "Label17"
        Me.Label17.Size = New System.Drawing.Size(49, 16)
        Me.Label17.TabIndex = 44
        Me.Label17.Text = "Address"
        '
        'sfn
        '
        Me.sfn.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.sfn.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.sfn.Location = New System.Drawing.Point(110, 309)
        Me.sfn.Name = "sfn"
        Me.sfn.Size = New System.Drawing.Size(232, 21)
        Me.sfn.TabIndex = 29
        '
        'smn
        '
        Me.smn.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.smn.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.smn.Location = New System.Drawing.Point(110, 336)
        Me.smn.Name = "smn"
        Me.smn.Size = New System.Drawing.Size(232, 21)
        Me.smn.TabIndex = 30
        '
        'sln
        '
        Me.sln.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.sln.Location = New System.Drawing.Point(110, 363)
        Me.sln.Name = "sln"
        Me.sln.Size = New System.Drawing.Size(232, 21)
        Me.sln.TabIndex = 33
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Font = New System.Drawing.Font("Century Gothic", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label7.ForeColor = System.Drawing.Color.Black
        Me.Label7.Location = New System.Drawing.Point(3, 311)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(62, 16)
        Me.Label7.TabIndex = 41
        Me.Label7.Text = "First Name"
        '
        'Label18
        '
        Me.Label18.AutoSize = True
        Me.Label18.Font = New System.Drawing.Font("Century Gothic", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label18.ForeColor = System.Drawing.Color.Black
        Me.Label18.Location = New System.Drawing.Point(3, 338)
        Me.Label18.Name = "Label18"
        Me.Label18.Size = New System.Drawing.Size(82, 16)
        Me.Label18.TabIndex = 40
        Me.Label18.Text = "Middle Name"
        '
        'Label19
        '
        Me.Label19.AutoSize = True
        Me.Label19.Font = New System.Drawing.Font("Century Gothic", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label19.ForeColor = System.Drawing.Color.Black
        Me.Label19.Location = New System.Drawing.Point(3, 365)
        Me.Label19.Name = "Label19"
        Me.Label19.Size = New System.Drawing.Size(65, 16)
        Me.Label19.TabIndex = 42
        Me.Label19.Text = "Last Name"
        '
        'Label20
        '
        Me.Label20.AutoSize = True
        Me.Label20.Font = New System.Drawing.Font("Century Gothic", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label20.ForeColor = System.Drawing.Color.Black
        Me.Label20.Location = New System.Drawing.Point(3, 284)
        Me.Label20.Name = "Label20"
        Me.Label20.Size = New System.Drawing.Size(47, 16)
        Me.Label20.TabIndex = 46
        Me.Label20.Text = "Spouse"
        '
        'refcontact1
        '
        Me.refcontact1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.refcontact1.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.refcontact1.Location = New System.Drawing.Point(494, 309)
        Me.refcontact1.Name = "refcontact1"
        Me.refcontact1.Size = New System.Drawing.Size(232, 21)
        Me.refcontact1.TabIndex = 29
        '
        'refcontact2
        '
        Me.refcontact2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.refcontact2.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.refcontact2.Location = New System.Drawing.Point(494, 336)
        Me.refcontact2.Name = "refcontact2"
        Me.refcontact2.Size = New System.Drawing.Size(232, 21)
        Me.refcontact2.TabIndex = 30
        '
        'refcontact3
        '
        Me.refcontact3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.refcontact3.Location = New System.Drawing.Point(494, 363)
        Me.refcontact3.Name = "refcontact3"
        Me.refcontact3.Size = New System.Drawing.Size(232, 21)
        Me.refcontact3.TabIndex = 33
        '
        'Label21
        '
        Me.Label21.AutoSize = True
        Me.Label21.Font = New System.Drawing.Font("Century Gothic", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label21.ForeColor = System.Drawing.Color.Black
        Me.Label21.Location = New System.Drawing.Point(378, 311)
        Me.Label21.Name = "Label21"
        Me.Label21.Size = New System.Drawing.Size(96, 16)
        Me.Label21.TabIndex = 41
        Me.Label21.Text = "Ref./Contact #1"
        '
        'Label22
        '
        Me.Label22.AutoSize = True
        Me.Label22.Font = New System.Drawing.Font("Century Gothic", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label22.ForeColor = System.Drawing.Color.Black
        Me.Label22.Location = New System.Drawing.Point(378, 338)
        Me.Label22.Name = "Label22"
        Me.Label22.Size = New System.Drawing.Size(96, 16)
        Me.Label22.TabIndex = 40
        Me.Label22.Text = "Ref./Contact #2"
        '
        'Label23
        '
        Me.Label23.AutoSize = True
        Me.Label23.Font = New System.Drawing.Font("Century Gothic", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label23.ForeColor = System.Drawing.Color.Black
        Me.Label23.Location = New System.Drawing.Point(378, 365)
        Me.Label23.Name = "Label23"
        Me.Label23.Size = New System.Drawing.Size(96, 16)
        Me.Label23.TabIndex = 42
        Me.Label23.Text = "Ref./Contact #3"
        '
        'Label24
        '
        Me.Label24.AutoSize = True
        Me.Label24.Font = New System.Drawing.Font("Century Gothic", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label24.ForeColor = System.Drawing.Color.Black
        Me.Label24.Location = New System.Drawing.Point(378, 284)
        Me.Label24.Name = "Label24"
        Me.Label24.Size = New System.Drawing.Size(164, 16)
        Me.Label24.TabIndex = 46
        Me.Label24.Text = "References/Contact Number"
        '
        'cbStatus
        '
        Me.cbStatus.FormattingEnabled = True
        Me.cbStatus.Items.AddRange(New Object() {"Active", "Inactive"})
        Me.cbStatus.Location = New System.Drawing.Point(849, 33)
        Me.cbStatus.Name = "cbStatus"
        Me.cbStatus.Size = New System.Drawing.Size(153, 24)
        Me.cbStatus.TabIndex = 53
        '
        'Label25
        '
        Me.Label25.AutoSize = True
        Me.Label25.Font = New System.Drawing.Font("Century Gothic", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label25.ForeColor = System.Drawing.Color.Black
        Me.Label25.Location = New System.Drawing.Point(802, 38)
        Me.Label25.Name = "Label25"
        Me.Label25.Size = New System.Drawing.Size(41, 16)
        Me.Label25.TabIndex = 54
        Me.Label25.Text = "Status"
        '
        'Label26
        '
        Me.Label26.AutoSize = True
        Me.Label26.Font = New System.Drawing.Font("Century Gothic", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label26.ForeColor = System.Drawing.Color.Black
        Me.Label26.Location = New System.Drawing.Point(749, 65)
        Me.Label26.Name = "Label26"
        Me.Label26.Size = New System.Drawing.Size(40, 16)
        Me.Label26.TabIndex = 54
        Me.Label26.Text = "Photo"
        '
        'Panel2
        '
        Me.Panel2.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Panel2.Controls.Add(Me.cbStatus)
        Me.Panel2.Controls.Add(Me.Label2)
        Me.Panel2.Controls.Add(Me.Label26)
        Me.Panel2.Controls.Add(Me.btnBrowse)
        Me.Panel2.Controls.Add(Me.Label25)
        Me.Panel2.Controls.Add(Me.txtFname)
        Me.Panel2.Controls.Add(Me.dtBirthdate)
        Me.Panel2.Controls.Add(Me.txtDriver)
        Me.Panel2.Controls.Add(Me.ClientImage)
        Me.Panel2.Controls.Add(Me.sfn)
        Me.Panel2.Controls.Add(Me.Label14)
        Me.Panel2.Controls.Add(Me.refcontact1)
        Me.Panel2.Controls.Add(Me.Label24)
        Me.Panel2.Controls.Add(Me.txtMname)
        Me.Panel2.Controls.Add(Me.Label20)
        Me.Panel2.Controls.Add(Me.txtVoters)
        Me.Panel2.Controls.Add(Me.Label9)
        Me.Panel2.Controls.Add(Me.smn)
        Me.Panel2.Controls.Add(Me.Label13)
        Me.Panel2.Controls.Add(Me.refcontact2)
        Me.Panel2.Controls.Add(Me.Label8)
        Me.Panel2.Controls.Add(Me.txtLname)
        Me.Panel2.Controls.Add(Me.Label12)
        Me.Panel2.Controls.Add(Me.sln)
        Me.Panel2.Controls.Add(Me.Label17)
        Me.Panel2.Controls.Add(Me.refcontact3)
        Me.Panel2.Controls.Add(Me.Label16)
        Me.Panel2.Controls.Add(Me.Label11)
        Me.Panel2.Controls.Add(Me.txtSSS)
        Me.Panel2.Controls.Add(Me.txtAge)
        Me.Panel2.Controls.Add(Me.txtContact)
        Me.Panel2.Controls.Add(Me.Label15)
        Me.Panel2.Controls.Add(Me.txtTIN)
        Me.Panel2.Controls.Add(Me.txtGSIS)
        Me.Panel2.Controls.Add(Me.Label4)
        Me.Panel2.Controls.Add(Me.txtAddress)
        Me.Panel2.Controls.Add(Me.Label10)
        Me.Panel2.Controls.Add(Me.txtEmployer)
        Me.Panel2.Controls.Add(Me.Label23)
        Me.Panel2.Controls.Add(Me.txtEmail)
        Me.Panel2.Controls.Add(Me.Label19)
        Me.Panel2.Controls.Add(Me.txtOccupation)
        Me.Panel2.Controls.Add(Me.Label3)
        Me.Panel2.Controls.Add(Me.txtFBAccount)
        Me.Panel2.Controls.Add(Me.txt_ID)
        Me.Panel2.Controls.Add(Me.txtIncome)
        Me.Panel2.Controls.Add(Me.Label6)
        Me.Panel2.Controls.Add(Me.Label7)
        Me.Panel2.Controls.Add(Me.Label22)
        Me.Panel2.Controls.Add(Me.Label21)
        Me.Panel2.Controls.Add(Me.Label18)
        Me.Panel2.Controls.Add(Me.Label5)
        Me.Panel2.Controls.Add(Me.Label1)
        Me.Panel2.Location = New System.Drawing.Point(12, 33)
        Me.Panel2.MaximumSize = New System.Drawing.Size(1012, 418)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(1012, 418)
        Me.Panel2.TabIndex = 55
        '
        'txtTIN
        '
        Me.txtTIN.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtTIN.Location = New System.Drawing.Point(494, 144)
        Me.txtTIN.Name = "txtTIN"
        Me.txtTIN.Size = New System.Drawing.Size(232, 21)
        Me.txtTIN.TabIndex = 34
        '
        'Label16
        '
        Me.Label16.AutoSize = True
        Me.Label16.Font = New System.Drawing.Font("Century Gothic", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label16.ForeColor = System.Drawing.Color.Black
        Me.Label16.Location = New System.Drawing.Point(378, 146)
        Me.Label16.Name = "Label16"
        Me.Label16.Size = New System.Drawing.Size(35, 16)
        Me.Label16.TabIndex = 43
        Me.Label16.Text = "TIN #"
        '
        'frmClient
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1036, 502)
        Me.ControlBox = False
        Me.Controls.Add(Me.btnCancel)
        Me.Controls.Add(Me.btnUpdate)
        Me.Controls.Add(Me.btnSave)
        Me.Controls.Add(Me.ToolStrip1)
        Me.Controls.Add(Me.Panel1)
        Me.Controls.Add(Me.Panel2)
        Me.Font = New System.Drawing.Font("Century Gothic", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.Name = "frmClient"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        CType(Me.ClientImage, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ToolStrip1.ResumeLayout(False)
        Me.ToolStrip1.PerformLayout()
        Me.Panel2.ResumeLayout(False)
        Me.Panel2.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents ClientImage As PictureBox
    Friend WithEvents ToolStripLabel1 As ToolStripLabel
    Friend WithEvents ToolStripButton2 As ToolStripButton
    Friend WithEvents OpenFileDialog1 As OpenFileDialog
    Friend WithEvents Label9 As Label
    Friend WithEvents Label8 As Label
    Friend WithEvents Label4 As Label
    Friend WithEvents Label3 As Label
    Friend WithEvents txt_ID As Label
    Friend WithEvents Label1 As Label
    Friend WithEvents Label2 As Label
    Friend WithEvents txtFBAccount As TextBox
    Friend WithEvents txtEmail As TextBox
    Friend WithEvents txtAddress As TextBox
    Friend WithEvents txtContact As TextBox
    Friend WithEvents txtLname As TextBox
    Friend WithEvents txtMname As TextBox
    Friend WithEvents txtFname As TextBox
    Friend WithEvents btnCancel As Button
    Friend WithEvents btnUpdate As Button
    Friend WithEvents btnBrowse As Button
    Friend WithEvents btnSave As Button
    Friend WithEvents ToolStrip1 As ToolStrip
    Friend WithEvents Panel1 As Panel
    Friend WithEvents txtDriver As TextBox
    Friend WithEvents txtVoters As TextBox
    Friend WithEvents txtSSS As TextBox
    Friend WithEvents txtGSIS As TextBox
    Friend WithEvents txtEmployer As TextBox
    Friend WithEvents txtOccupation As TextBox
    Friend WithEvents txtIncome As TextBox
    Friend WithEvents Label5 As Label
    Friend WithEvents Label6 As Label
    Friend WithEvents Label10 As Label
    Friend WithEvents Label11 As Label
    Friend WithEvents Label12 As Label
    Friend WithEvents Label13 As Label
    Friend WithEvents Label14 As Label
    Friend WithEvents dtBirthdate As DateTimePicker
    Friend WithEvents Label15 As Label
    Friend WithEvents txtAge As Label
    Friend WithEvents Label17 As Label
    Friend WithEvents sfn As TextBox
    Friend WithEvents smn As TextBox
    Friend WithEvents sln As TextBox
    Friend WithEvents Label7 As Label
    Friend WithEvents Label18 As Label
    Friend WithEvents Label19 As Label
    Friend WithEvents Label20 As Label
    Friend WithEvents refcontact1 As TextBox
    Friend WithEvents refcontact2 As TextBox
    Friend WithEvents refcontact3 As TextBox
    Friend WithEvents Label21 As Label
    Friend WithEvents Label22 As Label
    Friend WithEvents Label23 As Label
    Friend WithEvents Label24 As Label
    Friend WithEvents cbStatus As ComboBox
    Friend WithEvents Label25 As Label
    Friend WithEvents Label26 As Label
    Friend WithEvents Panel2 As Panel
    Friend WithEvents Label16 As Label
    Friend WithEvents txtTIN As TextBox
End Class
