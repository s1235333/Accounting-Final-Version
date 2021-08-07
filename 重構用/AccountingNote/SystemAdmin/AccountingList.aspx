<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AccountingList.aspx.cs" Inherits="AccountingNote.SystemAdmin.AccountingList" %>

<%@ Register Src="~/UserControls/Ucpages.ascx" TagPrefix="uc1" TagName="Ucpages" %>


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
                <td colspan="2">
                    <h1>流水帳管理系統-後台</h1>
                </td>
            </tr>
            <tr>
                <td>
                    <a href ="UserInfo.aspx">使用者資訊</a><br />
                    <a href="AccountingList.aspx">流水帳管理</a><br />
                     <a href="UserList.aspx">會員管理</a>

                </td>
                <td>
                        <%--這裡放主要內容--%>
                    <asp:Button ID="btnCreate" runat="server" Text="新增" OnClick="btnCreate_Click1" />
                    <asp:GridView ID="gvAccountingList" runat="server" AutoGenerateColumns="False" 
                     OnRowDataBound="gvAccountingList_RowDataBound" BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CellPadding="3">  <%--事件RowDataBound--%>
                        <Columns>
                            <asp:BoundField HeaderText="建立日期" DataField="CreateDate" DataFormatString="{0:yyyy-MM-dd}"/>
                             <asp:TemplateField HeaderText="收/支">
                                <ItemTemplate>
                                   <%-- <%# ((int)Eval("ActType") == 0) ?"支出" :"收入" %>--%>
                               <%--     <asp:Literal runat="server" ID="ltActType"></asp:Literal>--%><%--此處改寫為不使用樣板來顯示支出及收入--%>
                                    <asp:Label ID="lblActType" runat="server" Text="Label"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                             <asp:BoundField HeaderText="金額" DataField="Amount" />
                            <asp:BoundField HeaderText="標題" DataField="Caption" />  
                            <asp:TemplateField HeaderText="Act">
                                <ItemTemplate>
                                    <a href="/SystemAdmin/AccountingDetail.aspx?ID=<%# Eval("ID")%>">編輯</a>
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
                    </asp:GridView >

                    <asp:Literal ID="ltPager" runat="server"></asp:Literal>
                    <uc1:Ucpages runat="server" ID="Ucpages" PageSize="10" TotalSize="10" CurrentPage="1" Url="AccountingList.aspx" />

                    <asp:PlaceHolder ID="PlcNoData" runat="server" Visible="false">
                    <p style="color: red; background-color:aquamarine">
                        No data in your AccountingNote.
                    </p>
                    </asp:PlaceHolder>
                     <asp:Label ID="lbl_Total" runat="server" Text=""></asp:Label>
                </td>
            </tr>
        </table>

    </form>
</body>
</html>
