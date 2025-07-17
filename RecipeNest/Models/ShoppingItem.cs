using SQLite;
using SQLiteNetExtensions.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecipeNest.Models
{
    [SQLite.Table("shopping_item")]
    public class ShoppingItem : IEntity
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        public string Name { get; set; }

        public double Quantity { get; set; }

        public string Unit { get; set; }

        public bool IsChecked { get; set; }

        [ForeignKey(typeof(ShoppingList))]
        public int ShoppingListId { get; set; }

        [ManyToOne]
        public ShoppingList ShoppingList { get; set; }
    }
}
