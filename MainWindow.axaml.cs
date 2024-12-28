using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia;
using Avalonia.Media;
using Avalonia.Media.Imaging;

namespace PLG_Pdac;

public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
        ImageV.Source = new Bitmap("resources/LOGO_white.png");
    }

    public void Upload_Click(object sender, RoutedEventArgs e){

    }
}