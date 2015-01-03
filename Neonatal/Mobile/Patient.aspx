<%@ Page Language="VB" MasterPageFile="~/Mobile/MobileMasterPage.master" AutoEventWireup="false" CodeFile="Patient.aspx.vb" Inherits="Mobile_Patient" title="Neonatal Database - Patient Data" %>


<%--<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>--%>

<%--<%@ Register Assembly="System.Web.DataVisualization, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" Namespace="System.Web.UI.DataVisualization.Charting" TagPrefix="asp" %>--%>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

<%--
<asp:UpdatePanel ID="UpdatePanel2" runat="server">
<ContentTemplate>
<asp:UpdateProgress runat="server">
<ProgressTemplate>
one moment please &nbsp;....
</ProgressTemplate>
</asp:UpdateProgress>
</ContentTemplate>
</asp:UpdatePanel>
--%>

<div style="background-color:#CCCCCC; text-align: left; font-size:10pt" >
    <asp:PlaceHolder ID="Top" runat="server"></asp:PlaceHolder>
    <asp:Button ID="PatientList" runat="server" Text="Patient List"  width = "130px" style ="font-size:12pt"  />&nbsp&nbsp;&nbsp
    <asp:Button ID="PrevBed" runat="server" Text="Prev" width = "70px" style ="font-size:12pt"  />&nbsp&nbsp;&nbsp
    <asp:DropDownList ID="BedList" width = "200px" style ="font-size:12pt" runat="server" DataSourceID="BedListDataSource" DataTextField="Bed" DataValueField="BedName" AutoPostBack="True">
    </asp:DropDownList>&nbsp&nbsp&nbsp
    <asp:Button ID="NextBed" runat="server" Text="Next"  width = "70px" style ="font-size:12pt"  />
    </div>
    <asp:Label ID="Label1" runat="server" Text="Surname, FirstName (M) DOB: 23 Nov 25+3 500g" Font-Bold="True" ForeColor="#990033" style ="font-size:16pt"></asp:Label><br />
    <asp:Label ID="Label2" runat="server" Text="MRN 1234567  Day 55 CorrGA 35+3" ForeColor="#990033" style ="font-size:15pt"></asp:Label>
    
    <asp:Label ID="EpisodeID" visible = "false" runat ="server" Text=""></asp:Label>
    <asp:Label ID="PrevBedLabel" visible = "false" runat ="server" Text=""></asp:Label>
    <asp:Label ID="NextBedLabel" visible = "false" runat ="server" Text=""></asp:Label>
    <hr />
    <asp:Label ID="PeriHxLabel"  runat ="server" ForeColor="#990033" width = "200px" Text="Perinatal History:" Font-Bold="True" style ="font-size:16pt"></asp:Label>
    <asp:Label ID="Suburb"  runat ="server"  ForeColor="#990033" width = "300px" Text="" Font-Bold="false" style ="font-size:16pt; text-align: Right"></asp:Label>
    <br />
    <asp:Label ID="PeriHx"  runat ="server" Text="Perinatal History" Width="550px" style ="font-size:10pt"></asp:Label>
    <hr />
    <asp:Label ID = "UsercommentLabel" runat="server" Visible="false" ForeColor="salmon">Personal </asp:Label>
    <asp:TextBox ID="UserComment" runat="server" width = "450px" style ="font-size:11pt" visible = "false"/> 
    
    <asp:UpdatePanel ID="UpdatePanel2" runat="server">
    <ContentTemplate>
    <asp:Label ID="BedSpacer1" visible = "false" runat ="server" Text=""><br /></asp:Label>
    <asp:DropDownList ID="BedList3" visible = "false" width = "220px" style ="font-size:12pt" runat="server" DataSourceID="BedList3DataSource" DataTextField="Bed" DataValueField="BedName" AutoPostBack="True">
    </asp:DropDownList>
    <asp:Label ID="BedSpacer2" visible = "false" runat ="server" Text=""><br /></asp:Label>


    <asp:Label ID="DischLabel" visible = "false" runat ="server" Text=""><br />Discharge from Ward </asp:Label>
    <asp:TextBox ID="DischDate" runat="server" width = "130px" style ="font-size:11pt" visible = "False"/> 
<asp:DropDownList ID="ModeSep" runat="server" style ="font-size:11pt" Visible = "false" Width="95px" >
            <asp:ListItem Value="">Mode?</asp:ListItem>
            <asp:ListItem Value="1">Discharge</asp:ListItem>
            <asp:ListItem Value="2">Transfer</asp:ListItem>
            <asp:ListItem Value="3">Death</asp:ListItem>
</asp:DropDownList>
    <asp:Button ID="DischSubmit" runat="server" Text="Discharge"  style ="font-size:12pt" visible = "False"/>
    <asp:Label ID="DischLabel2" visible = "false" runat ="server" Text=""><br /></asp:Label>
<br/>
    <asp:Label ID="Consultant"  runat ="server"  ForeColor="#990033" Text="Consultant" Font-Bold="True" style ="font-size:14pt; text-align: Left"></asp:Label>
    <asp:DropDownList ID="Insurance" runat="server" Font-Names="Times New Roman" style ="font-size:14pt" Visible = "False" Width="125px">
                <asp:ListItem Value="0">Insurance?</asp:ListItem>
                <asp:ListItem Value="1">Private</asp:ListItem>
                <asp:ListItem Value="2">Hospital</asp:ListItem>
                <asp:ListItem Value="3">Self funded</asp:ListItem>
                <asp:ListItem Value="4">Overseas</asp:ListItem>
                <asp:ListItem Value="5">Unclassified</asp:ListItem>
                <asp:ListItem Value="6">Unknown</asp:ListItem>
    </asp:DropDownList>

 <%--   <asp:DropDownList ID="MoveBed" runat="server" Font-Names="Times New Roman" style ="font-size:14pt" Visible = "False" Width="125px">
                <asp:ListItem Value="0">Move Bed</asp:ListItem>
    </asp:DropDownList>--%>
    &nbsp;



    <asp:Button ID="ProblemDetails" runat="server" Text="Expand"  style ="font-size:12pt" visible = "true"/> &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;

    <asp:Button ID="Refresh" runat="server" Text="Refresh"  style ="font-size:12pt" visible = "true"/><br />

    <asp:GridView ID="ProblemList" visible = "False" datakeynames = "ProblemID" runat="server" BackColor="White" BorderColor="#999999" 
        BorderStyle="None" BorderWidth="1px" CellPadding="3" GridLines="Vertical" AllowSorting="True" AutoGenerateEditButton="True" AllowPaging="True" DataSourceID="ProblemListDataSource" AutoGenerateColumns="False" OnRowDataBound="GreyInactiveProblems_RowDataBOund" >
        <RowStyle BackColor="#EEEEEE" ForeColor="Black" />
         <Columns>
             <asp:BoundField DataField="Created" HeaderText="" Visible="true" ReadOnly="True" />
             <asp:BoundField DataField="Problem" HeaderText="Problem" SortExpression="Problem" ><ItemStyle Width="50px" /></asp:BoundField>

<%--             <asp:BoundField DataField="Text" HeaderText="Text" SortExpression="Text">
                 <ControlStyle Width="400px" />
                 <ItemStyle Wrap = "true" Width="400px" />
             </asp:BoundField>--%>
             
             <asp:TemplateField HeaderText = "Text">
             <ItemTemplate>
             <asp:Label ID="TextLabel" runat="server" width = "400px" Text='<%#Eval("Text") %>'></asp:Label>
             </ItemTemplate>
             <EditItemTemplate>
             <asp:TextBox ID="TextTextbox" runat="server" textmode="MultiLine" Wrap="true" width = "390px" Height="60px" Text= '<%#Bind("Text") %>'></asp:TextBox>
             </EditItemTemplate>
             </asp:TemplateField>
             
             <asp:BoundField DataField="Finish" HeaderText="Finish" Visible="false" ReadOnly="True" />
             <asp:BoundField DataField="ProblemID" HeaderText="ProblemID" Visible="False" ReadOnly="True" />
            <asp:CheckBoxField DataField="Summary" HeaderText="Summ" />
         </Columns>
        <FooterStyle BackColor="#CCCCCC" ForeColor="Black" />
        <PagerStyle BackColor="#999999" ForeColor="Black" HorizontalAlign="Center" />
        <SelectedRowStyle BackColor="#008A8C" Font-Bold="True" ForeColor="White" />
        <HeaderStyle BackColor="White" Font-Bold="True" ForeColor="#990033" />
        <AlternatingRowStyle BackColor="Gainsboro" />
    </asp:GridView>


    <asp:SqlDataSource ID="ProblemListDataSource" runat="server" DatasourceMode ="DataSet" ConnectionString="<%$ ConnectionStrings:NeonatalConnectionString %>"
        SelectCommand="select ProblemID, [Problem#] as Problem, [Text1#] as Text, summary, isnull(convert(nvarchar(6),start),'') as Created, isnull(convert(nvarchar(6),finish),'') as Finish from [problems#] where neonatalid = @neoid order by isnull(finish, getdate()) desc, start desc" 
        UpdateCommand="UPDATE Problems# SET [Problem#] = @problem, [Text1#] = @Text, Summary = @summary where problemID = @ProblemID">
        <SelectParameters>
            <asp:QueryStringParameter Name="neoid" QueryStringField="@neoid" />
        </SelectParameters>
    </asp:SqlDataSource>
    
 <%--       </ContentTemplate>
    </asp:UpdatePanel>--%>
    
    <table>
        <tr>
            <td valign="top">
    <a href="#RespChartHolder">
    <asp:Label ID="RespSuppLabel"  runat ="server" ForeColor="#990033" Text="Respiratory:" Font-Bold="True" style ="font-size:16pt"></asp:Label></a> 
    <asp:Label ID="FiO2label" runat="server"> FiO2 </asp:Label><asp:DropDownList id="FiO2" runat="server" width = "70" style ="font-size:12pt" ForeColor="DarkGray">
                <asp:ListItem Value="" />
                <asp:ListItem Value="Air" />
                <asp:ListItem Value="21" />
                <asp:ListItem Value="22" />
                <asp:ListItem Value="23" />
                <asp:ListItem Value="24" />
                <asp:ListItem Value="25" />
                <asp:ListItem Value="26" />
                <asp:ListItem Value="27" />
                <asp:ListItem Value="28" />
                <asp:ListItem Value="29" />
                <asp:ListItem Value="30" />
                <asp:ListItem Value="31" />
                <asp:ListItem Value="32" />
                <asp:ListItem Value="33" />
                <asp:ListItem Value="34" />
                <asp:ListItem Value="35" />
                <asp:ListItem Value="36" />
                <asp:ListItem Value="37" />
                <asp:ListItem Value="38" />
                <asp:ListItem Value="39" />
                <asp:ListItem Value="40" />
                <asp:ListItem Value="45" />
                <asp:ListItem Value="50" />
                <asp:ListItem Value="55" />
                <asp:ListItem Value="60" />
                <asp:ListItem Value="65" />
                <asp:ListItem Value="70" />
                <asp:ListItem Value="75" />
                <asp:ListItem Value="80" />
                <asp:ListItem Value="85" />
                <asp:ListItem Value="90" />
                <asp:ListItem Value="95" />
                <asp:ListItem Value="100" />
    </asp:DropDownList> <br />
    <asp:Label ID="RespSupp"  runat ="server" Text="... to be retrieved" Width="280px" style ="font-size:14pt"></asp:Label>
    <asp:ImageButton ID="Imaging" runat="server" Height="50" ImageUrl="~/Images/XRayInvert.jpg" OnClientClick="aspnetForm.target = '_blank';"/> &nbsp;&nbsp;&nbsp;&nbsp;
    
    <asp:Panel ID="O2SatStudy" runat="server" Visible="false">
    Liverpool O2 Sat Survey<br />
    <asp:DropDownList id="O2Last24Hr" runat="server" width = "250" style ="font-size:12pt" ForeColor="Salmon">
    <asp:ListItem Value="Null">O2 given in last 24 hours?</asp:ListItem>
    <asp:ListItem Value="-1">Yes</asp:ListItem>
    <asp:ListItem Value="0">No</asp:ListItem>
    </asp:DropDownList><br />    
    <asp:DropDownList id="TargetCard" runat="server" width = "250" style ="font-size:12pt" ForeColor="Salmon">
    <asp:ListItem Value="Null">Target range on monitor?</asp:ListItem>
    <asp:ListItem Value="-1">Yes</asp:ListItem>
    <asp:ListItem Value="0">No</asp:ListItem>
    </asp:DropDownList><br />
    <asp:DropDownList id="SatRangeApprop" runat="server" width = "250" style ="font-size:12pt" ForeColor="Salmon">
    <asp:ListItem Value="Null">Sats range appropriate?</asp:ListItem>
    <asp:ListItem Value="-1">Yes</asp:ListItem>
    <asp:ListItem Value="0">No</asp:ListItem>
    </asp:DropDownList><br />
    <asp:DropDownList id="O2AnalyserApprop" runat="server" width = "250" style ="font-size:12pt" ForeColor="Salmon">
    <asp:ListItem Value="Null">O2 analyser set appropriate?</asp:ListItem>
    <asp:ListItem Value="-1">Yes</asp:ListItem>
    <asp:ListItem Value="0">No</asp:ListItem>
    </asp:DropDownList><br />
    <asp:DropDownList id="SkinToSkinLast24Hr" runat="server" width = "250" style ="font-size:12pt" ForeColor="Salmon">
    <asp:ListItem Value="Null">Skin to skin in last 24 hours?</asp:ListItem>
    <asp:ListItem Value="-1">Yes</asp:ListItem>
    <asp:ListItem Value="0">No</asp:ListItem>
    </asp:DropDownList><br />
    Why O2 inc <asp:TextBox ID="ReasonIncO2" runat="server" width = "170px" style ="font-size:11pt" ForeColor="Salmon"/> <br />
    Comments <asp:TextBox ID="Comments" runat="server" width = "180px" style ="font-size:11pt" ForeColor="Salmon"/> <br />
    <asp:Button ID="O2SatSubmit" runat="server" Text="Submit Liverpool O2 Sat Survey"  style ="font-size:12pt"/> 
    </asp:Panel>
    
    <br /><br />
    <a href="#GrowthChartholder">
        <asp:Label ID="NutritionLabel"  runat ="server" ForeColor="#990033" Text="Nutrition:" Font-Bold="True"  style ="font-size:16pt"></asp:Label></a>
    &nbsp;ml/kg/d&nbsp&nbsp<asp:DropDownList id="Volume" runat="server" width = "70" style ="font-size:12pt" >
                <asp:ListItem Value="" />
                <asp:ListItem Value="30" />
                <asp:ListItem Value="40" />
                <asp:ListItem Value="50" />
                <asp:ListItem Value="60" />
                <asp:ListItem Value="70" />
                <asp:ListItem Value="80" />
                <asp:ListItem Value="90" />
                <asp:ListItem Value="100" />
                <asp:ListItem Value="110" />
                <asp:ListItem Value="120" />
                <asp:ListItem Value="130" />
                <asp:ListItem Value="140" />
                <asp:ListItem Value="150" />
                <asp:ListItem Value="160" />
                <asp:ListItem Value="170" />
                <asp:ListItem Value="180" />
                <asp:ListItem Value="190" />
                <asp:ListItem Value="200" />
                <asp:ListItem Value="210" />
                <asp:ListItem Value="220" />
    </asp:DropDownList>
    <br />
    <asp:Label ID="Nutrition"  runat ="server" Text="... to be retrieved" Width="280px" style ="font-size:14pt"></asp:Label><br />
<asp:DropDownList id="Cal" runat="server" width = "85" style ="font-size:12pt" ForeColor="Salmon">
    <asp:ListItem Value="">Cals?</asp:ListItem>
    <asp:ListItem Value="20">20Cal</asp:ListItem>
    <asp:ListItem Value="22">22Cal</asp:ListItem>
    <asp:ListItem Value="24">24Cal</asp:ListItem>
    <asp:ListItem Value="26">26Cal</asp:ListItem>
    <asp:ListItem Value="28">28Cal</asp:ListItem>
    <asp:ListItem Value="30">30Cal</asp:ListItem>
    <asp:ListItem Value="32">32Cal</asp:ListItem>
    </asp:DropDownList> 
 
<asp:TextBox ID="ml" runat="server" width = "29px" style ="font-size:11pt" ForeColor="Salmon"/> 
    <asp:Label ID="mlLabel"  runat ="server" Text="Jaundice:" style ="font-size:12pt">ml</asp:Label>
    &nbsp;&nbsp;&nbsp;
<asp:DropDownList id="Hrly" runat="server" width = "77" style ="font-size:12pt" ForeColor="Salmon">
    <asp:ListItem Value="">Frequ?</asp:ListItem>
    <asp:ListItem Value="0">Cont.</asp:ListItem>
    <asp:ListItem Value="1">1hrly</asp:ListItem>
    <asp:ListItem Value="2">2hrly</asp:ListItem>
    <asp:ListItem Value="3">3hrly</asp:ListItem>
    <asp:ListItem Value="4">4hrly</asp:ListItem>
    <asp:ListItem Value="6">6hrly</asp:ListItem>
    <asp:ListItem Value="5">Demand</asp:ListItem>
    </asp:DropDownList> 
    <br />

<asp:DropDownList id="MilkRoute" runat="server" width = "150px" visible = "true" style ="font-size:12pt" ForeColor="Salmon" DataSourceID="MilkRouteDataSource" DataTextField="Label" DataValueField="Number">
    </asp:DropDownList>&nbsp;&nbsp;

<asp:DropDownList ID="BIntent" runat="server" Font-Names="Times New Roman" style ="font-size:12pt" ForeColor="Salmon" Visible = "false" Width="75px" >
            <asp:ListItem Value="">Intent?</asp:ListItem>
            <asp:ListItem Value="1">Bottle</asp:ListItem>
            <asp:ListItem Value="2">Both</asp:ListItem>
            <asp:ListItem Value="3">Breast</asp:ListItem>
</asp:DropDownList>

    <asp:Label ID="ConfirmLabel"  runat ="server" ForeColor="#990033" Text="Intent?" style ="font-size:12pt"></asp:Label>
    <br />

    <asp:TextBox id = "MilkHolder" runat="server" Width="17px" visible = "false"/>
<asp:DropDownList ID="Milk" runat="server" Font-Names="Times New Roman" forecolor="Salmon" style ="font-size:12pt" Width="150px" DatasourceID = "MilkDataSource" DataTextField="Label" DataValueField="Number">
</asp:DropDownList>
<asp:Button ID="MilkButton" runat="server" Text="Confirm"  style ="font-size:12pt" visible = "true"/> 
<br />    


<%--        <cc1:SliderExtender ID="SliderExtender2" runat="server" BehaviorID="FormulaEBM" Length="130" TargetControlID="FormulaEBM" BoundControlID="SliderLabel2" Minimum="0" Maximum="100" Steps="11" >
        </cc1:SliderExtender>
<table><tr><td  style="width:50px">
        Form.</td><td>
<asp:TextBox ID="FormulaEBM"  runat="server" width = "120px" style ="font-size:11pt"  AutoPostBack="true" />
</td><td>EBM
<asp:Label ID="SliderLabel2" visible = "true" runat ="server" Text="" ForeColor ="gray" />
</td></tr></table>    
    
        <cc1:SliderExtender ID="SliderExtender1" runat="server" BehaviorID="DonorMother" Length="130" TargetControlID="DonorMother" BoundControlID="SliderLabel" Minimum="0" Maximum="100" Steps="11" TooltipText ="Approximate percentage" >
        </cc1:SliderExtender>
<table><tr><td  style="width:50px">
        Donor</td><td>
<asp:TextBox ID="DonorMother"  runat="server" width = "120px" style ="font-size:11pt" text="50" AutoPostBack="true" />
</td><td>Mother
        <asp:Label ID="SliderLabel" visible = "true" runat ="server" Text="" ForeColor ="gray" />
</td></tr></table> 
--%>


&nbsp;Nipple shield <asp:CheckBox ID = "Nippleshield" runat="server"></asp:CheckBox>    
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Kangaroo <asp:CheckBox ID = "Kangaroo" runat="server"></asp:CheckBox>   <br />
&nbsp;First expressed &nbsp;<asp:TextBox ID="FirstExpress" runat="server" width = "150px" style ="font-size:11pt" ForeColor="Salmon"/> 
    <br /> 
    
    <asp:DropDownList id="StopCatheter" runat="server" width = "150px" visible = "true" forecolor = "CornflowerBlue" style ="font-size:10pt" DataSourceID="CatheterDataSource2" DataTextField="Label" DataValueField="Number">
    </asp:DropDownList>&nbsp;
    <asp:DropDownList id="StartCatheter" runat="server" width = "110px" visible = "true" forecolor = "CornflowerBlue" style ="font-size:10pt" DataSourceID="CatheterDataSource1" DataTextField="Label" DataValueField="Number">
    </asp:DropDownList>
<br />
<br />
    <a href="#JaundiceChartholder">
    
    <asp:Label ID="JaundiceLabel"  runat ="server" ForeColor="#990033" Text="Jaundice:" Font-Bold="True"  style ="font-size:16pt"></asp:Label></a><br />
    <asp:Label ID="Jaundice"  runat ="server" Text="... to be retrieved" Width="280px" style ="font-size:14pt"></asp:Label><br />
<br />
    <a href="#ResultsChartHolder">
    <asp:Label ID="ResultsLabel"  runat ="server" ForeColor="#990033" Text="Test Results:" Font-Bold="True"  style ="font-size:16pt"></asp:Label></a><br />
    <asp:Label ID="Results"  runat ="server" Text="... to be retrieved" Width="280px" style ="font-size:13pt"></asp:Label>
            </td>
            <td valign="top">
    <asp:Label ID="ProblemsLabel"  runat ="server" ForeColor="#990033" Text="Problems:" Font-Bold="True"  style ="font-size:16pt"></asp:Label>
   &nbsp&nbsp+&nbsp
   <asp:TextBox ID="Problem" runat="server" width = "100px" AutoPostBack="false"  style ="font-size:12pt"/>
   <br />
   <asp:Label ID="Problems"  runat ="server" Text="" Width="250px" style ="font-size:14pt"></asp:Label><br />
   <asp:DropDownList id="InactivateProblem" runat="server" width = "118px" visible = "true" ForeColor="DarkGray" style ="font-size:12pt" DataSourceID="ProblemDataSource" DataTextField="Problem#" DataValueField="Problem#" autopostback="true"></asp:DropDownList><br />
   <asp:Label ID="Problems2"  runat ="server" Text="" Width="250px" style ="font-size:14pt" ForeColor="DarkGray"></asp:Label><br />

    <asp:Label ID="MedicationsLabel"  runat ="server" ForeColor="#990033" Text="Treatment:" Font-Bold="True"  style ="font-size:16pt"></asp:Label>
    +&nbsp <asp:DropDownList id="AddTreatment" runat="server" width = "110px" visible = "true" style ="font-size:12pt" DataSourceID="TreatmentDataSource" DataTextField="Generic" DataValueField="Number"></asp:DropDownList>
    <br />
    <asp:Label ID="Medications"  runat ="server" Text="" Width="250px" style ="font-size:14pt"></asp:Label><br />
    <asp:Label ID="StopTreatmentLabel"  runat ="server" ForeColor="#990033" Text="" style ="font-size:12pt"></asp:Label>
 <asp:DropDownList id="StopTreatment" runat="server" width = "118px" visible = "true" ForeColor="DarkGray" style ="font-size:12pt" DataSourceID="TreatmentDataSource2" DataTextField="Generic" DataValueField="Number"></asp:DropDownList>
<br />
    <asp:Label ID="MedNote"  runat ="server" Text="" Width="250px" style ="font-size:14pt" ForeColor="#990033"></asp:Label><br />
    <asp:TextBox ID="MedNote2" runat="server" textmode="MultiLine" onkeypress="return textboxMultilineMaxNumber(this,500)" Wrap="true" width = "250px" Height="60px" Visible = "false" Text= '<%#Bind("Text") %>'></asp:TextBox>
    <asp:Label ID="MedOrders"  runat ="server" Text="" Width="250px"  style ="font-size:14pt"></asp:Label>
    &nbsp<asp:Label ID="NewOrderLabel"  runat ="server" ForeColor="#990033" Text="<br/>New Order " style ="font-size:12pt"></asp:Label>
    <asp:TextBox ID="Order" runat="server" width = "128px" AutoPostBack="false"  style ="font-size:12pt"/>
    <br /><asp:TextBox ID="MedOrders2" runat="server" textmode="MultiLine" onkeypress="return textboxMultilineMaxNumber(this,100)" Wrap="true" width = "250px" Height="60px" Visible = "false" Text= '<%#Bind("Text") %>'></asp:TextBox>

<br />Transfer 
 <asp:DropDownList id="THospList" runat="server" width = "170px" visible = "true" style ="font-size:12pt" DataSourceID="THospDataSource" DataTextField="HospName" DataValueField="HospitalID"></asp:DropDownList>

            </td>
        </tr>
    </table>
   
          </ContentTemplate>
    </asp:UpdatePanel> 

<asp:UpdatePanel ID="UpdatePanel1" runat="server">
<ContentTemplate>



    <asp:GridView ID="PendingCulture" visible = "true" datakeynames = "InfectionID" runat="server" BackColor="White" BorderColor="#999999" 
        BorderStyle="None" BorderWidth="1px" CellPadding="3" GridLines="Vertical" AllowSorting="false" AutoGenerateEditButton="True" 
        AllowPaging="True" DataSourceID="PendingListDataSource" AutoGenerateColumns="False" >
        <RowStyle BackColor="#EEEEEE" ForeColor="Black" />
         <Columns>
             <asp:BoundField DataField="Date" HeaderText="Date" ReadOnly="True" ><ItemStyle Width="80px" /></asp:BoundField>
             <asp:BoundField DataField="Site2" HeaderText="Site" ReadOnly="True" ><ItemStyle Width="100px" /></asp:BoundField>
<%--             <asp:TemplateField HeaderText = "Site">
             <ItemTemplate>
           <asp:Label ID="TextLabel" runat="server" width = "100px" Text='<%#Eval("Site2") %>'></asp:Label>
             </ItemTemplate>
             <EditItemTemplate>
           <asp:DropDownList ID="SiteList" width = "100px" style ="font-size:12pt" runat="server" DataSourceID="SiteDataSource" 
           DataTextField="label" DataValueField="Number" selectedvalue = '<%#Eval("Site") %>'></asp:DropDownList>
             </EditItemTemplate>
             </asp:TemplateField> --%>
             <asp:TemplateField HeaderText = "Organsim">
             <ItemTemplate>
           <asp:Label ID="TextLabel" runat="server" width = "120px" Text='<%#Eval("Org2") %>'></asp:Label>
             </ItemTemplate>
             <EditItemTemplate>
           <asp:DropDownList ID="OrgList2" width = "120px" style ="font-size:12pt" runat="server" DataSourceID="OrgDataSource" 
           DataTextField="label" DataValueField="Number" selectedvalue = '<%#Bind("Org") %>'></asp:DropDownList>
             </EditItemTemplate>
             </asp:TemplateField> 
             <asp:BoundField DataField="Oorg" HeaderText="Comment" ><ItemStyle Width="100px" /></asp:BoundField>
            <asp:BoundField DataField="InfectionID" HeaderText="InfectionID" Visible="False" ReadOnly="True" />
             
            <%--<asp:CheckBoxField DataField="Summary" HeaderText="Summ" />--%>
                     </Columns>
        <FooterStyle BackColor="#CCCCCC" ForeColor="Black" />
        <PagerStyle BackColor="#999999" ForeColor="Black" HorizontalAlign="Center" />
        <SelectedRowStyle BackColor="#008A8C" Font-Bold="True" ForeColor="White" />
        <HeaderStyle BackColor="White" Font-Bold="True" ForeColor="#990033" />
        <AlternatingRowStyle BackColor="Gainsboro" />
    </asp:GridView>
    <asp:SqlDataSource ID="PendingListDataSource" runat="server" DatasourceMode ="DataSet" ConnectionString="<%$ ConnectionStrings:NeonatalConnectionString %>"
        SelectCommand="select InfectionID, left(Date,11) as date, Site, Org, Oorg, _site.label as Site2, _org.label as Org2 from sepsis inner join _site on _site.number = sepsis.site left outer join _org on _org.number = sepsis.org where neonatalid = @neoid and (org = -1 or org is null) and date > DateAdd(d, -14, getdate()) order by date desc" 
        UpdateCommand="UPDATE sepsis SET Org = @Org, Oorg = @Oorg where infectionID = @InfectionID">
        <SelectParameters>
            <asp:QueryStringParameter Name="neoid" QueryStringField="@neoid" />
        </SelectParameters>
    </asp:SqlDataSource>
    
        <asp:SqlDataSource ID="SiteDataSource" runat="server" DatasourceMode ="DataSet" ConnectionString="<%$ ConnectionStrings:NeonatalConnectionString %>"
        SelectCommand="select number, label from _site order by number" >
        </asp:SqlDataSource>
                <asp:SqlDataSource ID="OrgDataSource" runat="server" DatasourceMode ="DataSet" ConnectionString="<%$ ConnectionStrings:NeonatalConnectionString %>"
        SelectCommand="select number, label from _org order by number" >
        </asp:SqlDataSource>
    
<asp:UpdateProgress ID = "UpdateProgress1" runat = "server" AssociatedUpdatePanelID = "UpdatePanel1" DisplayAfter="10">
<ProgressTemplate>
retrieving data &nbsp;....
</ProgressTemplate>
</asp:UpdateProgress>


        <asp:Table ID="NewResults" runat="server" Visible="false">
            <asp:TableRow ID="TableRow1" runat="server" BackColor= "lightGray">
                <asp:TableCell ID="TableCell1" runat="server" forecolor = "DarkBlue">Na</asp:TableCell>
                <asp:TableCell ID="TableCell2" runat="server" forecolor = "DarkBlue">K</asp:TableCell>
                <asp:TableCell ID="TableCell3" runat="server" forecolor = "DarkBlue">Creat</asp:TableCell>
                <asp:TableCell ID="TableCell4" runat="server" forecolor = "DarkBlue">CRP</asp:TableCell>
                <asp:TableCell ID="TableCell5" runat="server" forecolor = "maroon">Hb</asp:TableCell>
                <asp:TableCell ID="TableCell6" runat="server" forecolor = "maroon">WCC</asp:TableCell>
                <asp:TableCell ID="TableCell7" runat="server" forecolor = "maroon">Plat</asp:TableCell>
                <asp:TableCell ID="TableCell8" runat="server" forecolor = "maroon">Retic</asp:TableCell>
                <asp:TableCell ID="TableCell9" runat="server" forecolor = "goldenrod">SBr</asp:TableCell>
                <asp:TableCell ID="TableCell10" runat="server" forecolor = "goldenrod">BSL</asp:TableCell>
                <asp:TableCell ID="TableCell10b" runat="server" forecolor = "goldenrod">Other</asp:TableCell>
            </asp:TableRow>
                        <asp:TableRow ID="TableRow2" runat="server">
                <asp:TableCell ID="TableCell11" runat="server">
                <asp:TextBox ID="Na" runat="server" Width="30px"></asp:TextBox>
                </asp:TableCell>
                <asp:TableCell ID="TableCell12" runat="server">
                <asp:TextBox ID="K" runat="server" Width="30px"></asp:TextBox>
                </asp:TableCell>
                <asp:TableCell ID="TableCell13" runat="server">
                <asp:TextBox ID="Creat" runat="server" Width="30px"></asp:TextBox>
                </asp:TableCell>
                <asp:TableCell ID="TableCell14" runat="server">
                <asp:TextBox ID="CRP" runat="server" Width="30px"></asp:TextBox>
                </asp:TableCell>
                <asp:TableCell ID="TableCell15" runat="server">
                <asp:TextBox ID="Hb" runat="server" Width="30px"></asp:TextBox>
                </asp:TableCell>
                <asp:TableCell ID="TableCell16" runat="server">
                <asp:TextBox ID="WCC" runat="server" Width="30px"></asp:TextBox>
                </asp:TableCell>
                <asp:TableCell ID="TableCell17" runat="server">
                <asp:TextBox ID="Plat" runat="server" Width="30px"></asp:TextBox>
                </asp:TableCell>
                <asp:TableCell ID="TableCell18" runat="server">
                <asp:TextBox ID="Retic" runat="server" Width="30px"></asp:TextBox>
                </asp:TableCell>
                <asp:TableCell ID="TableCell19" runat="server">
                <asp:TextBox ID="Bili" runat="server" Width="30px"></asp:TextBox>
                </asp:TableCell>
                <asp:TableCell ID="TableCell20" runat="server">
                <asp:TextBox ID="BSL" runat="server" Width="30px"></asp:TextBox>
                </asp:TableCell>
                <asp:TableCell ID="TableCell21" runat="server">
                <asp:TextBox ID="Other" runat="server" Width="110px"></asp:TextBox>
                </asp:TableCell>
            </asp:TableRow>
        </asp:Table>
        <asp:Label ID = "ResultsLabel2" runat="server" Visible="false">Date of Results </asp:Label>
        <asp:TextBox ID="ResultsDate" runat="server" width = "145px" Visible="false"></asp:TextBox>&nbsp;
        <asp:Button ID="SendResults" runat="server" Text="Submit Results" Width="250px"  Visible="false"/>
        <asp:Label ID = "ResultsLabel3" runat="server" Visible="false"><br /></asp:Label>
        
      
    
    <asp:Table ID="Table1" width = "525px" runat="server">
        <asp:TableRow runat="server">
            <asp:TableCell runat="server">
    <asp:Button ID="ShowNursing" runat="server" Text="Nursing Notes"  style ="font-size:12pt" visible = "true"/>
    <asp:Button ID="ShowGrowthChart" runat="server" Text="Growth Chart"  style ="font-size:12pt"  Visible="false"/>
    <asp:Button ID="ShowJaundiceChart" runat="server" Text="Jaundice Chart"  style ="font-size:12pt" Visible="false"/>
        <asp:Label ID = "vaccination" runat="server" Visible="true"></asp:Label>
            </asp:TableCell>
            <asp:TableCell runat="server" HorizontalAlign="Right">
    <asp:Button ID="Refresh2" runat="server" Text="Refresh"  style ="font-size:12pt" visible = "true"/><br />
            </asp:TableCell>
        </asp:TableRow>
    </asp:Table>
<br/>
<asp:Label ID="NursingLabel"  runat = "server" visible = "false" Forecolor="#990033" Text="Nursing Issues:" Font-Bold="True"  style ="font-size:16pt"></asp:Label>
    <asp:Button ID="EditDiagnosisComment" runat="server" Text="Edit"  style ="font-size:12pt" Visible="false"/>
    <br />
<asp:TextBox ID="diagnosiscomment" runat="server" textmode="MultiLine" onkeypress="return textboxMultilineMaxNumber(this,500)" Wrap="true" width = "530px" Height="120px" Visible = "false" Text= '<%#Bind("Text") %>' ForeColor="purple" ></asp:TextBox>
<asp:Label ID="diagnosiscommentLabel"  runat = "server" visible = "false" width = "530px" Forecolor="purple" Text="" style ="font-size:12pt" ></asp:Label>
<asp:Label ID="NursingLabel2"  runat = "server" visible = "false" Forecolor="#990033" Text="<br /> Daily Notes: autodate->" Font-Bold="True"  style ="font-size:12pt"></asp:Label>
   <asp:TextBox ID="NursingAdd" runat="server" AutoPostBack = "true" visible = "false" width = "350px" style ="font-size:12pt" BackColor = "PaleTurquoise"/> 

<%--    <asp:Label ID="NurseSummary"  runat ="server"  ForeColor="purple" Text="" Width="550px" visible = "false"></asp:Label>
--%>
    <br /><asp:TextBox ID="NurseSummary" runat="server" textmode="MultiLine" ForeColor="purple" AutoPostBack = "true" visible = "false" onkeypress="return textboxMultilineMaxNumber(this,2000)" Wrap="true" width = "530px" Height="120px" ></asp:TextBox>
<br />
    <a name="GrowthChartholder" runat="server"></a>
<a href="#top">Top</a>
<br/>
Add Weight <asp:TextBox ID="Wgt" runat="server" width = "38px" style ="font-size:11pt"/> HC <asp:TextBox ID="HC" runat="server" width = "38px" style ="font-size:11pt"/>
 on <asp:TextBox ID="WgtDate" runat="server" width = "100px" style ="font-size:11pt"/> 
<asp:Button ID="WgtSubmit" runat="server" Text="Submit"  style ="font-size:12pt" visible = "true"/> 
&nbsp;&nbsp;&nbsp;
<br />
    <asp:Chart id="GrowthChart1" DataSourceID="GrowthChartDataSource" runat="server" Height="296px" Width="550px" BorderColor="#1A3B69" BackColor="WhiteSmoke" BorderWidth="2px" BackGradientStyle="TopBottom" BackSecondaryColor="White" BorderDashStyle="Solid" >
<Series>
<asp:Series Color = "red" Name="WGT" XValueMember="GA" YValueMembers="weight" ChartType="Line" BorderWidth="5"/>
<asp:Series Color = "red" Name="WGT2" XValueMember="GA" YValueMembers="weight" ChartType="point" BorderWidth="5"/>
<asp:Series Color = "blue" Name="Mean" XValueMember="GA" YValueMembers="wgt_mean" ChartType="Line"/>
<asp:Series Color = "aqua" Name="97%" XValueMember="GA" YValueMembers="wgt_high" ChartType="Line"/>
<asp:Series Color = "aqua" Name="3%" XValueMember="GA" YValueMembers="wgt_low" ChartType="Line"/>
</Series>
<titles>
<asp:title ShadowColor="32, 0, 0, 0" Font="Trebuchet MS, 12pt, style=Bold" ShadowOffset="3"  ForeColor="26, 59, 105" Text="Growth Chart" ></asp:title>
<asp:title ShadowColor="32, 0, 0, 0" Font="Trebuchet MS, 8pt, style=Bold" ShadowOffset="3"  ForeColor="26, 59, 105" Text="" ></asp:title>
</titles>
<%--<legends><asp:legend IsTextAutoFit="False" Name="Default" BackColor="Transparent" Font="Trebuchet MS, 8.25pt, style=Bold"></asp:legend></legends>--%>
<borderskin skinstyle="Emboss"></borderskin>
<chartareas><asp:chartarea Name="ChartArea1" BorderColor="64, 64, 64, 64" BorderDashStyle="Solid" BackSecondaryColor="White" BackColor="Gainsboro" ShadowColor="Transparent" BackGradientStyle="TopBottom">
<area3dstyle Rotation="10" perspective="10" Inclination="15" IsRightAngleAxes="False" wallwidth="0" IsClustered="False"></area3dstyle>
<axisy linecolor="64, 64, 64, 64" IsLabelAutoFit="False"><labelstyle font="Trebuchet MS, 8.25pt, style=Bold" /><majorgrid linecolor="64, 64, 64, 64" /></axisy>
<axisx Minimum = "24" interval = "1" linecolor="64, 64, 64, 64" IsLabelAutoFit="False"><labelstyle font="Trebuchet MS, 8.25pt, style=Bold" /><majorgrid linecolor="64, 64, 64, 64" /></axisx>
</asp:chartarea></chartareas></asp:Chart> 
<br/>
<a name="RespChartHolder" runat="server"></a>
<asp:TextBox id = "respsuppholder" runat="server" Width="17px" visible = "false"/>
<asp:TextBox id = "TextBox1" runat="server" Width="17px" visible = "false"/>
<a href="#top">Top</a>&nbsp;&nbsp;&nbsp; <strong>Respiratory Support </strong><br /> 
Mode <asp:DropDownList ID="RespSuppChange" runat="server" Font-Names="Times New Roman" style ="font-size:14pt" Width="230px">
            <asp:ListItem Value="">No Support</asp:ListItem>
            <asp:ListItem Value="1">ECMO</asp:ListItem>
            <asp:ListItem Value="2">HFO</asp:ListItem>
            <asp:ListItem Value="3">IMV / SIMV</asp:ListItem>
            <asp:ListItem Value="4">CPAP</asp:ListItem>
            <asp:ListItem Value="5">Nasal IMV / SIMV</asp:ListItem>
            <asp:ListItem Value="7">Nasal Hi Flow Device</asp:ListItem>
<%--            <asp:ListItem Value="8">Vapotherm</asp:ListItem>--%>
            <asp:ListItem Value="10">Oxygen</asp:ListItem>
            <asp:ListItem Value="">No Support</asp:ListItem>
</asp:DropDownList>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
<asp:Button ID="RespSuppButton" runat="server" Text="Confirm"  style ="font-size:12pt" visible = "true"/> 

<br/>
FiO2 &nbsp;<asp:DropDownList id="FiO2b" runat="server" width = "70" style ="font-size:12pt">
                <asp:ListItem Value="" />
                <asp:ListItem Value="Air" />
                <asp:ListItem Value="21" />
                <asp:ListItem Value="22" />
                <asp:ListItem Value="23" />
                <asp:ListItem Value="24" />
                <asp:ListItem Value="25" />
                <asp:ListItem Value="26" />
                <asp:ListItem Value="27" />
                <asp:ListItem Value="28" />
                <asp:ListItem Value="29" />
                <asp:ListItem Value="30" />
                <asp:ListItem Value="31" />
                <asp:ListItem Value="32" />
                <asp:ListItem Value="33" />
                <asp:ListItem Value="34" />
                <asp:ListItem Value="35" />
                <asp:ListItem Value="36" />
                <asp:ListItem Value="37" />
                <asp:ListItem Value="38" />
                <asp:ListItem Value="39" />
                <asp:ListItem Value="40" />
                <asp:ListItem Value="45" />
                <asp:ListItem Value="50" />
                <asp:ListItem Value="55" />
                <asp:ListItem Value="60" />
                <asp:ListItem Value="65" />
                <asp:ListItem Value="70" />
                <asp:ListItem Value="75" />
                <asp:ListItem Value="80" />
                <asp:ListItem Value="85" />
                <asp:ListItem Value="90" />
                <asp:ListItem Value="95" />
                <asp:ListItem Value="100" />
    </asp:DropDownList>
&nbsp;
<asp:Label ID="PIPLabel" runat="server" Text="PIP  &nbsp;&nbsp;"> </asp:Label>
<asp:DropDownList ID="PipData" runat="server" Font-Names="Times New Roman" style ="font-size:14pt" Width="70px"></asp:DropDownList>
<asp:Label ID="MAPLabel" runat="server" Text="PEEP"> </asp:Label>
<asp:DropDownList ID="MAPData" runat="server" Font-Names="Times New Roman" style ="font-size:14pt" Width="70px"></asp:DropDownList>
<asp:Label ID="VGLabel" runat="server" Text="VG(ml)"> </asp:Label>
<asp:DropDownList ID="VgData" runat="server" Font-Names="Times New Roman" style ="font-size:14pt" Width="70px"></asp:DropDownList>
<asp:Label ID="RateLabel" runat="server" Text="Rate"> </asp:Label>
<asp:DropDownList ID="RateData" runat="server" Font-Names="Times New Roman" style ="font-size:14pt" Width="70px"></asp:DropDownList>
<asp:Label ID="FlowLabel" runat="server" Text="Flow"> </asp:Label> 
<asp:DropDownList ID="FlowData" runat="server" Font-Names="Times New Roman" style ="font-size:14pt" Width="70px">
            <asp:ListItem Value="" />
            <asp:ListItem>0.1</asp:ListItem>
            <asp:ListItem>0.2</asp:ListItem>
            <asp:ListItem>0.3</asp:ListItem>
            <asp:ListItem>0.5</asp:ListItem>
            <asp:ListItem>1</asp:ListItem>
            <asp:ListItem>2</asp:ListItem>
            <asp:ListItem>3</asp:ListItem>
            <asp:ListItem>4</asp:ListItem>
            <asp:ListItem>5</asp:ListItem>
            <asp:ListItem>6</asp:ListItem>
            <asp:ListItem>7</asp:ListItem>
            <asp:ListItem>8</asp:ListItem>
            <asp:ListItem>9</asp:ListItem>
            <asp:ListItem>10</asp:ListItem>
</asp:DropDownList>
&nbsp;&nbsp;
Circuit change due <asp:TextBox id = "Circuit" runat="server" Width="120px" visible = "true" Text = "Not in Use"/>

<br/>


<asp:Chart id="RespChart" DataSourceID="RespChartDataSource" runat="server" Height="296px" Width="550px" BorderColor="#1A3B69" BackColor="WhiteSmoke" BorderWidth="2px" BackGradientStyle="TopBottom" BackSecondaryColor="White" BorderDashStyle="Solid" >
<Series>
<asp:Series Color = "Orange" Name="Steroid" XValueMember="Age" YValueMembers="Steroid" ChartType="Line" BorderWidth="3"/>
<asp:Series Color= "Aquamarine" Name="Diuretic" XValueMember="Age" YValueMembers="Diuretic" ChartType="Line" BorderWidth="3"/>
<asp:Series Color = "pink" Name="Vasodilator" XValueMember="Age" YValueMembers="Vasodilator" ChartType="Line" BorderWidth="3"/>
<asp:Series Color = "red" Name="FiO2" XValueMember="Age" YValueMembers="FiO2" ChartType="Line" BorderWidth="3"/>
<asp:Series Color = "gray" Name="MAP/PEEP" XValueMember="Age" YValueMembers="MAP/PEEP" ChartType="Line" BorderWidth="3"/>
<asp:Series Color = "black" Name="HFO" XValueMember="Age" YValueMembers="HFO" ChartType="Line" BorderWidth="6"/>
<asp:Series Color = "DarkBlue" Name="IMV" XValueMember="Age" YValueMembers="IMV" ChartType="Line" BorderWidth="6"/>
<asp:Series Color = "aqua" Name="CPAP" XValueMember="Age" YValueMembers="CPAP" ChartType="Line" BorderWidth="6"/>
<asp:Series Color = "yellow" Name="HiFLow" XValueMember="Age" YValueMembers="Hi FLow" ChartType="Line" BorderWidth="6"/>
<asp:Series Color = "pink" Name="Oxygen" XValueMember="Age" YValueMembers="Oxygen" ChartType="Line" BorderWidth="6"/>
<asp:Series Color = "orange" Name="Surfactant" XValueMember="Age" YValueMembers="Surfactant" ChartType="Point" BorderWidth="5"/>
<asp:Series Color = "yellow" Name="Now" XValueMember="Age" YValueMembers="Now" ChartType="Line" BorderWidth="2"/>
</Series>
<titles>
<asp:title ShadowColor="32, 0, 0, 0" Font="Trebuchet MS, 12pt, style=Bold" ShadowOffset="3"  ForeColor="26, 59, 105" Text="Respiratory Support" ></asp:title>
<asp:title ShadowColor="32, 0, 0, 0" Font="Trebuchet MS, 8pt, style=Bold" ShadowOffset="3"  ForeColor="26, 59, 105" Text="" ></asp:title>
</titles>
<legends><asp:legend IsTextAutoFit="False" Name="Default" BackColor="Transparent" Font="Trebuchet MS, 8.25pt, style=Bold"></asp:legend></legends>
<borderskin skinstyle="Emboss"></borderskin>
<chartareas><asp:chartarea Name="ChartArea1" BorderColor="64, 64, 64, 64" BorderDashStyle="Solid" BackSecondaryColor="White" BackColor="Gainsboro" ShadowColor="Transparent" BackGradientStyle="TopBottom">
<area3dstyle Rotation="10" perspective="10" Inclination="15" IsRightAngleAxes="False" wallwidth="0" IsClustered="False"></area3dstyle>
<axisy minimum = "0" Interval = "10" linecolor="64, 64, 64, 64" IsLabelAutoFit="False"><labelstyle font="Trebuchet MS, 8.25pt, style=Bold" /><majorgrid linecolor="64, 64, 64, 64" /></axisy>
<axisx Minimum = "0" interval = "1" linecolor="64, 64, 64, 64" IsLabelAutoFit="False"><labelstyle font="Trebuchet MS, 8.25pt, style=Bold" /><majorgrid linecolor="64, 64, 64, 64" /></axisx>
</asp:chartarea></chartareas></asp:Chart> 


<br />
<asp:Chart id="resultsChart2" DataSourceID="resultsChart2DataSource" runat="server" Height="296px" Width="550px" BorderColor="#1A3B69" BackColor="WhiteSmoke" BorderWidth="2px" BackGradientStyle="TopBottom" BackSecondaryColor="White" BorderDashStyle="Solid" >
<Series>
<asp:Series Color = "red" Name="Hb" XValueMember="Age" YValueMembers="Hb" ChartType="Line" BorderWidth="3"/>
<asp:Series Color = "purple" Name="Plat" XValueMember="Age" YValueMembers="Plat" ChartType="Line" BorderWidth="3"/>
<asp:Series Color = "DarkGreen" Name="WCC x10" XValueMember="Age" YValueMembers="WCC x10" ChartType="Line" BorderWidth="3"/>
<asp:Series Color = "lime" Name="Neut x10" XValueMember="Age" YValueMembers="Neut x10" ChartType="Line" BorderWidth="3"/>
<%--<asp:Series Color = "red" Name=" " XValueMember="Age" YValueMembers="Hb" ChartType="point" BorderWidth="3"/>
<asp:Series Color = "purple" Name="  " XValueMember="Age" YValueMembers="Plat" ChartType="point" BorderWidth="3"/>
--%>
<asp:Series Color = "red" Name="RBC Tx" XValueMember="Age" YValueMembers="RBC Tx" ChartType="Point" BorderWidth="10"/>
<asp:Series Color = "purple" Name="Plat Tx" XValueMember="Age" YValueMembers="Plat Tx" ChartType="Point" BorderWidth="10"/>
<asp:Series Color = "aqua" Name="Antibiotics" XValueMember="Age" YValueMembers="Antibiotics" ChartType="Line" BorderWidth="2"/>
<asp:Series Color = "Orange" Name="BC" XValueMember="Age" YValueMembers="BC" ChartType="Point" BorderWidth="10"/>
<asp:Series Color = "Orange" Name="CVL" XValueMember="Age" YValueMembers="CVL" ChartType="Line" BorderWidth="2"/>
<asp:Series Color= "darkgreen" Name="Dilution" XValueMember="Age" YValueMembers="Dilution" ChartType="Point" BorderWidth="2"/>
<asp:Series Color = "gray" Name="Iron" XValueMember="Age" YValueMembers="Iron" ChartType="Line" BorderWidth="2"/>
<asp:Series Color = "yellow" Name="Now" XValueMember="Age" YValueMembers="Now" ChartType="Line" BorderWidth="3"/>
</Series>
<titles>
<asp:title ShadowColor="32, 0, 0, 0" Font="Trebuchet MS, 12pt, style=Bold" ShadowOffset="3"  ForeColor="26, 59, 105" Text="Haematology Trends" ></asp:title>
<asp:title ShadowColor="32, 0, 0, 0" Font="Trebuchet MS, 8pt, style=Bold" ShadowOffset="3"  ForeColor="26, 59, 105" Text="Plat>300 & WCC>30 x10 have been truncated" ></asp:title>
</titles>
<legends><asp:legend IsTextAutoFit="False" Name="Default" BackColor="Transparent" Font="Trebuchet MS, 8.25pt, style=Bold"></asp:legend></legends>
<borderskin skinstyle="Emboss"></borderskin>
<chartareas><asp:chartarea Name="ChartArea1" BorderColor="64, 64, 64, 64" BorderDashStyle="Solid" BackSecondaryColor="White" BackColor="Gainsboro" ShadowColor="Transparent" BackGradientStyle="TopBottom">
<area3dstyle Rotation="10" perspective="10" Inclination="15" IsRightAngleAxes="False" wallwidth="0" IsClustered="False"></area3dstyle>
<axisy minimum = "0" Maximum = "300" Interval = "50" linecolor="64, 64, 64, 64" IsLabelAutoFit="False"><labelstyle font="Trebuchet MS, 8.25pt, style=Bold" /><majorgrid linecolor="64, 64, 64, 64" /></axisy>
<axisx Minimum = "0" interval = "1" linecolor="64, 64, 64, 64" IsLabelAutoFit="False"><labelstyle font="Trebuchet MS, 8.25pt, style=Bold" /><majorgrid linecolor="64, 64, 64, 64" /></axisx>
</asp:chartarea></chartareas></asp:Chart> 

<br />
<a id="A1" name="ResultsChartHolder" runat="server"></a>
<asp:Chart id="ResultsChart1" DataSourceID="resultsChart1DataSource" runat="server" Height="296px" Width="550px" BorderColor="#1A3B69" BackColor="WhiteSmoke" BorderWidth="2px" BackGradientStyle="TopBottom" BackSecondaryColor="White" BorderDashStyle="Solid" >
<Series>
<asp:Series Color = "darkblue" Name="Sodium" XValueMember="Age" YValueMembers="Sodium" ChartType="Line" BorderWidth="3"/>
<asp:Series Color = "darkblue" Name=" " XValueMember="Age" YValueMembers="Sodium" ChartType="Point" BorderWidth="10"/>
<asp:Series Color = "orange" Name="NaCl supp" XValueMember="Age" YValueMembers="NaCl supp" ChartType="Line" BorderWidth="3"/>
<asp:Series Color = "aqua" Name="Diuretic" XValueMember="Age" YValueMembers="Diuretic" ChartType="Line" BorderWidth="3"/>
<asp:Series Color = "red" Name="Ibuprofen" XValueMember="Age" YValueMembers="Ibuprofen" ChartType="Line" BorderWidth="3"/>
<asp:Series Color = "yellow" Name="Now" XValueMember="Age" YValueMembers="Now" ChartType="Line" BorderWidth="5"/>
</Series>
<Series>
<asp:Series Color = "green" Name="BSL" XValueMember="Age" YValueMembers="BSL" Chartarea = "chartarea2" ChartType="Line" BorderWidth="3"/>
<asp:Series Color = "green" Name="  " XValueMember="Age" YValueMembers="BSL" Chartarea = "chartarea2" ChartType="point" BorderWidth="10"/>
</Series>
<titles>
<asp:title ShadowColor="32, 0, 0, 0" Font="Trebuchet MS, 12pt, style=Bold" ShadowOffset="3"  ForeColor="26, 59, 105" Text="Biochemistry Trends" ></asp:title>
<asp:title ShadowColor="32, 0, 0, 0" Font="Trebuchet MS, 8pt, style=Bold" ShadowOffset="3"  ForeColor="26, 59, 105" Text="" ></asp:title>
</titles>
<legends><asp:legend IsTextAutoFit="False" Name="Default" BackColor="Transparent" Font="Trebuchet MS, 8.25pt, style=Bold"></asp:legend></legends>
<borderskin skinstyle="Emboss"></borderskin>
<chartareas>
<asp:chartarea Name="chartarea1" BorderColor="64, 64, 64, 64" BorderDashStyle="Solid" BackSecondaryColor="White" BackColor="Gainsboro" ShadowColor="Transparent" BackGradientStyle="TopBottom">
<area3dstyle Rotation="10" perspective="10" Inclination="15" IsRightAngleAxes="False" wallwidth="0" IsClustered="False"></area3dstyle>
<axisy minimum = "110" Interval = "10" linecolor="64, 64, 64, 64" IsLabelAutoFit="False"><labelstyle font="Trebuchet MS, 8.25pt, style=Bold" /><majorgrid linecolor="64, 64, 64, 64" /></axisy>
<axisy2 minimum = "0" Maximum = "20" Interval = "20" linecolor="64, 64, 64, 64" IsLabelAutoFit="False"><labelstyle font="Trebuchet MS, 8.25pt, style=Bold" /><majorgrid linecolor="64, 64, 64, 64" /></axisy2>
<axisx Minimum = "0" interval = "1" linecolor="64, 64, 64, 64" IsLabelAutoFit="False"><labelstyle font="Trebuchet MS, 8.25pt, style=Bold" /><majorgrid linecolor="64, 64, 64, 64" /></axisx>
</asp:chartarea>
<asp:chartarea Name="chartarea2" BorderColor="64, 64, 64, 64" BorderDashStyle="Solid" BackSecondaryColor="White" BackColor="Gainsboro" ShadowColor="Transparent" BackGradientStyle="TopBottom">
<area3dstyle Rotation="10" perspective="10" Inclination="15" IsRightAngleAxes="False" wallwidth="0" IsClustered="False"></area3dstyle>
<axisy minimum = "0" Interval = "2.5" linecolor="64, 64, 64, 64" IsLabelAutoFit="False"><labelstyle font="Trebuchet MS, 8.25pt, style=Bold" /><majorgrid linecolor="64, 64, 64, 64" /></axisy>
<axisx Minimum = "0" interval = "1" linecolor="64, 64, 64, 64" IsLabelAutoFit="False"><labelstyle font="Trebuchet MS, 8.25pt, style=Bold" /><majorgrid linecolor="64, 64, 64, 64" /></axisx>
</asp:chartarea>
</chartareas>
</asp:Chart> 

<br />
Add SBr <asp:TextBox ID="SBr" runat="server" width = "38px" style ="font-size:11pt"/>
 on <asp:TextBox ID="SBrDate" runat="server" width = "150px" style ="font-size:11pt"/> 
<asp:Button ID="SBrSubmit" runat="server" Text="Submit"  style ="font-size:12pt" visible = "true"/> 
&nbsp;&nbsp;&nbsp;<a href="#top">Top</a>
<br />

<a name="JaundiceChartholder" runat="server"></a>
<asp:Chart id="JaundiceChart" DataSourceID="JaundiceChartDataSource" runat="server" Height="296px" Width="550px" BorderColor="#1A3B69" BackColor="WhiteSmoke" BorderWidth="2px" BackGradientStyle="TopBottom" BackSecondaryColor="White" BorderDashStyle="Solid" >
<Series>
<asp:Series Color = "red" Name="Sbr" XValueMember="Age" YValueMembers="SBr" ChartType="Line" BorderWidth="3"/>
<asp:Series Color = "red" Name="Sbr2" XValueMember="Age" YValueMembers="SBr" ChartType="Point" BorderWidth="10"/>
<asp:Series Color = "blue" Name="exchline" XValueMember="Age" YValueMembers="exchline" ChartType="Line"/>
<asp:Series Color = "aqua" Name="Photo" XValueMember="Age" YValueMembers="photoline" ChartType="Line"/>
<asp:Series Color = "yellow" Name="AgeLine" XValueMember="Age" YValueMembers="ageline" ChartType="Line" BorderWidth="5"/>
<asp:Series Color = "green" Name="Photo1" XValueMember="Age" YValueMembers="Photo1" ChartType="Line" BorderWidth="5"/>
<asp:Series Color = "green" Name="Photo2" XValueMember="Age" YValueMembers="Photo2" ChartType="Line" BorderWidth="5"/>
<asp:Series Color = "green" Name="Photo3" XValueMember="Age" YValueMembers="Photo3" ChartType="Line" BorderWidth="5"/>
<asp:Series Color = "brown" Name="immunoglob" XValueMember="Age" YValueMembers="immunoglob" ChartType="Point" MarkerSize="8" BorderWidth="5"/>
<asp:Series Color = "red" Name="exchange" XValueMember="Age" YValueMembers="exchange" ChartType="Point" MarkerSize="10" BorderWidth="10"/>

</Series>
<titles>
<asp:title ShadowColor="32, 0, 0, 0" Font="Trebuchet MS, 12pt, style=Bold" ShadowOffset="3"  ForeColor="26, 59, 105" Text="Jaundice Chart" ></asp:title>
<asp:title ShadowColor="32, 0, 0, 0" Font="Trebuchet MS, 8pt, style=Bold" ShadowOffset="3"  ForeColor="26, 59, 105" Text="" ></asp:title>
</titles>
<%--<legends><asp:legend IsTextAutoFit="False" Name="Default" BackColor="Transparent" Font="Trebuchet MS, 8.25pt, style=Bold"></asp:legend></legends>--%>
<borderskin skinstyle="Emboss"></borderskin>
<chartareas><asp:chartarea Name="ChartArea1" BorderColor="64, 64, 64, 64" BorderDashStyle="Solid" BackSecondaryColor="White" BackColor="Gainsboro" ShadowColor="Transparent" BackGradientStyle="TopBottom">
<area3dstyle Rotation="10" perspective="10" Inclination="15" IsRightAngleAxes="False" wallwidth="0" IsClustered="False"></area3dstyle>
<axisy minimum = "50" linecolor="64, 64, 64, 64" IsLabelAutoFit="False"><labelstyle font="Trebuchet MS, 8.25pt, style=Bold" /><majorgrid linecolor="64, 64, 64, 64" /></axisy>
<axisx Minimum = "0" interval = "1" linecolor="64, 64, 64, 64" IsLabelAutoFit="False"><labelstyle font="Trebuchet MS, 8.25pt, style=Bold" /><majorgrid linecolor="64, 64, 64, 64" /></axisx>
</asp:chartarea></chartareas></asp:Chart> 
<br />

</ContentTemplate>
</asp:UpdatePanel>



<div style="background-color:#CCCCCC; text-align: left; font-size:10pt" >
<asp:Button ID="PatientList2" runat="server" Text="Patient List"  width = "130px" style ="font-size:12pt"  />&nbsp&nbsp;&nbsp
    <asp:Button ID="PrevBed2" runat="server" Text="Prev" width = "70px" style ="font-size:12pt"  />&nbsp&nbsp;&nbsp
    <asp:DropDownList ID="BedList2" width = "200px" style ="font-size:12pt" runat="server" DataSourceID="BedListDataSource" DataTextField="Bed" DataValueField="BedName" AutoPostBack="True">
    </asp:DropDownList>&nbsp&nbsp&nbsp
    <asp:Button ID="NextBed2" runat="server" Text="Next"  width = "70px" style ="font-size:12pt"  />
</div>

<asp:SqlDataSource ID="BedListDataSource" runat="server" ConnectionString="<%$ ConnectionStrings:NeonatalConnectionString1 %>"
    SelectCommand="select Bed, BedName from (select '' as Bed, '' as Bedname, 0 as Bedorder union all select bed, bedname, Bedorder from (SELECT top 100 percent CASE WHEN LEFT(BedName, 3) = 'bed' THEN right(BedName, len(bedname)-3) ELSE BedName END + ' - ' + isnull(PMI.BName,'') + case when PMI.BOrder > 0 then ' ' + convert(nvarchar(2),PMI.BOrder) else ' ' end AS Bed, _Beds.BedName, _beds.BedOrder FROM _Beds INNER JOIN Episode ON _Beds.BedName = Episode.BedNo# AND _Beds.HospCode = Episode.HospitalID INNER JOIN PMI ON Episode.NeonatalID = PMI.NeonatalID WHERE (_Beds.HospCode = @hospid) AND (Episode.DischDate IS NULL) GROUP BY _Beds.BedName, _Beds.BedOrder, PMI.BName, PMI.BOrder) as T2) as T3 ORDER BY BedOrder">
<%--    SelectCommand="SELECT CASE WHEN LEFT(BedName, 3) = 'bed' THEN BedName ELSE 'Bed ' + BedName END AS Bed, BedName FROM _Beds INNER JOIN Episode ON BedName = BedNo# AND HospCode = HospitalID WHERE HospCode = @hospid AND DischDate IS NULL GROUP BY BedName, BedOrder ORDER BY BedOrder">--%>
<%--SelectCommand="SELECT case when left(BedName,3) = 'bed' then BedName else 'Bed ' + BedName end as Bed, BedName FROM _Beds WHERE (HospCode = @hospid) ORDER BY BedOrder">--%>
    <SelectParameters>
        <asp:QueryStringParameter Name="hospid" QueryStringField="@hospid" />
    </SelectParameters>
</asp:SqlDataSource>

<asp:SqlDataSource ID="BedList3DataSource" runat="server" ConnectionString="<%$ ConnectionStrings:NeonatalConnectionString1 %>"
SelectCommand="select 'Move Baby to new bed' as Bed, '' as BedName, 0 as BedORder union all SELECT case when left(BedName,3) = 'bed' then BedName else 'Bed ' + BedName end as Bed, BedName, Bedorder FROM _Beds WHERE (HospCode = @hospid) ORDER BY BedOrder">
    <SelectParameters>
        <asp:QueryStringParameter Name="hospid" QueryStringField="@hospid" />
    </SelectParameters>
</asp:SqlDataSource>

<asp:SqlDataSource ID="GrowthChartDataSource" runat="server" 
        ConnectionString="<%$ ConnectionStrings:NeonatalConnectionString %>" 
    SelectCommand="exec procgrowthmobile @neoid">
    <SelectParameters>
        <asp:QueryStringParameter Name="neoid" QueryStringField="@neoid" />
    </SelectParameters>
</asp:SqlDataSource>

    <asp:SqlDataSource ID="JaundiceChartDataSource" runat="server" 
        ConnectionString="<%$ ConnectionStrings:NeonatalConnectionString %>" 
    SelectCommand="exec procjaundicemobile @neoid, @hospid">
    <SelectParameters>
        <asp:QueryStringParameter Name="neoid" QueryStringField="@neoid" />
        <asp:QueryStringParameter Name="hospid" QueryStringField="@hospid" />
    </SelectParameters>
</asp:SqlDataSource>
   
    <asp:SqlDataSource ID="RespChartDataSource" runat="server" 
        ConnectionString="<%$ ConnectionStrings:NeonatalConnectionString %>" 
    SelectCommand="exec procrespgraphmobile @neoid">
    <SelectParameters>
        <asp:QueryStringParameter Name="neoid" QueryStringField="@neoid" />
    </SelectParameters>
</asp:SqlDataSource> 
    
    <asp:SqlDataSource ID="ResultsChart1DataSource" runat="server" 
        ConnectionString="<%$ ConnectionStrings:NeonatalConnectionString %>" 
    SelectCommand="exec procresultsgraph1 @neoid">
    <SelectParameters>
        <asp:QueryStringParameter Name="neoid" QueryStringField="@neoid" />
    </SelectParameters>
</asp:SqlDataSource> 


    <asp:SqlDataSource ID="ResultsChart2DataSource" runat="server" 
        ConnectionString="<%$ ConnectionStrings:NeonatalConnectionString %>" 
    SelectCommand="exec procresultsgraph2 @neoid">
    <SelectParameters>
        <asp:QueryStringParameter Name="neoid" QueryStringField="@neoid" />
    </SelectParameters>
</asp:SqlDataSource>     

    <asp:SqlDataSource ID="MilkDataSource" runat="server" ConnectionString="<%$ ConnectionStrings:NeonatalConnectionString %>"
        SelectCommand="select Null as Number, 'Milk?' as Label union all SELECT [Number], [Label] FROM [_Milk]"></asp:SqlDataSource>

    <asp:SqlDataSource ID="MilkRouteDataSource" runat="server" ConnectionString="<%$ ConnectionStrings:NeonatalConnectionString %>"
        SelectCommand="select Null as Number, 'Route?' as Label union all SELECT [Number], [Label] FROM [_MilkRoute]"></asp:SqlDataSource>
        
     <asp:SqlDataSource ID="TreatmentDataSource" runat="server" ConnectionString="<%$ ConnectionStrings:NeonatalConnectionString %>"
        SelectCommand="select Null as Number, '' as Generic union all SELECT Number, Generic FROM _Treatment WHERE EndDate IS NULL and mobilelist = -1 ORDER BY Generic "></asp:SqlDataSource>
 
      <asp:SqlDataSource ID="THospDataSource" runat="server" ConnectionString="<%$ ConnectionStrings:NeonatalConnectionString %>"
        SelectCommand="select Null as HospitalID, 'Destination?' as Hospname, 7 as Carelevel union all SELECT HospitalID, HospName, Carelevel FROM _Hospitals where not hospitalid in ('NETS', '003','W400') ORDER BY Carelevel desc, Hospname "></asp:SqlDataSource>

     <asp:SqlDataSource ID="TreatmentDataSource2" runat="server" 
        ConnectionString="<%$ ConnectionStrings:NeonatalConnectionString %>" 
    SelectCommand="select null as Number, 'Cease Rx' as Generic, 0 as Sortorder union all SELECT Number, Generic, sortorder FROM Treatment INNER JOIN _Treatment ON Treatment.Treatment = _Treatment.Number WHERE (Treatment.NeonatalID = @neoid) AND (Treatment.Finish IS NULL) order by sortorder">
    <SelectParameters>
        <asp:QueryStringParameter Name="neoid" QueryStringField="@neoid" />
    </SelectParameters>
    </asp:SqlDataSource>  

     <asp:SqlDataSource ID="ProblemDataSource" runat="server" 
        ConnectionString="<%$ ConnectionStrings:NeonatalConnectionString %>" 
    SelectCommand="select 'Inactivate' as problem# union all SELECT Problem# FROM Problems# WHERE NeonatalID = @neoid AND Finish IS NULL and len(problem#) > 2">
    <SelectParameters>
        <asp:QueryStringParameter Name="neoid" QueryStringField="@neoid" />
    </SelectParameters>
    </asp:SqlDataSource>  

    <asp:SqlDataSource ID="CatheterDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:NeonatalConnectionString %>"
    SelectCommand="select 0 as Number, 'New Cath' as Label union all SELECT _Catheter.Number, _Catheter.Shortname as Label FROM (SELECT Catheter FROM Catheters WHERE (NeonatalID = @neoid) AND (DateOut IS NULL)) AS T1 RIGHT OUTER JOIN _Catheter ON T1.Catheter = _Catheter.Number WHERE (T1.Catheter IS NULL and not _Catheter.Number = 6)">
    <SelectParameters>
        <asp:QueryStringParameter Name="neoid" QueryStringField="@neoid" />
    </SelectParameters>
    </asp:SqlDataSource>

     <asp:SqlDataSource ID="CatheterDataSource2" runat="server" 
        ConnectionString="<%$ ConnectionStrings:NeonatalConnectionString %>" 
    SelectCommand="select 0 as Number, 'Stop Catheter' as Label union all SELECT Catheters.Catheter as Number, _Catheter.Shortname + ' ' + convert(nchar(6),Catheters.Datein, 113) + ' (' + convert(nvarchar(3),datediff(d, catheters.datein, getdate())) + ')' as Label FROM Catheters INNER JOIN _Catheter ON Catheters.Catheter = _Catheter.Number WHERE (Catheters.NeonatalID = @neoid) AND (Catheters.DateOut IS NULL)">
    <SelectParameters>
        <asp:QueryStringParameter Name="neoid" QueryStringField="@neoid" />
    </SelectParameters>
    </asp:SqlDataSource>  
</asp:Content>

