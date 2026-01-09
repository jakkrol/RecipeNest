using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RecipeNest.Backend.Models;


namespace RecipeNest.Backend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RecipeController : ControllerBase
    {
        private readonly Data.AppDbContext _dbContext;

        public RecipeController(Data.AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Recipe>> GetRecipes()
        {
            var recipes = _dbContext.Recipes.ToList();
            return Ok(recipes);
        }
    }
}
