using TeleSales.Core.Dto.User;
using TeleSales.Core.Response;

namespace TeleSales.Core.Interfaces.User;

public interface IUserService
{
    Task<BaseResponse<GetUserDto>> Create(CreateUserDto dto);
    Task<BaseResponse<ICollection<GetUserDto>>> GetAll();
    Task<BaseResponse<ICollection<GetUserDto>>> GetAllAdmin();
    Task<BaseResponse<ICollection<GetUserDto>>> GetAllUser();
    Task<BaseResponse<GetUserDto>> GetById(long id);
    Task<BaseResponse<GetUserDto>> Update(long id, UpdateUserDto dto);
    Task<BaseResponse<GetUserDto>> ChangePassword(long id, ChangePasswordDto dto);
    Task<BaseResponse<GetUserDto>> ChangeRole(long id);
    Task<BaseResponse<GetUserDto>> Remove(long id);
}
