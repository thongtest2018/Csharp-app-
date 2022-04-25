using System;
using System.Collections.Generic;
using System.Globalization;
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
    /// Interaction logic for AddPurchaseScreen.xaml
    /// </summary>
    public partial class AddPurchaseScreen : Window
    {
        MYSHOPEntities db = new MYSHOPEntities();
        List<DetailPurchaseForView> listDetail = new List<DetailPurchaseForView>();

        public AddPurchaseScreen()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            brandComboBox.ItemsSource = db.Brands.ToList();
        }

        private void brandComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(brandComboBox.SelectedItem != null)
            {
                var selectedbrand = brandComboBox.SelectedItem as Brand;
                productComboBox.ItemsSource = db.Brands.Find(selectedbrand.ID).Products.ToList();
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if(quantityTextBox.Text == string.Empty)
            {
                MessageBox.Show("Giá sản phẩm không được để trống");
            }else
            {
                if (brandComboBox.SelectedItem != null && productComboBox.SelectedItem != null)
                {
                    var chosenBrand = brandComboBox.SelectedItem as Brand;
                    var chosenPhone = productComboBox.SelectedItem as Product;

                    bool exist = false;
                    foreach (DetailPurchaseForView dt in listDetail)
                    {
                        if (dt.name == chosenPhone.Name)
                        {
                            exist = true;
                            dt.quantity = dt.quantity + int.Parse(quantityTextBox.Text);
                            dt.total = dt.quantity * dt.price;
                        }
                    }

                    if (!exist)
                    {
                        var detail = new DetailPurchaseForView();
                        detail.image = chosenPhone.ImageData;
                        detail.name = chosenPhone.Name;
                        detail.price = (int)chosenPhone.Price;
                        detail.quantity = int.Parse(quantityTextBox.Text);
                        detail.product_id = chosenPhone.ID;
                        detail.total = detail.price * detail.quantity;
                        listDetail.Add(detail);
                    }

                    updateListDetail();
                }
                else
                {
                    MessageBox.Show("Chưa chọn sản phẩm để thêm");
                }
            }
        }

        private void updateListDetail()
        {
            PurchaseDetail.ItemsSource = listDetail;
            PurchaseDetail.Items.Refresh();
            var tong = 0;
            foreach(DetailPurchaseForView dt in listDetail)
            {
                tong = tong + (dt.price * dt.quantity);
            }
            CultureInfo cultureInfo = CultureInfo.GetCultureInfo("vi-VN");
            totalLabel.Content = tong.ToString("#,###", cultureInfo.NumberFormat);
        }

        private void quantityTextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            //Click +
            if(PurchaseDetail.SelectedItem != null)
            {
                var selecteddetail = PurchaseDetail.SelectedItem as DetailPurchaseForView;
                foreach (DetailPurchaseForView dt in listDetail)
                {
                    if (dt.name == selecteddetail.name)
                    {
                        dt.quantity = dt.quantity + 1;
                        dt.total = dt.quantity * dt.price;
                    }
                }
                updateListDetail();
            }
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            //Click -
            if (PurchaseDetail.SelectedItem != null)
            {
                var selecteddetail = PurchaseDetail.SelectedItem as DetailPurchaseForView;
                if(selecteddetail.quantity == 1)
                {
                    listDetail.Remove(selecteddetail);

                }else
                {
                    foreach (DetailPurchaseForView dt in listDetail)
                    {
                        if (dt.name == selecteddetail.name)
                        {
                            dt.quantity = dt.quantity - 1;
                            dt.total = dt.quantity * dt.price;
                        }
                    }
                }
                
                updateListDetail();
            }
        }

        private void quantityTextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            if (quantityTextBox.Text == string.Empty)
            {
                MessageBox.Show("Số lượng sản phẩm không được để trống");
            }
        }

        private void addPurchase_Click(object sender, RoutedEventArgs e)
        {
            bool newCus = (bool)newCusRadioButton.IsChecked;
            bool oldCus = (bool)oldCusRadioButton.IsChecked;
            if(newCus)
            {
                var Customer = new Customer();
                Customer.Name = customerNameTextBox.Text;
                Customer.Tel = PhoneTextBox.Text;
                Customer.Address = AddressTextBox.Text;
                db.Customers.Add(Customer);
                db.SaveChanges();

                var addedCus = db.Customers.OrderByDescending(c => c.ID).First();

                Purchase purchase = new Purchase();
                purchase.CustomerID = addedCus.ID;
                purchase.FinalTotal = int.Parse(totalLabel.Content.ToString(), NumberStyles.AllowThousands, new CultureInfo("vi-VN"));
                purchase.Created_At = DateTime.Now;
                purchase.Status = 1;

                db.Purchases.Add(purchase);
                db.SaveChanges();

                var addedPur = db.Purchases.OrderByDescending(p => p.ID).First();
                foreach (DetailPurchaseForView dt in listDetail)
                {
                    var purchaseDetail = new PurchaseDetail();
                    purchaseDetail.PurchaseID = addedPur.ID;
                    purchaseDetail.ProductID = dt.product_id;
                    purchaseDetail.UnitPrice = dt.price;
                    purchaseDetail.Quantity = dt.quantity;
                    purchaseDetail.Total = dt.total;

                    db.PurchaseDetails.Add(purchaseDetail);
                    db.SaveChanges();
                }
                MessageBox.Show("Đã lưu thành công đơn hàng");
                PurchaseScreen.addpurchasescreenclosed = true;
                this.Close();
            }

            if(oldCus)
            {
                if(oldCustomerComboBox.SelectedItem != null)
                {
                    var selectedCus = oldCustomerComboBox.SelectedItem as Customer;
                    Purchase purchase = new Purchase();
                    purchase.CustomerID = selectedCus.ID;
                    purchase.FinalTotal = int.Parse(totalLabel.Content.ToString(), NumberStyles.AllowThousands, new CultureInfo("vi-VN"));
                    purchase.Created_At = DateTime.Now;
                    purchase.Status = 1;

                    db.Purchases.Add(purchase);
                    db.SaveChanges();

                    var addedPur = db.Purchases.OrderByDescending(p => p.ID).First();
                    foreach (DetailPurchaseForView dt in listDetail)
                    {
                        var purchaseDetail = new PurchaseDetail();
                        purchaseDetail.PurchaseID = addedPur.ID;
                        purchaseDetail.ProductID = dt.product_id;
                        purchaseDetail.UnitPrice = dt.price;
                        purchaseDetail.Quantity = dt.quantity;
                        purchaseDetail.Total = dt.total;

                        db.PurchaseDetails.Add(purchaseDetail);
                        db.SaveChanges();
                    }
                    MessageBox.Show("Đã lưu thành công đơn hàng");
                    PurchaseScreen.addpurchasescreenclosed = true;
                    this.Close();
                }
            }
        }

        private void cancelButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void oldCusRadioButton_Checked(object sender, RoutedEventArgs e)
        {
            oldCustomerComboBox.ItemsSource = db.Customers.ToList();
        }
    }
}
