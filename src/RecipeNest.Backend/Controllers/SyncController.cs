using Microsoft.AspNetCore.Mvc;
using RecipeNest.Backend.Models;
using Microsoft.EntityFrameworkCore;

namespace RecipeNest.Backend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SyncController : Controller
    {
        private readonly Data.AppDbContext _dbContext;
        public SyncController(Data.AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }


        // ---------> Tutaj to trzeba dokończyć i ogarnąć, żeby się synchronizowało tak jak user chce.
        // ---------> Na cliencie nic jeszcze do tego nie ma, w SyncDbData, trzeba napisać metody i połączyć z View profilu 

        // prob I should make universal recipe class
        [HttpPost("/syncLocal")]
        public async Task<ActionResult> SyncLocal(List<Recipe> recipes, Guid userId)
        {
            var existing = _dbContext.Recipes.Where(r => r.UserId == userId);
            _dbContext.Recipes.RemoveRange(existing);

            await _dbContext.Recipes.AddRangeAsync(recipes);
            await _dbContext.SaveChangesAsync();

            return Ok(new {message = "Synchronizacja ---> TEST"});
        }

        
        [HttpPost("/syncGlobal")]
        public async Task<ActionResult> SyncGlobal(Guid userId)
        {
            List<Recipe> r = new List<Recipe>();

            return Ok(new {message = "Global sync completed" ,data = r});
        }
    }
}
