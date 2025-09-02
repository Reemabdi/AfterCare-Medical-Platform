using System;
using System.Web.UI;

namespace Final11373.Patient
{
    public partial class pHome : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Response.Cache.SetCacheability(System.Web.HttpCacheability.NoCache);
            Response.Cache.SetNoStore();
            Response.Cache.SetExpires(DateTime.UtcNow.AddMinutes(-1));

            var role = Session["Role"] as string;
            if (string.IsNullOrEmpty(role) ||
                !(role.Equals("Patient", StringComparison.OrdinalIgnoreCase) ||
                  role.Equals("Admin", StringComparison.OrdinalIgnoreCase)))
            {
                var ret = Server.UrlEncode(Request.RawUrl);
                Response.Redirect("~/Account/Login.aspx?ReturnUrl=" + ret, false);
                Context.ApplicationInstance.CompleteRequest();
                return;
            }

            if (!IsPostBack)
            {
                var nameOrEmail =
                    (Session["UserName"] as string) ??
                    (Session["Email"] as string) ??
                    (User?.Identity?.Name ?? "guest");

                lblWelcome.Text = "✅ Welcome to Patient Home, " + nameOrEmail;
            }
        }
    }
}
