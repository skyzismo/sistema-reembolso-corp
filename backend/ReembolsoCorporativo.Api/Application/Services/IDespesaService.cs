using ReembolsoCorporativo.Api.Contracts.Dtos;

namespace ReembolsoCorporativo.Api.Application.Services;

public interface IDespesaService
{
    DespesaResponseDto Criar(CreateDespesaRequestDto request);

    IEnumerable<DespesaResponseDto> Listar();

    bool Aprovar(Guid id);

    bool Rejeitar(Guid id);

    bool Excluir(Guid id);
}
