Imports MySql.Data.MySqlClient

Public Class frmBestSelling
    Private Sub frmBestSelling_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.KeyPreview = True

        dtFrom.Value = Now
        dtTo.Value = Now
    End Sub

    Private Sub frmBestSelling_KeyDown(sender As Object, e As KeyEventArgs) Handles Me.KeyDown
        If e.KeyCode = Keys.Escape Then
            ToolStripButton2_Click(sender, e)
        End If
    End Sub

    Sub loadRecords()
        Try
            Dim sdate1 As String = dtFrom.Value.ToString("yyyy-MM-dd")
            Dim sdate2 As String = dtTo.Value.ToString("yyyy-MM-dd")
            dgBestSelling.Rows.Clear()
            Dim i As Integer
            cn.Open()
            cm = New MySqlCommand("SELECT CONCAT(t4.name) as 'property', SUM(t1.`amount`) as 'totalamount' FROM `tblpayment` t1 JOIN `tblclient` t2 ON t1.client_id = t2.id JOIN `tblproperty` t3 ON t1.`property_id` = t3.`id` JOIN `tblpropertytype` t4 ON t3.`type_id` = t4.`id` where t1.payment_status = 'COMPLETED' and t1.`date_paid` between '" & sdate1 & "' and '" & sdate2 & "' group by t4.`name` order by totalamount desc", cn)
            dr = cm.ExecuteReader
            While dr.Read
                i += 1
                dgBestSelling.Rows.Add(i, dr.Item("property").ToString, Format(CDec(dr.Item("totalamount").ToString), "#,##0.00"))
            End While
            dr.Close()
            cn.Close()
        Catch ex As Exception
            MsgBox(ex.Message, vbCritical)
            cn.Close()
        End Try
    End Sub

    Private Sub dtFrom_ValueChanged(sender As Object, e As EventArgs) Handles dtFrom.ValueChanged, dtTo.ValueChanged
        loadRecords()
    End Sub

    Private Sub ToolStripButton2_Click(sender As Object, e As EventArgs) Handles ToolStripButton2.Click
        Me.Dispose()
    End Sub

    Private Sub ToolStripLabel1_Click(sender As Object, e As EventArgs) Handles ToolStripLabel1.Click
        OpenFormToAdminPanel(frmPrintPreview)
        Dim dt As New DataTable
        With dt
            .Columns.Add("description")
            .Columns.Add("total")
        End With
        For Each dr As DataGridViewRow In dgBestSelling.Rows
            dt.Rows.Add(dr.Cells(1).Value, dr.Cells(2).Value)
        Next
        Dim rptdoc As CrystalDecisions.CrystalReports.Engine.ReportDocument
        rptdoc = New reportBestSelling
        With rptdoc
            .SetDataSource(dt)
            .SetParameterValue("preparedby", str_name)
            .SetParameterValue("DateRange", dtFrom.Text & " - " & dtTo.Text)

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
        AuditTrail("Generated Payment Monitoring Summary Details from " & dtFrom.Value.ToString("yyyy-MM-dd") & " to " & dtTo.Value.ToString("yyyy-MM-dd") & "")
    End Sub
End Class