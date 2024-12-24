using EFCore.BulkExtensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using OfficeOpenXml;
using QuestPDF.Helpers;
using TeleSales.Core.Dto.Call;
using TeleSales.Core.Interfaces.Call;
using TeleSales.Core.Response;
using TeleSales.DataProvider.Context;
using TeleSales.DataProvider.Entities;
using TeleSales.DataProvider.Enums.Debitor;

namespace TeleSales.Core.Services.Call;

public class CallService : ICallService
{
    private readonly IMemoryCache _memoryCache;
    private readonly ApplicationDbContext _db;

    public CallService(IMemoryCache memoryCache, ApplicationDbContext db)
    {
        _memoryCache = memoryCache;
        _db = db;
    }


    public async Task<BaseResponse<bool>> ImportFromExcelAsync(Stream excelFileStream, long kanalId)
    {
        try
        {
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

            using (var package = new ExcelPackage(excelFileStream))
            {
                var worksheet = package.Workbook.Worksheets[0];
                var callsList = new List<Calls>();

                for (int row = 4; row <= worksheet.Dimension.End.Row; row++)
                {
                    DateOnly? permissionStartDate = null;
                    DateOnly? permissionEndDate = null;

                    if (DateTime.TryParse(worksheet.Cells[row, 6].Text, out var startDate))
                    {
                        permissionStartDate = DateOnly.FromDateTime(startDate);
                    }
                    if (DateTime.TryParse(worksheet.Cells[row, 7].Text, out var endDate))
                    {
                        permissionEndDate = DateOnly.FromDateTime(endDate);
                    }

                    var call = new Calls
                    {
                        VOEN = worksheet.Cells[row, 3].Text,
                        InvoiceNumber = Convert.ToInt64(worksheet.Cells[row, 4].Value),
                        District = worksheet.Cells[row, 5].Text,
                        PermissionStartDate = permissionStartDate,
                        PermissionEndDate = permissionEndDate,
                        Street = worksheet.Cells[row, 8].Text,
                        Subject = worksheet.Cells[row, 9].Text,
                        Phone = worksheet.Cells[row, 10].Text,
                        LegalName = worksheet.Cells[row, 11].Text,
                        TotalDebt = string.IsNullOrEmpty(worksheet.Cells[row, 12].Text) || !decimal.TryParse(worksheet.Cells[row, 12].Text, out var totalDebt) ? null : totalDebt,
                        Year2018 = string.IsNullOrEmpty(worksheet.Cells[row, 13].Text) || !decimal.TryParse(worksheet.Cells[row, 13].Text, out var year2018) ? null : year2018,
                        Year2019 = string.IsNullOrEmpty(worksheet.Cells[row, 14].Text) || !decimal.TryParse(worksheet.Cells[row, 14].Text, out var year2019) ? null : year2019,
                        Year2020 = string.IsNullOrEmpty(worksheet.Cells[row, 15].Text) || !decimal.TryParse(worksheet.Cells[row, 15].Text, out var year2020) ? null : year2020,
                        Year2021 = string.IsNullOrEmpty(worksheet.Cells[row, 16].Text) || !decimal.TryParse(worksheet.Cells[row, 16].Text, out var year2021) ? null : year2021,
                        Year2022 = string.IsNullOrEmpty(worksheet.Cells[row, 17].Text) || !decimal.TryParse(worksheet.Cells[row, 17].Text, out var year2022) ? null : year2022,
                        Year2023 = string.IsNullOrEmpty(worksheet.Cells[row, 18].Text) || !decimal.TryParse(worksheet.Cells[row, 18].Text, out var year2023) ? null : year2023,
                        Month1_2024 = string.IsNullOrEmpty(worksheet.Cells[row, 19].Text) || !decimal.TryParse(worksheet.Cells[row, 19].Text, out var month1_2024) ? null : month1_2024,
                        Month2_2024 = string.IsNullOrEmpty(worksheet.Cells[row, 20].Text) || !decimal.TryParse(worksheet.Cells[row, 20].Text, out var month2_2024) ? null : month2_2024,
                        Month3_2024 = string.IsNullOrEmpty(worksheet.Cells[row, 21].Text) || !decimal.TryParse(worksheet.Cells[row, 21].Text, out var month3_2024) ? null : month3_2024,
                        Month4_2024 = string.IsNullOrEmpty(worksheet.Cells[row, 22].Text) || !decimal.TryParse(worksheet.Cells[row, 22].Text, out var month4_2024) ? null : month4_2024,
                        Month5_2024 = string.IsNullOrEmpty(worksheet.Cells[row, 23].Text) || !decimal.TryParse(worksheet.Cells[row, 23].Text, out var month5_2024) ? null : month5_2024,
                        Month6_2024 = string.IsNullOrEmpty(worksheet.Cells[row, 24].Text) || !decimal.TryParse(worksheet.Cells[row, 24].Text, out var month6_2024) ? null : month6_2024,
                        Month7_2024 = string.IsNullOrEmpty(worksheet.Cells[row, 25].Text) || !decimal.TryParse(worksheet.Cells[row, 25].Text, out var month7_2024) ? null : month7_2024,
                        Month8_2024 = string.IsNullOrEmpty(worksheet.Cells[row, 26].Text) || !decimal.TryParse(worksheet.Cells[row, 26].Text, out var month8_2024) ? null : month8_2024,
                        Month9_2024 = string.IsNullOrEmpty(worksheet.Cells[row, 27].Text) || !decimal.TryParse(worksheet.Cells[row, 27].Text, out var month9_2024) ? null : month9_2024,
                        Month10_2024 = string.IsNullOrEmpty(worksheet.Cells[row, 28].Text) || !decimal.TryParse(worksheet.Cells[row, 28].Text, out var month10_2024) ? null : month10_2024,
                        Month11_2024 = string.IsNullOrEmpty(worksheet.Cells[row, 29].Text) || !decimal.TryParse(worksheet.Cells[row, 29].Text, out var month11_2024) ? null : month11_2024,
                        Month12_2024 = string.IsNullOrEmpty(worksheet.Cells[row, 30].Text) || !decimal.TryParse(worksheet.Cells[row, 30].Text, out var month12_2024) ? null : month12_2024,
                        Month1_2025 = string.IsNullOrEmpty(worksheet.Cells[row, 31].Text) || !decimal.TryParse(worksheet.Cells[row, 31].Text, out var month1_2025) ? null : month1_2025,
                        Month2_2025 = string.IsNullOrEmpty(worksheet.Cells[row, 32].Text) || !decimal.TryParse(worksheet.Cells[row, 32].Text, out var month2_2025) ? null : month2_2025,
                        Month3_2025 = string.IsNullOrEmpty(worksheet.Cells[row, 33].Text) || !decimal.TryParse(worksheet.Cells[row, 33].Text, out var month3_2025) ? null : month3_2025,
                        KanalId = kanalId,
                        Status = CallStatus.Yeni,
                    };

                    callsList.Add(call);
                }

                await _db.BulkInsertAsync(callsList);

                return new BaseResponse<bool>(true, true, "Data successfully imported from Excel.");
            }
        }
        catch (Exception ex)
        {
            return new BaseResponse<bool>(false, false, $"Error while processing the Excel file: {ex.Message}");
        }
    }


    public async Task<byte[]> ExportToExcelAsync(long kanalId)
    {
        try
        {
            var currentDateTime = DateTime.Now;

            var calls = await _db.Calls
                .Where(x => x.KanalId == kanalId && !x.isDeleted &&
                    (x.Conclusion != null || (x.Conclusion == CallResult.YenidənZəng && x.NextCall.HasValue && x.NextCall.Value > currentDateTime)))
                .ToListAsync();
            if (!calls.Any())
            {
                throw new Exception("No calls found for the specified channel.");
            }

            foreach (var item in calls)
            {
                item.User = await _db.Users.SingleOrDefaultAsync(x => x.id == item.ExcludedBy);
            }

            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
            using (var package = new ExcelPackage())
            {
                var worksheet = package.Workbook.Worksheets.Add("Calls");

                var headers = new[]
                {
                "Kanal", "Status", "VÖEN", "Fakturanın nömrəsi","Rayon", "Başlama Tarixi", "Bitmə Tarixi", "Kücə", "Mövzu", "Əlaqə Nömrəsi", "Reklam yayıcının adı","Debitor üzrə cəm borclar",
                "2018", "2019", "2020", "2021","2022", "2023", "2024-1","2024-2","2024-3","2024-4","2024-5","2024-6","2024-7","2024-8","2024-9","2024-10","2024-11","2024-12","2025-1","2025-2","2025-3"
            };

                for (int i = 0; i < headers.Length; i++)
                {
                    worksheet.Cells[1, i + 1].Value = headers[i];
                }

                for (int row = 0; row < calls.Count; row++)
                {
                    var call = calls[row];

                    var user = await _db.Users.SingleOrDefaultAsync(x => x.id == call.ExcludedBy);
                    var kanal = await _db.Kanals.SingleOrDefaultAsync(x => x.id == call.KanalId);

                    worksheet.Cells[row + 2, 1].Value = kanal?.Name;
                    worksheet.Cells[row + 2, 2].Value = call.Status.ToString();
                    worksheet.Cells[row + 2, 3].Value = call.VOEN;
                    worksheet.Cells[row + 2, 4].Value = call.InvoiceNumber;
                    worksheet.Cells[row + 2, 5].Value = call.District;
                    worksheet.Cells[row + 2, 8].Value = call.PermissionStartDate?.ToString();
                    worksheet.Cells[row + 2, 9].Value = call.PermissionEndDate?.ToString();
                    worksheet.Cells[row + 2, 9].Value = call.Street;
                    worksheet.Cells[row + 2, 10].Value = call.Subject;
                    worksheet.Cells[row + 2, 11].Value = call.LegalName;
                    worksheet.Cells[row + 2, 11].Value = call.Phone;
                    worksheet.Cells[row + 2, 12].Value = user?.FullName;
                    worksheet.Cells[row + 2, 13].Value = call.Conclusion?.ToString();
                    worksheet.Cells[row + 2, 14].Value = call.Note;

                    worksheet.Cells[row + 2, 15].Value = call.TotalDebt;
                    worksheet.Cells[row + 2, 16].Value = call.Year2018;
                    worksheet.Cells[row + 2, 17].Value = call.Year2019;
                    worksheet.Cells[row + 2, 18].Value = call.Year2020;
                    worksheet.Cells[row + 2, 19].Value = call.Year2021;
                    worksheet.Cells[row + 2, 20].Value = call.Year2022;
                    worksheet.Cells[row + 2, 21].Value = call.Year2023;
                    worksheet.Cells[row + 2, 22].Value = call.Month1_2024;
                    worksheet.Cells[row + 2, 23].Value = call.Month2_2024;
                    worksheet.Cells[row + 2, 24].Value = call.Month3_2024;
                    worksheet.Cells[row + 2, 25].Value = call.Month4_2024;
                    worksheet.Cells[row + 2, 26].Value = call.Month5_2024;
                    worksheet.Cells[row + 2, 27].Value = call.Month6_2024;
                    worksheet.Cells[row + 2, 28].Value = call.Month7_2024;
                    worksheet.Cells[row + 2, 29].Value = call.Month8_2024;
                    worksheet.Cells[row + 2, 30].Value = call.Month9_2024;
                    worksheet.Cells[row + 2, 31].Value = call.Month10_2024;
                    worksheet.Cells[row + 2, 32].Value = call.Month11_2024;
                    worksheet.Cells[row + 2, 33].Value = call.Month12_2024;
                    worksheet.Cells[row + 2, 34].Value = call.Month1_2025;
                    worksheet.Cells[row + 2, 35].Value = call.Month2_2025;
                    worksheet.Cells[row + 2, 36].Value = call.Month3_2025;
                }
                worksheet.Cells[worksheet.Dimension.Address].AutoFitColumns();

                return package.GetAsByteArray();
            }
        }
        catch (Exception ex)
        {
            throw new Exception($"An error occurred while exporting data: {ex.Message}");
        }
    }


    public async Task<BaseResponse<GetCallDto>> Create(CreateCallDto dto)
    {
        var call = new Calls()
        {
            VOEN = dto.VOEN,
            InvoiceNumber = dto.InvoiceNumber,
            Subject = dto.Subject,
            District = dto.District,
            PermissionStartDate = dto.PermissionStartDate,
            PermissionEndDate = dto.PermissionEndDate,
            Street = dto.Street,
            Phone = dto.Phone,
            LegalName = dto.LegalName,
            TotalDebt = dto.TotalDebt,
            Year2018 = dto.Year2018,
            Year2019 = dto.Year2019,
            Year2020 = dto.Year2020,
            Year2021 = dto.Year2021,
            Year2022 = dto.Year2022,
            Year2023 = dto.Year2023,
            Month1_2024 = dto.Month1_2024,
            Month2_2024 = dto.Month2_2024,
            Month3_2024 = dto.Month3_2024,
            Month4_2024 = dto.Month4_2024,
            Month5_2024 = dto.Month5_2024,
            Month6_2024 = dto.Month6_2024,
            Month7_2024 = dto.Month7_2024,
            Month8_2024 = dto.Month8_2024,
            Month9_2024 = dto.Month9_2024,
            Month10_2024 = dto.Month10_2024,
            Month11_2024 = dto.Month11_2024,
            Month12_2024 = dto.Month12_2024,
            Month1_2025 = dto.Month1_2025,
            Month2_2025 = dto.Month2_2025,
            Month3_2025 = dto.Month3_2025,
            KanalId = dto.KanalId,
            Status = dto.Status,
        };

        await _db.Calls.AddAsync(call);
        await _db.SaveChangesAsync();

        var newCall = new GetCallDto()
        {
            Id = call.id,
            VOEN = call.VOEN,
            InvoiceNumber = call.InvoiceNumber,
            Subject = call.Subject,
            District = call.District,
            PermissionStartDate = call.PermissionStartDate,
            PermissionEndDate = call.PermissionEndDate,
            Street = call.Street,
            Phone = call.Phone,
            LegalName = call.LegalName,
            TotalDebt = call.TotalDebt,
            Year2018 = call.Year2018,
            Year2019 = call.Year2019,
            Year2020 = call.Year2020,
            Year2021 = call.Year2021,
            Year2022 = call.Year2022,
            Year2023 = call.Year2023,
            Month1_2024 = call.Month1_2024,
            Month2_2024 = call.Month2_2024,
            Month3_2024 = call.Month3_2024,
            Month4_2024 = call.Month4_2024,
            Month5_2024 = call.Month5_2024,
            Month6_2024 = call.Month6_2024,
            Month7_2024 = call.Month7_2024,
            Month8_2024 = call.Month8_2024,
            Month9_2024 = call.Month9_2024,
            Month10_2024 = call.Month10_2024,
            Month11_2024 = call.Month11_2024,
            Month12_2024 = call.Month12_2024,
            Month1_2025 = call.Month1_2025,
            Month2_2025 = call.Month2_2025,
            Month3_2025 = call.Month3_2025,
            KanalId = call.KanalId,
            Status = call.Status.ToString(),
        };
        return new BaseResponse<GetCallDto>(newCall);
    }


    public async Task<BaseResponse<PagedResponse<GetCallDto>>> GetAllNotExcluded(long kanalId, int pageNumber, int pageSize)
    {
        var currentDateTime = DateTime.Now;

        var calls = await _db.Calls
            .Where(x => x.KanalId == kanalId && !x.isDeleted &&
                (x.Conclusion == null || (x.Conclusion == CallResult.YenidənZəng && x.NextCall.HasValue && x.NextCall.Value <= currentDateTime)))
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();

        var totalCount = await _db.Calls.CountAsync(x => x.KanalId == kanalId && !x.isDeleted &&
            (x.Conclusion == null || (x.Conclusion == CallResult.YenidənZəng && x.NextCall.HasValue && x.NextCall.Value <= currentDateTime)));

        var callDtos = new List<GetCallDto>();
        foreach (var call in calls)
        {
            callDtos.Add(new GetCallDto
            {
                Id = call.id,
                VOEN = call.VOEN,
                InvoiceNumber = call.InvoiceNumber,
                Subject = call.Subject,
                District = call.District,
                PermissionStartDate = call.PermissionStartDate,
                PermissionEndDate = call.PermissionEndDate,
                Street = call.Street,
                Phone = call.Phone,
                LegalName = call.LegalName,
                TotalDebt = call.TotalDebt,
                Year2018 = call.Year2018,
                Year2019 = call.Year2019,
                Year2020 = call.Year2020,
                Year2021 = call.Year2021,
                Year2022 = call.Year2022,
                Year2023 = call.Year2023,
                Month1_2024 = call.Month1_2024,
                Month2_2024 = call.Month2_2024,
                Month3_2024 = call.Month3_2024,
                Month4_2024 = call.Month4_2024,
                Month5_2024 = call.Month5_2024,
                Month6_2024 = call.Month6_2024,
                Month7_2024 = call.Month7_2024,
                Month8_2024 = call.Month8_2024,
                Month9_2024 = call.Month9_2024,
                Month10_2024 = call.Month10_2024,
                Month11_2024 = call.Month11_2024,
                Month12_2024 = call.Month12_2024,
                Month1_2025 = call.Month1_2025,
                Month2_2025 = call.Month2_2025,
                Month3_2025 = call.Month3_2025,
                KanalId = call.KanalId,
                Status = call.Status.ToString(),
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
        var currentDateTime = DateTime.Now;

        var calls = await _db.Calls
            .Where(x => x.KanalId == kanalId && !x.isDeleted &&  
                (x.Conclusion != null || (x.Conclusion == CallResult.YenidənZəng && x.NextCall.HasValue && x.NextCall.Value > currentDateTime)))
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();

        var totalCount = await _db.Calls.CountAsync(x => x.KanalId == kanalId && !x.isDeleted && 
            (x.Conclusion != null || (x.Conclusion == CallResult.YenidənZəng && x.NextCall.HasValue && x.NextCall.Value > currentDateTime)));

        var callDtos = new List<GetCallDto>();
        foreach (var call in calls)
        {
            var user = await _db.Users.SingleOrDefaultAsync(x => x.id == call.ExcludedBy);
            callDtos.Add(new GetCallDto
            {
                Id = call.id,
                VOEN = call.VOEN,
                LegalName = call.LegalName,
                District = call.District,
                PermissionStartDate = call.PermissionStartDate,
                PermissionEndDate = call.PermissionEndDate,
                Street = call.Street,
                Phone = call.Phone,
                InvoiceNumber = call.InvoiceNumber,
                Subject = call.Subject,
                TotalDebt = call.TotalDebt,
                Year2018 = call.Year2018,
                Year2019 = call.Year2019,
                Year2020 = call.Year2020,
                Year2021 = call.Year2021,
                Year2022 = call.Year2022,
                Year2023 = call.Year2023,
                Month1_2024 = call.Month1_2024,
                Month2_2024 = call.Month2_2024,
                Month3_2024 = call.Month3_2024,
                Month4_2024 = call.Month4_2024,
                Month5_2024 = call.Month5_2024,
                Month6_2024 = call.Month6_2024,
                Month7_2024 = call.Month7_2024,
                Month8_2024 = call.Month8_2024,
                Month9_2024 = call.Month9_2024,
                Month10_2024 = call.Month10_2024,
                Month11_2024 = call.Month11_2024,
                Month12_2024 = call.Month12_2024,
                Month1_2025 = call.Month1_2025,
                Month2_2025 = call.Month2_2025,
                Month3_2025 = call.Month3_2025,
                KanalId = call.KanalId,
                Status = call.Status.ToString(),

                ExcludedBy = call.ExcludedBy,
                ExcludedByName = user?.FullName,
                Conclusion = call.Conclusion.ToString(),
                LastStatusUpdate = call.LastStatusUpdate,
                NextCall = call.NextCall,
                isDone = call.isDone,
                Note = call.Note,
                isDeleted = call.isDeleted,
                CreateAt = call.CreateAt,

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


    public async Task<BaseResponse<PagedResponse<GetCallDto>>> GetAllByUser(long userId, int pageNumber, int pageSize)
    {
        var calls = await _db.Calls
            .Where(x => x.ExcludedBy == userId && !x.isDeleted)
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();

        var totalCount = await _db.Calls.CountAsync(x => x.ExcludedBy == userId && !x.isDeleted);


        var callDtos = new List<GetCallDto>();
        foreach (var call in calls)
        {
            var user = await _db.Users.SingleOrDefaultAsync(x => x.id == call.ExcludedBy);
            callDtos.Add(new GetCallDto
            {
                Id = call.id,
                VOEN = call.VOEN,
                InvoiceNumber = call.InvoiceNumber,
                Subject = call.Subject,
                District = call.District,
                PermissionStartDate = call.PermissionStartDate,
                PermissionEndDate = call.PermissionEndDate,
                Street = call.Street,
                Phone = call.Phone,
                LegalName = call.LegalName,
                TotalDebt = call.TotalDebt,
                Year2018 = call.Year2018,
                Year2019 = call.Year2019,
                Year2020 = call.Year2020,
                Year2021 = call.Year2021,
                Year2022 = call.Year2022,
                Year2023 = call.Year2023,
                Month1_2024 = call.Month1_2024,
                Month2_2024 = call.Month2_2024,
                Month3_2024 = call.Month3_2024,
                Month4_2024 = call.Month4_2024,
                Month5_2024 = call.Month5_2024,
                Month6_2024 = call.Month6_2024,
                Month7_2024 = call.Month7_2024,
                Month8_2024 = call.Month8_2024,
                Month9_2024 = call.Month9_2024,
                Month10_2024 = call.Month10_2024,
                Month11_2024 = call.Month11_2024,
                Month12_2024 = call.Month12_2024,
                Month1_2025 = call.Month1_2025,
                Month2_2025 = call.Month2_2025,
                Month3_2025 = call.Month3_2025,
                KanalId = call.KanalId,
                Status = call.Status.ToString(),

                ExcludedBy = call.ExcludedBy,
                ExcludedByName = user?.FullName,
                Conclusion = call.Conclusion.ToString(),
                LastStatusUpdate = call.LastStatusUpdate,
                NextCall = call.NextCall,
                isDone = call.isDone,
                Note = call.Note,
                isDeleted = call.isDeleted,
                CreateAt = call.CreateAt,
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

        var user = await _db.Users.SingleOrDefaultAsync(x => x.id == call.ExcludedBy);
        var newCall = new GetCallDto()
        {
            Id = call.id,
            VOEN = call.VOEN,
            InvoiceNumber = call.InvoiceNumber,
            Subject = call.Subject,
            District = call.District,
            PermissionStartDate = call.PermissionStartDate,
            PermissionEndDate = call.PermissionEndDate,
            Street = call.Street,
            Phone = call.Phone,
            LegalName = call.LegalName,
            TotalDebt = call.TotalDebt,
            Year2018 = call.Year2018,
            Year2019 = call.Year2019,
            Year2020 = call.Year2020,
            Year2021 = call.Year2021,
            Year2022 = call.Year2022,
            Year2023 = call.Year2023,
            Month1_2024 = call.Month1_2024,
            Month2_2024 = call.Month2_2024,
            Month3_2024 = call.Month3_2024,
            Month4_2024 = call.Month4_2024,
            Month5_2024 = call.Month5_2024,
            Month6_2024 = call.Month6_2024,
            Month7_2024 = call.Month7_2024,
            Month8_2024 = call.Month8_2024,
            Month9_2024 = call.Month9_2024,
            Month10_2024 = call.Month10_2024,
            Month11_2024 = call.Month11_2024,
            Month12_2024 = call.Month12_2024,
            Month1_2025 = call.Month1_2025,
            Month2_2025 = call.Month2_2025,
            Month3_2025 = call.Month3_2025,
            KanalId = call.KanalId,
            Status = call.Status.ToString(),

            ExcludedBy = call.ExcludedBy,
            ExcludedByName = user?.FullName,
            Conclusion = call.Conclusion.ToString(),
            LastStatusUpdate = call.LastStatusUpdate,
            NextCall = call.NextCall,
            isDone = call.isDone,
            Note = call.Note,
            isDeleted = call.isDeleted,
            CreateAt = call.CreateAt,
        };
        return new BaseResponse<GetCallDto>(newCall);
    }


    public async Task<BaseResponse<ICollection<GetCallDto>>> GetRandomCallsByVoen(long kanalId)
    {
        ICollection<GetCallDto> cachedCalls;
        if (!_memoryCache.TryGetValue("RandomCalls", out cachedCalls) || !cachedCalls.Any())
        {
            var thresholdDate = DateTime.Now;

            var calls = await _db.Calls
                .Where(x => !x.isDeleted && x.KanalId == kanalId && (x.LastStatusUpdate == null || x.LastStatusUpdate <= thresholdDate))
                .ToListAsync();

            if (!calls.Any())
                return new BaseResponse<ICollection<GetCallDto>>(null, false, "No eligible calls available.");

            var groupedByVoen = calls.GroupBy(x => x.VOEN).ToList();

            var random = new Random();
            var randomVoenGroup = groupedByVoen.OrderBy(_ => random.Next()).FirstOrDefault();

            if (randomVoenGroup == null)
                return new BaseResponse<ICollection<GetCallDto>>(null, false, "No calls found for the selected VOEN.");

            cachedCalls = randomVoenGroup.Select(call => new GetCallDto
            {
                Id = call.id,
                VOEN = call.VOEN,
                InvoiceNumber = call.InvoiceNumber,
                Subject = call.Subject,
                District = call.District,
                PermissionStartDate = call.PermissionStartDate,
                PermissionEndDate = call.PermissionEndDate,
                Street = call.Street,
                Phone = call.Phone,
                LegalName = call.LegalName,
                TotalDebt = call.TotalDebt,
                Year2018 = call.Year2018,
                Year2019 = call.Year2019,
                Year2020 = call.Year2020,
                Year2021 = call.Year2021,
                Year2022 = call.Year2022,
                Year2023 = call.Year2023,
                Month1_2024 = call.Month1_2024,
                Month2_2024 = call.Month2_2024,
                Month3_2024 = call.Month3_2024,
                Month4_2024 = call.Month4_2024,
                Month5_2024 = call.Month5_2024,
                Month6_2024 = call.Month6_2024,
                Month7_2024 = call.Month7_2024,
                Month8_2024 = call.Month8_2024,
                Month9_2024 = call.Month9_2024,
                Month10_2024 = call.Month10_2024,
                Month11_2024 = call.Month11_2024,
                Month12_2024 = call.Month12_2024,
                Month1_2025 = call.Month1_2025,
                Month2_2025 = call.Month2_2025,
                Month3_2025 = call.Month3_2025,
                KanalId = call.KanalId,
                Status = call.Status.ToString(),
            }).ToList();

            _memoryCache.Set("RandomCalls", cachedCalls);
        }

        return new BaseResponse<ICollection<GetCallDto>>(cachedCalls);
    }




    //public async Task<BaseResponse<PagedResponse<GetCallDto>>> FindAsync(string query, int pageNumber, int pageSize)
    //{
    //    var callsQuery = _db.Set<Calls>().AsQueryable();

    //    if (!string.IsNullOrWhiteSpace(query))
    //    {
    //        callsQuery = callsQuery.Where(c =>
    //            c.EntrepreneurName.Contains(query) ||
    //            c.LegalName.Contains(query) ||
    //            c.VOEN.Contains(query) ||
    //            c.Phone.Contains(query));
    //    }

    //    var totalItems = await callsQuery.CountAsync();
    //    var calls = await callsQuery
    //        .Skip((pageNumber - 1) * pageSize)
    //        .Take(pageSize)
    //        .Select(c => new GetCallDto
    //        {
    //            id = c.id,
    //            isDelete = c.isDeleted,
    //            Status = c.Status,
    //            KanalId = c.KanalId,
    //            EntrepreneurName = c.EntrepreneurName,
    //            LegalName = c.LegalName,
    //            VOEN = c.VOEN,
    //            PermissionStartDate = c.PermissionStartDate,
    //            PermissionNumber = c.PermissionNumber,
    //            Address = c.Address,
    //            Phone = c.Phone,
    //            UserId = c.UserId,
    //            CreateAt = DateTime.UtcNow,
    //            Note = c.Note,
    //            LastStatusUpdate = c.LastStatusUpdate,
    //            Conclusion = c.Conclusion,
    //            isDone = c.isDone,
    //            NextCall = c.NextCall,

    //        })
    //        .ToListAsync();

    //    var response = new PagedResponse<GetCallDto>
    //    {
    //        Items = calls,
    //        TotalCount = totalItems,
    //        CurrentPage = pageNumber,
    //        PageSize = pageSize
    //    };

    //    return new BaseResponse<PagedResponse<GetCallDto>>(response);
    //}


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
            Id = call.id,
            VOEN = call.VOEN,
            InvoiceNumber = call.InvoiceNumber,
            Subject = call.Subject,
            District = call.District,
            PermissionStartDate = call.PermissionStartDate,
            PermissionEndDate = call.PermissionEndDate,
            Street = call.Street,
            Phone = call.Phone,
            isDeleted = call.isDeleted,
            CreateAt = call.CreateAt,
            LegalName = call.LegalName,
            TotalDebt = call.TotalDebt,
            Year2018 = call.Year2018,
            Year2019 = call.Year2019,
            Year2020 = call.Year2020,
            Year2021 = call.Year2021,
            Year2022 = call.Year2022,
            Year2023 = call.Year2023,
            Month1_2024 = call.Month1_2024,
            Month2_2024 = call.Month2_2024,
            Month3_2024 = call.Month3_2024,
            Month4_2024 = call.Month4_2024,
            Month5_2024 = call.Month5_2024,
            Month6_2024 = call.Month6_2024,
            Month7_2024 = call.Month7_2024,
            Month8_2024 = call.Month8_2024,
            Month9_2024 = call.Month9_2024,
            Month10_2024 = call.Month10_2024,
            Month11_2024 = call.Month11_2024,
            Month12_2024 = call.Month12_2024,
            Month1_2025 = call.Month1_2025,
            Month2_2025 = call.Month2_2025,
            Month3_2025 = call.Month3_2025,
            KanalId = call.KanalId,
            Status = call.Status.ToString(),
        };

        return new BaseResponse<GetCallDto>(newCall);
    }


    public async Task<BaseResponse<GetCallDto>> Update(long id, UpdateCallDto dto)
    {
        if (id <= 0)
            return new BaseResponse<GetCallDto>(null, false, "Id cant be 0.");

        var call = await _db.Calls.SingleOrDefaultAsync(x => x.id == id && !x.isDeleted);

        if (call == null)
            return new BaseResponse<GetCallDto>(null, false, "Call cant be NULL.");



        call.LegalName = dto.LegalName;
        call.VOEN = dto.VOEN;
        call.PermissionStartDate = dto.PermissionStartDate;
        call.PermissionEndDate = dto.PermissionEndDate;
        call.InvoiceNumber = dto.InvoiceNumber;
        call.Subject = dto.Subject;
        call.District = dto.District;
        call.Street = dto.Street;
        call.Phone = dto.Phone;


        _db.Calls.Update(call);
        await _db.SaveChangesAsync();

        var newCall = new GetCallDto()
        {
            Id = call.id,
            VOEN = call.VOEN,
            InvoiceNumber = call.InvoiceNumber,
            Subject = call.Subject,
            District = call.District,
            PermissionStartDate = call.PermissionStartDate,
            PermissionEndDate = call.PermissionEndDate,
            Street = call.Street,
            Phone = call.Phone,
            isDeleted = call.isDeleted,
            CreateAt = call.CreateAt,
            LegalName = call.LegalName,
            TotalDebt = call.TotalDebt,
            Year2018 = call.Year2018,
            Year2019 = call.Year2019,
            Year2020 = call.Year2020,
            Year2021 = call.Year2021,
            Year2022 = call.Year2022,
            Year2023 = call.Year2023,
            Month1_2024 = call.Month1_2024,
            Month2_2024 = call.Month2_2024,
            Month3_2024 = call.Month3_2024,
            Month4_2024 = call.Month4_2024,
            Month5_2024 = call.Month5_2024,
            Month6_2024 = call.Month6_2024,
            Month7_2024 = call.Month7_2024,
            Month8_2024 = call.Month8_2024,
            Month9_2024 = call.Month9_2024,
            Month10_2024 = call.Month10_2024,
            Month11_2024 = call.Month11_2024,
            Month12_2024 = call.Month12_2024,
            Month1_2025 = call.Month1_2025,
            Month2_2025 = call.Month2_2025,
            Month3_2025 = call.Month3_2025,
            KanalId = call.KanalId,
            Status = call.Status.ToString(),
        };

        return new BaseResponse<GetCallDto>(newCall);
    }


    public async Task<BaseResponse<GetCallDto>> Exclude(long id, ExcludeCallDto dto)
    {
        if (id <= 0)
            return new BaseResponse<GetCallDto>(null, false, "Id cant be 0.");

        var call = await _db.Calls.SingleOrDefaultAsync(x => x.id == id && !x.isDeleted);

        if (call == null)
            return new BaseResponse<GetCallDto>(null, false, "Call cant be NULL.");

        call.ExcludedBy = dto.ExcludedBy;
        call.LastStatusUpdate = DateTime.Now;
        call.Conclusion = dto.Conclusion;
        call.Status = CallStatus.YenidənZənq;

        switch (dto.Conclusion)
        {
            case CallResult.Razılaşdı:
                call.Conclusion = CallResult.Razılaşdı;
                call.isDone = true;
                if (string.IsNullOrEmpty(dto.Note))
                    return new BaseResponse<GetCallDto>(null, false, "Необходимо указать номер договора.");
                call.Note = dto.Note;
                break;

            case CallResult.ImtinaEtdi:
                call.Conclusion = CallResult.ImtinaEtdi;
                if (string.IsNullOrEmpty(dto.Note))
                    return new BaseResponse<GetCallDto>(null, false, "Необходимо указать причину отказа.");
                call.Note = dto.Note;
                break;

            case CallResult.NömrəSəhvdir:
                call.Conclusion = CallResult.NömrəSəhvdir;
                call.Note = dto.Note;
                break;

            case CallResult.ZəngÇatmır:
                call.Conclusion = CallResult.ZəngÇatmır;
                call.Note = dto.Note;
                break;

            case CallResult.YenidənZəng:
                call.Conclusion = CallResult.YenidənZəng;
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

        if (_memoryCache.TryGetValue("RandomCalls", out ICollection<GetCallDto> cachedCalls))
        {
            var callToRemove = cachedCalls.FirstOrDefault(c => c.Id == id);
            if (callToRemove != null)
            {
                cachedCalls.Remove(callToRemove);
            }
        }

        var newCall = new GetCallDto()
        {
            Id = call.id,
            VOEN = call.VOEN,
            InvoiceNumber = call.InvoiceNumber,
            Subject = call.Subject,
            District = call.District,
            PermissionStartDate = call.PermissionStartDate,
            PermissionEndDate = call.PermissionEndDate,
            Street = call.Street,
            Phone = call.Phone,
            isDeleted = call.isDeleted,
            CreateAt = call.CreateAt,
            LegalName = call.LegalName,
            TotalDebt = call.TotalDebt,
            Year2018 = call.Year2018,
            Year2019 = call.Year2019,
            Year2020 = call.Year2020,
            Year2021 = call.Year2021,
            Year2022 = call.Year2022,
            Year2023 = call.Year2023,
            Month1_2024 = call.Month1_2024,
            Month2_2024 = call.Month2_2024,
            Month3_2024 = call.Month3_2024,
            Month4_2024 = call.Month4_2024,
            Month5_2024 = call.Month5_2024,
            Month6_2024 = call.Month6_2024,
            Month7_2024 = call.Month7_2024,
            Month8_2024 = call.Month8_2024,
            Month9_2024 = call.Month9_2024,
            Month10_2024 = call.Month10_2024,
            Month11_2024 = call.Month11_2024,
            Month12_2024 = call.Month12_2024,
            Month1_2025 = call.Month1_2025,
            Month2_2025 = call.Month2_2025,
            Month3_2025 = call.Month3_2025,
            KanalId = call.KanalId,
            Status = call.Status.ToString(),

            Conclusion = call.Conclusion.ToString(),
            NextCall = call.NextCall,
            ExcludedBy = call.ExcludedBy,
            LastStatusUpdate = call.LastStatusUpdate,
            isDone = call.isDone,
            Note = call.Note,
        };

        return new BaseResponse<GetCallDto>(newCall);
    }

}
