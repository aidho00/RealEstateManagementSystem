Imports MySql.Data.MySqlClient

Public Class frmAuditTrail
    Private Sub ToolStripButton2_Click(sender As Object, e As EventArgs) Handles ToolStripButton2.Click
        Me.Dispose()
    End Sub

    Private Sub frmAuditTrail_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.KeyPreview = True

        dtFrom.Value = Now
        dtTo.Value = Now
    End Sub

    Private Sub frmAuditTrail_KeyDown(sender As Object, e As KeyEventArgs) Handles Me.KeyDown
        If e.KeyCode = Keys.Escape Then
            ToolStripButton2_Click(sender, e)
        End If
    End Sub

    Private Sub dtFrom_ValueChanged(sender As Object, e As EventArgs) Handles dtFrom.ValueChanged, dtTo.ValueChanged
        loadAuditTrail()
    End Sub

    Sub loadAuditTrail()
        Try
            Dim sdate1 As String = dtFrom.Value.ToString("yyyy-MM-dd")
            Dim sdate2 As String = dtTo.Value.ToString("yyyy-MM-dd")
            dgAuditTrail.Rows.Clear()
            Dim i As Integer
            cn.Open()
            cm = New MySqlCommand("SELECT a.id, u.name, a.summary, DATE_FORMAT(a.sdate,'%Y-%m-%d') as sdate, time_format(a.stime, '%h:%i %p') as stime from tblaudit a inner join tbluser u on a.user = u.username where sdate between '" & sdate1 & "' and '" & sdate2 & "'", cn)
            dr = cm.ExecuteReader
            While dr.Read
                i += 1
                dgAuditTrail.Rows.Add(dr.Item("id").ToString, dr.Item("name").ToString, dr.Item("summary").ToString, dr.Item("sdate").ToString, dr.Item("stime").ToString)
                lblCount.Text = "Record Count: " & i & ""
            End While
            dr.Close()
            cn.Close()
        Catch ex As Exception
            MsgBox(ex.Message, vbCritical)
            cn.Close()
        End Try
    End Sub
End Class