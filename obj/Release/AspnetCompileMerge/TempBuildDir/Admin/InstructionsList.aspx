<%@ Page Title="Instruction List" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="InstructionsList.aspx.cs" Inherits="Final11373.Admin.InstructionsList" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <style>
        .instruction-list {
            display: flex;
            flex-direction: column;
            gap: 20px;
            max-width: 600px;
            margin: auto;
        }

        .instruction-card {
            padding: 15px;
            border: 1px solid #ccc;
            border-radius: 8px;
            background-color: #fdfdfd;
            box-shadow: 0 2px 5px rgba(0, 0, 0, 0.05);
        }

        .instruction-card h4 {
            margin: 0 0 10px 0;
            color: #333;
        }

        .instruction-card p {
            margin: 0 0 8px 0;
            color: #555;
        }

        .instruction-card small {
            color: #888;
        }

        .export-buttons {
            text-align: center;
            margin-bottom: 20px;
        }

        .export-buttons .asp-button {
            margin: 5px;
            padding: 8px 16px;
            background-color: #007bff;
            color: white;
            border: none;
            border-radius: 5px;
            cursor: pointer;
        }

        .export-buttons .asp-button:hover {
            background-color: #0056b3;
        }

        .export-buttons .btn-back {
            background-color: #6c757d;
        }

        .export-buttons .btn-back:hover {
            background-color: #5a6268;
        }
    </style>


    <div class="export-buttons">
        <asp:Button ID="btnExportWord" runat="server" Text="Export to Word" CssClass="asp-button" OnClick="btnExportWord_Click" />
        <asp:Button ID="btnExportPDF" runat="server" Text="Export to PDF" CssClass="asp-button" OnClick="btnExportPDF_Click" />
        

    <!-- Repeater Content -->
    <asp:Repeater ID="rptInstructions" runat="server">
        <HeaderTemplate>
            <div class="instruction-list">
        </HeaderTemplate>
       <ItemTemplate>
    <div class="instruction-card">
        <h4><asp:Label ID="lblTitle" runat="server" Text='<%# Eval("Title") %>'></asp:Label></h4>
        <p><asp:Label ID="lblContent" runat="server" Text='<%# Eval("Content") %>'></asp:Label></p>
        <small><asp:Label ID="lblDate" runat="server" Text='<%# Eval("CreatedAt", "{0:yyyy-MM-dd}") %>'></asp:Label></small>
    </div>
</ItemTemplate>

        <FooterTemplate>
            </div>
        </FooterTemplate>
    </asp:Repeater>
</asp:Content>
