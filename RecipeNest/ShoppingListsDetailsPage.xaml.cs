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

    private void CheckBox_CheckedChanged(object sender, CheckedChangedEventArgs e)
    {
        var checkbox = sender as CheckBox;
        var item = checkbox?.BindingContext as Models.ShoppingItem;

        if (item != null)
        {
            var viewModel = BindingContext as ShoppingListDetailsViewModel;
            viewModel?.checkItem(item);
        }
    }
}