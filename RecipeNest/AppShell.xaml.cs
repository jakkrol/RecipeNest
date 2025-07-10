namespace RecipeNest
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();
            Routing.RegisterRoute("AddRecipePage", typeof(AddRecipePage));
        }
    }
}
