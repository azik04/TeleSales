namespace TeleSales.Infrastructure;

public static class CorsServiceExtensions
{
    public static void Cors(this IServiceCollection services)
    {
        services.AddCors(options =>
        {
            options.AddPolicy("AllowLocalhost",
                builder => builder.WithOrigins("http://localhost:3000")
                                  .AllowAnyMethod()
                                  .AllowAnyHeader());
        });
    }
}
