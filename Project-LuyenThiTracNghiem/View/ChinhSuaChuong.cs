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
    public partial class ChinhSuaChuong : Form
    {
        private string MaChuong;
        private string tenChuong;

        public ChinhSuaChuong(string MaChuong, string tenChuong)
        {
            InitializeComponent();
            this.MaChuong = MaChuong;
            this.tenChuong = tenChuong;
        }

        private void ChinhSuaChuong_Load(object sender, EventArgs e)
        {
            txtMaChuongHoc.Text = MaChuong;
            txtTenChuongHoc.Text = tenChuong;
        }

            private void btnLuuC_Click(object sender, EventArgs e)
        {
            // Lấy giá trị từ TextBox
            string newMaChuong = txtMaChuongHoc.Text.Trim();
            string newTenChuong = txtTenChuongHoc.Text.Trim();

            // Kiểm tra điều kiện
            if (string.IsNullOrEmpty(newMaChuong) || string.IsNullOrEmpty(newTenChuong))
            {
                MessageBox.Show("Mã chương và Tên chương không được để trống.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Thực hiện lưu dữ liệu vào cơ sở dữ liệu
            LuuThongTinChuong(newMaChuong, newTenChuong);

            // Đóng form
            this.Close();
        }
        private TextBox txtTenChuongHoc;
        private Button btnLuuChuong;
        // Khai báo biến để lưu trữ thông tin mã và tên chương


        // Hàm lưu thông tin chương vào cơ sở dữ liệu
        private void LuuThongTinChuong(string maChuong, string tenChuong)
        {
            using (LuyenThiTracNghiemEntities2 db = new LuyenThiTracNghiemEntities2())
            {
                // Tìm chương cần chỉnh sửa
                var chuong = db.QUANLYCHUONGs.Find(maChuong);

                if (chuong != null)
                {
                    // Cập nhật thông tin chương
                    chuong.TenChuong = tenChuong;

                    // Lưu thay đổi vào cơ sở dữ liệu
                    db.SaveChanges();

                    MessageBox.Show("Chỉnh sửa chương thành công.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Không tìm thấy chương cần chỉnh sửa.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }

}
