using AttendanceManagement.Configuration;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace AttendanceManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly JwtConfig _jwtConfig;

        public AuthenticationController(UserManager<IdentityUser> userManager, IOptions<JwtConfig> jwtConfig)
        {
            _userManager = userManager;
            _jwtConfig = jwtConfig.Value;
        }

        [HttpPost("Register")]
        public async Task<IActionResult> Register([FromBody] UserRegistrationRequestDto request)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var existingUser = await _userManager.FindByEmailAsync(request.Email);
            if (existingUser != null)
                return BadRequest("Email is already in use");

            var newUser = new IdentityUser
            {
                Email = request.Email,
                UserName = request.Email
            };

            var result = await _userManager.CreateAsync(newUser, request.Password);
            if (!result.Succeeded)
                return BadRequest(result.Errors.Select(e => e.Description));

            var token = GenerateJwtToken(newUser);
            await _userManager.AddToRoleAsync(newUser, request.Role);
            return Ok(new { Token = token });
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] UserLoginRequestDto request)
        {
            var user = await _userManager.FindByEmailAsync(request.Email);
            if (user == null) return Unauthorized("Invalid credentials");

            var passwordValid = await _userManager.CheckPasswordAsync(user, request.Password);
            if (!passwordValid) return Unauthorized("Invalid credentials");

            var token = GenerateJwtToken(user);

            return Ok(new { Token = token });
        }

        private async Task<string> GenerateJwtToken(IdentityUser user)
        {
            var key = Encoding.UTF8.GetBytes(_jwtConfig.Secret);
            var roles = await _userManager.GetRolesAsync(user);
            var claims = new List<Claim>
            {
                new Claim("Id", user.Id),
                new Claim(JwtRegisteredClaimNames.Sub, user.Email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };
            claims.AddRange(roles.Select(role => new Claim(ClaimTypes.Role, role)));
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddHours(_jwtConfig.ExpiryInHours),
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(key),
                    SecurityAlgorithms.HmacSha256Signature)
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }
    }

    // DTOs
    public class UserRegistrationRequestDto
    {
        public string Email { get; set; } = null!;
        public string Password { get; set; } = null!;
        public string Role { get; set; } = null!;
    }

    public class UserLoginRequestDto
    {
        public string Email { get; set; } = null!;
        public string Password { get; set; } = null!;
    }
}
