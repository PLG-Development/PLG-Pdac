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
    public WndResult(string path)
    {
        InitializeComponent();

        filePath = path;
    }
}