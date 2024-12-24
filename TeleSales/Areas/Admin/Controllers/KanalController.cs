using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TeleSales.Core.Dto.Kanal;
using TeleSales.Core.Interfaces.Kanal;

namespace TeleSales.Areas.Admin.Controllers;

[Route("api/Admin/[controller]")]
[ApiController]
[Area("Admin")]
public class KanalController : ControllerBase
{
    private readonly IKanalService _service;

    public KanalController(IKanalService service)
    {
        _service = service;
    }

    /// <summary>
    /// Create a new Kanal
    /// </summary>
    /// <param name="dto">The data for creating a new Kanal</param>
    /// <returns>A response with the created Kanal data or error message</returns>
    [HttpPost]

    public async Task<IActionResult> Create(CreateKanalDto dto)
    {
        var res = await _service.Create(dto);
        if (!res.Success)
            return BadRequest(res.Message);

        return Ok(res);
    }


    /// <summary>
    /// Update the Kanal by its ID
    /// </summary>
    /// <param name="id">The ID of the Kanal to update</param>
    /// <param name="dto">The updated data for the Kanal</param>
    /// <returns>A response with the updated Kanal data or error message</returns>
    [HttpPut("{id}")]
    [Authorize(Policy = "Admin")]

    public async Task<IActionResult> Update(long id, UpdateKanalDto dto)
    {
        var res = await _service.Update(id, dto);
        if (!res.Success)
            return BadRequest(res.Message);

        return Ok(res);
    }

    /// <summary>
    /// Remove a Kanal by its ID
    /// </summary>
    /// <param name="id">The ID of the Kanal to remove</param>
    /// <returns>A response indicating whether the Kanal was successfully removed or an error message</returns>
    [HttpDelete("{id}")]
    [Authorize(Policy = "Admin")]

    public async Task<IActionResult> Remove(long id)
    {
        var res = await _service.Remove(id);
        if (!res.Success)
            return BadRequest(res.Message);

        return Ok(res);
    }
}
