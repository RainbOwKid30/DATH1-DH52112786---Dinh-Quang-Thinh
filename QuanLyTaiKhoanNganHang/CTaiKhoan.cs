using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyTaiKhoanNganHang
{
    [Serializable]
    class CTaiKhoan
    {
        private string m_sotaikhoan, m_sodu, m_hoten, m_cmnd, m_diachi,m_makh,m_loaiTK;
        
        public string SoTaiKhoan { get => m_sotaikhoan; set => m_sotaikhoan = value; }
        public string SoDu { get => m_sodu; set => m_sodu = value; }
        public string HoTen { get => m_hoten; set => m_hoten = value; }
        public string CMND { get => m_cmnd; set => m_cmnd = value; }
        public string DiaChi { get => m_diachi; set => m_diachi = value; }
        public string MaKH { get => m_makh; set => m_makh = value; }

        public string LoaiTK { get => m_loaiTK; set => m_loaiTK = value; }

        public CTaiKhoan()
        {
            m_sotaikhoan = "";
            m_sodu = "";
            m_hoten = "";
            m_cmnd = "";
            m_diachi = "";
            m_makh = "";
            m_loaiTK = "";



        }

        public CTaiKhoan(string sotaikhoan, string sodu, string hoten, string cmnd, string diachi, string makh,string loaitk)
        {
            m_sotaikhoan = sotaikhoan;
            m_sodu = sodu;
            m_hoten = hoten;
            m_cmnd = cmnd;
            m_diachi = diachi;
            m_makh = makh;
            m_loaiTK = loaitk;

        }
    }
}
