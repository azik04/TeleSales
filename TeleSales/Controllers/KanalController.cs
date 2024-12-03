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
    /// Get all available Kanals
    /// </summary>
    /// <returns>A response with a list of all Kanals or error message</returns>
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var res = await _service.GetAll();
        if (!res.Success)
            return BadRequest(res.Message);

        return Ok(res);
    }

    /// <summary>
    /// Get a Kanal by its ID
    /// </summary>
    /// <param name="id">The ID of the Kanal</param>
    /// <returns>A response with the Kanal data or error message</returns>
    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(long id)
    {
        var res = await _service.GetById(id);
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
    public async Task<IActionResult> Remove(long id)
    {
        var res = await _service.Remove(id);
        if (!res.Success)
            return BadRequest(res.Message);

        return Ok(res);
    }
}
