using TeleSales.Core.Dto.User;
using TeleSales.Core.Response;
using TeleSales.DataProvider.Enums;

namespace TeleSales.Core.Interfaces.User;

public interface IUserService
{
    Task<BaseResponse<GetUserDto>> Create(CreateUserDto dto);
    Task<BaseResponse<ICollection<GetUserDto>>> GetAll();
    Task<BaseResponse<ICollection<GetUserDto>>> GetAllAdmin();
    Task<BaseResponse<ICollection<GetUserDto>>> GetAllUser();
    Task<BaseResponse<ICollection<GetUserDto>>> GetAllViewer();
    Task<BaseResponse<ICollection<GetUserDto>>> GetAllBasOperator();
    Task<BaseResponse<GetUserDto>> GetById(long id);
    Task<BaseResponse<GetUserDto>> Update(long id, UpdateUserDto dto);
    Task<BaseResponse<GetUserDto>> ChangePassword(long id, ChangePasswordDto dto);
    Task<BaseResponse<GetUserDto>> ChangeRole(long id, Role role);
    Task<BaseResponse<GetUserDto>> Remove(long id);
}
