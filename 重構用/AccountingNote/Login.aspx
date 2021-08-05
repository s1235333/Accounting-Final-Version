﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="AccountingNote.Login" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body>
    <h1>理財小幫手-開始你一天的錢錢日記</h1>
    <form id="form1" runat="server">
        <asp:PlaceHolder ID="plcLogin" runat="server" Visible="false">
        帳號:<asp:TextBox ID="txtAccount" runat="server"></asp:TextBox><br />
        密碼:<asp:TextBox ID="txtPWD" runat="server" TextMode="Password"></asp:TextBox><br /><br />
        <asp:Button ID="btnLogin" runat="server" Text="登入" OnClick="btnLogin_Click" /><br />
        <asp:Literal ID="ltlMsg" runat="server"></asp:Literal>
        </asp:PlaceHolder>
    </form>
</body>
</html>
