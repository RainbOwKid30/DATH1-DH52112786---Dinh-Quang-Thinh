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

namespace QuanLyTaiKhoanNganHang
{
    [Serializable]
    public partial class DS_QL_TK_KH : Form
    {
        private List<CTaiKhoan> dsTK_list = new List<CTaiKhoan>();
        public DS_QL_TK_KH()
        {
            InitializeComponent();
        }

        public void hienthiDStk()
        {
            dgvTK.DataSource = dsTK_list.ToList();
        }
        private CTaiKhoan timKH(string stk)
        {
            foreach (CTaiKhoan item in dsTK_list)
            {
                if (item.SoTaiKhoan == stk)
                    return item;
            }
            return null;
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            CTaiKhoan cTaiKhoan = new CTaiKhoan();
            cTaiKhoan.SoTaiKhoan = txtSTK.Text;
            cTaiKhoan.HoTen = txtHoTen.Text;
            cTaiKhoan.CMND = txtCMND.Text;
            cTaiKhoan.DiaChi = txtDiaChi.Text;
            cTaiKhoan.SoDu = txtSoDu.Text;
            cTaiKhoan.MaKH = txtMaKH.Text;
            cTaiKhoan.LoaiTK = cmbLoaiTK.SelectedItem.ToString();
            if (timKH(cTaiKhoan.SoTaiKhoan) == null)
            {
                dsTK_list.Add(cTaiKhoan);
                hienthiDStk();
            }
            else
            {
                MessageBox.Show("Khách hàng này đã có trong DS!");
            }
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            string stk = txtSTK.Text;
            CTaiKhoan n = timKH(stk);
            if (n == null) MessageBox.Show("Không tìm thấy !!!");
            else
            {
                foreach (CTaiKhoan item in dsTK_list)
                    if (item.SoTaiKhoan == stk)
                    {
                        item.SoTaiKhoan = txtSTK.Text;
                        item.HoTen = txtHoTen.Text;
                        item.CMND = txtCMND.Text;
                        item.DiaChi = txtDiaChi.Text;
                        item.SoDu = txtSoDu.Text; 
                        item.MaKH = txtMaKH.Text;
                        item.LoaiTK = cmbLoaiTK.SelectedItem.ToString();
                        hienthiDStk();
                        MessageBox.Show("bạn đã sửa thành công!");
                    }
            }
        }

        private void btnLuuTK_Click(object sender, EventArgs e)
        {
            FileStream f = new FileStream("TaiKhoan.txt", FileMode.Create);
            BinaryFormatter bf = new BinaryFormatter();
            bf.Serialize(f, dsTK_list);
            f.Close();
            MessageBox.Show("Ghi dữ liệu thành công!");
        }

        private void btnDocDSTK_Click(object sender, EventArgs e)
        {
            try
            {
                FileStream f = new FileStream("TaiKhoan.txt", FileMode.Open);
                BinaryFormatter bf = new BinaryFormatter();
                dsTK_list = bf.Deserialize(f) as List<CTaiKhoan>;
                dgvTK.DataSource = dsTK_list;
                f.Close();
                MessageBox.Show("Đọc dữ liệu thành công!");
            }
            catch
            {
                dsTK_list = new List<CTaiKhoan>();
            }
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            string stk = txtSTK.Text;
            CTaiKhoan n = timKH(stk);
            if (n == null) MessageBox.Show("");
            else if (MessageBox.Show("Bạn muốn xóa khách hàng này khỏi danh sách?", "Thong bao", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                foreach (CTaiKhoan item in dsTK_list)
                    if (item.SoTaiKhoan == stk)
                    {
                        dsTK_list.Remove(item);
                        hienthiDStk();

                        return;
                    }
            }
        }
        public event EventHandler QuayVe;
        public bool isQuayVe = true;
        private void btnLogOut_Click(object sender, EventArgs e)
        {
            QuayVe(this, new EventArgs());
        }

        private void DS_QL_TK_KH_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (isQuayVe)
                Application.Exit();
        }

        private void btnTim_Click(object sender, EventArgs e)
        {
            string sotk = txtTim.Text;
            List<CTaiKhoan> ketQuaTimKiem = new List<CTaiKhoan>();

            foreach (CTaiKhoan tk in dsTK_list)
            {
                if (tk.MaKH == sotk)
                {
                    ketQuaTimKiem.Add(tk);
                }
            }

            dgvTK.DataSource = ketQuaTimKiem;
        }

        private void DS_QL_TK_KH_Load(object sender, EventArgs e)
        {
            cmbLoaiTK.Items.Add("Tiết Kiệm");
            cmbLoaiTK.Items.Add("Lãi xuất");
            cmbLoaiTK.Items.Add("Premium");
        }

        private void button1_Click(object sender, EventArgs e)
        {
            hienthiDStk();
        }
        
        private void dgvTK_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            CTaiKhoan currentTK = new CTaiKhoan();
            if (e.RowIndex != -1)
            {

                currentTK = (CTaiKhoan)dgvTK.Rows[e.RowIndex].DataBoundItem;
                txtSTK.Text = currentTK.SoTaiKhoan.ToString();
                txtHoTen.Text = currentTK.HoTen.ToString();
                txtMaKH.Text = currentTK.MaKH.ToString();
                txtCMND.Text = currentTK.CMND.ToString();
                txtDiaChi.Text = currentTK.DiaChi.ToString();
                txtSoDu.Text = currentTK.SoDu.ToString();
                cmbLoaiTK.Text = currentTK.LoaiTK.ToString();

            }
        }
    }
}
