using System;

namespace Final11373
{
    public partial class About : System.Web.UI.Page
    {
        
        private const string CreatorName = "Reema Alaboudi";

        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack) return;

            lblCreatedBy.Text =  CreatorName;
       
            Response.Cache.SetCacheability(System.Web.HttpCacheability.NoCache);
            Response.Cache.SetNoStore();
            Response.Cache.SetExpires(DateTime.UtcNow.AddMinutes(-1));
        }
    }
}
