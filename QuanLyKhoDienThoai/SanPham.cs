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

namespace QuanLyKhoDienThoai
{
    public partial class SanPham : Form
    {
        DBConnection DbConn = new DBConnection();
        public SanPham()
        {
            InitializeComponent();
        }

        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            try
            {
                lsvDanhSach.Items.Clear();

                string maSP = txtTkMaSP.Text.Trim();
                string tenSP = txtTkTenSP.Text.Trim();

                if (maSP.Trim() == "" && tenSP.Trim() == "")
                {
                    MessageBox.Show("Mã SP hoặc Tên SP không được để trống!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadList();
                    return;
                }
                if (maSP.Trim() != "" && tenSP.Trim() != "")
                {
                    MessageBox.Show("Vui lòng tìm theo Mã SP hoặc Tên SP !", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadList();
                    return;
                }
                if (maSP != "")
                {
                    DbConn.GetConn();

                    string query = $"SELECT * FROM tbl_Products WHERE masanpham = N'{maSP}'";
                    SqlDataReader reader = DbConn.Reader(query);
                    if (reader.HasRows)
                    {
                        if (reader.Read())
                        {
                            string MaSP = maSP.Trim();
                            string TenSP = reader.GetString(1);
                            string Hang = reader.GetString(2);
                            string MauSac = reader.GetString(3);
                            string DungLuong = reader.GetString(4);
                            string SoLuong = reader.GetString(5);
                            string GiaBan = reader.GetString(6);
                            string NgayCapNhat = reader.GetString(7);

                            ListViewItem lvi = new ListViewItem(MaSP);
                            lvi.SubItems.Add(TenSP);
                            lvi.SubItems.Add(Hang);
                            lvi.SubItems.Add(MauSac);
                            lvi.SubItems.Add(DungLuong);
                            lvi.SubItems.Add(SoLuong);
                            lvi.SubItems.Add(GiaBan);
                            lvi.SubItems.Add(NgayCapNhat);

                            lsvDanhSach.Items.Add(lvi);
                            txtTkMaSP.Text = "";
                        }
                        reader.Close();
                        DbConn.CloseConn();
                    }
                    else
                    {
                        reader.Close();
                        DbConn.CloseConn();
                        MessageBox.Show("Không tìm thấy sản phẩm!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        txtTkMaSP.Text = "";
                        LoadList();
                    }
                }
                else
                {
                    DbConn.GetConn();

                    string query = $"SELECT * FROM tbl_Products WHERE tensanpham LIKE N'%{tenSP}%'";

                    SqlDataReader reader = DbConn.Reader(query);
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            string MaSP = reader.GetString(0);
                            string TenSP = reader.GetString(1);
                            string Hang = reader.GetString(2);
                            string MauSac = reader.GetString(3);
                            string DungLuong = reader.GetString(4);
                            string SoLuong = reader.GetString(5);
                            string GiaBan = reader.GetString(6);
                            string NgayCapNhat = reader.GetString(7);

                            ListViewItem lvi = new ListViewItem(MaSP);
                            lvi.SubItems.Add(TenSP);
                            lvi.SubItems.Add(Hang);
                            lvi.SubItems.Add(MauSac);
                            lvi.SubItems.Add(DungLuong);
                            lvi.SubItems.Add(SoLuong);
                            lvi.SubItems.Add(GiaBan);
                            lvi.SubItems.Add(NgayCapNhat);

                            lsvDanhSach.Items.Add(lvi);
                            txtTkTenSP.Text = "";
                        }
                        reader.Close();
                        DbConn.CloseConn();
                    }
                    else
                    {
                        reader.Close();
                        DbConn.CloseConn();
                        MessageBox.Show("Không tìm thấy sản phẩm!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        txtTkTenSP.Text = "";
                        LoadList();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.ToString(), "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void SanPham_Load(object sender, EventArgs e)
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

        private void LoadList()
        {
            try
            {
                lsvDanhSach.Items.Clear();

                DbConn.GetConn();

                string query = @"SELECT * FROM tbl_Products";
                SqlDataReader reader = DbConn.Reader(query);
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        string MaSP = reader.GetString(0);
                        string TenSP = reader.GetString(1);
                        string Hang = reader.GetString(2);
                        string MauSac = reader.GetString(3);
                        string DungLuong = reader.GetString(4);
                        string SoLuong = reader.GetString(5);
                        string GiaBan = reader.GetString(6);
                        string NgayCapNhat = reader.GetString(7);

                        ListViewItem lvi = new ListViewItem(MaSP);
                        lvi.SubItems.Add(TenSP);
                        lvi.SubItems.Add(Hang);
                        lvi.SubItems.Add(MauSac);
                        lvi.SubItems.Add(DungLuong);
                        lvi.SubItems.Add(SoLuong);
                        lvi.SubItems.Add(GiaBan);
                        lvi.SubItems.Add(NgayCapNhat);

                        lsvDanhSach.Items.Add(lvi);
                    }
                    reader.Close();
                    DbConn.CloseConn();
                }
                else
                {
                    DbConn.CloseConn();
                    MessageBox.Show("Danh sách sản phẩm không có dữ liệu!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show("Lỗi: "+ex.ToString(),"Thông báo",MessageBoxButtons.OK, MessageBoxIcon.Error);
            }          
        }

        private bool KiemTraTrungMa(string MaSP)
        {
            DbConn.GetConn();
            string query = $"SELECT * FROM tbl_Products WHERE masanpham = N'{MaSP}'";
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

        private void ResetTextBox()
        {
            txtMaSP.Text = "";
            txtTenSP.Text = "";
            txtHang.Text = "";
            txtMauSac.Text = "";
            txtDungLuong.Text = "";
            txtSoLuong.Text = "";
            txtGiaBan.Text = "";
        }

        private void btnThem_Click(object sender, EventArgs e)
        {           
            try
            {
                string MaSP = txtMaSP.Text.Trim();
                string TenSP = txtTenSP.Text.Trim();
                string Hang = txtHang.Text.Trim();
                string MauSac = txtMauSac.Text.Trim();
                string DungLuong = txtDungLuong.Text.Trim();
                string SoLuong = txtSoLuong.Text.Trim();
                string GiaBan = txtGiaBan.Text.Trim();
                string NgayCapNhat = DateTime.Now.ToString("hh:mm-dd/MM/yy");

                if (MaSP == "" || TenSP == "" || Hang == "" || MauSac == "" || DungLuong == "" || GiaBan == "")
                {
                    MessageBox.Show("Vui lòng nhập đầy đủ thông tin!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                if (KiemTraTrungMa(MaSP) == true){
                    MessageBox.Show("Mã sản phẩm bị trùng!", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                DbConn.GetConn();
                string query = $"INSERT INTO tbl_Products (masanpham,tensanpham,thuonghieu,mausac,dungluong,soluong,giaban,ngaycapnhat)" +
                               $" VALUES (N'{MaSP}',N'{TenSP}',N'{Hang}',N'{MauSac}',N'{DungLuong}',N'{SoLuong}',N'{GiaBan}',N'{NgayCapNhat}')";
                int check = DbConn.Command(query);
                if (check > 0)
                {
                    MessageBox.Show("Thêm sản phẩm thành công!","Thông báo",MessageBoxButtons.OK, MessageBoxIcon.Information);
                    ResetTextBox();
                    LoadList();
                    DbConn.CloseConn();
                }
                else
                {
                    MessageBox.Show("Thêm không thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    LoadList();
                    DbConn.CloseConn();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: "+ex.ToString(),"Thông báo",MessageBoxButtons.OK,MessageBoxIcon.Error);
            }
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            ResetTextBox();
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            string MaSP = txtMaSP.Text.Trim();

            try
            {
                if (MaSP == "")
                {
                    MessageBox.Show("Mã SP không được để trống!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                if (KiemTraTrungMa(MaSP) == true)
                {
                    DialogResult result = MessageBox.Show($"Bạn thật sự muốn xóa sản phẩm có mã {MaSP}", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (result == DialogResult.Yes)
                    {
                        DbConn.GetConn();
                        string query = $"DELETE FROM tbl_Products WHERE masanpham = N'{MaSP}'";
                        int check = DbConn.Command(query);
                        if (check > 0)
                        {
                            MessageBox.Show("Xóa sản phẩm thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            LoadList();
                            ResetTextBox();
                            DbConn.CloseConn();
                        }
                        else
                        {
                            MessageBox.Show("Xóa không thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            LoadList();
                            DbConn.CloseConn();
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Mã sản phẩm không tồn tại!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    ResetTextBox();
                    LoadList();
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.ToString(), "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            string MaSP = txtMaSP.Text.Trim();
            string TenSP = txtTenSP.Text.Trim();
            string Hang = txtHang.Text.Trim();
            string MauSac = txtMauSac.Text.Trim();
            string DungLuong = txtDungLuong.Text.Trim();
            string SoLuong = txtSoLuong.Text.Trim();
            string GiaBan = txtGiaBan.Text.Trim();
            string NgayCapNhat = DateTime.Now.ToString("hh:mm-dd/MM/yy");
            try
            {
                if (MaSP == "" || TenSP == "" || Hang == "" || MauSac == "" || DungLuong == "" || GiaBan == "")
                {
                    MessageBox.Show("Vui lòng nhập đầy đủ thông tin!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                if (KiemTraTrungMa(MaSP) == true)
                {
                    DialogResult result = MessageBox.Show($"Bạn thật sự muốn sửa thông tin của sản phẩm có mã {MaSP}", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (result == DialogResult.Yes)
                    {
                        DbConn.GetConn();
                        string query = $"UPDATE tbl_Products SET tensanpham = N'{TenSP}', thuonghieu = N'{Hang}'," +
                                       $" mausac = N'{MauSac}', dungluong = N'{DungLuong}', soluong = N'{SoLuong}'," +
                                       $" giaban = N'{GiaBan}', ngaycapnhat = N'{NgayCapNhat}' WHERE masanpham = N'{MaSP}'";

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
                    MessageBox.Show("Mã SP không tồn tại!", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadList();
                }
            }catch(Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.ToString(), "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void lsvDanhSach_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(lsvDanhSach.SelectedItems.Count > 0)
            {
                ListViewItem lvi = lsvDanhSach.SelectedItems[0];

                string MaSP = lvi.SubItems[0].Text;
                string TenSP = lvi.SubItems[1].Text;
                string Hang = lvi.SubItems[2].Text;
                string MauSac = lvi.SubItems[3].Text;
                string DungLuong = lvi.SubItems[4].Text;
                string SoLuong = lvi.SubItems[5].Text;
                string GiaBan = lvi.SubItems[6].Text;

                txtMaSP.Text = MaSP;
                txtTenSP.Text = TenSP;
                txtHang.Text = Hang;
                txtMauSac.Text = MauSac;
                txtDungLuong.Text = DungLuong;
                txtSoLuong.Text = SoLuong;
                txtGiaBan.Text = GiaBan;
            }
        }
    }
}
