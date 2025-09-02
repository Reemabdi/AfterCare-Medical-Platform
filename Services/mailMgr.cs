using System;
using System.Configuration;
using System.IO;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.UI.WebControls;

namespace Final11373   
{
    public class mailMgr : IDisposable
    {
        private readonly SmtpClient _smtp;
        private readonly MailMessage _msg;

        public string mySubject { get; set; }
        public string myBody { get; set; }

        public mailMgr()
        {
            var user = (ConfigurationManager.AppSettings["GmailUser"] ?? "").Trim();
            
            var pass = (ConfigurationManager.AppSettings["GmailAppPassword"] ?? "").Replace(" ", "").Trim();

            _msg = new MailMessage { IsBodyHtml = true };
            _smtp = new SmtpClient("smtp.gmail.com", 587)
            {
                EnableSsl = true,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(user, pass),
                DeliveryMethod = SmtpDeliveryMethod.Network
            };
        }


        // 1) Existing single-arg version (kept)
        public string sendEmailViaGmail(FileUpload fuAttachment = null)
        {
            try
            {
                var from = ConfigurationManager.AppSettings["GmailUser"];
                var to = ConfigurationManager.AppSettings["Mail.Doctor"];
                if (string.IsNullOrWhiteSpace(to)) to = from;

                _msg.From = new MailAddress(from);
                _msg.To.Clear();
                _msg.To.Add(new MailAddress(to));
                _msg.Subject = mySubject ?? "";
                _msg.Body = myBody ?? "";

                AttachFiles(fuAttachment);

                _smtp.Send(_msg);
                return "Email sent successfully.";
            }
            catch (Exception ex)
            {
                return "Failed to send email: " + ex.Message;
            }
        }

        // 2) NEW overload (attachments + Reply-To)
        public string sendEmailViaGmail(FileUpload fuAttachment, string replyToEmail)
        {
            try
            {
                var from = ConfigurationManager.AppSettings["GmailUser"];
                var to = ConfigurationManager.AppSettings["Mail.Doctor"];
                if (string.IsNullOrWhiteSpace(to)) to = from;

                _msg.From = new MailAddress(from);
                _msg.To.Clear();
                _msg.To.Add(new MailAddress(to));

                if (!string.IsNullOrWhiteSpace(replyToEmail))
                    _msg.ReplyToList.Add(new MailAddress(replyToEmail));

                _msg.Subject = mySubject ?? "";
                _msg.Body = myBody ?? "";

                AttachFiles(fuAttachment);

                _smtp.Send(_msg);
                return "Email sent successfully.";
            }
            catch (Exception ex)
            {
                return "Failed to send email: " + ex.Message;
            }
        }

        // helper for attachments
        private void AttachFiles(FileUpload fuAttachment)
        {
            if (fuAttachment == null) return;

            if (fuAttachment.PostedFiles != null && fuAttachment.PostedFiles.Count > 0)
            {
                for (int i = 0; i < fuAttachment.PostedFiles.Count; i++)
                {
                    HttpPostedFile f = fuAttachment.PostedFiles[i];
                    if (f != null && f.ContentLength > 0)
                        _msg.Attachments.Add(new Attachment(f.InputStream, Path.GetFileName(f.FileName)));
                }
            }
            else if (fuAttachment.HasFile)
            {
                _msg.Attachments.Add(new Attachment(fuAttachment.FileContent, Path.GetFileName(fuAttachment.FileName)));
            }
        }

        public void Dispose()
        {
            _msg.Dispose();
            _smtp.Dispose();
        }
    }
}
