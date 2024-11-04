using SQLData;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SQLLogic
{
    public class XuatKetQua
    {
        public static bool kiemtraketnoi()
        {
            return Data.taoketnoi();
        }

        public static bool ketnoindata()
        {
            return Data.ketnoiNdata();
        }

        public static int nhapketqua(string cccd, DateTime ngaythi, int socaudung, int socausai, string trangthai)
        {
            return Data.NhapKetQua(cccd, ngaythi, socaudung, socausai, trangthai);
        }

        public static DataTable xuatketquatheodate(DateTime date)
        {
            return Data.Xuatketquatheodate(date);
        }
        public static DataTable xuatketqua()
        {
            return Data.Xuatketqua();
        }
    }
}
