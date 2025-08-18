using RecipeNest.Models;
using RecipeNest.Services;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Windows.Input;

namespace RecipeNest.ViewModels
{
    public class RecipeLibraryViewModel : INotifyPropertyChanged
    {
        private readonly RecipeApiService _apiService;

        public ObservableCollection<Recipe> Recipes { get; set; } = new();

        private Recipe _recipeOfTheDay;
        public Recipe RecipeOfTheDay
        {
            get => _recipeOfTheDay;
            set
            {
                if (_recipeOfTheDay != value)
                {
                    _recipeOfTheDay = value;
                    OnPropertyChanged(nameof(RecipeOfTheDay));
                }
            }
        }

        public List<string> SearchModes { get; } = new()
        {
            "Name", "Ingredient", "Category", "Country"
        };

        private string _selectedSearchMode;
        public string SelectedSearchMode
        {
            get => _selectedSearchMode;
            set
            {
                if (_selectedSearchMode != value)
                {
                    _selectedSearchMode = value;
                    OnPropertyChanged(nameof(SelectedSearchMode));
                }
            }
        }

        private string _searchQuery;
        public string SearchQuery
        {
            get => _searchQuery;
            set
            {
                if (_searchQuery != value)
                {
                    _searchQuery = value;
                    OnPropertyChanged(nameof(SearchQuery));
                }
            }
        }

        public ICommand SearchCommand { get; }

        public RecipeLibraryViewModel()
        {
            _apiService = new Services.RecipeApiService();
            SearchCommand = new Command(async () => await FetchRecipes());

            //Debug.WriteLine("---------------------------------------------------------------------------------");
            //_apiService.getAllCategories();
            //_apiService.getAllRegions();
            //_apiService.getAllIngredients();

            Task.Run(async () =>
            {
                RecipeOfTheDay = await _apiService.SearchRecipeOfDay();
            });
        }

        private async Task FetchRecipes()
        {
            if (string.IsNullOrWhiteSpace(SearchQuery))
                return;

            var results = await _apiService.SearchRecipesAsync(SelectedSearchMode, SearchQuery);

            Recipes.Clear();
            foreach (var recipe in results)
                Recipes.Add(recipe);
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string propertyName) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
