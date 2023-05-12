using System;
using System.Globalization;
using System.Windows.Data;

namespace MatrixMultiplicationProject.Converters;

[ValueConversion(typeof(string), typeof(long))]
public class LongToStringConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        return value.ToString() ?? string.Empty;
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        long.TryParse(value.ToString() ?? string.Empty, out long result);

        return result;
    }
}