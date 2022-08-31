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
using System.Windows.Navigation;
using System.Windows.Shapes;
using ChaoTho.Models;
namespace ChaoTho
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            CbNhomHang();
            HienThi();
        }
        QuanLySanPhamDBContext db = new QuanLySanPhamDBContext();
        public void CbNhomHang()
        {
            var cbquery= from NH in db.NhomHangs
                         select NH.MaNhomHang;
            cbo.ItemsSource=cbquery.ToList();
        }
        public void HienThi()
        {
            var query = from NH in db.NhomHangs
                        join SP in db.SanPhams
                        on NH.MaNhomHang equals SP.MaNhomHang
                        orderby SP.SoLuongBan descending
                        select new
                        {
                            SP.MaSp,
                            SP.TenSanPham,
                            SP.DonGia,
                            SP.SoLuongBan,
                            SP.TienBan,
                            NH.MaNhomHang
                        };
            dgvSanPham.ItemsSource=query.ToList();
        }

        private void btnThem_Click(object sender, RoutedEventArgs e)
        {
            int n;
            bool slb = int.TryParse(txtSL.Text, out n);

            if (!slb)
                throw new Exception("Số lượng bán phải là số nguyên");

            var MaSP = (from SP in db.SanPhams
                        where SP.MaSp == int.Parse(txtMa.Text)
                        select SP.MaSp).SingleOrDefault();
            if (MaSP != 0)
                throw new Exception("Không được trùng mã sản phẩm");

            int sl = int.Parse(txtSL.Text);
            if (sl < 1)
                throw new Exception("Số lượn bán phải >1");

            SanPham spm = new SanPham();
            spm.MaSp = int.Parse(txtMa.Text);
            spm.TenSanPham = txtTen.Text;
            spm.DonGia=int.Parse(txtGia.Text);
            spm.SoLuongBan = int.Parse(txtSL.Text);
            spm.MaNhomHang=int.Parse(cbo.Text);

            db.SanPhams.Add(spm);
            db.SaveChanges();
            HienThi();

            MessageBox.Show("Thêm mới thành công");
     
        }

        private void btnTim_Click(object sender, RoutedEventArgs e)
        {
            Window1 Wd = new Window1();
            Wd.ShowDialog();
        }
    }
}
