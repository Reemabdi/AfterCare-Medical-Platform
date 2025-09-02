using System;
using System.Web;
using System.Web.Security;
using System.Web.SessionState;

namespace Final11373
{
    public class Global : System.Web.HttpApplication
    {
        protected void Application_Start(object sender, EventArgs e)
        {
            // Application startup logic (optional)
        }

        void Application_AuthenticateRequest(object sender, EventArgs e)
        {
            // علّقي كل شيء مؤقتًا
        }


    }
}
