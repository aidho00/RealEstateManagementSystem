Imports MySql.Data.MySqlClient
Module Modules
    Public id, transno As String
    Public amount As Double

    Function checkStatus() As Boolean
        Try
            Dim found As Boolean
            Dim sdate As String = Format(Convert.ToDateTime(frmLogin.txtbox_date.Text), "yyyy-MM-dd")
            cn.Open()
            cm = New MySqlCommand("select * from tblstart where sdate between '" & sdate & "' and '" & sdate & "' and status = 'Open' and cashier = '" & str_user & "'", cn)
            dr = cm.ExecuteReader
            dr.Read()
            If dr.HasRows Then
                found = True
            Else
                found = False
            End If
            dr.Close()
            cn.Close()
            Return found
        Catch ex As Exception
            MsgBox("Please Re-Login", vbCritical)
        End Try
    End Function

    Function checkTransaction() As Boolean
        Dim isopen As Boolean
        Dim sdate As String = Format(Convert.ToDateTime(frmLogin.txtbox_date.Text), "yyyy-MM-dd")
        cn.Open()
        cm = New MySqlCommand("select * from tblstart where sdate between '" & sdate & "' and '" & sdate & "' and status = 'Close' and cashier = '" & str_user & "'", cn)
        dr = cm.ExecuteReader
        dr.Read()
        If dr.HasRows Then
            isopen = True
        Else
            isopen = False
        End If
        dr.Close()
        cn.Close()

        Return isopen
    End Function

    Function CountRecords(ByVal sql As String) As Integer
        Dim count As Integer
        cn.Open()
        cm = New MySqlCommand(sql, cn)
        count = CInt(cm.ExecuteScalar)
        cn.Close()
        Return count
    End Function

    Sub AuditTrail(ByVal summary)
        Try
            cn.Close()
            cn.Open()
            cm = New MySqlCommand("insert into tblaudit(user, summary, sdate, stime)values(@1,@2,NOW(),NOW())", cn)
            With cm
                .Parameters.AddWithValue("@1", str_user)
                .Parameters.AddWithValue("@2", summary)
                .ExecuteNonQuery()
            End With
            cn.Close()
        Catch ex As Exception
            cn.Close()
            MsgBox(ex.Message, vbCritical)
        End Try
    End Sub

    Sub Logout()
        'Try
        '    Dim found As Boolean
        '    cn.Open()
        '    cm = New MySqlCommand("select * from tbllog where user = @1 and status = 'ONLINE'", cn)
        '    With cm
        '        .Parameters.AddWithValue("@1", str_user)
        '    End With
        '    dr = cm.ExecuteReader
        '    dr.Read()
        '    If dr.HasRows Then
        '        found = True
        '    Else
        '        found = False
        '    End If
        '    cn.Close()

        '    If found = True Then
        '        cn.Open()
        '        cm = New MySqlCommand("update tbllog set timeout = @2, status = @3 where user = @1 and status = 'ONLINE' ", cn)
        '        With cm
        '            .Parameters.AddWithValue("@1", str_user)
        '            .Parameters.AddWithValue("@2", Now.ToShortTimeString)
        '            .Parameters.AddWithValue("@3", "OFFLINE")
        '            .ExecuteNonQuery()
        '        End With
        '        cn.Close()
        '    End If
        'Catch ex As Exception
        '    cn.Close()
        '    MsgBox(ex.Message, vbCritical)
        'End Try
    End Sub

    Sub Login()
        'Try
        '    Dim found As Boolean
        '    cn.Open()
        '    cm = New MySqlCommand("select * from tbllog where user = @1 and status = 'ONLINE'", cn)
        '    With cm
        '        .Parameters.AddWithValue("@1", str_user)
        '    End With
        '    dr = cm.ExecuteReader
        '    dr.Read()
        '    If dr.HasRows Then
        '        found = True
        '    Else
        '        found = False
        '    End If
        '    cn.Close()

        '    If found = False Then
        '        cn.Open()
        '        cm = New MySqlCommand("insert into tbllog(user, sdate, timein, status)values(@1,@2,@3,@4)", cn)
        '        With cm
        '            .Parameters.AddWithValue("@1", str_user)
        '            .Parameters.AddWithValue("@2", Now.ToString("yyyy-MM-dd"))
        '            .Parameters.AddWithValue("@3", Now.ToShortTimeString)
        '            .Parameters.AddWithValue("@4", "ONLINE")
        '            .ExecuteNonQuery()
        '        End With
        '        cn.Close()
        '    Else

        '    End If
        'Catch ex As Exception
        '    cn.Close()
        '    MsgBox(ex.Message, vbCritical)
        'End Try
    End Sub

    Sub DashBoard()
        Try
            Dim sdate As String = Format(Convert.ToDateTime(frmLogin.txtbox_date.Text), "yyyy-MM-dd")
            Dim sdate2 As String = Format(Convert.ToDateTime(frmLogin.txtbox_date.Text), "MMM dd, yyyy")
            With frmMain
                cn.Open()
                cm = New MySqlCommand("Select ifnull(sum(amount),0) as total from tblpayment where date_paid between '" & sdate & "' and '" & sdate & "' and payment_status = 'COMPLETED'", cn)
                .lblTotalSales.Text = Format(CDbl(cm.ExecuteScalar), "₱ #,##0.00")
                cn.Close()

                cn.Open()
                cm = New MySqlCommand("Select ifnull(count(*),0) as total from tblpropertytype", cn)
                .lblTotalProducts.Text = cm.ExecuteScalar
                cn.Close()

                cn.Open()
                cm = New MySqlCommand("Select ifnull(count(*),0) as id from tblclient where status = 'Active'", cn)
                .lblTotalClient.Text = cm.ExecuteScalar
                cn.Close()

                cn.Open()
                cm = New MySqlCommand("Select ifnull(count(*),0) as id from tblagent where status = 'Active'", cn)
                .lblTotalAgent.Text = cm.ExecuteScalar
                cn.Close()

                cn.Open()
                cm = New MySqlCommand("Select ifnull(SUM(price),0.00) from accountsreceivable", cn)
                .totalaccount.Text = Format(CDbl(cm.ExecuteScalar), "₱ #,##0.00")
                cn.Close()

                cn.Open()
                cm = New MySqlCommand("Select ifnull(SUM(total_payments),0.00) from accountsreceivable", cn)
                .accountreceived.Text = Format(CDbl(cm.ExecuteScalar), "₱ #,##0.00")
                cn.Close()

                cn.Open()
                cm = New MySqlCommand("Select ifnull(SUM(remaining_balance),0.00) from accountsreceivable", cn)
                .accountsreceivable.Text = Format(CDbl(cm.ExecuteScalar), "₱ #,##0.00")
                cn.Close()

                cn.Open()
                cm = New MySqlCommand("Select ifnull(SUM(total_interest),0.00) from accountsreceivable", cn)
                .accountsinterest.Text = Format(CDbl(cm.ExecuteScalar), "₱ #,##0.00")
                cn.Close()

                .sales_date.Text = "TOTAL PAYMENT COLLECTED [" & sdate2 & "]"

            End With
        Catch ex As Exception
            'cn.Close()
            'MsgBox(ex.Message, vbCritical)
        End Try
    End Sub

    Sub PaymentAlert()
        Try

            frmMain.dgAlert.Rows.Clear()
            Dim i As Integer
            cn.Close()
            cn.Open()
            'cm = New MySqlCommand("SELECT t1.client_id, CONCAT(t3.lname, ', ', t3.fname, ' ', t3.mname) as client, t4.name as subdivision, CONCAT('Block ',t1.block, ' - Lot ', t1.lot) as property, t3.contact FROM tblproperty t1 LEFT JOIN tblpayment t2 ON t1.client_id = t2.client_id JOIN tblclient t3 ON t1.client_id = t3.id JOIN tblpropertytype t4 ON t1.type_id = t4.id GROUP BY t1.client_id, t1.id HAVING COUNT(DISTINCT t2.date_paid) < 3 OR COUNT(DISTINCT t2.date_paid) IS NULL", cn)
            'cm = New MySqlCommand("Select t1.id, CONCAT(t3.lname, ', ', t3.fname, ' ', t3.mname) as client, t4.name as subdivision, CONCAT('Block ',t1.block, ' - Lot ', t1.lot) as property, t3.contact FROM tblproperty t1 LEFT JOIN tblpayment t2 ON t1.client_id = t2.client_id JOIN tblclient t3 ON t1.client_id = t3.id JOIN tblpropertytype t4 ON t1.type_id = t4.id JOIN tblinstallments t5 ON t1.id = t5.property_id WHERE t5.status = 'UNPAID' and (payment_date BETWEEN DATE_SUB(CURDATE(), INTERVAL 3 MONTH) AND CURDATE()) OR (payment_date <= DATE_SUB(CURDATE(), INTERVAL 3 MONTH)) GROUP BY t1.client_id, t1.id", cn)
            cm = New MySqlCommand("Select t1.id, CONCAT(t3.lname, ', ', t3.fname, ' ', t3.mname) as client, t4.name as subdivision, CONCAT('Block ',t1.block, ' - Lot ', t1.lot) as property, t3.contact, t1.penalty FROM tblproperty t1 LEFT JOIN tblpayment t2 ON t1.client_id = t2.client_id JOIN tblclient t3 ON t1.client_id = t3.id JOIN tblpropertytype t4 ON t1.type_id = t4.id JOIN tblinstallments t5 ON t1.id = t5.property_id WHERE t5.status = 'UNPAID' and payment_date < CURDATE() GROUP BY t1.client_id, t1.id", cn)

            dr = cm.ExecuteReader
            While dr.Read
                i += 1
                frmMain.dgAlert.Rows.Add(i, dr.Item("id").ToString, dr.Item("client").ToString, dr.Item("subdivision").ToString, dr.Item("property").ToString, dr.Item("contact").ToString, dr.Item("penalty").ToString, "👁️‍🗨️")
            End While
            dr.Close()
            cn.Close()

            If frmMain.dgAlert.RowCount <> 0 Then
                frmMain.AlertPanel1.Visible = True
            Else
                frmMain.AlertPanel1.Visible = False
            End If

            UpdateInterestOutStandingBalance()
        Catch ex As Exception
            MsgBox(ex.Message, vbCritical)
            cn.Close()
        End Try
    End Sub

    Sub UpdateInterestOutStandingBalance()
        Try
            For Each row As DataGridViewRow In frmMain.dgAlert.Rows

                If row.Cells(6).Value = "ON" Then
                    cn.Open()
                    cm = New MySqlCommand("UPDATE tblinstallments set interest_rate = If(interest_date_applied is not NULL, interest_rate,If(interest_rate is NULL,3,interest_rate + 3)), interest_date_applied = If(interest_date_applied is not NULL, interest_date_applied, CURDATE()) where property_id = '" & row.Cells(1).Value & "' and status = 'UNPAID' and payment_date < CURDATE() and interest_rate is NULL", cn)
                    cm.ExecuteNonQuery()
                    cn.Close()
                Else
                End If
                'Dim OutstandingCount As Integer

                'cn.Open()
                ''cm = New MySqlCommand("SELECT COUNT(*) FROM tblinstallments where property_id = '" & row.Cells(1).Value & "' and status = 'UNPAID' and (payment_date BETWEEN DATE_SUB(CURDATE(), INTERVAL 3 MONTH) AND CURDATE()) OR (payment_date <= DATE_SUB(CURDATE(), INTERVAL 3 MONTH))", cn)
                'cm = New MySqlCommand("SELECT COUNT(*) FROM tblinstallments where property_id = '" & row.Cells(1).Value & "' and status = 'UNPAID' and payment_date < CURDATE()", cn)
                'OutstandingCount = cm.ExecuteScalar
                'cn.Close()

                'If OutstandingCount = 3 Then

                '    cn.Open()
                '    cm = New MySqlCommand("UPDATE tblinstallments AS p1 JOIN ( SELECT MIN(payment_date) AS next_payment_date FROM tblinstallments WHERE payment_date > CURDATE() AND status = 'UNPAID' ) AS p2 JOIN ( SELECT SUM(interest_rate) AS total_interest FROM tblinstallments WHERE payment_date < CURDATE() AND status = 'UNPAID' AND interest_rate <> 0 ) AS p3 SET p1.interest_rate = If(p1.interest_rate is not NULL, p1.interest_rate,p3.total_interest) WHERE p1.payment_date = p2.next_payment_date AND p1.status = 'UNPAID'", cn)
                '    cm.ExecuteNonQuery()
                '    cn.Close()

                '    cn.Open()
                '    cm = New MySqlCommand("UPDATE tblinstallments set interest_rate = 0 where property_id = '" & row.Cells(1).Value & "' and status = 'UNPAID' and payment_date < CURDATE()", cn)
                '    cm.ExecuteNonQuery()
                '    cn.Close()

                'End If

            Next
        Catch ex As Exception
            MsgBox(ex.Message, vbCritical)
            cn.Close()
        End Try
    End Sub
    Sub OpenFormToAdminPanel(ByVal frm As Form)
        Try
            If frm.IsHandleCreated Then
                frm.BringToFront()
            Else
                frm.TopLevel = False
                With frmMain
                    .adminPanel.Controls.Add(frm)
                End With
                frm.BringToFront()
                frm.Show()
            End If
        Catch ex As Exception
            frm.Show()
        End Try
    End Sub
End Module
