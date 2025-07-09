using System;
using System.Collections.Generic;
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
        public ICommand AddRecipeCommand { get; }
        public RecipeViewModel()
        {
            PerformSearchCommand = new Command(PerformSearch);
            AddRecipeCommand = new Command(AddRecipe);
        }

        private void PerformSearch()
        {
            Debug.WriteLine($"Searching for: {SearchText}");
            // Logic to perform search
            // This could involve filtering a list of recipes based on a search term
            // For example, you might filter a collection of recipes by name or category
        }
        private void AddRecipe()
        {
            // Logic to add a new recipe
            // This could involve navigating to a new page where the user can enter recipe details
            // or opening a dialog to input the recipe information
        }




        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
