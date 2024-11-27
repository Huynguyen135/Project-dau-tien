using OfficeOpenXml;
using SQLLogic;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Winform_App_thi_trac_nghiem
{
    public partial class danhsachketquathi : Form
    {

        public static DataTable dtFrom2 = new DataTable();


        public danhsachketquathi()
        {
            InitializeComponent();
        }

        private void danhsachketquathi_Load(object sender, EventArgs e)
        {
            DataTable dt2 = XuatKetQua.xuatketqua();
            dtFrom2 = dt2;

            dataGridView2.DataSource = dt2;
            dataGridView2.AutoGenerateColumns = true;
            dataGridView2.Refresh();
            TaiDanhSachKetQuaThi();
        }
        private void TaiDanhSachKetQuaThi()
        {
            DataTable dt = XuatKetQua.xuatketqua();
            dtFrom2 = dt;
            dataGridView2.DataSource = dt;
        }


        private void button1_Click(object sender, EventArgs e)
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
    }
}
