using Project_LuyenThiTracNghiem.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using System.Windows.Forms;

namespace Project_LuyenThiTracNghiem.View
{
    public partial class ThayKM : Form
    {
        private string loaiNguoiDung;
    

        public ThayKM()
        {
            InitializeComponent();
            LoadData();
        }
        void LoadData()
        {
            LuyenThiTracNghiemEntities2 db = new LuyenThiTracNghiemEntities2();
        }

        private void btnTDGiangVien_Click(object sender, EventArgs e)
        {
            ClearButtonColors();
            btnTDGiangVien.BackColor = System.Drawing.Color.Yellow;
            loaiNguoiDung = "GiangVien";
        }

        private void btnTDHocVien_Click(object sender, EventArgs e)
        {
            ClearButtonColors();
            btnTDHocVien.BackColor = System.Drawing.Color.Yellow;
            loaiNguoiDung = "HocVien";
        }

        private void btnTDAdmin_Click(object sender, EventArgs e)
        {
            ClearButtonColors();
            btnTDAdmin.BackColor = System.Drawing.Color.Yellow;
            loaiNguoiDung = "Admin";
        }
        private void ClearButtonColors()
        {
            // Đặt màu nền của tất cả các nút về màu mặc định
            btnTDGiangVien.BackColor = SystemColors.Control;
            btnTDHocVien.BackColor = SystemColors.Control;
            btnTDAdmin.BackColor = SystemColors.Control;
        }

        private void btnThayDoi_Click(object sender, EventArgs e)
        {
            string tenTaiKhoan = txtTenTK.Text.Trim();
            string matKhauHienTai = txtMatKhauHienTai.Text.Trim();
            string matKhauMoi = txtMatKhauMoi.Text.Trim();
            string xacNhanMK = txtXNMK.Text.Trim();

            // Kiểm tra xem đã nhập đầy đủ thông tin chưa
            if (string.IsNullOrEmpty(tenTaiKhoan) || string.IsNullOrEmpty(matKhauHienTai) ||
                string.IsNullOrEmpty(matKhauMoi) || string.IsNullOrEmpty(xacNhanMK))
            {
                MessageBox.Show("Vui lòng nhập đầy đủ thông tin.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            using (LuyenThiTracNghiemEntities2 db = new LuyenThiTracNghiemEntities2())
            {
                // Kiểm tra xem tên tài khoản có tồn tại không
                if (!db.Database.SqlQuery<int>($"SELECT COUNT(*) FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = '{loaiNguoiDung}'").Any())
                {
                    MessageBox.Show("Tên tài khoản không tồn tại.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Kiểm tra xem mật khẩu hiện tại có đúng không
                if (!db.Database.SqlQuery<string>($"SELECT {loaiNguoiDung}.MatKhau FROM {loaiNguoiDung} WHERE {loaiNguoiDung}.TenTaiKhoan = @p0", tenTaiKhoan).FirstOrDefault().Equals(matKhauHienTai))
                {
                    MessageBox.Show("Mật khẩu hiện tại không đúng.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Thay đổi mật khẩu trong database
                db.Database.ExecuteSqlCommand($"UPDATE {loaiNguoiDung} SET MatKhau = @p0 WHERE {loaiNguoiDung}.TenTaiKhoan = @p1", matKhauMoi, tenTaiKhoan);

                MessageBox.Show("Thay đổi mật khẩu thành công.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void ThayKM_Load(object sender, EventArgs e)
        {

        }
    }
}
