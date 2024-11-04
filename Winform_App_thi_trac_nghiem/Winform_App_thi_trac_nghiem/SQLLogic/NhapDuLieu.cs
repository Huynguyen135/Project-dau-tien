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

        public static DataTable laydulieutheoIDCauhoi(string idcauhoi)
        {
            return Data.LaydulieutheoIDcauhoi(idcauhoi);
        }

        public static DataTable laydulieubangCauhoi()
        {
            return Data.LaydulieuBangCauhoi();
        }

        public static int nhapCauhoi(string idcauhoi, string debai, string dapan1, string dapan2, string dapan3, string dapan4, string hinhanh, string loaicauhoi, string idbangthi, byte dapandung)
        {
            return Data.ThemDulieu(idcauhoi, debai, dapan1, dapan2, dapan3, dapan4, hinhanh, loaicauhoi, idbangthi, dapandung);     
        }

        public static int capnhatCauHoi(string idcauhoi, string debai, string dapan1, string dapan2, string dapan3, string dapan4, string hinhanh, string loaicauhoi, string idbangthi, byte dapandung)
        {
            return Data.CapNhatDuLieu(idcauhoi, debai, dapan1, dapan2, dapan3, dapan4, hinhanh, loaicauhoi, idbangthi, dapandung);
        }

        public static int xoadulieu(string idcauhoi)
        {
            return Data.Xoadulieu(idcauhoi);
        }

        public void luuhinhanh(string idcauhoi, Image image)
        {
            Data.Luuhinhanh(idcauhoi, image);
        }


    }
}
