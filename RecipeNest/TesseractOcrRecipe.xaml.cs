using System.Net.Sockets;
using System.Text.RegularExpressions;
using TesseractOcrMaui;

namespace RecipeNest;

public partial class TesseractOcrRecipe : ContentPage
{
    public string fullText = "";
    private string selectedMode = "FullText";
    private string ingredients = "";
    private string preparation = "";
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
                PickerTitle = "Wybierz zdjêcie"
            });

            if (result != null)
            {
                using var stream = await result.OpenReadAsync();
                fullText = await PerformOcrAsync(stream);

                //var lower = fullText.ToLower();
                //int idxIngredients = lower.IndexOf("sk³adniki");
                //int idxPreparation = lower.IndexOf("przygotowanie");

                //ingredients = "";
                //preparation = "";
                //string title = "";

                //if (idxIngredients >= 0 && idxPreparation > idxIngredients)
                //{
                //    ingredients = fullText.Substring(idxIngredients, idxPreparation - idxIngredients).Trim();
                //    preparation = fullText.Substring(idxPreparation).Trim();
                //}
                fullText = RemoveJunkLines(fullText);
                OcrEditor.Text = fullText;
                //ingredients = NormalizeByColumnsIngredients(ingredients);
                //Ingredients.Text = ingredients;
                //Instructions.Text = preparation;

                //await Shell.Current.GoToAsync($"{nameof(AddRecipePage)}?name={Uri.EscapeDataString(title)}&ingredients={Uri.EscapeDataString(ingredients)}&instructions={Uri.EscapeDataString(preparation)}");
            }
        }
        catch (Exception ex)
        {
            await DisplayAlert("B³¹d", $"Nie uda³o siê wykonaæ OCR: {ex.Message}", "OK");
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

    private string RemoveJunkLines(string rawText)
    {
        var junkPattern = new Regex(@"(?i)(autor|³atwe|czas|min\.|przepis|©|email|listonic|^[-=]+$|^\d+$|^\d{6,}$)");

        var cleanedLines = rawText
            .Split(new[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries)
            .Select(l => l.Trim())
            .Where(l => !string.IsNullOrWhiteSpace(l))
            .Where(l => !junkPattern.IsMatch(l)) // remove junk lines
            .Select(l => Regex.Replace(l, @"\.{2,}", ".")) // normalize multiple dots
            .ToList();

        return string.Join(Environment.NewLine, cleanedLines);
    }
    string StandardParser(string rawText)
    {
        // Podziel tekst na linie i usuñ puste linie
        var lines = rawText
            .Split(new[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries)
            .Select(l => l.Trim())
            .Where(l => !string.IsNullOrWhiteSpace(l))
            .ToList();
        // ZnajdŸ indeks nag³ówka "Sk³adniki"
        int startIndex = lines.FindIndex(l => Regex.IsMatch(l, @"(?i)^sk³adniki$"));
        if (startIndex == -1) return "";
        // Od tego miejsca analizuj linie
        var ingredientsSection = lines.Skip(startIndex + 1).ToList();
        // Odfiltruj œmieci typu "Autor", "³atwe", "czas", itp.
        ingredientsSection = ingredientsSection
            .Where(l => !Regex.IsMatch(l, @"(?i)(autor|³atwe|©|min\.|przepis|czas|l\s©|=\s*)"))
            .ToList();
        return string.Join(Environment.NewLine, ingredientsSection);
    }
    string NormalizeByColumnsIngredients(string rawText)
    {
        var lines = rawText
            .Split(new[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries)
            .Select(l => l.Trim())
            .Where(l => !string.IsNullOrWhiteSpace(l))
            .ToList();

        // ZnajdŸ indeks nag³ówka "Sk³adniki"
        int startIndex = lines.FindIndex(l => Regex.IsMatch(l, @"(?i)^sk³adniki$"));
        if (startIndex == -1) return "";

        // Od tego miejsca analizuj linie
        var ingredientsSection = lines.Skip(startIndex + 1).ToList();

        // Odfiltruj œmieci typu "Autor", "³atwe", "czas", itp.
        ingredientsSection = ingredientsSection
            .Where(l => !Regex.IsMatch(l, @"(?i)(autor|³atwe|©|min\.|przepis|czas|l\s©|=\s*)"))
            .ToList();

        // Podziel listê: zak³adamy pierwsza po³owa to nazwy, druga po³owa to iloœci
        int half = ingredientsSection.Count / 2;

        var names = ingredientsSection.Take(half).ToList();
        var amounts = ingredientsSection.Skip(half).ToList();

        var result = new List<string>();
        for (int i = 0; i < Math.Max(names.Count, amounts.Count); i++)
        {
            string name = i < names.Count ? names[i] : "(brak nazwy)";
            string amount = i < amounts.Count ? amounts[i] : "(brak iloœci)";
            result.Add($"{name} – {amount}");
        }

        return string.Join(Environment.NewLine, result);
    }

    private void RadioButton_CheckedChanged(object sender, CheckedChangedEventArgs e)
    {
        if (e.Value && sender is RadioButton rb)
        {
            selectedMode = rb.Value?.ToString();
            if (selectedMode == "FullText")
            {
                // Handle FullText mode
                // You can call your parsing logic here based on selectedMode
                OcrEditor.Text = fullText;
            }
            else if (selectedMode == "Line")
            {
                // Handle Ingredients mode
                // You can call your parsing logic here based on selectedMode
                //Ingredients.Text = StandardParser(ingredients);
                //Instructions.Text = preparation;
                OcrEditor.Text = StandardParser(ingredients);
            }
            else if (selectedMode == "Column")
            {
                // Handle Instructions mode
                // You can call your parsing logic here based on selectedMode
                //Ingredients.Text = NormalizeByColumnsIngredients(ingredients);
                //Instructions.Text = preparation;
                OcrEditor.Text = NormalizeByColumnsIngredients(ingredients);
            }
        }
    }




private async void OnSplitRecipeClicked(object sender, EventArgs e)
    {
        string rawText = OcrEditor.Text ?? "";
        var lines = rawText
            .Split(new[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries)
            .Select(l => l.Trim())
            .ToList();

        int idxTitle = lines.FindIndex(l => Regex.IsMatch(l, @"(?i)^TYT$"));
        int idxIngredients = lines.FindIndex(l => Regex.IsMatch(l, @"(?i)^SKL$"));
        int idxPreparation = lines.FindIndex(l => Regex.IsMatch(l, @"(?i)^PREP$"));

        string title = "";
        string ingredients = "";
        string preparation = "";

        if (idxTitle != -1 && idxIngredients > idxTitle)
            title = string.Join("\n", lines.Skip(idxTitle + 1).Take(idxIngredients - idxTitle - 1));

        if (idxIngredients != -1 && idxPreparation > idxIngredients)
            ingredients = string.Join("\n", lines.Skip(idxIngredients + 1).Take(idxPreparation - idxIngredients - 1));
        else if (idxIngredients != -1)
            ingredients = string.Join("\n", lines.Skip(idxIngredients + 1));

        if (idxPreparation != -1)
            preparation = string.Join("\n", lines.Skip(idxPreparation + 1));

        //Ingredients.Text = ingredients.Trim();
        //Instructions.Text = preparation.Trim();

        //Optional: navigate to AddRecipePage with parsed data
        await Shell.Current.GoToAsync($"{nameof(AddRecipePage)}?name={Uri.EscapeDataString(title)}&ingredients={Uri.EscapeDataString(ingredients)}&instructions={Uri.EscapeDataString(preparation)}");
    }
}