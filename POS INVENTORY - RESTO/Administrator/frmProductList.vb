Imports MySql.Data.MySqlClient
Imports System.IO
Public Class frmProductList
    Private Sub ToolStripButton1_Click(sender As Object, e As EventArgs) Handles ToolStripButton1.Click
        With frmProduct
            OpenFormToAdminPanel(frmProduct)
            .loadCategory()
            .btnSave.Enabled = True
            .btnUpdate.Enabled = False
            .txtBlock.Focus()
            .print_contract.Visible = False
        End With
    End Sub

    Private Sub ToolStripButton2_Click(sender As Object, e As EventArgs) Handles ToolStripButton2.Click
        Me.Dispose()
    End Sub

    Private Sub ToolStripTextBox1_TextChanged(sender As Object, e As EventArgs) Handles ToolStripTextBox1.TextChanged
        loadPropertyRecords()
    End Sub

    Sub loadPropertyRecords()
        dgPropertyList.Rows.Clear()
        Dim i As Integer
        cn.Close()
        cn.Open()
        cm = New MySqlCommand("SELECT t1.`id`, CONCAT(t3.lname, ', ' ,t3.fname,' ',t3.mname) as client, t2.`name` as type, t1.`block`, t1.`lot`, t1.`square_meter`, t1.`price`, t1.`status`, CONCAT(t4.lname, ', ' ,t4.fname,' ',t4.mname) as 'agent_id', t1.`thumbnail`, t1.`monthly_amort`, DATE_FORMAT(t1.`start_contract`, '%b %d, %Y') as 'start_contract', DATE_FORMAT(t1.`end_contract`, '%b %d, %Y') as 'end_contract', t1.`length_years`, t1.`interest_rate`, date_format(t1.`property_date`, '%b %d, %Y') as 'property_date', t1.`status`, t5.total_payments, t1.payment_option, t1.down_payment, t1.property_type, t1.interest_rate, t1.commission, t1.penalty FROM `tblproperty` t1 JOIN `tblpropertytype` t2 ON t1.type_id = t2.id JOIN `tblclient` t3 ON t1.client_id = t3.id JOIN `tblagent` t4 ON t1.`agent_id` = t4.`id` JOIN accountsreceivable t5 ON t1.id = t5.id where (CONCAT(t3.lname, ', ' ,t3.fname,' ',t3.mname) like '%" & ToolStripTextBox1.Text & "%' or t2.`name` like '%" & ToolStripTextBox1.Text & "%' or t1.`id` like '%" & ToolStripTextBox1.Text & "%') order by t1.`start_contract` desc", cn)
        dr = cm.ExecuteReader
        While dr.Read
            i += 1
            dgPropertyList.Rows.Add(i, dr.Item("id").ToString, dr.Item("client").ToString, dr.Item("type").ToString, dr.Item("block").ToString, dr.Item("lot").ToString, dr.Item("square_meter").ToString, Format(CDec(dr.Item("price").ToString), "#,##0.00"), dr.Item("status").ToString, dr.Item("thumbnail"), dr.Item("agent_id").ToString, dr.Item("monthly_amort").ToString, dr.Item("start_contract").ToString, dr.Item("end_contract").ToString, dr.Item("length_years").ToString, dr.Item("interest_rate").ToString, dr.Item("property_date").ToString, dr.Item("status").ToString, dr.Item("total_payments").ToString, dr.Item("payment_option").ToString, dr.Item("down_payment").ToString, dr.Item("property_type").ToString, dr.Item("interest_rate").ToString, dr.Item("commission").ToString, dr.Item("penalty").ToString, "👁️‍🗨️")
        End While
        dr.Close()
        dgPropertyList.Columns("Column9").DefaultCellStyle.Format = "MM/dd/yyyy"
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

    Private Sub dgPropertyList_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgPropertyList.CellContentClick
        Dim colname As String = dgPropertyList.Columns(e.ColumnIndex).Name
        If colname = "colEdit" Then

            With frmProduct
                OpenFormToAdminPanel(frmProduct)
                .loadCategory()
                .print_contract.Visible = True

                .cbOption.Text = dgPropertyList.CurrentRow.Cells(19).Value

                .txtID.Text = dgPropertyList.CurrentRow.Cells(1).Value
                .cbClient.Text = dgPropertyList.CurrentRow.Cells(2).Value
                .cbSubdivision.Text = dgPropertyList.CurrentRow.Cells(3).Value
                .txtBlock.Text = dgPropertyList.CurrentRow.Cells(4).Value
                .txtlot.Text = dgPropertyList.CurrentRow.Cells(5).Value
                .txtSquaremeter.Text = dgPropertyList.CurrentRow.Cells(6).Value
                .txtPrice.Text = dgPropertyList.CurrentRow.Cells(7).Value
                .txtLength.Text = dgPropertyList.CurrentRow.Cells(14).Value
                .cbAgent.Text = dgPropertyList.CurrentRow.Cells(10).Value
                .cbStatus.Text = dgPropertyList.CurrentRow.Cells(17).Value
                .property_paid.Text = dgPropertyList.CurrentRow.Cells(18).Value
                .txtDownPayment.Text = dgPropertyList.CurrentRow.Cells(20).Value
                .cbPropertyType.Text = dgPropertyList.CurrentRow.Cells(21).Value
                .txtRate.Text = dgPropertyList.CurrentRow.Cells(22).Value
                .datetoday.Text = CDate(dgPropertyList.CurrentRow.Cells(16).Value)

                .dtToday.Value = CDate(dgPropertyList.CurrentRow.Cells(16).Value)

                .commission_id.Text = dgPropertyList.CurrentRow.Cells(23).Value

                Try
                    cn.Close()
                    cn.Open()
                    cm = New MySqlCommand("Select commission_percentage from tblcomission where id = " & CInt(dgPropertyList.CurrentRow.Cells(23).Value) & "", cn)
                    .txtComission.Text = cm.ExecuteScalar
                    cn.Close()
                Catch ex As Exception
                End Try
                Try
                    cn.Close()
                    cn.Open()
                    cm = New MySqlCommand("Select commision_months from tblcomission where id = " & CInt(dgPropertyList.CurrentRow.Cells(23).Value) & "", cn)
                    .commission_month.Text = cm.ExecuteScalar
                    cn.Close()
                Catch ex As Exception
                End Try

                .cbPenalty.Text = dgPropertyList.CurrentRow.Cells(24).Value
                Try
                    .client_id.Text = CInt(.cbClient.SelectedValue)
                Catch ex As Exception
                End Try
                Try
                    .agent_id.Text = CInt(.cbAgent.SelectedValue)
                Catch ex As Exception
                End Try
                Try
                    .sub_id.Text = CInt(.cbSubdivision.SelectedValue)
                Catch ex As Exception
                End Try

                .calculateamortization()
                .btnUpdate.Enabled = True
                .btnSave.Enabled = False
                Me.Enabled = False
            End With
        ElseIf colname = "colDelete" Then
            'cn.Open()
            'cm = New MySqlCommand("select image from tblproduct where id = '" & dgProductList.Rows(e.RowIndex).Cells(1).Value.ToString & "'", cn)
            'cm.ExecuteNonQuery()
            'cn.Close()
            'AuditTrail("Deleted a product; description " & dgProductList.Rows(e.RowIndex).Cells(1).Value.ToString & "; category " & dgProductList.Rows(e.RowIndex).Cells(4).Value.ToString & "; weight " & dgProductList.Rows(e.RowIndex).Cells(5).Value.ToString & "; price " & dgProductList.Rows(e.RowIndex).Cells(3).Value.ToString & " where id " & dgProductList.Rows(e.RowIndex).Cells(1).Value.ToString & "")
            'MsgBox("Product successfully deleted!", vbInformation)
            'DashBoard()

            MsgBox("Unable to delete Property!", vbCritical)

        ElseIf colname = "colView" Then
            With frmLedger
                OpenFormToAdminPanel(frmLedger)
                .payment_option.Text = dgPropertyList.CurrentRow.Cells(19).Value

                .property_id.Text = dgPropertyList.CurrentRow.Cells(1).Value
                .client_name.Text = dgPropertyList.CurrentRow.Cells(2).Value
                .subdivision.Text = dgPropertyList.CurrentRow.Cells(3).Value
                .block.Text = dgPropertyList.CurrentRow.Cells(4).Value
                .lot.Text = dgPropertyList.CurrentRow.Cells(5).Value
                .square_meter.Text = dgPropertyList.CurrentRow.Cells(6).Value
                .contract_price.Text = Format(CDec(dgPropertyList.CurrentRow.Cells(7).Value), "#,##0.00")
                .term.Text = "" & CInt(dgPropertyList.CurrentRow.Cells(14).Value) & " Years Or " & CInt(dgPropertyList.CurrentRow.Cells(14).Value) * 12 & " Months"
                .agent.Text = dgPropertyList.CurrentRow.Cells(10).Value
                .total_paid.Text = Format(CDec(dgPropertyList.CurrentRow.Cells(18).Value), "#,##0.00")
                .down_payment.Text = Format(CDec(dgPropertyList.CurrentRow.Cells(20).Value), "#,##0.00")
                .property_type.Text = dgPropertyList.CurrentRow.Cells(21).Value
                .monthly_payment.Text = Format(CDec(dgPropertyList.CurrentRow.Cells(11).Value), "#,##0.00")
                .total_contract_price.Text = Format(CDec(dgPropertyList.CurrentRow.Cells(7).Value) - CDec(dgPropertyList.CurrentRow.Cells(20).Value), "#,##0.00")

                .payment_collected.Text = Format(CDec(dgPropertyList.CurrentRow.Cells(18).Value), "#,##0.00")
                .start_contract.Text = dgPropertyList.CurrentRow.Cells(12).Value
                .end_contract.Text = dgPropertyList.CurrentRow.Cells(13).Value

                Try
                    cn.Close()
                    cn.Open()
                    cm = New MySqlCommand("select property_date from tblproperty where id = @1", cn)
                    With cm
                        .Parameters.AddWithValue("@1", frmLedger.property_id.Text)
                    End With
                    dr = cm.ExecuteReader
                    dr.Read()
                    If dr.HasRows Then
                        frmLedger.dtFrom.Value = dr.Item("property_date")
                    End If
                    cn.Close()
                Catch ex As Exception
                    MsgBox(ex.Message, vbCritical)
                    cn.Close()
                End Try

                .lodTransactions()
            End With
        End If
    End Sub

    Private Sub calculateinstallmentpayment()

    End Sub

    Private Sub frmProductList_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.KeyPreview = True
    End Sub

    Private Sub frmProductList_KeyDown(sender As Object, e As KeyEventArgs) Handles Me.KeyDown
        If e.KeyCode = Keys.Escape Then
            ToolStripButton2_Click(sender, e)
        End If
    End Sub


    Private Sub dgPropertyList_CellMouseEnter(sender As Object, e As DataGridViewCellEventArgs) Handles dgPropertyList.CellMouseEnter
        If e.ColumnIndex = 24 Then
            dgPropertyList.Cursor = Cursors.Hand
        Else
            dgPropertyList.Cursor = Cursors.Default
        End If
    End Sub

    Private Sub dgClientList_CellMouseLeave(sender As Object, e As DataGridViewCellEventArgs) Handles dgPropertyList.CellMouseLeave
        dgPropertyList.Cursor = Cursors.Default
    End Sub
End Class