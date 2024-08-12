Imports MySql.Data.MySqlClient

Public Class frmLedger
    Private Sub ToolStripButton2_Click(sender As Object, e As EventArgs) Handles ToolStripButton2.Click
        Me.Dispose()
    End Sub

    Sub lodTransactions()
        Try
            Dim sdate1 As String = dtFrom.Value.ToString("yyyy-MM-dd")
            Dim sdate2 As String = dtTo.Value.ToString("yyyy-MM-dd")

            dgSales.Rows.Clear()
            Dim i As Integer
            cn.Close()
            cn.Open()
            cm = New MySqlCommand("SELECT t1.`transaction_no`, CONCAT(t2.lname, ', ', t2.fname, ' ',t2.mname) as 'client', t1.`property_id` as 'property_id', t1.`amount`, DATE_FORMAT(t1.`date_paid`, '%m/%d/%Y') as 'date_paid', t5.`name`, `t1`.payment_mode, `t1`.remarks FROM `tblpayment` t1 JOIN `tblclient` t2 ON t1.client_id = t2.id JOIN `tblproperty` t3 ON t1.`property_id` = t3.`id` JOIN `tblpropertytype` t4 ON t3.`type_id` = t4.`id` JOIN `tbluser` t5 ON t1.`user` = t5.`username` where t1.payment_status = 'COMPLETED' and t1.`date_paid` between '" & sdate1 & "' and '" & sdate2 & "' and t1.property_id = '" & property_id.Text & "' order by t1.`date_paid` desc", cn)
            dr = cm.ExecuteReader
            While dr.Read
                i += 1
                dgSales.Rows.Add(i, dr.Item("property_id").ToString, dr.Item("transaction_no").ToString, dr.Item("client").ToString, dr.Item("payment_mode").ToString, Format(CDec(dr.Item("amount").ToString), "#,##0.00"), Format(Convert.ToDateTime(dr.Item("date_paid").ToString), "MMM dd, yyyy"), dr.Item("remarks").ToString)
            End While
            dr.Close()
            cn.Close()
        Catch ex As Exception
            MsgBox(ex.Message, vbCritical)
            cn.Close()
        End Try
    End Sub

    Private Sub frmLedger_Load(sender As Object, e As EventArgs) Handles Me.Load

    End Sub

    Private Sub Label16_Click(sender As Object, e As EventArgs) Handles Label16.Click
        Try
            cn.Close()
            cn.Open()
            cm = New MySqlCommand("select property_date from tblproperty where id = @1", cn)
            With cm
                .Parameters.AddWithValue("@1", property_id.Text)
            End With
            dr = cm.ExecuteReader
            dr.Read()
            If dr.HasRows Then
                dtFrom.Value = dr.Item("property_date")
            End If
            cn.Close()
        Catch ex As Exception
            MsgBox(ex.Message, vbCritical)
            cn.Close()
        End Try
        dtTo.Text = Format(Convert.ToDateTime(frmLogin.txtbox_date.Text), "yyyy-MM-dd")
        lodTransactions()
    End Sub

    Private Sub dtFrom_ValueChanged(sender As Object, e As EventArgs) Handles dtFrom.ValueChanged, dtTo.ValueChanged
        lodTransactions()
    End Sub

    Private Sub Label17_Click(sender As Object, e As EventArgs) Handles Label17.Click
        Try
            OpenFormToAdminPanel(frmPrintPreview)
            Dim dt As New DataTable
            With dt
                .Columns.Add("number")
                .Columns.Add("payment_date")
                .Columns.Add("opening_bal")
                .Columns.Add("payment")
                .Columns.Add("interest")
                .Columns.Add("closing_bal")
            End With
            For Each dr As DataGridViewRow In dgSales.Rows
                If dr.Cells(0).Value = "0" Then
                Else
                dt.Rows.Add(dr.Cells(0).Value, dr.Cells(1).Value, dr.Cells(3).Value, dr.Cells(4).Value, dr.Cells(5).Value, dr.Cells(6).Value)
            End If
            Next
            Dim rptdoc As CrystalDecisions.CrystalReports.Engine.ReportDocument
            rptdoc = New reportPropertyLedger
            With rptdoc
                .SetDataSource(dt)
                .SetParameterValue("price", Format(CDec(contract_price.Text), "#,##0.00"))
                .SetParameterValue("total_price", Format(CDec(total_contract_price.Text), "#,##0.00"))
                .SetParameterValue("subdivision", subdivision.Text)
                .SetParameterValue("property_type", property_type.Text)
                .SetParameterValue("block", block.Text)
                .SetParameterValue("lot", lot.Text)
                .SetParameterValue("square_meter", square_meter.Text)
                .SetParameterValue("term", term.Text)
                .SetParameterValue("payment_option", payment_option.Text)
                .SetParameterValue("down_payment", Format(CDec(down_payment.Text), "#,##0.00"))
                .SetParameterValue("monthly_payment", Format(CDec(monthly_payment.Text), "#,##0.00"))
                .SetParameterValue("agent", agent.Text)
                .SetParameterValue("client_name", client_name.Text)
                .SetParameterValue("collection", Format(CDec(payment_collected.Text), "#,##0.00"))

                .SetParameterValue("start_contract", start_contract.Text)
                .SetParameterValue("end_contract", end_contract.Text)

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
            frmPrintPreview.ReportViewer.ReportSource = rptdoc
        Catch ex As Exception
        MsgBox(ex.Message, vbCritical)
        End Try
    End Sub

    Private Sub Label21_Click(sender As Object, e As EventArgs) Handles Label21.Click

    End Sub
End Class