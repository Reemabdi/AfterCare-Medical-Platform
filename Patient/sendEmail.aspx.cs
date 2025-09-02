using System;
using Final11373;   // ✅ mailMgr lives in namespace Final11373 (not Final11373.Services)

namespace Final11373.Patient
{
    public partial class sendEmail : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack) return;

            // Pre-fill From with session email if available
            var sessionEmail = (Session["Email"] as string)?.Trim();
            if (!string.IsNullOrEmpty(sessionEmail))
                txtSenderEmail.Text = sessionEmail;
        }

        protected void btnSend_Click(object sender, EventArgs e)
        {
            lblMsg.Text = "";

            if (string.IsNullOrWhiteSpace(txtSenderEmail.Text) ||
                string.IsNullOrWhiteSpace(txtSubject.Text) ||
                string.IsNullOrWhiteSpace(txtBody.Text))
            {
                lblMsg.Text = "Please fill all required fields.";
                lblMsg.ForeColor = System.Drawing.Color.Red;
                return;
            }

            using (var mailer = new mailMgr())
            {
                mailer.mySubject = txtSubject.Text.Trim();
                mailer.myBody = txtBody.Text.Trim();

              
                var result = mailer.sendEmailViaGmail(fuAttachment, txtSenderEmail.Text.Trim());

                lblMsg.Text = result;
                lblMsg.ForeColor = result.IndexOf("success", StringComparison.OrdinalIgnoreCase) >= 0
                    ? System.Drawing.Color.Green
                    : System.Drawing.Color.Red;
            }
        }
    }
}
