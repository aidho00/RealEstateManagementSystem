Public Class frmPrintPreview
    Private Sub ToolStripButton2_Click(sender As Object, e As EventArgs) Handles ToolStripButton2.Click
        Me.Close()
    End Sub


    Private Sub frmPrintPreview_KeyDown(sender As Object, e As KeyEventArgs) Handles Me.KeyDown
        If e.KeyCode = Keys.Escape Then
            ToolStripButton2_Click(sender, e)
        End If
    End Sub

    Private Sub frmPrintPreview_Load(sender As Object, e As KeyEventArgs) Handles Me.Load
        Me.KeyPreview = True
    End Sub
End Class