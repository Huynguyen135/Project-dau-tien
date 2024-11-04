using SQLLogic;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace UserLogin
{
    public partial class DangKy : Form
    {
        public DangKy()
        {
            InitializeComponent();
        }

        private void DangKy_Load(object sender, EventArgs e)
        {
            txtCCCD.Focus();
            if (!NhapNguoiDung.taoketnoi()) MessageBox.Show("Ket noi that bai");

            DataTable dtbangthi = NhapNguoiDung.hienthibangthi();
            cbbHangXe.DataSource = dtbangthi;

            cbbHangXe.DisplayMember = "TenBangThi";
            cbbHangXe.ValueMember = "IdBangThi";

        }

        private void btnDangKi_Click(object sender, EventArgs e)
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

        private void cbbHangXe_SelectedIndexChanged(object sender, EventArgs e)
        {
            cbbHangXe.SelectedItem = NhapNguoiDung.hienthibangthi();
        }
    }
}
