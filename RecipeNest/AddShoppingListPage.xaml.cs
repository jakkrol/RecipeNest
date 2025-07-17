using RecipeNest.ViewModels;

namespace RecipeNest;

public partial class AddShoppingListPage : ContentPage
{
	AddShoppingListViewModel ViewModel = new AddShoppingListViewModel();
	public AddShoppingListPage()
	{
		InitializeComponent();
		this.BindingContext = ViewModel;
	}
}