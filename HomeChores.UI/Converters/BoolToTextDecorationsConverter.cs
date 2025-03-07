using System.Globalization;

namespace HomeChores.UI.Converters;

public class BoolToTextDecorationsConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value is bool and true)
            return TextDecorations.Strikethrough;
        return TextDecorations.None;
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}