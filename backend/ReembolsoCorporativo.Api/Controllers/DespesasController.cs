using Microsoft.AspNetCore.Mvc;
using ReembolsoCorporativo.Api.Contracts.Dtos;
using ReembolsoCorporativo.Api.Domain.Entities;

namespace ReembolsoCorporativo.Api.Controllers;

[ApiController]
[Route("api/despesas")]
public class DespesasController : ControllerBase
{
    [HttpPost]
    public IActionResult Criar([FromBody] CreateDespesaRequestDto request)
    {
        if (request.Data.Date > DateTime.Today)
        {
            return BadRequest("Não é permitido cadastrar despesas com data futura.");
        }

        var despesa = new Despesa(
            request.Tipo,
            request.Valor,
            request.Data
        );

        var response = new DespesaResponseDto
        {
            Id = despesa.Id,
            Tipo = despesa.Tipo,
            Valor = despesa.Valor,
            Data = despesa.Data,
            Status = despesa.Status
        };

        return Created(string.Empty, response);
    }
}
