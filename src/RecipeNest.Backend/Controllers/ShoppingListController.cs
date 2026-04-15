using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RecipeNest.Backend.Models;

namespace RecipeNest.Backend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ShoppingListController : Controller
    {
        private readonly Data.AppDbContext _dbContext;

        public ShoppingListController(Data.AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ShoppingList>>> GetShoppingLists()
        {
            var result = await _dbContext.ShoppingList.ToListAsync();
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ShoppingList>> GetShoppingList(Guid id)
        {
            var result = await _dbContext.ShoppingList.FindAsync(id);
            if (result == null)
            {
                return NotFound();
            }

            return Ok(result);
            //public IActionResult Index()
            //{
            //    return View();
            //}
        }

        [HttpPost]
        public async Task<ActionResult<ShoppingList>> PostShoppingList(ShoppingList sl)
        {
            _dbContext.ShoppingList.Add(sl);
            await _dbContext.SaveChangesAsync();

            return Ok(sl);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<ShoppingList>> PutShoppingList(Guid id, ShoppingList sl)
        {
            if (id != sl.Id)
            {
                return BadRequest();
            }

            _dbContext.Entry(sl).State = EntityState.Modified;

            try
            {
                await _dbContext.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                if(!_dbContext.ShoppingList.Any(x => x.Id == id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteShoppingList(Guid id)
        {
            var sl = await _dbContext.ShoppingList.FindAsync(id);
            if(sl == null)
            {
                return NotFound();
            }

            _dbContext.ShoppingList.Remove(sl);
            await _dbContext.SaveChangesAsync();
            return Ok();
        }
    }
}
