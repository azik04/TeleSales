using TeleSales.Core.Dto.Call;
using TeleSales.Core.Response;

namespace TeleSales.Core.Interfaces.Call;

public interface ICallService
{
    Task<BaseResponse<bool>> ImportFromExcelAsync(Stream excelFileStream, long kanalId);
    Task<byte[]> ExportToExcelAsync(long kanalId);
    Task<BaseResponse<GetCallDto>> Create(CreateCallDto dto);
    Task<BaseResponse<PagedResponse<GetCallDto>>> GetAllByUser(long userId, long kanalId, int pageNumber, int pageSize);
    Task<BaseResponse<PagedResponse<GetCallDto>>> GetAllNotExcluded(long kanalId, int pageNumber, int pageSize);
    Task<BaseResponse<PagedResponse<GetCallDto>>> GetAllExcluded(long kanalId, int pageNumber, int pageSize);
    Task<BaseResponse<ICollection<GetCallDto>>> GetRandomCallsByVoen(long kanalId);
    Task<BaseResponse<GetCallDto>> GetById(long id);
    Task<BaseResponse<GetCallDto>> Update(long id, UpdateCallDto dto);
    Task<BaseResponse<GetCallDto>> Exclude(long id, ExcludeCallDto dto);
    Task<BaseResponse<GetCallDto>> Remove(long id);
}
