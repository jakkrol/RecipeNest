    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    namespace RecipeNest.Models
    {
        public class Recipe
        {
            public string Id { get; set; } = Guid.NewGuid().ToString();
            public string Name { get; set; }
            public string Category { get; set; }
            public string Description { get; set; }
            public List<string> Ingredients { get; set; }
            public string Instructions { get; set; }
            public string ImageUrl { get; set; }
            public Recipe(string name, string category, string description, string ingredients, string instructions, string imageUrl = null)
            {
                Name = name;
                Category = category;
                Description = description;
                Ingredients = ingredients.Split(',').Select(i => i.Trim()).ToList();
                Instructions = instructions;
                ImageUrl = imageUrl;
            }
            public Recipe() { }
        }
    }
