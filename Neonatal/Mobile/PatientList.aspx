<%@ Page Language="VB" MasterPageFile="~/Mobile/MobileMasterPage.master" AutoEventWireup="false" CodeFile="PatientList.aspx.vb" Inherits="Mobile_PatientList" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<asp:Panel ID="panel1" runat="server"><asp:PlaceHolder ID="Top" runat="server"></asp:PlaceHolder>
    <asp:DropDownList ID="HospList" runat="server" AutoPostBack="True" DataSourceID="SqlDataSource1"
        DataTextField="hospname" DataValueField="hospitalid" style ="font-size:12pt" Width = "240px">
    </asp:DropDownList>
    <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:NeonatalConnectionString1 %>"
        SelectCommand="select hospitalid, hospname from _hospitals where clinicaluse = -1 order by carelevel desc, hospname">
    </asp:SqlDataSource>
<asp:Label ID="Label1" runat="server" Text="" style ="font-size:14pt"></asp:Label>
&nbsp;&nbsp;&nbsp;<a href="#Options"><img src="../Images/options.JPG" alt = "Bottom" style="width: 24px; height: 23px; border-top-style: none; border-right-style: none; border-left-style: none; border-bottom-style: none;" align ="bottom"/></a>
<asp:GridView ID="GridView1" datakeynames = "NeonatalID" 
        runat="server" BackColor="White" BorderColor="#999999" 
            BorderStyle="None" BorderWidth="1px" CellPadding="3" 
        GridLines="Vertical" AllowSorting="True" AutoGenerateColumns="False">
        <RowStyle BackColor="#EEEEEE" ForeColor="Black" />
         <Columns>
         <asp:ButtonField Text="Go" ButtonType="Button" commandname= "View"><ItemStyle font-size="14pt" foreCOLOR = "aqua" Height="50" Width="30px" /></asp:ButtonField>
             <asp:BoundField DataField="Bed" HeaderText="Bed"><ItemStyle Width="60px" /></asp:BoundField>
             <asp:BoundField DataField="MRN" HeaderText="MRN"><ItemStyle Width="60px" /></asp:BoundField>
             <asp:BoundField DataField="RespSupp" HeaderText="Resp"><ItemStyle Width="10px" /></asp:BoundField>
             <asp:BoundField DataField="Baby" HeaderText="Baby"><ItemStyle Width="150px" /></asp:BoundField>
             <asp:BoundField DataField="GA" HeaderText="GA"><ItemStyle Width="10px" /></asp:BoundField>
             <asp:BoundField DataField="BW" HeaderText="BW"><ItemStyle Width="10px" /></asp:BoundField>
             <asp:BoundField DataField="DOB" HeaderText="DOB"><ItemStyle Width="50px" /></asp:BoundField>
         </Columns>
        <FooterStyle BackColor="#CCCCCC" ForeColor="Black" />
        <PagerStyle BackColor="#999999" ForeColor="Black" HorizontalAlign="Center" />
        <SelectedRowStyle BackColor="#008A8C" Font-Bold="True" ForeColor="White" />
        <HeaderStyle BackColor="#000084" Font-Bold="True" ForeColor="White" />
        <AlternatingRowStyle BackColor="#DCDCDC" />
    </asp:GridView>
    <asp:Button ID="NewBaby" runat="server" Text="Create New Baby" Width="200px" visible = "False"/><br />
    <asp:UpdatePanel ID="UpdatePanel" runat="server">
<ContentTemplate>
<asp:UpdateProgress ID="UpdateProgress1" runat="server">
<ProgressTemplate>
Updating &nbsp;....
</ProgressTemplate>
</asp:UpdateProgress>
    <strong>Inpatient Results in Database during last 
        <asp:DropDownList ID="freq" runat="server" Font-Names="Times New Roman" style ="font-size:10pt" Visible = "true" Width="80px" AutoPostBack="true">
                <asp:ListItem Value="4">&nbsp;&nbsp;4 hrs</asp:ListItem>
                <asp:ListItem Value="12">12 hrs</asp:ListItem>
                <asp:ListItem Value="24" Selected="True">24 hrs</asp:ListItem>
                <asp:ListItem Value="48">48 hrs</asp:ListItem>
                <asp:ListItem Value="72">72 hrs</asp:ListItem>
                <asp:ListItem Value="166">&nbsp;&nbsp;1 wk</asp:ListItem>
    </asp:DropDownList>
    </strong><br /> 
          <asp:GridView ID="LatestResults" datakeynames = "NeonatalID" 
        runat="server" BackColor="White" BorderColor="#999999" 
            BorderStyle="None" BorderWidth="1px" CellPadding="3" 
        GridLines="Vertical" AllowSorting="True" AutoGenerateColumns="False" OnRowDataBound="HighlightToday_RowDataBOund" >
        <RowStyle BackColor="#EEEEEE" ForeColor="Black" />
         <Columns>
         <asp:ButtonField Text="Go" ButtonType="Button" commandname= "View"><ItemStyle font-size="14pt" foreCOLOR = "aqua" Height="50" Width="30px" /></asp:ButtonField>
         <asp:BoundField DataField="Bed" HeaderText="Bed"><ItemStyle Width="35px" /></asp:BoundField>
         <asp:BoundField DataField="Baby" HeaderText="Baby (Day)"><ItemStyle Width="170px" /></asp:BoundField>
         <asp:BoundField DataField="GA" HeaderText="GA"><ItemStyle Width="0px" /></asp:BoundField>
         <asp:BoundField DataField="Day" HeaderText="Time"><ItemStyle Width="35px" /></asp:BoundField>
         <asp:BoundField DataField="Hb" HeaderText="Hb"><ItemStyle Width="35px" /></asp:BoundField>
         <asp:BoundField DataField="Plat" HeaderText="Plt"><ItemStyle Width="35px" /></asp:BoundField>
<%--         <asp:BoundField DataField="Spacer" HeaderText=""><ItemStyle Width="0px" /></asp:BoundField>--%>
         <asp:BoundField DataField="Sodium" HeaderText="Na"><ItemStyle Width="35px" /></asp:BoundField>
         <asp:BoundField DataField="Creat" HeaderText="Cr "><ItemStyle Width="35px" /></asp:BoundField>
         <asp:BoundField DataField="SBr" HeaderText="SBr"><ItemStyle Width="35px" /></asp:BoundField>
<%--         <asp:BoundField DataField="Hct" HeaderText="Hct"><ItemStyle Width="40px" /></asp:BoundField>
         <asp:BoundField DataField="Retic" HeaderText="Retic"><ItemStyle Width="40px" /></asp:BoundField>
         <asp:BoundField DataField="WCC" HeaderText="WCC"><ItemStyle Width="40px" /></asp:BoundField>
         <asp:BoundField DataField="Neutro" HeaderText="Neut"><ItemStyle Width="40px" /></asp:BoundField>
         <asp:BoundField DataField="Potass" HeaderText="K+"><ItemStyle Width="40px" /></asp:BoundField>
         <asp:BoundField DataField="Calcium" HeaderText="Ca++"><ItemStyle Width="40px" /></asp:BoundField>--%>
         <asp:BoundField DataField="Comment" HeaderText="Comment"><ItemStyle Width="150px" /></asp:BoundField>
         </Columns>
        <FooterStyle BackColor="#CCCCCC" ForeColor="Black" />
        <PagerStyle BackColor="#999999" ForeColor="Black" HorizontalAlign="Center" />
        <SelectedRowStyle BackColor="#008A8C" Font-Bold="True" ForeColor="White" />
        <HeaderStyle BackColor="#000084" Font-Bold="True" ForeColor="White" />
        <AlternatingRowStyle BackColor="#DCDCDC" />
    </asp:GridView>
</ContentTemplate>
</asp:UpdatePanel>

        <br />
    <strong>Patient Outliers</strong> (for Babies and Fetuses not in database)<br /> 
          <asp:GridView ID="Outlier" datakeynames = "PatientID" 
        runat="server" BackColor="White" BorderColor="#999999" 
            BorderStyle="None" BorderWidth="1px" CellPadding="3" 
        GridLines="Vertical" AllowSorting="True" AutoGenerateColumns="False">
        <RowStyle BackColor="#EEEEEE" ForeColor="Black" />
         <Columns>
         <asp:BoundField DataField="Location" HeaderText="Location"><ItemStyle Width="95px" /></asp:BoundField>
         <asp:BoundField DataField="Name" HeaderText="Name"><ItemStyle Width="105px" /></asp:BoundField>
         <asp:BoundField DataField="Age" HeaderText="Age"><ItemStyle Width="50px" /></asp:BoundField>
         <asp:BoundField DataField="Diagnosis" HeaderText="Diagnosis"><ItemStyle Width="200px" /></asp:BoundField>
         <asp:ButtonField Text="Del" ButtonType="Button" commandname= "Remove"><ItemStyle font-size="14pt" foreCOLOR = "aqua" Height="50" Width="30px" /></asp:ButtonField>
         </Columns>
        <FooterStyle BackColor="#CCCCCC" ForeColor="Black" />
        <PagerStyle BackColor="#999999" ForeColor="Black" HorizontalAlign="Center" />
        <SelectedRowStyle BackColor="#008A8C" Font-Bold="True" ForeColor="White" />
        <HeaderStyle BackColor="#000084" Font-Bold="True" ForeColor="White" />
        <AlternatingRowStyle BackColor="#DCDCDC" />
    </asp:GridView>
    <br />

    <asp:GridView ID="GridView2" datakeynames = "commentID" 
        runat="server" BackColor="White" BorderColor="#999999" 
            BorderStyle="None" BorderWidth="1px" CellPadding="3" 
        GridLines="Vertical" AllowSorting="True" AutoGenerateColumns="False" DataSourceID = "PersonalDataSource">
        <RowStyle BackColor="#EEEEEE" ForeColor="Black" />
         <Columns>
         <asp:ButtonField Text="Go" ButtonType="Button" commandname= "Go"><ItemStyle font-size="14pt" foreCOLOR = "aqua" Height="50" Width="30px" /></asp:ButtonField>
         <asp:BoundField DataField="Location" HeaderText="Bed"><ItemStyle Width="40px" /></asp:BoundField>
         <asp:BoundField DataField="Baby" HeaderText="Baby"><ItemStyle Width="120px" /></asp:BoundField>
         <asp:BoundField DataField="Comment" HeaderText="Personal Comment"><ItemStyle Width="240px" /></asp:BoundField>
         <asp:ButtonField Text="Del" ButtonType="Button" commandname= "Remove"><ItemStyle font-size="14pt" foreCOLOR = "aqua" Height="50" Width="30px" /></asp:ButtonField>
         </Columns>
        <FooterStyle BackColor="#CCCCCC" ForeColor="Black" />
        <PagerStyle BackColor="#999999" ForeColor="Black" HorizontalAlign="Center" />
        <SelectedRowStyle BackColor="#008A8C" Font-Bold="True" ForeColor="White" />
        <HeaderStyle BackColor="#000084" Font-Bold="True" ForeColor="White" />
        <AlternatingRowStyle BackColor="#DCDCDC" />
    </asp:GridView>
    <asp:SqlDataSource ID="PersonalDataSource" runat="server" 
        ConnectionString="<%$ ConnectionStrings:NeonatalConnectionString %>" 
    SelectCommand="SELECT CASE WHEN NeoStaff.HospitalID <> _Hospitals.HospitalID THEN AbbrevName WHEN _Hospitals.HospitalID IS NULL THEN 'Disch' ELSE 'Bed ' + Episode.BedNo# END AS Location, BName + ' ' + ISNULL(CONVERT(nvarchar(5),CASE BOrder WHEN 0 THEN Null ELSE BOrder END), '') + ', ' + ISNULL(BFName, '') + case gender when 1 then ' (M)' when 2 then ' (F)' else ' (?)' end as Baby, MobileComment.Comment, CASE WHEN NeoStaff.HospitalID <> _Hospitals.HospitalID THEN 3 WHEN _Hospitals.HospitalID IS NULL THEN 2 ELSE 1 END AS BabyOrder, convert(nvarchar(50), MobileComment.CommentID) as CommentID FROM MobileComment INNER JOIN PMI ON PMI.NeonatalID = MobileComment.NeonatalID INNER JOIN NeoStaff ON MobileComment.StaffID = NeoStaff.StaffID and NeoStaff.usercode = @sessionusername left outer JOIN Episode ON Episode.NeonatalID = PMI.NeonatalID AND Episode.DischDate IS NULL left outer JOIN _Hospitals ON Episode.HospitalID = _Hospitals.HospitalID left outer JOIN _Beds on Episode.BedNo# = _Beds.BedName and _Beds.HospCode = episode.hospitalid ORDER BY babyorder , bedorder">
    <SelectParameters>
        <asp:SessionParameter Name ="sessionusername" SessionField="sessionusername" />
    </SelectParameters>
    </asp:SqlDataSource>     
  <br />  
    <asp:GridView ID="GridView3" datakeynames = "commentID" 
        runat="server" BackColor="White" BorderColor="#999999" 
            BorderStyle="None" BorderWidth="1px" CellPadding="3" 
        GridLines="Vertical" AllowSorting="True" AutoGenerateColumns="False" DataSourceID = "PersonalDataSource2">
        <RowStyle BackColor="#EEEEEE" ForeColor="Black" />
         <Columns>
         <asp:BoundField DataField="CreateDate" HeaderText="Created on "><ItemStyle Width="95px" /></asp:BoundField>
         <asp:BoundField DataField="Comment" HeaderText="Personal Comment"><ItemStyle Width="370px" /></asp:BoundField>
         <asp:ButtonField Text="Del" ButtonType="Button" commandname= "Remove"><ItemStyle font-size="14pt" foreCOLOR = "aqua" Height="50" Width="30px" /></asp:ButtonField>
         </Columns>
        <FooterStyle BackColor="#CCCCCC" ForeColor="Black" />
        <PagerStyle BackColor="#999999" ForeColor="Black" HorizontalAlign="Center" />
        <SelectedRowStyle BackColor="#008A8C" Font-Bold="True" ForeColor="White" />
        <HeaderStyle BackColor="#000084" Font-Bold="True" ForeColor="White" />
        <AlternatingRowStyle BackColor="#DCDCDC" />
    </asp:GridView>
    <asp:SqlDataSource ID="PersonalDataSource2" runat="server" 
        ConnectionString="<%$ ConnectionStrings:NeonatalConnectionString %>" 
    SelectCommand="SELECT convert(nvarchar(11),created,113) as CreateDate, Comment, convert(nvarchar(50), MobileComment.CommentID) as CommentID FROM MobileComment INNER JOIN NeoStaff ON MobileComment.StaffID = NeoStaff.StaffID and NeoStaff.usercode = @sessionusername where neonatalid is null ORDER BY created desc">
    <SelectParameters>
        <asp:SessionParameter Name ="sessionusername" SessionField="sessionusername" />
    </SelectParameters>
    </asp:SqlDataSource>  
    
    
    
    
    Add Comment 
    <asp:TextBox ID="NewComment" runat="server" Width = "355"></asp:TextBox> 
    <asp:Button ID="AddComment" runat="server" Text="Add" /> 
    <br /> 

    
<%--    Add / Edit Outlier Details (NOT Functional Yet)
    <asp:TextBox ID="TextBox8" runat="server" Width="34px"></asp:TextBox><br />
    <br />
    Location* <asp:TextBox ID="Location" runat="server"></asp:TextBox>
    Diagnosis *<asp:TextBox ID="Diagnosis" runat="server" Width="328px"></asp:TextBox><br />
    Consultant<asp:TextBox ID="Consultant" runat="server"></asp:TextBox><br />
    DOB/EDC <asp:TextBox ID="TextBox3" runat="server"></asp:TextBox><br />
    or GA+d <asp:TextBox ID="TextBox4" runat="server"></asp:TextBox><br />
    Progress <asp:TextBox ID="TextBox6" runat="server" Height="80px" Width="332px"></asp:TextBox><br />
    MRN <asp:TextBox ID="TextBox7" runat="server"></asp:TextBox><br />
    Consult <asp:RadioButtonList ID="RadioButtonList1" runat="server">
        <asp:ListItem Value="0">Not Needed</asp:ListItem>
        <asp:ListItem Value="1">Required</asp:ListItem>
        <asp:ListItem Value="2">Performed</asp:ListItem>
    </asp:RadioButtonList>
    Print <asp:CheckBox ID="Print" runat="server" /><br />
    Remove <asp:CheckBox ID="Remove" runat="server" /><br />
    <asp:Button ID="OutlierSave" runat="server" Text="Save" /> <asp:Button ID="OutlierDelete" runat="server" Text="Delete" /><br />
    <br />--%>
    
    
    <br />
    <strong><span style="text-decoration: underline">User defaults</span></strong><br />
    <a id="A1" name = "Options" runat="server"></a>
    <asp:CheckBox ID="NursingNotes" runat="server" autopostback="false"/>  - View Nursing Notes by default<br />
    <asp:CheckBox ID="Comment" runat="server" autopostback="false"/>  - Enable Personal Comments (TextBox after Perinatal Hx - review later from PC)<br />
    <br />
    <asp:Button ID="RPA" runat="server" Text="RPA Interface" visible = "false"/>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
    <a href="#top">Return to Top</a>
        </asp:Panel>
</asp:Content>

