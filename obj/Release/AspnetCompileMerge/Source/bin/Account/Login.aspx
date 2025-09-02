<%@ Page Language="C#" AutoEventWireup="true"
    CodeBehind="Login.aspx.cs"
    Inherits="Final11373.Account.Login" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
  <title>Login</title>
  <meta name="viewport" content="width=device-width, initial-scale=1" />
  <link href="../Content/bootstrap.min.css" rel="stylesheet" />
  <link href="../Content/styles.css" rel="stylesheet" />
  <style>
    body { background:#fafbff; font-family:Arial, sans-serif; }

    /* زر العودة أعلى الصفحة */
    .back-btn {
        position: fixed;   /* ثابت في أعلى الصفحة */
        top: 20px;
        left: 20px;
        z-index: 1000;
    }

    /* الصندوق بنفس الحجم اللي عندك */
    .login-card { 
        max-width:540px; 
        margin:6rem auto; 
        padding:2rem 2.25rem; 
        background:#fff; 
        border-radius:16px; 
        box-shadow:0 10px 30px rgba(0,0,0,.08); 
    }

    .muted { color:#6b7280; }
    .form-control { width:100% !important; }
  </style>
</head>
<body>
  <form id="form1" runat="server">
    
 
    <asp:Button ID="btnBack" runat="server" Text="← Home"
                CssClass="btn btn-outline-secondary back-btn"
                CausesValidation="false"
                PostBackUrl="~/Home.aspx" />

   
    <div class="login-card">
      <h2 class="h4 mb-3 text-center">Login</h2>
  
      <div class="mb-3">
        <label class="form-label">Email</label>
        <asp:TextBox ID="txtEmail" runat="server" CssClass="form-control" />
        <asp:RequiredFieldValidator runat="server" ControlToValidate="txtEmail"
          ErrorMessage="Email is required" Display="Dynamic" ForeColor="Red" ValidationGroup="login" />
      </div>

      <div class="mb-3">
        <label class="form-label">Password</label>
        <asp:TextBox ID="txtPassword" runat="server" TextMode="Password" CssClass="form-control" />
        <asp:RequiredFieldValidator runat="server" ControlToValidate="txtPassword"
          ErrorMessage="Password is required" Display="Dynamic" ForeColor="Red" ValidationGroup="login" />
      </div>

      <asp:Button ID="btnLogin" runat="server" Text="Login"
                  UseSubmitBehavior="true"
                  OnClick="btnLogin_Click"
                  CssClass="btn btn-primary w-100"
                  ValidationGroup="login" />

      <div class="mt-3">
        <asp:Label ID="lblMsg"   runat="server" ForeColor="Red" EnableViewState="false" />
        <br />
        <asp:Label ID="lblTrace" runat="server" CssClass="muted"   EnableViewState="false" />
      </div>
    </div>
  </form>
</body>
</html>
