using RecipeNest.ViewModels;
using System.Diagnostics;
using System.Threading.Tasks;
namespace RecipeNest;

public partial class RecipesPage : ContentPage
{
	public RecipeViewModel ViewModel = new RecipeViewModel();
	public RecipesPage()
	{
		InitializeComponent();
		this.BindingContext = ViewModel;
    }

    private void CollectionView_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
		
    }

    private async void AddRecipe_Clicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("AddRecipePage");
    }

    private async void Update_Clicked(object sender, EventArgs e)
    {
        Models.Recipe selectedRecipe = (Models.Recipe)((Button)sender).BindingContext;
        await Shell.Current.GoToAsync($"AddRecipePage?recipeId={selectedRecipe.Id}");
    }

    private void TapGestureRecognizer_Tapped(object sender, TappedEventArgs e)
    {

    }
}