using RecipeNest.ViewModels;
using SkiaSharp;
using System.Net.Sockets;
using System.Text.RegularExpressions;
using TesseractOcrMaui;


namespace RecipeNest;

public partial class OcrPage : ContentPage
{
    private SKBitmap _rawBitmap;
    
    private SKRect _cropRect = new SKRect(50, 50, 250, 250);


    private float _lastTouchX, _lastTouchY;
    private bool _isResizing = false;
    private const float TouchThreshold = 40f; 

    
    private SKRect _displayedImageRect;



    public string fullText = "";
    private string selectedMode = "FullText";
    private string ingredients = "";
    private string preparation = "";

    public OcrPageViewModel vm { get; }
    public OcrPage()
	{
		InitializeComponent();
        vm = new OcrPageViewModel();
        this.BindingContext = vm;
    }

    private async void OnPickPhotoClicked(object sender, EventArgs e)
    {
        try
        {
            var result = await MediaPicker.Default.PickPhotoAsync();

            if (result != null)
            {
                using var stream = await result.OpenReadAsync();
                if (BindingContext is OcrPageViewModel viewModel)
                {
                    await viewModel.ProcessImageAsync(stream);
                }
            }
        }
        catch (Exception ex)
        {
            await DisplayAlert("B³¹d", ex.Message, "OK");
        }
        
    }

    private void CanvasView_PaintSurface(object sender, SkiaSharp.Views.Maui.SKPaintSurfaceEventArgs e)
    {

    }

    private void PanGestureRecognizer_PanUpdated(object sender, PanUpdatedEventArgs e)
    {

    }

    private void OnCropClicked(object sender, EventArgs e)
    {

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