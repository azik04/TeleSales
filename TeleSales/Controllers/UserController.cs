using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TeleSales.Core.Dto.User;
using TeleSales.Core.Interfaces.User;

namespace TeleSales.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UserController : ControllerBase
{
    private readonly IUserService _service;

    public UserController(IUserService service)
    {
        _service = service;
    }


    /// <summary>
    /// Get a User by its ID
    /// </summary>
    /// <param name="id">The ID of the User</param>
    /// <returns>A response with the User data or an error message</returns>
    [HttpGet("{id}")]
    [Authorize(Policy = "User")]

    public async Task<IActionResult> GetById(long id)
    {
        var res = await _service.GetById(id);
        if (!res.Success)
            return BadRequest(res.Message);

        return Ok(res);
    }

    /// <summary>
    /// Get all Users
    /// </summary>
    /// <returns>A response with a list of all Users or an error message</returns>
    [HttpGet]
    [Authorize(Policy = "User")]

    public async Task<IActionResult> GetAll()
    {
        var res = await _service.GetAll();
        if (!res.Success)
            return BadRequest(res.Message);

        return Ok(res);
    }


    /// <summary>
    /// Update a User by its ID
    /// </summary>
    /// <param name="id">The ID of the User to update</param>
    /// <param name="dto">The updated data for the User</param>
    /// <returns>A response with the updated User data or an error message</returns>
    [HttpPut("{id}")]
    [Authorize(Policy = "User")]

    public async Task<IActionResult> Update(long id, UpdateUserDto dto)
    {
        var res = await _service.Update(id, dto);
        if (!res.Success)
            return BadRequest(res.Message);

        return Ok(res);
    }



    /// <summary>
    /// Update a Users Password by its ID
    /// </summary>
    /// <param name="id">The ID of the User to update</param>
    /// <param name="dto">The updated data for the Users Password</param>
    /// <returns>A response with the updated User data or an error message</returns>
    [HttpPut("{id}/ChangePassword")]
    [Authorize(Policy = "User")]

    public async Task<IActionResult> ChangePassword(long id, ChangePasswordDto dto)
    {
        var res = await _service.ChangePassword(id, dto);
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
    [Authorize(Policy = "User")]

    public async Task<IActionResult> Remove(long id)
    {
        var res = await _service.Remove(id);
        if (!res.Success)
            return BadRequest(res.Message);

        return Ok(res);
    }
}
