using TeleSales.Core.Dto.Call;
using TeleSales.Core.Dto.CallCenter;
using TeleSales.Core.Response;

namespace TeleSales.Core.Interfaces.CallCenter;

public interface ICallCenterService
{
    //Task<BaseResponse<bool>> ImportFromExcelAsync(Stream excelFileStream, long kanalId);
    Task<byte[]> ExportToExcelAsync(long kanalId);
    Task<BaseResponse<GetCallCenterDto>> Create(CreateCallCenterDto dto);
    Task<BaseResponse<PagedResponse<GetCallCenterDto>>> GetAllByUser(long userId, int pageNumber, int pageSize);
    Task<BaseResponse<PagedResponse<GetCallCenterDto>>> GetAll(long kanalId, int pageNumber, int pageSize);
    Task<BaseResponse<GetCallCenterDto>> GetById(long id);
    Task<BaseResponse<GetCallCenterDto>> Update(long id, UpdateCallCenterDto dto);
    Task<BaseResponse<GetCallCenterDto>> Remove(long id);
}
