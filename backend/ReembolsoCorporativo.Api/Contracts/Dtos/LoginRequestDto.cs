using System.ComponentModel.DataAnnotations;

namespace ReembolsoCorporativo.Api.Contracts.Dtos;

public class LoginRequestDto
{
    [Required]
    [EmailAddress]
    public string Email { get; set; } = string.Empty;

    [Required]
    public string Senha { get; set; } = string.Empty;
}
