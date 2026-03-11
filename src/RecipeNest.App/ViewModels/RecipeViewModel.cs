using RecipeNest.Services;
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

        private string order;
        public string Order
        {
            get => order;
            set
            {
                if(order != value)
                {
                    order = value;
                    //Debug.WriteLine("DEBUG: " + order);
                    OnPropertyChanged(nameof(Order));
                    PerformOrderSelectionCommand.Execute(this);
                }
            }
        }

        public ICommand PerformSearchCommand { get; }
        public ICommand PerformOrderSelectionCommand {  get; }
        public ICommand DeleteRecipeCommand { get; }

        private readonly RecipeService _recipeService;
        public RecipeViewModel(RecipeService recipeService)
        {
            _recipeService = recipeService;
            Recipes = recipeService.Recipes;
            //Recipes = Services.RecipeService.Instance.Recipes;
            FilteredRecipes = new ObservableCollection<Models.Recipe>(Recipes);
            Recipes.CollectionChanged += Recipes_CollectionChanged;
            PerformSearchCommand = new Command(PerformSearch);
            DeleteRecipeCommand = new Command<Models.Recipe>(DeleteRecipe);
            PerformOrderSelectionCommand = new Command(PerformOrder);
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

            PerformOrder();
        }

        private void PerformOrder()
        {
            Debug.WriteLine($"ORDERING BY: {Order}");
            if(Order == "Asc")
            FilteredRecipes = new ObservableCollection<Models.Recipe>(FilteredRecipes.OrderBy(x => x.Name));

            if(Order == "Desc")
            FilteredRecipes = new ObservableCollection<Models.Recipe>(FilteredRecipes.OrderByDescending(x => x.Name));
        }

        private async void DeleteRecipe(Models.Recipe recipe)
        {
            //Debug.WriteLine($"Deleting recipe: {recipe.Name}");
            await _recipeService.RemoveRecipe(recipe);
        }

        public async void checkItem(Models.Recipe Item)
        {
            //Debug.WriteLine("ITEMEK: " + Item.Name);


            await _recipeService.CheckItem(Item);
        }


        public event PropertyChangedEventHandler PropertyChanged;

            protected void OnPropertyChanged(string propertyName)
            {
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
