using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TeleSales.Core.Dto.Call;
using TeleSales.Core.Interfaces.Call;

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
    /// Get all calls that are not excluded
    /// </summary>
    /// <param name="kanalId">The channel ID</param>
    /// <param name="pageNumber">The page number for pagination</param>
    /// <param name="pageSize">The number of records per page</param>
    /// <returns>A paginated list of calls that are not excluded</returns>
    [HttpGet("NotExcluded")]
    [Authorize(Policy = "Viewer")]
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
    [Authorize(Policy = "Viewer")]

    public async Task<IActionResult> GetAllExcluded(long kanalId, int pageNumber, int pageSize)
    {
        var res = await _service.GetAllExcluded(kanalId, pageNumber, pageSize);
        if (!res.Success)
            return BadRequest(res.Message);

        return Ok(res);
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
        var res = await _service.GetAllByUser(userId, kanalId , pageNumber, pageSize);
        if (!res.Success)
            return BadRequest(res.Message);

        return Ok(res);
    }


    /// <summary>
    /// Get a random call
    /// </summary>
    /// <returns>A random call from the database</returns>
    [HttpGet("Random")]
    [Authorize(Policy = "Operator")]
    public async Task<IActionResult> GetRandomCall(long kanalId)
    {
        var res = await _service.GetRandomCallsByVoen(kanalId);
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


    /// <summary>
    /// Exclude a call by its ID
    /// </summary>
    /// <param name="id">The call ID</param>
    /// <param name="dto">The details for excluding the call</param>
    /// <returns>The excluded call or an error message</returns>
    [HttpPut("Exclude/{id}")]
    [Authorize(Policy = "Operator")]

    public async Task<IActionResult> Exclude(long id, ExcludeCallDto dto)
    {
        var res = await _service.Exclude(id, dto);
        if (!res.Success)
            return BadRequest(res.Message);

        return Ok(res);
    }
}
