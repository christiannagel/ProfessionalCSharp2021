using ConnectedAnimationSample.Models;

using Microsoft.UI;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Media;

namespace ConnectedAnimationSample.Converters;

public class ItemToBrushConverter : IValueConverter
{
    private Brush? solidRed;
    private Brush? solidGreen;
    private Brush? solidBlue;
    
    public Brush? RedBrush { get; set; }
    public Brush? GreenBrush { get; set; }
    public Brush? BlueBrush { get; set; }

    public object? Convert(object value, Type targetType, object parameter, string language)
    {
        return value switch
        {
            Item { Value: "red" } => RedBrush ?? (solidRed ??= new SolidColorBrush(Colors.Red)),
            Item { Value: "green" } => GreenBrush ?? (solidGreen ??= new SolidColorBrush(Colors.Green)),
            Item { Value: "blue" } => BlueBrush ?? (solidBlue ??= new SolidColorBrush(Colors.Blue)),
            _ => throw new InvalidOperationException("unexpected item")
        };
    }

    public object ConvertBack(object value, Type targetType, object parameter, string language)
    {
        throw new NotImplementedException();
    }
}
