using System;

namespace Final11373.Admin
{
    public partial class AdminDashboard : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Response.Cache.SetCacheability(System.Web.HttpCacheability.NoCache);
            Response.Cache.SetNoStore();
            Response.Cache.SetExpires(DateTime.UtcNow.AddMinutes(-1));

            var role = Session["Role"] as string;
            if (!"Admin".Equals(role, StringComparison.OrdinalIgnoreCase))
            {
                var ret = "~/Account/Login.aspx?ReturnUrl=" + Server.UrlEncode(Request.RawUrl);
                Response.Redirect(ret, false);
                Context.ApplicationInstance.CompleteRequest();
            }
        }

    }
}
