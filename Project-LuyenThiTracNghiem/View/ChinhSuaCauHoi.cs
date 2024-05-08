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
    public partial class ChinhSuaCauHoi : Form
    {
        public string NoidungCauhoi { get; set; }
        public string NoidungCauA { get; set; }
        public string NoidungCauB { get; set; }
        public string NoidungCauC { get; set; }
        public string NoidungCauD { get; set; }
        public string DapAn { get; set; }
        public ChinhSuaCauHoi(string maCH, string noidungCauhoi, string noidungCauA, string noidungCauB, string noidungCauC, string noidungCauD, string dapAn, string maDeThi)
        {
            InitializeComponent();
            txtMaCH.Text = maCH;
            txtChinhCauHoi.Text = noidungCauhoi;
            txtchinhA.Text = noidungCauA;
            txtchinhB.Text = noidungCauB;
            txtchinhC.Text = noidungCauC;
            txtchinhD.Text = noidungCauD;
            switch (dapAn)
            {
                case "A":
                    radioA.Checked = true;
                    break;
                case "B":
                    radioB.Checked = true;
                    break;
                case "C":
                    radioC.Checked = true;
                    break;
                case "D":
                    radioD.Checked = true;
                    break;
                default:
                    break;
            }

            btnLuu.Click += (sender, e) =>
            {
                NoidungCauhoi = txtChinhCauHoi.Text.Trim();
                NoidungCauA = txtchinhA.Text.Trim();
                NoidungCauB = txtchinhB.Text.Trim();
                NoidungCauC = txtchinhC.Text.Trim();
                NoidungCauD = txtchinhD.Text.Trim();
                DapAn = GetSelectedDapAn();

                DialogResult = DialogResult.OK;
                this.Close();
            };
        }


        private void ChinhSuaCauHoi_Load(object sender, EventArgs e)
        {

        }
          private string GetSelectedDapAn()
    {
        if (radioA.Checked)
        {
            return "A";
        }
        else if (radioB.Checked)
        {
            return "B";
        }
        else if (radioC.Checked)
        {
            return "C";
        }
        else if (radioD.Checked)
        {
            return "D";
        }
        else
        {
            return string.Empty;
        }
    }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            // Thực hiện các kiểm tra bổ sung nếu cần

            if (IsValidData()) // Ví dụ: Thực hiện phương thức IsValidData() để kiểm tra hợp lệ
            {
                // Lưu các thay đổi vào cơ sở dữ liệu hoặc thực hiện các hành động khác cần thiết
                LuuThayDoi();

                MessageBox.Show("Lưu thành công.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);

                // Đóng biểu mẫu
                DialogResult = DialogResult.OK;
                this.Close();
            }
            else
            {
                MessageBox.Show("Vui lòng điền đầy đủ thông tin và chọn đáp án.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
            // Phương thức kiểm tra hợp lệ, bạn có thể tùy chỉnh nó dựa trên yêu cầu cụ thể của mình
            private bool IsValidData()
            {
                if (string.IsNullOrWhiteSpace(txtChinhCauHoi.Text) || string.IsNullOrWhiteSpace(txtchinhA.Text)
                    || string.IsNullOrWhiteSpace(txtchinhB.Text) || string.IsNullOrWhiteSpace(txtchinhC.Text)
                    || string.IsNullOrWhiteSpace(txtchinhD.Text) || string.IsNullOrWhiteSpace(GetSelectedDapAn()))
                {
                    return false;
                }

                // Thêm nhiều điều kiện khác nếu cần

                return true;
            }

            // Phương thức ví dụ để lưu các thay đổi vào cơ sở dữ liệu
            private void LuuThayDoi()
            {
                
            }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }
    }
    }
    

