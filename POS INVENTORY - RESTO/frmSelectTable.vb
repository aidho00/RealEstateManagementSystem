Imports MySql.Data.MySqlClient
Imports System.Globalization

Public Class frmSelectTable
    Dim culture As New CultureInfo("en-US")


#Region " Move Form "

    Public MoveForm As Boolean
    Public MoveForm_MousePosition As Point
    Public Sub MoveForm_MouseDown(sender As Object, e As MouseEventArgs) Handles _
    MyBase.MouseDown, ToolStrip1.MouseDown ' Add more handles here (Example: PictureBox1.MouseDown)
        If e.Button = MouseButtons.Left Then
            MoveForm = True
            Me.Cursor = Cursors.Default
            MoveForm_MousePosition = e.Location
        End If
    End Sub

    Public Sub MoveForm_MouseMove(sender As Object, e As MouseEventArgs) Handles _
    MyBase.MouseMove, ToolStrip1.MouseMove ' Add more handles here (Example: PictureBox1.MouseMove)
        If MoveForm Then
            Me.Location = Me.Location + (e.Location - MoveForm_MousePosition)
        End If
    End Sub

    Public Sub MoveForm_MouseUp(sender As Object, e As MouseEventArgs) Handles _
    MyBase.MouseUp, ToolStrip1.MouseUp ' Add more handles here (Example: PictureBox1.MouseUp)
        If e.Button = MouseButtons.Left Then
            MoveForm = False
            Me.Cursor = Cursors.Default
        End If
    End Sub
#End Region

    Private Sub frmSelectTable_Load(sender As Object, e As EventArgs) Handles Me.Load
        Me.KeyPreview = True
        loadclient()
    End Sub

    Private Sub frmSelectTable_KeyDown(sender As Object, e As KeyEventArgs) Handles Me.KeyDown
        If e.KeyCode = Keys.Escape Then
            Me.Dispose()
        End If
    End Sub

    Sub loadclient()
        dgPropertyList.Rows.Clear()
        Dim i As Integer
        cn.Close()
        cn.Open()
        cm = New MySqlCommand("SELECT t1.`id`, CONCAT(t3.lname, ', ' ,t3.fname,' ',t3.mname) as client, t2.`name` as type, t1.`block`, t1.`lot`, t1.`square_meter`, t1.`price`, t1.`status`, CONCAT(t4.lname, ', ' ,t4.fname,' ',t4.mname) as 'agent_id', t1.`thumbnail`, t1.`monthly_amort`, DATE_FORMAT(t1.`start_contract`, '%m/%d/%Y') as 'start_contract', DATE_FORMAT(t1.`end_contract`, '%m/%d/%Y') as 'end_contract', t1.`length_years`, t1.`interest_rate`, date_format(t1.`property_date`, '%M %d %Y') as 'property_date', t1.`status`, t5.total_payments, t5.remaining_balance, t1.client_id, t1.`down_payment`, t1.`payment_option` FROM `tblproperty` t1 JOIN `tblpropertytype` t2 ON t1.type_id = t2.id JOIN `tblclient` t3 ON t1.client_id = t3.id JOIN `tblagent` t4 ON t1.`agent_id` = t4.`id` JOIN accountsreceivable t5 ON t1.id = t5.id where (CONCAT(t3.lname, ', ' ,t3.fname,' ',t3.mname) LIKE '%" & ToolStripTextBox1.Text & "%' or t1.`id` LIKE '%" & ToolStripTextBox1.Text & "%') order by t1.`start_contract` desc", cn)
        dr = cm.ExecuteReader
        While dr.Read
            i += 1
            dgPropertyList.Rows.Add(i, dr.Item("id").ToString, dr.Item("client").ToString, dr.Item("type").ToString, dr.Item("block").ToString, dr.Item("lot").ToString, dr.Item("square_meter").ToString, dr.Item("price").ToString, dr.Item("status").ToString, dr.Item("thumbnail"), dr.Item("agent_id").ToString, dr.Item("monthly_amort").ToString, dr.Item("start_contract").ToString, dr.Item("end_contract").ToString, dr.Item("length_years").ToString, dr.Item("interest_rate").ToString, dr.Item("property_date").ToString, dr.Item("status").ToString, dr.Item("total_payments").ToString, Format(CDec(dr.Item("remaining_balance").ToString), "#,##0.00"), dr.Item("client_id").ToString, dr.Item("down_payment").ToString, dr.Item("payment_option").ToString)
        End While
        dr.Close()

        'For i = 0 To dgPropertyList.Rows.Count - 1
        '    Dim r As DataGridViewRow = dgPropertyList.Rows(i)
        '    r.Height = 50
        'Next
        If dgPropertyList.Rows.Count = 0 Then
        Else
            Dim imageColumn = DirectCast(dgPropertyList.Columns("Column6"), DataGridViewImageColumn)
            imageColumn.ImageLayout = DataGridViewImageCellLayout.Stretch
        End If
        cn.Close()
    End Sub

    Private Sub ToolStripButton2_Click(sender As Object, e As EventArgs) Handles ToolStripButton2.Click
        Me.Dispose()
    End Sub


    Private Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        'Try

        If dgPropertyList.CurrentRow.Cells(19).Value = 0 Then
            MsgBox("Property already paid!", vbExclamation)
        Else
            With frmPOS
                .property_id.Text = dgPropertyList.CurrentRow.Cells(1).Value
                .client_ID.Text = dgPropertyList.CurrentRow.Cells(20).Value
                .client_name.Text = dgPropertyList.CurrentRow.Cells(2).Value
                .lenght_years.Text = dgPropertyList.CurrentRow.Cells(14).Value
                .iRate.Text = dgPropertyList.CurrentRow.Cells(15).Value

                .start_contract.Text = Format(dgPropertyList.CurrentRow.Cells(12).Value, "MMM dd, yyyy")
                .end_contract.Text = Format(dgPropertyList.CurrentRow.Cells(13).Value, "MMM dd, yyyy")
                .property_name.Text = dgPropertyList.CurrentRow.Cells(3).Value & " - Block " & dgPropertyList.CurrentRow.Cells(4).Value & ", Lot " & dgPropertyList.CurrentRow.Cells(5).Value
                .Subdivision.Text = dgPropertyList.CurrentRow.Cells(3).Value
                .txtAmort.Text = Format(CDec(dgPropertyList.CurrentRow.Cells(11).Value), "N2")

                .property_date.Text = dgPropertyList.CurrentRow.Cells(16).Value

                .property_price.Text = dgPropertyList.CurrentRow.Cells(7).Value
                .property_paid.Text = Format(CDec(dgPropertyList.CurrentRow.Cells(18).Value), "N2")
                .property_balance.Text = Format(CDec(dgPropertyList.CurrentRow.Cells(19).Value), "N2")
                .down_payment.Text = dgPropertyList.CurrentRow.Cells(21).Value
                .cash_installment.Text = dgPropertyList.CurrentRow.Cells(22).Value
            End With
            Me.Close()

            calculateamortization()

            'With frmPOSEncoding
            '    .txtbox_amountpaid.Text = Format(frmPOS.dgInstallment2.CurrentRow.Cells(6).Value, "#,##0.00")
            '    .property_id.Text = frmPOS.property_id.Text
            '    .txtbox_amountreceived.Focus()
            '    .ShowDialog()
            'End With

            With frmPOSEncoding
                .CheckBox1.Visible = False
                .txtbox_amountpaid.Text = Format(frmPOS.dgInstallment2.CurrentRow.Cells(6).Value, "N2")
                .property_id.Text = frmPOS.property_id.Text
                .btnSettle.Text = "SETTLE PAYMENT"
                .txtbox_amountreceived.Focus()
                .ShowDialog()
            End With
        End If


        'Catch ex As Exception

        'End Try
    End Sub


    Sub calculateamortization()
        Dim DateNow As String = Format(Convert.ToDateTime(frmPOS.property_date.Text), "MM/dd/yyyy")

        'Dim amortizationTable As New DataTable()
        'amortizationTable.Columns.Add("Payment No.", GetType(Integer))
        'amortizationTable.Columns.Add("Payment Date", GetType(DateTime))
        'amortizationTable.Columns.Add("Payment", GetType(Double))
        'amortizationTable.Columns.Add("Paid", GetType(Double))
        'amortizationTable.Columns.Add("Balance", GetType(Double))

        If frmPOS.cash_installment.Text = "Cash" Then
            Dim downPayment As Decimal = CDec(frmPOS.down_payment.Text)
            frmPOS.dgInstallment2.Rows.Add(1, DateNow, Format(CDec(frmPOS.property_price.Text) - downPayment, "#,##0.00"), 0, Format(CDec(frmPOS.property_price.Text), "#,##0.00"), Format(CDec(frmPOS.property_price.Text), "#,##0.00"))

            frmPOS.total_contract_price.Text = Format(CDec(frmPOS.property_price.Text), "#,##0.00")
            frmPOS.property_balance.Text = Format(CDec(frmPOS.property_price.Text), "#,##0.00")


            frmPOS.start_contract.Text = Format(Convert.ToDateTime(frmPOS.dgInstallment2.Rows(0).Cells(1).Value), "MM/dd/yyyy")
            frmPOS.end_contract.Text = Format(Convert.ToDateTime(frmPOS.dgInstallment2.Rows(frmPOS.dgInstallment2.Rows.Count - 1).Cells(1).Value), "MM/dd/yyyy")
        Else
            'Dim loanAmount As Double = CDbl(frmPOS.property_price.Text) ' The initial loan amount
            'Dim interestRate As Double = CDbl(frmPOS.iRate.Text) / 100 ' The annual interest rate
            'Dim loanTerm As Integer = CInt(frmPOS.lenght_years.Text) ' The loan term in years
            'Dim downPayment As Double = CDbl(frmPOS.down_payment.Text) ' Advance payment made towards the loan

            ''Dim monthlyInterestRate As Decimal = (1 + interestRate) ^ (1 / 12) - 1 ' Monthly interest rate
            'Dim monthlyInterestRate As Double = interestRate / 12 ' Monthly interest rate
            'Dim totalMonths As Integer = loanTerm * 12 ' Total number of months

            'Dim loanAmountAfterDownPayment As Double = loanAmount - downPayment ' Loan amount after down payment
            'Dim monthlyPayment As Double = loanAmountAfterDownPayment * (monthlyInterestRate * (1 + monthlyInterestRate) ^ totalMonths) / ((1 + monthlyInterestRate) ^ totalMonths - 1)


            'frmPOS.txtAmort.Text = Format(CDec(monthlyPayment), "#,##0.00")
            '' Create a DataTable to hold the amortization schedule



            '' Generate the amortization schedule
            'Dim balance As Double = loanAmountAfterDownPayment
            'Dim month As Integer = 1
            'Dim totalInterest As Double = 0
            'Dim paymentDate As DateTime = DateAdd(DateInterval.Month, 1, DateTime.Parse(DateNow))



            'Dim tcp As Double = 0

            'While balance > 0 AndAlso month <= totalMonths
            '    Dim interestPayment As Double = balance * monthlyInterestRate
            '    Dim principalPayment As Double = monthlyPayment - interestPayment

            '    Dim opening_balance As Double = balance
            '    balance -= principalPayment
            '    totalInterest += interestPayment

            '    amortizationTable.Rows.Add(month, paymentDate, monthlyPayment, 0.00, monthlyPayment)

            '    month += 1
            '    paymentDate = paymentDate.AddMonths(1)
            '    tcp = tcp + monthlyPayment
            'End While





            'Dim loanAmount As Double = CDbl(frmPOS.property_price.Text) ' The initial loan amount
            ''Dim interestRate As Double = CDbl(txtRate.Text.Trim) / 100 ' The annual interest rate
            'Dim loanTerm As Integer = CInt(frmPOS.lenght_years.Text)  ' The loan term in years
            'Dim downPayment As Double = CDbl(frmPOS.down_payment.Text)  ' Advance payment made towards the loan
            ''Dim monthlyInterestRate As Decimal = (1 + interestRate) ^ (1 / 12) - 1 ' Monthly interest rate
            ''Dim monthlyInterestRate As Double = interestRate / 12 ' Monthly interest rate
            'Dim totalMonths As Integer = loanTerm * 12 ' Total number of months
            'Dim loanAmountAfterDownPayment As Double = loanAmount - downPayment ' Loan amount after down payment
            'Dim monthlyPayment As Double = loanAmountAfterDownPayment / totalMonths
            'frmPOS.txtAmort.Text = Math.Round(monthlyPayment, 2)
            '' Generate the amortization schedule
            'Dim balance As Double = loanAmountAfterDownPayment
            'Dim month As Integer = 1
            'Dim totalInterest As Double = 0
            'Dim paymentDate As DateTime = DateAdd(DateInterval.Month, 1, DateTime.Parse(DateNow))
            'Dim dPdate As DateTime = DateTime.Parse(DateNow)
            '' Add row for the down payment
            ''amortizationTable.Rows.Add(0, dPdate, loanAmount, 0, balance)
            'Dim tcp As Double = 0
            'While balance > 0 AndAlso month <= totalMonths
            '    'Dim interestPayment As Double = balance * monthlyInterestRate
            '    'Dim principalPayment As Double = monthlyPayment - interestPayment
            '    Dim opening_balance As Double = balance
            '    balance -= monthlyPayment
            '    'totalInterest += interestPayment
            '    amortizationTable.Rows.Add(month, paymentDate, monthlyPayment, 0.00, monthlyPayment)
            '    month += 1
            '    paymentDate = paymentDate.AddMonths(1)
            '    tcp = tcp + monthlyPayment
            'End While

            frmPOS.dgInstallment2.Rows.Clear()
            cn.Close()
            cn.Open()
            'Dim tcp As Double = 0
            cm = New MySqlCommand("SELECT `payment_no`,`payment_date`,`amount`,`amount` * ifnull(`interest_rate`*0.01,0) as interest, `amount` + (`amount` * ifnull(`interest_rate`*0.01,0)) as total from tblinstallments where `property_id` = '" & frmPOS.property_id.Text & "' order by `payment_no` asc", cn)
            dr = cm.ExecuteReader
            While dr.Read
                If dr.Item("payment_no").ToString = 0 Then
                Else
                    frmPOS.dgInstallment2.Rows.Add(dr.Item("payment_no").ToString, Format(Convert.ToDateTime(dr.Item("payment_date").ToString), "MMM dd, yyyy"), Format(CDec(dr.Item("amount").ToString), "N2"), Format(CDec(dr.Item("interest").ToString), "N2"), Format(CDec(dr.Item("total").ToString), "N2"))
                    'tcp = tcp + CDbl(dr.Item("total").ToString())
                End If
            End While
            dr.Close()
            cn.Close()

            cn.Open()
            cm = New MySqlCommand("Select ifnull(tcp,0.00) from accountsreceivable where id = '" & frmPOS.property_id.Text & "'", cn)
            frmPOS.total_contract_price.Text = Format(CDec(cm.ExecuteScalar), "N2")
            cn.Close()
        End If


        frmPOS.start_contract.Text = Format(Convert.ToDateTime(frmPOS.dgInstallment2.Rows(0).Cells(1).Value), "MMM dd, yyyy")
        frmPOS.end_contract.Text = Format(Convert.ToDateTime(frmPOS.dgInstallment2.Rows(frmPOS.dgInstallment2.Rows.Count - 1).Cells(1).Value), "MMM dd, yyyy")

        'frmPOS.dgInstallment.DataSource = amortizationTable
        'frmPOS.dgInstallment.Columns("Payment Date").DefaultCellStyle.Format = "MMM dd, yyyy"
        'frmPOS.dgInstallment.Columns("Payment").DefaultCellStyle.Format = "#,##0.00"
        'frmPOS.dgInstallment.Columns("Paid").DefaultCellStyle.Format = "#,##0.00"
        'frmPOS.dgInstallment.Columns("Balance").DefaultCellStyle.Format = "#,##0.00"

        Dim totalAmount As Decimal = frmPOS.property_paid.Text

        For Each row As DataGridViewRow In frmPOS.dgInstallment2.Rows
            Dim amount As Decimal = CDec(row.Cells(4).Value)
            Dim paid As Decimal = Math.Min(amount, totalAmount)

            row.Cells(5).Value = paid
            row.Cells(6).Value = amount - paid

            totalAmount -= paid

            If row.Cells(6).Value = 0 Then
                cn.Open()
                cm = New MySqlCommand("update tblinstallments set status = 'PAID' where property_id = '" & frmPOS.property_id.Text & "' and payment_no = " & CInt(row.Cells(0).Value) & "", cn)
                cm.ExecuteNonQuery()
                cn.Close()
            End If
        Next

        For Each row As DataGridViewRow In frmPOS.dgInstallment2.Rows
            If row.Cells(6).Value <> 0 Then
                row.Selected = True
                frmPOS.dgInstallment2.CurrentCell = row.Cells(6)
                Exit For
            End If
        Next

        Dim valueToRemove As String = "0"

        For i As Integer = frmPOS.dgInstallment2.Rows.Count - 1 To 0 Step -1
            Dim row As DataGridViewRow = frmPOS.dgInstallment2.Rows(i)
            If row.Cells(6).Value IsNot Nothing AndAlso row.Cells(6).Value.ToString() = valueToRemove Then
                frmPOS.dgInstallment2.Rows.Remove(row)
            End If
        Next


    End Sub

    Private Sub ToolStripTextBox1_TextChanged(sender As Object, e As EventArgs) Handles ToolStripTextBox1.TextChanged
        loadclient()
    End Sub

    Private Sub dgPropertyList_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgPropertyList.CellContentClick

    End Sub

    Private Sub ToolStripTextBox1_Click(sender As Object, e As EventArgs) Handles ToolStripTextBox1.Click

    End Sub
End Class