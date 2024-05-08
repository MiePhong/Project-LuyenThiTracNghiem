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
    public partial class AdminHocVien : Form
    {
        public AdminHocVien()
        {
            InitializeComponent();
        }

        void LoadData() // xem hoc vien 
        {
            LuyenThiTracNghiemEntities2 db = new LuyenThiTracNghiemEntities2();

            var result = from a in db.HOCVIENs select new { MaHV = a.MaHV, TenHV = a.TenHV, DiaChi = a.Diachi, tongsocautraloidung = a.tongsocautraloidung, Tenlophoc = a.TenLopHoc, Ngaythi = a.NgayThi, Madethi = a.Madethi, Ketqua = a.Ketqua };
            dtaGridView_Xemdiemthi.DataSource = result.ToList();
        }
        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();
            Login lg = new Login();
            lg.ShowDialog();
        }

        private void btnAdminXemDiem_Click(object sender, EventArgs e)
        {
            LoadData();
        }

        private void AdminHocVien_Load(object sender, EventArgs e)
        {

        }
    }
}
