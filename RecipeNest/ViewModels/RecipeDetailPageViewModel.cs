using RecipeNest.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RecipeNest.Models;

namespace RecipeNest.ViewModels
{
    [QueryProperty(nameof(RecipeId), "recipeId")]
    public class RecipeDetailPageViewModel : INotifyPropertyChanged
    {
        public string Name { get; set; } = "";
        public string Category { get; set; } = "";
        public string Description { get; set; } = "";
        public string Ingredients { get; set; } = "";
        public string Instructions { get; set; } = "";
        public string ImageUrl { get; set; } = "";

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

            var recipe = RecipeService.Instance.Recipes.FirstOrDefault(r => r.Id == recipeId.Value);
            if (recipe != null)
            {
                Name = recipe.Name;
                Category = recipe.Category;
                Description = "Opis: " + recipe.Description;
                Ingredients = "Ingridients: " + string.Join(", ", recipe.Ingredients);
                Instructions = "Instructions: " + recipe.Instructions;
                ImageUrl = recipe.ImageUrl;
            }
            OnPropertyChanged(nameof(Name));
            OnPropertyChanged(nameof(Category));
            OnPropertyChanged(nameof(Description));
            OnPropertyChanged(nameof(Ingredients));
            OnPropertyChanged(nameof(Instructions));
        }


        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
