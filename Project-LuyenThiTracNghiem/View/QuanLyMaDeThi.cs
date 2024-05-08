using Project_LuyenThiTracNghiem.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Entity.Infrastructure;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Project_LuyenThiTracNghiem.View
{
    public partial class QuanLyMaDeThi : Form
    {
        public QuanLyMaDeThi()
        {
            InitializeComponent();
            // Khởi tạo cột cho DataGridView
            dataGridView4.Columns.Add("IdMonhoc", "ID môn học");
            dataGridView4.Columns.Add("MaChuong", "Mã chương");
            dataGridView4.Columns.Add("Madethi", "Mã đề thi");
        }

        private void btn_QuanLyChuonng_Click(object sender, EventArgs e)
        {
            QuanLyChuong chuong = new QuanLyChuong();
            chuong.ShowDialog();
        }

        private void btnMon_Click(object sender, EventArgs e)
        {
            QuanLyMon mon = new QuanLyMon();
            mon.ShowDialog();
        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {

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

        private void btnXemAll_Click(object sender, EventArgs e)
        {

            LuyenThiTracNghiemEntities2 db = new LuyenThiTracNghiemEntities2();

            // Lấy dữ liệu từ bảng DETHIs
            var result = from a in db.DETHIs
                         select new
                         {
                             IDMônHọc = a.IdMonhoc,
                             MãChương = a.MaChuong,
                             MãĐềThi = a.Madethi
                         };


            dataGridView4.Rows.Clear(); // Xóa tất cả các dòng trong dataGridView4

            // Hiển thị dữ liệu từ bảng DETHIs
            foreach (var item in result)
            {
                dataGridView4.Rows.Add(item.IDMônHọc, item.MãChương, item.MãĐềThi);
            }

            // Hiển thị dữ liệu từ bảng MONHOCs không có bản ghi tương ứng trong DETHIs
            foreach (var id in db.MONHOCs)
            {
                bool exists = db.DETHIs.Any(d => d.IdMonhoc == id.IdMonhoc);
                if (!exists)
                {
                    dataGridView4.Rows.Add(id.IdMonhoc, "", ""); // Hiển thị MãChương trống
                }
            }

            // Hiển thị dữ liệu từ bảng CHUONGs không có bản ghi tương ứng trong DETHIs
            foreach (var chuong in db.QUANLYCHUONGs)
            {
                bool exists = db.DETHIs.Any(q => q.MaChuong == chuong.MaChuong);
                if (!exists)
                {
                    dataGridView4.Rows.Add(chuong.IdMonhoc, chuong.MaChuong, ""); // Hiển thị IdMonhoc với MaChuong tương ứng
                }
            }
        
    }

        private void QuanLyMaDeThi_Load(object sender, EventArgs e)
        {
            // Load dữ liệu ban đầu cho ComboBox cbMaMHoc
            LoadMaMonHocData();
        }
        private void LoadMaMonHocData()
        {
            using (LuyenThiTracNghiemEntities2 db = new LuyenThiTracNghiemEntities2())
            {
                // Lấy danh sách các giá trị duy nhất của MaMonHoc từ cơ sở dữ liệu
                var maMonHocList = db.MONHOCs.Select(monhoc => monhoc.IdMonhoc.ToString()).ToList();

                // Load dữ liệu vào ComboBox
                cbMaMHoc.DataSource = maMonHocList;
            }
        }
        private void cbMaMHoc_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbMaMHoc.SelectedItem != null)
            {
                Console.WriteLine("Selected Item: " + cbMaMHoc.SelectedItem.ToString());

                // Load dữ liệu MaChuong dựa trên MaMonHoc đã chọn
                string selectedMaMonHoc = cbMaMHoc.SelectedItem.ToString();
                LoadMaChuongData(selectedMaMonHoc);
            }


        }
        private void LoadMaChuongData(string selectedMaMonHoc)
        {
            using (LuyenThiTracNghiemEntities2 db = new LuyenThiTracNghiemEntities2())
            {
                // Convert selectedMaMonHoc to int? for comparison
                int? selectedMonHoc = int.TryParse(selectedMaMonHoc, out int parsedValue) ? parsedValue : (int?)null;

                // Lấy danh sách các giá trị duy nhất của MaChuong dựa trên MaMonHoc đã chọn
                var maChuongList = db.DETHIs
                    .Where(d => d.IdMonhoc == selectedMonHoc)
                    .Select(d => d.MaChuong)
                    .Distinct()
                    .ToList();

                // Load dữ liệu vào ComboBox
                cbMaChuongMD.DataSource = maChuongList;
            }
        }
        private void cbMaChuongMD_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbMaMHoc.SelectedItem != null && cbMaChuongMD.SelectedItem != null)
            {
                // Load dữ liệu MaDe dựa trên MaMonHoc và MaChuong đã chọn
                string selectedMaMonHoc = cbMaMHoc.SelectedItem.ToString();
                string selectedMaChuong = cbMaChuongMD.SelectedItem.ToString();
                HienThiMaDe(selectedMaMonHoc, selectedMaChuong);
            }
        }
        private void HienThiMaDe(string selectedMaMonHoc, string selectedMaChuong)
        {
            using (LuyenThiTracNghiemEntities2 db = new LuyenThiTracNghiemEntities2())
            {
                var maDe = db.DETHIs
                    .Where(d => d.IdMonhoc.ToString() == selectedMaMonHoc && d.MaChuong == selectedMaChuong) // Compare strings
                    .Select(d => d.Madethi)
                    .FirstOrDefault();

                // Hiển thị giá trị MaDe trong TextBox
                txtMDTHI.Text = maDe ?? string.Empty;
            }
        }


        private void btnTimKiemMaDe_Click(object sender, EventArgs e)
        {
            // Thực hiện tìm kiếm dựa trên giá trị đã chọn trong ComboBox
            if (cbMaMHoc.SelectedItem != null && cbMaChuongMD.SelectedItem != null)
            {
                string selectedMaMonHoc = cbMaMHoc.SelectedItem.ToString();
                string selectedMaChuong = cbMaChuongMD.SelectedItem.ToString();

                // Load dữ liệu dựa trên giá trị đã chọn
                LoadDataMC(selectedMaMonHoc, selectedMaChuong);
            }
            else
            {
                MessageBox.Show("Vui lòng chọn đầy đủ thông tin để tìm kiếm.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void txtMDTHI_TextChanged(object sender, EventArgs e)
        {

        }
        private void LoadDataMC(string selectedMaMonHoc, string selectedMaChuong)
        {
            using (LuyenThiTracNghiemEntities2 db = new LuyenThiTracNghiemEntities2())
            {
                // Lấy dữ liệu dựa trên MaMonHoc và MaChuong đã chọn
                var result = from a in db.DETHIs
                             where a.IdMonhoc.ToString() == selectedMaMonHoc && a.MaChuong == selectedMaChuong // Compare strings
                             select new
                             {
                                 IdMonhoc = a.IdMonhoc,
                                 MaChuong = a.MaChuong,
                                 MaDeThi = a.Madethi
                             };

                // Xóa dữ liệu trước đó trong DataGridView
                dataGridView4.Rows.Clear();

                // Hiển thị dữ liệu trong DataGridView
                foreach (var item in result)
                {
                    dataGridView4.Rows.Add(item.IdMonhoc, item.MaChuong, item.MaDeThi);
                }
            }
        }
            private void dataGridView4_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dataGridView4.Rows[e.RowIndex];

                // Giả sử các chỉ số cột, sửa chúng dựa trên chỉ số cột thực tế của bạn
                string IdMonHoc = row.Cells["IdMonhoc"].Value.ToString(); // Cột Mamonhoc
                string maChuong = row.Cells["MaChuong"].Value.ToString();
                string maDeThi = row.Cells["Madethi"].Value.ToString();

                // Hiển thị các giá trị trong các ô văn bản
                txtIdMHMD.Text = IdMonHoc;
                txtMaChuong.Text = maChuong;
                txtMD.Text = maDeThi;
            }
        }

        private void btnXoaMD_Click(object sender, EventArgs e)
        {
            // Kiểm tra xem đã chọn một dòng trong dataGridView3 chưa
            if (dataGridView4.SelectedRows.Count > 0)
            {
                // Hiển thị hộp thoại xác nhận
                DialogResult result = MessageBox.Show("Bạn có chắc chắn muốn xóa không?", "Xác nhận xóa", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                // Nếu người dùng chọn Yes, thực hiện xóa
                if (result == DialogResult.Yes)
                {
                    // Lấy mã đề thi của dòng được chọn
                    string maDeThi = dataGridView4.SelectedRows[0].Cells["Madethi"].Value.ToString();

                    XoaMaDe(maDeThi);

                    // Sau khi xóa, cập nhật lại DataGridView và các TextBox
                    ClearTextBoxes();

                }

            }

            }
        private void ClearTextBoxes()
        {
            // Xóa nội dung trong các TextBox
            txtIdMHMD.Text = string.Empty;
            txtMaChuong.Text = string.Empty;
            txtMD.Text = string.Empty;
        }
        private void XoaMaDe(string maDeThi)
            {
                using (LuyenThiTracNghiemEntities2 db = new LuyenThiTracNghiemEntities2())
                {
                    // Tìm đề cần xóa
                    var de = db.DETHIs.Find(maDeThi);

                    if (de != null)
                    {
                        // Xóa chương và lưu thay đổi vào cơ sở dữ liệu
                        db.DETHIs.Remove(de);
                        db.SaveChanges();

                        MessageBox.Show("Xóa chương thành công.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }

        private void btnThemMD_Click(object sender, EventArgs e)
        {
            // Lấy thông tin từ các TextBox
            int idMonHoc = Convert.ToInt32(txtIdMHMD.Text.Trim());
            string maChuong = txtMaChuong.Text.Trim();
            string maDeThi = txtMD.Text.Trim();

            // Thêm môn học mới
            ThemMonHoc(idMonHoc, maChuong, maDeThi);
            LuyenThiTracNghiemEntities2 db = new LuyenThiTracNghiemEntities2();

            // Lấy dữ liệu từ bảng DETHIs
            var result = from a in db.DETHIs
                         select new
                         {
                             IDMônHọc = a.IdMonhoc,
                             MãChương = a.MaChuong,
                             MãĐềThi = a.Madethi
                         };


            dataGridView4.Rows.Clear(); // Xóa tất cả các dòng trong dataGridView4

            // Hiển thị dữ liệu từ bảng DETHIs
            foreach (var item in result)
            {
                dataGridView4.Rows.Add(item.IDMônHọc, item.MãChương, item.MãĐềThi);
            }

            // Hiển thị dữ liệu từ bảng MONHOCs không có bản ghi tương ứng trong DETHIs
            foreach (var id in db.MONHOCs)
            {
                bool exists = db.DETHIs.Any(d => d.IdMonhoc == id.IdMonhoc);
                if (!exists)
                {
                    dataGridView4.Rows.Add(id.IdMonhoc, "", ""); // Hiển thị MãChương trống
                }
            }

            // Hiển thị dữ liệu từ bảng CHUONGs không có bản ghi tương ứng trong DETHIs
            foreach (var chuong in db.QUANLYCHUONGs)
            {
                bool exists = db.DETHIs.Any(q => q.MaChuong == chuong.MaChuong);
                if (!exists)
                {
                    dataGridView4.Rows.Add(chuong.IdMonhoc, chuong.MaChuong, ""); // Hiển thị IdMonhoc với MaChuong tương ứng
                }
            }
        }
        private void ThemMonHoc(int idMonHoc, string maChuong, string maDeThi)
        {
            using (LuyenThiTracNghiemEntities2 db = new LuyenThiTracNghiemEntities2())
            {
                // Tạo một đối tượng QUANLYCHUONG mới
                DETHI de = new DETHI
                {
                    IdMonhoc = idMonHoc,
                    MaChuong = maChuong,
                    Madethi = maDeThi,
                };

                // Thêm chương vào DbSet và lưu vào cơ sở dữ liệu
                db.DETHIs.Add(de);

                try
                {
                    db.SaveChanges();
                    MessageBox.Show("Thêm mã đề thành công.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (DbUpdateException ex)
                {
                    Exception innerException = ex.InnerException;

                    if (innerException != null && !string.IsNullOrEmpty(innerException.Message))
                    {
                        MessageBox.Show($"Lỗi: {innerException.Message}", "Lỗi Cơ sở dữ liệu", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    else
                    {
                        MessageBox.Show("Có lỗi xảy ra khi lưu vào cơ sở dữ liệu.", "Lỗi Cơ sở dữ liệu", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void btnChinhMD_Click(object sender, EventArgs e)
        {
            
        }
    }
    
}


