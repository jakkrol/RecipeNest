namespace RecipeNest.Backend.Models
{
    public class User
    {
        public Guid Id { get; set; }
        public required string Name { get; set; }
        public required string Login { get; set; }
        public required string Password { get; set; }    
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public ICollection<Recipe>? Recipes { get; set; }

    }
}
