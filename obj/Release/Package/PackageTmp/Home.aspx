<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Home.aspx.cs" Inherits="Final11373.Home" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Aftercare Platform</title>
    <!-- Use app-root URLs -->
    <link href="<%= ResolveUrl("~/Content/bootstrap.min.css") %>" rel="stylesheet" />
    <link href="<%= ResolveUrl("~/Content/styles.css") %>" rel="stylesheet" />
    <style>
        body {
            font-family: Arial, sans-serif;
            text-align: center;
            padding-top: 100px;
            background-color: #f0f0f0;
        }
        h1 { color: #333; }
        .btn {
            display: inline-block;
            margin: 15px;
            padding: 15px 40px;
            font-size: 18px;
            color: #fff;
            background-color: #007ACC;
            border: none;
            border-radius: 6px;
            cursor: pointer;
            text-decoration: none;
        }
        .btn:hover { background-color: #005F9E; }
    </style>
</head>
<body>
  <div class="text-center" style="margin-top:120px">
    <h1>Welcome to Aftercare Platform</h1>
    <div class="mt-4">
     
      <a class="btn btn-primary btn-lg" href="<%= ResolveUrl("~/Account/Register.aspx") %>">Register</a>
      <a class="btn btn-primary btn-lg" href="<%= ResolveUrl("~/Account/Login.aspx") %>">Login</a>
    </div>
  </div>
</body>
</html>
