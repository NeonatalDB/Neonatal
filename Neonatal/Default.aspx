<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="Neonatal.Default" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Ian and Robs Webpage</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <h1>How about this plan!</h1>
        If we actually use the real database instead of a copy we wont have any database sturcture sync issues.<br />
        We could either connect at work directly to teh sql or via vpn if away from work<br />
        <br />
        I cannot use Github when I am at work but am currently at home connected to the VPN (so database works) and am also able to sync my changes with Github.<br />
        THe web client would be out of sync when we are at work but this can be done intermittently at home<br />
        &#39;?<br />
        Thoughts?<br />
        <br />
        I have linked some data below from PSN server as part of the concept<p>
            .
          </p>
        <p>
            &nbsp;</p>
        
        <p>

            <asp:GridView ID="GridView1" runat="server" AllowPaging="True" AllowSorting="True" AutoGenerateColumns="False" DataKeyNames="HospCode,BedName" DataSourceID="SqlDataSource1">
                <Columns>
                    <asp:CommandField ShowSelectButton="True" />
                    <asp:BoundField DataField="HospCode" HeaderText="HospCode" ReadOnly="True" SortExpression="HospCode" />
                    <asp:BoundField DataField="BedName" HeaderText="BedName" ReadOnly="True" SortExpression="BedName" />
                </Columns>
            </asp:GridView>
            <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConflictDetection="CompareAllValues" ConnectionString="<%$ ConnectionStrings:NeonatalConnectionString %>" DeleteCommand="DELETE FROM [_Beds] WHERE [HospCode] = @original_HospCode AND [BedName] = @original_BedName" InsertCommand="INSERT INTO [_Beds] ([HospCode], [BedName]) VALUES (@HospCode, @BedName)" OldValuesParameterFormatString="original_{0}" SelectCommand="SELECT [HospCode], [BedName] FROM [_Beds] ORDER BY [HospCode], [BedOrder], [BedName]">
                <DeleteParameters>
                    <asp:Parameter Name="original_HospCode" Type="String" />
                    <asp:Parameter Name="original_BedName" Type="String" />
                </DeleteParameters>
                <InsertParameters>
                    <asp:Parameter Name="HospCode" Type="String" />
                    <asp:Parameter Name="BedName" Type="String" />
                </InsertParameters>
            </asp:SqlDataSource>
        </p>

        <p>Do you have the Github working properly from VS?</p>


    </div>
    </form>
</body>
</html>
