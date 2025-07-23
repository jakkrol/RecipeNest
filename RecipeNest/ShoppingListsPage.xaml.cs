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

    protected override void OnAppearing()
    {
        base.OnAppearing();
        //(BindingContext as ShoppingListViewModel)?.RefreshShoppingLists();
        MyCollection.SelectedItem = null;
        ViewModel.UpdateProgress();
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

    private async void Update_Clicked(object sender, EventArgs e)
    {
        Models.ShoppingList selectedList = (Models.ShoppingList)((Button)sender).BindingContext;
        await Shell.Current.GoToAsync($"AddShoppingListPage?listId={selectedList.Id}");
    }
}