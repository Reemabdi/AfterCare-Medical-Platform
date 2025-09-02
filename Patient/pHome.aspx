<%@ Page Language="C#" MasterPageFile="~/Site.Master"
    AutoEventWireup="true"
    CodeBehind="pHome.aspx.cs"
    Inherits="Final11373.Patient.pHome"
    Title="Patient Home" %>


<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <asp:Label ID="lblWelcome" runat="server" Font-Bold="true" ForeColor="Green" Font-Size="Large" />
    <br />
    <asp:HyperLink ID="lnkSurvey" runat="server" NavigateUrl="~/Patient/Survey.aspx" CssClass="btn btn-success">
        Tell us how you feel today
    </asp:HyperLink>
    <br /><br />
   
 
</asp:Content>
