using RecipeNest.ViewModels;
namespace RecipeNest;

public partial class AddRecipePage : ContentPage
{
	public AddRecipeViewModel ViewModel = new AddRecipeViewModel();
    public AddRecipePage()
	{
		InitializeComponent();
		this.BindingContext = ViewModel;
    }
}