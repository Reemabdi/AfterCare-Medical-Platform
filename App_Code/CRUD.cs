using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace Final11373
{
    public class CRUD
    {
        private string connStr;

        // Constructor افتراضي: يقرأ connection string من web.config
        public CRUD()
        {
            connStr = ConfigurationManager.ConnectionStrings["AftercareDBConnectionString"].ConnectionString;
        }

        // 🔹 Execute Insert, Update, Delete
        public int ExecuteNonQuery(string sql, Dictionary<string, object> parameters)
        {
            int rows = 0;
            using (SqlConnection conn = new SqlConnection(connStr))
            using (SqlCommand cmd = new SqlCommand(sql, conn))
            {
                if (parameters != null)
                {
                    foreach (KeyValuePair<string, object> p in parameters)
                    {
                        cmd.Parameters.AddWithValue(p.Key, p.Value ?? DBNull.Value);
                    }
                }

                conn.Open();
                rows = cmd.ExecuteNonQuery();
            }
            return rows;
        }

        // 🔹 Execute Scalar (يرجع قيمة واحدة)
        public object ExecuteScalar(string sql, Dictionary<string, object> parameters)
        {
            object result = null;
            using (SqlConnection conn = new SqlConnection(connStr))
            using (SqlCommand cmd = new SqlCommand(sql, conn))
            {
                if (parameters != null)
                {
                    foreach (KeyValuePair<string, object> p in parameters)
                    {
                        cmd.Parameters.AddWithValue(p.Key, p.Value ?? DBNull.Value);
                    }
                }

                conn.Open();
                result = cmd.ExecuteScalar();
            }
            return result;
        }

        // 🔹 Get DataTable
        public DataTable getDT(string sql, Dictionary<string, object> parameters)
        {
            DataTable dt = new DataTable();
            using (SqlConnection conn = new SqlConnection(connStr))
            using (SqlCommand cmd = new SqlCommand(sql, conn))
            {
                if (parameters != null)
                {
                    foreach (KeyValuePair<string, object> p in parameters)
                    {
                        cmd.Parameters.AddWithValue(p.Key, p.Value ?? DBNull.Value);
                    }
                }

                conn.Open();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
            }
            return dt;
        }

        // 🔹 Get DataReader
        public SqlDataReader getDrPassSql(string sql, Dictionary<string, object> parameters)
        {
            SqlConnection conn = new SqlConnection(connStr);
            SqlCommand cmd = new SqlCommand(sql, conn);

            if (parameters != null)
            {
                foreach (KeyValuePair<string, object> p in parameters)
                {
                    cmd.Parameters.AddWithValue(p.Key, p.Value ?? DBNull.Value);
                }
            }

            conn.Open();
            return cmd.ExecuteReader(CommandBehavior.CloseConnection);
        }
    }
}
