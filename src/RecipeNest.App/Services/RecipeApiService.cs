using RecipeNest.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace RecipeNest.Services
{
    public class RecipeApiService
    {
        private readonly HttpClient _httpClient;

        public RecipeApiService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<Models.Recipe> SearchRecipeOfDay()
        {
            string url = "random.php";
            var response = await _httpClient.GetStringAsync(url);

            using var doc = JsonDocument.Parse(response);
            var mealJson = doc.RootElement.GetProperty("meals")[0];

            var meal = new Models.Recipe
            {
                Id = int.Parse(mealJson.GetProperty("idMeal").GetString()),
                Name = mealJson.GetProperty("strMeal").GetString(),
                ImageUrl = mealJson.GetProperty("strMealThumb").GetString()
            };
            return meal;
        }


        public async Task<List<Models.Recipe>> SearchRecipesAsync(string searchMode, string query)
        {
            Debug.WriteLine("Im in SearchRecipeAsync right now!!!");
            if (string.IsNullOrWhiteSpace(query))
            {
                Debug.WriteLine("Query null");
                return new List<Models.Recipe>();
            }

            string endpoint = (searchMode ?? "name").ToLower() switch
            {
                "name" => $"search.php?s={query}",
                "ingredient" => $"filter.php?i={query}",
                "category" => $"filter.php?c={query}",
                "country" => $"filter.php?a={query}",
                _ => $"search.php?s={query}"
            };

            try
            {
                Debug.WriteLine("JESTEM W TRY!!!:   " +  endpoint);
                var cts = new CancellationTokenSource(TimeSpan.FromSeconds(10));
                var httpreq = await _httpClient.GetAsync(endpoint, cts.Token);
                httpreq.EnsureSuccessStatusCode();
                var response = await httpreq.Content.ReadAsStringAsync();
                using var doc = JsonDocument.Parse(response);
                Debug.WriteLine("POD1");
                if (!doc.RootElement.TryGetProperty("meals", out var mealsJson) || mealsJson.ValueKind == JsonValueKind.Null)
                {
                    throw new Exception($"No meals returned from API for query '{query}'");
                    //Debug.WriteLine("COS SIE ZJEBALO");
                    //return new List<Models.Recipe>();
                }
                Debug.WriteLine("POD2");
                var recipes = new List<Models.Recipe>();

                    foreach (var mealJson in mealsJson.EnumerateArray())
                    {
                        recipes.Add(new Models.Recipe
                        {
                            Id = int.Parse(mealJson.GetProperty("idMeal").GetString()),
                            Name = mealJson.GetProperty("strMeal").GetString(),
                            ImageUrl = mealJson.GetProperty("strMealThumb").GetString()
                        });
                    }
                Debug.WriteLine("TESTTSTSTTS");
                    Debug.WriteLine($"Recipes: {recipes}");

                return recipes;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
                return new List<Models.Recipe>();
            }
        }

        public async Task<Recipe> getRecipeById(int id)
        {
            string url = $"lookup.php?i={id}";
            var response = await _httpClient.GetStringAsync(url);

            using var doc = JsonDocument.Parse(response);
            var mealJson = doc.RootElement.GetProperty("meals")[0];

            var meal = new Models.Recipe
            {
                Id = int.Parse(mealJson.GetProperty("idMeal").GetString()),
                Name = mealJson.GetProperty("strMeal").GetString(),
                Category = mealJson.GetProperty("strCategory").GetString(),
                Description = $"{mealJson.GetProperty("strMeal").GetString()} - {mealJson.GetProperty("strCategory").GetString()}",
                Instructions = mealJson.GetProperty("strInstructions").GetString(),
                ImageUrl = mealJson.GetProperty("strMealThumb").GetString(),
                Ingredients = ExtractIngredients(mealJson)
            };
            Debug.WriteLine(mealJson);
            return meal;
        }

        public async Task<List<string>> getAllRegions()
        {
            string url = "list.php?a=list";
            var response = await _httpClient.GetStringAsync(url);

            var doc = JsonDocument.Parse(response);
            var JsonDoc = doc.RootElement.GetProperty("meals");
            var regions = new List<string>();
            foreach(var region in JsonDoc.EnumerateArray())
            {
                regions.Add(region.GetProperty("strArea").GetString());
            }
            //Debug.WriteLine(response);
            return regions;
        }
        public async Task<List<string>> getAllCategories()
        {
            string url = "list.php?c=list";
            var response = await _httpClient.GetStringAsync(url);

            var doc = JsonDocument.Parse(response);
            var JsonDoc = doc.RootElement.GetProperty("meals");
            var categories = new List<string>();
            foreach (var category in JsonDoc.EnumerateArray())
            {
                categories.Add(category.GetProperty("strCategory").GetString());
            }
            //Debug.WriteLine(response);
            return categories;
        }
        public async Task<List<string>> getAllIngredients()
        {
            string url = "list.php?i=list";
            var response = await _httpClient.GetStringAsync(url);

            var doc = JsonDocument.Parse(response);
            var JsonDoc = doc.RootElement.GetProperty("meals");
            var ingredients = new List<string>();
            foreach (var ingredient in JsonDoc.EnumerateArray())
            {
                ingredients.Add(ingredient.GetProperty("strIngredient").GetString());
            }
            //foreach (var ingredient1 in ingredients)
            //{
            //    Debug.WriteLine(ingredient1); 
            //}
            return ingredients;
        }


        //public async Task<Recipe> getRecipeById(int id)
        //{
        //    string url = $"https://www.themealdb.com/api/json/v1/1/lookup.php?i={id}";
        //    var response = await _httpClient.GetStringAsync(url);

        //    using var doc = JsonDocument.Parse(response);
        //    var mealJson = doc.RootElement.GetProperty("meals")[0];

        //    var meal = new Models.Recipe
        //    {
        //        Id = int.Parse(mealJson.GetProperty("idMeal").GetString()),
        //        Name = mealJson.GetProperty("strMeal").GetString(),
        //        Category = mealJson.GetProperty("strCategory").GetString(),
        //        Description = $"{mealJson.GetProperty("strMeal").GetString()} - {mealJson.GetProperty("strCategory").GetString()}",
        //        Instructions = mealJson.GetProperty("strInstructions").GetString(),
        //        ImageUrl = mealJson.GetProperty("strMealThumb").GetString(),
        //        Ingredients = ExtractIngredients(mealJson)
        //    };
        //    Debug.WriteLine(mealJson);
        //    return meal;
        //}


        private string ExtractIngredients(JsonElement mealJson)
        {
            var ingredients = new List<string>();

            for (int i = 1; i <= 20; i++)
            {
                var ingredientProp = $"strIngredient{i}";
                var measureProp = $"strMeasure{i}";

                if (mealJson.TryGetProperty(ingredientProp, out var ingredientElem) &&
                    mealJson.TryGetProperty(measureProp, out var measureElem))
                {
                    var ingredient = ingredientElem.GetString()?.Trim();
                    var measure = measureElem.GetString()?.Trim();

                    if (!string.IsNullOrWhiteSpace(ingredient))
                        ingredients.Add($"{measure} {ingredient}".Trim());
                }
            }

            return string.Join(", ", ingredients);
        }


    }
}
