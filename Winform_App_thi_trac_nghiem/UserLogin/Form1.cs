using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


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

        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void btnBatDauThi_Click(object sender, EventArgs e)
        {
            
            BatDauThi frmBatdau = new BatDauThi();
            if (checkValid())
            {
                frmBatdau.ShowDialog();
            }
            else
            {
                MessageBox.Show("Vui long nhap day du thong tin", "Canh bao!");
            }
        }
        public bool checkValid()
        {
            if(txtCCCD.Text == null && txtHoTen.Text== null)
            {
                return false;
            }
            if(cbbDeThi.SelectedItem == null && cbbHangXe.SelectedItem == null)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        private void cbbDeThi_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
