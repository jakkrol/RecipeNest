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
                },
                new Recipe
                {
                    Name = "Beef Tacos",
                    Category = "Mexican",
                    Description = "Crunchy taco shells filled with seasoned beef, cheese, and fresh vegetables.",
                    Ingredients = new List<string> { "Ground beef", "Taco shells", "Cheddar cheese", "Lettuce", "Tomatoes", "Taco seasoning" },
                    Instructions = "Cook beef with seasoning. Fill taco shells with beef, cheese, and chopped vegetables.",
                    //ImageUrl = "https://example.com/beef-tacos.jpg"
                },
                new Recipe
                {
                    Name = "Sushi Rolls",
                    Category = "Japanese",
                    Description = "Delicate rolls of rice, seaweed, and fresh seafood or vegetables.",
                    Ingredients = new List<string> { "Sushi rice", "Nori", "Cucumber", "Avocado", "Raw fish (optional)" },
                    Instructions = "Prepare sushi rice. Place on nori, add fillings, and roll tightly. Slice into pieces.",
                    //ImageUrl = "https://example.com/sushi-rolls.jpg"
                },
                new Recipe
                {
                    Name = "French Onion Soup",
                    Category = "French",
                    Description = "A rich soup made with caramelized onions, beef broth, and topped with cheesy bread.",
                    Ingredients = new List<string> { "Onions", "Beef broth", "Butter", "Baguette", "Gruyère cheese" },
                    Instructions = "Caramelize onions in butter. Add broth and simmer. Top with cheese bread and broil.",
                    //ImageUrl = "https://example.com/french-onion-soup.jpg"
                },
                new Recipe
                {
                    Name = "Pad Thai",
                    Category = "Thai",
                    Description = "A stir-fried rice noodle dish with a sweet-savory sauce, shrimp, and peanuts.",
                    Ingredients = new List<string> { "Rice noodles", "Shrimp", "Bean sprouts", "Peanuts", "Tamarind paste", "Eggs" },
                    Instructions = "Soak noodles. Stir-fry shrimp and eggs. Add noodles and sauce. Top with sprouts and peanuts.",
                    //ImageUrl = "https://example.com/pad-thai.jpg"
                },
                new Recipe
                {
                    Name = "Greek Salad",
                    Category = "Greek",
                    Description = "A refreshing salad with tomatoes, cucumber, olives, feta, and a lemon-olive oil dressing.",
                    Ingredients = new List<string> { "Tomatoes", "Cucumber", "Red onion", "Olives", "Feta cheese", "Olive oil", "Lemon juice" },
                    Instructions = "Chop veggies. Combine with feta and olives. Drizzle with olive oil and lemon juice.",
                    //ImageUrl = "https://example.com/greek-salad.jpg"
                },
                new Recipe
                {
                    Name = "Vegetable Stir Fry",
                    Category = "Asian",
                    Description = "Quick and healthy stir-fried vegetables in a savory soy-based sauce.",
                    Ingredients = new List<string> { "Broccoli", "Carrots", "Bell peppers", "Soy sauce", "Garlic", "Ginger" },
                    Instructions = "Sauté garlic and ginger. Add vegetables and stir-fry with soy sauce until tender.",
                    //ImageUrl = "https://example.com/vegetable-stir-fry.jpg"
                },
                new Recipe
                {
                    Name = "Pancakes",
                    Category = "Breakfast",
                    Description = "Fluffy pancakes perfect for breakfast, served with syrup or fresh fruit.",
                    Ingredients = new List<string> { "Flour", "Eggs", "Milk", "Baking powder", "Sugar", "Butter" },
                    Instructions = "Mix ingredients. Cook batter on a griddle until golden on both sides. Serve warm.",
                    //ImageUrl = "https://example.com/pancakes.jpg"
                }

            };
        }

        public void AddNewRecipe(string name, string category, string description, string ingredients, string instructions, string imageUrl = null)
        {
            var newRecipe = new Recipe
            {
                Name = name,
                Category = category,
                Description = description,
                Ingredients = ingredients.Split(',').Select(i => i.Trim()).ToList(),
                Instructions = instructions,
                ImageUrl = imageUrl
            };
            Recipes.Add(newRecipe);
        }
        public void RemoveRecipe(Recipe recipe)
        {
            if (Recipes.Contains(recipe))
            {
                Recipes.Remove(recipe);
            }
        }
        public void UpdateRecipe(string id, string name, string category, string description, string ingredients, string instructions, string imageUrl = null)
        {
            var newRecipe = new Recipe
            {
                Id = id,
                Name = name,
                Category = category,
                Description = description,
                Ingredients = ingredients.Split(',').Select(i => i.Trim()).ToList(),
                Instructions = instructions,
                ImageUrl = imageUrl
            };
            for (int i = 0; i < Recipes.Count; i++)
            {
                if (Recipes[i].Id == id)
                {
                    Recipes.RemoveAt(i);
                    Recipes.Insert(i, newRecipe);
                    break;
                }
            }
        }

    }
}
