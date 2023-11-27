using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QuanLyKhoDienThoai
{
    public partial class Home : Form
    {
        private Form formCon;

        #region Method
        private void ShowFromCon( Form form)
        {
            if (formCon != null)
            {
                formCon.Close();
            }
            formCon = form;
            form.TopLevel = false;
            form.FormBorderStyle = FormBorderStyle.None;
            form.Dock = DockStyle.Fill;
            content.Controls.Add( form );
            content.Tag = form;
            form.BringToFront();
            form.Show();
        }


        #endregion

        #region Events
        public Home(string tenNv,string chucVu)
        {
            InitializeComponent();
            lb_Name.Text = tenNv;
            lb_ChucVu.Text = chucVu;
            lb_NgayGio.Text = DateTime.Now.ToString("ddd--dd/MM/yyyy", new CultureInfo("vi-VN"));
            if(chucVu == "Nhân Viên")
            {
                lb_NhaCungCap.Enabled = false;
                lb_NhanVien.Enabled = false;
                lb_ThongKe.Enabled = false;
            }
        }

        private void lb_SanPham_Click(object sender, EventArgs e)
        {
            ShowFromCon(new SanPham());
        }

        private void lb_Home_Click(object sender, EventArgs e)
        {
            if (formCon != null)
            {
                formCon.Close();
            }
        }

        private void lb_Home_MouseEnter(object sender, EventArgs e)
        {
            lb_Home.ForeColor = Color.Yellow;
        }

        private void lb_Home_MouseLeave(object sender, EventArgs e)
        {
            lb_Home.ForeColor = Color.White;
        }

        private void lb_SanPham_MouseEnter(object sender, EventArgs e)
        {
            lb_SanPham.ForeColor = Color.Yellow;
        }

        private void lb_SanPham_MouseLeave(object sender, EventArgs e)
        {
            lb_SanPham.ForeColor = Color.White;
        }

        private void lb_XuatHang_MouseEnter(object sender, EventArgs e)
        {
            lb_XuatHang.ForeColor = Color.Yellow;
        }

        private void lb_XuatHang_MouseLeave(object sender, EventArgs e)
        {
            lb_XuatHang.ForeColor = Color.White;
        }

        private void lb_NhapHang_MouseEnter(object sender, EventArgs e)
        {
            lb_NhapHang.ForeColor = Color.Yellow;
        }

        private void lb_NhapHang_MouseLeave(object sender, EventArgs e)
        {
            lb_NhapHang.ForeColor = Color.White;
        }

        private void lb_NhaCungCap_MouseEnter(object sender, EventArgs e)
        {
            lb_NhaCungCap.ForeColor = Color.Yellow;
        }

        private void lb_NhaCungCap_MouseLeave(object sender, EventArgs e)
        {
            lb_NhaCungCap.ForeColor = Color.White;
        }

        private void lb_NhanVien_MouseEnter(object sender, EventArgs e)
        {
            lb_NhanVien.ForeColor = Color.Yellow;
        }

        private void lb_NhanVien_MouseLeave(object sender, EventArgs e)
        {
            lb_NhanVien.ForeColor = Color.White;
        }

        private void lb_ThongKe_MouseEnter(object sender, EventArgs e)
        {
            lb_ThongKe.ForeColor = Color.Yellow;
        }

        private void lb_ThongKe_MouseLeave(object sender, EventArgs e)
        {
            lb_ThongKe.ForeColor = Color.White;
        }

        private void lb_LogOut_MouseEnter(object sender, EventArgs e)
        {
            lb_LogOut.Cursor = Cursors.Hand;
            lb_LogOut.ForeColor = Color.Yellow;
        }

        private void lb_LogOut_MouseLeave(object sender, EventArgs e)
        {
            lb_LogOut.ForeColor = Color.White;
        }

        private void lb_XuatHang_Click(object sender, EventArgs e)
        {
            ShowFromCon(new XuatHang());
        }
        #endregion

        private void lb_LogOut_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void lb_NhapHang_Click(object sender, EventArgs e)
        {
            ShowFromCon(new NhapHang());
        }

        private void lb_NhaCungCap_Click(object sender, EventArgs e)
        {
            ShowFromCon(new NhaCungCap());
        }

        private void lb_NhanVien_Click(object sender, EventArgs e)
        {
            ShowFromCon(new NhanVien());
        }

        private void lb_ThongKe_Click(object sender, EventArgs e)
        {
            ShowFromCon(new ThongKe());
        }
    }
}
