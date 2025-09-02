using System;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Text; // for Encoding.UTF8
using System.Web;
using System.Web.Configuration;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Drawing;
using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Spreadsheet;
using WebUIControl = System.Web.UI.Control;
using WebColor = System.Drawing.Color;
using System.IO.Compression;   // ZipArchive, CompressionLevel


namespace Final11373.Admin
{
    public partial class ManageInstructions : System.Web.UI.Page
    {
        private readonly string connStr =
            WebConfigurationManager.ConnectionStrings["AftercareDBConnectionString"].ConnectionString;

        protected void Page_Load(object sender, EventArgs e)
        {
            var role = Session["Role"] as string;
            if (!"Admin".Equals(role, StringComparison.OrdinalIgnoreCase))
            {
                Response.Redirect("~/Home.aspx", false);
                Context.ApplicationInstance.CompleteRequest();
                return;
            }

            if (!Page.IsPostBack)
            {
                LoadPatients();
                getData();
            }
        }

        // needed for RenderControl export (harmless for CSV path)
        // was: public override void VerifyRenderingInServerForm(Control control) { }
        public override void VerifyRenderingInServerForm(WebUIControl control) { }


        private void LoadPatients()
        {
            using (var cn = new SqlConnection(connStr))
            using (var da = new SqlDataAdapter(
                "SELECT UserId, LOWER(Email) AS Email FROM dbo.aspnet_Membership ORDER BY Email", cn))
            {
                var dt = new DataTable();
                da.Fill(dt);
                ddlPatients.DataSource = dt;
                ddlPatients.DataTextField = "Email";
                ddlPatients.DataValueField = "UserId";
                ddlPatients.DataBind();
            }
            ddlPatients.Items.Insert(0, new ListItem("-- All Patients --", ""));
        }

        protected void ddlPatients_SelectedIndexChanged(object sender, EventArgs e) { getData(); }
        protected void btnGetData_Click(object sender, EventArgs e) { getData(); }

        // ===== CRUD =====

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(ddlPatients.SelectedValue))
            { lblMessage.Text = "Select a patient."; lblMessage.ForeColor = WebColor.Firebrick; return; }

            using (SqlConnection cn = new SqlConnection(connStr))
            using (SqlCommand cmd = new SqlCommand(@"
INSERT INTO dbo.Instructions (Title, Content, CategoryID, UserId, CreatedBy, CreatedAt)
VALUES (@t,@c,@cat,@uid,@by,GETDATE());", cn))
            {
                cmd.Parameters.AddWithValue("@t", txtTitle.Text.Trim());
                cmd.Parameters.AddWithValue("@c", txtContent.Text.Trim());
                cmd.Parameters.AddWithValue("@cat", DBNull.Value);
                cmd.Parameters.AddWithValue("@uid", new Guid(ddlPatients.SelectedValue));
                cmd.Parameters.AddWithValue("@by", "admin");
                cn.Open();
                cmd.ExecuteNonQuery();
            }

            lblMessage.Text = "Successful Ops";
            lblMessage.ForeColor = WebColor.ForestGreen;
            ClearForm();
            getData();
        }

        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            int id;
            if (!int.TryParse(hfInstructionID.Value, out id))
            { lblMessage.Text = "Nothing selected."; lblMessage.ForeColor = WebColor.Firebrick; return; }

            using (SqlConnection cn = new SqlConnection(connStr))
            using (SqlCommand cmd = new SqlCommand(@"
UPDATE dbo.Instructions SET Title=@t, Content=@c WHERE InstructionID=@id;", cn))
            {
                cmd.Parameters.AddWithValue("@t", txtTitle.Text.Trim());
                cmd.Parameters.AddWithValue("@c", txtContent.Text.Trim());
                cmd.Parameters.AddWithValue("@id", id);
                cn.Open();
                cmd.ExecuteNonQuery();
            }

            lblMessage.Text = "Successful Ops";
            lblMessage.ForeColor = WebColor.ForestGreen;
            ClearForm();
            getData();
        }

        protected void btnDelete_Click(object sender, EventArgs e)
        {
            int id;
            if (!int.TryParse(hfInstructionID.Value, out id))
            { lblMessage.Text = "Nothing selected."; lblMessage.ForeColor = WebColor.Firebrick; }

                using (SqlConnection cn = new SqlConnection(connStr))
            using (SqlCommand cmd = new SqlCommand("DELETE FROM dbo.Instructions WHERE InstructionID=@id;", cn))
            {
                cmd.Parameters.AddWithValue("@id", id);
                cn.Open();
                cmd.ExecuteNonQuery();
            }

            lblMessage.Text = "Successful Ops";
            lblMessage.ForeColor = WebColor.ForestGreen;
            ClearForm();
            getData();
        }

        protected void btnClear_Click(object sender, EventArgs e)
        {
            ClearForm();
            lblMessage.Text = "";
        }

        private void ClearForm()
        {
            txtTitle.Text = "";
            txtContent.Text = "";
            hfInstructionID.Value = "";
            btnSubmit.Visible = true;
            btnUpdate.Visible = false;
            btnDelete.Visible = false;
        }

        // ===== Data bind =====

        protected void getData()
        {
            Guid uid; bool hasPatient = Guid.TryParse(ddlPatients.SelectedValue, out uid);

            string sql = @"
SELECT  i.InstructionID,
        i.Title,
        i.Content,
        i.CreatedBy,
        i.CreatedAt,
        ISNULL(c.CategoryName,'Uncategorized') AS CategoryName
FROM    dbo.Instructions i
LEFT    JOIN dbo.Categories c ON c.CategoryID = i.CategoryID
WHERE   (@has = 0 OR i.UserId = @uid)
ORDER BY i.CreatedAt DESC;";

            using (SqlConnection cn = new SqlConnection(connStr))
            using (SqlDataAdapter da = new SqlDataAdapter(sql, cn))
            {
                da.SelectCommand.Parameters.AddWithValue("@has", hasPatient ? 1 : 0);
                da.SelectCommand.Parameters.AddWithValue("@uid", hasPatient ? (object)uid : DBNull.Value);

                DataTable dt = new DataTable();
                da.Fill(dt);
                gvInstructions.DataSource = dt;
                gvInstructions.DataBind();
            }
        }

        protected void gvInstructions_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "EditInstruction")
            {
                int id;
                if (int.TryParse(e.CommandArgument.ToString(), out id))
                    populateForm(id);
            }
        }

        private void populateForm(int id)
        {
            string sql = "SELECT InstructionID, Title, Content, UserId FROM dbo.Instructions WHERE InstructionID=@id;";
            using (SqlConnection cn = new SqlConnection(connStr))
            using (SqlCommand cmd = new SqlCommand(sql, cn))
            {
                cmd.Parameters.AddWithValue("@id", id);
                cn.Open();
                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    if (dr.Read())
                    {
                        hfInstructionID.Value = dr["InstructionID"].ToString();
                        txtTitle.Text = dr["Title"].ToString();
                        txtContent.Text = dr["Content"].ToString();

                        Guid uid;
                        if (Guid.TryParse(Convert.ToString(dr["UserId"]), out uid))
                        {
                            var item = ddlPatients.Items.FindByValue(uid.ToString());
                            if (item != null) ddlPatients.SelectedValue = uid.ToString();
                        }

                        btnSubmit.Visible = false;
                        btnUpdate.Visible = true;
                        btnDelete.Visible = true;
                    }
                }
            }
        }

        // ===== EXPORT: CSV (stable with Arabic, avoids AV blocks) =====
        protected void btnExportTopExcel_Click(object sender, EventArgs e)
        {
            btnExportSafeCsv_Click(sender, e);
        }

        protected void btnExportSafeCsv_Click(object sender, EventArgs e)
        {
            bool oldPaging = gvInstructions.AllowPaging;
            gvInstructions.AllowPaging = false;
            getData();

            ExportGridToCsv(gvInstructions);

            gvInstructions.AllowPaging = oldPaging;
            getData();
        }

        private static void ExportGridToCsv(GridView grid)
        {
            DataTable dt = GridToDataTable(grid);

            var resp = HttpContext.Current.Response;
            resp.Clear();
            resp.Buffer = true;
            resp.Charset = "utf-8";
            resp.ContentEncoding = Encoding.UTF8;
            resp.Cache.SetCacheability(HttpCacheability.NoCache);
            resp.ContentType = "text/csv";
            string file = "ExportedReport_" + DateTime.Now.ToString("M_d_yyyy h_mm_ss tt") + ".csv";
            resp.AddHeader("Content-Disposition", "attachment; filename=" + file);

    
            resp.Write('\uFEFF');

            using (var sw = new StringWriter())
            {
                // headers
                for (int i = 0; i < dt.Columns.Count; i++)
                {
                    if (i > 0) sw.Write(",");
                    sw.Write(EscapeCsv(dt.Columns[i].ColumnName));
                }
                sw.Write("\r\n");

                // rows
                foreach (DataRow row in dt.Rows)
                {
                    for (int i = 0; i < dt.Columns.Count; i++)
                    {
                        if (i > 0) sw.Write(",");
                        sw.Write(EscapeCsv(row[i] == null ? "" : row[i].ToString()));
                    }
                    sw.Write("\r\n");
                }

                resp.Write(sw.ToString());
                resp.End();
            }
        }

        private static string EscapeCsv(string s)
        {
            if (s == null) return "";
            bool needsQuotes = s.Contains(",") || s.Contains("\"") || s.Contains("\r") || s.Contains("\n");
            s = s.Replace("\"", "\"\"");
            return needsQuotes ? "\"" + s + "\"" : s;
        }


        private static Cell CreateTextCell(string text)
        {
            return new Cell
            {
                DataType = CellValues.InlineString,
                InlineString = new InlineString(new Text(text ?? string.Empty))
            };
        }

        private static DataTable GridToDataTable(GridView gv)
        {
            DataTable dt = new DataTable();

            // headers
            if (gv.HeaderRow != null)
            {
                foreach (TableCell cell in gv.HeaderRow.Cells)
                {
                    string header = ExtractCellText(cell);
                    if (string.IsNullOrEmpty(header)) header = "Column" + dt.Columns.Count.ToString();
                    dt.Columns.Add(header);
                }
            }
            else
            {
                int colCount = gv.Rows.Count > 0 ? gv.Rows[0].Cells.Count : 0;
                for (int i = 0; i < colCount; i++) dt.Columns.Add("Column" + (i + 1).ToString());
            }

            // rows
            foreach (GridViewRow row in gv.Rows)
            {
                DataRow dr = dt.NewRow();
                for (int i = 0; i < row.Cells.Count; i++)
                    dr[i] = ExtractCellText(row.Cells[i]);
                dt.Rows.Add(dr);
            }

            return dt;
        }

        private static string ExtractCellText(TableCell cell)
        {
            if (cell.Controls.Count == 0)
                return Clean(cell.Text);

            foreach (WebUIControl c in cell.Controls)
            {
                if (c is LinkButton) return Clean(((LinkButton)c).Text);
                if (c is Button) return Clean(((Button)c).Text);
                if (c is Label) return Clean(((Label)c).Text);
                if (c is LiteralControl) return Clean(((LiteralControl)c).Text);
                if (c is CheckBox) return ((CheckBox)c).Checked ? "True" : "False";
            }
            return Clean(cell.Text);
        }

        private static string Clean(string s)
        {
            return (s ?? string.Empty).Replace("&nbsp;", " ").Trim();
        }

        protected void btnLogout_Click(object sender, EventArgs e)
        {
            Session.Clear(); Session.Abandon(); FormsAuthentication.SignOut();
            Response.Redirect("~/Home.aspx");
        }
    }
}
