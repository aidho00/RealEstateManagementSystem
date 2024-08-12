Imports MySql.Data.MySqlClient

Public Class frmCancelOrderList
    Sub loadSales()
        Try
            Dim sdate1 As String = dtFrom.Value.ToString("yyyy-MM-dd")
            Dim sdate2 As String = dtTo.Value.ToString("yyyy-MM-dd")
            dgSales.Rows.Clear()
            Dim i As Integer
            cn.Open()
            cm = New MySqlCommand("SELECT t1.`id`, t1.`transaction_no`, CONCAT(t2.lname, ', ', t2.fname, ' ',t2.mname) as 'client', CONCAT(t1.`property_id`,' - ', t4.name) as 'property_id', t1.`amount`, DATE_FORMAT(t1.`date_paid`, '%m/%d/%Y') as 'date_paid', t5.`name`, t1.`cancellation_reason` FROM `tblpayment` t1 JOIN `tblclient` t2 ON t1.client_id = t2.id JOIN `tblproperty` t3 ON t1.`property_id` = t3.`id` JOIN `tblpropertytype` t4 ON t3.`type_id` = t4.`id` JOIN `tbluser` t5 ON t1.`user` = t5.`username` where t1.payment_status = 'CANCELLED' and t1.`date_paid` between '" & sdate1 & "' and '" & sdate2 & "'", cn)
            dr = cm.ExecuteReader
            While dr.Read
                i += 1
                dgSales.Rows.Add(i, dr.Item("id").ToString, dr.Item("transaction_no").ToString, dr.Item("client").ToString, dr.Item("property_id").ToString, Format(CDec(dr.Item("amount").ToString), "#,##0.00"), dr.Item("date_paid").ToString, dr.Item("name").ToString, dr.Item("cancellation_reason").ToString)
            End While
            dr.Close()
            cn.Close()
        Catch ex As Exception
            MsgBox(ex.Message, vbCritical)
            cn.Close()
        End Try
    End Sub
    Private Sub frmCancelOrderList_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.KeyPreview = True

        dtFrom.Value = Now
        dtTo.Value = Now
    End Sub

    Private Sub frmCancelOrderList_KeyDown(sender As Object, e As KeyEventArgs) Handles Me.KeyDown
        If e.KeyCode = Keys.Escape Then
            ToolStripButton2_Click(sender, e)
        End If
    End Sub

    Private Sub dtFrom_ValueChanged(sender As Object, e As EventArgs) Handles dtFrom.ValueChanged, dtTo.ValueChanged
        loadSales()
    End Sub

    Private Sub ToolStripLabel1_Click(sender As Object, e As EventArgs) Handles ToolStripLabel1.Click
        OpenFormToAdminPanel(frmPrintPreview)
        Dim dt As New DataTable
        With dt
            .Columns.Add("transno")
            .Columns.Add("description")
            .Columns.Add("price")
            .Columns.Add("qty")
            .Columns.Add("total")
        End With
        For Each dr As DataGridViewRow In dgSales.Rows
            dt.Rows.Add(dr.Cells(2).Value, dr.Cells(3).Value, dr.Cells(4).Value, dr.Cells(6).Value, dr.Cells(5).Value)
        Next
        Dim rptdoc As CrystalDecisions.CrystalReports.Engine.ReportDocument
        rptdoc = New reportCancelledOrder
        With rptdoc
            .SetDataSource(dt)
            .SetParameterValue("preparedby", str_name)
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
            .SetParameterValue("DateRange", dtFrom.Text & " - " & dtTo.Text)
        End With
        frmPrintPreview.ReportViewer.ReportSource = rptdoc
        AuditTrail("Generated Cancelled Payments Details from " & dtFrom.Value.ToString("yyyy-MM-dd") & " to " & dtTo.Value.ToString("yyyy-MM-dd") & "")
    End Sub

    Private Sub ToolStripButton2_Click(sender As Object, e As EventArgs) Handles ToolStripButton2.Click
        Me.Dispose()
    End Sub
End Class