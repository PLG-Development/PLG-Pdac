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
                    metadata["Title"] = documentInfo.Title ?? "[Fehlt]";
                    metadata["Author"] = documentInfo.Author ?? "[Fehlt]";
                    metadata["Subject"] = documentInfo.Subject ?? "[Fehlt]";
                    metadata["Keywords"] = documentInfo.Keywords ?? "[Fehlt]";
                }
            }
            catch
            {
                metadata["Error"] = "Fehler beim Lesen der Metadaten.";
            }
            return metadata;
        }

        public List<(string, string)> HasAltTexts(string pdfPath)
        {
            // var imagesWithoutAltText = new List<(string, string)>();
            // try
            // {
            //     using (var pdfDocument = PdfDocument.Open(pdfPath))
            //     {
            //         foreach (var page in pdfDocument.GetPages())
            //         {
            //             var images = page.GetImages();
            //             if (images != null)
            //             {
            //                 foreach (var image in images)
            //                 {
            //                     // PdfPig bietet keine direkte Unterstützung für Alt-Texte,
            //                     // aber wir gehen davon aus, dass Metadaten oder ähnliche Informationen geprüft werden können.
            //                     var altText = image.GetOptionalContentLabel(); // Beispiel-Attribut (anpassbar)
            //                     if (string.IsNullOrWhiteSpace(altText))
            //                     {
            //                         imagesWithoutAltText.Add(($"Seite {page.Number}, Bild ID: {image.Id}", altText));
            //                     }
            //                 }
            //             }
            //         }
            //     }
            // }
            // catch
            // {
            //     // Fehler beim Lesen der Datei
            //     return new List<string> { "Fehler beim Lesen der Datei." };
            // }

            // return imagesWithoutAltText;
            throw new NotImplementedException();
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
