using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RecipeNest.Models;
using System.Collections.ObjectModel;

namespace RecipeNest.Services
{
    public class RecipeService
    {
        private static RecipeService _instance;
        public static RecipeService Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new RecipeService();
                }
                return _instance;
            }
        }

        public ObservableCollection<Recipe> Recipes { get; }

        private RecipeService()
        {

            Recipes = new ObservableCollection<Recipe>
            {
                new Recipe
                {
                    Name = "Spaghetti Carbonara",
                    Category = "Italian",
                    Description = "A classic Italian pasta dish made with eggs, cheese, pancetta, and pepper.",
                    Ingredients = new List<string> { "Spaghetti", "Eggs", "Parmesan cheese", "Pancetta", "Black pepper" },
                    Instructions = "Cook spaghetti. In a bowl, mix eggs and cheese. Fry pancetta. Combine all with pepper.",
                    //ImageUrl = "https://example.com/spaghetti-carbonara.jpg"
                    },
                new Recipe
                {
                    Name = "Chicken Curry",
                    Category = "Indian",
                    Description = "A spicy and flavorful chicken dish cooked in a rich curry sauce.",
                    Ingredients = new List<string> { "Chicken", "Curry powder", "Coconut milk", "Onion", "Garlic" },
                    Instructions = "Sauté onion and garlic. Add chicken and curry powder. Pour in coconut milk and simmer.",
                    //ImageUrl = "https://example.com/chicken-curry.jpg"
                }
            };
        }
    }
}
