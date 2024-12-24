using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TeleSales.Core.Dto.Call;
using TeleSales.Core.Dto.CallCenter;
using TeleSales.Core.Interfaces.Call;
using TeleSales.Core.Interfaces.CallCenter;

namespace TeleSales.Areas.Admin.Controllers;

[Route("api/Admin/[controller]")]
[ApiController]
[Area("Admin")]
public class CallCenterController : ControllerBase
{
    private readonly ICallCenterService _service;
    public CallCenterController(ICallCenterService service)
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

    public async Task<IActionResult> Update(long id, UpdateCallCenterDto dto)
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


 
}
