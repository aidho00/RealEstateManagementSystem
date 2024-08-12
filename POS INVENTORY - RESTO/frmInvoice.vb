Imports MySql.Data.MySqlClient
Imports CrystalDecisions.CrystalReports.Engine

Public Class frmInvoice

    Dim crReportDocument As ReportDocument

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

    Private Sub frmInvoice_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.KeyPreview = True
    End Sub

    Private Sub frmInvoice_KeyDown(sender As Object, e As KeyEventArgs) Handles Me.KeyDown
        If e.KeyCode = Keys.Escape Then
            Me.Dispose()
        End If
    End Sub

    Private Sub btnPrint_Click(sender As Object, e As EventArgs) Handles btnPrint.Click
        Dim PrinterName As String = PrintDocument1.PrinterSettings.PrinterName
        crReportDocument = ReportViewer.ReportSource
        Dim nCopy As Integer = PrintDocument1.PrinterSettings.Copies
        Dim sPage As Integer = PrintDocument1.PrinterSettings.FromPage
        Dim ePage As Integer = PrintDocument1.PrinterSettings.ToPage
        Try
            crReportDocument.PrintOptions.PrinterName = PrinterName
            crReportDocument.PrintToPrinter(nCopy, True, sPage, ePage)
        Catch ex As Exception
        End Try
        ReportViewer.ReportSource = Nothing
    End Sub
End Class