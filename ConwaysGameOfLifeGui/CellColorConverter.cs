using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;

namespace ConwaysGameOfLifeGui
{
    internal class CellColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            object convertedValue = null;

            if(value is bool trueOrFalse)
            {
                convertedValue = trueOrFalse ?  Brushes.White : Brushes.Red;
            }

            return convertedValue;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var boolValue = false;
            if (value is Brush brush)
            {
                boolValue = object.ReferenceEquals(brush, Brushes.White);
            }

            return boolValue;
        }
    }
}