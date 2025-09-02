<%@ Page Language="C#" %>
<script runat="server">
protected void Page_Load(object sender, EventArgs e)
{
    if (!Request.IsAuthenticated)
    {
        // اكتب الكوكي ثم اطبع النتيجة بدون أي Redirect
        System.Web.Security.FormsAuthentication.SetAuthCookie("authprobe@example.com", false);
        Response.Write("SET COOKIE. Now press F5 to reload this page.<br/>");
        Response.Write("Request.IsAuthenticated = " + Request.IsAuthenticated);
        return;
    }

    Response.Write("AUTH OK as: " + Context.User.Identity.Name + "<br/>");
    Response.Write("Request.IsAuthenticated = " + Request.IsAuthenticated);
}
</script>
