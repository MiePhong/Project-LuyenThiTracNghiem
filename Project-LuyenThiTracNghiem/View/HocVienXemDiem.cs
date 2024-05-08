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
    public partial class HocVienXemDiem : Form
    {

        private string maHocVien { get; set; }
        public HocVienXemDiem(string maHV)
        {
            InitializeComponent();


            maHocVien = maHV;
        }
        void LoadData()
        {
            if (string.IsNullOrEmpty(maHocVien))
            {
                MessageBox.Show("Mã học viên không hợp lệ.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            LuyenThiTracNghiemEntities2 db = new LuyenThiTracNghiemEntities2();

            var result = from a in db.HOCVIENs
                         where a.MaHV == maHocVien
                         select new
                         {
                             MaHV = a.MaHV,
                             TenHV = a.TenHV,
                             tongsocautraloidung = a.tongsocautraloidung,
                             Tenlophoc = a.TenLopHoc,
                             Ngaythi = a.NgayThi,
                             Madethi = a.Madethi,
                             Ketqua = a.Ketqua
                         };

            var hocVien = result.FirstOrDefault();

            if (hocVien != null)
            {
                dtaGridView_Xemdiemthi.DataSource = new List<dynamic> { hocVien };
            }
            else
            {
                MessageBox.Show("Không tìm thấy thông tin điểm cho học viên có mã " + maHocVien);
            }
        }
        private void btnchuyenThi_Click(object sender, EventArgs e)
        {
            HocVien hocvien = new HocVien();
            hocvien.ShowDialog();
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

        private void btnchuyenXD_Click(object sender, EventArgs e)
        {

        }

        private void btnXemDiem_Click(object sender, EventArgs e)
        {
           
        }

        private void HocVienXemDiem_Load(object sender, EventArgs e)
        {
            LoadData();
        }

        private void dtaGridView_Xemdiemthi_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
