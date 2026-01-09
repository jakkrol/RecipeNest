using Microsoft.EntityFrameworkCore;
using RecipeNest.Backend.Models;

namespace RecipeNest.Backend.Data
{
    public class AppDbContext : DbContext
    {
        protected readonly IConfiguration _configuration;

        public AppDbContext(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql(_configuration.GetConnectionString("WebApiDatabase"));
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Recipe> Recipes { get; set; }
        public DbSet<ShoppingItem> ShoppingItems { get; set; }
        public DbSet<ShoppingList> ShoppingList { get; set; }
    }
}
