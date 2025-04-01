using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Orderly.Models;
using Microsoft.AspNetCore.Identity;
using System.Text.RegularExpressions;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.Extensions.Options;

[Route("api/[controller]")]
[ApiController]
public class AuthController : ControllerBase
{
    private readonly AppDbContext _context;
    private readonly PasswordHasher<User> _passwordHasher = new PasswordHasher<User>();
    private readonly JwtSettings _jwtSettings;

    public AuthController(AppDbContext context, IOptions<JwtSettings> jwtSettings)
    {
        _context = context;
        _jwtSettings = jwtSettings.Value;
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] User user)
    {
        if (user == null)
        {
            return BadRequest("Invalid request.");
        }

        if (string.IsNullOrWhiteSpace(user.Email))
        {
            return BadRequest("Email is required.");
        }

        if (string.IsNullOrWhiteSpace(user.Username))
        {
            return BadRequest("Username is required.");
        }

        if (string.IsNullOrWhiteSpace(user.Password))
        {
            return BadRequest("Password is required.");
        }

        var emailRegex = new Regex(@"^\S+@\S+\.\S+$");
        if (!emailRegex.IsMatch(user.Email))
        {
            return BadRequest("The Email field is not a valid e-mail address.");
        }

        var existingUser = await _context.Users.FirstOrDefaultAsync(u => u.Email == user.Email || u.Username == user.Username);
        if (existingUser != null)
        {
            return Conflict("User with this email or username already exists.");
        }

        user.PasswordHash = _passwordHasher.HashPassword(user, user.Password);
        user.Password = null;

        _context.Users.Add(user);
        await _context.SaveChangesAsync();

        return Ok(user);
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] UserLoginRequest loginUser)
    {
        if (loginUser == null)
        {
            return BadRequest("Invalid request.");
        }

        if (string.IsNullOrWhiteSpace(loginUser.EmailOrUsername))
        {
            return BadRequest("Either email or username is required.");
        }

        if (string.IsNullOrWhiteSpace(loginUser.Password))
        {
            return BadRequest("Password is required.");
        }

        var user = loginUser.EmailOrUsername.Contains('@')
              ? await _context.Users.FirstOrDefaultAsync(u => u.Email == loginUser.EmailOrUsername)
              : await _context.Users.FirstOrDefaultAsync(u => u.Username == loginUser.EmailOrUsername);

        if (user == null)
        {
            return Unauthorized("Invalid email or username.");
        }

        var passwordVerificationResult = _passwordHasher.VerifyHashedPassword(user, user.PasswordHash, loginUser.Password);
        if (passwordVerificationResult == PasswordVerificationResult.Failed)
        {
            return Unauthorized("Invalid password.");
        }

        var token = GenerateJwtToken(user);
        return Ok(new { Token = token });
    }

    private string GenerateJwtToken(User user)
    {
        var claims = new[]
        {
            new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
            new Claim(JwtRegisteredClaimNames.Email, user.Email),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
        };

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.SecretKey));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            issuer: _jwtSettings.Issuer,
            audience: _jwtSettings.Audience,
            claims: claims,
            expires: DateTime.Now.AddMinutes(_jwtSettings.ExpirationMinutes),
            signingCredentials: creds);

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}
