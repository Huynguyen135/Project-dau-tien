using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;
using System.IO;

namespace SQLData
{
    public class Data
    {

        public static SqlConnection connect;
        public static string chuoiketnoi = "Data Source = LAPTOP-PLNH04; Initial Catalog = DeThiTracNghiem; Integrated Security = True;";
        public static Boolean taoketnoi()
        {
            connect = new SqlConnection(chuoiketnoi);
            try
            {
                if (connect.State == ConnectionState.Closed)
                {
                    connect.Open();
                }
                return true;



            }
            catch (Exception e)
            {
                MessageBox.Show($"Không kết nối được, lỗi: {e.Message}");
                return false;
            }
        }

        public static int ThemDulieu(string idcauhoi, string debai, string dapan1, string dapan2, string dapan3, string dapan4, string hinhanh, string loaicauhoi, string idbangthi, byte dapandung)
        {
            // Câu lệnh SQL sử dụng tham số
            string sql = "INSERT INTO CauHoi (IdCauHoi, Debai, DapAn1, DapAn2, DapAn3, DapAn4, HinhAnh, LoaiCauHoi, DapAnDung, IdBangThi) " +
                         "VALUES (@IdCauHoi, @Debai, @DapAn1, @DapAn2, @DapAn3, @DapAn4, @HinhAnh, @LoaiCauHoi, @DapAnDung, @IdBangThi)";

            int sodong = 0;
            try
            {
                // Mở kết nối với database
                using (SqlCommand cmd = new SqlCommand(sql, connect))
                {
                    // Thêm tham số cho các giá trị
                    cmd.Parameters.AddWithValue("@IdCauHoi", idcauhoi);
                    cmd.Parameters.AddWithValue("@Debai", debai);
                    cmd.Parameters.AddWithValue("@DapAn1", dapan1);
                    cmd.Parameters.AddWithValue("@DapAn2", dapan2);
                    cmd.Parameters.AddWithValue("@DapAn3", dapan3);
                    cmd.Parameters.AddWithValue("@DapAn4", dapan4);
                    cmd.Parameters.AddWithValue("@HinhAnh", hinhanh);  // Hình ảnh được lưu dưới dạng chuỗi (nvarchar)
                    cmd.Parameters.AddWithValue("@LoaiCauHoi", loaicauhoi);
                    cmd.Parameters.AddWithValue("@DapAnDung", dapandung);  // byte cho đáp án đúng
                    cmd.Parameters.AddWithValue("@IdBangThi", idbangthi);

                    // Thực thi câu lệnh SQL và trả về số dòng bị ảnh hưởng
                    sodong = cmd.ExecuteNonQuery();
                    return sodong;
                }
            }
            catch (Exception ex)
            {
                // Ghi lại lỗi (nếu cần) và trả về 0 nếu có lỗi xảy ra
                Console.WriteLine("Lỗi: " + ex.Message);
                return sodong;
            }


        }

        public static int CapNhatDuLieu(string idcauhoi, string debai, string dapan1, string dapan2, string dapan3, string dapan4, string hinhanh, string loaicauhoi, string idbangthi, byte dapandung)
        {
            string sql = "UPDATE CauHoi SET Debai = @Debai, DapAn1 = @DapAn1, DapAn2 = @DapAn2, DapAn3 = @DapAn3, DapAn4 = @DapAn4, " +
             "HinhAnh = @HinhAnh, LoaiCauHoi = @LoaiCauHoi, DapAnDung = @DapAnDung, IdBangThi = @IdBangThi " +
             "WHERE IdCauHoi = @IdCauHoi";

            int sodong = 0;
            try
            {
                // Mở kết nối với database
                using (SqlCommand cmd = new SqlCommand(sql, connect))
                {
                    // Thêm tham số cho các giá trị
                    cmd.Parameters.AddWithValue("@IdCauHoi", idcauhoi);
                    cmd.Parameters.AddWithValue("@Debai", debai);
                    cmd.Parameters.AddWithValue("@DapAn1", dapan1);
                    cmd.Parameters.AddWithValue("@DapAn2", dapan2);
                    cmd.Parameters.AddWithValue("@DapAn3", dapan3);
                    cmd.Parameters.AddWithValue("@DapAn4", dapan4);
                    cmd.Parameters.AddWithValue("@HinhAnh", hinhanh);  // Hình ảnh được lưu dưới dạng chuỗi (nvarchar)
                    cmd.Parameters.AddWithValue("@LoaiCauHoi", loaicauhoi);
                    cmd.Parameters.AddWithValue("@DapAnDung", dapandung);  // byte cho đáp án đúng
                    cmd.Parameters.AddWithValue("@IdBangThi", idbangthi);

                    // Thực thi câu lệnh SQL và trả về số dòng bị ảnh hưởng
                    sodong = cmd.ExecuteNonQuery();
                    return sodong;
                }
            }
            catch (Exception ex)
            {
                // Ghi lại lỗi (nếu cần) và trả về 0 nếu có lỗi xảy ra
                Console.WriteLine("Lỗi: " + ex.Message);
                return sodong;
            }
        }

        public static DataTable LaydulieuBangCauhoi()
        {
            string sql = "select * from CauHoi";
            DataTable dt = new DataTable();
            try
            {
                using (SqlCommand sqlcmd = new SqlCommand(sql, connect))
                {
                    sqlcmd.CommandText = sql;
                    SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sqlcmd);
                    sqlDataAdapter.Fill(dt);
                    return dt;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi: {ex.Message}");
                return dt;
            }
        }

        public static DataTable LaydulieutheoIDcauhoi(string idcauhoi)
        {
            string sql = $"select * from CauHoi where IDCauHoi = @idcauhoi";

            DataTable dt = new DataTable();

            using (SqlCommand sqlcmd = new SqlCommand(sql, connect))
            {
                sqlcmd.Parameters.AddWithValue("@idcauhoi", idcauhoi);

                // Sử dụng SqlDataAdapter để điền dữ liệu vào DataTable
                using (SqlDataAdapter adapter = new SqlDataAdapter(sqlcmd))
                {
                    // Đổ dữ liệu vào DataTable
                    adapter.Fill(dt);
                }
            }
            return dt;
        }

        public static int Xoadulieu(string idcauhoi)
        {
            string sql = "Delete from CauHoi where IDCauHoi =" + idcauhoi;
            int sodong = 0;

            try
            {
                using (SqlCommand sqlcmd = new SqlCommand(sql, connect))
                {
                    sqlcmd.CommandText = sql;
                    SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sqlcmd);
                    sodong = sqlcmd.ExecuteNonQuery();
                    return sodong;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi: {ex.Message}");
                return sodong;
            }
        }

        public static void Luuhinhanh(string idcauhoi, Image image)
        {
            if (image == null)
            {
                throw new ArgumentNullException(nameof(image), "hinh anh ko dc null");

            }

            using (var ms = new MemoryStream())
            {
                var systemImage = System.Drawing.Imaging.ImageFormat.Png;
                image.Save(ms, systemImage);

                byte[] imageByte = ms.ToArray();
                string base64string = Convert.ToBase64String(imageByte);


            }
        }

    }
}
