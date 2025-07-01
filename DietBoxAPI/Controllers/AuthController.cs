using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly IConfiguration _config;

    public AuthController(IConfiguration config)
    {
        _config = config;
    }

    [HttpPost("login")]
    public IActionResult Login([FromBody] LoginRequest login)
    {
        // Simula autenticação (substitua por validação real)
        if (login.Username == "admin" && login.Password == "admin123")
        {
            var token = GenerateToken("admin", "Admin");
            return Ok(new { token });
        }

        if (login.Username == "nutri" && login.Password == "nutri123")
        {
            var token = GenerateToken("nutri", "Nutritionist");
            return Ok(new { token });
        }

        return Unauthorized();
    }

    private string GenerateToken(string username, string role)
    {
        var jwtSettings = _config.GetSection("JwtSettings");

        // Garantir que a chave secreta não seja nula
        var secretKey = jwtSettings["SecretKey"];
        if (string.IsNullOrEmpty(secretKey))
        {
            throw new InvalidOperationException("A chave secreta não está configurada.");
        }

        var expiresInMinutesValue = jwtSettings["ExpiresInMinutes"];
        if (string.IsNullOrEmpty(expiresInMinutesValue) || !int.TryParse(expiresInMinutesValue, out var expiresInMinutes))
        {
            throw new InvalidOperationException("A configuração 'ExpiresInMinutes' não está configurada corretamente.");
        }

        var claims = new[]
        {
            new Claim(ClaimTypes.Name, username),
            new Claim(ClaimTypes.Role, role)
        };

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            issuer: jwtSettings["Issuer"],
            audience: jwtSettings["Audience"],
            claims: claims,
            expires: DateTime.Now.AddMinutes(expiresInMinutes),
            signingCredentials: creds);

        return new JwtSecurityTokenHandler().WriteToken(token);
    }

    public class LoginRequest
    {
        public required string Username { get; set; }
        public required string Password { get; set; }
    }
}
