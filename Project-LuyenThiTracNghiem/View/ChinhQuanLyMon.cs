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
    public partial class ChinhQuanLyMon : Form
    {
        private int idMonHoc;
        
        public ChinhQuanLyMon()
        {
            InitializeComponent();
            
        }
        // Hàm khởi tạo có thể dùng để truyền dữ liệu từ form gọi
        public ChinhQuanLyMon(int idMonHoc)
        {
            InitializeComponent();

            // Lưu idMonHoc để sử dụng trong form
            this.idMonHoc = idMonHoc;

            // Load dữ liệu để chỉnh sửa
            LoadDataForEdit();
        }

        private void ChinhQuanLyMon_Load(object sender, EventArgs e)
        {
            LoadDataForEdit();
        }
        private void LoadDataForEdit()
        {
            using (LuyenThiTracNghiemEntities2 db = new LuyenThiTracNghiemEntities2())
            {
                // Lấy thông tin môn học từ database dựa trên ID
                var monHoc = db.MONHOCs.FirstOrDefault(mh => mh.IdMonhoc == idMonHoc);

                if (monHoc != null)
                {
                    // Hiển thị thông tin môn học trong các control
                    txtMaMonHoc.Text = monHoc.Mamonhoc;
                    txtTenMonHoc.Text = monHoc.Tenmonhoc;
                }
            }
        }

       

        private bool ValidateInput()
        {
            // Đây là nơi bạn kiểm tra các điều kiện hợp lệ trước khi lưu
            // Ví dụ: Kiểm tra xem các TextBox có rỗng không, kiểm tra định dạng, ...

            if (string.IsNullOrWhiteSpace(txtMaMonHoc.Text) || string.IsNullOrWhiteSpace(txtTenMonHoc.Text))
            {
                MessageBox.Show("Vui lòng nhập đầy đủ thông tin.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            return true;
        }

        private void LuuThayDoi()
        {
            using (LuyenThiTracNghiemEntities2 db = new LuyenThiTracNghiemEntities2())
            {
                // Lấy thông tin môn học từ database dựa trên ID
                var monHoc = db.MONHOCs.FirstOrDefault(mh => mh.IdMonhoc == idMonHoc);

                if (monHoc != null)
                {
                    // Cập nhật thông tin môn học dựa trên dữ liệu từ các control
                    monHoc.Mamonhoc = txtMaMonHoc.Text.Trim();
                    monHoc.Tenmonhoc = txtTenMonHoc.Text.Trim();

                    // Lưu thay đổi vào cơ sở dữ liệu
                    db.SaveChanges();
                }
            }
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            if (ValidateInput())
            {
                LuuThayDoi();
               
                DialogResult = DialogResult.OK;
                Close();

                // Hiển thị MessageBox khi lưu thành công
                MessageBox.Show("Lưu thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
    }
}
