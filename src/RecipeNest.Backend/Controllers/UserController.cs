using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RecipeNest.Backend.Models;
using System.Net.WebSockets;
using RecipeNest.Shared.DTO;

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
        public async Task<ActionResult<IEnumerable<UserDTO>>> GetUsers()
        {
            //var users = await _context.Users.ToListAsync();
            var users = await _context.Users.Select(u => new UserDTO
            {
                Id = u.Id,
                Name = u.Name,
                CreatedAt = u.CreatedAt
            }).ToListAsync();

            return Ok(users);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<UserDTO>> GetUser(Guid id)
        {
            //var user = await _context.Users.FindAsync(id);
            var user = await _context.Users.Where(u => u.Id == id).Select(u => new UserDTO
            {
                Id = u.Id,
                Name = u.Name,
                CreatedAt = u.CreatedAt
            }).FirstOrDefaultAsync();

            if (user == null)
            {
                return NotFound();
            }

            return Ok(user);
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> PutUser(Guid id, UpdateUserDTO user)
        {
            //if(id != user.Id)
            //{
            //    return BadRequest();
            //}
            

            //_context.Entry(user).State = EntityState.Modified;

            var userToUpdate = await _context.Users.FindAsync(id);
            if (userToUpdate == null)
            {
                return NotFound();
            }

            //Updating user properties
            userToUpdate.Name = user.Name;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException) 
            {
                return StatusCode(500, "An error occurred while updating the user.");
            }

            return Ok();
        }


        [HttpPost]
        public async Task<ActionResult<User>> PostUser(RegisterDTO registerDTO)
        {

            //user = new User
            //{
            //    Login = "a",
            //    Password = "b",
            //    Name = "c",
            //};
            User user = new User
            {
                Login = registerDTO.Login,
                Password = registerDTO.Password,
                Name = registerDTO.Name,
                CreatedAt = DateTime.UtcNow
            };

            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return Ok();
            //return CreatedAtAction(nameof(User), user);
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(Guid id)
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
        public async Task<ActionResult<LoginDTO>> Login(LoginDTO loginuser)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Login == loginuser.Login && u.Password == loginuser.Password);

            if(user != null)
                return Ok();
            else
                return Unauthorized();
        }


    }
}
