using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace order_processing
{
    public class StatusToVisibilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is string status)
            {
                string action = parameter as string;
                if (action == "edit" && (status == "ожидает оплаты" || status == "оплачен"))
                    return Visibility.Visible;
                if (action == "cancel" && (status == "ожидает оплаты" || status == "оплачен"))
                    return Visibility.Visible;
            }
            return Visibility.Collapsed;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
