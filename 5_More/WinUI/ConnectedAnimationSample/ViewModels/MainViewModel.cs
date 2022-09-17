using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;

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
    private void AddItems()
    {      
        if (SelectedItem1 is not null && SelectedItem2 is not null)
        {
            WeakReferenceMessenger.Default.Send(new AddItemMessage("Started"));
            DisplayItem displayItem = new(SelectedItem1, SelectedItem2);
            _addedItems.Add(displayItem);

            WeakReferenceMessenger.Default.Send(new AddItemMessage("Completed", displayItem));

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

public record class AddItemMessage(string Status, DisplayItem? DisplayItem = default);
