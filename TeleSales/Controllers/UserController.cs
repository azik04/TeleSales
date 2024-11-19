using Microsoft.AspNetCore.Mvc;
using TeleSales.Core.Dto.User;
using TeleSales.Core.Interfaces.User;

namespace TeleSales.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UserController : ControllerBase
{
    private readonly IUserService _service;
    public UserController(IUserService service)
    {
        _service = service;
    }


    [HttpPost]
    public async Task<IActionResult> Create(CreateUserDto dto)
    {
        var res = await _service.Create(dto);
        if (!res.Success)
            return BadRequest(res.Message);

        return Ok(res);
    }


    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(long id)
    {
        var res = await _service.GetById(id);
        if (!res.Success)
            return BadRequest(res.Message);

        return Ok(res);
    }


    [HttpPut("{id}")]
    public async Task<IActionResult> Update(long id, UpdateUserDto dto)
    {
        var res = await _service.Update(id, dto);
        if (!res.Success)
            return BadRequest(res.Message);

        return Ok(res);
    }


    [HttpDelete("{id}")]
    public async Task<IActionResult> Remove(long id)
    {
        var res = await _service.Remove(id);
        if (!res.Success)
            return BadRequest(res.Message);

        return Ok(res);
    }
}

