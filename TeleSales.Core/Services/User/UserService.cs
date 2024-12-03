using Microsoft.EntityFrameworkCore;
using TeleSales.Core.Dto.User;
using TeleSales.Core.Interfaces.User;
using TeleSales.Core.Response;
using TeleSales.DataProvider.Context;
using TeleSales.DataProvider.Entities;

namespace TeleSales.Core.Services.User;

public class UserService : IUserService
{
    private readonly ApplicationDbContext _db;
    public UserService(ApplicationDbContext db)
    {
        _db = db;
    }
    public async Task<BaseResponse<GetUserDto>> Create(CreateUserDto dto)
    {
        var user = new Users()
        {
            Email = dto.Email,
            FullName = dto.FirstName + " " + dto.LastName,
            Password = dto.Password,
            CreateAt = DateTime.UtcNow
        };
        await _db.Users.AddAsync(user);
        await _db.SaveChangesAsync();

        var newUser = new GetUserDto()
        {
            id = user.id,
            CreateAt = user.CreateAt,
            Email = user.Email,
            FullName = user.FullName,
            isDeleted = user.isDeleted,
            Password = user.Password
        };

        return new BaseResponse<GetUserDto>(newUser);
    }


    public async Task<BaseResponse<ICollection<GetUserDto>>> GetAll()
    {
        var users = _db.Users.Where(x => !x.isDeleted);

        var userDtos = users.Select(user => new GetUserDto
        {
            id = user.id,
            isDeleted = user.isDeleted,
            CreateAt = user.CreateAt,
            Email = user.Email,
            FullName = user.FullName,
            Password = user.Password,
        }).ToList();

        return new BaseResponse<ICollection<GetUserDto>>(userDtos);
    }


    public async Task<BaseResponse<GetUserDto>> GetById(long id)
    {
        if (id == 0)
            return new BaseResponse<GetUserDto>(null, false, "Id cant be 0.");

        var user = await _db.Users.SingleOrDefaultAsync(x => x.id == id && !x.isDeleted);

        if (user == null)
        {
            return new BaseResponse<GetUserDto>("User not found");
        }

        var newUser = new GetUserDto()
        {
            id = user.id,
            CreateAt = user.CreateAt,
            Email = user.Email,
            FullName = user.FullName,
            isDeleted = user.isDeleted,
            Password = user.Password
        };

        return new BaseResponse<GetUserDto>(newUser);
    }


    public async Task<BaseResponse<GetUserDto>> Update(long id, UpdateUserDto dto)
    {
        if (id == 0)
            return new BaseResponse<GetUserDto>(null, false, "Id cant be 0.");

        var user = await _db.Users.SingleOrDefaultAsync(x => x.id == id && !x.isDeleted);

        if (user == null)
        {
            return new BaseResponse<GetUserDto>("User not found");
        }

        user.Email = dto.Email;
        user.FullName = dto.FirstName + " " + dto.LastName;
        user.Password = dto.Password;

        _db.Users.Update(user);
        await _db.SaveChangesAsync();

        var newUser = new GetUserDto()
        {
            id = user.id,
            CreateAt = user.CreateAt,
            Email = user.Email,
            FullName = user.FullName,
            isDeleted = user.isDeleted,
            Password = user.Password
        };

        return new BaseResponse<GetUserDto>(newUser);
    }


    public async Task<BaseResponse<GetUserDto>> Remove(long id)
    {
        if (id == 0)
            return new BaseResponse<GetUserDto>(null, false, "Id cant be 0.");

        var user = await _db.Users.SingleOrDefaultAsync(x => x.id == id);

        if (user == null)
        {
            return new BaseResponse<GetUserDto>("User not found");
        }

        user.isDeleted = true;
        _db.Users.Update(user);
        await _db.SaveChangesAsync();

        var newUser = new GetUserDto()
        {
            id = user.id,
            CreateAt = user.CreateAt,
            Email = user.Email,
            FullName = user.FullName,
            isDeleted = user.isDeleted,
            Password = user.Password
        };

        return new BaseResponse<GetUserDto>(newUser);
    }
}
