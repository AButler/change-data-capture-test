using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace UserEditor.Converters;

public class PixelsToGridLengthConverter : IValueConverter
{
    public object Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        var length = (double?)value;

        return new GridLength(length ?? 0);
    }

    public object ConvertBack(
        object? value,
        Type targetType,
        object? parameter,
        CultureInfo culture
    )
    {
        throw new NotImplementedException();
    }
}
