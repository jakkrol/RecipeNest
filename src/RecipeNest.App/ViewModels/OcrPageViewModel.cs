using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using TesseractOcrMaui;

namespace RecipeNest.ViewModels
{
    public class OcrPageViewModel : INotifyPropertyChanged
    {

        private string fullText = String.Empty;
        public string FullText
        {
            get => fullText;
            set
            {
                if (fullText != value)
                {
                    fullText = value;
                    OnPropertyChanged(nameof(FullText));
                }
            }
        }

        private void OnPropertyChanged(string v)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(v));
        }



        public async Task ProcessImageAsync(Stream imageStream)
        {
            string result = await PerformOcrAsync(imageStream);
            FullText = result;
            //Debug.WriteLine($"OCR Result: {fullText}");
        }




        private async Task<string> PerformOcrAsync(Stream imageStream)
        {
            var tessdataPath = Path.Combine(FileSystem.AppDataDirectory, "tessdata");
            using var engine = new TessEngine("pol", tessdataPath);
            using var img = Pix.LoadFromMemory(await ReadFullyAsync(imageStream));
            using var page = engine.ProcessImage(img);
            return page.GetText();
        }
        private static async Task<byte[]> ReadFullyAsync(Stream input)
        {
            using var ms = new MemoryStream();
            await input.CopyToAsync(ms);
            return ms.ToArray();
        }


        private string RemoveJunkLines(string rawText)
        {
            var junkPattern = new Regex(@"(?i)(autor|łatwe|czas|min\.|przepis|©|email|listonic|^[-=]+$|^\d+$|^\d{6,}$)");

            var cleanedLines = rawText
                .Split(new[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries)
                .Select(l => l.Trim())
                .Where(l => !string.IsNullOrWhiteSpace(l))
                .Where(l => !junkPattern.IsMatch(l)) // remove junk lines
                .Select(l => Regex.Replace(l, @"\.{2,}", ".")) // normalize multiple dots
                .Select(l => Regex.Replace(l, @"(.)\1{2,}", "$1")) // remove bad format
                .ToList();

            return string.Join(Environment.NewLine, cleanedLines);
        }

        public event PropertyChangedEventHandler? PropertyChanged;
    }
}
