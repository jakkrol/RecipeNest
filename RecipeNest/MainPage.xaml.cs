using RecipeNest.DbConfig;
using RecipeNest.Services;
using RecipeNest.ViewModels;
using System.Diagnostics;
namespace RecipeNest
{
    public partial class MainPage : ContentPage
    {
        public MainPageViewModel ViewModel = new MainPageViewModel();
        public MainPage()
        {
            InitializeComponent();

            this.BindingContext = ViewModel;
        }
        protected override async void OnAppearing()
        {
            base.OnAppearing();
            Debug.WriteLine("DB PATH:  " + DbConfig.Constants.DatabasePath);
            await RecipeService.Instance.LoadRecipesFromDb();
            await ShoppingListService.Instance.LoadShoppingListsFromDb();
        }
        private async void OnBrowseRecipes(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync("///RecipesPage");
        }

        private async void OnAddRecipe(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync("AddRecipePage");
        }
    }
}
