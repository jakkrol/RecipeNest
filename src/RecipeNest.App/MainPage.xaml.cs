using RecipeNest.BackendServices;
using RecipeNest.DbConfig;
using RecipeNest.Services;
using RecipeNest.ViewModels;
using System.Diagnostics;
namespace RecipeNest
{
    public partial class MainPage : ContentPage
    {
        public MainPageViewModel ViewModel = new MainPageViewModel();
        AuthService _auth;
        public MainPage(AuthService sr)
        {
            InitializeComponent();
            this._auth = sr;
            this.BindingContext = ViewModel;

        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            Debug.WriteLine("DB PATH:  " + Constants.DatabasePath);
            await RecipeService.Instance.LoadRecipesFromDb();
            await ShoppingListService.Instance.LoadShoppingListsFromDb();
            

            //HttpClient cl = new HttpClient();
            //BackendServices.AuthService sr = new BackendServices.AuthService(cl);
            var test = await _auth.getUsers();
            //await sr.getUsers();
            await DisplayAlert("Test", test[0].Name, "X");
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
