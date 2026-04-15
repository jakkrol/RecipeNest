namespace RecipeNest.Backend.Models
{
    public class ShoppingList
    {
        public Guid Id { get; set; }
        public required string Name { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        //private double _listProgress { get; set; }
        public ICollection<ShoppingItem>? Items { get; set; }
        public Guid UserId { get; set; }
        public User? User { get; set; }

    }
}
