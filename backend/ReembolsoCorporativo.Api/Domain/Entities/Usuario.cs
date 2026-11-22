using ReembolsoCorporativo.Api.Domain.Enums;

namespace ReembolsoCorporativo.Api.Domain.Entities;

public class Usuario
{
    public Guid Id { get; private set; }

    public string Nome { get; private set; } = string.Empty;

    public string Email { get; private set; } = string.Empty;

    public string SenhaHash { get; private set; } = string.Empty;

    public PerfilUsuario Perfil { get; private set; }

    protected Usuario() { }

    public Usuario(string nome, string email, PerfilUsuario perfil)
    {
        if (string.IsNullOrWhiteSpace(nome))
            throw new ArgumentException("O nome é obrigatório.");

        if (string.IsNullOrWhiteSpace(email))
            throw new ArgumentException("O email é obrigatório.");

        if (!email.Contains('@'))
            throw new ArgumentException("O email informado é inválido.");

        if (!Enum.IsDefined(perfil))
            throw new ArgumentException("O perfil informado é inválido.");

        Id = Guid.NewGuid();
        Nome = nome.Trim();
        Email = email.Trim().ToLowerInvariant();
        Perfil = perfil;
    }

    public void DefinirSenhaHash(string senhaHash)
    {
        if (string.IsNullOrWhiteSpace(senhaHash))
            throw new ArgumentException("O hash da senha é obrigatório.");

        SenhaHash = senhaHash;
    }
}
