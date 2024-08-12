Imports MySql.Data.MySqlClient
Imports System.IO
Public Class frmDiscount
    Private Sub ToolStripButton1_Click(sender As Object, e As EventArgs) Handles ToolStripButton1.Click
        With frmAgentAdd
            .btnSave.Enabled = True
            .btnUpdate.Enabled = False
            .txtFname.Focus()
            .ShowDialog()
        End With
    End Sub

    Private Sub ToolStripButton2_Click(sender As Object, e As EventArgs) Handles ToolStripButton2.Click
        Me.Dispose()
    End Sub

    Sub loadAgentRecords()
        dgAgentList.Rows.Clear()
        Dim i As Integer
        cn.Close()
        cn.Open()
        cm = New MySqlCommand("select * from tblagent where (fname like '%" & ToolStripTextBox1.Text & "%' or mname like '%" & ToolStripTextBox1.Text & "%' or lname like '%" & ToolStripTextBox1.Text & "%')", cn)
        dr = cm.ExecuteReader
        While dr.Read
            i += 1
            dgAgentList.Rows.Add(i, dr.Item("id").ToString, dr.Item("lname").ToString, dr.Item("fname").ToString, dr.Item("mname").ToString, dr.Item("address").ToString, dr.Item("image"))
        End While
        dr.Close()
        cn.Close()
        'AgentloadPropertyRecords()
    End Sub

    Private Sub dgClientList_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgAgentList.CellContentClick
        Dim colname As String = dgAgentList.Columns(e.ColumnIndex).Name
        If colname = "colEdit" Then

            With frmAgentAdd
                cn.Open()
                cm = New MySqlCommand("select image from tblagent where id = '" & dgAgentList.Rows(e.RowIndex).Cells(1).Value.ToString & "'", cn)
                dr = cm.ExecuteReader
                While dr.Read
                    Dim len As Long = dr.GetBytes(0, 0, Nothing, 0, 0)
                    Dim array(CInt(len)) As Byte
                    dr.GetBytes(0, 0, array, 0, CInt(len))
                    Dim ms As New MemoryStream(array)
                    Dim bitmap As New System.Drawing.Bitmap(ms)
                    .AgentImage.BackgroundImage = bitmap
                    .txt_ID.Text = dgAgentList.Rows(e.RowIndex).Cells(1).Value.ToString
                End While
                dr.Close()
                cn.Close()
                .btnUpdate.Enabled = True
                .btnSave.Enabled = False

                cn.Close()
                cn.Open()
                cm = New MySqlCommand("select * from tblagent where id = @1", cn)
                With cm
                    .Parameters.AddWithValue("@1", dgAgentList.Rows(e.RowIndex).Cells(1).Value.ToString)
                End With
                dr = cm.ExecuteReader
                dr.Read()
                If dr.HasRows Then
                    .txtFname.Text = dr.Item("fname").ToString
                    .txtMname.Text = dr.Item("mname").ToString
                    .txtLname.Text = dr.Item("lname").ToString
                    .txtContact.Text = dr.Item("contact").ToString
                    .txtAddress.Text = dr.Item("address").ToString
                    .txtEmail.Text = dr.Item("email").ToString
                    .txtFBAccount.Text = dr.Item("fbaccount").ToString
                Else
                End If
                dr.Close()
                cn.Close()

                .ShowDialog()
            End With
        ElseIf colname = "colDelete" Then
            'cn.Open()
            'cm = New MySqlCommand("select image from tblproduct where id = '" & dgProductList.Rows(e.RowIndex).Cells(1).Value.ToString & "'", cn)
            'cm.ExecuteNonQuery()
            'cn.Close()
            'AuditTrail("Deleted a product; description " & dgProductList.Rows(e.RowIndex).Cells(1).Value.ToString & "; category " & dgProductList.Rows(e.RowIndex).Cells(4).Value.ToString & "; weight " & dgProductList.Rows(e.RowIndex).Cells(5).Value.ToString & "; price " & dgProductList.Rows(e.RowIndex).Cells(3).Value.ToString & " where id " & dgProductList.Rows(e.RowIndex).Cells(1).Value.ToString & "")
            'MsgBox("Product successfully deleted!", vbInformation)
            'DashBoard()

            MsgBox("Unable to delete Agent!", vbCritical)
        Else
            AgentloadPropertyRecords()
        End If
    End Sub

    Sub AgentloadPropertyRecords()
        dgPropertyList.Rows.Clear()
        Dim i As Integer
        cn.Close()
        cn.Open()
        cm = New MySqlCommand("SELECT t1.`id`, CONCAT(t3.lname, ', ' ,t3.fname,' ',t3.mname) as client, t2.`name` as type, t1.`block`, t1.`lot`, t1.`square_meter`, t1.`price`, t1.`status`, CONCAT(t4.lname, ', ' ,t4.fname,' ',t4.mname) as 'agent_id', t1.`thumbnail`, t1.`monthly_amort`, DATE_FORMAT(t1.`start_contract`, '%m/%d/%Y') as 'start_contract', DATE_FORMAT(t1.`end_contract`, '%m/%d/%Y') as 'end_contract', t1.`length_years`, t1.`interest_rate`, t1.`property_date` as 'property_date', t1.`status`, t5.total_payments, t1.payment_option, t1.down_payment, t1.property_type, t1.interest_rate, t6.commission_percentage, t6.commision_months, t1.price * (t6.commission_percentage *0.01) as commission_total, (t1.price * (t6.commission_percentage *0.01)) / t6.commision_months as commission_monthly  FROM `tblproperty` t1 JOIN `tblpropertytype` t2 ON t1.type_id = t2.id JOIN `tblclient` t3 ON t1.client_id = t3.id JOIN `tblagent` t4 ON t1.`agent_id` = t4.`id` JOIN accountsreceivable t5 ON t1.id = t5.id JOIN tblcomission t6 ON t1.commission = t6.id where (CONCAT(t3.lname, ', ' ,t3.fname,' ',t3.mname) like '%" & ToolStripTextBox2.Text & "%' or t2.`name` like '%" & ToolStripTextBox2.Text & "%' or t1.`id` like '%" & ToolStripTextBox2.Text & "%') order by t1.`start_contract` desc", cn)
        'cm = New MySqlCommand("SELECT t1.`id`, CONCAT(t3.lname, ', ' ,t3.fname,' ',t3.mname) as client, t2.`name` as type, t1.`block`, t1.`lot`, t1.`square_meter`, t1.`price`, t1.`status`, CONCAT(t4.lname, ', ' ,t4.fname,' ',t4.mname) as 'agent_id', t1.`thumbnail`, t1.`monthly_amort`, DATE_FORMAT(t1.`start_contract`, '%b %d, %Y') as 'start_contract', DATE_FORMAT(t1.`end_contract`, '%b %d, %Y') as 'end_contract', t1.`length_years`, t1.`interest_rate`, date_format(t1.`property_date`, '%b %d, %Y') as 'property_date', t1.`status`, t5.total_payments, t1.payment_option, t1.down_payment, t1.property_type, t1.interest_rate, t1.commission, t1.penalty FROM `tblproperty` t1 JOIN `tblpropertytype` t2 ON t1.type_id = t2.id JOIN `tblclient` t3 ON t1.client_id = t3.id JOIN `tblagent` t4 ON t1.`agent_id` = t4.`id` JOIN accountsreceivable t5 ON t1.id = t5.id where t1.client_id = " & CInt(dgAgentList.CurrentRow.Cells(1).Value) & " order by t1.`start_contract` desc", cn)

        dr = cm.ExecuteReader
        While dr.Read
            i += 1
            dgPropertyList.Rows.Add(i, dr.Item("id").ToString, dr.Item("client").ToString, dr.Item("type").ToString, dr.Item("block").ToString, dr.Item("lot").ToString, dr.Item("square_meter").ToString, Format(CDec(dr.Item("price").ToString), "#,##0.00"), dr.Item("status").ToString, dr.Item("thumbnail"), dr.Item("agent_id").ToString, dr.Item("monthly_amort").ToString, dr.Item("start_contract").ToString, dr.Item("end_contract").ToString, dr.Item("length_years").ToString, dr.Item("interest_rate").ToString, Format(Convert.ToDateTime(dr.Item("property_date").ToString), "MMM dd, yyyy"), dr.Item("status").ToString, dr.Item("total_payments").ToString, dr.Item("payment_option").ToString, dr.Item("down_payment").ToString, dr.Item("property_type").ToString, dr.Item("interest_rate").ToString, dr.Item("commission_percentage").ToString & " %", dr.Item("commision_months").ToString & " Months", Format(CDec(dr.Item("commission_total").ToString), "#,##0.00"), CDec(dr.Item("commission_monthly").ToString), "👁️‍🗨️")
        End While
        dr.Close()
        dgPropertyList.Columns("Column9").DefaultCellStyle.Format = "MM/dd/yyyy"
        'For i = 0 To dgPropertyList.Rows.Count - 1
        '    Dim r As DataGridViewRow = dgPropertyList.Rows(i)
        '    r.Height = 50
        'Next
        'If dgPropertyList.Rows.Count = 0 Then
        'Else
        '    Dim imageColumn = DirectCast(dgPropertyList.Columns("Column6"), DataGridViewImageColumn)
        '    imageColumn.ImageLayout = DataGridViewImageCellLayout.Stretch
        'End If
        cn.Close()
    End Sub

    Private Sub frmProductList_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.KeyPreview = True
    End Sub

    Private Sub frmProductList_KeyDown(sender As Object, e As KeyEventArgs) Handles Me.KeyDown
        If e.KeyCode = Keys.Escape Then
            ToolStripButton2_Click(sender, e)
        End If
    End Sub
    Private Sub ToolStripTextBox1_TextChanged(sender As Object, e As EventArgs) Handles ToolStripTextBox1.TextChanged
        loadAgentRecords()
    End Sub

    Private Sub ToolStripButton3_Click(sender As Object, e As EventArgs)

    End Sub

    Private Sub dgAgentList_SelectionChanged(sender As Object, e As EventArgs) Handles dgAgentList.SelectionChanged

    End Sub

    Private Sub ToolStripButton4_Click(sender As Object, e As EventArgs) Handles ToolStripButton4.Click
        MsgBox("Unable to enter! Function is under maintenance.", vbCritical)
        'With frmComission
        '    .btnSave.Enabled = True
        '    .btnUpdate.Enabled = False
        '    .txtPercent.Focus()
        '    .ShowDialog()
        'End With
    End Sub

    Private Sub dgPropertyList_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgPropertyList.CellContentClick
        Dim colname As String = dgPropertyList.Columns(e.ColumnIndex).Name
        If colname = "colView" Then
            With frmCommisionSched
                .ToolStripLabelAGENTID.Text = dgAgentList.CurrentRow.Cells(1).Value
                .ToolStripLabelID.Text = dgPropertyList.CurrentRow.Cells(1).Value
                .ToolStripLabelProperty.Text = "Client: " & dgPropertyList.CurrentRow.Cells(2).Value & dgPropertyList.CurrentRow.Cells(3).Value & " Block " & dgPropertyList.CurrentRow.Cells(4).Value & " Lot " & dgPropertyList.CurrentRow.Cells(5).Value
                .ToolStripLabelAgent.Text = "Agent: " & dgAgentList.CurrentRow.Cells(2).Value & ", " & dgAgentList.CurrentRow.Cells(3).Value & " " & dgAgentList.CurrentRow.Cells(4).Value
                .calculatecommission_agent()
                OpenFormToAdminPanel(frmCommisionSched)
            End With
        End If
    End Sub

    Private Sub dgPropertyList_SelectionChanged(sender As Object, e As EventArgs) Handles dgPropertyList.SelectionChanged

    End Sub
End Class