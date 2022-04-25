using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace _1988089
{
    class StatusToImageConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string result = "";
            int status = (int)value;
            switch(status)
            {
                case 1:
                    result = "/Images/new.png";
                    break;
                case 2:
                    result = "/Images/paid.png";
                    break;
                case 3:
                    result = "/Images/cancel.png";
                    break;
                case 4:
                    result = "/Images/truck.png";
                    break;
            }
            return result;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
