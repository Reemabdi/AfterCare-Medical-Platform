using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Final11373.Patient
{
    public partial class SessionTest : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Session["TestSession"] = "✅ Session Works!";
            lblSession.Text = Session["TestSession"]?.ToString() ?? "❌ Session Failed!";
        }
    }


}
