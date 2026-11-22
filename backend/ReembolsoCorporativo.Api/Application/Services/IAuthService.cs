using ReembolsoCorporativo.Api.Contracts.Dtos;

namespace ReembolsoCorporativo.Api.Application.Services;

public interface IAuthService
{
    Task<LoginResponseDto?> LoginAsync(LoginRequestDto request);
}
