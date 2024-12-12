using Microsoft.EntityFrameworkCore;
using OfficeOpenXml;
using QuestPDF.Fluent;
using TeleSales.Core.Dto.Call;
using TeleSales.Core.Interfaces.Call;
using TeleSales.Core.Response;
using TeleSales.DataProvider.Context;
using TeleSales.DataProvider.Entities;

namespace TeleSales.Core.Services.Call;

public class CallService : ICallService
{
    private static readonly Dictionary<long, GetCallDto> _cachedCalls = new();
    private static readonly TimeSpan CallHoldDuration = TimeSpan.FromDays(7);
    private readonly ApplicationDbContext _db;
    public CallService(ApplicationDbContext db)
    {
        _db = db;
    }


    public async Task<BaseResponse<ICollection<GetCallDto>>> ImportFromExcelAsync(Stream excelFileStream, long kanalId)
    {
        var response = new BaseResponse<ICollection<GetCallDto>>(new List<GetCallDto>());

        try
        {
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

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
                        EntrepreneurName = worksheet.Cells[row, 1].Text,
                        LegalName = worksheet.Cells[row, 2].Text,
                        VOEN = worksheet.Cells[row, 3].Text,
                        PermissionNumber = worksheet.Cells[row, 4].Text,
                        Status = worksheet.Cells[row, 5].Text,
                        Address = worksheet.Cells[row, 6].Text,
                        Phone = worksheet.Cells[row, 7].Text,
                        KanalId = kanalId,
                        PermissionStartDate = DateOnly.FromDateTime(DateTime.Now),
                    };

                    calls.Add(call);
                }

                await _db.Calls.AddRangeAsync(calls);
                await _db.SaveChangesAsync();

                var getCallDtos = calls.Select(c => new GetCallDto
                {
                    Status = c.Status,
                    KanalId = c.KanalId,
                    EntrepreneurName = c.EntrepreneurName,
                    LegalName = c.LegalName,
                    VOEN = c.VOEN,
                    PermissionStartDate = c.PermissionStartDate,
                    PermissionNumber = c.PermissionNumber,
                    Address = c.Address,
                    Phone = c.Phone,
                    UserId = c.UserId,
                    Note = c.Note,
                    LastStatusUpdate = c.LastStatusUpdate,
                    CreateAt = DateTime.UtcNow,
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


    public async Task<byte[]> ExportToExcelAsync(long kanalId)
    {
        try
        {
            var thresholdDate = DateTime.UtcNow.AddDays(-7);

            var calls = await _db.Calls
                .Where(x => x.KanalId == kanalId && !x.isDeleted &&
                (x.LastStatusUpdate != null && x.LastStatusUpdate.Value.ToUniversalTime() >= thresholdDate))
                .ToListAsync();

            if (!calls.Any())
            {
                throw new Exception("No calls found for the specified channel.");
            }

            foreach(var item in calls)
            {
                item.User = await _db.Users.SingleOrDefaultAsync(x => x.id == item.UserId);
            }

            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
            using (var package = new ExcelPackage())
            {
                // Add a worksheet
                var worksheet = package.Workbook.Worksheets.Add("Calls");

                // Define header row
                var headers = new[]
                {
                "Entrepreneur Name", "Legal Name", "VOEN", "Permission Number",
                "Status", "Address", "Phone", "Permission Start Date", "FullName", "LastStatusUpdate", "Conclusion"
                };

                for (int i = 0; i < headers.Length; i++)
                {
                    worksheet.Cells[1, i + 1].Value = headers[i];
                }

                // Populate rows with data
                for (int row = 0; row < calls.Count; row++)
                {
                    var call = calls[row];
                    worksheet.Cells[row + 2, 1].Value = call.EntrepreneurName;
                    worksheet.Cells[row + 2, 2].Value = call.LegalName;
                    worksheet.Cells[row + 2, 3].Value = call.VOEN;
                    worksheet.Cells[row + 2, 4].Value = call.PermissionNumber;
                    worksheet.Cells[row + 2, 5].Value = call.Status;
                    worksheet.Cells[row + 2, 6].Value = call.Address;
                    worksheet.Cells[row + 2, 7].Value = call.Phone;
                    worksheet.Cells[row + 2, 8].Value = call.PermissionStartDate.ToString();
                    worksheet.Cells[row + 2, 9].Value = call.User.FullName;
                    worksheet.Cells[row + 2, 10].Value = call.LastStatusUpdate.ToString();
                    worksheet.Cells[row + 2, 11].Value = call.Conclusion;

                }

                // Auto-fit columns
                worksheet.Cells[worksheet.Dimension.Address].AutoFitColumns();

                // Return the file as a byte array
                return package.GetAsByteArray();
            }
        }
        catch (Exception ex)
        {
            throw new Exception($"An error occurred while exporting data: {ex.Message}");
        }
    }


    public async Task<byte[]> ExportToPdfAsync(long kanalId)
    {
        try
        {
            QuestPDF.Settings.License = QuestPDF.Infrastructure.LicenseType.Community;

            var thresholdDate = DateTime.UtcNow.AddDays(-7);

            var calls = await  _db.Calls
                .Where(x => x.KanalId == kanalId && !x.isDeleted &&
                (x.LastStatusUpdate != null && x.LastStatusUpdate.Value.ToUniversalTime() >= thresholdDate))
                .ToListAsync();
            foreach (var item in calls)
            {
                item.User = await _db.Users.SingleOrDefaultAsync(x => x.id == item.id);
            }
            var kanal = await _db.Kanals.SingleOrDefaultAsync(x => x.id == kanalId);

            if (!calls.Any())
            {
                throw new Exception("No calls found for the specified channel.");
            }

            // Generate PDF using QuestPDF
            return QuestPDF.Fluent.Document.Create(container =>
            {
                container.Page(page =>
                {
                    page.Margin(30);

                    page.Header()
                        .Text($"Calls Report - Kanal ID: {kanal.Name}")
                        .FontSize(20)
                        .Bold()
                        .AlignCenter();

                    page.Content()
                        .Table(table =>
                        {
                            table.ColumnsDefinition(columns =>
                            {
                                columns.RelativeColumn(); // Entrepreneur Name
                                columns.RelativeColumn(); // Legal Name
                                columns.RelativeColumn(); // VOEN
                                columns.RelativeColumn(); // Permission Number
                                columns.RelativeColumn(); // Status
                                columns.RelativeColumn(); // Address
                                columns.RelativeColumn(); // Phone
                                columns.RelativeColumn(); // Permission Start Date
                                columns.RelativeColumn(); // Excluded User
                                columns.RelativeColumn(); // Last Status Update
                            });

                            table.Header(header =>
                            {
                                header.Cell().Text("Entrepreneur Name").Bold();
                                header.Cell().Text("Legal Name").Bold();
                                header.Cell().Text("VOEN").Bold();
                                header.Cell().Text("Permission Number").Bold();
                                header.Cell().Text("Status").Bold();
                                header.Cell().Text("Address").Bold();
                                header.Cell().Text("Phone").Bold();
                                header.Cell().Text("Permission Start Date").Bold();
                                header.Cell().Text("User").Bold();
                                header.Cell().Text("Last Status Update").Bold();
                            });

                            // Data rows
                            foreach (var call in calls)
                            {
                                table.Cell().Text(call.EntrepreneurName);
                                table.Cell().Text(call.LegalName);
                                table.Cell().Text(call.VOEN);
                                table.Cell().Text(call.PermissionNumber);
                                table.Cell().Text(call.Status);
                                table.Cell().Text(call.Address);
                                table.Cell().Text(call.Phone);
                                table.Cell().Text(call.PermissionStartDate);
                                table.Cell().Text(call.User.FullName);
                                table.Cell().Text(call.LastStatusUpdate);
                            }
                        });
                });
            }).GeneratePdf();
        }
        catch (Exception ex)
        {
            throw new Exception($"An error occurred while exporting data to PDF: {ex.Message}");
        }
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
            Status = "Yeni",
            KanalId = dto.KanalId,
            EntrepreneurName = dto.EntrepreneurName,
            LegalName = dto.LegalName,
            VOEN = dto.VOEN,
            PermissionStartDate = DateOnly.FromDateTime(DateTime.Now),
            PermissionNumber = dto.PermissionNumber,
            Address = dto.Address,
            Phone = dto.Phone,
            CreateAt = DateTime.UtcNow,
        };

        await _db.Calls.AddAsync(call);
        await _db.SaveChangesAsync();

        var newCall = new GetCallDto()
        {
            id = call.id,
            isDelete = call.isDeleted,
            Status = call.Status,
            KanalId = call.KanalId,
            EntrepreneurName = call.EntrepreneurName,
            LegalName = call.LegalName,
            VOEN = call.VOEN,
            PermissionStartDate = call.PermissionStartDate,
            PermissionNumber = call.PermissionNumber,
            Address = call.Address,
            Phone = call.Phone,
            UserId = call.UserId,
            CreateAt = call.CreateAt,
            Note = call.Note,
            LastStatusUpdate = call.LastStatusUpdate,
            Conclusion = call.Conclusion,
            isDone = call.isDone,
            NextCall = call.NextCall,
        };
        return new BaseResponse<GetCallDto>(newCall);
    }


    public async Task<BaseResponse<PagedResponse<GetCallDto>>> GetAllByKanal(long kanalId, int pageNumber, int pageSize)
    {
        if (kanalId == 0)
            return new BaseResponse<PagedResponse<GetCallDto>>(null, false, "KanalId cant be 0.");

        var calls = _db.Calls.Where(x => x.KanalId == kanalId && !x.isDeleted);

        var totalCount = await calls.CountAsync();

        calls = calls
           .Skip((pageNumber - 1) * pageSize)
           .Take(pageSize);
        var callDtos = calls.Select(call => new GetCallDto
        {
            id = call.id,
            isDelete = call.isDeleted,
            Status = call.Status,
            KanalId = call.KanalId,
            EntrepreneurName = call.EntrepreneurName,
            LegalName = call.LegalName,
            VOEN = call.VOEN,
            PermissionStartDate = call.PermissionStartDate,
            PermissionNumber = call.PermissionNumber,
            Address = call.Address,
            Phone = call.Phone,
            UserId = call.UserId,
            CreateAt = DateTime.UtcNow,
            Note = call.Note,
            LastStatusUpdate = call.LastStatusUpdate,
            Conclusion = call.Conclusion,
            isDone = call.isDone,
            NextCall = call.NextCall,
        }).ToList();

        var pagedResult = new PagedResponse<GetCallDto>
        {
            Items = callDtos,
            TotalCount = totalCount,
            PageSize = pageSize,
            CurrentPage = pageNumber,
        };
        return new BaseResponse<PagedResponse<GetCallDto>>(pagedResult);
    }


    public async Task<BaseResponse<PagedResponse<GetCallDto>>> GetAllNotExcluded(long kanalId, int pageNumber, int pageSize)
    {
        var currentDateTime = DateTime.UtcNow.AddHours(4);

        var calls = await _db.Calls
            .Where(x => x.KanalId == kanalId && !x.isDeleted &&
                        (string.IsNullOrEmpty(x.Conclusion) ||
                         (x.Conclusion == "Yenidən zəng" && x.NextCall.HasValue && x.NextCall.Value <= currentDateTime)))
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();

        var totalCount = await _db.Calls.CountAsync(x => x.KanalId == kanalId && !x.isDeleted &&
                                                         (string.IsNullOrEmpty(x.Conclusion) ||
                                                          (x.Conclusion == "Yenidən zəng" && x.NextCall.HasValue && x.NextCall.Value <= currentDateTime)));

        var callDtos = new List<GetCallDto>();
        foreach (var call in calls)
        {
            callDtos.Add(new GetCallDto
            {
                id = call.id,
                isDelete = call.isDeleted,
                Status = call.Status,
                KanalId = call.KanalId,
                EntrepreneurName = call.EntrepreneurName,
                LegalName = call.LegalName,
                VOEN = call.VOEN,
                PermissionStartDate = call.PermissionStartDate,
                PermissionNumber = call.PermissionNumber,
                Address = call.Address,
                Phone = call.Phone,
                UserId = call.UserId,
                CreateAt = DateTime.UtcNow,
            });
        }
        var pagedResult = new PagedResponse<GetCallDto>
        {
            Items = callDtos,
            TotalCount = totalCount,
            PageSize = pageSize,
            CurrentPage = pageNumber,
        };
        return new BaseResponse<PagedResponse<GetCallDto>>(pagedResult);
    }


    public async Task<BaseResponse<PagedResponse<GetCallDto>>> GetAllExcluded(long kanalId, int pageNumber, int pageSize)
    {
        var currentDateTime = DateTime.UtcNow.AddHours(4);

        var calls = await _db.Calls
            .Where(x => x.KanalId == kanalId && !x.isDeleted && x.Conclusion != null &&
                        x.Conclusion != "Yenidən zəng" ||
                        (x.Conclusion == "Yenidən zəng" && x.NextCall.HasValue && x.NextCall.Value > currentDateTime))
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();

        var totalCount = await _db.Calls.CountAsync(x => x.KanalId == kanalId && !x.isDeleted && x.Conclusion != null &&
                                                         x.Conclusion != "Yenidən zəng" ||
                                                         (x.Conclusion == "Yenidən zəng" && x.NextCall.HasValue && x.NextCall.Value > currentDateTime));

        var callDtos = new List<GetCallDto>();
        foreach (var call in calls)
        {
            var user = await _db.Users.SingleOrDefaultAsync(x => x.id == call.UserId);
            callDtos.Add(new GetCallDto
            {
                id = call.id,
                isDelete = call.isDeleted,
                Status = call.Status,
                KanalId = call.KanalId,
                EntrepreneurName = call.EntrepreneurName,
                LegalName = call.LegalName,
                VOEN = call.VOEN,
                PermissionStartDate = call.PermissionStartDate,
                PermissionNumber = call.PermissionNumber,
                Address = call.Address,
                Phone = call.Phone,
                UserId = call.UserId,
                CreateAt = DateTime.UtcNow,
                Note = call.Note,
                LastStatusUpdate = call.LastStatusUpdate,
                Conclusion = call.Conclusion,
                isDone = call.isDone,
                NextCall = call.NextCall,
                UserName = user?.FullName 
            });
        }
        var pagedResult = new PagedResponse<GetCallDto>
        {
            Items = callDtos,
            TotalCount = totalCount,
            PageSize = pageSize,
            CurrentPage = pageNumber,
        };
        return new BaseResponse<PagedResponse<GetCallDto>>(pagedResult);
    }



    public async Task<BaseResponse<PagedResponse<GetCallDto>>> GetAllByUser( long userId, int pageNumber, int pageSize)
    {
        var calls = await _db.Calls
            .Where(x => x.UserId == userId && !x.isDeleted)
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();

        var totalCount = await _db.Calls.CountAsync(x => x.UserId == userId && !x.isDeleted);


        var callDtos = new List<GetCallDto>();
        foreach (var call in calls)
        {
            var user = await _db.Users.SingleOrDefaultAsync(x => x.id == call.UserId);
            callDtos.Add(new GetCallDto
            {
                id = call.id,
                isDelete = call.isDeleted,
                Status = call.Status,
                KanalId = call.KanalId,
                EntrepreneurName = call.EntrepreneurName,
                LegalName = call.LegalName,
                VOEN = call.VOEN,
                PermissionStartDate = call.PermissionStartDate,
                PermissionNumber = call.PermissionNumber,
                Address = call.Address,
                Phone = call.Phone,
                UserId = call.UserId,
                CreateAt = DateTime.UtcNow,
                Note = call.Note,
                LastStatusUpdate = call.LastStatusUpdate,
                Conclusion = call.Conclusion,
                isDone = call.isDone,
                NextCall = call.NextCall,
                UserName = user?.FullName
            });
        }
        var pagedResult = new PagedResponse<GetCallDto>
        {
            Items = callDtos,
            TotalCount = totalCount, 
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
            KanalId = call.KanalId,
            EntrepreneurName = call.EntrepreneurName,
            LegalName = call.LegalName,
            VOEN = call.VOEN,
            PermissionStartDate = call.PermissionStartDate,
            PermissionNumber = call.PermissionNumber,
            Address = call.Address,
            Phone = call.Phone,
            UserId = call.UserId,
            CreateAt = DateTime.UtcNow,
            Note = call.Note,
            LastStatusUpdate = call.LastStatusUpdate,
            Conclusion = call.Conclusion,
            isDone = call.isDone,
            NextCall = call.NextCall,
        };
        return new BaseResponse<GetCallDto>(newCall);
    }


    public async Task<BaseResponse<GetCallDto>> GetRandomCall()
    {
        var currentDateTime = DateTime.UtcNow.AddHours(4);

        var priorityCalls = await _db.Calls
    .Where(x =>
        !x.isDeleted &&
        (string.IsNullOrEmpty(x.Conclusion) ||
        (x.Conclusion == "Yenidən zəng" && x.NextCall.HasValue && x.NextCall <= currentDateTime))
    )
    .ToListAsync();

        if (!priorityCalls.Any())
            return new BaseResponse<GetCallDto>(null, false, "No eligible calls available.");

        if (_cachedCalls.TryGetValue(priorityCalls.First().KanalId, out var cachedCall))
            return new BaseResponse<GetCallDto>(cachedCall);

        var random = new Random();
        var selectedCall = priorityCalls.OrderBy(_ => random.Next()).FirstOrDefault();

        if (selectedCall == null)
            return new BaseResponse<GetCallDto>(null, false, "No call selected.");

        var dto = new GetCallDto
        {
            id = selectedCall.id,
            isDelete = selectedCall.isDeleted,
            CreateAt = selectedCall.CreateAt,
            Status = selectedCall.Status,
            KanalId = selectedCall.KanalId,
            EntrepreneurName = selectedCall.EntrepreneurName,
            LegalName = selectedCall.LegalName,
            VOEN = selectedCall.VOEN,
            PermissionStartDate = selectedCall.PermissionStartDate,
            PermissionNumber = selectedCall.PermissionNumber,
            Address = selectedCall.Address,
            Phone = selectedCall.Phone,
            UserId = selectedCall.UserId,
            Note = selectedCall.Note,
            LastStatusUpdate = selectedCall.LastStatusUpdate,
            Conclusion = selectedCall.Conclusion,
            isDone = selectedCall.isDone,
            NextCall = selectedCall.NextCall,
        };

        _cachedCalls[selectedCall.KanalId] = dto;

        return new BaseResponse<GetCallDto>(dto);
    }


    public async Task<BaseResponse<PagedResponse<GetCallDto>>> FindAsync(string query, int pageNumber, int pageSize)
    {
        var callsQuery = _db.Set<Calls>().AsQueryable();

        if (!string.IsNullOrWhiteSpace(query))
        {
            callsQuery = callsQuery.Where(c =>
                c.EntrepreneurName.Contains(query) ||
                c.LegalName.Contains(query) ||
                c.VOEN.Contains(query) ||
                c.Phone.Contains(query));
        }

        var totalItems = await callsQuery.CountAsync();
        var calls = await callsQuery
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .Select(c => new GetCallDto
            {
                id = c.id,
                isDelete = c.isDeleted,
                Status = c.Status,
                KanalId = c.KanalId,
                EntrepreneurName = c.EntrepreneurName,
                LegalName = c.LegalName,
                VOEN = c.VOEN,
                PermissionStartDate = c.PermissionStartDate,
                PermissionNumber = c.PermissionNumber,
                Address = c.Address,
                Phone = c.Phone,
                UserId = c.UserId,
                CreateAt = DateTime.UtcNow,
                Note = c.Note,
                LastStatusUpdate = c.LastStatusUpdate,
                Conclusion = c.Conclusion,
                isDone = c.isDone,
                NextCall = c.NextCall,

            })
            .ToListAsync();

        var response = new PagedResponse<GetCallDto>
        {
            Items = calls,
            TotalCount = totalItems,
            CurrentPage = pageNumber,
            PageSize = pageSize
        };

        return new BaseResponse<PagedResponse<GetCallDto>>(response);
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
            KanalId = call.KanalId,
            EntrepreneurName = call.EntrepreneurName,
            LegalName = call.LegalName,
            VOEN = call.VOEN,
            PermissionStartDate = call.PermissionStartDate,
            PermissionNumber = call.PermissionNumber,
            Address = call.Address,
            Phone = call.Phone,
            UserId = call.UserId,
            CreateAt = DateTime.UtcNow,
            Note = call.Note,
            LastStatusUpdate = call.LastStatusUpdate,
            Conclusion = call.Conclusion,
            isDone = call.isDone,
            NextCall = call.NextCall,
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



        call.EntrepreneurName = dto.EntrepreneurName;
        call.LegalName = dto.LegalName;
        call.VOEN = dto.VOEN;
        call.PermissionNumber = dto.PermissionNumber;
        call.Address = dto.Address;
        call.Phone = dto.Phone;

        _db.Calls.Update(call);
        await _db.SaveChangesAsync();

        var newCall = new GetCallDto()
        {
            id = call.id,
            isDelete = call.isDeleted,
            Status = call.Status,
            KanalId = call.KanalId,
            EntrepreneurName = call.EntrepreneurName,
            LegalName = call.LegalName,
            VOEN = call.VOEN,
            PermissionStartDate = call.PermissionStartDate,
            PermissionNumber = call.PermissionNumber,
            Address = call.Address,
            Phone = call.Phone,
            UserId = call.UserId,
            CreateAt = DateTime.UtcNow,
            Note = call.Note,
            LastStatusUpdate = call.LastStatusUpdate,
            Conclusion = call.Conclusion,
            isDone = call.isDone,
            NextCall = call.NextCall,
        };

        return new BaseResponse<GetCallDto>(newCall);
    }


    public async Task<BaseResponse<GetCallDto>> Exclude(long id, ExcludeCallDto dto)
    {
        if (id == 0)
            return new BaseResponse<GetCallDto>(null, false, "Id cant be 0.");

        var call = await _db.Calls.SingleOrDefaultAsync(x => x.id == id && !x.isDeleted);

        if (call == null)
            return new BaseResponse<GetCallDto>(null, false, "Call cant be NULL.");


        call.UserId = dto.UserId;
        call.LastStatusUpdate = DateTime.Now;
        call.Conclusion = dto.Conclusion;
        call.Status = "Yenidən zəng";
        switch (dto.Conclusion)
        {
            case "Razılaşdı":
                call.Conclusion = "Razılaşdı";
                call.isDone = true;
                if (string.IsNullOrEmpty(dto.Note))
                    return new BaseResponse<GetCallDto>(null, false, "Необходимо указать номер договора.");
                call.Note = dto.Note;

                break;

            case "İmtina etdi":
                call.Conclusion = "İmtina etdi";
                if (string.IsNullOrEmpty(dto.Note))
                    return new BaseResponse<GetCallDto>(null, false, "Необходимо указать причину отказа.");
                call.Note = dto.Note;

                break;

            case "Nömrə səhvdir":
                call.Conclusion = "Nömrə səhvdir";
                call.Note = dto.Note;
                break;

            case "Zəng çatmır":
                call.Conclusion = "Zəng çatmır";
                call.Note = dto.Note;
                break;

            case "Yenidən zəng":
                call.Conclusion = "Yenidən zəng";
                if (string.IsNullOrEmpty(dto.Note))
                    return new BaseResponse<GetCallDto>(null, false, "Необходимо указать причину повторного звонка.");
                if (!dto.NextCall.HasValue)
                    return new BaseResponse<GetCallDto>(null, false, "Необходимо указать дату и время повторного звонка.");
                call.NextCall = dto.NextCall.Value;
                call.Note = dto.Note;
                break;

            default:
                return new BaseResponse<GetCallDto>(null, false, "Некорректное заключение.");
        }

        _db.Calls.Update(call);
        await _db.SaveChangesAsync();

        if (_cachedCalls.ContainsKey(call.KanalId))
        {
            _cachedCalls.Remove(call.KanalId);
        }

        var newCall = new GetCallDto()
        {
            id = call.id,
            isDelete = call.isDeleted,
            Status = call.Status,
            KanalId = call.KanalId,
            EntrepreneurName = call.EntrepreneurName,
            LegalName = call.LegalName,
            VOEN = call.VOEN,
            PermissionStartDate = call.PermissionStartDate,
            PermissionNumber = call.PermissionNumber,
            Address = call.Address,
            Phone = call.Phone,
            UserId = call.UserId,
            CreateAt = DateTime.UtcNow,
            Note = call.Note,
            LastStatusUpdate = call.LastStatusUpdate,
            Conclusion = call.Conclusion,
            isDone = call.isDone,
        };

        return new BaseResponse<GetCallDto>(newCall);
    }
}
