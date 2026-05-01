using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace RecipeNest.ViewModels
{
    public class LoginPageViewModel
    {
        public ICommand LoginCommand { get; set; }

        public LoginPageViewModel() 
        {
            LoginCommand = new Command(Login);
        }

        private void Login()
        {

        }
    }
}
