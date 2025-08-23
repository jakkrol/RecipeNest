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

    protected override void OnAppearing()
    {
        base.OnAppearing();
        MyCollection.SelectedItem = null;
    }

    private async void CollectionView_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        var selectedRecipe = e.CurrentSelection.FirstOrDefault() as Models.Recipe;
        if(selectedRecipe != null) 
        await Shell.Current.GoToAsync($"RecipeDetailPage?recipeId={selectedRecipe.Id}&source=local");
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

    private async void TapGestureRecognizer_Tapped(object sender, TappedEventArgs e)
    {
        
        //var border = (Border)sender;
        //var selectedRecipe = (Models.Recipe)border.BindingContext;
        //await Shell.Current.GoToAsync($"RecipeDetailPage?recipeId={selectedRecipe.Id}");
    }

    private void CollectionView_SelectionChanged_1(object sender, SelectionChangedEventArgs e)
    {

    }

    private void CheckBox_CheckedChanged(object sender, CheckedChangedEventArgs e)
    {
        var checkbox = sender as CheckBox;
        var item = checkbox?.BindingContext as Models.Recipe;

        if (item != null)
        {
            var viewModel = BindingContext as RecipeViewModel;
            viewModel?.checkItem(item);
        }
    }
}