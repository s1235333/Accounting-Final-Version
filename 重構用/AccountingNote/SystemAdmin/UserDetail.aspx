<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="UserDetail.aspx.cs" Inherits="AccountingNote.SystemAdmin.UserDetail" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
           <table>
        <tr>
            <td>帳號:</td>
            <td><asp:TextBox ID="txtAccount" runat="server" TextMode="SingleLine"></asp:TextBox></td>
        </tr>
        
        <asp:PlaceHolder ID="PlaceHolderPWD" runat="server" Visible="false">
            <tr>
                <td>密碼:</td>
            <td> <asp:TextBox ID="txtPassword" runat="server" TextMode="Password"></asp:TextBox></td>  
            </tr>
            <tr>
                <td>確認密碼:</td>
                <td><asp:TextBox ID="txtPWDCheck" runat="server" TextMode="Password"></asp:TextBox></td>
            </tr>
        </asp:PlaceHolder>
        <tr>
            <td>姓名:</td>
            <td><asp:TextBox ID="txtName" runat="server" style="margin-bottom: 0px"></asp:TextBox></td>
        </tr>
        <tr>
            <td>E-mail:</td>
            <td><asp:TextBox ID="txtMail" runat="server" TextMode="Email"></asp:TextBox></td>
        </tr>
    </table>
                <asp:Button ID="btnSave" runat="server" Text="存檔" OnClick="btnSave_Click"  />
                &nbsp;
                <asp:Button ID="btnDelete" runat="server" Text="刪除" OnClick="btnDelete_Click"/>
                &nbsp;
                <br />
                <a href="UserChangePWD.aspx" runat="server" id="pwLink">進入密碼變更頁面</a>
                <br />
                <asp:Literal ID="ltMsg" runat="server"></asp:Literal>
    </form>
</body>
</html>
