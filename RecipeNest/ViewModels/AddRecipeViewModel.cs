using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using RecipeNest.Models;
using RecipeNest.Services;

namespace RecipeNest.ViewModels
{
    public class AddRecipeViewModel
    {
        public ICommand SaveRecipeCommand { get; }
        public string Name { get; set; } = "";
        public string Category { get; set; } = "";
        public string Description { get; set; } = "";
        public string Ingredients { get; set; } = "";
        public string Instructions { get; set; } = "";
        public string ImageUrl { get; set; } = "";

        public AddRecipeViewModel()
        {
            SaveRecipeCommand = new Command(SaveRecipe);
        }

        private async void SaveRecipe()
        {   
            Recipe newRecipe = new Recipe
            {
                Name = Name,
                Category = Category,
                Description = Description,
                Ingredients = Ingredients.Split(',').Select(i => i.Trim()).ToList(),
                Instructions = Instructions,
                ImageUrl = ImageUrl
            };
            Services.RecipeService.Instance.Recipes.Add(newRecipe);
            await Shell.Current.GoToAsync(".."); 
        }
    }
}
