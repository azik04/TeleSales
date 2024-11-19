using TeleSales.Core.Dto.Call;
using TeleSales.Core.Response;

namespace TeleSales.Core.Interfaces.Call;

public interface ICallService
{
    Task<BaseResponse<ICollection<GetCallDto>>> ImportFromExcelAsync(Stream excelFileStream); 
    Task<BaseResponse<GetCallDto>> Create(CreateCallDto dto);
    Task<BaseResponse<PagedResponse<GetCallDto>>> GetAllByKanal(long kanalId, int pageNumber, int pageSize);
    Task<BaseResponse<PagedResponse<GetCallDto>>> GetAllByKanalAndUser(long kanalId, long userId, int pageNumber, int pageSize);
    Task<BaseResponse<GetCallDto>> GetById(long id);
    Task<BaseResponse<GetCallDto>> Update(long id, UpdateCallDto dto);
    Task<BaseResponse<GetCallDto>> Remove(long id);
}
