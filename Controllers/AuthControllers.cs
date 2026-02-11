using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Peminjaman.Backend.Data;
using Peminjaman.Backend.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;


namespace Peminjaman.Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly IConfiguration _configuration;

        public AuthController(AppDbContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        [HttpPost("register")]
        public async Task<ActionResult<User>> Register(User request)
        {

            string passwordHash = BCrypt.Net.BCrypt.HashPassword(request.Password);

            request.Password = passwordHash;

            _context.Users.Add(request);
            await _context.SaveChangesAsync();

            return Ok("Registrasi Berhasil!");
        }

        [HttpPost("login")]
        public async Task<ActionResult<string>> Login(User request)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Username == request.Username);


            if (user == null || !BCrypt.Net.BCrypt.Verify(request.Password, user.Password))
            {
                return BadRequest("Username atau Password salah.");
            }


            string token = CreateToken(user);

            return Ok(token);
        }
        private string CreateToken(User user)
        {
            List<Claim> claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.Username),
                new Claim(ClaimTypes.Role, user.Role)
            };


            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(
                _configuration.GetSection("Jwt:Key").Value!));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.Now.AddDays(1),
                signingCredentials: creds
            );

            var jwt = new JwtSecurityTokenHandler().WriteToken(token);

            return jwt;
        }
    }
}