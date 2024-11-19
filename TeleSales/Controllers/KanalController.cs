using Microsoft.AspNetCore.Mvc;
using TeleSales.Core.Dto.Kanal;
using TeleSales.Core.Interfaces.Kanal;

namespace TeleSales.Controllers;

[Route("api/[controller]")]
[ApiController]
public class KanalController : ControllerBase
{
    private readonly IKanalService _service;
    public KanalController(IKanalService service)
    {
        _service = service;
    }


    [HttpPost]
    public async Task<IActionResult> Create(CreateKanalDto dto)
    {
        var res = await _service.Create(dto);
        if (!res.Success)
            return BadRequest(res.Message);

        return Ok(res);
    }
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var res = await _service.GetAll();
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
    public async Task<IActionResult> Update(long id, UpdateKanalDto dto)
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

