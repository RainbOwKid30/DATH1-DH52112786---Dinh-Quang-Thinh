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
    public partial class EditThongTinTaiKhoanKH : Form
    {
        public EditThongTinTaiKhoanKH(string makh, string sotk, string hoten, string cmnd, string diachi, string sodu, string loaitk)
        {
            InitializeComponent();
        }

        public string SoTaiKhoan { get ; set ; }
        public string SoDu { get; set ; }
        public string HoTen { get; set ; }
        public string CMND { get ; set ; }
        public string DiaChi { get ; set ; }
        public string MaKH { get ; set ; }

        public string LoaiTK { get ; set ; }

        public void SetTextBoxMa(string text) { txtMaKH.Text = text; }
        public void SetTextBoxSoTK(string text) { txtSoTK.Text = text; }
        public void SetTextBoxHoTen(string text) { txtHoTen.Text = text; }
        public void SetTextBoxCMND(string text) { txtCMND.Text = text; }
        public void SetTextBoxDiaChi(string text) { txtDiaChi.Text = text; }
        public void SetTextBoxSoDu(string text) { txtSoDu.Text = text; }
        
        public void SetTextBoxLoai(string text) { cmbLoaiTK.Text = text; }
        private void EditThongTinTaiKhoanKH_Load(object sender, EventArgs e)
        {
            cmbLoaiTK.Items.Add("Tiết Kiệm");
            cmbLoaiTK.Items.Add("Lãi xuất");
            cmbLoaiTK.Items.Add("Premium");
            txtMaKH.ReadOnly = true;
            txtSoTK.ReadOnly = true;
        }

        private List<CTaiKhoan> ds_TK = new List<CTaiKhoan>();
        private List<CKhachHang> ds_KH = new List<CKhachHang>();
        public bool docfileTK(string filename)
        {
            FileStream f = new FileStream(filename, FileMode.Open);
            BinaryFormatter bf = new BinaryFormatter();
            ds_TK = bf.Deserialize(f) as List<CTaiKhoan>;
            f.Close();
            return true;
        }
        private void LuuDuLieuTK()
        {
            try
            {
                FileStream f = new FileStream("TaiKhoan.txt", FileMode.Create);
                BinaryFormatter bf = new BinaryFormatter();
                bf.Serialize(f, ds_TK);
                f.Close();
                MessageBox.Show("dữ liệu vừa cập nhật!!!");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi lưu dữ liệu: " + ex.Message);
            }
        }

        private CTaiKhoan timTK(string code)
        {
            foreach (CTaiKhoan item in ds_TK)
            {
                if (item.MaKH == code)
                    return item;
            }
            return null;
        }
        private void btnSua_Click(object sender, EventArgs e)
        {
            CTaiKhoan tk = new CTaiKhoan();
            SoTaiKhoan = txtSoTK.Text;
            HoTen = txtHoTen.Text;
            CMND = txtCMND.Text;
            DiaChi = txtDiaChi.Text;
            SoDu = txtSoDu.Text;
            LoaiTK = cmbLoaiTK.Text;
            docfileTK("TaiKhoan.txt");
            if (timTK(MaKH) != null)
            {
                ds_TK.Add(tk);
                LuuDuLieuTK();
            }
            this.DialogResult = DialogResult.OK;
        }
    }
}
