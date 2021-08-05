<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="AccountingNote.Default" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
         <h1>理財小幫手-開始你一天的錢錢日記</h1>
        初次記帳:<asp:Literal ID="ltlFirst" runat="server"></asp:Literal><br />
        最後記帳:<asp:Literal ID="ltlLast" runat="server"></asp:Literal><br />
        記帳數量:<asp:Literal ID="ltlCount" runat="server"></asp:Literal><br />
        會員數:<asp:Literal ID="ltlMember" runat="server"></asp:Literal><br />
        <asp:Literal ID="ltlmsg" runat="server"></asp:Literal>
        <asp:Button ID="btnLogin" runat="server" Text="點我登入"  OnClick="btnLogin_Click"/>
    </form>
</body>
</html>

