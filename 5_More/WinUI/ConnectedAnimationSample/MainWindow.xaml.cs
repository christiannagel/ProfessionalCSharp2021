using ConnectedAnimationSample.ViewModels;

using Microsoft.UI.Xaml;

namespace ConnectedAnimationSample;

/// <summary>
/// An empty window that can be used on its own or navigated to within a Frame.
/// </summary>
public sealed partial class MainWindow : Window
{
    public MainWindow()
    {
        ViewModel = new MainViewModel();
        InitializeComponent();
    }

    public MainViewModel ViewModel { get; }
}
