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
    public partial class ThongKe : Form
    {
        DBConnection DbConn = new DBConnection();

        public ThongKe()
        {
            InitializeComponent();
        }

        private void LoadList()
        {
            try
            {
                lsvDanhSach.Items.Clear();

                DbConn.GetConn();

                string query = @"SELECT * FROM tbl_ThongKe";
                SqlDataReader reader = DbConn.Reader(query);
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        string MaDH = reader.GetString(0);
                        string TrangThai = reader.GetString(1);
                        string TenKH = reader.GetString(2);
                        string TenNV = reader.GetString(3);
                        string TenSP = reader.GetString(4);
                        string SoLuongSP = reader.GetString(5);
                        string NgayCapNhat = reader.GetString(6);

                        ListViewItem lvi = new ListViewItem(MaDH);
                        lvi.SubItems.Add(TrangThai);
                        lvi.SubItems.Add(TenKH);
                        lvi.SubItems.Add(TenNV);
                        lvi.SubItems.Add(TenSP);
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
                    MessageBox.Show("Danh sách thống kê không có dữ liệu!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.ToString(), "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            try
            {
                lsvDanhSach.Items.Clear();

                string maDH = txtTkMaDH.Text.Trim();
                string tenKH_NV = txtTkTenNV_KH.Text.Trim();

                if (maDH.Trim() == "" && tenKH_NV.Trim() == "")
                {
                    MessageBox.Show("Mã ĐH hoặc Tên KH/NV không được để trống!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadList();
                    return;
                }
                if (maDH.Trim() != "" && tenKH_NV.Trim() != "")
                {
                    MessageBox.Show("Vui lòng tìm theo Mã ĐH hoặc Tên KH/NV !", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadList();
                    return;
                }
                if (maDH != "")
                {
                    DbConn.GetConn();

                    string query = $"SELECT * FROM tbl_ThongKe WHERE madonhang = N'{maDH}'";
                    SqlDataReader reader = DbConn.Reader(query);
                    if (reader.HasRows)
                    {
                        if (reader.Read())
                        {
                            string MaDH = reader.GetString(0);
                            string TrangThai = reader.GetString(1);
                            string TenKH = reader.GetString(2);
                            string TenNV = reader.GetString(3);
                            string TenSP = reader.GetString(4);
                            string SoLuongSP = reader.GetString(5);
                            string NgayCapNhat = reader.GetString(6);

                            ListViewItem lvi = new ListViewItem(MaDH);
                            lvi.SubItems.Add(TrangThai);
                            lvi.SubItems.Add(TenKH);
                            lvi.SubItems.Add(TenNV);
                            lvi.SubItems.Add(TenSP);
                            lvi.SubItems.Add(SoLuongSP);
                            lvi.SubItems.Add(NgayCapNhat);

                            lsvDanhSach.Items.Add(lvi);
                            txtTkMaDH.Text = "";
                        }
                        reader.Close();
                        DbConn.CloseConn();
                    }
                    else
                    {
                        reader.Close();
                        DbConn.CloseConn();
                        MessageBox.Show("Không tìm thấy đơn hàng!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        txtTkMaDH.Text = "";
                        LoadList();
                    }
                }
                else
                {
                    DbConn.GetConn();

                    string query = $"SELECT * FROM tbl_ThongKe WHERE tenkhachhang LIKE N'%{tenKH_NV}%' OR tennhanvien LIKE N'%{tenKH_NV}%'";

                    SqlDataReader reader = DbConn.Reader(query);
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            string MaDH = reader.GetString(0);
                            string TrangThai = reader.GetString(1);
                            string TenKH = reader.GetString(2);
                            string TenNV = reader.GetString(3);
                            string TenSP = reader.GetString(4);
                            string SoLuongSP = reader.GetString(5);
                            string NgayCapNhat = reader.GetString(6);

                            ListViewItem lvi = new ListViewItem(MaDH);
                            lvi.SubItems.Add(TrangThai);
                            lvi.SubItems.Add(TenKH);
                            lvi.SubItems.Add(TenNV);
                            lvi.SubItems.Add(TenSP);
                            lvi.SubItems.Add(SoLuongSP);
                            lvi.SubItems.Add(NgayCapNhat);

                            lsvDanhSach.Items.Add(lvi);
                            txtTkTenNV_KH.Text = "";
                        }
                        reader.Close();
                        DbConn.CloseConn();
                    }
                    else
                    {
                        reader.Close();
                        DbConn.CloseConn();
                        MessageBox.Show("Không tìm thấy đơn hàng!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        txtTkTenNV_KH.Text = "";
                        LoadList();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.ToString(), "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ThongKe_Load(object sender, EventArgs e)
        {
            LoadList();
            lbSLDN.Text = SLDN();
            lbSLDX.Text = SLDX();
        }

        private void lsvDanhSach_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lsvDanhSach.SelectedItems.Count > 0)
            {
                ListViewItem lvi = lsvDanhSach.SelectedItems[0];

                string MaDH = lvi.SubItems[0].Text;
                string TrangThai = lvi.SubItems[1].Text;
                string TenKH = lvi.SubItems[2].Text;
                string TenNV = lvi.SubItems[3].Text;
                string TenSP = lvi.SubItems[4].Text;
                string SLSP = lvi.SubItems[5].Text;

                txtMaDH.Text = MaDH;
                txtTenKH.Text = TenKH;
                txtTenSP.Text = TenSP;
                txtTenNV.Text = TenNV;
                txtTrangThai.Text = TrangThai;
                txtSLSP.Text = SLSP;
            }
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            txtMaDH.Text = "";
            txtTrangThai.Text = "";
            txtTenKH.Text = "";
            txtTenNV.Text = "";
            txtTenSP.Text = "";
            txtSLSP.Text = "";
        }

        private string SLDX()
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

        private string SLDN()
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
