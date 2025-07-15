using RecipeNest.ViewModels;

namespace RecipeNest;

public partial class RecipeDetailPage : ContentPage
{
	public RecipeDetailPageViewModel ViewModel = new RecipeDetailPageViewModel();
	public RecipeDetailPage()
	{
		InitializeComponent();
		this.BindingContext = ViewModel;
	}
}