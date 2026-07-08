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
    [QueryProperty(nameof(SourceId), "sourceId")]
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

        private Int32 sourceId;
        public string? SourceId
        {
            get => sourceId.ToString();
            set
            {
                if (!string.IsNullOrEmpty(value))
                    sourceId = int.Parse(value);
                LoadRecipeDetails();
            }
        }

        async private void LoadRecipeDetails()
        {
            if (source == "local")
            {
                if (!recipeId.HasValue)
                    return;
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
            else if(source == "internet")
            {
                Debug.WriteLine("FROM INTERNET");
                //RecipeApiService _apiService = new RecipeApiService();
                var recipe = await _recipeApiService.getRecipeById(sourceId);
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
