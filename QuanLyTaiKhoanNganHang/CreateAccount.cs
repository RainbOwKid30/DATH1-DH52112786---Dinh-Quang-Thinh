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
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace QuanLyTaiKhoanNganHang
{
    public partial class CreateAccount : Form
    {
        private List<CTaiKhoan> ds_TK = new List<CTaiKhoan>();
        private List<CKhachHang> ds_KH = new List<CKhachHang>();
        public CreateAccount()
        {
            InitializeComponent();
        }

        public event EventHandler QuayVe;
        public bool isQuayVe = true;

        private void btnQuayLai_Click(object sender, EventArgs e)
        {
            QuayVe(this, new EventArgs());
            ghiFile("TaiKhoan.txt");
        }

        private void CreateAccount_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (isQuayVe)
                Application.Exit();
        }
        private void DocFile(string filename)
        {
            try
            {
                FileStream f = new FileStream(filename, FileMode.Open);
                BinaryFormatter bf = new BinaryFormatter();
                ds_TK = bf.Deserialize(f) as List<CTaiKhoan>;
                //ds_KhachHang = bf.Deserialize(f) as Dictionary<string, List<CTaiKhoan>>;
                f.Close();

            }
            catch
            {
                ds_TK = new List<CTaiKhoan>();
                //ds_KhachHang = new Dictionary<string, List<CTaiKhoan>>();
            }
        }

        private void DocFile1(string filename)
        {
            try
            {
                FileStream f = new FileStream(filename, FileMode.Open);
                BinaryFormatter bf = new BinaryFormatter();
                ds_KH = bf.Deserialize(f) as List<CKhachHang>;
                //ds_KhachHang = bf.Deserialize(f) as Dictionary<string, List<CTaiKhoan>>;
                f.Close();

            }
            catch
            {
                ds_KH = new List<CKhachHang>();
                //ds_KhachHang = new Dictionary<string, List<CTaiKhoan>>();
            }

        }


        

        private void CreateAccount_Load(object sender, EventArgs e)
        {
            cmbLoaiTK.Items.Add("Tiết Kiệm");
            cmbLoaiTK.Items.Add("Lãi xuất");
            cmbLoaiTK.Items.Add("Premium");
            DocFile("TaiKhoan.txt");
            DocFile1("KhachHang.txt");

            cmbMaKH.DataSource = ds_KH;
            cmbMaKH.DisplayMember = "MaKH";

            txtCMND.Visible = false;
            txtDiaChi.Visible = false;

           


        }

      
        private void ghiFile(string filename)
        {
            FileStream f = new FileStream(filename, FileMode.Create);
            BinaryFormatter bf = new BinaryFormatter();
            bf.Serialize(f, ds_TK);
            f.Close();
        }
        private CTaiKhoan timTK(string makh)
        {
            foreach (CTaiKhoan item in ds_TK)
            {
                if (item.MaKH == makh)
                    return item;
            }
            return null;
        }
        private CKhachHang timKH(string ma)
        {
            foreach (CKhachHang item in ds_KH)
            {
                if (item.MaKH ==  ma)
                    return item;
            }
            return null;
        }

        private void btnSignUp_Click(object sender, EventArgs e)
        {
            CTaiKhoan tk = new CTaiKhoan();
            
            tk.SoTaiKhoan = txtTaiKhoan.Text;
            tk.HoTen = txtHoTen.Text;
            tk.CMND = txtCMND.Text;
            tk.DiaChi = txtDiaChi.Text;
            tk.SoDu = txtSoDu.Text;
            tk.MaKH = (cmbMaKH.SelectedItem as CKhachHang)?.MaKH;
            tk.LoaiTK = cmbLoaiTK.SelectedItem.ToString();
            DocFile1("KhachHang.txt");
            if (timKH(tk.MaKH) != null)
            {
                ds_TK.Add(tk);
                MessageBox.Show("đã thêm 1 tài khoản mới cho khách hàng đã tồn tại!!!");
                ghiFile("TaiKhoan.txt");


            }
            else
            {
                MessageBox.Show("Không có khách hàng này trong danh sách!!!");
            }


        }

        private void Frm_QuayVe(object sender, EventArgs e)
        {
            (sender as ThemKhachHang).isQuayVe = false;
            (sender as ThemKhachHang).Close();
            this.Show();
        }

        private void cmbMaKH_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Khi chọn một khách hàng, hiển thị thông tin của khách hàng đó
            CKhachHang selectedKhachHang = cmbMaKH.SelectedItem as CKhachHang;

            if (selectedKhachHang != null)
            {
                // Hiển thị thông tin trong các TextBox hoặc control khác
                txtHoTen.Text = selectedKhachHang.HoKH + selectedKhachHang.TenKH;
                txtCMND.Text = selectedKhachHang.CMND;
                txtDiaChi.Text = selectedKhachHang.DiaChi;

            }
        }
    }
}
