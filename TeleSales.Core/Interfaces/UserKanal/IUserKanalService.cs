using TeleSales.Core.Dto.Kanal;
using TeleSales.Core.Dto.User;
using TeleSales.Core.Dto.UserKanal;
using TeleSales.Core.Response;

namespace TeleSales.Core.Interfaces.UserKanal;

public interface IUserKanalService
{
    Task<BaseResponse<GetUserKanalDto>> AddToChanelAsync(CreateUserKanalDto dto);
    Task<BaseResponse<ICollection<GetKanalDto>>> GetAllByUserId(long userId);
    Task<BaseResponse<ICollection<GetUserDto>>> GetAllByKanalId(long kanalId);
    Task<BaseResponse<GetUserKanalDto>> RemoveUserKanalAsync(long userId, long kanalId);
}
