using System.Data;
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
            return Data.Xuatketqua();
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

        public static int xoacauhoi(int idcauhoi)
        {
            return Data.xoacauhoitheoid(idcauhoi);
        }

        public static int xulinull()
        {
            return Data.xulinull();
        }

        public static DataTable hienthitaikhoannguoidung()
        {
            return Data.hienthitaikhoan();
        }

        public static int capnhattaikhoan(string cccd, string hoten, byte tuoi, string bangthi)
        {
            return Data.capnhattaikhoan(cccd, hoten, tuoi, bangthi);
        }

        public static DataTable timkiemtaikhoan(string cccd)
        {
            return Data.timkiemtaikhoan(cccd);
        }
    }
}
