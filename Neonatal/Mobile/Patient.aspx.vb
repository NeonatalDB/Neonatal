Imports System.Web
Imports System.Data
Imports System.Data.SqlClient
Imports System.Data.OleDb
'Imports System.DateTimeOffset

Partial Class Mobile_Patient
    Inherits System.Web.UI.Page
    ''Define the connection string 
    Dim strconn As String = ConfigurationManager.ConnectionStrings("NeonatalConnectionString").ConnectionString
    Dim conn As New SqlClient.SqlConnection(strconn)

    Protected Sub page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If conn.State = ConnectionState.Closed Then
            conn.Open()
        End If
        Dim strNeoid, strSQL, strJob
        strNeoid = Request.QueryString("@neoid")
        If Len(Session("sessionusername")) > 0 Then  'error message that connection is closed
            strSQL = "exec procMobileAudit '" & Session("sessionusername") & "', 'Mobile PatientView', '" & strNeoid & "'"
            strJob = New SqlClient.SqlCommand(strSQL, conn)
            strJob.ExecuteNonQuery()
        Else
            Response.Redirect("MobLogon.aspx?ReturnUrl=PatientList.aspx")
        End If
    End Sub

    Protected Sub Page_Init(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Init
        On Error Resume Next
        If Session("sessionusername") = "callai" Then
            O2SatStudy.Visible = "true"
        End If
        load_lists()
        PatientList.Text = "List - " & Left(Today(), 6) & Right(Today(), 2)
        PatientList2.Text = "List - " & Left(Today(), 6) & Right(Today(), 2)
        If SBr.Text = "Done" Then
            SBrDate.Focus() ' Not working
        End If
        Dim strNeoid
        strNeoid = Request.QueryString("@neoid")

        SBrDate.Text = Format(Now(), "yyyy MMM dd  HH00")
        WgtDate.Text = Format(Now(), "yyyy MMM dd")
        DischDate.Text = Format(Now(), "yyyy MMM dd  HH00")
        ResultsDate.Text = Format(Now(), "yyyy MMM dd  HH00")
        FirstExpress.Text = Format(Now(), "yyyy MMM dd  HH00")

        Dim strSQL As String = "exec procPatientMobPeri '" & strNeoid & "'"
        Dim adp As SqlClient.SqlDataAdapter = New SqlClient.SqlDataAdapter(strSQL, conn)
        Dim dataset As New DataSet
        adp.Fill(dataset)
        If dataset.Tables(0).Rows.Count <> 0 Then
            Dim DataRow As DataRow = dataset.Tables(0).Rows(0)
            If IsDBNull(DataRow.Item("perihx")) = False Then
                PeriHx.Text = Replace(DataRow.Item("perihx"), vbNewLine, "<br>")
            End If
        End If

        Imaging.Visible = False
        Dim u As String = Request.ServerVariables("HTTP_USER_AGENT")
        Dim b As New Regex("MSIE|iexpl", RegexOptions.IgnoreCase)
        If b.IsMatch(u) Then
            strSQL = "select Userdef1 from PMI where neonatalid = '" & strNeoid & "'"
            adp = New SqlClient.SqlDataAdapter(strSQL, conn)
            dataset = New DataSet
            adp.Fill(dataset)
            If dataset.Tables(0).Rows.Count <> 0 Then
                Dim DataRow As DataRow = dataset.Tables(0).Rows(0)
                If IsNumeric(DataRow.Item("Userdef1")) Then
                    Imaging.Visible = True
                End If
            End If
        End If

        strSQL = "SELECT Catheter FROM Catheters WHERE NeonatalID = '" & strNeoid & "' AND DateOut IS NULL"
        adp = New SqlClient.SqlDataAdapter(strSQL, conn)
        dataset = New DataSet
        adp.Fill(dataset)
        If dataset.Tables(0).Rows.Count = 0 Then
            StopCatheter.Visible = False
        End If
        '

        strSQL = "exec procPatientMobResp '" & strNeoid & "'"
        adp = New SqlClient.SqlDataAdapter(strSQL, conn)
        dataset = New DataSet
        adp.Fill(dataset)
        If dataset.Tables(0).Rows.Count <> 0 Then
            Dim strCircuit
            Dim DataRow As DataRow = dataset.Tables(0).Rows(0)
            If IsDBNull(DataRow.Item("circuit")) Then
                Circuit.Text = Format(DateAdd("d", 6, Now()), "yyyy MMM dd")
                Circuit.ForeColor = Drawing.Color.Gray
                strCircuit = ""
            Else
                Circuit.Text = Format(DataRow.Item("circuit"), "yyyy MMM dd")
                If DataRow.Item("circuit") > Now() Then
                    strCircuit = "<br> Circuit due " & Format(DataRow.Item("circuit"), "MMM dd (") & CStr(DateDiff("d", Now(), DataRow.Item("circuit")) + 1) & " d)"
                Else
                    strCircuit = "<br> Circuit changed " & Format(DataRow.Item("circuit"), "MMM dd")
                End If
            End If
            RespSupp.Text = Replace(DataRow.Item("respsupp"), vbNewLine, "<br>") & strCircuit

            If Len(DataRow.Item("respsupp")) < 3 Then
                RespChart.Visible = False
            End If
        Else
            RespChart.Visible = False
        End If
        strSQL = "exec procPatientMobNutr '" & strNeoid & "'"
        adp = New SqlClient.SqlDataAdapter(strSQL, conn)
        dataset = New DataSet
        adp.Fill(dataset)
        If dataset.Tables(0).Rows.Count <> 0 Then
            Dim DataRow As DataRow = dataset.Tables(0).Rows(0)
            Nutrition.Text = Replace(DataRow.Item("nutrition"), vbNewLine, "<br>")
        End If

        strSQL = "select comment from mobilecomment m inner join neostaff n on m.staffid = n.staffid where neonatalid = '" & strNeoid & "' and usercode = '" & Session("sessionusername") & "'"
        adp = New SqlClient.SqlDataAdapter(strSQL, conn)
        dataset = New DataSet
        adp.Fill(dataset)
        If dataset.Tables(0).Rows.Count <> 0 Then
            Dim DataRow As DataRow = dataset.Tables(0).Rows(0)
            UserComment.Text = DataRow.Item("comment")
        End If

        'Seed Patient Data from procPatientMobile
        Dim Newdataset As New PatientTableAdapters.procPatientMobileTableAdapter
        Dim Baby As Patient.procPatientMobileDataTable
        Baby = Newdataset.GetData(strNeoid)
        Dim neo As Patient.procPatientMobileRow

        If Baby.Rows.Count <> 0 Then
            neo = Baby.Rows.Item(0)
            Label1.Text = neo.Line1
            Label2.Text = neo.Line2
            MedNote.Text = "Note: - " & neo.MedNote & "<br/>"
            MedNote2.Text = neo.MedNote
            MedOrders.Text = "Orders: - " & neo.MedOrders
            MedOrders2.Text = neo.MedOrders
            Suburb.Text = neo.suburb
            MilkHolder.Text = neo.Milk
            If CInt(neo.Milk) > 0 Then
                'ml.Text = neo.ml
                'Hrly.Text = neo.hrly
                Milk.Text = neo.Milk
                'MilkRoute.Text = neo.MilkRoute
                'Cal.Text = neo.Cal
                'Else
                '    ml.Visible = False
                '    Hrly.Visible = False
                '    'Milk.Visible = False Need to be able to start feeds
                '    MilkRoute.Visible = False
                '    Cal.Visible = False
                '    mlLabel.Visible = False
            End If
            If CInt(neo.ml) > 0 Then
                ml.Text = neo.ml
            End If
            If CInt(neo.hrly) >= 0 Then
                Hrly.Text = neo.hrly
            End If
            If CInt(neo.MilkRoute) > 0 Then
                MilkRoute.Text = neo.MilkRoute
            End If
            If CInt(neo.Cal) > 0 Then
                Cal.Text = neo.Cal
            End If

            ConfirmLabel.Text = "Intent?"
            BIntent.SelectedValue = ""
            'If CInt(neo.BIntent) >= 0 Then
            'BIntent.SelectedValue = neo.BIntent
            Select Case neo.BIntent
                Case 1
                    ConfirmLabel.Text = "Bottle"
                    BIntent.SelectedValue = "1"
                Case 2
                    ConfirmLabel.Text = "Both"
                    BIntent.SelectedValue = "2"
                Case 3
                    ConfirmLabel.Text = "Breast"
                    BIntent.SelectedValue = "3"
                Case Else
                    ConfirmLabel.Text = "Intent?"
                    BIntent.SelectedValue = ""
            End Select


            'FormulaEBM.Text = neo.FormulaEBM
            'DonorMother.Text = neo.DonorMother
            ''neo.NippleShield
            ''End If


            If neo.Volume >= 30 And neo.Volume <= 200 Then
                Volume.Text = CInt(neo.Volume / 10) * 10
            End If
            If Len(neo.FiO2) = 0 Then
                FiO2.Visible = False
                FiO2label.Visible = False
            Else
                FiO2.Visible = True
                FiO2.Text = neo.FiO2
                FiO2label.Visible = True
            End If
            FiO2b.Text = neo.FiO2
            PipData.Text = neo.PIP
            MAPData.Text = neo.MAP
            VgData.Text = neo.VG
            RateData.Text = neo.Rate
            FlowData.Text = neo.Flow
            RespSuppChange.Text = neo.RespSupp
            respsuppholder.Text = neo.RespSupp
            If IsDBNull(neo.FirstExpress) Then
                FirstExpress.ForeColor = Drawing.Color.Gray
            Else
                FirstExpress.Text = Format(neo.FirstExpress, "yyyy MMM dd  HHmm") 'ianc
                FirstExpress.ForeColor = Drawing.Color.Black
            End If
            'AC SIMV
            Dim StrInsurance
            Select Case neo.Elect
                Case 0
                    StrInsurance = ""
                Case 1
                    StrInsurance = " (P)"
                Case 2
                    StrInsurance = " (H)"
                Case 5
                    StrInsurance = " (S)"
                Case 6
                    StrInsurance = " (OS)"
                Case Else
                    StrInsurance = " (?)"
            End Select


            Consultant.Text = "AMO: " & neo.Consultant & StrInsurance
            If Not IsDBNull(neo.Elect) Then
                Insurance.SelectedValue = neo.Elect
            End If
            Dim strNurseSummary, strDiagnosisComment
            strNurseSummary = neo.NurseSummary
            strDiagnosisComment = neo.diagnosiscomment
            If Session("sessionNursingNotes") = -1 Then
                NurseSummary.Visible = True
                NursingLabel.Visible = True
                NursingLabel2.Visible = True
                NursingAdd.Visible = True
                diagnosiscommentLabel.Visible = True
                EditDiagnosisComment.Visible = True
                ShowNursing.Visible = False
            End If
            If Len(strNurseSummary) > 3 Then
                'strNurseSummary = Replace(strNurseSummary, vbNewLine, " <br> ")
                'strNurseSummary = Replace(strNurseSummary, " <br>  <br>  <br>  <br> ", " <br> ")
                'strNurseSummary = Replace(strNurseSummary, " <br>  <br> ", " <br> ")
                'strNurseSummary = Replace(strNurseSummary, " <br>  <br> ", " <br> ")
                NurseSummary.Text = strNurseSummary
                diagnosiscomment.Text = strDiagnosisComment
                'strDiagnosisComment = Replace(strDiagnosisComment, vbCrLf, " <br/> ")
                'strDiagnosisComment = Replace(strDiagnosisComment, Environment.NewLine, " <br/> ")
                strDiagnosisComment = Replace(strDiagnosisComment, "\n", " <br/> ")
                diagnosiscommentLabel.Text = Replace(strDiagnosisComment, vbNewLine, " <br/> ")
            Else
                'ShowNursing.Visible = False
            End If
            If Session("sessionComment") = -1 Then
                UserComment.Visible = True
                UsercommentLabel.Visible = True
                'ShowNursing.Visible = False
                Dim strComment
                strComment = ""
                If Len(strComment) > 3 Then
                    'Comment.Text = Replace(strNurseSummary, vbNewLine, " <br> ")
                Else
                    'UserComment.Visible = False
                End If
            End If
            'Request.QueryString("@hospid")
            If Len(neo.Bed) > 0 Then
                strSQL = "select * from [_Beds] where hospcode = '" & Request.QueryString("@hospid") & "' and bedname = '" & neo.Bed & "'"
                adp = New SqlClient.SqlDataAdapter(strSQL, conn)
                dataset = New DataSet
                adp.Fill(dataset)
                If dataset.Tables(0).Rows.Count <> 0 Then
                    BedList.SelectedValue = neo.Bed
                    BedList2.SelectedValue = neo.Bed
                    'BedList.SelectedValue = Request.QueryString("@neoid")
                End If
            End If
            If neo.Gender = 1 Then
                Label1.ForeColor = Drawing.Color.Navy
                Label2.ForeColor = Drawing.Color.Navy
                Consultant.ForeColor = Drawing.Color.Navy
                Suburb.ForeColor = Drawing.Color.Navy
            ElseIf neo.Gender = 2 Then
                Label1.ForeColor = Drawing.Color.Red
                Label2.ForeColor = Drawing.Color.Red
                Consultant.ForeColor = Drawing.Color.Red
                Suburb.ForeColor = Drawing.Color.Red
            Else
                Label1.ForeColor = Drawing.Color.DarkGreen
                Label2.ForeColor = Drawing.Color.DarkGreen
                Consultant.ForeColor = Drawing.Color.DarkGreen
                Suburb.ForeColor = Drawing.Color.DarkGreen
            End If
            If Len(neo.THospCode) > 0 Then
                THospList.SelectedValue = neo.THospCode
            End If
        End If

        strSQL = "exec procPatientMobItems '" & strNeoid & "'"
        adp = New SqlClient.SqlDataAdapter(strSQL, conn)
        dataset = New DataSet
        adp.Fill(dataset)
        If dataset.Tables(0).Rows.Count <> 0 Then
            Dim DataRow As DataRow = dataset.Tables(0).Rows(0)
            Results.Text = Replace(DataRow.Item("Results"), vbNewLine, "<br>")
            Jaundice.Text = Replace(DataRow.Item("Jaundice"), vbNewLine, "<br>")
            vaccination.Text = DataRow.Item("Vaccination")
            If Len(DataRow.Item("problems")) > 0 Then
                Problems.Text = Replace(DataRow.Item("problems"), vbNewLine, "<br>")
            Else
                InactivateProblem.Visible = False
            End If
            If Len(DataRow.Item("problems2")) > 0 Then
                Problems2.Text = Replace(DataRow.Item("problems2"), vbNewLine, "<br>")
            End If
            If Len(DataRow.Item("medications")) > 0 Then
                Medications.Text = Replace(DataRow.Item("medications"), vbNewLine, "<br>")
            Else
                'StopTreatment.Visible = False  'cant stop solo phototherapy otherwise
                'StopTreatmentLabel.Visible = False
            End If
            If InStr(DataRow.Item("Jaundice"), "SBr") = 0 Then
                ShowJaundiceChart.Visible = True
                JaundiceChart.Visible = False
            End If
            If CDate(neo.DOB) > DateAdd("d", -3, Today()) Then
                ShowGrowthChart.Visible = True
                GrowthChart1.Visible = False
            End If
            If InStr(DataRow.Item("results"), "Na") = 0 And InStr(DataRow.Item("results"), "BSL") = 0 Then
                ResultsChart1.Visible = False
            End If
            If InStr(DataRow.Item("results"), "Hb") = 0 And InStr(DataRow.Item("results"), "plat") = 0 Then
                resultsChart2.Visible = False
            End If
        End If

        'EpisodeID.Text = neo.EpisodeID

        'ConfirmLabel.Visible = False

        If Request.QueryString("@hospid") = Session("sessionhospid") Then
            Dim Newdataset2 As New PatientTableAdapters.procPatientNextTableAdapter
            Dim NewRow2 As Patient.procPatientNextDataTable
            NewRow2 = Newdataset2.GetData(strNeoid, Session("sessionhospid"))
            Dim AdjacentBed As Patient.procPatientNextRow
            AdjacentBed = NewRow2.Rows.Item(0)
            If Len(AdjacentBed.NextBed) > 0 Then
                NextBedLabel.Text = AdjacentBed.NextBed
            Else
                NextBed.Enabled = False
                NextBed2.Enabled = False
            End If
            If Len(AdjacentBed.PrevBed) > 0 Then
                PrevBedLabel.Text = AdjacentBed.PrevBed
            Else
                PrevBed.Enabled = False
                PrevBed2.Enabled = False
            End If
        Else
            PrevBed.Enabled = False
            NextBed.Enabled = False
            BedList.Enabled = False
            PrevBed2.Enabled = False
            NextBed2.Enabled = False
            BedList2.Enabled = False
        End If

        If neo.GA > 36 Then
            GrowthChart1.ChartAreas("ChartArea1").AxisX.Minimum = "35"
        ElseIf neo.GA > 32 Then
            GrowthChart1.ChartAreas("ChartArea1").AxisX.Minimum = "32"
        ElseIf neo.GA > 30 Then
            GrowthChart1.ChartAreas("ChartArea1").AxisX.Minimum = "30"
        ElseIf neo.GA > 28 Then
            GrowthChart1.ChartAreas("ChartArea1").AxisX.Minimum = "28"
        ElseIf neo.GA > 26 Then
            GrowthChart1.ChartAreas("ChartArea1").AxisX.Minimum = "26"
        Else
            GrowthChart1.ChartAreas("ChartArea1").AxisX.Minimum = "23"
        End If
        'growthchart1.
        GrowthChart1.Titles(1).Text() = "Last weight - " & Right(neo.Line2, 15)

        Resp_Parameters()

        'strSQL = "select ProblemID, [Problem#] as Problem, [Text1#] as Text from [problems#] where neonatalid = '" & strNeoid & "' and finish is null and not [Text1#] is null order by start desc"
        'adp = New SqlClient.SqlDataAdapter(strSQL, conn)
        'dataset = New DataSet
        'adp.Fill(dataset)
        'If dataset.Tables(0).Rows.Count <> 0 Then
        '    ProblemList.DataSource = dataset
        '    ProblemList.DataBind()
        'End If

        Dim audit As New NICUSTableAdapters.AuditWebTableAdapter
        Dim audit2 As Boolean
        audit2 = audit.InsertQuery(Session("sessionusername"), strNeoid)
    End Sub


    Protected Sub PatientList_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles PatientList.Click
        Response.Redirect("PatientList.aspx")
    End Sub
    Protected Sub PatientList2_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles PatientList2.Click
        Response.Redirect("PatientList.aspx")
    End Sub

    Protected Sub BedList_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles BedList.SelectedIndexChanged
        Dim strNeoid
        strNeoid = Request.QueryString("@neoid")
        Dim strSQL As String = "select convert(nvarchar(50),neonatalid) as Neonatalid from episode where DischDate is null and BedNo# = '" & BedList.SelectedValue & "' and Hospitalid = '" & Request.QueryString("@hospid") & "'"
        Dim adp As SqlClient.SqlDataAdapter = New SqlClient.SqlDataAdapter(strSQL, conn)
        Dim dataset As New DataSet

        adp.Fill(dataset)
        If dataset.Tables(0).Rows.Count <> 0 Then
            Dim Ian As DataRow = dataset.Tables(0).Rows(0)
            Response.Redirect("Patient.aspx?@neoid=" & Ian.Item("neonatalid") & "&@hospid=" & Request.QueryString("@hospid"))
        End If
    End Sub
    Protected Sub Insurance_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles Insurance.TextChanged
        If Len(Session("sessionusername")) > 0 Then
            If Len(Insurance.Text) > 0 Then
                Dim strNeoid
                strNeoid = Request.QueryString("@neoid")
                Dim strSQL As String = "update episode set [elect#] = " & Insurance.Text & " from episode where dischdate is null and Neonatalid = '" & strNeoid & "'"
                Dim strJob As SqlClient.SqlCommand = New SqlClient.SqlCommand(strSQL, conn)
                strJob.ExecuteNonQuery()
                strSQL = "exec procMobileAudit '" & Session("sessionusername") & "', 'Mobile Insurance', '" & strNeoid & "'"
                strJob = New SqlClient.SqlCommand(strSQL, conn)
                strJob.ExecuteNonQuery()
            End If
        Else
            Response.Redirect("MobLogon.aspx?ReturnUrl=PatientList.aspx")
        End If
    End Sub

    Protected Sub BedList3_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles BedList3.SelectedIndexChanged
        If Len(Session("sessionusername")) > 0 Then
            If Len(Insurance.Text) > 0 Then
                Dim strNeoid
                strNeoid = Request.QueryString("@neoid")
                Dim strSQL As String = "update episode set [BedNo#] = '" & BedList3.text & "' from episode where dischdate is null and Neonatalid = '" & strNeoid & "'"
                Dim strJob As SqlClient.SqlCommand = New SqlClient.SqlCommand(strSQL, conn)
                strJob.ExecuteNonQuery()
                strSQL = "exec procMobileAudit '" & Session("sessionusername") & "', 'Mobile BedMove', '" & strNeoid & "'"
                strJob = New SqlClient.SqlCommand(strSQL, conn)
                strJob.ExecuteNonQuery()
            End If
        Else
            Response.Redirect("MobLogon.aspx?ReturnUrl=PatientList.aspx")
        End If
    End Sub

    Protected Sub BedList2_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles BedList2.SelectedIndexChanged
        Dim strNeoid
        strNeoid = Request.QueryString("@neoid")
        Dim strSQL As String = "select convert(nvarchar(50),neonatalid) as Neonatalid from episode where DischDate is null and BedNo# = '" & BedList2.SelectedValue & "' and Hospitalid = '" & Request.QueryString("@hospid") & "'"
        Dim adp As SqlClient.SqlDataAdapter = New SqlClient.SqlDataAdapter(strSQL, conn)
        Dim dataset As New DataSet

        adp.Fill(dataset)
        If dataset.Tables(0).Rows.Count <> 0 Then
            Dim Ian As DataRow = dataset.Tables(0).Rows(0)
            Response.Redirect("Patient.aspx?@neoid=" & Ian.Item("neonatalid") & "&@hospid=" & Request.QueryString("@hospid"))
        End If
    End Sub

    Protected Sub PrevBed_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles PrevBed.Click
        Response.Redirect("Patient.aspx?@neoid=" & PrevBedLabel.Text & "&@hospid=" & Request.QueryString("@hospid"))
    End Sub
    Protected Sub PrevBed2_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles PrevBed2.Click
        Response.Redirect("Patient.aspx?@neoid=" & PrevBedLabel.Text & "&@hospid=" & Request.QueryString("@hospid"))
    End Sub

    Protected Sub NextBed_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles NextBed.Click
        Response.Redirect("Patient.aspx?@neoid=" & NextBedLabel.Text & "&@hospid=" & Request.QueryString("@hospid"))
    End Sub
    Protected Sub NextBed2_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles NextBed2.Click
        Response.Redirect("Patient.aspx?@neoid=" & NextBedLabel.Text & "&@hospid=" & Request.QueryString("@hospid"))
    End Sub

    Protected Sub ShowGrowthChart_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ShowGrowthChart.Click
        GrowthChart1.Visible = True
        ShowGrowthChart.Visible = False
    End Sub
    Protected Sub ShowJaundiceChart_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ShowJaundiceChart.Click
        JaundiceChart.Visible = True
        ShowJaundiceChart.Visible = False
    End Sub
    Protected Sub ShowNursing_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ShowNursing.Click
        NurseSummary.Visible = True
        NursingLabel.Visible = True
        NursingLabel2.Visible = True
        NursingAdd.Visible = True
        diagnosiscommentLabel.Visible = True
        EditDiagnosisComment.visible = True
        ShowNursing.Visible = False
    End Sub
    Protected Sub O2SatSubmit_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles O2SatSubmit.Click
        Dim strNeoid, strSQL, strJob, StrO2Last24Hr, StrTargetCard, StrSatRangeApprop, StrO2AnalyserApprop, StrSkinToSkinLast24Hr
        StrO2Last24Hr = O2Last24Hr.SelectedValue
        If Len(StrO2Last24Hr) = 0 Then
            StrO2Last24Hr = "null"
        End If
        StrTargetCard = TargetCard.SelectedValue
        If Len(StrTargetCard) = 0 Then
            StrTargetCard = "null"
        End If
        StrSatRangeApprop = SatRangeApprop.SelectedValue
        If Len(StrSatRangeApprop) = 0 Then
            StrSatRangeApprop = "null"
        End If
        StrO2AnalyserApprop = O2AnalyserApprop.SelectedValue
        If Len(StrO2AnalyserApprop) = 0 Then
            StrO2AnalyserApprop = "null"
        End If
        StrSkinToSkinLast24Hr = SkinToSkinLast24Hr.SelectedValue
        If Len(StrSkinToSkinLast24Hr) = 0 Then
            StrSkinToSkinLast24Hr = "null"
        End If

        strNeoid = Request.QueryString("@neoid")
        strSQL = "insert into liverpool.O2SatSurvey (Neonatalid, Date, O2Last24Hr, TargetCard, SatRangeApprop, O2AnalyserApprop, SkinToSkinLast24Hr, ReasonIncO2, Comments, usercode) values ('" & strNeoid & "', Getdate(), " & StrO2Last24Hr & ", " & StrTargetCard & ", " & StrSatRangeApprop & ", " & StrO2AnalyserApprop & ", " & StrSkinToSkinLast24Hr & ", '" & ReasonIncO2.Text & "', '" & Comments.Text & "', '" & Session("sessionusername") & "')"
        strJob = New SqlClient.SqlCommand(strSQL, conn)
        strJob.ExecuteNonQuery()

        Response.Redirect("Patient.aspx?@neoid=" & Request.QueryString("@neoid") & "&@hospid=" & Request.QueryString("@hospid"))

        'O2Last24Hr.SelectedValue = ""
        'TargetCard.SelectedValue = ""
        'SatRangeApprop.SelectedValue = ""
        'O2AnalyserApprop.SelectedValue = ""
        'SkinToSkinLast24Hr.SelectedValue = ""
        'ReasonIncO2.Text = ""
        'Comments.Text = ""
    End Sub

    Protected Sub Refresh_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Refresh.Click
        Response.Redirect("Patient.aspx?@neoid=" & Request.QueryString("@neoid") & "&@hospid=" & Request.QueryString("@hospid"))
    End Sub
    Protected Sub Refresh2_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Refresh2.Click
        Response.Redirect("Patient.aspx?@neoid=" & Request.QueryString("@neoid") & "&@hospid=" & Request.QueryString("@hospid"))
    End Sub
    Protected Sub SBrSubmit_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles SBrSubmit.Click
        Dim TempSBrDate
        TempSBrDate = SBrDate.Text
        If Not IsDate(SBrDate.Text) And Len(TempSBrDate) > 5 Then
            TempSBrDate = Left(TempSBrDate, Len(TempSBrDate) - 2) + ":" + Right(TempSBrDate, 2)
        End If
        If IsNumeric(SBr.Text) And IsDate(TempSBrDate) Then
            Dim strNeoid, strSQL, strJob
            strNeoid = Request.QueryString("@neoid")
            strSQL = "if not exists(select * from Bloodresults# where Neonatalid = '" & strNeoid & "' and date = '" & TempSBrDate & "') insert into Bloodresults# (NeonatalID, Date, SBr) values ('" & strNeoid & "', '" & TempSBrDate & "', " & SBr.Text & ")"
            strJob = New SqlClient.SqlCommand(strSQL, conn)
            strJob.ExecuteNonQuery()
            SBr.Text = "Done"
            SBrDate.Text = Format(Now(), "yyyy MMM dd  HH00")
            strSQL = "exec procMobileAudit '" & Session("sessionusername") & "', 'Mobile SBr', '" & strNeoid & "'"
            strJob = New SqlClient.SqlCommand(strSQL, conn)
            strJob.ExecuteNonQuery()
        End If
    End Sub

    Protected Sub SendResults_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles SendResults.Click
        Dim TempDate, StrNa, StrK, StrCreat, StrCRP, StrHb, StrWCC, StrPlat, StrRetic, StrBili, StrBSL, StrOther
        TempDate = ResultsDate.Text
        If Not IsDate(ResultsDate.Text) And Len(TempDate) > 5 Then
            TempDate = Left(TempDate, Len(TempDate) - 2) + ":" + Right(TempDate, 2)
        End If
	StrNa = Na.text
	if not isnumeric(StrNa) then
            StrNa = "Null"
	end if 
	StrK = K.text
	if not isnumeric(StrK) then
            StrK = "Null"
	end if
	StrCreat = Creat.text
	if not isnumeric(StrCreat) then
            StrCreat = "Null"
	end if
	StrCRP = CRP.text
	if not isnumeric(StrCRP) then
            StrCRP = "Null"
	end if
	StrHb = Hb.text
	if not isnumeric(StrHb) then
            StrHb = "Null"
	end if
	StrWCC = WCC.text
	if not isnumeric(StrWCC) then
            StrWCC = "Null"
	end if
	StrPlat = Plat.text
	if not isnumeric(StrPlat) then
            StrPlat = "Null"
	end if
	StrRetic = Retic.text
	if not isnumeric(StrRetic) then
            StrRetic = "Null"
	end if
	StrBili = Bili.text
	if not isnumeric(StrBili) then
            StrBili = "Null"
	end if
	StrBSL = BSL.text
	if not isnumeric(StrBSL) then
            StrBSL = "Null"
	end if
	StrOther = Other.text
        If Not Len(StrOther) > 0 Then
            StrOther = ""
        End If

        If IsDate(TempDate) Then
            Dim strNeoid, strSQL, strJob
            strNeoid = Request.QueryString("@neoid")
            'strSQL = "if not exists(select * from Bloodresults# where Neonatalid = '" & strNeoid & "' and date = '" & TempDate & "') insert into Bloodresults# (NeonatalID, Date, SBr) values ('" & strNeoid & "', '" & TempSBrDate & "', " & SBr.Text & ")"
            strSQL = "if not exists(select * from Bloodresults# where Neonatalid = '" & strNeoid & "' and date = '" & TempDate & "') insert into Bloodresults# (NeonatalID, Date, [sodium], [potass], [creat], [CRP], [hb], [wcc], [plat], [retic], [sbr], [bsl], Comment) Values('" & strNeoid & "', '" & TempDate & "', " & StrNa & ", " & StrK & ", " & StrCreat & ", " & StrCRP & ", " & StrHb & ", " & StrWCC & ", " & StrPlat & ", " & StrRetic & ", " & StrBili & ", " & StrBSL & ", '" & StrOther & "')"
            strJob = New SqlClient.SqlCommand(strSQL, conn)
            strJob.ExecuteNonQuery()
            strSQL = "exec procMobileAudit '" & Session("sessionusername") & "', 'Mobile Results', '" & strNeoid & "'"
            strJob = New SqlClient.SqlCommand(strSQL, conn)
            strJob.ExecuteNonQuery()

            ResultsDate.Text = Format(Now(), "yyyy MMM dd  HH00")
            Na.Text = ""
            K.Text = ""
            Creat.Text = ""
            CRP.Text = ""
            Hb.Text = ""
            WCC.Text = ""
            Plat.Text = ""
            Retic.Text = ""
            Bili.Text = ""
            BSL.Text = ""
            Other.Text = ""
        End If
    End Sub


    Protected Sub WgtSubmit_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles WgtSubmit.Click
        Dim TempWgtDate
        TempWgtDate = WgtDate.Text
        If Not IsDate(WgtDate.Text) And Len(TempWgtDate) > 5 Then
            TempWgtDate = Left(TempWgtDate, Len(TempWgtDate) - 2) + ":" + Right(TempWgtDate, 2)
        End If
        If IsNumeric(Wgt.Text) And IsDate(TempWgtDate) Then
            Dim strNeoid, strSQL, strJob
            strNeoid = Request.QueryString("@neoid")
            If CInt(Wgt.Text) > 250 And CInt(Wgt.Text) < 10000 And CDate(TempWgtDate) <= Today() And CDate(TempWgtDate) > DateAdd(DateInterval.Day, -10, Today()) Then
                strSQL = "if not exists(select * from [Growth#] where Neonatalid = '" & strNeoid & "' and date = '" & TempWgtDate & "') insert into [Growth#](NeonatalID, Date, Weight) values('" & strNeoid & "', '" & TempWgtDate & "', " & Wgt.Text & ")"
                If IsNumeric(HC.Text) Then
                    If CInt(HC.Text) > 10 And CInt(HC.Text) < 100 Then
                        strSQL = "if not exists(select * from [Growth#] where Neonatalid = '" & strNeoid & "' and date = '" & TempWgtDate & "') insert into [Growth#](NeonatalID, Date, Weight, HC) values('" & strNeoid & "', '" & TempWgtDate & "', " & Wgt.Text & ", " & HC.Text & ")"
                    End If
                End If
                strJob = New SqlClient.SqlCommand(strSQL, conn)
                strJob.ExecuteNonQuery()
                Wgt.Text = ""
                HC.Text = ""
                WgtDate.Text = Format(Now(), "yyyy MMM dd")
                strSQL = "exec procMobileAudit '" & Session("sessionusername") & "', 'Mobile Growth', '" & strNeoid & "'"
                strJob = New SqlClient.SqlCommand(strSQL, conn)
                strJob.ExecuteNonQuery()
		Wgt.Text = ""
                'Response.Redirect("Patient.aspx?@neoid=" & Request.QueryString("@neoid") & "&@hospid=" & Request.QueryString("@hospid") & "#GrowthChartholder")
	        Response.Redirect("Patient.aspx?@neoid=" & Request.QueryString("@neoid") & "&@hospid=" & Request.QueryString("@hospid"))

            Else
                System.Web.HttpContext.Current.Response.Write("<SCRIPT LANGUAGE=""JavaScript"">" & vbCrLf)
                System.Web.HttpContext.Current.Response.Write("alert('Growth values were not updated due to weight or date being outside permitted range')" & vbCrLf)
                System.Web.HttpContext.Current.Response.Write("</SCRIPT>")

            End If
        Else
            System.Web.HttpContext.Current.Response.Write("<SCRIPT LANGUAGE=""JavaScript"">" & vbCrLf)
            System.Web.HttpContext.Current.Response.Write("alert('Growth values were not updated due to error of entered values')" & vbCrLf)
            System.Web.HttpContext.Current.Response.Write("</SCRIPT>")

        End If
    End Sub

    Protected Sub Problem_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles Problem.TextChanged
        If Len(Session("sessionusername")) > 0 Then
            If Len(Problem.Text) > 1 Then
                Dim strNeoid
                strNeoid = Request.QueryString("@neoid")
                Dim strSQL As String = "if not exists(select * from Problems# where Neonatalid = '" & strNeoid & "' and Problem# = '" & Problem.Text & "' and Finish is null) insert into Problems#(Neonatalid, Problem#, start) values('" & strNeoid & "', '" & Problem.Text & "', getdate())"
                Dim strJob As SqlClient.SqlCommand = New SqlClient.SqlCommand(strSQL, conn)
                strJob.ExecuteNonQuery()
                Problem.Text = ""
                strSQL = "exec procMobileAudit '" & Session("sessionusername") & "', 'Mobile Problem', '" & strNeoid & "'"
                strJob = New SqlClient.SqlCommand(strSQL, conn)
                strJob.ExecuteNonQuery()
            End If
        Else
            Response.Redirect("MobLogon.aspx?ReturnUrl=PatientList.aspx")
        End If
    End Sub
    Protected Sub Circuit_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles Circuit.TextChanged
        Dim TempCctDate
        TempCctDate = Circuit.Text
        If Not IsDate(TempCctDate) And Len(TempCctDate) > 5 Then
            TempCctDate = Left(TempCctDate, Len(TempCctDate) - 2) + ":" + Right(TempCctDate, 2)
        End If
        Dim strNeoid, strSQL, strJob
        strNeoid = Request.QueryString("@neoid")
        If IsDate(TempCctDate) Then
            strSQL = "update respsupp set [CircuitChange#] = '" & TempCctDate & "' where finish is null and neonatalid = '" & strNeoid & "'"
            strJob = New SqlClient.SqlCommand(strSQL, conn)
            strJob.ExecuteNonQuery()
            strSQL = "exec procMobileAudit '" & Session("sessionusername") & "', 'Mobile Circuit', '" & strNeoid & "'"
            strJob = New SqlClient.SqlCommand(strSQL, conn)
            strJob.ExecuteNonQuery()
        ElseIf Len(TempCctDate) = 0 Then
            strSQL = "update respsupp set [CircuitChange#] = Null where finish is null and neonatalid = '" & strNeoid & "'"
            strJob = New SqlClient.SqlCommand(strSQL, conn)
            strJob.ExecuteNonQuery()
        Else
            System.Web.HttpContext.Current.Response.Write("<SCRIPT LANGUAGE=""JavaScript"">" & vbCrLf)
            System.Web.HttpContext.Current.Response.Write("alert('Date not recognised - Circuit change date was not updated!')" & vbCrLf)
            System.Web.HttpContext.Current.Response.Write("</SCRIPT>")

        End If
    End Sub




    Protected Sub Volume_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles Volume.TextChanged
        If Len(Session("sessionusername")) > 0 Then
            If Len(Volume.Text) > 1 Then
                Dim strNeoid
                strNeoid = Request.QueryString("@neoid")
                Dim strSQL As String = "update pmi set volume# = " & Volume.Text & " where Neonatalid = '" & strNeoid & "'"
                Dim strJob As SqlClient.SqlCommand = New SqlClient.SqlCommand(strSQL, conn)
                strJob.ExecuteNonQuery()
                strSQL = "exec procMobileAudit '" & Session("sessionusername") & "', 'Mobile Volume', '" & strNeoid & "'"
                strJob = New SqlClient.SqlCommand(strSQL, conn)
                strJob.ExecuteNonQuery()
            End If
        Else
            Response.Redirect("MobLogon.aspx?ReturnUrl=PatientList.aspx")
        End If
    End Sub
    Protected Sub FiO2_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles FiO2.TextChanged
        If Len(Session("sessionusername")) > 0 Then
            If Len(FiO2.Text) > 0 Then
                Dim strNeoid
                strNeoid = Request.QueryString("@neoid")
                Dim strSQL As String = "update pmi set FiO2# = '" & FiO2.Text & "' where Neonatalid = '" & strNeoid & "'"
                Dim strJob As SqlClient.SqlCommand = New SqlClient.SqlCommand(strSQL, conn)
                strJob.ExecuteNonQuery()

                strSQL = "INSERT into RespChange (NeonatalID, Date, fiO2#) values ('" & strNeoid & "', getdate(), '" & FiO2.Text & "')"
                strJob = New SqlClient.SqlCommand(strSQL, conn)
                strJob.ExecuteNonQuery()

                strSQL = "exec procMobileAudit '" & Session("sessionusername") & "', 'Mobile FiO2', '" & strNeoid & "'"
                strJob = New SqlClient.SqlCommand(strSQL, conn)
                strJob.ExecuteNonQuery()
            End If
        Else
            Response.Redirect("MobLogon.aspx?ReturnUrl=PatientList.aspx")
        End If
    End Sub
    Protected Sub FiO2b_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles FiO2b.TextChanged
        If Len(Session("sessionusername")) > 0 Then
            If Len(FiO2b.Text) > 0 Then
                Dim strNeoid
                strNeoid = Request.QueryString("@neoid")
                Dim strSQL As String = "update pmi set FiO2# = '" & FiO2b.Text & "' where Neonatalid = '" & strNeoid & "'"
                Dim strJob As SqlClient.SqlCommand = New SqlClient.SqlCommand(strSQL, conn)
                strJob.ExecuteNonQuery()

                strSQL = "INSERT into RespChange (NeonatalID, Date, fiO2#) values ('" & strNeoid & "', getdate(), '" & FiO2b.Text & "')"
                strJob = New SqlClient.SqlCommand(strSQL, conn)
                strJob.ExecuteNonQuery()

                strSQL = "exec procMobileAudit '" & Session("sessionusername") & "', 'Mobile FiO2', '" & strNeoid & "'"
                strJob = New SqlClient.SqlCommand(strSQL, conn)
                strJob.ExecuteNonQuery()
            End If
        Else
            Response.Redirect("MobLogon.aspx?ReturnUrl=PatientList.aspx")
        End If
    End Sub

    Protected Sub PIPData_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles PipData.TextChanged
        If Len(Session("sessionusername")) > 0 Then
            If Len(PipData.Text) > 0 Then
                Dim strNeoid
                strNeoid = Request.QueryString("@neoid")
                Dim strSQL As String = "update pmi set PIP# = " & PipData.Text & " where Neonatalid = '" & strNeoid & "'"
                Dim strJob As SqlClient.SqlCommand = New SqlClient.SqlCommand(strSQL, conn)
                strJob.ExecuteNonQuery()

                strSQL = "exec procMobileAudit '" & Session("sessionusername") & "', 'Mobile PIP', '" & strNeoid & "'"
                strJob = New SqlClient.SqlCommand(strSQL, conn)
                strJob.ExecuteNonQuery()
            End If
        Else
            Response.Redirect("MobLogon.aspx?ReturnUrl=PatientList.aspx")
        End If
    End Sub
    Protected Sub MAPData_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles MAPData.TextChanged
        If Len(Session("sessionusername")) > 0 Then
            If Len(MAPData.Text) > 0 Then
                Dim strNeoid
                strNeoid = Request.QueryString("@neoid")
                Dim strSQL As String = "update pmi set MAP# = " & MAPData.Text & " where Neonatalid = '" & strNeoid & "'"
                Dim strJob As SqlClient.SqlCommand = New SqlClient.SqlCommand(strSQL, conn)
                strJob.ExecuteNonQuery()

                strSQL = "INSERT into RespChange (NeonatalID, Date, MAP#) values ('" & strNeoid & "', getdate(), " & MAPData.Text & ")"
                strJob = New SqlClient.SqlCommand(strSQL, conn)
                strJob.ExecuteNonQuery()

                strSQL = "exec procMobileAudit '" & Session("sessionusername") & "', 'Mobile MAP', '" & strNeoid & "'"
                strJob = New SqlClient.SqlCommand(strSQL, conn)
                strJob.ExecuteNonQuery()
            End If
        Else
            Response.Redirect("MobLogon.aspx?ReturnUrl=PatientList.aspx")
        End If
    End Sub
    Protected Sub VGData_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles VgData.TextChanged
        If Len(Session("sessionusername")) > 0 Then
            If Len(VgData.Text) > 0 Then
                Dim strNeoid
                strNeoid = Request.QueryString("@neoid")
                Dim strSQL As String = "update pmi set VG# = " & VgData.Text & " where Neonatalid = '" & strNeoid & "'"
                Dim strJob As SqlClient.SqlCommand = New SqlClient.SqlCommand(strSQL, conn)
                strJob.ExecuteNonQuery()

                strSQL = "exec procMobileAudit '" & Session("sessionusername") & "', 'Mobile VG', '" & strNeoid & "'"
                strJob = New SqlClient.SqlCommand(strSQL, conn)
                strJob.ExecuteNonQuery()
            End If
        Else
            Response.Redirect("MobLogon.aspx?ReturnUrl=PatientList.aspx")
        End If
    End Sub
    Protected Sub RateData_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles RateData.TextChanged
        If Len(Session("sessionusername")) > 0 Then
            If Len(RateData.Text) > 0 Then
                Dim strNeoid
                strNeoid = Request.QueryString("@neoid")
                Dim strSQL As String = "update pmi set Rate# = " & RateData.Text & " where Neonatalid = '" & strNeoid & "'"
                Dim strJob As SqlClient.SqlCommand = New SqlClient.SqlCommand(strSQL, conn)
                strJob.ExecuteNonQuery()

                strSQL = "exec procMobileAudit '" & Session("sessionusername") & "', 'Mobile Rate', '" & strNeoid & "'"
                strJob = New SqlClient.SqlCommand(strSQL, conn)
                strJob.ExecuteNonQuery()
            End If
        Else
            Response.Redirect("MobLogon.aspx?ReturnUrl=PatientList.aspx")
        End If
    End Sub
    Protected Sub FlowData_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles FlowData.TextChanged
        If Len(Session("sessionusername")) > 0 Then
            If Len(FlowData.Text) > 0 Then
                Dim strNeoid
                strNeoid = Request.QueryString("@neoid")
                Dim strSQL As String = "update pmi set Flow# = " & FlowData.Text & " where Neonatalid = '" & strNeoid & "'"
                Dim strJob As SqlClient.SqlCommand = New SqlClient.SqlCommand(strSQL, conn)
                strJob.ExecuteNonQuery()

                strSQL = "exec procMobileAudit '" & Session("sessionusername") & "', 'Mobile Flow', '" & strNeoid & "'"
                strJob = New SqlClient.SqlCommand(strSQL, conn)
                strJob.ExecuteNonQuery()
            End If
        Else
            Response.Redirect("MobLogon.aspx?ReturnUrl=PatientList.aspx")
        End If
    End Sub
    Protected Sub Milk_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles Milk.TextChanged
        'If Len(Session("sessionusername")) > 0 Then
        '    If Len(Milk.Text) > 0 Then
        '        Dim strNeoid
        '        strNeoid = Request.QueryString("@neoid")
        '        Dim strSQL As String = "update feeds set Milk = " & Milk.Text & " from feeds where finish is null and Neonatalid = '" & strNeoid & "'"
        '        Dim strJob As SqlClient.SqlCommand = New SqlClient.SqlCommand(strSQL, conn)
        '        strJob.ExecuteNonQuery()
        '        strSQL = "exec procMobileAudit '" & Session("sessionusername") & "', 'Mobile Milk', '" & strNeoid & "'"
        '        strJob = New SqlClient.SqlCommand(strSQL, conn)
        '        strJob.ExecuteNonQuery()
        '    End If
        'Else
        '    Response.Redirect("MobLogon.aspx?ReturnUrl=PatientList.aspx")
        'End If
    End Sub
    Protected Sub MilkRoute_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles MilkRoute.TextChanged
        If Len(Session("sessionusername")) > 0 Then
            If Len(MilkRoute.Text) > 0 Then
                Dim strNeoid
                strNeoid = Request.QueryString("@neoid")
                Dim strSQL As String = "update feeds set MilkRoute = " & MilkRoute.Text & " from feeds where finish is null and Neonatalid = '" & strNeoid & "'"
                Dim strJob As SqlClient.SqlCommand = New SqlClient.SqlCommand(strSQL, conn)
                strJob.ExecuteNonQuery()
                strSQL = "exec procMobileAudit '" & Session("sessionusername") & "', 'Mobile MilkRoute', '" & strNeoid & "'"
                strJob = New SqlClient.SqlCommand(strSQL, conn)
                strJob.ExecuteNonQuery()
            End If
        Else
            Response.Redirect("MobLogon.aspx?ReturnUrl=PatientList.aspx")
        End If
    End Sub
    Protected Sub Hrly_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles Hrly.TextChanged
        If Len(Session("sessionusername")) > 0 Then
            If Len(Hrly.Text) > 0 Then
                Dim strNeoid
                strNeoid = Request.QueryString("@neoid")
                Dim strSQL As String = "update feeds set Hrly# = " & Hrly.Text & " from feeds where finish is null and Neonatalid = '" & strNeoid & "'"
                Dim strJob As SqlClient.SqlCommand = New SqlClient.SqlCommand(strSQL, conn)
                strJob.ExecuteNonQuery()
                strSQL = "exec procMobileAudit '" & Session("sessionusername") & "', 'Mobile Hrly', '" & strNeoid & "'"
                strJob = New SqlClient.SqlCommand(strSQL, conn)
                strJob.ExecuteNonQuery()
            End If
        Else
            Response.Redirect("MobLogon.aspx?ReturnUrl=PatientList.aspx")
        End If
    End Sub
    Protected Sub ml_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ml.TextChanged
        If Len(Session("sessionusername")) > 0 Then
            If Len(ml.Text) > 0 And IsNumeric(ml.Text) Then
                Dim strNeoid
                strNeoid = Request.QueryString("@neoid")
                Dim strSQL As String = "update feeds set ml# = " & ml.Text & " from feeds where finish is null and Neonatalid = '" & strNeoid & "'"
                Dim strJob As SqlClient.SqlCommand = New SqlClient.SqlCommand(strSQL, conn)
                strJob.ExecuteNonQuery()
                strSQL = "exec procMobileAudit '" & Session("sessionusername") & "', 'Mobile ml', '" & strNeoid & "'"
                strJob = New SqlClient.SqlCommand(strSQL, conn)
                strJob.ExecuteNonQuery()
                'Else
                '    System.Web.HttpContext.Current.Response.Write("<SCRIPT LANGUAGE=""JavaScript"">" & vbCrLf)
                '    System.Web.HttpContext.Current.Response.Write("alert('Sorry that is Not a number and will not be added')" & vbCrLf)
                '    System.Web.HttpContext.Current.Response.Write("</SCRIPT>")
            End If
        Else
            Response.Redirect("MobLogon.aspx?ReturnUrl=PatientList.aspx")
        End If
    End Sub
    Protected Sub Cal_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles Cal.TextChanged
        If Len(Session("sessionusername")) > 0 Then
            If Len(Cal.Text) > 0 Then
                Dim strNeoid
                strNeoid = Request.QueryString("@neoid")
                Dim strSQL As String = "update feeds set Cal# = " & Cal.Text & " from feeds where finish is null and Neonatalid = '" & strNeoid & "'"
                Dim strJob As SqlClient.SqlCommand = New SqlClient.SqlCommand(strSQL, conn)
                strJob.ExecuteNonQuery()
                strSQL = "exec procMobileAudit '" & Session("sessionusername") & "', 'Mobile Cal', '" & strNeoid & "'"
                strJob = New SqlClient.SqlCommand(strSQL, conn)
                strJob.ExecuteNonQuery()
            End If
        Else
            Response.Redirect("MobLogon.aspx?ReturnUrl=PatientList.aspx")
        End If
    End Sub
    Protected Sub Order_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles Order.TextChanged
        If Len(Session("sessionusername")) > 0 Then
            If Len(Order.Text) > 0 Then
                Dim strNeoid, strSQL, strJob
                strNeoid = Request.QueryString("@neoid")
                If Len(MedOrders.Text) + Len(Order.Text) <= 108 Then
                    'If Len(MedOrders.Text) > 2 Then
                    strSQL = "update episode set orders#  = case when orders# is null then '' else orders# + ', ' end + '" & Order.Text & "' from episode where dischdate is null and Neonatalid = '" & strNeoid & "'"
                    'Else
                    'strSQL = "update episode set orders#  = '" & Order.Text & "' from episode where dischdate is null and Neonatalid = '" & strNeoid & "'"
                    'End If
                    strJob = New SqlClient.SqlCommand(strSQL, conn)
                    strJob.ExecuteNonQuery()
                    Order.Text = ""
                    strSQL = "exec procMobileAudit '" & Session("sessionusername") & "', 'Mobile Order', '" & strNeoid & "'"
                    strJob = New SqlClient.SqlCommand(strSQL, conn)
                    strJob.ExecuteNonQuery()
                Else
                    System.Web.HttpContext.Current.Response.Write("<SCRIPT LANGUAGE=""JavaScript"">" & vbCrLf)
                    System.Web.HttpContext.Current.Response.Write("alert('Sorry - the total orders cannot exceed 100 characters " & CStr(Len(MedOrders.Text)) & "')" & vbCrLf)
                    System.Web.HttpContext.Current.Response.Write("</SCRIPT>")
                End If
            End If
        Else
            Response.Redirect("MobLogon.aspx?ReturnUrl=PatientList.aspx")
        End If
    End Sub
    Protected Sub MedOrders2_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles MedOrders2.TextChanged
        If Len(Session("sessionusername")) > 0 Then
            Dim strNeoid, strSQL, strJob
            strNeoid = Request.QueryString("@neoid")
            strSQL = "update episode set orders#  = '" & MedOrders2.Text & "' from episode where dischdate is null and Neonatalid = '" & strNeoid & "'"
            strJob = New SqlClient.SqlCommand(strSQL, conn)
            strJob.ExecuteNonQuery()
            Order.Text = ""
            strSQL = "exec procMobileAudit '" & Session("sessionusername") & "', 'Mobile Order', '" & strNeoid & "'"
            strJob = New SqlClient.SqlCommand(strSQL, conn)
            strJob.ExecuteNonQuery()
        Else
            Response.Redirect("MobLogon.aspx?ReturnUrl=PatientList.aspx")
        End If
    End Sub
    Protected Sub MedNote2_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles MedNote2.TextChanged
        If Len(Session("sessionusername")) > 0 Then
            Dim strNeoid, strSQL, strJob
            strNeoid = Request.QueryString("@neoid")
            strSQL = "update episode set summary#  = '" & MedNote2.Text & "' from episode where dischdate is null and Neonatalid = '" & strNeoid & "'"
            strJob = New SqlClient.SqlCommand(strSQL, conn)
            strJob.ExecuteNonQuery()
            Order.Text = ""
            strSQL = "exec procMobileAudit '" & Session("sessionusername") & "', 'Mobile Note', '" & strNeoid & "'"
            strJob = New SqlClient.SqlCommand(strSQL, conn)
            strJob.ExecuteNonQuery()
        Else
            Response.Redirect("MobLogon.aspx?ReturnUrl=PatientList.aspx")
        End If
    End Sub

    Protected Sub Usercomment_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles UserComment.TextChanged
        If Len(Session("sessionusername")) > 1 Then
            Dim strNeoid, strSQL, strJob
            strNeoid = Request.QueryString("@neoid")
            strSQL = "update mobilecomment set comment = '" & UserComment.Text & "' where neonatalid = '" & strNeoid & "' and staffid = (select staffid from neostaff where usercode = '" & Session("sessionusername") & "')"
            strJob = New SqlClient.SqlCommand(strSQL, conn)
            strJob.ExecuteNonQuery()
            'strSQL = "if not exists(select comment from mobilecomment m inner join neostaff n on m.staffid = n.staffid where neonatalid = '" & strNeoid & "' and usercode = '" & Session("sessionusername") & "') insert into mobilecomment(Neonatalid, StaffID, Comment) values('" & strNeoid & "', 'AB6BAFAD-E7C1-4BC3-AC6A-29F42D707FFF' ,'" & UserComment.Text & "')"
            strSQL = "if not exists(select comment from mobilecomment m inner join neostaff n on m.staffid = n.staffid where neonatalid = '" & strNeoid & "' and usercode = '" & Session("sessionusername") & "') insert into mobilecomment (neonatalid, staffid, comment) select '" & strNeoid & "', staffid, '" & UserComment.Text & "' from neostaff where usercode = '" & Session("sessionusername") & "'"
            'strSQL = "if not exists(select comment from mobilecomment m inner join neostaff n on m.staffid = n.staffid where neonatalid = '" & strNeoid & "' and usercode = '" & Session("sessionusername") & "') insert into mobilecomment(Neonatalid, StaffID, Comment) values('" & strNeoid & "', (select staffid from neostaff where usercode = '" & Session("sessionusername") & "') ,'" & UserComment.Text & "')"
            strJob = New SqlClient.SqlCommand(strSQL, conn)
            strJob.ExecuteNonQuery()

            strSQL = "exec procMobileAudit '" & Session("sessionusername") & "', 'Mobile Comment', '" & strNeoid & "'"
            strJob = New SqlClient.SqlCommand(strSQL, conn)
            strJob.ExecuteNonQuery()
        End If
    End Sub
    Protected Sub AddTreatment_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles AddTreatment.TextChanged
        If Len(Session("sessionusername")) > 0 Then
            If Len(AddTreatment.Text) > 0 Then
                Dim strNeoid, strSQL, strJob, strFinish
                strSQL = "select stat from _Treatment where number = " & AddTreatment.Text & ""
                Dim adp As SqlClient.SqlDataAdapter = New SqlClient.SqlDataAdapter(strSQL, conn)
                Dim dataset As New DataSet
                adp.Fill(dataset)
                If dataset.Tables(0).Rows.Count <> 0 Then
                    Dim DataRow As DataRow = dataset.Tables(0).Rows(0)
                    If DataRow.Item("stat") = "1" Then
                        strFinish = "getdate()"
                    Else
                        strFinish = "null"
                    End If
                End If

                strNeoid = Request.QueryString("@neoid")
                'strSQL = "if not exists(select * from Treatment where Neonatalid = '" & strNeoid & "' and Treatment = '" & AddTreatment.Text & "' and Finish is null) insert into Treatment(Neonatalid, Treatment, start) values('" & strNeoid & "', " & AddTreatment.Text & ", getdate())"
                strSQL = "if not exists(select * from Treatment where Neonatalid = '" & strNeoid & "' and Treatment = '" & AddTreatment.Text & "' and Finish is null) insert into Treatment(Neonatalid, Treatment, start, finish) values('" & strNeoid & "', " & AddTreatment.Text & ", getdate(), " & strFinish & ")"
                strJob = New SqlClient.SqlCommand(strSQL, conn)
                strJob.ExecuteNonQuery()
                AddTreatment.Text = ""
                strSQL = "exec procMobileAudit '" & Session("sessionusername") & "', 'Mobile AddTreatment', '" & strNeoid & "'"
                strJob = New SqlClient.SqlCommand(strSQL, conn)
                strJob.ExecuteNonQuery()
            End If
        Else
            Response.Redirect("MobLogon.aspx?ReturnUrl=PatientList.aspx")
        End If
    End Sub

    Protected Sub StopTreatment_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles StopTreatment.TextChanged
        If Len(Session("sessionusername")) > 0 Then
            If Len(StopTreatment.Text) > 0 Then
                Dim strNeoid, strSQL, strJob
                strNeoid = Request.QueryString("@neoid")
                strSQL = "update treatment set finish = getdate() from Treatment where Neonatalid = '" & strNeoid & "' and treatment = " & StopTreatment.Text & " and finish is null"
                strJob = New SqlClient.SqlCommand(strSQL, conn)
                strJob.ExecuteNonQuery()
                StopTreatment.Text = ""
                strSQL = "exec procMobileAudit '" & Session("sessionusername") & "', 'Mobile StopTreatment', '" & strNeoid & "'"
                strJob = New SqlClient.SqlCommand(strSQL, conn)
                strJob.ExecuteNonQuery()
            End If
        Else
            Response.Redirect("MobLogon.aspx?ReturnUrl=PatientList.aspx")
        End If
    End Sub
    Protected Sub InactivateProblem_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles InactivateProblem.TextChanged
        If Len(Session("sessionusername")) > 0 Then
            Dim strNeoid, strSQL, strJob
            strSQL = InactivateProblem.Text
            If Len(strSQL) > 0 Then
                If InStr(strSQL, ": ") > 0 Then
                    strSQL = Right(strSQL, Len(strSQL) - 1 - InStr(strSQL, ": "))
                End If
                strNeoid = Request.QueryString("@neoid")
                strSQL = "update problems# set finish = getdate() from problems# where Neonatalid = '" & strNeoid & "' and problem# = '" & strSQL & "' and finish is null"
                strJob = New SqlClient.SqlCommand(strSQL, conn)
                strJob.ExecuteNonQuery()
                StopTreatment.Text = ""
                strSQL = "exec procMobileAudit '" & Session("sessionusername") & "', 'Mobile InactivateProblem', '" & strNeoid & "'"
                strJob = New SqlClient.SqlCommand(strSQL, conn)
                strJob.ExecuteNonQuery()
            End If
        Else
            Response.Redirect("MobLogon.aspx?ReturnUrl=PatientList.aspx")
        End If
    End Sub

    Protected Sub NursingAdd_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles NursingAdd.TextChanged
        If Len(Session("sessionusername")) > 0 Then
            If Len(NursingAdd.Text) > 0 Then
                Dim strNeoid, strSQL, strJob
                strNeoid = Request.QueryString("@neoid")
                'If Len(NurseSummary.Text) > 0 Then
                strSQL = "update episode set comment0#  = case when comment0# is null then '* ' + convert(nvarchar(5),getdate(),103) + ' ' + '" & NursingAdd.Text & "' when left(comment0#,8) = '* ' + convert(nvarchar(5),getdate(),103) + ' ' then left(comment0#,8) + '" & NursingAdd.Text & "' + ', ' + left(right(comment0#,len(comment0#)-8), 1500) else '* ' + convert(nvarchar(5),getdate(),103) + ' ' + '" & NursingAdd.Text & "' + char(13) + char(10) + left(comment0#, 1500) end from episode where dischdate is null and Neonatalid = '" & strNeoid & "'"
                strJob = New SqlClient.SqlCommand(strSQL, conn)
                strJob.ExecuteNonQuery()
                NursingAdd.Text = ""
                strSQL = "exec procMobileAudit '" & Session("sessionusername") & "', 'Mobile Nursing', '" & strNeoid & "'"
                strJob = New SqlClient.SqlCommand(strSQL, conn)
                strJob.ExecuteNonQuery()
                'Else
                '    System.Web.HttpContext.Current.Response.Write("<SCRIPT LANGUAGE=""JavaScript"">" & vbCrLf)
                '    System.Web.HttpContext.Current.Response.Write("alert('Sorry - the Nursing Notes cannot exceed 1500 characters " & CStr(Len(MedOrders.Text)) & "')" & vbCrLf)
                '    System.Web.HttpContext.Current.Response.Write("</SCRIPT>")
                'End If
                strSQL = "select comment0# from episode where dischdate is null and Neonatalid = '" & strNeoid & "'"
                Dim adp As SqlClient.SqlDataAdapter = New SqlClient.SqlDataAdapter(strSQL, conn)
                Dim dataset As New DataSet
                adp.Fill(dataset)
                If dataset.Tables(0).Rows.Count <> 0 Then
                    Dim DataRow As DataRow = dataset.Tables(0).Rows(0)
                    NurseSummary.Text = DataRow.Item("comment0#")
                End If

            End If
        Else
            Response.Redirect("MobLogon.aspx?ReturnUrl=PatientList.aspx")
        End If
    End Sub

    Public Function Resp_Parameters() As Integer
        Select Case RespSuppChange.Text
            Case "1" 'ECMO
                PIPLabel.Visible = True
                PipData.Visible = True
                MAPLabel.Visible = True
                MAPData.Visible = True
                VGLabel.Visible = True
                VgData.Visible = True
                RateLabel.Visible = True
                RateData.Visible = True
                FlowLabel.Visible = True
                FlowData.Visible = True
            Case "2" 'HFO
                PIPLabel.Visible = True
                PIPLabel.Text = "Amp"
                PipData.Visible = True
                MAPLabel.Visible = True
                MAPLabel.Text = "MAP"
                MAPData.Visible = True
                VGLabel.Visible = False
                VgData.Visible = False
                RateLabel.Visible = True
                RateLabel.Text = "Hz"
                RateData.Visible = True
                FlowLabel.Visible = False
                FlowData.Visible = False
            Case "3" 'IMV
                PIPLabel.Visible = True
                PIPLabel.Text = "PIP"
                PipData.Visible = True
                MAPLabel.Visible = True
                MAPLabel.Text = "PEEP"
                MAPData.Visible = True
                VGLabel.Visible = True
                VgData.Visible = True
                RateLabel.Visible = True
                RateData.Visible = True
                FlowLabel.Visible = False
                FlowData.Visible = False
            Case "4" 'CPAP
                PIPLabel.Visible = False
                PipData.Visible = False
                MAPLabel.Visible = True
                MAPLabel.Text = "CPAP"
                MAPData.Visible = True
                VGLabel.Visible = False
                VgData.Visible = False
                RateLabel.Visible = False
                RateData.Visible = False
                FlowLabel.Visible = False
                FlowData.Visible = False
            Case "5" 'Nasal IMV
                PIPLabel.Visible = True
                PipData.Visible = True
                MAPLabel.Visible = True
                MAPData.Visible = True
                VGLabel.Visible = True
                VgData.Visible = True
                RateLabel.Visible = True
                RateData.Visible = True
                FlowLabel.Visible = True
                FlowData.Visible = True
            Case "7" 'Nasal Hi Flow
                PIPLabel.Visible = False
                PipData.Visible = False
                MAPLabel.Visible = False
                MAPData.Visible = False
                VGLabel.Visible = False
                VgData.Visible = False
                RateLabel.Visible = False
                RateData.Visible = False
                FlowLabel.Visible = True
                FlowData.Visible = True
            Case "8" 'Vapotherm
                PIPLabel.Visible = True
                PipData.Visible = True
                MAPLabel.Visible = True
                MAPData.Visible = True
                VGLabel.Visible = True
                VgData.Visible = True
                RateLabel.Visible = True
                RateData.Visible = True
                FlowLabel.Visible = True
                FlowData.Visible = True
            Case "10" 'Oxygen
                PIPLabel.Visible = False
                PipData.Visible = False
                MAPLabel.Visible = False
                MAPData.Visible = False
                VGLabel.Visible = False
                VgData.Visible = False
                RateLabel.Visible = False
                RateData.Visible = False
                FlowLabel.Visible = True
                FlowData.Visible = True
            Case Else
                PIPLabel.Visible = False
                PipData.Visible = False
                MAPLabel.Visible = False
                MAPData.Visible = False
                VGLabel.Visible = False
                VgData.Visible = False
                RateLabel.Visible = False
                RateData.Visible = False
                FlowLabel.Visible = False
                FlowData.Visible = False
        End Select


    End Function

    Public Function load_lists() As Integer
        Dim i
        Dim j

        PipData.Items.Clear()
        PipData.Items.Add("")
        For i = 5 To 60 Step 1
            PipData.Items.Add(i.ToString())
        Next
        'PipData.SelectedIndex = 15

        MAPData.Items.Clear()
        MAPData.Items.Add("")
        For i = 1 To 40 Step 1
            MAPData.Items.Add(i.ToString())
        Next

        RateData.Items.Clear()
        RateData.Items.Add("")
        For i = 5 To 120 Step 1
            RateData.Items.Add(i.ToString())
        Next

        VgData.Items.Clear()
        VgData.Items.Add("")
        For j = 1.5 To 25 Step 0.1
            VgData.Items.Add(Math.Round(j, 1).ToString())
        Next

        Return i
    End Function

    Protected Sub RespSuppButton_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles RespSuppButton.Click
        '        If Len(RespSuppChange.Text) > 0 Or Len(respsuppholder.Text) > 0 Then
        Dim TempRespDate, strNeoid, strSQL, strJob
        TempRespDate = Format(Now(), "yyyy/MM/dd HH:00")
        'TextBox1.text = TempRespDate
        strNeoid = Request.QueryString("@neoid")
        If RespSuppChange.Text <> respsuppholder.Text Then
            If Len(respsuppholder.Text) > 0 Then 'stop current respsupp
                strSQL = "update respsupp set finish = '" & TempRespDate & "' where Neonatalid = '" & strNeoid & "' and finish is null and respsupp = " & respsuppholder.Text
                strJob = New SqlClient.SqlCommand(strSQL, conn)
                strJob.ExecuteNonQuery()
            End If
            If Len(RespSuppChange.Text) > 0 Then 'start new respsupp
                strSQL = "insert into respsupp(Neonatalid, Start, Respsupp) values('" & strNeoid & "', '" & TempRespDate & "', " & RespSuppChange.Text & ")"
                strJob = New SqlClient.SqlCommand(strSQL, conn)
                strJob.ExecuteNonQuery()
            End If
            strSQL = "exec procMobileAudit '" & Session("sessionusername") & "', 'Mobile RespSupp', '" & strNeoid & "'"
            strJob = New SqlClient.SqlCommand(strSQL, conn)
            strJob.ExecuteNonQuery()
        End If
        Response.Redirect("Patient.aspx?@neoid=" & Request.QueryString("@neoid") & "&@hospid=" & Request.QueryString("@hospid"))
    End Sub

    Protected Sub MilkButton_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles MilkButton.Click
        Dim TempDate, strNeoid, strStaffID, strSQL, strJob, strWgt, strBW
        Dim strMilk, strMilkRoute, strMl, strTFI, strHrly, strCal, strNippleShield, strKangaroo, strFormulaEBM, strDonorMother
        strMilk = Milk.SelectedValue
        If Not IsNumeric(strMilk) Then strMilk = "Null"
        strMilkRoute = MilkRoute.SelectedValue
        If Not IsNumeric(strMilkRoute) Then strMilkRoute = "Null"
        strMl = ml.Text
        If Not IsNumeric(strMl) Then strMl = "Null"
        strHrly = Hrly.SelectedValue
        If Not IsNumeric(strHrly) Then strHrly = "Null"
        strCal = Cal.SelectedValue
        If Not IsNumeric(strCal) Then strCal = "Null"
        strTFI = Volume.SelectedValue
        If Not IsNumeric(strTFI) Then strTFI = "Null"
        If Nippleshield.Checked = True Then
            strNippleShield = "-1"
        Else
            strNippleShield = "0"
        End If
        If Kangaroo.Checked = True Then
            strKangaroo = "-1"
        Else
            strKangaroo = "0"
        End If

        TempDate = Format(Now(), "yyyy/MM/dd HH:00")
        strNeoid = Request.QueryString("@neoid")

        'strFormulaEBM = FormulaEBM.Text
        'strDonorMother = DonorMother.Text




        strSQL = "select convert(nvarchar(50),staffid) as staffid from neostaff where usercode = '" & Session("sessionusername") & "'"
        Dim adp As SqlClient.SqlDataAdapter = New SqlClient.SqlDataAdapter(strSQL, conn)
        Dim dataset As New DataSet

        adp.Fill(dataset)
        If dataset.Tables(0).Rows.Count <> 0 Then
            Dim Ian As DataRow = dataset.Tables(0).Rows(0)
            strStaffID = Ian.Item("staffid")
            ' [calcwgt#] 
            strSQL = "select top 1 weight from [growth#] where neonatalid = '" & strNeoid & "' and not weight is null order by date desc"
            adp = New SqlClient.SqlDataAdapter(strSQL, conn)
            dataset = New DataSet
            adp.Fill(dataset)
            If dataset.Tables(0).Rows.Count <> 0 Then
                Dim DataRow As DataRow = dataset.Tables(0).Rows(0)
                strWgt = DataRow.Item("weight")
            End If
            strSQL = "select top 1 BW from PMI where neonatalid = '" & strNeoid & "'"
            adp = New SqlClient.SqlDataAdapter(strSQL, conn)
            dataset = New DataSet
            adp.Fill(dataset)
            If dataset.Tables(0).Rows.Count <> 0 Then
                Dim DataRow As DataRow = dataset.Tables(0).Rows(0)
                strBW = DataRow.Item("BW")
            End If
            If strBW > strWgt Then
                strWgt = strBW
            End If

            If Len(strNeoid) > 0 Then
                strSQL = "insert into feedchange (Neonatalid, staffid, date, milk, milkroute, [ml#], [hrly#], [cal#], TFI, calcwgt, FormulaEBM, DonorMother, Nippleshield, Kangaroo) values('" & strNeoid & "', '" & strStaffID & "', getdate(), " & strMilk & ", " & strMilkRoute & ", " & strMl & ", " & strHrly & ", " & strCal & ", " & strTFI & ", " & strWgt & ", " & strFormulaEBM & ", " & strDonorMother & ", " & strNippleShield & ", " & strKangaroo & ")"
                strJob = New SqlClient.SqlCommand(strSQL, conn)
                strJob.ExecuteNonQuery()
            End If
        End If


        If Milk.Text <> MilkHolder.Text Then
            If Len(MilkHolder.Text) > 0 Then 'stop current feed
                strSQL = "update Feeds set finish = '" & TempDate & "' where Neonatalid = '" & strNeoid & "' and finish is null"
                strJob = New SqlClient.SqlCommand(strSQL, conn)
                strJob.ExecuteNonQuery()
            End If
            If Len(Milk.Text) > 0 Then 'start new feed
                'If Milk.Text = "0" Or Len(MilkHolder.Text) = 0 Then 'No values to carry
                '    strSQL = "insert into Feeds(Neonatalid, Start, Milk) values('" & strNeoid & "', '" & TempDate & "', " & Milk.Text & ")"
                '    strJob = New SqlClient.SqlCommand(strSQL, conn)
                '    strJob.ExecuteNonQuery()
                'Else 'New Milk Type with old values to carry over
                strSQL = "insert into Feeds(Neonatalid, Start, Milk, Milkroute, [ml#], [hrly#], [cal#]) values ('" & strNeoid & "', '" & TempDate & "', " & Milk.Text & ", " & strMilkRoute & ", " & strMl & ", " & strHrly & ", " & strCal & ")"
                strJob = New SqlClient.SqlCommand(strSQL, conn)
                strJob.ExecuteNonQuery()
                'End If
            End If



            strSQL = "exec procMobileAudit '" & Session("sessionusername") & "', 'Mobile Feeds', '" & strNeoid & "'"
            strJob = New SqlClient.SqlCommand(strSQL, conn)
            strJob.ExecuteNonQuery()
        End If


        strSQL = "update Feeds set DonorMother = " & strDonorMother & ", FormulaEBM = " & strFormulaEBM & " where NeonatalID = '" & strNeoid & "' and finish is null"
        strJob = New SqlClient.SqlCommand(strSQL, conn)
        strJob.ExecuteNonQuery()


        ConfirmLabel.Visible = True
        BIntent.Visible = False
        ConfirmLabel.Text = "Recorded"
        'Response.Redirect("Patient.aspx?@neoid=" & Request.QueryString("@neoid") & "&@hospid=" & Request.QueryString("@hospid"))
    End Sub

    Protected Sub ProblemDetails_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ProblemDetails.Click
        Dim strNeoid, strSQL, strJob
        strNeoid = Request.QueryString("@neoid")
        Bedspacer1.Visible = True
        Bedspacer2.Visible = True
        BedList3.Visible = True
        DischLabel.Visible = True
        DischLabel2.Visible = True
        DischDate.Visible = True
        DischSubmit.Visible = True
        ModeSep.Visible = True
        Insurance.Visible = True
        ProblemList.Visible = True
        ProblemDetails.Visible = False
        ConfirmLabel.Visible = False
        BIntent.Visible = True
        NewResults.Visible = True
        ResultsLabel2.Visible = True
        ResultsDate.Visible = True
        SendResults.Visible = True
        ResultsLabel3.Visible = True
        MedNote.Text = "Note:"
        MedNote2.Visible = True
        MedOrders.Visible = False
        MedOrders2.Visible = True
        Order.Visible = False
        NewOrderLabel.Text = "<br/>Orders:"
        strSQL = "exec procMobileAudit '" & Session("sessionusername") & "', 'Mobile ProblemDetails', '" & strNeoid & "'"
        strJob = New SqlClient.SqlCommand(strSQL, conn)
        'strJob.ExecuteNonQuery()
    End Sub


    Protected Sub DischSubmit_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles DischSubmit.Click
        Dim strNeoid, strSQL, strJob, strDischDate
        strNeoid = Request.QueryString("@neoid")
        strDischDate = DischDate.Text
        If Not IsDate(DischDate.Text) And Len(strDischDate) > 5 Then
            strDischDate = Left(strDischDate, Len(strDischDate) - 2) + ":" + Right(strDischDate, 2)
        End If
	if IsDate(strDischDate) and len(ModeSep.text) > 0 then
        strSQL = "update episode set dischdate = '" & strDischDate & "', ModeSep = " & ModeSep.text & " where dischdate is null and Neonatalid = '" & strNeoid & "'"
        strJob = New SqlClient.SqlCommand(strSQL, conn)
        strJob.ExecuteNonQuery()
        Response.Redirect("PatientList.aspx")
	End If
    End Sub


    Protected Sub GreyInactiveProblems_RowDataBOund(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs)
        If e.Row.RowType = DataControlRowType.DataRow Then
            Dim problem As String = DataBinder.Eval(e.Row.DataItem, "Finish")
            If Len(problem) > 3 Then
                e.Row.ForeColor = Drawing.Color.DarkSalmon
            End If
        End If
    End Sub
    Protected Sub BIntent_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles BIntent.TextChanged
        If Len(Session("sessionusername")) > 0 Then
            Dim strNeoid, strSQL As String, strBintent
            strBintent = BIntent.SelectedValue
            strNeoid = Request.QueryString("@neoid")
            If IsNumeric(BIntent.SelectedValue) Then 'CInt(BIntent.SelectedValue) >= 0 
                strSQL = "update Pregnancy set BIntent# = " & BIntent.SelectedValue & " from pregnancy inner join pmi on pregnancy.pregid = pmi.pregid where Neonatalid = '" & strNeoid & "'"
            Else
                strSQL = "update Pregnancy set BIntent# = Null from pregnancy inner join pmi on pregnancy.pregid = pmi.pregid where pmi.Neonatalid = '" & strNeoid & "'"
            End If
            Dim strJob As SqlClient.SqlCommand = New SqlClient.SqlCommand(strSQL, conn)
            strJob.ExecuteNonQuery()

            If IsNumeric(strBintent) Then
                Select Case strBintent
                    Case "1" 'Bottle
                        strBintent = "0"
                    Case "2" 'Both
                        strBintent = "-1"
                    Case "3" 'Breast
                        strBintent = "-1"
                End Select
                strSQL = "update PMI set BIntent = " & strBintent & " where BIntent is null and Neonatalid = '" & strNeoid & "'"
                strJob = New SqlClient.SqlCommand(strSQL, conn)
                strJob.ExecuteNonQuery()
            End If

            strSQL = "exec procMobileAudit '" & Session("sessionusername") & "', 'Mobile BIntent', '" & strNeoid & "'"
            strJob = New SqlClient.SqlCommand(strSQL, conn)
            strJob.ExecuteNonQuery()
        Else
            Response.Redirect("MobLogon.aspx?ReturnUrl=PatientList.aspx")
        End If
    End Sub
    Protected Sub FirstExpress_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles FirstExpress.TextChanged
        Dim TempDate
        TempDate = FirstExpress.Text
        If Not IsDate(TempDate) And Len(TempDate) > 5 Then
            TempDate = Left(TempDate, Len(TempDate) - 2) + ":" + Right(TempDate, 2)
        End If
        Dim strNeoid, strSQL, strJob
        strNeoid = Request.QueryString("@neoid")
        If IsDate(TempDate) Then
            strSQL = "update Pregnancy set FirstExpress = '" & TempDate & "' from pregnancy inner join pmi on pregnancy.pregid = pmi.pregid where Neonatalid = '" & strNeoid & "'"
            strJob = New SqlClient.SqlCommand(strSQL, conn)
            strJob.ExecuteNonQuery()
            strSQL = "exec procMobileAudit '" & Session("sessionusername") & "', 'FirstExpress', '" & strNeoid & "'"
            strJob = New SqlClient.SqlCommand(strSQL, conn)
            strJob.ExecuteNonQuery()
        ElseIf Len(TempDate) = 0 Then
            strSQL = "update Pregnancy set FirstExpress = Null from pregnancy inner join pmi on pregnancy.pregid = pmi.pregid where pmi.Neonatalid = '" & strNeoid & "'"
            strJob = New SqlClient.SqlCommand(strSQL, conn)
            strJob.ExecuteNonQuery()
        Else
            System.Web.HttpContext.Current.Response.Write("<SCRIPT LANGUAGE=""JavaScript"">" & vbCrLf)
            System.Web.HttpContext.Current.Response.Write("alert('Date not recognised - First Express date was not updated!')" & vbCrLf)
            System.Web.HttpContext.Current.Response.Write("</SCRIPT>")

        End If
    End Sub

    Protected Sub Imaging_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Imaging.Click
        Dim strNeoid, strSQL, strPatID, strImagingURL
        strNeoid = Request.QueryString("@neoid")
        strSQL = "select Userdef1 from PMI where neonatalid = '" & strNeoid & "'"
        Dim adp As SqlClient.SqlDataAdapter = New SqlClient.SqlDataAdapter(strSQL, conn)
        Dim dataset As New DataSet
        adp.Fill(dataset)
        If dataset.Tables(0).Rows.Count <> 0 Then
            Dim Ian As DataRow = dataset.Tables(0).Rows(0)
            strPatID = Ian.Item("Userdef1")
            If IsNumeric(strPatID) Then
                strSQL = "select ImagingURL from [_Hospitals] where Hospitalid = '" & Session("sessionhospid") & "'"
                adp = New SqlClient.SqlDataAdapter(strSQL, conn)
                dataset = New DataSet
                adp.Fill(dataset)
                If dataset.Tables(0).Rows.Count <> 0 Then
                    Dim Ian2 As DataRow = dataset.Tables(0).Rows(0)
                    strImagingURL = Ian2.Item("ImagingURL")
                    'Response.Redirect("http://10.55.195.106/ami/html/webviewer.html?showlist&un=POWERCHART&pw=P0w3RCH@rt!&ris_pat_id=" & strPatID & "&authority=MPI")
                    Response.Redirect(Replace(strImagingURL, "ReplaceWithUPI", strPatID))
                End If
            End If
        End If
    End Sub
    Protected Sub StartCatheter_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles StartCatheter.TextChanged
        If Len(Session("sessionusername")) > 0 Then
            Dim strNeoid, strSQL As String
            strNeoid = Request.QueryString("@neoid")
            strSQL = "insert into Catheters(Neonatalid, DateIn, Catheter) values('" & strNeoid & "', getdate(), " & StartCatheter.SelectedValue & ")"
            Dim strJob As SqlClient.SqlCommand = New SqlClient.SqlCommand(strSQL, conn)
            strJob.ExecuteNonQuery()

            strSQL = "exec procMobileAudit '" & Session("sessionusername") & "', 'Mobile CatheterStart', '" & strNeoid & "'"
            strJob = New SqlClient.SqlCommand(strSQL, conn)
            strJob.ExecuteNonQuery()
        Else
            Response.Redirect("MobLogon.aspx?ReturnUrl=PatientList.aspx")
        End If
    End Sub
    Protected Sub StopCatheter_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles StopCatheter.TextChanged
        If Len(Session("sessionusername")) > 0 Then
            Dim strNeoid, strSQL As String
            strNeoid = Request.QueryString("@neoid")
            strSQL = "update Catheters set DateOut = getdate() where Neonatalid = '" & strNeoid & "' and Catheter = " & StopCatheter.SelectedValue & " and DateOut is null"
            Dim strJob As SqlClient.SqlCommand = New SqlClient.SqlCommand(strSQL, conn)
            strJob.ExecuteNonQuery()

            strSQL = "exec procMobileAudit '" & Session("sessionusername") & "', 'Mobile CatheterFinish', '" & strNeoid & "'"
            strJob = New SqlClient.SqlCommand(strSQL, conn)
            strJob.ExecuteNonQuery()
        Else
            Response.Redirect("MobLogon.aspx?ReturnUrl=PatientList.aspx")
        End If
    End Sub
    Protected Sub NurseSummary_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles NurseSummary.TextChanged
        If Len(Session("sessionusername")) > 0 Then
            If Len(NurseSummary.Text) > 0 Then
                Dim strNeoid, strSQL, strJob, strText1, strText2
                If IsDBNull(NursingAdd.Text) Then
                    strText1 = ""
                Else
                    strText1 = NursingAdd.Text
                End If
                strText1 = Replace(strText1, "'", "`")
                strText2 = Replace(NurseSummary.Text, "'", "`")
                strNeoid = Request.QueryString("@neoid")
                If Len(strText1) > 0 Then
                    strSQL = "update episode set comment0#  = case when '" & Left(strText2, 8) & "' = '* ' + convert(nvarchar(5),getdate(),103) + ' ' then '" & Left(strText2, 8) & "' + '" & NursingAdd.Text & "' + ', ' + '" & Left(Right(strText2, Len(strText2) - 8), 1500) & "' else '* ' + convert(nvarchar(5),getdate(),103) + ' ' + '" & NursingAdd.Text & "' + char(13) + char(10) + '" & Left(strText2, 1500) & "' end + '" & strText2 & "' from episode where dischdate is null and Neonatalid = '" & strNeoid & "'"
                    'case when '" & left(strText2,8) & "' = '* ' + convert(nvarchar(5),getdate(),103) + ' ' then '" & left(strText2,8) & "' + '" & NursingAdd.Text & "' + ', ' + '" & left(right(strText2,len(strText2)-8), 1500) & "' else '* ' + convert(nvarchar(5),getdate(),103) + ' ' + '" & NursingAdd.Text & "' + char(13) + char(10) + '" & left(strText2, 1500) & "' end
                    'case when comment0# is null then '* ' + convert(nvarchar(5),getdate(),103) + ' ' + '" & NursingAdd.Text & "' when left(comment0#,8) = '* ' + convert(nvarchar(5),getdate(),103) + ' ' then left(comment0#,8) + '" & NursingAdd.Text & "' + ', ' + left(right(comment0#,len(comment0#)-8), 1500) else '* ' + convert(nvarchar(5),getdate(),103) + ' ' + '" & NursingAdd.Text & "' + char(13) + char(10) + left(comment0#, 1500) end
                ElseIf Len(strText1) = 0 Then
                    strSQL = "update episode set comment0#  = '" & strText2 & "' from episode where dischdate is null and Neonatalid = '" & strNeoid & "'"
                    strJob = New SqlClient.SqlCommand(strSQL, conn)
                    strJob.ExecuteNonQuery()
                    Order.Text = ""
                    strSQL = "exec procMobileAudit '" & Session("sessionusername") & "', 'Mobile Nursing', '" & strNeoid & "'"
                    strJob = New SqlClient.SqlCommand(strSQL, conn)
                    strJob.ExecuteNonQuery()
                Else
                    System.Web.HttpContext.Current.Response.Write("<SCRIPT LANGUAGE=""JavaScript"">" & vbCrLf)
                    System.Web.HttpContext.Current.Response.Write("alert('Sorry - there is too much text')" & vbCrLf)
                    System.Web.HttpContext.Current.Response.Write("</SCRIPT>")
                End If
            End If
        Else
            Response.Redirect("MobLogon.aspx?ReturnUrl=PatientList.aspx")
        End If
    End Sub
    Protected Sub DiagnosisComment_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles DiagnosisComment.TextChanged
        If Len(Session("sessionusername")) > 0 Then
            Dim strNeoid, strSQL, strJob, strText1
            If IsDBNull(DiagnosisComment.Text) Then
                strText1 = ""
            Else
                strText1 = DiagnosisComment.Text
            End If
            strText1 = Replace(strText1, "'", "`")
            strNeoid = Request.QueryString("@neoid")
            strSQL = "update pmi set DiagnosisComment  = '" & strText1 & "' where Neonatalid = '" & strNeoid & "'"
            strJob = New SqlClient.SqlCommand(strSQL, conn)
            strJob.ExecuteNonQuery()

        Else
        Response.Redirect("MobLogon.aspx?ReturnUrl=PatientList.aspx")
        End If
    End Sub
    Protected Sub EditDiagnosisComment_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles EditDiagnosisComment.Click
        diagnosiscomment.Visible = True
        diagnosiscommentLabel.Visible = False
        EditDiagnosisComment.Visible = False
    End Sub
    Protected Sub THospList_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles THospList.TextChanged
        If Len(Session("sessionusername")) > 0 Then
            If Len(THospList.SelectedValue) > 0 Then
                Dim strNeoid, strSQL, strJob
                strNeoid = Request.QueryString("@neoid")
                strSQL = "update episode set THospCode = '" & THospList.SelectedValue & "', THospCodeOrig = '" & THospList.SelectedValue & "' where Neonatalid = '" & strNeoid & "' and dischdate is null"
                strJob = New SqlClient.SqlCommand(strSQL, conn)
                strJob.ExecuteNonQuery()

                strSQL = "exec procMobileAudit '" & Session("sessionusername") & "', 'Mobile TransferHosp', '" & strNeoid & "'"
                strJob = New SqlClient.SqlCommand(strSQL, conn)
                strJob.ExecuteNonQuery()
            End If
        Else
            Response.Redirect("MobLogon.aspx?ReturnUrl=PatientList.aspx")
        End If
    End Sub
End Class
