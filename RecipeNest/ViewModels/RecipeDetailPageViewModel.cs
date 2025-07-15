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
        public int? RecipeId
        {
            get => recipeId;
            set
            {
                recipeId = value;
                LoadRecipeDetails();
            }
        }


        private void LoadRecipeDetails()
        {
            if (!RecipeId.HasValue)
                return;

            var recipe = RecipeService.Instance.Recipes.FirstOrDefault(r => r.Id == RecipeId.Value);
            if (recipe != null)
            {
                Name = recipe.Name;
                Category = recipe.Category;
                Description = recipe.Description;
                Ingredients = string.Join(", ", recipe.Ingredients);
                Instructions = recipe.Instructions;
                ImageUrl = recipe.ImageUrl;
            }
        }


        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
