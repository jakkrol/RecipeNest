using RecipeNest.ViewModels;
namespace RecipeNest;

public partial class AddRecipePage : ContentPage
{
	public AddRecipeViewModel ViewModel { get; }
    public AddRecipePage(AddRecipeViewModel viewModel)
	{
		InitializeComponent();
		ViewModel = viewModel;
        this.BindingContext = ViewModel;
    }
}