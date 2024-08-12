<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmLedger
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmLedger))
        Dim DataGridViewCellStyle1 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle4 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle2 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle3 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Me.ToolStrip1 = New System.Windows.Forms.ToolStrip()
        Me.lblTitle = New System.Windows.Forms.ToolStripLabel()
        Me.ToolStripButton2 = New System.Windows.Forms.ToolStripButton()
        Me.ToolStripLabel1 = New System.Windows.Forms.ToolStripLabel()
        Me.lblDay = New System.Windows.Forms.ToolStripLabel()
        Me.property_id = New System.Windows.Forms.ToolStripLabel()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.Panel2 = New System.Windows.Forms.Panel()
        Me.Label21 = New System.Windows.Forms.Label()
        Me.Label19 = New System.Windows.Forms.Label()
        Me.Label20 = New System.Windows.Forms.Label()
        Me.Label18 = New System.Windows.Forms.Label()
        Me.Label14 = New System.Windows.Forms.Label()
        Me.Label15 = New System.Windows.Forms.Label()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.Label13 = New System.Windows.Forms.Label()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.payment_collected = New System.Windows.Forms.Label()
        Me.end_contract = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.start_contract = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.monthly_payment = New System.Windows.Forms.Label()
        Me.term = New System.Windows.Forms.Label()
        Me.total_contract_price = New System.Windows.Forms.Label()
        Me.down_payment = New System.Windows.Forms.Label()
        Me.payment_option = New System.Windows.Forms.Label()
        Me.agent = New System.Windows.Forms.Label()
        Me.contract_price = New System.Windows.Forms.Label()
        Me.square_meter = New System.Windows.Forms.Label()
        Me.lot = New System.Windows.Forms.Label()
        Me.block = New System.Windows.Forms.Label()
        Me.property_type = New System.Windows.Forms.Label()
        Me.subdivision = New System.Windows.Forms.Label()
        Me.client_name = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.dgSales = New System.Windows.Forms.DataGridView()
        Me.Column5 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Column1 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Column6 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Column2 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Column3 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Column4 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Column9 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Column10 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Panel3 = New System.Windows.Forms.Panel()
        Me.Label17 = New System.Windows.Forms.Label()
        Me.total_paid = New System.Windows.Forms.Label()
        Me.dtTo = New System.Windows.Forms.DateTimePicker()
        Me.dtFrom = New System.Windows.Forms.DateTimePicker()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label16 = New System.Windows.Forms.Label()
        Me.ToolStrip1.SuspendLayout()
        Me.Panel2.SuspendLayout()
        CType(Me.dgSales, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel3.SuspendLayout()
        Me.SuspendLayout()
        '
        'ToolStrip1
        '
        Me.ToolStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.lblTitle, Me.ToolStripButton2, Me.ToolStripLabel1, Me.lblDay, Me.property_id})
        Me.ToolStrip1.Location = New System.Drawing.Point(0, 5)
        Me.ToolStrip1.Name = "ToolStrip1"
        Me.ToolStrip1.Size = New System.Drawing.Size(723, 25)
        Me.ToolStrip1.TabIndex = 8
        Me.ToolStrip1.Text = "ToolStrip1"
        '
        'lblTitle
        '
        Me.lblTitle.Font = New System.Drawing.Font("Century Gothic", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblTitle.Name = "lblTitle"
        Me.lblTitle.Size = New System.Drawing.Size(50, 22)
        Me.lblTitle.Text = "LEDGER"
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
        'ToolStripLabel1
        '
        Me.ToolStripLabel1.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right
        Me.ToolStripLabel1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text
        Me.ToolStripLabel1.Image = CType(resources.GetObject("ToolStripLabel1.Image"), System.Drawing.Image)
        Me.ToolStripLabel1.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ToolStripLabel1.Name = "ToolStripLabel1"
        Me.ToolStripLabel1.Size = New System.Drawing.Size(22, 22)
        Me.ToolStripLabel1.Text = "     "
        '
        'lblDay
        '
        Me.lblDay.Name = "lblDay"
        Me.lblDay.Size = New System.Drawing.Size(0, 22)
        '
        'property_id
        '
        Me.property_id.Name = "property_id"
        Me.property_id.Size = New System.Drawing.Size(18, 22)
        Me.property_id.Text = "ID"
        Me.property_id.Visible = False
        '
        'Panel1
        '
        Me.Panel1.BackColor = System.Drawing.Color.DarkOrange
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel1.Location = New System.Drawing.Point(0, 0)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(723, 5)
        Me.Panel1.TabIndex = 7
        '
        'Panel2
        '
        Me.Panel2.Controls.Add(Me.Label21)
        Me.Panel2.Controls.Add(Me.Label19)
        Me.Panel2.Controls.Add(Me.Label20)
        Me.Panel2.Controls.Add(Me.Label18)
        Me.Panel2.Controls.Add(Me.Label14)
        Me.Panel2.Controls.Add(Me.Label15)
        Me.Panel2.Controls.Add(Me.Label9)
        Me.Panel2.Controls.Add(Me.Label13)
        Me.Panel2.Controls.Add(Me.Label8)
        Me.Panel2.Controls.Add(Me.Label12)
        Me.Panel2.Controls.Add(Me.Label7)
        Me.Panel2.Controls.Add(Me.Label11)
        Me.Panel2.Controls.Add(Me.Label6)
        Me.Panel2.Controls.Add(Me.Label10)
        Me.Panel2.Controls.Add(Me.payment_collected)
        Me.Panel2.Controls.Add(Me.end_contract)
        Me.Panel2.Controls.Add(Me.Label5)
        Me.Panel2.Controls.Add(Me.start_contract)
        Me.Panel2.Controls.Add(Me.Label4)
        Me.Panel2.Controls.Add(Me.monthly_payment)
        Me.Panel2.Controls.Add(Me.term)
        Me.Panel2.Controls.Add(Me.total_contract_price)
        Me.Panel2.Controls.Add(Me.down_payment)
        Me.Panel2.Controls.Add(Me.payment_option)
        Me.Panel2.Controls.Add(Me.agent)
        Me.Panel2.Controls.Add(Me.contract_price)
        Me.Panel2.Controls.Add(Me.square_meter)
        Me.Panel2.Controls.Add(Me.lot)
        Me.Panel2.Controls.Add(Me.block)
        Me.Panel2.Controls.Add(Me.property_type)
        Me.Panel2.Controls.Add(Me.subdivision)
        Me.Panel2.Controls.Add(Me.client_name)
        Me.Panel2.Controls.Add(Me.Label3)
        Me.Panel2.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel2.Location = New System.Drawing.Point(0, 30)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(723, 235)
        Me.Panel2.TabIndex = 9
        '
        'Label21
        '
        Me.Label21.AutoSize = True
        Me.Label21.BackColor = System.Drawing.Color.Transparent
        Me.Label21.Font = New System.Drawing.Font("Century Gothic", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label21.ForeColor = System.Drawing.Color.Black
        Me.Label21.Location = New System.Drawing.Point(403, 213)
        Me.Label21.Name = "Label21"
        Me.Label21.Size = New System.Drawing.Size(73, 15)
        Me.Label21.TabIndex = 7
        Me.Label21.Text = "COLLECTED :"
        '
        'Label19
        '
        Me.Label19.AutoSize = True
        Me.Label19.BackColor = System.Drawing.Color.Transparent
        Me.Label19.Font = New System.Drawing.Font("Century Gothic", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label19.ForeColor = System.Drawing.Color.Black
        Me.Label19.Location = New System.Drawing.Point(9, 213)
        Me.Label19.Name = "Label19"
        Me.Label19.Size = New System.Drawing.Size(71, 15)
        Me.Label19.TabIndex = 7
        Me.Label19.Text = "PAYMENTS :"
        '
        'Label20
        '
        Me.Label20.AutoSize = True
        Me.Label20.BackColor = System.Drawing.Color.Transparent
        Me.Label20.Font = New System.Drawing.Font("Century Gothic", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label20.ForeColor = System.Drawing.Color.Black
        Me.Label20.Location = New System.Drawing.Point(403, 147)
        Me.Label20.Name = "Label20"
        Me.Label20.Size = New System.Drawing.Size(98, 15)
        Me.Label20.TabIndex = 6
        Me.Label20.Text = "END CONTRACT :"
        '
        'Label18
        '
        Me.Label18.AutoSize = True
        Me.Label18.BackColor = System.Drawing.Color.Transparent
        Me.Label18.Font = New System.Drawing.Font("Century Gothic", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label18.ForeColor = System.Drawing.Color.Black
        Me.Label18.Location = New System.Drawing.Point(403, 131)
        Me.Label18.Name = "Label18"
        Me.Label18.Size = New System.Drawing.Size(106, 15)
        Me.Label18.TabIndex = 6
        Me.Label18.Text = "START CONTRACT :"
        '
        'Label14
        '
        Me.Label14.AutoSize = True
        Me.Label14.BackColor = System.Drawing.Color.Transparent
        Me.Label14.Font = New System.Drawing.Font("Century Gothic", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label14.ForeColor = System.Drawing.Color.Black
        Me.Label14.Location = New System.Drawing.Point(9, 148)
        Me.Label14.Name = "Label14"
        Me.Label14.Size = New System.Drawing.Size(121, 15)
        Me.Label14.TabIndex = 6
        Me.Label14.Text = "MONTHLY PAYMENT :"
        '
        'Label15
        '
        Me.Label15.AutoSize = True
        Me.Label15.BackColor = System.Drawing.Color.Transparent
        Me.Label15.Font = New System.Drawing.Font("Century Gothic", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label15.ForeColor = System.Drawing.Color.Black
        Me.Label15.Location = New System.Drawing.Point(9, 179)
        Me.Label15.Name = "Label15"
        Me.Label15.Size = New System.Drawing.Size(133, 15)
        Me.Label15.TabIndex = 6
        Me.Label15.Text = "SALES PERSON/AGENT :"
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.BackColor = System.Drawing.Color.Transparent
        Me.Label9.Font = New System.Drawing.Font("Century Gothic", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label9.ForeColor = System.Drawing.Color.Black
        Me.Label9.Location = New System.Drawing.Point(9, 130)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(105, 15)
        Me.Label9.TabIndex = 6
        Me.Label9.Text = "CONTRACT PRICE :"
        '
        'Label13
        '
        Me.Label13.AutoSize = True
        Me.Label13.BackColor = System.Drawing.Color.Transparent
        Me.Label13.Font = New System.Drawing.Font("Century Gothic", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label13.ForeColor = System.Drawing.Color.Black
        Me.Label13.Location = New System.Drawing.Point(403, 114)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(41, 15)
        Me.Label13.TabIndex = 6
        Me.Label13.Text = "TERM :"
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.BackColor = System.Drawing.Color.Transparent
        Me.Label8.Font = New System.Drawing.Font("Century Gothic", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label8.ForeColor = System.Drawing.Color.Black
        Me.Label8.Location = New System.Drawing.Point(9, 114)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(93, 15)
        Me.Label8.TabIndex = 6
        Me.Label8.Text = "SQUARE METER :"
        '
        'Label12
        '
        Me.Label12.AutoSize = True
        Me.Label12.BackColor = System.Drawing.Color.Transparent
        Me.Label12.Font = New System.Drawing.Font("Century Gothic", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label12.ForeColor = System.Drawing.Color.Black
        Me.Label12.Location = New System.Drawing.Point(403, 98)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(140, 15)
        Me.Label12.TabIndex = 6
        Me.Label12.Text = "TOTAL CONTRACT PRICE :"
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.BackColor = System.Drawing.Color.Transparent
        Me.Label7.Font = New System.Drawing.Font("Century Gothic", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label7.ForeColor = System.Drawing.Color.Black
        Me.Label7.Location = New System.Drawing.Point(9, 98)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(32, 15)
        Me.Label7.TabIndex = 6
        Me.Label7.Text = "LOT :"
        '
        'Label11
        '
        Me.Label11.AutoSize = True
        Me.Label11.BackColor = System.Drawing.Color.Transparent
        Me.Label11.Font = New System.Drawing.Font("Century Gothic", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label11.ForeColor = System.Drawing.Color.Black
        Me.Label11.Location = New System.Drawing.Point(403, 82)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(104, 15)
        Me.Label11.TabIndex = 6
        Me.Label11.Text = "DOWN PAYMENT :"
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.BackColor = System.Drawing.Color.Transparent
        Me.Label6.Font = New System.Drawing.Font("Century Gothic", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.ForeColor = System.Drawing.Color.Black
        Me.Label6.Location = New System.Drawing.Point(9, 82)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(49, 15)
        Me.Label6.TabIndex = 6
        Me.Label6.Text = "BLOCK :"
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.BackColor = System.Drawing.Color.Transparent
        Me.Label10.Font = New System.Drawing.Font("Century Gothic", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label10.ForeColor = System.Drawing.Color.Black
        Me.Label10.Location = New System.Drawing.Point(403, 66)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(109, 15)
        Me.Label10.TabIndex = 5
        Me.Label10.Text = "PAYMENT OPTION :"
        '
        'payment_collected
        '
        Me.payment_collected.AutoSize = True
        Me.payment_collected.BackColor = System.Drawing.Color.Transparent
        Me.payment_collected.Font = New System.Drawing.Font("Century Gothic", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.payment_collected.ForeColor = System.Drawing.Color.Black
        Me.payment_collected.Location = New System.Drawing.Point(482, 213)
        Me.payment_collected.Name = "payment_collected"
        Me.payment_collected.Size = New System.Drawing.Size(122, 16)
        Me.payment_collected.TabIndex = 3
        Me.payment_collected.Text = "___________________"
        '
        'end_contract
        '
        Me.end_contract.AutoSize = True
        Me.end_contract.BackColor = System.Drawing.Color.Transparent
        Me.end_contract.Font = New System.Drawing.Font("Century Gothic", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.end_contract.ForeColor = System.Drawing.Color.Black
        Me.end_contract.Location = New System.Drawing.Point(507, 147)
        Me.end_contract.Name = "end_contract"
        Me.end_contract.Size = New System.Drawing.Size(122, 16)
        Me.end_contract.TabIndex = 3
        Me.end_contract.Text = "___________________"
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.BackColor = System.Drawing.Color.Transparent
        Me.Label5.Font = New System.Drawing.Font("Century Gothic", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.ForeColor = System.Drawing.Color.Black
        Me.Label5.Location = New System.Drawing.Point(9, 66)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(38, 15)
        Me.Label5.TabIndex = 5
        Me.Label5.Text = "TYPE :"
        '
        'start_contract
        '
        Me.start_contract.AutoSize = True
        Me.start_contract.BackColor = System.Drawing.Color.Transparent
        Me.start_contract.Font = New System.Drawing.Font("Century Gothic", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.start_contract.ForeColor = System.Drawing.Color.Black
        Me.start_contract.Location = New System.Drawing.Point(515, 131)
        Me.start_contract.Name = "start_contract"
        Me.start_contract.Size = New System.Drawing.Size(122, 16)
        Me.start_contract.TabIndex = 3
        Me.start_contract.Text = "___________________"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.BackColor = System.Drawing.Color.Transparent
        Me.Label4.Font = New System.Drawing.Font("Century Gothic", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.ForeColor = System.Drawing.Color.Black
        Me.Label4.Location = New System.Drawing.Point(9, 40)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(139, 15)
        Me.Label4.TabIndex = 4
        Me.Label4.Text = "PROPERTY APPLIED FOR :"
        '
        'monthly_payment
        '
        Me.monthly_payment.AutoSize = True
        Me.monthly_payment.BackColor = System.Drawing.Color.Transparent
        Me.monthly_payment.Font = New System.Drawing.Font("Century Gothic", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.monthly_payment.ForeColor = System.Drawing.Color.Black
        Me.monthly_payment.Location = New System.Drawing.Point(130, 148)
        Me.monthly_payment.Name = "monthly_payment"
        Me.monthly_payment.Size = New System.Drawing.Size(122, 16)
        Me.monthly_payment.TabIndex = 3
        Me.monthly_payment.Text = "___________________"
        '
        'term
        '
        Me.term.AutoSize = True
        Me.term.BackColor = System.Drawing.Color.Transparent
        Me.term.Font = New System.Drawing.Font("Century Gothic", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.term.ForeColor = System.Drawing.Color.Black
        Me.term.Location = New System.Drawing.Point(444, 114)
        Me.term.Name = "term"
        Me.term.Size = New System.Drawing.Size(122, 16)
        Me.term.TabIndex = 3
        Me.term.Text = "___________________"
        '
        'total_contract_price
        '
        Me.total_contract_price.AutoSize = True
        Me.total_contract_price.BackColor = System.Drawing.Color.Transparent
        Me.total_contract_price.Font = New System.Drawing.Font("Century Gothic", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.total_contract_price.ForeColor = System.Drawing.Color.Black
        Me.total_contract_price.Location = New System.Drawing.Point(543, 98)
        Me.total_contract_price.Name = "total_contract_price"
        Me.total_contract_price.Size = New System.Drawing.Size(122, 16)
        Me.total_contract_price.TabIndex = 3
        Me.total_contract_price.Text = "___________________"
        '
        'down_payment
        '
        Me.down_payment.AutoSize = True
        Me.down_payment.BackColor = System.Drawing.Color.Transparent
        Me.down_payment.Font = New System.Drawing.Font("Century Gothic", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.down_payment.ForeColor = System.Drawing.Color.Black
        Me.down_payment.Location = New System.Drawing.Point(507, 82)
        Me.down_payment.Name = "down_payment"
        Me.down_payment.Size = New System.Drawing.Size(122, 16)
        Me.down_payment.TabIndex = 3
        Me.down_payment.Text = "___________________"
        '
        'payment_option
        '
        Me.payment_option.AutoSize = True
        Me.payment_option.BackColor = System.Drawing.Color.Transparent
        Me.payment_option.Font = New System.Drawing.Font("Century Gothic", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.payment_option.ForeColor = System.Drawing.Color.Black
        Me.payment_option.Location = New System.Drawing.Point(512, 66)
        Me.payment_option.Name = "payment_option"
        Me.payment_option.Size = New System.Drawing.Size(122, 16)
        Me.payment_option.TabIndex = 3
        Me.payment_option.Text = "___________________"
        '
        'agent
        '
        Me.agent.AutoSize = True
        Me.agent.BackColor = System.Drawing.Color.Transparent
        Me.agent.Font = New System.Drawing.Font("Century Gothic", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.agent.ForeColor = System.Drawing.Color.Black
        Me.agent.Location = New System.Drawing.Point(142, 179)
        Me.agent.Name = "agent"
        Me.agent.Size = New System.Drawing.Size(122, 16)
        Me.agent.TabIndex = 3
        Me.agent.Text = "___________________"
        '
        'contract_price
        '
        Me.contract_price.AutoSize = True
        Me.contract_price.BackColor = System.Drawing.Color.Transparent
        Me.contract_price.Font = New System.Drawing.Font("Century Gothic", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.contract_price.ForeColor = System.Drawing.Color.Black
        Me.contract_price.Location = New System.Drawing.Point(114, 130)
        Me.contract_price.Name = "contract_price"
        Me.contract_price.Size = New System.Drawing.Size(122, 16)
        Me.contract_price.TabIndex = 3
        Me.contract_price.Text = "___________________"
        '
        'square_meter
        '
        Me.square_meter.AutoSize = True
        Me.square_meter.BackColor = System.Drawing.Color.Transparent
        Me.square_meter.Font = New System.Drawing.Font("Century Gothic", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.square_meter.ForeColor = System.Drawing.Color.Black
        Me.square_meter.Location = New System.Drawing.Point(96, 114)
        Me.square_meter.Name = "square_meter"
        Me.square_meter.Size = New System.Drawing.Size(122, 16)
        Me.square_meter.TabIndex = 3
        Me.square_meter.Text = "___________________"
        '
        'lot
        '
        Me.lot.AutoSize = True
        Me.lot.BackColor = System.Drawing.Color.Transparent
        Me.lot.Font = New System.Drawing.Font("Century Gothic", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lot.ForeColor = System.Drawing.Color.Black
        Me.lot.Location = New System.Drawing.Point(41, 98)
        Me.lot.Name = "lot"
        Me.lot.Size = New System.Drawing.Size(122, 16)
        Me.lot.TabIndex = 3
        Me.lot.Text = "___________________"
        '
        'block
        '
        Me.block.AutoSize = True
        Me.block.BackColor = System.Drawing.Color.Transparent
        Me.block.Font = New System.Drawing.Font("Century Gothic", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.block.ForeColor = System.Drawing.Color.Black
        Me.block.Location = New System.Drawing.Point(54, 82)
        Me.block.Name = "block"
        Me.block.Size = New System.Drawing.Size(122, 16)
        Me.block.TabIndex = 3
        Me.block.Text = "___________________"
        '
        'property_type
        '
        Me.property_type.AutoSize = True
        Me.property_type.BackColor = System.Drawing.Color.Transparent
        Me.property_type.Font = New System.Drawing.Font("Century Gothic", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.property_type.ForeColor = System.Drawing.Color.Black
        Me.property_type.Location = New System.Drawing.Point(47, 66)
        Me.property_type.Name = "property_type"
        Me.property_type.Size = New System.Drawing.Size(122, 16)
        Me.property_type.TabIndex = 3
        Me.property_type.Text = "___________________"
        '
        'subdivision
        '
        Me.subdivision.AutoSize = True
        Me.subdivision.BackColor = System.Drawing.Color.Transparent
        Me.subdivision.Font = New System.Drawing.Font("Century Gothic", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.subdivision.ForeColor = System.Drawing.Color.Black
        Me.subdivision.Location = New System.Drawing.Point(148, 39)
        Me.subdivision.Name = "subdivision"
        Me.subdivision.Size = New System.Drawing.Size(122, 16)
        Me.subdivision.TabIndex = 3
        Me.subdivision.Text = "___________________"
        '
        'client_name
        '
        Me.client_name.AutoSize = True
        Me.client_name.BackColor = System.Drawing.Color.Transparent
        Me.client_name.Font = New System.Drawing.Font("Century Gothic", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.client_name.ForeColor = System.Drawing.Color.Black
        Me.client_name.Location = New System.Drawing.Point(96, 10)
        Me.client_name.Name = "client_name"
        Me.client_name.Size = New System.Drawing.Size(122, 16)
        Me.client_name.TabIndex = 3
        Me.client_name.Text = "___________________"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.BackColor = System.Drawing.Color.Transparent
        Me.Label3.Font = New System.Drawing.Font("Century Gothic", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.ForeColor = System.Drawing.Color.Black
        Me.Label3.Location = New System.Drawing.Point(9, 10)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(84, 15)
        Me.Label3.TabIndex = 3
        Me.Label3.Text = "CLIENT NAME :"
        '
        'dgSales
        '
        Me.dgSales.AllowUserToAddRows = False
        Me.dgSales.AllowUserToDeleteRows = False
        Me.dgSales.AllowUserToResizeColumns = False
        Me.dgSales.AllowUserToResizeRows = False
        Me.dgSales.BackgroundColor = System.Drawing.SystemColors.Control
        Me.dgSales.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.dgSales.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleHorizontal
        Me.dgSales.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None
        DataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle1.BackColor = System.Drawing.Color.MidnightBlue
        DataGridViewCellStyle1.Font = New System.Drawing.Font("Century Gothic", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle1.ForeColor = System.Drawing.Color.White
        DataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.dgSales.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle1
        Me.dgSales.ColumnHeadersHeight = 30
        Me.dgSales.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing
        Me.dgSales.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.Column5, Me.Column1, Me.Column6, Me.Column2, Me.Column3, Me.Column4, Me.Column9, Me.Column10})
        DataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle4.BackColor = System.Drawing.SystemColors.Window
        DataGridViewCellStyle4.Font = New System.Drawing.Font("Century Gothic", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle4.ForeColor = System.Drawing.Color.Black
        DataGridViewCellStyle4.SelectionBackColor = System.Drawing.Color.Silver
        DataGridViewCellStyle4.SelectionForeColor = System.Drawing.Color.Black
        DataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
        Me.dgSales.DefaultCellStyle = DataGridViewCellStyle4
        Me.dgSales.Dock = System.Windows.Forms.DockStyle.Fill
        Me.dgSales.EnableHeadersVisualStyles = False
        Me.dgSales.Location = New System.Drawing.Point(0, 296)
        Me.dgSales.Name = "dgSales"
        Me.dgSales.ReadOnly = True
        Me.dgSales.RowHeadersVisible = False
        Me.dgSales.RowTemplate.Height = 35
        Me.dgSales.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.dgSales.Size = New System.Drawing.Size(723, 196)
        Me.dgSales.TabIndex = 18
        '
        'Column5
        '
        Me.Column5.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells
        Me.Column5.HeaderText = "#"
        Me.Column5.Name = "Column5"
        Me.Column5.ReadOnly = True
        Me.Column5.Width = 39
        '
        'Column1
        '
        Me.Column1.HeaderText = "ID"
        Me.Column1.Name = "Column1"
        Me.Column1.ReadOnly = True
        Me.Column1.Visible = False
        '
        'Column6
        '
        Me.Column6.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill
        Me.Column6.HeaderText = "TRANSACTION NO."
        Me.Column6.Name = "Column6"
        Me.Column6.ReadOnly = True
        '
        'Column2
        '
        Me.Column2.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill
        Me.Column2.HeaderText = "CLIENT"
        Me.Column2.Name = "Column2"
        Me.Column2.ReadOnly = True
        Me.Column2.Visible = False
        '
        'Column3
        '
        Me.Column3.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells
        DataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        Me.Column3.DefaultCellStyle = DataGridViewCellStyle2
        Me.Column3.HeaderText = "PAYMENT METHOD"
        Me.Column3.Name = "Column3"
        Me.Column3.ReadOnly = True
        Me.Column3.Width = 133
        '
        'Column4
        '
        Me.Column4.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells
        DataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        Me.Column4.DefaultCellStyle = DataGridViewCellStyle3
        Me.Column4.HeaderText = "AMOUNT"
        Me.Column4.Name = "Column4"
        Me.Column4.ReadOnly = True
        Me.Column4.Width = 80
        '
        'Column9
        '
        Me.Column9.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells
        Me.Column9.HeaderText = "DATE"
        Me.Column9.Name = "Column9"
        Me.Column9.ReadOnly = True
        Me.Column9.Width = 57
        '
        'Column10
        '
        Me.Column10.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells
        Me.Column10.HeaderText = "REMARK"
        Me.Column10.Name = "Column10"
        Me.Column10.ReadOnly = True
        Me.Column10.Width = 76
        '
        'Panel3
        '
        Me.Panel3.Controls.Add(Me.Label17)
        Me.Panel3.Controls.Add(Me.total_paid)
        Me.Panel3.Controls.Add(Me.dtTo)
        Me.Panel3.Controls.Add(Me.dtFrom)
        Me.Panel3.Controls.Add(Me.Label1)
        Me.Panel3.Controls.Add(Me.Label2)
        Me.Panel3.Controls.Add(Me.Label16)
        Me.Panel3.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel3.Location = New System.Drawing.Point(0, 265)
        Me.Panel3.Name = "Panel3"
        Me.Panel3.Size = New System.Drawing.Size(723, 31)
        Me.Panel3.TabIndex = 17
        '
        'Label17
        '
        Me.Label17.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label17.AutoSize = True
        Me.Label17.BackColor = System.Drawing.Color.Transparent
        Me.Label17.Cursor = System.Windows.Forms.Cursors.Hand
        Me.Label17.Font = New System.Drawing.Font("Century Gothic", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label17.ForeColor = System.Drawing.Color.Black
        Me.Label17.Location = New System.Drawing.Point(671, 9)
        Me.Label17.Name = "Label17"
        Me.Label17.Size = New System.Drawing.Size(47, 16)
        Me.Label17.TabIndex = 8
        Me.Label17.Text = "[PRINT]"
        '
        'total_paid
        '
        Me.total_paid.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.total_paid.BackColor = System.Drawing.Color.Transparent
        Me.total_paid.Font = New System.Drawing.Font("Century Gothic", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.total_paid.ForeColor = System.Drawing.Color.Black
        Me.total_paid.Location = New System.Drawing.Point(477, 9)
        Me.total_paid.Name = "total_paid"
        Me.total_paid.Size = New System.Drawing.Size(188, 16)
        Me.total_paid.TabIndex = 4
        Me.total_paid.Text = "0.00"
        Me.total_paid.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.total_paid.Visible = False
        '
        'dtTo
        '
        Me.dtTo.CustomFormat = "yyyy-MM-dd"
        Me.dtTo.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtTo.Location = New System.Drawing.Point(269, 5)
        Me.dtTo.Name = "dtTo"
        Me.dtTo.Size = New System.Drawing.Size(112, 21)
        Me.dtTo.TabIndex = 3
        '
        'dtFrom
        '
        Me.dtFrom.CustomFormat = "yyyy-MM-dd"
        Me.dtFrom.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtFrom.Location = New System.Drawing.Point(152, 5)
        Me.dtFrom.Name = "dtFrom"
        Me.dtFrom.Size = New System.Drawing.Size(112, 21)
        Me.dtFrom.TabIndex = 3
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.BackColor = System.Drawing.Color.Transparent
        Me.Label1.Font = New System.Drawing.Font("Century Gothic", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.ForeColor = System.Drawing.Color.Black
        Me.Label1.Location = New System.Drawing.Point(251, 7)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(12, 16)
        Me.Label1.TabIndex = 2
        Me.Label1.Text = "-"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.BackColor = System.Drawing.Color.Transparent
        Me.Label2.Font = New System.Drawing.Font("Century Gothic", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.ForeColor = System.Drawing.Color.Black
        Me.Label2.Location = New System.Drawing.Point(9, 7)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(141, 16)
        Me.Label2.TabIndex = 2
        Me.Label2.Text = "FILTER DATE  [FROM - TO]"
        '
        'Label16
        '
        Me.Label16.AutoSize = True
        Me.Label16.BackColor = System.Drawing.Color.Transparent
        Me.Label16.Cursor = System.Windows.Forms.Cursors.Hand
        Me.Label16.Font = New System.Drawing.Font("Century Gothic", 15.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label16.ForeColor = System.Drawing.Color.Black
        Me.Label16.Location = New System.Drawing.Point(382, 1)
        Me.Label16.Name = "Label16"
        Me.Label16.Size = New System.Drawing.Size(27, 25)
        Me.Label16.TabIndex = 7
        Me.Label16.Text = "↻"
        '
        'frmLedger
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(723, 492)
        Me.ControlBox = False
        Me.Controls.Add(Me.dgSales)
        Me.Controls.Add(Me.Panel3)
        Me.Controls.Add(Me.Panel2)
        Me.Controls.Add(Me.ToolStrip1)
        Me.Controls.Add(Me.Panel1)
        Me.Font = New System.Drawing.Font("Century Gothic", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.Name = "frmLedger"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        Me.ToolStrip1.ResumeLayout(False)
        Me.ToolStrip1.PerformLayout()
        Me.Panel2.ResumeLayout(False)
        Me.Panel2.PerformLayout()
        CType(Me.dgSales, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel3.ResumeLayout(False)
        Me.Panel3.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents ToolStrip1 As ToolStrip
    Friend WithEvents lblTitle As ToolStripLabel
    Friend WithEvents ToolStripButton2 As ToolStripButton
    Friend WithEvents ToolStripLabel1 As ToolStripLabel
    Friend WithEvents lblDay As ToolStripLabel
    Friend WithEvents property_id As ToolStripLabel
    Friend WithEvents Panel1 As Panel
    Friend WithEvents Panel2 As Panel
    Friend WithEvents Label3 As Label
    Friend WithEvents dgSales As DataGridView
    Friend WithEvents Panel3 As Panel
    Friend WithEvents dtTo As DateTimePicker
    Friend WithEvents dtFrom As DateTimePicker
    Friend WithEvents Label1 As Label
    Friend WithEvents Label2 As Label
    Friend WithEvents total_paid As Label
    Friend WithEvents Label14 As Label
    Friend WithEvents Label15 As Label
    Friend WithEvents Label9 As Label
    Friend WithEvents Label13 As Label
    Friend WithEvents Label8 As Label
    Friend WithEvents Label12 As Label
    Friend WithEvents Label7 As Label
    Friend WithEvents Label11 As Label
    Friend WithEvents Label6 As Label
    Friend WithEvents Label10 As Label
    Friend WithEvents Label5 As Label
    Friend WithEvents Label4 As Label
    Friend WithEvents monthly_payment As Label
    Friend WithEvents term As Label
    Friend WithEvents total_contract_price As Label
    Friend WithEvents down_payment As Label
    Friend WithEvents payment_option As Label
    Friend WithEvents agent As Label
    Friend WithEvents contract_price As Label
    Friend WithEvents square_meter As Label
    Friend WithEvents lot As Label
    Friend WithEvents block As Label
    Friend WithEvents property_type As Label
    Friend WithEvents subdivision As Label
    Friend WithEvents client_name As Label
    Friend WithEvents Label16 As Label
    Friend WithEvents Column5 As DataGridViewTextBoxColumn
    Friend WithEvents Column1 As DataGridViewTextBoxColumn
    Friend WithEvents Column6 As DataGridViewTextBoxColumn
    Friend WithEvents Column2 As DataGridViewTextBoxColumn
    Friend WithEvents Column3 As DataGridViewTextBoxColumn
    Friend WithEvents Column4 As DataGridViewTextBoxColumn
    Friend WithEvents Column9 As DataGridViewTextBoxColumn
    Friend WithEvents Column10 As DataGridViewTextBoxColumn
    Friend WithEvents Label20 As Label
    Friend WithEvents Label18 As Label
    Friend WithEvents end_contract As Label
    Friend WithEvents start_contract As Label
    Friend WithEvents Label17 As Label
    Friend WithEvents Label19 As Label
    Friend WithEvents Label21 As Label
    Friend WithEvents payment_collected As Label
End Class
