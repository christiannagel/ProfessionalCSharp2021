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
            static void Animate(ConnectedAnimationService animationService, string key, UIElement target)
            {
                var animation = animationService.GetAnimation(key);
                // animation.Configuration = new GravityConnectedAnimationConfiguration();
                // animation.Configuration = new DirectConnectedAnimationConfiguration();
                // animation.Configuration = new BasicConnectedAnimationConfiguration();
                animation.TryStart(target);
            }
            
            if (m.Status == "Completed")
            {
                var animationService = ConnectedAnimationService.GetForCurrentView();
                var easing = animationService.DefaultEasingFunction;

                // animationService.DefaultEasingFunction = CompositionEasingFunction.CreateBounceEasingFunction(easing.Compositor, CompositionEasingFunctionMode.Out, 3, 1.5F);
                // animationService.DefaultEasingFunction = CompositionEasingFunction.CreateElasticEasingFunction(easing.Compositor, CompositionEasingFunctionMode.Out, 4, 4);
                // animationService.DefaultEasingFunction = CompositionEasingFunction.CreateStepEasingFunction(easing.Compositor, 14);
                //                animationService.DefaultEasingFunction = CompositionEasingFunction.CreatePowerEasingFunction(easing.Compositor, CompositionEasingFunctionMode.Out, 8);
                // connectedAnimation.DefaultEasingFunction = CompositionEasingFunction.CreateElasticEasingFunction(f.Compositor, CompositionEasingFunctionMode.Out, 3, 8); //.CreateBackEasingFunction(f.Compositor, CompositionEasingFunctionMode.Out, 20);
                // animationService.DefaultDuration = TimeSpan.FromSeconds(1.5);

                var container = AddedItemsList.ItemContainerGenerator.ContainerFromItem(m.DisplayItem);

                var items = FindItemsOfType<Ellipse>(container).ToList();
                for (int i = 0; i < items.Count; i++)
                {
                    Animate(animationService, $"item{i + 1}", items[i]);
                }
            }
        });
    }

    private IEnumerable<T> FindItemsOfType<T>(DependencyObject container) 
        where T : DependencyObject
    {
        for (int i = 0; i < VisualTreeHelper.GetChildrenCount(container); i++)
        {
            var child = VisualTreeHelper.GetChild(container, i);
            if (child is null)
            {
                yield break;
            }
            if (child is T item)
            {
                yield return item;
            }

            foreach (T childOfChild in FindItemsOfType<T>(child))
            {
                yield return childOfChild;
            }
        }
    }

    public MainViewModel ViewModel
    {
        get => (MainViewModel)GetValue(ViewModelProperty);
        set => SetValue(ViewModelProperty, value);
    }

    public static readonly DependencyProperty ViewModelProperty =
        DependencyProperty.Register("ViewModel", typeof(MainViewModel), typeof(SelectionView), new PropertyMetadata(null));
}
