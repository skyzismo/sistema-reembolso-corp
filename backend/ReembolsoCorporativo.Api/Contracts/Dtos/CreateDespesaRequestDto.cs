using ReembolsoCorporativo.Api.Domain.Enums;

namespace ReembolsoCorporativo.Api.Contracts.Dtos;

public class CreateDespesaRequestDto
{
    public TipoDespesa Tipo { get; set; }
    public decimal Valor { get; set; }
    public DateTime Data { get; set; }
}
