Imports MySql.Data.MySqlClient
Public Class frmSalesDetails
    Dim credRole As String
    Private Sub ToolStripButton2_Click(sender As Object, e As EventArgs) Handles ToolStripButton2.Click
        Me.Dispose()
    End Sub

    Sub loadSales()
        Try
            Dim _total As Double
            dgSales.Rows.Clear()
            Dim sdate As String = Format(Convert.ToDateTime(frmLogin.txtbox_date.Text), "yyyy/MM/dd")
            Dim i As Integer
            If str_role = "Administrator" Then
                cn.Close()
                cn.Open()
                cm = New MySqlCommand("SELECT t1.`id`, t1.`transaction_no`, CONCAT(t2.lname, ', ', t2.fname, ' ',t2.mname) as 'client', CONCAT(t1.`property_id`,' - ', t4.name) as 'property_id', t1.`amount`, DATE_FORMAT(t1.`date_paid`, '%m/%d/%Y') as 'date_paid', t5.`name` FROM `tblpayment` t1 JOIN `tblclient` t2 ON t1.client_id = t2.id JOIN `tblproperty` t3 ON t1.`property_id` = t3.`id` JOIN `tblpropertytype` t4 ON t3.`type_id` = t4.`id` JOIN `tbluser` t5 ON t1.`user` = t5.`username` where t1.payment_status = 'COMPLETED' and t1.`date_paid` between '" & sdate & "' and '" & sdate & "'", cn)
                dr = cm.ExecuteReader
                While dr.Read
                    i += 1
                    _total += CDbl(dr.Item("amount").ToString)
                    dgSales.Rows.Add(i, dr.Item("id").ToString, dr.Item("transaction_no").ToString, dr.Item("client").ToString, dr.Item("property_id").ToString, Format(CDec(dr.Item("amount").ToString), "#,##0.00"), dr.Item("date_paid").ToString, dr.Item("name").ToString)
                End While
                dr.Close()
                cn.Close()
            Else
                cn.Close()
                cn.Open()
                cm = New MySqlCommand("SELECT t1.`id`, t1.`transaction_no`, CONCAT(t2.lname, ', ', t2.fname, ' ',t2.mname) as 'client', CONCAT(t1.`property_id`,' - ', t4.name) as 'property_id', t1.`amount`, DATE_FORMAT(t1.`date_paid`, '%m/%d/%Y') as 'date_paid', t5.`name` FROM `tblpayment` t1 JOIN `tblclient` t2 ON t1.client_id = t2.id JOIN `tblproperty` t3 ON t1.`property_id` = t3.`id` JOIN `tblpropertytype` t4 ON t3.`type_id` = t4.`id` JOIN `tbluser` t5 ON t1.`user` = t5.`username` where t1.user = '" & str_user & "' and t1.payment_status = 'COMPLETED' and t1.`date_paid` between '" & sdate & "' and '" & sdate & "'", cn)
                dr = cm.ExecuteReader
                While dr.Read
                    i += 1
                    _total += CDbl(dr.Item("amount").ToString)
                    dgSales.Rows.Add(i, dr.Item("id").ToString, dr.Item("transaction_no").ToString, dr.Item("client").ToString, dr.Item("property_id").ToString, Format(CDec(dr.Item("amount").ToString), "#,##0.00"), dr.Item("date_paid").ToString, dr.Item("name").ToString)
                End While
                dr.Close()
                cn.Close()
            End If
            lblTotal.Text = "TOTAL : " & Format(_total, "#,##0.00")
        Catch ex As Exception
            MsgBox(ex.Message, vbCritical)
            cn.Close()
        End Try
    End Sub

    Private Sub frmSalesDetails_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.KeyPreview = True
    End Sub

    'Private Sub frmSalesDetails_KeyDown(sender As Object, e As KeyEventArgs) Handles Me.KeyDown
    '    If e.KeyCode = Keys.Escape Then
    '        ToolStripButton2_Click(sender, e)
    '    End If
    'End Sub

    Private Sub dgSales_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgSales.CellContentClick
        Dim colname As String = dgSales.Columns(e.ColumnIndex).Name
        If colname = "colCancel" Then
            If str_role = "Administrator" Then
                If MsgBox("Are you sure you want to cancel this payment transaction?", vbYesNo + vbQuestion) = vbYes Then
                    cancelOrder()
                Else
                    MsgBox("Cancelling payment transaction denied.", vbInformation)
                End If
            Else
                Panel_permission.Visible = True
                CenterPanelInForm(Panel_permission)
                dgSales.Enabled = False
            End If
        End If
    End Sub

    Private Sub frmSalesDetails_KeyDown(sender As Object, e As KeyEventArgs) Handles Me.KeyDown
        If Panel_permission.Visible = True Then
            If e.KeyCode = Keys.Escape Then
                Panel_permission.Visible = False
                dgSales.Enabled = True
            ElseIf e.KeyCode = Keys.Enter Then
                btnGrant_Click(sender, e)
            End If
        Else
            If e.KeyCode = Keys.Escape Then
                ToolStripButton2_Click(sender, e)
            End If
        End If
    End Sub

    Private Sub btnGrant_Click(sender As Object, e As EventArgs) Handles btnGrant.Click
        'Try
        Dim found As Boolean
        If txtUsername.Text = String.Empty Or txtPassword.Text = String.Empty Or txtReason.Text = String.Empty Then
            MsgBox("Required empty field.", vbCritical)
            Return
        End If
        cn.Close()
            cn.Open()
            cm = New MySqlCommand("select * from tbluser where username = @1 and password = sha2(@2, 224)", cn)
            With cm
                .Parameters.AddWithValue("@1", txtUsername.Text)
                .Parameters.AddWithValue("@2", txtPassword.Text)
            End With
            dr = cm.ExecuteReader
            dr.Read()
        If dr.HasRows Then
            found = True
            credRole = dr.Item("role").ToString
        Else
            found = False
        End If
        dr.Dispose()

        If found = True Then
            If credRole = "Administrator" Then

                If MsgBox("Are you sure you want to cancel this payment transaction?", vbYesNo + vbQuestion) = vbYes Then
                    credRole = ""
                    MsgBox("Permission granted.", vbInformation)
                    cancelOrder()
                Else
                    MsgBox("Cancelling payment transaction denied.", vbInformation)
                End If

            ElseIf credRole = "Cashier" Then
                MsgBox("Account credential invalid!", vbCritical)
            Else
                MsgBox("Failed to cancel order!", vbExclamation)
                Me.Dispose()
            End If
        Else
            MsgBox("Invalid username or password!", vbExclamation)
        End If
        cn.Close()
        DashBoard()
        'Catch ex As Exception
        '    cn.Close()
        '    MsgBox(ex.Message, vbCritical)
        'End Try
    End Sub

    Private Sub cancelOrder()
        cn.Close()
        cn.Open()
        cm = New MySqlCommand("Update tblpayment set payment_status = 'CANCELLED', cancellation_reason = '" & txtReason.Text & "' where transaction_no = '" & dgSales.CurrentRow.Cells(2).Value & "'", cn)
        result = cm.ExecuteNonQuery
        If result = 0 Then
            MsgBox("Cancelling transaction failed.", vbCritical)
        Else
            MsgBox("Transaction has been successfully cancelled.", vbInformation)

            If str_role = "Administrator" Then
                AuditTrail("Cancelled a transaction; transaction_no '" & dgSales.CurrentRow.Cells(2).Value & "' where id '" & dgSales.CurrentRow.Cells(1).Value & "' with reason '" & txtReason.Text & "'.")
            Else
                AuditTrail("Cancelled a transaction; transaction_no '" & dgSales.CurrentRow.Cells(2).Value & "' where id '" & dgSales.CurrentRow.Cells(1).Value & "' with reason '" & txtReason.Text & "' and administrator '" & txtUsername.Text & "' permission.")
            End If
            loadSales()
            Panel_permission.Visible = False
            dgSales.Enabled = True
            txtUsername.Text = ""
            txtPassword.Text = ""
            txtReason.Text = ""
        End If
        cm.Dispose()
        cn.Close()
    End Sub

    Private Sub CenterPanelInForm(panel As Panel)
        Dim newX As Integer = (Me.ClientSize.Width - panel.Width) \ 2
        Dim newY As Integer = (Me.ClientSize.Height - panel.Height) \ 2
        panel.Location = New Point(newX, newY)
        panel.Anchor = AnchorStyles.None
    End Sub
End Class