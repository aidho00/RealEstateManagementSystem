Imports MySql.Data.MySqlClient
Public Class frmMain
    Private Sub frmMain_KeyDown(sender As Object, e As KeyEventArgs) Handles Me.KeyDown
        If e.KeyCode = Keys.Escape Then
            If adminPanel.Controls.Count > 1 Then
                If Me.Focus Then
                    ExitToDashBoard()
                End If
            ElseIf adminPanel.Controls.Count = 0 Then
                btnLogout_Click(sender, e)
            End If
        End If
    End Sub

    Private Sub frmMain_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        'Timer1.Start()
        Timer2.Start()
        PaymentAlert()
        company_name.Text = str_company
        Me.KeyPreview = True
    End Sub

    Private Sub btnManageProduct_Click(sender As Object, e As EventArgs) Handles btnManageProduct.Click
        cn.Close()
        cn.Open()
        cm = New MySqlCommand("select * from tblclient", cn)
        dr = cm.ExecuteReader
        dr.Read()
        If dr.HasRows Then
            cn.Close()
            cn.Open()
            cm = New MySqlCommand("select * from tblpropertytype", cn)
            dr = cm.ExecuteReader
            dr.Read()
            If dr.HasRows Then
                With frmProductList
                    .loadPropertyRecords()
                    OpenFormToAdminPanel(frmProductList)
                End With
            Else
                MsgBox("No subdivisions available for the transaction. Please add subdivisions before proceeding.", vbCritical)
            End If
            cn.Close()
        Else
            MsgBox("No clients available for the transaction. Please add clients before proceeding.", vbCritical)
        End If
        cn.Close()
    End Sub

    Private Sub btnManageTable_Click(sender As Object, e As EventArgs) Handles btnManageTable.Click
        With frmTable
            .loadClientRecords()
            OpenFormToAdminPanel(frmTable)
        End With
    End Sub

    Private Sub btnLogout_Click(sender As Object, e As EventArgs) Handles btnLogout.Click
        If MsgBox("Are you sure you want to exit?", vbYesNo + vbQuestion) = vbYes Then
            Me.Close()

            My.Application.OpenForms.Cast(Of Form)() _
             .Except({frmLogin}) _
             .ToList() _
             .ForEach(Sub(form) form.Close())

            frmLogin.Show()
        End If
    End Sub

    Private Sub btnSales_Click(sender As Object, e As EventArgs) Handles btnSales.Click
        With frmSales
            .loadSales()
            OpenFormToAdminPanel(frmSales)
        End With
    End Sub

    Private Sub btnUsers_Click(sender As Object, e As EventArgs) Handles btnUsers.Click
        With frmUserList
            .loadUserRecords()
            OpenFormToAdminPanel(frmUserList)
        End With
    End Sub

    Private Sub btnCancelledOrder_Click(sender As Object, e As EventArgs) Handles btnCancelledOrder.Click
        With frmCancelOrderList
            .loadSales()
            OpenFormToAdminPanel(frmCancelOrderList)
        End With
    End Sub

    Private Sub btnBestSelling_Click(sender As Object, e As EventArgs) Handles btnBestSelling.Click

    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        With frmSalesSummary
            .loadDate()
            .loadYearlySales()
            .loadQuarterlySales()
            .loadMonthlySales()
            .loadDailySales()
            OpenFormToAdminPanel(frmSalesSummary)
        End With
    End Sub

    Private Sub frmMain_FormClosed(sender As Object, e As FormClosedEventArgs) Handles Me.FormClosed
        AuditTrail("Has Logged-out")
        Logout()
    End Sub

    Private Sub btnAuditTrail_Click(sender As Object, e As EventArgs) Handles btnAuditTrail.Click
        With frmAuditTrail
            .loadAuditTrail()
            OpenFormToAdminPanel(frmAuditTrail)
        End With
    End Sub

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        With frmLogHistory
            .loadSubdivisions()
            OpenFormToAdminPanel(frmLogHistory)
        End With
    End Sub

    Private Sub btnDiscount_Click(sender As Object, e As EventArgs) Handles btnDiscount.Click
        With frmDiscount
            .loadAgentRecords()
            OpenFormToAdminPanel(frmDiscount)
        End With
    End Sub

    Private Sub Button5_Click(sender As Object, e As EventArgs) Handles Button5.Click
        With frmSettings
            .ShowDialog()
        End With
    End Sub

    Private Sub Label1_Click(sender As Object, e As EventArgs) Handles Label1.Click
        With frmTable
            .loadClientRecords()
            OpenFormToAdminPanel(frmTable)
        End With
    End Sub

    Private Sub Label3_Click(sender As Object, e As EventArgs) Handles Label3.Click
        With frmDiscount
            .loadAgentRecords()
            OpenFormToAdminPanel(frmDiscount)
        End With
    End Sub

    Private Sub company_name_Click(sender As Object, e As EventArgs) Handles company_name.Click

    End Sub

    Private Sub company_name_DoubleClick(sender As Object, e As EventArgs) Handles company_name.DoubleClick
        ExitToDashBoard()
    End Sub

    Private Sub ExitToDashBoard()
        Dim formsToClose As New List(Of Form)()

        For Each control As Control In adminPanel.Controls
            If TypeOf control Is Form Then
                Dim form As Form = DirectCast(control, Form)
                formsToClose.Add(form)
            End If
        Next

        For Each formToClose As Form In formsToClose
            formToClose.Close()
        Next
    End Sub

    Private Sub LoadAlert_Click(sender As Object, e As EventArgs) Handles LoadAlert.Click
        PaymentAlert()

        If AlertPanel2.Visible = True Then
            AlertPanel2.Visible = False
            LoadAlert.Text = "📤"
        ElseIf AlertPanel2.Visible = False Then
            AlertPanel2.Visible = True
            LoadAlert.Text = "📥"
        End If
    End Sub

    Private Sub dgAlert_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgAlert.CellContentClick
        Dim colname As String = dgAlert.Columns(e.ColumnIndex).Name
        If colname = "colView" Then
            With frmPayments
                .property_id.Text = dgAlert.CurrentRow.Cells(1).Value
                .lblTitle.Text = "" & dgAlert.CurrentRow.Cells(2).Value & " - " & dgAlert.CurrentRow.Cells(3).Value & " " & dgAlert.CurrentRow.Cells(4).Value & " |  PAYMENTS"
                .calculatepayments_client()
                .ShowDialog()
            End With
        End If
    End Sub

    Private Sub dgAlert_MouseHover(sender As Object, e As EventArgs) Handles dgAlert.MouseHover
        'frmLogin.Timer1.Stop()
        'Timer1.Stop()
    End Sub

    Private Sub dgAlert_MouseLeave(sender As Object, e As EventArgs) Handles dgAlert.MouseLeave
        'frmLogin.Timer1.Start()
        'Timer1.Start()
    End Sub

    Private Sub dgAlert_MouseEnter(sender As Object, e As EventArgs) Handles dgAlert.MouseEnter
        'frmLogin.Timer1.Stop()
        'Timer1.Stop()
    End Sub

    Private Sub dgAlert_CellMouseEnter(sender As Object, e As DataGridViewCellEventArgs) Handles dgAlert.CellMouseEnter
        If e.ColumnIndex = 6 Then
            dgAlert.Cursor = Cursors.Hand
        Else
            dgAlert.Cursor = Cursors.Default
        End If
        'frmLogin.Timer1.Stop()
        'Timer1.Stop()
    End Sub

    Private Sub dgAlert_CellMouseLeave(sender As Object, e As DataGridViewCellEventArgs) Handles dgAlert.CellMouseLeave
        dgAlert.Cursor = Cursors.Default
    End Sub

    Private Sub btnBilling_Click(sender As Object, e As EventArgs) Handles btnBilling.Click
        With frmPOS
            If checkStatus() = True Then
                .btnPayment.Enabled = True
                .btnStart.Enabled = False
                .btnEnd.Enabled = True
                '.btnCancelOrder.Enabled = True
            Else
                .btnPayment.Enabled = False
                .btnStart.Enabled = True
                .btnEnd.Enabled = False
                '.btnCancelOrder.Enabled = False
            End If
            .lblRole.Text = "Role: " & str_role
            .lblUser.Text = "Name: " & str_name
            .Show()
        End With
    End Sub

    Private Sub Label5_Click(sender As Object, e As EventArgs) Handles Label5.Click
        With frmLogHistory
            .loadSubdivisions()
            OpenFormToAdminPanel(frmLogHistory)
        End With
    End Sub

    Private Sub lblTotalProducts_Click(sender As Object, e As EventArgs) Handles lblTotalProducts.Click

    End Sub

    Private Sub lblTotalClient_Click(sender As Object, e As EventArgs) Handles lblTotalClient.Click

    End Sub

    Private Sub Label6_Click(sender As Object, e As EventArgs) Handles Label6.Click

    End Sub

    Private Sub sales_date_Click(sender As Object, e As EventArgs) Handles sales_date.Click
        With frmSales
            .loadSales()
            OpenFormToAdminPanel(frmSales)
        End With
    End Sub

    Private Sub Timer2_Tick(sender As Object, e As EventArgs) Handles Timer2.Tick
        Try
            load_data("SELECT date_format(curdate(), '%b %d, %Y'), time_format(curtime(), '%h:%i %p'), dayname(curdate()), LEFT(time_format(curtime(), '%h:%i:%s %p'),5), RIGHT(time_format(curtime(), '%h:%i %p'),2), LEFT(time_format(curtime(), '%h:%i:%s %p'),8), date_format(curdate(), '%M %d, %Y')", "CURR")
            datetime.Text = ds.Tables("CURR").Rows(0)(0).ToString & " - " & ds.Tables("CURR").Rows(0)(1).ToString
            ds = New DataSet
        Catch ex As Exception
            'MsgBox(ex.Message, vbCritical)
        End Try
        DashBoard()
    End Sub

    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        PaymentAlert()
    End Sub

    Private Sub frmMain_Closed(sender As Object, e As EventArgs) Handles Me.Closed

    End Sub

    Private Sub Timer3_Tick(sender As Object, e As EventArgs) Handles Timer3.Tick

    End Sub

    Private Sub dgAlert_Scroll(sender As Object, e As ScrollEventArgs) Handles dgAlert.Scroll

    End Sub
End Class