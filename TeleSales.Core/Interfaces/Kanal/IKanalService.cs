using TeleSales.Core.Dto.Kanal;
using TeleSales.Core.Response;

namespace TeleSales.Core.Interfaces.Kanal;

public interface IKanalService
{
    Task<BaseResponse<GetKanalDto>> Create(CreateKanalDto dto);
    Task<BaseResponse<ICollection<GetKanalDto>>> GetAll();
    Task<BaseResponse<GetKanalDto>> GetById(long id);
    Task<BaseResponse<GetKanalDto>> Update(long id, UpdateKanalDto dto);
    Task<BaseResponse<GetKanalDto>> Remove(long id);
}
