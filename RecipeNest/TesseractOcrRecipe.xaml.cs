using System.Text.RegularExpressions;
using TesseractOcrMaui;

namespace RecipeNest;

public partial class TesseractOcrRecipe : ContentPage
{
	public TesseractOcrRecipe()
	{
		InitializeComponent();
	}
    private async void OnPickPhotoClicked(object sender, EventArgs e)
    {
        try
        {
            var result = await FilePicker.PickAsync(new PickOptions
            {
                FileTypes = FilePickerFileType.Images,
                PickerTitle = "Wybierz zdj�cie"
            });

            if (result != null)
            {
                using var stream = await result.OpenReadAsync();
                var fullText = await PerformOcrAsync(stream);

                var lower = fullText.ToLower();
                int idxIngredients = lower.IndexOf("sk�adniki");
                int idxPreparation = lower.IndexOf("przygotowanie");

                string ingredients = "";
                string preparation = "";
                string title = "";

                if (idxIngredients >= 0 && idxPreparation > idxIngredients)
                {
                    ingredients = fullText.Substring(idxIngredients, idxPreparation - idxIngredients).Trim();
                    preparation = fullText.Substring(idxPreparation).Trim();
                }
                //OcrResultLabel.Text = fullText;
                ingredients = NormalizeByColumnsIngredients(ingredients);
                Ingredients.Text = ingredients;
                Instructions.Text = preparation;

                //await Shell.Current.GoToAsync($"{nameof(AddRecipePage)}?name={Uri.EscapeDataString(title)}&ingredients={Uri.EscapeDataString(ingredients)}&instructions={Uri.EscapeDataString(preparation)}");
            }
        }
        catch (Exception ex)
        {
            await DisplayAlert("B��d", $"Nie uda�o si� wykona� OCR: {ex.Message}", "OK");
        }
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

    string NormalizeByColumnsIngredients(string rawText)
    {
        var lines = rawText
            .Split(new[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries)
            .Select(l => l.Trim())
            .Where(l => !string.IsNullOrWhiteSpace(l))
            .ToList();

        // Znajd� indeks nag��wka "Sk�adniki"
        int startIndex = lines.FindIndex(l => Regex.IsMatch(l, @"(?i)^sk�adniki$"));
        if (startIndex == -1) return "";

        // Od tego miejsca analizuj linie
        var ingredientsSection = lines.Skip(startIndex + 1).ToList();

        // Odfiltruj �mieci typu "Autor", "�atwe", "czas", itp.
        ingredientsSection = ingredientsSection
            .Where(l => !Regex.IsMatch(l, @"(?i)(autor|�atwe|�|min\.|przepis|czas|l\s�|=\s*)"))
            .ToList();

        // Podziel list�: zak�adamy pierwsza po�owa to nazwy, druga po�owa to ilo�ci
        int half = ingredientsSection.Count / 2;

        var names = ingredientsSection.Take(half).ToList();
        var amounts = ingredientsSection.Skip(half).ToList();

        var result = new List<string>();
        for (int i = 0; i < Math.Max(names.Count, amounts.Count); i++)
        {
            string name = i < names.Count ? names[i] : "(brak nazwy)";
            string amount = i < amounts.Count ? amounts[i] : "(brak ilo�ci)";
            result.Add($"{name} � {amount}");
        }

        return string.Join(Environment.NewLine, result);
    }

}