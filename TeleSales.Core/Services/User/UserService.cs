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
            CreateAt = DateTime.UtcNow,
            Role = DataProvider.Enums.Role.Operator,
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
            Role = user.Role,
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
            Role = user.Role,
        }).ToList();

        return new BaseResponse<ICollection<GetUserDto>>(userDtos);
    }

    public async Task<BaseResponse<ICollection<GetUserDto>>> GetAllUser()
    {
        var users = _db.Users.Where(x => !x.isDeleted && x.Role == DataProvider.Enums.Role.Operator);

        var userDtos = users.Select(user => new GetUserDto
        {
            id = user.id,
            isDeleted = user.isDeleted,
            CreateAt = user.CreateAt,
            Email = user.Email,
            FullName = user.FullName,
            Role = user.Role,
        }).ToList();

        return new BaseResponse<ICollection<GetUserDto>>(userDtos);
    }

    public async Task<BaseResponse<ICollection<GetUserDto>>> GetAllAdmin()
    {
        var users = _db.Users.Where(x => !x.isDeleted  && x.Role == DataProvider.Enums.Role.Admin);

        var userDtos = users.Select(user => new GetUserDto
        {
            id = user.id,
            isDeleted = user.isDeleted,
            CreateAt = user.CreateAt,
            Email = user.Email,
            FullName = user.FullName,
            Role = user.Role,
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
            Role = user.Role,
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

        _db.Users.Update(user);
        await _db.SaveChangesAsync();

        var newUser = new GetUserDto()
        {
            id = user.id,
            CreateAt = user.CreateAt,
            Email = user.Email,
            FullName = user.FullName,
            isDeleted = user.isDeleted,
            Role = user.Role,
        };

        return new BaseResponse<GetUserDto>(newUser);
    }


    public async Task<BaseResponse<GetUserDto>> ChangePassword(long id, ChangePasswordDto dto)
    {
        var user = await _db.Users.SingleOrDefaultAsync(x => x.id == id && !x.isDeleted);

        if (user == null)
            return new BaseResponse<GetUserDto>("User not found.");

        if (dto.OldPassword != user.Password)
            return new BaseResponse<GetUserDto>("Incorrect old password.");

        if (dto.NewPassword != dto.ConfirmNewPassword)
            return new BaseResponse<GetUserDto>("New password and confirmation do not match.");

        if (dto.NewPassword == dto.OldPassword)
            return new BaseResponse<GetUserDto>("New password cannot be the same as the old password.");

        user.Password = dto.NewPassword;

        _db.Users.Update(user);
        await _db.SaveChangesAsync();

        var newUser = new GetUserDto()
        {
            id = user.id,
            CreateAt = user.CreateAt,
            Email = user.Email,
            FullName = user.FullName,
            isDeleted = user.isDeleted,
            Role = user.Role,
        };

        return new BaseResponse<GetUserDto>(newUser);
    }



    public async Task<BaseResponse<GetUserDto>> ChangeRole(long id)
    {
        if (id == 0)
            return new BaseResponse<GetUserDto>(null, false, "Id cannot be 0.");

        var user = await _db.Users.SingleOrDefaultAsync(x => x.id == id && !x.isDeleted);
        if (user == null)
            return new BaseResponse<GetUserDto>(null, false, "User not found.");

        user.Role = user.Role == DataProvider.Enums.Role.Operator
            ? DataProvider.Enums.Role.Admin
            : DataProvider.Enums.Role.Operator;

    
        _db.Users.Update(user);
        await _db.SaveChangesAsync();

        var newUser = new GetUserDto()
        {
            id = user.id,
            CreateAt = user.CreateAt,
            Email = user.Email,
            FullName = user.FullName,
            isDeleted = user.isDeleted,
            Role = user.Role,
        };

        return new BaseResponse<GetUserDto>(newUser, true, "Role updated successfully.");
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
            Role = user.Role,
        };

        return new BaseResponse<GetUserDto>(newUser);
    }
}
