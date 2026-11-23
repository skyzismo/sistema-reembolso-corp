using System.ComponentModel.DataAnnotations;
using ReembolsoCorporativo.Api.Domain.Enums;

namespace ReembolsoCorporativo.Api.Contracts.Dtos;

public class CreateUsuarioRequestDto
{
    [Required]
    [StringLength(100, MinimumLength = 2)]
    public string Nome { get; set; } = string.Empty;

    [Required]
    [EmailAddress]
    public string Email { get; set; } = string.Empty;

    [Required]
    [MinLength(8)]
    public string Senha { get; set; } = string.Empty;

    [EnumDataType(typeof(PerfilUsuario))]
    public PerfilUsuario Perfil { get; set; }
}
