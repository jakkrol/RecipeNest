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
}