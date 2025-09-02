<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Register.aspx.cs" Inherits="Final11373.Account.Register" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Register</title>
    <link href="../Content/bootstrap.min.css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
        <div class="container mt-5">
            <h2>Register</h2>

                <asp:Button ID="btnBack" runat="server" Text="← Home"
                CssClass="btn btn-outline-secondary back-btn"
                CausesValidation="false"
                PostBackUrl="~/Home.aspx" />

            <asp:Label ID="lblStatus" runat="server" CssClass="text-success" />
            <asp:Label ID="lblMessage" runat="server" CssClass="text-danger" />

            <div class="form-group">
                <label>Email:</label>
                <asp:TextBox ID="txtEmail" runat="server" CssClass="form-control" />
            </div>

            <div class="form-group">
                <label>Password:</label>
                <asp:TextBox ID="txtPassword" runat="server" TextMode="Password" CssClass="form-control" />
            </div>

            <div class="form-group">
                <label>Role:</label>
                <asp:DropDownList ID="ddlRole" runat="server" CssClass="form-control">
                    <asp:ListItem Text="Patient" Value="Patient" />
                    <asp:ListItem Text="Admin" Value="Admin" />
                </asp:DropDownList>
            </div>

            <div class="form-group">
                <label>Full Name:</label>
                <asp:TextBox ID="txtFullName" runat="server" CssClass="form-control" />
            </div>

            <div class="form-group">
                <label>Gender:</label>
                <asp:DropDownList ID="ddlGender" runat="server" CssClass="form-control">
                    <asp:ListItem Text="Male" />
                    <asp:ListItem Text="Female" />
                </asp:DropDownList>
            </div>

            <div class="form-group">
                <label>Age:</label>
                <asp:TextBox ID="txtAge" runat="server" CssClass="form-control" />
            </div>

            <div class="form-group">
                <label>Condition:</label>
                <asp:TextBox ID="txtCondition" runat="server" CssClass="form-control" />
            </div>


            <asp:Button ID="btnRegister" runat="server" CssClass="btn btn-primary mt-3" Text="Register" OnClick="btnRegister_Click" />
        </div>
    </form>
</body>
</html>
