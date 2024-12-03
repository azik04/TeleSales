using TeleSales.Core.Interfaces.Auth;
using TeleSales.Core.Interfaces.Call;
using TeleSales.Core.Interfaces.Kanal;
using TeleSales.Core.Interfaces.User;
using TeleSales.Core.Services.AUTH;
using TeleSales.Core.Services.Call;
using TeleSales.Core.Services.Kanal;
using TeleSales.Core.Services.User;

namespace TeleSales.Infrastructure;

public static class ServiceRegistrationExtensions
{
    public static void AddServiceDependencies(this IServiceCollection services)
    {
        services.AddScoped<IAuthService, AuthService>();
        services.AddScoped<IUserService, UserService>();
        services.AddScoped<IKanalService, KanalService>();
        services.AddScoped<ICallService, CallService>();
    }
}
