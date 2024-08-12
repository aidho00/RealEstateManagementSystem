Imports MySql.Data.MySqlClient

Public Class frmUserRegistration


    Private Sub Label1_Click(sender As Object, e As EventArgs) Handles Label1.Click
        Me.Dispose()
    End Sub

    Private Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        Try
            If String.IsNullOrEmpty(txtName.Text) Then
                MsgBox("Name field cannot be empty! Please input user name!", vbCritical)
                Return
            ElseIf String.IsNullOrEmpty(txtUsername.Text) Then
                MsgBox("Username field cannot be empty! Please input user username!", vbCritical)
                Return
            ElseIf String.IsNullOrEmpty(txtPassword.Text) Then
                MsgBox("Password field cannot be empty! Please input user password!", vbCritical)
                Return
            ElseIf String.IsNullOrEmpty(txtRePassword.Text) Then
                MsgBox("Password field cannot be empty! Please input user password!", vbCritical)
                Return
            ElseIf String.IsNullOrEmpty(cbRole.Text) Then
                MsgBox("Role field cannot be empty! Please select user role!", vbCritical)
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
                        .Parameters.AddWithValue("@4", cbRole.Text)

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
                        AuditTrail("Added a new user '" & txtName.Text & "'; username '" & txtUsername.Text & "'; role '" & cbRole.Text & "'.")
                        With frmUserList
                            .loadUserRecords()
                        End With
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

    Private Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
        Me.Dispose()
    End Sub

    Private Sub btnUpdate_Click(sender As Object, e As EventArgs) Handles btnUpdate.Click
        Try
            If CheckBox1.Checked = True Then
                If String.IsNullOrEmpty(txtName.Text) Then
                    MsgBox("Name field cannot be empty! Please input user name!", vbCritical)
                    Return
                ElseIf String.IsNullOrEmpty(txtPassword.Text) Then
                    MsgBox("Password field cannot be empty! Please input user password!", vbCritical)
                    Return
                ElseIf String.IsNullOrEmpty(txtRePassword.Text) Then
                    MsgBox("Password field cannot be empty! Please input user password!", vbCritical)
                    Return
                ElseIf String.IsNullOrEmpty(cbRole.Text) Then
                    MsgBox("Role field cannot be empty! Please select user role!", vbCritical)
                    Return
                ElseIf txtRePassword.Text <> txtPassword.Text Then
                    MsgBox("Re Eentered password does not matched!", vbCritical)
                    Return
                End If
                If MsgBox("Update this user?", vbYesNo + vbQuestion) = vbYes Then

                    cn.Open()
                    cm = New MySqlCommand("Update tbluser set password=sha2(@2,224), name=@3, role=@4, status=@5 where username=@1", cn)
                    With cm
                        .Parameters.AddWithValue("@1", txtUsername.Text)
                        .Parameters.AddWithValue("@2", txtPassword.Text)
                        .Parameters.AddWithValue("@3", txtName.Text)
                        .Parameters.AddWithValue("@4", cbRole.Text)
                        .Parameters.AddWithValue("@5", cbStatus.Text)
                    End With
                    result = cm.ExecuteNonQuery
                    If result = 0 Then
                        MsgBox("User has failed to update!", vbCritical)
                        cm.Dispose()
                        cn.Close()
                    Else
                        MsgBox("User has been successfully update!", vbInformation)
                        cm.Dispose()
                        cn.Close()
                        AuditTrail("Updated a user username " & txtUsername.Text & ".")
                        With frmUserList
                            .loadUserRecords()
                        End With
                        Me.Dispose()
                    End If
                End If
            Else
                If String.IsNullOrEmpty(txtName.Text) Then
                    MsgBox("Description field cannot be empty! Please input user name!", vbCritical)
                    Return
                ElseIf String.IsNullOrEmpty(cbRole.Text) Then
                    MsgBox("Status field cannot be empty! Please select user role!", vbCritical)
                    Return
                End If
                If MsgBox("Update this user?", vbYesNo + vbQuestion) = vbYes Then

                    cn.Open()
                    cm = New MySqlCommand("Update tbluser set name=@3, role=@4 where username=@1", cn)
                    With cm
                        .Parameters.AddWithValue("@1", txtUsername.Text)
                        .Parameters.AddWithValue("@3", txtName.Text)
                        .Parameters.AddWithValue("@4", cbRole.Text)
                    End With
                    result = cm.ExecuteNonQuery
                    If result = 0 Then
                        MsgBox("User has failed to update!", vbCritical)
                        cm.Dispose()
                        cn.Close()
                    Else
                        MsgBox("User has been successfully update!", vbInformation)
                        cm.Dispose()
                        cn.Close()
                        AuditTrail("Update a user username " & txtUsername.Text & ".")
                        With frmUserList
                            .loadUserRecords()
                        End With
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

    Private Sub CheckBox1_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBox1.CheckedChanged

    End Sub

    Private Sub frmUserRegistration_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        cbStatus.SelectedIndex = 0
    End Sub

    Private Sub cbStatus_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cbStatus.SelectedIndexChanged

    End Sub

    Private Sub cbStatus_KeyPress(sender As Object, e As KeyPressEventArgs) Handles cbStatus.KeyPress, cbRole.KeyPress
        e.Handled = True
    End Sub

    Private Sub cbRole_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cbRole.SelectedIndexChanged

    End Sub
End Class