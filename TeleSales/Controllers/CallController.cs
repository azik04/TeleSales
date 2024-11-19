using Microsoft.AspNetCore.Mvc;
using TeleSales.Core.Dto.Call;
using TeleSales.Core.Interfaces.Call;
using TeleSales.Core.Services.Call;

namespace TeleSales.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CallController : ControllerBase
{
    private readonly ICallService _service;
    public CallController(ICallService service)
    {
        _service = service;
    }


    /// <summary>
    /// Import calls from an Excel file
    /// </summary>
    /// <param name="file">The Excel file containing call data</param>
    /// <returns>A response with the imported call data or error message</returns>
    [HttpPost("import")]
    public async Task<IActionResult> ImportFromExcel(IFormFile file)
    {
        if (file == null || file.Length == 0)
            return BadRequest("No file uploaded.");
          
        using (var fileStream = file.OpenReadStream())
        {
            var response = await _service.ImportFromExcelAsync(fileStream);

            if (response.Success)
                return Ok(response);
            return BadRequest(response.Message);            
        }
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateCallDto dto)
    {
        var res = await _service.Create(dto);
        if (!res.Success)
            return BadRequest(res.Message);

        return Ok(res);
    }
    [HttpGet("{kanalId}/Kanal")]
    public async Task<IActionResult> GetAllByKanal(long kanalId, int pageNumber, int pageSize)
    {
        var res = await _service.GetAllByKanal(kanalId, pageNumber, pageSize);
        if (!res.Success)
            return BadRequest(res.Message);

        return Ok(res);
    }

    [HttpGet("{kanalId}/Kanal/User/{userId}")]
    public async Task<IActionResult> GetAllByKanalAndUser(long kanalId, long userId, int pageNumber, int pageSize)
    {
        var res = await _service.GetAllByKanalAndUser(kanalId, userId, pageNumber, pageSize);
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
    public async Task<IActionResult> Update(long id, UpdateCallDto dto)
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

