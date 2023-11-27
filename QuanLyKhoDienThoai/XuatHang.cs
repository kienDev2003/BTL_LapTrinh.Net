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
    public partial class XuatHang : Form
    {
        DBConnection DbConn = new DBConnection();
        public XuatHang()
        {
            InitializeComponent();
        }

        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            try
            {
                lsvDanhSach.Items.Clear();

                string maDX = txtTkMaDX.Text.Trim();
                string tenKh = txtTkTenKH.Text.Trim();

                if (maDX.Trim() == "" && tenKh.Trim() == "")
                {
                    MessageBox.Show("Mã ĐX hoặc Tên KH không được để trống!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadList();
                    return;
                }
                if (maDX.Trim() != "" && tenKh.Trim() != "")
                {
                    MessageBox.Show("Vui lòng tìm theo Mã ĐX hoặc Tên KH !", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadList();
                    return;
                }
                if (maDX != "")
                {
                    DbConn.GetConn();

                    string query = $"SELECT * FROM tbl_DonXuat WHERE madonhang = N'{maDX}'";
                    SqlDataReader reader = DbConn.Reader(query);
                    if (reader.HasRows)
                    {
                        if (reader.Read())
                        {
                            string MaDX = maDX.Trim();
                            string TenKH = reader.GetString(1);
                            string SĐT = reader.GetString(2);
                            string DiaChi = reader.GetString(3);
                            string LoaiSP = reader.GetString(5);
                            string SoLuongSP = reader.GetString(6);
                            string NgayCapNhat = reader.GetString(4);

                            ListViewItem lvi = new ListViewItem(MaDX);
                            lvi.SubItems.Add(TenKH);
                            lvi.SubItems.Add(LoaiSP);
                            lvi.SubItems.Add(SĐT);
                            lvi.SubItems.Add(DiaChi);
                            lvi.SubItems.Add(SoLuongSP);
                            lvi.SubItems.Add(NgayCapNhat);

                            lsvDanhSach.Items.Add(lvi);
                            txtTkMaDX.Text = "";
                        }
                        reader.Close();
                        DbConn.CloseConn();
                    }
                    else
                    {
                        reader.Close();
                        DbConn.CloseConn();
                        MessageBox.Show("Không tìm thấy đơn xuất!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        txtTkMaDX.Text = "";
                        LoadList();
                    }
                }
                else
                {
                    DbConn.GetConn();

                    string query = $"SELECT * FROM tbl_DonXuat WHERE tenkhachhang LIKE N'%{tenKh}%'";

                    SqlDataReader reader = DbConn.Reader(query);
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            string MaDX = reader.GetString(0);
                            string TenKH = reader.GetString(1);
                            string SĐT = reader.GetString(2);
                            string DiaChi = reader.GetString(3);
                            string LoaiSP = reader.GetString(5);
                            string SoLuongSP = reader.GetString(6);
                            string NgayCapNhat = reader.GetString(4);

                            ListViewItem lvi = new ListViewItem(MaDX);
                            lvi.SubItems.Add(TenKH);
                            lvi.SubItems.Add(LoaiSP);
                            lvi.SubItems.Add(SĐT);
                            lvi.SubItems.Add(DiaChi);
                            lvi.SubItems.Add(SoLuongSP);
                            lvi.SubItems.Add(NgayCapNhat);

                            lsvDanhSach.Items.Add(lvi);
                            txtTkTenKH.Text = "";
                        }
                        reader.Close();
                        DbConn.CloseConn();
                    }
                    else
                    {
                        reader.Close();
                        DbConn.CloseConn();
                        MessageBox.Show("Không tìm thấy đơn xuất!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        txtTkTenKH.Text = "";
                        LoadList();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.ToString(), "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadList()
        {
            try
            {
                lsvDanhSach.Items.Clear();

                DbConn.GetConn();

                string query = @"SELECT * FROM tbl_DonXuat";
                SqlDataReader reader = DbConn.Reader(query);
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        string MaDX = reader.GetString(0);
                        string TenKH = reader.GetString(1);
                        string SĐT = reader.GetString(2);
                        string DiaChi = reader.GetString(3);
                        string LoaiSP = reader.GetString(5);
                        string SoLuongSP = reader.GetString(6);
                        string NgayCapNhat = reader.GetString(4);

                        ListViewItem lvi = new ListViewItem(MaDX);
                        lvi.SubItems.Add(TenKH);
                        lvi.SubItems.Add(LoaiSP);
                        lvi.SubItems.Add(SĐT);
                        lvi.SubItems.Add(DiaChi);
                        lvi.SubItems.Add(SoLuongSP);
                        lvi.SubItems.Add(NgayCapNhat);

                        lsvDanhSach.Items.Add(lvi);
                    }
                    reader.Close();
                    DbConn.CloseConn();
                }
                else
                {
                    DbConn.CloseConn();
                    MessageBox.Show("Danh sách đơn xuất không có dữ liệu!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.ToString(), "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadDataToCbo()
        {
            try
            {
                DbConn.GetConn();
                string query = @"SELECT tensanpham FROM tbl_Products";
                DataTable data = DbConn.DataTable(query);
                cboLoaiSP.DataSource = data;
                cboLoaiSP.DisplayMember = "tensanpham";
                DbConn.CloseConn();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.ToString(), "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ResetTextBox()
        {
            txtMaDX.Text = "";
            txtTenKH.Text = "";
            cboLoaiSP.Text = "--Chọn SP--";
            txtSDT.Text = "";
            txtAddr.Text = "";
            txtSLSP.Text = "";
        }

        private bool KiemTraTrungMa(string MaDX)
        {
            DbConn.GetConn();
            string query = $"SELECT * FROM tbl_DonXuat WHERE madonhang = N'{MaDX}'";
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

        private void XuatHang_Load(object sender, EventArgs e)
        {
            LoadList();
            LoadDataToCbo();
            Home homeForm = Application.OpenForms["Home"] as Home;
            if (homeForm != null && homeForm.lb_ChucVu.Text != "Quản Lý")
            {
                cboLoaiSP.DropDownStyle = ComboBoxStyle.DropDownList;
                btnSua.Enabled = false;
                btnXoa.Enabled = false;
            }
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            ResetTextBox();
        }

        private void lsvDanhSach_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lsvDanhSach.SelectedItems.Count > 0)
            {
                ListViewItem lvi = lsvDanhSach.SelectedItems[0];

                string MaDX = lvi.SubItems[0].Text;
                string TenKH = lvi.SubItems[1].Text;
                string LoaiSP = lvi.SubItems[2].Text;
                string SĐT = lvi.SubItems[3].Text;
                string DiaCHi = lvi.SubItems[4].Text;
                string SLSP = lvi.SubItems[5].Text;

                txtMaDX.Text = MaDX;
                txtTenKH.Text = TenKH;
                cboLoaiSP.Text = LoaiSP;
                txtSDT.Text = SĐT;
                txtAddr.Text = DiaCHi;
                txtSLSP.Text = SLSP;

            }
        }

        private void btnXuatDon_Click(object sender, EventArgs e)
        {
            try
            {
                string MaDX = txtMaDX.Text.Trim();
                string TenKH = txtTenKH.Text.Trim();
                string LoaiSP = cboLoaiSP.Text.Trim();
                string SĐT = txtSDT.Text.Trim();
                string DiaChi = txtAddr.Text.Trim();
                string SLSP = txtSLSP.Text.Trim();
                string NgayCapNhat = DateTime.Now.ToString("hh:mm-dd/MM/yy");

                if (MaDX == "" || TenKH == "" || LoaiSP == "" || SĐT == "" || DiaChi == "" || SLSP == "")
                {
                    MessageBox.Show("Vui lòng nhập đầy đủ thông tin!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                if (KiemTraTrungMa(MaDX) == false)
                {
                    DbConn.GetConn();
                    string query = $"INSERT INTO tbl_DonXuat (madonhang,tenkhachhang,sodienthoai,diachi,soluongsanpham,loaisanpham,ngaycapnhat)" +
                                    $" VALUES (N'{MaDX}', N'{TenKH}', N'{SĐT}', N'{DiaChi}', N'{SLSP}', N'{LoaiSP}', N'{NgayCapNhat}') ";
                    int check = DbConn.Command(query);
                    if (check > 0)
                    {
                        MessageBox.Show("Tạo đơn xuất hàng thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        DbConn.CloseConn();
                        LoadList();
                    }
                    else
                    {
                        MessageBox.Show("Tạo đơn xuất hàng KHÔNG thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        DbConn.CloseConn();
                    }
                }
                else
                {
                    MessageBox.Show("Mã đơn xuất bị trùng!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.ToString(), "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            string MaDX = txtMaDX.Text.Trim();
            string TenKH = txtTenKH.Text.Trim();
            string LoaiSP = cboLoaiSP.Text.Trim();
            string SĐT = txtSDT.Text.Trim();
            string DiaChi = txtAddr.Text.Trim();
            string SLSP = txtSLSP.Text.Trim();
            string NgayCapNhat = DateTime.Now.ToString("hh:mm-dd/MM/yy");

            try
            {
                if (MaDX == "" || TenKH == "" || LoaiSP == "" || SĐT == "" || DiaChi == "" || SLSP == "")
                {
                    MessageBox.Show("Vui lòng nhập đầy đủ thông tin!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                if (KiemTraTrungMa(MaDX) == true)
                {
                    DialogResult result = MessageBox.Show($"Bạn thật sự muốn sửa thông tin của đơn xuất có mã {MaDX}", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (result == DialogResult.Yes)
                    {
                        DbConn.GetConn();
                        string query = $"UPDATE tbl_DonXuat SET tenkhachhang = N'{TenKH}', sodienthoai = N'{SĐT}'," +
                                           $" diachi = N'{DiaChi}', loaisanpham = N'{LoaiSP}', soluongsanpham = N'{SLSP}'," +
                                           $" ngaycapnhat = N'{NgayCapNhat}' WHERE madonhang = N'{MaDX}'";
                        int check = DbConn.Command(query);
                        if (check > 0)
                        {
                            MessageBox.Show("Sửa đơn xuất hàng thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            DbConn.CloseConn();
                            ResetTextBox();
                            LoadList();
                        }
                        else
                        {
                            MessageBox.Show("Sửa đơn xuất hàng KHÔNG thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            DbConn.CloseConn();
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Mã đơn xuất bị trùng!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.ToString(), "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            string MaDX = txtMaDX.Text.Trim();

            try
            {
                if (MaDX == "")
                {
                    MessageBox.Show("Mã ĐX không được để trống!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                if (KiemTraTrungMa(MaDX) == true)
                {
                    DialogResult result = MessageBox.Show($"Bạn thật sự muốn xóa đơn xuất có mã {MaDX}", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (result == DialogResult.Yes)
                    {
                        DbConn.GetConn();
                        string query = $"DELETE FROM tbl_DonXuat WHERE madonhang = N'{MaDX}'";
                        int check = DbConn.Command(query);
                        if (check > 0)
                        {
                            MessageBox.Show("Xóa đơn xuất thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            LoadList();
                            ResetTextBox();
                            DbConn.CloseConn();
                        }
                        else
                        {
                            MessageBox.Show("Xóa đơn xuất KHÔNG thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            LoadList();
                            DbConn.CloseConn();
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Mã đơn xuất không tồn tại!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    ResetTextBox();
                    LoadList();
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.ToString(), "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
