using Microsoft.Extensions.Options;
using TeleSales.Core.Interfaces.Auth;
using TeleSales.Core.Interfaces.Call;
using TeleSales.Core.Interfaces.CallCenter;
using TeleSales.Core.Interfaces.Kanal;
using TeleSales.Core.Interfaces.User;
using TeleSales.Core.Interfaces.UserKanal;
using TeleSales.Core.Services.AUTH;
using TeleSales.Core.Services.Call;
using TeleSales.Core.Services.CallCenter;
using TeleSales.Core.Services.Kanal;
using TeleSales.Core.Services.User;
using TeleSales.Core.Services.UserKanal;
using TeleSales.Mail;
using TRAK.EmailSender;

namespace TeleSales.Infrastructure;

public static class ServiceRegistrationExtensions
{
    public static void AddServiceDependencies(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddMemoryCache();

        services.Configure<SmptSettings>(configuration.GetSection("SmptSettings"));
        services.AddScoped<SmptSettings>(sp =>
            sp.GetRequiredService<IOptions<SmptSettings>>().Value);

        services.AddScoped<IAuthService, AuthService>();
        services.AddScoped<ICallCenterService, CallCenterService>();
        services.AddScoped<IUserService, UserService>();
        services.AddScoped<IKanalService, KanalService>();
        services.AddScoped<ICallService, CallService>();
        services.AddScoped<IUserKanalService, UserKanalService>();
        services.AddScoped<IEmailService, EmailService>();
    }
}
