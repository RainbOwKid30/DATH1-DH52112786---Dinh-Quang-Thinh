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
    public partial class ThongTinKH : Form
    {
        private List<CKhachHang> dsKH_list = new List<CKhachHang>();
        public ThongTinKH()
        {
            InitializeComponent();
        }
        private void hienThiDSKhachHang()
        {
            dgvKH.DataSource = dsKH_list.ToArray();
        }
        public void docfile(string filename)
        {
            FileStream f = new FileStream(filename, FileMode.Open);
            BinaryFormatter bf = new BinaryFormatter();
            dsKH_list = bf.Deserialize(f) as List<CKhachHang>;
            hienThiDSKhachHang();
            f.Close();

        }
        private void ThongTinKH_Load(object sender, EventArgs e)
        {
            this.AutoSize = true;
            try
            {
                docfile("KhachHang.txt");
                    
            }
            catch (Exception)
            {
                MessageBox.Show("File không tồn tại!!!");
            }
            
        }

        private void btnTim_Click(object sender, EventArgs e)
        {
            string maNV = txtTim.Text;
            List<CKhachHang> ketQuaTimKiem = new List<CKhachHang>();

            foreach (CKhachHang tk in dsKH_list)
            {
                if (tk.MaKH == maNV)
                {
                    ketQuaTimKiem.Add(tk);
                }
            }

            dgvKH.DataSource = ketQuaTimKiem;
        }

        private void btnVeBanDau_Click(object sender, EventArgs e)
        {
            hienThiDSKhachHang();
        }

        public event EventHandler QuayVe;
        public bool isQuayVe = true;
        private void btnQuayLai_Click(object sender, EventArgs e)
        {
            QuayVe(this, new EventArgs());
            
        }

        private void ThongTinKH_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (isQuayVe)
                Application.Exit();
        }   
    }
}
