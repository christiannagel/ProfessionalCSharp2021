using CommunityToolkit.Mvvm.Messaging;

using ConnectedAnimationSample.ViewModels;

using Microsoft.UI.Composition;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Media.Animation;
using Microsoft.UI.Xaml.Shapes;

using System.Runtime.InteropServices.WindowsRuntime;


namespace ConnectedAnimationSample.Views;

public sealed partial class DisplayListView : UserControl
{
    public DisplayListView()
    {
        InitializeComponent();

        WeakReferenceMessenger.Default.Register<AddItemMessage>(this, (r, m) =>
        {
            if (m.Status == "Completed")
            {
                var connectedAnimation = ConnectedAnimationService.GetForCurrentView();
                var f = connectedAnimation.DefaultEasingFunction;
                connectedAnimation.DefaultEasingFunction = CompositionEasingFunction.CreateElasticEasingFunction(f.Compositor, CompositionEasingFunctionMode.Out, 5, 5); //.CreateBackEasingFunction(f.Compositor, CompositionEasingFunctionMode.Out, 20);
                var animation1 = connectedAnimation?.GetAnimation("item1");
                var animation2 = connectedAnimation?.GetAnimation("item2");

                var container = AddedItemsList.ItemContainerGenerator.ContainerFromItem(m.DisplayItem);

                var items = FindItems<Ellipse>(container);

                ConnectedAnimation?[] animations = new[] { animation1, animation2 };
                Ellipse[] targets = items.ToArray();

                for (int i = 0; i < 2; i++)
                {
                    animations[i]?.TryStart(targets[i]);
                }
            }
        });
    }

    private IEnumerable<T> FindItems<T>(DependencyObject container) where T : DependencyObject
    {
        var items = new List<T>();
        var children = VisualTreeHelper.GetChildrenCount(container);
        for (int i = 0; i < children; i++)
        {
            var child = VisualTreeHelper.GetChild(container, i);
            if (child is T)
            {
                items.Add((T)child);
            }
            else
            {
                items.AddRange(FindItems<T>(child));
            }
        }
        return items;
    }

    public MainViewModel ViewModel
    {
        get => (MainViewModel)GetValue(ViewModelProperty);
        set => SetValue(ViewModelProperty, value);
    }

    public static readonly DependencyProperty ViewModelProperty =
        DependencyProperty.Register("ViewModel", typeof(MainViewModel), typeof(SelectionView), new PropertyMetadata(null));
}
