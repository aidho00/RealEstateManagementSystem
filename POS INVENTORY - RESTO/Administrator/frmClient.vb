Imports MySql.Data.MySqlClient
Imports System.IO

Public Class frmClient
#Region " Move Form "

    Public MoveForm As Boolean
    Public MoveForm_MousePosition As Point
    Public Sub MoveForm_MouseDown(sender As Object, e As MouseEventArgs) Handles _
    MyBase.MouseDown, ToolStrip1.MouseDown ' Add more handles here (Example: PictureBox1.MouseDown)
        If e.Button = MouseButtons.Left Then
            MoveForm = True
            Me.Cursor = Cursors.Default
            MoveForm_MousePosition = e.Location
        End If
    End Sub

    Public Sub MoveForm_MouseMove(sender As Object, e As MouseEventArgs) Handles _
    MyBase.MouseMove, ToolStrip1.MouseMove ' Add more handles here (Example: PictureBox1.MouseMove)
        If MoveForm Then
            Me.Location = Me.Location + (e.Location - MoveForm_MousePosition)
        End If
    End Sub

    Public Sub MoveForm_MouseUp(sender As Object, e As MouseEventArgs) Handles _
    MyBase.MouseUp, ToolStrip1.MouseUp ' Add more handles here (Example: PictureBox1.MouseUp)
        If e.Button = MouseButtons.Left Then
            MoveForm = False
            Me.Cursor = Cursors.Default
        End If
    End Sub
#End Region

    Private Sub ToolStripButton2_Click(sender As Object, e As EventArgs) Handles ToolStripButton2.Click
        Me.Dispose()
    End Sub


    Private Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        Try
            If ClientImage.BackgroundImage Is Nothing Then
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
            ElseIf String.IsNullOrEmpty(smn.Text) Or String.IsNullOrEmpty(sln.Text) Or String.IsNullOrEmpty(sfn.Text) Then
                MsgBox("Spouse fields cannot be empty! Please select product status!", vbCritical)
                Return
            ElseIf dtBirthdate.Value = Date.Now Then
                MsgBox("Invalid birthDate! Please select birthdate!", vbCritical)
                Return
            End If
            If MsgBox("Save this Client information?", vbYesNo + vbQuestion) = vbYes Then
                cn.Open()
                Dim mstream As New MemoryStream
                ClientImage.BackgroundImage.Save(mstream, System.Drawing.Imaging.ImageFormat.Jpeg)
                Dim arrImage() As Byte = mstream.GetBuffer
                cm = New MySqlCommand("Select * from tblclient where fname = '" & txtFname.Text & "' and lname = '" & txtLname.Text & "'", cn)
                dr = cm.ExecuteReader
                If dr.HasRows Then
                    MsgBox("Duplicate Entry Found!", vbCritical)
                    dr.Close()
                    cn.Close()
                Else
                    dr.Close()
                    cm = New MySqlCommand("INSERT INTO `tblclient` (`fname`, `mname`, `lname`, `contact`, `address`, `email`, `fbaccount`, `birthdate`, `spouse_lname`, `spouse_fname`, `spouse_mname`, `reference1`, `reference2`, `reference3`, `drivers_lic_no`, `voters`, `sss_no`, `gsis_no`, `employer`, `occupation`, `monthly_income`, `image`, `status`, `tin_no`) values (@1, @2, @3, @4, @5, @6, @7, @8, @9, @10, @11, @12, @13, @14, @15, @16, @17, @18, @19, @20, @21, @22, @23, @24)", cn)
                    With cm
                        .Parameters.AddWithValue("@1", txtFname.Text)
                        .Parameters.AddWithValue("@2", txtMname.Text)
                        .Parameters.AddWithValue("@3", txtLname.Text)
                        .Parameters.AddWithValue("@4", txtContact.Text)
                        .Parameters.AddWithValue("@5", txtAddress.Text)
                        .Parameters.AddWithValue("@6", txtEmail.Text)
                        .Parameters.AddWithValue("@7", txtFBAccount.Text)
                        .Parameters.AddWithValue("@8", dtBirthdate.Value)

                        .Parameters.AddWithValue("@9", sln.Text)
                        .Parameters.AddWithValue("@10", sfn.Text)
                        .Parameters.AddWithValue("@11", smn.Text)
                        .Parameters.AddWithValue("@12", refcontact1.Text)
                        .Parameters.AddWithValue("@13", refcontact2.Text)
                        .Parameters.AddWithValue("@14", refcontact3.Text)
                        .Parameters.AddWithValue("@15", txtDriver.Text)
                        .Parameters.AddWithValue("@16", txtVoters.Text)
                        .Parameters.AddWithValue("@17", txtSSS.Text)
                        .Parameters.AddWithValue("@18", txtGSIS.Text)
                        .Parameters.AddWithValue("@19", txtEmployer.Text)
                        .Parameters.AddWithValue("@20", txtOccupation.Text)
                        .Parameters.AddWithValue("@21", txtIncome.Text)
                        .Parameters.AddWithValue("@22", arrImage)
                        .Parameters.AddWithValue("@23", cbStatus.Text)
                        .Parameters.AddWithValue("@24", cbStatus.Text)
                    End With
                    result = cm.ExecuteNonQuery
                    If result = 0 Then
                        MsgBox("Client information has failed to save!", vbCritical)
                        cm.Dispose()
                        cn.Close()
                    Else
                        MsgBox("Client information has been successfully saved!", vbInformation)
                        cm.Dispose()
                        cn.Close()
                        AuditTrail("Added a client information with FName '" & txtFname.Text & "'; MName '" & txtMname.Text & "'; LName '" & txtLname.Text & "'; Address '" & txtAddress.Text & "'; Contact '" & txtContact.Text & "';")
                        clearSave()
                        With frmTable
                            .loadClientRecords()
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

    Private Sub btnBrowse_Click(sender As Object, e As EventArgs) Handles btnBrowse.Click
        Using ofd As New OpenFileDialog With {.Filter = "(Image Files)|*.jpg;*.png;*.bmp|Jpg Files|*.jpg|Png Files|*.png|Bitmap Files|*.bmp",
               .Multiselect = False, .Title = "Select Product Image"}

            If ofd.ShowDialog = 1 Then
                ClientImage.BackgroundImage = Image.FromFile(ofd.FileName)
                OpenFileDialog1.FileName = ofd.FileName
            End If
        End Using
    End Sub

    Private Sub frmClient_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.KeyPreview = True
    End Sub

    Private Sub frmClient_KeyDown(sender As Object, e As KeyEventArgs) Handles Me.KeyDown
        If e.KeyCode = Keys.Escape Then
            ToolStripButton2_Click(sender, e)
        End If
    End Sub


    Sub clearSave()
        txtAddress.Clear()

        txtContact.Clear()
        txtEmail.Clear()
        txtFBAccount.Clear()
        txtFname.Clear()
        txtMname.Clear()
        txtLname.Clear()
        btnUpdate.Enabled = False
        btnSave.Enabled = True
        ClientImage.BackgroundImage = Nothing

        sln.Clear()
        sfn.Clear()
        smn.Clear()
        refcontact1.Clear()
        refcontact2.Clear()
        refcontact3.Clear()
        txtDriver.Clear()
        txtVoters.Clear()
        txtSSS.Clear()
        txtGSIS.Clear()
        txtEmployer.Clear()
        txtOccupation.Clear()
        txtIncome.Clear()

    End Sub

    Private Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
        If btnSave.Enabled = True Then
            clearSave()
        ElseIf btnUpdate.Enabled = True Then
            Me.Dispose()
        End If
    End Sub

    Private Sub btnUpdate_Click(sender As Object, e As EventArgs) Handles btnUpdate.Click
        cn.Open()
        Try
            If ClientImage.BackgroundImage Is Nothing Then
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
            ElseIf String.IsNullOrEmpty(smn.Text) Or String.IsNullOrEmpty(sln.Text) Or String.IsNullOrEmpty(sfn.Text) Then
                MsgBox("Spouse fields cannot be empty! Please select product status!", vbCritical)
                Return
            ElseIf dtBirthdate.Value = Date.Now Then
                MsgBox("Invalid birthDate! Please select birthdate!", vbCritical)
                Return
            End If
            If MsgBox("Update this client information?", vbYesNo + vbQuestion) = vbYes Then
                Dim mstream As New MemoryStream
                ClientImage.BackgroundImage.Save(mstream, System.Drawing.Imaging.ImageFormat.Jpeg)
                Dim arrImage() As Byte = mstream.GetBuffer
                cm = New MySqlCommand("UPDATE `tblclient` SET `fname`=@2,`mname`=@3,`lname`=@4,`contact`=@5,`address`=@6,`email`=@7,`fbaccount`=@8,`birthdate`=@9,`spouse_lname`=@10,`spouse_fname`=@11,`spouse_mname`=@12,`reference1`=@13,`reference2`=@14,`reference3`=@15,`drivers_lic_no`=@16,`voters`=@17,`sss_no`=@18,`gsis_no`=@19,`employer`=@20,`occupation`=@21,`monthly_income`=@22,`image`=@23,`status`=@24,`tin_no`=@25 WHERE id = @0", cn)
                With cm
                    .Parameters.AddWithValue("@2", txtFname.Text)
                    .Parameters.AddWithValue("@3", txtMname.Text)
                    .Parameters.AddWithValue("@4", txtLname.Text)
                    .Parameters.AddWithValue("@5", txtContact.Text)
                    .Parameters.AddWithValue("@6", txtAddress.Text)
                    .Parameters.AddWithValue("@7", txtEmail.Text)
                    .Parameters.AddWithValue("@8", txtFBAccount.Text)
                    .Parameters.AddWithValue("@9", dtBirthdate.Value)

                    .Parameters.AddWithValue("@10", sln.Text)
                    .Parameters.AddWithValue("@11", sfn.Text)
                    .Parameters.AddWithValue("@12", smn.Text)
                    .Parameters.AddWithValue("@13", refcontact1.Text)
                    .Parameters.AddWithValue("@14", refcontact2.Text)
                    .Parameters.AddWithValue("@15", refcontact3.Text)
                    .Parameters.AddWithValue("@16", txtDriver.Text)
                    .Parameters.AddWithValue("@17", txtVoters.Text)
                    .Parameters.AddWithValue("@18", txtSSS.Text)
                    .Parameters.AddWithValue("@19", txtGSIS.Text)
                    .Parameters.AddWithValue("@20", txtEmployer.Text)
                    .Parameters.AddWithValue("@21", txtOccupation.Text)
                    .Parameters.AddWithValue("@22", txtIncome.Text)
                    .Parameters.AddWithValue("@23", arrImage)
                    .Parameters.AddWithValue("@24", cbStatus.Text)
                    .Parameters.AddWithValue("@25", txtTIN.Text)
                End With
                result = cm.ExecuteNonQuery
                If result = 0 Then
                    MsgBox("Client information has failed to update!", vbCritical)
                    cm.Dispose()
                    cn.Close()
                Else
                    MsgBox("Client information has been successfully updated!", vbInformation)
                    cm.Dispose()
                    cn.Close()
                    AuditTrail("Updated a client information with FName " & txtFname.Text & "; MName " & txtMname.Text & "; LName " & txtLname.Text & "; Address " & txtAddress.Text & "; Contact " & txtContact.Text & "; wher id = " & txt_ID.Text & "")
                    With frmTable
                        .loadClientRecords()
                    End With
                    Me.Dispose()
                End If
            End If
        Catch ex As Exception
            cn.Close()
            MsgBox(ex.Message, vbCritical)
        End Try
        cn.Close()
    End Sub

    Private Sub Label25_Click(sender As Object, e As EventArgs) Handles Label25.Click, Label26.Click

    End Sub

    Private Sub dtBirthdate_ValueChanged(sender As Object, e As EventArgs) Handles dtBirthdate.ValueChanged
        Try
            Dim dateOfBirth As DateTime = dtBirthdate.Value
            Dim DateNow As String = Format(Convert.ToDateTime(frmLogin.txtbox_date.Text), "yyyy/MM/dd")
            Dim today As DateTime = DateTime.Parse(DateNow)
            Dim age As Integer = today.Year - dateOfBirth.Year
            If today < dateOfBirth.AddYears(age) Then
                age -= 1
            End If
            txtAge.Text = age & " Years Old"
        Catch ex As Exception
            txtAge.Text = "# Years Old"
        End Try

    End Sub

    Sub CenterPanelInForm(panel As Panel)
        Dim newX As Integer = (Me.ClientSize.Width - panel.Width) \ 2
        Dim newY As Integer = (Me.ClientSize.Height - panel.Height) \ 2
        panel.Location = New Point(newX, newY)
        panel.Anchor = AnchorStyles.None
    End Sub
End Class