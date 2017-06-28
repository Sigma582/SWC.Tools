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
            return string.Format("{0: dd MMM @ HH:mm}", TimeHelper.FromSeconds(utcSeconds).ToLocalTime());
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}