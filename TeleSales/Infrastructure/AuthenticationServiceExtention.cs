using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using TeleSales.DataProvider.Enums;

namespace TeleSales.Infrastructure;

public static class AuthenticationServiceExtensions
{
    public static void AddAuthenticationConfiguration(this IServiceCollection services, IConfiguration configuration)
    {
        var jwtSettings = configuration.GetSection("Jwt");

        // Настройка JWT Authentication

        services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        })
        .AddJwtBearer(options =>
        {
            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes("77BD25DB-C4D1-46EE-97F9-6847892262C0")),
                ValidateIssuer = false,
                ValidateAudience = false,

            };
        });

        services.AddAuthorization(options =>
        {
            options.AddPolicy("Admin", policy => policy.RequireRole(Role.Admin.ToString()));
            options.AddPolicy("BaşOperator", policy => policy.RequireRole(Role.BaşOperator.ToString(), Role.Admin.ToString()));
            options.AddPolicy("Operator", policy => policy.RequireRole(Role.Operator.ToString(), Role.BaşOperator.ToString(), Role.Admin.ToString()));
        });

    }
}
