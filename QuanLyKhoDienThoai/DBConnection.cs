using System;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
namespace QuanLyKhoDienThoai
{
    public class DBConnection
    {
        private string StrConn = ConfigurationManager.ConnectionStrings["StrConn"].ConnectionString;
        private SqlConnection conn = null;

        public SqlConnection GetConn()
        {
            if(conn == null)
            {
                conn = new SqlConnection(StrConn);
            }
            if(conn != null && conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }
            return conn;
        }
        
        public SqlConnection CloseConn()
        {
            if(conn != null && conn.State == ConnectionState.Open)
            {
                conn.Close();
            }
            return conn;
        }

        public int Command(string query)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = query;
            cmd.Connection = conn;

            int check = cmd.ExecuteNonQuery();
            return check;
        }

        public SqlDataReader Reader(string query)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = query;
            cmd.Connection = conn;

            return cmd.ExecuteReader();
        }

        public DataTable DataTable(string query)
        {
            SqlDataAdapter dataAdapter = new SqlDataAdapter(query,conn);
            DataTable data = new DataTable();
            dataAdapter.Fill(data);
            return data;
        }
    }
}
