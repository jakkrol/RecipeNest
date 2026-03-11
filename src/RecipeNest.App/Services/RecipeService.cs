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
        //private static RecipeService _instance;
        //public static RecipeService Instance
        //{
        //    get
        //    {
        //        if (_instance == null)
        //        {
        //            _instance = new RecipeService();
        //        }
        //        return _instance;
        //    }
        //}

        private readonly RecipeNestDb _db;
        private readonly UserSession _userSession;
        public ObservableCollection<Recipe> Recipes { get; } 

        public RecipeService(UserSession userSession)
        {
            Recipes = new ObservableCollection<Recipe>();
            _db = new RecipeNestDb();
           _userSession = userSession;
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

        public async Task CheckItem(Models.Recipe Item)
        {
            Debug.WriteLine("ITEMEK: " + Item.Name);
            //shoppingItem.IsChecked = !shoppingItem.IsChecked;

            await _db.checkItemInList(Item);
        }
    }
}
