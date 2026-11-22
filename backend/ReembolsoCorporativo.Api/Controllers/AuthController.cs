using Microsoft.AspNetCore.Mvc;
using ReembolsoCorporativo.Api.Application.Services;
using ReembolsoCorporativo.Api.Contracts.Dtos;

namespace ReembolsoCorporativo.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;

    public AuthController(IAuthService authService)
    {
        _authService = authService;
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login(
        [FromBody] LoginRequestDto request)
    {
        var resultado = await _authService.LoginAsync(request);

        if (resultado is null)
        {
            return Unauthorized(new
            {
                mensagem = "E-mail ou senha inválidos."
            });
        }

        return Ok(resultado);
    }
}
