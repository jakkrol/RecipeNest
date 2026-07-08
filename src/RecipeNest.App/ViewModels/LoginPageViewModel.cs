using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using RecipeNest.BackendServices;
using RecipeNest.Views;

namespace RecipeNest.ViewModels
{
    public partial class LoginPageViewModel : ObservableObject 
    {
        [ObservableProperty]
        private string _login = "";
        [ObservableProperty]
        private string _password = "";
        [ObservableProperty]
        private bool _isLoginError = false;

        private readonly AuthService _authService;

        public LoginPageViewModel(AuthService authService)
        {
            _authService = authService;
        }

        [RelayCommand]
        private async Task PerformLogin()
        {
            IsLoginError = false;
            Debug.WriteLine("GIGA TEST");
            Debug.WriteLine(Login + ", " + Password);
            bool res = await _authService.Login(new Shared.DTO.LoginDTO { Login = Login, Password = Password });
            if (res)
            {
                await Shell.Current.GoToAsync("//MainPage");

            }
            else
            {
                Debug.WriteLine("Login failed");
                IsLoginError = true;
            }
        }

        [RelayCommand]
        private async Task GoToRegister()
        {
            await Shell.Current.GoToAsync(nameof(RegisterPage));
        }
    }
}
