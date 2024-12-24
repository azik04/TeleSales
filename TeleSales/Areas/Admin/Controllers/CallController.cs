using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TeleSales.Core.Dto.Call;
using TeleSales.Core.Interfaces.Call;

namespace TeleSales.Areas.Admin.Controllers;

[Route("api/Admin/[controller]")]
[ApiController]
[Area("Admin")]
public class CallController : ControllerBase
{
    private readonly ICallService _service;
    public CallController(ICallService service)
    {
        _service = service;
    }


    /// <summary>
    /// Update a call by its ID
    /// </summary>
    /// <param name="id">The call ID</param>
    /// <param name="dto">The updated call details</param>
    /// <returns>The updated call data or an error message</returns>
    [HttpPut("{id}")]
    [Authorize(Policy = "Admin")]

    public async Task<IActionResult> Update(long id, UpdateCallDto dto)
    {
        var res = await _service.Update(id, dto);
        if (!res.Success)
            return BadRequest(res.Message);

        return Ok(res);
    }


    /// <summary>
    /// Remove a call by its ID
    /// </summary>
    /// <param name="id">The call ID</param>
    /// <returns>A success message or an error message</returns>
    [HttpDelete("{id}")]
    [Authorize(Policy = "Admin")]

    public async Task<IActionResult> Remove(long id)
    {
        var res = await _service.Remove(id);
        if (!res.Success)
            return BadRequest(res.Message);

        return Ok(res);
    }


    /// <summary>
    /// Import calls from an Excel file
    /// </summary>
    /// <param name="file">The Excel file containing call data</param>
    /// <returns>A response with the imported call data or error message</returns>
    [HttpPost("import")]
    [Authorize(Policy = "Admin")]

    public async Task<IActionResult> ImportFromExcel(IFormFile file, long kanalId)
    {
        if (file == null || file.Length == 0)
            return BadRequest("No file uploaded.");

        using (var fileStream = file.OpenReadStream())
        {
            var response = await _service.ImportFromExcelAsync(fileStream, kanalId);

            if (response.Success)
                return Ok(response);
            return BadRequest(response.Message);
        }
    }


    /// <summary>
    /// Create a new call record
    /// </summary>
    /// <param name="dto">The details for the new call</param>
    /// <param name="kanalId">The channel to which the call belongs</param>
    /// <returns>A response with the created call data or an error message</returns>
    [HttpPost]
    [Authorize(Policy = "Admin")]

    public async Task<IActionResult> Create(CreateCallDto dto, long kanalId)
    {
        var res = await _service.Create(dto);
        if (!res.Success)
            return BadRequest(res.Message);

        return Ok(res);
    }
}
