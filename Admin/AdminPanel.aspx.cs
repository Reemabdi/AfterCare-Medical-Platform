using System;
using System.Data;
using System.Data.SqlClient;

using System.Web.UI;

using System.Configuration;

namespace Final11373.Admin
{
    public partial class AdminPanel : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadUserSchedule();
            }
        }

        private void LoadUserSchedule()
        {
              string connStr = ConfigurationManager.ConnectionStrings["AftercareDBConnectionString"].ConnectionString;



            using (SqlConnection conn = new SqlConnection(connStr))
            {
                string query = @"
            SELECT 
                FullName,
                Age,
                Gender,
                Condition,
                CreatedAt
            FROM PatientProfile
            ORDER BY CreatedAt DESC";

                SqlCommand cmd = new SqlCommand(query, conn);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);

                gvUserSchedule.DataSource = dt;
                gvUserSchedule.DataBind();
            }
        }


        
        public override void VerifyRenderingInServerForm(Control control)
        {
            
        }
        protected void btnLogout_Click(object sender, EventArgs e)
        {
            Session.Clear();
            Session.Abandon();
            Response.Redirect("~/Home.aspx"); 
        }


    }
}
