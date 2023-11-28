using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QuanLyKhoDienThoai
{
    internal class DocSL
    {
        DBConnection DbConn = new DBConnection();
        public int SLSP(string MaSP)
        {
            int slsp = 0;
            DbConn.GetConn();
            string query = $"SELECT * FROM tbl_Products WHERE masanpham = '{MaSP}'";
            SqlDataReader reader = DbConn.Reader(query);
            if(reader.HasRows )
            {
                if(reader.Read())
                {
                    slsp = Convert.ToInt32(reader["soluong"].ToString());
                }
                reader.Close();
                return slsp;
            }
            else
            {
                return -1;
            }
        }

        public int SLSPDN(string MaDN)
        {
            int slsp = 0;
            DbConn.GetConn();
            string query = $"SELECT * FROM tbl_NhapHang WHERE madonhang = '{MaDN}'";
            SqlDataReader reader = DbConn.Reader(query);
            if (reader.HasRows)
            {
                if (reader.Read())
                {
                    slsp = Convert.ToInt32(reader["soluongsanpham"].ToString());
                }
                reader.Close();
                return slsp;
            }
            else
            {
                return -1;
            }
        }

        public string SLDX()
        {
            int sldx = 0;
            try
            {
                DbConn.GetConn();
                string query = "SELECT trangthai FROM tbl_ThongKe";
                SqlDataReader reader = DbConn.Reader(query);
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        String trangthai = reader["trangthai"].ToString();
                        if (trangthai == "Xuất Hàng")
                        {
                            sldx++;
                        }
                    }
                    reader.Close();
                }
                reader.Close();
                DbConn.CloseConn();
                return sldx.ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.ToString(), "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return "Không đếm được đơn xuất!";
            }
        }

        public string SLDN()
        {
            int sldn = 0;
            try
            {
                DbConn.GetConn();
                string query = "SELECT trangthai FROM tbl_ThongKe";
                SqlDataReader reader = DbConn.Reader(query);
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        String trangthai = reader["trangthai"].ToString();
                        if (trangthai == "Nhập Hàng")
                        {
                            sldn++;
                        }
                    }
                    reader.Close();
                }
                reader.Close();
                DbConn.CloseConn();
                return sldn.ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.ToString(), "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return "Không đếm được đơn nhập!";
            }
        }
    }
}
