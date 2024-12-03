using Microsoft.EntityFrameworkCore;
using TeleSales.DataProvider.Context;

namespace TeleSales.Infrastructure;

public static class DatabaseServiceExtensions
{
    public static void AddDatabaseConfiguration(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<ApplicationDbContext>(options =>
            options.UseInMemoryDatabase("DefaultConnection"));
        //configuration.GetConnectionString("MvcMovieContext"))
    }
}
