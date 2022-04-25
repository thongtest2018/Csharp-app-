using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace _1988089
{
    class NumberToMoneyConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            int number = (int)value;
            CultureInfo cultureInfo = CultureInfo.GetCultureInfo("vi-VN");
            string result = number.ToString("#,###", cultureInfo.NumberFormat);
            return result;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            
            string numberNeedConvert = (string)value;
            int num = int.Parse(numberNeedConvert, NumberStyles.AllowThousands, new CultureInfo("vi-VN"));
            return num;
        }
    }
}
