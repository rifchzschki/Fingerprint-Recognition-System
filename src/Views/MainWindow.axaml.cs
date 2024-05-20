using Avalonia.Controls;
using Tubes3.ViewModels;

namespace Tubes3.Views;

public partial class MainWindow : Window
{
    [System.Obsolete]
    public MainWindow()
    {
        InitializeComponent();
        DataContext = new MainWindowViewModel(); // Replace "parameter" with the appropriate value
    }
}