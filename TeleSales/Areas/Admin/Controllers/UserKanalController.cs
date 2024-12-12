using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TeleSales.Core.Dto.UserKanal;
using TeleSales.Core.Interfaces.UserKanal;

namespace TeleSales.Areas.Admin.Controllers;

[Route("api/Admin/[controller]")]
[ApiController]
[Area("Admin")]

public class UserKanalController : ControllerBase
{
    private readonly IUserKanalService _service;

    public UserKanalController(IUserKanalService service)
    {
        _service = service;
    }


    /// <summary>
    /// Добавляет пользователя в канал.
    /// </summary>
    /// <param name="dto">Данные для добавления пользователя в канал.</param>
    /// <returns>Результат операции (успех или ошибка).</returns>
    [HttpPost]
    [Authorize(Policy = "Admin")]
    public async Task<IActionResult> AddToChanelAsync(CreateUserKanalDto dto)
    {
        var data = await _service.AddToChanelAsync(dto);
        if (data.Success)
            return Ok(data);
        return BadRequest();
    }


    /// <summary>
    /// Получает список пользователей по ID канала.
    /// </summary>
    /// <param name="kanalId">ID канала.</param>
    /// <returns>Список пользователей в указанном канале.</returns>
    [HttpGet("ByKanal")]
    [Authorize(Policy = "Admin")]
    public async Task<IActionResult> GetAllByKanalId(long kanalId)
    {
        var data = await _service.GetAllByKanalId(kanalId);
        if (data.Success)
            return Ok(data);
        return BadRequest();
    }


    /// <summary>
    /// Удаляет пользователя из канала.
    /// </summary>
    /// <param name="userId">ID пользователя.</param>
    /// <param name="kanalId">ID канала.</param>
    /// <returns>Результат операции (успех или ошибка).</returns>
    [HttpDelete]
    [Authorize(Policy = "Admin")]
    public async Task<IActionResult> RemoveUserFromKanalAsync(long userId, long kanalId)
    {
        var data = await _service.RemoveUserKanalAsync(userId, kanalId);
        if (data.Success)
            return Ok(data);
        return BadRequest();
    }
}
