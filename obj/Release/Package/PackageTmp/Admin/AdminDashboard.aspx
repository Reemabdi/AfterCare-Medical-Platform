<%@ Page Language="C#" MasterPageFile="~/Site.Master"
    AutoEventWireup="true"
    CodeBehind="AdminDashboard.aspx.cs"
    Inherits="Final11373.Admin.AdminDashboard"
    Title="Admin Dashboard" %>


<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <h2>Admin Dashboard</h2>
    <asp:Button ID="btnManageInstructions" runat="server" Text="Manage Instructions"
        PostBackUrl="~/Admin/ManageInstructions.aspx" CssClass="aspNetButton" />
</asp:Content>
