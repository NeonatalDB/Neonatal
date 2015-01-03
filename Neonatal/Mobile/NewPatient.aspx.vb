Imports System.Web
Imports System.Data
Imports System.Data.SqlClient
Imports System.Data.OleDb
Partial Class Mobile_NewPatient
    Inherits System.Web.UI.Page
    'Define the connection string 
    Dim strconn As String = ConfigurationManager.ConnectionStrings("NeonatalConnectionString").ConnectionString
    Dim conn As New SqlClient.SqlConnection(strconn)
    Protected Sub page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If conn.State = ConnectionState.Closed Then
            conn.Open()
        End If
    End Sub
    Protected Sub Page_Init(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Init
        On Error Resume Next
        DOB.Text = Format(Now(), "yyyy MMM dd  HH00")
        Admitdate.Text = Format(Now(), "yyyy MMM dd  HH00")
    End Sub

    Protected Sub submit_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles submit.Click

        Dim strSurname, strDOB, strGA, strGADays, strBW, strMGest, strBed, strConsultant, strAdmitDate, strGender, strMRN
        strSurname = UCase(Surname.Text)
        strBed = Bed.Text
        strGA = GA.Text
        strBW = BW.Text
        strMGest = mgest.text
        strConsultant = Consultant.Text
	strGender = Gender.text
	strMRN = MRN.Text
        If Len(GADays.Text) > 0 Then
            strGADays = GADays.Text
        Else
            strGADays = "Null"
        End If

        strDOB = DOB.Text
        If Not IsDate(DOB.Text) And Len(strDOB) > 5 Then
            strDOB = Left(strDOB, Len(strDOB) - 2) + ":" + Right(strDOB, 2)
        End If

        strAdmitDate = Admitdate.Text
        If Not IsDate(Admitdate.Text) And Len(strAdmitDate) > 5 Then
            strAdmitDate = Left(strAdmitDate, Len(strAdmitDate) - 2) + ":" + Right(strAdmitDate, 2)
        End If

        'Dim strSQL As String = "exec procPatientMobileNew 'D209', 'Callander', '1/7/2012', 26, 2, 1100, 1, 'B1Bed1', 'Charles', '1/7/2012'"
        Dim strSQL As String = "exec procPatientMobileNew 'D209', '" & strSurname & "', '" & strDOB & "', " & strGA & ", " & strGADays & ", " & strBW & ", " & strMGest & ", '" & strBed & "', '" & strConsultant & "', '" & strAdmitDate & "', " & strGender & ", '" & strMRN & "'" 

        Dim adp As SqlClient.SqlDataAdapter = New SqlClient.SqlDataAdapter(strSQL, conn)
        Dim dataset As New DataSet
        adp.Fill(dataset)
        If dataset.Tables(0).Rows.Count <> 0 Then
            Dim DataRow As DataRow = dataset.Tables(0).Rows(0)
            If IsDBNull(DataRow.Item("neoid")) = False Then
                Response.Redirect("Patient.aspx?@neoid=" & DataRow.Item("neoid") & "&@hospid=D209")
            End If
        End If
    End Sub
End Class
