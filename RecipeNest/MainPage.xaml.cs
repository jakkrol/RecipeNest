using RecipeNest.ViewModels;
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
