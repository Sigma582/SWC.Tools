using System;
using System.Globalization;
using System.Windows.Data;

namespace SWC.Tools.DefenseTracker.Converters
{
    internal class StarsToResultConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var stars = (int) value;
            return stars == 0 ? "You Won" : "You Lost";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}