using RecipeNest.BackendServices;
using RecipeNest.Shared.DTO;
using System.Diagnostics;

namespace RecipeNest
{
    public partial class AppShell : Shell
    {
        AuthService _auth;
        public AppShell()
        {
            InitializeComponent();
            Routing.RegisterRoute("RecipesPage", typeof(RecipesPage));
            Routing.RegisterRoute("AddRecipePage", typeof(AddRecipePage));
            Routing.RegisterRoute("RecipeDetailPage", typeof(RecipeDetailPage));
            Routing.RegisterRoute("ShoppingListsPage", typeof(ShoppingListsPage));
            Routing.RegisterRoute("AddShoppingListPage", typeof(AddShoppingListPage));
            Routing.RegisterRoute("ShoppingListsDetailsPage", typeof(ShoppingListsDetailsPage));
            Routing.RegisterRoute("OcrPage", typeof(OcrPage));
            Routing.RegisterRoute("RecipesLibraryPage", typeof(RecipesLibraryPage));

            // --- For teting purposes only, to be removed later ---
            _auth = IPlatformApplication.Current.Services.GetService<AuthService>();
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
           
            LoginDTO loginuUser = new LoginDTO
            {
                Login = "123",
                Password = "123"
            };
            await _auth.Login(loginuUser);
        }

        //private async void OnShellNavigating(object sender, ShellNavigatedEventArgs e)
        //{
        //    await ShoppingList.Navigation.PopToRootAsync();
        //}
    }
}
