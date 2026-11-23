
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ReembolsoCorporativo.Api.Application.Services;
using ReembolsoCorporativo.Api.Contracts.Dtos;

namespace ReembolsoCorporativo.Api.Controllers;

[ApiController]
[Route("api/despesas")]
[Authorize]
public class DespesasController : ControllerBase
{
    private readonly IDespesaService _service;

    public DespesasController(IDespesaService service)
    {
        _service = service;
    }

    [HttpPost]
    [ProducesResponseType(typeof(DespesaResponseDto), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    public IActionResult Criar([FromBody] CreateDespesaRequestDto request)
    {
        if (!request.Data.HasValue)
        {
            return BadRequest("A data da despesa é obrigatória.");
        }
        
        if (request.Data.Value.Date > DateTime.Today)
        {
            return BadRequest(
                "Não é permitido cadastrar despesas com data futura.");
        }

        try
        {
            var response = _service.Criar(request);
            return Created(string.Empty, response);
        }
        catch (InvalidOperationException ex)
        {
            return Conflict(ex.Message);
        }
    }

    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<DespesaResponseDto>), StatusCodes.Status200OK)]
    public IActionResult Listar()
    {
        var despesas = _service.Listar();
        return Ok(despesas);
    }

    [HttpPut("{id:guid}/aprovar")]
    [Authorize(Roles = "Gestor")]
    public IActionResult Aprovar(Guid id)
    {
        try
        {
            var resultado = _service.Aprovar(id);

            if (!resultado)
                return NotFound("Despesa não encontrada.");

            return Ok("Despesa aprovada com sucesso.");
        }
        catch (InvalidOperationException ex)
        {
            return Conflict(ex.Message);
        }
    }

    [HttpPut("{id:guid}/rejeitar")]
    [Authorize(Roles = "Gestor")]
    public IActionResult Rejeitar(Guid id)
    {
        try
        {
            var resultado = _service.Rejeitar(id);

            if (!resultado)
                return NotFound("Despesa não encontrada.");

            return Ok("Despesa rejeitada com sucesso.");
        }
        catch (InvalidOperationException ex)
        {
            return Conflict(ex.Message);
        }
    }

    [HttpDelete("{id:guid}")]
    [Authorize(Roles = "Gestor")]
    public IActionResult Excluir(Guid id)
    {
        try
        {
            var resultado = _service.Excluir(id);

            if (!resultado)
                return NotFound("Despesa não encontrada.");

            return NoContent();
        }
        catch (InvalidOperationException ex)
        {
            return Conflict(ex.Message);
        }
    }
}
