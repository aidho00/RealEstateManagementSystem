Imports MySql.Data.MySqlClient

Public Class frmSalesSummary
    Private Sub ToolStripButton2_Click(sender As Object, e As EventArgs) Handles ToolStripButton2.Click
        Me.Dispose()
    End Sub

    Private Sub frmSalesSummary_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.KeyPreview = True
        cbSelect.SelectedIndex = 0
    End Sub
    Sub loadDate()
        Try
            cn.Open()
            cm = New MySqlCommand("select IFNULL(min(date_paid),CURDATE()) as sdate from tblpayment", cn)
            dr = cm.ExecuteReader
            While dr.Read
                dtFrom.Value = dr.Item("sdate")
            End While
            dr.Close()
            cn.Close()
            dtTo.Value = Now
        Catch ex As Exception
            'dtFrom.Value = Now
            dtTo.Value = Now
            cn.Close()
        End Try
    End Sub

    Private Sub frmSalesSummary_KeyDown(sender As Object, e As KeyEventArgs) Handles Me.KeyDown
        If e.KeyCode = Keys.Escape Then
            ToolStripButton2_Click(sender, e)
        End If
    End Sub

    Private Sub dtFrom_ValueChanged(sender As Object, e As EventArgs) Handles dtFrom.ValueChanged, dtTo.ValueChanged
        loadDailySales()
        loadMonthlySales()
        loadQuarterlySales()
        loadYearlySales()
    End Sub

    Sub loadYearlySales()
        Try
            cn.Close()
            Dim sdate1 As String = dtFrom.Value.ToString("yyyy-MM-dd")
            Dim sdate2 As String = dtTo.Value.ToString("yyyy-MM-dd")
            dgYearlySales.Rows.Clear()
            cn.Open()
            cm = New MySqlCommand("select year(date_paid) as year, sum(amount) as total from tblpayment where date_paid between '" & sdate1 & "' and '" & sdate2 & "' group by year(date_paid) order by year(date_paid) desc", cn)
            dr = cm.ExecuteReader
            While dr.Read
                dgYearlySales.Rows.Add(dr.Item("year").ToString, Format(CDec(dr.Item("total").ToString), "#,##0.00"))
            End While
            dr.Close()
            cn.Close()
        Catch ex As Exception
            MsgBox(ex.Message, vbCritical)
            cn.Close()
        End Try
    End Sub

    Sub loadQuarterlySales()
        Try
            cn.Close()
            Dim sdate1 As String = dtFrom.Value.ToString("yyyy-MM-dd")
            Dim sdate2 As String = dtTo.Value.ToString("yyyy-MM-dd")
            dgQuarterlySales.Rows.Clear()
            cn.Open()
            cm = New MySqlCommand("select year(date_paid) as year, quarter(date_paid) as quarter, sum(amount) as total from tblpayment where date_paid between '" & sdate1 & "' and '" & sdate2 & "' group by year(date_paid), quarter(date_paid) order by year(date_paid) desc, quarter(date_paid) desc", cn)
            dr = cm.ExecuteReader
            While dr.Read
                dgQuarterlySales.Rows.Add(dr.Item("year").ToString, dr.Item("quarter").ToString, Format(CDec(dr.Item("total").ToString), "#,##0.00"))
            End While
            dr.Close()
            cn.Close()
        Catch ex As Exception
            MsgBox(ex.Message, vbCritical)
            cn.Close()
        End Try
    End Sub

    Sub loadMonthlySales()
        Try
            cn.Close()
            Dim sdate1 As String = dtFrom.Value.ToString("yyyy-MM-dd")
            Dim sdate2 As String = dtTo.Value.ToString("yyyy-MM-dd")
            dgMonthlySales.Rows.Clear()
            cn.Open()
            cm = New MySqlCommand("select year(date_paid) as year, DATE_FORMAT(date_paid,'%M') as month, sum(amount) as total from tblpayment where date_paid between '" & sdate1 & "' and '" & sdate2 & "' group by year(date_paid), DATE_FORMAT(date_paid,'%M') order by year(date_paid) desc, DATE_FORMAT(date_paid,'%M') desc", cn)
            dr = cm.ExecuteReader
            While dr.Read
                dgMonthlySales.Rows.Add(dr.Item("year").ToString, dr.Item("month").ToString, Format(CDec(dr.Item("total").ToString), "#,##0.00"))
            End While
            dr.Close()
            cn.Close()
        Catch ex As Exception
            MsgBox(ex.Message, vbCritical)
            cn.Close()
        End Try
    End Sub

    Sub loadDailySales()
        Try
            cn.Close()
            Dim sdate1 As String = dtFrom.Value.ToString("yyyy-MM-dd")
            Dim sdate2 As String = dtTo.Value.ToString("yyyy-MM-dd")
            dgDailySales.Rows.Clear()
            cn.Open()
            cm = New MySqlCommand("select year(date_paid) as year, DATE_FORMAT(date_paid,'%M') as month, DATE_FORMAT(date_paid,'%d') as days, sum(amount) as total from tblpayment where date_paid between '" & sdate1 & "' and '" & sdate2 & "' group by year(date_paid), DATE_FORMAT(date_paid,'%M'), DATE_FORMAT(date_paid,'%d') order by year(date_paid) desc, DATE_FORMAT(date_paid,'%M') desc, DATE_FORMAT(date_paid,'%d')", cn)
            dr = cm.ExecuteReader
            While dr.Read
                dgDailySales.Rows.Add(dr.Item("year").ToString, dr.Item("month").ToString, dr.Item("days").ToString, Format(CDec(dr.Item("total").ToString), "#,##0.00"))
            End While
            dr.Close()
            cn.Close()
        Catch ex As Exception
            MsgBox(ex.Message, vbCritical)
            cn.Close()
        End Try
    End Sub

    Private Sub ToolStripLabel1_Click(sender As Object, e As EventArgs) Handles ToolStripLabel1.Click
        OpenFormToAdminPanel(frmPrintPreview)

        If cbSelect.Text = "YEARLY" Then
            Dim dt As New DataTable
            With dt
                .Columns.Add("year")
                .Columns.Add("total")
            End With
            For Each dr As DataGridViewRow In dgYearlySales.Rows
                dt.Rows.Add(dr.Cells(0).Value, Replace(dr.Cells(1).Value, ",", ""))
            Next
            Dim rptdoc As CrystalDecisions.CrystalReports.Engine.ReportDocument
            rptdoc = New reportYearlySalesSummary
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

                .SetParameterValue("date1", dtFrom.Value.ToString("yyyy-MM-dd"))
                .SetParameterValue("date2", dtTo.Value.ToString("yyyy-MM-dd"))
            End With
            frmPrintPreview.ReportViewer.ReportSource = rptdoc
            AuditTrail("Generated Payments Summary (YEARLY) Details from " & dtFrom.Value.ToString("yyyy-MM-dd") & " to " & dtTo.Value.ToString("yyyy-MM-dd") & "")
        ElseIf cbSelect.Text = "QUARTERLY" Then
            Dim dt As New DataTable
            With dt
                .Columns.Add("year")
                .Columns.Add("quarter")
                .Columns.Add("total")
            End With
            For Each dr As DataGridViewRow In dgQuarterlySales.Rows
                dt.Rows.Add(dr.Cells(0).Value, dr.Cells(1).Value, Replace(dr.Cells(2).Value, ",", ""))
            Next
            Dim rptdoc As CrystalDecisions.CrystalReports.Engine.ReportDocument
            rptdoc = New reportQuarterlySalesSummary
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
                .SetParameterValue("date1", dtFrom.Value.ToString("yyyy-MM-dd"))
                .SetParameterValue("date2", dtTo.Value.ToString("yyyy-MM-dd"))
            End With
            frmPrintPreview.ReportViewer.ReportSource = rptdoc
            AuditTrail("Generated Payments Summary (QUARTERLY) Details from " & dtFrom.Value.ToString("yyyy-MM-dd") & " to " & dtTo.Value.ToString("yyyy-MM-dd") & "")
        ElseIf cbSelect.Text = "MONTHLY" Then
            Dim dt As New DataTable
            With dt
                .Columns.Add("year")
                .Columns.Add("month")
                .Columns.Add("total")
            End With
            For Each dr As DataGridViewRow In dgMonthlySales.Rows
                dt.Rows.Add(dr.Cells(0).Value, dr.Cells(1).Value, Replace(dr.Cells(2).Value, ",", ""))
            Next
            Dim rptdoc As CrystalDecisions.CrystalReports.Engine.ReportDocument
            rptdoc = New reportMonthlySalesSummary
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
                .SetParameterValue("date1", dtFrom.Value.ToString("yyyy-MM-dd"))
                .SetParameterValue("date2", dtTo.Value.ToString("yyyy-MM-dd"))
            End With
            frmPrintPreview.ReportViewer.ReportSource = rptdoc
            AuditTrail("Generated Payments Summary (MONTHLY) Details from " & dtFrom.Value.ToString("yyyy-MM-dd") & " to " & dtTo.Value.ToString("yyyy-MM-dd") & "")
        ElseIf cbSelect.Text = "DAILY" Then
            Dim dt As New DataTable
            With dt
                .Columns.Add("year")
                .Columns.Add("month")
                .Columns.Add("day")
                .Columns.Add("total")
            End With
            For Each dr As DataGridViewRow In dgDailySales.Rows
                dt.Rows.Add(dr.Cells(0).Value, dr.Cells(1).Value, dr.Cells(2).Value, Replace(dr.Cells(3).Value, ",", ""))
            Next
            Dim rptdoc As CrystalDecisions.CrystalReports.Engine.ReportDocument
            rptdoc = New reportDailySalesSummary
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
                .SetParameterValue("date1", dtFrom.Value.ToString("yyyy-MM-dd"))
                .SetParameterValue("date2", dtTo.Value.ToString("yyyy-MM-dd"))
            End With
            frmPrintPreview.ReportViewer.ReportSource = rptdoc
            AuditTrail("Generated Payments Summary (DAILY) Details from " & dtFrom.Value.ToString("yyyy-MM-dd") & " to " & dtTo.Value.ToString("yyyy-MM-dd") & "")
        End If
    End Sub

    Private Sub cbStatus_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cbSelect.SelectedIndexChanged

    End Sub

    Private Sub cbStatus_KeyPress(sender As Object, e As KeyPressEventArgs) Handles cbSelect.KeyPress
        e.Handled = True
    End Sub
End Class