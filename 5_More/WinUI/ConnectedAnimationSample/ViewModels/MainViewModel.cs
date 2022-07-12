using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

using ConnectedAnimationSample.Models;

using System.Collections.ObjectModel;

namespace ConnectedAnimationSample.ViewModels;

[ObservableObject]
public partial class MainViewModel
{
    public MainViewModel()
    {
        AvailableList = new List<Item>(new Item[] { "red", "green", "blue" });
    }
    public IList<Item> AvailableList { get; }

    [ObservableProperty]
    private Item? _selectedItem1;
    
    [ObservableProperty]
    private Item? _selectedItem2;

    [ICommand]
    private void CopyItems()
    {
        if (_selectedItem1 is not null && _selectedItem2 is not null)
        {
            _addedItems.Add(new(_selectedItem1, _selectedItem2));
            SelectedItem1 = null;
            SelectedItem2 = null;
        }
    }

    [ICommand]
    private void ClearList()
    {
        AddedItems.Clear();
    }

    [ObservableProperty]
    private ObservableCollection<DisplayItem> _addedItems = new ObservableCollection<DisplayItem>();
}
