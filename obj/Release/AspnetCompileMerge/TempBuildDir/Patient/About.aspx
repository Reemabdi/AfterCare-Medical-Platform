<%@ Page Language="C#"
    MasterPageFile="~/Site.Master"
    AutoEventWireup="true"
    CodeBehind="About.aspx.cs"
    Inherits="Final11373.About"
    Title="About - Aftercare Platform" %>

<asp:Content ID="HeadExtras" ContentPlaceHolderID="HeadContent" runat="server">
    <style>
        .about-hero { background:#f6f8ff; border:1px solid #e5e9ff; border-radius:12px; padding:24px; }
        .about-section { margin-top:24px; }
        .about-section h3 { margin-bottom:10px; }
        .badge { display:inline-block; padding:6px 10px; border-radius:999px; background:#e9eefc; }
    </style>
</asp:Content>

<asp:Content ID="Main" ContentPlaceHolderID="MainContent" runat="server">
    <div class="about-hero">
        <h1>About Aftercare Platform</h1>
      
        <p class="mt-2">
            Aftercare Platform helps patients follow clear, personalized instructions
            after procedures and surgeries. Doctor create and manage <em> aftercare instructions </em>,
            and each patient sees only their own guidance when they sign in.
        </p>
    </div>

  

     <div class="about-section">
        <h3>Created by</h3>
        <p>
            <asp:Label ID="lblCreatedBy" runat="server" CssClass="fw-bold" />
        </p>
       
    </div>
</asp:Content>
