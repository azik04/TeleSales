using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TeleSales.Core.Dto.User;
using TeleSales.Core.Interfaces.User;
using TeleSales.DataProvider.Enums;

namespace TeleSales.Areas.Admin.Controllers;

[Route("api/Admin/[controller]")]
[ApiController]
[Area("Admin")]
public class UserController : ControllerBase
{
    private readonly IUserService _service;

    public UserController(IUserService service)
    {
        _service = service;
    }

    /// <summary>
    /// Create a new User
    /// </summary>
    /// <param name="dto">The data for creating a new User</param>
    /// <returns>A response with the created User data or an error message</returns>
    [HttpPost]
    public async Task<IActionResult> Create(CreateUserDto dto)
    {
        var res = await _service.Create(dto);
        if (!res.Success)
            return BadRequest(res.Message);

        return Ok(res);
    }
    

    /// <summary>
    /// Get all Users
    /// </summary>
    /// <returns>A response with a list of all Users or an error message</returns>
    /// <summary>
    /// Get all Admin Users
    /// </summary>
    /// <returns>A response with a list of all Users or an error message</returns>
    [HttpGet("Admin")]
    [Authorize(Policy = "Admin")]
    public async Task<IActionResult> GetAllAdmin()
    {
        var res = await _service.GetAllAdmin();
        if (!res.Success)
            return BadRequest(res.Message);

        return Ok(res);
    }


    /// <summary>
    /// Get all User Users
    /// </summary>
    /// <returns>A response with a list of all Users or an error message</returns>
    [HttpGet("User")]
    [Authorize(Policy = "Admin")]
    public async Task<IActionResult> GetAllUser()
    {
        var res = await _service.GetAllUser();
        if (!res.Success)
            return BadRequest(res.Message);

        return Ok(res);
    }


    /// <summary>
    /// Get all User Users
    /// </summary>
    /// <returns>A response with a list of all Users or an error message</returns>
    [HttpGet()]
    [Authorize(Policy = "Admin")]
    public async Task<IActionResult> GetAll()
    {
        var res = await _service.GetAll();
        if (!res.Success)
            return BadRequest(res.Message);

        return Ok(res);
    }


    /// <summary>
    /// Get all Viewer Users
    /// </summary>
    /// <returns>A response with a list of all Users or an error message</returns>
    [HttpGet("Viewer")]
    [Authorize(Policy = "Admin")]
    public async Task<IActionResult> GetAllViewer()
    {
        var res = await _service.GetAllViewer();
        if (!res.Success)
            return BadRequest(res.Message);

        return Ok(res);
    }



    /// <summary>
    /// Get all BasOperator Users
    /// </summary>
    /// <returns>A response with a list of all Users or an error message</returns>
    [HttpGet("BasOperator")]
    [Authorize(Policy = "Admin")]
    public async Task<IActionResult> GetAllBasOperator()
    {
        var res = await _service.GetAllBasOperator();
        if (!res.Success)
            return BadRequest(res.Message);

        return Ok(res);
    }


    /// <summary>
    /// Change a Users Role by its ID
    /// </summary>
    /// <param name="id">The ID of the User to update</param>
    /// <returns>A response with the updated Users Role or an error message</returns>
    [HttpPut("{id}/Role")]
    [Authorize(Policy = "Admin")]
    public async Task<IActionResult> Role(long id , Role role)
    {
        var res = await _service.ChangeRole(id , role);
        if (!res.Success)
            return BadRequest(res.Message);

        return Ok(res);
    }

    /// <summary>
    /// Remove a User by its ID
    /// </summary>
    /// <param name="id">The ID of the User to remove</param>
    /// <returns>A response indicating whether the User was successfully removed or an error message</returns>
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
