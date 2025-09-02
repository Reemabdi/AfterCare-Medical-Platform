<%@ Page Language="C#" AutoEventWireup="true" %>
<%@ Import Namespace="System.Data.SqlClient" %>
<script runat="server">
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            using (SqlConnection conn = new SqlConnection("Server=sql1004.site4now.net;Database=db_abbbca_term3april302023;User Id=db_abbbca_term3april302023_admin;Password=Aa123456;"))
            {
                conn.Open();
                Response.Write("✅ Database Connected Successfully");
            }
        }
        catch (Exception ex)
        {
            Response.Write("❌ DB Error: " + ex.Message);
        }
    }
</script>
