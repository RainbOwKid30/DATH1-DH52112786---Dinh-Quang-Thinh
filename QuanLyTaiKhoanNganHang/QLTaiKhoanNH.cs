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
    public partial class QLTaiKhoanNH : Form
    {
        public QLTaiKhoanNH()
        {
            InitializeComponent();
        }

        public string GetValueFromFile()
        {
            string value = null;

            if (File.Exists("DanhSachNhanVien.txt"))
            {
                using (StreamReader reader = new StreamReader("DanhSachNhanVien.txt"))
                {
                    value = reader.ReadLine();
                }
            }
            else
            {
                // Xử lý trường hợp tệp không tồn tại
                Console.WriteLine("Tệp không tồn tại.");
            }

            return value;
        }
        private List<CNhanVien> dsNV_list = new List<CNhanVien>();

        // Đăng Nhập
        private string ComputeMD5Hash(string input)
        {
            using (MD5 md5 = MD5.Create())
            {
                byte[] inputBytes = Encoding.UTF8.GetBytes(input);
                byte[] hashBytes = md5.ComputeHash(inputBytes);

                StringBuilder sb = new StringBuilder();
                for (int i = 0; i < hashBytes.Length; i++)
                {
                    sb.Append(hashBytes[i].ToString("x2"));
                }

                return sb.ToString();
            }
        } 

        private void btnNV_Click(object sender, EventArgs e)
        {
            // Lưu trữ thông tin nhân viên
            using (FileStream fileStream = new FileStream("DanhSachNhanVien.txt", FileMode.Open))
            {
                BinaryFormatter bf = new BinaryFormatter();
                dsNV_list = bf.Deserialize(fileStream) as List<CNhanVien>;
            }
            DesignDaoDien main = new DesignDaoDien();

            bool isLoginSuccessful = false; 
            foreach (CNhanVien nv in dsNV_list) {
                string enteredPassword = txtMK.Text;
                // mã hóa ra dãy số ký tự đặc biệt r so sánh với pass có sẵn trong file
                string hashedEnteredPassword = ComputeMD5Hash(enteredPassword);
                if (txtTK.Text == nv.MaNV && hashedEnteredPassword == nv.Pass)
                {
                    isLoginSuccessful = true;
                    main.Show();
                    this.Hide();
                    main.QuayVe += Main_QuayVe;
                    break;
                }
            }
            if (!isLoginSuccessful)
            {
                MessageBox.Show("Tài Khoản hoặc mật khẩu không chính xác!!!");
            }

        }

        private void Main_QuayVe(object sender, EventArgs e)
        {
            (sender as DesignDaoDien).isQuayVe = false;
            (sender as DesignDaoDien).Close();
            this.Show();
        }

        private void QLTaiKhoanNH_Load(object sender, EventArgs e)
        {
        }

        private void btnQL_Click(object sender, EventArgs e)
        {
            MainQuanLy ql = new MainQuanLy();
            if (txtTK.Text == "quanly" && txtMK.Text == "@quanly")
            {
                ql.Show();
                this.Hide();
                ql.BackHome += Ql_BackHome;
            }
            else
                MessageBox.Show("Tài khoản hoặc mật khẩu không đúng!!!");

        }

        private void Ql_BackHome(object sender, EventArgs e)
        {
            (sender as MainQuanLy).isBackHome = false;
            (sender as MainQuanLy).Close();
            this.Show();
        }
    }
}
