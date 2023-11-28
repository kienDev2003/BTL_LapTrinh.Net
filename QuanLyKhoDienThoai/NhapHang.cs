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
    public partial class NhapHang : Form
    {
        DBConnection DbConn = new DBConnection();

        public NhapHang()
        {
            InitializeComponent();
        }

        private DataTable LoadDataToCbo(string columns, string table)
        {
            try
            {
                DbConn.GetConn();
                string query = $"SELECT {columns} FROM {table}";
                DataTable data = DbConn.DataTable(query);
                DbConn.CloseConn();
                return data;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.ToString(), "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }
        }

        private void ResetTextBox()
        {
            txtMaDN.Text = "";
            cboTenNV.Text = "--Chọn NV--";
            cboLoaiSP.Text = "--Chọn SP--";
            cboTenNCC.Text = "--Chọn NCC--";
            txtSLSP.Text = "";
        }

        private bool KiemTraTrungMa(string MaDN)
        {
            DbConn.GetConn();
            string query = $"SELECT * FROM tbl_NhapHang WHERE madonhang = N'{MaDN}'";
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

        private void LoadList()
        {
            try
            {
                lsvDanhSach.Items.Clear();

                DbConn.GetConn();

                string query = @"SELECT * FROM tbl_NhapHang";
                SqlDataReader reader = DbConn.Reader(query);
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        string MaDN = reader.GetString(0);
                        string TenNV = reader.GetString(1);
                        string TenNCC = reader.GetString(2);
                        string LoaiSP = reader.GetString(3);
                        string SoLuongSP = reader.GetString(4);
                        string NgayCapNhat = reader.GetString(5);

                        ListViewItem lvi = new ListViewItem(MaDN);
                        lvi.SubItems.Add(TenNV);
                        lvi.SubItems.Add(TenNCC);
                        lvi.SubItems.Add(LoaiSP);
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
                    MessageBox.Show("Danh sách đơn nhập không có dữ liệu!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.ToString(), "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void NhapHang_Load(object sender, EventArgs e)
        {
            Home homeForm = Application.OpenForms["Home"] as Home;
            if (homeForm != null && homeForm.lb_ChucVu.Text != "Quản Lý")
            {
                cboLoaiSP.DropDownStyle = ComboBoxStyle.DropDownList;
                btnSua.Enabled = false;
                btnXoa.Enabled = false;
            }
            DataTable dataCboLoaiSP = LoadDataToCbo("*", "tbl_Products");
            dataCboLoaiSP.Columns.Add("TenSP,MS,DL", typeof(string), "tensanpham + '-' + mausac + '-' + dungluong");

            DataTable dataCboNhaCC = LoadDataToCbo("tennhacungcap", "tbl_NCC");
            DataTable dataCboTkTenNV = LoadDataToCbo("tennhanvien", "tbl_Accounts");
            DataTable dataCboTenNV = LoadDataToCbo("tennhanvien", "tbl_Accounts");

            cboLoaiSP.DataSource = dataCboLoaiSP;
            cboLoaiSP.DisplayMember = "TenSP,MS,DL";
            cboLoaiSP.ValueMember = "masanpham";

            cboTenNCC.DataSource = dataCboNhaCC;
            cboTenNCC.DisplayMember = "tennhacungcap";

            cboTkTenNV.DataSource = dataCboTkTenNV;
            cboTkTenNV.DisplayMember = "tennhanvien";

            cboTenNV.DataSource = dataCboTenNV;
            cboTenNV.DisplayMember = "tennhanvien";

            LoadList();
        }

        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            try
            {
                lsvDanhSach.Items.Clear();

                string maDN = txtTkMaDN.Text.Trim();
                string tenNV = cboTkTenNV.Text.Trim();

                if (maDN != "")
                {
                    DbConn.GetConn();

                    string query = $"SELECT * FROM tbl_NhapHang WHERE madonhang = N'{maDN}'";
                    SqlDataReader reader = DbConn.Reader(query);
                    if (reader.HasRows)
                    {
                        if (reader.Read())
                        {
                            string MaDN = maDN.Trim();
                            string TenNV = reader.GetString(1);
                            string TenNCC = reader.GetString(2);
                            string LoaiSP = reader.GetString(3);
                            string SoLuongSP = reader.GetString(4);
                            string NgayCapNhat = reader.GetString(5);

                            ListViewItem lvi = new ListViewItem(MaDN);
                            lvi.SubItems.Add(TenNV);
                            lvi.SubItems.Add(TenNCC);
                            lvi.SubItems.Add(LoaiSP);
                            lvi.SubItems.Add(SoLuongSP);
                            lvi.SubItems.Add(NgayCapNhat);

                            lsvDanhSach.Items.Add(lvi);
                            txtTkMaDN.Text = "";
                        }
                        reader.Close();
                        DbConn.CloseConn();
                    }
                    else
                    {
                        reader.Close();
                        DbConn.CloseConn();
                        MessageBox.Show($"Không tìm thấy đơn nhập có mã {maDN} !", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        txtTkMaDN.Text = "";
                        LoadList();
                    }
                }
                else
                {
                    DbConn.GetConn();

                    string query = $"SELECT * FROM tbl_NhapHang WHERE tennhanvien LIKE N'%{tenNV}%'";

                    SqlDataReader reader = DbConn.Reader(query);
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            string MaDN = reader.GetString(0);
                            string TenNV = reader.GetString(1);
                            string TenNCC = reader.GetString(2);
                            string LoaiSP = reader.GetString(3);
                            string SoLuongSP = reader.GetString(4);
                            string NgayCapNhat = reader.GetString(5);

                            ListViewItem lvi = new ListViewItem(MaDN);
                            lvi.SubItems.Add(TenNV);
                            lvi.SubItems.Add(TenNCC);
                            lvi.SubItems.Add(LoaiSP);
                            lvi.SubItems.Add(SoLuongSP);
                            lvi.SubItems.Add(NgayCapNhat);

                            lsvDanhSach.Items.Add(lvi);
                        }
                        reader.Close();
                        DbConn.CloseConn();
                    }
                    else
                    {
                        reader.Close();
                        DbConn.CloseConn();
                        MessageBox.Show("Không tìm thấy đơn nhập!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        LoadList();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.ToString(), "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnNhapHang_Click(object sender, EventArgs e)
        {
            string MaDN = txtMaDN.Text.Trim();
            string TenNV = cboTenNV.Text.Trim();
            string TenNCC = cboTenNCC.Text.Trim();
            string LoaiSP = cboLoaiSP.Text.Trim();
            string SLSP = txtSLSP.Text.Trim();
            string NgayCapNhat = DateTime.Now.ToString("hh:mm-dd/MM/yy");

            try
            {
                if (MaDN == "" || TenNV == "--Chọn Tên NV--" || TenNCC == "Chọn Tên NCC--" || LoaiSP == "--Chọn SP--" || SLSP == "")
                {
                    MessageBox.Show("Vui lòng nhập đầy đủ thông tin!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                if (KiemTraTrungMa(MaDN) == false)
                {
                    DbConn.GetConn();
                    string query = $"INSERT INTO tbl_NhapHang (madonhang,tennhanvien,tennhacungcap,loaisanpham,soluongsanpham,ngaycapnhat)" +
                                    $" VALUES (N'{MaDN}', N'{TenNV}', N'{TenNCC}', N'{LoaiSP}', N'{SLSP}', N'{NgayCapNhat}') ";
                    int check = DbConn.Command(query);
                    if (check > 0)
                    {
                        MessageBox.Show("Tạo đơn nhập hàng thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        DbConn.CloseConn();
                        ResetTextBox();
                        LoadList();
                    }
                    else
                    {
                        MessageBox.Show("Tạo đơn nhập hàng KHÔNG thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        DbConn.CloseConn();
                    }
                }
                else
                {
                    MessageBox.Show("Mã đơn nhập bị trùng!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.ToString(), "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            string MaDN = txtMaDN.Text.Trim();
            string TenNV = cboTenNV.Text.Trim();
            string TenNCC = cboTenNCC.Text.Trim();
            string LoaiSP = cboLoaiSP.Text.Trim();
            string SLSP = txtSLSP.Text.Trim();
            string NgayCapNhat = DateTime.Now.ToString("hh:mm-dd/MM/yy");

            try
            {
                if (MaDN == "" || TenNV == "--Chọn Tên NV--" || TenNCC == "Chọn Tên NCC--" || LoaiSP == "--Chọn SP--" || SLSP == "")
                {
                    MessageBox.Show("Vui lòng nhập đầy đủ thông tin!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                if (KiemTraTrungMa(MaDN) == true)
                {
                    DialogResult result = MessageBox.Show($"Bạn thật sự muốn sửa thông tin của đơn nhập có mã {MaDN}", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (result == DialogResult.Yes)
                    {
                        DbConn.GetConn();
                        string query = $"UPDATE tbl_NhapHang SET tennhanvien = N'{TenNV}', tennhacungcap = N'{TenNCC}'," +
                                           $" loaisanpham = N'{LoaiSP}', soluongsanpham = N'{SLSP}'," +
                                           $" ngaycapnhat = N'{NgayCapNhat}' WHERE madonhang = N'{MaDN}'";
                        int check = DbConn.Command(query);
                        if (check > 0)
                        {
                            MessageBox.Show("Sửa đơn nhập hàng thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            DbConn.CloseConn();
                            ResetTextBox();
                            LoadList();
                        }
                        else
                        {
                            MessageBox.Show("Sửa đơn nhập hàng KHÔNG thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            DbConn.CloseConn();
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Mã đơn nhập bị trùng!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.ToString(), "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            string MaDN = txtMaDN.Text.Trim();

            try
            {
                if (MaDN == "")
                {
                    MessageBox.Show("Mã ĐN không được để trống!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                if (KiemTraTrungMa(MaDN) == true)
                {
                    DialogResult result = MessageBox.Show($"Bạn thật sự muốn xóa đơn nhập có mã {MaDN}", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (result == DialogResult.Yes)
                    {
                        DbConn.GetConn();
                        string query = $"DELETE FROM tbl_NhapHang WHERE madonhang = N'{MaDN}'";
                        int check = DbConn.Command(query);
                        if (check > 0)
                        {
                            MessageBox.Show("Xóa đơn nhập thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            LoadList();
                            ResetTextBox();
                            DbConn.CloseConn();
                        }
                        else
                        {
                            MessageBox.Show("Xóa đơn nhập KHÔNG thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            LoadList();
                            DbConn.CloseConn();
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Mã đơn nhập không tồn tại!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    ResetTextBox();
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

        private void lsvDanhSach_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lsvDanhSach.SelectedItems.Count > 0)
            {
                ListViewItem lvi = lsvDanhSach.SelectedItems[0];

                string MaDN = lvi.SubItems[0].Text;
                string TenNV = lvi.SubItems[1].Text;
                string LoaiSP = lvi.SubItems[3].Text;
                string TenNCC = lvi.SubItems[2].Text;
                string SLSP = lvi.SubItems[4].Text;

                txtMaDN.Text = MaDN;
                cboTenNV.Text = TenNV;
                cboLoaiSP.Text = LoaiSP;
                cboTenNCC.Text = TenNCC;
                txtSLSP.Text = SLSP;
            }
        }
    }
}
