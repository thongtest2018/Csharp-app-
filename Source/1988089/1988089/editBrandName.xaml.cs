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
    /// Interaction logic for editBrandName.xaml
    /// </summary>
    public partial class editBrandName : Window
    {
        int brand_id;
        Brand thisBrand;
        MYSHOPEntities db = new MYSHOPEntities();
        public editBrandName(int id)
        {
            InitializeComponent();
            brand_id = id;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            thisBrand = db.Brands.Where(b => b.ID == brand_id).First();
            brandNameTextBox.Text = thisBrand.Name;
        }

        private void Image_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            thisBrand.Name = brandNameTextBox.Text;
            db.SaveChanges();
            PhoneBrand.editBrandScreenClosed = true;
            this.Close();
        }

        private void Image_MouseLeftButtonDown_1(object sender, MouseButtonEventArgs e)
        {
            this.Close();
        }
    }
}
