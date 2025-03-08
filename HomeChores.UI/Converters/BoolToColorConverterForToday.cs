using System.Globalization;

namespace HomeChores.UI.Converters;

public class BoolToColorConverterForToday : IValueConverter
{
    public object Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        if (value is bool isCurrent && isCurrent)
            return Colors.Orange;
        return Colors.Gray;
    }

    public object ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}