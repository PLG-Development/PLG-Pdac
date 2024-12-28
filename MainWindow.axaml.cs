using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia;
using Avalonia.Media;
using Avalonia.Media.Imaging;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace PLG_Pdac;

public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
        ImageV.Source = new Bitmap("resources/LOGO_white.png");
    }

    public async void Upload_Click(object sender, RoutedEventArgs e){
        WndResult w = new WndResult(await OpenPdfFileAsync(this), this);
        w.Show();
        this.Hide();
    }

    public async Task<string?> OpenPdfFileAsync(Window window)
    {
        var dialog = new OpenFileDialog
        {
            Title = "PDF-Datei wählen...",
            Filters = new List<FileDialogFilter>
            {
                new FileDialogFilter { Name = "PDF Dateien", Extensions = { "pdf" } }
            }
        };

        var result = await dialog.ShowAsync(window);
        return result?.Length > 0 ? result[0] : null;
    }
}