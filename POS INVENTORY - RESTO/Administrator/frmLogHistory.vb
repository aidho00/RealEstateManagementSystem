Imports MySql.Data.MySqlClient

Public Class frmLogHistory
    Private Sub ToolStripButton2_Click(sender As Object, e As EventArgs) Handles ToolStripButton2.Click
        Me.Dispose()
    End Sub

    Private Sub frmLogHistory_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.KeyPreview = True
    End Sub

    Private Sub frmLogHistory_KeyDown(sender As Object, e As KeyEventArgs) Handles Me.KeyDown
        If e.KeyCode = Keys.Escape Then
            ToolStripButton2_Click(sender, e)
        End If
    End Sub

    Sub loadSubdivisions()
        Try
            dgSubdivisiontList.Rows.Clear()
            Dim i As Integer
            cn.Open()
            cm = New MySqlCommand("SELECT id, name, `default_term_months`, `amount_per_sqm`, `amount_corner_lot_additional`, amount_corner_lot_additional, status from tblpropertytype where name like '%" & ToolStripTextBox1.Text & "%'", cn)
            dr = cm.ExecuteReader
            While dr.Read
                i += 1
                dgSubdivisiontList.Rows.Add(i, dr.Item("id").ToString, dr.Item("name").ToString, dr.Item("default_term_months").ToString, dr.Item("amount_per_sqm").ToString, dr.Item("amount_corner_lot_additional").ToString, dr.Item("status").ToString)
            End While
            dr.Close()
            cn.Close()
        Catch ex As Exception
            MsgBox(ex.Message, vbCritical)
            cn.Close()
        End Try
    End Sub

    Private Sub ToolStripTextBox1_TextChanged(sender As Object, e As EventArgs) Handles ToolStripTextBox1.TextChanged
        loadSubdivisions()
    End Sub

    Private Sub ToolStripButton1_Click(sender As Object, e As EventArgs) Handles ToolStripButton1.Click
        With frmCategory
            .btnSave.Enabled = True
            .btnUpdate.Enabled = False
            .ShowDialog()
        End With
    End Sub

    Private Sub dgSubdivisiontList_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgSubdivisiontList.CellContentClick
        Dim colname As String = dgSubdivisiontList.Columns(e.ColumnIndex).Name
        If colname = "colEdit" Then

            With frmCategory
                .btnUpdate.Enabled = True
                .btnSave.Enabled = False

                cn.Close()
                cn.Open()
                cm = New MySqlCommand("select * from tblpropertytype where id = @1", cn)
                With cm
                    .Parameters.AddWithValue("@1", dgSubdivisiontList.Rows(e.RowIndex).Cells(1).Value.ToString)
                End With
                dr = cm.ExecuteReader
                dr.Read()
                If dr.HasRows Then
                    .subID.Text = dr.Item("id").ToString
                    .txtType.Text = dr.Item("name").ToString
                    .cbStatus.Text = dr.Item("status").ToString
                    .txtAmount.Text = dr.Item("amount_per_sqm").ToString
                    .txtTerm.Text = dr.Item("default_term_months").ToString
                    .txtAmountAdditional.Text = dr.Item("amount_corner_lot_additional").ToString
                Else
                End If
                dr.Close()
                cn.Close()

                .ShowDialog()
            End With
        ElseIf colname = "colDelete" Then
            'cn.Open()
            'cm = New MySqlCommand("select image from tblproduct where id = '" & dgProductList.Rows(e.RowIndex).Cells(1).Value.ToString & "'", cn)
            'cm.ExecuteNonQuery()
            'cn.Close()
            'AuditTrail("Deleted a product; description " & dgProductList.Rows(e.RowIndex).Cells(1).Value.ToString & "; category " & dgProductList.Rows(e.RowIndex).Cells(4).Value.ToString & "; weight " & dgProductList.Rows(e.RowIndex).Cells(5).Value.ToString & "; price " & dgProductList.Rows(e.RowIndex).Cells(3).Value.ToString & " where id " & dgProductList.Rows(e.RowIndex).Cells(1).Value.ToString & "")
            'MsgBox("Product successfully deleted!", vbInformation)
            'DashBoard()

            MsgBox("Unable to delete Subdivision!", vbCritical)
        End If
    End Sub

    Private Sub ToolStripButton3_Click(sender As Object, e As EventArgs) Handles ToolStripButton3.Click
        With frmBestSelling
            .loadRecords()
            OpenFormToAdminPanel(frmBestSelling)
        End With
    End Sub
End Class