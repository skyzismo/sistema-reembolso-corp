
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using ReembolsoCorporativo.Api.Domain.Entities;
using ReembolsoCorporativo.Api.Domain.Enums;
using ReembolsoCorporativo.Api.Infrastructure.Persistence;

namespace ReembolsoCorporativo.Api.Application.Services;

public class AdminSeedService
{
    private readonly AppDbContext _context;
    private readonly IConfiguration _configuration;
    private readonly IPasswordHasher<Usuario> _passwordHasher;

    public AdminSeedService(
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
        var nome = _configuration["Admin:Nome"];
        var email = _configuration["Admin:Email"];
        var senha = _configuration["Admin:Senha"];

        if (string.IsNullOrWhiteSpace(nome) ||
            string.IsNullOrWhiteSpace(email) ||
            string.IsNullOrWhiteSpace(senha))
        {
            throw new InvalidOperationException(
                "As configurações iniciais do administrador não foram encontradas.");
        }

        email = email.Trim().ToLowerInvariant();

        var administradorExiste = await _context.Usuarios
            .AnyAsync(usuario => usuario.Email == email);

        if (administradorExiste)
            return;

        var administrador = new Usuario(
            nome,
            email,
            PerfilUsuario.Gestor);

        var senhaHash = _passwordHasher.HashPassword(
            administrador,
            senha);

        administrador.DefinirSenhaHash(senhaHash);

        _context.Usuarios.Add(administrador);

        await _context.SaveChangesAsync();
    }
}
