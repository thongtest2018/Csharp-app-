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

namespace _1988089
{
    /// <summary>
    /// Interaction logic for PhoneBrand.xaml
    /// </summary>
    public partial class PhoneBrand : Window
    {
        static public bool editBrandScreenClosed;
        //List<Brand> brandsList;
        MYSHOPEntities db = new MYSHOPEntities();
        public PhoneBrand()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            updateBrandListView();
        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            if (BrandListView.SelectedItem != null)
            {
                var brand = BrandListView.SelectedItem as Brand;
                editBrandName editBrandScreen = new editBrandName(brand.ID);
                editBrandScreenClosed = false;
                editBrandScreen.ShowDialog();

                if (editBrandScreenClosed)
                {
                    var brandssss = new List<Brand>();
                    MYSHOPEntities db = new MYSHOPEntities();
                    brandssss = db.Brands.ToList();
                    BrandListView.ItemsSource = brandssss;
                }
            }
        }

        private void MenuItem_Click_1(object sender, RoutedEventArgs e)
        {
            if (BrandListView.SelectedItem != null)
            {
                var brand = BrandListView.SelectedItem as Brand;
                var phones = db.Products.Where(p => p.Brand_ID == brand.ID).ToList();
                if (phones.Count != 0)
                {
                    MessageBox.Show("Vui lòng xóa tất cả sản phẩm liên quan trước khi xóa hãng !", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
                else
                {
                    var deletebrandID = brand.ID;
                    Brand deleteBrand = db.Brands.Where(s => s.ID == deletebrandID).First();
                    db.Brands.Remove(deleteBrand);
                    db.SaveChanges();
                    updateBrandListView();
                }
            }
        }

        private void Image_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (brandName.Text == "")
            {
                MessageBox.Show("Vui lòng nhập tên hãng điện thoại", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            else
            {
                bool exist = false;
                var brand = new Brand();
                brand.Name = brandName.Text;
                var brandsCount = db.Brands.Where(b => b.Name.Contains(brand.Name)).ToList();
                if (brandsCount.Count != 0)
                {
                    exist = true;
                }
                if (exist)
                {
                    MessageBox.Show("Đã có hãng điện thoại này", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
                else
                {
                    db.Brands.Add(brand);
                    db.SaveChanges();
                    updateBrandListView();
                }
            }
        }

        void updateBrandListView()
        {
            List<Brand> brand = db.Brands.ToList();
            BrandListView.ItemsSource = brand;
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            MainWindow.phoneBrandScreenClosed = true;
        }
    }
}

