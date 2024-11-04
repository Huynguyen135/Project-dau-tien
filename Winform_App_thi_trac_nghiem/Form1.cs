using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using SQLLogic;
using OfficeOpenXml;


namespace Winform_App_thi_trac_nghiem
{
    public partial class Form1 : Form
    {
        public static DataTable dtFrom = new DataTable();
        public static DataTable dtFrom2 = new DataTable();

        public Form1()
        {
            InitializeComponent();

        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            var thoat = MessageBox.Show("chắc chắn là thoát chưa?", "chắc chưa", MessageBoxButtons.OKCancel);
            if (thoat == DialogResult.OK) Application.Exit();

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.Width = 1942;
            this.Height = 1102;

            if (!NhapDuLieu.kiemtra()) MessageBox.Show("Ket noi that bai");

            DataTable dt = NhapDuLieu.laydulieubangCauhoi();
            DataTable dt2 = XuatKetQua.xuatketqua();
            dtFrom = dt;
            dtFrom2 = dt2;

            Taidanhsach();
            TaiDanhSachKetQuaThi();

            dataGridView1.DataSource = dt;
            dataGridView1.AutoGenerateColumns = true;
            dataGridView1.Refresh();

            dataGridView2.DataSource = dt2;
            dataGridView2.AutoGenerateColumns = true;
            dataGridView2.Refresh();

            //ListViewItem item;
            //DataTable cauhoi = Data.LaydulieuBangCauhoi();
            //for(int i = 0; i < cauhoi.Rows.Count;i++)
            //{
            //    item = new ListViewItem(cauhoi.Rows[i][1].ToString());

            //}

        }

        public void CapNhatHienThi(DataTable dt)
        {
            if (dt != null && dt.Rows.Count > 0)
            {
                txtMaCau.Text = dt.Rows[0]["IDCauHoi"].ToString();
                txtDeBai.Text = dt.Rows[0]["Debai"].ToString();
                txtDapAn1.Text = dt.Rows[0]["DapAn1"].ToString();
                txtDapAn2.Text = dt.Rows[0]["DapAn2"].ToString();
                txtDapAn3.Text = dt.Rows[0]["DapAn3"].ToString();
                txtDapAn4.Text = dt.Rows[0]["DapAn4"].ToString();
                pictureBox.Image = Base64ToImage(dt.Rows[0]["HinhAnh"].ToString());

                string loaicauhoi = dt.Rows[0]["LoaiCauHoi"].ToString();
                if (loaicauhoi == "Liet") rbCauHoiLiet.Checked = true;
                if (loaicauhoi == "Thuong") rbCauHoiThuong.Checked = true;
                else rbSaHinh.Checked = true;

                string dapandung = dt.Rows[0]["DapAnDung"].ToString();
                if (dapandung == "1") rbDapAn1.Checked = true;
                if (dapandung == "2") rbDapAn2.Checked = true;
                if (dapandung == "3") rbDapAn3.Checked = true;
                else rbDapAn4.Checked = true;

                string loaibang = dt.Rows[0]["IdBangThi"].ToString();
                if (loaibang == "A1") rbBangA1.Checked = true;
                else rbBangA2.Checked = true;
            }
        }

        private void Taidanhsach()
        {
            DataTable dt = NhapDuLieu.laydulieubangCauhoi();
            dtFrom = dt;
            dataGridView1.DataSource = dt;
        }

        private void TaiDanhSachKetQuaThi()
        {
            DataTable dt = XuatKetQua.xuatketqua();
            dtFrom2 = dt;
            dataGridView2.DataSource = dt;
        }

        public bool CheckValid()
        {
            string txtdebai = txtDeBai.Text;
            string dapan1 = txtDapAn1.Text;
            string dapan2 = txtDapAn2.Text;
            string dapan3 = txtDapAn3.Text;
            string dapan4 = txtDapAn4.Text;
            if (txtdebai == null || dapan1 == null && dapan2 == null)
            {
                MessageBox.Show("Ma cau khong duoc de trong");
                return false;
            }


            if (!rbCauHoiThuong.Checked && !rbCauHoiLiet.Checked && !rbSaHinh.Checked)
            {
                MessageBox.Show("hay chon loai cau hoi");
                return false;
            }

            if (!rbDapAn1.Checked && !rbDapAn2.Checked && !rbDapAn3.Checked && !rbDapAn4.Checked)
            {
                MessageBox.Show("hay chon dap an dung");
                return false;
            }

            else return true;

        }

        public byte NhapDapandung()
        {

            if (rbDapAn1.Checked) return 1;
            if (rbDapAn2.Checked) return 2;
            if (rbDapAn3.Checked) return 3;
            if (rbDapAn4.Checked) return 4;

            else return 0;
        }

        public string NhapLoaiBang()
        {
            if (rbBangA1.Checked) return "A1";
            if (rbBangA2.Checked) return "A2";
            else return null;
        }

        public string NhapLoaiCauHoi()
        {

            if (rbCauHoiThuong.Checked)
            {
                return "Thuong";
            }

            if (rbCauHoiLiet.Checked)
            {
                return "Liet";
            }

            if (rbSaHinh.Checked) return "SaHinh";
            else return null;
        }


        public static string ImageToBase64(Image img, System.Drawing.Imaging.ImageFormat format)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                try
                {
                    // Lưu hình ảnh vào MemoryStream
                    img.Save(ms, format);
                    byte[] imagebyte = ms.ToArray();

                    //chuyen doi byte[] thanh base64
                    string base64string = Convert.ToBase64String(imagebyte);
                    return base64string;
                }
                catch (NullReferenceException)
                {
                    MessageBox.Show("anh chua duoc luu");
                    return null;
                }
            }
        }



        private void btnNhap_Click(object sender, EventArgs e)
        {
            int idcauhoi = Convert.ToInt32(txtMaCau.Text);
            string txtdebai = txtDeBai.Text;
            string dapan1 = txtDapAn1.Text;
            string dapan2 = txtDapAn2.Text;
            string dapan3 = txtDapAn3.Text;
            string dapan4 = txtDapAn4.Text;
            byte dapandung = NhapDapandung();
            string hinhanh;
            if (rbCauHoiThuong.Checked && rbCauHoiLiet.Checked)
            {
                hinhanh = "null";
            }
            else
            {
                hinhanh = ImageToBase64(pictureBox.Image, System.Drawing.Imaging.ImageFormat.Png);
            }

            string idbangthi = NhapLoaiBang();
            string loaicauhoi = NhapLoaiCauHoi();
            byte dapannguoidung = 0;
            try
            {
                int result = NhapDuLieu.nhapCauhoi(idcauhoi, txtdebai, dapan1, dapan2, dapan3, dapan4, hinhanh, loaicauhoi, idbangthi, dapandung, dapannguoidung);
                if (result > 0)
                {
                    MessageBox.Show("Nhập thành công");
                    Taidanhsach(); // Refresh lại danh sách
                }
                else
                {
                    MessageBox.Show("Không thể nhập dữ liệu. Vui lòng kiểm tra lại!");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi lưu câu hỏi: {ex.Message}");
            }



        }

        private void btnTaiAnh_Click(object sender, EventArgs e)
        {
            OpenFileDialog open = new OpenFileDialog();
            if (open.ShowDialog() == DialogResult.OK)
            {
                string path = open.FileName;
                pictureBox.ImageLocation = path;
            }
        }

        private void btnXoaAnh_Click(object sender, EventArgs e)
        {
            pictureBox.Image = null;
        }

        public static Image Base64ToImage(string img)
        {
            if (string.IsNullOrEmpty(img))
            {
                MessageBox.Show("khong co anh de chuyen doi");
            }
            //Giải mã chuỗi base64 sang byte    
            byte[] imageByte = Convert.FromBase64String(img);

            //thực hiện chuyển đổi hình ảnh từ màng byte    
            using (MemoryStream ms = new MemoryStream(imageByte))
            {
                return Image.FromStream(ms);
            }

        }

        private void btnTimMaCau_Click(object sender, EventArgs e)
        {
            int idmacau = Convert.ToInt32(txtMaCau.Text);
            DataTable nhap = NhapDuLieu.laydulieutheoIDCauhoi(idmacau);
            if (nhap == null)
            {
                MessageBox.Show("khong tim thay cau hoi");

            }
            else
                CapNhatHienThi(nhap);

        }

        private void btnXoaDeBai_Click(object sender, EventArgs e)
        {
            txtDeBai.Clear();
        }

        private void btnXoaDapAn1_Click(object sender, EventArgs e)
        {
            txtDapAn1.Clear();
        }

        private void btnXoaDapAn2_Click(object sender, EventArgs e)
        {
            txtDapAn2.Clear();
        }

        private void btnXoaDapAn3_Click(object sender, EventArgs e)
        {
            txtDapAn3.Clear();
        }

        private void btnXoaDapAn4_Click(object sender, EventArgs e)
        {
            txtDapAn4.Clear();
        }

        private void btnXoaMaCau_Click(object sender, EventArgs e)
        {
            txtMaCau.Clear();
        }

        private void btnXoaCau_Click(object sender, EventArgs e)
        {
            txtMaCau.Focus();
            txtMaCau.Clear();
            txtDapAn3.Clear();
            txtDapAn2.Clear();
            txtDapAn1.Clear();
            txtDeBai.Clear();
            txtDapAn4.Clear();
            pictureBox.Image = null;
        }

        private void btnXuatExel_Click(object sender, EventArgs e)
        {

            DataTable dataTable = NhapDuLieu.laydulieubangCauhoi();
            string filePath = @"D:\VisualStudio\C#\Winform_App_thi_trac_nghiem\Excel_file\CauHoi.xlsx";

            try
            {
                XuatExcel(dataTable, filePath);
                MessageBox.Show("Xuất file thành công");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi xuất file: {ex.Message}");
            }

        }

        public static void XuatExcel(DataTable dt, string path)
        {
            ExcelPackage.LicenseContext = OfficeOpenXml.LicenseContext.NonCommercial;

            // Khởi tạo file Excel
            using (ExcelPackage excel = new ExcelPackage())
            {
                // Tạo một worksheet mới
                ExcelWorksheet worksheet = excel.Workbook.Worksheets.Add("CauHoi");

                // Đổ dữ liệu từ DataTable vào worksheet
                for (int i = 1; i < dt.Columns.Count; i++)
                {
                    // Thêm tiêu đề cho cột
                    worksheet.Cells[1, i + 1].Value = dt.Columns[i].ColumnName;
                }

                // Thêm dữ liệu từng dòng
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    for (int j = 0; j < dt.Columns.Count; j++)
                    {
                        worksheet.Cells[i + 2, j + 1].Value = dt.Rows[i][j].ToString();
                    }
                }

                // Lưu file Excel ra file đích
                FileInfo excelFile = new FileInfo(path);
                excel.SaveAs(excelFile);
            }
        }

        private void btnNhapFile_Click(object sender, EventArgs e)
        {

            var filepath = @"D:\VisualStudio\C#\Winform_App_thi_trac_nghiem\Excel_File\CauHoi1.xlsx";
            try
            {
                NhapFileExel(filepath);
                Taidanhsach();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi nhập file: " + ex.Message);
            }

        }

        public static void NhapFileExel(string excelFilePath)
        {
            try
            {
                ExcelPackage.LicenseContext = OfficeOpenXml.LicenseContext.NonCommercial;
                if (!File.Exists(excelFilePath))
                {
                    MessageBox.Show("File Ko tồn tại");
                    return;
                }

                // Sử dụng EPPlus để đọc file Excel
                using (var package = new ExcelPackage(new FileInfo(excelFilePath)))
                {
                    var FirstWorksheet = package.Workbook.Worksheets[0]; // lay worksheet dau tien
                    var RowCount = FirstWorksheet.Dimension.Rows; //so dong trong worksheet
                    var ColCount = FirstWorksheet.Dimension.Columns; //so cot trong worksheet


                    for (int row = 2; row < RowCount + 2; row++) // bo qua dong dau tien (tieu de)
                    {
                        //var value = new Object[ColCount];
                        //for(int col = 1; col < ColCount; col++)
                        //{
                        //    value[col-1] = FirstWorksheet.Cells[row, col].Value? .ToString();
                        //}
                        int idcauhoi = int.TryParse(FirstWorksheet.Cells[row, 1].Value?.ToString(), out int result0) ? result0 : (int)0;
                        string debai = FirstWorksheet.Cells[row, 2].Value?.ToString(); // Cột 2
                        string dapan1 = FirstWorksheet.Cells[row, 3].Value?.ToString(); // Cột 3
                        string dapan2 = FirstWorksheet.Cells[row, 4].Value?.ToString(); // Cột 4
                        string dapan3 = FirstWorksheet.Cells[row, 5].Value?.ToString(); // Cột 5
                        string dapan4 = FirstWorksheet.Cells[row, 6].Value?.ToString(); // Cột 6
                        string hinhanh = FirstWorksheet.Cells[row, 7].Value?.ToString(); // Cột 7
                        string loaicauhoi = FirstWorksheet.Cells[row, 8].Value?.ToString(); // Cột 8
                        byte dapandung = byte.TryParse(FirstWorksheet.Cells[row, 9].Value?.ToString(), out byte result) ? result : (byte)0; // Cột 10, đảm bảo chuyển đổi an toàn
                        byte dapannguoidung = byte.TryParse(FirstWorksheet.Cells[row, 10].Value?.ToString(), out byte result2) ? result2 : (byte)0;
                        string idbangthi = FirstWorksheet.Cells[row, 11].Value?.ToString(); // Cột 9


                        NhapDuLieu.nhapCauhoi(idcauhoi, debai, dapan1, dapan2, dapan3, dapan4, hinhanh, loaicauhoi, idbangthi, dapandung, dapannguoidung);
                    }
                    MessageBox.Show("nhập file thành công");
                    
                }
                
            }
            catch (SqlException ex)
            {
                MessageBox.Show($"Lỗi liên quan đến sql khi nhập: {ex.Message}");
            }
        }

        private void btnCapNhat_Click(object sender, EventArgs e)
        {
            try
            {
                int idcauhoi = Convert.ToInt32(txtMaCau.Text);
                string txtmacau = txtMaCau.Text;
                string txtdebai = txtDeBai.Text;
                string dapan1 = txtDapAn1.Text;
                string dapan2 = txtDapAn2.Text;
                string dapan3 = txtDapAn3.Text;
                string dapan4 = txtDapAn4.Text;
                byte dapandung = NhapDapandung();
                string hinhanh;
                if (rbCauHoiThuong.Checked && rbCauHoiLiet.Checked)
                {
                    hinhanh = "null";
                }
                else
                {
                    hinhanh = ImageToBase64(pictureBox.Image, System.Drawing.Imaging.ImageFormat.Png);
                }

                string idbangthi = NhapLoaiBang();
                string loaicauhoi = NhapLoaiCauHoi();
                try
                {
                    int result = NhapDuLieu.capnhatCauHoi(idcauhoi, txtdebai, dapan1, dapan2, dapan3, dapan4, hinhanh, loaicauhoi, idbangthi, dapandung);
                    if (result > 0)
                    {
                        MessageBox.Show("Cập nhật thành công");
                        Taidanhsach(); // Refresh lại danh sách
                    }
                    else
                    {
                        MessageBox.Show("Không thể Cập nhật dữ liệu. Vui lòng kiểm tra lại!");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Lỗi khi Cập nhật  câu hỏi: {ex.Message}");
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show($"loi: {ex.Message}");
            }



        }
        private void btnXuLiNull_Click(object sender, EventArgs e)
        {
            NhapDuLieu.xulinull();
            MessageBox.Show("Xử lí thành công");
            Taidanhsach();
        }

        private void btnXuatKetQuaThi_Click(object sender, EventArgs e)
        {
            DataTable dataTable = NhapDuLieu.xuatketquathi();
            string filePath = @"D:\VisualStudio\C#\Winform_App_thi_trac_nghiem\Excel_file\KetQuaThiCuaToanBoThiSinh.xlsx";
            try
            {
                XuatExcel(dataTable, filePath);
                MessageBox.Show("Xuất file thành công");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi xuất file: {ex.Message}");
            }

        }
    }
}
