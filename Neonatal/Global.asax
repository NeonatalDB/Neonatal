<%@ Application Language="VB" %>

<script runat="server">

    Sub Application_Start(ByVal sender As Object, ByVal e As EventArgs)
        ' Code that runs on application startup
    End Sub
    
    Sub Application_End(ByVal sender As Object, ByVal e As EventArgs)
        ' Code that runs on application shutdown
    End Sub
        
    Sub Application_Error(ByVal sender As Object, ByVal e As EventArgs)
        ' Code that runs when an unhandled error occurs
    End Sub

    Sub Session_Start(ByVal sender As Object, ByVal e As EventArgs)
        ' Code that runs when a new session is started
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
        Session("sessionNursingNotes") = 0
        Session("sessioncomment") = 0
    End Sub

    Sub Session_End(ByVal sender As Object, ByVal e As EventArgs)
        ' Code that runs when a session ends. 
        ' Note: The Session_End event is raised only when the sessionstate mode
        ' is set to InProc in the Web.config file. If session mode is set to StateServer 
        ' or SQLServer, the event is not raised.
    End Sub
       
</script>