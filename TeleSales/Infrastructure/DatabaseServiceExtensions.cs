using Microsoft.EntityFrameworkCore;
using TeleSales.DataProvider.Context;

namespace TeleSales.Infrastructure;

public static class DatabaseServiceExtensions
{
    public static void AddDatabaseConfiguration(this IServiceCollection services, IConfiguration configuration)
    {
        var connection = configuration.GetConnectionString("DefaultConnection");
        services.AddDbContext<ApplicationDbContext>(options =>
            options.UseSqlServer(connection));

        //configuration.GetConnectionString("MvcMovieContext"))
    }
}
