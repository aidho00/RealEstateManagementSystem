Imports MySql.Data.MySqlClient
Imports System.IO

Public Class frmAgentAdd
    Private Sub ToolStripButton2_Click(sender As Object, e As EventArgs) Handles ToolStripButton2.Click
        Me.Dispose()
    End Sub


    Private Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        Try
            If AgentImage.BackgroundImage Is Nothing Then
                MsgBox("Please select image!", vbCritical)
                Return
            End If
            If String.IsNullOrEmpty(txtFname.Text) Or String.IsNullOrEmpty(txtLname.Text) Or String.IsNullOrEmpty(txtMname.Text) Then
                MsgBox("Name field cannot be empty! Please input product description!", vbCritical)
                Return
            ElseIf String.IsNullOrEmpty(txtContact.Text) Then
                MsgBox("Contact field cannot be empty! Please select product category!", vbCritical)
                Return
            ElseIf String.IsNullOrEmpty(txtAddress.Text) Then
                MsgBox("Address field cannot be empty! Please select product status!", vbCritical)
                Return
            ElseIf String.IsNullOrEmpty(txtEmail.Text) Then
                MsgBox("Email field cannot be empty! Please select product status!", vbCritical)
                Return
            ElseIf String.IsNullOrEmpty(txtFBAccount.Text) Then
                MsgBox("FB Account field cannot be empty! Please select product status!", vbCritical)
                Return
            End If
            If MsgBox("Save this Agent information?", vbYesNo + vbQuestion) = vbYes Then
                cn.Open()
                Dim mstream As New MemoryStream
                AgentImage.BackgroundImage.Save(mstream, System.Drawing.Imaging.ImageFormat.Jpeg)
                Dim arrImage() As Byte = mstream.GetBuffer
                cm = New MySqlCommand("Select * from tblclient where fname = '" & txtFname.Text & "' and lname = '" & txtLname.Text & "'", cn)
                dr = cm.ExecuteReader
                If dr.HasRows Then
                    MsgBox("Duplicate Entry Found!", vbCritical)
                    dr.Close()
                    cn.Close()
                Else
                    dr.Close()
                    cm = New MySqlCommand("INSERT INTO `tblagent`(`fname`, `mname`, `lname`, `contact`, `address`, `email`, `fbaccount`, `image`) values (@1, @2, @3, @4, @5, @6, @7, @8)", cn)
                    With cm
                        .Parameters.AddWithValue("@1", txtFname.Text)
                        .Parameters.AddWithValue("@2", txtMname.Text)
                        .Parameters.AddWithValue("@3", txtLname.Text)
                        .Parameters.AddWithValue("@4", txtContact.Text)
                        .Parameters.AddWithValue("@5", txtAddress.Text)
                        .Parameters.AddWithValue("@6", txtEmail.Text)
                        .Parameters.AddWithValue("@7", txtFBAccount.Text)
                        .Parameters.AddWithValue("@8", arrImage)
                    End With
                    result = cm.ExecuteNonQuery
                    If result = 0 Then
                        MsgBox("Agent information has failed to save!", vbCritical)
                        cm.Dispose()
                        cn.Close()
                    Else
                        MsgBox("Agent information has been successfully saved!", vbInformation)
                        cm.Dispose()
                        cn.Close()
                        AuditTrail("Added an Agent information with FName '" & txtFname.Text & "'; MName '" & txtMname.Text & "'; LName '" & txtLname.Text & "'; Address '" & txtAddress.Text & "'; Contact '" & txtContact.Text & "';")
                        clearSave()
                        With frmDiscount
                            .loadAgentRecords()
                        End With
                    End If
                End If
            End If
        Catch ex As Exception
            cn.Close()
            MsgBox(ex.Message, vbCritical)
        End Try
        cn.Close()
    End Sub

    Private Sub btnBrowse_Click(sender As Object, e As EventArgs) Handles btnBrowse.Click
        Using ofd As New OpenFileDialog With {.Filter = "(Image Files)|*.jpg;*.png;*.bmp|Jpg Files|*.jpg|Png Files|*.png|Bitmap Files|*.bmp",
               .Multiselect = False, .Title = "Select Product Image"}

            If ofd.ShowDialog = 1 Then
                AgentImage.BackgroundImage = Image.FromFile(ofd.FileName)
                OpenFileDialog1.FileName = ofd.FileName
            End If
        End Using
    End Sub

    Private Sub frmProduct_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    End Sub


    Sub clearSave()
        txtAddress.Clear()
        txt_ID.Text = "(Auto)"
        txtContact.Clear()
        txtEmail.Clear()
        txtFBAccount.Clear()
        txtFname.Clear()
        txtMname.Clear()
        txtLname.Clear()
        btnUpdate.Enabled = False
        btnSave.Enabled = True
        AgentImage.BackgroundImage = Nothing
    End Sub

    Private Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
        If btnSave.Enabled = True Then
            clearSave()
        ElseIf btnUpdate.Enabled = True Then
            Me.Dispose()
        End If
    End Sub

    Private Sub btnUpdate_Click(sender As Object, e As EventArgs) Handles btnUpdate.Click

        Try

            If AgentImage.BackgroundImage Is Nothing Then
                MsgBox("Please select image!", vbCritical)
                Return
            End If
            If String.IsNullOrEmpty(txtFname.Text) Or String.IsNullOrEmpty(txtLname.Text) Or String.IsNullOrEmpty(txtMname.Text) Then
                MsgBox("Name field cannot be empty! Please input product description!", vbCritical)
                Return
            ElseIf String.IsNullOrEmpty(txtContact.Text) Then
                MsgBox("Contact field cannot be empty! Please select product category!", vbCritical)
                Return
            ElseIf String.IsNullOrEmpty(txtAddress.Text) Then
                MsgBox("Address field cannot be empty! Please select product status!", vbCritical)
                Return
            ElseIf String.IsNullOrEmpty(txtEmail.Text) Then
                MsgBox("Email field cannot be empty! Please select product status!", vbCritical)
                Return
            ElseIf String.IsNullOrEmpty(txtFBAccount.Text) Then
                MsgBox("FB Account field cannot be empty! Please select product status!", vbCritical)
                Return
            End If
            If MsgBox("Update this Agent information?", vbYesNo + vbQuestion) = vbYes Then
                cn.Close()
                cn.Open()
                Dim mstream As New MemoryStream
                AgentImage.BackgroundImage.Save(mstream, System.Drawing.Imaging.ImageFormat.Jpeg)
                Dim arrImage() As Byte = mstream.GetBuffer
                cm = New MySqlCommand("UPDATE `tblagent` SET `fname`=@1,`mname`=@2,`lname`=@3,`contact`=@4,`address`=@5,`email`=@6,`fbaccount`=@7,`image`=@8 WHERE id = @9", cn)
                With cm
                    .Parameters.AddWithValue("@1", txtFname.Text)
                    .Parameters.AddWithValue("@2", txtMname.Text)
                    .Parameters.AddWithValue("@3", txtLname.Text)
                    .Parameters.AddWithValue("@4", txtContact.Text)
                    .Parameters.AddWithValue("@5", txtAddress.Text)
                    .Parameters.AddWithValue("@6", txtEmail.Text)
                    .Parameters.AddWithValue("@7", txtFBAccount.Text)
                    .Parameters.AddWithValue("@8", arrImage)
                    .Parameters.AddWithValue("@9", txt_ID.Text)
                End With
                result = cm.ExecuteNonQuery
                If result = 0 Then
                    MsgBox("Agent information has failed to update!", vbCritical)
                    cm.Dispose()
                    cn.Close()
                Else
                    MsgBox("Agent information has been successfully updated!", vbInformation)
                    cm.Dispose()
                    cn.Close()
                    AuditTrail("Updated an Agent information with FName " & txtFname.Text & "; MName " & txtMname.Text & "; LName " & txtLname.Text & "; Address " & txtAddress.Text & "; Contact " & txtContact.Text & "; where id = " & txt_ID.Text & "")
                    With frmDiscount
                        .loadAgentRecords()
                    End With
                    Me.Dispose()
                End If
            End If
            cn.Close()
        Catch ex As Exception
            cn.Close()
            MsgBox(ex.Message, vbCritical)
        End Try

    End Sub

    Private Sub txtID_TextChanged(sender As Object, e As EventArgs)

    End Sub
End Class