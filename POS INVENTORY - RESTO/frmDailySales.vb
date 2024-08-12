Imports MySql.Data.MySqlClient

Public Class frmDailySales
    Private Sub Label1_Click(sender As Object, e As EventArgs) Handles Label1.Click
        Me.Dispose()
    End Sub

    Private Sub frmDailySales_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    End Sub

    Sub generateSales()
        Try
            Dim sdate As String = Format(Convert.ToDateTime(frmLogin.txtbox_date.Text), "yyyy-MM-dd")

            cn.Open()
            cm = New MySqlCommand("Select ifnull(sum(amount),0) as total from tblpayment where date_paid between '" & sdate & "' and '" & sdate & "' and payment_status = 'COMPLETED' and user = '" & str_user & "' ", cn)
            lblSales.Text = Format(CDbl(cm.ExecuteScalar), "#,##0.00")
            cn.Close()

            cn.Open()
            cm = New MySqlCommand("Select ifnull(initialcash,0) as total from tblstart where sdate between '" & sdate & "' and '" & sdate & "' and cashier = '" & str_user & "'", cn)
            lblInitialCash.Text = Format(CDbl(cm.ExecuteScalar), "#,##0.00")
            cn.Close()

            Dim _total As Double = CDbl(lblSales.Text) + CDbl(lblInitialCash.Text)

            lblTotal.Text = Format(_total, "#,##0.00")
        Catch ex As Exception
            cn.Close()
            MsgBox(ex.Message, vbCritical)
        End Try
    End Sub

    Private Sub btnView_Click(sender As Object, e As EventArgs) Handles btnView.Click

    End Sub
End Class