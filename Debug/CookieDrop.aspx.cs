using System;
using System.Web;

namespace Final11373.Debug
{
    public partial class CookieDrop : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Response.Cookies.Add(new HttpCookie("Probe", "1")
            {
                Expires = DateTime.Now.AddMinutes(10),
                Path = "/"
            });
            Response.Redirect("~/Debug/WhoAmI.aspx", true);
        }
    }
}
