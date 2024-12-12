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
    /// Get all calls by channel ID with pagination
    /// </summary>
    /// <param name="kanalId">The channel ID</param>
    /// <param name="pageNumber">The page number for pagination</param>
    /// <param name="pageSize">The number of records per page</param>
    /// <returns>A paginated list of calls associated with the given channel</returns>
    [HttpGet("{kanalId}/Kanal")]
    [Authorize(Policy = "Admin")]
    public async Task<IActionResult> GetAllByKanal(long kanalId, int pageNumber, int pageSize)
    {
        var res = await _service.GetAllByKanal(kanalId, pageNumber, pageSize);
        if (!res.Success)
            return BadRequest(res.Message);

        return Ok(res);
    }

    /// <summary>
    /// Get all calls that are not excluded
    /// </summary>
    /// <param name="kanalId">The channel ID</param>
    /// <param name="pageNumber">The page number for pagination</param>
    /// <param name="pageSize">The number of records per page</param>
    /// <returns>A paginated list of calls that are not excluded</returns>
    [HttpGet("NotExcluded")]
    [Authorize(Policy = "Admin")]
    public async Task<IActionResult> GetAllNotExcluded(long kanalId, int pageNumber, int pageSize)
    {
        var res = await _service.GetAllNotExcluded(kanalId, pageNumber, pageSize);
        if (!res.Success)
            return BadRequest(res.Message);

        return Ok(res);
    }

    /// <summary>
    /// Get all calls that are excluded
    /// </summary>
    /// <param name="kanalId">The channel ID</param>
    /// <param name="pageNumber">The page number for pagination</param>
    /// <param name="pageSize">The number of records per page</param>
    /// <returns>A paginated list of excluded calls</returns>
    [HttpGet("Excluded")]
    [Authorize(Policy = "Admin")]
    public async Task<IActionResult> GetAllExcluded(long kanalId, int pageNumber, int pageSize)
    {
        var res = await _service.GetAllExcluded(kanalId, pageNumber, pageSize);
        if (!res.Success)
            return BadRequest(res.Message);

        return Ok(res);
    }


    /// <summary>
    /// Update a call by its ID
    /// </summary>
    /// <param name="id">The call ID</param>
    /// <param name="dto">The updated call details</param>
    /// <returns>The updated call data or an error message</returns>
    [HttpPut("{id}")]
    [Authorize(Policy = "Operator")]

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
    [Authorize(Policy = "Operator")]

    public async Task<IActionResult> Remove(long id)
    {
        var res = await _service.Remove(id);
        if (!res.Success)
            return BadRequest(res.Message);

        return Ok(res);
    }
}
