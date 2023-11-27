using System;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;

namespace QuanLyKhoDienThoai
{
    public partial class ResetPassword : Form
    {
        DBConnection Dbconn = new DBConnection();

        public ResetPassword()
        {
            InitializeComponent();
        }

        private void btnLayMatKhau_Click(object sender, EventArgs e)
        {
            try
            {
                string email = txtEmail.Text.Trim();

                Dbconn.GetConn();

                string query = $"SELECT * FROM tbl_Accounts WHERE email = '{email}'";
                SqlDataReader reader = Dbconn.Reader(query);
                if (reader.HasRows)
                {
                    string matKhau = "";
                    if (reader.Read())
                    {
                        matKhau = reader.GetString(5);
                    }
                    reader.Close();
                    Dbconn.CloseConn();

                    MessageBox.Show($"Mật khẩu là: {matKhau} .Mời quay lại trang đăng nhập!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.Close();
                }
                else
                {
                    Dbconn.CloseConn();
                    MessageBox.Show("Email không tồn tại!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.ToString(), "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
