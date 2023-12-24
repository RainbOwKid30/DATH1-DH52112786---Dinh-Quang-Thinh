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
    public partial class SuaThongTin : Form
    {
        private List<CKhachHang> ds_KH = new List<CKhachHang>();
        public SuaThongTin(string maKH,string hoKH ,string tenKH, string CMND, string DiaChi, string SDT, string gioitinh,string quoctich ,DateTime ngaysinh)
        {
            InitializeComponent();
        }

        public string MaKH { get; internal set; }
        public string HoKH { get; internal set; }
        public string TenKH { get; internal set; }
        public string CMND { get; internal set; }
        public string DiaChi { get; internal set; }
        public string SDT { get; internal set; }
        public string GioiTinh { get; internal set; }
        public string QuocTich { get; internal set; }

        public DateTime NgaySinh { get; internal set; }

        private void SuaThongTin_Load(object sender, EventArgs e)
        {

        }
        private void LuuDuLieu()
        {
            try
            {
                FileStream f = new FileStream("KhachHang.txt", FileMode.Create);
                BinaryFormatter bf = new BinaryFormatter();
                bf.Serialize(f, ds_KH);
                f.Close();
                MessageBox.Show("dữ liệu vừa cập nhật!!!");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi lưu dữ liệu: " + ex.Message);
            }
        }
        public void docfile1(string filename)
        {
            FileStream f = new FileStream(filename, FileMode.Open);
            BinaryFormatter bf = new BinaryFormatter();
            ds_KH = bf.Deserialize(f) as List<CKhachHang>;
            f.Close();

        }
        private CKhachHang timKH(string code)
        {
            foreach (CKhachHang item in ds_KH)
            {
                if (item.MaKH == code)
                    return item;
            }
            return null;
        }

        
        public void SetTextBoxHo(string text) { txtHo.Text = text; }
        public void SetTextBoxTen(string text) { txtTen.Text = text; }
        public void SetTextBoxCMND(string text) { txtCMND.Text = text; }
        public void SetTextBoxDiaChi(string text) { txtDiaChi.Text = text; }
        public void SetTextBoxSDT(string text) { txtSDT.Text = text; }
        public void SetTextBoxGioiTinh(string text) { txtGioiTinh.Text = text; }
        public void SetTextBoxQuocTich(string text) { txtQuocTich.Text = text; }
        public void SetTextBoxNgaySinh(DateTime date) {dtpNgaySinh.Value = date; }

        private void btnSua_Click(object sender, EventArgs e)
        {
            CKhachHang kh = new CKhachHang();
            HoKH = txtHo.Text;
            TenKH = txtTen.Text;
            CMND = txtCMND.Text;
            DiaChi = txtDiaChi.Text;
            SDT = txtSDT.Text;
            GioiTinh = txtGioiTinh.Text;
            QuocTich = txtQuocTich.Text;
            NgaySinh = dtpNgaySinh.Value.Date;
            docfile1("KhachHang.txt");
            if(timKH(MaKH) != null)
            {
                ds_KH.Add(kh);
                LuuDuLieu();
            }
            this.DialogResult = DialogResult.OK;
            
        }
    }
}
