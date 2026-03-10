using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecipeNest.Shared.DTO
{
    public class UserDTO
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public DateTime CreatedAt { get; set; }
        public ICollection<RecipeSummaryDTO>? Recipes { get; set; }
    }
}
