using System;
using System.Configuration;
using System.Data.SqlClient;
using System.Web.Security;
using System.Web.UI.WebControls;
using System.Linq;
using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

using System.Web.Security;
using System.Data;
using System.Text;
using System.Drawing;

using System.Web.Configuration;

namespace Final11373.Patient
{
    public partial class Survey : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
        }
        protected void btnSubmitSurvey_Click(object sender, EventArgs e)
        {
            try
            {
                //  selected mood
                string mood = rblMood.SelectedValue;

                //  selected symptoms
                string symptoms = string.Join(",",
                    cbxSymptoms.Items.Cast<ListItem>().Where(i => i.Selected).Select(i => i.Value));

                //  text answers
                string changes = txtChanges.Text;
                string concerns = txtConcerns.Text;

                // Save to DB
                using (SqlConnection conn = new SqlConnection(
                    ConfigurationManager.ConnectionStrings["AftercareDBConnectionString"].ConnectionString))
                {
                    string query = @"INSERT INTO PatientSurvey (Mood, Symptoms, SubmittedAt)
                             VALUES (@Mood, @Symptoms, GETDATE())";

                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@Mood", string.IsNullOrEmpty(mood) ? (object)DBNull.Value : mood);
                    cmd.Parameters.AddWithValue("@Symptoms", string.IsNullOrEmpty(symptoms) ? (object)DBNull.Value : symptoms);

                    conn.Open();
                    cmd.ExecuteNonQuery();
                }

               
                lblSurveyStatus.Text = "Survey submitted successfully ✅";
                lblSurveyStatus.ForeColor = System.Drawing.Color.Green;

                //  back button
                btnBack.Visible = true;

                //  clear 
                rblMood.ClearSelection();
                foreach (ListItem item in cbxSymptoms.Items) item.Selected = false;
                txtChanges.Text = "";
                txtConcerns.Text = "";
            }
            catch (Exception ex)
            {
                lblSurveyStatus.Text = "Error: " + ex.Message;
                lblSurveyStatus.ForeColor = System.Drawing.Color.Red;
            }
        }

        protected void btnBack_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Patient/pHome.aspx");
        }


    }
}
