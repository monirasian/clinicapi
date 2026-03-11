using clinicapi.Data;
using clinicapi.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace clinicapi.Controllers;

[ApiController]
[Route("api/auth")]
public sealed class AuthController : ControllerBase
{
    private readonly ClinicDbContext _db;
    private readonly IConfiguration _config;

    public AuthController(ClinicDbContext db, IConfiguration config)
    {
        _db = db;
        _config = config;
    }

    public sealed record LoginRequest(string UserNameOrEmail, string Password);

    public sealed record LoginResponse(
        int UserId,
        string UserName,
        string Email,
        IReadOnlyList<string> Roles,
        string AccessToken,
        string TokenType,
        long ExpiresInSeconds);

    [AllowAnonymous]
    [HttpPost("login")]
    public async Task<ActionResult<LoginResponse>> Login([FromBody] LoginRequest request)
    {
        if (string.IsNullOrWhiteSpace(request.UserNameOrEmail) || string.IsNullOrWhiteSpace(request.Password))
        {
            return BadRequest("UserNameOrEmail and Password are required.");
        }

        var login = request.UserNameOrEmail.Trim();

        var user = await _db.users
            .Include(u => u.Role)
            .SingleOrDefaultAsync(u => u.UserName == login || u.Email == login);

        if (user is null || user.IsActive != true)
        {
            return Unauthorized();
        }

        if (!VerifyPassword(user, request.Password))
        {
            return Unauthorized();
        }

        var roles = user.Role
            .Select(r => r.Name)
            .Where(name => !string.IsNullOrWhiteSpace(name))
            .Distinct(StringComparer.OrdinalIgnoreCase)
            .ToArray();

        var expiresMinutes = int.TryParse(_config["Jwt:ExpiresMinutes"], out var m) ? m : 120;
        if (expiresMinutes <= 0) expiresMinutes = 120;

        var now = DateTime.UtcNow;
        var expires = now.AddMinutes(expiresMinutes);
        var token = CreateJwt(user, roles, now, expires);

        return Ok(new LoginResponse(
            UserId: user.Id,
            UserName: user.UserName,
            Email: user.Email,
            Roles: roles,
            AccessToken: token,
            TokenType: "Bearer",
            ExpiresInSeconds: (long)(expires - now).TotalSeconds));
    }

    // JWT is stateless; "logout" is typically done client-side by deleting the token.
    [Authorize]
    [HttpPost("logout")]
    public IActionResult Logout()
    {
        return Ok(new { message = "Logged out (client should discard token)." });
    }

    private string CreateJwt(users user, IReadOnlyList<string> roles, DateTime nowUtc, DateTime expiresUtc)
    {
        var issuer = _config["Jwt:Issuer"];
        var audience = _config["Jwt:Audience"];
        var key = _config["Jwt:Key"];

        if (string.IsNullOrWhiteSpace(key))
        {
            throw new InvalidOperationException("Jwt:Key is not configured.");
        }

        var claims = new List<Claim>
        {
            new(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
            new(JwtRegisteredClaimNames.UniqueName, user.UserName),
            new(JwtRegisteredClaimNames.Email, user.Email),
            new(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString("N")),
            new(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new(ClaimTypes.Name, user.UserName),
        };

        foreach (var role in roles)
        {
            claims.Add(new Claim(ClaimTypes.Role, role));
        }

        var signingKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));
        var creds = new SigningCredentials(signingKey, SecurityAlgorithms.HmacSha256);

        var jwt = new JwtSecurityToken(
            issuer: issuer,
            audience: audience,
            claims: claims,
            notBefore: nowUtc,
            expires: expiresUtc,
            signingCredentials: creds);

        return new JwtSecurityTokenHandler().WriteToken(jwt);
    }

    private static bool VerifyPassword(users user, string password)
    {
        var stored = user.PasswordHash?.Trim();
        if (string.IsNullOrWhiteSpace(stored))
        {
            return false;
        }

        // CodeIgniter / PHP password_hash() typically stores bcrypt hashes like "$2y$...".
        if (stored.StartsWith("$2", StringComparison.Ordinal))
        {
            // Some BCrypt implementations use $2y$; BCrypt.Net accepts it, but normalize just in case.
            var normalized = stored.StartsWith("$2y$", StringComparison.Ordinal)
                ? "$2a$" + stored[4..]
                : stored;
            return BCrypt.Net.BCrypt.Verify(password, normalized);
        }

        // ASP.NET Core Identity default hasher format usually starts with "AQAAAA".
        if (stored.StartsWith("AQAAAA", StringComparison.Ordinal))
        {
            var hasher = new PasswordHasher<users>();
            return hasher.VerifyHashedPassword(user, stored, password) != PasswordVerificationResult.Failed;
        }

        // Fallback for legacy/plaintext (not recommended).
        return string.Equals(stored, password, StringComparison.Ordinal);
    }
}
