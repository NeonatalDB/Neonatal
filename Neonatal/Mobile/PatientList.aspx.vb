
Imports System.Web
Imports System.Data
Imports System.Data.SqlClient
Imports System.Data.OleDb


Partial Class Mobile_PatientList
    Inherits System.Web.UI.Page
    ''Define the connection string 
    Dim strconn As String = ConfigurationManager.ConnectionStrings("NeonatalConnectionString").ConnectionString
    Dim conn As New SqlClient.SqlConnection(strconn)

    Protected Sub Page_Init(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Init
        '    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        HospList.SelectedValue = Session("sessionhospid")
        If Session("sessionNursingNotes") = -1 Then
            NursingNotes.Checked = True
        End If
        If Session("sessioncomment") = -1 Then
            Comment.Checked = True
        End If
        If Session("sessionusername") = "callai" or Session("sessionusername") = "huacar" Then
            Newbaby.visible = True
        End If
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If conn.State = ConnectionState.Closed Then
            conn.Open()
        End If
        If Len(Session("sessionhospid")) = 0 Then
            panel1.Visible = False
        Else
            Dim StrBedstate
            StrBedstate = ""
            Dim Newdataset As New PatientTableAdapters.procPatientNumbersTableAdapter
            Dim Bedstate As Patient.procPatientNumbersDataTable
            If Len(HospList.SelectedValue) > 0 Then
                Bedstate = Newdataset.GetData(HospList.SelectedValue)
            Else
                Bedstate = Newdataset.GetData(Session("sessionhospid"))
            End If
            For Each bedrow As Patient.procPatientNumbersRow In Bedstate
                If bedrow.NICU > 0 Then
                    StrBedstate = CStr(bedrow.NICU) & "NICU + " & CStr(bedrow.Babies - bedrow.NICU) & "SCN = "
                End If
                StrBedstate = StrBedstate & CStr(bedrow.Babies) & "Total"
            Next

            Label1.Text = StrBedstate 'Format(TimeOfDay(), "hh:mm on ") & Today()
            Dim Newdataset1 As New StorProcDataTableAdapters.procInfHospInpatientList2TableAdapter
            '            GridView1.DataSource = Newdataset1.GetData(Session("sessionhospid"))
            If Len(HospList.SelectedValue) > 0 Then
                GridView1.DataSource = Newdataset1.GetData(HospList.SelectedValue)
                'GridView2.DataSource = Newdataset1.GetData(HospList.SelectedValue)
            Else
                GridView1.DataSource = Newdataset1.GetData(Session("sessionhospid"))
                'GridView2.DataSource = Newdataset1.GetData(Session("sessionhospid"))
            End If
            GridView1.DataBind()
            'GridView2.DataBind()

            Dim strSQL As String = "SELECT convert(nvarchar(50), patientid) as patientid, location + case consultation when 1 then ' #' when 2 then ' +' else '  ' end as Location, Consultant, MRN, [Name], case when dob <= getdate() then 'Day ' + convert(nvarchar, datediff(d, DOB, getdate())) when DOB > getdate() then convert(nchar(2),40 - datediff(d, getdate(), dob)/7) + '/40' else '' end as Age, Diagnosis, Progress FROM Outliers where hospitalid = '" & Session("sessionhospid") & "' and inactive = 0 and [print] = -1 order by dob, createdate desc"
            Dim adp As SqlClient.SqlDataAdapter = New SqlClient.SqlDataAdapter(strSQL, conn)
            Dim dataset As New DataSet
            adp.Fill(dataset)
            If dataset.Tables(0).Rows.Count <> 0 Then
                Outlier.DataSource = dataset
                Outlier.DataBind()
            End If

            'LatestResults
            strSQL = "exec procPatientMobResults '" & Session("sessionhospid") & "', '" & freq.SelectedValue & "'"
            adp = New SqlClient.SqlDataAdapter(strSQL, conn)
            dataset = New DataSet
            adp.Fill(dataset)
            If dataset.Tables(0).Rows.Count <> 0 Then
                LatestResults.Visible = True
                LatestResults.DataSource = dataset
                LatestResults.DataBind()
            Else
                LatestResults.Visible = False
            End If

        End If



    End Sub
    Protected Sub HighlightToday_RowDataBOund(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs)
        If e.Row.RowType = DataControlRowType.DataRow Then
            Dim Holder = DataBinder.Eval(e.Row.DataItem, "Day")
            If IsNumeric(Left(Holder, 1)) Then
                e.Row.Cells(5).ForeColor = Drawing.Color.Chocolate
                e.Row.Cells(6).ForeColor = Drawing.Color.Chocolate
                e.Row.Cells(7).ForeColor = Drawing.Color.DarkBlue
                e.Row.Cells(8).ForeColor = Drawing.Color.DarkBlue
                e.Row.Cells(9).ForeColor = Drawing.Color.Chocolate 'Goldenrod
                e.Row.Cells(10).ForeColor = Drawing.Color.Chocolate
                'e.Row.ForeColor = Drawing.Color.DarkSlateBlue '.DarkSalmon '.DarkViolet 
            Else
                e.Row.ForeColor = Drawing.Color.Gray '.DarkGray  '.DarkKhaki '.DarkSalmon '.DarkViolet 
                'e.Row.Cells(7).ForeColor = Drawing.Color.LightSkyBlue
                'e.Row.Cells(8).ForeColor = Drawing.Color.LightSkyBlue
            End If
            'e.Row.Cells(7).ForeColor = Drawing.Color.DarkBlue
            'e.Row.Cells(8).ForeColor = Drawing.Color.DarkBlue
            Holder = DataBinder.Eval(e.Row.DataItem, "Hb")
            If IsDBNull(Holder) Then
            Else
                If Holder < 100 Then
                    e.Row.Cells(5).ForeColor = Drawing.Color.Red
                    e.Row.Cells(5).Font.Bold = True
                End If
            End If

            Holder = DataBinder.Eval(e.Row.DataItem, "Plat")
            If IsDBNull(Holder) Then
            Else
                If Holder < 100 Then
                    e.Row.Cells(6).ForeColor = Drawing.Color.Red
                    e.Row.Cells(6).Font.Bold = True
                End If
            End If

            Holder = DataBinder.Eval(e.Row.DataItem, "Sodium")
            If IsDBNull(Holder) Then
            Else
                If Holder <= 132 Or Holder >= 150 Then
                    e.Row.Cells(7).ForeColor = Drawing.Color.Red
                    e.Row.Cells(7).Font.Bold = True
                End If
            End If

            Holder = DataBinder.Eval(e.Row.DataItem, "Creat")
            If IsDBNull(Holder) Then
            Else
                If Holder >= 80 Then
                    e.Row.Cells(8).ForeColor = Drawing.Color.Red
                    e.Row.Cells(8).Font.Bold = True
                End If
            End If

            Holder = DataBinder.Eval(e.Row.DataItem, "SBr")
            Dim strGA = DataBinder.Eval(e.Row.DataItem, "GA")
            If IsDBNull(Holder) Or IsDBNull(strGA) Then
            Else
                If Holder >= 10 * strGA - 100 Then
                    e.Row.Cells(9).ForeColor = Drawing.Color.Red
                    e.Row.Cells(9).Font.Bold = True
                End If
            End If
        End If
    End Sub

    Protected Sub GridView1_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles GridView1.RowCommand
        If e.CommandName.CompareTo("View") = 0 Then
            Dim ReportID As String = GridView1.DataKeys(Convert.ToInt32(e.CommandArgument)).Value
            Dim Newdataset As New PatientTableAdapters.procPatientAccessMobile1TableAdapter
            Dim NewRow As Patient.procPatientAccessMobile1DataTable
            NewRow = Newdataset.GetData(Session("sessionusername"), ReportID)
            Dim AccessAllowed As Patient.procPatientAccessMobile1Row
            AccessAllowed = NewRow.Rows.Item(0)
            'Label1.Text = AccessAllowed.Allowed
            If AccessAllowed.Allowed = "yes" Or Session("sessionadmin") = -1 Then
                Response.Redirect("Patient.aspx?@neoid=" & ReportID & "&@hospid=" & HospList.SelectedValue)
            Else
                System.Web.HttpContext.Current.Response.Write("<SCRIPT LANGUAGE=""JavaScript"">" & vbCrLf)
                System.Web.HttpContext.Current.Response.Write("alert('You do not have permission to view this patient - you can ask another hospital to nominate a transfer hospital')" & vbCrLf)
                System.Web.HttpContext.Current.Response.Write("</SCRIPT>")
            End If
        End If
    End Sub

    Protected Sub GridView2_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles GridView2.RowCommand
        Dim CommentID As String = GridView2.DataKeys(Convert.ToInt32(e.CommandArgument)).Value
        Dim strSQL, strJob
        If e.CommandName.CompareTo("Remove") = 0 Then
            strSQL = "delete from mobilecomment where commentid = '" & Trim(CommentID) & "'"
            strJob = New SqlClient.SqlCommand(strSQL, conn)
            strJob.ExecuteNonQuery()
            Response.Redirect("PatientList.aspx#Options")
        ElseIf e.CommandName.CompareTo("Go") = 0 Then
            strSQL = "select convert(nvarchar(50), m.neonatalid) as Neoid, hospitalid from mobilecomment m left outer join episode e on m.neonatalid = e.neonatalid and dischdate is null where commentid = '" & Trim(CommentID) & "'"
            Dim adp As SqlClient.SqlDataAdapter = New SqlClient.SqlDataAdapter(strSQL, conn)
            Dim dataset As New DataSet
            adp.Fill(dataset)
            If dataset.Tables(0).Rows.Count <> 0 Then
                Dim DataRow As DataRow = dataset.Tables(0).Rows(0)
                Response.Redirect("Patient.aspx?@neoid=" & DataRow.Item("neoid") & "&@hospid=" & DataRow.Item("hospitalid"))
            End If
        End If
    End Sub
    Protected Sub GridView3_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles GridView3.RowCommand
        Dim RowID As String = GridView3.DataKeys(Convert.ToInt32(e.CommandArgument)).Value
        Dim strSQL, strJob
        If e.CommandName.CompareTo("Remove") = 0 Then
            strSQL = "delete from mobilecomment where commentid = '" & Trim(RowID) & "'"
            strJob = New SqlClient.SqlCommand(strSQL, conn)
            strJob.ExecuteNonQuery()
            Response.Redirect("PatientList.aspx#Options")
        End If
    End Sub

    Protected Sub Outlier_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles Outlier.RowCommand
        Dim RowID As String = Outlier.DataKeys(Convert.ToInt32(e.CommandArgument)).Value

        'System.Web.HttpContext.Current.Response.Write("<SCRIPT LANGUAGE=""JavaScript"">" & vbCrLf)
        'System.Web.HttpContext.Current.Response.Write("alert('RowID is ')" & vbCrLf)
        'System.Web.HttpContext.Current.Response.Write("</SCRIPT>")


        Dim strSQL, strJob
        If e.CommandName.CompareTo("Remove") = 0 Then
            'strSQL = "select Location from Outliers where Patientid = '" & Trim(RowID) & "'"
            'Dim adp As SqlClient.SqlDataAdapter = New SqlClient.SqlDataAdapter(strSQL, conn)
            'Dim dataset As New DataSet
            'adp.Fill(dataset)
            'If dataset.Tables(0).Rows.Count <> 0 Then
            '    Dim DataRow As DataRow = dataset.Tables(0).Rows(0)
            '    'If IsDBNull(DataRow.Item("perihx")) = False Then
            '    Location.Text = DataRow.Item("Location")
            '    'End If
            'End If

            strSQL = "update outliers set inactive = -1 where Patientid = '" & Trim(RowID) & "'"
            strJob = New SqlClient.SqlCommand(strSQL, conn)
            strJob.ExecuteNonQuery()

            Response.Redirect("PatientList.aspx#Options")
        End If
    End Sub

    'Protected Sub configuration_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles configuration.Click
    '    System.Web.HttpContext.Current.Response.Write("<SCRIPT LANGUAGE=""JavaScript"">" & vbCrLf)
    '    System.Web.HttpContext.Current.Response.Write("alert('When the amount of clinical information that is available increases enough, I will create a user configuration for the default view')" & vbCrLf)
    '    System.Web.HttpContext.Current.Response.Write("</SCRIPT>")
    'End Sub

    Protected Sub NursingNotes_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles NursingNotes.CheckedChanged
        Dim strSQL, strJob
        If NursingNotes.Checked = True Then
            Session("sessionNursingNotes") = -1
            strSQL = "update neostaff set MobNursingNotes = -1 where usercode = '" & Session("sessionusername") & "'"
            strJob = New SqlClient.SqlCommand(strSQL, conn)
            strJob.ExecuteNonQuery()

        Else
            Session("sessionNursingNotes") = 0
            strSQL = "update neostaff set MobNursingNotes = 0 where usercode = '" & Session("sessionusername") & "'"
            strJob = New SqlClient.SqlCommand(strSQL, conn)
            strJob.ExecuteNonQuery()
        End If
    End Sub
    Protected Sub Comment_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles Comment.CheckedChanged
        Dim strSQL, strJob
        If Comment.Checked = True Then
            Session("sessioncomment") = -1
            strSQL = "update neostaff set Mobcomment = -1 where usercode = '" & Session("sessionusername") & "'"
            strJob = New SqlClient.SqlCommand(strSQL, conn)
            strJob.ExecuteNonQuery()

        Else
            Session("sessioncomment") = 0
            strSQL = "update neostaff set Mobcomment = 0 where usercode = '" & Session("sessionusername") & "'"
            strJob = New SqlClient.SqlCommand(strSQL, conn)
            strJob.ExecuteNonQuery()
        End If
    End Sub

    Protected Sub RPA_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles RPA.Click
        Response.Redirect("http://10.202.1.3/RPA/PatientList.aspx")
    End Sub

    Protected Sub AddComment_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles AddComment.Click
        If Len(Session("sessionusername")) > 1 And Len(NewComment.Text) > 0 Then
            Dim strSQL, strJob
            strSQL = "insert into mobilecomment (staffid, comment) select staffid, '" & NewComment.Text & "' from neostaff where usercode = '" & Session("sessionusername") & "'"
            strJob = New SqlClient.SqlCommand(strSQL, conn)
            strJob.ExecuteNonQuery()
            Response.Redirect("PatientList.aspx#Options")
        End If
    End Sub

    Protected Sub LatestResults_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles LatestResults.RowCommand
        If e.CommandName.CompareTo("View") = 0 Then
            Dim ReportID As String = LatestResults.DataKeys(Convert.ToInt32(e.CommandArgument)).Value
            Dim Newdataset As New PatientTableAdapters.procPatientAccessMobile1TableAdapter
            Dim NewRow As Patient.procPatientAccessMobile1DataTable
            NewRow = Newdataset.GetData(Session("sessionusername"), ReportID)
            Dim AccessAllowed As Patient.procPatientAccessMobile1Row
            AccessAllowed = NewRow.Rows.Item(0)
            'Label1.Text = AccessAllowed.Allowed
            If AccessAllowed.Allowed = "yes" Or Session("sessionadmin") = -1 Then
                Response.Redirect("Patient.aspx?@neoid=" & ReportID & "&@hospid=" & HospList.SelectedValue)
            Else
                System.Web.HttpContext.Current.Response.Write("<SCRIPT LANGUAGE=""JavaScript"">" & vbCrLf)
                System.Web.HttpContext.Current.Response.Write("alert('You do not have permission to view this patient - you can ask another hospital to nominate a transfer hospital')" & vbCrLf)
                System.Web.HttpContext.Current.Response.Write("</SCRIPT>")
            End If
        End If
    End Sub

    Protected Sub NewComment_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles NewComment.TextChanged
        'If Len(Session("sessionusername")) > 1 And Len(NewComment.Text) > 0 Then
        '    Dim strSQL, strJob
        '    strSQL = "insert into mobilecomment (staffid, comment) select staffid, '" & NewComment.Text & "' from neostaff where usercode = '" & Session("sessionusername") & "'"
        '    strJob = New SqlClient.SqlCommand(strSQL, conn)
        '    strJob.ExecuteNonQuery()
        'End If
    End Sub

    Protected Sub NewBaby_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles NewBaby.Click
        Response.Redirect("NewPatient.aspx")
    End Sub


    'Protected Sub OutlierSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles OutlierSave.Click
    '    Diagnosis.Text = "Hello"
    'End Sub
End Class
