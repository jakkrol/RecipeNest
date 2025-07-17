using SQLite;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecipeNest.Models
{
    [SQLite.Table("recipes")]
    public class Recipe : IEntity
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Category { get; set; }
        public string Description { get; set; }
        public string Ingredients { get; set; }
        public string Instructions { get; set; }
        public string ImageUrl { get; set; }
        public Recipe(string name, string category, string description, string ingredients, string instructions, string imageUrl = null)
        {
            Name = name;
            Category = category;
            Description = description;
            Ingredients = ingredients;
            Instructions = instructions;
            ImageUrl = imageUrl;
        }
        public Recipe() { }
    }
}
