using ReembolsoCorporativo.Api.Contracts.Dtos;

namespace ReembolsoCorporativo.Api.Application.Services;

public interface IDespesaService
{
    DespesaResponseDto Criar(CreateDespesaRequestDto request);
}
