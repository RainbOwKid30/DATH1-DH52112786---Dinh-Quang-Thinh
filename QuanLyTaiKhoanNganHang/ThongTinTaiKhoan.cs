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
    public partial class ThongTinTaiKhoan : Form
    {
        private List<CTaiKhoan> ds_TTTK = new List<CTaiKhoan>();
        private List<CKhachHang> ds_KH = new List<CKhachHang>();
        public ThongTinTaiKhoan()
        {
            InitializeComponent();
        }
        public event EventHandler QuayVe;
        public bool isQuayVe = true;
        private void btnQuayLai_Click(object sender, EventArgs e)
        {
            QuayVe(this, new EventArgs());
        }

        private void ThongTinTaiKhoan_FormClosed(object sender, FormClosedEventArgs e)
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
                ds_TTTK = bf.Deserialize(f) as List<CTaiKhoan>;
                f.Close();

            }
            catch
            {
                ds_TTTK = new List<CTaiKhoan>();
            }
        }
        private void DocFileKH(string filename)
        {
            try
            {
                FileStream f = new FileStream(filename, FileMode.Open);
                BinaryFormatter bf = new BinaryFormatter();
                ds_KH = bf.Deserialize(f) as List<CKhachHang>;
                f.Close();

            }
            catch
            {
                ds_KH = new List<CKhachHang>();
            }
        }
        private void ThongTinTaiKhoan_Load(object sender, EventArgs e)
        {

            DocFileKH("KhachHang.txt");
            DocFile("TaiKhoan.txt");
            dgvKH.DataSource = ds_KH.ToArray();
            
            dgvKH.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvTaiKhoan.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            //dgvTaiKhoan.DataSource = ds_TTTK.ToArray();
            dgvTaiKhoan.Columns[0].Visible = false;
            dgvTaiKhoan.Columns[3].Visible = false;
            dgvTaiKhoan.Columns[4].Visible = false;

        }

        private void btnTim_Click(object sender, EventArgs e)
        {
            //string sotk = txtTim.Text;
            //List<CTaiKhoan> ketQuaTimKiem = new List<CTaiKhoan>();
            ////CTaiKhoan tk = new CTaiKhoan();
            //foreach (CTaiKhoan tk in ds_TTTK)
            //{
            //    if (tk.MaKH == sotk)
            //    {
            //        DocFile("TaiKhoan.txt");
            //        ketQuaTimKiem.Add(tk);

            //    }
            //}
            //dgvTaiKhoan.DataSource = ketQuaTimKiem;
            string maKH = txtTim.Text;
            List<CKhachHang> ketQuaTimKiem = new List<CKhachHang>();

            foreach (CKhachHang kh in ds_KH)
            {
                if (kh.MaKH == maKH)
                {
                    ketQuaTimKiem.Add(kh);
                }
            }

            if (ketQuaTimKiem.Count > 0)
            {
                dgvKH.DataSource = ketQuaTimKiem.ToArray();
                dgvKH.ClearSelection();
            }
            else
            {
                MessageBox.Show("Không tìm thấy tài khoản cho khách hàng có mã " + maKH);
            }

        }

        private void btnVeBanDau_Click(object sender, EventArgs e)
        {
            dgvTaiKhoan.DataSource = ds_TTTK.ToArray();
        }


        
        private void dgvKH_CellClick(object sender, DataGridViewCellEventArgs e)
        {

            if (e.RowIndex >= 0)
            {              
                // Lấy giá trị của cột "MaKH" tại dòng được chọn
                string selectedCustomerID = dgvKH.Rows[e.RowIndex].Cells["MaKH"].Value.ToString();

                // Lọc dữ liệu tài khoản dựa trên MaKH
                List<CTaiKhoan> filteredAccounts = ds_TTTK.Where(tk => tk.MaKH == selectedCustomerID).ToList();

                // Hiển thị dữ liệu tài khoản trong dgvAccounts
                dgvTaiKhoan.DataSource = filteredAccounts.ToArray();
            }
        }


        private void LuuDuLieu()
        {
            try
            {
                FileStream f = new FileStream("TaiKhoan.txt", FileMode.Create);
                BinaryFormatter bf = new BinaryFormatter();
                bf.Serialize(f, ds_TTTK);
                f.Close();
                MessageBox.Show("dữ liệu vừa cập nhật!!!");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi lưu dữ liệu: " + ex.Message);
            }
        }

        private void dgvTaiKhoan_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            dgvKH.DataSource = ds_KH.ToArray();
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            try
            {
                // Kiểm tra xem có dòng nào được chọn không
                if (dgvTaiKhoan.SelectedCells.Count > 0)
                {

                    // Lấy dòng được chọn
                    int select = dgvTaiKhoan.SelectedCells[0].RowIndex;
                    DataGridViewRow selectedRow = dgvTaiKhoan.Rows[select];

                    // Lấy thông tin từ các ô trong dòng được chọn
                    string maKH = selectedRow.Cells["MaKH1"].Value?.ToString();
                    string soTK = selectedRow.Cells["SoTK"].Value?.ToString();
                    string HoTen = selectedRow.Cells["HoTen"].Value?.ToString();
                    string cmnd = selectedRow.Cells["CMND"].Value?.ToString();
                    string diachi = selectedRow.Cells["DiaChi"].Value?.ToString();
                    string sodu = selectedRow.Cells["sodu"].Value?.ToString();
                    string loaiTK = selectedRow.Cells["LoaiTK"].Value?.ToString();

                    // Hiển thị form sửa thông tin
                    EditThongTinTaiKhoanKH formSua = new EditThongTinTaiKhoanKH(maKH, soTK, HoTen, cmnd, diachi, sodu, loaiTK);
                    formSua.SetTextBoxMa(maKH);
                    formSua.SetTextBoxSoTK(soTK);
                    formSua.SetTextBoxHoTen(HoTen);
                    formSua.SetTextBoxCMND(cmnd);
                    formSua.SetTextBoxDiaChi(diachi);
                    formSua.SetTextBoxSoDu(sodu);
                    formSua.SetTextBoxLoai(loaiTK);
                    if (formSua.ShowDialog() == DialogResult.OK)

                    {
                        // Cập nhật thông tin trong danh sách
                        // (ở đây giả sử dsKH_list là danh sách chứa thông tin khách hàng)
                        foreach (CTaiKhoan tk in ds_TTTK)
                        {

                            if (tk.MaKH == maKH)
                            {
                                tk.SoTaiKhoan = formSua.SoTaiKhoan;
                                tk.HoTen = formSua.HoTen;
                                tk.CMND = formSua.CMND;
                                tk.DiaChi = formSua.DiaChi;
                                tk.SoDu = formSua.SoDu;
                                tk.LoaiTK = formSua.LoaiTK;
                                // Cập nhật thông tin mới

                                break;
                            }
                        }

                        // Cập nhật lại DataGridView( có thể dùng kế thừa cho nó ngắn)

                        selectedRow.Cells["SoTK"].Value = formSua.SoTaiKhoan;
                        selectedRow.Cells["HoTen"].Value = formSua.HoTen;
                        selectedRow.Cells["CMND"].Value = formSua.CMND;
                        selectedRow.Cells["DiaChi"].Value = formSua.DiaChi;
                        selectedRow.Cells["sodu"].Value = formSua.SoDu;
                        selectedRow.Cells["LoaiTK"].Value = formSua.LoaiTK;
                        LuuDuLieu();
                                         
                    }

                }
                else
                {
                    MessageBox.Show("Chọn một dòng để sửa thông tin.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message);
            }
        }

        private void dgvTaiKhoan_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dgvKH_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
