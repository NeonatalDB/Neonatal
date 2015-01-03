Imports System.Web
Imports System.Data
Imports System.Data.SqlClient
Imports System.Data.OleDb

Partial Class MobLogon
    Inherits System.Web.UI.Page
    Dim strconn As String = ConfigurationManager.ConnectionStrings("NeonatalConnectionString").ConnectionString
    Dim conn As New SqlClient.SqlConnection(strconn)


    Protected Sub Logon_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Logon.Click

        Dim strSQL As String = "select * from Neostaff where usercode = '" & username.Text & "' and passw = '" & password.Text & "'"
        Dim adp As SqlClient.SqlDataAdapter = New SqlClient.SqlDataAdapter(strSQL, conn)
        Dim dataset As New DataSet

        adp.Fill(dataset)
        If dataset.Tables(0).Rows.Count = 1 Then
            Dim Userdetails As DataRow = dataset.Tables(0).Rows(0)
                Session("sessionusername") = Userdetails.Item("usercode")
                Session("sessionhospid") = Userdetails.Item("HospitalID")
                Session("sessionadmin") = Userdetails.Item("menuserveradmin")
                Session("sessionauditOfficer") = Userdetails.Item("menuaudit")
                Session("sessionstaffid") = Userdetails.Item("StaffID")
                'Session("sessionhospname") = Userdetails.Item("HospName")
                Session("sessionNursingNotes") = Userdetails.Item("MobNursingNotes")
                Session("sessioncomment") = Userdetails.Item("MobComment")

'Audit trail
                FormsAuthentication.RedirectFromLoginPage(Userdetails.Item("StaffName"), False)
        End If

    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If conn.State = ConnectionState.Closed Then
            conn.Open()
        End If
        Session("sessionusername") = ""
        Session("sessionhospid") = ""
        Session("sessionhospidencoded") = ""
        Session("sessionhospname") = "[awaiting hospital]"
        Session("sessionadmin") = 0
        Session("sessionstaffid") = ""
        Session("sessionstaffemail") = ""
        Session("sessionauditOfficer") = 0
        Session("sessionNICUSALL") = 0
        Session("sessionReportMenu") = 0
        username.Focus()
    End Sub
End Class
