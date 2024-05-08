using Project_LuyenThiTracNghiem.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Project_LuyenThiTracNghiem.View
{
    public partial class GiangVien : Form
    {
        public GiangVien()
        {
            InitializeComponent();
        }
        void LoadData() // xem hoc vien 
        {
            LuyenThiTracNghiemEntities2 db = new LuyenThiTracNghiemEntities2();
            var result = from a in db.HOCVIENs select new { MaHV = a.MaHV, TenHV = a.TenHV, DiaChi = a.Diachi, tongsocautraloidung = a.tongsocautraloidung, Tenlophoc = a.TenLopHoc, Ngaythi = a.NgayThi, Madethi = a.Madethi, Ketqua = a.Ketqua };
            dataGridView_GiangVien.DataSource = result.ToList();
        }
        private void btnGVDeThi_Click(object sender, EventArgs e)
        {
            GVDETHI gvdethi = new GVDETHI();
            gvdethi.ShowDialog();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            // Hiển thị hộp thoại xác nhận
            DialogResult result = MessageBox.Show("Bạn có chắc chắn muốn đăng xuất không?", "Xác nhận đăng xuất", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            // Kiểm tra kết quả từ hộp thoại xác nhận
            if (result == DialogResult.Yes)
            {
                // Đăng xuất: Đóng form hiện tại và mở form đăng nhập
                this.Close();

                // Mở form đăng nhập
                Login loginForm = new Login();
                loginForm.ShowDialog();
            }
        }

        private void GiangVien_Load(object sender, EventArgs e)
        {

        }

        private void btnGVXemDiem_Click(object sender, EventArgs e)
        {
            LoadData();
        }
    }
}
