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
        private readonly HttpClient _httpClient = new();

        public async Task<Models.Recipe> SearchRecipeOfDay()
        {
            string url = "https://www.themealdb.com/api/json/v1/1/random.php";
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
