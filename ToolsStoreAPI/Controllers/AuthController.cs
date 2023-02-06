using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ToolsStoreAPI.Data;
using ToolsStoreAPI.Models;
using ToolsStoreAPI.Services;

namespace ToolsStoreAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class AuthController : ControllerBase
    {
        private readonly DataContext _context;
        private readonly AuthService _authService;

        public AuthController(IConfiguration configuration, DataContext context)
        {
            _authService = new AuthService(configuration);
            _context = context;
        }

        /// <summary>
        /// Register as a new user
        /// </summary>
        [HttpPost("register")]
        public async Task<ActionResult<string>> Register(UserDto request)
        {
            request.Username = request.Username.ToLower();

            if (await _context.Users.AnyAsync(user => user.Username == request.Username))
            {
                return BadRequest("Username already exist.");
            }

            _authService.CreatePasswordHash(request.Password, out byte[] passwordHash, out byte[] passwordSalt);

            User user = new User(request.Username, passwordHash, passwordSalt);
            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            string token = _authService.CreateToken(user);
            return Ok("Bearer " + token);
        }

        /// <summary>
        /// Login as an exist user
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// 
        ///     {        
        ///       "username": "maorc",
        ///       "password": "Aa123456!"
        ///     }
        /// </remarks>
        [HttpPost("login")]
        public async Task<ActionResult<string>> Login(UserDto request)
        {
            request.Username = request.Username.ToLower();

            User user = await _context.Users.Where(u => u.Username == request.Username).FirstOrDefaultAsync(); ;

            if (user == null || !_authService.VerifyPasswordHash(request.Password, user.PasswordHash, user.PasswordSalt))
            {
                return BadRequest("Wrong username or password.");
            }

            user.LastLogIn= DateTime.Now;

            string token = _authService.CreateToken(user);
            return Ok("Bearer " + token);
        }
    }
}
