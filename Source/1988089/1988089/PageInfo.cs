using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _1988089
{
    class PageInfo: INotifyPropertyChanged
    {
        public string pageInfo { get; set; }
        public string currentPage { get; set; }
        public string brandName { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
