<%@ Page Language="C#" %>
<script runat="server">
protected void Page_Load(object sender, EventArgs e)
{
    if (!IsPostBack)
    {
        Response.Cookies.Add(new System.Web.HttpCookie("probe_cookie","1"){ Path="/", HttpOnly=true });
        msg.Text = "WROTE cookie. Now refresh this page (F5).";
    }
    else
    {
        var c = Request.Cookies["probe_cookie"];
        msg.Text = "READ cookie: " + (c==null ? "NULL" : c.Value);
    }
}
</script>
<!DOCTYPE html>
<html><body>
<form id="form1" runat="server">
  <h3>CookieProbe</h3>
  <asp:Label ID="msg" runat="server" />
  <br/><asp:Button runat="server" Text="Postback" />
</form>
</body></html>
