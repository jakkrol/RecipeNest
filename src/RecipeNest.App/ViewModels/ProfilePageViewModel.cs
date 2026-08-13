using CommunityToolkit.Mvvm.Input;
using RecipeNest.BackendServices;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecipeNest.ViewModels
{
    public partial class ProfilePageViewModel
    {
        private SyncDbData _syncDbData;
        public ProfilePageViewModel(SyncDbData syncDbData) 
        { 
            _syncDbData = syncDbData;
        }


        [RelayCommand]
        private async Task Synchronize()
        {
            Debug.WriteLine("TEST SYNCHRONIZACJI");
        }


    }
}
