using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace RecipeNest.ViewModels
{
    public partial class LoginPageViewModel : ObservableObject 
    {
        [ObservableProperty]
        private string _login;
        [ObservableProperty]
        private string _password;


        [RelayCommand]
        private void PerformLogin()
        {
            Debug.WriteLine("GIGA TEST");
            Debug.WriteLine(Login + ", " + Password);
        }
    }
}
