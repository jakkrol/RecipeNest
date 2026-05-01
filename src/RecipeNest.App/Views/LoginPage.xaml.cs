using RecipeNest.ViewModels;

namespace RecipeNest.Views;

public partial class LoginPage : ContentPage
{
	public LoginPageViewModel vm { get; set; }
	public LoginPage(LoginPageViewModel viewModel)
	{
		InitializeComponent();
		vm = viewModel;
		this.BindingContext = vm;
	}
}