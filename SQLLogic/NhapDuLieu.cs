using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using SQLData;
using System.Drawing;

namespace SQLLogic
{
    public class NhapDuLieu
    {
        public static bool kiemtra()
        {
            return Data.taoketnoi();
        }

        public static DataTable laydulieutheoIDCauhoi(int idcauhoi)
        {
            return Data.LaydulieutheoIDcauhoi(idcauhoi);
        }

        public static DataTable laydulieubangCauhoi()
        {
            return Data.LaydulieuBangCauhoi();
        }

        public static DataTable xuatketquathi()
        {
            return Data.Xuatdulieuketquathi();
        }

        public static int nhapCauhoi(int idcauhoi, string debai, string dapan1, string dapan2, string dapan3, string dapan4, string hinhanh, string loaicauhoi, string idbangthi, byte dapandung, byte dapannguoidung)
        {
            return Data.ThemDulieu(idcauhoi, debai, dapan1, dapan2, dapan3, dapan4, hinhanh, loaicauhoi, idbangthi, dapandung, dapannguoidung);     
        }

        public static int capnhatCauHoi(int idcauhoi, string debai, string dapan1, string dapan2, string dapan3, string dapan4, string hinhanh, string loaicauhoi, string idbangthi, byte dapandung)
        {
            return Data.CapNhatDuLieu(idcauhoi, debai, dapan1, dapan2, dapan3, dapan4, hinhanh, loaicauhoi, idbangthi, dapandung);
        }

        public static int xoadulieu(int idcauhoi)
        {
            return Data.Xoadulieu(idcauhoi);
        }

        public void luuhinhanh(int idcauhoi, Image image)
        {
            Data.Luuhinhanh(idcauhoi, image);
        }

        public static int xulinull()
        {
            return Data.xulinull();
        }
    }
}
