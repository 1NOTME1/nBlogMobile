using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Xamarin.Forms;

namespace AppMobilenBlog.Helpers
{
    public class StringToListConverter : IValueConverter
    {
        /// <summary>
        /// Converts a delimited string into a list of strings, removing any empty entries and trimming whitespace from each item.
        /// </summary>
        /// <param name="value">The input value to be converted. Expected to be a string.</param>
        /// <param name="targetType">The type of the binding target property. This parameter is not used.</param>
        /// <param name="parameter">An optional separator string to use for splitting the input string.</param>
        /// <param name="culture">The culture to use in the converter. This parameter is not used.</param>
        /// <returns>
        /// A list of strings that were separated by the specified separator in the input string. 
        /// Returns null if the input value is null.
        /// </returns>
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null)
                return null;

            var separator = parameter?.ToString();
            var stringValue = value.ToString();

            return stringValue.Split(new[] { separator }, StringSplitOptions.RemoveEmptyEntries).Select(item => item.Trim()).ToList();
        }

        /// <summary>
        /// Converts a list of strings back into a single delimited string, trimming whitespace from each item.
        /// </summary>
        /// <param name="value">The input value to be converted. Expected to be a list of strings.</param>
        /// <param name="targetType">The type of the binding target property. This parameter is not used.</param>
        /// <param name="parameter">An optional separator string to use for joining the list items.</param>
        /// <param name="culture">The culture to use in the converter. This parameter is not used.</param>
        /// <returns>
        /// A single string where each list item is separated by the specified separator. 
        /// Returns null if the input value is null.
        /// </returns>
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null)
                return null;

            var list = value as List<string>;
            var separator = parameter?.ToString();

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
