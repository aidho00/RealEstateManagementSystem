Imports MySql.Data.MySqlClient

Public Class frmStart
    Private Sub Label1_Click(sender As Object, e As EventArgs) Handles Label1.Click
        Me.Dispose()
    End Sub

    Private Sub txtCash_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtCash.KeyPress
        Select Case Asc(e.KeyChar)
            Case 48 To 57
            Case 46
            Case 8
            Case 13
                btnStart_Click(sender, e)
            Case Else
                e.Handled = True
        End Select
    End Sub


    Private Sub btnStart_Click(sender As Object, e As EventArgs) Handles btnStart.Click
        Try
            If txtCash.Text = String.Empty Then
                Return
            End If

            Dim sdate As String = Now.ToString("yyyy-MM-dd")
            cn.Open()
            cm = New MySqlCommand("Insert into tblstart (sdate, initialcash, status, cashier)values(@1,@2,@3,@4)", cn)
            With cm
                .Parameters.AddWithValue("@1", sdate)
                .Parameters.AddWithValue("@2", CDbl(txtCash.Text))
                .Parameters.AddWithValue("@3", "OPEN")
                .Parameters.AddWithValue("@4", str_user)
                .ExecuteNonQuery()
            End With
            cm.Dispose()
            cn.Close()

            AuditTrail("Starts billing transaction for the day with starting cash of " & CDbl(txtCash.Text) & ".")

            With frmPOS
                If checkStatus() = True Then
                    .btnStart.Enabled = False
                    .btnEnd.Enabled = True
                    .btnPayment.Enabled = True
                    '.btnCancelOrder.Enabled = True
                Else
                    .btnPayment.Enabled = False
                    .btnStart.Enabled = True
                    .btnEnd.Enabled = False
                    '.btnCancelOrder.Enabled = False
                End If
            End With
            Me.Dispose()
        Catch ex As Exception
            cn.Close()
            MsgBox(ex.Message, vbCritical)
        End Try
    End Sub

    Private Sub frmStart_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.KeyPreview = True
    End Sub

    Private Sub frmStart_KeyDown(sender As Object, e As KeyEventArgs) Handles Me.KeyDown
        If e.KeyCode = Keys.Escape Then
            Me.Dispose()
        End If
    End Sub

    Private Sub txtCash_TextChanged(sender As Object, e As EventArgs) Handles txtCash.TextChanged
        If txtCash.Text = "" Then
            txtCash.Text = "0.00"
            txtCash.SelectionStart = txtCash.TextLength
        Else

            txtCash.Text = Format(CDec(txtCash.Text), "#,##0")
            txtCash.SelectionStart = txtCash.TextLength
        End If
    End Sub
End Class