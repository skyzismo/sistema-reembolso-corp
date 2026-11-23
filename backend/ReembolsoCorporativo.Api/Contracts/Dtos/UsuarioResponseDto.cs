using ReembolsoCorporativo.Api.Domain.Enums;

namespace ReembolsoCorporativo.Api.Contracts.Dtos;

public class UsuarioResponseDto
{
    public Guid Id { get; set; }
    public string Nome { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public PerfilUsuario Perfil { get; set; }
}
