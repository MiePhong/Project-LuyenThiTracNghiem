using Project_LuyenThiTracNghiem.Model;
using Project_LuyenThiTracNghiem.View;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Entity;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Project_LuyenThiTracNghiem
{
    public partial class Login : Form
    {
        private string tenTaiKhoan;
        private string matKhau;
        private string loaiNguoiDung;

        public Login()
        {
            InitializeComponent();
            LoadData();
           
        }

        public Login(string tenTaiKhoan, string matKhau) : this()
        {
            this.tenTaiKhoan = tenTaiKhoan;
            this.matKhau = matKhau;
        }

        public string TenTaiKhoan { get => tenTaiKhoan; set => tenTaiKhoan = value; }
        public string MatKhau { get => matKhau; set => matKhau = value; }
        void LoadData()
        {
            LuyenThiTracNghiemEntities2 db = new LuyenThiTracNghiemEntities2();
        }

        private void DangKy_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            DangKy dangKy = new DangKy();
            dangKy.ShowDialog();
        }

        private void thayDoiMK_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            ThayKM thayMK = new ThayKM();
            thayMK.ShowDialog();
        }

        private void btnDangNhap_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(loaiNguoiDung))
            {
                MessageBox.Show("Vui lòng chọn vai trò của bạn trước khi đăng nhập.");
                return;
            }

            if (string.IsNullOrEmpty(txtTenDangNhap.Text) || string.IsNullOrEmpty(txtMatKhau.Text))
            {
                MessageBox.Show("Vui lòng nhập tên tài khoản và mật khẩu.");
                return;
            }

            using (LuyenThiTracNghiemEntities2 db = new LuyenThiTracNghiemEntities2())
            {
                switch (loaiNguoiDung)
                {
                    case "HocVien":
                        var hocVien = db.HOCVIENs.FirstOrDefault(hv => hv.TenTaiKhoan == txtTenDangNhap.Text && hv.MatKhau == txtMatKhau.Text);
                        if (hocVien != null)
                        {
                            HocVien formHocVien = new HocVien(txtTenDangNhap.Text, txtMatKhau.Text);
                            formHocVien.Show();
                            this.Hide();
                        }
                        else
                        {
                            MessageBox.Show("Tên tài khoản hoặc mật khẩu không đúng cho học viên.");
                        }
                        break;

                    case "GiangVien":
                        var giangVien = db.GIANGVIENs.FirstOrDefault(gv => gv.TenTaiKhoan == txtTenDangNhap.Text && gv.MatKhau == txtMatKhau.Text);
                        if (giangVien != null)
                        {
                            // Thực hiện các hành động cho giảng viên
                            // Ví dụ: Mở form của giảng viên
                            OpenFormGiangVien();
                        }
                        else
                        {
                            MessageBox.Show("Tên tài khoản hoặc mật khẩu không đúng cho giảng viên.");
                        }
                        break;

                    case "Admin":
                        var admin = db.Quantriviens.FirstOrDefault(qtv => qtv.TenTaiKhoan == txtTenDangNhap.Text && qtv.MatKhau == txtMatKhau.Text);
                        if (admin != null)
                        {
                            // Thực hiện các hành động cho admin
                            // Ví dụ: Mở form của admin
                            OpenFormAdmin();
                        }
                        else
                        {
                            MessageBox.Show("Tên tài khoản hoặc mật khẩu không đúng cho admin.");
                        }
                        break;
                }

            }
        }
                private void OpenFormHocVien()
            {
            HocVien hocvien = new HocVien();
            hocvien.ShowDialog();
        }

            private void OpenFormGiangVien()
            {
            GiangVien giangvien = new GiangVien();
            giangvien.ShowDialog();
        }

            private void OpenFormAdmin()
            {
            Admin admin = new Admin();
            admin.ShowDialog();
        }

        private void btnGiangVien_Click(object sender, EventArgs e)
        {
            ClearButtonColors();
            btnGiangVien.BackColor = Color.Yellow;
            loaiNguoiDung = "GiangVien";
        }

        private void btnHocVien_Click(object sender, EventArgs e)
        {
            ClearButtonColors();
            btnHocVien.BackColor = Color.Yellow;
            loaiNguoiDung = "HocVien";
        }

        private void btnAdmin_Click(object sender, EventArgs e)
        {
            ClearButtonColors();
            btnAdmin.BackColor = Color.Yellow;
            loaiNguoiDung = "Admin";
        }
        private void ClearButtonColors()
        {
            // Đặt màu nền của tất cả các nút về màu mặc định
            btnGiangVien.BackColor = SystemColors.Control;
            btnHocVien.BackColor = SystemColors.Control;
            btnAdmin.BackColor = SystemColors.Control;
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Login_Load(object sender, EventArgs e)
        {

        }
    }
    }

