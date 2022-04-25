using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _1988089
{
    class ImagePath : INotifyPropertyChanged
    {
        public string path { get; set; }
        public int price { get; set; }
        public event PropertyChangedEventHandler PropertyChanged;
    }
}
