using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecipeNest.Models
{
    public class ShoppingItem
    {
        public int Id { get; set; }
        public int ShoppingListId { get; set; }
        public string Name { get; set; }
        public bool isChecked { get; set; }
    }
}
