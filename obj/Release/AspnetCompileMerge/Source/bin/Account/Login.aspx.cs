using System;

namespace Final11373.Account
{
    public partial class Login : System.Web.UI.Page
    {
        private const string AdminEmail = "rrrr@gmail.com";

        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack) return;

           
            var role = Session["Role"] as string;
            if (string.Equals(role, "Admin", StringComparison.OrdinalIgnoreCase))
            {
                Response.Redirect("~/Admin/AdminDashboard.aspx", false);
                Context.ApplicationInstance.CompleteRequest(); return;
            }
            if (string.Equals(role, "Patient", StringComparison.OrdinalIgnoreCase))
            {
                Response.Redirect("~/Patient/pHome.aspx", false);
                Context.ApplicationInstance.CompleteRequest(); return;
            }
        }

        protected void btnLogin_Click(object sender, EventArgs e)
        {
            var email = (txtEmail.Text ?? "").Trim().ToLowerInvariant();
            if (string.IsNullOrEmpty(email))
            {
                lblMsg.Text = "Enter email."; return;
            }
            
            Session["Email"] = email;

            if (string.Equals(email, AdminEmail, StringComparison.OrdinalIgnoreCase))
            {
                Session["Role"] = "Admin";
                
                var ret = Request.QueryString["ReturnUrl"];
                if (!string.IsNullOrEmpty(ret) &&
                    ret.IndexOf("/Admin/", StringComparison.OrdinalIgnoreCase) >= 0)
                {
                    Response.Redirect(ret, false);
                    Context.ApplicationInstance.CompleteRequest(); return;
                }

                Response.Redirect("~/Admin/AdminDashboard.aspx", false);
                Context.ApplicationInstance.CompleteRequest(); return;
            }

            
            Session["Role"] = "Patient";
            Response.Redirect("~/Patient/pHome.aspx", false); 
            Context.ApplicationInstance.CompleteRequest();
        }
    }
}
