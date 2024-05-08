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
    public partial class QuanLyMon : Form
    {
        
        public QuanLyMon()
        {
            InitializeComponent();
        }
        void LoadData() // xem hoc vien 
        {
            LuyenThiTracNghiemEntities2 db = new LuyenThiTracNghiemEntities2();

            var result = from a in db.MONHOCs select new { ID_môn_học = a.IdMonhoc, Mã_môn_học = a.Mamonhoc, Tên_môn_học = a.Tenmonhoc };
            dataGridView1.DataSource = result.ToList();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            QuanLyChuong chuong = new QuanLyChuong();
            chuong.ShowDialog();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Hide();
            Admin ad = new Admin();
            ad.ShowDialog();
        }
        private void CBIdMon_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Lấy ID môn học từ ComboBox
            int selectedId = Convert.ToInt32(cbIdMocHoc.SelectedItem);

            // Hiển thị thông tin tương ứng trong các TextBox
            HienThiThongTinMonHoc(selectedId);
        }

        private void HienThiThongTinMonHoc(int idMonHoc)
        {
            using (LuyenThiTracNghiemEntities2 db = new LuyenThiTracNghiemEntities2())
            {
                // Lấy thông tin môn học từ database
                var monHoc = db.MONHOCs.FirstOrDefault(mh => mh.IdMonhoc == idMonHoc);

                // Hiển thị thông tin trong các TextBox
                if (monHoc != null)
                {
                    txtMaMH.Text = monHoc.Mamonhoc;
                    txtTenMH.Text = monHoc.Tenmonhoc;
                }
                else
                {
                    // Nếu không tìm thấy môn học, xóa nội dung trong TextBox
                    txtMaMH.Text = string.Empty;
                    txtTenMH.Text = string.Empty;
                }

                // Hiển thị thông tin môn học tại datagridview
                var result = from a in db.MONHOCs select new { ID_môn_học = a.IdMonhoc, Mã_môn_học = a.Mamonhoc, Tên_môn_học = a.Tenmonhoc };
                dataGridView1.DataSource = result.ToList();
            }
        }
        private void QuanLyMon_Load(object sender, EventArgs e)
        {
            LoadComboBoxData();
            dataGridView1.SelectionChanged += dataGridView1_SelectionChanged;

        }

        private void btnXemMonHoc_Click(object sender, EventArgs e)
        {
            LoadData();
        }
        private void LoadComboBoxData()
        {
            using (LuyenThiTracNghiemEntities2 db = new LuyenThiTracNghiemEntities2())
            {
                // Lấy danh sách ID môn học
                var idMonHocList = db.MONHOCs.Select(mh => mh.IdMonhoc).ToList();

                // Gán danh sách ID môn học cho ComboBox
                cbIdMocHoc.DataSource = idMonHocList;
            }
        }
        private void button1_Click(object sender, EventArgs e)
        {

            // Lấy ID môn học từ ComboBox
            int selectedId = Convert.ToInt32(cbIdMocHoc.SelectedItem);

            // Hiển thị thông tin môn học
            HienThiThongTinMonHoc(selectedId);

            // Hiển thị thông tin môn học tại datagridview
            LoadDataMH(selectedId);

        }
        private void LoadDataMH(int IDMonHoc)
        {
            using (LuyenThiTracNghiemEntities2 db = new LuyenThiTracNghiemEntities2())
            {
                var result = from a in db.MONHOCs
                             where a.IdMonhoc == IDMonHoc
                             select new { Id_môn_học = a.IdMonhoc, Mã_môn_học = a.Mamonhoc, Tên_môn_học = a.Tenmonhoc };
                dataGridView1.DataSource = result.ToList();
            }
        }

        private void button7_Click(object sender, EventArgs e)
        {

        }
       

        private void btnThem_Click(object sender, EventArgs e)
        {
            // Lấy thông tin từ các TextBox
            int idMonHoc = Convert.ToInt32(txtIdMH.Text.Trim());
            string maMonHoc = txtMaMonHoc.Text.Trim();
            string tenMonHoc = txtTenMonHoc.Text.Trim();

            // Kiểm tra mã môn học đã tồn tại chưa
            if (KiemTraTonTaiMaMonHoc(maMonHoc))
            {
                MessageBox.Show("Mã môn học đã tồn tại.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Kiểm tra ID môn học đã tồn tại chưa
            if (KiemTraTonTaiIdMonHoc(idMonHoc))
            {
                MessageBox.Show("ID môn học đã tồn tại.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Kiểm tra tên môn học đã tồn tại chưa
            if (KiemTraTonTaiTenMonHoc(tenMonHoc))
            {
                MessageBox.Show("Tên môn học đã tồn tại.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Thêm môn học mới
            ThemMonHoc(idMonHoc, maMonHoc, tenMonHoc);

            // Hiển thị lại danh sách môn học
            LoadData();
        }
        private bool KiemTraTonTaiMaMonHoc(string maMonHoc)
        {
            using (LuyenThiTracNghiemEntities2 db = new LuyenThiTracNghiemEntities2())
            {
                return db.MONHOCs.Any(mh => mh.Mamonhoc == maMonHoc);
            }
        }

        private bool KiemTraTonTaiIdMonHoc(int idMonHoc)
        {
            using (LuyenThiTracNghiemEntities2 db = new LuyenThiTracNghiemEntities2())
            {
                return db.MONHOCs.Any(mh => mh.IdMonhoc == idMonHoc);
            }
        }

        private bool KiemTraTonTaiTenMonHoc(string tenMonHoc)
        {
            using (LuyenThiTracNghiemEntities2 db = new LuyenThiTracNghiemEntities2())
            {
                return db.MONHOCs.Any(mh => mh.Tenmonhoc == tenMonHoc);
            }
        }

        private void ThemMonHoc(int idMonHoc, string maMonHoc, string tenMonHoc)
        {
            using (LuyenThiTracNghiemEntities2 db = new LuyenThiTracNghiemEntities2())
            {
                // Tạo một đối tượng MONHOC mới
                MONHOC monHoc = new MONHOC
                {
                    IdMonhoc = idMonHoc,
                    Mamonhoc = maMonHoc,
                    Tenmonhoc = tenMonHoc
                };

                // Thêm môn học vào DbSet và lưu vào cơ sở dữ liệu
                db.MONHOCs.Add(monHoc);
                db.SaveChanges();

                MessageBox.Show("Thêm môn học thành công.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void button7_Click_1(object sender, EventArgs e)
        {
            // Kiểm tra xem đã chọn một dòng trong dataGridView1 chưa
            if (dataGridView1.SelectedRows.Count > 0)
            {
                // Hiển thị hộp thoại xác nhận
                DialogResult result = MessageBox.Show("Bạn có chắc chắn muốn xóa không?", "Xác nhận xóa", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                // Nếu người dùng chọn Yes, thực hiện xóa
                if (result == DialogResult.Yes)
                {
                    // Lấy ID môn học của dòng được chọn
                    int idMonHoc = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells["ID_môn_học"].Value);

                    // Thực hiện xóa môn học với idMonHoc tương ứng
                    XoaMonHoc(idMonHoc);

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
        private void XoaMonHoc(int idMonHoc)
        {
            using (LuyenThiTracNghiemEntities2 db = new LuyenThiTracNghiemEntities2())
            {
                // Tìm môn học cần xóa
                var monHoc = db.MONHOCs.Find(idMonHoc);

                if (monHoc != null)
                {
                    // Xóa môn học và lưu thay đổi vào cơ sở dữ liệu
                    db.MONHOCs.Remove(monHoc);
                    db.SaveChanges();

                    MessageBox.Show("Xóa môn học thành công.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
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

        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
          
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow selectedRow = dataGridView1.Rows[e.RowIndex];

                int idMonHoc = Convert.ToInt32(selectedRow.Cells["ID_môn_học"].Value);
                string maMonHoc = selectedRow.Cells["Mã_môn_học"].Value.ToString();
                string tenMonHoc = selectedRow.Cells["Tên_môn_học"].Value.ToString();

                // Hiển thị thông tin tương ứng trong TextBox
                txtIdMH.Text = idMonHoc.ToString();
                txtMaMonHoc.Text = maMonHoc;
                txtTenMonHoc.Text = tenMonHoc;
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            // Kiểm tra xem đã chọn một dòng trong dataGridView1 chưa
            if (dataGridView1.SelectedRows.Count > 0)
            {
                // Lấy ID môn học của dòng được chọn
                int idMonHoc = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells["ID_môn_học"].Value);

                // Tạo một đối tượng ChinhSuaMonForm và chuyển thông tin môn học sang form đó
                using (ChinhQuanLyMon chinhSua = new ChinhQuanLyMon(idMonHoc))
                {
                    // Mở form ChinhSuaMonForm để chỉnh sửa thông tin
                    DialogResult result = chinhSua.ShowDialog();

                    // Kiểm tra xem người dùng đã xác nhận chỉnh sửa hay không
                    if (result == DialogResult.OK)
                    {
                        // Sau khi chỉnh sửa, cập nhật lại DataGridView và các TextBox
                        LoadData();
                        ClearTextBoxes();
                    }
                }
            }
            else
            {
                MessageBox.Show("Vui lòng chọn một dòng để chỉnh sửa.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        
    }

        private void cbIdMocHoc_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {
            QuanLyMaDeThi dethi = new QuanLyMaDeThi();
            dethi.ShowDialog();
        }
    }
}
