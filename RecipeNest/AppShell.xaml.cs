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
        }
    }
}
