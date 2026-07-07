using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecipeNest.Services
{
    public partial class UserSession : ObservableObject
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

        [ObservableProperty]
        private string? userName = "";


        public UserSession()
        {

        }

        public void StartSession(Guid userId, string username)
        {
            UserId = userId;
            UserName = username;
            IsLoggedIn = true;
            Preferences.Set("UserId", userId.ToString());
            Debug.WriteLine("User session started for user: " + UserName + " with ID: " + UserId);
        }

        public void ClearSession()
        {
            UserId = null;
            UserName = "";
            IsLoggedIn = false;
            Preferences.Remove("UserId");
        }
    }
}
