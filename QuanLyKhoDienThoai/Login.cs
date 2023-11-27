using System;
using System.Data.SqlClient;
using System.Drawing;
using System.Web;
using System.Windows.Forms;

namespace QuanLyKhoDienThoai
{
    public partial class Login : Form
    {
        DBConnection DbConn = new DBConnection();

        public Login()
        {
            InitializeComponent();
        }

        private void chkHienMatKhau_CheckedChanged(object sender, EventArgs e)
        {
            if(chkHienMatKhau.Checked)
            {
                txtMatKhau.UseSystemPasswordChar = false;
            }
            else
            {
                txtMatKhau.UseSystemPasswordChar=true;
            }
        }

        private void Login_FormClosing(object sender, FormClosingEventArgs e)
        {
            DialogResult result;
            result = MessageBox.Show("Bạn thật sự muốn thoát?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if(result != DialogResult.Yes)
            {
                e.Cancel = true;
            }
        }

        private void lbQuenMatKhau_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form resetPass = new ResetPassword();
            resetPass.ShowDialog();
            this.Show();
        }

        private void btnDangNhap_Click(object sender, EventArgs e)
        {
            try
            {
                string taiKhoan = txtTaiKhoan.Text.Trim();
                string matKhau = txtMatKhau.Text.Trim();

                DbConn.GetConn();

                string query = $"SELECT * FROM tbl_Accounts WHERE taikhoan = '{taiKhoan}' AND matkhau = '{matKhau}'";
                SqlDataReader reader = DbConn.Reader(query);
                if (reader.HasRows)
                {
                    string tenNV = "";
                    string chucVu = "";
                    if (reader.Read())
                    {
                        tenNV = reader.GetString(1);
                        chucVu = reader.GetString(2);
                    }
                    reader.Close();
                    DbConn.CloseConn();

                    MessageBox.Show("Đăng nhập thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtMatKhau.Text = "";
                    this.Hide();
                    Form home = new Home(tenNV, chucVu);
                    home.ShowDialog();
                    this.Show();
                }
                else
                {
                    DbConn.CloseConn();
                    MessageBox.Show("Tài khoản/Mật khẩu không đúng!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtMatKhau.Text = "";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: "+ex.ToString(),"Thông báo",MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
