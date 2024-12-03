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
    /// Import calls from an Excel file
    /// </summary>
    /// <param name="file">The Excel file containing call data</param>
    /// <returns>A response with the imported call data or error message</returns>
    [HttpPost("import")]
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
    /// Export calls to an Excel file
    /// </summary>
    /// <param name="kanalId">The channel ID to filter calls</param>
    /// <returns>The Excel file containing the exported call data</returns>
    [HttpGet("ExportExcel")]
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
    /// Export calls to an Pdf file
    /// </summary>
    /// <param name="kanalId">The channel ID to filter calls</param>
    /// <returns>The Pdf file containing the exported call data</returns>
    [HttpGet("ExportPdf")]
    public async Task<IActionResult> ExportToPdfAsync(long kanalId)
    {
        try
        {
            var pdfBytes = await _service.ExportToPdfAsync(kanalId);

            if (pdfBytes == null || pdfBytes.Length == 0)
                return BadRequest(new { Message = "No data found to export." });

            var fileName = $"Calls_{kanalId}_{DateTime.UtcNow:yyyyMMdd}.pdf";

            return File(pdfBytes, "application/pdf", fileName);
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
    public async Task<IActionResult> Create(CreateCallDto dto, long kanalId)
    {
        var res = await _service.Create(dto);
        if (!res.Success)
            return BadRequest(res.Message);

        return Ok(res);
    }

    /// <summary>
    /// Get all calls by channel ID with pagination
    /// </summary>
    /// <param name="kanalId">The channel ID</param>
    /// <param name="pageNumber">The page number for pagination</param>
    /// <param name="pageSize">The number of records per page</param>
    /// <returns>A paginated list of calls associated with the given channel</returns>
    [HttpGet("{kanalId}/Kanal")]
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
    public async Task<IActionResult> GetAllExcluded(long kanalId, int pageNumber, int pageSize)
    {
        var res = await _service.GetAllExcluded(kanalId, pageNumber, pageSize);
        if (!res.Success)
            return BadRequest(res.Message);

        return Ok(res);
    }

    /// <summary>
    /// Get all calls by channel ID and user ID with pagination
    /// </summary>
    /// <param name="kanalId">The channel ID</param>
    /// <param name="userId">The user ID</param>
    /// <param name="pageNumber">The page number for pagination</param>
    /// <param name="pageSize">The number of records per page</param>
    /// <returns>A paginated list of calls by channel and user</returns>
    [HttpGet("{kanalId}/Kanal/User/{userId}")]
    public async Task<IActionResult> GetAllByKanalAndUser(long kanalId, long userId, int pageNumber, int pageSize)
    {
        var res = await _service.GetAllByKanalAndUser(kanalId, userId, pageNumber, pageSize);
        if (!res.Success)
            return BadRequest(res.Message);

        return Ok(res);
    }

    /// <summary>
    /// Get a random call
    /// </summary>
    /// <returns>A random call from the database</returns>
    [HttpGet("Random")]
    public async Task<IActionResult> GetRandomCall()
    {
        var res = await _service.GetRandomCall();
        if (!res.Success)
            return BadRequest(res.Message);

        return Ok(res);
    }

    /// <summary>
    /// Search for calls based on a query string
    /// </summary>
    /// <param name="query">The search query</param>
    /// <param name="pageNumber">The page number for pagination</param>
    /// <param name="pageSize">The number of records per page</param>
    /// <returns>A paginated list of calls that match the search query</returns>
    [HttpGet("Search")]
    public async Task<IActionResult> FindAsync(string query, int pageNumber, int pageSize)
    {
        var res = await _service.FindAsync(query, pageNumber, pageSize);
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
    public async Task<IActionResult> GetById(long id)
    {
        var res = await _service.GetById(id);
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
    public async Task<IActionResult> Update(long id, UpdateCallDto dto)
    {
        var res = await _service.Update(id, dto);
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
    public async Task<IActionResult> Exclude(long id, ExcludeCallDto dto)
    {
        var res = await _service.Exclude(id, dto);
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
    public async Task<IActionResult> Remove(long id)
    {
        var res = await _service.Remove(id);
        if (!res.Success)
            return BadRequest(res.Message);

        return Ok(res);
    }
}
