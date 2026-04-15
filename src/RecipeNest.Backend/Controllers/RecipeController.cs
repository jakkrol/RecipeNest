using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RecipeNest.Backend.Models;
using System.Diagnostics;


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
        public async Task<ActionResult<IEnumerable<Recipe>>> GetRecipes()
        {
            var recipes = await _dbContext.Recipes.ToListAsync();
            return Ok(recipes);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Recipe>> GetRecipe(Guid id)
        {
            var recipe = await _dbContext.Recipes.FindAsync(id);
            if (recipe == null) 
            {
                return NotFound();
            }
            return Ok(recipe);
        }

        [HttpPost]
        public async Task<ActionResult<Recipe>> PostRecipe(Recipe recipe)
        {

            _dbContext.Recipes.Add(recipe);
            await _dbContext.SaveChangesAsync();
            //await _dbContext.Recipes.AddAsync(recipe);
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRecipe(Guid id)
        {
            var recipe = await _dbContext.Recipes.FindAsync(id);
            if (recipe == null)
            {
                return NotFound();
            }

            _dbContext.Recipes.Remove(recipe);
            await _dbContext.SaveChangesAsync();
            return Ok();
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Recipe>> PutRecipe(Guid id, Recipe recipe)
        {
            if(id != recipe.Id)
            {
                return BadRequest();
            }

            _dbContext.Entry(recipe).State = EntityState.Modified;
            try
            {
                await _dbContext.SaveChangesAsync();
            }catch(Exception ex)
            {
                if (!_dbContext.Recipes.Any(e => e.Id == id))
                {
                    
                    return NotFound();
                }
                else
                {
                    Debug.WriteLine($"Error updating recipe with id {id}: {ex.Message}");
                    throw;
                }
            }

            return Ok();
        }

    }
}
