Imports MySql.Data.MySqlClient

Public Class frmUserList
    Private Sub ToolStripButton1_Click(sender As Object, e As EventArgs) Handles ToolStripButton1.Click
        With frmUserRegistration
            .cbRole.Text = ""
            .btnSave.Enabled = True
            .btnUpdate.Enabled = False
            .txtName.Focus()
            .ShowDialog()
        End With
    End Sub

    Private Sub ToolStripButton2_Click(sender As Object, e As EventArgs) Handles ToolStripButton2.Click
        Me.Dispose()
    End Sub

    Sub loadUserRecords()
        dgUserList.Rows.Clear()
        Dim i As Integer
        cn.Open()
        cm = New MySqlCommand("select * from tbluser", cn)
        dr = cm.ExecuteReader
        While dr.Read
            i += 1
            dgUserList.Rows.Add(i, dr.Item("username").ToString, dr.Item("password").ToString, dr.Item("name").ToString, dr.Item("role").ToString, dr.Item("status").ToString)
        End While
        dr.Close()

        'For i = 0 To dgUserList.Rows.Count - 1
        '    Dim r As DataGridViewRow = dgUserList.Rows(i)
        '    r.Height = 100
        'Next
        cn.Close()
    End Sub

    Private Sub dgProductList_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgUserList.CellContentClick
        Dim colname As String = dgUserList.Columns(e.ColumnIndex).Name
        If colname = "colEdit" Then

            With frmUserRegistration
                .txtUsername.Text = dgUserList.Rows(e.RowIndex).Cells(1).Value.ToString
                '.txtRePassword.Text = dgUserList.Rows(e.RowIndex).Cells(2).Value.ToString
                .txtName.Text = dgUserList.Rows(e.RowIndex).Cells(3).Value.ToString
                .cbRole.Text = dgUserList.Rows(e.RowIndex).Cells(4).Value.ToString
                .cbStatus.Text = dgUserList.Rows(e.RowIndex).Cells(5).Value.ToString
                .txtUsername.ReadOnly = True
                .CheckBox1.Visible = True
                .btnUpdate.Enabled = True
                .btnSave.Enabled = False
                .ShowDialog()
            End With
        ElseIf colname = "colDelete" Then
            'cn.Open()
            'cm = New MySqlCommand("select username from tbluser where username = '" & dgUserList.Rows(e.RowIndex).Cells(1).Value.ToString & "'", cn)
            'cm.ExecuteNonQuery()
            'cn.Close()
            'MsgBox("User successfully deleted!", vbInformation)
            MsgBox("Unable to delete user!", vbInformation)
        End If
    End Sub

    Private Sub frmUserList_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.KeyPreview = True
    End Sub

    Private Sub frmUserList_KeyDown(sender As Object, e As KeyEventArgs) Handles Me.KeyDown
        If e.KeyCode = Keys.Escape Then
            ToolStripButton2_Click(sender, e)
        End If
    End Sub
End Class