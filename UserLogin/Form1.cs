using System;
using System.Windows.Forms;
using SQLLogic;
using SQLData;

namespace UserLogin
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            txtCCCD.Focus();
            if (!NhapNguoiDung.taoketnoi()) MessageBox.Show("Ket noi that bai");
        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void btnBatDauThi_Click(object sender, EventArgs e)
        {
           string cccd = txtCCCD.Text;

            if(string.IsNullOrEmpty(cccd))
            {
                MessageBox.Show("Vui Long Nhap CCCD");
                return;
            }

            var (hoten, tuoi, bangthi) = LayDuLieu.DangNhap(cccd);
                
            if(hoten != null)
            {
                DateTime ngaythi = DateTime.Now;
  
                BatDauThi batdau = new BatDauThi(cccd, hoten,tuoi, ngaythi, bangthi);
                batdau.ShowDialog();
                this.Close();
            }
            else
            {
                MessageBox.Show("Thong tin ko juan");
                return;
            }   
            
            
        }
       
        private void btnDangKi_Click(object sender, EventArgs e)
        {
            DangKy dangky = new DangKy();
            dangky.Show();
        }
    }
}
