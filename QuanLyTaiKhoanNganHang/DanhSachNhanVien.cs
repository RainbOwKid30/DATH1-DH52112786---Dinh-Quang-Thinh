using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QuanLyTaiKhoanNganHang
{
    [Serializable]
    public partial class DanhSachNhanVien : Form
    {
        private List<CNhanVien> dsNV_list = new List<CNhanVien>();
        public DanhSachNhanVien()
        {
            InitializeComponent();
        }
        private void hienthiDSNV()
        {
            dgvNhanVien.DataSource = dsNV_list.ToArray();
        }
        // thêm 1 nhân viên mới vào
        private CNhanVien timNV(string ma)
        {
            foreach (CNhanVien item in dsNV_list)
            {
                if (item.MaNV == ma)
                    return item;
            }
            return null;
        }
        
        private void btnThem_Click(object sender, EventArgs e)
        {
            CNhanVien nv = new CNhanVien();
            nv.MaNV = txtMaNV.Text;
            nv.HoTenNV = txtHoTenNV.Text;
            nv.DiaChi = txtDiaChi.Text;
            nv.CMND = txtCMND.Text;
            nv.SDT = txtSDT.Text;
            //nv.Pass = txtPass.Text;
            nv.Pass = ComputeMD5Hash(txtPass.Text);

            if (timNV(nv.MaNV) == null)
            {
                dsNV_list.Add(nv);
                hienthiDSNV();
            }
            else
                MessageBox.Show("Nhân viên này đã có trong danh sách!");
        }

        private void dgvNhanVien_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.RowIndex < dgvNhanVien.Rows.Count)
            {
                if (dgvNhanVien.Rows[e.RowIndex].Cells[0].Value != null)
                    txtMaNV.Text= dgvNhanVien.Rows[e.RowIndex].Cells[0].Value.ToString();
                else
                    txtMaNV.Text = "";
                if (dgvNhanVien.Rows[e.RowIndex].Cells[1].Value != null)
                    txtHoTenNV.Text = dgvNhanVien.Rows[e.RowIndex].Cells[1].Value.ToString();
                else
                    txtHoTenNV.Text = "";
                if (dgvNhanVien.Rows[e.RowIndex].Cells[2].Value != null)
                    txtDiaChi.Text = dgvNhanVien.Rows[e.RowIndex].Cells[2].Value.ToString();
                else
                    txtDiaChi.Text = "";
                if (dgvNhanVien.Rows[e.RowIndex].Cells[3].Value != null)
                    txtCMND.Text = dgvNhanVien.Rows[e.RowIndex].Cells[3].Value.ToString();
                else
                    txtCMND.Text = "";
                if (dgvNhanVien.Rows[e.RowIndex].Cells[4].Value != null)
                    txtSDT.Text = dgvNhanVien.Rows[e.RowIndex].Cells[4].Value.ToString();
                else
                    txtSDT.Text = "";
                //if (dgvNhanVien.Rows[e.RowIndex].Cells[5].Value != null)
                //    txtPass.Text = dgvNhanVien.Rows[e.RowIndex].Cells[5].Value.ToString();
                //else
                //    txtPass.Text = "";
            }
            else
                MessageBox.Show("Lỗi! Hàng không hợp lệ.");
        }
        // xóa nhân viên khỏi danh sách
        private void btnXoa_Click(object sender, EventArgs e)
        {
            string maNV = txtMaNV.Text;
            CNhanVien n = timNV(maNV);
            if (n == null) MessageBox.Show("");
            else if (MessageBox.Show("Bạn muốn xóa khách hàng này khỏi danh sách?", "Thong bao", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                foreach (CNhanVien item in dsNV_list)
                    if (item.MaNV == maNV)
                    {
                        dsNV_list.Remove(item);
                        hienthiDSNV();

                        return;
                    }
            }
        }

        // chỉnh sửa thông tin của nhân viên
        private void btnSua_Click(object sender, EventArgs e)
        {
            string maNV = txtMaNV.Text;
            CNhanVien n = timNV(maNV);
            if (n == null) MessageBox.Show("Không tìm thấy nhân viên này trong danh sách!!!");
            else
            {
                foreach (CNhanVien item in dsNV_list)
                    if (item.MaNV == maNV)
                    {
                        item.MaNV = txtMaNV.Text;
                        item.HoTenNV = txtHoTenNV.Text;
                        item.DiaChi = txtDiaChi.Text;
                        item.CMND = txtCMND.Text;
                        item.SDT = txtSDT.Text;
                        item.Pass = txtPass.Text;
                        hienthiDSNV();
                        MessageBox.Show("bạn đã sửa thành công!");
                    }
            }
        }

        // lưu thông tin của các nhân viên ra 1 file danh sách

        // mã hóa MD5( tạo ra key 32 bit)
        private string ComputeMD5Hash(string input)
        {
            using (MD5 md5 = MD5.Create())
            {
                //đổi sang utf8 
                byte[] inputBytes = Encoding.UTF8.GetBytes(input);
                // lưu trữ 
                byte[] hashBytes = md5.ComputeHash(inputBytes);

                StringBuilder sb = new StringBuilder();
                for (int i = 0; i < hashBytes.Length; i++)
                {
                    sb.Append(hashBytes[i].ToString("x2"));
                }

                return sb.ToString();
            }
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            
            FileStream f = new FileStream("DanhSachNhanVien.txt", FileMode.Create);
            BinaryFormatter bf = new BinaryFormatter();
            bf.Serialize(f, dsNV_list);
            f.Close();
            MessageBox.Show("Ghi dữ liệu thành công!");


        }
        // xem danh sách có những nhân viên nào
        private void btnHTDSNV_Click(object sender, EventArgs e)
        {
            try
            {
                FileStream f = new FileStream("DanhSachNhanVien.txt", FileMode.Open);
                BinaryFormatter bf = new BinaryFormatter();
                dsNV_list = bf.Deserialize(f) as List<CNhanVien>;
                dgvNhanVien.DataSource = dsNV_list;// đây là nó sẽ đưa dữ liệu trong file vào datagirdview
                f.Close();
                MessageBox.Show("Đọc dữ liệu thành công!");
            }
            catch
            {
                dsNV_list = new List<CNhanVien>();
            }
        }
        // tìm 1 nhân viên theo mã
        private void btTim_Click(object sender, EventArgs e)
        {
            string maNV = txtTim.Text;
            List<CNhanVien> ketQuaTimKiem = new List<CNhanVien>();

            foreach (CNhanVien tk in dsNV_list)
            {
                if (tk.MaNV == maNV)
                {
                    ketQuaTimKiem.Add(tk);
                }
            }

            dgvNhanVien.DataSource = ketQuaTimKiem;
        }

        private void btnQuayLai_Click(object sender, EventArgs e)
        {
            hienthiDSNV();
        }
        public event EventHandler QuayVe;
        public bool isQuayVe = true;
        private void btnLogOut_Click(object sender, EventArgs e)
        {
            QuayVe(this, new EventArgs());
        }

        private void DanhSachNhanVien_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (isQuayVe)
                Application.Exit();
        }

        private void DanhSachNhanVien_Load(object sender, EventArgs e)
        {
            dgvNhanVien.Columns[5].Visible = false;
        }
    }
}
