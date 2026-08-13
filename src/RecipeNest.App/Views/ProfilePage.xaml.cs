using RecipeNest.ViewModels;

namespace RecipeNest.Views;

public partial class ProfilePage : ContentPage
{
	private ProfilePageViewModel vm { get; set; }
	public ProfilePage(ProfilePageViewModel viewModel)
	{
		InitializeComponent();
		vm = viewModel;
		this.BindingContext = vm;
	}
}