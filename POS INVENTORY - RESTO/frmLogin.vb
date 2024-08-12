Imports MySql.Data.MySqlClient

Public Class frmLogin
    Private Sub frmLogin_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.KeyPreview = True
        txtUsername.Text = My.Settings.Username
        OpenDBConnection()
        Try
            If CountRecords("select count(*) from tbluser where role = 'Administrator'") = 0 Then
                With frmAdminRegistration
                    .ShowDialog()
                End With
            End If
            DateToday()
            ''Timer2.Start()
            'Timer1.Start()
        Catch ex As Exception
            Me.Size = New Size(540, 421)
            MsgBox("System Connection Failed!", vbCritical)
            Panel2.Enabled = False
        End Try
    End Sub

    Private Sub btnLogin_Click(sender As Object, e As EventArgs) Handles btnLogin.Click
        'Try
        Dim found As Boolean
            If txtUsername.Text = String.Empty Or txtPassword.Text = String.Empty Then
                MsgBox("Required empty field.", vbCritical)
                Return
            End If
            cn.Open()
            cm = New MySqlCommand("select * from tbluser where username = @1 and password = sha2(@2, 224)", cn)
            With cm
                .Parameters.AddWithValue("@1", txtUsername.Text)
                .Parameters.AddWithValue("@2", txtPassword.Text)
            End With
        dr = cm.ExecuteReader
        dr.Read()
            If dr.HasRows Then
                found = True
                str_user = dr.Item("username").ToString
                str_pass = dr.Item("password").ToString
                str_name = dr.Item("name").ToString
                str_role = dr.Item("role").ToString
            Else
                found = False
            End If
        cn.Close()

        Try
            load_data("select * from tblsetting", "CURR")

            str_company = ds.Tables("CURR").Rows(0)(0).ToString
            str_address = ds.Tables("CURR").Rows(0)(1).ToString
            str_h1 = ds.Tables("CURR").Rows(0)(2).ToString

            ds = New DataSet
        Catch ex As Exception
            'MsgBox(ex.Message, vbCritical)
        End Try

        If found = True Then
                MsgBox("Welcome " & str_name & "!", vbInformation)
            AuditTrail("Has Logged-in")
            My.Settings.Username = txtUsername.Text
            'Login()
            If str_role = "Administrator" Then
                With frmMain
                    .lblRole.Text = "Role: " & str_role
                    .lblUser.Text = "Name: " & str_name
                    DashBoard()
                    .Show()
                End With
            ElseIf str_role = "Transaction Coordinator" Then
                With frmMain
                    .lblRole.Text = "Role: " & str_role
                    .lblUser.Text = "Name: " & str_name
                    DashBoard()
                    .btnAuditTrail.Visible = False
                    .btnSales.Visible = True
                    .btnUsers.Visible = False
                    .btnCancelledOrder.Visible = True
                    .btnBilling.Visible = True
                    .Button3.Visible = True
                    .Show()
                End With
            ElseIf str_role = "Payment Coordinator" Then
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
            End If
                txtPassword.Text = ""
                Me.Hide()
            Else
                MsgBox("Invalid username or password!", vbExclamation)
            End If
        'Catch ex As Exception
        '    cn.Close()
        '    MsgBox(ex.Message, vbCritical)
        'End Try


    End Sub

    Private Sub DateToday()
        Try
            load_data("SELECT date_format(curdate(), '%M %d %Y'), time_format(curtime(), '%h:%i %p'), dayname(curdate()), LEFT(time_format(curtime(), '%h:%i:%s %p'),5), RIGHT(time_format(curtime(), '%h:%i %p'),2), LEFT(time_format(curtime(), '%h:%i:%s %p'),8), date_format(curdate(), '%M %d, %Y')", "CURR")
            txtbox_date.Text = ds.Tables("CURR").Rows(0)(0).ToString
            ds = New DataSet
        Catch ex As Exception
            'MsgBox(ex.Message, vbCritical)
        End Try
    End Sub

    Private Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
        If MsgBox("Are you sure you want to exit?", vbYesNo + vbQuestion) = vbYes Then
            MsgBox("System Exit!", vbExclamation)
            Application.Exit()
        End If
    End Sub

    Private Sub txtPassword_TextChanged(sender As Object, e As EventArgs) Handles txtPassword.TextChanged

    End Sub

    Private Sub txtPassword_KeyDown(sender As Object, e As KeyEventArgs) Handles txtPassword.KeyDown
        If e.KeyCode = Keys.Enter Then
            btnLogin_Click(sender, e)
        End If
    End Sub

    Private Sub frmLogin_KeyDown(sender As Object, e As KeyEventArgs) Handles Me.KeyDown
        If e.KeyCode = Keys.Escape Then
            btnCancel_Click(sender, e)
        End If
    End Sub

    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        'PaymentAlert()
    End Sub

    Private Sub Timer3_Tick(sender As Object, e As EventArgs) Handles Timer3.Tick

    End Sub

    Private Sub Label3_Click(sender As Object, e As EventArgs) Handles Label3.Click
        'If Me.Size = New Size(270, 421) Then
        '    Me.Size = New Size(540, 421)
        'Else
        '    Me.Size = New Size(270, 421)
        'End If
    End Sub

    Private Sub Label9_Click(sender As Object, e As EventArgs) Handles Label9.Click
        Try
            cn.Close()
            cn.Open()

            If CountRecords("select count(*) from tbluser where role = 'Administrator'") = 0 Then
                Panel2.Enabled = True
                Me.Size = New Size(270, 421)
                MsgBox("System Connected Successfully!", vbInformation)
                With frmAdminRegistration
                    .ShowDialog()
                End With
            End If
            cn.Close()
        Catch ex As Exception
            cn.Close()
            MsgBox("System Connection Failed!", vbCritical)
        End Try
    End Sub
End Class