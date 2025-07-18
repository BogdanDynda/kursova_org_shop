using System;
using System.Globalization;
using System.Linq;
using System.Windows.Data;

namespace kurs_frontapp
{
    public class OrderTotalConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is System.Collections.ICollection items)
            {
                decimal total = 0;
                foreach (dynamic item in items)
                {
                    total += item.quantity * item.price;
                }
                return $"{total:N2}₴";
            }
            return "0₴";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}