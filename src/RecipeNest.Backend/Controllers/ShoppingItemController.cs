using Microsoft.AspNetCore.Mvc;
using RecipeNest.Backend.Models;
using Microsoft.EntityFrameworkCore;

namespace RecipeNest.Backend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ShoppingItemController : Controller
    {
        private readonly Data.AppDbContext _dbContext;

        public ShoppingItemController(Data.AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ShoppingItem>>> GetItems()
        {
            var itemList = await _dbContext.ShoppingItems.ToListAsync();
            return Ok(itemList);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ShoppingItem>> GetItem(int id)
        {
            var item = await _dbContext.ShoppingItems.FindAsync(id);
            if (item == null)
            {
                return NotFound();
            }
            return Ok(item);
        }

        [HttpPost]
        public async Task<ActionResult<ShoppingItem>> PostItem(ShoppingItem item)
        {
            _dbContext.ShoppingItems.Add(item);
            await _dbContext.SaveChangesAsync();
            return CreatedAtAction(nameof(GetItem), new { id = item.Id }, item);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutItem(int id, ShoppingItem item)
        {
            if (id != item.Id)
            {
                return BadRequest();
            }
            _dbContext.Entry(item).State = EntityState.Modified;
            try
            {
                await _dbContext.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_dbContext.ShoppingItems.Any(e => e.Id == id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteItem(int id)
        {
            var item = await _dbContext.ShoppingItems.FindAsync(id);
            if (item == null)
            {
                return NotFound();
            }
            _dbContext.ShoppingItems.Remove(item);
            await _dbContext.SaveChangesAsync();
            return NoContent();
        }


    }
}
