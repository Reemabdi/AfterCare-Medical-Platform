using System;
using System.Web.Security;

namespace Final11373
{
    public partial class RedirectHelper : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (User.IsInRole("Admin"))
                Response.Redirect("~/Admin/AdminDashboard.aspx");
            else if (User.IsInRole("Patient"))
                Response.Redirect("~/Patient/pHome.aspx");
            else
                Response.Redirect("~/Home.aspx"); // <-- You are landing here

        }
    }
}

