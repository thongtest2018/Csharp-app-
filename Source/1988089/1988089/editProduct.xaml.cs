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
    /// Interaction logic for editProduct.xaml
    /// </summary>
    public partial class editProduct : Window
    {
        CultureInfo cultureInfo = CultureInfo.GetCultureInfo("vi-VN");
        public int phone_id;
        List<Brand> brands;
        MYSHOPEntities db = new MYSHOPEntities();
        Product phone;
        string filename = "";
        public editProduct()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            brands = db.Brands.ToList();
            brandListBox.ItemsSource = brands;
            phone = db.Products.Where(p => p.ID == phone_id).First();
            this.DataContext = phone;
        }

        private void productName_LostFocus(object sender, RoutedEventArgs e)
        {
            if (productName.Text == string.Empty)
            {
                productNameErrorMes.Content = "Tên sản phẩm không được bỏ trống";
            }
        }

        private void price_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }


        private void updateButton_Click(object sender, RoutedEventArgs e)
        {
            var selectedBrand = brandListBox.SelectedItem as Brand;
            phone.Name = productName.Text;
            phone.Price = int.Parse(price.Text, NumberStyles.AllowThousands, new CultureInfo("vi-VN"));
            phone.Quantity = int.Parse(quantity.Text);
            phone.Description = description.Text;
            phone.Brand_ID = selectedBrand.ID;
            phone.InternalMemory = int.Parse(internalMemoryTextBox.Text);
            phone.Ram = int.Parse(ramTextBox.Text);
            db.SaveChanges();
            MessageBox.Show("Đã lưu thành công thay đổi");
            MainWindow.EditScreenClosed = true;
            this.Close();
        }

        private void cancelButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Image_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            var dialog = new OpenFileDialog();
            if (dialog.ShowDialog() == true)
            {
                filename = dialog.FileName;

                var image = new BitmapImage(new Uri(filename, UriKind.Absolute));
                var encoder = new JpegBitmapEncoder();
                encoder.Frames.Add(BitmapFrame.Create(image));

                using (var stream = new MemoryStream())
                {
                    encoder.Save(stream);
                    phone.ImageData = stream.ToArray();
                }
                labelAddImage.Text = $"Đã chọn {filename}";
            }
        }
    }
}
