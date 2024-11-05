using System.Data;
using SQLData;

namespace SQLLogic
{
    public class NhapNguoiDung
    {

        public static bool taoketnoi()
        {
            return LayDuLieu.Taoketnoi();
        }
        public static int nhapnguoidung(string cccd, string hoten, byte tuoi, string idbangthi)
        {
            return LayDuLieu.NhapdulieuNguoiDung(cccd, hoten, tuoi, idbangthi);
        }

        public static DataTable hienthibangthi()
        {
            return LayDuLieu.HienThiBangThi();
        }

        public static string dangnhap(string cccd)
        {
            var (hoten, tuoi, bangthi) = LayDuLieu.DangNhap(cccd);
            if (hoten != null)
            {
                return hoten;
            }
            else
            {
                return null;
            }
        }
    }
}
