Imports MySql.Data.MySqlClient
Imports System.IO
Public Class frmTable
    Private Sub ToolStripButton1_Click(sender As Object, e As EventArgs) Handles ToolStripButton1.Click
        With frmClient
            OpenFormToAdminPanel(frmClient)
            .CenterPanelInForm(.Panel2)
            .btnSave.Enabled = True
            .btnUpdate.Enabled = False
            .txtFname.Focus()

        End With
    End Sub

    Private Sub ToolStripButton2_Click(sender As Object, e As EventArgs) Handles ToolStripButton2.Click
        Me.Dispose()
    End Sub

    Sub loadClientRecords()
        dgClientList.Rows.Clear()
        Dim i As Integer
        cn.Open()
        cm = New MySqlCommand("select * from tblclient where (fname like '%" & ToolStripTextBox1.Text & "%' or mname like '%" & ToolStripTextBox1.Text & "%' or lname like '%" & ToolStripTextBox1.Text & "%')", cn)
        dr = cm.ExecuteReader
        While dr.Read
            i += 1
            dgClientList.Rows.Add(i, dr.Item("id").ToString, dr.Item("fname").ToString, dr.Item("mname").ToString, dr.Item("lname").ToString, dr.Item("address").ToString, dr.Item("image"))
        End While
        dr.Close()

        For i = 0 To dgClientList.Rows.Count - 1
            Dim r As DataGridViewRow = dgClientList.Rows(i)
            r.Height = 100
        Next
        If dgClientList.Rows.Count = 0 Then
        Else
            Dim imageColumn = DirectCast(dgClientList.Columns("Column6"), DataGridViewImageColumn)
            imageColumn.ImageLayout = DataGridViewImageCellLayout.Stretch
        End If
        cn.Close()
    End Sub

    Private Sub dgClientList_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgClientList.CellContentClick
        Dim colname As String = dgClientList.Columns(e.ColumnIndex).Name
        If colname = "colEdit" Then

            With frmClient
                OpenFormToAdminPanel(frmClient)
                .CenterPanelInForm(.Panel2)
                cn.Close()
                cn.Open()
                cm = New MySqlCommand("select image from tblclient where id = '" & dgClientList.Rows(e.RowIndex).Cells(1).Value.ToString & "'", cn)
                dr = cm.ExecuteReader
                While dr.Read
                    Dim len As Long = dr.GetBytes(0, 0, Nothing, 0, 0)
                    Dim array(CInt(len)) As Byte
                    dr.GetBytes(0, 0, array, 0, CInt(len))
                    Dim ms As New MemoryStream(array)
                    Dim bitmap As New System.Drawing.Bitmap(ms)
                    .ClientImage.BackgroundImage = bitmap
                    .txt_ID.Text = dgClientList.Rows(e.RowIndex).Cells(1).Value.ToString
                End While
                dr.Dispose()
                cn.Close()
                .btnUpdate.Enabled = True
                .btnSave.Enabled = False

                cn.Close()
                cn.Open()
                cm = New MySqlCommand("select * from tblclient where id = @1", cn)
                With cm
                    .Parameters.AddWithValue("@1", dgClientList.Rows(e.RowIndex).Cells(1).Value.ToString)
                End With
                dr = cm.ExecuteReader
                dr.Read()
                If dr.HasRows Then
                    .txtFname.Text = dr.Item("fname").ToString
                    .txtMname.Text = dr.Item("mname").ToString
                    .txtLname.Text = dr.Item("lname").ToString
                    .txtContact.Text = dr.Item("contact").ToString
                    .txtAddress.Text = dr.Item("address").ToString
                    .txtEmail.Text = dr.Item("email").ToString
                    .txtFBAccount.Text = dr.Item("fbaccount").ToString
                    .dtBirthdate.Value = dr.Item("birthdate")

                    .sln.Text = dr.Item("spouse_lname").ToString
                    .sfn.Text = dr.Item("spouse_fname").ToString
                    .smn.Text = dr.Item("spouse_mname").ToString
                    .refcontact1.Text = dr.Item("reference1").ToString
                    .refcontact2.Text = dr.Item("reference2").ToString
                    .refcontact3.Text = dr.Item("reference3").ToString
                    .txtDriver.Text = dr.Item("drivers_lic_no").ToString
                    .txtVoters.Text = dr.Item("voters").ToString
                    .txtSSS.Text = dr.Item("sss_no").ToString
                    .txtGSIS.Text = dr.Item("gsis_no").ToString
                    .txtEmployer.Text = dr.Item("employer").ToString
                    .txtOccupation.Text = dr.Item("occupation").ToString
                    .txtIncome.Text = dr.Item("monthly_income").ToString

                    .cbStatus.Text = dr.Item("status").ToString
                    .txtTIN.Text = dr.Item("tin_no").ToString
                Else
                End If
                dr.Close()
                cn.Close()
            End With
        ElseIf colname = "colDelete" Then
            'cn.Open()
            'cm = New MySqlCommand("select image from tblproduct where id = '" & dgProductList.Rows(e.RowIndex).Cells(1).Value.ToString & "'", cn)
            'cm.ExecuteNonQuery()
            'cn.Close()
            'AuditTrail("Deleted a product; description " & dgProductList.Rows(e.RowIndex).Cells(1).Value.ToString & "; category " & dgProductList.Rows(e.RowIndex).Cells(4).Value.ToString & "; weight " & dgProductList.Rows(e.RowIndex).Cells(5).Value.ToString & "; price " & dgProductList.Rows(e.RowIndex).Cells(3).Value.ToString & " where id " & dgProductList.Rows(e.RowIndex).Cells(1).Value.ToString & "")
            'MsgBox("Product successfully deleted!", vbInformation)
            'DashBoard()

            MsgBox("Unable to delete Client!", vbCritical)
        End If
    End Sub

    Private Sub frmProductList_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.KeyPreview = True
    End Sub

    Private Sub frmProductList_KeyDown(sender As Object, e As KeyEventArgs) Handles Me.KeyDown
        If e.KeyCode = Keys.Escape Then
            ToolStripButton2_Click(sender, e)
        End If
    End Sub

    Private Sub ToolStripTextBox1_TextChanged(sender As Object, e As EventArgs) Handles ToolStripTextBox1.TextChanged
        loadClientRecords()
    End Sub

    Private Sub dgClientList_CellMouseEnter(sender As Object, e As DataGridViewCellEventArgs) Handles dgClientList.CellMouseEnter
        If e.ColumnIndex = 7 Then
            dgClientList.Cursor = Cursors.Hand
        Else
            dgClientList.Cursor = Cursors.Default
        End If
    End Sub

    Private Sub dgClientList_CellMouseLeave(sender As Object, e As DataGridViewCellEventArgs) Handles dgClientList.CellMouseLeave
        dgClientList.Cursor = Cursors.Default
    End Sub
End Class