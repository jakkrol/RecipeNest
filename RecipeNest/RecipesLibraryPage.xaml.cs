using RecipeNest.ViewModels;
using System.Diagnostics;

namespace RecipeNest;

public partial class RecipesLibraryPage : ContentPage
{
	public RecipeLibraryViewModel vm = new RecipeLibraryViewModel();
    public RecipesLibraryPage()
	{
		InitializeComponent();
		this.BindingContext = vm;
    }

    private async void CollectionView_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        Debug.WriteLine("CLICKED");
        var selectedRecipe = e.CurrentSelection.FirstOrDefault() as Models.Recipe;
        if (selectedRecipe != null)
            await Shell.Current.GoToAsync($"RecipeDetailPage?recipeId={selectedRecipe.Id}&source=internet");
    }

    private void MainPicker_SelectedIndexChanged(object sender, EventArgs e)
    {
        //Debug.WriteLine($"INDEX: {MainPicker.SelectedItem}");

        switch (MainPicker.SelectedItem)
        {
            case "Country":
                MySearchbar.IsVisible = false;
                MyPicker.IsVisible = true;
                break;

            case "Category":
                MySearchbar.IsVisible = false;
                MyPicker.IsVisible = true;
                break;

            case "Ingredient":
                MySearchbar.IsVisible = false;
                MyPicker.IsVisible = true;
                break;

            default:
                MySearchbar.IsVisible = true;
                MyPicker.IsVisible = false;
                break;
        }
    }

    private async void TapGestureRecognizer_Tapped(object sender, TappedEventArgs e)
    {
        Debug.WriteLine("Header CLICKED");
        
        var recipeId = vm.RecipeOfTheDay.Id;
        await Shell.Current.GoToAsync($"RecipeDetailPage?recipeId={recipeId}&source=internet");
    }
}