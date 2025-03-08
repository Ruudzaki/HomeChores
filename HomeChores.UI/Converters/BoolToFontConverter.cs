using System.Globalization;

namespace HomeChores.UI.Converters;

public class BoolToFontConverter : IValueConverter
{
    public object Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        if (value is int count && count > 0)
            return FontAttributes.Bold;
        return FontAttributes.None;
    }

    public object ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}