using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace QuanLyTaiKhoanNganHang
{
    public partial class CreateAccountBeforeAdd : Form
    {
        private List<CTaiKhoan> ds_TK = new List<CTaiKhoan>();
        private List<CKhachHang> ds_KH = new List<CKhachHang>();
        public CreateAccountBeforeAdd()
        {
            InitializeComponent();
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
        private void CreateAccountBeforeAdd_Load(object sender, EventArgs e)
        {
            cmbLoaiTK.Items.Add("Tiết Kiệm");
            cmbLoaiTK.Items.Add("Lãi xuất");
            cmbLoaiTK.Items.Add("Premium");
            DocFile("TaiKhoan.txt");
            txtCMND.Visible = false;
            txtDiaChi.Visible = false;
        }
        //lấy dữ liệu textbox bên form thêm
        public void SetTextBox2Text(string text) { txtMaKH.Text = text; }
        public void SetTextBox3Text(string text) { txtHoTen.Text = text; }
        public void SetTextBox4Text(string text) { txtCMND.Text = text; }
        public void SetTextBox5Text(string text) { txtDiaChi.Text = text; }

        // quay trở về form thêm kh




        //doc file khách hàng
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
        // tìm dữ liệu khách hàng
        private CKhachHang timKH(string ma)
        {
            foreach (CKhachHang item in ds_KH)
            {
                if (item.MaKH == ma)
                    return item;
            }
            return null;
        }
        //lưu dữ liệu mới
        private void ghiFile(string filename)
        {
            FileStream f = new FileStream(filename, FileMode.Create);
            BinaryFormatter bf = new BinaryFormatter();
            bf.Serialize(f, ds_TK);
            f.Close();
        }
        //Tạo tài khoản
        private void btnSignUp_Click(object sender, EventArgs e)
        {
            CTaiKhoan tk = new CTaiKhoan();
            tk.SoTaiKhoan = txtTaiKhoan.Text;
            tk.HoTen = txtHoTen.Text;
            tk.CMND = txtCMND.Text;
            tk.DiaChi = txtSoDu.Text;
            tk.SoDu = txtSoDu.Text;
            tk.MaKH = txtMaKH.Text;
            tk.LoaiTK = cmbLoaiTK.SelectedItem.ToString();
            DocFile1("KhachHang.txt");
            if (timKH(tk.MaKH) != null)
            {
                ds_TK.Add(tk);
                MessageBox.Show("đã thêm 1 tài khoản mới cho khách hàng !!!");
                ghiFile("TaiKhoan.txt");


            }
            else
            {
                MessageBox.Show("Không có khách hàng này trong danh sách!!!");
            }
        }
    }
}
