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
    public partial class MainQuanLy : Form
    {
        List<CTaiKhoan> tk = new List<CTaiKhoan>();
        public MainQuanLy()
        {
            InitializeComponent();
        }

        private void btnDSNV_Click(object sender, EventArgs e)
        {
            DanhSachNhanVien dsNV = new DanhSachNhanVien();
            dsNV.Show();
            this.Hide();
            dsNV.QuayVe += DsNV_QuayVe;
        }

        private void DsNV_QuayVe(object sender, EventArgs e)
        {
            (sender as DanhSachNhanVien).isQuayVe = false;
            (sender as DanhSachNhanVien).Close();
            this.Show();
        }

        private void MainQuanLy_Load(object sender, EventArgs e)
        {

        }

        public event EventHandler BackHome;
        public bool isBackHome = true;

        private void btnBack_Click(object sender, EventArgs e)
        {
            BackHome(this, new EventArgs());
        }

        private void MainQuanLy_FormClosed(object sender, FormClosedEventArgs e)
        {
            if(isBackHome)
                Application.Exit();
        }

        private void btnDSTKNH_Click(object sender, EventArgs e)
        {
            DS_QL_TK_KH dsqlKH = new DS_QL_TK_KH();
            dsqlKH.Show();
            this.Hide();
            dsqlKH.QuayVe += DsqlKH_QuayVe;
        }

        private void DsqlKH_QuayVe(object sender, EventArgs e)
        {
            (sender as DS_QL_TK_KH).isQuayVe = false;
            (sender as DS_QL_TK_KH).Close();
            this.Show();
        }

        private void btnDSTTKH_Click(object sender, EventArgs e)
        {
            DS_QL_TT_KH dsql_TT = new DS_QL_TT_KH();
            dsql_TT.Show();
            this.Hide();
            dsql_TT.QuayVe += Dsql_QuayVe;

        }

        private void Dsql_QuayVe(object sender, EventArgs e)
        {
            (sender as DS_QL_TT_KH).isQuayVe = false;
            (sender as DS_QL_TT_KH).Close();
            this.Show();
        }

    }
}
