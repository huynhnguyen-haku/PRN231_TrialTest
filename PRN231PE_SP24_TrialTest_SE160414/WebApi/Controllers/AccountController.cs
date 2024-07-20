using BusinessObjects.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Services;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using WebApi.DTO;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAccountService _accountService;

        public AccountController(IAccountService accountService)
        {
            _accountService = accountService;
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginDTO request)
        {
            try
            {
                var user = _accountService.Login(request.Email, request.Password);
                if (user == null)
                {
                    return NotFound("Invalid username or password");
                }
                var token = CreateToken(user);
                return Ok(new { token });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        public static string CreateToken(UserAccount user)
        {
            List<Claim> claims = new List<Claim>();
            claims.Add(new Claim("UserID", user.UserAccountId.ToString()));
            claims.Add(new Claim(ClaimTypes.Role, user.Role.ToString()));
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(
                new ConfigurationBuilder().AddJsonFile("appsettings.json").Build().GetSection("Jwt")["Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.Now.AddMinutes(30),
                signingCredentials: creds
                );
            var jwt = new JwtSecurityTokenHandler().WriteToken(token);
            return jwt;
        }
    }
}
