using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyTaiKhoanNganHang
{
    [Serializable]
    class CNhanVien
    {
        private string m_manv, m_hotennv, m_diachi, m_cmnd, m_sdt,m_pass;

        public string MaNV { get => m_manv; set => m_manv = value; }
        public string HoTenNV { get => m_hotennv; set => m_hotennv = value; }
        public string DiaChi { get => m_diachi; set => m_diachi = value; }
        public string CMND { get => m_cmnd; set => m_cmnd = value; }
        public string SDT { get => m_sdt; set => m_sdt = value; }
        public string Pass { get => m_pass; set => m_pass = value; }

        public CNhanVien()
        {
            m_manv = "";
            m_hotennv = "";
            m_diachi = "";
            m_cmnd = "";
            m_sdt = "";
            m_pass = "";
        }
        public CNhanVien(string manv, string hotennv, string diachi, string cmnd, string sdt, string pass)
        {
            m_manv = manv;
            m_hotennv = hotennv;
            m_diachi = diachi;
            m_cmnd = cmnd;
            m_sdt = sdt;
            m_pass = pass;
        }
    }
}
