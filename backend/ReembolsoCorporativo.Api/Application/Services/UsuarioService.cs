
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using ReembolsoCorporativo.Api.Contracts.Dtos;
using ReembolsoCorporativo.Api.Domain.Entities;
using ReembolsoCorporativo.Api.Infrastructure.Persistence;

namespace ReembolsoCorporativo.Api.Application.Services;

public class UsuarioService
{
    private readonly AppDbContext _context;
    private readonly IPasswordHasher<Usuario> _passwordHasher;

    public UsuarioService(
        AppDbContext context,
        IPasswordHasher<Usuario> passwordHasher)
    {
        _context = context;
        _passwordHasher = passwordHasher;
    }

    public async Task<UsuarioResponseDto> CriarAsync(
        CreateUsuarioRequestDto request)
    {
        var email = request.Email.Trim().ToLowerInvariant();

        var emailExiste = await _context.Usuarios
            .AnyAsync(u => u.Email == email);

        if (emailExiste)
            throw new InvalidOperationException(
                "Já existe um usuário cadastrado com este e-mail.");

        var usuario = new Usuario(
            request.Nome,
            email,
            request.Perfil);

        var senhaHash = _passwordHasher.HashPassword(
            usuario,
            request.Senha);

        usuario.DefinirSenhaHash(senhaHash);

        _context.Usuarios.Add(usuario);
        await _context.SaveChangesAsync();

        return new UsuarioResponseDto
        {
            Id = usuario.Id,
            Nome = usuario.Nome,
            Email = usuario.Email,
            Perfil = usuario.Perfil
        };
    }
}
