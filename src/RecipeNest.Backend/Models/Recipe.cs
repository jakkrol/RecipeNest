namespace RecipeNest.Backend.Models
{
    public class Recipe
    {
        public Guid Id { get; set; }


        public required string Name { get; set; }
        public string? Category { get; set; }
        public string? Description { get; set; }
        public string? Ingredients { get; set; }
        public string? Instructions { get; set; }
        public bool IsFavourite { get; set; } = false;
        public string? ImageUrl { get; set; }

  
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

  
        public Guid UserId { get; set; }
        public User? User { get; set; }
    }
}
