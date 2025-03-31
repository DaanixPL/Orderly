using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Orderly.Models;
using System.Threading.Tasks;

[Route("api/[controller]")]
[ApiController]
public class AuthController : ControllerBase
{
    private readonly AppDbContext _context;

    public AuthController(AppDbContext context)
    {
        _context = context;
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] User user)
    {
        Console.WriteLine($"Otrzymano request do rejestracji: Email={user?.Email}, Password={user?.Password}");

        if (user == null)
        {
            Console.WriteLine("Błąd: Brak danych użytkownika w żądaniu!");
            return BadRequest("Invalid request.");
        }

        if (string.IsNullOrEmpty(user.Email) || string.IsNullOrEmpty(user.Password))
        {
            Console.WriteLine("Błąd: Email i hasło są wymagane.");
            return BadRequest("Email and password are required.");
        }

        var existingUser = await _context.Users.FirstOrDefaultAsync(u => u.Email == user.Email);
        if (existingUser != null)
        {
            Console.WriteLine($"Użytkownik z emailem {user.Email} już istnieje.");
            return Conflict("User with this email already exists.");
        }

        // 🔥 Generowanie losowego Username
        user.Username = "user_" + Guid.NewGuid().ToString("N").Substring(0, 8);

        _context.Users.Add(user);
        await _context.SaveChangesAsync();

        Console.WriteLine($"Zarejestrowano nowego użytkownika: {user.Email} z Username: {user.Username}");
        return Ok(user);
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] User loginUser)
    {
        Console.WriteLine($"📩 Otrzymano request do logowania: Email={loginUser?.Email}, Password={loginUser?.Password}");

        if (loginUser == null)
        {
            Console.WriteLine("❌ Błąd: Brak danych logowania w żądaniu!");
            return BadRequest("Invalid request.");
        }

        if (string.IsNullOrEmpty(loginUser.Email) || string.IsNullOrEmpty(loginUser.Password))
        {
            Console.WriteLine("❌ Błąd: Email i hasło są wymagane.");
            return BadRequest("Email and password are required.");
        }

        var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == loginUser.Email && u.Password == loginUser.Password);
        if (user == null)
        {
            Console.WriteLine($"❌ Błąd logowania: Niepoprawny email lub hasło dla {loginUser.Email}");
            return Unauthorized("Invalid email or password.");
        }

        Console.WriteLine($"✅ Zalogowano użytkownika: {user.Email}");
        return Ok(user);
    }
}
