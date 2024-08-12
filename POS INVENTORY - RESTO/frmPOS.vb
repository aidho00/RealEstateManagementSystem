Imports MySql.Data.MySqlClient
Imports System.IO

Public Class frmPOS
    Dim btnProperty As New Button

    Dim _action As String
    Dim _filter As String = ""

    Private Sub frmPOS_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        For Each col As DataGridViewColumn In dgInstallment2.Columns
            col.SortMode = DataGridViewColumnSortMode.NotSortable
        Next
        Timer2.Start()
        lbl_company_name.Text = str_company
        Me.KeyPreview = True
        btnPayment.Focus()
    End Sub

    Private Sub frmPOS_KeyDown(sender As Object, e As KeyEventArgs) Handles MyBase.KeyDown
        'If e.KeyCode = Keys.F1 Then
        '    If btnNewOrder.Enabled = True Then
        '        btnNewOrder_Click(sender, e)
        '    End If
        'ElseIf e.KeyCode = Keys.F2 Then
        '    If btnSettle.Enabled = True Then
        '        btnSettle_Click(sender, e)
        '    End If
        'ElseIf e.KeyCode = Keys.F3 Then
        '    If btnCancelOrder.Enabled = True Then
        '        btnCancelOrder_Click(sender, e)
        '    End If
        'ElseIf e.KeyCode = Keys.F4 Then
        '    If btnDiscount.Enabled = True Then
        '        btnDiscount_Click(sender, e)
        '    End If
        'ElseIf e.KeyCode = Keys.F5 Then
        '    If btnSales.Enabled = True Then
        '        btnSales_Click(sender, e)
        '    End If
        'Else
        If e.KeyCode = Keys.Escape Then
            If client_name.Text = "" Then
                If MsgBox("Are you sure you want to exit?", vbYesNo + vbQuestion) = vbYes Then
                    'cn.Open()
                    'cm = New MySqlCommand("Update tbltable set tablestatus = 'False' where tableno = '" & lblTable.Text & "'", cn)
                    'cm.ExecuteNonQuery()
                    'cn.Close()
                    If str_role = "Administrator" Or str_role = "Transaction Coordinator" Then
                        Me.Close()
                        frmMain.Show()
                    Else
                        Me.Close()
                        My.Application.OpenForms.Cast(Of Form)() _
                         .Except({frmLogin}) _
                         .ToList() _
                         .ForEach(Sub(form) form.Close())

                        frmLogin.Show()
                    End If
                End If
            Else
                clearvalues()
            End If
        End If
    End Sub


    Private Sub btnSales_Click(sender As Object, e As EventArgs) Handles btnSales.Click
        'With frmDailySales
        '    .lblTitle.Text = "CASHIER " & str_name.ToString.ToUpper & " DAILY SALES [" & Now.ToString("MM-dd-yyyy") & "]"
        '    .generateSales()
        '    .ShowDialog()
        'End With
        'If str_role = "Administrator" Then
        Dim DateNow As String = Format(Convert.ToDateTime(frmLogin.txtbox_date.Text), "MM-dd-yyyy")
            With frmSalesDetails
                .lblTitle.Text = "CASHIER " & str_name.ToString.ToUpper & " DAILY PAYMENTS TRANSACTIONS [" & DateNow & "]"
                .loadSales()
                .ShowDialog()
            End With
            AuditTrail("Viewed Daily Payments Transactions [" & DateNow & "].")
        'Else
        '    With frmAdminPermission
        '        .txtArea.Text = "POS - Cashier Payments Transaction"
        '        .ShowDialog()
        '    End With
        'End If

    End Sub

    Private Sub btnStart_Click(sender As Object, e As EventArgs) Handles btnStart.Click
        If checkTransaction() = False Then
            With frmStart
                .ShowDialog()
            End With
        Else
            MsgBox("Billing transaction for this day is already closed!", vbExclamation)
        End If
    End Sub

    Private Sub btnEnd_Click(sender As Object, e As EventArgs) Handles btnEnd.Click
        Try
            Dim sdate As String = Format(Convert.ToDateTime(frmLogin.txtbox_date.Text), "yyyyMMdd")
            If MsgBox("Are you sure you want to end billing transaction for this day?", vbYesNo + vbExclamation) = vbYes Then
                cn.Open()
                cm = New MySqlCommand("Update tblstart set status = 'Close' where cashier = '" & str_user & "' and sdate between '" & sdate & "' and '" & sdate & "'", cn)
                cm.ExecuteNonQuery()
                cn.Close()
                AuditTrail("Ended the transaction for the day.")
                If checkStatus() = True Then
                    btnEnd.Enabled = True
                Else
                    btnEnd.Enabled = False
                End If
                MsgBox("Billing transaction for this day has been successfully closed!", vbInformation)
            End If
        Catch ex As Exception
            cn.Close()
            MsgBox(ex.Message, vbCritical)
        End Try
    End Sub

    Private Sub frmPOS_FormClosed(sender As Object, e As FormClosedEventArgs) Handles MyBase.FormClosed
        AuditTrail("Has Logged-out")
        Logout()
    End Sub

    Private Sub client_ID_TextChanged(sender As Object, e As EventArgs) Handles property_id.TextChanged, property_date.TextChanged, lenght_years.TextChanged, Label4.TextChanged, iRate.TextChanged, down_payment.TextChanged, client_ID.TextChanged, cash_installment.TextChanged
        'loadClientProperties()
    End Sub

    Private Sub client_ID_Click(sender As Object, e As EventArgs) Handles property_id.Click, property_date.Click, lenght_years.Click, Label4.Click, iRate.Click, down_payment.Click, client_ID.Click, cash_installment.Click

    End Sub

    Private Sub dgInstallment_CellMouseEnter(sender As Object, e As DataGridViewCellEventArgs) Handles dgInstallment2.CellMouseEnter, dgInstallment.CellMouseEnter
        If (e.ColumnIndex = 6) Then
            Cursor = Cursors.Hand
        Else
            Cursor = Cursors.Default
        End If
    End Sub

    Private Sub dgInstallment_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgInstallment2.CellContentClick
        'If (e.ColumnIndex = 6) Then
        '    If String.IsNullOrEmpty(dgInstallment.CurrentRow.Cells(6).Value) Then
        '    Else

        '        With frmPOSEncoding
        '            .txtPaymentNo.Text = "PAYMENT " & dgInstallment.CurrentRow.Cells(0).Value
        '            .txtPaymentAmount.Text = dgInstallment.CurrentRow.Cells(5).Value
        '            .txtbox_amountpaid.Text = dgInstallment.CurrentRow.Cells(5).Value
        '            .property_id.Text = dgPropertyList.CurrentRow.Cells(1).Value
        '            .ShowDialog()
        '        End With
        '    End If
        'End If
    End Sub

    Private Sub btnPayment_Click(sender As Object, e As EventArgs) Handles btnPayment.Click
        If client_name.Text.Length > 0 Then
            With frmPOSEncoding
                .txtbox_amountpaid.Text = Format(dgInstallment2.CurrentRow.Cells(6).Value, "#,##0.00")
                .property_id.Text = property_id.Text
                .btnSettle.Text = "SETTLE PAYMENT"
                .CheckBox1.Visible = True
                .txtbox_amountreceived.Focus()
                .ShowDialog()
            End With
        Else
            clearvalues()
            If checkStatus() = True Then
                With frmSelectTable
                    .loadclient()
                    .Show()
                End With
            Else
                If btnStart.Enabled = True Then
                    MsgBox("Transaction for this day is still close!", vbExclamation)
                Else
                    MsgBox("Transaction for this day is already closed!", vbExclamation)
                End If
            End If
        End If
    End Sub

    Private Sub Timer2_Tick(sender As Object, e As EventArgs) Handles Timer2.Tick
        Try
            load_data("SELECT date_format(curdate(), '%b %d, %Y'), time_format(curtime(), '%h:%i %p'), dayname(curdate()), LEFT(time_format(curtime(), '%h:%i:%s %p'),5), RIGHT(time_format(curtime(), '%h:%i %p'),2), LEFT(time_format(curtime(), '%h:%i:%s %p'),8), date_format(curdate(), '%M %d, %Y')", "CURR")
            datetime.Text = ds.Tables("CURR").Rows(0)(0).ToString & " - " & ds.Tables("CURR").Rows(0)(1).ToString
            ds = New DataSet
        Catch ex As Exception
            'MsgBox(ex.Message, vbCritical)
        End Try
    End Sub

    Private Sub clearvalues()
        property_id.Text = 0

        dgInstallment2.Rows.Clear()
        client_name.Text = ""

        property_paid.Text = "0.00"
        property_balance.Text = "0.00"
        property_price.Text = "0.00"
        total_contract_price.Text = "0.00"

        property_name.Text = "-"
        Subdivision.Text = "-"
        txtAmort.Text = "-"
        start_contract.Text = "-"
        end_contract.Text = "-"
    End Sub

    Private Sub frmPOS_Closed(sender As Object, e As EventArgs) Handles Me.Closed

    End Sub
End Class
