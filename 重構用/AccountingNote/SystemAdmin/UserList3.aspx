<%@ Page Title="" Language="C#" MasterPageFile="~/SystemAdmin/Admin.Master" AutoEventWireup="true" CodeBehind="UserList3.aspx.cs" Inherits="AccountingNote.SystemAdmin.UserList3" %>

<%@ Register Src="~/UserControls/Ucpages.ascx" TagPrefix="uc1" TagName="Ucpages" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:Button ID="btnNew" runat="server" Text="Add" />
    <asp:GridView ID="GridView1" runat="server"></asp:GridView>
    <uc1:Ucpages runat="server" id="Ucpages" />
</asp:Content>


