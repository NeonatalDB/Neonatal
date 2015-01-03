<%@ Page Language="VB" AutoEventWireup="false" CodeFile="NewPatient.aspx.vb" Inherits="Mobile_NewPatient" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Untitled Page</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
<h1>Liverpool Hospital</H1><br />
<H2>Add New Baby to Database</H2>
        <br />
        <asp:Table ID="Table1" runat="server" Height="285px" Width="477px">
            <asp:TableRow ID="TableRow1" runat="server">
                <asp:TableCell ID="TableCell1" runat="server">Bed</asp:TableCell>
                <asp:TableCell ID="TableCell2" runat="server">
                <asp:DropDownList ID="Bed" runat="server" DataSourceID="SqlDataSource1" DataTextField="BedName" DataValueField="BedName" width = "155"></asp:DropDownList>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="bed" ErrorMessage="RequiredFieldValidator">* Required</asp:RequiredFieldValidator>
                </asp:TableCell>
            </asp:TableRow>
            <asp:TableRow ID="TableRow2" runat="server">
                <asp:TableCell ID="TableCell3" runat="server">Consultant</asp:TableCell>
                <asp:TableCell ID="TableCell4" runat="server">
                <asp:DropDownList ID="Consultant" runat="server" DataSourceID="SqlDataSource2" DataTextField="c_lname" DataValueField="c_lname" width = "155"></asp:DropDownList>
                </asp:TableCell>
            </asp:TableRow>
            <asp:TableRow ID="TableRow10" runat="server">
                <asp:TableCell ID="TableCell19" runat="server">MRN</asp:TableCell>
                <asp:TableCell ID="TableCell20" runat="server">
                <asp:TextBox ID="MRN" runat="server"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server" ControlToValidate="MRN" ErrorMessage="RequiredFieldValidator">* Required</asp:RequiredFieldValidator>
                </asp:TableCell>
            </asp:TableRow>
            <asp:TableRow ID="TableRow3" runat="server">
                <asp:TableCell ID="TableCell5" runat="server">Surname</asp:TableCell>
                <asp:TableCell ID="TableCell6" runat="server">
                <asp:TextBox ID="Surname" runat="server"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="surname" ErrorMessage="RequiredFieldValidator">* Required</asp:RequiredFieldValidator>
                </asp:TableCell>
            </asp:TableRow>
            <asp:TableRow ID="TableRow4" runat="server">
                <asp:TableCell ID="TableCell7" runat="server">DOB</asp:TableCell>
                <asp:TableCell ID="TableCell8" runat="server">
                <asp:TextBox ID="DOB" runat="server"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="dob" ErrorMessage="RequiredFieldValidator">* Required</asp:RequiredFieldValidator>
                </asp:TableCell>
            </asp:TableRow>
            <asp:TableRow ID="TableRow5" runat="server">
                <asp:TableCell ID="TableCell9" runat="server">Admitted</asp:TableCell>
                <asp:TableCell ID="TableCell10" runat="server">
                <asp:TextBox ID="Admitdate" runat="server"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="admitdate" ErrorMessage="RequiredFieldValidator">* Required</asp:RequiredFieldValidator>
                </asp:TableCell>
            </asp:TableRow>
            <asp:TableRow ID="TableRow6" runat="server">
                <asp:TableCell ID="TableCell11" runat="server">GA</asp:TableCell>
                <asp:TableCell ID="TableCell12" runat="server">
                    <asp:DropDownList ID="GA" runat="server">
                            <asp:ListItem Value="" />
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
                            <asp:ListItem Value="41" />
                            <asp:ListItem Value="42" />
                            <asp:ListItem Value="43" />
                            <asp:ListItem Value="44" />
                    </asp:DropDownList> wks + 
                    <asp:DropDownList ID="GADays" runat="server" Width="36px">
                            <asp:ListItem Value="" />
                            <asp:ListItem Value="0" />
                            <asp:ListItem Value="1" />
                            <asp:ListItem Value="2" />
                            <asp:ListItem Value="3" />
                            <asp:ListItem Value="4" />
                            <asp:ListItem Value="5" />
                            <asp:ListItem Value="6" />
                    </asp:DropDownList> days
                <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="ga" ErrorMessage="RequiredFieldValidator">* Required</asp:RequiredFieldValidator>
                </asp:TableCell>
            </asp:TableRow>
            <asp:TableRow ID="TableRow7" runat="server">
                <asp:TableCell ID="TableCell13" runat="server">BW</asp:TableCell>
                <asp:TableCell ID="TableCell14" runat="server">
                <asp:TextBox ID="BW" runat="server"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="bw" ErrorMessage="RequiredFieldValidator">* Required</asp:RequiredFieldValidator>
                <asp:RangeValidator ID="RangeValidator1" runat="server" ControlToValidate="bw" ErrorMessage="RangeValidator" MaximumValue="9000" MinimumValue="250" Type="Integer">* BW in gram</asp:RangeValidator>
                <%--<asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ErrorMessage="RegularExpressionValidator" ControlToValidate="bw" ValidationExpression="9{3,4}">Numbers only</asp:RegularExpressionValidator>--%>
                </asp:TableCell>
            </asp:TableRow>
            <asp:TableRow ID="TableRow8" runat="server">
                <asp:TableCell ID="TableCell15" runat="server">Gender</asp:TableCell>
                <asp:TableCell ID="TableCell16" runat="server">
                <asp:DropDownList ID="gender" runat="server" DataSourceID="SqlDataSource4" DataTextField="Label" DataValueField="Number" width = "155"></asp:DropDownList>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ControlToValidate="gender" ErrorMessage="RequiredFieldValidator">* Required</asp:RequiredFieldValidator>
                </asp:TableCell>
            </asp:TableRow>
            <asp:TableRow ID="TableRow9" runat="server">
                <asp:TableCell ID="TableCell17" runat="server">Plurality</asp:TableCell>
                <asp:TableCell ID="TableCell18" runat="server">
                <asp:DropDownList ID="mgest" runat="server" DataSourceID="SqlDataSource3" DataTextField="Label" DataValueField="Number" width = "155"></asp:DropDownList>
                </asp:TableCell>
            </asp:TableRow>
            <asp:TableRow ID="TableRow20" runat="server">
                <asp:TableCell ID="TableCell22" runat="server"></asp:TableCell>
                <asp:TableCell ID="TableCell23" runat="server">
		<br />
		<asp:Button ID="submit" runat="server" Text="Create Baby" />
                </asp:TableCell>
            </asp:TableRow>
        </asp:Table>



        <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:NeonatalConnectionString %>"
            SelectCommand="select '' as bedname, 0 as bedorder union all select bedname, bedorder from _beds where hospcode = 'd209' order by bedorder"></asp:SqlDataSource>
        <asp:SqlDataSource ID="SqlDataSource2" runat="server" ConnectionString="<%$ ConnectionStrings:NeonatalConnectionString %>"
            SelectCommand="select '' as c_lname union all select isnull(c_fname,'') + ' ' + c_lname as c_lname from _consultants where hospitalid = 'd209' and not (c_type = 2 or c_type =3) order by c_lname ">
        </asp:SqlDataSource>
        <asp:SqlDataSource ID="SqlDataSource3" runat="server" ConnectionString="<%$ ConnectionStrings:NeonatalConnectionString %>"
            SelectCommand="SELECT [Number], [Label] FROM [_MGest]"></asp:SqlDataSource>
        <asp:SqlDataSource ID="SqlDataSource4" runat="server" ConnectionString="<%$ ConnectionStrings:NeonatalConnectionString %>"
            SelectCommand="select Null as [Number], '' as Label union all SELECT [Number], [Label] FROM [_Gender]"></asp:SqlDataSource>
        &nbsp;<br />
        <br />
        <br />
                </div>
    </form>
</body>
</html>
