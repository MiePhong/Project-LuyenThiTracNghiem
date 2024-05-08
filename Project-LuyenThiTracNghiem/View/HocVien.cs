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
    public partial class HocVien : Form
    {
        private string maTaiKhoan;
        private string matKhau;
        private string maHocVien;
        private string tenHocVien;
        private string diaChi;
        private string email;
        private string soDienThoai;


        private DateTime selectedDate;
        private string selectedMaChuong;


        private string maChuong;
        private string tenChuong;
        public int IdMonhoc { get; set; }

        public MONHOC MONHOC { get; set; }

        public string MaChuong
        {
            get { return maChuong; }
            set { maChuong = value; }
        }

        public string TenChuong
        {
            get { return tenChuong; }
            set { tenChuong = value; }
        }

        public HocVien(string maTaiKhoan, string matKhau) : this()
        {
            this.maTaiKhoan = maTaiKhoan;
            this.matKhau = matKhau;

            LoadThongTinHocVien();
        }

        private void LoadThongTinHocVien()
        {
            using (LuyenThiTracNghiemEntities2 db = new LuyenThiTracNghiemEntities2())
            {
                var hocVien = db.HOCVIENs.FirstOrDefault(hv => hv.TenTaiKhoan == maTaiKhoan && hv.MatKhau == matKhau);

                if (hocVien != null)
                {
                    maHocVien = hocVien.MaHV;
                    tenHocVien = hocVien.TenHV;
                    diaChi = hocVien.Diachi;
                    email = hocVien.email;
                    soDienThoai = hocVien.SDT.ToString();

                    txtMaHV.Text = maHocVien;
                    txtTenHV.Text = tenHocVien;
                    txtDiaChi.Text = diaChi;
                    txtEmailHV.Text = email;
                    txtSoDienThoai.Text = soDienThoai;
                }
            }
        }

        public HocVien()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Hide();
            HocVienXemDiem hocvienXD = new HocVienXemDiem(maHocVien);
            hocvienXD.ShowDialog();
        }

        private void button3_Click(object sender, EventArgs e)
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

        private void btn_Giangvien_Click(object sender, EventArgs e)
        {

        }

        private void HocVien_Load(object sender, EventArgs e)
        {


            LoadMonHoc();
            LoadNgayThi();
            selectedDate = dtNgayThi.Value;

            // Gắn sự kiện cho cbMonThi và cbChuong
            cbMonThi.SelectedIndexChanged += cbMon_SelectedIndexChanged;
            cbChuong.SelectedIndexChanged += cbChuong_SelectedIndexChanged;
        }
       
       


        private void LoadNgayThi()
        {
            // Allow only future dates
            dtNgayThi.MinDate = DateTime.Today;
        }

        private void LoadMonHoc()
        {
            using (LuyenThiTracNghiemEntities2 db = new LuyenThiTracNghiemEntities2())
            {
                var danhSachMonHoc = db.MONHOCs.ToList();
                cbMonThi.DisplayMember = "Tenmonhoc";
                cbMonThi.ValueMember = "IdMonhoc";
                cbMonThi.DataSource = danhSachMonHoc;
            }
        }

        private void LoadChuong(int idMonhoc)
        {
            using (LuyenThiTracNghiemEntities2 db = new LuyenThiTracNghiemEntities2())
            {
                var danhSachChuong = db.QUANLYCHUONGs
                    .Where(ch => ch.IdMonhoc == idMonhoc)
                    .ToList();

                cbChuong.DisplayMember = "TenChuong";
                cbChuong.ValueMember = "MaChuong";
                cbChuong.DataSource = danhSachChuong;
            }
        }


        private void cbMon_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbMonThi.SelectedValue != null)
            {
                int idMonhoc = Convert.ToInt32(cbMonThi.SelectedValue);
                LoadChuong(idMonhoc); // Gọi phương thức để load danh sách chương khi môn học thay đổi
            }
        }

        private void cbChuong_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbChuong.SelectedIndex != -1)
            {
                selectedMaChuong = cbChuong.SelectedValue.ToString();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            selectedDate = dtNgayThi.Value;
            BaiThi formBaiThi = new BaiThi();
            formBaiThi.MaHV_BaiThi = maHocVien;
            formBaiThi.TenHV_BaiThi = tenHocVien;
            formBaiThi.NgayThi_BaiThi = selectedDate;

            if (!string.IsNullOrEmpty(selectedMaChuong))
            {
                // Sử dụng selectedMaChuong để lấy MaDe từ bảng DETHI
                // Thay thế điều này bằng logic của bạn để lấy MaDe dựa trên MaChuong
                string maDe = LayMaDe(selectedMaChuong);
                // Bắt đầu đếm thời gian khi nút "Làm Bài Thi" được ấn
              
                formBaiThi.MaDe_BaiThi = maDe;
            }

            // Hiển thị Form BaiThi
            formBaiThi.ShowDialog();
        }
        private string LayMaDe(string maChuong)
        {
            using (LuyenThiTracNghiemEntities2 db = new LuyenThiTracNghiemEntities2())
            {
                var maDe = db.DETHIs
                    .Where(d => d.MaChuong == maChuong)
                    .Select(d => d.Madethi)
                    .FirstOrDefault();

                return maDe ?? "MaDeMacDinh"; // Thay thế bằng giá trị mặc định hoặc xử lý khác nếu cần
            }
        }
    }
}
