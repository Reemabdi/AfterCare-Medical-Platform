using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Web.Security;
using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.text.html.simpleparser;
using System.IO;
using iTextSharp.text;
using iTextSharp.text.html.simpleparser;
using iTextSharp.text.pdf;
using System.IO;



namespace Final11373.Admin
{
    public partial class InstructionsList : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string connStr = ConfigurationManager.ConnectionStrings["AftercareDBConnectionString"].ConnectionString;
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                SqlCommand cmd = new SqlCommand("SELECT Title, Content, CreatedAt FROM Instructions", conn);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                rptInstructions.DataSource = dt;
                rptInstructions.DataBind();
            }
        }
        protected void btnLogout_Click(object sender, EventArgs e)
        {
            Session.Clear();
            FormsAuthentication.SignOut();
            Response.Redirect("~/Login.aspx");
        }
        protected void btnExportWord_Click(object sender, EventArgs e)
        {
            Response.Clear();
            Response.Buffer = true;
            Response.AddHeader("content-disposition", "attachment;filename=Instructions.doc");
            Response.Charset = "";
            Response.ContentType = "application/vnd.ms-word";

            StringWriter sw = new StringWriter();
            HtmlTextWriter hw = new HtmlTextWriter(sw);

       
            sw.Write("<html><head><style>body{font-family:Arial;} h4{color:#2F4F4F;} p{margin-bottom:10px;}</style></head><body>");

            foreach (RepeaterItem item in rptInstructions.Items)
            {
                Label lblTitle = (Label)item.FindControl("lblTitle");
                Label lblContent = (Label)item.FindControl("lblContent");
                Label lblDate = (Label)item.FindControl("lblDate");

                sw.Write($"<h4>{lblTitle.Text}</h4>");
                sw.Write($"<p>{lblContent.Text}</p>");
                sw.Write($"<p><small>{lblDate.Text}</small></p><hr/>");
            }

            sw.Write("</body></html>");

            Response.Output.Write(sw.ToString());
            Response.Flush();
            Response.End();
        }

        protected void btnExportPDF_Click(object sender, EventArgs e)
        {
            Response.ContentType = "application/pdf";
            Response.AddHeader("content-disposition", "attachment;filename=Instructions.pdf");
            Response.Cache.SetCacheability(HttpCacheability.NoCache);

            StringWriter sw = new StringWriter();
            HtmlTextWriter hw = new HtmlTextWriter(sw);

            rptInstructions.RenderControl(hw);
            StringReader sr = new StringReader(sw.ToString());

            Document pdfDoc = new Document(PageSize.A4, 10f, 10f, 10f, 10f);
            HTMLWorker htmlparser = new HTMLWorker(pdfDoc);
            PdfWriter.GetInstance(pdfDoc, Response.OutputStream);

            pdfDoc.Open();
            htmlparser.Parse(sr);
            pdfDoc.Close();

            Response.Write(pdfDoc);
            Response.End();
        }
 
        protected void btnBack_Click(object sender, EventArgs e)
        {
            Response.Redirect("AdminDashboard.aspx"); 
        }


        public override void VerifyRenderingInServerForm(Control control)
        {
          
        }

    }
}