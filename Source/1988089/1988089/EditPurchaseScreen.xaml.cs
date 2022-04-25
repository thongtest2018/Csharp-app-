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
    /// Interaction logic for EditPurchaseScreen.xaml
    /// </summary>
    public partial class EditPurchaseScreen : Window
    {
        int id;
        MYSHOPEntities db = new MYSHOPEntities();
        List<DetailPurchaseForView> listDetail = new List<DetailPurchaseForView>();
        string[] purstatus = { "Mới tạo", "Đã thanh toán", "Đã hủy", "Đang giao" };
        public EditPurchaseScreen(int id)
        {
            InitializeComponent();
            this.id = id;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            brandComboBox.ItemsSource = db.Brands.ToList();
            purchaseStatus.ItemsSource = purstatus;
            purchaseStatus.SelectedIndex = db.Purchases.Find(this.id).Status - 1;
            List<PurchaseDetail> purchaseDetails = new List<PurchaseDetail>();
            purchaseDetails = db.PurchaseDetails.Where(s => s.PurchaseID == this.id).ToList();
            foreach(PurchaseDetail p in purchaseDetails)
            {
                DetailPurchaseForView d = new DetailPurchaseForView();
                d.image = db.Products.Find(p.ProductID).ImageData;
                d.name = db.Products.Find(p.ProductID).Name;
                d.price = (int)db.Products.Find(p.ProductID).Price;
                d.quantity = (int)p.Quantity;
                d.total = (int)(d.price * p.Quantity);
                d.product_id = (int)p.ProductID;
                listDetail.Add(d);
            }

            oldCusRadioButton.IsChecked = true;
            var customerID = db.Purchases.Find(this.id).CustomerID;
            oldCustomerComboBox.SelectedItem = db.Customers.Find(customerID);

            updateListDetail();

        }
        private void updateListDetail()
        {
            PurchaseDetail.ItemsSource = listDetail;
            PurchaseDetail.Items.Refresh();
            var tong = 0;
            foreach (DetailPurchaseForView dt in listDetail)
            {
                tong = tong + (dt.price * dt.quantity);
            }
            CultureInfo cultureInfo = CultureInfo.GetCultureInfo("vi-VN");
            totalLabel.Content = tong.ToString("#,###", cultureInfo.NumberFormat);
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            //Click +
            if (PurchaseDetail.SelectedItem != null)
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
            if (PurchaseDetail.SelectedItem != null)
            {
                var selecteddetail = PurchaseDetail.SelectedItem as DetailPurchaseForView;
                if (selecteddetail.quantity == 1)
                {
                    listDetail.Remove(selecteddetail);

                }
                else
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

        private void brandComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (brandComboBox.SelectedItem != null)
            {
                var selectedbrand = brandComboBox.SelectedItem as Brand;
                productComboBox.ItemsSource = db.Brands.Find(selectedbrand.ID).Products.ToList();
            }
        }

        private void quantityTextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            if (quantityTextBox.Text == string.Empty)
            {
                MessageBox.Show("Số lượng sản phẩm không được để trống");
            }
        }

        private void quantityTextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (quantityTextBox.Text == string.Empty)
            {
                MessageBox.Show("Giá sản phẩm không được để trống");
            }
            else
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

        private void oldCusRadioButton_Checked(object sender, RoutedEventArgs e)
        {
            oldCustomerComboBox.ItemsSource = db.Customers.ToList();
        }


        private void cancelButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void updatePurchase_Click(object sender, RoutedEventArgs e)
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

                var updatePurchase = db.Purchases.Find(this.id);
                updatePurchase.CustomerID = addedCus.ID;
                updatePurchase.FinalTotal = int.Parse(totalLabel.Content.ToString(), NumberStyles.AllowThousands, new CultureInfo("vi-VN"));
                updatePurchase.Status = purchaseStatus.SelectedIndex + 1;
                List<PurchaseDetail> purchaseDetailsInDb = db.PurchaseDetails.Where(s => s.PurchaseID == this.id).ToList();
                foreach(PurchaseDetail p in purchaseDetailsInDb)
                {
                    bool notExsit = true;
                    for(int i = 0; i < listDetail.Count; i++)
                    {
                        if(p.ProductID == listDetail[i].product_id)
                        {
                            p.Quantity = listDetail[i].quantity;
                            notExsit = false;
                        }
                    }

                    if(notExsit)
                    {
                        db.PurchaseDetails.Remove(p);
                    }
                }

                foreach (DetailPurchaseForView d in listDetail)
                {
                    bool notExsit = true;
                    for(int i = 0; i < purchaseDetailsInDb.Count; i++)
                    {
                        if(d.product_id == purchaseDetailsInDb[i].ProductID)
                        {
                            notExsit = false;
                        }
                    }
                    if(notExsit)
                    {
                        PurchaseDetail newp = new PurchaseDetail();
                        newp.PurchaseID = this.id;
                        newp.ProductID = d.product_id;
                        newp.UnitPrice = d.price;
                        newp.Quantity = d.quantity;
                        newp.Total = d.total;

                        db.PurchaseDetails.Add(newp);
                    }
                }
                db.SaveChanges();
                MessageBox.Show("Đã lưu thành công đơn hàng");
                PurchaseScreen.editpurchasescreenclosed = true;
                this.Close();
            }

            if(oldCus)
            {
                var updatePurchase = db.Purchases.Find(this.id);
                var seletedCustomer = oldCustomerComboBox.SelectedItem as Customer;
                updatePurchase.CustomerID = seletedCustomer.ID;
                updatePurchase.FinalTotal = int.Parse(totalLabel.Content.ToString(), NumberStyles.AllowThousands, new CultureInfo("vi-VN"));
                updatePurchase.Status = purchaseStatus.SelectedIndex + 1;
                List<PurchaseDetail> purchaseDetailsInDb = db.PurchaseDetails.Where(s => s.PurchaseID == this.id).ToList();
                foreach (PurchaseDetail p in purchaseDetailsInDb)
                {
                    bool notExsit = true;
                    for (int i = 0; i < listDetail.Count; i++)
                    {
                        if (p.ProductID == listDetail[i].product_id)
                        {
                            p.Quantity = listDetail[i].quantity;
                            notExsit = false;
                        }
                    }

                    if (notExsit)
                    {
                        db.PurchaseDetails.Remove(p);
                    }
                }

                foreach (DetailPurchaseForView d in listDetail)
                {
                    bool notExsit = true;
                    for (int i = 0; i < purchaseDetailsInDb.Count; i++)
                    {
                        if (d.product_id == purchaseDetailsInDb[i].ProductID)
                        {
                            notExsit = false;
                        }
                    }
                    if (notExsit)
                    {
                        PurchaseDetail newp = new PurchaseDetail();
                        newp.PurchaseID = this.id;
                        newp.ProductID = d.product_id;
                        newp.UnitPrice = d.price;
                        newp.Quantity = d.quantity;
                        newp.Total = d.total;

                        db.PurchaseDetails.Add(newp);
                    }
                }
                db.SaveChanges();
                MessageBox.Show("Đã lưu thành công đơn hàng");
                PurchaseScreen.editpurchasescreenclosed = true;
                this.Close();
            }
        }
    }
}
