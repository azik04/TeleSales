using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using TeleSales.DataProvider.Entities;

namespace TeleSales.Core.Helpers
{
    public class GenerateJwtHelper
    {
        private readonly string _jwtKey;

        public GenerateJwtHelper()
        {
            _jwtKey = "77BD25DB-C4D1-46EE-97F9-6847892262C0"; // Use a secure key, e.g., from environment variables
        }

        // Generate JWT Token
        public string GenerateJwtToken(Users user, TimeSpan expiration)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_jwtKey); // Ensure the key is correctly encoded

            // Set up the token descriptor
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.Name, user.id.ToString()),
                    new Claim(ClaimTypes.Role, user.Role.ToString())
                }),
                Expires = DateTime.UtcNow.Add(expiration), // Set expiration time
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor); // Create token
            return tokenHandler.WriteToken(token); // Return the token string
        }

        // Validate the JWT Token
        public ClaimsPrincipal ValidateJwtToken(string token)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_jwtKey); // Ensure the key used here is the same as the one used for signing

            try
            {
                var validationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ValidateLifetime = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key) // Key for validating the token signature
                };

                var principal = tokenHandler.ValidateToken(token, validationParameters, out var validatedToken);
                return principal; // Return the ClaimsPrincipal if valid
            }
            catch (SecurityTokenException)
            {
                return null; // Return null if token validation fails
            }
        }
    }
}
