using System;
using System.Data;
using System.Data.SqlClient;
using System.Web.Configuration;
using System.Web.Security;

namespace Final11373.Account
{
    public partial class Register : System.Web.UI.Page
    {
        private readonly string connStr =
            WebConfigurationManager.ConnectionStrings["AftercareDBConnectionString"].ConnectionString;

        protected void Page_Load(object sender, EventArgs e)
        {
        }

        protected void btnRegister_Click(object sender, EventArgs e)
        {
            lblMessage.Text = "";
            lblStatus.Text = "";

            var username = txtEmail.Text.Trim();     // using email as username
            var password = txtPassword.Text;
            var role = ddlRole.SelectedValue;    // "Patient" or "Admin"
            var fullName = string.IsNullOrWhiteSpace(txtFullName.Text) ? username : txtFullName.Text.Trim();
            var gender = ddlGender.SelectedValue;  // "Male"/"Female"
            var condition = string.IsNullOrWhiteSpace(txtCondition.Text) ? "Unknown" : txtCondition.Text.Trim();

            int age = 0;
            int.TryParse(txtAge.Text, out age);

            // 1) Create the membership user
            MembershipCreateStatus status;
            MembershipUser user = null;

            try
            {
                user = Membership.CreateUser(username, password, username, null, null, true, out status);
            }
            catch (MembershipCreateUserException ex)
            {
                status = ex.StatusCode;
            }

            if (status != MembershipCreateStatus.Success || user == null)
            {
                lblMessage.Text = "Registration failed: " + status;
                return;
            }

            // 2) Ensure role exists and assign it
            try
            {
                if (!Roles.RoleExists(role)) Roles.CreateRole(role);
                if (!Roles.IsUserInRole(username, role)) Roles.AddUserToRole(username, role);
            }
            catch (Exception ex)
            {
                lblMessage.Text = "User created, but role assignment failed: " + ex.Message;
                return;
            }

            // 3) UPSERT PatientProfile (we insert even for Admins if you want their profile row; 
            //    if you want ONLY patients to have a profile, wrap the Upsert call in: if (role == "Patient") { ... }
            try
            {
                Guid userId = (Guid)user.ProviderUserKey;
                UpsertPatientProfile(userId, fullName, gender, age, condition);
            }
            catch (Exception ex)
            {
                lblMessage.Text = "User created, but saving PatientProfile failed: " + ex.Message;
                return;
            }

            lblStatus.Text = "Registered successfully.";
            lblMessage.Text = "";
          
        }

        private void UpsertPatientProfile(Guid userId, string fullName, string gender, int age, string condition)
        {
            using (var conn = new SqlConnection(connStr))
            using (var cmd = new SqlCommand(@"
                IF NOT EXISTS (SELECT 1 FROM dbo.PatientProfile WHERE UserId = @UserId)
                BEGIN
                    INSERT INTO dbo.PatientProfile (UserId, FullName, Gender, Age, [Condition])
                    VALUES (@UserId, @FullName, @Gender, @Age, @Condition);
                END
                ELSE
                BEGIN
                    UPDATE dbo.PatientProfile
                    SET FullName = @FullName,
                        Gender   = @Gender,
                        Age      = @Age,
                        [Condition] = @Condition
                    WHERE UserId = @UserId;
                END
            ", conn))
            {
                cmd.Parameters.Add("@UserId", SqlDbType.UniqueIdentifier).Value = userId;
                cmd.Parameters.Add("@FullName", SqlDbType.NVarChar, 150).Value = (object)fullName ?? DBNull.Value;
                cmd.Parameters.Add("@Gender", SqlDbType.NVarChar, 10).Value = (object)gender ?? DBNull.Value;
                cmd.Parameters.Add("@Age", SqlDbType.Int).Value = age;
                cmd.Parameters.Add("@Condition", SqlDbType.NVarChar, 200).Value = condition; // NOT NULL in your DB

                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }
    }
}
