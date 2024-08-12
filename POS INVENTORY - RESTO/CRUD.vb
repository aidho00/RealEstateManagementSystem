Imports MySql.Data.MySqlClient
Module CRUD
    Public result As String
    Public da As New MySqlDataAdapter
    Public dr As MySqlDataReader
    Public dt As New DataTable
    Public ds As New DataSet

    Public Sub myinsert(ByVal sql As String, ByVal msg_boolean As Boolean, ByVal msgSuccess As String, ByVal msgFailed As String)
        Try

            cm = New MySqlCommand(sql, cn)
            If msg_boolean = True Then
                result = cm.ExecuteNonQuery
                If result = 0 Then
                    MsgBox(msgFailed, vbCritical)
                Else
                    MsgBox(msgSuccess, vbInformation)
                End If
            Else
                cm.ExecuteNonQuery()
            End If
            cm.Dispose()

        Catch ex As Exception
            cn.Close()
            MsgBox(ex.Message, vbCritical)
        End Try
    End Sub

    Public Sub mydisplay(ByVal sql As String, ByVal DTG As Object)
        Try

            dt = New DataTable
            With cm
                .Connection = cn
                .CommandText = sql
            End With
            da.SelectCommand = cm
            da.Fill(dt)
            DTG.DataSource = dt
            da.Dispose()

        Catch ex As Exception
            cn.Close()
            MsgBox(ex.Message, vbCritical)
        End Try

    End Sub

    Public Sub myupdate(ByVal sql As String, ByVal msg_boolean As Boolean, ByVal msgSuccess As String, ByVal msgFailed As String)
        'Try

        cm = New MySqlCommand(sql, cn)
            If msg_boolean = True Then
                result = cm.ExecuteNonQuery
                If result = 0 Then
                    MsgBox(msgFailed, vbCritical)
                Else
                    MsgBox(msgSuccess, vbInformation)
                End If
            Else
                cm.ExecuteNonQuery()
            End If
            cm.Dispose()

        'Catch ex As Exception
        '    cn.Close()
        '    MsgBox(ex.Message, vbCritical)
        'End Try
    End Sub

    Public Sub mydelete(ByVal sql As String, ByVal msg_boolean As Boolean, ByVal msgSuccess As String, ByVal msgFailed As String)
        Try


            cm = New MySqlCommand(sql, cn)
            If msg_boolean = True Then
                result = cm.ExecuteNonQuery
                If result = 0 Then
                    MsgBox(msgFailed, vbCritical)
                Else
                    MsgBox(msgSuccess, vbInformation)
                End If
            Else
                cm.ExecuteNonQuery()
            End If
            cm.Dispose()

        Catch ex As Exception
            cn.Close()
            MsgBox(ex.Message, vbCritical)
        End Try
    End Sub

    Public Sub fillCombo(ByVal sql As String, ByVal combo_box As Object, ByVal table As String, ByVal dmember As String, ByVal vmember As String)
        'Try
        cn.Close()
            cn.Open()
            Dim dtc As DataTableCollection
            ds = New DataSet
            dtc = ds.Tables
            da = New MySqlDataAdapter(sql, cn)
            da.Fill(ds, table)
            Dim view1 As New DataView(dtc(0))
            With combo_box
                .DataSource = ds.Tables(table)
                .DisplayMember = dmember
                .ValueMember = vmember
                .AutoCompleteSource = AutoCompleteSource.ListItems
                .AutoCompleteMode = AutoCompleteMode.SuggestAppend
            End With
            cn.Close()
        'Catch ex As Exception
        '    cn.Close()
        '    MsgBox(ex.Message, vbCritical)
        'End Try
    End Sub

    Public Sub load_datagrid(ByVal sql As String, ByVal DTG As Object)
        Try
            CloseDBConnection()
            OpenDBConnection()
            dt = New DataTable
            With cm
                .Connection = cn
                .CommandText = sql
            End With
            da.SelectCommand = cm
            da.Fill(dt)
            DTG.DataSource = dt
            da.Dispose()
        Catch ex As Exception

        End Try
        CloseDBConnection()
    End Sub

    Public Sub load_data(ByVal sql As String, ByVal table As String)
        cn.Close()
        cn.Open()
        da = New MySqlDataAdapter(sql, cn)
        cm = New MySqlCommand(sql)
        da.Fill(ds, table)
        cn.Close()
    End Sub

End Module

