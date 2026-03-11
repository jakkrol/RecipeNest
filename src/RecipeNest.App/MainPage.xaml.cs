using RecipeNest.BackendServices;
using RecipeNest.DbConfig;
using RecipeNest.Services;
using RecipeNest.ViewModels;
using System.Diagnostics;
namespace RecipeNest
{
    public partial class MainPage : ContentPage
    {
        public MainPageViewModel ViewModel { get; }
        private readonly AuthService _auth;
        private readonly RecipeService _recipeService;
        private readonly ShoppingListService _shoppingListService;
        public MainPage(AuthService sr, RecipeService recipeService, ShoppingListService shoppingListService)
        {
            InitializeComponent();
            _auth = sr;
            _recipeService = recipeService;
            _shoppingListService = shoppingListService;

            this.BindingContext = ViewModel;
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            Debug.WriteLine("DB PATH:  " + Constants.DatabasePath);
            await _recipeService.LoadRecipesFromDb();
            await _shoppingListService.LoadShoppingListsFromDb();
            

            //HttpClient cl = new HttpClient();
            //BackendServices.AuthService sr = new BackendServices.AuthService(cl);
            //await _auth.addUser();
            //var test = await _auth.getUsers();
            //await sr.getUsers();
            //await DisplayAlert("Test", test[0].Name, "X");
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
