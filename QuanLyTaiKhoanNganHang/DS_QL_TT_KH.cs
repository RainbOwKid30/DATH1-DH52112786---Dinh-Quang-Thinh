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
    public partial class DS_QL_TT_KH : Form
    {
        private List<CKhachHang> dsKH_list = new List<CKhachHang>();
        private List<CTaiKhoan> dstk_list = new List<CTaiKhoan>();
        public DS_QL_TT_KH()
        {
            InitializeComponent();
            btnThem.Click += btnAddRow_Click;
            btnXoa.Click += btnXoa_Click;
            dgvKhachHang.UserDeletingRow += dgvKhachHang_UserDeletingRow;
        }
        private void LuuDuLieu()
        {
            try
            {
                FileStream f = new FileStream("KhachHang.txt", FileMode.Create);
                BinaryFormatter bf = new BinaryFormatter();
                bf.Serialize(f, dsKH_list);
                f.Close();
                MessageBox.Show("dữ liệu vừa cập nhật!!!");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi lưu dữ liệu: " + ex.Message);
            }
        }
        private void LuuTaiKHoan()
        {
            try
            {
                FileStream f = new FileStream("TaiKhoan.txt", FileMode.Create);
                BinaryFormatter bf = new BinaryFormatter();
                bf.Serialize(f, dstk_list);
                f.Close();
                
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi lưu dữ liệu: " + ex.Message);
            }
        }
        public bool docfileTK(string filename)
        {
            FileStream f = new FileStream(filename, FileMode.Open);
            BinaryFormatter bf = new BinaryFormatter();
            dstk_list = bf.Deserialize(f) as List<CTaiKhoan>;
            f.Close();
            return true;
        }

        private void dgvKhachHang_UserDeletingRow(object sender, DataGridViewRowCancelEventArgs e)
        {
            DialogResult result = MessageBox.Show("Bạn có chắc muốn xóa dòng này không?", "Xác nhận xóa", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

            if (result == DialogResult.No)
            {
                // Cancel the row deletion if the user chooses not to delete
                e.Cancel = true;
            }
            else
            {
                // If the user confirms deletion, remove the corresponding object from the data source
                string maKHToRemove = e.Row.Cells["MaKH"].Value?.ToString()?.Trim();
                CKhachHang khachHangToRemove = dsKH_list.FirstOrDefault(kh => kh.MaKH == maKHToRemove);

                if (khachHangToRemove != null)
                {
                    dsKH_list.Remove(khachHangToRemove);
                    LuuDuLieu(); // Save changes to the file or database after deletion
                }
            }
        }
        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Bạn muốn xóa khách hàng này khỏi danh sách?", "Thong bao", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                if (dgvKhachHang.SelectedRows.Count > 0)
                {
                    // Lấy hàng đã chọn
                    DataGridViewRow selectedRow = dgvKhachHang.SelectedRows[0];

                    // Đảm bảo rằng hàng đã chọn chứa ô hợp lệ trong cột "MaKH"
                    object maKHObject = selectedRow.Cells[0].Value;

                    if (maKHObject != null )
                    {
                        // Chuyển đổi giá trị thành chuỗi và cắt bớt mọi khoảng trắng ở đầu/cuối
                        string maKHToRemove = maKHObject.ToString().Trim();
                        // Tìm đối tượng CKhachHang trong danh sách với MaKH được chỉ định
                        CKhachHang khachHangToRemove = dsKH_list.FirstOrDefault(kh => kh.MaKH == maKHToRemove);
                        
                        docfileTK("TaiKhoan.txt");

                        for (int i = dstk_list.Count - 1; i >= 0; i--)
                        {
                            CTaiKhoan tk = dstk_list[i];
                            if (tk.MaKH == khachHangToRemove.MaKH)
                            {
                                dstk_list.RemoveAt(i);
                                LuuTaiKHoan();
                            }
                        }

                        // Kiểm tra xem đối tượng CKhachHang đã được tìm thấy chưa
                        if (khachHangToRemove != null)
                        {
                            // Xóa đối tượng CKhachHang khỏi danh sách
                            dsKH_list.Remove(khachHangToRemove);
                            
                            
                            // Làm mới DataGridView để phản ánh những thay đổi trong nguồn dữ liệu
                            hienThiDSKhachHang();
                            LuuDuLieu();// Save changes to the file or database
                        }
                        else
                            MessageBox.Show($"Không tìm thấy khách hàng với mã {maKHToRemove} trong danh sách.");
                    }
                    else
                        MessageBox.Show("Dòng đã chọn không có giá trị hợp lệ trong cột 'MaKH'.");
                }
                else
                    MessageBox.Show("Vui lòng chọn một hàng để xóa.");
            }
        }

        private void hienThiDSKhachHang()
        {
            dgvKhachHang.DataSource = null;  // Clear the previous data source
            //dgvKhachHang.DataSource = dsKH_list;
            dgvKhachHang.DataSource = dsKH_list.ToArray();
        }

        private void DS_QL_TT_KH_Load(object sender, EventArgs e)
        {
            try {
                if (docfile("KhachHang.txt") == true)
                {
                    hienThiDSKhachHang();
                }

            }
            catch (Exception) {
                MessageBox.Show("File không tồn tại!!!");
            }
        }

        public bool docfile(string filename)
        {
            FileStream f = new FileStream(filename, FileMode.Open);
            BinaryFormatter bf = new BinaryFormatter();
            dsKH_list = bf.Deserialize(f) as List<CKhachHang>;
            f.Close();
            return true;
        }

        private CKhachHang timKH(string ma)
        {
            foreach (CKhachHang item in dsKH_list)
            {
                if (item.MaKH == ma)
                    return item;
            }
            return null;
        }

        private void btnAddRow_Click(object sender, EventArgs e)
        {
            try
            {
                // Tạo một đối tượng CKhachHang mới với các giá trị mặc định 
                CKhachHang newKhachHang = new CKhachHang();

                // Thêm đối tượng CKhachHang mới vào dữ liệu ban đầu (dsKH_list)
                dsKH_list.Add(newKhachHang);

                // Làm mới DataGridView để phản ánh những thay đổi nguồn dữ liệu cũ đã lưu trước đó
                hienThiDSKhachHang();

                // Tùy chọn, di chuyển vùng chọn sang hàng mới được thêm vào
                dgvKhachHang.CurrentCell = dgvKhachHang.Rows[dgvKhachHang.Rows.Count - 1].Cells[0];
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred: " + ex.Message);
            }
        }



        private void dgvKhachHang_UserAddedRow(object sender, DataGridViewRowEventArgs e)
        {


        }
        public event EventHandler QuayVe;
        public bool isQuayVe = true;
        private void btnQuayLai_Click(object sender, EventArgs e)
        {
            QuayVe(this, new EventArgs());
        }

        private void DS_QL_TT_KH_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (isQuayVe)
                Application.Exit();
        }

        private void dgvKhachHang_RowEnter(object sender, DataGridViewCellEventArgs e)
        {

        }
   
        private CKhachHang timTTKH(string code)
        {
            foreach (CKhachHang item in dsKH_list)
            {
                if (item.MaKH == code || item.CMND == code)
                    return item;
            }
            return null;
        }

        public void docfile1(string filename)
        {
            FileStream f = new FileStream(filename, FileMode.Open);
            BinaryFormatter bf = new BinaryFormatter();
            dsKH_list = bf.Deserialize(f) as List<CKhachHang>;
            f.Close();
            
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            try
            {
                // Lấy mã khách hàng từ ô cuối cùng trong cột "MaKH" của dòng mới thêm vào, và lấy các giá trị cuối cùng trong dòng đó
                string makh = dgvKhachHang.Rows[dgvKhachHang.Rows.Count - 1].Cells["MaKH"].Value?.ToString();
                string hokh = dgvKhachHang.Rows[dgvKhachHang.Rows.Count - 1].Cells["HoKH"].Value?.ToString();
                string tenkh = dgvKhachHang.Rows[dgvKhachHang.Rows.Count - 1].Cells["TenKH"].Value?.ToString();
                string cmnd = dgvKhachHang.Rows[dgvKhachHang.Rows.Count - 1].Cells["CMND"].Value?.ToString();
                string diachi = dgvKhachHang.Rows[dgvKhachHang.Rows.Count - 1].Cells["DiaChi"].Value?.ToString();
                string sdt = dgvKhachHang.Rows[dgvKhachHang.Rows.Count - 1].Cells["SDT"].Value?.ToString();
                string gioitinh = dgvKhachHang.Rows[dgvKhachHang.Rows.Count - 1].Cells["GioiTinh"].Value?.ToString();
                string quoctich = dgvKhachHang.Rows[dgvKhachHang.Rows.Count - 1].Cells["QuocTich"].Value?.ToString();
                string ngaysinhstring = dgvKhachHang.Rows[dgvKhachHang.Rows.Count - 1].Cells["NgaySinh"].Value?.ToString();
                CKhachHang ma = new CKhachHang();
                // đọc file chứa dữ liệu vào để đem so sánh với cái mã đó.
                docfile1("KhachHang.txt");
                
                if (DateTime.TryParse(ngaysinhstring, out DateTime ngaysinh))// đổi ngày sinh kiểu chuổi sáng kiểu date
                {
                    ma.MaKH = makh;
                    ma.HoKH = hokh;
                    ma.TenKH = tenkh;
                    ma.CMND = cmnd;
                    ma.DiaChi = diachi;
                    ma.SDT = sdt;
                    ma.GioiTinh = gioitinh;
                    ma.QuocTich = quoctich;
                    ma.NgaySinh = ngaysinh;
                    if (timTTKH(makh) == null)
                    {
                        if (timTTKH(cmnd) == null)
                        {   // Ghi dữ liệu vào tệp
                            dsKH_list.Add(ma);
                            LuuDuLieu();
                        }
                        else
                            MessageBox.Show("CMND này đã có người dùng!!!");
                    }
                    else
                        MessageBox.Show("Khách hàng đã có trong danh sách!!!");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message);
            }
        }
        
        private void btnSua_Click(object sender, EventArgs e)
        {
            try
            {
                // Kiểm tra xem có dòng nào được chọn không
                if (dgvKhachHang.SelectedRows.Count > 0)
                {
                    // Lấy dòng được chọn
                    DataGridViewRow selectedRow = dgvKhachHang.SelectedRows[0];

                    // Lấy thông tin từ các ô trong dòng được chọn
                    string maKH = selectedRow.Cells["MaKH"].Value?.ToString();
                    string hoKH = selectedRow.Cells["HoKH"].Value?.ToString();
                    string tenKH = selectedRow.Cells["TenKH"].Value?.ToString();
                    string cmnd = selectedRow.Cells["CMND"].Value?.ToString();
                    string diachi = selectedRow.Cells["DiaChi"].Value?.ToString();
                    string sodienthoai = selectedRow.Cells["SDT"].Value?.ToString();
                    string gioitinh = selectedRow.Cells["GioiTinh"].Value?.ToString();
                    string quoctich = selectedRow.Cells["QuocTich"].Value?.ToString();
                    string ngaysinhstring = selectedRow.Cells["NgaySinh"].Value?.ToString();

                    // Hiển thị form sửa thông tin
                    if (DateTime.TryParse(ngaysinhstring, out DateTime ngaysinh))
                    {
                        SuaThongTin formSua = new SuaThongTin(maKH, hoKH, tenKH, cmnd, diachi, sodienthoai, gioitinh, quoctich, ngaysinh);
                        formSua.SetTextBoxHo(hoKH);
                        formSua.SetTextBoxTen(tenKH);
                        formSua.SetTextBoxCMND(cmnd);
                        formSua.SetTextBoxDiaChi(diachi);
                        formSua.SetTextBoxSDT(sodienthoai);
                        formSua.SetTextBoxGioiTinh(gioitinh);
                        formSua.SetTextBoxQuocTich(quoctich);
                        formSua.SetTextBoxNgaySinh(ngaysinh);
                        if (formSua.ShowDialog() == DialogResult.OK)
                        {

                            // Cập nhật thông tin trong danh sách
                            // (ở đây giả sử dsKH_list là danh sách chứa thông tin khách hàng)
                            foreach (CKhachHang kh in dsKH_list)
                            {
                                if (kh.MaKH == maKH)
                                {
                                    // Cập nhật thông tin mới
                                    kh.HoKH = formSua.HoKH;
                                    kh.TenKH = formSua.TenKH;
                                    kh.CMND = formSua.CMND;
                                    kh.DiaChi = formSua.DiaChi;
                                    kh.SDT = formSua.SDT;
                                    kh.GioiTinh = formSua.GioiTinh;
                                    kh.QuocTich = formSua.QuocTich;
                                    kh.NgaySinh = formSua.NgaySinh;
                                    break;
                                }
                            }

                            // Cập nhật lại DataGridView( có thể dùng kế thừa cho nó ngắn)
                       
                            selectedRow.Cells["HoKH"].Value = formSua.HoKH;
                            selectedRow.Cells["TenKH"].Value = formSua.TenKH;
                            selectedRow.Cells["CMND"].Value = formSua.CMND;
                            selectedRow.Cells["DiaChi"].Value = formSua.DiaChi;
                            selectedRow.Cells["SDT"].Value = formSua.SDT;
                            selectedRow.Cells["GioiTinh"].Value = formSua.GioiTinh;
                            selectedRow.Cells["QuocTich"].Value = formSua.QuocTich;
                            selectedRow.Cells["NgaySinh"].Value = formSua.NgaySinh.ToString("dd/MM/yyyy");
                            LuuDuLieu();
                        }
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
        // fix lỗi phát sinh ngoài ý muốn 
        private void dgvKhachHang_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            // Ngăn chặn hiển thị hộp thoại lỗi mặc định
            e.Cancel = true;

            // Xử lý lỗi dữ liệu ở đây
            //MessageBox.Show($"Lỗi xảy ra trong cột {e.ColumnIndex + 1}, hàng {e.RowIndex + 1}: {e.Exception.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
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

            dgvKhachHang.DataSource = ketQuaTimKiem;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            hienThiDSKhachHang();
        }

        private void btnThem_Click(object sender, EventArgs e)
        {

        }

        private void btnXoa_Click_1(object sender, EventArgs e)
        {

        }
    }
}
