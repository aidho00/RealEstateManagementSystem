Imports MySql.Data.MySqlClient

Public Class frmComission


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

    Private Sub frmComission_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        cbPropertyType.SelectedIndex = 0
        cbStatus.SelectedIndex = 0
        loadCommissions()
    End Sub


    Sub loadCommissions()
        Try
            dgCom.Rows.Clear()
            Dim i As Integer
            cn.Close()
            cn.Open()
            cm = New MySqlCommand("SELECT id, property_type, commission_percentage, commision_months,  status from tblcomission where property_type like '%" & txtSearch.Text & "%'", cn)
            dr = cm.ExecuteReader
            While dr.Read
                i += 1
                dgCom.Rows.Add(i, dr.Item("id").ToString, dr.Item("property_type").ToString, dr.Item("commission_percentage").ToString, dr.Item("commision_months").ToString, dr.Item("status").ToString)
            End While
            dr.Close()
            cn.Close()
        Catch ex As Exception
            MsgBox(ex.Message, vbCritical)
            cn.Close()
        End Try
    End Sub


    Private Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click

        If String.IsNullOrEmpty(txtPercent.Text) Then
            MsgBox("Percentage field cannot be empty!", vbCritical)
            Return
        ElseIf String.IsNullOrEmpty(txtMonths.Text) Then
            MsgBox("Percentage field cannot be empty!", vbCritical)
            Return
        ElseIf CInt(txtMonths.Text) < 1 Then
            MsgBox("Month(s) field cannot be zero!", vbCritical)
            Return
        End If

        If MsgBox("Save this Commission?", vbYesNo + vbQuestion) = vbYes Then
            cn.Close()
            cn.Open()
            cm = New MySqlCommand("Select commission_percentage from tblcomission where property_type = '" & cbPropertyType.Text & "' and commission_percentage = '" & txtPercent.Text & "' and commision_months = '" & txtMonths.Text & "'", cn)
            dr = cm.ExecuteReader
            If dr.HasRows Then
                MsgBox("Duplicate Entry Found!", vbExclamation)
                dr.Close()
                cn.Close()
            Else
                dr.Close()
                cn.Close()
                cn.Open()
                cm = New MySqlCommand("Insert into tblcomission (`property_type`, `commission_percentage`, `commision_months`, `commision_date`, `status`) values (@1,@2,@3,NOW(),@4)", cn)
                With cm
                    .Parameters.AddWithValue("@1", cbPropertyType.Text)
                    .Parameters.AddWithValue("@2", txtPercent.Text)
                    .Parameters.AddWithValue("@2", txtMonths.Text)
                    .Parameters.AddWithValue("@4", cbStatus.Text)
                End With
                result = cm.ExecuteNonQuery
                If result = 0 Then
                    MsgBox("Commission has failed to save!", vbCritical)
                    cm.Dispose()
                    cn.Close()
                Else
                    MsgBox("Commission has been successfully saved!", vbInformation)
                    cm.Dispose()
                    cn.Close()
                    AuditTrail("Added a Commission for property type " & cbPropertyType.Text & " with a percentage of " & txtPercent.Text & "%.")
                    loadCommissions()
                End If
                txtPercent.Clear()
                txtPercent.Focus()
            End If
        End If

    End Sub

    Private Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
        comID.Text = "0"
        cbPropertyType.SelectedIndex = 0
        txtPercent.Text = "0"
        cbStatus.SelectedIndex = 0
        btnSave.Enabled = True
        btnUpdate.Enabled = False
    End Sub

    Private Sub btnUpdate_Click(sender As Object, e As EventArgs) Handles btnUpdate.Click

        If String.IsNullOrEmpty(txtPercent.Text) Then
            MsgBox("Percentage field cannot be empty!", vbCritical)
            Return
        ElseIf String.IsNullOrEmpty(txtMonths.Text) Then
            MsgBox("Percentage field cannot be empty!", vbCritical)
            Return
        ElseIf CInt(txtMonths.Text) < 1 Then
            MsgBox("Month(s) field cannot be zero!", vbCritical)
            Return
        End If
        If MsgBox("Update this commission?", vbYesNo + vbQuestion) = vbYes Then
            cn.Close()
            cn.Open()
            cm = New MySqlCommand("Select commission_percentage from tblcomission where property_type = '" & cbPropertyType.Text & "' and id not in(" & CInt(comID.Text) & ")", cn)
            dr = cm.ExecuteReader
            If dr.HasRows Then
                MsgBox("Duplicate Entry Found!", vbExclamation)
                dr.Close()
                cn.Close()
            Else
                dr.Close()
                cn.Close()
                cn.Open()
                cm = New MySqlCommand("update comission set `property_type`=@1,`commission_percentage`=@2,`commision_months`=@3,`status`=@4 WHERE id = @0", cn)
                With cm
                    .Parameters.AddWithValue("@0", comID.Text)
                    .Parameters.AddWithValue("@1", cbPropertyType.Text)
                    .Parameters.AddWithValue("@2", txtPercent.Text)
                    .Parameters.AddWithValue("@2", txtMonths.Text)
                    .Parameters.AddWithValue("@4", cbStatus.Text)
                End With
                result = cm.ExecuteNonQuery
                If result = 0 Then
                    MsgBox("Commission has failed to update!", vbCritical)
                    cm.Dispose()
                    cn.Close()
                Else
                    MsgBox("Commission has been successfully updated!", vbInformation)
                    cm.Dispose()
                    cn.Close()
                    AuditTrail("Updated a Commission for property type " & cbPropertyType.Text & " with a percentage of " & txtPercent.Text & "%.")
                    loadCommissions()
                End If
                txtPercent.Clear()
                txtPercent.Focus()
            End If
        End If

    End Sub

    Private Sub cbPropertyType_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cbPropertyType.SelectedIndexChanged

    End Sub

    Private Sub cbPropertyType_KeyPress(sender As Object, e As KeyPressEventArgs) Handles cbPropertyType.KeyPress, cbStatus.KeyPress
        e.Handled = True
    End Sub

    Private Sub dgCom_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgCom.CellContentClick

    End Sub

    Private Sub dgCom_CellContentDoubleClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgCom.CellContentDoubleClick
        Try
            comID.Text = dgCom.CurrentRow.Cells(1).Value
            cbPropertyType.Text = dgCom.CurrentRow.Cells(2).Value
            txtPercent.Text = dgCom.CurrentRow.Cells(3).Value
            cbStatus.Text = dgCom.CurrentRow.Cells(4).Value

            btnSave.Enabled = False
            btnUpdate.Enabled = True
        Catch ex As Exception

        End Try

    End Sub

    Private Sub txtPercent_TextChanged(sender As Object, e As EventArgs) Handles txtPercent.TextChanged, txtMonths.TextChanged

    End Sub

    Private Sub txtPercent_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtPercent.KeyPress, txtMonths.KeyPress
        If Not Char.IsDigit(e.KeyChar) AndAlso e.KeyChar <> "." AndAlso e.KeyChar <> ControlChars.Back Then
            e.Handled = True ' Ignore the key press event
        End If
    End Sub

    Private Sub ToolStripButton2_Click(sender As Object, e As EventArgs) Handles ToolStripButton2.Click
        Me.Dispose()
    End Sub
End Class