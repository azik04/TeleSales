using Microsoft.EntityFrameworkCore;
using TeleSales.Core.Dto.Kanal;
using TeleSales.Core.Dto.User;
using TeleSales.Core.Dto.UserKanal;
using TeleSales.Core.Interfaces.UserKanal;
using TeleSales.Core.Response;
using TeleSales.DataProvider.Context;
using TeleSales.DataProvider.Entities;

namespace TeleSales.Core.Services.UserKanal;

public class UserKanalService : IUserKanalService
{
    private readonly ApplicationDbContext _db;
    public UserKanalService(ApplicationDbContext db)
    {
        _db = db; 
    }
    public async Task<BaseResponse<GetUserKanalDto>> AddToChanelAsync(CreateUserKanalDto dto)
    {
        var kanal = await _db.Kanals.SingleOrDefaultAsync(x => x.id == dto.KanalId);
        var user = await _db.Users.SingleOrDefaultAsync(x => x.id == dto.UserId);

        if (user == null || kanal == null)
            return new BaseResponse<GetUserKanalDto>(null, false, "User or Kanal not found.");

        var existingUser = await _db.UserKanals
            .SingleOrDefaultAsync(x => x.KanalId == dto.KanalId && x.UserId == dto.UserId);

        if (existingUser != null)
        {
            if (existingUser.isDeleted)
            {
                existingUser.isDeleted = false;
                existingUser.CreateAt = DateTime.Now; 

                await _db.SaveChangesAsync();

                var restoredDto = new GetUserKanalDto
                {
                    KanalId = existingUser.KanalId,
                    UserId = existingUser.UserId,
                    UserEmail = user.Email,
                    KanalName = kanal.Name,
                    
                };

                return new BaseResponse<GetUserKanalDto>(restoredDto, true, "User successfully re-added to the channel.");
            }

            return new BaseResponse<GetUserKanalDto>(null, true, "User is already assigned to this channel.");
        }

        var userKanal = new UserKanals
        {
            KanalId = dto.KanalId,
            UserId = dto.UserId,
            CreateAt = DateTime.Now
        };

        await _db.UserKanals.AddAsync(userKanal);
        await _db.SaveChangesAsync();

        var newDto = new GetUserKanalDto
        {
            KanalId = userKanal.KanalId,
            UserId = userKanal.UserId,
            UserEmail = user.Email,
            KanalName = kanal.Name,
        };

        return new BaseResponse<GetUserKanalDto>(newDto, true, "User successfully added to the channel.");
    }



    public async Task<BaseResponse<ICollection<GetUserDto>>> GetAllByKanalId(long kanalId)
    {
        var users = await _db.UserKanals
            .Where(x => x.KanalId == kanalId && !x.isDeleted)
            .Include(x => x.Users) 
            .ToListAsync();

        if (!users.Any())
            return new BaseResponse<ICollection<GetUserDto>>(null, false, "Нет данных для данного KanalId.");

        var dto = users.Select(user => new GetUserDto
        {
            FullName = user.Users.FullName, 
            Email = user.Users.Email,      
            id = user.Users.id ,
            Role = user.Users.Role,
            CreateAt = user.CreateAt,
            isDeleted = user.isDeleted
        }).ToList();

        return new BaseResponse<ICollection<GetUserDto>>(dto, true, "");
    }

    public async Task<BaseResponse<ICollection<GetKanalDto>>> GetAllByUserId(long userId)
    {
        var kanals = await _db.UserKanals
        .Where(x => x.UserId == userId && !x.isDeleted)
        .Include(x => x.Kanals)
        .ToListAsync();

        if (!kanals.Any())
            return new BaseResponse<ICollection<GetKanalDto>>(null, false, "Нет данных для данного UserId.");

        var dto = kanals.Select(kanal => new GetKanalDto
        {
            id = kanal.Kanals.id,         
            Name = kanal.Kanals.Name,
            CreateAt = kanal.Kanals.CreateAt,
            Type = kanal.Kanals.Type.ToString(),
            isDeleted = kanal.Kanals.isDeleted
        }).ToList();

        return new BaseResponse<ICollection<GetKanalDto>>(dto, true, "");
    }

    

    public async Task<BaseResponse<GetUserKanalDto>> RemoveUserKanalAsync(long userId, long kanalId)
    {
        var userKanal = await _db.UserKanals.SingleOrDefaultAsync(x => x.KanalId == kanalId && x.UserId == userId && !x.isDeleted);

        if (userKanal == null)
            return new BaseResponse<GetUserKanalDto>(null, false, "");

        userKanal.isDeleted = true;
        _db.UserKanals.Update(userKanal);
        await _db.SaveChangesAsync();

        var dto = new GetUserKanalDto
        {
            KanalId = userKanal.KanalId,
            UserId = userKanal.UserId,
        };

        return new BaseResponse<GetUserKanalDto>(dto, true, "");
    }
}
