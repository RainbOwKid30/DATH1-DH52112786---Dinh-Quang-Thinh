using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QuanLyTaiKhoanNganHang
{
    internal static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new QLTaiKhoanNH());
            //Application.Run(new MainNhanVien());
            //Application.Run(new DanhSachTaiKhoan());

            //Application.Run(new DanhSachNhanVien());
            //Application.Run(new MainQuanLy());
            //Application.Run(new DS_QL_TK_KH());
            //Application.Run(new DS_QL_TT_KH());
            //Application.Run(new DesignDaoDien());
        }
    }
}
