Imports MySql.Data.MySqlClient

Public Class frmCategory


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

        If String.IsNullOrEmpty(txtType.Text) Then
            MsgBox("Type field cannot be empty!", vbCritical)
            Return
        End If
        If MsgBox("Save this subdivision name?", vbYesNo + vbQuestion) = vbYes Then
            cn.Open()
            cm = New MySqlCommand("Select name from tblpropertytype where name = '" & txtType.Text & "'", cn)
            dr = cm.ExecuteReader
            If dr.HasRows Then
                MsgBox("Duplicate Entry Found!", vbExclamation)
                dr.Close()
                cn.Close()
            Else
                dr.Close()
                cm = New MySqlCommand("Insert into tblpropertytype (name, status, default_term_months, amount_per_sqm, amount_corner_lot_additional) values (@1, @2, @3, @4, @5)", cn)
                With cm
                    .Parameters.AddWithValue("@1", txtType.Text)
                    .Parameters.AddWithValue("@2", cbStatus.Text)
                    .Parameters.AddWithValue("@3", CDec(txtTerm.Text))
                    .Parameters.AddWithValue("@4", CDec(txtAmount.Text))
                    .Parameters.AddWithValue("@5", CDec(txtAmountAdditional.Text))
                End With
                result = cm.ExecuteNonQuery
                If result = 0 Then
                    MsgBox("Subdivision name has failed to save!", vbCritical)
                    cm.Dispose()
                    cn.Close()
                Else
                    MsgBox("Subdivision name has been successfully saved!", vbInformation)
                    cm.Dispose()
                    cn.Close()
                    AuditTrail("Added a subdivision name " & txtType.Text & ".")
                End If
                txtType.Clear()
                txtType.Focus()
                frmLogHistory.loadSubdivisions()
                frmProduct.loadCategory()
                Me.Dispose()
            End If
        End If

    End Sub

    Private Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
        Me.Dispose()
    End Sub

    Private Sub btnUpdate_Click(sender As Object, e As EventArgs) Handles btnUpdate.Click

        If String.IsNullOrEmpty(txtType.Text) Then
            MsgBox("Type field cannot be empty!", vbCritical)
            Return
        End If
        If MsgBox("Update this subdivision name?", vbYesNo + vbQuestion) = vbYes Then
            cn.Open()
            cm = New MySqlCommand("Select name from tblpropertytype where name = '" & txtType.Text & "' and id not in(" & CInt(subID.Text) & ")", cn)
            dr = cm.ExecuteReader
            If dr.HasRows Then
                MsgBox("Duplicate Entry Found!", vbExclamation)
                dr.Close()
                cn.Close()
            Else
                dr.Close()
                cm = New MySqlCommand("update tblpropertytype set name= @1, status = @2, default_term_months = @3, amount_per_sqm = @4, amount_corner_lot_additional = @5 where id = @0", cn)
                With cm
                    .Parameters.AddWithValue("@0", CInt(subID.Text))
                    .Parameters.AddWithValue("@1", txtType.Text)
                    .Parameters.AddWithValue("@2", cbStatus.Text)
                    .Parameters.AddWithValue("@3", CDec(txtTerm.Text))
                    .Parameters.AddWithValue("@4", CDec(txtAmount.Text))
                    .Parameters.AddWithValue("@5", CDec(txtAmountAdditional.Text))
                End With
                result = cm.ExecuteNonQuery
                If result = 0 Then
                    MsgBox("Subdivision name has failed to update!", vbCritical)
                    cm.Dispose()
                    cn.Close()
                Else
                    MsgBox("Subdivision name has been successfully updated!", vbInformation)
                    cm.Dispose()
                    cn.Close()
                    AuditTrail("Updated a subdivision name " & frmLogHistory.dgSubdivisiontList.CurrentRow.Cells(2).Value & " to " & txtType.Text & ".")
                End If
                txtType.Clear()
                txtType.Focus()
                With frmProduct
                    .loadCategory()
                    .cbSubdivision.Text = txtType.Text
                End With
                frmLogHistory.loadSubdivisions()
                Me.Dispose()
            End If
        End If

    End Sub

    Private Sub frmCategory_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        cbStatus.SelectedIndex = 0
    End Sub

    Private Sub txtAmount_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtAmount.KeyPress, txtTerm.KeyPress, txtAmountAdditional.KeyPress
        If Not Char.IsDigit(e.KeyChar) AndAlso e.KeyChar <> ControlChars.Back Then
            e.Handled = True ' Ignore the key press event
        End If
    End Sub
End Class