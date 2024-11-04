using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SQLData;

namespace SQLData
{
    public class LayDuLieu
    {
        public static SqlConnection connect;
        public static string chuoiketnoi = "Data Source=MSI;Initial Catalog=DeThiTracNghiem;User ID=sa;Password=1234";
        
        public static Boolean Taoketnoi()
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

        //vung dang nhap
        public static(string hoten, byte tuoi, string bangthi)DangNhap(string cccd)
        {
            string sql = "select HoTen, Tuoi, IdBangThi from NguoiDung where CCCD = @cccd";
            object sodong;
            try
            {
                using (SqlCommand sqlcmd = new SqlCommand(sql, connect))
                {
                    sqlcmd.Parameters.AddWithValue("@cccd", cccd);
                    sodong = sqlcmd.ExecuteScalar();

                    //return sodong != null ? sodong.ToString() : null; ;
                    using (SqlDataReader reader = sqlcmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            string hoten = reader["HoTen"].ToString();
                            byte tuoi = Convert.ToByte(reader["Tuoi"]);
                            string bangthi = reader["IdBangThi"].ToString();
                            return (hoten, tuoi, bangthi);
                        }
                    }

                }
            }
            catch (Exception e) 
            {
                MessageBox.Show($"lỗi: {e.Message}");
              
            }
            return (null, 0, null);
           
        }
        //end of vung dang nhap

        //vung dang ki
        public static int NhapdulieuNguoiDung(string cccd, string hoten, byte tuoi, string idbangthi)
        {

            string sql = "insert into NguoiDung(CCCD, HoTen, Tuoi, IdBangThi) values(@cccd, @hoten, @tuoi, @idbangthi)";
            int sodong = 0;
            try
            {

                using (SqlCommand sqlcmd = new SqlCommand(sql, connect))
                {
                    sqlcmd.Parameters.AddWithValue("@cccd", cccd);
                    sqlcmd.Parameters.AddWithValue("@hoten", hoten);
                    sqlcmd.Parameters.AddWithValue("@tuoi", tuoi);
                    sqlcmd.Parameters.AddWithValue("@idbangthi", idbangthi);
                    sodong = sqlcmd.ExecuteNonQuery();
                    return sodong;
                }

            }
            catch(SqlException e) when(e.Number == 2627)
            {
                MessageBox.Show("CCCD đã tồn tại, vui lòng kiểm tra lại.");
                return sodong;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi đăng kí: {ex.Message}");
                return sodong;
            }

        }
        // end of vung dang ki

        //vung hien thi bang thi
        public static DataTable HienThiBangThi()
        {
            string sql = "select IdBangThi from BangThi";
            DataTable dt = new DataTable();

            try
            {
                using (SqlCommand sqlcmd = new SqlCommand(sql, connect))
                {
                    sqlcmd.CommandText = sql;
                    SqlDataAdapter adapter = new SqlDataAdapter(sqlcmd);
                    adapter.Fill(dt);
                    return dt;
                }
            }
            catch (Exception ex) 
            {
                MessageBox.Show($"loi hien thi bang thi: {ex.Message}");
                return dt;
            }
        }
        //vung hien thi bang thi
    }
}
