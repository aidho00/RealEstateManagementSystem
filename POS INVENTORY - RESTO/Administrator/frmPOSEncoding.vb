Imports MySql.Data.MySqlClient
Imports System.Globalization

Public Class frmPOSEncoding
    Dim current_balance As String
    Dim trans_no As String
    Dim culture As New CultureInfo("en-US")
#Region " Move Form "

    Public MoveForm As Boolean
    Public MoveForm_MousePosition As Point
    Public Sub MoveForm_MouseDown(sender As Object, e As MouseEventArgs) Handles _
    MyBase.MouseDown, Label1.MouseDown ' Add more handles here (Example: PictureBox1.MouseDown)
        If e.Button = MouseButtons.Left Then
            MoveForm = True
            Me.Cursor = Cursors.Default
            MoveForm_MousePosition = e.Location
        End If
    End Sub

    Public Sub MoveForm_MouseMove(sender As Object, e As MouseEventArgs) Handles _
    MyBase.MouseMove, Label1.MouseMove ' Add more handles here (Example: PictureBox1.MouseMove)
        If MoveForm Then
            Me.Location = Me.Location + (e.Location - MoveForm_MousePosition)
        End If
    End Sub

    Public Sub MoveForm_MouseUp(sender As Object, e As MouseEventArgs) Handles _
    MyBase.MouseUp, Label1.MouseUp ' Add more handles here (Example: PictureBox1.MouseUp)
        If e.Button = MouseButtons.Left Then
            MoveForm = False
            Me.Cursor = Cursors.Default
        End If
    End Sub
#End Region


    Private Sub frmPOSEncoding_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.KeyPreview = True
        payment_method.SelectedIndex = 0
    End Sub

    Private Sub frmPOSEncoding_KeyDown(sender As Object, e As KeyEventArgs) Handles Me.KeyDown
        If e.KeyCode = Keys.Escape Then
            Me.Close()
            txtclearvalues()
        ElseIf e.KeyCode = Keys.Enter Then
            btnSettle_Click(sender, e)
        End If
    End Sub

    Private Sub Panel7_Paint(sender As Object, e As PaintEventArgs) Handles Panel7.Paint

    End Sub

    Private Sub txtbox_amountpaid_TextChanged(sender As Object, e As EventArgs) Handles txtbox_amountpaid.TextChanged, txtbox_amountreceived.TextChanged, txtAdvance.TextChanged
        Try
            If CDec(txtbox_amountchange.Text) > 0 Then
                CheckBox1.Visible = True
            Else
                CheckBox1.Visible = False
                'If CheckBox1.Visible = True Then
                'Else
                CheckBox1.Checked = False
                'End If
            End If
        Catch ex As Exception

        End Try

        Try
            Dim amountpaid As Decimal
            Dim amountreceived As Decimal
            Dim amountadvance As Decimal
            If txtbox_amountpaid.Text = "" Then
                amountpaid = 0
            Else
                amountpaid = txtbox_amountpaid.Text
            End If
            If txtAdvance.Text = "" Then
                amountadvance = 0
            Else
                amountadvance = txtAdvance.Text
            End If

            If txtbox_amountreceived.Text = "" Then
                amountreceived = 0
            Else
                amountreceived = txtbox_amountreceived.Text
            End If
            txtbox_amountchange.Text = Format(amountreceived - (amountpaid + amountadvance), "#,##0.00")

            If txtbox_amountreceived.Text = "" Then
                txtbox_amountreceived.Text = "0.00"
                txtbox_amountreceived.SelectionStart = txtbox_amountreceived.TextLength
            Else

                txtbox_amountreceived.Text = Format(CDec(txtbox_amountreceived.Text), "#,##0")
                txtbox_amountreceived.SelectionStart = txtbox_amountreceived.TextLength
            End If

            If txtAdvance.Text = "" Then
                txtAdvance.Text = "0.00"
                txtAdvance.SelectionStart = txtAdvance.TextLength
            Else

                txtAdvance.Text = Format(CDec(txtAdvance.Text), "#,##0.00")
                txtAdvance.SelectionStart = txtAdvance.TextLength
            End If


        Catch ex As Exception

        End Try



    End Sub

    Function GetTransno() As String
        cn.Close()
        cn.Open()
        Try

            Dim sdate As String = Format(Convert.ToDateTime(frmLogin.txtbox_date.Text), "yyyyMMdd")
            cm = New MySqlCommand("select * from tblpayment where transaction_no like '" & sdate & "%' order by id desc", cn)
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

    Private Sub txtbox_amountpaid_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtbox_amountreceived.KeyPress, txtbox_amountpaid.KeyPress
        If Not Char.IsControl(e.KeyChar) AndAlso Not Char.IsDigit(e.KeyChar) AndAlso e.KeyChar <> ","c AndAlso e.KeyChar <> "."c Then
            e.Handled = True ' Suppress the key press event
        End If
    End Sub

    Private Sub btnSettle_Click(sender As Object, e As EventArgs) Handles btnSettle.Click

        If String.IsNullOrEmpty(txtbox_amountpaid.Text) Or CDbl(txtbox_amountpaid.Text) = 0 Then
            MsgBox("Amount fields cannot be empty!", vbCritical)
            Return
        ElseIf String.IsNullOrEmpty(txtbox_amountreceived.Text) Or CDbl(txtbox_amountreceived.Text) = 0 Then
            MsgBox("Amount fields cannot be empty!", vbCritical)
            Return
            'ElseIf CDbl(txtbox_amountpaid.Text) > CDbl(txtPaymentAmount.Text) Then
            '    MsgBox("Amount to pay cannot be greater than the amount balance for the current payment!", vbCritical)
            '    Return
        ElseIf CDbl(txtbox_amountreceived.Text) < CDbl(txtbox_amountpaid.Text) Then
            MsgBox("Amount to pay cannot be less than the amount received for the current payment!", vbCritical)
            Return
        End If

        If btnSettle.Text = "SAVE" Then
            Me.Hide()
            frmProduct.SaveProperty()
        Else
            If MsgBox("Settle this transaction?", vbYesNo + vbQuestion) = vbYes Then


                Dim paymentNumberColumnIndex As Integer = 0
                Dim amountColumnIndex As Integer = 6
                Dim clientPayment As Double = CDbl(txtbox_amountpaid.Text) + CDbl(txtAdvance.Text)

                Dim paymentNumbers As New List(Of Integer)()
                Dim lastPaymentNumber As Integer = 0

                For Each row As DataGridViewRow In frmPOS.dgInstallment2.Rows
                    Dim paymentNumber As Integer = Convert.ToInt32(row.Cells(paymentNumberColumnIndex).Value)
                    Dim amount As Double = Convert.ToDouble(row.Cells(amountColumnIndex).Value)
                    If amount <= clientPayment Then
                        paymentNumbers.Add(paymentNumber)
                        clientPayment -= amount

                    ElseIf clientPayment < amount Then
                        If clientPayment = 0 Then
                        Else
                            paymentNumbers.Add(paymentNumber)
                        End If

                        Exit For
                    End If
                Next

                Dim payment_numbers As String = String.Join(", ", paymentNumbers)


                trans_no = GetTransno()
                cn.Close()
                cn.Open()
                cm = New MySqlCommand("INSERT INTO `tblpayment`(`client_id`, `property_id`, `payment_no`, `amount`, `amount_received`, `date_paid`, `user`, `transaction_no`, `time_paid`, `payment_mode`, `cheque_bank`, `cheque_no`) VALUES (@1,@2,@3,@4,@5,@6,@7,@8,NOW(),@9,@10,@11)", cn)
                With cm
                    .Parameters.AddWithValue("@1", frmPOS.client_ID.Text)
                    .Parameters.AddWithValue("@2", property_id.Text)
                    .Parameters.AddWithValue("@3", payment_numbers)
                    .Parameters.AddWithValue("@4", CDec(txtbox_amountpaid.Text) + CDec(txtAdvance.Text))
                    .Parameters.AddWithValue("@5", CDec(txtbox_amountreceived.Text))
                    .Parameters.AddWithValue("@6", Format(Convert.ToDateTime(frmLogin.txtbox_date.Text), "yyyy-MM-dd"))
                    .Parameters.AddWithValue("@7", str_user)
                    .Parameters.AddWithValue("@8", trans_no)
                    .Parameters.AddWithValue("@9", payment_method.Text)
                    .Parameters.AddWithValue("@10", txtbox_cheque_no.Text)
                    .Parameters.AddWithValue("@11", txtbox_cheque_bank.Text)
                End With
                cn.Close()
                cn.Open()
                result = cm.ExecuteNonQuery
                If result = 0 Then
                    MsgBox("Transaction has failed to save!", vbCritical)
                    cm.Dispose()
                    cn.Close()
                Else
                    MsgBox("Transaction has been successfully saved!", vbInformation)
                    cm.Dispose()
                    cn.Close()
                    AuditTrail("Settled payment transaction for Property " & property_id.Text & " with an amount paid of " & Format(CDec(txtbox_amountpaid.Text), "#,##0.00") & " and an advanced payment of " & Format(CDec(txtAdvance.Text), "#,##0.00") & " for Payment(s) " & payment_numbers & ".")
                End If
                current_balance = Format(CDec(frmPOS.property_balance.Text), "#,##0.00")
                currentTotalpaid()
                frmSelectTable.calculateamortization()

                payment_receipt()

                txtclearvalues()

                Me.Dispose()
            End If

        End If
    End Sub

    Private Sub currentTotalpaid()
        cn.Close()
        cn.Open()
        cm = New MySqlCommand("Select ifnull(SUM(total_payments),0.00) from accountsreceivable where id = '" & property_id.Text & "'", cn)
        frmPOS.property_paid.Text = Format(CDec(cm.ExecuteScalar), "#,##0.00")
        cn.Close()
        frmPOS.property_balance.Text = Format(CDec(frmPOS.total_contract_price.Text) - CDec(frmPOS.property_paid.Text), "#,##0.00")
    End Sub

    Sub txtclearvalues()
        txtbox_amountpaid.Text = "0.00"
        txtbox_amountreceived.Text = "0.00"
        txtAdvance.Text = "0.00"
        txtPaymentAmount.Text = "0.00"
        property_id.Text = 0


        'frmPOS.dgInstallment2.Rows.Clear()
        'frmPOS.client_name.Text = ""

        'frmPOS.property_paid.Text = "0.00"
        'frmPOS.property_balance.Text = "0.00"
        'frmPOS.property_price.Text = "0.00"
        'frmPOS.total_contract_price.Text = "0.00"

        'frmPOS.property_name.Text = "-"
        'frmPOS.Subdivision.Text = "-"
        'frmPOS.txtAmort.Text = "-"
        'frmPOS.start_contract.Text = "-"
        'frmPOS.end_contract.Text = "-"
    End Sub

    Private Sub payment_method_SelectedIndexChanged(sender As Object, e As EventArgs) Handles payment_method.SelectedIndexChanged
        If payment_method.Text = "CASH" Then
            Me.Size = New Size(457, 392)
            Panel2.Size = New Size(435, 10)
        Else
            Me.Size = New Size(457, 513)
            Panel2.Size = New Size(435, 126)
        End If
    End Sub

    Private Sub payment_method_KeyPress(sender As Object, e As KeyPressEventArgs) Handles payment_method.KeyPress
        e.Handled = True
    End Sub

    Private Sub payment_receipt()

        Dim rptdoc As CrystalDecisions.CrystalReports.Engine.ReportDocument
        rptdoc = New reportReceipt
        With rptdoc
            .SetDataSource(dt)
            .SetParameterValue("cashier_name", str_name)
            .SetParameterValue("client", frmPOS.client_name.Text)
            .SetParameterValue("property", frmPOS.property_name.Text)
            .SetParameterValue("method", payment_method.Text)
            .SetParameterValue("currentbalance", current_balance)
            .SetParameterValue("amount", Format(CDec(txtbox_amountpaid.Text), "#,##0.00"))
            .SetParameterValue("advance", Format(CDec(txtAdvance.Text), "#,##0.00"))
            .SetParameterValue("balance", frmPOS.property_balance.Text)
            .SetParameterValue("transaction_no", trans_no)
            .SetParameterValue("p_date", Format(Convert.ToDateTime(frmLogin.txtbox_date.Text), "yyyy/MM/dd"))

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

    Private Sub payment_numbers()
        Dim paymentNumberColumnIndex As Integer = 0
        Dim amountColumnIndex As Integer = 6
        Dim clientPayment As Double = CDbl(txtbox_amountpaid.Text) + CDbl(txtAdvance.Text)

        Dim paymentNumbers As New List(Of Integer)()
        Dim lastPaymentNumber As Integer = 0

        For Each row As DataGridViewRow In frmPOS.dgInstallment2.Rows
            Dim paymentNumber As Integer = Convert.ToInt32(row.Cells(paymentNumberColumnIndex).Value)
            Dim amount As Double = Convert.ToDouble(row.Cells(amountColumnIndex).Value)
            If amount <= clientPayment Then
                paymentNumbers.Add(paymentNumber)
                clientPayment -= amount
            ElseIf clientPayment < amount Then
                paymentNumbers.Add(paymentNumber)
                Exit For
            End If
        Next

        Dim payment_numbers As String = String.Join(", ", paymentNumbers)
    End Sub

    Private Sub CheckBox1_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBox1.CheckedChanged
        If CheckBox1.Checked = True Then
            Panel9.Visible = True
            txtAdvance.Text = txtbox_amountchange.Text
            txtAdvance.Focus()
        Else
            txtAdvance.Text = "0.00"
            Panel9.Visible = False
            txtbox_amountreceived.Focus()
        End If
    End Sub

    Private Sub txtbox_amountchange_TextChanged(sender As Object, e As EventArgs) Handles txtbox_amountchange.TextChanged
        If txtbox_amountchange.Text.Contains("-") Then
            txtbox_amountchange.Text = "0.00"
        End If
    End Sub
End Class