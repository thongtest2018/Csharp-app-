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

namespace _1988089
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        MYSHOPEntities db = new MYSHOPEntities();
        static List<int> RowPerPageSelection = new List<int> { 10, 15, 20 };
        static public bool phoneBrandScreenClosed;
        public MainWindow()
        {
            InitializeComponent();
        }

        int _totalItems;
        int _currentPage = 1;
        int _totalPages;
        int _rowPerPages;
        int brand_id;
        static public bool AddScreenClosed;
        static public bool EditScreenClosed;

        string keyword = "";
        int price;
        int ram;
        int internalmemory;

        List<Product> products;
        List<Brand> brands;
       
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            //Gan du lieu cho combobox chon bao nhieu record cho 1 trang
            RowPerPageCombobox.ItemsSource = RowPerPageSelection;
            brand_id = 0;
            
            brands = db.Brands.ToList();
            products = db.Products.ToList();
            _totalItems = products.Count;
            _totalPages = (_totalItems / _rowPerPages) + ((_totalItems % _rowPerPages == 0) ? 0 : 1);
            BrandListView.ItemsSource = brands;
            updateProductionListView(brand_id);
        }

        void updateTotalPage()
        {
            products = db.Products.ToList();
            _totalItems = products.Count;
            _totalPages = (_totalItems / _rowPerPages) + ((_totalItems % _rowPerPages == 0) ? 0 : 1);
        }

        void updateToTalPageBrand()
        {
            _totalPages = (_totalItems / _rowPerPages) + ((_totalItems % _rowPerPages == 0) ? 0 : 1);
        }

        private void RowPerPage_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(RowPerPageCombobox.SelectedItem != null)
            {
                _rowPerPages = (int)RowPerPageCombobox.SelectedItem;
            }
            _currentPage = 1;
            
            updateProductionListView(brand_id);
            updateToTalPageBrand();

        }

        public void updateProductionListView(int brand_id = 0)
        {
            MYSHOPEntities db = new MYSHOPEntities();
            var pageinfo = new PageInfo();
            if(brand_id == 0)
            {
                if(keyword == "")
                {
                    if(price == 1)
                    {
                        ProductionListView.ItemsSource = db.Products.Where(p => p.Price < 5000000).OrderBy(p => p.ID).Skip((_currentPage - 1) * _rowPerPages).Take(_rowPerPages).ToList();
                        var phones = db.Products.Where(p => p.Price < 5000000).ToList();
                        _totalItems = phones.Count;
                    }else if(price == 2)
                    {
                        ProductionListView.ItemsSource = db.Products.Where(p => p.Price < 10000000 && p.Price >= 5000000).OrderBy(p => p.ID).Skip((_currentPage - 1) * _rowPerPages).Take(_rowPerPages).ToList();
                        var phones = db.Products.Where(p => p.Price < 10000000 && p.Price >= 5000000).ToList();
                        _totalItems = phones.Count;
                    }else if (price == 3)
                    {
                        ProductionListView.ItemsSource = db.Products.Where(p => p.Price < 15000000 && p.Price >= 10000000).OrderBy(p => p.ID).Skip((_currentPage - 1) * _rowPerPages).Take(_rowPerPages).ToList();
                        var phones = db.Products.Where(p => p.Price < 15000000 && p.Price >= 10000000).ToList();
                        _totalItems = phones.Count;
                    }
                    else if(price == 4)
                    {
                        ProductionListView.ItemsSource = db.Products.Where(p => p.Price >= 15000000).OrderBy(p => p.ID).Skip((_currentPage - 1) * _rowPerPages).Take(_rowPerPages).ToList();
                        var phones = db.Products.Where(p => p.Price >= 15000000).ToList();
                        _totalItems = phones.Count;
                    }
                    else
                    {
                        ProductionListView.ItemsSource = db.Products.OrderBy(s => s.ID).Skip((_currentPage - 1) * _rowPerPages).Take(_rowPerPages).ToList();
                    }
                }else
                {
                    if (price == 1)
                    {
                        ProductionListView.ItemsSource = db.Products.Where(p => p.Price < 5000000 && p.Name.Contains(keyword)).OrderBy(p => p.ID).Skip((_currentPage - 1) * _rowPerPages).Take(_rowPerPages).ToList();
                        var phones = db.Products.Where(p => p.Price < 5000000).ToList();
                        _totalItems = phones.Count;
                    }
                    else if (price == 2)
                    {
                        ProductionListView.ItemsSource = db.Products.Where(p => p.Price < 10000000 && p.Price >= 5000000 && p.Name.Contains(keyword)).OrderBy(p => p.ID).Skip((_currentPage - 1) * _rowPerPages).Take(_rowPerPages).ToList();
                        var phones = db.Products.Where(p => p.Price < 10000000 && p.Price >= 5000000).ToList();
                        _totalItems = phones.Count;
                    }
                    else if (price == 3)
                    {
                        ProductionListView.ItemsSource = db.Products.Where(p => p.Price < 15000000 && p.Price >= 10000000 && p.Name.Contains(keyword)).OrderBy(p => p.ID).Skip((_currentPage - 1) * _rowPerPages).Take(_rowPerPages).ToList();
                        var phones = db.Products.Where(p => p.Price < 15000000 && p.Price >= 10000000).ToList();
                        _totalItems = phones.Count;
                    }
                    else if (price == 4)
                    {
                        ProductionListView.ItemsSource = db.Products.Where(p => p.Price >= 15000000 && p.Name.Contains(keyword)).OrderBy(p => p.ID).Skip((_currentPage - 1) * _rowPerPages).Take(_rowPerPages).ToList();
                        var phones = db.Products.Where(p => p.Price >= 15000000).ToList();
                        _totalItems = phones.Count;
                    }
                    else
                    {
                        ProductionListView.ItemsSource = db.Products.Where(p => p.Name.Contains(keyword)).OrderBy(p => p.ID).Skip((_currentPage - 1) * _rowPerPages).Take(_rowPerPages).ToList();
                        var phones = db.Products.Where(p => p.Name.Contains(keyword)).ToList();
                        _totalItems = phones.Count;
                    }
                }
            }else
            {
                if(keyword == "")
                {
                    ProductionListView.ItemsSource = db.Brands.Find(brand_id).Products.Skip((_currentPage - 1) * _rowPerPages).Take(_rowPerPages).ToList();
                }
                else
                {
                    ProductionListView.ItemsSource = db.Brands.Find(brand_id).Products.Where(p => p.Name.Contains(keyword)).Skip((_currentPage - 1) * _rowPerPages).Take(_rowPerPages).ToList();
                    var phones = db.Brands.Find(brand_id).Products.Where(p => p.Name.Contains(keyword)).ToList();
                    _totalItems = phones.Count;
                }
                var thisBrandName = db.Brands.Where(b => b.ID == brand_id).First();
                pageinfo.brandName = thisBrandName.Name;
            }
            updateToTalPageBrand();
            int itemCount;
            if (_currentPage == _totalPages)
            {
                if(_totalItems % _rowPerPages == 0)
                {
                    itemCount = _rowPerPages;
                }else
                {
                    itemCount = _totalItems % _rowPerPages;
                }
            }else if(_currentPage == 1 && _totalItems < _rowPerPages)
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
            ProductionListView.Items.Refresh();
        }

        private void BrandListView_PreviewMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            Brand selectedBrand = (sender as ListView).SelectedItem as Brand;
            if (selectedBrand != null)
            {
                keyword = "";
                brand_id = selectedBrand.ID;
                _currentPage = 1;
                var phones = db.Brands.Find(brand_id).Products.ToList();
                _totalItems = phones.Count;
                updateToTalPageBrand();
                updateProductionListView(brand_id);
            }
        }

        private void First_Click(object sender, RoutedEventArgs e)
        {
            if(_currentPage != 1 && _totalPages != 0)
            {
                _currentPage = 1;
                updateProductionListView(brand_id);
            }
        }

        private void Previous_Click(object sender, RoutedEventArgs e)
        {
            if(_currentPage != 1 && _totalPages != 0)
            {
                _currentPage--;
                updateProductionListView(brand_id);
            }
        }

        private void Next_Click(object sender, RoutedEventArgs e)
        {
            if(_currentPage != _totalPages && _totalPages != 0)
            {
                _currentPage++;
                updateProductionListView(brand_id);
            }
        }

        private void Last_Click(object sender, RoutedEventArgs e)
        {
            if (_currentPage != _totalPages && _totalPages != 0)
            {
                _currentPage = _totalPages;
                updateProductionListView(brand_id);
            }
        }

        private void Image_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            addProduct addscreen = new addProduct();
            addscreen.ShowDialog();
            if(AddScreenClosed)
            {
                updateTotalPage();
                updateProductionListView();
                AddScreenClosed = false;
               
            }
        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            editProduct editscreen = new editProduct();
            var choosenPhone = ProductionListView.SelectedItem as Product;
            editscreen.phone_id = choosenPhone.ID;
            editscreen.ShowDialog();
            if(EditScreenClosed)
            {
                updateProductionListView();
                EditScreenClosed = false;
            }
            
        }

        private void Image_MouseLeftButtonDown_1(object sender, MouseButtonEventArgs e)
        {
            var choosenPhone = ProductionListView.SelectedItem as Product;
            if (choosenPhone != null)
            {
                if (_currentPage == _totalPages)
                {
                    if (_totalItems % _rowPerPages == 1)
                    {
                        _currentPage--;
                    }
                }
                var deleteID = choosenPhone.ID;
                Product deletedProduct = db.Products.Where(s => s.ID == deleteID).First();
                db.Products.Remove(deletedProduct);
                db.SaveChanges();
                updateTotalPage();
                updateProductionListView(brand_id);
            }else
            {
                MessageBox.Show("Vui lòng chọn sản phẩm để xóa");
            }
            
        }

        private void Label_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            keyword = "";
            searchBox.Text = "";
            updateTotalPage();
            brand_id = 0;
            _currentPage = 1;
            updateProductionListView(brand_id);
            price = 0;
            brands = db.Brands.ToList();
            products = db.Products.ToList();
            _totalItems = products.Count;
            _totalPages = (_totalItems / _rowPerPages) + ((_totalItems % _rowPerPages == 0) ? 0 : 1);
            BrandListView.ItemsSource = brands;
            updateProductionListView(brand_id);
        }

        private void search_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            keyword = searchBox.Text;
            _currentPage = 1;
            updateProductionListView(brand_id);
        }

        private void RadioButton_Checked(object sender, RoutedEventArgs e)
        {
            internalmemory = 1;
        }

        private void RadioButton_Checked_1(object sender, RoutedEventArgs e)
        {
            internalmemory = 2;
        }

        private void RadioButton_Checked_2(object sender, RoutedEventArgs e)
        {
            internalmemory = 3;
        }

        private void RadioButton_Checked_3(object sender, RoutedEventArgs e)
        {
            internalmemory = 4;
        }
        private void memory5_Checked(object sender, RoutedEventArgs e)
        {
            internalmemory = 5;
        }

        private void RadioButton_Checked_4(object sender, RoutedEventArgs e)
        {
            price = 1;
        }

        private void RadioButton_Checked_5(object sender, RoutedEventArgs e)
        {
            price = 2;
        }

        private void RadioButton_Checked_6(object sender, RoutedEventArgs e)
        {
            price = 3;
        }

        private void RadioButton_Checked_7(object sender, RoutedEventArgs e)
        {
            price = 4;
        }

        private void RadioButton_Checked_8(object sender, RoutedEventArgs e)
        {
            ram = 1;
        }

        private void RadioButton_Checked_9(object sender, RoutedEventArgs e)
        {
            ram = 2;
        }

        private void RadioButton_Checked_10(object sender, RoutedEventArgs e)
        {
            ram = 3;
        }

        private void filterButton_Click(object sender, RoutedEventArgs e)
        {
            _currentPage = 1;
            updateProductionListView(brand_id);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            ram1.IsChecked = false;
            ram2.IsChecked = false;
            ram3.IsChecked = false;
            memory1.IsChecked = false;
            memory2.IsChecked = false;
            memory3.IsChecked = false;
            memory4.IsChecked = false;
            memory5.IsChecked = false;
            price1.IsChecked = false;
            price2.IsChecked = false;
            price3.IsChecked = false;
            price4.IsChecked = false;
            price = 0;
            ram = 0;
            internalmemory = 0;
        }

        private void Image_MouseLeftButtonDown_2(object sender, MouseButtonEventArgs e)
        {
            PhoneBrand phoneBrandScreen = new PhoneBrand();
            phoneBrandScreenClosed = false;
            phoneBrandScreen.ShowDialog();
            if(phoneBrandScreenClosed)
            {
                MYSHOPEntities db = new MYSHOPEntities();
                BrandListView.ItemsSource = db.Brands.ToList();
            }
        }
    }
}
