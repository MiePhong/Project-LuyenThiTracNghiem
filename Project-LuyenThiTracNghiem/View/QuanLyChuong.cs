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
    public partial class QuanLyChuong : Form
    {
        public QuanLyChuong()
        {
            InitializeComponent();
            dataGridView3.Columns.Add("IDMonHoc", "Mã môn học");
            dataGridView3.Columns.Add("MaChuong", "Mã chương");
            dataGridView3.Columns.Add("TenChuong", "Tên chương");
           
        }

        private void btnMon_Click(object sender, EventArgs e)
        {
            QuanLyMon mon = new QuanLyMon();
            mon.ShowDialog();
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

        void LoadData() // xem hoc vien 
        {
            dataGridView3.Rows.Clear();
             LuyenThiTracNghiemEntities2 db = new LuyenThiTracNghiemEntities2();

            var result = from a in db.QUANLYCHUONGs
                         select new
                         {
                             ID_môn_học = a.IdMonhoc,
                             Mã_chương = a.MaChuong,
                             Tên_chương = a.TenChuong
                             
                         };
            
            }
        private void QuanLyChuong_Load(object sender, EventArgs e)
        {
            LoadComboBoxData();
            cbID.SelectedIndexChanged += cbID_SelectedIndexChanged;
            cbMaChuong.SelectedIndexChanged += cbMaChuong_SelectedIndexChanged;

            // Gọi phương thức LoadData khi form được load
            LoadData();
        }


        private void LoadComboBoxData()
        {
            using (LuyenThiTracNghiemEntities2 db = new LuyenThiTracNghiemEntities2())
            {
                // Lấy danh sách ID môn học từ bảng MONHOC
                var idMonHocList = db.MONHOCs.Select(m => m.IdMonhoc).ToList();

                // Thêm ID mới vào danh sách
                idMonHocList.Add(GetNewlyCreatedIdMonHoc());

                // Gán danh sách ID môn học cho ComboBox
                cbID.DataSource = idMonHocList;
            }
        }
        private int GetNewlyCreatedIdMonHoc()
        {
            // Đặt logic để trả về ID môn học mới được tạo (đọc từ bảng hoặc giả định)
            // Ví dụ: return 1000;
            return 0;
        }

        private void HienThiMaChuong(int idMonHoc)
        {
            using (LuyenThiTracNghiemEntities2 db = new LuyenThiTracNghiemEntities2())
            {
                // Lấy danh sách mã chương từ bảng QUANLYCHUONG dựa trên ID môn học
                var maChuongList = db.QUANLYCHUONGs
                    .Where(qc => qc.IdMonhoc == idMonHoc)
                    .Select(qc => qc.MaChuong)
                    .ToList();

                // Gán danh sách mã chương cho ComboBox cbMaChuong
                cbMaChuong.DataSource = maChuongList;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            LuyenThiTracNghiemEntities2 db = new LuyenThiTracNghiemEntities2();

            var result = from a in db.QUANLYCHUONGs
                         select new
                         {
                             MãMônHọc = a.IdMonhoc,
                             MãChương = a.MaChuong,
                             TênChương= a.TenChuong
                         };

            dataGridView3.Rows.Clear(); // Xóa tất cả các dòng trong dataGridView3

            foreach (var item in result)
            {
                dataGridView3.Rows.Add(item.MãMônHọc, item.MãChương, item.TênChương);
            }

            foreach (var monhoc in db.MONHOCs)
            {
                bool exists = db.QUANLYCHUONGs.Any(d => d.IdMonhoc == monhoc.IdMonhoc);
                if (!exists)
                {
                    dataGridView3.Rows.Add(monhoc.IdMonhoc, "", "");
                }
            }
        
        
        }

        private void cbID_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbID.SelectedItem != null)
            {
                // Lấy ID môn học từ ComboBox
                int selectedId = Convert.ToInt32(cbID.SelectedItem);

                // Hiển thị mã chương tương ứng
                HienThiMaChuong(selectedId);
            }
        }
        private void HienThiMaTenChuong(int idMonHoc)
        {
            using (LuyenThiTracNghiemEntities2 db = new LuyenThiTracNghiemEntities2())
            {
                // Lấy mã và tên chương từ database dựa trên ID môn học
                var quanLyChuong = db.QUANLYCHUONGs.Where(qc => qc.IdMonhoc == idMonHoc).FirstOrDefault();

                // Hiển thị thông tin trong TextBox
                if (quanLyChuong != null)
                {
                    cbMaChuong.Text = quanLyChuong.MaChuong;
                    cbTenChuong.Text = quanLyChuong.TenChuong;
                }
                else
                {
                    // Nếu không có thông tin, làm sạch TextBox
                    cbMaChuong.Text = string.Empty;
                    cbTenChuong.Text = string.Empty;
                }
            }
        }

        private void HienThiThongTinMonHoc(int Machuong)
        {

        }

        private void cbMaChuong_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Lấy ID môn học từ ComboBox
            int selectedId = Convert.ToInt32(cbID.SelectedItem);

            // Lấy mã chương từ ComboBox
            string selectedMaChuong = cbMaChuong.SelectedItem as string;

            // Hiển thị thông tin tương ứng trong TextBox
            HienThiTenChuong(selectedId, selectedMaChuong);
        }
        private void HienThiTenChuong(int idMonHoc, string maChuong)
        {
            using (LuyenThiTracNghiemEntities2 db = new LuyenThiTracNghiemEntities2())
            {
                // Lấy tên chương từ database dựa trên ID môn học và mã chương
                var tenChuong = db.QUANLYCHUONGs
                    .Where(qc => qc.IdMonhoc == idMonHoc && qc.MaChuong == maChuong)
                    .Select(qc => qc.TenChuong)
                    .FirstOrDefault();

                // Hiển thị thông tin trong TextBox
                cbTenChuong.Text = tenChuong ?? string.Empty;
            }
        }

        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            // Lấy giá trị đã chọn từ các ComboBox
            int selectedId = Convert.ToInt32(cbID.SelectedItem);
            string selectedMaChuong = cbMaChuong.SelectedItem as string;
            string selectedTenChuong = cbTenChuong.Text.Trim();

            // Kiểm tra xem đã chọn đủ thông tin chưa
            if (selectedId > 0 && !string.IsNullOrEmpty(selectedMaChuong) && !string.IsNullOrEmpty(selectedTenChuong))
            {
                // Thực hiện truy vấn dữ liệu từ cơ sở dữ liệu
                LoadDataMC(selectedId, selectedMaChuong);

                // Cập nhật lại TextBoxes (nếu cần)
                HienThiTenChuong(selectedId, selectedMaChuong);
            }
            else
            {
                MessageBox.Show("Vui lòng chọn đầy đủ thông tin để tìm kiếm.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
        private void HienThiThongTinMonHoc(int idMonHoc, string maChuong)
        {
            using (LuyenThiTracNghiemEntities2 db = new LuyenThiTracNghiemEntities2())
            {
                // Lấy tên chương từ database dựa trên ID môn học và mã chương
                var tenChuong = db.QUANLYCHUONGs
                    .Where(qc => qc.IdMonhoc == idMonHoc && qc.MaChuong == maChuong)
                    .Select(qc => qc.TenChuong)
                    .FirstOrDefault();

                // Hiển thị thông tin trong TextBox
                cbTenChuong.Text = tenChuong ?? string.Empty;
            }
        }

        private void LoadDataMC(int idMonHoc, string maChuong)
        {
            // Xóa tất cả các cột hiện có trong dataGridView3
            dataGridView3.Columns.Clear();

            // Thêm 3 cột vào dataGridView3
            dataGridView3.Columns.Add("MaChuong", "Mã chương");
            dataGridView3.Columns.Add("TenChuong", "Tên chương");
            dataGridView3.Columns.Add("IDMonHoc", "Mã môn học");

            using (LuyenThiTracNghiemEntities2 db = new LuyenThiTracNghiemEntities2())
            {
                var result = from a in db.QUANLYCHUONGs
                             where a.IdMonhoc == idMonHoc && a.MaChuong == maChuong
                             select new { Mã_chương = a.MaChuong, Tên_chương = a.TenChuong, ID_môn_học = a.IdMonhoc };

                // Thêm dòng mới từ kết quả tìm kiếm nếu có
                var firstRow = result.FirstOrDefault();
                if (firstRow != null)
                {
                    dataGridView3.Rows.Add(firstRow.Mã_chương, firstRow.Tên_chương, firstRow.ID_môn_học);
                }
            }
            }

        private void dataGridView3_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dataGridView3.Rows[e.RowIndex];

                // Assuming the column indices, modify them based on your actual column indices
                string idMonHoc = row.Cells["IDMonHoc"].Value.ToString(); // Cột IDMonHoc
                string maChuong = row.Cells["MaChuong"].Value.ToString();
                string tenChuong = row.Cells["TenChuong"].Value.ToString();

                // Display the values in text boxes
                txtIdMH.Text = idMonHoc;
                txtMaMonHoc.Text = maChuong;
                txtTenMonHoc.Text = tenChuong;
            }
        }

        private void button7_Click(object sender, EventArgs e)
        {
            // Kiểm tra xem đã chọn một dòng trong dataGridView3 chưa
            if (dataGridView3.SelectedRows.Count > 0)
            {
                // Hiển thị hộp thoại xác nhận
                DialogResult result = MessageBox.Show("Bạn có chắc chắn muốn xóa không?", "Xác nhận xóa", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                // Nếu người dùng chọn Yes, thực hiện xóa
                if (result == DialogResult.Yes)
                {
                    // Lấy mã chương của dòng được chọn
                    string machuong = dataGridView3.SelectedRows[0].Cells["MaChuong"].Value.ToString();

                    // Thực hiện xóa chương
                    XoaChuong(machuong);

                    // Sau khi xóa, cập nhật lại DataGridView và các TextBox
                    LoadData();
                    ClearTextBoxes();
                }
            }
            else
            {
                MessageBox.Show("Vui lòng chọn một dòng để xóa.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
        private void XoaChuong(string machuong)
        {
            using (LuyenThiTracNghiemEntities2 db = new LuyenThiTracNghiemEntities2())
            {
                // Tìm chương cần xóa
                var chuong = db.QUANLYCHUONGs.Find(machuong);

                if (chuong != null)
                {
                    // Xóa chương và lưu thay đổi vào cơ sở dữ liệu
                    db.QUANLYCHUONGs.Remove(chuong);
                    db.SaveChanges();

                    MessageBox.Show("Xóa chương thành công.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        private void ClearTextBoxes()
        {
            // Xóa nội dung trong các TextBox
            txtIdMH.Text = string.Empty;
            txtMaMonHoc.Text = string.Empty;
            txtTenMonHoc.Text = string.Empty;
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
        ;

            // Lấy thông tin từ các TextBox
            int idMonHoc = Convert.ToInt32(txtIdMH.Text.Trim());
            string maChuong = txtMaMonHoc.Text.Trim();
            string tenChuong = txtTenMonHoc.Text.Trim();

            // Thêm môn học mới
            ThemMonHoc(idMonHoc, maChuong, tenChuong);

            // Hiển thị lại danh sách môn học
            LoadData();

        }

        private bool KiemTraTonTaiMaChuong(string MaChuong)
        {
            using (LuyenThiTracNghiemEntities2 db = new LuyenThiTracNghiemEntities2())
            {
                return db.QUANLYCHUONGs.Any(c => c.MaChuong == MaChuong);
            }
        }

        private bool KiemTraTonTaiIdMonHoc(int idMonHoc)
        {
            using (LuyenThiTracNghiemEntities2 db = new LuyenThiTracNghiemEntities2())
            {
                return db.QUANLYCHUONGs.Any(c => c.IdMonhoc == idMonHoc);
            }
        }

        private bool KiemTraTonTaiTenChuong(string TenChuong)
        {
            using (LuyenThiTracNghiemEntities2 db = new LuyenThiTracNghiemEntities2())
            {
                return db.QUANLYCHUONGs.Any(c => c.TenChuong == TenChuong);
            }
        }

        private void ThemMonHoc(int idMonHoc, string maChuong, string tenChuong)
        {
            using (LuyenThiTracNghiemEntities2 db = new LuyenThiTracNghiemEntities2())
            {
                // Tạo một đối tượng QUANLYCHUONG mới
                QUANLYCHUONG chuong = new QUANLYCHUONG
                {
                    IdMonhoc = idMonHoc,
                    MaChuong = maChuong,
                    TenChuong = tenChuong,
                };

                // Thêm chương vào DbSet và lưu vào cơ sở dữ liệu
                db.QUANLYCHUONGs.Add(chuong);

                try
                {
                    db.SaveChanges();
                    MessageBox.Show("Thêm chương thành công.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
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

        private void btnChinh_Click(object sender, EventArgs e)
        {
            // Kiểm tra xem đã chọn một dòng trong dataGridView3 chưa
            if (dataGridView3.SelectedRows.Count > 0)
            {
                // Lấy thông tin từ dòng được chọn
                DataGridViewRow selectedRow = dataGridView3.SelectedRows[0];

                // Lấy các giá trị từ các ô của dòng được chọn
                string maChuong = selectedRow.Cells["MaChuong"].Value.ToString();
                string tenChuong = selectedRow.Cells["TenChuong"].Value.ToString();

                // Chuyển sang form chỉnh sửa và truyền các giá trị
                ChinhSuaChuong chinhSuaForm = new ChinhSuaChuong(maChuong, tenChuong);
                chinhSuaForm.ShowDialog();

                // Refresh lại DataGridView sau khi chỉnh sửa (nếu cần)
                LoadData();
            }
            else
            {
                MessageBox.Show("Vui lòng chọn một dòng để chỉnh sửa.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            QuanLyMaDeThi dethi = new QuanLyMaDeThi();
            dethi.ShowDialog();
        }

        private void cbTenChuong_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void dataGridView3_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }

        }

