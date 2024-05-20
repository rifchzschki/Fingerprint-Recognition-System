using Avalonia.Controls;
using Avalonia.Media.Imaging;
using System.Collections.Generic;
using System.IO;

namespace Tubes3.Views;

public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
    }

    [System.Obsolete]
    private async void UploadButton_Click(object sender, Avalonia.Interactivity.RoutedEventArgs e)
    {
        var openFileDialog = new OpenFileDialog
        {
            AllowMultiple = false,
            Filters = new List<FileDialogFilter>
                {
                    new FileDialogFilter { Name = "Bitmap Images", Extensions = { "bmp" } }
                }
        };

        var result = await openFileDialog.ShowAsync(this);
        if (result != null && result.Length > 0)
        {
            var filePath = result[0];
            if (File.Exists(filePath))
            {
                var bitmap = new Bitmap(filePath);
                UploadedImage.Source = bitmap;
            }
        }
    }
}