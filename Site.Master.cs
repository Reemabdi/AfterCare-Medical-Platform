using System;

namespace Final11373
{
    public partial class SiteMaster : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            // no FormsAuth checks here
        }

        protected void btnLogout_Click(object sender, EventArgs e)
        {
            Session.Clear();
            Session.Abandon();

            Response.Redirect("~/Account/Login.aspx", false);
            Context.ApplicationInstance.CompleteRequest();
        }
    }
}
