using System;
using System.Collections.Generic;
using System.Globalization;
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
using LiveCharts;
using LiveCharts.Wpf;

namespace _1988089
{
    /// <summary>
    /// Interaction logic for Revenue.xaml
    /// </summary>
    public partial class Revenue : Window
    {
        int _totalItems;
        int _currentPage = 1;
        int _totalPages;
        int _rowPerPages = 5;

        public Revenue()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            UpdatePurchaseList();
            
            
        }

        private void UpdatePieChart(List<Purchase> purchases)
        {
            MYSHOPEntities db = new MYSHOPEntities();

            List<brandValue> listbrandValue = new List<brandValue>();

            foreach (Purchase p in purchases)
            {
                List<PurchaseDetail> listPurchaseDetail = db.PurchaseDetails.Where(s => s.PurchaseID == p.ID).ToList();
                foreach(PurchaseDetail pd in listPurchaseDetail)
                {
                    var choosenProduct = db.Products.Where(s => s.ID == pd.ProductID).First();
                    var choosenBrand = db.Brands.Where(s => s.ID == choosenProduct.Brand_ID).First();
                    int existIndex = -1;

                    for(int i = 0; i < listbrandValue.Count; i++)
                    {
                        if(listbrandValue[i].brand == choosenBrand.Name)
                        {
                            existIndex = i;
                        }
                    }

                    if(existIndex == -1)
                    {
                        var branV = new brandValue();
                        branV.brand = choosenBrand.Name;
                        branV.value = (double)pd.Total;
                        listbrandValue.Add(branV);
                    }else
                    {
                        listbrandValue[existIndex].value += (double)pd.Total;
                    }

                }
            }

            revenueChart.Series = new SeriesCollection();

            foreach(brandValue b in listbrandValue)
            {
                var Pis = new PieSeries();
                Pis.Values = new ChartValues<double> { b.value };
                Pis.Title = b.brand;
                revenueChart.Series.Add(Pis);
            }
        }

        private void UpdatePurchaseList()
        {
            int revenue = 0;
            MYSHOPEntities db = new MYSHOPEntities();
            List<PurchaseList> listps = new List<PurchaseList>();
            var pageinfo = new PageInfo();
            List<Purchase> dbCount;
            List<Purchase> purcha;

            if (fromDtpk.SelectedDate == null && toDtpk.SelectedDate != null)
            {
                dbCount = db.Purchases.Where(s => (DateTime)s.Created_At <= toDtpk.SelectedDate).ToList();
                purcha = db.Purchases.Where(s => (DateTime)s.Created_At <= toDtpk.SelectedDate).OrderBy(s => s.ID).Skip((_currentPage - 1) * _rowPerPages).Take(_rowPerPages).ToList();
            }
            else if (fromDtpk.SelectedDate != null && toDtpk.SelectedDate != null)
            {
                dbCount = db.Purchases.Where(s => (DateTime)s.Created_At >= fromDtpk.SelectedDate && (DateTime)s.Created_At <= toDtpk.SelectedDate).ToList();
                purcha = db.Purchases.Where(s => (DateTime)s.Created_At >= fromDtpk.SelectedDate && (DateTime)s.Created_At <= toDtpk.SelectedDate).OrderBy(s => s.ID).Skip((_currentPage - 1) * _rowPerPages).Take(_rowPerPages).ToList();
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

            foreach(Purchase p in dbCount)
            {
                revenue += (int)p.FinalTotal;
            }
            CultureInfo cultureInfo = CultureInfo.GetCultureInfo("vi-VN");
            revenueLabel.Content = revenue.ToString("#,###", cultureInfo.NumberFormat);

            PurchaseList.ItemsSource = listps.ToList();
            PurchaseList.Items.Refresh();

            UpdatePieChart(dbCount);
        }

        private void First_Click(object sender, RoutedEventArgs e)
        {
            _currentPage = 1;
            UpdatePurchaseList();
        }

        private void Previous_Click(object sender, RoutedEventArgs e)
        {
            if (_currentPage != 1)
            {
                _currentPage = _currentPage - 1;
                UpdatePurchaseList();
            }
        }

        private void Next_Click(object sender, RoutedEventArgs e)
        {
            if (_currentPage != _totalPages && _totalPages != 0)
            {
                _currentPage = _currentPage + 1;
                UpdatePurchaseList();
            }
        }

        private void Last_Click(object sender, RoutedEventArgs e)
        {
            if(_totalPages != 0)
            {
                _currentPage = _totalPages;
                UpdatePurchaseList();
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (fromDtpk.SelectedDate > toDtpk.SelectedDate)
            {
                MessageBox.Show("Ngày bắt đầu lớn hơn ngày kết thúc");
            }
            else if (toDtpk.SelectedDate == null)
            {
                MessageBox.Show("Chưa chọn ngày kết thúc");
            }
            else if (fromDtpk.SelectedDate == null && toDtpk.SelectedDate != null)
            {
                UpdatePurchaseList();
            }
            else
            {
                UpdatePurchaseList();
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            fromDtpk.SelectedDate = null;
            toDtpk.SelectedDate = null;
            UpdatePurchaseList();
        }
    }
}
