using System.Globalization;

namespace HomeChores.UI.Converters;

public class BoolToColorConverter : IValueConverter
{
    // Returns Black for current month, Gray otherwise.
    public object Convert(object? value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value is bool isCurrent && isCurrent)
            return Colors.Black;
        return Colors.Gray;
    }

    public object ConvertBack(object? value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}