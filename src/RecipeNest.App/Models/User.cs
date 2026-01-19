using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecipeNest.Models
{
    internal class User
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public required string Login { get; set; }
        public required string Password { get; set; }
        public DateTime CreatedAt { get; set; } 

        public ICollection<Recipe>? Recipes { get; set; }
    }
}
