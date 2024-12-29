using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia;
using Avalonia.Media;
using Avalonia.Media.Imaging;
using System.Threading.Tasks;
using System.Collections.Generic;
using System;

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

        var pdfCheck = new PdfCheck();
        bool textExtracted, pdfUaCompliant, pdfHasTags;
        (bool, string?, string?) metadata;
        List<(string, string)> altTexts;

            

        try{
            textExtracted = pdfCheck.CanExtractText(pdfPath);
            StpRes.Children.Add(InitializeResultGrid((textExtracted ? "Text ist extrahierbar" : "Text ist nicht extrahierbar"), textExtracted ? new SolidColorBrush(Color.FromRgb(34,86,39)) : new SolidColorBrush(Color.FromRgb(86,34,39)), "Text-Extrahierbarkeit ist ein wichtiger Bestandteil eines barrierefreien PDFs: es ermöglicht Screen-Readern (also die Möglichkeit, das PDF vorlesen zu lassen), den Text korrekt zu interpretieren und alles vorzulesen"));
        } catch (NotImplementedException niex) {
            StpRes.Children.Add(InitializeResultGrid("Feature nicht implementiert: Text Extraktion", new SolidColorBrush(Color.FromRgb(86,86,94)), ""));
            
        }


        try{
            pdfHasTags = pdfCheck.DocumentTagCheck(pdfPath);

        } catch (NotImplementedException niex) {
            StpRes.Children.Add(InitializeResultGrid("Feature nicht implementiert: PDF Tags", new SolidColorBrush(Color.FromRgb(86,86,94)), ""));
            
        }

        
        try{
            altTexts = pdfCheck.HasAltTexts(pdfPath);

        } catch (NotImplementedException niex) {
            StpRes.Children.Add(InitializeResultGrid("Feature nicht implementiert: Alt-Texte hinter Bildern/Grafiken", new SolidColorBrush(Color.FromRgb(86,86,94)), "Derzeit noch nicht implementiert\nAlternativtexte sind ein wichtiges Element in barrierefreien PDFs. Dies sind nicht-sichtbare Texte für Bilder oder andere Grafiken, die es Screenreadern ermöglichen, seheingeschränkten Personen über das Bild zu informieren."));
        }

        
        try{
            metadata = pdfCheck.HasDocumentMetadata(pdfPath);
            StpRes.Children.Add(InitializeResultGrid((metadata.Item1 ? "Titel- und Autor-Tag sind vorhanden" : "Titel- und/oder Autor-Tag fehlen"), metadata.Item1 ? new SolidColorBrush(Color.FromRgb(34,86,39)) : new SolidColorBrush(Color.FromRgb(86,34,39)), $"Autor: {metadata.Item2}\nTitel: {metadata.Item3}\nMetadaten in PDFs bieten die Möglichkeit, z.B. Autor und Titel eines Dokumentes unabhängig vom Inhalt festzuhalten."));

        } catch (NotImplementedException niex) {
            StpRes.Children.Add(InitializeResultGrid("Feature nicht implementiert: PDF Metadaten", new SolidColorBrush(Color.FromRgb(86,86,94)), ""));
            
        }

        
        try{
            pdfUaCompliant = pdfCheck.IsPdfUaCompliant(pdfPath);
            
        } catch (NotImplementedException niex) {
            StpRes.Children.Add(InitializeResultGrid("Feature nicht implementiert: PDF/UA Kompatibilität", new SolidColorBrush(Color.FromRgb(86,86,94)), "Derzeit noch nicht implementiert\nPDF/UA Kompatibilität bedeutet das Vorhandensein von Struktur im PDF, also intern als Überschrift deklarierte Überschriften, als Absätze deklarierte Absätze und eine geregelte Text-Reihenfolge in der PDF-Struktur. Das bezieht sich dabei nicht auf die äußerlich sichtbaren Elemente im PDF, sondern im Hintergrund vorhandene Eigenschaften des PDFs.\nWenn Sie das PDF mit LaTeX erstellt haben und Überschriften als \\section deklariert haben, sollte diese Eigenschaft erfüllt sein"));
            
        }

    }

    public Grid InitializeResultGrid(string resCat, SolidColorBrush backgroundColor, string description){
        Grid g = new Grid(){
            Margin = new Thickness(10,10,10,10),
            Background=backgroundColor,
        };

        StackPanel sp = new StackPanel(){

        };

        TextBlock l = new TextBlock(){
            Text=resCat,
            FontSize=18,
            Margin=new Thickness(10,10,10,10)
        };
        TextBlock t = new TextBlock(){
            Text=description,
            FontSize=12,
            TextWrapping = Avalonia.Media.TextWrapping.Wrap,
            Margin=new Thickness(10,10,10,10)

        };

        sp.Children.Add(l);
        sp.Children.Add(t);
        g.Children.Add(sp);

        return g;
    }

    public void WndResult_Closing(object sender, WindowClosingEventArgs e){
        mw.Show();
    }
}