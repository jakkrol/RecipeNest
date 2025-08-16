namespace RecipeNest
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();
            Routing.RegisterRoute("RecipesPage", typeof(RecipesPage));
            Routing.RegisterRoute("AddRecipePage", typeof(AddRecipePage));
            Routing.RegisterRoute("RecipeDetailPage", typeof(RecipeDetailPage));
            Routing.RegisterRoute("ShoppingListsPage", typeof(ShoppingListsPage));
            Routing.RegisterRoute("AddShoppingListPage", typeof(AddShoppingListPage));
            Routing.RegisterRoute("ShoppingListsDetailsPage", typeof(ShoppingListsDetailsPage));
            Routing.RegisterRoute("TesseractOcrRecipe", typeof(TesseractOcrRecipe));
            Routing.RegisterRoute("RecipesLibraryPage", typeof(RecipesLibraryPage));

            //this.Navigated += OnShellNavigating;
        }

        private void ToolbarItem_Clicked(object sender, EventArgs e)
        {
            Application.Current.UserAppTheme = Application.Current.UserAppTheme == AppTheme.Light ? (AppTheme.Dark) : AppTheme.Light;
            if (sender is ToolbarItem toolbarItem)
                ThemeItem.Text = Application.Current.UserAppTheme == AppTheme.Dark ? "☀️" : "🌙";
        }

        //private async void OnShellNavigating(object sender, ShellNavigatedEventArgs e)
        //{
        //    await ShoppingList.Navigation.PopToRootAsync();
        //}
    }
}
