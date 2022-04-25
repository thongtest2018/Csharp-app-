using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _1988089
{
    class PurchaseList : INotifyPropertyChanged
    {
        public int id { get; set; }
        public string cusname { get; set; }
        public string custel { get; set; }
        public string cusadd { get; set; }

        public int total { get; set; }
        public DateTime datecreate { get; set; }
        public int status { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
