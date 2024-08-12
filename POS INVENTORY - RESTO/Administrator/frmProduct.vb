Imports MySql.Data.MySqlClient
Imports System.IO
Imports System.Globalization

Public Class frmProduct
    Dim culture As New CultureInfo("en-US")
    Dim dtt As New DataTable
    Dim trans_no As String

    Dim Num, n As Integer

    Dim subAmountPerSQM As Decimal
    Dim subAdditionalCornerLot As Decimal
    Dim subDefaultTerm As Decimal

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


    Private Sub frmProduct_KeyDown(sender As Object, e As KeyEventArgs) Handles Me.KeyDown
        If e.KeyCode = Keys.Escape Then
            ToolStripButton2_Click(sender, e)
        End If
    End Sub

    Private Sub ToolStripButton2_Click(sender As Object, e As EventArgs) Handles ToolStripButton2.Click
        If MsgBox("Are you sure you want to close this form?", vbYesNo + vbQuestion) = vbYes Then
            Me.Dispose()
            frmProductList.Enabled = True
            frmPOSEncoding.txtclearvalues()
            frmPOSEncoding.Dispose()
        Else

        End If
    End Sub

    Private Sub btnCategory_Click(sender As Object, e As EventArgs) Handles btnCategory.Click
        With frmCategory
            .btnUpdate.Enabled = True
            .btnUpdate.Enabled = False
            .ShowDialog()
        End With

    End Sub

    Sub loadCategory()
        fillCombo("Select name, id from tblpropertytype", cbSubdivision, "tblpropertytype", "name", "id")

        fillCombo("Select CONCAT(lname, ', ',fname,' ',mname) as Agent, id from tblagent", cbAgent, "tblagent", "Agent", "id")

        fillCombo("Select CONCAT(lname, ', ',fname,' ',mname) as Client, id from tblclient", cbClient, "tblclient", "Client", "id")

        Try
            agent_id.Text = CInt(cbAgent.SelectedValue)
        Catch ex As Exception
        End Try

        Try
            client_id.Text = CInt(cbClient.SelectedValue)
        Catch ex As Exception
        End Try

        txtPrice.Text = "0.00"
        txtAmort.Text = "0.00"
        txtComAmount.Text = "0.00"
        txtLength.Text = "0"
        txtLength2.Text = "0"
        'txtInterestRate.Text = "0"
        txtPrice.Focus()
        txtPrice.SelectionStart = txtPrice.TextLength
        cbStatus.SelectedIndex = 0

        cbAgent.Text = ""
        cbClient.Text = ""
        SendKeys.Send("+{HOME}")
    End Sub


    Private Sub AutoCodeNumber()
        Dim s As String = frmLogin.txtbox_date.Text
        Dim t As String = s.Substring(s.Length - 2, 1)
        Dim v As String = s.Substring(s.Length - 3, 1)
        Dim u As String = s.Substring(s.Length - 4, 1)
        yearid.Text = u & v & t & s.Substring(s.Length - 1, 1)

        cm = New MySqlCommand("SELECT id FROM tblproperty WHERE id like '" & yearid.Text & "%'", cn)
        Dim sdr As MySqlDataReader = cm.ExecuteReader()
        If (sdr.Read() = True) Then
            sdr.Dispose()
            load_data("SELECT MAX(id) as Code from tblproperty", "tblproperty")
            code_last_no.Text = ds.Tables("tblproperty").Rows(0)(0).ToString
            ds = New DataSet
            Dim yr As String = yearid.Text
            Dim str As String = code_last_no.Text
            str = str.Remove(0, 4)
            Dim n As String
            Dim m As Long
            Dim r As Long
            n = yr + str
            m = n
            r = m + 1
            txtID.Text = r
        Else
            sdr.Dispose()
            txtID.Text = yearid.Text + "00001"
        End If
        CloseDBConnection()
    End Sub

    Function GetTransno() As String
        cn.Close()
        cn.Open()
        Try

            Dim sdate As String = Format(Convert.ToDateTime(frmLogin.txtbox_date.Text), "yyyyMMdd")
            cm = New MySqlCommand("select * from tblpayment where transaction_no like '" & sdate & "%' order by id desc", cn)
            dr = cm.ExecuteReader
            dr.Read()
            If dr.HasRows Then
                GetTransno = CDec(dr.Item("transaction_no").ToString) + 1
            Else
                GetTransno = sdate & "0001"
            End If
            dr.Close()
            Return GetTransno

        Catch ex As Exception
            MsgBox(ex.Message, vbCritical)
        End Try
        cn.Close()

    End Function

    Private Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        'Try
        'If propertyImage.BackgroundImage Is Nothing Then
        '    MsgBox("Please select property Thumbnail!", vbCritical)
        '    Return
        'End If
        If String.IsNullOrEmpty(txtBlock.Text) Or String.IsNullOrEmpty(txtlot.Text) Or String.IsNullOrEmpty(txtSquaremeter.Text) Then
            MsgBox("Property block, lot and square meter field cannot be empty!", vbCritical)
            Return
        ElseIf String.IsNullOrEmpty(cbSubdivision.Text) Then
            MsgBox("Subdivision cannot be empty!", vbCritical)
            Return
        ElseIf String.IsNullOrEmpty(cbStatus.Text) Then
            MsgBox("Status field cannot be empty!", vbCritical)
            Return
        ElseIf CDec(txtPrice.Text) = 0 Then
            MsgBox("Price field cannot be zero!", vbCritical)
            Return
        ElseIf agent_id.Text = "0" Then
            MsgBox("Please select a valid Agent!", vbCritical)
            Return
        ElseIf client_id.Text = "0" Then
            MsgBox("Please select a valid Client!", vbCritical)
            Return
        ElseIf sub_id.Text = "0" Then
            MsgBox("Please select a valid Subdivision!", vbCritical)
            Return
        ElseIf cbPropertyType.Text = "" Then
            MsgBox("Please select the type of property!", vbCritical)
            Return
        End If

        If CDec(txtDownPayment.Text) = 0 Then

            SaveProperty()
        Else
            With frmPOSEncoding
                .txtbox_amountpaid.Text = Format(CDec(txtDownPayment.Text), "#,##0.00")
                .property_id.Text = txtID.Text
                .btnSettle.Text = "SAVE"
                .CheckBox1.Visible = False
                .txtbox_amountreceived.Focus()
                .ShowDialog()
            End With
        End If
        'Catch ex As Exception
        '    cn.Close()
        '    MsgBox(ex.Message, vbCritical)
        'End Try
        cn.Close()
    End Sub

    Sub SaveProperty()
        If MsgBox("Save this Client Property?", vbYesNo + vbQuestion) = vbYes Then
            btnAmort.PerformClick()
            cn.Close()
            cn.Open()
            Dim mstream As New MemoryStream
            propertyImage.BackgroundImage.Save(mstream, System.Drawing.Imaging.ImageFormat.Jpeg)
            Dim arrImage() As Byte = mstream.GetBuffer
            cm = New MySqlCommand("Select * from tblproperty where block = '" & txtBlock.Text & "' and lot = '" & txtlot.Text & "' and square_meter = '" & txtSquaremeter.Text & "' and type_id = " & cbSubdivision.SelectedValue & "", cn)
            dr = cm.ExecuteReader
            If dr.HasRows Then
                MsgBox("Duplicate Property Entry Found! Property with block '" & txtBlock.Text & "', lot '" & txtlot.Text & "', square_meter = '" & txtSquaremeter.Text & "' in area " & cbSubdivision.SelectedValue & " already exist.", vbCritical)
                dr.Close()
                cn.Close()
            Else
                dr.Close()

                btnAmort.PerformClick()
                Dim property_transaction As MySqlTransaction = cn.BeginTransaction()
                Try
                    AutoCodeNumber()
                    cn.Open()
                    Using property_transaction_Cmd As MySqlCommand = cn.CreateCommand()
                        property_transaction_Cmd.Transaction = property_transaction
                        property_transaction_Cmd.CommandText = "Insert into tblproperty (`block`,`lot`, `square_meter`, `type_id`, `price`, `status`, `agent_id`, `thumbnail`, `client_id`, `monthly_amort`, `start_contract`, `end_contract`, `length_years`, `interest_rate`, `id`, `property_date`, `payment_option`, `property_type`, `commission`, `down_payment`, `penalty`, `is_corner_lot`, `additionalAmount`) values (@0,@1, @2, @3, @4, @5, @6, @7,@8, @9, @10, @11, @12, @13, @14, @15, @16, @17, @18, @19, @20, @21, @22)"
                        property_transaction_Cmd.Parameters.AddWithValue("@0", txtBlock.Text)
                        property_transaction_Cmd.Parameters.AddWithValue("@1", txtlot.Text)
                        property_transaction_Cmd.Parameters.AddWithValue("@2", txtSquaremeter.Text)
                        property_transaction_Cmd.Parameters.AddWithValue("@3", cbSubdivision.SelectedValue)
                        property_transaction_Cmd.Parameters.AddWithValue("@4", CDec(txtPrice.Text))
                        property_transaction_Cmd.Parameters.AddWithValue("@5", cbStatus.Text)
                        property_transaction_Cmd.Parameters.AddWithValue("@6", agent_id.Text)
                        property_transaction_Cmd.Parameters.AddWithValue("@7", arrImage)
                        property_transaction_Cmd.Parameters.AddWithValue("@8", client_id.Text)
                        property_transaction_Cmd.Parameters.AddWithValue("@9", CDec(txtAmort.Text))
                        property_transaction_Cmd.Parameters.AddWithValue("@10", Format(Convert.ToDateTime(txtStart.Text), "yyyy-MM-dd"))
                        property_transaction_Cmd.Parameters.AddWithValue("@11", Format(Convert.ToDateTime(txtEnd.Text), "yyyy-MM-dd"))
                        property_transaction_Cmd.Parameters.AddWithValue("@12", CDec(txtLength.Text))
                        property_transaction_Cmd.Parameters.AddWithValue("@13", CDec(txtRate.Text))
                        property_transaction_Cmd.Parameters.AddWithValue("@14", txtID.Text)
                        property_transaction_Cmd.Parameters.AddWithValue("@15", Format(Convert.ToDateTime(dtToday.Text), "yyyy-MM-dd"))
                        If cbOption.Text = "Cash" Then
                            property_transaction_Cmd.Parameters.AddWithValue("@16", "Cash")
                        Else
                            property_transaction_Cmd.Parameters.AddWithValue("@16", "Installment")
                        End If
                        property_transaction_Cmd.Parameters.AddWithValue("@17", cbPropertyType.Text)
                        property_transaction_Cmd.Parameters.AddWithValue("@18", commission_id.Text)
                        property_transaction_Cmd.Parameters.AddWithValue("@19", CDec(txtDownPayment.Text))
                        property_transaction_Cmd.Parameters.AddWithValue("@20", cbPenalty.Text)

                        If cbCorner.Checked = True Then
                            property_transaction_Cmd.Parameters.AddWithValue("@21", 1)
                        Else
                            property_transaction_Cmd.Parameters.AddWithValue("@21", 0)
                        End If

                        property_transaction_Cmd.Parameters.AddWithValue("@22", CDec(txtAdditional.Text))
                        property_transaction_Cmd.ExecuteNonQuery()
                    End Using

                    For Each row As DataGridViewRow In dgvAmortizationSchedule.Rows
                        If row.Cells(0).Value = "0" Then
                        Else
                            Using property_payments_Cmd As MySqlCommand = cn.CreateCommand()
                                property_payments_Cmd.Transaction = property_transaction
                                property_payments_Cmd.CommandText = "Insert into tblinstallments (`property_id`, `payment_no`, `payment_date`, `amount`) values (@1, @2, @3, @4)"
                                property_payments_Cmd.Parameters.AddWithValue("@1", txtID.Text)
                                property_payments_Cmd.Parameters.AddWithValue("@2", row.Cells(0).Value)
                                property_payments_Cmd.Parameters.AddWithValue("@3", Format(Convert.ToDateTime(row.Cells(1).Value), "yyyy-MM-dd"))
                                property_payments_Cmd.Parameters.AddWithValue("@4", row.Cells(3).Value)
                                property_payments_Cmd.ExecuteNonQuery()
                            End Using
                        End If
                    Next


                    For Each row As DataGridViewRow In dgvCommissionSchedule.Rows
                        If row.Cells(0).Value = "0" Then
                        Else
                            Using commission_payments_Cmd As MySqlCommand = cn.CreateCommand()
                                commission_payments_Cmd.Transaction = property_transaction
                                commission_payments_Cmd.CommandText = "Insert into tblcommissionsched (`property_id`, `payment_no`, `payment_date`, `amount`) values (@1, @2, @3, @4)"
                                commission_payments_Cmd.Parameters.AddWithValue("@1", txtID.Text)
                                commission_payments_Cmd.Parameters.AddWithValue("@2", row.Cells(0).Value)
                                commission_payments_Cmd.Parameters.AddWithValue("@3", Format(Convert.ToDateTime(row.Cells(1).Value), "yyyy-MM-dd"))
                                commission_payments_Cmd.Parameters.AddWithValue("@4", row.Cells(3).Value)
                                commission_payments_Cmd.ExecuteNonQuery()
                            End Using
                        End If
                    Next

                    If CDec(txtDownPayment.Text) = 0 Then
                    Else
                        trans_no = GetTransno()
                        Using property_downpayment_Cmd As MySqlCommand = cn.CreateCommand()
                            property_downpayment_Cmd.Transaction = property_transaction
                            property_downpayment_Cmd.CommandText = "INSERT INTO `tblpayment`(`client_id`, `property_id`, `payment_no`, `amount`, `amount_received`, `date_paid`, `user`, `transaction_no`, `time_paid`, `payment_mode`, `cheque_bank`, `cheque_no`, remarks) VALUES (@1,@2,@3,@4,@5,@6,@7,@8,NOW(),@9,@10,@11,@12)"
                            property_downpayment_Cmd.Parameters.AddWithValue("@1", client_id.Text)
                            property_downpayment_Cmd.Parameters.AddWithValue("@2", txtID.Text)
                            property_downpayment_Cmd.Parameters.AddWithValue("@3", 1)
                            property_downpayment_Cmd.Parameters.AddWithValue("@4", CDec(frmPOSEncoding.txtbox_amountpaid.Text) + CDec(frmPOSEncoding.txtAdvance.Text))
                            property_downpayment_Cmd.Parameters.AddWithValue("@5", CDec(frmPOSEncoding.txtbox_amountreceived.Text))
                            property_downpayment_Cmd.Parameters.AddWithValue("@6", Format(Convert.ToDateTime(frmLogin.txtbox_date.Text), "yyyy-MM-dd"))
                            property_downpayment_Cmd.Parameters.AddWithValue("@7", str_user)
                            property_downpayment_Cmd.Parameters.AddWithValue("@8", trans_no)
                            property_downpayment_Cmd.Parameters.AddWithValue("@9", frmPOSEncoding.payment_method.Text)
                            property_downpayment_Cmd.Parameters.AddWithValue("@10", frmPOSEncoding.txtbox_cheque_no.Text)
                            property_downpayment_Cmd.Parameters.AddWithValue("@11", frmPOSEncoding.txtbox_cheque_bank.Text)
                            property_downpayment_Cmd.Parameters.AddWithValue("@12", "DOWN PAYMENT")
                            property_downpayment_Cmd.ExecuteNonQuery()
                        End Using
                    End If

                    property_transaction.Commit()

                    MsgBox("Property has been successfully saved!", vbInformation)
                    AuditTrail("Added a Property For client '" & cbClient.Text & "' with on Area: '" & cbSubdivision.Text & "'; Block '" & txtBlock.Text & "'; Lot '" & txtlot.Text & "'; Square Meter = '" & txtSquaremeter.Text & "'; Type '" & cbPropertyType.Text & "'; Price '" & Format(CDec(txtPrice.Text), "#,##0.00") & "'; Agent: '" & cbAgent.Text & "'; Length; '" & txtLength.Text & " Years & " & txtLength.Text & " Months'; Payment Option: '" & cbOption.Text & "'; Down Payment: '" & Format(CDec(txtDownPayment.Text), "#,##0.00") & "'; Penalty/Interest;" & cbPenalty.Text & ".")

                    PrintContract()

                    If CDec(txtDownPayment.Text) = 0 Then
                    Else
                        payment_receipt()
                    End If

                    Me.Close()
                    frmPOSEncoding.txtclearvalues()
                    frmPOSEncoding.Dispose()

                    With frmProductList
                        .loadPropertyRecords()
                    End With

                Catch ex As Exception
                    property_transaction.Rollback()
                    MessageBox.Show("Transaction failed. Transaction rolled back.", "", MessageBoxButtons.OK, MessageBoxIcon.Error)
                End Try

            End If
        End If
    End Sub



    Private Sub payment_receipt()

        Dim rptdoc As CrystalDecisions.CrystalReports.Engine.ReportDocument
        rptdoc = New reportReceipt2
        With rptdoc
            .SetDataSource(dt)
            .SetParameterValue("cashier_name", str_name)
            .SetParameterValue("client", cbClient.Text)
            .SetParameterValue("property", cbSubdivision.Text & " - " & txtBlock.Text & ", " & txtlot.Text)
            .SetParameterValue("method", frmPOSEncoding.payment_method.Text)
            '.SetParameterValue("currentbalance", current_balance)
            .SetParameterValue("amount", Format(CDec(frmPOSEncoding.txtbox_amountpaid.Text), "#,##0.00"))
            '.SetParameterValue("advance", Format(CDec(txtAdvance.Text), "#,##0.00"))
            '.SetParameterValue("balance", frmPOS.property_balance.Text)
            .SetParameterValue("transaction_no", trans_no)
            .SetParameterValue("p_date", Format(Convert.ToDateTime(frmLogin.txtbox_date.Text), "yyyy/MM/dd"))

            If str_company Is Nothing Then
                .SetParameterValue("company_name", "COMPANY")
            Else
                .SetParameterValue("company_name", str_company)
            End If
            If str_address Is Nothing Then
                .SetParameterValue("company_address", "ADDRESS")
            Else
                .SetParameterValue("company_address", str_address)
            End If
            If str_h1 Is Nothing Then
                .SetParameterValue("header1", "- - - - - - - - - - - - - - -")
            Else
                .SetParameterValue("header1", str_h1)
            End If
        End With
        frmInvoice.ReportViewer.ReportSource = rptdoc
        frmInvoice.ShowDialog()
    End Sub

    Private Sub PrintContract()
        Try
            OpenFormToAdminPanel(frmPrintPreview)
            Dim dt As New DataTable
            With dt
                .Columns.Add("number")
                .Columns.Add("payment_date")
                .Columns.Add("opening_bal")
                .Columns.Add("payment")
                .Columns.Add("interest")
                .Columns.Add("closing_bal")
            End With
            For Each dr As DataGridViewRow In dgvAmortizationSchedule.Rows
                If dr.Cells(0).Value = "0" Then
                Else
                    dt.Rows.Add(dr.Cells(0).Value, Format(Convert.ToDateTime(dr.Cells(1).Value), "MMM dd, yyyy"), Format(CDec(dr.Cells(2).Value), "#,##0.00"), Format(CDec(dr.Cells(3).Value), "#,##0.00"), Format(CDec(dr.Cells(5).Value), "#,##0.00"), Format(CDec(dr.Cells(7).Value), "#,##0.00"))
                End If
            Next
            Dim rptdoc As CrystalDecisions.CrystalReports.Engine.ReportDocument
            rptdoc = New reportPropertyPayment
            With rptdoc
                .SetDataSource(dt)
                .SetParameterValue("price", Format(CDec(txtPrice.Text), "#,##0.00"))
                .SetParameterValue("total_price", Format(CDec(totalPrice.Text), "#,##0.00"))
                .SetParameterValue("subdivision", cbSubdivision.Text)
                .SetParameterValue("property_type", cbPropertyType.Text)
                .SetParameterValue("block", txtBlock.Text)
                .SetParameterValue("lot", txtlot.Text)
                .SetParameterValue("square_meter", txtSquaremeter.Text)
                .SetParameterValue("term", "" & CInt(txtLength.Text) & " Years Or " & CInt(txtLength.Text) * 12 & " Months")
                .SetParameterValue("payment_option", cbOption.Text)
                .SetParameterValue("down_payment", Format(CDec(txtDownPayment.Text), "#,##0.00"))
                .SetParameterValue("monthly_payment", Format(CDec(txtAmort.Text), "#,##0.00"))
                .SetParameterValue("agent", cbAgent.Text)
                .SetParameterValue("client_name", cbClient.Text)
                .SetParameterValue("start_contract", txtStart.Text)
                .SetParameterValue("end_contract", txtEnd.Text)
                If str_company Is Nothing Then
                    .SetParameterValue("company_name", "COMPANY")
                Else
                    .SetParameterValue("company_name", str_company)
                End If
                If str_address Is Nothing Then
                    .SetParameterValue("company_address", "ADDRESS")
                Else
                    .SetParameterValue("company_address", str_address)
                End If
                If str_h1 Is Nothing Then
                    .SetParameterValue("header1", "- - - - - - - - - - - - - - -")
                Else
                    .SetParameterValue("header1", str_h1)
                End If
            End With
            frmPrintPreview.ReportViewer.ReportSource = rptdoc
        Catch ex As Exception
            MsgBox(ex.Message, vbCritical)
        End Try
    End Sub

    Sub clearSaveUpdate()
        txtBlock.Text = ""
        txtlot.Text = ""
        txtSquaremeter.Text = "0"
        txtPrice.Text = "0.00"
        cbPropertyType.SelectedIndex = 0
        cbStatus.SelectedIndex = 0
        cbOption.SelectedIndex = 0
        agent_id.Text = "0"
        client_id.Text = "0"
        txtAmort.Text = "0.00"
        txtStart.Text = ""
        txtEnd.Text = ""
        txtLength.Text = "0"
        txtRate.Text = "0"
        dgvAmortizationSchedule.Columns.Clear()

        Try
            client_id.Text = CInt(cbClient.SelectedValue)
        Catch ex As Exception
        End Try
        Try
            agent_id.Text = CInt(cbAgent.SelectedValue)
        Catch ex As Exception
        End Try
        Try
            sub_id.Text = CInt(cbSubdivision.SelectedValue)
        Catch ex As Exception
        End Try
    End Sub


    Private Sub btnBrowse_Click(sender As Object, e As EventArgs) Handles btnBrowse.Click
        Using ofd As New OpenFileDialog With {.Filter = "(Image Files)|*.jpg;*.png;*.bmp|Jpg Files|*.jpg|Png Files|*.png|Bitmap Files|*.bmp",
               .Multiselect = False, .Title = "Select Property Image"}

            If ofd.ShowDialog = 1 Then
                propertyImage.BackgroundImage = Image.FromFile(ofd.FileName)
                OpenFileDialog1.FileName = ofd.FileName
            End If
        End Using
    End Sub

    Private Sub frmProduct_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.KeyPreview = True
        clearSaveUpdate()

        datetoday.Text = frmLogin.txtbox_date.Text
        dtToday.Value = CDate(frmLogin.txtbox_date.Text)

        'If frmMain.lblRole.Text.Contains("Administrator") = True Then
        '    PenaltyPanel.Visible = True
        'Else
        '    PenaltyPanel.Visible = False
        'End If
    End Sub

    Private Sub txtPrice_TextChanged(sender As Object, e As EventArgs) Handles txtPrice.TextChanged, txtAdditional.TextChanged
        If txtPrice.Text = "" Then
            txtPrice.Text = "0.00"
            txtPrice.SelectionStart = txtPrice.TextLength
        Else

            txtPrice.Text = Format(CDec(txtPrice.Text), "#,##0")
            txtPrice.SelectionStart = txtPrice.TextLength
        End If

        calculate_commission()
    End Sub

    Private Sub txtPrice_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtPrice.KeyPress, txtLength.KeyPress, txtDownPayment.KeyPress, txtComission.KeyPress, commission_month.KeyPress, txtAdditional.KeyPress
        If Not Char.IsDigit(e.KeyChar) AndAlso e.KeyChar <> ControlChars.Back Then
            e.Handled = True ' Ignore the key press event
        End If
    End Sub

    Private Sub txtRate_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtRate.KeyPress
        If Not Char.IsDigit(e.KeyChar) AndAlso e.KeyChar <> "." AndAlso e.KeyChar <> ControlChars.Back Then
            e.Handled = True ' Ignore the key press event
        End If
    End Sub
    Private Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
        If MsgBox("Are you sure you want to cancel?", vbYesNo + vbQuestion) = vbYes Then
            Me.Dispose()
            frmProductList.Enabled = True
            frmPOSEncoding.txtclearvalues()
            frmPOSEncoding.Dispose()
        Else

        End If
    End Sub

    Private Sub btnUpdate_Click(sender As Object, e As EventArgs) Handles btnUpdate.Click
        'Try
        If String.IsNullOrEmpty(txtBlock.Text) Or String.IsNullOrEmpty(txtlot.Text) Or String.IsNullOrEmpty(txtSquaremeter.Text) Then
            MsgBox("Property block, lot and square meter field cannot be empty!", vbCritical)
            Return
        ElseIf String.IsNullOrEmpty(cbSubdivision.Text) Then
            MsgBox("Subdivision cannot be empty!", vbCritical)
            Return
        ElseIf String.IsNullOrEmpty(cbStatus.Text) Then
            MsgBox("Status field cannot be empty!", vbCritical)
            Return
        ElseIf CDec(txtPrice.Text) = 0 Then
            MsgBox("Price field cannot be zero!", vbCritical)
            Return
        ElseIf sub_id.Text = "0" Then
            MsgBox("Please select a valid Subdivision!", vbCritical)
            Return
        ElseIf client_id.Text = "0" Then
            MsgBox("Please select a valid Client!", vbCritical)
            Return
        ElseIf agent_id.Text = "0" Then
            MsgBox("Please select a valid Agent!", vbCritical)
            Return
        ElseIf cbPropertyType.Text = "" Then
            MsgBox("Please select the type of property!", vbCritical)
            Return
        ElseIf cbPropertyType.Text = "" Then
            MsgBox("Please select the type of property!", vbCritical)
            Return
        End If
        If MsgBox("Update this property?", vbYesNo + vbQuestion) = vbYes Then
            btnAmort.PerformClick()
            cn.Close()
            cn.Open()
            Dim property_transaction_update As MySqlTransaction = cn.BeginTransaction()
            'Try
            Using property_transaction_Cmd As MySqlCommand = cn.CreateCommand()
                property_transaction_Cmd.Transaction = property_transaction_update
                property_transaction_Cmd.CommandText = "UPDATE `tblproperty` SET `block`=@0, `lot`=@1,`square_meter`=@2,`type_id`=@3,`price`=@4,`status`=@5,`agent_id`=@6, client_id = @7, monthly_amort = @8, start_contract = @9, end_contract = @10, length_years = @11, property_type = @12, property_date = @14, commission = @13, penalty = @15, is_corner_lot = @16, additionalAmount = @17 WHERE `id`=@00"
                property_transaction_Cmd.Parameters.AddWithValue("@00", txtID.Text)
                property_transaction_Cmd.Parameters.AddWithValue("@0", txtBlock.Text)
                property_transaction_Cmd.Parameters.AddWithValue("@1", txtlot.Text)
                property_transaction_Cmd.Parameters.AddWithValue("@2", txtSquaremeter.Text)
                property_transaction_Cmd.Parameters.AddWithValue("@3", cbSubdivision.SelectedValue)
                property_transaction_Cmd.Parameters.AddWithValue("@4", CDec(txtPrice.Text))
                property_transaction_Cmd.Parameters.AddWithValue("@5", cbStatus.Text)
                property_transaction_Cmd.Parameters.AddWithValue("@6", agent_id.Text)
                property_transaction_Cmd.Parameters.AddWithValue("@7", client_id.Text)

                property_transaction_Cmd.Parameters.AddWithValue("@8", CDec(txtAmort.Text))
                property_transaction_Cmd.Parameters.AddWithValue("@9", Format(Convert.ToDateTime(txtStart.Text), "yyyy-MM-dd"))
                property_transaction_Cmd.Parameters.AddWithValue("@10", Format(Convert.ToDateTime(txtEnd.Text), "yyyy-MM-dd"))
                property_transaction_Cmd.Parameters.AddWithValue("@11", txtLength.Text)
                property_transaction_Cmd.Parameters.AddWithValue("@12", cbPropertyType.Text)
                property_transaction_Cmd.Parameters.AddWithValue("@13", commission_id.Text)
                property_transaction_Cmd.Parameters.AddWithValue("@14", Format(Convert.ToDateTime(dtToday.Text), "yyyy-MM-dd"))
                property_transaction_Cmd.Parameters.AddWithValue("@15", cbPenalty.Text)

                If cbCorner.Checked = True Then
                    property_transaction_Cmd.Parameters.AddWithValue("@16", 1)
                Else
                    property_transaction_Cmd.Parameters.AddWithValue("@16", 0)
                End If

                property_transaction_Cmd.Parameters.AddWithValue("@17", CDec(txtAdditional.Text))

                property_transaction_Cmd.ExecuteNonQuery()
            End Using

            Using property_payments_delete As MySqlCommand = cn.CreateCommand()
                property_payments_delete.Transaction = property_transaction_update
                property_payments_delete.CommandText = "Delete from tblinstallments where property_id = '" & txtID.Text & "'"
                property_payments_delete.ExecuteNonQuery()
            End Using

            For Each row As DataGridViewRow In dgvAmortizationSchedule.Rows
                If row.Cells(0).Value = "0" Then
                Else
                    Using property_payments_Cmd As MySqlCommand = cn.CreateCommand()
                        property_payments_Cmd.Transaction = property_transaction_update
                        property_payments_Cmd.CommandText = "Insert into tblinstallments (`property_id`, `payment_no`, `payment_date`, `amount`) values (@1, @2, @3, @4)"
                        property_payments_Cmd.Parameters.AddWithValue("@1", txtID.Text)
                        property_payments_Cmd.Parameters.AddWithValue("@2", row.Cells(0).Value)
                        property_payments_Cmd.Parameters.AddWithValue("@3", Format(Convert.ToDateTime(row.Cells(1).Value), "yyyy-MM-dd"))
                        property_payments_Cmd.Parameters.AddWithValue("@4", row.Cells(3).Value)
                        property_payments_Cmd.ExecuteNonQuery()
                    End Using
                End If
            Next


            Using commission_payments_delete As MySqlCommand = cn.CreateCommand()
                commission_payments_delete.Transaction = property_transaction_update
                commission_payments_delete.CommandText = "Delete from tblcommissionsched where property_id = '" & txtID.Text & "'"
                commission_payments_delete.ExecuteNonQuery()
            End Using

            For Each row As DataGridViewRow In dgvCommissionSchedule.Rows
                If row.Cells(0).Value = "0" Then
                Else
                    Using commission_payments_Cmd As MySqlCommand = cn.CreateCommand()
                        commission_payments_Cmd.Transaction = property_transaction_update
                        commission_payments_Cmd.CommandText = "Insert into tblcommissionsched (`property_id`, `payment_no`, `payment_date`, `amount`) values (@1, @2, @3, @4)"
                        commission_payments_Cmd.Parameters.AddWithValue("@1", txtID.Text)
                        commission_payments_Cmd.Parameters.AddWithValue("@2", row.Cells(0).Value)
                        commission_payments_Cmd.Parameters.AddWithValue("@3", Format(Convert.ToDateTime(row.Cells(1).Value), "yyyy-MM-dd"))
                        commission_payments_Cmd.Parameters.AddWithValue("@4", row.Cells(3).Value)
                        commission_payments_Cmd.ExecuteNonQuery()
                    End Using
                End If
            Next



            For Each row As DataGridViewRow In dgvAmortizationSchedule.Rows
                If CDec(row.Cells(9).Value) = 0 Then
                    Using property_payments_installment As MySqlCommand = cn.CreateCommand()
                        property_payments_installment.Transaction = property_transaction_update
                        property_payments_installment.CommandText = "update tblinstallments set status = 'PAID' where property_id = @1 and payment_no = @2"
                        property_payments_installment.Parameters.AddWithValue("@1", txtID.Text)
                        property_payments_installment.Parameters.AddWithValue("@2", CInt(row.Cells(0).Value))
                        property_payments_installment.ExecuteNonQuery()
                    End Using
                Else
                    Using property_payments_installment As MySqlCommand = cn.CreateCommand()
                        property_payments_installment.Transaction = property_transaction_update
                        property_payments_installment.CommandText = "update tblinstallments Set status = 'UNPAID' where property_id = @1 and payment_no = @2"
                        property_payments_installment.Parameters.AddWithValue("@1", txtID.Text)
                        property_payments_installment.Parameters.AddWithValue("@2", CInt(row.Cells(0).Value))
                        property_payments_installment.ExecuteNonQuery()
                    End Using
                End If
            Next


            For Each row As DataGridViewRow In dgvCommissionSchedule.Rows
                If CDec(row.Cells(9).Value) = 0 Then
                    Using property_payments_installment As MySqlCommand = cn.CreateCommand()
                        property_payments_installment.Transaction = property_transaction_update
                        property_payments_installment.CommandText = "update tblcommissionsched set status = 'RELEASED' where property_id = @1 and payment_no = @2"
                        property_payments_installment.Parameters.AddWithValue("@1", txtID.Text)
                        property_payments_installment.Parameters.AddWithValue("@2", CInt(row.Cells(0).Value))
                        property_payments_installment.ExecuteNonQuery()
                    End Using
                Else
                    Using property_payments_installment As MySqlCommand = cn.CreateCommand()
                        property_payments_installment.Transaction = property_transaction_update
                        property_payments_installment.CommandText = "update tblcommissionsched Set status = '-' where property_id = @1 and payment_no = @2"
                        property_payments_installment.Parameters.AddWithValue("@1", txtID.Text)
                        property_payments_installment.Parameters.AddWithValue("@2", CInt(row.Cells(0).Value))
                        property_payments_installment.ExecuteNonQuery()
                    End Using
                End If
            Next


            property_transaction_update.Commit()

            MsgBox("Property has been successfully updated!", vbInformation)

            cn.Close()
            AuditTrail("Updated a Property for client '" & cbClient.Text & "' set Block '" & txtBlock.Text & "'; Lot '" & txtlot.Text & "'; Square Meter = '" & txtSquaremeter.Text & "'; Description '" & txtlot.Text & "'; Type '" & cbSubdivision.Text & "'; Price '" & txtPrice.Text & "'; Agent: '" & cbAgent.Text & "'; Length; '" & txtLength.Text & " Years & " & txtLength.Text & " Months'; Penalty/Interest;" & cbPenalty.Text & " with property id '" & txtID.Text & "'.")

            PrintContract()

            With frmProductList
                .loadPropertyRecords()
            End With
            Me.Close()

            'Catch ex As Exception
            '    property_transaction_update.Rollback()
            '    MessageBox.Show("Transaction failed. Transaction rolled back.", "", MessageBoxButtons.OK, MessageBoxIcon.Error)
            'End Try
        End If
        'Catch ex As Exception

        '    MsgBox(ex.Message, vbCritical)
        'End Try

    End Sub

    Private Sub cbAgent_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cbAgent.SelectedIndexChanged
        Try
            agent_id.Text = CInt(cbAgent.SelectedValue)
        Catch ex As Exception
        End Try
    End Sub

    Private Sub cbAgent_TextChanged(sender As Object, e As EventArgs) Handles cbAgent.TextChanged, cbClient.TextChanged
        Try
            agent_id.Text = CInt(cbAgent.SelectedValue)
        Catch ex As Exception
        End Try
    End Sub

    Sub calculateamortization()

        Dim DateNow As String = Format(Convert.ToDateTime(datetoday.Text), "yyyy/MM/dd")
        ' Create a DataTable to hold the amortization schedule
        Dim amortizationTable As New DataTable()
        amortizationTable.Columns.Add("Payment No.", GetType(Integer))
        amortizationTable.Columns.Add("Payment Date", GetType(DateTime))
        amortizationTable.Columns.Add("Opening Balance", GetType(Decimal))
        amortizationTable.Columns.Add("Payment", GetType(Decimal))
        amortizationTable.Columns.Add("Principal", GetType(Decimal))
        amortizationTable.Columns.Add("Interest", GetType(Decimal))
        amortizationTable.Columns.Add("Total Interest", GetType(Decimal))
        amortizationTable.Columns.Add("Closing Balance", GetType(Decimal))
        amortizationTable.Columns.Add("Total Paid", GetType(Decimal))
        amortizationTable.Columns.Add("Balance", GetType(Decimal))






        If cbOption.Text = "Cash" Then
            Dim downPayment As Decimal = CDec(txtDownPayment.Text)
            Dim dPdate As DateTime = DateTime.Parse(DateNow)
            amortizationTable.Rows.Add(1, dPdate, Format(CDec(txtPrice.Text), "#,##0.00"), Format(CDec(txtPrice.Text) - downPayment, "#,##0.00"), 0, 0, 0, 0)
            totalPrice.Text = Format(CDec(txtPrice.Text), "#,##0.00")
        Else
            'Dim loanAmount As Double = CDec(txtPrice.Text.Trim) ' The initial loan amount
            'Dim interestRate As Double = CDec(txtRate.Text.Trim) / 100 ' The annual interest rate
            'Dim loanTerm As Integer = CInt(txtLength.Text.Trim) ' The loan term in years
            'Dim downPayment As Double = CDec(txtDownPayment.Text) ' Advance payment made towards the loan
            ''Dim monthlyInterestRate As Decimal = (1 + interestRate) ^ (1 / 12) - 1 ' Monthly interest rate
            'Dim monthlyInterestRate As Double = interestRate / 12 ' Monthly interest rate
            'Dim totalMonths As Integer = loanTerm * 12 ' Total number of months
            'Dim loanAmountAfterDownPayment As Double = loanAmount - downPayment ' Loan amount after down payment
            'Dim monthlyPayment As Double = loanAmountAfterDownPayment * (monthlyInterestRate * (1 + monthlyInterestRate) ^ totalMonths) / ((1 + monthlyInterestRate) ^ totalMonths - 1)
            'txtAmort.Text = Math.Round(monthlyPayment, 2)
            '' Generate the amortization schedule
            'Dim balance As Double = loanAmountAfterDownPayment
            'Dim month As Integer = 1
            'Dim totalInterest As Double = 0
            'Dim paymentDate As DateTime = DateAdd(DateInterval.Month, 1, DateTime.Parse(DateNow))
            'Dim dPdate As DateTime = DateTime.Parse(DateNow)
            '' Add row for the down payment
            'amortizationTable.Rows.Add(0, dPdate, balance, downPayment, downPayment, 0, 0, balance)
            'While balance > 0 AndAlso month <= totalMonths
            '    Dim interestPayment As Double = balance * monthlyInterestRate
            '    Dim principalPayment As Double = monthlyPayment - interestPayment
            '    Dim opening_balance As Double = balance
            '    balance -= principalPayment
            '    totalInterest += interestPayment
            '    amortizationTable.Rows.Add(month, paymentDate, opening_balance, monthlyPayment, principalPayment, interestPayment, totalInterest, balance)
            '    month += 1
            '    paymentDate = paymentDate.AddMonths(1)
            'End While

            Dim loanAmount As Decimal = CDec(txtPrice.Text.Trim) ' The initial loan amount
            'Dim interestRate As Double = CDec(txtRate.Text.Trim) / 100 ' The annual interest rate
            Dim loanTerm As Integer = CInt(txtLength.Text.Trim) ' The loan term in years
            Dim downPayment As Decimal = CDec(txtDownPayment.Text) ' Advance payment made towards the loan

            Dim AdditionalPayment As Decimal = CDec(txtAdditional.Text)
            'Dim monthlyInterestRate As Decimal = (1 + interestRate) ^ (1 / 12) - 1 ' Monthly interest rate
            'Dim monthlyInterestRate As Double = interestRate / 12 ' Monthly interest rate
            Dim totalMonths As Integer = CInt(txtLength.Text.Trim) ' Total number of months
            Dim loanAmountAfterDownPayment As Decimal = (loanAmount + AdditionalPayment) - downPayment ' Loan amount after down payment
            Dim monthlyPayment As Decimal = loanAmountAfterDownPayment / totalMonths
            txtAmort.Text = Format(monthlyPayment, "N12")
            ' Generate the amortization schedule
            Dim balance As Decimal = loanAmountAfterDownPayment
            Dim month As Integer = 1
            Dim totalInterest As Decimal = 0
            Dim paymentDate As DateTime = DateAdd(DateInterval.Month, 0, DateTime.Parse(DateNow))
            Dim dPdate As DateTime = DateTime.Parse(DateNow)
            ' Add row for the down payment
            amortizationTable.Rows.Add(0, dPdate, loanAmount, downPayment, 0, 0, 0, balance)
            Dim tcp As Decimal = 0


            While balance > 0 AndAlso month <= totalMonths
                'Dim interestPayment As Double = balance * monthlyInterestRate
                'Dim principalPayment As Double = monthlyPayment - interestPayment
                Dim opening_balance As Decimal = balance
                balance -= monthlyPayment
                'totalInterest += interestPayment
                amortizationTable.Rows.Add(month, paymentDate, opening_balance, monthlyPayment, 0, 0, totalInterest, balance)
                month += 1
                paymentDate = paymentDate.AddMonths(1)
                tcp = tcp + monthlyPayment
            End While

            totalPrice.Text = Format(tcp, "#,##0.00")

        End If

        dgvAmortizationSchedule.DataSource = amortizationTable
        dgvAmortizationSchedule.Columns("Payment Date").DefaultCellStyle.Format = "MMM dd, yyyy"
        dgvAmortizationSchedule.Columns("Opening Balance").DefaultCellStyle.Format = "N12"
        dgvAmortizationSchedule.Columns("Payment").DefaultCellStyle.Format = "N12"
        dgvAmortizationSchedule.Columns("Principal").DefaultCellStyle.Format = "N12"
        dgvAmortizationSchedule.Columns("Interest").DefaultCellStyle.Format = "N12"
        dgvAmortizationSchedule.Columns("Total Interest").DefaultCellStyle.Format = "N12"
        dgvAmortizationSchedule.Columns("Closing Balance").DefaultCellStyle.Format = "N12"
        dgvAmortizationSchedule.Columns("Total Paid").DefaultCellStyle.Format = "N12"
        dgvAmortizationSchedule.Columns("Balance").DefaultCellStyle.Format = "N12"

        dgvAmortizationSchedule.Columns(4).Visible = False
        dgvAmortizationSchedule.Columns(6).Visible = False
        dgvAmortizationSchedule.Columns(8).Visible = False
        dgvAmortizationSchedule.Columns(9).Visible = False

        If cbOption.Text = "Cash" Then
            txtStart.Text = Format(Convert.ToDateTime(dgvAmortizationSchedule.Rows(0).Cells(1).Value), "MMM dd, yyyy")
            txtEnd.Text = Format(Convert.ToDateTime(dgvAmortizationSchedule.Rows(dgvAmortizationSchedule.Rows.Count - 1).Cells(1).Value), "MMM dd, yyyy")

            txtStartContract.Text = Format(Convert.ToDateTime(dgvAmortizationSchedule.Rows(0).Cells(1).Value), "MM/dd/yyyy")
            txtEndContract.Text = Format(Convert.ToDateTime(dgvAmortizationSchedule.Rows(dgvAmortizationSchedule.Rows.Count - 1).Cells(1).Value), "MM/dd/yyyy")
        Else

            txtStart.Text = Format(Convert.ToDateTime(dgvAmortizationSchedule.Rows(1).Cells(1).Value), "MMM dd, yyyy")
            txtEnd.Text = Format(Convert.ToDateTime(dgvAmortizationSchedule.Rows(dgvAmortizationSchedule.Rows.Count - 1).Cells(1).Value), "MMM dd, yyyy")

            txtStartContract.Text = Format(Convert.ToDateTime(dgvAmortizationSchedule.Rows(1).Cells(1).Value), "MM/dd/yyyy")
            txtEndContract.Text = Format(Convert.ToDateTime(dgvAmortizationSchedule.Rows(dgvAmortizationSchedule.Rows.Count - 1).Cells(1).Value), "MM/dd/yyyy")
        End If


        Dim totalAmount As Decimal = CDec(property_paid.Text)

        For Each row As DataGridViewRow In dgvAmortizationSchedule.Rows
            Dim amount As Decimal = CDec(row.Cells(3).Value)
            Dim paid As Decimal = Math.Min(amount, totalAmount)

            row.Cells(8).Value = paid
            row.Cells(9).Value = amount - paid

            totalAmount -= paid

        Next

        For Each col As DataGridViewColumn In dgvAmortizationSchedule.Columns
            col.SortMode = DataGridViewColumnSortMode.NotSortable
        Next
        'For Each row As DataGridViewRow In dgvAmortizationSchedule.Rows
        '    If row.Cells(9).Value <> 0 Then
        '        row.Selected = True
        '        dgvAmortizationSchedule.CurrentCell = row.Cells(9)
        '        Exit For
        '    End If
        'Next
    End Sub

    Sub calculateCommission()

        Dim DateNow As String = Format(Convert.ToDateTime(datetoday.Text), "yyyy/MM/dd")
        ' Create a DataTable to hold the amortization schedule
        Dim amortizationCommissionTable As New DataTable()
        amortizationCommissionTable.Columns.Add("Payment No.", GetType(Integer))
        amortizationCommissionTable.Columns.Add("Payment Date", GetType(DateTime))
        amortizationCommissionTable.Columns.Add("Opening Balance", GetType(Decimal))
        amortizationCommissionTable.Columns.Add("Payment", GetType(Decimal))
        amortizationCommissionTable.Columns.Add("Principal", GetType(Decimal))
        amortizationCommissionTable.Columns.Add("Interest", GetType(Decimal))
        amortizationCommissionTable.Columns.Add("Total Interest", GetType(Decimal))
        amortizationCommissionTable.Columns.Add("Closing Balance", GetType(Decimal))
        amortizationCommissionTable.Columns.Add("Total Paid", GetType(Decimal))
        amortizationCommissionTable.Columns.Add("Balance", GetType(Decimal))






        If cbOption.Text = "Cash" Then
            Dim downPayment As Decimal = 0
            Dim dPdate As DateTime = DateTime.Parse(DateNow)
            amortizationCommissionTable.Rows.Add(1, dPdate, Format(CDec(txtComAmount.Text), "#,##0.00"), Format(CDec(txtComAmount.Text) - downPayment, "#,##0.00"), 0, 0, 0, 0)
            'totalPrice.Text = Format(CDec(txtComAmount.Text), "#,##0.00")
        Else
            'Dim loanAmount As Double = CDec(txtPrice.Text.Trim) ' The initial loan amount
            'Dim interestRate As Double = CDec(txtRate.Text.Trim) / 100 ' The annual interest rate
            'Dim loanTerm As Integer = CInt(txtLength.Text.Trim) ' The loan term in years
            'Dim downPayment As Double = CDec(txtDownPayment.Text) ' Advance payment made towards the loan
            ''Dim monthlyInterestRate As Decimal = (1 + interestRate) ^ (1 / 12) - 1 ' Monthly interest rate
            'Dim monthlyInterestRate As Double = interestRate / 12 ' Monthly interest rate
            'Dim totalMonths As Integer = loanTerm * 12 ' Total number of months
            'Dim loanAmountAfterDownPayment As Double = loanAmount - downPayment ' Loan amount after down payment
            'Dim monthlyPayment As Double = loanAmountAfterDownPayment * (monthlyInterestRate * (1 + monthlyInterestRate) ^ totalMonths) / ((1 + monthlyInterestRate) ^ totalMonths - 1)
            'txtAmort.Text = Math.Round(monthlyPayment, 2)
            '' Generate the amortization schedule
            'Dim balance As Double = loanAmountAfterDownPayment
            'Dim month As Integer = 1
            'Dim totalInterest As Double = 0
            'Dim paymentDate As DateTime = DateAdd(DateInterval.Month, 1, DateTime.Parse(DateNow))
            'Dim dPdate As DateTime = DateTime.Parse(DateNow)
            '' Add row for the down payment
            'amortizationTable.Rows.Add(0, dPdate, balance, downPayment, downPayment, 0, 0, balance)
            'While balance > 0 AndAlso month <= totalMonths
            '    Dim interestPayment As Double = balance * monthlyInterestRate
            '    Dim principalPayment As Double = monthlyPayment - interestPayment
            '    Dim opening_balance As Double = balance
            '    balance -= principalPayment
            '    totalInterest += interestPayment
            '    amortizationTable.Rows.Add(month, paymentDate, opening_balance, monthlyPayment, principalPayment, interestPayment, totalInterest, balance)
            '    month += 1
            '    paymentDate = paymentDate.AddMonths(1)
            'End While

            Dim loanAmount As Decimal = CDec(txtComAmount.Text.Trim) ' The initial loan amount
            'Dim interestRate As Double = CDec(txtRate.Text.Trim) / 100 ' The annual interest rate
            'Dim loanTerm As Integer = CInt(txtLength.Text.Trim) ' The loan term in years
            Dim downPayment As Decimal = 0 ' Advance payment made towards the loan
            'Dim monthlyInterestRate As Decimal = (1 + interestRate) ^ (1 / 12) - 1 ' Monthly interest rate
            'Dim monthlyInterestRate As Double = interestRate / 12 ' Monthly interest rate
            Dim totalMonths As Integer = CInt(commission_month.Text) ' Total number of months
            Dim loanAmountAfterDownPayment As Decimal = loanAmount - downPayment ' Loan amount after down payment
            Dim monthlyPayment As Decimal = loanAmountAfterDownPayment / totalMonths
            'txtAmort.Text = Format(monthlyPayment, "#,##0.00")
            ' Generate the amortization schedule
            Dim balance As Decimal = loanAmountAfterDownPayment
            Dim month As Integer = 1
            Dim totalInterest As Decimal = 0
            Dim paymentDate As DateTime = DateAdd(DateInterval.Month, 0, DateTime.Parse(DateNow))
            Dim dPdate As DateTime = DateTime.Parse(DateNow)
            ' Add row for the down payment
            amortizationCommissionTable.Rows.Add(0, dPdate, loanAmount, downPayment, 0, 0, 0, balance)
            Dim tcp As Decimal = 0


            While balance > 0 AndAlso month <= totalMonths
                'Dim interestPayment As Double = balance * monthlyInterestRate
                'Dim principalPayment As Double = monthlyPayment - interestPayment
                Dim opening_balance As Decimal = balance
                balance -= monthlyPayment
                'totalInterest += interestPayment
                amortizationCommissionTable.Rows.Add(month, paymentDate, opening_balance, monthlyPayment, 0, 0, totalInterest, balance)
                month += 1
                paymentDate = paymentDate.AddMonths(1)
                tcp = tcp + monthlyPayment
            End While

            txtComAmount.Text = Format(tcp, "#,##0.00")
        End If

        dgvCommissionSchedule.DataSource = amortizationCommissionTable
        dgvCommissionSchedule.Columns("Payment Date").DefaultCellStyle.Format = "MMM dd, yyyy"
        dgvCommissionSchedule.Columns("Opening Balance").DefaultCellStyle.Format = "N12"
        dgvCommissionSchedule.Columns("Payment").DefaultCellStyle.Format = "N12"
        dgvCommissionSchedule.Columns("Principal").DefaultCellStyle.Format = "N12"
        dgvCommissionSchedule.Columns("Interest").DefaultCellStyle.Format = "N12"
        dgvCommissionSchedule.Columns("Total Interest").DefaultCellStyle.Format = "N12"
        dgvCommissionSchedule.Columns("Closing Balance").DefaultCellStyle.Format = "N12"
        dgvCommissionSchedule.Columns("Total Paid").DefaultCellStyle.Format = "N12"
        dgvCommissionSchedule.Columns("Balance").DefaultCellStyle.Format = "N12"

        dgvCommissionSchedule.Columns(4).Visible = False
        dgvCommissionSchedule.Columns(6).Visible = False
        dgvCommissionSchedule.Columns(8).Visible = False
        dgvCommissionSchedule.Columns(9).Visible = False


        'Dim totalAmount As Double = CDec(commission_paid.Text)
        Dim totalAmount As Decimal = 0

        For Each row As DataGridViewRow In dgvCommissionSchedule.Rows
            Dim amount As Decimal = CDec(row.Cells(3).Value)
            Dim paid As Decimal = Math.Min(amount, totalAmount)

            row.Cells(8).Value = paid
            row.Cells(9).Value = amount - paid

            totalAmount -= paid

        Next

        For Each col As DataGridViewColumn In dgvCommissionSchedule.Columns
            col.SortMode = DataGridViewColumnSortMode.NotSortable
        Next
    End Sub

    Private Sub txtAmort_TextChanged(sender As Object, e As EventArgs) Handles txtAmort.TextChanged
        'Try
        '    totalPrice.Text = Format(CDec(txtAmort.Text) * (CInt(txtLength.Text) * 12), "#,##0.00")
        'Catch ex As Exception
        '    totalPrice.Text = "0.00"
        'End Try
    End Sub

    Private Sub txtLength_TextChanged(sender As Object, e As EventArgs) Handles txtLength.TextChanged
        If txtLength.Text = "" Then
            txtLength.Text = "0"
            txtLength2.Text = "0 Months"
        Else
            Dim months As Integer = (CInt(txtLength.Text.Trim) * 12)
            txtLength2.Text = months & " Months"
        End If

        calculate_commission()
    End Sub

    Private Sub cbClient_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cbClient.SelectedIndexChanged
        Try
            client_id.Text = CInt(cbClient.SelectedValue)
        Catch ex As Exception
        End Try
    End Sub

    Private Sub frmProduct_FormClosed(sender As Object, e As FormClosedEventArgs) Handles Me.FormClosed
        frmProductList.Enabled = True
    End Sub

    Private Sub txtStart_TextChanged(sender As Object, e As EventArgs) Handles txtStart.TextChanged

    End Sub

    Private Sub btnAmort_Click(sender As Object, e As EventArgs) Handles btnAmort.Click
        If cbOption.Text = "Installment" Then
            If String.IsNullOrEmpty(txtPrice.Text) Or txtPrice.Text = 0 Then
                MsgBox("Contract Price cannot be 0 or empty.", vbCritical)
                Return
            End If
            'If String.IsNullOrEmpty(txtRate.Text) Or txtRate.Text = 0 Then
            '    MsgBox("Interest Rate cannot be 0% for installment.", vbCritical)
            '    Return
            'End If
            If String.IsNullOrEmpty(txtLength.Text) Or txtLength.Text = 0 Then
                MsgBox("Term cannot be 0 or empty for installment.", vbCritical)
                Return
            End If
            calculateamortization()
            calculateCommission()
        Else
            If String.IsNullOrEmpty(txtPrice.Text) Or txtPrice.Text = 0 Then
                MsgBox("Contract Price cannot be 0 or empty.", vbCritical)
                Return
            End If
            calculateamortization()
            calculateCommission()
        End If
    End Sub

    Private Sub InstallmentOption()
        txtLength.Enabled = True
        txtLength.Text = "0"
        'txtLength2.Visible = True
        'Label19.Visible = True
        txtRate.Enabled = True
        txtRate.Text = "0"
        txtDownPayment.Enabled = True
        txtDownPayment.Text = "0.00"
        'btnAmort.Visible = True
        txtAmort.Text = "0.00"
    End Sub

    Private Sub CashOption()
        txtLength.Enabled = False
        'txtLength2.Visible = False
        'Label19.Visible = False
        txtRate.Enabled = False
        txtDownPayment.Enabled = False
        'btnAmort.Visible = False
    End Sub

    Private Sub txtDownPayment_TextChanged(sender As Object, e As EventArgs) Handles txtDownPayment.TextChanged
        If txtDownPayment.Text = "" Then
            txtDownPayment.Text = "0.00"
            txtDownPayment.SelectionStart = txtDownPayment.TextLength
        Else

            txtDownPayment.Text = Format(CDec(txtDownPayment.Text), "#,##0")
            txtDownPayment.SelectionStart = txtDownPayment.TextLength
        End If
    End Sub

    Private Sub txtRate_TextChanged(sender As Object, e As EventArgs) Handles txtRate.TextChanged
        If txtRate.Text = "" Then
            txtRate.Text = "0"
        Else
        End If
    End Sub

    Private Sub cbOption_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cbOption.SelectedIndexChanged
        If cbOption.Text = "Installment" Then
            InstallmentOption()
        ElseIf cbOption.Text = "Cash" Then
            CashOption()
        End If
    End Sub

    Private Sub cbSubdivision_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cbSubdivision.SelectedIndexChanged
        Try
            sub_id.Text = CInt(cbSubdivision.SelectedValue)
            Try
                cn.Close()
                cn.Open()
                cm = New MySqlCommand("Select `amount_per_sqm` from tblpropertytype where id = " & CInt(cbSubdivision.SelectedValue) & "", cn)
                subAmountPerSQM = cm.ExecuteScalar
                cn.Close()
            Catch ex As Exception
            End Try
            Try
                cn.Close()
                cn.Open()
                cm = New MySqlCommand("Select `default_term_months`from tblpropertytype where id = " & CInt(cbSubdivision.SelectedValue) & "", cn)
                subDefaultTerm = cm.ExecuteScalar
                cn.Close()
                txtLength.Text = subDefaultTerm
            Catch ex As Exception
            End Try
            Try
                cn.Close()
                cn.Open()
                cm = New MySqlCommand("Select `amount_corner_lot_additional` from tblpropertytype where id = " & CInt(cbSubdivision.SelectedValue) & "", cn)
                subAdditionalCornerLot = cm.ExecuteScalar
                cn.Close()
            Catch ex As Exception
            End Try
        Catch ex As Exception
        End Try
    End Sub

    Private Sub Label17_Click(sender As Object, e As EventArgs) Handles Label17.Click, Label18.Click

    End Sub

    Private Sub cbPropertyType_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cbPropertyType.SelectedIndexChanged
        Try
            fillCombo("Select DISTINCT(commission_percentage) as commission_percentage from tblcomission where status = 'Active' and property_type = '" & cbPropertyType.Text & "'", txtComission, "tblcomission", "commission_percentage", "commission_percentage")
            txtComission.SelectedIndex = 0
        Catch ex As Exception
            txtComission.Text = "0"
            commission_month.Text = "1"
        End Try

    End Sub

    Private Sub txtComission_SelectedIndexChanged(sender As Object, e As EventArgs) Handles txtComission.SelectedIndexChanged
        Try
            fillCombo("Select ifnull(commission_percentage,0) as commission_percentage, ifnull(commision_months,0) as comMonths from tblcomission where tblcomission.commission_percentage = " & CInt(txtComission.Text) & "", commission_month, "tblcomission", "comMonths", "commission_percentage")
            commission_month.SelectedIndex = 0
        Catch ex As Exception
            commission_month.Text = "1"
        End Try

    End Sub

    Private Sub calculate_commission()
        Try
            txtComAmount.Text = Format(CDec(txtPrice.Text) * (CDec(txtComission.Text) * 0.01), "#,##0.00")
        Catch ex As Exception
            txtComAmount.Text = "0.00"
        End Try
        Try
            txtComAmountMonthly.Text = Format((CDec(txtPrice.Text) * (CDec(txtComission.Text) * 0.01)) / CInt(commission_month.Text), "#,##0.00")
        Catch ex As Exception
            txtComAmountMonthly.Text = "0.00"
        End Try
    End Sub

    Private Sub txtComission_TextChanged(sender As Object, e As EventArgs) Handles txtComission.TextChanged, commission_month.TextChanged

    End Sub

    Private Sub print_contract_Click(sender As Object, e As EventArgs) Handles print_contract.Click
        PrintContract()
        AuditTrail("Re-printed the Property Contract of client '" & cbClient.Text & "'.")
    End Sub

    Private Sub dtTo_ValueChanged(sender As Object, e As EventArgs) Handles dtToday.ValueChanged
        datetoday.Text = Format(Convert.ToDateTime(dtToday.Value), "MMM dd, yyyy")
    End Sub

    Private Sub commission_month_SelectedIndexChanged(sender As Object, e As EventArgs) Handles commission_month.SelectedIndexChanged
        Try
            cn.Close()
            cn.Open()
            cm = New MySqlCommand("Select id from tblcomission where commission_percentage = " & CInt(txtComission.Text) & " and commision_months = " & CInt(commission_month.Text) & "", cn)
            commission_id.Text = cm.ExecuteScalar
            cn.Close()
        Catch ex As Exception

        End Try
        calculate_commission()
    End Sub

    Private Sub txtSquaremeter_TextChanged(sender As Object, e As EventArgs) Handles txtSquaremeter.TextChanged
        Try
            txtPrice.Text = subAmountPerSQM * CInt(txtSquaremeter.Text)
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub cbCorner_CheckedChanged(sender As Object, e As EventArgs) Handles cbCorner.CheckedChanged
        If cbCorner.Checked = True Then
            txtAdditional.Text = subAdditionalCornerLot
        Else
            txtAdditional.Text = "0.00"
        End If
    End Sub

    Private Sub cbOption_KeyPress(sender As Object, e As KeyPressEventArgs) Handles cbOption.KeyPress, cbPropertyType.KeyPress, cbStatus.KeyPress, txtComission.KeyPress, cbPenalty.KeyPress, commission_month.KeyPress
        e.Handled = True
    End Sub
End Class