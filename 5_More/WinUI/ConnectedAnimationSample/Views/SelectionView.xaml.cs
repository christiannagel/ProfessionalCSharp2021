using CommunityToolkit.Mvvm.Messaging;

using ConnectedAnimationSample.ViewModels;

using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Media.Animation;
using Microsoft.UI.Xaml.Shapes;

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
                var animationService = ConnectedAnimationService.GetForCurrentView();

                var container1 = SelectedItem1.ItemContainerGenerator.ContainerFromItem(SelectedItem1.SelectedItem);
                var container2 = SelectedItem2.ItemContainerGenerator.ContainerFromItem(SelectedItem2.SelectedItem);
                //if (container1 is FrameworkElement fr1)
                //{
                //    var grid1 = fr1.FindName("grid1");
                //    animationService?.PrepareToAnimate("item1", fr1);
                //}
                var x = container1 as UIElement;
                animationService?.PrepareToAnimate("item1", x);


                //var ui1 = FindFirstItemOfType<Ellipse>(container1);
                //var ui2 = FindFirstItemOfType<Ellipse>(container2);

                //if (ui1 is null || ui2 is null) throw new InvalidOperationException("Ellipse not found in containers");

                ////animationService?.PrepareToAnimate("item1", container1 as UIElement);
                ////animationService?.PrepareToAnimate("item2", container2 as UIElement);

                //animationService?.PrepareToAnimate("item1", SelectedItem1);
                //animationService?.PrepareToAnimate("item2", SelectedItem2);
            }
        });
    }

    private T? FindFirstItemOfType<T>(DependencyObject container)
        where T : DependencyObject
    {
        for (int i = 0; i < VisualTreeHelper.GetChildrenCount(container); i++)
        {
            var child = VisualTreeHelper.GetChild(container, i);
            if (child is null) return null;
            
            if (child is T item)
            {
                return item;
            }
            T? childOfChild = FindFirstItemOfType<T>(child);
            if (childOfChild is not null)
            {
                return childOfChild;
            }
        }
        return null;
    }

    public MainViewModel ViewModel
    {
        get => (MainViewModel)GetValue(ViewModelProperty);
        set => SetValue(ViewModelProperty, value);
    }

    public static readonly DependencyProperty ViewModelProperty =
        DependencyProperty.Register("ViewModel", typeof(MainViewModel), typeof(SelectionView), new PropertyMetadata(null));
}
