using Microsoft.AspNetCore.Mvc;
using RecipeNest.Backend.Models;

namespace RecipeNest.Backend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ShoppingItemController : Controller
    {
        private readonly Data.AppDbContext _dbContext;
        //public IActionResult Index()
        //{
        //    return View();
        //}

        public ShoppingItemController(Data.AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet]
        public ActionResult<IEnumerable<ShoppingItem>> GetItem()
        {
            var item = _dbContext.Recipes.ToList();
            return Ok(item);
        }
        //public IActionResult Get() { 
        //}

    }
}
