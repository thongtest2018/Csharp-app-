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
    /// Interaction logic for ReportScreen.xaml
    /// </summary>
    public partial class ReportScreen : Window
    {
        public ReportScreen()
        {
            InitializeComponent();
        }

        private void inventory_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            InventoryScreen inventoryScreen = new InventoryScreen();
            inventoryScreen.ShowDialog();
        }

        private void report_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Revenue revenue = new Revenue();
            revenue.ShowDialog();
        }
    }
}
