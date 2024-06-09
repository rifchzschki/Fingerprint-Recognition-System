using Avalonia;
using Avalonia.ReactiveUI;
using System;

namespace Tubes3;

sealed class Program
{
    // Initialization code. Don't use any Avalonia, third-party APIs or any
    // SynchronizationContext-reliant code before AppMain is called: things aren't initialized
    // yet and stuff might break.
    private static void Initialize()
    {
        Console.WriteLine("Encrypt database...");
        // dijalankan ketika pertama kali ingin mengenkripsi (jika database sudah terenkripsi maka comment kode di bawah)
        // Models.Converter.EncryptDatabase();
        
    }
    [STAThread]
    public static void Main(string[] args)
    {
        Initialize();
        BuildAvaloniaApp().StartWithClassicDesktopLifetime(args);
    }

    // Avalonia configuration, don't remove; also used by visual designer.
    public static AppBuilder BuildAvaloniaApp()
        => AppBuilder.Configure<App>()
            .UsePlatformDetect()
            .WithInterFont()
            .LogToTrace()
            .UseReactiveUI();
}
