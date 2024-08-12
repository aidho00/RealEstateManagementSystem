Imports MySql.Data.MySqlClient

Public Class frmAdminPermission
    Dim credRole As String
    Dim credName As String
    Dim DateNow As String = Format(Convert.ToDateTime(frmLogin.txtbox_date.Text), "MM-dd-yyyy")
    Private Sub frmAdminPermission_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.KeyPreview = True
    End Sub

    Private Sub frmAdminPermission_KeyDown(sender As Object, e As KeyEventArgs) Handles Me.KeyDown
        If e.KeyCode = Keys.Escape Then
            Me.Dispose()
        ElseIf e.KeyCode = Keys.Enter Then
            btnGrant_Click(sender, e)
        End If
    End Sub

    Sub validateCredential()
        Try
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
                credRole = dr.Item("role").ToString
                credName = dr.Item("name").ToString
            Else
                found = False
            End If
            cn.Close()

            If found = True Then
                If credRole = "Administrator" Then
                    If MsgBox("Grant access to this user?", vbYesNo + vbQuestion) = vbYes Then
                        MsgBox("Permission granted.", vbInformation)
                        credRole = ""
                        grant_access()
                        credName = ""
                    Else
                        MsgBox("Permission denied.", vbInformation)
                    End If

                ElseIf credRole = "Cashier" Then
                    MsgBox("Account credential invalid!", vbCritical)
                Else
                    MsgBox("Failed to cancel order!", vbExclamation)
                    Me.Dispose()
                End If
            Else
                MsgBox("Invalid username or password!", vbExclamation)
            End If
        Catch ex As Exception
            cn.Close()
            MsgBox(ex.Message, vbCritical)
        End Try
    End Sub
    Private Sub btnGrant_Click(sender As Object, e As EventArgs) Handles btnGrant.Click
        validateCredential()
    End Sub

    Private Sub grant_access()
        If txtArea.Text = "POS - Cashier Payments Transaction" Then
            With frmSalesDetails
                .lblTitle.Text = "CASHIER " & str_name.ToString.ToUpper & " DAILY PAYMENTS TRANSACTIONS [" & DateNow & "]"
                .loadSales()
                .ShowDialog()
            End With
            AuditTrail("Viewed Daily Payments Transactions [" & DateNow & "] with admin '" & credName & "' permission.")
            Me.Dispose()
        End If
    End Sub
End Class