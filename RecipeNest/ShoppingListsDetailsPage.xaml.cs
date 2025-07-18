using RecipeNest.ViewModels;

namespace RecipeNest;

public partial class ShoppingListsDetailsPage : ContentPage
{
	ShoppingListDetailsViewModel ViewModel = new ShoppingListDetailsViewModel();
	public ShoppingListsDetailsPage()
	{
		InitializeComponent();
		this.BindingContext = ViewModel;
	}
}