using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using ChaoTho.Models;
namespace ChaoTho
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class Window1 : Window
    {
        public Window1()
        {
            InitializeComponent();
            HienThi();
        }
        QuanLySanPhamDBContext db = new QuanLySanPhamDBContext();
        public void HienThi()
        {
            var query = from NH in db.NhomHangs
                        join SP in db.SanPhams
                        on NH.MaNhomHang equals SP.MaNhomHang
                        orderby SP.SoLuongBan descending
                        where SP.MaNhomHang==1
                        select new
                        {
                            SP.MaSp,
                            SP.TenSanPham,
                            SP.DonGia,
                            SP.SoLuongBan,
                            SP.TienBan,
                            NH.MaNhomHang
                        };
            dgvSanPham.ItemsSource = query.ToList();
        }
    }
}
