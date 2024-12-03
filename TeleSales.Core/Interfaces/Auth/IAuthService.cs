using TeleSales.Core.Dto.AUTH;
using TeleSales.Core.Response;

namespace TeleSales.Core.Interfaces.Auth;

public interface IAuthService
{
    Task<BaseResponse<string>> LogIn(AuthDto dto);
}
