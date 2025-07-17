using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using RecipeNest.DbConfig;
using RecipeNest.Models;

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

        private readonly RecipeNestDb _db;
        public ObservableCollection<Recipe> Recipes { get; } 

        private RecipeService()
        {
            Recipes = new ObservableCollection<Recipe>();
            _db = new RecipeNestDb();
            //Recipes = new ObservableCollection<Recipe>
            //{
            //    new Recipe
            //    {
            //        Name = "Spaghetti Carbonara",
            //        Category = "Italian",
            //        Description = "A classic Italian pasta dish made with eggs, cheese, pancetta, and pepper.",
            //        Ingredients = "Spaghetti, Eggs, Parmesan cheese, Pancetta, Black pepper",
            //        Instructions = "Cook spaghetti. In a bowl, mix eggs and cheese. Fry pancetta. Combine all with pepper."
            //    },
            //    new Recipe
            //    {
            //        Name = "Chicken Curry",
            //        Category = "Indian",
            //        Description = "A spicy and flavorful chicken dish cooked in a rich curry sauce.",
            //        Ingredients = "Chicken, Curry powder, Coconut milk, Onion, Garlic",
            //        Instructions = "Sauté onion and garlic. Add chicken and curry powder. Pour in coconut milk and simmer."
            //    },
            //    new Recipe
            //    {
            //        Name = "Beef Tacos",
            //        Category = "Mexican",
            //        Description = "Crunchy taco shells filled with seasoned beef, cheese, and fresh vegetables.",
            //        Ingredients = "Ground beef, Taco shells, Cheddar cheese, Lettuce, Tomatoes, Taco seasoning",
            //        Instructions = "Cook beef with seasoning. Fill taco shells with beef, cheese, and chopped vegetables."
            //    },
            //    new Recipe
            //    {
            //        Name = "Sushi Rolls",
            //        Category = "Japanese",
            //        Description = "Delicate rolls of rice, seaweed, and fresh seafood or vegetables.",
            //        Ingredients = "Sushi rice, Nori, Cucumber, Avocado, Raw fish (optional)",
            //        Instructions = "Prepare sushi rice. Place on nori, add fillings, and roll tightly. Slice into pieces."
            //    },
            //    new Recipe
            //    {
            //        Name = "French Onion Soup",
            //        Category = "French",
            //        Description = "A rich soup made with caramelized onions, beef broth, and topped with cheesy bread.",
            //        Ingredients = "Onions, Beef broth, Butter, Baguette, Gruyère cheese",
            //        Instructions = "Caramelize onions in butter. Add broth and simmer. Top with cheese bread and broil."
            //    },
            //    new Recipe
            //    {
            //        Name = "Pad Thai",
            //        Category = "Thai",
            //        Description = "A stir-fried rice noodle dish with a sweet-savory sauce, shrimp, and peanuts.",
            //        Ingredients = "Rice noodles, Shrimp, Bean sprouts, Peanuts, Tamarind paste, Eggs",
            //        Instructions = "Soak noodles. Stir-fry shrimp and eggs. Add noodles and sauce. Top with sprouts and peanuts."
            //    },
            //    new Recipe
            //    {
            //        Name = "Greek Salad",
            //        Category = "Greek",
            //        Description = "A refreshing salad with tomatoes, cucumber, olives, feta, and a lemon-olive oil dressing.",
            //        Ingredients = "Tomatoes, Cucumber, Red onion, Olives, Feta cheese, Olive oil, Lemon juice",
            //        Instructions = "Chop veggies. Combine with feta and olives. Drizzle with olive oil and lemon juice."
            //    },
            //    new Recipe
            //    {
            //        Name = "Vegetable Stir Fry",
            //        Category = "Asian",
            //        Description = "Quick and healthy stir-fried vegetables in a savory soy-based sauce.",
            //        Ingredients = "Broccoli, Carrots, Bell peppers, Soy sauce, Garlic, Ginger",
            //        Instructions = "Sauté garlic and ginger. Add vegetables and stir-fry with soy sauce until tender."
            //    },
            //    new Recipe
            //    {
            //        Name = "Pancakes",
            //        Category = "Breakfast",
            //        Description = "Fluffy pancakes perfect for breakfast, served with syrup or fresh fruit.",
            //        Ingredients = "Flour, Eggs, Milk, Baking powder, Sugar, Butter",
            //        Instructions = "Mix ingredients. Cook batter on a griddle until golden on both sides. Serve warm."
            //    }
            //};
        }

        public async Task LoadRecipesFromDb()
        {
            var recipesFromDb = await _db.GetItemsAsync<Recipe>();
            Recipes.Clear();
            foreach (var item in recipesFromDb)
            {
                Recipes.Add(item);
            }
        }

        public async Task AddNewRecipe(string name, string category, string description, string ingredients, string instructions, string imageUrl = null)
        {
            var newRecipe = new Recipe
            {
                Name = name,
                Category = category,
                Description = description,
                Ingredients = ingredients,
                Instructions = instructions,
                ImageUrl = imageUrl
            };
            await _db.SaveItemAsync<Recipe>(newRecipe);
            await LoadRecipesFromDb();
        }

        public async Task RemoveRecipe(Recipe recipe)
        {
            await _db.DeleteItemAsync<Recipe>(recipe);
            await LoadRecipesFromDb();
        }

        public async Task UpdateRecipe(int id, string name, string category, string description, string ingredients, string instructions, string imageUrl = null)
        {
            var updatedRecipe = new Recipe
            {
                Id = id,
                Name = name,
                Category = category,
                Description = description,
                Ingredients = ingredients,
                Instructions = instructions,
                ImageUrl = imageUrl
            };

            await _db.SaveItemAsync<Recipe>(updatedRecipe);
            await LoadRecipesFromDb();
        }
    }
}
