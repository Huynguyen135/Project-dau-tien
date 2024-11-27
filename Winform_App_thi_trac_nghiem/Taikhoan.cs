using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SQLLogic;

namespace Winform_App_thi_trac_nghiem
{
    public partial class Taikhoan : Form
    {
        public Taikhoan()
        {
            InitializeComponent();
        }
        public static DataTable dtFrom3 = new DataTable();

        private void Taikhoan_Load(object sender, EventArgs e)
        {
            if (!NhapDuLieu.kiemtra()) MessageBox.Show("Ket noi that bai");
            if (!NhapNguoiDung.taoketnoi()) MessageBox.Show("Ket noi that bai");

            DataTable dt3 = NhapDuLieu.hienthitaikhoannguoidung();
            DataTable dtbangthi = NhapNguoiDung.hienthibangthi();
            cbbHangXe.DataSource = dtbangthi;
            dtFrom3 = dt3;

            cbbHangXe.DisplayMember = "TenBangThi";
            cbbHangXe.ValueMember = "IdBangThi";

            taidanhsachtaikhoan();
            dataGridView3.DataSource = dt3;
            dataGridView3.AutoGenerateColumns = true;
            dataGridView3.Refresh();
        }

        public void taidanhsachtaikhoan()
        {
            DataTable dt = NhapDuLieu.hienthitaikhoannguoidung();
            dtFrom3 = dt;
            dataGridView3.DataSource = dt;
        }

        private void btnThemTaiKhoan_Click(object sender, EventArgs e)
        {
            string cccd = txtCCCD.Text;
            string hoten = txtHoTen.Text;
            byte tuoi = Convert.ToByte(txtTuoi.Text);
            string bangthi = cbbHangXe.SelectedValue.ToString();
            DateTime date = DateTime.Now;
            try
            {
                if (checkValid())
                {
                    int result = NhapNguoiDung.nhapnguoidung(cccd, hoten, tuoi, bangthi);
                    if (result != 0)
                    {
                        MessageBox.Show("Đăng kí thành công");
                        this.Close();

                        //BatDauThi frmBatdau = new BatDauThi(hoten, date, bangthi);
                        //frmBatdau.ShowDialog();

                    }

                }
                else
                {
                    MessageBox.Show("Vui long nhap day du thong tin", "Canh bao!");
                }
            }
            catch (SqlException)
            {
                MessageBox.Show("Thông tin này đã được đăng kí");
            }

        }
        public bool checkValid()
        {
            if (txtCCCD.Text == null && txtHoTen.Text == null && txtTuoi.Text == null)
            {
                return false;
            }
            if (cbbHangXe.SelectedItem == null)
            {
                return false;
            }
            else
            {
                return true;
            }
        }


        private void btnCapNhat_Click(object sender, EventArgs e)
        {
            string cccd = txtCCCD.Text;
            string hoten = txtHoTen.Text;
            byte tuoi = Convert.ToByte(txtTuoi.Text);
            string bangthi = cbbHangXe.SelectedValue.ToString();
            DateTime date = DateTime.Now;
            try
            {
                if (checkValid())
                {
                    int result =NhapDuLieu.capnhattaikhoan(cccd, hoten, tuoi, bangthi);
                    if (result != 0)
                    {
                        MessageBox.Show("Cap nhat thành công");
                        this.Close();

                        //BatDauThi frmBatdau = new BatDauThi(hoten, date, bangthi);
                        //frmBatdau.ShowDialog();

                    }

                }
                else
                {
                    MessageBox.Show("Vui long nhap day du thong tin", "Canh bao!");
                }
            }
            catch (SqlException)
            {
                MessageBox.Show("loi cap nhat");
            }
        }
        private void capnhathienthi(DataTable dt)
        {
             if (dt != null && dt.Rows.Count > 0)
            {
                txtCCCD.Text = dt.Rows[0]["CCCD"].ToString();
                txtHoTen.Text = dt.Rows[0]["HoTen"].ToString();
                txtTuoi.Text = dt.Rows[0]["Tuoi"].ToString();
                cbbHangXe.Text = dt.Rows[0]["IdBangThi"].ToString();
            }
        }
        

        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            
               string CCCD =txtCCCD.Text;
                DataTable nhap = NhapDuLieu.timkiemtaikhoan(CCCD);
                if (nhap == null)
                {
                    MessageBox.Show("khong tim thay cau hoi");

                }
                else
                capnhathienthi(nhap);

            
        }
    }
}
