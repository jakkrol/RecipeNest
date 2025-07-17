using SQLite;
using SQLiteNetExtensions.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecipeNest.Models
{
    [SQLite.Table("shopping_list")]
    public class ShoppingList : IEntity
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        public string Name { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        [OneToMany(CascadeOperations = CascadeOperation.All)]
        public List<ShoppingItem> Items { get; set; } = new();
    }
}
