<%@ Page Language="C#" AutoEventWireup="true"
    CodeBehind="PatientDashboard.aspx.cs"
    Inherits="Final11373.Patient.PatientDashboard" %>

<!DOCTYPE html>
<html>
<head runat="server">
    <title>Patient Dashboard</title>
    <meta name="viewport" content="width=device-width, initial-scale=1" />
    <link href="../Content/bootstrap.min.css" rel="stylesheet" />
    <link href="../Content/styles.css" rel="stylesheet" />
    <style>
        .page-header {
            display:flex; align-items:center; justify-content:space-between;
            padding:16px 24px; background:#5f7ec7; color:#fff; border-radius:0 0 14px 14px;
        }
        .page-header .nav-title { color:#fff; text-decoration:none; font-weight:700; font-size:22px; }
        .nav-buttons a, .nav-buttons button { margin-left:12px; }
        .welcome { display:block; margin:12px 0 18px; font-size:18px; }
        .table { background:#fff; border-radius:10px; overflow:hidden; }
        body { background:#f6f8ff; }
    </style>
</head>
<body>
<form id="form1" runat="server">
    <div class="page-header">
        <div class="header-title">
            <a href="pHome.aspx" class="nav-title">🩺 Aftercare Platform</a>
        </div>
        <div class="nav-buttons">
            <a class="btn btn-primary" href="pHome.aspx">Home</a>
            <a class="btn btn-primary" href="PatientDashboard.aspx">Patient Dashboard</a>
            <a class="btn btn-primary" href="sendEmail.aspx">Send Email</a>
            <asp:Button ID="btnLogout" runat="server" CssClass="btn btn-light" Text="Logout"
                        OnClick="btnLogout_Click" />
        </div>
    </div>

    <div class="container mt-4">
        <h1>Aftercare Instructions</h1>
        <asp:Label ID="lblUserId" runat="server" ForeColor="Gray" />
        <br />
        <asp:Label ID="lblWelcome" runat="server" CssClass="welcome" />

        <h2>Your Aftercare Instructions</h2>

        <asp:GridView ID="gvInstructions" runat="server"
            AutoGenerateColumns="false" CssClass="table table-striped"
            OnRowCommand="gvInstructions_RowCommand">
            <Columns>
                <asp:BoundField DataField="InstructionID" HeaderText="ID" />
                <asp:BoundField DataField="Title"         HeaderText="Title" />
                <asp:BoundField DataField="Content"       HeaderText="Content" />
                <asp:BoundField DataField="CategoryName"  HeaderText="Category" />
                <asp:BoundField DataField="CreatedBy"     HeaderText="Created By" />
                <asp:BoundField DataField="CreatedAt"     HeaderText="Created At"
                                DataFormatString="{0:yyyy-MM-dd HH:mm}" />
                <asp:TemplateField>
                    <ItemTemplate>
                        <asp:Button ID="btnContactDoctor" runat="server"
                            Text="Contact Doctor"
                            CommandName="ContactDoctor"
                            CommandArgument='<%# Eval("InstructionID") %>'
                            CssClass="btn btn-primary btn-sm" />
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
            <EmptyDataTemplate>
                <div class="text-muted p-3">No instructions yet.</div>
            </EmptyDataTemplate>
        </asp:GridView>
    </div>
</form>
</body>
</html>
