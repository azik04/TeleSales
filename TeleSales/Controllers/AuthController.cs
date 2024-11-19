using Microsoft.AspNetCore.Mvc;
using TeleSales.Core.Dto.AUTH;
using TeleSales.Core.Interfaces.Auth;

namespace TeleSales.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AccountController : ControllerBase
{
    private readonly IAuthService _service;
    public AccountController(IAuthService service)
    {
        _service = service;
    }


    [HttpPost]
    public async Task<IActionResult> LogIn(AuthDto dto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState); 

        var res = await _service.LogIn(dto);
        return Ok(res);
    }

}