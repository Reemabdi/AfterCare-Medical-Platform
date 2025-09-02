<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AdminPanel.aspx.cs" Inherits="Final11373.Admin.AdminPanel" %>

<!DOCTYPE html>
<html>
<head runat="server">
    <title>Admin Panel - Patient Schedule</title>
    <link href="../Content/bootstrap.min.css" rel="stylesheet" type="text/css" />
    <link href="../Content/styles.css" rel="stylesheet" type="text/css" />
</head>
<body>
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

    <form id="form1" runat="server">
        <div class="form-box">
            <h2>All Patients Schedule</h2>

  <asp:GridView 
    ID="gvUserSchedule" 
    runat="server" 
    AutoGenerateColumns="False" 
    CssClass="table table-bordered">
    <Columns>
        <asp:BoundField DataField="FullName" HeaderText="Full Name" />
        <asp:BoundField DataField="Age" HeaderText="Age" />
        <asp:BoundField DataField="Gender" HeaderText="Gender" />
        <asp:BoundField DataField="Condition" HeaderText="Condition" />
        <asp:BoundField DataField="CreatedAt" HeaderText="Registered On" DataFormatString="{0:yyyy-MM-dd HH:mm}" />
    </Columns>
</asp:GridView>


        </div>
    </form>
</body>
</html>
