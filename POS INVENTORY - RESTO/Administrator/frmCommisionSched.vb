Imports MySql.Data.MySqlClient

Public Class frmCommisionSched
    Dim trans_no As String
    Sub calculatecommission_agent()
        Dim commission_date As String
        Dim commission_price As String
        Dim commission_released As String
        Dim payment_option As String
        cn.Close()
        cn.Open()
        cm = New MySqlCommand("select * from agentcommissions where property_id = @1", cn)
        With cm
            .Parameters.AddWithValue("@1", ToolStripLabelID.Text)
        End With
        dr = cm.ExecuteReader
        dr.Read()
        If dr.HasRows Then
            payment_option = dr.Item("payment_option").ToString
            commission_date = dr.Item("property_date").ToString
            commission_price = dr.Item("commission_total").ToString
            commission_released = dr.Item("total_released").ToString
        Else
        End If
        cn.Close()

        Dim DateNow As String = Format(Convert.ToDateTime(commission_date), "MM/dd/yyyy")

        If payment_option = "Cash" Then
            Dim downPayment As Double = 0
            dgCommission.Rows.Add(1, DateNow, Format(CDec(commission_price) - downPayment, "#,##0.00"), 0, Format(CDec(commission_price), "#,##0.00"), Format(CDec(commission_price), "#,##0.00"))
        Else
            dgCommission.Rows.Clear()
            cn.Close()
            cn.Open()
            cm = New MySqlCommand("SELECT `payment_no`,`payment_date`,`amount`,`amount` * ifnull(`interest_rate`*0.01,0) as interest, `amount` + (`amount` * ifnull(`interest_rate`*0.01,0)) as total, status from tblcommissionsched where `property_id` = '" & ToolStripLabelID.Text & "' order by `payment_no` asc", cn)
            dr = cm.ExecuteReader
            While dr.Read
                If dr.Item("payment_no").ToString = 0 Then
                Else
                    dgCommission.Rows.Add(dr.Item("payment_no").ToString, Format(Convert.ToDateTime(dr.Item("payment_date").ToString), "MMM dd, yyyy"), dr.Item("amount").ToString, Format(CDec(dr.Item("interest").ToString), "#,##0.00"), Format(CDec(dr.Item("total").ToString), "#,##0.00"), "", "", dr.Item("status").ToString, "")
                End If
            End While
            dr.Close()
            cn.Close()
        End If

        Dim totalAmount As Double = commission_released


        Dim now_date As String = Format(Convert.ToDateTime(frmLogin.txtbox_date.Text), "yyyy-MM-dd")

        For Each row As DataGridViewRow In dgCommission.Rows
            Dim amount As Double = CDec(row.Cells(4).Value)
            Dim paid As Double = Math.Min(amount, totalAmount)
            Dim pay_date As String = Format(Convert.ToDateTime(row.Cells(1).Value), "yyyy-MM-dd")

            row.Cells(5).Value = paid
            row.Cells(6).Value = amount - paid

            totalAmount -= paid

            If CDbl(row.Cells(6).Value) = 0 Then
                cn.Close()
                cn.Open()
                cm = New MySqlCommand("update tblcommissionsched set status = 'RELEASED' where property_id = '" & ToolStripLabelID.Text & "' and payment_no = " & CInt(row.Cells(0).Value) & "", cn)
                cm.ExecuteNonQuery()
                cn.Close()
            Else
                cn.Close()
                cn.Open()
                cm = New MySqlCommand("select * from tblinstallments where property_id = '" & ToolStripLabelID.Text & "' and payment_no = " & row.Cells(0).Value & " and status = 'PAID'", cn)
                dr = cm.ExecuteReader
                dr.Read()
                If dr.HasRows Then
                    row.Cells(8).Value = "[RELEASE]"
                Else
                    row.Cells(8).Value = ""
                End If
                cn.Close()
            End If
        Next

        For Each row As DataGridViewRow In dgCommission.Rows
            If row.Cells(6).Value <> 0 Then
                row.Selected = True
                dgCommission.CurrentCell = row.Cells(1)
                Exit For
            End If
        Next

        dgCommission.Columns("Column4").DefaultCellStyle.Format = "#,##0.00"
        dgCommission.Columns("Column6").DefaultCellStyle.Format = "#,##0.00"
        dgCommission.Columns("Column7").DefaultCellStyle.Format = "#,##0.00"
        For Each col As DataGridViewColumn In dgCommission.Columns
            col.SortMode = DataGridViewColumnSortMode.NotSortable
        Next
        'Dim startdate As DateTime = DateTime.Parse("" & dgCommission.Rows(0).Cells(1).Value & "")
        'Dim enddate As DateTime = DateTime.Parse("" & frmLogin.txtbox_date.Text & "")
    End Sub

    Private Sub ToolStripButton2_Click(sender As Object, e As EventArgs) Handles ToolStripButton2.Click
        Me.Close()
    End Sub

    Function GetTransno() As String
        cn.Close()
        cn.Open()
        Try

            Dim sdate As String = Format(Convert.ToDateTime(frmLogin.txtbox_date.Text), "yyyyMMdd")
            cm = New MySqlCommand("select * from tblcommisionpay where transaction_no like '" & sdate & "%' order by id desc", cn)
            dr = cm.ExecuteReader
            dr.Read()
            If dr.HasRows Then
                GetTransno = CDbl(dr.Item("transaction_no").ToString) + 1
            Else
                GetTransno = sdate & "0001"
            End If
            dr.Close()
            Return GetTransno

        Catch ex As Exception
            MsgBox(ex.Message, vbCritical)
        End Try
        cn.Close()

    End Function

    Private Sub dgCommission_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgCommission.CellContentClick
        Dim colname As String = dgCommission.Columns(e.ColumnIndex).Name
        If colname = "colRelease" Then
            If dgCommission.CurrentRow.Cells(8).Value = "[RELEASE]" Then
                If MsgBox("Settle/Release this commission pay?", vbYesNo + vbQuestion) = vbYes Then

                    trans_no = GetTransno()
                    cn.Close()
                    cn.Open()
                    cm = New MySqlCommand("INSERT INTO `tblcommisionpay`(`agent_id`, `property_id`, `payment_no`, `amount`, `date_released`, `user`, `transaction_no`, `time_released`) VALUES (@1,@2,@3,@4,@5,@6,@7,NOW())", cn)
                    With cm
                        .Parameters.AddWithValue("@1", ToolStripLabelAGENTID.Text)
                        .Parameters.AddWithValue("@2", ToolStripLabelID.Text)
                        .Parameters.AddWithValue("@3", dgCommission.CurrentRow.Cells(0).Value)
                        .Parameters.AddWithValue("@4", CDec(dgCommission.CurrentRow.Cells(6).Value))
                        .Parameters.AddWithValue("@5", Format(Convert.ToDateTime(frmLogin.txtbox_date.Text), "yyyy-MM-dd"))
                        .Parameters.AddWithValue("@6", str_user)
                        .Parameters.AddWithValue("@7", trans_no)
                    End With
                    cn.Close()
                    cn.Open()
                    result = cm.ExecuteNonQuery
                    If result = 0 Then
                        MsgBox("Settling/Releasing commission pay has failed!", vbCritical)
                        cm.Dispose()
                        cn.Close()
                    Else
                        MsgBox("Settling/Releasing commission pay has been successfully saved!", vbInformation)
                        cm.Dispose()
                        cn.Close()
                        AuditTrail("Settled/Released client commission pay transaction for Property " & ToolStripLabelID.Text & " with an amount of " & dgCommission.CurrentRow.Cells(6).Value & ".")
                    End If
                    payment_receipt()
                    calculatecommission_agent()
                    Me.Dispose()
                End If
            End If
        End If
    End Sub

    Function ConvertToOrdinal(number As Integer) As String
        Dim suffix As String = "th"
        Select Case number Mod 100
            Case 11, 12, 13
                suffix = "th"
            Case Else
                Select Case number Mod 10
                    Case 1
                        suffix = "st"
                    Case 2
                        suffix = "nd"
                    Case 3
                        suffix = "rd"
                End Select
        End Select

        Return number.ToString() & suffix
    End Function

    Private Sub payment_receipt()

        Dim rptdoc As CrystalDecisions.CrystalReports.Engine.ReportDocument
        rptdoc = New reportReceiptAgent
        With rptdoc
            .SetDataSource(dt)
            .SetParameterValue("cashier_name", str_name)
            .SetParameterValue("client", ToolStripLabelAgent.Text)
            .SetParameterValue("property", ToolStripLabelProperty.Text)
            .SetParameterValue("amount", Format(CDec(dgCommission.CurrentRow.Cells(6).Value), "#,##0.00"))
            .SetParameterValue("transaction_no", trans_no)
            .SetParameterValue("p_date", Format(Convert.ToDateTime(frmLogin.txtbox_date.Text), "yyyy/MM/dd"))
            .SetParameterValue("pay_no", ConvertToOrdinal(CInt(dgCommission.CurrentRow.Cells(0).Value)))
            If str_company Is Nothing Then
                .SetParameterValue("company_name", "COMPANY")
            Else
                .SetParameterValue("company_name", str_company)
            End If
            If str_address Is Nothing Then
                .SetParameterValue("company_address", "ADDRESS")
            Else
                .SetParameterValue("company_address", str_address)
            End If
            If str_h1 Is Nothing Then
                .SetParameterValue("header1", "- - - - - - - - - - - - - - -")
            Else
                .SetParameterValue("header1", str_h1)
            End If

        End With
        frmInvoice.ReportViewer.ReportSource = rptdoc
        frmInvoice.ShowDialog()
    End Sub

    Private Sub ToolStripButton1_Click(sender As Object, e As EventArgs) Handles ToolStripButton1.Click
        calculatecommission_agent()
    End Sub
End Class