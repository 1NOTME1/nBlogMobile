using System;
using System.Collections.Generic;
using System.Globalization;
using Xamarin.Forms;

namespace AppMobilenBlog.Helpers
{
    public class StringToListConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null || parameter == null)
                return null;

            var delimiter = parameter.ToString();
            var stringValue = value.ToString();
            return stringValue.Split(new[] { delimiter }, StringSplitOptions.RemoveEmptyEntries);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null || parameter == null)
                return null;

            var delimiter = parameter.ToString();
            var list = value as IEnumerable<string>;
            return string.Join(delimiter, list);
        }
    }
}
