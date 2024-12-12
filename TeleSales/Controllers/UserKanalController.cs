using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TeleSales.Core.Dto.UserKanal;
using TeleSales.Core.Interfaces.UserKanal;

namespace TeleSales.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UserKanalController : ControllerBase
{
    private readonly IUserKanalService _service;

    public UserKanalController(IUserKanalService service)
    {
        _service = service;
    }


    /// <summary>
    /// Получает список каналов по ID пользователя.
    /// </summary>
    /// <param name="userId">ID пользователя.</param>
    /// <returns>Список каналов, в которых состоит указанный пользователь.</returns>
    [HttpGet("ByUser")]
    [Authorize(Policy = "Operator")]

    public async Task<IActionResult> GetAllByUserId(long userId)
    {
        var data = await _service.GetAllByUserId(userId);
        if (data.Success)
            return Ok(data);
        return BadRequest();
    }
}
