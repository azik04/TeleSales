using TeleSales.Core.Dto.AUTH;

namespace TeleSales.Core.Interfaces.Auth;

public interface IAuthService
{
    Task<string> LogIn(AuthDto dto);
}
