using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RecipeNest.Backend.Models;

namespace RecipeNest.Backend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly Data.AppDbContext _context;

        public UserController(Data.AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> GetUsers()
        {
            var users = await _context.Users.ToListAsync();
            return Ok(users);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<User>> GetUser(int id)
        {
            var user = await _context.Users.FindAsync(id);
            if(user == null)
            {
                return NotFound();
            }

            return Ok(user);
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> PutUser(long id, User user)
        {
            if(id != user.Id)
            {
                return BadRequest();
            }

            _context.Entry(user).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException) 
            {
                if (!_context.Users.Any(e => e.Id == id))
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


        [HttpPost]
        public async Task<ActionResult<User>> PostUser(User user)
        {

            //user = new User
            //{
            //    Login = "a",
            //    Password = "b",
            //    Name = "c",
            //};

            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return Ok();
            //return CreatedAtAction(nameof(User), user);
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(long id)
        {
            var user = await _context.Users.FindAsync(id);
            if(user == null)
            {
                return NotFound();
            }

            _context.Users.Remove(user);
            await _context.SaveChangesAsync();
            return Ok();
        }



        // to complete this - login
        [HttpPost("login")]
        public async Task<ActionResult<User>> Login(User loginuser)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Login == loginuser.Login && u.Password == loginuser.Password);

            if(user != null)
                return Ok();
            else
                return Unauthorized();
        }


    }
}
