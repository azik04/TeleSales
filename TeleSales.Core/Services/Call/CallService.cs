using Microsoft.EntityFrameworkCore;
using OfficeOpenXml;
using TeleSales.Core.Dto.Call;
using TeleSales.Core.Interfaces.Call;
using TeleSales.Core.Response;
using TeleSales.DataProvider.Context;
using TeleSales.DataProvider.Entities;

namespace TeleSales.Core.Services.Call;

public class CallService : ICallService
{
    private readonly ApplicationDbContext _db;
    public CallService(ApplicationDbContext db)
    {
        _db = db;
    }



    public async Task<BaseResponse<ICollection<GetCallDto>>> ImportFromExcelAsync(Stream excelFileStream)
    {
        var response = new BaseResponse<ICollection<GetCallDto>>(new List<GetCallDto>());

        try
        {
            using (var package = new ExcelPackage(excelFileStream))
            {
                var worksheet = package.Workbook.Worksheets[0];
                var rows = worksheet.Dimension.Rows;
                var calls = new List<Calls>();

                var users = await _db.Users.Where(u => !u.isDeleted).ToListAsync();
                if (!users.Any())
                {
                    response.Success = false;
                    response.Message = "No active users found for call assignment.";
                    return response;
                }

                int userCount = users.Count;
                int userIndex = 0;

                for (int row = 2; row <= rows; row++)
                {
                    var user = users[userIndex];
                    userIndex = (userIndex + 1) % userCount; 

                    var call = new Calls
                    {
                        Status = worksheet.Cells[row, 1].Text,
                        AcquisitionDate = DateOnly.Parse(worksheet.Cells[row, 2].Text),
                        KanalId = long.Parse(worksheet.Cells[row, 3].Text),
                        EntrepreneurName = worksheet.Cells[row, 4].Text,
                        LegalName = worksheet.Cells[row, 5].Text,
                        VOEN = worksheet.Cells[row, 6].Text,
                        PermissionStartDate = DateOnly.Parse(worksheet.Cells[row, 7].Text),
                        PermissionNumber = worksheet.Cells[row, 8].Text,
                        Address = worksheet.Cells[row, 9].Text,
                        ContactDetails = worksheet.Cells[row, 10].Text,
                        Result = worksheet.Cells[row, 11].Text,
                        UserId = user.id 
                    };

                    calls.Add(call);
                }

                await _db.Calls.AddRangeAsync(calls);
                await _db.SaveChangesAsync();

                var getCallDtos = calls.Select(c => new GetCallDto
                {
                    Status = c.Status,
                    AcquisitionDate = c.AcquisitionDate,
                    KanalId = c.KanalId,
                    EntrepreneurName = c.EntrepreneurName,
                    LegalName = c.LegalName,
                    VOEN = c.VOEN,
                    PermissionStartDate = c.PermissionStartDate,
                    PermissionNumber = c.PermissionNumber,
                    Address = c.Address,
                    ContactDetails = c.ContactDetails,
                    Result = c.Result,
                    UserId = c.UserId
                }).ToList();

                response.Data = getCallDtos;
            }
        }
        catch (Exception ex)
        {
            response.Success = false;
            response.Message = $"Ошибка при обработке файла: {ex.Message}";
        }

        return response;
    }


    public async Task<BaseResponse<GetCallDto>> Create(CreateCallDto dto)
    {
        var user = await _db.Users
            .Where(u => !u.isDeleted)
            .OrderBy(u => _db.Calls.Count(c => c.UserId == u.id))
            .FirstOrDefaultAsync();

        if (user == null)
            return new BaseResponse<GetCallDto>(null, false, "No active users found.");

        var call = new Calls()
        {
            Status = dto.Status,
            AcquisitionDate = dto.AcquisitionDate,
            KanalId = dto.KanalId,
            EntrepreneurName = dto.EntrepreneurName,
            LegalName = dto.LegalName,
            VOEN = dto.VOEN,
            PermissionStartDate = dto.PermissionStartDate,
            PermissionNumber = dto.PermissionNumber,
            Address = dto.Address,
            ContactDetails = dto.ContactDetails,
            Result = dto.Result,
            UserId = user.id, 
            CreateAt = DateTime.UtcNow,
        };

        await _db.Calls.AddAsync(call);
        await _db.SaveChangesAsync();

        var newCall = new GetCallDto()
        {
            id = call.id,
            isDelete = call.isDeleted,
            Status = call.Status,
            AcquisitionDate = call.AcquisitionDate,
            KanalId = call.KanalId,
            EntrepreneurName = call.EntrepreneurName,
            LegalName = call.LegalName,
            VOEN = call.VOEN,
            PermissionStartDate = call.PermissionStartDate,
            PermissionNumber = call.PermissionNumber,
            Address = call.Address,
            ContactDetails = call.ContactDetails,
            Result = call.Result,
            UserId = call.UserId,
            CreateAt = call.CreateAt,
        };
        return new BaseResponse<GetCallDto>(newCall);
    }


    public async Task<BaseResponse<PagedResponse<GetCallDto>>> GetAllByKanal(long kanalId, int pageNumber, int pageSize)
    {
        if (kanalId == 0)
            return new BaseResponse<PagedResponse<GetCallDto>>(null, false, "KanalId cant be 0.");

        var calls = _db.Calls.Where(x => x.KanalId == kanalId && !x.isDeleted);
        calls = calls
           .Skip((pageNumber - 1) * pageSize)
           .Take(pageSize);
        var callDtos = calls.Select(call => new GetCallDto
        {
            id = call.id,
            isDelete = call.isDeleted,
            Status = call.Status,
            AcquisitionDate = call.AcquisitionDate,
            KanalId = call.KanalId,
            EntrepreneurName = call.EntrepreneurName,
            LegalName = call.LegalName,
            VOEN = call.VOEN,
            PermissionStartDate = call.PermissionStartDate,
            PermissionNumber = call.PermissionNumber,
            Address = call.Address,
            ContactDetails = call.ContactDetails,
            Result = call.Result,
            UserId = call.UserId,
            CreateAt = DateTime.UtcNow,
        }).ToList();

        var pagedResult = new PagedResponse<GetCallDto>
        {
            Items = callDtos,
            TotalCount = callDtos.Count,
            PageSize = pageSize,
            CurrentPage = pageNumber,
        };
        return new BaseResponse<PagedResponse<GetCallDto>>(pagedResult);
    }

    public async Task<BaseResponse<PagedResponse<GetCallDto>>> GetAllByKanalAndUser(long kanalId, long userId, int pageNumber, int pageSize)
    {
        if (kanalId == 0 || userId == 0)
            return new BaseResponse<PagedResponse<GetCallDto>>(null, false, "KanalId or UserId cant be 0.");

        var calls = _db.Calls.Where(x => x.KanalId == kanalId && x.UserId == userId && !x.isDeleted);
        calls = calls
           .Skip((pageNumber - 1) * pageSize)
           .Take(pageSize);

        var callDtos = calls.Select(call => new GetCallDto
        {
            id = call.id,
            isDelete = call.isDeleted,
            Status = call.Status,
            AcquisitionDate = call.AcquisitionDate,
            KanalId = call.KanalId,
            EntrepreneurName = call.EntrepreneurName,
            LegalName = call.LegalName,
            VOEN = call.VOEN,
            PermissionStartDate = call.PermissionStartDate,
            PermissionNumber = call.PermissionNumber,
            Address = call.Address,
            ContactDetails = call.ContactDetails,
            Result = call.Result,
            UserId = call.UserId,
            CreateAt = DateTime.UtcNow,
        }).ToList();

        var pagedResult = new PagedResponse<GetCallDto>
        {
            Items = callDtos,
            TotalCount = callDtos.Count,
            PageSize = pageSize,
            CurrentPage = pageNumber,
        };

        return new BaseResponse<PagedResponse<GetCallDto>>(pagedResult);
    }

    public async Task<BaseResponse<GetCallDto>> GetById(long id)
    {
        if (id == 0)
            return new BaseResponse<GetCallDto>(null, false, "Id cant be 0.");
        
        var call = await _db.Calls.SingleOrDefaultAsync(x => x.id == id && !x.isDeleted);

        if (call == null)
            return new BaseResponse<GetCallDto>(null, false, "Call cant be NULL.");

        var newCall = new GetCallDto()
        {
            id = call.id,
            isDelete = call.isDeleted,
            Status = call.Status,
            AcquisitionDate = call.AcquisitionDate,
            KanalId = call.KanalId,
            EntrepreneurName = call.EntrepreneurName,
            LegalName = call.LegalName,
            VOEN = call.VOEN,
            PermissionStartDate = call.PermissionStartDate,
            PermissionNumber = call.PermissionNumber,
            Address = call.Address,
            ContactDetails = call.ContactDetails,
            Result = call.Result,
            UserId = call.UserId,
            CreateAt = DateTime.UtcNow,
        };

        return new BaseResponse<GetCallDto>(newCall);
    }

    public async Task<BaseResponse<GetCallDto>> Remove(long id)
    {
        if (id == 0)
            return new BaseResponse<GetCallDto>(null, false, "Id cant be 0.");

        var call = await _db.Calls.SingleOrDefaultAsync(x => x.id == id && !x.isDeleted);
        
        if (call == null)
            return new BaseResponse<GetCallDto>(null, false, "Call cant be NULL.");
        
        call.isDeleted = true;

        _db.Calls.Update(call);
        await _db.SaveChangesAsync();

        var newCall = new GetCallDto()
        {
            id = call.id,
            isDelete = call.isDeleted,
            Status = call.Status,
            AcquisitionDate = call.AcquisitionDate,
            KanalId = call.KanalId,
            EntrepreneurName = call.EntrepreneurName,
            LegalName = call.LegalName,
            VOEN = call.VOEN,
            PermissionStartDate = call.PermissionStartDate,
            PermissionNumber = call.PermissionNumber,
            Address = call.Address,
            ContactDetails = call.ContactDetails,
            Result = call.Result,
            UserId = call.UserId,
            CreateAt = DateTime.UtcNow,
        };

        return new BaseResponse<GetCallDto>(newCall);
    }

    public async Task<BaseResponse<GetCallDto>> Update(long id, UpdateCallDto dto)
    {
        if (id == 0)
            return new BaseResponse<GetCallDto>(null, false, "Id cant be 0.");

        var call = await _db.Calls.SingleOrDefaultAsync(x => x.id == id && !x.isDeleted);

        if (call == null)
            return new BaseResponse<GetCallDto>(null, false, "Call cant be NULL.");



        call.Status = dto.Status;
        call.AcquisitionDate = dto.AcquisitionDate;
        call.EntrepreneurName = dto.EntrepreneurName;
        call.LegalName = dto.LegalName;
        call.VOEN = dto.VOEN;
        call.PermissionStartDate = dto.PermissionStartDate;
        call.PermissionNumber = dto.PermissionNumber;
        call.Address = dto.Address;
        call.ContactDetails = dto.ContactDetails;
        call.Result = dto.Result;
        call.UserId = dto.UserId;

        _db.Calls.Update(call);
        await _db.SaveChangesAsync();

        var newCall = new GetCallDto()
        {
            id = call.id,
            isDelete = call.isDeleted,
            Status = call.Status,
            AcquisitionDate = call.AcquisitionDate,
            KanalId = call.KanalId,
            EntrepreneurName = call.EntrepreneurName,
            LegalName = call.LegalName,
            VOEN = call.VOEN,
            PermissionStartDate = call.PermissionStartDate,
            PermissionNumber = call.PermissionNumber,
            Address = call.Address,
            ContactDetails = call.ContactDetails,
            Result = call.Result,
            UserId = call.UserId,
            CreateAt = DateTime.UtcNow,
        };

        return new BaseResponse<GetCallDto>(newCall);
    }
}
