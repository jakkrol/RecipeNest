using RecipeNest.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RecipeNest.Models;
using System.Diagnostics;

namespace RecipeNest.ViewModels
{
    [QueryProperty(nameof(Source), "source")]
    [QueryProperty(nameof(RecipeId), "recipeId")]
    public class RecipeDetailPageViewModel : INotifyPropertyChanged
    {
        private readonly RecipeService _recipeService;
        private readonly RecipeApiService _recipeApiService;

        public RecipeDetailPageViewModel(RecipeService recipeService, RecipeApiService recipeApiService)
        {
            _recipeService = recipeService;
            _recipeApiService = recipeApiService;
        }

        public string Name { get; set; } = "";
        public string Category { get; set; } = "";
        public string Description { get; set; } = "";
        public string Ingredients { get; set; } = "";
        public string Instructions { get; set; } = "";
        public string ImageUrl { get; set; } = "";


        private string? source;
        public string? Source
        {
            get => source;
            set
            {
                source = value;

            }
        }

private Guid? recipeId;
        public string? RecipeId
        {
            get => recipeId.ToString();
            set
            {
                recipeId = Guid.Parse(value);
                LoadRecipeDetails();
            }
        }

        async private void LoadRecipeDetails()
        {
            if (!recipeId.HasValue)
                return;

            if (source == "local")
            {
                var recipe = _recipeService.Recipes.FirstOrDefault(r => r.Id == recipeId.Value);
                if (recipe != null)
                {
                    Name = recipe.Name;
                    Category = recipe.Category;
                    Description = recipe.Description;
                    Ingredients = string.Join(", ", recipe.Ingredients);
                    Instructions = recipe.Instructions;
                    ImageUrl = recipe.ImageUrl;
                }
                OnPropertyChanged(nameof(Name));
                OnPropertyChanged(nameof(Category));
                OnPropertyChanged(nameof(Description));
                OnPropertyChanged(nameof(Ingredients));
                OnPropertyChanged(nameof(Instructions));
            }
            if(source == "internet")
            {
                Debug.WriteLine("FROM INTERNET");
                //RecipeApiService _apiService = new RecipeApiService();
                var recipe = await _recipeApiService.getRecipeById(Convert.ToInt32(recipeId));
                Debug.WriteLine("TEST RECIPE: " + recipe.Name);
                if(recipe != null)
                {
                    Name = recipe.Name;
                    Category = recipe.Category;
                    Description = recipe.Description;
                    Ingredients = string.Join(", ", recipe.Ingredients);
                    Instructions = recipe.Instructions;
                    ImageUrl = recipe.ImageUrl;
                }
                OnPropertyChanged(nameof(Name));
                OnPropertyChanged(nameof(Category));
                OnPropertyChanged(nameof(Description));
                OnPropertyChanged(nameof(Ingredients));
                OnPropertyChanged(nameof(Instructions));
            }
        }


        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
