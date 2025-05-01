using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using TaskManagerAPI.Data;
using TaskManagerAPI.Dto;
using TaskManagerAPI.Enum;
using TaskManagerAPI.Models;
using TaskManagerAPI.Utility;

namespace TaskManagerAPI.Controllers {
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase {
        private readonly AppDbContext _context;
        private readonly IConfiguration _config;

        public AuthController(AppDbContext context, IConfiguration config) {
            _context = context;
            _config = config;
            }

        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginDto login) {
            var user = _context.Users.FirstOrDefault(u =>
                u.Username == login.Username);

            if (user == null || !Passwordhasher.VerifyPassword(login.Password, user.Password))
                return Unauthorized("Invalid credentials.");

            var claims = new[]
            {
                new Claim(ClaimTypes.Name, user.Username),
                new Claim(ClaimTypes.Role, user.Role.ToString())
            };
            var jwtKey = _config["Jwt:Key"];
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey!));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.UtcNow.AddHours(1),
                signingCredentials: creds
            );

            return Ok(new { token = new JwtSecurityTokenHandler().WriteToken(token) });
            }

        [HttpPost("signup")]
        public IActionResult SignUp([FromBody] SignUpDto signUp) {
            if (_context.Users.Any(u => u.Username == signUp.Username)) {
                return BadRequest("Username already exists.");
                }

            var user = new User {
                Username = signUp.Username,
                Password = Passwordhasher.HashPassword(signUp.Password),
                Role = Getrole.GetRole(signUp.Role)
                };

            _context.Users.Add(user);
            _context.SaveChanges();

            return Ok(new ResponseDto { Status = Resultstatus.SUCCESS, Message = "User registered successfully." });
            }


        }



    }
