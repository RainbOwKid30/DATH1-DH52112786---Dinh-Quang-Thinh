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
    public partial class ThemKhachHang : Form
    {

        private List<CKhachHang> dsKH_list = new List<CKhachHang>();
        public ThemKhachHang()
        {
            InitializeComponent();
        }
        // Trở Lại form DesignDaoDien
        public event EventHandler QuayVe;
        public bool isQuayVe = true;
        private void btnQuayLai_Click(object sender, EventArgs e)
        {
            QuayVe(this, new EventArgs());
            ghiFile("KhachHang.txt");
        }

        private void ThemKhachHang_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (isQuayVe)
                Application.Exit();
        }

        private void ThemKhachHang_Load(object sender, EventArgs e)
        {
            DocFile("KhachHang.txt");
            

        }

        private CKhachHang timKH(string maKH)
        {
            foreach (CKhachHang item in dsKH_list)
            {
                if (item.MaKH == maKH)
                    return item;
            }
            return null;
        }
        private void ghiFile(string filename)
        {
            FileStream f = new FileStream(filename, FileMode.Create);
            BinaryFormatter bf = new BinaryFormatter();
            bf.Serialize(f, dsKH_list);
            f.Close();
        }
        private void DocFile(string filename)
        {
            try
            {
                FileStream f = new FileStream(filename, FileMode.Open);
                BinaryFormatter bf = new BinaryFormatter();
                dsKH_list = bf.Deserialize(f) as List<CKhachHang>;
                f.Close();
               
            }
            catch
            {
                dsKH_list = new List<CKhachHang>();
            }
        }

        public string GetTextBox1(){ return txtMaKH.Text; }
        public string GetTextBox2() { return txtHo.Text +" "+ txtTen.Text; }
        public string GetTextBox3() { return txtCMND.Text; }
        public string GetTextBox4() { return txtDiaChi.Text; }
        private void btnThem_Click(object sender, EventArgs e)
        {
            CKhachHang dskh = new CKhachHang();
            dskh.MaKH = txtMaKH.Text;
            dskh.HoKH = txtHo.Text;
            dskh.TenKH = txtTen.Text;
            dskh.CMND = txtCMND.Text;
            dskh.DiaChi = txtDiaChi.Text;
            dskh.SDT = txtSDT.Text; 
            dskh.GioiTinh = txtGioiTinh.Text;
            dskh.QuocTich = txtQuocTich.Text;
            dskh.NgaySinh = dtpNgaySinh.Value.Date;
            if (timKH(dskh.MaKH) == null && timKH(dskh.CMND) == null)
            {
                dsKH_list.Add(dskh);
                MessageBox.Show("Đã thêm 1 khách hàng!!!");
                if (MessageBox.Show("Bạn muốn tạo tài khoản cho khách hàng này?", "Thong bao", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    ghiFile("KhachHang.txt");
                    CreateAccountBeforeAdd frmcr = new CreateAccountBeforeAdd();
                    
                    string ma = GetTextBox1();
                    string hoten = GetTextBox2();
                    string cmnd = GetTextBox3();
                    string diachi = GetTextBox4();
                    frmcr.SetTextBox2Text(ma);
                    frmcr.SetTextBox3Text(hoten);
                    frmcr.SetTextBox4Text(cmnd);
                    frmcr.SetTextBox5Text(diachi);
                    frmcr.ShowDialog();
                }
            }
            else
            {
                if (timKH(dskh.MaKH) != null)
                    MessageBox.Show("Khách hàng này đã có trong DS!");
                else if(timKH(dskh.CMND) != null )
                {
                    MessageBox.Show("Số CMND đã có trong danh sách!!!");
                }
                else
                    MessageBox.Show("Số CMND hoặc mã khách hàng đã có trong danh sách!!!");
            }
        }


        private void btnQuayLaiTK_Click(object sender, EventArgs e)
        {
            QuayVe(this, new EventArgs());
        }
    }
}
