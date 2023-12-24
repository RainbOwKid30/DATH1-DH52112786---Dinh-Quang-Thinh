using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyTaiKhoanNganHang
{
    [Serializable]
    class CKhachHang
    {
    
        private string m_makh, m_hokh, m_tenkh, m_cmnd, m_diachi, m_sdt, m_gioitinh, m_quoctich;

        private DateTime m_ngaysinh;
        private List<CTaiKhoan> ds_TaiKhoan;

        public string MaKH { get => m_makh; set => m_makh = value; }
        public string HoKH { get => m_hokh; set => m_hokh = value; }
        public string TenKH { get => m_tenkh; set => m_tenkh = value; }
        public string CMND { get => m_cmnd; set => m_cmnd = value; }
        public string DiaChi { get => m_diachi; set => m_diachi = value; }
        public string SDT { get => m_sdt; set => m_sdt = value; }
        public string GioiTinh { get => m_gioitinh; set => m_gioitinh = value; }
        public string QuocTich { get => m_quoctich; set => m_quoctich = value; }
        public DateTime NgaySinh { get => m_ngaysinh; set => m_ngaysinh = value; }
        public List<CTaiKhoan> Ds_TaiKhoan
        {
            get { return ds_TaiKhoan; }
        }

        public CKhachHang()
        {
            m_makh = "";
            m_hokh = "";
            m_tenkh = "";
            m_cmnd = "";
            m_diachi = "";
            m_sdt = "";
            m_gioitinh = "";
            m_quoctich = "";
            m_ngaysinh = DateTime.Now;
            ds_TaiKhoan = new List<CTaiKhoan>();
        }
        public CKhachHang(string makh, string hokh, string tenkh, string cmnd, string diachi, string sdt, string gioitinh, string quoctich, DateTime ngaysinh)
        {
            m_makh = makh;
            m_hokh = hokh;
            m_tenkh = tenkh;
            m_cmnd = cmnd;
            m_diachi = diachi;
            m_sdt = sdt;
            m_gioitinh = gioitinh;
            m_quoctich = quoctich;
            m_ngaysinh = ngaysinh;
            ds_TaiKhoan = new List<CTaiKhoan>();
        }
    }
}
