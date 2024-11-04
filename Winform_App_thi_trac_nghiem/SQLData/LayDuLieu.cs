using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLData;

namespace SQLData
{
    public class LayDuLieu
    {
        public static SqlConnection connect = new SqlConnection(chuoiketnoi);
        public static string chuoiketnoi = "Data Source = LAPTOP-PLNH04; Initial Catalog = DeThiTracNghiem; Integrated Security = True;";
        
        public Boolean Taoketnoi()
        {
            
            return Data.taoketnoi();
        }

        public static int NhapdulieuNguoiDung(string cccd, string hoten,  )
    }
}
