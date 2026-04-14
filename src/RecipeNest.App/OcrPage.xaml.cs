using RecipeNest.ViewModels;
using SkiaSharp;
using System.Diagnostics;
using System.Net.Sockets;
using System.Text.RegularExpressions;
using TesseractOcrMaui;


namespace RecipeNest;

public partial class OcrPage : ContentPage
{
    private SKBitmap _rawBitmap;
    
    private SKRect _cropRect = new SKRect(50, 50, 250, 250);
    private SKRect _startRect;


    private float _lastTouchX, _lastTouchY;
    private const float TouchThreshold = 20f; 

    
    private SKRect _displayedImageRect;

    private enum ResizeCorner
    {
        None,
        RightTop,
        RightBottom,
        LeftTop,
        LeftBottom
    }
    private ResizeCorner _activeResizeCorner = ResizeCorner.None;

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
                _rawBitmap = SKBitmap.Decode(stream);
                CanvasView.InvalidateSurface();
                //if (BindingContext is OcrPageViewModel viewModel)
                //{
                //    await viewModel.ProcessImageAsync(stream);
                //}
            }
        }
        catch (Exception ex)
        {
            await DisplayAlert("B³¹d", ex.Message, "OK");
        }
        
    }

    private void CanvasView_PaintSurface(object sender, SkiaSharp.Views.Maui.SKPaintSurfaceEventArgs e)
    {
        SKCanvas canvas = e.Surface.Canvas;
        canvas.Clear(SKColors.Black);

        if(_rawBitmap == null) return;

        float canvasWidth = e.Info.Width;
        float canvasHeight = e.Info.Height;

        float bitmapWidth = _rawBitmap.Width;
        float bitmapHeight = _rawBitmap.Height;

        float scale = Math.Min(canvasWidth / bitmapWidth, canvasHeight / bitmapHeight);

        float x = (canvasWidth - bitmapWidth * scale) / 2;
        float y = (canvasHeight - bitmapHeight * scale) / 2;

        _displayedImageRect = new SKRect(x, y, x + bitmapWidth * scale, y + bitmapHeight * scale);
        canvas.DrawBitmap(_rawBitmap, _displayedImageRect);


        using var strokePaint = new SKPaint
        {
            Color = SKColors.Red,
            Style = SKPaintStyle.Stroke,
            StrokeWidth = 3,
            IsAntialias = true
        };

        canvas.DrawRect(_cropRect, strokePaint);

    }

    private bool isDragging = false;
    
    private void PanGestureRecognizer_PanUpdated(object sender, PanUpdatedEventArgs e)
    {
        if (!isDragging || _rawBitmap == null) return;
        float density = (float)DeviceDisplay.Current.MainDisplayInfo.Density;

        float currentX = (float)e.TotalX * density;
        float currentY = (float)e.TotalY * density;

        switch (e.StatusType)
        {
            case GestureStatus.Started:

                _lastTouchX = currentX;
                _lastTouchY = currentY;
                break;

            case GestureStatus.Running:
                float deltaX = currentX - _lastTouchX;
                float deltaY = currentY - _lastTouchY;

                switch (_activeResizeCorner)
                {
                    case ResizeCorner.RightTop:
                        Debug.WriteLine($"Resizing RightTop: currentX={currentX}, currentY={currentY}");
                        _cropRect.Right += deltaX;
                        _cropRect.Top += deltaY;

                        if(_cropRect.Right < _cropRect.Left)
                        {
                            _activeResizeCorner = ResizeCorner.LeftTop;
                            _startRect = _cropRect;
                        }
                        if(_cropRect.Top > _cropRect.Bottom)
                        {
                            _activeResizeCorner = ResizeCorner.RightBottom;
                            _startRect = _cropRect;
                        }
                        break;
                    case ResizeCorner.RightBottom:
                        Debug.WriteLine($"Resizing RightBottom: currentX={currentX}, currentY={currentY}");
                        _cropRect.Right += deltaX;
                        _cropRect.Bottom += deltaY;

                        if(_cropRect.Right < _cropRect.Left)
                        {
                            _activeResizeCorner = ResizeCorner.LeftBottom;
                            _startRect = _cropRect;
                        }
                        if(_cropRect.Bottom < _cropRect.Top)
                        {
                            _activeResizeCorner = ResizeCorner.RightTop;
                            _startRect = _cropRect;
                        }
                        break;
                    case ResizeCorner.LeftTop:
                        Debug.WriteLine($"Resizing LeftTop: currentX={currentX}, currentY={currentY}");
                        _cropRect.Left += deltaX;
                        _cropRect.Top += deltaY;
                        if(_cropRect.Left > _cropRect.Right)
                        {
                            _activeResizeCorner = ResizeCorner.RightTop;
                            _startRect = _cropRect;
                        }
                        if(_cropRect.Top > _cropRect.Bottom)
                        {
                            _activeResizeCorner = ResizeCorner.LeftBottom;
                            _startRect = _cropRect;
                        }
                        break;
                    case ResizeCorner.LeftBottom:
                        Debug.WriteLine($"Resizing LeftBottom: currentX={currentX}, currentY={currentY}");
                        _cropRect.Left += deltaX;
                        _cropRect.Bottom += deltaY;
                        if(_cropRect.Left > _cropRect.Right)
                        {
                            _activeResizeCorner = ResizeCorner.RightBottom;
                            _startRect = _cropRect;
                        }
                        if(_cropRect.Bottom < _cropRect.Top)
                        {
                            _activeResizeCorner = ResizeCorner.LeftTop;
                            _startRect = _cropRect;
                        }
                        break;
                }

                if (_activeResizeCorner == ResizeCorner.None)
                {
                    _cropRect.Offset(deltaX, deltaY);
                }

                _lastTouchX = currentX;
                _lastTouchY = currentY;
                CanvasView.InvalidateSurface();
                break;
        }
    }

    private void PointerGestureRecognizer_PointerPressed(object sender, PointerEventArgs e)
    {
        if (_rawBitmap == null) return;

        var point = e.GetPosition(CanvasView);
        float density = (float)DeviceDisplay.Current.MainDisplayInfo.Density;
        SKPoint touchPoint = new SKPoint((float)point.Value.X * density, (float)point.Value.Y * density);

        _startRect = _cropRect;

        float t = TouchThreshold * density;
        
        //Check if touch is near one of the corners for resizing
        bool nearRightTop = Math.Abs(touchPoint.X - _cropRect.Right) < t && Math.Abs(touchPoint.Y - _cropRect.Top) < t;
        bool nearRightBottom = Math.Abs(touchPoint.X - _cropRect.Right) < t && Math.Abs(touchPoint.Y - _cropRect.Bottom) < t;
        bool nearLeftTop = Math.Abs(touchPoint.X - _cropRect.Left) < t && Math.Abs(touchPoint.Y - _cropRect.Top) < t;
        bool nearLeftBottom = Math.Abs(touchPoint.X - _cropRect.Left) < t && Math.Abs(touchPoint.Y - _cropRect.Bottom) < t;
        if (nearLeftTop)
        {
            _activeResizeCorner = ResizeCorner.LeftTop;
        }
        else if (nearRightTop)
        {
            _activeResizeCorner = ResizeCorner.RightTop;
        }
        else if (nearRightBottom)
        {
            _activeResizeCorner = ResizeCorner.RightBottom;
        }
        else if (nearLeftBottom)
        {
            _activeResizeCorner = ResizeCorner.LeftBottom;
        }
        else
        {
            _activeResizeCorner = ResizeCorner.None;
        }


        if (_cropRect.Contains(touchPoint))
        {
            isDragging = true;
        }
        else
        {
            isDragging = false;
        }
    }

    async private void OnCropClicked(object sender, EventArgs e)
    {
        Debug.WriteLine(_cropRect.Left + ", " + _cropRect.Top + ", " + _cropRect.Right + ", " + _cropRect.Bottom);

        if (_rawBitmap == null) return;
        try
        {
            float scaleX = _rawBitmap.Width / _displayedImageRect.Width;
            float scaleY = _rawBitmap.Height / _displayedImageRect.Height;

            float cropLeft = (_cropRect.Left - _displayedImageRect.Left) * scaleX;
            float cropTop = (_cropRect.Top - _displayedImageRect.Top) * scaleY;
            float cropWidth = _cropRect.Width * scaleX;
            float cropHeight = _cropRect.Height * scaleY;

            SKRectI cropRect = new SKRectI((int)cropLeft, (int)cropTop, (int)(cropLeft + cropWidth), (int)(cropTop + cropHeight));

            using var croppedBitmap = new SKBitmap(cropRect.Width, cropRect.Height);
            if (_rawBitmap.ExtractSubset(croppedBitmap, cropRect))
            {
                using var image = SKImage.FromBitmap(croppedBitmap);
                using var data = image.Encode(SKEncodedImageFormat.Png, 100);
                using var stream = data.AsStream();
                await vm.ProcessImageAsync(stream);
            }
        }
        catch (Exception ex)
        {
            await DisplayAlert("B³¹d", ex.Message, "OK");
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