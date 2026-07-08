using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecipeNest.Models
{
    public class ApiRecipe
    {
        public int Id { get; set; }            
        public string Name { get; set; }
        public string Category { get; set; }
        public string Description { get; set; }
        public string Instructions { get; set; }
        public string ImageUrl { get; set; }
        public string Ingredients { get; set; }
    }
}
