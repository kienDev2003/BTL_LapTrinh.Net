using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;

namespace QuanLyKhoDienThoai
{
    public partial class NhanVien : Form
    {
        DBConnection DbConn = new DBConnection();

        public NhanVien()
        {
            InitializeComponent();
        }

        private void LoadList()
        {
            try
            {
                lsvDanhSach.Items.Clear();

                DbConn.GetConn();

                string query = @"SELECT * FROM tbl_Accounts";
                SqlDataReader reader = DbConn.Reader(query);
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        string MaNV = reader.GetString(0);
                        string TenNV = reader.GetString(1);
                        string ChucVu = reader.GetString(2);
                        string TaiKhoan = reader.GetString(4);
                        string MatKhau = reader.GetString(5);
                        string Email = reader.GetString(3);
                        string NgayCapNhat = reader.GetString(6);

                        ListViewItem lvi = new ListViewItem(MaNV);
                        lvi.SubItems.Add(TenNV);
                        lvi.SubItems.Add(ChucVu);
                        lvi.SubItems.Add(TaiKhoan);
                        lvi.SubItems.Add(MatKhau);
                        lvi.SubItems.Add(Email);
                        lvi.SubItems.Add(NgayCapNhat);

                        lsvDanhSach.Items.Add(lvi);
                    }
                    reader.Close();
                    DbConn.CloseConn();
                }
                else
                {
                    DbConn.CloseConn();
                    MessageBox.Show("Danh sách nhân viên không có dữ liệu!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.ToString(), "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private bool KiemTraTrungMa(string MaNV)
        {
            DbConn.GetConn();
            string query = $"SELECT * FROM tbl_Accounts WHERE manhanvien = N'{MaNV}'";
            SqlDataReader reader = DbConn.Reader(query);
            if (reader.HasRows)
            {
                reader.Close();
                DbConn.CloseConn();
                return true;
            }
            DbConn.CloseConn();
            return false;
        }

        private bool KiemTraTrungTaiKhoan(string TaiKhoan)
        {
            DbConn.GetConn();
            string query = $"SELECT * FROM tbl_Accounts WHERE taikhoan = N'{TaiKhoan}'";
            SqlDataReader reader = DbConn.Reader(query);
            if (reader.HasRows)
            {
                reader.Close();
                DbConn.CloseConn();
                return true;
            }
            DbConn.CloseConn();
            return false;
        }

        private bool KiemTraTrungThayDoiTaiKhoan(string TaiKhoan)
        {
            string TaiKhoanMoi = txtTaiKhoan.Text;
            string TaiKhoanCu = "";
            DbConn.GetConn();
            string query = $"SELECT * FROM tbl_Accounts WHERE taikhoan = N'{TaiKhoan}'";
            SqlDataReader reader = DbConn.Reader(query);
            if (reader.HasRows)
            {
                if (reader.Read())
                {
                    TaiKhoanCu = reader.GetString(4);
                }
                reader.Close();
                DbConn.CloseConn();
                if(TaiKhoanCu == TaiKhoanMoi) return true;
                return false;
            }
            DbConn.CloseConn();
            return false;
        }

        private void ResetTextBox()
        {
            txtMaNV.Text = "";
            txtTenNV.Text = "";
            txtChucVu.Text = "";
            txtEmail.Text = "";
            txtTaiKhoan.Text = "";
            txtMatKhau.Text = "";
        }

        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            try
            {
                lsvDanhSach.Items.Clear();

                string maNV = txtTkMaNV.Text.Trim();
                string tenNV = txtTkTenNV.Text.Trim();
                if (maNV.Trim() == "" && tenNV.Trim() == "")
                {
                    MessageBox.Show("Mã NV hoặc Tên NV không được để trống!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadList();
                    return;
                }
                if (maNV.Trim() != "" && tenNV.Trim() != "")
                {
                    MessageBox.Show("Vui lòng tìm theo Mã NV hoặc Tên NV !", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadList();
                    return;
                }

                if (maNV != "")
                {
                    DbConn.GetConn();

                    string query = $"SELECT * FROM tbl_Accounts WHERE manhanvien = N'{maNV}'";
                    SqlDataReader reader = DbConn.Reader(query);
                    if (reader.HasRows)
                    {
                        if (reader.Read())
                        {
                            string MaNV = reader.GetString(0);
                            string TenNV = reader.GetString(1);
                            string ChucVu = reader.GetString(2);
                            string TaiKhoan = reader.GetString(4);
                            string MatKhau = reader.GetString(5);
                            string Email = reader.GetString(3);
                            string NgayCapNhat = reader.GetString(6);

                            ListViewItem lvi = new ListViewItem(MaNV);
                            lvi.SubItems.Add(TenNV);
                            lvi.SubItems.Add(ChucVu);
                            lvi.SubItems.Add(TaiKhoan);
                            lvi.SubItems.Add(MatKhau);
                            lvi.SubItems.Add(Email);
                            lvi.SubItems.Add(NgayCapNhat);

                            lsvDanhSach.Items.Add(lvi);
                            txtTkMaNV.Text = "";
                        }
                        reader.Close();
                        DbConn.CloseConn();
                    }
                    else
                    {
                        reader.Close();
                        DbConn.CloseConn();
                        MessageBox.Show($"Không tìm thấy nhân viên có mã {maNV} !", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        txtTkMaNV.Text = "";
                        LoadList();
                    }
                }
                else
                {
                    DbConn.GetConn();

                    string query = $"SELECT * FROM tbl_Accounts WHERE tennhanvien LIKE N'%{tenNV}%'";

                    SqlDataReader reader = DbConn.Reader(query);
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            string MaNV = reader.GetString(0);
                            string TenNV = reader.GetString(1);
                            string ChucVu = reader.GetString(2);
                            string TaiKhoan = reader.GetString(3);
                            string MatKhau = reader.GetString(4);
                            string Email = reader.GetString(5);
                            string NgayCapNhat = reader.GetString(6);

                            ListViewItem lvi = new ListViewItem(MaNV);
                            lvi.SubItems.Add(TenNV);
                            lvi.SubItems.Add(ChucVu);
                            lvi.SubItems.Add(TaiKhoan);
                            lvi.SubItems.Add(MatKhau);
                            lvi.SubItems.Add(Email);
                            lvi.SubItems.Add(NgayCapNhat);

                            lsvDanhSach.Items.Add(lvi);
                        }
                        reader.Close();
                        DbConn.CloseConn();
                        txtTkTenNV.Text = "";
                    }
                    else
                    {
                        reader.Close();
                        DbConn.CloseConn();
                        MessageBox.Show("Không tìm thấy nhân viên!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        LoadList();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.ToString(), "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void lsvDanhSach_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lsvDanhSach.SelectedItems.Count > 0)
            {
                ListViewItem lvi = lsvDanhSach.SelectedItems[0];

                string MaNV = lvi.SubItems[0].Text;
                string TenNV = lvi.SubItems[1].Text;
                string ChucVu = lvi.SubItems[2].Text;
                string Email = lvi.SubItems[5].Text;
                string TaiKhoan = lvi.SubItems[3].Text;
                string MatKhau = lvi.SubItems[4].Text;

                txtMaNV.Text = MaNV;
                txtTenNV.Text = TenNV;
                txtChucVu.Text = ChucVu;
                txtEmail.Text = Email;
                txtTaiKhoan.Text = TaiKhoan;
                txtMatKhau.Text = MatKhau;
            }
        }

        private void NhanVien_Load(object sender, EventArgs e)
        {
            LoadList();
            Home homeForm = Application.OpenForms["Home"] as Home;
            if (homeForm != null && homeForm.lb_ChucVu.Text != "Quản Lý")
            {
                btnThem.Enabled = false;
                btnSua.Enabled = false;
                btnXoa.Enabled = false;
            }
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            try
            {
                string MaNV = txtMaNV.Text;
                string TenNV = txtTenNV.Text;
                string ChucVu = txtChucVu.Text;
                string TaiKhoan = txtTaiKhoan.Text;
                string MatKhau = txtMatKhau.Text;
                string Email = txtEmail.Text;
                string NgayCapNhat = DateTime.Now.ToString("hh:mm-dd/MM/yy");

                if (MaNV == "" || TenNV == "" || ChucVu == "" || Email == "" || TaiKhoan == "" || MatKhau == "")
                {
                    MessageBox.Show("Vui lòng nhập đầy đủ thông tin!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                if (KiemTraTrungMa(MaNV) == true)
                {
                    MessageBox.Show("Mã nhân viên bị trùng!", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                if (KiemTraTrungTaiKhoan(TaiKhoan) == true)
                {
                    MessageBox.Show("Tài khoản bị trùng!", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                DbConn.GetConn();
                string query = $"INSERT INTO tbl_Accounts (manhanvien,tennhanvien,chucvu,email,ngaycapnhat,taikhoan,matkhau)" +
                               $" VALUES (N'{MaNV}',N'{TenNV}',N'{ChucVu}',N'{Email}',N'{NgayCapNhat}',N'{TaiKhoan}',N'{MatKhau}')";
                int check = DbConn.Command(query);
                if (check > 0)
                {
                    MessageBox.Show("Thêm nhân viên thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    ResetTextBox();
                    LoadList();
                    DbConn.CloseConn();
                }
                else
                {
                    MessageBox.Show("Thêm nhân viên KHÔNG thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    LoadList();
                    DbConn.CloseConn();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.ToString(), "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            string MaNV = txtMaNV.Text;
            string TenNV = txtTenNV.Text;
            string ChucVu = txtChucVu.Text;
            string TaiKhoan = txtTaiKhoan.Text;
            string MatKhau = txtMatKhau.Text;
            string Email = txtEmail.Text;
            string NgayCapNhat = DateTime.Now.ToString("hh:mm-dd/MM/yy");
            try
            {
                if (MaNV == "" || TenNV == "" || ChucVu == "" || Email == "" || TaiKhoan == "" || MatKhau == "")
                {
                    MessageBox.Show("Vui lòng nhập đầy đủ thông tin!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                if (KiemTraTrungMa(MaNV) == true)
                {
                    DialogResult result = MessageBox.Show($"Bạn thật sự muốn sửa thông tin của NCC có mã {MaNV}", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (result == DialogResult.Yes)
                    {
                        if (KiemTraTrungThayDoiTaiKhoan(TaiKhoan) == false)
                        {
                            MessageBox.Show("Không được thay đổi tài khoản!", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            return;
                        }
                        DbConn.GetConn();
                        string query = $"UPDATE tbl_Accounts SET tennhanvien = N'{TenNV}', chucvu = N'{ChucVu}'," +
                                       $" email = N'{Email}', ngaycapnhat = N'{NgayCapNhat}', matkhau = N'{MatKhau}' WHERE manhanvien = N'{MaNV}'";

                        int check = DbConn.Command(query);
                        if (check > 0)
                        {
                            MessageBox.Show("Sửa thông tin thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            ResetTextBox();
                            LoadList();
                            DbConn.CloseConn();
                        }
                        else
                        {
                            MessageBox.Show("Sửa thông tin KHÔNG thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            LoadList();
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Mã nhân viên không tồn tại!", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadList();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.ToString(), "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            ResetTextBox();
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            string MaNV = txtMaNV.Text.Trim();

            try
            {
                if (MaNV == "")
                {
                    MessageBox.Show("Mã nhân viên không được để trống!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                if (KiemTraTrungMa(MaNV) == true)
                {
                    DialogResult result = MessageBox.Show($"Bạn thật sự muốn xóa nhân viên có mã {MaNV}", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (result == DialogResult.Yes)
                    {
                        DbConn.GetConn();
                        string query = $"DELETE FROM tbl_Accounts WHERE manhanvien = N'{MaNV}'";
                        int check = DbConn.Command(query);
                        if (check > 0)
                        {
                            MessageBox.Show("Xóa nhân viên thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            LoadList();
                            ResetTextBox();
                            DbConn.CloseConn();
                        }
                        else
                        {
                            MessageBox.Show("Xóa nhân viên KHÔNG thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            LoadList();
                            DbConn.CloseConn();
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Mã nhân viên không tồn tại!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    ResetTextBox();
                    LoadList();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.ToString(), "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
