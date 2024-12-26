using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TeleSales.Core.Dto.Call;
using TeleSales.Core.Dto.CallCenter;
using TeleSales.Core.Interfaces.CallCenter;

namespace TeleSales.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CallCenterController : ControllerBase
{
    private readonly ICallCenterService _service;
    public CallCenterController(ICallCenterService service)
    {
    _service = service; 
    }


    /// <summary>
    /// Export calls to an Excel file
    /// </summary>
    /// <param name="kanalId">The channel ID to filter calls</param>
    /// <returns>The Excel file containing the exported call data</returns>
    [HttpGet("ExportExcel")]
    [Authorize(Policy = "Viewer")]
    public async Task<IActionResult> ExportToExcelAsync(long kanalId)
    {
        try
        {
            var fileBytes = await _service.ExportToExcelAsync(kanalId);

            if (fileBytes == null || fileBytes.Length == 0)
                return BadRequest(new { Message = "No data found to export." });

            var fileName = $"Calls_{kanalId}_{DateTime.UtcNow:yyyyMMdd}.xlsx";

            return File(fileBytes, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", fileName);
        }
        catch (Exception ex)
        {
            return BadRequest(new { Message = $"An error occurred: {ex.Message}" });
        }
    }


    /// <summary>
    /// Create a new call record
    /// </summary>
    /// <param name="dto">The details for the new call</param>
    /// <param name="kanalId">The channel to which the call belongs</param>
    /// <returns>A response with the created call data or an error message</returns>
    [HttpPost]
    [Authorize(Policy = "BaşOperator")]

    public async Task<IActionResult> Create(CreateCallCenterDto dto, long kanalId)
    {
        var res = await _service.Create(dto);
        if (!res.Success)
            return BadRequest(res.Message);

        return Ok(res);
    }


    /// <summary>
    /// Get all calls by channel ID and user ID with pagination
    /// </summary>
    /// <param name="userId">The user ID</param>
    /// <param name="pageNumber">The page number for pagination</param>
    /// <param name="pageSize">The number of records per page</param>
    /// <returns>A paginated list of calls by channel and user</returns>
    [HttpGet("User/{userId}/Kanal/{kanalId}")]
    [Authorize(Policy = "Operator")]

    public async Task<IActionResult> GetAllByUser(long userId, long kanalId, int pageNumber, int pageSize)
    {
        var res = await _service.GetAllByUser(userId, kanalId, pageNumber, pageSize);
        if (!res.Success)
            return BadRequest(res.Message);

        return Ok(res);
    }



    /// <summary>
    /// Get all calls by channel ID and user ID with pagination
    /// </summary>
    /// <param name="kanalId">The kanal ID</param>
    /// <param name="pageNumber">The page number for pagination</param>
    /// <param name="pageSize">The number of records per page</param>
    /// <returns>A paginated list of calls by channel and user</returns>
    [HttpGet("Kanal/{kanalId}")]
    [Authorize(Policy = "Viewer")]

    public async Task<IActionResult> GetAll(long kanalId, int pageNumber, int pageSize)
    {
        var res = await _service.GetAll(kanalId, pageNumber, pageSize);
        if (!res.Success)
            return BadRequest(res.Message);

        return Ok(res);
    }

    /// <summary>
    /// Get a call by its ID
    /// </summary>
    /// <param name="id">The call ID</param>
    /// <returns>The call details if found</returns>
    [HttpGet("{id}")]
    [Authorize]

    public async Task<IActionResult> GetById(long id)
    {
        var res = await _service.GetById(id);
        if (!res.Success)
            return BadRequest(res.Message);

        return Ok(res);
    }
}
