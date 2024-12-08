using Microsoft.EntityFrameworkCore;
using TeleSales.Core.Dto.AUTH;
using TeleSales.Core.Response;
using TeleSales.Core.Helpers;
using TeleSales.DataProvider.Context;
using TeleSales.Core.Interfaces.Auth;

namespace TeleSales.Core.Services.AUTH
{
    public class AuthService : IAuthService
    {
        private readonly ApplicationDbContext _db;
        public AuthService(ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task<BaseResponse<string>> LogIn(AuthDto dto)
        {
            var user = await _db.Users.FirstOrDefaultAsync(u => u.Email == dto.Email && !u.isDeleted);

            if (user == null || user.isDeleted)
            {
                return new BaseResponse<string>(
                    data: null,
                    success: false,
                    message: "Invalid email or password"
                );
            }

            if (user.Password != dto.Password)
            {
                return new BaseResponse<string>(
                    data: null,
                    success: false,
                    message: "Invalid email or password"
                );
            }

            var tokenExpiration = dto.RememberMe ? TimeSpan.FromDays(365 * 100) : TimeSpan.FromDays(1);

            var jwtHelper = new GenerateJwtHelper();
            var token = jwtHelper.GenerateJwtToken(user, tokenExpiration);

            return new BaseResponse<string>(
                data: token,
                success: true,
                message: "Login successful"
            );
        }
    }
}
