using System;
using System.Collections.Generic;
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
    /// Interaction logic for InventoryScreen.xaml
    /// </summary>
    public partial class InventoryScreen : Window
    {
        int _totalItems;
        int _currentPage = 1;
        int _totalPages;
        int _rowPerPages = 10;
        int inventoryQuantity = 0;
        string searchKeyWord = "";

        public InventoryScreen()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            UpdateInventoryList();
        }

        private void First_Click(object sender, RoutedEventArgs e)
        {
            _currentPage = 1;
            UpdateInventoryList();
        }

        private void Previous_Click(object sender, RoutedEventArgs e)
        {
            if (_currentPage != 1 && _totalItems != 0)
            {
                _currentPage = _currentPage - 1;
                UpdateInventoryList();
            }
        }

        private void Next_Click(object sender, RoutedEventArgs e)
        {
            if (_currentPage != _totalPages && _totalItems != 0)
            {
                _currentPage = _currentPage + 1;
                UpdateInventoryList();
            }
        }

        private void Last_Click(object sender, RoutedEventArgs e)
        {
            if (_totalPages != 0)
            {
                _currentPage = _totalPages;
                UpdateInventoryList();
            }
            
        }

        private void UpdateInventoryList()
        {
            MYSHOPEntities db = new MYSHOPEntities();
            List<Product> listpr = new List<Product>();
            var pageinfo = new PageInfo();

            List<Product> dbCount = new List<Product>();
            
            if(inventoryQuantity == 0)
            {
                if(searchKeyWord == "")
                {
                    dbCount = db.Products.ToList();
                    listpr = db.Products.OrderBy(s => s.Quantity).Skip((_currentPage - 1) * _rowPerPages).Take(_rowPerPages).ToList();
                }else
                {
                    dbCount = db.Products.Where(s => s.Name.Contains(searchKeyWord)).ToList();
                    listpr = db.Products.Where(s => s.Name.Contains(searchKeyWord)).OrderBy(s => s.Quantity).Skip((_currentPage - 1) * _rowPerPages).Take(_rowPerPages).ToList();
                }
            }else
            {
                dbCount = db.Products.Where(s => s.Quantity <= inventoryQuantity).ToList();
                listpr = db.Products.Where(s => s.Quantity <= inventoryQuantity).OrderBy(s => s.Quantity).Skip((_currentPage - 1) * _rowPerPages).Take(_rowPerPages).ToList();
            }
            
            _totalItems = dbCount.Count;
            _totalPages = (_totalItems / _rowPerPages) + ((_totalItems % _rowPerPages == 0) ? 0 : 1);

            int itemCount = 0;
            if (_currentPage == _totalPages)
            {
                if (_totalItems % _rowPerPages == 0)
                {
                    itemCount = _rowPerPages;
                }
                else
                {
                    itemCount = _totalItems % _rowPerPages;
                }
            }
            else if (_currentPage == 1 && _totalItems < _rowPerPages)
            {
                itemCount = _totalItems;
            }
            else
            {
                itemCount = _rowPerPages;
            }

            pageinfo.pageInfo = $"Đang hiển thị {itemCount} sản phẩm trong {_totalItems} sản phẩm";
            pageinfo.currentPage = $"Trang {_currentPage}";
            this.DataContext = pageinfo;
            
            inventoryList.ItemsSource = listpr;
        }

        private void TextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            inventoryQuantity = int.Parse(quantityInventory.Text);
            _currentPage = 1;
            UpdateInventoryList();
        }

        private void Image_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            inventoryQuantity = 0;
            _currentPage = 1;
            searchKeyWord = keyword.Text;
            UpdateInventoryList();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            searchKeyWord = "";
            inventoryQuantity = 0;
            keyword.Text = string.Empty;
            UpdateInventoryList();
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            searchKeyWord = "";
            inventoryQuantity = 0;
            keyword.Text = string.Empty;
            UpdateInventoryList();
        }
    }
}
