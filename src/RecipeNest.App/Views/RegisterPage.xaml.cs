using RecipeNest.ViewModels;

namespace RecipeNest.Views;

public partial class RegisterPage : ContentPage
{
	private readonly RegisterPageViewModel _viewModel;
    public RegisterPage(RegisterPageViewModel vm)
	{
		InitializeComponent();
		_viewModel = vm;
		this.BindingContext = _viewModel;
	}
}