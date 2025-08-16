using RecipeNest.ViewModels;

namespace RecipeNest;

public partial class RecipesLibraryPage : ContentPage
{
	public RecipeLibraryViewModel vm = new RecipeLibraryViewModel();
    public RecipesLibraryPage()
	{
		InitializeComponent();
		this.BindingContext = vm;
    }
}