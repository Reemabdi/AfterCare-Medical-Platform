<%@ Page Language="C#" %>
<%@ Import Namespace="System.Web.Security" %>
<script runat="server">
protected void Page_Load(object sender, EventArgs e) {}
protected void btn_Click(object sender, EventArgs e)
{
    bool ok = Membership.ValidateUser(u.Text.Trim(), p.Text);
    res.Text = "ValidateUser = " + ok;
}
</script>
<!DOCTYPE html>
<html><body>
<form id="form1" runat="server">
  <h3>MembershipProbe</h3>
  Username: <asp:TextBox ID="u" runat="server" /><br/>
  Password: <asp:TextBox ID="p" runat="server" TextMode="Password" /><br/>
  <asp:Button ID="btn" runat="server" Text="Check"
              OnClick="btn_Click" UseSubmitBehavior="false" />
  <br/><asp:Label ID="res" runat="server" />
</form>
</body></html>
