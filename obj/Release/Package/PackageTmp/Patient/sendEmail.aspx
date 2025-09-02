<%@ Page Title="Send Email" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="sendEmail.aspx.cs" Inherits="Final11373.Patient.sendEmail" %>


<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <h2>Send Email to Doctor</h2>
    <asp:Label ID="lblMsg" runat="server" Text=""></asp:Label><br /><br />
    <table>
        <tr>
            <td>From Email:</td>
            <td><asp:TextBox ID="txtSenderEmail" runat="server" Width="300px" /></td>
        </tr>
        <tr>
            <td>Subject:</td>
            <td><asp:TextBox ID="txtSubject" runat="server" Width="300px" /></td>
        </tr>
        <tr>
            <td>Attachments:</td>
            <td><asp:FileUpload ID="fuAttachment" runat="server" AllowMultiple="true" Width="300px" /></td>
        </tr>
        <tr>
            <td>Message:</td>
            <td><asp:TextBox ID="txtBody" runat="server" TextMode="MultiLine" Rows="6" Width="300px" /></td>
        </tr>
        <tr>
            <td></td>
            <td><asp:Button ID="btnSend" runat="server" Text="Send Email" OnClick="btnSend_Click" /></td>
        </tr>
    </table>

</asp:Content>
