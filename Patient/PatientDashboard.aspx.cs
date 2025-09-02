using System;
using System.Data;
using System.Data.SqlClient;
using System.Web.Configuration;
using System.Web.UI;

namespace Final11373.Patient
{
    public partial class PatientDashboard : Page
    {
        private readonly string connStr =
            WebConfigurationManager.ConnectionStrings["AftercareDBConnectionString"].ConnectionString;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack) return;

            // get email the same way login stored it
            string email =
                (Session["Email"] as string)?.Trim().ToLowerInvariant() ??
                (Session["UserName"] as string)?.Trim().ToLowerInvariant() ??
                (User?.Identity?.Name?.Trim().ToLowerInvariant());

            lblWelcome.Text = "Welcome to your Dashboard, " + (email ?? "guest");
            lblUserId.Text = "Signed in as: " + (email ?? "");

            LoadInstructionsByEmail(email);
        }

        private void LoadInstructionsByEmail(string email)
        {
            // if email missing, show empty grid gracefully
            if (string.IsNullOrWhiteSpace(email))
            {
                gvInstructions.DataSource = new DataTable();
                gvInstructions.DataBind();
                return;
            }

            var sql = @"
DECLARE @uid UNIQUEIDENTIFIER;
SELECT @uid = m.UserId
FROM dbo.aspnet_Membership m
WHERE LOWER(m.Email) = LOWER(@Email);

SELECT  i.InstructionID, i.Title, i.Content,
        ISNULL(c.CategoryName,'Uncategorized') AS CategoryName,
        i.CreatedBy, i.CreatedAt
FROM    dbo.Instructions i
LEFT    JOIN dbo.Categories c ON c.CategoryID = i.CategoryID
WHERE   i.UserId = @uid
ORDER BY i.CreatedAt DESC;";

            using (var cn = new SqlConnection(connStr))
            using (var da = new SqlDataAdapter(sql, cn))
            {
                da.SelectCommand.Parameters.AddWithValue("@Email", email);
                var dt = new DataTable();
                da.Fill(dt);
                gvInstructions.DataSource = dt;
                gvInstructions.DataBind();
            }
        }

        protected void btnLogout_Click(object sender, EventArgs e)
        {
            Session.Clear(); Session.Abandon();
            Response.Redirect("~/Account/Login.aspx", false);
            Context.ApplicationInstance.CompleteRequest();
        }

        protected void gvInstructions_RowCommand(object sender, System.Web.UI.WebControls.GridViewCommandEventArgs e)
        {
            if (e.CommandName == "ContactDoctor")
            {
                int id;
                if (int.TryParse(e.CommandArgument.ToString(), out id))
                {
                    Response.Redirect("~/Patient/SendEmail.aspx?instructionId=" + id);
                }
            }
        }
    }
}
