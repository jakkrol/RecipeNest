using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RecipeNest.Models;
using RecipeNest.Services;

namespace RecipeNest.ViewModels
{
    public class MainPageViewModel : INotifyPropertyChanged
    {
        private readonly RecipeService _recipeService;
        public ObservableCollection<Recipe> Recipes => _recipeService.Recipes;
        
        public string RecipesCount => $"You have {Recipes.Count} recipes saved";

        public MainPageViewModel(RecipeService recipeService)
        {
            Recipes.CollectionChanged += (s, e) => OnPropertyChanged(nameof(RecipesCount));
            _recipeService = recipeService;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
