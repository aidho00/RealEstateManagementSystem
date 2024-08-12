Imports MySql.Data.MySqlClient

Public Class frmSettings
    Private Sub ToolStripButton2_Click(sender As Object, e As EventArgs) Handles ToolStripButton2.Click
        Me.Dispose()
    End Sub

    Private Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click

        If String.IsNullOrEmpty(txtName.Text) Then
            MsgBox("Fields cannot be empty!", vbCritical)
            Return
        ElseIf String.IsNullOrEmpty(txtH1.Text) Then
            MsgBox("Fields cannot be empty!", vbCritical)
            Return
        ElseIf String.IsNullOrEmpty(txtH2.Text) Then
            MsgBox("Fields cannot be empty!", vbCritical)
            Return
        ElseIf String.IsNullOrEmpty(txtH3.Text) Then
            MsgBox("Fields cannot be empty!", vbCritical)
            Return
        End If

        If MsgBox("Save this settings?", vbYesNo + vbQuestion) = vbYes Then
            cn.Open()
            cm = New MySqlCommand("Select * from tblsetting", cn)
            dr = cm.ExecuteReader
            If dr.HasRows Then
                dr.Close()
                cm = New MySqlCommand("UPDATE `tblsetting` SET `companyname`=@1,`h1`=@2,`h2`=@3,`h3`=@4", cn)
                With cm
                    .Parameters.AddWithValue("@1", txtName.Text)
                    .Parameters.AddWithValue("@2", txtH1.Text)
                    .Parameters.AddWithValue("@3", txtH2.Text)
                    .Parameters.AddWithValue("@4", txtH3.Text)
                End With
                result = cm.ExecuteNonQuery
                If result = 0 Then
                    MsgBox("Data has failed to save!", vbCritical)
                    cm.Dispose()
                    cn.Close()
                Else
                    MsgBox("Data has been successfully saved!", vbInformation)
                    cm.Dispose()
                    cn.Close()
                    AuditTrail("Updated system setting; company/bussiness name; " & txtName.Text & "; H1; " & txtH1.Text & "; H2; " & txtH2.Text & "; H1; " & txtH3.Text & "")
                End If
                Me.Dispose()
            Else
                dr.Close()
                cm = New MySqlCommand("INSERT INTO `tblsetting`(`companyname`, `h1`, `h2`, `h3`) VALUES (@1,@2,@3,@4)", cn)
                With cm
                    .Parameters.AddWithValue("@1", txtName.Text)
                    .Parameters.AddWithValue("@2", txtH1.Text)
                    .Parameters.AddWithValue("@3", txtH2.Text)
                    .Parameters.AddWithValue("@4", txtH3.Text)
                End With
                result = cm.ExecuteNonQuery
                If result = 0 Then
                    MsgBox("Data has failed to save!", vbCritical)
                    cm.Dispose()
                    cn.Close()
                Else
                    MsgBox("Data has been successfully saved!", vbInformation)
                    cm.Dispose()
                    cn.Close()
                    AuditTrail("Updated system setting; company/bussiness name; " & txtName.Text & "; Address; " & txtH1.Text & "; H1; " & txtH2.Text & "; H2; " & txtH3.Text & "")
                End If
                Me.Dispose()
            End If

            If str_role = "Administrator" Then
            Else
                cn.Close()
                cn.Open()
                cm = New MySqlCommand("Select * from tbluser where username = '" & str_user & "' and change_name_count = 0", cn)
                dr = cm.ExecuteReader
                If dr.HasRows Then
                    dr.Close()
                    cn.Close()
                    cn.Open()
                    cm = New MySqlCommand("UPDATE `tbluser` SET `name` = '" & txtbox_name.Text & "', change_name_count = 1 where `username` = '" & str_user & "'", cn)
                    cm.ExecuteNonQuery()
                    cn.Close()
                Else
                    dr.Close()
                    cn.Close()

                    cn.Open()
                    cm = New MySqlCommand("Select * from tbluser where username = '" & str_user & "' and name = '" & txtbox_name.Text & "'", cn)
                    dr = cm.ExecuteReader
                    If dr.HasRows Then
                        dr.Close()
                        cn.Close()
                    Else
                        dr.Close()
                        cn.Close()
                        MsgBox("You already change your name once. You do not have permission to update your name anymore. Please contact sytem administrator if you want to change your name.", vbExclamation)
                    End If
                End If
            End If
        End If
    End Sub

    Private Sub frmSettings_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try
            load_data("select * from tblsetting", "CURR")
            txtName.Text = ds.Tables("CURR").Rows(0)(0).ToString
            txtH1.Text = ds.Tables("CURR").Rows(0)(1).ToString
            txtH2.Text = ds.Tables("CURR").Rows(0)(2).ToString
            txtH3.Text = ds.Tables("CURR").Rows(0)(3).ToString
            ds = New DataSet

            txtbox_name.Text = str_name
        Catch ex As Exception
            'MsgBox(ex.Message, vbCritical)
        End Try
    End Sub
End Class