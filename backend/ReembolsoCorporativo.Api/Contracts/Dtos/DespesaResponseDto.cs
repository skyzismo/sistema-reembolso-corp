using ReembolsoCorporativo.Api.Domain.Enums;

namespace ReembolsoCorporativo.Api.Contracts.Dtos;

public class DespesaResponseDto
{
    public Guid Id { get; set; }
    public TipoDespesa Tipo { get; set; }
    public decimal Valor { get; set; }
    public DateTime Data { get; set; }
    public StatusDespesa Status { get; set; }
}
