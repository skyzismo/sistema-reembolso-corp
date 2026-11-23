using System.ComponentModel.DataAnnotations;
using ReembolsoCorporativo.Api.Domain.Enums;

namespace ReembolsoCorporativo.Api.Contracts.Dtos;

public class CreateDespesaRequestDto
{
    [EnumDataType(typeof(TipoDespesa))]
    public TipoDespesa Tipo { get; set; }

    [Range(typeof(decimal), "0.01", "999999999")]
    public decimal Valor { get; set; }

    [Required]
    public DateTime? Data { get; set; }
}
