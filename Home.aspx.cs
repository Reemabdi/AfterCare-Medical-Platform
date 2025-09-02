using System;

namespace Final11373
{
    public partial class Home : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack) return;

            var role = Session["Role"] as string;
            if (string.IsNullOrEmpty(role)) return;

            var dest = role.Equals("Admin", StringComparison.OrdinalIgnoreCase)
                ? "~/Admin/AdminDashboard.aspx"
                : "~/Patient/PatientDashboard.aspx";

            Response.Redirect(dest, false);
            Context.ApplicationInstance.CompleteRequest();
        }
    }
}
