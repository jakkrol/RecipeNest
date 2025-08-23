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

        private int? recipeId;
        public string? RecipeId
        {
            get => recipeId.ToString();
            set
            {
                recipeId = Convert.ToInt32(value);
                LoadRecipeDetails();
            }
        }

        private void LoadRecipeDetails()
        {
            if (!recipeId.HasValue)
                return;

            if (source == "local")
            {
                var recipe = RecipeService.Instance.Recipes.FirstOrDefault(r => r.Id == recipeId.Value);
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
            }
        }


        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
