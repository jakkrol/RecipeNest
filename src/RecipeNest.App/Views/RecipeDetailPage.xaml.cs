using RecipeNest.ViewModels;

namespace RecipeNest;

public partial class RecipeDetailPage : ContentPage
{
    public RecipeDetailPageViewModel ViewModel { get; }
    public RecipeDetailPage(RecipeDetailPageViewModel viewModel)
	{
		InitializeComponent();
		ViewModel = viewModel;
        this.BindingContext = ViewModel;
	}
}