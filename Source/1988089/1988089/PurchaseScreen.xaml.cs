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
    /// Interaction logic for PurchaseScreen.xaml
    /// </summary>
    public partial class PurchaseScreen : Window
    {
        public static bool addpurchasescreenclosed;
        public static bool editpurchasescreenclosed;

        string[] comboBoxValue = { "ID", "Tên khách hàng", "Trạng thái đơn hàng", "Ngày tháng tạo đơn hàng" };
        string[] statusValue = { "Mới tạo", "Đã thanh toán", "Đã hủy", "Đang giao" };

        MYSHOPEntities db = new MYSHOPEntities();

        int _totalItems;
        int _currentPage = 1;
        int _totalPages;
        int _rowPerPages = 5;
        int searchID = 0;
        string keyword = string.Empty;
        int status = 0;

        public PurchaseScreen()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            UpdatePurchaseList();
            search.ItemsSource = comboBoxValue;
            statusComboBox.ItemsSource = statusValue;
        }

        private void UpdatePurchaseList()
        {
            MYSHOPEntities db = new MYSHOPEntities();
            List<PurchaseList> listps = new List<PurchaseList>();
            var pageinfo = new PageInfo();
            List<Purchase> dbCount;
            List<Purchase> purcha;
            if (searchID != 0)
            {
                dbCount = db.Purchases.Where(s => s.ID == searchID).ToList();
                purcha = db.Purchases.Where(s => s.ID == searchID).OrderBy(s => s.ID).Skip((_currentPage - 1) * _rowPerPages).Take(_rowPerPages).ToList();
            }
            else if(keyword != string.Empty)
            {
                List<Customer> cusList = db.Customers.Where(s => s.Name.Contains(keyword)).ToList();
                List<int> cusIDList = new List<int>();
                foreach(Customer c in cusList)
                {
                    cusIDList.Add(c.ID);
                }
                dbCount = db.Purchases.Where(s => cusIDList.Contains((int)s.CustomerID)).ToList();
                purcha = db.Purchases.Where(s => cusIDList.Contains((int)s.CustomerID)).OrderBy(s => s.ID).Skip((_currentPage - 1) * _rowPerPages).Take(_rowPerPages).ToList();
            }
            else if( status != 0)
            {
                dbCount = db.Purchases.Where(s => s.Status == status).ToList();
                purcha = db.Purchases.Where(s => s.Status == status).OrderBy(s => s.ID).Skip((_currentPage - 1) * _rowPerPages).Take(_rowPerPages).ToList();
            }
            else if (fromDatepicker.SelectedDate == null && toDatePicker.SelectedDate != null)
            {
                dbCount = db.Purchases.Where(s => (DateTime)s.Created_At <= toDatePicker.SelectedDate).ToList();
                purcha = db.Purchases.Where(s => (DateTime)s.Created_At <= toDatePicker.SelectedDate).OrderBy(s => s.ID).Skip((_currentPage - 1) * _rowPerPages).Take(_rowPerPages).ToList();
            }
            else if (fromDatepicker.SelectedDate != null && toDatePicker.SelectedDate != null)
            {
                dbCount = db.Purchases.Where(s => (DateTime)s.Created_At >= fromDatepicker.SelectedDate && (DateTime)s.Created_At <= toDatePicker.SelectedDate).ToList();
                purcha = db.Purchases.Where(s => (DateTime)s.Created_At >= fromDatepicker.SelectedDate && (DateTime)s.Created_At <= toDatePicker.SelectedDate).OrderBy(s => s.ID).Skip((_currentPage - 1) * _rowPerPages).Take(_rowPerPages).ToList();
            }
            else
            {
                dbCount = db.Purchases.ToList();
                purcha = db.Purchases.OrderBy(s => s.ID).Skip((_currentPage - 1) * _rowPerPages).Take(_rowPerPages).ToList();
            }

            _totalItems = dbCount.Count;
            _totalPages = (_totalItems / _rowPerPages) + ((_totalItems % _rowPerPages == 0) ? 0 : 1);
            foreach (Purchase p in purcha)
            {
                PurchaseList pl = new PurchaseList();
                pl.id = p.ID;
                pl.total = (int)p.FinalTotal;
                pl.datecreate = (DateTime)p.Created_At;

                pl.status = p.Status;

                var cus = db.Customers.Where(s => s.ID == p.CustomerID).First();

                pl.cusname = cus.Name;
                pl.cusadd = cus.Address;
                pl.custel = cus.Tel;

                listps.Add(pl);
            }
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
            pageinfo.pageInfo = $"Đang hiển thị {itemCount} đơn hàng trong {_totalItems} đơn hàng";
            pageinfo.currentPage = $"Trang {_currentPage}";
            this.DataContext = pageinfo;
            
            PurchaseList.ItemsSource = listps.ToList();
            PurchaseList.Items.Refresh();
        }

        private void Image_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            var choosenPurchase = PurchaseList.SelectedItem as PurchaseList;
            if(choosenPurchase != null)
            {
                EditPurchaseScreen editPurchaseScreen = new EditPurchaseScreen(choosenPurchase.id);
                editpurchasescreenclosed = false;
                editPurchaseScreen.ShowDialog();
                
                if (editpurchasescreenclosed)
                {
                    UpdatePurchaseList();
                    
                }
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            AddPurchaseScreen addpurchase = new AddPurchaseScreen();
            addpurchasescreenclosed = false;
            addpurchase.ShowDialog();
            if(addpurchasescreenclosed)
            {
                UpdatePurchaseList();
            }
        }

        private void Next_Click(object sender, RoutedEventArgs e)
        {
            if(_currentPage != _totalPages && _totalPages != 0)
            {
                _currentPage = _currentPage + 1;
                UpdatePurchaseList();
            }
        }

        private void Previous_Click(object sender, RoutedEventArgs e)
        {
            if (_currentPage != 1)
            {
                _currentPage = _currentPage - 1;
                UpdatePurchaseList();
            }
        }

        private void First_Click(object sender, RoutedEventArgs e)
        {
            _currentPage = 1;
            UpdatePurchaseList();
        }

        private void Last_Click(object sender, RoutedEventArgs e)
        {
            if (_totalPages != 0)
            {
                _currentPage = _totalPages;
                UpdatePurchaseList();
            }
        }

        private void Image_MouseLeftButtonDown_1(object sender, MouseButtonEventArgs e)
        {
            if(search.SelectedIndex == 0)
            {
                if (searchIDTextBox.Text == "")
                {
                    MessageBox.Show("Vui lòng nhập ID đơn hàng");
                }
                else
                {
                    searchID = int.Parse(searchIDTextBox.Text);
                    UpdatePurchaseList();
                    searchID = 0;
                }
            }

            if(search.SelectedIndex == 1)
            {
                if(nameTextBox.Text == "")
                {
                    MessageBox.Show("Vui lòng nhập tên khách hàng cần tìm kiếm");
                }else
                {
                    keyword = nameTextBox.Text;
                    UpdatePurchaseList();
                }
            }

            if(search.SelectedIndex == 2)
            {
                if(statusComboBox.SelectedItem == null)
                {
                    MessageBox.Show("Vui lòng chọn trạng thái đơn hàng cần tìm kiếm");
                }else
                {
                    status = statusComboBox.SelectedIndex + 1;
                    UpdatePurchaseList();
                }
            }
            
            if(search.SelectedIndex == 3)
            {
                if(fromDatepicker.SelectedDate > toDatePicker.SelectedDate)
                {
                    MessageBox.Show("Ngày bắt đầu lớn hơn ngày kết thúc");
                }else if(toDatePicker.SelectedDate == null)
                {
                    MessageBox.Show("Chưa chọn ngày kết thúc");
                }else if(fromDatepicker.SelectedDate == null && toDatePicker.SelectedDate != null)
                {
                    UpdatePurchaseList();
                }
                else
                {
                    UpdatePurchaseList();
                }
            }
        }

        private void searchIDTextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }

        private void search_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(search.SelectedItem != null)
            {
                keyword = string.Empty;
                status = 0;
                fromDatepicker.SelectedDate = null;
                toDatePicker.SelectedDate = null;
                UpdatePurchaseList();
                if (search.SelectedIndex == 0)
                {
                    nameLabel.Visibility = Visibility.Hidden;
                    nameTextBox.Visibility = Visibility.Hidden;
                    idLabel.Visibility = Visibility.Visible;
                    searchIDTextBox.Visibility = Visibility.Visible;

                    fromDateLabel.Visibility = Visibility.Hidden;
                    fromDatepicker.Visibility = Visibility.Hidden;
                    toDateLabel.Visibility = Visibility.Hidden;
                    toDatePicker.Visibility = Visibility.Hidden;

                    statusLabel.Visibility = Visibility.Hidden;
                    statusComboBox.Visibility = Visibility.Hidden;
                }

                if(search.SelectedIndex == 1)
                {
                    nameLabel.Visibility = Visibility.Visible;
                    nameTextBox.Visibility = Visibility.Visible;

                    idLabel.Visibility = Visibility.Hidden;
                    searchIDTextBox.Visibility = Visibility.Hidden;

                    fromDateLabel.Visibility = Visibility.Hidden;
                    fromDatepicker.Visibility = Visibility.Hidden;
                    toDateLabel.Visibility = Visibility.Hidden;
                    toDatePicker.Visibility = Visibility.Hidden;
                    statusLabel.Visibility = Visibility.Hidden;
                    statusComboBox.Visibility = Visibility.Hidden;
                }

                if (search.SelectedIndex == 2)
                {
                    nameLabel.Visibility = Visibility.Hidden;
                    nameTextBox.Visibility = Visibility.Hidden;

                    idLabel.Visibility = Visibility.Hidden;
                    searchIDTextBox.Visibility = Visibility.Hidden;

                    fromDateLabel.Visibility = Visibility.Hidden;
                    fromDatepicker.Visibility = Visibility.Hidden;
                    toDateLabel.Visibility = Visibility.Hidden;
                    toDatePicker.Visibility = Visibility.Hidden;
                    statusLabel.Visibility = Visibility.Visible;
                    statusComboBox.Visibility = Visibility.Visible;
                }

                if (search.SelectedIndex == 3)
                {
                    nameLabel.Visibility = Visibility.Hidden;
                    nameTextBox.Visibility = Visibility.Hidden;

                    idLabel.Visibility = Visibility.Hidden;
                    searchIDTextBox.Visibility = Visibility.Hidden;

                    fromDateLabel.Visibility = Visibility.Visible;
                    fromDatepicker.Visibility = Visibility.Visible;
                    toDateLabel.Visibility = Visibility.Visible;
                    toDatePicker.Visibility = Visibility.Visible;
                    statusLabel.Visibility = Visibility.Hidden;
                    statusComboBox.Visibility = Visibility.Hidden;
                }
            }
        }

        private void Image_MouseLeftButtonDown_2(object sender, MouseButtonEventArgs e)
        {
            if(PurchaseList.SelectedItem != null)
            {
                var choosenPurchaseList = PurchaseList.SelectedItem as PurchaseList;
                MessageBoxResult messageBoxResult = MessageBox.Show($"Bạn có chắc chắn muốn xóa đơn hàng có ID là: {choosenPurchaseList.id} ?", "Xác nhận",MessageBoxButton.YesNo);
                if(messageBoxResult == MessageBoxResult.Yes)
                {
                    var deleteID = choosenPurchaseList.id;
                    Purchase deletepur = db.Purchases.Where(s => s.ID == deleteID).First();
                    List<PurchaseDetail> listpurchasedetail = new List<PurchaseDetail>();
                    listpurchasedetail = db.PurchaseDetails.Where(s => s.PurchaseID == deletepur.ID).ToList();
                    foreach(PurchaseDetail p in listpurchasedetail)
                    {
                        db.PurchaseDetails.Remove(p);
                    }
                    db.Purchases.Remove(deletepur);
                    db.SaveChanges();
                    if (_currentPage == _totalPages)
                    {
                        if (_totalItems % _rowPerPages == 1)
                        {
                            _currentPage--;
                        }
                    }
                    UpdatePurchaseList();
                }
            }
        }
    }
}
