<%@ Page Language="C#" AutoEventWireup="true"
    CodeBehind="ManageInstructions.aspx.cs"
    Inherits="Final11373.Admin.ManageInstructions" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Manage Instructions</title>
    <link href="~/Content/bootstrap.min.css" rel="stylesheet" />
    <link href="~/Content/styles.css" rel="stylesheet" />
    <style>
        .id-link { cursor:pointer; text-decoration:underline; }
    </style>
</head>
<body>
<form id="form1" runat="server">

    <!-- Header -->
    <div class="page-header">
        <div class="header-title">
            <a href="AdminDashboard.aspx" class="nav-title">🩺 Aftercare Platform</a>
        </div>
        <div class="nav-buttons">
            <a href="AdminDashboard.aspx">Home</a>
            <a href="ManageInstructions.aspx">Manage Instructions</a>
            <a href="AdminPanel.aspx">Admin Panel</a>
            <a href="InstructionsList.aspx">Instruction List</a>
            <asp:LinkButton ID="btnLogout" runat="server" CssClass="nav-logout" OnClick="btnLogout_Click">Logout</asp:LinkButton>
        </div>
    </div>

    <h2>Manage Instructions</h2>
    <asp:Label ID="lblMessage" runat="server" ForeColor="ForestGreen"></asp:Label>

    
        <label>Patient</label><div style="margin:12px 0;">
        &nbsp;<asp:DropDownList ID="ddlPatients" runat="server" AutoPostBack="true"
            OnSelectedIndexChanged="ddlPatients_SelectedIndexChanged" Width="363px" Height="16px" />
    </div>

    <asp:HiddenField ID="hfInstructionID" runat="server" />
    <div style="margin:12px 0;">
        <div>Instruction Title</div>
        <asp:TextBox ID="txtTitle" runat="server" Width="1897px" />
    </div>
    <div style="margin:12px 0;">
        <div>Instruction Content</div>
        <asp:TextBox ID="txtContent" runat="server" TextMode="MultiLine" Rows="6" Width="1894px" />
    </div>

    <div style="margin:12px 0;">
        <asp:Button ID="btnSubmit" runat="server" Text="Save" CssClass="btn btn-primary" OnClick="btnSubmit_Click" />
        <asp:Button ID="btnUpdate" runat="server" Text="Update" CssClass="btn btn-success" OnClick="btnUpdate_Click" Visible="false" />
        <asp:Button ID="btnDelete" runat="server" Text="Delete"
    OnClick="btnDelete_Click"
    OnClientClick="return confirm('Are you sure you want to delete this item?');" />

        <asp:Button ID="btnClear" runat="server" Text="Clear" CssClass="btn btn-secondary" OnClick="btnClear_Click" />
        <asp:Button ID="btnGetData" runat="server" Text="Get Data" CssClass="btn btn-info"
            OnClick="btnGetData_Click" />
<asp:Button ID="btnExportTopExcel" runat="server"
    Text="ExportTopExcel"
    OnClick="btnExportSafeCsv_Click" />

    </div>

 
 <asp:GridView ID="gvInstructions" runat="server"
    AutoGenerateColumns="False" CssClass="table table-striped"
    OnRowCommand="gvInstructions_RowCommand">
    <Columns>
        <asp:TemplateField HeaderText="ID">
            <ItemTemplate>
                <asp:LinkButton ID="lnkId" runat="server"
                    Text='<%# Eval("InstructionID") %>'
                    CommandName="EditInstruction"
                    CommandArgument='<%# Eval("InstructionID") %>'
                    CssClass="id-link" />
            </ItemTemplate>
        </asp:TemplateField>

        <asp:BoundField DataField="Title" HeaderText="Title" />
        <asp:BoundField DataField="CategoryName" HeaderText="Category" />
        <asp:BoundField DataField="CreatedBy" HeaderText="By" />
        <asp:BoundField DataField="CreatedAt" HeaderText="Created At"
                        DataFormatString="{0:yyyy-MM-dd HH:mm}" HtmlEncode="false" />
    </Columns>
</asp:GridView>


</form>
</body>
</html>
