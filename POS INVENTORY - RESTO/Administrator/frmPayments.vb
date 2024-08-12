Imports MySql.Data.MySqlClient

Public Class frmPayments


    Private Sub dgPayments_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgPayments.CellContentClick
        Dim colname As String = dgPayments.Columns(e.ColumnIndex).Name
        If colname = "colUpdate" Then
            If dgPayments.CurrentRow.Cells(8).Value = "[REMOVE]" Then
                If str_role = "Administrator" Then
                    If MsgBox("Remove interest/penalty?", vbYesNo + vbQuestion) = vbYes Then
                        Try
                            cn.Open()
                            cm = New MySqlCommand("update tblinstallments set interest_rate = NULL where property_id = '" & property_id.Text & "' and payment_no = " & CInt(dgPayments.CurrentRow.Cells(0).Value) & "", cn)
                            cm.ExecuteNonQuery()
                            cn.Close()
                            AuditTrail("Removed interest/penalty to property " & property_id.Text & " outstanding balance for payment number " & dgPayments.CurrentRow.Cells(0).Value & ".")
                            calculatepayments_client()
                            MsgBox("Interest/Penalty successfully removed!", vbInformation)
                        Catch ex As Exception
                        End Try
                    End If
                Else
                    MsgBox("You do not have permission to remove Interest/Penalty!", vbCritical)
                End If

            ElseIf dgPayments.CurrentRow.Cells(8).Value = "[RE-APPLY]" Then
                If str_role = "Administrator" Then
                    If MsgBox("Re-apply interest/penalty?", vbYesNo + vbQuestion) = vbYes Then
                        Try
                            cn.Open()
                            cm = New MySqlCommand("update tblinstallments set interest_rate = 3 where property_id = '" & property_id.Text & "' and payment_no = " & CInt(dgPayments.CurrentRow.Cells(0).Value) & "", cn)
                            cm.ExecuteNonQuery()
                            cn.Close()
                            AuditTrail("Re-Applies interest/penalty to property " & property_id.Text & " outstanding balance for payment number " & dgPayments.CurrentRow.Cells(0).Value & ".")
                            calculatepayments_client()
                            MsgBox("Interest/Penalty successfully re-added/re-applied!", vbInformation)
                        Catch ex As Exception
                        End Try
                    End If
                Else
                    MsgBox("You do not have permission to re-apply Interest/Penalty!", vbCritical)
                End If
            Else
            End If
        End If
    End Sub

    Sub calculatepayments_client()
        Dim property_date As String
        Dim property_price As String
        Dim property_paid As String
        Dim property_downpayment As String
        Dim payment_option As String

        cn.Open()
        cm = New MySqlCommand("select * from accountsreceivable where id = @1", cn)
        With cm
            .Parameters.AddWithValue("@1", property_id.Text)
        End With
        dr = cm.ExecuteReader
        dr.Read()
        If dr.HasRows Then
            payment_option = dr.Item("payment_option").ToString
            property_date = dr.Item("property_date").ToString
            property_price = dr.Item("price").ToString
            property_paid = dr.Item("total_payments").ToString
            property_downpayment = dr.Item("down_payment").ToString
        Else
        End If
        cn.Close()

        Dim DateNow As String = Format(Convert.ToDateTime(property_date), "MM/dd/yyyy")

        If payment_option = "Cash" Then
            Dim downPayment As Double = CDbl(property_downpayment)
            dgPayments.Rows.Add(1, DateNow, Format(CDec(property_price) - downPayment, "#,##0.00"), 0, Format(CDec(property_price), "#,##0.00"), Format(CDec(property_price), "#,##0.00"))
        Else
            dgPayments.Rows.Clear()
            cn.Close()
            cn.Open()
            cm = New MySqlCommand("SELECT `payment_no`,`payment_date`,`amount`,`amount` * ifnull(`interest_rate`*0.01,0) as interest, `amount` + (`amount` * ifnull(`interest_rate`*0.01,0)) as total, ifnull(interest_rate,'-') as status from tblinstallments where `property_id` = '" & property_id.Text & "' order by `payment_no` asc", cn)
            dr = cm.ExecuteReader
            While dr.Read
                If dr.Item("payment_no").ToString = 0 Then
                Else
                    dgPayments.Rows.Add(dr.Item("payment_no").ToString, Format(Convert.ToDateTime(dr.Item("payment_date").ToString), "MMM dd, yyyy"), Format(CDec(dr.Item("amount").ToString), "#,##0.00"), Format(CDec(dr.Item("interest").ToString), "#,##0.00"), Format(CDec(dr.Item("total").ToString), "#,##0.00"), "", "", dr.Item("status").ToString)
                End If
            End While
            dr.Close()
            cn.Close()
        End If

        Dim totalAmount As Double = property_paid

        For Each row As DataGridViewRow In dgPayments.Rows
            Dim amount As Double = CDec(row.Cells(4).Value)
            Dim paid As Double = Math.Min(amount, totalAmount)

            row.Cells(5).Value = paid
            row.Cells(6).Value = amount - paid

            totalAmount -= paid

            Dim sdate As String = Format(Convert.ToDateTime(frmLogin.txtbox_date.Text), "yyyy-MM-dd")
            Dim Date_date As DateTime = Convert.ToDateTime(sdate)

            Dim sdate2 As String = Format(Convert.ToDateTime(row.Cells(1).Value), "yyyy-MM-dd")
            Dim Date_date2 As DateTime = Convert.ToDateTime(sdate2)

            Dim balanceamount As Double = CDec(row.Cells(6).Value)

            If row.Cells(7).Value = "3" And Date_date2 < Date_date And balanceamount > 0 Then
                row.Cells(8).Value = "[REMOVE]"
            ElseIf row.Cells(7).Value = "-" And Date_date2 < Date_date And balanceamount > 0 Then
                row.Cells(8).Value = "[RE-APPLY]"
            Else
                row.Cells(8).Value = ""
            End If

        Next

        For Each row As DataGridViewRow In dgPayments.Rows
            If row.Cells(6).Value <> 0 Then
                row.Selected = True
                dgPayments.CurrentCell = row.Cells(6)
                Exit For
            End If
        Next
        Dim startdate As DateTime = DateTime.Parse("" & dgPayments.Rows(0).Cells(1).Value & "")
        Dim enddate As DateTime = DateTime.Parse("" & frmLogin.txtbox_date.Text & "")
    End Sub

    Private Sub frmPayments_Load(sender As Object, e As EventArgs) Handles Me.Load
        'calculatepayments_client()
        Me.KeyPreview = True
    End Sub

    Private Sub frmPayments_KeyDown(sender As Object, e As KeyEventArgs) Handles Me.KeyDown
        If e.KeyCode = Keys.Escape Then
            Me.Close()
        End If
    End Sub

    Private Sub ToolStripButton2_Click(sender As Object, e As EventArgs) Handles ToolStripButton2.Click
        Me.Dispose()
    End Sub

    Private Sub dgPayments_CellMouseLeave(sender As Object, e As DataGridViewCellEventArgs) Handles dgPayments.CellMouseLeave
        dgPayments.Cursor = Cursors.Default
    End Sub

    Private Sub dgPayments_CellMouseEnter(sender As Object, e As DataGridViewCellEventArgs) Handles dgPayments.CellMouseEnter
        If e.ColumnIndex = 7 Then
            dgPayments.Cursor = Cursors.Hand
        Else
            dgPayments.Cursor = Cursors.Default
        End If
    End Sub

    Private Sub ToolStripButton1_Click(sender As Object, e As EventArgs) Handles ToolStripButton1.Click
        calculatepayments_client()
    End Sub
End Class