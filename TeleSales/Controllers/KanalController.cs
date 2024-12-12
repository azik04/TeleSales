using Microsoft.AspNetCore.Authorization;
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
    /// Get all available Kanals
    /// </summary>
    /// <returns>A response with a list of all Kanals or error message</returns>
    [HttpGet]
    [Authorize(Policy = "Operator")]

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
    [Authorize(Policy = "Operator")]

    public async Task<IActionResult> GetById(long id)
    {
        var res = await _service.GetById(id);
        if (!res.Success)
            return BadRequest(res.Message);

        return Ok(res);
    }
}
