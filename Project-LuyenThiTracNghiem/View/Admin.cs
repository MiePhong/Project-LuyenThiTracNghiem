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
    public partial class Admin : Form
    {
        public Admin()
        {
            InitializeComponent();
        }

        private void btnGiangVien_Click(object sender, EventArgs e)
        {
            GiangVien gv = new GiangVien();
            gv.ShowDialog();
        }

        private void btnHocVien_Click(object sender, EventArgs e)
        {
           AdminHocVien ad = new AdminHocVien();
            ad.ShowDialog();
        }

        private void btnAdmin_Click(object sender, EventArgs e)
        {
            QuanLyMon mon = new QuanLyMon();
            mon.ShowDialog();
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

        private void Admin_Load(object sender, EventArgs e)
        {

        }
    }
}
