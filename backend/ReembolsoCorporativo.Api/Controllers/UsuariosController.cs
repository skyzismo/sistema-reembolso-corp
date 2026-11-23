
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ReembolsoCorporativo.Api.Application.Services;
using ReembolsoCorporativo.Api.Contracts.Dtos;

namespace ReembolsoCorporativo.Api.Controllers;

[ApiController]
[Route("api/usuarios")]
[Authorize(Roles = "Gestor")]
public class UsuariosController : ControllerBase
{
    private readonly UsuarioService _service;

    public UsuariosController(UsuarioService service)
    {
        _service = service;
    }

    [HttpPost]
    [ProducesResponseType(typeof(UsuarioResponseDto), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    public async Task<IActionResult> Criar(
        [FromBody] CreateUsuarioRequestDto request)
    {
        try
        {
            var usuario = await _service.CriarAsync(request);

            return StatusCode(
                StatusCodes.Status201Created,
                usuario);
        }
        catch (InvalidOperationException ex)
        {
            return Conflict(new { mensagem = ex.Message });
        }
        catch (DbUpdateException)
        {
            return Conflict(new
            {
                mensagem = "Não foi possível cadastrar o usuário. Verifique se o e-mail já está em uso."
            });
        }
    }

    [HttpGet]
    [ProducesResponseType(typeof(List<UsuarioResponseDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    public async Task<IActionResult> Listar()
    {
        var usuarios = await _service.ListarAsync();
    
        return Ok(usuarios);
    }
}
