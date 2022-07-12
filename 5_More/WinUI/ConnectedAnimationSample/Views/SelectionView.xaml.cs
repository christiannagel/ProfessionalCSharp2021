using CommunityToolkit.Mvvm.Messaging;

using ConnectedAnimationSample.ViewModels;

using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Media.Animation;

namespace ConnectedAnimationSample.Views;

public sealed partial class SelectionView : UserControl
{
    public SelectionView()
    {
        InitializeComponent();

        WeakReferenceMessenger.Default.Register<AddItemMessage>(this, (r, m) =>
        {
            if (m.Status == "Started")
            {
                var connectedAnimation = ConnectedAnimationService.GetForCurrentView();
                connectedAnimation?.PrepareToAnimate("item1", SelectedItem1);
                connectedAnimation?.PrepareToAnimate("item2", SelectedItem2);
            }
        });
    }

    public MainViewModel ViewModel
    {
        get => (MainViewModel)GetValue(ViewModelProperty);
        set => SetValue(ViewModelProperty, value);
    }

    public static readonly DependencyProperty ViewModelProperty =
        DependencyProperty.Register("ViewModel", typeof(MainViewModel), typeof(SelectionView), new PropertyMetadata(null));
}
