<%@ Page Title="" Language="VB" MasterPageFile="MobileMasterPage.master" AutoEventWireup="false" CodeFile="MobLogon.aspx.vb" Inherits="MobLogon" %>


<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<p>
    <strong><span style="font-size: 16pt; color: #003366; text-decoration: underline">Neonatal Database - Clinical Web Interface</span></strong></p>
    <span style="font-size: 16pt">
Access to Patient information via this site is with</br> the usual obligation of confidentiality 
as a health care provider </br>but with addition of 2 rules for a handheld device.</span><ul>
        <li><span style="font-size: 16pt; color: #ff3333;">Patient Information must be deleted
            from your device after use</span></li>
        <li><span style="font-size: 16pt; color: #ff3333;">Your Password must be entered each time and not be
            saved </span></li>
    </ul>

<asp:Table ID="table1" runat = "server" style="text-align: center">
<asp:TableRow>
<asp:TableCell style ="font-size:18pt"> Username:
</asp:TableCell>
<asp:TableCell> <asp:TextBox ID="Username" width = "200px" style ="font-size:18pt" runat="server"></asp:TextBox>
</asp:TableCell>
</asp:TableRow>
<asp:TableRow>
<asp:TableCell style ="font-size:18pt">Password:
</asp:TableCell>
<asp:TableCell> <asp:TextBox ID="Password" width = "200px" style ="font-size:18pt" runat="server" TextMode="Password"></asp:TextBox>  <p />
</asp:TableCell>
</asp:TableRow>
<asp:TableRow>
<asp:TableCell>
</asp:TableCell>
<asp:TableCell><asp:Button ID="Logon" runat="server" Text="Logon" width = "200px" style ="font-size:18pt" onclick="Logon_Click" />
</asp:TableCell>
</asp:TableRow>
</asp:Table>
<br />
<span style="font-size: 16pt; color: #0066CC; text-align:center">
*EXPAND Button will display many more interactive options</br>
*Haematology graph now also displays Clines, BC's as well as Abs!</br>
*Consultants/Fellows may access here via smartphone using VPN.</br></br>
*OPTIONAL personalised items at bottom of the Patient List!!</br>
 ToDo List - added to the patient view - just for your eyes </br>
 Default Nursing Notes - see the nursing handover automatically</br>
</span></br>
<span style="font-size: 16pt; color: #990033; text-align:center">
*Quality Android 7" tablets are now as cheap as $200<br />
 Easily held in one hand and fits in your back pocket</br> 
 - unlike iPad mini ($370)</span>
<br /><br />

    <p> <span style="font-size: 14pt">
    <a href="Mobile Help.pdf" target="_blank">Mobile Help</a>
        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
    <a href="../Menu3/Form0.aspx">Full Website</a> 
    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
    <a href="http://virtcws-mws002.nswhealth.net/pprs/" target="_blank">Bedstate</a>
    
    <br /><br /><a href="../Images/Neonatal.ico">Database Icon</a>
        </span></p>
</asp:Content>

