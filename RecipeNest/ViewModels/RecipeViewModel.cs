using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

    namespace RecipeNest.ViewModels
    {
        public class RecipeViewModel : INotifyPropertyChanged
        {
        private ObservableCollection<Models.Recipe> Recipes;
        //public ObservableCollection<Models.Recipe> FilteredRecipes { get; set; } = null;
        private ObservableCollection<Models.Recipe> filteredRecipes;
        public ObservableCollection<Models.Recipe> FilteredRecipes
        {
            get => filteredRecipes;
            set
            {
                if (filteredRecipes != value)
                {
                    filteredRecipes = value;
                    OnPropertyChanged(nameof(FilteredRecipes));
                }
            }
        }

        private string searchText;
        public string SearchText
        {
            get => searchText;
            set
            {
                if (searchText != value)
                {
                    searchText = value;
                    OnPropertyChanged(nameof(SearchText));
                }
            }
        }
        public ICommand PerformSearchCommand { get; }
        public ICommand DeleteRecipeCommand { get; }
        public RecipeViewModel()
        {
            Recipes = Services.RecipeService.Instance.Recipes;
            FilteredRecipes = new ObservableCollection<Models.Recipe>(Recipes);
            Recipes.CollectionChanged += Recipes_CollectionChanged;
            PerformSearchCommand = new Command(PerformSearch);
            DeleteRecipeCommand = new Command<Models.Recipe>(DeleteRecipe);
        }

        private void Recipes_CollectionChanged(object? sender, NotifyCollectionChangedEventArgs e)
        {
            PerformSearch();
        }

        private void PerformSearch()
        {
                Debug.WriteLine($"Searching for: {SearchText}");
                if(SearchText == null || SearchText == "")
                {
                    FilteredRecipes = Recipes;
                }
                else
                {
                    FilteredRecipes = new ObservableCollection<Models.Recipe>(
                        Recipes.Where(r => r.Name.Contains(SearchText, StringComparison.OrdinalIgnoreCase) || r.Category.Contains(SearchText, StringComparison.OrdinalIgnoreCase) || r.Description.Contains(SearchText, StringComparison.OrdinalIgnoreCase)));
                }
        }

        private void DeleteRecipe(Models.Recipe recipe)
        {
            Debug.WriteLine($"Deleting recipe: {recipe.Name}");
            Services.RecipeService.Instance.Recipes.Remove(recipe);
        }


        public event PropertyChangedEventHandler PropertyChanged;

            protected void OnPropertyChanged(string propertyName)
            {
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
