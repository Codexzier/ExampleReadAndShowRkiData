﻿using System;
using System.Globalization;
using System.Windows.Data;

namespace OverviewRkiData.Converters
{
    public class CheckUrlAddressConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is string str)
            {
                if (string.IsNullOrEmpty(str))
                {
                    return value;
                }

                if (!str.StartsWith("http://"))
                {
                    return $"http://{str}";
                }
            }

            return value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) => throw new NotImplementedException();
    }
}
