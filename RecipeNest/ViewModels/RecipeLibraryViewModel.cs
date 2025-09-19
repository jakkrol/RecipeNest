
using RecipeNest.Models;
using RecipeNest.Services;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Text.Json;
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
                    SelectionModeChanged();
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

        private string _selectedData;
        public string SelectedData
        {
            get => _selectedData;
            set
            {
                if (_selectedData != value)
                {
                    _selectedData = value;
                    FetchRecipes(SelectedData);
                    OnPropertyChanged(nameof(SelectedData));
                }
            }
        }

        private List<string> _searchingData;
        public List<string> SearchingData
        {
            get => _searchingData;
            set
            {
                if (_searchingData != value)
                {
                    _searchingData = value;
                    OnPropertyChanged(nameof(SearchingData));
                }
            }
        }

        public ICommand SearchCommand { get; }

        public RecipeLibraryViewModel()
        {
            Debug.WriteLine("FETCHING READY!!!");
            _apiService = new Services.RecipeApiService();
            SearchCommand = new Command(async () => await FetchRecipes(SearchQuery));


            //Debug.WriteLine("---------------------------------------------------------------------------------");
            //_apiService.getAllCategories();
            //_apiService.getAllRegions();
            //_apiService.getAllIngredients();


            Task.Run(async () =>
            {
                RecipeOfTheDay = await _apiService.SearchRecipeOfDay();
            });
        }

        private async Task SelectionModeChanged()
        {
           List<string> ls = new List<string>();
            switch (SelectedSearchMode)
            {
                case "Country":
                    Debug.WriteLine("Selection mode = Country");
                    ls = await _apiService.getAllRegions();
                    break;

                case "Category":
                    Debug.WriteLine("Selection mode = Category");
                    ls = await _apiService.getAllCategories();
                    break;

                case "Ingredient":
                    Debug.WriteLine($"Selection mode = Ingredient");
                    ls = await _apiService.getAllIngredients();
                    break;

                default:
                    Debug.WriteLine($"Unknown selection mode: {SelectedSearchMode}");
                    break;
            }
            
            foreach (string s in ls)
            {
                Debug.WriteLine(s);
            }
            SearchingData = ls;

        }
        private async Task FetchRecipes(string search)
        {
            Debug.WriteLine(search);
            if (string.IsNullOrWhiteSpace(search))
                return;

            var results = await _apiService.SearchRecipesAsync(SelectedSearchMode, search);

            Recipes.Clear();
            foreach (var recipe in results)
                Recipes.Add(recipe);
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string propertyName) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
