using CommunityToolkit.Mvvm.ComponentModel;

namespace ConnectedAnimationSample.Models;

[ObservableObject]
public partial class Item
{
    public Item()
    {
        
    }
    public Item(string value)
    {
        _value = value;
    }
    
    [ObservableProperty]
    private string? _value;

    public static implicit operator Item(string value) => new Item(value);
}
