using Project_LuyenThiTracNghiem.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Project_LuyenThiTracNghiem.View
{
    public partial class DangKy : Form
    {
        public DangKy()
        {
            InitializeComponent();
            LoadData();
        }
        void LoadData()
        {
            // Load danh sách lớp học vào combobox
            using (LuyenThiTracNghiemEntities2 db = new LuyenThiTracNghiemEntities2())
            {
                var lopHocList = db.LOPHOCs.ToList();
                cbChonLop.DataSource = lopHocList;
                cbChonLop.DisplayMember = "TenLop";
                cbChonLop.ValueMember = "MaLop";
            }
        }
        private void DangKy_Load(object sender, EventArgs e)
        {

        }

        private void btnDangKy_Click(object sender, EventArgs e)
        {
            // Kiểm tra các điều kiện
            if (!IsValidMaHV() || !IsValidEmail() || !IsValidTenTaiKhoan() || !IsValidMatKhau() || !IsValidXacNhanMatKhau())
            {
                return;
            }

            using (LuyenThiTracNghiemEntities2 db = new LuyenThiTracNghiemEntities2())
            {
                // Kiểm tra Mã Học Viên đã tồn tại chưa
                string maHV = txtDKMaHV.Text.Trim();
                if (db.HOCVIENs.Any(hv => hv.MaHV == maHV))
                {
                    MessageBox.Show("Mã Học Viên đã tồn tại. Hãy nhập mã khác.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Kiểm tra Email đã tồn tại chưa
                string email = txtDKEmail.Text.Trim();
                if (db.HOCVIENs.Any(hv => hv.email == email))
                {
                    MessageBox.Show("Email đã tồn tại. Hãy nhập email khác.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Kiểm tra Tên Tài Khoản đã tồn tại chưa
                string tenTaiKhoan = txtDKTenTaiKhoan.Text.Trim();
                if (db.HOCVIENs.Any(hv => hv.TenTaiKhoan == tenTaiKhoan))
                {
                    MessageBox.Show("Tên Tài Khoản đã tồn tại. Hãy nhập tên tài khoản khác.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Kiểm tra và lưu thông tin vào database
                HOCVIEN hocVien = new HOCVIEN
                {
                    MaHV = maHV,
                    TenHV = txtDKTenHV.Text.Trim(),
                    Diachi =txtDKDiaChi.Text.Trim(),
                    SDT = Convert.ToDecimal(txtSDT.Text.Trim()),
                    email = email,
                    TenTaiKhoan = tenTaiKhoan,
                    MatKhau = txtMK.Text.Trim(),
                    TenLopHoc = cbChonLop.SelectedValue.ToString(),
                };

                db.HOCVIENs.Add(hocVien);
                db.SaveChanges();

                MessageBox.Show("Đăng ký thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Close();
            }
        }
        private bool IsValidMaHV()
        {
            string maHV = txtDKMaHV.Text.Trim();
            if (!Regex.IsMatch(maHV, @"^HV\d{3}$"))
            {
                MessageBox.Show("Mã Học Viên không hợp lệ. Vui lòng nhập theo định dạng HV001, HV002, ...", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            return true;
        }

        private bool IsValidEmail()
        {
            string email = txtDKEmail.Text.Trim();
            if (!Regex.IsMatch(email, @"^[a-zA-Z0-9_.]{3,20}@gmail.com(.vn|)$"))
            {
                MessageBox.Show("Email không hợp lệ. Vui lòng nhập đúng định dạng.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            return true;
        }

        private bool IsValidTenTaiKhoan()
        {
            string tenTaiKhoan = txtDKTenTaiKhoan.Text.Trim();
            if (string.IsNullOrEmpty(tenTaiKhoan))
            {
                MessageBox.Show("Vui lòng nhập Tên Tài Khoản.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            return true;
        }

        private bool IsValidMatKhau()
        {
            string matKhau = txtMK.Text.Trim();
            if (!Regex.IsMatch(matKhau, @"^[a-zA-Z0-9]{6,24}$"))
            {
                MessageBox.Show("Mật khẩu không hợp lệ. Vui lòng nhập từ 6 đến 24 ký tự, bao gồm chữ và số.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            return true;
        } 

        private bool IsValidXacNhanMatKhau()
        {
            string xacNhanMatKhau = txtDKXNMK.Text.Trim();
            string matKhau = txtMK.Text.Trim();
            if (xacNhanMatKhau != matKhau)
            {
                MessageBox.Show("Vui lòng xác nhận mật khẩu chính xác.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            return true;

        }

        private void txtMK_TextChanged(object sender, EventArgs e)
        {

        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {

        }
    }
}
