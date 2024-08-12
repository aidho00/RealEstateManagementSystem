Imports MySql.Data.MySqlClient
Imports System.Net

Module Connection
    Public cn As New MySqlConnection
    Public cm As New MySqlCommand

    Public str_user, str_pass, str_name, str_role, str_company, str_address, str_h1, str_h2, str_h3 As String
    Public startid As String

    Public strHostName As String = System.Net.Dns.GetHostName()
    Public strIPAddress As String = System.Net.Dns.GetHostEntry(System.Net.Dns.GetHostName()).AddressList.Where(Function(a As IPAddress) Not a.IsIPv6LinkLocal AndAlso Not a.IsIPv6Multicast AndAlso Not a.IsIPv6SiteLocal).First().ToString()

    Sub OpenDBConnection()

        'Dim dbhost As String = "127.0.0.1"
        'Dim dbuser As String = "root"
        'Dim dbpass As String = ""

        Dim dbhost As String = "10.0.1.2"
        Dim dbuser As String = "server"
        Dim dbpass As String = "cronasia"

        Dim dbport As String = "3306"
        Dim dbname As String = "remdb"

        'Dim dbname As String = "remdbdummy"

        cn = New MySqlConnection

            With cn
                .ConnectionString = "Data Source='" & dbhost & "'; Database='" & dbname & "'; User='" & dbuser & "'; Password='" & dbpass & "'; Port='" & dbport & "'; Convert Zero Datetime=True"
            End With

    End Sub
    Sub CloseDBConnection()
        cm.Dispose()
        cn.Close()
    End Sub
End Module
