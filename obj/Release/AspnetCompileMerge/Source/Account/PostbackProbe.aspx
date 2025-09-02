<%@ Page Language="C#" %>
<script runat="server">
protected void Page_Load(object sender, EventArgs e)
{
    if (!IsPostBack) lbl.Text = "First load " + DateTime.Now.ToString("HH:mm:ss.fff");
}
protected void btn_Click(object sender, EventArgs e)
{
    lbl.Text = "POSTBACK OK " + DateTime.Now.ToString("HH:mm:ss.fff");
}
</script>
<!DOCTYPE html>
<html><body>
<form id="form1" runat="server">
  <h3>PostbackProbe</h3>
  <asp:Button ID="btn" runat="server" Text="Click"
              OnClick="btn_Click" UseSubmitBehavior="false" />
  <br/><asp:Label ID="lbl" runat="server" />
</form>
</body></html>
