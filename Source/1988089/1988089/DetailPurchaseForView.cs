using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _1988089
{
    class DetailPurchaseForView : INotifyPropertyChanged
    {
        public byte[] image { get; set; }
        public string name { get; set; }
        public int price { get; set; }
        public int quantity { get; set; }
        public int total { get; set; }

        public int product_id { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
