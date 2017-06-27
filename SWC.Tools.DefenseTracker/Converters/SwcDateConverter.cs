using System;
using System.Globalization;
using System.Windows.Data;
using SWC.Tools.Common.Util;

namespace SWC.Tools.DefenseTracker.Converters
{
    internal class SwcDateConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null)
            {
                return null;
            }

            var utcSeconds = (int) value;
            return $"{TimeHelper.FromSeconds(utcSeconds).ToLocalTime() : dd MMM @ HH:mm}";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}