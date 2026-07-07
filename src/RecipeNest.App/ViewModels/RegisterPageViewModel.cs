using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using RecipeNest.BackendServices;
using RecipeNest.Shared.DTO;
using RecipeNest.Views;
using System.Diagnostics;

namespace RecipeNest.ViewModels
{
    public partial class RegisterPageViewModel : ObservableObject
    {
        [ObservableProperty]
        private string _username = "";
        [ObservableProperty]
        private string _login = "";
        [ObservableProperty]
        private string _password = "";
        [ObservableProperty]
        private string _confirmPassword = "";

        private readonly AuthService _authService;
        public RegisterPageViewModel(AuthService authService)
        {
            _authService = authService;
        }

        [RelayCommand]
        private async Task PerformRegister()
        {
            Debug.WriteLine("TEST REGISTER: " + _username + ", " + _login + ", " + _password + ", " + _confirmPassword);
            if(_username != "" && _login != "" && _password != "" && _password == _confirmPassword)
            {
                var res = await _authService.Register(new RegisterDTO { Name = _username, Login = _login, Password = _password });
                await Shell.Current.GoToAsync(nameof(LoginPage));
            }   
        }

        [RelayCommand]
        private async Task GoToLogin()
        {
            await Shell.Current.GoToAsync(nameof(LoginPage));
        }
    }
}
