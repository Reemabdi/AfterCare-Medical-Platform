using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;

using System.Configuration;

namespace Final11373 { 
    public partial class TestConnection : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string connStr = ConfigurationManager.ConnectionStrings["MyDB"].ConnectionString;
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                try
                {
                    conn.Open();
                    lblResult.Text = "✅ Connection successful!";
                }
                catch (Exception ex)
                {
                    lblResult.Text = "❌ Error: " + ex.Message;
                }
            }
        }
    }
}