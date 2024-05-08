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
    public partial class BaiThi : Form
    {

        private int currentQuestionIndex = 0;
        private List<CCauHoi> danhSachCauHoi;
        private string[] cauTraLoiHocVien;

        private int Minutes = 45;
        private int Seconds = 0;

        public string MaHV_BaiThi { get; set; }
        public string TenHV_BaiThi { get; set; }
        public DateTime NgayThi_BaiThi { get; set; }
        public string MaDe_BaiThi { get; set; }





        public BaiThi()
        {
            InitializeComponent();




            timer_baithi.Start();

            this.Size = new System.Drawing.Size(720, 650);
            this.FormBorderStyle = FormBorderStyle.Sizable;
            this.MaximumSize = new System.Drawing.Size(0, 0);
            this.AutoScaleMode = AutoScaleMode.None;
        }
        private void LoadData()
        {
            danhSachCauHoi = new List<CCauHoi>();

            using (LuyenThiTracNghiemEntities2 db = new LuyenThiTracNghiemEntities2())
            {
                danhSachCauHoi = db.CAUHOIs
              .Where(c => c.Madethi == lblMaDe.Text)  // Assuming Madethi is a string
               .Select(c => new CCauHoi
               {
                   MaCauHoi = c.Macauhoi,
                   NoiDungCauHoi = c.Noidungcauhoi,
                   NoiDungCauA = c.NoidungcauA,
                   NoiDungCauB = c.NoidungcauB,
                   NoiDungCauC = c.NoidungcauC,
                   NoiDungCauD = c.NoidungcauD,
                   DapAn = c.Dapan
               })
                   .ToList();
            }

            // Khởi tạo mảng câu trả lời của học viên
            cauTraLoiHocVien = new string[danhSachCauHoi.Count];
        }


        private void ShowQuestion(int index)
        {
            if (index >= 0 && index < danhSachCauHoi.Count)
            {
                CCauHoi currentQuestion = danhSachCauHoi[index];
                txtCauHoi.Text = currentQuestion.NoiDungCauHoi;
                radA.Text = $" {currentQuestion.NoiDungCauA}";
                radB.Text = $" {currentQuestion.NoiDungCauB}";
                radC.Text = $" {currentQuestion.NoiDungCauC}";
                radD.Text = $" {currentQuestion.NoiDungCauD}";

                // Nếu học viên đã chọn câu trả lời cho câu hỏi này, đặt lại trạng thái của các RadioButton
                if (!string.IsNullOrEmpty(cauTraLoiHocVien[index]))
                {
                    switch (cauTraLoiHocVien[index])
                    {
                        case "A":
                            radA.Checked = true;
                            break;
                        case "B":
                            radB.Checked = true;
                            break;
                        case "C":
                            radC.Checked = true;
                            break;
                        case "D":
                            radD.Checked = true;
                            break;
                    }
                }
                else
                {
                    // Nếu học viên chưa chọn câu trả lời, đặt trạng thái RadioButton về trạng thái không chọn
                    radA.Checked = radB.Checked = radC.Checked = radD.Checked = false;
                }
            }
        }


        private void BaiThi_Load(object sender, EventArgs e)
        {
            // Load thông tin học viên và bài thi vào các control trên Form BaiThi
            lblMaHV.Text = MaHV_BaiThi;
            lblTenHV.Text = TenHV_BaiThi;
            lblNgayThi.Text = NgayThi_BaiThi.ToString("dd/MM/yyyy");
            lblMaDe.Text = MaDe_BaiThi;

            // Load danh sách câu hỏi dựa trên mã đề
            LoadData();

            // Hiển thị câu hỏi đầu tiên
            ShowQuestion(currentQuestionIndex);
        }

        private void tv1_time_Click(object sender, EventArgs e)
        {

        }

        private void timer_baithi_Tick(object sender, EventArgs e)
        {
            if (Minutes == 0 && Seconds == 0)
            {
                // Hết thời gian, hiển thị MessageBox và thực hiện các hành động cần thiết
                timer_baithi.Stop();
                MessageBox.Show("Đã hết thời gian!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                // Thêm code xử lý khi hết thời gian ở đây (ví dụ: đóng form hoặc chuyển đến form khác)
            }
            else
            {
                // Giảm thời gian còn lại
                if (Seconds > 0)
                {
                    Seconds--;
                }
                else
                {
                    Seconds = 59;
                    Minutes--;
                }

                // Hiển thị thời gian còn lại
                tv1_time.Text = Minutes.ToString("D2");
                tv2_time.Text = Seconds.ToString("D2");
            }
        }

        private void btnCauTruocDo_Click(object sender, EventArgs e)
        {
            SaveAnswer();
            // Chuyển đến câu hỏi trước đó (nếu có)
            if (currentQuestionIndex > 0)
            {
                currentQuestionIndex--;
                ShowQuestion(currentQuestionIndex);
            }
        }

        private void btn_Ketiep_Click(object sender, EventArgs e)
        {
            SaveAnswer();
            // Chuyển đến câu hỏi kế tiếp (nếu có)
            if (currentQuestionIndex < danhSachCauHoi.Count - 1)
            {
                currentQuestionIndex++;
                ShowQuestion(currentQuestionIndex);
            }
        }

        private void btn_Nopbai_Click(object sender, EventArgs e)
        {
            if (IsAllQuestionsAnswered())
            {
                DialogResult result = MessageBox.Show("Bạn có chắc chắn muốn nộp bài không?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (result == DialogResult.Yes)
                {
                    // Nếu chọn "Yes", lưu thông tin và đóng form
                    SaveAndCloseForm();
                }
                // Nếu chọn "No", không làm gì cả
            }
            else
            {
                MessageBox.Show("Bạn chưa làm xong bài thi!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
        private bool IsAllQuestionsAnswered()
        {
            foreach (string answer in cauTraLoiHocVien)
            {
                if (string.IsNullOrEmpty(answer))
                    return false;
            }
            return true;
        }
        private void SaveAnswer()
        {
            // Lưu câu trả lời của học viên trước khi chuyển sang câu hỏi mới
            if (currentQuestionIndex >= 0 && currentQuestionIndex < cauTraLoiHocVien.Length)
            {
                if (radA.Checked)
                    cauTraLoiHocVien[currentQuestionIndex] = "A";
                else if (radB.Checked)
                    cauTraLoiHocVien[currentQuestionIndex] = "B";
                else if (radC.Checked)
                    cauTraLoiHocVien[currentQuestionIndex] = "C";
                else if (radD.Checked)
                    cauTraLoiHocVien[currentQuestionIndex] = "D";
                else
                    cauTraLoiHocVien[currentQuestionIndex] = null;
            }
        }

        private void SaveToDatabase()
        {
            using (LuyenThiTracNghiemEntities2 db = new LuyenThiTracNghiemEntities2())
            {
                // Lấy mã học viên từ lblMaHV
                string maHocVien = lblMaHV.Text;

                // Kiểm tra xem học viên đã tồn tại trong cơ sở dữ liệu hay chưa
                HOCVIEN hocvien = db.HOCVIENs.FirstOrDefault(hv => hv.MaHV == maHocVien);

                // Nếu học viên chưa tồn tại, thêm mới
                if (hocvien == null)
                {
                    hocvien = new HOCVIEN
                    {
                        MaHV = maHocVien,
                        Madethi = MaHV_BaiThi,
                        NgayThi = NgayThi_BaiThi,
                        tongsocautraloidung = 0, // Giá trị ban đầu, bạn sẽ cập nhật sau
                        Ketqua = "Chưa đánh giá" // Giá trị ban đầu, bạn sẽ cập nhật sau
                    };

                    // Thêm hocvien vào cơ sở dữ liệu
                    db.HOCVIENs.Add(hocvien);
                }
                else
                {
                    // Nếu học viên đã tồn tại, cập nhật thông tin
                    hocvien.Madethi = MaHV_BaiThi;
                    hocvien.NgayThi = NgayThi_BaiThi;
                    hocvien.tongsocautraloidung = 0; // Reset giá trị, bạn sẽ cập nhật sau
                    hocvien.Ketqua = "Chưa đánh giá"; // Reset giá trị, bạn sẽ cập nhật sau
                }

                // Lưu các thay đổi vào cơ sở dữ liệu
                db.SaveChanges();
            }
        }




        private void SaveAndCloseForm()
        {
            using (LuyenThiTracNghiemEntities2 db = new LuyenThiTracNghiemEntities2())
            {
                // Lấy mã học viên từ lblMaHV
                string maHocVien = lblMaHV.Text;

                // Lấy lại thông tin học viên từ cơ sở dữ liệu để cập nhật
                HOCVIEN hocVien = db.HOCVIENs.FirstOrDefault(hv => hv.MaHV == maHocVien);

                if (hocVien != null)
                {
                    List<CAUHOI> danhSachCauHoiTrongDe = db.CAUHOIs.Where(c => c.Madethi == lblMaDe.Text).ToList();
                    int soCauDung = 0;

                    // Kiểm tra đáp án và câu trả lời của học viên
                    for (int i = 0; i < danhSachCauHoiTrongDe.Count; i++)
                    {
                        if (i < cauTraLoiHocVien.Length && !string.IsNullOrEmpty(cauTraLoiHocVien[i]))
                        {
                            if (cauTraLoiHocVien[i] == danhSachCauHoiTrongDe[i].Dapan)
                            {
                                soCauDung++;
                            }
                        }
                    }

                    // Cập nhật thông tin vào bảng học viên
                    hocVien.Madethi = lblMaDe.Text;
                    hocVien.NgayThi = NgayThi_BaiThi;
                    hocVien.tongsocautraloidung = soCauDung;
                    hocVien.Ketqua = soCauDung >= 2 ? "Đạt" : "Không đạt";

                    // Lưu các thay đổi vào cơ sở dữ liệu
                    db.SaveChanges();
                }
            }

            MessageBox.Show("Đã nộp bài thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            this.Close();
        }

    }


}
