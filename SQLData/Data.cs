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
using NDatabase;
using NFunction;
using System.Runtime.CompilerServices;

namespace SQLData
{
    public class Data
    {

        public static SqlConnection connect;
        public static ClassDatabase clsData;
       
        public static string chuoiketnoi = "Data Source=MSI;Initial Catalog=DeThiTracNghiem;User ID=sa;Password=1234";

        //vung ket noi voi database
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
        public static Boolean ketnoiNdata()
        {
            clsData = new ClassDatabase();
            if(clsData.cnConnect != null)
            {
                clsData.SServerName = "MSI";
                clsData.SDatabase = "DeThiTracNghiem";
                clsData.SUsername = "sa";
                clsData.SPassword = "1234";
            }
            return true;
          
        }
        // end of vung ket noi voi database

        // vung thao tac du lieu 
        public static int ThemDulieu(int idcauhoi, string debai, string dapan1, string dapan2, string dapan3, string dapan4, string hinhanh, string loaicauhoi, string idbangthi, byte dapandung, byte ketquabaithi)
        {
            // Câu lệnh SQL sử dụng tham số
            string sql = "INSERT INTO CauHoi(IdCauHoi, Debai, DapAn1, DapAn2, DapAn3, DapAn4, HinhAnh, LoaiCauHoi, DapAnDung, KetQuaBaiThi, IdBangThi) " +
                         "VALUES (@idcauhoi, @Debai, @DapAn1, @DapAn2, @DapAn3, @DapAn4, @HinhAnh, @LoaiCauHoi, @DapAnDung,@ketquabaithi, @IdBangThi)";

            int sodong = 0;
            try
            {
                // Mở kết nối với database
                using (SqlCommand cmd = new SqlCommand(sql, connect))
                {
                    // Thêm tham số cho các giá trị
                    cmd.Parameters.AddWithValue("@idcauhoi", idcauhoi);
                    cmd.Parameters.AddWithValue("@Debai", debai);
                    cmd.Parameters.AddWithValue("@DapAn1", dapan1);
                    cmd.Parameters.AddWithValue("@DapAn2", dapan2);
                    cmd.Parameters.AddWithValue("@DapAn3", dapan3);
                    cmd.Parameters.AddWithValue("@DapAn4", dapan4);
                    cmd.Parameters.AddWithValue("@HinhAnh", hinhanh);  // Hình ảnh được lưu dưới dạng chuỗi (nvarchar)
                    cmd.Parameters.AddWithValue("@LoaiCauHoi", loaicauhoi);
                    cmd.Parameters.AddWithValue("@DapAnDung", dapandung);  // byte cho đáp án đúng
                    cmd.Parameters.AddWithValue("@ketquabaithi", ketquabaithi);
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

        public static int CapNhatDuLieu(int idcauhoi, string debai, string dapan1, string dapan2, string dapan3, string dapan4, string hinhanh, string loaicauhoi, string idbangthi, byte dapandung)
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

        public static int xulinull()
        {
            string sql = "UPDATE CauHoi\r\nSET DapAn3 = ''\r\nWHERE DapAn3 = 'null'\r\nUpdate CauHoi\r\nset DapAn4 = ''\r\nwhere DapAn4 = 'null'";
            int sodong = 0;
            try
            {
                using(SqlCommand sqlcmd =  new SqlCommand(sql, connect))
                {
                    sodong = sqlcmd.ExecuteNonQuery();
                    return sodong;
                }
            }
            catch(Exception e)
            {
                MessageBox.Show($"lỗi khi xử lí: {e.Message}");
                return 0;
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

        public static DataTable LaydulieutheoIDcauhoi(int idcauhoi)
        {
            string sql = $"select * from CauHoi with (NoLock) where IDCauHoi = @idcauhoi";

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

        public static int Xoadulieu(int idcauhoi)
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

        public static void Luuhinhanh(int idcauhoi, Image image)
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
        public static int NhapKetQua(string cccd, DateTime ngaythi, int socaudung, int socausai, string trangthai)
        {
            string sql = "INSERT INTO KetQuaThi VALUES(@cccd, @ngaythi, @socaudung, @socausai, @trangthai)";
            int sodong = 0;
            try
            {
                using (SqlCommand sqlcmd = new SqlCommand(sql, connect))
                {
                    sqlcmd.Parameters.AddWithValue("@cccd", cccd);
                    sqlcmd.Parameters.AddWithValue("@ngaythi", ngaythi);
                    sqlcmd.Parameters.AddWithValue("@socaudung", socaudung);
                    sqlcmd.Parameters.AddWithValue("@socausai", socausai);
                    sqlcmd.Parameters.AddWithValue("@trangthai", trangthai);

                    sodong = sqlcmd.ExecuteNonQuery();

                }
                return sodong;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"loi luu ket qua: {ex.Message}");
                return sodong;
            }
        }

        //end of vung thao tac du lieu 

        //vung xuat du lieu, ket qua
        public static DataTable Xuatdulieuketquathi()
        {
            string sql = "select * from KetQuaThi";
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

        public static DataTable Xuatketquatheodate(DateTime date)
        {
            string sql = "select * from KetQuaThi WHERE CAST(NgayThi AS DATE) = CAST(@ngaythi AS DATE)";
            DataTable dt = new DataTable();
            try
            {
                using (SqlCommand sqlcmd = new SqlCommand(sql, connect))
                {
                    sqlcmd.Parameters.AddWithValue("@ngaythi", date);
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
        public static DataTable Xuatketqua()
        {
            string sql = "select * from KetQuaThi ";
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
        //end of vung xuat du lieu, ket qua
    }
}
