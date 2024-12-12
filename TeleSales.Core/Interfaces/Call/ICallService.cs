﻿using TeleSales.Core.Dto.Call;
using TeleSales.Core.Response;

namespace TeleSales.Core.Interfaces.Call;

public interface ICallService
{
    Task<BaseResponse<ICollection<GetCallDto>>> ImportFromExcelAsync(Stream excelFileStream, long kanalId);
    Task<byte[]> ExportToExcelAsync(long kanalId);
    Task<byte[]> ExportToPdfAsync(long kanalId);
    Task<BaseResponse<GetCallDto>> Create(CreateCallDto dto);
    Task<BaseResponse<PagedResponse<GetCallDto>>> GetAllByKanal(long kanalId, int pageNumber, int pageSize);
    Task<BaseResponse<PagedResponse<GetCallDto>>> GetAllByUser(long userId, int pageNumber, int pageSize);
    Task<BaseResponse<PagedResponse<GetCallDto>>> GetAllNotExcluded(long kanalId, int pageNumber, int pageSize);
    Task<BaseResponse<PagedResponse<GetCallDto>>> GetAllExcluded(long kanalId, int pageNumber, int pageSize);
    Task<BaseResponse<GetCallDto>> GetRandomCall();
    Task<BaseResponse<PagedResponse<GetCallDto>>> FindAsync(string query, int pageNumber, int pageSize);
    Task<BaseResponse<GetCallDto>> GetById(long id);
    Task<BaseResponse<GetCallDto>> Update(long id, UpdateCallDto dto);
    Task<BaseResponse<GetCallDto>> Exclude(long id, ExcludeCallDto dto);
    Task<BaseResponse<GetCallDto>> Remove(long id);
}
