using Microsoft.AspNetCore.Mvc;
using ReembolsoCorporativo.Api.Application.Services;
using ReembolsoCorporativo.Api.Contracts.Dtos;

namespace ReembolsoCorporativo.Api.Controllers;

[ApiController]
[Route("api/despesas")]
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
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    public IActionResult Criar([FromBody] CreateDespesaRequestDto request)
    {
        if (request.Data.Date > DateTime.Today)
        {
            return BadRequest("Não é permitido cadastrar despesas com data futura.");
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

}
