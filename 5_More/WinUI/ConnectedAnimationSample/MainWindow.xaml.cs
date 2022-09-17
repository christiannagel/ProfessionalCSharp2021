using ConnectedAnimationSample.ViewModels;

using Microsoft.UI.Xaml;

namespace ConnectedAnimationSample;
public sealed partial class MainWindow : Window
{
    public MainWindow()
    {
        ViewModel = new MainViewModel();
        InitializeComponent();
    }

    public MainViewModel ViewModel { get; }
}
