using UglyToad.PdfPig;
using UglyToad.PdfPig.Content;
using UglyToad.PdfPig.Geometry;
using System;
using System.Collections.Generic;
using System.IO;

namespace PLG_Pdac
{
    public class PdfCheck
    {
        public bool CanExtractText(string pdfPath)
        {
            try
            {
                using (var pdfDocument = PdfDocument.Open(pdfPath))
                {
                    for (int i = 0; i < pdfDocument.NumberOfPages; i++)
                    {
                        var page = pdfDocument.GetPage(i + 1);
                        var pageText = page.Text;
                        if (!string.IsNullOrWhiteSpace(pageText))
                        {
                            return true; // Text vorhanden
                        }
                    }
                }
            }
            catch
            {
                return false; // Fehler beim Lesen der Datei
            }
            return false; // Kein extrahierbarer Text gefunden
        }

        public Dictionary<string, string> GetMetadata(string pdfPath)
        {
            var metadata = new Dictionary<string, string>();
            try
            {
                using (var pdfDocument = PdfDocument.Open(pdfPath))
                {
                    // PdfPig bietet eine einfache Möglichkeit, Metadaten zu extrahieren
                    var documentInfo = pdfDocument.Information;
                    metadata["Title"] = documentInfo.Title ?? "Nicht vorhanden";
                    metadata["Author"] = documentInfo.Author ?? "Nicht vorhanden";
                    metadata["Subject"] = documentInfo.Subject ?? "Nicht vorhanden";
                    metadata["Keywords"] = documentInfo.Keywords ?? "Nicht vorhanden";
                }
            }
            catch
            {
                metadata["Error"] = "Fehler beim Lesen der Metadaten.";
            }
            return metadata;
        }

        public bool HasAltTexts(string pdfPath)
        {
            try
            {
                using (var pdfDocument = PdfDocument.Open(pdfPath))
                {
                    foreach (var page in pdfDocument.GetPages())
                    {
                        // PdfPig bietet keine direkte Möglichkeit, Alt-Texte auszulesen, daher verwenden wir einen Platzhalter.
                        // Sie können hier eine erweiterte Logik einbauen, die nach Bild-Tags oder Alt-Texten sucht.
                        var text = page.Text;
                        if (text.Contains("Alt-Text"))
                        {
                            return true; // Beispiel: Wenn Alt-Text im Text gefunden wird
                        }
                    }
                }
            }
            catch
            {
                return false; // Fehler beim Lesen der Datei
            }
            return false; // Kein Alt-Text gefunden
        }

        public bool HasDocumentTags(string pdfPath)
        {
            try
            {
                using (var pdfDocument = PdfDocument.Open(pdfPath))
                {
                    // PdfPig bietet keine spezifische API zur Extraktion von Tags, daher wird hier der Vorabcheck verwendet.
                    // Es kann eine erweiterte Logik hinzugefügt werden, die nach Tags in der Struktur sucht.
                    return false; // PdfPig hat keine eingebaute Tag-Extraktion wie iText
                }
            }
            catch
            {
                return false; // Fehler beim Lesen der Datei
            }
        }

        public bool IsPdfUaCompliant(string pdfPath)
        {
            try
            {
                using (var pdfDocument = PdfDocument.Open(pdfPath))
                {
                    // PdfPig bietet keine direkte Unterstützung für die Überprüfung von PDF/UA-Konformität
                    // Ein möglicher Ansatz könnte sein, nach markierten Katalog-Objekten zu suchen, aber es gibt keine eingebaute Funktion.
                    return false; // PdfPig bietet keine direkte Überprüfung für PDF/UA
                }
            }
            catch
            {
                return false; // Fehler beim Lesen der Datei
            }
        }

        // static void Main(string[] args)
        // {
        //     string pdfPath = "Pfad/zur/deiner/PDF-Datei.pdf";

        //     var pdfCheck = new PdfCheck();

        //     // Text extrahieren und prüfen
        //     bool textExtracted = pdfCheck.CanExtractText(pdfPath);
        //     Console.WriteLine($"Kann Text extrahieren: {textExtracted}");

        //     // Metadaten extrahieren
        //     var metadata = pdfCheck.GetMetadata(pdfPath);
        //     Console.WriteLine("Metadaten:");
        //     foreach (var entry in metadata)
        //     {
        //         Console.WriteLine($"{entry.Key}: {entry.Value}");
        //     }

        //     // Überprüfen, ob Alt-Texte vorhanden sind
        //     bool altTextFound = pdfCheck.HasAltTexts(pdfPath);
        //     Console.WriteLine($"Alt-Text vorhanden: {altTextFound}");

        //     // Überprüfen, ob Dokument-Tags vorhanden sind
        //     bool tagsPresent = pdfCheck.HasDocumentTags(pdfPath);
        //     Console.WriteLine($"Dokument-Tags vorhanden: {tagsPresent}");

        //     // Überprüfen, ob das PDF PDF/UA-konform ist
        //     bool pdfUaCompliant = pdfCheck.IsPdfUaCompliant(pdfPath);
        //     Console.WriteLine($"PDF/UA-konform: {pdfUaCompliant}");
        // }
    }
}
