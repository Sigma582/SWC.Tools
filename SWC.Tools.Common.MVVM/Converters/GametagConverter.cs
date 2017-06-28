using System;
using System.Globalization;
using System.Text.RegularExpressions;
using System.Web;
using System.Windows.Data;

namespace SWC.Tools.Common.MVVM.Converters
{
    public class GametagConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null) return null;

            var str = (string) value;
            str = HttpUtility.UrlDecode(str);

            var regex = new Regex("\\[[0-9A-Fa-f]{6}\\]");
            return regex.Replace(str, "");
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}