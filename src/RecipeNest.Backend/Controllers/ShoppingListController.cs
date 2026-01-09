using Microsoft.AspNetCore.Mvc;
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
        public ActionResult<IEnumerable<ShoppingList>> GetShoppingList()
        {
            var result = _dbContext.ShoppingList.ToList();
            return Ok(result);
        }

        //public IActionResult Index()
        //{
        //    return View();
        //}
    }
}
