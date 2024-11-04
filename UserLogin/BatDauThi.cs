using System;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using NDatabase;
using NFunction;
using SQLLogic;
using OfficeOpenXml;
using System.IO;
using System.Data.SqlClient;



namespace UserLogin
{
    public partial class BatDauThi : Form
    {

        static ClassDatabase clsData = new ClassDatabase();
        static ClassFunction clsFunction = new ClassFunction();
        public static DataTable dtTracnghiem = new DataTable();
        public static int tongcauhoitrongdb = 50;
        public static int tongsocauhoi = 25;

        int thoigian = 20;
        int phut = 0;
        int giay = 0;

     
        public static int pos = 1;

        public BatDauThi(string cccd, string hoten,byte tuoi, DateTime date, string bangthi)
        {
            if(!XuatCauHoi.ketnoidata())
            {
                MessageBox.Show("ket noi that bai");
            }

            if(!XuatKetQua.kiemtraketnoi())
            {
                MessageBox.Show("ket noi that bai");
            }

            InitializeComponent();

            txtCCCD.Text = cccd;
            txtHovaTen.Text = hoten;
            txtTuoi.Text = tuoi.ToString();
            txtHangxeThi.Text = bangthi.ToString();
            txtNgayThi.Text = date.ToString("dd/MM/yyyy");
        }

        private void BatDauThi_Load(object sender, EventArgs e)
        {
            //khoi tao danh sach cau hoi
            this.Cursor = Cursors.WaitCursor;
            this.Width = 1280;
            this.Height = 770;
            InitData();

            phut = thoigian - 1;
            giay = 60;
            this.timer1.Enabled = true;
            hienthidongho();
            //chon cau dau tien
            pos = 1;
           
            this.Cursor = Cursors.Default;
          
          
        }

        private void hienthidongho()
        {
            string strPhut = "";
            string strGiay = "";

            if (phut < 10)
            {
                strPhut = $"0" + phut.ToString();
            }
            else
            {
                strPhut = phut.ToString();
            }
            if (giay < 10)
            {
                strGiay = $"0"+ giay.ToString();
            }
            else
            {
                strGiay = giay.ToString();
            }
               this.labelThoiGianDemNguoc.Text = "Thời gian thi: " + strPhut + ":" + strGiay; 


        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            Application.DoEvents();
            giay--;
            timer1.Interval = 1000;
            if (giay < 0) 
            {
                phut --;
                giay = 59;
                
            }
            if(phut <= 4 )
            {
               labelThoiGianDemNguoc.ForeColor = Color.Red;
            }
            if(phut == -1)
            {
                timer1.Enabled = false;
                MessageBox.Show("Đã hết thời gian làm bài", "thông báo", MessageBoxButtons.OK);
                KetThucThi();
                string cccd = txtCCCD.Text;
                DateTime dateTime = DateTime.Now;
                int caudung = Convert.ToInt32(txtSoCauDung.Text);
                int causai = Convert.ToInt32(txtSoCauSai.Text);
                string Trangthai = labebKetQua.Text;

                XuatKetQua.nhapketqua(cccd, dateTime, caudung, causai, Trangthai);
            }
            hienthidongho();
        }

        public static void randomcauhoi()
        {
            int[] dscauhoi = new int[tongsocauhoi];
            int layso = 0;
            int i, j = 0;
            Boolean cautrung = false;   

            Random random = new Random();
            for (i = 1; i < tongsocauhoi + 1; i++)
            {
                while (true)
                {
                    cautrung = false;
                  
                    layso = random.Next(1, tongcauhoitrongdb);

                    for (j = 0; j < i; j++)
                    {
                        if (dscauhoi[j] == layso)
                        {
                            cautrung = true;
                            break;
                        }
                    }
                    if (cautrung == false)
                        break;
                }
                dscauhoi[i - 1] = layso;
            }
            //XuatCauhoi(dtTracnghiem, clsData, tongsocauhoi, tongcauhoitrongdb, dscauhoi);
            SQLData.DsBangcauhoi dsBangcauhoi = new SQLData.DsBangcauhoi();
            dtTracnghiem = dsBangcauhoi.Tables[0];

            for ( i = 0; i < dscauhoi.Count(); i++)
            {
                dtTracnghiem.Rows.Add();        
                dtTracnghiem.Rows[i]["IdCauHoi"] = i + 1;
              
           

                string sql = $"select * from CauHoi where IdCauHoi = {dscauhoi[i]}";
                using (SqlCommand sqlcmd = new SqlCommand(sql, clsData.cnConnect))
                {
                    clsData.OpenConnect(); 
                    SqlDataReader reader = sqlcmd.ExecuteReader();
                    while (reader.Read())
                    {

                        dtTracnghiem.Rows[i]["Debai"] = clsData.IsDBNullString(reader["Debai"]);

                        dtTracnghiem.Rows[i]["DapAn1"] = clsData.IsDBNullString(reader["DapAn1"]);
                        dtTracnghiem.Rows[i]["DapAn2"] = clsData.IsDBNullString(reader["DapAn2"]);
                        dtTracnghiem.Rows[i]["DapAn3"] = clsData.IsDBNullString(reader["DapAn3"]);
                        dtTracnghiem.Rows[i]["DapAn4"] = clsData.IsDBNullString(reader["DapAn4"]);

                        dtTracnghiem.Rows[i]["HinhAnh"] = clsData.IsDBNullString(reader["HinhAnh"]);
                        dtTracnghiem.Rows[i]["LoaiCauHoi"] = clsData.IsDBNullString(reader["LoaiCauHoi"]);

                        dtTracnghiem.Rows[i]["DapAnDung"] = clsData.IsDBNullInt16(Convert.ToInt16(reader["DapAnDung"]));
                        dtTracnghiem.Rows[i]["KetQuaBaiThi"] = clsData.IsDBNullInt16(Convert.ToInt16(reader["KetQuaBaiThi"]));

                        dtTracnghiem.Rows[i]["IdBangThi"] = clsData.IsDBNullString(reader["IdBangThi"]);
                        dtTracnghiem.Rows[i]["KetQuaCauHoi"] = clsData.IsDBNullString(reader["KetQuaCauHoi"]);

                    }
                    reader.Close();
                }
            }
        }
        
        //public static void XuatCauhoi(DataTable dtTracnghiem, ClassDatabase clsData, int tongsocauhoi, int tongcauhoitrongdb, int[] dscauhoi)
        //{
            
        //}


        private void InitData()
        {
            randomcauhoi();
           
           this.listcauhoi.Items.Clear();

            for(int i = 0; i < dtTracnghiem.Rows.Count ;i++)
            {
                this.listcauhoi.Items.Add(dtTracnghiem.Rows[i]["IdCauHoi"].ToString());
            }
        }

       

        private void listcauhoi_SelectedIndexChanged(object sender, EventArgs e)
        {
          
        }
        public static Image Base64ToImage(string img)
        {
            try
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
            catch (Exception ex) 
            {
                 MessageBox.Show($"Lỗi xuất ảnh: {ex.Message}");
                 throw ;
            }

        }
        //function hien thi thong tin cau trac nghiem
        private void ShowdataQuestionlist(int IdCauHoi)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;

                rtxtDapAn1.BackColor = Color.White;
                rtxtDapAn2.BackColor = Color.White;
                rtxtDapAn3.BackColor = Color.White;
                rtxtDapAn4.BackColor = Color.White;

                rtxtDeBai.Text = "Câu " + (IdCauHoi + 1).ToString() + ": " + dtTracnghiem.Rows[IdCauHoi]["Debai"].ToString();
                rtxtDapAn1.Text = dtTracnghiem.Rows[IdCauHoi]["DapAn1"].ToString();
                rtxtDapAn2.Text = dtTracnghiem.Rows[IdCauHoi]["DapAn2"].ToString();

                pictureBox1.Image = Base64ToImage(dtTracnghiem.Rows[IdCauHoi]["HinhAnh"].ToString());

                if(dtTracnghiem.Rows[IdCauHoi]["DapAn3"].ToString() == "")
                {
                    rbDapAn3.Visible = false;
                    rtxtDapAn3.Visible = false ;

                }
                else
                {
                    rbDapAn3.Visible = true;
                    rtxtDapAn3.Visible = true;
                    rtxtDapAn3.Text = dtTracnghiem.Rows[IdCauHoi]["DapAn3"].ToString();
                }

                if(dtTracnghiem.Rows[IdCauHoi]["DapAn4"].ToString() == "")
                {
                    rbDapAn4.Visible = false;
                    rtxtDapAn4 .Visible = false ;
                }
                else
                {
                    rbDapAn4.Visible = true;
                    rtxtDapAn4.Visible = true;
                    rtxtDapAn4.Text = dtTracnghiem.Rows[IdCauHoi]["DapAn4"].ToString();
                }

                switch (Convert.ToInt32(dtTracnghiem.Rows[IdCauHoi]["KetQuaBaiThi"])) 
                {
                    case 0:
                        rbDapAn1.Checked = false;
                        rbDapAn2.Checked = false;
                        rbDapAn3.Checked = false;
                        rbDapAn4.Checked = false;
                        break;
                    case 1:
                        rbDapAn1.Checked = true;
                        TomauCauHoi(rbDapAn1, null);
                        
                        break;
                    case 2:
                        rbDapAn2.Checked = true;
                        TomauCauHoi(rbDapAn2, null);
                        
                        break;
                    case 3:
                        rbDapAn3.Checked = true;
                        TomauCauHoi(rbDapAn3, null);
                       
                        break;
                    case 4:
                        rbDapAn4.Checked = true;
                        TomauCauHoi(rbDapAn4, null);
                        
                        break;
                    default:
                        break;
                }
                this.Cursor = Cursors.Default;
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
                throw;
            }
        }

        private void TomauCauHoi(object sender, EventArgs e)
        {
            RadioButton rb = sender as RadioButton;

            for (int i = 1; i <= 4; i++)
            {
                var txt = Controls.Find("rtxtDapAn" + i.ToString(), true).FirstOrDefault() as System.Windows.Forms.TextBox;
                if ((rb.Text.Trim() == i.ToString()) && (rb.Checked))
                {
                    txt.BackColor = Color.Gray;
                }
                else
                {
                    txt.BackColor = Color.White;
                }
            }
        }

        //function thuc hien su kien khi check vao textbox thay vi radiobox
        private void XulycheckTextBox(object sender, EventArgs e) 
        {
            System.Windows.Forms.TextBox txtclick = sender as System.Windows.Forms.TextBox;
            int chon = Convert.ToInt32(clsFunction.Right(txtclick.Name.Trim(), 1));

            var txt = Controls.Find("rbDapAn" + chon.ToString(), true).FirstOrDefault() as RadioButton;
            txt.Checked = true;

            dtTracnghiem.Rows[pos - 1]["KetQuaBaiThi"] = chon;
        }

        private void XulyClickRadio(object sender, EventArgs e)
        {
            RadioButton rb = sender as RadioButton;
            int chon = Convert.ToInt32(clsFunction.Right(rb.Name.Trim(), 1));

            dtTracnghiem.Rows[pos - 1]["KetQuaBaiThi"] = chon;
        }

        private void btnKetThucBaiThi_Click(object sender, EventArgs e)
        {
            var chon = MessageBox.Show("Bạn đã chắc chắn về việc thoát bài thi chưa?", "thong bao", MessageBoxButtons.OKCancel);
            if (chon == DialogResult.OK) 
            {
                KetThucThi();
                Luuketquabaithi();
            }
        }

        private void btnPrevious_Click(object sender, EventArgs e)
        {
            
            if (this.btnKetThucBaiThi.Visible ==false) return;
            if (pos == 1) return;

            pos--;
            listcauhoi.SelectedIndex = pos - 1;
            ShowdataQuestionlist(pos - 1);
        }

        private void btnNext_Click(object sender, EventArgs e)
        {

           
            if (this.btnKetThucBaiThi.Visible == false) return;
            if (pos == tongsocauhoi) return;

            pos++;
            listcauhoi.SelectedIndex = pos - 1;
            ShowdataQuestionlist(pos - 1);
        }

        private void KetThucThi()
        {
            this.Cursor = Cursors.WaitCursor;

            //hien thi du lieu len 
            this.timer1.Enabled = false;

            this.panelGridcontrol.Visible = true;
            this.btnKetThucBaiThi.Visible = false;
            this.btnThoat.Visible = true;

            string strKetQua = "";
            int caudung = 0;
            int causai = 0;
            int cauliet= 0;

            for (int i = 0; i < dtTracnghiem.Rows.Count; i++)
            {
                this.gridView1.AddNewRow();
                int rowhandle = this.gridView1.GetRowHandle(this.gridView1.DataRowCount);

                if (dtTracnghiem.Rows[i]["DapAnDung"].ToString() == dtTracnghiem.Rows[i]["KetQuaBaiThi"].ToString())
                {
                    strKetQua = "Đúng";
                    caudung += 1;
                }
                else
                {
                    strKetQua = "Sai";
                    causai += 1;
                }

                if (dtTracnghiem.Rows[i]["LoaiCauHoi"].ToString() == "Liet" && (dtTracnghiem.Rows[i]["DapAnDung"].ToString() != dtTracnghiem.Rows[i]["KetQuaBaiThi"].ToString()))
                 {
                    strKetQua = "Hết cứu";
                    cauliet += 1;
                 }
            

                this.gridView1.SetRowCellValue(rowhandle, this.gridView1.Columns[0], i + 1);
                this.gridView1.SetRowCellValue(rowhandle, this.gridView1.Columns[1], dtTracnghiem.Rows[i]["DapAnDung"]);
                this.gridView1.SetRowCellValue(rowhandle, this.gridView1.Columns[2], dtTracnghiem.Rows[i]["KetQuaBaiThi"]);
                this.gridView1.SetRowCellValue(rowhandle, this.gridView1.Columns[3], strKetQua);
                
            }
            this.gridView1.BestFitColumns();

            this.txtTongSoCauHoi.Text = tongsocauhoi.ToString();
            this.txtSoCauDung.Text = caudung.ToString();
            this.txtSoCauSai.Text = causai.ToString();
            if(causai == 0)
            {
                labelCauLiet.Visible = false;
                this.txtCauLiet.Visible = false;
            }
            else
            {
                labelCauLiet.Visible = true;
                txtCauLiet.Visible = true;
                this.txtCauLiet.Text = cauliet.ToString();
            }

            if (causai > 4 && caudung < 21 || cauliet == 1)
            {
                this.labebKetQua.Text = "Trượt";
            }
            else
            {
                this.labebKetQua.Text = "Đạt" ;
            }

            this.Cursor = Cursors.Default;
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            //cho nay xu li luu bai thi
            var chon = MessageBox.Show("bạn có muốn xuất kết quả ra file excel không?", "", MessageBoxButtons.YesNo);
            if(chon == DialogResult.No)
            {
                this.Close();
            }
            else
            {
                DateTime dateTime = DateTime.Now;
                DataTable dataTable = XuatKetQua.xuatketquatheodate(dateTime);
                string path = $@"D:\VisualStudio\C#\Winform_App_thi_trac_nghiem\Excel_File\KetQuaThingay{DateTime.Now.Day}thang{DateTime.Now.Month}nam{DateTime.Now.Year}.xlsx";
                XuatExcel(dataTable, path);
                this.Close();
            }
           
        }

        private void Luuketquabaithi()
        {
            string cccd = txtCCCD.Text;
            DateTime dateTime = DateTime.Now;
            int caudung = Convert.ToInt32(txtSoCauDung.Text);
            int causai = Convert.ToInt32(txtSoCauSai.Text);
            string Trangthai = labebKetQua.Text;

            XuatKetQua.nhapketqua(cccd, dateTime, caudung, causai, Trangthai);
        }

        public static void XuatExcel(DataTable dt, string path)
        {
            ExcelPackage.LicenseContext = OfficeOpenXml.LicenseContext.NonCommercial;

            // Khởi tạo file Excel
            using (ExcelPackage excel = new ExcelPackage())
            {
                // Tạo một worksheet mới
                ExcelWorksheet worksheet = excel.Workbook.Worksheets.Add("KetQuaThi");

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
                //kiểm tra file tồn tại hay không   
                if (!excelFile.Exists) 
                {
                    excelFile.Directory.Create();
                }
                excel.SaveAs(excelFile);
            }
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {
           
        }

        private void listcauhoi_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            if (this.listcauhoi.SelectedIndex < 0) return;
            pos = Convert.ToInt32(listcauhoi.SelectedValue);

            ShowdataQuestionlist(pos - 1);
            
        }

        private void gridControl1_Click(object sender, EventArgs e)
        {
            if (this.gridView1.RowCount == 0) 
            {
                return;
            }
            if (this.gridView1.GetRowCellValue(this.gridView1.FocusedRowHandle, "IdCauHoi") == null) return;
            if(this.gridControl1.Visible == false) return;
            int ipos = clsData.IsDBNullInt32(this.gridView1.GetRowCellValue(this.gridView1.FocusedRowHandle, "IdCauHoi"));
            if (ipos == 0) return;

            ShowdataQuestionlist(ipos - 1);
        }
    }
}
