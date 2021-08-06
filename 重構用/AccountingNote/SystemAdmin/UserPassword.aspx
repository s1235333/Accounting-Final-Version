<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="UserPassword.aspx.cs" Inherits="AccountingNote.SystemAdmin.UserPassword" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <table>
            <tr>
                <td colspan="2">
                    <h1>流水帳管理系統 - 後台</h1>
                </td>
            </tr>
            
                <td>
                    <a href="UserInfo.aspx">使用者資訊</a><br />
                    <a href="AccountingList.aspx">流水帳管理</a><br />
                    <a href="UserList.aspx">會員管理</a>
                </td>
                
        </table>
        <table>
            <tr>
                <td>
                    <asp:PlaceHolder ID="AccPlaceHolder" runat="server">請再輸入一次帳號與密碼，確認你的身分。
                   
                        <br />
                        帳號
                   
                        <asp:TextBox ID="txtAccount" runat="server"></asp:TextBox>
                        <br />
                        密碼:
                   
                        <asp:TextBox ID="txtPWD" runat="server" TextMode="Password"></asp:TextBox>
                        <br />
                        <asp:Button ID="BtnCheck" runat="server" Text="確認" OnClick="BtnCheck_Click" />
                    </asp:PlaceHolder>
                    <asp:PlaceHolder ID="PWDPlaceHolder" runat="server" Visible="false">
                        請輸入新密碼: <asp:TextBox ID="txtPassword" runat="server" TextMode="Password"></asp:TextBox>
                        <br />
                        新密碼再次確認:
                   
                        <asp:TextBox ID="txtPWDCheck" runat="server" TextMode="Password"></asp:TextBox>
                        <br />
                        <asp:Button ID="BtnOK" runat="server" Text="密碼變更確認" OnClick="BtnOK_Click" />
                    </asp:PlaceHolder>
                    <br />
                    <asp:Literal ID="ltlMsg" runat="server"></asp:Literal>
                </td>
            </tr>
        </table>
    </form>
</body>
</html>
