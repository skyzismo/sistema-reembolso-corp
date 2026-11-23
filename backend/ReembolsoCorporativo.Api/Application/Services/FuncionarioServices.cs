using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using ReembolsoCorporativo.Api.Domain.Entities;
using ReembolsoCorporativo.Api.Domain.Enums;
using ReembolsoCorporativo.Api.Infrastructure.Persistence;

namespace ReembolsoCorporativo.Api.Application.Services;

public class FuncionarioSeedService
{
    private readonly AppDbContext _context;
    private readonly IConfiguration _configuration;
    private readonly IPasswordHasher<Usuario> _passwordHasher;

    public FuncionarioSeedService(
        AppDbContext context,
        IConfiguration configuration,
        IPasswordHasher<Usuario> passwordHasher)
    {
        _context = context;
        _configuration = configuration;
        _passwordHasher = passwordHasher;
    }

    public async Task SeedAsync()
    {
        var nome = _configuration["Funcionario:Nome"];
        var email = _configuration["Funcionario:Email"];
        var senha = _configuration["Funcionario:Senha"];

        if (string.IsNullOrWhiteSpace(nome) ||
            string.IsNullOrWhiteSpace(email) ||
            string.IsNullOrWhiteSpace(senha))
        {
            throw new InvalidOperationException(
                "As configurações do funcionário de teste não foram encontradas.");
        }

        email = email.Trim().ToLowerInvariant();

        var funcionarioExiste = await _context.Usuarios
            .AnyAsync(usuario => usuario.Email == email);

        if (funcionarioExiste)
            return;

        var funcionario = new Usuario(
            nome,
            email,
            PerfilUsuario.Funcionario);

        var senhaHash = _passwordHasher.HashPassword(
            funcionario,
            senha);

        funcionario.DefinirSenhaHash(senhaHash);

        _context.Usuarios.Add(funcionario);

        await _context.SaveChangesAsync();
    }
}
