using Microsoft.AspNetCore.Mvc;
using RecipeNest.Backend.Data;
using RecipeNest.Backend.Services;
using Microsoft.EntityFrameworkCore;
using RecipeNest.Shared.DTO;
using RecipeNest.Backend.Models;

namespace RecipeNest.Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly HashingService _hashingService;
        private readonly AppDbContext _context;

        public AuthController(HashingService hashingService, AppDbContext dbContext)
        {
            _hashingService = hashingService;
            _context = dbContext;
        }

        [HttpPost("login")]
        public async Task<ActionResult<UserDTO>> Login(LoginDTO loginuser)
        {
            try
            {
                var user = await _context.Users.FirstOrDefaultAsync(u => u.Login == loginuser.Login);
                
                if (user == null)
                {
                    return Unauthorized("Wrong login or password");
                }


                if (_hashingService.VerifyPassword(user.Password, loginuser.Password))
                {
                    UserDTO verifiedUser = new UserDTO
                    {
                        Id = user.Id,
                        Name = user.Name,
                        CreatedAt = user.CreatedAt,
                    };
                    return Ok(verifiedUser);
                }
                else
                {
                    return Unauthorized("Wrong login or password");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An internal server error occurred.");
            }
        }


        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterDTO registerDTO)
        {
            try
            {
                var userExists = await _context.Users.AnyAsync(u => u.Login == registerDTO.Login);

                if (userExists)
                {
                    return BadRequest("This login is already taken. Try different one");
                }



                var hashed = _hashingService.HashUserPassword(registerDTO.Password);
                var user = new User
                {
                    Name = registerDTO.Name,
                    Login = registerDTO.Login,
                    Password = hashed,
                    CreatedAt = DateTime.UtcNow,
                };

                _context.Users.Add(user);
                await _context.SaveChangesAsync();
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Error occured during registration");
            }
        }
    }
}

