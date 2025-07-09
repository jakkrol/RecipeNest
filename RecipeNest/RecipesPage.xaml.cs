using RecipeNest.ViewModels;
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

    private void AddRecipe_Clicked(object sender, EventArgs e)
    {

    }
}