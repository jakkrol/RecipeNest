using TesseractOcrMaui;

namespace RecipeNest;

public partial class TesseractTests : ContentPage
{
	public TesseractTests()
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
                var text = await PerformOcrAsync(stream);
                OcrResultLabel.Text = text;
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
}