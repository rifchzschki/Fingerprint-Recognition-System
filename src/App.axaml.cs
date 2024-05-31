using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
using Tubes3.ViewModels;
using Tubes3.Views;

namespace Tubes3;

public partial class App : Application
{
    public override void Initialize()
    {
        AvaloniaXamlLoader.Load(this);
    }

    [System.Obsolete]
#pragma warning disable CS0809 // Obsolete member overrides non-obsolete member
    public override void OnFrameworkInitializationCompleted()
#pragma warning restore CS0809 // Obsolete member overrides non-obsolete member
    {
        if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
        {
            desktop.MainWindow = new MainWindow
            {
                DataContext = new MainWindowViewModel(),
            };
        }

        base.OnFrameworkInitializationCompleted();
    }
}