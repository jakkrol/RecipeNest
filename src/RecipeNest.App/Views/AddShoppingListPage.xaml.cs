using RecipeNest.ViewModels;

namespace RecipeNest;

public partial class AddShoppingListPage : ContentPage
{
	AddShoppingListViewModel ViewModel { get; }

	public AddShoppingListPage(AddShoppingListViewModel viewModel)
	{
		InitializeComponent();
		ViewModel = viewModel;
        this.BindingContext = ViewModel;
	}
}