using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using TeleSales.Core.Dto.AUTH;
using TeleSales.Core.Dto.User;
using TeleSales.Core.Interfaces.Auth;
using TeleSales.Core.Response;
using TeleSales.DataProvider.Context;
using TeleSales.DataProvider.Entities;

namespace TeleSales.Core.Services.AUTH;

public class AuthService : IAuthService
{
    private readonly ApplicationDbContext _db;
    public AuthService(ApplicationDbContext db)
    {
        _db = db;
    }
    public async Task<string> LogIn(AuthDto dto)
    {
        var user = await _db.Users.FirstOrDefaultAsync(u => u.Email == dto.Email);
        if (user == null || user.isDeleted)
        {
            return new string("Invalid email or password");
        }

        if (user.Password != dto.Password)
        {
            return new string("Invalid email or password");
        }

        var token = GenerateJwtToken(user);

        return token;
    }


    private string GenerateJwtToken(Users user)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.ASCII.GetBytes("77BD25DB-C4D1-46EE-97F9-6847892262C0");

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new[]
            {
            new Claim(ClaimTypes.Name, user.id.ToString()),
            new Claim(ClaimTypes.Email, user.Email)
        }),
            Expires = DateTime.UtcNow.AddHours(1),
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        };

        var token = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(token);
    }
}
