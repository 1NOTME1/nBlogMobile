using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Xamarin.Forms;

namespace AppMobilenBlog.Helpers
{
    public class StringToListConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null)
                return null;

            var separator = parameter?.ToString();
            var stringValue = value.ToString();

            // Split by separator and remove empty entries
            return stringValue.Split(new[] { separator }, StringSplitOptions.RemoveEmptyEntries).Select(item => item.Trim()).ToList();
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null)
                return null;

            var list = value as List<string>;
            var separator = parameter?.ToString();

            // Join list items with separator
            return string.Join(separator, list.Select(item => item.Trim()));
        }
    }

    public class StringToTagListConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null)
                return null;

            var stringValue = value.ToString();

            // Split by '#' and remove empty entries
            return stringValue.Split(new[] { '#' }, StringSplitOptions.RemoveEmptyEntries).Select(tag => "#" + tag.Trim()).ToList();
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null)
                return null;

            var list = value as List<string>;

            // Join list items with '#'
            return string.Join(" ", list.Select(tag => tag.Trim()));
        }
    }
}
