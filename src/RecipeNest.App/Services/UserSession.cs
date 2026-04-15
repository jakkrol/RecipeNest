using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecipeNest.Services
{
    public class UserSession : BindableObject
    {
        private bool _isLoggedIn;
        public bool IsLoggedIn
        {
            get => _isLoggedIn;
            set 
            {
                _isLoggedIn = value;
                OnPropertyChanged(nameof(IsLoggedIn));
            }
        }

        private Guid? _userId;
        public Guid? UserId
        {
            get => _userId;
            set
            {
                _userId = value;
                OnPropertyChanged(nameof(UserId));
            }
        }


        public UserSession()
        {

        }

        public void StartSession(Guid userId)
        {
            UserId = userId;
            IsLoggedIn = true;
            Preferences.Set("UserId", userId.ToString());
        }

        public void ClearSession()
        {
            UserId = null;
            IsLoggedIn = false;
            Preferences.Remove("UserId");
        }
    }
}
