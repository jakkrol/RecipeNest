namespace RecipeNest.Backend.Models
{
    public class ShoppingItem
    {
        public Guid Id { get; set; }
        public required string Name { get; set; }
        public double Quantity { get; set; }
        public string? Unit { get; set; }
        public bool IsChecked { get; set; }
        public Guid ShoppingListId { get; set; }
        public ShoppingList? ShoppingList { get; set; }
    }
}
