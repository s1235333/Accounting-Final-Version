<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="UserList.aspx.cs" Inherits="AccountingNote.SystemAdmin.UserList" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <asp:Label ID="Label1" runat="server" Text="目前尚無使用者" Visible="False"></asp:Label>
     <asp:Button ID="btn_addUser" runat="server" Text="新增使用者" OnClick="btn_addUser_Click" />
    <asp:GridView ID="gv_UserList" runat="server" AutoGenerateColumns="False" CellPadding="3" BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px">
        <Columns>
            <asp:BoundField DataField="Account" HeaderText="帳號" />
            <asp:BoundField DataField="Name" HeaderText="姓名" />
            <asp:BoundField DataField="Email" HeaderText="電子郵件" />
            <asp:TemplateField HeaderText="編輯">
                <ItemTemplate>
                    <a href="/SystemAdmin/UserDetail.aspx?ID=<%# Eval("ID") %>">Edit</a>
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
        <FooterStyle BackColor="White" ForeColor="#000066" />
        <HeaderStyle BackColor="#006699" Font-Bold="True" ForeColor="White" />
        <PagerStyle BackColor="White" ForeColor="#000066" HorizontalAlign="Left" />
        <RowStyle ForeColor="#000066" />
        <SelectedRowStyle BackColor="#669999" Font-Bold="True" ForeColor="White" />
        <SortedAscendingCellStyle BackColor="#F1F1F1" />
        <SortedAscendingHeaderStyle BackColor="#007DBB" />
        <SortedDescendingCellStyle BackColor="#CAC9C9" />
        <SortedDescendingHeaderStyle BackColor="#00547E" />
    </asp:GridView>
    <br />
    
    </form>
</body>
</html>
