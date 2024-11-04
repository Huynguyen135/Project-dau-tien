using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLData;
using NDatabase;
namespace SQLLogic
{
    public class XuatCauHoi
    {
        public static bool taoketnoi()
        {
            return Data.taoketnoi();
        }
        //public static void nhapcauhoiThi(DataTable dtTracnghiem, ClassDatabase clsData, int tongsocauhoi, int tongcauhoitrongdb, int[] dscauhoi)
        //{
        //    Data.XuatCauhoi(dtTracnghiem, clsData, tongsocauhoi, tongcauhoitrongdb, dscauhoi);
        //}

        public static bool ketnoidata()
        {
            return Data.ketnoiNdata();
        }
    }
}
