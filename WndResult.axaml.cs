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
        Check(filePath);
    }


    public void Check(string pdfPath)
    {

        try{
            var pdfCheck = new PdfCheck();
            bool textExtracted = pdfCheck.CanExtractText(pdfPath);
            var metadata = pdfCheck.GetMetadata(pdfPath);
            List<(string, string)> altTextFound = pdfCheck.HasAltTexts(pdfPath);
            bool tagsPresent = pdfCheck.HasDocumentTags(pdfPath);
            bool pdfUaCompliant = pdfCheck.IsPdfUaCompliant(pdfPath);


            LblResult.Content  = "Text exrahierbar: " + textExtracted;
            LblResult.Content += "\nAlt-Text gefunden: " + altTextFound;
            LblResult.Content += "\nTags vorhanden: " + tagsPresent;
            LblResult.Content += "\nUA-Kompatibilität: " + pdfUaCompliant;
            LblResult.Content += "\nMetadaten: ";
            foreach (var entry in metadata)
            {
                LblResult.Content += $"\n{entry.Key}: {entry.Value}";
            }
        } catch (NotImplementedException niex) {
            
        }
    }

    public void WndResult_Closing(object sender, WindowClosingEventArgs e){
        mw.Show();
    }
}