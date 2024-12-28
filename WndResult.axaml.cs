using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia;
using Avalonia.Media;
using Avalonia.Media.Imaging;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace PLG_Pdac;

public partial class WndResult : Window
{
    public string filePath;
    private MainWindow mw;
    public WndResult(string path, MainWindow main)
    {
        InitializeComponent();

        filePath = path;
        mw = main;
    }
}