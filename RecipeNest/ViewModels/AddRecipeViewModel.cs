using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using RecipeNest.Models;
using RecipeNest.Services;

namespace RecipeNest.ViewModels
{
    [QueryProperty(nameof(RecipeId), "recipeId")]
    public class AddRecipeViewModel : INotifyPropertyChanged
    {
        public ICommand SaveRecipeCommand { get; }
        public string Name { get; set; } = "";
        public string Category { get; set; } = "";
        public string Description { get; set; } = "";
        public string Ingredients { get; set; } = "";
        public string Instructions { get; set; } = "";
        public string ImageUrl { get; set; } = "";

        private string recipeId;
        public string RecipeId
        {
            get => recipeId;
            set
            {
                recipeId = value;
                LoadRecipe();
            }
        }

        public AddRecipeViewModel()
        {
            SaveRecipeCommand = new Command(SaveRecipe);
        }
        private void LoadRecipe()
        {
            if (string.IsNullOrEmpty(RecipeId))
                return;
            var recipe = RecipeService.Instance.Recipes.FirstOrDefault(r => r.Id == RecipeId);
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
            OnPropertyChanged(nameof(ImageUrl));
        }

        private async void SaveRecipe()
        {
            if (recipeId != null)
            {
                Services.RecipeService.Instance.UpdateRecipe(recipeId, Name, Category, Description, Ingredients, Instructions, ImageUrl);
            }
            Services.RecipeService.Instance.AddNewRecipe(Name, Category, Description, Ingredients, Instructions, ImageUrl);
            await Shell.Current.GoToAsync("..");
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

        }
    }
}
