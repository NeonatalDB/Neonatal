<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="Neonatal.Default" %>

<%@ Register Assembly="System.Web.DataVisualization, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" Namespace="System.Web.UI.DataVisualization.Charting" TagPrefix="asp" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Ian and Robs Webpage</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <h1>How about this plan!</h1>
        If we actually use the real database instead of a copy we wont have any database structure sync issues.<br />
        We could either connect at work directly to the sql or via vpn if away from work<br />
        <br />
        I cannot use Github when I am at work but am currently at home connected to the VPN (so database works) and am also able to sync my changes with Github.<br />
        The web client would be out of sync when we are at work but this can be done intermittently at home<br />
        <br />
        Thoughts?<br />
        <br />
        I have copied up the website for mobile devices for you to check this out in principle.<br />
        It is pretty clumsy written in vb but is quite useful on ward rounds on a tablet footprint.<br />
         It has migrated from .NET 3.5 to 4.5 and now the chart controls are part of .NET so there are no addons at all (I disabled a slider control I was using with the AjaxControlToolkit addon)<br />
        <br />
        <h1><a href="Mobile/MobLogon.aspx?ReturnUrl=PatientList.aspx">Mobile Website Logon</a></h1><br />
           <p/>
        You will need to connect to the Neonatal Database by VPN if you are at home.<br />
        I suggest you use the following logon for Liverpool hospital so you can see what it looks like when in daily use.<br />
        Remember this is live data so please dont actually change anything (clicking save is NOT required for changes to be sent to the database<br />
        username livtest    password neo1

        <p>I would be keen to discuss / meet up to begin the project.<br />
            Do you have the Github working properly from VS?</p>

  
    </div>
    </form>
</body>
</html>
