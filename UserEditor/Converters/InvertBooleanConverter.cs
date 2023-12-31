﻿using System;
using System.Globalization;
using System.Windows.Data;

namespace UserEditor.Converters;

public class InvertBooleanConverter : IValueConverter
{
    public object Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        return !(value is bool b && b);
    }

    public object ConvertBack(
        object? value,
        Type targetType,
        object? parameter,
        CultureInfo culture
    )
    {
        return !(value is bool b && b);
    }
}
