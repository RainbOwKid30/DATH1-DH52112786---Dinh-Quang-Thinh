using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QuanLyTaiKhoanNganHang
{
    
    public partial class DesignDaoDien : Form
    {
        private List<CNhanVien> dslist = new List<CNhanVien>();
        private List<CKhachHang> dsKH_list = new List<CKhachHang>();
        public DesignDaoDien()
        {
            InitializeComponent();
        }

        public event EventHandler QuayVe;
        public bool isQuayVe = true;

        private void picQuayVe_Click(object sender, EventArgs e)
        {
            QuayVe(this, new EventArgs());
        }

        private void DesignDaoDien_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (isQuayVe)
                Application.Exit();
        }
        // thêm tài khoản khách hàng
        private void picThem_Click(object sender, EventArgs e)
        {
            ThemKhachHang dsKH = new ThemKhachHang();
            dsKH.Show();
            this.Hide();
            dsKH.QuayVe += DsKH_QuayVe;
            


        }

        private void DsKH_QuayVe(object sender, EventArgs e)
        {
            (sender as ThemKhachHang).isQuayVe = false;
            (sender as ThemKhachHang).Close();
            this.Show();
        }

        private void picThongTin_Click(object sender, EventArgs e)
        {
            ThongTinKH ttkh = new ThongTinKH();
            ttkh.Show();
            this.Hide();
            ttkh.QuayVe += Ttkh_QuayVe;
        }

        private void Ttkh_QuayVe(object sender, EventArgs e)
        {
            (sender as ThongTinKH).isQuayVe = false;
            (sender as ThongTinKH).Close();
            this.Show();
        }

        private void picSignUp_Click(object sender, EventArgs e)
        {
            CreateAccount CrAc = new CreateAccount();
            CrAc.Show();
            this.Hide();
            CrAc.QuayVe += CrAc_QuayVe;
        }

        private void CrAc_QuayVe(object sender, EventArgs e)
        {
            (sender as CreateAccount).isQuayVe = false;
            (sender as CreateAccount).Close();
            this.Show();
        }

        private void picUser_Click(object sender, EventArgs e)
        {
            ThongTinTaiKhoan TTTK = new ThongTinTaiKhoan();
            TTTK.Show();
            this.Hide();
            TTTK.QuayVe += TTTK_QuayVe;
        }

        private void TTTK_QuayVe(object sender, EventArgs e)
        {
            (sender as ThongTinTaiKhoan).isQuayVe = false;
            (sender as ThongTinTaiKhoan).Close();
            this.Show();
        }

        private void DesignDaoDien_Load(object sender, EventArgs e)
        {

        }
    }
}
