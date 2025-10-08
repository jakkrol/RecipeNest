namespace RecipeNest.Backend.Models
{
    public class ShoppingItem
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public double Quantity { get; set; }
        public string? Unit { get; set; }
        public bool IsChecked { get; set; }
        public int ShoppingListId { get; set; }
        public ShoppingList? ShoppingList { get; set; }
    }
}
