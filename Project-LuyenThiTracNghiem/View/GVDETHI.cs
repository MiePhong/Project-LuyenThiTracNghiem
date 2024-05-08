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

    public partial class GVDETHI : Form
    {

        private string maChuong;
        private string tenChuong;
        private string selectedMaChuong;
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

        public GVDETHI()
        {
            InitializeComponent();
            dataGridView_GV.SelectionChanged += dataGridView_GV_SelectionChanged;
            LoadMonHoc();


            // Gắn sự kiện cho cbMonThi và cbChuong
            CBChonMon.SelectedIndexChanged += CBChonMon_SelectedIndexChanged;
            CBChonChuong.SelectedIndexChanged += CBChonChuong_SelectedIndexChanged;
            // Đặt kích thước mong muốn
            this.Size = new Size(1220, 480); // Thay đổi 800 và 600 thành kích thước mong muốn của bạn

            // Đặt cài đặt kích thước mặc định cho form
            this.FormBorderStyle = FormBorderStyle.Sizable;

            // Xóa giá trị MaximumSize nếu có
            this.MaximumSize = new Size(0, 0); // Giá trị (0, 0) có nghĩa là không có giới hạn về kích thước

            // Chuyển thuộc tính AutoScaleMode sang None
            this.AutoScaleMode = AutoScaleMode.None;
        }
        private void LoadMonHoc()
        {
            using (LuyenThiTracNghiemEntities2 db = new LuyenThiTracNghiemEntities2())
            {
                var danhSachMonHoc = db.MONHOCs.ToList();
                CBChonMon.DisplayMember = "Tenmonhoc";
                CBChonMon.ValueMember = "IdMonhoc";
                CBChonMon.DataSource = danhSachMonHoc;
            }
        }
        private void LoadData(string maDeThi)
        {
            LuyenThiTracNghiemEntities2 db = new LuyenThiTracNghiemEntities2();

            var result = from a in db.CAUHOIs
                         where a.Madethi == maDeThi
                         select new { MaCH = a.Macauhoi, NoidungCauhoi = a.Noidungcauhoi, NoidungCauA = a.NoidungcauA, NoidungCauB = a.NoidungcauB, NoidungCauC = a.NoidungcauC, NoidungCauD = a.NoidungcauD, DapAn = a.Dapan, MaDeThi = a.Madethi };
            dataGridView_GV.DataSource = result.ToList();
        }

        private void LoadChuong(int idMonhoc)
        {
            using (LuyenThiTracNghiemEntities2 db = new LuyenThiTracNghiemEntities2())
            {
                var danhSachChuong = db.QUANLYCHUONGs
                    .Where(ch => ch.IdMonhoc == idMonhoc)
                    .ToList();

                CBChonChuong.DisplayMember = "TenChuong";
                CBChonChuong.ValueMember = "MaChuong";
                CBChonChuong.DataSource = danhSachChuong;
            }
        }
        private void btnchuyenXDHV_Click(object sender, EventArgs e)
        {
            GiangVien gv = new GiangVien();
            gv.ShowDialog();
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

        private void GVDETHI_Load(object sender, EventArgs e)
        {

        }

        private void CBChonMon_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (CBChonMon.SelectedValue != null)
            {
                int idMonhoc = Convert.ToInt32(CBChonMon.SelectedValue);
                LoadChuong(idMonhoc); // Gọi phương thức để load danh sách chương khi môn học thay đổi
            }
        }
        private void LoadMaDe(string maChuong)
        {
            using (LuyenThiTracNghiemEntities2 db = new LuyenThiTracNghiemEntities2())
            {
                // Replace 'YourExamCodeTable' with the actual table or data structure storing exam codes
                var examCode = db.DETHIs
                    .Where(ec => ec.MaChuong == maChuong)
                    .Select(ec => ec.Madethi)
                    .FirstOrDefault();

                // Display the exam code in the txtMD TextBox
                txtMD.Text = examCode;
            }
        }
        private void CBChonChuong_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (CBChonChuong.SelectedIndex != -1)
            {
                selectedMaChuong = CBChonChuong.SelectedValue.ToString();
                LoadMaDe(selectedMaChuong);
            }
        }
        void LoadDataAll() // xem hoc vien 
        {
            LuyenThiTracNghiemEntities2 db = new LuyenThiTracNghiemEntities2();

            var result = from a in db.CAUHOIs select new { MaCH = a.Macauhoi, NoidungCauhoi = a.Noidungcauhoi, NoidungCauA = a.NoidungcauA, NoidungCauB = a.NoidungcauB, NoidungCauC = a.NoidungcauC, NoidungCauD = a.NoidungcauD, DapAn = a.Dapan, MaDeThi = a.Madethi };
            dataGridView_GV.DataSource = result.ToList();
        }
        private void btnXem_Click(object sender, EventArgs e)
        {
            LoadDataAll();
        }

        private void dataGridView_GV_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dataGridView_GV_SelectionChanged(object sender, EventArgs e)
        {
            if (dataGridView_GV.SelectedRows.Count > 0)
            {
                DataGridViewRow selectedRow = dataGridView_GV.SelectedRows[0];

                string maCH = selectedRow.Cells["MaCH"].Value.ToString();
                string noidungCauhoi = selectedRow.Cells["NoidungCauhoi"].Value.ToString();
                string noidungCauA = selectedRow.Cells["NoidungCauA"].Value.ToString();
                string noidungCauB = selectedRow.Cells["NoidungCauB"].Value.ToString();
                string noidungCauC = selectedRow.Cells["NoidungCauC"].Value.ToString();
                string noidungCauD = selectedRow.Cells["NoidungCauD"].Value.ToString();
                string dapAn = selectedRow.Cells["DapAn"].Value.ToString();
                string maDeThi = selectedRow.Cells["Madethi"].Value.ToString();

                txtGVCauHoi.Text = noidungCauhoi;
                rdA.Text = noidungCauA;
                rdB.Text = noidungCauB;
                rdC.Text = noidungCauC;
                rdD.Text = noidungCauD;

                switch (dapAn)
                {
                    case "A":
                        rdA.Checked = true;
                        break;
                    case "B":
                        rdB.Checked = true;
                        break;
                    case "C":
                        rdC.Checked = true;
                        break;
                    case "D":
                        rdD.Checked = true;
                        break;
                    default:
                        break;
                }

                txtMD.Text = maDeThi;
            }

        }

        private void btnxemch_Click(object sender, EventArgs e)
        {
            string selectedMaDeThi = txtMD.Text.Trim();
            LoadData(selectedMaDeThi);
        }

        private void btnThemCH_Click(object sender, EventArgs e)
        {
            string maCauHoiMoi = txtTaoMa.Text.Trim();
            string noiDungCauHoi = txtGVCauHoi.Text.Trim();
            string noiDungA = txtA.Text.Trim();
            string noiDungB = txtB.Text.Trim();
            string noiDungC = txtC.Text.Trim();
            string noiDungD = txtD.Text.Trim();
            string dapAn = GetSelectedDapAn();

            using (LuyenThiTracNghiemEntities2 db = new LuyenThiTracNghiemEntities2())
            {
                CAUHOI cauHoiMoi = new CAUHOI
                {
                    Macauhoi = maCauHoiMoi,
                    Noidungcauhoi = noiDungCauHoi,
                    NoidungcauA = noiDungA,
                    NoidungcauB = noiDungB,
                    NoidungcauC = noiDungC,
                    NoidungcauD = noiDungD,
                    Dapan = dapAn,
                    Madethi = txtMD.Text.Trim()
                };

                db.CAUHOIs.Add(cauHoiMoi);
                db.SaveChanges();

                MessageBox.Show("Câu hỏi đã được thêm thành công.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);

                txtGVCauHoi.Enabled = false;
                txtA.Enabled = false;
                txtB.Enabled = false;
                txtC.Enabled = false;
                txtD.Enabled = false;
                rdbtA.Enabled = false;
                rdbtB.Enabled = false;
                rdbtC.Enabled = false;
                rdbtD.Enabled = false;
                btnThemCH.Enabled = false;

                txtGVCauHoi.Text = string.Empty;
                txtA.Text = string.Empty;
                txtB.Text = string.Empty;
                txtC.Text = string.Empty;
                txtD.Text = string.Empty;
                rdbtA.Checked = false;
                rdbtB.Checked = false;
                rdbtC.Checked = false;
                rdbtD.Checked = false;
            }
        }

        private void btnChinhCH_Click(object sender, EventArgs e)
        {

            if (dataGridView_GV.SelectedRows.Count > 0)
            {
                DataGridViewRow selectedRow = dataGridView_GV.SelectedRows[0];

                string maCH = selectedRow.Cells["MaCH"].Value.ToString();
                string noidungCauhoi = selectedRow.Cells["NoidungCauhoi"].Value.ToString();
                string noidungCauA = selectedRow.Cells["NoidungCauA"].Value.ToString();
                string noidungCauB = selectedRow.Cells["NoidungCauB"].Value.ToString();
                string noidungCauC = selectedRow.Cells["NoidungCauC"].Value.ToString();
                string noidungCauD = selectedRow.Cells["NoidungCauD"].Value.ToString();
                string dapAn = selectedRow.Cells["DapAn"].Value.ToString();
                string maDeThi = selectedRow.Cells["Madethi"].Value.ToString();

                using (ChinhSuaCauHoi chinhSuaForm = new ChinhSuaCauHoi(maCH, noidungCauhoi, noidungCauA, noidungCauB, noidungCauC, noidungCauD, dapAn, maDeThi))
                {
                    if (chinhSuaForm.ShowDialog() == DialogResult.OK)
                    {
                        // Lấy giá trị chỉnh sửa từ form ChinhSuaCauHoiForm
                        noidungCauhoi = chinhSuaForm.NoidungCauhoi;
                        noidungCauA = chinhSuaForm.NoidungCauA;
                        noidungCauB = chinhSuaForm.NoidungCauB;
                        noidungCauC = chinhSuaForm.NoidungCauC;
                        noidungCauD = chinhSuaForm.NoidungCauD;
                        dapAn = chinhSuaForm.DapAn;

                        // Cập nhật lại dữ liệu trong DataGridView
                        selectedRow.Cells["NoidungCauhoi"].Value = noidungCauhoi;
                        selectedRow.Cells["NoidungCauA"].Value = noidungCauA;
                        selectedRow.Cells["NoidungCauB"].Value = noidungCauB;
                        selectedRow.Cells["NoidungCauC"].Value = noidungCauC;
                        selectedRow.Cells["NoidungCauD"].Value = noidungCauD;
                        selectedRow.Cells["DapAn"].Value = dapAn;

                        // Cập nhật lại dữ liệu trong CSDL (nếu cần)
                        // Lưu ý: Dưới đây là một hàm giả định, bạn cần sửa lại theo đúng cách lưu dữ liệu của bạn
                        UpdateCauHoiInDatabase(maCH, noidungCauhoi, noidungCauA, noidungCauB, noidungCauC, noidungCauD, dapAn, maDeThi);
                    }
                }
            }
            else
            {
                MessageBox.Show("Vui lòng chọn một câu hỏi để chỉnh sửa.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
        private void UpdateCauHoiInDatabase(string maCH, string noidungCauhoi, string noidungCauA, string noidungCauB, string noidungCauC, string noidungCauD, string dapAn, string maDeThi)
        {
            using (LuyenThiTracNghiemEntities2 db = new LuyenThiTracNghiemEntities2())
            {
                var cauHoi = db.CAUHOIs.FirstOrDefault(ch => ch.Macauhoi == maCH);
                if (cauHoi != null)
                {
                    cauHoi.Noidungcauhoi = noidungCauhoi;
                    cauHoi.NoidungcauA = noidungCauA;
                    cauHoi.NoidungcauB = noidungCauB;
                    cauHoi.NoidungcauC = noidungCauC;
                    cauHoi.NoidungcauD = noidungCauD;
                    cauHoi.Dapan = dapAn;

                    // Lưu thay đổi vào CSDL
                    db.SaveChanges();
                }
            }
        }
            private void btnTronCH_Click(object sender, EventArgs e)
            {
            TronCauHoi();
        }

            private void btnTao_Click(object sender, EventArgs e)
            {
                string maCauHoiMoi = txtTaoMa.Text.Trim();

                if (IsMaCauHoiExists(maCauHoiMoi))
                {
                    MessageBox.Show("Mã câu hỏi này đã tồn tại, vui lòng chọn mã khác.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else
                {
                    MessageBox.Show("Mã câu hỏi hợp lý, bạn có thể tiếp tục tạo.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    // Tiếp tục xử lý tạo câu hỏi nếu muốn
                }

                txtTaoMa.Text = maCauHoiMoi;

                txtGVCauHoi.Enabled = true;
                txtA.Enabled = true;
                txtB.Enabled = true;
                txtC.Enabled = true;
                txtD.Enabled = true;
                rdbtA.Enabled = true;
                rdbtB.Enabled = true;
                rdbtC.Enabled = true;
                rdbtD.Enabled = true;
                btnThemCH.Enabled = true;

                txtGVCauHoi.Text = string.Empty;
                txtA.Text = string.Empty;
                txtB.Text = string.Empty;
                txtC.Text = string.Empty;
                txtD.Text = string.Empty;
                rdbtA.Checked = false;
                rdbtB.Checked = false;
                rdbtC.Checked = false;
                rdbtD.Checked = false;
            } 

            // Hàm kiểm tra mã câu hỏi có tồn tại trong bảng CAUHOI hay không
            private bool IsMaCauHoiExists(string maCauHoi)
            {
                using (LuyenThiTracNghiemEntities2 db = new LuyenThiTracNghiemEntities2())
                {
                    return db.CAUHOIs.Any(ch => ch.Macauhoi == maCauHoi);
                }
            } 
            private string GetSelectedDapAn()
            {
                if (rdA.Checked)
                {
                    return "A";
                }
                else if (rdB.Checked)
                {
                    return "B";
                }
                else if (rdC.Checked)
                {
                    return "C";
                }
                else if (rdD.Checked)
                {
                    return "D";
                }
                else
                {
                    return string.Empty; // Bạn có thể xử lý trường hợp không có lựa chọn
                }
            }
        
            private string GetMaMonHoc()
            {
                if (CBChonMon.SelectedValue != null)
                {
                    return CBChonMon.SelectedValue.ToString();
                }
                else
                {
                    return string.Empty; // Bạn có thể xử lý trường hợp không có giá trị được chọn
                }
            }

        private void btnXoaCH_Click(object sender, EventArgs e)
        {
            if (dataGridView_GV.SelectedRows.Count > 0)
            {
                DialogResult result = MessageBox.Show("Bạn có chắc chắn muốn xóa câu hỏi này không?", "Xác nhận xóa", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (result == DialogResult.Yes)
                {
                    // Lấy mã câu hỏi từ dòng được chọn
                    DataGridViewRow selectedRow = dataGridView_GV.SelectedRows[0];
                    string maCauHoi = selectedRow.Cells["MaCH"].Value.ToString();

                    // Thực hiện xóa câu hỏi khỏi cơ sở dữ liệu
                    XoaCauHoi(maCauHoi);

                    // Cập nhật DataGridView
                    LoadDataAll();
                }
            }
            else
            {
                MessageBox.Show("Vui lòng chọn một câu hỏi để xóa.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        private void XoaCauHoi(string maCauHoi)
        {
            using (LuyenThiTracNghiemEntities2 db = new LuyenThiTracNghiemEntities2())
            {
                // Tìm câu hỏi cần xóa
                CAUHOI cauHoi = db.CAUHOIs.Find(maCauHoi);

                if (cauHoi != null)
                {
                    // Xóa câu hỏi khỏi DbSet và cơ sở dữ liệu
                    db.CAUHOIs.Remove(cauHoi);
                    db.SaveChanges();
                }
            }

        }
        private void TronCauHoi()
        {
            using (LuyenThiTracNghiemEntities2 db = new LuyenThiTracNghiemEntities2())
            {
                // Lấy mã đề từ TextBox txtMD
                string maDeThi = txtMD.Text.Trim();

                // Lấy danh sách câu hỏi thuộc về đề thi có mã là maDeThi
                List<CAUHOI> danhSachCauHoi = db.CAUHOIs
                    .Where(ch => ch.Madethi == maDeThi)
                    .ToList();

                // Trộn câu hỏi bằng cách sử dụng hàm TronNgauNhien
                danhSachCauHoi = TronNgauNhien(danhSachCauHoi);

                // Hiển thị câu hỏi trong DataGridView
                dataGridView_GV.DataSource = danhSachCauHoi;
            }
        }

        private List<T> TronNgauNhien<T>(List<T> danhSach)
        {
            Random rand = new Random();
            int n = danhSach.Count;

            // Áp dụng giải thuật trộn ngẫu nhiên Fisher-Yates
            for (int i = n - 1; i > 0; i--)
            {
                int j = rand.Next(0, i + 1);

                // Hoán đổi vị trí giữa phần tử thứ i và j
                T temp = danhSach[i];
                danhSach[i] = danhSach[j];
                danhSach[j] = temp;
            }

            return danhSach;
        }

        private void dataGridView_GV_CellContentClick_1(object sender, DataGridViewCellEventArgs e)
        {

        }
    } }
