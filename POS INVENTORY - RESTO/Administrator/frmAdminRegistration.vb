Imports MySql.Data.MySqlClient

Public Class frmAdminRegistration
    Private Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
        Me.Close()
        frmLogin.Close()
    End Sub

    Private Sub Label1_Click(sender As Object, e As EventArgs) Handles Label1.Click
        Me.Close()
        frmLogin.Close()
    End Sub

    Private Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        Try
            If String.IsNullOrEmpty(txtName.Text) Then
                MsgBox("Description field cannot be empty! Please input user name!", vbCritical)
                Return
            ElseIf String.IsNullOrEmpty(txtUsername.Text) Then
                MsgBox("Status field cannot be empty! Please input user username!", vbCritical)
                Return
            ElseIf String.IsNullOrEmpty(txtPassword.Text) Then
                MsgBox("Status field cannot be empty! Please input user password!", vbCritical)
                Return
            ElseIf String.IsNullOrEmpty(txtRePassword.Text) Then
                MsgBox("Status field cannot be empty! Please input user password!", vbCritical)
                Return
            ElseIf txtRePassword.Text <> txtPassword.Text Then
                MsgBox("Re entered password does not matched!", vbCritical)
                Return
            End If
            If MsgBox("Save this user?", vbYesNo + vbQuestion) = vbYes Then
                cn.Open()
                cm = New MySqlCommand("Select * from tbluser where username = '" & txtName.Text & "'", cn)
                dr = cm.ExecuteReader
                If dr.HasRows Then
                    MsgBox("Duplicate User Found!", vbCritical)
                    dr.Close()
                    cn.Close()
                Else
                    dr.Close()
                    cm = New MySqlCommand("Insert into tbluser (username, password, name, role) values (@1, sha2(@2, 224), @3, @4)", cn)
                    With cm
                        .Parameters.AddWithValue("@1", txtUsername.Text)
                        .Parameters.AddWithValue("@2", txtPassword.Text)
                        .Parameters.AddWithValue("@3", txtName.Text)
                        .Parameters.AddWithValue("@4", "Administrator")
                    End With
                    result = cm.ExecuteNonQuery
                    If result = 0 Then
                        MsgBox("User has failed to save!", vbCritical)
                        cm.Dispose()
                        cn.Close()
                    Else
                        MsgBox("User has been successfully saved!", vbInformation)
                        cm.Dispose()
                        cn.Close()
                        Me.Dispose()
                    End If
                End If
            End If
        Catch ex As Exception
            cn.Close()
            MsgBox(ex.Message, vbCritical)
        End Try
        cn.Close()
    End Sub
End Class