using Microsoft.EntityFrameworkCore;
using TeleSales.Core.Dto.Kanal;
using TeleSales.Core.Interfaces.Kanal;
using TeleSales.Core.Response;
using TeleSales.DataProvider.Context;
using TeleSales.DataProvider.Entities;

namespace TeleSales.Core.Services.Kanal;

public class KanalService : IKanalService
{
    private readonly ApplicationDbContext _db;
    public KanalService(ApplicationDbContext db)
    {
        _db = db;
    }


    public async Task<BaseResponse<GetKanalDto>> Create(CreateKanalDto dto)
    {
        var kanal = new Kanals()
        {
            Name = dto.Name,
            CreateAt = DateTime.UtcNow
        };

        await _db.Kanals.AddAsync(kanal);
        await _db.SaveChangesAsync();

        var newKanal = new GetKanalDto()
        {
            id = kanal.id,
            CreateAt = kanal.CreateAt,
            Name = kanal.Name,
            isDeleted = kanal.isDeleted,
        };

        return new BaseResponse<GetKanalDto>(newKanal);
    }

    public async Task<BaseResponse<ICollection<GetKanalDto>>> GetAll()
    {
        var kanals = _db.Kanals.Where(x => !x.isDeleted);

        var kanalDtos = kanals.Select(kanal => new GetKanalDto
        {
            id = kanal.id,
            isDeleted = kanal.isDeleted,
            CreateAt = kanal.CreateAt,
            Name = kanal.Name,
        }).ToList();

        return new BaseResponse<ICollection<GetKanalDto>>(kanalDtos);
    }

    public async Task<BaseResponse<GetKanalDto>> GetById(long id)
    {
        if (id == 0)
            return new BaseResponse<GetKanalDto>(null, false, "Id cant be 0.");

        var kanal = await _db.Kanals.SingleOrDefaultAsync(x => x.id == id && !x.isDeleted);
        
        if (kanal == null)
            return new BaseResponse<GetKanalDto>(null, false, "Kanal cant be NULL.");

        var newKanal = new GetKanalDto()
        {
            id = kanal.id,
            CreateAt = kanal.CreateAt,
            Name = kanal.Name,
            isDeleted = kanal.isDeleted,
        };

        return new BaseResponse<GetKanalDto>(newKanal);
    }

    public async Task<BaseResponse<GetKanalDto>> Remove(long id)
    {
        if (id == 0)
            return new BaseResponse<GetKanalDto>(null, false, "Id cant be 0.");

        var kanal = await _db.Kanals.SingleOrDefaultAsync(x => x.id == id && !x.isDeleted);
        
        if (kanal == null)
            return new BaseResponse<GetKanalDto>(null, false, "Kanal cant be NULL.");

        kanal.isDeleted = true;

        _db.Kanals.Update(kanal);
        await _db.SaveChangesAsync();

        var newKanal = new GetKanalDto()
        {
            id = kanal.id,
            CreateAt = kanal.CreateAt,
            Name = kanal.Name,
            isDeleted = kanal.isDeleted,
        };

        return new BaseResponse<GetKanalDto>(newKanal);
    }

    public async Task<BaseResponse<GetKanalDto>> Update(long id, UpdateKanalDto dto)
    {
        if (id == 0)
            return new BaseResponse<GetKanalDto>(null, false, "Id cant be 0.");

        var kanal = await _db.Kanals.SingleOrDefaultAsync(x => x.id == id && !x.isDeleted);

        if (kanal == null)
            return new BaseResponse<GetKanalDto>(null, false, "Kanal cant be NULL.");

        kanal.Name = dto.Name;

        _db.Kanals.Update(kanal);
        await _db.SaveChangesAsync();

        var newKanal = new GetKanalDto()
        {
            id = kanal.id,
            CreateAt = kanal.CreateAt,
            Name = kanal.Name,
            isDeleted = kanal.isDeleted,
        };

        return new BaseResponse<GetKanalDto>(newKanal);
    }
}
