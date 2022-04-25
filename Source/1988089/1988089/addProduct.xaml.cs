using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
    /// Interaction logic for addProduct.xaml
    /// </summary>
    /// 
    public partial class addProduct : Window
    {
        CultureInfo cultureInfo = CultureInfo.GetCultureInfo("vi-VN");
        string filename = "";
        byte[] ImageData;
        List<Brand> brands;
        MYSHOPEntities db = new MYSHOPEntities();
        
        ImagePath path;

        public addProduct()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            brands = db.Brands.ToList();
            brandListBox.ItemsSource = brands;
            path = new ImagePath();
            path.path = "/Images/imageholder.png";
            this.DataContext = path;
        }

        private void cancelButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Image_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            var dialog = new OpenFileDialog();
            if(dialog.ShowDialog() == true)
            {
                filename = dialog.FileName;

                var image = new BitmapImage(new Uri(filename, UriKind.Absolute));
                var encoder = new JpegBitmapEncoder();
                encoder.Frames.Add(BitmapFrame.Create(image));

                using (var stream = new MemoryStream())
                {
                    encoder.Save(stream);
                    ImageData = stream.ToArray();
                }
                labelAddImage.Text = $"Đã chọn {filename}";
                path.path = filename;
            }
        }

        private void addButton_Click(object sender, RoutedEventArgs e)
        {
            if(filename == string.Empty)
            {
                MessageBox.Show("Chưa chọn hình ảnh cho sản phẩm");
            }
            else if(brandListBox.SelectedItem as Brand == null)
            {
                MessageBox.Show("Chưa chọn hãng cho sản phẩm");
            }else
            {
                var phone = new Product();
                var selectedBrand = brandListBox.SelectedItem as Brand;

                phone.Name = productName.Text;
                phone.Price = path.price;
                phone.Quantity = int.Parse(quantity.Text);
                phone.Description = description.Text;
                phone.Brand_ID = selectedBrand.ID;
                phone.InternalMemory = int.Parse(internalMemoryTextBox.Text);
                phone.Ram = int.Parse(ramTextBox.Text);
                phone.ImageData = ImageData;

                db.Products.Add(phone);
                db.SaveChanges();
                MessageBox.Show("Đã lưu thành công sản phẩm");
                MainWindow.AddScreenClosed = true;
                this.Close();
            }
        }

        private void price_LostFocus(object sender, RoutedEventArgs e)
        {
            if(price.Text == string.Empty)
            {
                priceErrorMess.Content = "Giá sản phẩm không được để trống";
            }
        }

        private void productName_LostFocus(object sender, RoutedEventArgs e)
        {
            if(productName.Text == string.Empty)
            {
                productNameErrorMes.Content = "Tên sản phẩm không được bỏ trống";
            }
        }

        private void price_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }
    }
}
