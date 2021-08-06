<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="UserDetail.aspx.cs" Inherits="AccountingNote.SystemAdmin.UserDetail" %>

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
            <tr>
                <td>
                    <a href="UserInfo.aspx">使用者資訊</a><br />
                    <a href="AccountingList.aspx">流水帳管理</a><br />
                    <a href="UserList.aspx">會員管理</a>
                </td>
                <td>
                    <!--這裡放主要內容-->
                    <table>
                        <tr>
                            <th>帳號</th>
                            <td>
                                <asp:TextBox ID="txt_Account" runat="server"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <th>姓名</th>
                            <td>
                                <asp:TextBox ID="txt_Name" runat="server"></asp:TextBox> 
                            </td>
                        </tr>
                        <tr>
                            <th>Email</th>
                            <td>
                                <asp:TextBox ID="txt_Mail" runat="server"></asp:TextBox>
                            </td>
                        </tr>
                        <asp:PlaceHolder ID="PlacePWD" runat="server">
                        <tr>
                            <th>密碼</th>
                            <td>
                                <asp:TextBox ID="txt_PWD" runat="server"></asp:TextBox></td>
                        </tr>
                        <tr>
                            <th>確認密碼</th>
                            <td>
                                <asp:TextBox ID="txt_CheckPWD" runat="server"></asp:TextBox></td>
                        </tr>
                   </asp:PlaceHolder>
                    </table>
                    <
                    <placeholder>
                    <asp:Button ID="btnSave" runat="server" Text="存檔" OnClick="btnSave_Click" />  
                    &nbsp;
                    <asp:Button ID="btnDelete" runat="server" Text="刪除" OnClick="btnDelete_Click" />  
                    &nbsp;
                    <br />  
                    <asp:Button ID="btnPassword" runat="server" Text="前往變更密碼" OnClick="btnPassword_Click" />
                    <br />
                    <asp:Literal ID="ltMsg" runat="server"></asp:Literal>
                    </placeholder>
                </td>
            </tr>
        </table>

    </form>
</body>
</html>
