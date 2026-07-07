using RecipeNest.BackendServices;
using RecipeNest.Services;
using RecipeNest.Shared.DTO;
using RecipeNest.Views;
using System.Diagnostics;

namespace RecipeNest
{
    public partial class AppShell : Shell
    {
        //AuthService _auth;
        UserSession _userSession;
        public AppShell(UserSession userSession)
        {
            InitializeComponent();
            _userSession = userSession;
            this.BindingContext = _userSession;

            Routing.RegisterRoute("RecipesPage", typeof(RecipesPage));
            Routing.RegisterRoute("AddRecipePage", typeof(AddRecipePage));
            Routing.RegisterRoute("RecipeDetailPage", typeof(RecipeDetailPage));
            Routing.RegisterRoute("ShoppingListsPage", typeof(ShoppingListsPage));
            Routing.RegisterRoute("AddShoppingListPage", typeof(AddShoppingListPage));
            Routing.RegisterRoute("ShoppingListsDetailsPage", typeof(ShoppingListsDetailsPage));
            Routing.RegisterRoute("OcrPage", typeof(OcrPage));
            Routing.RegisterRoute("RecipesLibraryPage", typeof(RecipesLibraryPage));

            Routing.RegisterRoute("LoginPage", typeof(LoginPage));
            Routing.RegisterRoute("RegisterPage", typeof(RegisterPage));
            Routing.RegisterRoute("ProfilePage", typeof(ProfilePage));

            // --- For teting purposes only, to be removed later ---
            //_auth = IPlatformApplication.Current.Services.GetService<AuthService>();
            //this.Navigated += OnShellNavigating;
        }

        private void ToolbarItem_Clicked(object sender, EventArgs e)
        {
            Application.Current.UserAppTheme = Application.Current.UserAppTheme == AppTheme.Light ? (AppTheme.Dark) : AppTheme.Light;
            if (sender is ToolbarItem toolbarItem)
                ThemeItem.Text = Application.Current.UserAppTheme == AppTheme.Dark ? "☀️" : "🌙";
        }


        ///Only for testing purposes, to be removed later
        async private void Account_Clicked(object sender, EventArgs e)
        {
            if (_userSession.IsLoggedIn)
            {
                await Current.GoToAsync("/ProfilePage");
            }
            else
            {
                await Current.GoToAsync("/LoginPage");
            }
            //LoginDTO loginuUser = new LoginDTO
            //{
            //    Login = "123",
            //    Password = "123"
            //};
            //await _auth.Login(loginuUser);
        }

        //private async void OnShellNavigating(object sender, ShellNavigatedEventArgs e)
        //{
        //    await ShoppingList.Navigation.PopToRootAsync();
        //}
    }
}
