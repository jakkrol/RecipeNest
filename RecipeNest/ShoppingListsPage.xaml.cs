using RecipeNest.ViewModels;

namespace RecipeNest;

public partial class ShoppingListsPage : ContentPage
{
	public ShoppingListViewModel ViewModel = new ShoppingListViewModel();
	public ShoppingListsPage()
	{
		InitializeComponent();
		this.BindingContext = ViewModel;
	}

    private async void AddList_Clicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("AddShoppingListPage");
    }

    private async void MyCollection_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        var selectedRecipe = e.CurrentSelection.FirstOrDefault() as Models.ShoppingList;
        if (selectedRecipe != null)
            await Shell.Current.GoToAsync($"ShoppingListsDetailsPage?listId={selectedRecipe.Id}");
    }
}